using System.Collections.Generic;
using Hevadea.Game.Entities;
using Hevadea.Game.Registry;
using Hevadea.Game.Tiles;
using Hevadea.Game.Worlds;
using Maker.Rise;
using Maker.Rise.Ressource;

namespace Hevadea.Game.Items
{
    public class Item
    {
        public int Id { get; }
        private readonly string _name;
        private readonly Sprite _sprite;
        private List<ItemTag> _tags;
        
        public Item(string name, Sprite sprite)
        {
            Id = ITEMS.ById.Count;
            ITEMS.ById.Add(this);

            _sprite = sprite;
            _name = name;
            _tags = new List<ItemTag>();
        }

        public virtual string GetName()
        {
            return _name;
        }

        public virtual Sprite GetSprite()
        {
            return _sprite;
        }

        public bool HasTag<T>() where T : ItemTag
        {
            foreach (var t in _tags)
            {
                if (t is T) return true;
            }

            return false;
        }

        public T Tag<T>() where T : ItemTag
        {
            foreach (var t in _tags)
            {
                if (t is T variable) return variable;
            }

            return null;
        }

        public void AddTag(ItemTag tag) {tag.AttachedItem = this; _tags.Add(tag); }
        public void AddTags(params ItemTag[] tags) { foreach (var t in tags) AddTag(t); }
        
        public virtual float GetAttackBonus(Entity target)
        {
            return 1f;
        }

        public virtual float GetAttackBonus(Tile target)
        {
            return 1f;
        }

        public void Drop(Level level, float x, float y, int quantity)
        {
            for (var i = 0; i < quantity; i++)
            {
                var dropItem = new ItemEntity(this, Engine.Random.Next(-50, 50) / 10f,
                    Engine.Random.Next(-50, 50) / 10f);
                level.AddEntity(dropItem);
                dropItem.SetPosition(x, y);
            }
        }

        public void Drop(Level level, TilePosition tilePosition, int quantity)
        {
            Drop(level, tilePosition.X * ConstVal.TileSize + ConstVal.TileSize / 2,
                tilePosition.Y * ConstVal.TileSize + ConstVal.TileSize / 2, quantity);
        }
    }
}