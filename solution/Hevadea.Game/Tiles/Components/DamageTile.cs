using Hevadea.Framework.Graphic;
using Hevadea.Registry;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Hevadea.Tiles.Components
{
    public class DamageTile : TileComponent, IDrawableTileComponent
    {
        public Tile ReplacementTile { get; set; } = TILES.VOID;
        public float MaxDamages { get; set; } = 5f;
        public static Sprite[] damageSprites;

        public DamageTile()
        {
            if (damageSprites == null)
                damageSprites = new Sprite[]
                {
                    new Sprite(Resources.TileIcons, new Point(0, 1)),
                    new Sprite(Resources.TileIcons, new Point(1, 1)),
                    new Sprite(Resources.TileIcons, new Point(2, 1)),
                    new Sprite(Resources.TileIcons, new Point(3, 1))
                };
        }

        public void Draw(Tile tile, SpriteBatch spriteBatch, Coordinates position, Dictionary<string, object> data,
            Level level, GameTime gameTime)
        {
            var damages = level.GetTileData(position, "damages", 0f);

            if (damages > 0f)
            {
                var damageStage = (int) (damages / MaxDamages * 4);
                damageSprites[damageStage].Draw(spriteBatch, position.ToRectangle(), Color.White);
            }
        }

        public void Hurt(float damages, Coordinates position, Level level)
        {
            var dmg = level.GetTileData(position, "damages", 0f) + damages;
            if (dmg > MaxDamages)
            {
                level.SetTile(position, ReplacementTile);
                level.ClearTileDataAt(position);
                AttachedTile.Tag<DroppableTile>()?.Drop(position, level);
            }
            else
            {
                level.SetTileData(position, "damages", dmg);
            }
        }
    }
}