using Maker.Hevadea.Enum;
using Maker.Hevadea.Game.Registry;
using Maker.Rise;

namespace Maker.Hevadea.Game.Tiles
{
    public class DirtTile : Tile
    {
        public DirtTile(byte id) : base(id)
        {
        }

        public override void Update(Level level, int tx, int ty)
        {
            if (Engine.Random.Next(10) == 5)
            {
                var d = (Direction)Engine.Random.Next(0, 4);
                var p = d.ToPoint(); 

                if (level.GetTile(tx + p.X, ty + p.Y) is GrassTile)
                {
                    level.SetTile(tx, ty, TILES.GRASS);
                }
            }
        }
    }
}
