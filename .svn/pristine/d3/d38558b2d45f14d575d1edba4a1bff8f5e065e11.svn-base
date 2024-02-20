using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;

using System.Configuration;

using System.Globalization;

using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.IO;
using ems.hbapiconn.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web.Helpers;
using System.Dynamic;
using System.Threading.Tasks;



namespace ems.hbapiconn.Functions
{
    public class FnHBAPISamAgroConn
    {
        string msSQL, lsinstitution_gid, lscontact_gid, lsemployee_gid, lsuser_gid, msGetUALimitGID;
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        int mnResult, mnResultContact, mnResultAddress;
        string loglspath = "", logFileName = "";
        bool postResult;
        string urn;
        string lsapplication_gid, lscontactpersonfirst_name, lscontactpersonmiddle_name, lscontactpersonlast_name, lsdesignation;
        DataTable dt_mobileno, dt_email;
        string iterator_table;
        string mscontactdetailsGID;
        ContactDetails[] arrContactDetails;

        //Actual API

        public MdlBuyerUALimitDetailsResponse DaPostBuyerUALimitDetails(MdlRequestBuyerUALimitDetails values)
        {
            MdlBuyerUALimitDetailsResponse ObjMdlBuyerUALimitDetailsResponse = new MdlBuyerUALimitDetailsResponse();
            try
            {
                LogForAuditReverseUpdateHBAPI("Logging started in API - " + UpdateAPIReverseMetaList.DaPostBuyerUALimitDetails + " for Buyer at " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), LoggingTypeHBAPIReverseUpdate.Buyer);

                MdlBuyerUALimitDetails ObjMdlBuyerUALimitDetails = new MdlBuyerUALimitDetails();
                ObjMdlBuyerUALimitDetails = JsonConvert.DeserializeObject<MdlBuyerUALimitDetails>(values.buyerlimitJSON);

                LogForAuditReverseUpdateHBAPI("JSON deserialized", LoggingTypeHBAPIReverseUpdate.Buyer);

                LogForAuditReverseUpdateHBAPI("Buyer ID obtained - " + ObjMdlBuyerUALimitDetails.buyerID, LoggingTypeHBAPIReverseUpdate.Buyer);
                LogForAuditReverseUpdateHBAPI("Contract ID obtained - " + ObjMdlBuyerUALimitDetails.contractID, LoggingTypeHBAPIReverseUpdate.Buyer);


                msSQL = " select application_gid from agr_mst_tbyronboard where application_no = '" + ObjMdlBuyerUALimitDetails.buyerID + "'";
                using (dt_datatable = objdbconn.GetDataTable(msSQL))
                {
                    if (dt_datatable.Rows.Count == 0)
                    {
                        LogForAuditReverseUpdateHBAPI("Buyer ID not found", LoggingTypeHBAPIReverseUpdate.Buyer);
                        ObjMdlBuyerUALimitDetailsResponse.status = false;
                        ObjMdlBuyerUALimitDetailsResponse.message = BuyerUALimitResponseMessage.BuyerIDNotFound;
                        return ObjMdlBuyerUALimitDetailsResponse;
                    }
                }
                    

                msSQL = " select application_gid from agr_mst_tapplication where contract_id = '" + ObjMdlBuyerUALimitDetails.contractID + "'";
                using (dt_datatable = objdbconn.GetDataTable(msSQL))
                {
                    if (dt_datatable.Rows.Count == 0)
                    {
                        LogForAuditReverseUpdateHBAPI("Contract ID not found", LoggingTypeHBAPIReverseUpdate.Buyer);
                        ObjMdlBuyerUALimitDetailsResponse.status = false;
                        ObjMdlBuyerUALimitDetailsResponse.message = BuyerUALimitResponseMessage.ContractIDNotFound;
                        return ObjMdlBuyerUALimitDetailsResponse;
                    }
                }
                    

                msSQL = " select loanproduct_gid from agr_mst_tloanproduct where loanproduct_name = '" + ObjMdlBuyerUALimitDetails.productType + "'";
                using (dt_datatable = objdbconn.GetDataTable(msSQL))
                {
                    if (dt_datatable.Rows.Count == 0)
                    {
                        LogForAuditReverseUpdateHBAPI("Product Type not found", LoggingTypeHBAPIReverseUpdate.Buyer);
                        ObjMdlBuyerUALimitDetailsResponse.status = false;
                        ObjMdlBuyerUALimitDetailsResponse.message = BuyerUALimitResponseMessage.ProductTypeNotFound;
                        return ObjMdlBuyerUALimitDetailsResponse;
                    }
                }
                    

                msSQL = " select loansubproduct_gid from agr_mst_tloansubproduct where loansubproduct_name = '" + ObjMdlBuyerUALimitDetails.productSubType + "'";
                using (dt_datatable = objdbconn.GetDataTable(msSQL))
                {
                    if (dt_datatable.Rows.Count == 0)
                    {
                        LogForAuditReverseUpdateHBAPI("Product Sub Type not found", LoggingTypeHBAPIReverseUpdate.Buyer);
                        ObjMdlBuyerUALimitDetailsResponse.status = false;
                        ObjMdlBuyerUALimitDetailsResponse.message = BuyerUALimitResponseMessage.ProductSubTypeNotFound;
                        return ObjMdlBuyerUALimitDetailsResponse;
                    }
                }
                    

                msSQL = " select product_type,productsub_type from agr_mst_tapplication2loan where application_gid = (select application_gid from agr_mst_tapplication where contract_id='" + ObjMdlBuyerUALimitDetails.contractID + "')";
                using (dt_datatable = objdbconn.GetDataTable(msSQL))
                {
                    int productTypeFound = 0;
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dt in dt_datatable.Rows)
                        {
                            if ((dt["product_type"].ToString() == ObjMdlBuyerUALimitDetails.productType) && (dt["productsub_type"].ToString() == ObjMdlBuyerUALimitDetails.productSubType))
                            {
                                productTypeFound++;
                            }
                        }
                        if(productTypeFound == 0)
                        {
                            LogForAuditReverseUpdateHBAPI("Product Type not matching Contract ID", LoggingTypeHBAPIReverseUpdate.Buyer);
                            ObjMdlBuyerUALimitDetailsResponse.status = false;
                            ObjMdlBuyerUALimitDetailsResponse.message = BuyerUALimitResponseMessage.ProductTypeNotMatch;
                            return ObjMdlBuyerUALimitDetailsResponse;
                        }

                    }
                }



                msSQL = " update agr_mst_tapplication2loan set" +
                        " utilized_limit='" + ObjMdlBuyerUALimitDetails.utilizedLimit + "'," +
                        " available_limit='" + ObjMdlBuyerUALimitDetails.availableLimit + "'" +
                        " where (application_gid =(select application_gid from agr_mst_tapplication where contract_id='" + ObjMdlBuyerUALimitDetails.contractID + "'))" +
                        " and (product_type='" + ObjMdlBuyerUALimitDetails.productType + "') and (productsub_type='" + ObjMdlBuyerUALimitDetails.productSubType + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    LogForAuditReverseUpdateHBAPI("Buyer Limit details updated in Storyboard DB successfully", LoggingTypeHBAPIReverseUpdate.Buyer);
                    msGetUALimitGID = objcmnfunctions.GetMasterGID("BEUL");
                    msSQL = " insert into agr_trn_tbuyererpualimitupdatelog(" +
                         " buyererpualimitupdatelog_gid," +
                         " buyer_id," +
                         " contract_id, " +
                         " utilized_limit, " +
                         " available_limit, " +
                         " product_type, " +
                         " product_subtype, " +
                         " created_date)" +
                             " VALUES(" +
                             "'" + msGetUALimitGID + "'," +
                             "'" + ObjMdlBuyerUALimitDetails.buyerID + "'," +
                             "'" + ObjMdlBuyerUALimitDetails.contractID + "'," +
                             "'" + ObjMdlBuyerUALimitDetails.utilizedLimit + "'," +
                             "'" + ObjMdlBuyerUALimitDetails.availableLimit + "'," +
                             "'" + ObjMdlBuyerUALimitDetails.productType + "'," +
                             "'" + ObjMdlBuyerUALimitDetails.productSubType + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    ObjMdlBuyerUALimitDetailsResponse.status = true;
                    ObjMdlBuyerUALimitDetailsResponse.message = BuyerUALimitResponseMessage.Success;
                }
                else
                {
                    LogForAuditReverseUpdateHBAPI("Error occurred in Buyer Limit details updation in Storyboard DB", LoggingTypeHBAPIReverseUpdate.Buyer);
                    ObjMdlBuyerUALimitDetailsResponse.status = false;
                    ObjMdlBuyerUALimitDetailsResponse.message = BuyerUALimitResponseMessage.ErrorOccurredInUpdate;
                }


            }
            catch (Exception ex)
            {
                LogForAuditReverseUpdateHBAPI("Exception occurred in Buyer Limit details updation in StoryboardAPI - " + ex.ToString(), LoggingTypeHBAPIReverseUpdate.Buyer);

                ObjMdlBuyerUALimitDetailsResponse.status = false;
                ObjMdlBuyerUALimitDetailsResponse.message = BuyerUALimitResponseMessage.ErrorOccurredInUpdate;

            }
            LogForAuditReverseUpdateHBAPI("End of logging", LoggingTypeHBAPIReverseUpdate.Buyer);
            return ObjMdlBuyerUALimitDetailsResponse;
        }


        public void LogForAuditReverseUpdateHBAPI(string strVal, string type)
        {
            try
            {

                if (type == LoggingTypeHBAPIUpdate.Buyer)
                {

                    loglspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + fetchCompanyCode() + "/" + "SamAgro/HyperbridgeAPI/NetsuiteERPLogReverseUpdate/" + type + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    if ((!System.IO.Directory.Exists(loglspath)))
                        System.IO.Directory.CreateDirectory(loglspath);
                    if (logFileName == "")
                    {
                        logFileName = "Log_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
                    }
                    loglspath = loglspath + logFileName;
                }

                System.IO.StreamWriter sw = new System.IO.StreamWriter(loglspath, true);
                sw.WriteLine(strVal);
                sw.Close();

            }
            catch (Exception ex)
            {
            }
        }

        public string fetchCompanyCode()
        {
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            string lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            return lscompany_code;
        }

    }
}