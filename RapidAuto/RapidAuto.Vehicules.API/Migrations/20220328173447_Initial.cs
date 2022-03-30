using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapidAuto.Vehicules.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Constructeur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modele = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Annee = table.Column<int>(type: "int", nullable: false),
                    TypeVehicule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreDeSieges = table.Column<int>(type: "int", nullable: false),
                    Couleur = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NIV = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    NomImage1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomImage2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    EstDisponible = table.Column<bool>(type: "bit", nullable: false),
                    Prix = table.Column<double>(type: "float", nullable: false),
                    CodeUnique = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicule", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicule");
        }
    }
}
