using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyBanHang.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhuyenMai",
                columns: table => new
                {
                    KhuyenMaiId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenKhuyenMai = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhuyenMai", x => x.KhuyenMaiId);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCap",
                columns: table => new
                {
                    NhaCungCapId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenNhaCungCap = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    DiaChi = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    email = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    SDT = table.Column<string>(unicode: false, maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCap", x => x.NhaCungCapId);
                });

            migrationBuilder.CreateTable(
                name: "PhanLoai",
                columns: table => new
                {
                    PhanLoaiId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Loai = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    NhomLoai = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanLoai", x => x.PhanLoaiId);
                    table.ForeignKey(
                        name: "FKPhanLoai664321",
                        column: x => x.NhomLoai,
                        principalTable: "PhanLoai",
                        principalColumn: "PhanLoaiId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhiShip",
                columns: table => new
                {
                    PhiShipId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Quan = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ChiPhi = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhiShip", x => x.PhiShipId);
                });

            migrationBuilder.CreateTable(
                name: "VaiTro",
                columns: table => new
                {
                    VaiTroId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MoTa = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTro", x => x.VaiTroId);
                });

            migrationBuilder.CreateTable(
                name: "PhieuNhap",
                columns: table => new
                {
                    PhieuNhapId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NhaCungCapId = table.Column<int>(nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    TongTien = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuNhap", x => x.PhieuNhapId);
                    table.ForeignKey(
                        name: "FKPhieuNhap473390",
                        column: x => x.NhaCungCapId,
                        principalTable: "NhaCungCap",
                        principalColumn: "NhaCungCapId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    SanPhamId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenSanPham = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    GiaBanLe = table.Column<float>(nullable: true),
                    Kho = table.Column<float>(nullable: true),
                    HinhAnh = table.Column<string>(type: "text", nullable: true),
                    TrangThai = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    PhanLoaiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.SanPhamId);
                    table.ForeignKey(
                        name: "FKSanPham562583",
                        column: x => x.PhanLoaiId,
                        principalTable: "PhanLoai",
                        principalColumn: "PhanLoaiId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    TaiKhoanId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    password = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    VaiTroId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.TaiKhoanId);
                    table.ForeignKey(
                        name: "FKTaiKhoan804946",
                        column: x => x.VaiTroId,
                        principalTable: "VaiTro",
                        principalColumn: "VaiTroId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietKhuyenMai",
                columns: table => new
                {
                    SanPhamId = table.Column<int>(nullable: false),
                    KhuyenMaiId = table.Column<int>(nullable: false),
                    PhanTramGiam = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietK__6D38D89D58D447C3", x => new { x.SanPhamId, x.KhuyenMaiId });
                    table.ForeignKey(
                        name: "FKChiTietKhu747862",
                        column: x => x.KhuyenMaiId,
                        principalTable: "KhuyenMai",
                        principalColumn: "KhuyenMaiId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKChiTietKhu316972",
                        column: x => x.SanPhamId,
                        principalTable: "SanPham",
                        principalColumn: "SanPhamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhieuNhap_SanPham",
                columns: table => new
                {
                    PhieuNhapId = table.Column<int>(nullable: false),
                    SanPhamId = table.Column<int>(nullable: false),
                    GiaGoc = table.Column<float>(nullable: true),
                    SoLuong = table.Column<int>(nullable: true),
                    TongTien = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhieuNha__8E6BB81F618F9E59", x => new { x.PhieuNhapId, x.SanPhamId });
                    table.ForeignKey(
                        name: "FKPhieuNhap_864668",
                        column: x => x.PhieuNhapId,
                        principalTable: "PhieuNhap",
                        principalColumn: "PhieuNhapId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKPhieuNhap_301200",
                        column: x => x.SanPhamId,
                        principalTable: "SanPham",
                        principalColumn: "SanPhamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    adminid = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HoTen = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SDT = table.Column<string>(unicode: false, maxLength: 11, nullable: true),
                    DiaChi = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    TaiKhoanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.adminid);
                    table.ForeignKey(
                        name: "FKAdmin457962",
                        column: x => x.TaiKhoanId,
                        principalTable: "TaiKhoan",
                        principalColumn: "TaiKhoanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    khachHangId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HoTen = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SDT = table.Column<string>(unicode: false, maxLength: 11, nullable: true),
                    DiaChi = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    TaiKhoanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.khachHangId);
                    table.ForeignKey(
                        name: "FKKhachHang993389",
                        column: x => x.TaiKhoanId,
                        principalTable: "TaiKhoan",
                        principalColumn: "TaiKhoanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    NhanVienId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HoTen = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime", nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SDT = table.Column<string>(unicode: false, maxLength: 11, nullable: true),
                    DiaChi = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    TaiKhoanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.NhanVienId);
                    table.ForeignKey(
                        name: "FKNhanVien22888",
                        column: x => x.TaiKhoanId,
                        principalTable: "TaiKhoan",
                        principalColumn: "TaiKhoanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BinhLuan",
                columns: table => new
                {
                    BinhLuanId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    khachHangId = table.Column<int>(nullable: false),
                    SanPhamId = table.Column<int>(nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime", nullable: false),
                    NoiDung = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinhLuan", x => x.BinhLuanId);
                    table.ForeignKey(
                        name: "FKBinhLuan571794",
                        column: x => x.khachHangId,
                        principalTable: "KhachHang",
                        principalColumn: "khachHangId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKBinhLuan224061",
                        column: x => x.SanPhamId,
                        principalTable: "SanPham",
                        principalColumn: "SanPhamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    HoaDonId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PhuongThucThanhToan = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    SoNha = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Quan = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    PhiShipId = table.Column<int>(nullable: false),
                    KhachHangId = table.Column<int>(nullable: true),
                    NhanVienId = table.Column<int>(nullable: true),
                    TongTien = table.Column<float>(nullable: true),
                    TongTienThanhToan = table.Column<float>(nullable: true),
                    GhiChu = table.Column<string>(type: "text", nullable: true),
                    TrangThai = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ThoiGianChoXuLy = table.Column<DateTime>(type: "datetime", nullable: true),
                    ThoiGianDaXuLy = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.HoaDonId);
                    table.ForeignKey(
                        name: "FKHoaDon363575",
                        column: x => x.KhachHangId,
                        principalTable: "KhachHang",
                        principalColumn: "khachHangId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKHoaDon185377",
                        column: x => x.NhanVienId,
                        principalTable: "NhanVien",
                        principalColumn: "NhanVienId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKHoaDon241522",
                        column: x => x.PhiShipId,
                        principalTable: "PhiShip",
                        principalColumn: "PhiShipId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reply",
                columns: table => new
                {
                    ReplyId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BinhLuanId = table.Column<int>(nullable: false),
                    khachHangId = table.Column<int>(nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime", nullable: true),
                    NoiDung = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reply", x => x.ReplyId);
                    table.ForeignKey(
                        name: "FKReply278339",
                        column: x => x.BinhLuanId,
                        principalTable: "BinhLuan",
                        principalColumn: "BinhLuanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKReply816004",
                        column: x => x.khachHangId,
                        principalTable: "KhachHang",
                        principalColumn: "khachHangId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDon",
                columns: table => new
                {
                    HoaDonId = table.Column<int>(nullable: false),
                    SanPhamId = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: true),
                    TienKhuyenMai = table.Column<float>(nullable: true),
                    TongTien = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietH__39074EB45F6CE441", x => new { x.HoaDonId, x.SanPhamId });
                    table.ForeignKey(
                        name: "FKChiTietHoa204072",
                        column: x => x.HoaDonId,
                        principalTable: "HoaDon",
                        principalColumn: "HoaDonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKChiTietHoa598700",
                        column: x => x.SanPhamId,
                        principalTable: "SanPham",
                        principalColumn: "SanPhamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Admin__A9D10534EA75C671",
                table: "Admin",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Admin__CA1930A56CCDF7B7",
                table: "Admin",
                column: "SDT",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admin_TaiKhoanId",
                table: "Admin",
                column: "TaiKhoanId");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuan_khachHangId",
                table: "BinhLuan",
                column: "khachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuan_SanPhamId",
                table: "BinhLuan",
                column: "SanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_SanPhamId",
                table: "ChiTietHoaDon",
                column: "SanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietKhuyenMai_KhuyenMaiId",
                table: "ChiTietKhuyenMai",
                column: "KhuyenMaiId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_KhachHangId",
                table: "HoaDon",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_NhanVienId",
                table: "HoaDon",
                column: "NhanVienId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_PhiShipId",
                table: "HoaDon",
                column: "PhiShipId");

            migrationBuilder.CreateIndex(
                name: "UQ__KhachHan__A9D105341FFA78AE",
                table: "KhachHang",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__KhachHan__CA1930A517AC4D6D",
                table: "KhachHang",
                column: "SDT",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_TaiKhoanId",
                table: "KhachHang",
                column: "TaiKhoanId");

            migrationBuilder.CreateIndex(
                name: "UQ__KhuyenMa__A956B87CF74F314B",
                table: "KhuyenMai",
                column: "TenKhuyenMai",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__NhaCungC__AB6E61643EB18DE8",
                table: "NhaCungCap",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__NhaCungC__CA1930A5258026F9",
                table: "NhaCungCap",
                column: "SDT",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__NhanVien__A9D10534CD4DAFE8",
                table: "NhanVien",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__NhanVien__CA1930A5F4727F15",
                table: "NhanVien",
                column: "SDT",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_TaiKhoanId",
                table: "NhanVien",
                column: "TaiKhoanId");

            migrationBuilder.CreateIndex(
                name: "UQ__PhanLoai__4E48BB75914F482E",
                table: "PhanLoai",
                column: "Loai",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhanLoai_NhomLoai",
                table: "PhanLoai",
                column: "NhomLoai");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_NhaCungCapId",
                table: "PhieuNhap",
                column: "NhaCungCapId");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_SanPham_SanPhamId",
                table: "PhieuNhap_SanPham",
                column: "SanPhamId");

            migrationBuilder.CreateIndex(
                name: "UQ__PhiShip__D06970C39D3C794E",
                table: "PhiShip",
                column: "Quan",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reply_BinhLuanId",
                table: "Reply",
                column: "BinhLuanId");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_khachHangId",
                table: "Reply",
                column: "khachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_PhanLoaiId",
                table: "SanPham",
                column: "PhanLoaiId");

            migrationBuilder.CreateIndex(
                name: "UQ__SanPham__FCA804697CFD3704",
                table: "SanPham",
                column: "TenSanPham",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__TaiKhoan__F3DBC572A462F274",
                table: "TaiKhoan",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_VaiTroId",
                table: "TaiKhoan",
                column: "VaiTroId");

            migrationBuilder.CreateIndex(
                name: "UQ__VaiTro__24B0CA9E54BECCB0",
                table: "VaiTro",
                column: "MoTa",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "ChiTietHoaDon");

            migrationBuilder.DropTable(
                name: "ChiTietKhuyenMai");

            migrationBuilder.DropTable(
                name: "PhieuNhap_SanPham");

            migrationBuilder.DropTable(
                name: "Reply");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "KhuyenMai");

            migrationBuilder.DropTable(
                name: "PhieuNhap");

            migrationBuilder.DropTable(
                name: "BinhLuan");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "PhiShip");

            migrationBuilder.DropTable(
                name: "NhaCungCap");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "PhanLoai");

            migrationBuilder.DropTable(
                name: "VaiTro");
        }
    }
}
