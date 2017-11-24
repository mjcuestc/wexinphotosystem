using MMPSystemManager.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMPSystemManager.Module
{
    public class Userinfo
    {
        [Key]
        public string UserNumber { get; set; }         //编号
        public string UserName { get; set; }        //姓名
        public string UserWechatName { get; set; }        //微信号
        public string UserId { get; set; }          //身份证号
        public string UserContactPhone { get; set; }        //联系电话
        public string UserContactEmail { get; set; }        //联系邮箱
        public string UserFacepict{ get; set; }     //用户人脸信息
        public DateTime UserPicTime { get; set; }       //拍照时间
        public string Remark { get; set; }          //备注
    }
}
