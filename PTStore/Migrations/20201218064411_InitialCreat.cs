using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PTStore.Migrations
{
    public partial class InitialCreat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GopY",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    SoDienThoai = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ChuDe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GopY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Subcriber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    TrangThai = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcriber", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThuongHieu",
                columns: table => new
                {
                    ThuongHieuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenThuongHieu = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    HinhAnhThuongHieu = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuongHieu", x => x.ThuongHieuId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoDienThoai = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Email = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    HoVaTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    MatKhau = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    TrangThai = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK__Account__UserId__398D8EEE",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    DonHangId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDonHang = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    TenNguoiNhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    SoDienThoai = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    NgayDatHang = table.Column<DateTime>(type: "date", nullable: true),
                    NgayDuKienGiao = table.Column<DateTime>(type: "date", nullable: true),
                    HinhThucThanhToan = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    TrangTrai = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.DonHangId);
                    table.ForeignKey(
                        name: "FK__DonHang__UserId__3E52440B",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserRole__AF2760AD7E056BDC", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK__UserRole__RoleId__412EB0B6",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__UserRole__UserId__403A8C7D",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    DonHangId = table.Column<int>(type: "int", nullable: false),
                    DienThoaiId = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    Gia = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietD__5230C870CA3FEFF2", x => new { x.DonHangId, x.DienThoaiId });
                    table.ForeignKey(
                        name: "FK__ChiTietDo__DonHa__3A81B327",
                        column: x => x.DonHangId,
                        principalTable: "DonHang",
                        principalColumn: "DonHangId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ThongSoKyThuat",
                columns: table => new
                {
                    ThongSoKyThuatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManHinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeDieuHanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CameraSau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CameraTruoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RAM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoNhoTrong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TheSim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DungLuongPin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayRaMat = table.Column<DateTime>(type: "date", nullable: true),
                    DienThoaiId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongSoKyThuat", x => x.ThongSoKyThuatId);
                });

            migrationBuilder.CreateTable(
                name: "DienThoai",
                columns: table => new
                {
                    DienThoaiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TinhTrang = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    HinhAnh = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Gia = table.Column<double>(type: "float", nullable: true),
                    GiaGoc = table.Column<double>(type: "float", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    ThuongHieuId = table.Column<int>(type: "int", nullable: true),
                    ThongSoKyThuatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DienThoai", x => x.DienThoaiId);
                    table.ForeignKey(
                        name: "FK__DienThoai__Thong__3C69FB99",
                        column: x => x.ThongSoKyThuatId,
                        principalTable: "ThongSoKyThuat",
                        principalColumn: "ThongSoKyThuatId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__DienThoai__Thuon__3C69FB99",
                        column: x => x.ThuongHieuId,
                        principalTable: "ThuongHieu",
                        principalColumn: "ThuongHieuId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserId",
                table: "Account",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_DienThoaiId",
                table: "ChiTietDonHang",
                column: "DienThoaiId");

            migrationBuilder.CreateIndex(
                name: "IX_DienThoai_ThongSoKyThuatId",
                table: "DienThoai",
                column: "ThongSoKyThuatId");

            migrationBuilder.CreateIndex(
                name: "IX_DienThoai_ThuongHieuId",
                table: "DienThoai",
                column: "ThuongHieuId");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_UserId",
                table: "DonHang",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ThongSoKyThuat_DienThoaiId",
                table: "ThongSoKyThuat",
                column: "DienThoaiId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccountId",
                table: "Users",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK__Users__AccountId__4222D4EF",
                table: "Users",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__ChiTietDo__DienT__3B75D760",
                table: "ChiTietDonHang",
                column: "DienThoaiId",
                principalTable: "DienThoai",
                principalColumn: "DienThoaiId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__ThongSoKy__DienT__3F466844",
                table: "ThongSoKyThuat",
                column: "DienThoaiId",
                principalTable: "DienThoai",
                principalColumn: "DienThoaiId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Account__UserId__398D8EEE",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK__ThongSoKy__DienT__3F466844",
                table: "ThongSoKyThuat");

            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "GopY");

            migrationBuilder.DropTable(
                name: "Subcriber");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "DienThoai");

            migrationBuilder.DropTable(
                name: "ThongSoKyThuat");

            migrationBuilder.DropTable(
                name: "ThuongHieu");
        }
    }
}
