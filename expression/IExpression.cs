using System.Collections.Generic;

namespace expression
{
    public interface IExpression
    {
        object Evaluate(Dictionary<string, object> variables);
    }
}