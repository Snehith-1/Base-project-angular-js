using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.system.DataAccess;
using ems.system.Models;

namespace ems.system.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        DaUser objdauser = new DaUser();

        [Route("userdata")]
        [HttpGet]
        public HttpResponseMessage getUserData(string user_gid)
        {
            UserData objresult = new UserData();
            objdauser.userDataFromDB(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        // Get User Menu

        [ActionName("menu")]
        [HttpGet]
        public HttpResponseMessage getMenu(string user_gid)
        {
            menu_response objresult = new menu_response();
            objdauser.loadMenuFromDB(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("privilege")]
        [HttpGet]
        public HttpResponseMessage getprivilege(string user_gid)
        {
            project_list objresult = new project_list();
            objdauser.projectprivilege(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
        [ActionName("privilegelevel3")]
        [HttpGet]
        public HttpResponseMessage getprivilegelevel3(string user_gid)
        {
            projectlist objresult = new projectlist();
            objdauser.privilegelevel3(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("companydetails")]
        [HttpGet]
        public HttpResponseMessage getcompanydetails()
        {
            companydetails objcompanydetails = new companydetails();
            objdauser.getcompanydetails(objcompanydetails);
            return Request.CreateResponse(HttpStatusCode.OK, objcompanydetails);
        }

        [ActionName("topmenu")]
        [HttpGet]
        public HttpResponseMessage getTopMenu(string user_gid)
        {
            menu_response objresult = new menu_response();
            objdauser.loadTopMenuFromDB(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }
    }
}
