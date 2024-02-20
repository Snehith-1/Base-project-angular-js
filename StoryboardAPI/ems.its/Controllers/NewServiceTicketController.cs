using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.its.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using ems.its.DataAccess;
using System.Web;

namespace StoryboardAPI.Controllers.its
{
    [RoutePrefix("api/newServiceTicket")]
    [Authorize]
    public class newServiceTicketController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        //fnNewServiceTicket objfnNewServiceTicket = new fnNewServiceTicket();
        DaNewServiceTicket objDaNewServiceTicket = new DaNewServiceTicket();
        //  New Service Ticket .......//

        //Category //

        [ActionName("category")]
        [HttpGet]
        public HttpResponseMessage GetCategory()
        {
            category objcategory = new category();
           objDaNewServiceTicket.DaGetCategory(objcategory);
            return Request.CreateResponse(HttpStatusCode.OK, objcategory);
        }

        // Subcategory//

        [ActionName("subcategory")]
        [HttpGet]
        public HttpResponseMessage GetSubCategory(string category_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            subcategory objsubcategory = new subcategory();
            var status = objDaNewServiceTicket.DaGetSubCategory(category_gid, getsessionvalues.employee_gid, objsubcategory);
            return Request.CreateResponse(HttpStatusCode.OK, objsubcategory);

        }

        //Type//

        [ActionName("type")]
        [HttpGet]
        public HttpResponseMessage Gettype(string subcategory_gid)
        {
            type objtype = new type();
            var status = objDaNewServiceTicket.Gettype_da(subcategory_gid, objtype);
            return Request.CreateResponse(HttpStatusCode.OK, objtype);
        }

        // Employee //
        [ActionName("employee")]
        [HttpGet]
        public HttpResponseMessage Getemployee()
        {
            employee objemployee = new employee();
            //objemployee = objfnNewServiceTicket.Getemployeefn();
            var status = objDaNewServiceTicket.DaGetEmployee(objemployee);
            if (status == true)
            {
                objemployee.status = true;
            }
            else
            {
                objemployee.status = false;
            }
            return Request.CreateResponse(HttpStatusCode.OK, objemployee);
        }

        // Employee_Contactlist From Session of user_gid //

        [ActionName("employee_contactdetails")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeContactDetails(string category_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            employeecontactlist objvalues = new employeecontactlist();
            var status = objDaNewServiceTicket.DaEmployeeContactDetails(getsessionvalues.employee_gid, objvalues, category_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        // Employee Contact List //


        [ActionName("employeecontactdetails")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeContactDetails(string employee_gid, string category_gid)
        {
            employeecontact_getgid objemployee_get = new employeecontact_getgid();
            var status = objDaNewServiceTicket.DaGetEmployeeContactDetails(employee_gid, objemployee_get, category_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee_get);
        }

        // Temp Document Clear ..//

        [ActionName("tmpcleardocument")]
        [HttpGet]
        public HttpResponseMessage GetTmpDocumentClear()
        {
            cleartmpdocument objtmpclearDocument = new cleartmpdocument();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            var status = objDaNewServiceTicket.DaDocumentClear(getsessionvalues.user_gid,objtmpclearDocument);
            return Request.CreateResponse(HttpStatusCode.OK, objtmpclearDocument);
        }

        // Raise Ticket //
        [ActionName("raiseticket")]
        [HttpPost]
        public HttpResponseMessage PostRaiseTicket(raisesubmit objvalues)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            raisesubmit objraisesubmit = new raisesubmit();
            var status = objDaNewServiceTicket.DaPostRaiseTicketInsert(objvalues, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("UploadDocument")]
        [HttpPost]
        public HttpResponseMessage PostUploadDocument()
        {

            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            UploadDocumentname documentname = new UploadDocumentname();
            documentname = objDaNewServiceTicket.DaPostUploadDocument(httpRequest, getsessionvalues.employee_gid, getsessionvalues.user_gid, documentname);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("documentdelete")]
        [HttpGet]
        public HttpResponseMessage GetDocumentName(string id)

        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            documentcancel objdocumentcancel = new documentcancel();
            var status = objDaNewServiceTicket.DaGetDocumentCancel(id,objdocumentcancel);
            return Request.CreateResponse(HttpStatusCode.OK, objdocumentcancel);
        }

    }
}
