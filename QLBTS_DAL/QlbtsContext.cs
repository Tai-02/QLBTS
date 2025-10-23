using QLBTS_DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DAL
{
    public class QlbtsContext : DbContext
    {
        public DbSet<SanPhamDTO> SanPhams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Dùng chuỗi kết nối trong DatabaseHelper
                optionsBuilder.UseMySql(
                    "Server=127.0.0.1;Database=QLBTS;Uid=root;Pwd=48692005;",
                    new MySqlServerVersion(new Version(8, 0, 34))
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SanPhamDTO>(entity =>
            {
                entity.ToTable("SanPham");

                entity.HasKey(e => e.MaSP);
                entity.Property(e => e.TenSP)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.HinhAnh)
                      .IsRequired(false);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
