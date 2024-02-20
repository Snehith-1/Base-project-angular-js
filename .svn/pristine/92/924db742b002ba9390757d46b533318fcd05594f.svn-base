using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.idas.Models;
using ems.idas.DataAccess;

namespace StoryboardAPI.Controllers.ems.idas
{
    [RoutePrefix("api/IdasMstSanction")]
    [Authorize]

    public class IdasMstSanctionController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaSanctionMst objDaSanction = new DaSanctionMst();

        [ActionName("CreateSanction")]
        [HttpPost]
        public HttpResponseMessage PostSanctionDetails(sanctiondetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.DaPostSanctionDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionDtls")]
        [HttpGet]
        public HttpResponseMessage GetSanctionDtlList()
        {
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaSanction.DaGetSanctionDtlList(objsanctiondetailsList);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }
        //pending Sanction Info
        [ActionName("PendingSanctionDtl")]
        [HttpGet]
        public HttpResponseMessage GetPendingSanctionDtlList()
        {
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaSanction.DaGetPendingSanctionDtlList(objsanctiondetailsList);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }
        //pending Sanction Info
        [ActionName("CompletedSanctionDtl")]
        [HttpGet]
        public HttpResponseMessage GetCompletedSanctionDtlList()
        {
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaSanction.DaGetCompletedSanctionDtlList(objsanctiondetailsList);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }
        [ActionName ("UpdateSanction")]
        [HttpPost ]
        public HttpResponseMessage PostUpdateSAnction(sanctiondetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.DaPostSanctionUpdate(values, getsessionvalues.user_gid,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostSanctionDetails1")]
        [HttpPost]
        public HttpResponseMessage PostSanctionDetails1(sanctiondetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.DaPostSanctionDetails1(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostSanctionDetails2")]
        [HttpPost]
        public HttpResponseMessage PostSanctionDetails2(sanctiondetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.DaPostSanctionDetails2(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostSanctionDetails3")]
        [HttpPost]
        public HttpResponseMessage PostSanctionDetails3(sanctiondetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.DaPostSanctionDetails3(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName ("SanctionDtlsEdit")]
        [HttpGet]
        public HttpResponseMessage DaGetSanctionEdit(string sanction_gid)
        {
            sanctiondetails values = new sanctiondetails();
            objDaSanction.DaGetSanctionEdit(sanction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK ,values);
        }

        [ActionName("postexcelupload")]
        [HttpPost]
        public HttpResponseMessage PostExcelUpload()
        {

            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objsanctiondetails = new result();
            objDaSanction.DaPostExcelUpload(httpRequest, getsessionvalues.employee_gid, objsanctiondetails);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetails);
        }
        [ActionName("postCAMddocument")]
        [HttpPost]
        public HttpResponseMessage PostCAMddocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDaSanction.DaPostCAMddocument(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("postMOMddocument")]
        [HttpPost]
        public HttpResponseMessage PostMOMddocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDaSanction.DaPostMOMddocument(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("postgeneraldocument")]
        [HttpPost]
        public HttpResponseMessage Postgeneraldocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDaSanction.DaPostgeneraldocument(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("Editgeneraldocument")]
        [HttpPost]
        public HttpResponseMessage editgeneraldocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDaSanction.DaEditgeneraldocument(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        
        [ActionName("camdoc_delete")]
        [HttpGet]
        public HttpResponseMessage getcamdoc_delete(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocumentcancel = new UploadDocumentname();
            objDaSanction.DaGetcamdoc_delete(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("camdoc_delete_add")]
        [HttpGet]
        public HttpResponseMessage getcamdoc_delete_add(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocumentcancel = new UploadDocumentname();
            objDaSanction.DaGetcamdoc_delete_add(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("documentdelete")]
        [HttpGet]
        public HttpResponseMessage getdocumentname(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocumentcancel = new UploadDocumentname();
            objDaSanction.DaGetDocumentCancel(document_gid, objdocumentcancel,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("documentdelete_add")]
        [HttpGet]
        public HttpResponseMessage getdocumentname_add(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocumentcancel = new UploadDocumentname();
            objDaSanction.DaGetDocumentCancel_add(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("momdoc_delete")]
        [HttpGet]
        public HttpResponseMessage getmomdoc_delete(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocumentcancel = new UploadDocumentname();
            objDaSanction.DaGetmomdoc_delete(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("momdoc_delete_add")]
        [HttpGet]
        public HttpResponseMessage getmomdoc_delete_add(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocumentcancel = new UploadDocumentname();
            objDaSanction.DaGetmomdoc_delete_add(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        [ActionName("tempdelete")]
        [HttpGet]
        public HttpResponseMessage gettempcamdelete()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objdocumentcancel = new result();
            objDaSanction.DaGetTempDelete(getsessionvalues.employee_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        

        [ActionName("Editmomdoc")]
        [HttpGet]
        public HttpResponseMessage Editmomdoc(string sanction_gid)
        {         
            result objdocumentcancel = new result();
            objDaSanction.DaEditmomdoc(sanction_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("Editcamdoc")]
        [HttpGet]
        public HttpResponseMessage Editcamdoc(string sanction_gid)

        {
            result objdocumentcancel = new result();
            objDaSanction.DaEditcamdoc(sanction_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("Getgeneraldocment")]
        [HttpGet]
        public HttpResponseMessage Getgeneraldocment(string sanction_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocument = new UploadDocumentname();
            objDaSanction.DaGetgeneraldocment(sanction_gid, objdocument,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("mandatoryfile_check")]
        [HttpGet]
        public HttpResponseMessage getmandatoryfile_check(string esdeclaration_status)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objMdlEmployee = new result();
            objDaSanction.Dagetmandatoryfile_check(esdeclaration_status,objMdlEmployee, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }
        [ActionName("editmandatory_check")]
        [HttpGet]
        public HttpResponseMessage geteditmandatory_check(string sanction_gid, string esdeclaration_status)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocument = new UploadDocumentname();
            objDaSanction.DaGeteditmandatory_check(objdocument,sanction_gid, esdeclaration_status, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("postBALdocument")]
        [HttpPost]
        public HttpResponseMessage postBALdocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDaSanction.DapostBALdocument(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("GetBuyerinfo")]
        [HttpGet]
        public HttpResponseMessage GetBuyerinfo()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdladdbuyer objdocument = new Mdladdbuyer();
            objDaSanction.DaGetBuyerinfo(objdocument, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("GetBuyerinfoEdit")]
        [HttpGet]
        public HttpResponseMessage GetBuyerinfoEdit(string sanction_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdladdbuyer objdocument = new Mdladdbuyer();
            objDaSanction.DaGetBuyerinfoEdit(objdocument, getsessionvalues.employee_gid, sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("PostBuyerInfo")]
        [HttpPost]
        public HttpResponseMessage PostBuyerInfo(Mdladdbuyer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.DaPostBuyerInfo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("buyerdelete")]
        [HttpGet]
        public HttpResponseMessage getbuyerdelete(string buyer_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocumentcancel = new UploadDocumentname();
            objDaSanction.DaGetbuyerCancel(buyer_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("buyerdelete_add")]
        [HttpGet]
        public HttpResponseMessage getbuyerdelete_add(string buyer_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocumentcancel = new UploadDocumentname();
            objDaSanction.DaGetbuyerCancel_add(buyer_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("EditCAMddocument")]
        [HttpPost]
        public HttpResponseMessage EditCAMddocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDaSanction.DaEditCAMddocument(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("EditMOMddocument")]
        [HttpPost]
        public HttpResponseMessage EditMOMddocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDaSanction.DaEditMOMddocument(httpRequest, documentname, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("Getcamdocment")]
        [HttpGet]
        public HttpResponseMessage Getcamdocment(string sanction_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocument = new UploadDocumentname();
            objDaSanction.DaGetcamdocment(sanction_gid, objdocument, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("Getmomdocment")]
        [HttpGet]
        public HttpResponseMessage Getmomdocment(string sanction_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocument = new UploadDocumentname();
            objDaSanction.DaGetmomdocment(sanction_gid, objdocument, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("postccmembers")]
        [HttpPost]
        public HttpResponseMessage postccmembers(mdlccmembers values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.Dapostccmembers(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("updateccmembers")]
        [HttpPost]
        public HttpResponseMessage updateccmembers(mdlccmembers values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.Daupdateccmembers(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCCmembers")]
        [HttpGet]
        public HttpResponseMessage GetCCmembers()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlccmembers objdocument = new mdlccmembers();
            objDaSanction.DaGetCCmembers( getsessionvalues.employee_gid, objdocument);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("deleteccmember")]
        [HttpGet]
        public HttpResponseMessage deleteccmember(string ccmemberlist_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlccmembers objdocument = new mdlccmembers();
            objDaSanction.Dadeleteccmember(ccmemberlist_gid, objdocument, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("deleteccmember_add")]
        [HttpGet]
        public HttpResponseMessage deleteccmember_add(string ccmemberlist_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlccmembers objdocument = new mdlccmembers();
            objDaSanction.Dadeleteccmember_add(ccmemberlist_gid, objdocument, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("Editccmember")]
        [HttpGet]
        public HttpResponseMessage Editccmember(string sanction_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlccmembers objdocument = new mdlccmembers();
            objDaSanction.DaEditccmember(getsessionvalues.employee_gid, sanction_gid,objdocument);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("postloanfacilitytype")]
        [HttpPost]
        public HttpResponseMessage postloanfacilitytype(Mdlloanfacility_type values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.Dapostloanfacilitytype(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("updateloanfacilitytype")]
        [HttpPost]
        public HttpResponseMessage updateloanfacilitytype(Mdlloanfacility_type values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.Daupdateloanfacilitytype(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getloanfacilitytype")]
        [HttpGet]
        public HttpResponseMessage Getloanfacilitytype()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlloanfacility_type objdocument = new Mdlloanfacility_type();
            objDaSanction.DaGetloanfacilitytype(getsessionvalues.employee_gid, objdocument);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("deleteloanfacilitytype")]
        [HttpGet]
        public HttpResponseMessage deleteloanfacilitytype(string sanction2loanfacilitytype_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlloanfacility_type objdocument = new Mdlloanfacility_type();
            objDaSanction.Dadeleteloanfacilitytype(sanction2loanfacilitytype_gid, objdocument, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("deleteloanfacilitytype_add")]
        [HttpGet]
        public HttpResponseMessage deleteloanfacilitytype_add(string sanction2loanfacilitytype_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlloanfacility_type objdocument = new Mdlloanfacility_type();
            objDaSanction.Dadeleteloanfacilitytype_add(sanction2loanfacilitytype_gid, objdocument, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("EditLoanfacilitytype")]
        [HttpGet]
        public HttpResponseMessage EditLoanfacilitytype(string sanction_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlloanfacility_type objdocument = new Mdlloanfacility_type();
            objDaSanction.DaEditLoanfacilitytype(sanction_gid,getsessionvalues.employee_gid,objdocument);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("validation")]
        [HttpGet]
        public HttpResponseMessage Getvalidation()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlloanfacility_type objColletarl = new Mdlloanfacility_type();
            objDaSanction.DaGetvalidation(objColletarl, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objColletarl);
        }
        [ActionName("editvalidation")]
        [HttpGet]
        public HttpResponseMessage editvalidation(string sanction_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlloanfacility_type objColletarl = new Mdlloanfacility_type();
            objDaSanction.Daeditvalidation(objColletarl,sanction_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objColletarl);
        }
        [ActionName("Getloanfacilityref_no")]
        [HttpGet]
        public HttpResponseMessage Getloanfacilityref_no()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlloanfacility_type objlsamgmt = new Mdlloanfacility_type();
            objDaSanction.DaGetloanfacilityref_no(objlsamgmt, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("Editloanfacilityref_no")]
        [HttpGet]
        public HttpResponseMessage Editloanfacilityref_no(string sanction_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlloanfacility_type objlsamgmt = new Mdlloanfacility_type();
            objDaSanction.DaEditloanfacilityref_no(objlsamgmt, getsessionvalues.employee_gid,sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("Sanction_cancel")]
        [HttpPost]
        public HttpResponseMessage Sanction_cancel(sanctiondetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.DaSanction_cancel(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        [ActionName("GetSanctioninfo")]
        [HttpGet]
        public HttpResponseMessage GetSanctioninfo(string sanction_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sanctiondetails objlsamgmt = new sanctiondetails();
            objDaSanction.DaGetSanctioninfo(objlsamgmt, getsessionvalues.employee_gid, sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("historysanctionref_no")]
        [HttpGet]
        public HttpResponseMessage Gethistoryvisible(string customer2sanction_gid)
        {

            sanctionhistory objlsamgmt = new sanctionhistory();
            objDaSanction.DaGethistoryvisible(objlsamgmt, customer2sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("GetSanctionsummary")]
        [HttpGet]
        public HttpResponseMessage GetSanctionsummary()
        {
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaSanction.DaGetSanctionsummary(objsanctiondetailsList);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }
        [ActionName("Getcamdocmentadd")]
        [HttpGet]
        public HttpResponseMessage Getcamdocmentadd()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocument = new UploadDocumentname();
            objDaSanction.Getcamdocmentadd(objdocument, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("Getmomdocmentadd")]
        [HttpGet]
        public HttpResponseMessage Getmomdocmentadd()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocument = new UploadDocumentname();
            objDaSanction.Getmomdocmentadd(objdocument, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("Uploadsanctionletter")]
        [HttpPost]
        public HttpResponseMessage uploadsanctionletter()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDaSanction.DaUploadsanctionletter(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("Getsanctionletter")]
        [HttpGet]
        public HttpResponseMessage Getsanctionletter()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocument = new UploadDocumentname();
            objDaSanction.DaGetsanctionletter(objdocument, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("GetEditsanctionletter")]
        [HttpGet]
        public HttpResponseMessage GetEditsanctionletter(string sanction_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocument = new UploadDocumentname();
            objDaSanction.DaGetEditsanctionletter(sanction_gid, objdocument, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("sanctionletter_delete")]
        [HttpGet]
        public HttpResponseMessage sanctionletter_delete(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocumentcancel = new UploadDocumentname();
            objDaSanction.DaGetsanctionletterCancel(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        //Penal Interest
        [ActionName("GetPenalInterest")]
        [HttpGet]
        public HttpResponseMessage getpenalinterest(string sanction_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlloanfacility_type objlsamgmt = new Mdlloanfacility_type();
            objDaSanction.DaGetPenalInterest(objlsamgmt, sanction_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("UpdateProposedROI")]
        [HttpPost]
        public HttpResponseMessage UpdateProposedROI(Mdlloanfacility_type values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.DaGetUpdateProposedROI(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
            
        }

        [ActionName("Uploades_declarationdocument")]
        [HttpPost]
        public HttpResponseMessage Uploades_declarationdocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDaSanction.DaUploades_declarationdocument(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("Getesdocument")]
        [HttpGet]
        public HttpResponseMessage Getesdocument()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocument = new UploadDocumentname();
            objDaSanction.DaGetesdocument(objdocument, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }

        [ActionName("uploadesdocument_delete")]
        [HttpGet]
        public HttpResponseMessage uploadesdocument_delete(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocumentcancel = new UploadDocumentname();
            objDaSanction.DaGetuploadesdocument_delete(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        [ActionName("Uploadmaildocument")]
        [HttpPost]
        public HttpResponseMessage Uploadmaildocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objDaSanction.DaUploadmaildocument(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetMaildocument")]
        [HttpGet]
        public HttpResponseMessage GetMaildocument()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocument = new UploadDocumentname();
            objDaSanction.DaGetMaildocument(objdocument, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }

        [ActionName("Maildocument_delete")]
        [HttpGet]
        public HttpResponseMessage Maildocument_delete(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocumentcancel = new UploadDocumentname();
            objDaSanction.DaMaildocument_delete(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        [ActionName("uploadesdocumentadd_delete")]
        [HttpGet]
        public HttpResponseMessage uploadesdocumentadd_delete(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocumentcancel = new UploadDocumentname();
            objDaSanction.DaGetuploadesdocumentadd_delete(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

        [ActionName("Maildocumentadd_delete")]
        [HttpGet]
        public HttpResponseMessage Maildocumentadd_delete(string document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objdocumentcancel = new UploadDocumentname();
            objDaSanction.DaMaildocumentadd_delete(document_gid, objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        
        [ActionName("UpdateCheckerApproval")]
        [HttpPost]
        public HttpResponseMessage UpdateCheckerApproval(template_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.DaUpdateCheckerApproval(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTemplate_list")]
        [HttpGet]
        public HttpResponseMessage GetTemplate_list()
        {
            mdltemplate objTemplateContent = new mdltemplate();
            objDaSanction.DaGetTemplate_list(objTemplateContent);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        
        [ActionName("SanctionContent")]
        [HttpPost]
        public HttpResponseMessage SanctionContent(template_list values)
        {

            objDaSanction.DaSanctionContent(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Sanction2Facility")]
        [HttpPost]
        public HttpResponseMessage Sanction2Facility(template_list values)
        {

            objDaSanction.DaPostTemplateSanction2Facility(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionMultipleFacility")]
        [HttpPost]
        public HttpResponseMessage SanctionMultipleFacility(template_list values)
        {

            objDaSanction.DaPostTemplateSanctionMultipleFacility(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionStandbyLine")]
        [HttpPost]
        public HttpResponseMessage SanctionStandbyLine(template_list values)
        {

            objDaSanction.DaPostTemplateSanctionStandbyLine(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DBSColending")]
        [HttpPost]
        public HttpResponseMessage DBSColending(template_list values)
        {

            objDaSanction.DaPostTemplateDBSColending(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Sanction Letter Submit 
        [ActionName("SanctionLetterSubmit")]
        [HttpPost]
        public HttpResponseMessage SanctionLetterSubmit(template_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.DaSanctionLetterSubmit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTemplateDetails")]
        [HttpGet]
        public HttpResponseMessage GetTemplateDetails(string sanction_gid)
        {
            mdltemplate values = new mdltemplate();
            objDaSanction.DaGetTemplateDetails(values, sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTemplateLogDetails")]
        [HttpGet]
        public HttpResponseMessage GetTemplateLogDetails(string sanctionapprovallog_gid, string sanction_gid)
        {
            mdltemplate values = new mdltemplate();
            objDaSanction.DaGetTemplateLogDetails(values, sanctionapprovallog_gid, sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionLetterSummary")]
        [HttpGet]
        public HttpResponseMessage SanctionLetterSummary(string sanction_gid)
        {
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaSanction.DaSanctionLetterSummary(sanction_gid, objsanctiondetailsList);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        // Sanction Letter Moved To Checker 
        [ActionName("PostProceedToChecker")]
        [HttpPost]
        public HttpResponseMessage PostProceedToChecker(template_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.DaPostProceedToChecker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionToCheckerSummary")]
        [HttpGet]
        public HttpResponseMessage SanctionToCheckerSummary()
        {
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaSanction.DaSanctionToCheckerSummary(objsanctiondetailsList);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("PostProceedToApproval")]
        [HttpPost]
        public HttpResponseMessage PostProceedToApproval(template_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.DaPostProceedToApproval(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Sanction Pushback To Maker 
        [ActionName("PusbackToMaker")]
        [HttpPost]
        public HttpResponseMessage PusbackToMaker(template_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSanction.DaPusbackToMaker(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CheckerApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage CheckerApprovalSummary()
        {
            sanctiondetailsList objsanctiondetailsList = new sanctiondetailsList();
            objDaSanction.DaCheckerApprovalSummary(objsanctiondetailsList);
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetailsList);
        }

        [ActionName("PostDigitalSignature")]
        [HttpGet]
        public HttpResponseMessage PostDigitalSignature(string sanction_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            template_list values = new template_list();
            objDaSanction.DaPostDigitalSignature(sanction_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPDFGenerate")]
        [HttpGet]
        public HttpResponseMessage GetPDFGenerate(string sanction_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            template_list values = new template_list();
            objDaSanction.DaGetPDFGenerate(sanction_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionLetterLogDownload")]
        [HttpGet]
        public HttpResponseMessage SanctionLetterLogDownload(string sanctionapprovallog_gid)
        {
            template_list values = new template_list();
            objDaSanction.DaSanctionLetterLogDownload(sanctionapprovallog_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Sanction Document Upload
        [ActionName("SanctionDocAttachment")]
        [HttpPost]
        public HttpResponseMessage SanctionDocAttachment()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            UploadDocumentname objResult = new UploadDocumentname();
            httpRequest = HttpContext.Current.Request;
            objDaSanction.DaSanctionDocAttachment(httpRequest, objResult, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        // Sanction Document List
        [ActionName("SanctionDocumentList")]
        [HttpGet]
        public HttpResponseMessage SanctionDocumentList(string sanction_gid)
        {
            UploadDocumentname objfilename = new UploadDocumentname();
            objDaSanction.DaSanctionDocumentList(sanction_gid, objfilename);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }
        // Sanction Document Delete
        [ActionName("SanctionDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage SanctionDocumentDelete(string sanctiondoc_gid, string sanction_gid)
        {
            UploadDocumentname objfilename = new UploadDocumentname();
            objDaSanction.DaSanctionDocumentDelete(sanctiondoc_gid, sanction_gid, objfilename);
            return Request.CreateResponse(HttpStatusCode.OK, objfilename);
        }
    }
}                           