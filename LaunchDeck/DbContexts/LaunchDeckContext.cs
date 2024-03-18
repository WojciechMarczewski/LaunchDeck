using LaunchDeck.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace LaunchDeck.DbContexts
{
    public class LaunchDeckContext : DbContext
    {
        public LaunchDeckContext(): base()
        {
        }
        public DbSet<Note> Notes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\dB\\LaunchDeckDB.db";
            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\dB");
                File.Create(filePath);
            }
            optionsBuilder.UseSqlite($"Data Source={filePath}");
        }
    }
}
