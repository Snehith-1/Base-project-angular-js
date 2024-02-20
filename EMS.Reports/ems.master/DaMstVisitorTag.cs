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

namespace EMS.Reports.ems.master
{
    public class DaMstVisitorTag 
    {

        string msSQL = string.Empty;
        dbconn objdbconn = new dbconn();
        OdbcConnection myConnection = new OdbcConnection();
        string company_logo_path;
        Image company_logo;
        DataTable dt1 = new DataTable();
        DataTable DataTable3 = new DataTable();

        public string DagetVisitorTagpdf(string visitor_gid)
        {


            OdbcCommand Mycommand = new OdbcCommand();
            DataSet MyDS = new DataSet();
            OdbcDataAdapter MyDA = new OdbcDataAdapter();

            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["AuthConn"].ToString();

            Mycommand.Connection = myConnection;
            msSQL = " select visit_id,branch_name, date_format(created_date,'%d-%m-%Y  %h:%i %p') as created_date," +
                    " visiting_type,purpose_of_visit,visitingofficer_name" +
                    " from ocs_mst_tvisitor where visitor_gid='" + visitor_gid + "'";

            MyDA.SelectCommand = Mycommand;
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;

            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "DataTable1");

            msSQL = "select visitor_name,date_format(tag_validity_from,'%d-%m-%Y') as tag_validity_from," +
                   " date_format(tag_validity_to,'%d-%m-%Y') as tag_validity_to,tag_id from ocs_mst_tvisitorname" +
                   " where visitor_gid='" + visitor_gid + "'";

            MyDA.SelectCommand = Mycommand;
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;

            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "DataTable2");

            msSQL = "select group_concat(employee_name) as official_name from ocs_mst_tvisitingofficer_name" +
                   " where visitor_gid='" + visitor_gid + "'";

            MyDA.SelectCommand = Mycommand;
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;

            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "DataTable4");


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
            oRpt.Load(Path.Combine(ConfigurationManager.AppSettings["report_file_path"].ToString(), "ems.master/RptMstVisitorTag.rpt"));
            oRpt.SetDataSource(MyDS);
            var path = Path.Combine(ConfigurationManager.AppSettings["report_path"].ToString(), "visitorform_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
            oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, path);
            myConnection.Close();
            return path;

        }
    }
}