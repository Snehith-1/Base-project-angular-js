using ems.masterng.DataAccess;
using ems.masterng.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.masterng.Controllers
{
    [RoutePrefix("api/MstNgProductChargesAddEdit")]
    [Authorize]
    public class MstNgProductChargesAddEditController:ApiController
    {
        DaMstNgProductChargesAddEdit objMstNgProductChargesAddEdit = new DaMstNgProductChargesAddEdit();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        //overall amount add & edit
        [ActionName("SubmitOverallLimit")]
        [HttpPost]
        public HttpResponseMessage SubmitOverallLimit(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgProductChargesAddEdit.DaSubmitOverallLimit(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Product & Charges Edit

        [ActionName("GetProductChargesEdit")]
        [HttpGet]
        public HttpResponseMessage GetProductChargesEdit(string application_gid)
        {
            MdlProductCharges values = new MdlProductCharges();
            objMstNgProductChargesAddEdit.DaGetProductChargesEdit(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //overall limit and sum of loan amount
        [ActionName("GetEditLimit")]
        [HttpGet]
        public HttpResponseMessage GetEditLimit(string application_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlMstApplicationAdd values = new MdlMstApplicationAdd();
            objMstNgProductChargesAddEdit.DaGetEditLimit(application_gid, values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Product Limit Card
        [ActionName("PostLoanDtl")]
        [HttpPost]
        public HttpResponseMessage PostLoanDtl(MdlMstLoanDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgProductChargesAddEdit.DaPostLoanDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Product Collateral Card
        [ActionName("PostLoanDtlCollateral")]
        [HttpPost]
        public HttpResponseMessage PostLoanDtlCollateral(MdlMstLoanDtl values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgProductChargesAddEdit.DaPostLoanDtlCollateral(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("PostHypothecation")]
        [HttpPost]
        public HttpResponseMessage PostHypothecation(MdlMstHypothecation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgProductChargesAddEdit.DaPostHypothecation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Facility and Charges
        [ActionName("PostServiceCharges")]
        [HttpPost]
        public HttpResponseMessage PostServiceCharges(MdlProductCharges values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objMstNgProductChargesAddEdit.DaPostServiceCharges(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
    }
}