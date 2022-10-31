using EmpMgmnt.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmpMgmnt.Data
{
    public class MvcDemoDbContext : DbContext
    {
        public MvcDemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
