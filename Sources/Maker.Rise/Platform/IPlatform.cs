using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Rise.Platform
{
    public interface IPlatform
    {
        void Initialize();
        int GetHardwareWidth();
        int GetHardwareHeight();
    }
}
