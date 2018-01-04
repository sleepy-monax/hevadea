using Maker.Hevadea.Game.Items;
using Maker.Hevadea.Game.Tiles;
using Maker.Hevadea.Scenes;
using Maker.Rise;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Maker.Hevadea.Game.Entities
{
    public class Player : Mob
    {
        private GameScene Game;

        public int CurrentLevel = 0;
        public Item HoldingItem = new Item();
        public Inventory Inventory;


        public Player()
        {
            Width = 8;
            Height = 8;
            Sprite = new Sprite(Ressources.tile_creatures, 0, new Point(16,16));
            Light.On = true;
            Light.Color = Color.Orange * 0.50f;
            Light.Power = 72;

            Health = MaxHealth = 20;
            IsInvincible = false;
            Inventory = new Inventory();
        }

        public void Initialize(GameScene game, Level level, World world)
        {
            Game = game;
            Initialize(level, world);
        }

        public override bool Pickup(Item item)
        {
            return Inventory.AddItem(item);
        }        
    }
}
