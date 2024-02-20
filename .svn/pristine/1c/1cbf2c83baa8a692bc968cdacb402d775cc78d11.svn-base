using ems.audit.DataAccess;
using ems.audit.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace ems.audit.Controllers
{
    [RoutePrefix("api/AtmTrnSampling")]
    [Authorize]
    public class AtmTrnSamplingController : ApiController
    {
        DaAtmTrnSampling objDaAtmTrnSampling = new DaAtmTrnSampling();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("ImportExcelSample")]
        [HttpPost]
        public HttpResponseMessage ImportExcelSample()
        {
            HttpRequest httpRequest;
            MdlAtmTrnSampling values = new MdlAtmTrnSampling();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaAtmTrnSampling.DaAtmTrnSamplingimportexcel(httpRequest, getsessionvalues.employee_gid, objResult, values);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("GetSampleDynamicdata")]
        [HttpGet]
        public HttpResponseMessage GetSampleDynamicdata(string auditcreation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sampledynamicdatadtl values = new sampledynamicdatadtl();
            objDaAtmTrnSampling.DaGetSampleDynamicdata(auditcreation_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //[ActionName("GetTagFlag")]
        //[HttpGet]
        //public HttpResponseMessage GetTagFlag(string sampleimport_gid)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    MdlAtmTrnSampling values = new MdlAtmTrnSampling();
        //    objDaAtmTrnSampling.DaGetTagFlag(sampleimport_gid, values, getsessionvalues.employee_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}


        [ActionName("GetSampleRaiseQueryDynamicdata")]
        [HttpGet]
        public HttpResponseMessage GetSampleRaiseQueryDynamicdata(string auditcreation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            sampledynamicdatadtl values = new sampledynamicdatadtl();
            objDaAtmTrnSampling.DaGetSampleRaiseQueryDynamicdata(auditcreation_gid,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSampleAuditor")]
        [HttpGet]
        public HttpResponseMessage GetSampleAuditor(string auditcreation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnSampling values = new MdlAtmTrnSampling();
            objDaAtmTrnSampling.DaGetSampleAuditor(auditcreation_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSamplesummary")]
        [HttpGet]
        public HttpResponseMessage GetSamplesummary(string auditcreation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnSampling values = new MdlAtmTrnSampling();
            objDaAtmTrnSampling.DaGetSamplesummary(auditcreation_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
      
        [ActionName("GetSample")]
        [HttpGet]
        public HttpResponseMessage GetSample(string auditcreation_gid, string sampleimport_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAtmTrnSampling values = new MdlAtmTrnSampling();
            objDaAtmTrnSampling.DaGetSample(auditcreation_gid, sampleimport_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK,  values);
        }

        [ActionName("GetSampleimportexcel")]
        [HttpGet]
        public HttpResponseMessage GetSampleimportexcel()
        {
            HttpRequest httpRequest;
            MdlAtmTrnSampling values = new MdlAtmTrnSampling();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaAtmTrnSampling.DaGetSampleimportexcel(httpRequest, getsessionvalues.employee_gid, objResult, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetTagUser")]
        [HttpPost]
        public HttpResponseMessage GetTagUser(MdlAtmTrnSampling values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmTrnSampling.DaGetTagUser(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSampleName")]
        [HttpGet]
        public HttpResponseMessage GetSampleName(string sampleimport_gid)
        {
            MdlAtmTrnSampling values = new MdlAtmTrnSampling();
            objDaAtmTrnSampling.DaGetSampleName(sampleimport_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSampleView")]
        [HttpGet]
        public HttpResponseMessage GetSampleView(string sampleimport_gid)
        {
            MdlAtmTrnSampling values = new MdlAtmTrnSampling();
            objDaAtmTrnSampling.DaGetSampleView(sampleimport_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmployeeName")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeName(string sampleimport_gid)
        {
            employelist values = new employelist();
            objDaAtmTrnSampling.DaGetEmployeeName(sampleimport_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostRaiseQuery(samplequerydata values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmTrnSampling.DaPostRaiseQuery(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditSampleQuery")]
        [HttpGet]
        public HttpResponseMessage EditSampleQuery(string sampleimport_gid)
        {
            MdlAtmTrnSampling values = new MdlAtmTrnSampling();
            objDaAtmTrnSampling.DaEditSampleQuery(sampleimport_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAssignedQuerySummary")]
        [HttpGet]
        public HttpResponseMessage GetAssignedQuerySummary( string auditcreation_gid)
        {
            MdlAtmTrnSampling values = new MdlAtmTrnSampling();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmTrnSampling.DaAssignedQuerySummary(getsessionvalues.employee_gid, values, auditcreation_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }      
        [ActionName("PostSampleQuerydetail")]
        [HttpPost]
        public HttpResponseMessage PostSampleQuerydetail(MdlAtmTrnMyAuditTaskAuditee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmTrnSampling.DaPostSampleQuerydetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSampleQuerydetaillist")]
        [HttpGet]
        public HttpResponseMessage GetSampleQuerydetaillist(string sampleimport_gid)
        {
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmTrnSampling.DaGetSampleQuerydetaillist(getsessionvalues.employee_gid, sampleimport_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAssignedQuery")]
        [HttpGet]
        public HttpResponseMessage EditAssignedQuery(string sampleraisequery_gid)
        {
            MdlAtmTrnSampling values = new MdlAtmTrnSampling();
            objDaAtmTrnSampling.DaEditAssignedQuery(sampleraisequery_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("closequerysample")]
        [HttpPost]
        public HttpResponseMessage closequerysample(MdlAtmTrnMyAuditTaskAuditee values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmTrnSampling.Daclosequerysample(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("closesamplequerysummary")]
        [HttpGet]
        public HttpResponseMessage closesamplequerysummary(string sampleimport_gid)
        {
            MdlAtmTrnMyAuditTaskAuditee values = new MdlAtmTrnMyAuditTaskAuditee();
            objDaAtmTrnSampling.Daclosesamplequerysummary(values, sampleimport_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Sampleexcelupload")]
        [HttpPost]
        public HttpResponseMessage Sampleexcelupload()
        {
            HttpRequest httpRequest;
            MdlAtmTrnSampling values = new MdlAtmTrnSampling();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            result objResult = new result();
            objDaAtmTrnSampling.DaAtmTrnSampleexcelupload(httpRequest, getsessionvalues.employee_gid, objResult, values);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("AssignedTagUserSummary")]
        [HttpGet]
        public HttpResponseMessage AssignedTagUserSummary(string sampleimport_gid)
        {
            MdlAtmTrnSampling values = new MdlAtmTrnSampling();
            objDaAtmTrnSampling.DaAssignedTagUserSummary(sampleimport_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDocUploadlist")]
        [HttpGet]
        public HttpResponseMessage GetDocUploadlist(string auditcreation_gid)
        {
            DocUploadlog values = new DocUploadlog();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAtmTrnSampling.DaGetDocUploadlist(getsessionvalues.employee_gid, auditcreation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("QueryDocUpload")]
        [HttpPost]
        public HttpResponseMessage QueryDocUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            document_upload documentname = new document_upload();
            objDaAtmTrnSampling.DaQueryDocUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
    }
}
