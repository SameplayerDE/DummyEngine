using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyEngine.Models
{
    public class Character
    {
        public string Name { get; set; }
        public string ImagePath { get; set; } = "generic";
        public Texture2D Texture { get; set; }
    }
}
