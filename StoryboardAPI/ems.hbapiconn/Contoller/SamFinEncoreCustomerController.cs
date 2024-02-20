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
    [RoutePrefix("api/SamFinEncoreCustomer")]
    [Authorize]

    public class SamFinEncoreCustomerController : ApiController
    {
        FnSamFinEncoreCustomer objFnSamFinEncoreCustomer = new FnSamFinEncoreCustomer();
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();

        //Create Customer in Encore
        [ActionName("PostCreateCustomerEncore")]
        [HttpPost]
        public HttpResponseMessage PostCreateCustomerEncore(MdlcustomercreationLMSAPI values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);        
            var postResult = objFnSamFinEncoreCustomer.DaPostCreateCustomerEncore(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, postResult);
        }

        //Find Customer in Encore Farmer
        [ActionName("PostFindCustomerEncoreFarmer")]
        [HttpPost]
        public HttpResponseMessage PostFindCustomerEncoreFarmer(MdlcustomercreationLMSAPI values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            var postResult = objFnSamFinEncoreCustomer.DaPostFindCustomerEncoreFarmer(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, postResult);
        }

        //Find Customer in Encore Applicant        
        [ActionName("PostFindCustomerEncoreApplicant")]
        [HttpPost]
        public HttpResponseMessage PostFindCustomerEncoreApplicant(MdlcustomercreationLMSAPI values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            var postResult = objFnSamFinEncoreCustomer.DaPostFindCustomerEncoreApplicant(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, postResult);
        }

        //Batch Customer Creation for Farmer(B2B2C)
        [ActionName("BatchCustomerCreationforFarmer")]
        [HttpPost]
        public HttpResponseMessage BatchCustomerCreationforFarmer(MdlCreateCustomer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);            
            objFnSamFinEncoreCustomer.DaBatchCustomerCreationforFarmer(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Batch Find Customer for Farmer(B2B2C)
        [ActionName("BatchFindCustomerforFarmer")]
        [HttpPost]
        public HttpResponseMessage BatchFindCustomerforFarmer(MdlCreateCustomer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objFnSamFinEncoreCustomer.DaBatchFindCustomerforFarmer(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}