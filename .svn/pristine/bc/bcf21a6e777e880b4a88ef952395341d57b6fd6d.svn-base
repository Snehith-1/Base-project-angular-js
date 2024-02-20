using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using ems.master.Models;
using ems.utilities.Functions;
using System.Data;
using System.Data.Odbc;
using vcidex_kyc.Models;
using System.Text;
using System.Net;


/// <summary>
/// (It's used for Kyc ) Kyc DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Praveen Raj</remarks>
/// 

namespace vcidex_kyc.Function
{
    public class DaCADKyc
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid;
        int mnResult;

        public PanNumberResponse GetPanNumberDetails(string employee_gid, PanNumberModel Values)
        {
            PanNumberResponse ObjPanNumberResponse = new PanNumberResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "pan");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{\"consent\":\"" + Values.consent + "\",\"pan\":\"" + Values.pan + "\"}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjPanNumberResponse = JsonConvert.DeserializeObject<PanNumberResponse>(response.Content);
                

                msSQL = "select kycpanauthentication_gid from ocs_trn_tcadkycpanauthentication where function_gid='" + employee_gid + "' and remarks='" + "Active" + "'";

                string kycpanauthentication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update ocs_trn_tcadkycpanauthentication set " +
                        " remarks='" + "Deleted" + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where kycpanauthentication_gid='" + kycpanauthentication_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("KPNA");
                msSQL = " insert into ocs_trn_tcadkycpanauthentication(" +
                        " kycpanauthentication_gid," +
                        " function_gid," +
                        " pan_no," +
                        " pan_name," +
                        " validation_status," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + Values.pan + "',";
                if (ObjPanNumberResponse.statusCode == "101")
                {
                    msSQL += "'" + ObjPanNumberResponse.result.name + "'," +
                             "'" + "Verified" + "',";
                }
                else
                {
                    msSQL += "''," +
                             "'" + "Not Verified" + "',";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (ObjPanNumberResponse.statusCode == "101")
                {
                    ObjPanNumberResponse.status = true;
                }
                else
                {
                    ObjPanNumberResponse.status = false;
                    ObjPanNumberResponse.message = ErrorResponseKyc.errorResponse + ObjPanNumberResponse.error;
                }


            }
            catch (Exception ex)
            {
                ObjPanNumberResponse.status = false;
                ObjPanNumberResponse.message = "Error Occurred in PAN Validation";
            }
            return ObjPanNumberResponse;
        }

        public PanVerificationResponse GetPanVerificationDetails(PanVerificationModel Values)
        {
            PanVerificationResponse ObjPanVerificationResponse = new PanVerificationResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "pan-authentication");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\",  \"pan\" : \"" + Values.pan + "\",\"dob\" : \"" + Values.dob + "\",  \"name\" : \"" + Values.name + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjPanVerificationResponse = JsonConvert.DeserializeObject<PanVerificationResponse>(response.Content);
                ObjPanVerificationResponse.status = true;

            }
            catch (Exception ex)
            {
                ObjPanVerificationResponse.status = false;
                ObjPanVerificationResponse.message = "Error Occurred";
            }
            return ObjPanVerificationResponse;
        }

        public PanAadhaarLinkResponse GetPANAaadharLinkDetails(string employee_gid, PanNumberModel Values)
        {
            PanAadhaarLinkResponse ObjPanAadhaarLinkResponse = new PanAadhaarLinkResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis2"].ToString() + "pan-link");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{\"consent\":\"" + Values.consent + "\",\"pan\":\"" + Values.pan + "\"}", ParameterType.RequestBody); IRestResponse response = client.Execute(request);
                ObjPanAadhaarLinkResponse = JsonConvert.DeserializeObject<PanAadhaarLinkResponse>(response.Content);
                ObjPanAadhaarLinkResponse.status = true;

                msSQL = "select kycpanaadhaarlink_gid from ocs_mst_tkycpanaadhaarlink where function_gid='" + employee_gid + "' and remarks='" + "Active" + "'";

                string kycpanaadhaarlink_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update ocs_mst_tkycpanaadhaarlink set " +
                        " remarks='" + "Deleted" + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where kycpanaadhaarlink_gid='" + kycpanaadhaarlink_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                msGetGid = objcmnfunctions.GetMasterGID("KPAL");
                msSQL = " insert into ocs_mst_tkycpanaadhaarlink(" +
                        " kycpanaadhaarlink_gid," +
                        " function_gid," +
                        " pan_no," +
                        " panaadhaarlink_status," +
                        " validation_status," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + Values.pan + "',";

                if (ObjPanAadhaarLinkResponse.statusCode == "101")
                {
                    if (ObjPanAadhaarLinkResponse.result.isAadhaarLinked == true)
                    {
                        msSQL += "'" + "Linked" + "'," +
                                 "'" + "Verified" + "',";
                    }
                    else
                    {
                        msSQL += "'" + "Not Linked" + "'," +
                                 "'" + "Verified" + "',";
                    }

                }
                else
                {
                    msSQL += "''," +
                             "'" + "Not Verified" + "',";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);




            }
            catch (Exception ex)
            {
                ObjPanAadhaarLinkResponse.status = false;
                ObjPanAadhaarLinkResponse.message = "Error Occurred in PAN Aadhar Link Verification";
            }
            return ObjPanAadhaarLinkResponse;
        }

        public DrivingLicenseResponse GetDrivingLicenseAuthenticationDtl(string employee_gid, DrivingLicenseModel Values)
        {
            DrivingLicenseResponse ObjDrivingLicenseResponse = new DrivingLicenseResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis2"].ToString() + "dl");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\",  \"dlNo\" : \"" + Values.dlno + "\",\"dob\" : \"" + Values.dob + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjDrivingLicenseResponse = JsonConvert.DeserializeObject<DrivingLicenseResponse>(response.Content);
                

                msSQL = "select kycdlauthentication_gid from ocs_trn_tcadkycdlauthentication where function_gid='" + employee_gid + "' and remarks='" + "Active" + "'";

                string kycdlauthentication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update ocs_trn_tcadkycdlauthentication set " +
                        " remarks='" + "Deleted" + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where kycdlauthentication_gid='" + kycdlauthentication_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("KDLA");
                msSQL = " insert into ocs_trn_tcadkycdlauthentication(" +
                        " kycdlauthentication_gid," +
                        " function_gid," +
                        " dlno," +
                        " validation_status," +
                        " response," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + Values.dlno + "',";
                if (ObjDrivingLicenseResponse.statusCode == 101)
                {
                    msSQL += "'" + "Verified" + "'," +
                             "'" + response.Content + "',";

                }
                else
                {
                    msSQL += "'" + "Not Verified" + "'," +
                             "null,";

                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (ObjDrivingLicenseResponse.statusCode == 101)
                {
                    ObjDrivingLicenseResponse.status = true;
                }
                else
                {
                    ObjDrivingLicenseResponse.status = false;
                    ObjDrivingLicenseResponse.message = ErrorResponseKyc.errorResponse + ObjDrivingLicenseResponse.error;
                }


            }
            catch (Exception ex)
            {
                ObjDrivingLicenseResponse.status = false;
                ObjDrivingLicenseResponse.message = "Error Occurred  in Driving License Authentication";
            }
            return ObjDrivingLicenseResponse;
        }

        public VoterIDResponse GetVoterIDDetails(string employee_gid, VoterIDModel Values)
        {
            VoterIDResponse ObjVoterIDResponse = new VoterIDResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "voter");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\",  \"epic_no\" : \"" + Values.epic_no + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjVoterIDResponse = JsonConvert.DeserializeObject<VoterIDResponse>(response.Content);
                

                msSQL = "select kycepicauthentication_gid from ocs_trn_tcadkycepicauthentication where function_gid='" + employee_gid + "' and remarks='" + "Active" + "'";

                string kycepicauthentication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update ocs_trn_tcadkycepicauthentication set " +
                        " remarks='" + "Deleted" + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where kycepicauthentication_gid='" + kycepicauthentication_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("KEPA");
                msSQL = " insert into ocs_trn_tcadkycepicauthentication(" +
                        " kycepicauthentication_gid," +
                        " function_gid," +
                        " epic_no," +
                        " validation_status," +
                        " response," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + Values.epic_no + "',";
                if (ObjVoterIDResponse.statusCode == "101")
                {
                    msSQL += "'" + "Verified" + "'," +
                             "'" + response.Content + "',";
                }
                else
                {
                    msSQL += "'" + "Not Verified" + "'," +
                             "null,";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (ObjVoterIDResponse.statusCode == "101")
                {
                    ObjVoterIDResponse.status = true;
                }
                else
                {
                    ObjVoterIDResponse.status = false;
                    ObjVoterIDResponse.message = ErrorResponseKyc.errorResponse + ObjVoterIDResponse.error;
                }


            }
            catch (Exception ex)
            {
                ObjVoterIDResponse.status = false;
                ObjVoterIDResponse.message = "Error Occurred in Voter ID Authentication";
            }
            return ObjVoterIDResponse;
        }

        public PassportDetailsResponse GetPassportDetails(string employee_gid, PassportModel Values)
        {
            PassportDetailsResponse ObjPassportDetailsResponse = new PassportDetailsResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "passport-verification");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\",  \"fileNo\" : \"" + Values.fileNo + "\",  \"dob\" : \"" + Values.dob + "\",  \"passportNo\" : \"" + Values.passportNo + "\",  \"doi\" : \"" + Values.doi + "\",  \"name\" : \"" + Values.pname + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjPassportDetailsResponse = JsonConvert.DeserializeObject<PassportDetailsResponse>(response.Content);
                

                msSQL = "select kycpassportauthentication_gid from ocs_trn_tcadkycpassportauthentication where function_gid='" + employee_gid + "' and remarks='" + "Active" + "'";

                string kycpassportauthentication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update ocs_trn_tcadkycpassportauthentication set " +
                        " remarks='" + "Deleted" + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where kycpassportauthentication_gid='" + kycpassportauthentication_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("KEPA");
                msSQL = " insert into ocs_trn_tcadkycpassportauthentication(" +
                        " kycpassportauthentication_gid," +
                        " function_gid," +
                        " fileNo," +
                        " dob," +
                        " validation_status," +
                        " response," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + Values.fileNo + "'," +
                        "'" + Values.dob + "',";
                if (ObjPassportDetailsResponse.statusCode == "101")
                {
                    msSQL += "'" + "Verified" + "'," +
                             "'" + response.Content + "',";
                }
                else
                {
                    msSQL += "'" + "Not Verified" + "'," +
                             "null,";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (ObjPassportDetailsResponse.statusCode == "101")
                {
                    ObjPassportDetailsResponse.status = true;
                }
                else
                {
                    ObjPassportDetailsResponse.status = false;
                    ObjPassportDetailsResponse.message = ErrorResponseKyc.errorResponse + ObjPassportDetailsResponse.error;
                }

            }
            catch (Exception ex)
            {
                ObjPassportDetailsResponse.status = false;
                ObjPassportDetailsResponse.message = "Error Occurred";
            }
            return ObjPassportDetailsResponse;
        }

        public GSTSBPANDetailsResponse Getgstsbpandetails(string employee_gid, PanNumberModel Values)
        {
            GSTSBPANDetailsResponse ObjGSTSBPANDetailsResponse = new GSTSBPANDetailsResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis3"].ToString() + "search");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\",  \"pan\" : \"" + Values.pan + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjGSTSBPANDetailsResponse = JsonConvert.DeserializeObject<GSTSBPANDetailsResponse>(response.Content);
                

                msSQL = "select kycgstsbpan_gid from ocs_trn_tcadkycgstsbpan where function_gid='" + employee_gid + "' and remarks='" + "Active" + "'";

                string kycgstsbpan_gid = objdbconn.GetExecuteScalar(msSQL);
                string gstList = "";


                msSQL = " update ocs_trn_tcadkycgstsbpan set " +
                        " remarks='" + "Deleted" + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where kycgstsbpan_gid='" + kycgstsbpan_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("KGSP");
                msSQL = " insert into ocs_trn_tcadkycgstsbpan(" +
                        " kycgstsbpan_gid," +
                        " function_gid," +
                        " pan," +
                        " gstValues," +
                        " validation_status," +
                        " response," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + Values.pan + "',";
                if (ObjGSTSBPANDetailsResponse.statusCode == 101)
                {

                    for (int i = 0; i < ObjGSTSBPANDetailsResponse.result.Length; i++)
                    {
                        string gst = "";
                        gst += ObjGSTSBPANDetailsResponse.result[i].gstinId;
                        gstList += "" + gst + ",";
                    }
                    gstList = gstList.TrimEnd(',');

                    msSQL += "'" + gstList + "'," +
                             "'" + "Verified" + "'," +
                             "'" + response.Content + "',";
                }
                else
                {
                    msSQL += "''," +
                             "'" + "Not Verified" + "'," +
                             "null,";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (ObjGSTSBPANDetailsResponse.statusCode == 101)
                {
                    ObjGSTSBPANDetailsResponse.status = true;
                }
                else
                {
                    ObjGSTSBPANDetailsResponse.status = false;
                    ObjGSTSBPANDetailsResponse.message = ErrorResponseKyc.errorResponse + ObjGSTSBPANDetailsResponse.error;
                }

            }
            catch (Exception ex)
            {
                ObjGSTSBPANDetailsResponse.status = false;
                ObjGSTSBPANDetailsResponse.message = "Error Occurred in GST Search Basis PAN";
            }
            return ObjGSTSBPANDetailsResponse;
        }

        public GSTVerificationResponse GSTVerificationDetails(string employee_gid, GSTVerificationModel Values)
        {
            GSTVerificationResponse ObjGSTVerificationResponse = new GSTVerificationResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis3"].ToString() + "gst-verification");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\",  \"gstin\" : \"" + Values.gstin + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjGSTVerificationResponse = JsonConvert.DeserializeObject<GSTVerificationResponse>(response.Content);
                
                msSQL = "select gspinverification_gid from ocs_trn_tcadgspinverification where function_gid='" + Values.institution2branch_gid + "' and remarks='" + "Active" + "'";

                string gspinverification_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update ocs_trn_tcadgspinverification set " +
                        " remarks='" + "Deleted" + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where gspinverification_gid='" + gspinverification_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("KGSP");
                msSQL = " insert into ocs_trn_tcadgspinverification(" +
                        " gspinverification_gid ," +
                        " function_gid," +
                        " application_gid," +
                        " gst_no," +
                        " business_registered ," +
                        " buiness_constitution," +
                        " registration_status ," +
                        " business_legalname," +
                        " taxpayer," +
                        " trade_name  ," +
                        " validation_status," +
                        " response," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + Values.institution2branch_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + Values.gstin + "',";

                if (ObjGSTVerificationResponse.statusCode == 101)
                {

                    msSQL += "'" + ObjGSTVerificationResponse.result.nba + "'," +
                             "'" + ObjGSTVerificationResponse.result.ctb + "'," +
                             "'" + ObjGSTVerificationResponse.result.sts + "'," +
                             "'" + ObjGSTVerificationResponse.result.lgnm + "'," +
                             "'" + ObjGSTVerificationResponse.result.dty + "'," +
                             "'" + ObjGSTVerificationResponse.result.tradeNam + "'," +
                             "'" + "Verified" + "'," +
                             "'" + response.Content + "',";
                }
                else
                {
                    msSQL += "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "'" + "Not Verified" + "'," +
                             "null,";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (ObjGSTVerificationResponse.statusCode == 101)
                {
                    msSQL = "update ocs_trn_tcadinstitution2branch set verification_status='Verified' where institution2branch_gid='" + Values.institution2branch_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    ObjGSTVerificationResponse.result.nba = null;
                    msSQL = "update ocs_trn_tcadinstitution2branch set verification_status='Not Verified' where institution2branch_gid='" + Values.institution2branch_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (ObjGSTVerificationResponse.statusCode == 101)
                {
                    ObjGSTVerificationResponse.status = true;
                }
                else
                {
                    ObjGSTVerificationResponse.status = false;
                    ObjGSTVerificationResponse.message = ErrorResponseKyc.errorResponse + ObjGSTVerificationResponse.error;
                }

            }
            catch (Exception ex)
            {
                ObjGSTVerificationResponse.status = false;
                ObjGSTVerificationResponse.message = "Error Occurred";
            }
            return ObjGSTVerificationResponse;
        }

        public GSPGSTReturnFilingResponse GetGSPGSTReturnFiling(string employee_gid, GSTVerificationModel Values)
        {
            GSPGSTReturnFilingResponse ObjGSPGSTReturnFilingResponse = new GSPGSTReturnFilingResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis3"].ToString() + "gst-return-status");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\",  \"gstin\" : \"" + Values.gstin + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjGSPGSTReturnFilingResponse = JsonConvert.DeserializeObject<GSPGSTReturnFilingResponse>(response.Content);
                
                msSQL = "select gstreturnfilling_gid from ocs_trn_tcadgstreturnfilling where function_gid='" + Values.institution2branch_gid + "' and remarks='" + "Active" + "'";

                string gstreturnfilling_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update ocs_trn_tcadgstreturnfilling set " +
                        " remarks='" + "Deleted" + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where gstreturnfilling_gid='" + gstreturnfilling_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("KPNA");
                msSQL = " insert into ocs_trn_tcadgstreturnfilling(" +
                        " gstreturnfilling_gid," +
                        " function_gid," +
                        " application_gid," +
                        " gst_no ," +
                        " is_defaulter," +
                        " is_any_delay," +
                        " validation_status," +
                        " response," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + Values.institution2branch_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + Values.gstin + "',";
                if (ObjGSPGSTReturnFilingResponse.result.gstin == "" || ObjGSPGSTReturnFilingResponse.result.gstin == null)
                {
                    msSQL += "''," +
                            "''," +
                            "'" + "Not Verified" + "'," +
                            "null,";
                }
                else
                {
                    msSQL += "'" + ObjGSPGSTReturnFilingResponse.result.compliance_status.is_defaulter + "'," +
                             "'" + ObjGSPGSTReturnFilingResponse.result.compliance_status.is_any_delay + "'," +
                             "'" + "Verified" + "'," +
                             "'" + response.Content + "',";

                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (ObjGSPGSTReturnFilingResponse.result.gstin == "" || ObjGSPGSTReturnFilingResponse.result.gstin == null)
                {
                    msSQL = "update ocs_trn_tcadinstitution2branch set returnfilling_status='Not Verified' where institution2branch_gid='" + Values.institution2branch_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = "update ocs_trn_tcadinstitution2branch set returnfilling_status='Verified' where institution2branch_gid='" + Values.institution2branch_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (ObjGSPGSTReturnFilingResponse.statusCode == 101)
                {
                    ObjGSPGSTReturnFilingResponse.status = true;
                }
                else
                {
                    ObjGSPGSTReturnFilingResponse.status = false;
                    ObjGSPGSTReturnFilingResponse.message = ErrorResponseKyc.errorResponse + ObjGSPGSTReturnFilingResponse.error;
                }

            }
            catch (Exception ex)
            {
                ObjGSPGSTReturnFilingResponse.status = false;
                ObjGSPGSTReturnFilingResponse.message = "Error Occurred";
            }
            return ObjGSPGSTReturnFilingResponse;
        }

        public GSTAuthenticationResponse GetGSTDetailed(string employee_gid, GSTAuthenticationModel Values)
        {
            GSTAuthenticationResponse ObjGSTAuthenticationResponse = new GSTAuthenticationResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                string additionalData = Values.additionalData.ToString().ToLower();
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis3"].ToString() + "gstdetailed");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\",  \"additionalData\" : " + additionalData + ", \"gstin\" : \"" + Values.gstin + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjGSTAuthenticationResponse = JsonConvert.DeserializeObject<GSTAuthenticationResponse>(response.Content);
                
                msSQL = "select gspinauthentication_gid from ocs_trn_tcadgspinauthentication where function_gid='" + Values.institution2branch_gid + "' and remarks='" + "Active" + "'";

                string gspinauthentication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update ocs_trn_tcadgspinauthentication set " +
                        " remarks='" + "Deleted" + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where gspinauthentication_gid='" + gspinauthentication_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("KGSP");
                msSQL = " insert into ocs_trn_tcadgspinauthentication(" +
                        " gspinauthentication_gid ," +
                        " function_gid," +
                        " application_gid," +
                        " gst_no," +
                        " business_registered ," +
                        " buiness_constitution," +
                        " registration_status ," +
                        " business_legalname," +
                        " taxpayer," +
                        " trade_name  ," +
                        " validation_status  ," +
                        " response," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + Values.institution2branch_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + Values.gstin + "',";

                if (ObjGSTAuthenticationResponse.statusCode == 101)
                {

                    msSQL += "'" + ObjGSTAuthenticationResponse.result.nba + "'," +
                             "'" + ObjGSTAuthenticationResponse.result.ctb + "'," +
                             "'" + ObjGSTAuthenticationResponse.result.sts + "'," +
                             "'" + ObjGSTAuthenticationResponse.result.lgnm + "'," +
                             "'" + ObjGSTAuthenticationResponse.result.dty + "'," +
                             "'" + ObjGSTAuthenticationResponse.result.tradeNam + "'," +
                             "'" + "Verified" + "'," +
                             "'" + response.Content + "',";

                }
                else
                {
                    msSQL += "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "'" + "Not Verified" + "'," +
                             "null,";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (ObjGSTAuthenticationResponse.statusCode == 101)
                {
                    msSQL = "update ocs_trn_tcadinstitution2branch set authentication_status='Verified' where institution2branch_gid='" + Values.institution2branch_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    ObjGSTAuthenticationResponse.result.nba = null;
                    msSQL = "update ocs_trn_tcadinstitution2branch set authentication_status='Not Verified' where institution2branch_gid='" + Values.institution2branch_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (ObjGSTAuthenticationResponse.statusCode == 101)
                {
                    ObjGSTAuthenticationResponse.status = true;
                }
                else
                {
                    ObjGSTAuthenticationResponse.status = false;
                    ObjGSTAuthenticationResponse.message = ErrorResponseKyc.errorResponse + ObjGSTAuthenticationResponse.error;
                }

            }
            catch (Exception ex)
            {
                ObjGSTAuthenticationResponse.status = false;
                ObjGSTAuthenticationResponse.message = "Error Occurred";
            }
            return ObjGSTAuthenticationResponse;
        }

        public CompanyLLPResponse GetCompanyLLP(CompanyLLP Values)
        {
            CompanyLLPResponse ObjCompanyLLPResponse = new CompanyLLPResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "cinlookup");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\", \"company_name\" : \"" + Values.company_name + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjCompanyLLPResponse = JsonConvert.DeserializeObject<CompanyLLPResponse>(response.Content);
                ObjCompanyLLPResponse.status = true;

            }
            catch (Exception ex)
            {
                ObjCompanyLLPResponse.status = false;
                ObjCompanyLLPResponse.message = "Error Occurred";
            }
            return ObjCompanyLLPResponse;
        }

        public CINLLPResponse GetCompanyLLP_no(string employee_gid, CIN Values)
        {
            CINLLPResponse objCINLLPResponse = new CINLLPResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "mca");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\", \"cin\" : \"" + Values.cin_no + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                objCINLLPResponse = JsonConvert.DeserializeObject<CINLLPResponse>(response.Content);
                

                msSQL = "delete from ocs_trn_tcadcompanyllpno where companyllpno_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                msSQL = " insert into ocs_trn_tcadcompanyllpno(" +
                        " companyllpno_gid," +
                        " function_gid," +
                        " application_gid," +
                        " cin_no ," +
                        " company_name ," +
                        " roc_code ," +
                        " registration_no," +
                        " company_category ," +
                        " company_subcategory ," +
                        " class_of_company ," +
                        " number_of_members ," +
                        " date_of_incorporation ," +
                        " company_status ," +
                        " registered_address ," +
                        " alternative_address ," +
                        " email_address ," +
                        " listed_status ," +
                        " suspended_at_stock_exchange ," +
                        " date_of_last_AGM ," +
                        " date_of_balance_sheet ," +
                        " paid_up_capital ," +
                        " authorised_capital ," +
                        " validation_status," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + employee_gid + "'," +
                        "'" + Values.function_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + Values.cin_no + "',";
                if (objCINLLPResponse.statuscode == "101")
                {
                    msSQL += "'" + objCINLLPResponse.result.Company_Name + "'," +
                             "'" + objCINLLPResponse.result.ROC_Code + "'," +
                             "'" + objCINLLPResponse.result.Registration_Number + "'," +
                             "'" + objCINLLPResponse.result.Company_Category + "'," +
                             "'" + objCINLLPResponse.result.Company_SubCategory + "'," +
                             "'" + objCINLLPResponse.result.Class_of_Company + "'," +
                             "'" + objCINLLPResponse.result.Number_of_Members + "'," +
                             "'" + objCINLLPResponse.result.Date_of_Incorporation + "'," +
                             "'" + objCINLLPResponse.result.Company_Status + "'," +
                             "'" + objCINLLPResponse.result.Registered_Address + "'," +
                             "'" + objCINLLPResponse.result.alternative_address + "'," +
                             "'" + objCINLLPResponse.result.Email_Id + "'," +
                             "'" + objCINLLPResponse.result.Whether_Listed_or_not + "'," +
                             "'" + objCINLLPResponse.result.Suspended_at_stock_exchange + "'," +
                             "'" + objCINLLPResponse.result.Date_of_last_AGM + "'," +
                             "'" + objCINLLPResponse.result.Date_of_Balance_Sheet + "'," +
                             "'" + objCINLLPResponse.result.Paid_up_Capital + "'," +
                             "'" + objCINLLPResponse.result.Authorised_Capital + "'," +
                             "'" + "Verified" + "',";
                }
                else
                {
                    msSQL += "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," + "'" + "Not Verified" + "',";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (objCINLLPResponse.statuscode == "101")
                {
                    objCINLLPResponse.status = true;
                }
                else
                {
                    objCINLLPResponse.status = false;
                    objCINLLPResponse.message = ErrorResponseKyc.errorResponse + objCINLLPResponse.error;
                }

            }
            catch (Exception ex)
            {
                objCINLLPResponse.status = false;
                objCINLLPResponse.message = "Error Occurred";
            }
            return objCINLLPResponse;
        }

        public McaSignatoriesResponse Getmcasignatories(string employee_gid, MCASignatories Values)
        {
            McaSignatoriesResponse ObjMcaSignatoriesResponse = new McaSignatoriesResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "mca-signatories");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\", \"cin\" : \"" + Values.cin + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjMcaSignatoriesResponse = JsonConvert.DeserializeObject<McaSignatoriesResponse>(response.Content);
                

                msSQL = "delete from ocs_trn_cadmcasignatories where mcasignatories_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("KPNA");
                msSQL = " insert into ocs_trn_cadmcasignatories(" +
                        " mcasignatories_gid," +
                        " function_gid," +
                        " application_gid," +
                        " cin ," +
                        " validation_status," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + employee_gid + "'," +
                        "'" + Values.function_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + Values.cin + "',";
                if (ObjMcaSignatoriesResponse.statuscode == "101")
                {
                    msSQL += "'" + "Verified" + "',";
                }
                else
                {
                    msSQL += "'" + "Not Verified" + "',";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (ObjMcaSignatoriesResponse.result != null)
                {
                    for (int i = 0; i < ObjMcaSignatoriesResponse.result.Length; i++)
                    {
                        msSQL = " insert into ocs_trn_cadmcasignatorydetails(" +
                       " mcasignatories_gid," +
                       " date_of_appointment," +
                       " designation ," +
                       " dsc_expiry_date," +
                       " wheather_dsc_registered," +
                       " DINDPINPAN," +
                       " full_name," +
                       " address," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + employee_gid + "'," +
                       "'" + ObjMcaSignatoriesResponse.result[i].date_of_appointment + "'," +
                       "'" + ObjMcaSignatoriesResponse.result[i].designation + "'," +
                       "'" + ObjMcaSignatoriesResponse.result[i].dsc_expiry_date + "'," +
                       "'" + ObjMcaSignatoriesResponse.result[i].wheather_dsc_registered + "'," +
                       "'" + ObjMcaSignatoriesResponse.result[i].DINDPINPAN + "'," +
                       "'" + ObjMcaSignatoriesResponse.result[i].full_name + "'," +
                       "'" + ObjMcaSignatoriesResponse.result[i].address + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }

                if (ObjMcaSignatoriesResponse.statuscode == "101")
                {
                    ObjMcaSignatoriesResponse.status = true;
                }
                else
                {
                    ObjMcaSignatoriesResponse.status = false;
                    ObjMcaSignatoriesResponse.message = ErrorResponseKyc.errorResponse + ObjMcaSignatoriesResponse.error;
                }

            }
            catch (Exception ex)
            {
                ObjMcaSignatoriesResponse.status = false;
                ObjMcaSignatoriesResponse.message = "Error Occurred";
            }
            return ObjMcaSignatoriesResponse;
        }

        public UamResponse Getuam(uam Values)
        {
            UamResponse ObjUamResponse = new UamResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "uam");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\", \"uan\" : \"" + Values.uan + "\", \"mobile\" : \"" + Values.mobile + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjUamResponse = JsonConvert.DeserializeObject<UamResponse>(response.Content);
                ObjUamResponse.status = true;

            }
            catch (Exception ex)
            {
                ObjUamResponse.status = false;
                ObjUamResponse.message = "Error Occurred";
            }
            return ObjUamResponse;
        }

        public FSSAIResponse GetFssaidetails(string employee_gid, Fssai Values)
        {
            FSSAIResponse ObjFSSAIResponse = new FSSAIResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "fssai");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\", \"reg_no\" : \"" + Values.reg_no + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjFSSAIResponse = JsonConvert.DeserializeObject<FSSAIResponse>(response.Content);
                

                msSQL = "delete from ocs_trn_tcadfssailicenseauthentication where fssailicenseauthentication_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = " insert into ocs_trn_tcadfssailicenseauthentication(" +
                        " fssailicenseauthentication_gid," +
                        " function_gid," +
                        " application_gid," +
                        " reg_no ," +
                        " fssai_status," +
                        " license_type," +
                        " license_no," +
                        " firm_name," +
                        " address," +
                        " validation_status," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + employee_gid + "'," +
                        "'" + Values.function_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + Values.reg_no + "',";
                if (ObjFSSAIResponse.statuscode == "101")
                {
                    msSQL += "'" + ObjFSSAIResponse.result.Status + "'," +
                             "'" + ObjFSSAIResponse.result.LicType + "'," +
                             "'" + ObjFSSAIResponse.result.LicNO + "'," +
                             "'" + ObjFSSAIResponse.result.FirmName.Replace("'", @"\'") + "'," +
                             "'" + ObjFSSAIResponse.result.Address.Replace("'", @"\'") + "'," +
                             "'" + "Verified" + "',";
                }
                else
                {
                    msSQL += "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "'" + "Not Verified" + "',";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (ObjFSSAIResponse.statuscode == "101")
                {
                    ObjFSSAIResponse.status = true;
                }
                else
                {
                    ObjFSSAIResponse.status = false;
                    ObjFSSAIResponse.message = ErrorResponseKyc.errorResponse + ObjFSSAIResponse.error;
                }

            }
            catch (Exception ex)
            {
                ObjFSSAIResponse.status = false;
                ObjFSSAIResponse.message = "Error Occurred";
            }
            return ObjFSSAIResponse;
        }

        public FDAResponse GetFdadetails(string employee_gid, Fda Values)
        {
            FDAResponse ObjFDAResponse = new FDAResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "fda");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\", \"licence_no\" : \"" + Values.licence_no + "\", \"state\" : \"" + Values.state + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjFDAResponse = JsonConvert.DeserializeObject<FDAResponse>(response.Content);
                
                msSQL = "delete from ocs_trn_tcadfdalicenseauthentication where fdalicenseauthentication_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " insert into ocs_trn_tcadfdalicenseauthentication(" +
                        " fdalicenseauthentication_gid," +
                        " function_gid," +
                        " application_gid," +
                        " license_no ," +
                        " state," +
                        " store_name," +
                        " contact_no," +
                        " license_detail," +
                        " name," +
                        " address," +
                        " validation_status," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                       "'" + employee_gid + "'," +
                        "'" + Values.function_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + Values.licence_no + "'," +
                "'" + Values.state + "',";
                if (ObjFDAResponse.statuscode == "101")
                {
                    msSQL += "'" + ObjFDAResponse.result.store_name + "'," +
                             "'" + ObjFDAResponse.result.contact_number + "'," +
                             "'" + ObjFDAResponse.result.license_detail + "'," +
                             "'" + ObjFDAResponse.result.name + "'," +
                             "'" + ObjFDAResponse.result.address + "'," +
                             "'" + "Verified" + "',";
                }
                else
                {
                    msSQL += "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "'" + "Not Verified" + "',";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (ObjFDAResponse.statuscode == "101")
                {
                    ObjFDAResponse.status = true;
                }
                else
                {
                    ObjFDAResponse.status = false;
                    ObjFDAResponse.message = ErrorResponseKyc.errorResponse + ObjFDAResponse.error;
                }

            }
            catch (Exception ex)
            {
                ObjFDAResponse.status = false;
                ObjFDAResponse.message = "Error Occurred";
            }
            return ObjFDAResponse;
        }

        public EBAResponse GetebaDetails(EBA Values)
        {
            EBAResponse ObjEBAResponse = new EBAResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "elec");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\", \"consumer_id\" : \"" + Values.consumer_id + "\", \"service_provider\" : \"" + Values.service_provider + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjEBAResponse = JsonConvert.DeserializeObject<EBAResponse>(response.Content);
                ObjEBAResponse.status = true;

            }
            catch (Exception ex)
            {
                ObjEBAResponse.status = false;
                ObjEBAResponse.message = "Error Occurred";
            }
            return ObjEBAResponse;
        }

        public TelephoneAuthenticationResponse GetTeleDetails(TelephoneAuthentication Values)
        {
            TelephoneAuthenticationResponse ObjTelephoneAuthenticationResponse = new TelephoneAuthenticationResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "tele");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\", \"tel_no\" : \"" + Values.tel_no + "\", \"city\" : \"" + Values.city + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjTelephoneAuthenticationResponse = JsonConvert.DeserializeObject<TelephoneAuthenticationResponse>(response.Content);
                ObjTelephoneAuthenticationResponse.status = true;

            }
            catch (Exception ex)
            {
                ObjTelephoneAuthenticationResponse.status = false;
                ObjTelephoneAuthenticationResponse.message = "Error Occurred";
            }
            return ObjTelephoneAuthenticationResponse;
        }

        public IfscVerificationResponse GetIfscVerification(string employee_gid, IfscVerification Values)
        {
            IfscVerificationResponse ObjIfscVerificationResponse = new IfscVerificationResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "ifsc");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{\"ifsc\" : \"" + Values.ifsc + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjIfscVerificationResponse = JsonConvert.DeserializeObject<IfscVerificationResponse>(response.Content);
                

                msSQL = "select kycifscauthentication_gid from ocs_trn_tcadkycifscauthentication where function_gid='" + employee_gid + "' and remarks='" + "Active" + "'";

                string kycifscauthentication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update ocs_trn_tcadkycifscauthentication set " +
                        " remarks='" + "Deleted" + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where kycifscauthentication_gid='" + kycifscauthentication_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                msGetGid = objcmnfunctions.GetMasterGID("KIFA");
                msSQL = " insert into ocs_trn_tcadkycifscauthentication(" +
                        " kycifscauthentication_gid," +
                        " function_gid," +
                        " ifsc," +
                        " bank," +
                        " branch," +
                        " address," +
                        " micr," +
                        " validation_status," +
                        " response," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + Values.ifsc + "',";

                if (ObjIfscVerificationResponse.statusCode == "101")
                {
                    msSQL += "'" + ObjIfscVerificationResponse.result.bank + "'," +
                             "'" + ObjIfscVerificationResponse.result.branch + "'," +
                             "'" + ObjIfscVerificationResponse.result.address + "'," +
                             "'" + ObjIfscVerificationResponse.result.micr + "'," +
                             "'" + "Verified" + "'," +
                             "'" + response.Content + "',";
                }
                else
                {
                    msSQL += "''," +
                             "''," +
                             "''," +
                             "''," +
                             "'" + "Not Verified" + "'," +
                             "null,";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (ObjIfscVerificationResponse.statusCode == "101")
                {
                    ObjIfscVerificationResponse.status = true;
                }
                else
                {
                    ObjIfscVerificationResponse.status = false;
                    ObjIfscVerificationResponse.message = ErrorResponseKyc.errorResponse + ObjIfscVerificationResponse.error;
                }


            }
            catch (Exception ex)
            {
                ObjIfscVerificationResponse.status = false;
                ObjIfscVerificationResponse.message = "Error Occurred in IFSC Verification";
            }
            return ObjIfscVerificationResponse;
        }

        public BankAccVerificationResponse GetBankAccVerification(string employee_gid, BankAccVerification Values)
        {
            BankAccVerificationResponse ObjBankAccVerificationResponse = new BankAccVerificationResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "bankacc");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\", \"ifsc\" : \"" + Values.ifsc + "\", \"accountNumber\" : \"" + Values.accountNumber + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjBankAccVerificationResponse = JsonConvert.DeserializeObject<BankAccVerificationResponse>(response.Content);
                

                msSQL = "select kycbankaccverification_gid from ocs_trn_tcadkycbankaccverification where function_gid='" + employee_gid + "' and remarks='" + "Active" + "'";

                string kycbankaccverification_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update ocs_trn_tcadkycbankaccverification set " +
                        " remarks='" + "Deleted" + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where kycbankaccverification_gid='" + kycbankaccverification_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                msGetGid = objcmnfunctions.GetMasterGID("KBAV");
                msSQL = " insert into ocs_trn_tcadkycbankaccverification(" +
                        " kycbankaccverification_gid," +
                        " function_gid," +
                        " ifsc," +
                        " accountNumber," +
                        " accountName," +
                        " validation_status," +
                        " response," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + Values.ifsc + "'," +
                        "'" + Values.accountNumber + "',";

                if (ObjBankAccVerificationResponse.statusCode == "101")
                {
                    msSQL += "'" + ObjBankAccVerificationResponse.result.accountName + "'," +
                             "'" + "Verified" + "'," +
                             "'" + response.Content + "',";

                }
                else
                {
                    msSQL += "''," +
                             "'" + "Not Verified" + "'," +
                             "null,";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (ObjBankAccVerificationResponse.statusCode == "101")
                {
                    ObjBankAccVerificationResponse.status = true;
                }
                else
                {
                    ObjBankAccVerificationResponse.status = false;
                    ObjBankAccVerificationResponse.message = ErrorResponseKyc.errorResponse + ObjBankAccVerificationResponse.error;
                }

            }
            catch (Exception ex)
            {
                ObjBankAccVerificationResponse.status = false;
                ObjBankAccVerificationResponse.message = "Error Occurred in Bank Acc Verification";
            }
            return ObjBankAccVerificationResponse;
        }

        public ITRVOCRResponse GetITRVOCRDetails(HttpRequest httpRequest)
        {
            ITRVOCRResponse ObjITRVOCRResponse = new ITRVOCRResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                HttpFileCollection httpFileCollection;
                httpFileCollection = httpRequest.Files;
                string filename = string.Empty;

                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    HttpPostedFile httpPostedFile = httpFileCollection[i];
                    filename = httpPostedFile.FileName;
                    MemoryStream ms_file = new MemoryStream();
                    httpPostedFile.InputStream.CopyTo(ms_file);
                    ms_file.Position = 0;
                    FileStream file = new FileStream(HttpContext.Current.Server.MapPath("../../files/" + filename), FileMode.Create, FileAccess.Write);
                    ms_file.WriteTo(file);
                    file.Close();
                    ms_file.Close();

                    var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "ocr/itrv");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "multipart/form-data");
                    request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                    request.AddFile(filename, HttpContext.Current.Server.MapPath("../../files/" + filename));
                    IRestResponse response = client.Execute(request);
                    ObjITRVOCRResponse = JsonConvert.DeserializeObject<ITRVOCRResponse>(response.Content);
                    ObjITRVOCRResponse.status = true;
                }



            }
            catch (Exception ex)
            {
                ObjITRVOCRResponse.status = false;
                ObjITRVOCRResponse.message = "Error Occurred";
            }
            return ObjITRVOCRResponse;
        }

        public ChequeOCRResponse GetChequeOCR(HttpRequest httpRequest)
        {
            ChequeOCRResponse ObjChequeOCRResponse = new ChequeOCRResponse();
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            string lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                HttpFileCollection httpFileCollection;
                httpFileCollection = httpRequest.Files;
                string filename = string.Empty;

                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    HttpPostedFile httpPostedFile = httpFileCollection[i];
                    filename = httpPostedFile.FileName;
                    MemoryStream ms_file = new MemoryStream();
                    httpPostedFile.InputStream.CopyTo(ms_file);
                    ms_file.Position = 0;
                    FileStream file = new FileStream(HttpContext.Current.Server.MapPath("../../../erpdocument/" + "/" + lscompany_code + "/" + "Master/ChequeDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + filename), FileMode.Create, FileAccess.Write);
                    ms_file.WriteTo(file);
                    file.Close();
                    ms_file.Close();

                    string image = Convert.ToBase64String(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("../../../erpdocument/" + "/" + lscompany_code + "/" + "Master/ChequeDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + filename)));

                    var client = new RestClient(ConfigurationManager.AppSettings["kycapis2"].ToString() + "ocr/cheque");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "multipart/form-data");
                    request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                    request.AddParameter("application/json", "{ \"fileB64\" : \"" + image + "\" }", ParameterType.RequestBody);

                    IRestResponse response = client.Execute(request);
                    ObjChequeOCRResponse = JsonConvert.DeserializeObject<ChequeOCRResponse>(response.Content);
                    ObjChequeOCRResponse.status = true;
                }



            }
            catch (Exception ex)
            {
                ObjChequeOCRResponse.status = false;
                ObjChequeOCRResponse.message = "Error Occurred";
            }
            return ObjChequeOCRResponse;
        }

        public TANPResponse GetPostTANauthetication(string employee_gid, tan Values)
        {
            TANPResponse objTANPResponse = new TANPResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "tan");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\",  \"tan\" : \"" + Values.tan_no + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                objTANPResponse = JsonConvert.DeserializeObject<TANPResponse>(response.Content);
                

                msSQL = "delete from ocs_trn_tcadtandtl where tandtl_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " insert into ocs_trn_tcadtandtl(" +
                        " tandtl_gid," +
                        " function_gid," +
                        " application_gid," +
                        " tan ," +
                        " name ," +
                        " validation_status," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + employee_gid + "'," +
                        "'" + Values.function_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + Values.tan_no + "',";
                if (objTANPResponse.statuscode == "101")
                {
                    msSQL += "'" + objTANPResponse.result.name + "'," +
                             "'" + "Verified" + "',";
                }
                else
                {
                    msSQL += "''," +
                             "'" + "Not Verified" + "',";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (objTANPResponse.statuscode == "101")
                {
                    objTANPResponse.status = true;
                }
                else
                {
                    objTANPResponse.status = false;
                    objTANPResponse.message = ErrorResponseKyc.errorResponse + objTANPResponse.error;
                }

            }
            catch (Exception ex)
            {
                objTANPResponse.status = false;
                objTANPResponse.message = "Error Occurred";
            }
            return objTANPResponse;
        }

        public iecdetailedPResponse PostIECDetailed(string employee_gid, iec_detailed Values)
        {
            iecdetailedPResponse objiecdetailedPResponse = new iecdetailedPResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "iecdetailed");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\",  \"iec\" : \"" + Values.iec_no + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                objiecdetailedPResponse = JsonConvert.DeserializeObject<iecdetailedPResponse>(response.Content);
                


                msSQL = "delete from ocs_trn_tcadiecdtl where iecdtl_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("KPNA");
                msSQL = " insert into ocs_trn_tcadiecdtl(" +
                        " iecdtl_gid," +
                        " function_gid," +
                        " application_gid," +
                        " iec_no ," +
                        " address," +
                        " iecgate_status," +
                        " pan," +
                        " iec_allotment_date," +
                        " file_number," +
                        " file_date," +
                        " party_name_and_address," +
                        " phone_no," +
                        " e_mail," +
                        " exporter_type," +
                        " iec_status," +
                        " date_of_establishment," +
                        " bin_pan_extension," +
                        " pan_issue_date," +
                        " pan_issued_by," +
                        " nature_of_concern," +
                        " no_of_branches," +
                        " response," +
                        " validation_status," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + employee_gid + "'," +
                        "'" + Values.function_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + Values.iec_no + "',";
                if (objiecdetailedPResponse.statuscode == "101")
                {
                    msSQL += "'" + objiecdetailedPResponse.result.address + "'," +
                             "'" + objiecdetailedPResponse.result.iecgate_status + "'," +
                             "'" + objiecdetailedPResponse.result.pan + "'," +

                             "'" + objiecdetailedPResponse.result.iec_allotment_date + "'," +
                             "'" + objiecdetailedPResponse.result.file_number + "'," +
                             "'" + objiecdetailedPResponse.result.file_date + "'," +
                             "'" + objiecdetailedPResponse.result.party_name_and_address + "'," +
                             "'" + objiecdetailedPResponse.result.phone_no + "'," +
                             "'" + objiecdetailedPResponse.result.e_mail + "'," +
                             "'" + objiecdetailedPResponse.result.exporter_type + "'," +
                             "'" + objiecdetailedPResponse.result.iec_status + "'," +
                             "'" + objiecdetailedPResponse.result.date_of_establishment + "'," +
                             "'" + objiecdetailedPResponse.result.bin_pan_extension + "'," +
                             "'" + objiecdetailedPResponse.result.pan_issue_date + "'," +
                             "'" + objiecdetailedPResponse.result.pan_issued_by + "'," +
                             "'" + objiecdetailedPResponse.result.nature_of_concern + "'," +
                             "'" + objiecdetailedPResponse.result.no_of_branches + "'," +
                             "'" + response.Content + "'," +


                             "'" + "Verified" + "',";
                }
                else
                {
                    msSQL += "''," +
                             "''," +
                             "''," +

                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "''," +
                             "null," +

                             "'" + "Not Verified" + "',";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (objiecdetailedPResponse.statuscode == "101")
                {
                    objiecdetailedPResponse.status = true;
                }
                else
                {
                    objiecdetailedPResponse.status = false;
                    objiecdetailedPResponse.message = ErrorResponseKyc.errorResponse + objiecdetailedPResponse.error;
                }

            }
            catch (Exception ex)
            {
                objiecdetailedPResponse.status = false;
                objiecdetailedPResponse.message = "Error Occurred";
            }
            return objiecdetailedPResponse;
        }

        public ShopAndEstablishmentResponse GetShopAndEstablishment(string employee_gid, ShopAndEstablishmentRequest Values)
        {
            ShopAndEstablishmentResponse ObjShopAndEstablishmentResponse = new ShopAndEstablishmentResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                Values.pdfRequired = "N";

                var client = new RestClient(ConfigurationManager.AppSettings["kycapis2"].ToString() + "shop");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"regNo\" : \"" + Values.regNo + "\",  \"pdfRequired\" : \"" + Values.pdfRequired + "\",  \"areaCode\" : \"" + Values.areaCode + "\",  \"consent\" : \"" + Values.consent + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjShopAndEstablishmentResponse = JsonConvert.DeserializeObject<ShopAndEstablishmentResponse>(response.Content);
                

                msSQL = "delete from ocs_trn_tcadshopandestablishment where shopandestablishment_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " insert into ocs_trn_tcadshopandestablishment(" +
                        " shopandestablishment_gid," +
                        " function_gid," +
                        " application_gid," +
                        " regNo ," +
                        " areaCode ," +
                        " response," +
                        " validation_status," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + employee_gid + "'," +
                        "'" + Values.function_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + Values.regNo + "'," +
                        "'" + Values.areaCode + "',";
                if (ObjShopAndEstablishmentResponse.statusCode == 101)
                {
                    msSQL += "'" + response.Content.Replace("'", "") + "'," +
                             "'" + "Verified" + "',";
                }
                else
                {
                    msSQL += "null," +
                             "'" + "Not Verified" + "',";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (ObjShopAndEstablishmentResponse.statusCode == 101)
                {
                    ObjShopAndEstablishmentResponse.status = true;
                }
                else
                {
                    ObjShopAndEstablishmentResponse.status = false;
                    ObjShopAndEstablishmentResponse.message = ErrorResponseKyc.errorResponse + ObjShopAndEstablishmentResponse.error;
                }

            }
            catch (Exception ex)
            {
                ObjShopAndEstablishmentResponse.status = false;
                ObjShopAndEstablishmentResponse.message = "Error Occurred";
            }
            return ObjShopAndEstablishmentResponse;
        }

        public PropertyTaxResponse GetPropertyTax(string employee_gid, PropertyTaxRequest Values)
        {
            PropertyTaxResponse ObjPropertyTaxResponse = new PropertyTaxResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";

                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "property-tax");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"state\" : \"" + Values.state + "\",  \"city\" : \"" + Values.city + "\",  \"propertyNo\" : \"" + Values.propertyNo + "\",  \"district\" : \"" + Values.district + "\", \"ulb\" : \"" + Values.ulb + "\", \"consent\" : \"" + Values.consent + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjPropertyTaxResponse = JsonConvert.DeserializeObject<PropertyTaxResponse>(response.Content);
                

                msSQL = "delete from ocs_trn_tcadpropertytax where propertytax_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " insert into ocs_trn_tcadpropertytax(" +
                        " propertytax_gid," +
                        " function_gid," +
                        " application_gid," +
                        " propertyNo ," +
                        " city ," +
                        " state ," +
                        " district ," +
                        " ulb ," +
                        " response," +
                        " validation_status," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + employee_gid + "'," +
                        "'" + Values.function_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + Values.propertyNo + "'," +
                        "'" + Values.city + "'," +
                        "'" + Values.state + "'," +
                        "'" + Values.district + "'," +
                        "'" + Values.ulb + "',";
                if (ObjPropertyTaxResponse.statusCode == 101)
                {
                    msSQL += "'" + response.Content + "'," +
                             "'" + "Verified" + "',";
                }
                else
                {
                    msSQL += "null," +
                             "'" + "Not Verified" + "',";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (ObjPropertyTaxResponse.statusCode == 101)
                {
                    ObjPropertyTaxResponse.status = true;
                }
                else
                {
                    ObjPropertyTaxResponse.status = false;
                    ObjPropertyTaxResponse.message = ErrorResponseKyc.errorResponse + ObjPropertyTaxResponse.error;
                }

            }
            catch (Exception ex)
            {
                ObjPropertyTaxResponse.status = false;
                ObjPropertyTaxResponse.message = "Error Occurred";
            }
            return ObjPropertyTaxResponse;
        }

        public VehicleRCAuthAdvancedResponse GetVehicleRCAuthAdvanced(string employee_gid, VehicleRCAuthAdvancedRequest Values)
        {
            VehicleRCAuthAdvancedResponse ObjVehicleRCAuthAdvancedResponse = new VehicleRCAuthAdvancedResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                Values.version = 3.1;

                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "rc-advanced");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"registrationNumber\" : \"" + Values.registrationNumber + "\",  \"consent\" : \"" + Values.consent + "\",  \"version\" : \"" + Values.version + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjVehicleRCAuthAdvancedResponse = JsonConvert.DeserializeObject<VehicleRCAuthAdvancedResponse>(response.Content);
                

                msSQL = "delete from ocs_trn_tcadvehiclercauthadvanced where vehiclercauthadvanced_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " insert into ocs_trn_tcadvehiclercauthadvanced(" +
                        " vehiclercauthadvanced_gid," +
                        " function_gid," +
                        " application_gid," +
                        " registrationNumber ," +
                        " response," +
                        " validation_status," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + employee_gid + "'," +
                        "'" + Values.function_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + Values.registrationNumber + "',";
                if (ObjVehicleRCAuthAdvancedResponse.statusCode == 101)
                {
                    msSQL += "'" + response.Content + "'," +
                             "'" + "Verified" + "',";
                }
                else
                {
                    msSQL += "null," +
                             "'" + "Not Verified" + "',";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (ObjVehicleRCAuthAdvancedResponse.statusCode == 101)
                {
                    ObjVehicleRCAuthAdvancedResponse.status = true;
                }
                else
                {
                    ObjVehicleRCAuthAdvancedResponse.status = false;
                    ObjVehicleRCAuthAdvancedResponse.message = ErrorResponseKyc.errorResponse + ObjVehicleRCAuthAdvancedResponse.error;
                }

            }
            catch (Exception ex)
            {
                ObjVehicleRCAuthAdvancedResponse.status = false;
                ObjVehicleRCAuthAdvancedResponse.message = "Error Occurred";
            }
            return ObjVehicleRCAuthAdvancedResponse;
        }

        public VehicleRCSearchResponse GetVehicleRCSearch(string employee_gid, VehicleRCSearchRequest Values)
        {
            VehicleRCSearchResponse ObjVehicleRCSearchResponse = new VehicleRCSearchResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";

                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "rcsearch");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\",  \"engine_no\" : \"" + Values.engine_no + "\",  \"chassis_no\" : \"" + Values.chassis_no + "\", \"state\" : \"" + Values.state + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjVehicleRCSearchResponse = JsonConvert.DeserializeObject<VehicleRCSearchResponse>(response.Content);
                

                msSQL = "delete from ocs_trn_tcadvehiclercsearch where vehiclercsearch_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " insert into ocs_trn_tcadvehiclercsearch(" +
                        " vehiclercsearch_gid," +
                        " function_gid," +
                        " application_gid," +
                        " engine_no ," +
                        " chassis_no ," +
                        " state ," +
                        " response," +
                        " validation_status," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + employee_gid + "'," +
                        "'" + Values.function_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + Values.engine_no + "'," +
                        "'" + Values.chassis_no + "'," +
                        "'" + Values.state + "',";
                if (ObjVehicleRCSearchResponse.statuscode == "101")
                {
                    msSQL += "'" + response.Content + "'," +
                             "'" + "Verified" + "',";
                }
                else
                {
                    msSQL += "null," +
                             "'" + "Not Verified" + "',";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (ObjVehicleRCSearchResponse.statuscode == "101")
                {
                    ObjVehicleRCSearchResponse.status = true;
                }
                else
                {
                    ObjVehicleRCSearchResponse.status = false;
                    ObjVehicleRCSearchResponse.message = ErrorResponseKyc.errorResponse + ObjVehicleRCSearchResponse.error;
                }

            }
            catch (Exception ex)
            {
                ObjVehicleRCSearchResponse.status = false;
                ObjVehicleRCSearchResponse.message = "Error Occurred";
            }
            return ObjVehicleRCSearchResponse;
        }

        public LPGIDAuthenticationResponse GetLPGIDAuthentication(string employee_gid, LPGIDAuthenticationRequest Values)
        {
            LPGIDAuthenticationResponse ObjLPGIDAuthenticationResponse = new LPGIDAuthenticationResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";

                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "lpg");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + Values.consent + "\",  \"lpg_id\" : \"" + Values.lpg_id + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjLPGIDAuthenticationResponse = JsonConvert.DeserializeObject<LPGIDAuthenticationResponse>(response.Content);
                

                msSQL = "delete from ocs_trn_tcadlpgiddtl where ocs_trn_tlpgiddtl='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " insert into ocs_trn_tcadlpgiddtl(" +
                        " lpgiddtl_gid," +
                        " function_gid," +
                        " application_gid," +
                        " lpg_id ," +
                        " response," +
                        " validation_status," +
                        " remarks," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + employee_gid + "'," +
                        "'" + Values.function_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + Values.lpg_id + "',";
                if (ObjLPGIDAuthenticationResponse.statuscode == 101)
                {
                    msSQL += "'" + response.Content + "'," +
                             "'" + "Verified" + "',";
                }
                else
                {
                    msSQL += "null," +
                             "'" + "Not Verified" + "',";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (ObjLPGIDAuthenticationResponse.statuscode == 101)
                {
                    ObjLPGIDAuthenticationResponse.status = true;
                }
                else
                {
                    ObjLPGIDAuthenticationResponse.status = false;
                    ObjLPGIDAuthenticationResponse.message = ErrorResponseKyc.errorResponse + ObjLPGIDAuthenticationResponse.error;
                }

            }
            catch (Exception ex)
            {
                ObjLPGIDAuthenticationResponse.status = false;
                ObjLPGIDAuthenticationResponse.message = "Error Occurred";
            }
            return ObjLPGIDAuthenticationResponse;
        }


    }
}