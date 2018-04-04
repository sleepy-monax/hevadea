using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Entities
{
    public abstract class EntityBlueprint
    {
        public string Name { get; }

        public EntityBlueprint(string name) { Name = name; }

        public virtual Entity Construct()
        {
            var entity = new Entity();
            entity.Blueprint = this;

            AttachComponents(entity);

            return entity;
        }

        public abstract void AttachComponents(Entity e);
    }
}
