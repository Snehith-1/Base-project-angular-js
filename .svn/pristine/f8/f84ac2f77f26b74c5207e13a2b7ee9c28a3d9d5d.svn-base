using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.rsk.Models;
using ems.rsk.DataAccess;
using System.Web;
using ems.utilities.Models;
using ems.utilities.Functions;

namespace StoryboardAPI.Controllers.ems.lgl
{
    [RoutePrefix("api/customerManagement")]
    [Authorize]

    public class customerManagementController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaCustomerManagement objDaCustomerManagement = new DaCustomerManagement();

        [ActionName("getcustomerListdtl")]
        [HttpGet]
        public HttpResponseMessage GetCustomerList()
        {
            customerlist objvalues = new customerlist();
            objDaCustomerManagement.DaGetCustomerList(objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("postcustomerPromoter")]
        [HttpPost]
        public HttpResponseMessage PostCustomerPromoter(customerPromoter values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomerManagement.DaPostCustomerPromoter(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("postcustomerGuarantors")]
        [HttpPost]
        public HttpResponseMessage PostcustomerGuarantors(customerGuarantors values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomerManagement.DaPostcustomerGuarantors(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("postcustomerPromoterEdit")]
        [HttpPost]
        public HttpResponseMessage PostCustomerPromoterEdit(customerPromoter values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomerManagement.DaPostCustomerPromoterEdit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("postcustomerGuarantorsEdit")]
        [HttpPost]
        public HttpResponseMessage PostCustomerGuarantorsEdit(customerGuarantors values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomerManagement.DaPostCustomerGuarantorsEdit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getcustomerGuarantors")]
        [HttpGet]
        public HttpResponseMessage GetCustomerGuarantors(string customer_gid)
        {
            customerGuarantorslist objcustomerGuarantors = new customerGuarantorslist();
            objDaCustomerManagement.DaGetCustomerGuarantors(customer_gid, objcustomerGuarantors);
            return Request.CreateResponse(HttpStatusCode.OK, objcustomerGuarantors);
        }

        [ActionName("getcustomerPromoter")]
        [HttpGet]
        public HttpResponseMessage GetCustomerPromoter(string customer_gid)
        {
            customerpromotorslist objcustomerPromoter = new customerpromotorslist();
            objDaCustomerManagement.DaGetCustomerPromoter(customer_gid, objcustomerPromoter);
            return Request.CreateResponse(HttpStatusCode.OK, objcustomerPromoter);
        }


        // Delete  Guarantors &  Promoter......//

        [ActionName("postGuarantorsdetail")]
        [HttpGet]
        public HttpResponseMessage PostGuarantorsDetail(string customer2guarantor_gid)
        {
            customerGuarantors objcustomerGuarantors = new customerGuarantors();
            objDaCustomerManagement.DaPostGuarantorsDetail(customer2guarantor_gid, objcustomerGuarantors);
            return Request.CreateResponse(HttpStatusCode.OK, objcustomerGuarantors);
        }

        [ActionName("postPromoterdetail")]
        [HttpGet]
        public HttpResponseMessage PostPromoterdetail(string customer2promotor_gid)
        {
            customerPromoter objcustomerPromoter = new customerPromoter();
            objDaCustomerManagement.DaPostPromoterdetail(customer2promotor_gid, objcustomerPromoter);
            return Request.CreateResponse(HttpStatusCode.OK, objcustomerPromoter);
        }

        [ActionName("getPromoterdetail")]
        [HttpGet]
        public HttpResponseMessage GetPromoterdetail(string customer2promotor_gid)
        {
            customerPromoter objcustomerPromoter = new customerPromoter();
            objDaCustomerManagement.DaGetPromoterdetail(customer2promotor_gid, objcustomerPromoter);
            return Request.CreateResponse(HttpStatusCode.OK, objcustomerPromoter);
        }


        [ActionName("getGuarantorsdetail")]
        [HttpGet]
        public HttpResponseMessage GetGuarantorsdetail(string customer2guarantor_gid)
        {
            customerGuarantors objcustomerGuarantors = new customerGuarantors();
            objDaCustomerManagement.DaGetGuarantorsdetail(customer2guarantor_gid, objcustomerGuarantors);
            return Request.CreateResponse(HttpStatusCode.OK, objcustomerGuarantors);
        }


        [ActionName("getCollateraldetail")]
        [HttpGet]
        public HttpResponseMessage DaGetCollateraldetail(string customer_gid)
        {
            customerCollaterallist objcustomerCollateral = new customerCollaterallist();
            objDaCustomerManagement.DaGetCollateraldetail(customer_gid, objcustomerCollateral);
            return Request.CreateResponse(HttpStatusCode.OK, objcustomerCollateral);
        }

        [ActionName("getSanctiondetail")]
        [HttpGet]
        public HttpResponseMessage DaGetSanctionDtl(string customer_gid)
        {
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaCustomerManagement.DaGetSanctionDtl(customer_gid, objsanctiondetailsList);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("getAssignRMdetail")]
        [HttpGet]
        public HttpResponseMessage GetAssignRMdetail(string customer_gid)
        {
            assignedRM objassignedRM = new assignedRM();
            objDaCustomerManagement.DaGetAssignRMdetail(customer_gid, objassignedRM);
            return Request.CreateResponse(HttpStatusCode.OK, objassignedRM);
        }

        [ActionName("Getsanctionloandetails")]
        [HttpGet]
        public HttpResponseMessage Getsanctionloandetails(string customer_gid)
        {
            sanctionloan values = new sanctionloan();
            objDaCustomerManagement.DaGetsanctionloandetails(values, customer_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetloanListDetails")]
        [HttpGet]
        public HttpResponseMessage GetloanListDetails(string sanction_gid)
        {
            loanListdetail values = new loanListdetail();
            objDaCustomerManagement.DaGetloanListDetails(sanction_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCustomerRMDetail")]
        [HttpGet]
        public HttpResponseMessage GetCustomerRMDetail()
        {
            customerRMdtl objMdlCustomer = new customerRMdtl();
            objDaCustomerManagement.DaGetCustomerRMDetail(objMdlCustomer);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCustomer);
        }

        #region escrow
        // Create Escrow
        [ActionName("EscrowCreate")]
        [HttpPost]
        public HttpResponseMessage EscrowCreate(escrow objResult)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCustomerManagement.DaPostEscrowCreate (objResult,getsessionvalues .user_gid );
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        // Delete Escrow
        [ActionName("EscrowDelete")]
        [HttpGet]
        public HttpResponseMessage EscrowDelete(String escrow_gid)
        {
            result objResult = new result();
            objDaCustomerManagement.DaPostEscrowDelete (escrow_gid,objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        //  Escrow Summary
        [ActionName("EscrowSummary")]
        [HttpGet]
        public HttpResponseMessage EscrowSummary(String customer_gid)
        {
            escrowSummaryList objResult = new escrowSummaryList();
            objDaCustomerManagement.DaGetEscrowSummary(customer_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("EscrowView")]
        [HttpGet]
        public HttpResponseMessage EscrowView(String escrow_gid)
        {
            escrow  objResult = new escrow ();
            objDaCustomerManagement.DaGetEscrowView(escrow_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        #endregion

        [ActionName("HistoryEscrowSummary")]
        [HttpGet]
        public HttpResponseMessage HistoryEscrowSummary(String allocationdtl_gid)
        {
            escrowSummaryList objResult = new escrowSummaryList();
            objDaCustomerManagement.DaGetHistoryEscrowSummary(allocationdtl_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("HistoryEscrowView")]
        [HttpGet]
        public HttpResponseMessage HistoryEscrowView(String escrow_gid)
        {
            escrow objResult = new escrow();
            objDaCustomerManagement.DaGetHistoryEscrowView(escrow_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("GetTrnSanctionDocumentUpload")]
        [HttpGet]
        public HttpResponseMessage GetTrnSanctionDocumentUpload(string allocationdtl_gid,string customer_gid)
        {
            uploaddocument values = new uploaddocument();
            objDaCustomerManagement.DaGetTrnSanctionDocumentUpload(values, allocationdtl_gid, customer_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
