namespace Sevriukoff.Gwalt.Application.Helpers;

public class DateTimeHelper
{
    public string GetTimeAgo(DateTime dateTime)
    {
        var currentDate = new DateTime(2024, 6, 14);
        var timeSpan = currentDate - dateTime;

        int years = (int)(timeSpan.Days / 365.25);
        int months = (int)((timeSpan.Days % 365.25) / 30.44);
        int days = timeSpan.Days % 30;

        if (years > 0)
            return $"{years} {GetYearString(years)} назад";

        return months > 0 ? $"{months} {GetMonthString(months)} назад" : $"{days} {GetDayString(days)} назад";
    }

    private string GetYearString(int years)
    {
        return (years % 10) switch
        {
            1 when years % 100 != 11 => "год",
            >= 2 and <= 4 when !(years % 100 >= 12 && years % 100 <= 14) => "года",
            _ => "лет"
        };
    }

    private string GetMonthString(int months)
    {
        return (months % 10) switch
        {
            1 when months % 100 != 11 => "месяц",
            >= 2 and <= 4 when !(months % 100 >= 12 && months % 100 <= 14) => "месяца",
            _ => "месяцев"
        };
    }

    private string GetDayString(int days)
    {
        return (days % 10) switch
        {
            1 when days % 100 != 11 => "день",
            >= 2 and <= 4 when !(days % 100 >= 12 && days % 100 <= 14) => "дня",
            _ => "дней"
        };
    }
}