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
        private enum FadeState
        {
            FadeInTitle,
            FadeOutTitle,
            FadeInContent,
            FadeOutContent,
            None
        }

        private FadeState currentFadeState;
        private float currentTitelAlpha = 0.0f;
        private float currentContentAlpha = 0.0f;
        private double fadeSpeed = 0.001;
        private bool isSpeaking = false;

        public string CurrentSceneIndex { get; set; }
        public string NextSceneIndex { get; set; }
        public int CurrentSceneDialogIndex { get; set; }
        public List<Scene> Scenes { get; set; }

        private int _currentLetterCount = 0;
        private double _timeSinceLastLetter = 0;
        private double _letterDelay = 25;

        private AssetsManager _assetInstance => AssetsManager.Instance;
        private KeyboardState previousKeyboardState;
        private SpeechSynthesizer synthesizer;

        public GameState()
        {
            synthesizer = new SpeechSynthesizer();
            synthesizer.SetOutputToDefaultAudioDevice();
            synthesizer.SpeakCompleted += OnSpeakCompleted;
            synthesizer.Rate = 4;
            synthesizer.SelectVoice("Microsoft Hedda Desktop");
            currentFadeState = FadeState.FadeInTitle;
        }

        private void OnSpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            isSpeaking = false;
            AdvanceDialog();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            switch (currentFadeState)
            {
                case FadeState.FadeInTitle:
                    currentTitelAlpha += (float)(fadeSpeed * gameTime.ElapsedGameTime.TotalMilliseconds);
                    if (currentTitelAlpha >= 1)
                    {
                        currentTitelAlpha = 1;
                        currentFadeState = FadeState.FadeOutTitle;
                    }
                    break;
                case FadeState.FadeOutTitle:
                    currentTitelAlpha -= (float)(fadeSpeed * gameTime.ElapsedGameTime.TotalMilliseconds);
                    if (currentTitelAlpha <= 0)
                    {
                        currentTitelAlpha = 0;
                        currentFadeState = FadeState.FadeInContent;
                    }
                    break;
                case FadeState.FadeInContent:
                    currentContentAlpha += (float)(fadeSpeed * gameTime.ElapsedGameTime.TotalMilliseconds);
                    if (currentContentAlpha >= 1)
                    {
                        currentContentAlpha = 1;
                        currentFadeState = FadeState.None;
                        TrySpeak();
                    }
                    break;
                case FadeState.FadeOutContent:
                    currentContentAlpha -= (float)(fadeSpeed * gameTime.ElapsedGameTime.TotalMilliseconds);
                    if (currentContentAlpha <= 0)
                    {
                        currentContentAlpha = 0;
                    }
                    break;
                default:
                    break;
            }

            if (currentFadeState == FadeState.FadeOutContent && currentContentAlpha <= 0)
            {
                // Setze den aktuellen Szenenindex auf den Index der nächsten Szene
                CurrentSceneIndex = NextSceneIndex;

                // Setze den Dialog-Index zurück und führe einen Reset anderer relevanter Variablen durch
                CurrentSceneDialogIndex = 0;
                _currentLetterCount = 0;

                // Setze den Fade-In Zustand für die neue Szene
                currentFadeState = FadeState.FadeInTitle;
            }

            if (SceneManager.Instance.GetSceneById(CurrentSceneIndex) == null)
            {
                return;
            }
            var scene = SceneManager.Instance.GetSceneById(CurrentSceneIndex);

            var font = _assetInstance.GetFont("font");
            var fontHeight = font.LineSpacing;

            if (currentFadeState == FadeState.FadeInTitle || currentFadeState == FadeState.FadeOutTitle)
            {
                // Render den Titel der Szene mit currentAlpha
                var title = scene.Name;
                var titlePosition = new Vector2(100, 100);  // Beispielposition
                spriteBatch.DrawString(font, title, titlePosition, Color.Black * currentTitelAlpha);
            }

            if (currentContentAlpha <= 0)
            {
                return;
            }

            if (CurrentSceneDialogIndex >= scene.Dialogs.Count || scene.Dialogs[CurrentSceneDialogIndex] == null)
            {
                return;
            }
            var dialog = scene.Dialogs[CurrentSceneDialogIndex];

            var name = dialog.Speaker.Name;
            var content = dialog.Content.Substring(0, _currentLetterCount);  // Nur einen Teil des Inhalts zeichnen

            var namePosition = Vector2.Zero;
            var contentPosition = Vector2.Zero;

            Texture2D speakerImage = _assetInstance.GetTexture(dialog.Speaker.ImagePath);

            var contentDimensions = font.MeasureString(dialog.Content);
            var nameDimensions = font.MeasureString(dialog.Speaker.Name);

            //Calculate position
            contentPosition.Y = contentPosition.Y + nameDimensions.Y + fontHeight;
            contentPosition.X = contentPosition.X + 16 + 200;

            namePosition.X = namePosition.X + 200;

            if (speakerImage != null)
            {
                float aspectRatio = (float)speakerImage.Width / (float)speakerImage.Height;
                int newWidth = 200;  // Maximale Breite in Pixeln
                int newHeight = (int)(newWidth / aspectRatio);  // Höhe anpassen, um das Seitenverhältnis zu wahren

                var destinationRectangle = new Rectangle(0, 0, newWidth, newHeight);  // Position und Größe für das Zeichnen
                var sourceRectangle = new Rectangle(0, 0, speakerImage.Width, speakerImage.Height);  // Der Bereich des Bildes, der gezeichnet werden soll

                spriteBatch.Draw(speakerImage, destinationRectangle, sourceRectangle, Color.White * currentContentAlpha);
            }

            //Draw Background
            //spriteBatch.Draw(_assetInstance.GetTexture("textbox"), Vector2.Zero, Color.AliceBlue);

            //Draw Name
            spriteBatch.DrawString(font, name, namePosition, Color.Black * currentContentAlpha);

            //Draw Content
            spriteBatch.DrawString(font, content, contentPosition, Color.Black * currentContentAlpha);
        }
        public void Update(GameTime gameTime)
        {
            // Prüfe, ob der Inhalt vollständig sichtbar ist
            if (currentContentAlpha < 1)
            {
                return;
            }

            // Update der Textanimation
            _timeSinceLastLetter += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_timeSinceLastLetter > _letterDelay)
            {
                if (_currentLetterCount < GetCurrentScene().Dialogs[CurrentSceneDialogIndex].Content.Length)
                {
                    _currentLetterCount++;
                }
                _timeSinceLastLetter = 0;
            }

            // Überprüfe Tastendruck
            KeyboardState currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Enter) && previousKeyboardState.IsKeyUp(Keys.Enter))
            {
                synthesizer.SpeakAsyncCancelAll();

                if (_currentLetterCount < GetCurrentScene().Dialogs[CurrentSceneDialogIndex].Content.Length)
                {
                    _currentLetterCount = GetCurrentScene().Dialogs[CurrentSceneDialogIndex].Content.Length;
                }
                else
                {
                    AdvanceDialog(); // Verschiebt den Dialog und spricht den nächsten Text
                }
            }

            previousKeyboardState = currentKeyboardState;
        }


        private void UpdateTextRendering(GameTime gameTime)
        {
            _timeSinceLastLetter += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_timeSinceLastLetter > _letterDelay)
            {
                if (_currentLetterCount < GetCurrentDialog().Content.Length)
                {
                    _currentLetterCount++;
                }
                _timeSinceLastLetter = 0;
            }
        }

        private void SkipOrAdvanceDialog()
        {
            synthesizer.SpeakAsyncCancelAll();
            if (_currentLetterCount < GetCurrentDialog().Content.Length)
            {
                _currentLetterCount = GetCurrentDialog().Content.Length;
            }
            else
            {
                AdvanceDialog();
            }
        }

        private Dialog GetCurrentDialog()
        {
            return SceneManager.Instance.GetSceneById(CurrentSceneIndex).Dialogs[CurrentSceneDialogIndex];
        }

        public void AdvanceDialog()
        {
            if (currentContentAlpha < 1)
            {
                return;
            }

            var currentScene = GetCurrentScene();

            if (CurrentSceneDialogIndex < currentScene.Dialogs.Count - 1)
            {
                // Wenn es noch weitere Dialoge gibt, gehe zum nächsten Dialog
                CurrentSceneDialogIndex++;
                _currentLetterCount = 0; // Letter count zurücksetzen
                TrySpeak();
            }
            else
            {
                // Überprüfen, ob es eine 'Next'-Szene gibt
                if (!string.IsNullOrEmpty(currentScene.Next))
                {
                    // Setze den Index der nächsten Szene
                    NextSceneIndex = currentScene.Next;
                    currentFadeState = FadeState.FadeOutContent;
                }
                else
                {
                    // Wenn es keine 'Next'-Szene gibt, mache nichts
                    isSpeaking = false;
                    synthesizer.SpeakAsyncCancelAll();
                }
            }
        }



        public void TrySpeak()
        {
            if (isSpeaking)
            {
                return;
            }

            if (currentContentAlpha < 1)
            {
                return;
            }

            isSpeaking = true;
            var currentScene = SceneManager.Instance.GetSceneById(CurrentSceneIndex);
            var dialog = currentScene.Dialogs[CurrentSceneDialogIndex];
            synthesizer.SpeakAsyncCancelAll();
            synthesizer.SpeakAsync(dialog.Content);
        }


        private Scene GetCurrentScene()
        {
            return SceneManager.Instance.GetSceneById(CurrentSceneIndex);
        }    
    }
}
