using Maker.Rise;
using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.Hevadea.Game.Entities
{
    public class Player : Mob
    {
        public int CurrentLevel = 0;

        public Player()
        {
            Width = 8;
            Height = 8;
            Sprite = new Sprite(Ressources.tile_creatures, 0, new Point(16,16));
            IsLightSource = true;
            LightColor = Color.Orange * 0.50f;
            LightLevel = 72;
        }

        public override void Update(GameTime gameTime)
        {
            var moveX = 0;
            var moveY = 0;
            if (Engine.Input.KeyDown(Keys.Q)) { Facing = Direction.Left; moveX = -1; }
            if (Engine.Input.KeyDown(Keys.D)) { Facing = Direction.Right; moveX = 1; }
            if (Engine.Input.KeyDown(Keys.Z)) { Facing = Direction.Up; moveY = -1; }
            if (Engine.Input.KeyDown(Keys.S)) {Facing = Direction.Down; moveY = 1; }

            Move(moveX, moveY);

            if (Engine.Input.MouseLeftClick) { Level.AddEntity(new TorchEntity { Position = new EntityPosition(this.Position.X, this.Position.Y) }); }
        }

        
    }
}
