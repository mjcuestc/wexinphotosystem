using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using MMPSystemManager.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;


namespace MMPSystemManager.DBContext
{
    public class MMPContext :DbContext
    {
        public MMPContext(DbContextOptions<MMPContext> options) : base(options)
        {

        }
        
        public DbSet<Userinfo> Userinfos { get; set; }
        public DbSet<Userpicture> Userpictures { get; set; }
        public DbSet<AdminLog> AdminLogs { get; set; }
        public DbSet<AdminInfo> AdminInfos { get; set; }
        public DbSet<AdminUploadPicture> AdminUploadPictures { get; set; }
    }

}
