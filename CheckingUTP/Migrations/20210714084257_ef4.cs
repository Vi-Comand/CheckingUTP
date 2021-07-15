using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CheckingUTP.Migrations
{
    public partial class ef4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UTP_type_training_load");

            migrationBuilder.AddColumn<int>(
                name: "Utp_id",
                table: "Type_trainig_load",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Utp_id",
                table: "Type_trainig_load");

            migrationBuilder.CreateTable(
                name: "UTP_type_training_load",
                columns: table => new
                {
                    UTP_type_training_load_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type_training_load_id = table.Column<int>(type: "int", nullable: false),
                    UTP_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UTP_type_training_load", x => x.UTP_type_training_load_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
