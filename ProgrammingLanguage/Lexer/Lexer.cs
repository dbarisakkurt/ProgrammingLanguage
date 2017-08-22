using System;
using System.Collections.Generic;

namespace ProgrammingLanguage.LexicalAnalysis
{
    internal class Lexer
    {
        //###################################################################################
        #region Fields

        private string m_Input;
        private int m_CurrentPosition;
        private char m_CurrentChar;
        private Dictionary<string, TokenType> m_Keywords = new Dictionary<string, TokenType>();
        private List<Token> m_TokenList = new List<Token>();

        #endregion

        //###################################################################################
        #region Properties

        internal List<Token> TokenList
        {
            get
            {
                return m_TokenList;
            }
        }

        #endregion

        //###################################################################################
        #region Internal Methods

        internal Lexer(string input)
        {
            m_Input = input;
            m_CurrentPosition = 0;
            GetKeywords();
        }

        internal void Lex()
        {
            while (!IsAtEnd())
            {

                m_CurrentChar = m_Input[m_CurrentPosition];
                Token token = null;

                switch (m_CurrentChar)
                {
                    case ' ':
                        SkipWhitespace();
                        break;
                    case ',':
                        token = new Token(TokenType.COMMA, ',');
                        Advance();
                        m_TokenList.Add(token);
                        break;
                    case '+':
                        token = new Token(TokenType.PLUS, '+');
                        Advance();
                        m_TokenList.Add(token);
                        break;
                    case '-':
                        token = new Token(TokenType.MINUS, '-');
                        Advance();
                        m_TokenList.Add(token);
                        break;
                    case '*':
                        token = new Token(TokenType.MULTIPLY, '*');
                        Advance();
                        m_TokenList.Add(token);
                        break;
                    case '/':
                        token = new Token(TokenType.DIVIDE, '/');
                        Advance();
                        m_TokenList.Add(token);
                        break;
                    case '=':
                        if(Peek() == '=')
                        {
                            token = new Token(TokenType.EQUAL, "==");
                            Advance();
                        }
                        else
                        {
                            token = new Token(TokenType.ASSIGNMENT, '=');
                        }
                        Advance();
                        m_TokenList.Add(token);
                        break;
                    case '!':
                        if(Peek() == '=')
                        {
                            token = new Token(TokenType.NOT_EQUAL, "!=");
                            Advance();
                        }
                        else
                        {
                            token = new Token(TokenType.NOT, '!'); 
                        }

                        Advance();
                        m_TokenList.Add(token);
                        break;
                    case '<':
                        if(Peek() == '=')
                        {
                            token = new Token(TokenType.LESS_THAN_OR_EQUAL, "<=");
                            Advance();
                        }
                        else
                        {
                            token = new Token(TokenType.LESS_THAN, '<');
                        }
                        Advance();
                        m_TokenList.Add(token);
                        break;
                    case '>':
                        if (Peek() == '=')
                        {
                            token = new Token(TokenType.GREATER_THAN_OR_EQUAL, ">=");
                            Advance();
                        }
                        else
                        {
                            token = new Token(TokenType.GREATER_THAN, '>');
                        }
                        Advance();
                        m_TokenList.Add(token);
                        break;
                    case '#':
                        IgnoreComments();
                        break;
                    case '"':
                        string resultString = ParseString();
                        token = new Token(TokenType.STRING, resultString);
                        m_TokenList.Add(token);
                        break;
                    default:
                        if (char.IsDigit(m_CurrentChar))
                        {
                            string resultNumber = ParseNumber();
                            token = new Token(TokenType.NUMBER, resultNumber);
                            m_TokenList.Add(token);
                            Advance();
                        }
                        else if (char.IsLetter(m_CurrentChar))
                        {
                            string resultVar = ParseVariable();
                            if(m_Keywords.ContainsKey(resultVar))
                            {
                                token = new Token(m_Keywords[resultVar], resultVar);
                            }
                            else
                            {
                                token = new Token(TokenType.VARIABLE, resultVar);
                            }
                            m_TokenList.Add(token);
                            Advance();
                        }
                        else if(IsAtEnd())
                        {
                            token = new Token(TokenType.EOF, null);
                            m_TokenList.Add(token);
                        }
                        else
                        {
                            throw new InvalidOperationException();
                        }
                        break;
                }
            }
        }

        #endregion

        //###################################################################################
        #region Private Methods

        private string ParseNumber()
        {
            int start = m_CurrentPosition;
            while (char.IsDigit(m_CurrentChar))
            {
                Advance();
            }

            if(m_CurrentChar == '.')
            {
                Advance();

                while (char.IsDigit(m_CurrentChar))
                {
                    Advance();
                }
            }

            int end = m_CurrentPosition;

            return m_Input.Substring(start, end-start);
        }

        private string ParseVariable()
        {
            int start = m_CurrentPosition;
            while (char.IsLetterOrDigit(m_CurrentChar))
            {
                Advance();
            }

            int end = m_CurrentPosition;

            return m_Input.Substring(start, end - start);
        }

        private string ParseString()
        {
            int start = m_CurrentPosition +1;
            Advance();

            while (m_CurrentChar != '"')
            {
                Advance();
            }

            Advance();

            int end = m_CurrentPosition - 1;
            return m_Input.Substring(start, end - start);
        }

        private char Peek()
        {
            if(m_CurrentPosition < m_Input.Length - 1)
            {
                return m_Input[m_CurrentPosition + 1];
            }
            return ' ';
        }

        private void Advance()
        {
            m_CurrentPosition += 1;

            if (m_CurrentPosition < m_Input.Length)
            {
                m_CurrentChar = m_Input[m_CurrentPosition];
            }
            else
            {
                m_CurrentChar = '\0';
            }
        }

        private bool IsAtEnd()
        {
            if (m_CurrentPosition < m_Input.Length)
            {
                return false;
            }
            return true;
        }

        private void SkipWhitespace()
        {
            if (char.IsWhiteSpace(m_CurrentChar))
            {
                Advance();
            }
        }

        private bool IsNewLine()
        {
            if(m_CurrentChar == '\r')
            {
                if(Peek() == '\n')
                {
                    Advance();
                    Advance();
                    return true;
                }
            }
            return false;
        }

        private void IgnoreComments()
        {
            while(!IsNewLine())
            {
                Advance();

                if (IsAtEnd())
                    break;
            }
        }

        private void GetKeywords()
        {
            m_Keywords.Add("eğer", TokenType.IF_KEYWORD);
            m_Keywords.Add("değilse", TokenType.ELSE_KEYWORD);
            m_Keywords.Add("doğru", TokenType.TRUE);
            m_Keywords.Add("yanlış", TokenType.FALSE);
            m_Keywords.Add("ve", TokenType.AND);
            m_Keywords.Add("veya", TokenType.OR);
            m_Keywords.Add("oldukça", TokenType.WHILE_KEYWORD);
            m_Keywords.Add("yazdır", TokenType.PRINT);
            m_Keywords.Add("değişken", TokenType.VARIABLE);
        }

        #endregion
    }
}
