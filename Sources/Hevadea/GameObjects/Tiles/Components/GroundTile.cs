using Hevadea.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Tiles.Components
{
    public class GroundTile : TileComponent
    {
        public float MoveSpeed { get; set; } = 1f;
        public virtual void SteppedOn(Entity entity, TilePosition position) { }
    }
}
