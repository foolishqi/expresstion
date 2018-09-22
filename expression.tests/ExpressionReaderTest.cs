using Xunit;

namespace expression.tests
{
    public class ExpressionReaderTest
    {
        private ExpressionReader reader;

        [Fact]
        // 空测试
        public void EmptyTest()
        {
            reader = new ExpressionReader("");

            Assert.Equal("", reader.Read());
        }

        [Fact]
        // 整数测试
        public void NumberTest()
        {
            reader = new ExpressionReader("1");

            Assert.Equal("1", reader.Read());
        }

        [Fact]
        // 多位整数测试
        public void MultiBitNumberTest()
        {
            reader = new ExpressionReader("123");

            AssertItemsEqual("123");
        }

        [Fact]
        // 小数测试
        public void DecimalTest()
        {
            reader = new ExpressionReader("3.1415926");

            AssertItemsEqual("3.1415926");
        }

        [Fact]
        // 负数测试
        public void NegativeNumberTest()
        {
            reader = new ExpressionReader("-1");

            AssertItemsEqual("-1");
        }

        [Fact]
        // 空字符串测试
        public void EmptyStringTest()
        {
            reader = new ExpressionReader("\"\"");

            AssertItemsEqual("\"\"");
        }

        [Fact]
        // 字符串测试
        public void StringTest()
        {
            reader = new ExpressionReader("\"test\"");

            AssertItemsEqual("\"test\"");
        }

        [Fact]
        // 数字字符串测试
        public void NumberStringTest()
        {
            reader = new ExpressionReader("\"123\"");

            AssertItemsEqual("\"123\"");
        }

        [Fact]
        // 日期字符串测试
        public void DateStringTest()
        {
            reader = new ExpressionReader("\"2018-10-01 12:00:00\"");

            AssertItemsEqual("\"2018-10-01 12:00:00\"");
        }

        [Fact]
        // 拼接字符串(回溯)测试
        public void ConcatStringTest()
        {
            reader = new ExpressionReader("\"hello\" + \"world.\"");

            AssertItemsEqual("\"hello\"", "+", "\"world.\"");
        }

        [Fact]
        // 加法测试
        public void OnePlusOneTest()
        {
            reader = new ExpressionReader("1 + 1");

            AssertItemsEqual("1", "+", "1");
        }

        [Fact]
        // 不含空格的加法测试
        public void OnePlusOneWithSpaceTest()
        {
            reader = new ExpressionReader("1+1");

            AssertItemsEqual("1", "+", "1");
        }

        [Fact]
        // 负数的加法测试
        public void NegativePlusTest()
        {
            reader = new ExpressionReader("1+-1");

            AssertItemsEqual("1", "+", "-1");
        }

        [Fact]
        // 算术运算符测试
        public void ArithmeticOperationSpliceTest()
        {
            reader = new ExpressionReader("1 + 2 - 3 * 4 / 5 % 6");

            AssertItemsEqual("1", "+", "2", "-", "3", "*", "4", "/", "5", "%", "6");
        }

        [Fact]
        // 比较运算符分割测试
        public void CompareOperationSpliceTest()
        {
            reader = new ExpressionReader("1 < 2");

            AssertItemsEqual("1", "<", "2");
        }

        [Fact]
        // 大于等于测试
        public void DoubleCompareOperationSpliceTest()
        {
            reader = new ExpressionReader("2 >= 1");

            AssertItemsEqual("2", ">=", "1");
        }

        [Fact]
        // 逗号分割测试
        public void CommaSpliceTest()
        {
            reader = new ExpressionReader("1,2, 3");

            AssertItemsEqual("1", "2", "3");
        }

        [Fact]
        // 空格分割测试
        public void SpaceSpliceTest()
        {
            reader = new ExpressionReader("1 2  3");

            AssertItemsEqual("1", "2", "3");
        }

        [Fact]
        // 括号测试
        public void BracketsTest()
        {
            reader = new ExpressionReader("1 + (2 + 3)");

            AssertItemsEqual("1", "+", "(", "2", "+", "3", ")");
        }

        [Fact]
        // 含负数的括号测试
        public void BracketsWithNegativeTest()
        {
            reader = new ExpressionReader("1 + (-2 + 3)");

            AssertItemsEqual("1", "+", "(", "-2", "+", "3", ")");
        }

        [Fact]
        // 函数测试
        public void FunctionTest()
        {
            reader = new ExpressionReader("Now()");

            AssertItemsEqual("Now", "(", ")");
        }

        [Fact]
        // 含参数的函数测试
        public void FunctionWithParametersTest()
        {
            reader = new ExpressionReader("Add(1, 2)");

            AssertItemsEqual("Add", "(", "1", "2", ")");
        }

        [Fact]
        // 嵌套函数测试
        public void NestedFunctionTest()
        {
            reader = new ExpressionReader("Add(1, Add(2, 3)");

            AssertItemsEqual("Add", "(", "1", "Add", "(", "2", "3", ")");
        }

        private void AssertItemsEqual(params string[] items)
        {
            foreach (var item in items)
            {
                Assert.Equal(item, reader.Read());
            }
        }
    }
}
