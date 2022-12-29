using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.Migrations
{
    /// <inheritdoc />
    public partial class FromShopToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WorkingLocationId",
                table: "Employee",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Employee_WorkingLocationId",
                table: "Employee",
                column: "WorkingLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Shop_WorkingLocationId",
                table: "Employee",
                column: "WorkingLocationId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Shop_WorkingLocationId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_WorkingLocationId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "WorkingLocationId",
                table: "Employee");
        }
    }
}
