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
            this.resolver = new ExpressionResolver();
        }

        [Fact]
        public void EmptyTest()
        {
            var expressions = resolver.Resolve("");

            Assert.Empty(expressions);
        }

        [Fact]
        // 数值常量测试
        public void NumberConstTest()
        {
            var expressions = resolver.Resolve("123");

            Assert.Collection(expressions,
                x => Assert.Equal("123", x));
        }

        [Fact]
        // 字符串常量测试
        public void StringConstTest()
        {
            var expressions = resolver.Resolve("\"hello world.\"");

            Assert.Collection(expressions,
                x => Assert.Equal("\"hello world.\"", x));
        }

        [Fact]
        // 算术运算测试
        public void ArithmeticOperationTest()
        {
            var expressions = resolver.Resolve("1 + 1");

            Assert.Equal(3, expressions.Count);
            Assert.Collection(expressions,
                x => Assert.Equal("1", x),
                x => Assert.Equal("1", x),
                x => Assert.Equal("+", x));
        }

        [Fact]
        // 优先级测试
        public void PrecedenceTest()
        {
            var expressions = resolver.Resolve("1 + 2 * 3 - 4");

            Assert.Collection(expressions,
                x => Assert.Equal("1", x),
                x => Assert.Equal("2", x),
                x => Assert.Equal("3", x),
                x => Assert.Equal("*", x),
                x => Assert.Equal("+", x),
                x => Assert.Equal("4", x),
                x => Assert.Equal("-", x));
        }

        [Fact]
        // 结合性测试
        public void AssociativityTest()
        {
            var expressions = resolver.Resolve("1 + 2 - 3 + 4");

            Assert.Collection(expressions,
                x => Assert.Equal("1", x),
                x => Assert.Equal("2", x),
                x => Assert.Equal("+", x),
                x => Assert.Equal("3", x),
                x => Assert.Equal("-", x),
                x => Assert.Equal("4", x),
                x => Assert.Equal("+", x));
        }

        [Fact]
        // 括号测试
        public void ParenthesisTest()
        {
            var expressions = resolver.Resolve("1 * (2 + 3)");

            Assert.Collection(expressions,
                x => Assert.Equal("1", x),
                x => Assert.Equal("2", x),
                x => Assert.Equal("3", x),
                x => Assert.Equal("+", x),
                x => Assert.Equal("*", x));
        }

        [Fact]
        // 嵌套括号测试
        public void NestedParenthesisTest()
        {
            var expressions = resolver.Resolve("(1 + 2) * ((3 + 4) - 5)");

            Assert.Collection(expressions,
                x => Assert.Equal("1", x),
                x => Assert.Equal("2", x),
                x => Assert.Equal("+", x),
                x => Assert.Equal("3", x),
                x => Assert.Equal("4", x),
                x => Assert.Equal("+", x),
                x => Assert.Equal("5", x),
                x => Assert.Equal("-", x),
                x => Assert.Equal("*", x));
        }

        [Fact]
        // 函数测试
        public void FunctionTest()
        {
            var expressions = resolver.Resolve("Add(1, 2)");

            Assert.Collection(expressions,
                x => Assert.Equal("1", x),
                x => Assert.Equal("2", x),
                x => Assert.Equal("Add", x));
        }

        [Fact]
        // 嵌套函数测试
        public void NestedFunctionTest()
        {
            var expressions = resolver.Resolve("Datediff(\"2018-10-01\", Now())");

            Assert.Collection(expressions,
                x => Assert.Equal("\"2018-10-01\"", x),
                x => Assert.Equal("Now", x),
                x => Assert.Equal("Datediff", x));
        }
    }
}
