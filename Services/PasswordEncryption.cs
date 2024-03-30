using MarketingEvent.Api.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace MarketingEvent.Api.Services
{
    public static class PasswordEncryption
    {
        private static byte[] key = new byte[32];

        static PasswordEncryption()
        {
            byte[] initialKey = Encoding.ASCII.GetBytes(ConfigurationHelper.Configuration["Jwt:Key"]);
            Array.Copy(initialKey, key, Math.Min(initialKey.Length, 32));
        }

        public static string EncryptPassword(string username, string password)
        {
            using (var manager = Aes.Create())
            {
                manager.Key = key;
                var ciphertext = manager.EncryptCbc(Encoding.UTF8.GetBytes(password), GetIVFromUsername(username));
                var base64ciphertext = Convert.ToBase64String(ciphertext);
                return base64ciphertext;
            }
        }

        private static byte[] GetIVFromUsername(string username)
        {
            byte[] initialIV = Encoding.UTF8.GetBytes(username);
            byte[] IV = new byte[16];
            Array.Copy(initialIV, IV, Math.Min(initialIV.Length, 16));
            return IV;
        }
    }
}
