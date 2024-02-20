using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.lgl.Models;
using ems.lgl.DataAccess;
namespace StoryboardAPI.Controllers.ems.lgl
{
    [RoutePrefix("api/raiseLegalSR")]
    [Authorize]
    public class raiseLegalSRController : ApiController
    {
        DaRasiselegalSR objlegalSRdataaccess = new DaRasiselegalSR();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        [ActionName("Getpromoter")]
        [HttpGet]
        public HttpResponseMessage getpromoterdtl(string templegalsr_gid)
        {
            MdlRaiselegalSR objlegalSR = new MdlRaiselegalSR();
            objlegalSRdataaccess.DaGetPromoterDtl(templegalsr_gid, objlegalSR);
            return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        }
        [ActionName("collateraldetails")]
        [HttpGet]
        public HttpResponseMessage getcollateral(string templegalsr_gid)
        {
            MdlRaiselegalSR objlegalSR = new MdlRaiselegalSR();
            objlegalSRdataaccess.DaGetCollateral(templegalsr_gid, objlegalSR);
            return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);

        }
        [ActionName("Customer")]
        [HttpGet]
        public HttpResponseMessage getCustomer()
        {
            MdlRaiselegalSR objCustomer = new MdlRaiselegalSR();
            objlegalSRdataaccess.DaGetCustomer(objCustomer);
            return Request.CreateResponse(HttpStatusCode.OK, objCustomer);
        }
        [ActionName("Getguarantor")]
        [HttpGet]
        public HttpResponseMessage getguarantordtl(string templegalsr_gid)
        {
            MdlRaiselegalSR objlegalSR = new MdlRaiselegalSR();
            objlegalSRdataaccess.DaGetGuarantorDtl(templegalsr_gid, objlegalSR);
            return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        }
        [ActionName("getCollateraldetail")]
        [HttpGet]
        public HttpResponseMessage getCollateraldetail(string templegalsr_gid)
        {
            MdlRaiselegalSR objlegalSR = new MdlRaiselegalSR();
            objlegalSRdataaccess.DaGetCollateral(templegalsr_gid, objlegalSR);
            return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        }
        [ActionName("Getcustomerdtl")]
        [HttpGet]
        public HttpResponseMessage getcustomerdtl(string customer_gid)
        {
            MdlRaiselegalSR objlegalSR = new MdlRaiselegalSR();
            objlegalSRdataaccess.DaGetCustomerDtl(customer_gid, objlegalSR);
            return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        }
        [ActionName("raiselegalsr")]
        [HttpPost]
        public HttpResponseMessage postregisterlawyer(MdlRaiselegalSR values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var status = objlegalSRdataaccess.DaPostRaiseLegalSR(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("savelegalsr")]
        [HttpPost]
        public HttpResponseMessage postsavelegalsr(MdlRaiselegalSR values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlegalSRdataaccess.DaPostSaveLegalSR(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("submitraiselegalsr")]
        [HttpPost]
        public HttpResponseMessage postsubmitraiselegalsr(MdlRaiselegalSR values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlegalSRdataaccess.DaPostSubmitRaiseLegalSR(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        ///update
        [ActionName("updateraiselegalsr")]
        [HttpPost]
        public HttpResponseMessage postupdateraiselegalsr(MdlRaiselegalSR values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlegalSRdataaccess.DaPostUpdateRaiseLegalSR(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }





        [ActionName("GetraiselegalSRuser")]
        [HttpGet]
        public HttpResponseMessage getraiselegalSRuser()
        {
            MdlRaiselegalSR objlegalSR = new MdlRaiselegalSR();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlegalSRdataaccess.DaGetRaiseLegalSRUser(objlegalSR, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        }
        [ActionName("GetSR")]
        [HttpGet]
        public HttpResponseMessage getSR()
        {
            MdlRaiselegalSR objlegalSR = new MdlRaiselegalSR();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlegalSRdataaccess.DaGetSR(objlegalSR, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        }

        [ActionName("GetraiselegalSR")]
        [HttpGet]
        public HttpResponseMessage getraiselegalSR()
        {
            MdlRaiselegalSR objlegalSR = new MdlRaiselegalSR();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlegalSRdataaccess.DaGetRaiseLegalSR(objlegalSR, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        }

        [ActionName("facility")]
        [HttpPost]
        public HttpResponseMessage getfacility(facility objfacility)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlegalSRdataaccess.DaGetFacility(getsessionvalues.employee_gid, objfacility);
            return Request.CreateResponse(HttpStatusCode.OK, objfacility);
        }
        [ActionName("getfacilitydtl")]
        [HttpGet]
        public HttpResponseMessage getfacilitydtl(string customer_gid)
        {
            facility objfacilitydtl = new facility();
            objlegalSRdataaccess.DaGetFacilityDtl(customer_gid, objfacilitydtl);
            return Request.CreateResponse(HttpStatusCode.OK, objfacilitydtl);
        }

        [ActionName("deletefacility")]
        [HttpGet]
        public HttpResponseMessage getdeletefacility(string facility_gid)
        {
            facility objfacilitydtl = new facility();
            objlegalSRdataaccess.DaGetDeleteFacility(facility_gid, objfacilitydtl);
            return Request.CreateResponse(HttpStatusCode.OK, objfacilitydtl);
        }

        [ActionName("viewLegalSR")]
        [HttpGet]
        public HttpResponseMessage getviewLegalSR(string raiselegalSR_gid)
        {
            MdlRaiselegalSR objfacilitydtl = new MdlRaiselegalSR();
            var status = objlegalSRdataaccess.DaGetViewLegalSR(raiselegalSR_gid, objfacilitydtl);
            return Request.CreateResponse(HttpStatusCode.OK, objfacilitydtl);
        }

        [ActionName("legalSRApproval")]
        [HttpPost]
        public HttpResponseMessage postlegalSRapproval(approvalstatus values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlegalSRdataaccess.DaPostLegalSRApproval(values, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("legalSRreject")]
        [HttpPost]
        public HttpResponseMessage postlegalSRreject(approvalstatus values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlegalSRdataaccess.DaPostLegalSRReject(values,getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }
        [ActionName("legalSRrejected")]
        [HttpPost]
        public HttpResponseMessage postlegalSRrejected(approvalstatus values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlegalSRdataaccess.DaPostLegalSRRejected(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }


        //[ActionName("legalSRReApproval")]
        //[HttpPost]
        //public HttpResponseMessage postlegalSRReApproval(approvalstatus values)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objlegalSRdataaccess.DaPostlegalSRReApproval(values, getsessionvalues.employee_gid, getsessionvalues.user_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);

        //}

        //[ActionName("legalSRrereject")]
        //[HttpPost]
        //public HttpResponseMessage postlegalSRrereject(approvalstatus values)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objlegalSRdataaccess.DaPostlegalSRrereject(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);

        //}

        [ActionName("legalSRApprovalMgmt")]
        [HttpPost]
        public HttpResponseMessage postlegalSRApprovalMgmt(approvalstatus values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlegalSRdataaccess.DaPostLegalSRApprovalMgmt(values, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("legalSRrejectMgmt")]
        [HttpPost]
        public HttpResponseMessage postlegalSRrejectMgmt(approvalstatus values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlegalSRdataaccess.DaPostLegalSRRejectedMgmt(values, getsessionvalues.employee_gid,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("legalSRAppovesummary")]
        [HttpGet]
        public HttpResponseMessage getlegalSRmgmt()
        {
            MdlRaiselegalSR objvalues = new MdlRaiselegalSR();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlegalSRdataaccess.DaGetLegalSRmgmt(objvalues, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("getlegalSRAppovemgmtSummary")]
        [HttpGet]
        public HttpResponseMessage getlegalSRAppovemgmtSummary()
        {
            MdlRaiselegalSR objvalues = new MdlRaiselegalSR();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlegalSRdataaccess.DaGetLegalSRmgmtSummary(objvalues, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("getlegalSRapprovals")]
        [HttpGet]
        public HttpResponseMessage getlegalSRapprovals(string legalsr_gid)
        {
            approvalstatus objvalues = new approvalstatus();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlegalSRdataaccess.DaGetlegarSRapprovals(objvalues, legalsr_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }


        [ActionName("postlegalSRcontactdtl")]
        [HttpPost]
        public HttpResponseMessage postlegalSRcontactdtl(contactdetailsRM values)
        {
            contactdetailsRM_list objvalues = new contactdetailsRM_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlegalSRdataaccess.DaPostContactDetails(getsessionvalues.employee_gid, values, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);

        }

        [ActionName("getlegalSRtmpcontactdtl")]
        [HttpGet]
        public HttpResponseMessage getlegalSRtempcontactdtl(string customer_gid)
        {
            contactdetailsRM_list objvalues = new contactdetailsRM_list();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlegalSRdataaccess.DaGetLegalSRContactDtl(customer_gid,getsessionvalues.employee_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);

        }


        [ActionName("getlegalSRcontactdtl")]
        [HttpGet]
        public HttpResponseMessage getlegalSRcontactdtl(string templegalsr_gid)
        {
            contactdetailsRM_list objvalues = new contactdetailsRM_list();
            objlegalSRdataaccess.DaGetContactDetailsRM(templegalsr_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }
        [ActionName("Getcontactdtl")]
        [HttpGet]
        public HttpResponseMessage Getcontactdtl(string raiselegalSR_gid)
        {
            contactdetailsRM_list objvalues = new contactdetailsRM_list();
            objlegalSRdataaccess.DaGetcontactdtl(raiselegalSR_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("deletecontactdtl")]
        [HttpGet]
        public HttpResponseMessage getdeletecontactdtl(string tmpcontactdtl_gid)
        {
            result objvalues = new result();
            excel values=new excel();
            objlegalSRdataaccess.GaGetDeleteContactDtl(tmpcontactdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }
        [ActionName("getsamgdetails")]
        [HttpGet]
        public HttpResponseMessage getsamgdetails(string customer_gid,string legalsr_gid)
        {
            MdlRaiselegalSR objvalues = new MdlRaiselegalSR();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objlegalSRdataaccess.DaGetSamgDetails(objvalues, customer_gid, legalsr_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("SLNUpload")]
        [HttpPost]
        public HttpResponseMessage postSLNUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objlegalSRdataaccess.DaPostSLNUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("SLNuploadcancel")]
        [HttpGet]
        public HttpResponseMessage getSLNuploadcancel(string tmpSLN_documentgid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            uploaddocument values = new uploaddocument();
            objlegalSRdataaccess.DaGetSLNuploadcancel(tmpSLN_documentgid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("tmpSLNdocumentclear")]
        [HttpGet]
        public HttpResponseMessage gettmpSLNdocumentclear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            excel objresult = new excel();
            objlegalSRdataaccess.DaGettmpSLNdocumentClear(getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("getSLNdocument")]
        [HttpGet]
        public HttpResponseMessage getSLNdocument(string legalSR_gid)
        {
            assignSRLawyer values = new assignSRLawyer();
            objlegalSRdataaccess.DaGetSLNdocumentDtl(values, legalSR_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getcustomerdetails")]
        [HttpGet]
        public HttpResponseMessage GetCustomer(string customer_gid)
        {
            mdlcustomer values = new mdlcustomer();
            objlegalSRdataaccess.DaGetCustomer(customer_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTempLegalSRdtl")]
        [HttpGet]
        public HttpResponseMessage GetTempLegalSRdtl(string customer_gid)
        {
            MdlRaiselegalSR values = new MdlRaiselegalSR();
            objlegalSRdataaccess.DaGetTempLegalSRdtl(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTempLegalSRdtls")]
        [HttpGet]
        public HttpResponseMessage GetTempLegalSRdtls(string templegalsr_gid)
        {
            MdlRaiselegalSR values = new MdlRaiselegalSR();
            objlegalSRdataaccess.DaGetTempLegalSRdtls(templegalsr_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDemandNoticedtl")]
        [HttpGet]
        public HttpResponseMessage getDemandNoticedtl(string customer_gid)
        {
            Demand_notice objlegalSR = new Demand_notice();
            objlegalSRdataaccess.DaGetDemandNoticedtl(customer_gid, objlegalSR);
            return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        }
        //[ActionName("GetHoldremarks")]
        //[HttpGet]
        //public HttpResponseMessage getHoldremarks(string raiselegalSR_gid)
        //{
        //    holdremarks objlegalSR = new holdremarks();
        //    objlegalSRdataaccess.DaGetHoldremarks(raiselegalSR_gid, objlegalSR);
        //    return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        //}
        [ActionName("getlegalsr2dndtl")]
        [HttpGet]
        public HttpResponseMessage getlegalsr2dndtl(string legalsr_gid)
        {
            Demand_notice objlegalSR = new Demand_notice();
            objlegalSRdataaccess.Dagetlegalsr2dndtl(legalsr_gid, objlegalSR);
            return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        }
        [ActionName("Getsanctionloandtl")]
        [HttpGet]
        public HttpResponseMessage Getsanctionloandtl(string customer_gid)
        {
            sanctionloanlist objvalues = new sanctionloanlist();
            objlegalSRdataaccess. DaGetsanctionloandtl( customer_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }
        [ActionName("GetloanList")]
        [HttpGet]
        public HttpResponseMessage GetloanList(string customer_gid)
        {
            sanctionloanlist objvalues = new sanctionloanlist();
            objlegalSRdataaccess.DaGetloanList(objvalues, customer_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }
        [ActionName("getdeletetempcontRM")]
        [HttpGet]
        public HttpResponseMessage getdeletetempcontRM()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            excel objvalues = new excel();
            objlegalSRdataaccess.DagetdeletetempcontRM(getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }
        [ActionName("getcustomerGuarantors")]
        [HttpGet]
        public HttpResponseMessage GetCustomerGuarantors(string customer_gid)
        {
            customerGuarantorslist objcustomerGuarantors = new customerGuarantorslist();
            objlegalSRdataaccess.DaGetCustomerGuarantors(customer_gid, objcustomerGuarantors);
            return Request.CreateResponse(HttpStatusCode.OK, objcustomerGuarantors);
        }

        [ActionName("getcustomerPromoter")]
        [HttpGet]
        public HttpResponseMessage GetCustomerPromoter(string customer_gid)
        {
            customerpromotorslist objcustomerPromoter = new customerpromotorslist();
            objlegalSRdataaccess.DaGetCustomerPromoter(customer_gid, objcustomerPromoter);
            return Request.CreateResponse(HttpStatusCode.OK, objcustomerPromoter);
        }

        [ActionName("getcollateralinfo")]
        [HttpGet]
        public HttpResponseMessage getcollateralinfo(string legalsr_gid)
        {
            customerCollaterallist objcollateral = new customerCollaterallist();
             objlegalSRdataaccess.Dagetcollateralinfo(legalsr_gid, objcollateral);
            return Request.CreateResponse(HttpStatusCode.OK, objcollateral);
        }
        [ActionName("gettemprmdtl")]
        [HttpGet]
        public HttpResponseMessage gettemprmdtl(string templegalsr_gid)
        {
            contactdetailsRM_list objvalues = new contactdetailsRM_list();
            objlegalSRdataaccess.Dagettemprmdtl(templegalsr_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }
        [ActionName("getrequesteddtl")]
        [HttpGet]
        public HttpResponseMessage getrequesterdetails(string legalsr_gid)
        {
            MdlRaiselegalSR objlegalSR = new MdlRaiselegalSR();
            objlegalSRdataaccess.DaGetRequestedDtl(legalsr_gid, objlegalSR);
            return Request.CreateResponse(HttpStatusCode.OK, objlegalSR);
        }
    }
}
