using Hevadea.Framework.UI.Widgets;

namespace Hevadea.Scenes.Menus
{
    public class Menu : Panel
    {
        public GameState GameState;
        public bool PauseGame { get; set; } = false;

        public Menu(GameState gameState)
        {
            GameState = gameState;
        }
    }
}