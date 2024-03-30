using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketingEvent.Database.Migrations
{
    /// <inheritdoc />
    public partial class AlterAddRelationshipSubmissionEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Submissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_EventId",
                table: "Submissions",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Events_EventId",
                table: "Submissions",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Events_EventId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_EventId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Submissions");
        }
    }
}
