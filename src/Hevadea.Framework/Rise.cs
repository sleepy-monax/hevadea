using Hevadea.Framework.Debug;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Input;
using Hevadea.Framework.Platform;
using Hevadea.Framework.Ressource;
using Hevadea.Framework.Scening;
using Hevadea.Framework.Threading;
using Hevadea.Framework.UI;
using Hevadea.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Concurrent;

namespace Hevadea.Framework
{
    public static class Rise
    {
        // Configs
        public static bool ShowGui { get; set; } = true;

        public static bool NoGraphic { get; private set; } = false;

        public static ConcurrentQueue<Job> BackgroundThread = new ConcurrentQueue<Job>();
        public static ConcurrentQueue<Job> GameLoopThread = new ConcurrentQueue<Job>();

        // Components
        public static LegacyInputManager Input;

        public static Controller Controller;
        public static Pointing Pointing;

        public static PlatformBase Platform;
        public static DebugManager Debug;
        public static GraphicManager Graphic;
        public static MonoGameHandler MonoGame;
        public static SceneManager Scene;
        public static UiManager Ui;
        public static RessourceManager Ressource;
        public static FastRandom Rnd = new FastRandom();

        private static Scene _startScene;
        private static Action _initializeAction;

        public static void InitializeNoGraphic(PlatformBase platform)
        {
            Platform = platform;
            NoGraphic = true;
            Graphic = new GraphicManager(null, null);
            Ressource = new RessourceManager();
        }

        public static void Initialize(PlatformBase platform)
        {
            Platform = platform;

            MonoGame = new MonoGameHandler();
            MonoGame.OnInitialize += MonoGameOnInitialize;
            MonoGame.OnLoadContent += MonoGameOnLoadContent;
            MonoGame.OnUnloadContent += MonoGameOnUnloadContent;

            MonoGame.OnUpdate += MonoGameOnUpdate;
            MonoGame.OnDraw += MonoGameOnDraw;
        }

        public static void Start(Scene startScene, Action initializeAction = null)
        {
            _initializeAction = initializeAction;
            _startScene = startScene;
            //GCListener.Start();
            MonoGame.Run();
        }

        private static void MonoGameOnInitialize(object sender, EventArgs eventArgs)
        {
            Ressource = new RessourceManager();

            Platform.Initialize();

            Logger.Log("Rise", LoggerLevel.Info, "Initializing modules...");

            Controller = new Controller();
            Pointing = new Pointing();
            Input = new LegacyInputManager();

            Graphic = new GraphicManager(MonoGame.Graphics, MonoGame.GraphicsDevice);
            Scene = new SceneManager();
            Ui = new UiManager();
            Debug = new DebugManager();

            _initializeAction?.Invoke();

            Graphic.ResetRenderTargets();
            Scene.Initialize();
            Input.Initialize();

            Scene.Switch(_startScene);

            if (Platform.Family == PlatformFamily.Desktop)
            {
                Graphic.SetSize(1366, 768);
            }
        }

        private static void MonoGameOnLoadContent(object sender, EventArgs eventArgs)
        {
        }

        private static void MonoGameOnUnloadContent(object sender, EventArgs eventArgs)
        {
        }

        private static void MonoGameOnUpdate(Game sender, GameTime gameTime)
        {
            if (GameLoopThread.TryDequeue(out var job))
            {
                job.Start(false);
            }

            Pointing.Update();
            Input.Update(gameTime);
            Scene.Update(gameTime);
            Debug.Update(gameTime);

            if (Input.KeyPress(Keys.F1))
            {
                Debug.HELP = !Debug.HELP;
            }

            if (Input.KeyPress(Keys.F2))
            {
                Debug.GAME = !Debug.GAME;
            }

            if (Input.KeyPress(Keys.F3))
            {
                Debug.GENERAL = !Debug.GENERAL;
            }

            if (Input.KeyPress(Keys.F4))
            {
                Debug.UI = !Debug.UI;
            }

            if (Input.KeyPress(Keys.F5))
            {
                ShowGui = !ShowGui;
            }

            if (Input.KeyPress(Keys.F6))
            {
                Ui.ScaleFactor /= 2f;
                Scene.GetCurrentScene()?.RefreshLayout();
            }

            if (Input.KeyPress(Keys.F7))
            {
                Ui.ScaleFactor *= 2f;
                Scene.GetCurrentScene()?.RefreshLayout();
            }
        }

        private static void MonoGameOnDraw(Game sender, GameTime gameTime)
        {
            Graphic.ResetScissor();
            Graphic.Clear(Color.Black);
            Scene.Draw(gameTime);
            Debug.Draw(gameTime);
        }
    }
}