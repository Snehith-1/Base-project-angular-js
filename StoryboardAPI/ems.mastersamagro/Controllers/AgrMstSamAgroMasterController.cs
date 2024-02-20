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
    /// This Controllers will provide access to various masters in samagro and their summary, add, edit, view, active, inactive & delete records (Samagro Master includes Scope, Other creditor Applicant type, Milestone Payment, Sector classification, type of warehouse, Buyer/Supplier Type, Product desk mapping & Insurane company).
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Kalaiarsan, Praveen Raj.R</remarks>

    [RoutePrefix("api/AgrMstSamAgroMaster")]
    [Authorize]
    public class AgrMstSamAgroMasterController : ApiController
    {

        DaAgrMstSamAgroMaster objDaAgrMstSamAgroMaster = new DaAgrMstSamAgroMaster();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        // Product Desk Mapping ---- STARTING
        [ActionName("PostProductDeskAdd")]
        [HttpPost]
        public HttpResponseMessage PostProductDeskAdd(MdlProductDesk values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaPostProductDeskAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductDeskSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductDeskSummary()
        {
            MdlProductDesk objmaster = new MdlProductDesk();
            objDaAgrMstSamAgroMaster.DaGetProductDeskSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetProductsNameSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductsNameSummary()
        {
            MdlProductDesk objmaster = new MdlProductDesk();
            objDaAgrMstSamAgroMaster.DaGetProductsNameSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetProductDeskEdit")]
        [HttpGet]
        public HttpResponseMessage GetProductDeskEdit(string productdesk_gid)
        {
            MdlProductDesk objmaster = new MdlProductDesk();
            objDaAgrMstSamAgroMaster.DaGetProductDeskEdit(productdesk_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostProductDeskUpdate")]
        [HttpPost]
        public HttpResponseMessage PostProductDeskUpdate(MdlProductDesk values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaPostProductDeskUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetProductDeskDetails")]
        [HttpGet]
        public HttpResponseMessage GetProductDeskDetails(string productdesk_gid)
        {
            ProductDeskDetails values = new ProductDeskDetails();
            objDaAgrMstSamAgroMaster.DaGetProductDeskDetails(productdesk_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostProductDeskInactive")]
        [HttpPost]
        public HttpResponseMessage PostProductDeskInactive(ProductDesk values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaPostProductDeskInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductDeskInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetProductDeskInactiveLogview(string productdesk_gid)
        {
            MdlProductDesk values = new MdlProductDesk();
            objDaAgrMstSamAgroMaster.DaGetProductDeskInactiveLogview(productdesk_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Product Desk Mapping ---- ENDING

        //typeofsupplynature
        [ActionName("Gettypeofsupplynature")]
        [HttpGet]
        public HttpResponseMessage Gettypeofsupplynature()
        {
            MdlAgrMstSamAgroMaster objagroapplication360 = new MdlAgrMstSamAgroMaster();
            objDaAgrMstSamAgroMaster.DaGettypeofsupplynature(objagroapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objagroapplication360);
        }

        [ActionName("Createtypeofsupplynature")]
        [HttpPost]
        public HttpResponseMessage Createtypeofsupplynature(applicationmst values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaCreatetypeofsupplynature(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Edittypeofsupplynature")]
        [HttpGet]
        public HttpResponseMessage Edittypeofsupplynature(string typeofsupplynature_gid)
        {
            applicationmst values = new applicationmst();
            objDaAgrMstSamAgroMaster.DaEdittypeofsupplynature(typeofsupplynature_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Updatetypeofsupplynature")]
        [HttpPost]
        public HttpResponseMessage Updatetypeofsupplynature(applicationmst values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaUpdatetypeofsupplynature(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Inactivetypeofsupplynature")]
        [HttpPost]
        public HttpResponseMessage Inactivetypeofsupplynature(applicationmst values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaInactivetypeofsupplynature(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Deletetypeofsupplynature")]
        [HttpGet]
        public HttpResponseMessage Deletetypeofsupplynature(string typeofsupplynature_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaDeletetypeofsupplynature(typeofsupplynature_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("typeofsupplynatureInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage typeofsupplynatureInactiveLogview(string typeofsupplynature_gid)
        {
            MdlAgrMstSamAgroMaster values = new MdlAgrMstSamAgroMaster();
            objDaAgrMstSamAgroMaster.DatypeofsupplynatureInactiveLogview(typeofsupplynature_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactivetypeofsupplynatureHistory")]
        [HttpGet]
        public HttpResponseMessage InactivetypeofsupplynatureHistory(string typeofsupplynature_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstSamAgroMaster.InactivetypeofsupplynatureHistory(objapplicationhistory, typeofsupplynature_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }
        [ActionName("typeofsupplynatureList")]
        [HttpGet]
        public HttpResponseMessage typeofsupplynatureList()
        {
            MdlAgrMstSamAgroMaster values = new MdlAgrMstSamAgroMaster();
            objDaAgrMstSamAgroMaster.DatypeofsupplynatureList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //sectorclassification

        [ActionName("Getsectorclassification")]
        [HttpGet]
        public HttpResponseMessage Getsectorclassification()
        {
            MdlAgrMstSamAgroMaster objagroapplication360 = new MdlAgrMstSamAgroMaster();
            objDaAgrMstSamAgroMaster.DaGetsectorclassification(objagroapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objagroapplication360);
        }

        [ActionName("Createsectorclassification")]
        [HttpPost]
        public HttpResponseMessage Createsectorclassification(applicationmst values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaCreatesectorclassification(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Editsectorclassification")]
        [HttpGet]
        public HttpResponseMessage Editsectorclassification(string sectorclassification_gid)
        {
            applicationmst values = new applicationmst();
            objDaAgrMstSamAgroMaster.DaEditsectorclassification(sectorclassification_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Updatesectorclassification")]
        [HttpPost]
        public HttpResponseMessage Updatesectorclassification(applicationmst values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaUpdatesectorclassification(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Inactivesectorclassification")]
        [HttpPost]
        public HttpResponseMessage Inactivesectorclassification(applicationmst values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaInactivesectorclassification(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Deletesectorclassification")]
        [HttpGet]
        public HttpResponseMessage Deletesectorclassification(string sectorclassification_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaDeletesectorclassification(sectorclassification_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("sectorclassificationInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage sectorclassificationInactiveLogview(string sectorclassification_gid)
        {
            MdlAgrMstSamAgroMaster values = new MdlAgrMstSamAgroMaster();
            objDaAgrMstSamAgroMaster.DasectorclassificationInactiveLogview(sectorclassification_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactivesectorclassificationHistory")]
        [HttpGet]
        public HttpResponseMessage InactivesectorclassificationHistory(string sectorclassification_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstSamAgroMaster.InactivesectorclassificationHistory(objapplicationhistory, sectorclassification_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }
        [ActionName("sectorclassificationList")]
        [HttpGet]
        public HttpResponseMessage sectorclassificationList()
        {
            MdlAgrMstSamAgroMaster values = new MdlAgrMstSamAgroMaster();
            objDaAgrMstSamAgroMaster.DasectorclassificationList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //typeofwarehouse

        [ActionName("Gettypeofwarehouse")]
        [HttpGet]
        public HttpResponseMessage Gettypeofwarehouse()
        {
            MdlAgrMstSamAgroMaster objagroapplication360 = new MdlAgrMstSamAgroMaster();
            objDaAgrMstSamAgroMaster.DaGettypeofwarehouse(objagroapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objagroapplication360);
        }

        [ActionName("Createtypeofwarehouse")]
        [HttpPost]
        public HttpResponseMessage Createtypeofwarehouse(applicationmst values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaCreatetypeofwarehouse(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Edittypeofwarehouse")]
        [HttpGet]
        public HttpResponseMessage Edittypeofwarehouse(string typeofwarehouse_gid)
        {
            applicationmst values = new applicationmst();
            objDaAgrMstSamAgroMaster.DaEdittypeofwarehouse(typeofwarehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Updatetypeofwarehouse")]
        [HttpPost]
        public HttpResponseMessage Updatetypeofwarehouse(applicationmst values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaUpdatetypeofwarehouse(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Inactivetypeofwarehouse")]
        [HttpPost]
        public HttpResponseMessage Inactivetypeofwarehouse(applicationmst values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaInactivetypeofwarehouse(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Deletetypeofwarehouse")]
        [HttpGet]
        public HttpResponseMessage Deletetypeofwarehouse(string typeofwarehouse_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaDeletetypeofwarehouse(typeofwarehouse_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("typeofwarehouseInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage typeofwarehouseInactiveLogview(string typeofwarehouse_gid)
        {
            MdlAgrMstSamAgroMaster values = new MdlAgrMstSamAgroMaster();
            objDaAgrMstSamAgroMaster.DatypeofwarehouseInactiveLogview(typeofwarehouse_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactivetypeofwarehouseHistory")]
        [HttpGet]
        public HttpResponseMessage InactivetypeofwarehouseHistory(string typeofwarehouse_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstSamAgroMaster.InactivetypeofwarehouseHistory(objapplicationhistory, typeofwarehouse_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }
        [ActionName("typeofwarehouseList")]
        [HttpGet]
        public HttpResponseMessage typeofwarehouseList()
        {
            MdlAgrMstSamAgroMaster values = new MdlAgrMstSamAgroMaster();
            objDaAgrMstSamAgroMaster.DatypeofwarehouseList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Product / Commodity Master changes

        [ActionName("GettypeofsupplynatureDropdown")]
        [HttpGet]
        public HttpResponseMessage GettypeofsupplynatureDropdown()
        {
            Mdltypeofsupplynature objvalues = new Mdltypeofsupplynature();
            objDaAgrMstSamAgroMaster.GettypeofsupplynatureDropdown(objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        } 

        [ActionName("GetsectorclassificationDropdown")]
        [HttpGet]
        public HttpResponseMessage GetsectorclassificationDropdown()
        {
            Mdlsectorclassification objvalues = new Mdlsectorclassification();
            objDaAgrMstSamAgroMaster.DaGetsectorclassificationDropdown(objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetinsurancecompanyDropdown")]
        [HttpGet]
        public HttpResponseMessage GetinsurancecompanyDropdown()
        {
            Mdlinsurancecompany objvalues = new Mdlinsurancecompany();
            objDaAgrMstSamAgroMaster.DaGetinsurancecompanyDropdown(objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }


        [ActionName("GetinsurancePolicyDropdown")]
        [HttpGet]
        public HttpResponseMessage GetinsurancePolicyDropdown(string insurancecompany_gid)
        {
            MdlinsurancePolicy objvalues = new MdlinsurancePolicy();
            objDaAgrMstSamAgroMaster.DaGetinsurancePolicyDropdown(insurancecompany_gid,objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetMstProductDropdown")]
        [HttpGet]
        public HttpResponseMessage GetMstProductDropdown()
        {
            MdlMstProductDropDown objvalues = new MdlMstProductDropDown();
            objDaAgrMstSamAgroMaster.DaGetMstProductDropdown(objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }
         
        [ActionName("GetMstSubProductDropdown")]
        [HttpGet]
        public HttpResponseMessage GetMstSubProductDropdown(string loanproduct_gid)
        {
            MdlMstSubProductDropDown objvalues = new MdlMstSubProductDropDown();
            objDaAgrMstSamAgroMaster.DaGetMstSubProductDropdown(loanproduct_gid,objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }
        // Commodity GST Add Event

        [ActionName("CreateCommodityGst")]
        [HttpPost]
        public HttpResponseMessage CreateCommodityGst(commoditygststatus values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaCreateCommodityGst(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCommodityGstList")]
        [HttpGet]
        public HttpResponseMessage GetCommodityGstList(string variety_gid)
        {
            commoditygststatuslist values = new commoditygststatuslist();
            objDaAgrMstSamAgroMaster.DaGetCommodityGstList(variety_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCommodityGst")]
        [HttpGet]
        public HttpResponseMessage DeleteCommodityGst(string commoditygststatus_gid)
        {
            result values = new result();
            objDaAgrMstSamAgroMaster.DaDeleteCommodityGst(commoditygststatus_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Commodity - Trade Product Details

        [ActionName("CreateCommodityTradeProdct")]
        [HttpPost]
        public HttpResponseMessage CreateCommodityTradeProdct(commodityTradeProdct values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaCreateCommodityTradeProdct(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCommodityTradeProdctList")]
        [HttpGet]
        public HttpResponseMessage GetCommodityTradeProdctList(string variety_gid)
        {
            commodityTradeProdctlist values = new commodityTradeProdctlist();
            objDaAgrMstSamAgroMaster.DaGetCommodityTradeProdctList(variety_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCommodityTradeProdct")]
        [HttpGet]
        public HttpResponseMessage DeleteCommodityTradeProdct(string commoditytradeproductdtl_gid)
        {
            result values = new result();
            objDaAgrMstSamAgroMaster.DaDeleteCommodityTradeProdct(commoditytradeproductdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Commodity - Document Upload

        [ActionName("CreateCommodityDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage CreateCommodityDocumentUpload()
        {
            HttpRequest httpRequest; 
            httpRequest = HttpContext.Current.Request;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            commodityDocumentUpload values = new commodityDocumentUpload();
            objDaAgrMstSamAgroMaster.DaCreateCommodityDocumentUpload(httpRequest,values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCommodityDocumentUploadList")]
        [HttpGet]
        public HttpResponseMessage GetCommodityDocumentUploadList(string variety_gid)
        {
            commodityDocumentUploadlist values = new commodityDocumentUploadlist();
            objDaAgrMstSamAgroMaster.DaGetCommodityDocumentUploadList(variety_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCommodityDocumentUpload")]
        [HttpGet]
        public HttpResponseMessage DeleteCommodityDocumentUpload(string commoditydocument_gid)
        {
            result values = new result();
            objDaAgrMstSamAgroMaster.DaDeleteCommodityDocumentUpload(commoditydocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Insurance Company Master

        [ActionName("GetInsuranceCompanySummary")]
        [HttpGet]
        public HttpResponseMessage GetInsuranceCompanySummary()
        {
            MdlInsuranceCompany objMdlInsuranceCompany = new MdlInsuranceCompany();
            objDaAgrMstSamAgroMaster.DaGetInsuranceCompanySummary(objMdlInsuranceCompany);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlInsuranceCompany);
        }

        [ActionName("PostPolicyAdd")]
        [HttpPost]
        public HttpResponseMessage PostPolicyAdd(MdlPolicy values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaPostPolicyAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPolicyList")]
        [HttpGet]
        public HttpResponseMessage GetPolicyList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPolicy values = new MdlPolicy();
            objDaAgrMstSamAgroMaster.DaGetPolicyList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeletePolicy")]
        [HttpGet]
        public HttpResponseMessage DeletePolicy(string insurancecompany2policy_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPolicy values = new MdlPolicy();
            objDaAgrMstSamAgroMaster.DaDeletePolicy(insurancecompany2policy_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PolicyDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage PolicyDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            MdlPolicy values = new MdlPolicy();
            objDaAgrMstSamAgroMaster.DaPolicyDocumentUpload(httpRequest, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PolicyDocumentUploadTmpList")]
        [HttpGet]
        public HttpResponseMessage PolicyDocumentUploadTmpList(string insurancecompany2policy_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPolicy values = new MdlPolicy();
            objDaAgrMstSamAgroMaster.DaPolicyDocumentUploadTmpList(insurancecompany2policy_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PolicyDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage PolicyDocumentDelete(string insurancecompanypolicy2document_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPolicy values = new MdlPolicy();
            objDaAgrMstSamAgroMaster.DaPolicyDocumentDelete(insurancecompanypolicy2document_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InsuranceCompanySubmit")]
        [HttpPost]
        public HttpResponseMessage InsuranceCompanySubmit(MdlInsuranceCompany values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaInsuranceCompanySubmit(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetInsuranceCompanyTempClear")]
        [HttpGet]
        public HttpResponseMessage GetPolicyTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            objDaAgrMstSamAgroMaster.DaGetInsuranceCompanyTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditInsuranceCompany")]
        [HttpGet]
        public HttpResponseMessage EditProgram(string insurancecompany_gid)
        {
            MdlInsuranceCompany objmaster = new MdlInsuranceCompany();
            objDaAgrMstSamAgroMaster.DaEditInsuranceCompany(insurancecompany_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("InactiveInsuranceCompany")]
        [HttpPost]
        public HttpResponseMessage InactiveInsuranceCompany(MdlInsuranceCompany values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaInactiveInsuranceCompany(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InsuranceCompanyInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage InsuranceCompanyInactiveLogview(string insurancecompany_gid)
        {
            MdlInsuranceCompany values = new MdlInsuranceCompany();
            objDaAgrMstSamAgroMaster.DaInsuranceCompanyInactiveLogview(insurancecompany_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditPolicy")]
        [HttpGet]
        public HttpResponseMessage EditPolicy(string insurancecompany2policy_gid)
        {
            MdlPolicy values = new MdlPolicy();
            objDaAgrMstSamAgroMaster.DaEditPolicy(insurancecompany2policy_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatePolicy")]
        [HttpPost]
        public HttpResponseMessage UpdatePolicy(MdlPolicy values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaUpdatePolicy(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPolicyTempList")]
        [HttpGet]
        public HttpResponseMessage GetPolicyTempList(string insurancecompany_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPolicy values = new MdlPolicy();
            objDaAgrMstSamAgroMaster.DaGetPolicyTempList(insurancecompany_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PolicyList")]
        [HttpGet]
        public HttpResponseMessage PolicyList(string insurancecompany_gid)
        {
            MdlPolicy values = new MdlPolicy();
            objDaAgrMstSamAgroMaster.DaPolicyList(insurancecompany_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateInsuranceCompany")]
        [HttpPost]
        public HttpResponseMessage UpdateInsuranceCompany(MdlInsuranceCompany values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaUpdateInsuranceCompany(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Buyer/Supplier Type

        [ActionName("GetBuyerSupplierType")]
        [HttpGet]
        public HttpResponseMessage GetBuyerSupplierType()
        {
            MdlBuyerSupplierType objbuyersuppliertype = new MdlBuyerSupplierType();
            objDaAgrMstSamAgroMaster.DaGetBuyerSupplierType(objbuyersuppliertype);
            return Request.CreateResponse(HttpStatusCode.OK, objbuyersuppliertype);
        }

        [ActionName("CreatetBuyerSupplierType")]
        [HttpPost]
        public HttpResponseMessage CreatetBuyerSupplierType(BuyerSupplierType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaCreatetBuyerSupplierType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditBuyerSupplierType")]
        [HttpGet]
        public HttpResponseMessage EditBuyerSupplierType(string buyersuppliertype_gid)
        {
            BuyerSupplierType values = new BuyerSupplierType();
            objDaAgrMstSamAgroMaster.DaEditBuyerSupplierType(buyersuppliertype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateBuyerSupplierType")]
        [HttpPost]
        public HttpResponseMessage UpdateBuyerSupplierType(BuyerSupplierType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaUpdateBuyerSupplierType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveBuyerSupplierType")]
        [HttpPost]
        public HttpResponseMessage InactiveBuyerSupplierType(BuyerSupplierType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaInactiveBuyerSupplierType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBuyerSupplierType")]
        [HttpGet]
        public HttpResponseMessage DeleteBuyerSupplierType(string buyersuppliertype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstSamAgroMaster.DaDeleteBuyerSupplierType(buyersuppliertype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BuyerSupplierTypeInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage BuyerSupplierTypeInactiveLogview(string buyersuppliertype_gid)
        {
            MdlBuyerSupplierType values = new MdlBuyerSupplierType();
            objDaAgrMstSamAgroMaster.DaBuyerSupplierTypeInactiveLogview(buyersuppliertype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}