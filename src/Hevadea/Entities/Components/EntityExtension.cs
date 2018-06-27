using Microsoft.Xna.Framework;

namespace Hevadea.Entities.Components
{
    public static class EntityExtension
    {
        public static Entity MakeLightSource(this Entity entity, int power, Color color)
        {
            entity += new LightSource() { Power = power, Color = color };
            return entity;
        }
    }
}