using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.Renderer;
using Hevadea.Entities.Components.States;
using Hevadea.Items;
using Hevadea.Scenes.Menus;
using Hevadea.Storage;
using Microsoft.Xna.Framework;

namespace Hevadea.Entities
{
    public class Player : Entity
    {
        public const float MAX_SPEED = 16f * 3f;

        public int LastLevel { get; set; }
        public Item HoldingItem { get; set; }

        public Player()
        {
            LastLevel = 0;
            HoldingItem = null;

            AddComponent(new Move());
            var health = new Health(3) { ShowHealthBar = false, NaturalRegeneration = true };
            health.Killed += Health_Killed;
            AddComponent(health);

            AddComponent(new LightSource { IsOn = true, Color = Color.White * 0.50f, Power = 64 });
            AddComponent(new MobRenderer(Ressources.ImgPlayer));
            AddComponent(new Flammable());

            AddComponent(new Attack());
            AddComponent(new Colider(new Rectangle(-2, -2, 4, 4)));
            AddComponent(new Energy());
            AddComponent(new Interact());
            AddComponent(new Inventory(64) { AlowPickUp = true });
            AddComponent(new Move());
            AddComponent(new Pickup());
            AddComponent(new Pushable());
            AddComponent(new RevealMap());
            AddComponent(new ShadowCaster());
            AddComponent(new Swim());
            AddComponent(new PlayerBody());
        }

        private void Health_Killed(object sender, System.EventArgs e)
        {
            GameState.CurrentMenu = new MenuRespawn(this, GameState);
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