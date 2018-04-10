using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Registry;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            {
                damageSprites = new Sprite[]
                {
                        new Sprite(Ressources.TileIcons, new Point(0,1)),
                        new Sprite(Ressources.TileIcons, new Point(1,1)),
                        new Sprite(Ressources.TileIcons, new Point(2,1)),
                        new Sprite(Ressources.TileIcons, new Point(3,1))
                };
            }

        }

        public void Draw(Tile tile, SpriteBatch spriteBatch, TilePosition position, Dictionary<string, object> data, Level level, GameTime gameTime)
        {
            var damages = level.GetTileData(position, "damages", 0f);

            if (damages > 0f)
            {
                int damageStage = (int)((damages / MaxDamages) * 4);
                damageSprites[damageStage].Draw(spriteBatch, position.ToRectangle(), Color.White);
            }
        }

        public void Hurt(float damages, TilePosition position, Level level)
        {
            var dmg = level.GetTileData(position, "damages", 0f) + damages;
            if (dmg > MaxDamages)
            {
                level.SetTile(position, ReplacementTile);
                level.ClearTileData(position);
                AttachedTile.Tag<DroppableTile>()?.Drop(position, level);
            }
            else
            {
                level.SetTileData(position, "damages", dmg);
            }
        }

    }
}
