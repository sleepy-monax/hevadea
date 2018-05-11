using Hevadea.Framework.Scening;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Scenes.Menus;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes
{
    public class SceneGameplay : Scene
    {
        public readonly GameManager Game;

        public SceneGameplay(GameManager game)
        {
            Game = game;
            Game.CurrentMenuChange += Game_CurrentMenuChange;
            Container = new Panel
            {
                Padding = new Padding(16)
            };
        }

        private void Game_CurrentMenuChange(Menu oldMenu, Menu newMenu)
        {
            var p = (Panel)Container;
            p.Content = newMenu;
            Container.RefreshLayout();
        }

        public override void Load()
        {
            Game_CurrentMenuChange(null, Game.CurrentMenu);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            Game.Update(gameTime);
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
$@"World time: {(int)Game.World.DayNightCycle.Time}
Time of the day: {(int)Game.World.DayNightCycle.TimeOfTheDay} / {Game.World.DayNightCycle.CycleDuration}
Days : {Game.World.DayNightCycle.DayCount}
Current Stage: {Game.World.DayNightCycle.GetCurrentStage().Name} : {(int)Game.World.DayNightCycle.GetTimeOfTheCurrentStage()}/{(int)Game.World.DayNightCycle.GetCurrentStage().Duration}
Player pos {Game.MainPlayer.X} {Game.MainPlayer.Y}
";
        }
    }
}