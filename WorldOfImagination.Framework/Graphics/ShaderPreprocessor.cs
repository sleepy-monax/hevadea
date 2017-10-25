namespace WorldOfImagination.Framework.Graphics
{
    public static class ShaderPreprocessor
    {

        public static string Process(string shaderCode, string[] defs)
        {
            string newCode = "";
            string tempBuffer = "";
            bool useTemBuffer = false;

            foreach (var c in shaderCode)
            {
                if (c == '#')
                {
                    useTemBuffer = true;
                }

                if (useTemBuffer)
                {
                    tempBuffer += c;
                }
                else
                {
                    newCode += c;
                }

                if (tempBuffer == "#ifdef")
                {

                }
                else if (tempBuffer == "#else")
                {

                }
                else if (tempBuffer == "#endif")
                {

                }
            }

            return newCode;
        }

    }
}
