using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace DummyEngine.Models;

public class Character
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string ImagePath { get; set; } = "generic";
    [JsonIgnore] public Texture2D Texture { get; set; }
}