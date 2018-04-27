using System.Collections.Generic;
using Hevadea.GameObjects.Entities;
using Hevadea.GameObjects.Tiles;

namespace Hevadea.Worlds
{
    public class Chunk
    {
        public Tile[] Tiles;
        public List<Entity> Entities;
    }
}