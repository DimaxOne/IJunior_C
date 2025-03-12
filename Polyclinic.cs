using System;

namespace Polyclinic
{
    class Program
    {
        static void Main(string[] args)
        {
            int admissionDuration = 10;
            int minutesOnHour = 60;
            int peopleInLine;
            int hoursInLine;
            int minutesInLine;
            int durationMinutesInLine;

            Console.Write("Людей в очереди: ");
            peopleInLine = Convert.ToInt32(Console.ReadLine());

            durationMinutesInLine = peopleInLine * admissionDuration;
            hoursInLine = durationMinutesInLine / minutesOnHour;
            minutesInLine = durationMinutesInLine % minutesOnHour;

            Console.WriteLine($"Вы должны отстоять в очереди {hoursInLine} часа и {minutesInLine} минут.");
        }
    }
}
