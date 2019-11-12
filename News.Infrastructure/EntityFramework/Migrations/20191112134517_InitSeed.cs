using Microsoft.EntityFrameworkCore.Migrations;

namespace News.Infrastructure.EntityFramework.Migrations
{
    public partial class InitSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Clients (Title, IsActive) 
                VALUES
                    ('Farsnews', 1), 
                    ('Tasnimnews', 1)");

            migrationBuilder.Sql(@"
                INSERT INTO Categories (Title) 
                VALUES
                    (N'سیاسی'), 
                    (N'ورزشی'), 
                    (N'اقتصادی')");

            migrationBuilder.Sql(@"
                INSERT INTO ClientCategories (ClientId, CategoryId, Url, IsActive) 
                    VALUES
                        (1, 1, 'https://www.farsnews.com/rss/politics', 1), 
                        (1, 2, 'https://www.farsnews.com/rss/sports', 1), 
                        (1, 3, 'https://www.farsnews.com/rss/economy', 1),
                        (2, 1, 'https://www.tasnimnews.com/fa/rss/feed/0/7/1/%D8%B3%DB%8C%D8%A7%D8%B3%DB%8C', 1), 
                        (2, 2, 'https://www.tasnimnews.com/fa/rss/feed/0/7/3/%D9%88%D8%B1%D8%B2%D8%B4%DB%8C', 1), 
                        (2, 3, 'https://www.tasnimnews.com/fa/rss/feed/0/7/7/%D8%A7%D9%82%D8%AA%D8%B5%D8%A7%D8%AF%DB%8C', 1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
