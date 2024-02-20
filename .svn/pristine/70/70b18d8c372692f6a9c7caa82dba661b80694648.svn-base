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
    [RoutePrefix("api/zonalMapping")]
    [Authorize]

    public class zonalMappingController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaZonalMapping objDaZonalMapping = new DaZonalMapping();

        [ActionName("postzonalMapping")]
        [HttpPost]
        public HttpResponseMessage PostzonalMapping(zonalMapping values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaZonalMapping.DaPostzonalMapping(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("updatezonalMapping")]
        [HttpPost]
        public HttpResponseMessage PostUpdatezonalMapping(zonalMapping values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaZonalMapping.DaPostUpdatezonalMapping(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getzonalMappingdtl")]
        [HttpGet]
        public HttpResponseMessage GetZonalMappingDtl()
        {
            zonalMappinglist objmappingdtl = new zonalMappinglist();
            objDaZonalMapping.DaGetZonalMappingDtl(objmappingdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtl);
        }

        [ActionName("getviewzonalmappingdtl")]
        [HttpGet]
        public HttpResponseMessage getviewzonalmappingdtl(string zonalmapping_gid)
        {
            zonalMapping objmappingdtl = new zonalMapping();
            objDaZonalMapping.DaGetViewZonalMappingDtl(zonalmapping_gid,objmappingdtl);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtl);
        }

        [ActionName("poststatetag2zonal")]
        [HttpPost]
        public HttpResponseMessage PostStateTag2Zonal(tagzonalmappinglist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaZonalMapping.DaPostStateTag2Zonal(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("postupdatezonalmapping")]
        [HttpPost]
        public HttpResponseMessage PostUdateZonalMapping(tagzonalmappinglist values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            tagzonalmappinglist objvalues = new tagzonalmappinglist();
            objDaZonalMapping.DaPostUpdateZonalMapping(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("getzonaldtl")]
        [HttpGet]
        public HttpResponseMessage GetZonalList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            zonalMappinglist objmappingdtl = new zonalMappinglist();
            objDaZonalMapping.DaGetZonalList(objmappingdtl, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmappingdtl);
        }

        [ActionName("getRMstatedistrict")]
        [HttpGet]
        public HttpResponseMessage GetRMStateDistrict(string assigned_RMGid)
        {
            assignedRMdtl objvalues = new assignedRMdtl();
            objDaZonalMapping.DaGetRMStateDistrict(assigned_RMGid, objvalues);
            return Request.CreateResponse(HttpStatusCode.OK, objvalues);
        }

        [ActionName("employee")]
        [HttpGet]
        public HttpResponseMessage GetEmployee()
        {
            employee objemployee = new employee();
            objDaZonalMapping.DaGetEmployee(objemployee);
            return Request.CreateResponse(HttpStatusCode.OK, objemployee);
        }
    }
}
