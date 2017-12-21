using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfImagination.Game.Tiles;

namespace WorldOfImagination.Game.Entities
{
    public class EntityPosition
    {
        public int X { get; set; }
        public int Y { get; set; }

        public EntityPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public EntityPosition(EntityPosition p)
        {
            X = p.X;
            Y = p.Y;
        }

        public TilePosition ToTilePosition()
        {
            return new TilePosition(X / ConstVal.TileSize, 
                                    Y / ConstVal.TileSize);
        }

        public override bool Equals(object obj)
        {

            if (obj is EntityPosition e)
            {
                return X == e.X && Y == e.Y;
            }

            return false;
        }
    }
}
