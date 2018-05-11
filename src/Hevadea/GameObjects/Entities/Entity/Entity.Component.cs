using System.Collections.Generic;
using System.Linq;
using Hevadea.GameObjects.Entities.Components;

namespace Hevadea.GameObjects.Entities
{
    public partial class Entity
    {
        public List<EntityComponent> Componenents { get; set; } = new List<EntityComponent>();

        public T AddComponent<T>(T component) where T : EntityComponent
        {
            if (Componenents.Any(e => e == component)) return null;

            Componenents.Add(component);
            component.Owner = this;

            return component;
        }

        public T GetComponent<T>() where T : EntityComponent
        {
            for (int i = 0; i < Componenents.Count; i++)
            {
                if (Componenents[i] is T component)
                {
                    return component;
                }
            }

            return null;
        }

        public bool HasComponent<T>() where T : EntityComponent
        {
            return Componenents.OfType<T>().Any();
        }
    }
}