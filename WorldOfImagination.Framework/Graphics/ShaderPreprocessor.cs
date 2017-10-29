namespace WorldOfImagination.Framework.Graphics
{
    public static class ShaderPreprocessor
    {
        //TODO: Add Include statement.
        public static string Process(string shaderCode, string glslVersion)
        {
            string newCode = shaderCode.Replace("%glslVersion%", glslVersion);

            return newCode;
        }

    }
}
