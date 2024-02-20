using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.IO;
using ems.lgl.Models;
using ems.utilities.Functions;
using System.Configuration;
using ems.storage.Functions;

namespace ems.lgl.DataAccess
{
    public class DaLglTrnDNTrackerVertical
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGETGID;
        int mnResult;
      
        public bool DaGetExclusionCustomer(string customer_urn, string exclusion_reason, string employee_gid, result values)
        {
            msSQL = "select customer_name from lgl_tmp_tmisdata where urn='" + customer_urn + "' group by urn";
            string lscustomer_name = objdbconn.GetExecuteScalar(msSQL);


            msGETGID = objcmnfunctions.GetMasterGID("EXCH");
            msSQL = "insert into lgl_trn_texclusionhistory(" +
                       " exclusionhistory_gid ," +
                       " customer_urn," +
                       " customer_name," +
                       " excluded_status," +
                       " exclusion_reason," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGETGID + "'," +
                       "'" + customer_urn + "'," +
                       "'" + lscustomer_name + "'," +
                       "'Excluded'," +
                       "'" + exclusion_reason.Replace("'", "\\'") + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update lgl_trn_tmisdata" +
                        " set exclusion_flag ='Y', " +
                        " exclusion_updatedby='" + employee_gid + "'," +
                        " exclusion_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                        " where urn = '" + customer_urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            

            if (mnResult != 0)
            {
                values.message = "Customer Excluded Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured..!";
                values.status = false;
                return false;
            }

        }
        public bool DaGetActivationCustomer(string customer_urn, string exclusion_reason, string employee_gid, result values)
        {

            msSQL = "select customer_name from lgl_tmp_tmisdata where urn='" + customer_urn + "' group by urn";
            string lscustomer_name = objdbconn.GetExecuteScalar(msSQL);
            //string msGet_Gid = objcmnfunctions.GetMasterGID("EXCH");
            //msSQL = "insert into lgl_trn_tdnexclusion(" +
            //           " dnexclusion_gid ," +
            //           " customer_urn," +
            //           " customer_name," +
            //           " excluded_status," +
            //           " exclusion_reason," +
            //           " created_by," +
            //           " created_date)" +
            //           " values(" +
            //           "'" + msGet_Gid + "'," +
            //           "'" + customer_urn + "'," +
            //           "'" + lscustomer_name + "'," +
            //           "'Activated'," +
            //           "'" + exclusion_reason.Replace("'", "\\'") + "'," +
            //           "'" + employee_gid + "'," +
            //           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGETGID = objcmnfunctions.GetMasterGID("EXCH");
            msSQL = "insert into lgl_trn_texclusionhistory(" +
                       " exclusionhistory_gid ," +
                       " customer_urn," +
                       " customer_name," +
                       " excluded_status," +
                       " exclusion_reason," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGETGID + "'," +
                       "'" + customer_urn + "'," +
                       "'" + lscustomer_name + "'," +
                       "'Activated'," +
                       "'" + exclusion_reason.Replace("'", "\\'") + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update lgl_trn_tmisdata" +
                    " set exclusion_flag ='N', " +
                    " exclusion_updatedby='" + employee_gid + "'," +
                    " exclusion_updateddate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                    " where urn = '" + customer_urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                values.message = "Customer Activated Successfully..!";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured..!";
                values.status = false;
                return false;
            }

        }
        public bool DaGetExclusionCustomerHistory(string customer_urn, exclusionhistorylist values)
        {
           msSQL=" select date_format(a.created_date, '%d-%m-%Y') as exclusion_updateddate,excluded_status, "+ 
                      " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as excludedby,exclusion_reason"+ 
                      " from lgl_trn_texclusionhistory a"+ 
                      " left join hrm_mst_temployee b on a.created_by = b.employee_gid"+ 
                      " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                      " where a.customer_urn='" + customer_urn + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_mappingdtl = new List<exclusionhistory>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.exclusionhistory = dt_datatable.AsEnumerable().Select(row => new exclusionhistory
                {
                    excluded_date = row["exclusion_updateddate"].ToString(),
                    excluded_by = row["excludedby"].ToString(),
                    excluded_status = row["excluded_status"].ToString(),
                    exclusion_reason = row["exclusion_reason"].ToString(),

                }).ToList();
            }
            dt_datatable.Dispose();
            return true;
        }
        public bool DaGetTemplateContent(string urn, MdlMisdataimportlist values)
        {
            msSQL = "select DN_status from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
            values.dn_status = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select tempdn1format_gid,DN1template_content,DN2template_content,DN3template_content,cbotemplate_content" +
               " from lgl_tmp_tdnformat where customer_urn='" + urn + "' and status<>'Closed'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

           
            if (objODBCDatareader.HasRows == true)
            {
                if ((values.dn_status == "DN1 Generated") || (values.dn_status == "DN1 Sent"))
                {
                    values.tempdn1format_gid= objODBCDatareader["tempdn1format_gid"].ToString();
                    values.template_content = objODBCDatareader["DN1template_content"].ToString();
                    msSQL= "select dn1annexuredocument_name,dn1annexuredocument_path  from lgl_trn_tsanctiondtl where customer_urn='" + urn + "'and status<>'Closed'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if(objODBCDatareader1.HasRows==true)
                    {
                       values.document_name = objODBCDatareader1["dn1annexuredocument_name"].ToString();
                       values.document_path = objcmnstorage.EncryptData(HttpContext.Current.Server.MapPath(objODBCDatareader1["dn1annexuredocument_path"].ToString()));
                    }
                    objODBCDatareader1.Close();

                }
                else if((values.dn_status == "DN2 Generated") || (values.dn_status == "DN2 Sent"))
                {
                    values.tempdn1format_gid = objODBCDatareader["tempdn1format_gid"].ToString();
                    values.template_content = objODBCDatareader["DN2template_content"].ToString();
                    msSQL = "select dn2annexuredocument_name,dn2annexuredocument_path  from lgl_trn_tsanctiondtl where customer_urn='" + urn + "'and status<>'Closed'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        values.document_name = objODBCDatareader1["dn2annexuredocument_name"].ToString();
                        values.document_path = objcmnstorage.EncryptData(HttpContext.Current.Server.MapPath(objODBCDatareader1["dn2annexuredocument_path"].ToString()));
                    }
                    objODBCDatareader1.Close();

                }
                else if((values.dn_status == "DN3 Generated") || (values.dn_status == "DN3 Sent"))
                {
                    values.tempdn1format_gid = objODBCDatareader["tempdn1format_gid"].ToString();
                    values.template_content = objODBCDatareader["DN3template_content"].ToString();
                    msSQL = "select dn3annexuredocument_name,dn3annexuredocument_path  from lgl_trn_tsanctiondtl where customer_urn='" + urn + "'and status<>'Closed'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        values.document_name = objODBCDatareader1["dn3annexuredocument_name"].ToString();
                        values.document_path = objcmnstorage.EncryptData(HttpContext.Current.Server.MapPath(objODBCDatareader1["dn3annexuredocument_path"].ToString()));
                    }
                    objODBCDatareader1.Close();

                }
                
                //values.dn2_content = objODBCDatareader["DN2template_content"].ToString();
                //values.dn3_content = objODBCDatareader["DN3template_content"].ToString();
                //values.cbotemplate_content = objODBCDatareader["cbotemplate_content"].ToString();
            }
            objODBCDatareader.Close();

            values.status = true;
            return true;
        }

        public bool DaPostRaiseLegalSR(string employee_gid, string user_gid, MdlRaiselegalSR values)
        {
            msSQL = "select customer_gid from ocs_mst_tcustomer where customer_urn = '" + values.customer_urn + "'";
            string lscustomer_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select vertical_code from ocs_mst_tcustomer where customer_gid = '" + lscustomer_gid + "'";
            string lsvertical = objdbconn.GetExecuteScalar(msSQL);

            string msgetGID = objcmnfunctions.GetMasterGID("TLSR");
         string   msGetGIDREF = objcmnfunctions.GetMasterGID("LSR_");
            msSQL = "insert into lgl_tmp_traiselegalSR (" +
                " templegalsr_gid," +
                " srref_no," +
                " vertical,"+
                " customer_gid," +
                " customer_urn ," +
                " account_name," +
                " remarks," +
                " created_by," +
                " created_date)" +
                " values (" +
                "'" + msgetGID + "'," +
                      "'" + msGetGIDREF + "'," +
                      "'" + lsvertical + "'," +
                      "'" + lscustomer_gid + "'," +
                      "'" + values.customer_urn + "'," +
                      "'" + values.customer_name.Replace("    ", "").Replace("\n", "") + "'," +
                      "'" + values.remarks.Replace("'", "") + "'," +
                      "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msSQL = " update lgl_trn_tmisdata set DN_status='Legal SR'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.customer_urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Legal SR added successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while adding Legal SR";
                return false;
            }
        }
    }
}