using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DBModels
{
    public class SheduleDay
    {
        public int Id { get; set; }
        public string Day { get; set; }

        public List<Flight_Shedule> Flight_Shedules { get; set; }
    }
}
