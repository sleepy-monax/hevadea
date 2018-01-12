using Maker.Hevadea.Enum;
using Maker.Hevadea.Game.Tiles;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities.Component.Misc
{
    public class MoveComponent : EntityComponent, IUpdatableComponent
    {
        public bool IsMoving { get; private set; } = false;
        public bool NoClip { get; set; } = false;

        /// <summary>
        /// Move the entity relative to him.
        /// With colision detection.
        /// </summary>
        public virtual bool Move(float accelerationX, float accelerationY, Direction facing)
        {
            var oldPosition = Owner.GetTilePosition();
            Owner.Facing = facing;
            if (accelerationX != 0 || accelerationY != 0)
            {
                if (MoveInternal(accelerationX, 0) | MoveInternal(0, accelerationY))
                {
                    var pos = Owner.GetTilePosition();
                    Owner.Level.GetTile(pos.X, pos.Y).SteppedOn(Owner, pos);

                    //TODO: make this smarter
                    Owner.Level?.RemoveEntityFromTile(oldPosition, Owner);
                    Owner.Level?.AddEntityToTile(pos, Owner);

                    IsMoving = true;
                    
                    return true;
                }
            }

            return false;
        }

        private bool MoveInternal(float aX, float aY)
        {
            var onTilePosition = Owner.GetTilePosition();

            if (Owner.X + aX + Owner.Width >= Owner.Level.Width * ConstVal.TileSize) aX = 0;
            if (Owner.Y + aY + Owner.Height >= Owner.Level.Height * ConstVal.TileSize) aY = 0;
            if (Owner.X + aX < 0) aX = 0;
            if (Owner.Y + aY < 0) aY = 0;

            for (var ox = -1; ox < 2; ox++)
            {
                for (var oy = -1; oy < 2; oy++)
                {
                    var t = new TilePosition(onTilePosition.X + ox, onTilePosition.Y + oy);

                    if (Owner.Level.GetTile(t.X, t.Y).IsBlocking(Owner, t) & !NoClip)
                    {
                        if (Tile.IsColiding(t, Owner.X, Owner.Y + aY, Owner.Width, Owner.Height))
                        {
                            aY = 0;
                        }

                        if (Tile.IsColiding(t, Owner.X + aX, Owner.Y, Owner.Width, Owner.Height))
                        {
                            aX = 0;
                        }

                        if (Tile.IsColiding(t, Owner.X + aX, Owner.Y + aY, Owner.Width, Owner.Height))
                        {
                            aX = 0;
                            aY = 0;
                        }
                    }

                    foreach (var e in Owner.Level.GetEntityOnTile(t.X, t.Y))
                    {
                        if (e != Owner && e.IsBlocking(Owner) && !NoClip)
                        {
                            if (e.IsColliding(Owner.X, Owner.Y + aY, Owner.Width, Owner.Height))
                            {
                                aY = 0f;
                            }

                            if (e.IsColliding(Owner.X + aX, Owner.Y, Owner.Width, Owner.Height))
                            {
                                aX = 0f;
                            }

                            if (e.IsColliding(Owner.X + aX, Owner.Y + aY, Owner.Width, Owner.Height))
                            {
                                aX = 0f;
                                aY = 0f;
                            }
                        }
                    }
                }
            }
            
            Owner.SetPosition(Owner.X + aX, Owner.Y + aY);
            return !(aX == 0f && aY == 0f);
        }
        
        public void Update(GameTime gameTime)
        {
            IsMoving = false;
        }
    }
}
