using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Component.Interaction;
using Maker.Hevadea.Game.Registry;
using Maker.Hevadea.Game.Tiles;
using Maker.Rise.Ressource;
using System;
using Maker.Rise;

namespace Maker.Hevadea.Game.Items
{
    public class Item
    {
        public int Id { get; }
        public string Name { get; }

        public Item(byte id, string name)
        {
            Id = id;
            Name = name;
            if (ITEMS.ById[id] != null) throw new Exception($"Duplicate item ID: {Id}!");
            ITEMS.ById[Id] = this;
        }

        public virtual float GetAttackBonus(Entity target)
        {
            return 1f;
        }

        public virtual float GetAttackBonus(Tile target)
        {
            return 1f;
        }

        public virtual void Attack(Entity user, Entity target, int baseDamages)
        {
            if (target.HasComponent<HealthComponent>())
            {
                target.GetComponent<HealthComponent>().Hurt(user, (int) (baseDamages * GetAttackBonus(target)), user.Facing);
            }
        }

        public virtual void Attack(Entity user, TilePosition target, int baseDamages)
        {
            var tile = user.Level.GetTile(target);
            tile.Hurt(user, (int) (baseDamages * GetAttackBonus(tile)), target, user.Facing);
        }

        public virtual void InteracteOn(Entity user, TilePosition pos)
        {
            var tile = user.Level.GetTile(pos);
            tile.Interacte(user, this, pos, user.Facing);
        }

        public virtual Sprite GetSprite()
        {
            return new Sprite(Ressources.tile_items, 0);
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