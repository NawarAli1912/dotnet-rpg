using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Character> Characters => Set<Character>();

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}