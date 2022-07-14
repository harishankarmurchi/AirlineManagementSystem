using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DBModels
{
    public class Flight_Shedule
    {
        public int Id { get; set; }

        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        public int SheduleId { get; set; }
        public SheduleDay SheduleDay { get; set; }
    }
}
