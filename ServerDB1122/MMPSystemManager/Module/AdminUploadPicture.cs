using MMPSystemManager.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMPSystemManager.Module
{
    public class AdminUploadPicture
    {
        [Key]
        public string AdminNumber { get; set; }    //编号
        public string AdminUploadPict { get; set; }      //上传的图片
        public DateTime AdminUploadTime { get; set; }       //最近一次上传图片时间
        public string Remark { get; set; }      //备注
    }
}
