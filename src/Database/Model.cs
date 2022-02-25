using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BDiC.Database
{
    public class IncomeContext : DbContext
    {
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Post> Posts { get; set; }

        public String DbPath { get; }

        public IncomeContext()
        {
            // 実行ファイルのパスを取得
            var path = Environment.CurrentDirectory;

            // DBファイルの保存先とDBファイル名
            DbPath = System.IO.Path.Join(path, "income.db");
        }

        // パスに SQLite のDBファイルを作成
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }

    public class Income
    {
        public int Id { get; set; }
        public DateTime date { get; set; }

        public List<Post> Posts { get; } = new();
    }

    public class Post
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String State { get; set; }
        public String Condition { get; set; }
        public String Warranty { get; set; }
        public int Value { get; set; }
        public String Url { get; set; }

        public int IncomeNo { get; set; }
        public Income Income { get; set; }
    }
}
