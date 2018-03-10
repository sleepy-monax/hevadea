using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Game.Tiles.Renderers
{
    public class TileConection
    {
        public bool Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight;

        public TileConection(bool u, bool d, bool l, bool r, bool ul, bool ur, bool dl, bool dr)
        {
            Up = u;
            Down = d;
            Left = l;
            Right = r;
            UpLeft = ul;
            UpRight = ur;
            DownLeft = dl;
            DownRight = dr;
        }
    }
}
