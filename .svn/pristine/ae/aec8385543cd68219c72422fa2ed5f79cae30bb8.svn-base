using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstSAOnboardingBussDevtVerification")]
    [Authorize]
    public class MstSAOnboardingBussDevtVerificationController : ApiController
    {
        DaMstSAOnboardingBussDevtVerification objDaSABussDevtVerification = new DaMstSAOnboardingBussDevtVerification();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("GetDropDown")]
        [HttpGet]
        public HttpResponseMessage GetDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlValuesList values = new MdlValuesList();
            objDaSABussDevtVerification.DaGetValues(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetComboname")]
        [HttpGet]
        public HttpResponseMessage GetComboname()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlValuesList values = new MdlValuesList();
            objDaSABussDevtVerification.DaGetValuesCombo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSaInstitutionSummary")]
        [HttpGet]
        public HttpResponseMessage GetSaInstitutionSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSABussDevtVerification.DaGetSaInstitutionSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaInstitutionCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetSaInstitutionCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSABussDevtVerification.DaGetSaInstitutionCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetIndividualData")]
        [HttpGet]
        public HttpResponseMessage GetIndividualData()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlValuesList values = new MdlValuesList();
            objDaSABussDevtVerification.DaGetIndividualValues(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetInstitutionData")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionData(string sbainstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlValuesList values = new MdlValuesList();
            objDaSABussDevtVerification.DaGetInstitutionValues(getsessionvalues.employee_gid, values, sbainstitution_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TagRmLoad")]
        [HttpGet]
        public HttpResponseMessage TagRmLoad()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            RM_List values = new RM_List();
            objDaSABussDevtVerification.DaRMLoad(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CompanyBussDevtVerificationupdate")]
        [HttpPost]
        public HttpResponseMessage CompanyBussDevtVerificationupdate(InstitutionbussVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaInstitutionUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CompanyBussDevtVerificationupdateRaise")]
        [HttpPost]
        public HttpResponseMessage CompanyBussDevtVerificationupdateRaise(InstitutionbussVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaInstitutionUpdateRaise(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InstitutionEditSaveAsDraft")]
        [HttpPost]
        public HttpResponseMessage InstitutionEditSaveAsDraft(InstitutionbussVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaInstitutionEditSaveAsDraft(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage IndividualDetailsEdit(string sacontact_gid)
        {
            Individualverifedit values = new Individualverifedit();
            objDaSABussDevtVerification.DaIndividualDetailsEdit(sacontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        [ActionName("GetDocumentDownload")]
        [HttpGet]
        public HttpResponseMessage GetDocumentDownload()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlValuesList values = new MdlValuesList();
            objDaSABussDevtVerification.DaGetValues(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //[ActionName("GetDocumentsList")]
        //[HttpGet]
        //public HttpResponseMessage GetDocumentsList()
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    MdlsaOnboardDocument values = new MdlsaOnboardDocument();
        //    objDaSABussDevtVerification.DaGetDocumentList(getsessionvalues.employee_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}
        [ActionName("GetSBAInstitutionSummary")]
        [HttpGet]
        public HttpResponseMessage GetSBAInstitutionSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlValuesList values = new MdlValuesList();
            objDaSABussDevtVerification.DaGetSBAInstitutionSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSBAIndividualSummary")]
        [HttpGet]
        public HttpResponseMessage GetSBAIndividualSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlValuesList values = new MdlValuesList();
            objDaSABussDevtVerification.DaGetSBAIndividualSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Update

        [ActionName("SBAApprove")]
        [HttpPost]
        public HttpResponseMessage SBAApprove(institutionupdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaInstitutionApprove(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("sbiregisterSubmit")]
        [HttpPost]
        public HttpResponseMessage sbiregisterSubmit(Individual_update values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaIndividualRegister(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
        [ActionName("SaInstitutionDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage SaInstitutionDocumentUpload()
        {            
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaSABussDevtVerification.DaSaInstitutionDocumentUpload(httpRequest, documentname);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("IndividualUpdate")]
        [HttpPost]
        public HttpResponseMessage IndividualUpdate(IndividualeditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaIndividualUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualEditSaveAsDraft")]
        [HttpPost]
        public HttpResponseMessage IndividualEditSaveAsDraft(IndividualeditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaIndividualEditSaveAsDraft(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualUpdateafterraisequery")]
        [HttpPost]
        public HttpResponseMessage IndividualUpdateafterraisequery(IndividualeditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaIndividualUpdateafterraisequery(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividuaRMUpdate")]
        [HttpPost]
        public HttpResponseMessage IndividuaRMUpdate(IndividualeditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaIndividualRMUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionRMUpdate")]
        [HttpPost]
        public HttpResponseMessage InstitutionRMUpdate(InstitutioneditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaInstitutionRMUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IndividualVerify")]
        [HttpPost]
        public HttpResponseMessage IndividualVerify(IndividualeditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaIndividualVerification(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualVerifyUpdate")]
        [HttpPost]
        public HttpResponseMessage IndividualVerifyUpdate(IndividualeditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaIndividualVerifyUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionVerify")]
        [HttpPost]
        public HttpResponseMessage InstitutionVerify(InstitutioneditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaInstitutionVerification(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionVerifyUpdate")]
        [HttpPost]
        public HttpResponseMessage InstitutionVerifyUpdate(InstitutioneditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaInstitutionVerifyUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SaIndividualDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage SaIndividualDocumentUpload()
        {            
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaSABussDevtVerification.DaAddDocuments(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("InstitutionDetailsEdit")]
        [HttpGet]
        public HttpResponseMessage InstitutionDetailsEdit(string sacontactinstitution_gid)
        {
            InstitutioneditVerification values = new InstitutioneditVerification();
            objDaSABussDevtVerification.DaInstitutionDetailsEdit(sacontactinstitution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Summary

        [ActionName("GetOnboardSummary")]
        [HttpGet]
        public HttpResponseMessage GetOnboardSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSABussDevtVerification.DaGetOnboardSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOnboardCompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetOnboardCompletedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSABussDevtVerification.DaGetOnboardCompletedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionRejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionRejectedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSABussDevtVerification.DaGetInstitutionRejectedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualRejectedSummary")]
        [HttpGet]
        public HttpResponseMessage GetIndividualRejectedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSABussDevtVerification.DaGetIndividualRejectedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualOnboardingCounts")]
        [HttpGet]
        public HttpResponseMessage GetIndividualOnboardingCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSABussDevtVerification.DaGetSaonboardingCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBDVerificationCounts")]
        [HttpGet]
        public HttpResponseMessage GetBDVerificationCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSABussDevtVerification.DaGetSaonboardingBDVerificationCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBDVerificationRMCounts")]
        [HttpGet]
        public HttpResponseMessage GetBDVerificationRMCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSABussDevtVerification.DaGetSaonboardingBDVerificationRMCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBDVerificationCADCounts")]
        [HttpGet]
        public HttpResponseMessage GetBDVerificationCADCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSABussDevtVerification.DaGetSaonboardingBDVerificationCADCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SaonboardingBDVerificationRejectedCounts")]
        [HttpGet]
        public HttpResponseMessage SaonboardingBDVerificationRejectedCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSABussDevtVerification.DaSaonboardingBDVerificationRejectedCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaonboardingBDVerificationCompletedCounts")]
        [HttpGet]
        public HttpResponseMessage GetSaonboardingBDVerificationCompletedCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSABussDevtVerification.DaGetSaonboardingBDVerificationCompletedCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaVerificationpendingCounts")]
        [HttpGet]
        public HttpResponseMessage GetSaVerificationpendingCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSABussDevtVerification.DaGetSaVerificationpendingCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaVerificationapprovedCounts")]
        [HttpGet]
        public HttpResponseMessage GetSaVerificationapprovedCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSABussDevtVerification.DaGetSaVerificationapprovedCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaVerificationRejectedCounts")]
        [HttpGet]
        public HttpResponseMessage GetSaVerificationRejectedCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSABussDevtVerification.DaGetSaVerificationRejectedCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionSamCodesupdate")]
        [HttpPost]
        public HttpResponseMessage InstitutionSamCodesupdate(institutionsamcodesupdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaInstitutionSamcodesApprovalupdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualSamCodesupdate")]
        [HttpPost]
        public HttpResponseMessage IndividualSamCodesupdate(institutionsamcodesupdate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaIndividualSamcodesApprovalupdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTagFlag")]
        [HttpGet]
        public HttpResponseMessage GetTagFlag(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlValuesList values = new MdlValuesList();
            objDaSABussDevtVerification.DaGetTagFlag(sacontactinstitution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IndividualRMTaggedUpdate")]
        [HttpPost]
        public HttpResponseMessage IndividualRMTaggedUpdate(IndividualeditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaIndividualRMTaggedUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionRMTaggedUpdate")]
        [HttpPost]
        public HttpResponseMessage InstitutionRMTaggedUpdate(InstitutioneditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaInstitutionRMTaggedUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionRMTaggedLog")]
        [HttpGet]
        public HttpResponseMessage InstitutionRMTaggedLog(string sacontactinstitution_gid)
        {

            InstitutioneditVerification values = new InstitutioneditVerification();
            objDaSABussDevtVerification.DaInstitutionRMTaggedLog(sacontactinstitution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualRMTaggedLog")]
        [HttpGet]
        public HttpResponseMessage IndividualRMTaggedLog(string sacontact_gid)
        {

            IndividualeditVerification values = new IndividualeditVerification();
            objDaSABussDevtVerification.DaIndividualRMTaggedLog(sacontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionRejected")]
        [HttpPost]
        public HttpResponseMessage InstitutionRejected(InstitutioneditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaInstitutionRejected(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualRejected")]
        [HttpPost]
        public HttpResponseMessage IndividualRejected(IndividualeditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaIndividualRejected(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }       
        [ActionName("Report")]
        [HttpPost]
        public HttpResponseMessage SbaReport(SbaReport values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            //SbaReport objsbaReport = new SbaReport();
            objDaSABussDevtVerification.DaSbaReport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBothReport")]
        [HttpPost]
        public HttpResponseMessage GetBothReport(reportValues values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaGetBothReport(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetInstitutionReport")]
        [HttpPost]
        public HttpResponseMessage GetInstitutionReport(reportValues values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);            
            objDaSABussDevtVerification.DaGetInstitutionReport(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualReport")]
        [HttpPost]
        public HttpResponseMessage GetIndividualReport(reportValues values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);            
            objDaSABussDevtVerification.DaGetIndividualReport(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualReportOnly")]
        [HttpPost]
        public HttpResponseMessage GetIndividualReportOnly(reportValues values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaGetIndividualReportOnly(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionReportOnly")]
        [HttpPost]
        public HttpResponseMessage GetInstitutionReportOnly(reportValues values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaGetInstitutionReportOnly(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostBDInstitutionRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostBDInstitutionRaiseQuery(Mdlbdinstitutionraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaPostBDInstitutionRaiseQuery(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBDInstitutionRaiseQuery")]
        [HttpGet]
        public HttpResponseMessage GetBDInstitutionRaiseQuery(string sacontactinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlbdinstitutionraisequery values = new Mdlbdinstitutionraisequery();
            objDaSABussDevtVerification.DaGetBDInstitutionRaiseQuery(sacontactinstitution_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmployeelist")]
        [HttpGet]
        public HttpResponseMessage GetEmployeelist()
        {
            bdemployee values = new bdemployee();
            objDaSABussDevtVerification.DaGetEmployeelist(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostBDInstitutionresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostBDInstitutionresponsequery(Mdlbdinstitutionraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaPostBDInstitutionresponsequery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostBDIndividualRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostBDIndividualRaiseQuery(Mdlbdindividualraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaPostBDIndividualRaiseQuery(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBDIndividualRaiseQuery")]
        [HttpGet]
        public HttpResponseMessage GetBDIndividualRaiseQuery(string sacontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlbdindividualraisequery values = new Mdlbdindividualraisequery();
            objDaSABussDevtVerification.DaGetBDIndividualRaiseQuery(sacontact_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostBDIndividualresponsequery")]
        [HttpPost]
        public HttpResponseMessage PostBDIndividualresponsequery(Mdlbdindividualraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaPostBDIndividualresponsequery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPendingBDVerification")]
        [HttpGet]
        public HttpResponseMessage GetPendingBDVerification()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSABussDevtVerification.DaGetPendingBDVerification(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPendingBDVerificationInstitution")]
        [HttpGet]
        public HttpResponseMessage GetPendingBDVerificationInstitution()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSABussDevtVerification.DaGetPendingBDVerificationInstitution(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBDVerificationPendingCounts")]
        [HttpGet]
        public HttpResponseMessage GetBDVerificationPendingCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSABussDevtVerification.DaGetBDVerificationPendingCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPendingwithRMInstitutionSummary")]
        [HttpGet]
        public HttpResponseMessage GetPendingwithRMInstitutionSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSABussDevtVerification.DaGetPendingwithRMInstitutionSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPendingwithRMIndividualSummary")]
        [HttpGet]
        public HttpResponseMessage GetPendingwithRMIndividualSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSABussDevtVerification.DaGetPendingwithRMIndividualSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPendingwithCADInstitutionSummary")]
        [HttpGet]
        public HttpResponseMessage GetPendingwithCADInstitutionSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSABussDevtVerification.DaGetPendingwithCADInstitutionSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPendingwithCADIndividualSummary")]
        [HttpGet]
        public HttpResponseMessage GetPendingwithCADIndividualSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSABussDevtVerification.DaGetPendingwithCADIndividualSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualRegisterationRejected")]
        [HttpPost]
        public HttpResponseMessage IndividualRegisterationRejected(IndividualeditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaIndividualRegisterationRejected(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionRegisterationRejected")]
        [HttpPost]
        public HttpResponseMessage InstitutionRegisterationRejected(InstitutioneditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaInstitutionRegisterationRejected(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualVerificationPendingRMTaggedUpdate")]
        [HttpPost]
        public HttpResponseMessage IndividualVerificationPendingRMTaggedUpdate(IndividualeditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaIndividualVerificationPendingRMTaggedUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualPendingRMTaggedUpdate")]
        [HttpPost]
        public HttpResponseMessage IndividualPendingRMTaggedUpdate(IndividualeditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaIndividualPendingRMTaggedUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualVettingRMTaggedUpdate")]
        [HttpPost]
        public HttpResponseMessage IndividualVettingRMTaggedUpdate(IndividualeditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaIndividualVettingRMTaggedUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionVerificationPendingRMTaggedUpdate")]
        [HttpPost]
        public HttpResponseMessage InstitutionVerificationPendingRMTaggedUpdate(InstitutioneditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaInstitutionVerificationPendingRMTaggedUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionPendingRMTaggedUpdate")]
        [HttpPost]
        public HttpResponseMessage InstitutionPendingRMTaggedUpdate(InstitutioneditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaInstitutionPendingRMTaggedUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionVettingRMTaggedUpdate")]
        [HttpPost]
        public HttpResponseMessage InstitutionVettingRMTaggedUpdate(InstitutioneditVerification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSABussDevtVerification.DaInstitutionVettingRMTaggedUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IndividualPannumbervalidate")]
        [HttpGet]
        public HttpResponseMessage IndividualPannumbervalidate(string pan)
        {
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSABussDevtVerification.DaIndividualPannumbervalidate(pan, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InstitutionPannumbervalidate")]
        [HttpGet]
        public HttpResponseMessage InstitutionPannumbervalidate(string pan)
        {
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSABussDevtVerification.DaInstitutionPannumbervalidate(pan, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaApprovalInitiateSummary")]
        [HttpGet]
        public HttpResponseMessage GetSaApprovalInitiateSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSABussDevtVerification.DaGetSaApprovalInitiateSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaApproverCounts")]
        [HttpGet]
        public HttpResponseMessage GetSaApproverCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSABussDevtVerification.DaGetSaApproverCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSaApprovalInitiatedSummary")]
        [HttpGet]
        public HttpResponseMessage GetSaApprovalInitiatedSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSABussDevtVerification.DaGetSaApprovalInitiatedSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PasswordUpdate")]
        [HttpPost]
        public HttpResponseMessage PasswordUpdate(employee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            
            objDaSABussDevtVerification.DaPostPasswordUpdate(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Deactivation")]
        [HttpPost]
        public HttpResponseMessage Deactivation(employee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            objDaSABussDevtVerification.DaPostDeActivate(getsessionvalues.employee_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Activation")]
        [HttpPost]
        public HttpResponseMessage Activation(employee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            objDaSABussDevtVerification.DaPostActivate(getsessionvalues.employee_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("WebActivation")]
        [HttpPost]
        public HttpResponseMessage WebActivation(employee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            objDaSABussDevtVerification.DaPostWebActivate(getsessionvalues.employee_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("samcodesview")]
        [HttpGet]
        public HttpResponseMessage samcodesview(string sa_autogeneratedid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            employee values = new employee();
           objDaSABussDevtVerification.Dasamcodesview(values, sa_autogeneratedid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("WebDeActivation")]
        [HttpPost]
        public HttpResponseMessage WebDeActivation(employee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            objDaSABussDevtVerification.DaPostWebDeActivate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetWebAccessActiveLog")]
        [HttpGet]
        public HttpResponseMessage GetWebAccessActiveLog(string sa_autogeneratedid)
        {
            employee values = new employee();
            objDaSABussDevtVerification.DaGetWebAccessActiveLog(sa_autogeneratedid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLoginActiveLog")]
        [HttpGet]
        public HttpResponseMessage GetLoginActiveLog(string sa_autogeneratedid)
        {
            employee values = new employee();
            objDaSABussDevtVerification.DaGetLoginActiveLog(sa_autogeneratedid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionDeferredSummary")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionDeferredSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSABussDevtVerification.DaGetInstitutionDeferredSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualDeferredSummary")]
        [HttpGet]
        public HttpResponseMessage GetIndividualDeferredSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlsaOnboardSummary values = new MdlsaOnboardSummary();
            objDaSABussDevtVerification.DaGetIndividualDeferredSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("SaonboardingBDVerificationDeferredCounts")]
        [HttpGet]
        public HttpResponseMessage SaonboardingBDVerificationDeferredCounts()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            geSaOnboardingCount values = new geSaOnboardingCount();
            objDaSABussDevtVerification.DaSaonboardingBDVerificationDeferredCounts(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}