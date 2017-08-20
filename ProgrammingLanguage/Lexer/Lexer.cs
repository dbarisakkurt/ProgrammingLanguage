using System;
using System.Collections.Generic;

namespace ProgrammingLanguage.Lexer
{
    internal class Lexer
    {
        private string m_Input;
        private int m_CurrentPosition;
        private char m_CurrentChar;
        private Dictionary<TokenType, string> m_Keywords = new Dictionary<TokenType, string>();

        internal Lexer(string input)
        {
            m_Input = input;
            m_CurrentPosition = 0;
            GetKeywords();
        }

        internal void Lex()
        {
            m_CurrentChar = m_Input[m_CurrentPosition];
            Token token;

            switch (m_CurrentChar)
            {
                case '+':
                    token = new Token(TokenType.PLUS, '+');
                    Advance();
                    break;
                case '-':
                    token = new Token(TokenType.MINUS, '-');
                    Advance();
                    break;
                case '*':
                    token = new Token(TokenType.MULTIPLY, '*');
                    Advance();
                    break;
                case '/':
                    token = new Token(TokenType.DIVIDE, '/');
                    Advance();
                    break;
                case '=':
                    token = new Token(TokenType.ASSIGNMENT, '=');  //==
                    Advance();
                    break;
                case '!':
                    token = new Token(TokenType.NOT, '!'); // !=
                    Advance();
                    break;
                case '<':
                    token = new Token(TokenType.LESS_THAN, '<'); // <=
                    Advance();
                    break;
                case '>':
                    token = new Token(TokenType.GREATER_THAN, '>'); // >=
                    Advance();
                    break;
                case '#':
                    token = new Token(TokenType.COMMENT, null);
                    IgnoreComments();
                    break;
                default:
                    if(char.IsDigit(m_CurrentChar))
                    {

                    }
                    else if(char.IsLetter(m_CurrentChar))
                    {

                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                    break;
            }
        }

        private void Advance()
        {
            if (m_CurrentPosition < m_Input.Length)
            {
                m_CurrentPosition += 1;
            }
        }

        private void SkipWhitespace()
        {
            if (char.IsWhiteSpace(m_CurrentChar))
            {
                Advance();
            }
        }

        private void IgnoreComments()
        {
        }

        private void GetKeywords()
        {
            m_Keywords.Add(TokenType.IF_KEYWORD, "eğer");
            m_Keywords.Add(TokenType.ELSE_KEYWORD, "değilse");
            m_Keywords.Add(TokenType.TRUE, "doğru");
            m_Keywords.Add(TokenType.FALSE, "yanlış");
            m_Keywords.Add(TokenType.AND, "ve");
            m_Keywords.Add(TokenType.OR, "veya");
            m_Keywords.Add(TokenType.WHILE_KEYWORD, "oldukça");
            m_Keywords.Add(TokenType.PRINT, "yazdır");
            m_Keywords.Add(TokenType.VARIABLE, "değişken");
        }
    }
}
