using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

/// <summary>
/// (It's used for Application Creation View in Samfin)ApplicationView Controller Class containing API methods for accessing the related DataAccess class and returning relevant response to client. 
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash </remarks>

namespace ems.master.Controllers
{
    [RoutePrefix("api/MstApplicationView")]
    [Authorize]

    public class MstApplicationViewController : ApiController
    {
        DaMstApplicationView objMstApplicationView = new DaMstApplicationView();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetApplicationBasicView")]
        [HttpGet]
        public HttpResponseMessage GetApplicationBasicView(string application_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objMstApplicationView.DaGetApplicationBasicView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMobileMailDetailsView")]
        [HttpGet]
        public HttpResponseMessage GetMobileMailDetailsView(string application_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objMstApplicationView.DaGetMobileMailDetailsView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGeneticDetailsView")]
        [HttpGet]
        public HttpResponseMessage GetGeneticDetailsView(string application_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objMstApplicationView.DaGetGeneticDetailsView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBorrowerInstitutionView")]
        [HttpGet]
        public HttpResponseMessage GetBorrowerInstitutionView(string application_gid)
        {
            MdlMstInstitutionDtlView values = new MdlMstInstitutionDtlView();
            objMstApplicationView.DaGetBorrowerInstitutionView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVisitReportList")]
        [HttpGet]
        public HttpResponseMessage GetVisitReportList(string application_gid, string statusupdated_by)
        {

            MdlMstVisitPersonView values = new MdlMstVisitPersonView();
            objMstApplicationView.DaGetVisitReportList(application_gid, statusupdated_by, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVisitReportDtls")]
        [HttpGet]
        public HttpResponseMessage GetVisitReportDtls(string visitreport_gid)
        {

            MdlMstVisitPersonView values = new MdlMstVisitPersonView();
            objMstApplicationView.DaGetVisitReportDtls(visitreport_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetVisitContactList")]
        [HttpGet]
        public HttpResponseMessage GetVisitContactList(string applicationvisit2person_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            mstVisitpersondtl_list values = new mstVisitpersondtl_list();
            objMstApplicationView.DaGetVisitContactList(getsessionvalues.employee_gid, applicationvisit2person_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGradingToolDtls")]
        [HttpGet]
        public HttpResponseMessage GetGradingToolDtls(string application_gid, string statusupdated_by)
        {

            MdlMstGradeToolView values = new MdlMstGradeToolView();
            objMstApplicationView.DaGetGradingToolDtls(application_gid, statusupdated_by, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetBorrowerIndividualView")]
        [HttpGet]
        public HttpResponseMessage GetBorrowerIndividualView(string application_gid)
        {

            MdlMstIndividualDtlView values = new MdlMstIndividualDtlView();
            objMstApplicationView.DaGetBorrowerIndividualView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGurantorIndividualList")]
        [HttpGet]
        public HttpResponseMessage GetGurantorIndividualList(string application_gid)
        {

            MdlMstGurantorView values = new MdlMstGurantorView();
            objMstApplicationView.DaGetGurantorIndividualList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGurantorInstitutionList")]
        [HttpGet]
        public HttpResponseMessage GetGurantorInstitutionList(string application_gid)
        {

            MdlMstGurantorView values = new MdlMstGurantorView();
            objMstApplicationView.DaGetGurantorInstitutionList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGurantorInstitutionView")]
        [HttpGet]
        public HttpResponseMessage GetGurantorInstitutionView(string institution_gid)
        {

            MdlMstInstitutionDtlView values = new MdlMstInstitutionDtlView();
            objMstApplicationView.DaGetGurantorInstitutionView(institution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGurantorIndividualView")]
        [HttpGet]
        public HttpResponseMessage GetGurantorIndividualView(string contact_gid)
        {

            MdlMstIndividualDtlView values = new MdlMstIndividualDtlView();
            objMstApplicationView.DaGetGurantorIndividualView(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetProductChargesDtl")]
        [HttpGet]
        public HttpResponseMessage GetProductChargesDtl(string application_gid)
        {

            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objMstApplicationView.DaGetProductChargesDtl(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetHypoDocDtl")]
        [HttpGet]
        public HttpResponseMessage GetHypoDocDtl(string application2hypothecation_gid)
        {

            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objMstApplicationView.DaGetHypoDocDtl(application2hypothecation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetCollateralDocDtl")]
        [HttpGet]
        public HttpResponseMessage GetCollateralDocDtl(string application2loan_gid)
        {

            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objMstApplicationView.DaGetCollateralDocDtl(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetPurposeofLoan")]
        [HttpGet]
        public HttpResponseMessage GetPurposeofLoan(string application2loan_gid)
        {
            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objMstApplicationView.DaGetPurposeofLoan(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLoantoBuyerList")]
        [HttpGet]
        public HttpResponseMessage GetLoantoBuyerList(string application2loan_gid)
        {
            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objMstApplicationView.DaGetLoantoBuyerList(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetGradingView")]
        [HttpGet]
        public HttpResponseMessage GetGradingView(string application2gradingtool_gid)
        {
            MdlMstGradeToolView values = new MdlMstGradeToolView();
            objMstApplicationView.DaGetGradingView(application2gradingtool_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetGrouptoMemberList")]
        [HttpGet]
        public HttpResponseMessage GetGrouptoMemberList(string group_gid)
        {
            MdlMstGroupMember values = new MdlMstGroupMember();
            objMstApplicationView.DaGetGrouptoMemberList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPrimaryAndOtherMobileNumbers")]
        [HttpGet]
        public HttpResponseMessage GetPrimaryAndOtherMobileNoList(string contact_gid)
        {
            MdlMstContactMobileNumbers values = new MdlMstContactMobileNumbers();
            objMstApplicationView.DaGetPrimaryAndOtherMobileNumbers(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetPrimaryAndOtherEmails")]
        [HttpGet]
        public HttpResponseMessage GetPrimaryAndOtherEmails(string contact_gid)
        {
            MdlMstContactEmails values = new MdlMstContactEmails();
            objMstApplicationView.DaGetGetPrimaryAndOtherEmails(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualBureauDtls")]
        [HttpGet]
        public HttpResponseMessage GetIndividualBureauDtls(string contact_gid)
        {

            MdlMstContactBureau values = new MdlMstContactBureau();
            objMstApplicationView.DaGetIndividualBureauDtls(contact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetIndividualList")]
        [HttpGet]
        public HttpResponseMessage GetIndividualList(string application_gid)
        {
            MdlCreditView values = new MdlCreditView();
            objMstApplicationView.DaGetIndividualList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetInstitutionList")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionList(string application_gid)
        {
            MdlCreditView values = new MdlCreditView();
            objMstApplicationView.DaGetInstitutionList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetRMDetailsView")]
        [HttpGet]
        public HttpResponseMessage GetRMDetailsView(string application_gid)
        {
            MdlRMDtlView values = new MdlRMDtlView();
            objMstApplicationView.DaGetRMDetailsView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetLoanProgramValueChain")]
        [HttpGet]
        public HttpResponseMessage GetLoanProgramValueChain(string application2loan_gid)
        {
            MdlMstProductChargesView values = new MdlMstProductChargesView();
            objMstApplicationView.DaGetLoanProgramValueChain(application2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetIndividualImportLog")]
        [HttpGet]
        public HttpResponseMessage GetIndividualImportLog(string application_gid)
        {
            MdlExcelImportApplication values = new MdlExcelImportApplication();
            objMstApplicationView.DaGetIndividualImportLog(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetInstitutionImportLog")]
        [HttpGet]
        public HttpResponseMessage GetInstitutionImportLog(string application_gid)
        {
            MdlExcelImportApplication values = new MdlExcelImportApplication();
            objMstApplicationView.DaGetInstitutionImportLog(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetGroupImportLog")]
        [HttpGet]
        public HttpResponseMessage GetGroupImportLog(string application_gid)
        {
            MdlExcelImportApplication values = new MdlExcelImportApplication();
            objMstApplicationView.DaGetGroupImportLog(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Loan Details View
        [ActionName("GetLoanDetailsView")]
        [HttpGet]
        public HttpResponseMessage GetLoanDetailsView(string application_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objMstApplicationView.DaGetLoanDetailsView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Penality View
        [ActionName("GetPenaltyView")]
        [HttpGet]
        public HttpResponseMessage GetPenaltyView(string application_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objMstApplicationView.DaGetPenaltyView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //TDS View
        [ActionName("GetTDSView")]
        [HttpGet]
        public HttpResponseMessage GetTDSView(string application_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objMstApplicationView.DaGetTDSView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //NDC View
        [ActionName("GetNDCView")]
        [HttpGet]
        public HttpResponseMessage GetNDCView(string application_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objMstApplicationView.DaGetNDCView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Moratorium View
        [ActionName("GetNOCView")]
        [HttpGet]
        public HttpResponseMessage GetNOCView(string application_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objMstApplicationView.DaGetNOCView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Penality View
        [ActionName("GetMoratoriumView")]
        [HttpGet]
        public HttpResponseMessage GetMoratoriumView(string application_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objMstApplicationView.DaGetMoratoriumView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        // Bank Account Details List
        [ActionName("BankAccountDetailsList")]
        [HttpGet]
        public HttpResponseMessage BankAccountDetailsList(string application_gid)
        {
            MdlMstBankAccountDetails values = new MdlMstBankAccountDetails();
            objMstApplicationView.DaBankAccountDetailsList(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
         //Deviation View
        [ActionName("GetDeviationView")]
        [HttpGet]
        public HttpResponseMessage GetDeviationView(string application_gid)
        {
            MdlMstApplicationView values = new MdlMstApplicationView();
            objMstApplicationView.DaGetDeviationView(application_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

       
    }
}
