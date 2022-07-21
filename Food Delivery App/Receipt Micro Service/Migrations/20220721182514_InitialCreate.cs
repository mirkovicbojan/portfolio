using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Receipt_Micro_Service.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "receipts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    userEmail = table.Column<string>(type: "text", nullable: false),
                    priceSum = table.Column<double>(type: "double precision", nullable: false),
                    deliveryFee = table.Column<double>(type: "double precision", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    createdOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    foods = table.Column<List<string>>(type: "text[]", nullable: false),
                    isCompleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receipts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "receipts");
        }
    }
}
