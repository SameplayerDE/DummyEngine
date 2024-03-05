using System.Collections.Generic;
using System.IO;
using DummyEngine.Models;
using Newtonsoft.Json;

namespace DummyEngine;

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
        string jsonData = File.ReadAllText(jsonFilePath);
        List<Character> characters = JsonConvert.DeserializeObject<List<Character>>(jsonData);
        return characters;
    }
}