using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetaVerseApi.Core.Migrations
{
    public partial class PetaverseMedia_Name_Guid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaGuid",
                table: "PetaverseMedias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MediaName",
                table: "PetaverseMedias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaGuid",
                table: "PetaverseMedias");

            migrationBuilder.DropColumn(
                name: "MediaName",
                table: "PetaverseMedias");
        }
    }
}
