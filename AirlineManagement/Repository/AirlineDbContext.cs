using Microsoft.EntityFrameworkCore;
using Repository.DBModels;

namespace Repository
{
    public class AirlineDbContext:DbContext
    {
        public AirlineDbContext(DbContextOptions<AirlineDbContext> options):base(options)
        {

        }

        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<SheduleDay> SheduleDays { get; set; }
        public DbSet<Flight_Shedule> Flight_Shedules { get; set; }
        public DbSet<Meal> Meals { get; set; }
    }
}
