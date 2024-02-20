using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ems.utilities.Functions;
using ems.utilities.Models;
using StoryboardAPI.Models;
using StoryboardAPI.Authorization;
using System.Data;
using System.Data.Odbc;
using Newtonsoft.Json;
using RestSharp;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spire.Pdf;

namespace StoryboardAPI.Controllers
{
    [RoutePrefix("api/Login")]
    [AllowAnonymous]
    public class LoginController : ApiController
   	{
        dbconn objdbconn = new dbconn();
        OdbcDataReader objODBCdatareader;
        cmnfunctions objcmnfunctions = new cmnfunctions();
        
        string msSQL = string.Empty;
        int mnResult;
        string user_status;
        string vendoruser_status;
        string tokenvalue = string.Empty;
        string user_gid = string.Empty;
        string employee_gid = string.Empty;
        string department_gid = string.Empty;
        string password = string.Empty;
        string username = string.Empty;
        string departmentname = string.Empty;
        string lscompany_code;
        string domain = string.Empty;
        string lsexpiry_time;
        DataTable dt_datatable;

        // Validate Token

        [AllowAnonymous]
        [ActionName("LoginReturn")]
        [HttpPost]
        public HttpResponseMessage GetLoginReturn(logininput values)
        {
            var url = ConfigurationManager.AppSettings["host"];
            if (url == ConfigurationManager.AppSettings["livedomain_url"].ToString())
            {
                var getSpireDocLicense = ConfigurationManager.AppSettings["SpireDocLicenseKey"];
                Spire.License.LicenseProvider.SetLicenseKey(getSpireDocLicense);
            } 

            loginresponse GetLoginResponse = new loginresponse();
            string code = values.code; 
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;  
            var client = new RestSharp.RestClient("https://login.microsoftonline.com/655a0e0e-4a74-4a0c-86d8-370a992e90a6/oauth2/v2.0/token");
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("client_id", ConfigurationManager.AppSettings["client_id"]);
            request.AddParameter("code", code);
            request.AddParameter("scope", "https://graph.microsoft.com/User.Read");
            request.AddParameter("client_secret", ConfigurationManager.AppSettings["client_secret"]);
            request.AddParameter("redirect_uri", ConfigurationManager.AppSettings["redirect_url"]);
            request.AddParameter("grant_type", "authorization_code");
            IRestResponse response = client.Execute(request);
            token json = JsonConvert.DeserializeObject<token>(response.Content);

            var client1 = new RestSharp.RestClient("https://graph.microsoft.com/v1.0/me");
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var request1 = new RestRequest(Method.GET);
            request1.AddHeader("Authorization", "Bearer " + json.access_token);
            IRestResponse response1 = client1.Execute(request1);
            Rootobject json1 = JsonConvert.DeserializeObject<Rootobject>(response1.Content);
            object lsDBmobilePhone;
            
            if (json1.userPrincipalName != null && json1.userPrincipalName != "")
            {
                msSQL = " SELECT b.user_gid,a.department_gid, a.employee_gid, user_password, user_code, a.employee_mobileno, concat(user_firstname, ' ', user_lastname) as username FROM hrm_mst_temployee a " +
                        " INNER JOIN adm_mst_tuser b on b.user_gid = a.user_gid " +
                        " WHERE employee_emailid = '" + json1.userPrincipalName + "' and b.user_status = 'Y'";
                objODBCdatareader = objdbconn.GetDataReader(msSQL);
                
                if (objODBCdatareader.HasRows == true)
                {

                    objODBCdatareader.Read();
                    var tokenresponse = Token(objODBCdatareader["user_code"].ToString(), objODBCdatareader["user_password"].ToString());
                    dynamic newobj = Newtonsoft.Json.JsonConvert.DeserializeObject(tokenresponse);
                    tokenvalue = newobj.access_token;
                    employee_gid = objODBCdatareader["employee_gid"].ToString();
                    user_gid = objODBCdatareader["user_gid"].ToString();
                    department_gid = objODBCdatareader["department_gid"].ToString();
                    GetLoginResponse.username = objODBCdatareader["username"].ToString();
                    lsDBmobilePhone = objODBCdatareader["employee_mobileno"].ToString();
                    objODBCdatareader.Close(); 
                }
                else
                    objODBCdatareader.Close();

                msSQL = " INSERT INTO adm_mst_ttoken ( " +
                         " token, " +
                         " employee_gid, " +
                         " user_gid, " +
                         " department_gid, " +
                         " company_code " +
                         " )VALUES( " +
                         " 'Bearer " + tokenvalue + "'," +
                         " '" + employee_gid + "'," +
                         " '" + user_gid + "'," +
                         " '" + department_gid + "'," +
                         " '" + ConfigurationManager.AppSettings["company_code"] + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                GetLoginResponse.status = true;
                GetLoginResponse.message = "";
                GetLoginResponse.token = "Bearer " + tokenvalue;
                GetLoginResponse.user_gid = user_gid;
                
            }
            else
            {
                GetLoginResponse.user_gid = null; 
            }
            return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
        }

        public void LogSSOLoginReturn(string mobno_errlog)
        {
            try
            {
                string lspath = ConfigurationManager.AppSettings["file_path"].ToString() + "/erpdocument/SSOMOB_ERRLOG/" + DateTime.Now.Year + @"\" + DateTime.Now.Month;
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);

                lspath = lspath + @"\" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(lspath, true);
                sw.WriteLine(mobno_errlog);
                sw.Close();
            }
            catch (Exception ex)
            {
            }
        }

        //public void LogSSOLoginReturn_2(IRestResponse Response, IRestResponse Response1, Rootobject ResponseContent1)
        //{
        //    try
        //    {

        //        string lspath = ConfigurationManager.AppSettings["file_path"].ToString() + "/erpdocument/SSOMOB_ERRLOG_2/" + DateTime.Now.Year + @"\" + DateTime.Now.Month;
        //        if ((!System.IO.Directory.Exists(lspath)))
        //            System.IO.Directory.CreateDirectory(lspath);

        //        lspath = lspath + @"\" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt";
        //        System.IO.StreamWriter sw = new System.IO.StreamWriter(lspath, true);
        //        sw.WriteLine("Main Response :-" + Response, "Response 1 :-" + Response1, "Response Content (json1) :-" + ResponseContent1);
        //        sw.Close();


        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        public void LogForAudit(string strVal)
        {
            try
            {
                string lspath = ConfigurationManager.AppSettings["file_path"].ToString() + "/erp_documents/SSOLOG/" + DateTime.Now.Year + @"\" + DateTime.Now.Month;
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);

                lspath = lspath + @"\" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(lspath, true);
                sw.WriteLine(strVal);
                sw.Close();
            }
            catch (Exception ex)
            {
            }
        }


        [AllowAnonymous]
        [ActionName("userLogin")]
        [HttpPost]
        public HttpResponseMessage GetUserLoginReturn(userlogininput values)
        {
            loginresponse GetLoginResponse = new loginresponse();
            try
            {
                var url = ConfigurationManager.AppSettings["host"];
                if (url == ConfigurationManager.AppSettings["livedomain_url"].ToString())
                {
                    var getSpireDocLicense = ConfigurationManager.AppSettings["SpireDocLicenseKey"];
                    Spire.License.LicenseProvider.SetLicenseKey(getSpireDocLicense);
                } 
                var username = string.Empty;

                msSQL = " SELECT b.user_gid,a.department_gid,a.employee_gid, user_password, " +
                       " user_code, concat(user_firstname, ' ', user_lastname) as username, " +
                       " concat(c.department_code, '/', c.department_name) as departmentname FROM hrm_mst_temployee a " +
                       " INNER JOIN adm_mst_tuser b on b.user_gid = a.user_gid " +
                       " inner join hrm_mst_tdepartment c on a.department_gid = c.department_gid " +
                       " WHERE user_code = '" + values.user_code + "' and user_password = '" + objcmnfunctions.ConvertToAscii(values.user_password) + "' and b.user_status = 'Y'";
                objODBCdatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCdatareader.HasRows == true)
                {
                    objODBCdatareader.Read();
                    var tokenresponse = Token(values.user_code, objcmnfunctions.ConvertToAscii(values.user_password));
                    dynamic newobj = Newtonsoft.Json.JsonConvert.DeserializeObject(tokenresponse);
                    tokenvalue = newobj.access_token;
                    employee_gid = objODBCdatareader["employee_gid"].ToString();
                    user_gid = objODBCdatareader["user_gid"].ToString();
                    username = objODBCdatareader["username"].ToString();
                    department_gid = objODBCdatareader["department_gid"].ToString();
                    departmentname = objODBCdatareader["departmentname"].ToString();
                    objODBCdatareader.Close();
                }
                else
                    objODBCdatareader.Close();
                msSQL = " INSERT INTO adm_mst_ttoken ( " +
                         " token, " +
                         " employee_gid, " +
                         " user_gid, " +
                         " department_gid, " +
                         " company_code " +
                         " )VALUES( " +
                         " 'Bearer " + tokenvalue + "'," +
                         " '" + employee_gid + "'," +
                         " '" + user_gid + "'," +
                         " '" + department_gid + "'," +
                         " '" + ConfigurationManager.AppSettings["company_code"] + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                GetLoginResponse.status = true;
                GetLoginResponse.message = "";
                GetLoginResponse.token = "Bearer " + tokenvalue;
                GetLoginResponse.username = username;
                GetLoginResponse.user_gid = user_gid;
                GetLoginResponse.departmentname = departmentname;
            }
            catch (Exception ex)
            {
                GetLoginResponse.status = false;
                GetLoginResponse.message = ex.ToString();
            }
            finally
            {

            }
            return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
        }

        // Login From ERP

        [AllowAnonymous]
        [ActionName("loginERP")]
        [HttpPost]
        public HttpResponseMessage GetUserReturn(loginERPinput values)
        {
            loginresponse GetLoginResponse = new loginresponse();
            try
            {
                var username = string.Empty;

                msSQL = " SELECT b.user_gid,a.department_gid, a.employee_gid, user_password, user_code, concat(user_firstname, ' ', user_lastname) as username FROM hrm_mst_temployee a " +
                        " INNER JOIN adm_mst_tuser b on b.user_gid = a.user_gid " +
                        " WHERE user_code = '" + values.user_code + "'";
                objODBCdatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCdatareader.HasRows == true)
                {
                    objODBCdatareader.Read();
                    string password = objODBCdatareader["user_password"].ToString();
                    var tokenresponse = Token(values.user_code, password);
                    dynamic newobj = Newtonsoft.Json.JsonConvert.DeserializeObject(tokenresponse);
                    tokenvalue = newobj.access_token;
                    employee_gid = objODBCdatareader["employee_gid"].ToString();
                    user_gid = objODBCdatareader["user_gid"].ToString();
                    username = objODBCdatareader["username"].ToString();
                    department_gid = objODBCdatareader["department_gid"].ToString();
                    objODBCdatareader.Close();
                }
                msSQL = " INSERT INTO adm_mst_ttoken ( " +
                         " token, " +
                         " employee_gid, " +
                         " user_gid, " +
                         " department_gid, " +
                         " company_code " +
                         " )VALUES( " +
                         " 'Bearer " + tokenvalue + "'," +
                         " '" + employee_gid + "'," +
                         " '" + user_gid + "'," +
                         " '" + department_gid + "'," +
                         " '" + ConfigurationManager.AppSettings["company_code"] + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                GetLoginResponse.status = true;
                GetLoginResponse.message = "";
                GetLoginResponse.token = "Bearer " + tokenvalue;
                GetLoginResponse.username = username;
                GetLoginResponse.user_gid = user_gid;
            }
            catch (Exception ex)
            {
                GetLoginResponse.status = false;
                GetLoginResponse.message = ex.ToString();
            }
            finally
            {

            }
            return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
        }


        //OTP LOGIN
        [AllowAnonymous]
        [ActionName("OTPlogin")]
        [HttpPost]
        public HttpResponseMessage GetUserotpReturn(otplogin values)

        {

            try
            {

                msSQL = " SELECT * FROM hrm_mst_temployee ";

                dt_datatable = objdbconn.GetDataTable(msSQL);

                List<string> employeeemailid_List = new List<string>();

                employeeemailid_List = dt_datatable.AsEnumerable().Select(p => p.Field<string>("employee_emailid")).ToList();

                if (employeeemailid_List.Contains(values.employee_emailid))
                {
                    var username = string.Empty;
                    //string *randomNumber*/;
                    Random rnd = new Random();
                    values.otpvalue = (rnd.Next(100000, 999999)).ToString();

                    //msSQL = " SELECT * FROM hrm_mst_temployee a " +
                    //        " INNER JOIN adm_mst_tuser b on b.user_gid = a.user_gid " +
                    //        " WHERE employee_emailid = '" + values.emailid + "'";
                    //msSQL= "SELECT employee_gid,user_gid,employee_emailid,employee_mobileno, FROM hrm_mst_temployee where employee_emailid ='" + values.employee_emailid + "'";
                    msSQL = "SELECT * FROM hrm_mst_temployee where employee_emailid ='" + values.employee_emailid + "'";
                    objODBCdatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCdatareader.HasRows == true)
                    {
                        objODBCdatareader.Read();
                        employee_gid = objODBCdatareader["employee_gid"].ToString();
                        user_gid = objODBCdatareader["user_gid"].ToString();
                        //values.employee_mobileno = objODBCdatareader["employee_mobileno"].ToString();
                        //values.created_time= objODBCdatareader["created_time"].ToString();
                        //values.expiry_time = objODBCdatareader["expiry_time"].ToString();
                        values.employee_mobileno = objODBCdatareader["employee_mobileno"].ToString();
                        values.employee_emailid = objODBCdatareader["employee_emailid"].ToString();
                        string requestUri = ConfigurationManager.AppSettings["smspushapi_url"].ToString();
                        var client = new RestClient(requestUri);
                        var request = new RestRequest(Method.GET);
                        request.AddParameter("appid", ConfigurationManager.AppSettings["smspushapi_appid"].ToString());
                        request.AddParameter("userId", ConfigurationManager.AppSettings["smspushapi_userid"].ToString());
                        request.AddParameter("pass", ConfigurationManager.AppSettings["smspushapi_password"].ToString());
                        request.AddParameter("contenttype", "3");
                        request.AddParameter("from", ConfigurationManager.AppSettings["smspushapi_from"].ToString());
                        request.AddParameter("selfid", "true");
                        request.AddParameter("alert", "1");
                        request.AddParameter("dlrreq", "true");
                        request.AddParameter("intflag", "false");

                        request.AddParameter("to", values.employee_mobileno);
                        request.AddParameter("text", "Use Verification code " + values.otpvalue + " for One.Samunnati portal authentication.\nTEAM SAMUNNATI");

                        IRestResponse response = client.Execute(request);


                        objODBCdatareader.Close();
                    }
                    msSQL = " INSERT INTO adm_mst_totplogin ( " +
                             " otpvalue, " +
                             " employee_gid, " +
                             " user_gid," +
                             " employee_mobileno," +
                             " created_time," +
                             " expiry_time" +
                             " )VALUES( " +
                             " '" + values.otpvalue + "'," +
                             " '" + employee_gid + "'," +
                             " '" + user_gid + "'," +
                             " '" + values.employee_mobileno + "'," +
                           " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                          " '" + DateTime.Now.AddSeconds(60).ToString("yyyy-MM-dd HH:mm:ss") + "'" + ")";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        values.status = true;
                        values.message = "OTP sent successfully to your registered mobile number ending with" + " " + values.employee_mobileno.Substring(values.employee_mobileno.Length - 4) + "... ";

                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error occurred while sending the OTP to your registered mobile number";

                    }

                }

                else
                {
                    values.status = false;
                    values.message = "Invalid email id";

                }




            }
            catch (Exception ex)
            {
                values.status = false;

                values.message = ex.ToString();
            }
            finally
            {

            }

            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // OTPLogin verification
        [AllowAnonymous]
        [ActionName("otpverify")]
        [HttpPost]
        public HttpResponseMessage GetUserReturn(otpverify values)
        {
            otpverifyresponse GetLoginResponse = new otpverifyresponse();
            try
            {
                var username = string.Empty;

                msSQL = "SELECT expiry_time FROM adm_mst_totplogin where otpvalue ='" + values.otpvalue + "'";
                lsexpiry_time = objdbconn.GetExecuteScalar(msSQL);



                DateTime expiry_time = DateTime.Parse(lsexpiry_time);

                DateTime now = DateTime.Now;




                if (expiry_time > now)
                {
                    msSQL = "SELECT user_gid FROM adm_mst_totplogin where otpvalue ='" + values.otpvalue + "'";
                    //msSQL = " SELECT b.user_gid, a.employee_gid, a.otpvalue, b.employee_mobileno FROM adm_mst_totplogin a " +
                    //        " INNER JOIN hrm_mst_temployee b on b.employee_mobileno = a.employee_mobileno " +
                    //        " WHERE otpvalues = '" + values.otpvalue + "'";
                    //msSQL = " SELECT b.user_gid,a.department_gid, a.employee_gid, user_password, user_code, concat(user_firstname, ' ', user_lastname) as username FROM hrm_mst_temployee a " +
                    //        " INNER JOIN adm_mst_tuser b on b.user_gid = a.user_gid " +
                    //        " WHERE otpvalue = '" + values.otpvalue + "'";
                    objODBCdatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCdatareader.HasRows == true)
                    {
                        objODBCdatareader.Read();
                        var user_gid = objODBCdatareader["user_gid"].ToString();
                        msSQL = "select user_code, user_password from adm_mst_tuser where user_gid = '" + user_gid + "'";
                        objODBCdatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCdatareader.HasRows == true)
                        {
                            values.user_code = objODBCdatareader["user_code"].ToString();
                            values.user_password = objODBCdatareader["user_password"].ToString();
                            var ObjToken = Token(values.user_code, values.user_password);
                            dynamic newobj = JsonConvert.DeserializeObject(ObjToken);
                            tokenvalue = "Bearer " + newobj.access_token;

                            if (tokenvalue != null)
                            {
                                msSQL = "CALL storyboarddb.adm_mst_spstoretoken('" + tokenvalue + "', '" + values.user_code + "',  '" + values.user_password + "', '" + ConfigurationManager.AppSettings[domain].ToString() + "')";
                                user_gid = objdbconn.GetExecuteScalar("CALL storyboarddb.adm_mst_spstoretoken('" + tokenvalue + "','" + values.user_code + "','" + values.user_password + "','" + ConfigurationManager.AppSettings[domain].ToString() + "','Web','')");
                                GetLoginResponse.status = true;
                                GetLoginResponse.message = "";
                                GetLoginResponse.token = tokenvalue;
                                GetLoginResponse.user_gid = user_gid;
                            }
                        }



                    }
                    objODBCdatareader.Close();
                }

                else
                {
                    GetLoginResponse.status = false;
                    GetLoginResponse.message = "Login time has been expired. kindly click the blade resend OTP ";

                }

            }
            catch (Exception ex)
            {
                GetLoginResponse.status = false;
                GetLoginResponse.message = "Invalid mail ID. Kindly contact your administrator";
            }
            finally
            {

            }
            return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
        }

        // Application Vendor Login

        [AllowAnonymous]
        [ActionName("vendorLogin")]
        [HttpPost]
        public HttpResponseMessage appVenLogin(appVendorInput values)
        {
            loginresponse GetLoginResponse = new loginresponse();
            try
            {
                domain = Request.RequestUri.Host.ToLower();
                string lscompany_code = ConfigurationManager.AppSettings[domain].ToString();
                msSQL = " SELECT vendoruser_status FROM " + lscompany_code + ".adm_mst_tvendoruser WHERE vendoruser_code='" + values.app_code + "' ";
                vendoruser_status = objdbconn.GetExecuteScalar(msSQL);
                if (vendoruser_status == "Y")
                {
                    msSQL = " SELECT vendoruser_gid,vendoruser_password, CONCAT(vendoruser_firstname, ' ' ,vendoruser_lastname) as username " +
                                " FROM " + lscompany_code + ".adm_mst_tvendoruser WHERE vendoruser_code='" + values.app_code + "' ";
                    objODBCdatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCdatareader.HasRows)
                    {
                        password = objODBCdatareader["vendoruser_password"].ToString();
                        if (password == objcmnfunctions.ConvertToAscii(values.password))
                        {
                            user_gid = objODBCdatareader["vendoruser_gid"].ToString();
                            username = objODBCdatareader["username"].ToString();
                            objODBCdatareader.Close();

                            msSQL = " SELECT applicationmaster_gid FROM " + lscompany_code + ".its_mst_tapplicationmaster WHERE application_code='" + values.app_code + "' ";
                            objODBCdatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCdatareader.HasRows)
                            {
                                objODBCdatareader.Close();

                                var ObjToken = Token(values.app_code, objcmnfunctions.ConvertToAscii(values.password), null);
                                dynamic newobj = JsonConvert.DeserializeObject(ObjToken);
                                tokenvalue = "Bearer " + newobj.access_token;
                                if (tokenvalue != null)
                                {

                                    msSQL = " DELETE FROM adm_trn_tconsumertoken " +
                                            " WHERE company_code = '" + lscompany_code + "' " +
                                            " AND user_Code = '" + values.app_code + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    msSQL = " INSERT INTO adm_trn_tconsumertoken(token, company_code, user_code, connection_string) " +
                                          " VALUES('" + tokenvalue + "', '" + lscompany_code + "', '" + values.app_code + "', (SELECT connection_string FROM adm_mst_tconsumerdb " +
                                          " WHERE company_code = '" + lscompany_code + "'))";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    msSQL = " SELECT vendoruser_gid,vendoruser_password, CONCAT(vendoruser_firstname, ' ' ,vendoruser_lastname) as username " +
                                            " FROM " + lscompany_code + ".adm_mst_tvendoruser WHERE vendoruser_code='" + values.app_code + "' ";
                                    objODBCdatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCdatareader.HasRows == true)
                                    {
                                        user_gid = objODBCdatareader["vendoruser_gid"].ToString();
                                    }
                                    objODBCdatareader.Close();

                                    msSQL = " INSERT INTO " + lscompany_code + ".adm_mst_ttoken ( " +
                                        " token, " +
                                        " user_gid, " +
                                        " created_date," +
                                        " company_code " +
                                        " )VALUES( " +
                                        " '" + tokenvalue + "'," +
                                        " '" + user_gid + "'," +
                                        " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                        " '" + lscompany_code + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                                    GetLoginResponse.status = true;
                                    GetLoginResponse.message = "success";
                                    GetLoginResponse.token = tokenvalue;
                                    GetLoginResponse.username = username;

                                }
                                else
                                {
                                    GetLoginResponse.status = false;
                                }

                            }
                            else
                            {
                                objODBCdatareader.Close();

                                GetLoginResponse.status = false;
                                GetLoginResponse.message = "appcode";
                            }

                        }
                        else
                        {
                            GetLoginResponse.status = false;
                            GetLoginResponse.message = "password";
                        }
                    }
                    else
                    {
                        objODBCdatareader.Close();

                        GetLoginResponse.status = false;
                        GetLoginResponse.message = "usercode";
                    }
                }
                else
                {
                  
                    GetLoginResponse.status = false;
                    GetLoginResponse.message = "userstatus";
                }
            }
            catch (Exception ex)
            {
                GetLoginResponse.status = false;
                GetLoginResponse.message = "error" + ex.ToString();
            }
            return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
                
        }

        // Application Lawyer Login

        [AllowAnonymous]
        [ActionName("lawyerUserLogin")]
        [HttpPost]
        public HttpResponseMessage GetLawyerUserLogin(userlogininput values)
        {
            loginresponse GetLoginResponse = new loginresponse();
            try
            {
                domain = Request.RequestUri.Host.ToLower();
                var ObjToken = Token(values.user_code, objcmnfunctions.ConvertToAscii(values.user_password), null);
                dynamic newobj = JsonConvert.DeserializeObject(ObjToken);
                tokenvalue = "Bearer " + newobj.access_token;
                if (tokenvalue != null)
                {
                    string lscompany_code = ConfigurationManager.AppSettings[domain].ToString();
                    msSQL = " DELETE FROM adm_trn_tconsumertoken " +
                            " WHERE company_code = '" + lscompany_code + "' " +
                            " AND user_Code = '" + values.user_code + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " INSERT INTO adm_trn_tconsumertoken(token, company_code, user_code, connection_string) " +
                          " VALUES('" + tokenvalue + "', '" + lscompany_code + "', '" + values.user_code + "', (SELECT connection_string FROM adm_mst_tconsumerdb " +
                          " WHERE company_code = '" + lscompany_code + "'))";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select lawyeruser_gid,lawyeruser_code,lawyeruser_password from " + lscompany_code + ".lgl_mst_tlawyeruser " +
                        " WHERE lawyeruser_status='Y' and lawyeruser_code = '" + values.user_code + "' and lawyeruser_password ='" + objcmnfunctions.ConvertToAscii(values.user_password) + "'";
                    objODBCdatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCdatareader.HasRows == true)
                    {
                        user_gid = objODBCdatareader["lawyeruser_gid"].ToString();
                    }
                    objODBCdatareader.Close();

                    msSQL = " INSERT INTO " + lscompany_code + ".adm_mst_ttoken ( " +
                        " token, " +
                        " user_gid, " +
                        " created_date," +
                        " company_code " +
                        " )VALUES( " +
                        " '" + tokenvalue + "'," +
                        " '" + user_gid + "'," +
                        " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " '" + lscompany_code + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    GetLoginResponse.status = true;
                    GetLoginResponse.message = "";
                    GetLoginResponse.token = tokenvalue;
                    GetLoginResponse.user_gid = user_gid;

                }
                else
                {
                    GetLoginResponse.status = false;
                }
            }
            catch (Exception ex)
            {
                GetLoginResponse.status = false;
                GetLoginResponse.message = ex.ToString();

            }

            return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
 
        }

        // Application External Vendor Login _RSK

        [AllowAnonymous]
        [ActionName("externalVendorUserLogin")]
        [HttpPost]
        public HttpResponseMessage externalVendorUserLogin(userlogininput values)
        {
            loginresponse GetLoginResponse = new loginresponse();
            try
            {
                domain = Request.RequestUri.Host.ToLower();
                var ObjToken = Token(values.user_code, objcmnfunctions.ConvertToAscii(values.user_password), null);
                dynamic newobj = JsonConvert.DeserializeObject(ObjToken);
                tokenvalue = "Bearer " + newobj.access_token;
                if (tokenvalue != null)
                {
                    string lscompany_code = ConfigurationManager.AppSettings[domain].ToString();

                    msSQL = " DELETE FROM adm_trn_tconsumertoken " +
                            " WHERE company_code = '" + lscompany_code + "' " +
                            " AND user_Code = '" + values.user_code + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " INSERT INTO adm_trn_tconsumertoken(token, company_code, user_code, connection_string) " +
                          " VALUES('" + tokenvalue + "', '" + lscompany_code + "', '" + values.user_code + "', (SELECT connection_string FROM adm_mst_tconsumerdb " +
                          " WHERE company_code = '" + lscompany_code + "'))";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select external_usergid,external_usercode from  " + lscompany_code + ".rsk_mst_texternaluser " +
                       " WHERE external_userstatus='Y' and external_usercode = '" + values.user_code + "' and external_userpassword ='" + objcmnfunctions.ConvertToAscii(values.user_password) + "'";
                    objODBCdatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCdatareader.HasRows == true)
                    {
                        user_gid = objODBCdatareader["external_usergid"].ToString();
                    }
                    objODBCdatareader.Close();

                    msSQL = " INSERT INTO " + lscompany_code + ".adm_mst_ttoken ( " +
                        " token, " +
                        " user_gid, " +
                        " created_date," +
                        " company_code " +
                        " )VALUES( " +
                        " '" + tokenvalue + "'," +
                        " '" + user_gid + "'," +
                        " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " '" + lscompany_code + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    GetLoginResponse.status = true;
                    GetLoginResponse.message = "";
                    GetLoginResponse.token = tokenvalue;
                    GetLoginResponse.user_gid = user_gid;

                }
                else
                {
                    GetLoginResponse.status = false;
                }
            }
            catch (Exception ex)
            {
                GetLoginResponse.status = false;
                GetLoginResponse.message = ex.ToString();

            }

            return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
        }

        [HttpPost]
        [ActionName("PostUserLogin")]
        public HttpResponseMessage PostUserLogin(PostUserLogin values)
        {
            loginresponse GetLoginResponse = new loginresponse();
            try
            {
                var url = ConfigurationManager.AppSettings["host"];
                if (url == ConfigurationManager.AppSettings["livedomain_url"].ToString())
                {
                    var getSpireDocLicense = ConfigurationManager.AppSettings["SpireDocLicenseKey"];
                    Spire.License.LicenseProvider.SetLicenseKey(getSpireDocLicense);
                } 

                domain = Request.RequestUri.Host.ToLower();
                    var ObjToken = Token(values.user_code, objcmnfunctions.ConvertToAscii(values.user_password), ConfigurationManager.AppSettings[domain].ToString());
                    dynamic newobj = JsonConvert.DeserializeObject(ObjToken);
                    tokenvalue = "Bearer " + newobj.access_token;
                    if (tokenvalue != null)
                    {
                        msSQL = "CALL storyboarddb.adm_mst_spstoretoken('" + tokenvalue + "', '" + values.user_code + "',  '" + objcmnfunctions.ConvertToAscii(values.user_password) + "', '" + ConfigurationManager.AppSettings[domain].ToString() + "')";
                        user_gid = objdbconn.GetExecuteScalar("CALL storyboarddb.adm_mst_spstoretoken('" + tokenvalue + "','" + values.user_code + "','" + objcmnfunctions.ConvertToAscii(values.user_password) + "','" + ConfigurationManager.AppSettings[domain].ToString() + "','Web','')");
                    GetLoginResponse.status = true;
                        GetLoginResponse.message = "";
                        GetLoginResponse.token = tokenvalue;
                        GetLoginResponse.user_gid = user_gid;

                        //return Ok(JsonConvert.SerializeObject(new {tokenvalue,user_gid }));
                        return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);

                    }
                    else
                    {
                        GetLoginResponse.status = false;
                        return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
                    }
            }
            catch (Exception ex)
            {
                GetLoginResponse.status = false;
                GetLoginResponse.message = ex.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
            }
        }
        public string Token(string userName, string password, string company_code = null)
        {

            var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>( "grant_type", "password" ),
                            new KeyValuePair<string, string>( "username", userName ),
                            new KeyValuePair<string, string> ( "Password", password ),
                            new KeyValuePair<string, string>("Scope",company_code)
                        };
            var content = new FormUrlEncodedContent(pairs);
            using (var client = new HttpClient())

            {
                domain = Request.RequestUri.Host.ToLower();
                var host = HttpContext.Current.Request.Url.Host;
                if (host == "localhost")
                {
                    var response = client.PostAsync(ConfigurationManager.AppSettings["protocol"].ToString() + ConfigurationManager.AppSettings["host"].ToString() +
                   "/StoryboardAPI/token", new FormUrlEncodedContent(pairs)).Result;
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    var response = client.PostAsync(ConfigurationManager.AppSettings["protocol"].ToString() + domain +
                   "/StoryboardAPI/token", new FormUrlEncodedContent(pairs)).Result;
                    return response.Content.ReadAsStringAsync().Result;
                }

            }
        }


        [AllowAnonymous]
        [ActionName("GetOTPFlag")]
        [HttpGet]
        public HttpResponseMessage GetOTPFlag()
        {
            otpresponse GetOtpResponse = new otpresponse();
            try
            {
                GetOtpResponse.otp_flag = ConfigurationManager.AppSettings["otpFlag"].ToString();

            }
            catch (Exception ex)
            {
                GetOtpResponse.otp_flag = "N";

            }

            return Request.CreateResponse(HttpStatusCode.OK, GetOtpResponse);
        }


    }
}
