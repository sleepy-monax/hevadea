using System;
using Maker.Hevadea.Game.Storage;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game.Entities.Component
{
    public class Energy : EntityComponent, IUpdatableComponent, ISaveLoadComponent
    {

        public float Value { get; set; }           = 10f;
        public float MaxValue { get; set; }        = 10f;
        public float Regeneration { get; set; }    = 0.01f;
        public float MaxRegeneration { get; set; } = 1f;

        public void OnGameSave(EntityStorage store)
        {
            store.Set(nameof(Value), Value);
            store.Set(nameof(Regeneration), Regeneration);
        }

        public void OnGameLoad(EntityStorage store)
        {
            Value = store.Get(nameof(Value), Value);
            Regeneration = store.Get(nameof(Regeneration), Regeneration);
        }

        public bool Reduce(float value)
        {
            if (Value >= value)
            {
                Value -= value;
                Regeneration = 0.01f;
                return true;
            }
            
            return false;
        }

        public void Update(GameTime gameTime)
        {
            Value = Math.Min(MaxValue, Value + Regeneration);
            Regeneration = Math.Min(MaxRegeneration, Regeneration * 1.02f);
        }
    }
}
