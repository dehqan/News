using Microsoft.EntityFrameworkCore.Migrations;

namespace News.Infrastructure.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Client (Title, IsActive) 
                VALUES
                    ('farsnews', 1), 
                    ('tasnimnews', 1),
                    ('mehrnews', 0),
                    ('isna', 0),
                    ('mashreghnews', 0),
                    ('tabnak', 0),
                    ('irna', 0),
                    ('aftabnews', 0),
                    ('yjc', 0)");

            migrationBuilder.Sql(@"
                INSERT INTO Category (Title, [Order], IsActive) 
                VALUES
                    (N'سیاسی',1 , 1), 
                    (N'ورزشی',2 , 1), 
                    (N'اقتصادی',3 , 1),
                    (N'فرهنگ و هنر',4 , 1),
                    (N'بین الملل',5 , 1),
                    (N'اجتماعی',6 , 1)");

            migrationBuilder.Sql(@"
                INSERT INTO ClientCategory (ClientId, CategoryId, Url, IsActive) 
                    VALUES

                        (1, 1, 'https://www.farsnews.com/rss/politics', 1), 
                        (1, 2, 'https://www.farsnews.com/rss/sports', 1), 
                        (1, 3, 'https://www.farsnews.com/rss/economy', 1),
                        (1, 4, 'https://www.farsnews.com/rss/culture', 1),
                        (1, 5, 'https://www.farsnews.com/rss/world', 1),
                        (1, 6, 'https://www.farsnews.com/rss/social', 1),

                        (2, 1, N'https://www.tasnimnews.com/fa/rss/feed/0/7/1/سیاسی', 1), 
                        (2, 2, N'https://www.tasnimnews.com/fa/rss/feed/0/7/3/ورزشی', 1), 
                        (2, 3, N'https://www.tasnimnews.com/fa/rss/feed/0/7/7/اقتصادی', 1),
                        (2, 4, N'https://www.tasnimnews.com/fa/rss/feed/0/7/4/فرهنگی', 1),
                        (2, 5, N'https://www.tasnimnews.com/fa/rss/feed/0/7/8/بین-الملل', 1),
                        (2, 6, N'https://www.tasnimnews.com/fa/rss/feed/0/7/2/اجتماعی', 1),

                        (3, 1, 'https://www.mehrnews.com/rss/tp/7', 1),
                        (3, 2, 'https://www.mehrnews.com/rss/tp/9', 1),
                        (3, 3, 'https://www.mehrnews.com/rss/tp/25', 1),
                        (3, 4, 'https://www.mehrnews.com/rss/tp/2', 1),
                        (3, 4, 'https://www.mehrnews.com/rss/tp/1', 1),
                        (3, 5, 'https://www.mehrnews.com/rss/tp/8', 1),
                        (3, 6, 'https://www.mehrnews.com/rss/tp/6', 1),

                        (4, 1, 'https://www.isna.ir/rss/tp/14', 1),
                        (4, 2, 'https://www.isna.ir/rss/tp/24', 1),
                        (4, 3, 'https://www.isna.ir/rss/tp/34', 1),
                        (4, 4, 'https://www.isna.ir/rss/tp/20', 1),
                        (4, 5, 'https://www.isna.ir/rss/tp/17', 1),
                        (4, 6, 'https://www.isna.ir/rss/tp/9', 1),

                        (5, 1, 'https://www.mashreghnews.ir/rss/tp/2', 1),
                        (5, 2, 'https://www.mashreghnews.ir/rss/tp/10', 1),
                        (5, 3, 'https://www.mashreghnews.ir/rss/tp/16', 1),
                        (5, 4, 'https://www.mashreghnews.ir/rss/tp/4', 1),
                        (5, 5, 'https://www.mashreghnews.ir/rss/tp/5', 1),
                        (5, 6, 'https://www.mashreghnews.ir/rss/tp/14', 1),

                        (6, 1, 'https://www.tabnak.ir/fa/rss/1/1', 1),
                        (6, 2, 'https://www.tabnak.ir/fa/rss/1/19', 1),
                        (6, 3, 'https://www.tabnak.ir/fa/rss/1/4', 1),
                        (6, 4, 'https://www.tabnak.ir/fa/rss/1', 1),
                        (6, 4, 'https://www.tabnak.ir/fa/rss/1/9', 1),
                        (6, 5, 'https://www.tabnak.ir/fa/rss/1/5', 1),
                        (6, 6, 'https://www.tabnak.ir/fa/rss/1/3', 1),

                        (7, 1, 'https://www.irna.ir/rss/tp/5', 1),
                        (7, 2, 'https://www.irna.ir/rss/tp/14', 1),
                        (7, 3, 'https://www.irna.ir/rss/tp/20', 1),
                        (7, 4, 'https://www.irna.ir/rss/tp/41', 1),
                        (7, 5, 'https://www.irna.ir/rss/tp/1', 1),
                        (7, 6, 'https://www.irna.ir/rss/tp/32', 1),

                        (8, 1, 'https://aftabnews.ir/fa/rss/5', 1),
                        (8, 2, 'https://aftabnews.ir/fa/rss/8', 1),
                        (8, 3, 'https://aftabnews.ir/fa/rss/2', 1),
                        (8, 4, 'https://aftabnews.ir/fa/rss/3', 1),
                        (8, 5, 'https://aftabnews.ir/fa/rss/4', 1),
                        (8, 6, 'https://aftabnews.ir/fa/rss/14', 1),

                        (9, 1, 'https://www.yjc.ir/fa/rss/3', 1),
                        (9, 2, 'https://www.yjc.ir/fa/rss/8', 1),
                        (9, 3, 'https://www.yjc.ir/fa/rss/6', 1),
                        (9, 4, 'https://www.yjc.ir/fa/rss/4', 1),
                        (9, 5, 'https://www.yjc.ir/fa/rss/9', 1),
                        (9, 6, 'https://www.yjc.ir/fa/rss/5', 1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
