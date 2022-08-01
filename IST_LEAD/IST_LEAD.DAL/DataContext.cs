using IST_LEAD.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IST_LEAD.DAL
{
    public class DataContext : DbContext
    {
        public DbSet<ExcelEntity> ExcelFiles { get; set; }

        public DataContext(DbContextOptions<DataContext> options, IServiceProvider serviceProvider) :base(options)
        {

        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
