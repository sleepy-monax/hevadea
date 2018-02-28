using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Framework.Graphic.OpenGL
{
    public abstract class GlObject
    {
        public int Handle { get; private set; }
        public GlObject(int handle) { Handle = handle; }
    }
}
