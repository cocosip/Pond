using System.IO;
using System.Text.RegularExpressions;

namespace Pond.Utilities
{
    public static class WorkerUtil
    {

        /// <summary>
        /// Generate worker name by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GenerateWorkerName(int index)
        {
            return $"_{index.ToString().PadLeft(6, '0')}_";
        }

        /// <summary>
        /// Generate worker path by worker name
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GenerateWorkerPath(string path, string name)
        {
            return Path.Combine(path, name);
        }

        /// <summary>
        /// Judge workName is right
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsWorkerName(string name)
        {
            return Regex.IsMatch(name, @"^_[\d]{6}_$");
        }

        /// <summary>
        /// Get worker index by worker name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int GetWorkerIndex(string name)
        {
            if (int.TryParse(name.Replace('_', ' '), out int r))
            {
                return r;
            }
            return 0;
        }
    }
}
