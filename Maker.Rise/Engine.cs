using Maker.Rise.Components;
using Maker.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;
using Maker.Rise.UI;

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
        public static readonly Random Random = new Random();
        
        public static EngineConfig Configuration = new EngineConfig();
        
        public static InternalGame MonoGameHandle;
        private static Scene MainScene;
        

        public static void Initialize()
        {
            
            // Initialize the logger
            Logger.DefaultInitialization();
            
            // Initialize the game engine.
            MonoGameHandle = new InternalGame();

            Audio = new AudioManager(MonoGameHandle);
            Input = new InputManager(MonoGameHandle);
            Network = new NetworkManager(MonoGameHandle);
            Scene = new SceneManager(MonoGameHandle);
            Ui = new UiManager(MonoGameHandle);
            Ressource = new RessourceManager(MonoGameHandle);
            Debug = new DebugManager(MonoGameHandle);
            Graphic = new GraphicComponent(MonoGameHandle);
            CommonRasterizerState = new RasterizerState {ScissorTestEnable = true};
            
        }

        public static void Start(Scene mainScene)
        {
            Engine.MainScene = mainScene;
            MonoGameHandle.OnLoad += Game_OnLoad;
            MonoGameHandle.Run();
        }

        public static void Stop()
        {
            MonoGameHandle.Exit();
            Environment.Exit(0);
        }

        public static void SetMouseVisibility(bool visible)
        {
            MonoGameHandle.IsMouseVisible = visible;
        }

        private static void Game_OnLoad(InternalGame sender, EventArgs e)
        {
            Scene.Switch(MainScene);
            Graphic.ResetRenderTargets();
        }
    }
}