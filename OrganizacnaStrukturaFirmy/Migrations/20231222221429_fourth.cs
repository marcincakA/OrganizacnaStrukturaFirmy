using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizacnaStrukturaFirmy.Migrations
{
    /// <inheritdoc />
    public partial class fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //alter node table - add foreign key from nodeTable(column Id) to Id_parentNode
            migrationBuilder.CreateIndex(name: "Index_Node_ParentNode", table: "Nodes", column: "Id_parentNode");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentNode", table: "Nodes", column: "Id_parentNode", principalTable: "Nodes", principalColumn: "Id", onDelete: ReferentialAction.Restrict
            );


            migrationBuilder.CreateIndex(
                name: "index_Employee_WorkplaceID", table: "Employees", column: "Id_workplace"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_WorkId", table: "Employees", column: "Id_workplace", principalTable: "Nodes", principalColumn: "Id", onDelete: ReferentialAction.Restrict
            );

            //alter employees table - ad foreig key to Id_workplace from note(column Id)
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentNode",
                table: "Nodes");

            migrationBuilder.DropIndex(
                name: "Index_Node_ParentNode",
                table: "Nodes");

            // Drop foreign key and index for Employees
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_WorkId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "index_Employee_WorkplaceID",
                table: "Employees");
        }
    }
}
