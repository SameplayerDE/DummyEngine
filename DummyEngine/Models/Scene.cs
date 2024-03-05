using System.Collections.Generic;
using Newtonsoft.Json;

namespace DummyEngine.Models;

public class Scene
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string Next { get; set; } = string.Empty;
    [JsonProperty("Dialogs")] public List<string> DialogIds { get; set; }
    [JsonIgnore] public List<Dialog> Dialogs { get; set; }
}