using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseCenter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_field_mission_visiun : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OurMission",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OurVision",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e8ea7f1d-b834-4e0e-bf6a-cea9df1503e5", "AQAAAAIAAYagAAAAEMUIEVhFugKtwiIPjVO9eKIc+ykdd6KpaDKlmknjY9ly3jjA16hPzJ0IUwLor2GHjQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OurMission",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "OurVision",
                table: "Abouts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d92ecd7f-cc81-4851-b316-ee5150b139ae", "AQAAAAIAAYagAAAAEGYdlQGxqe7zheJPU8JX4t07124tkPhstQ4prTmPaErFjFGPjszIVGNnXjaviacoxw==" });
        }
    }
}
