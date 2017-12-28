using Maker.Rise.Components;
using Maker.Rise.UI;
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