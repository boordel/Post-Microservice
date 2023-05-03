using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Post.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitPostDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Headline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostMedias_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "تکنولوژی" },
                    { 2, "فین تک" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CategoryId", "Description", "Headline", "Keywords", "Title" },
                values: new object[,]
                {
                    { 1, 1, "ردمی نوت ۱۱ پرو 5G در مقایسه‌با نسل قبل بهبود معناداری تجربه نمی‌کند و در بخش‌هایی همچون عملکرد و دوربین، رقبای سرسختی دارد. با بررسی ردمی نوت ۱۱ پرو همراه باشید.", "ردمی نوت ۱۱ پرو 5G در مقایسه‌با نسل قبل بهبود معناداری تجربه نمی‌کند و در بخش‌هایی همچون عملکرد و دوربین، رقبای سرسختی دارد. با بررسی ردمی نوت ۱۱ پرو همراه باشید.", "ردمی موبایل شیائومی", "بررسی ردمی نوت ۱۱ پرو 5G شیائومی" },
                    { 2, 1, "براساس شایعه‌ای جدید، سامسونگ قصد دارد اواسط بهمن مراسم Galaxy Unpacked را برای رونمایی گوشی‌های سری گلکسی اس ۲۳ برگزار کند.", "براساس شایعه‌ای جدید، سامسونگ قصد دارد اواسط بهمن مراسم Galaxy Unpacked را برای رونمایی گوشی‌های سری گلکسی اس ۲۳ برگزار کند.", "سامسونگ گلکسی رونمایی موبایل", "احتمال رونمایی گلکسی اس ۲۳ در هفته دوم بهمن قوت گرفت" },
                    { 3, 2, "گلکسی واچ ۴ و گلکسی واچ ۵ هنگام انجام تجزیه‌و‌تحلیل امپدانس بیوالکتریک (BIA) به دقتی قابل مقایسه با معیارهای آزمایشگاهی اما کمتر از آن دست می‌یابد.", "گلکسی واچ ۴ و گلکسی واچ ۵ هنگام انجام تجزیه‌و‌تحلیل امپدانس بیوالکتریک (BIA) به دقتی قابل مقایسه با معیارهای آزمایشگاهی اما کمتر از آن دست می‌یابد.", "پوشیدنی گلکسی واچ", "آنالیز ترکیب بدن گلکسی واچ 4 و گلکسی واچ 5 نسبتاً دقیق است" },
                    { 4, 2, "براساس گزارشی جدید، هدفون Buds 4 شیائومی در رویداد دهم آذر با قیمتی مقرون‌به‌صرفه‌تر از نسخه‌ی Buds 4 پرو فعلی، رونمایی خواهد شد.", "براساس گزارشی جدید، هدفون Buds 4 شیائومی در رویداد دهم آذر با قیمتی مقرون‌به‌صرفه‌تر از نسخه‌ی Buds 4 پرو فعلی، رونمایی خواهد شد.", "پوشیدنی شیائومی هدفون", "قیمت هدفون Buds 4 و ساعت هوشمند Watch S2 شیائومی پیش از رونمایی اعلام شد" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostMedias_PostId",
                table: "PostMedias",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostMedias");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
