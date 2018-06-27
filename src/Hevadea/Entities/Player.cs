using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.Render;
using Hevadea.Entities.Components.States;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Items;
using Hevadea.Scenes.Menus;
using Hevadea.Storage;
using Microsoft.Xna.Framework;

namespace Hevadea.Entities
{
    public class Player : Entity
    {
        public int LastLevel { get; set; }
        public Item HoldingItem { get; set; }

        public Player()
        {
            LastLevel = 0;
            HoldingItem = null;

            var health = new Health(3) { ShowHealthBar = false, NaturalRegeneration = true };
            health.Killed += Health_Killed;
            AddComponent(health);

            AddComponent(new Attack());
            AddComponent(new Burnable(1.5f));
            AddComponent(new Colider(new Rectangle(-2, -2, 4, 4)));
            AddComponent(new Energy());
            AddComponent(new Interact());
            AddComponent(new Inventory(64) { AlowPickUp = true });
            AddComponent(new LightSource { IsOn = true, Color = Color.White * 0.50f, Power = 64 });
            AddComponent(new Move());
            AddComponent(new NpcRender(new Sprite(Ressources.TileCreatures, 0, new Point(16, 32))));
            AddComponent(new Pickup());
            AddComponent(new Pushable());
            AddComponent(new RevealMap(16));
            AddComponent(new Shadow());
            AddComponent(new Swim());
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