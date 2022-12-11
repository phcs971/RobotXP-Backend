using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RobotXP.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Measures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsMoving = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Joints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Current = table.Column<long>(type: "bigint", nullable: false),
                    Voltage = table.Column<long>(type: "bigint", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    MeasureModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Joints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Joints_Measures_MeasureModelId",
                        column: x => x.MeasureModelId,
                        principalTable: "Measures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Joints_MeasureModelId",
                table: "Joints",
                column: "MeasureModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Joints");

            migrationBuilder.DropTable(
                name: "Measures");
        }
    }
}
