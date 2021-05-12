using System;

namespace Pond
{
    public class PoolFile : IEquatable<PoolFile>
    {
        /// <summary>
        /// The name of file pool
        /// </summary>
        public string FilePool { get; set; }

        /// <summary>
        /// The index of worker
        /// </summary>
        public int Worker { get; set; }

        /// <summary>
        /// File path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// File extension
        /// </summary>
        public string FileExt
        {
            get
            {
                return System.IO.Path.GetExtension(Path);
            }
        }


        public PoolFile()
        {

        }

        public PoolFile(string filePool, int worker) : this(filePool, worker, string.Empty)
        {

        }

        public PoolFile(string filePool, int worker, string path)
        {
            FilePool = filePool;
            Worker = worker;
            Path = path;
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public PoolFile Clone()
        {
            return new PoolFile(FilePool, Worker, Path);
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PoolFile other)
        {
            if (other == null)
            {
                return false;
            }

            return FilePool == other.FilePool && Worker == other.Worker && Path == other.Path;
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            return obj is PoolFile other && Equals(other);
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return StringComparer.InvariantCulture.GetHashCode(FilePool) | StringComparer.InvariantCulture.GetHashCode(Path) | Worker.GetHashCode();
        }
    }
}
