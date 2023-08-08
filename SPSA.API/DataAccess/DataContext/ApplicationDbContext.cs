using Microsoft.EntityFrameworkCore;

namespace SPSA.API.DataAccess.DataContext
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
