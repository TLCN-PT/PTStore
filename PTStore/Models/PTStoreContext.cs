using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace PTStore.Models
{
    public partial class PTStoreContext : DbContext
    {
        public PTStoreContext()
        {
        }

        public PTStoreContext(DbContextOptions<PTStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<DienThoai> DienThoais { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<GopY> Gopies { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Subcriber> Subcribers { get; set; }
        public virtual DbSet<ThongSoKyThuat> ThongSoKyThuats { get; set; }
        public virtual DbSet<ThuongHieu> ThuongHieus { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("PTStoreDatabase"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.MatKhau).IsUnicode(false);

                entity.Property(e => e.TenDangNhap).IsUnicode(false);

                entity.Property(e => e.TrangThai).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Account__UserId__398D8EEE");
            });

            modelBuilder.Entity<ChiTietDonHang>(entity =>
            {
                entity.HasKey(e => new { e.DonHangId, e.DienThoaiId })
                    .HasName("PK__ChiTietD__5230C870CA3FEFF2");

                entity.ToTable("ChiTietDonHang");

                entity.HasOne(d => d.DienThoai)
                    .WithMany(p => p.ChiTietDonHangs)
                    .HasForeignKey(d => d.DienThoaiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietDo__DienT__3B75D760");

                entity.HasOne(d => d.DonHang)
                    .WithMany(p => p.ChiTietDonHangs)
                    .HasForeignKey(d => d.DonHangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ChiTietDo__DonHa__3A81B327");
            });

            modelBuilder.Entity<DienThoai>(entity =>
            {
                entity.ToTable("DienThoai");

                entity.Property(e => e.HinhAnh).IsUnicode(false);

                entity.Property(e => e.TinhTrang).IsUnicode(false);

                entity.HasOne(d => d.ThongSoKyThuat)
                    .WithMany(p => p.DienThoais)
                    .HasForeignKey(d => d.ThongSoKyThuatId)
                    .HasConstraintName("FK__DienThoai__Thong__3C69FB99");

                entity.HasOne(d => d.ThuongHieu)
                    .WithMany(p => p.DienThoais)
                    .HasForeignKey(d => d.ThuongHieuId)
                    .HasConstraintName("FK__DienThoai__Thuon__3C69FB99");
            });

            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.ToTable("DonHang");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.HinhThucThanhToan).IsUnicode(false);

                entity.Property(e => e.MaDonHang).IsUnicode(false);

                entity.Property(e => e.NgayDatHang).HasColumnType("date");

                entity.Property(e => e.NgayDuKienGiao).HasColumnType("date");

                entity.Property(e => e.SoDienThoai).IsUnicode(false);

                entity.Property(e => e.TrangTrai).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DonHangs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__DonHang__UserId__3E52440B");
            });

            modelBuilder.Entity<GopY>(entity =>
            {
                entity.ToTable("GopY");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleName).IsUnicode(false);
            });

            modelBuilder.Entity<Subcriber>(entity =>
            {
                entity.ToTable("Subcriber");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.TrangThai)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ThongSoKyThuat>(entity =>
            {
                entity.ToTable("ThongSoKyThuat");

                entity.Property(e => e.Cpu).HasColumnName("CPU");

                entity.Property(e => e.NgayRaMat).HasColumnType("date");

                entity.Property(e => e.Ram).HasColumnName("RAM");

                entity.HasOne(d => d.DienThoai)
                    .WithMany(p => p.ThongSoKyThuats)
                    .HasForeignKey(d => d.DienThoaiId)
                    .HasConstraintName("FK__ThongSoKy__DienT__3F466844");
            });

            modelBuilder.Entity<ThuongHieu>(entity =>
            {
                entity.ToTable("ThuongHieu");

                entity.Property(e => e.HinhAnhThuongHieu).IsUnicode(false);

                entity.Property(e => e.TenThuongHieu)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.SoDienThoai).IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Users__AccountId__4222D4EF");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK__UserRole__AF2760AD7E056BDC");

                entity.ToTable("UserRole");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRole__RoleId__412EB0B6");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRole__UserId__403A8C7D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
