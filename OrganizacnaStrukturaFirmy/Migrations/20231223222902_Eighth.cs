using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizacnaStrukturaFirmy.Migrations
{
    /// <inheritdoc />
    public partial class Eighth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Nodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            /*migrationBuilder.Sql(
                "CREATE FUNCTION GetLevel (@Id INT)" +
                "RETURNS INT" +
                "BEGIN" +
                "DECLARE @level INT = 1" +
                "WHILE @Id IS NOT NULL" + 
            "BEGIN" +
                "    SET @level = @level + 1;" + 

            "    SELECT @Id = ParentId" +
                "    FROM YourTableName" +
            "    WHERE Id = @Id;" +

            "END;" +

                "RETURN @level;" + 
            "END");*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Nodes");

            /*migrationBuilder.Sql(
                "IF OBJECT_ID('dbo.GetLevel', 'FN') IS NOT NULL" +
                "   DROP FUNCTION dbo.GetLevel"
            );*/
        }

    }
}
