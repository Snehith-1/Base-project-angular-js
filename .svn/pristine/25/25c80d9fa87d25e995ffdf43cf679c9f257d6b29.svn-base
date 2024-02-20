using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ems.idas.Models;
using System.Configuration;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace ems.idas.DataAccess
{
    public class DaIdasTrnMakerCheckerDtls
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL;
        int mnResult;
        string lsuser_name = string.Empty;
        string msGetGid,msGetGidDoc;

        // Get Mail ID
        public void DaGetMailid(string customer_gid, mdlmailid objmdlmailid)
        {
            try
            {
                msSQL = " SELECT b.employee_emailid " +
                        " FROM ocs_mst_tcustomer a " +
                        " LEFT JOIN hrm_mst_temployee b ON a.relationship_manager = b.employee_gid" +
                        " WHERE a.customer_gid='" + customer_gid + "'";
                objmdlmailid.rmmail_id = objdbconn.GetExecuteScalar(msSQL);

                objmdlmailid.status = true;
                objmdlmailid.message = "Success";

            }
            catch
            {
                objmdlmailid.status = false;
                objmdlmailid.message = "Failure";
            }
          

        }

        #region View Status Update
        public void DaPostCadQuieryRMViwed(mdlviewupdate objResult)
        {
            msSQL = " UPDATE ids_trn_tdocconversation SET" +
                    " view_status='CQ-Viewed'" +
                    " WHERE sanctiondocument_gid='" + objResult.sanctiondocument_gid + "'AND type_of_conversation='" + objResult.types_of_conversation + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
            }
        }
        public void DaPostRmResponseCadViwed(mdlviewupdate objResult)
        {
            msSQL = " UPDATE ids_trn_tdocconversation SET" +
                    " view_status='Res-Viewed'" +
                   " WHERE sanctiondocument_gid='" + objResult.sanctiondocument_gid + "' AND type_of_conversation='" + objResult.types_of_conversation + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                objResult.status = true;
            }
        }
        #endregion

        public void DaPostForwardQuery(string user_gid,string docconversation_gid,result objResult)
        {

            int query_no = 0;
            lsuser_name = objdbconn.GetExecuteScalar("SELECT concat(user_firstname,' ',user_lastname)as user  FROM adm_mst_tuser WHERE user_gid='" + user_gid + "'");

            var sanctiondocument_gid = objdbconn.GetExecuteScalar("SELECT DISTINCT sanctiondocument_gid FROM ids_trn_tdocconversation WHERE docconversation_gid='"+ docconversation_gid +"'");
            var lsquery_no = objdbconn.GetExecuteScalar("SELECT COUNT(*) FROM ids_trn_tdocconversation WHERE sanctiondocument_gid='" + sanctiondocument_gid + "' AND type_of_conversation='External' AND type_of_doc='Scan Copy'");
            if (lsquery_no == "")
            {
                query_no = 1;
            }
            else
            {
                query_no = Convert.ToInt16(lsquery_no) + 1;
            }
            msGetGid = objcmnfunctions.GetMasterGID("DOCC");
            msGetGidDoc = objcmnfunctions.GetMasterGID("DOCV");
            msSQL = " INSERT INTO ids_trn_tdocconversation(" +
                   " docconversation_gid ," +
                   " sanctiondocument_gid," +
                   " sanction_gid," +
                   " document_gid," +
                   " document_name," +
                   " docconversationref_no," +
                   " reference_query," +
                   " type_of_conversation," +
                   " type_of_doc," +
                   " cad_query," +
                   " cad_name," +
                   " cad_gid," +
                   " cadquery_on," +
                   " query_no," +
                   " created_by," +
                   " created_date)" +
                   " SELECT '"+ msGetGidDoc + "',sanctiondocument_gid,sanction_gid,document_gid,document_name,'"+ msGetGid + "',docconversationref_no,'External'," +
                   " type_of_doc,cad_query,cad_name,cad_gid,cadquery_on," +
                   " '"+ query_no +"','"+ user_gid +"',current_timestamp"+
                   " from ids_trn_tdocconversation "+
                   " WHERE docconversation_gid='" + docconversation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " UPDATE ids_trn_tdocconversation SET" +
                   " flag='Y'," +
                   " forwarded_flag='N'," +
                   " rm_response='Checker Forward this query Directly to RM'," +
                   " view_status='Res'," +
                   " relationshipmgr_gid='" + user_gid + "'," +
                   " relationshipmgr_name='" + lsuser_name + "'," +
                   " relationshipmgrquery_on=current_timestamp" +
                   " WHERE docconversation_gid='" + docconversation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = " UPDATE ids_trn_tdocconversation SET"+
                    " view_status='Res',"+
                    " forwarded_on=current_timestamp," +
                    " forwarded_by_gid='"+user_gid+"',"+
                    " forwarded_by_name='"+lsuser_name+"',"+
                    " forwarded_flag='Y'"+
                    " WHERE docconversation_gid='" + msGetGidDoc + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if(mnResult ==1)
            {
                objResult.status = true;
                objResult.message = "Query Forwarded Successfully...";

            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";

            }
        }

        public void DaGetCreditMailId(mdlmailid objmdlmailid)
        {
            try
            {
                msSQL = " select a.employee_emailid from hrm_mst_temployee a " +
                    " left join hrm_mst_tdepartment b on b.department_gid=a.department_gid " +
                    " where b.department_name like '%Credit Administration%'";
                objmdlmailid.creditmail_id = objdbconn.GetExecuteScalar(msSQL);

                objmdlmailid.status = true;
                objmdlmailid.message = "Success";

            }
            catch
            {
                objmdlmailid.status = false;
                objmdlmailid.message = "Failure";
            }


        }
    }
}