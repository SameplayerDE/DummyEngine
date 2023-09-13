using DummyEngine.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyEngine
{
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
            string jsonData = System.IO.File.ReadAllText(jsonFilePath);
            List<Scene> scenes = JsonConvert.DeserializeObject<List<Scene>>(jsonData);
            return scenes;
        }
    }
}
