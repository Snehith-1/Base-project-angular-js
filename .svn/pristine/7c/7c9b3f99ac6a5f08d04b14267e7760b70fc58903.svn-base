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

namespace EMS.Reports.ems.lgl
{
    public class rpt_DN1format
    {
        string msSQL = string.Empty;
        dbconn objdbconn = new dbconn();
        string  company_logo_path;
        Image  company_logo;
        ImageConverter visitimage_converter;
        byte image_byte;

        DataTable dt = new DataTable();
        DataTable DataTable2 = new DataTable();
      //  DataTable DataTable3 = new DataTable();

        OdbcConnection myConnection = new OdbcConnection();
        public string gettemplatecontentdn1(string id)
        {
            OdbcCommand Mycommand = new OdbcCommand();
            DataSet MyDS = new DataSet();
            OdbcDataAdapter MyDA = new OdbcDataAdapter();

            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["AuthConn"].ToString();

            Mycommand.Connection = myConnection;

            msSQL = " SELECT DN1template_content,DN2template_content,DN3template_content,cbotemplate_content FROM lgl_tmp_tdnformat where customer_urn='" + id + "' and status='Opening'";
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
            oRpt.Load(Path.Combine(ConfigurationManager.AppSettings["report_file_path"].ToString(), "ems.lgl/DN1format.rpt"));
            oRpt.SetDataSource(MyDS);
            myConnection.Close();
            var path = Path.Combine(ConfigurationManager.AppSettings["report_path"].ToString(), "DN1format_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
            oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, path);
            return path;
        }
        public string gettemplatecontentdn2(string id)
        {
            OdbcCommand Mycommand = new OdbcCommand();
            DataSet MyDS = new DataSet();
            OdbcDataAdapter MyDA = new OdbcDataAdapter();

            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["AuthConn"].ToString();

            Mycommand.Connection = myConnection;

            msSQL = " SELECT DN1template_content,DN2template_content,DN3template_content,cbotemplate_content FROM lgl_tmp_tdnformat where customer_urn='" + id + "'and status='Opening'";
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
            oRpt.Load(Path.Combine(ConfigurationManager.AppSettings["report_file_path"].ToString(), "ems.lgl/DN2format.rpt"));
            oRpt.SetDataSource(MyDS);
            myConnection.Close();
            var path = Path.Combine(ConfigurationManager.AppSettings["report_path"].ToString(), "DN2format_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
            oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, path);
            return path;
        }
        public string gettemplatecontentdn3(string id)
        {
            OdbcCommand Mycommand = new OdbcCommand();
            DataSet MyDS = new DataSet();
            OdbcDataAdapter MyDA = new OdbcDataAdapter();

            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["AuthConn"].ToString();

            Mycommand.Connection = myConnection;

            msSQL = " SELECT DN1template_content,DN2template_content,DN3template_content,cbotemplate_content FROM lgl_tmp_tdnformat where customer_urn='" + id + "'and status='Opening'";
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
            oRpt.Load(Path.Combine(ConfigurationManager.AppSettings["report_file_path"].ToString(), "ems.lgl/DN3format.rpt"));
            oRpt.SetDataSource(MyDS);
            myConnection.Close();
            var path = Path.Combine(ConfigurationManager.AppSettings["report_path"].ToString(), "DN3format_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
            oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, path);
            return path;
        }
        public string gettemplatecontentCBO(string id)
        {
            OdbcCommand Mycommand = new OdbcCommand();
            DataSet MyDS = new DataSet();
            OdbcDataAdapter MyDA = new OdbcDataAdapter();

            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["AuthConn"].ToString();

            Mycommand.Connection = myConnection;

            msSQL = " SELECT DN1template_content,DN2template_content,DN3template_content,cbotemplate_content FROM lgl_tmp_tdnformat where customer_urn='" + id + "'and status='Opening'";
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
            oRpt.Load(Path.Combine(ConfigurationManager.AppSettings["report_file_path"].ToString(), "ems.lgl/CBOformat.rpt"));
            oRpt.SetDataSource(MyDS);
            myConnection.Close();
            var path = Path.Combine(ConfigurationManager.AppSettings["report_path"].ToString(), "CBOformat_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
            oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, path);
            return path;
        }


    }
}