using Hevadea.Framework;
using Hevadea.Game;
using Hevadea.Game.Storage;
using Hevadea.Game.Worlds;
using Hevadea.GameObjects.Entities.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.GameObjects.Entities
{
    public partial class Entity
    {
        internal void Initialize(Level level, World world, GameManager game)
        {
            Level = level;
            World = world;
            Game = game;

            if (Ueid == -1 && world != null) {
                Ueid = world.GetUeid();
            }
        }
        
        public EntityStorage Save()
        {
            var store = new EntityStorage(Blueprint.Name)
            {
                Type = Blueprint.Name,
                Ueid = Ueid
            };

            store.Set("X", X);
            store.Set("Y", Y);
            store.Set("Facing", (int) Facing);

            foreach (var c in GetComponnents())
                if (c is IEntityComponentSaveLoad s)
                    s.OnGameSave(store);
            OnSave(store);

            return store;
        }

        public void Load(EntityStorage store)
        {
            Ueid = store.Ueid;
            X    = store.GetFloat("X", X);
            Y    = store.GetFloat("Y", Y);

            Facing = (Direction)(int)store.Get("Facing", (int) Facing);

            foreach (var c in GetComponnents())
                if (c is IEntityComponentSaveLoad s)
                    s.OnGameLoad(store);
            
            OnLoad(store);
        }

        public void Remove()
        {
            Level.RemoveEntity(this);
        }
    }
}