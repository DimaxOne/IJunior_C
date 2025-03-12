using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polyclinic
{
    class Program
    {
        static void Main(string[] args)
        {
            int admissionDuration = 10;
            int minutesOnHour = 60;
            int peopleInLine;
            int hours;
            int minutes;

            Console.Write("Людей в очереди: ");
            peopleInLine = Convert.ToInt32(Console.ReadLine());

            hours = peopleInLine * admissionDuration / minutesOnHour;
            minutes = peopleInLine * admissionDuration % minutesOnHour;

            Console.WriteLine($"Вы должны отстоять в очереди {hours} часа и {minutes} минут.");
        }
    }
}
