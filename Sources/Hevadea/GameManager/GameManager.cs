using System.Collections.Generic;
using Hevadea.GameObjects.Entities.Blueprints.Legacy;
using Hevadea.Player;
using Hevadea.Scenes.Menus;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.GameManager
{
    public partial class GameManager
    {
        public string SavePath { get; set; } = "./test/";
        
        private Menu _currentMenu;

        public World World { get; set; }
        public EntityPlayer MainPlayer { get; set; }
        public List<EntityPlayer> Players { get; } = new List<EntityPlayer>();

        public Camera Camera { get; set; }
        public PlayerInputHandler PlayerInput { get; set; }

        public delegate void CurrentMenuChangeHandler(Menu oldMenu, Menu newMenu);
        public event CurrentMenuChangeHandler CurrentMenuChange;
        
        public Menu CurrentMenu
        {
            get => _currentMenu;
            set
            {
                CurrentMenuChange?.Invoke(_currentMenu, value);
                _currentMenu = value;
            }
        }

        public GameManager()
        {
        }

        public GameManager(World world, EntityPlayer player)
        {
            World = world;
            MainPlayer = player;
        }

        public void Initialize()
        {
            World.Initialize(this);
            CurrentMenu = new MenuInGame(this);

            if (MainPlayer.Removed)
            { 
                if (MainPlayer.X == 0f && MainPlayer.Y == 0f)
                {
                    World.SpawnPlayer(MainPlayer);
                }
                else
                {
                    World.GetLevel(MainPlayer.LastLevel).AddEntity(MainPlayer);
                }
            }
            
            PlayerInput = new PlayerInputHandler(MainPlayer);
            Camera = new Camera(MainPlayer);
            Camera.JumpToFocusEntity();
        }

        public void Draw(GameTime gameTime)
        {
            World.Draw(gameTime, Camera);
        }

        public void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
            PlayerInput.Update(gameTime);
            
            if (!CurrentMenu?.PauseGame ?? false)
            {
            
                foreach (var l in World.Levels)
                {
                    var state = l.GetRenderState(Camera);
                    l.Update(state, gameTime);
                }
            }

            World.DayNightCycle.UpdateTime(gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}