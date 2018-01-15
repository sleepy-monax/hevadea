using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component.Interaction;
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

        public Item(byte id)
        {
            Id = id;
            if (ITEMS.ById[id] != null) throw new Exception($"Duplicate item ID: {Id}!");
            ITEMS.ById[Id] = this;
        }

        public virtual string GetName()
        {
            return "Item #" + Id;
        }
        public virtual Sprite GetSprite()
        {
            return new Sprite(Ressources.tile_items, 0);
        }

        public virtual float GetAttackBonus(Entity target)
        {
            return 1f;
        }
        public virtual float GetAttackBonus(Tile target)
        {
            return 1f;
        }

        public void Attack(Entity user, Entity target, float baseDamages)
        {
            if (target.Components.Has<HealthComponent>())
            {
                target.Components.Get<HealthComponent>().Hurt(user, baseDamages * GetAttackBonus(target), user.Facing);
            }
        }
        public void Attack(Entity user, TilePosition target, float baseDamages)
        {
            var tile = user.Level.GetTile(target);
            tile.Hurt(user, baseDamages * GetAttackBonus(tile), target, user.Facing);
        }

        public void InteracteOn(Entity user, TilePosition pos)
        {
            var tile = user.Level.GetTile(pos);
            tile.Interacte(user, this, pos, user.Facing);
        }
        public void Drop(Level level, float x, float y, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                var dropWood = new ItemEntity(this, Engine.Random.Next(-50,50) / 10f, Engine.Random.Next(-50, 50) / 10f);
                level.AddEntity(dropWood);
                dropWood.SetPosition(x, y);
            }
        }
    }
}