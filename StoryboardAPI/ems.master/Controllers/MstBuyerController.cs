using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.master.Models;
using ems.master.DataAccess;
/// <summary>
/// (It's used for Buyer Master in Samfin)Buyer Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>

namespace ems.master.Controllers
{
    [RoutePrefix("api/Mstbuyer")]
    [Authorize]
    public class MstbuyerController : ApiController
    {
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMstBuyer objDaMstbuyer = new DaMstBuyer();

        [ActionName("PostMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostMobileNo(MdlbuyerMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaMstbuyer.DaPostMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetMobileNoList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerMobileNo values = new MdlbuyerMobileNo();
            objDaMstbuyer.DaGetMobileNoList(getsessionvalues.employee_gid, values);
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
            objDaMstbuyer.DaDeleteMobileNo(buyer2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostEmailAddress(MdlbuyerEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaMstbuyer.DaPostEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetEmailAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerEmailAddress values = new MdlbuyerEmailAddress();
            objDaMstbuyer.DaGetEmailAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteEmailAddress(string buyer2emailaddress_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerEmailAddress values = new MdlbuyerEmailAddress();
            objDaMstbuyer.DaDeleteEmailAddress(buyer2emailaddress_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostAddress")]
        [HttpPost]
        public HttpResponseMessage PostAaddress(MdlbuyerAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaMstbuyer.DaPostAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerAddress values = new MdlbuyerAddress();
            objDaMstbuyer.DaGetAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAddress")]
        [HttpGet]
        public HttpResponseMessage DeleteAddress(string buyer2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerAddress values = new MdlbuyerAddress();
            objDaMstbuyer.DaDeleteAddress(buyer2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostBank")]
        [HttpPost]
        public HttpResponseMessage PostBank(MdlbuyerBank values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaMstbuyer.DaPostBank(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBankList")]
        [HttpGet]
        public HttpResponseMessage GetBankList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerBank values = new MdlbuyerBank();
            objDaMstbuyer.DaGetBankList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBank")]
        [HttpGet]
        public HttpResponseMessage DeleteBank(string buyer2bank_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerBank values = new MdlbuyerBank();
            objDaMstbuyer.DaDeleteBank(buyer2bank_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBankAccountLevel")]
        [HttpGet]
        public HttpResponseMessage GetBankAccountLevel()
        {
            MdlMstbuyer objMdlMstbuyer = new MdlMstbuyer();
            objDaMstbuyer.DaGetBankAccountLevel(objMdlMstbuyer);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlMstbuyer);
        }

        [ActionName("GetYearsAndMonthsInBusiness")]
        [HttpGet]
        public HttpResponseMessage getage(string businessstart_date)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlMstbuyer values = new MdlMstbuyer();
            objDaMstbuyer.DaGetYearsAndMonthsInBusiness(businessstart_date, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("buyerSave")]
        [HttpPost]
        public HttpResponseMessage buyerSave(MdlMstbuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaMstbuyer.DabuyerSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("buyerSubmit")]
        [HttpPost]
        public HttpResponseMessage buyerSubmit(MdlMstbuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaMstbuyer.DabuyerSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetbuyerSummary")]
        [HttpGet]
        public HttpResponseMessage GetbuyerSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlMstbuyer values = new MdlMstbuyer();
            objDaMstbuyer.DaGetbuyerSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Deletebuyer")]
        [HttpGet]
        public HttpResponseMessage Deletebuyer(string buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlMstbuyer values = new MdlMstbuyer();
            objDaMstbuyer.DaDeletebuyer(buyer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostGST")]
        [HttpPost]
        public HttpResponseMessage PostGSTDetail(MdlbuyerGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaMstbuyer.DaPostGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostGSTList")]
        [HttpPost]
        public HttpResponseMessage PostGSTList(MdlbuyerGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaMstbuyer.DaPostGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGSTList")]
        [HttpGet]
        public HttpResponseMessage GetGSTDetailsList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerGST values = new MdlbuyerGST();
            objDaMstbuyer.DaGetGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteGST")]
        [HttpGet]
        public HttpResponseMessage DeleteGST(string buyer2gst_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerGST values = new MdlbuyerGST();
            objDaMstbuyer.DaDeleteGST(buyer2gst_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteGSTBuyer")]
        [HttpGet]
        public HttpResponseMessage DeleteGSTBuyer(string buyer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerGST values = new MdlbuyerGST();
            objDaMstbuyer.DaDeleteGSTBuyer(getsessionvalues.employee_gid, buyer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPostalCodeDetails")]
        [HttpGet]
        public HttpResponseMessage GetPostalCodeDetails(string postal_code)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlbuyerAddress values = new MdlbuyerAddress();
            objDaMstbuyer.DaGetPostalCodeDetails(postal_code, values);
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
            objDaMstbuyer.DaGetbuyerTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // buyer Basic Details Update
        [ActionName("buyerEditSave")]
        [HttpPost]
        public HttpResponseMessage buyerEditSave(MdlMstbuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaMstbuyer.DabuyerEditSave(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("buyerEditSubmit")]
        [HttpPost]
        public HttpResponseMessage buyerEditSubmit(MdlMstbuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaMstbuyer.DabuyerEditSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("buyerEditUpdate")]
        [HttpPost]
        public HttpResponseMessage buyerEditUpdate(MdlMstbuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaMstbuyer.DabuyerEditUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBankAccountType")]
        [HttpGet]
        public HttpResponseMessage GetBankAccountType()
        {
            MdlMstbuyer objMdlMstbuyer = new MdlMstbuyer();
            objDaMstbuyer.DaGetBankAccountType(objMdlMstbuyer);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlMstbuyer);
        }

    }
}