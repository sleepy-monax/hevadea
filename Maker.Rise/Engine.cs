using Maker.Rise.Components;
using Maker.Rise.Extension;
using Maker.Rise.Logging;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;

namespace Maker.Rise
{
    public static class Engine
    {
        public static GraphicComponent Graphic;
        public static AudioManager Audio;
        public static DebugManager Debug;
        public static InputManager Input;
        public static NetworkManager Network;
        public static RessourceManager Ressource;
        public static SceneManager Scene;
        public static UiManager Ui;
        public static RasterizerState CommonRasterizerState;
        public static Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public static Random Random = new Random();
        
        private static RiseGame game;
        private static Scene MainScene;
        

        public static void Initialize()
        {
            
            // Initialize the logger
            Logger.DefaultInitialization();
            
            // Initialize the game engine.
            game = new RiseGame();

            Audio = new AudioManager(game);
            Input = new InputManager(game);
            Network = new NetworkManager(game);
            Scene = new SceneManager(game);
            Ui = new UiManager(game);
            Ressource = new RessourceManager(game);
            Debug = new DebugManager(game);
            Graphic = new GraphicComponent(game);
            CommonRasterizerState = new RasterizerState {ScissorTestEnable = true};
            
        }

        public static void Start(Scene mainScene)
        {
            MainScene = mainScene;
            game.OnLoad += Game_OnLoad;
            game.Run();
        }

        public static void Stop()
        {
            game.Exit();
            Environment.Exit(0);
        }

        public static void SetMouseVisibility(bool visible)
        {
            game.IsMouseVisible = visible;
        }

        private static void Game_OnLoad(RiseGame sender, EventArgs e)
        {
            Scene.Switch(MainScene);
        }
    }
}