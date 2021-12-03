using Hevadea.Framework;
using Hevadea.Scenes.Menus;
using Microsoft.Xna.Framework;

namespace Hevadea.Scenes
{
    public class SceneGameplay : Scene
    {
        public readonly GameState GameState;

        public SceneGameplay(GameState gameState)
        {
            Rise.Sound.Play(Resources.Overworld0);
            GameState = gameState;
            GameState.CurrentMenuChange += Game_CurrentMenuChange;
        }

        private void Game_CurrentMenuChange(Menu oldMenu, Menu newMenu)
        {
            Container = newMenu;
            RefreshLayout();
        }

        public override void Load()
        {
            Game_CurrentMenuChange(null, GameState.CurrentMenu);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            GameState.Update(gameTime);
        }

        public override void OnDraw(GameTime gameTime)
        {
            GameState.Draw(gameTime);
        }

        public override void Unload()
        {
        }

        public override string GetDebugInfo()
        {
            return
                $@"World time: {(int) GameState.World.DayNightCycle.Time}
Time of the day: {(int) GameState.World.DayNightCycle.TimeOfTheDay} / {GameState.World.DayNightCycle.CycleDuration}
Days : {GameState.World.DayNightCycle.DayCount}
Current Stage: {GameState.World.DayNightCycle.GetCurrentStage().Name} : {(int) GameState.World.DayNightCycle.GetTimeOfTheCurrentStage()}/{(int) GameState.World.DayNightCycle.GetCurrentStage().Duration}
Player pos {GameState.LocalPlayer.Entity.X} {GameState.LocalPlayer.Entity.Y}
";
        }
    }
}