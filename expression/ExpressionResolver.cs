using System;
using System.Collections.Generic;

namespace expression
{
    public class ExpressionResolver
    {
        private string text;

        public ExpressionResolver(string text)
        {
            this.text = text;
            this.OperandsStack = new Stack<IExpression>();

            this.Resolve();
        }

        public Stack<IExpression> OperandsStack { get; private set; }

        public void Resolve()
        {
            if (!string.IsNullOrEmpty(text))
            {
                this.OperandsStack.Push(new ConstExpression());
                this.OperandsStack.Push(new AdditionExpression());
                this.OperandsStack.Push(new ConstExpression());
            }
        }
    }
}