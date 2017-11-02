using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WorldOfImagination.Framework.Scripting
{
    internal class ScriptLoaderDefault
        : ScriptLoader
    {
        #region Public Methods

        public List<String> LoadScript(String strResourceName)
        {
            try
            {
                List<String> listStatements = new List<string>();

                StreamReader streamReader = new StreamReader(strResourceName);
                while (!streamReader.EndOfStream)
                {
                    String strStatement = streamReader.ReadLine();
                    listStatements.Add(strStatement);
                }
                streamReader.Close();

                return listStatements;
            }
            catch (Exception exception)
            {
                throw new ScriptException("Error while reading script file", exception);
            }
        }

        #endregion
    }
}
