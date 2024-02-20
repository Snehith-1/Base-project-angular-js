using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMS.Reports.Classes;
using System.IO;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.Common;
using System.Data.Odbc;
using System.Drawing;

namespace EMS.Reports.ems.idas
{
    public class ids_rpt_lsaManagement
    {
        string msSQL = string.Empty;
        dbconn objdbconn = new dbconn();
        string company_logo_path;
        Image company_logo;
        ImageConverter visitimage_converter;
        byte image_byte;
        OdbcDataReader objODBCDatareader;
        DataTable dt = new DataTable();
        DataTable CompanyLogo = new DataTable();
        OdbcConnection myConnection = new OdbcConnection();
        public string DaGetLSAManagement(string lsacreate_gid)
        {
            OdbcCommand Mycommand = new OdbcCommand();
            DataSet MyDS = new DataSet();
            OdbcDataAdapter MyDA = new OdbcDataAdapter();

            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["AuthConn"].ToString();

            Mycommand.Connection = myConnection;
            //--- Master Join - as per client request----//
            msSQL =" select c.customername as customer_name,b.sanction_branch_name as branch_name,b.sanction_state_name as state,c.customer_urn,customer_location,"+
                " b.sanction_refno as sanctionref_no,date_format(b.sanction_date, '%d-%m-%Y') as sanction_date,customer_address,b.ccapproved_by as approved_by,"+
                " date_format(b.ccapproved_date, '%d-%m-%Y') as approved_date,c.gst_number as gst_no,c.pan_number as pan_no, b.purpose_lending,facility,"+
                " c.major_corporate,date_format(hypothecation_date, '%d-%m-%Y') as hypothecation_date,date_format(approvalupdated_date, '%d-%m-%Y') as lsacreated_date, " +
                " date_format(mortgage_date, '%d-%m-%Y') as mortgage_date,b.product_solution,b.major_intervention as majot_intervention,sector," +
                " b.primary_value_chain as primaryvalue_chain,b.secondary_value_chain as secondaryvalue_chain,a.remarks,c.vertical_code as vertical,sa_code," + 
                " c.constitution_name as constitution,lsaref_no,customer_address1 as natureof_proposal,b.sanction_type,"+
                " concat(c.creditmgmt_name, ' / ', d.employee_emailid) as credit_manager," +
                "  concat(c.cluster_manager_name, ' / ', e.employee_emailid) as cluster_head," +
                " concat(c.zonal_name, ' / ', f.employee_emailid) as zonal_head,concat(c.businesshead_name, ' / ', g.employee_emailid) as business_head," +
                " concat(c.relationshipmgmt_name, ' / ', h.employee_emailid) as rm_name from ids_trn_tlsa a" +
                " left join ocs_mst_tcustomer2sanction b on a.customer2sanction_gid = b.customer2sanction_gid" +
                " left join ocs_mst_tcustomer c on b.customer_gid = c.customer_gid" +
                " left join hrm_mst_temployee d on c.creditmanager_gid = d.employee_gid" +
                " left join hrm_mst_temployee e on c.cluster_manager_gid = e.employee_gid" +
                " left join hrm_mst_temployee f on c.zonal_head = f.employee_gid" +
                " left join hrm_mst_temployee g on c.business_head = g.employee_gid" +
                " left join hrm_mst_temployee h on c.relationship_manager = h.employee_gid  where lsacreate_gid ='" + lsacreate_gid + "' ";
            MyDA.SelectCommand = Mycommand;
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;

            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "CustomerInfo");

            msSQL = string.Empty;


            msSQL = " select node,margin,format(existing_limit,2) as existing_limit,format(document_limit,2) as document_limit,format(limit_released,2) as limit_released,"+
                    " tenure,revolving_type,sub_limit,rate_interest,date_format(expiry_date,'%d-%m-%Y') as expiry_date,facility_type,"+
                    " limitinfo_remarks,if(principal is null,'---',principal) as principal,if(interest is null,'---',interest) as interest," +
                    " if(moratorium is null,'---',moratorium) as moratorium ,if(calloption is null,'---',calloption) as calloption," +
                    " limitref_no,odlim,if(report_structure='','---',report_structure) as report_structure,interchangeability,"+
                    " change_request,format((26- rate_interest),0) as penal_interest from ids_trn_tlimitinfodtl where lsacreate_gid='" + lsacreate_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "LimitInfo");
            msSQL = string.Empty;


            msSQL = " select ifsc_code,account_no,bank_name from ids_mst_tlsacustomer2bankinfo where " +
                    " lsacreate_gid='" + lsacreate_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "BankInfo");
            msSQL = string.Empty;


            msSQL = " select recovered_type,format(recovered_amount,2) as recovered_amount,chequeno_details,date_format(chequedate_details,'%d-%m-%Y') as chequedate_details," +
                    " bank_name,account_no,recover_remarks,format(to_be_recoveredamount,2) as to_be_recoveredamount from ids_trn_tprocessingfees " +
                    " where  lsacreate_gid='" + lsacreate_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "ProcessingInfo");
            msSQL = string.Empty;


            msSQL = "select format(recovered_amount,2) as doc_recovered_amount,chequeno_details as doc_chequeno_details,date_format(chequedate_details,'%d-%m-%Y') as doc_chequedate_details," +
                    " bank_name as doc_bank_name,account_no as doc_account_no,documentcharge_remarks from ids_trn_tdocumentcharges " +
                    " where   lsacreate_gid='" + lsacreate_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "DocumentchargeInfo");
            msSQL = string.Empty;



            msSQL = "select terms_conditions,deferral_captured,head from ids_trn_tfinal " +
                " where   lsacreate_gid='" + lsacreate_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "final");
            msSQL = string.Empty;

            msSQL = " select  maker_signature from ids_trn_tmakersignature where " +
                   " lsacreate_gid='" + lsacreate_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "maker");
            msSQL = string.Empty;

            msSQL = "select a.interchangeability,applicable_condition from ids_trn_tlimitinfodtl a" +
                      " left join ocs_mst_tsanction2loanfacilitytype b on a.facility_type_gid = b.sanction2loanfacilitytype_gid where lsacreate_gid = '" + lsacreate_gid + "' limit 0,1";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                if (objODBCDatareader["interchangeability"].ToString() == "No")
                {
                    if ((objODBCDatareader["applicable_condition"].ToString() == "No") || (objODBCDatareader["applicable_condition"].ToString() == null) || (objODBCDatareader["applicable_condition"].ToString() == ""))
                    {
                        msSQL = "select  case when b.sanctionref_no like '%DBS%' then format(document_limit,2,'en_IN')" +
                              " when b.sanctionref_no not like '%DBS%' then format(sum(document_limit),2,'en_IN')  end as total_doc_limit," +
                              " format(sum(limit_released),2,'en_IN') as total_limit_released from ids_trn_tlimitinfodtl a" +
                              " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                              " where a.lsacreate_gid ='" + lsacreate_gid + "'";
                        Mycommand.CommandText = msSQL;
                        Mycommand.CommandType = CommandType.Text;
                        MyDS.EnforceConstraints = false;

                    }
                    else
                    {

                        msSQL = "select limit_released from ids_trn_tlimitinfodtl a where lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                        string totol_limit_released = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select odlim from ids_trn_tlimitinfodtl a where lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                        string odlim = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = "select sum(document_limit) from ids_trn_tlimitinfodtl a" +
                                " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid where a.lsacreate_gid ='" + lsacreate_gid + "' ";
                        string total_document_limit = objdbconn.GetExecuteScalar(msSQL);

                        var lstotol_limit_released = Int64.Parse(totol_limit_released);
                        var lsodlim = Int64.Parse(odlim);
                        var lstotal_document_limit = Int64.Parse(total_document_limit);

                        if ((lsodlim) >= (lstotol_limit_released))
                        {
                            
                            if ((lsodlim) == (lstotal_document_limit))
                            {
                                msSQL = "select format(sum(limit_released),2,'en_IN') as total_limit_released, "+
                                    " case when b.sanctionref_no like '%DBS%' then format(document_limit,2,'en_IN')" +
                                    " when b.sanctionref_no not like '%DBS%' then format(odlim,2,'en_IN')  end as total_doc_limit" +
                                    " from ids_trn_tlimitinfodtl a" +
                                    " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                                    " where a.lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                                Mycommand.CommandText = msSQL;
                                Mycommand.CommandType = CommandType.Text;
                                MyDS.EnforceConstraints = false;
                            }
                            else
                            {
                                msSQL = "select  case when b.sanctionref_no like '%DBS%' then document_limit" +
                                    " when b.sanctionref_no not like '%DBS%' then sum(document_limit)  end as document_limit" +
                                    " from ids_trn_tlimitinfodtl a" +
                                    " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                                    " where a.lsacreate_gid ='" + lsacreate_gid + "'";
                                string document_limit = objdbconn.GetExecuteScalar(msSQL);
                                var lsdocument_limit = Int64.Parse(document_limit);

                                if (lsodlim >= lsdocument_limit)
                                {
                                    msSQL = "select   format(sum(limit_released),2,'en_IN') as total_limit_released,  format(document_limit,2,'en_IN') as total_doc_limit " +
                                   " from ids_trn_tlimitinfodtl a" +
                                   " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                                   " where a.lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                                    Mycommand.CommandText = msSQL;
                                    Mycommand.CommandType = CommandType.Text;
                                    MyDS.EnforceConstraints = false;
                                }
                                else
                                {
                                    msSQL = "select  format(sum(limit_released),2,'en_IN') as total_limit_released, format(odlim,2,'en_IN')  as total_doc_limit" +
                                                                       " from ids_trn_tlimitinfodtl a" +
                                                                       " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                                                                       " where a.lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                                    Mycommand.CommandText = msSQL;
                                    Mycommand.CommandType = CommandType.Text;
                                    MyDS.EnforceConstraints = false;
                                }

                            }
                        }
                        else
                        {
                            if ((lsodlim) == (lstotal_document_limit))
                            {
                                msSQL = "select format(limit_released,2,'en_IN') as total_limit_released, case when b.sanctionref_no like '%DBS%' then format(document_limit,2,'en_IN')" +
                                    " when b.sanctionref_no not like '%DBS%' then format(odlim,2,'en_IN')  end as total_doc_limit" +
                                    " from ids_trn_tlimitinfodtl a" +
                                    " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                                    " where a.lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                                Mycommand.CommandText = msSQL;
                                Mycommand.CommandType = CommandType.Text;
                                MyDS.EnforceConstraints = false;
                            }
                            else
                            {
                                msSQL = "select  case when b.sanctionref_no like '%DBS%' then document_limit" +
                                    " when b.sanctionref_no not like '%DBS%' then sum(document_limit)  end as document_limit" +
                                    " from ids_trn_tlimitinfodtl a" +
                                    " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                                    " where a.lsacreate_gid ='" + lsacreate_gid + "'";
                                string document_limit = objdbconn.GetExecuteScalar(msSQL);
                                var lsdocument_limit = Int64.Parse(document_limit);

                                if (lsodlim >= lsdocument_limit)
                                {
                                    msSQL = "select format(limit_released,2,'en_IN') as total_limit_released,format(document_limit,2,'en_IN') as total_doc_limit" +
                                   " from ids_trn_tlimitinfodtl a" +
                                   " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                                   " where a.lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                                    Mycommand.CommandText = msSQL;
                                    Mycommand.CommandType = CommandType.Text;
                                    MyDS.EnforceConstraints = false;
                                }
                                else
                                {
                                    msSQL = "select format(limit_released,2,'en_IN') as total_limit_released,format(odlim,2,'en_IN') as total_doc_limit" +
                                                                       " from ids_trn_tlimitinfodtl a" +
                                                                       " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                                                                       " where a.lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                                    Mycommand.CommandText = msSQL;
                                    Mycommand.CommandType = CommandType.Text;
                                    MyDS.EnforceConstraints = false;
                                }

                            }
                        }
                       
                    }

                }
                else
                {
                  

                    msSQL = "select  case when b.sanctionref_no like '%DBS%' then format(document_limit,2,'en_IN')" +
                              " when b.sanctionref_no not like '%DBS%' then format(odlim,2,'en_IN')  end as total_doc_limit," +
                              " format(limit_released,2,'en_IN') as total_limit_released from ids_trn_tlimitinfodtl a" +
                              " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                              " where a.lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                    Mycommand.CommandText = msSQL;
                    Mycommand.CommandType = CommandType.Text;
                    MyDS.EnforceConstraints = false;


                }
                objODBCDatareader.Close();
            }


          
           
            MyDA.Fill(MyDS, "totallimitamount");

            msSQL = string.Empty;
            //msSQL = "select format((26- rate_interest),0) as penal_interest,group_concat(facility_type) as facility_type from ids_trn_tlimitinfodtl where " +
            //    " lsacreate_gid='" + lsacreate_gid + "'  group by rate_interest";
            //Mycommand.CommandText = msSQL;
            //Mycommand.CommandType = CommandType.Text;
            //MyDS.EnforceConstraints = false;

            //MyDA.Fill(MyDS, "PenalInterest");
            //msSQL = string.Empty;


            msSQL = "select company_logo_path as company_logo from adm_mst_tcompany";

            dt = objdbconn.GetDAtaTable(msSQL, myConnection);
            CompanyLogo.Columns.Add("company_logo", typeof(byte[]));
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt.Rows)
                {
                    company_logo_path = HttpContext.Current.Server.MapPath((dr_datarow["company_logo"].ToString()));
                    company_logo_path = company_logo_path.Replace("/", "\\");
                    company_logo_path = company_logo_path.Replace("EMS.Reports\\", "");
                    company_logo_path = company_logo_path.Replace("api\\", "");
                    company_logo_path = company_logo_path.Replace("/", "\\");
                    if (System.IO.File.Exists(company_logo_path) == true)
                    {
                        //Convert  Image Path to Byte
                        company_logo = System.Drawing.Image.FromFile(company_logo_path);
                        byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(company_logo, typeof(byte[]));

                        CompanyLogo.Rows.Add(bytes);
                    }
                }
            }
            dt.Dispose();
            CompanyLogo.TableName = "CompanyLogo";
            MyDS.Tables.Add(CompanyLogo);

           

            ReportDocument oRpt = new ReportDocument();
            oRpt.Load(Path.Combine(ConfigurationManager.AppSettings["report_file_path"].ToString(), "ems.idas/lsaManagmentreport.rpt"));
            oRpt.SetDataSource(MyDS);
            myConnection.Close();
            var path = Path.Combine(ConfigurationManager.AppSettings["report_path"].ToString(), "LSA" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
            oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, path);
         
            return path;
        }
    }
}