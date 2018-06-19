using Hevadea.Framework.Extension;
using System.Collections.Generic;

namespace Hevadea.Tiles.Renderers
{
    public class TileConnection
    {
        public bool Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight;
        public Tile Tile;

        public TileConnection(Tile tile, bool u, bool d, bool l, bool r, bool ul, bool ur, bool dl, bool dr)
        {
            Tile = tile;

            Up = u;
            Down = d;
            Left = l;
            Right = r;
            UpLeft = ul;
            UpRight = ur;
            DownLeft = dl;
            DownRight = dr;
        }

        public byte ToByte()
        {
            byte v = 0;

            v = v.WriteBit(0, UpLeft & Up & Left);
            v = v.WriteBit(1, Up);
            v = v.WriteBit(2, UpRight & Up & Right);
            v = v.WriteBit(4, Right);
            v = v.WriteBit(3, Left);
            v = v.WriteBit(5, DownLeft & Down & Left);
            v = v.WriteBit(6, Down);
            v = v.WriteBit(7, DownRight & Down & Right);

            if (conv.ContainsKey(v))
                return conv[v];
            return v;
        }

        public static Dictionary<int, byte> conv = new Dictionary<int, byte>
        {
            {2, 1},
            {8, 2},
            {10, 3},
            {11, 4},
            {16, 5},
            {18, 6},
            {22, 7},
            {24, 8},
            {26, 9},
            {27, 10},
            {30, 11},
            {31, 12},
            {64, 13},
            {66, 14},
            {72, 15},
            {74, 16},
            {75, 17},
            {80, 18},
            {82, 19},
            {86, 20},
            {88, 21},
            {90, 22},
            {91, 23},
            {94, 24},
            {95, 25},
            {104, 26},
            {106, 27},
            {107, 28},
            {120, 29},
            {122, 30},
            {123, 31},
            {126, 32},
            {127, 33},
            {208, 34},
            {210, 35},
            {214, 36},
            {216, 37},
            {218, 38},
            {219, 39},
            {222, 40},
            {223, 41},
            {248, 42},
            {250, 43},
            {251, 44},
            {254, 45},
            {255, 46},
            {0, 47}
        };
    }
}