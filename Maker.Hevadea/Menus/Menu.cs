using Maker.Rise.UI.Widgets;

namespace Maker.Hevadea.Game.Menus
{
    public class Menu : Widget
    {
        public bool PauseGame = false;
        public GameManager Game;

        public Menu(GameManager game)
        {
            Game = game;
            Padding = new Padding(8);
        }

        public void Show()
        {
        }

        public void Close()
        {

        }
    }
}