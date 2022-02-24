using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetaVerseApi.Core.Migrations
{
    public partial class MyFirstVer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnimal_AspNetUsers_UserId1",
                table: "UserAnimal");

            migrationBuilder.DropIndex(
                name: "IX_UserAnimal_UserId1",
                table: "UserAnimal");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserAnimal");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserAnimal",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimal_UserId",
                table: "UserAnimal",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnimal_AspNetUsers_UserId",
                table: "UserAnimal",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnimal_AspNetUsers_UserId",
                table: "UserAnimal");

            migrationBuilder.DropIndex(
                name: "IX_UserAnimal_UserId",
                table: "UserAnimal");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserAnimal",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserAnimal",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimal_UserId1",
                table: "UserAnimal",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnimal_AspNetUsers_UserId1",
                table: "UserAnimal",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
