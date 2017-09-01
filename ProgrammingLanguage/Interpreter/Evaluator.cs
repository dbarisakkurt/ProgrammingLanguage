using System;
using System.Collections.Generic;
using System.Globalization;
using ProgrammingLanguage.SyntaxAnalysis.Nodes;
using ProgrammingLanguage.LexicalAnalysis;
using System.Collections.Specialized;
using System.Collections;
using System.Linq;

namespace ProgrammingLanguage.Interpreter
{
    internal class Evaluator
    {
        //###################################################################################
        #region Internal Methods

        internal List<object> Eval(ProgramNode programNode)
        {
            List<object> values = new List<object>();
            FunctionTable functionTable = new FunctionTable();
            SymbolTable symbolTable = new SymbolTable();

            foreach(Node node in programNode.Statements)
            {
                var lineRes = Evaluate(node, symbolTable, functionTable);

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

        private object Evaluate(Node node, SymbolTable symbolTable, FunctionTable functionTable)
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
                object value = Evaluate((Node)rhv, symbolTable, functionTable);

                symbolTable.Add(varName, value);

                return null;
            }
            else if (node is PrintNode)      //returns null
            {
                object value = null;
                object printResult =null;
                Node toBePrinted = ((PrintNode)node).ToBePrinted;
                Node singleNode = (toBePrinted as AtomicNode);
                if(singleNode != null)
                {
                    value = (toBePrinted as AtomicNode).Value;
                    if(((AtomicNode)singleNode).Token.TokenType == TokenType.VARIABLE)
                    {
                        printResult = symbolTable.Get(value.ToString());
                    }
                    else
                    {
                        printResult = value;
                    }
                }
                singleNode = (toBePrinted as UnaryNode);
                if (singleNode != null)
                {
                    value = (toBePrinted as UnaryNode).Node;

                    if (value is AtomicNode)
                    {
                        var a = ((AtomicNode)value).Value;
                    }
                    else if (value is CallNode)
                    {
                        var funName = ((AtomicNode)((CallNode)value).m_Name).Value.ToString();
                        FunctionDeclarationNode  fdn = functionTable.Get(funName);
                        List<Node> parameterValues = ((CallNode)value).m_Arguments;

                        SymbolTable symTable = (SymbolTable)symbolTable.Get(funName);
                        //symboltable yerine symtable kullan
                        for (int i = 0; i < parameterValues.Count; i++)
                        {
                            object nodeVal = ((AtomicNode)parameterValues[i]).Value;

                            DictionaryEntry de = symTable.m_Variables.Cast<DictionaryEntry>().ElementAt(i);
                            de.Value = nodeVal;
                            symTable.Add(de.Key.ToString(), de.Value);
                        }

                        for (int i = 0; i<fdn.Statements.Count; i++)
                        {
                            if(i == fdn.Statements.Count - 1 && fdn.Statements[i] is ReturnNode)
                            {
                                printResult = Evaluate(fdn.Statements[i], symTable, functionTable);
                            }
                            else
                            {
                                Evaluate(fdn.Statements[i], symTable, functionTable);
                            }
                            
                        }
                    }
                }
                singleNode = (toBePrinted as CallNode);
                if (singleNode != null)
                {
                    value = (toBePrinted as CallNode);

                    if (value is AtomicNode)
                    {
                        var a = ((AtomicNode)value).Value;
                    }
                    else if (value is CallNode)
                    {
                        var funName = ((AtomicNode)((CallNode)value).m_Name).Value.ToString();
                        FunctionDeclarationNode fdn = functionTable.Get(funName);


                        List<Node> parameterValues = ((CallNode)value).m_Arguments;
                        SymbolTable symTable = (SymbolTable)symbolTable.Get(funName);

                        for (int i = 0; i < parameterValues.Count; i++)
                        {
                            object nodeVal = ((AtomicNode)parameterValues[i]).Value;

                            DictionaryEntry de = symTable.m_Variables.Cast<DictionaryEntry>().ElementAt(i);
                            de.Value = nodeVal;
                            symTable.Add(de.Key.ToString(), de.Value);
                        }

                        for (int i = 0; i < fdn.Statements.Count; i++)
                        {
                            if (i == fdn.Statements.Count - 1 && fdn.Statements[i] is ReturnNode)
                            {
                                Evaluate(fdn.Statements[i], symTable, functionTable);
                            }

                            Evaluate(fdn.Statements[i], symTable, functionTable);
                        }
                    }


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
                object leftVal = Evaluate(left, symbolTable, functionTable);

                Node oper = ((BinaryOperatorNode)node).Operator;
                Token operatorToken = ((AtomicNode)oper).Token;

                Node right = ((BinaryOperatorNode)node).Right;
                object rightVal = Evaluate(right, symbolTable, functionTable);

                if (operatorToken.TokenType == TokenType.PLUS) //int or variable
                {
                    return Operate(left, right, leftVal, rightVal, (a, b) => a + b, symbolTable);
                }
                else if (operatorToken.TokenType == TokenType.MINUS)
                {
                    return Operate(left, right, leftVal, rightVal, (a, b) => a - b, symbolTable);
                }
                else if (operatorToken.TokenType == TokenType.MULTIPLY)
                {
                    return Operate(left, right, leftVal, rightVal, (a, b) => a * b, symbolTable);
                }
                else if (operatorToken.TokenType == TokenType.DIVIDE)
                {
                    return Operate(left, right, leftVal, rightVal, (a, b) => a / b, symbolTable);
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
                    return Compare(left, right, leftVal, rightVal, (a, b) => a > b, symbolTable);
                }
                else if (operatorToken.TokenType == TokenType.GREATER_THAN_OR_EQUAL)
                {
                    return Compare(left, right, leftVal, rightVal, (a, b) => a >= b, symbolTable);
                }
                else if (operatorToken.TokenType == TokenType.LESS_THAN)
                {
                    return Compare(left, right, leftVal, rightVal, (a, b) => a < b, symbolTable);
                }
                else if (operatorToken.TokenType == TokenType.LESS_THAN_OR_EQUAL)
                {
                    return Compare(left, right, leftVal, rightVal, (a, b) => a <= b, symbolTable);
                }
                else if (operatorToken.TokenType == TokenType.EQUAL)
                {
                    return Compare(left, right, leftVal, rightVal, (a, b) => a == b, symbolTable);
                }
                else if (operatorToken.TokenType == TokenType.NOT_EQUAL)
                {
                    return Compare(left, right, leftVal, rightVal, (a, b) => a != b, symbolTable);
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

                object r = Evaluate(actual, symbolTable, functionTable);

                if(prefix!= null && ((AtomicNode)prefix).Token.TokenType ==TokenType.NOT)
                {
                    bool val = (bool)r;
                    return !val;
                }
                else if (prefix!=null && ((AtomicNode)prefix).Token.TokenType == TokenType.MINUS)
                {
                    int val = Int32.Parse(r.ToString());
                    return -val;
                }
                else
                {
                    return r;
                }
            }
            else if(node is IfNode)
            {
                Node condition = ((IfNode)node).Condition;
                List<Node> ifBlock = ((IfNode)node).Statements;
                List<Node> elseBlock = ((IfNode)node).ElseBlock;

                object res = Evaluate(condition, symbolTable, functionTable);
                if(res is bool && (bool)res == true)
                {
                    foreach(Node n in ifBlock)
                    {
                        Evaluate(n, symbolTable, functionTable);
                    }
                    
                }
                else
                {
                    foreach (Node n in elseBlock)
                    {
                        Evaluate(n, symbolTable, functionTable);
                    }
                }

            }
            else if(node is WhileNode)
            {
                Node condition = ((WhileNode)node).Condition;
                List<Node> whileBlock = ((WhileNode)node).Statements;

                while (Evaluate(condition, symbolTable, functionTable) is bool && (bool)Evaluate(condition, symbolTable, functionTable) == true)
                {
                    foreach(Node n in whileBlock)
                    {
                        Evaluate(n, symbolTable, functionTable);
                    }
                }               
            }
            else if(node is FunctionDeclarationNode)
            {
                SymbolTable funcSymTable = new SymbolTable();

                Node fnName = ((FunctionDeclarationNode)node).FunctionName;
                string functionName = ((AtomicNode)fnName).Value.ToString();

                functionTable.Add(functionName, (FunctionDeclarationNode)node);
                List<Node> parameters = ((FunctionDeclarationNode)node).ParameterList;

                foreach(Node n in parameters)
                {
                    funcSymTable.Add(((AtomicNode)n).Value.ToString(), null);
                }

                symbolTable.Add(functionName, funcSymTable);
            }
            else if(node is CallNode)
            {
                Node fnName = ((CallNode)node).m_Name;
                List<Node> parameterValues = ((CallNode)node).m_Arguments;
                string fonName = ((AtomicNode)fnName).Value.ToString();

                SymbolTable symTable = (SymbolTable)symbolTable.Get(fonName);

                for(int i = 0; i< parameterValues.Count; i++)
                {
                    object nodeVal = ((AtomicNode)parameterValues[i]).Value;

                    DictionaryEntry de = symTable.m_Variables.Cast<DictionaryEntry>().ElementAt(i);
                    de.Value = nodeVal;
                    symTable.Add(de.Key.ToString(), de.Value);
                }


                symbolTable = symTable;

                FunctionDeclarationNode fnBlock = functionTable.Get(fonName);
                List<Node> statements = fnBlock.Statements;

                foreach(Node stat in statements)
                {
                    if(stat is ReturnNode)
                    {
                        return Evaluate(stat, symbolTable, functionTable);
                    }

                    Evaluate(stat, symbolTable, functionTable);
                }
            }
            else if(node is ReturnNode)
            {
                ReturnNode retNode = ((ReturnNode)node);

                if(retNode.ToBeReturned is AtomicNode)
                {
                    if (((AtomicNode)retNode.ToBeReturned).Token.TokenType == TokenType.VARIABLE)
                    {
                        return symbolTable.Get(((AtomicNode)retNode.ToBeReturned).Value.ToString());
                    }
                    else
                    {
                        return ((AtomicNode)retNode.ToBeReturned).Value;
                    }
                }
                else if(retNode.ToBeReturned is BinaryOperatorNode)
                {
                    return Evaluate(retNode.ToBeReturned, symbolTable, functionTable);
                }
                
            }

            return null;
        }

        bool Compare(Node left, Node right, object leftVal, object rightVal, Func<int, int, bool> compare, SymbolTable symbolTable)
        {
            int leftInt, rightInt;
            if(Int32.TryParse(leftVal.ToString(), out leftInt) && Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                return CompareInteger(Int32.Parse(leftVal.ToString()), Int32.Parse(rightVal.ToString()), compare);
            }
            //left only is integer
            else if(Int32.TryParse(leftVal.ToString(), out leftInt) && !Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                int rightVal__ = Int32.Parse(symbolTable.Get(((AtomicNode)right).Value.ToString()).ToString());
                return CompareInteger(Int32.Parse(leftVal.ToString()), rightVal__, compare);
            }
            //right only is integer
            else if (!Int32.TryParse(leftVal.ToString(), out leftInt) && Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                int leftVal__ = Int32.Parse(symbolTable.Get(((AtomicNode)left).Value.ToString()).ToString());
                return CompareInteger(leftVal__, Int32.Parse(rightVal.ToString()), compare);
            }
            //no integer, means both are variables
            else if (!Int32.TryParse(leftVal.ToString(), out leftInt) && !Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                int leftVal__ = Int32.Parse(symbolTable.Get(((AtomicNode)left).Value.ToString()).ToString());
                int rightVal__ = Int32.Parse(symbolTable.Get(((AtomicNode)right).Value.ToString()).ToString());
                return CompareInteger(leftVal__, rightVal__, compare);
            }
            else
            {
                throw new InvalidOperationException("Compare error");
            }
        }

        int Operate(Node left, Node right, object leftVal, object rightVal, Func<int, int, int> operate, SymbolTable symbolTable)
        {
            int leftInt, rightInt;
            if (Int32.TryParse(leftVal.ToString(), out leftInt) && Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                return OperateMath(Int32.Parse(leftVal.ToString()), Int32.Parse(rightVal.ToString()), operate);
            }
            //left only is integer
            else if (Int32.TryParse(leftVal.ToString(), out leftInt) && !Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                int rightVal__ = Int32.Parse(symbolTable.Get(((AtomicNode)right).Value.ToString()).ToString());
                return OperateMath(Int32.Parse(leftVal.ToString()), rightVal__, operate);
            }
            //right only is integer
            else if (!Int32.TryParse(leftVal.ToString(), out leftInt) && Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                int leftVal__ = Int32.Parse(symbolTable.Get(((AtomicNode)left).Value.ToString()).ToString());
                return OperateMath(leftVal__, Int32.Parse(rightVal.ToString()), operate);
            }
            //no integer, means both are variables
            else if (!Int32.TryParse(leftVal.ToString(), out leftInt) && !Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                int leftVal__ = Int32.Parse(symbolTable.Get(((AtomicNode)left).Value.ToString()).ToString());
                int rightVal__ = Int32.Parse(symbolTable.Get(((AtomicNode)right).Value.ToString()).ToString());
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
