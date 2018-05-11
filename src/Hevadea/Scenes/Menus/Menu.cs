using Hevadea.Framework.UI.Widgets;

namespace Hevadea.Scenes.Menus
{
    public class Menu : Panel
    {
        public GameManager Game;
		public bool PauseGame { get; set; } = false;

        public Menu(GameManager game)
        {
            Game = game;
        }
    }
}