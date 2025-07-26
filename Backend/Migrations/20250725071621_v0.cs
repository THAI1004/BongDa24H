using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class v0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "User"),
                    AccumulatedPoints = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__3214EC07D101141F", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PitchClusters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClusterName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PitchClu__3214EC07413191A0", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PitchClusters_Users",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReporterId = table.Column<int>(type: "int", nullable: false),
                    TargetId = table.Column<int>(type: "int", nullable: false),
                    TargetType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ReportDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Pending")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reports__3214EC07E2BB0956", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_ReporterUser",
                        column: x => x.ReporterId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    TotalMatches = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    Wins = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    SkillLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Teams__3214EC07FF40421D", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Users",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pitches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PitchName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClusterId = table.Column<int>(type: "int", nullable: false),
                    PitchType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Facilities = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pitches__3214EC070324212E", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pitches_PitchClusters",
                        column: x => x.ClusterId,
                        principalTable: "PitchClusters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiscountPercent = table.Column<int>(type: "int", nullable: false),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ClusterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Promotio__3214EC07B17FAE2F", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promotions_PitchClusters",
                        column: x => x.ClusterId,
                        principalTable: "PitchClusters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PitchId = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeSlot = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Pending"),
                    DepositAmount = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    IsDeposited = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bookings__3214EC075AA1CD4F", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Pitches",
                        column: x => x.PitchId,
                        principalTable: "Pitches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MatchRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    PitchId = table.Column<int>(type: "int", nullable: true),
                    MatchDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeSlot = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SkillLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MatchReq__3214EC076C61D1BC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchRequests_Pitches",
                        column: x => x.PitchId,
                        principalTable: "Pitches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MatchRequests_Users",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PricingRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PitchId = table.Column<int>(type: "int", nullable: false),
                    TimeSlot = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PricingR__3214EC07673927EB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PricingRules_Pitches",
                        column: x => x.PitchId,
                        principalTable: "Pitches",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SentTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Messages__3214EC0711BA0588", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Bookings",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_ReceiverUser",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_SenderUser",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PaymentTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payments__3214EC07BF2F04E3", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    PitchRating = table.Column<int>(type: "int", nullable: true),
                    OpponentRating = table.Column<int>(type: "int", nullable: true),
                    OpponentSkill = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OpponentAttitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reviews__3214EC0727D03501", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Bookings",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MatchResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    ResponderId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Pending")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MatchRes__3214EC070DACD176", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchResponses_MatchRequests",
                        column: x => x.RequestId,
                        principalTable: "MatchRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MatchResponses_Users",
                        column: x => x.ResponderId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PitchId",
                table: "Bookings",
                column: "PitchId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchRequests_CreatorId",
                table: "MatchRequests",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchRequests_PitchId",
                table: "MatchRequests",
                column: "PitchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchResponses_RequestId",
                table: "MatchResponses",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchResponses_ResponderId",
                table: "MatchResponses",
                column: "ResponderId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_BookingId",
                table: "Messages",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_PitchClusters_OwnerId",
                table: "PitchClusters",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Pitches_ClusterId",
                table: "Pitches",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_PricingRules_PitchId",
                table: "PricingRules",
                column: "PitchId");

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_ClusterId",
                table: "Promotions",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "UQ__Promotio__A1120AF5A31A65A7",
                table: "Promotions",
                column: "DiscountCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReporterId",
                table: "Reports",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "UQ__Reviews__73951AECCD141392",
                table: "Reviews",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ManagerId",
                table: "Teams",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D1053416EF43B0",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchResponses");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PricingRules");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "MatchRequests");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Pitches");

            migrationBuilder.DropTable(
                name: "PitchClusters");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
