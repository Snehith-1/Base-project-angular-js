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
using System.Text.RegularExpressions;
using ems.storage.Functions;
using System.Net;

namespace ems.master.DataAccess
{
    public class DaProbeAPI
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetGidLog, lscompany_code, lsdocumentabspath, lsdocumentrelpath, lspath, fileName, documentId,responseDoc;
        int mnResult;

        public void DaGetInstitutionDetails(string institution_gid, MdlMstInstitutionAdd values)
        {
            try
            {
                msSQL = " select company_name, companypan_no, cin_no" +
                        " from ocs_mst_tinstitution where institution_gid='" + institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.company_name = objODBCDatareader["company_name"].ToString();
                    values.companypan_no = objODBCDatareader["companypan_no"].ToString();
                    values.cin_no = objODBCDatareader["cin_no"].ToString();
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


        public MdlBaseDetailsResponse DaGetBaseDetails(string employee_gid, MdlBaseDetailsRequest Values)
        {
            MdlBaseDetailsResponse ObjMdlBaseDetailsResponse = new MdlBaseDetailsResponse();
            try
            {               
                var client = new RestClient(ConfigurationManager.AppSettings["probeapicompanyurl"].ToString() + Values.pan + "/base-details?identifier_type=PAN");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-api-key", ConfigurationManager.AppSettings["probeapicompanykey"].ToString());
                request.AddHeader("x-api-version", "1.3");
                request.AddHeader("accept", "application/json");
                IRestResponse response = client.Execute(request);
                ObjMdlBaseDetailsResponse = JsonConvert.DeserializeObject<MdlBaseDetailsResponse>(response.Content);
                

                msSQL = "delete from ocs_trn_tinstitutionprobedetails where institution_gid='" + Values.institution_gid + "' and api_name='Base Details'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                response.Content = response.Content.Replace("'", "");

                msGetGid = objcmnfunctions.GetMasterGID("IPDT");
                msSQL = " insert into ocs_trn_tinstitutionprobedetails(" +

                        " institutionprobedetails_gid," +
                        " institution_gid," +
                        " application_gid," +
                        " api_name," +

                        " response," +
                        " apicall_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +

                        "'" + msGetGid + "'," +
                        "'" + Values.institution_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + "Base Details" + "',";

                if (response.StatusCode.ToString() == "OK")
                {
                    msSQL += "'" + response.Content + "'," +
                            "'" + "Success" + "',";                         
                }
                else
                {
                    msSQL += "null," +
                          "'" + "Failed" + "',";
                }

               msSQL += "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if(mnResult == 1)
                {
                    msGetGidLog = objcmnfunctions.GetMasterGID("PDTL");
                    msSQL = " insert into ocs_trn_tinstitutionprobedetailslog(" +
                            " institutionprobedetailslog_gid," +
                            " institutionprobedetails_gid," +
                            " institution_gid," +
                            " application_gid," +
                            " api_name," +
                            " response," +
                            " apicall_status," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGidLog + "'," +
                            "'" + msGetGid + "'," +
                            "'" + Values.institution_gid + "'," +
                            "'" + Values.application_gid + "'," +
                            "'" + "Base Details" + "',";

                    if (response.StatusCode.ToString() == "OK")
                    {
                        msSQL += "'" + response.Content + "'," +
                                "'" + "Success" + "',";
                    }
                    else
                    {
                        msSQL += "null," +
                              "'" + "Failed" + "',";
                    }

                    msSQL += "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if(ObjMdlBaseDetailsResponse.metadata != null)
                    {
                        ObjMdlBaseDetailsResponse.status = true;
                        ObjMdlBaseDetailsResponse.message = "Base Details Obtained Successfully..!";
                    }
                    else
                    {
                        ObjMdlBaseDetailsResponse.status = false;
                        ObjMdlBaseDetailsResponse.message = ErrorResponseProbe.errorResponse + ObjMdlBaseDetailsResponse.message;
                    }

                    
                }
                else
                {
                    ObjMdlBaseDetailsResponse.status = false;
                    ObjMdlBaseDetailsResponse.message = "Error Occurred in Obtaining Base Details";
                }

            }
            catch (Exception ex)
            {
                ObjMdlBaseDetailsResponse.status = false;
                ObjMdlBaseDetailsResponse.message = "Error Occurred in Obtaining Base Details";
            }
            return ObjMdlBaseDetailsResponse;
        }

        public void DaInstitutionProbeList(string institution_gid, MdlInstitutionProbe values)
        {
            msSQL = "  select institutionprobedetails_gid,api_name,apicall_status, date_format(created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from ocs_trn_tinstitutionprobedetails where institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinstitutionprobe_list = new List<institutionprobe_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getinstitutionprobe_list.Add(new institutionprobe_list
                    {
                        institutionprobedetails_gid = (dr_datarow["institutionprobedetails_gid"].ToString()),
                        api_name = (dr_datarow["api_name"].ToString()),
                        apicall_status = (dr_datarow["apicall_status"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                    });
                }
                values.institutionprobe_list = getinstitutionprobe_list;
            }
            dt_datatable.Dispose();
        }

        public MdlComprehensiveDetailsResponse DaGetComprehensiveDetails(string employee_gid, MdlComprehensiveDetailsRequest Values)
        {
            MdlComprehensiveDetailsResponse ObjMdlComprehensiveDetailsResponse = new MdlComprehensiveDetailsResponse();
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings["probeapicompanyurl"].ToString() + Values.pan + "/comprehensive-details?identifier_type=PAN");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-api-key", ConfigurationManager.AppSettings["probeapicompanykey"].ToString());
                request.AddHeader("x-api-version", "1.3");
                request.AddHeader("accept", "application/json");
                IRestResponse response = client.Execute(request);
                ObjMdlComprehensiveDetailsResponse = JsonConvert.DeserializeObject<MdlComprehensiveDetailsResponse>(response.Content);
                

                msSQL = "delete from ocs_trn_tinstitutionprobedetails where institution_gid='" + Values.institution_gid + "' and api_name='Comprehensive Details'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                response.Content = response.Content.Replace("'", "");

                msGetGid = objcmnfunctions.GetMasterGID("IPDT");
                msSQL = " insert into ocs_trn_tinstitutionprobedetails(" +

                        " institutionprobedetails_gid," +
                        " institution_gid," +
                        " application_gid," +
                        " api_name," +

                        " response," +
                        " apicall_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +

                        "'" + msGetGid + "'," +
                        "'" + Values.institution_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + "Comprehensive Details" + "',";

                if (response.StatusCode.ToString() == "OK")
                {
                    msSQL += "'" + response.Content + "'," +
                            "'" + "Success" + "',";
                }
                else
                {
                    msSQL += "null," +
                          "'" + "Failed" + "',";
                }

                msSQL += "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGidLog = objcmnfunctions.GetMasterGID("PDTL");
                    msSQL = " insert into ocs_trn_tinstitutionprobedetailslog(" +
                        " institutionprobedetailslog_gid," +
                        " institutionprobedetails_gid," +
                        " institution_gid," +
                        " application_gid," +
                        " api_name," +

                        " response," +
                        " apicall_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +

                        "'" + msGetGidLog + "'," +
                        "'" + msGetGid + "'," +
                        "'" + Values.institution_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + "Comprehensive Details" + "',";

                    if (response.StatusCode.ToString() == "OK")
                    {
                        msSQL += "'" + response.Content + "'," +
                                "'" + "Success" + "',";
                    }
                    else
                    {
                        msSQL += "null," +
                              "'" + "Failed" + "',";
                    }

                    msSQL += "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    if (ObjMdlComprehensiveDetailsResponse.metadata != null)
                    {
                        ObjMdlComprehensiveDetailsResponse.status = true;
                        ObjMdlComprehensiveDetailsResponse.message = "Comprehensive Details Obtained Successfully..!";
                    }
                    else
                    {
                        ObjMdlComprehensiveDetailsResponse.status = false;
                        ObjMdlComprehensiveDetailsResponse.message = ErrorResponseProbe.errorResponse + ObjMdlComprehensiveDetailsResponse.message;
                    }
                }
                else
                {
                    ObjMdlComprehensiveDetailsResponse.status = false;
                    ObjMdlComprehensiveDetailsResponse.message = "Error Occurred in Obtaining Comprehensive Details";
                }

               

            }
            catch (Exception ex)
            {
                ObjMdlComprehensiveDetailsResponse.status = false;
                ObjMdlComprehensiveDetailsResponse.message = "Error Occurred in Obtaining Comprehensive Details";
            }
            return ObjMdlComprehensiveDetailsResponse;
        }

        public MdlDataStatusResponse DaGetDataStatus(string employee_gid, MdlDataStatusRequest Values)
        {
            MdlDataStatusResponse ObjMdlDataStatusResponse = new MdlDataStatusResponse();
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings["probeapicompanyurl"].ToString() + Values.pan + "/datastatus?identifier_type=PAN");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-api-key", ConfigurationManager.AppSettings["probeapicompanykey"].ToString());
                request.AddHeader("x-api-version", "1.3");
                request.AddHeader("accept", "application/json");
                IRestResponse response = client.Execute(request);
                ObjMdlDataStatusResponse = JsonConvert.DeserializeObject<MdlDataStatusResponse>(response.Content);
                

                msSQL = "delete from ocs_trn_tinstitutionprobedetails where institution_gid='" + Values.institution_gid + "' and api_name='Data Status'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                response.Content = response.Content.Replace("'", "");

                msGetGid = objcmnfunctions.GetMasterGID("IPDT");
                msSQL = " insert into ocs_trn_tinstitutionprobedetails(" +

                        " institutionprobedetails_gid," +
                        " institution_gid," +
                        " application_gid," +
                        " api_name," +

                        " response," +
                        " apicall_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +

                        "'" + msGetGid + "'," +
                        "'" + Values.institution_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + "Data Status" + "',";

                if (response.StatusCode.ToString() == "OK")
                {
                    msSQL += "'" + response.Content + "'," +
                            "'" + "Success" + "',";
                }
                else
                {
                    msSQL += "null," +
                          "'" + "Failed" + "',";
                }

                msSQL += "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if(mnResult == 1)
                {
                    msGetGidLog = objcmnfunctions.GetMasterGID("PDTL");
                    msSQL = " insert into ocs_trn_tinstitutionprobedetailslog(" +
                        " institutionprobedetailslog_gid," +
                        " institutionprobedetails_gid," +
                        " institution_gid," +
                        " application_gid," +
                        " api_name," +

                        " response," +
                        " apicall_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGidLog + "'," +
                        "'" + msGetGid + "'," +
                        "'" + Values.institution_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        "'" + "Data Status" + "',";

                    if (response.StatusCode.ToString() == "OK")
                    {
                        msSQL += "'" + response.Content + "'," +
                                "'" + "Success" + "',";
                    }
                    else
                    {
                        msSQL += "null," +
                              "'" + "Failed" + "',";
                    }

                    msSQL += "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (ObjMdlDataStatusResponse.metadata != null)
                    {
                        ObjMdlDataStatusResponse.status = true;
                        ObjMdlDataStatusResponse.message = "Data Status Obtained Successfully..!";
                    }
                    else
                    {
                        ObjMdlDataStatusResponse.status = false;
                        ObjMdlDataStatusResponse.message = ErrorResponseProbe.errorResponse + ObjMdlDataStatusResponse.message;
                    }

                }
                else
                {
                    ObjMdlDataStatusResponse.status = false;
                    ObjMdlDataStatusResponse.message = "Error Occurred in Obtaining Data Status";
                }

            }
            catch (Exception ex)
            {
                ObjMdlDataStatusResponse.status = false;
                ObjMdlDataStatusResponse.message = "Error Occurred in Obtaining Data Status";
            }
            return ObjMdlDataStatusResponse;
        }

        public MdlDocDetailsResponse DaGetArticleOfAssociationDoc(string employee_gid, MdlComprehensiveDetailsRequest Values)
        {
            MdlDocDetailsResponse ObjMdlDocDetailsResponse = new MdlDocDetailsResponse();
            try
            {
                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                lsdocumentabspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                {
                    if ((!System.IO.Directory.Exists(lsdocumentabspath)))
                        System.IO.Directory.CreateDirectory(lsdocumentabspath);
                }

                fileName = Values.pan + "ArticleOfAssociation.pdf";
                lsdocumentabspath = lsdocumentabspath + Values.pan + "ArticleOfAssociation.pdf";

                var client = new RestClient(ConfigurationManager.AppSettings["probeapicompanyreporturl"].ToString() + Values.pan + "/reference-document?type=AoA&identifier_type=PAN");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-api-key", ConfigurationManager.AppSettings["probeapicompanykey"].ToString());
                request.AddHeader("x-api-version", "1.3");
                request.AddHeader("accept", "application/pdf");
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    byte[] buffer = response.RawBytes;

                    MemoryStream ms = new MemoryStream(buffer);

                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + fileName, ms);
                    ms.Close();
                    lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";




                    msSQL = "delete from ocs_trn_tinstitutionprobedocumentdetails where institution_gid='" + Values.institution_gid + "' and api_name='Article of Association'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msGetGid = objcmnfunctions.GetMasterGID("IPDD");
                    msSQL = " insert into ocs_trn_tinstitutionprobedocumentdetails(" +

                            " institutionprobedocumentdetails_gid," +
                            " institution_gid," +
                            " application_gid," +
                            " api_name," +
                            " probedocument_name," +
                            " probedocument_path," +
                            " apicall_status," +
                            " created_by," +
                            " created_date)" +
                            " values(" +

                            "'" + msGetGid + "'," +
                            "'" + Values.institution_gid + "'," +
                            "'" + Values.application_gid + "'," +
                            "'" + "Article of Association" + "'," +
                            "'" + fileName + "'," +
                            "'" + lspath + fileName + "'," +
                            "'" + "Success" + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        msGetGidLog = objcmnfunctions.GetMasterGID("PDTL");
                        msSQL = " insert into ocs_trn_tinstitutionprobedocumentdetailslog(" +

                                    " institutionprobedocumentdetailslog_gid," +
                                    " institutionprobedocumentdetails_gid," +
                                    " institution_gid," +
                                    " application_gid," +
                                    " api_name," +
                                    " probedocument_name," +
                                    " probedocument_path," +
                                    " apicall_status," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGidLog + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + Values.institution_gid + "'," +
                                    "'" + Values.application_gid + "'," +
                                    "'" + "Article of Association" + "'," +
                                    "'" + fileName + "'," +
                                    "'" + lspath + fileName + "'," +
                                    "'" + "Success" + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        ObjMdlDocDetailsResponse.status = true;
                        ObjMdlDocDetailsResponse.message = "Article of Association Document obtained Successfully..!";

                    }
                    else
                    {
                        ObjMdlDocDetailsResponse.status = false;
                        ObjMdlDocDetailsResponse.message = "Error Occurred in Obtaining Article Of Association Document";
                    }
                }
                else
                {
                    ObjMdlDocDetailsResponse.status = false;
                    ObjMdlDocDetailsResponse.message = ErrorResponseProbe.errorResponse + response.StatusDescription;
                }

            }
            catch (Exception ex)
            {
                ObjMdlDocDetailsResponse.status = false;
                ObjMdlDocDetailsResponse.message = "Error Occurred in Obtaining Article Of Association Document";
            }
            return ObjMdlDocDetailsResponse;
        }

        public void DaInstitutionProbeDocList(string institution_gid, MdlInstitutionProbeDoc values)
        {
            msSQL = " select institutionprobedocumentdetails_gid,api_name,probedocument_name,date_format(created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " probedocument_path from ocs_trn_tinstitutionprobedocumentdetails " +
                                 " where institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinstitutionprobedoc_list = new List<institutionprobedoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getinstitutionprobedoc_list.Add(new institutionprobedoc_list
                    {

                        institutionprobedocumentdetails_gid = dt["institutionprobedocumentdetails_gid"].ToString(),
                        api_name = dt["api_name"].ToString(),
                        probedocument_name = dt["probedocument_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        probedocument_path = objcmnstorage.EncryptData(dt["probedocument_path"].ToString()),                        
                    });
                    values.institutionprobedoc_list = getinstitutionprobedoc_list;
                }
            }
            dt_datatable.Dispose();
        }

        public MdlDocDetailsResponse DaGetMemorandumOfAssociationDoc(string employee_gid, MdlProbeDocDetailsRequest Values)
        {
            MdlDocDetailsResponse ObjMdlDocDetailsResponse = new MdlDocDetailsResponse();
            try
            {
                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                lsdocumentabspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                {
                    if ((!System.IO.Directory.Exists(lsdocumentabspath)))
                        System.IO.Directory.CreateDirectory(lsdocumentabspath);
                }

                fileName = Values.pan + "MemorandumOfAssociation.pdf";
                lsdocumentabspath = lsdocumentabspath + Values.pan + "MemorandumOfAssociation.pdf";

                var client = new RestClient(ConfigurationManager.AppSettings["probeapicompanyreporturl"].ToString() + Values.pan + "/reference-document?type=MoA&identifier_type=PAN");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-api-key", ConfigurationManager.AppSettings["probeapicompanykey"].ToString());
                request.AddHeader("x-api-version", "1.3");
                request.AddHeader("accept", "application/pdf");

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    byte[] buffer = response.RawBytes;

                    MemoryStream ms = new MemoryStream(buffer);

                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + fileName, ms);
                    ms.Close();
                    lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";



                    msSQL = "delete from ocs_trn_tinstitutionprobedocumentdetails where institution_gid='" + Values.institution_gid + "' and api_name='Certificate Of Incorporation'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msGetGid = objcmnfunctions.GetMasterGID("IPDD");
                    msSQL = " insert into ocs_trn_tinstitutionprobedocumentdetails(" +
                            " institutionprobedocumentdetails_gid," +
                            " institution_gid," +
                            " application_gid," +
                            " api_name," +
                            " probedocument_name," +
                            " probedocument_path," +
                            " apicall_status," +
                            " created_by," +
                            " created_date)" +
                            " values(" +

                            "'" + msGetGid + "'," +
                            "'" + Values.institution_gid + "'," +
                            "'" + Values.application_gid + "'," +
                            "'" + "Memorandum of Association" + "'," +
                            "'" + fileName + "'," +
                            "'" + lspath + fileName + "'," +
                            "'" + "Success" + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        msGetGidLog = objcmnfunctions.GetMasterGID("PDTL");
                        msSQL = " insert into ocs_trn_tinstitutionprobedocumentdetailslog(" +

                                    " institutionprobedocumentdetailslog_gid," +
                                    " institutionprobedocumentdetails_gid," +
                                    " institution_gid," +
                                    " application_gid," +
                                    " api_name," +
                                    " probedocument_name," +
                                    " probedocument_path," +
                                    " apicall_status," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGidLog + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + Values.institution_gid + "'," +
                                    "'" + Values.application_gid + "'," +
                                    "'" + "Memorandum of Association" + "'," +
                                    "'" + fileName + "'," +
                                    "'" + lspath + fileName + "'," +
                                    "'" + "Success" + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        ObjMdlDocDetailsResponse.status = true;
                        ObjMdlDocDetailsResponse.message = "Memorandum of Association Document obtained Successfully..!";

                    }
                    else
                    {
                        ObjMdlDocDetailsResponse.status = false;
                        ObjMdlDocDetailsResponse.message = "Error Occurred in Obtaining Memorandum Of Association Document";
                    }
                }
                else
                {
                    ObjMdlDocDetailsResponse.status = false;
                    ObjMdlDocDetailsResponse.message = ErrorResponseProbe.errorResponse + response.StatusDescription;
                }


            }
            catch (Exception ex)
            {
                ObjMdlDocDetailsResponse.status = false;
                ObjMdlDocDetailsResponse.message = "Error Occurred in Obtaining Memorandum Of Association Document";
            }
            return ObjMdlDocDetailsResponse;
        }

        public MdlDocDetailsResponse DaGetCertificateOfIncorporationDoc(string employee_gid, MdlProbeDocDetailsRequest Values)
        {
            MdlDocDetailsResponse ObjMdlDocDetailsResponse = new MdlDocDetailsResponse();
            try
            {
                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                lsdocumentabspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                {
                    if ((!System.IO.Directory.Exists(lsdocumentabspath)))
                        System.IO.Directory.CreateDirectory(lsdocumentabspath);
                }

                fileName = Values.pan + "CertificateOfIncorporation.pdf";
                lsdocumentabspath = lsdocumentabspath + Values.pan + "CertificateOfIncorporation.pdf";

                var client = new RestClient(ConfigurationManager.AppSettings["probeapicompanyreporturl"].ToString() + Values.pan + "/reference-document?type=CoI&identifier_type=PAN");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-api-key", ConfigurationManager.AppSettings["probeapicompanykey"].ToString());
                request.AddHeader("x-api-version", "1.3");
                request.AddHeader("accept", "application/pdf");

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    byte[] buffer = response.RawBytes;

                    MemoryStream ms = new MemoryStream(buffer);

                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + fileName, ms);
                    ms.Close();
                    lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                    msSQL = "delete from ocs_trn_tinstitutionprobedocumentdetails where institution_gid='" + Values.institution_gid + "' and api_name='Certificate Of Incorporation'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msGetGid = objcmnfunctions.GetMasterGID("IPDD");
                    msSQL = " insert into ocs_trn_tinstitutionprobedocumentdetails(" +

                            " institutionprobedocumentdetails_gid," +
                            " institution_gid," +
                            " application_gid," +
                            " api_name," +
                            " probedocument_name," +
                            " probedocument_path," +
                            " apicall_status," +
                            " created_by," +
                            " created_date)" +
                            " values(" +

                            "'" + msGetGid + "'," +
                            "'" + Values.institution_gid + "'," +
                            "'" + Values.application_gid + "'," +
                            "'" + "Certificate Of Incorporation" + "'," +
                            "'" + fileName + "'," +
                            "'" + lspath + fileName + "'," +
                            "'" + "Success" + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        msGetGidLog = objcmnfunctions.GetMasterGID("PDTL");
                        msSQL = " insert into ocs_trn_tinstitutionprobedocumentdetailslog(" +

                                    " institutionprobedocumentdetailslog_gid," +
                                    " institutionprobedocumentdetails_gid," +
                                    " institution_gid," +
                                    " application_gid," +
                                    " api_name," +
                                    " probedocument_name," +
                                    " probedocument_path," +
                                    " apicall_status," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGidLog + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + Values.institution_gid + "'," +
                                    "'" + Values.application_gid + "'," +
                                    "'" + "Certificate Of Incorporation" + "'," +
                                    "'" + fileName + "'," +
                                    "'" + lspath + fileName + "'," +
                                    "'" + "Success" + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        ObjMdlDocDetailsResponse.status = true;
                        ObjMdlDocDetailsResponse.message = "Certificate Of Incorporation Document obtained Successfully..!";

                    }
                    else
                    {
                        ObjMdlDocDetailsResponse.status = false;
                        ObjMdlDocDetailsResponse.message = "Error Occurred in Obtaining Certificate Of Incorporation Document";
                    }
                }
                else
                {
                    ObjMdlDocDetailsResponse.status = false;
                    ObjMdlDocDetailsResponse.message = ErrorResponseProbe.errorResponse + response.StatusDescription;
                }


            }
            catch (Exception ex)
            {
                ObjMdlDocDetailsResponse.status = false;
                ObjMdlDocDetailsResponse.message = "Error Occurred in Obtaining Certificate Of Incorporation Document";
            }
            return ObjMdlDocDetailsResponse;
        }

        public MdlDocDetailsResponse DaGetFormMGT7Doc(string employee_gid, MdlProbeDocDetailsRequest Values)
        {
            MdlDocDetailsResponse ObjMdlDocDetailsResponse = new MdlDocDetailsResponse();
            try
            {
                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                lsdocumentabspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                {
                    if ((!System.IO.Directory.Exists(lsdocumentabspath)))
                        System.IO.Directory.CreateDirectory(lsdocumentabspath);
                }

                fileName = Values.pan + "MGT7Form.pdf";
                lsdocumentabspath = lsdocumentabspath + Values.pan + "MGT7Form.pdf";

                var client = new RestClient(ConfigurationManager.AppSettings["probeapicompanyreporturl"].ToString() + Values.pan + "/reference-document?type=MGT-7&identifier_type=PAN");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-api-key", ConfigurationManager.AppSettings["probeapicompanykey"].ToString());
                request.AddHeader("x-api-version", "1.3");
                request.AddHeader("accept", "application/pdf");

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    byte[] buffer = response.RawBytes;

                    MemoryStream ms = new MemoryStream(buffer);

                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + fileName, ms);
                    ms.Close();
                    lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                    msSQL = "delete from ocs_trn_tinstitutionprobedocumentdetails where institution_gid='" + Values.institution_gid + "' and api_name='MGT-7 Form'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msGetGid = objcmnfunctions.GetMasterGID("IPDD");
                    msSQL = " insert into ocs_trn_tinstitutionprobedocumentdetails(" +

                            " institutionprobedocumentdetails_gid," +
                            " institution_gid," +
                            " application_gid," +
                            " api_name," +
                            " probedocument_name," +
                            " probedocument_path," +
                            " apicall_status," +
                            " created_by," +
                            " created_date)" +
                            " values(" +

                            "'" + msGetGid + "'," +
                            "'" + Values.institution_gid + "'," +
                            "'" + Values.application_gid + "'," +
                            "'" + "MGT-7 Form" + "'," +
                            "'" + fileName + "'," +
                            "'" + lspath + fileName + "'," +
                            "'" + "Success" + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        msGetGidLog = objcmnfunctions.GetMasterGID("PDTL");
                        msSQL = " insert into ocs_trn_tinstitutionprobedocumentdetailslog(" +

                                    " institutionprobedocumentdetailslog_gid," +
                                    " institutionprobedocumentdetails_gid," +
                                    " institution_gid," +
                                    " application_gid," +
                                    " api_name," +
                                    " probedocument_name," +
                                    " probedocument_path," +
                                    " apicall_status," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGidLog + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + Values.institution_gid + "'," +
                                    "'" + Values.application_gid + "'," +
                                    "'" + "MGT-7 Form" + "'," +
                                    "'" + fileName + "'," +
                                    "'" + lspath + fileName + "'," +
                                    "'" + "Success" + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        ObjMdlDocDetailsResponse.status = true;
                        ObjMdlDocDetailsResponse.message = "MGT-7 Form Document obtained Successfully..!";

                    }
                    else
                    {
                        ObjMdlDocDetailsResponse.status = false;
                        ObjMdlDocDetailsResponse.message = "Error Occurred in Obtaining MGT-7 Form Document";
                    }
                }
                else
                {
                    ObjMdlDocDetailsResponse.status = false;
                    ObjMdlDocDetailsResponse.message = ErrorResponseProbe.errorResponse + response.StatusDescription;
                }

            }
            catch (Exception ex)
            {
                ObjMdlDocDetailsResponse.status = false;
                ObjMdlDocDetailsResponse.message = "Error Occurred";
            }
            return ObjMdlDocDetailsResponse;
        }

        public MdlDocDetailsResponse DaGetFormAOC4Doc(string employee_gid, MdlProbeDocDetailsRequest Values)
        {
            MdlDocDetailsResponse ObjMdlDocDetailsResponse = new MdlDocDetailsResponse();
            try
            {
                msSQL = "SELECT response FROM ocs_trn_tinstitutionprobedetails a where institution_gid='" + Values.institution_gid + "' and api_name='Comprehensive Details'";
                responseDoc = objdbconn.GetExecuteScalar(msSQL);

                MdlComprehensiveDetailsResponse values = new MdlComprehensiveDetailsResponse();
                values = JsonConvert.DeserializeObject<MdlComprehensiveDetailsResponse>(responseDoc);

                documentId = values.data.financials[0].bs.metadata.doc_id;
                documentId = documentId.Replace("/", "%2F");

                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                lsdocumentabspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                {
                    if ((!System.IO.Directory.Exists(lsdocumentabspath)))
                        System.IO.Directory.CreateDirectory(lsdocumentabspath);
                }



                fileName = Values.pan + "AOC4Form.pdf";
                lsdocumentabspath = lsdocumentabspath + Values.pan + "AOC4Form.pdf";

                var client = new RestClient(ConfigurationManager.AppSettings["probeapicompanyreporturl"].ToString() + Values.pan + "/reference-document-by-id?doc-id=" + documentId + "&identifier_type=PAN");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-api-key", ConfigurationManager.AppSettings["probeapicompanykey"].ToString());
                request.AddHeader("x-api-version", "1.3");
                request.AddHeader("accept", "application/pdf");

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    byte[] buffer = response.RawBytes;


                    MemoryStream ms = new MemoryStream(buffer);

                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "Master/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + fileName, ms);
                    ms.Close();
                    lspath = "erpdocument" + "/" + lscompany_code + "/" + "Master/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                    msSQL = "delete from ocs_trn_tinstitutionprobedocumentdetails where institution_gid='" + Values.institution_gid + "' and api_name='AOC-4 Form'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msGetGid = objcmnfunctions.GetMasterGID("IPDD");
                    msSQL = " insert into ocs_trn_tinstitutionprobedocumentdetails(" +

                            " institutionprobedocumentdetails_gid," +
                            " institution_gid," +
                            " application_gid," +
                            " api_name," +
                            " probedocument_name," +
                            " probedocument_path," +
                            " apicall_status," +
                            " created_by," +
                            " created_date)" +
                            " values(" +

                            "'" + msGetGid + "'," +
                            "'" + Values.institution_gid + "'," +
                            "'" + Values.application_gid + "'," +
                            "'" + "AOC-4 Form" + "'," +
                            "'" + fileName + "'," +
                            "'" + lspath + fileName + "'," +
                            "'" + "Success" + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        msGetGidLog = objcmnfunctions.GetMasterGID("PDTL");
                        msSQL = " insert into ocs_trn_tinstitutionprobedocumentdetailslog(" +

                                    " institutionprobedocumentdetailslog_gid," +
                                    " institutionprobedocumentdetails_gid," +
                                    " institution_gid," +
                                    " application_gid," +
                                    " api_name," +
                                    " probedocument_name," +
                                    " probedocument_path," +
                                    " apicall_status," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGidLog + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + Values.institution_gid + "'," +
                                    "'" + Values.application_gid + "'," +
                                    "'" + "AOC-4 Form" + "'," +
                                    "'" + fileName + "'," +
                                    "'" + lspath + fileName + "'," +
                                    "'" + "Success" + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        ObjMdlDocDetailsResponse.status = true;
                        ObjMdlDocDetailsResponse.message = "AOC-4 Form Document obtained Successfully..!";

                    }
                    else
                    {
                        ObjMdlDocDetailsResponse.status = false;
                        ObjMdlDocDetailsResponse.message = "Error Occurred in Obtaining AOC-4 Form Document";
                    }
                }
                else
                {
                    ObjMdlDocDetailsResponse.status = false;
                    ObjMdlDocDetailsResponse.message = ErrorResponseProbe.errorResponse + response.StatusDescription;
                }


            }
            catch (Exception ex)
            {
                ObjMdlDocDetailsResponse.status = false;
                ObjMdlDocDetailsResponse.message = "Error Occurred in Obtaining AOC-4 Form Document";
            }
            return ObjMdlDocDetailsResponse;
        }

        public void DaInstitutionProbeLogList(string application_gid, MdlInstitutionProbe values)
        {
            msSQL = "  select institutionprobedetailslog_gid,api_name,apicall_status, date_format(created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from ocs_trn_tinstitutionprobedetailslog where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinstitutionprobelog_list = new List<institutionprobelog_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getinstitutionprobelog_list.Add(new institutionprobelog_list
                    {
                        institutionprobedetailslog_gid = (dr_datarow["institutionprobedetailslog_gid"].ToString()),
                        api_name = (dr_datarow["api_name"].ToString()),
                        apicall_status = (dr_datarow["apicall_status"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                    });
                }
                values.institutionprobelog_list = getinstitutionprobelog_list;
            }
            dt_datatable.Dispose();
        }

        public void DaInstitutionProbeDocLogList(string application_gid, MdlInstitutionProbeDoc values)
        {
            msSQL = " select institutionprobedocumentdetailslog_gid,api_name,apicall_status,date_format(created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from ocs_trn_tinstitutionprobedocumentdetailslog " +
                    " where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinstitutionprobedoclog_list = new List<institutionprobedoclog_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getinstitutionprobedoclog_list.Add(new institutionprobedoclog_list
                    {

                        institutionprobedocumentdetailslog_gid = dt["institutionprobedocumentdetailslog_gid"].ToString(),
                        api_name = dt["api_name"].ToString(),
                        apicall_status = dt["apicall_status"].ToString(),
                        created_date = dt["created_date"].ToString(),                       
                    });
                    values.institutionprobedoclog_list = getinstitutionprobedoclog_list;
                }
            }
            dt_datatable.Dispose();
        }

      

    }
}