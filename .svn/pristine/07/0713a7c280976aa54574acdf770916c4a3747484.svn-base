using ems.mastersamagro.Models;
using ems.storage.Functions;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Web;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess provide access to supplier cheque management flow
    /// </summary>
    /// <remarks>Written by Praveen Raj.R </remarks>
    public class DaAgrSuprUdcManagement
    {
    
            dbconn objdbconn = new dbconn();
            cmnfunctions objcmnfunctions = new cmnfunctions();
            Fnazurestorage objcmnstorage = new Fnazurestorage();
            OdbcDataReader objODBCDatareader, objODBCDatareader1;
            HttpPostedFile httpPostedFile;
            DataTable dt_datatable, dt_tcontact, dt_tinstitution, dt_tgroup;
            string msSQL, msGetGid, msGetUdcGid, msGetDocumentGid, lsapp_refno, lspath, lsmakersummary_flag;
            int mnResult;
            string lsudcmanagement2cheque_gid, lsudcmanagement_gid, lsstakeholder_gid, lsstakeholder_name, lsstakeholder_type, lsdesignation_name,
            lsaccountholder_name, lsaccount_number, lsbank_name, lscheque_no, lsifsc_code, lsbranch_address, lsbranch_name, lscity, lsdistrict, lsstate,
            lsmergedbankingentity_gid, lsmergedbankingentity_name, lsspecial_condition, lsgeneral_remarks, lscts_enabled, lscheque_type, lsdate_chequetype,
            lsdate_chequepresentation, lsstatus_chequepresentation, lsdate_chequeclearance, lsstatus_chequeclearance;

            public void DaPostChequeDetail(string employee_gid, MdlCheque values)
            {
                msGetGid = objcmnfunctions.GetMasterGID("UM2C");
                msGetUdcGid = objcmnfunctions.GetMasterGID("UCMG");

                msSQL = " insert into agr_mst_tsuprudcmanagement2cheque(" +
                       " udcmanagement2cheque_gid ," +
                       " udcmanagement_gid ," +
                       " stakeholder_gid," +
                       " stakeholder_name," +
                       " stakeholder_type," +
                       " designation," +
                       " accountholder_name," +
                       " account_number," +
                       " bank_name," +
                       " cheque_no," +
                       " ifsc_code," +
                       " micr," +
                       " branch_address," +
                       " branch_name," +
                       " city," +
                       " district," +
                       " state," +
                       " mergedbankingentity_gid," +
                       " mergedbankingentity_name," +
                       " special_condition," +
                       " general_remarks," +
                       " cts_enabled," +
                       " cheque_type," +
                       " date_chequetype," +
                       " date_chequepresentation," +
                       " status_chequepresentation," +
                       " date_chequeclearance," +
                       " status_chequeclearance," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + msGetUdcGid + "'," +
                       "'" + values.stakeholder_gid + "'," +
                       "'" + values.stakeholder_name + "'," +
                       "'" + values.stakeholder_type + "'," +
                       "'" + values.designation + "'," +
                       "'" + values.accountholder_name + "'," +
                       "'" + values.account_number + "'," +
                       "'" + values.bank_name + "'," +
                       "'" + values.cheque_no + "'," +
                       "'" + values.ifsc_code + "'," +
                       "'" + values.micr + "'," +
                       "'" + values.branch_address + "'," +
                       "'" + values.branch_name + "'," +
                       "'" + values.city + "'," +
                       "'" + values.district + "'," +
                       "'" + values.state + "'," +
                       "'" + values.mergedbankingentity_gid + "'," +
                       "'" + values.mergedbankingentity_name + "'," +
                       "'" + values.special_condition + "'," +
                       "'" + values.general_remarks + "'," +
                       "'" + values.cts_enabled + "'," +
                       "'" + values.cheque_type + "',";
                if ((values.date_chequetype == null) || (values.date_chequetype == ""))
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.date_chequetype).ToString("yyyy-MM-dd") + "',";
                }
                if ((values.date_chequepresentation == null) || (values.date_chequepresentation == ""))
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.date_chequepresentation).ToString("yyyy-MM-dd") + "',";
                }
                msSQL += "'" + values.status_chequepresentation + "',";
                if ((values.date_chequeclearance == null) || (values.date_chequeclearance == ""))
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.date_chequeclearance).ToString("yyyy-MM-dd") + "',";
                }
                msSQL += "'" + values.status_chequeclearance + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    //Updates

                    msSQL = "update agr_mst_tsuprcheque2document set udcmanagement2cheque_gid ='" + msGetGid + "' where udcmanagement2cheque_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " insert into agr_mst_tsuprudcmanagement(" +
                            " udcmanagement_gid," +
                            " customer_gid," +
                            " customer_name," +
                            " application_gid," +
                            " udc_status," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetUdcGid + "'," +
                            "'" + values.customer_gid + "'," +
                            "'" + values.customer_name + "'," +
                            "'" + values.application_gid + "'," +
                            "'Completed'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Cheque details Added successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured while Adding Cheque";
                }
            }

            public void DaGetChequeSummary(string employee_gid, MdlCheque values)
            {

                msSQL = " select udcmanagement2cheque_gid, cheque_no, stakeholder_name, mergedbankingentity_name ,cheque_type ,cts_enabled " +
                        " from agr_mst_tsuprudcmanagement2cheque" +
                        " where udcmanagement_gid='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getchequeList = new List<cheque_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getchequeList.Add(new cheque_list
                        {
                            udcmanagement2cheque_gid = dt["udcmanagement2cheque_gid"].ToString(),
                            cheque_no = dt["cheque_no"].ToString(),
                            stakeholder_name = dt["stakeholder_name"].ToString(),
                            mergedbankingentity_name = dt["mergedbankingentity_name"].ToString(),
                            cheque_type = dt["cheque_type"].ToString(),
                            cts_enabled = dt["cts_enabled"].ToString(),
                        });

                    }
                }
                values.cheque_list = getchequeList;
                dt_datatable.Dispose();
            }


            public bool DaChequeDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
            {
                upload_list objdocumentmodel = new upload_list();
                HttpFileCollection httpFileCollection;
                string lsfilepath = string.Empty;
                string lsdocument_gid = string.Empty;
                MemoryStream ms_stream = new MemoryStream();
                string document_gid = string.Empty;
                string lscompany_code = string.Empty;
                //string lsdocument_title = httpRequest.Form["document_title"].ToString();
                String path = lspath;
                string project_flag = httpRequest.Form["project_flag"].ToString();

                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                //path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/ChequeDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
                path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ChequeDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
                {
                    if ((!System.IO.Directory.Exists(path)))
                        System.IO.Directory.CreateDirectory(path);
                }
                try
                {
                    if (httpRequest.Files.Count > 0)
                    {
                        string lsfirstdocument_filepath = string.Empty;
                        httpFileCollection = httpRequest.Files;
                        for (int i = 0; i < httpFileCollection.Count; i++)
                        {
                            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                            httpPostedFile = httpFileCollection[i];
                            string FileExtension = httpPostedFile.FileName;
                            string lsfile_gid = msdocument_gid;
                            FileExtension = Path.GetExtension(FileExtension).ToLower();
                            lsfile_gid = lsfile_gid + FileExtension;
                            Stream ls_readStream;
                            ls_readStream = httpPostedFile.InputStream;
                            MemoryStream ms = new MemoryStream();
                            ls_readStream.CopyTo(ms);
                            // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                            //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "SamAgro/ChequeDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                            //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                            //ms.WriteTo(file);
                            //file.Close();
                            //ms.Close();


                            bool status;
                            status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/ChequeDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                            ms.Close();
                            lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/ChequeDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                            //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "SamAgro/ChequeDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                            msGetGid = objcmnfunctions.GetMasterGID("CQ2D");
                            msGetDocumentGid = objcmnfunctions.GetMasterGID("CQDA");

                            msSQL = " delete from agr_mst_tsuprcheque2document where udcmanagement2cheque_gid = '" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = " insert into agr_mst_tsuprcheque2document( " +
                                        " cheque2document_gid," +
                                        " udcmanagement2cheque_gid," +
                                        " document_gid ," +
                                        " document_name ," +
                                        " document_path," +
                                        " created_by," +
                                        " created_date" +
                                        " )values(" +
                                        "'" + msGetGid + "'," +
                                        "'" + employee_gid + "'," +
                                        "'" + msGetDocumentGid + "'," +
                                        "'" + httpPostedFile.FileName + "'," +
                                        "'" + lspath + msdocument_gid + FileExtension + "'," +
                                        "'" + employee_gid + "'," +
                                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult == 1)
                            {
                                objfilename.status = true;
                                objfilename.message = "Cheque Document Uploaded Successfully..!";
                            }
                            else
                            {
                                objfilename.status = false;
                                objfilename.message = "Error Occured in uploading cheque document..!";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    objfilename.message = ex.ToString();
                }
                return true;
            }

            public void DaGetChequeDocumentList(string employee_gid, MdlChequeDocument values)
            {
                msSQL = " select cheque2document_gid ,document_name,document_path from agr_mst_tsuprcheque2document " +
                                     " where udcmanagement2cheque_gid ='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocumentdtlList = new List<chequedocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdocumentdtlList.Add(new chequedocument_list
                        {
                            document_name = dt["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData(HttpContext.Current.Server.MapPath(dt["document_path"].ToString())),
                            cheque2document_gid = dt["cheque2document_gid"].ToString(),
                        });
                        values.chequedocument_list = getdocumentdtlList;
                    }
                }
                dt_datatable.Dispose();
            }

            public void DaChequeDocumentDelete(string cheque2document_gid, MdlChequeDocument values)
            {
                msSQL = "delete from agr_mst_tsuprcheque2document where cheque2document_gid='" + cheque2document_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {

                    values.message = "Document Deleted Successfully";
                    values.status = true;
                }
                else
                {
                    values.message = "Error Occured";
                    values.status = false;

                }
            }

            public void DaUpdateChequeDetail(string employee_gid, MdlCheque values)
            {
                msSQL = " select mergedbankingentity_gid,mergedbankingentity_name,special_condition," +
                        " general_remarks, cts_enabled, cheque_type, date_chequetype, date_chequepresentation," +
                        " status_chequepresentation, date_chequeclearance,status_chequeclearance" +
                        " from agr_mst_tsuprudcmanagement2cheque where udcmanagement2cheque_gid='" + values.udcmanagement2cheque_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsmergedbankingentity_gid = objODBCDatareader["mergedbankingentity_gid"].ToString();
                    lsmergedbankingentity_name = objODBCDatareader["mergedbankingentity_name"].ToString();

                    lsspecial_condition = objODBCDatareader["special_condition"].ToString();
                    lsgeneral_remarks = objODBCDatareader["general_remarks"].ToString();
                    lscts_enabled = objODBCDatareader["cts_enabled"].ToString();
                    lscheque_type = objODBCDatareader["cheque_type"].ToString();
                    lsdate_chequetype = objODBCDatareader["date_chequetype"].ToString();
                    lsdate_chequepresentation = objODBCDatareader["date_chequepresentation"].ToString();
                    lsstatus_chequepresentation = objODBCDatareader["status_chequepresentation"].ToString();
                    lsdate_chequeclearance = objODBCDatareader["date_chequeclearance"].ToString();
                    lsstatus_chequeclearance = objODBCDatareader["status_chequeclearance"].ToString();

                }
                objODBCDatareader.Close();
                try
                {
                    msSQL = " update agr_mst_tsuprudcmanagement2cheque set " +
                             " mergedbankingentity_gid='" + values.mergedbankingentity_gid + "'," +
                             " mergedbankingentity_name='" + values.mergedbankingentity_name + "'," +

                    " special_condition='" + values.special_condition + "'," +
                    " general_remarks='" + values.general_remarks + "'," +
                    " cts_enabled='" + values.cts_enabled + "'," +
                    " cheque_type='" + values.cheque_type + "'," +
                    " status_chequepresentation='" + values.status_chequepresentation + "'," +
                    " status_chequeclearance='" + values.status_chequeclearance + "',";

                    try
                    {
                        if (values.date_chequetype == "" || values.date_chequetype == null)
                        {

                        }
                        else
                        {
                            msSQL += " date_chequetype='" + Convert.ToDateTime(values.date_chequetype).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        }

                        if (values.date_chequepresentation == "" || values.date_chequepresentation == null)
                        {

                        }
                        else
                        {
                            msSQL += " date_chequepresentation='" + Convert.ToDateTime(values.date_chequepresentation).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        }
                        if (values.date_chequeclearance == "" || values.date_chequeclearance == null)
                        {

                        }
                        else
                        {
                            msSQL += " date_chequeclearance='" + Convert.ToDateTime(values.date_chequeclearance).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        }

                        msSQL += " updated_by='" + employee_gid + "'," +
                                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    }
                    catch (Exception ex)
                    {
                        msSQL += " updated_by='" + employee_gid + "'," +
                                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    }


                    msSQL += " where udcmanagement2cheque_gid='" + values.udcmanagement2cheque_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult == 1)
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("U2CL");
                        msSQL = " insert into agr_mst_tsuprudcmanagement2chequeupdatelog(" +
                       " udcmanagement2chequeupdatelog_gid ," +
                       " udcmanagement2cheque_gid ," +
                       " udcmanagement_gid ," +
                       " mergedbankingentity_gid," +
                       " mergedbankingentity_name," +
                       " special_condition," +
                       " general_remarks," +
                       " cts_enabled," +
                       " cheque_type," +
                       " date_chequetype," +
                       " date_chequepresentation," +
                       " status_chequepresentation," +
                       " date_chequeclearance," +
                       " status_chequeclearance," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + values.udcmanagement2cheque_gid + "'," +
                       "'" + values.udcmanagement_gid + "'," +
                       "'" + lsmergedbankingentity_gid + "'," +
                       "'" + lsmergedbankingentity_name + "'," +
                       "'" + lsspecial_condition + "'," +
                       "'" + lsgeneral_remarks + "'," +
                       "'" + lscts_enabled + "'," +
                       "'" + lscheque_type + "'," +
                       "'" + Convert.ToDateTime(lsdate_chequetype).ToString("yyyy-MM-dd") + "'," +
                       "'" + Convert.ToDateTime(lsdate_chequepresentation).ToString("yyyy-MM-dd") + "'," +
                       "'" + lsstatus_chequepresentation + "'," +
                       "'" + Convert.ToDateTime(lsdate_chequeclearance).ToString("yyyy-MM-dd") + "'," +
                       "'" + lsstatus_chequeclearance + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        values.status = true;
                        values.message = "Cheque Details Updated Successfully";
                    }
                }
                catch (Exception ex)
                {
                    values.status = false;
                    values.message = "Error Occured..";
                }
            }

            public void DaChequeDetailsEdit(string udcmanagement2cheque_gid, MdlCheque values)
            {
                try
                {
                    msSQL = " select stakeholder_gid, stakeholder_name, stakeholder_type,designation,accountholder_name,account_number,bank_name,cheque_no," +
                        " ifsc_code, micr, branch_address,branch_name,city,district,state,mergedbankingentity_gid,mergedbankingentity_name,special_condition," +
                        " general_remarks, cts_enabled, cheque_type,date_chequetype,date_chequepresentation,status_chequepresentation,date_chequeclearance,status_chequeclearance" +
                        " from agr_mst_tsuprudcmanagement2cheque where udcmanagement2cheque_gid='" + udcmanagement2cheque_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.stakeholder_gid = objODBCDatareader["stakeholder_gid"].ToString();
                        values.stakeholder_name = objODBCDatareader["stakeholder_name"].ToString();
                        values.stakeholder_type = objODBCDatareader["stakeholder_type"].ToString();
                        values.designation = objODBCDatareader["designation"].ToString();
                        values.accountholder_name = objODBCDatareader["accountholder_name"].ToString();
                        values.account_number = objODBCDatareader["account_number"].ToString();
                        values.bank_name = objODBCDatareader["bank_name"].ToString();
                        values.cheque_no = objODBCDatareader["cheque_no"].ToString();
                        values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();

                        values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                        values.micr = objODBCDatareader["micr"].ToString();
                        values.branch_address = objODBCDatareader["branch_address"].ToString();
                        values.branch_name = objODBCDatareader["branch_name"].ToString();
                        values.city = objODBCDatareader["city"].ToString();
                        values.district = objODBCDatareader["district"].ToString();
                        values.state = objODBCDatareader["state"].ToString();
                        values.mergedbankingentity_gid = objODBCDatareader["mergedbankingentity_gid"].ToString();
                        values.mergedbankingentity_name = objODBCDatareader["mergedbankingentity_name"].ToString();

                        values.special_condition = objODBCDatareader["special_condition"].ToString();
                        values.general_remarks = objODBCDatareader["general_remarks"].ToString();
                        values.cts_enabled = objODBCDatareader["cts_enabled"].ToString();
                        values.cheque_type = objODBCDatareader["cheque_type"].ToString();
                        if (objODBCDatareader["date_chequetype"].ToString() != "" && objODBCDatareader["date_chequetype"].ToString() != null)
                        {
                            values.datechequetype = Convert.ToDateTime(objODBCDatareader["date_chequetype"].ToString());
                        }
                        values.date_chequetype = Convert.ToDateTime(objODBCDatareader["date_chequetype"]).ToString("MM-dd-yyyy");
                        if (objODBCDatareader["date_chequepresentation"].ToString() != "" && objODBCDatareader["date_chequepresentation"].ToString() != null)
                        {
                            values.datechequepresentation = Convert.ToDateTime(objODBCDatareader["date_chequepresentation"].ToString());
                        }
                        values.date_chequepresentation = Convert.ToDateTime(objODBCDatareader["date_chequepresentation"]).ToString("MM-dd-yyyy");
                        values.status_chequepresentation = objODBCDatareader["status_chequepresentation"].ToString();
                        if (objODBCDatareader["date_chequeclearance"].ToString() != "" && objODBCDatareader["date_chequeclearance"].ToString() != null)
                        {
                            values.datechequeclearance = Convert.ToDateTime(objODBCDatareader["date_chequeclearance"].ToString());
                        }
                        values.date_chequeclearance = Convert.ToDateTime(objODBCDatareader["date_chequeclearance"]).ToString("MM-dd-yyyy");
                        values.status_chequeclearance = objODBCDatareader["status_chequeclearance"].ToString();


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

            public void DaChequeList(string employee_gid, string udcmanagement_gid, MdlCheque values)
            {

                msSQL = " select udcmanagement2cheque_gid, cheque_no, stakeholder_name, stakeholder_type,cheque_type" +
                        " from agr_mst_tsuprudcmanagement2cheque" +
                        " where udcmanagement_gid='" + employee_gid + "' or udcmanagement_gid = '" + udcmanagement_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getchequeList = new List<cheque_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getchequeList.Add(new cheque_list
                        {
                            udcmanagement2cheque_gid = dt["udcmanagement2cheque_gid"].ToString(),
                            cheque_no = dt["cheque_no"].ToString(),
                            stakeholder_name = dt["stakeholder_name"].ToString(),
                            stakeholder_type = dt["stakeholder_type"].ToString(),
                            cheque_type = dt["cheque_type"].ToString(),
                        });

                    }
                }
                values.cheque_list = getchequeList;
                dt_datatable.Dispose();
            }

            public void DaChequeDocumentList(string udcmanagement2cheque_gid, MdlChequeDocument values)
            {
                msSQL = " select cheque2document_gid,document_name,document_path from agr_mst_tsuprcheque2document " +
                                     " where udcmanagement2cheque_gid='" + udcmanagement2cheque_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocumentdtlList = new List<chequedocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdocumentdtlList.Add(new chequedocument_list
                        {
                            document_name = dt["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData(HttpContext.Current.Server.MapPath(dt["document_path"].ToString())),
                            cheque2document_gid = dt["cheque2document_gid"].ToString(),
                        });
                        values.chequedocument_list = getdocumentdtlList;
                    }
                }
                dt_datatable.Dispose();
            }


            public void DaDeleteChequeDetail(string udcmanagement2cheque_gid, MdlCheque values)
            {
                msSQL = "delete from agr_mst_tsuprudcmanagement2cheque where udcmanagement2cheque_gid='" + udcmanagement2cheque_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {

                    values.message = "Cheque Details Deleted Successfully";
                    values.status = true;
                }
                else
                {
                    values.message = "Error Occured";
                    values.status = false;

                }
            }

            public void DaUdcDetailsEdit(string udcmanagement_gid, MdlUdcManagement values)
            {
                try
                {
                    msSQL = " select customer_gid,customer_name" +
                        " from agr_mst_tsuprudcmanagement where udcmanagement_gid='" + udcmanagement_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                        values.customer_name = objODBCDatareader["customer_name"].ToString();
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

            public void DaUpdateUdcDetail(string employee_gid, MdlUdcManagement values)
            {
                //Updates

                msSQL = "update agr_mst_tsuprudcmanagement2cheque set udcmanagement_gid ='" + values.udcmanagement_gid + "' where udcmanagement_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Udc Details Updated Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured";
                }

            }

            public void DaGetUdcSummary(string employee_gid, string application_gid, MdlUdcManagement values)
            {

                msSQL = " select d.udcmanagement2cheque_gid,a.udcmanagement_gid, a.customer_name , a.sanctionrefno_name, a.udc_status, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " d.cheque_no,d.stakeholder_name,d.cheque_type " +
                        " from agr_mst_tsuprudcmanagement a" +
                        " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                        " left join agr_mst_tsuprudcmanagement2cheque d on d.udcmanagement_gid=a.udcmanagement_gid " +
                        " where a.application_gid = '" + application_gid + "' order by a.udcmanagement_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getudcList = new List<udcmanagement_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getudcList.Add(new udcmanagement_list
                        {
                            udcmanagement_gid = dt["udcmanagement_gid"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            sanctionrefno_name = dt["sanctionrefno_name"].ToString(),
                            udc_status = dt["udc_status"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            udcmanagement2cheque_gid = dt["udcmanagement2cheque_gid"].ToString(),
                            cheque_no = dt["cheque_no"].ToString(),
                            stakeholder_name = dt["stakeholder_name"].ToString(),
                            cheque_type = dt["cheque_type"].ToString(),
                        });

                    }
                }
                values.udcmanagement_list = getudcList;
                dt_datatable.Dispose();
            }

            public void DaDeleteUdc(string udcmanagement_gid, MdlUdcManagement values)
            {
                msSQL = "select udcmanagement2cheque_gid from agr_mst_tsuprudcmanagement2cheque where udcmanagement_gid ='" + udcmanagement_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                string udcmanagement2cheque_gid;
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    udcmanagement2cheque_gid = (dr_datarow["udcmanagement2cheque_gid"].ToString());
                    msSQL = "delete from agr_mst_tsuprudcmanagement2cheque where udcmanagement2cheque_gid='" + udcmanagement2cheque_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                dt_datatable.Dispose();

                msSQL = "delete from agr_mst_tsuprudcmanagement where udcmanagement_gid='" + udcmanagement_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {

                    values.message = "Borrower Details Deleted Successfully";
                    values.status = true;
                }
                else
                {
                    values.message = "Error Occured";
                    values.status = false;

                }
            }

            public void DaGetDropDownUdc(string employee_gid, MdlDropDownUdc values)
            {
                //Bank Merging Entity
                msSQL = " SELECT a.bankname_gid,a.bankname_name" +
                        " FROM agr_mst_tbankname a  where status='Y' order by a.bankname_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSegment = new List<bankname_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new bankname_list
                        {
                            bankname_gid = (dr_datarow["bankname_gid"].ToString()),
                            bankname_name = (dr_datarow["bankname_name"].ToString()),
                        });
                    }
                    values.bankname_list = getSegment;
                }
                dt_datatable.Dispose();

                values.status = true;
            }

            public void DaGetCustomers(CustomersList values)
            {
                msSQL = " SELECT application_gid," +
                        " case when customer_urn<>'' then concat(customerref_name, ' / ', customer_urn)" +
                        " else concat(customerref_name, ' / ', '-') end as customerref_name" +
                        " FROM agr_mst_tsuprapplication ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSegment = new List<Customer>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new Customer
                        {
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            customerref_name = (dr_datarow["customerref_name"].ToString()),
                        });
                    }
                    values.CustomerList = getSegment;
                }
                dt_datatable.Dispose();

                values.status = true;

            }

            public void DaGetStakeholders(string application_gid, StakeholdersList values)
            {
                var getStakeholderList = new List<Stakeholder>();

                msSQL = " SELECT contact_gid, concat(first_name,' ',middle_name,' ',last_name) as contact_name," +
                        " stakeholder_type, designation_name" +
                        " FROM agr_mst_tsuprcontact " +
                        " where application_gid='" + application_gid + "' ";
                dt_tcontact = objdbconn.GetDataTable(msSQL);

                if (dt_tcontact.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_tcontact.Rows)
                    {
                        getStakeholderList.Add(new Stakeholder
                        {
                            stakeholder_gid = (dr_datarow["contact_gid"].ToString()),
                            stakeholder_name = (dr_datarow["contact_name"].ToString()),
                            stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                            designation = (dr_datarow["designation_name"].ToString()),
                        });
                    }
                }
                dt_tcontact.Dispose();

                msSQL = " SELECT institution_gid, company_name," +
                        " stakeholder_type" +
                        " FROM agr_mst_tsuprinstitution " +
                        " where application_gid='" + application_gid + "' ";
                dt_tinstitution = objdbconn.GetDataTable(msSQL);

                if (dt_tinstitution.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_tinstitution.Rows)
                    {
                        getStakeholderList.Add(new Stakeholder
                        {
                            stakeholder_gid = (dr_datarow["institution_gid"].ToString()),
                            stakeholder_name = (dr_datarow["company_name"].ToString()),
                            stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                            designation = "NA",
                        });
                    }
                }
                dt_tinstitution.Dispose();

                msSQL = " SELECT group_gid, group_name" +
                        " FROM agr_mst_tgroup " +
                        " where application_gid='" + application_gid + "' ";
                dt_tgroup = objdbconn.GetDataTable(msSQL);

                if (dt_tgroup.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_tgroup.Rows)
                    {
                        getStakeholderList.Add(new Stakeholder
                        {
                            stakeholder_gid = (dr_datarow["group_gid"].ToString()),
                            stakeholder_name = (dr_datarow["group_name"].ToString()),
                            stakeholder_type = "Group",
                            designation = "NA",
                        });
                    }
                }
                dt_tgroup.Dispose();

                values.StakeholderList = getStakeholderList;

                values.status = true;

            }

            public void DaGetUdcTempClear(string employee_gid, result values)
            {
                msSQL = "delete from agr_mst_tsuprcheque2document where udcmanagement2cheque_gid ='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from agr_mst_tsuprudcmanagement2cheque where udcmanagement_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
            }

            public void DaGetChequeMakerSummary(string employee_gid, MdlUdcManagement values)
            {
                msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,f.sanction_refno, " +
                         " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                         " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by,  " +
                         " a.creditgroup_gid,e.cadgroup_name,a.customer_urn from agr_mst_tsuprapplication a " +
                         " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " left join agr_trn_tsuprprocesstype_assign e on e.application_gid = a.application_gid " +
                         " left join agr_trn_tsuprapplication2sanction f on f.application_gid = a.application_gid " +
                         " where a.process_type = 'Accept' and e.menu_gid = '" + getMenuClass.ChequeManagement + "' and " +
                         " e.maker_gid = '" + employee_gid + "' and e.maker_approvalflag = 'N' and (select approver_approvalflag from agr_trn_tsuprprocesstype_assign where menu_gid = 'CADMGTSAN' and application_gid = a.application_gid) = 'Y'" +
                         " group by a.application_gid order by a.updated_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplicationadd_list = new List<cadapplicationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        string lsccgroup_name;
                        string lsccadmin_name;
                        string lscadgroup_name;

                        msSQL = "select group_concat(ccgroup_name) as ccgroup_name from agr_mst_tsuprapplication where application_gid='" + dt["application_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsccgroup_name = objODBCDatareader["ccgroup_name"].ToString();
                        }
                        else
                        {
                            lsccgroup_name = "";
                        }
                        objODBCDatareader.Close();
                        msSQL = "select group_concat(ccadmin_name) as ccadmin_name from agr_mst_tsuprccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsccadmin_name = objODBCDatareader["ccadmin_name"].ToString();
                        }
                        else
                        {
                            lsccadmin_name = "";
                        }
                        objODBCDatareader.Close();
                        //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from agr_trn_tsuprprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                        //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        //if (objODBCDataReader.HasRows == true)
                        //{
                        //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                        //}
                        //else
                        //{
                        //    lscadgroup_name = "";
                        //}
                        //objODBCDataReader.Close();
                        getapplicationadd_list.Add(new cadapplicationlist
                        {
                            application_no = dt["application_no"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            approval_status = dt["approval_status"].ToString(),
                            ccapproved_date = dt["ccapproved_date"].ToString(),
                            creditgroup_name = dt["creditgroup_name"].ToString(),
                            ccgroup_name = lsccgroup_name,
                            ccadmin_name = lsccadmin_name,
                            cadgroupname = dt["cadgroup_name"].ToString(),
                            cadaccepted_by = dt["cadaccepted_by"].ToString(),
                            cadaccepted_date = dt["cadaccepted_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            lsmakersummary_flag = "N",
                            sanction_refno = dt["sanction_refno"].ToString(),
                            customer_urn = dt["customer_urn"].ToString()
                        });

                    }
                }
                values.cadapplicationlist = getapplicationadd_list;
                dt_datatable.Dispose();
            }

            public void DaGetChequeFollowupMakerSummary(string employee_gid, MdlUdcManagement values)
            {
                msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name,g.sanction_refno, " +
                         " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                         " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by,  " +
                         " a.creditgroup_gid,e.cadgroup_name,f.approval_status as followup_status,a.customer_urn from agr_mst_tsuprapplication a " +
                         " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " left join agr_trn_tsuprprocesstype_assign e on e.application_gid = a.application_gid " +
                         " left join agr_trn_tsuprapplication2chequelist f on f.application_gid = a.application_gid " +
                         " left join agr_trn_tsuprapplication2sanction g on g.application_gid = a.application_gid " +
                         " where a.process_type = 'Accept' and e.approver_approvalflag = 'N' and " +
                         "  e.menu_gid = '" + getMenuClass.ChequeManagement + "' and e.maker_gid = '" + employee_gid + "'  and e.maker_approvalflag = 'Y' " +
                         " group by a.application_gid order by a.updated_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplicationadd_list = new List<cadapplicationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        string lsccgroup_name;
                        string lsccadmin_name;
                        string lscadgroup_name;

                        msSQL = "select group_concat(ccgroup_name) as ccgroup_name from agr_mst_tsuprapplication where application_gid='" + dt["application_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsccgroup_name = objODBCDatareader["ccgroup_name"].ToString();
                        }
                        else
                        {
                            lsccgroup_name = "";
                        }
                        objODBCDatareader.Close();
                        msSQL = "select group_concat(ccadmin_name) as ccadmin_name from agr_mst_tsuprccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsccadmin_name = objODBCDatareader["ccadmin_name"].ToString();
                        }
                        else
                        {
                            lsccadmin_name = "";
                        }
                        objODBCDatareader.Close();
                        //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from agr_trn_tsuprprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                        //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        //if (objODBCDataReader.HasRows == true)
                        //{
                        //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                        //}
                        //else
                        //{
                        //    lscadgroup_name = "";
                        //}
                        //objODBCDataReader.Close();
                        getapplicationadd_list.Add(new cadapplicationlist
                        {
                            application_no = dt["application_no"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            approval_status = dt["approval_status"].ToString(),
                            ccapproved_date = dt["ccapproved_date"].ToString(),
                            creditgroup_name = dt["creditgroup_name"].ToString(),
                            ccgroup_name = lsccgroup_name,
                            ccadmin_name = lsccadmin_name,
                            cadgroupname = dt["cadgroup_name"].ToString(),
                            cadaccepted_by = dt["cadaccepted_by"].ToString(),
                            cadaccepted_date = dt["cadaccepted_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            lsmakersummary_flag = "Y",
                            followup_status = dt["followup_status"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            customer_urn = dt["customer_urn"].ToString()
                        });

                    }
                }
                values.cadapplicationlist = getapplicationadd_list;
                dt_datatable.Dispose();
            }

            public void DaGetChequeCheckerSummary(string employee_gid, MdlUdcManagement values)
            {
                msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, f.sanction_refno, " +
                         " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                         " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by,  " +
                         " a.creditgroup_gid,e.cadgroup_name,a.customer_urn from agr_mst_tsuprapplication a " +
                         " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " left join agr_trn_tsuprprocesstype_assign e on e.application_gid = a.application_gid " +
                         " left join agr_trn_tsuprapplication2sanction f on f.application_gid = a.application_gid " +
                         " where a.process_type = 'Accept' and e.menu_gid = '" + getMenuClass.ChequeManagement + "' and " +
                         " e.checker_gid = '" + employee_gid + "' and e.checker_approvalflag = 'N' " +
                         " and (select approver_approvalflag from agr_trn_tsuprprocesstype_assign where menu_gid = 'CADMGTSAN' and application_gid = a.application_gid) = 'Y'" +
                         " group by a.application_gid order by a.updated_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplicationadd_list = new List<cadapplicationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        string lsccgroup_name;
                        string lsccadmin_name;
                        string lscadgroup_name;

                        msSQL = "select group_concat(ccgroup_name) as ccgroup_name from agr_mst_tsuprapplication where application_gid='" + dt["application_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsccgroup_name = objODBCDatareader["ccgroup_name"].ToString();
                        }
                        else
                        {
                            lsccgroup_name = "";
                        }
                        objODBCDatareader.Close();
                        msSQL = "select group_concat(ccadmin_name) as ccadmin_name from agr_mst_tsuprccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsccadmin_name = objODBCDatareader["ccadmin_name"].ToString();
                        }
                        else
                        {
                            lsccadmin_name = "";
                        }
                        objODBCDatareader.Close();
                        //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from agr_trn_tsuprprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                        //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        //if (objODBCDataReader.HasRows == true)
                        //{
                        //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                        //}
                        //else
                        //{
                        //    lscadgroup_name = "";
                        //}
                        //objODBCDataReader.Close();
                        getapplicationadd_list.Add(new cadapplicationlist
                        {
                            application_no = dt["application_no"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            approval_status = dt["approval_status"].ToString(),
                            ccapproved_date = dt["ccapproved_date"].ToString(),
                            creditgroup_name = dt["creditgroup_name"].ToString(),
                            ccgroup_name = lsccgroup_name,
                            ccadmin_name = lsccadmin_name,
                            cadgroupname = dt["cadgroup_name"].ToString(),
                            cadaccepted_by = dt["cadaccepted_by"].ToString(),
                            cadaccepted_date = dt["cadaccepted_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            customer_urn = dt["customer_urn"].ToString()
                        });

                    }
                }
                values.cadapplicationlist = getapplicationadd_list;
                dt_datatable.Dispose();
            }

            public void DaGetChequeFollowupCheckerSummary(string employee_gid, MdlUdcManagement values)
            {
                msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, g.sanction_refno, " +
                         " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                         " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by,  " +
                         " a.creditgroup_gid,e.cadgroup_name,f.approval_status as followup_status, a.customer_urn from agr_mst_tsuprapplication a " +
                         " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " left join agr_trn_tsuprprocesstype_assign e on e.application_gid = a.application_gid " +
                         " left join agr_trn_tsuprapplication2chequelist f on f.application_gid = a.application_gid " +
                         " left join agr_trn_tsuprapplication2sanction g on g.application_gid = a.application_gid " +
                         " where a.process_type = 'Accept' and e.approver_approvalflag = 'N' and " +
                         " e.menu_gid = '" + getMenuClass.ChequeManagement + "' and e.checker_gid = '" + employee_gid + "'and e.checker_approvalflag = 'Y' " +
                         " group by a.application_gid order by a.updated_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplicationadd_list = new List<cadapplicationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        string lsccgroup_name;
                        string lsccadmin_name;
                        string lscadgroup_name;

                        msSQL = "select group_concat(ccgroup_name) as ccgroup_name from agr_mst_tsuprapplication where application_gid='" + dt["application_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsccgroup_name = objODBCDatareader["ccgroup_name"].ToString();
                        }
                        else
                        {
                            lsccgroup_name = "";
                        }
                        objODBCDatareader.Close();
                        msSQL = "select group_concat(ccadmin_name) as ccadmin_name from agr_mst_tsuprccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsccadmin_name = objODBCDatareader["ccadmin_name"].ToString();
                        }
                        else
                        {
                            lsccadmin_name = "";
                        }
                        objODBCDatareader.Close();
                        //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from agr_trn_tsuprprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                        //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        //if (objODBCDataReader.HasRows == true)
                        //{
                        //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                        //}
                        //else
                        //{
                        //    lscadgroup_name = "";
                        //}
                        //objODBCDataReader.Close();
                        getapplicationadd_list.Add(new cadapplicationlist
                        {
                            application_no = dt["application_no"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            approval_status = dt["approval_status"].ToString(),
                            ccapproved_date = dt["ccapproved_date"].ToString(),
                            creditgroup_name = dt["creditgroup_name"].ToString(),
                            ccgroup_name = lsccgroup_name,
                            ccadmin_name = lsccadmin_name,
                            cadgroupname = dt["cadgroup_name"].ToString(),
                            cadaccepted_by = dt["cadaccepted_by"].ToString(),
                            cadaccepted_date = dt["cadaccepted_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            followup_status = dt["followup_status"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            customer_urn = dt["customer_urn"].ToString()
                        });

                    }
                }
                values.cadapplicationlist = getapplicationadd_list;
                dt_datatable.Dispose();
            }

            public void DaGetChequeApproverSummary(string employee_gid, MdlUdcManagement values)
            {
                msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, f.sanction_refno," +
                         " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                         " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by,  " +
                         " a.creditgroup_gid,e.cadgroup_name,a.customer_urn from agr_mst_tsuprapplication a " +
                         " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " left join agr_trn_tsuprprocesstype_assign e on e.application_gid = a.application_gid " +
                         " left join agr_trn_tsuprapplication2sanction f on f.application_gid = a.application_gid " +
                         " where a.process_type = 'Accept' and e.approver_approvalflag = 'N' and " +
                         " e.menu_gid = '" + getMenuClass.ChequeManagement + "' and e.approver_gid = '" + employee_gid + "'" +
                         " and (select approver_approvalflag from agr_trn_tsuprprocesstype_assign where menu_gid = 'CADMGTSAN' and application_gid = a.application_gid) = 'Y'" +
                         " group by a.application_gid order by a.updated_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplicationadd_list = new List<cadapplicationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        string lsccgroup_name;
                        string lsccadmin_name;
                        string lscadgroup_name;

                        msSQL = "select group_concat(ccgroup_name) as ccgroup_name from agr_mst_tsuprapplication where application_gid='" + dt["application_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsccgroup_name = objODBCDatareader["ccgroup_name"].ToString();
                        }
                        else
                        {
                            lsccgroup_name = "";
                        }
                        objODBCDatareader.Close();
                        msSQL = "select group_concat(ccadmin_name) as ccadmin_name from agr_mst_tsuprccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsccadmin_name = objODBCDatareader["ccadmin_name"].ToString();
                        }
                        else
                        {
                            lsccadmin_name = "";
                        }
                        objODBCDatareader.Close();
                        //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from agr_trn_tsuprprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                        //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        //if (objODBCDataReader.HasRows == true)
                        //{
                        //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                        //}
                        //else
                        //{
                        //    lscadgroup_name = "";
                        //}
                        //objODBCDataReader.Close();
                        getapplicationadd_list.Add(new cadapplicationlist
                        {
                            application_no = dt["application_no"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            approval_status = dt["approval_status"].ToString(),
                            ccapproved_date = dt["ccapproved_date"].ToString(),
                            creditgroup_name = dt["creditgroup_name"].ToString(),
                            ccgroup_name = lsccgroup_name,
                            ccadmin_name = lsccadmin_name,
                            cadgroupname = dt["cadgroup_name"].ToString(),
                            cadaccepted_by = dt["cadaccepted_by"].ToString(),
                            cadaccepted_date = dt["cadaccepted_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            customer_urn = dt["customer_urn"].ToString()
                        });

                    }
                }
                values.cadapplicationlist = getapplicationadd_list;
                dt_datatable.Dispose();
            }

            public void DaGetChequeCompletedApprover(string employee_gid, MdlUdcManagement values)
            {
                msSQL = " select a.application_gid,a.application_no,a.customerref_name,a.customer_urn,a.creditgroup_name, f.sanction_refno," +
                         " a.customer_name as customer_name,date_format(a.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, a.approval_status," +
                         " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as cadaccepted_by, " +
                         " date_format(a.processupdated_date, '%d-%m-%Y %h:%i %p') as cadaccepted_date, a.created_by,  " +
                         " a.creditgroup_gid,e.cadgroup_name,a.customer_urn from agr_mst_tsuprapplication a " +
                         " left join hrm_mst_temployee b on b.employee_gid = a.processupdated_by " +
                         " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                         " left join agr_mst_tsuprudcmanagement d on d.application_gid = a.application_gid " +
                         " left join agr_trn_tsuprprocesstype_assign e on e.application_gid = a.application_gid " +
                          " left join agr_trn_tsuprapplication2sanction f on f.application_gid = a.application_gid " +
                         " where a.process_type = 'Accept' and e.approver_approvalflag = 'Y' and " +
                         " e.menu_gid = '" + getMenuClass.ChequeManagement + "' and e.approver_gid = '" + employee_gid + "'" +
                         " group by a.application_gid order by a.updated_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplicationadd_list = new List<cadapplicationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        string lsccgroup_name;
                        string lsccadmin_name;
                        string lscadgroup_name;

                        msSQL = "select group_concat(ccgroup_name) as ccgroup_name from agr_mst_tsuprapplication where application_gid='" + dt["application_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsccgroup_name = objODBCDatareader["ccgroup_name"].ToString();
                        }
                        else
                        {
                            lsccgroup_name = "";
                        }
                        objODBCDatareader.Close();
                        msSQL = "select group_concat(ccadmin_name) as ccadmin_name from agr_mst_tsuprccschedulemeeting where application_gid='" + dt["application_gid"] + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsccadmin_name = objODBCDatareader["ccadmin_name"].ToString();
                        }
                        else
                        {
                            lsccadmin_name = "";
                        }
                        objODBCDatareader.Close();
                        //msSQL = "select group_concat(cadgroup_name) as cadgroup_name from agr_trn_tsuprprocesstype_assign where application_gid='" + dt["application_gid"] + "'";
                        //objODBCDataReader = objdbconn.GetDataReader(msSQL);
                        //if (objODBCDataReader.HasRows == true)
                        //{
                        //    lscadgroup_name = objODBCDataReader["cadgroup_name"].ToString();
                        //}
                        //else
                        //{
                        //    lscadgroup_name = "";
                        //}
                        //objODBCDataReader.Close();
                        getapplicationadd_list.Add(new cadapplicationlist
                        {
                            application_no = dt["application_no"].ToString(),
                            customer_name = dt["customer_name"].ToString(),
                            application_gid = dt["application_gid"].ToString(),
                            approval_status = dt["approval_status"].ToString(),
                            ccapproved_date = dt["ccapproved_date"].ToString(),
                            creditgroup_name = dt["creditgroup_name"].ToString(),
                            ccgroup_name = lsccgroup_name,
                            ccadmin_name = lsccadmin_name,
                            cadgroupname = dt["cadgroup_name"].ToString(),
                            cadaccepted_by = dt["cadaccepted_by"].ToString(),
                            cadaccepted_date = dt["cadaccepted_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            customer_urn = dt["customer_urn"].ToString()
                        });

                    }
                }
                values.cadapplicationlist = getapplicationadd_list;
                dt_datatable.Dispose();
            }

            public void DaCADChequeSummaryCount(string user_gid, string employee_gid, CadChequeCount values)
            {
                msSQL = " select count(a.application_gid) as MakerPendingCount from agr_trn_tsuprprocesstype_assign a " +
                        " left join agr_mst_tsuprapplication b on a.application_gid = b.application_gid " +
                        " where b.process_type = 'Accept' and a.menu_gid = '" + getMenuClass.ChequeManagement + "' and " +
                        " a.maker_gid = '" + employee_gid + "' and a.maker_approvalflag = 'N'  " +
                        " and (select approver_approvalflag from agr_trn_tsuprprocesstype_assign where menu_gid = 'CADMGTSAN' and application_gid = a.application_gid) = 'Y'";
                values.MakerPendingCount = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select count(a.application_gid) as MakerFollowUpCount from agr_trn_tsuprprocesstype_assign a " +
                        " left join agr_mst_tsuprapplication b on a.application_gid = b.application_gid " +
                        " where b.process_type = 'Accept' and a.menu_gid = '" + getMenuClass.ChequeManagement + "'" +
                        " and a.maker_gid =  '" + employee_gid + "'  and a.maker_approvalflag = 'Y' and a.approver_approvalflag = 'N'";
                values.MakerFollowUpCount = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select count(a.application_gid) as CheckerPendingCount from agr_trn_tsuprprocesstype_assign a " +
                        " left join agr_mst_tsuprapplication b on a.application_gid = b.application_gid " +
                        " where b.process_type = 'Accept' and a.menu_gid = '" + getMenuClass.ChequeManagement + "' and " +
                        " a.checker_gid = '" + employee_gid + "' and a.checker_approvalflag = 'N' " +
                        " and (select approver_approvalflag from agr_trn_tsuprprocesstype_assign where menu_gid = 'CADMGTSAN' and application_gid = a.application_gid) = 'Y'";
                values.CheckerPendingCount = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select count(a.application_gid) as CheckerFollowUpCount from agr_trn_tsuprprocesstype_assign a " +
                        " left join agr_mst_tsuprapplication b on a.application_gid = b.application_gid " +
                        " where b.process_type = 'Accept' and a.menu_gid = '" + getMenuClass.ChequeManagement + "' and " +
                        " a.checker_gid = '" + employee_gid + "' and a.checker_approvalflag = 'Y' and a.approver_approvalflag = 'N'";
                values.CheckerFollowUpCount = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select count(a.application_gid) as ApproverPendingCount from agr_trn_tsuprprocesstype_assign a " +
                        " left join agr_mst_tsuprapplication b on a.application_gid = b.application_gid " +
                        " where b.process_type = 'Accept' and a.menu_gid = '" + getMenuClass.ChequeManagement + "' and " +
                        " a.approver_gid = '" + employee_gid + "' and a.approver_approvalflag = 'N'" +
                        " and (select approver_approvalflag from agr_trn_tsuprprocesstype_assign where menu_gid = 'CADMGTSAN' and application_gid = a.application_gid) = 'Y'";
                values.ApproverPendingCount = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select count(a.application_gid) as CompletedCount from agr_trn_tsuprprocesstype_assign a " +
                        " left join agr_mst_tsuprapplication b on a.application_gid = b.application_gid " +
                        " where b.process_type = 'Accept' and a.menu_gid = '" + getMenuClass.ChequeManagement + "' and " +
                        " a.approver_gid = '" + employee_gid + "' and a.approver_approvalflag = 'Y' ";
                values.CompletedCount = objdbconn.GetExecuteScalar(msSQL);
            }

            public void DaPostChequeMakerSubmit(string employee_gid, Mdlmakerchequedetails values)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ACHL");
                msSQL = " insert into agr_trn_tsuprapplication2chequelist(" +
                        " application2chequelist_gid ," +
                        " application_gid," +
                        " application_no," +
                        " customer_name ," +
                        " makersubmitted_by," +
                        " makersubmitted_on ," +
                        " maker_flag," +
                        " approval_status," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.application_gid + "'," +
                        "'" + values.application_no + "'," +
                        "'" + values.customer_name + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'Y'," +
                        "'Checker Approval Pending'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update agr_trn_tsuprprocesstype_assign set maker_approvalflag='Y', maker_approveddate= NOW()" +
                           " where application_gid='" + values.application_gid + "' and menu_gid='" + getMenuClass.ChequeManagement + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Cheque Submitted for Checker";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }

            public void DaPostChequeCheckerSubmit(string employee_gid, Mdlcheckerchequedetails values)
            {
                msSQL = " update agr_trn_tsuprapplication2chequelist set checkerapproved_by='" + employee_gid + "', " +
                            " checkerapproved_on = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                            " approval_status = 'Final Approval Pending', " +
                            " checker_flag = 'Y' " +
                            " where application_gid='" + values.application_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update agr_trn_tsuprprocesstype_assign set checker_approvalflag='Y', checker_approveddate= NOW()" +
                           " where application_gid='" + values.application_gid + "' and menu_gid='" + getMenuClass.ChequeManagement + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Cheque Submitted for Approval";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }

            public void DaPostChequeApproval(string employee_gid, Mdlapprovalchequedetails values)
            {
                msSQL = " update agr_trn_tsuprapplication2chequelist set approved_by='" + employee_gid + "', " +
                            " approved_on = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                            " approval_status = 'Approved', " +
                            " approval_flag = 'Y' " +
                            " where application_gid='" + values.application_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {

                    msSQL = " update agr_trn_tsuprprocesstype_assign set approver_approvalflag='Y', approver_approveddate= NOW()" +
                           " where application_gid='" + values.application_gid + "' and menu_gid='" + getMenuClass.ChequeManagement + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Cheque Approved Successfully";
                }
                else
                {
                    values.message = "Error Occured";
                    values.status = false;
                }
            }


            public void DaGetApprovalDetails(string application_gid, MdlChequeApprovalDetails values)
            {
                try
                {
                    msSQL = " select application_gid,maker_name,checker_name,approver_name,maker_approveddate,checker_approveddate,approver_approveddate " +
                            " from agr_trn_tsuprprocesstype_assign where processtype_name = 'Accept' and menu_gid = '" + getMenuClass.ChequeManagement + "' and application_gid='" + application_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.application_gid = objODBCDatareader["application_gid"].ToString();
                        values.maker_name = objODBCDatareader["maker_name"].ToString();
                        values.checker_name = objODBCDatareader["checker_name"].ToString();
                        values.approver_name = objODBCDatareader["approver_name"].ToString();
                        values.maker_approveddate = objODBCDatareader["maker_approveddate"].ToString();
                        values.checker_approveddate = objODBCDatareader["checker_approveddate"].ToString();
                        values.approver_approveddate = objODBCDatareader["approver_approveddate"].ToString();
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

        }
    }


