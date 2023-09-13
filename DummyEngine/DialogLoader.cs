using DummyEngine.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyEngine
{
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
            string jsonData = System.IO.File.ReadAllText(jsonFilePath);
            List<Dialog> dialogs = JsonConvert.DeserializeObject<List<Dialog>>(jsonData);
            return dialogs;
        }
    }
}
