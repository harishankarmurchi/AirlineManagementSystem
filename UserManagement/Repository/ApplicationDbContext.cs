using Microsoft.EntityFrameworkCore;
using Repository.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
