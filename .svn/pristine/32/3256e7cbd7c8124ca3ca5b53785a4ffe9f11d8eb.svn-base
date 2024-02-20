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
    public class rpt_observationReport
    {
        string msSQL = string.Empty;
        dbconn objdbconn = new dbconn();           
        OdbcConnection myConnection = new OdbcConnection();
        string  company_logo_path;
        Image company_logo;
        DataTable  dt1 = new DataTable();     
        DataTable DataTable3 = new DataTable();

        public string getobservationReport(string observation_reportgid)
        {


            OdbcCommand Mycommand = new OdbcCommand();
            DataSet MyDS = new DataSet();
            OdbcDataAdapter MyDA = new OdbcDataAdapter();

            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["AuthConn"].ToString();

            Mycommand.Connection = myConnection;
            msSQL = " select observation_reportgid,visitreport_generategid,allocationdtl_gid,customer_name,customer_urn,risk_code, " +
                   " date_format(dateof_RMDvisit, '%d-%m-%Y') as dateof_RMDvisit,report_pertainingto, " +
                   " vertical,format(disbursement_amount,2) as disbursement_amount,approving_authority," +
                   " date_format(loansanction_date, '%d-%m-%Y') as loansanction_date ,observation_flag," +
                   " relationship_manager_gid,relationship_manager_name,date_format(relationshipmanager_updateddate,'%d-%m-%Y') as atr_completiondate, " +
                   " PPA_name,RMDvisit_officialname, date_format(loandisbursement_date, '%d-%m-%Y') as loandisbursement_date , " +
                   " people_accompaniedRMD,format(sanction_amount,2) as sanction_amount,format(outstanding_amount,2) as outstanding_amount, " +
                   " current_DPD,contact_details1,contact_details2, " +
                   " concat(b.user_firstname, ' / ', b.user_lastname) as created_by,date_format(a.created_date, '%d-%m-%Y') as created_date " +
                   " from rsk_trn_tobservationreport a " +
                   " left join adm_mst_tuser b on a.created_by = b.user_gid" +
                   " where observation_reportgid='" + observation_reportgid + "'";

            MyDA.SelectCommand = Mycommand;
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;

            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "DataTable1");


          msSQL = " select critical_observationgid,criteria, RMD_observations, actionable_recommended, relationship_manager_remarks, remarks_flag, " +
                  " concat(b.user_firstname, ' / ', b.user_lastname) as remarks_updatedby,date_format(a.remarks_updateddate, '%d-%m-%Y') as remarks_updateddate, " +
                  " concat(c.user_firstname, ' / ', c.user_lastname) as created_by" +
                  " from rsk_trn_tcriticalobservation a " +
                  " left join adm_mst_tuser b on a.remarks_updatedby = b.user_gid" +
                  " left join adm_mst_tuser c on a.created_by = c.user_gid" +
                  " where observation_reportgid= '" + observation_reportgid + "'";
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

            ReportDocument oRpt = new ReportDocument();
            oRpt.Load(Path.Combine(ConfigurationManager.AppSettings["report_file_path"].ToString(), "ems.rsk/observationReport.rpt"));
            oRpt.SetDataSource(MyDS);
            var path = Path.Combine(ConfigurationManager.AppSettings["report_path"].ToString(), "observationReport_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
            oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, path);
            myConnection.Close();
            return path;

        }
    }
}