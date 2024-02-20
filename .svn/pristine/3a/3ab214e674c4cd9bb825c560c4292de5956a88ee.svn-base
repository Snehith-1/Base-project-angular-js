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

    public class rpt_lsaManagement
    {
        string msSQL = string.Empty;
        dbconn objdbconn = new dbconn();
        OdbcConnection myConnection = new OdbcConnection();
        Image  company_logo;
        DataTable dt, dt1 = new DataTable();
        string  company_logo_path;
        DataTable CompanyLogo = new DataTable();
        public string DaGetlsaManagement(string lsacreate_gid)
        {
            OdbcCommand Mycommand = new OdbcCommand();
            DataSet MyDS = new DataSet();
            OdbcDataAdapter MyDA = new OdbcDataAdapter();

            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["AuthConn"].ToString();

            Mycommand.Connection = myConnection;

            msSQL = " select customer_name,branch_name,state,customer_urn,customer_location ,rm_name,business_head," +
                      " cluster_head,zonal_head,credit_manager,sanctionref_no,date_format(sanction_date, '%d-%m-%Y') as sanction_date,customer_address," +
                      " approved_by,date_format(approved_date, '%d-%m-%Y') as approved_date,gst_no,pan_no,purpose_lending,facility," +
                      " major_corporate,date_format(hypothecation_date, '%d-%m-%Y') as hypothecation_date,date_format(lsacreated_date,'%d-%m-%Y') as lsacreated_date," +
                      " date_format(mortgage_date, '%d-%m-%Y') as mortgage_date,product_solution,majot_intervention,sector,primaryvalue_chain,secondaryvalue_chain," +
                      " remarks,vertical,sa_code,constitution,lsaref_no,natureof_proposal,sanction_type from ids_trn_tlsa where lsacreate_gid='" + lsacreate_gid + "' ";
            MyDA.SelectCommand = Mycommand;
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;

            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "CustomerInfo");

            msSQL = string.Empty;


            msSQL = " select node,margin,format(existing_limit,2) as existing_limit,format(document_limit,2) as document_limit," +
                    " format(limit_released,2) as limit_released,tenure,revolving_type,sub_limit,rate_interest,date_format(expiry_date,'%d-%m-%Y') as expiry_date,"+
                    " facility_type,limitinfo_remarks,if(principal is null,'---',principal) as principal,if(interest is null,'---',interest) as interest,"+
                    " if(moratorium is null,'---',moratorium) as moratorium ,if(calloption is null,'---',calloption) as calloption," +
                     " limitref_no,odlim,if(report_structure='','---',report_structure) as report_structure,interchangeability from ids_trn_tlimitinfodtl where lsacreate_gid='" + lsacreate_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "LimitInfo");
            msSQL = string.Empty;


            msSQL = " select bank_name,account_no,ifsc_code  from ids_mst_tlsacustomer2bankinfo where " +
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
                    " bank_name as doc_bank_name,account_no as doc_account_no from ids_trn_tdocumentcharges " +
                    " where   lsacreate_gid='" + lsacreate_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "DocumentchargeInfo");
            msSQL = string.Empty;


            msSQL = " select borrower_chequeno,borrower_bankname,borrower_acno,borrower_deviation,borrower_remarks from ids_trn_tlsaborrowerinfo where " +
                    " lsacreate_gid='" + lsacreate_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "borrowerInfo");
            msSQL = string.Empty;


            msSQL = " select guarantor_chequeno,guarantor_bankname,guarantor_acno,guarantor_deviation,personal_guarantor_name,guarantor_panno" +
                    " from ids_trn_tlsaguarantorinfo where " +
                        "  lsacreate_gid='" + lsacreate_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "guarantorInfo");
            msSQL = string.Empty;


            msSQL =  "select terms_conditions,deferral_captured,head from ids_trn_tfinal " +
                " where   lsacreate_gid='" + lsacreate_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "final");
            msSQL = string.Empty;

             
            msSQL = " select group_concat(maker_signature) as maker_signature from ids_trn_tmakersignature " +
                    " where   lsacreate_gid='" + lsacreate_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "makersignature");
            msSQL = string.Empty;


            msSQL = "select nach_mandate,sign_match,sign_match_kyc,escrow_opened,appropriate_stamp,roc_filling," +
                    " nach_mandate_remarks,sign_match_remarks,sign_match_kyc_remarks,escrow_opened_remarks,appropriate_stamp_remarks," +
                    " roc_filling_remarks from ids_trn_tcompliancecheck " +
                     " where   lsacreate_gid='" + lsacreate_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "compliancecheck");

            msSQL = "select sum(document_limit) as total_doc_limit,sum(limit_released) as total_limit_released from ids_trn_tlimitinfodtl " +
                   " where   lsacreate_gid='" + lsacreate_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "totallimitamount");

            msSQL = string.Empty;
            msSQL = "select company_logo_path as company_logo from adm_mst_tcompany";

            dt1 = objdbconn.GetDAtaTable(msSQL, myConnection);
            CompanyLogo.Columns.Add("company_logo", typeof(byte[]));
            if (dt1.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt1.Rows)
                {
                    company_logo_path = (dr_datarow["company_logo"].ToString());
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
            dt1.Dispose();
            CompanyLogo.TableName = "CompanyLogo";
            MyDS.Tables.Add(CompanyLogo);

            



            ReportDocument oRpt = new ReportDocument();
            oRpt.Load(Path.Combine(ConfigurationManager.AppSettings["report_file_path"].ToString(), "ems.idas/lsaManagement.rpt"));
            oRpt.SetDataSource(MyDS);
            var path = Path.Combine(ConfigurationManager.AppSettings["report_path"].ToString(), "LSA" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
            oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, path);
            return path;
        }
    }
}