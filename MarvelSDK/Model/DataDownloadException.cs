using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace MarvelSDK.Model
{
    [Serializable]
    public class DataDownloadException : Exception
    {
        public DataDownloadException(string errorText, string reasonPhrase)
            : base("Couldn't download data from Marvel: " + reasonPhrase + "\n" + errorText)
        {
        }

        protected DataDownloadException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}