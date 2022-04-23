using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SklepAPI.Migrations
{
    public partial class AddDeliveryOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryOptionId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeliveryOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerDelivery = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryOptions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryOptionId",
                table: "Orders",
                column: "DeliveryOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryOptions_DeliveryOptionId",
                table: "Orders",
                column: "DeliveryOptionId",
                principalTable: "DeliveryOptions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryOptions_DeliveryOptionId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "DeliveryOptions");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryOptionId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryOptionId",
                table: "Orders");
        }
    }
}
