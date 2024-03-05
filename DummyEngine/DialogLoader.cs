using System.Collections.Generic;
using System.IO;
using DummyEngine.Models;
using Newtonsoft.Json;

namespace DummyEngine;

public class DialogLoader
{
    public static DialogLoader Instance { get; } = new DialogLoader();

    static DialogLoader()
    {
    }

    private DialogLoader()
    {
    }

    public List<Dialog> LoadDialogs(string jsonFilePath)
    {
        string jsonData = File.ReadAllText(jsonFilePath);
        List<Dialog> dialogs = JsonConvert.DeserializeObject<List<Dialog>>(jsonData);
        return dialogs;
    }
}