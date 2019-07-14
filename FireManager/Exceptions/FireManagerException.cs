using System;

namespace FireManager.Exceptions
{
    [Serializable()]
    public class FireManagerException : Exception
    {
        public FireManagerException()
        { }

        public FireManagerException(string Message) : base(Message)
        { }

        public FireManagerException(string Message, Exception inner) : base(Message, inner)
        { }
    }
}