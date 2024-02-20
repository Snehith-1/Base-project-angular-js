using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.its.Models;
using System.Configuration;


namespace ems.its.DataAccess
{
    public class DaFeedBack
    {
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        dbconn objdbconn = new dbconn();
        string msSQL;
        int mnResult;
        string lscomplaint_gid;
        DateTime lsexpiry_date;
        string lsfeedback_flag;

        public void GetFeedbackDtl(string token, viewticket_details values)
        {
            DateTime today = DateTime.Now;
            string lscompany_name = ConfigurationManager.AppSettings["externalportal"].ToString();
            msSQL = "select complaint_gid,expiry_time,feedback_flag from " + lscompany_name + ".its_trn_tfeedback where token='" + token + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscomplaint_gid = objODBCDatareader["complaint_gid"].ToString();
                lsexpiry_date = Convert.ToDateTime(objODBCDatareader["expiry_time"].ToString());
                lsfeedback_flag = objODBCDatareader["feedback_flag"].ToString();
                objODBCDatareader.Close();

                if ((lsexpiry_date > today) && (lsfeedback_flag != "Y"))
                {
                    msSQL = " select date_format(a.complaint_date,'%d-%m-%Y %h:%i %p') as complaint_date ,a.complaint_refno,a.complaint_title,a.customer_gid,a.complaint_gid,g.subcategory_name,h.type_name, " +
                    " a.complaint_gid,a.subcategory_gid,a.type_gid, concat(a.raised_for,'/',b.user_firstname,' ',b.user_lastname) as raisedfor_employee," +
                    " case when m.status is null then 'No Approval' else m.status end as status, " +
                    " f.category_name, a.complaint_remarks, " +
                    " CASE WHEN e.leadstage_name <>'' then e.leadstage_name else a.assign_status END as 'leadstage_name' " +
                    " from " + lscompany_name + ".its_trn_tcomplaint a" +
                    " left join " + lscompany_name + ".hrm_mst_temployee c on a.raisedfor_employee=c.employee_gid " +
                    " left join " + lscompany_name + ".adm_mst_tuser b on c.user_gid=b.user_gid " +
                    " left join " + lscompany_name + ".its_trn_tcomplaint2campaign d on a.complaint_gid=d.complaint_gid " +
                    " left join " + lscompany_name + ".its_mst_tleadstage e on d.leadstage_gid=e.leadstage_gid " +
                    " left join " + lscompany_name + ".its_mst_tcategory f on a.category_gid=f.category_gid " +
                    " left join " + lscompany_name + ".its_mst_tsubcategory g on a.subcategory_gid=g.subcategory_gid" +
                    " left join " + lscompany_name + ".its_mst_ttype h on a.type_gid=h.type_gid " +
                    " left join " + lscompany_name + ".its_trn_tserviceapproval m on m.complaint_gid=a.complaint_gid " +
                    " where a.complaint_gid='" + lscomplaint_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.complaint_refno = objODBCDatareader["complaint_refno"].ToString();
                        values.complaint_date = objODBCDatareader["complaint_date"].ToString();
                        values.complaint_title = objODBCDatareader["complaint_title"].ToString();
                        values.complaint_remarks = objODBCDatareader["complaint_remarks"].ToString();
                        values.category_name = objODBCDatareader["category_name"].ToString();
                        values.subcategory_name = objODBCDatareader["subcategory_name"].ToString();
                        values.type_name = objODBCDatareader["type_name"].ToString();
                        values.raisedfor_employee = objODBCDatareader["raisedfor_employee"].ToString();
                        values.leadstage_name = objODBCDatareader["leadstage_name"].ToString();

                        values.status = true;
                    }
                    objODBCDatareader.Close();

                }
                else
                {
                    if (lsfeedback_flag != "Y")
                    {
                        msSQL = " update " + lscompany_name + ".its_trn_tfeedback set " +
                                " rating_count='5'," +
                                " rating_text='Very Good'," +
                                " remarks ='System Generated Feedback'," +
                                " feedback_flag ='Y'," +
                                " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                " where token='" + token + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update " + lscompany_name + ".its_trn_tcomplaint2campaign set " +
                                " followup_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                " close_date ='" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                                " close_time ='" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                                " leadstage_gid = '4'" +
                                " where complaint_gid='" + lscomplaint_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                    values.status = false;
                    values.message = "Already Feedback Submitted or Expired";
                }
            }
            else
            {
                objODBCDatareader.Close();
                values.status = false;
            }

        }

        public void DaPostFeedbackdtl(feedbackdtl values)
        {
            if(values.feedback_remarks!=null)
            {
                values.feedback_remarks = values.feedback_remarks.Replace("'", "\\'");
               
            }
            else
            {
                values.feedback_remarks = "";
            }
            string lscompany_name = ConfigurationManager.AppSettings["externalportal"].ToString();

            msSQL = " update " + lscompany_name + ".its_trn_tfeedback set " +
               " rating_count='" + values.rating_count + "'," +
               " rating_text='" + values.rating_text + "'," +
               " remarks ='" + values.feedback_remarks + "'," +
               " feedback_flag ='Y'," +
               " updated_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
               " where token='" + values.token + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select complaint_gid from " + lscompany_name + ".its_trn_tfeedback where token='" + values.token + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lscomplaint_gid = objODBCDatareader["complaint_gid"].ToString();
            }
            objODBCDatareader.Close();

                msSQL = " update " + lscompany_name + ".its_trn_tcomplaint2campaign set " +
               " followup_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
               " close_date ='" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
               " close_time ='" + DateTime.Now.ToString("HH:mm:ss") + "'," +
               " leadstage_gid = '4'" +
               " where complaint_gid='" + lscomplaint_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Feedback Sent Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }
    }
}