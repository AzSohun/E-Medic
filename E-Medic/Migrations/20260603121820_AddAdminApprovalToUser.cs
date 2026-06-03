using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Medic.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminApprovalToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedByAdmin",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApprovedByAdmin",
                table: "Users");
        }
    }
}
