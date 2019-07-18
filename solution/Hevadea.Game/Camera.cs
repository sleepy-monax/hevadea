using Hevadea.Entities;
using Hevadea.Tiles;
using Microsoft.Xna.Framework;

namespace Hevadea
{
    public class Camera : Framework.Graphic.Camera
    {
        public Entity FocusEntity { get; set; }
        public Coordinates Coordinates => new Coordinates((int) (X / Game.Unit), (int) (Y / Game.Unit));

        public Camera(Entity focusEntity)
        {
            FocusEntity = focusEntity;
            Zoom = 6f;
        }

        public void JumpToFocusEntity()
        {
            X = FocusEntity.X;
            Y = FocusEntity.Y;
        }

        public override void Animate(GameTime gameTime)
        {
            X += (FocusEntity.X - X) * (float) gameTime.ElapsedGameTime.TotalSeconds * Zoom;
            Y += (FocusEntity.Y - Y) * (float) gameTime.ElapsedGameTime.TotalSeconds * Zoom;

            //X = FocusEntity.X;
            //Y = FocusEntity.Y;
        }
    }
}