using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Entities : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=PC-BILGISAYAR\SQLEXPRESS; Database=Test4; Trusted_Connection=True;",

                options =>
                {
                    options.EnableRetryOnFailure();
                    options.CommandTimeout(300);
                    options.MaxBatchSize(Int32.MaxValue);
                });
            }
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
