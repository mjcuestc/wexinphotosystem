using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MMPSystemManager.DBContext;
using MMPSystemManager.Controllers.SystemApi;
using MMPSystemManager.Module;

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
        public LoginResponse Login([FromBody]LoginRequest req)
        {
            LoginResponse res = new LoginResponse();


            SystemUser user = _context.SystemUsers.Where(e => e.SystemUserID == req.UserID & e.Passwd == req.Passwd).FirstOrDefault();

            if(user != null)
            {
                res.IsSuccess = true;
                res.Message = "Success";
            }
            else
            {
                res.IsSuccess = false;
                res.Message = "ErrorInfo";
            }

            return res;

        }
    }
}