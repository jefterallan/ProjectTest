using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjectTest.Data.Models;

#nullable disable

namespace ProjectTest.Data
{
    public partial class ProjectTestContext : DbContext
    {
        public ProjectTestContext()
        {
        }

        public ProjectTestContext(DbContextOptions<ProjectTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblUser> TblUsers { get; set; }
        public virtual DbSet<TblUsersAuth> TblUsersAuths { get; set; }

        //this line here i put after the scaffolding
        public DbSet<SpStoreUserData> SpStoreUserData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TblUsersAuth>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                
                entity.Property(e => e.Password).IsFixedLength(true);

                entity.Property(e => e.User).IsFixedLength(true);
            });

            //this line here i put after the scaffolding
            modelBuilder.Entity<SpStoreUserData>(builder => { builder.HasNoKey(); });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
