using MMPSystemManager.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMPSystemManager.Module
{
    public class UserAerialpicture
    {
        [Key]
        public string UserNumber { get; set; }     //编号
        public string UserPictureLocation { get; set; }     //航拍地点
        public string UserAerialPict { get; set; }      //航拍图片
        public DateTime UserPicTime { get; set; }       //拍照时间
        public string Remark { get; set; }      //备注
    }
}
