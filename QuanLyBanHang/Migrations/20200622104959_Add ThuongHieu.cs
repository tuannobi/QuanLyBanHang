using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyBanHang.Migrations
{
    public partial class AddThuongHieu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThuongHieu",
                table: "SanPham",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThuongHieu",
                table: "SanPham");
        }
    }
}
