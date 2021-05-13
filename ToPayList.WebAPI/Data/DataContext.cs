using Microsoft.EntityFrameworkCore;
using ToPayList.WebAPI.Model;

namespace ToPayList.WebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Conta> Contas { get; set; }
    }
}