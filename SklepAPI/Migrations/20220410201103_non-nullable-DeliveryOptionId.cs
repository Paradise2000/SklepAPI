using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SklepAPI.Migrations
{
    public partial class nonnullableDeliveryOptionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryOptions_DeliveryOptionId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryOptionId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryOptions_DeliveryOptionId",
                table: "Orders",
                column: "DeliveryOptionId",
                principalTable: "DeliveryOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryOptions_DeliveryOptionId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryOptionId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryOptions_DeliveryOptionId",
                table: "Orders",
                column: "DeliveryOptionId",
                principalTable: "DeliveryOptions",
                principalColumn: "Id");
        }
    }
}
