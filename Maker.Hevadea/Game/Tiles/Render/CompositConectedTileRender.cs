using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Tiles.Render
{
    public class CompositConectedTileRender
    {
        public CompositConectedTileRender(Sprite sprite)
        {
            Sprite = sprite;
        }

        public Sprite Sprite { get; }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, TileConection connection)
        {
            DrawCorner(spriteBatch, connection.Left, connection.UpLeft, connection.Up, new Point(0, 0), new Point(0, 2),
                new Point(0, 3), new Point(2, 0),
                new Point(2, 2), (int) (position.X + 0), (int) (position.Y + 0));
            DrawCorner(spriteBatch, connection.Up, connection.UpRight, connection.Right, new Point(1, 0),
                new Point(1, 2), new Point(0, 2), new Point(3, 0),
                new Point(2, 2), (int) (position.X + 8), (int) (position.Y + 0));

            DrawCorner(spriteBatch, connection.Right, connection.DownRight, connection.Down, new Point(1, 1),
                new Point(1, 3), new Point(1, 2), new Point(3, 1),
                new Point(2, 2), (int) (position.X + 8), (int) (position.Y + 8));
            DrawCorner(spriteBatch, connection.Down, connection.DownLeft, connection.Left, new Point(0, 1),
                new Point(0, 3), new Point(1, 3), new Point(2, 1),
                new Point(2, 2), (int) position.X, (int) (position.Y + 8));
        }

        public void DrawCorner(SpriteBatch spriteBatch,
            bool a, bool b, bool c,
            Point case1, Point case2, Point case3, Point case4, Point case5,
            int x, int y)
        {
            if (!a & !c)
                Sprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case1, Color.White);
            else if (a & !c)
                Sprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case2, Color.White);
            else if (!a & c)
                Sprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case3, Color.White);
            else if (!b)
                Sprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case4, Color.White);
            else
                Sprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case5 + case1, Color.White);
        }
    }
}