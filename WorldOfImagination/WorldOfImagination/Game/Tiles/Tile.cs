using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WorldOfImagination.Game.Entities;

namespace WorldOfImagination.Game.Tiles
{
    public class Tile
    {
        public static Tile[] Tiles = new Tile[256];

        public static VoidTile Void = new VoidTile(0);
        public static GrassTile Grass = new GrassTile(1);
        public static SandTile Sand = new SandTile(2);
        public static WaterTile Water = new WaterTile(3);
        public static RockTile Rock = new RockTile(4);

        public readonly byte ID;

        public Tile(byte id)
        {
            ID = id;
            if (Tiles[id] != null) throw new Exception($"Duplicate tile ids {ID}!");
            Tiles[ID] = this;
        }

        

        public virtual void Update(Level level, TilePosition pos)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, Level level, TilePosition pos)
        {
        }

        // Properties ---------------------------------------------------------

        /* Returns if the entity can walk on it */
        public virtual bool CanPass(Level level, TilePosition pos, Entity e)
        {
            return true;
        }

        // Intercation --------------------------------------------------------

        /* What happens when you are inside the tile (ex: lava) */
        public virtual void SteppedOn(Level level, TilePosition pos, Entity entity)
        {
        }


        public static bool Colide(TilePosition tile, EntityPosition position, int width, int height)
        {
            return Colision.Check(tile.X * ConstVal.TileSize, tile.Y * ConstVal.TileSize, ConstVal.TileSize, ConstVal.TileSize,
                                  position.X, position.Y, width, height);

        }
    }
}
