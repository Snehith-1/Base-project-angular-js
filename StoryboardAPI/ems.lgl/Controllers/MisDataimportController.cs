using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using ems.utilities.Models;
using ems.utilities.Functions;
using ems.lgl.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Configuration;
using ems.lgl.DataAccess;
using System.Collections.Generic;
using System;
using ems.storage.Functions;

namespace StoryboardAPI.Controllers.ems.lgl
{
    [RoutePrefix("api/misDataimport")]
    [Authorize]

    public class MisDataimportController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaMisdataimport objdaMisdataimport = new DaMisdataimport();
        Fnazurestorage objcmnstorage = new Fnazurestorage();

        [ActionName("mistempdataupload")]
        [HttpPost]
        public HttpResponseMessage postexcelupload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            excel objsanctiondetails = new excel();
            objdaMisdataimport.DaPostExcelUpload(httpRequest, getsessionvalues.employee_gid, objsanctiondetails);           
            return Request.CreateResponse(HttpStatusCode.OK, objsanctiondetails);
        }
        [ActionName("Getmisdata")]
        [HttpGet]
        public HttpResponseMessage getmisdata()
        {
            MdlMisdataimportlist objmisdataimportlist = new MdlMisdataimportlist();
            objdaMisdataimport.DaGetMisDataDetail(objmisdataimportlist);          
            return Request.CreateResponse(HttpStatusCode.OK, objmisdataimportlist);
        }
        [ActionName("processdata")]
        [HttpPost]
        public HttpResponseMessage postprocessdata_update(MdlMisdataimportlist values)
        {
            objdaMisdataimport.DaPostProcessData(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getimporteddata")]
        [HttpGet]
        public HttpResponseMessage getcustomer2misdata(string misdocumentimport_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMisdataimportlist values = new MdlMisdataimportlist();
            objdaMisdataimport.DaGetImportedData(getsessionvalues.employee_gid, misdocumentimport_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("getrepaymentcases")]
        [HttpGet]
        public HttpResponseMessage getrepaymentcases()
        {
            MdlMisdataimportlist objmisdataimportlist = new MdlMisdataimportlist();
            objdaMisdataimport.DaGetRepaymentCases(objmisdataimportlist);
            return Request.CreateResponse(HttpStatusCode.OK, objmisdataimportlist);
        }
        [ActionName("GetDN1list")]
        [HttpGet]
        public HttpResponseMessage getDN1list()
        {
            MdlMisdataimportlist objmisdataimportlist = new MdlMisdataimportlist();
            objdaMisdataimport.DaGetDN1List(objmisdataimportlist);
            return Request.CreateResponse(HttpStatusCode.OK, objmisdataimportlist);
        }
        [ActionName("GetDN2list")]
        [HttpGet]
        public HttpResponseMessage getDN2list()
        {
            MdlMisdataimportlist objmisdataimportlist = new MdlMisdataimportlist();
            objdaMisdataimport.DaGetDN2List(objmisdataimportlist);
            return Request.CreateResponse(HttpStatusCode.OK, objmisdataimportlist);
        }
        [ActionName("GetDN3list")]
        [HttpGet]
        public HttpResponseMessage getDN3list()
        {
            MdlMisdataimportlist objmisdataimportlist = new MdlMisdataimportlist();
            objdaMisdataimport.DaGetDN3List(objmisdataimportlist);
            return Request.CreateResponse(HttpStatusCode.OK, objmisdataimportlist);
        }
        [ActionName("GetDNcount")]
        [HttpGet]
        public HttpResponseMessage getDNcount()
        {
            DNcount objmisdataimportlist = new DNcount();
            objdaMisdataimport.DaGetDNCount(objmisdataimportlist);
            return Request.CreateResponse(HttpStatusCode.OK, objmisdataimportlist);
        }
        [ActionName("getcustomer2Loan")]
        [HttpGet]
        public HttpResponseMessage getcustomer2Loan(string urn)
        {
            MdlMisdataimportlist objmisdataimportlist = new MdlMisdataimportlist();
            objdaMisdataimport.DaGetCustomer2Loan(urn, getsessionvalues.employee_gid, objmisdataimportlist);
            return Request.CreateResponse(HttpStatusCode.OK, objmisdataimportlist);
        }
        [ActionName("DN1Status")]
        [HttpPost]
        public HttpResponseMessage postDN1status(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostDN1Status(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DNskip")]
        [HttpPost]
        public HttpResponseMessage postDNskip(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostDNskip(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DN2Status")]
        [HttpPost]
        public HttpResponseMessage postDN2status(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostDN2Status(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DN2skip")]
        [HttpPost]
        public HttpResponseMessage postDN2skip(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostDN2skip(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DN3Status")]
        [HttpPost]
        public HttpResponseMessage postDN3status(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostDN3Status(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DN3skip")]
        [HttpPost]
        public HttpResponseMessage postDN3skip(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostDN3skip(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("getcustomerDNGID")]
        [HttpGet]
        public HttpResponseMessage getcustomerDNGID(string urn)
        {
            MdlMisdataimportlist objmisdataimportlist = new MdlMisdataimportlist();
            objdaMisdataimport.DaGetCustomerDNGID(urn, getsessionvalues.employee_gid, objmisdataimportlist);
            return Request.CreateResponse(HttpStatusCode.OK, objmisdataimportlist);
        }
        [ActionName("getDN1Status")]
        [HttpGet]
        public HttpResponseMessage getDN1Status(string urn)
        {
            MdlDN_History objmisdataimportlist = new MdlDN_History();
            objdaMisdataimport.DaGetDN1Status(urn, getsessionvalues.employee_gid, objmisdataimportlist);
            return Request.CreateResponse(HttpStatusCode.OK, objmisdataimportlist);
        }
        [ActionName("getcourierinfo")]
        [HttpGet]
        public HttpResponseMessage getcourierinfo(string urn)
        {
            courierinfo objmisdataimportlist = new courierinfo();
            objdaMisdataimport.DaGetCourierInfo(urn, getsessionvalues.employee_gid, objmisdataimportlist);
            return Request.CreateResponse(HttpStatusCode.OK, objmisdataimportlist);
        }
        [ActionName("dn1ackstatusupdate")]
        [HttpPost]
        public HttpResponseMessage postdn1ackstatus(courierinfo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostDn1ackStatus(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("dn2ackstatusupdate")]
        [HttpPost]
        public HttpResponseMessage postdn2ackstatus(courierinfo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostDn2ackStatus(values, getsessionvalues.employee_gid);          
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("dn3ackstatusupdate")]
        [HttpPost]
        public HttpResponseMessage postdn3ackstatus(courierinfo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostDn3ackStatus(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("getDN1courierinfo")]
        [HttpGet]
        public HttpResponseMessage getDN1courierinfo(string urn)
        {
            courierinfo objmisdataimportlist = new courierinfo();
            objdaMisdataimport.DaGetDN1CourierInfo(urn, getsessionvalues.employee_gid, objmisdataimportlist);
            return Request.CreateResponse(HttpStatusCode.OK, objmisdataimportlist);
        }
        [ActionName("DN1Content")]
        [HttpGet]
        public HttpResponseMessage getTemplateDN1Content(string urn)
        {
            template_list objTemplateContent = new template_list();
            objdaMisdataimport.DaGetTemplateDN1Content(objTemplateContent, urn);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("DN2Content")]
        [HttpGet]
        public HttpResponseMessage getTemplateDN2Content(string urn)
        {
            template_list objTemplateContent = new template_list();
            objdaMisdataimport.DaGetTemplateDN2Content(objTemplateContent, urn);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("DN3Content")]
        [HttpGet]
        public HttpResponseMessage getTemplateDN3Content(string urn)
        {
            template_list objTemplateContent = new template_list();
            objdaMisdataimport.DaGetTemplateDN3Content(objTemplateContent, urn);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("DNCBOContent")]
        [HttpGet]
        public HttpResponseMessage getTemplateDNCBOContent(string urn)
        {
            template_list objTemplateContent = new template_list();
            objdaMisdataimport.DaGetTemplateDNCBOContent(objTemplateContent, urn);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("DN1generate")]
        [HttpPost]
        public HttpResponseMessage postDN1generate(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostDN1Generate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DN2generate")]
        [HttpPost]
        public HttpResponseMessage postDN2generate(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostDN2Generate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DN3generate")]
        [HttpPost]
        public HttpResponseMessage postDN3generate(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostDN3Generate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DN1ContentDTL")]
        [HttpGet]
        public HttpResponseMessage getTemplateDN1ContentDTL(string urn)
        {
            MdlMisdataimportlist objTemplateContent = new MdlMisdataimportlist();
            objdaMisdataimport.DaGetTemplateDN1ContentDTL(urn, objTemplateContent);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateContent);
        }
        [ActionName("DN1pdfcontent")]
        [HttpGet]
        public HttpResponseMessage getdn1pdf(string urn)
        {
            pdfContent objlsadoc = new pdfContent();
            var ls_response = new Dictionary<string, object>();
            var client = new RestClient(ConfigurationManager.AppSettings["report_api_path"].ToString() + "/DN1/templatecontentdn1/" + urn);
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
        [ActionName("DN2pdfcontent")]
        [HttpGet]
        public HttpResponseMessage getdn2pdf(string urn)
        {
            pdfContent objlsadoc = new pdfContent();
            var ls_response = new Dictionary<string, object>();
            var client = new RestClient(ConfigurationManager.AppSettings["report_api_path"].ToString() + "/DN1/templatecontentdn2/" + urn);
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
        [ActionName("DN3pdfcontent")]
        [HttpGet]
        public HttpResponseMessage getdn3pdf(string urn)
        {
            pdfContent objlsadoc = new pdfContent();
            var ls_response = new Dictionary<string, object>();
            var client = new RestClient(ConfigurationManager.AppSettings["report_api_path"].ToString() + "/DN1/templatecontentdn3/" + urn);
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
        [ActionName("DNCBOpdfcontent")]
        [HttpGet]
        public HttpResponseMessage getdncbopdf(string urn)
        {
            pdfContent objlsadoc = new pdfContent();
            var ls_response = new Dictionary<string, object>();
            var client = new RestClient(ConfigurationManager.AppSettings["report_api_path"].ToString() + "/DN1/templatecontentCBO/" + urn);
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
        [ActionName("RecoveredCases")]
        [HttpGet]
        public HttpResponseMessage getrecoveredCases()
        {
            MdlMisdataimportlist objrecover_cases = new MdlMisdataimportlist();
            objdaMisdataimport.DaGetRecoveredCases(objrecover_cases);
            return Request.CreateResponse(HttpStatusCode.OK, objrecover_cases);
        }
        [ActionName("DNlist_cbo")]
        [HttpGet]
        public HttpResponseMessage getdnlist_cbo()
        {
            MdlMisdataimportlist objmisdataimportlist = new MdlMisdataimportlist();
            objdaMisdataimport.DaGetdnList_cbo(objmisdataimportlist);
            return Request.CreateResponse(HttpStatusCode.OK, objmisdataimportlist);

        }
        [ActionName("cbogenerate")]
        [HttpPost]
        public HttpResponseMessage postcbogenerate(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostCBOGenerate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getcustomerdetails")]
        [HttpGet]
        public HttpResponseMessage Getcustomerdetails(string customer_gid)
        {
            customeredit values = new customeredit();
            objdaMisdataimport.DaGetCustomerDetails(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CBOStatus")]
        [HttpPost]
        public HttpResponseMessage postCBOStatus(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostCBOStatus(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("CBOskip")]
        [HttpPost]
        public HttpResponseMessage postCBOskip(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostCBOskip(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("dnCBOackstatusupdate")]
        [HttpPost]
        public HttpResponseMessage postdnCBOackstatus(courierinfo values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
             objdaMisdataimport.DaPostdnCBOackStatus(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("RevertDN1")]
        [HttpPost]
        public HttpResponseMessage postRevertDN1(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostRevertDN1(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("RevertDN2")]
        [HttpPost]
        public HttpResponseMessage postRevertDN2(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostRevertDN2(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("RevertDN3")]
        [HttpPost]
        public HttpResponseMessage postRevertDN3(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostRevertDN3(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("RevertDN_CBO")]
        [HttpPost]
        public HttpResponseMessage postRevertDN_CBO(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostRevertDN_CBO(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getrevertdetails")]
        [HttpGet]
        public HttpResponseMessage getrevertdetails(string urn)
        {
            dnrevert values = new dnrevert();
            objdaMisdataimport.DaGetRevertDetails(urn, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("HoldDN_CBO")]
        [HttpPost]
        public HttpResponseMessage postHoldDN_CBO(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostHoldDN_CBO(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("HoldDN1")]
        [HttpPost]
        public HttpResponseMessage postHoldDN1(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostHoldDN1(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("HoldDN2")]
        [HttpPost]
        public HttpResponseMessage postHoldDN2(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostHoldDN2(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("HoldDN3")]
        [HttpPost]
        public HttpResponseMessage postHoldDN3(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostHoldDN3(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UnholdDN_CBO")]
        [HttpPost]
        public HttpResponseMessage postunholdDN_CBO(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostunholdDN_CBO(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UnholdDN1")]
        [HttpPost]
        public HttpResponseMessage postUnholdDN1(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostUnholdDN1(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UnholdDN2")]
        [HttpPost]
        public HttpResponseMessage postUnholdDN2(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostUnholdDN2(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UnholdDN3")]
        [HttpPost]
        public HttpResponseMessage postUnholdDN3(MdlMisdataimportlist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostUnholdDN3(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DN1sanctiondtl")]
        [HttpPost]
        public HttpResponseMessage postDN1sanctiondtl(sanctiondtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostDN1sanctiondtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DN2sanctiondtl")]
        [HttpPost]
        public HttpResponseMessage postDN2sanctiondtl(sanctiondtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostDN2sanctiondtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DN3sanctiondtl")]
        [HttpPost]
        public HttpResponseMessage postDN3sanctiondtl(sanctiondtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostDN3sanctiondtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DNCBOsanctiondtl")]
        [HttpPost]
        public HttpResponseMessage postDNCBOsanctiondtl(sanctiondtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objdaMisdataimport.DaPostDNCBOsanctiondtl(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetSanctiondtl")]
        [HttpGet]
        public HttpResponseMessage GetSanctiondtl(string urn)
        {
            sanctiondtl values = new sanctiondtl();
            objdaMisdataimport.DaGetSanctiondtl(urn, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getsanctionloandetails")]
        [HttpGet]
        public HttpResponseMessage Getsanctionloandetails(string urn)
        {
            sanctionloan values = new sanctionloan();
            objdaMisdataimport.DaGetsanctionloandetails(values, urn);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDN1Template_history")]
        [HttpGet]
        public HttpResponseMessage GetDN1Template_history(string tempdn1format_gid)
        {
            pdfContent objlsadoc = new pdfContent();
            var ls_response = new Dictionary<string, object>();
            var client = new RestClient(ConfigurationManager.AppSettings["report_api_path"].ToString() + "DN1/GetTemplatedn1_history?tempdn1format_gid=" + tempdn1format_gid); 
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
        [ActionName("GetDN2Template_history")]
        [HttpGet]
        public HttpResponseMessage GetDN2Template_history(string tempdn1format_gid)
        {
            pdfContent objlsadoc = new pdfContent();
            var ls_response = new Dictionary<string, object>();
            var client = new RestClient(ConfigurationManager.AppSettings["report_api_path"].ToString() + "DN1/GetTemplatedn2_history?tempdn1format_gid=" + tempdn1format_gid);
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
        [ActionName("GetDN3Template_history")]
        [HttpGet]
        public HttpResponseMessage GetDN3Template_history(string tempdn1format_gid)
        {
            pdfContent objlsadoc = new pdfContent();
            var ls_response = new Dictionary<string, object>();
            var client = new RestClient(ConfigurationManager.AppSettings["report_api_path"].ToString() + "DN1/GetTemplatedn3_history?tempdn1format_gid=" + tempdn1format_gid);
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
    }
}  
