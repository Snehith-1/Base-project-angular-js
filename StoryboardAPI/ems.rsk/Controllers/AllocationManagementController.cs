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

namespace StoryboardAPI.Controllers.ems.rsk
{
    [RoutePrefix("api/allocationManagement")]
    [Authorize]

    public class allocationManagementController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAllocationManagement objDaAllocationManagement = new DaAllocationManagement();

        [ActionName("getallocateRM")]
        [HttpGet]
        public HttpResponseMessage GetAllocateRM(string district_gid)
        {
            mappingdtl objmappingdtl = new mappingdtl();
            objDaAllocationManagement.DaGetAllocateRM(district_gid, objmappingdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtl);
        }

        [ActionName("postRMallocationdetails")]
        [HttpPost]
        public HttpResponseMessage PostAllocationRMdetails(mappingdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mappingdtl objmappingdtl = new mappingdtl();
            objDaAllocationManagement.DaPostAllocationRMdetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostCreateAllocation")]
        [HttpPost]
        public HttpResponseMessage PostCreateAllocation(mappingdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAllocationManagement.DaPostCreateAllocation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getRMAllocationSave")]
        [HttpPost]
        public HttpResponseMessage GetAllocationSaveDetails(allocation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAllocationManagement.DaGetAllocationSaveDetails(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("getallocationSummary")]
        [HttpGet]
        public HttpResponseMessage GetAllocationSummary()
        {
            mappingdtlList objmappingdtlList = new mappingdtlList();
            objDaAllocationManagement.DaGetAllocationSummary(objmappingdtlList);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }

        [ActionName("GetUpcomingAllocation")]
        [HttpGet]
        public HttpResponseMessage GetUpcomingAllocation()
        {
            allocationlist objmappingdtlList = new allocationlist();
            objDaAllocationManagement.DaGetUpcomingAllocation(objmappingdtlList);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }

        [ActionName("getExternalAllocationSummary")]
        [HttpGet]
        public HttpResponseMessage GetExternalAllocationSummary()
        {
            allocationlist objmappingdtlList = new allocationlist();
            objDaAllocationManagement.DaGetExternalAllocationSummary(objmappingdtlList);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }

        [ActionName("getNotallocateSummary")]
        [HttpGet]
        public HttpResponseMessage DaGetNotAllocateSummary()
        {
            mappingdtlList objmappingdtlList = new mappingdtlList();
            objDaAllocationManagement.DaGetNotAllocateSummary(objmappingdtlList);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }

        [ActionName("GetCurrentAllocateSummary")]
        [HttpGet]
        public HttpResponseMessage GetCurrentAllocateSummary()
        {
            allocationlist objmappingdtlList = new allocationlist();
            objDaAllocationManagement.DaGetCurrentAllocateSummary(objmappingdtlList);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }

        [ActionName("getcompletedAllocationSummary")]
        [HttpGet]
        public HttpResponseMessage GetcompletedAllocationSummary()
        {
            allocationlist objmappingdtlList = new allocationlist();
            objDaAllocationManagement.DaGetcompletedAllocationSummary(objmappingdtlList);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }

        [ActionName("getallocatedtls")]
        [HttpGet]
        public HttpResponseMessage GetAllocationViewdtl(string allocationdtl_gid)
        {
            mappingdtl objmappingdtl = new mappingdtl();
            objDaAllocationManagement.DaGetAllocationViewdtl(allocationdtl_gid, objmappingdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtl);
        }


        [ActionName("getcreateallocatedtls")]
        [HttpGet]
        public HttpResponseMessage GetCreateAllocatedtls(string customer_gid)
        {
            customerdetail objmappingdtl = new customerdetail();
            objDaAllocationManagement.DaGetCreateAllocatedtls(customer_gid, objmappingdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtl);
        }

        [ActionName("getRMallocationList")]
        [HttpGet]
        public HttpResponseMessage GetAllocatedRMList()
        {
            mappingdtlList objmappingdtl = new mappingdtlList();
            objDaAllocationManagement.DaGetAllocatedRMList(objmappingdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtl);
        }

        [ActionName("getassignedAllocation")]
        [HttpGet]
        public HttpResponseMessage GetAssignedAllocation(string assignedRM_gid)
        {
            rmallocationlist values = new rmallocationlist();
            objDaAllocationManagement.DaGetRMcurrentallocateddtl(assignedRM_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRMupcomingallocateddtl")]
        [HttpGet]
        public HttpResponseMessage GetRMPendingdetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            rmallocationlist values = new rmallocationlist();
            objDaAllocationManagement.DaGetRMupcomingallocateddtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getRMCompleteddetails")]
        [HttpGet]
        public HttpResponseMessage GetRMCompleteddetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            rmallocationlist values = new rmallocationlist();
            objDaAllocationManagement.DaGetRMCompleteddetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRMExclusiondetails")]
        [HttpGet]
        public HttpResponseMessage GetRMExclusiondetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            exclusionAllocationlist values = new exclusionAllocationlist();
            objDaAllocationManagement.DaGetRMExclusiondetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRMcurrentallocateddtl")]
        [HttpGet]
        public HttpResponseMessage GetRMcurrentallocateddtl()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            rmallocationlist objmappingdtl = new rmallocationlist();
            objDaAllocationManagement.DaGetRMcurrentallocateddtl(getsessionvalues.employee_gid, objmappingdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtl);
        }

        [ActionName("postAllocationTransfer")]
        [HttpPost]
        public HttpResponseMessage PostAllocationTransfer(allocationTransfer values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            allocationTransfer objvalues = new allocationTransfer();
            objDaAllocationManagement.DaPostAllocationTransfer(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("gettransferDetails")]
        [HttpGet]
        public HttpResponseMessage GetTransferDetails(string allocationdtl_gid)
        {
            allocationTransfer objallocationTransfer = new allocationTransfer();
            objDaAllocationManagement.DaGetTransferDetails(allocationdtl_gid, objallocationTransfer);
            return Request.CreateResponse(HttpStatusCode.OK, objallocationTransfer);
        }

        [ActionName("getExternalDetails")]
        [HttpGet]
        public HttpResponseMessage GetExternalDetails(string allocationdtl_gid)
        {
            externalAllcoation objexternalAllcoation = new externalAllcoation();
            objDaAllocationManagement.DaGetExternalDetails(allocationdtl_gid, objexternalAllcoation);
            return Request.CreateResponse(HttpStatusCode.OK, objexternalAllcoation);
        }


        [ActionName("getExternalNamelist")]
        [HttpGet]
        public HttpResponseMessage GetExternalNamelist()
        {
            externaldtlList objexternaldtlList = new externaldtlList();
            objDaAllocationManagement.DaGetExternalNamelist(objexternaldtlList);
            return Request.CreateResponse(HttpStatusCode.OK, objexternaldtlList);
        }


        [ActionName("postAllocationExternal")]
        [HttpPost]
        public HttpResponseMessage postAllocationExternal(externalAllocate values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAllocationManagement.DaPostUpdateAllocateExternal(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ExternalUpload")]
        [HttpPost]
        public HttpResponseMessage PostExternalUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaAllocationManagement.DaPostExternalUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("ExternalUploadcancel")]
        [HttpGet]
        public HttpResponseMessage GetExternalUploadcancel(string tmp_documentGid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            uploaddocument values = new uploaddocument();
            objDaAllocationManagement.DaGetExternalUploadcancel(tmp_documentGid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("tmpExternaldocumentclear")]
        [HttpGet]
        public HttpResponseMessage GetTmpExtDocumentClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            document objresult = new document();
            objDaAllocationManagement.DaGetTmpExtDocumentClear(getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("getExternaldocument")]
        [HttpGet]
        public HttpResponseMessage GetExternaldocument(string allocationdtl_gid)
        {
            uploaddocument values = new uploaddocument();
            objDaAllocationManagement.DaGetExternaldocument(values, allocationdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getCustomerAllocation")]
        [HttpGet]
        public HttpResponseMessage GetCustomerAllocation()
        {
            customerList objvalues = new customerList();
            objDaAllocationManagement.DaGetCustomerAllocation(objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }


        [ActionName("AllocateUpload")]
        [HttpPost]
        public HttpResponseMessage AllocateUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDaAllocationManagement.DaPostAllocateUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("AllocateUploadcancel")]
        [HttpGet]
        public HttpResponseMessage AllocateUploadcancel(string tmp_documentGid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            uploaddocument values = new uploaddocument();
            objDaAllocationManagement.DaAllocateUploadcancel(tmp_documentGid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("tmpAllocatedocumentclear")]
        [HttpGet]
        public HttpResponseMessage gettmpAllodocumentclear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            document objresult = new document();
            objDaAllocationManagement.DaGettmpAllodocumentclear(getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("getAllocationdocument")]
        [HttpGet]
        public HttpResponseMessage getAllocationdocument(string allocationdtl_gid)
        {
            uploaddocument values = new uploaddocument();
            objDaAllocationManagement.DaGetAllocationdocument(values, allocationdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getAllocationHistory")]
        [HttpGet]
        public HttpResponseMessage getAllocationHistory(string customer_gid)
        {
            overallhistoryallocationlist objmappingdtlList = new overallhistoryallocationlist();
            objDaAllocationManagement.DaGetAllocationHistory(customer_gid,objmappingdtlList);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }

        [ActionName("postlastVisitDate")]
        [HttpPost]
        public HttpResponseMessage postlastVisitDate(lastvisitdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAllocationManagement.DaPostlastvisitdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAllocationCustomerDtl")]
        [HttpGet]
        public HttpResponseMessage GetAllocationCustomerDtl(string allocationdtl_gid)
        {
            sanctionloanlist values = new sanctionloanlist();
            objDaAllocationManagement.DaGetAllocationCustomerDtl(values, allocationdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAllocateloanList")]
        [HttpPost]
        public HttpResponseMessage GetAllocateloanList(allocationsanction objvalues)
        {
            loanListdetail values = new loanListdetail();
            objDaAllocationManagement.DaGetAllocateloanList(objvalues, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetViewCancelReason")]
        [HttpGet]
        public HttpResponseMessage GetViewCancelReason(string allocationdtl_gid)
        {
            visistreportcancel values = new visistreportcancel();
            objDaAllocationManagement.DaGetViewCancelReason(values, allocationdtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCaseAllocaCancelChanges")]
        [HttpGet]
        public HttpResponseMessage DaGetCaseAllocaCancelChanges()
        {
            allocationlist objmappingdtlList = new allocationlist();
            objDaAllocationManagement.DaGetCaseAllocaCancelChanges(objmappingdtlList);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }
        // Latest...//
        [ActionName("GetQualifiedAllocationSummary")]
        [HttpGet]
        public HttpResponseMessage GetQualifiedAllocationSummary()
        {
            qualifiedallocationlist objmappingdtlList = new qualifiedallocationlist();
            objDaAllocationManagement.DaGetQualifiedAllocationSummary(objmappingdtlList);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }

        [ActionName("GetQualifiedFreshAllocation")]
        [HttpGet]
        public HttpResponseMessage GetQualifiedFreshAllocation()
        {
            qualifiedallocationlist objmappingdtlList = new qualifiedallocationlist();
            objDaAllocationManagement.DaGetQualifiedFreshAllocation(objmappingdtlList);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }

        [ActionName("GetQualifiedReVisitAllocation")]
        [HttpGet]
        public HttpResponseMessage GetQualifiedReVisitAllocation()
        {
            qualifiedallocationlist objmappingdtlList = new qualifiedallocationlist();
            objDaAllocationManagement.DaGetQualifiedReVisitAllocation(objmappingdtlList);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }

        [ActionName("GetQualifiedUnmatched")]
        [HttpGet]
        public HttpResponseMessage GetQualifiedUnmatched()
        {
            qualifiedallocationlist objmappingdtlList = new qualifiedallocationlist();
            objDaAllocationManagement.DaGetQualifiedUnmatched(objmappingdtlList);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }

        [ActionName("GetBreachedAllocationSummary")]
        [HttpGet]
        public HttpResponseMessage GetBreachedAllocationSummary()
        {
            breachedlist objmappingdtlList = new breachedlist();
            objDaAllocationManagement.DaGetBreachedAllocationSummary(objmappingdtlList);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }

        [ActionName("GetCustomerGid")]
        [HttpGet]
        public HttpResponseMessage GetCustomerGid(string customer_urn)
        {
            customergid values = new customergid();
            objDaAllocationManagement.DaGetCustomerGid(customer_urn,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetHoldAllocation")]
        [HttpGet]
        public HttpResponseMessage GetHoldAllocation(string allocationdtl_gid,string allocationhold_reason)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaAllocationManagement.DaGetHoldAllocation(allocationdtl_gid, allocationhold_reason, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAllocationPendingCount")]
        [HttpGet]
        public HttpResponseMessage GetAllocationPendingCount(string zonalmapping_gid)
        {
            zonalwisecountList values = new zonalwisecountList();
            objDaAllocationManagement.DaGetAllocationPendingCount(zonalmapping_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOverallZonalPendingCount")]
        [HttpGet]
        public HttpResponseMessage GetOverallZonalPendingCount()
        {
            overallzonalcountList objvalues = new overallzonalcountList();
            objDaAllocationManagement.DaGetOverallZonalPendingCount(objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetMovetoCurrentAllocation")]
        [HttpGet]
        public HttpResponseMessage GetMovetoCurrentAllocation(string allocationdtl_gid, string allocationmove_reason)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaAllocationManagement.DaGetMovetoCurrentAllocation(allocationdtl_gid, allocationmove_reason, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAllocationReport")]
        [HttpGet]
        public HttpResponseMessage GetAllocationReport()
        {
            allocationlist objallocationlist = new allocationlist();
            objDaAllocationManagement.DaGetAllocationReport(objallocationlist);
            return Request.CreateResponse(HttpStatusCode.OK, objallocationlist);
        }
        [ActionName("GetAllocationReportExcel")]
        [HttpGet]
        public HttpResponseMessage GetAllocationReportExcel()
        {
            exportAllocation values = new exportAllocation();
            objDaAllocationManagement.DaGetAllocationReportExcel(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetAllocationReportSummaryreport")]
        [HttpPost]
        public HttpResponseMessage GetAllocationReportSummaryreport(allocationSummary values)
        {
            objDaAllocationManagement.GetAllocationReportSummaryreport(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRiskCustomerList")]
        [HttpGet]
        public HttpResponseMessage GetRiskCustomerList(string customer_gid)
        {
            Customers values = new Customers();
            objDaAllocationManagement.DaGetRiskCustomerList(values, customer_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
