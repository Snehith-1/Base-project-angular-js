using ems.hbapiconn.Functions;
using ems.hbapiconn.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.hbapiconn.Contoller
{
    [RoutePrefix("api/SamFinEncoreDisbursement")]
    [Authorize]

    public class SamFinEncoreDisbursementController : ApiController
    {
        FnSamFinEncoreDisbursement objFnSamFinEncoreDisbursement = new FnSamFinEncoreDisbursement();
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();

        //Disbursement booking in Encore for Farmer
        [ActionName("PostEncoreDisbursementFarmer")]
        [HttpPost]
        public HttpResponseMessage PostEncoreDisbursementFarmer(MdlEncoreDisbursement values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            var postResult = objFnSamFinEncoreDisbursement.DaPostEncoreDisbursementFarmer(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, postResult);
        }

        //Disbursement booking in Encore for Applicant(B2B)
        [ActionName("PostEncoreDisbursementB2B")]
        [HttpPost]
        public HttpResponseMessage PostEncoreDisbursementB2B(MdlEncoreDisbursement values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            var postResult = objFnSamFinEncoreDisbursement.DaPostEncoreDisbursementB2B(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, postResult);
        }

        //Disbursement booking in Encore for Supplier
        [ActionName("PostEncoreDisbursementSupplier")]
        [HttpPost]
        public HttpResponseMessage PostEncoreDisbursementSupplier(MdlEncoreDisbursement values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            var postResult = objFnSamFinEncoreDisbursement.DaPostEncoreDisbursementSupplier(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, postResult);
        }

        //Batch Disbursement booking in Encore for Farmer
        [ActionName("BatchEncoreDisbursementFarmer")]
        [HttpPost]
        public HttpResponseMessage BatchEncoreDisbursementFarmer(MdlEncoreDisbursement values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objFnSamFinEncoreDisbursement.DaBatchEncoreDisbursementFarmer(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Batch Disbursement booking in Encore for Supplier
        [ActionName("BatchEncoreDisbursementSupplier")]
        [HttpPost]
        public HttpResponseMessage BatchEncoreDisbursementSupplier(MdlEncoreDisbursement values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objFnSamFinEncoreDisbursement.DaBatchEncoreDisbursementSupplier(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}