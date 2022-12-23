using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace gNetComApi.Models
{
    public partial class josericardo3_Context : DbContext
    {
        public josericardo3_Context()
        {
        }

        public josericardo3_Context(DbContextOptions<josericardo3_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Commentary> Commentaries { get; set; } = null!;
        public virtual DbSet<Friend> Friends { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Route> Routes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRoute> UserRoutes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=sql.bsite.net\\MSSQL2016;Database=josericardo3_;user id=josericardo3_;password=Database2022;Integrated Security=False;Trusted_Connection=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Commentary>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.ToTable("commentary");

                entity.Property(e => e.CommentId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("comment_id");

                entity.Property(e => e.Body)
                    .HasColumnType("text")
                    .HasColumnName("body");

                entity.Property(e => e.PostId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("post_id");

                entity.Property(e => e.UserId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Commentaries)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_commentary_post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Commentaries)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_commentary_user");
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.ToTable("friends");

                entity.Property(e => e.FriendId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("friend_id");

                entity.Property(e => e.FromUserId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("from_user_id");

                entity.Property(e => e.IsReqAccepted).HasColumnName("is_req_accepted");

                entity.Property(e => e.ToUserId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("to_user_id");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.FriendFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .HasConstraintName("FK_friends_user");

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.FriendToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .HasConstraintName("FK_friends_user1");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");

                entity.Property(e => e.PostId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("post_id");

                entity.Property(e => e.Body)
                    .HasColumnType("text")
                    .HasColumnName("body");

                entity.Property(e => e.Image)
                    .HasColumnType("text")
                    .HasColumnName("image");

                entity.Property(e => e.RouteId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("route_id");

                entity.Property(e => e.Title)
                    .HasColumnType("text")
                    .HasColumnName("title");

                entity.Property(e => e.UserId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.RouteId)
                    .HasConstraintName("FK_post_route");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_post_user");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.ToTable("route");

                entity.Property(e => e.RouteId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("route_id");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasColumnName("date_created");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.LatitudeFin).HasColumnName("latitude_fin");

                entity.Property(e => e.LatitudeInit).HasColumnName("latitude_init");

                entity.Property(e => e.LongitudeFin).HasColumnName("longitude_fin");

                entity.Property(e => e.LongitudeInit).HasColumnName("longitude_init");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.UserOwner)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("user_owner");

                entity.HasOne(d => d.UserOwnerNavigation)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.UserOwner)
                    .HasConstraintName("user_owner");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("user_id");

                entity.Property(e => e.CellNumber)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("cell_number");

                entity.Property(e => e.LastName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.UserName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("user_name");
            });

            modelBuilder.Entity<UserRoute>(entity =>
            {
                entity.ToTable("user_route");

                entity.Property(e => e.Id)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.RouteId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("route_id");

                entity.Property(e => e.UserId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.UserRoutes)
                    .HasForeignKey(d => d.RouteId)
                    .HasConstraintName("FK_user_route_route");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoutes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_user_route_user");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
