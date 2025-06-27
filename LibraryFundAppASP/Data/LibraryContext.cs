using System;
using System.Collections.Generic;
using LibraryFundAppASP.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryFundAppASP.Data;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Bookdetail> Bookdetails { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderHasBook> OrderHasBooks { get; set; }

    public virtual DbSet<Visitor> Visitors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;DataBase=libraryfund;user id =root;pwd=root");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.IdAuthor).HasName("PRIMARY");

            entity.ToTable("author");

            entity.HasIndex(e => new { e.Name, e.Surname }, "UQ_Name_Surname").IsUnique();

            entity.Property(e => e.Country).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.IdBook).HasName("PRIMARY");

            entity.ToTable("book");

            entity.HasIndex(e => e.IdAuthor, "fk_book_author_idx");

            entity.Property(e => e.Genre)
                .HasDefaultValueSql("'проза'")
                .HasComment("[0 - 9999.99]")
                .HasColumnType("enum('проза','поэзия','другое')");
            entity.Property(e => e.ReleaseYear).HasColumnType("year");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.IdAuthorNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdAuthor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_book_author");
        });

        modelBuilder.Entity<Bookdetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("bookdetails");

            entity.Property(e => e.Genre)
                .HasDefaultValueSql("'проза'")
                .HasComment("[0 - 9999.99]")
                .HasColumnType("enum('проза','поэзия','другое')");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PRIMARY");

            entity.ToTable("order");

            entity.HasIndex(e => e.IdVisitor, "fk_order_visitor1_idx");

            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdVisitorNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdVisitor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_visitor1");
        });

        modelBuilder.Entity<OrderHasBook>(entity =>
        {
            entity.HasKey(e => new { e.IdOrder, e.IdBook }).HasName("PRIMARY");

            entity.ToTable("order_has_book");

            entity.HasIndex(e => e.Amount, "Amount_UNIQUE").IsUnique();

            entity.HasIndex(e => e.IdBook, "fk_order_has_book_book1_idx");

            entity.HasIndex(e => e.IdOrder, "fk_order_has_book_order1_idx");

            entity.Property(e => e.Amount).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.OrderHasBooks)
                .HasForeignKey(d => d.IdBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_has_book_book1");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.OrderHasBooks)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_has_book_order1");
        });

        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.HasKey(e => e.IdVisitor).HasName("PRIMARY");

            entity.ToTable("visitor");

            entity.Property(e => e.Class).HasMaxLength(3);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
