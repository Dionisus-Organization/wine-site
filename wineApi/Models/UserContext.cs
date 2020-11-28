using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace wineApi.Models
{
    public class UserContext : DbContext
    {
        public UserContext( DbContextOptions<UserModel> options )
            : base( options ) { }

        public DbSet<UserModel> User { get; set; }
    }
}
