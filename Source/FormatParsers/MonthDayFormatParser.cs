using System;
using System.Text.RegularExpressions;

namespace Exceptionless.DateTimeExtensions.FormatParsers {
    [Priority(50)]
    public class MonthDayFormatParser : IFormatParser {
        private static readonly Regex _parser = new Regex(@"^\s*(?<month>\d{2})-(?<day>\d{2})\s*$");

        public DateTimeRange Parse(string content, DateTime now) {
            var m = _parser.Match(content);
            if (!m.Success)
                return null;

            int month = Int32.Parse(m.Groups["month"].Value);
            int day = Int32.Parse(m.Groups["day"].Value);
            try {
                return new DateTimeRange(now.Date.ChangeMonth(month).ChangeDay(day), now.Date.ChangeMonth(month).ChangeDay(day).EndOfDay());
            } catch {
                return null;
            }
        }
    }
}