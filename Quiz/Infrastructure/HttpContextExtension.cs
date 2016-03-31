using System.Web;
using System.Web.Security;
using Newtonsoft.Json;

namespace Quiz.Infrastructure
{
    public static class HttpContextExtensions
    {
        public static string GetCurrentLogonUserIdentifier(this HttpContextBase context)
        {
            var cookie = context.Request.Cookies?[FormsAuthentication.FormsCookieName];

            if (cookie == null)
            {
                return string.Empty;
            }

            var decrypted = FormsAuthentication.Decrypt(cookie.Value);

            if (string.IsNullOrEmpty(decrypted?.UserData))
            {
                return string.Empty;

            }

            var identifier = JsonConvert.DeserializeObject(decrypted.UserData);
            return identifier.ToString();
        }
    }
}