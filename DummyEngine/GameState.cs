using DummyEngine.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Synthesis;
using System.Threading.Tasks;

namespace DummyEngine
{
    public class GameState
    {
        public int CurrentSceneIndex { get; set; } = -1;
        public int CurrentSceneDialogIndex { get; set; }
        public List<Scene> Scenes { get; set; }

        private int _currentLetterCount = 0; // Anzahl der sichtbaren Buchstaben
        private double _timeSinceLastLetter = 0; // Zeit seit dem letzten sichtbaren Buchstaben
        private double _letterDelay = 50; // Zeitverzögerung zwischen den Buchstaben in Millisekunden

        private AssetsManager _assetInstance => AssetsManager.Instance;
        private KeyboardState previousKeyboardState;

        private SpeechSynthesizer synthesizer;

        public GameState()
        {
            synthesizer = new SpeechSynthesizer();
            synthesizer.SetOutputToDefaultAudioDevice();
            synthesizer.SpeakCompleted += OnSpeakCompleted;
            synthesizer.Rate = 10;
        }

        private void OnSpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            AdvanceDialog();
            _currentLetterCount = 0;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (CurrentSceneIndex >= Scenes.Count || Scenes[CurrentSceneIndex] == null)
            {
                return;
            }
            var scene = Scenes[CurrentSceneIndex];


            if (CurrentSceneDialogIndex >= scene.Dialogs.Count || scene.Dialogs[CurrentSceneDialogIndex] == null)
            {
                return;
            }
            var dialog = scene.Dialogs[CurrentSceneDialogIndex];

            var font = _assetInstance.GetFont("font");
            var fontHeight = font.LineSpacing;

            var name = dialog.Speaker.Name;
            var content = dialog.Content.Substring(0, _currentLetterCount);  // Nur einen Teil des Inhalts zeichnen

            var namePosition = Vector2.Zero;
            var contentPosition = Vector2.Zero;

            Texture2D speakerImage = _assetInstance.GetTexture(dialog.Speaker.ImagePath);

            var contentDimensions = font.MeasureString(dialog.Content);
            var nameDimensions = font.MeasureString(dialog.Speaker.Name);

            //Calculate position
            contentPosition.Y = contentPosition.Y + nameDimensions.Y + fontHeight;
            contentPosition.X = contentPosition.X + 16 + speakerImage.Width;

            namePosition.X = namePosition.X + speakerImage.Width;

            if (speakerImage != null)
            {
                var imagePosition = new Vector2(0, 0); // Position, wo das Bild gezeichnet werden soll
                spriteBatch.Draw(speakerImage, imagePosition, Color.White);
            }

            //Draw Background
            //spriteBatch.Draw(_assetInstance.GetTexture("textbox"), Vector2.Zero, Color.AliceBlue);

            //Draw Name
            spriteBatch.DrawString(font, name, namePosition, Color.Black);
            
            //Draw Content
            spriteBatch.DrawString(font, content, contentPosition, Color.Black);
        }

        public void Update(GameTime gameTime)
        {
            _timeSinceLastLetter += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_timeSinceLastLetter > _letterDelay)
            {
                if (_currentLetterCount < Scenes[CurrentSceneIndex].Dialogs[CurrentSceneDialogIndex].Content.Length)
                {
                    _currentLetterCount++;
                }
                _timeSinceLastLetter = 0;
            }

            KeyboardState currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Enter) && previousKeyboardState.IsKeyUp(Keys.Enter))
            {

                synthesizer.SpeakAsyncCancelAll();

                if (_currentLetterCount < Scenes[CurrentSceneIndex].Dialogs[CurrentSceneDialogIndex].Content.Length)
                {
                    _currentLetterCount = Scenes[CurrentSceneIndex].Dialogs[CurrentSceneDialogIndex].Content.Length;
                }
                else
                {
                    AdvanceDialog();
                    _currentLetterCount = 0;
                }
                
            }

            previousKeyboardState = currentKeyboardState;
        }

        private void AdvanceDialog()
        {
            if (CurrentSceneIndex >= Scenes.Count)
            {
                return;
            }

            var currentScene = Scenes[CurrentSceneIndex];

            if (CurrentSceneDialogIndex < currentScene.Dialogs.Count - 1)
            {
                CurrentSceneDialogIndex++;
            }
            else
            {
                CurrentSceneIndex++;
                CurrentSceneDialogIndex = 0;
            }

            var dialog = currentScene.Dialogs[CurrentSceneDialogIndex];

            synthesizer.SpeakAsyncCancelAll();
            synthesizer.SpeakAsync(dialog.Content);
        }

    }
}
