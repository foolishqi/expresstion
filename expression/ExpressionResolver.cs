using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace expression
{
    public class ExpressionResolver
    {
        public ExpressionResolver()
        {
        }

        public List<String> Resolve(string text)
        {
            ExpressionReader reader = new ExpressionReader(text);

            // 1. 初始化一个空堆栈，将结果字符串变量置空。
            Stack<String> operatorsStack = new Stack<String>();
            List<String> expressions = new List<String>();

            string item = string.Empty;
            // 2. 从左到右读入中缀表达式，每次一个字符。
            while ((item = reader.Read()) != string.Empty)
            {
                var type = GetExpressionType(item);

                // 3. 如果字符是操作数，将它添加到结果字符串。
                if (type == ExpressionType.Operand)
                {
                    expressions.Add(item);
                }
                else if (type == ExpressionType.Operators || type == ExpressionType.Function)
                {
                    // 5. 如果字符是个开括号，把它压入堆栈。
                    if (IsOpeningParenthesis(item))
                    {
                        operatorsStack.Push(item);
                    }
                    else
                    {
                        var precedenceOfCurrent = GetOperatorPrecedence(item);

                        while (operatorsStack.Count > 0)
                        {
                            var operatorOnStack = operatorsStack.Peek();
                            var precedenceOfStack = GetOperatorPrecedence(operatorOnStack);

                            if (
                                // 4. 如果字符是个操作符，弹出（pop）操作符，直至遇见开括号（opening parenthesis）、
                                //    优先级较低的操作符或者同一优先级的右结合符号。把这个操作符压入（push）堆栈。
                                !(IsOpeningParenthesis(operatorOnStack) || precedenceOfCurrent < precedenceOfStack) ||
                                // 6. 如果字符是个闭括号（closing parenthesis），在遇见开括号前，弹出所有操作符，
                                //    然后把它们添加到结果字符串。
                                (IsClosingParenthesis(item) && !IsOpeningParenthesis(operatorOnStack))
                            )
                            {
                                expressions.Add(operatorsStack.Pop());
                            }
                            else
                            {
                                if (IsClosingParenthesis(item) && IsOpeningParenthesis(operatorOnStack))
                                {
                                    operatorsStack.Pop();
                                }
                                break;
                            }
                        }

                        if (!IsClosingParenthesis(item))
                        {
                            operatorsStack.Push(item);
                        }
                    }
                }
            }

            // 7. 如果到达输入字符串的末尾，弹出所有操作符并添加到结果字符串。
            foreach (var operators in operatorsStack)
            {
                expressions.Add(operators);
            }

            return expressions;
        }

        private ExpressionType GetExpressionType(string input)
        {
            if (Regex.IsMatch(input, @"(?:-?\d+\.?\d*)|(?:\"".*?\"")"))
            {
                return ExpressionType.Operand;
            }
            else if (Regex.IsMatch(input, @"(?:\(|\)|!|\*|/|%|\+|-|<=?|>=?|==|!=|&|\|)"))
            {
                return ExpressionType.Operators;
            }
            else if (Regex.IsMatch(input, @"(?:[A-Za-z_]+)"))
            {
                return ExpressionType.Function;
            }
            else
            {
                throw new ArgumentOutOfRangeException(
                    nameof(input), input, "未知的表达式类型."
                );
            }
        }

        private int GetOperatorPrecedence(string operators)
        {
            int precedence;

            switch (operators)
            {
                case "(":
                case ")":
                    precedence = 1;
                    break;
                case "!":
                    precedence = 2;
                    break;
                case "*":
                case "/":
                case "%":
                    precedence = 3;
                    break;
                case "+":
                case "-":
                    precedence = 4;
                    break;
                case "<":
                case "<=":
                case ">":
                case ">=":
                    precedence = 6;
                    break;
                case "==":
                case "!=":
                    precedence = 7;
                    break;
                case "&":
                    precedence = 11;
                    break;
                case "|":
                    precedence = 12;
                    break;
                default:
                    precedence = 20;
                    break;
            }

            return precedence;
        }

        private bool IsOpeningParenthesis(string item)
        {
            return "(".Equals(item);
        }

        private bool IsClosingParenthesis(string item)
        {
            return ")".Equals(item);
        }

        public enum ExpressionType
        {
            Operand,
            Operators,
            Function
        }
    }
}