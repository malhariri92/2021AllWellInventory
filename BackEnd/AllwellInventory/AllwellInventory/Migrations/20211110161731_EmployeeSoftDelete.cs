using Microsoft.EntityFrameworkCore.Migrations;

namespace AllwellInventory.Migrations
{
    public partial class EmployeeSoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "inactive",
                table: "employee",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "inactive",
                table: "employee");
        }
    }
}
