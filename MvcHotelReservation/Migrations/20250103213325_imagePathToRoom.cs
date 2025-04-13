using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcHotelReservation.Migrations
{
    /// <inheritdoc />
    public partial class imagePathToRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "chambres",
                nullable: true,  
                defaultValue: "" 
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "chambres"
            );
        }
    }
}
