using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Tiles;
using Microsoft.Xna.Framework;

namespace Hevadea.Worlds
{
    public partial class Level
    {
        public bool IsAll(Tile tile, Rectangle rectangle)
        {
            var beginX = rectangle.X / GLOBAL.Unit - 1;
            var beginY = rectangle.Y / GLOBAL.Unit - 1;

            var endX = (rectangle.X + rectangle.Width) / GLOBAL.Unit + 1;
            var endY = (rectangle.Y + rectangle.Height) / GLOBAL.Unit + 1;

            var result = true;

            for (var x = beginX; x < endX; x++)
                for (var y = beginY; y < endY; y++)
                {
                    if (x < 0 || y < 0 || x >= Width || y >= Height) continue;
                    result &= GetTile(x, y) == tile;
                }

            return result;
        }

        public bool IsAll<T>(Rectangle rectangle) where T : TileComponent
        {
            var beginX = rectangle.X / GLOBAL.Unit;
            var beginY = rectangle.Y / GLOBAL.Unit;

            var endX = (rectangle.X + rectangle.Width) / GLOBAL.Unit;
            var endY = (rectangle.Y + rectangle.Height) / GLOBAL.Unit;

            bool result = GetTile(beginX, beginY).HasTag<T>(); ;

            for (var x = beginX; x <= endX; x++)
                for (var y = beginY; y <= endY; y++)
                {
                    if (x < 0 || y < 0 || x >= Width || y >= Height) continue;

                    result &= GetTile(x, y).HasTag<T>();
                }

            return result;
        }


    }
}