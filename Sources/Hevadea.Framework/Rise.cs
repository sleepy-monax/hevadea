using Hevadea.Framework.Debug;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Input;
using Hevadea.Framework.Platform;
using Hevadea.Framework.Ressource;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Framework
{
    public static class Rise
    {
        // Configs
        public static bool DebugUI { get; set; } = false;
        public static bool ShowDebug { get; set; } = false;
        
        // Components
        [Obsolete] public static LegacyInputManager Input;

        public static KeyboardInputManager Keyboard;
        public static Controller Controller;
        public static Pointing Pointing;
        
        public static IPlatform Platform;
        public static DebugManager Debug;
        public static GraphicManager Graphic;
        public static MonoGameHandler MonoGame;
        public static SceneManager Scene;
        public static UiManager Ui;
        public static RessourceManager Ressource;
        public static Random Random = new Random();

        private static Scene _startScene;
        
        public static void Initialize(IPlatform platform)
        {
            Platform = platform;
            
            MonoGame = new MonoGameHandler();
            MonoGame.OnInitialize += MonoGameOnInitialize;
            MonoGame.OnLoadContent += MonoGameOnLoadContent;
            MonoGame.OnUnloadContent += MonoGameOnUnloadContent;
            
            MonoGame.OnUpdate += MonoGameOnUpdate;
            MonoGame.OnDraw += MonoGameOnDraw;
        } 

        public static void Start(Scene startScene)
        {
            _startScene = startScene;
            Platform.Initialize();
            MonoGame.Run();
        }
        
        private static void MonoGameOnInitialize(object sender, EventArgs eventArgs)
        {
            Ressource = new RessourceManager();
            
            Keyboard = new KeyboardInputManager();
            Controller = new Controller();
            Pointing = new Pointing();
            Input = new LegacyInputManager();
            
            Graphic = new GraphicManager(MonoGame.Graphics, MonoGame.GraphicsDevice);
            Scene = new SceneManager();
            Ui = new UiManager();
            Debug = new DebugManager();

            Graphic.ResetRenderTargets();
            Scene.Initialize();
            Input.Initialize();
            if (Platform.GetPlatformName() != "Android")
            {
                Keyboard.Initialize(MonoGame, 300, 5);
            }
            Scene.Switch(_startScene);
            
        }
        
        private static void MonoGameOnLoadContent(object sender, EventArgs eventArgs)
        {

        }

        private static void MonoGameOnUnloadContent(object sender, EventArgs eventArgs)
        {

        }

        private static void MonoGameOnUpdate(Game sender, GameTime gameTime)
        {
            Pointing.Update();
            Input.Update(gameTime);
            Keyboard.Update();
            Scene.Update(gameTime);
            Debug.Update(gameTime);

            if (Input.KeyPress(Keys.F1))
            {
                DebugUI = !DebugUI;
            }
            
            if (Input.KeyPress(Keys.F2))
            {
                ShowDebug = !ShowDebug;
            }
        }

        private static void MonoGameOnDraw(Game sender, GameTime gameTime)
        {
            Graphic.ResetScissor();
            Graphic.Clear(Color.Black);
            Scene.Draw(gameTime);
            
            if (ShowDebug)
                Debug.Draw(gameTime);
        }
    }
}