using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Security;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using Quiz.Gameplay;
using Quiz.Models;

namespace Quiz.Hubs
{
    [Authorize]
    [HubName("quizHub")]
    public class QuizHub : Hub
    {
        private static readonly ConcurrentDictionary<string, User> Users
            = new ConcurrentDictionary<string, User>(StringComparer.InvariantCultureIgnoreCase);

        public static GameMaster GameMaster => new GameMaster();

        public User LogonUser => GetUser(Context.User.Identity.Name);

        public void PlayerIsReady(string playerName)
        {
            var users = GetUsersByIdentifier(LogonUser.Identifier);
            LogonUser.IsReadyForNextQuestion = true;
            foreach (var u in users)
            {
                Clients.User(u.Name).updateConnectedUsers();
            }

            CheckIfCanAskNextQuestion(users);
        }

        private void CheckIfCanAskNextQuestion(IEnumerable<User> users)
        {
            var enumerable = users as User[] ?? users.ToArray();
            if (enumerable.Count(u => u.IsReadyForNextQuestion) >= enumerable.Length)
            {
                GameMaster.ProceedToNextQuestion();
                foreach (var user in enumerable)
                {
                    Clients.User(user.Name).nextQuestion();
                    user.IsReadyForNextQuestion = false;
                }
            }
        }

        internal static object GetScore()
        {
            var listOfAnswers = GameMaster.PlayerAnswers;
            var scores = new List<Score>();
            foreach (var user in Users)
            {
                var score = new Score
                {
                    User = user.Value,
                    Points = listOfAnswers.Count(u => u.Key == user.Value && u.Value.IsCorrect)
                };
                scores.Add(score);
            }
            return scores;
        }

        public static IList<User> GetUsersByIdentifier(string identifier)
        {
            return Users.Values.Where(user => user.Identifier == identifier).ToList();
        }

        private string CurrentLogonUserIdentifier
        {
            get
            {
                if (Context.Request.Cookies != null)
                {
                    var cookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];

                    if (null == cookie)
                        return string.Empty;

                    var decrypted = FormsAuthentication.Decrypt(cookie.Value);

                    if (decrypted != null && !string.IsNullOrEmpty(decrypted.UserData))
                    {
                        var identifier = JsonConvert.DeserializeObject(decrypted.UserData);
                        return identifier.ToString();
                    }
                }
                return string.Empty;
            }
        }

        public IEnumerable<string> GetConnectedUsers()
        {
            return Users.Where(x =>
            {
                lock (x.Value.ConnectionIds)
                {
                    return
                        !x.Value.ConnectionIds.Contains(Context.ConnectionId, StringComparer.InvariantCultureIgnoreCase);
                }
            }).Select(x => x.Key);
        }

        public User GetUser(string username)
        {
            User user;
            Users.TryGetValue(username, out user);
            return user;
        }

        public Task PlayerAnswer(int answerId)
        {
            PlayerIsReady(LogonUser.Name);
            return  GameMaster.SetPlayerAnswer(LogonUser, answerId);
        }

        #region Connection
        public override Task OnConnected()
        {
            var userName = Context.User.Identity.Name;
            var connectionId = Context.ConnectionId;
            var identifier = CurrentLogonUserIdentifier;

            var user = Users.GetOrAdd(userName, _ => new User
            {
                Name = userName,
                ConnectionIds = new HashSet<string>(),
                Identifier = identifier,
            });


            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);

                foreach (var u in GetUsersByIdentifier(user.Identifier))
                {
                    Clients.User(u.Name).updateConnectedUsers();
                }
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var userName = Context.User.Identity.Name;
            var connectionId = Context.ConnectionId;

            var user = GetUser(userName);

            if (user != null)
            {
                lock (user.ConnectionIds)
                {
                    user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));
                    if (!user.ConnectionIds.Any())
                    {
                        User removedUser;
                        Users.TryRemove(userName, out removedUser);

                        foreach (var u in GetUsersByIdentifier(user.Identifier))
                        {
                            Clients.User(u.Name).updateConnectedUsers();
                        }
                    }
                }
            }

            return base.OnDisconnected(stopCalled);
        }
        #endregion
    }
}