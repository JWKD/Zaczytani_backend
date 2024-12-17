﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zaczytani.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EnableBlockUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE AspNetUsers SET LockoutEnabled = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE AspNetUsers SET LockoutEnabled = 0");
        }
    }
}
