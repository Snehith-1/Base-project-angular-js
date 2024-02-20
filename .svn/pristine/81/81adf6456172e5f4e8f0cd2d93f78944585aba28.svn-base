using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.master.Models;
using ems.master.DataAccess;

using System.Data.Odbc;
using Newtonsoft.Json;

namespace ems.master.Controllers
{
    [RoutePrefix("api/ProbeAPI")]
    public class ProbeAPIController : ApiController
    {
        session_values ObjGetGID = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaProbeAPI objDaProbeAPI = new DaProbeAPI();
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
                var response = objDaProbeAPI.DaGetBaseDetails(getsessionvalues.employee_gid, Values);
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
                       " from ocs_trn_tinstitutionprobedetails where institutionprobedetails_gid='" + institutionprobedetails_gid + "'";
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
            objDaProbeAPI.DaGetInstitutionDetails(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InstitutionProbeList")]
        [HttpGet]
        public HttpResponseMessage InstitutionProbeList(string institution_gid)
        {
            MdlInstitutionProbe values = new MdlInstitutionProbe();
            objDaProbeAPI.DaInstitutionProbeList(institution_gid, values);
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
                var response = objDaProbeAPI.DaGetComprehensiveDetails(getsessionvalues.employee_gid, Values);
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
                       " from ocs_trn_tinstitutionprobedetails where institutionprobedetails_gid='" + institutionprobedetails_gid + "'";
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
                var response = objDaProbeAPI.DaGetDataStatus(getsessionvalues.employee_gid, Values);
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
                       " from ocs_trn_tinstitutionprobedetails where institutionprobedetails_gid='" + institutionprobedetails_gid + "'";
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
                var response = objDaProbeAPI.DaGetArticleOfAssociationDoc(getsessionvalues.employee_gid, Values);
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
            objDaProbeAPI.DaInstitutionProbeDocList(institution_gid, values);
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
                var response = objDaProbeAPI.DaGetMemorandumOfAssociationDoc(getsessionvalues.employee_gid, Values);
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
                var response = objDaProbeAPI.DaGetCertificateOfIncorporationDoc(getsessionvalues.employee_gid, Values);
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
                var response = objDaProbeAPI.DaGetFormMGT7Doc(getsessionvalues.employee_gid, Values);
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
                var response = objDaProbeAPI.DaGetFormAOC4Doc(getsessionvalues.employee_gid, Values);
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
            objDaProbeAPI.DaInstitutionProbeLogList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InstitutionProbeDocLogList")]
        [HttpGet]
        public HttpResponseMessage InstitutionProbeDocLogList(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = ObjGetGID.gettokenvalues(token);
            MdlInstitutionProbeDoc values = new MdlInstitutionProbeDoc();
            objDaProbeAPI.DaInstitutionProbeDocLogList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        


    }
}