using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using ems.mastersamagro.Models;
using ems.utilities.Functions;
using System.Data;
using System.Data.Odbc;
//using vcidex_kyc.Models;
using System.Text;
using System.Text.RegularExpressions;
using ems.storage.Functions;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess provide access for various events (Get InstitutionDetails, 
    ///  Probe - Institution & Individual, Comprehensive, FormMGT7, FormAOC4, Article Of Association) in Supplier ProbeAPI.
    /// </summary>
    /// <remarks>Written by Praveen </remarks>
    public class DaAgrSuprProbeAPI
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetGidLog, lscompany_code, lsdocumentabspath, lsdocumentrelpath, fileName, documentId, responseDoc;
        int mnResult;

        public void DaGetInstitutionDetails(string institution_gid, MdlMstInstitutionAdd values)
        {
            try
            {
                msSQL = " select company_name, companypan_no, cin_no" +
                        " from agr_mst_tsuprinstitution where institution_gid='" + institution_gid + "'";
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
                ObjMdlBaseDetailsResponse.status = true;

                msSQL = "delete from agr_trn_tsuprinstitutionprobedetails where institution_gid='" + Values.institution_gid + "' and api_name='Base Details'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                response.Content = response.Content.Replace("'", "");

                msGetGid = objcmnfunctions.GetMasterGID("IPDT");
                msSQL = " insert into agr_trn_tsuprinstitutionprobedetails(" +

                        " institutionprobedetails_gid," +
                        " institution_gid," +
                        " application_gid," +
                        //" institution_type," +
                        " api_name," +

                        " response," +
                        " apicall_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +

                        "'" + msGetGid + "'," +
                        "'" + Values.institution_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        //"'" + "Company" + "'," +
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

                if (mnResult == 1)
                {
                    msGetGidLog = objcmnfunctions.GetMasterGID("PDTL");
                    msSQL = " insert into agr_trn_tsuprinstitutionprobedetailslog(" +
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

                    ObjMdlBaseDetailsResponse.status = true;
                    ObjMdlBaseDetailsResponse.message = "Base Details Obtained Successfully..!";
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
            //msSQL = "  select institutionprobedetails_gid,institution_type,api_name,apicall_status, date_format(created_date,'%d-%m-%Y %h:%i %p') as created_date" +
            //        " from agr_trn_tsuprinstitutionprobedetails where institution_gid='" + institution_gid + "'";
            msSQL = "  select institutionprobedetails_gid,api_name,apicall_status, date_format(created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                   " from agr_trn_tsuprinstitutionprobedetails where institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinstitutionprobe_list = new List<institutionprobe_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getinstitutionprobe_list.Add(new institutionprobe_list
                    {
                        institutionprobedetails_gid = (dr_datarow["institutionprobedetails_gid"].ToString()),
                        //institution_type = (dr_datarow["institution_type"].ToString()),
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
                ObjMdlComprehensiveDetailsResponse.status = true;

                msSQL = "delete from agr_trn_tsuprinstitutionprobedetails where institution_gid='" + Values.institution_gid + "' and api_name='Comprehensive Details'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                response.Content = response.Content.Replace("'", "");

                msGetGid = objcmnfunctions.GetMasterGID("IPDT");
                msSQL = " insert into agr_trn_tsuprinstitutionprobedetails(" +

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
                    msSQL = " insert into agr_trn_tsuprinstitutionprobedetailslog(" +
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


                    ObjMdlComprehensiveDetailsResponse.status = true;
                    ObjMdlComprehensiveDetailsResponse.message = "Comprehensive Details Obtained Successfully..!";
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
                ObjMdlDataStatusResponse.status = true;

                msSQL = "delete from agr_trn_tsuprinstitutionprobedetails where institution_gid='" + Values.institution_gid + "' and api_name='Data Status'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                response.Content = response.Content.Replace("'", "");

                msGetGid = objcmnfunctions.GetMasterGID("IPDT");
                msSQL = " insert into agr_trn_tsuprinstitutionprobedetails(" +

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

                if (mnResult == 1)
                {
                    msGetGidLog = objcmnfunctions.GetMasterGID("PDTL");
                    msSQL = " insert into agr_trn_tsuprinstitutionprobedetailslog(" +
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

                    ObjMdlDataStatusResponse.status = true;
                    ObjMdlDataStatusResponse.message = "Data Status Obtained Successfully..!";

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
                //lsdocumentabspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                lsdocumentabspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

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
                //IRestResponse response = client.Execute(request);

                byte[] buffer = client.DownloadData(request);


                MemoryStream ms = new MemoryStream(buffer);
                FileStream file = new FileStream(lsdocumentabspath, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                file.Close();
                ms.Close();

                //lsdocumentrelpath = "../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                lsdocumentrelpath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                msSQL = "delete from agr_trn_tinstitutionprobedocumentdetails where institution_gid='" + Values.institution_gid + "' and api_name='Article of Association'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("IPDD");
                msSQL = " insert into agr_trn_tinstitutionprobedocumentdetails(" +

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
                        "'" + lsdocumentrelpath + fileName + "'," +
                        "'" + "Success" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGidLog = objcmnfunctions.GetMasterGID("PDTL");
                    msSQL = " insert into agr_trn_tinstitutionprobedocumentdetailslog(" +

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
                                "'" + lsdocumentrelpath + fileName + "'," +
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
                    " probedocument_path from agr_trn_tinstitutionprobedocumentdetails " +
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
                        //probedocument_path = HttpContext.Current.Server.MapPath(dt["probedocument_path"].ToString()),
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
                //lsdocumentabspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                lsdocumentabspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

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

                byte[] buffer = client.DownloadData(request);


                MemoryStream ms = new MemoryStream(buffer);
                FileStream file = new FileStream(lsdocumentabspath, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                file.Close();
                ms.Close();

                //lsdocumentrelpath = "../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                lsdocumentrelpath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                msSQL = "delete from agr_trn_tinstitutionprobedocumentdetails where institution_gid='" + Values.institution_gid + "' and api_name='Certificate Of Incorporation'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("IPDD");
                msSQL = " insert into agr_trn_tinstitutionprobedocumentdetails(" +

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
                        "'" + lsdocumentrelpath + fileName + "'," +
                        "'" + "Success" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGidLog = objcmnfunctions.GetMasterGID("PDTL");
                    msSQL = " insert into agr_trn_tinstitutionprobedocumentdetailslog(" +

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
                                "'" + lsdocumentrelpath + fileName + "'," +
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
                //lsdocumentabspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                lsdocumentabspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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

                byte[] buffer = client.DownloadData(request);


                MemoryStream ms = new MemoryStream(buffer);
                FileStream file = new FileStream(lsdocumentabspath, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                file.Close();
                ms.Close();

                //lsdocumentrelpath = "../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                lsdocumentrelpath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                msSQL = "delete from agr_trn_tinstitutionprobedocumentdetails where institution_gid='" + Values.institution_gid + "' and api_name='Certificate Of Incorporation'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("IPDD");
                msSQL = " insert into agr_trn_tinstitutionprobedocumentdetails(" +

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
                        "'" + lsdocumentrelpath + fileName + "'," +
                        "'" + "Success" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGidLog = objcmnfunctions.GetMasterGID("PDTL");
                    msSQL = " insert into agr_trn_tinstitutionprobedocumentdetailslog(" +

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
                                "'" + lsdocumentrelpath + fileName + "'," +
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
                //lsdocumentabspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                lsdocumentabspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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

                byte[] buffer = client.DownloadData(request);


                MemoryStream ms = new MemoryStream(buffer);
                FileStream file = new FileStream(lsdocumentabspath, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                file.Close();
                ms.Close();

                //lsdocumentrelpath = "../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                lsdocumentrelpath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                msSQL = "delete from agr_trn_tinstitutionprobedocumentdetails where institution_gid='" + Values.institution_gid + "' and api_name='MGT-7 Form'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("IPDD");
                msSQL = " insert into agr_trn_tinstitutionprobedocumentdetails(" +

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
                        "'" + lsdocumentrelpath + fileName + "'," +
                        "'" + "Success" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGidLog = objcmnfunctions.GetMasterGID("PDTL");
                    msSQL = " insert into agr_trn_tinstitutionprobedocumentdetailslog(" +

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
                                "'" + lsdocumentrelpath + fileName + "'," +
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
                msSQL = "SELECT response FROM agr_trn_tsuprinstitutionprobedetails a where institution_gid='" + Values.institution_gid + "' and api_name='Comprehensive Details'";
                responseDoc = objdbconn.GetExecuteScalar(msSQL);

                MdlComprehensiveDetailsResponse values = new MdlComprehensiveDetailsResponse();
                values = JsonConvert.DeserializeObject<MdlComprehensiveDetailsResponse>(responseDoc);

                documentId = values.data.financials[0].bs.metadata.doc_id;
                documentId = documentId.Replace("/", "%2F");

                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                //lsdocumentabspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                lsdocumentabspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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

                byte[] buffer = client.DownloadData(request);


                MemoryStream ms = new MemoryStream(buffer);
                FileStream file = new FileStream(lsdocumentabspath, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                file.Close();
                ms.Close();

                //lsdocumentrelpath = "../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                lsdocumentrelpath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ProbeDocuments/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                msSQL = "delete from agr_trn_tinstitutionprobedocumentdetails where institution_gid='" + Values.institution_gid + "' and api_name='AOC-4 Form'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("IPDD");
                msSQL = " insert into agr_trn_tinstitutionprobedocumentdetails(" +

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
                        "'" + lsdocumentrelpath + fileName + "'," +
                        "'" + "Success" + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGidLog = objcmnfunctions.GetMasterGID("PDTL");
                    msSQL = " insert into agr_trn_tinstitutionprobedocumentdetailslog(" +

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
                                "'" + lsdocumentrelpath + fileName + "'," +
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
                    " from agr_trn_tsuprinstitutionprobedetailslog where application_gid='" + application_gid + "'";
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
                    " from agr_trn_tinstitutionprobedocumentdetailslog " +
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

        public MdlBaseDetailsLLPResponse DaGetBaseDetailsLLP(string employee_gid, MdlBaseDetailsRequest Values)
        {
            MdlBaseDetailsLLPResponse ObjMdlBaseDetailsLLPResponse = new MdlBaseDetailsLLPResponse();
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings["probeapillpurl"].ToString() + Values.pan + "/base-details?identifier_type=PAN");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-api-key", ConfigurationManager.AppSettings["probeapicompanykey"].ToString());
                request.AddHeader("x-api-version", "1.3");
                request.AddHeader("accept", "application/json");
                IRestResponse response = client.Execute(request);
                ObjMdlBaseDetailsLLPResponse = JsonConvert.DeserializeObject<MdlBaseDetailsLLPResponse>(response.Content);
                ObjMdlBaseDetailsLLPResponse.status = true;

                msSQL = "delete from agr_trn_tsuprinstitutionprobedetails where institution_gid='" + Values.institution_gid + "' and api_name='Base Details'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                response.Content = response.Content.Replace("'", "");

                msGetGid = objcmnfunctions.GetMasterGID("IPDT");
                msSQL = " insert into agr_trn_tsuprinstitutionprobedetails(" +

                        " institutionprobedetails_gid," +
                        " institution_gid," +
                        " application_gid," +
                        //" institution_type," +
                        " api_name," +

                        " response," +
                        " apicall_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +

                        "'" + msGetGid + "'," +
                        "'" + Values.institution_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        //"'" + "LLP" + "'," +
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

                if (mnResult == 1)
                {
                    msGetGidLog = objcmnfunctions.GetMasterGID("PDTL");
                    msSQL = " insert into agr_trn_tsuprinstitutionprobedetailslog(" +
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

                    ObjMdlBaseDetailsLLPResponse.status = true;
                    ObjMdlBaseDetailsLLPResponse.message = "Base Details Obtained Successfully..!";
                }
                else
                {
                    ObjMdlBaseDetailsLLPResponse.status = false;
                    ObjMdlBaseDetailsLLPResponse.message = "Error Occurred in Obtaining Base Details";
                }

            }
            catch (Exception ex)
            {
                ObjMdlBaseDetailsLLPResponse.status = false;
                ObjMdlBaseDetailsLLPResponse.message = "Error Occurred in Obtaining Base Details";
            }
            return ObjMdlBaseDetailsLLPResponse;
        }

        public MdlComprehensiveDetailsLLPResponse DaGetComprehensiveDetailsLLP(string employee_gid, MdlComprehensiveDetailsRequest Values)
        {
            MdlComprehensiveDetailsLLPResponse ObjMdlComprehensiveDetailsLLPResponse = new MdlComprehensiveDetailsLLPResponse();
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings["probeapillpurl"].ToString() + Values.pan + "/comprehensive-details?identifier_type=PAN");
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-api-key", ConfigurationManager.AppSettings["probeapicompanykey"].ToString());
                request.AddHeader("x-api-version", "1.3");
                request.AddHeader("accept", "application/json");
                IRestResponse response = client.Execute(request);
                ObjMdlComprehensiveDetailsLLPResponse = JsonConvert.DeserializeObject<MdlComprehensiveDetailsLLPResponse>(response.Content);
                ObjMdlComprehensiveDetailsLLPResponse.status = true;

                msSQL = "delete from agr_trn_tsuprinstitutionprobedetails where institution_gid='" + Values.institution_gid + "' and api_name='Comprehensive Details'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                response.Content = response.Content.Replace("'", "");

                msGetGid = objcmnfunctions.GetMasterGID("IPDT");
                msSQL = " insert into agr_trn_tsuprinstitutionprobedetails(" +

                        " institutionprobedetails_gid," +
                        " institution_gid," +
                        " application_gid," +
                        //" institution_type," +
                        " api_name," +

                        " response," +
                        " apicall_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +

                        "'" + msGetGid + "'," +
                        "'" + Values.institution_gid + "'," +
                        "'" + Values.application_gid + "'," +
                        //"'" + "LLP" + "'," +
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
                    msSQL = " insert into agr_trn_tsuprinstitutionprobedetailslog(" +
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


                    ObjMdlComprehensiveDetailsLLPResponse.status = true;
                    ObjMdlComprehensiveDetailsLLPResponse.message = "Comprehensive Details Obtained Successfully..!";
                }
                else
                {
                    ObjMdlComprehensiveDetailsLLPResponse.status = false;
                    ObjMdlComprehensiveDetailsLLPResponse.message = "Error Occurred in Obtaining Comprehensive Details";
                }



            }
            catch (Exception ex)
            {
                ObjMdlComprehensiveDetailsLLPResponse.status = false;
                ObjMdlComprehensiveDetailsLLPResponse.message = "Error Occurred in Obtaining Comprehensive Details";
            }
            return ObjMdlComprehensiveDetailsLLPResponse;
        }


    }
}