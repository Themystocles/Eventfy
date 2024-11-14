using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventfy.Migrations
{
    public partial class eventmigrationsnotrequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locals_LocalId",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "LocalId",
                table: "Events",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locals_LocalId",
                table: "Events",
                column: "LocalId",
                principalTable: "Locals",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locals_LocalId",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "LocalId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locals_LocalId",
                table: "Events",
                column: "LocalId",
                principalTable: "Locals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
