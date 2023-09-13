using DummyEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyEngine
{
    public class SceneManager
    {
        public static SceneManager Instance { get; } = new SceneManager();

        static SceneManager()
        {
        }

        private SceneManager()
        {
        }

        private Dictionary<string, Scene> _scenes = new();

        public void Init()
        {
            SceneLoader sceneLoader = SceneLoader.Instance;
            string jsonFilePath = "Assets/Scenes.json"; // Replace with the actual path to your JSON file
            List<Scene> scenes = sceneLoader.LoadScenes(jsonFilePath);

            // Now you have a list of characters loaded from the JSON file
            foreach (Scene scene in scenes)
            {
                scene.Dialogs = new();
                foreach (string dialogIds in scene.DialogIds)
                {
                    scene.Dialogs.Add(DialogManager.Instance.GetDialogById(dialogIds));
                }
                _scenes[scene.ID] = scene;
            }
        }

        public Scene GetSceneById(string id)
        {
            if (_scenes.TryGetValue(id, out Scene scene))
            {
                return scene;
            }
            return null;
        }
    }
}
