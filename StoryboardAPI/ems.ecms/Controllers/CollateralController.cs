using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.ecms.Models;
using ems.ecms.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.ecms.Controllers
{
    /// <summary>
    /// collateral Controller Class containing API methods for accessing the  DataAccess class DaCustomerCollateral 
    /// To create collateral details, delete collateral, get collateral details and update collateral details
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    [RoutePrefix("api/collateral")]
    [Authorize]
    public class CollateralController : ApiController
    {
        DaCustomerCollateral objDaCollateral = new DaCustomerCollateral();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        [ActionName("createCollateral")]
        [HttpPost]
        public HttpResponseMessage CreateCollateral(customercollateral values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCollateral.DaPostcreateCollateral( values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getCollateralSummary")]
        [HttpGet]
        public HttpResponseMessage GetCollateral()
        {
            Mdlcollateral objMdlcollateral = new Mdlcollateral();
             objDaCollateral.DaGetCollateral(objMdlcollateral );
            return Request.CreateResponse(HttpStatusCode.OK, objMdlcollateral);
        }
        [ActionName("collateralDelete")]
        [HttpGet]
        public HttpResponseMessage CollateralDelete(string collateral_gid)
        {
            customercollateral values = new customercollateral();
            objDaCollateral.DaPostDeleteCollateral(collateral_gid,values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getCollateralDetails")]
        [HttpGet]
        public HttpResponseMessage GetCollateralDetails(string collateral_gid)
        {
            customercollateral objMdlCollateral = new customercollateral();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaCollateral.DaGetCollateralDetails(collateral_gid, objMdlCollateral);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlCollateral);
        }

        [ActionName("UpdateCollateral")]
        [HttpPost]
        public HttpResponseMessage UpdateCollateral(customercollateral values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
             objDaCollateral.DaPostUpdateCollateralDetails(values, getsessionvalues.user_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}
