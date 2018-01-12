using Maker.Hevadea.Enum;
using Maker.Hevadea.Game.Registry;
using Maker.Rise.Ressource;
using System;

namespace Maker.Hevadea.Game.Tiles
{
    public class GrassTile : Tile
    {
        Random rnd = new Random();

        public GrassTile(byte id) : base(id)
        {
            Sprite = new Sprite(Ressources.tile_tiles, 2);
        }

        public override void Update(Level level, int tx, int ty)
        {

            base.Update(level, tx, ty);
            var direction = (Direction)(rnd.Next(0, 4));
            var p = direction.ToPoint();

            if (level.GetTile(tx + p.X, ty + p.Y) is DirtTile && rnd.Next(10) == 5)
            {
                level.SetTile(tx + p.X, ty + p.Y, TILES.GRASS);
            }
        }
    }
}