using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProgrammingLanguage.LexicalAnalysis;
using ProgrammingLanguage.SyntaxAnalysis.Nodes;

namespace ProgrammingLanguage.Interpreter
{
    internal class Evaluator
    {
        //###################################################################################
        #region Internal Methods

        internal void Evaluate(ProgramNode programNode)
        {
            List<object> values = new List<object>();
            FunctionTable functionTable = new FunctionTable();
            SymbolTable symbolTable = new SymbolTable(null);

            foreach (Node node in programNode.Statements)
            {
                Eval(node, symbolTable, functionTable);
            }
        }

        #endregion

        //###################################################################################
        #region Private Methods

        private object Eval(Node node, SymbolTable symbolTable, FunctionTable functionTable)
        {
            if (node is OperatorNode)
            {
                string value = ((OperatorNode)node).OperatorValue;
                return value;
            }
            if (node is VariableNode)
            {
                string value = ((VariableNode)node).VariableName;
                return symbolTable.Get(value);
            }
            else if (node is VariableDeclarationNode)
            {
                string varName = ((VariableDeclarationNode)node).VariableName;
                object rhv = ((VariableDeclarationNode)node).RightHandValue;
                object value = Eval((Node)rhv, symbolTable, functionTable);

                symbolTable.Add(varName, value);

                return null;
            }
            else if (node is WhileNode)
            {
                Node condition = ((WhileNode)node).Condition;
                List<Node> whileBlock = ((WhileNode)node).Statements;

                while (Eval(condition, symbolTable, functionTable) is bool && (bool)Eval(condition, symbolTable, functionTable) == true)
                {
                    EvalStatementsInList(whileBlock, symbolTable, functionTable);
                }
            }
            else if (node is IfNode)
            {
                Node condition = ((IfNode)node).Condition;
                List<Node> ifBlock = ((IfNode)node).Statements;
                List<Node> elseBlock = ((IfNode)node).ElseBlock;

                object conditionResult = Eval(condition, symbolTable, functionTable);
                if (conditionResult is bool && (bool)conditionResult == true)
                {
                    return EvalStatementsInList(ifBlock, symbolTable, functionTable);
                }
                else if(conditionResult is bool && (bool)conditionResult == false)
                {
                    return EvalStatementsInList(elseBlock, symbolTable, functionTable);
                }
                else
                {
                    throw new InvalidOperationException("Evaluation error in IfNode");
                }
            }
            else if (node is UnaryNode)
            {
                Node prefix = ((UnaryNode)node).PrefixNode;
                Node actual = ((UnaryNode)node).Node;

                object actualNodeResult = Eval(actual, symbolTable, functionTable);

                if (prefix != null && ((OperatorNode)prefix).Token.TokenType == TokenType.NOT)
                {
                    bool val = (bool)actualNodeResult;
                    return !val;
                }
                else if (prefix != null && ((OperatorNode)prefix).Token.TokenType == TokenType.MINUS)
                {
                    int val = Int32.Parse(actualNodeResult.ToString());
                    return -val;
                }
                else
                {
                    return actualNodeResult;
                }
            }
            else if (node is ReturnNode)
            {
                ReturnNode retNode = ((ReturnNode)node);
                return Eval(retNode.ToBeReturned, symbolTable, functionTable);
            }
            else if (node is FunctionDeclarationNode)
            {
                SymbolTable funcSymTable = new SymbolTable(symbolTable);
                string functionName = ((FunctionDeclarationNode)node).FunctionName;

                functionTable.Add(functionName, (FunctionDeclarationNode)node);

                List<Node> parameters = ((FunctionDeclarationNode)node).ParameterList;
                foreach (Node parameter in parameters)
                {
                    funcSymTable.Add(((VariableNode)parameter).VariableName, null);
                }

                symbolTable.Add(functionName, funcSymTable);
            }
            else if (node is BinaryExpressionNode)
            {
                Node left = ((BinaryExpressionNode)node).Left;
                object leftVal = Eval(left, symbolTable, functionTable);

                Node oper = ((BinaryExpressionNode)node).Operator;
                Token operatorToken = ((OperatorNode)oper).Token;

                Node right = ((BinaryExpressionNode)node).Right;
                object rightVal = Eval(right, symbolTable, functionTable);

                if (operatorToken.TokenType == TokenType.PLUS)
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
            else if(node is CallNode)
            {
                try
                {


                    string functionName = ((CallNode)node).FunctionName;
                    List<Node> parameterValues = ((CallNode)node).Arguments;

                    SymbolTable localSymbolTable = (SymbolTable)symbolTable.Get(functionName);

                    FunctionDeclarationNode fnBlock = functionTable.Get(functionName);


                    if (fnBlock.ParameterList.Count != parameterValues.Count)
                    {
                        throw new InvalidOperationException("Number of parameters in function and function call does not match");
                    }

                    for (int i = 0; i < parameterValues.Count; i++)
                    {
                        object nodeVal = Eval(parameterValues[i], symbolTable, functionTable);

                        DictionaryEntry dictEntry = localSymbolTable.m_Variables.Cast<DictionaryEntry>().ElementAt(i);
                        dictEntry.Value = nodeVal;
                        localSymbolTable.Add(dictEntry.Key.ToString(), dictEntry.Value);
                    }

                    symbolTable = localSymbolTable;

                    List<Node> statements = fnBlock.Statements;

                    foreach (Node stat in statements)
                    {
                        if (stat is ReturnNode)
                        {
                            object result = Eval(stat, symbolTable, functionTable);
                            throw new ReturnException("Returned", result);
                        }
                        else
                        {
                            Eval(stat, symbolTable, functionTable);
                        }
                    }
                }
                catch(ReturnException returnException)
                {
                    return returnException.Value;
                }
            }
            else if(node is PrintNode)
            {
                Node toBePrinted = ((PrintNode)node).ToBePrinted;
                object result = Eval(toBePrinted, symbolTable, functionTable);
                string printResult = result.ToString();

                if(result is bool)
                {
                    printResult = NormalizeBooleanOutput((bool)result);
                }

                Console.WriteLine(printResult);

                return result;
            }
            else if(node is BooleanNode)
            {
                return ((BooleanNode)node).Value;
            }
            else if (node is NumberNode)
            {
                return ((NumberNode)node).Value;
            }
            else if (node is StringNode)
            {
                return ((StringNode)node).Value;
            }

            return null;
        }

        private string NormalizeBooleanOutput(bool value)
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

        private object EvalStatementsInList(List<Node> nodes, SymbolTable symbolTable, FunctionTable functionTable)
        {
            foreach (Node node in nodes)
            {
                if(node is ReturnNode)
                {
                    object result = Eval(node, symbolTable, functionTable);
                    throw new ReturnException("Returned", result);
                }
                else
                {
                    Eval(node, symbolTable, functionTable);
                }
                
            }
            return null;
        }

        private bool CompareInteger(int input1, int input2, Func<int, int, bool> comparisonOperator)
        {
            return comparisonOperator(input1, input2);
        }

        private int OperateMath(int input1, int input2, Func<int, int, int> comparisonOperator)
        {
            return comparisonOperator(input1, input2);
        }

        private bool Compare(Node left, Node right, object leftVal, object rightVal, Func<int, int, bool> compare, SymbolTable symbolTable)
        {
            int leftInt, rightInt;
            if (left is StringNode && right is StringNode)
            {
                return leftVal.ToString() == rightVal.ToString();
            }
            else if (Int32.TryParse(leftVal.ToString(), out leftInt) && Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                return CompareInteger(Int32.Parse(leftVal.ToString()), Int32.Parse(rightVal.ToString()), compare);
            }
            //left only is integer
            else if (Int32.TryParse(leftVal.ToString(), out leftInt) && !Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                int rightVal__ = Int32.Parse(symbolTable.Get(((NumberNode)right).Value.ToString()).ToString());
                return CompareInteger(Int32.Parse(leftVal.ToString()), rightVal__, compare);
            }
            //right only is integer
            else if (!Int32.TryParse(leftVal.ToString(), out leftInt) && Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                int leftVal__ = Int32.Parse(symbolTable.Get(((NumberNode)left).Value.ToString()).ToString());
                return CompareInteger(leftVal__, Int32.Parse(rightVal.ToString()), compare);
            }
            //no integer, means both are variables
            else if (!Int32.TryParse(leftVal.ToString(), out leftInt) && !Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                return ComparetringOrInteger(left, right, compare, symbolTable);
            }
            else
            {
                throw new InvalidOperationException("Compare error");
            }
        }

        private bool ComparetringOrInteger(Node left, Node right, Func<int, int, bool> compare, SymbolTable symbolTable)
        {
            if(left is NumberNode && right is NumberNode)
            {
                int leftVal__ = Int32.Parse(symbolTable.Get(((NumberNode)left).Value.ToString()).ToString());
                int rightVal__ = Int32.Parse(symbolTable.Get(((NumberNode)right).Value.ToString()).ToString());
                return CompareInteger(leftVal__, rightVal__, compare);
            }
            if(left is StringNode && right is StringNode)
            {
                int leftVal__ = Int32.Parse(symbolTable.Get(((StringNode)left).Value.ToString()).ToString());
                int rightVal__ = Int32.Parse(symbolTable.Get(((StringNode)right).Value.ToString()).ToString());
                return CompareInteger(leftVal__, rightVal__, compare);
            }
            else
            {
                return false;
            }
        }

        private int Operate(Node left, Node right, object leftVal, object rightVal, Func<int, int, int> operate, SymbolTable symbolTable)
        {
            int leftInt, rightInt;
            if (Int32.TryParse(leftVal.ToString(), out leftInt) && Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                return OperateMath(Int32.Parse(leftVal.ToString()), Int32.Parse(rightVal.ToString()), operate);
            }
            //left only is integer
            else if (Int32.TryParse(leftVal.ToString(), out leftInt) && !Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                int rightVal__ = Int32.Parse(symbolTable.Get(((NumberNode)right).Value.ToString()).ToString());
                return OperateMath(Int32.Parse(leftVal.ToString()), rightVal__, operate);
            }
            //right only is integer
            else if (!Int32.TryParse(leftVal.ToString(), out leftInt) && Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                int leftVal__ = Int32.Parse(symbolTable.Get(((NumberNode)left).Value.ToString()).ToString());
                return OperateMath(leftVal__, Int32.Parse(rightVal.ToString()), operate);
            }
            //no integer, means both are variables
            else if (!Int32.TryParse(leftVal.ToString(), out leftInt) && !Int32.TryParse(rightVal.ToString(), out rightInt))
            {
                int leftVal__ = Int32.Parse(symbolTable.Get(((NumberNode)left).Value.ToString()).ToString());
                int rightVal__ = Int32.Parse(symbolTable.Get(((NumberNode)right).Value.ToString()).ToString());
                return OperateMath(leftVal__, rightVal__, operate);
            }
            else
            {
                throw new InvalidOperationException("Compare error");
            }
        }

        #endregion
    }
}
