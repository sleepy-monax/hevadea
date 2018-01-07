using Maker.Hevadea.Scenes;
using Maker.Rise.UI;

namespace Maker.Hevadea.Game.Menus
{
    public abstract class Menu : Control
    {
        private World World;
        public bool PauseGame = false;
        public GameScene Scene;

        public Menu(World world, GameScene scene)
        {
            World = world;
            Scene = scene;
        }

        public void Show()
        {
        }

        public void Close()
        {
        }
    }
}