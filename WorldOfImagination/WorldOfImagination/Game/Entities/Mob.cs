using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfImagination.Game.Entities
{
    public enum Facing
    {
        Up = 0, Right = 1, Down = 2, Left = 3
    }

    public class Mob : Entity
    {
        public Facing Facing = Facing.Down;
    }
}
