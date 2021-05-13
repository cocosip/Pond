using System.IO;

namespace Pond.Utilities
{
    public static class FileHelper
    {
        /// <summary>
        /// Delete file if exist
        /// </summary>
        public static bool DeleteIfExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
