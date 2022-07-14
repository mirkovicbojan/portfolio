using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food_Delivery_App.Migrations
{
    public partial class creditMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "credit",
                table: "users",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "credit",
                table: "users");
        }
    }
}
