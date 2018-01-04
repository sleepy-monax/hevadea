using Maker.Rise.Components;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Rise
{
    public static class Engine
    {
        public static Microsoft.Xna.Framework.GraphicsDeviceManager Graphic;
        public static AudioManager Audio;
        public static DebugManager Debug;
        public static InputManager Input;
        public static NetworkManager Network;
        public static RessourceManager Ressource;
        public static SceneManager Scene;
        public static UiManager UI;
        public static RasterizerState CommonRasterizerState;
        public static Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        private static RiseGame game;
        private static Scene MainScene;


        public static void Initialize()
        {
            game = new RiseGame();

            Audio = new AudioManager(game);
            Input = new InputManager(game);
            Network = new NetworkManager(game);
            Scene = new SceneManager(game);
            UI = new UiManager(game);
            Ressource = new RessourceManager(game);
            Debug = new DebugManager(game);
            Graphic = new Microsoft.Xna.Framework.GraphicsDeviceManager(game);
            CommonRasterizerState = new RasterizerState { ScissorTestEnable = true };
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

        public static void SetFullScreen(bool fullscreen)
        {
            Graphic.SetFullScreen();
        }

        private static void Game_OnLoad(RiseGame sender, EventArgs e)
        {
            Scene.Switch(MainScene);
        }
    }
}
