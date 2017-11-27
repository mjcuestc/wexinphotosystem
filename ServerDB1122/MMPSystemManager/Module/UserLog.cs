using MMPSystemManager.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMPSystemManager.Module
{
    public class UserLog
    {
        [Key]
        public string UserNumber { get; set; }         //编号
        public string UserPasswd { get; set; }         //密码
        public DateTime UserLoginTime { get; set; }       //最近登录时间
        public string Remark { get; set; }              //备注
    }
}
