using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstCreditStatusAdd")]
    [Authorize]

    public class MstCreditStatusAddController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstCreditStatusAdd objDaMstCreditStatusAdd = new DaMstCreditStatusAdd();


        [ActionName("GetbuyerToCreditSummary")]
        [HttpGet]
        public HttpResponseMessage GetbuyerToCreditSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditStatusAdd values = new MdlMstCreditStatusAdd();
            objDaMstCreditStatusAdd.DaGetbuyerToCreditSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BureauDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage BureauDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaMstCreditStatusAdd.DaBureauDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("TmpDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage TmpDocumentDelete(string tmp_documentGid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            uploaddocument objvalues = new uploaddocument();
            objDaMstCreditStatusAdd.DaTmpDocumentDelete(tmp_documentGid, objvalues, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

// Bureau Score Add 
        [ActionName("BureauScoreAdd")]
        [HttpPost]
        public HttpResponseMessage BureauScoreAdd(MdlMstCreditStatusAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditStatusAdd.DaBureauScoreAdd(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

 // Bureau Name View
        [ActionName("BureauScoreView")]
        [HttpGet]
        public HttpResponseMessage BureauScoreView(string buyer_gid)
        {
            creditstatuslist values = new creditstatuslist();
            objDaMstCreditStatusAdd.DaBureauScoreView(buyer_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Bureau Name Delete 
        [ActionName("BureauScoreDelete")]
        [HttpGet]
        public HttpResponseMessage BureauScoreDelete(string bureauscoreadd_GID)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            creditstatuslist values = new creditstatuslist();
            objDaMstCreditStatusAdd.DaBureauScoreDelete(bureauscoreadd_GID, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Temp Clear 
        [ActionName("GetTempClear")]
        [HttpGet]
        public HttpResponseMessage GetTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaMstCreditStatusAdd.DaGetTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// buyer Basic Details Edit
        [ActionName("buyerDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage buyerDetailsEdit(string buyer_gid)
        {
            buyeredit values = new buyeredit();
            objDaMstCreditStatusAdd.DabuyerDetailsEdit(buyer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// buyer Basic Details Update
        [ActionName("buyerDetailsUpdate")]
        [HttpPost]
        public HttpResponseMessage buyerDetailsUpdate(buyeredit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditStatusAdd.DabuyerDetailsUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Mobile Number Add 
        [ActionName("MobileNumberAdd")]
        [HttpPost]
        public HttpResponseMessage MobileNumberAdd(Mdlmobile_no values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditStatusAdd.DaMobileNumberAdd(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

 // Get Mobile Number List
        [ActionName("GetMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetMobileNoList(string buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlmobile_no values = new Mdlmobile_no();
            objDaMstCreditStatusAdd.DaGetMobileNoList(buyer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Mobile Number Edit
        [ActionName("MobileNoEdit")]
        [HttpGet]
        public HttpResponseMessage MobileNoEdit(string buyer2mobileno_gid)
        {
            Mdlmobile_no values = new Mdlmobile_no();
            objDaMstCreditStatusAdd.DaMobileNoEdit(buyer2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Mobile Number Update
        [ActionName("MobileNoUpdate")]
        [HttpPost]
        public HttpResponseMessage MobileNoUpdate(Mdlmobile_no values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditStatusAdd.DaMobileNoUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Delete Mobile No
        [ActionName("DeleteMobileNo")]
        [HttpGet]
        public HttpResponseMessage DeleteMobileNo(string buyer2mobileno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlmobile_no values = new Mdlmobile_no();
            objDaMstCreditStatusAdd.DaDeleteMobileNo(buyer2mobileno_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Email Address Add 
        [ActionName("EmailAddressAdd")]
        [HttpPost]
        public HttpResponseMessage EmailAddressAdd(MdlEmail_address values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditStatusAdd.DaEmailAddressAdd(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Get Email Address List
        [ActionName("GetEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetEmailAddressList(string buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlEmail_address values = new MdlEmail_address();
            objDaMstCreditStatusAdd.DaGetEmailAddressList(buyer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Email Address Edit
        [ActionName("EmailAddressEdit")]
        [HttpGet]
        public HttpResponseMessage EmailAddressEdit(string buyer2emailaddress_gid)
        {
            MdlEmail_address values = new MdlEmail_address();
            objDaMstCreditStatusAdd.DaEmailAddressEdit(buyer2emailaddress_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Email Address Update
        [ActionName("EmailAddressUpdate")]
        [HttpPost]
        public HttpResponseMessage EmailAddressUpdate(MdlEmail_address values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditStatusAdd.DaEmailAddressUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Email Address Delete
        [ActionName("DeleteEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteEmailAddress(string buyer2emailaddress_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlEmail_address values = new MdlEmail_address();
            objDaMstCreditStatusAdd.DaDeleteEmailAddress(buyer2emailaddress_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


// Bank Details Add 
        [ActionName("BankDetailsAdd")]
        [HttpPost]
        public HttpResponseMessage BankDetailsAdd(MdlBank_Details values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditStatusAdd.DaBankDetailsAdd(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Get Bank List
        [ActionName("GetBankList")]
        [HttpGet]
        public HttpResponseMessage GetBankList(string buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBank_Details values = new MdlBank_Details();
            objDaMstCreditStatusAdd.DaGetBankList(buyer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Bank Details Edit
        [ActionName("BankDetailEdit")]
        [HttpGet]
        public HttpResponseMessage BankDetailsEdit(string buyer2bank_gid)
        {
            MdlBank_Details values = new MdlBank_Details();
            objDaMstCreditStatusAdd.DaBankDetailsEdit(buyer2bank_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Bank Details Update
        [ActionName("BankDetailUpdate")]
        [HttpPost]
        public HttpResponseMessage BankDetailUpdate(MdlBank_Details values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditStatusAdd.DaBankDetailUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Bank Details Delete
        [ActionName("DeleteBankDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteBankDetail(string buyer2bank_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBank_Details values = new MdlBank_Details();
            objDaMstCreditStatusAdd.DaDeleteBankDetail(buyer2bank_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Address Details Add 
        [ActionName("AddressDetailAdd")]
        [HttpPost]
        public HttpResponseMessage AddressDetailAdd(MdlMstaddresstype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditStatusAdd.DaAddressDetailAdd(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Get Address List
        [ActionName("GetAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAddressList(string buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlbuyerAddress values = new MdlbuyerAddress();
            objDaMstCreditStatusAdd.DaGetAddressList(buyer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Address Details Edit
        [ActionName("AddressDetailEdit")]
        [HttpGet]
        public HttpResponseMessage AddressDetailEdit(string buyer2address_gid)
        {
            MdlMstaddresstype values = new MdlMstaddresstype();
            objDaMstCreditStatusAdd.DaAddressDetailEdit(buyer2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Address Details Update
        [ActionName("AddressDetailUpdate")]
        [HttpPost]
        public HttpResponseMessage AddressDetailUpdate(MdlMstaddresstype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditStatusAdd.DaAddressDetailUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Address Details Delete
        [ActionName("DeleteAddressDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteAddressDetail(string buyer2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstaddresstype values = new MdlMstaddresstype();
            objDaMstCreditStatusAdd.DaDeleteAddressDetail(buyer2address_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Save As Draft buyer
        [ActionName("SaveAsDraftbuyer")]
        [HttpPost]
        public HttpResponseMessage SaveAsDraftbuyer(buyeredit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditStatusAdd.DaSaveAsDraftbuyer(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Submit Credit Status
        [ActionName("SubmitCreditStatus")]
        [HttpPost]
        public HttpResponseMessage SubmitCreditStatus(buyeredit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditStatusAdd.DaSubmitCreditStatus(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Update Credit Status
        [ActionName("UpdateCreditStatus")]
        [HttpPost]
        public HttpResponseMessage UpdateCreditStatus(buyeredit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditStatusAdd.DaUpdateCreditStatus(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetGSTList")]
        [HttpGet]
        public HttpResponseMessage GetGSTDetailsList(string buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlbuyerGST values = new MdlbuyerGST();
            objDaMstCreditStatusAdd.DaGetGSTList(buyer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostGST")]
        [HttpPost]
        public HttpResponseMessage PostGSTDetail(MdlbuyerGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditStatusAdd.DaPostGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("DeleteGST")]
        [HttpGet]
        public HttpResponseMessage DeleteGST(string buyer2gst_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlbuyerGST values = new MdlbuyerGST();
            objDaMstCreditStatusAdd.DaDeleteGST(buyer2gst_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GSTEdit")]
        [HttpGet]
        public HttpResponseMessage GSTEdit(string buyer2gst_gid)
        {
            MdlbuyerGST values = new MdlbuyerGST();
            objDaMstCreditStatusAdd.DaGSTEdit(buyer2gst_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("GSTUpdate")]
        [HttpPost]
        public HttpResponseMessage GSTUpdate(MdlbuyerGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditStatusAdd.DaGSTUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Inactive CreditStatus buyer
        [ActionName("InactiveCreditStatusbuyer")]
        [HttpPost]
        public HttpResponseMessage InactiveCreditStatusbuyer(buyeredit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditStatusAdd.DaInactiveCreditStatusbuyer(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditStatusbuyerInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage CreditStatusbuyerInactiveLogview(string buyer_gid)
        {
            buyeredit values = new buyeredit();
            objDaMstCreditStatusAdd.DaCreditStatusbuyerInactiveLogview(buyer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("buyerMobileNoList")]
        [HttpGet]
        public HttpResponseMessage buyerMobileNoList(string buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlmobile_no values = new Mdlmobile_no();
            objDaMstCreditStatusAdd.DabuyerMobileNoList(buyer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("buyerEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage buyerEmailAddressList(string buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlEmail_address values = new MdlEmail_address();
            objDaMstCreditStatusAdd.DabuyerEmailAddressList(buyer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("buyerAddressList")]
        [HttpGet]
        public HttpResponseMessage buyerAddressList(string buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlbuyerAddress values = new MdlbuyerAddress();
            objDaMstCreditStatusAdd.DabuyerAddressList(buyer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("buyerBankList")]
        [HttpGet]
        public HttpResponseMessage buyerBankList(string buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlBank_Details values = new MdlBank_Details();
            objDaMstCreditStatusAdd.DabuyerBankList(buyer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("buyerGSTList")]
        [HttpGet]
        public HttpResponseMessage buyerGSTList(string buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlbuyerGST values = new MdlbuyerGST();
            objDaMstCreditStatusAdd.DabuyerGSTList(buyer_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BureauDocList")]
        [HttpGet]
        public HttpResponseMessage BureauDocList(string bureauscoreadd_GID)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditStatusAdd values = new MdlMstCreditStatusAdd();
            objDaMstCreditStatusAdd.DaBureauDocList(bureauscoreadd_GID, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// CreditStatus Approval
        [ActionName("CreditStatusApproval")]
        [HttpPost]
        public HttpResponseMessage CreditStatusApproval(buyeredit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstCreditStatusAdd.DaCreditStatusApproval(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditStatusApprovedBuyer")]
        [HttpGet]
        public HttpResponseMessage GetCreditStatusApprovedBuyer()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditStatusAdd values = new MdlMstCreditStatusAdd();
            objDaMstCreditStatusAdd.DaGetCreditStatusApprovedBuyer(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Approval Status Update

        [ActionName("GetCreditStatusNonApprovedBuyer")]
        [HttpGet]
        public HttpResponseMessage GetCreditStatusNonApprovedBuyer()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstCreditStatusAdd values = new MdlMstCreditStatusAdd();
            objDaMstCreditStatusAdd.DaGetCreditStatusNonApprovedBuyer(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditStatusApprovalLogview")]
        [HttpGet]
        public HttpResponseMessage GetCreditStatusApprovalLogview(string buyer_gid)
        {
            buyeredit values = new buyeredit();
            objDaMstCreditStatusAdd.DaGetCreditStatusApprovalLogview(buyer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Status Summary Count

        [ActionName("GetCreditStatusCount")]
        [HttpGet]
        public HttpResponseMessage GetCreditStatusCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            buyer_count values = new buyer_count();
            objDaMstCreditStatusAdd.DaGetCreditStatusCount(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}