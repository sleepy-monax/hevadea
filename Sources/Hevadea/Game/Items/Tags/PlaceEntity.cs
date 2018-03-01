using Hevadea.Game.Registry;
using Hevadea.Game.Tiles;
using Hevadea.Game.Worlds;

namespace Hevadea.Game.Items.Tags
{
    public class PlaceEntity : PlacableItemTag
    {
        private readonly EntityBlueprint _blueprint;

        public PlaceEntity(EntityBlueprint blueprint)
        {
            _blueprint = blueprint;
        }

        public override void Place(Level level, TilePosition tile)
        {
            level.SpawnEntity(_blueprint.Build(), tile.X, tile.Y);
        }
    }
}