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
                Node toBePrinted = ((PrintNode)node).ToBePrinted;
                var value = ((AtomicNode)toBePrinted).Value;
                object result = Environment.Get((string)value);

                if(result is bool && (bool)result == true)
                {
                    result = "doğru";
                }
                else if (result is bool && (bool)result == false)
                {
                    result = "yanlış";
                }

                string resultText = string.Format(CultureInfo.InvariantCulture, result.ToString());
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
                //add other operators

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
                    int val = (int)r;
                    return -val;
                }
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
