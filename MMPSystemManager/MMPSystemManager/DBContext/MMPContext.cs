using Microsoft.EntityFrameworkCore;
using MMPSystemManager.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMPSystemManager.DBContext
{
    public class MMPContext : DbContext
    {
        public MMPContext(DbContextOptions<MMPContext> options) : base(options)
        {

        }

        public DbSet<SystemUser> SystemUsers { get; set; }
    }
}
