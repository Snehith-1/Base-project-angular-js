using ems.masterng.DataAccess;
using ems.masterng.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Threading.Tasks;


namespace ems.masterng.Controllers
{

    [RoutePrefix("api/MstNgApplicationAdd")]
    [Authorize]
    public class MstNgApplicationAddController : ApiController
    {
        DaMstNgApplicationAdd objMstNgApplicationAdd = new DaMstNgApplicationAdd();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        

        //Get New Application

        [ActionName("GetApplicationNewSummary")]
        [HttpPost]
        public HttpResponseMessage GetApplicationNewSummary(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgApplicationAdd.DaGetApplicationNewSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Rejected Applicaiton
        [ActionName("GetApplicationRejectedSummary")]
        [HttpPost]
        public HttpResponseMessage GetApplicationRejectedSummary(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgApplicationAdd.DaGetApplicationRejectedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Hold Applicaiton
        [ActionName("GetApplicationHoldSummary")]
        [HttpPost]
        public HttpResponseMessage GetApplicationHoldSummary(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgApplicationAdd.DaGetApplicationHoldSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Get Approved Applicaiton
        [ActionName("GetApplicationApprovedSummary")]
        [HttpPost]
        public HttpResponseMessage GetApplicationapprovedSummary(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgApplicationAdd.DaGetApplicationApprovedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ApplicationCount")]
        [HttpGet]
        public HttpResponseMessage ApplicationCount()
        {
            ApplicationCount values = new ApplicationCount();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgApplicationAdd.DaApplicationCount(getsessionvalues.user_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("GetInstitutionGSTList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionGSTList()
        
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstGST values = new MdlMstGST();
            objMstNgApplicationAdd.DaGetInstitutionGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostProductDetailAdd")]
        [HttpPost]
        public HttpResponseMessage PostProductDetailAdd(MdlMstProductDetailAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgApplicationAdd.DaPostProductDetailAdd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductDetailList")]
        [HttpGet]
        public HttpResponseMessage GetProductDetailList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailList values = new MdlMstProductDetailList();
            objMstNgApplicationAdd.DaGetProductDetailList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DeleteProductDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteProductDetail(string application2product_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailAdd values = new MdlMstProductDetailAdd();
            objMstNgApplicationAdd.DaDeleteProductDetail(application2product_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SubmitGeneralDtl")]
        [HttpPost]
        public HttpResponseMessage SubmitGeneralDtl(MdlMstApplicationAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgApplicationAdd.DaSubmitGeneralDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTempApp")]
        [HttpGet]
        public HttpResponseMessage GetTempApp()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstNgApplicationAdd.GetTempApp(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SubmitInstitutionDtl")]
        [HttpPost]
        public HttpResponseMessage SubmitInstitutionDtl(MdlNgMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgApplicationAdd.DaSubmitInstitutionDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostInstitutionGST")]
        [HttpPost]
        public HttpResponseMessage PostInstitutionGST(MdlMstGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgApplicationAdd.DaPostInstitutionGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostProduct")]
        [HttpPost]
        public HttpResponseMessage PostProduct(MdlMstProductDetailAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgApplicationAdd.DaPostProduct(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProductDetails")]
        [HttpGet]
        public HttpResponseMessage GetProductDetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailList values = new MdlMstProductDetailList();
            objMstNgApplicationAdd.DaGetProductDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTempProduct")]
        [HttpGet]
        public HttpResponseMessage GetTempProduct()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objMstNgApplicationAdd.GetTempProduct(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualSubmit")]
        [HttpPost]
        public HttpResponseMessage IndividualSubmit(MdlMstContact values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgApplicationAdd.DaIndividualSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("stakeholderindividuallistsummary")]
        [HttpGet]
        public HttpResponseMessage stakeholderindividuallistsummary(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstProductDetailList values = new MdlMstProductDetailList();
            objMstNgApplicationAdd.Dastakeholderindividuallistsummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateGSTHeadOffice")]
        [HttpPost]
        public HttpResponseMessage UpdateGSTHeadOffice(MdlGSTHeadOffice values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgApplicationAdd.DaUpdateGSTHeadOffice(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SubmitInstitutionAddressDtl")]
        [HttpPost]
        public HttpResponseMessage SubmitInstitutionAddressDtl(MdlNgMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgApplicationAdd.DaSubmitInstitutionAddressDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SubmitIndividualAddressDtl")]
        [HttpPost]
        public HttpResponseMessage SubmitIndividualAddressDtl(MdlNgMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgApplicationAdd.DaSubmitIndividualAddressDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SubmitGSTDtl")]
        [HttpPost]
        public HttpResponseMessage SubmitGSTDtl(MdlNgMstInstitutionAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgApplicationAdd.SubmitGSTDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Institution Address List

        [ActionName("GetInstitutionAddressList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstAddressDetails values = new MdlMstAddressDetails();
            objMstNgApplicationAdd.DaGetInstitutionAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Genetic Code
        [ActionName("GetGeneticCodeList")]
        [HttpGet]
        public HttpResponseMessage GetGeneticCodeList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGeneticCode values = new MdlGeneticCode();
            objMstNgApplicationAdd.DaGetGeneticCodeList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Genetic Code
        [ActionName("PostGeneticCode")]
        [HttpPost]
        public HttpResponseMessage PostGeneticCode(MdlMstBuisnessGeneticCode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgApplicationAdd.DaPostGeneticCode(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Summary Borrower Details --> Individual --->Address Form -->Address List Show FunctionLity

        [ActionName("GetAddressList")]
        [HttpGet]
        public HttpResponseMessage GetAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactAddress values = new MdlContactAddress();
            objMstNgApplicationAdd.DaGetAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDocumentSummary")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionEditSummary(string application_gid)
        {
            applicationdocument values = new applicationdocument();
            objMstNgApplicationAdd.DaGetDocumentSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDocumentListSummary")]
        [HttpGet]
        public HttpResponseMessage GetDocumentListSummary(string application_gid, string type, string stackholder_gid)
        {
            applicationlistdocument values = new applicationlistdocument();
            objMstNgApplicationAdd.DaGetDocumentListSummary(application_gid,type, stackholder_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Bureau 
        [ActionName("GetapplicationBureauList")]
        [HttpGet]
        public HttpResponseMessage GetapplicationBureauList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlContactBureau values = new MdlContactBureau();
            objMstNgApplicationAdd.DaGetapplicationBureauList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}