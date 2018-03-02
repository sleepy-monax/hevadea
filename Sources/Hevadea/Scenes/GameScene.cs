using Hevadea.Framework;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Game;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Entities.Component.Attributes;
using Hevadea.Game.Entities.Creatures;
using Hevadea.Game.Entities.Furnitures;
using Hevadea.Game.Registry;
using Hevadea.Scenes.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hevadea.Scenes
{
    public class GameScene : Scene
    {
        public readonly GameManager Game;

        public GameScene(GameManager game)
        {
            Game = game;
            Game.CurrentMenuChange += Game_CurrentMenuChange;
            Container = new Panel();
        }

        private void Game_CurrentMenuChange(Menu oldMenu, Menu newMenu)
        {
            // Add the menu to the ui tree.
            var p = (Panel) Container;
            p.Content = newMenu;
            Container.RefreshLayout();
        }

        public override void Load()
        {
            Game.Initialize();
        }


        public override void OnUpdate(GameTime gameTime)
        {
            if (Game.CurrentMenu == null || !Game.CurrentMenu.PauseGame)
            {
                Game.Update(gameTime);

                if (Rise.Input.KeyDown(Keys.Z)) Game.PlayerInput.HandleInput(PlayerInput.MoveUp);
                if (Rise.Input.KeyDown(Keys.S)) Game.PlayerInput.HandleInput(PlayerInput.MoveDown);
                if (Rise.Input.KeyDown(Keys.Q)) Game.PlayerInput.HandleInput(PlayerInput.MoveLeft);
                if (Rise.Input.KeyDown(Keys.D)) Game.PlayerInput.HandleInput(PlayerInput.MoveRight);
                
                if (Rise.Input.KeyPress(Keys.E)) Game.PlayerInput.HandleInput(PlayerInput.OpenInventory);

                if (Rise.Input.KeyDown(Keys.J)) Game.PlayerInput.HandleInput(PlayerInput.Attack);
                if (Rise.Input.KeyPress(Keys.K)) Game.PlayerInput.HandleInput(PlayerInput.Action);
            }

            if (Rise.Input.KeyPress(Keys.Escape)) Game.PlayerInput.HandleInput(PlayerInput.OpenPauseMenu);
        }

        public override void OnDraw(GameTime gameTime)
        {
            Game.Draw(gameTime);
        }

        public override void Unload()
        {
        }

        public override string GetDebugInfo()
        {
            return
                $@"World time: {Game.World.DayNightCycle.Time}
Time of the day: {Game.World.DayNightCycle.TimeOfTheDay} / {Game.World.DayNightCycle.CycleDuration}
Days : {Game.World.DayNightCycle.DayCount}
Current Stage: {Game.World.DayNightCycle.GetCurrentStage().Name} : {(int)Game.World.DayNightCycle.GetTimeOfTheCurrentStage()}/{(int)Game.World.DayNightCycle.GetCurrentStage().Duration}
Player pos {Game.Player.X} {Game.Player.Y}
";
        }
    }
}