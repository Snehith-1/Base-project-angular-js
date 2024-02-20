using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.its.Models;

namespace ems.its.DataAccess
{
    public class DaViewServiceTicket 
    {
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcConnection objODBCconnection;
        OdbcDataReader objODBCDatareader;
        dbconn objdbconn = new dbconn();
        DataTable dt_datatable;
        string msSQL;
        int mnResult;
        string flag;
        string lscomplaint_assigned;

        // View Service Ticket ................//

        public bool DaGetViewServiceticket(viewserviceticket objviewticket, string employee_gid)
        {
            try
            {

                msSQL = " select date_format(a.complaint_date,'%d-%m-%Y %h:%i %p') as complaint_date,a.complaint_refno,a.complaint_title,a.customer_gid,a.complaint_gid,g.subcategory_name,h.type_name, " +
                        " a.complaint_gid,a.subcategory_gid,a.type_gid, concat(a.raised_for,'/',b.user_firstname,' ',b.user_lastname) as raisedfor_employee," +
                        " case when m.status is null then 'No Approval' else m.status end as status, " +
                        " concat(a.customer_contactperson,' / ',a.customer_contactno,' / ',a.customer_email)  as contactdetails,f.category_name, " +
                        " a.complaint_remarks,b.user_firstname,concat(j.user_firstname,' ',j.user_lastname) as created_by,  " +
                        " CASE WHEN e.leadstage_name <>'' then e.leadstage_name else a.assign_status END as 'leadstage_name',d.campaign_gid,d.complaint2campaign_gid " +
                        " from its_trn_tcomplaint a" +
                        " left join hrm_mst_temployee c on a.raisedfor_employee=c.employee_gid " +
                        " left join adm_mst_tuser b on c.user_gid=b.user_gid " +
                        " left join hrm_mst_temployee i on a.created_by=i.employee_gid" +
                        " left join adm_mst_tuser j on j.user_gid=i.user_gid " +
                        " left join its_trn_tcomplaint2campaign d on a.complaint_gid=d.complaint_gid " +
                        " left join its_mst_tleadstage e on d.leadstage_gid=e.leadstage_gid " +
                        " left join its_mst_tcategory f on a.category_gid=f.category_gid " +
                        " left join its_mst_tsubcategory g on a.subcategory_gid=g.subcategory_gid" +
                        " left join its_mst_ttype h on a.type_gid=h.type_gid " +
                        " left join its_trn_tserviceapproval m on m.complaint_gid=a.complaint_gid " +
                        " where a.created_by='" + employee_gid + "' or a.raisedfor_employee='" + employee_gid + "'" +
                        " group by a.complaint_gid order by a.complaint_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var view_serviceticket = new List<viewservice_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        msSQL = " SELECT complaint_gid FROM its_trn_tresponselog WHERE complaint_gid='" + dr_datarow["complaint_gid"].ToString() + "' AND response_new='Y'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            flag = "Y";
                        }
                        else
                        {
                            flag = "N";
                        }
                        objODBCDatareader.Close();
                        view_serviceticket.Add(new viewservice_list
                        {

                            complaint_gid = (dr_datarow["complaint_gid"].ToString()),
                            complaint_refno = (dr_datarow["complaint_refno"].ToString()),
                            complaint_date = (dr_datarow["complaint_date"].ToString()),
                            complaint_title = (dr_datarow["complaint_title"].ToString()),
                            complaint_remarks = (dr_datarow["complaint_remarks"].ToString()),
                            user_firstname = (dr_datarow["user_firstname"].ToString()),
                            category_name = (dr_datarow["category_name"].ToString()),
                            subcategory_name = (dr_datarow["subcategory_name"].ToString()),
                            type_name = (dr_datarow["type_name"].ToString()),
                            raisedfor_employee = (dr_datarow["raisedfor_employee"].ToString()),
                            raised_by = (dr_datarow["created_by"].ToString()),
                            status_view = (dr_datarow["status"].ToString()),
                            leadstage_name = (dr_datarow["leadstage_name"].ToString()),
                            response_new = flag
                        });

                    }
                }
                objviewticket.viewservice_list = view_serviceticket;
                dt_datatable.Dispose();

                objviewticket.status = true;
                objviewticket.message = "Ticket Raised Successfully!";
           
                
                return true;
            }
            catch
            {
                objviewticket.status = false;
                objviewticket.message = "Error Occured";
                return false;
            }
        }


        public bool DaGetTicketDetails(string complaint_gid, viewticket_details objviewticketdetails)
        {
            try
            {
                msSQL = " select date_format(a.complaint_date,'%d-%m-%Y %h:%i %p') as complaint_date ,a.complaint_refno,a.complaint_title,a.customer_gid,a.complaint_gid,g.subcategory_name,h.type_name, " +
                    " a.complaint_gid,a.subcategory_gid,a.type_gid, concat(a.raised_for,'/',b.user_firstname,' ',b.user_lastname) as raisedfor_employee," +
                    " case when m.status is null then 'No Approval' else m.status end as status, " +
                    " concat(a.customer_contactperson,' / ',a.customer_contactno,' / ',a.customer_email)  as contactdetails,f.category_name, " +
                    " a.complaint_remarks,b.user_firstname, " +
                    " CASE WHEN e.leadstage_name <>'' then e.leadstage_name else a.assign_status END as 'leadstage_name',d.campaign_gid,d.complaint2campaign_gid, " +
                    " case when n.departmentrejected_remarks is not null then n.departmentrejected_remarks" +
                    " when n.managementrejected_remarks is not null then n.managementrejected_remarks" +
                    " when n.servicerejected_remarks is not null then n.servicerejected_remarks  else '-' end as remarks " +
                    " from its_trn_tcomplaint a" +
                    " left join hrm_mst_temployee c on a.raisedfor_employee=c.employee_gid " +
                    " left join adm_mst_tuser b on c.user_gid=b.user_gid " +
                    " left join its_trn_tcomplaint2campaign d on a.complaint_gid=d.complaint_gid " +
                    " left join its_mst_tleadstage e on d.leadstage_gid=e.leadstage_gid " +
                    " left join its_mst_tcategory f on a.category_gid=f.category_gid " +
                    " left join its_mst_tsubcategory g on a.subcategory_gid=g.subcategory_gid" +
                    " left join its_mst_ttype h on a.type_gid=h.type_gid " +
                    " left join its_trn_tserviceapproval m on m.complaint_gid=a.complaint_gid " +
                    " left join its_trn_tcomplaintapprovallog n on a.complaint_gid = n.complaint_gid"+
                    " where a.complaint_gid='" + complaint_gid + "' group by a.complaint_gid";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    objviewticketdetails.complaint_gid = objODBCDatareader["complaint_gid"].ToString();
                    objviewticketdetails.complaint_refno = objODBCDatareader["complaint_refno"].ToString();
                    objviewticketdetails.complaint_date = objODBCDatareader["complaint_date"].ToString();
                    objviewticketdetails.complaint_title = objODBCDatareader["complaint_title"].ToString();
                    objviewticketdetails.complaint_remarks = objODBCDatareader["complaint_remarks"].ToString();
                    objviewticketdetails.user_firstname = objODBCDatareader["user_firstname"].ToString();
                    objviewticketdetails.category_name = objODBCDatareader["category_name"].ToString();
                    objviewticketdetails.subcategory_name = objODBCDatareader["subcategory_name"].ToString();
                    objviewticketdetails.type_name = objODBCDatareader["type_name"].ToString();
                    objviewticketdetails.raisedfor_employee = objODBCDatareader["raisedfor_employee"].ToString();
                    objviewticketdetails.status_view = objODBCDatareader["status"].ToString();
                    objviewticketdetails.leadstage_name = objODBCDatareader["leadstage_name"].ToString();
                    objviewticketdetails.remarks = objODBCDatareader["remarks"].ToString();
                    objODBCDatareader.Close();
                    msSQL = " select a.complaint_gid,a.status,concat(c.user_firstname,' ',c.user_lastname) as user_firstname from its_trn_tserviceapproval a " +
                            " left join hrm_mst_temployee b on b.employee_gid=a.dept_manager " +
                            " left join adm_mst_tuser c on b.user_gid=c.user_gid " +
                            " where a.complaint_gid='" + complaint_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        objviewticketdetails.department_manager = objODBCDatareader["user_firstname"].ToString();
                        objviewticketdetails.approval_status = objODBCDatareader["status"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = " select a.manager_name from its_trn_tserviceapproval a " +
                            " where a.complaint_gid='" + complaint_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        objviewticketdetails.manager = objODBCDatareader["manager_name"].ToString();

                    }
                    objODBCDatareader.Close();
                    msSQL = " select a.complaint_gid,a.status,concat(c.user_firstname,' ',c.user_lastname) as user_firstname from its_trn_tserviceapproval a " +
                            " left join hrm_mst_temployee b on b.employee_gid=a.service_manager " +
                            " left join adm_mst_tuser c on b.user_gid=c.user_gid " +
                            " where a.complaint_gid='" + complaint_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        objviewticketdetails.service_manager = objODBCDatareader["user_firstname"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as user_firstname, " +
                         " case when d.departmentapproved_date is null then date_format(d.departmentrejected_date,'%d-%m-%Y  %h:%i %p') " +
                         " else date_format(d.departmentapproved_date,'%d-%m-%Y  %h:%i %p') END as 'DepartmentApproved/RejectedDate', " +
                         " d.departmentapproval_status, case when d.departmentrejected_remarks is not null then d.departmentrejected_remarks" +
                         " when a.department_remarks is not null then a.department_remarks end as departmentrejected_remarks from its_trn_tserviceapproval a " +
                         " left join hrm_mst_temployee b on b.employee_gid = a.dept_manager " +
                         " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                         " left join its_trn_tcomplaintapprovallog d on a.complaint_gid = d.complaint_gid " +
                         " where a.complaint_gid = '" + complaint_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objviewticketdetails.department_approval = objODBCDatareader["user_firstname"].ToString();
                        objviewticketdetails.department_approveddate = objODBCDatareader["DepartmentApproved/RejectedDate"].ToString();
                        objviewticketdetails.department_approvedstatus = objODBCDatareader["departmentapproval_status"].ToString();
                        objviewticketdetails.departmentrejected_remarks = objODBCDatareader["departmentrejected_remarks"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select a.manager_name,b.managementapproval_status,case when b.managementapproved_date is null then " +
                            " date_format(b.managementrejected_date,'%d-%m-%Y  %h:%i %p') else date_format(b.managementapproved_date,'%d-%m-%Y  %h:%i %p') " +
                            " END as 'ManagementApproved/RejectedDate',case when b.managementrejected_remarks is not null then b.managementrejected_remarks" +
                            " when a.management_remarks is not null then a.management_remarks end as managementrejected_remarks " +
                            " from its_trn_tserviceapproval a " +
                            " left join its_trn_tcomplaintapprovallog b on a.complaint_gid = b.complaint_gid " +
                            "  where a.complaint_gid='" + complaint_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        objviewticketdetails.management_approval = objODBCDatareader["manager_name"].ToString();
                        objviewticketdetails.management_approveddate = objODBCDatareader["ManagementApproved/RejectedDate"].ToString();
                        objviewticketdetails.management_approvedstatus = objODBCDatareader["managementapproval_status"].ToString();
                        objviewticketdetails.managementrejected_remarks = objODBCDatareader["managementrejected_remarks"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select a.complaint_gid,a.status,concat(c.user_firstname,' ',c.user_lastname) as user_firstname, " +
                        " case when d.serviceapproved_date is null then date_format(d.servicerejected_date,'%d-%m-%Y  %h:%i %p') " +
                        " else  date_format(d.serviceapproved_date,'%d-%m-%Y  %h:%i %p') END as 'ServiceApproved/RejectedDate', " +
                        " d.serviceapproval_status, case when d.servicerejected_remarks is not null then d.servicerejected_remarks" +
                        " when a.service_remarks is not null then a.service_remarks end as servicerejected_remarks from its_trn_tserviceapproval a " +
                        " left join hrm_mst_temployee b on b.employee_gid=a.service_manager " +
                        " left join adm_mst_tuser c on b.user_gid=c.user_gid " +
                         " left join its_trn_tcomplaintapprovallog d on a.complaint_gid = d.complaint_gid " +
                        " where a.complaint_gid='" + complaint_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        objviewticketdetails.service_approval = objODBCDatareader["user_firstname"].ToString();
                        objviewticketdetails.service_approveddate = objODBCDatareader["ServiceApproved/RejectedDate"].ToString();
                        objviewticketdetails.service_approvedstatus = objODBCDatareader["serviceapproval_status"].ToString();
                        objviewticketdetails.servicerejected_remarks = objODBCDatareader["servicerejected_remarks"].ToString();
                    }
                    objODBCDatareader.Close();
                }
                else
                {
                    objODBCDatareader.Close();
                }

                objviewticketdetails.status = true;
                return true;

            }
            catch
            {
                objviewticketdetails.status = false;
                objviewticketdetails.message = "Error Occured";
                return false;
            }
        }

        // Close Service Ticket.....//
        public bool DaPostCloseTicket(string complaint_gid, closeticket values)
        {
          try
            {
                msSQL = " update its_trn_tcomplaint2campaign set " +
               " followup_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
               " close_date ='" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
               " close_time ='" + DateTime.Now.ToString("HH:mm:ss") + "'," +
               " leadstage_gid = '4'" +
               " where complaint_gid='" + complaint_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    values.status = true;
                    return true;
                }
                else
                {
                    values.status = false;
                    return false;
                }
            }
          catch
            {
                values.status = false;
                return false;
            }
        }

        public bool DaGetIncompleteTicket(string complaint_gid, incompleteticket objincompleteticket)
        {
            try
            {
                msSQL = " update its_trn_tcomplaint2campaign set " +
                 " followup_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                 " incomplete_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                 " leadstage_gid = '2'" +
                 " where complaint_gid='" + complaint_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    objincompleteticket.status = true;
                    return true;
                }
                else
                {
                    objincompleteticket.status = false;
                    return false;
                }
            }
            catch
            {
                objincompleteticket.status = false;
                return false;
            }
        }
        // Get Uploaded Document Details

        public bool DaGetUploadDoc(viewDocument objviewDocument, string complaint_gid)
        {
            try
            {
                msSQL = " SELECT * FROM its_trn_tticketdocument where complaint_gid='" + complaint_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var Get_documentlist = new List<viewDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt_datatable.Rows)
                    {
                        Get_documentlist.Add(new viewDocumentList
                        {
                            ticketdocument_gid = dr["ticketdocument_gid"].ToString(),
                            complaint_gid = dr["complaint_gid"].ToString(),
                            document_name = dr["document_name"].ToString(),
                            document_path = HttpContext.Current.Server.MapPath(dr["document_path"].ToString())
                        });
                    }
                    objviewDocument.viewDocumentList = Get_documentlist;
                    dt_datatable.Dispose();
                    objviewDocument.status = true;
                    objviewDocument.message = "Success";
               
                    
                    return true;
                }
                else
                {
                    objviewDocument.status = false;
                    objviewDocument.message = "No Record Found";
                    return false;
                }
            }
            catch
            {
                objviewDocument.status = false;
                objviewDocument.message = "No Record Found";
                return false;
            }
        }

        // Response Log......//

        public bool DaPostResponseLog(responselog values, string employee_gid)
        {
            try
            {
                msSQL = "select complaint_assigned from its_trn_tresponselog where complaint_gid='" + values.complaint_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    lscomplaint_assigned = objODBCDatareader["complaint_assigned"].ToString();
                }
                objODBCDatareader.Close();

                string msresponse_gid = objcmnfunctions.GetMasterGID("RELOG");


                msSQL = "insert into its_trn_tresponselog ( " +
                       "responselog_gid," +
                       "complaint_gid," +
                       "response_description," +
                       " complaint_raisedby," +
                       " assigned_to," +
                       " response_new," +
                       " created_by," +
                       " created_date ) " +
                       " Values ( " +
                       "'" + msresponse_gid + "'," +
                       "'" + values.complaint_gid + "'," +
                       "'" + values.response_description.Replace("\'", "") + "'," +
                       "'" + employee_gid + "'," +
                       "'" + lscomplaint_assigned + "'," +
                       "'N'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Response Sent Successfully";
               
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured";
                    return false;
                }
            }
            catch
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        // Response Log Details......//


        public bool DaGetResponseLogDetails(string complaint_gid, responselogdetails objresponselogdetails, string employee_gid)
        {
         try
            {

                msSQL = " select responselog_gid,complaint_gid,response_new,response_description,complaint_raisedby,complaint_assigned," +
                       " created_by,date_format(created_date,'%d-%m-%Y %h:%i %p') as createddate" +
                       " from its_trn_tresponselog where complaint_gid='" + complaint_gid + "' order by created_date asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_responselogdetails = new List<responselog_detailslist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt_datatable.Rows)
                    {
                        msSQL = " select concat(a.user_code, '/', a.user_firstname, ' ', a.user_lastname) as user_name from adm_mst_tuser a " +
                                " left join hrm_mst_temployee b on a.user_gid = b.user_gid " +
                                " where b.employee_gid = '" + dr["created_by"].ToString() + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            objODBCDatareader.Read();
                            string lsresponse_employee = dr["created_by"].ToString();
                            try
                            {
                                if (lsresponse_employee == employee_gid)
                                {
                                    get_responselogdetails.Add(new responselog_detailslist
                                    {
                                        session_user = "Y",
                                        responselog_gid = dr["responselog_gid"].ToString(),
                                        complaint_raisedby = dr["complaint_raisedby"].ToString(),
                                        complaint_assigned = dr["complaint_assigned"].ToString(),
                                        complaint_gid = dr["complaint_gid"].ToString(),
                                        response_description = dr["response_description"].ToString(),
                                        created_by = (objODBCDatareader["user_name"].ToString()),
                                        created_date = dr["createddate"].ToString()

                                    });
                                }
                                else
                                {
                                    get_responselogdetails.Add(new responselog_detailslist
                                    {
                                        session_user = "N",
                                        responselog_gid = dr["responselog_gid"].ToString(),
                                        complaint_raisedby = dr["complaint_raisedby"].ToString(),
                                        complaint_assigned = dr["complaint_assigned"].ToString(),
                                        complaint_gid = dr["complaint_gid"].ToString(),
                                        response_description = dr["response_description"].ToString(),
                                        created_by = (objODBCDatareader["user_name"].ToString()),
                                        created_date = dr["createddate"].ToString()

                                    });
                                }
                            }

                            catch (Exception ex)
                            {
                                var msg = ex.ToString();
                            }
                            objODBCDatareader.Close();
                        }
                        else
                        {
                            objODBCDatareader.Close();
                        }

                    }
                }
                objresponselogdetails.responselog_detailslist = get_responselogdetails;
                dt_datatable.Dispose();

                msSQL = "select response_new,complaint_assigned from its_trn_tresponselog where complaint_gid='" + complaint_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        msSQL = " update its_trn_tresponselog set response_new='' where response_new='Y' and " +
                                " complaint_gid='" + complaint_gid + "' and created_by='" + dt["complaint_assigned"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                dt_datatable.Dispose();


                if (mnResult == 1)
                {
                    objresponselogdetails.status = true;
                    return true;
                }
                else
                {
                    objresponselogdetails.status = false;
                    return false;
                }
            }
            catch
            {
                objresponselogdetails.status = false;
                return false;
            }
        }

    }
}
