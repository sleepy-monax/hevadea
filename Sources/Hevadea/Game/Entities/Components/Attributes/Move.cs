using Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities.Components.Attributes
{
    public class Move : EntityComponent, IEntityComponentUpdatable
    {
        public bool IsMoving { get; private set; }
        public bool NoClip { get; set; } = false;

        public void Update(GameTime gameTime)
        {
            IsMoving = false;
        }

        /// <summary>
        /// Move the entity relative to him With colision detection.
        /// </summary>
        public virtual bool Do(float accelerationX, float accelerationY, Direction facing)
        {
            if (Owner.Removed) return false;
            Owner.Facing = facing;
            // Handle the move speed tag on a tile.
            var curpos = Owner.GetTilePosition();
            if (Owner.Level.GetTile(curpos.X, curpos.Y).HasTag<Tags.Ground>())
            {
                var ground = Owner.Level.GetTile(curpos.X, curpos.Y).Tag<Tags.Ground>();

                accelerationX *= ground.MoveSpeed;
                accelerationY *= ground.MoveSpeed;
            }

            // Apply the movement.
            if (accelerationX == 0 && accelerationY == 0
                || !(MoveInternal(accelerationX, 0) | MoveInternal(0, accelerationY))) return false;

            // Tell the tile we walked on it.
            var pos = Owner.GetTilePosition();
            Owner.Level.GetTile(pos.X, pos.Y).Tag<Tags.Ground>()?.SteppedOn(Owner, pos);

            IsMoving = true;

            return true;
        }

        private bool MoveInternal(float aX, float aY)
        {
            var onTilePosition = Owner.GetTilePosition();

            if (Owner.X + aX + Owner.Width >= Owner.Level.Width * ConstVal.TileSize) aX = 0;
            if (Owner.Y + aY + Owner.Height >= Owner.Level.Height * ConstVal.TileSize) aY = 0;
            if (Owner.X + aX < 0) aX = 0;
            if (Owner.Y + aY < 0) aY = 0;

            for (var ox = -1; ox < 2; ox++)
            for (var oy = -1; oy < 2; oy++)
            {
                var t = new TilePosition(onTilePosition.X + ox, onTilePosition.Y + oy);
                
                bool canPass = Owner.Level.GetTile(t.X, t.Y).Tag<Tags.Solide>()?.CanPassThrought(Owner) ?? true;
                
                if (!canPass & !NoClip)
                {
                    if (t.IsColiding(Owner.X, Owner.Y + aY, Owner.Width, Owner.Height)) aY = 0;

                    if (t.IsColiding(Owner.X + aX, Owner.Y, Owner.Width, Owner.Height)) aX = 0;

                    if (t.IsColiding(Owner.X + aX, Owner.Y + aY, Owner.Width, Owner.Height))
                    {
                        aX = 0;
                        aY = 0;
                    }
                }

                foreach (var e in Owner.Level.GetEntityOnTile(t.X, t.Y))
                {
                    if (e == Owner || !e.IsBlocking(Owner) || NoClip) continue;

                    if (e.IsColliding(Owner.X + aX, Owner.Y, Owner.Width, Owner.Height))
                    {
                        e.Get<Pushable>()?.Push(Owner, Owner.Facing, 1f);
                    }
                    
                    
                    if (e.IsColliding(Owner.X, Owner.Y + aY, Owner.Width, Owner.Height))
                    {
                        e.Get<Pushable>()?.Push(Owner, Owner.Facing, 1f);
                    }
                    
                    if (e.IsColliding(Owner.X + aX, Owner.Y, Owner.Width, Owner.Height)) aX = 0f;
                    if (e.IsColliding(Owner.X, Owner.Y + aY, Owner.Width, Owner.Height)) aY = 0f;


                    if (e.IsColliding(Owner.X + aX, Owner.Y + aY, Owner.Width, Owner.Height))
                    {
                        e.Get<Pushable>()?.Push(Owner, Owner.Facing, 1f);
                        aX = 0f;
                        aY = 0f;
                    }
                }
            }

            Owner.SetPosition(Owner.X + aX, Owner.Y + aY);
            return !(aX == 0f && aY == 0f);
        }
    }
}