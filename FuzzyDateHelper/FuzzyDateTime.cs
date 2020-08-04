using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FuzzyDateHelper
{
    public class FuzzyDateTime
    {

        #region Patterns

        #region Years

        private static readonly DateTimeRegexPattern LastOrNextYearPattern = new DateTimeRegexPattern
        (
            @"(last|next) year$",
            delegate (Match m)
            {
                var direction = m.Groups[1].Value;

                return FuzzyDateTimeCore.LastOrNextYear(direction);
            }
        );

        private static readonly DateTimeRegexPattern XYearsAgoPattern = new DateTimeRegexPattern
        (
            @"^^([0-9]\d*) years ago$",
            delegate (Match m)
            {
                var numOfYears = int.Parse(m.Groups[1].Value);

                return FuzzyDateTimeCore.XYearsAgo(numOfYears);
            }
        );

        private static readonly DateTimeRegexPattern NextXYearsPattern = new DateTimeRegexPattern
        (
            @"^next ([0-9]\d*) years$",
            delegate (Match m)
            {
                var numOfYears = int.Parse(m.Groups[1].Value);

                return FuzzyDateTimeCore.NextXYears(numOfYears);
            }
        );

        private static readonly DateTimeRegexPattern XYearsFromTodayPattern = new DateTimeRegexPattern
        (
            @"^(-)?([0-9]\d*) years from today$",
            delegate (Match m)
            {
                var isNegative = m.Groups[1].Value == "-";
                var numOfYears = int.Parse(m.Groups[2].Value) * (isNegative ? -1 : 1);

                return FuzzyDateTimeCore.XYearsFromToday(numOfYears);
            }
        );

        #endregion


        #region Months

        private static readonly DateTimeRegexPattern LastMonthPattern = new DateTimeRegexPattern
        (
            @"^last month$",
            delegate (Match m) { return FuzzyDateTimeCore.LastMonth(); }
        );

        private static readonly DateTimeRegexPattern NextMonthPattern = new DateTimeRegexPattern
        (
            @"^next month$",
            delegate (Match m) { return FuzzyDateTimeCore.NextMonth(); }
        );

        private static readonly DateTimeRegexPattern XMonthsAgoPattern = new DateTimeRegexPattern
        (
            @"^([0-9]\d*) months ago$",
            delegate (Match m)
            {
                var numOfMonths = int.Parse(m.Groups[1].Value);

                return FuzzyDateTimeCore.XMonthsAgo(numOfMonths);
            }
        );

        private static readonly DateTimeRegexPattern NextXMonthsPattern = new DateTimeRegexPattern
        (
            @"^next ([0-9]\d*) months$",
            delegate (Match m)
            {
                var numOfMonths = int.Parse(m.Groups[1].Value);

                return FuzzyDateTimeCore.NextXMonths(numOfMonths);
            }
        );

        private static readonly DateTimeRegexPattern XMonthsFromTodayPattern = new DateTimeRegexPattern
        (
            @"^(-)?([0-9]\d*) months from today$",
            delegate (Match m)
            {
                var isNegative = m.Groups[1].Value == "-";
                var numOfMonths = int.Parse(m.Groups[2].Value) * (isNegative ? -1 : 1);

                return FuzzyDateTimeCore.XMonthsFromToday(numOfMonths);
            }
        );

        private static readonly DateTimeRegexPattern FirstOrLastOfTheMonthXMonthsAgoPattern = new DateTimeRegexPattern
        (
            @"^(first|last) of the month ([0-9]\d*) months ago$",
            delegate (Match m)
            {
                var timeOfMonth = m.Groups[1].Value;
                var numOfMonths = int.Parse(m.Groups[2].Value);

                return FuzzyDateTimeCore.FirstOrLastOfTheMonthXMonthsAgo(timeOfMonth, numOfMonths);
            }
        );

        private static readonly DateTimeRegexPattern FirstOrLastOfTheMonthNextXMonthsPattern = new DateTimeRegexPattern
        (
            @"^(first|last) of the month next ([0-9]\d*) months$",
            delegate (Match m)
            {
                var timeOfMonth = m.Groups[1].Value;
                var numOfMonths = int.Parse(m.Groups[2].Value);

                return FuzzyDateTimeCore.FirstOrLastOfTheMonthNextXMonths(timeOfMonth, numOfMonths);
            }
        );

        private static readonly DateTimeRegexPattern XDaysBeforeOrAfterTheFirstOrLastOfTheNextXMonthsPattern =
            new DateTimeRegexPattern
            (
                @"^([0-9]\d*) days (before|after) the (first|last) of the next ([0-9]\d*) months$",
                delegate (Match m)
                {
                    var numOfDays = int.Parse(m.Groups[1].Value);
                    var daysDirection = m.Groups[2].Value;
                    var timeOfMonth = m.Groups[3].Value;
                    var numOfMonths = int.Parse(m.Groups[4].Value);

                    return FuzzyDateTimeCore.XDaysBeforeOrAfterTheFirstOrLastOfTheNextXMonths(numOfDays, daysDirection,
                        timeOfMonth, numOfMonths);
                }
            );

        private static readonly DateTimeRegexPattern XDaysBeforeOrAfterTheFirstOrLastOfTheMonthXMonthsAgoPattern =
            new DateTimeRegexPattern
            (
                @"^([0-9]\d*) days (before|after) the (first|last) of the month ([0-9]\d*) months ago$",
                delegate (Match m)
                {
                    var numOfDays = int.Parse(m.Groups[1].Value);
                    var daysDirection = m.Groups[2].Value;
                    var timeOfMonth = m.Groups[3].Value;
                    var numOfMonths = int.Parse(m.Groups[4].Value);

                    return FuzzyDateTimeCore.XDaysBeforeOrAfterTheFirstOrLastOfTheMonthXMonthsAgo(numOfDays, daysDirection,
                        timeOfMonth, numOfMonths);
                }
            );

        private static readonly DateTimeRegexPattern DayXOfLastOrThisOrNextMonthPattern = new DateTimeRegexPattern
        (
            @"^day ([0-9]\d*) of (last|this|next) month$",
            delegate (Match m)
            {
                var dayOfMonth = int.Parse(m.Groups[1].Value);
                var direction = m.Groups[2].Value;

                return FuzzyDateTimeCore.DayXOfLastOrThisOrNextMonth(dayOfMonth, direction);
            }
        );

        #endregion


        #region Days

        private static readonly DateTimeRegexPattern XDaysAgoPattern = new DateTimeRegexPattern
        (
            @"^([0-9]\d*) days ago$",
            delegate (Match m)
            {
                var numOfDays = int.Parse(m.Groups[1].Value);

                return FuzzyDateTimeCore.XDaysAgo(numOfDays);
            }
        );

        private static readonly DateTimeRegexPattern NextXDaysPattern = new DateTimeRegexPattern
        (
            @"^next ([0-9]\d*) days$",
            delegate (Match m)
            {
                var numOfDays = int.Parse(m.Groups[1].Value);

                return FuzzyDateTimeCore.NextXDays(numOfDays);
            }
        );

        private static readonly DateTimeRegexPattern XDaysFromTodayPattern = new DateTimeRegexPattern
        (
            @"^(-)?([0-9]\d*) days from today$",
            delegate (Match m)
            {
                var isNegative = m.Groups[1].Value == "-";
                var numOfDays = int.Parse(m.Groups[2].Value) * (isNegative ? -1 : 1);

                return FuzzyDateTimeCore.XDaysFromToday(numOfDays);
            }
        );

        private static readonly DateTimeRegexPattern YesterdayPattern = new DateTimeRegexPattern
        (
            @"^yesterday$",
            delegate (Match m) { return FuzzyDateTimeCore.Yesterday(); }
        );

        private static readonly DateTimeRegexPattern TodayPattern = new DateTimeRegexPattern
        (
            @"^today$",
            delegate (Match m) { return FuzzyDateTimeCore.Today(); }
        );

        private static readonly DateTimeRegexPattern TomorrowPattern = new DateTimeRegexPattern
        (
            @"^tomorrow$",
            delegate (Match m) { return FuzzyDateTimeCore.Tomorrow(); }
        );

        private static readonly DateTimeRegexPattern LastOrNextDayOfWeekPattern = new DateTimeRegexPattern
        (
            @"^(last|next) (?i)(Sunday|Monday|Tuesday|Wednesday|Thursday|Friday|Saturday)$",
            delegate (Match m)
            {
                var direction = m.Groups[1].Value;
                var dayOfWeekStr = m.Groups[2].Value;

                return FuzzyDateTimeCore.LastOrNextDayOfWeek(direction, dayOfWeekStr);
            }
        );

        #endregion


        #region Hours

        private static readonly DateTimeRegexPattern XHoursAgoPattern = new DateTimeRegexPattern
        (
            @"^([0-9]\d*) hours ago$",
            delegate (Match m)
            {
                var numOfHours = int.Parse(m.Groups[1].Value);

                return FuzzyDateTimeCore.XHoursAgo(numOfHours);
            }
        );

        private static readonly DateTimeRegexPattern NextXHoursPattern = new DateTimeRegexPattern
        (
            @"^next ([0-9]\d*) hours$",
            delegate (Match m)
            {
                var numOfHours = int.Parse(m.Groups[1].Value);

                return FuzzyDateTimeCore.NextXHours(numOfHours);
            }
        );

        #endregion


        #region Minutes

        private static readonly DateTimeRegexPattern XMinutesAgoPattern = new DateTimeRegexPattern
        (
            @"^([0-9]\d*) minutes ago$",
            delegate (Match m)
            {
                var numOfMinutes = int.Parse(m.Groups[1].Value);

                return FuzzyDateTimeCore.XMinutesAgo(numOfMinutes);
            }
        );

        private static readonly DateTimeRegexPattern NextXMinutesPattern = new DateTimeRegexPattern
        (
            @"^next ([0-9]\d*) minutes$",
            delegate (Match m)
            {
                var numOfMinutes = int.Parse(m.Groups[1].Value);

                return FuzzyDateTimeCore.NextXMinutes(numOfMinutes);
            }
        );

        #endregion


        #region Seconds

        private static readonly DateTimeRegexPattern XSecondsAgoPattern = new DateTimeRegexPattern
        (
            @"^([0-9]\d*) seconds ago$",
            delegate (Match m)
            {
                var numOfSeconds = int.Parse(m.Groups[1].Value);

                return FuzzyDateTimeCore.XSecondsAgo(numOfSeconds);
            }
        );

        private static readonly DateTimeRegexPattern NextXSecondsPattern = new DateTimeRegexPattern
        (
            @"^next ([0-9]\d*) seconds$",
            delegate (Match m)
            {
                var numOfSeconds = int.Parse(m.Groups[1].Value);

                return FuzzyDateTimeCore.NextXSeconds(numOfSeconds);
            }
        );

        #endregion


        #region Generic

        private static readonly DateTimeRegexPattern DatePattern = new DateTimeRegexPattern
        (
            @".*",
            delegate (Match m)
            {
                var text = m.Groups[0].Value;

                return DateTime.TryParse(text, out var dt) ? dt : DateTime.MinValue;
            }
        );

        #endregion

        #endregion

        private static readonly List<DateTimeRegexPattern> RegexPatterns = new List<DateTimeRegexPattern>
        {
            LastOrNextYearPattern,
            XYearsAgoPattern,
            NextXYearsPattern,
            XYearsFromTodayPattern,
            LastMonthPattern,
            NextMonthPattern,
            XMonthsAgoPattern,
            NextXMonthsPattern,
            XMonthsFromTodayPattern,
            FirstOrLastOfTheMonthXMonthsAgoPattern,
            FirstOrLastOfTheMonthNextXMonthsPattern,
            XDaysBeforeOrAfterTheFirstOrLastOfTheNextXMonthsPattern,
            XDaysBeforeOrAfterTheFirstOrLastOfTheMonthXMonthsAgoPattern,
            DayXOfLastOrThisOrNextMonthPattern,
            XDaysAgoPattern,
            NextXDaysPattern,
            XDaysFromTodayPattern,
            YesterdayPattern,
            TodayPattern,
            TomorrowPattern,
            LastOrNextDayOfWeekPattern,
            XHoursAgoPattern,
            NextXHoursPattern,
            XMinutesAgoPattern,
            NextXMinutesPattern,
            XSecondsAgoPattern,
            NextXSecondsPattern,
            DatePattern,
        };

        /// <summary>
        /// Parses a string using fuzzy date logic.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DateTime Parse(string text)
        {
            text = text.Trim().ToLower();
            var dateTime = DateTime.MinValue;
            foreach (var regexPattern in RegexPatterns)
            {
                dateTime = regexPattern.Parse(text);
                if (dateTime != DateTime.MinValue)
                    break;
            }

            return dateTime;
        }
    }
}