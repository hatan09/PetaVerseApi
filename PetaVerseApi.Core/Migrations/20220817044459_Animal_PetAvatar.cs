using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetaVerseApi.Core.Migrations
{
    public partial class Animal_PetAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaGuid",
                table: "PetaverseMedias");

            migrationBuilder.DropColumn(
                name: "PetAvatar",
                table: "Animals");

            migrationBuilder.AddColumn<int>(
                name: "PetAvatarId",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_PetAvatarId",
                table: "Animals",
                column: "PetAvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_PetaverseMedias_PetAvatarId",
                table: "Animals",
                column: "PetAvatarId",
                principalTable: "PetaverseMedias",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_PetaverseMedias_PetAvatarId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_PetAvatarId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "PetAvatarId",
                table: "Animals");

            migrationBuilder.AddColumn<string>(
                name: "MediaGuid",
                table: "PetaverseMedias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PetAvatar",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
