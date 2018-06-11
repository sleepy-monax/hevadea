using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Entities.Components.Actions;
using Hevadea.GameObjects.Entities.Components.Attributes;
using Hevadea.GameObjects.Entities.Components.Render;
using Hevadea.GameObjects.Entities.Components.States;
using Hevadea.GameObjects.Items;
using Hevadea.Storage;
using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Entities
{
    public class Player : Entity
    {
        public int LastLevel { get; set; }
        public Item HoldingItem { get; set; }

        public Player()
        {
            LastLevel = 0;
            HoldingItem = null;

            var health = new Health(20) { ShowHealthBar = false, NaturalRegeneration = true };
            health.Killed += Health_Killed;
            AddComponent(health);
            AddComponent(new Attack());
            AddComponent(new Energy());
            AddComponent(new NpcRender(new Sprite(Ressources.TileCreatures, 0, new Point(16, 32))));
            AddComponent(new Inventory(64) { AlowPickUp = true });
            AddComponent(new Interact());
            AddComponent(new LightSource { IsOn = true, Color = Color.White * 0.50f, Power = 64 });
            AddComponent(new Move());
            AddComponent(new Swim());
            AddComponent(new Pushable());
            AddComponent(new Colider(new Rectangle(-2, -2, 4, 4)));
            AddComponent(new Pickup());
            AddComponent(new Burnable(1.5f));
            AddComponent(new RevealMap(16));
        }

        private void Health_Killed(object sender, System.EventArgs e)
        {
            GameState.GetSession(this).Respawn();
        }

        public override void OnSave(EntityStorage store)
        {
            store.Value("level", Level?.Id ?? -1);
        }

        public override void OnLoad(EntityStorage store)
        {
            LastLevel = store.ValueOf("level", 0);
        }
    }
}