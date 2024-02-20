using ems.brs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using ems.brs.Dataacess;
using ems.utilities.Functions;
using ems.utilities.Models;
namespace ems.brs.Controllers
{
    /// <summary>
    /// The remianing is below 2 or 3  rupees the ticket will closed automatically condition based master
    /// </summary>
    ///<remarks>Written by Komathi</remarks>
    [RoutePrefix("api/CloseRemaindingAmount")]
    [Authorize]
        public class CloseRemaindingAmountController : ApiController
        {
            DaCloseRemaindingAmount objDaCloseRemaindingAmount = new DaCloseRemaindingAmount();
            session_values Objgetgid = new session_values();
            logintoken getsessionvalues = new logintoken();
            
            [ActionName("GetRemaindingAmount")]
            [HttpGet]
            public HttpResponseMessage GetRemaindingAmount()
            {
                MdlCloseRemaindingAmount objMdlCloseRemaindingAmount = new MdlCloseRemaindingAmount();
                objDaCloseRemaindingAmount.DaGetRemaindingAmount(objMdlCloseRemaindingAmount);
                return Request.CreateResponse(HttpStatusCode.OK, objMdlCloseRemaindingAmount);
            }
            [ActionName("CreateRemaindingAmount")]
            [HttpPost]
            public HttpResponseMessage CreateRemaindingAmount(remaindingamount_list values)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = Objgetgid.gettokenvalues(token);
                objDaCloseRemaindingAmount.DaCreateRemaindingAmount(values, getsessionvalues.employee_gid);
                return Request.CreateResponse(HttpStatusCode.OK, values);
            }
        [ActionName("GetRemainingAmount")]
        [HttpGet]
        public HttpResponseMessage GetRemainingAmount(string remaindingamount_gid)
        {
            remaindingamount_list values = new remaindingamount_list();
            objDaCloseRemaindingAmount.DaGetRemainingAmount(values, remaindingamount_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("RemainingAmountClosed")]
        [HttpPost]
        public HttpResponseMessage RemainingAmountClosed(MdlUnreconciliationManagement values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCloseRemaindingAmount.DaRemainingAmountClosed(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveRemaindingAmount")]
        [HttpPost]
        public HttpResponseMessage InactiveRemaindingAmount(remaindingamount_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCloseRemaindingAmount.DaInactiveRemaindingAmount(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("RemaindingAmountInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage RemaindingAmountInactiveLogview(string remaindingamount_gid)
        {
            MdlCloseRemaindingAmount values = new MdlCloseRemaindingAmount();
            objDaCloseRemaindingAmount.DaRemaindingAmountInactiveLogview(remaindingamount_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRemainingAmountStatus")]
        [HttpGet]
        public HttpResponseMessage GetRemainingAmountStatus()
        {
            remaindingamount_list values = new remaindingamount_list();
            objDaCloseRemaindingAmount.DaGetRemainingAmountStatus(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }

}
