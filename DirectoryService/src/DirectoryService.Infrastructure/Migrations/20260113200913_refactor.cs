using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DirectoryService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Depth",
                table: "departments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId1",
                table: "DepartmentLocations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_departments_ParentId",
                table: "departments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentLocations_LocationId1",
                table: "DepartmentLocations",
                column: "LocationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentLocations_locations_LocationId1",
                table: "DepartmentLocations",
                column: "LocationId1",
                principalTable: "locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_departments_departments_ParentId",
                table: "departments",
                column: "ParentId",
                principalTable: "departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentLocations_locations_LocationId1",
                table: "DepartmentLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_departments_departments_ParentId",
                table: "departments");

            migrationBuilder.DropIndex(
                name: "IX_departments_ParentId",
                table: "departments");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentLocations_LocationId1",
                table: "DepartmentLocations");

            migrationBuilder.DropColumn(
                name: "LocationId1",
                table: "DepartmentLocations");

            migrationBuilder.AlterColumn<short>(
                name: "Depth",
                table: "departments",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
