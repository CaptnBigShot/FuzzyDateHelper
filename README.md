# FuzzyDateHelper
A .NET helper to parse `strings` to `DateTime` values.


## Installation

#### NuGet Package Manager
```sh
PM> Install-Package FuzzyDateHelper
```


#### .NET CLI
```sh
dotnet add package FuzzyDateHelper
```

#### From Source
1. Clone the repo
```sh
git clone https://github.com/CaptnBigShot/FuzzyDateHelper.git
```
2. CD to the project root directory and build
```sh
dotnet build
```


## Usage

#### FuzzyDateTime.Parse()

This method takes a `string` as a parameter and returns a `DateTime` using RegEx pattern matching. If no match is found, `DateTime.MinValue` is returned.

```c#
// Years
DateTime date = FuzzyDateTime.Parse("last year");
DateTime date = FuzzyDateTime.Parse("next year");
DateTime date = FuzzyDateTime.Parse("2 years ago");
DateTime date = FuzzyDateTime.Parse("next 2 years");
// x years from today (accepts positive and negative integers)
DateTime date = FuzzyDateTime.Parse("5 years from today");
DateTime date = FuzzyDateTime.Parse("-5 years from today");

// Months
DateTime date = FuzzyDateTime.Parse("last month");
DateTime date = FuzzyDateTime.Parse("next month");
DateTime date = FuzzyDateTime.Parse("3 months ago");
DateTime date = FuzzyDateTime.Parse("next 12 months");
// x months from today (accepts positive and negative integers)
DateTime date = FuzzyDateTime.Parse("6 months from today");
DateTime date = FuzzyDateTime.Parse("-6 months from today");
// first/last of the month x months ago
DateTime date = FuzzyDateTime.Parse("first of the month 3 months ago");
DateTime date = FuzzyDateTime.Parse("last of the month 3 months ago");
// first/last of the month next x months
DateTime date = FuzzyDateTime.Parse("first of the month next 8 months");
DateTime date = FuzzyDateTime.Parse("last of the month next 8 months");
// x days before/after the first/last of the next x months
DateTime date = FuzzyDateTime.Parse("7 days before the first of the next 2 months");
DateTime date = FuzzyDateTime.Parse("7 days before the last of the next 2 months");
DateTime date = FuzzyDateTime.Parse("12 days after the first of the next 11 months");
DateTime date = FuzzyDateTime.Parse("12 days after the last of the next 11 months");
// x days before/after the first/last of the month x months ago
DateTime date = FuzzyDateTime.Parse("9 days before the first of the month 5 months ago");
DateTime date = FuzzyDateTime.Parse("9 days before the last of the month 5 months ago");
DateTime date = FuzzyDateTime.Parse("1 days after the first of the month 14 months ago");
DateTime date = FuzzyDateTime.Parse("1 days after the last of the month 14 months ago");
// day X of last/this/next month
DateTime date = FuzzyDateTime.Parse("day 3 of last month");
DateTime date = FuzzyDateTime.Parse("day 17 of this month");
DateTime date = FuzzyDateTime.Parse("day 28 of next month");

// Days
DateTime date = FuzzyDateTime.Parse("3 days ago");
DateTime date = FuzzyDateTime.Parse("next 15 days");
// x days from today (accepts positive and negative integers)
DateTime date = FuzzyDateTime.Parse("12 days from today");
DateTime date = FuzzyDateTime.Parse("-12 days from today");
DateTime date = FuzzyDateTime.Parse("yesterday");
DateTime date = FuzzyDateTime.Parse("today");
DateTime date = FuzzyDateTime.Parse("tomorrow");
DateTime date = FuzzyDateTime.Parse("last Wednesday");

// Hours
DateTime date = FuzzyDateTime.Parse("3 hours ago");
DateTime date = FuzzyDateTime.Parse("next 4 hours");

// Minutes
DateTime date = FuzzyDateTime.Parse("15 minutes ago");
DateTime date = FuzzyDateTime.Parse("next 5 minutes");

// Seconds
DateTime date = FuzzyDateTime.Parse("5 seconds ago");
DateTime date = FuzzyDateTime.Parse("next 12 seconds");

// Dates
DateTime date = FuzzyDateTime.Parse("01/01/2020");
DateTime date = FuzzyDateTime.Parse("1/1/2020");
```

#### FuzzyDateTimeCore
The core `DateTime` calculation methods can be accessed directly.

```c#
DateTime date = FuzzyDateTimeCore.LastOrNextYear("last");
DateTime date = FuzzyDateTimeCore.FirstOrLastOfTheMonthXMonthsAgo("first", 7);
DateTime date = FuzzyDateTimeCore.LastOrNextDayOfWeek("next", "Thursday");
```

#### Example of integration with SpecFlow for automated tests

Data-driven tests requiring dynamic dates can use the fuzzy date strings in the scenario steps.
```cucumber
Scenario: SpecFlow and FuzzyDateHelper example
	Given the Event Registration page is displayed
	When the following people are registered for the event
		| First Name | Last Name | Registration Date |
		| Chloe      | Velasquez | 2 years ago       |
		| Michael    | Smith     | 4 months ago      |
		| Jane       | Doe       | 12 days ago       |
		| Tom        | Jones     | yesterday         |
		| Julia      | Winston   | 01/01/2020        |
	Then the event will show "5" people registered
```

These `DateTime` values can be parsed in the corresponding step definition.
```c#
[When(@"the following people are registered for the event")]
public void WhenTheFollowingPeopleAreRegisteredForTheEvent(Table table)
{
  // Iterate through each row in the table and parse the DateTime values
  foreach (TableRow row in table.Rows)
  {
    row["Registration Date"] = FuzzyDateTime.Parse(row["Registration Date"]).ToShortDateString();
  }
}
```


## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as needed.


## License
[MIT](https://choosealicense.com/licenses/mit/)


## Contact
Project Link: [https://github.com/CaptnBigShot/FuzzyDateHelper](https://github.com/CaptnBigShot/FuzzyDateHelper)
