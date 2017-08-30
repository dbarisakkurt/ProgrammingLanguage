using System;
using System.Collections.Generic;
using System.Globalization;
using ProgrammingLanguage.SyntaxAnalysis.Nodes;
using ProgrammingLanguage.LexicalAnalysis;

namespace ProgrammingLanguage.Interpreter
{
    internal class Evaluator
    {
        //###################################################################################
        #region Internal Methods

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

        #endregion

        //###################################################################################
        #region Private Methods

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

                if (operatorToken.TokenType == TokenType.PLUS) //int or variable
                {
                    return Operate(left, right, leftVal, rightVal, (a, b) => a + b);
                }
                else if (operatorToken.TokenType == TokenType.MINUS)
                {
                    return Operate(left, right, leftVal, rightVal, (a, b) => a - b);
                }
                else if (operatorToken.TokenType == TokenType.MULTIPLY)
                {
                    return Operate(left, right, leftVal, rightVal, (a, b) => a * b);
                }
                else if (operatorToken.TokenType == TokenType.DIVIDE)
                {
                    return Operate(left, right, leftVal, rightVal, (a, b) => a / b);
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
                    return Compare(left, right, leftVal, rightVal, (a, b) => a > b);
                }
                else if (operatorToken.TokenType == TokenType.GREATER_THAN_OR_EQUAL)
                {
                    return Compare(left, right, leftVal, rightVal, (a, b) => a >= b);
                }
                else if (operatorToken.TokenType == TokenType.LESS_THAN)
                {
                    return Compare(left, right, leftVal, rightVal, (a, b) => a < b);
                }
                else if (operatorToken.TokenType == TokenType.LESS_THAN_OR_EQUAL)
                {
                    return Compare(left, right, leftVal, rightVal, (a, b) => a <= b);
                }
                else if (operatorToken.TokenType == TokenType.EQUAL)
                {
                    return Compare(left, right, leftVal, rightVal, (a, b) => a == b);
                }
                else if (operatorToken.TokenType == TokenType.NOT_EQUAL)
                {
                    return Compare(left, right, leftVal, rightVal, (a, b) => a != b);
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
                Node condition = ((WhileNode)node).Condition;
                List<Node> whileBlock = ((WhileNode)node).Statements;

                while (Evaluate(condition) is bool && (bool)Evaluate(condition) == true)
                {
                    foreach(Node n in whileBlock)
                    {
                        Evaluate(n);
                    }
                }               
            }

            return null;
        }

        bool Compare(Node left, Node right, object leftVal, object rightVal, Func<int, int, bool> compare)
        {
            int leftInt, rightInt;
            if(Int32.TryParse(leftVal.ToString(), out leftInt) && Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                return CompareInteger(Int32.Parse(leftVal.ToString()), Int32.Parse(rightVal.ToString()), compare);
            }
            //left only is integer
            else if(Int32.TryParse(leftVal.ToString(), out leftInt) && !Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                int rightVal__ = Int32.Parse(Environment.Get(((AtomicNode)right).Value.ToString()).ToString());
                return CompareInteger(Int32.Parse(leftVal.ToString()), rightVal__, compare);
            }
            //right only is integer
            else if (!Int32.TryParse(leftVal.ToString(), out leftInt) && Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                int leftVal__ = Int32.Parse(Environment.Get(((AtomicNode)left).Value.ToString()).ToString());
                return CompareInteger(leftVal__, Int32.Parse(rightVal.ToString()), compare);
            }
            //no integer, means both are variables
            else if (!Int32.TryParse(leftVal.ToString(), out leftInt) && !Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                int leftVal__ = Int32.Parse(Environment.Get(((AtomicNode)left).Value.ToString()).ToString());
                int rightVal__ = Int32.Parse(Environment.Get(((AtomicNode)right).Value.ToString()).ToString());
                return CompareInteger(leftVal__, rightVal__, compare);
            }
            else
            {
                throw new InvalidOperationException("Compare error");
            }
        }

        int Operate(Node left, Node right, object leftVal, object rightVal, Func<int, int, int> operate)
        {
            int leftInt, rightInt;
            if (Int32.TryParse(leftVal.ToString(), out leftInt) && Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                return OperateMath(Int32.Parse(leftVal.ToString()), Int32.Parse(rightVal.ToString()), operate);
            }
            //left only is integer
            else if (Int32.TryParse(leftVal.ToString(), out leftInt) && !Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                int rightVal__ = Int32.Parse(Environment.Get(((AtomicNode)right).Value.ToString()).ToString());
                return OperateMath(Int32.Parse(leftVal.ToString()), rightVal__, operate);
            }
            //right only is integer
            else if (!Int32.TryParse(leftVal.ToString(), out leftInt) && Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                int leftVal__ = Int32.Parse(Environment.Get(((AtomicNode)left).Value.ToString()).ToString());
                return OperateMath(leftVal__, Int32.Parse(rightVal.ToString()), operate);
            }
            //no integer, means both are variables
            else if (!Int32.TryParse(leftVal.ToString(), out leftInt) && !Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                int leftVal__ = Int32.Parse(Environment.Get(((AtomicNode)left).Value.ToString()).ToString());
                int rightVal__ = Int32.Parse(Environment.Get(((AtomicNode)right).Value.ToString()).ToString());
                return OperateMath(leftVal__, rightVal__, operate);
            }
            else
            {
                throw new InvalidOperationException("Compare error");
            }
        }

        bool CompareInteger(int input1, int input2, Func<int, int, bool> comparisonOperator)
        {
            return comparisonOperator(input1, input2);
        }

        int OperateMath(int input1, int input2, Func<int, int, int> comparisonOperator)
        {
            return comparisonOperator(input1, input2);
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

        #endregion
    }
}
