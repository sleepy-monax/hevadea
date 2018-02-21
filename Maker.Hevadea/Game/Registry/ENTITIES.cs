using System.Collections.Generic;
using Maker.Hevadea.Game.Entities;
using Maker.Hevadea.Game.Entities.Creatures;

namespace Maker.Hevadea.Game.Registry
{
    public class GenericEntityBlueprint<T> : EntityBlueprint where T : Entity, new()
    {
        public GenericEntityBlueprint(string name) : base(name)
        {
        }
        
        public override Entity CreateEntity()
        {
            return new T();
        }
    }
    
    public class EntityBlueprint
    {
        public string Name { get; }

        public EntityBlueprint(string name)
        {
            Name = name;
        }
        
        public virtual Entity CreateEntity()
        {
            return new Entity();
        }
    }
    
    public static class ENTITIES
    {
        public static void Initialize()
        {
            RegisterEntityBlueprint(new GenericEntityBlueprint<PlayerEntity>("player")); 
        }
        
        private static Dictionary<string, EntityBlueprint> _library;

        public static void RegisterEntityBlueprint(EntityBlueprint blueprint)
        {
            if (_library.ContainsKey(blueprint.Name))
            {
                _library[blueprint.Name] = blueprint;
            }
            else
            {
                _library.Add(blueprint.Name, blueprint);
            }
        }

        public static EntityBlueprint GetBlueprint(string name)
        {
            return _library.ContainsKey(name) ? _library[name] : null;
        }
    }
}