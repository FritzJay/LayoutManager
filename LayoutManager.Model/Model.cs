using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LayoutManager.Model
{
    public class LayoutManagerContext : DbContext
    {
        public DbSet<Layout> Layouts { get; set; }
        public DbSet<Process> Processes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=layoutManager.db");
        }
    }

    public class Layout
    {
        public int LayoutId { get; set; }
        public string Name { get; set; }

        public List<Process> Posts { get; set; }
    }

    public class Process
    {
        public int ProcessId { get; set; }
        public int ProcessName { get; set; }
        public int Path { get; set; }
        public int Arguments { get; set; }
        public int LocationURL { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
    }
}
