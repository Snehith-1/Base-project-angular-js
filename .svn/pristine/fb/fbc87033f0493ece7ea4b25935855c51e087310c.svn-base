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
    /// loan Controller Class containing API methods for accessing the  DataAccess class DaLoan
    ///     Loan  - Create loan, sanction loan, loan details, loan details summary, 
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    [RoutePrefix("api/loan")]
    [Authorize]
    public class LoanController : ApiController
    {

        DaLoan ObjDaLoan = new DaLoan();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("loanCreate")]
        [HttpPost]
        public HttpResponseMessage LoanFacility(createLoan values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
           ObjDaLoan.DaPostLoanFacility(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("SanctionLoanFacility")]
        [HttpGet]
        public HttpResponseMessage SanctionLoanFacility(string customer2sanction_gid)
        {
            loanfaciity_list objResult = new loanfaciity_list();
            ObjDaLoan.DaGetSantionFacilityType(customer2sanction_gid, objResult);
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }
        [ActionName("createLoan")]
        [HttpPost]
        public HttpResponseMessage CreateLoan(createLoan values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            ObjDaLoan.DaPostCreateLoan(values,getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("loanSummary")]
        [HttpGet]
        public HttpResponseMessage GetLoanSummary()
        {
            loan objMdlLoan = new loan();
           ObjDaLoan.DaGetLoanSummary(objMdlLoan);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlLoan);
        }

        [ActionName("loan_list")]
        [HttpGet]
        public HttpResponseMessage LoanList()
        {
            loanmaster objMdlLoan = new loanmaster();
            ObjDaLoan.DaGetLoanList(objMdlLoan); 
            return Request.CreateResponse(HttpStatusCode.OK, objMdlLoan);
        }


        [ActionName("getLoanmasterSummary")]
        [HttpGet]
        public HttpResponseMessage GetLoanmasterSummary()
        {
            loan objvendordtl = new loan();
            ObjDaLoan.DaGetLoan(objvendordtl);
            return Request.CreateResponse(HttpStatusCode.OK, objvendordtl);
        }

        [ActionName("loan")]
        [HttpGet]
        public HttpResponseMessage Customer2loan()
        {
            loandetails objMdlloan = new loandetails();
            ObjDaLoan.DaGetLoan(objMdlloan);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlloan);
        }

        [ActionName("customer_getheads")]
        [HttpGet]
        public HttpResponseMessage Customer_getheads(string customer_gid)
        {
            mdlheadsofcustomer values = new mdlheadsofcustomer();
            ObjDaLoan. DaGetCustomerHeads(values, customer_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getLoandetails")]
        [HttpGet]
        public HttpResponseMessage GetLoandetails(string loan_gid)
        {
            LoanDetails values = new LoanDetails();
            ObjDaLoan.DaGetLoanDetails(values, loan_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("getLoandeferraldetails")]
        [HttpGet]
        public HttpResponseMessage GetLoandeferraldetails(string loan_gid)
        {
            LoanDetails values = new LoanDetails();
            ObjDaLoan.DaGetLoanDeferraldetails(values, loan_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("getdeferralcriticallity")]
        [HttpGet]
        public HttpResponseMessage Getcriticallity(string deferral)
        {
            MDLcriticallity objMdlValues = new MDLcriticallity();
            ObjDaLoan.DaGetCriticallity(objMdlValues, deferral);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlValues);
        }

        [ActionName("getcovenanttypecriticallity")]
        [HttpGet]
        public HttpResponseMessage Getcriticallitycov(string covenanttype)
        {
            MDLcriticallity values = new MDLcriticallity();
            ObjDaLoan.DaGetCriticallityCov(values, covenanttype);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("loanUpdate")]
        [HttpPost]
        public HttpResponseMessage LoanUpdate(loanedit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            ObjDaLoan.DaPostUpdateLoan(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("editloanmaster")]
        [HttpGet]
        public HttpResponseMessage Editloanmaster(string loanmaster_gid)
        {
            loanedit objMdlloan = new loanedit();
           ObjDaLoan.DaGetEditLoan(loanmaster_gid, objMdlloan);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlloan);
        }

        [ActionName("deleteloanmaster")]
        [HttpGet]
        public HttpResponseMessage DeleteloanMaster(string loanmaster_gid)
        {
            loanedit values = new loanedit();
            ObjDaLoan.DaGetDeleteLoan(loanmaster_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("updateloanmaster")]
        [HttpPost]
        public HttpResponseMessage UpdateloanFacility(loanedit values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            ObjDaLoan.DaPostupdateloanFacility(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetSanctionDate")]
        [HttpGet]
        public HttpResponseMessage GetSanctionDate(string sanction_gid)
        {
            sanctiondtl values = new sanctiondtl();
            ObjDaLoan.DaGetSanctionDate(values, sanction_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("LoanRefUpdate")]
        [HttpPost]
        public HttpResponseMessage PostLoanRefUpdate(NewloanRef  objResult)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            ObjDaLoan.DaPostNewLoanRef (objResult,getsessionvalues .employee_gid );
            return Request.CreateResponse(HttpStatusCode.OK, objResult);
        }


    }
}
