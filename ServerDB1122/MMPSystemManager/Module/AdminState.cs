using MMPSystemManager.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMPSystemManager.Module
{
    public class AdminState
    {
        [Key]
        public string AdminNumber { get; set; }    //编号
        public bool AdminOnline { get; set; }         //是否在线
        public DateTime AdminLoginTime { get; set; }       //最近登录时间
        public string Remark { get; set; }              //备注
    }
}
