using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Framework.Graphic;
using Hevadea.Tiles;
using Hevadea.Tiles.Components;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems.PhysicSystem
{
    public class PhysicRenderer : EntityDrawSystem
    {
        public PhysicRenderer()
        {
            Filter.AllOf(typeof(Physic));
        }

        public override void Draw(Entity entity, LevelSpriteBatchPool pool, GameTime gameTime)
        {
            if (Rise.Debug.GAME)
            {
                var physic = entity.GetComponent<Physic>();
                pool.Overlay.DrawLine(entity.Position2D, entity.Position2D + physic.Velocity, Color.BlueViolet);

                var entityCoords = entity.Coordinates;
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        var coords = new Coordinates(entityCoords.X + x, entityCoords.Y + y);
                        var tile = entity.Level.GetTile(coords);
                        var tileHitbox = coords.ToRectangle();

                        if (tile.HasTag<SolideTile>() &&
                            !tile.Tag<SolideTile>().CanPassThrought(entity))
                        {
                            
                            pool.Overlay.DrawRectangle(tileHitbox, Color.Yellow, 1f / entity.GameState.Camera.Zoom);
                        }
                    }
                }
            }

        }
    }
}
