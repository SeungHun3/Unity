using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ChampionCard.Models.Game;

public partial class GameContext : DbContext
{
    //public GameContext()
    //{
    //}

    public GameContext(DbContextOptions<GameContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDeck> UserDecks { get; set; }

    public virtual DbSet<UserInventory> UserInventories { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySQL("server=192.168.0.3;user=mongs;password=mongs12#$;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.AccountSn).HasName("PRIMARY");

            entity.ToTable("User", "CC_Game");

            entity.Property(e => e.AccountSn).HasColumnName("AccountSN");
            entity.Property(e => e.LastEnergyUpdateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserDeck>(entity =>
        {
            entity.HasKey(e => e.AccountSn).HasName("PRIMARY");

            entity.ToTable("UserDeck", "CC_Game");

            entity.Property(e => e.AccountSn).HasColumnName("AccountSN");
        });

        modelBuilder.Entity<UserInventory>(entity =>
        {
            entity.HasKey(e => e.AccountSn).HasName("PRIMARY");

            entity.ToTable("UserInventory", "CC_Game");

            entity.Property(e => e.AccountSn).HasColumnName("AccountSN");
            entity.Property(e => e.ItemSn).HasColumnName("ItemSN");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
