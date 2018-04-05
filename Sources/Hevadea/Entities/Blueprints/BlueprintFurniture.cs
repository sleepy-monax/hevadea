using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hevadea.Entities.Components;
using Hevadea.Framework.Graphic.SpriteAtlas;

namespace Hevadea.Entities.Blueprints
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
