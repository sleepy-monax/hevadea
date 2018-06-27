using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Linq;

namespace Hevadea.Modding
{
    //public static class ModBuilder
    //{
    //    private static string[] Assemblies = null;

    //    public static ModCompilationResult BuildFromSource(string source, string fileName = "none")
    //    {
    //        var provider = new CSharpCodeProvider();

    //        if (Assemblies == null)
    //        {
    //            Assemblies = AppDomain.CurrentDomain
    //                        .GetAssemblies()
    //                        .Where(a => !a.IsDynamic)
    //                        .Select(a => a.Location).ToArray();
    //        }

    //        var parameters = new CompilerParameters();
    //        parameters.GenerateInMemory = true;
    //        parameters.ReferencedAssemblies.AddRange(Assemblies);

    //        var result = provider.CompileAssemblyFromSource(parameters, source);

    //        return new ModCompilationResult(result.Errors.Count == 0, fileName, result.CompiledAssembly, result.Errors);
    //    }
    //}
}