using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to Product & PMG Approval master and corresponding approvals in warehouse masters.
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Premchander.K</remarks>

    [RoutePrefix("api/AgrMstProductPmgApproval")]
    [Authorize]

    public class AgrMstProductPmgApprovalController : ApiController
    {
        DaAgrMstProductPmgApproval objDaAgrMstProductPmgApproval = new DaAgrMstProductPmgApproval();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetPmgApproval")]
        [HttpGet]
        public HttpResponseMessage GetPmgApproval()
        {
            PmgApprovallist objvalues = new PmgApprovallist();
            objDaAgrMstProductPmgApproval.DaGetPmgApproval(objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("CreatePmgApproval")]
        [HttpPost]
        public HttpResponseMessage CreatePmgApproval(PmgApprovaldtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstProductPmgApproval.DaCreatePmgApproval(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditPmgApproval")]
        [HttpGet]
        public HttpResponseMessage EditPmgApproval(string mstpmgapproval_gid)
        {
            PmgApprovaldtl values = new PmgApprovaldtl();
            objDaAgrMstProductPmgApproval.DaEditPmgApproval(mstpmgapproval_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatePmgApproval")]
        [HttpPost]
        public HttpResponseMessage UpdatePmgApproval(PmgApprovaldtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstProductPmgApproval.DaUpdatePmgApproval(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeletePmgApproval")]
        [HttpGet]
        public HttpResponseMessage DeletePmgApproval(string mstpmgapproval_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstProductPmgApproval.DaDeletePmgApproval(mstpmgapproval_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductApproval")]
        [HttpGet]
        public HttpResponseMessage GetProductApproval()
        {
            ProductApprovallist objvalues = new ProductApprovallist();
            objDaAgrMstProductPmgApproval.DaGetProductApproval(objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }


        [ActionName("CreateProductApproval")]
        [HttpPost]
        public HttpResponseMessage CreateProductApproval(ProductApprovaldtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstProductPmgApproval.DaCreateProductApproval(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditProductApproval")]
        [HttpGet]
        public HttpResponseMessage EditProductApproval(string mstproductapproval_gid)
        {
            ProductApprovaldtl values = new ProductApprovaldtl();
            objDaAgrMstProductPmgApproval.DaEditProductApproval(mstproductapproval_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateProductApproval")]
        [HttpPost]
        public HttpResponseMessage UpdateProductApproval(ProductApprovaldtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstProductPmgApproval.DaUpdateProductApproval(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteProductApproval")]
        [HttpGet]
        public HttpResponseMessage DeleteProductApproval(string mstproductapproval_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstProductPmgApproval.DaDeleteProductApproval(mstproductapproval_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPendingProductApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetPendingProductApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            PendingProductApprovallist values = new PendingProductApprovallist();
            objDaAgrMstProductPmgApproval.DaGetPendingProductApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            PendingProductApprovallist values = new PendingProductApprovallist();
            objDaAgrMstProductPmgApproval.DaGetProductApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPendingPMGApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetPendingPMGApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            PendingProductApprovallist values = new PendingProductApprovallist();
            objDaAgrMstProductPmgApproval.DaGetPendingPMGApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
         
        [ActionName("GetRejectedApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetRejectedApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            PendingProductApprovallist values = new PendingProductApprovallist();
            objDaAgrMstProductPmgApproval.DaGetRejectedApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovedwarehouseSummary")]
        [HttpGet]
        public HttpResponseMessage GetApprovedwarehouseSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            PendingProductApprovallist values = new PendingProductApprovallist();
            objDaAgrMstProductPmgApproval.DaGetApprovedwarehouseSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetWarehouseApprovalCount")]
        [HttpGet]
        public HttpResponseMessage GetWarehouseApprovalCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            WarehouseCountdtl values = new WarehouseCountdtl();
            objDaAgrMstProductPmgApproval.DaGetWarehouseApprovalCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductbasedWarehouseApprovalCount")]
        [HttpGet]
        public HttpResponseMessage GetProductbasedWarehouseApprovalCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            WarehouseCountdtl values = new WarehouseCountdtl();
            objDaAgrMstProductPmgApproval.DaGetProductbasedWarehouseApprovalCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPmgbasedWarehouseApprovalCount")]
        [HttpGet]
        public HttpResponseMessage GetPmgbasedWarehouseApprovalCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            WarehouseCountdtl values = new WarehouseCountdtl();
            objDaAgrMstProductPmgApproval.DaGetPmgbasedWarehouseApprovalCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetUpcomingApprovalSummary")]
        [HttpGet]
        public HttpResponseMessage GetUpcomingApprovalSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            PendingProductApprovallist values = new PendingProductApprovallist();
            objDaAgrMstProductPmgApproval.DaGetUpcomingApprovalSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}