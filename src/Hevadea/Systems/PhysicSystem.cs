using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Utils;
using Hevadea.Tiles;
using Hevadea.Tiles.Components;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems
{
    public class PhysicSystem : GameSystem, IEntityProcessSystem
    {
        public const float AIR_FRICTION = 0.5f;

        public PhysicSystem()
        {
            Filter.AllOf(typeof(Physic), typeof(Move));
        }

        public void Process(Entity entity, GameTime gameTime)
        {
            var physic = entity.GetComponent<Physic>();

            physic.Velocity += physic.Acceleration + (GetFriction(entity, physic) * gameTime.GetDeltaTime());
            physic.Acceleration = Vector2.Zero;

            var delta = physic.Velocity * gameTime.GetDeltaTime();

            if (entity.HasComponent<Colider>(out var colider))
            {
                if (CheckColisionWithEntities(entity, colider.GetHitBox(), delta.GetXVector2()) ||
                    CheckColisionWithTiles(entity, colider.GetHitBox(), delta.GetXVector2()))
                {
                    delta = delta.GetYVector2();
                    physic.Velocity = physic.Velocity.GetYVector2();
                }

                if (CheckColisionWithEntities(entity, colider.GetHitBox(), delta.GetYVector2()) ||
                    CheckColisionWithTiles(entity, colider.GetHitBox(), delta.GetYVector2()))
                {
                    delta = delta.GetXVector2();
                    physic.Velocity = physic.Velocity.GetXVector2();
                }

                if (CheckColisionWithEntities(entity, colider.GetHitBox(), delta) ||
                    CheckColisionWithTiles(entity, colider.GetHitBox(), delta))
                {
                    delta = Vector2.Zero;
                    physic.Velocity = Vector2.Zero;
                }
            }

            entity.Position += delta;
        }

        public bool CheckColisionWithEntities(Entity entity, RectangleF colider, Vector2 vec)
        {
            var hitbox = new RectangleF(colider.X + vec.X, colider.Y + vec.Y, colider.Width, colider.Height);
            var colidingEntities = entity.Level.GetEntitiesOnArea(hitbox);
            var coliding = false;

            foreach (var e in colidingEntities)
            {
                if (e != entity &&
                    e.HasComponent<Colider>(out var eColider) &&
                    eColider.CanCollideWith(entity) &&
                    hitbox.IntersectsWith(eColider.GetHitBox()))
                {
                    coliding = true;
                }
            }

            return coliding;
        }

        public bool CheckColisionWithTiles(Entity entity, RectangleF colider, Vector2 vec)
        {
            var coliding = false;
            var entityCoords = entity.Coordinates;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    var coords = new Coordinates(entityCoords.X + x, entityCoords.Y + y);
                    var tile = entity.Level.GetTile(coords);
                    var tileHitbox = coords.ToRectangle();

                    if (tile.HasTag<SolideTile>() &&
                        !tile.Tag<SolideTile>().CanPassThrought(entity) &&
                        colider.IntersectsWith(tileHitbox))
                    {
                        coliding = true;
                    }
                }
            }

            return coliding;
        }

        public Vector2 GetFriction(Entity entity, Physic physic)
        {
            return (physic.Velocity * AIR_FRICTION) - physic.Velocity;
        }
    }
}