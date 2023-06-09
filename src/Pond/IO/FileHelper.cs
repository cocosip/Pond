using System.IO;

namespace Pond.IO
{
    internal static class FileHelper
    {
        internal static void DeleteIfExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
