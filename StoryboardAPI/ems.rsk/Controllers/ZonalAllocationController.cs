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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Newtonsoft.Json.Converters;
using ems.storage.Functions;
using RestSharp;

namespace StoryboardAPI.Controllers.ems.rsk
{
    [RoutePrefix("api/zonalAllocation")]
    [Authorize]

    public class zonalAllocationController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaZonalAllocation objDaZonalAllocation = new DaZonalAllocation();
        Fnazurestorage objFnazurestorage = new Fnazurestorage();
        Fnazurestorage objcmnstorage = new Fnazurestorage();

        [ActionName("GetZonalQualifiedAllocation")]
        [HttpGet]
        public HttpResponseMessage GetZonalQualifiedAllocation()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            qualifiedallocationlist objmappingdtlList = new qualifiedallocationlist();
            objDaZonalAllocation.DaGetZonalQualifiedAllocation(getsessionvalues.employee_gid, objmappingdtlList);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }
        [ActionName("GetZoanlFreshAllocation")]
        [HttpGet]
        public HttpResponseMessage GetQualifiedFreshAllocation()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            qualifiedallocationlist objmappingdtlList = new qualifiedallocationlist();
            objDaZonalAllocation.DaGetZoanlFreshAllocation(getsessionvalues.employee_gid, objmappingdtlList);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }

        [ActionName("GetZonalReVisitAllocation")]
        [HttpGet]
        public HttpResponseMessage GetQualifiedReVisitAllocation()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            qualifiedallocationlist objmappingdtlList = new qualifiedallocationlist();
            objDaZonalAllocation.DaGetZoanlReVisitAllocation(getsessionvalues.employee_gid, objmappingdtlList);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }

        [ActionName("GetZonalCurrentAllocation")]
        [HttpGet]
        public HttpResponseMessage GetZonalCurrentAllocation()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            allocationlist objmappingdtl = new allocationlist();
            objDaZonalAllocation.DaGetZonalCurrentAllocation(getsessionvalues.employee_gid, objmappingdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtl);
        }

        [ActionName("GetZonalUpcomingAllocation")]
        [HttpGet]
        public HttpResponseMessage GetZonalUpcomingAllocation()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            allocationlist objmappingdtl = new allocationlist();
            objDaZonalAllocation.DaGetZonalUpcomingAllocation(getsessionvalues.employee_gid, objmappingdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtl);
        }

        [ActionName("GetZonalBreachedAllocation")]
        [HttpGet]
        public HttpResponseMessage GetBreachedAllocationSummary()
        {
            breachedlist objmappingdtl = new breachedlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaZonalAllocation.DaGetBreachedAllocationSummary(objmappingdtl, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtl);
        }

        [ActionName("GetZonalcompletedAlloSummary")]
        [HttpGet]
        public HttpResponseMessage DaGetZonalcompletedAlloSummary()
        {
            allocationlist objmappingdtlList = new allocationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaZonalAllocation.DaGetZonalcompletedAlloSummary(objmappingdtlList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }

        [ActionName("GetVisitCancelChanges")]
        [HttpGet]
        public HttpResponseMessage DaGetVisitCancelChanges()
        {
            allocationlist objmappingdtlList = new allocationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaZonalAllocation.DaGetVisitCancelChanges(objmappingdtlList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }

        [ActionName("GetZonalExternalAllocation")]
        [HttpGet]
        public HttpResponseMessage GetZonalExternalAllocation()
        {
            allocationlist objmappingdtlList = new allocationlist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaZonalAllocation.DaGetZonalExternalAllocation(objmappingdtlList, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }

        [ActionName("visitReportpdfcontent")]
        [HttpGet]
        public HttpResponseMessage dn1pdf(string allocationdtl_gid)
        {
            visitReportPDFContent objvisitReportPDFContent = new visitReportPDFContent();
            var ls_response = new Dictionary<string, object>();
            var client = new RestClient(ConfigurationManager.AppSettings["report_api_path"].ToString() + "/VisitReport/getvisitreportpdf/" + allocationdtl_gid);
            var request = new RestRequest(Method.GET);
            request.AddParameter("allocationdtl_gid", allocationdtl_gid);
            IRestResponse response = client.Execute(request);
            string path = JsonConvert.DeserializeObject<string>(response.Content);
            var pathArray = path.Split(new string[] { "EMS/" }, StringSplitOptions.None);
            var fileNameArrray = path.Split(new string[] { "Report/" }, StringSplitOptions.None);
            objvisitReportPDFContent.file_path = pathArray[1].ToString();
            objvisitReportPDFContent.file_name = fileNameArrray[1].ToString();
            ls_response = objcmnstorage.DaFileUploadDocument(objvisitReportPDFContent.file_path);
            objvisitReportPDFContent.file_path = objcmnstorage.EncryptData(objvisitReportPDFContent.file_path);
            var byName = (IDictionary<string, object>)ls_response;
            objvisitReportPDFContent.status = (bool)byName["status"];
            return Request.CreateResponse(HttpStatusCode.OK, objvisitReportPDFContent);
        }


        [ActionName("getZonalCustomerAllocation")]
        [HttpGet]
        public HttpResponseMessage GetCustomerAllocation()
        {
            customerList objvalues = new customerList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaZonalAllocation.DaGetZonalCustomerAllocation(objvalues,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetZonalAllocationLogDetail")]
        [HttpGet]
        public HttpResponseMessage GetZonalAllocationLogDetail()
        {
            todayactivityList objvalues = new todayactivityList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaZonalAllocation.DaGetZonalAllocationLogDetail(getsessionvalues.employee_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetZRMCalenderDtl")]
        [HttpGet]
        public HttpResponseMessage GetZRMCalenderDtl()
        {
            calendarevent values = new calendarevent();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaZonalAllocation.DaGetZRMCalenderDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetExclusionCustomer")]
        [HttpGet]
        public HttpResponseMessage GetExclusionCustomer(string customer_urn,string exclusion_reason)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaZonalAllocation.DaGetExclusionCustomer(customer_urn, exclusion_reason, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetExclusionZonalSummary")]
        [HttpGet]
        public HttpResponseMessage GetExclusionZonalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            exclusioncustomerlist values = new exclusioncustomerlist();
            objDaZonalAllocation.DaGetExclusionZonalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetExclusionCustomerHistory")]
        [HttpGet]
        public HttpResponseMessage GetExclusionCustomerHistory(string customer_urn)
        {
            exclusionhistorylist values = new exclusionhistorylist();
            objDaZonalAllocation.DaGetExclusionCustomerHistory(customer_urn, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetActivationCustomer")]
        [HttpGet]
        public HttpResponseMessage GetActivationCustomer(string customer_urn,string exclusion_reason)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaZonalAllocation.DaGetActivationCustomer(customer_urn, exclusion_reason, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
