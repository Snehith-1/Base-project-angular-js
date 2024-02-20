using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Net.Http;
using System.Web.Http;
using ems.help.Models;
using ems.help.DataAccess;
namespace ems.help.Controllers
{
    [RoutePrefix("api/HlpMstHelp")]
    [Authorize]
    public class HlpMstHelpController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaHlpMstHelp objDahelp = new DaHlpMstHelp();


        
        [ActionName("getpage")]
        [HttpGet]
        public HttpResponseMessage getpage(string module_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mdlpagename values = new mdlpagename();
            objDahelp.Dagetpage(values, getsessionvalues.employee_gid,module_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Getfaq")]
        [HttpGet]
        public HttpResponseMessage Getfaq(string module_gid)
        {
            FaqList values = new FaqList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDahelp.DaGetfaqSummary(getsessionvalues.user_gid, module_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("Getreplyofqueries")]
        [HttpGet]
        public HttpResponseMessage Getreplyofqueries(string module_gid)
        {
            replyquerylist values = new replyquerylist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDahelp.DaGetreplyofquerySummary(getsessionvalues.user_gid, module_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("Getqueries")]
        [HttpGet]
        public HttpResponseMessage Getqueries(string module_gid)
        {
            querylist values = new querylist();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDahelp.DaGetquerySummary(getsessionvalues.employee_gid, module_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("Getusefulresource")]
        [HttpGet]
        public HttpResponseMessage Getusefulresource( string module_gid)
        {
            UsefulResourceList values = new UsefulResourceList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDahelp.DaGetusefulresourceSummary(getsessionvalues.user_gid, module_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

         [ActionName("Getmodule")]
        [HttpGet]
        public HttpResponseMessage Getmodule()
        {
            moduleList values = new moduleList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDahelp.DaGetmodule(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("Getsinglemodule")]
        [HttpGet]
        public HttpResponseMessage Getsinglemodule()
        {
            modulename values = new modulename();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDahelp.DaGetsinglemodule(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("Getcompanylogo")]
        [HttpGet]
        public HttpResponseMessage Getcompanylogo()
        {
            logo values = new logo();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDahelp.DaGetcompanylogo(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }

        [ActionName("Getsinglemodulename")]
        [HttpGet]
        public HttpResponseMessage Getsinglemodulename( string module_gid)
        {
            modulename values = new modulename();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDahelp.DaGetsinglemodulename(getsessionvalues.user_gid, module_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }


        [ActionName("Gethowtouse")]
        [HttpGet]
        public HttpResponseMessage Gethowtouse(string module_gid)
        {
            HowToUseList values = new HowToUseList();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDahelp.DaGethowtouseSummary(getsessionvalues.user_gid, module_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);

        }


        //[ActionName("Gethowtouse")]
        //[HttpGet]
        //public HttpResponseMessage Gethowtouse(string module_gid)
        //{
        //    HowToUseList values = new HowToUseList();
        //    string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
        //    getsessionvalues = Objgetgid.gettokenvalues(token);
        //    objDaJob.DaGethowtouseSummary(module_gid,getsessionvalues.user_gid, values);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);

        //}

        [ActionName("GetUsefulDocument")]
        [HttpGet]
        public HttpResponseMessage GetUsefulDocument(string usefulresourcesdocument_gid)
        {
            usedocument values = new usedocument();
            objDahelp.DaGetUsefulDocument(values, usefulresourcesdocument_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("queryCreate")]
        [HttpPost]
        public HttpResponseMessage PostqueryCreate(mdlquery values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDahelp.DaPostqueryCreate(values, getsessionvalues.employee_gid,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("attachmentUpload")]
        [HttpPost]
        public HttpResponseMessage attachmentUpload()
        {
            HttpRequest httpRequest;
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            httpRequest = HttpContext.Current.Request;
            uploaddocument documentname = new uploaddocument();
            objDahelp.DaPostattachPhoto(httpRequest, documentname, getsessionvalues.employee_gid, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, documentname);
        }

        [ActionName("getattachPhoto")]
        [HttpGet]
        public HttpResponseMessage getattachPhoto()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            attachphotolist objvalues = new attachphotolist();
            objDahelp.DaGetattachPhoto(objvalues,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("querycancel")]
        [HttpGet]
        public HttpResponseMessage querycancel(string raisequery_gid)
        {
            querylist values = new querylist();
            objDahelp.DaqueryCancel(raisequery_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("attachphotoUploadcancel")]
        [HttpGet]
        public HttpResponseMessage attachphotoUploadcancel(string id)
        {
            attachphotolist values = new attachphotolist();
            objDahelp.DaattachphotoCancel(id, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("screenshottempdelete")]
        [HttpGet]
        public HttpResponseMessage screenshottempdelete()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            attachphotolist values = new attachphotolist();
            objDahelp.DaattachphotooverallCancel(values,getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}
