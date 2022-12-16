using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderOop.EntityFramework
{
    public class TraderDbContextFactory : IDesignTimeDbContextFactory<TraderDbContext>
    {
        public TraderDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<TraderDbContext>();


            options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=TraderDB;Trusted_Connection=True;");

            return new TraderDbContext(options.Options);

            //return new TraderDbContext(new DbContextOptionsBuilder().UseSqlite("Data Source=Trader.db").Options);

        }
    }
}
