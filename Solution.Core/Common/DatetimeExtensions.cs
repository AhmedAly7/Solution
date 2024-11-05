namespace Solution.Core.Common;

public static class DatetimeExtensions
{
	public static DateTime FirstDayOfTheMonth(this DateTime date)
	{
		return new DateTime(date.Year, date.Month, 1);
	}

	public static DateTime LastDayOfTheMonth(this DateTime date)
	{
		return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
	}

	public static DateTime GetFirstMondayOfYear(this DateTime date)
	{
		var dt = new DateTime(date.Year, 1, 1);
		while (dt.DayOfWeek != DayOfWeek.Monday)
		{
			dt = dt.AddDays(1);
		}

		return dt;
	}

	public static int GetWeekNumber(this DateTime date)
	{
		if (date == DateTime.MinValue) return 0;
		int week = ((int)((date - date.GetFirstMondayOfYear()).TotalDays / 7)) + 1;
		return week;
	}
}
