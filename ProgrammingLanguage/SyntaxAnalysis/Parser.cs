using System;
using ProgrammingLanguage.LexicalAnalysis;
using System.Collections.Generic;
using ProgrammingLanguage.SyntaxAnalysis.Nodes;
using System.Collections.Specialized;
using System.Collections;

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

//assignment  → IDENTIFIER (( "=" ) expression )? | expression                      
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
        private ProgramNode m_ProgramNode = new ProgramNode();

        #endregion

        public ProgramNode ProgramNode { get { return m_ProgramNode; } }

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
                ParseDecleration(m_ProgramNode);
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

        private Node ParseAssignment()
        {
            Node result = null;

            if (Match(TokenType.VARIABLE))
            {
                string variableName = m_CurrentToken.Value.ToString();
                Eat(TokenType.VARIABLE);

                if (Match(TokenType.ASSIGNMENT))
                {
                    Eat(TokenType.ASSIGNMENT);
                    Node exprNode = ParseExpression();
                    result = new VariableDeclarationNode(variableName, m_CurrentToken.Value, exprNode);
                }
            }
            else
            {
                Node exprNode = ParseExpression();
                result = new VariableDeclarationNode("", m_CurrentToken.Value, exprNode);
            }

            return result;
        }

        private void ParseDecleration(INodeList nodeList, bool elseCall = false)
        {
            if(Match(TokenType.FUN_KEYWORD))
            {
                Node node = ParseFunDeclaration();
                nodeList.Statements.Add(node);
            }
            else if(Match(TokenType.VAR_KEYWORD))
            {
                Node node = ParseVariableDecleration();
                nodeList.Statements.Add(node);
            }
            else
            {
                ParseStatement(nodeList, elseCall);
            }
        }

        private Node ParseVariableDecleration()
        {
            Node result = null;
            Eat(TokenType.VAR_KEYWORD);

            string variableName = m_CurrentToken.Value.ToString();
            object value = m_CurrentToken.Value;
            Eat(TokenType.VARIABLE);

            if (Match(TokenType.ASSIGNMENT))
            {
                AdvanceToken();
                Node node = ParseExpression();
                result = new VariableDeclarationNode(variableName, value, node);
            }

            return result;
        }

        private void ParseStatement(INodeList nodeList, bool elseCall = false)
        {
            if (Match(TokenType.VARIABLE) && NextToken().TokenType == TokenType.LEFT_PAREN)
            {
                Node returnValNode = ParseCall();
                nodeList.AddStatement(returnValNode);
            }
            else if (Match(TokenType.VARIABLE))
            {
                Node exprStatement = ParseExprStatement();
                nodeList.AddStatement(exprStatement, elseCall);
            }
            else if(Match(TokenType.IF_KEYWORD))
            {
                Node ifStatement = ParseIfStatement();
                nodeList.AddStatement(ifStatement, elseCall);
            }
            else if (Match(TokenType.PRINT_KEYWORD))
            {
                Node printNode = ParsePrintStatement();
                nodeList.AddStatement(printNode, elseCall);
            }
            else if (Match(TokenType.WHILE_KEYWORD))
            {
                Node whileStatement = ParseWhileStatement();
                nodeList.AddStatement(whileStatement, elseCall);
            }
            else if (Match(TokenType.LEFT_CURLY_BRACE))
            {
                ParseBlock(nodeList, elseCall);
            }
            else if (Match(TokenType.RETURN_KEYWORD))
            {
                Node returnValNode = ParseReturn();
                nodeList.AddStatement(returnValNode);
            }
            else
            {
                throw new ParseException("Statement is expected.");
            }
        }

        private Node ParseReturn()
        {
            ReturnNode rNode = null;
            if(Match(TokenType.RETURN_KEYWORD))
            {
                Eat(TokenType.RETURN_KEYWORD);
                Node returnValNode = ParseExpression();
                rNode = new ReturnNode(returnValNode);
            }
            return rNode;
        }

        private Node ParsePrintStatement()
        {
            Node result = null;

            if(Match(TokenType.PRINT_KEYWORD))
            {
                Eat(TokenType.PRINT_KEYWORD);
                result = ParseExpression();
                return new PrintNode(result);
            }

            return result;
        }

        private Node ParseWhileStatement()
        {
            List<Node> block = new List<Node>();
            INodeList node = null;
            Node condition = null;

            if (Match(TokenType.WHILE_KEYWORD))
            {
                Eat(TokenType.WHILE_KEYWORD);

                Eat(TokenType.LEFT_PAREN);
                condition = ParseExpression();
                Eat(TokenType.RIGHT_PAREN);

                if (!Match(TokenType.LEFT_CURLY_BRACE))
                {
                    throw new ParseException("Left curly brace is expected after right paranthesis in if statement");
                }
                node = new WhileNode(condition, block);

                ParseStatement(node);
            }

            

            return (Node)node;
        }

        private void ParseBlock(INodeList nodeList, bool elseCall = false)
        {
            if (Match(TokenType.LEFT_CURLY_BRACE))
            {
                Eat(TokenType.LEFT_CURLY_BRACE);

                while(!Match(TokenType.RIGHT_CURLY_BRACE))
                {
                    ParseDecleration(nodeList, elseCall);
                }

                Eat(TokenType.RIGHT_CURLY_BRACE);
            }
        }

        private Node ParseExprStatement()
        {
            return ParseAssignment();
        }

        private Node ParseIfStatement()
        {
            List<Node> ifStatementBlock = new List<Node>();
            List<Node> elseStatementBlock = new List<Node>();
            INodeList node = null;
            Node expr = null;

            if (Match(TokenType.IF_KEYWORD))
            {
                Eat(TokenType.IF_KEYWORD);
                Eat(TokenType.LEFT_PAREN);
                expr = ParseExpression();
                Eat(TokenType.RIGHT_PAREN);

                if(!Match(TokenType.LEFT_CURLY_BRACE))
                {
                    throw new ParseException("Left curly brace is expected after right paranthesis in if statement");
                }

                node = new IfNode(expr, ifStatementBlock, elseStatementBlock);

                ParseStatement(node);
                if (Match(TokenType.ELSE_KEYWORD))
                {
                    Eat(TokenType.ELSE_KEYWORD);

                    if (!Match(TokenType.LEFT_CURLY_BRACE))
                    {
                        throw new ParseException("Left curly brace is expected after right paranthesis in if statement");
                    }

                    ParseStatement(node, true);
                }

            }

            

            return (Node)node;

        }

        private Node ParseFunDeclaration()
        {
            if(Match(TokenType.FUN_KEYWORD))
            {
                Eat(TokenType.FUN_KEYWORD);

                string funcName = m_CurrentToken.Value.ToString();
                Eat(TokenType.VARIABLE);
                Node funcBlock = ParseFunctionBody(funcName);
                return funcBlock;
            }
            return null;
        }

        #endregion

        #region Expression Parse Methods

        private Node ParseExpression()
        {
            return ParseLogicOr();
        }

        private Node ParseLogicOr()
        {
            Node andNode = ParseLogicAnd();

            while (Match(TokenType.OR_KEYWORD))
            {
                OperatorNode operatorNode = new OperatorNode(m_CurrentToken, m_CurrentToken.Value.ToString());
                Eat(TokenType.OR_KEYWORD);
                Node rightAndNode = ParseLogicAnd();

                andNode = new BinaryExpressionNode(andNode, operatorNode, rightAndNode);
            }
            return andNode;
        }

        private Node ParseLogicAnd()
        {
            Node equalityNode = Equality();

            while (Match(TokenType.AND_KEYWORD))
            {
                OperatorNode operatorNode = new OperatorNode(m_CurrentToken, m_CurrentToken.Value.ToString());

                Eat(TokenType.AND_KEYWORD);
                Node rightEqualityNode = Equality();
                equalityNode = new BinaryExpressionNode(equalityNode, operatorNode, rightEqualityNode);

            }
            return equalityNode;
        }

        private Node Equality()
        {
            Node comparisonNode = ParseComparison();

            while (Match(TokenType.EQUAL) || Match(TokenType.NOT_EQUAL))
            {
                OperatorNode operatorNode = new OperatorNode(m_CurrentToken, m_CurrentToken.Value.ToString());

                if(Match(TokenType.EQUAL))
                {
                    Eat(TokenType.EQUAL);
                }
                else if(Match(TokenType.NOT_EQUAL))
                {
                    Eat(TokenType.NOT_EQUAL);
                }

                Node rightComparisonNode = ParseComparison();
                comparisonNode = new BinaryExpressionNode(comparisonNode, operatorNode, rightComparisonNode);
            }
            return comparisonNode;
        }
        
        private Node ParseComparison()
        {
            Node termNode = ParseTerm();

            while (Match(TokenType.GREATER_THAN) || Match(TokenType.GREATER_THAN_OR_EQUAL)
                || Match(TokenType.LESS_THAN) || Match(TokenType.LESS_THAN_OR_EQUAL))
            {
                OperatorNode operatorNode = new OperatorNode(m_CurrentToken, m_CurrentToken.Value.ToString());
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

                Node rightTermNode = ParseTerm();
                termNode = new BinaryExpressionNode(termNode, operatorNode, rightTermNode);
            }
            return termNode;
        }

        private Node ParseTerm()
        {
            Node factorNode = ParseFactor();

            while (Match(TokenType.MINUS) || Match(TokenType.PLUS))
            {
                OperatorNode operatorNode = new OperatorNode(m_CurrentToken, m_CurrentToken.Value.ToString());
                if (Match(TokenType.MINUS))
                {
                    Eat(TokenType.MINUS);
                }
                else if (Match(TokenType.PLUS))
                {
                    Eat(TokenType.PLUS);
                }

                Node rightFactorNode = ParseFactor();
                factorNode = new BinaryExpressionNode(factorNode, operatorNode, rightFactorNode);
            }

            return factorNode;
        }

        private Node ParseFactor()
        {
            Node unaryNode = ParseUnary();

            while (Match(TokenType.DIVIDE) || Match(TokenType.MULTIPLY))
            {
                OperatorNode operatorNode = new OperatorNode(m_CurrentToken, m_CurrentToken.Value.ToString());
                if (Match(TokenType.DIVIDE))
                {
                    Eat(TokenType.DIVIDE);
                }
                else if (Match(TokenType.MULTIPLY))
                {
                    Eat(TokenType.MULTIPLY);
                }

                Node rightUnaryNode = ParseUnary();
                unaryNode = new BinaryExpressionNode(unaryNode, operatorNode, rightUnaryNode);
            }

            return unaryNode;
        }

        private Node ParseUnary()
        {
            Node node = null;

            if (Match(TokenType.NOT) || Match(TokenType.MINUS))
            {
                OperatorNode prefixOpNode = new OperatorNode(m_CurrentToken, m_CurrentToken.Value.ToString());
                if (Match(TokenType.NOT))
                {
                    Eat(TokenType.NOT);
                }
                if(Match(TokenType.MINUS))
                {
                    Eat(TokenType.MINUS);
                }
                
                Node tempNode = ParseUnary();
                node = new UnaryNode(prefixOpNode, tempNode);
            }
            else if((Match(TokenType.NUMBER) || Match(TokenType.STRING) || Match(TokenType.TRUE_KEYWORD) ||
                Match(TokenType.FALSE_KEYWORD) || Match(TokenType.VARIABLE) ||
                Match(TokenType.LEFT_PAREN))  && NextToken().TokenType != TokenType.LEFT_PAREN )
            {
                node = ParsePrimary();
            }
            else if(Match(TokenType.NUMBER) || Match(TokenType.STRING)
                    || Match(TokenType.TRUE_KEYWORD) || Match(TokenType.FALSE_KEYWORD)
                    || Match(TokenType.VARIABLE) || Match(TokenType.LEFT_PAREN))
            {
                Node tempNode = ParseCall();
                node = new UnaryNode(null, tempNode);  //TODO think about first parameter
            }
            return node;
        }

        private Node ParsePrimary()
        {
            Node node;
            if(Match(TokenType.NUMBER))
            {
                node = new NumberNode(m_CurrentToken, Int32.Parse(m_CurrentToken.Value.ToString()));
                Eat(TokenType.NUMBER);
                
            }
            else if (Match(TokenType.STRING))
            {
                node = new StringNode(m_CurrentToken, (string)m_CurrentToken.Value);
                Eat(TokenType.STRING);
                
            }
            else if (Match(TokenType.TRUE_KEYWORD))
            {
                node = new BooleanNode(m_CurrentToken, true);
                Eat(TokenType.TRUE_KEYWORD);
                
            }
            else if (Match(TokenType.FALSE_KEYWORD))
            {
                node = new BooleanNode(m_CurrentToken, false);
                Eat(TokenType.FALSE_KEYWORD);
                
            }
            else if (Match(TokenType.VARIABLE))
            {
                node = new VariableNode(m_CurrentToken.Value.ToString(), m_CurrentToken);
                Eat(TokenType.VARIABLE);
                
            }
            else if (Match(TokenType.LEFT_PAREN))
            {
                Eat(TokenType.LEFT_PAREN);
                node = ParseExpression();
                Eat(TokenType.RIGHT_PAREN);
            }
            else
            {
                throw new ParseException("Parse error");
            }
            return node;

        }

        private Node ParseCall()
        {
            Node functionCallName = ParsePrimary();
            List<Node> args = new List<Node>();

            while (Match(TokenType.LEFT_PAREN))
            {
                Eat(TokenType.LEFT_PAREN);
                
                args = ParseArguments();
                
                Eat(TokenType.RIGHT_PAREN);
            }

            Node callNode = new CallNode(((VariableNode)functionCallName).VariableName, args);

            return callNode;
        }

        #endregion

        #region Function Call Parse

        private List<Node> ParseArguments()
        {
            List<Node> arguments = new List<Node>();
            Node n1 = ParseExpression();
            if(n1!=null)
            {
                arguments.Add(n1);
            }

            while(Match(TokenType.COMMA))
            {
                Eat(TokenType.COMMA);
                Node n2 = ParseExpression();
                if(n2!=null)
                {
                    arguments.Add(n2);
                }
            }
            return arguments;
        }

        private List<Node> ParseParameters()
        {
            List<Node> parameters = new List<Node>();

            if(Match(TokenType.VARIABLE))
            {
                parameters.Add(new VariableNode(m_CurrentToken.Value.ToString(), m_CurrentToken));

                Eat(TokenType.VARIABLE);
                while(Match(TokenType.COMMA))
                {
                    Eat(TokenType.COMMA);
                    parameters.Add(new VariableNode(m_CurrentToken.Value.ToString(), m_CurrentToken));
                    Eat(TokenType.VARIABLE);
                }
            }
            return parameters;
        }

        private Node ParseFunctionBody(string funcName)
        {
            List<Node> funcStatements = new List<Node>();
            INodeList funcBlock = new FunctionDeclarationNode(funcName, new List<Node>(), funcStatements); ;

            if(Match(TokenType.LEFT_PAREN))
            {
                Eat(TokenType.LEFT_PAREN);

                if(Match(TokenType.VARIABLE))
                {

                    List<Node> parameterList = ParseParameters();
                    if(parameterList.Count > 32)
                    {
                        throw new ParseException("Number of function arguments must be less than 32");
                    }

                    funcBlock = new FunctionDeclarationNode(funcName, parameterList, funcStatements);

                    if (Match(TokenType.RIGHT_PAREN))
                    {
                        Eat(TokenType.RIGHT_PAREN);
                    }

                    ParseBlock(funcBlock);
                }
                else //no variable given to function
                {
                    if (Match(TokenType.RIGHT_PAREN))
                    {
                        Eat(TokenType.RIGHT_PAREN);
                    }

                    ParseBlock(funcBlock);
                }
            }
            return (Node)funcBlock;
        }

        #endregion

        #endregion
    }
}
