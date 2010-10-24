using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace QueryablesCompared.EF
{
    class FooContext : DbContext
    {
        static readonly DbModel model;

        static FooContext()
        {
            var modelBuilder = new ModelBuilder();
            modelBuilder.DiscoverEntitiesFromContext(typeof(FooContext));
            modelBuilder.Entity<Foo>().HasKey(x => x.Id);
            model = modelBuilder.CreateModel();
        }

        public FooContext(bool rebuildSchema)
            : base("db", model)
        {
            if (rebuildSchema)
                System.Data.Entity.Infrastructure.Database.SetInitializer(
                    new RecreateDatabaseIfModelChanges<FooContext>());
            
        }

        public DbSet<Foo> Foos { get; set; }
    }
}
