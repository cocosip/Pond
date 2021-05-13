using System;

namespace Pond
{
    public class PondFile : IEquatable<PondFile>
    {
        /// <summary>
        /// The name of file pool
        /// </summary>
        public string FilePoolName { get; set; }

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


        public PondFile()
        {

        }

        public PondFile(string filePool, int worker) : this(filePool, worker, string.Empty)
        {

        }

        public PondFile(string filePool, int worker, string path)
        {
            FilePoolName = filePool;
            Worker = worker;
            Path = path;
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public PondFile Clone()
        {
            return new PondFile(FilePoolName, Worker, Path);
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(PondFile other)
        {
            if (other == null)
            {
                return false;
            }

            return FilePoolName == other.FilePoolName && Worker == other.Worker && Path == other.Path;
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

            return obj is PondFile other && Equals(other);
        }

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return StringComparer.InvariantCulture.GetHashCode(FilePoolName) | StringComparer.InvariantCulture.GetHashCode(Path) | Worker.GetHashCode();
        }
    }
}
