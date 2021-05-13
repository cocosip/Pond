using System.IO;

namespace Pond.Utilities
{
    public static class DirectoryHelper
    {
        /// <summary>
        /// Create directory if not exist
        /// </summary>
        public static void CreateIfNotExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// Delete directory if exist
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="recursive"></param>
        public static void DeleteIfExist(string directory, bool recursive = false)
        {
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, recursive);
            }
        }


        /// <summary>
        /// Copy directory
        /// </summary>
        public static void DirectoryCopy(string sourceDirectory, string targetDirectory)
        {

            CreateIfNotExists(targetDirectory);
            DirectoryInfo dir = new DirectoryInfo(sourceDirectory);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
            foreach (FileSystemInfo i in fileinfo)
            {
                //判断是否文件夹
                if (i is DirectoryInfo)
                {
                    //递归调用复制子文件夹
                    DirectoryCopy(i.FullName, Path.Combine(targetDirectory, i.Name));
                }
                else
                {
                    //拷贝文件,覆盖的形式
                    File.Copy(i.FullName, Path.Combine(targetDirectory, i.Name), true);
                }
            }

        }

    }
}
