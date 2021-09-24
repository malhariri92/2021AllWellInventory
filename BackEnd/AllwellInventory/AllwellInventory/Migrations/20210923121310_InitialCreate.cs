using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AllwellInventory.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "location",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nchar(30)", fixedLength: true, maxLength: 30, nullable: false),
                    city = table.Column<string>(type: "nchar(30)", fixedLength: true, maxLength: 30, nullable: true),
                    county = table.Column<string>(type: "nchar(30)", fixedLength: true, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    fName = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
                    lName = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
                    username = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
                    password = table.Column<string>(type: "nchar(14)", fixedLength: true, maxLength: 14, nullable: true),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.id);
                    table.ForeignKey(
                        name: "FK_employee_location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "location",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    typeId = table.Column<int>(type: "int", nullable: false),
                    cost = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    workerId = table.Column<int>(type: "int", nullable: true),
                    locationId = table.Column<int>(type: "int", nullable: false),
                    condition = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    damaged = table.Column<bool>(type: "bit", nullable: false),
                    serialNo = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_Products_Employee",
                        column: x => x.workerId,
                        principalTable: "employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Location",
                        column: x => x.locationId,
                        principalTable: "location",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Type",
                        column: x => x.typeId,
                        principalTable: "type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "assign_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    employeeId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    assignedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    returnedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assign_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_assign_log_Employee",
                        column: x => x.employeeId,
                        principalTable: "employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_assign_log_Products",
                        column: x => x.productId,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_assign_log_employeeId",
                table: "assign_log",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_assign_log_productId",
                table: "assign_log",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_LocationId",
                table: "employee",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_products_locationId",
                table: "products",
                column: "locationId");

            migrationBuilder.CreateIndex(
                name: "IX_products_typeId",
                table: "products",
                column: "typeId");

            migrationBuilder.CreateIndex(
                name: "IX_products_workerId",
                table: "products",
                column: "workerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assign_log");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "type");

            migrationBuilder.DropTable(
                name: "location");
        }
    }
}
