using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Entities.Components.Attributes
{
    public class Physic : EntityComponent
    {
        public float Mass { get; set; } = 1f;
        public Vector2 Velocity { get; set; } = new Vector2();
        public Vector2 Acceleration { get; set; } = new Vector2();
    }
}
