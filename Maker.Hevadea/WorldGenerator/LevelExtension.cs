using System;
using Maker.Hevadea.Game;
using Maker.Hevadea.Game.Tiles;

namespace Maker.Hevadea.WorldGenerator
{
    public static class LevelExtension
    {
        private static void Swap<T>(ref T lhs, ref T rhs) { T temp; temp = lhs; lhs = rhs; rhs = temp; }

        public static void Rectangle(this Level level, int x, int y, int w, int h, Tile tile)
        {
            level.PlotLine(x, y, x, y + h, tile);
            level.PlotLine(x, y, x + w, y, tile);
            level.PlotLine(x, y + h, x + w, y + h, tile);
            level.PlotLine(x + w, y, x + w, y + h, tile);
        }
        
        public static void FillRectangle(this Level level, int x, int y, int w, int h, Tile tile)
        {
            for (int offx = 0; offx < w; offx++)
            {
                for (int offy = 0; offy < h; offy++)
                {
                    level.SetTile(x + offx, y + offy, tile);
                }                
            }
        }

        public static void PlotLine(this Level level, int x0, int y0, int x1, int y1, Tile tile)
        {
            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep) { Swap(ref x0, ref y0); Swap(ref x1, ref y1); }
            if (x0 > x1) { Swap(ref x0, ref x1); Swap(ref y0, ref y1); }

            int dX = x1 - x0;
            int dY = Math.Abs(y1 - y0);
            int err = (dX / 2);
            int ystep = (y0 < y1 ? 1 : -1), y = y0;
 
            for (int x = x0; x <= x1; ++x)
            {
                if (!(steep ? level.SetTile(y, x, tile) : level.SetTile(x, y, tile))) return;
                err = err - dY;
                if (err < 0) { y += ystep;  err += dX; }
            }
        }
    }
}