using Hevadea.Entities.Components;
using Hevadea.Items;
using Hevadea.Scenes.Menus;
using Hevadea.Storage;
using Hevadea.Systems.CircleMenuSystem;
using Microsoft.Xna.Framework;

namespace Hevadea.Entities
{
    public class Player : Entity
    {
        public const float MAX_SPEED = 16f * 3f;

        public int LastLevel { get; set; }

        public Player()
        {
            LastLevel = 0;

            AddComponent(new ComponentMove());
            var health = new ComponentHealth(3) {ShowHealthBar = false, NaturalRegeneration = true};
            health.Killed += Health_Killed;
            AddComponent(health);

            AddComponent(new ComponentLightSource {IsOn = true, Color = Color.White * 0.50f, Power = 64});
            AddComponent(new RendererCreature(Resources.Sprites["entity/player"], Resources.Sprites["entity/player_lifting"]));
            AddComponent(new ComponentFlammable());
            AddComponent(new ComponentItemHolder());

            AddComponent(new ComponentAttack());
            AddComponent(new ComponentCollider(new Rectangle(-2, -2, 4, 4)));
            AddComponent(new ComponentEnergy());
            AddComponent(new ComponentInteract());
            AddComponent(new ComponentInventory(64) {AlowPickUp = true});
            AddComponent(new ComponentMove());
            AddComponent(new ComponentPickup());
            AddComponent(new ComponentPushable());
            AddComponent(new ComponentRevealMap());
            AddComponent(new ComponentCastShadow());
            AddComponent(new ComponentSwim());
            AddComponent(new ComponentPlayerBody());
            AddComponent(new CircleMenu());
            AddComponent(new ComponentExperience());
        }

        private void Health_Killed(object sender, System.EventArgs e)
        {
            GameState.CurrentMenu = new MenuPlayerRespawn(GameState);
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