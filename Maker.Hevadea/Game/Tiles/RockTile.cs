using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Registry;
using Maker.Hevadea.Game.Tiles.Render;
using Maker.Rise;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Tiles
{
    public class RockTile : Tile
    {
        private ConnectedTileRender render;
        public RockTile(byte id) : base(id)
        {
            Sprite = new Sprite(Ressources.TileTiles, 1);
            render = new ConnectedTileRender(Ressources.TileRock);
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

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, Level level, TilePosition pos)
        {
            render.Draw(spriteBatch, new Vector2(pos.WorldX, pos.WorldY), new TileConection(this, pos, level));
        }

        public override bool IsBlocking(Entity e, TilePosition pos)
        {
            return true;
        }
    }
}