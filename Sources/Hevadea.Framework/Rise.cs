using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml;
using Hevadea.Framework.Debug;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Input;
using Hevadea.Framework.Platform;
using Hevadea.Framework.Ressource;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI;
using Microsoft.Xna.Framework;

namespace Hevadea.Framework
{
    public static class Rise
    {
        // Configs
        public static bool DebugUI { get; set; }
        public static bool ShowDebug { get; set; }
        
        // Components
        [Obsolete] public static LegacyInputManager Input;
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
            Controller = new Controller();
            Pointing = new Pointing();
            Debug = new DebugManager();
            Graphic = new GraphicManager(new GraphicsDeviceManager(MonoGame) , MonoGame.GraphicsDevice);
            Scene = new SceneManager();
        }
        
        private static void MonoGameOnLoadContent(object sender, EventArgs eventArgs)
        {
            Input.Initialize();
            Scene.Switch(_startScene);
        }

        private static void MonoGameOnUnloadContent(object sender, EventArgs eventArgs)
        {

        }

        private static void MonoGameOnUpdate(Game sender, GameTime gameTime)
        {
            Input.Update(gameTime);
            Scene.Draw(gameTime);
        }

        private static void MonoGameOnDraw(Game sender, GameTime gameTime)
        {
            Scene.Update(gameTime);
        }
    }
}