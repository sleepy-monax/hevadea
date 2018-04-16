using Hevadea.Framework.UI.Widgets;

namespace Hevadea.Scenes.Menus
{
    public class Menu : Panel
    {
        public GameManager.GameManager Game;

        public Menu(GameManager.GameManager game)
        {
            Game = game;
        }

        public bool PauseGame { get; set; } = false;

        public void Show()
        {
        }

        public void Close()
        {
        }
    }
}