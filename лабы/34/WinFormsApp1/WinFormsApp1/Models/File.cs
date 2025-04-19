using System;
using System.ComponentModel.DataAnnotations;

namespace DeviceFileLoggerWinForms.Models
{
    public class File
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string FileName { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int FileSize { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }  // Изменили на DateTime

        [Required]
        public DateTime CreationTime { get; set; }  // Изменили на TimeSpan
    }
}