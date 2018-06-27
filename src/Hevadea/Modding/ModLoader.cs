using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hevadea.Modding
{
    //public class ModCompilationResult
    //{
    //    public bool Success { get; }
    //    public string OriginalFile { get; }
    //    public Assembly Assembly { get; }
    //    public CompilerErrorCollection Errors { get; }

    //    public ModCompilationResult(bool success, string originalFile, Assembly assembly, CompilerErrorCollection errors)
    //    {
    //        Success = success;
    //        OriginalFile = originalFile;
    //        Assembly = assembly;
    //        Errors = errors;
    //    }
    //}

    //public class ModLoader
    //{
    //    public static void LoadFrom(string path)
    //    {
    //    }

    //    public static List<IMod> LoadFromFile(string path)
    //    {
    //        Assembly assembly = Assembly.Load(AssemblyName.GetAssemblyName(path));
    //        return LoadFromAssembly(assembly);
    //    }

    //    public static List<IMod> LoadFromAssembly(Assembly assembly)
    //    {
    //        if (assembly != null)
    //        {
    //            var types = assembly.GetTypes();
    //            var pluginTypes = types.Where(t => !t.IsInterface && !t.IsAbstract &&
    //                                               t.GetInterface(typeof(IMod).FullName) != null).ToList();

    //            return pluginTypes.Select(t => (IMod)Activator.CreateInstance(t)).ToList();
    //        }

    //        return null;
    //    }
    //}
}