using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyBanHang.Migrations
{
    public partial class AddMoTaSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MoTa",
                table: "SanPham",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoTa",
                table: "SanPham");
        }
    }
}
