using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Pond
{
    public static class PondFileExtensions
    {
        /// <summary>
        /// Generate pondFile code
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GenerateCode(this PondFile file)
        {
            var source = $"{file.FilePoolName}{file.Worker}{file.Path}";
            var sourceBytes = Encoding.UTF8.GetBytes(source);
            using (var sha1 = SHA1.Create())
            {
                var buffer = sha1.ComputeHash(sourceBytes);
                return buffer.Aggregate("", (current, b) => current + b.ToString("X2"));
            }
        }
    }
}
