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
            if (i  !=  2)
                return false;

            req.TryGetValue(tt[0], out id);//ID
            req.TryGetValue(tt[1], out passwd);//password

            if ((string.Compare(id, null) == 0) || (string.Compare(passwd, null) == 0))
            {
                return false;
            }
            bool tableempty = true;
            foreach (var ty in _context.AdminLogs)           
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

            var db = _context.AdminLogs.Find(id);

            if (object.Equals(db,null))
                return false;
            else
            {
                if (string.Compare(db.AdminPasswd, passwd) == 0)
                {
                    var linq = (from obj in _context.AdminLogs
                                where obj.AdminNumber == id
                                select obj).SingleOrDefault();

                    linq.AdminOnline = true;
                    linq.AdminLoginTime = System.DateTime.Now;

                    int row = _context.SaveChanges();
                    if (row > 0)
                    {
                        return true;
                    }
                    else return false;

                }
                else return false;
            }

        }

        [HttpPost]
        [Route("Getall")]
        public JArray Getall()              //目前只写Admin,userinfo表
        {
            IFormCollection req = Request.Form;
            System.Collections.Generic.ICollection<string> t3;
            //var session = Request.Cookies;
            var sed = HttpContext.Request.Headers["Authorization"];
            t3 = req.Keys;
            string[] tt = new string[20];
            StringValues id;
            int i = 0;
            foreach (string x in t3)
            {
                tt[i++] = x;
            }
            req.TryGetValue(tt[0], out id);

            JObject staff1 = new JObject();
            JArray staff = new JArray();

            if (i != 1)             //传入格式判断
                return staff;

            //try                     //权限判断 
            //{
            //    string query2 = (from obj in _context.AdminInfos
            //                     where (obj.AdminNumber == id)
            //                     select obj.AdminGrade).First();//grade

            //    if (string.Compare(query2, "2") > 0)               
            //    {
            //        if (string.Compare(tt[0], "admin") == 0)         //权限等级大于2级   无法查看其余管理员信息
            //            return staff;
            //        else if (((string.Compare(tt[0], "userinfo") == 0) || (string.Compare(tt[0], "userpicture") == 0))
            //            && (string.Compare(query2, "3") == 0))           //权限等级3级   可以查看用户信息
            //            ;
            //        else return staff;
            //    }
                    
            //}
            //catch
            //{
            //    var linq = (from obj in _context.AdminInfos
            //                select obj).SingleOrDefault();

            //    if (!((string.Compare(id, "SuperAdministrator") == 0) && (object.Equals(linq,null) )))
            //        return staff;
            //}



            if (string.Compare(tt[0], "admin") == 0)
            {
                var query =
                    from obj in _context.AdminInfos
                    select new
                    {
                        AdminNumber = obj.AdminNumber,
                        AdminId = obj.AdminId,
                        AdminGrade = obj.AdminGrade,
                        AdminName = obj.AdminName,
                        AdminContactPhone = obj.AdminContactPhone,
                        AdminContactEmail = obj.AdminContactEmail,
                        AdminLogTime = obj.AdminLogTime,
                        AdminPicture = obj.AdminPicture,
                        AdminIdPict = obj.AdminIdPict,
                    };
                var res = query.ToList();
                var query2 =
                    from obj2 in _context.AdminLogs
                    select new
                    {
                        AdminOnline = obj2.AdminOnline,
                        AdminLoginTime = obj2.AdminLoginTime,
                    };
                var res2 = query2.ToList();
                var query3 =
                    from obj3 in _context.AdminUploadPictures
                    select new
                    {
                        AdminUploadPict = obj3.AdminUploadPict,
                        AdminUploadTime = obj3.AdminUploadTime
                    };
                var res3 = query3.ToList();

                for (i = 0; i < res.Count; i++)
                {
                    staff1.Add(new JProperty("AdminName", "" + res[i].AdminName + ""));
                    staff1.Add(new JProperty("AdminId", "" + res[i].AdminId + ""));
                    staff1.Add(new JProperty("AdminGrade", "" + res[i].AdminGrade + ""));
                    staff1.Add(new JProperty("AdminIdPict", "" + res[i].AdminIdPict + ""));
                    staff1.Add(new JProperty("AdminPicture", "" + res[i].AdminPicture + ""));
                    staff1.Add(new JProperty("AdminLogTime", "" + res[i].AdminLogTime + ""));
                    staff1.Add(new JProperty("AdminContactEmail", "" + res[i].AdminContactEmail + ""));
                    staff1.Add(new JProperty("AdminContactPhone", "" + res[i].AdminContactPhone + ""));
                    staff1.Add(new JProperty("AdminNumber", "" + res[i].AdminNumber + ""));

                    if (i < res2.Count)
                    {
                        staff1.Add(new JProperty("AdminLoginTime", "" + res2[i].AdminLoginTime + ""));
                        staff1.Add(new JProperty("AdminOnline", "" + res2[i].AdminOnline + ""));
                    }
                    else
                    {
                        staff1.Add(new JProperty("AdminLoginTime", " "));
                        staff1.Add(new JProperty("AdminOnline", " "));
                    }

                    if (i < res3.Count)
                    {
                        staff1.Add(new JProperty("AdminUploadPict", "" + res3[i].AdminUploadPict + ""));
                        staff1.Add(new JProperty("AdminUploadTime", "" + res3[i].AdminUploadTime + ""));
                    }
                    else
                    {
                        staff1.Add(new JProperty("AdminUploadPict", " "));
                        staff1.Add(new JProperty("AdminUploadTime", " "));
                    }


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
            System.Collections.Generic.ICollection<string> t3;

            t3 = req.Keys;
            string[] tt = new string[20];
            StringValues id1,id2;
            int i = 0;
            foreach (string x in t3)
            {
                tt[i++] = x;
            }
            req.TryGetValue(tt[0], out id1);
            //req.TryGetValue(tt[1], out id2);

            //if (i != 2)             //传入格式判断
            //    return false;

            //try                     //权限判断 
            //{
            //    string query2 = (from obj in _context.AdminInfos
            //                     where (obj.AdminNumber == id2)
            //                     select obj.AdminGrade).First();//grade
            //    if (!(string.Compare(query2, "1") == 0))                //只有权限等级为1级 才可以删除
            //        return false;
            //}
            //catch
            //{
            //    var linq = (from obj in _context.AdminInfos
            //                select obj).SingleOrDefault();

            //    if (!((string.Compare(id2, "SuperAdministrator") == 0) && (object.Equals(linq, null))))
            //        return false;
            //}

            var ty = _context.AdminInfos.Find(id1);        
            if (object.Equals(ty, null))
            {
                return false;
            }
            else
            {
                _context.AdminInfos.Remove(ty);
                int row = _context.SaveChanges();
                if (row <=  0)
                {
                    return false;
                }
                var ty2 = _context.AdminLogs.Find(id1);
                _context.AdminLogs.Remove(ty2);
                 row = _context.SaveChanges();
                if (row <= 0)
                {
                    return false;
                }
                var ty3 = _context.AdminUploadPictures.Find(id1);
                _context.AdminUploadPictures.Remove(ty3);
                row = _context.SaveChanges();
                if (row <= 0)
                {
                    return false;
                }
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

            //if (string.Compare(nam[0], "admin") == 0)
            //{
            //    try                     //权限判断 
            //    {
            //        string query2 = (from obj in _context.AdminInfos
            //                         where (obj.AdminNumber == val[9])
            //                         select obj.AdminGrade).First();//grade
            //        if (!(string.Compare(query2, "1") == 0))            //只有权限等级为1级 才可以插入
            //            return false;
            //    }
            //    catch
            //    {
            //        var linq = (from obj in _context.AdminInfos
            //                    select obj).SingleOrDefault();

            //        if (!((string.Compare(val[9], "SuperAdministrator") == 0) && (object.Equals(linq, null))))
            //            return false;
            //    }
            //}
             

            

            if (string.Compare(nam[0], "admin") == 0)
            {
                //if (i != 10)
                //    return false;

                var ty = _context.AdminInfos.Find(val[0]);
                if (!object.Equals(ty, null))
                {
                    return false;
                }

                AdminInfo dest = new AdminInfo
                {
                    AdminNumber = "" + val[0] + "",
                    AdminId = "" + val[1] + "",
                    AdminGrade = "" + val[2] + "",
                    AdminName = "" + val[3] + "",
                    AdminContactPhone = "" + val[5] + "",
                    AdminContactEmail = "" + val[6] + "",
                    AdminPicture = "" + val[7] + "",
                    AdminIdPict = "" + val[8] + "",

                };
                AdminLog dest2 = new AdminLog
                {
                    AdminNumber = "" + val[0] + "",
                    AdminPasswd = "" + val[4] + "",
                    AdminOnline = false,
                };
                AdminUploadPicture dest3 = new AdminUploadPicture
                {
                    AdminNumber = "" + val[0] + "",
                    AdminUploadPict = "C://adminuploadpicture",
                    AdminUploadTime = System.DateTime.Now,
                };

                _context.AdminInfos.Add(dest);
                int row = _context.SaveChanges();
                if (row <= 0)
                {
                    return false;
                }
                _context.AdminLogs.Add(dest2);
                 row = _context.SaveChanges();
                if (row <= 0)
                {
                    return false;
                }
                _context.AdminUploadPictures.Add(dest3);
                row = _context.SaveChanges();
                if (row <= 0)
                {
                    return false;
                }

                return true;
            }
            else if (string.Compare(nam[0], "userinfo") == 0)
            {
                if (i != 8)
                    return false;

                var dest = new Userinfo
                {
                    UserNumber = Guid.NewGuid(),
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
                int row = _context.SaveChanges();
                if (row > 0)
                {
                    return true;
                }
                else return false;
            }
            else
            {
                return false;
            }

        }

        [HttpPost]
        [Route("Update")]
        public bool Update()        //admin
        {
            IFormCollection req = Request.Form;
            String[] val = new String[20];
            System.Collections.Generic.ICollection<string> t3;

            t3 = req.Keys;
            int i = 0;
            StringValues te;
            foreach (string x in t3)
            {
                req.TryGetValue(x, out te);
                val[i] = te;
                te = x;
                i++;
            }
            //if (i != 10)
            //    return false;

            //try                     //权限判断 
            //{
            //    string query2 = (from obj in _context.AdminInfos
            //                     where (obj.AdminNumber == val[9])
            //                     select obj.AdminGrade).First();//grade
            //    if (!((string.Compare(query2, "1") == 0)||(string.Compare(val[0], val[9]) == 0)))            //只有权限等级为1级 或自己 才可以更新
            //        return false;
            //}
            //catch
            //{
            //    var linq3 = (from obj in _context.AdminInfos
            //                select obj).SingleOrDefault();

            //    if (!((string.Compare(val[9], "SuperAdministrator") == 0) && (object.Equals(linq3, null))))
            //        return false;
            //}

            var linq=(from obj in _context.AdminInfos
                      where obj.AdminNumber == val[0]
                      select obj).SingleOrDefault();

            if (object.Equals(linq, null))
            {
                return false;
            }

            linq.AdminId = val[1];
            linq.AdminGrade = val[2];
            linq.AdminName = val[3];
            linq.AdminContactPhone = val[5];
            linq.AdminContactEmail = val[6];
            linq.AdminPicture = val[7];
            linq.AdminIdPict = val[8];

            int row = _context.SaveChanges();
            if (row >= 0)
            {
                return true;
            }

            var linq1 = (from obj in _context.AdminLogs
                        where obj.AdminNumber == val[0]
                        select obj).SingleOrDefault();

            if (object.Equals(linq1, null))
            {
                return false;
            }
            if (string.Compare(val[0], val[10]) == 0)
            {
                linq1.AdminOnline = true;
            }           
            else linq1.AdminOnline = false;
            linq1.AdminPasswd = val[4];

             row = _context.SaveChanges();
            if (row >= 0)
            {
                return true;
            }
            else return false;

        }

        [HttpPost]
        [Route("Logout")]
        public bool Logout()
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
            if (i != 1)
                return false;

            req.TryGetValue(tt[0], out id);//ID

            var linq = (from obj in _context.AdminLogs
                        where obj.AdminNumber == id
                        select obj).SingleOrDefault();

            if (object.Equals(linq, null))
            {
                return false;
            }
            linq.AdminOnline = false;

            int row = _context.SaveChanges();
            if (row > 0)
            {
                return true;
            }
            else return false;
        }

        [HttpPost]
        [Route("Search")]
        public JArray Search()
        {
            IFormCollection req = Request.Form;
            StringValues id,id2;
            System.Collections.Generic.ICollection<string> t3;

            t3 = req.Keys;
            string[] tt = new string[20];
            int i = 0;
            foreach (string x in t3)
            {
                tt[i++] = x;
            }

            req.TryGetValue(tt[0], out id);//ID
            //req.TryGetValue(tt[1], out id2);//ID

            JObject staff1 = new JObject();
            JArray staff = new JArray();

            //if (i != 2)
            //{
            //    staff1.Add(new JProperty("Requestformat", "false"));
            //    staff.Add(new JObject(staff1));
            //    return staff;
            //}

            //try                     //权限判断 
            //{
            //    string query2 = (from obj in _context.AdminInfos
            //                     where (obj.AdminNumber == id2)
            //                     select obj.AdminGrade).First();//grade

            //    if (string.Compare(query2, "2") > 0)
            //    {
            //        if ((string.Compare(tt[0], "adminname") == 0) ||(string.Compare(tt[0], "adminid") == 0))        //权限等级大于2级   无法查看其余管理员信息
            //            return staff;
            //        else if  (string.Compare(query2, "3") != 0)         
            //            return staff;
            //    }

            //}
            //catch
            //{
            //    var linq = (from obj in _context.AdminInfos
            //                select obj).SingleOrDefault();

            //    if (!((string.Compare(id2, "SuperAdministrator") == 0) && (object.Equals(linq, null))))
            //        return staff;
            //}

            if (string.Compare(tt[0], "adminid") == 0)
            {
                var query = 
                    from obj in _context.AdminInfos
                    from obj2 in _context.AdminLogs
                    from obj3 in _context.AdminUploadPictures
                    where (obj.AdminNumber == id && obj2.AdminNumber==id  && obj3.AdminNumber==id)
                    select new 
                    {
                        AdminNumber = obj.AdminNumber,
                        AdminId = obj.AdminId,
                        AdminGrade = obj.AdminGrade,
                        AdminName = obj.AdminName,
                        AdminContactPhone = obj.AdminContactPhone,
                        AdminContactEmail = obj.AdminContactEmail,
                        AdminLogTime = obj.AdminLogTime,
                        AdminPicture = obj.AdminPicture,
                        AdminIdPict = obj.AdminIdPict,
                        AdminOnline = obj2.AdminOnline,
                        AdminLoginTime = obj2.AdminLoginTime,
                        AdminUploadPict=obj3.AdminUploadPict,
                        AdminUploadTime=obj3.AdminUploadTime
                    };
                var res = query.ToList();
                

                for (i = 0; i < res.Count; i++)
                {
                    staff1.Add(new JProperty("AdminName", "" + res[i].AdminName + ""));
                    staff1.Add(new JProperty("AdminId", "" + res[i].AdminId + ""));
                    staff1.Add(new JProperty("AdminGrade", "" + res[i].AdminGrade + ""));
                    staff1.Add(new JProperty("AdminIdPict", "" + res[i].AdminIdPict + ""));
                    staff1.Add(new JProperty("AdminLoginTime", "" + res[i].AdminLoginTime + ""));
                    staff1.Add(new JProperty("AdminNumber", "" + res[i].AdminNumber + ""));
                    staff1.Add(new JProperty("AdminOnline", "" + res[i].AdminOnline + ""));
                    staff1.Add(new JProperty("AdminPicture", "" + res[i].AdminPicture + ""));
                    staff1.Add(new JProperty("AdminLogTime", "" + res[i].AdminLogTime + ""));
                    staff1.Add(new JProperty("AdminContactEmail", "" + res[i].AdminContactEmail + ""));
                    staff1.Add(new JProperty("AdminContactPhone", "" + res[i].AdminContactPhone + ""));
                    staff1.Add(new JProperty("AdminUploadPict", "" + res[i].AdminUploadPict + ""));
                    staff1.Add(new JProperty("AdminUploadTime", "" + res[i].AdminUploadTime + ""));

                    staff.Add(new JObject(staff1));
                    staff1.RemoveAll();
                }
            }
            else if (string.Compare(tt[0], "adminname") == 0)
            {                            
                var query = 
                    from obj in _context.AdminInfos
                    from obj2 in _context.AdminLogs
                    from obj3 in _context.AdminUploadPictures
                    where (obj.AdminName == id && obj.AdminNumber == obj2.AdminNumber && obj3.AdminNumber == obj2.AdminNumber)
                    select new 
                    {
                        AdminNumber = obj.AdminNumber,
                        AdminId = obj.AdminId,
                        AdminGrade = obj.AdminGrade,
                        AdminName = obj.AdminName,
                        AdminContactPhone = obj.AdminContactPhone,
                        AdminContactEmail = obj.AdminContactEmail,
                        AdminLogTime = obj.AdminLogTime,
                        AdminPicture = obj.AdminPicture,
                        AdminIdPict = obj.AdminIdPict,
                        AdminOnline = obj2.AdminOnline,
                        AdminLoginTime = obj2.AdminLoginTime,
                        AdminUploadPict = obj3.AdminUploadPict,
                        AdminUploadTime = obj3.AdminUploadTime
                    };
                var res = query.ToList();
               

                for (i = 0; i < res.Count; i++)
                {
                    staff1.Add(new JProperty("AdminName", "" + res[i].AdminName + ""));
                    staff1.Add(new JProperty("AdminId", "" + res[i].AdminId + ""));
                    staff1.Add(new JProperty("AdminGrade", "" + res[i].AdminGrade + ""));
                    staff1.Add(new JProperty("AdminIdPict", "" + res[i].AdminIdPict + ""));
                    staff1.Add(new JProperty("AdminLoginTime", "" + res[i].AdminLoginTime + ""));
                    staff1.Add(new JProperty("AdminNumber", "" + res[i].AdminNumber + ""));
                    staff1.Add(new JProperty("AdminOnline", "" + res[i].AdminOnline + ""));
                    staff1.Add(new JProperty("AdminPicture", "" + res[i].AdminPicture + ""));
                    staff1.Add(new JProperty("AdminLogTime", "" + res[i].AdminLogTime + ""));
                    staff1.Add(new JProperty("AdminContactEmail", "" + res[i].AdminContactEmail + ""));
                    staff1.Add(new JProperty("AdminContactPhone", "" + res[i].AdminContactPhone + ""));
                    staff1.Add(new JProperty("AdminUploadPict", "" + res[i].AdminUploadPict + ""));
                    staff1.Add(new JProperty("AdminUploadTime", "" + res[i].AdminUploadTime + ""));

                    staff.Add(new JObject(staff1));
                    staff1.RemoveAll();
                }
            }
            else if (string.Compare(tt[0], "username") == 0)
            {
                var query = from obj in _context.Userinfos
                            where obj.UserName == id
                            select new Userinfo
                            {
                                UserNumber = obj.UserNumber,
                                UserName = obj.UserName,
                                UserWechatName = obj.UserWechatName,
                                UserId = obj.UserId,
                                UserContactPhone = obj.UserContactPhone,
                                UserContactEmail = obj.UserContactEmail,
                                UserFacepict = obj.UserFacepict,
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
            else if (string.Compare(tt[0], "usertelephone") == 0)
            {
                var query = from obj in _context.Userinfos
                            where obj.UserContactPhone == id
                            select new Userinfo
                            {
                                UserNumber = obj.UserNumber,
                                UserName = obj.UserName,
                                UserWechatName = obj.UserWechatName,
                                UserId = obj.UserId,
                                UserContactPhone = obj.UserContactPhone,
                                UserContactEmail = obj.UserContactEmail,
                                UserFacepict = obj.UserFacepict,
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

            return staff;
        }


    }
}