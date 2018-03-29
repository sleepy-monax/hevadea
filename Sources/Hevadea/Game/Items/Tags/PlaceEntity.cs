using Hevadea.Game.Registry;
using Hevadea.Game.Tiles;
using Hevadea.Game.Worlds;
using Hevadea.Utils;

namespace Hevadea.Game.Items.Tags
{
    public class PlaceEntity : PlacableItemTag
    {
        private readonly EntityBlueprint _blueprint;

        public PlaceEntity(EntityBlueprint blueprint)
        {
            _blueprint = blueprint;
        }

        public override void Place(Level level, TilePosition tile, Direction facing)
        {
            var entity = _blueprint.Construct();
            entity.Facing = facing;
            level.SpawnEntity(entity, tile.X, tile.Y);
        }
    }
}