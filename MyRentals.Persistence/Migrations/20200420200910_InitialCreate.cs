namespace MyRentals.Persistence.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<ulong>(nullable: false),
                    ClientName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Realtors",
                columns: table => new
                {
                    RealtorId = table.Column<ulong>(nullable: false),
                    RealtorName = table.Column<string>(nullable: false),
                    RealtorEmail = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Realtors", x => x.RealtorId);
                });

            migrationBuilder.CreateTable(
                name: "Apartments",
                columns: table => new
                {
                    ApartmentId = table.Column<ulong>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    FloorArea = table.Column<int>(nullable: false),
                    PricePerMonth = table.Column<decimal>(nullable: false),
                    NumberOfRooms = table.Column<int>(nullable: false),
                    Lat = table.Column<decimal>(nullable: false),
                    Lon = table.Column<decimal>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false, defaultValueSql: "(getdate())"),
                    RealtorId = table.Column<ulong>(nullable: false),
                    IsRented = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.ApartmentId);
                    table.ForeignKey(
                        name: "FK_Apartments_Realtors_RealtorId",
                        column: x => x.RealtorId,
                        principalTable: "Realtors",
                        principalColumn: "RealtorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_RealtorId",
                table: "Apartments",
                column: "RealtorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apartments");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Realtors");
        }
    }
}
