using MMPSystemManager.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMPSystemManager.Module
{
    public class AdminInfo
    {      
        [Key]
        public string AdminNumber{ get; set; }    //编号
        public string AdminId { get; set; }         //身份证号
        public string AdminGrade { get; set; }      //权限等级
        public string AdminName { get; set; }       //姓名
        public string AdminContactPhone { get; set; }        //联系电话
        public string AdminContactEmail { get; set; }        //联系邮箱
        public string AdminPicture { get; set; }            //人脸图片
        public DateTime  AdminLogTime { get; set; }         //注册时间
        public string AdminIdPict { get; set; }             //身份证图片
        public string Remark { get; set; }              //备注
    }
}
