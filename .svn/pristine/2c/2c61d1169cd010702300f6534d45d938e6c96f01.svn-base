using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using ems.masterng.Models;
using ems.utilities.Functions;
using System.Data;
using System.Data.Odbc;
using System.Text;
using System.Net;
using ems.masterng.DataAccess;
using System.Text.RegularExpressions;
using System.Threading;

namespace ems.masterng.DataAccess
{
    public class DaKycNg
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid;
        int mnResult;
        string lsgender_gid, lsgender_name;
        string loglspath = "", logFileName = "";
        DaGoogleMapsAPINg objDaGoogleMapsAPINg = new DaGoogleMapsAPINg();

        public CINFromPANResponse GetCINFromPAN(string employee_gid, PanNumberModel Values)
        {
            CINFromPANResponse ObjCINFromPANResponse = new CINFromPANResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Values.consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapikscanurl"].ToString() + "pan-cin");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapikscankey"].ToString());
                request.AddParameter("application/json", "{\"pan\":\"" + Values.pan + "\"}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjCINFromPANResponse = JsonConvert.DeserializeObject<CINFromPANResponse>(response.Content);


                msSQL = "select kycpantocin_gid from ocs_trn_tkycpantocin where application_gid='" + employee_gid + "' and record_status='" + "Active" + "'";
                string kycpantocin_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update ocs_trn_tkycpantocin set " +
                        " record_status='" + "Deleted" + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where kycpantocin_gid='" + kycpantocin_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("KPTC");
                msSQL = " insert into ocs_trn_tkycpantocin(" +
                        " kycpantocin_gid," +
                        " application_gid," +
                        " pan_no," +
                        " name," +
                        " cin," +
                        " validation_status," +
                        " record_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + Values.pan + "',";
                if (ObjCINFromPANResponse.statusCode == 101)
                {
                    msSQL += "'" + ObjCINFromPANResponse.result[0].name.Replace("'", "\\'") + "'," +
                             "'" + ObjCINFromPANResponse.result[0].entityId + "'," +
                             "'" + "Verified" + "',";
                }
                else
                {
                    msSQL += "''," +
                             "''," +
                             "'" + "Not Verified" + "',";
                }

                msSQL += "'" + "Active" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                if (ObjCINFromPANResponse.statusCode == 101)
                {
                    HttpContext ctx = HttpContext.Current;

                    Thread tDirector = new Thread(new ThreadStart(() =>
                    {
                        HttpContext.Current = ctx;
                        GetDirectorsListFromCIN(ObjCINFromPANResponse.result[0].entityId, employee_gid);
                    }));

                    tDirector.Start();

                    Thread tGST = new Thread(new ThreadStart(() =>
                    {
                        HttpContext.Current = ctx;
                        GetGSTSBPANDetails(Values.pan, employee_gid);
                    }));

                    tGST.Start();

                    ObjCINFromPANResponse.status = true;
                }
                else
                {
                    ObjCINFromPANResponse.status = false;
                    ObjCINFromPANResponse.message = ErrorResponseKyc.errorResponse + ObjCINFromPANResponse.error;
                }


            }
            catch (Exception ex)
            {
                ObjCINFromPANResponse.status = false;
                ObjCINFromPANResponse.message = "Error Occurred in PAN Validation";
            }
            return ObjCINFromPANResponse;
        }

        public void GetDirectorsListFromCIN(string cin, string employee_gid)
        {
            DirectorsListResponse objDirectorsListResponse = new DirectorsListResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                string consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis1"].ToString() + "mca-signatories");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{\"consent\":\"" + consent + "\",\"cin\":\"" + cin + "\"}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                objDirectorsListResponse = JsonConvert.DeserializeObject<DirectorsListResponse>(response.Content);

                msSQL = "select kycdirectorslistfromcin_gid from ocs_trn_tkycdirectorslistfromcin where institution_gid='" + employee_gid + "' and record_status='" + "Active" + "'";

                string kycdirectorslistfromcin_gid = objdbconn.GetExecuteScalar(msSQL);

                if (!String.IsNullOrEmpty(kycdirectorslistfromcin_gid))
                {
                    msSQL = " update ocs_trn_tkycdirectorslistfromcin set " +
                        " record_status='" + "Deleted" + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where kycdirectorslistfromcin_gid='" + kycdirectorslistfromcin_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msGetGid = objcmnfunctions.GetMasterGID("KDFC");
                msSQL = " insert into ocs_trn_tkycdirectorslistfromcin(" +
                        " kycdirectorslistfromcin_gid," +
                        " institution_gid," +
                        " cin," +
                        " validation_status," +
                        " response," +
                        " record_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + cin + "',";
                if (objDirectorsListResponse.statuscode == "101")
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

                if (objDirectorsListResponse.statuscode == "101")
                {
                    bool directorDetailResult;
                    int directorDetailResultCount = 0;

                    foreach (var item in objDirectorsListResponse.result)
                    {
                        directorDetailResult = GetDirectorDetailFromDIN(item.DINDPINPAN, employee_gid, cin);
                        if (directorDetailResult == true)
                            directorDetailResultCount++;
                    }

                    
                    if(directorDetailResultCount == objDirectorsListResponse.result.Length)
                    {
                        LogForAuditKYCAPI("Director details inserted successfully..!");
                    }
                    else
                    {
                        LogForAuditKYCAPI("Error occurred in fetching and inserting director details");

                        msSQL = "delete from ocs_mst_tcontact where employee_gid = '" + employee_gid + "'";                             
                       mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }

                }

            }
            catch (Exception ex)
            {

            }


        }

        public bool GetDirectorDetailFromDIN(string din, string employee_gid, string cin)
        {

            try
            {
                DirectorDetailResponse objDirectorDetailResponse = new DirectorDetailResponse();

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                DirectorDetailRequest objDirectorDetailRequest = new DirectorDetailRequest();
                objDirectorDetailRequest.id = din;
                objDirectorDetailRequest.name = "";
                objDirectorDetailRequest.address = "";
                objDirectorDetailRequest.fatherName = "";
                objDirectorDetailRequest.dateOfInception = "";

                string DirectorDetailRequestJSON = JsonConvert.SerializeObject(objDirectorDetailRequest);

                var client = new RestClient(ConfigurationManager.AppSettings["kycapikscanurl"].ToString() + "din-details");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapikscankey"].ToString());
                request.AddParameter("application/json", DirectorDetailRequestJSON, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                objDirectorDetailResponse = JsonConvert.DeserializeObject<DirectorDetailResponse>(response.Content);

                msSQL = "select kycdirectordetailfromdin_gid from ocs_trn_tkycdirectordetailfromdin where institution_gid='" + employee_gid + "' and record_status='" + "Active" + "'";

                string kycdirectordetailfromdin_gid = objdbconn.GetExecuteScalar(msSQL);

                if (!String.IsNullOrEmpty(kycdirectordetailfromdin_gid))
                {
                    msSQL = " update ocs_trn_tkycdirectordetailfromdin set " +
                        " record_status='" + "Deleted" + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where kycdirectordetailfromdin_gid='" + kycdirectordetailfromdin_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                }

                msGetGid = objcmnfunctions.GetMasterGID("KDDD");
                msSQL = " insert into ocs_trn_tkycdirectordetailfromdin(" +
                        " kycdirectordetailfromdin_gid," +
                        " institution_gid," +
                        " din," +
                        " validation_status," +
                        " response," +
                        " record_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + din + "',";
                if (objDirectorDetailResponse.statusCode == 101)
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

                if (objDirectorDetailResponse.statusCode == 101)
                {
                    foreach (var item in objDirectorDetailResponse.result)
                    {
                        if (item.dinPan_ == din)
                        {
                            DirectorDetailForIndividual objDirectorDetailForIndividual = new DirectorDetailForIndividual();

                            foreach (var itemIndividual in item.entities)
                            {
                                if (itemIndividual.entityId == cin)
                                {
                                    objDirectorDetailForIndividual.entityName = itemIndividual.entityName;
                                    break;
                                }
                            }

                            objDirectorDetailForIndividual.firstName = item.firstName;
                            objDirectorDetailForIndividual.middleName = item.middleName;
                            objDirectorDetailForIndividual.lastName = item.lastName;

                            objDirectorDetailForIndividual.fathersFirstName = item.fathersFirstName;
                            objDirectorDetailForIndividual.fathersMiddleName = item.fathersMiddleName;
                            objDirectorDetailForIndividual.fathersLastName = item.fathersLastName;
                            objDirectorDetailForIndividual.lastName = item.lastName;
                            objDirectorDetailForIndividual.pan = item.pans[0];
                            objDirectorDetailForIndividual.dateOfBirth = "25-06-1932";
                            // objDirectorDetailForIndividual.dateOfBirth = DateTime.ParseExact(item.dateOfBirth, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                            msSQL = "select gender_gid,gender_name from ocs_mst_tgender where gender_name='" + item.gender + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                objDirectorDetailForIndividual.gender_gid = objODBCDatareader["gender_gid"].ToString();
                                objDirectorDetailForIndividual.gender_name = objODBCDatareader["gender_name"].ToString();
                            }

                            msGetGid = objcmnfunctions.GetMasterGID("CTCT");

                            msSQL = " insert into ocs_mst_tcontact(" +
                            " contact_gid," +
                            " pan_no," +
                            " first_name," +
                            " middle_name," +
                            " last_name," +
                            " individual_dob," +
                            //" age," +
                            " gender_gid," +
                            " gender_name," +

                            " father_firstname," +
                            " father_middlename," +
                            " father_lastname," +

                            // " institution_gid," +
                            " institution_name," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + objDirectorDetailForIndividual.pan + "'," +
                            "'" + objDirectorDetailForIndividual.firstName + "'," +
                            "'" + objDirectorDetailForIndividual.middleName + "'," +
                            "'" + objDirectorDetailForIndividual.lastName + "'," +
                            "'" + objDirectorDetailForIndividual.dateOfBirth + "'," +
                            "'" + objDirectorDetailForIndividual.gender_gid + "'," +
                            "'" + objDirectorDetailForIndividual.gender_name + "'," +
                            "'" + objDirectorDetailForIndividual.fathersFirstName + "'," +
                            "'" + objDirectorDetailForIndividual.fathersMiddleName + "'," +
                            "'" + objDirectorDetailForIndividual.fathersLastName + "'," +
                            "'" + objDirectorDetailForIndividual.entityName + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult == 1)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public void GetGSTSBPANDetails(string pan_no, string employee_gid)
        {
            GSTSBPANDetailsResponse ObjGSTSBPANDetailsResponse = new GSTSBPANDetailsResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                string consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis3"].ToString() + "search");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + consent + "\",  \"pan\" : \"" + pan_no + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjGSTSBPANDetailsResponse = JsonConvert.DeserializeObject<GSTSBPANDetailsResponse>(response.Content);

                msSQL = "select kycgstsbpan_gid from ocs_mst_tkycgstsbpan where function_gid='" + employee_gid + "' and remarks='" + "Active" + "'";
                string kycgstsbpan_gid = objdbconn.GetExecuteScalar(msSQL);

                List<string> gstList = new List<string>();
                string gstListString = String.Empty;

                if (!String.IsNullOrEmpty(kycgstsbpan_gid))
                {
                    msSQL = " update ocs_mst_tkycgstsbpan set " +
                        " remarks='" + "Deleted" + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where kycgstsbpan_gid='" + kycgstsbpan_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                    

                msGetGid = objcmnfunctions.GetMasterGID("KGSP");
                msSQL = " insert into ocs_mst_tkycgstsbpan(" +
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
                        "'" + pan_no + "',";
                if (ObjGSTSBPANDetailsResponse.statusCode == 101)
                {

                    for (int i = 0; i < ObjGSTSBPANDetailsResponse.result.Length; i++)
                    {
                        string gstTemp = "";
                        gstTemp += ObjGSTSBPANDetailsResponse.result[i].gstinId;
                        gstList.Add(gstTemp);

                        gstListString += string.Join(",", gstList.ToArray());
                    }

                    msSQL += "'" + gstListString + "'," +
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

               

                    bool gstDetailResult;
                    int gstDetailResultCount = 0;

                    bool gstPostResult;
                    int gstPostResultCount = 0;

                    foreach (var item in gstList)
                    {
                        gstPostResult = PostKYCInstitutionGST(item, employee_gid);
                        if (gstPostResult == true)
                            gstPostResultCount++;

                        gstDetailResult = GetAddressDetailFromGST(item, employee_gid);
                        if (gstDetailResult == true)
                            gstDetailResultCount++;
                    }

                    if (gstDetailResultCount == gstList.Count)
                    {
                        LogForAuditKYCAPI("All GSTs for Institution posted");
                    }
                    else
                    {
                        LogForAuditKYCAPI("Error occurred in posting institution GSTs");

                        msSQL = "delete from ocs_mst_tinstitution2branch where employee_gid = '" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }

                    if (gstDetailResultCount == gstList.Count)
                    {
                        LogForAuditKYCAPI("Addresses obtained for all the GSTs");
                    }
                    else
                    {
                        LogForAuditKYCAPI("Error occurred in obtaining address for GSTs");

                        msSQL = "delete from ocs_mst_tinstitution2address where employee_gid = '" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }


                    LogForAuditKYCAPI("GSTs for PAN obtained successfully");
                }
                else
                {
                    LogForAuditKYCAPI("Error occurred in obtaining GSTs for PAN");
                }

            }
            catch (Exception ex)
            {
                LogForAuditKYCAPI("Exception occurred in obtaining GSTs for PAN");
            }
            
        }

        public bool GetAddressDetailFromGST(string gst_no, string employee_gid)
        {
            GSTVerificationResponse objGSTVerificationResponse = new GSTVerificationResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                string consent = "Y";
                var client = new RestClient(ConfigurationManager.AppSettings["kycapis3"].ToString() + "gst-verification");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapiskey"].ToString());
                request.AddParameter("application/json", "{ \"consent\" : \"" + consent + "\",  \"gstin\" : \"" + gst_no + "\" }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                objGSTVerificationResponse = JsonConvert.DeserializeObject<GSTVerificationResponse>(response.Content);

                msSQL = "select gstaddressdetail_gid from ocs_trn_tgstaddressdetail where institution_gid='" + employee_gid + "' and remarks='" + "Active" + "'";

                string gstaddressdetail_gid = objdbconn.GetExecuteScalar(msSQL);

                if (!string.IsNullOrEmpty(gstaddressdetail_gid))
                {
                    msSQL = " update ocs_trn_tgstaddressdetail set " +
                        " remarks='" + "Deleted" + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where gstaddressdetail_gid='" + gstaddressdetail_gid + "' ";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }



                msGetGid = objcmnfunctions.GetMasterGID("KGSA");
                msSQL = " insert into ocs_trn_tkycgstaddressdetail(" +
                        " kycgstaddressdetail_gid ," +
                        " institution_gid," +
                        " gst_no," +
                        " validation_status," +
                        " response," +
                        " record_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + gst_no + "',";

                if (objGSTVerificationResponse.statusCode == 101)
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

                string primaryAddress = string.Empty;

                primaryAddress = objGSTVerificationResponse.result.pradr.adr;
                List<string> additionalAddressList = new List<string>();

                for (int i = 0; i < objGSTVerificationResponse.result.adadr.Length; i++)
                {
                    additionalAddressList.Add(objGSTVerificationResponse.result.adadr[i].adr);
                }

                additionalAddressList.Add(primaryAddress); //primary address becomes just another address; choosing primary address left to user

                foreach (var item in additionalAddressList)
                {
                    MdlAddressPINDetails objMdlAddressPINDetailsResp = new MdlAddressPINDetails();
                    string postal_code = string.Empty;
                    string pattern = @"pin: [1-9]{1}[0-9]{2}[0-9]{3}";

                    Match m = Regex.Match(item, pattern, RegexOptions.IgnoreCase);
                    if (m.Success)
                    {
                        postal_code = m.Value.Substring(5, 6);
                    }

                    objMdlAddressPINDetailsResp = GetAddressPostalCodeDetails(postal_code);

                    GeoCodingResponse objGeoCodingResponse = new GeoCodingResponse();
                    objGeoCodingResponse = objDaGoogleMapsAPINg.DaGetGeoCoding(item);

                    string latitude = objGeoCodingResponse.results[0].geometry.location.lat;
                    string longitude = objGeoCodingResponse.results[0].geometry.location.lng;


                    msSQL = " insert into ocs_mst_tinstitution2address(" +
                  " institution2address_gid," +
                  " institution_gid," +
                  " addressline1," +
                  " apifetch_flag," +
                  " primary_status," +
                  " postal_code," +
                  " city," +
                  " taluka," +
                  " district," +
                  " state," +
                  " country," +
                  " latitude," +
                  " longitude," +
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + employee_gid + "'," +
                  "'" + item + "'," +
                  "'Y'," +
                  "'" + "No" + "'," +
                  "'" + postal_code + "'," +
                  "'" + objMdlAddressPINDetailsResp.city + "'," +
                  "'" + objMdlAddressPINDetailsResp.taluka + "'," +
                  "'" + objMdlAddressPINDetailsResp.district + "'," +
                  "'" + objMdlAddressPINDetailsResp.state + "'," +
                  "'" + "India" + "'," +
                  "'" + latitude + "'," +
                  "'" + longitude + "'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

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


                msSQL = "select kycpanauthentication_gid from ocs_mst_tkycpanauthentication where function_gid='" + employee_gid + "' and remarks='" + "Active" + "'";

                string kycpanauthentication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update ocs_mst_tkycpanauthentication set " +
                        " remarks='" + "Deleted" + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where kycpanauthentication_gid='" + kycpanauthentication_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("KPNA");
                msSQL = " insert into ocs_mst_tkycpanauthentication(" +
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
                    msSQL += "'" + ObjPanNumberResponse.result.name.Replace("'", "") + "'," +
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

        public LEINumberResponse GetLEINumberDetails(string employee_gid, PanNumberModel Values)
        {
            LEINumberResponse ObjLEINumberResponse = new LEINumberResponse();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var client = new RestClient(ConfigurationManager.AppSettings["kycapikscanurllei"].ToString() + "lei-details");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("x-karza-key", ConfigurationManager.AppSettings["kycapikscankey"].ToString());
                request.AddParameter("application/json", "{\"id\":\"" + Values.pan + "\"}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                ObjLEINumberResponse = JsonConvert.DeserializeObject<LEINumberResponse>(response.Content);


                msSQL = "select kycleifrompan_gid from ocs_trn_tkycleifrompan where function_gid='" + employee_gid + "' and record_status='" + "Active" + "'";

                string kycleifrompan_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update ocs_trn_tkycleifrompan set " +
                        " record_status='" + "Deleted" + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where kycleifrompan_gid='" + kycleifrompan_gid + "' ";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("KLFP");
                msSQL = " insert into ocs_trn_tkycleifrompan(" +
                        " kycleifrompan_gid," +
                        " function_gid," +
                        " pan_no," +
                        " pan_name," +
                        " validation_status," +
                        " record_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + Values.pan + "',";
                if (ObjLEINumberResponse.statusCode == 101)
                {
                    msSQL += "'" + ObjLEINumberResponse.result[0].lei.Replace("'", "") + "'," +
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



                if (ObjLEINumberResponse.statusCode == 101)
                {
                    ObjLEINumberResponse.status = true;
                }
                else
                {
                    ObjLEINumberResponse.status = false;
                    ObjLEINumberResponse.message = ErrorResponseKyc.errorResponse + ObjLEINumberResponse.error;
                }



            }
            catch (Exception ex)
            {
                ObjLEINumberResponse.status = false;
                ObjLEINumberResponse.message = "Error Occurred in PAN Validation";
            }
            return ObjLEINumberResponse;
        }

        //Auxillary Functions
        public string fetchCompanyCode()
        {
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            string lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            return lscompany_code;
        }

        public void LogForAuditKYCAPI(string strVal)
        {
            try
            {
                loglspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + fetchCompanyCode() + "/" + "SamAgro/Master/KYCAPIV2/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                if ((!System.IO.Directory.Exists(loglspath)))
                    System.IO.Directory.CreateDirectory(loglspath);

                if (logFileName == "")
                {
                    logFileName = "Log_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
                }

                loglspath = loglspath + logFileName;

                System.IO.StreamWriter sw = new System.IO.StreamWriter(loglspath, true);
                sw.WriteLine(strVal);
                sw.Close();

            }
            catch (Exception ex)
            {

            }
        }

        public MdlAddressPINDetails GetAddressPostalCodeDetails(string postal_code)
        {
            MdlAddressPINDetails objMdlAddressPINDetails = new MdlAddressPINDetails();
            try
            {

                msSQL = "select city,taluka,district, state from ocs_mst_tpostalcode where " +
                        " postalcode_value='" + postal_code + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        objMdlAddressPINDetails.city = (dr_datarow["city"].ToString());
                        objMdlAddressPINDetails.taluka = (dr_datarow["taluka"].ToString());
                        objMdlAddressPINDetails.district = (dr_datarow["district"].ToString());
                        objMdlAddressPINDetails.state = (dr_datarow["state"].ToString());
                    }

                }
                return objMdlAddressPINDetails;
            }
            catch
            {
                return objMdlAddressPINDetails;
            }
        }

        public bool PostKYCInstitutionGST(string gst_no, string employee_gid)
        {

            string GSTValue, GSTStateCode, GSTState;

                GSTValue = gst_no;
                GSTStateCode = GSTValue.Substring(0, 2);

                msSQL = "select gst_state from ocs_mst_tgstcode2state where " +
                       " gst_code='" + GSTStateCode + "'";
                GSTState = objdbconn.GetExecuteScalar(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("ITGS");
                msSQL = " insert into ocs_mst_tinstitution2branch(" +
                    " institution2branch_gid," +
                    " institution_gid," +
                    " gst_state," +
                    " gst_no," +
                    " gst_registered," +
                    " headoffice_status, " +
                    " apifetch_flag," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + GSTState + "'," +
                    "'" + GSTValue + "'," +
                    "'" + "Yes" + "'," +
                    "'No'," +
                    "'Y'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            

            if (mnResult != 0)
            {
              
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}