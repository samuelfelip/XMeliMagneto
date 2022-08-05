using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meli.Data.EF
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContextFactory()
        {
        }
        //private IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("appsettings.json")
        //    .Build();
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            //builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            builder.UseSqlServer(@"Server=localhost,1433; Database=MeliMagneto; User=sa; Password =abc123$.;MultipleActiveResultSets=True");
            return new AppDbContext(builder.Options);
        }
    }
}
