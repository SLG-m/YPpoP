using DeviceFileLoggerWinForms.Models;
using Microsoft.EntityFrameworkCore;

namespace DeviceFileLoggerWinForms.Data
{
    public class FilesContext : DbContext
    {
        public DbSet<DeviceFileLoggerWinForms.Models.File> Files { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-9062C6O;Database=FilesDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}