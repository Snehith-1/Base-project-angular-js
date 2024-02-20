using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.idas.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Configuration;
using ems.idas.DataAccess;
using ems.storage.Functions;
using System.Collections.Generic;
using System;

namespace ems.idas.Controllers
{
    [RoutePrefix("api/IdasTrnLsaManagement")]
    [Authorize]
    public class IdasTrnLsaManagementController : ApiController
    {
        DaIdasTrnLsaManagement objLSAdataaccess = new DaIdasTrnLsaManagement();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        Fnazurestorage objcmnstorage = new Fnazurestorage();

        [ActionName("customerdtl")]
        [HttpGet]
        public HttpResponseMessage getcustomerdtl(string customer_gid)
        {
            MdlLsaManagement objlsamgmt = new MdlLsaManagement();
            objLSAdataaccess.DaGetCustomerdtl(customer_gid, objlsamgmt);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("customer2sanction")]
        [HttpGet]
        public HttpResponseMessage getcustomer2sanction(string customer_gid)
        {
            MdlLsaManagement objlsamgmt = new MdlLsaManagement();
            objLSAdataaccess.DaGetcustomer2sanction(customer_gid, objlsamgmt);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("customer2sanctiondtl")]
        [HttpGet]
        public HttpResponseMessage getsanctionmis_dtl(string customer2sanction_gid)
        {
            Mdlcustomer2sanction objlsamgmt = new Mdlcustomer2sanction();
            objLSAdataaccess.DaGetsanctionmis_dtl(customer2sanction_gid, objlsamgmt);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("createLSA")]
        [HttpPost]
        public HttpResponseMessage PostLSAcreation(MdlLsaManagement values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.DaPostLSAcreation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("lsadetails")]
        [HttpGet]
        public HttpResponseMessage getlsadetails()
        {
            MdlLsaManagement objlsamgmt = new MdlLsaManagement();
            objLSAdataaccess.Dagetlsadetails(objlsamgmt);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("branch")]
        [HttpGet]
        public HttpResponseMessage GetBranchdtl()
        {
            MdlLsaManagement objlsamgmt = new MdlLsaManagement();
            objLSAdataaccess.DaGetBranchdtl(objlsamgmt);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("loanfacility")]
        [HttpGet]
        public HttpResponseMessage Getloanfacility()
        {
            MdlLsaManagement objlsamgmt = new MdlLsaManagement();
            objLSAdataaccess.DAGetloanfacility(objlsamgmt);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
       
        [ActionName("postlimitinfo")]
        [HttpPost]
        public HttpResponseMessage Postlimitinfo(MdlLsaManagement values)
        {
            
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.DaPostlimitinfo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("limitinfodtl")]
        [HttpGet]
        public HttpResponseMessage Getlimitinfodtl(string lsacreate_gid)
        {
            MdlLsaManagement objlsamgmt = new MdlLsaManagement();
            objLSAdataaccess.DaGetlimitinfodtl(objlsamgmt, lsacreate_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("limitref_no")]
        [HttpGet]
        public HttpResponseMessage Getlimitref_no(string lsacreate_gid)
        {
            MdlLsaManagement objlsamgmt = new MdlLsaManagement();
            objLSAdataaccess.Dagetlimitref_no(objlsamgmt, lsacreate_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
      
        [ActionName("ODLIM")]
        [HttpGet]
        public HttpResponseMessage Getodlim(string lsacreate_gid)
        {
            limit objlsamgmt = new limit();
            objLSAdataaccess.Dagetodlim(objlsamgmt, lsacreate_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("editlimitref_no")]
        [HttpGet]
        public HttpResponseMessage Geteditlimitref_no(string limitinfodtl_gid)
        {
            MdlLsaManagement objlsamgmt = new MdlLsaManagement();
            objLSAdataaccess.DaGETeditlimitref_no(objlsamgmt, limitinfodtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("postbankinfo")]
        [HttpPost]
        public HttpResponseMessage postbankinfo(MdlLsaManagement values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.Dapostbankinfo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("bankinfodtl")]
        [HttpGet]
        public HttpResponseMessage Getbankinfodtl(string lsacreate_gid)
        {
            MdlLsaManagement objlsamgmt = new MdlLsaManagement();
            objLSAdataaccess.DaGetbankinfodtl(objlsamgmt, lsacreate_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("Getrepaymentinfo")]
        [HttpGet]
        public HttpResponseMessage Getrepaymentinfo(string limitinfodtl_gid)
        {
            MdlLsaManagement objlsamgmt = new MdlLsaManagement();
            objLSAdataaccess.DaGetrepaymentinfo(objlsamgmt, limitinfodtl_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("repaymentdtl")]
        [HttpPost]
        public HttpResponseMessage postrepaymentdtl(MdlLsaManagement values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.Dapostrepaymentdtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("processingfee")]
        [HttpPost]
        public HttpResponseMessage postprocessingfee(MdlLsaManagement values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.Dapostprocessingfee(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("documentcharge")]
        [HttpPost]
        public HttpResponseMessage postdocumentcharge(MdlLsaManagement values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.Dapostdocumentcharge(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
   
       
        [ActionName("Getprocessingfeeinfo")]
        [HttpGet]
        public HttpResponseMessage Getprocessingfeeinfo(string lsacreate_gid)
        {
            MdlLsaManagement objlsamgmt = new MdlLsaManagement();
            objLSAdataaccess.DaGetprocessingfeeinfo(objlsamgmt, lsacreate_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("Getdocumentchargeinfo")]
        [HttpGet]
        public HttpResponseMessage Getdocumentchargeinfo(string lsacreate_gid)
        {
            MdlLsaManagement objlsamgmt = new MdlLsaManagement();
            objLSAdataaccess.DaGetdocumentchargeinfo(objlsamgmt, lsacreate_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("Getlsainfo")]
        [HttpGet]
        public HttpResponseMessage Getlsainfo(string lsacreate_gid)
        {
            MdlLsaManagement objlsamgmt = new MdlLsaManagement();
            objLSAdataaccess.DaGetlsainfo(objlsamgmt, lsacreate_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("Getcustomer2sanctioninfo")]
        [HttpGet]
        public HttpResponseMessage Getcustomer2sanctioninfo(string lsacreate_gid)
        {
            MdlLsaManagement objlsamgmt = new MdlLsaManagement();
            objLSAdataaccess.DaGetcustomer2sanctioninfo(objlsamgmt, lsacreate_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("Postfinal")]
        [HttpPost]
        public HttpResponseMessage Postfinal(MdlLsaManagement values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.DaPostfinal(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("compliancecheck")]
        [HttpPost]
        public HttpResponseMessage Postcompliancecheck(MdlLsaManagement values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.DaPostcompliancecheck(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getmakerinfo")]
        [HttpGet]
        public HttpResponseMessage Getmakerinfo(string lsacreate_gid)
        {
            MdlLsaManagement objlsamgmt = new MdlLsaManagement();
            objLSAdataaccess.DaGetmakerinfo(objlsamgmt, lsacreate_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("Getcompliancecheckinfo")]
        [HttpGet]
        public HttpResponseMessage Getcompliancecheckinfo(string lsacreate_gid)
        {
            MdlLsaManagement objlsamgmt = new MdlLsaManagement();
            objLSAdataaccess.DaGetcompliancecheckinfo(objlsamgmt, lsacreate_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("proceed_finalinfo")]
        [HttpPost]
        public HttpResponseMessage Postproceed_finalinfo(MdlLsaManagement values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.Dapostproceed_finalinfo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Uploaddocument")]
        [HttpPost]
        public HttpResponseMessage PostUploaddocument()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objLSAdataaccess.DaPostUploaddocument(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("Getdocument")]
        [HttpGet]
        public HttpResponseMessage Getdocument(string lsacreate_gid)
        {
            UploadDocumentname objlsamgmt = new UploadDocumentname();
            objLSAdataaccess.DaGetdocument(objlsamgmt, lsacreate_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        
        [ActionName("GetdetailsLSA")]
        [HttpGet]
        public HttpResponseMessage GetdetailsLSA(string lsacreate_gid)
        {
            MdlLsaManagement objlsamgmt = new MdlLsaManagement();
            objLSAdataaccess.DaGetdetailsLSA(objlsamgmt, lsacreate_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("Getaccountno_validation")]
        [HttpPost]
        public HttpResponseMessage Getaccountno_validation(MdlLsaManagement values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.DaGetaccountno_validation( values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("updatebankinfo")]
        [HttpPost]
        public HttpResponseMessage Postupdatebankinfo(MdlLsaManagement values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.DaPostupdatebankinfo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBankinfo")]
        [HttpGet]
        public HttpResponseMessage GetBankinfo(string lsacustomer2bankinfo)
        {
            MdlLsaManagement objlsamgmt = new MdlLsaManagement();
            objLSAdataaccess.DaGetBankinfo(objlsamgmt, lsacustomer2bankinfo);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("Uploaddoc")]
        [HttpPost]
        public HttpResponseMessage postuploaddoc()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objLSAdataaccess.Dapostuploaddoc(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("documentchargewithdoc")]
        [HttpPost]
        public HttpResponseMessage postdocumentchargewithdoc(MdlLsaManagement values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.Dapostdocumentchargewithdoc(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLSApdf")]
        [HttpGet]
        public HttpResponseMessage GetLSApdf(string lsacreate_gid)
        {
            
            reportpdf objlsadoc = new reportpdf();
            var ls_response = new Dictionary<string, object>();
            var client = new RestClient(ConfigurationManager.AppSettings["report_api_path"].ToString() + "LsaManagement/GetLSAreport?lsacreate_gid=" + lsacreate_gid);
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
        [ActionName("GetditLimitInfo")]
        [HttpGet]
        public HttpResponseMessage geteditdetails(string limitinfodtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLimitinfoEdit values = new MdlLimitinfoEdit();
            objLSAdataaccess.DaGetlimitinfo(getsessionvalues.employee_gid, limitinfodtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("updatelimitinfo")]
        [HttpPost]
        public HttpResponseMessage updatecustomer(MdlLimitinfoEdit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.Daupdatelimitinfo(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("updateprocessingfee")]
        [HttpPost]
        public HttpResponseMessage updateprocessingfee(MdlLsaManagement values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.Daupdateprocessingfee(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("updatedocumentcharges")]
        [HttpPost]
        public HttpResponseMessage updatedocumentcharges(MdlLsaManagement values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.Daupdatedocumentcharges(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("credit_manager")]
        [HttpGet]
        public HttpResponseMessage getcredit_manager()
        {
            Mdlcredit_manager objMdlEmployee = new Mdlcredit_manager();
            objLSAdataaccess.DaGetcreditmanager(objMdlEmployee);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }
        [ActionName("tempdelete")]
        [HttpGet]
        public HttpResponseMessage gettempcamdelete()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objdocumentcancel = new result();
            objLSAdataaccess.DaGetTempDelete( objdocumentcancel, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("documentmandatory_check")]
        [HttpGet]
        public HttpResponseMessage geteditmandatory_check()

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objdocument = new result();
            objLSAdataaccess.DaGetdocumentmandatory_check(objdocument, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objdocument);
        }
        [ActionName("postsecurityinfo")]
        [HttpPost]
        public HttpResponseMessage postsecurityinfo(Mdlsecurity values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.DaPostsecurityinfo(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("securityinfo_delete")]
        [HttpGet]
        public HttpResponseMessage getsecurityinfo_delete(string collateral_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            result objdocumentcancel = new result();
            objLSAdataaccess.DaGetsecurityinfo_delete(collateral_gid, objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }
        [ActionName("Getsanction2Colletarl")]
        [HttpGet]
        public HttpResponseMessage Getsanction2Colletarl(string lsacreate_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlsecurity objColletarl = new Mdlsecurity();
            objLSAdataaccess.DaGetsanction2Colletarl(lsacreate_gid, objColletarl, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objColletarl);
        }
        [ActionName("GetColletarl")]
        [HttpGet]
        public HttpResponseMessage GetColletarl(string collateral_gid)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlsecurity objColletarl = new Mdlsecurity();
            objLSAdataaccess.DaGetColletarl(collateral_gid, objColletarl, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objColletarl);
        }
        [ActionName("updatesecurityinfo")]
        [HttpPost]
        public HttpResponseMessage updatesecurityinfo(Mdlsecurity values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.Daupdatesecurityinfo(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getloanfacilityamount")]
        [HttpPost]
        public HttpResponseMessage Getloanfacilityamount(Mdlloanfacility_type values)
        {
         //   Mdlloanfacility_type values = new Mdlloanfacility_type();
            objLSAdataaccess.DaGetloanfacilityamount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetEditloanfacilityamount")]
        [HttpPost]
        public HttpResponseMessage GetEditloanfacilityamount(Mdlloanfacility_type values)
        {
            //   Mdlloanfacility_type values = new Mdlloanfacility_type();
            objLSAdataaccess.DaGetEditloanfacilityamount(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("documentchargeapplicable")]
        [HttpPost]
        public HttpResponseMessage postdocumentchargeapplicable(MdlLsaManagement values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.Dapostdocumentchargeapplicable(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("Getsanction2loanfacility")]
        [HttpGet]
        public HttpResponseMessage Getsanction2loanfacility(string lsacreate_gid)

        {
            MdlLsaManagement objloanfacility = new MdlLsaManagement();
            objLSAdataaccess.DaGetsanction2loanfacility(lsacreate_gid, objloanfacility);
            return Request.CreateResponse(HttpStatusCode.OK, objloanfacility);
        }
        [ActionName("editsanction2loanfacility")]
        [HttpGet]
        public HttpResponseMessage editsanction2loanfacility(string limitinfodtl_gid)

        {
            MdlLsaManagement objloanfacility = new MdlLsaManagement();
            objLSAdataaccess.Daeditsanction2loanfacility(limitinfodtl_gid, objloanfacility);
            return Request.CreateResponse(HttpStatusCode.OK, objloanfacility);
        }
        [ActionName("updatelsa")]
        [HttpPost]
        public HttpResponseMessage updatelsa(MdlLsaManagement values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.Daupdatelsa(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("LSAapproval")]
        [HttpPost]
        public HttpResponseMessage postapprovalstatuslsa(MdlLsaManagement values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.Dapostapprovalstatuslsa(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("LSAapprovalpendinginfo")]
        [HttpGet]
       
        public HttpResponseMessage getLSAapprovalpendinginfo()
        {
            LSApending_list objlsamgmt = new LSApending_list();
            objLSAdataaccess.DagetLSAapprovalpendinginfo(objlsamgmt);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        [ActionName("postLSAstatusapprove")]
        [HttpPost]
        public HttpResponseMessage postLSAstatusapprove(MdlLsaManagement values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objLSAdataaccess.DapostLSAstatusapprove(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
       
        // -----Checking Loan facility Info from sanction-----------//
        [ActionName("Getloanfacilityinfo")]
        [HttpGet]
        public HttpResponseMessage Getloanfacilityinfo(string customer2sanction_gid)

        {
            MdlLSAvalidation objloanfacility = new MdlLSAvalidation();
            objLSAdataaccess.DaGetloanfacilityinfo(customer2sanction_gid, objloanfacility);
            return Request.CreateResponse(HttpStatusCode.OK, objloanfacility);
        }
        [ActionName("Getdocument_validation")]
        [HttpGet]
        public HttpResponseMessage Getdocument_validation(string customer2sanction_gid)

        {
            MdlLSAvalidation objloanfacility = new MdlLSAvalidation();
            objLSAdataaccess.DaGetdocument_validation(customer2sanction_gid, objloanfacility);
            return Request.CreateResponse(HttpStatusCode.OK, objloanfacility);
        }
        [ActionName("PostROIDocumentUpload")]
        [HttpPost]
        public HttpResponseMessage postROIDocumentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            objLSAdataaccess.DaPostROIDocumentUpload(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }
        [ActionName("CancelDocument")]
        [HttpGet]
        public HttpResponseMessage canceldocument(string limitinfodtl_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlLimitinfoEdit values = new MdlLimitinfoEdit();
            objLSAdataaccess.DaCancelDocument(getsessionvalues.employee_gid, limitinfodtl_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Penal Interest
        [ActionName("GetPenalInterest")]
        [HttpGet]
        public HttpResponseMessage getpenalinterest(string lsacreate_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            Mdlloanfacility_type objlsamgmt = new Mdlloanfacility_type();
            objLSAdataaccess.DaGetPenalInterest(objlsamgmt, lsacreate_gid, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }
        //Export Data
        [ActionName("ExportDocumentCoversation")]
        [HttpPost]
        public HttpResponseMessage ExportDocumentCoversation(MdlExportConversation values)
        {
            objLSAdataaccess.DaGetExportDocumentCoversation(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
