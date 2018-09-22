using System;
using System.Text.RegularExpressions;

namespace expression
{
    public class ExpressionReader
    {
        private static string[] patterns = new string[] {
            @"(?:-?\d+\.?\d*)",
            @"(?:\"".*?\"")",
            @"(?:\(|\)|!|\*|/|%|\+|-|<=?|>=?|==|!=|&|\|)",
            @"(?:[A-Za-z_]+(?=\(.*\)))"
        };

        private string text;
        private Match match;

        public ExpressionReader(string text)
        {
            this.text = text;
        }

        public string Read()
        {
            match = match != null 
                ? match.NextMatch()
                : Regex.Match(this.text, String.Join('|', patterns));

            if (!match.Success && !string.IsNullOrEmpty(this.text))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(this.text), this.text, "无法检索的表达式.");
            }

            return match.Value;
        }
    }
}