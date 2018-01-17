using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Rise.Utils
{
    public static class SDL
    {
        private const string SDLname = "SDL2.dll";

        [DllImport(SDLname, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_MaximizeWindow")]
        public static extern void MaximizeWindow(IntPtr window);
 
        [DllImport(SDLname, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_MinimizeWindow")]
        public static extern void MinimizeWindow(IntPtr window);
 
        [DllImport(SDLname, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_RestoreWindow")]
        public static extern void RestoreWindow(IntPtr window);
    }
}
