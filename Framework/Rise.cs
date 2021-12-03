using Hevadea.Framework.Audio;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Input;
using Hevadea.Framework.Platform;
using Hevadea.Framework.Ressource;
using Hevadea.Framework.Threading;
using Hevadea.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Concurrent;
using Hevadea.Framework.Development;

namespace Hevadea.Framework
{
    public static class Rise
    {
        public static bool NoGraphic { get; private set; } = false;

        public static ConcurrentQueue<Job> BackgroundThread = new ConcurrentQueue<Job>();
        public static ConcurrentQueue<Job> GameLoopThread = new ConcurrentQueue<Job>();

        public static Controller Controller;
        public static Pointing Pointing;
        public static LegacyInputManager Input;

        public static GraphicManager Graphic;
        public static SoundManager Sound;

        public static PlatformBase Platform;
        public static DebugManager Debug;
        public static MonoGameHandler MonoGame;
        public static SceneManager Scene;
        public static UiManager Ui;
        public static ResourcesManager Resources;
        public static Random Rnd = new Random();

        public static Config Config = new Config();

        private static Scene _startScene;
        private static Action _initializeAction;

        public static void InitializeNoGraphic(PlatformBase platform)
        {
            Platform = platform;
            NoGraphic = true;
            Graphic = new GraphicManager(null, null);
            Resources = new ResourcesManager();
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
            GCListener.Start();
            MonoGame.Run();
        }

        private static void MonoGameOnInitialize(object sender, EventArgs eventArgs)
        {
            Resources = new ResourcesManager();

            Platform.Initialize();

            Logger.Log("Rise", LoggerLevel.Info, "Initializing modules...");

            Controller = new Controller();
            Pointing = new Pointing();
            Input = new LegacyInputManager();

            Sound = new SoundManager();
            Graphic = new GraphicManager(MonoGame.Graphics, MonoGame.GraphicsDevice);
            Scene = new SceneManager();
            Ui = new UiManager();
            Debug = new DebugManager();

            _initializeAction?.Invoke();

            Scene.Initialize();
            Input.Initialize();

            Scene.Switch(_startScene);

            Config.Apply();

            Graphic.AllowUserResizing();
        }

        private static void MonoGameOnLoadContent(object sender, EventArgs eventArgs)
        {
        }

        private static void MonoGameOnUnloadContent(object sender, EventArgs eventArgs)
        {
        }

        private static void MonoGameOnUpdate(Microsoft.Xna.Framework.Game sender, GameTime gameTime)
        {
            if (GameLoopThread.TryDequeue(out var job)) job.Start(false);

            Pointing.Update();
            Input.Update(gameTime);
            Scene.Update(gameTime);
            Debug.Update(gameTime);
            Sound.Update(gameTime);

            if (Input.KeyTyped(Keys.F1))
                Debug.HELP = !Debug.HELP;

            if (Input.KeyTyped(Keys.F2))
                Debug.GAME = !Debug.GAME;

            if (Input.KeyTyped(Keys.F3))
                Debug.GENERAL = !Debug.GENERAL;

            if (Input.KeyTyped(Keys.F4))
                Debug.UI = !Debug.UI;

            if (Input.KeyTyped(Keys.F5))
                Ui.Enabled = !Ui.Enabled;

            if (Input.KeyTyped(Keys.F6))
            {
                Config.UIScaling -= 0.1f;
                Ui.RefreshLayout();
            }

            if (Input.KeyTyped(Keys.F7))
            {
                Config.UIScaling += 0.1f;
                Ui.RefreshLayout();
            }

            if (Input.KeyTyped(Keys.F8))
            {
                Config.UIScaling = 1f;
                Ui.RefreshLayout();
            }
        }

        private static void MonoGameOnDraw(Microsoft.Xna.Framework.Game sender, GameTime gameTime)
        {
            Graphic.ResetScissor();
            Graphic.Clear(Color.Black);
            Scene.Draw(gameTime);
            Debug.Draw(gameTime);
        }
    }
}