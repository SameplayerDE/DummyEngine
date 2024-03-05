using System.Collections.Generic;
using System.Text.RegularExpressions;
using DummyEngine.Models;

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
                
                var pattern = @"%(\d+)";
                var regex = new Regex(pattern);
                
                var replacedText = regex.Replace(dialog.Content, match =>
                {
                    var speakerID = match.Groups[1].Value;
                    var speaker = CharacterManager.Instance.GetCharacterById(speakerID);
                    if (speaker != null)
                    {
                        return speaker.Name;
                    }
                    return match.Value;
                });

                dialog.Content = replacedText;
                
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
