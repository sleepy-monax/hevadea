using Microsoft.Xna.Framework;

namespace Hevadea.Worlds
{
    public class LevelProperties
    {
        public string Name { get; }
        public bool AffectedByDayNightCycle { get; }
        public Color AmbiantLight { get; }
        public float AmbiantTemperature { get; }

        public LevelProperties(string name, bool affectedByDayNightCycle, Color ambiantLight,
            float ambiantTemperature = 0.25f)
        {
            Name = name;
            AffectedByDayNightCycle = affectedByDayNightCycle;
            AmbiantLight = ambiantLight;
            AmbiantTemperature = ambiantTemperature;
        }
    }
}