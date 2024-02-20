using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using EMS.Reports.Classes;
using System;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Web;

namespace EMS.Reports.ems.idas
{
    public class rpt_idassanctiongeneration
    {
        string msSQL = string.Empty;
        dbconn objdbconn = new dbconn();
        string company_logo_path;
        Image company_logo;
        ImageConverter visitimage_converter;
        byte image_byte;

        DataTable dt = new DataTable();
        DataTable DataTable2 = new DataTable();
        //  DataTable DataTable3 = new DataTable();

        OdbcConnection myConnection = new OdbcConnection();
        public string Dasanctionlettercontent(string id)
        {
            OdbcCommand Mycommand = new OdbcCommand();
            DataSet MyDS = new DataSet();
            OdbcDataAdapter MyDA = new OdbcDataAdapter();

            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["AuthConn"].ToString();

            Mycommand.Connection = myConnection;
            msSQL = " SELECT template_content FROM ids_mst_tdocumentlist where documentlist_gid='"+ id +"'";
            MyDA.SelectCommand = Mycommand;
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;

            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "DataTable1");

            msSQL = string.Empty;


            msSQL = "select company_logo_path as company_logo from adm_mst_tcompany";

            dt = objdbconn.GetDAtaTable(msSQL, myConnection);
            DataTable2.Columns.Add("company_logo", typeof(byte[]));
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt.Rows)
                {
                    company_logo_path = HttpContext.Current.Server.MapPath((dr_datarow["company_logo"].ToString()));
                    company_logo_path = company_logo_path.Replace("EMS.Reports\\", "");
                    company_logo_path = company_logo_path.Replace("api\\", "");
                    company_logo_path = company_logo_path.Replace("/", "\\");
                    if (System.IO.File.Exists(company_logo_path) == true)
                    {
                        //Convert  Image Path to Byte
                        company_logo = System.Drawing.Image.FromFile(company_logo_path);
                        byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(company_logo, typeof(byte[]));

                        DataTable2.Rows.Add(bytes);
                    }
                }
            }
            dt.Dispose();
            DataTable2.TableName = "DataTable2";
            MyDS.Tables.Add(DataTable2);


            ReportDocument oRpt = new ReportDocument();
            oRpt.Load(Path.Combine(ConfigurationManager.AppSettings["report_file_path"].ToString(), "ems.idas/Sanctionletter.rpt"));
            oRpt.SetDataSource(MyDS);
            myConnection.Close();
            var path = Path.Combine(ConfigurationManager.AppSettings["report_path"].ToString(), "Sanctionletter_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc");
            oRpt.ExportToDisk(ExportFormatType.WordForWindows, path);

            return path;
        }
    }
}