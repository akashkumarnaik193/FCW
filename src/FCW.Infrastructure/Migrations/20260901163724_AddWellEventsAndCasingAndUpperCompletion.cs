using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCW.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWellEventsAndCasingAndUpperCompletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WellEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignConceptId = table.Column<int>(type: "int", nullable: false),
                    EventType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PlannedStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlannedEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    PlannedDepth = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    MudType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompletionType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TubingSize = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    InterventionReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ToolUsed = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PlugDepth = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    AbandonmentReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WellEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WellEvents_DesignConcepts_DesignConceptId",
                        column: x => x.DesignConceptId,
                        principalTable: "DesignConcepts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Casings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WellEventId = table.Column<int>(type: "int", nullable: false),
                    CasingType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Diameter = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Depth = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Connection = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Casings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Casings_WellEvents_WellEventId",
                        column: x => x.WellEventId,
                        principalTable: "WellEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UpperCompletions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WellEventId = table.Column<int>(type: "int", nullable: false),
                    ComponentConfiguration = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TubingType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TubingLength = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    PackerType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpperCompletions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpperCompletions_WellEvents_WellEventId",
                        column: x => x.WellEventId,
                        principalTable: "WellEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Casings_WellEventId",
                table: "Casings",
                column: "WellEventId");

            migrationBuilder.CreateIndex(
                name: "IX_UpperCompletions_WellEventId",
                table: "UpperCompletions",
                column: "WellEventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WellEvents_DesignConceptId",
                table: "WellEvents",
                column: "DesignConceptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Casings");

            migrationBuilder.DropTable(
                name: "UpperCompletions");

            migrationBuilder.DropTable(
                name: "WellEvents");
        }
    }
}
