using System;
using System.Collections.Generic;

namespace Hevadea.Systems.CircleMenuSystem
{
    public abstract class CircleMenuLevel
    {
        public abstract IEnumerable<CircleMenuItem> Items();
    }
}
