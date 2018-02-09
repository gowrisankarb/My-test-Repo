using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestAPI.Models
{
    public class ShipperInfoContext : DbContext
    {
        public ShipperInfoContext(DbContextOptions<ShipperInfoContext> options)
            : base(options)
        {
        }

        public DbSet<ShipperInfo> ShipperInfoItems { get; set; }
    }
}
