using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Entities.Components.Render;
using Hevadea.GameObjects.Items;
using Hevadea.Storage;
using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Entities.Blueprints.Legacy
{
    public class EntityPlayer : Entity
    {
        public int LastLevel { get; set; } = 0;
        
        public EntityPlayer()
        {
            HoldingItem = null;

            AddComponent(new Health(20){ ShowHealthBar = false, NaturalRegeneration = true });
            AddComponent(new Attack());
            AddComponent(new Energy());
            AddComponent(new NpcRender(new Sprite(Ressources.TileCreatures, 0, new Point(16, 32))));
            AddComponent(new Inventory(64) {AlowPickUp = true});
            AddComponent(new Interact());
            AddComponent(new Light {On = true, Color = Color.White * 0.50f, Power = 64});
            AddComponent(new Move());
            AddComponent(new Swim());
            AddComponent(new Pushable());
            AddComponent(new Colider(new Rectangle(-2, -2, 4, 4)));
            AddComponent(new Pickup());
            AddComponent(new Burnable(1.5f));
            AddComponent(new RevealMap());
        }

        public Item HoldingItem { get; set; }

        public override void OnSave(EntityStorage store)
        {
            store.Set("level", Level?.Id ?? -1);
        }

        public override void OnLoad(EntityStorage store)
        {
            LastLevel = store.GetInt("level", 0);
        }
    }
}