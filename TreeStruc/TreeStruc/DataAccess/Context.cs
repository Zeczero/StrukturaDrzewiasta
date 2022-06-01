using Microsoft.EntityFrameworkCore;
using TreeStruc.Models;

namespace TreeStruc.DataAccess
{
    public class Context : DbContext
    {
        // deploying database objects
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<Node> Nodes { get; set; }
    }
}
