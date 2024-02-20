using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.master.Models;
using ems.master.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;
namespace ems.master.Controllers

/// <summary>
/// (It's used for pages in CAD Group Master page)CADGroupAssignment Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>
{
    [RoutePrefix("api/MstCADGroupAssignment")]
    [Authorize]
    public class MstCADGroupAssignmentController : ApiController
    {
        DaCADGroupAssignment objDaCADGroupAssignment = new DaCADGroupAssignment();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        //Summary
        [ActionName("GetCADGroupAssignmentSummary")]
        [HttpGet]
        public HttpResponseMessage GetCADGroupAssignmentSummary()
        {
            MdlMstCADGroupAssignment objmaster = new MdlMstCADGroupAssignment();
            objDaCADGroupAssignment.DaGetCADGroupAssignmentSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        //Menu
        [ActionName("GetMenu")]
        [HttpGet]
        public HttpResponseMessage DaGetMenu()
        {
            MdlMstCADGroupAssignment objmenu = new MdlMstCADGroupAssignment();
            objDaCADGroupAssignment.DaGetMenu(objmenu);
            return Request.CreateResponse(HttpStatusCode.OK, objmenu);
        }
        //Add
        [ActionName("PostCADGroupAssign")]
        [HttpPost]
        public HttpResponseMessage PostCADGroupAssign(MdlMstCADGroupAssignment values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADGroupAssignment.DaPostCADGroupAssign(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Edit
        [ActionName("GetCADGroupAssignmentEdit")]
        [HttpGet]
        public HttpResponseMessage GetCADGroupAssignmentEdit(string cadgroupassign_gid)
        {
            MdlMstCADGroupAssignment objmaster = new MdlMstCADGroupAssignment();
            objDaCADGroupAssignment.DaGetCADGroupAssignmentEdit(cadgroupassign_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        //Popup
        [ActionName("GetCADGroupMaker")]
        [HttpGet]
        public HttpResponseMessage GetCADGroupMaker(string cadgroupassign_gid)
        {
            Cadmaker values = new Cadmaker();
            objDaCADGroupAssignment.DaGetCADGroupMaker(cadgroupassign_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADGroupChecker")]
        [HttpGet]
        public HttpResponseMessage GetCADGroupChecker(string cadgroupassign_gid)
        {
            Cadchecker values = new Cadchecker();
            objDaCADGroupAssignment.DaGetCADGroupChecker(cadgroupassign_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetCADGroupApprover")]
        [HttpGet]
        public HttpResponseMessage GetCADGroupApprover(string cadgroupassign_gid)
        {
            cadapprover values = new cadapprover();
            objDaCADGroupAssignment.DaGetCADGroupApprover(cadgroupassign_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Update
        [ActionName("CADGroupAssignedUpdate")]
        [HttpPost]
        public HttpResponseMessage CADGroupAssignedUpdate(MdlMstCADGroupAssignment values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADGroupAssignment.DaCADGroupAssignedUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //Delete
        [ActionName("DeleteCADGroupAssignment")]
        [HttpGet]
        public HttpResponseMessage DeleteCADGroupAssignment(string cadgroupassign_gid)
        {
            MdlMstCADGroupAssignment values = new MdlMstCADGroupAssignment();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCADGroupAssignment.DaDeleteCADGroupAssignment(cadgroupassign_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        
    }
}