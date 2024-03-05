using Newtonsoft.Json;

namespace DummyEngine.Models;

public class Dialog
{
    public string ID { get; set; }
    public string Content { get; set; }
    public string SpeakerID { get; set; }
    [JsonIgnore] public Character Speaker { get; set; }
}