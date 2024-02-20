using System;
using System.Collections.Generic;
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
    [RoutePrefix("api/requestCompliance")]
    [Authorize]
    public class requestComplianceController : ApiController
    {

        MdlRequestcompliance objrequestCompliance = new MdlRequestcompliance();
        DaRequestcompliance objDaRequestcompliance = new DaRequestcompliance();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("requestcompliance")]
        [HttpPost]
        public HttpResponseMessage postrequestCompliance(MdlRequestcompliance objrequestCompliance)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRequestcompliance.DaPostRequestCompliance(getsessionvalues.employee_gid,  objrequestCompliance);
            return Request.CreateResponse(HttpStatusCode.OK, objrequestCompliance);
        }
        [ActionName("requestCompliancesummary")]
        [HttpGet]
        public HttpResponseMessage getrequestCompliancesummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRequestcompliance objrequestCompliance = new MdlRequestcompliance();
            objDaRequestcompliance.DagetrequestCompliancesummary(objrequestCompliance, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objrequestCompliance);
        }
        [ActionName("Compliancemanagementsummary")]
        [HttpGet]
        public HttpResponseMessage getCompliancemanagementsummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRequestcompliance objrequestCompliance = new MdlRequestcompliance();
            objDaRequestcompliance.DaGetComplianceSummary(objrequestCompliance, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objrequestCompliance);
        }
        [ActionName("compliancemanagement360")]
        [HttpGet]
        public HttpResponseMessage getcompliancemanagementsummary(string requestcompliance_gid)
        {
            objDaRequestcompliance.DaGetComplianceManagementSummary(requestcompliance_gid, objrequestCompliance);
            return Request.CreateResponse(HttpStatusCode.OK, objrequestCompliance);
        }
        [ActionName("requestcomplianceview")]
        [HttpGet]
        public HttpResponseMessage getrequestcomplianceview(string requestcompliance_gid)
        {
            objDaRequestcompliance.DaGetrequestComplianceView(requestcompliance_gid, objrequestCompliance);
            return Request.CreateResponse(HttpStatusCode.OK, objrequestCompliance);
        }

        [ActionName("lawyerList")]
        [HttpGet]
        public HttpResponseMessage getassignLawyer()
        {
            taggedcase objtaggedcase = new taggedcase();
            objDaRequestcompliance.DaGetAssignLawyer(objtaggedcase);
            return Request.CreateResponse(HttpStatusCode.OK, objtaggedcase);
        }

        [ActionName("tmpseekdocumentclear")]
        [HttpGet]
        public HttpResponseMessage getseekdocumentclear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            excel objresult = new excel();
            objDaRequestcompliance.DaGetSeekDocumentClear(getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }


        [ActionName("assignLawyer")]
        [HttpPost]
        public HttpResponseMessage getassignLawyer_compliance(assignlawyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            assignlawyer objvalues = new assignlawyer();
            objDaRequestcompliance.DaGetAssignCompliance(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("Uploaddocument")]
        [HttpPost]
        public HttpResponseMessage UploadEnrollcertificate()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaRequestcompliance.DaUploaddocument(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("seekLawyerUpload")]
        [HttpPost]
        public HttpResponseMessage postseekLawyerUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaRequestcompliance.DaPostSeekLawyerUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("seekLawyerUploadcancel")]
        [HttpGet]
        public HttpResponseMessage getseekLawyerUploadcancel(string tmpseek_documentgid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            uploaddocument values = new uploaddocument();
            objDaRequestcompliance.DaGetSeekLawyerUploadCancel(tmpseek_documentgid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("additionaldocupload")]
        [HttpPost]
        public HttpResponseMessage postadditionaldocupload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaRequestcompliance.DaPostAdditionalDocUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("Edituploaddocument")]
        [HttpPost]
        public HttpResponseMessage EdituploadEnrollcertificate()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaRequestcompliance.DaEdituploaddocument(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("documentdelete")]
        [HttpGet]
        public HttpResponseMessage getdocumentname(string uploaddocument_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            resultvalue objdocumentcancel = new resultvalue();
            objDaRequestcompliance.DaGetDocumentCancel(uploaddocument_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        [ActionName("Getrequestcompliance")]
        [HttpGet]
        public HttpResponseMessage getlawyerdetails(string requestcompliance_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRequestcompliance values = new MdlRequestcompliance();
            objDaRequestcompliance.DaGetRequestCompliance(getsessionvalues.user_gid, requestcompliance_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("updateRequestcompliance")]
        [HttpPost]
        public HttpResponseMessage updaterequestcompliance(MdlRequestcompliance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRequestcompliance.DaUpdateRequestCompliance(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("requestcompliancedelete")]
        [HttpGet]
        public HttpResponseMessage getrequestresponsedelete(string requestcompliance_gid)
        {
            MdlRequestcompliance values = new MdlRequestcompliance();
             objDaRequestcompliance.DaGetRequestComplianceDelete(requestcompliance_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("sendqueries")]
        [HttpPost]
        public HttpResponseMessage postsendqueries(querydetails values)
        {
            querydetails objquerydetails = new querydetails();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRequestcompliance.DaPostSendQuery(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("querieslist")]
        [HttpGet]
        public HttpResponseMessage getqueriesdetails(string requestcompliance_gid)
        {
            querylist objquerydetails = new querylist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRequestcompliance.DaGetQueryDetails(getsessionvalues.employee_gid, requestcompliance_gid, objquerydetails);
            return Request.CreateResponse(HttpStatusCode.OK, objquerydetails);
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
            objDaRequestcompliance.DaUploadCorrectedDocument(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("uploadremarrks")]
        [HttpPost]
        public HttpResponseMessage uploadcorrectedremarks(uploaddocument objfilename)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
             objDaRequestcompliance.DaUploadCorrectedRemarks(objfilename, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }
      
        [ActionName("correcteddoc_delete")]
        [HttpGet]
        public HttpResponseMessage getcorrecteddoc_delete(string uploaddocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            resultvalue objdocumentcancel = new resultvalue();
            objDaRequestcompliance.DaGetcorrecteddoc_delete(getsessionvalues.employee_gid, uploaddocument_gid,objdocumentcancel);         
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("mandatory_check")]
        [HttpGet]
        public HttpResponseMessage getmandatory_check()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            resultvalue objdocumentcancel = new resultvalue();
            objDaRequestcompliance.DaGetmandatory_check(getsessionvalues.employee_gid,  objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("editmandatory_check")]
        [HttpGet]
        public HttpResponseMessage geteditmandatory_check(string requestcompliance_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            resultvalue objdocumentcancel = new resultvalue();
            objDaRequestcompliance.DaGeteditmandatory_check(getsessionvalues.employee_gid,objdocumentcancel, requestcompliance_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("postrequesttype")]
        [HttpPost]
        public HttpResponseMessage postrequesttype(mdlrequesttype objrequestCompliance)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRequestcompliance.Dapostrequesttype(getsessionvalues.employee_gid, objrequestCompliance);
            return Request.CreateResponse(HttpStatusCode.OK, objrequestCompliance);
        }
        [ActionName("getrequesttype")]
        [HttpGet]
        public HttpResponseMessage getrequesttype()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlrequesttype objrequestCompliance = new mdlrequesttype();
            objDaRequestcompliance.Dagetrequesttype(objrequestCompliance, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objrequestCompliance);
        }
        [ActionName("updaterequesttype")]
        [HttpPost]
        public HttpResponseMessage updaterequesttype(mdlrequesttype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRequestcompliance.Daupdaterequesttype(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("editrequesttype")]
        [HttpGet]
        public HttpResponseMessage editrequesttype(string requesttype_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlrequesttype values = new mdlrequesttype();
            objDaRequestcompliance.Daeditrequesttype(getsessionvalues.employee_gid, requesttype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("deleterequesttype")]
        [HttpGet]
        public HttpResponseMessage deleterequesttype(string requesttype_gid)
        {
           
            mdlrequesttype values = new mdlrequesttype();
            objDaRequestcompliance.Dadeleterequesttype(requesttype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("updatestatus")]
        [HttpPost]
        public HttpResponseMessage updatestatus(MdlRequestcompliance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRequestcompliance.Daupdatestatus(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UploadComplianceCorrected_doc")]
        [HttpPost]
        public HttpResponseMessage PostUploadComplianceCorrected_doc()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            Managecomplianuploaddoc documentname = new Managecomplianuploaddoc();
            objDaRequestcompliance.DaUploadComplianceCorrected_doc(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("submitComplianceCorrected_doc")]
        [HttpPost]
        public HttpResponseMessage submitComplianceCorrected_doc(MdlRequestcompliance values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRequestcompliance.DasubmitComplianceCorrected_doc(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("deletecorrecteddo_upload")]
        [HttpGet]
        public HttpResponseMessage deletecorrecteddo_upload(string document_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRequestcompliance values = new MdlRequestcompliance();
            objDaRequestcompliance.Dadeletecorrecteddo_upload(document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("getcorrecteddocument")]
        [HttpGet]
        public HttpResponseMessage getcorrecteddocument(string requestcompliance_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Managecomplianuploaddoc values = new Managecomplianuploaddoc();
            objDaRequestcompliance.Dagetcorrecteddocument(requestcompliance_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("getrequesttype2compliance")]
        [HttpGet]
        public HttpResponseMessage getrequesttype2compliance()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlrequesttype objrequestCompliance = new mdlrequesttype();
            objDaRequestcompliance.Dagetrequesttype2compliance(objrequestCompliance, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objrequestCompliance);
        }
        [ActionName("Gettaggedlist")]
        [HttpGet]
        public HttpResponseMessage Gettaggedlist(string requestcompliance_gid)
        {

            MdlTaggedInfo objrequestCompliance = new MdlTaggedInfo();
            objDaRequestcompliance.DaGettaggedlist(objrequestCompliance, requestcompliance_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objrequestCompliance);
        }
        [ActionName("Viewuploaddoc_lawyer")]
        [HttpGet]
        public HttpResponseMessage Viewuploaddoc_lawyer(string requestcompliance2lawyerdtl_gid)
        {
            MdlTaggedInfo objMdldetails = new MdlTaggedInfo();
            objDaRequestcompliance.DaViewuploaddoc_lawyer(requestcompliance2lawyerdtl_gid, objMdldetails);
            return Request.CreateResponse(HttpStatusCode.OK, objMdldetails);
        }
        [ActionName("GetuploadbyLawyer")]
        [HttpGet]
        public HttpResponseMessage GetuploadbyLawyer(string requestcompliance2lawyerdtl_gid)
        {
            MdlTaggedInfo objMdldetails = new MdlTaggedInfo();
            objDaRequestcompliance.DaGetuploadbyLawyer(requestcompliance2lawyerdtl_gid, objMdldetails);
            return Request.CreateResponse(HttpStatusCode.OK, objMdldetails);
        }

        [ActionName("LawyerSummary")]
        [HttpGet]
        public HttpResponseMessage LawyerSummary(string requestcompliance_gid)
        {
            MdlLawyerSummaryList objMdldetails = new MdlLawyerSummaryList();
            objDaRequestcompliance.DaGetLawyerSummary(requestcompliance_gid, objMdldetails);
            return Request.CreateResponse(HttpStatusCode.OK, objMdldetails);
        }

        [ActionName("LawyerConversation")]
        [HttpPost]
        public HttpResponseMessage LawyerConversation(MdlLawyerConversation values)
        {
            result objResult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult = objDaRequestcompliance.DaPostLawyerConversation(values, getsessionvalues.user_gid,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("GetLawyerConversation")]
        [HttpPost]
        public HttpResponseMessage GetLawyerConversation(ComplainceValue values)
        {
            MdlConversationSummaryList objResult = new MdlConversationSummaryList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRequestcompliance.DaGetLawyerConversation(values, objResult,getsessionvalues .user_gid );
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("MsgViewed")]
        [HttpPost]
        public HttpResponseMessage MsgViewed(ComplainceValue values)
        {
            objDaRequestcompliance.DaMsgViewed(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("LawyerGroupDtls")]
        [HttpGet]
        public HttpResponseMessage DaGetLawyerGroupDtls(string requestcompliance_gid)
        {
            MdlLawyerGroupDtls values = new MdlLawyerGroupDtls();
            objDaRequestcompliance.DaGetLawyerGroupDtls(requestcompliance_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Geteditrequesttype")]
        [HttpGet]
        public HttpResponseMessage Geteditrequesttype(string requesttype_gid)
        {
            mdlrequesttype objdocumentcancel = new mdlrequesttype();
            objDaRequestcompliance.DaGeteditrequesttype(requesttype_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("LawyerGroupConversation")]
        [HttpPost]
        public HttpResponseMessage LawyergroupConversation(MdlLawyerConversation values)
        {
            result objResult = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult = objDaRequestcompliance.DaPostLawyerGroupConversation(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        //----Web Service----//
        [ActionName("GetlawyerStatus")]
        [HttpGet]
        public HttpResponseMessage GetlawyerStatus(string requestcompliance_gid)
        {
            MdlLawyerSummaryList values = new MdlLawyerSummaryList();
            objDaRequestcompliance.DaGetlawyerStatus(requestcompliance_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //------------Upload additional Document to lawyer-----------//
        [ActionName("UploadAdditionalDocument")]
        [HttpPost]
        public HttpResponseMessage UploadAdditionalDocument(assignlawyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            assignlawyer objvalues = new assignlawyer();
            objDaRequestcompliance.DaUploadAdditionalDocument(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("tempdelete")]
        [HttpGet]
        public HttpResponseMessage gettempdelete()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            resultvalue objdocumentcancel = new resultvalue();
            objDaRequestcompliance.DaGetTempDelete(getsessionvalues.user_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        [ActionName("reqtempdelete")]
        [HttpGet]
        public HttpResponseMessage getreqtempdelete()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            resultvalue objdocumentcancel = new resultvalue();
            objDaRequestcompliance.DaGetTempDel(getsessionvalues.user_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }



    }
}
