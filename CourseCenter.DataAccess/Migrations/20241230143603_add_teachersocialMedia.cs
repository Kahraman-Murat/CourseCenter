using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseCenter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_teachersocialMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeacherSocialMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSocialMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherSocialMedias_AspNetUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d92ecd7f-cc81-4851-b316-ee5150b139ae", "AQAAAAIAAYagAAAAEGYdlQGxqe7zheJPU8JX4t07124tkPhstQ4prTmPaErFjFGPjszIVGNnXjaviacoxw==" });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSocialMedias_TeacherId",
                table: "TeacherSocialMedias",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherSocialMedias");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5d797c35-2322-443a-84a3-5096111a3425", "AQAAAAIAAYagAAAAEO8Vg0u5Ik6uC0GfUfF3EsVWWB5QZtA9K4Ce+zbhbUZg/yxkE2Oc//70Dv9svwK+0Q==" });
        }
    }
}
