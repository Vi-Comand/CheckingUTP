using Microsoft.EntityFrameworkCore.Migrations;

namespace CheckingUTP.Migrations
{
    public partial class ef15072021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Voulme_hours_per_listener",
                table: "Type_trainig_load",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Voulme_hours_per_listener",
                table: "Type_trainig_load");
        }
    }
}
