using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Hevadea.Game.Tiles
{
    public class TilePosition
    {
        public int X { get; set; }
        public int Y { get; set; }

        public TilePosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {

            if (obj is TilePosition e)
            {
                return X == e.X && Y == e.Y;
            }

            return false;
        }

        public Point ToOnScreenPosition()
        {

            return new Point(X * ConstVal.TileSize, Y * ConstVal.TileSize);

        }
    }
}
