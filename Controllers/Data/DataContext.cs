using Microsoft.EntityFrameworkCore;

namespace CRUD_Entity_.NET_6._0.Controllers.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<SuperHero> SuperHeroes { get; set; }  
    }
}
