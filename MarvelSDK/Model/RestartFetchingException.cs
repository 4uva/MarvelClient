using System;
using System.Runtime.Serialization;

namespace MarvelSDK.Model
{
    [Serializable]
    public class RestartFetchingException : Exception
    {
        public RestartFetchingException()
        {
        }

        protected RestartFetchingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}