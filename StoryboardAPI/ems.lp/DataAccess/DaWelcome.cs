using ems.lp.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Web;

namespace ems.lp.DataAccess
{
    public class DaWelcome
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcConnection objODBCconnection;
        OdbcDataReader objodbcdatareader, objodbcdatareader1;
        string msSQL;
        int mnresult;


        public void DaGetLawyerDetails(lawyerdetails values, string lawyeruser_gid)
        {
            try
            {
                msSQL = " select a.lawyerregister_gid,a.lawyerref_no,a.lawyer_name,a.lawyerphoto_path,a.educational_qualification from lgl_mst_tregisterlawyer a " +
                  " left join lgl_mst_tlawyeruser b on a.lawyerregister_gid=b.lawyerregister_gid" +
                  " where b.lawyeruser_gid='" + lawyeruser_gid + "'";
                objodbcdatareader = objdbconn.GetDataReader(msSQL);
                if (objodbcdatareader.HasRows == true)
                {
                    values.lawyeruser_code = objodbcdatareader["lawyerref_no"].ToString();
                    values.lawyer_name = objodbcdatareader["lawyer_name"].ToString();
                    if (objodbcdatareader["lawyerphoto_path"].ToString() != "")
                    {
                        values.lawyer_photo =  HttpContext.Current.Server.MapPath("../../" + objodbcdatareader["lawyerphoto_path"].ToString());
                    }
                    else
                    {
                        values.lawyer_photo = "N";
                    }
                    values.educational_qualification = objodbcdatareader["educational_qualification"].ToString();
                }
                else
                {
                    msSQL = " select a.lawfirm_gid,a.firm_refno,a.firm_name from lgl_mst_tlawfirm a " +
                 " left join lgl_mst_tlawyeruser b on a.lawfirm_gid=b.lawyerregister_gid" +
                 " where b.lawyeruser_gid='" + lawyeruser_gid + "'";
                    objodbcdatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objodbcdatareader1.HasRows == true)
                    {
                        values.lawyeruser_code = objodbcdatareader1["firm_refno"].ToString();
                        values.lawyer_name = objodbcdatareader1["firm_name"].ToString();
                       
                    }
                }
                objodbcdatareader.Close();

                msSQL = " select a.compliance_assignedcount,b.legalSR_assignedcount,c.invoice_count from " +
                    " (select count(*) as compliance_assignedcount from lgl_mst_trequestcompliance where assign_lawyergid = '" + lawyeruser_gid + "') as a ," +
                    " (select count(*) as legalSR_assignedcount from lgl_trn_traiselegalSR where SRassigned_lawyer = '" + lawyeruser_gid + "') as b," +
                    " (select count(*) as invoice_count from lgl_trn_tlawyerinvoice where  created_by= '" + lawyeruser_gid + "') as c";
                objodbcdatareader = objdbconn.GetDataReader(msSQL);
                if (objodbcdatareader.HasRows == true)
                {
                    values.count_compliance = objodbcdatareader["compliance_assignedcount"].ToString();
                    values.count_legalSR = objodbcdatareader["legalSR_assignedcount"].ToString();
                    values.count_invoice = objodbcdatareader["invoice_count"].ToString();
                }

                objodbcdatareader.Close();

                values.status = true;
                values.message = "Success";
            }


            catch
            {
                values.status = true;
                values.message = "Failure";
            }
        }
        public void DaPostLawyerEmail(lawyerdetails values)
        {
           msSQL= "select lawyerregister_gid from lgl_mst_tregisterlawyer where email_address='" + values.lawyer_email_id+"' or "+
               " lawyerref_no ='"+ values.user_code+"'";
            string lslawyerregister_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lslawyerregister_gid == "")
            {
                values.status= false;
            }
            else
            {
                values.status = true;
            }
        }
        public void Daupdatelawyerpassword(lawyerdetails values)
        {
            msSQL = "update lgl_mst_tregisterlawyer set lawyeruser_password='"+ values.lawyer_password+"' where email_address='" + values.lawyer_email_id + "' or " +
                " lawyerref_no ='" + values.user_code + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_mst_tlawyeruser set lawyeruser_password='" + objcmnfunctions.ConvertToAscii(values.lawyer_password) + "'" +
                " where lawyeruser_code='" + values.user_code + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult == 0)
            {
                values.status = false;
            }
            else
            {
                values.status = true;
            }
        }

    }
}