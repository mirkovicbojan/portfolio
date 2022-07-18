using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food_Delivery_App.Migrations
{
    public partial class OrderCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    orderId = table.Column<Guid>(type: "uuid", nullable: false),
                    priceSum = table.Column<double>(type: "double precision", nullable: false),
                    deliveryFee = table.Column<double>(type: "double precision", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    createdOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    foods = table.Column<List<string>>(type: "text[]", nullable: false),
                    isCompleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.orderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");
        }
    }
}
