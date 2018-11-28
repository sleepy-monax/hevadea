using System;
using System.Collections.Generic;

namespace Hevadea.Systems.CircleMenuSystem
{
    public class CircleMenuLevel
    {
        List<CircleMenuItem> Items { get; }

        public CircleMenuLevel()
        {
            Items = new List<CircleMenuItem>();
        }
    }
}
