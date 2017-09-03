
namespace ProgrammingLanguage.LexicalAnalysis
{
    public enum TokenType
    {
        VARIABLE,
        NUMBER,
        STRING,

        ASSIGNMENT,
        PLUS,
        MINUS,
        MULTIPLY,
        DIVIDE,
        LEFT_PAREN,
        RIGHT_PAREN,
        EQUAL,
        NOT_EQUAL,
        GREATER_THAN,
        GREATER_THAN_OR_EQUAL,
        LESS_THAN,
        LESS_THAN_OR_EQUAL,
        AND_KEYWORD,
        OR_KEYWORD,
        NOT,
        TRUE_KEYWORD,
        FALSE_KEYWORD,
        LEFT_CURLY_BRACE,
        RIGHT_CURLY_BRACE,
        COMMA,
        MODULO,

        PRINT_KEYWORD,
        IF_KEYWORD,
        ELSE_KEYWORD,
        WHILE_KEYWORD,
        FUN_KEYWORD,
        VAR_KEYWORD,
        RETURN_KEYWORD,

        COMMENT,
        EOF,

    }
}
