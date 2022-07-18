using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetaVerseApi.Core.Migrations
{
    public partial class Species_Icon_Color : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Species",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Species",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Species");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Species");
        }
    }
}
