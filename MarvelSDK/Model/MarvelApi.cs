using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MarvelSDK.Model
{
    public class MarvelApi
    {
        static int ts = 0;

        RestHelper restHelper = new RestHelper();
        HashHelper hashHelper;
        string publicKey;
        const int RequestLimit = 100;

        public MarvelApi(string privateKey, string publicKey)
        {
            this.publicKey = publicKey;
            hashHelper = new HashHelper(privateKey, publicKey);
        }

        public async Task<CharacterDataContainer> GetCharactersAsync(
            string name,
            string nameStartsWith,
            DateTime? modifiedSince,
            IEnumerable<int> comics,
            IEnumerable<int> series,
            IEnumerable<int> events,
            IEnumerable<int> stories,
            IEnumerable<CharacterOrder> orderBy,
            int? limit,
            int? offset)
        {
            // https://stackoverflow.com/q/28743530/10243782
            // need to add timestamp and private key + md5
            ++ts;
            var hash = hashHelper.ComputeHash(ts);
            var parameters = new Dictionary<string, string>()
            {
                ["ts"] = ts.ToString(),
                ["hash"] = hash,
                ["apikey"] = publicKey
            };
            if (name != null)
                parameters["name"] = name;
            if (nameStartsWith != null)
                parameters["nameStartsWith"] = nameStartsWith;
            if (modifiedSince != null)
                parameters["modifiedSince"] = modifiedSince.Value.ToString("o");
            if (comics != null)
                parameters["comics"] = FormatIdList(comics);
            if (series != null)
                parameters["series"] = FormatIdList(series);
            if (events != null)
                parameters["events"] = FormatIdList(events);
            if (stories != null)
                parameters["stories"] = FormatIdList(stories);
            if (orderBy != null)
                parameters["orderBy"] = FormatOrderList(orderBy);
            if (limit != null)
                parameters["limit"] = limit.Value.ToString();
            if (offset != null)
                parameters["offset"] = offset.Value.ToString();
            var wrapper =
                await restHelper.GetAsync<CharacterDataWrapper>("characters", parameters);
            return wrapper.Data;
        }

        // comma-separated list
        static string FormatIdList(IEnumerable<int> ids) =>
            string.Join(",", ids);

        static string FormatOrderList(IEnumerable<CharacterOrder> orderBy)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var orderByItem in orderBy)
            {
                if (sb.Length != 0)
                    sb.Append(',');
                sb.Append(FormatOrderItem(orderByItem));
            }
            return sb.ToString();
        }

        static string FormatOrderItem(CharacterOrder orderByItem)
        {
            switch (orderByItem)
            {
                case CharacterOrder.NameAscending:
                    return "name";
                case CharacterOrder.NameDescending:
                    return "-name";
                case CharacterOrder.ModifiedAscending:
                    return "modified";
                case CharacterOrder.ModifiedDescending:
                    return "-modified";
                default:
                    throw new ArgumentException("Unknown enum value");
            }
        }
    }
}
