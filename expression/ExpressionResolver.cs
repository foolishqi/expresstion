using System;
using System.Collections.Generic;
using System.IO;

namespace expression
{
    public class ExpressionResolver
    {
        private string text;
        private Stack<String> operandsStack;
        private Stack<String> operationsStack;

        public ExpressionResolver(string text)
        {
            this.text = text;
            this.operandsStack = new Stack<String>();
            this.operationsStack = new Stack<String>();

            this.Resolve();
        }

        public Stack<String> OperandsStack => this.operandsStack;

        public void Resolve()
        {
            if (!string.IsNullOrEmpty(this.text))
            {
                foreach (var item in this.text.Split(' '))
                {
                    this.operandsStack.Push(item);
                }
            }

            // StringReader reader = new StringReader(this.text);
            // var currentItem = string.Empty;
            // char currentChar;

            // do
            // {
            //     currentChar = (char)reader.Read();

            //     if (currentChar == '+')
            //     {
            //         this.operationsStack.Push(currentChar);
            //     }
            // } while (currentChar != -1);

            // if (!string.IsNullOrEmpty(text))
            // {
            //     this.OperandsStack.Push("1");

            //     if (text.Contains("+"))
            //     {
            //         this.OperandsStack.Push("+");
            //         this.OperandsStack.Push("1");
            //     }
            // }
        }
    }
}