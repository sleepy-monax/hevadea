using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hevadea.Modding
{
    public interface IMod
    {
        string Name { get; }
        string Author { get; }
        Version Version { get; }

        void OnRegistery();
    }
}
