using System.Collections.Generic;
using Maker.Utils;

namespace Maker.Hevadea.Game.Tiles.Render
{
    public class TileConection
    {
        public static Dictionary<int, int> conv = new Dictionary<int, int>
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

        public bool Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight;

        public TileConection()
        {
        }

        public TileConection(Tile tile, TilePosition pos, Level level)
        {
            Up = level.GetTile(pos.X, pos.Y - 1) == tile;
            Down = level.GetTile(pos.X, pos.Y + 1) == tile;
            Left = level.GetTile(pos.X - 1, pos.Y) == tile;
            Right = level.GetTile(pos.X + 1, pos.Y) == tile;
            UpLeft = level.GetTile(pos.X - 1, pos.Y - 1) == tile;
            UpRight = level.GetTile(pos.X + 1, pos.Y - 1) == tile;
            DownLeft = level.GetTile(pos.X - 1, pos.Y + 1) == tile;
            DownRight = level.GetTile(pos.X + 1, pos.Y + 1) == tile;
        }

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

        public TileConection(int flag)
        {
            UpLeft = flag.ReadBit(0);
            Up = flag.ReadBit(1);
            UpRight = flag.ReadBit(2);
            Left = flag.ReadBit(3);
            Right = flag.ReadBit(4);
            DownLeft = flag.ReadBit(5);
            Down = flag.ReadBit(6);
            DownRight = flag.ReadBit(7);
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
                return (byte) conv[v];
            return v;
        }
    }
}