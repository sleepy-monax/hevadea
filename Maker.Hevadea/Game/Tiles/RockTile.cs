using Maker.Hevadea.Enums;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Registry;
using Maker.Rise;
using Maker.Rise.Ressource;

namespace Maker.Hevadea.Game.Tiles
{
    public class RockTile : Tile
    {
        public RockTile(byte id) : base(id)
        {
            Sprite = new Sprite(Ressources.tile_tiles, 1);
        }

        public override void Hurt(Entity e, float damages, TilePosition tilePosition, Direction attackDirection)
        {
            var dmg = e.Level.GetTileData(tilePosition, "damages", 0f) + damages;
            if (dmg > 5)
            {
                e.Level.SetTile(tilePosition.X, tilePosition.Y, TILES.DIRT);
                ITEMS.Stone.Drop(e.Level, tilePosition, Engine.Random.Next(1, 4));
                ITEMS.Coal.Drop(e.Level, tilePosition, Engine.Random.Next(0, 3));
            }
            else
            {
                e.Level.SetTileData(tilePosition, "damages", dmg);
            }
        }

        public override bool IsBlocking(Entity e, TilePosition pos)
        {
            return true;
        }
    }
}