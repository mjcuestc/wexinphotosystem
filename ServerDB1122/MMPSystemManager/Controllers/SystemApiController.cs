using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MMPSystemManager.DBContext;
using MMPSystemManager.Module;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;

namespace MMPSystemManager.Controllers
{
    [Produces("application/json")]
    [Route("api/SystemApi")]
    public class SystemApiController : Controller
    {
        private readonly MMPContext _context;

        public SystemApiController(MMPContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public bool Login()
        {
            IFormCollection req = Request.Form;
            StringValues id, passwd;
            System.Collections.Generic.ICollection<string> t3;

            t3 = req.Keys;
            string[] tt = new string[20];
            int i = 0;
            foreach (string x in t3)
            {
                tt[i++] = x;
            }

            req.TryGetValue(tt[0], out id);//ID
            req.TryGetValue(tt[1], out passwd);//password

            bool tableempty = true;
            foreach (var ty in _context.Admins)           
            {
                tableempty = false;
                break;
            }
            if (tableempty)             //当管理员表为空时  登录账号 密码为SuperAdministrator SuperAdministrator
            {
                if ((string.Compare((string)id, "SuperAdministrator") == 0) && (string.Compare((string)passwd, "SuperAdministrator") == 0))
                    return true;
                else return false;
            }

            var db = _context.Admins.Find(id);

            if (db.Equals(null))
                return false;
            else
            {
                if (string.Compare(db.AdminPasswd, passwd) == 0)
                    return true;
                else
                    return false;
            }


            //_context.SystemUsers.Add(destination);            //插入
            //_context.SaveChanges();
            //_context.SystemUsers.Update(destination);           //更新    
            //_context.SaveChanges();
            //_context.SystemUsers.Remove(destination);           //删除    
            //_context.SaveChanges();
            //var ty = _context.SystemUsers.Find("44");               //查找
            //System.DateTime currentTime = new System.DateTime();              //时间
            // var currentTime = System.DateTime.Now.ToString();
            //string StringName = System.Guid.NewGuid().ToString();         //uuid
            //Guid guid = Guid.NewGuid();

            //foreach (var ty in _context.SystemUsers)            //获取所有
            //{
            //    ;
            //}
        }

        [HttpPost]
        [Route("Getall")]
        public JArray Getall()              //目前只写Admin表
        {
            IFormCollection req = Request.Form;
            System.Collections.Generic.ICollection<string> t3;

            t3 = req.Keys;
            string[] tt = new string[20];
            int i = 0;
            foreach (string x in t3)
            {
                tt[i++] = x;
            }


            //var query = from obj in _context.Admins
            //            select new Admin
            //            {
            //                AdminNumber = obj.AdminNumber,
            //                AdminId = obj.AdminId,
            //                AdminGrade = obj.AdminGrade,
            //                AdminName = obj.AdminName,
            //                AdminPasswd = obj.AdminPasswd,
            //                AdminContactPhone = obj.AdminContactPhone,
            //                AdminContactEmail = obj.AdminContactEmail,
            //                AdminOnline = obj.AdminOnline,
            //                AdminPicture = obj.AdminPicture,
            //                AdminPicTime = obj.AdminPicTime,
            //                AdminPicUpdateTime = obj.AdminPicUpdateTime,
            //                AdminLoginTime = obj.AdminLoginTime,
            //                AdminIdPict = obj.AdminIdPict,
            //                Remark = obj.Remark
            //            };

            //var res = query.ToList();
            //res.Count;

            JObject staff1 = new JObject();
            JArray staff = new JArray();

            if (string.Compare(tt[0], "admin") == 0)
            {
                foreach (var ty in _context.Admins)            //获取所有
                {
                    staff1.Add(new JProperty("AdminPasswd", "" + ty.AdminPasswd + ""));
                    staff1.Add(new JProperty("AdminName", "" + ty.AdminName + ""));
                    staff1.Add(new JProperty("AdminId", "" + ty.AdminId + ""));
                    staff1.Add(new JProperty("AdminGrade", "" + ty.AdminGrade + ""));
                    staff1.Add(new JProperty("AdminIdPict", "" + ty.AdminIdPict + ""));
                    staff1.Add(new JProperty("AdminLoginTime", "" + ty.AdminLoginTime + ""));
                    staff1.Add(new JProperty("AdminNumber", "" + ty.AdminNumber + ""));
                    staff1.Add(new JProperty("AdminOnline", "" + ty.AdminOnline + ""));
                    staff1.Add(new JProperty("AdminPicture", "" + ty.AdminPicture + ""));
                    staff1.Add(new JProperty("AdminPictUpdateTime", "" + ty.AdminPicUpdateTime + ""));
                    staff1.Add(new JProperty("AdminContactEmail", "" + ty.AdminContactEmail + ""));
                    staff1.Add(new JProperty("AdminContactPhone", "" + ty.AdminContactPhone + ""));
                    staff1.Add(new JProperty("Remark", "" + ty.Remark + ""));
                    staff1.Add(new JProperty("AdminPicTime", "" + ty.AdminPicTime + ""));

                    staff.Add(new JObject(staff1));
                    staff1.RemoveAll();
                }
            }
            else if (string.Compare(tt[0], "userinfo") == 0)
            {
                var query = from obj in _context.Userinfos
                            orderby obj.UserPicTime descending
                            select new Userinfo
                            {
                                UserNumber=obj.UserNumber,
                                UserName=obj.UserName,
                                UserWechatName=obj.UserWechatName,
                                UserId =obj.UserId,
                                UserContactPhone =obj.UserContactPhone,
                                UserContactEmail =obj.UserContactEmail,
                                UserFacepict=obj.UserFacepict,
                                UserPicTime = obj.UserPicTime,
                                Remark = obj.Remark
                            };

                var res = query.ToList();

                for (i = 0; i < res.Count; i++)
                {
                    staff1.Add(new JProperty("UserNumber", "" + res[i].UserNumber + ""));
                    staff1.Add(new JProperty("UserName", "" + res[i].UserName + ""));
                    staff1.Add(new JProperty("UserWechatName", "" + res[i].UserWechatName + ""));
                    staff1.Add(new JProperty("UserId", "" + res[i].UserId + ""));
                    staff1.Add(new JProperty("UserContactPhone", "" + res[i].UserContactPhone + ""));
                    staff1.Add(new JProperty("UserContactEmail", "" + res[i].UserContactEmail + ""));
                    staff1.Add(new JProperty("UserFacepict", "" + res[i].UserFacepict + ""));
                    staff1.Add(new JProperty("UserPicTime", "" + res[i].UserPicTime + ""));
                    staff1.Add(new JProperty("Remark", "" + res[i].Remark + ""));

                    staff.Add(new JObject(staff1));
                    staff1.RemoveAll();
                }
            }
            else if (string.Compare(tt[0], "userpicture") == 0)
            {
                var query = from obj in _context.Userpictures
                            orderby obj.UserPicTime descending
                            select new Userpicture
                            {
                                UserNumber = obj.UserNumber,
                                UserName = obj.UserName,
                                UserPictureLocation = obj.UserPictureLocation,
                                UserAerialPict = obj.UserAerialPict,
                                UserChoosePict = obj.UserChoosePict,
                                UserPicTime = obj.UserPicTime,
                                Remark = obj.Remark
                            };

                var res = query.ToList();

                for (i = 0; i < res.Count; i++)
                {
                    staff1.Add(new JProperty("UserNumber", "" + res[i].UserNumber + ""));
                    staff1.Add(new JProperty("UserName", "" + res[i].UserName + ""));
                    staff1.Add(new JProperty("UserPictureLocation", "" + res[i].UserPictureLocation + ""));
                    staff1.Add(new JProperty("UserAerialPict", "" + res[i].UserAerialPict + ""));
                    staff1.Add(new JProperty("UserChoosePict", "" + res[i].UserChoosePict + ""));
                    staff1.Add(new JProperty("UserPicTime", "" + res[i].UserPicTime + ""));
                    staff1.Add(new JProperty("Remark", "" + res[i].Remark + ""));

                    staff.Add(new JObject(staff1));
                    staff1.RemoveAll();
                }
            }


            return staff;
        }

        [HttpPost]
        [Route("Delete")]
        public bool Delete()             //目前只写Admin表
        {
            IFormCollection req = Request.Form;
            StringValues id;
            System.Collections.Generic.ICollection<string> t3;

            t3 = req.Keys;
            string[] tt = new string[20];
            int i = 0;
            foreach (string x in t3)
            {
                tt[i++] = x;
            }
            req.TryGetValue(tt[0], out id);//ID

            var ty = _context.Admins.Find(id);        
            if (object.Equals(ty, null))
            {
                return false;
            }
            else
            {
                _context.Admins.Remove(ty);
                _context.SaveChanges();
                return true;
            }

        }

        [HttpPost]
        [Route("Insert")]
        public bool Insert()         //目前只写Admin表 userinfo
        {
            IFormCollection req = Request.Form;
            String[] val = new String[20];
            System.Collections.Generic.ICollection<string> t3;

            t3 = req.Keys;
            string[] nam = new string[20];
            int i = 0;
            StringValues te;
            foreach (string x in t3)
            {
                nam[i] = x;
                req.TryGetValue(x, out te);
                val[i] = te;
                i++;
            }

            if (string.Compare(nam[0], "admin") == 0)
            {
                if (i != 10)
                    return false;

                Admin dest = new Admin
                {
                    AdminNumber = "" + val[0] + "",
                    AdminId = "" + val[1] + "",
                    AdminGrade = "" + val[2] + "",
                    AdminName = "" + val[3] + "",
                    AdminPasswd = "" + val[4] + "",
                    AdminContactPhone = "" + val[5] + "",
                    AdminContactEmail = "" + val[6] + "",
                    AdminOnline = "0",
                    AdminPicture = "" + val[7] + "",
                    AdminPicTime = System.DateTime.Now,
                    AdminPicUpdateTime = System.DateTime.Now,
                    AdminLoginTime = System.DateTime.Now,
                    AdminIdPict = "" + val[8] + "",
                    Remark = "" + val[9] + ""
                };

                _context.Admins.Add(dest);
                _context.SaveChanges();
            }
            else if (string.Compare(nam[0], "userinfo") == 0)
            {
                if (i != 9)
                    return false;

                var dest = new Userinfo
                {
                    UserNumber = "" + val[0] + "",
                    UserName = "" + val[1] + "",
                    UserWechatName = "" + val[2] + "",
                    UserId = "" + val[3] + "",
                    UserContactPhone = "" + val[4] + "",
                    UserContactEmail = "" + val[5] + "",
                    UserFacepict = "" + val[6] + "",
                    UserPicTime = System.DateTime.Now,
                    Remark = "" + val[7] + ""
                };

                _context.Userinfos.Add(dest);
                _context.SaveChanges();
            }
            else
            {
                return false;
            }

                return true;
        }

        [HttpPost]
        [Route("Update")]
        public bool Update()        //admin
        {
            IFormCollection req = Request.Form;
            String[] val = new String[20];
            System.Collections.Generic.ICollection<string> t3;

            t3 = req.Keys;
            //string[] nam = new string[20];
            int i = 0;
            StringValues te;
            foreach (string x in t3)
            {
              //  nam[i] = x;
                req.TryGetValue(x, out te);
                val[i] = te;
                i++;
            }
            if (i != 10)
                return false;


            var linq=(from obj in _context.Admins
                      where obj.AdminNumber == val[0]
                      select obj).SingleOrDefault();

            linq.AdminId = val[1];
            linq.AdminGrade = val[2];
            linq.AdminName = val[3];
            linq.AdminPasswd = val[4];
            linq.AdminContactPhone = val[5];
            linq.AdminContactEmail = val[6];
            linq.AdminOnline = "1";
            linq.AdminPicture = val[7];
            linq.AdminIdPict = val[8];
            linq.Remark = val[9];

            _context.SaveChanges();

            return true;
            //var dest = new Admin
            //{
            //    AdminNumber = "" + val[0] + "",
            //    AdminId = "" + val[1] + "",
            //    AdminGrade = "" + val[2] + "",
            //    AdminName = "" + val[3] + "",
            //    AdminPasswd = "" + val[4] + "",
            //    AdminContactPhone = "" + val[5] + "",
            //    AdminContactEmail = "" + val[6] + "",
            //    AdminOnline = "" + val[7] + "",
            //    AdminPicture = "" + val[8] + "",
            //    AdminPicTime = System.DateTime.Now,
            //    AdminPicUpdateTime = System.DateTime.Now,
            //    AdminLoginTime = System.DateTime.Now,
            //    AdminIdPict = "" + val[9] + "",
            //    Remark = "" + val[10] + ""
            //};

            //_context.Admins.Update(dest);
            //_context.SaveChanges();

            //return true;
        }
    }
}