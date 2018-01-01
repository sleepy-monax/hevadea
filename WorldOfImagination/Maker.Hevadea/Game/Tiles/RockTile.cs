using Maker.Rise.Ressource;
using Maker.Hevadea.Game.Entities;
using System;
using Maker.Hevadea.Game.Entities.Particles;
using Microsoft.Xna.Framework;

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
            var dmg = e.Level.GetTileData(tilePosition, "damages", 0) + damages;
            if (dmg > 5)
            {
                e.Level.SetTile(tilePosition.X, tilePosition.Y, Tile.Grass.ID);
            }
            else
            {
                e.Level.SetTileData<int>(tilePosition, "damages", dmg);
            }
            Console.WriteLine("hurt: " + dmg);
        }

        public override bool IsBlocking(Entity e, TilePosition pos)
        {
            return true;
        }
    }
}
