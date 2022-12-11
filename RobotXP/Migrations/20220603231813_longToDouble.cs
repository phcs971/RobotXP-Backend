using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RobotXP.Migrations
{
    public partial class longToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Voltage",
                table: "Joints",
                type: "float",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<double>(
                name: "Current",
                table: "Joints",
                type: "float",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Voltage",
                table: "Joints",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "Current",
                table: "Joints",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
