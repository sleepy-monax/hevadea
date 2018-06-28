using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Entities.Components.Actions;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Utils;
using Hevadea.Tiles;
using Hevadea.Tiles.Components;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems.PhysicSystem
{
    public class PhysicProcessor : GameSystem, IEntityProcessSystem
    {
        public static float AIR_FRICTION = 0.1f;
        public static float DEFAULT_FRICTION = float.PositiveInfinity;

        public PhysicProcessor()
        {
            Filter.AllOf(typeof(Physic));
        }

        public void Process(Entity entity, GameTime gameTime)
        {
            var physic = entity.GetComponent<Physic>();
            var delta = physic.Velocity * gameTime.GetDeltaTime();

            if (entity.HasComponent<Colider>(out var colider))
            {
                if (CheckColisionWithEntities(entity, colider.GetHitBox(), delta.GetXVector2()) ||
                    CheckColisionWithTiles(entity, colider.GetHitBox(), delta.GetXVector2()))
                {
                    delta = delta.GetYVector2();
                    physic.Velocity = physic.Velocity.GetYVector2() + physic.Velocity.GetXVector2() * -0.5f;
                }

                if (CheckColisionWithEntities(entity, colider.GetHitBox(), delta.GetYVector2()) ||
                    CheckColisionWithTiles(entity, colider.GetHitBox(), delta.GetYVector2()))
                {
                    delta = delta.GetXVector2();
                    physic.Velocity = physic.Velocity.GetXVector2() + physic.Velocity.GetYVector2() * -0.5f;
                }

                if (CheckColisionWithEntities(entity, colider.GetHitBox(), delta) ||
                    CheckColisionWithTiles(entity, colider.GetHitBox(), delta))
                {
                    delta = Vector2.Zero;
                    physic.Velocity = Vector2.Zero;
                }
            }

            entity.Position2D += delta;
            physic.Velocity += physic.Acceleration;
            physic.Velocity = ApplyFriction(physic.Velocity, GetFriction(entity), gameTime);

            if (physic.Velocity.Length() < 0.5f) physic.Velocity = Vector2.Zero; 

            physic.Acceleration = Vector2.Zero;

            entity.Z = Mathf.Clamp(entity.Z - gameTime.GetDeltaTime(), 0, 999);
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
            var hitbox = new RectangleF(colider.X + vec.X, colider.Y + vec.Y, colider.Width, colider.Height);
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
                        hitbox.IntersectsWith(tileHitbox))
                    {
                        coliding = true;
                    }
                }
            }

            return coliding;
        }

        public Vector2 ApplyFriction(Vector2 velocity, float friction, GameTime gameTime)
        {
            var xRatio = 1 / (1 + (gameTime.GetDeltaTime() * friction));
            return velocity * xRatio;
        }

        public float GetFriction(Entity entity)
        {
            return AIR_FRICTION + (entity.Z <= 0f ? DEFAULT_FRICTION : 0f);
        }

    }
}