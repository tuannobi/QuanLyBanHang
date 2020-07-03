using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuanLyBanHang.Models
{
    public partial class QuanLyBanHangDbContext : DbContext
    {
        public QuanLyBanHangDbContext()
        {
        }

        public QuanLyBanHangDbContext(DbContextOptions<QuanLyBanHangDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<BinhLuan> BinhLuan { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public virtual DbSet<ChiTietKhuyenMai> ChiTietKhuyenMai { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<KhuyenMai> KhuyenMai { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCap { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }
        public virtual DbSet<PhanLoai> PhanLoai { get; set; }
        public virtual DbSet<PhiShip> PhiShip { get; set; }
        public virtual DbSet<PhieuNhap> PhieuNhap { get; set; }
        public virtual DbSet<PhieuNhapSanPham> PhieuNhapSanPham { get; set; }
        public virtual DbSet<Reply> Reply { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoan { get; set; }
        public virtual DbSet<VaiTro> VaiTro { get; set; }
        public virtual DbSet<ThongTinHoaDon> ThongTinHoaDon { get; set; }
        public object HttpContext { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;database=quanlybanhang;user=root;password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Admin__A9D10534EA75C671")
                    .IsUnique();

                entity.HasIndex(e => e.Sdt)
                    .HasName("UQ__Admin__CA1930A56CCDF7B7")
                    .IsUnique();

                entity.Property(e => e.Adminid).HasColumnName("adminid");

                entity.Property(e => e.DiaChi)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HoTen)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.TaiKhoan)
                    .WithMany(p => p.Admin)
                    .HasForeignKey(d => d.TaiKhoanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKAdmin457962");
            });

            modelBuilder.Entity<BinhLuan>(entity =>
            {
                entity.Property(e => e.KhachHangId).HasColumnName("khachHangId");

                entity.Property(e => e.NoiDung).HasColumnType("text");

                entity.Property(e => e.ThoiGian).HasColumnType("datetime");

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.BinhLuan)
                    .HasForeignKey(d => d.KhachHangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKBinhLuan571794");

                entity.HasOne(d => d.SanPham)
                    .WithMany(p => p.BinhLuan)
                    .HasForeignKey(d => d.SanPhamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKBinhLuan224061");
            });

            modelBuilder.Entity<ChiTietHoaDon>(entity =>
            {
                entity.HasKey(e => new { e.HoaDonId, e.SanPhamId })
                    .HasName("PK__ChiTietH__39074EB45F6CE441");

                entity.HasOne(d => d.HoaDon)
                    .WithMany(p => p.ChiTietHoaDon)
                    .HasForeignKey(d => d.HoaDonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKChiTietHoa204072");

                entity.HasOne(d => d.SanPham)
                    .WithMany(p => p.ChiTietHoaDon)
                    .HasForeignKey(d => d.SanPhamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKChiTietHoa598700");
            });

            modelBuilder.Entity<ChiTietKhuyenMai>(entity =>
            {
                entity.HasKey(e => new { e.SanPhamId, e.KhuyenMaiId })
                    .HasName("PK__ChiTietK__6D38D89D58D447C3");

                entity.HasOne(d => d.KhuyenMai)
                    .WithMany(p => p.ChiTietKhuyenMai)
                    .HasForeignKey(d => d.KhuyenMaiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKChiTietKhu747862");

                entity.HasOne(d => d.SanPham)
                    .WithMany(p => p.ChiTietKhuyenMai)
                    .HasForeignKey(d => d.SanPhamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKChiTietKhu316972");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.Property(e => e.GhiChu).HasColumnType("text");

                entity.Property(e => e.PhuongThucThanhToan)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Quan)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SoNha)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ThoiGianChoXuLy).HasColumnType("datetime");

                entity.Property(e => e.ThoiGianDaXuLy).HasColumnType("datetime");

                entity.Property(e => e.TrangThai)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.HoaDon)
                    .HasForeignKey(d => d.KhachHangId)
                    .HasConstraintName("FKHoaDon363575");

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.HoaDon)
                    .HasForeignKey(d => d.NhanVienId)
                    .HasConstraintName("FKHoaDon185377");

                entity.HasOne(d => d.PhiShip)
                    .WithMany(p => p.HoaDon)
                    .HasForeignKey(d => d.PhiShipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKHoaDon241522");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__KhachHan__A9D105341FFA78AE")
                    .IsUnique();

                entity.HasIndex(e => e.Sdt)
                    .HasName("UQ__KhachHan__CA1930A517AC4D6D")
                    .IsUnique();

                entity.Property(e => e.KhachHangId).HasColumnName("khachHangId");

                entity.Property(e => e.DiaChi)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HoTen)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.TaiKhoan)
                    .WithMany(p => p.KhachHang)
                    .HasForeignKey(d => d.TaiKhoanId)
                    .HasConstraintName("FKKhachHang993389");
            });

            modelBuilder.Entity<KhuyenMai>(entity =>
            {
                entity.HasIndex(e => e.TenKhuyenMai)
                    .HasName("UQ__KhuyenMa__A956B87CF74F314B")
                    .IsUnique();

                entity.Property(e => e.MoTa).HasColumnType("text");

                entity.Property(e => e.NgayBatDau).HasColumnType("datetime");

                entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");

                entity.Property(e => e.TenKhuyenMai)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__NhaCungC__AB6E61643EB18DE8")
                    .IsUnique();

                entity.HasIndex(e => e.Sdt)
                    .HasName("UQ__NhaCungC__CA1930A5258026F9")
                    .IsUnique();

                entity.Property(e => e.DiaChi)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.TenNhaCungCap)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__NhanVien__A9D10534CD4DAFE8")
                    .IsUnique();

                entity.HasIndex(e => e.Sdt)
                    .HasName("UQ__NhanVien__CA1930A5F4727F15")
                    .IsUnique();

                entity.Property(e => e.DiaChi)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HoTen)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.TaiKhoan)
                    .WithMany(p => p.NhanVien)
                    .HasForeignKey(d => d.TaiKhoanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKNhanVien22888");
            });

            modelBuilder.Entity<PhanLoai>(entity =>
            {
                entity.HasIndex(e => e.Loai)
                    .HasName("UQ__PhanLoai__4E48BB75914F482E")
                    .IsUnique();

                entity.Property(e => e.Loai)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MoTa).HasColumnType("text");

                entity.HasOne(d => d.NhomLoaiNavigation)
                    .WithMany(p => p.InverseNhomLoaiNavigation)
                    .HasForeignKey(d => d.NhomLoai)
                    .HasConstraintName("FKPhanLoai664321");
            });

            modelBuilder.Entity<PhiShip>(entity =>
            {
                entity.HasIndex(e => e.Quan)
                    .HasName("UQ__PhiShip__D06970C39D3C794E")
                    .IsUnique();

                entity.Property(e => e.Quan)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PhieuNhap>(entity =>
            {
                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.HasOne(d => d.NhaCungCap)
                    .WithMany(p => p.PhieuNhap)
                    .HasForeignKey(d => d.NhaCungCapId)
                    .HasConstraintName("FKPhieuNhap473390");
            });

            modelBuilder.Entity<PhieuNhapSanPham>(entity =>
            {
                entity.HasKey(e => new { e.PhieuNhapId, e.SanPhamId })
                    .HasName("PK__PhieuNha__8E6BB81F618F9E59");

                entity.ToTable("PhieuNhap_SanPham");

                entity.HasOne(d => d.PhieuNhap)
                    .WithMany(p => p.PhieuNhapSanPham)
                    .HasForeignKey(d => d.PhieuNhapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPhieuNhap_864668");

                entity.HasOne(d => d.SanPham)
                    .WithMany(p => p.PhieuNhapSanPham)
                    .HasForeignKey(d => d.SanPhamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPhieuNhap_301200");
            });

            modelBuilder.Entity<Reply>(entity =>
            {
                entity.Property(e => e.KhachHangId).HasColumnName("khachHangId");

                entity.Property(e => e.NoiDung).HasColumnType("text");

                entity.Property(e => e.ThoiGian).HasColumnType("datetime");

                entity.HasOne(d => d.BinhLuan)
                    .WithMany(p => p.Reply)
                    .HasForeignKey(d => d.BinhLuanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKReply278339");

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.Reply)
                    .HasForeignKey(d => d.KhachHangId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKReply816004");
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasIndex(e => e.TenSanPham)
                    .HasName("UQ__SanPham__FCA804697CFD3704")
                    .IsUnique();

                entity.Property(e => e.HinhAnh).HasColumnType("text");

                entity.Property(e => e.TenSanPham)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TrangThai)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.PhanLoai)
                    .WithMany(p => p.SanPham)
                    .HasForeignKey(d => d.PhanLoaiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKSanPham562583");
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasIndex(e => e.Username)
                    .HasName("UQ__TaiKhoan__F3DBC572A462F274")
                    .IsUnique();

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.VaiTro)
                    .WithMany(p => p.TaiKhoan)
                    .HasForeignKey(d => d.VaiTroId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTaiKhoan804946");
            });

            modelBuilder.Entity<VaiTro>(entity =>
            {
                entity.HasIndex(e => e.MoTa)
                    .HasName("UQ__VaiTro__24B0CA9E54BECCB0")
                    .IsUnique();

                entity.Property(e => e.MoTa)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
