using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMPSystemManager.Module
{
    public class SystemUser
    {
        [Key]
        public string SystemUserID { get; set; }
        public string SystemUserName { get; set; }
        public string Passwd { get; set; }
    }
}
