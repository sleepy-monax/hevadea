using Hevadea.Entities;

namespace Hevadea.Tiles.Components
{
    public class GroundTile : TileComponent
    {
        public float MoveSpeed { get; set; } = 1f;

        public virtual void SteppedOn(Entity entity, Coordinates coords)
        {
        }
    }
}