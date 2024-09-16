using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Labb__Minimal_API___Anrop_till_ASP.Net.Migrations
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PublicationYear = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "ID", "Author", "Description", "Genre", "IsAvailable", "PublicationYear", "Title" },
                values: new object[,]
                {
                    { new Guid("2ab5a7ae-3d9b-4d20-810e-f6251df1d5e5"), "J.K. Rowling", "Book about Harry Potter and his wizarding friends.", "Fantasy", true, 1999, "Harry Potter and the Prisoner of Azkaban" },
                    { new Guid("bea4ed64-1dcb-4e02-8c6f-6c4fc24ad59b"), "Jordan Belfort", "About Jordan Belforts interesting life!", "Autobiography", true, 2008, "Wolf of the Wall Street" },
                    { new Guid("cde12fa6-53c7-405f-8a09-c30b79d1b95e"), "Suzanne Collins", "We follow Katniss Everdeen and her struggles.", "Science fiction", false, 2008, "The Hunger Games" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
