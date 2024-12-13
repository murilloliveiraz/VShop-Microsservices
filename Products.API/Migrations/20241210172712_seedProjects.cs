using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products.API.Migrations
{
    /// <inheritdoc />
    public partial class seedProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO products (name, price, description, stock, imageurl, categoryid)"
                + "VALUES('Caderno', 7.55, 'Caderno', 10, 'caderno.jpg', 1)");
            migrationBuilder.Sql("INSERT INTO products (name, price, description, stock, imageurl, categoryid)"
                + "VALUES('Lápis', 3.45, 'Lápis', 20, 'lapis.jpg', 1)");
            migrationBuilder.Sql("INSERT INTO products (name, price, description, stock, imageurl, categoryid)"
                + "VALUES('Clips', 5.33, 'Caderno', 50, 'clips.jpg', 2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM products");
        }
    }
}
