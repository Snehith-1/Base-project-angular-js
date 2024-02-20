using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.rsk.Models;
using ems.rsk.DataAccess;

namespace StoryboardAPI.Controllers.ems.rsk
{

    [RoutePrefix("api/rmMapping")]
    [Authorize]

    public class rmMappingController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaRmMapping objDaRmMapping = new DaRmMapping();

        [ActionName("getstatedtls")]
        [HttpGet]
        public HttpResponseMessage GetStateDtls()
        {
            statedtlList objstatedtl = new statedtlList();
            objDaRmMapping.DaGetStateDtls(objstatedtl);
            return Request.CreateResponse(HttpStatusCode.OK, objstatedtl);
        }

        [ActionName("getdistrictdtls")]
        [HttpGet]
        public HttpResponseMessage GetDistrictDtls(string state_gid)
        {
            statedtlList objstatedtlList = new statedtlList();
             objDaRmMapping.DaGetDistrictDtls(objstatedtlList,state_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objstatedtlList);
        }

        [ActionName("GetZonalStateDtls")]
        [HttpGet]
        public HttpResponseMessage GetZonalStateDtls(string zonalmapping_gid)
        {
            statedtlList values = new statedtlList();
            objDaRmMapping.GetZonalStateDtls(values, zonalmapping_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getdistrictList")]
        [HttpGet]
        public HttpResponseMessage GetDistrictList()
        {
            statedtlList objstatedtl = new statedtlList();
            objDaRmMapping.DaGetDistrictList(objstatedtl);
            return Request.CreateResponse(HttpStatusCode.OK, objstatedtl);
        }

        [ActionName("postmappingdetails")]
        [HttpPost]
        public HttpResponseMessage PostMappingDetails(mappingdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mappingdtl objmappingdtl = new mappingdtl();
            objDaRmMapping.DaPostMappingDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtl);
        }

        [ActionName("getmappingsummary")]
        [HttpGet]
        public HttpResponseMessage GetMappingSummay()
        {
            mappingdtlList objmappingdtlList = new mappingdtlList();
            objDaRmMapping.DaGetMappingSummay(objmappingdtlList);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtlList);
        }

        [ActionName("updatemappingdetails")]
        [HttpPost]
        public HttpResponseMessage PostUpdMappingDetails(mappingdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRmMapping.DaPostUpdMappingDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("postcustomerMappingdetails")]
        [HttpPost]
        public HttpResponseMessage PostCustomerMappingDetails(mappingdtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaRmMapping.DaPostCustomerMappingDetails(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("deletemappingdtl")]
        [HttpGet]
        public HttpResponseMessage GetDeleteMappingDtl(string RMmapping_gid)
        {
            resultsample objresult = new resultsample();
            objDaRmMapping.DaGetDeleteMappingDtl(RMmapping_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("getmappingdtl")]
        [HttpGet]
        public HttpResponseMessage GetMappingDtl(string RMmapping_gid)
        {
            mappingdtl objmappingdtl = new mappingdtl();
            objDaRmMapping.DaGetMappingDtl(RMmapping_gid, objmappingdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtl);
        }

    }
}
