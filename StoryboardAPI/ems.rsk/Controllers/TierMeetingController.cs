using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.rsk.Models;
using ems.rsk.DataAccess;

namespace ems.rsk.Controllers
{
    [RoutePrefix("api/TierMeeting")]
    [Authorize]

    public class TierMeetingController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaTierMeeting objDaTierMeeting = new DaTierMeeting();

        // Tier 1 Details...//

        [ActionName("GetTier1formatlist")]
        [HttpGet]
        public HttpResponseMessage GetTier1formatlist()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            tier1formatlist objreportdtl = new tier1formatlist();
            objDaTierMeeting.DaGetTier1formatlist(getsessionvalues.employee_gid, objreportdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objreportdtl);
        }

        [ActionName("GetTier1ApprovalDtl")]
        [HttpGet]
        public HttpResponseMessage GetTier1FormatDtl(string tier1format_gid)
        {
            tier1format objreportdtl = new tier1format();
            objDaTierMeeting.DaGetTier1ApprovalDtl(tier1format_gid, objreportdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objreportdtl);
        }

        [ActionName("PostTier1Upload")]
        [HttpPost]
        public HttpResponseMessage PostTier1Upload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            tier1documentlist documentname = new tier1documentlist();
            objDaTierMeeting.DaPostTier1Upload(httpRequest, documentname, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

   

        [ActionName("GetTier1UploadCancel")]
        [HttpGet]
        public HttpResponseMessage GetTier1UploadCancel(string tmp_documentGid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            uploaddocument values = new uploaddocument();
            objDaTierMeeting.DaGetTier1UploadCancel(tmp_documentGid, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostTier1Approved")]
        [HttpPost]
        public HttpResponseMessage PostTier1Approved(tier1approval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTierMeeting.DaPostTier1Approved(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostTier1Rejected")]
        [HttpPost]
        public HttpResponseMessage PostTier1Rejected(tier1approval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTierMeeting.DaPostTier1Rejected(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Tier 2 Details...//

        [ActionName("GetVertical")]
        [HttpGet]
        public HttpResponseMessage GetVertical(string month, string tier2,string zonalmapping_gid)
        {
            tierverticallist objMdlVertical = new tierverticallist();
            objDaTierMeeting.DaGetVertical(month, tier2, zonalmapping_gid, objMdlVertical);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlVertical);
        }

        [ActionName("GetVerticalAllocationdtl")]
        [HttpGet]
        public HttpResponseMessage GetVerticalAllocationdtl(string vertical_gid, string month, string tier2_flag)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            tierallocationdtllist objtierallocationdtl = new tierallocationdtllist();
            objDaTierMeeting.DaGetVerticalAllocationdtl(vertical_gid, month, tier2_flag, objtierallocationdtl, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objtierallocationdtl);
        }

        [ActionName("GetTier2Monthdtl")]
        [HttpGet]
        public HttpResponseMessage GetTier2Monthdtl()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            tier2zonaldtl values = new tier2zonaldtl();
            objDaTierMeeting.DaGetTier2Monthdtl(values, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTier2Summary")]
        [HttpGet]
        public HttpResponseMessage GetTier2Summary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            tier2preparationlist values = new tier2preparationlist();
            objDaTierMeeting.DaGetTier2Summary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostTier2Upload")]
        [HttpPost]
        public HttpResponseMessage PostTier2Upload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            tier2documentlist documentname = new tier2documentlist();
            objDaTierMeeting.DaPostTier2Upload(httpRequest, documentname, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetTier2UploadCancel")]
        [HttpGet]
        public HttpResponseMessage GetTier2UploadCancel(string tmp_documentGid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            uploaddocument values = new uploaddocument();
            objDaTierMeeting.DaGetTier2UploadCancel(tmp_documentGid, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostTier2Preparation")]
        [HttpPost]
        public HttpResponseMessage PostTier2Preparation(tier2preparation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTierMeeting.DaPostTier2Preparation(values, getsessionvalues.user_gid,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTier2ApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetTier2ApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            tier2preparationlist objvalues = new tier2preparationlist();
            objDaTierMeeting.DaGetTier2ApprovalSummary(getsessionvalues.employee_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetTier2ViewDtl")]
        [HttpGet]
        public HttpResponseMessage GetTier2ViewDtl(string tier2preparation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            tier2viewdtl objvalues = new tier2viewdtl();
            objDaTierMeeting.DaGetTier2ViewDtl(tier2preparation_gid, objvalues,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("PostTier2TrnUpload")]
        [HttpPost]
        public HttpResponseMessage PostTrnTier2Upload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            tier2documentlist documentname = new tier2documentlist();
            objDaTierMeeting.DaPostTrnTier2Upload(httpRequest, documentname, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetTier2TrnUploadCancel")]
        [HttpGet]
        public HttpResponseMessage GetTier2TrnUploadCancel(string tier2document_gid, string tier2preparation_gid)
        {
            tier2documentlist values = new tier2documentlist();
            objDaTierMeeting.DaGetTier2TrnUploadCancel(tier2document_gid, tier2preparation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostUpdateTier2")]
        [HttpPost]
        public HttpResponseMessage PostUpdateTier2(tier2preparation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTierMeeting.DaPostUpdateTier2(getsessionvalues.user_gid, values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostTier2Approved")]
        [HttpPost]
        public HttpResponseMessage PostTier2Approved(tier2approval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTierMeeting.DaPostTier2Approved(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostTier2Rejected")]
        [HttpPost]
        public HttpResponseMessage PostTier2Rejected(tier2approval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTierMeeting.DaPostTier2Rejected(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Tier 3 Details...//

        [ActionName("GetTier3Monthdtl")]
        [HttpGet]
        public HttpResponseMessage GetTier3Monthdtl()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            tier2zonaldtl values = new tier2zonaldtl();
            objDaTierMeeting.DaGetTier3Monthdtl(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostTier3Preparation")]
        [HttpPost]
        public HttpResponseMessage PostTier3Preparation(tier3preparation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTierMeeting.DaPostTier3Preparation(values, getsessionvalues.user_gid,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostTier3Complete")]
        [HttpPost]
        public HttpResponseMessage PostTier3Complete(tier3completedtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTierMeeting.DaPostTier3Complete(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostTier3Upload")]
        [HttpPost]
        public HttpResponseMessage PostTier3Upload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            tier3documentlist documentname = new tier3documentlist();
            objDaTierMeeting.DaPostTier3Upload(httpRequest, documentname, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetTier3UploadCancel")]
        [HttpGet]
        public HttpResponseMessage GetTier3UploadCancel(string tmp_documentGid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            uploaddocument values = new uploaddocument();
            objDaTierMeeting.DaGetTier3UploadCancel(tmp_documentGid, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTier3Summary")]
        [HttpGet]
        public HttpResponseMessage GetTier3Summary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            tier3preparationlist values = new tier3preparationlist();
            objDaTierMeeting.DaGetTier3Summary(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTier3CompletedSummary")]
        [HttpGet]
        public HttpResponseMessage GetTier3CompletedSummary()
        {
            tier3preparationlist values = new tier3preparationlist();
            objDaTierMeeting.DaGetTier3CompletedSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTier3ViewDtl")]
        [HttpGet]
        public HttpResponseMessage GetTier3ViewDtl(string tier3preparation_gid)
        {
            tier3viewdtl objvalues = new tier3viewdtl();
            objDaTierMeeting.DaGetTier3ViewDtl(tier3preparation_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("PostTrnTier3Upload")]
        [HttpPost]
        public HttpResponseMessage PostTrnTier3Upload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            tier3documentlist documentname = new tier3documentlist();
            objDaTierMeeting.DaPostTrnTier3Upload(httpRequest, documentname, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetTier3TrnUploadCancel")]
        [HttpGet]
        public HttpResponseMessage GetTier3TrnUploadCancel(string tier3document_gid, string tier3preparation_gid)
        {
            tier3documentlist values = new tier3documentlist();
            objDaTierMeeting.DaGetTier3TrnUploadCancel(tier3document_gid, tier3preparation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostUpdateTier3")]
        [HttpPost]
        public HttpResponseMessage PostUpdateTier3(tier3preparation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTierMeeting.DaPostUpdateTier3(getsessionvalues.user_gid, values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostTier2codeChange")]
        [HttpPost]
        public HttpResponseMessage PostTier2codeChange(tier2code values)
        {
            result objvalues = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTierMeeting.DaPostTier2codeChange(values,getsessionvalues.user_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetTier2ColorDetails")]
        [HttpGet]
        public HttpResponseMessage GetTier2ColorDetails(string allocationdtl_gid)
        {
            tiercodedtllist values = new tiercodedtllist();
            objDaTierMeeting.DaGetTier2ColorDetails(allocationdtl_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTier2ColorDelete")]
        [HttpGet]
        public HttpResponseMessage GetTier2ColorDelete(string tmptier2_codechange)
        {
            result values = new result();
            objDaTierMeeting.DaGetTier2ColorDelete(tmptier2_codechange, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTierColorDetails")]
        [HttpGet]
        public HttpResponseMessage GetTierColorDetails(string allocationdtl_gid)
        {
            tiercodedtllist values = new tiercodedtllist();
            objDaTierMeeting.DaGetTierColorDetails(allocationdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostTier3codeChange")]
        [HttpPost]
        public HttpResponseMessage PostTier3codeChange(tier3code values)
        {
            result objvalues = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaTierMeeting.DaPostTier3codeChange(values, getsessionvalues.user_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetTier3ColorDelete")]
        [HttpGet]
        public HttpResponseMessage GetTier3ColorDelete(string tmptier3_codechangegid)
        {
            result values = new result();
            objDaTierMeeting.DaGetTier3ColorDelete(tmptier3_codechangegid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostTierColorUpdate")]
        [HttpPost]
        public HttpResponseMessage PostTierColorUpdate(tiercodechange values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objvalues = new result();
            objDaTierMeeting.DaPostTierColorUpdate(values, objvalues, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        // Allocation 360 ..//

        [ActionName("GetViewTierObservationdtl")]
        [HttpGet]
        public HttpResponseMessage GetViewTierObservationdtl(string allocationdtl_gid)
        {
            observationTierdtl objreportdtl = new observationTierdtl();
            objDaTierMeeting.DaGetViewTierObservationdtl(allocationdtl_gid, objreportdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objreportdtl);
        }
        [ActionName("GetTier1Format360Dtl")]
        [HttpGet]
        public HttpResponseMessage GetTier1Format360Dtl(string tier1format_gid)
        {
            tier1format objreportdtl = new tier1format();
            objDaTierMeeting.DaGetTier1Format360Dtl(tier1format_gid, objreportdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objreportdtl);
        }

        [ActionName("GetTier2Report360Dtl")]
        [HttpGet]
        public HttpResponseMessage GetTier2Report360Dtl(string allocationdtl_gid)
        {
            tier2Reportviewdtl objvalues = new tier2Reportviewdtl();
            objDaTierMeeting.DaGetTier2Report360Dtl(allocationdtl_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetTier3Report360Dtl")]
        [HttpGet]
        public HttpResponseMessage GetTier3Report360Dtl(string allocationdtl_gid)
        {
            tier3viewdtl objvalues = new tier3viewdtl();
            objDaTierMeeting.DaGetTier3Report360Dtl(allocationdtl_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }
    }
}
