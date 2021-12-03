using Hevadea.Worlds;
using System.Collections.Generic;

namespace Hevadea.Tiles.Renderers
{
    public abstract class TileRenderConnected : TileRender
    {
        public List<Tile> ConnectTo { get; set; } = new List<Tile>();
        public bool OnlyRenderOnConnection = false;
        public bool DoNotConnectToMe { get; set; } = false;

        public TileConnection GetTileConnection(Level level, Coordinates pos)
        {
            var cache = level.GetTileConnection(pos);
            if (cache != null && cache.Tile == Tile) return cache;

            var Up = false;
            var Down = false;
            var Left = false;
            var Right = false;
            var UpLeft = false;
            var UpRight = false;
            var DownLeft = false;
            var DownRight = false;

            if (ConnectTo.Count > 0)
            {
                Up |= ConnectTo.Contains(level.GetTile(pos.X, pos.Y - 1));
                Down |= ConnectTo.Contains(level.GetTile(pos.X, pos.Y + 1));
                Left |= ConnectTo.Contains(level.GetTile(pos.X - 1, pos.Y));
                Right |= ConnectTo.Contains(level.GetTile(pos.X + 1, pos.Y));
                UpLeft |= ConnectTo.Contains(level.GetTile(pos.X - 1, pos.Y - 1));
                UpRight |= ConnectTo.Contains(level.GetTile(pos.X + 1, pos.Y - 1));
                DownLeft |= ConnectTo.Contains(level.GetTile(pos.X - 1, pos.Y + 1));
                DownRight |= ConnectTo.Contains(level.GetTile(pos.X + 1, pos.Y + 1));
            }

            if (!DoNotConnectToMe || Up || Down || Left || Right || UpLeft || UpRight || DownLeft || DownRight)
            {
                Up |= level.GetTile(pos.X, pos.Y - 1) == Tile;
                Down |= level.GetTile(pos.X, pos.Y + 1) == Tile;
                Left |= level.GetTile(pos.X - 1, pos.Y) == Tile;
                Right |= level.GetTile(pos.X + 1, pos.Y) == Tile;
                UpLeft |= level.GetTile(pos.X - 1, pos.Y - 1) == Tile;
                UpRight |= level.GetTile(pos.X + 1, pos.Y - 1) == Tile;
                DownLeft |= level.GetTile(pos.X - 1, pos.Y + 1) == Tile;
                DownRight |= level.GetTile(pos.X + 1, pos.Y + 1) == Tile;
            }

            var connections = new TileConnection(Tile, Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight);
            level.SetTileConnection(pos, connections);
            return connections;
        }
    }
}