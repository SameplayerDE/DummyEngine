using DummyEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyEngine
{
    public class DialogManager
    {
        public static DialogManager Instance { get; } = new DialogManager();

        static DialogManager()
        {
        }

        private DialogManager()
        {
        }

        private Dictionary<string, Dialog> _dialogs = new();

        public void Init()
        {
            DialogLoader dialogLoader = DialogLoader.Instance;
            string jsonFilePath = "Assets/Dialogs.json"; // Replace with the actual path to your JSON file
            List<Dialog> dialogs = dialogLoader.LoadDialogs(jsonFilePath);

            // Now you have a list of characters loaded from the JSON file
            foreach (Dialog dialog in dialogs)
            {
                dialog.Speaker = CharacterManager.Instance.GetCharacterById(dialog.SpeakerID);
                _dialogs[dialog.ID] = dialog;
            }
        }

        public Dialog GetDialogById(string id)
        {
            if (_dialogs.TryGetValue(id, out Dialog dialog))
            {
                return dialog;
            }
            return null;
        }
    }
}
