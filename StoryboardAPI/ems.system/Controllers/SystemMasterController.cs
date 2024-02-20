using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.system.Models;
using ems.system.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.system.Controllers
{
    [RoutePrefix("api/SystemMaster")]
    [Authorize]

    public class SystemMasterController : ApiController
    {
        DaSystemMaster objDaSystemMaster = new DaSystemMaster();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        //
        //Blood Group
        [ActionName("GetBloodGroup")]
        [HttpGet]
        public HttpResponseMessage GetBloodGroup()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetBloodGroup(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        [ActionName("GetBloodGroupActive")]
        [HttpGet]
        public HttpResponseMessage GetBloodGroupActive()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetBloodGroupActive(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("CreateBloodGroup")]
        [HttpPost]
        public HttpResponseMessage CreateBloodGroup(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateBloodGroup(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditBloodGroup")]
        [HttpGet]
        public HttpResponseMessage EditBloodGroup(string bloodgroup_gid)
        {
            master values = new master();
            objDaSystemMaster.DaEditBloodGroup(bloodgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateBloodGroup")]
        [HttpPost]
        public HttpResponseMessage UpdateBloodGroup(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateBloodGroup(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveBloodGroup")]
        [HttpPost]
        public HttpResponseMessage InactiveBloodGroup(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveBloodGroup(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBloodGroup")]
        [HttpGet]
        public HttpResponseMessage DeleteBloodGroup(string bloodgroup_gid)
        {
            master values = new master();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteBloodGroup(bloodgroup_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BloodGroupInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage BloodGroupInactiveLogview(string bloodgroup_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaBloodGroupInactiveLogview(bloodgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Base Location
        [ActionName("GetBaseLocation")]
        [HttpGet]
        public HttpResponseMessage GetBaseLocation()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetBaseLocation(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        [ActionName("GetBaseLocationlist")]
        [HttpGet]
        public HttpResponseMessage GetBaseLocationlist()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetBaseLocationlist(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        [ActionName("GetBaseLocationlistActive")]
        [HttpGet]
        public HttpResponseMessage GetBaseLocationlistActive()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetBaseLocationlistActive(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("CreateBaseLocation")]
        [HttpPost]
        public HttpResponseMessage CreateBaseLocation(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateBaseLocation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditBaseLocation")]
        [HttpGet]
        public HttpResponseMessage EditBaseLocation(string baselocation_gid)
        {
            master values = new master();
            objDaSystemMaster.DaEditBaseLocation(baselocation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateBaseLocation")]
        [HttpPost]
        public HttpResponseMessage UpdateBaseLocation(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateBaseLocation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveBaseLocation")]
        [HttpPost]
        public HttpResponseMessage InactiveBaseLocation(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveBaseLocation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBaseLocation")]
        [HttpGet]
        public HttpResponseMessage DeleteBaseLocation(string baselocation_gid)
        {
            master values = new master();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteBaseLocation(baselocation_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BaseLocationInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage BaseLocationInactiveLogview(string baselocation_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaBaseLocationInactiveLogview(baselocation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Physical Status
        [ActionName("GetPhysicalStatus")]
        [HttpGet]
        public HttpResponseMessage GetPhysicalStatus()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetPhysicalStatus(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("CreatePhysicalStatus")]
        [HttpPost]
        public HttpResponseMessage CreatePhysicalStatus(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreatePhysicalStatus(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditPhysicalStatus")]
        [HttpGet]
        public HttpResponseMessage EditPhysicalStatus(string physicalstatus_gid)
        {
            master values = new master();
            objDaSystemMaster.DaEditPhysicalStatus(physicalstatus_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdatePhysicalStatus")]
        [HttpPost]
        public HttpResponseMessage UpdatePhysicalStatus(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdatePhysicalStatus(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactivePhysicalStatus")]
        [HttpPost]
        public HttpResponseMessage InactivePhysicalStatus(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactivePhysicalStatus(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeletePhysicalStatus")]
        [HttpGet]
        public HttpResponseMessage DeletePhysicalStatus(string physicalstatus_gid)
        {
            master values = new master();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeletePhysicalStatus(physicalstatus_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PhysicalStatusInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage PhysicalStatusInactiveLogview(string physicalstatus_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaPhysicalStatusInactiveLogview(physicalstatus_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Calendar Group
        [ActionName("GetCalendarGroup")]
        [HttpGet]
        public HttpResponseMessage GetCalendarGroup()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetCalendarGroup(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("CreateCalendarGroup")]
        [HttpPost]
        public HttpResponseMessage CreateCalendarGroup(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateCalendarGroup(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditCalendarGroup")]
        [HttpGet]
        public HttpResponseMessage EditCalendarGroup(string calendargroup_gid)
        {
            master values = new master();
            objDaSystemMaster.DaEditCalendarGroup(calendargroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateCalendarGroup")]
        [HttpPost]
        public HttpResponseMessage UpdateCalendarGroup(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateCalendarGroup(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveCalendarGroup")]
        [HttpPost]
        public HttpResponseMessage InactiveCalendarGroup(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveCalendarGroup(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteCalendarGroup")]
        [HttpGet]
        public HttpResponseMessage DeleteCalendarGroup(string calendargroup_gid)
        {
            master values = new master();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteCalendarGroup(calendargroup_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CalendarGroupInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage CalendarGroupInactiveLogview(string calendargroup_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaCalendarGroupInactiveLogview(calendargroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //SubFunction
        [ActionName("GetSubFunction")]
        [HttpGet]
        public HttpResponseMessage GetSubFunction()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetSubFunction(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("CreateSubFunction")]
        [HttpPost]
        public HttpResponseMessage CreateSubFunction(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateSubFunction(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditSubFunction")]
        [HttpGet]
        public HttpResponseMessage EditSubFunction(string subfunction_gid)
        {
            master values = new master();
            objDaSystemMaster.DaEditSubFunction(subfunction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateSubFunction")]
        [HttpPost]
        public HttpResponseMessage UpdateSubFunction(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateSubFunction(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveSubFunction")]
        [HttpPost]
        public HttpResponseMessage InactiveSubFunction(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveSubFunction(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteSubFunction")]
        [HttpGet]
        public HttpResponseMessage DeleteSubFunction(string subfunction_gid)
        {
            master values = new master();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteSubFunction(subfunction_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SubFunctionInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage SubFunctionInactiveLogview(string subfunction_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaSubFunctionInactiveLogview(subfunction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Salutation
        [ActionName("GetSalutation")]
        [HttpGet]
        public HttpResponseMessage GetSalutation()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetSalutation(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("CreateSalutation")]
        [HttpPost]
        public HttpResponseMessage CreateSalutation(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateSalutation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditSalutation")]
        [HttpGet]
        public HttpResponseMessage EditSalutation(string salutation_gid)
        {
            master values = new master();
            objDaSystemMaster.DaEditSalutation(salutation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateSalutation")]
        [HttpPost]
        public HttpResponseMessage UpdateSalutation(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateSalutation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveSalutation")]
        [HttpPost]
        public HttpResponseMessage InactiveSalutation(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveSalutation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteSalutation")]
        [HttpGet]
        public HttpResponseMessage DeleteSalutation(string salutation_gid)
        {
            master values = new master();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteSalutation(salutation_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SalutationInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage SalutationInactiveLogview(string salutation_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaSalutationInactiveLogview(salutation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Project

        [ActionName("CreateProject")]
        [HttpPost]
        public HttpResponseMessage CreateProject(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateProject(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProject")]
        [HttpGet]
        public HttpResponseMessage GetProject()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetProject(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("EditProject")]
        [HttpGet]
        public HttpResponseMessage EditProject(string project_gid)
        {
            master values = new master();
            objDaSystemMaster.DaEditProject(project_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateProject")]
        [HttpPost]
        public HttpResponseMessage UpdateProject(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateProject(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveProject")]
        [HttpPost]
        public HttpResponseMessage InactiveProject(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveProject(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteProject")]
        [HttpGet]
        public HttpResponseMessage DeleteProject(string project_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteProject(project_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ProjectInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage ProjectInactiveLogview(string project_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaProjectInactiveLogview(project_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Cluster Add
        [ActionName("PostClusterAdd")]
        [HttpPost]
        public HttpResponseMessage PostClusterAdd(cluster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostClusterAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Cluster Summary
        [ActionName("GetClusterSummary")]
        [HttpGet]
        public HttpResponseMessage GetClusterSummary()
        {
            cluster objmaster = new cluster();
            objDaSystemMaster.DaGetClusterSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetClusterEdit")]
        [HttpGet]
        public HttpResponseMessage GetClusterEdit(string cluster_gid)
        {
            cluster objmaster = new cluster();
            objDaSystemMaster.DaGetClusterEdit(cluster_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostClusterUpdate")]
        [HttpPost]
        public HttpResponseMessage PostClusterUpdate(cluster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostClusterUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCluster2BaseLocation")]
        [HttpGet]
        public HttpResponseMessage GetCluster2BaseLocation(string cluster_gid)
        {
            cluster objmaster = new cluster();
            objDaSystemMaster.DaGetCluster2BaseLocation(cluster_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        //UnTagged Locations
        [ActionName("GetUnTaggedLocations")]
        [HttpGet]
        public HttpResponseMessage GetUnTaggedLocations()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetUnTaggedLocations(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        //UnTagged Locations Edit
        [ActionName("GetUnTaggedLocationsEdit")]
        [HttpGet]
        public HttpResponseMessage GetUnTaggedLocationsEdit(string cluster_gid)
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetUnTaggedLocationsEdit(objmaster, cluster_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("InactiveCluster")]
        [HttpPost]
        public HttpResponseMessage InactiveCluster(cluster values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveCluster(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ClusterInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage ClusterInactiveLogview(string cluster_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaClusterInactiveLogview(cluster_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Region Add
        [ActionName("PostRegionAdd")]
        [HttpPost]
        public HttpResponseMessage PostRegionAdd(region values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostRegionAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Region Summary
        [ActionName("GetRegionSummary")]
        [HttpGet]
        public HttpResponseMessage GetRegionSummary()
        {
            region objmaster = new region();
            objDaSystemMaster.DaGetRegionSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetRegionEdit")]
        [HttpGet]
        public HttpResponseMessage GetRegionEdit(string region_gid)
        {
            region objmaster = new region();
            objDaSystemMaster.DaGetRegionEdit(region_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostRegionUpdate")]
        [HttpPost]
        public HttpResponseMessage PostRegionUpdate(region values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostRegionUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRegion2Cluster")]
        [HttpGet]
        public HttpResponseMessage GetRegion2Cluster(string region_gid)
        {
            cluster objmaster = new cluster();
            objDaSystemMaster.DaGetRegion2Cluster(region_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        //UnTagged Clusters
        [ActionName("GetUnTaggedClusters")]
        [HttpGet]
        public HttpResponseMessage GetUnTaggedClusters()
        {
            region objmaster = new region();
            objDaSystemMaster.DaGetUnTaggedClusters(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        //UnTagged Clusters
        [ActionName("GetUnTaggedClustersEdit")]
        [HttpGet]
        public HttpResponseMessage GetUnTaggedClustersEdit(string region_gid)
        {
            region objmaster = new region();
            objDaSystemMaster.DaGetUnTaggedClustersEdit(objmaster, region_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostRegionInactive")]
        [HttpPost]
        public HttpResponseMessage PostRegionInactive(region values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostRegionInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRegionInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetRegionInactiveLogview(string region_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaGetRegionInactiveLogview(region_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //// Zone Add
        [ActionName("PostZoneAdd")]
        [HttpPost]
        public HttpResponseMessage PostZoneAdd(zone values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostZoneAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Zone Summary
        [ActionName("GetZoneSummary")]
        [HttpGet]
        public HttpResponseMessage GetZoneSummary()
        {
            zone objmaster = new zone();
            objDaSystemMaster.DaGetZoneSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetZoneEdit")]
        [HttpGet]
        public HttpResponseMessage GetZoneEdit(string zone_gid)
        {
            zone objmaster = new zone();
            objDaSystemMaster.DaGetZoneEdit(zone_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostZoneUpdate")]
        [HttpPost]
        public HttpResponseMessage PostZoneUpdate(zone values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostZoneUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetZone2Region")]
        [HttpGet]
        public HttpResponseMessage GetZone2Region(string zone_gid)
        {
            region objmaster = new region();
            objDaSystemMaster.DaGetZone2Region(zone_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        //UnTagged Region
        [ActionName("GetUnTaggedRegions")]
        [HttpGet]
        public HttpResponseMessage GetUnTaggedRegions()
        {
            zone objmaster = new zone();
            objDaSystemMaster.DaGetUnTaggedRegions(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        //UnTagged Region
        [ActionName("GetUnTaggedRegionsEdit")]
        [HttpGet]
        public HttpResponseMessage GetUnTaggedRegionsEdit(string zone_gid)
        {
            zone objmaster = new zone();
            objDaSystemMaster.DaGetUnTaggedRegionsEdit(objmaster, zone_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostZoneInactive")]
        [HttpPost]
        public HttpResponseMessage PostZoneInactive(zone values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostZoneInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetZoneInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetZoneInactiveLogview(string zone_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaGetZoneInactiveLogview(zone_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Region List
        [ActionName("GetRegionList")]
        [HttpGet]
        public HttpResponseMessage GetRegionList()
        {
            region objmaster = new region();
            objDaSystemMaster.DaGetRegionList(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        // Vertical List

        [ActionName("GetVerticallist")]
        [HttpGet]
        public HttpResponseMessage GetVerticallist()
        {
            mdlvertical objmaster = new mdlvertical();
            objDaSystemMaster.DaGetVerticallist(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        // Employee list 

        [ActionName("GetEmployeelist")]
        [HttpGet]
        public HttpResponseMessage GetEmployeelist()
        {
            mdlemployee objmaster = new mdlemployee();
            objDaSystemMaster.DaGetEmployeelist(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        // Region Head Add
        [ActionName("PostRegionHeadAdd")]
        [HttpPost]
        public HttpResponseMessage PostRegionHeadAdd(mdlregionhead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostRegionHeadAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Region Head Summary
        [ActionName("GetRegionHeadSummary")]
        [HttpGet]
        public HttpResponseMessage GetRegionHeadSummary()
        {
            region objmaster = new region();
            objDaSystemMaster.DaGetRegionHeadSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostRegionHeadInactive")]
        [HttpPost]
        public HttpResponseMessage PostRegionHeadInactive(mdlregionhead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostRegionHeadInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRegionHeadInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetRegionHeadInactiveLogview(string regionhead_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaGetRegionHeadInactiveLogview(regionhead_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetRegionHeadEdit")]
        [HttpGet]
        public HttpResponseMessage GetRegionHeadEdit(string regionhead_gid)
        {
            mdlregionhead objmaster = new mdlregionhead();
            objDaSystemMaster.DaGetRegionHeadEdit(regionhead_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostRegionHeadUpdate")]
        [HttpPost]
        public HttpResponseMessage PostRegionHeadUpdate(mdlregionhead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostRegionHeadUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Zone List
        [ActionName("GetZoneList")]
        [HttpGet]
        public HttpResponseMessage GetZoneList()
        {
            zone objmaster = new zone();
            objDaSystemMaster.DaGetZoneList(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        // Business Head Add
        [ActionName("PostBusinessHeadAdd")]
        [HttpPost]
        public HttpResponseMessage PostBusinessHeadAdd(mdlbusinesshead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostBusinessHeadAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Business Head Summary
        [ActionName("GetBusinessHeadSummary")]
        [HttpGet]
        public HttpResponseMessage GetBusinessHeadSummary()
        {
            mdlbusinesshead objmaster = new mdlbusinesshead();
            objDaSystemMaster.DaGetBusinessHeadSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostBusinessHeadInactive")]
        [HttpPost]
        public HttpResponseMessage PostBusinessHeadInactive(mdlbusinesshead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostBusinessHeadInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBusinessHeadInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetBusinessHeadInactiveLogview(string businesshead_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaGetBusinessHeadInactiveLogview(businesshead_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBusinessHeadEdit")]
        [HttpGet]
        public HttpResponseMessage GetBusinessHeadEdit(string businesshead_gid)
        {
            mdlbusinesshead objmaster = new mdlbusinesshead();
            objDaSystemMaster.DaGetBusinessHeadEdit(businesshead_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostBusinessHeadUpdate")]
        [HttpPost]
        public HttpResponseMessage PostBusinessHeadUpdate(mdlbusinesshead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostBusinessHeadUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Group Business Head Add
        [ActionName("PostGroupBusinessHeadAdd")]
        [HttpPost]
        public HttpResponseMessage PostGroupBusinessHeadAdd(mdlbusinesshead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostGroupBusinessHeadAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Business Head Summary
        [ActionName("GetGroupBusinessHeadSummary")]
        [HttpGet]
        public HttpResponseMessage GetGroupBusinessHeadSummary()
        {
            mdlbusinesshead objmaster = new mdlbusinesshead();
            objDaSystemMaster.DaGetGroupBusinessHeadSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostGroupBusinessHeadInactive")]
        [HttpPost]
        public HttpResponseMessage PostGroupBusinessHeadInactive(mdlbusinesshead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostGroupBusinessHeadInactive(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGroupBusinessHeadInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage GetGroupBusinessHeadInactiveLogview(string groupbusinesshead_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaGetGroupBusinessHeadInactiveLogview(groupbusinesshead_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGroupBusinessHeadEdit")]
        [HttpGet]
        public HttpResponseMessage GetGroupBusinessHeadEdit(string groupbusinesshead_gid)
        {
            mdlbusinesshead objmaster = new mdlbusinesshead();
            objDaSystemMaster.DaGetGroupBusinessHeadEdit(groupbusinesshead_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostGroupBusinessHeadUpdate")]
        [HttpPost]
        public HttpResponseMessage PostGroupBusinessHeadUpdate(mdlbusinesshead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostGroupBusinessHeadUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Cluster Head codes

        // Clusters List
        [ActionName("GetClusterslist")]
        [HttpGet]
        public HttpResponseMessage GetClusterslist()
        {
            region objmaster = new region();
            objDaSystemMaster.DaGetClusterslist(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        // Cluster Head Add
        [ActionName("PostClusterheadAdd")]
        [HttpPost]
        public HttpResponseMessage PostClusterheadAdd(mdlclusterhead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostClusterheadAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("InactiveClusterhead")]
        [HttpPost]
        public HttpResponseMessage InactiveClusterhead(mdlclusterhead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveClusterhead(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ClusterheadInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage ClusterheadInactiveLogview(string clusterhead_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaClusterheadInactiveLogview(clusterhead_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Cluster Head Summary
        [ActionName("GetClusterHeadSummary")]
        [HttpGet]
        public HttpResponseMessage GetClusterHeadSummary()
        {
            cluster objmaster = new cluster();
            objDaSystemMaster.DaGetClusterHeadSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }


        [ActionName("GetClusterHeadEdit")]
        [HttpGet]
        public HttpResponseMessage GetClusterHeadEdit(string clusterhead_gid)
        {
            mdlclusterhead objmaster = new mdlclusterhead();
            objDaSystemMaster.DaGetClusterHeadEdit(clusterhead_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostClusterHeadUpdate")]
        [HttpPost]
        public HttpResponseMessage PostClusterHeadUpdate(mdlclusterhead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostClusterHeadUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Zonal Head Codes



        [ActionName("GetZonallist")]
        [HttpGet]
        public HttpResponseMessage GetZonalslist()
        {
            zone objmaster = new zone();
            objDaSystemMaster.DaGetZonalslist(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostZonalheadAdd")]
        [HttpPost]
        public HttpResponseMessage PostZonalheadAdd(mdlzonalhead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostZonalheadAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("InactiveZonalhead")]
        [HttpPost]
        public HttpResponseMessage InactiveZonalhead(mdlzonalhead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveZonalhead(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ZonalheadInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage ZonalheadInactiveLogview(string zonalhead_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaZonalheadInactiveLogview(zonalhead_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Cluster Head Summary
        [ActionName("GetZonalHeadSummary")]
        [HttpGet]
        public HttpResponseMessage GetZonalHeadSummary()
        {
            zone objmaster = new zone();
            objDaSystemMaster.DaGetZonalHeadSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }


        [ActionName("GetZonalHeadEdit")]
        [HttpGet]
        public HttpResponseMessage GetZonalHeadEdit(string zonalhead_gid)
        {
            mdlzonalhead objmaster = new mdlzonalhead();
            objDaSystemMaster.DaGetZonalHeadEdit(zonalhead_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostZonalHeadUpdate")]
        [HttpPost]
        public HttpResponseMessage PostZonalHeadUpdate(mdlzonalhead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostZonalHeadUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVerticalProgramList")]
        [HttpGet]
        public HttpResponseMessage GetVerticalProgramList(string vertical_gid,string lstype,string lstypegid)
        {
            verticalprogram_list objmaster = new verticalprogram_list();
            objDaSystemMaster.DaGetVerticalProgramList(vertical_gid, lstype, lstypegid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetEditVerticalProgramList")]
        [HttpGet]
        public HttpResponseMessage GetEditVerticalProgramList(string vertical_gid, string lstype, string lstypegid,string lsmaster_gid)
        {
            verticalprogram_list objmaster = new verticalprogram_list();
            objDaSystemMaster.DaGetEditVerticalProgramList(vertical_gid, lstype, lstypegid, lsmaster_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }


        // Product Head Codes


        [ActionName("PostProductheadAdd")]
        [HttpPost]
        public HttpResponseMessage PostProductheadAdd(mdlproducthead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostProductheadAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("InactiveProducthead")]
        [HttpPost]
        public HttpResponseMessage InactiveProducthead(mdlproducthead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveProducthead(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("ProductheadInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage ProductheadInactiveLogview(string producthead_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaProductheadInactiveLogview(producthead_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetProductHeadSummary")]
        [HttpGet]
        public HttpResponseMessage GetProductHeadSummary()
        {
            zone objmaster = new zone();
            objDaSystemMaster.DaGetProductHeadSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }


        [ActionName("GetProductHeadEdit")]
        [HttpGet]
        public HttpResponseMessage GetProductHeadEdit(string producthead_gid)
        {
            mdlproducthead objmaster = new mdlproducthead();
            objDaSystemMaster.DaGetProductHeadEdit(producthead_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("PostProductHeadUpdate")]
        [HttpPost]
        public HttpResponseMessage PostProductHeadUpdate(mdlproducthead values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostProductHeadUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Task

        [ActionName("PostTaskAdd")]
        [HttpPost]
        public HttpResponseMessage PostTaskAdd(MdlTask values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostTaskAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaskSummary")]
        [HttpGet]
        public HttpResponseMessage GetTaskSummary()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetTaskSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("EditTask")]
        [HttpGet]
        public HttpResponseMessage EditTask(string task_gid)
        {
            MdlTask objmaster = new MdlTask();
            objDaSystemMaster.DaEditTask(task_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("UpdateTask")]
        [HttpPost]
        public HttpResponseMessage UpdateTask(MdlTask values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateTask(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveTask")]
        [HttpPost]
        public HttpResponseMessage InactiveTask(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveTask(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TaskInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage TaskInactiveLogview(string task_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaTaskInactiveLogview(task_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteTask")]
        [HttpGet]
        public HttpResponseMessage DeleteTask(string task_gid)
        {
            master values = new master();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteTask(task_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaskMultiselectList")]
        [HttpGet]
        public HttpResponseMessage GetTaskMultiselectList(string task_gid)
        {
            MdlTask objmaster = new MdlTask();
            objDaSystemMaster.DaGetTaskMultiselectList(task_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        // First Level Menu List
        [ActionName("GetFirstLevelMenu")]
        [HttpGet]
        public HttpResponseMessage GetFirstLevelMenu()
        {
            menu objmaster = new menu();
            objDaSystemMaster.DaGetFirstLevelMenu(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        // Second Level Menu List Based on First Level
        [ActionName("GetSecondLevelMenu")]
        [HttpGet]
        public HttpResponseMessage GetSecondLevelMenu(string module_gid_parent)
        {
            menu objmaster = new menu();
            objDaSystemMaster.DaGetSecondLevelMenu(objmaster, module_gid_parent);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        // Third Level Menu List Based on Second Level
        [ActionName("GetThirdLevelMenu")]
        [HttpGet]
        public HttpResponseMessage GetThirdLevelMenu(string module_gid_parent)
        {
            menu objmaster = new menu();
            objDaSystemMaster.DaGetThirdLevelMenu(objmaster, module_gid_parent);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        // Fourth Level Menu List Based on Second Level
        [ActionName("GetFourthLevelMenu")]
        [HttpGet]
        public HttpResponseMessage GetFourthLevelMenu(string module_gid_parent)
        {
            menu objmaster = new menu();
            objDaSystemMaster.DaGetFourthLevelMenu(objmaster, module_gid_parent);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        // Menu Add
        [ActionName("PostMenudAdd")]
        [HttpPost]
        public HttpResponseMessage PostMenudAdd(menu values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostMenudAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Menu Mapping Summary

        [ActionName("GetMenuMappingSummary")]
        [HttpGet]
        public HttpResponseMessage GetMenuMappingSummary()
        {
            menu objmaster = new menu();
            objDaSystemMaster.DaGetMenuMappingSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetMenuMappingEdit")]
        [HttpGet]
        public HttpResponseMessage GetMenuMappingEdit(string menu_gid)
        {
            menu objmaster = new menu();
            objDaSystemMaster.DaGetMenuMappingEdit(menu_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        //Delete Menu Mappling
        [ActionName("GetMenuMappingDelete")]
        [HttpGet]
        public HttpResponseMessage GetMenuMappingDelete(string menu_gid)
        {
            menu values = new menu();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaGetMenuMappingDelete(menu_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Delete Menu Mappling
        [ActionName("GetMenuMappingInactivate")]
        [HttpPost]
        public HttpResponseMessage GetMenuMappingInactivate(menu values)
        { 
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaGetMenuMappingInactivate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMenuMappingInactivateview")]
        [HttpGet]
        public HttpResponseMessage GetMenuMappingInactivateview(string menu_gid)
        {
            menu values = new menu();
            objDaSystemMaster.DaGetMenuMappingInactivateview(menu_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //HR Notification

        [ActionName("PostHRNotification")]
        [HttpPost]
        public HttpResponseMessage PostHRNotification(MdlHRNotification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostHRNotification(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetHRNotificationSummary")]
        [HttpGet]
        public HttpResponseMessage GetHRNotificationSummary()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetHRNotificationSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("EditHRNotification")]
        [HttpGet]
        public HttpResponseMessage EditHRNotification(string hrnotification_gid)
        {
            MdlHRNotification objmaster = new MdlHRNotification();
            objDaSystemMaster.DaEditHRNotification(hrnotification_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("UpdateHRNotification")]
        [HttpPost]
        public HttpResponseMessage UpdateHRNotification(MdlHRNotification values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateHRNotification(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveHRNotification")]
        [HttpPost]
        public HttpResponseMessage InactiveHRNotificaton(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveHRNotification(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("HRNotificationInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage HRNotificationInactiveLogview(string hrnotification_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaHRNotificationInactiveLogview(hrnotification_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteHRNotification")]
        [HttpGet]
        public HttpResponseMessage DeleteHRNotification(string hrnotification_gid)
        {
            master values = new master();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteHRNotification(hrnotification_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetHRNotificationNotifyToList")]
        [HttpGet]
        public HttpResponseMessage GetHRNotificationNotifyToList(string hrnotification_gid)
        {
            MdlHRNotification objmaster = new MdlHRNotification();
            objDaSystemMaster.DaGetHRNotificationNotifyToList(hrnotification_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("BaselocationReportExcel")]
        [HttpGet]
        public HttpResponseMessage BaselocationReportExcel()
        {
            exportexcel values = new exportexcel();
            objDaSystemMaster.DaBaselocationReportExcel(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ClusterReportExcel")]
        [HttpGet]
        public HttpResponseMessage ClusterReportExcel()
        {
            exportexcel values = new exportexcel();
            objDaSystemMaster.DaClusterReportExcel(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("RegionalReportExcel")]
        [HttpGet]
        public HttpResponseMessage RegionalReportExcel()
        {
            exportexcel values = new exportexcel();
            objDaSystemMaster.DaRegionalReportExcel(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("ZonalReportExcel")]
        [HttpGet]
        public HttpResponseMessage ZonalReportExcel()
        {
            exportexcel values = new exportexcel();
            objDaSystemMaster.DaZonalReportExcel(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Branch Summary     

        [ActionName("GetBranchSummary")]
        [HttpGet]
        public HttpResponseMessage GetBranchSummary()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetBranchSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        //Department Summary     

        [ActionName("GetDepartmentSummary")]
        [HttpGet]
        public HttpResponseMessage GetDepartmentSummary()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetDepartmentSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        //State Summary     

        [ActionName("GetGstStateSummary")]
        [HttpGet]
        public HttpResponseMessage GetGstStateSummary()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetGstStateSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        //OneApiUserRegistration

        [ActionName("GetExternalUser")]
        [HttpGet]
        public HttpResponseMessage GetExternalUser()
        {
            MdlSystemMaster objoneapiuser = new MdlSystemMaster();
            objDaSystemMaster.DaGetExternalUser(objoneapiuser);
            return Request.CreateResponse(HttpStatusCode.OK, objoneapiuser);
        }

        [ActionName("PostExternalUser")]
        [HttpPost]
        public HttpResponseMessage PostExternalUser(externaluser_lists values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);           
            objDaSystemMaster.DaPostExternalUser(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        

        [ActionName("PopSystemOwner")]
        [HttpGet]
        public HttpResponseMessage PopSystemOwner()
        {
            MdlSystemMaster objoneapiuser = new MdlSystemMaster();
            objDaSystemMaster.DaPopSystemOwner(objoneapiuser);
            return Request.CreateResponse(HttpStatusCode.OK, objoneapiuser);
        }
        [ActionName("CreateUserReg")]
        [HttpPost]
        public HttpResponseMessage CreateUserReg(userpurpose values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateUserReg(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("AssignedSystemOwner")]
       
        [HttpGet]
        public HttpResponseMessage AssignedSystemOwner(string user2oneapi_gid)
        {
            userpurpose values = new userpurpose();
            objDaSystemMaster.DaAssignedSystemOwner(user2oneapi_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetUserEdit")]
        [HttpGet]
        public HttpResponseMessage GetUserEdit(string user2oneapi_gid)
        {
            userpurpose objmaster = new userpurpose();
            objDaSystemMaster.DaGetUserEdit(user2oneapi_gid,objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        [ActionName("PostUserUpdate")]
        [HttpPost]
        public HttpResponseMessage PostUserUpdate(userpurpose values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostUserUpdate(getsessionvalues.user_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("InactiveUserReg")]
        [HttpPost]
        public HttpResponseMessage InactiveUserReg(userpurpose values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveUserReg(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UserRegInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage UserRegInactiveLogview(string user2oneapi_gid)
        {
            userpurpose values = new userpurpose();
            objDaSystemMaster.DaUserRegInactiveLogview(user2oneapi_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOneApiCode")]
        [HttpGet]
        public HttpResponseMessage GetOneApiCode(string user2oneapi_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            userpurpose values = new userpurpose();
            objDaSystemMaster.DaGetOneApiCode(values, user2oneapi_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PasswordUpdate")]
        [HttpPost]
        public HttpResponseMessage PasswordUpdate(userpurpose values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPasswordUpdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("UserRegResetPwdLogview")]
        [HttpGet]
        public HttpResponseMessage UserRegResetPwdLogview(string user2oneapi_gid)
        {
           
            userpurpose values = new userpurpose();
            objDaSystemMaster.DaUserRegResetPwdLogview(user2oneapi_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("WebDeActivation")]
        [HttpPost]
        public HttpResponseMessage WebDeActivation(userpurpose values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            objDaSystemMaster.DaPostWebDeActivate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("WebActivation")]
        [HttpPost]
        public HttpResponseMessage WebActivation(userpurpose values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);

            objDaSystemMaster.DaPostWebActivate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetWebAccessActiveLog")]
        [HttpGet]
        public HttpResponseMessage GetWebAccessActiveLog(string user2oneapi_gid)
        {
            userpurpose values = new userpurpose();
            objDaSystemMaster.DaGetWebAccessActiveLog(user2oneapi_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCityList")]
        [HttpGet]
        public HttpResponseMessage GetCityList()
        {
            MdlFPOCity objapplication360 = new MdlFPOCity();
            objDaSystemMaster.DaGetCityList(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }
    }
}