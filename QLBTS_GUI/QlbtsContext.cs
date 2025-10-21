using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_GUI
{
    public class QlbtsContext :DbContext
    {
        public DbSet<SanPham> SanPhams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // ⚠️ NHỚ sửa lại thông tin kết nối cho đúng máy bạn
            optionsBuilder.UseMySql(
                "Server=127.0.0.1;Database=QLBTS;User=root;Password=48692005;",
                new MySqlServerVersion(new Version(8, 0, 34))
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SanPham>()
                .HasKey(s => s.MaSP);

            modelBuilder.Entity<SanPham>()
                .ToTable("SANPHAM");
        }
    }
}
