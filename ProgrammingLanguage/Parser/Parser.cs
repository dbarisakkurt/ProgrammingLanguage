using System;
using ProgrammingLanguage.LexicalAnalysis;
using System.Collections.Generic;

//grammar based on https://github.com/fsacer/FailLang by fsacer but simplified

//program      → declaration* EOF;
//declaration  → funDecl | varDecl | statement ;                                                        
//funDecl      → "fonk" IDENTIFIER functionBody;                                                        
//varDecl      → "değişken" IDENTIFIER( "=" expression )? ";" ;
//statement    → exprStmt | ifStmt | printStmt | returnStmt | whileStmt | block;                        
//exprStmt     → assignment ";" ;                                                                       
//ifStmt       → "eğer" "(" expression ")" statement( "değilse" statement )? ;                          
//printStmt    → "yazdır" expression ";" ;                                                              
//whileStmt    → "oldukça" "(" expression ")" statement ;                                               
//block        → "{" declaration* "}" ;              
//return       → "dön" expression ";"

//assignment  → IDENTIFIER (( "=" ) assignment )? | expression                      
//expression  → logic_or ;                                                          
//logic_or    → logic_and ( "veya" logic_and )*
//logic_and   → equality ( "ve" equality )*
//equality    → comparison (( "!=" | "==" ) comparison )*
//comparison  → term (( ">" | ">=" | "<" | "<=" ) term )*
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

        private void ParseAssignment()
        {
            if(Match(TokenType.VARIABLE))
            {
                Eat(TokenType.VARIABLE);

                if(Match(TokenType.ASSIGNMENT))
                {
                    Eat(TokenType.ASSIGNMENT);
                    ParseAssignment();
                }
            }
            else
            {
                ParseExpression();
            }
        }

        private void ParseDecleration()
        {
            if(Match(TokenType.FUN_KEYWORD))
            {
                ParseFunDeclaration();
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
            if(Match(TokenType.VARIABLE))
            {
                ParseExprStatement();
            }
            else if(Match(TokenType.IF_KEYWORD))
            {
                ParseIfStatement();
            }
            else if (Match(TokenType.PRINT_KEYWORD))
            {
                ParsePrintStatement();
            }
            else if (Match(TokenType.WHILE_KEYWORD))
            {
                ParseWhileStatement();
            }
            else if (Match(TokenType.LEFT_CURLY_BRACE))
            {
                ParseBlock();
            }
            else if (Match(TokenType.RETURN_KEYWORD))
            {
                ParseReturn();
            }
        }

        private void ParseReturn()
        {
            if(Match(TokenType.RETURN_KEYWORD))
            {
                Eat(TokenType.RETURN_KEYWORD);
                ParseExpression();
            }
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

                while(!Match(TokenType.RIGHT_CURLY_BRACE))
                {
                    ParseDecleration();
                }

                Eat(TokenType.RIGHT_CURLY_BRACE);
            }
        }

        private void ParseExprStatement()
        {
            ParseAssignment();
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

        private void ParseFunDeclaration()
        {
            if(Match(TokenType.FUN_KEYWORD))
            {
                Eat(TokenType.FUN_KEYWORD);
                Eat(TokenType.VARIABLE);
                ParseFunctionBody();
            }
        }

        #endregion

        #region Expression Parse Methods

        private void ParseExpression()
        {
            ParseLogicOr();

            //if(Match(TokenType.VARIABLE))
            //{
            //    Eat(TokenType.VARIABLE);

            //    if(Match(TokenType.ASSIGNMENT))
            //    {
            //        Eat(TokenType.ASSIGNMENT);

            //        ParseExpression();
            //    }

            //}
            //else
            //{
            //    ParseLogicOr();
            //}
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
            else if(this.NextToken().TokenType == TokenType.NUMBER || this.NextToken().TokenType == TokenType.STRING
                    || this.NextToken().TokenType == TokenType.TRUE_KEYWORD || this.NextToken().TokenType == TokenType.FALSE_KEYWORD
                    || this.NextToken().TokenType == TokenType.NIL || this.NextToken().TokenType == TokenType.VARIABLE 
                    || this.NextToken().TokenType == TokenType.LEFT_PAREN)
            {
                ParseCall();
            }
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
            ParsePrimary();

            while(Match(TokenType.LEFT_PAREN))
            {
                Eat(TokenType.LEFT_PAREN);

                ParseArguments();

                Eat(TokenType.RIGHT_PAREN);
            }
        }

        #endregion

        #region Function Call Parse

        private void ParseArguments()
        {
            ParseExpression();

            while(Match(TokenType.COMMA))
            {
                ParseExpression();
            }
        }

        private void ParseParameters()
        {
            if(Match(TokenType.VARIABLE))
            {
                Eat(TokenType.VARIABLE);
                while(Match(TokenType.COMMA))
                {
                    Eat(TokenType.COMMA);
                    Eat(TokenType.VARIABLE);
                }
            }
        }

        private void ParseFunctionBody()
        {
            if(Match(TokenType.LEFT_PAREN))
            {
                Eat(TokenType.LEFT_PAREN);

                if(Match(TokenType.VARIABLE))
                {
                    ParseParameters();

                    if (Match(TokenType.RIGHT_PAREN))
                    {
                        Eat(TokenType.RIGHT_PAREN);
                    }

                    ParseBlock();
                }
            }
        }

        #endregion

        #endregion
    }
}
