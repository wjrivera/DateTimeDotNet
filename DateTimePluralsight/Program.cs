// Based on: https://app.pluralsight.com/library/courses/dotnet-dates-times
using System;
using System.Globalization;
using System.Linq;

namespace DateTimePluralsight
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("\nDate Times!");
            var traceDate = DateTime.Parse(DateTime.Now.ToString(CultureInfo.CurrentCulture));
            PrintTime("Current Culture", traceDate);
            PrintTime("To long date", traceDate.ToLongDateString());
            PrintTime("Great Britain Culture", DateTime.Parse(DateTime.Now.ToString(CultureInfo.CurrentCulture), CultureInfo.GetCultureInfo("en-GB")));

            Console.WriteLine("\nUTC");
            PrintTime("Universal Time", DateTime.Now.ToUniversalTime());
            PrintTime("UTC Now", DateTime.UtcNow);
            
            Console.WriteLine("\nTIMEZONES");
            PrintTime("DateTime Now", DateTime.Now);
            var timezoneList = TimeZoneInfo.GetSystemTimeZones();
            var eastern = timezoneList.FirstOrDefault(x => x.Id == "Eastern Standard Time");
            if (eastern != null) PrintTime("Eastern Time", TimeZoneInfo.ConvertTime(DateTime.Now, eastern));
            var timezone = TimeZoneInfo.FindSystemTimeZoneById("E. Australia Standard Time");
            var time = TimeZoneInfo.ConvertTime(DateTime.Now, timezone);
            PrintTime("Australia Time", time);
            Console.WriteLine("DATETIME OFFSET");
            var datetime_regular = DateTime.Now;
            var offset = DateTimeOffset.Now;
            PrintTime("Offset", DateTimeOffset.Now);

            foreach (var tz in timezoneList)
            {
                if (tz.GetUtcOffset(offset) == offset.Offset || tz.GetUtcOffset(offset) == offset.ToOffset(TimeSpan.FromHours(10)).Offset) 
                    PrintTime("Found Offset On Time", tz.ToString());
            }

            Console.WriteLine("PARSING");
            var stringDate = "9/10/2019 10:00:00 PM";
            PrintTime("Parse Exact", DateTime.ParseExact(stringDate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
            stringDate = "9/10/2019 10:00:00 PM +02:00";
            PrintTime("Parse Date", DateTime.Parse(stringDate));
            PrintTime("Kind ", DateTime.Parse(stringDate).Kind.ToString());
            PrintTime("Adjust UTC ", DateTime.Parse(stringDate, CultureInfo.InstalledUICulture, DateTimeStyles.AdjustToUniversal));
            PrintTime("Kind Adjust UTC ", DateTime.Parse(stringDate, CultureInfo.InstalledUICulture, DateTimeStyles.AdjustToUniversal).Kind.ToString());

            var isoDate = "2019-06-10T18:00:00+00:00";
            PrintTime("ISO8601 Date", DateTime.Parse(isoDate));
            PrintTime("Format ToString", DateTime.Parse(isoDate).ToString("yyyy-MMM-dd"));
            PrintTime("Format ToString", DateTime.Parse(isoDate).ToString("s"));
            isoDate = "9/10/2019 10:00:00 PM";
            PrintTime("Format ToString", DateTimeOffset.ParseExact(isoDate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture).ToOffset(TimeSpan.FromHours(10)).ToString("o"));
            
            Console.WriteLine("UTC ZULU TIME");
            PrintTime("Datetime Now", DateTime.Now);
            PrintTime("Offset Now", DateTimeOffset.Now);
            PrintTime("Offset UTC", DateTimeOffset.UtcNow);
            PrintTime("Convert Offset UTC to local", DateTimeOffset.UtcNow.ToLocalTime());
            PrintTime("Convert Offset Now to Universal time", DateTimeOffset.Now.ToUniversalTime());
            var offsetClass = new DateTimeOffsetClass{DateTimeOffset = DateTimeOffset.UtcNow};
            PrintTime("Offset Class offset", offsetClass.DateTimeOffset);
            PrintTime("Offset Class universal", offsetClass.DateTimeOffset.ToUniversalTime());

            Console.WriteLine("DATE ARITHMETIC");
            var timespan = new TimeSpan(60, 100, 200);
            PrintTime("Timespan", timespan.ToString());
            PrintTime("Timespan", timespan.TotalHours.ToString(CultureInfo.InvariantCulture));
            var start = DateTimeOffset.UtcNow;
            var end = start.AddSeconds(45);
            var difference = end - start;
            PrintTime("Difference timespan", difference);
            PrintTime("Difference multiply", difference.Multiply(2));

            Console.WriteLine("WEEK NUMBER");
            var calendar = CultureInfo.InvariantCulture.Calendar;
            PrintTime("Week number", calendar.GetWeekOfYear(DateTimeOffset.UtcNow.DateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday).ToString());
            PrintTime("ISO Week", ISOWeek.GetWeekOfYear(DateTimeOffset.UtcNow.DateTime).ToString());
        }

        private static void PrintTime(string message, DateTime date) => Console.WriteLine($"{message}: {date}");
        private static void PrintTime(string message, DateTimeOffset date) => Console.WriteLine($"{message}: {date}");
        private static void PrintTime(string message, TimeSpan timespan) => Console.WriteLine($"{message}: {timespan}");
        private static void PrintTime(string message, string date) => Console.WriteLine($"{message}: {date}");
    }

    public class DateTimeOffsetClass
    {
        public DateTimeOffset DateTimeOffset { get; set; }
    }
}
