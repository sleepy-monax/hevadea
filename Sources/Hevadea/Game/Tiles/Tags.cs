using Hevadea.Framework;
using Hevadea.Game.Entities;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Items;
using Hevadea.Game.Registry;
using Hevadea.Game.Worlds;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Hevadea.Game.Tiles
{
    public static class Tags
    {
        #region interaction

        /// <summary>
        /// Allow the tile to be break by entities.
        /// </summary>
        public class Breakable : TileTag
        {
            public Tile ReplacementTile { get; set; } = TILES.VOID;

            public void Break(TilePosition position, Level level)
            {
                level.SetTile(position, ReplacementTile);
                level.ClearTileData(position);
                AttachedTile.Tag<Droppable>()?.Drop(position, level);
            }
        }

        /// <summary>
        /// Allow the tile to get damages from entities.
        /// </summary>
        public class Damage : TileTag
        {
            public Tile ReplacementTile { get; set; } = TILES.VOID;
            public float MaxDamages { get; set; } = 5f;

            public void Hurt(float damages, TilePosition position, Level level)
            {
                var dmg = level.GetTileData(position, "damages", 0f) + damages;
                if (dmg > MaxDamages)
                {
                    level.SetTile(position, ReplacementTile);
                    level.ClearTileData(position);
                    AttachedTile.Tag<Droppable>()?.Drop(position, level);
                }
                else
                {
                    level.SetTileData(position, "damages", dmg);
                }
            }

        }

        /// <summary>
        /// Allow entities to interacte width the tile.
        /// </summary>
        public class Interactable : TileTag
        {
        }

        /// <summary>
        /// Alow the tile to loot item wen damaged or break.
        /// </summary>
        public class Droppable : TileTag
        {
            public List<Drop> Items { get; set; } = new List<Drop>();

            public Droppable() { }
            public Droppable(params Drop[] items)
            {
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }

            public void Drop(TilePosition position, Level level)
            {
                foreach (var d in Items)
                    if (Rise.Random.NextDouble() < d.Chance) d.Item.Drop(level, position, Rise.Random.Next(d.Min, d.Max + 1));
            }
        }
        #endregion

        #region Physic

        /// <summary>
        /// Make the tile solide, entity connot pass througt.
        /// </summary>
        public class Solide : TileTag
        {
            public virtual bool CanPassThrought(Entity entity)
            {
                return false;
            }
        }

        /// <summary>
        /// Only Entity with the Swim component can pass throught.
        /// </summary>
        public class Liquide : Solide
        {
            public override bool CanPassThrought(Entity entity)
            {
                return entity.Has<Swim>();
            }
        }
        
        /// <summary>
        /// Allow to set te movement speed on the tile.
        /// </summary>
        public class Ground : TileTag
        {
            public float MoveSpeed { get; set; } = 1f;
            public virtual void SteppedOn(Entity entity, TilePosition position) { }
        }
        #endregion

        #region Behavior

        /// <summary>
        /// The tile spread.
        /// ex: Grass, Water
        /// </summary>
        public class Spread : TileTag, IUpdatableTag
        {

            public List<Tile> SpreadTo { get; set; } = new List<Tile>();
            public int SpreadChance { get; set; } = 10;

            public void Update(Tile tile, TilePosition position, Dictionary<string, object> data, Level level, GameTime gameTime)
            {
                if (Rise.Random.Next(SpreadChance) == 0)
                {
                    var d = (Direction)Rise.Random.Next(0, 4);
                    var p = d.ToPoint();

                    if (SpreadTo.Contains(level.GetTile(position.X + p.X, position.Y + p.Y))) level.SetTile(position.X + p.X, position.Y + p.Y, AttachedTile);
                }
            }
        }
        #endregion
    }
}
