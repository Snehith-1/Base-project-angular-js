using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Web;
using ems.osd.Models;
using ems.osd.DataAccess;

namespace ems.osd.Controllers
{
    [RoutePrefix("api/OsdMstDepartmentManagement")]
    [Authorize]
    public class OsdMstDepartmentManagementController : ApiController
    {
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        DaOsdMstDepartmentManagement objDaOsdMstActivatedept = new DaOsdMstDepartmentManagement();

        [ActionName("GetActivatedeptSummary")]
        [HttpGet]
        public HttpResponseMessage GetActivatedeptSummary()
        {
            activatedeptlist values = new activatedeptlist();
            objDaOsdMstActivatedept.DaGetActivatedeptSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetActivatedept")]
        [HttpGet]
        public HttpResponseMessage GetActivatedept()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            activatedeptlist values = new activatedeptlist();
            objDaOsdMstActivatedept.DaGetActivatedept(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetActivaterequestdept")]
        [HttpGet]
        public HttpResponseMessage GetActivaterequestdept()
        {
            activatedeptlist values = new activatedeptlist();
            objDaOsdMstActivatedept.DaGetActivaterequestdept(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("PostActivatedeptadd")]
        [HttpPost]
        public HttpResponseMessage PostActivatedeptadd(acivatedept values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstActivatedept.DaPostActivatedeptAdd(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetActivatedeptUpdate")]
        [HttpPost]
        public HttpResponseMessage GetActivityUpdate(acivatedept values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstActivatedept.DaGetActivatedeptUpdate(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetActivatedeptDelete")]
        [HttpGet]
        public HttpResponseMessage GetActivityDelete(string activedepartment_gid)
        {
            result values = new result();
            objDaOsdMstActivatedept.DaGetActivatedeptDelete(activedepartment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetActivatedeptView")]
        [HttpGet]
        public HttpResponseMessage GetActivityView(string activedepartment_gid)
        {
            acivatedept values = new acivatedept();
            objDaOsdMstActivatedept.DaGetActivatedeptView(activedepartment_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("Postdepartmentstatusupdate")]
        [HttpPost]
        public HttpResponseMessage Postdepartmentstatusupdate(acivatedept values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstActivatedept.DaPostdepartmentstatusupdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("DepartmentstatusHistory")]
        [HttpGet]
        public HttpResponseMessage DepartmentstatusHistory(string activedepartment_gid)
        {
            departmentstatusHistory values = new departmentstatusHistory();
            objDaOsdMstActivatedept.DaDepartmentstatusHistory(values, activedepartment_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


  
        [ActionName("Employee")]
        [HttpGet]
        public HttpResponseMessage Employee(string activedepartment_gid)
        {
            MdlEmployeeassign objMdlEmployee = new MdlEmployeeassign();
            objDaOsdMstActivatedept.DaGetEmployee(objMdlEmployee, activedepartment_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }

        [ActionName("MemberEmployee")]
        [HttpGet]
        public HttpResponseMessage MemberEmployee(string activedepartment_gid)
        {
            MdlEmployeeassign objMdlEmployee = new MdlEmployeeassign();
            objDaOsdMstActivatedept.DaMemberEmployee(objMdlEmployee, activedepartment_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }

        [ActionName("GetDeptEmployee")]
        [HttpGet]
        public HttpResponseMessage GetDeptEmployee(string department_gid)
        {
            MdlEmployeeassign objMdlEmployee = new MdlEmployeeassign();
            objDaOsdMstActivatedept.DaGetDeptEmployee(objMdlEmployee, department_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }

        [ActionName("Assignedemplyee")]
        [HttpGet]
        public HttpResponseMessage Assignedemplyee(string activedepartment_gid)
        {
            MdlEmployeeassign objMdlEmployee = new MdlEmployeeassign();
            objDaOsdMstActivatedept.DaAssignedemplyee(objMdlEmployee, activedepartment_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }
        

        [ActionName("Assignedmemberemplyee")]
        [HttpGet]
        public HttpResponseMessage Assignedmemberemplyee(string activedepartment_gid)
        {
            MdlEmployeeassign objMdlEmployee = new MdlEmployeeassign();
            objDaOsdMstActivatedept.DaAssignedmemberemplyee(objMdlEmployee, activedepartment_gid);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }

        [ActionName("AssignDeptmanager")]
        [HttpPost]
        public HttpResponseMessage AssignDeptmanager(Mdlassignmanager values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstActivatedept.DaPostAssignDeptmanager(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("AssignDeptmember")]
        [HttpPost]
        public HttpResponseMessage AssignDeptmember(Mdlassignmanager values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstActivatedept.DaPostAssignDeptmember(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAssignmangerDelete")]
        [HttpGet]
        public HttpResponseMessage GetAssignmangerDelete(string activedepartment2manager_gid)
        {
            result values = new result();
            objDaOsdMstActivatedept.DaGetAssignmangerDelete(activedepartment2manager_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetAssignmemberDelete")]
        [HttpGet]
        public HttpResponseMessage GetAssignmemberDelete(string activedepartment2member_gid)
        {
            result values = new result();
            objDaOsdMstActivatedept.DaGetAssignmemberDelete(activedepartment2member_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetDepartment")]
        [HttpGet]
        public HttpResponseMessage GetPopDepartment()
        {
            mdldepartment values = new mdldepartment();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstActivatedept.DaGetDepartment(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDepartmentEdit")]
        [HttpGet]
        public HttpResponseMessage GetDepartmentEdit(string activedepartment_gid)
        {
            mdldepartment values = new mdldepartment();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstActivatedept.DaGetDepartmentEdit(values, activedepartment_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetDeptActivityList")]
        [HttpGet]
        public HttpResponseMessage GetDeptActivityList(string department_gid)
        {
            actvitydtllist values = new actvitydtllist();
            objDaOsdMstActivatedept.DaGetDeptActivityList(values, department_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Business Unit Summary
        [ActionName("GetBusinessUnitSummary")]
        [HttpGet]
        public HttpResponseMessage GetBusinessUnitSummary()
        {
            activatedeptlist values = new activatedeptlist();
            objDaOsdMstActivatedept.DaGetBusinessUnitSummary(values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        // Business Unit Status Summary
        //[ActionName("GetBusinessstatusList")]
        //[HttpGet]
        //public HttpResponseMessage GetBusinessstatusList(string user_gid)
        //{
        //    activatedeptlist values = new activatedeptlist();
        //    objDaOsdMstActivatedept.DaGetBusinessstatusList(values, employee_gid);
        //    return Request.CreateResponse(HttpStatusCode.OK, values);
        //}
        [ActionName("GetBusinessstatusList")]
        [HttpGet]
        public HttpResponseMessage GetBusinessstatusList()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            activatedeptlist values = new activatedeptlist();
            objDaOsdMstActivatedept.DaGetBusinessstatusList(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetBusinessStatusTempClear")]
        [HttpGet]
        public HttpResponseMessage GetBusinessStatusTempClear()
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            activatedeptlist values = new activatedeptlist();
            objDaOsdMstActivatedept.DaGetBusinessStatusTempClear(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Add Business Unit
        [ActionName("PostBusinessUnit")]
        [HttpPost]
        public HttpResponseMessage PostBusinessUnit(businessunit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstActivatedept.DaPostBusinessUnit(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Add Business Unit Status
        [ActionName("PostBusinessUnitStatus")]
        [HttpPost]
        public HttpResponseMessage PostBusinessUnitStatus(businessunit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstActivatedept.DaPostBusinessUnitStatus(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostBusinessUnitStatusEdit")]
        [HttpPost]
        public HttpResponseMessage PostBusinessUnitStatusEdit(businessunit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstActivatedept.DaPostBusinessUnitStatusEdit(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // View Business Unit
        [ActionName("BusinessUnitView")]
        [HttpGet]
        public HttpResponseMessage BusinessUnitView(string businessunit_gid)
        {
            businessunit_list values = new businessunit_list();
            objDaOsdMstActivatedept.DaBusinessUnitView(businessunit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBusinessstatusEdit")]
        [HttpGet]
        public HttpResponseMessage GetBusinessstatusEdit(string businessunit_gid)
        {
            activatedeptlist values = new activatedeptlist();
            objDaOsdMstActivatedept.DaGetBusinessstatusEdit(businessunit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Update Business Unit
        [ActionName("UpdateBusinessUnit")]
        [HttpPost]
        public HttpResponseMessage UpdateBusinessUnit(businessunit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstActivatedept.DaUpdateBusinessUnit(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Delete Business Unit
        [ActionName("DeleteBusinessUnit")]
        [HttpGet]
        public HttpResponseMessage DeleteBusinessUnit(string businessunit_gid)
        {
            result values = new result();
            objDaOsdMstActivatedept.DaDeleteBusinessUnit(businessunit_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBusinessstatus")]
        [HttpGet]
        public HttpResponseMessage DeleteBusinessstatus(string businessstatus_gid)
        {
            result values = new result();
            objDaOsdMstActivatedept.DaDeleteBusinessstatus(businessstatus_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        } 
        [ActionName("Postbusinessunitstatusupdate")]
        [HttpPost]
        public HttpResponseMessage Postbusinessunitstatusupdate(businessunit_list values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaOsdMstActivatedept.DaPostbusinessunitstatusupdate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BusinessunitstatusHistory")]
        [HttpGet]
        public HttpResponseMessage BusinessunitstatusHistory(string businessunit_gid)
        {
            businessunitstatusHistory values = new businessunitstatusHistory();
            objDaOsdMstActivatedept.DaBusinessunitstatusHistory(values, businessunit_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}