using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetupReminder.Core.Service;
using MeetupReminder.Core;
using MeetupReminder.Core.Domain;

namespace MeetupReminder
{
    class Program
    {
        static void Main(string[] args)
        {       

            Console.WriteLine("Enter the name of the group you want to see meetups for");
            string groupname = Console.ReadLine();
            
            MeetupSearchResult result = MeetupService.GetMeetupsFor(groupname).Result;

            var message = "These are your meetups: ";
            for (int i = 0; i < result.results.Count; i++)
            {
                double dateTime = (result.results[i].time);
                Console.WriteLine(result.results[i].name);
                Console.WriteLine("--" + result.results[i].status);
                Console.WriteLine("--" + UnixToDateTime(dateTime));

                message += ("\n" + result.results[i].name);
                message += ("\n" + "--" + result.results[i].status);
                message += ("\n" + "--" + UnixToDateTime(dateTime));
            }

            SendSms.SendMessage("+18185711449", message);
            Console.ReadLine();
        }

        public static DateTime UnixToDateTime(double i)
        {
            DateTime dt;
            dt = new DateTime(1970,1,1,0,0,0,0,DateTimeKind.Utc);
            dt = dt.AddMilliseconds(i).ToLocalTime();
            return dt;
        }
    }
}