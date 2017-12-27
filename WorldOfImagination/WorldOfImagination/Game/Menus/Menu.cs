using Maker.Rise.GameComponent;
using Maker.Rise.GameComponent.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WorldOfImagination.Scenes;

namespace WorldOfImagination.Game.Menus
{
    public abstract class Menu : Control
    {
        private World World;
        public bool PauseGame = false;
        public GameScene Scene;
        
        public Menu(UiManager ui, World world, GameScene scene) : base(ui)
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