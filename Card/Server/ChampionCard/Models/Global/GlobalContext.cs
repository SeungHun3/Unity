using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ChampionCard.Models.Global;

public partial class GlobalContext : DbContext
{
    //public GlobalContext()
    //{
    //}

    public GlobalContext(DbContextOptions<GlobalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<PlatformLink> PlatformLinks { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySQL("server=192.168.0.3;user=mongs;password=mongs12#$;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountSn).HasName("PRIMARY");

            entity.ToTable("Account", "CC_Global", tb => tb.HasComment("	"));

            entity.HasIndex(e => e.NickName, "NickName").IsUnique();

            entity.Property(e => e.AccountSn)
                .HasDefaultValueSql("'1000000'")
                .HasColumnName("AccountSN");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.LastLoginAt).HasColumnType("datetime");
            entity.Property(e => e.NickName).HasMaxLength(100);
        });

        modelBuilder.Entity<PlatformLink>(entity =>
        {
            entity.HasKey(e => e.Sn).HasName("PRIMARY");

            entity.ToTable("PlatformLink", "CC_Global");

            entity.HasIndex(e => e.AccountSn, "AccountSN");

            entity.HasIndex(e => new { e.PlatformId, e.PlatformType }, "PlatformType&PlatformID");

            entity.Property(e => e.Sn).HasColumnName("sn");
            entity.Property(e => e.AccountSn)
                .HasComment("Account SN ")
                .HasColumnName("AccountSN");
            entity.Property(e => e.PlatformId).HasMaxLength(100);
            entity.Property(e => e.PlatformType).HasComment("플랫폼별 키");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
