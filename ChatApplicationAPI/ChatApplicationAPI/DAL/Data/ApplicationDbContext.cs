using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.SeedUsers(modelBuilder);
            this.SeedRoles(modelBuilder);
            this.SeedUserRoles(modelBuilder);
            //MessageTable
            modelBuilder.Entity<Message>()
               .HasOne(cb => cb.Sender)
               .WithMany()
               .HasForeignKey(cb => cb.SenderId)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Message>()
               .HasOne(cb => cb.Receiver)
               .WithMany()
               .HasForeignKey(cb => cb.ReceiverId)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Message>()
               .HasOne(cb => cb.Group)
               .WithMany()
               .HasForeignKey(cb => cb.GroupId)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Message>()
              .HasOne(cb => cb.Group)
              .WithMany()
              .HasForeignKey(cb => cb.GroupId)
              .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Message>()
              .HasOne(cb => cb.Attachment)
              .WithMany()
              .HasForeignKey(cb => cb.AttachmentId)
              .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Message>().Property(e => e.Content).IsRequired(false);


            //UserGroupTable
            modelBuilder.Entity<UserGroup>()
           .HasKey(e => new { e.UserId, e.GroupId }); // Composite primary key

            modelBuilder.Entity<UserGroup>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserGroup>()
                .HasOne(e => e.Group)
                .WithMany()
                .HasForeignKey(e => e.GroupId)
                .OnDelete(DeleteBehavior.NoAction);

            //MutualRelationTable
            modelBuilder.Entity<MutualRelation>()
          .HasKey(e => new {e.MutualId, e.UserId});

            modelBuilder.Entity<MutualRelation>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MutualRelation>()
                .HasOne(e => e.Mutual)
                .WithMany()
                .HasForeignKey(e => e.MutualId)
                .OnDelete(DeleteBehavior.NoAction);

            //RequestTable
            modelBuilder.Entity<Request>()
               .HasOne(e => e.Sender)
               .WithMany()
               .HasForeignKey(e => e.SenderId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Request>()
                .HasOne(e => e.Receiver)
                .WithMany()
                .HasForeignKey(e => e.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);


        }

        private void SeedUsers(ModelBuilder builder)
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            builder.Entity<User>().Property(p => p.Name).HasMaxLength(255).IsRequired();
            User Admin = new User()
            {
                Id = "1",
                Name = "Admin",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                LockoutEnabled = true,
                PasswordHash = passwordHasher.HashPassword(null, "Admin@123")
            };
            User user = new User()
            {
                Id = "2",
                Name = "User",
                Email = "user@gmail.com",
                NormalizedEmail = "USER@GMAIL.COM",
                UserName = "user@gmail.com",
                NormalizedUserName = "USER@GMAIL.COM",
                LockoutEnabled = true,
                PasswordHash = passwordHasher.HashPassword(null, "User@123")
            };



            builder.Entity<User>().HasData(Admin);
            builder.Entity<User>().HasData(user);
        }
        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "1", UserId = "1" },
                new IdentityUserRole<string>() { RoleId = "2", UserId = "2" }
                );
        }
        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "1", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new IdentityRole() { Id = "2", Name = "User", ConcurrencyStamp = "2", NormalizedName = "USER" }
                );
        }


        public DbSet<GroupChat> GroupChat { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        public DbSet<MutualRelation> MutualRelations { get; set; }
        public DbSet<Request> Requests { get; set; }
    }
}
