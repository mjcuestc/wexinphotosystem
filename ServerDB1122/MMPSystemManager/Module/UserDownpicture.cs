using MMPSystemManager.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMPSystemManager.Module
{
    public class UserDownpicture
    {
        [Key]
        public string UserNumber { get; set; }     //编号
        public string UserDownPicture { get; set; }      //下载的图片
        public DateTime UserDownTime { get; set; }       //最近一次下载时间
        public string Remark { get; set; }      //备注
    }
}
