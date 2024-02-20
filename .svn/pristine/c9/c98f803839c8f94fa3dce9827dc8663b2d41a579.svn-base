using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This controllers provide access for various Single fields and Mutliple events ( View, Download and Approvals) in Warehouse Master Add Page.
    /// </summary>
    /// <remarks>Written by Sherin Augusta </remarks>

    [RoutePrefix("api/AgrMstWarehouseview")]
    [Authorize]
    public class AgrMstWarehouseViewController : ApiController
    {

        DaAgrMstWarehouseView objAgrMstWarehouseView = new DaAgrMstWarehouseView();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        [ActionName("warehouseGSTView")]
        [HttpGet]
        public HttpResponseMessage warehouseGSTView(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstGST values = new MdlagrmstGST();
            objAgrMstWarehouseView.DawarehouseGSTView(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // warehouse Mobile Number View List

        [ActionName("warehouseMobileNoView")]
        [HttpGet]
        public HttpResponseMessage warehouseMobileNoView(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstMobileNo values = new MdlagrmstMobileNo();
            objAgrMstWarehouseView.DawarehouseMobileNoView(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // warehouse Email Address View List

        [ActionName("warehouseEmailAddressView")]
        [HttpGet]
        public HttpResponseMessage warehouseEmailAddressView(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstEmailAddress values = new MdlagrmstEmailAddress();
            objAgrMstWarehouseView.DawarehouseEmailAddressView(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // warehouse Address View List

        [ActionName("warehouseAddressView")]
        [HttpGet]
        public HttpResponseMessage warehouseAddressView(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstAddressDetails values = new MdlagrmstAddressDetails();
            objAgrMstWarehouseView.DawarehouseAddressView(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("warehouseSpocView")]
        [HttpGet]
        public HttpResponseMessage warehouseSpocView(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlagrmstspoc values = new Mdlagrmstspoc();
            objAgrMstWarehouseView.DaGetWarehouseSpocView(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WarehouseCommodityView")]
        [HttpGet]
        public HttpResponseMessage WarehouseCommodityView(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlWarehouseSectorcategory values = new MdlWarehouseSectorcategory();
            objAgrMstWarehouseView.DaGetWarehouseCommodityView(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

   
        [ActionName("WarehouseDocumentUploadView")]
        [HttpGet]
        public HttpResponseMessage InstitutionEditDocumentView(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAgrMstWarhouseAdd values = new MdlAgrMstWarhouseAdd();
            objAgrMstWarehouseView.DaWarehouseDocumentUploadView(warehouse_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    

        [ActionName("WarehouseAgreementDetailsView")]
        [HttpGet]
        public HttpResponseMessage WarehouseAgreementDetailsView(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAgrMstWarhouseAdd values = new MdlAgrMstWarhouseAdd();
            objAgrMstWarehouseView.DaGetWarehouseAgreementDetailsView(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getwarehoueflag")]
        [HttpGet]
        public HttpResponseMessage Getwarehoueflag(string warehouse_gid)
        {        
            objAgrMstWarehouseView.DaGetapprovalflag( warehouse_gid);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [ActionName("UpdateProductApprovalDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateProductApprovalDtl(Mdlapprovalremark values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseView.DaUpdateProductApproval( getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductApprovaldtl")]
        [HttpGet]
        public HttpResponseMessage GetProductApprovaldtl(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Warehouseapprovaldtl values = new Warehouseapprovaldtl();
            objAgrMstWarehouseView.DaGetProductApprovaldtl(warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPmgApprovaldtl")]
        [HttpGet]
        public HttpResponseMessage GetPmgApprovaldtl(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Warehouseapprovaldtl values = new Warehouseapprovaldtl();
            objAgrMstWarehouseView.DaGetPmgApprovaldtl(warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostWarehouseRaiseQuery")]
        [HttpPost]
        public HttpResponseMessage PostWarehouseRaiseQuery(mdlwarehouseraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseView.DaPostWarehouseRaiseQuery(values, getsessionvalues.user_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetWarehouseRaiseQuerySummary")]
        [HttpGet]
        public HttpResponseMessage GetWarehouseRaiseQuerySummary(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlwarehouseraisequery values = new mdlwarehouseraisequery();
            objAgrMstWarehouseView.DaGetWarehouseRaiseQuerySummary(values, warehouse_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOpenQueryStatus")]
        [HttpGet]
        public HttpResponseMessage GetOpenQueryStatus(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlwarehouseraisequery values = new mdlwarehouseraisequery();
            objAgrMstWarehouseView.DaGetOpenQueryStatus(values, warehouse_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetRaiseQuerydesc")]
        [HttpGet]
        public HttpResponseMessage GetRaiseQuerydesc(string warehouse2query_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlwarehouseraisequery values = new mdlwarehouseraisequery();
            objAgrMstWarehouseView.DaGetRaiseQuerydesc(values, warehouse2query_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("PostUpdateQueryStatus")]
        [HttpPost]
        public HttpResponseMessage PostUpdateQueryStatus(mdlwarehouseraisequery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseView.DaPostUpdateQueryStatus(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



    }
}