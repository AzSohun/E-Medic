using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Medic.Migrations
{
    /// <inheritdoc />
    public partial class CleanIdentityArchitecture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalRatingCount",
                table: "Doctors",
                newName: "TotalRatingsCount");

            migrationBuilder.RenameColumn(
                name: "Specification",
                table: "Doctors",
                newName: "Specialty");

            migrationBuilder.RenameColumn(
                name: "Qualifiations",
                table: "Doctors",
                newName: "Qualifications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalRatingsCount",
                table: "Doctors",
                newName: "TotalRatingCount");

            migrationBuilder.RenameColumn(
                name: "Specialty",
                table: "Doctors",
                newName: "Specification");

            migrationBuilder.RenameColumn(
                name: "Qualifications",
                table: "Doctors",
                newName: "Qualifiations");
        }
    }
}
