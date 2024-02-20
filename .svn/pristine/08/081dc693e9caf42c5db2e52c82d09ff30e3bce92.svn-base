using ems.brs.Dataacess;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.brs.Models;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace ems.brs.Controllers
{
    [RoutePrefix("api/BRSMaster")]
    [Authorize]
    public class BRSMasterController : ApiController
    {
        DaBRSMaster objDaBRSMaster = new DaBRSMaster();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        //BRS Avtivity - start
        [ActionName("GetBRSActivity")]
        [HttpGet]
        public HttpResponseMessage GetBRSActivity()
        {
            MdlBRSMaster objMdlBRSMaster = new MdlBRSMaster();
            objDaBRSMaster.DaGetBRSActivity(objMdlBRSMaster);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlBRSMaster);
        }

        [ActionName("CreateBRSActivity")]
        [HttpPost]
        public HttpResponseMessage CreateBRSActivity(BRSActivity values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaBRSMaster.DaCreateBRSActivity(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditBRSActivity")]
        [HttpGet]
        public HttpResponseMessage EditBRSActivity(string brsactivity_gid)
        {
            BRSActivity values = new BRSActivity();
            objDaBRSMaster.DaEditBRSActivity(brsactivity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateBRSActivity")]
        [HttpPost]
        public HttpResponseMessage UpdateBRSActivity(BRSActivity values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaBRSMaster.DaUpdateBRSActivity(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //[ActionName("BRSActivityDelete")]
        //[HttpGet]
        //public HttpResponseMessage BRSActivityDelete(string brsactivity_gid)
        //{
        //    BRSActivity values = new BRSActivity();
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaBRSMaster.DaBRSActivityDelete(brsactivity_gid, getsessionvalues.employee_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("BRSActivityStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage BRSActivityStatusUpdate(BRSActivity values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaBRSMaster.DaBRSActivityStatusUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBRSActivityActiveLog")]
        [HttpGet]
        public HttpResponseMessage GetBRSActivityActiveLog(string brsactivity_gid)
        {
            MdlBRSMaster values = new MdlBRSMaster();
            objDaBRSMaster.DaGetBRSActivityActiveLog(brsactivity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBRSActivityStatus")]
        [HttpGet]
        public HttpResponseMessage GetBRSActivityStatus()
        {
            MdlBRSMaster values = new MdlBRSMaster();
            objDaBRSMaster.DaGetBRSActivityStatus(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //BRS Avtivity - end


    }
}