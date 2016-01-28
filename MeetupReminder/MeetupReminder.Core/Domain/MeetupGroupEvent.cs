using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupReminder.Core.Domain
{
    public class MeetupGroupEvent
    {
        public string name { get; set; }
        public string status { get; set; }
        public double time { get; set; }
        public double utc_offset { get; set; }
    }
}
