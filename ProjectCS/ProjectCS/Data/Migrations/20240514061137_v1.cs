using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCS.Data.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    ClassId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Titlle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Topic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Room = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.ClassId);
                });

            migrationBuilder.CreateTable(
                name: "Loai",
                columns: table => new
                {
                    LoaiId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    LoaiName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loai", x => x.LoaiId);
                });

            migrationBuilder.CreateTable(
                name: "ListStudent",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ClassId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListStudent", x => new { x.UserId, x.ClassId });
                    table.ForeignKey(
                        name: "FK_ListStudent_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ListStudent_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassId");
                });

            migrationBuilder.CreateTable(
                name: "Assign",
                columns: table => new
                {
                    AssignId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    AssignName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Posttime = table.Column<DateTime>(type: "datetime", nullable: false),
                    AssignFile1 = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    AssignFile2 = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ClassId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    LoaiId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assign", x => x.AssignId);
                    table.ForeignKey(
                        name: "FK_Assign_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ClassId");
                    table.ForeignKey(
                        name: "FK_Assign_Loai_LoaiId",
                        column: x => x.LoaiId,
                        principalTable: "Loai",
                        principalColumn: "LoaiId");
                });

            migrationBuilder.CreateTable(
                name: "ListAssign",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    AssignId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    LoaiId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Point = table.Column<decimal>(type: "decimal(18,2)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListAssign", x => new { x.UserId, x.AssignId, x.LoaiId });
                    table.ForeignKey(
                        name: "FK_ListAssign_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ListAssign_Assign_AssignId",
                        column: x => x.AssignId,
                        principalTable: "Assign",
                        principalColumn: "AssignId");
                });

            migrationBuilder.CreateTable(
                name: "ListFile",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    AssignId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    LoaiId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    FileId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    SubmissTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListFile", x => new { x.UserId, x.AssignId, x.LoaiId, x.FileId });
                    table.ForeignKey(
                        name: "FK_ListFile_ListAssign_UserId_AssignId_LoaiId",
                        columns: x => new { x.UserId, x.AssignId, x.LoaiId },
                        principalTable: "ListAssign",
                        principalColumns: new[] { "UserId", "AssignId", "LoaiId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assign_ClassId",
                table: "Assign",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Assign_LoaiId",
                table: "Assign",
                column: "LoaiId");

            migrationBuilder.CreateIndex(
                name: "IX_ListAssign_AssignId",
                table: "ListAssign",
                column: "AssignId");

            migrationBuilder.CreateIndex(
                name: "IX_ListStudent_ClassId",
                table: "ListStudent",
                column: "ClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListFile");

            migrationBuilder.DropTable(
                name: "ListStudent");

            migrationBuilder.DropTable(
                name: "ListAssign");

            migrationBuilder.DropTable(
                name: "Assign");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Loai");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");
        }
    }
}
