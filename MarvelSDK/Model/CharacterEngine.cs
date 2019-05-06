using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MarvelSDK.Model
{
    public class CharacterEngine
    {
        Storage storage = new Storage();
        RestHelper restHelper = new RestHelper();
        HashHelper hashHelper;
        string publicKey;
        const int RequestLimit = 100;
        int? numberOfCharactersFromApi;
        bool downloadedAll;

        public CharacterEngine(string privateKey, string publicKey, string storageContent, bool downloadedAll)
        {
            this.publicKey = publicKey;
            hashHelper = new HashHelper(privateKey, publicKey);
            if (storageContent != null)
                storage.Deserialize(storageContent);
            this.downloadedAll = downloadedAll;
        }

        public IEnumerable<Character> GetCharactersFrom(int startIndex)
        {
            return storage.GetAllCharactersFrom(startIndex);
        }

        public bool CanDownloadMore()
        {
            return !downloadedAll;
        }

        static int ts = 0;
        public async Task DownloadMore()
        {
            try
            {
                var offset = storage.Length;
                // https://stackoverflow.com/q/28743530/10243782
                // need to add timestamp and private key + md5
                ++ts;
                var hash = hashHelper.ComputeHash(ts);
                var parameters = new Dictionary<string, object>()
                {
                    ["orderBy"] = "name",
                    ["offset"] = offset,
                    ["limit"] = 100,
                    ["apikey"] = publicKey,
                    ["ts"] = ts,
                    ["hash"] = hash
                };
                var wrapper =
                    await restHelper.GetAsync<CharacterDataWrapper>("characters", parameters);
                var characterData = wrapper.Data;
                // проверить длину

                if (characterData.Total != null)
                {
                    var totalCharactersFromApi = characterData.Total.Value;
                    if (numberOfCharactersFromApi == null)
                    {
                        numberOfCharactersFromApi = totalCharactersFromApi;
                    }
                    else
                    {
                        if (totalCharactersFromApi != numberOfCharactersFromApi)
                        {
                            storage.ResetCharacters();
                            numberOfCharactersFromApi = totalCharactersFromApi;
                            throw new RestartFetchingException();
                        }
                    }
                }

                if (characterData.Results.Length > 0)
                    storage.AddCharacters(characterData.Results);
                else
                    downloadedAll = true;

            }
            catch (HttpRequestException ex)
            {
                throw new DataDownloadException(ex);
            }
        }

        public string SerializeCharacters()
        {
            return storage.Serialize();
        }
    }
}
