using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDWrox.DataModel.SecurityModels
{
    public class SecurityModelContext : DbContext
    {
        public SecurityModelContext(DbContextOptions<SecurityModelContext> options) : base(options)
        {
            
        }

        public SecurityModelContext(string connectionString) : base(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-DENKRMG\\BMSSQLSERVER;Initial Catalog=SecurityModel;Encrypt=False;Integrated Security=True");


        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "System",
                    LastName = "",
                    Username = "System",
                    Password = "System",
                }
            );
        }
    }
}
