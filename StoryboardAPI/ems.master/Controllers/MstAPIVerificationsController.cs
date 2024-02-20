using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System;
using System.Data.Odbc;
using Newtonsoft.Json;

/// <summary>
/// (It's used for APIVerifications)APIVerifications Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Praveen Raj </remarks>
namespace ems.master.Controllers
{
    [RoutePrefix("api/MstAPIVerifications")]
    [Authorize]
    public class MstAPIVerificationsController : ApiController
    {
        DaMstAPIVerifications objDaMstAPIVerifications = new DaMstAPIVerifications();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        string msSQL;
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;

        [ActionName("PostTAN")]
        [HttpPost]
        public HttpResponseMessage PostTAN(MdlTAN values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstAPIVerifications.DaPostTAN(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetTAN")]
        [HttpGet]
        public HttpResponseMessage GetTANsummary(string function_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTAN values = new MdlTAN();
            objDaMstAPIVerifications.DaGetTANsummary(function_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostCompanyLLP")]
        [HttpPost]
        public HttpResponseMessage PostCompanyLLP(MdlCIN values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstAPIVerifications.DaPostCompanyLLP(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCompanyLLP")]
        [HttpGet]
        public HttpResponseMessage GetCompanyLLP(string function_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCIN values = new MdlCIN();
            objDaMstAPIVerifications.DaGetCompanyLLP(function_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostMCASignature")]
        [HttpPost]
        public HttpResponseMessage PostMCASignature(MdlCIN values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstAPIVerifications.DaPostMCASignature(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetMCASignature")]
        [HttpGet]
        public HttpResponseMessage GetMCASignature(string function_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCIN values = new MdlCIN();
            objDaMstAPIVerifications.DaGetMCASignature(function_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostIECDetailed")]
        [HttpPost]
        public HttpResponseMessage PostIECDetailed(MdlIECDetailed values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstAPIVerifications.DaPostIECDetailed(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIECDetailed")]
        [HttpGet]
        public HttpResponseMessage GetIECDetailed(string function_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIECDetailed values = new MdlIECDetailed();
            objDaMstAPIVerifications.DaGetIECDetailed(function_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       //FSSAI
        [ActionName("PostFSSAI")]
        [HttpPost]
        public HttpResponseMessage PostFSSAI(MdlFSSAI values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstAPIVerifications.DaPostFSSAI(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetFSSAI")]
        [HttpGet]
        public HttpResponseMessage GetFSSAI(string function_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFSSAI values = new MdlFSSAI();
            objDaMstAPIVerifications.DaGetFSSAI(function_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //FDA 
        [ActionName("PostFDA")]
        [HttpPost]
        public HttpResponseMessage PostFDA(MdlFDA values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstAPIVerifications.DaPostFDA(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetFDA")]
        [HttpGet]
        public HttpResponseMessage GetFDA(string function_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFDA values = new MdlFDA();
            objDaMstAPIVerifications.DaGetFDA(function_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Delete
        [ActionName("TANdelete")]
        [HttpGet]
        public HttpResponseMessage TANdelete(string tandtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTAN values = new MdlTAN();
            objDaMstAPIVerifications.DaGetTANdelete(tandtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CompanyLLPNodelete")]
        [HttpGet]
        public HttpResponseMessage CompanyLLPNodelete(string companyllpno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTAN values = new MdlTAN();
            objDaMstAPIVerifications.DaCompanyLLPNodelete(companyllpno_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("MCASigndelete")]
        [HttpGet]
        public HttpResponseMessage MCASigndelete(string mcasignatories_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTAN values = new MdlTAN();
            objDaMstAPIVerifications.DaMCASigndelete(mcasignatories_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IECdelete")]
        [HttpGet]
        public HttpResponseMessage IECdelete(string iecdtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTAN values = new MdlTAN();
            objDaMstAPIVerifications.DaIECdelete(iecdtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("FSSAIdelete")]
        [HttpGet]
        public HttpResponseMessage FSSAIdelete(string fssailicenseauthentication_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTAN values = new MdlTAN();
            objDaMstAPIVerifications.DaFSSAIdelete(fssailicenseauthentication_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("FDAdelete")]
        [HttpGet]
        public HttpResponseMessage FDAdelete(string fdalicenseauthentication_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTAN values = new MdlTAN();
            objDaMstAPIVerifications.DaFDAdelete(fdalicenseauthentication_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetStateList")]
        [HttpGet]
        public HttpResponseMessage GetStateList()
        {
            MdlMstGST objMdlMstGST = new MdlMstGST();
            objDaMstAPIVerifications.DaGetStateList(objMdlMstGST);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlMstGST);
        }
        [ActionName("GetGSTList")]
        [HttpGet]
        public HttpResponseMessage GetGSTList(string institution_gid)
        {
            MdlMstGST objMdlMstGST = new MdlMstGST();
            objDaMstAPIVerifications.DaGetGSTList(objMdlMstGST, institution_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlMstGST);
        }

        [ActionName("CompanyLLPViewDetails")]
        [HttpGet]
        public HttpResponseMessage CompanyLLPViewDetails(string companyllpno_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCompanyLLPDetails values = new MdlCompanyLLPDetails();
            objDaMstAPIVerifications.DaCompanyLLPViewDetails(companyllpno_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("FDAViewDetails")]
        [HttpGet]
        public HttpResponseMessage FDAViewDetails(string fdalicenseauthentication_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFDADetails values = new MdlFDADetails();
            objDaMstAPIVerifications.DaFDAViewDetails(fdalicenseauthentication_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("FSSAIViewDetails")]
        [HttpGet]
        public HttpResponseMessage FSSAIViewDetails(string fssailicenseauthentication_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFSSAIDetails values = new MdlFSSAIDetails();
            objDaMstAPIVerifications.DaFSSAIViewDetails(fssailicenseauthentication_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("MCASignatoriesViewDetails")]
        [HttpGet]
        public HttpResponseMessage MCASignatoriesViewDetails(string mcasignatories_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMCASignatoryDetails values = new MdlMCASignatoryDetails();
            objDaMstAPIVerifications.DaMCASignatoriesViewDetails(mcasignatories_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GSTAuthenticationViewDetails")]
        [HttpGet]
        public HttpResponseMessage GSTAuthenticationViewDetails(string institution2branch_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGSTAuthenticationDetails values = new MdlGSTAuthenticationDetails();

            msSQL = " select CAST(response as char) as response" +
                       " from ocs_trn_tgspinauthentication where function_gid='" + institution2branch_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlGSTAuthenticationDetails>(objODBCDatareader["response"].ToString());

            }

            //objDaMstAPIVerifications.DaGSTAuthenticationViewDetails(institution2branch_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GSTVerificationViewDetails")]
        [HttpGet]
        public HttpResponseMessage GSTVerificationViewDetails(string institution2branch_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGSTVerificationDetails values = new MdlGSTVerificationDetails();

            msSQL = " select CAST(response as char) as response" +
                       " from ocs_trn_tgspinverification where function_gid='" + institution2branch_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlGSTVerificationDetails>(objODBCDatareader["response"].ToString());

            }

            //objDaMstAPIVerifications.DaGSTVerificationViewDetails(institution2branch_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GSTReturnFillingViewDetails")]
        [HttpGet]
        public HttpResponseMessage GSTReturnFillingViewDetails(string institution2branch_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGSPGSTReturnFilingDetails values = new MdlGSPGSTReturnFilingDetails();

            msSQL = " select CAST(response as char) as response" +
                      " from ocs_trn_tgstreturnfilling where function_gid='" + institution2branch_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlGSPGSTReturnFilingDetails>(objODBCDatareader["response"].ToString());

            }

            //objDaMstAPIVerifications.DaGSTReturnFillingViewDetails(institution2branch_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("IECProfileViewDetails")]
        [HttpGet]
        public HttpResponseMessage IECProfileViewDetails(string iecdtl_gid)
        {
            string msSQL;
            dbconn objdbconn = new dbconn();
            cmnfunctions objcmnfunctions = new cmnfunctions();
            OdbcDataReader objODBCDatareader;

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIECProfileDetails values = new MdlIECProfileDetails();
            msSQL = " select CAST(response as char) as response" +
                       " from ocs_trn_tiecdtl where iecdtl_gid='" + iecdtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlIECProfileDetails>(objODBCDatareader["response"].ToString());

            }
            //   objDaMstAPIVerifications.DaIECProfileViewDetails(iecdtl_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //LPG ID
        [ActionName("PostLPGID")]
        [HttpPost]
        public HttpResponseMessage PostLPGID(MdlLPGID values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstAPIVerifications.DaPostLPGID(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLPGIDList")]
        [HttpGet]
        public HttpResponseMessage GetLPGIDList(string function_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLPGID values = new MdlLPGID();
            objDaMstAPIVerifications.DaGetLPGIDList(function_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("LPGIDdelete")]
        [HttpGet]
        public HttpResponseMessage LPGIDdelete(string lpgiddtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLPGID values = new MdlLPGID();
            objDaMstAPIVerifications.DaLPGIDdelete(lpgiddtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("LPGIDViewDetails")]
        [HttpGet]
        public HttpResponseMessage LPGIDViewDetails(string lpgiddtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLPGIDAuthenticationDetails values = new MdlLPGIDAuthenticationDetails();
            msSQL = " select CAST(response as char) as response" +
                       " from ocs_trn_tlpgiddtl where lpgiddtl_gid='" + lpgiddtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlLPGIDAuthenticationDetails>(objODBCDatareader["response"].ToString());

            }
            objODBCDatareader.Close();
            //   objDaMstAPIVerifications.DaLPGIDViewDetails(lpgiddtl_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //SHOP AND ESTABLISHMENT
        [ActionName("PostShop")]
        [HttpPost]
        public HttpResponseMessage PostShop(MdlShop values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstAPIVerifications.DaPostShop(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetShopList")]
        [HttpGet]
        public HttpResponseMessage GetShopList(string function_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlShop values = new MdlShop();
            objDaMstAPIVerifications.DaGetShopList(function_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Shopdelete")]
        [HttpGet]
        public HttpResponseMessage Shopdelete(string shopandestablishment_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlShop values = new MdlShop();
            objDaMstAPIVerifications.DaShopdelete(shopandestablishment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ShopViewDetails")]
        [HttpGet]
        public HttpResponseMessage ShopViewDetails(string shopandestablishment_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlShopAndEstablishmentDetails values = new MdlShopAndEstablishmentDetails();
            msSQL = " select CAST(response as char) as response" +
                       " from ocs_trn_tshopandestablishment where shopandestablishment_gid='" + shopandestablishment_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlShopAndEstablishmentDetails>(objODBCDatareader["response"].ToString());
            }
            objODBCDatareader.Close();
            //objDaMstAPIVerifications.DaFDAViewDetails(shopandestablishment_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Vehicle RC Auth Advanced
        [ActionName("PostRCAuthAdvanced")]
        [HttpPost]
        public HttpResponseMessage PostRCAuthAdvanced(MdlRCAuthAdvanced values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstAPIVerifications.DaPostRCAuthAdvanced(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRCAuthAdvancedList")]
        [HttpGet]
        public HttpResponseMessage GetRCAuthAdvancedList(string function_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRCAuthAdvanced values = new MdlRCAuthAdvanced();
            objDaMstAPIVerifications.DaGetRCAuthAdvancedList(function_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("RCAuthAdvanceddelete")]
        [HttpGet]
        public HttpResponseMessage RCAuthAdvanceddelete(string vehiclercauthadvanced_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRCAuthAdvanced values = new MdlRCAuthAdvanced();
            objDaMstAPIVerifications.DaRCAuthAdvanceddelete(vehiclercauthadvanced_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("RCAuthAdvancedViewDetails")]
        [HttpGet]
        public HttpResponseMessage RCAuthAdvancedViewDetails(string vehiclercauthadvanced_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlVehicleRCAuthAdvancedDetails values = new MdlVehicleRCAuthAdvancedDetails();
            msSQL = " select CAST(response as char) as response" +
                       " from ocs_trn_tvehiclercauthadvanced where vehiclercauthadvanced_gid='" + vehiclercauthadvanced_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlVehicleRCAuthAdvancedDetails>(objODBCDatareader["response"].ToString());

            }
            objODBCDatareader.Close();
            //   objDaMstAPIVerifications.DaRCAuthAdvancedViewDetails(vehiclercauthadvanced_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Vehicle RC Search
        [ActionName("PostRCSearch")]
        [HttpPost]
        public HttpResponseMessage PostRCSearch(MdlRCSearch values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstAPIVerifications.DaPostRCSearch(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRCSearchList")]
        [HttpGet]
        public HttpResponseMessage GetRCSearchList(string function_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRCSearch values = new MdlRCSearch();
            objDaMstAPIVerifications.DaGetRCSearchList(function_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("RCSearchdelete")]
        [HttpGet]
        public HttpResponseMessage RCSearchdelete(string vehiclercsearch_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRCSearch values = new MdlRCSearch();
            objDaMstAPIVerifications.DaRCSearchdelete(vehiclercsearch_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("RCSearchViewDetails")]
        [HttpGet]
        public HttpResponseMessage RCSearchViewDetails(string vehiclercsearch_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlVehicleRCSearchDetails values = new MdlVehicleRCSearchDetails();
            msSQL = " select CAST(response as char) as response" +
                       " from ocs_trn_tvehiclercsearch where vehiclercsearch_gid='" + vehiclercsearch_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlVehicleRCSearchDetails>(objODBCDatareader["response"].ToString());

            }
            objODBCDatareader.Close();
            //   objDaMstAPIVerifications.DaRCSearchViewDetails(vehiclercsearch_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Property Tax
        [ActionName("PostPropertyTax")]
        [HttpPost]
        public HttpResponseMessage PostPropertyTax(MdlPropertyTax values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaMstAPIVerifications.DaPostPropertyTax(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPropertyTaxList")]
        [HttpGet]
        public HttpResponseMessage GetPropertyTaxList(string function_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPropertyTax values = new MdlPropertyTax();
            objDaMstAPIVerifications.DaGetPropertyTaxList(function_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PropertyTaxdelete")]
        [HttpGet]
        public HttpResponseMessage PropertyTaxdelete(string propertytax_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPropertyTax values = new MdlPropertyTax();
            objDaMstAPIVerifications.DaPropertyTaxdelete(propertytax_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PropertyTaxViewDetails")]
        [HttpGet]
        public HttpResponseMessage PropertyTaxViewDetails(string propertytax_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPropertyTaxDetails values = new MdlPropertyTaxDetails();
            msSQL = " select CAST(response as char) as response" +
                       " from ocs_trn_tpropertytax where propertytax_gid='" + propertytax_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlPropertyTaxDetails>(objODBCDatareader["response"].ToString());

            }
            objODBCDatareader.Close();
            //   objDaMstAPIVerifications.DaPropertyTaxViewDetails(propertytax_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Application KycAPI Lists

        [ActionName("AppnTANList")]
        [HttpGet]
        public HttpResponseMessage AppnTANList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlTAN values = new MdlTAN();
            objDaMstAPIVerifications.DaAppnTANList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AppnCompanyLLPList")]
        [HttpGet]
        public HttpResponseMessage AppnCompanyLLPList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCIN values = new MdlCIN();
            objDaMstAPIVerifications.DaAppnCompanyLLPList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AppnMCASignatureList")]
        [HttpGet]
        public HttpResponseMessage AppnMCASignatureList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlCIN values = new MdlCIN();
            objDaMstAPIVerifications.DaAppnMCASignatureList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AppnIECDetailedList")]
        [HttpGet]
        public HttpResponseMessage AppnIECDetailedList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIECDetailed values = new MdlIECDetailed();
            objDaMstAPIVerifications.DaAppnIECDetailedList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AppnFSSAIList")]
        [HttpGet]
        public HttpResponseMessage AppnFSSAIList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFSSAI values = new MdlFSSAI();
            objDaMstAPIVerifications.DaAppnFSSAIList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AppnFDAList")]
        [HttpGet]
        public HttpResponseMessage AppnFDAList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlFDA values = new MdlFDA();
            objDaMstAPIVerifications.DaAppnFDAList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AppnGSTVerificationList")]
        [HttpGet]
        public HttpResponseMessage AppnGSTVerificationList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGST values = new MdlGST();
            objDaMstAPIVerifications.DaAppnGSTVerificationList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AppnGSTReturnFilingList")]
        [HttpGet]
        public HttpResponseMessage AppnGSTReturnFilingList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGST values = new MdlGST();
            objDaMstAPIVerifications.DaAppnGSTReturnFilingList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AppnGSTAuthenticationList")]
        [HttpGet]
        public HttpResponseMessage AppnGSTAuthenticationList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlGST values = new MdlGST();
            objDaMstAPIVerifications.DaAppnGSTAuthenticationList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AppnLPGIDAuthenticationList")]
        [HttpGet]
        public HttpResponseMessage AppnLPGIDAuthenticationList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLPGID values = new MdlLPGID();
            objDaMstAPIVerifications.DaAppnLPGIDAuthenticationList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AppnShopList")]
        [HttpGet]
        public HttpResponseMessage AppnShopList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlShop values = new MdlShop();
            objDaMstAPIVerifications.DaAppnShopList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AppnRCAuthAdvancedList")]
        [HttpGet]
        public HttpResponseMessage AppnRCAuthAdvancedList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRCAuthAdvanced values = new MdlRCAuthAdvanced();
            objDaMstAPIVerifications.DaAppnRCAuthAdvancedList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AppnRCSearchList")]
        [HttpGet]
        public HttpResponseMessage AppnRCSearchList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlRCSearch values = new MdlRCSearch();
            objDaMstAPIVerifications.DaAppnRCSearchList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AppnPropertyTaxList")]
        [HttpGet]
        public HttpResponseMessage AppnPropertyTaxList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlPropertyTax values = new MdlPropertyTax();
            objDaMstAPIVerifications.DaAppnPropertyTaxList(application_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
