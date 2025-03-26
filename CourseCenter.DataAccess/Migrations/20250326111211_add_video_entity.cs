using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseCenter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_video_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseVideos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoNumber = table.Column<int>(type: "int", nullable: false),
                    VideoTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseVideos_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2b602e0d-ccfb-455b-97ec-3b50d0304249", "AQAAAAIAAYagAAAAEHiCVBE0SDtj3mWdct1r0BGWMCd6cCWfB+OcK+bVt91ZksYz9RTqwjzfFzITsputoA==" });

            migrationBuilder.CreateIndex(
                name: "IX_CourseVideos_CourseId",
                table: "CourseVideos",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseVideos");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e8ea7f1d-b834-4e0e-bf6a-cea9df1503e5", "AQAAAAIAAYagAAAAEMUIEVhFugKtwiIPjVO9eKIc+ykdd6KpaDKlmknjY9ly3jjA16hPzJ0IUwLor2GHjQ==" });
        }
    }
}
