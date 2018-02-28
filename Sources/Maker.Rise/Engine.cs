using Hevadea.Framework.Utils;
using Hevadea.Framework.Utils.Json;
using Maker.Rise.Components;
using Maker.Rise.Platform;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Maker.Rise
{
    public static class Engine
    {
        public static IPlatform Platform;
        public static GraphicComponent Graphic;
        public static AudioManager Audio;
        public static DebugManager Debug;
        public static InputManager Input;
        public static NetworkManager Network;
        public static RessourceManager Ressource;
        public static SceneManager Scene;
        public static RasterizerState CommonRasterizerState;
        public static Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public static readonly Random Random = new Random();
        
        public static EngineConfig Configuration = new EngineConfig();
        public static InternalGame MonoGameHandle;
        private static Scene MainScene;
        

        public static void Initialize(IPlatform platform)
        {
            Platform = platform;
            // Initialize the game engine.
            MonoGameHandle = new InternalGame();

            Audio = new AudioManager(MonoGameHandle);
            Input = new InputManager(MonoGameHandle);
            Network = new NetworkManager(MonoGameHandle);
            Scene = new SceneManager(MonoGameHandle);
            Ressource = new RessourceManager(MonoGameHandle);
            Debug = new DebugManager(MonoGameHandle);
            Graphic = new GraphicComponent(MonoGameHandle);
            CommonRasterizerState = new RasterizerState {ScissorTestEnable = false};

            if (File.Exists("engine.json"))
            {
                var watcher = new FileSystemWatcher(".", "engine.json")
                {
                    EnableRaisingEvents = true,
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime,
                };
                watcher.Changed += (sender, args) =>
                    {
                        Logger.Log(LoggerLevel.Info,"Reloading engine configs...");
                        try
                        {
                            Thread.Sleep(200);
                            Configuration = File.ReadAllText("engine.json").FromJson<EngineConfig>();
                            Logger.Log(LoggerLevel.Fine, "Config reloaded!");
                        }
                        catch (Exception ex)
                        {
                            Logger.Log(LoggerLevel.Error, "Failled to reload configs...");
                            Logger.Log(ex);
                        }
                    };
                Configuration = File.ReadAllText("engine.json").FromJson<EngineConfig>();
            }
        }

        public static void Start(Scene mainScene)
        {
            Engine.MainScene = mainScene;
            MonoGameHandle.OnLoad += Game_OnLoad;
            MonoGameHandle.Run();
        }

        public static void Stop()
        {
            File.WriteAllText("engine.json", Configuration.ToJson());
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