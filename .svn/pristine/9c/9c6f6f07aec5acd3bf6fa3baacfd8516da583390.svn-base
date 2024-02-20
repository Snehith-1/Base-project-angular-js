using ems.master.DataAccess;
using ems.master.Models;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ems.master.Controllers
{
    [RoutePrefix("api/OpsApplicationView")]
    [Authorize]

    public class OpsApplicationViewController : ApiController
    {
        DaOpsApplicationView objOpsApplicationView = new DaOpsApplicationView();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("GetOPSApplicationBasicView")]
        [HttpGet]
        public HttpResponseMessage GetOPSApplicationBasicView(string opsapplication_gid)
        {
            MdlOpsApplicationView values = new MdlOpsApplicationView();
            objOpsApplicationView.DaGetOPSApplicationBasicView(opsapplication_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOPSMobileMailDetailsView")]
        [HttpGet]
        public HttpResponseMessage GetOPSMobileMailDetailsView(string opsapplication_gid)
        {
            MdlOpsApplicationView values = new MdlOpsApplicationView();
            objOpsApplicationView.DaGetOPSMobileMailDetailsView(opsapplication_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOPSGeneticDetailsView")]
        [HttpGet]
        public HttpResponseMessage GetOPSGeneticDetailsView(string opsapplication_gid)
        {
            MdlOpsApplicationView values = new MdlOpsApplicationView();
            objOpsApplicationView.DaGetOPSGeneticDetailsView(opsapplication_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOPSInstitutionList")]
        [HttpGet]
        public HttpResponseMessage GetOPSInstitutionList(string opsapplication_gid)
        {
            MdlOPSCreditView values = new MdlOPSCreditView();
            objOpsApplicationView.DaGetOPSInstitutionList(opsapplication_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOPSIndividualList")]
        [HttpGet]
        public HttpResponseMessage GetOPSIndividualList(string opsapplication_gid)
        {
            MdlOPSCreditView values = new MdlOPSCreditView();
            objOpsApplicationView.DaGetOPSIndividualList(opsapplication_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOPSRMDetailsView")]
        [HttpGet]
        public HttpResponseMessage GetOPSRMDetailsView(string opsapplication_gid)
        {
            MdlOPSRMDtlView values = new MdlOPSRMDtlView();
            objOpsApplicationView.DaGetOPSRMDetailsView(opsapplication_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOPSGurantorInstitutionView")]
        [HttpGet]
        public HttpResponseMessage GetOPSGurantorInstitutionView(string opsinstitution_gid)
        {
            MdlOPSInstitutionDtlView values = new MdlOPSInstitutionDtlView();
            objOpsApplicationView.DaGetOPSGurantorInstitutionView(opsinstitution_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOPSGurantorIndividualView")]
        [HttpGet]
        public HttpResponseMessage GetOPSGurantorIndividualView(string opscontact_gid)
        {

            MdlOPSIndividualDtlView values = new MdlOPSIndividualDtlView();
            objOpsApplicationView.DaGetOPSGurantorIndividualView(opscontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOPSProductChargesDtl")]
        [HttpGet]
        public HttpResponseMessage GetOPSProductChargesDtl(string opsapplication_gid)
        {

            MdlOPSProductChargesView values = new MdlOPSProductChargesView();
            objOpsApplicationView.DaGetOPSProductChargesDtl(opsapplication_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOPSHypoDocDtl")]
        [HttpGet]
        public HttpResponseMessage GetOPSHypoDocDtl(string opsapplication2hypothecation_gid)
        {

            MdlOPSProductChargesView values = new MdlOPSProductChargesView();
            objOpsApplicationView.DaGetOPSHypoDocDtl(opsapplication2hypothecation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOPSCollateralDocDtl")]
        [HttpGet]
        public HttpResponseMessage GetOPSCollateralDocDtl(string opsapplication2loan_gid)
        {

            MdlOPSProductChargesView values = new MdlOPSProductChargesView();
            objOpsApplicationView.DaGetOPSCollateralDocDtl(opsapplication2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOPSPurposeofLoan")]
        [HttpGet]
        public HttpResponseMessage GetOPSPurposeofLoan(string opsapplication2loan_gid)
        {
            MdlOPSProductChargesView values = new MdlOPSProductChargesView();
            objOpsApplicationView.DaGetOPSPurposeofLoan(opsapplication2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetOPSLoantoBuyerList")]
        [HttpGet]
        public HttpResponseMessage GetOPSLoantoBuyerList(string opsapplication2loan_gid)
        {
            MdlOPSProductChargesView values = new MdlOPSProductChargesView();
            objOpsApplicationView.DaGetOPSLoantoBuyerList(opsapplication2loan_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOPSGroupSummary")]
        [HttpGet]
        public HttpResponseMessage GetOPSGroupSummary(string opsapplication_gid)
        {
            MdlOPSGroup values = new MdlOPSGroup();
            objOpsApplicationView.DaGetOPSGroupSummary(opsapplication_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOPSGrouptoMemberList")]
        [HttpGet]
        public HttpResponseMessage GetOPSGrouptoMemberList(string group_gid)
        {
            MdlOPSGroupMember values = new MdlOPSGroupMember();
            objOpsApplicationView.DaGetOPSGrouptoMemberList(group_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOPSGroupView")]
        [HttpGet]
        public HttpResponseMessage GetOPSGroupView(string opsgroup_gid)
        {
            MdlOPSGroup values = new MdlOPSGroup();
            objOpsApplicationView.DaGetOPSGroupView(opsgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("OPSGroupAddressList")]
        [HttpGet]
        public HttpResponseMessage OPSGroupAddressList(string opsgroup_gid)
        {
            MdlOPSAddressDetails values = new MdlOPSAddressDetails();
            objOpsApplicationView.DaOPSGroupAddressList(opsgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("OPSGroupBankList")]
        [HttpGet]
        public HttpResponseMessage OPSGroupBankList(string opsgroup_gid)
        {
            MdOPSBankDetails values = new MdOPSBankDetails();
            objOpsApplicationView.DaOPSGroupBankList(opsgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("OPSGroupDocumentList")]
        [HttpGet]
        public HttpResponseMessage OPSGroupDocumentList(string opsgroup_gid)
        {
            OPSGroupDocument values = new OPSGroupDocument();
            objOpsApplicationView.DaOPSGroupDocumentList(opsgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOPSIndividualView")]
        [HttpGet]
        public HttpResponseMessage GetOPSIndividualView(string opscontact_gid)
        {
            MdlOPSContact values = new MdlOPSContact();
            objOpsApplicationView.DaGetOPSIndividualView(opscontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOPSIndividualAddressList")]
        [HttpGet]
        public HttpResponseMessage GetOPSIndividualAddressList(string opscontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlOPSContactAddress values = new MdlOPSContactAddress();
            objOpsApplicationView.DaGetOPSIndividualAddressList(opscontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOPSIndividualProofList")]
        [HttpGet]
        public HttpResponseMessage GetOPSIndividualProofList(string opscontact_gid)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            MdlOPSContactIdProof values = new MdlOPSContactIdProof();
            objOpsApplicationView.DaGetOPSIndividualProofList(opscontact_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOPSIndividualDocList")]
        [HttpGet]
        public HttpResponseMessage GetOPSIndividualDocList(string opscontact_gid)
        {
            MdlOPSContactIdProof values = new MdlOPSContactIdProof();
            objOpsApplicationView.DaGetOPSIndividualDocList(opscontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOPSPrimaryAndOtherMobileNumber")]
        [HttpGet]
        public HttpResponseMessage GetOPSPrimaryAndOtherMobileNumber(string opscontact_gid)
        {
            MdlOPSContactMobileNumber values = new MdlOPSContactMobileNumber();
            objOpsApplicationView.DaGetOPSPrimaryAndOtherMobileNumber(opscontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOPSPrimaryAndOtherEmail")]
        [HttpGet]
        public HttpResponseMessage GetOPSPrimaryAndOtherEmail(string opscontact_gid)
        {
            MdlOPSContactEmail values = new MdlOPSContactEmail();
            objOpsApplicationView.DaGetOPSPrimaryAndOtherEmail(opscontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetOPSIndividualBureauDtls")]
        [HttpGet]
        public HttpResponseMessage GetOPSIndividualBureauDtls(string opscontact_gid)
        {
            MdlOPSContactBureau values = new MdlOPSContactBureau();
            objOpsApplicationView.DaGetOPSIndividualBureauDtls(opscontact_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

    }
}