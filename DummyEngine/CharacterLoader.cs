using DummyEngine.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyEngine
{
    public class CharacterLoader
    {

        public static CharacterLoader Instance { get; } = new CharacterLoader();

        static CharacterLoader()
        {
        }

        private CharacterLoader()
        {
        }

        public List<Character> LoadCharacters(string jsonFilePath)
        {
            string jsonData = System.IO.File.ReadAllText(jsonFilePath);
            List<Character> characters = JsonConvert.DeserializeObject<List<Character>>(jsonData);
            return characters;
        }
    }
}
