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
using System.Web.Http;
using System.Drawing.Imaging;
using static System.Drawing.ImageConverter;
using System.Drawing;

namespace EMS.Reports.ems.rsk
{
    public class rpt_visitReport
    {
        string msSQL = string.Empty;
        dbconn objdbconn = new dbconn();
        DataTable DataTable2;
        OdbcDataReader objOdbcDataReader;
        string visit_photo_path, company_logo_path;
        Image visitimage, company_logo;
        ImageConverter visitimage_converter;
        byte image_byte;
        
        DataTable dt,dt1 = new DataTable();
        DataTable DataTable4 = new DataTable();
        DataTable DataTable3 = new DataTable();


        OdbcConnection myConnection = new OdbcConnection();
        public string getvisitReport(string allocationdtl_gid)
        {
           

            OdbcCommand Mycommand = new OdbcCommand();
            DataSet MyDS = new DataSet();
            OdbcDataAdapter MyDA = new OdbcDataAdapter();

            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["AuthConn"].ToString();

            Mycommand.Connection = myConnection;
            msSQL = " select concat(b.state_name, ' / ' ,b.district_name) as location,risk_code,a.customer_name,constitution,date_format(visit_date, '%d-%m-%Y') as visit_date, " +
                    " visit_latitude,visit_longitude,typeof_riskreview," +
                    " date_format(dealing_withsince, '%d-%m-%Y') as dealing_withsince,business_vintage,typeof_loanvertical,business_sector," +
                    " registeredoffice_address,present_address,contact_details1,contact_details2,sanctioned_limit,tenure_period," +
                    " relationship_startedfrom,primarysecondary_valuechain,clientbusiness_vintage,geneticcode_complied," +
                    " RMD_visitedGid,RMD_visitedname,RM_name,a.PPA_name,credit_managername,visit_done,purpose_ofloan,adequacy_additional_funding," +
                    " requestedamount_byclient,sanctionedamount_byclient," +
                    " date_format(disbursement_date, '%d-%m-%Y') as disbursement_date,disbursement_amount," +
                    " totalloan_outstanding,CONCAT(COALESCE(`basicrecords_maintain`,''),' & ',COALESCE(`basicrecords_remarks`,'')) AS basicrecords_maintain," +
                    " overdue,repayment_track,repayment_trackremarks,turnover_lastFY," +
                    " presentFY_sales,deferral_pendency,total_noofGroups," +
                    " CBOfunded_noofGroups,RMD_visitgroups,borrower_commitment,pending_documentation,assetverification_createdoutofloan," +
                    " assetverification_securitydtls,assetverification_mortgaged,assetverification_ROCcreation,briefdtls_client," +
                    " purposeof_funding,utilisation_details,adequacy_loanamount,adequacy_impactassessment,overall_remarks,PDD_compliance," +
                    " portfolio_noofmembers,portfolio_activemembers,total_disbursementamount," +
                    " outstanding_ondate,overdue_beneficiary,valuechain_mapanalysis,competitorbusiness_segment," +
                    " overdue_amount,overdueaccount_funding,briefrpt_financials,briefrpt_process,briefrpt_customer," +
                    " briefrpt_learnings,briefrpt_valuechain,report_status,date_format(b.completed_date, '%d-%m-%Y') as completed_date" +
                    " from rsk_trn_tvisitreportgenerate a" +
                    " left join rsk_trn_tallocationdtl b on a.allocationdtl_gid = b.allocationdtl_gid" +
                    " where a.allocationdtl_gid='" + allocationdtl_gid + "'";

            MyDA.SelectCommand = Mycommand;
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
           
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "DataTable1");

            msSQL = string.Empty;
            msSQL = "select group_concat(visit_type) as visit_type from rsk_trn_tvisitdone where allocationdtl_gid='" + allocationdtl_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;
            MyDA.Fill(MyDS, "DataTable2");

            msSQL = "select company_logo_path as company_logo from adm_mst_tcompany";

            dt1 = objdbconn.GetDAtaTable(msSQL, myConnection);
            DataTable3.Columns.Add("company_logo", typeof(byte[]));
            if (dt1.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt1.Rows)
                {
                    company_logo_path = HttpContext.Current.Server.MapPath(dr_datarow["company_logo"].ToString());
                    company_logo_path = company_logo_path.Replace("EMS.Reports", "");
                    company_logo_path = company_logo_path.Replace("api\\", "");
                    company_logo_path = company_logo_path.Replace("/", "\\");
                    if (System.IO.File.Exists(company_logo_path) == true)
                    {
                        //Convert  Image Path to Byte
                        company_logo = System.Drawing.Image.FromFile(company_logo_path);
                        byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(company_logo, typeof(byte[]));

                        DataTable3.Rows.Add(bytes);
                    }   
                }
            }
            dt1.Dispose();
            DataTable3.TableName = "DataTable3";
            MyDS.Tables.Add(DataTable3);

            

            msSQL = "select document_path as visit_reportphoto from rsk_trn_tvisitreportphoto where allocationdtl_gid='" + allocationdtl_gid + "'";
            dt = objdbconn.GetDAtaTable(msSQL, myConnection);
            DataTable4.Columns.Add("visit_reportphoto", typeof(byte[]));
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt.Rows)
                {
                    visit_photo_path = HttpContext.Current.Server.MapPath(dr_datarow["visit_reportphoto"].ToString());
                    visit_photo_path = visit_photo_path.Replace("EMS.Reports", "");
                    visit_photo_path = visit_photo_path.Replace("api\\", "");
                    visit_photo_path = visit_photo_path.Replace("/", "\\");
                    if (System.IO.File.Exists(visit_photo_path) == true)
                    { 
                        //Convert  Image Path to Byte
                        visitimage = System.Drawing.Image.FromFile(visit_photo_path);
                        byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(visitimage, typeof(byte[]));

                        DataTable4.Rows.Add(bytes);

                    }
                       
                }
            }
            dt.Dispose();
            DataTable4.TableName = "DataTable4";
            MyDS.Tables.Add(DataTable4);
          

            msSQL = " select sanction_refno,date_format(sanction_date,'%d-%m-%Y') as sanction_date,facility_type,tenure_months, " +
                   " format(sanction_amount, 2) as sanction_amount from rsk_trn_tallocatesanctiondtl " +
                   " where allocationdtl_gid = '" + allocationdtl_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "DataTable5");


            ReportDocument oRpt = new ReportDocument();
            oRpt.Load(Path.Combine(ConfigurationManager.AppSettings["report_file_path"].ToString(), "ems.rsk/visitReport.rpt"));
            oRpt.SetDataSource(MyDS);
            var path = Path.Combine(ConfigurationManager.AppSettings["report_path"].ToString(), "visitReport_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
            oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, path);
            myConnection.Close();
            return path;
          
        }
    }
}