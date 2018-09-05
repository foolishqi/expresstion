using System;
using Xunit;
using expression;

namespace expression.tests
{
    public class ExpressionResolverTest
    {
        private ExpressionResolver resolver;

        public ExpressionResolverTest()
        {
        }

        [Fact]
        public void EmptyTest()
        {
            resolver = new ExpressionResolver("");

            Assert.Empty(resolver.OperandsStack);
        }

        [Fact]
        public void ConstTest()
        {
            resolver = new ExpressionResolver("1");

            Assert.Equal(1, resolver.OperandsStack.Count);
            Assert.Collection(resolver.OperandsStack,
                x => Assert.IsType<ConstExpression>(x));
        }

        [Fact]
        public void OnePlusOneTest()
        {
            resolver = new ExpressionResolver("1 + 1");

            Assert.Equal(3, resolver.OperandsStack.Count);
            Assert.Collection(resolver.OperandsStack,
                x => Assert.IsType<ConstExpression>(x),
                x => Assert.IsType<AdditionExpression>(x),
                x => Assert.IsType<ConstExpression>(x));
        }
    }
}
