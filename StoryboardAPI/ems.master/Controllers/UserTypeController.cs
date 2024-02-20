using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.master.Controllers
{
    [RoutePrefix("api/UserType")]
    [Authorize]
    public class UserTypeController : ApiController
    {
        DaUserType objDaUserType = new DaUserType();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("Getipandlogintime")]
        [HttpGet]
        public HttpResponseMessage Getipandlogintime()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            ipandlogintimemodel objipandlogintimemodel = new ipandlogintimemodel();
            DaUserType objDaUserType = new DaUserType();
            objDaUserType.DaGetipandlogintime(objipandlogintimemodel, getsessionvalues.employee_gid, token, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objipandlogintimemodel);
        }

        [ActionName("GetUserType")]
        [HttpGet]
        public HttpResponseMessage getUserType()
        {
            MdlUserType objMdlUserType = new MdlUserType();
            objDaUserType.DaGetUserType(objMdlUserType);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlUserType);
        }

        [ActionName("CreateUserType")]
        [HttpPost]
        public HttpResponseMessage createusertype(usertype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUserType.DaCreateUserType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditUserType")]
        [HttpGet]
        public HttpResponseMessage editusertype(string usertype_gid)
        {
            usertype values = new usertype();
            objDaUserType.DaEditUserType(usertype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateUserType")]
        [HttpPost]
        public HttpResponseMessage updateusertype(usertype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUserType.DaUpdateUserType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteUserType")]
        [HttpGet]
        public HttpResponseMessage deleteusertype(string usertype_gid)
        {
            usertype values = new usertype();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUserType.DaDeleteUserType(usertype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //---get User Type- order by ASC----..
        [ActionName("GetUserTypeASC")]
        [HttpGet]
        public HttpResponseMessage getUserTypeASC()
        {
            MdlUserType objMdlUserType = new MdlUserType();
            objDaUserType.DaGetUserTypeASC(objMdlUserType);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlUserType);
        }
        [ActionName("UserTypeStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage UserTypeStatusUpdate(usertype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaUserType.DaUserTypeStatusUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetActiveLog")]
        [HttpGet]
        public HttpResponseMessage GetActiveLog(string usertype_gid)
        {
            MdlUserType values = new MdlUserType();
            objDaUserType.DaGetActiveLog(usertype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}