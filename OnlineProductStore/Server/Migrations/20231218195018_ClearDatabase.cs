using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineProductStore.Server.Migrations
{
    /// <inheritdoc />
    public partial class ClearDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_AddressId",
                table: "Users",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_AddressId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Users_AddressId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Deleted", "Name", "Url", "Visible" },
                values: new object[,]
                {
                    { 1, false, "Fruits", "fruits", true },
                    { 2, false, "Vegetables", "vegetables", true }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Deleted", "Description", "ImageUrl", "Price", "Title", "Visible" },
                values: new object[,]
                {
                    { 1, 2, false, "Дуже свіжі", "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRemk0pOj3avWb06RvabQarkPJ-BUaZPIT9UjLWrwM6xL8TyRbj", 20m, "Помидори Гнилі", true },
                    { 2, 2, false, "Смачно капець", "https://www.fruit-market.com.ua/wp-content/uploads/2020/04/kornishon.jpg", 60m, "Огірочічки незнаю", true },
                    { 3, 2, false, "не Дуже свіжі", "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRemk0pOj3avWb06RvabQarkPJ-BUaZPIT9UjLWrwM6xL8TyRbj", 20m, "Помидори Смачні", true },
                    { 4, 2, false, "Смачно капець", "https://www.fruit-market.com.ua/wp-content/uploads/2020/04/kornishon.jpg", 60m, "Огірочічки Короткі", true },
                    { 5, 2, false, "Дуже свіжі", "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRemk0pOj3avWb06RvabQarkPJ-BUaZPIT9UjLWrwM6xL8TyRbj", 20m, "Помидори Дивні", true },
                    { 6, 2, false, "Смачно капець", "https://www.fruit-market.com.ua/wp-content/uploads/2020/04/kornishon.jpg", 60m, "Огірочічки Довгі", true }
                });
        }
    }
}
