using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(16,2)", precision: 16, scale: 2, nullable: false),
                    Neighborhood = table.Column<string>(type: "VARCHAR(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BedroomQuantity = table.Column<int>(type: "int", nullable: false),
                    BusinessType = table.Column<string>(type: "VARCHAR(7)", maxLength: 7, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "VARCHAR(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "VARCHAR(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telephone = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "VARCHAR(12)", maxLength: 12, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ImageFileName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RealStateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Properties_RealStateId",
                        column: x => x.RealStateId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Address", "BedroomQuantity", "BusinessType", "Neighborhood", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Rua dos Tambaquis, 100, Jurerê Internacional, Florianópolis, Santa Catarina, Brasil", 6, "Aluguel", "Jurerê Internacional", 30000m, "Mansão à beira mar de Jurerê Internacional" },
                    { 2, "Avenida Governador Irineu Bornhausen, 3600, Agronômica, Florianópolis, Santa Catarina, Brasil", 3, "Venda", "Agronômica", 150000m, "Apartamento na Beira Mar de Florianópolis" },
                    { 3, "Rua dos Tambaquis, 100, Jurerê Internacional, Florianópolis, Santa Catarina, Brasil", 5, "Venda", "Jurerê", 300000m, "Casa em Jurerê" },
                    { 4, "Avenida dos Buzios, 55, Jurerê Internacional, Florianópolis, Santa Catarina, Brasil", 5, "Venda", "Jurerê", 3000000m, "Mansão" },
                    { 5, "Rua das Algas, 488, Jurerê Internacional, Florianópolis, Santa Catarina, Brasil", 3, "Aluguel", "Jurerê", 300000m, "Casa" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Telephone" },
                values: new object[] { 1, "alexandrenolla@gmail.com", "Alexandre Nolla", "fullstack123", "48988050165" });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "ImageFileName", "RealStateId" },
                values: new object[,]
                {
                    { 1, "casa.jpeg", 1 },
                    { 2, "casa2.jpeg", 1 },
                    { 3, "casa3.jpeg", 1 },
                    { 4, "casa4.jpeg", 2 },
                    { 5, "casa5.jpeg", 2 },
                    { 6, "casa6.jpeg", 2 },
                    { 7, "casa7.jpeg", 3 },
                    { 8, "casa8.jpeg", 3 },
                    { 9, "casa9.jpeg", 4 },
                    { 10, "casa10.jpeg", 4 },
                    { 11, "casa11.jpeg", 5 },
                    { 12, "casa12.jpeg", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_RealStateId",
                table: "Photos",
                column: "RealStateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Properties");
        }
    }
}
