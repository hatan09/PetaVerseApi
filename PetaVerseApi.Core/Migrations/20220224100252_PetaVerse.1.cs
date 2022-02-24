using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetaVerseApi.Core.Migrations
{
    public partial class PetaVerse1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnimal_Animal_AnimalId",
                table: "UserAnimal");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnimal_AspNetUsers_UserId",
                table: "UserAnimal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnimal",
                table: "UserAnimal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Animal",
                table: "Animal");

            migrationBuilder.RenameTable(
                name: "UserAnimal",
                newName: "UserAnimals");

            migrationBuilder.RenameTable(
                name: "Animal",
                newName: "Animals");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnimal_UserId",
                table: "UserAnimals",
                newName: "IX_UserAnimals_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnimal_AnimalId",
                table: "UserAnimals",
                newName: "IX_UserAnimals_AnimalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnimals",
                table: "UserAnimals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Animals",
                table: "Animals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnimals_Animals_AnimalId",
                table: "UserAnimals",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnimals_AspNetUsers_UserId",
                table: "UserAnimals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnimals_Animals_AnimalId",
                table: "UserAnimals");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnimals_AspNetUsers_UserId",
                table: "UserAnimals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnimals",
                table: "UserAnimals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Animals",
                table: "Animals");

            migrationBuilder.RenameTable(
                name: "UserAnimals",
                newName: "UserAnimal");

            migrationBuilder.RenameTable(
                name: "Animals",
                newName: "Animal");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnimals_UserId",
                table: "UserAnimal",
                newName: "IX_UserAnimal_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnimals_AnimalId",
                table: "UserAnimal",
                newName: "IX_UserAnimal_AnimalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnimal",
                table: "UserAnimal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Animal",
                table: "Animal",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnimal_Animal_AnimalId",
                table: "UserAnimal",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnimal_AspNetUsers_UserId",
                table: "UserAnimal",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
