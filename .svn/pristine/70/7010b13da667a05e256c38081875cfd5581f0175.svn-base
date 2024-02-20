using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.mastersamagro.Models;
using ems.mastersamagro.DataAccess;
using System.Data;
using System.Data.Odbc;
using Newtonsoft.Json;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to third party API for verifying the crime track records to our client's customer data 
    /// </summary>
    /// <remarks>Written by Praveen Raj.R </remarks>

    [RoutePrefix("api/AgrCrimeCheckAPI")]
    [Authorize]
    public class AgrCrimeCheckAPIController : ApiController
    {
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAgrCrimeCheckAPI objDaCrimeCheckAPI = new DaAgrCrimeCheckAPI();
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid;
        int mnResult;

        [ActionName("GetCrimeRecordsIndividual")]
        [HttpPost]
        public HttpResponseMessage GetCrimeRecordsIndividual(MdlIndividualCrimeRecordRequest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            var response = objDaCrimeCheckAPI.DaGetCrimeRecordsIndividual(getsessionvalues.employee_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [ActionName("GetCrimeRecordsCompany")]
        [HttpPost]
        public HttpResponseMessage GetCrimeRecordsCompany(MdlCompanyCrimeRecordRequest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            var response = objDaCrimeCheckAPI.DaGetCrimeRecordsCompany(getsessionvalues.employee_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [ActionName("RequestCrimeReportIndividual")]
        [HttpPost]
        public HttpResponseMessage RequestCrimeReportIndividual(MdlIndividualCrimeReportRequest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            var response = objDaCrimeCheckAPI.DaRequestCrimeReportIndividual(getsessionvalues.employee_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [AllowAnonymous]
        [ActionName("PostCallbackReportDetailsIndividual")]
        [HttpPost]
        public HttpResponseMessage PostCallbackReportDetailsIndividual(MdlCallbackResponse values)
        {         
            var response = objDaCrimeCheckAPI.DaPostCallbackReportDetailsIndividual(values);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [ActionName("GetIndividualDetailsReport")]
        [HttpGet]
        public HttpResponseMessage GetIndividualDetailsReport(string contact_gid)
        {
            MdlIndividualCrimeReport values = new MdlIndividualCrimeReport();
            objDaCrimeCheckAPI.DaGetIndividualDetailsReport(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualReportList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualReportList(string contact_gid)
        {
            MdlIndividualCrimeReportList values = new MdlIndividualCrimeReportList();
            objDaCrimeCheckAPI.DaGetIndividualReportList(values, contact_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CrimeReportIndividualView")]
        [HttpGet]
        public HttpResponseMessage CrimeReportIndividualView(string crimereportcontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlIndividualCrimeReportResponse values = new MdlIndividualCrimeReportResponse();
            msSQL = " select report_content" +
                       " from ocs_mst_tcrimereportcontact where crimereportcontact_gid='" + crimereportcontact_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlIndividualCrimeReportResponse>(objODBCDatareader["report_content"].ToString());
            }
            objODBCDatareader.Close();
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("RequestCrimeReportCompany")]
        [HttpPost]
        public HttpResponseMessage RequestCrimeReportCompany(MdlCompanyCrimeReportRequest values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            var response = objDaCrimeCheckAPI.DaRequestCrimeReportCompany(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [AllowAnonymous]
        [ActionName("PostCallbackReportDetailsCompany")]
        [HttpPost]
        public HttpResponseMessage PostCallbackReportDetailsCompany(MdlCallbackResponse values)
        {
            var response = objDaCrimeCheckAPI.DaPostCallbackReportDetailsCompany(values);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [ActionName("GetCompanyReportList")]
        [HttpGet]
        public HttpResponseMessage GetCompanyReportList(string institution_gid)
        {
            MdlCompanyCrimeReportList values = new MdlCompanyCrimeReportList();
            objDaCrimeCheckAPI.DaGetCompanyReportList(values, institution_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCompanyDetailsReport")]
        [HttpGet]
        public HttpResponseMessage GetCompanyDetailsReport(string institution_gid)
        {
            MdlCompanyCrimeReport values = new MdlCompanyCrimeReport();
            objDaCrimeCheckAPI.DaGetCompanyDetailsReport(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CrimeReportCompanyView")]
        [HttpGet]
        public HttpResponseMessage CrimeReportCompanyView(string crimereportinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlCompanyCrimeReportResponse values = new MdlCompanyCrimeReportResponse();
            msSQL = " select report_content" +
                       " from ocs_mst_tcrimereportinstitution where crimereportinstitution_gid='" + crimereportinstitution_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlCompanyCrimeReportResponse>(objODBCDatareader["report_content"].ToString());
            }
            objODBCDatareader.Close();
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCrimeRecordIndividualDetail")]
        [HttpGet]
        public HttpResponseMessage GetCrimeRecordIndividualDetail(string contact_gid)
        {
            MdlIndividualCrimeRecord values = new MdlIndividualCrimeRecord();
            objDaCrimeCheckAPI.DaGetCrimeRecordIndividualDetail(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCrimeRecordCompanyDetail")]
        [HttpGet]
        public HttpResponseMessage GetCrimeRecordCompanyDetail(string institution_gid)
        {
            MdlCompanyCrimeRecord values = new MdlCompanyCrimeRecord();
            objDaCrimeCheckAPI.DaGetCrimeRecordCompanyDetail(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TagCaseIndividual")]
        [HttpPost]
        public HttpResponseMessage TagCaseIndividual(MdlTagCaseIndividual values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaCrimeCheckAPI.DaTagCaseIndividual(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaggedCaseIndividualSummary")]
        [HttpGet]
        public HttpResponseMessage GetTaggedCaseIndividualSummary(string contact_gid)
        {
            MdlTagCaseIndividual values = new MdlTagCaseIndividual();
            objDaCrimeCheckAPI.DaGetTaggedCaseIndividualSummary(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteTaggedCaseIndividual")]
        [HttpGet]
        public HttpResponseMessage DeleteTaggedCaseIndividual(string crimecasetaggedcontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlTagCaseIndividual values = new MdlTagCaseIndividual();
            objDaCrimeCheckAPI.DaDeleteTaggedCaseIndividual(crimecasetaggedcontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TagCaseInstitution")]
        [HttpPost]
        public HttpResponseMessage TagCaseInstitution(MdlTagCaseInstitution values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            objDaCrimeCheckAPI.DaTagCaseInstitution(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaggedCaseInstitutionSummary")]
        [HttpGet]
        public HttpResponseMessage GetTaggedCaseInstitutionSummary(string institution_gid)
        {
            MdlTagCaseInstitution values = new MdlTagCaseInstitution();
            objDaCrimeCheckAPI.DaGetTaggedCaseInstitutionSummary(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteTaggedCaseInstitution")]
        [HttpGet]
        public HttpResponseMessage DeleteTaggedCaseInstitution(string crimecasetaggedinstitution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlTagCaseInstitution values = new MdlTagCaseInstitution();
            objDaCrimeCheckAPI.DaDeleteTaggedCaseInstitution(crimecasetaggedinstitution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCCCaseTaggedIndividualSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCCaseTaggedIndividualSummary(string application_gid)
        {
            MdlCCCaseTaggedIndividual values = new MdlCCCaseTaggedIndividual();
            objDaCrimeCheckAPI.DaGetCCCaseTaggedIndividualSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCCCaseTaggedInstitutionSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCCaseTaggedInstitutionSummary(string application_gid)
        {
            MdlCCCaseTaggedInstitution values = new MdlCCCaseTaggedInstitution();
            objDaCrimeCheckAPI.DaGetCCCaseTaggedInstitutionSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCCReportInstitutionSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCReportInstitutionSummary(string application_gid)
        {
            MdlCCReportInstitution values = new MdlCCReportInstitution();
            objDaCrimeCheckAPI.DaGetCCReportInstitutionSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCCReportIndividualSummary")]
        [HttpGet]
        public HttpResponseMessage GetCCReportIndividualSummary(string application_gid)
        {
            MdlCCReportIndividual values = new MdlCCReportIndividual();
            objDaCrimeCheckAPI.DaGetCCReportIndividualSummary(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



    }
}