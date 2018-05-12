using Hevadea.GameObjects.Tiles;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hevadea.WorldGenerator
{
    public static class LevelExtension
    {
        private static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp; temp = lhs; lhs = rhs; rhs = temp;
        }

        public static bool IsValid(this Level level, int x, int y, int w, int h, bool needToBeEmpty, List<Tile> AcceptedTiles)
        {
            bool result = level.IsEmpty(x, y, w, h) || !needToBeEmpty;

            for (var offx = 0; offx < w; offx++)
                for (var offy = 0; offy < h; offy++)
                {
                    result &= AcceptedTiles.Contains(level.GetTile(x + offx, y + offy));
                }

            return result;
        }

        public static bool IsEmpty(this Level level, int x, int y, int w, int h)
        {
            var result = true;
            for (var offx = 0; offx < w; offx++)
                for (var offy = 0; offy < h; offy++)
                {
                    result &= !level.GetEntitiesAt(x + offx, y + offy).Any();
                }
            return result;
        }

        public static void ClearEntitiesAt(this Level level, int x, int y, int w, int h)
        {
            for (var offx = 0; offx < w; offx++)
                for (var offy = 0; offy < h; offy++)
                {
                    var entities = level.GetEntitiesAt(x + offx, y + offy);
                    foreach (var e in entities) { level.RemoveEntity(e); }
                }
        }

        public static void Rectangle(this Level level, int x, int y, int w, int h, Tile tile)
        {
            level.PlotLine(x, y, x, y + h - 1, tile);
            level.PlotLine(x, y, x + w - 1, y, tile);
            level.PlotLine(x, y + h - 1, x + w - 1, y + h - 1, tile);
            level.PlotLine(x + w - 1, y, x + w - 1, y + h - 1, tile);
        }

        public static void FillRectangle(this Level level, Rectangle rect, Tile tile)
        {
            level.FillRectangle(rect.X, rect.Y, rect.Width, rect.Height, tile);
        }

        public static void FillRectangle(this Level level, int x, int y, int w, int h, Tile tile)
        {
            for (var offx = 0; offx < w; offx++)
                for (var offy = 0; offy < h; offy++)
                {
                    level.SetTile(x + offx, y + offy, tile);
                }
        }

        public static void PlotLine(this Level level, int x0, int y0, int x1, int y1, Tile tile)
        {
            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep) { Swap(ref x0, ref y0); Swap(ref x1, ref y1); }
            if (x0 > x1) { Swap(ref x0, ref x1); Swap(ref y0, ref y1); }

            int dX = x1 - x0;
            int dY = Math.Abs(y1 - y0);
            int err = dX / 2;
            int ystep = y0 < y1 ? 1 : -1;
            int y = y0;

            for (int x = x0; x <= x1; ++x)
            {
                if (!(steep ? level.SetTile(y, x, tile) : level.SetTile(x, y, tile))) return;
                err = err - dY;
                if (err < 0) { y += ystep; err += dX; }
            }
        }
    }
}