using Microsoft.EntityFrameworkCore;
using StoredProcMVC.Models;

namespace StoredProcMVC.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }
        public DbSet<Emp> emps { get; set; }
    }
}
