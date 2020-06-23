﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Migrations
{
    [DbContext(typeof(QuanLyBanHangDbContext))]
    [Migration("20200622104959_Add ThuongHieu")]
    partial class AddThuongHieu
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("QuanLyBanHang.Models.Admin", b =>
                {
                    b.Property<int>("Adminid")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("adminid")
                        .HasColumnType("int");

                    b.Property<string>("DiaChi")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Email")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("HoTen")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime");

                    b.Property<string>("Sdt")
                        .HasColumnName("SDT")
                        .HasColumnType("varchar(11) CHARACTER SET utf8mb4")
                        .HasMaxLength(11)
                        .IsUnicode(false);

                    b.Property<int>("TaiKhoanId")
                        .HasColumnType("int");

                    b.HasKey("Adminid");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("UQ__Admin__A9D10534EA75C671");

                    b.HasIndex("Sdt")
                        .IsUnique()
                        .HasName("UQ__Admin__CA1930A56CCDF7B7");

                    b.HasIndex("TaiKhoanId");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.BinhLuan", b =>
                {
                    b.Property<int>("BinhLuanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("KhachHangId")
                        .HasColumnName("khachHangId")
                        .HasColumnType("int");

                    b.Property<string>("NoiDung")
                        .HasColumnType("text");

                    b.Property<int>("SanPhamId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ThoiGian")
                        .HasColumnType("datetime");

                    b.HasKey("BinhLuanId");

                    b.HasIndex("KhachHangId");

                    b.HasIndex("SanPhamId");

                    b.ToTable("BinhLuan");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.ChiTietHoaDon", b =>
                {
                    b.Property<int>("HoaDonId")
                        .HasColumnType("int");

                    b.Property<int>("SanPhamId")
                        .HasColumnType("int");

                    b.Property<int?>("SoLuong")
                        .HasColumnType("int");

                    b.Property<float?>("TienKhuyenMai")
                        .HasColumnType("float");

                    b.Property<float?>("TongTien")
                        .HasColumnType("float");

                    b.HasKey("HoaDonId", "SanPhamId")
                        .HasName("PK__ChiTietH__39074EB45F6CE441");

                    b.HasIndex("SanPhamId");

                    b.ToTable("ChiTietHoaDon");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.ChiTietKhuyenMai", b =>
                {
                    b.Property<int>("SanPhamId")
                        .HasColumnType("int");

                    b.Property<int>("KhuyenMaiId")
                        .HasColumnType("int");

                    b.Property<float?>("PhanTramGiam")
                        .HasColumnType("float");

                    b.HasKey("SanPhamId", "KhuyenMaiId")
                        .HasName("PK__ChiTietK__6D38D89D58D447C3");

                    b.HasIndex("KhuyenMaiId");

                    b.ToTable("ChiTietKhuyenMai");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.HoaDon", b =>
                {
                    b.Property<int>("HoaDonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("GhiChu")
                        .HasColumnType("text");

                    b.Property<int?>("KhachHangId")
                        .HasColumnType("int");

                    b.Property<int?>("NhanVienId")
                        .HasColumnType("int");

                    b.Property<int>("PhiShipId")
                        .HasColumnType("int");

                    b.Property<string>("PhuongThucThanhToan")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Quan")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("SoNha")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<DateTime?>("ThoiGianChoXuLy")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("ThoiGianDaXuLy")
                        .HasColumnType("datetime");

                    b.Property<float?>("TongTien")
                        .HasColumnType("float");

                    b.Property<float?>("TongTienThanhToan")
                        .HasColumnType("float");

                    b.Property<string>("TrangThai")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("HoaDonId");

                    b.HasIndex("KhachHangId");

                    b.HasIndex("NhanVienId");

                    b.HasIndex("PhiShipId");

                    b.ToTable("HoaDon");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.KhachHang", b =>
                {
                    b.Property<int>("KhachHangId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("khachHangId")
                        .HasColumnType("int");

                    b.Property<string>("DiaChi")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Email")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("HoTen")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime");

                    b.Property<string>("Sdt")
                        .HasColumnName("SDT")
                        .HasColumnType("varchar(11) CHARACTER SET utf8mb4")
                        .HasMaxLength(11)
                        .IsUnicode(false);

                    b.Property<int?>("TaiKhoanId")
                        .HasColumnType("int");

                    b.HasKey("KhachHangId");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("UQ__KhachHan__A9D105341FFA78AE");

                    b.HasIndex("Sdt")
                        .IsUnique()
                        .HasName("UQ__KhachHan__CA1930A517AC4D6D");

                    b.HasIndex("TaiKhoanId");

                    b.ToTable("KhachHang");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.KhuyenMai", b =>
                {
                    b.Property<int>("KhuyenMaiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasColumnType("text");

                    b.Property<DateTime?>("NgayBatDau")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgayKetThuc")
                        .HasColumnType("datetime");

                    b.Property<string>("TenKhuyenMai")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("KhuyenMaiId");

                    b.HasIndex("TenKhuyenMai")
                        .IsUnique()
                        .HasName("UQ__KhuyenMa__A956B87CF74F314B");

                    b.ToTable("KhuyenMai");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.NhaCungCap", b =>
                {
                    b.Property<int>("NhaCungCapId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DiaChi")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Sdt")
                        .HasColumnName("SDT")
                        .HasColumnType("varchar(11) CHARACTER SET utf8mb4")
                        .HasMaxLength(11)
                        .IsUnicode(false);

                    b.Property<string>("TenNhaCungCap")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("NhaCungCapId");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("UQ__NhaCungC__AB6E61643EB18DE8");

                    b.HasIndex("Sdt")
                        .IsUnique()
                        .HasName("UQ__NhaCungC__CA1930A5258026F9");

                    b.ToTable("NhaCungCap");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.NhanVien", b =>
                {
                    b.Property<int>("NhanVienId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DiaChi")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Email")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("HoTen")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime");

                    b.Property<string>("Sdt")
                        .HasColumnName("SDT")
                        .HasColumnType("varchar(11) CHARACTER SET utf8mb4")
                        .HasMaxLength(11)
                        .IsUnicode(false);

                    b.Property<int>("TaiKhoanId")
                        .HasColumnType("int");

                    b.HasKey("NhanVienId");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("UQ__NhanVien__A9D10534CD4DAFE8");

                    b.HasIndex("Sdt")
                        .IsUnique()
                        .HasName("UQ__NhanVien__CA1930A5F4727F15");

                    b.HasIndex("TaiKhoanId");

                    b.ToTable("NhanVien");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.PhanLoai", b =>
                {
                    b.Property<int>("PhanLoaiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Loai")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("MoTa")
                        .HasColumnType("text");

                    b.Property<int?>("NhomLoai")
                        .HasColumnType("int");

                    b.HasKey("PhanLoaiId");

                    b.HasIndex("Loai")
                        .IsUnique()
                        .HasName("UQ__PhanLoai__4E48BB75914F482E");

                    b.HasIndex("NhomLoai");

                    b.ToTable("PhanLoai");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.PhiShip", b =>
                {
                    b.Property<int>("PhiShipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float?>("ChiPhi")
                        .HasColumnType("float");

                    b.Property<string>("Quan")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("PhiShipId");

                    b.HasIndex("Quan")
                        .IsUnique()
                        .HasName("UQ__PhiShip__D06970C39D3C794E");

                    b.ToTable("PhiShip");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.PhieuNhap", b =>
                {
                    b.Property<int>("PhieuNhapId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime");

                    b.Property<int?>("NhaCungCapId")
                        .HasColumnType("int");

                    b.Property<float?>("TongTien")
                        .HasColumnType("float");

                    b.HasKey("PhieuNhapId");

                    b.HasIndex("NhaCungCapId");

                    b.ToTable("PhieuNhap");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.PhieuNhapSanPham", b =>
                {
                    b.Property<int>("PhieuNhapId")
                        .HasColumnType("int");

                    b.Property<int>("SanPhamId")
                        .HasColumnType("int");

                    b.Property<float?>("GiaGoc")
                        .HasColumnType("float");

                    b.Property<int?>("SoLuong")
                        .HasColumnType("int");

                    b.Property<float?>("TongTien")
                        .HasColumnType("float");

                    b.HasKey("PhieuNhapId", "SanPhamId")
                        .HasName("PK__PhieuNha__8E6BB81F618F9E59");

                    b.HasIndex("SanPhamId");

                    b.ToTable("PhieuNhap_SanPham");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.Reply", b =>
                {
                    b.Property<int>("ReplyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BinhLuanId")
                        .HasColumnType("int");

                    b.Property<int>("KhachHangId")
                        .HasColumnName("khachHangId")
                        .HasColumnType("int");

                    b.Property<string>("NoiDung")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ThoiGian")
                        .HasColumnType("datetime");

                    b.HasKey("ReplyId");

                    b.HasIndex("BinhLuanId");

                    b.HasIndex("KhachHangId");

                    b.ToTable("Reply");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.SanPham", b =>
                {
                    b.Property<int>("SanPhamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float?>("GiaBanLe")
                        .HasColumnType("float");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("text");

                    b.Property<float?>("Kho")
                        .HasColumnType("float");

                    b.Property<int>("PhanLoaiId")
                        .HasColumnType("int");

                    b.Property<string>("TenSanPham")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("ThuongHieu")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TrangThai")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("SanPhamId");

                    b.HasIndex("PhanLoaiId");

                    b.HasIndex("TenSanPham")
                        .IsUnique()
                        .HasName("UQ__SanPham__FCA804697CFD3704");

                    b.ToTable("SanPham");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.TaiKhoan", b =>
                {
                    b.Property<int>("TaiKhoanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime");

                    b.Property<string>("Password")
                        .HasColumnName("password")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Username")
                        .HasColumnName("username")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("VaiTroId")
                        .HasColumnType("int");

                    b.HasKey("TaiKhoanId");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasName("UQ__TaiKhoan__F3DBC572A462F274");

                    b.HasIndex("VaiTroId");

                    b.ToTable("TaiKhoan");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.VaiTro", b =>
                {
                    b.Property<int>("VaiTroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("VaiTroId");

                    b.HasIndex("MoTa")
                        .IsUnique()
                        .HasName("UQ__VaiTro__24B0CA9E54BECCB0");

                    b.ToTable("VaiTro");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.Admin", b =>
                {
                    b.HasOne("QuanLyBanHang.Models.TaiKhoan", "TaiKhoan")
                        .WithMany("Admin")
                        .HasForeignKey("TaiKhoanId")
                        .HasConstraintName("FKAdmin457962")
                        .IsRequired();
                });

            modelBuilder.Entity("QuanLyBanHang.Models.BinhLuan", b =>
                {
                    b.HasOne("QuanLyBanHang.Models.KhachHang", "KhachHang")
                        .WithMany("BinhLuan")
                        .HasForeignKey("KhachHangId")
                        .HasConstraintName("FKBinhLuan571794")
                        .IsRequired();

                    b.HasOne("QuanLyBanHang.Models.SanPham", "SanPham")
                        .WithMany("BinhLuan")
                        .HasForeignKey("SanPhamId")
                        .HasConstraintName("FKBinhLuan224061")
                        .IsRequired();
                });

            modelBuilder.Entity("QuanLyBanHang.Models.ChiTietHoaDon", b =>
                {
                    b.HasOne("QuanLyBanHang.Models.HoaDon", "HoaDon")
                        .WithMany("ChiTietHoaDon")
                        .HasForeignKey("HoaDonId")
                        .HasConstraintName("FKChiTietHoa204072")
                        .IsRequired();

                    b.HasOne("QuanLyBanHang.Models.SanPham", "SanPham")
                        .WithMany("ChiTietHoaDon")
                        .HasForeignKey("SanPhamId")
                        .HasConstraintName("FKChiTietHoa598700")
                        .IsRequired();
                });

            modelBuilder.Entity("QuanLyBanHang.Models.ChiTietKhuyenMai", b =>
                {
                    b.HasOne("QuanLyBanHang.Models.KhuyenMai", "KhuyenMai")
                        .WithMany("ChiTietKhuyenMai")
                        .HasForeignKey("KhuyenMaiId")
                        .HasConstraintName("FKChiTietKhu747862")
                        .IsRequired();

                    b.HasOne("QuanLyBanHang.Models.SanPham", "SanPham")
                        .WithMany("ChiTietKhuyenMai")
                        .HasForeignKey("SanPhamId")
                        .HasConstraintName("FKChiTietKhu316972")
                        .IsRequired();
                });

            modelBuilder.Entity("QuanLyBanHang.Models.HoaDon", b =>
                {
                    b.HasOne("QuanLyBanHang.Models.KhachHang", "KhachHang")
                        .WithMany("HoaDon")
                        .HasForeignKey("KhachHangId")
                        .HasConstraintName("FKHoaDon363575");

                    b.HasOne("QuanLyBanHang.Models.NhanVien", "NhanVien")
                        .WithMany("HoaDon")
                        .HasForeignKey("NhanVienId")
                        .HasConstraintName("FKHoaDon185377");

                    b.HasOne("QuanLyBanHang.Models.PhiShip", "PhiShip")
                        .WithMany("HoaDon")
                        .HasForeignKey("PhiShipId")
                        .HasConstraintName("FKHoaDon241522")
                        .IsRequired();
                });

            modelBuilder.Entity("QuanLyBanHang.Models.KhachHang", b =>
                {
                    b.HasOne("QuanLyBanHang.Models.TaiKhoan", "TaiKhoan")
                        .WithMany("KhachHang")
                        .HasForeignKey("TaiKhoanId")
                        .HasConstraintName("FKKhachHang993389");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.NhanVien", b =>
                {
                    b.HasOne("QuanLyBanHang.Models.TaiKhoan", "TaiKhoan")
                        .WithMany("NhanVien")
                        .HasForeignKey("TaiKhoanId")
                        .HasConstraintName("FKNhanVien22888")
                        .IsRequired();
                });

            modelBuilder.Entity("QuanLyBanHang.Models.PhanLoai", b =>
                {
                    b.HasOne("QuanLyBanHang.Models.PhanLoai", "NhomLoaiNavigation")
                        .WithMany("InverseNhomLoaiNavigation")
                        .HasForeignKey("NhomLoai")
                        .HasConstraintName("FKPhanLoai664321");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.PhieuNhap", b =>
                {
                    b.HasOne("QuanLyBanHang.Models.NhaCungCap", "NhaCungCap")
                        .WithMany("PhieuNhap")
                        .HasForeignKey("NhaCungCapId")
                        .HasConstraintName("FKPhieuNhap473390");
                });

            modelBuilder.Entity("QuanLyBanHang.Models.PhieuNhapSanPham", b =>
                {
                    b.HasOne("QuanLyBanHang.Models.PhieuNhap", "PhieuNhap")
                        .WithMany("PhieuNhapSanPham")
                        .HasForeignKey("PhieuNhapId")
                        .HasConstraintName("FKPhieuNhap_864668")
                        .IsRequired();

                    b.HasOne("QuanLyBanHang.Models.SanPham", "SanPham")
                        .WithMany("PhieuNhapSanPham")
                        .HasForeignKey("SanPhamId")
                        .HasConstraintName("FKPhieuNhap_301200")
                        .IsRequired();
                });

            modelBuilder.Entity("QuanLyBanHang.Models.Reply", b =>
                {
                    b.HasOne("QuanLyBanHang.Models.BinhLuan", "BinhLuan")
                        .WithMany("Reply")
                        .HasForeignKey("BinhLuanId")
                        .HasConstraintName("FKReply278339")
                        .IsRequired();

                    b.HasOne("QuanLyBanHang.Models.KhachHang", "KhachHang")
                        .WithMany("Reply")
                        .HasForeignKey("KhachHangId")
                        .HasConstraintName("FKReply816004")
                        .IsRequired();
                });

            modelBuilder.Entity("QuanLyBanHang.Models.SanPham", b =>
                {
                    b.HasOne("QuanLyBanHang.Models.PhanLoai", "PhanLoai")
                        .WithMany("SanPham")
                        .HasForeignKey("PhanLoaiId")
                        .HasConstraintName("FKSanPham562583")
                        .IsRequired();
                });

            modelBuilder.Entity("QuanLyBanHang.Models.TaiKhoan", b =>
                {
                    b.HasOne("QuanLyBanHang.Models.VaiTro", "VaiTro")
                        .WithMany("TaiKhoan")
                        .HasForeignKey("VaiTroId")
                        .HasConstraintName("FKTaiKhoan804946")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
