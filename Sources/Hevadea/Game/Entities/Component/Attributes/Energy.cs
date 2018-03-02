using Hevadea.Framework.Utils;
using Hevadea.Game.Storage;
using Microsoft.Xna.Framework;

namespace Hevadea.Game.Entities.Component.Attributes
{
    public class Energy : EntityComponent, IUpdatableComponent, ISaveLoadComponent
    {
        public float Value { get; set; } = 10f;
        public float MaxValue { get; set; } = 10f;
        public float ValuePercent => Value / MaxValue;

        public bool EnableNaturalRegeneration { get; set; } = true;
        public float Regeneration { get; set; } = 0.01f;
        public float MaxRegeneration { get; set; } = 1f;

        public void OnGameSave(EntityStorage store)
        {
            store.Set(nameof(Energy), Value);
            store.Set(nameof(Energy) + nameof(Regeneration), Regeneration);
        }

        public void OnGameLoad(EntityStorage store)
        {
            Value = store.GetFloat(nameof(Energy), Value);
            Regeneration = store.GetFloat(nameof(Energy) + nameof(Regeneration), Regeneration);
        }

        public void Update(GameTime gameTime)
        {
            if (EnableNaturalRegeneration)
            {   
                Value = Mathf.Min(MaxValue, Value + Regeneration);
                Regeneration = Mathf.Min(MaxRegeneration, Mathf.Max(Regeneration, 0.01f) * 1.02f);
            }
        }

        public bool Reduce(float value)
        {
            if (Value >= value)
            {
                Value -= value;
                Regeneration = 0.01f;
                return true;
            }

            return Value >= value;
        }
    }
}