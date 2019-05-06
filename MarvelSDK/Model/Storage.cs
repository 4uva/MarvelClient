using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace MarvelSDK.Model
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
        XmlSerializer serializer = new XmlSerializer(typeof(List<Character>));

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
            //// код взят отсюда: https://stackoverflow.com/a/2434558/10243782
            //using (var textWriter = new StringWriter())
            //{
            //    serializer.Serialize(textWriter, characters);
            //    return textWriter.ToString();
            //}
        }

        public void Deserialize(string content)
        {
            try
            {
                characters = JsonConvert.DeserializeObject<List<Character>>(content);
                //using (var textReader = new StringReader(content))
                //{
                //    characters = (List<Character>)serializer.Deserialize(textReader);
                //}
            }
            catch (Exception ex)
            {
                // если не можем прочитать контент, ничего страшного
                Debug.WriteLine("Content deserialization failed: " + ex.ToString());
            }
        }
    }
}
