using System;
using System.Collections.Generic;
using Hevadea.Framework.Utils;
using Hevadea.GameObjects.Tiles;

namespace Hevadea.Worlds
{
    public class Level
    {
		public string Name;
		Dictionary<Pair<int, int>, Chunk> _chunks;

        public Level()
        {
			_chunks = new Dictionary<Pair<int, int>, Chunk>();
        }

		public Dictionary<string, object> SetTileData(int tx, int ty, Dictionary<string, object> data) { throw new NotImplementedException(); }
		public Dictionary<string, object> GetTileData(int tx, int ty) { throw new NotImplementedException(); }
		public void SetTile(int tx, int ty, Tile tile) { throw new NotImplementedException(); }
		public Tile GetTile(int tx, int ty) { throw new NotImplementedException(); }
    }
}
