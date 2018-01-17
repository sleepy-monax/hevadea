using Maker.Hevadea.Enum;
using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component.Interaction;
using Maker.Hevadea.Game.Entities.Component.Misc;
using Maker.Hevadea.Game.Menus;
using Maker.Hevadea.Game.UI;
using Maker.Hevadea.Json;
using Maker.Rise;
using Maker.Rise.Components;
using Maker.Rise.Enum;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace Maker.Hevadea.Scenes
{
    public class GameScene : Scene
    {
        private SpriteBatch spriteBatch;
        public World World;
        public Menu CurrentMenu = null;
        public Random Random = new Random();

        public GameScene(World world)
        {
            World = world;
        }

        public override void Load()
        {
            spriteBatch = Engine.Graphic.CreateSpriteBatch();
            World.Initialize(this);
            UiRoot.Padding = new Padding(16);
            UiRoot.AddChild(new PlayerInfoPanel(World.Player) { Dock = Dock.Bottom, Bound = new Rectangle(64, 64, 64, 64) });
            Engine.Scene.Background = null;
        }


        public override void Update(GameTime gameTime)
        {
            if (CurrentMenu == null || !CurrentMenu.PauseGame)
            {
                World.Update(gameTime);


                var playerMovement = World.Player.Components.Get<MoveComponent>();
            
                if (Engine.Input.KeyDown(Keys.Q))  { playerMovement.Move(-1, 0, Direction.Left); }
                if (Engine.Input.KeyDown(Keys.D))  { playerMovement.Move(1, 0, Direction.Right); }
                if (Engine.Input.KeyDown(Keys.Z))  { playerMovement.Move(0, -1, Direction.Up);   }
                if (Engine.Input.KeyDown(Keys.S))  { playerMovement.Move(0, 1, Direction.Down);  }
                if (Engine.Input.KeyPress(Keys.I)) { SetMenu(new InventoryMenu(World.Player, World, this)); }
                if (Engine.Input.KeyPress(Keys.N)) { playerMovement.NoClip = !playerMovement.NoClip; }

                if (Engine.Input.KeyPress(Keys.D1)) {var z = new ZombieEntity(); World.Player.Level.AddEntity(z); z.SetPosition(World.Player.X, World.Player.Y);}
                if (Engine.Input.KeyPress(Keys.D2)) { var z = new ChestEntity(); World.Player.Level.AddEntity(z); z.SetPosition(World.Player.X, World.Player.Y); }
                if (Engine.Input.KeyPress(Keys.D3)) { var z = new TorchEntity(); World.Player.Level.AddEntity(z); z.SetPosition(World.Player.X, World.Player.Y); }

                if (Engine.Input.MouseLeft) World.Player.Components.Get<AttackComponent>().Attack(World.Player.HoldingItem);
                if (Engine.Input.MouseRight) World.Player.Components.Get<InteractComponent>().Interact(World.Player.HoldingItem);

                if (Engine.Input.KeyPress(Keys.P)) { File.WriteAllText("test.json", World[0].Save().ToJson()); }
            }

            if (Engine.Input.KeyPress(Keys.Escape) && CurrentMenu != null)
            {
                UiRoot.RemoveChild(CurrentMenu);
                CurrentMenu = null;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            World.Draw(gameTime);
        }

        public override void Unload()
        {
        }

        public void SetMenu(Menu menu)
        {
            if (CurrentMenu != null)
            {
                UiRoot.RemoveChild(CurrentMenu);
            }

            CurrentMenu = menu;
            menu.Dock = Dock.Fill;
            UiRoot.AddChild(menu);
            UiRoot.RefreshLayout();
        }

        public override string GetDebugInfo()
        {
            return
                $@"World time: {World.Time}
Player pos {World.Player.X} {World.Player.Y}
";
        }
    }
}