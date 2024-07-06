using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectCS.Models;
using System.Reflection.Emit;

namespace ProjectCS.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
            
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Class> Classes { get; set; }

        public  DbSet<ListStudent> ListStudents { get; set; }

        public DbSet<Assign> Assigns { get; set; }

        public DbSet<ListAssign> ListAssigns { get; set; }

        public DbSet<Loai> Loais { get; set; }

        public DbSet<ListFile> ListFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.FullName).HasMaxLength(450);
                entity.Property(e => e.AvatarPath).HasMaxLength(256);
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.ClassId);

                entity.ToTable("Class");

                entity.Property(e => e.ClassId).HasMaxLength(450);
                entity.Property(e => e.Image).HasMaxLength(255);
                entity.Property(e => e.Name).HasMaxLength(150);
                entity.Property(e => e.Room).HasMaxLength(50);
                entity.Property(e => e.Titlle).HasMaxLength(50);
                entity.Property(e => e.Topic).HasMaxLength(50);

                entity.HasMany(e => e.Assigns)
                    .WithOne(e => e.Class)
                    .HasForeignKey(e => e.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasMany(e => e.ListStudents)
                    .WithOne(e => e.Class)
                    .HasForeignKey(e => e.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ListStudent>(entity =>
            {

                entity.HasKey(e => new { e.UserId, e.ClassId });

                entity.ToTable("ListStudent");

                entity.Property(e => e.ClassId).HasMaxLength(450);
                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ListStudents)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ListStudents)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Assign>(entity => 
            {
                entity.HasKey(e => e.AssignId);

                entity.ToTable("Assign");

                entity.Property(e => e.AssignId).HasMaxLength(450);
                entity.Property(e => e.AssignName).HasMaxLength(450);
                entity.Property(e => e.Description).HasMaxLength(450);
                entity.Property(e => e.Posttime).HasColumnType("datetime");
                entity.Property(e => e.AssignFile1).HasMaxLength(450);
                entity.Property(e => e.AssignFile2).HasMaxLength(450);
                entity.Property(e => e.ClassId).HasMaxLength(450);
                entity.Property(e => e.LoaiId).HasMaxLength(450);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Assigns)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Loai)
                    .WithMany(p => p.Assigns)
                    .HasForeignKey(d => d.LoaiId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasMany(e => e.ListAssigns)
                    .WithOne(e => e.Assign)
                    .HasForeignKey(e => e.AssignId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ListAssign>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.AssignId, e.LoaiId });

                entity.ToTable("ListAssign");

                entity.Property(e => e.UserId).HasMaxLength(450);
                entity.Property(e => e.AssignId).HasMaxLength(450);
                entity.Property(e => e.LoaiId).HasMaxLength(450);
                entity.Property(e => e.Point).HasMaxLength(450);

                entity.HasOne(e => e.Assign)
                    .WithMany(e => e.ListAssigns)
                    .HasForeignKey(e => e.AssignId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(e => e.User)
                    .WithMany(e => e.ListAssigns)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasMany(e => e.ListFiles)
                    .WithOne(e => e.ListAssigns)
                    .HasForeignKey(e => new { e.UserId, e.AssignId, e.LoaiId })
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Loai>(entity =>
            {
                entity.HasKey(e => e.LoaiId);

                entity.ToTable("Loai");

                entity.Property(e => e.LoaiId).HasMaxLength(450);
                entity.Property(e => e.LoaiName).HasMaxLength(450);

                entity.HasMany(e => e.Assigns)
                    .WithOne(e => e.Loai)
                    .HasForeignKey(e => e.LoaiId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ListFile>(entity => 
            {
                entity.HasKey(e => new { e.UserId, e.AssignId, e.LoaiId, e.FileId });

                entity.ToTable("ListFile");

                entity.Property(e => e.FileId).HasMaxLength(450);
                entity.Property(e => e.UserId).HasMaxLength(450);
                entity.Property(e => e.AssignId).HasMaxLength(450);
                entity.Property(e => e.LoaiId).HasMaxLength(450);
                entity.Property(e => e.FilePath).HasMaxLength(450);
                entity.Property(e => e.SubmissTime).HasColumnType("datetime");

                entity.HasOne(e => e.ListAssigns)
                    .WithMany(e => e.ListFiles)
                    .HasForeignKey(e => new { e.UserId, e.AssignId, e.LoaiId })
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
    
}
