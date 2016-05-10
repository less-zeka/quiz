using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Models;

namespace Quiz.Gameplay
{
    public class GameMaster
    {
        private int CurrentRound { get; set; }
        public static Question CurrentQuestion { get; set; }
        private bool NewQuestionNeeded { get; set; }

        public static IList<KeyValuePair<User, Answer>> PlayerAnswers = new List<KeyValuePair<User, Answer>>();

        private static ICollection<Question> _questions;

        public GameMaster()
        {
            CurrentRound = 0;
            NewQuestionNeeded = true;
            if (_questions == null)
            {
                LoadQuestions();
                LoadNextQuestion();
            }
        }

        public void LoadNextQuestion()
        {
            if (!NewQuestionNeeded) return;
            NewQuestionNeeded = false;
            var randomNr = new Random().Next(0, _questions.Count);
            CurrentQuestion = _questions.ElementAt(randomNr);
        }

        public void ProceedToNextQuestion()
        {
            CurrentRound ++;
            NewQuestionNeeded = true;
            LoadNextQuestion();
        }

        private static void LoadQuestions()
        {
            _questions = new List<Question>
            {
                new Question
                {
                    QuestionText = "Wieviel gibt 2+2?",
                    Answers = new List<Answer>
                    {
                        new Answer {Id = 1, Text = "2", IsCorrect = false},
                        new Answer {Id = 2, Text = "1", IsCorrect = false},
                        new Answer {Id = 3, Text = "4", IsCorrect = true},
                        new Answer {Id = 4, Text = "3", IsCorrect = false}
                    }
                },
                new Question
                {
                    QuestionText = "Wieviel gibt 1+1?",
                    Answers = new List<Answer>
                    {
                        new Answer {Id = 1, Text = "2", IsCorrect = true},
                        new Answer {Id = 2, Text = "1", IsCorrect = false},
                        new Answer {Id = 3, Text = "4", IsCorrect = false},
                        new Answer {Id = 4, Text = "3", IsCorrect = false}
                    }
                },
                //new Question
                //{
                //    QuestionText = "Wer hat Recht?",
                //    Answers = new List<Answer>
                //    {
                //        new Answer {Id = 1, Text = "Sabine", IsCorrect = true},
                //        new Answer {Id = 2, Text = "Silvana", IsCorrect = true},
                //        new Answer {Id = 3, Text = "Alle", IsCorrect = true},
                //        new Answer {Id = 4, Text = "Keiner", IsCorrect = true}
                //    }
                //},
                //new Question
                //{
                //    QuestionText = "Wie wird das Wetter?",
                //    Answers = new List<Answer>
                //    {
                //        new Answer {Id = 1, Text = "Das Wetter wird nicht. Das Wetter ist.", IsCorrect = true},
                //        new Answer {Id = 2, Text = "Schön", IsCorrect = true},
                //        new Answer {Id = 3, Text = "42", IsCorrect = false},
                //        new Answer {Id = 4, Text = "Zu kalt!", IsCorrect = false}
                //    }
                //},
                new Question
                {
                    QuestionText = "C# ist ...",
                    Answers = new List<Answer>
                    {
                        new Answer {Id = 1, Text = "Ein preprocessing Compiler", IsCorrect = false},
                        new Answer {Id = 2, Text = "Eine Programmiersprache", IsCorrect = true},
                        new Answer {Id = 3, Text = "Ein Framework", IsCorrect = false},
                    }
                }
            };
        }

        public Task SetPlayerAnswer(User logonUser, int answerId)
        {
            PlayerAnswers.Add(
                new KeyValuePair<User, Answer>(
                    logonUser,
                    CurrentQuestion.Answers.FirstOrDefault(answer => answer.Id == answerId)));

            return Task.Run(() => { });
        }
    }
}