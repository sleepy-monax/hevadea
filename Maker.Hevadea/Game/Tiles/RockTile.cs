using Maker.Hevadea.Enum;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Registry;
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