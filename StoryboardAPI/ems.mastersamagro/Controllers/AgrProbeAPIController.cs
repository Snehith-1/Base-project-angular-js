using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.mastersamagro.Models;
using ems.mastersamagro.DataAccess;

using System.Data.Odbc;
using Newtonsoft.Json;

namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This controllers provide access to third party in probing the customer data
    /// </summary>
    /// <remarks>Written by Praveen Raj.R </remarks>


    [RoutePrefix("api/AgrProbeAPI")]
    public class AgrProbeAPIController : ApiController
    {
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaAgrProbeAPI objDaAgrProbeAPI = new DaAgrProbeAPI();
        string msSQL;
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;

       
        [HttpPost]
        [ActionName("GetBaseDetails")]
        public HttpResponseMessage GetBaseDetails(MdlBaseDetailsRequest Values)
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = ObjGetGID.gettokenvalues(token);                
                var response = objDaAgrProbeAPI.DaGetBaseDetails(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [ActionName("BaseDetailsView")]
        [HttpGet]
        public HttpResponseMessage BaseDetailsView(string institutionprobedetails_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlBaseDetailsResponse values = new MdlBaseDetailsResponse();
            msSQL = " select CAST(response as char) as response" +
                       " from agr_trn_tinstitutionprobedetails where institutionprobedetails_gid='" + institutionprobedetails_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlBaseDetailsResponse>(objODBCDatareader["response"].ToString());
            }
            objODBCDatareader.Close();
            //   objDaMstAPIVerifications.DaRCSearchViewDetails(vehiclercsearch_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetInstitutionDetails")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionDetails(string institution_gid)
        {
            MdlMstInstitutionAdd values = new MdlMstInstitutionAdd();
            objDaAgrProbeAPI.DaGetInstitutionDetails(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InstitutionProbeList")]
        [HttpGet]
        public HttpResponseMessage InstitutionProbeList(string institution_gid)
        {
            MdlInstitutionProbe values = new MdlInstitutionProbe();
            objDaAgrProbeAPI.DaInstitutionProbeList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [HttpPost]
        [ActionName("GetComprehensiveDetails")]
        public HttpResponseMessage GetComprehensiveDetails(MdlComprehensiveDetailsRequest Values)
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = ObjGetGID.gettokenvalues(token);
                var response = objDaAgrProbeAPI.DaGetComprehensiveDetails(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [ActionName("ComprehensiveDetailsView")]
        [HttpGet]
        public HttpResponseMessage ComprehensiveDetailsView(string institutionprobedetails_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlComprehensiveDetailsResponse values = new MdlComprehensiveDetailsResponse();
            msSQL = " select CAST(response as char) as response" +
                       " from agr_trn_tinstitutionprobedetails where institutionprobedetails_gid='" + institutionprobedetails_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlComprehensiveDetailsResponse>(objODBCDatareader["response"].ToString());
            }
            objODBCDatareader.Close();
            //   objDaMstAPIVerifications.DaRCSearchViewDetails(vehiclercsearch_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("GetDataStatus")]
        public HttpResponseMessage GetDataStatus(MdlDataStatusRequest Values)
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = ObjGetGID.gettokenvalues(token);
                var response = objDaAgrProbeAPI.DaGetDataStatus(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [ActionName("DataStatusView")]
        [HttpGet]
        public HttpResponseMessage DataStatusView(string institutionprobedetails_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlDataStatusResponse values = new MdlDataStatusResponse();
            msSQL = " select CAST(response as char) as response" +
                       " from agr_trn_tinstitutionprobedetails where institutionprobedetails_gid='" + institutionprobedetails_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlDataStatusResponse>(objODBCDatareader["response"].ToString());
            }
            objODBCDatareader.Close();          
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("GetArticleOfAssociationDoc")]
        public HttpResponseMessage GetArticleOfAssociationDoc(MdlComprehensiveDetailsRequest Values)
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = ObjGetGID.gettokenvalues(token);
                var response = objDaAgrProbeAPI.DaGetArticleOfAssociationDoc(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [ActionName("InstitutionProbeDocList")]
        [HttpGet]
        public HttpResponseMessage InstitutionProbeDocList(string institution_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlInstitutionProbeDoc values = new MdlInstitutionProbeDoc();
            objDaAgrProbeAPI.DaInstitutionProbeDocList(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        [HttpPost]
        [ActionName("GetMemorandumOfAssociationDoc")]
        public HttpResponseMessage GetMemorandumOfAssociationDoc(MdlProbeDocDetailsRequest Values)
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = ObjGetGID.gettokenvalues(token);
                var response = objDaAgrProbeAPI.DaGetMemorandumOfAssociationDoc(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPost]
        [ActionName("GetCertificateOfIncorporationDoc")]
        public HttpResponseMessage GetCertificateOfIncorporationDoc(MdlProbeDocDetailsRequest Values)
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = ObjGetGID.gettokenvalues(token);
                var response = objDaAgrProbeAPI.DaGetCertificateOfIncorporationDoc(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPost]
        [ActionName("GetFormMGT7Doc")]
        public HttpResponseMessage GetFormMGT7Doc(MdlProbeDocDetailsRequest Values)
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = ObjGetGID.gettokenvalues(token);
                var response = objDaAgrProbeAPI.DaGetFormMGT7Doc(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPost]
        [ActionName("GetFormAOC4Doc")]
        public HttpResponseMessage GetFormAOC4Doc(MdlProbeDocDetailsRequest Values)
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = ObjGetGID.gettokenvalues(token);
                var response = objDaAgrProbeAPI.DaGetFormAOC4Doc(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [ActionName("InstitutionProbeLogList")]
        [HttpGet]
        public HttpResponseMessage InstitutionProbeLogList(string application_gid)
        {
            MdlInstitutionProbe values = new MdlInstitutionProbe();
            objDaAgrProbeAPI.DaInstitutionProbeLogList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InstitutionProbeDocLogList")]
        [HttpGet]
        public HttpResponseMessage InstitutionProbeDocLogList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlInstitutionProbeDoc values = new MdlInstitutionProbeDoc();
            objDaAgrProbeAPI.DaInstitutionProbeDocLogList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [HttpPost]
        [ActionName("GetBaseDetailsLLP")]
        public HttpResponseMessage GetBaseDetailsLLP(MdlBaseDetailsRequest Values)
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = ObjGetGID.gettokenvalues(token);
                var response = objDaAgrProbeAPI.DaGetBaseDetailsLLP(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [ActionName("BaseDetailsLLPView")]
        [HttpGet]
        public HttpResponseMessage BaseDetailsLLPView(string institutionprobedetails_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlBaseDetailsLLPResponse values = new MdlBaseDetailsLLPResponse();
            msSQL = " select CAST(response as char) as response" +
                       " from agr_trn_tinstitutionprobedetails where institutionprobedetails_gid='" + institutionprobedetails_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlBaseDetailsLLPResponse>(objODBCDatareader["response"].ToString());
            }
            objODBCDatareader.Close();
            //   objDaMstAPIVerifications.DaRCSearchViewDetails(vehiclercsearch_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [HttpPost]
        [ActionName("GetComprehensiveDetailsLLP")]
        public HttpResponseMessage GetComprehensiveDetailsLLP(MdlComprehensiveDetailsRequest Values)
        {
            if (ModelState.IsValid)
            {
                string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
                getsessionvalues = ObjGetGID.gettokenvalues(token);
                var response = objDaAgrProbeAPI.DaGetComprehensiveDetailsLLP(getsessionvalues.employee_gid, Values);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [ActionName("ComprehensiveDetailsLLPView")]
        [HttpGet]
        public HttpResponseMessage ComprehensiveDetailsLLPView(string institutionprobedetails_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlComprehensiveDetailsLLPResponse values = new MdlComprehensiveDetailsLLPResponse();
            msSQL = " select CAST(response as char) as response" +
                       " from agr_trn_tinstitutionprobedetails where institutionprobedetails_gid='" + institutionprobedetails_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                values = JsonConvert.DeserializeObject<MdlComprehensiveDetailsLLPResponse>(objODBCDatareader["response"].ToString());
            }
            objODBCDatareader.Close();
            //   objDaMstAPIVerifications.DaRCSearchViewDetails(vehiclercsearch_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


    }
}