using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.lp.Models;
using ems.lp.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Web;

namespace ems.lp.Controllers
{
    [RoutePrefix("api/documentCompliance")]
    [AllowAnonymous]
    public class DocumentComplianceController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaDocumentCompliance objDaDocumentCompliancce = new DaDocumentCompliance();

        [ActionName("Compliancemanagementsummary")]
        [HttpGet]
        public HttpResponseMessage getCompliancemanagementsummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlRequestcompliance objrequestCompliance = new mdlRequestcompliance();
            objDaDocumentCompliancce.GetCompliancesummary(objrequestCompliance, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objrequestCompliance);
        }

        [ActionName("compliancemanagement360")]
        [HttpGet]
        public HttpResponseMessage getcompliancemanagementsummary(string requestcompliance_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlRequestcompliance objrequestCompliance = new mdlRequestcompliance();
            objDaDocumentCompliancce.GetComplianceManagementSummary(getsessionvalues.user_gid,requestcompliance_gid, objrequestCompliance);
            return Request.CreateResponse(HttpStatusCode.OK, objrequestCompliance);
        }

        [ActionName("uploadCorrectedDoc")]
        [HttpPost]
        public HttpResponseMessage uploadcorrecteddocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaDocumentCompliancce.Uploadcorrecteddocument(httpRequest, documentname, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("uploadremarrks")]
        [HttpPost]
        public HttpResponseMessage uploadcorrectedremarks(uploaddocument objfilename)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDocumentCompliancce.Uploadcorrectedremarks(objfilename, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }
        [ActionName("uploadlawyerCorrected_doc")]
        [HttpPost]
        public HttpResponseMessage uploadlawyerCorrected_doc()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaDocumentCompliancce.DauploadlawyerCorrected_doc(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("deletecorrecteddo_upload")]
        [HttpGet]
        public HttpResponseMessage deletecorrecteddo_upload(string lawyerdocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            uploaddocument values = new uploaddocument();
            objDaDocumentCompliancce.Dadeletecorrecteddo_upload(lawyerdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("submitComplianceCorrected_doc")]
        [HttpPost]
        public HttpResponseMessage submitComplianceCorrected_doc(uploaddocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDocumentCompliancce.DasubmitComplianceCorrected_doc(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("getcorrecteddocument")]
        [HttpGet]
        public HttpResponseMessage getcorrecteddocument(string requestcompliance2lawyerdtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
           uploaddocument values = new uploaddocument();
            objDaDocumentCompliancce.Dagetcorrecteddocument(requestcompliance2lawyerdtl_gid,getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("getuploaddoc2lawyer")]
        [HttpGet]
        public HttpResponseMessage getuploaddoc2lawyer(string requestcompliance2lawyerdtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTaggedInfo objrequestCompliance = new MdlTaggedInfo();
            objDaDocumentCompliancce.Dagetuploaddoc2lawyer(getsessionvalues.user_gid, requestcompliance2lawyerdtl_gid, objrequestCompliance);
            return Request.CreateResponse(HttpStatusCode.OK, objrequestCompliance);
        }
        [ActionName("updatestatus")]
        [HttpPost]
        public HttpResponseMessage updatestatus(mdlRequestcompliance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaDocumentCompliancce.Daupdatestatus(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("LegalDtls")]
        [HttpGet]
        public HttpResponseMessage DaGetLegalDtls(string requestcompliance2lawyerdtl_gid)
        {
            LegalDtls values = new LegalDtls();
            objDaDocumentCompliancce.DaGetLegalDtls(requestcompliance2lawyerdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
