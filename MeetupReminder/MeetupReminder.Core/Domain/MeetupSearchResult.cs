using Spring.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupReminder.Core.Domain
{
    public class MeetupSearchResult
    {
        public List<MeetupGroupEvent> results { get; set; }
    }
}
