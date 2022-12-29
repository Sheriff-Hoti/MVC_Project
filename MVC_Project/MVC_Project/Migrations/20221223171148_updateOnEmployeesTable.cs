using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.Migrations
{
    /// <inheritdoc />
    public partial class updateOnEmployeesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Shop_WorkingLocationId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_WorkingLocationId",
                table: "Employee");

            migrationBuilder.AddColumn<Guid>(
                name: "ShopId",
                table: "Employee",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ShopId",
                table: "Employee",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Shop_ShopId",
                table: "Employee",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Shop_ShopId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_ShopId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Employee");

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
    }
}
