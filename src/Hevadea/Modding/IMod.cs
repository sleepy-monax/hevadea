using System;

namespace Hevadea.Modding
{
    public interface IMod
    {
        string Name { get; }
        string Author { get; }
        Version Version { get; }

        void Initialize();
    }
}