using System;

namespace FuzzyDateHelper
{
    public static class FuzzyDateTimeCore
    {

        #region Years

        public static DateTime LastOrNextYear(string direction)
        {
            return new DateTime(DateTime.Now.Year + (direction == "last" ? -1 : 1), 1, 1);
        }

        public static DateTime XYearsAgo(int numOfYears)
        {
            return DateTime.Now.AddYears(-numOfYears);
        }

        public static DateTime NextXYears(int numOfYears)
        {
            return DateTime.Now.AddYears(numOfYears);
        }

        public static DateTime XYearsFromToday(int numOfYears)
        {
            return DateTime.Now.AddYears(numOfYears);
        }

        #endregion


        #region Months

        private static int GetDayForTimeOfMonth(DateTime date, string timeOfMonth)
        {
            return timeOfMonth == "first" ? 1 : DateTime.DaysInMonth(date.Year, date.Month);
        }

        public static DateTime LastMonth()
        {
            return DateTime.Now.AddMonths(-1);
        }

        public static DateTime NextMonth()
        {
            return DateTime.Now.AddMonths(1);
        }

        public static DateTime XMonthsAgo(int numOfMonths)
        {
            return DateTime.Now.AddMonths(-numOfMonths);
        }

        public static DateTime NextXMonths(int numOfMonths)
        {
            return DateTime.Now.AddMonths(numOfMonths);
        }

        public static DateTime XMonthsFromToday(int numOfMonths)
        {
            return DateTime.Now.AddMonths(numOfMonths);
        }

        public static DateTime FirstOrLastOfTheMonthXMonthsAgo(string timeOfMonth, int numOfMonths)
        {
            var date = DateTime.Today.AddMonths(-numOfMonths);
            var day = GetDayForTimeOfMonth(date, timeOfMonth);

            return date.SetDay(day);
        }

        public static DateTime FirstOrLastOfTheMonthNextXMonths(string timeOfMonth, int numOfMonths)
        {
            var date = DateTime.Today.AddMonths(numOfMonths);
            var day = GetDayForTimeOfMonth(date, timeOfMonth);

            return date.SetDay(day);
        }

        public static DateTime XDaysBeforeOrAfterTheFirstOrLastOfTheNextXMonths(int numOfDays, string daysDirection, string timeOfMonth, int numOfMonths)
        {
            var date = FirstOrLastOfTheMonthNextXMonths(timeOfMonth, numOfMonths);

            return date.AddDays(numOfDays * (daysDirection == "before" ? -1 : 1));
        }

        public static DateTime XDaysBeforeOrAfterTheFirstOrLastOfTheMonthXMonthsAgo(int numOfDays, string daysDirection, string timeOfMonth, int numOfMonths)
        {
            var date = FirstOrLastOfTheMonthXMonthsAgo(timeOfMonth, numOfMonths);

            return date.AddDays(numOfDays * (daysDirection == "before" ? -1 : 1));
        }

        public static DateTime DayXOfLastOrThisOrNextMonth(int dayOfMonth, string direction)
        {
            var date = DateTime.Today;
            switch (direction)
            {
                case "last":
                    date = date.AddMonths(-1);
                    break;
                case "next":
                    date = date.AddMonths(1);
                    break;
            }

            return date.SetDay(dayOfMonth);
        }

        #endregion


        #region Days

        private static DateTime SetDay(this DateTime dateTime, int day)
        {
            return dateTime.AddDays(-dateTime.Day + day);
        }

        public static DateTime XDaysAgo(int numOfDays)
        {
            return DateTime.Now.AddDays(-numOfDays);
        }

        public static DateTime NextXDays(int numOfDays)
        {
            return DateTime.Now.AddDays(numOfDays);
        }

        public static DateTime XDaysFromToday(int numOfDays)
        {
            return DateTime.Now.AddDays(numOfDays);
        }

        public static DateTime Yesterday()
        {
            return DateTime.Now.AddDays(-1);
        }

        public static DateTime Today()
        {
            return DateTime.Now;
        }

        public static DateTime Tomorrow()
        {
            return DateTime.Now.AddDays(1);
        }

        public static DateTime LastOrNextDayOfWeek(string direction, string dayOfWeekStr)
        {
            var parsed = Enum.TryParse(dayOfWeekStr, true, out DayOfWeek dayOfWeek);

            // Return min value if the day of week couldn't be parsed
            if (!parsed) return DateTime.MinValue;

            var dayMultiplier = direction == "last" ? -1 : 1;
            var diff = dayMultiplier * (dayOfWeek - DateTime.Today.DayOfWeek);
            if (diff <= 0)
            {
                diff = 7 + diff;
            }

            return DateTime.Today.AddDays(dayMultiplier * diff);
        }

        #endregion


        #region Hours

        public static DateTime XHoursAgo(int numOfHours)
        {
            return DateTime.Now.AddHours(-numOfHours);
        }

        public static DateTime NextXHours(int numOfHours)
        {
            return DateTime.Now.AddHours(numOfHours);
        }

        #endregion


        #region Minutes

        public static DateTime XMinutesAgo(int numOfMinutes)
        {
            return DateTime.Now.AddMinutes(-numOfMinutes);
        }

        public static DateTime NextXMinutes(int numOfMinutes)
        {
            return DateTime.Now.AddMinutes(numOfMinutes);
        }

        #endregion


        #region Seconds

        public static DateTime XSecondsAgo(int numOfSeconds)
        {
            return DateTime.Now.AddSeconds(-numOfSeconds);
        }

        public static DateTime NextXSeconds(int numOfSeconds)
        {
            return DateTime.Now.AddSeconds(numOfSeconds);
        }

        #endregion

    }
}