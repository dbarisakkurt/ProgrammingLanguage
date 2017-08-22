using System;
using ProgrammingLanguage.LexicalAnalysis;

//grammar based on https://github.com/fsacer/FailLang by fsacer but simplified

//program      → declaration* EOF;
//declaration  → funDecl | varDecl | statement ;
//funDecl      → "fonk" IDENTIFIER functionBody;
//varDecl      → "değişken" IDENTIFIER( "=" expression )? ";" ;
//statement    → exprStmt | ifStmt | printStmt | returnStmt | whileStmt | block;
//exprStmt     → expression ";" ;
//ifStmt       → "eğer" "(" expression ")" statement( "değilse" statement )? ;
//printStmt    → "yazdır" expression ";" ;
//whileStmt    → "oldukça" "(" expression ")" statement ;
//block        → "{" declaration* "}" ;


//expression  → assignment ;
//assignment  → identifier(( "=" ) assignment )? | logic_or ;
//logic_or    → logic_and( "veya" logic_and )*
//logic_and   → equality( "ve" equality )*
//equality    → comparison(( "!=" | "==" ) comparison )*
//comparison  → term(( ">" | ">=" | "<" | "<=" ) term )*
//term        → factor(( "-" | "+" ) factor )*
//factor      → unary(( "/" | "*" ) unary )*
//unary       → ( "!" | "-" ) unary | primary | call  ;
//call        → primary( "(" arguments? ")" )* ;
//primary     → NUMBER | STRING | "doğru" | "yanlış" | "boşdeğer" | IDENTIFIER | "(" expression ")"


//arguments    → expression( "," expression )* ;
//parameters   → IDENTIFIER( "," IDENTIFIER )* ;
//functionBody → "(" parameters? ")" block ;

namespace ProgrammingLanguage.SyntaxAnalysis
{
    internal class Parser
    {
        //###################################################################################
        #region Fields

        private Lexer m_Lexer;

        #endregion

        //###################################################################################
        #region Constructor

        internal Parser(Lexer lexer)
        {
            #region Preconditions
            if(lexer == null)
            {
                throw new ArgumentNullException(nameof(lexer));
            }      
            #endregion

            m_Lexer = lexer;
        }

        #endregion

        //###################################################################################
        #region Internal Methods

        internal void Parse()
        {
            while(Match(TokenType.EOF))
            {
                ParseDecleration();
            }
        }

        #endregion

        //###################################################################################
        #region Private Methods

        private bool Match(TokenType tokenType)
        {
            return false;
        }

        private void ParseDecleration()
        {
            if(Match(TokenType.FUN_KEYWORD))
            {
                ParseFunctionDecleration();
            }
            else if(Match(TokenType.VAR_KEYWORD))
            {
                ParseVariableDecleration();
            }
            else
            {
                ParseStatement();
            }
        }

        private void ParseFunctionDecleration()
        {

        }

        private void ParseVariableDecleration()
        {

        }

        private void ParseStatement()
        {

        }

        #endregion
    }
}
