using Hevadea.Framework.Graphic;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Tiles.Renderers
{
    public class TileRenderComposite : TileRenderConnected
    {
        public Sprite Sprite { get; }

        public TileRenderComposite(Sprite sprite)
        {
            Sprite = sprite;
        }

        public override void Draw(SpriteBatch spriteBatch, Coordinates coords, Level level)
        {
            var connection = GetTileConnection(level, coords);
            var onScreenPosition = coords.ToOnScreenPosition();

            if (!OnlyRenderOnConnection || connection.Up || connection.Down || connection.Left || connection.Right ||
                connection.UpLeft || connection.DownLeft || connection.UpRight || connection.DownRight)
            {
                DrawCorner(spriteBatch, connection.Left, connection.UpLeft, connection.Up,
                    new Point(0, 0), new Point(0, 2), new Point(0, 3), new Point(2, 0), new Point(2, 2),
                    (int) (onScreenPosition.X + 0), (int) (onScreenPosition.Y + 0));

                DrawCorner(spriteBatch, connection.Up, connection.UpRight, connection.Right, new Point(1, 0),
                    new Point(1, 2), new Point(0, 2), new Point(3, 0),
                    new Point(2, 2), (int) (onScreenPosition.X + 8), (int) (onScreenPosition.Y + 0));

                DrawCorner(spriteBatch, connection.Right, connection.DownRight, connection.Down, new Point(1, 1),
                    new Point(1, 3), new Point(1, 2), new Point(3, 1),
                    new Point(2, 2), (int) (onScreenPosition.X + 8), (int) (onScreenPosition.Y + 8));
                DrawCorner(spriteBatch, connection.Down, connection.DownLeft, connection.Left, new Point(0, 1),
                    new Point(0, 3), new Point(1, 3), new Point(2, 1),
                    new Point(2, 2), (int) onScreenPosition.X, (int) (onScreenPosition.Y + 8));
            }
        }

        public void DrawCorner(SpriteBatch spriteBatch, bool a, bool b, bool c, Point case1, Point case2, Point case3,
            Point case4, Point case5, int x, int y)
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