using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CRUDDOTNET.Migrations
{
    /// <inheritdoc />
    public partial class IdUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "dtnasc",
                table: "users",
                newName: "dt_nasc");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "dt_nasc",
                table: "users",
                newName: "dtnasc");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "cpf");
        }
    }
}
