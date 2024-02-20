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

namespace EMS.Reports.ems.master
{    public class DaMstGenerateCAM
    {
        string msSQL = string.Empty;
        dbconn objdbconn = new dbconn();
        string company_logo_path;
        Image company_logo;
        ImageConverter visitimage_converter;
        byte image_byte;

        DataTable dt = new DataTable();
        DataTable DataTable2 = new DataTable();
        OdbcConnection myConnection = new OdbcConnection();
        public string GetCAMGenerate(string id)
        {
            OdbcCommand Mycommand = new OdbcCommand();
            DataSet MyDS = new DataSet();
            OdbcDataAdapter MyDA = new OdbcDataAdapter();

            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["AuthConn"].ToString();

            Mycommand.Connection = myConnection;

            msSQL = " select a.application_no,a.customer_name,a.camref_no,credit_appraisal,date_format(nextreview_date,'%d-%m-%Y') as nextreview_date,"+
                " approve_authority,business_desc,date_format(unitvisit_date, '%d-%m-%Y') as unitvisit_date,external_rating,"+
                " type_enterprise,security,rcu_screening,world_check,esms_category,internal_rating,internal_dedupe,borrower_description," +
                " cin,pan,gst_location,udayamreg_no,b.vertical_name,b.constitution_name,b.customer_urn from ocs_trn_tcamgeneration a" +
                " left join ocs_mst_tapplication b on a.application_gid = b.application_gid where a.application_gid='" + id + "'"; 
            MyDA.SelectCommand = Mycommand;
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;

            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "DataTable1");

            msSQL = string.Empty;




            ReportDocument oRpt = new ReportDocument();
            oRpt.Load(Path.Combine(ConfigurationManager.AppSettings["report_file_path"].ToString(), "ems.master/MstRptCAMGenerate.rpt"));
            oRpt.SetDataSource(MyDS);
            myConnection.Close();
            var path = Path.Combine(ConfigurationManager.AppSettings["report_path"].ToString(), "CAM_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc");
            oRpt.ExportToDisk(ExportFormatType.WordForWindows, path);
            return path;
        }
    }
}