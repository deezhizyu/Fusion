partial class Utils
{
    public static bool IsDatesEqual(DateTime savedDate, DateTime currentDate)
    {
        TimeSpan savedDateTimeSpan = new TimeSpan(savedDate.TimeOfDay.Hours, savedDate.TimeOfDay.Minutes, savedDate.TimeOfDay.Seconds);
        TimeSpan currentDateTimeSpan = new TimeSpan(currentDate.TimeOfDay.Hours, currentDate.TimeOfDay.Minutes, currentDate.TimeOfDay.Seconds);

        return savedDate.Date != currentDate.Date || savedDateTimeSpan != currentDateTimeSpan;
    }
}
