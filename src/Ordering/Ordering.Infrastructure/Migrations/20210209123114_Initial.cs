
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ordering.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(nullable: true),
                    totalPrice = table.Column<decimal>(nullable: false),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    emailAddress = table.Column<string>(nullable: true),
                    addressLine = table.Column<string>(nullable: true),
                    country = table.Column<string>(nullable: true),
                    state = table.Column<string>(nullable: true),
                    zipCode = table.Column<string>(nullable: true),
                    cardName = table.Column<string>(nullable: true),
                    cardNumber = table.Column<string>(nullable: true),
                    expiration = table.Column<string>(nullable: true),
                    CVV = table.Column<string>(nullable: true),
                    paymentMethod = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
