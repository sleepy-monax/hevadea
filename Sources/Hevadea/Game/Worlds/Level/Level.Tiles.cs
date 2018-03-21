using Hevadea.Game.Registry;
using Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;

namespace Hevadea.Game.Worlds
{
    public partial class Level
    {
        public bool IsAll(Tile tile, Rectangle rectangle)
        {
            var beginX = rectangle.X / ConstVal.TileSize - 1;
            var beginY = rectangle.Y / ConstVal.TileSize - 1;

            var endX = (rectangle.X + rectangle.Width) / ConstVal.TileSize + 1;
            var endY = (rectangle.Y + rectangle.Height) / ConstVal.TileSize + 1;

            var result = true;

            for (var x = beginX; x < endX; x++)
            for (var y = beginY; y < endY; y++)
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height) continue;
                result &= GetTile(x, y) == tile;
            }

            return result;
        }
        
        public bool IsAll<T>(Rectangle rectangle) where T: TileTag
        {
            var beginX = rectangle.X / ConstVal.TileSize;
            var beginY = rectangle.Y / ConstVal.TileSize;

            var endX = (rectangle.X + rectangle.Width) / ConstVal.TileSize;
            var endY = (rectangle.Y + rectangle.Height) / ConstVal.TileSize;

            bool result =  GetTile(beginX, beginY).HasTag<T>();;
            for (var x = beginX; x <= endX; x++)
            for (var y = beginY; y <= endY; y++)
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height) continue;
                result &= GetTile(x, y).HasTag<T>();
            }

            return result;
        }

        public Tile GetTile(TilePosition tPos)
        {
            return GetTile(tPos.X, tPos.Y);
        }

        public Tile GetTile(int tx, int ty)
        {
            if (tx < 0 || ty < 0 || tx >= Width || ty >= Height) return TILES.WATER;
            return TILES.ById[Tiles[tx + ty * Width]];
        }

        public bool SetTile(TilePosition pos, Tile tile)
        {
            return SetTile(pos.X, pos.Y, tile.Id);
        }

        public bool SetTile(int tx, int ty, Tile tile)
        {
            return SetTile(tx, ty, tile.Id);
        }

        public bool SetTile(int tx, int ty, int id, bool notify = true)
        {
            if (tx < 0 || ty < 0 || tx >= Width || ty >= Height) return false;
                        
            if (IsInitialized)
            {   
                for (var x = -1; x <= 1; x++)
                for (var y = -1; y <= 1; y++)
                {
                    var xx = tx + x;
                    var yy = ty + y;
                        if (xx >= 0 && yy >= 0 && xx < Width && yy < Height)
                            CachedTileConnection[xx, yy] = null;
                }
            }
            
            Tiles[tx + ty * Width] = id;
            
            return true;
        }

        public void ClearTileData(TilePosition tilePosition) => ClearTileData(tilePosition.X, tilePosition.Y);

        public void ClearTileData(int tx, int ty)
        {
            TilesData[tx + ty * Width].Clear();
        }

        public T GetTileData<T>(TilePosition tilePosition, string dataName, T defaultValue)
        {
            return GetTileData(tilePosition.X, tilePosition.Y, dataName, defaultValue);
        }

        public T GetTileData<T>(int tx, int ty, string dataName, T defaultValue)
        {
            if (TilesData[tx + ty * Width].ContainsKey(dataName))
            {
                var data = TilesData[tx + ty * Width][dataName];
                return (T)data;
            }

            TilesData[tx + ty * Width].Add(dataName, defaultValue);
            return defaultValue;
        }

        internal void SetTileData<T>(TilePosition tilePosition, string dataName, T value)
        {
            SetTileData(tilePosition.X, tilePosition.Y, dataName, value);
        }

        public void SetTileData<T>(int tx, int ty, string dataName, T value)
        {
            TilesData[tx + ty * Width][dataName] = value;
        }   
    }
}