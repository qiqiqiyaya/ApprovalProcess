using System;

namespace Ap.Core.Exceptions
{
    public class ApException(string msg) : Exception(msg);

    public class ApAlreadyExistsException(string msg) : ApException(msg);

    public sealed class ApAlreadyExistsException<T> : ApException
    {
        public ApAlreadyExistsException(string msg, T data) : base(msg)
        {
            var key = typeof(T).FullName!;
            Data.Add(key, data);
        }
    }

    public sealed class ApNotFindException<T> : ApException
    {
        public ApNotFindException(string msg, T data) : base(msg)
        {
            var key = typeof(T).FullName!;
            Data.Add(key, data);
        }
    }
}
