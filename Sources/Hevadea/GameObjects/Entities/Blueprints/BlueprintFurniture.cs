using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.GameObjects.Entities.Components;
using Hevadea.GameObjects.Entities.Components.Actions;
using Hevadea.GameObjects.Entities.Components.Attributes;

namespace Hevadea.GameObjects.Entities.Blueprints
{
    public class BlueprintFurniture : EntityBlueprint
    {
        private readonly Sprite _sprite;
        
        public BlueprintFurniture(string name, Sprite sprite) : base(name)
        {
            _sprite = sprite;
        }

        public override void AttachComponents(Entity e)
        {
            e.AddComponent(new Move());
            e.AddComponent(new Pushable());
            e.AddComponent(new Pickupable(_sprite));
        }
    }
}
