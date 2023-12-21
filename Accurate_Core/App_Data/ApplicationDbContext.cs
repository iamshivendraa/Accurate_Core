using Accurate_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Accurate_Core.App_Data
{ 
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext()
        {
            
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<RegisteredUsers> RegisteredUsers { get; set; }
        public DbSet<OrderModel>? OrderDetails { get; set; }
        public DbSet<ExcelSample>? ExcelData { get; set; }
    }
}
