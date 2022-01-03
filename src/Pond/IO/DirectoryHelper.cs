using System.IO;

namespace Pond.IO
{
    internal static class DirectoryHelper
    {
        public static void CreateIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public static void DeleteIfExist(string directory, bool recursive = false)
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, recursive);
            }
        }

        public static void DirectoryCopy(string sourceDir, string targetDir)
        {
            CreateIfNotExists(targetDir);
            DirectoryInfo dir = new(sourceDir);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
            foreach (FileSystemInfo i in fileinfo)
            {
                if (i is DirectoryInfo)
                {
                    DirectoryCopy(i.FullName, Path.Combine(targetDir, i.Name));
                }
                else
                {
                    File.Copy(i.FullName, Path.Combine(targetDir, i.Name), true);
                }
            }

        }

    }
}
