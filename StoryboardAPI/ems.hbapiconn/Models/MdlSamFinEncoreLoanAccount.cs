using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hbapiconn.Models
{
    public class MdlSamFinEncoreLoanAccount
    {
    }

    public class MdlOpenLoanAcccountRequest
    {
        public string accountId { get; set; }
        public string amountMagnitude { get; set; }
        public string branchCode { get; set; }
        public string openedOnDate { get; set; }
        public string productCode { get; set; }
        public string tenureMagnitude { get; set; }
        public string tenureUnit { get; set; }
        public string normalInterestRate { get; set; }
        public string penalInterestRate { get; set; }
        public string openUserId { get; set; }
        public string roName { get; set; }
        public string customerId1 { get; set; }
        public string customer1FirstName { get; set; }
        public string customer1LastName { get; set; }
        public string customer1MiddleName { get; set; }
        public string customer1DateOfBirthStr { get; set; }
        public string gender { get; set; }
        public string customerType { get; set; }
        public string customer1Address1 { get; set; }
        public string customer1Address2 { get; set; }
        public string customer1Address3 { get; set; }
        public string customer1CityCode { get; set; }
        public string customer1DistrictCode { get; set; }
        public string customer1StateCode { get; set; }
        public string customer1CountryCode { get; set; }
        public string customer1PinCode { get; set; }
        public string customer1Phone1 { get; set; }
        public string customer1Phone2 { get; set; }
        public string customer1EmailId { get; set; }
        public string customer1MaritalStatus { get; set; }
        public string customer1MotherFirstName { get; set; }
        public string customer1MotherMiddleName { get; set; }
        public string customer1MotherLastName { get; set; }
        public string customer1fatherOrSpouseName { get; set; }
        public string customer1FatherOrSpouseDateOfBirth { get; set; }
        public string customer1Caste { get; set; }
        public string customer1Religion { get; set; }
        public string customer1BankName { get; set; }
        public string customer1BranchName { get; set; }
        public string customer1IfscCode { get; set; }
        public string customer1ResidenceType { get; set; }
        public string customer1AltAddress1 { get; set; }
        public string customer1AltAddress2 { get; set; }
        public string customer1AltAddress3 { get; set; }
        public string customer1AltCityCode { get; set; }
        public string customer1AltStateCode { get; set; }
        public string customer1AltPinCode { get; set; }
        public string pan { get; set; }
        public string uidNum { get; set; }
        public string kycDocumentType { get; set; }
        public string kycDocumentNum { get; set; }
        public string customerId2 { get; set; }
        public string customer2FirstName { get; set; }
        public string customer2LastName { get; set; }
        public string customer2MiddleName { get; set; }
        public string customer2DateOfBirth { get; set; }
        public string customer2Gender { get; set; }
        public string customer2Type { get; set; }
        public string customer2Address1 { get; set; }
        public string customer2Address2 { get; set; }
        public string customer2Address3 { get; set; }
        public string customer2CityCode { get; set; }
        public string customer2DistrictCode { get; set; }
        public string customer2StateCode { get; set; }
        public string customer2CountryCode { get; set; }
        public string customer2PinCode { get; set; }
        public string customer2Phone1 { get; set; }
        public string customer2Phone2 { get; set; }
        public string customer2EmailId { get; set; }
        public string customer2MaritalStatus { get; set; }
        public string customer2MotherFirstName { get; set; }
        public string customer2MotherMiddleName { get; set; }
        public string customer2MotherLastName { get; set; }
        public string customer2FatherOrSpouseName { get; set; }
        public string customer2FatherOrSpouseDateOfBirth { get; set; }
        public string customer2Caste { get; set; }
        public string customer2Religion { get; set; }
        public string customer2BankName { get; set; }
        public string customer2BranchName { get; set; }
        public string customer2IfscCode { get; set; }
        public string customer2ResidenceType { get; set; }
        public string customer2AltAddress1 { get; set; }
        public string customer2AltAddress2 { get; set; }
        public string customer2AltAddress3 { get; set; }
        public string customer2AltCityCode { get; set; }
        public string customer2AltStateCode { get; set; }
        public string customer2AltCountryCode { get; set; }
        public string customer2AltPinCode { get; set; }
        public string customer2Pan { get; set; }
        public string customer2UidNum { get; set; }
        public string customer2KycDocumentType { get; set; }
        public string customer2KycDocumentNum { get; set; }
        public string customer2RelationshipType { get; set; }
        public string customerId3 { get; set; }
        public string customer3FirstName { get; set; }
        public string customer3LastName { get; set; }
        public string customer3MiddleName { get; set; }
        public string customer3DateOfBirth { get; set; }
        public string customer3Gender { get; set; }
        public string customer3Type { get; set; }
        public string customer3Address1 { get; set; }
        public string customer3Address2 { get; set; }
        public string customer3Address3 { get; set; }
        public string customer3CityCode { get; set; }
        public string customer3DistrictCode { get; set; }
        public string customer3StateCode { get; set; }
        public string customer3CountryCode { get; set; }
        public string customer3PinCode { get; set; }
        public string customer3Phone1 { get; set; }
        public string customer3Phone2 { get; set; }
        public string customer3EmailId { get; set; }
        public string customer3MaritalStatus { get; set; }
        public string customer3MotherFirstName { get; set; }
        public string customer3MotherMiddleName { get; set; }
        public string customer3MotherLastName { get; set; }
        public string customer3FatherOrSpouseName { get; set; }
        public string customer3FatherOrSpouseDateOfBirth { get; set; }
        public string customer3Caste { get; set; }
        public string customer3Religion { get; set; }
        public string customer3BankName { get; set; }
        public string customer3BranchName { get; set; }
        public string customer3IfscCode { get; set; }
        public string customer3ResidenceType { get; set; }
        public string customer3AltAddress1 { get; set; }
        public string customer3AltAddress2 { get; set; }
        public string customer3AltAddress3 { get; set; }
        public string customer3AltCityCode { get; set; }
        public string customer3AltStateCode { get; set; }
        public string customer3AltCountryCode { get; set; }
        public string customer3AltPinCode { get; set; }
        public string customer3Pan { get; set; }
        public string customer3UidNum { get; set; }
        public string customer3KycDocumentType { get; set; }
        public string customer3KycDocumentNum { get; set; }
        public string customer3RelationshipType { get; set; }
        public string customerId4 { get; set; }
        public string customer4FirstName { get; set; }
        public string customer4LastName { get; set; }
        public string customer4MiddleName { get; set; }
        public string customer4DateOfBirth { get; set; }
        public string customer4Gender { get; set; }
        public string customer4Type { get; set; }
        public string customer4Address1 { get; set; }
        public string customer4Address2 { get; set; }
        public string customer4Address3 { get; set; }
        public string customer4CityCode { get; set; }
        public string customer4DistrictCode { get; set; }
        public string customer4StateCode { get; set; }
        public string customer4CountryCode { get; set; }
        public string customer4PinCode { get; set; }
        public string customer4Phone1 { get; set; }
        public string customer4Phone2 { get; set; }
        public string customer4EmailId { get; set; }
        public string customer4MaritalStatus { get; set; }
        public string customer4MotherFirstName { get; set; }
        public string customer4MotherMiddleName { get; set; }
        public string customer4MotherLastName { get; set; }
        public string customer4FatherOrSpouseName { get; set; }
        public string customer4FatherOrSpouseDateOfBirth { get; set; }
        public string customer4Caste { get; set; }
        public string customer4Religion { get; set; }
        public string customer4BankName { get; set; }
        public string customer4BranchName { get; set; }
        public string customer4IfscCode { get; set; }
        public string customer4ResidenceType { get; set; }
        public string customer4AltAddress1 { get; set; }
        public string customer4AltAddress2 { get; set; }
        public string customer4AltAddress3 { get; set; }
        public string customer4AltCityCode { get; set; }
        public string customer4AltStateCode { get; set; }
        public string customer4AltCountryCode { get; set; }
        public string customer4AltPinCode { get; set; }
        public string customer4Pan { get; set; }
        public string customer4UidNum { get; set; }
        public string customer4KycDocumentType { get; set; }
        public string customer4KycDocumentNum { get; set; }
        public string customer4RelationshipType { get; set; }
        public string customerId5 { get; set; }
        public string customer5FirstName { get; set; }
        public string customer5LastName { get; set; }
        public string customer5MiddleName { get; set; }
        public string customer5DateOfBirth { get; set; }
        public string customer5Gender { get; set; }
        public string customer5Type { get; set; }
        public string customer5Address1 { get; set; }
        public string customer5Address2 { get; set; }
        public string customer5Address3 { get; set; }
        public string customer5CityCode { get; set; }
        public string customer5DistrictCode { get; set; }
        public string customer5StateCode { get; set; }
        public string customer5CountryCode { get; set; }
        public string customer5PinCode { get; set; }
        public string customer5Phone1 { get; set; }
        public string customer5Phone2 { get; set; }
        public string customer5EmailId { get; set; }
        public string customer5MaritalStatus { get; set; }
        public string customer5MotherFirstName { get; set; }
        public string customer5MotherMiddleName { get; set; }
        public string customer5MotherLastName { get; set; }
        public string customer5FatherOrSpouseName { get; set; }
        public string customer5FatherOrSpouseDateOfBirth { get; set; }
        public string customer5Caste { get; set; }
        public string customer5Religion { get; set; }
        public string customer5BankName { get; set; }
        public string customer5BranchName { get; set; }
        public string customer5IfscCode { get; set; }
        public string customer5ResidenceType { get; set; }
        public string customer5AltAddress1 { get; set; }
        public string customer5AltAddress2 { get; set; }
        public string customer5AltAddress3 { get; set; }
        public string customer5AltCityCode { get; set; }
        public string customer5AltStateCode { get; set; }
        public string customer5AltCountryCode { get; set; }
        public string customer5AltPinCode { get; set; }
        public string customer5Pan { get; set; }
        public string customer5UidNum { get; set; }
        public string customer5KycDocumentType { get; set; }
        public string customer5KycDocumentNum { get; set; }
        public string customer5RelationshipType { get; set; }
        public string customerId6 { get; set; }
        public string customer6FirstName { get; set; }
        public string customer6LastName { get; set; }
        public string customer6MiddleName { get; set; }
        public string customer6DateOfBirth { get; set; }
        public string customer6Gender { get; set; }
        public string customer6Type { get; set; }
        public string customer6Address1 { get; set; }
        public string customer6Address2 { get; set; }
        public string customer6Address3 { get; set; }
        public string customer6CityCode { get; set; }
        public string customer6DistrictCode { get; set; }
        public string customer6StateCode { get; set; }
        public string customer6CountryCode { get; set; }
        public string customer6PinCode { get; set; }
        public string customer6Phone1 { get; set; }
        public string customer6Phone2 { get; set; }
        public string customer6EmailId { get; set; }
        public string customer6MaritalStatus { get; set; }
        public string customer6MotherFirstName { get; set; }
        public string customer6MotherMiddleName { get; set; }
        public string customer6MotherLastName { get; set; }
        public string customer6FatherOrSpouseName { get; set; }
        public string customer6FatherOrSpouseDateOfBirth { get; set; }
        public string customer6Caste { get; set; }
        public string customer6Religion { get; set; }
        public string customer6BankName { get; set; }
        public string customer6BranchName { get; set; }
        public string customer6IfscCode { get; set; }
        public string customer6ResidenceType { get; set; }
        public string customer6AltAddress1 { get; set; }
        public string customer6AltAddress2 { get; set; }
        public string customer6AltAddress3 { get; set; }
        public string customer6AltCityCode { get; set; }
        public string customer6AltStateCode { get; set; }
        public string customer6AltCountryCode { get; set; }
        public string customer6AltPinCode { get; set; }
        public string customer6Pan { get; set; }
        public string customer6UidNum { get; set; }
        public string customer6KycDocumentType { get; set; }
        public string customer6KycDocumentNum { get; set; }
        public string customer6RelationshipType { get; set; }
        public string guarantorCustomerId1 { get; set; }
        public string guarantorCustomerId2 { get; set; }
        public string nomineeCustomerId { get; set; }
        public string nomineeName { get; set; }
        public string nomineeDateOfBirth { get; set; }
        public string relationshipWithInsurer { get; set; }
        public string occupation { get; set; }
        public string annualIncome { get; set; }
        public string bankAccountNumber { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string ifscCode { get; set; }
        public string udfText1 { get; set; }
        public string udfText2 { get; set; }
        public string udfText3 { get; set; }
        public string udfText4 { get; set; }
        public string udfText5 { get; set; }
        public string udfText6 { get; set; }
        public string udfText7 { get; set; }     
        public string udfText8 { get; set; }
        public string udfText9 { get; set; }
        public string udfText10 { get; set; }
        public string udfText11 { get; set; }
        public string udfText12 { get; set; }
        public string udfText13 { get; set; }
        public string udfText14 { get; set; }
        public string udfText15 { get; set; }
        public string udfText16 { get; set; }
        public string udfText17 { get; set; }
        public string udfText18 { get; set; }
        public string udfText19 { get; set; }
        public string udfText20 { get; set; }
        public string udfText21 { get; set; }
        public string udfText22 { get; set; }
        public string udfText23 { get; set; }
        public string udfText24 { get; set; }
        public string udfText25 { get; set; }
        public string udfText26 { get; set; }
        public string udfText27 { get; set; }
        public string currencyCode { get; set; }
        public string accountHolderName { get; set; }
        public string sector { get; set; }
        public string segment { get; set; }
        public string gstin { get; set; }
        public string locationCode { get; set; }
        public string roEmailId { get; set; }
        public string moratoriumPeriod { get; set; }
        public string demandInterval { get; set; }
        public string normalInterestDemandInterval { get; set; }
        public string compoundingDay { get; set; }
        public string compoundingInterval { get; set; }
        public string postMaturityDemandInterval { get; set; }
        public string customerLimitCode { get; set; }
        public string guarantorLimitCode1 { get; set; }
        public string guarantorLimitCode2 { get; set; }
        public string groupAssociateId { get; set; }
        public string securityDepositInstallments { get; set; }
        public string customerLimitAmount { get; set; }
        public string customerSublimitAmount { get; set; }
        public string guarantor1LimitAmount { get; set; }
        public string guarantor1SublimitAmount { get; set; }
        public string guarantor2LimitAmount { get; set; }
        public string guarantor2SublimitAmount { get; set; }
        public string limitExpiryDate { get; set; }
        public string cin { get; set; }
        public string legalEntityIdentifier { get; set; }
    }

    public class MdlOpenLoanAccountResponse
    {
        public string transactionId { get; set; }
        public string valueDate { get; set; }
        public string valueDateStr { get; set; }
        public string transactionDate { get; set; }
        public string transactionDateStr { get; set; }
        public string systemDateAndTime { get; set; }
        public string systemDateAndTimeStr { get; set; }
        public string accountId { get; set; }
        public string entityId { get; set; }
        public string sequenceNum { get; set; }
        public string transactionName { get; set; }
        public string part1 { get; set; }
        public string part2 { get; set; }
        public string part3 { get; set; }
        public string part4 { get; set; }
        public string part5 { get; set; }
        public string part6 { get; set; }
        public string part7 { get; set; }
        public string part8 { get; set; }
        public string amount1 { get; set; }
        public string amount2 { get; set; }
        public string amount3 { get; set; }
        public string description { get; set; }
        public string userId { get; set; }
        public string customerName { get; set; }
        public string status { get; set; }
        public string responseCode { get; set; }
        public string response { get; set; }
        public string payeeAccountId { get; set; }
        public string param1 { get; set; }
        public string param2 { get; set; }
        public string param2Str { get; set; }
        public string param3 { get; set; }
        public string param4 { get; set; }
        public string param5 { get; set; }
        public string instrument { get; set; }
        public string reference { get; set; }
        public string transactionLotId { get; set; }
        public string currencyCode { get; set; }
        public string originalTransactionDate { get; set; }
        public string originalTransactionDateStr { get; set; }
        public string originalTransactionId { get; set; }
        public string originalUserId { get; set; }

    }

    public class MdlCreateLoanRequest : result1234
    {
        public string application_gid { get; set; }
        public string application2sanction_gid { get; set; }
        public string application2loan_gid { get; set; }
        public string customer_urn { get; set; }
        public string rmdisbursementrequest_gid { get; set; }
        public string farmercontact_gid { get; set; }

    }

    public class ApplicantType
    {
        public const string
            Institution = "Institution",
            Individual  = "Individual";
    }

    public class MdlCreateLoanResponse
    {
        public bool status { get; set; }
        public string message { get; set; }

    }

    public class MdlEncoreErrorMessageResponse
    {
        public string message { get; set; }
        public string errorCode { get; set; }
        public string localizedMessage { get; set; }
        public string causalMessage { get; set; }
    }

    public class MdlEncoreLoanAccountProduct
    {
        public string product { get; set; }
        public string sub_product { get; set; }
        public string principal_frequency { get; set; }
        public string interest_frequency { get; set; }
        public string interestdeduction_upfront { get; set; }
        public string loan_tenure { get; set; }
        public string moratorium_status { get; set; }
        public string moratorium_type { get; set; }

    }

    public class ProductCodeValidationResponse
    {
        public bool status { get; set; }
        public string product_code { get; set; }
        public string loanTenure_unit { get; set; }
    }

    public class ProductSpecificTenureUnitValidationResponse
    {
        public bool status { get; set; }
        public string message { get; set; }        
    }

    public class ProductLoanDetailsWithEncoreMasterValidationResponse
    {
        public bool status { get; set; }
        public string message { get; set; }

    }

    public class MdlProductLoanDetails
    {
        public string product { get; set; }
        public string sub_product { get; set; }
        public string principal_frequency { get; set; }
        public string interest_frequency { get; set; }
        public string interestdeduction_upfront { get; set; }
        public string loan_tenure { get; set; }
        public string moratorium_status { get; set; }
        public string moratorium_type { get; set; }

        public string facilityvalidity_days { get; set; }
        public string facilityvalidity_month { get; set; }
        public string facilityvalidity_year { get; set; }
       
    }
}