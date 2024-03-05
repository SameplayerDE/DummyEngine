using System.Collections.Generic;
using DummyEngine.Models;

namespace DummyEngine;

public class CharacterManager
{
    public static CharacterManager Instance { get; } = new CharacterManager();

    static CharacterManager()
    {
    }

    private CharacterManager()
    {
    }

    private Dictionary<string, Character> _characters = new();

    public void Init()
    {
        CharacterLoader characterLoader = CharacterLoader.Instance;
        string jsonFilePath = "Assets/Characters.json"; // Replace with the actual path to your JSON file
        List<Character> characters = characterLoader.LoadCharacters(jsonFilePath);

        // Now you have a list of characters loaded from the JSON file
        foreach (Character character in characters)
        {
            AssetsManager.Instance.LoadTextureFromFolder(character.ImagePath);
            _characters[character.ID] = character;
        }
    }

    public Character GetCharacterById(string id)
    {
        if (_characters.TryGetValue(id, out Character character))
        {
            return character;
        }

        return null;
    }
}