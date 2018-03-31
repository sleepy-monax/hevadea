using Microsoft.Xna.Framework;

namespace Hevadea.Worlds
{
    public class LevelProperties
    {
        public string Name { get; }
        public bool AffectedByDayNightCycle { get; }
        public Color AmbiantLight { get; }
        
        public LevelProperties(string name, bool affectedByDayNightCycle, Color ambiantLight)
        {
            Name = name;
            AffectedByDayNightCycle = affectedByDayNightCycle;
            AmbiantLight = ambiantLight;
        }        
    }
}