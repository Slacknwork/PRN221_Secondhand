using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PRN221_Secondhand.Models
{
    public partial class SecondhandContext : DbContext
    {
        public SecondhandContext()
        {
        }

        public SecondhandContext(DbContextOptions<SecondhandContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TagPost> TagPosts { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Wish> Wishes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =(local); database = Secondhand;uid=sa;pwd=sa;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.Id)
                    .HasMaxLength(16)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id)
                    .HasMaxLength(16)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Image).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Id)
                    .HasMaxLength(16)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(16)
                    .HasColumnName("Category_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Image).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Post__Category_I__60A75C0F");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.Property(e => e.Id)
                    .HasMaxLength(16)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.Value).HasMaxLength(50);
            });

            modelBuilder.Entity<TagPost>(entity =>
            {
                entity.ToTable("Tag_Post");

                entity.Property(e => e.Id)
                    .HasMaxLength(16)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.PostId)
                    .HasMaxLength(16)
                    .HasColumnName("Post_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.TagId)
                    .HasMaxLength(16)
                    .HasColumnName("Tag_ID")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.TagPosts)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Tag_Post__Post_I__693CA210");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.TagPosts)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK__Tag_Post__Tag_ID__6A30C649");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id)
                    .HasMaxLength(16)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Avatar).HasColumnType("text");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ExternalLoginId)
                    .HasMaxLength(16)
                    .HasColumnName("EXTERNAL_LOGIN_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Updated).HasColumnType("datetime");
            });

            modelBuilder.Entity<Wish>(entity =>
            {
                entity.ToTable("Wish");

                entity.Property(e => e.Id)
                    .HasMaxLength(16)
                    .HasColumnName("ID")
                    .IsFixedLength(true);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.PostId)
                    .HasMaxLength(16)
                    .HasColumnName("Post_ID")
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .HasMaxLength(16)
                    .HasColumnName("User_ID")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Wishes)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Wish__Post_ID__6477ECF3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wishes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Wish__User_ID__6383C8BA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
