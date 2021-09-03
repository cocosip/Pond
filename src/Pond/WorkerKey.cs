using System;

namespace Pond
{
    [Serializable]
    public class WorkerKey : IComparable<WorkerKey>, IComparable
    {
        public string Name { get; set; }
        public int WorkerId { get; set; }

        public WorkerKey(string name, int workerId)
        {
            Name = name;
            WorkerId = workerId;
        }

        public static bool operator ==(WorkerKey left, WorkerKey right)
        {
            return IsEqual(left, right);
        }
        public static bool operator !=(WorkerKey left, WorkerKey right)
        {
            return !IsEqual(left, right);
        }
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (WorkerKey)obj;

            return Name == other.Name && WorkerId == other.WorkerId;
        }
        public override int GetHashCode()
        {
            return (Name + WorkerId.ToString()).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}@{1}", Name, WorkerId);
        }

        private static bool IsEqual(WorkerKey left, WorkerKey right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }
            return left is null || left.Equals(right);
        }

        public int CompareTo(WorkerKey other)
        {
            return ToString().CompareTo(other.ToString());
        }
        public int CompareTo(object obj)
        {
            return ToString().CompareTo(obj.ToString());
        }
    }
}
