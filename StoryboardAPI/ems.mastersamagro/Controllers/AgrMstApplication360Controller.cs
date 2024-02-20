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
    /// This Controllers will provide access to various masters custopedia
    /// </summary>
    /// <remarks>Written by Abilash.A, Kalaiarasan, Premchandar.K</remarks>

    [RoutePrefix("api/AgrMstApplication360")]
    [Authorize]

    public class AgrMstApplication360Controller : ApiController
    {
        DaAgrMstApplication360 objDaAgrMstApplication360 = new DaAgrMstApplication360();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        // Entity
        [ActionName("GetEntity")]
        [HttpGet]
        public HttpResponseMessage GetEntity()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetEntity(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateEntity")]
        [HttpPost]
        public HttpResponseMessage CreateEntity(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateEntity(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditEntity")]
        [HttpGet]
        public HttpResponseMessage EditEntity(string entity_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditEntity(entity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateEntity")]
        [HttpPost]
        public HttpResponseMessage UpdateEntity(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateEntity(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveEntity")]
        [HttpPost]
        public HttpResponseMessage InactiveEntity(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveEntity(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteEntity")]
        [HttpGet]
        public HttpResponseMessage DeleteEntity(string entity_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteEntity(entity_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EntityInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage EntityInactiveLogview(string entity_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaEntityInactiveLogview(entity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EntityList")]
        [HttpGet]
        public HttpResponseMessage EntityList()
        {
            Mdlentitylist values = new Mdlentitylist();
            objDaAgrMstApplication360.DaEntityList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Vertical Taggs

        [ActionName("GetVerticalTaggs")]
        [HttpGet]
        public HttpResponseMessage GetVerticalTaggs()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetVerticalTaggs(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateVerticalTaggs")]
        [HttpPost]
        public HttpResponseMessage CreateVerticalTaggs(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateVerticalTaggs(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditVerticalTaggs")]
        [HttpGet]
        public HttpResponseMessage EditVerticalTaggs(string verticaltaggs_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditVerticalTaggs(verticaltaggs_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateVerticalTaggs")]
        [HttpPost]
        public HttpResponseMessage UpdateVerticalTaggs(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateVerticalTaggs(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveVerticalTaggs")]
        [HttpPost]
        public HttpResponseMessage InactiveVerticalTaggs(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveVerticalTaggs(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteVerticalTaggs")]
        [HttpGet]
        public HttpResponseMessage DeleteVerticalTaggs(string verticaltaggs_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteVerticalTaggs(verticaltaggs_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("VerticalTaggsInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage VerticalTaggsInactiveLogview(string verticaltaggs_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaVerticalTaggsInactiveLogview(verticaltaggs_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Business Category

        [ActionName("GetBusinessCategory")]
        [HttpGet]
        public HttpResponseMessage GetBusinessCategory()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.GetBusinessCategory(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateBusinessCategory")]
        [HttpPost]
        public HttpResponseMessage CreateBusinessCategory(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateBusinessCategory(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditBusinessCategory")]
        [HttpGet]
        public HttpResponseMessage EditBusinessCategory(string businesscategory_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditBusinessCategory(businesscategory_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateBusinessCategory")]
        [HttpPost]
        public HttpResponseMessage UpdateBusinessCategory(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateBusinessCategory(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveBusinessCategory")]
        [HttpPost]
        public HttpResponseMessage InactiveBusinessCategory(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveBusinessCategory(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBusinessCategory")]
        [HttpGet]
        public HttpResponseMessage DeleteBusinessCategory(string businesscategory_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DeleteBusinessCategory(businesscategory_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BusinessCategoryInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage BusinessCategoryInactiveLogview(string businesscategory_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaBusinessCategoryInactiveLogview(businesscategory_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Business Industry Type

        [ActionName("GetBusinessIndustryType")]
        [HttpGet]
        public HttpResponseMessage GetBusinessIndustryType()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetBusinessIndustryType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateBusinessIndustryType")]
        [HttpPost]
        public HttpResponseMessage CreateBusinessIndustryType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateBusinessIndustryType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditBusinessIndustryType")]
        [HttpGet]
        public HttpResponseMessage EditBusinessIndustryType(string businessindustrytype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditBusinessIndustryType(businessindustrytype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateBusinessIndustryType")]
        [HttpPost]
        public HttpResponseMessage UpdateBusinessIndustryType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateBusinessIndustryType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveBusinessIndustryType")]
        [HttpPost]
        public HttpResponseMessage InactiveBusinessIndustryType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveBusinessIndustryType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBusinessIndustryType")]
        [HttpGet]
        public HttpResponseMessage DeleteBusinessIndustryType(string businessindustrytype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteBusinessIndustryType(businessindustrytype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BusinessIndustryTypeInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage BusinessIndustryTypeInactiveLogview(string businessindustrytype_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaBusinessIndustryTypeInactiveLogview(businessindustrytype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Income Type

        [ActionName("GetIncomeType")]
        [HttpGet]
        public HttpResponseMessage GetIncomeType()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetIncomeType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateIncomeType")]
        [HttpPost]
        public HttpResponseMessage CreateIncomeType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateIncomeType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditIncomeType")]
        [HttpGet]
        public HttpResponseMessage EditIncomeType(string incometype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditIncomeType(incometype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateIncomeType")]
        [HttpPost]
        public HttpResponseMessage UpdateIncomeType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateIncomeType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveIncomeType")]
        [HttpPost]
        public HttpResponseMessage InactiveIncomeType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveIncomeType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteIncomeType")]
        [HttpGet]
        public HttpResponseMessage DeleteIncomeType(string incometype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteIncomeType(incometype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IncomeTypeInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage IncomeTypeInactiveLogview(string incometype_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaIncomeTypeInactiveLogview(incometype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Individual Proof

        [ActionName("GetIndividualProof")]
        [HttpGet]
        public HttpResponseMessage GetIndividualProof()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetIndividualProof(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateIndividualProof")]
        [HttpPost]
        public HttpResponseMessage CreateIndividualProof(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateIndividualProof(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditIndividualProof")]
        [HttpGet]
        public HttpResponseMessage EditIndividualProof(string individualproof_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditIndividualProof(individualproof_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateIndividualProof")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualProof(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateIndividualProof(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveIndividualProof")]
        [HttpPost]
        public HttpResponseMessage InactiveIndividualProof(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveIndividualProof(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteIndividualProof")]
        [HttpGet]
        public HttpResponseMessage DeleteIndividualProof(string individualproof_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteIndividualProof(individualproof_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("IndividualProofInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage IndividualProofInactiveLogview(string individualproof_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaIndividualProofInactiveLogview(individualproof_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Security Coverage

        [ActionName("GetSecurityCoverage")]
        [HttpGet]
        public HttpResponseMessage GetSecurityCoverage()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetSecurityCoverage(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateSecurityCoverage")]
        [HttpPost]
        public HttpResponseMessage CreateSecurityCoverage(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateSecurityCoverage(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditSecurityCoverage")]
        [HttpGet]
        public HttpResponseMessage EditSecurityCoverage(string securitycoverage_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditSecurityCoverage(securitycoverage_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateSecurityCoverage")]
        [HttpPost]
        public HttpResponseMessage UpdateSecurityCoverage(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateSecurityCoverage(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveSecurityCoverage")]
        [HttpPost]
        public HttpResponseMessage InactiveSecurityCoverage(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveSecurityCoverage(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveSecurityCoverageHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveSecurityCoverageHistory(string securitycoverage_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveSecurityCoverageHistory(objapplicationhistory, securitycoverage_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        [ActionName("DeleteSecurityCoverage")]
        [HttpGet]
        public HttpResponseMessage DeleteSecurityCoverage(string securitycoverage_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteSecurityCoverage(securitycoverage_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Type of facility

        [ActionName("CreateCreditTypeOfFacility")]
        [HttpPost]
        public HttpResponseMessage CreateCreditTypeOfFacility(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.CreateCreditTypeOfFacility(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditTypeOfFacilitySummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditTypeOfFacilitySummary()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetCreditTypeOfFacilitySummary(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreditTypeOfFacilityEdit")]
        [HttpGet]
        public HttpResponseMessage CreditTypeOfFacilityEdit(string credittypeoffacility_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaCreditTypeOfFacilityEdit(credittypeoffacility_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditTypeOfFacilityUpdate")]
        [HttpPost]
        public HttpResponseMessage CreditTypeOfFacilityUpdate(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditTypeOfFacilityUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditTypeOfFacilityInactive")]
        [HttpPost]
        public HttpResponseMessage CreditTypeOfFacilityInactive(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditTypeOfFacilityInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditTypeOfFacilityDelete")]
        [HttpGet]
        public HttpResponseMessage CreditTypeOfFacilityDelete(string credittypeoffacility_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditTypeOfFacilityDelete(credittypeoffacility_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditTypeOfFacilityInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage CreditTypeOfFacilityInactiveLogview(string credittypeoffacility_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaCreditTypeOfFacilityInactiveLogview(credittypeoffacility_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Type

        [ActionName("CreditTypeAdd")]
        [HttpPost]
        public HttpResponseMessage CreditTypeAdd(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditTypeAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditTypeSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditTypeSummary()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetCreditTypeSummary(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreditTypeEdit")]
        [HttpGet]
        public HttpResponseMessage CreditTypeEdit(string credittype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaCreditTypeEdit(credittype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditTypeUpdate")]
        [HttpPost]
        public HttpResponseMessage CreditTypeUpdate(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditTypeUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditTypeInactive")]
        [HttpPost]
        public HttpResponseMessage CreditTypeInactive(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditTypeInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditTypeDelete")]
        [HttpGet]
        public HttpResponseMessage CreditTypeDelete(string credittype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditTypeDelete(credittype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditTypeInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage CreditTypeInactiveLogview(string credittype_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaCreditTypeInactiveLogview(credittype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Account Classification

        [ActionName("CreditAccountClassificationAdd")]
        [HttpPost]
        public HttpResponseMessage CreditAccountClassificationAdd(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditAccountClassificationAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditAccountClassificationSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditAccountClassificationSummary()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetCreditAccountClassificationSummary(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreditAccountClassificationEdit")]
        [HttpGet]
        public HttpResponseMessage CreditAccountClassificationEdit(string creditaccountclassification_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaCreditAccountClassificationEdit(creditaccountclassification_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditAccountClassificationUpdate")]
        [HttpPost]
        public HttpResponseMessage CreditAccountClassificationUpdate(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditAccountClassificationUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditAccountClassificationInactive")]
        [HttpPost]
        public HttpResponseMessage CreditAccountClassificationInactive(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditAccountClassificationInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditAccountClassificationDelete")]
        [HttpGet]
        public HttpResponseMessage CreditAccountClassificationDelete(string creditaccountclassification_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditAccountClassificationDelete(creditaccountclassification_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditAccountClassificationInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage CreditAccountClassificationInactiveLogview(string creditaccountclassification_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaCreditAccountClassificationInactiveLogview(creditaccountclassification_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Instalment Frequency

        [ActionName("CreditInstalmentFrequencyAdd")]
        [HttpPost]
        public HttpResponseMessage CreditInstalmentFrequencyAdd(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditInstalmentFrequencyAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditInstalmentFrequencySummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditInstalmentFrequencySummary()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetCreditInstalmentFrequencySummary(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }


        [ActionName("CreditInstalmentFrequencyEdit")]
        [HttpGet]
        public HttpResponseMessage CreditInstalmentFrequencyEdit(string creditinstalmentfrequency_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaCreditInstalmentFrequencyEdit(creditinstalmentfrequency_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditInstalmentFrequencyUpdate")]
        [HttpPost]
        public HttpResponseMessage CreditInstalmentFrequencyUpdate(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditInstalmentFrequencyUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditInstalmentFrequencyInactive")]
        [HttpPost]
        public HttpResponseMessage CreditInstalmentFrequencyInactive(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditInstalmentFrequencyInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditInstalmentFrequencyDelete")]
        [HttpGet]
        public HttpResponseMessage CreditInstalmentFrequencyDelete(string creditinstalmentfrequency_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditInstalmentFrequencyDelete(creditinstalmentfrequency_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditInstalmentFrequencyInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage CreditInstalmentFrequencyInactiveLogview(string creditinstalmentfrequency_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaCreditInstalmentFrequencyInactiveLogview(creditinstalmentfrequency_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Credit Type Of Existing Funded

        [ActionName("CreditTypeOfExistingFundedAdd")]
        [HttpPost]
        public HttpResponseMessage CreditTypeOfExistingFundedAdd(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditTypeOfExistingFundedAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditTypeOfExistingFundedSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditTypeOfExistingFundedSummary()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetCreditTypeOfExistingFundedSummary(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreditTypeOfExistingFundedEdit")]
        [HttpGet]
        public HttpResponseMessage CreditTypeOfExistingFundedEdit(string credittypeofexistingfunded_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaCreditTypeOfExistingFundedEdit(credittypeofexistingfunded_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditTypeOfExistingFundedUpdate")]
        [HttpPost]
        public HttpResponseMessage CreditTypeOfExistingFundedUpdate(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditTypeOfExistingFundedUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditTypeOfExistingFundedInactive")]
        [HttpPost]
        public HttpResponseMessage CreditTypeOfExistingFundedInactive(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditTypeOfExistingFundedInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditTypeOfExistingFundedDelete")]
        [HttpGet]
        public HttpResponseMessage CreditTypeOfExistingFundedDelete(string credittypeofexistingfunded_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreditTypeOfExistingFundedDelete(credittypeofexistingfunded_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditTypeExistingFundInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage CreditTypeExistingFundInactiveLogview(string credittypeofexistingfunded_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaCreditTypeExistingFundInactiveLogview(credittypeofexistingfunded_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Lending Arrangement

        [ActionName("GetLendingArrangement")]
        [HttpGet]
        public HttpResponseMessage GetLendingArrangement()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetLendingArrangement(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateLendingArrangement")]
        [HttpPost]
        public HttpResponseMessage CreateLendingArrangement(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateLendingArrangement(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditLendingArrangement")]
        [HttpGet]
        public HttpResponseMessage EditLendingArrangegement(string lendingarrangement_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditLendingArrangement(lendingarrangement_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateLendingArrangement")]
        [HttpPost]
        public HttpResponseMessage UpdateLendingArrangement(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateLendingArrangement(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveLendingArrangement")]
        [HttpPost]
        public HttpResponseMessage InactiveLendingarrangement(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveLendingArrangement(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveLendingArrangementHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveLendingArrangementHistory(string lendingarrangement_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveLendingArrangementHistory(objapplicationhistory, lendingarrangement_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }
        [ActionName("DeleteLendingArrangement")]
        [HttpGet]
        public HttpResponseMessage DeleteLendingArrangement(string lendingarrangement_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteLendingArrangement(lendingarrangement_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Assets Type

        [ActionName("GetAssetsType")]
        [HttpGet]
        public HttpResponseMessage GetAssetsType()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetAssetsType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateAssetsType")]
        [HttpPost]
        public HttpResponseMessage CreateAssetsType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateAssetsType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAssetsType")]
        [HttpGet]
        public HttpResponseMessage EditAssetsType(string assetstype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditAssetsType(assetstype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAssetsType")]
        [HttpPost]
        public HttpResponseMessage UpdateAssetsType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateAssetsType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveAssetsType")]
        [HttpPost]
        public HttpResponseMessage InactiveAssetsType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveAssetsType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveAssetsTypeHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveAssetsTypeHistory(string assetstype_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveAssetsTypeHistory(objapplicationhistory, assetstype_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }
        [ActionName("DeleteAssetsType")]
        [HttpGet]
        public HttpResponseMessage DeleteAssetsType(string assetstype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteAssetsType(assetstype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Party Type

        [ActionName("GetPartyType")]
        [HttpGet]
        public HttpResponseMessage GetPartyType()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetPartyType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreatePartyType")]
        [HttpPost]
        public HttpResponseMessage CreatePartyType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreatePartyType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditPartyType")]
        [HttpGet]
        public HttpResponseMessage EditPartyType(string partytype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditPartyType(partytype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatePartyType")]
        [HttpPost]
        public HttpResponseMessage UpdatePartyType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdatePartyType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactivePartyType")]
        [HttpPost]
        public HttpResponseMessage InactivePartyType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactivePartyType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactivePartyTypeHistory")]
        [HttpGet]
        public HttpResponseMessage InactivePartyTypeHistory(string partytype_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactivePartyTypeHistory(objapplicationhistory, partytype_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }
        [ActionName("DeletePartyType")]
        [HttpGet]
        public HttpResponseMessage DeletePartyType(string partytype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeletePartyType(partytype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Caste

        [ActionName("GetCaste")]
        [HttpGet]
        public HttpResponseMessage GetCaste()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetCaste(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateCaste")]
        [HttpPost]
        public HttpResponseMessage CreateCaste(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateCaste(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditCaste")]
        [HttpGet]
        public HttpResponseMessage EditCaste(string caste_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditCaste(caste_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCaste")]
        [HttpPost]
        public HttpResponseMessage UpdateCaste(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateCaste(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveCaste")]
        [HttpPost]
        public HttpResponseMessage InactiveCaste(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveCaste(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCaste")]
        [HttpGet]
        public HttpResponseMessage DeleteCaste(string caste_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteCaste(caste_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveCasteHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveCasteHistory(string caste_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveCasteHistory(objapplicationhistory, caste_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        //Type Of Charge Created

        [ActionName("GetTypeOfChargeCreated")]
        [HttpGet]
        public HttpResponseMessage GetTypeOfChargeCreated()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetTypeOfChargeCreated(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateTypeOfChargeCreated")]
        [HttpPost]
        public HttpResponseMessage CreateTypeOfChargeCreated(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateTypeOfChargeCreated(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditTypeOfChargeCreated")]
        [HttpGet]
        public HttpResponseMessage EditTypeOfChargeCreated(string typeofchargecreated_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditTypeOfChargeCreated(typeofchargecreated_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateTypeOfChargeCreated")]
        [HttpPost]
        public HttpResponseMessage UpdateTypeOfChargeCreated(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateTypeOfChargeCreated(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveTypeOfChargeCreated")]
        [HttpPost]
        public HttpResponseMessage InactiveTypeOfChargeCreated(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveTypeOfChargeCreated(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteTypeOfChargeCreated")]
        [HttpGet]
        public HttpResponseMessage DeleteTypeOfChargeCreated(string typeofchargecreated_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteTypeOfChargeCreated(typeofchargecreated_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveTypeOfChargeCreatedHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveTypeOfChargeCreatedHistory(string typeofchargecreated_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveTypeOfChargeCreatedHistory(objapplicationhistory, typeofchargecreated_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        //Construction Type

        [ActionName("GetConstructionType")]
        [HttpGet]
        public HttpResponseMessage GetConstructionType()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetConstructionType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateConstructionType")]
        [HttpPost]
        public HttpResponseMessage CreateConstructionType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateConstructionType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditConstructionType")]
        [HttpGet]
        public HttpResponseMessage EditConstructionType(string constructiontype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditConstructionType(constructiontype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateConstructionType")]
        [HttpPost]
        public HttpResponseMessage UpdateConstructionType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateConstructionType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveConstructionType")]
        [HttpPost]
        public HttpResponseMessage InactiveConstructionType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveConstructionType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteConstructionType")]
        [HttpGet]
        public HttpResponseMessage DeleteConstructionType(string constructiontype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteConstructionType(constructiontype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveConstructionTypeHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveConstructionTypeHistory(string constructiontype_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveConstructionTypeHistory(objapplicationhistory, constructiontype_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        // SA Type

        [ActionName("GetSAType")]
        [HttpGet]
        public HttpResponseMessage GetSAType()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetSAType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateSAType")]
        [HttpPost]
        public HttpResponseMessage CreateSAType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateSAType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditSAType")]
        [HttpGet]
        public HttpResponseMessage EditSAType(string satype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditSAType(satype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateSAType")]
        [HttpPost]
        public HttpResponseMessage UpdateSAType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateSAType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveSAType")]
        [HttpPost]
        public HttpResponseMessage InactiveSAType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveSAType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteSAType")]
        [HttpGet]
        public HttpResponseMessage DeleteSAType(string satype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteSAType(satype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SATypeInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage SATypeInactiveLogview(string satype_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaSATypeInactiveLogview(satype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SATypeList")]
        [HttpGet]
        public HttpResponseMessage SATypeList()
        {
            MdlSATypeList values = new MdlSATypeList();
            objDaAgrMstApplication360.DaSATypeList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // SA Entity Type

        [ActionName("GetSAEntityType")]
        [HttpGet]
        public HttpResponseMessage GetSAEntityType()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetSAEntityType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateSAEntityType")]
        [HttpPost]
        public HttpResponseMessage CreateSAEntityType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateSAEntityType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditSAEntityType")]
        [HttpGet]
        public HttpResponseMessage EditSAEntityType(string saentitytype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditSAEntityType(saentitytype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateSAEntityType")]
        [HttpPost]
        public HttpResponseMessage UpdateSAEntityType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateSAEntityType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveSAEntityType")]
        [HttpPost]
        public HttpResponseMessage InactiveSAEntityType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveSAEntityType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteSAEntityType")]
        [HttpGet]
        public HttpResponseMessage DeleteSAEntityType(string saentitytype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteSAEntityType(saentitytype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SAEntityTypeInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage SAEntityTypeInactiveLogview(string saentitytype_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaSAEntityTypeInactiveLogview(saentitytype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // SA Document list 

        [ActionName("GetSADocumentList")]
        [HttpGet]
        public HttpResponseMessage GetSADocumentList()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetSADocumentList(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateSADocumentList")]
        [HttpPost]
        public HttpResponseMessage CreateSADocumentList(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateSADocumentList(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditSADocumentList")]
        [HttpGet]
        public HttpResponseMessage EditSADocumentList(string sadocumentlist_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditSADocumentList(sadocumentlist_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateSADocumentList")]
        [HttpPost]
        public HttpResponseMessage UpdateSADocumentList(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateSADocumentList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveSADocumentList")]
        [HttpPost]
        public HttpResponseMessage InactiveSADocumentList(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveSADocumentList(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteSADocumentList")]
        [HttpGet]
        public HttpResponseMessage DeleteSADocumentList(string sadocumentlist_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteSADocumentList(sadocumentlist_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SADocumentListInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage SADocumentListInactiveLogview(string sadocumentlist_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaSADocumentListInactiveLogview(sadocumentlist_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Loan purpose

        [ActionName("GetLoanPurpose")]
        [HttpGet]
        public HttpResponseMessage GetLoanPurpose()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetLoanPurpose(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateLoanPurpose")]
        [HttpPost]
        public HttpResponseMessage CreateLoanPurpose(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateLoanPurpose(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditLoanPurpose")]
        [HttpGet]
        public HttpResponseMessage EditLoanPurpose(string loanpurpose_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditLoanPurpose(loanpurpose_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateLoanpurpose")]
        [HttpPost]
        public HttpResponseMessage UpdateLoanPurpose(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateLoanPurpose(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveLoanPurpose")]
        [HttpPost]
        public HttpResponseMessage InactiveLoanPurpose(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveLoanPurpose(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveLoanPurposeHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveLoanPurposeHistory(string loanpurpose_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveLoanPurposeHistory(objapplicationhistory, loanpurpose_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        [ActionName("DeleteLoanPurpose")]
        [HttpGet]
        public HttpResponseMessage DeleteLoanpurpose(string loanpurpose_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteLoanPurpose(loanpurpose_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Type of Debt

        [ActionName("GetTypeofDebt")]
        [HttpGet]
        public HttpResponseMessage GetTypeofDebt()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetTypeofDebt(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateTypeofDebt")]
        [HttpPost]
        public HttpResponseMessage CreateTypeofDebt(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateTypeofDebt(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditTypeofDebt")]
        [HttpGet]
        public HttpResponseMessage EditTypeofDebt(string typeofdebt_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditTypeofDebt(typeofdebt_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateTypeofDebt")]
        [HttpPost]
        public HttpResponseMessage UpdateTypeofDebt(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateTypeofDebt(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveTypeofDebt")]
        [HttpPost]
        public HttpResponseMessage InactiveTypeofDebt(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveTypeofDebt(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveTypeofDebtHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveTypeofDebtHistory(string typeofdebt_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveTypeofDebtHistory(objapplicationhistory, typeofdebt_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        [ActionName("DeleteTypeofDebt")]
        [HttpGet]
        public HttpResponseMessage DeleteTypeofDebt(string typeofdebt_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteTypeofDebt(typeofdebt_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Bank Account Level
        [ActionName("GetBankAccountLevel")]
        [HttpGet]
        public HttpResponseMessage GetBankAccountLevel()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetBankAccountLevel(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateBankAccountLevel")]
        [HttpPost]
        public HttpResponseMessage CreateBankAccountLevel(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateBankAccountLevel(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditBankAccountLevel")]
        [HttpGet]
        public HttpResponseMessage EditBankAccountLevel(string bankaccountlevel_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditBankAccountLevel(bankaccountlevel_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateBankAccountLevel")]
        [HttpPost]
        public HttpResponseMessage UpdateBankAccountLevel(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateBankAccountLevel(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveBankAccountLevel")]
        [HttpPost]
        public HttpResponseMessage InactiveBankAccountLevel(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveBankAccountLevel(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBankAccountLevel")]
        [HttpGet]
        public HttpResponseMessage DeleteBankAccountLevel(string bankaccountlevel_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteBankAccountLevel(bankaccountlevel_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BankAccountLevelInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage BankAccountLevelInactiveLogview(string bankaccountlevel_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaBankAccountLevelInactiveLogview(bankaccountlevel_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Relationship
        [ActionName("GetRelationship")]
        [HttpGet]
        public HttpResponseMessage GetRelationship()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetRelationship(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateRelationship")]
        [HttpPost]
        public HttpResponseMessage CreateRelationship(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateRelationship(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditRelationship")]
        [HttpGet]
        public HttpResponseMessage EditRelationship(string relationship_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditRelationship(relationship_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateRelationship")]
        [HttpPost]
        public HttpResponseMessage UpdateRelationship(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateRelationship(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveRelationship")]
        [HttpPost]
        public HttpResponseMessage InactiveRelationship(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveRelationship(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteRelationship")]
        [HttpGet]
        public HttpResponseMessage DeleteRelationship(string relationship_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteRelationship(relationship_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("RelationshipInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage RelationshipInactiveLogview(string relationship_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaRelationshipInactiveLogview(relationship_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Samunnati Branch Name

        [ActionName("GetSamunnatiBranchName")]
        [HttpGet]
        public HttpResponseMessage GetSamunnatiBranchName()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetSamunnatiBranchName(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateSamunnatiBranchName")]
        [HttpPost]
        public HttpResponseMessage CreateSamunnatiBranchName(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateSamunnatiBranchName(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditSamunnatiBranchName")]
        [HttpGet]
        public HttpResponseMessage EditSamunnatiBranchName(string samunnatibranch_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditSamunnatiBranchName(samunnatibranch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateSamunnatiBranchName")]
        [HttpPost]
        public HttpResponseMessage UpdateSamunnatiBranchName(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateSamunnatiBranchName(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveSamunnatiBranchName")]
        [HttpPost]
        public HttpResponseMessage InactiveSamunnatiBranchName(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveSamunnatiBranchName(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteSamunnatiBranchName")]
        [HttpGet]
        public HttpResponseMessage DeleteSamunnatiBranchName(string samunnatibranch_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteSamunnatiBranchName(samunnatibranch_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SamunnatiBranchNameInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage SamunnatiBranchNameInactiveLogview(string samunnatibranch_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaSamunnatiBranchNameInactiveLogview(samunnatibranch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Samunnati Branch State
        [ActionName("GetSamunnatiBranchState")]
        [HttpGet]
        public HttpResponseMessage GetSamunnatiBranchState()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetSamunnatiBranchState(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateSamunnatiBranchState")]
        [HttpPost]
        public HttpResponseMessage CreateSamunnatiBranchState(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateSamunnatiBranchState(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditSamunnatiBranchState")]
        [HttpGet]
        public HttpResponseMessage EditSamunnatiBranchState(string samunnatibranchstate_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditSamunnatiBranchState(samunnatibranchstate_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateSamunnatiBranchState")]
        [HttpPost]
        public HttpResponseMessage UpdateSamunnatiBranchState(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateSamunnatiBranchState(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveSamunnatiBranchState")]
        [HttpPost]
        public HttpResponseMessage InactiveSamunnatiBranchState(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveSamunnatiBranchState(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteSamunnatiBranchState")]
        [HttpGet]
        public HttpResponseMessage DeleteSamunnatiBranchState(string samunnatibranchstate_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteSamunnatiBranchState(samunnatibranchstate_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SamunnatiBranchStateInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage SamunnatiBranchStateInactiveLogview(string samunnatibranchstate_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaSamunnatiBranchStateInactiveLogview(samunnatibranchstate_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Genetic Code

        [ActionName("GetGeneticCode")]
        [HttpGet]
        public HttpResponseMessage GetGeneticCode()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetGeneticCode(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateGeneticCode")]
        [HttpPost]
        public HttpResponseMessage CreateGeneticCode(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateGeneticCode(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGeneticCode")]
        [HttpGet]
        public HttpResponseMessage EditGeneticCode(string geneticcode_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditGeneticCode(geneticcode_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateGeneticCode")]
        [HttpPost]
        public HttpResponseMessage UpdateGeneticCode(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateGeneticCode(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveGeneticCode")]
        [HttpPost]
        public HttpResponseMessage InactiveGeneticCode(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveGeneticCode(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteGeneticCode")]
        [HttpGet]
        public HttpResponseMessage DeleteGeneticCode(string geneticcode_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteGeneticCode(geneticcode_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GeneticCodeInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GeneticCodeInactiveLogview(string geneticcode_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGeneticCodeInactiveLogview(geneticcode_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Gender

        [ActionName("GetGender")]
        [HttpGet]
        public HttpResponseMessage GetGender()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetGender(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateGender")]
        [HttpPost]
        public HttpResponseMessage CreateGender(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateGender(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGender")]
        [HttpGet]
        public HttpResponseMessage EditGender(string gender_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditGender(gender_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateGender")]
        [HttpPost]
        public HttpResponseMessage UpdateGender(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateGender(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveGender")]
        [HttpPost]
        public HttpResponseMessage InactiveGender(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveGender(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteGender")]
        [HttpGet]
        public HttpResponseMessage DeleteGender(string gender_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteGender(gender_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GenderInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GenderInactiveLogview(string gender_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGenderInactiveLogview(gender_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Vernacular Language

        [ActionName("GetVernacularLanguage")]
        [HttpGet]
        public HttpResponseMessage GetVernacularLanguage()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetVernacularLanguage(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateVernacularLanguage")]
        [HttpPost]
        public HttpResponseMessage CreateVernacularLanguage(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateVernacularLanguage(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditVernacularLanguage")]
        [HttpGet]
        public HttpResponseMessage EditVernacularLanguage(string vernacularlanguage_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditVernacularLanguage(vernacularlanguage_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateVernacularLanguage")]
        [HttpPost]
        public HttpResponseMessage UpdateVernacularLanguage(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateVernacularLanguage(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveVernacularLanguage")]
        [HttpPost]
        public HttpResponseMessage InactiveVernacularLanguage(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveVernacularLanguage(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteVernacularLanguage")]
        [HttpGet]
        public HttpResponseMessage DeleteVernacularLanguage(string vernacularlanguage_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteVernacularLanguage(vernacularlanguage_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("VernacularLanguageInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage VernacularLanguageInactiveLogview(string vernacularlanguage_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaVernacularLanguageInactiveLogview(vernacularlanguage_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //credit underwriting

        [ActionName("GetCreditUnderwritingFacilityType")]
        [HttpGet]
        public HttpResponseMessage GetCreditUnderwritingFacilityType()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetCreditUnderwritingFacilityType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateCreditUnderwritingFacilityType")]
        [HttpPost]
        public HttpResponseMessage CreateCreditUnderwritingFacilityType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateCreditUnderwritingFacilityType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditCreditUnderwritingFacilityType")]
        [HttpGet]
        public HttpResponseMessage EditCreditUnderwritingFacilityType(string creditunderwritingfacilitytype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditCreditUnderwritingFacilityType(creditunderwritingfacilitytype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCreditUnderwritingFacilityType")]
        [HttpPost]
        public HttpResponseMessage UpdateCreditUnderwritingFacilityType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateCreditUnderwritingFacilityType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveCreditUnderwritingFacilityType")]
        [HttpPost]
        public HttpResponseMessage InactiveCreditUnderwritingFacilityType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveCreditUnderwritingFacilityType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCreditUnderwritingFacilityType")]
        [HttpGet]
        public HttpResponseMessage DeleteCreditUnderwritingFacilityType(string creditunderwritingfacilitytype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteCreditUnderwritingFacilityType(creditunderwritingfacilitytype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditUnderwritingFacilityTypeInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage CreditUnderwritingFacilityTypeInactiveLogview(string creditunderwritingfacilitytype_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaCreditUnderwritingFacilityTypeInactiveLogview(creditunderwritingfacilitytype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Marital Status

        [ActionName("GetMaritalStatus")]
        [HttpGet]
        public HttpResponseMessage GetMaritalStatus()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetMaritalStatus(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateMaritalStatus")]
        [HttpPost]
        public HttpResponseMessage CreateMaritalStatus(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateMaritalStatus(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditMaritalStatus")]
        [HttpGet]
        public HttpResponseMessage EditMaritalStatus(string maritalstatus_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditMaritalStatus(maritalstatus_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateMaritalStatus")]
        [HttpPost]
        public HttpResponseMessage UpdateMaritalStatus(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateMaritalStatus(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveMaritalStatus")]
        [HttpPost]
        public HttpResponseMessage InactiveMaritalStatus(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveMaritalStatus(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteMaritalStatus")]
        [HttpGet]
        public HttpResponseMessage DeleteMaritalStatus(string maritalstatus_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteMaritalStatus(maritalstatus_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MaritalStatusInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage MaritalStatusInactiveLogview(string maritalstatus_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaMaritalStatusInactiveLogview(maritalstatus_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Educational Qualification 

        [ActionName("GetEducationalQualification")]
        [HttpGet]
        public HttpResponseMessage GetEducationalQualification()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetEducationalQualification(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateEducationalQualification")]
        [HttpPost]
        public HttpResponseMessage CreateEducationalQualification(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateEducationalQualification(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditEducationalQualification")]
        [HttpGet]

        public HttpResponseMessage EditEducationalQualification(string educationalqualification_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditEducationalQualification(educationalqualification_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateEducationalQualification")]
        [HttpPost]
        public HttpResponseMessage UpdateEducationalQualification(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateEducationalQualification(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveEducationalQualification")]
        [HttpPost]
        public HttpResponseMessage InactiveEducationalQualification(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveEducationalQualification(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteEducationalQualification")]
        [HttpGet]
        public HttpResponseMessage DeleteEducationalQualification(string educationalqualification_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteEducationalQualification(educationalqualification_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EducationalQualificationInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage EducationalQualificationInactiveLogview(string educationalqualification_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaEducationalQualificationInactiveLogview(educationalqualification_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Guarantee Coverage

        [ActionName("GetGuaranteeCoverage")]
        [HttpGet]
        public HttpResponseMessage GetGuaranteeCoverage()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetGuaranteeCoverage(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateGuaranteeCoverage")]
        [HttpPost]
        public HttpResponseMessage CreateGuaranteeCoverage(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateGuaranteeCoverage(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGuaranteeCoverage")]
        [HttpGet]
        public HttpResponseMessage EditGuaranteeCoverage(string guaranteecoverage_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditGuaranteeCoverage(guaranteecoverage_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateGuaranteeCoverage")]
        [HttpPost]
        public HttpResponseMessage UpdateGuaranteeCoverage(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateGuaranteeCoverage(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveGuaranteeCoverage")]
        [HttpPost]
        public HttpResponseMessage InactiveGuaranteeCoverage(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveGuaranteeCoverage(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteGuaranteeCoverage")]
        [HttpGet]
        public HttpResponseMessage DeleteGuaranteeCoverage(string guaranteecoverage_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteGuaranteeCoverage(guaranteecoverage_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GuaranteeCoverageInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GuaranteeCoverageInactiveLogview(string guaranteecoverage_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGuaranteeCoverageInactiveLogview(guaranteecoverage_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        // Company Document
        [ActionName("GetCompanyDocument")]
        [HttpGet]
        public HttpResponseMessage GetCompanyDocument()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetCompanyDocument(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateCompanydocument")]
        [HttpPost]
        public HttpResponseMessage CreateCompanyDocument(companydocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateCompanyDocument(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCompanyDropDown")]
        [HttpGet]
        public HttpResponseMessage GetCompanyDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetCompanyDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditCompanyDocument")]
        [HttpGet]
        public HttpResponseMessage EditCompanyDocument(string companydocument_gid)
        {
            companydocument values = new companydocument();
            objDaAgrMstApplication360.DaEditCompanyDocument(companydocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCompanyDocument")]
        [HttpPost]
        public HttpResponseMessage UpdateCompanyDocument(companydocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateCompanyDocument(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveCompanyDocument")]
        [HttpPost]
        public HttpResponseMessage InactiveCompanyDocument(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveCompanyDocument(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveCompanyDocumentHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveCompanyDocumentHistory(string companydocument_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveCompanyDocumentHistory(objapplicationhistory, companydocument_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }
        [ActionName("DeleteCompanyDocument")]
        [HttpGet]
        public HttpResponseMessage DeleteCompanyDocument(string companydocument_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteCompanyDocument(companydocument_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Area Type

        [ActionName("GetAreaType")]
        [HttpGet]
        public HttpResponseMessage GetAreaType()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetAreaType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateAreaType")]
        [HttpPost]
        public HttpResponseMessage CreateAreaType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateAreaType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAreaType")]
        [HttpGet]
        public HttpResponseMessage EditAreaType(string areatype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditAreaType(areatype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAreaType")]
        [HttpPost]
        public HttpResponseMessage UpdateAreaType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateAreaType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveAreaType")]
        [HttpPost]
        public HttpResponseMessage InactiveAreaType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveAreaType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAreaType")]
        [HttpGet]
        public HttpResponseMessage DeleteAreaType(string areatype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteAreaType(areatype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveAreaTypeHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveAreaTypeHistory(string areatype_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveAreaTypeHistory(objapplicationhistory, areatype_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        // Religion

        [ActionName("GetReligion")]
        [HttpGet]
        public HttpResponseMessage GetReligion()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetReligion(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateReligion")]
        [HttpPost]
        public HttpResponseMessage CreateReligion(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateReligion(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditReligion")]
        [HttpGet]
        public HttpResponseMessage EditReligion(string religion_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditReligion(religion_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateReligion")]
        [HttpPost]
        public HttpResponseMessage UpdateReligion(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateReligion(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveReligion")]
        [HttpPost]
        public HttpResponseMessage InactiveReligion(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveReligion(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteReligion")]
        [HttpGet]
        public HttpResponseMessage DeleteReligion(string religion_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteReligion(religion_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveReligionHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveReligionHistory(string religion_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveReligionHistory(objapplicationhistory, religion_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }


        // Assessment Agency

        [ActionName("GetAssessmentAgency")]
        [HttpGet]
        public HttpResponseMessage GetAssessmentAgency()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetAssessmentAgency(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateAssessmentAgency")]
        [HttpPost]
        public HttpResponseMessage CreateAssessmentAgency(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateAssessmentAgency(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAssessmentAgency")]
        [HttpGet]
        public HttpResponseMessage EditAssessmentAgency(string assessmentagency_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditAssessmentAgency(assessmentagency_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAssessmentAgency")]
        [HttpPost]
        public HttpResponseMessage UpdateAssessmentAgency(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateAssessmentAgency(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveAssessmentAgency")]
        [HttpPost]
        public HttpResponseMessage InactiveAssessmentAgency(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveAssessmentAgency(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAssessmentAgency")]
        [HttpGet]
        public HttpResponseMessage DeleteAssessmentAgency(string assessmentagency_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteAssessmentAgency(assessmentagency_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AssessmentAgencyInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage AssessmentAgencyInactiveLogview(string assessmentagency_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaAssessmentAgencyInactiveLogview(assessmentagency_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Loan Products
        [ActionName("GetLoanProduct")]
        [HttpGet]
        public HttpResponseMessage GetLoanProduct()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetLoanProduct(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreatetLoanProduct")]
        [HttpPost]
        public HttpResponseMessage CreatetLoanProduct(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreatetLoanProduct(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditLoanProduct")]
        [HttpGet]
        public HttpResponseMessage EditLoanProduct(string loanproduct_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.EditLoanProduct(loanproduct_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateLoanProduct")]
        [HttpPost]
        public HttpResponseMessage UpdateLoanProduct(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateLoanProduct(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveLoanProduct")]
        [HttpPost]
        public HttpResponseMessage InactiveLoanProduct(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveLoanProduct(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteLoanProduct")]
        [HttpGet]
        public HttpResponseMessage DeleteLoanProduct(string loanproduct_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteLoanProduct(loanproduct_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("LoanProductInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage LoanProductInactiveLogview(string loanproduct_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaLoanProductInactiveLogview(loanproduct_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Warehouse Facility

        [ActionName("GetWarehouseFacility")]
        [HttpGet]
        public HttpResponseMessage GetWarehouseFacility()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetWarehouseFacility(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreatetWarehouseFacility")]
        [HttpPost]
        public HttpResponseMessage CreatetWarehouseFacility(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreatetWarehouseFacility(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditWarehouseFacility")]
        [HttpGet]
        public HttpResponseMessage EditWarehouseFacility(string warehousefacility_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.EditWarehouseFacility(warehousefacility_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateWarehouseFacility")]
        [HttpPost]
        public HttpResponseMessage UpdateWarehouseFacility(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateWarehouseFacility(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveWarehouseFacility")]
        [HttpPost]
        public HttpResponseMessage InactiveWarehouseFacility(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveWarehouseFacility(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteWarehouseFacility")]
        [HttpGet]
        public HttpResponseMessage DeleteWarehouseFacility(string warehousefacility_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteWarehouseFacility(warehousefacility_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WarehouseFacilityInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage WarehouseFacilityInactiveLogview(string warehousefacility_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaWarehouseFacilityInactiveLogview(warehousefacility_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Bureau Name

        [ActionName("GetBureauName")]
        [HttpGet]
        public HttpResponseMessage GetBureauName()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetBureauName(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateBureauName")]
        [HttpPost]
        public HttpResponseMessage CreateBureauName(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateBureauName(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditBureauName")]
        [HttpGet]
        public HttpResponseMessage EditBureauName(string bureauname_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditBureauName(bureauname_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateBureauName")]
        [HttpPost]
        public HttpResponseMessage UpdateBureauName(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateBureauName(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveBureauName")]
        [HttpPost]
        public HttpResponseMessage InactiveBureauName(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveBureauName(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveBureauNameHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveBureauNameHistory(string bureauname_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveBureauNameHistory(objapplicationhistory, bureauname_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        [ActionName("DeleteBureauName")]
        [HttpGet]
        public HttpResponseMessage DeleteBureauName(string bureauname_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteBureauName(bureauname_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Company Type

        [ActionName("GetCompanyType")]
        [HttpGet]
        public HttpResponseMessage GetCompanyType()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetCompanyType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateCompanyType")]
        [HttpPost]
        public HttpResponseMessage CreateCompanyType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateCompanyType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditCompanyType")]
        [HttpGet]
        public HttpResponseMessage EditCompanyType(string companytype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditCompanyType(companytype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCompanyType")]
        [HttpPost]
        public HttpResponseMessage UpdateCompanyType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateCompanyType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveCompanyType")]
        [HttpPost]
        public HttpResponseMessage InactiveCompanyType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveCompanyType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveCompanyTypeHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveCompanyTypeHistory(string companytype_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveCompanyTypeHistory(objapplicationhistory, companytype_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }
        [ActionName("DeleteCompanyType")]
        [HttpGet]
        public HttpResponseMessage DeleteCompanyType(string companytype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteCompanyType(companytype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Funded Type Indicator

        [ActionName("GetFundedTypeIndicator")]
        [HttpGet]
        public HttpResponseMessage GetFundedTypeIndicator()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetFundedTypeIndicator(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateFundedTypeIndicator")]
        [HttpPost]
        public HttpResponseMessage CreateFundedTypeIndicator(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateFundedTypeIndicator(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditFundedTypeIndicator")]
        [HttpGet]
        public HttpResponseMessage EditFundedTypeIndicator(string fundedtypeindicator_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditFundedTypeIndicator(fundedtypeindicator_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateFundedTypeIndicator")]
        [HttpPost]
        public HttpResponseMessage UpdateFundedTypeIndicator(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateFundedTypeIndicator(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveFundedTypeIndicator")]
        [HttpPost]
        public HttpResponseMessage InactiveFundedTypeIndicator(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveFundedTypeIndicator(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteFundedTypeIndicator")]
        [HttpGet]
        public HttpResponseMessage DeleteFundedTypeIndicator(string fundedtypeindicator_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteFundedTypeIndicator(fundedtypeindicator_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveFundedTypeIndicatorHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveFundedTypeIndicatorHistory(string fundedtypeindicator_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveFundedTypeIndicatorHistory(objapplicationhistory, fundedtypeindicator_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }
        // AML Category

        [ActionName("GetAmlCategory")]
        [HttpGet]
        public HttpResponseMessage GetAmlCategory()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetAmlCategory(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateAmlCategory")]
        [HttpPost]
        public HttpResponseMessage CreateAmlCategory(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateAmlCategory(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAmlCategory")]
        [HttpGet]
        public HttpResponseMessage EditAmlCategory(string amlcategory_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditAmlCategory(amlcategory_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAmlcategory")]
        [HttpPost]
        public HttpResponseMessage UpdateAmlCategory(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateAmlCategory(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveAmlCategory")]
        [HttpPost]
        public HttpResponseMessage InactiveAmlCategory(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveAmlCategory(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveAmlCategoryHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveAmlCategoryHistory(string amlcategory_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveAmlCategoryHistory(objapplicationhistory, amlcategory_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }
        [ActionName("DeleteAmlCategory")]
        [HttpGet]
        public HttpResponseMessage DeleteAmlCategory(string amlcategory_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteAmlCategory(amlcategory_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetLoanType")]
        [HttpGet]
        public HttpResponseMessage GetLoanType()
        {
            MdlMstApplication360 objMdlType = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetLoanType(objMdlType);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlType);
        }

        [ActionName("CreateLoanType")]
        [HttpPost]
        public HttpResponseMessage CreateLoanType(loantype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateLoanType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditLoanType")]
        [HttpGet]
        public HttpResponseMessage EditLoanType(string loantype_gid)
        {
            loantype values = new loantype();
            objDaAgrMstApplication360.DaEditLoanType(loantype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateLoanType")]
        [HttpPost]
        public HttpResponseMessage UpdateLoanType(loantype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateLoanType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("LoanTypeDelete")]
        [HttpGet]
        public HttpResponseMessage LoanTypeDelete(string loantype_gid)
        {
            loantype values = new loantype();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaLoanTypeDelete(loantype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("LoanTypeStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage LoanTypeStatusUpdate(loantype values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaLoanTypeStatusUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetLoanTypeActiveLog")]
        [HttpGet]
        public HttpResponseMessage GetLLoanTypeActiveLog(string loantype_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetLLoanTypeActiveLog(loantype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCountryCode")]
        [HttpGet]
        public HttpResponseMessage GetCountryCode()
        {
            countrycode objMdlType = new countrycode();
            objDaAgrMstApplication360.DaGetCountryCode(objMdlType);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlType);
        }

        [ActionName("CreateCountryCode")]
        [HttpPost]
        public HttpResponseMessage CreateCountryCode(countrycode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateCountryCode(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditCountryCode")]
        [HttpGet]
        public HttpResponseMessage EditCountryCode(string countrycode_gid)
        {
            countrycode values = new countrycode();
            objDaAgrMstApplication360.DaEditCountryCode(countrycode_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCountryCode")]
        [HttpPost]
        public HttpResponseMessage UpdateCountryCode(countrycode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateCountryCode(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CountryCodeDelete")]
        [HttpGet]
        public HttpResponseMessage CountryCodeDelete(string countrycode_gid)
        {
            countrycode values = new countrycode();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.CountryCodeDelete(countrycode_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CountryStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage CountryStatusUpdate(countrycode values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCountryStatusUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCountryCodeActiveLog")]
        [HttpGet]
        public HttpResponseMessage GetCountryCodeActiveLog(string countrycode_gid)
        {
            countrycode values = new countrycode();
            objDaAgrMstApplication360.DaGetCountryCodeActiveLog(countrycode_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Loan Term Period
        [ActionName("GetLoanTermPeriod")]
        [HttpGet]
        public HttpResponseMessage GetLoanTermPeriod()
        {
            Mdlloantermperiod objMdlType = new Mdlloantermperiod();
            objDaAgrMstApplication360.DaGetLoanTermPeriod(objMdlType);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlType);
        }

        [ActionName("CreateLoanTermPeriod")]
        [HttpPost]
        public HttpResponseMessage CreateLoanTermPeriod(Mdlloantermperiod values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateLoanTermPeriod(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditLoanTermPeriod")]
        [HttpGet]
        public HttpResponseMessage EditLoanTermPeriod(string loantermperiod_gid)
        {
            Mdlloantermperiod values = new Mdlloantermperiod();
            objDaAgrMstApplication360.DaEditLoanTermPeriod(loantermperiod_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateLoanTermPeriod")]
        [HttpPost]
        public HttpResponseMessage UpdateLoanTermPeriod(Mdlloantermperiod values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateLoanTermPeriod(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("LoanTermPeriodDelete")]
        [HttpGet]
        public HttpResponseMessage LoanTermPeriodDelete(string loantermperiod_gid)
        {
            Mdlloantermperiod values = new Mdlloantermperiod();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaLoanTermPeriodDelete(loantermperiod_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("LoanTermPeriodStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage LoanTermPeriodStatusUpdate(Mdlloantermperiod values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaLoanTermPeriodStatusUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLoanTermPeriodActiveLog")]
        [HttpGet]
        public HttpResponseMessage GetLoanTermPeriodActiveLog(string loantermperiod_gid)
        {
            Mdlloantermperiod values = new Mdlloantermperiod();
            objDaAgrMstApplication360.DaGetLoanTermPeriodActiveLog(loantermperiod_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Amortization Method
        [ActionName("Getamortization_method")]
        [HttpGet]
        public HttpResponseMessage Getamortization_method()
        {
            Mdlamortization_method objMdlType = new Mdlamortization_method();
            objDaAgrMstApplication360.DaGetamortization_method(objMdlType);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlType);
        }

        [ActionName("Createamortization_method")]
        [HttpPost]
        public HttpResponseMessage Createamortization_mtd(Mdlamortization_method values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateamortization_mtd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Editamortization_mtd")]
        [HttpGet]
        public HttpResponseMessage Editamortization_mtd(string amortization_gid)
        {
            Mdlamortization_method values = new Mdlamortization_method();
            objDaAgrMstApplication360.DaEditamortization_mtd(amortization_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Updateamortization_mtd")]
        [HttpPost]
        public HttpResponseMessage Updateamortization_mtd(Mdlamortization_method values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateamortization_mtd(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("amortization_mtdDelete")]
        [HttpGet]
        public HttpResponseMessage amortization_mtdDelete(string amortization_gid)
        {
            Mdlamortization_method values = new Mdlamortization_method();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.Daamortization_mtdDelete(amortization_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("amortization_mtdStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage Getamortization_mtdStatusUpdate(Mdlamortization_method values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.Daamortization_mtdStatusUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetamortizationActiveLog")]
        [HttpGet]
        public HttpResponseMessage GetamortizationActiveLog(string amortization_gid)
        {
            Mdlamortization_method values = new Mdlamortization_method();
            objDaAgrMstApplication360.DaGetamortizationActiveLog(amortization_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAssessmentAgencyRating")]
        [HttpGet]
        public HttpResponseMessage GetAssessmentAgencyRating()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetAssessmentAgencyRating(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        // Assessment Agency Rating

        [ActionName("CreateAssessmentAgencyRating")]
        [HttpPost]
        public HttpResponseMessage CreateAssessmentAgencyRating(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateAssessmentAgencyRating(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAssessmentAgencyRating")]
        [HttpGet]
        public HttpResponseMessage EditAssessmentAgencyRating(string assessmentagencyrating_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditAssessmentAgencyRating(assessmentagencyrating_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAssessmentAgencyRating")]
        [HttpPost]
        public HttpResponseMessage UpdateAssessmentAgencyRating(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateAssessmentAgencyRating(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveAssessmentAgencyRating")]
        [HttpPost]
        public HttpResponseMessage InactiveAssessmentAgencyRating(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveAssessmentAgencyRating(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveAssessmentAgencyRatingHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveAssessmentAgencyRatingHistory(string assessmentagencyrating_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveAssessmentAgencyRatingHistory(objapplicationhistory, assessmentagencyrating_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        [ActionName("DeleteAssessmentAgencyRating")]
        [HttpGet]
        public HttpResponseMessage DeleteAssessmentAgencyRating(string assessmentagencyrating_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteAssessmentAgencyRating(assessmentagencyrating_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Security Classification

        [ActionName("GetSecurityClassification")]
        [HttpGet]
        public HttpResponseMessage GetSecurityClassification()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.GetSecurityClassification(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateSecurityClassification")]
        [HttpPost]
        public HttpResponseMessage CreateSecurityClassification(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateSecurityClassification(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditSecurityClassification")]
        [HttpGet]
        public HttpResponseMessage EditSecurityClassification(string securityclassification_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditSecurityClassification(securityclassification_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateSecurityClassification")]
        [HttpPost]
        public HttpResponseMessage UpdateSecurityClassification(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateSecurityClassification(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveSecurityClassification")]
        [HttpPost]
        public HttpResponseMessage InactiveSecurityClassification(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveSecurityClassification(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveSecurityClassificationHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveSecurityClassificationHistory(string securityclassification_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveSecurityClassificationHistory(objapplicationhistory, securityclassification_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }
        [ActionName("DeleteSecurityClassification")]
        [HttpGet]
        public HttpResponseMessage DeleteSecurityClassification(string securityclassification_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DeleteSecurityClassification(securityclassification_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Ownership Type

        [ActionName("GetOwnershipType")]
        [HttpGet]
        public HttpResponseMessage GetOwnershipType()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetOwnershipType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateOwnershipType")]
        [HttpPost]
        public HttpResponseMessage CreateOwnershipType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateOwnershipType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditOwnershipType")]
        [HttpGet]
        public HttpResponseMessage EditOwnershipType(string ownershiptype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditOwnershipType(ownershiptype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateOwnershipType")]
        [HttpPost]
        public HttpResponseMessage UpdateOwnershipType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateOwnershipType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveOwnershipType")]
        [HttpPost]
        public HttpResponseMessage InactiveOwnershipType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveOwnershipType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteOwnershipType")]
        [HttpGet]
        public HttpResponseMessage DeleteOwnershipType(string ownershiptype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteOwnershipType(ownershiptype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveOwnershipTypeHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveOwnershipTypeHistory(string ownershiptype_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveOwnershipTypeHistory(objapplicationhistory, ownershiptype_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        //Residence Type

        [ActionName("GetResidenceType")]
        [HttpGet]
        public HttpResponseMessage GetResidenceType()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetResidenceType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateResidenceType")]
        [HttpPost]
        public HttpResponseMessage CreateResidenceType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateResidenceType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditResidenceType")]
        [HttpGet]
        public HttpResponseMessage EditResidenceType(string residencetype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditResidenceType(residencetype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateResidenceType")]
        [HttpPost]
        public HttpResponseMessage UpdateResidenceType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateResidenceType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveResidenceType")]
        [HttpPost]
        public HttpResponseMessage InactiveResidenceType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveResidenceType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteResidenceType")]
        [HttpGet]
        public HttpResponseMessage DeleteResidenceType(string residencetype_gid)
        {
            result values = new result();

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteResidenceType(residencetype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveResidenceTypeHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveResidenceTypeHistory(string residencetype_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveResidenceTypeHistory(objapplicationhistory, residencetype_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        //License Type

        [ActionName("GetLicenseType")]
        [HttpGet]
        public HttpResponseMessage GetLicenseType()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetLicenseType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateLicenseType")]
        [HttpPost]
        public HttpResponseMessage CreateLicenseType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateLicenseType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditLicenseType")]
        [HttpGet]
        public HttpResponseMessage EditLicenseType(string licensetype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditLicenseType(licensetype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateLicenseType")]
        [HttpPost]
        public HttpResponseMessage UpdateLicenseType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateLicenseType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveLicenseType")]
        [HttpPost]
        public HttpResponseMessage InactiveLicenseType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveLicenseType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteLicenseType")]
        [HttpGet]
        public HttpResponseMessage DeleteLicenseType(string licensetype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteLicenseType(licensetype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveLicenseTypeHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveLicenseTypeHistory(string licensetype_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveLicenseTypeHistory(objapplicationhistory, licensetype_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        //InterestFrequency

        [ActionName("GetInterestFrequency")]
        [HttpGet]
        public HttpResponseMessage GetInterestFrequency()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetInterestFrequency(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateInterestFrequency")]
        [HttpPost]
        public HttpResponseMessage CreateInterestFrequency(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateInterestFrequency(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditInterestFrequency")]
        [HttpGet]
        public HttpResponseMessage EditInterestFrequency(string interestfrequency_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditInterestFrequency(interestfrequency_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateInterestFrequency")]
        [HttpPost]
        public HttpResponseMessage UpdateInterestFrequency(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateInterestFrequency(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveInterestFrequency")]
        [HttpPost]
        public HttpResponseMessage InactiveInterestFrequency(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveInterestFrequency(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteInterestFrequency")]
        [HttpGet]
        public HttpResponseMessage DeleteInterestFrequency(string interestfrequency_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteInterestFrequency(interestfrequency_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveInterestFrequencyHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveInterestFrequencyHistory(string interestfrequency_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveInterestFrequencyHistory(objapplicationhistory, interestfrequency_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        //Principal Frequency

        [ActionName("GetPrincipalFrequency")]
        [HttpGet]
        public HttpResponseMessage GetPrincipalFrequency()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetPrincipalFrequency(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreatePrincipalFrequency")]
        [HttpPost]
        public HttpResponseMessage CreatePrincipalFrequency(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreatePrincipalFrequency(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditPrincipalFrequency")]
        [HttpGet]
        public HttpResponseMessage EditPrincipalFrequency(string principalfrequency_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditPrincipalFrequency(principalfrequency_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatePrincipalFrequency")]
        [HttpPost]
        public HttpResponseMessage UpdatePrincipalFrequency(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdatePrincipalFrequency(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactivePrincipalFrequency")]
        [HttpPost]
        public HttpResponseMessage InactivePrincipalFrequency(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactivePrincipalFrequency(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeletePrincipalFrequency")]
        [HttpGet]
        public HttpResponseMessage DeletePrincipalFrequency(string principalfrequency_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeletePrincipalFrequency(principalfrequency_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactivePrincipalFrequencyHistory")]
        [HttpGet]
        public HttpResponseMessage InactivePrincipalFrequencyHistory(string principalfrequency_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactivePrincipalFrequencyHistory(objapplicationhistory, principalfrequency_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        // Loan Product List

        [ActionName("LoanProductList")]
        [HttpGet]
        public HttpResponseMessage LoanProductList()
        {
            MdlLoanProductList values = new MdlLoanProductList();
            objDaAgrMstApplication360.DaLoanProductList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Loan Sub Product

        [ActionName("GetLoanSubProduct")]
        [HttpGet]
        public HttpResponseMessage GetLoanSubProduct()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetLoanSubProduct(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateLoanSubProduct")]
        [HttpPost]
        public HttpResponseMessage CreateLoanSubProduct(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateLoanSubProduct(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditLoanSubProduct")]
        [HttpGet]
        public HttpResponseMessage EditLoanSubProduct(string loansubproduct_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditLoanSubProduct(loansubproduct_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateLoanSubProduct")]
        [HttpPost]
        public HttpResponseMessage UpdateLoanSubProduct(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateLoanSubProduct(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveLoanSubProduct")]
        [HttpPost]
        public HttpResponseMessage InactiveLoanSubProduct(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveLoanSubProduct(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteLoanSubProduct")]
        [HttpGet]
        public HttpResponseMessage DeleteLoanSubProduct(string loansubproduct_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteLoanSubProduct(loansubproduct_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("LoanSubProductInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage LoanSubProductInactiveLogview(string loansubproduct_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaLoanSubProductInactiveLogview(loansubproduct_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Colending Master

        [ActionName("GetColendingMaster")]
        [HttpGet]
        public HttpResponseMessage GetColendingMaster()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetColendingMaster(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateColendingMaster")]
        [HttpPost]
        public HttpResponseMessage CreateColendingMaster(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateColendingMaster(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditColendingMaster")]
        [HttpGet]
        public HttpResponseMessage EditColendingMaster(string colendingmaster_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditColendingMaster(colendingmaster_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateColendingMaster")]
        [HttpPost]
        public HttpResponseMessage UpdateColendingMaster(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateColendingMaster(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveColendingMaster")]
        [HttpPost]
        public HttpResponseMessage InactiveColendingMaster(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveColendingMaster(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteColendingMaster")]
        [HttpGet]
        public HttpResponseMessage DeleteColendingMaster(string colendingmaster_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteColendingMaster(colendingmaster_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveColendingMasterHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveColendingMaster(string colendingmaster_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveColendingMasterHistory(objapplicationhistory, colendingmaster_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }
        // Lender Type
        [ActionName("GetLenderType")]
        [HttpGet]
        public HttpResponseMessage GetLenderType()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetLenderType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateLenderType")]
        [HttpPost]
        public HttpResponseMessage CreateLenderType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateLenderType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditLenderType")]
        [HttpGet]
        public HttpResponseMessage EditLenderType(string lendertype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditLenderType(lendertype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateLenderType")]
        [HttpPost]
        public HttpResponseMessage UpdateLenderType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateLenderType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveLenderType")]
        [HttpPost]
        public HttpResponseMessage InactiveLenderType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveLenderType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteLenderType")]
        [HttpGet]
        public HttpResponseMessage DeleteLenderType(string lendertype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteLenderType(lendertype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveLenderTypeHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveLenderTypeHistory(string lendertype_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveLenderTypeHistory(objapplicationhistory, lendertype_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        // Assessment Agency List

        [ActionName("AssessmentAgencyList")]
        [HttpGet]
        public HttpResponseMessage AssessmentAgencyList()
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaAssessmentAgencyList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Credit Policy Compliance

        [ActionName("GetCreditPolicyCompliance")]
        [HttpGet]
        public HttpResponseMessage GetCreditPolicyCompliance()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetCreditPolicyCompliance(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateCreditPolicyCompliance")]
        [HttpPost]
        public HttpResponseMessage CreateCreditPolicyCompliance(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateCreditPolicyCompliance(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditCreditPolicyCompliance")]
        [HttpGet]
        public HttpResponseMessage EditCreditPolicyCompliance(string creditpolicycompliance_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditCreditPolicyCompliance(creditpolicycompliance_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCreditPolicyCompliance")]
        [HttpPost]
        public HttpResponseMessage UpdateCreditPolicyCompliance(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateCreditPolicyCompliance(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveCreditPolicyCompliance")]
        [HttpPost]
        public HttpResponseMessage InactiveCreditPolicyCompliance(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveCreditPolicyCompliance(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCreditPolicyCompliance")]
        [HttpGet]
        public HttpResponseMessage DeleteCreditPolicyCompliance(string creditpolicycompliance_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteCreditPolicyCompliance(creditpolicycompliance_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditPolicyComplianceInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage CreditPolicyComplianceInactiveLogview(string creditpolicycompliance_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaCreditPolicyComplianceInactiveLogview(creditpolicycompliance_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Assessment Criteria

        [ActionName("GetAssessmentCriteria")]
        [HttpGet]
        public HttpResponseMessage GetAssessmentCriteria()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetAssessmentCriteria(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateAssessmentCriteria")]
        [HttpPost]
        public HttpResponseMessage CreateAssessmentCriteria(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateAssessmentCriteria(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditAssessmentCriteria")]
        [HttpGet]
        public HttpResponseMessage EditAssessmentCriteria(string assessmentcriteria_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditAssessmentCriteria(assessmentcriteria_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateAssessmentCriteria")]
        [HttpPost]
        public HttpResponseMessage UpdateAssessmentCriteria(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateAssessmentCriteria(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveAssessmentCriteria")]
        [HttpPost]
        public HttpResponseMessage InactiveAssessmentCriteria(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveAssessmentCriteria(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteAssessmentCriteria")]
        [HttpGet]
        public HttpResponseMessage DeleteAssessmentCriteria(string assessmentcriteria_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteAssessmentCriteria(assessmentcriteria_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AssessmentCriteriaInactiveHistory")]
        [HttpGet]
        public HttpResponseMessage AssessmentCriteriaInactiveHistory(string assessmentcriteria_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaAssessmentCriteriaInactiveHistory(objapplicationhistory, assessmentcriteria_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        // Bureau Name List

        [ActionName("BureauNameList")]
        [HttpGet]
        public HttpResponseMessage BureauNameList()
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaBureauNameList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Bank Account Level List

        [ActionName("BankAccountLevelList")]
        [HttpGet]
        public HttpResponseMessage BankAccountLevelList()
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaBankAccountLevelList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Assessment Agency Rating List

        [ActionName("AssessmentAgencyRatingList")]
        [HttpGet]
        public HttpResponseMessage AssessmentAgencyRatingList()
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaAssessmentAgencyRatingList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // License Type List

        [ActionName("licensetypeList")]
        [HttpGet]
        public HttpResponseMessage licensetypeList()
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DalicensetypeList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Business Category List

        [ActionName("BusinessCategoryList")]
        [HttpGet]
        public HttpResponseMessage BusinessCategoryList()
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaBusinessCategoryList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // AML Category List

        [ActionName("AMLCategoryList")]
        [HttpGet]
        public HttpResponseMessage AMLCategoryList()
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaAMLCategoryList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Company Type List

        [ActionName("CompanyTypeList")]
        [HttpGet]
        public HttpResponseMessage CompanyTypeList()
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaCompanyTypeList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Company Document List

        [ActionName("CompanyDocumentList")]
        [HttpGet]
        public HttpResponseMessage CompanyDocumentList(string documenttypes_gid, string program_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaCompanyDocumentList(values,  documenttypes_gid, program_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Stackholder Type List

        [ActionName("GetUserTypeList")]
        [HttpGet]
        public HttpResponseMessage GetUserTypeList()
        {
            MdlUserType objMdlUserType = new MdlUserType();
            objDaAgrMstApplication360.DaGetUserTypeList(objMdlUserType);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlUserType);
        }

        [ActionName("GetUserTypeNoApplicantList")]
        [HttpGet]
        public HttpResponseMessage GetUserTypeNoApplicantList()
        {
            MdlUserType objMdlUserType = new MdlUserType();
            objDaAgrMstApplication360.DaGetUserTypeNoApplicantList(objMdlUserType);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlUserType);
        }


        // Get Designation List

        [ActionName("GetDesignationList")]
        [HttpGet]
        public HttpResponseMessage GetDesignationList()
        {
            MdlDesignation objMdlDesignation = new MdlDesignation();
            objDaAgrMstApplication360.DaGetDesignationList(objMdlDesignation);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlDesignation);
        }

        //Property in the Name of List 

        [ActionName("GetPropertyinNameList")]
        [HttpGet]
        public HttpResponseMessage GetPropertyinNameList()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetPropertyinNameList(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        // Gender List

        [ActionName("GenderList")]
        [HttpGet]
        public HttpResponseMessage GenderList()
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGenderList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Marital Status List

        [ActionName("GetMaritalStatusList")]
        [HttpGet]
        public HttpResponseMessage GetMaritalStatusList()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetMaritalStatusList(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        // Educational Qualification List

        [ActionName("EducationalQualificationList")]
        [HttpGet]
        public HttpResponseMessage EducationalQualificationList()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaEducationalQualificationList(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        // Income Type List

        [ActionName("IncomeTypeList")]
        [HttpGet]
        public HttpResponseMessage IncomeTypeList()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaIncomeTypeList(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        // Individual Proof List

        [ActionName("IndividualProofList")]
        [HttpGet]
        public HttpResponseMessage IndividualProofList()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaIndividualProofList(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        //Ownership Type List

        [ActionName("OwnershipTypeList")]
        [HttpGet]
        public HttpResponseMessage OwnershipTypeList()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaOwnershipTypeList(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        //Residence Type List

        [ActionName("ResidenceTypeList")]
        [HttpGet]
        public HttpResponseMessage ResidenceTypeList()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaResidenceTypeList(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        //Individual Document List

        [ActionName("IndividualDocumentList")]
        [HttpGet]
        public HttpResponseMessage IndividualDocumentList(string documenttypes_gid, string program_gid)
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaIndividualDocumentList(objapplication360, documenttypes_gid, program_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }
        //Bsr Code

        [ActionName("GetBsrCode")]
        [HttpGet]
        public HttpResponseMessage GetBsrCode()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetBsrCode(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateBsrCode")]
        [HttpPost]
        public HttpResponseMessage CreateBsrCode(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateBsrCode(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditBsrCode")]
        [HttpGet]
        public HttpResponseMessage EditBsrCode(string bsrcode_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditBsrCode(bsrcode_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateBsrCode")]
        [HttpPost]
        public HttpResponseMessage UpdateBsrCode(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateBsrCode(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveBsrCode")]
        [HttpPost]
        public HttpResponseMessage InactiveBsrCode(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveBsrCode(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBsrCode")]
        [HttpGet]
        public HttpResponseMessage DeleteBsrCode(string bsrcode_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteBsrCode(bsrcode_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BsrCodeInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage BsrCodeInactiveLogview(string bsrcode_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaBsrCodeInactiveLogview(bsrcode_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //Line of Activity

        [ActionName("GetLineofActivity")]
        [HttpGet]
        public HttpResponseMessage GetLineofActivity()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetLineofActivity(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateLineofActivity")]
        [HttpPost]
        public HttpResponseMessage CreateLineofActivity(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateLineofActivity(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditLineofActivity")]
        [HttpGet]
        public HttpResponseMessage EditLineofActivity(string lineofactivity_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditLineofActivity(lineofactivity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateLineofActivity")]
        [HttpPost]
        public HttpResponseMessage UpdateLineofActivity(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateLineofActivity(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveLineofActivity")]
        [HttpPost]
        public HttpResponseMessage InactiveLineofActivity(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveLineofActivity(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteLineofActivity")]
        [HttpGet]
        public HttpResponseMessage DeleteLineofActivity(string lineofactivity_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteLineofActivity(lineofactivity_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("LineofActivityInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage LineofActivityInactiveLogview(string lineofactivity_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaLineofActivityInactiveLogview(lineofactivity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //PSL Category

        [ActionName("GetPslCategory")]
        [HttpGet]
        public HttpResponseMessage GetPslCategory()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetPslCategory(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreatePslCategory")]
        [HttpPost]
        public HttpResponseMessage CreatePslCategory(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreatePslCategory(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditPslCategory")]
        [HttpGet]
        public HttpResponseMessage EditPslCategory(string pslcategory_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditPslCategory(pslcategory_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatePslCategory")]
        [HttpPost]
        public HttpResponseMessage UpdatePslCategory(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdatePslCategory(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactivePslCategory")]
        [HttpPost]
        public HttpResponseMessage InactivePslCategory(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactivePslCategory(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeletePslCategory")]
        [HttpGet]
        public HttpResponseMessage DeletePslCategory(string pslcategory_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeletePslCategory(pslcategory_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PslCategoryInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage PslCategoryInactiveLogview(string pslcategory_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaPslCategoryInactiveLogview(pslcategory_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Weaker Section

        [ActionName("GetWeakerSection")]
        [HttpGet]
        public HttpResponseMessage GetWeakerSection()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetWeakerSection(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateWeakerSection")]
        [HttpPost]
        public HttpResponseMessage CreateWeakerSection(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateWeakerSection(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditWeakerSection")]
        [HttpGet]
        public HttpResponseMessage EditWeakerSection(string weakersection_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditWeakerSection(weakersection_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateWeakerSection")]
        [HttpPost]
        public HttpResponseMessage UpdateWeakerSection(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateWeakerSection(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveWeakerSection")]
        [HttpPost]
        public HttpResponseMessage InactiveWeakerSection(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveWeakerSection(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteWeakerSection")]
        [HttpGet]
        public HttpResponseMessage DeleteWeakerSection(string weakersection_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteWeakerSection(weakersection_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("WeakerSectionInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage WeakerSectionInactiveLogview(string weakersection_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaWeakerSectionInactiveLogview(weakersection_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //PSL Purpose

        [ActionName("GetPslPurpose")]
        [HttpGet]
        public HttpResponseMessage GetPslPurpose()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetPslPurpose(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreatePslPurpose")]
        [HttpPost]
        public HttpResponseMessage CreatePslPurpose(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreatePslPurpose(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditPslPurpose")]
        [HttpGet]
        public HttpResponseMessage EditPslPurpose(string pslpurpose_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditPslPurpose(pslpurpose_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatePslPurpose")]
        [HttpPost]
        public HttpResponseMessage UpdatePslPurpose(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdatePslPurpose(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactivePslPurpose")]
        [HttpPost]
        public HttpResponseMessage InactivePslPurpose(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactivePslPurpose(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeletePslPurpose")]
        [HttpGet]
        public HttpResponseMessage DeletePslPurpose(string pslpurpose_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeletePslPurpose(pslpurpose_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PslPurposeInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage PslPurposeInactiveLogview(string pslpurpose_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaPslPurposeInactiveLogview(pslpurpose_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Occupation

        [ActionName("GetOccupation")]
        [HttpGet]
        public HttpResponseMessage GetOccupation()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetOccupation(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateOccupation")]
        [HttpPost]
        public HttpResponseMessage CreateOccupation(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateOccupation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditOccupation")]
        [HttpGet]
        public HttpResponseMessage EditOccupation(string occupation_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditOccupation(occupation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateOccupation")]
        [HttpPost]
        public HttpResponseMessage UpdateOccupation(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateOccupation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveOccupation")]
        [HttpPost]
        public HttpResponseMessage InactiveOccupation(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveOccupation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("OccupationInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage OccupationInactiveLogview(string occupation_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaOccupationInactiveLogview(occupation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("DeleteOccupation")]
        [HttpGet]
        public HttpResponseMessage DeleteOccupation(string occupation_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteOccupation(occupation_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }






        // Msme

        [ActionName("GetMsme")]
        [HttpGet]
        public HttpResponseMessage GetMsme()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetMsme(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateMsme")]
        [HttpPost]
        public HttpResponseMessage CreateMsme(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateMsme(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditMsme")]
        [HttpGet]
        public HttpResponseMessage EditMsme(string msme_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditMsme(msme_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateMsme")]
        [HttpPost]
        public HttpResponseMessage UpdateMsme(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateMsme(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveMsme")]
        [HttpPost]
        public HttpResponseMessage InactiveMsme(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveMsme(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("MsmeInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage MsmeInactiveLogview(string msme_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaMsmeInactiveLogview(msme_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteMsme")]
        [HttpGet]
        public HttpResponseMessage DeleteMsme(string msme_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteMsme(msme_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }




        // Turnover

        [ActionName("GetTurnover")]
        [HttpGet]
        public HttpResponseMessage GetTurnover()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetTurnover(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateTurnover")]
        [HttpPost]
        public HttpResponseMessage CreateTurnover(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateTurnover(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditTurnover")]
        [HttpGet]
        public HttpResponseMessage EditTurnover(string turnover_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditTurnover(turnover_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateTurnover")]
        [HttpPost]
        public HttpResponseMessage UpdateTurnover(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateTurnover(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveTurnover")]
        [HttpPost]
        public HttpResponseMessage InactiveTurnover(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveTurnover(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TurnoverInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage TurnoverInactiveLogview(string turnover_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaTurnoverInactiveLogview(turnover_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteTurnover")]
        [HttpGet]
        public HttpResponseMessage DeleteTurnover(string turnover_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteTurnover(turnover_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Purpose Column

        [ActionName("GetPurposecolumn")]
        [HttpGet]
        public HttpResponseMessage GetPurposecolumn()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetPurposecolumn(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreatePurposecolumn")]
        [HttpPost]
        public HttpResponseMessage CreatePurposecolumn(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreatePurposecolumn(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditPurposecolumn")]
        [HttpGet]
        public HttpResponseMessage EditPurposecolumn(string purposecolumn_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditPurposecolumn(purposecolumn_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatePurposecolumn")]
        [HttpPost]
        public HttpResponseMessage UpdatePurposecolumn(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdatePurposecolumn(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactivePurposecolumn")]
        [HttpPost]
        public HttpResponseMessage InactivePurposecolumn(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactivePurposecolumn(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("PurposecolumnInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage PurposecolumnInactiveLogview(string purposecolumn_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaPurposecolumnInactiveLogview(purposecolumn_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeletePurposecolumn")]
        [HttpGet]
        public HttpResponseMessage DeletePurposecolumn(string purposecolumn_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeletePurposecolumn(purposecolumn_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Investment

        [ActionName("GetInvestment")]
        [HttpGet]
        public HttpResponseMessage GetInvestment()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetInvestment(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateInvestment")]
        [HttpPost]
        public HttpResponseMessage CreateInvestment(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateInvestment(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditInvestment")]
        [HttpGet]
        public HttpResponseMessage EditInvestment(string investment_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditInvestment(investment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateInvestment")]
        [HttpPost]
        public HttpResponseMessage UpdateInvestment(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateInvestment(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveInvestment")]
        [HttpPost]
        public HttpResponseMessage InactiveInvestment(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveInvestment(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("InvestmentInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage InvestmentInactiveLogview(string investment_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaInvestmentInactiveLogview(investment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("DeleteInvestment")]
        [HttpGet]
        public HttpResponseMessage DeleteInvestment(string investment_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteInvestment(investment_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Nature of Entity

        [ActionName("GetNatureofEntity")]
        [HttpGet]
        public HttpResponseMessage GetNatureofEntity()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetNatureofEntity(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateNatureofEntity")]
        [HttpPost]
        public HttpResponseMessage CreateNatureofEntity(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateNatureofEntity(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditNatureofEntity")]
        [HttpGet]
        public HttpResponseMessage EditNatureofEntity(string natureofentity_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditNatureofEntity(natureofentity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateNatureofEntity")]
        [HttpPost]
        public HttpResponseMessage UpdateNatureofEntity(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateNatureofEntity(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveNatureofEntity")]
        [HttpPost]
        public HttpResponseMessage InactiveNatureofEntity(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveNatureofEntity(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("NatureofEntityInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage NatureofEntityInactiveLogview(string natureofentity_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaNatureofEntityInactiveLogview(natureofentity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteNatureofEntity")]
        [HttpGet]
        public HttpResponseMessage DeleteNatureofEntity(string natureofentity_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteNatureofEntity(natureofentity_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Bank Account Type
        [ActionName("GetBankAccountType")]
        [HttpGet]
        public HttpResponseMessage GetBankAccountType()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetBankAccountType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateBankAccountType")]
        [HttpPost]
        public HttpResponseMessage CreateBankAccountType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateBankAccountType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditBankAccountType")]
        [HttpGet]
        public HttpResponseMessage EditBankAccountType(string bankaccounttype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditBankAccountType(bankaccounttype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateBankAccountType")]
        [HttpPost]
        public HttpResponseMessage UpdateBankAccountType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateBankAccountType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveBankAccountType")]
        [HttpPost]
        public HttpResponseMessage InactiveBankAccountType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveBankAccountType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBankAccountType")]
        [HttpGet]
        public HttpResponseMessage DeleteBankAccountType(string bankaccounttype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteBankAccountType(bankaccounttype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveBankAccountTypeHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveBankAccountTypeHistory(string bankaccounttype_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveBankAccountTypeHistory(objapplicationhistory, bankaccounttype_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }
        //Bank Name
        [ActionName("GetBankName")]
        [HttpGet]
        public HttpResponseMessage GetBankName()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetBankName(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateBankName")]
        [HttpPost]
        public HttpResponseMessage CreateBankName(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateBankName(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditBankName")]
        [HttpGet]
        public HttpResponseMessage EditBankName(string BankName_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditBankName(BankName_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateBankName")]
        [HttpPost]
        public HttpResponseMessage UpdateBankName(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateBankName(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveBankName")]
        [HttpPost]
        public HttpResponseMessage InactiveBankName(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveBankName(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBankName")]
        [HttpGet]
        public HttpResponseMessage DeleteBankName(string BankName_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteBankName(BankName_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveBankNameHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveBankNameHistory(string BankName_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveBankNameHistory(objapplicationhistory, BankName_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }
        //Property
        [ActionName("GetProperty")]
        [HttpGet]
        public HttpResponseMessage GetProperty()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetProperty(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateProperty")]
        [HttpPost]
        public HttpResponseMessage CreateProperty(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateProperty(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditProperty")]
        [HttpGet]
        public HttpResponseMessage EditProperty(string Property_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditProperty(Property_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateProperty")]
        [HttpPost]
        public HttpResponseMessage UpdateProperty(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateProperty(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveProperty")]
        [HttpPost]
        public HttpResponseMessage InactiveProperty(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveProperty(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteProperty")]
        [HttpGet]
        public HttpResponseMessage DeleteProperty(string Property_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteProperty(Property_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactivePropertyHistory")]
        [HttpGet]
        public HttpResponseMessage InactivePropertyHistory(string Property_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactivePropertyHistory(objapplicationhistory, Property_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }
        //ClientDetails
        [ActionName("GetClientDetails")]
        [HttpGet]
        public HttpResponseMessage GetClientDetails()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetClientDetails(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateClientDetails")]
        [HttpPost]
        public HttpResponseMessage CreateClientDetails(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateClientDetails(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditClientDetails")]
        [HttpGet]
        public HttpResponseMessage EditClientDetails(string ClientDetails_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditClientDetails(ClientDetails_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateClientDetails")]
        [HttpPost]
        public HttpResponseMessage UpdateClientDetails(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateClientDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveClientDetails")]
        [HttpPost]
        public HttpResponseMessage InactiveClientDetails(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveClientDetails(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteClientDetails")]
        [HttpGet]
        public HttpResponseMessage DeleteClientDetails(string ClientDetails_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteClientDetails(ClientDetails_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveClientDetailsHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveClientDetailsHistory(string ClientDetails_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveClientDetailsHistory(objapplicationhistory, ClientDetails_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        //Supplier COntroller

        [ActionName("GetSupplier")]
        [HttpGet]
        public HttpResponseMessage GetSupplier()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetSupplier(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateSupplier")]
        [HttpPost]
        public HttpResponseMessage CreateSupplier(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateSupplier(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditSupplier")]
        [HttpGet]
        public HttpResponseMessage EditSupplier(string supplier_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditSupplier(supplier_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateSupplier")]
        [HttpPost]
        public HttpResponseMessage UpdateSupplier(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateSupplier(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveSupplier")]
        [HttpPost]
        public HttpResponseMessage InactiveSupplier(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveSupplier(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteSupplier")]
        [HttpGet]
        public HttpResponseMessage DeleteSupplier(string supplier_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteSupplier(supplier_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SuppierInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage SuppierInactiveLogview(string supplier_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaSupplierInactiveLogview(supplier_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Source of Contact

        [ActionName("GetSourceofContact")]
        [HttpGet]
        public HttpResponseMessage GetSourceofContact()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetSourceofContact(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateSourceofContact")]
        [HttpPost]
        public HttpResponseMessage CreateSourceofContact(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateSourceofContact(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditSourceofContact")]
        [HttpGet]
        public HttpResponseMessage EditSourceofContact(string sourceofcontact_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditSourceofContact(sourceofcontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateSourceofContact")]
        [HttpPost]
        public HttpResponseMessage UpdateSourceofContact(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateSourceofContact(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveSourceofContact")]
        [HttpPost]
        public HttpResponseMessage InactiveSourceofContact(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveSourceofContact(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveSourceofContactHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveSourceofContactHistory(string sourceofcontact_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveSourceofContactHistory(objapplicationhistory, sourceofcontact_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }
        [ActionName("DeleteSourceofContact")]
        [HttpGet]
        public HttpResponseMessage DeleteSourceofContact(string sourceofcontact_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteSourceofContact(sourceofcontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Call Type
        [ActionName("GetCallType")]
        [HttpGet]
        public HttpResponseMessage GetCallType()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetCallType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateCallType")]
        [HttpPost]
        public HttpResponseMessage CreateCallType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateCallType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditCallType")]
        [HttpGet]
        public HttpResponseMessage EditCallType(string calltype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditCallType(calltype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCallType")]
        [HttpPost]
        public HttpResponseMessage UpdateCallType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateCallType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveCallType")]
        [HttpPost]
        public HttpResponseMessage InactiveCallType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveCallType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveCallTypeHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveCallTypeHistory(string calltype_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveCallTypeHistory(objapplicationhistory, calltype_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        [ActionName("DeleteCallType")]
        [HttpGet]
        public HttpResponseMessage DeleteCallType(string calltype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteCallType(calltype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Tele calling - Function

        [ActionName("GetTelecallingFunction")]
        [HttpGet]
        public HttpResponseMessage GetTelecallingFunction()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetTelecallingFunction(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateTelecallingFunction")]
        [HttpPost]
        public HttpResponseMessage CreateTelecallingFunction(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateTelecallingFunction(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditTelecallingFunction")]
        [HttpGet]
        public HttpResponseMessage EditTelecallingFunction(string telecallingfunction_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditTelecallingFunction(telecallingfunction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateTelecallingFunction")]
        [HttpPost]
        public HttpResponseMessage UpdateTelecallingFunction(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateTelecallingFunction(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveTelecallingFunction")]
        [HttpPost]
        public HttpResponseMessage InactiveTelecallingFunction(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveTelecallingFunction(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveTelecallingFunctionHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveTelecallingFunctionHistory(string telecallingfunction_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveTelecallingFunctionHistory(objapplicationhistory, telecallingfunction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        [ActionName("DeleteTelecallingFunction")]
        [HttpGet]
        public HttpResponseMessage DeleteTelecallingFunction(string telecallingfunction_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteTelecallingFunction(telecallingfunction_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Call Received Number

        [ActionName("GetCallReceivedNumber")]
        [HttpGet]
        public HttpResponseMessage GetCallReceivedNumber()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetCallReceivedNumber(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateCallReceivedNumber")]
        [HttpPost]
        public HttpResponseMessage CreateCallReceivedNumber(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateCallReceivedNumber(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditCallReceivedNumber")]
        [HttpGet]
        public HttpResponseMessage EditCallReceivedNumber(string callreceivednumber_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditCallReceivedNumber(callreceivednumber_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCallReceivedNumber")]
        [HttpPost]
        public HttpResponseMessage UpdateCallReceivedNumber(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateCallReceivedNumber(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveCallReceivedNumber")]
        [HttpPost]
        public HttpResponseMessage InactiveCallReceivedNumber(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveCallReceivedNumber(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveCallReceivedNumberHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveCallReceivedNumberHistory(string callreceivednumber_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveCallReceivedNumberHistory(objapplicationhistory, callreceivednumber_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        [ActionName("DeleteCallReceivedNumber")]
        [HttpGet]
        public HttpResponseMessage DeleteCallReceivedNumber(string callreceivednumber_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteCallReceivedNumber(callreceivednumber_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Product

        [ActionName("GetProducts")]
        [HttpGet]
        public HttpResponseMessage GetProducts()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetProducts(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateProducts")]
        [HttpPost]
        public HttpResponseMessage CreateProducts(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateProducts(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVariety")]
        [HttpGet]
        public HttpResponseMessage GetVariety()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetVariety(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateVariety")]
        [HttpPost]
        public HttpResponseMessage CreateVariety(variety values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateVariety(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //[ActionName("UpdateVariety")]
        //[HttpPost]
        //public HttpResponseMessage UpdateVariety(variety values)
        //{
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaAgrMstApplication360.DaUpdateVariety(getsessionvalues.employee_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}

        [ActionName("DeleteVariety")]
        [HttpGet]
        public HttpResponseMessage DeleteVariety(string variety_gid)
        {
            variety values = new variety();
            objDaAgrMstApplication360.DaDeleteVariety(variety_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TempClear")]
        [HttpGet]
        public HttpResponseMessage TempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            variety objvalues = new variety();
            objDaAgrMstApplication360.DaTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditProduct")]
        [HttpGet]
        public HttpResponseMessage EditProducts(string product_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditProduct(product_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVarietyEditList")]
        [HttpGet]
        public HttpResponseMessage GetVarietyEditList(string product_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetVarietyEditList(product_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetVarietyTempEditList")]
        [HttpGet]
        public HttpResponseMessage GetVarietyTempEditList(string product_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplication360 values = new MdlMstApplication360();
            variety objvalues = new variety();
            objDaAgrMstApplication360.DaGetVarietyTempEditList(product_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateProduct")]
        [HttpPost]
        public HttpResponseMessage UpdateProduct(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateProduct(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveProduct")]
        [HttpPost]
        public HttpResponseMessage InactiveProduct(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveProduct(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveProductHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveProductHistory(string product_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveProductHistory(objapplicationhistory, product_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        [ActionName("DeleteProduct")]
        [HttpGet]
        public HttpResponseMessage DeleteProduct(string product_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteProduct(product_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDropDown")]
        [HttpGet]
        public HttpResponseMessage GetSectorDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Document Type

        // Add

        [ActionName("PostDocumentType")]
        [HttpPost]
        public HttpResponseMessage PostDocumentType(MdlDocumentType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaPostDocumentType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDocumentType")]
        [HttpGet]
        public HttpResponseMessage GetDocumentType()
        {
            MdlDocumentType objmaster = new MdlDocumentType();
            objDaAgrMstApplication360.DaGetDocumentType(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetDocumentEdit")]
        [HttpGet]
        public HttpResponseMessage GetDocumentEdit(string documenttypes_gid)
        {
            MdlDocumentType objmaster = new MdlDocumentType();
            objDaAgrMstApplication360.DaGetDocumentEdit(documenttypes_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("DocumentUpdate")]
        [HttpPost]
        public HttpResponseMessage DocumentUpdate(MdlDocumentType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDocumentUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete

        [ActionName("DeleteDocumentType")]
        [HttpGet]
        public HttpResponseMessage DeleteDocumentType(string documenttypes_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteDocumentType(documenttypes_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveDocumentType")]
        [HttpPost]
        public HttpResponseMessage InactiveDocumentType(MdlDocumentType values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveDocumentType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveDocumentTypeHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveDocumentTypeHistory(string documenttypes_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveDocumentTypeHistory(objapplicationhistory, documenttypes_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }

        //Group Document

        [ActionName("GetGroupDocument")]
        [HttpGet]
        public HttpResponseMessage GetGroupDocument()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetGroupDocument(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateGroupDocument")]
        [HttpPost]
        public HttpResponseMessage CreateGroupDocument(groupdocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateGroupDocument(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDocumentDropDown")]
        [HttpGet]
        public HttpResponseMessage GetDocumentDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetDocumentDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditGroupDocument")]
        [HttpGet]
        public HttpResponseMessage EditGroupDocument(string groupdocument_gid)
        {
            groupdocument values = new groupdocument();
            objDaAgrMstApplication360.DaEditGroupDocument(groupdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateGroupDocument")]
        [HttpPost]
        public HttpResponseMessage UpdateGroupDocument(groupdocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateGroupDocument(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveGroupDocument")]
        [HttpPost]
        public HttpResponseMessage InactiveGroupDocument(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveGroupDocument(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteGroupDocument")]
        [HttpGet]
        public HttpResponseMessage DeleteGroupDocument(string groupdocument_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteGroupDocument(groupdocument_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GroupDocumentInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GroupDocumentInactiveLogview(string groupdocument_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGroupDocumentInactiveLogview(groupdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProgram")]
        [HttpGet]
        public HttpResponseMessage GetProgram()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetProgram(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("PostProgramAdd")]
        [HttpPost]
        public HttpResponseMessage PostProgramAdd(MdlProgram values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaPostProgramAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditProgram")]
        [HttpGet]
        public HttpResponseMessage EditProgram(string program_gid)
        {
            MdlProgram objmaster = new MdlProgram();
            objDaAgrMstApplication360.DaEditProgram(program_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("UpdateProgram")]
        [HttpPost]
        public HttpResponseMessage UpdateProgram(MdlProgram values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateProgram(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveProgram")]
        [HttpPost]
        public HttpResponseMessage InactiveProgram(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveProgram(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteProgram")]
        [HttpGet]
        public HttpResponseMessage DeleteProgram(string program_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteProgram(program_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ProgramInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage ProgramInactiveLogview(string program_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaProgramInactiveLogview(program_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetLoanSubProductDropdown")]
        [HttpGet]
        public HttpResponseMessage GetLoanSubProductDropdown(string loanproduct_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetLoanSubProductDropdown(objapplication360, loanproduct_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("PostProductDetails")]
        [HttpPost]
        public HttpResponseMessage PostProductDetails(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaPostProductDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProgram2ProductList")]
        [HttpGet]
        public HttpResponseMessage GetProgram2ProductList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLoanProductList values = new MdlLoanProductList();
            objDaAgrMstApplication360.DaGetProgram2ProductList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProgram2ProductEditList")]
        [HttpGet]
        public HttpResponseMessage GetProgram2ProductEditList(string program_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLoanProductList values = new MdlLoanProductList();
            objDaAgrMstApplication360.DaGetProgram2ProductEditList(program_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("GetProgram2ProductEditTempList")]
        [HttpGet]
        public HttpResponseMessage GetProgram2ProductEditTempList(string program_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLoanProductList values = new MdlLoanProductList();
            //variety objvalues = new variety();
            objDaAgrMstApplication360.DaGetProgram2ProductEditTempList(program_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetProgramDocumentTempEditList")]
        [HttpGet]
        public HttpResponseMessage GetProgramDocumentTempEditList(string program_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlProgram values = new MdlProgram();
            //variety objvalues = new variety();
            objDaAgrMstApplication360.DaGetProgramDocumentTempEditList(program_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteProgram2Product")]
        [HttpGet]
        public HttpResponseMessage DeleteProgram2Product(string program2loanproduct_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            application360 values = new application360();
            objDaAgrMstApplication360.DaDeleteProgram2Product(program2loanproduct_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ProgramDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage ProgramDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument values = new uploaddocument();
            objDaAgrMstApplication360.DaProgramDocumentUpload(httpRequest, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PrograEditDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage PrograEditDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument values = new uploaddocument();
            objDaAgrMstApplication360.DaPrograEditDocumentUpload(httpRequest, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("TmpDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage TmpDocumentDelete(string tmp_documentGid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            uploaddocument objvalues = new uploaddocument();
            objDaAgrMstApplication360.DaTmpDocumentDelete(tmp_documentGid, objvalues, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetProgramDocumentDelete")]
        [HttpGet]
        public HttpResponseMessage GetProgramDocumentDelete(string programdocument_gid, string program_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            uploaddocument values = new uploaddocument();
            objDaAgrMstApplication360.DaGetProgramDocumentDelete(programdocument_gid, values, getsessionvalues.employee_gid, program_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



        [ActionName("ProgramTempClear")]
        [HttpGet]
        public HttpResponseMessage ProgramTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            MdlProgram objvalues = new MdlProgram();
            objDaAgrMstApplication360.DaProgramTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProgramDocumentEditList")]
        [HttpGet]
        public HttpResponseMessage GetProgramDocumentEditList(string program_gid)
        {
            MdlProgram values = new MdlProgram();
            objDaAgrMstApplication360.DaGetProgramDocumentEditList(program_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProgramMultiselectList")]
        [HttpGet]
        public HttpResponseMessage GetProgramMultiselectList(string program_gid)
        {
            MdlProgram values = new MdlProgram();
            objDaAgrMstApplication360.DaGetProgramMultiselectList(program_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEmployeeList")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeList()
        {
            MstDigitalSignature values = new MstDigitalSignature();
            objDaAgrMstApplication360.DaGetEmployeeList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SignatureUpload")]
        [HttpPost]
        public HttpResponseMessage SignatureUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploadSignature documentname = new uploadSignature();
            objDaAgrMstApplication360.SignatureUpload(httpRequest, documentname, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("GetDigitalSignatureList")]
        [HttpGet]
        public HttpResponseMessage GetDigitalSignatureList()
        {
            MstDigitalSignature values = new MstDigitalSignature();
            objDaAgrMstApplication360.DaGetDigitalSignatureList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteSignature")]
        [HttpGet]
        public HttpResponseMessage DeleteSignature(string digitalsignature_gid)
        {
            MstDigitalSignature values = new MstDigitalSignature();
            objDaAgrMstApplication360.DaDeleteSignature(digitalsignature_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Document Severity

        [ActionName("GetDocumentSeverity")]
        [HttpGet]
        public HttpResponseMessage GetDocumentSeverity()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetDocumentSeverity(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateDocumentSeverity")]
        [HttpPost]
        public HttpResponseMessage CreateDocumentSeverity(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateDocumentSeverity(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditDocumentSeverity")]
        [HttpGet]
        public HttpResponseMessage EditDocumentSeverity(string documentseverity_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.DaEditDocumentSeverity(documentseverity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateDocumentSeverity")]
        [HttpPost]
        public HttpResponseMessage UpdateDocumentSeverity(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateDocumentSeverity(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveDocumentSeverity")]
        [HttpPost]
        public HttpResponseMessage InactiveDocumentSeverity(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveDocumentSeverity(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteDocumentSeverity")]
        [HttpGet]
        public HttpResponseMessage DeleteDocumentSeverity(string documentseverity_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteDocumentSeverity(documentseverity_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DocumentSeverityInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage DocumentSeverityInactiveLogview(string documentseverity_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaDocumentSeverityInactiveLogview(documentseverity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Individual Document

        [ActionName("GetIndividualDocument")]
        [HttpGet]
        public HttpResponseMessage GetIndividualDocument()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetIndividualDocument(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreateIndividualDocument")]
        [HttpPost]
        public HttpResponseMessage CreateIndividualDocument(individualdocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateIndividualDocument(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualDropDown")]
        [HttpGet]
        public HttpResponseMessage GetIndividualDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetIndividualDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSeverityDropDown")]
        [HttpGet]
        public HttpResponseMessage GetSeverityDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetSeverityDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCheckList")]
        [HttpGet]
        public HttpResponseMessage GetCheckList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetCheckList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateCheckList")]
        [HttpPost]
        public HttpResponseMessage CreateCheckList(checklist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateCheckList(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCheckList")]
        [HttpGet]
        public HttpResponseMessage DeleteCheckList(string individualchecklist_gid)
        {
            checklist values = new checklist();
            objDaAgrMstApplication360.DaDeleteCheckList(individualchecklist_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CheckListTempClear")]
        [HttpGet]
        public HttpResponseMessage CheckListTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            checklist objvalues = new checklist();
            objDaAgrMstApplication360.DaCheckListTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditIndividualDocument")]
        [HttpGet]
        public HttpResponseMessage EditIndividualDocument(string individualdocument_gid)
        {
            individualdocument values = new individualdocument();
            objDaAgrMstApplication360.DaEditIndividualDocument(individualdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetCheckEditList")]
        [HttpGet]
        public HttpResponseMessage GetCheckEditList(string individualdocument_gid)
        {
            checklist values = new checklist();
            objDaAgrMstApplication360.DaGetCheckEditList(individualdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCheckListTempEditList")]
        [HttpGet]
        public HttpResponseMessage GetCheckListTempEditList(string individualdocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplication360 values = new MdlMstApplication360();
            variety objvalues = new variety();
            objDaAgrMstApplication360.DaGetCheckListTempEditList(individualdocument_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateIndividualDocument")]
        [HttpPost]
        public HttpResponseMessage UpdateIndividualDocument(individualdocument values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateIndividualDocument(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveIndividualDocument")]
        [HttpPost]
        public HttpResponseMessage InactiveIndividualDocument(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveIndividualDocument(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveIndividualDocumentHistory")]
        [HttpGet]
        public HttpResponseMessage InactiveIndividualDocumentHistory(string individualdocument_gid)
        {
            ApplicationInactiveHistory objapplicationhistory = new ApplicationInactiveHistory();
            objDaAgrMstApplication360.DaInactiveIndividualDocumentHistory(objapplicationhistory, individualdocument_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objapplicationhistory);
        }
        [ActionName("DeleteIndividualDocument")]
        [HttpGet]
        public HttpResponseMessage DeleteIndividualDocument(string individualdocument_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteIndividualDocument(individualdocument_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProgramDropDown")]
        [HttpGet]
        public HttpResponseMessage GetProgramDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetProgramDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetEntityDropDown")]
        [HttpGet]
        public HttpResponseMessage GetEntityDropDown()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetEntityDropDown(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //CHECK LIST

        [ActionName("GetCompanyCheckList")]
        [HttpGet]
        public HttpResponseMessage GetCompanyCheckList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetCompanyCheckList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateCompanyCheckList")]
        [HttpPost]
        public HttpResponseMessage CreateCompanyCheckList(checklist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateCompanyCheckList(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCompanyCheckList")]
        [HttpGet]
        public HttpResponseMessage DeleteCompanyCheckList(string companychecklist_gid)
        {
            variety values = new variety();
            objDaAgrMstApplication360.DaDeleteCompanyCheckList(companychecklist_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CompanyCheckListTempClear")]
        [HttpGet]
        public HttpResponseMessage CompanyCheckListTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            variety objvalues = new variety();
            objDaAgrMstApplication360.DaCompanyCheckListTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCompanyCheckListEditList")]
        [HttpGet]
        public HttpResponseMessage GetCompanyCheckListEditList(string companydocument_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetCompanyCheckListEditList(companydocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetCompanyCheckListTempEditList")]
        [HttpGet]
        public HttpResponseMessage GetCompanyCheckListTempEditList(string companydocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplication360 values = new MdlMstApplication360();
            variety objvalues = new variety();
            objDaAgrMstApplication360.DaGetCompanyCheckListTempEditList(companydocument_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //Group Check List



        [ActionName("GetGroupCheckList")]
        [HttpGet]
        public HttpResponseMessage GetGroupCheckList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetGroupCheckList(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateGroupCheckList")]
        [HttpPost]
        public HttpResponseMessage CreateGroupCheckList(checklist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreateGroupCheckList(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteGroupCheckList")]
        [HttpGet]
        public HttpResponseMessage DeleteGroupCheckList(string groupchecklist_gid)
        {
            variety values = new variety();
            objDaAgrMstApplication360.DaDeleteGroupCheckList(groupchecklist_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GroupCheckListTempClear")]
        [HttpGet]
        public HttpResponseMessage GroupCheckListTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result values = new result();
            variety objvalues = new variety();
            objDaAgrMstApplication360.DaGroupCheckListTempClear(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGroupCheckListEditList")]
        [HttpGet]
        public HttpResponseMessage GetGroupCheckListEditList(string groupdocument_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetGroupCheckListEditList(groupdocument_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetGroupCheckListTempEditList")]
        [HttpGet]
        public HttpResponseMessage GetGroupCheckListTempEditList(string groupdocument_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplication360 values = new MdlMstApplication360();
            variety objvalues = new variety();
            objDaAgrMstApplication360.DaGetGroupCheckListTempEditList(groupdocument_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Group Document List

        [ActionName("GroupDocumentList")]
        [HttpGet]
        public HttpResponseMessage GroupDocumentList()
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGroupDocumentList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Credit Ops Group Add
        [ActionName("PostCreditGroupAdd")]
        [HttpPost]
        public HttpResponseMessage PostCreditGroupAdd(MdlCreditOpsGroupAdd values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaPostCreditGroupAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Credit Group Summary
        [ActionName("GetCreditOpsGroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetCreditOpsGroupSummary()
        {
            MdlCreditOpsGroup objmaster = new MdlCreditOpsGroup();
            objDaAgrMstApplication360.DaGetCreditOpsGroupSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        [ActionName("GetCreditopsgroupHeads")]
        [HttpGet]
        public HttpResponseMessage GetCreditopsgroupHeads(string creditopsgroupmapping_gid)
        {
            creditopsheads values = new creditopsheads();
            objDaAgrMstApplication360.DaGetCreditopsgroupHeads(creditopsgroupmapping_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditOpsGroupEdit")]
        [HttpGet]
        public HttpResponseMessage GetCreditOpsGroupEdit(string creditopsgroupmapping_gid)
        {
            MdlCreditOpsGroup objmaster = new MdlCreditOpsGroup();
            objDaAgrMstApplication360.DaGetCreditOpsGroupEdit(creditopsgroupmapping_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        [ActionName("PostCreditOpsGroupUpdate")]
        [HttpPost]
        public HttpResponseMessage PostCreditOpsGroupUpdate(MdlCreditOpsGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaPostCreditOpsGroupUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCreditOpsgroupInactive")]
        [HttpPost]
        public HttpResponseMessage PostCreditOpsgroupInactive(MdlCreditOpsGroup values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaPostCreditOpsgroupInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCreditOpsgroupInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetCreditOpsgroupInactiveLogview(string creditopsgroupmapping_gid)
        {
            MdlCreditOpsGroup values = new MdlCreditOpsGroup();
            objDaAgrMstApplication360.DaGetCreditOpsgroupInactiveLogview(creditopsgroupmapping_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Scope

        [ActionName("GetScope")]
        [HttpGet]
        public HttpResponseMessage GetScope()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetScope(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreatetScope")]
        [HttpPost]
        public HttpResponseMessage CreatetScope(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreatetScope(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditScope")]
        [HttpGet]
        public HttpResponseMessage EditScope(string scope_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.EditScope(scope_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateScope")]
        [HttpPost]
        public HttpResponseMessage UpdateScope(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateScope(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveScope")]
        [HttpPost]
        public HttpResponseMessage InactiveScope(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveScope(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteScope")]
        [HttpGet]
        public HttpResponseMessage DeleteScope(string scope_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteScope(scope_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ScopeInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage ScopeInactiveLogview(string scope_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaScopeInactiveLogview(scope_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Other Creditor Applicant Type

        [ActionName("GetOtherCreditorApplicantType")]
        [HttpGet]
        public HttpResponseMessage GetOtherCreditorApplicantType()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetOtherCreditorApplicantType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("GetOtherCreditorApplicantTypedropdown")]
        [HttpGet]
        public HttpResponseMessage GetOtherCreditorApplicantTypedropdown()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetOtherCreditorApplicantTypedropdown(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }


        [ActionName("CreatetOtherCreditorApplicantType")]
        [HttpPost]
        public HttpResponseMessage CreatetOtherCreditorApplicantType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreatetOtherCreditorApplicantType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditOtherCreditorApplicantType")]
        [HttpGet]
        public HttpResponseMessage EditOtherCreditorApplicantType(string othercreditorapplicanttype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.EditOtherCreditorApplicantType(othercreditorapplicanttype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateOtherCreditorApplicantType")]
        [HttpPost]
        public HttpResponseMessage UpdateOtherCreditorApplicantType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateOtherCreditorApplicantType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveOtherCreditorApplicantType")]
        [HttpPost]
        public HttpResponseMessage InactiveOtherCreditorApplicantType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveOtherCreditorApplicantType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteOtherCreditorApplicantType")]
        [HttpGet]
        public HttpResponseMessage DeleteOtherCreditorApplicantType(string othercreditorapplicanttype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteOtherCreditorApplicantType(othercreditorapplicanttype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("OtherCreditorApplicantTypeInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage OtherCreditorApplicantTypeInactiveLogview(string othercreditorapplicanttype_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaOtherCreditorApplicantTypeInactiveLogview(othercreditorapplicanttype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Milestone Payment Type

        [ActionName("GetMilestonePaymentType")]
        [HttpGet]
        public HttpResponseMessage GetMilestonePaymentType()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetMilestonePaymentType(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreatetMilestonePaymentType")]
        [HttpPost]
        public HttpResponseMessage CreatetMilestonePaymentType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreatetMilestonePaymentType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditMilestonePaymentType")]
        [HttpGet]
        public HttpResponseMessage EditMilestonePaymentType(string milestonepaymenttype_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.EditMilestonePaymentType(milestonepaymenttype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateMilestonePaymentType")]
        [HttpPost]
        public HttpResponseMessage UpdateMilestonePaymentType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateMilestonePaymentType(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveMilestonePaymentType")]
        [HttpPost]
        public HttpResponseMessage InactiveMilestonePaymentType(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveMilestonePaymentType(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteMilestonePaymentType")]
        [HttpGet]
        public HttpResponseMessage DeleteMilestonePaymentType(string milestonepaymenttype_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteMilestonePaymentType(milestonepaymenttype_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MilestonePaymentTypeInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage MilestonePaymentTypeInactiveLogview(string milestonepaymenttype_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaMilestonePaymentTypeInactiveLogview(milestonepaymenttype_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Nature/Form/State of Commodity

        [ActionName("GetNatureFormStateofCommodity")]
        [HttpGet]
        public HttpResponseMessage GetNatureFormStateofCommodity()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetNatureFormStateofCommodity(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }

        [ActionName("CreatetNatureFormStateofCommodity")]
        [HttpPost]
        public HttpResponseMessage CreatetNatureFormStateofCommodity(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaCreatetNatureFormStateofCommodity(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditNatureFormStateofCommodity")]
        [HttpGet]
        public HttpResponseMessage EditNatureFormStateofCommodity(string natureformstateofcommodity_gid)
        {
            application360 values = new application360();
            objDaAgrMstApplication360.EditNatureFormStateofCommodity(natureformstateofcommodity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateNatureFormStateofCommodity")]
        [HttpPost]
        public HttpResponseMessage UpdateNatureFormStateofCommodity(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaUpdateNatureFormStateofCommodity(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveNatureFormStateofCommodity")]
        [HttpPost]
        public HttpResponseMessage InactiveNatureFormStateofCommodity(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaInactiveNatureFormStateofCommodity(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteNatureFormStateofCommodity")]
        [HttpGet]
        public HttpResponseMessage DeleteNatureFormStateofCommodity(string natureformstateofcommodity_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAgrMstApplication360.DaDeleteNatureFormStateofCommodity(natureformstateofcommodity_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

       
        [ActionName("NatureFormStateofCommodityInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage NatureFormStateofCommodityInactiveLogview(string natureformstateofcommodity_gid)
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaNatureFormStateofCommodityInactiveLogview(natureformstateofcommodity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //--------Get Asscociate From SBA-----//
        [ActionName("GetAssociateMasterASC")]
        [HttpGet]
        public HttpResponseMessage getAssociateMasterASC()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaGetAssociateMasterASC(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }


        [ActionName("GetInstitutionDocTypeList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionDocTypeList(string program_gid)
        {
            MdlInstitutionDocType objMdlInstitutionDocType = new MdlInstitutionDocType();
            objDaAgrMstApplication360.DaGetInstitutionDocTypeList(objMdlInstitutionDocType, program_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlInstitutionDocType);
        }
        [ActionName("GetIndividualDocTypeList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualDocTypeList(string program_gid)
        {
            MdlIndividualDocType objMdlIndividualDocType = new MdlIndividualDocType();
            objDaAgrMstApplication360.DaGetIndividualDocTypeList(objMdlIndividualDocType, program_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlIndividualDocType);
        }

        // Company Document List

        [ActionName("CompanyOnboardDocumentList")]
        [HttpGet]
        public HttpResponseMessage CompanyOnboardDocumentList()
        {
            MdlMstApplication360 values = new MdlMstApplication360();
            objDaAgrMstApplication360.DaCompanyOnboardDocumentList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("IndividualOnboardDocumentList")]
        [HttpGet]
        public HttpResponseMessage IndividualOnboardDocumentList()
        {
            MdlMstApplication360 objapplication360 = new MdlMstApplication360();
            objDaAgrMstApplication360.DaIndividualOnboardDocumentList(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }
    }

}