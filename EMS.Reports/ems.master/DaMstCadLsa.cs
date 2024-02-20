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
{
    public class DaMstCadLsa
    {
        string msSQL = string.Empty;
        dbconn objdbconn = new dbconn();
        string company_logo_path;
        Image company_logo; 
        byte image_byte;
        OdbcDataReader objODBCDatareader;
        DataTable dt = new DataTable();
        DataTable CompanyLogo = new DataTable();
        OdbcConnection myConnection = new OdbcConnection();
        public string DaGetCADLSAreport(string generatelsa_gid)
        {
            OdbcCommand Mycommand = new OdbcCommand();
            DataSet MyDS = new DataSet();
            OdbcDataAdapter MyDA = new OdbcDataAdapter();

            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["AuthConn"].ToString();

            Mycommand.Connection = myConnection;

            msSQL = " select a.application_gid from ocs_trn_tgeneratelsa a" +
                " where generatelsa_gid = '" + generatelsa_gid + "'";
            string lsapplication_gid = objdbconn.GetExecuteScalar(msSQL); 

            msSQL = " select lsa_refno, date_format(lsa_approveddate,'%d-%m-%Y') as lsa_approveddate, branch_name, c.region,k.approver_name, " +
                    " c.vertical_name,  b.sanctionto_name as nameof_theborrower, date_format(m.securityassessed_date, '%d-%m-%Y') as securityassessed_date," +
                    " b.contactperson_address as addressof_theborrrower, c.constitution_name, " +
                    " b.sanction_refno,  date_format(b.sanction_date,'%d-%m-%Y') as sanction_date,b.sanction_type, group_concat(c.ccgroup_name) as ccgroup_name, " +
                    " date_format(c.cccompleted_date, '%d-%m-%Y %h:%i %p') as ccapproved_date, " +
                    " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code, ' / ', d.employee_emailid) as rm_name, " +
                    " concat(c.clustermanager_name, ' / ', f.employee_emailid) as cluster_name, " +
                    " concat(c.zonalhead_name, ' / ', g.employee_emailid) as zonalhead_name, " +
                    " concat(c.businesshead_name, ' / ', h.employee_emailid) as businesshead_name, " +
                    " concat(c.creditmanager_name, ' / ', i.employee_emailid) as creditmanager_name,  " +
                    " allpredisbursement_stipulated, alldeferralcovenant_captured, maker_signaturename " +
                    " from ocs_trn_tgeneratelsa a " +
                    " left join ocs_trn_tapplication2sanction b on a.application_gid = b.application_gid " +
                    " left join ocs_trn_tcadapplication c on a.application_gid = c.application_gid " +
                    " left join ocs_trn_tcadapplication2hypothecation m on a.application_gid = m.application_gid " +
                    " left join hrm_mst_temployee d on d.employee_gid = c.created_by " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " left join hrm_mst_temployee f on f.employee_gid = c.clustermanager_gid " +
                    " left join hrm_mst_temployee g on g.employee_gid = c.zonalhead_gid " +
                    " left join hrm_mst_temployee h on h.employee_gid = c.businesshead_gid " +
                    " left join hrm_mst_temployee i on i.employee_gid = c.creditmanager_gid " +
                    " left join ocs_trn_tlsacompliancecheckdetail j on a.generatelsa_gid =  j.generatelsa_gid " +
                    " left join ocs_trn_tprocesstype_assign k on a.application_gid =  k.application_gid and k.menu_gid = 'CADMGTLSA' " +
                    " where a.generatelsa_gid = '" + generatelsa_gid + "'";
            MyDA.SelectCommand = Mycommand;
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;

            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "CustomerInform");

            msSQL = " select b.sanctionto_gid from ocs_trn_tgeneratelsa a" +
                    " left join ocs_trn_tapplication2sanction b on a.application_gid = b.application_gid " +
                    " where generatelsa_gid = '" + generatelsa_gid + "'";
            string lssanctionto_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = string.Empty;
            msSQL = " (select urn, companypan_no  from ocs_trn_tcadinstitution where institution_gid = '" + lssanctionto_gid + "') " +
                    " union (select group_urn, '' from ocs_trn_tcadgroup  where group_gid = '" + lssanctionto_gid + "') " +
                    " union (select urn, pan_no from ocs_trn_tcadcontact where contact_gid = '" + lssanctionto_gid + "')";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "Customerurnpan");

            msSQL = string.Empty;

        

            msSQL = " SELECT @a:= @a + 1 serial_number, enduse_purpose, concat(product_type, ' / ', productsub_type) as product_type , " +
                    " loan_type FROM ocs_trn_tcadapplication2loan, (SELECT @a:= 0) AS a where application_gid = '" + lsapplication_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "Customerloaninfo");


            msSQL = " select a.interchangeability,a.report_structure, concat(a.product_type , ' / ', a.productsub_type) as product_type, " +
                    " (b.rate_interest) as rate, (b.margin) as margin,concat(b.product_name , ' / ', b.variety_name) as product_name, format(documented_limit, 2, 'en_IN') as documented_limit, format(existing_limit, 2, 'en_IN') as existing_limit, " +
                    " format(limit_released, 2, 'en_IN') as limit_released, tenureoverall_limit, facility_mode as revolving_nonrevolvingtype,  " +
                    " penal_interest, date_format(dateof_Expiry, '%d-%m-%Y') as dateof_Expiry, limitinfo_remarks from ocs_trn_tlimitproductinfo a " +
                    " left join ocs_trn_tcadapplication2loan b on a.application2loan_gid = b.application2loan_gid  " +
                    " where generatelsa_gid = '" + generatelsa_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "limitinfo");
            msSQL = string.Empty;


            msSQL = " select  format(sum(limit_released),2,'en_IN') as total_limit_released, format(sum(documented_limit),2,'en_IN') as total_doc_limit " +
                    " from ocs_trn_tlimitproductinfo a  where a.generatelsa_gid = '" + generatelsa_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "limitinfototal");
            msSQL = string.Empty;


            msSQL = " SELECT  @a:=@a+1  serial_number, concat(product_type , ' / ', productsub_type, ' - ', limitinfo_remarks) as lsa_notes " +
                    " FROM ocs_trn_tlimitproductinfo, (SELECT @a:= 0) AS a where generatelsa_gid = '" + generatelsa_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "limitinfonotes");
            msSQL = string.Empty;


            msSQL = " select concat(a.product_type , ' / ', a.productsub_type) as product_type," +
                " case when interestfrequency_name = ' -----Select Interest Frequency -----' then  '' " +
                  " else interestfrequency_name end as interestfrequency_name, " +
                  " case when principalfrequency_name = ' -----Select Principal Frequency -----' then  '' " +
                  " else principalfrequency_name end as principalfrequency_name," +
                    " moratorium_type from ocs_trn_tcadapplication2loan a " +
                    " left join ocs_trn_tgeneratelsa b on a.application_gid = b.application_gid " +
                    " where generatelsa_gid = '" + generatelsa_gid + "'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "repaymentdetails");
            msSQL = string.Empty;



            msSQL = " (select  bank_name ,ifsc_code, bankaccount_number from ocs_trn_tcadcreditbankdtl a " +
                    " where application_gid = '" + lsapplication_gid + "' and disbursement_accountstatus = 'Yes') " +
                    " union (select bank_name, ifsc_code, bankaccount_number " +
                    " from ocs_trn_tlsabankaccountdtl where generatelsa_gid = '" + generatelsa_gid + "' and disbursement_accountstatus = 'Yes')";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "customerbankdetails");
            msSQL = string.Empty;
             

            msSQL = " select approver_name from ocs_trn_tprocesstype_assign a " +
                    " left join ocs_trn_tgeneratelsa b on a.application_gid = b.application_gid " +
                    " where generatelsa_gid = '" + generatelsa_gid + "' and a.menu_gid = 'CADMGTLSA'";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "makername");
            msSQL = string.Empty;

            msSQL = " select b.product_type,charge_typename,charge_applicable, charge_amount, " + 
                    " alreadycol_recoveredamount, alreadycol_gst, alreadycol_gstinpercentage, alreadycol_totalamount, " +
                    " alreadycol_remainingamountcollected, alreadycol_Chequenodetails, " +
                    " date_format(alreadycol_Cheque_date, '%d-%m-%Y') as alreadycol_Cheque_date,alreadycol_accountnumber, alreadycol_bankname, " +
                    " tobecol_recoveredamount,  tobecol_remainingamountcollected,tobecol_gst,tobecol_gstinpercentage, " +
                    " tobecol_totalamount,tobecol_Chequenodetails, date_format(tobecol_Cheque_date, '%d-%m-%Y') as tobecol_Cheque_date, " +
                    " tobecol_accountnumber,tobecol_bankname,charge_remarks  " +
                    " from ocs_trn_tlsachargestype a  " + 
                    " left join ocs_trn_tlsafeescharge b on a.lsafeescharge_gid = b.lsafeescharge_gid " +
                    " where a.generatelsa_gid = '" + generatelsa_gid + "' order by b.application2servicecharge_gid asc, charge_typeid asc ";
            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "feeandcharges");
            msSQL = string.Empty;

            msSQL = " (select  sa_name from ocs_trn_tcadapplication " +
                  " where application_gid = '" + lsapplication_gid + "' and sa_status = 'Yes') ";

            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "sacodedetails");
            msSQL = string.Empty;



            msSQL = " (select group_concat(gst_no) as gst_no from ocs_trn_tcadinstitution2branch a " +
                " left join ocs_trn_tcadinstitution b on b.institution_gid = a.institution_gid " +
                 " where b.application_gid = '" + lsapplication_gid + "' and a.headoffice_status = 'Yes') ";

            Mycommand.CommandText = msSQL;
            Mycommand.CommandType = CommandType.Text;
            MyDS.EnforceConstraints = false;

            MyDA.Fill(MyDS, "gstdetails");
            msSQL = string.Empty;

            msSQL = "select company_logo_path as company_logo from adm_mst_tcompany";

            dt = objdbconn.GetDAtaTable(msSQL, myConnection);
            CompanyLogo.Columns.Add("company_logo", typeof(byte[]));
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt.Rows)
                {
                    company_logo_path = HttpContext.Current.Server.MapPath("../../" + dr_datarow["company_logo"].ToString());
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
            CompanyLogo.TableName = "Companylogo";
            MyDS.Tables.Add(CompanyLogo);



            ReportDocument oRpt = new ReportDocument(); 

            var test = Path.Combine(ConfigurationManager.AppSettings["report_file_path"].ToString(), "ems.master/RptMstCadLsa.rpt");
            oRpt.Load(Path.Combine(ConfigurationManager.AppSettings["report_file_path"].ToString(), "ems.master/RptMstCadLsa.rpt"));
            oRpt.SetDataSource(MyDS);
            myConnection.Close();
            var path = Path.Combine(ConfigurationManager.AppSettings["report_path"].ToString(), "LSA" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf");
            oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, path);

            return path;
        }
    }
}



 