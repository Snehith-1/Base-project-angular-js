using System;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.master.Models;
using System.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Net;
using System.Drawing;
using System.Text;
using System.Net.Http;
using System.Collections.Generic;
using RestSharp.Authenticators;
using ems.storage.Functions;

/// <summary>
/// (It's used for CAD CrimeCheckAPI ) CAD CrimeCheckAPI DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Praveen Raj and Abilash</remarks>
/// 

namespace ems.master.DataAccess
{
    public class DaCADCrimeCheckAPI
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid;
        int mnResult;
        string searchTerm, precision, searchFields, riskProfile;
        string requestURL;
        string callbackPath;
        string page = "10";

        public MdlIndividualCrimeRecordResponse DaGetCrimeRecordsIndividual(String employee_gid, MdlIndividualCrimeRecordRequest values)
        {
            MdlIndividualCrimeRecordResponse ObjMdlIndividualCrimeRecordResponse = new MdlIndividualCrimeRecordResponse();
            try
            {

                searchTerm = "{ \"name\" : \"" + values.individual_name + "\",  \"fatherName\" : \"" + values.father_name + "\",  \"dob\" : \"" + values.individual_dob + "\",  \"address\" : \"" + values.individual_address + "\" }";
                searchFields = values.search_in;
                if (values.search_mode == "High Accuracy")
                {
                    precision = "98";
                }
                else if (values.search_mode == "High Coverage")
                {
                    precision = "25";
                }
                else
                {
                    precision = "0";
                }
                riskProfile = "true";

                var client = new RestClient(ConfigurationManager.AppSettings["crimecheckrecordurl"].ToString())
                {
                    Authenticator = new HttpBasicAuthenticator(ConfigurationManager.AppSettings["crimecheckapikey"].ToString(), "")
                };

                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("searchTerm", searchTerm);
                if (precision != "0")
                {
                    request.AddParameter("precision", precision);
                }

                request.AddParameter("searchFields", searchFields);
                request.AddParameter("riskProfile", riskProfile);
                request.AddParameter("page", page);




                IRestResponse response = client.Execute(request);

                ObjMdlIndividualCrimeRecordResponse = JsonConvert.DeserializeObject<MdlIndividualCrimeRecordResponse>(response.Content);

                msSQL = "select crimechecksearchrecord_gid from ocs_trn_tcadcrimechecksearchrecord where contact_gid='" + values.contact_gid + "' and created_by='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {

                    msGetGid = objcmnfunctions.GetMasterGID("CCSR");
                    msSQL = " insert into ocs_trn_tcadcrimechecksearchrecord(" +
                            " crimechecksearchrecord_gid," +
                            " application_gid," +
                            " contact_gid," +
                            " crimecheck_flag," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.contact_gid + "'," +
                            "'Y'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                    if (ObjMdlIndividualCrimeRecordResponse.Status == "OK")
                {
                    ObjMdlIndividualCrimeRecordResponse.requestStatus = true;
                    ObjMdlIndividualCrimeRecordResponse.message = "Crime Records Obtained Successfully";
                }
                else
                {
                    ObjMdlIndividualCrimeRecordResponse.requestStatus = false;
                    ObjMdlIndividualCrimeRecordResponse.message = ErrorResponseCrimeCheck.errorResponse + ObjMdlIndividualCrimeRecordResponse.Status;
                }


            }
            catch (Exception ex)
            {
                ObjMdlIndividualCrimeRecordResponse.requestStatus = false;
                ObjMdlIndividualCrimeRecordResponse.message = "Error occured in obtaining Crime Records";
            }
            return ObjMdlIndividualCrimeRecordResponse;
        }

        public MdlCompanyCrimeRecordResponse DaGetCrimeRecordsCompany(String employee_gid, MdlCompanyCrimeRecordRequest values)
        {
            MdlCompanyCrimeRecordResponse ObjMdlCompanyCrimeRecordResponse = new MdlCompanyCrimeRecordResponse();
            try
            {

                searchTerm = "{ \"companyName\" : \"" + values.company_name + "\",  \"cinNumber\" : \"" + values.company_cin + "\",  \"address\" : \"" + values.company_address + "\" }";
                searchFields = values.search_in;
                if (values.search_mode == "High Accuracy")
                {
                    precision = "98";
                }
                else if (values.search_mode == "High Coverage")
                {
                    precision = "25";
                }
                else
                {
                    precision = "0";
                }
                riskProfile = "true";

                var client = new RestClient(ConfigurationManager.AppSettings["crimecheckrecordurl"].ToString())
                {
                    Authenticator = new HttpBasicAuthenticator(ConfigurationManager.AppSettings["crimecheckapikey"].ToString(), "")
                };

                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("searchTerm", searchTerm);
                if (precision != "0")
                {
                    request.AddParameter("precision", precision);
                }

                request.AddParameter("searchFields", searchFields);
                request.AddParameter("riskProfile", riskProfile);


                IRestResponse response = client.Execute(request);

                ObjMdlCompanyCrimeRecordResponse = JsonConvert.DeserializeObject<MdlCompanyCrimeRecordResponse>(response.Content);

                msSQL = "select crimechecksearchrecord_gid from ocs_trn_tcadcrimechecksearchrecord where institution_gid='" + values.institution_gid + "' and created_by='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {

                    msGetGid = objcmnfunctions.GetMasterGID("CCSR");
                    msSQL = " insert into ocs_trn_tcadcrimechecksearchrecord(" +
                            " crimechecksearchrecord_gid," +
                            " application_gid," +
                            " institution_gid," +
                            " crimecheck_flag," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + values.institution_gid + "'," +
                            "'Y'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (ObjMdlCompanyCrimeRecordResponse.Status == "OK")
                {
                    ObjMdlCompanyCrimeRecordResponse.requestStatus = true;
                    ObjMdlCompanyCrimeRecordResponse.message = "Crime Records Obtained Successfully";
                }
                else
                {
                    ObjMdlCompanyCrimeRecordResponse.requestStatus = false;
                    ObjMdlCompanyCrimeRecordResponse.message = ErrorResponseCrimeCheck.errorResponse + ObjMdlCompanyCrimeRecordResponse.Status;
                }


            }
            catch (Exception ex)
            {
                ObjMdlCompanyCrimeRecordResponse.requestStatus = false;
                ObjMdlCompanyCrimeRecordResponse.message = "Error occured in obtaining Crime Records";
            }
            return ObjMdlCompanyCrimeRecordResponse;
        }


        public MdlIndividualCrimeReportRequestResponse DaRequestCrimeReportIndividual(string employee_gid, MdlIndividualCrimeReportRequest values)
        {
            MdlIndividualCrimeReportRequestResponse ObjMdlIndividualCrimeReportRequestResponse = new MdlIndividualCrimeReportRequestResponse();
            try
            {
                values.individual_name = System.Web.HttpUtility.UrlEncode(values.individual_name);
                values.individual_address = System.Web.HttpUtility.UrlEncode(values.individual_address);


                requestURL = "name=" + values.individual_name;
                requestURL = requestURL + "&" + "address=" + values.individual_address;
                requestURL = requestURL + "&" + "callbackUrl=" + ConfigurationManager.AppSettings["callbackurl_individual"].ToString();


                if (values.report_mode == "RealTime")
                {
                    requestURL = requestURL + "&" + "reportMode=realTimeHighAccuracy";
                }


                var client = new RestClient(ConfigurationManager.AppSettings["crimecheckreporturl"].ToString())
                {
                    Authenticator = new HttpBasicAuthenticator(ConfigurationManager.AppSettings["crimecheckapikey"].ToString(), "")
                };

                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("application/x-www-form-urlencoded", requestURL, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                ObjMdlIndividualCrimeReportRequestResponse = JsonConvert.DeserializeObject<MdlIndividualCrimeReportRequestResponse>(response.Content);

                if (ObjMdlIndividualCrimeReportRequestResponse.Status == "OK")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("CRCT");
                    msSQL = " insert into ocs_trn_tcadcrimereportcontact( " +
                                " crimereportcontact_gid, " +
                                " contact_gid," +
                                " request_id," +
                                " request_time ," +
                                " report_mode ," +
                                " report_status," +
                                " created_by," +
                                " created_date" +
                                " )values(" +
                                "'" + msGetGid + "'," +
                                "'" + values.contact_gid + "'," +
                                "'" + ObjMdlIndividualCrimeReportRequestResponse.requestId + "'," +
                                "'" + ObjMdlIndividualCrimeReportRequestResponse.requestTime + "'," +
                                "'" + values.report_mode + "'," +
                                "'" + "Report not yet obtained" + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    ObjMdlIndividualCrimeReportRequestResponse.status = true;
                    ObjMdlIndividualCrimeReportRequestResponse.requestStatusMessage = "Request for Crime Report raised Successfully";

                    msSQL = "select crimecheckrtsearchrecord_gid from ocs_trn_tcadcrimecheckrtsearchrecord where contact_gid='" + values.contact_gid + "' and created_by='" + employee_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {

                        msGetGid = objcmnfunctions.GetMasterGID("CCRT");
                        msSQL = " insert into ocs_trn_tcadcrimecheckrtsearchrecord(" +
                                " crimecheckrtsearchrecord_gid," +
                                " application_gid," +
                                " contact_gid," +
                                " crimecheck_flag," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + values.application_gid + "'," +
                                "'" + values.contact_gid + "'," +
                                "'Y'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }


                }
                else
                {
                    ObjMdlIndividualCrimeReportRequestResponse.status = false;
                    ObjMdlIndividualCrimeReportRequestResponse.requestStatusMessage = ErrorResponseCrimeCheck.errorResponse + ObjMdlIndividualCrimeReportRequestResponse.Status; ;
                }
            }
            catch (Exception ex)
            {
                ObjMdlIndividualCrimeReportRequestResponse.status = false;
                ObjMdlIndividualCrimeReportRequestResponse.requestStatusMessage = "Error occured in raising request for Crime Report";
            }
            return ObjMdlIndividualCrimeReportRequestResponse;
        }

        public MdlCallbackResponse DaPostCallbackReportDetailsIndividual(MdlCallbackResponse values)
        {
            try
            {

                MdlIndividualCrimeReportResponse ObjMdlIndividualCrimeReportResponse = new MdlIndividualCrimeReportResponse();

                ObjMdlIndividualCrimeReportResponse = JsonConvert.DeserializeObject<MdlIndividualCrimeReportResponse>(values.data);

                msSQL = "select crimereportcontact_gid from samunnati.ocs_trn_tcadcrimereportcontact where request_id='" + ObjMdlIndividualCrimeReportResponse.requestId + "'";
                string lscrimereportcontact_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " update samunnati.ocs_trn_tcadcrimereportcontact set " +
                         " report_status='" + "Report obtained" + "'," +
                           " report_content='" + values.data.Replace("'", "") + "'," +
                           " report_link='" + ObjMdlIndividualCrimeReportResponse.downloadLink + "'," +
                           " updated_by='" + "CrimeCheck Team" + "'," +
                           " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           " where crimereportcontact_gid='" + lscrimereportcontact_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            catch (Exception ex)
            {
                values.data = "Error occured in Callback";
            }
            return values;
        }

        public MdlCompanyCrimeReportRequestResponse DaRequestCrimeReportCompany(string employee_gid, MdlCompanyCrimeReportRequest values)
        {
            MdlCompanyCrimeReportRequestResponse ObjMdlCompanyCrimeReportRequestResponse = new MdlCompanyCrimeReportRequestResponse();
            try
            {
                string lscrimecheckcompanytype;
                values.company_name = System.Web.HttpUtility.UrlEncode(values.company_name);
                values.company_address = System.Web.HttpUtility.UrlEncode(values.company_address);

                msSQL = " select b.crimecheckcompanytype" +
                    " from ocs_trn_tcadinstitution a left join ocs_mst_tcrimecheckcompanytypemapping b on a.companytype_name=b.samcompanytype where institution_gid='" + values.institution_gid + "'";
                lscrimecheckcompanytype = objdbconn.GetExecuteScalar(msSQL);
                if (lscrimecheckcompanytype != null && lscrimecheckcompanytype != "")
                {
                    values.company_type = lscrimecheckcompanytype.ToString();

                }
                else
                {
                    values.company_type = "dontKnow";
                }
                values.company_type = System.Web.HttpUtility.UrlEncode(values.company_type);


                requestURL = "companyName=" + values.company_name;
                if (values.company_cin != "")
                {
                    requestURL = requestURL + "&" + "cinNumber=" + values.company_cin;
                }
                requestURL = requestURL + "&" + "companyAddress=" + values.company_address;
                requestURL = requestURL + "&" + "companyType=" + values.company_type;
                requestURL = requestURL + "&" + "callbackUrl=" + ConfigurationManager.AppSettings["callbackurl_company"].ToString();


                if (values.report_mode == "RealTime")
                {
                    requestURL = requestURL + "&" + "reportMode=realTimeHighAccuracy";
                }


                var client = new RestClient(ConfigurationManager.AppSettings["crimecheckreporturl"].ToString())
                {
                    Authenticator = new HttpBasicAuthenticator(ConfigurationManager.AppSettings["crimecheckapikey"].ToString(), "")
                };

                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("application/x-www-form-urlencoded", requestURL, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                ObjMdlCompanyCrimeReportRequestResponse = JsonConvert.DeserializeObject<MdlCompanyCrimeReportRequestResponse>(response.Content);

                if (ObjMdlCompanyCrimeReportRequestResponse.Status == "OK")
                {
                    msGetGid = objcmnfunctions.GetMasterGID("CRIN");
                    msSQL = " insert into ocs_trn_tcadcrimereportinstitution( " +
                                " crimereportinstitution_gid, " +
                                " institution_gid," +
                                " request_id," +
                                " request_time ," +
                                " report_mode ," +
                                " report_status," +
                                " created_by," +
                                " created_date" +
                                " )values(" +
                                "'" + msGetGid + "'," +
                                "'" + values.institution_gid + "'," +
                                "'" + ObjMdlCompanyCrimeReportRequestResponse.requestId + "'," +
                                "'" + ObjMdlCompanyCrimeReportRequestResponse.requestTime + "'," +
                                "'" + values.report_mode + "'," +
                                "'" + "Report not yet obtained" + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    ObjMdlCompanyCrimeReportRequestResponse.status = true;
                    ObjMdlCompanyCrimeReportRequestResponse.requestStatusMessage = "Request for Crime Report raised Successfully";

                    msSQL = "select crimecheckrtsearchrecord_gid from ocs_trn_tcrimecheckrtsearchrecord where institution_gid='" + values.institution_gid + "' and created_by='" + employee_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {

                        msGetGid = objcmnfunctions.GetMasterGID("CCRT");
                        msSQL = " insert into ocs_trn_tcadcrimecheckrtsearchrecord(" +
                                " crimecheckrtsearchrecord_gid," +
                                " application_gid," +
                                " institution_gid," +
                                " crimecheck_flag," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + values.application_gid + "'," +
                                "'" + values.institution_gid + "'," +
                                "'Y'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }
                else
                {
                    ObjMdlCompanyCrimeReportRequestResponse.status = false;
                    ObjMdlCompanyCrimeReportRequestResponse.requestStatusMessage = ErrorResponseCrimeCheck.errorResponse + ObjMdlCompanyCrimeReportRequestResponse.Status;
                }

            }
            catch (Exception ex)
            {
                ObjMdlCompanyCrimeReportRequestResponse.status = false;
                ObjMdlCompanyCrimeReportRequestResponse.requestStatusMessage = "Error occured in raising request for Crime Report";
            }
            return ObjMdlCompanyCrimeReportRequestResponse;
        }

        public void DaGetIndividualDetailsReport(string contact_gid, MdlIndividualCrimeReport values)
        {
            try
            {
                msSQL = " select first_name, middle_name, last_name, individual_dob" +
                        " from ocs_trn_tcadcontact where contact_gid='" + contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.individual_name = objODBCDatareader["first_name"].ToString() + " " + objODBCDatareader["middle_name"].ToString() + " " + objODBCDatareader["last_name"].ToString();
                    values.individual_dob = objODBCDatareader["individual_dob"].ToString();
                }

                msSQL = " select addressline1, addressline2, city, taluka, district, state" +
                        " from ocs_trn_tcadcontact2address where contact_gid='" + contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.individual_address = objODBCDatareader["addressline1"].ToString() + " " + objODBCDatareader["addressline2"].ToString() + " " + objODBCDatareader["city"].ToString() + " " + objODBCDatareader["taluka"].ToString() + " " + objODBCDatareader["district"].ToString();
                }

                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetIndividualReportList(MdlIndividualCrimeReportList values, string contact_gid)
        {
            try
            {
                msSQL = "select crimereportcontact_gid,contact_gid,request_id,request_time,report_mode,report_status,report_link " +
                    " from ocs_trn_tcadcrimereportcontact where contact_gid='" + contact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getindividualcrimereport_list = new List<individualcrimereport_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getindividualcrimereport_list.Add(new individualcrimereport_list
                        {
                            crimereportcontact_gid = (dr_datarow["crimereportcontact_gid"].ToString()),
                            contact_gid = (dr_datarow["contact_gid"].ToString()),
                            request_id = (dr_datarow["request_id"].ToString()),
                            request_time = (dr_datarow["request_time"].ToString()),
                            report_mode = (dr_datarow["report_mode"].ToString()),
                            report_status = (dr_datarow["report_status"].ToString()),
                            report_link = (dr_datarow["report_link"].ToString())
                        });
                    }
                    values.individualcrimereport_list = getindividualcrimereport_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public MdlCallbackResponse DaPostCallbackReportDetailsCompany(MdlCallbackResponse values)
        {
            try
            {

                MdlCompanyCrimeReportResponse ObjMdlCompanyCrimeReportResponse = new MdlCompanyCrimeReportResponse();

                ObjMdlCompanyCrimeReportResponse = JsonConvert.DeserializeObject<MdlCompanyCrimeReportResponse>(values.data);

                msSQL = "select crimereportinstitution_gid from samunnati.ocs_trn_tcadcrimereportinstitution where request_id='" + ObjMdlCompanyCrimeReportResponse.requestId + "'";
                string lscrimereportinstitution_gid = objdbconn.GetExecuteScalar(msSQL);



                msSQL = " update samunnati.ocs_trn_tcadcrimereportinstitution set " +
                         " report_status='" + "Report obtained" + "'," +
                           " report_content='" + values.data.Replace("'", "") + "'," +
                           " report_link='" + ObjMdlCompanyCrimeReportResponse.downloadLink + "'," +
                           " updated_by='" + "CrimeCheck Team" + "'," +
                           " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           " where crimereportinstitution_gid='" + lscrimereportinstitution_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            catch (Exception ex)
            {
                values.data = "Error occured in Callback";
            }
            return values;
        }

        public void DaGetCompanyReportList(MdlCompanyCrimeReportList values, string institution_gid)
        {
            try
            {
                msSQL = "select crimereportinstitution_gid,institution_gid,request_id,request_time,report_mode,report_status,report_link " +
                    " from ocs_trn_tcadcrimereportinstitution where institution_gid='" + institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcompanycrimereport_list = new List<companycrimereport_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcompanycrimereport_list.Add(new companycrimereport_list
                        {
                            crimereportinstitution_gid = (dr_datarow["crimereportinstitution_gid"].ToString()),
                            institution_gid = (dr_datarow["institution_gid"].ToString()),
                            request_id = (dr_datarow["request_id"].ToString()),
                            request_time = (dr_datarow["request_time"].ToString()),
                            report_mode = (dr_datarow["report_mode"].ToString()),
                            report_status = (dr_datarow["report_status"].ToString()),
                            report_link = (dr_datarow["report_link"].ToString())
                        });
                    }
                    values.companycrimereport_list = getcompanycrimereport_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetCompanyDetailsReport(string institution_gid, MdlCompanyCrimeReport values)
        {
            try
            {
                msSQL = " select company_name" +
                        " from ocs_trn_tcadinstitution where institution_gid='" + institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.company_name = objODBCDatareader["company_name"].ToString();
                }

                msSQL = " select addressline1, addressline2, city, taluka, district, state" +
                        " from ocs_trn_tcadinstitution2address where institution_gid='" + institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.company_address = objODBCDatareader["addressline1"].ToString() + ", " + objODBCDatareader["addressline2"].ToString() + ", " + objODBCDatareader["city"].ToString() + ", " + objODBCDatareader["taluka"].ToString() + ", " + objODBCDatareader["district"].ToString();
                }

                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetCrimeRecordIndividualDetail(string contact_gid, MdlIndividualCrimeRecord values)
        {
            try
            {
                msSQL = " select first_name, middle_name, last_name, father_firstname, father_middlename, father_lastname, individual_dob" +
                        " from ocs_trn_tcadcontact where contact_gid='" + contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    if (!String.IsNullOrEmpty(objODBCDatareader["middle_name"].ToString()))
                    {
                        values.individual_name = objODBCDatareader["first_name"].ToString() + " " + objODBCDatareader["middle_name"].ToString() + " " + objODBCDatareader["last_name"].ToString();
                    }
                    else
                    {
                        values.individual_name = objODBCDatareader["first_name"].ToString() + " " + objODBCDatareader["last_name"].ToString();
                    }
                    values.individual_dob = objODBCDatareader["individual_dob"].ToString();
                    values.individual_fathername = objODBCDatareader["father_firstname"].ToString() + " " + objODBCDatareader["father_middlename"].ToString() + " " + objODBCDatareader["father_lastname"].ToString();
                }

                msSQL = " select concat(addressline1,',',addressline2) as address, " +
            " contact2address_gid as address_gid from ocs_trn_tcadcontact2address where contact_gid = '" + contact_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getindividualaddress_list = new List<individualaddress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getindividualaddress_list.Add(new individualaddress_list
                        {
                            address_gid = dt["address_gid"].ToString(),
                            address = dt["address"].ToString(),
                        });
                    }
                }
                values.individualaddress_list = getindividualaddress_list;
                dt_datatable.Dispose();

                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetCrimeRecordCompanyDetail(string institution_gid, MdlCompanyCrimeRecord values)
        {
            try
            {
                msSQL = " select company_name, case when cin_no = '' then '-' else cin_no end as cin_no" +
                        " from ocs_trn_tcadinstitution where institution_gid='" + institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.company_name = objODBCDatareader["company_name"].ToString();
                    values.company_cin = objODBCDatareader["cin_no"].ToString();
                }

                msSQL = " select concat(addressline1,',',addressline2) as address, " +
            " institution2address_gid as address_gid from ocs_trn_tcadinstitution2address where institution_gid = '" + institution_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcompanyaddress_list = new List<companyaddress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcompanyaddress_list.Add(new companyaddress_list
                        {
                            address_gid = dt["address_gid"].ToString(),
                            address = dt["address"].ToString(),
                        });
                    }
                }
                values.companyaddress_list = getcompanyaddress_list;
                dt_datatable.Dispose();

                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public bool DaTagCaseIndividual(string employee_gid, MdlTagCaseIndividual values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CRTC");
            msSQL = " insert into ocs_trn_tcadcrimecasetaggedcontact(" +
                    " crimecasetaggedcontact_gid," +
                    " contact_gid," +
                    " cin_number," +
                    " case_type," +
                    " case_status," +
                    " petitioner," +
                    " petitioner_address," +
                    " respondent," +
                    " respondent_address," +
                    " casetype_name," +
                    " case_name," +
                    " court_type," +
                    " district," +
                    " state," +
                    " year," +
                    " gfc_updated_at," +
                    " gfc_uniqueid," +
                    " casedetails_link," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.contact_gid + "'," +
                    "'" + values.cin_number + "'," +
                    "'" + values.case_type + "'," +
                    "'" + values.case_status + "'," +
                    "'" + values.petitioner + "'," +
                    "'" + values.petitioner_address + "'," +
                    "'" + values.respondent + "'," +
                    "'" + values.respondent_address + "'," +
                    "'" + values.casetype_name + "'," +
                    "'" + values.case_name + "'," +
                    "'" + values.court_type + "'," +
                    "'" + values.district + "'," +
                    "'" + values.state + "'," +
                    "'" + values.year + "'," +
                    "'" + values.gfc_updated_at + "'," +
                    "'" + values.gfc_uniqueid + "'," +
                    "'" + values.casedetails_link + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Case Tagged Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured in Tagging Case";
                return false;
            }

        }

        public void DaGetTaggedCaseIndividualSummary(string contact_gid, MdlTagCaseIndividual values)
        {

            msSQL = " select crimecasetaggedcontact_gid,contact_gid,cin_number,case_type,case_status," +
                    " petitioner,petitioner_address,respondent,respondent_address,casetype_name,case_name," +
                    " court_type,district,state,year,gfc_updated_at,gfc_uniqueid,casedetails_link" +
                    " from ocs_trn_tcadcrimecasetaggedcontact a " +
                    " where a.contact_gid='" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettagcaseindividual_list = new List<tagcaseindividual_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    gettagcaseindividual_list.Add(new tagcaseindividual_list
                    {
                        crimecasetaggedcontact_gid = dt["crimecasetaggedcontact_gid"].ToString(),
                        contact_gid = dt["contact_gid"].ToString(),
                        cin_number = dt["cin_number"].ToString(),
                        case_type = dt["case_type"].ToString(),
                        case_status = dt["case_status"].ToString(),
                        petitioner = dt["petitioner"].ToString(),
                        petitioner_address = dt["petitioner_address"].ToString(),
                        respondent = dt["respondent"].ToString(),
                        respondent_address = dt["respondent_address"].ToString(),
                        casetype_name = dt["casetype_name"].ToString(),
                        case_name = dt["case_name"].ToString(),
                        court_type = dt["court_type"].ToString(),
                        district = dt["district"].ToString(),
                        state = dt["state"].ToString(),
                        year = dt["year"].ToString(),
                        gfc_updated_at = dt["gfc_updated_at"].ToString(),
                        gfc_uniqueid = dt["gfc_uniqueid"].ToString(),
                        casedetails_link = dt["casedetails_link"].ToString(),
                    });

                }
            }
            values.tagcaseindividual_list = gettagcaseindividual_list;
            dt_datatable.Dispose();
        }

        public void DaDeleteTaggedCaseIndividual(string crimecasetaggedcontact_gid, MdlTagCaseIndividual values)
        {
            msSQL = "delete from ocs_trn_tcadcrimecasetaggedcontact where crimecasetaggedcontact_gid='" + crimecasetaggedcontact_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Tagged Case Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaTagCaseInstitution(string employee_gid, MdlTagCaseInstitution values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CRTI");
            msSQL = " insert into ocs_trn_tcadcrimecasetaggedinstitution(" +
                    " crimecasetaggedinstitution_gid," +
                    " institution_gid," +
                    " cin_number," +
                    " case_type," +
                    " case_status," +
                    " petitioner," +
                    " petitioner_address," +
                    " respondent," +
                    " respondent_address," +
                    " casetype_name," +
                    " case_name," +
                    " court_type," +
                    " district," +
                    " state," +
                    " year," +
                    " gfc_updated_at," +
                    " gfc_uniqueid," +
                    " casedetails_link," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.institution_gid + "'," +
                    "'" + values.cin_number + "'," +
                    "'" + values.case_type + "'," +
                    "'" + values.case_status + "'," +
                    "'" + values.petitioner + "'," +
                    "'" + values.petitioner_address + "'," +
                    "'" + values.respondent + "'," +
                    "'" + values.respondent_address + "'," +
                    "'" + values.casetype_name + "'," +
                    "'" + values.case_name + "'," +
                    "'" + values.court_type + "'," +
                    "'" + values.district + "'," +
                    "'" + values.state + "'," +
                    "'" + values.year + "'," +
                    "'" + values.gfc_updated_at + "'," +
                    "'" + values.gfc_uniqueid + "'," +
                    "'" + values.casedetails_link + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Case Tagged Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured in Tagging Case";
                return false;
            }

        }

        public void DaGetTaggedCaseInstitutionSummary(string institution_gid, MdlTagCaseInstitution values)
        {

            msSQL = " select crimecasetaggedinstitution_gid,institution_gid,cin_number,case_type,case_status," +
                    " petitioner,petitioner_address,respondent,respondent_address,casetype_name,case_name," +
                    " court_type,district,state,year,gfc_updated_at,gfc_uniqueid,casedetails_link" +
                    " from ocs_trn_tcadcrimecasetaggedinstitution a " +
                    " where a.institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettagcaseinstitution_list = new List<tagcaseinstitution_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    gettagcaseinstitution_list.Add(new tagcaseinstitution_list
                    {
                        crimecasetaggedinstitution_gid = dt["crimecasetaggedinstitution_gid"].ToString(),
                        institution_gid = dt["institution_gid"].ToString(),
                        cin_number = dt["cin_number"].ToString(),
                        case_type = dt["case_type"].ToString(),
                        case_status = dt["case_status"].ToString(),
                        petitioner = dt["petitioner"].ToString(),
                        petitioner_address = dt["petitioner_address"].ToString(),
                        respondent = dt["respondent"].ToString(),
                        respondent_address = dt["respondent_address"].ToString(),
                        casetype_name = dt["casetype_name"].ToString(),
                        case_name = dt["case_name"].ToString(),
                        court_type = dt["court_type"].ToString(),
                        district = dt["district"].ToString(),
                        state = dt["state"].ToString(),
                        year = dt["year"].ToString(),
                        gfc_updated_at = dt["gfc_updated_at"].ToString(),
                        gfc_uniqueid = dt["gfc_uniqueid"].ToString(),
                        casedetails_link = dt["casedetails_link"].ToString(),
                    });

                }
            }
            values.tagcaseinstitution_list = gettagcaseinstitution_list;
            dt_datatable.Dispose();
        }

        public void DaDeleteTaggedCaseInstitution(string crimecasetaggedinstitution_gid, MdlTagCaseInstitution values)
        {
            msSQL = "delete from ocs_trn_tcadcrimecasetaggedinstitution where crimecasetaggedinstitution_gid='" + crimecasetaggedinstitution_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Tagged Case Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaGetCCCaseTaggedIndividualSummary(string application_gid, MdlCCCaseTaggedIndividual values)
        {

            msSQL = " select contact_gid,first_name,middle_name,last_name,stakeholder_type," +
                    " (select distinct concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) from ocs_trn_tcadcrimecasetaggedcontact a left join hrm_mst_temployee b on a.created_by = b.employee_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.contact_gid = f.contact_gid) as tagged_by," +
                    " (select count(crimecasetaggedcontact_gid) from ocs_trn_tcadcrimecasetaggedcontact a left join hrm_mst_temployee b on a.created_by = b.employee_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.contact_gid = f.contact_gid) as no_of_cases" +
                    " from ocs_trn_tcadcontact f " +
                    " where f.application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcccasetaggedindividual_list = new List<cccasetaggedindividual_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["no_of_cases"].ToString() != "0")
                    {
                        getcccasetaggedindividual_list.Add(new cccasetaggedindividual_list
                        {
                            contact_gid = dt["contact_gid"].ToString(),
                            individual_name = dt["first_name"].ToString() + dt["middle_name"].ToString() + dt["last_name"].ToString(),
                            stakeholder_type = dt["stakeholder_type"].ToString(),
                            tagged_by = dt["tagged_by"].ToString(),
                            no_of_cases = dt["no_of_cases"].ToString(),
                        });
                    }
                }
            }
            for (int i = 0; i < getcccasetaggedindividual_list.Count; i++)
            {
                msSQL = " select crimecasetaggedcontact_gid,contact_gid,cin_number,case_type,case_status," +
                    " petitioner,petitioner_address,respondent,respondent_address,casetype_name,case_name," +
                    " court_type,district,state,year,gfc_updated_at,gfc_uniqueid,casedetails_link" +
                    " from ocs_trn_tcadcrimecasetaggedcontact a " +
                    " where a.contact_gid='" + getcccasetaggedindividual_list[i].contact_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettagcaseindividual_list = new List<tagcaseindividual_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        gettagcaseindividual_list.Add(new tagcaseindividual_list
                        {
                            crimecasetaggedcontact_gid = dt["crimecasetaggedcontact_gid"].ToString(),
                            contact_gid = dt["contact_gid"].ToString(),
                            cin_number = dt["cin_number"].ToString(),
                            case_type = dt["case_type"].ToString(),
                            case_status = dt["case_status"].ToString(),
                            petitioner = dt["petitioner"].ToString(),
                            petitioner_address = dt["petitioner_address"].ToString(),
                            respondent = dt["respondent"].ToString(),
                            respondent_address = dt["respondent_address"].ToString(),
                            casetype_name = dt["casetype_name"].ToString(),
                            case_name = dt["case_name"].ToString(),
                            court_type = dt["court_type"].ToString(),
                            district = dt["district"].ToString(),
                            state = dt["state"].ToString(),
                            year = dt["year"].ToString(),
                            gfc_updated_at = dt["gfc_updated_at"].ToString(),
                            gfc_uniqueid = dt["gfc_uniqueid"].ToString(),
                            casedetails_link = dt["casedetails_link"].ToString(),
                        });

                    }
                }

                getcccasetaggedindividual_list[i].tagcaseindividual_list = gettagcaseindividual_list;

            }

            values.cccasetaggedindividual_list = getcccasetaggedindividual_list;
            dt_datatable.Dispose();
        }

        public void DaGetCCCaseTaggedInstitutionSummary(string application_gid, MdlCCCaseTaggedInstitution values)
        {

            msSQL = " select institution_gid,company_name,stakeholder_type," +
                    " (select distinct concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) from ocs_trn_tcadcrimecasetaggedinstitution a left join hrm_mst_temployee b on a.created_by = b.employee_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.institution_gid = f.institution_gid) as tagged_by," +
                    " (select count(crimecasetaggedinstitution_gid) from ocs_trn_tcadcrimecasetaggedinstitution a left join hrm_mst_temployee b on a.created_by = b.employee_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.institution_gid = f.institution_gid) as no_of_cases" +
                    " from ocs_trn_tcadinstitution f " +
                    " where f.application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcccasetaggedinstitution_list = new List<cccasetaggedinstitution_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["no_of_cases"].ToString() != "0")
                    {
                        getcccasetaggedinstitution_list.Add(new cccasetaggedinstitution_list
                        {
                            institution_gid = dt["institution_gid"].ToString(),
                            company_name = dt["company_name"].ToString(),
                            stakeholder_type = dt["stakeholder_type"].ToString(),
                            tagged_by = dt["tagged_by"].ToString(),
                            no_of_cases = dt["no_of_cases"].ToString(),
                        });
                    }
                }
            }
            for (int i = 0; i < getcccasetaggedinstitution_list.Count; i++)
            {
                msSQL = " select crimecasetaggedinstitution_gid,institution_gid,cin_number,case_type,case_status," +
                    " petitioner,petitioner_address,respondent,respondent_address,casetype_name,case_name," +
                    " court_type,district,state,year,gfc_updated_at,gfc_uniqueid,casedetails_link" +
                    " from ocs_trn_tcadcrimecasetaggedinstitution a " +
                    " where a.institution_gid='" + getcccasetaggedinstitution_list[i].institution_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettagcaseinstitution_list = new List<tagcaseinstitution_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        gettagcaseinstitution_list.Add(new tagcaseinstitution_list
                        {
                            crimecasetaggedinstitution_gid = dt["crimecasetaggedinstitution_gid"].ToString(),
                            institution_gid = dt["institution_gid"].ToString(),
                            cin_number = dt["cin_number"].ToString(),
                            case_type = dt["case_type"].ToString(),
                            case_status = dt["case_status"].ToString(),
                            petitioner = dt["petitioner"].ToString(),
                            petitioner_address = dt["petitioner_address"].ToString(),
                            respondent = dt["respondent"].ToString(),
                            respondent_address = dt["respondent_address"].ToString(),
                            casetype_name = dt["casetype_name"].ToString(),
                            case_name = dt["case_name"].ToString(),
                            court_type = dt["court_type"].ToString(),
                            district = dt["district"].ToString(),
                            state = dt["state"].ToString(),
                            year = dt["year"].ToString(),
                            gfc_updated_at = dt["gfc_updated_at"].ToString(),
                            gfc_uniqueid = dt["gfc_uniqueid"].ToString(),
                            casedetails_link = dt["casedetails_link"].ToString(),
                        });

                    }
                }

                getcccasetaggedinstitution_list[i].tagcaseinstitution_list = gettagcaseinstitution_list;

            }

            values.cccasetaggedinstitution_list = getcccasetaggedinstitution_list;
            dt_datatable.Dispose();
        }

        public void DaGetCCReportInstitutionSummary(string application_gid, MdlCCReportInstitution values)
        {

            msSQL = " select institution_gid,company_name,stakeholder_type," +
                    " (select count(crimereportinstitution_gid) from ocs_trn_tcadcrimereportinstitution a where a.institution_gid = f.institution_gid) as no_of_reports" +
                    " from ocs_trn_tcadinstitution f " +
                    " where f.application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getccreportinstitution_list = new List<ccreportinstitution_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["no_of_reports"].ToString() != "0")
                    {
                        getccreportinstitution_list.Add(new ccreportinstitution_list
                        {
                            institution_gid = dt["institution_gid"].ToString(),
                            company_name = dt["company_name"].ToString(),
                            stakeholder_type = dt["stakeholder_type"].ToString(),
                            no_of_reports = dt["no_of_reports"].ToString(),
                        });
                    }
                }
            }
            for (int i = 0; i < getccreportinstitution_list.Count; i++)
            {
                msSQL = " select crimereportinstitution_gid,request_id,report_mode,report_link" +
                    " from ocs_trn_tcadcrimereportinstitution a " +
                    " where a.institution_gid='" + getccreportinstitution_list[i].institution_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getreportinstitution_list = new List<reportinstitution_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getreportinstitution_list.Add(new reportinstitution_list
                        {
                            crimereportinstitution_gid = dt["crimereportinstitution_gid"].ToString(),
                            request_id = dt["request_id"].ToString(),
                            report_mode = dt["report_mode"].ToString(),
                            report_link = dt["report_link"].ToString(),
                        });

                    }
                }
                getccreportinstitution_list[i].reportinstitution_list = getreportinstitution_list;
            }
            values.ccreportinstitution_list = getccreportinstitution_list;
            dt_datatable.Dispose();
        }

        public void DaGetCCReportIndividualSummary(string application_gid, MdlCCReportIndividual values)
        {

            msSQL = " select contact_gid,first_name,middle_name,last_name,stakeholder_type," +
                    " (select count(crimereportcontact_gid) from ocs_trn_tcadcrimereportcontact a where a.contact_gid = f.contact_gid) as no_of_reports" +
                    " from ocs_trn_tcadcontact f " +
                    " where f.application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getccreportindividual_list = new List<ccreportindividual_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["no_of_reports"].ToString() != "0")
                    {
                        getccreportindividual_list.Add(new ccreportindividual_list
                        {
                            contact_gid = dt["contact_gid"].ToString(),
                            individual_name = dt["first_name"].ToString() + " " + dt["middle_name"].ToString() + " " + dt["last_name"].ToString(),
                            stakeholder_type = dt["stakeholder_type"].ToString(),
                            no_of_reports = dt["no_of_reports"].ToString(),
                        });
                    }
                }
            }
            for (int i = 0; i < getccreportindividual_list.Count; i++)
            {
                msSQL = " select crimereportcontact_gid,request_id,report_mode,report_link" +
                    " from ocs_trn_tcadcrimereportcontact a " +
                    " where a.contact_gid='" + getccreportindividual_list[i].contact_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getreportindividual_list = new List<reportindividual_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getreportindividual_list.Add(new reportindividual_list
                        {
                            crimereportcontact_gid = dt["crimereportcontact_gid"].ToString(),
                            request_id = dt["request_id"].ToString(),
                            report_mode = dt["report_mode"].ToString(),
                            report_link = dt["report_link"].ToString(),
                        });

                    }
                }
                getccreportindividual_list[i].reportindividual_list = getreportindividual_list;
            }
            values.ccreportindividual_list = getccreportindividual_list;
            dt_datatable.Dispose();
        }

    }
}