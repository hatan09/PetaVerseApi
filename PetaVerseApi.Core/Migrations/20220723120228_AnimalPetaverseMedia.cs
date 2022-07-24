using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetaVerseApi.Core.Migrations
{
    public partial class AnimalPetaverseMedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalPetaverseMedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    PetaverMediaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalPetaverseMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalPetaverseMedia_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalPetaverseMedia_PetaverseMedia_PetaverMediaId",
                        column: x => x.PetaverMediaId,
                        principalTable: "PetaverseMedia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalPetaverseMedia_AnimalId",
                table: "AnimalPetaverseMedia",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalPetaverseMedia_PetaverMediaId",
                table: "AnimalPetaverseMedia",
                column: "PetaverMediaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalPetaverseMedia");
        }
    }
}
