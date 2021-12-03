using Hevadea.Entities;
using Hevadea.Entities.Components;
using Hevadea.Framework;
using Hevadea.Tiles;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems.MinimapSystem
{
    public class RevealProcessor : EntityUpdateSystem
    {
        public const int REVEAL_RANGE = 16;

        public RevealProcessor()
        {
            Filter.AllOf(typeof(ComponentRevealMap));
        }

        public override void Update(Entity entity, GameTime gameTime)
        {
            var minimap = entity.Level.Minimap;

            var coordToReveal =
                entity.Coordinates +
                new Coordinates(
                    Rise.Rnd.Next(-REVEAL_RANGE, REVEAL_RANGE),
                    Rise.Rnd.Next(-REVEAL_RANGE, REVEAL_RANGE)
                );

            if (entity.Coordinates.Distance(coordToReveal) <= REVEAL_RANGE) minimap.Reveal(coordToReveal);
        }
    }
}