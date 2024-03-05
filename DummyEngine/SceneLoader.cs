using System.Collections.Generic;
using System.IO;
using DummyEngine.Models;
using Newtonsoft.Json;

namespace DummyEngine;

public class SceneLoader
{
    public static SceneLoader Instance { get; } = new SceneLoader();

    static SceneLoader()
    {
    }

    private SceneLoader()
    {
    }

    public List<Scene> LoadScenes(string jsonFilePath)
    {
        string jsonData = File.ReadAllText(jsonFilePath);
        List<Scene> scenes = JsonConvert.DeserializeObject<List<Scene>>(jsonData);
        return scenes;
    }
}