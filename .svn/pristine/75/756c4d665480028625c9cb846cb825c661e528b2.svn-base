using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.iasn.Models;
using ems.utilities.Functions;

namespace ems.iasn.DataAccess
{
    public class DaIasnTrnMergeWorkItem
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        string msSQL;
        int mnResult = 0;

        public void DaGetWIEmailFromList(string lsemail_from, MdlWISearchList   values)
        {
            msSQL = "SELECT DISTINCT email_from"+
                " FROM isn_trn_tmaildetails "+
                " WHERE email_from LIKE '%"+lsemail_from +"%'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlWISearch  = dt_datatable.AsEnumerable().Select(row => new MdlWISearch 
                {
                    email_data = row["email_from"].ToString(),
                    
                }).ToList();
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }

        }
        public void DaGetWISubjectList(string lsemail_subject, MdlWISearchList values)
        {
            msSQL = "SELECT DISTINCT email_subject" +
                " FROM isn_trn_tmaildetails " +
                " WHERE email_subject LIKE '%" + lsemail_subject + "%'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlWISearch = dt_datatable.AsEnumerable().Select(row => new MdlWISearch 
                {
                    email_data = row["email_subject"].ToString(),

                }).ToList();
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }

        }

        public void DaGetWIZoneList(string lszone_name, MdlWISearchList values)
        {
            msSQL = "SELECT DISTINCT zone_name" +
                " FROM isn_trn_tworkitemassign " +
                " WHERE zone_name LIKE '%" + lszone_name + "%'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlWISearch = dt_datatable.AsEnumerable().Select(row => new MdlWISearch
                {
                    email_data = row["zone_name"].ToString(),

                }).ToList();
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }

        }

        public void DaGetUnTaggedWISummary(MdlWI objInputValues, WorkItemList values)
        {
            msSQL = " SELECT a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                     " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date,b.zone_name," +
                     " IF(b.checkeremployee_name is null,'-',b.checkeremployee_name) as checkeremployee_name,b.zone_gid," +
                     " b.rmemployee_gid,if (b.rmemployee_name is null,'No', upper(substr(b.rmemployee_name, 1, 1))) as initial_caps,a.aging"+
                     " FROM isn_trn_tmaildetails a" +
                     " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid=b.email_gid" +
                     " WHERE 1 = 1 AND (a.email_from='"+objInputValues .email_from + "' OR a.email_subject='"+objInputValues .email_subject +"' OR b.zone_name='"+ objInputValues .zone_name +"')"+
                     " AND ( a.email_gid <> '" + objInputValues.email_gid  + "'" +
                     " AND a.status='Pending' AND b.checkeremployee_name IS NULL) ORDER BY a.email_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlWorkItem = dt_datatable.AsEnumerable().Select(row => new MdlWorkItem
                {
                    email_gid = row["email_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    workitemref_no = row["workitemref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    checkeremployee_name = row["checkeremployee_name"].ToString(),
                    zone_name = row["zone_name"].ToString(),
                    zone_gid = row["zone_gid"].ToString(),
                    rmemployee_gid =row["rmemployee_gid"].ToString (),
                    rmemployee_name =row["initial_caps"].ToString (),
                    aging=row["aging"].ToString()

                }).ToList();
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }


        }

       
        public void DaGeTaggedWISummary(string lsemail_gid, WorkItemList values)
        {
            msSQL = " SELECT a.email_gid,a.workitemref_no,a.email_from,date_format(a.email_date,'%d-%m-%Y %h:%i %p') as email_date," +
                     " a.email_subject,a.email_content,date_format(a.created_date, '%d-%m-%Y') as created_date,b.zone_name," +
                     " IF(b.checkeremployee_name is null,'-',b.checkeremployee_name) as checkeremployee_name,b.zone_gid," +
                     " b.rmemployee_gid,if (b.rmemployee_name is null,'No', upper(substr(b.rmemployee_name, 1, 1))) as initial_caps,a.aging" +
                     " FROM isn_trn_tmaildetails a" +
                     " LEFT JOIN isn_trn_tworkitemassign b ON a.email_gid=b.email_gid" +
                     " WHERE 1 = 1 AND a.mergemail_gid ='" + lsemail_gid + "' AND status='Merged' " +
                     " ORDER BY a.email_date desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlWorkItem = dt_datatable.AsEnumerable().Select(row => new MdlWorkItem
                {
                    email_gid = row["email_gid"].ToString(),
                    email_from = row["email_from"].ToString(),
                    workitemref_no = row["workitemref_no"].ToString(),
                    created_date = row["created_date"].ToString(),
                    email_date = row["email_date"].ToString(),
                    email_subject = row["email_subject"].ToString(),
                    email_content = row["email_content"].ToString(),
                    checkeremployee_name = row["checkeremployee_name"].ToString(),
                    zone_name = row["zone_name"].ToString(),
                    zone_gid = row["zone_gid"].ToString(),
                    rmemployee_gid = row["rmemployee_gid"].ToString(),
                    rmemployee_name = row["initial_caps"].ToString(),
                    aging = row["aging"].ToString()


                }).ToList();
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }


        }

        public result DaPostMergeWI(MdlEmailGId values,string user_gid)
        {
            result objResult = new Models.result();

            foreach(var i in values.mergeemail_gid )
            {
                msSQL = " UPDATE isn_trn_tmaildetails SET" +
                        " status='Merged',"+
                        " mergemail_gid='" + values.email_gid  + "'," +
                        " updated_date=current_timestamp,updated_by='" + user_gid + "'" +
                        " WHERE email_gid='" + i  + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " INSERT INTO isn_trn_tauditlog(" +
                          " email_gid," +
                          " action_taken," +
                          " created_by)" +
                          " VALUES(" +
                          "'" + i + "'," +
                          "'MailMerge'," +
                          "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult ==1)
            {
                objResult.status = true;
                objResult.message = "Mail Merged Successfully";
            }

            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
            }

            return objResult;

        }

        public result DaPostUndoMergeWI(string email_gid, string user_gid)
        {
            result objResult = new Models.result();

            
                msSQL = " UPDATE isn_trn_tmaildetails SET" +
                        " status='Pending'," +
                        " mergemail_gid=Null," +
                        " updated_date=current_timestamp,updated_by='" + user_gid + "'" +
                        " WHERE email_gid='" + email_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " INSERT INTO isn_trn_tauditlog(" +
                           " email_gid," +
                           " action_taken," +
                           " created_by)" +
                           " VALUES(" +
                           "'" + email_gid + "'," +
                           "'MailMergeUndo'," +
                           "'" + user_gid + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Reverted";
            }

            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
            }

            return objResult;

        }
    }
}