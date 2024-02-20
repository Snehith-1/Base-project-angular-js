using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.mastersamagro.Models;
using ems.mastersamagro.DataAccess;


namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to various functions in Buyer Master
    /// </summary>
    /// <remarks>Written by Abilash.A </remarks>

    [RoutePrefix("api/AgrMstbuyer")]
    [Authorize]
    public class AgrMstbuyerController : ApiController
    {
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAgrMstbuyer objDaAgrMstbuyer = new DaAgrMstbuyer();

        [ActionName("PostMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostMobileNo(MdlbuyerMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaAgrMstbuyer.DaPostMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetMobileNoList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerMobileNo values = new MdlbuyerMobileNo();
            objDaAgrMstbuyer.DaGetMobileNoList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //----------- Delete Mobile No----------//
        [ActionName("DeleteMobileNo")]
        [HttpGet]
        public HttpResponseMessage deleteMmobileno(string buyer2mobileno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerMobileNo values = new MdlbuyerMobileNo();
            objDaAgrMstbuyer.DaDeleteMobileNo(buyer2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostEmailAddress(MdlbuyerEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaAgrMstbuyer.DaPostEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetEmailAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerEmailAddress values = new MdlbuyerEmailAddress();
            objDaAgrMstbuyer.DaGetEmailAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteEmailAddress(string buyer2emailaddress_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerEmailAddress values = new MdlbuyerEmailAddress();
            objDaAgrMstbuyer.DaDeleteEmailAddress(buyer2emailaddress_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAddress")]
        [HttpPost]
        public HttpResponseMessage PostAaddress(MdlbuyerAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaAgrMstbuyer.DaPostAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerAddress values = new MdlbuyerAddress();
            objDaAgrMstbuyer.DaGetAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteAddress(string buyer2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerAddress values = new MdlbuyerAddress();
            objDaAgrMstbuyer.DaDeleteAddress(buyer2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostBank")]
        [HttpPost]
        public HttpResponseMessage PostBank(MdlbuyerBank values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaAgrMstbuyer.DaPostBank(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBankList")]
        [HttpGet]
        public HttpResponseMessage GetBankList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerBank values = new MdlbuyerBank();
            objDaAgrMstbuyer.DaGetBankList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBank")]
        [HttpGet]
        public HttpResponseMessage DeleteBank(string buyer2bank_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerBank values = new MdlbuyerBank();
            objDaAgrMstbuyer.DaDeleteBank(buyer2bank_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBankAccountLevel")]
        [HttpGet]
        public HttpResponseMessage GetBankAccountLevel()
        {
            MdlMstbuyer objMdlMstbuyer = new MdlMstbuyer();
            objDaAgrMstbuyer.DaGetBankAccountLevel(objMdlMstbuyer);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlMstbuyer);
        }

        [ActionName("GetYearsAndMonthsInBusiness")]
        [HttpGet]
        public HttpResponseMessage getage(string businessstart_date)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlMstbuyer values = new MdlMstbuyer();
            objDaAgrMstbuyer.DaGetYearsAndMonthsInBusiness(businessstart_date, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("buyerSave")]
        [HttpPost]
        public HttpResponseMessage buyerSave(MdlMstbuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaAgrMstbuyer.DabuyerSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("buyerSubmit")]
        [HttpPost]
        public HttpResponseMessage buyerSubmit(MdlMstbuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaAgrMstbuyer.DabuyerSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetbuyerSummary")]
        [HttpGet]
        public HttpResponseMessage GetbuyerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlMstbuyer values = new MdlMstbuyer();
            objDaAgrMstbuyer.DaGetbuyerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Deletebuyer")]
        [HttpGet]
        public HttpResponseMessage Deletebuyer(string buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlMstbuyer values = new MdlMstbuyer();
            objDaAgrMstbuyer.DaDeletebuyer(buyer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostGST")]
        [HttpPost]
        public HttpResponseMessage PostGSTDetail(MdlbuyerGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaAgrMstbuyer.DaPostGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostGSTList")]
        [HttpPost]
        public HttpResponseMessage PostGSTList(MdlbuyerGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaAgrMstbuyer.DaPostGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGSTList")]
        [HttpGet]
        public HttpResponseMessage GetGSTDetailsList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerGST values = new MdlbuyerGST();
            objDaAgrMstbuyer.DaGetGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteGST")]
        [HttpGet]
        public HttpResponseMessage DeleteGST(string buyer2gst_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerGST values = new MdlbuyerGST();
            objDaAgrMstbuyer.DaDeleteGST(buyer2gst_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteGSTBuyer")]
        [HttpGet]
        public HttpResponseMessage DeleteGSTBuyer(string buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerGST values = new MdlbuyerGST();
            objDaAgrMstbuyer.DaDeleteGSTBuyer(getsessionvalues.employee_gid, buyer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPostalCodeDetails")]
        [HttpGet]
        public HttpResponseMessage GetPostalCodeDetails(string postal_code)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerAddress values = new MdlbuyerAddress();
            objDaAgrMstbuyer.DaGetPostalCodeDetails(postal_code, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

// Temp Clear 
        [ActionName("GetbuyerTempClear")]
        [HttpGet]
        public HttpResponseMessage GetbuyerTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            result values = new result();
            objDaAgrMstbuyer.DaGetbuyerTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // buyer Basic Details Update
        [ActionName("buyerEditSave")]
        [HttpPost]
        public HttpResponseMessage buyerEditSave(MdlMstbuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaAgrMstbuyer.DabuyerEditSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("buyerEditSubmit")]
        [HttpPost]
        public HttpResponseMessage buyerEditSubmit(MdlMstbuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaAgrMstbuyer.DabuyerEditSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("buyerEditUpdate")]
        [HttpPost]
        public HttpResponseMessage buyerEditUpdate(MdlMstbuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaAgrMstbuyer.DabuyerEditUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBankAccountType")]
        [HttpGet]
        public HttpResponseMessage GetBankAccountType()
        {
            MdlMstbuyer objMdlMstbuyer = new MdlMstbuyer();
            objDaAgrMstbuyer.DaGetBankAccountType(objMdlMstbuyer);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlMstbuyer);
        }

    }
}