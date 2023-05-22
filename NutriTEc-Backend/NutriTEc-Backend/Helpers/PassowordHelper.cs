using System.Text;

namespace NutriTEc_Backend.Helpers
{
    public class PassowordHelper
    {
        public static string EncodePasswordMD5(string originalPassword)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(originalPassword);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +
            }
        }

    }
}
