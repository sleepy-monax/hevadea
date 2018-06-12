using Hevadea.Framework.Utils;
using Hevadea.Storage;
using Microsoft.Xna.Framework;

namespace Hevadea.GameObjects.Entities.Components.States
{
    public class Energy : EntityComponent, IEntityComponentUpdatable, IEntityComponentSaveLoad
    {
        public float Value { get; set; } = 10f;
        public float MaxValue { get; set; } = 10f;
        public float ValuePercent => Value / MaxValue;

        public bool EnableNaturalRegeneration { get; set; } = true;
        public float Regeneration { get; set; } = 0.01f;
        public float MaxRegeneration { get; set; } = 1f;

        public void OnGameSave(EntityStorage store)
        {
            store.Value(nameof(Energy), Value);
            store.Value(nameof(Energy) + nameof(Regeneration), Regeneration);
        }

        public void OnGameLoad(EntityStorage store)
        {
            Value = store.ValueOf(nameof(Energy), Value);
            Regeneration = store.ValueOf(nameof(Energy) + nameof(Regeneration), Regeneration);
        }

        public void Update(GameTime gameTime)
        {
            if (EnableNaturalRegeneration)
            {
                Value = Mathf.Min(MaxValue, Value + Regeneration);
                Regeneration = Mathf.Min(MaxRegeneration, Mathf.Max(Regeneration, 0.01f) * 1.02f);
            }
        }

        public void Restore()
        {
            Value = MaxValue;
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