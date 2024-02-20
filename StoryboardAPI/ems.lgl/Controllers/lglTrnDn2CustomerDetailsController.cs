using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.lgl.Models;
using ems.lgl.DataAccess;
using System.Web;
using ems.utilities.Models;
using ems.utilities.Functions;

namespace ems.lgl.Controllers
{
    [RoutePrefix("api/lglTrnDn2CustomerDetails")]
    [Authorize]
    public class lglTrnDn2CustomerDetailsController :ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DalglTrnDn2CustomerDetails objDalglTrnDn2CustomerDetails = new DalglTrnDn2CustomerDetails();

        [ActionName("Getsanctionloandetails")]
        [HttpGet]
        public HttpResponseMessage Getsanctionloandetails(string urn)
        {
            sanctionloanurn values = new sanctionloanurn();
            objDalglTrnDn2CustomerDetails.DaGetsanctionloandetails(values, urn);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetloanListDetails")]
        [HttpGet]
        public HttpResponseMessage GetloanListDetails(string sanction_gid)
        {
            loanListdetailurn values = new loanListdetailurn();
            objDalglTrnDn2CustomerDetails.DaGetloanListDetails(sanction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getcustomerupdatedetails")]
        [HttpGet]
        public HttpResponseMessage Getcustomerupdatedetails(string urn)
        {
            customerediturn values = new customerediturn();
            objDalglTrnDn2CustomerDetails.DaGetEditCustomerurn(urn, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getcustomerdetails")]
        [HttpGet]
        public HttpResponseMessage Getcustomerdetails(string urn)
        {
            mdlcustomer2userdtl values = new mdlcustomer2userdtl();
            objDalglTrnDn2CustomerDetails.DaGetcustomerdetails(urn, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetGuarantordetails")]
        [HttpGet]
        public HttpResponseMessage GetGuarantordetails(string urn)
        {
            mdlcustomer2userdtl values = new mdlcustomer2userdtl();
            objDalglTrnDn2CustomerDetails.DaGetGuarantordetails(urn, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetGuarantorlist")]
        [HttpGet]
        public HttpResponseMessage GetGuarantorlist(string urn)
        {
            mdlcustomer2userdtl values = new mdlcustomer2userdtl();
            objDalglTrnDn2CustomerDetails.DaGetGuarantorlist(urn, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostDN1Sanctiondtl")]
        [HttpPost]
        public HttpResponseMessage postDN1sanctiondtl(DNsanctiondtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDalglTrnDn2CustomerDetails.DaPostDN1sanctiondtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DN1Content")]
        [HttpGet]
        public HttpResponseMessage getTemplateDN1Content(string urn)
        {
            template_list objTemplateContent = new template_list();
            objDalglTrnDn2CustomerDetails.DaGetTemplateDN1Content(objTemplateContent, urn);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("template_list")]
        [HttpGet]
        public HttpResponseMessage gettemplate_list()
        {
            mdltemplate objTemplateContent = new mdltemplate();
            objDalglTrnDn2CustomerDetails.DaGettemplate_list(objTemplateContent);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("DN2template_list")]
        [HttpGet]
        public HttpResponseMessage getDN2template_list()
        {
            mdltemplate objTemplateContent = new mdltemplate();
            objDalglTrnDn2CustomerDetails.DaGetDN2template_list(objTemplateContent);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("DN3template_list")]
        [HttpGet]
        public HttpResponseMessage getDN3template_list()
        {
            mdltemplate objTemplateContent = new mdltemplate();
            objDalglTrnDn2CustomerDetails.DaGetDN3template_list(objTemplateContent);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("DN1Cancel")]
        [HttpGet]
        public HttpResponseMessage getDN1Cancel(string urn)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            template_list objTemplateContent = new template_list();
            objDalglTrnDn2CustomerDetails.DaGetDN1Cancel(objTemplateContent, urn, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("DN2Cancel")]
        [HttpGet]
        public HttpResponseMessage getDN2Cancel(string urn)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            template_list objTemplateContent = new template_list();
            objDalglTrnDn2CustomerDetails.DaGetDN2Cancel(objTemplateContent, urn, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("DN3Cancel")]
        [HttpGet]
        public HttpResponseMessage getDN3Cancel(string urn)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            template_list objTemplateContent = new template_list();
            objDalglTrnDn2CustomerDetails.DaGetDN3Cancel(objTemplateContent, urn, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("SanctionDN2Cancel")]
        [HttpGet]
        public HttpResponseMessage getSanctionDN2Cancel(string urn)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            template_list objTemplateContent = new template_list();
            objDalglTrnDn2CustomerDetails.DaGetSanctionDN2Cancel(objTemplateContent, urn, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("SanctionDN3Cancel")]
        [HttpGet]
        public HttpResponseMessage getSanctionDN3Cancel(string urn)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            template_list objTemplateContent = new template_list();
            objDalglTrnDn2CustomerDetails.DaGetSanctionDN3Cancel(objTemplateContent, urn, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("PostDN2Sanctiondtl")]
        [HttpPost]
        public HttpResponseMessage postDN2sanctiondtl(DNsanctiondtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDalglTrnDn2CustomerDetails.DaPostDN2sanctiondtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DN2Content")]
        [HttpGet]
        public HttpResponseMessage getTemplateDN2Content(string urn)
        {
            template_list objTemplateContent = new template_list();
            objDalglTrnDn2CustomerDetails.DaGetTemplateDN2Content(objTemplateContent, urn);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("PostDN3Sanctiondtl")]
        [HttpPost]
        public HttpResponseMessage postDN3sanctiondtl(DNsanctiondtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDalglTrnDn2CustomerDetails.DaPostDN3sanctiondtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DN3Content")]
        [HttpGet]
        public HttpResponseMessage getTemplateDN3Content(string urn)
        {
            template_list objTemplateContent = new template_list();
            objDalglTrnDn2CustomerDetails.DaGetTemplateDN3Content(objTemplateContent, urn);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }

        //CBO Format
        [ActionName("GetCBOTemplate_List")]
        [HttpGet]
        public HttpResponseMessage getCBOtemplate_list()
        {
            mdltemplate objTemplateContent = new mdltemplate();
            objDalglTrnDn2CustomerDetails.DaGetCBOTemplate_List(objTemplateContent);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("GetCBODN2template_list")]
        [HttpGet]
        public HttpResponseMessage getCBODN2template_list()
        {
            mdltemplate objTemplateContent = new mdltemplate();
            objDalglTrnDn2CustomerDetails.DaGetCBODN2template_list(objTemplateContent);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("GetCBODN3template_list")]
        [HttpGet]
        public HttpResponseMessage getCBODN3template_list()
        {
            mdltemplate objTemplateContent = new mdltemplate();
            objDalglTrnDn2CustomerDetails.DaGetCBODN3template_list(objTemplateContent);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("PostCBODN1Sanctiondtl")]
        [HttpPost]
        public HttpResponseMessage postCBODN1sanctiondtl(DNsanctiondtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDalglTrnDn2CustomerDetails.DaPostCBODN1sanctiondtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCBODN2Sanctiondtl")]
        [HttpPost]
        public HttpResponseMessage postCBODN2sanctiondtl(DNsanctiondtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDalglTrnDn2CustomerDetails.DaPostCBODN2sanctiondtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCBODN3Sanctiondtl")]
        [HttpPost]
        public HttpResponseMessage postCBODN3sanctiondtl(DNsanctiondtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDalglTrnDn2CustomerDetails.DaPostCBODN3sanctiondtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CBODN1Content")]
        [HttpGet]
        public HttpResponseMessage getCBOTemplateDN1Content(string urn)
        {
            template_list objTemplateContent = new template_list();
            objDalglTrnDn2CustomerDetails.DaGetCBOTemplateDN1Content(objTemplateContent, urn);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("CBODN2Content")]
        [HttpGet]
        public HttpResponseMessage getCBO2TemplateDNContent(string urn)
        {
            template_list objTemplateContent = new template_list();
            objDalglTrnDn2CustomerDetails.DaGetCBO2TemplateDNContent(objTemplateContent, urn);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("CBODN3Content")]
        [HttpGet]
        public HttpResponseMessage getCBO3TemplateDN1Content(string urn)
        {
            template_list objTemplateContent = new template_list();
            objDalglTrnDn2CustomerDetails.DaGetCBO3TemplateDNContent(objTemplateContent, urn);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("CBODN1generate")]
        [HttpPost]
        public HttpResponseMessage postCBODN1generate(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDalglTrnDn2CustomerDetails.DaPostCBODN1Generate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CBODN2generate")]
        [HttpPost]
        public HttpResponseMessage postCBODN2generate(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDalglTrnDn2CustomerDetails.DaPostCBODN2Generate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CBODN3generate")]
        [HttpPost]
        public HttpResponseMessage postCBODN3generate(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDalglTrnDn2CustomerDetails.DaPostCBODN3Generate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CBODN1Cancel")]
        [HttpGet]
        public HttpResponseMessage getCBODN1Cancel(string urn)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            template_list objTemplateContent = new template_list();
            objDalglTrnDn2CustomerDetails.DaGetCBODN1Cancel(objTemplateContent, urn, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("CBODN2Cancel")]
        [HttpGet]
        public HttpResponseMessage getCBODN2Cancel(string urn)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            template_list objTemplateContent = new template_list();
            objDalglTrnDn2CustomerDetails.DaGetCBODN2Cancel(objTemplateContent, urn, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("CBODN3Cancel")]
        [HttpGet]
        public HttpResponseMessage getCBODN3Cancel(string urn)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            template_list objTemplateContent = new template_list();
            objDalglTrnDn2CustomerDetails.DaGetCBODN3Cancel(objTemplateContent, urn, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("SanctionCBODN2Cancel")]
        [HttpGet]
        public HttpResponseMessage getSanctionCBODN2Cancel(string urn)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            template_list objTemplateContent = new template_list();
            objDalglTrnDn2CustomerDetails.DaGetSanctionCBODN2Cancel(objTemplateContent, urn, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("SanctionCBODN3Cancel")]
        [HttpGet]
        public HttpResponseMessage getSanctionCBODN3Cancel(string urn)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            template_list objTemplateContent = new template_list();
            objDalglTrnDn2CustomerDetails.DaGetSanctionCBODN3Cancel(objTemplateContent, urn, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("GetSkippedHistory")]
        [HttpGet]
        public HttpResponseMessage getSkippedHistory(string urn)
        {
            MdlDNSkipHistory objDNsttaus = new MdlDNSkipHistory();
            objDalglTrnDn2CustomerDetails.DaGetSkippedHistory(objDNsttaus, urn);
            return Request.CreateResponse(HttpStatusCode.OK, objDNsttaus);
        }
        [ActionName("PostDN1AnnexureUpload")]
        [HttpPost]
        public HttpResponseMessage postDN1AnnexureUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDalglTrnDn2CustomerDetails.DaPostDN1AnnexureUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("PostDN2AnnexureUpload")]
        [HttpPost]
        public HttpResponseMessage postDN2AnnexureUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDalglTrnDn2CustomerDetails.DaPostDN2AnnexureUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("PostDN3AnnexureUpload")]
        [HttpPost]
        public HttpResponseMessage postDN3AnnexureUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDalglTrnDn2CustomerDetails.DaPostDN3AnnexureUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("GetDNGenerated_History")]
        [HttpGet]
        public HttpResponseMessage GetDNGenerated_History(string urn)
        {
            MdlLglReport objDNstatus = new MdlLglReport();
            objDalglTrnDn2CustomerDetails.DaGetDNGenerated_History(objDNstatus, urn);
            return Request.CreateResponse(HttpStatusCode.OK, objDNstatus);
        }
      
    }
}