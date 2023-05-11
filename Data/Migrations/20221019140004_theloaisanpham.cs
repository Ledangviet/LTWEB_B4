using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LTWEB_B4.Data.Migrations
{
    public partial class theloaisanpham : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TheLoaiSanPham",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SanPhamId = table.Column<int>(type: "int", nullable: false),
                    TheLoaiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoaiSanPham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TheLoaiSanPham_SanPham_SanPhamId",
                        column: x => x.SanPhamId,
                        principalTable: "SanPham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TheLoaiSanPham_TheLoaiModel_TheLoaiId",
                        column: x => x.TheLoaiId,
                        principalTable: "TheLoaiModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TheLoaiSanPham_SanPhamId",
                table: "TheLoaiSanPham",
                column: "SanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_TheLoaiSanPham_TheLoaiId",
                table: "TheLoaiSanPham",
                column: "TheLoaiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TheLoaiSanPham");
        }
    }
}
