using System;
using Hevadea.Entities;
using Hevadea.Framework.Graphic.SpriteAtlas;

namespace Hevadea.Systems.CircleMenuSystem
{
    public class CircleMenuItem
    {
        public Sprite Icon { get; }
        public Action<Entity> Action { get; }
        public CircleMenuLevel Level { get; }

        public CircleMenuItem()
        {
        }
    }
}
