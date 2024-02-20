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
    public class idas_rpt_ComplianceCertificate
    {

        string msSQL = string.Empty;
        dbconn objdbconn = new dbconn();
        string company_logo_path;
        Image company_logo;
        
        DataTable dt = new DataTable();
        DataTable logo = new DataTable();
        OdbcConnection myConnection = new OdbcConnection();

        public string DaGetCaseCreation(string sanction_gid)
        {
            OdbcCommand Mycommand = new OdbcCommand();
            DataSet MyDS = new DataSet();
            OdbcDataAdapter MyDA = new OdbcDataAdapter();

            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["AuthConn"].ToString();

            Mycommand.Connection = myConnection;

            msSQL = " SELECT CONCAT(b.customer_urn,'/',b.customername) as customername,a.facility_type,a.sanction_amount,c.batchcreated_name," +
                    " CONCAT(a.sanction_refno,'  &  ',CAST(date_format(a.sanction_date, '%d-%m-%Y')AS CHAR)) as sanction_date," +
                    " b.vertical_code,a.collateral_security,b.zonal_name,b.businesshead_name,b.cluster_manager_name,b.creditmgmt_name,b.relationshipmgmt_name" +
                    " FROM ocs_mst_tcustomer2sanction a" +
                    " LEFT JOIN ids_trn_tbatch c on a.customer2sanction_gid=c.sanction_gid"+
                    " INNER JOIN ocs_mst_tcustomer b on a.customer_gid = b.customer_gid" +
                    " WHERE a.customer2sanction_gid='" + sanction_gid + "'";
            MyDA.SelectCommand = Mycommand;
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;

            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "customer2sanctioninfo");

            msSQL = string.Empty;

            msSQL = " SELECT document_code ,document_name ," +
                    " if(phydocument_date is null,'-',date_format(phydocument_date, '%d-%m-%Y')) as scandocument_date," +
                    " phydocument_type as types_of_copy," +
                    " documentrecord_id ,phyfinal_remarks" +
                    " FROM ids_trn_tsanctiondocumentdtls " +
                    " WHERE sanction_gid='" + sanction_gid + "'" +
                    " order by document_code ";
          
            MyDA.SelectCommand = Mycommand;
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;

            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "idasdocumentlist");

            msSQL = string.Empty;


            msSQL = "select company_logo_path as company_logo from adm_mst_tcompany";

            dt = objdbconn.GetDAtaTable(msSQL, myConnection);
            logo.Columns.Add("company_logo", typeof(byte[]));
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt.Rows)
                {
                    company_logo_path =HttpContext .Current.Server.MapPath ((dr_datarow["company_logo"].ToString()));
                    company_logo_path = company_logo_path.Replace("EMS.Reports\\", "");
                    company_logo_path = company_logo_path.Replace("api\\", "");
                    company_logo_path = company_logo_path.Replace("/", "\\");
                    if (System.IO.File.Exists(company_logo_path) == true)
                    {
                        //Convert  Image Path to Byte
                        company_logo = System.Drawing.Image.FromFile(company_logo_path);
                        byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(company_logo, typeof(byte[]));

                        logo.Rows.Add(bytes);
                    }
                }
            }
            dt.Dispose();
            logo.TableName = "logo";
            MyDS.Tables.Add(logo);

            ReportDocument oRpt = new ReportDocument();
            oRpt.Load(Path.Combine(ConfigurationManager.AppSettings["report_file_path"].ToString(), "ems.idas/ComplianceCertificate.rpt"));
            oRpt.SetDataSource(MyDS);
            myConnection.Close();
            var path = Path.Combine(ConfigurationManager.AppSettings["report_path"].ToString (), "IDAS" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
            oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, path);
            return path;

        }
    }
}