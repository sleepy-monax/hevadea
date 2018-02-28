using Maker.Rise.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLPlatform
{
    public class RiseOpenGLPlatform : IPlatform
    {
        public int GetHardwareHeight()
        {
            return 720;
        }

        public int GetHardwareWidth()
        {
            return 1280;
        }

        public void Initialize()
        {
            // do nothing
        }
    }
}
