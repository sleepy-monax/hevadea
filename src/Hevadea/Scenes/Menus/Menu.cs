using Hevadea.Framework.UI.Widgets;

namespace Hevadea.Scenes.Menus
{
    public class Menu : Panel
    {
        public Game Game;
        public bool PauseGame { get; set; } = false;

        public Menu(Game game)
        {
            Game = game;
        }
    }
}