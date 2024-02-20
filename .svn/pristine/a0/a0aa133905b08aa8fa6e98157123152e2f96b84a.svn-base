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
/// This controllers provide access for various Single fields and Mutliple events (Add, Edit, View, Delete, Upload, Download and Approvals) in Warehouse Master Add Page.
/// </summary>
/// <remarks>Written by Premchander.K </remarks>

namespace ems.mastersamagro.Controllers
{
    [RoutePrefix("api/AgrMstWarehouseAdd")]
    [Authorize]
    public class AgrMstWarehouseAddController : ApiController
    {
        DaAgrMstWarehouseAdd objAgrMstWarehouseAdd = new DaAgrMstWarehouseAdd();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();



        //Add Warehouse Address Details  

        [ActionName("PostWarehouseAddressDetail")]
        [HttpPost]
        public HttpResponseMessage PostWarehouseAddressDetail(MdlagrmstAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseAdd.DaPostWarehouseAddressDetail(getsessionvalues.employee_gid, getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Warehouse Address List

        [ActionName("GetWarehouseAddressList")]
        [HttpGet]
        public HttpResponseMessage GetWarehouseAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstAddressDetails values = new MdlagrmstAddressDetails();
            objAgrMstWarehouseAdd.DaGetWarehouseAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Warehouse Address Details 

        [ActionName("EditWarehouseAddressDetail")]
        [HttpGet]
        public HttpResponseMessage EditWarehouseAddressDetail(string Warehouse2address_gid)
        {
            MdlagrmstAddressDetails values = new MdlagrmstAddressDetails();
            objAgrMstWarehouseAdd.DaEditWarehouseAddressDetail(Warehouse2address_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Warehouse Address Details 

        [ActionName("UpdateWarehouseAddressDetail")]
        [HttpPost]
        public HttpResponseMessage UpdateWarehouseAddressDetail(MdlagrmstAddressDetails values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseAdd.DaUpdateWarehouseAddressDetail(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Warehouse Address Details 

        [ActionName("DeleteWarehouseAddressDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteWarehouseAddressDetail(string Warehouse2address_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstAddressDetails values = new MdlagrmstAddressDetails();
            objAgrMstWarehouseAdd.DaDeleteWarehouseAddressDetail(Warehouse2address_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add warehouse Email Address

        [ActionName("PostWarehouseEmailAddress")]
        [HttpPost]
        public HttpResponseMessage PostWarehouseEmailAddress(MdlagrmstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseAdd.DaPostWarehouseEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get warehouse Email Address List

        [ActionName("GetwarehouseEmailAddressList")]
        [HttpGet]
        public HttpResponseMessage GetwarehouseEmailAddressList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstEmailAddress values = new MdlagrmstEmailAddress();
            objAgrMstWarehouseAdd.DaGetWarehouseEmailAddressList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit warehouse Email Address

        [ActionName("EditwarehouseEmailAddress")]
        [HttpGet]
        public HttpResponseMessage EditwarehouseEmailAddress(string warehouse2email_gid)
        {
            MdlagrmstEmailAddress values = new MdlagrmstEmailAddress();
            objAgrMstWarehouseAdd.DaEditWarehouseEmailAddress(warehouse2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update warehouse Email Address

        [ActionName("UpdatewarehouseEmailAddress")]
        [HttpPost]
        public HttpResponseMessage UpdatewarehouseEmailAddress(MdlagrmstEmailAddress values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseAdd.DaUpdateWarehouseEmailAddress(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete warehouse Email Address

        [ActionName("DeletewarehouseEmailAddress")]
        [HttpGet]
        public HttpResponseMessage DeletewarehouseEmailAddress(string warehouse2email_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstEmailAddress values = new MdlagrmstEmailAddress();
            objAgrMstWarehouseAdd.DaDeleteWarehouseEmailAddress(warehouse2email_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Add Warehouse Mobile No

        [ActionName("PostWarehouseMobileNo")]
        [HttpPost]
        public HttpResponseMessage PostWarehouseMobileNo(MdlagrmstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseAdd.DaPostWarehouseMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Warehouse Mobile No

        [ActionName("GetWarehouseMobileNoList")]
        [HttpGet]
        public HttpResponseMessage GetWarehouseMobileNoList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstMobileNo values = new MdlagrmstMobileNo();
            objAgrMstWarehouseAdd.DaGetWarehouseMobileNoList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Warehouse Mobile No

        [ActionName("EditWarehouseMobileNo")]
        [HttpGet]
        public HttpResponseMessage EditWarehouseMobileNo(string warehouse2mobileno_gid)
        {
            MdlagrmstMobileNo values = new MdlagrmstMobileNo();
            objAgrMstWarehouseAdd.DaEditWarehouseMobileNo(warehouse2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Update Warehouse Mobile No

        [ActionName("UpdateWarehouseMobileNo")]
        [HttpPost]
        public HttpResponseMessage UpdateWarehouseMobileNo(MdlagrmstMobileNo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseAdd.DaUpdateWarehouseMobileNo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Warehouse Mobile No

        [ActionName("DeleteWarehouseMobileNo")]
        [HttpGet]
        public HttpResponseMessage DeleteWarehouseMobileNo(string warehouse2mobileno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstMobileNo values = new MdlagrmstMobileNo();
            objAgrMstWarehouseAdd.DaDeleteWarehouseMobileNo(warehouse2mobileno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WarehouseDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage WarehouseDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            MdlAgrMstWarhouseAdd values = new MdlAgrMstWarhouseAdd();
            objAgrMstWarehouseAdd.Dawarehousedocumentupload(httpRequest, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("warehousedoc_delete")]
        [HttpGet]
        public HttpResponseMessage warehousedoc_delete(string warehouse2docupload_gid, string warehouse2agreement_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAgrMstWarhouseAdd values = new MdlAgrMstWarhouseAdd();
            objAgrMstWarehouseAdd.Dagetwarehousedoc_delete(warehouse2docupload_gid, warehouse2agreement_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetWarehouseDocument")]
        [HttpGet]
        public HttpResponseMessage GetWarehouseDocument(string warehouse_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAgrMstWarhouseAdd values = new MdlAgrMstWarhouseAdd();
            objAgrMstWarehouseAdd.Dagetwarehousedocument(warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostWarehouseGST")]
        [HttpPost]
        public HttpResponseMessage PostWarehouseGST(MdlagrmstGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseAdd.DaPostWarehouseGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostWarehouseGSTList")]
        [HttpPost]
        public HttpResponseMessage PostWarehouseGSTList(MdlagrmstGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseAdd.DaPostWarehouseGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get Warehouse GST List

        [ActionName("GetWarehouseGSTList")]
        [HttpGet]
        public HttpResponseMessage GetWarehouseGSTList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstGST values = new MdlagrmstGST();
            objAgrMstWarehouseAdd.DaGetWarehouseGSTList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit Warehouse GST

        [ActionName("EditWarehouseGST")]
        [HttpGet]
        public HttpResponseMessage EditWarehouseGST(string Warehouse2branch_gid)
        {
            MdlagrmstGST values = new MdlagrmstGST();
            objAgrMstWarehouseAdd.DaEditWarehouseGST(Warehouse2branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update Warehouse GST

        [ActionName("UpdateWarehouseGST")]
        [HttpPost]
        public HttpResponseMessage UpdateWarehouseGST(MdlagrmstGST values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseAdd.DaUpdateWarehouseGST(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Warehouse GST

        [ActionName("DeleteWarehouseGST")]
        [HttpGet]
        public HttpResponseMessage DeleteWarehouseGST(string warehouse2branch_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstGST values = new MdlagrmstGST();
            objAgrMstWarehouseAdd.DaDeleteWarehouseGST(warehouse2branch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PostWarehouseCommodity")]
        [HttpPost]
        public HttpResponseMessage PostWarehouseCommodity(Warehousevarietyname_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseAdd.DaPostWarehouseCommodity(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Get 

        [ActionName("GetWarehouseCommodity")]
        [HttpGet]
        public HttpResponseMessage GetWarehouseCommodity()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlWarehouseSectorcategory values = new MdlWarehouseSectorcategory();
            objAgrMstWarehouseAdd.DaGetWarehouseCommodity(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Edit 

        [ActionName("EditWarehouseCommodity")]
        [HttpGet]
        public HttpResponseMessage EditWarehouseCommodity(string warehouse_gid)
        {
            Warehousevarietyname_list values = new Warehousevarietyname_list();
            objAgrMstWarehouseAdd.DaEditWarehouseCommodity(warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Update 

        [ActionName("UpdateWarehouseCommodity")]
        [HttpPost]
        public HttpResponseMessage UpdateWarehouseCommodity(Warehousevarietyname_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseAdd.DaUpdateWarehouseCommodity(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete

        [ActionName("DeleteWarehouseCommodity")]
        [HttpGet]
        public HttpResponseMessage DeleteWarehouseCommodity(string warehouse2commodity_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlWarehouseSectorcategory values = new MdlWarehouseSectorcategory();
            objAgrMstWarehouseAdd.DaDeleteWarehouseCommodity(warehouse2commodity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //spoc id 2 phone no

        //[ActionName("Getspocno")]
        //[HttpPost]
        //public HttpResponseMessage Getspocno(MdlAgrMstWarhouseAdd values)
        //{
        //    //string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    //getsessionvalues = Objgetgid.gettokenvalues(token);
        //    //spoc_id_2_phone_no values = new spoc_id_2_phone_no();
        //    objAgrMstWarehouseAdd.DaGetspocno(values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}


        [ActionName("Getspocno")]
        [HttpPost]
        public HttpResponseMessage Getspocno(spocid_list values)
        {
            //string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            //getsessionvalues = Objgetgid.gettokenvalues(token);
            //spoc_id_2_phone_no values = new spoc_id_2_phone_no();
            objAgrMstWarehouseAdd.DaGetspocno( values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //Post Spoc 

        [ActionName("PostWarehouseSpocDetails")]
        [HttpPost]
        public HttpResponseMessage PostWarehouseSpocDetails(Mdlagrmstspoc values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseAdd.DaPostWarehouseSpocDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetWarehouseSpocDetails")]
        [HttpGet]
        public HttpResponseMessage GetWarehouseSpocDetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlagrmstspoc values = new Mdlagrmstspoc();
            objAgrMstWarehouseAdd.DaGetWarehouseSpocDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditWarehouseSpocDetails")]
        [HttpGet]
        public HttpResponseMessage EditWarehouseSpocDetails(string warehouse2spoc_gid)
        {
            Mdlagrmstspoc values = new Mdlagrmstspoc();
            objAgrMstWarehouseAdd.DaEditWarehouseSpocDetails(warehouse2spoc_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostWarehouseSubmit")]
        [HttpPost]
        public HttpResponseMessage PostWarehouseSubmit(MdlAgrMstWarehouseCreation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseAdd.DaPostWarehouseSubmit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetNewWarehouseSummary")]
        [HttpGet]
        public HttpResponseMessage GetNewWarehouseSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAgrMstWarhouseAdd values = new MdlAgrMstWarhouseAdd();
            objAgrMstWarehouseAdd.DaGetNewWarehouseSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditWarehouseDetails")]
        [HttpGet]
        public HttpResponseMessage EditWarehouseDetails(string warehouse_gid)
        {
            MdlAgrMstWarehouseCreation values = new MdlAgrMstWarehouseCreation();
            objAgrMstWarehouseAdd.DaEditWarehouseDetails(warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteWarehousespoc")]
        [HttpGet]
        public HttpResponseMessage DeleteWarehousespoc(string warehouse2spoc_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlagrmstspoc values = new Mdlagrmstspoc();
            objAgrMstWarehouseAdd.DaDeleteWarehousespoc(warehouse2spoc_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostWarehouseAgreementDetails")]
        [HttpPost]
        public HttpResponseMessage PostWarehouseAgreementDetails(Mdlagrmstagreementdtllist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseAdd.DaPostWarehouseAgreementDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetWarehouseAgreementDetails")]
        [HttpGet]
        public HttpResponseMessage GetWarehouseAgreementDetails()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlAgrMstWarhouseAdd values = new MdlAgrMstWarhouseAdd();
            objAgrMstWarehouseAdd.DaGetWarehouseAgreementDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetWarehouseProductcommodity")]
        [HttpGet]
        public HttpResponseMessage GetCluster2BaseLocation(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlWarehouseSectorcategory values = new MdlWarehouseSectorcategory();
            objAgrMstWarehouseAdd.DaGetWarehouseProductcommodity(warehouse_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetApprovalPendingWarehouseSummary")]
        [HttpGet]
        public HttpResponseMessage GetApprovalPendingWarehouseSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlWarehouseSummary values = new MdlWarehouseSummary();
            objAgrMstWarehouseAdd.DaGetApprovalPendingWarehouseSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("DaGetApprovedWarehouseSummary")]
        [HttpGet]
        public HttpResponseMessage DaGetApprovedWarehouseSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlWarehouseSummary values = new MdlWarehouseSummary();
            objAgrMstWarehouseAdd.DaGetApprovedWarehouseSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRejectedWarehouseSummary")]
        [HttpGet]
        public HttpResponseMessage GetRejectedWarehouseSummary()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlWarehouseSummary values = new MdlWarehouseSummary();
            objAgrMstWarehouseAdd.DaGetRejectedWarehouseSummary(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetWarehouseCount")]
        [HttpGet]
        public HttpResponseMessage GetWarehouseCount()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            WarehouseCountdtl values = new WarehouseCountdtl();
            objAgrMstWarehouseAdd.DaGetWarehouseCount(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAgreementDetail")]
        [HttpGet]
        public HttpResponseMessage DeleteAgreementDetail(string warehouse2agreement_gid)
        {
            result values = new result();
            objAgrMstWarehouseAdd.DaDeleteAgreementDetail(warehouse2agreement_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WarehouseFacilityEdit")]
        [HttpGet]
        public HttpResponseMessage WarehouseFacilityEdit(string warehouse_gid)
        { 
            MdlAgrMstWarehouseCreation values = new MdlAgrMstWarehouseCreation();
            objAgrMstWarehouseAdd.DaWarehouseFacilityEdit(warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSpocEmployee")]
        [HttpGet]
        public HttpResponseMessage GetSpocEmployee(string warehouse_gid)
        {

            MdlEmployeeList values = new Models.MdlEmployeeList();
            objAgrMstWarehouseAdd.DaGetSpocEmployee(values, warehouse_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTmpSpocEmployee")]
        [HttpGet]
        public HttpResponseMessage GetTmpSpocEmployee(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlEmployeeList values = new Models.MdlEmployeeList();
            objAgrMstWarehouseAdd.DaGetTmpSpocEmployee(values, getsessionvalues.employee_gid, warehouse_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Gettypeofwarehouse")]
        [HttpGet]
        public HttpResponseMessage Gettypeofwarehouse()
        {
            MdlAgrMstWarhouseAdd values = new MdlAgrMstWarhouseAdd();
            objAgrMstWarehouseAdd.DaGettypeofwarehouse(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteGSTWarehouse")]
        [HttpGet]
        public HttpResponseMessage DeleteGSTWarehouse(string warehouse_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlagrmstGST values = new MdlagrmstGST();
            objAgrMstWarehouseAdd.DaDeleteGSTWarehouse(getsessionvalues.employee_gid, warehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateGSTHeadOffice")]
        [HttpPost]
        public HttpResponseMessage UpdateGSTHeadOffice(MdlGSTwarehouseHeadOffice values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objAgrMstWarehouseAdd.DaUpdateGSTHeadOffice(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}