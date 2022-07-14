using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DBModels
{
    public class Flight
    {
        public int Id { get; set; }
        public string FlightNo { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int AirlineId { get; set; }
        public Airline Airline { get; set; }

       
        public int BusinesClassSeats { get; set; }
        public int NonBusinessSeats { get; set; }
        public double TicketFair { get; set; }

        public int NoOfRows { get; set; }


        public int MealId { get; set; }
        public Meal Meal { get; set; }

        public List<Flight_Shedule> Flight_Shedules { get; set; }
    }
}
