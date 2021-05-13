using System;

namespace Pond.Exceptions
{
    public class PondFileNullException : Exception
    {
        public PondFileNullException()
        {

        }


        public PondFileNullException(string message) : base(message)
        {

        }
    }
}
