using Maker.Hevadea.Scenes;
using Maker.Rise.Extension;
using Maker.Rise.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Menus
{
    public class Menu : Control
    {
        private World World;
        public bool PauseGame = false;
        public GameScene Game;

        public Menu(World world, GameScene game)
        {
            World = world;
            Game = game;
            Padding = new Padding(8);
        }

        public void Show()
        {
        }

        protected override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.FillRectangle(Bound, new Color(176,112,48));
            spriteBatch.DrawRectangle(Bound, Color.White * 0.50f);
        }

        protected override void OnUpdate(GameTime gameTime)
        {

        }

        public void Close()
        {

        }
    }
}