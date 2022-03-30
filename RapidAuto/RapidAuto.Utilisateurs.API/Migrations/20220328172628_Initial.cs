using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapidAuto.Utilisateurs.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utilisateur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prenom = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NumeroTelephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentifiantUtilisateur = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateur", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Utilisateur");
        }
    }
}
