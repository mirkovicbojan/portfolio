using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeSheet.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    categoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    categoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.categoryID);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    clientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    zip = table.Column<int>(type: "int", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.clientID);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    memberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    memberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hoursPerWeek = table.Column<float>(type: "real", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.memberID);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    projectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    projectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    projectDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    currentclientID = table.Column<int>(type: "int", nullable: true),
                    memberID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.projectID);
                    table.ForeignKey(
                        name: "FK_Project_Client_currentclientID",
                        column: x => x.currentclientID,
                        principalTable: "Client",
                        principalColumn: "clientID");
                    table.ForeignKey(
                        name: "FK_Project_Member_memberID",
                        column: x => x.memberID,
                        principalTable: "Member",
                        principalColumn: "memberID");
                });

            migrationBuilder.CreateTable(
                name: "TimeSheets",
                columns: table => new
                {
                    sheetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    time = table.Column<double>(type: "float", nullable: false),
                    overtime = table.Column<double>(type: "float", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    clientID = table.Column<int>(type: "int", nullable: true),
                    projectID = table.Column<int>(type: "int", nullable: true),
                    categoryID = table.Column<int>(type: "int", nullable: true),
                    memberID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheets", x => x.sheetID);
                    table.ForeignKey(
                        name: "FK_TimeSheets_Categories_categoryID",
                        column: x => x.categoryID,
                        principalTable: "Categories",
                        principalColumn: "categoryID");
                    table.ForeignKey(
                        name: "FK_TimeSheets_Client_clientID",
                        column: x => x.clientID,
                        principalTable: "Client",
                        principalColumn: "clientID");
                    table.ForeignKey(
                        name: "FK_TimeSheets_Member_memberID",
                        column: x => x.memberID,
                        principalTable: "Member",
                        principalColumn: "memberID");
                    table.ForeignKey(
                        name: "FK_TimeSheets_Project_projectID",
                        column: x => x.projectID,
                        principalTable: "Project",
                        principalColumn: "projectID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_currentclientID",
                table: "Project",
                column: "currentclientID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_memberID",
                table: "Project",
                column: "memberID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_categoryID",
                table: "TimeSheets",
                column: "categoryID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_clientID",
                table: "TimeSheets",
                column: "clientID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_memberID",
                table: "TimeSheets",
                column: "memberID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheets_projectID",
                table: "TimeSheets",
                column: "projectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeSheets");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Member");
        }
    }
}
