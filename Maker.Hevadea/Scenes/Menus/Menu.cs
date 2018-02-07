using Maker.Rise.UI.Widgets;

namespace Maker.Hevadea.Game.Menus
{
    public class Menu : Panel
    {
        public bool PauseGame { get; set; } = false;
        public GameManager Game;

        public Menu(GameManager game)
        {
            Game = game;
        }

        public void Show()
        {
        }

        public void Close()
        {

        }
    }
}