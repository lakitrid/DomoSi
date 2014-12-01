using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fr.Lakitrid.DomoDb.Entities;

namespace Fr.Lakitrid.DomoDb
{
    public class DomoContext : DbContext
    {
        public DomoContext()
            : base("name=DomoDb")
        {
        }

        public DbSet<DataSerie> DataSerie { get; set; }

        public DbSet<Sample> Sample { get; set; }

        public DbSet<Parameter> Parameter { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Parameter>()
                .HasRequired(m => m.Serie)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sample>()
                .HasRequired(m => m.Serie)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}
