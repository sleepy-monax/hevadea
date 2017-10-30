using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfImagination.Framework
{
    public abstract class Game
    {
        public Host Host;
        public abstract void OnLoad();
        public abstract void OnExit();
        public abstract void OnUpdate(float deltaTime);
        public abstract void OnDraw();
    }


}
