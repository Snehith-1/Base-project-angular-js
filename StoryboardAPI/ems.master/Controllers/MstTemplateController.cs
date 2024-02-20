using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using ems.master.Models;
using ems.master.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.idas.Controllers
{
    [RoutePrefix("api/MstTemplate")]
    [Authorize]
    public class MstTemplateController : ApiController
    {
        DaMstTemplate objDaAccess = new DaMstTemplate();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
         
        [ActionName("GetTemplateSummary")]
        [HttpGet]
        public HttpResponseMessage GetTemplateSummary(string vertical_gid,string template_type)
        {
            MdlTemplateDtlsList objTemplateSummary = new MdlTemplateDtlsList();
            objDaAccess.DaGetTemplateSummary(objTemplateSummary, vertical_gid, template_type);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateSummary);
        }

        [ActionName("PostTemplateDtl")]
        [HttpPost]
        public HttpResponseMessage PostTemplateDtl(MdlTemplateDtls values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostTemplateDtl(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTemplateDtl")]
        [HttpGet]
        public HttpResponseMessage GetTemplateDtl(string template_gid)
        {
            MdlTemplateDtls objtemplatedtl = new MdlTemplateDtls();
            objDaAccess.DaGetTemplateDtl(template_gid, objtemplatedtl);
            return Request.CreateResponse(HttpStatusCode.OK, objtemplatedtl);
        } 

        [ActionName("UpdateTemplateDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateTemplateDtl(MdlTemplateDtls values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaUpdateTemplateDtl(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetInputDropdown")]
        [HttpGet]
        public HttpResponseMessage GetInputDropdown(string template_gid, string input_type)
        {
            MdlInputTypeList objTemplateSummary = new MdlInputTypeList();
            objDaAccess.DaGetInputDropdown(template_gid, input_type, objTemplateSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateSummary);
        }

        [ActionName("GetTrnInputList")]
        [HttpGet]
        public HttpResponseMessage GetTrnInputList(string template_gid, string templatetype_gid)
        {
            MdlInputTypeList objTemplateSummary = new MdlInputTypeList();
            objDaAccess.DaGetTrnInputList(template_gid, templatetype_gid, objTemplateSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateSummary);
        }

        [ActionName("PostTrnInputList")]
        [HttpPost]
        public HttpResponseMessage PostTrnInputList(MdlInputTypeList values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostTrnInputList(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVerticalProgramTemplate")]
        [HttpGet]
        public HttpResponseMessage GetVerticalProgramTemplate(string application_gid)
        {
            MdlTemplatelist objTemplateSummary = new MdlTemplatelist();
            objDaAccess.DaGetVerticalProgramTemplate(application_gid, objTemplateSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateSummary);
        }

        [ActionName("PostTrnTemplateConfirm")]
        [HttpPost]
        public HttpResponseMessage PostTrnTemplateConfirm(MdlTemplateGeneratedtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostTrnTemplateConfirm(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetFieldMappingSummary")]
        [HttpGet]
        public HttpResponseMessage GetFieldMappingSummary()
        {
            MdlFieldMappinglist objFieldMapping = new MdlFieldMappinglist();
            objDaAccess.DaGetFieldMappingSummary(objFieldMapping);
            return Request.CreateResponse(HttpStatusCode.OK, objFieldMapping);
        }

        [ActionName("PostFieldMappingDtl")]
        [HttpPost]
        public HttpResponseMessage PostFieldMappingDtl(MdlFieldMappingdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaAccess.DaPostFieldMappingDtl(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
         
        [ActionName("GetFieldMappingDtl")]
        [HttpGet]
        public HttpResponseMessage GetFieldMappingDtl(string fieldmapping_gid)
        {
            MdlFieldMappingdtl objvalues = new MdlFieldMappingdtl();
            objDaAccess.DaGetFieldMappingDtl(fieldmapping_gid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetFieldMappingDropdown")]
        [HttpGet]
        public HttpResponseMessage GetFieldMappingDropdown(string input_type)
        {
            MdlFieldMappingDropdownlist objvalues = new MdlFieldMappingDropdownlist();
            objDaAccess.DaGetFieldMappingDropdown(input_type, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("GetTrnDBInputList")]
        [HttpGet]
        public HttpResponseMessage GetTrnDBInputList(string template_gid, string templatetype_gid,string application_gid)
        {
            MdlDBInputList objTemplateSummary = new MdlDBInputList();
            objDaAccess.DaGetTrnDBInputList(template_gid, templatetype_gid, application_gid, objTemplateSummary);
            return Request.CreateResponse(HttpStatusCode.OK, objTemplateSummary);
        }
         
    }
}
