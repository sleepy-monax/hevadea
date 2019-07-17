using Hevadea.Framework.UI;

namespace Hevadea.Scenes.Menus
{
    public class Menu : WidgetPanel
    {
        public GameState GameState;
        public bool PauseGame { get; set; } = false;

        public Menu(GameState gameState)
        {
            GameState = gameState;
        }
    }
}