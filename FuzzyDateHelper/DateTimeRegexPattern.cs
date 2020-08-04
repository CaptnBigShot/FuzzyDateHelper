using System;
using System.Text.RegularExpressions;

namespace FuzzyDateHelper
{
    internal class DateTimeRegexPattern
    {
        public delegate DateTime Interpreter(Match m);

        private readonly Regex _regex;

        private readonly Interpreter _interpreter;

        public DateTimeRegexPattern(string regex, Interpreter interpreter)
        {
            _regex = new Regex(regex);
            _interpreter = interpreter;
        }

        public DateTime Parse(string text)
        {
            var match = _regex.Match(text);

            return match.Success ? _interpreter(match) : DateTime.MinValue;
        }
    }
}