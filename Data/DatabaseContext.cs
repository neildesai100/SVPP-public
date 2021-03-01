using Microsoft.EntityFrameworkCore;
using SVPP.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SVPP.Data
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<SVPP.Models.Nonprofit> Nonprofit { get; set; }
        public DbSet<SVPP.Models.Partner> Partner { get; set; }

    }
}
