using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MarvelSDK.Model;

namespace MarvelClientApp.Models
{
    public class CharacterEngine
    {
        Storage storage = new Storage();
        MarvelApi api;
        const int RequestLimit = 100;
        int? numberOfCharactersFromApi;
        bool downloadedAll;

        public CharacterEngine(string privateKey, string publicKey, string storageContent, bool downloadedAll)
        {
            api = new MarvelApi(privateKey, publicKey);
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

        public async Task DownloadMore()
        {
            var offset = storage.Length;
            var characterData = await api.GetCharactersAsync(
                name: null,
                nameStartsWith: null,
                modifiedSince: null,
                comics: null,
                series: null,
                events: null,
                stories: null,
                orderBy: new[] { CharacterOrder.NameAscending },
                limit: RequestLimit,
                offset: offset);
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

        public string SerializeCharacters()
        {
            return storage.Serialize();
        }
    }
}
