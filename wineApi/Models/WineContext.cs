using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wineApi.Models
{
    public class WineContext : DbContext
    {
        public WineContext(DbContextOptions<WineContext> options) 
            : base( options ) { }

        public DbSet<WineModel> WineModels { get; set; }
    }
}
