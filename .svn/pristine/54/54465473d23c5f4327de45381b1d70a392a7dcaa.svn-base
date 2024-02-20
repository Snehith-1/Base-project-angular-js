using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web;
using System.Web.Http;
using ems.hbapiconn.Models;
using ems.hbapiconn.Functions;
namespace ems.hbapiconn.Contoller
{
    [RoutePrefix("api/SamFinEncoreLoanAccount")]
    [Authorize]

    public class SamFinEncoreLoanAccountController : ApiController
    {
        FnSamFinEncoreLoanAccount objFnSamFinEncoreLoanAccount = new FnSamFinEncoreLoanAccount();
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();
 

        [ActionName("CreateLoanAccount")]
        [HttpPost]
        public HttpResponseMessage CreateLoanAccount(MdlCreateLoanRequest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlOpenLoanAccountResponse objMdlOpenLoanAccountResponse = new MdlOpenLoanAccountResponse();
            var postResult = objFnSamFinEncoreLoanAccount.CreateLoanAccount(values, getsessionvalues.employee_gid);

            return Request.CreateResponse(HttpStatusCode.OK, postResult);
        }

        [ActionName("CreateLoanAccountFarmer")]
        [HttpPost]
        public HttpResponseMessage CreateLoanAccountFarmer(MdlCreateLoanRequest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlOpenLoanAccountResponse objMdlOpenLoanAccountResponse = new MdlOpenLoanAccountResponse();
            var postResult = objFnSamFinEncoreLoanAccount.CreateLoanAccountFarmer(values, getsessionvalues.employee_gid);

            return Request.CreateResponse(HttpStatusCode.OK, postResult);
        }

        [ActionName("CreateLoanAccountSupplier")]
        [HttpPost]
        public HttpResponseMessage CreateLoanAccountSupplier(MdlCreateLoanRequest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlOpenLoanAccountResponse objMdlOpenLoanAccountResponse = new MdlOpenLoanAccountResponse();
            var postResult = objFnSamFinEncoreLoanAccount.CreateLoanAccountSupplier(values, getsessionvalues.employee_gid);

            return Request.CreateResponse(HttpStatusCode.OK, postResult);
        }

        //Batch Loan Account Creation in Encore for Farmer
        [ActionName("BatchCreateLoanAccountFarmer")]
        [HttpPost]
        public HttpResponseMessage BatchCreateLoanAccountFarmer(MdlCreateLoanRequest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objFnSamFinEncoreLoanAccount.DaBatchCreateLoanAccountFarmer(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}