
namespace ProgrammingLanguage.Lexer
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
        AND,
        OR,
        NOT,
        TRUE,
        FALSE,
        LEFT_CURLY_BRACE,
        RIGHT_CURLY_BRACE,
        NIL,
        COMMA,

        PRINT,
        IF_KEYWORD,
        ELSE_KEYWORD,
        WHILE_KEYWORD,

        COMMENT,
        EOF,

    }
}
