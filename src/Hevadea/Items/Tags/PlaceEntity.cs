using Hevadea.Entities.Blueprints;
using Hevadea.Tiles;
using Hevadea.Utils;
using Hevadea.Worlds;

namespace Hevadea.Items.Tags
{
    public class PlaceEntity : PlacableItemTag
    {
        private readonly EntityBlueprint _blueprint;

        public PlaceEntity(EntityBlueprint blueprint)
        {
            _blueprint = blueprint;
        }

        public override void Place(Level level, Coordinates coords, Direction facing)
        {
            var entity = _blueprint.Construct();
            entity.Facing = facing;
            level.AddEntityAt(entity, coords);
        }
    }
}