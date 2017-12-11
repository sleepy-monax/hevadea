namespace WorldOfImagination.Scripting.Compiler
{
    public enum TokenType
    {
        BRACEOPEN, BRACECLOSE,     // {, }
        BRACKETOPEN, BRACKETCLOSE, // (, )
        QUOTE,
        
        // math operator
        MATHADD, MATHSUM,
        MATHMULT, MATHDIV,
        MATHMOD,
        
        MATHEQUAL,
        MATHENOTQUAL,
        
        BIGGER_OR_EQUAL,
        SMALLER_OR_EQUAL,
        BIGGER_THAN,
        SMALLER_THAN,
        
        // bool operator
        AND, OR, NOT,
        
        // Keywords
        FUNCTION,
        RETURN, 
        END, 
        IF, 
        ELSE, 
        ELSEIF, 
        FOR, 
        WHILE,
        DO,
        TRUE,
        FALSE,

        // Value
        INT, 
        BOOL,
        STRING, 
        
        // Misc
        ARROW, 
        FUNCTIONNAME,
        FUNCTIONARGNAME,
        VARNAME,
        RAW, // Raw is for not yet identified token
        LINEEND,
        COMMA,
    }
}