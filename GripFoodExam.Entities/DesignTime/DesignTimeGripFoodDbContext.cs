using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GripFoodExam.Entities.DesignTime
{
    internal class DesignTimeGripFoodDbContext : IDesignTimeDbContextFactory<GripFoodDbContext>
    {
        public GripFoodDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GripFoodDbContext>();
            optionsBuilder.UseSqlite("Data Source=local.db");
            optionsBuilder.UseOpenIddict();


            var db = new GripFoodDbContext(optionsBuilder.Options);
            return db;
        }
    }
}
