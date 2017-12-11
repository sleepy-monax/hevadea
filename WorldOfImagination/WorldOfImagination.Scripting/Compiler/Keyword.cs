namespace WorldOfImagination.Scripting.Compiler
{
    public enum TokenType
    {
        BRACEOPEN, BRACECLOSE,     // {, }
        BRACKETOPEN, BRACKETCLOSE, // (, )
        QUOTE,
        
        // math operator
        MATHADD, MATHSUM, MATHMULT, MATHDIV, MATHMOD, MATHEXP, 
        MATHEQUAL,  MATHENOTQUAL,  MATHEBIGGERTHAN,  MATHSMALLERTHAN,
        
        // bool operator
        BOOLAND, BOOLOR, BOOLNOT,
        
        // Keywords
        kwFUNCTION, kwRETURN, kwEND, kwIF, kwELSE, kwELSEIF, kwFOR, kwWHILE, kwDO, kwTRUE, kwFALSE,

        // Value
        valINT, valBOOL, valSTRING, 
        
        // Misc
        ARROW, FUNCTIONNAME, VARNAME, RAW, LINEEND
    }
}