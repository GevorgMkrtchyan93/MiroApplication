using Microsoft.EntityFrameworkCore;

using System;

namespace Miro.Server.Entities
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
    }
}
