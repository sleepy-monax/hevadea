using Hevadea.Game.Entities;
using Hevadea.Game.Registry;
using Hevadea.Game.Tiles;
using Maker.Rise;
using Maker.Rise.Ressource;

namespace Hevadea.Game.Items
{
    public class Item
    {
        private readonly string Name;
        private readonly Sprite Sprite;

        public Item(string name, Sprite sprite)
        {
            Id = ITEMS.ById.Count;
            ITEMS.ById.Add(this);

            Sprite = sprite;
            Name = name;
        }

        public int Id { get; }

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
            //tile.Interacte(user, this, pos, user.Facing);
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