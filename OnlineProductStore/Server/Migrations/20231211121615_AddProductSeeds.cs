using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineProductStore.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddProductSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Дуже свіжі", "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRemk0pOj3avWb06RvabQarkPJ-BUaZPIT9UjLWrwM6xL8TyRbj", 20m, "Помидори" },
                    { 2, "Смачно капець", "https://www.fruit-market.com.ua/wp-content/uploads/2020/04/kornishon.jpg", 60m, "Огірочічки" },
                    { 3, "Дуже свіжі", "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRemk0pOj3avWb06RvabQarkPJ-BUaZPIT9UjLWrwM6xL8TyRbj", 20m, "Помидори" },
                    { 4, "Смачно капець", "https://www.fruit-market.com.ua/wp-content/uploads/2020/04/kornishon.jpg", 60m, "Огірочічки" },
                    { 5, "Дуже свіжі", "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRemk0pOj3avWb06RvabQarkPJ-BUaZPIT9UjLWrwM6xL8TyRbj", 20m, "Помидори" },
                    { 6, "Смачно капець", "https://www.fruit-market.com.ua/wp-content/uploads/2020/04/kornishon.jpg", 60m, "Огірочічки" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
