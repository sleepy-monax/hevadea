using Hevadea.GameObjects.Entities.Blueprints;
using Hevadea.GameObjects.Tiles;
using Hevadea.Utils;
using Hevadea.Worlds;

namespace Hevadea.GameObjects.Items.Tags
{
    public class PlaceEntity : PlacableItemTag
    {
        private readonly EntityBlueprint _blueprint;

        public PlaceEntity(EntityBlueprint blueprint)
        {
            _blueprint = blueprint;
        }

        public override void Place(Level level, Coordinates tile, Direction facing)
        {
            var entity = _blueprint.Construct();
            entity.Facing = facing;
            level.AddEntityAt(entity, tile.X, tile.Y);
        }
    }
}