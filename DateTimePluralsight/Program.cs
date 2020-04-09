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

            /*foreach (var tz in timezoneList)
            {
                if(tz.GetUtcOffset(offset) == offset.Offset) PrintTime("Found Offset On Time", tz.ToString());
                if(tz.GetUtcOffset(offset) == offset.ToOffset(TimeSpan.FromHours(10)).Offset) PrintTime("Found Offset 10 hours", tz.ToString());
            }
            */

            Console.WriteLine("PARSING");
            var stringDate = "9/10/2019 10:00:00 PM";
            PrintTime("Parse Exact", DateTime.ParseExact(stringDate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));


            Console.ReadKey();

        }

        private static void PrintTime(string message, DateTime date) => Console.WriteLine($"{message}: {date}");
        private static void PrintTime(string message, DateTimeOffset date) => Console.WriteLine($"{message}: {date}");
        private static void PrintTime(string message, string date) => Console.WriteLine($"{message}: {date}");
    }
}
