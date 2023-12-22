using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizacnaStrukturaFirmy.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Nodes",
                newName: "NodeName");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Nodes",
                newName: "NodeCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NodeName",
                table: "Nodes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NodeCode",
                table: "Nodes",
                newName: "Code");
        }
    }
}
