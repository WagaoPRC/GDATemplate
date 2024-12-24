using GDATemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GDATemplate.Data.Context
{
    public partial class SqlContext : DbContext
    {
        public SqlContext()
        {

        }

        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        public DbSet<Demo> Demos { get; set; }
        public DbSet<ExampleRelationship> Examples { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Referenciando ID

            modelBuilder.Entity<Demo>()
                .HasKey(d => d.IdDemo);
            modelBuilder.Entity<ExampleRelationship>()
                .HasKey(d => d.IdExampleRelationship);

            #endregion

            #region Relacionamento

            modelBuilder.Entity<ExampleRelationship>()
                .HasOne(pt => pt.Demo)
                .WithMany(t => t.ExampleRelationship)
                .HasForeignKey(pt => pt.IdDemo);

            #endregion

            #region Inverção de Relacionamento

            modelBuilder.Entity<Demo>()
                .HasMany(d => d.ExampleRelationship)
                .WithOne(p => p.Demo);

            #endregion

            #region Navegação 

            modelBuilder.Entity<ExampleRelationship>().Navigation(e => e.Demo).AutoInclude();

            #endregion
        }
    }
}
