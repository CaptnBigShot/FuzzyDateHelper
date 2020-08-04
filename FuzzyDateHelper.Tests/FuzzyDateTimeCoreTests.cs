using System;
using FluentAssertions;
using NUnit.Framework;

namespace FuzzyDateHelper.Tests
{
    [TestFixture]
    [Parallelizable]
    public class FuzzyDateTimeCoreTests
    {
        [TestCase("last", -1)]
        [TestCase("next", 1)]
        public void LastOrNextYearTest(string direction, int yearsToAdd)
        {
            var date = FuzzyDateTimeCore.LastOrNextYear(direction);
            var expectedDate = new DateTime(DateTime.Now.Year + yearsToAdd, 1, 1);

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test]
        public void XYearsAgoTest([Range(0, 100)] int numOfYears)
        {
            var date = FuzzyDateTimeCore.XYearsAgo(numOfYears);
            var expectedDate = DateTime.Now.AddYears(-numOfYears);

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test]
        public void NextXYearsTest([Range(0, 100)] int numOfYears)
        {
            var date = FuzzyDateTimeCore.NextXYears(numOfYears);
            var expectedDate = DateTime.Now.AddYears(numOfYears);

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test]
        public void XYearsFromToday([Values(-1000, -100, -10, -9, -1, 0, 1, 9, 10, 100, 1000)] int numOfYears)
        {
            var date = FuzzyDateTimeCore.XYearsFromToday(numOfYears);
            var expectedDate = DateTime.Now.AddYears(numOfYears);

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test]
        public void LastMonthTest()
        {
            var date = FuzzyDateTimeCore.LastMonth();
            var expectedDate = DateTime.Now.AddMonths(-1);

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test]
        public void NextMonthTest()
        {
            var date = FuzzyDateTimeCore.NextMonth();
            var expectedDate = DateTime.Now.AddMonths(1);

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test]
        public void XMonthsAgoTest([Range(0, 100)] int numOfMonths)
        {
            var date = FuzzyDateTimeCore.XMonthsAgo(numOfMonths);
            var expectedDate = DateTime.Now.AddMonths(-numOfMonths);

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test]
        public void NextXMonthsTest([Range(0, 100)] int numOfMonths)
        {
            var date = FuzzyDateTimeCore.NextXMonths(numOfMonths);
            var expectedDate = DateTime.Now.AddMonths(numOfMonths);

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test]
        public void XMonthsFromToday([Values(-1000, -100, -10, -9, -1, 0, 1, 9, 10, 100, 1000)] int numOfMonths)
        {
            var date = FuzzyDateTimeCore.XMonthsFromToday(numOfMonths);
            var expectedDate = DateTime.Now.AddMonths(numOfMonths);

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test, Combinatorial]
        public void FirstOrLastOfTheMonthXMonthsAgoTest([Values("first", "last")] string timeOfMonth, 
            [Range(0, 100)] int numOfMonths)
        {
            var date = FuzzyDateTimeCore.FirstOrLastOfTheMonthXMonthsAgo(timeOfMonth, numOfMonths);
            var expectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            if (timeOfMonth == "last")
            {
                expectedDate = expectedDate
                    .AddMonths(-numOfMonths + 1)
                    .AddDays(-1);
            }
            else
            {
                expectedDate = expectedDate
                    .AddMonths(-numOfMonths);
            }

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test, Combinatorial]
        public void FirstOrLastOfTheMonthNextXMonthsTest([Values("first", "last")] string timeOfMonth, 
            [Range(0, 100)] int numOfMonths)
        {
            var date = FuzzyDateTimeCore.FirstOrLastOfTheMonthNextXMonths(timeOfMonth, numOfMonths);
            var expectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            if (timeOfMonth == "last")
            {
                expectedDate = expectedDate
                    .AddMonths(numOfMonths + 1)
                    .AddDays(-1);
            }
            else
            {
                expectedDate = expectedDate
                    .AddMonths(numOfMonths);
            }

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test, Combinatorial]
        public void XDaysBeforeOrAfterTheFirstOrLastOfTheNextXMonthsTest([Values(1, 2, 3)] int numOfDays, 
            [Values("before", "after")] string daysDirection,
            [Values("first", "last")] string timeOfMonth,
            [Values(1, 2, 3)] int numOfMonths)
        {
            var date = FuzzyDateTimeCore.XDaysBeforeOrAfterTheFirstOrLastOfTheNextXMonths(numOfDays, daysDirection, timeOfMonth, numOfMonths);
            var expectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            if (timeOfMonth == "last")
            {
                expectedDate = expectedDate
                    .AddMonths(numOfMonths + 1)
                    .AddDays(-1);
            }
            else
            {
                expectedDate = expectedDate
                    .AddMonths(numOfMonths);
            }

            if (daysDirection == "before")
            {
                expectedDate = expectedDate
                    .AddDays(-numOfDays);
            }
            else
            {
                expectedDate = expectedDate
                    .AddDays(numOfDays);
            }

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test, Combinatorial]
        public void XDaysBeforeOrAfterTheFirstOrLastOfTheMonthXMonthsAgoTest([Values(1, 2, 3)] int numOfDays,
            [Values("before", "after")] string daysDirection,
            [Values("first", "last")] string timeOfMonth,
            [Values(1, 2, 3)] int numOfMonths)
        {
            var date = FuzzyDateTimeCore.XDaysBeforeOrAfterTheFirstOrLastOfTheMonthXMonthsAgo(numOfDays, daysDirection, timeOfMonth, numOfMonths);
            var expectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            if (timeOfMonth == "last")
            {
                expectedDate = expectedDate
                    .AddMonths(-numOfMonths + 1)
                    .AddDays(-1);
            }
            else
            {
                expectedDate = expectedDate
                    .AddMonths(-numOfMonths);
            }

            if (daysDirection == "before")
            {
                expectedDate = expectedDate
                    .AddDays(-numOfDays);
            }
            else
            {
                expectedDate = expectedDate
                    .AddDays(numOfDays);
            }

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test, Combinatorial]
        public void DayXOfLastOrThisOrNextMonthTest([Range(1, 28)] int day, 
            [Values("last", "this", "next")] string direction)
        {
            var date = FuzzyDateTimeCore.DayXOfLastOrThisOrNextMonth(day, direction);
            var expectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, day);
            expectedDate = direction switch
            {
                "last" => expectedDate.AddMonths(-1),
                "next" => expectedDate.AddMonths(1),
                _ => expectedDate
            };

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test]
        public void XDaysAgoTest([Values(0, 1, 9, 10, 100, 1000)] int numOfDays)
        {
            var date = FuzzyDateTimeCore.XDaysAgo(numOfDays);
            var expectedDate = DateTime.Now.AddDays(-numOfDays);

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test]
        public void NextXDaysTest([Values(0, 1, 9, 10, 100, 1000)] int numOfDays)
        {
            var date = FuzzyDateTimeCore.NextXDays(numOfDays);
            var expectedDate = DateTime.Now.AddDays(numOfDays);

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test]
        public void XDaysFromToday([Values(-1000, -100, -10, -9, -1, 0, 1, 9, 10, 100, 1000)] int numOfDays)
        {
            var date = FuzzyDateTimeCore.XDaysFromToday(numOfDays);
            var expectedDate = DateTime.Now.AddDays(numOfDays);

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test]
        public void YesterdayTest()
        {
            var date = FuzzyDateTimeCore.Yesterday();
            var expectedDate = DateTime.Now.AddDays(-1);

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test]
        public void TodayTest()
        {
            var date = FuzzyDateTimeCore.Today();
            var expectedDate = DateTime.Now;

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test]
        public void TomorrowTest()
        {
            var date = FuzzyDateTimeCore.Tomorrow();
            var expectedDate = DateTime.Now.AddDays(1);

            date.Should().BeSameDateAs(expectedDate);
        }


        [Test, Combinatorial]
        public void LastOrNextDayOfWeekTest([Values("last", "next")] string direction,
            [Values("Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday")] string dayOfWeekStr)
        {
            var date = FuzzyDateTimeCore.LastOrNextDayOfWeek(direction, dayOfWeekStr);
            var dayOfWeek = Enum.Parse<DayOfWeek>(dayOfWeekStr, true);

            date.DayOfWeek.Should().BeEquivalentTo(dayOfWeek);

            if (direction == "last")
            {
                date.Should()
                    .BeBefore(DateTime.Today).And
                    .BeWithin(TimeSpan.FromDays(7));
            }
            else
            {
                date.Should()
                    .BeAfter(DateTime.Today).And
                    .BeWithin(TimeSpan.FromDays(7));
            }
        }


        [Test]
        public void XHoursAgoTest([Range(0, 144)] int numOfHours)
        {
            var date = FuzzyDateTimeCore.XHoursAgo(numOfHours);
            var expectedDate = DateTime.Now.AddHours(-numOfHours);

            date.Should().BeCloseTo(expectedDate, 100);
        }


        [Test]
        public void NextXHoursTest([Range(0, 144)] int numOfHours)
        {
            var date = FuzzyDateTimeCore.NextXHours(numOfHours);
            var expectedDate = DateTime.Now.AddHours(numOfHours);

            date.Should().BeCloseTo(expectedDate, 100);
        }


        [Test]
        public void XMinutesAgoTest([Range(0, 120)] int numOfMinutes)
        {
            var date = FuzzyDateTimeCore.XMinutesAgo(numOfMinutes);
            var expectedDate = DateTime.Now.AddMinutes(-numOfMinutes);

            date.Should().BeCloseTo(expectedDate, 100);
        }


        [Test]
        public void NextXMinutesTest([Range(0, 120)] int numOfMinutes)
        {
            var date = FuzzyDateTimeCore.NextXMinutes(numOfMinutes);
            var expectedDate = DateTime.Now.AddMinutes(numOfMinutes);

            date.Should().BeCloseTo(expectedDate, 100);
        }


        [Test]
        public void XSecondsAgoTest([Range(0, 120)] int numOfSeconds)
        {
            var date = FuzzyDateTimeCore.XSecondsAgo(numOfSeconds);
            var expectedDate = DateTime.Now.AddSeconds(-numOfSeconds);

            date.Should().BeCloseTo(expectedDate, 100);
        }


        [Test]
        public void NextXSecondsTest([Range(0, 120)] int numOfSeconds)
        {
            var date = FuzzyDateTimeCore.NextXSeconds(numOfSeconds);
            var expectedDate = DateTime.Now.AddSeconds(numOfSeconds);

            date.Should().BeCloseTo(expectedDate, 100);
        }
    }
}