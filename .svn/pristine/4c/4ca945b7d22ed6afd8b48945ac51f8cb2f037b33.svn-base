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
    [RoutePrefix("api/allocationTransfer")]
    [Authorize]

    public class allocationTransferController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAllocationTransfer objDaAllocationTransfer = new DaAllocationTransfer();

        [ActionName("getallocateddetails")]
        [HttpGet]
        public HttpResponseMessage GetAllocation()
        {
            mappingdtlList objmappingdtl = new mappingdtlList();
            objDaAllocationTransfer.DaGetAllocation(objmappingdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtl);
        }

        [ActionName("posttransferAllcoation")]
        [HttpPost]
        public HttpResponseMessage PostAllocationTransfer(allocationtransferdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAllocationTransfer.DaPostAllocationTransfer(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("gettransferSummary")]
        [HttpGet]
        public HttpResponseMessage GetTransferSummary()
        {
            allocationtransferList objallocationtransferdtl = new allocationtransferList();
            objDaAllocationTransfer.DaGetTransferSummary(objallocationtransferdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objallocationtransferdtl);
        }

        [ActionName("gettransferApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetTransferApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            allocationtransferList objallocationtransferdtl = new allocationtransferList();
            objDaAllocationTransfer.DaGetTransferApprovalSummary(getsessionvalues.employee_gid, objallocationtransferdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objallocationtransferdtl);
        }

        [ActionName("posttransferFromApprove")]
        [HttpPost]
        public HttpResponseMessage PostTransferFromApprove(transferapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAllocationTransfer.DaPostTransferFromApprove(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("postransferToApprove")]
        [HttpPost]
        public HttpResponseMessage PostTransferToApprove(transferapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAllocationTransfer.DaPostTransferToApprove(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("posttransferFromReject")]
        [HttpPost]
        public HttpResponseMessage PostTransferFromReject(transferapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAllocationTransfer.DaPostTransferFromReject(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("posttransferToReject")]
        [HttpPost]
        public HttpResponseMessage PostTransferToReject(transferapproval values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAllocationTransfer.DaPostTransferToReject(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getApprovalHistorySummary")]
        [HttpGet]
        public HttpResponseMessage GetApprovalHistorySummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            allocationtransferList objallocationtransferdtl = new allocationtransferList();
            objDaAllocationTransfer.DaGetApprovalHistorySummary(getsessionvalues.employee_gid, objallocationtransferdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objallocationtransferdtl);
        }

        [ActionName("getviewtransapprovaldtl")]
        [HttpGet]
        public HttpResponseMessage GetViewTransferApprovalDetails(string allocation_transfergid)
        {
            viewtransferdtl objvalues = new viewtransferdtl();
            objDaAllocationTransfer.DaGetViewTransferApprovalDetails(allocation_transfergid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("getallocatedistritdtl")]
        [HttpGet]
        public HttpResponseMessage getallocatedistritdtl(string state_gid)
        {
            statedtlList objvalues = new statedtlList();
            objDaAllocationTransfer.DaGetAllocateDistritDtl(state_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("getdistrictallocateRM")]
        [HttpGet]
        public HttpResponseMessage getdistrictallocateRM(string district_gid)
        {
            allocateRMdtl objvalues = new allocateRMdtl();
            objDaAllocationTransfer.DaGetDistrictAllocateRM(district_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }
    }
}
