using System;
using ProgrammingLanguage.SyntaxAnalysis.Nodes;
using System.Collections.Generic;
using ProgrammingLanguage.LexicalAnalysis;
using System.Globalization;

namespace ProgrammingLanguage.Interpreter
{
    internal class Evaluator
    {
        internal List<object> Eval(ProgramNode programNode)
        {
            List<object> values = new List<object>();

            foreach(Node node in programNode.Statements)
            {
                var lineRes = Evaluate(node);

                if(lineRes != null)
                {
                    values.Add(lineRes);
                }
            }

            return values;
        }

        private object Evaluate(Node node)
        {
            if (node is AtomicNode)
            {
                var value = ((AtomicNode)node).Value;
                return value;
            }
            else if (node is VariableDeclarationNode)  //returns null
            {
                string varName = ((VariableDeclarationNode)node).VariableName;
                object rhv = ((VariableDeclarationNode)node).RightHandValue;
                object value = Evaluate((Node)rhv);

                Environment.Add(varName, value);

                return null;
            }
            else if (node is PrintNode)      //returns null
            {
                object printResult;
                Node toBePrinted = ((PrintNode)node).ToBePrinted;
                var value = ((AtomicNode)toBePrinted).Value;
                if(((AtomicNode)toBePrinted).Token.TokenType == TokenType.VARIABLE)
                {
                    printResult = Environment.Get(value.ToString());
                }
                else
                {
                    printResult = value;
                }

                if(printResult is bool && (bool)printResult == true)
                {
                    printResult = "doğru";
                }
                else if (printResult is bool && (bool)printResult == false)
                {
                    printResult = "yanlış";
                }

                string resultText = string.Format(CultureInfo.InvariantCulture, printResult.ToString());
                Console.WriteLine(resultText);
                return resultText;  //temporarily return print value for testing
            }
            else if (node is BinaryOperatorNode)
            {
                Node left = ((BinaryOperatorNode)node).Left;
                object leftVal = Evaluate(left);

                Node oper = ((BinaryOperatorNode)node).Operator;
                Token operatorToken = ((AtomicNode)oper).Token;

                Node right = ((BinaryOperatorNode)node).Right;
                object rightVal = Evaluate(right);

                if (operatorToken.TokenType == TokenType.PLUS)
                {
                    return Double.Parse( leftVal.ToString(), CultureInfo.InvariantCulture
                        ) + Double.Parse(rightVal.ToString(), CultureInfo.InvariantCulture);
                }
                else if (operatorToken.TokenType == TokenType.MINUS)
                {
                    return Double.Parse(leftVal.ToString(), CultureInfo.InvariantCulture
                        ) - Double.Parse(rightVal.ToString(), CultureInfo.InvariantCulture);
                }
                else if (operatorToken.TokenType == TokenType.MULTIPLY)
                {
                    return Double.Parse(leftVal.ToString(), CultureInfo.InvariantCulture
                        ) * Double.Parse(rightVal.ToString(), CultureInfo.InvariantCulture);
                }
                else if (operatorToken.TokenType == TokenType.DIVIDE)
                {
                    return Double.Parse(leftVal.ToString(), CultureInfo.InvariantCulture
                        ) / Double.Parse(rightVal.ToString(), CultureInfo.InvariantCulture);
                }
                else if (operatorToken.TokenType == TokenType.AND_KEYWORD)
                {
                    return (bool)leftVal && (bool)rightVal;
                }
                else if (operatorToken.TokenType == TokenType.OR_KEYWORD)
                {
                    return (bool)leftVal || (bool)rightVal;
                }
                else if (operatorToken.TokenType == TokenType.GREATER_THAN)
                {
                    return Int32.Parse(leftVal.ToString()) > Int32.Parse(rightVal.ToString());
                }
                else if (operatorToken.TokenType == TokenType.GREATER_THAN_OR_EQUAL)
                {
                    return Int32.Parse(leftVal.ToString()) >= Int32.Parse(rightVal.ToString());
                }
                else if (operatorToken.TokenType == TokenType.LESS_THAN)
                {
                    return Int32.Parse(leftVal.ToString()) < Int32.Parse(rightVal.ToString());
                }
                else if (operatorToken.TokenType == TokenType.LESS_THAN_OR_EQUAL)
                {
                    return Int32.Parse(leftVal.ToString()) <= Int32.Parse(rightVal.ToString());
                }
                else if (operatorToken.TokenType == TokenType.EQUAL)
                {
                    return object.Equals(leftVal, rightVal);
                }
                else if (operatorToken.TokenType == TokenType.NOT_EQUAL)
                {
                    return !object.Equals(leftVal, rightVal);
                }
                else
                {
                    throw new InvalidOperationException("Invalid token scan during evaluation");
                }

            }
            else if (node is UnaryNode)
            {
                Node prefix = ((UnaryNode)node).PrefixNode;
                Node actual = ((UnaryNode)node).Node;

                object r = Evaluate(actual);

                if(((AtomicNode)prefix).Token.TokenType ==TokenType.NOT)
                {
                    bool val = (bool)r;
                    return !val;
                }
                else if (((AtomicNode)prefix).Token.TokenType == TokenType.MINUS)
                {
                    int val = Int32.Parse(r.ToString());
                    return -val;
                }
            }
            else if(node is IfNode)
            {
                Node ifNode = ((IfNode)node).IfNodeProperty;
                Node condition = ((IfNode)node).Condition;
                List<Node> ifBlock = ((IfNode)node).Statements;
                List<Node> elseBlock = ((IfNode)node).ElseBlock;

                object res = Evaluate(condition);
                if(res is bool && (bool)res == true)
                {
                    foreach(Node n in ifBlock)
                    {
                        Evaluate(n);
                    }
                    
                }
                else
                {
                    foreach (Node n in elseBlock)
                    {
                        Evaluate(n);
                    }
                }

            }
            else if(node is WhileNode)
            {
                Node whileNode = ((WhileNode)node).WhileNodeProperty;
                Node condition = ((WhileNode)node).Condition;
                List<Node> whileBlock = ((WhileNode)node).Statements;

                if (Evaluate(condition) is bool && (bool)Evaluate(condition) == true)
                {
                    foreach(Node n in whileBlock)
                    {
                        Evaluate(n);
                    }
                }
                
                //todo
            }

            return null;
        }

        private string ConvertNativeBoolToBoolean(bool value)
        {
            if(value)
            {
                return "doğru";
            }
            else
            {
                return "yanlış";
            }
        }
    }
}
