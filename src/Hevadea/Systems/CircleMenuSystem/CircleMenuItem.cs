using System;
using Hevadea.Entities;
using Hevadea.Framework.Graphic.SpriteAtlas;

namespace Hevadea.Systems.CircleMenuSystem
{
    public class CircleMenuItem
    {
        public Sprite Icon { get; }
        public Action<Entity> Action { get; }
        public CircleMenuLevel Child { get; }
        public CircleMenuLevel Parent { get; }

        public CircleMenuItem(Sprite icon, Action<Entity> action)
        {
            Icon = icon ?? throw new ArgumentNullException(nameof(icon));
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public CircleMenuItem(Sprite icon, CircleMenuLevel child)
        {
            Icon = icon;
            Child = child;
        }
    }
}
