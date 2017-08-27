using System;
using ProgrammingLanguage.LexicalAnalysis;
using System.Collections.Generic;

//grammar based on https://github.com/fsacer/FailLang by fsacer but simplified

//program      → declaration* EOF;
//declaration  → funDecl | varDecl | statement ;                                                        NOT IMPLEMENTED
//funDecl      → "fonk" IDENTIFIER functionBody;                                                        NOT IMPLEMENTED
//varDecl      → "değişken" IDENTIFIER( "=" expression )? ";" ;
//statement    → exprStmt | ifStmt | printStmt | returnStmt | whileStmt | block;                        NOT IMPLEMENTED
//exprStmt     → expression ";" ;                                                                       
//ifStmt       → "eğer" "(" expression ")" statement( "değilse" statement )? ;                          
//printStmt    → "yazdır" expression ";" ;                                                              
//whileStmt    → "oldukça" "(" expression ")" statement ;                                               
//block        → "{" declaration* "}" ;                                                                


//expression  → IDENTIFIER (( "=" ) expression )? | logic_or ;
//logic_or    → logic_and ( "veya" logic_and )*
//logic_and   → equality ( "ve" equality )*
//equality    → comparison (( "!=" | "==" ) comparison )*
//comparison  → term (( ">" | ">=" | "<" | "<=" ) term )*
//term        → factor(( "-" | "+" ) factor )*                              
//factor      → unary(( "/" | "*" ) unary )*
//unary       → ( "!" | "-" ) unary | primary | call  ;
//call        → primary( "(" arguments? ")" )* ;                                                        NOT IMPLEMENTED
//primary     → NUMBER | STRING | "doğru" | "yanlış" | "boşdeğer" | IDENTIFIER | "(" expression ")"


//arguments    → expression( "," expression )* ;                                                        NOT IMPLEMENTED
//parameters   → IDENTIFIER( "," IDENTIFIER )* ;                                                        NOT IMPLEMENTED
//functionBody → "(" parameters? ")" block ;                                                            NOT IMPLEMENTED

namespace ProgrammingLanguage.SyntaxAnalysis
{
    internal class Parser
    {
        //###################################################################################
        #region Fields

        private List<Token> m_TokenList;
        private Token m_CurrentToken;
        private int m_TokenIndex;

        #endregion

        //###################################################################################
        #region Constructor

        internal Parser(List<Token> tokenList)
        {
            #region Preconditions
            if(tokenList == null)
            {
                throw new ArgumentNullException(nameof(tokenList));
            }
            #endregion

            m_TokenList = tokenList;
            m_TokenIndex = 0;
            m_CurrentToken = m_TokenList[m_TokenIndex];
        }

        #endregion

        //###################################################################################
        #region Internal Methods
        
        internal void ParseProgram()
        {
            while(!Match(TokenType.EOF))
            {
                ParseDecleration();
            }
        }

        #endregion

        //###################################################################################
        #region Private Methods

        #region Helper Methods

        private void Eat(TokenType tokenType)
        {
            if (tokenType == m_CurrentToken.TokenType)
            {
                AdvanceToken();
            }
            else
            {
                throw new ParseException($"Parse exception while consuming token: {tokenType}");
            }
        }

        private void AdvanceToken()
        {
            m_TokenIndex += 1;
            m_CurrentToken = m_TokenList[m_TokenIndex];
        }

        private Token NextToken()
        {
            return m_TokenList[m_TokenIndex + 1];
        }

        private bool Match(TokenType tokenType)
        {
            if(tokenType == m_CurrentToken.TokenType)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Statement Parse Methods

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
            Eat(TokenType.VAR_KEYWORD);

            Eat(TokenType.VARIABLE);

            if (Match(TokenType.ASSIGNMENT))
            {
                AdvanceToken();
                ParseExpression();
            }
        }

        private void ParseStatement()
        {

        }

        private void ParsePrintStatement()
        {
            if(Match(TokenType.PRINT_KEYWORD))
            {
                Eat(TokenType.PRINT_KEYWORD);

                ParseExpression();
            }
        }

        private void ParseWhileStatement()
        {
            if (Match(TokenType.WHILE_KEYWORD))
            {
                Eat(TokenType.WHILE_KEYWORD);

                Eat(TokenType.LEFT_PAREN);
                ParseExpression();
                Eat(TokenType.RIGHT_PAREN);
                ParseStatement();
            }
        }

        private void ParseBlock()
        {
            if (Match(TokenType.LEFT_CURLY_BRACE))
            {
                Eat(TokenType.LEFT_CURLY_BRACE);
                ParseDecleration();
                Eat(TokenType.RIGHT_CURLY_BRACE);
            }
        }

        private void ParseExprStatement()
        {
            ParseExpression();
        }

        private void ParseIfStatement()
        {
            if (Match(TokenType.IF_KEYWORD))
            {
                Eat(TokenType.IF_KEYWORD);
                Eat(TokenType.LEFT_PAREN);
                ParseExpression();
                Eat(TokenType.RIGHT_PAREN);

                ParseStatement();
                if (Match(TokenType.ELSE_KEYWORD))
                {
                    Eat(TokenType.ELSE_KEYWORD);
                    ParseStatement();
                }

            }

        }

        #endregion

        #region Expression Parse Methods

        private void ParseExpression()
        {
            if(Match(TokenType.VARIABLE))
            {
                Eat(TokenType.VARIABLE);

                if(Match(TokenType.ASSIGNMENT))
                {
                    Eat(TokenType.ASSIGNMENT);

                    ParseExpression();
                }

            }
            else
            {
                ParseLogicOr();
            }
        }

        private void ParseLogicOr()
        {
            ParseLogicAnd();

            while (Match(TokenType.OR_KEYWORD))
            {
                Eat(TokenType.OR_KEYWORD);

                ParseLogicAnd();
            }

        }

        private void ParseLogicAnd()
        {

            Equality();

            while (Match(TokenType.AND_KEYWORD))
            {
                Eat(TokenType.AND_KEYWORD);

                Equality();
            }

        }

        private void Equality()
        {
            ParseComparison();

            while (Match(TokenType.EQUAL) || Match(TokenType.NOT_EQUAL))
            {
                if(Match(TokenType.EQUAL))
                {
                    Eat(TokenType.EQUAL);
                }
                else if(Match(TokenType.NOT_EQUAL))
                {
                    Eat(TokenType.NOT_EQUAL);
                }

                ParseComparison();
            }
        }
        
        private void ParseComparison()
        {
            ParseTerm();

            while (Match(TokenType.GREATER_THAN) || Match(TokenType.GREATER_THAN_OR_EQUAL)
                || Match(TokenType.LESS_THAN) || Match(TokenType.LESS_THAN_OR_EQUAL))
            {
                if (Match(TokenType.GREATER_THAN))
                {
                    Eat(TokenType.GREATER_THAN);
                }
                else if (Match(TokenType.GREATER_THAN_OR_EQUAL))
                {
                    Eat(TokenType.GREATER_THAN_OR_EQUAL);
                }
                else if (Match(TokenType.LESS_THAN))
                {
                    Eat(TokenType.LESS_THAN);
                }
                else if (Match(TokenType.LESS_THAN_OR_EQUAL))
                {
                    Eat(TokenType.LESS_THAN_OR_EQUAL);
                }

                ParseTerm();
            }

        }

        private void ParseTerm()
        {
            ParseFactor();

            while (Match(TokenType.MINUS) || Match(TokenType.PLUS))
            {
                if (Match(TokenType.MINUS))
                {
                    Eat(TokenType.MINUS);
                }
                else if (Match(TokenType.PLUS))
                {
                    Eat(TokenType.PLUS);
                }

                ParseFactor();
            }

        }

        private void ParseFactor()
        {
            ParseUnary();

            while (Match(TokenType.DIVIDE) || Match(TokenType.MULTIPLY))
            {
                if (Match(TokenType.DIVIDE))
                {
                    Eat(TokenType.DIVIDE);
                }
                else if (Match(TokenType.MULTIPLY))
                {
                    Eat(TokenType.MULTIPLY);
                }

                ParseUnary();
            }

        }

        private void ParseUnary()
        {
            if (Match(TokenType.NOT) || Match(TokenType.MINUS))
            {

                ParseUnary();
            }
            else if(Match(TokenType.NUMBER) || Match(TokenType.STRING) || Match(TokenType.TRUE_KEYWORD) ||
                Match(TokenType.FALSE_KEYWORD) || Match(TokenType.NIL) || Match(TokenType.VARIABLE) ||
                Match(TokenType.LEFT_PAREN))
            {
                ParsePrimary();
            }
            //else if()
            //{
            //    ParseCall();
            //}
        }

        private void ParsePrimary()
        {
            if(Match(TokenType.NUMBER))
            {
                Eat(TokenType.NUMBER);
            }
            else if (Match(TokenType.STRING))
            {
                Eat(TokenType.STRING);
            }
            else if (Match(TokenType.TRUE_KEYWORD))
            {
                Eat(TokenType.TRUE_KEYWORD);
            }
            else if (Match(TokenType.FALSE_KEYWORD))
            {
                Eat(TokenType.FALSE_KEYWORD);
            }
            else if (Match(TokenType.NIL))
            {
                Eat(TokenType.NIL);
            }
            else if (Match(TokenType.VARIABLE))
            {
                Eat(TokenType.VARIABLE);
            }
            else if (Match(TokenType.LEFT_PAREN))
            {
                Eat(TokenType.LEFT_PAREN);
                ParseExpression();
                Eat(TokenType.RIGHT_PAREN);
            }

        }

        private void ParseCall()
        {

        }


        #endregion

        #endregion
    }
}
