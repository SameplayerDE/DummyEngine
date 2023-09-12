using DummyEngine.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DummyEngine
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameState _gameState;
        private AssetsManager _assetInstance => AssetsManager.Instance;

        //Assets
        private SpriteFont _font;

        //Characters
        private Character _oktay;
        private Character _merle;
        private Character _tessa;
        private Character _alisa;

        //Scenes
        private Scene _introScene;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _gameState = new GameState();
            _assetInstance.Init(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _assetInstance.LoadFont("font");
            _assetInstance.LoadTexture("generic");
            _assetInstance.LoadTexture("Oktay");
            _assetInstance.LoadTexture("Merle");
            _assetInstance.LoadTexture("Tessa");
            _assetInstance.LoadTexture("Alisa");
            _assetInstance.LoadTexture("textbox");

            _oktay = new Character();
            _oktay.Name = "Oktay";
            _oktay.ImagePath = "Oktay";

            _merle = new Character();
            _merle.Name = "Merle";
            _merle.ImagePath = "Merle";

            _tessa = new Character();
            _tessa.Name = "Tessa";
            _tessa.ImagePath = "Tessa";

            _alisa = new Character();
            _alisa.Name = "Alisa";
            _alisa.ImagePath = "Alisa";

            _introScene = new Scene();
            _introScene.Characters = new();
            _introScene.Dialogs = new();

            _introScene.Characters.Add(_oktay);
            _introScene.Characters.Add(_merle);
            _introScene.Characters.Add(_tessa);
            _introScene.Characters.Add(_alisa);

            Dialog dialog000 = new Dialog { Speaker = _oktay, Content = "Hallo... ist hier wer?" };
            Dialog dialog001 = new Dialog { Speaker = _merle, Content = "Oktay? Bist du das? Es ist eine Weile her..." };
            Dialog dialog002 = new Dialog { Speaker = _oktay, Content = "Merle? Was machst du hier?" };
            Dialog dialog003 = new Dialog { Speaker = _merle, Content = "Das könnte ich dich auch fragen." };
            Dialog dialog004 = new Dialog { Speaker = _tessa, Content = "Oktay, dich habe ich ja ewig nicht gesehen." };
            Dialog dialog005 = new Dialog { Speaker = _oktay, Content = "Tessa? Das ist ja unglaublich!" };
            Dialog dialog006 = new Dialog { Speaker = _alisa, Content = "Oktay, wer sind diese Leute?" };
            Dialog dialog007 = new Dialog { Speaker = _oktay, Content = "Ähm, das ist kompliziert..." };
            Dialog dialog008 = new Dialog { Speaker = _alisa, Content = "Nun, du könntest mir vielleicht ein paar Erklärungen liefern." };
            Dialog dialog009 = new Dialog { Speaker = _oktay, Content = "Okay, wo fange ich an..." };
            Dialog dialog010 = new Dialog { Speaker = _merle, Content = "Bei mir? Du könntest ihr sagen, dass ich deine erste Freundin war." };
            Dialog dialog011 = new Dialog { Speaker = _alisa, Content = "Deine erste Freundin? Du hast mir erzählt, dass du vor mir keine hattest!" };
            Dialog dialog012 = new Dialog { Speaker = _oktay, Content = "Ähm, also es ist so..." };
            Dialog dialog013 = new Dialog { Speaker = _tessa, Content = "Warte mal, du hast ihr gesagt, dass sie deine erste Freundin ist?" };
            Dialog dialog014 = new Dialog { Speaker = _oktay, Content = "Ich wollte nicht, dass sie sich Sorgen macht oder eifersüchtig wird." };
            Dialog dialog015 = new Dialog { Speaker = _merle, Content = "Das scheint ja super funktioniert zu haben." };
            Dialog dialog016 = new Dialog { Speaker = _alisa, Content = "Ich kann nicht glauben, dass du mich angelogen hast!" };
            Dialog dialog017 = new Dialog { Speaker = _oktay, Content = "Es war nicht meine Absicht, jemanden zu verletzen." };
            Dialog dialog018 = new Dialog { Speaker = _tessa, Content = "Vielleicht ist es besser, wenn wir alle ein bisschen Abstand gewinnen." };
            Dialog dialog019 = new Dialog { Speaker = _merle, Content = "Da stimme ich zu." };
            Dialog dialog020 = new Dialog { Speaker = _alisa, Content = "Abstand ist eine gute Idee. Ich muss darüber nachdenken." };
            Dialog dialog021 = new Dialog { Speaker = _oktay, Content = "Ich verstehe das. Ich habe Fehler gemacht." };
            Dialog dialog022 = new Dialog { Speaker = _merle, Content = "Fehler? Du hast uns alle belogen, Oktay." };
            Dialog dialog023 = new Dialog { Speaker = _tessa, Content = "Es ist vielleicht besser, wenn wir alle erstmal einen Schritt zurück machen." };
            Dialog dialog024 = new Dialog { Speaker = _oktay, Content = "Ja, vielleicht sollten wir das tun." };
            Dialog dialog025 = new Dialog { Speaker = _alisa, Content = "Ich brauche Zeit zum Nachdenken, Oktay." };
            Dialog dialog026 = new Dialog { Speaker = _oktay, Content = "Das hast du. Und ich entschuldige mich bei allen hier." };
            Dialog dialog027 = new Dialog { Speaker = _merle, Content = "Eine Entschuldigung ist ein Anfang, aber es wird Zeit brauchen." };
            Dialog dialog028 = new Dialog { Speaker = _tessa, Content = "Sie hat recht. Wir können nicht einfach so tun, als wäre nichts passiert." };
            Dialog dialog029 = new Dialog { Speaker = _oktay, Content = "Ich verstehe." };
            Dialog dialog030 = new Dialog { Speaker = _merle, Content = "Oktay, ich hoffe du lernst aus deinen Fehlern." };
            Dialog dialog031 = new Dialog { Speaker = _alisa, Content = "Das hoffe ich auch. Ich muss jetzt gehen." };
            Dialog dialog032 = new Dialog { Speaker = _tessa, Content = "Ich denke, wir alle könnten eine Pause gebrauchen." };
            Dialog dialog033 = new Dialog { Speaker = _oktay, Content = "Ja, das stimmt. Ich melde mich bei euch." };
            Dialog dialog034 = new Dialog { Speaker = _alisa, Content = "Du weißt, wie du mich erreichst." };
            Dialog dialog035 = new Dialog { Speaker = _tessa, Content = "Und du mich auch, Oktay." };
            Dialog dialog036 = new Dialog { Speaker = _merle, Content = "Ich muss darüber nachdenken, ob ich das auch will." };
            Dialog dialog037 = new Dialog { Speaker = _oktay, Content = "Ich verstehe. Ich gebe euch den Raum, den ihr braucht." };
            Dialog dialog038 = new Dialog { Speaker = _alisa, Content = "Danke, das ist das Mindeste, was du tun kannst." };
            Dialog dialog039 = new Dialog { Speaker = _tessa, Content = "Wir sehen uns dann... oder auch nicht." };
            Dialog dialog040 = new Dialog { Speaker = _merle, Content = "Ich bin mir da noch nicht sicher." };
            Dialog dialog041 = new Dialog { Speaker = _oktay, Content = "Ich respektiere eure Entscheidung, egal wie sie ausfällt." };
            Dialog dialog042 = new Dialog { Speaker = _alisa, Content = "Das solltest du auch." };
            Dialog dialog043 = new Dialog { Speaker = _tessa, Content = "Ich hoffe, du findest, was du suchst, Oktay." };
            Dialog dialog044 = new Dialog { Speaker = _merle, Content = "Das wünsche ich dir auch, für uns alle." };
            Dialog dialog045 = new Dialog { Speaker = _oktay, Content = "Ich werde mein Bestes tun, um ein besserer Mensch zu werden." };
            Dialog dialog046 = new Dialog { Speaker = _alisa, Content = "Das ist ein Anfang." };
            Dialog dialog047 = new Dialog { Speaker = _tessa, Content = "Nur Worte reichen nicht, Taten müssen folgen." };
            Dialog dialog048 = new Dialog { Speaker = _merle, Content = "Genau, wir werden sehen." };
            Dialog dialog049 = new Dialog { Speaker = _oktay, Content = "Ich weiß, dass ich viel zu tun habe." };
            Dialog dialog050 = new Dialog { Speaker = _alisa, Content = "Das ist dir hoffentlich klar." };
            Dialog dialog051 = new Dialog { Speaker = _tessa, Content = "Worte können trügen." };
            Dialog dialog052 = new Dialog { Speaker = _merle, Content = "Aber sie sind ein Anfang." };
            Dialog dialog053 = new Dialog { Speaker = _oktay, Content = "Dann ist dies mein erster Schritt." };
            Dialog dialog054 = new Dialog { Speaker = _alisa, Content = "Wir werden sehen." };
            Dialog dialog055 = new Dialog { Speaker = _tessa, Content = "Ja, das werden wir." };
            Dialog dialog056 = new Dialog { Speaker = _merle, Content = "Ich hoffe, du meinst es ernst, Oktay." };
            Dialog dialog057 = new Dialog { Speaker = _oktay, Content = "Ich meine es wirklich ernst." };
            Dialog dialog058 = new Dialog { Speaker = _alisa, Content = "Zeit wird es zeigen." };
            Dialog dialog059 = new Dialog { Speaker = _tessa, Content = "Ich hoffe es für dich, Oktay." };
            Dialog dialog060 = new Dialog { Speaker = _merle, Content = "Wir alle hoffen das, Oktay." };
            Dialog dialog061 = new Dialog { Speaker = _oktay, Content = "Danke, das bedeutet mir viel." };
            Dialog dialog062 = new Dialog { Speaker = _alisa, Content = "Gut, dann lass uns nach vorne schauen." };
            Dialog dialog063 = new Dialog { Speaker = _tessa, Content = "Genau, die Vergangenheit können wir nicht ändern." };
            Dialog dialog064 = new Dialog { Speaker = _merle, Content = "Aber die Zukunft liegt in unseren Händen." };
            Dialog dialog065 = new Dialog { Speaker = _oktay, Content = "Ich werde nicht enttäuschen." };
            Dialog dialog066 = new Dialog { Speaker = _alisa, Content = "Das will ich hoffen." };
            Dialog dialog067 = new Dialog { Speaker = _tessa, Content = "Aktionen sprechen lauter als Worte." };
            Dialog dialog068 = new Dialog { Speaker = _merle, Content = "Manchmal sagen Worte aber auch viel." };
            Dialog dialog069 = new Dialog { Speaker = _oktay, Content = "Ich verstehe, es braucht beides." };
            Dialog dialog070 = new Dialog { Speaker = _alisa, Content = "Ja, Worte und Taten." };
            Dialog dialog071 = new Dialog { Speaker = _tessa, Content = "Dann zeig uns, dass du es ernst meinst." };
            Dialog dialog072 = new Dialog { Speaker = _merle, Content = "Wir sind gespannt, Oktay." };
            Dialog dialog073 = new Dialog { Speaker = _oktay, Content = "Ich werde euch nicht enttäuschen, versprochen." };
            Dialog dialog074 = new Dialog { Speaker = _alisa, Content = "Das will ich hoffen." };
            Dialog dialog075 = new Dialog { Speaker = _merle, Content = "Gut gesprochen, Oktay." };
            Dialog dialog076 = new Dialog { Speaker = _tessa, Content = "Dann lass die Taten folgen." };
            Dialog dialog077 = new Dialog { Speaker = _oktay, Content = "Ich fange gleich morgen an." };
            Dialog dialog078 = new Dialog { Speaker = _alisa, Content = "Warum nicht jetzt?" };
            Dialog dialog079 = new Dialog { Speaker = _tessa, Content = "Stimmt, warum zögern?" };
            Dialog dialog080 = new Dialog { Speaker = _merle, Content = "Oktay, manchmal ist 'jetzt' der beste Moment." };
            Dialog dialog081 = new Dialog { Speaker = _oktay, Content = "Ihr habt recht. Keine Ausreden mehr." };
            Dialog dialog082 = new Dialog { Speaker = _alisa, Content = "Das ist die Einstellung." };
            Dialog dialog083 = new Dialog { Speaker = _merle, Content = "Ja, keine Zeit wie die Gegenwart." };
            Dialog dialog084 = new Dialog { Speaker = _tessa, Content = "Genau, lass uns das umsetzen." };
            Dialog dialog085 = new Dialog { Speaker = _oktay, Content = "In Ordnung, was machen wir als Erstes?" };
            Dialog dialog086 = new Dialog { Speaker = _alisa, Content = "Das bleibt abzuwarten." };
            Dialog dialog087 = new Dialog { Speaker = _tessa, Content = "Aber egal was es ist, wir werden es meistern." };
            Dialog dialog088 = new Dialog { Speaker = _merle, Content = "Gemeinsam sind wir stark." };
            Dialog dialog089 = new Dialog { Speaker = _oktay, Content = "Dann lasst uns loslegen." };
            Dialog dialog090 = new Dialog { Speaker = _alisa, Content = "Bereit wenn du es bist." };
            Dialog dialog091 = new Dialog { Speaker = _tessa, Content = "Ich bin immer bereit." };
            Dialog dialog092 = new Dialog { Speaker = _merle, Content = "Dann gibt es nichts mehr zu sagen, außer 'Auf geht's!'." };
            Dialog dialog093 = new Dialog { Speaker = _oktay, Content = "Auf geht's!" };


            // Füge die Dialoge zur Szene hinzu
            _introScene.Dialogs.AddRange(new[] {
                dialog000, dialog001, dialog002, dialog003, dialog004, dialog005, dialog006, dialog007,
                dialog008, dialog009, dialog010, dialog011, dialog012, dialog013, dialog014, dialog015,
                dialog016, dialog017, dialog018, dialog019, dialog020, dialog021, dialog022, dialog023,
                dialog024, dialog025, dialog026, dialog027, dialog028, dialog029, dialog030, dialog031,
                dialog032, dialog033, dialog034, dialog035, dialog036, dialog037, dialog038, dialog039,
                dialog040, dialog041, dialog042, dialog043, dialog044, dialog045, dialog046, dialog047,
                dialog048, dialog049, dialog050, dialog051, dialog052, dialog053, dialog054, dialog055,
                dialog056, dialog057, dialog058, dialog059, dialog060, dialog061, dialog062, dialog063,
                dialog064, dialog065, dialog066, dialog067, dialog068, dialog069, dialog070, dialog071,
                dialog072, dialog073, dialog074, dialog075, dialog076, dialog077, dialog078, dialog079,
                dialog080, dialog081, dialog082, dialog083, dialog084, dialog085, dialog086, dialog087,
                dialog088, dialog089, dialog090, dialog091, dialog092, dialog093
            });

            _gameState.Scenes = new();
            _gameState.Scenes.Add(_introScene);
            _gameState.CurrentSceneIndex = 0;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _gameState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            _gameState.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}