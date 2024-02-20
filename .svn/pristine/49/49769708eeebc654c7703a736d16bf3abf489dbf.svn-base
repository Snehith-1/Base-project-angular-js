using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

/// <summary>
/// This Controllers will provide access for various Single fields and Mutliple events (Add, Edit, View, Delete, Upload, Download and Approvals) in Warehouse Master Edit Page
/// </summary>
/// <remarks> Written by Premchander.K </remarks>

namespace ems.mastersamagro.Controllers
{
    [RoutePrefix("api/AgrMstWarehouseEdit")]
    [Authorize]
    public class AgrMstWarehouseEditController : ApiController
    {
        DaAgrMstWarehouseEdit objAgrMstWarehouseEdit = new DaAgrMstWarehouseEdit();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetWarehouseMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetWarehouseMobileNoList(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstMobileNo values = new MdlagrmstMobileNo();
            objAgrMstWarehouseEdit.DaGetWarehouseMobileNoList(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetwarehouseEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetwarehouseEmailAddressList(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstEmailAddress values = new MdlagrmstEmailAddress();
            objAgrMstWarehouseEdit.DaGetWarehouseEmailAddressList(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetWarehouseAddressList")]
        [HttpGet]
        public HttpResponseMessage GetWarehouseAddressList(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstAddressDetails values = new MdlagrmstAddressDetails();
            objAgrMstWarehouseEdit.DaGetWarehouseAddressList(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetWarehouseCommodity")]
        [HttpGet]
        public HttpResponseMessage GetWarehouseCommodity(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlWarehouseSectorcategory values = new MdlWarehouseSectorcategory();
            objAgrMstWarehouseEdit.DaGetWarehouseCommodity(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetWarehouseGSTList")]
        [HttpGet]
        public HttpResponseMessage GetWarehouseGSTList(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstGST values = new MdlagrmstGST();
            objAgrMstWarehouseEdit.DaGetWarehouseGSTList(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetWarehouseDocument")]
        [HttpGet]
        public HttpResponseMessage GetWarehouseDocument(string warehouse_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAgrMstWarhouseAdd values = new MdlAgrMstWarhouseAdd();
            objAgrMstWarehouseEdit.Dagetwarehousedocument(warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateGSTHeadOffice")]
        [HttpPost]
        public HttpResponseMessage UpdateGSTHeadOffice(MdlGSTwarehouseHeadOffice values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseEdit.DaUpdateGSTHeadOffice(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("warehouseGSTTmpList")]
        [HttpGet]
        public HttpResponseMessage warehouseGSTTmpList(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstGST values = new MdlagrmstGST();
            objAgrMstWarehouseEdit.DawarehouseGSTTmpList(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // warehouse Mobile Number Temp List

        [ActionName("warehouseMobileNoTmpList")]
        [HttpGet]
        public HttpResponseMessage warehouseMobileNoTmpList(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstMobileNo values = new MdlagrmstMobileNo();
            objAgrMstWarehouseEdit.DawarehouseMobileNoTmpList(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // warehouse Email Address Temp List

        [ActionName("warehouseEmailAddressTmpList")]
        [HttpGet]
        public HttpResponseMessage warehouseEmailAddressTmpList(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstEmailAddress values = new MdlagrmstEmailAddress();
            objAgrMstWarehouseEdit.DawarehouseEmailAddressTmpList(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // warehouse Address Temp List

        [ActionName("warehouseAddressTmpList")]
        [HttpGet]
        public HttpResponseMessage warehouseAddressTmpList(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstAddressDetails values = new MdlagrmstAddressDetails();
            objAgrMstWarehouseEdit.DawarehouseAddressTmpList(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("warehouseSpocTmpList")]
        [HttpGet]
        public HttpResponseMessage warehouseSpocTmpList(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlagrmstspoc values = new Mdlagrmstspoc();
            objAgrMstWarehouseEdit.DaGetWarehouseSpocTmpList(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WarehouseCommodityTmpList")]
        [HttpGet]
        public HttpResponseMessage WarehouseCommodityTmpList(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlWarehouseSectorcategory values = new MdlWarehouseSectorcategory();
            objAgrMstWarehouseEdit.DaGetWarehouseCommodityTmpList(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //[ActionName("WarehouseDocumentUploadTmpList")]
        //[HttpPost]
        //public HttpResponseMessage WarehouseDocumentUploadTmpList(string warehouse_gid)
        //{

        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);          
        //    MdlAgrMstWarhouseAdd values = new MdlAgrMstWarhouseAdd();
        //    objAgrMstWarehouseEdit.DaWarehouseDocumentUploadTmpList(warehouse_gid, getsessionvalues.employee_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("WarehouseDocumentUploadTmpList")]
        [HttpGet]
        public HttpResponseMessage InstitutionEditDocumentTmpList(string warehouse2agreement_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAgrMstWarhouseAdd values = new MdlAgrMstWarhouseAdd();
            objAgrMstWarehouseEdit.DaWarehouseDocumentUploadTmpList(warehouse2agreement_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatewarehouseDtl")]
        [HttpPost]
        public HttpResponseMessage UpdatewarehouseDtl(MdlAgrMstWarehouseCreation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseEdit.DaUpdatewarehouseDtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WarehouseAgreementDetailsTmpList")]
        [HttpGet]
        public HttpResponseMessage WarehouseAgreementDetailsTmpList(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAgrMstWarhouseAdd values = new MdlAgrMstWarhouseAdd();
            objAgrMstWarehouseEdit.DaGetWarehouseAgreementDetailsTmpList(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetWarehouseTmpClear")]
        [HttpGet]
        public HttpResponseMessage GetWarehouseTmpClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token); 
            objAgrMstWarehouseEdit.DaGetWarehouseTmpClear(getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}