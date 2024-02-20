using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hbapiconn.Models
{

    public class result1234
    {
        public bool status { get; set; }
        public string message { get; set; }

    }

    public class MdlRequestBuyerUALimitDetails
    {
        public string buyerlimitJSON { get; set; }

    }
    public class MdlBuyerUALimitDetails
    {
        public string buyerID { get; set; }
        public string contractID { get; set; }
        public double utilizedLimit { get; set; }
        public double availableLimit { get; set; }
        public string productType { get; set; }
        public string productSubType { get; set; }

    }

    public class MdlBuyerUALimitDetailsResponse : result1234
    {

    }

    public static class BuyerUALimitResponseMessage
    {
        public const string
            BuyerIDNotFound = "Buyer ID not found in Custopedia",
            ContractIDNotFound = "Contract ID not found in Custopedia",
            ProductTypeNotFound = "Product Type not found in Custopedia",
            ProductSubTypeNotFound = "Product Sub Type not found in Custopedia",
            ErrorOccurredInUpdate = "Error Occurred in Updating Limit Details in Custopedia",
            Success = "Buyer Limit Details Updated in Custopedia Successfully",
            ProductTypeNotMatch = "Product Type / Product SubType not found for Contract ID in Custopedia";
    }


    public static class UpdateAPIReverseMetaList
    {
        public const string
            DaPostBuyerUALimitDetails = "DaPostBuyerUALimitDetails";
           
    }

    public static class LoggingTypeHBAPIReverseUpdate
    {
        public const string
            Buyer = "Buyer";
    }

}