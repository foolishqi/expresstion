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

            Assert.Collection(resolver.OperandsStack,
                x => Assert.Equal("1", x));
        }

        [Fact]
        public void AnotherConstTest()
        {
            resolver = new ExpressionResolver("2");

            Assert.Collection(resolver.OperandsStack,
                x => Assert.Equal("2", x));
        }

        [Fact]
        public void MultiConstTest()
        {
            resolver = new ExpressionResolver("1 2 3");

            Assert.Collection(resolver.OperandsStack,
                x => Assert.Equal("3", x),
                x => Assert.Equal("2", x),
                x => Assert.Equal("1", x));
        }

        // [Fact]
        // public void OnePlusOneTest()
        // {
        //     resolver = new ExpressionResolver("1 + 1");

        //     Assert.Equal(3, resolver.OperandsStack.Count);
        //     Assert.Collection(resolver.OperandsStack,
        //         x => Assert.Equal("1", x),
        //         x => Assert.Equal("+", x),
        //         x => Assert.Equal("1", x));
        // }
    }
}
