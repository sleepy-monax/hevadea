using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
