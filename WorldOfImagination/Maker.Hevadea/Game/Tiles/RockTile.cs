using Maker.Rise.Ressource;
using Maker.Hevadea.Game.Entities;
using System;

namespace Maker.Hevadea.Game.Tiles
{
    public class RockTile : Tile
    {
        public RockTile(byte id) : base(id)
        {
            Sprite = new Sprite(Ressources.tile_tiles, 1);
        }

        public override void Hurt(Entity e, int damages, TilePosition tilePosition, Direction attackDirection)
        {
            var dmg = e.Level.GetData(tilePosition, "damages", 0) + damages;
            if (dmg > 5)
            {
                e.Level.SetTile(tilePosition.X, tilePosition.Y, Tile.Grass.ID);
            }
            else
            {
                e.Level.SetData<int>(tilePosition, "damages", dmg);
            }
            Console.WriteLine("hurt: " + dmg);
        }

        public override bool CanPass(Entity e, TilePosition pos)
        {
            return false;
        }
    }
}
