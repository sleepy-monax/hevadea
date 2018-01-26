using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Registry;
using Maker.Hevadea.Game.Tiles;
using Maker.Rise;
using Maker.Rise.Ressource;
using System;

namespace Maker.Hevadea.Game.Items
{
    public class Item
    {
        public int Id { get; }
        private readonly Sprite Sprite;
        private readonly string Name;

        public Item(byte id, string name, Sprite sprite)
        {
            if (ITEMS.ById[id] != null) throw new Exception($"Duplicate item ID: {Id}!");
            ITEMS.ById[id] = this;

            Id = id;
            Sprite = sprite;
            Name = name;
        }

        public virtual string GetName()
        {
            return Name;
        }
        public virtual Sprite GetSprite()
        {
            return Sprite;
        }

        public virtual float GetAttackBonus(Entity target)
        {
            return 1f;
        }
        public virtual float GetAttackBonus(Tile target)
        {
            return 1f;
        }


        public virtual void InteracteOn(Entity user, TilePosition pos)
        {
            var tile = user.Level.GetTile(pos);
            tile.Interacte(user, this, pos, user.Facing);
        }

        public void Drop(Level level, float x, float y, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                var dropItem = new ItemEntity(this, Engine.Random.Next(-50,50) / 10f, Engine.Random.Next(-50, 50) / 10f);
                level.AddEntity(dropItem);
                dropItem.SetPosition(x, y);
            }
        }
    }
}