using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.idas.Models;
using ems.idas.DataAccess;
using Newtonsoft.Json;
using RestSharp;
using System.Configuration;
using ems.storage.Functions;

namespace ems.idas.Controllers
{
    [RoutePrefix("api/idasTrnMakerCheckerDtls")]
    [Authorize]
    public class idasTrnMakerCheckerDtlsController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaIdasTrnMakerCheckerDtls objDamkrckrdtls = new DaIdasTrnMakerCheckerDtls();
        Fnazurestorage objcmnstorage = new Fnazurestorage();


        [ActionName("GetComplaintCertificatePdf")]
        [HttpGet]
        public HttpResponseMessage GetComplaintCertificatePdf(string sanction_gid)
        {
            reportpdf objlsadoc = new reportpdf();
            var ls_response = new Dictionary<string, object>();
            var client = new RestClient(ConfigurationManager.AppSettings["report_api_path"].ToString() + "IdasMgmt/ComplianceCertificate?sanction_gid=" + sanction_gid);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            string path = JsonConvert.DeserializeObject<string>(response.Content);
            var pathArray = path.Split(new string[] { "EMS/" }, StringSplitOptions.None);
            var fileNameArrray = path.Split(new string[] { "Report/" }, StringSplitOptions.None);
            objlsadoc.file_path = pathArray[1].ToString();
            objlsadoc.file_name = fileNameArrray[1].ToString();
            ls_response = objcmnstorage.DaFileUploadDocument(objlsadoc.file_path);
            objlsadoc.file_path = objcmnstorage.EncryptData(objlsadoc.file_path);
            var byName = (IDictionary<string, object>)ls_response;
            objlsadoc.status = (bool)byName["status"];
            return Request.CreateResponse(HttpStatusCode.OK, objlsadoc);

        }

        [ActionName("GetMailId")]
        [HttpGet]
        public HttpResponseMessage PostSanctionDoc(string customer_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlmailid objmdlmailid = new mdlmailid();
            objDamkrckrdtls .DaGetMailid(customer_gid,objmdlmailid);
            return Request.CreateResponse(HttpStatusCode.OK, objmdlmailid);
        }

       
        [ActionName("CadQuieryRMViwed")]
        [HttpPost]
        public HttpResponseMessage PostCadQuieryRMViwed(mdlviewupdate objResult)
        {

            
            objDamkrckrdtls.DaPostCadQuieryRMViwed(objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("RmResponseCadViwed")]
        [HttpPost]
        public HttpResponseMessage RmResponseCadViwed(mdlviewupdate objResult)
        {

            objDamkrckrdtls.DaPostRmResponseCadViwed(objResult );
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("PostForwardedQuery")]
        [HttpGet]
        public HttpResponseMessage PostForwardedQuery(string docconversation_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objResult = new result();
            objDamkrckrdtls.DaPostForwardQuery(getsessionvalues.user_gid,docconversation_gid, objResult );
            return Request.CreateResponse(HttpStatusCode.OK,objResult );
        }
        [ActionName("GetCreditMailId")]
        [HttpGet]
        public HttpResponseMessage GetCreditMailId()
        {
            mdlmailid objmdlmailid = new mdlmailid();
            objDamkrckrdtls.DaGetCreditMailId(objmdlmailid);
            return Request.CreateResponse(HttpStatusCode.OK, objmdlmailid);
        }

    }
}
