using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Entities.Component.Ai;
using Hevadea.Game.Entities.Component.Attributes;
using Hevadea.Game.Entities.Component.Render;
using Hevadea.Game.Items;
using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities.Creatures
{
    public class PlayerEntity : Entity
    {
        public PlayerEntity()
        {
            Width = 8;
            Height = 8;
            Origin = new Point(4, 4);

            HoldingItem = null;

            Add(new Health(20){ ShowHealthBar = false });
            Add(new Attack());
            Add(new Energy());
            Add(new NpcRender(new Sprite(Ressources.TileCreatures, 0, new Point(16, 32))));
            Add(new Inventory(64) {AlowPickUp = true});
            Add(new Interact());
            Add(new Light {On = true, Color = Color.White * 0.30f, Power = 48});
            Add(new Move());
            Add(new Swim());
            Add(new Agent());
        }

        public Item HoldingItem { get; set; }

        public override bool IsBlocking(Entity entity)
        {
            return !(entity is ItemEntity);
        }

        public override void OnUpdate(GameTime gameTime)
        {
        }
    }
}