using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using ems.mastersamagro.DataAccess;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Net;


namespace ems.mastersamagro.Controllers
{

    /// <summary>
    /// This Controllers will provide access to export buyer and supplier approved record details.
    /// </summary>
    /// <remarks>Written by Kalaiarasan</remarks>

    [RoutePrefix("api/AgrMstOnboardApprovalReport")]
    [Authorize]

    public class AgrMstOnboardApprovalReportController : ApiController
    {
        DaAgrMstOnboardApprovalReport objDaAgrMstOnboardApprovalReport = new DaAgrMstOnboardApprovalReport();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();


        //Buyer Onboard Report Export Excel
        [ActionName("ExportBuyerOnboardApproved")]
        [HttpGet]
        public HttpResponseMessage ExportBuyerOnboardApproved()
        {
            MdlAgrMstOnboardApprovalReport objAgrMstOnboardApprovalReport = new MdlAgrMstOnboardApprovalReport();
            objDaAgrMstOnboardApprovalReport.DaGetExportBuyerOnboardApproved(objAgrMstOnboardApprovalReport);
            return Request.CreateResponse(HttpStatusCode.OK, objAgrMstOnboardApprovalReport);
        }

        //Supplier Onboard Report Export Excel
        [ActionName("ExportSupplierOnboardApproved")]
        [HttpGet]
        public HttpResponseMessage ExportSupplierOnboardApproved()
        {
            MdlAgrMstOnboardApprovalReport objAgrMstOnboardApprovalReport = new MdlAgrMstOnboardApprovalReport();
            objDaAgrMstOnboardApprovalReport.DaGetExportSupplierOnboardApproved(objAgrMstOnboardApprovalReport);
            return Request.CreateResponse(HttpStatusCode.OK, objAgrMstOnboardApprovalReport);
        }


    }
}