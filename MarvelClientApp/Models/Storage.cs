using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using MarvelSDK.Model;
using Newtonsoft.Json;

namespace MarvelClientApp.Models
{
    /*
     *  1) мы идентифицируем героев по индексу и никогда не пользуемся фильтрацией,
     *     за исключением offset
     *     обоснование: нету возможности получить список всех имён или id
     *  2) мы храним количество героев (неявно, в размере списка characters),
     *     и если оно изменилось, наши индексы неправильные, поэтому нужно выбросить
     *     запомненных героев
     */
    class Storage
    {
        List<Character> characters = new List<Character>();

        public int Length => characters.Count;

        public void ResetCharacters()
        {
            characters = new List<Character>();
        }

        // может быть null
        public IEnumerable<Character> GetAllCharactersFrom(int startIndex)
        {
            return characters.Skip(startIndex);
        }

        public void AddCharacters(IEnumerable<Character> newCharacters)
        {
            characters.AddRange(newCharacters);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(characters);
        }

        public void Deserialize(string content)
        {
            try
            {
                characters = JsonConvert.DeserializeObject<List<Character>>(content);
            }
            catch (Exception ex)
            {
                // если не можем прочитать контент, ничего страшного
                Debug.WriteLine("Content deserialization failed: " + ex.ToString());
            }
        }
    }
}
