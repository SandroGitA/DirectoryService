using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DirectoryService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOfConnections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentLocation");

            migrationBuilder.DropTable(
                name: "DepartmentPosition");

            migrationBuilder.CreateTable(
                name: "DepartmentLocations",
                columns: table => new
                {
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentLocations", x => new { x.DepartmentId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_DepartmentLocations_departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentLocations_locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentPositions",
                columns: table => new
                {
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentPositions", x => new { x.DepartmentId, x.PositionId });
                    table.ForeignKey(
                        name: "FK_DepartmentPositions_departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentPositions_position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentLocations_LocationId",
                table: "DepartmentLocations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPositions_PositionId",
                table: "DepartmentPositions",
                column: "PositionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentLocations");

            migrationBuilder.DropTable(
                name: "DepartmentPositions");

            migrationBuilder.CreateTable(
                name: "DepartmentLocation",
                columns: table => new
                {
                    DepartmentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentLocation", x => new { x.DepartmentsId, x.LocationsId });
                    table.ForeignKey(
                        name: "FK_DepartmentLocation_departments_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentLocation_locations_LocationsId",
                        column: x => x.LocationsId,
                        principalTable: "locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentPosition",
                columns: table => new
                {
                    DepartmentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentPosition", x => new { x.DepartmentsId, x.PositionsId });
                    table.ForeignKey(
                        name: "FK_DepartmentPosition_departments_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentPosition_position_PositionsId",
                        column: x => x.PositionsId,
                        principalTable: "position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentLocation_LocationsId",
                table: "DepartmentLocation",
                column: "LocationsId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPosition_PositionsId",
                table: "DepartmentPosition",
                column: "PositionsId");
        }
    }
}
