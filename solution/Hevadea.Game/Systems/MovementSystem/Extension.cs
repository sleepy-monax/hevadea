using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Tiles.Components;
using Hevadea.Worlds;

namespace Hevadea.Systems.MovementSystem
{
    public static class Extension
    {
        public static void Move(this Entity e, float sx, float sy)
        {
            if (e.Removed) return;

            // Get move speed on this ground.
            var ground = e.TileOver.Tag<GroundTile>();
            if (ground != null)
            {
                sx *= ground.MoveSpeed;
                sy *= ground.MoveSpeed;
            }

            // Alias the level
            var l = e.Level;

            // Check for the world boundary
            if (e.X + sx >= l.Width * Game.Unit) sx = 0;
            if (e.Y + sy >= l.Height * Game.Unit) sy = 0;

            if (e.X + sx < 0) sx = 0;
            if (e.Y + sy < 0) sy = 0;

            // Alias the colider
            var c = e.GetComponent<ComponentCollider>();

            if (c != null)
            {
                // Alias the hitbox
                var hb = c.GetHitBox();

                var hbFuturX = hb.Offset(sx, 0);
                var hbFuturY = hb.Offset(0, sy);
                var hbFutur = hb.Offset(sx, sy);
            }
        }

        private static void CheckColision(Entity e, Level l, RectangleF hb, ref float ox, ref float oy)
        {
            var h = hb.Offset(ox, oy);

            // Check colision with entities
            foreach (var ee in l.QueryEntity(h))
            {
            }

            // Check colision with tiles
        }

        public static void MoveTo(this Entity e, Coordinates coords, float speed)
        {
        }

        public static void MoveTo(this Entity e, float x, float y)
        {
        }

        public static bool IsMoving(this Entity e)
        {
            return false;
        }
    }
}