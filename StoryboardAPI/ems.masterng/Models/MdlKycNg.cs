using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.masterng.Models
{
    public class MdlKycNg
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    public class Common
    {
        public string consent { get; set; }
    }
    public class PanNumberModel : Common
    {
        public string pan { get; set; }
        public string dob { get; set; }
    }

    public class PanNumberResponse : MdlKycNg
    {
        public Result result { get; set; }
        public string request_id { get; set; }

        [JsonProperty("status-code")]
        public string statusCode { get; set; }

        public string error { get; set; }

    }

    public class Result
    {
        public string name { get; set; }
    }

    public class CINFromPANResponse : MdlKycNg
    {
        public CINFromPANResult[] result { get; set; }
        public string requestId { get; set; }
        public int statusCode { get; set; }

        public string error { get; set; }

    }

    public class CINFromPANResult
    {
        public string name { get; set; }
        public string entityId { get; set; }
    }

    public static class ErrorResponseKyc
    {
        public const string
            errorResponse = "Error Response obtained - ";

    }

    public class DirectorsListResponse
    {
        public ResultDirectorsList[] result { get; set; }
        public string request_id { get; set; }

        [JsonProperty("status-code")]
        public string statuscode { get; set; }
        public Clientdata clientData { get; set; }
    }

    public class Clientdata
    {
        public string caseId { get; set; }
    }

    public class ResultDirectorsList
    {
        public string date_of_appointment { get; set; }
        public string designation { get; set; }
        public string dsc_expiry_date { get; set; }
        public string wheather_dsc_registered { get; set; }

        [JsonProperty("DIN/DPIN/PAN")]
        public string DINDPINPAN { get; set; }
        public string full_name { get; set; }
        public string address { get; set; }
    }


    public class DirectorDetailResponse
    {
        public string requestId { get; set; }
        public ResultDirectorDetail[] result { get; set; }
        public int statusCode { get; set; }
    }

    public class ResultDirectorDetail
    {
        public string dinPan_ { get; set; }
        public string[] additionalAddresses { get; set; }
        public string[] additionalContacts { get; set; }
        public object[] additionalDins_ { get; set; }
        public string address { get; set; }
        public string addressType { get; set; }
        public object areaOfOccupation { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string din { get; set; }
        public DateTime dinDateOfApproval { get; set; }
        public object dinDisqualificationBeginDate { get; set; }
        public object dinDisqualificationEndDate { get; set; }
        public string dinStatus { get; set; }
        public object[] disqualifications { get; set; }
        public object district { get; set; }
        public DateTime dscExpiryDate { get; set; }
        public string email { get; set; }
        public string emailDomain { get; set; }
        public Entity[] entities { get; set; }
        public string fatherName { get; set; }
        public string fathersFirstName { get; set; }
        public string fathersLastName { get; set; }
        public string fathersMiddleName { get; set; }
        public string firstName { get; set; }
        public Formerentity[] formerEntities { get; set; }
        public string gender { get; set; }
        public object isAddressQualityPoor_ { get; set; }
        public bool isCitizenOfIndia { get; set; }
        public object isDinDeactivated { get; set; }
        public object isDinDisabled { get; set; }
        public object isDinDisqualified { get; set; }
        public object isDinLapsed { get; set; }
        public bool isDinSurrendered { get; set; }
        public bool isDscRegistered { get; set; }
        public bool isFormerPep { get; set; }
        public object isMcaDirectorDefaulter { get; set; }
        public bool isPep { get; set; }
        public object isProclaimedOffender { get; set; }
        public bool isResidentOfIndia { get; set; }
        public string kid { get; set; }
        public string lastName { get; set; }
        public object locality { get; set; }
        public object[] mcaDirectorDefaults { get; set; }
        public object membershipNumber { get; set; }
        public string middleName { get; set; }
        public string name { get; set; }
        public string nationality { get; set; }
        public string occupation { get; set; }
        public object occupationType { get; set; }
        public string oidflag { get; set; }
        public object opcflag { get; set; }
        public object otherOccupation { get; set; }
        public string[] pans { get; set; }
        public string pincode { get; set; }
        public object placeOfBirth { get; set; }
        public object[] proclaimedOffenses { get; set; }
        public object qualification { get; set; }
        public object sourceUri { get; set; }
        public string state { get; set; }
        public object[] surrenderedDins { get; set; }
        public string timestamp { get; set; }
    }

    public class Entity
    {
        public string entityId { get; set; }
        public DateTime fromDate { get; set; }
        public object toDate { get; set; }
        public DateTime dateOfAppointment { get; set; }
        public string entityName { get; set; }
    }

    public class Formerentity
    {
        public string entityId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public DateTime? dateOfAppointment { get; set; }
        public string entityName { get; set; }
    }

    public class DirectorDetailRequest
    {
        public string id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string fatherName { get; set; }
        public string dateOfInception { get; set; }
    }

    public class DirectorDetailForIndividual
    {
        public string entityName { get; set; }
        public string fathersFirstName { get; set; }
        public string fathersMiddleName { get; set; }
        public string fathersLastName { get; set; }
        public string gender_gid { get; set; }
        public string gender_name { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string pan { get; set; }
        public string dateOfBirth { get; set; }

    }

    public class GSTSBPANDetailsResponse : MdlKycNg
    {
        public string requestId { get; set; }
        public ResultGSTSBPAN[] result { get; set; }
        public int statusCode { get; set; }
        public string error { get; set; }
    }

    public class ResultGSTSBPAN
    {
        public string authStatus { get; set; }
        public string applicationStatus { get; set; }
        public string emailId { get; set; }
        public string gstinId { get; set; }
        public string gstinRefId { get; set; }
        public string mobNum { get; set; }
        public string pan { get; set; }
        public string regType { get; set; }
        public string registrationName { get; set; }
        public string tinNumber { get; set; }
    }

    public class MdlAddressPINDetails
    {
        public string city { get; set; }
        public string taluka { get; set; }
        public string district { get; set; }
        public string state { get; set; }

    }

    public class GSTVerificationResponse : MdlKycNg
    {
        public string requestId { get; set; }
        public ResultGSTVerification result { get; set; }
        public int statusCode { get; set; }
        public string error { get; set; }
    }

    public class ResultGSTVerification
    {
        public string stjCd { get; set; }
        public string dty { get; set; }
        public string lgnm { get; set; }
        public string stj { get; set; }
        public Adadr[] adadr { get; set; }
        public string cxdt { get; set; }
        public string gstin { get; set; }
        public string[] nba { get; set; }
        public string lstupdt { get; set; }
        public string ctb { get; set; }
        public string rgdt { get; set; }
        public Pradr pradr { get; set; }
        public string ctjCd { get; set; }
        public string sts { get; set; }
        public string tradeNam { get; set; }
        public string ctj { get; set; }
        public object[] mbr { get; set; }
        public string canFlag { get; set; }
        public string cmpRt { get; set; }
        public Contacted contacted { get; set; }
        public string ppr { get; set; }
    }

    public class Pradr
    {
        public string addr { get; set; }
        public string ntr { get; set; }
        public string adr { get; set; }
        public string em { get; set; }
        public string lastUpdatedDate { get; set; }
        public string mb { get; set; }
    }

    public class Contacted
    {
        public object email { get; set; }
        public object mobNum { get; set; }
        public object name { get; set; }
    }

    public class Adadr
    {
        public string addr { get; set; }
        public string ntr { get; set; }
        public string adr { get; set; }
        public string em { get; set; }
        public string lastUpdatedDate { get; set; }
        public string mb { get; set; }
    }

    public class MdlMstGST: result
    {
        public string institution2branch_gid { get; set; }
        public string institution_gid { get; set; }
        public string gststate_gid { get; set; }
        public string gst_state { get; set; }
        public string gst_no { get; set; }
        public string gst_registered { get; set; }
        public string gstsource { get; set; }
        public string apifetch_flag { get; set; }
        public List<mstgst_list> mstgst_list { get; set; }
        public InstitutionGSTDetails[] GSTArray { get; set; }
        public string opsinstitution2branch_gid { get; set; }
        public string opsinstitution_gid { get; set; }
        public string statusupdated_by { get; set; }
        public string headoffice_status { get; set; }
    }

    public class InstitutionGSTDetails
    {
        public string authStatus { get; set; }
        public string applicationStatus { get; set; }
        public string emailId { get; set; }
        public string gstinId { get; set; }
        public string gstinRefId { get; set; }
        public string mobNum { get; set; }
        public string pan { get; set; }
        public string regType { get; set; }
        public string registrationName { get; set; }
        public string tinNumber { get; set; }
    }


    public class LEINumberResponse : MdlKycNg
    {
        public string requestId { get; set; }
        public ResultLEI[] result { get; set; }
        public int statusCode { get; set; }
        public string error { get; set; }
    }

    public class ResultLEI
    {
        public string lei { get; set; }
        public object associatedEntityApiUri { get; set; }
        public object associatedEntityLei { get; set; }
        public object associatedEntityName { get; set; }
        public object associatedEntityRelationType { get; set; }
        public object bankIdentifierCodes { get; set; }
        public object branches { get; set; }
        public string country { get; set; }
        public string countryCode { get; set; }
        public DateTime dateOfCreation { get; set; }
        public Directchildren[] directChildren { get; set; }
        public object directChildrenApiUri { get; set; }
        public object directParent { get; set; }
        public object directParentApiUri { get; set; }
        public object directParentExceptionDetails { get; set; }
        public object directParentExceptionUri { get; set; }
        public object entityExpirationDate { get; set; }
        public object entityExpirationReason { get; set; }
        public string entityLegalForm { get; set; }
        public string entityLegalFormStatus { get; set; }
        public string entityStatus { get; set; }
        public object feederFunds { get; set; }
        public string fullHeadquartersAddress { get; set; }
        public string fullLegalAddress { get; set; }
        public object fullOtherAddresses { get; set; }
        public object fundManager { get; set; }
        public string generalCategory { get; set; }
        public object headOffice { get; set; }
        public Headquartersaddress headquartersAddress { get; set; }
        public DateTime initialRegistrationDate { get; set; }
        public bool isLeiRegistrationDuplicate { get; set; }
        public bool isLeiRegistrationLapsed { get; set; }
        public bool isLeiRegistrationRetired { get; set; }
        public string jurisdiction { get; set; }
        public object jurisdictionOfFormation { get; set; }
        public string kid { get; set; }
        public Legaladdress legalAddress { get; set; }
        public string legalFormApiUri { get; set; }
        public DateTime legalFormDateOfCreation { get; set; }
        public string legalFormDescriptionLine1 { get; set; }
        public string legalFormDescriptionLine2 { get; set; }
        public object legalFormDescriptionLine3 { get; set; }
        public object legalFormDescriptionLine4 { get; set; }
        public string legalFormId { get; set; }
        public object legalFormJurisdiction { get; set; }
        public Legalformname[] legalFormNames { get; set; }
        public object legalFormOther { get; set; }
        public DateTime leiIssuerAccreditationDate { get; set; }
        public string leiIssuerApiUri { get; set; }
        public string leiIssuerFundJurisdictionApiUri { get; set; }
        public string leiIssuerJurisdictionApiUri { get; set; }
        public string leiIssuerLei { get; set; }
        public string leiIssuerMarketingName { get; set; }
        public string leiIssuerName { get; set; }
        public string leiIssuerWebsite { get; set; }
        public object listOfIsin { get; set; }
        public object listOfIsinApiUri { get; set; }
        public object managedFunds { get; set; }
        public object masterFund { get; set; }
        public object micCodes { get; set; }
        public string name { get; set; }
        public object otherAddresses { get; set; }
        public object otherNames { get; set; }
        public object otherRegistrationValidatedAuthority { get; set; }
        public object otherValidationAuthorities { get; set; }
        public object registrationAuthority { get; set; }
        public string registrationAuthorityApiUri { get; set; }
        public string registrationAuthorityCode { get; set; }
        public string registrationAuthorityDescriptionLine1 { get; set; }
        public object registrationAuthorityDescriptionLine2 { get; set; }
        public string registrationAuthorityDescriptionLine3 { get; set; }
        public string registrationAuthorityDescriptionLine4 { get; set; }
        public object registrationAuthorityDescriptionLine5 { get; set; }
        public string registrationAuthorityDetails { get; set; }
        public string registrationAuthorityId { get; set; }
        public string registrationAuthorityInternationalName { get; set; }
        public string registrationAuthorityInternationalOrganizationName { get; set; }
        public Registrationauthorityjurisdiction[] registrationAuthorityJurisdictions { get; set; }
        public object registrationAuthorityLocalName { get; set; }
        public object registrationAuthorityLocalOrganizationName { get; set; }
        public string registrationAuthorityWebsite { get; set; }
        public string registrationCorroborationLevel { get; set; }
        public string registrationEntityId { get; set; }
        public DateTime registrationLastUpdatedDate { get; set; }
        public DateTime registrationNextRenewalDate { get; set; }
        public string registrationStatus { get; set; }
        public object registrationValidatedAuthority { get; set; }
        public string registrationValidationId { get; set; }
        public string sourceUri { get; set; }
        public string[] spglobalCompanyIds { get; set; }
        public object subCategory { get; set; }
        public object subFunds { get; set; }
        public object successorEntityLei { get; set; }
        public object successorEntityName { get; set; }
        public string timestamp { get; set; }
        public object transliteratedName { get; set; }
        public Ultimatechildren[] ultimateChildren { get; set; }
        public object ultimateChildrenApiUri { get; set; }
        public object ultimateParent { get; set; }
        public object ultimateParentApiUri { get; set; }
        public object ultimateParentExceptionDetails { get; set; }
        public object ultimateParentExceptionUri { get; set; }
        public object umbrellaFund { get; set; }
    }

    public class Headquartersaddress
    {
        public string[] addressLines { get; set; }
        public object addressNumber { get; set; }
        public object addressNumberWithinBuilding { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public object mailRouting { get; set; }
        public string pincode { get; set; }
        public string region { get; set; }
        public object type { get; set; }
    }

    public class Legaladdress
    {
        public string[] addressLines { get; set; }
        public object addressNumber { get; set; }
        public object addressNumberWithinBuilding { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public object mailRouting { get; set; }
        public string pincode { get; set; }
        public string region { get; set; }
        public object type { get; set; }
    }

    public class Directchildren
    {
        public string lei { get; set; }
        public string relationshipType { get; set; }
        public string registrationCorroborationDocuments { get; set; }
        public string registrationCorroborationLevel { get; set; }
        public object registrationCorroborationReference { get; set; }
        public DateTime initialRegistrationDate { get; set; }
        public DateTime registrationLastUpdatedDate { get; set; }
        public string managingLou { get; set; }
        public DateTime registrationNextRenewalDate { get; set; }
        public string registrationStatus { get; set; }
        public string relationshipStatus { get; set; }
        public string validFrom { get; set; }
        public object validTo { get; set; }
        public Period[] periods { get; set; }
        public DateTime relationshipStartDate { get; set; }
        public object relationshipEndDate { get; set; }
    }

    public class Period
    {
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string type { get; set; }
    }

    public class Legalformname
    {
        public string localName { get; set; }
        public string transliteratedName { get; set; }
        public string language { get; set; }
        public string languageCode { get; set; }
    }

    public class Registrationauthorityjurisdiction
    {
        public string country { get; set; }
        public string countryCode { get; set; }
        public string jurisdiction { get; set; }
    }

    public class Ultimatechildren
    {
        public string lei { get; set; }
        public string relationshipType { get; set; }
        public string registrationCorroborationDocuments { get; set; }
        public string registrationCorroborationLevel { get; set; }
        public object registrationCorroborationReference { get; set; }
        public DateTime initialRegistrationDate { get; set; }
        public DateTime registrationLastUpdatedDate { get; set; }
        public string managingLou { get; set; }
        public DateTime registrationNextRenewalDate { get; set; }
        public string registrationStatus { get; set; }
        public string relationshipStatus { get; set; }
        public string validFrom { get; set; }
        public object validTo { get; set; }
        public Period1[] periods { get; set; }
        public DateTime relationshipStartDate { get; set; }
        public object relationshipEndDate { get; set; }
    }

    public class Period1
    {
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string type { get; set; }
    }


}