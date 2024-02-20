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
namespace ems.idas.Controllers
{
    [RoutePrefix("api/IdasTrnDocumentUpload")]
    [Authorize]
    public class IdasTrnDocumentUploadController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaIdasTrnDocumentUpload objDaAccess = new DaIdasTrnDocumentUpload();
        result objResult = new result();


        [ActionName("CreateFolder")]
        [HttpPost]
        public HttpResponseMessage GetDocumentList(MdlIdasDocumentUpload values)
        {
           
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
       
            objResult = objDaAccess.DaPostCreateFolder(values ,getsessionvalues .user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("FileUpload")]
        [HttpPost]
        public HttpResponseMessage FileUpload()
        {
            HttpRequest httpRequest;
            httpRequest = HttpContext.Current.Request;

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            MdlIdasDocumentUpload values = new MdlIdasDocumentUpload();

            objDaAccess.DaPostFileUpload(httpRequest,values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("FolderDtls")]
        [HttpGet]
        public HttpResponseMessage DaGetFolderDtls(string parent_directorygid)
        {


            DirectoryDtlsList values = new DirectoryDtlsList();
           objDaAccess.DaGetFolderDtls(parent_directorygid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values );
        }

        [ActionName("RenameFolder")]
        [HttpPost]
        public HttpResponseMessage DaPostRenameFolder(FolderDtls  values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
           
            objResult = objDaAccess.DaPostRenameFolder(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }


        [ActionName("RenameFile")]
        [HttpPost]
        public HttpResponseMessage DaPostRenameFile(FolderDtls values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            objResult = objDaAccess.DaPostRenameFile(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("BreadCrumb")]
        [HttpGet]
        public HttpResponseMessage BreadCrumb(string fileupload_gid)
        {
            breadCrumbList values = new breadCrumbList();
            objDaAccess.DaGetBreadCrumb(fileupload_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Delete")]
        [HttpGet]
        public HttpResponseMessage Delete(string fileupload_gid)
        {
           objResult =objDaAccess.DaDelete(fileupload_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult );
        }

        [ActionName("sanction2customer")]
        [HttpGet]
        public HttpResponseMessage getsanction2customer(string customer_gid)
        {
            Mdlsanction2customer values = new Mdlsanction2customer();
            objDaAccess.DaGetsanction2customer(customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDocumentsofSanction")]
        [HttpGet]
        public HttpResponseMessage GetDocumentsofSanction(string customer2sanction_gid)
        {
            Mdlsanction2customer objlsamgmt = new Mdlsanction2customer();
            objDaAccess.DaGetDocumentsofSanction(objlsamgmt, customer2sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objlsamgmt);
        }

        [ActionName("CreateDocumentLabel")]
        [HttpPost]
        public HttpResponseMessage PostCreateDocumentLabel(MdlDocumentLabel values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);         
            objDaAccess.DaPostCreateDocumentLabel(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UpdateDocumentLabel")]
        [HttpPost]
        public HttpResponseMessage PostUpdateDocumentLabel(MdlDocumentLabel values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult = objDaAccess.PostUpdateDocumentLabel(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("DocumentLabelDelete")]
        [HttpGet]
        public HttpResponseMessage DocumentLabelDelete(string documentlabel_gid)
        {
            objResult = objDaAccess.DaDocumentLabelDelete(documentlabel_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("DocumentLabelSummary")]
        [HttpGet]
        public HttpResponseMessage GetDocumentLabelSummary()
        {
            MdlDocumentLabel values = new MdlDocumentLabel();
            objDaAccess.DaGetDocumentLabelSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDocumentLabellist")]
        [HttpGet]
        public HttpResponseMessage GetDocumentLabellist()
        {
            MdlDocumentLabel values = new MdlDocumentLabel();
            objDaAccess.DaGetDocumentLabel(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDocumentLabel")]
        [HttpGet]
        public HttpResponseMessage GetDocumentLabel(string documentlabel_gid)
        {
            MdlDocumentLabel values = new MdlDocumentLabel();
            objDaAccess.DaGetGetDocumentLabel(documentlabel_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Document Tagging

        [ActionName("CreateCustomerFolder")]
        [HttpPost]
        public HttpResponseMessage CreateCustomerFolder(MdlIdasDocumentUpload values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult = objDaAccess.DaPostCreateCustomerFolder(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("CustomerFolderDtls")]
        [HttpGet]
        public HttpResponseMessage DaGetCustomerFolderDtls(string customer2sanction_gid, string parent_directorygid, string customer_gid)
        {
            DirectoryDtlsList values = new DirectoryDtlsList();
            objDaAccess.DaGetCustomerFolderDtls(customer2sanction_gid, parent_directorygid, customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CustomerFileUpload")]
        [HttpPost]
        public HttpResponseMessage DaPostCustomerFileUpload()
        {
            HttpRequest httpRequest;
            httpRequest = HttpContext.Current.Request;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIdasDocumentUpload values = new MdlIdasDocumentUpload();
            objDaAccess.DaPostCustomerFileUpload(httpRequest, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CustomerFilesBreadCrumb")]
        [HttpGet]
        public HttpResponseMessage CustomerFilesBreadCrumb(string customerfileupload_gid)
        {
            breadCrumbList values = new breadCrumbList();
            objDaAccess.DaGetCustomerFilesBreadCrumb(customerfileupload_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("RenameCustomerFile")]
        [HttpPost]
        public HttpResponseMessage RenameCustomerFile(FolderDtls values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objResult = objDaAccess.DaPostRenameCustomerFile(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("FileDelete")]
        [HttpGet]
        public HttpResponseMessage FileDelete(string customerfileupload_gid)
        {
            objResult = objDaAccess.DaFileDelete(customerfileupload_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }

        [ActionName("GetCadTeamFlag")]
        [HttpGet]
        public HttpResponseMessage GetCadTeamFlag()
        {
            MdlCadTeamFlag values = new MdlCadTeamFlag();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetCadTeamFlag(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetDocumentTaggedCustomer")]
        [HttpGet]
        public HttpResponseMessage Customerdetail()
        {
            MdlTaggedCustomer objMdlCustomer = new MdlTaggedCustomer();
            objDaAccess.DaGetDocumentTaggedCustomer(objMdlCustomer);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCustomer);
        }
        [ActionName("GetDocumentUnTaggedCustomer")]
        [HttpGet]
        public HttpResponseMessage UnCustomerdetail()
        {
            MdlTaggedCustomer objMdlCustomer = new MdlTaggedCustomer();
            objDaAccess.DaGetDocumentUnTaggedCustomer(objMdlCustomer);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCustomer);
        }
        [ActionName("GetDocumentCustomerCount")]
        [HttpGet]
        public HttpResponseMessage GetDocumentCustomerCount()
        {
            MdlTaggedCustomer objMdlCustomer = new MdlTaggedCustomer();
            objDaAccess.DaGetDocumentCustomerCount(objMdlCustomer);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCustomer);
        }
        [ActionName("WorkItemArchivalCustomerSummary")]
        [HttpPost]
        public HttpResponseMessage WorkItemArchivalCustomerSummary(MdlArchivalCondition objConditions)
        {
            WorkItemList values = new WorkItemList();
            objDaAccess.DaGetWorkItemArchivalCustomerSummary(values, objConditions);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("WorkItemArchivalSpecificSummary")]
        [HttpPost]
        public HttpResponseMessage WorkItemArchivalSpecificSummary(MdlArchivalCondition objConditions)
        {
            WorkItemList values = new WorkItemList();
            objDaAccess.DaGetWorkItemArchivalSpecificSummary(values, objConditions);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Underwritting Document Upload

        [ActionName("CreditFileUpload")]
        [HttpPost]
        public HttpResponseMessage CreditFileUpload()
        {
            HttpRequest httpRequest;
            httpRequest = HttpContext.Current.Request;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIdasDocumentUpload values = new MdlIdasDocumentUpload();
            objDaAccess.DaCreditFileUpload(httpRequest, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditFolderDtls")]
        [HttpGet]
        public HttpResponseMessage CreditFolderDtls(string parent_directorygid, string customer_gid)
        {
            DirectoryDtlsList values = new DirectoryDtlsList();
            objDaAccess.DaCreditFolderDtls(parent_directorygid, customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateCreditFolder")]
        [HttpPost]
        public HttpResponseMessage CreateCreditFolder(MdlIdasDocumentUpload values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaCreateCreditFolder(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("RenameCreditFile")]
        [HttpPost]
        public HttpResponseMessage RenameCreditFile(FolderDtls values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaRenameCreditFile(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditFileDelete")]
        [HttpGet]
        public HttpResponseMessage CreditFileDelete(string creditfileupload_gid)
        {
            result values = new result();
            objDaAccess.DaCreditFileDelete(creditfileupload_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditFilesBreadCrumb")]
        [HttpGet]
        public HttpResponseMessage CreditFilesBreadCrumb(string creditfileupload_gid)
        {
            breadCrumbList values = new breadCrumbList();
            objDaAccess.DaCreditFilesBreadCrumb(creditfileupload_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditTeamFlag")]
        [HttpGet]
        public HttpResponseMessage GetCreditTeamFlag()
        {
            MdlCreditTeamFlag values = new MdlCreditTeamFlag();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetCreditTeamFlag(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Credit Operations Document Upload

        [ActionName("CreditOperationsFileUpload")]
        [HttpPost]
        public HttpResponseMessage CreditOperationsFileUpload()
        {
            HttpRequest httpRequest;
            httpRequest = HttpContext.Current.Request;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlIdasDocumentUpload values = new MdlIdasDocumentUpload();
            objDaAccess.DaCreditOperationsFileUpload(httpRequest, values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditOperationsFolderDtls")]
        [HttpGet]
        public HttpResponseMessage CreditOperationsFolderDtls(string parent_directorygid, string customer_gid)
        {
            DirectoryDtlsList values = new DirectoryDtlsList();
            objDaAccess.DaCreditOperationsFolderDtls(parent_directorygid, customer_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateCreditOperationsFolder")]
        [HttpPost]
        public HttpResponseMessage CreateCreditOperationsFolder(MdlIdasDocumentUpload values)
        {

            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaCreateCreditOperationsFolder(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("RenameCreditOperationsFile")]
        [HttpPost]
        public HttpResponseMessage RenameCreditOperationsFile(FolderDtls values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaRenameCreditOperationsFile(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditOperationsFileDelete")]
        [HttpGet]
        public HttpResponseMessage CreditOperationsFileDelete(string creditoperationsfileupload_gid)
        {
            result values = new result();
            objDaAccess.DaCreditOperationsFileDelete(creditoperationsfileupload_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreditOperationsFilesBreadCrumb")]
        [HttpGet]
        public HttpResponseMessage CreditOperationsFilesBreadCrumb(string creditoperationsfileupload_gid)
        {
            breadCrumbList values = new breadCrumbList();
            objDaAccess.DaCreditOperationsFilesBreadCrumb(creditoperationsfileupload_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCreditOperationsTeamFlag")]
        [HttpGet]
        public HttpResponseMessage GetCreditOperationsTeamFlag()
        {
            MdlCreditTeamFlag values = new MdlCreditTeamFlag();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaGetCreditOperationsTeamFlag(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDepartmentList")]
        [HttpGet]
        public HttpResponseMessage GetDepartmentList()
        {
            Departmentname values = new Departmentname();
            objDaAccess.DaGetDepartmentList(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
