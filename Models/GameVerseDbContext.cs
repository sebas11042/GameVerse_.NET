using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GameVerse.Models;

public partial class GameVerseDbContext : DbContext
{
    public GameVerseDbContext()
    {
    }

    public GameVerseDbContext(DbContextOptions<GameVerseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; }

    public virtual DbSet<Rental> Rentals { get; set; }

    public virtual DbSet<RentalDetail> RentalDetails { get; set; }

    public virtual DbSet<Shopping> Shoppings { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=GameVerseDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__Categori__E548B673EF47F7E4");

            entity.Property(e => e.IdCategory)
                .ValueGeneratedNever()
                .HasColumnName("id_category");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.IdGame).HasName("PK__Games__0E819B2CC35EB4EF");

            entity.Property(e => e.IdGame)
                .ValueGeneratedNever()
                .HasColumnName("id_game");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.PriceBuy).HasColumnName("price_buy");
            entity.Property(e => e.PriceRental).HasColumnName("price_rental");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");

            entity.HasMany(d => d.IdCategories).WithMany(p => p.IdGames)
                .UsingEntity<Dictionary<string, object>>(
                    "GamesCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_GC_Category"),
                    l => l.HasOne<Game>().WithMany()
                        .HasForeignKey("IdGame")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_GC_Game"),
                    j =>
                    {
                        j.HasKey("IdGame", "IdCategory").HasName("PK_GamesCategory");
                        j.ToTable("Games_Category");
                        j.IndexerProperty<int>("IdGame").HasColumnName("id_game");
                        j.IndexerProperty<int>("IdCategory").HasColumnName("id_category");
                    });
        });

        modelBuilder.Entity<PurchaseDetail>(entity =>
        {
            entity.HasKey(e => new { e.IdBuy, e.IdGame }).HasName("PK_Purchase");

            entity.ToTable("Purchase_Detail");

            entity.Property(e => e.IdBuy).HasColumnName("id_buy");
            entity.Property(e => e.IdGame).HasColumnName("id_game");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.IdBuyNavigation).WithMany(p => p.PurchaseDetails)
                .HasForeignKey(d => d.IdBuy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PD_Buy");

            entity.HasOne(d => d.IdGameNavigation).WithMany(p => p.PurchaseDetails)
                .HasForeignKey(d => d.IdGame)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PD_Game");
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.IdRent).HasName("PK__Rentals__0F4BF3B061394CDA");

            entity.Property(e => e.IdRent)
                .ValueGeneratedNever()
                .HasColumnName("id_rent");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.RentDays).HasColumnName("rent_days");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Rentals_User");
        });

        modelBuilder.Entity<RentalDetail>(entity =>
        {
            entity.HasKey(e => new { e.IdRental, e.IdGame }).HasName("PK_RentalDetail");

            entity.ToTable("Rental_Detail");

            entity.Property(e => e.IdRental).HasColumnName("id_rental");
            entity.Property(e => e.IdGame).HasColumnName("id_game");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.ExpireDate).HasColumnName("expire_date");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.RentalDate).HasColumnName("rental_date");

            entity.HasOne(d => d.IdGameNavigation).WithMany(p => p.RentalDetails)
                .HasForeignKey(d => d.IdGame)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RD_Game");

            entity.HasOne(d => d.IdRentalNavigation).WithMany(p => p.RentalDetails)
                .HasForeignKey(d => d.IdRental)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RD_Rental");
        });

        modelBuilder.Entity<Shopping>(entity =>
        {
            entity.HasKey(e => e.IdBuy).HasName("PK__Shopping__D507E43F2007AD30");

            entity.ToTable("Shopping");

            entity.Property(e => e.IdBuy)
                .ValueGeneratedNever()
                .HasColumnName("id_buy");
            entity.Property(e => e.BuyDate).HasColumnName("buy_date");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Shoppings)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Shopping_User");
        });

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(e => new { e.IdUser, e.IdGame }).HasName("PK_ShoppingCart");

            entity.ToTable("Shopping_Cart");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IdGame).HasColumnName("id_game");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.IdGameNavigation).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.IdGame)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SC_Game");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SC_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__D2D14637154F8C6B");

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnName("id_user");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Rol)
                .HasMaxLength(255)
                .HasColumnName("rol");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => new { e.IdUser, e.IdGame });

            entity.ToTable("Wishlist");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.IdGame).HasColumnName("id_game");
            entity.Property(e => e.AddedAt).HasColumnName("added_at");

            entity.HasOne(d => d.IdGameNavigation).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.IdGame)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WL_Game");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WL_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
