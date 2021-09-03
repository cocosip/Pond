using System;
using System.IO;

namespace Pond
{
    public class PondFile : IComparable<PondFile>, IComparable
    {
        /// <summary>
        /// 文件池的名称
        /// </summary>
        public string FilePoolName { get; set; }

        /// <summary>
        /// Worker id
        /// </summary>
        public int WorkerId { get; set; }

        /// <summary>
        /// 文件的存储路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExt
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(FilePath))
                {
                    return Path.GetExtension(FilePath);
                }
                return "";
            }
        }

        public PondFile()
        {

        }

        public PondFile(string filePoolName, int workerId, string filePath)
        {
            FilePoolName = filePoolName;
            WorkerId = workerId;
            FilePath = filePath;
        }

        public static bool operator ==(PondFile left, PondFile right)
        {
            return IsEqual(left, right);
        }
        public static bool operator !=(PondFile left, PondFile right)
        {
            return !IsEqual(left, right);
        }
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (PondFile)obj;

            return FilePoolName == other.FilePoolName && WorkerId == other.WorkerId && FilePath == other.FilePath;
        }

        public override int GetHashCode()
        {
            return (FilePoolName + WorkerId.ToString() + FilePath).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}@{1}", FilePoolName, WorkerId);
        }

        private static bool IsEqual(PondFile left, PondFile right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }
            return left is null || left.Equals(right);
        }

        public int CompareTo(PondFile other)
        {
            return ToString().CompareTo(other.ToString());
        }
        public int CompareTo(object obj)
        {
            return ToString().CompareTo(obj.ToString());
        }

    }
}
