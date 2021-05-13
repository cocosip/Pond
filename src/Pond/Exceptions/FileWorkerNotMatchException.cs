using System;

namespace Pond.Exceptions
{
    public class FileWorkerNotMatchException : Exception
    {

        public FileWorkerNotMatchException()
        {

        }

        public FileWorkerNotMatchException(string message) : base(message)
        {

        }
    }
}
