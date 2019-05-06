using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace MarvelSDK.Model
{
    [Serializable]
    public class DataDownloadException : Exception
    {
        public DataDownloadException(Exception innerException)
            : base("Couldn't download data from Marvel", innerException)
        {
        }

        protected DataDownloadException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}