using GDATemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GDATemplate.Data.Context
{
    public partial class TesteContext : DbContext
    {
        public TesteContext()
        {

        }

        public TesteContext(DbContextOptions<TesteContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        public DbSet<Demo> Demos { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Referenciando ID

            modelBuilder.Entity<Demo>()
                .HasKey(d => d.IdDemo);


            #endregion

        }
    }
}
