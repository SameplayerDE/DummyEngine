using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace DummyEngine
{
    public class AssetsManager
    {
        public static AssetsManager Instance { get; } = new AssetsManager();

        static AssetsManager()
        {
        }

        private AssetsManager()
        {
        }

        public static ContentManager Content;
        public static Game Game;

        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public void Init(Game game)
        {
            Game = game;
            Content = new ContentManager(Game.Services, "Content");
        }

        public void LoadTexture(string assetName)
        {
            Texture2D texture = Content.Load<Texture2D>(assetName);
            textures[assetName] = texture;
        }

        public Texture2D GetTexture(string assetName)
        {
            if (string.IsNullOrEmpty(assetName))
            {
                return null;
            }
            return textures.ContainsKey(assetName) ? textures[assetName] : null;
        }

        public void LoadFont(string assetName)
        {
            SpriteFont font = Content.Load<SpriteFont>(assetName);
            fonts[assetName] = font;
        }

        public SpriteFont GetFont(string assetName)
        {
            return fonts.ContainsKey(assetName) ? fonts[assetName] : null;
        }
    }
}
