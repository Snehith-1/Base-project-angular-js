using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.its.Models;
using System.Web;

namespace ems.its.DataAccess
{
    public class DaMyApprovals
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcConnection objODBCconnection;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL;
        int mnResult;
        bool mailflag;
        string msgetlead2campaign_gid, msgetmail_gid;
        string lscomplaint_gid, lscategory_gid;
        string lsapproval_status, lsinternalapproval;
        string lscomplaintgid, lscategorygid;
        string lsmanager_gid, lsmanagername;
        string lsservicemanager, msgetgid;
        string lscampaign_gid, lsdepartment_manager;
        string lsservice_manager, lscategory_name;
        string lsraised_employee, lscomplaint_refno;
        string lscomplaint_title, lscomplaint_remarks;
        string lscomplaint_date, lsstatus;
        string lsemployee_gid, employee_mailid;
        string message, lspop_mail, lsmail_status;
        string lsrelease_gid, lsdependent_approvalcount;
        string lsdependency_approvedcount;
        string lscap_approvalcount, lsapprovedcount;
        string lsissuetracker_gid, lsapprovalgid;
        string lsdepndency_approval, lscab_approval, lsteam_gid;
        string lsrole, approval_count, head_count, lsdependencyapproval_gid, lscacapproval_gid, total_head_count;
        string lsinprogress_count, lsnoofassigned_employee, lsapproval_member;
        


        //   Department Approval..............//

        public bool DAGetDepartmentApproval(myapproval objdepartmentapproval, string employee_gid, string user_gid)
        {
            try
            {

                msSQL = " select serviceapproval_gid,e.user_code,date_format(f.complaint_date,'%d-%m-%Y %h:%i %p') as complaint_date,f.complaint_remarks, " +
                    " f.complaint_gid,f.complaint_refno, f.complaint_title, a.dept_manager, a.service_manager, a.status, " +
                    "  concat(e.user_firstname,'',e.user_lastname,'/',f.department_name) as raisedfor_employee, " +
                    " concat(j.category_name,'/',i.subcategory_name,'/',k.type_name) as category_name,f.category_gid " +
                    " from its_trn_tserviceapproval a " +
                    " left join its_trn_tcomplaint f on f.complaint_gid=a.complaint_gid " +
                    " left join hrm_mst_temployee d on d.employee_gid = f.raisedfor_employee " +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                    " left join its_mst_tcategory j on j.category_gid =f.category_gid " +
                    " left join its_mst_tsubcategory i on i.subcategory_gid=f.subcategory_gid " +
                    " left join its_mst_ttype k on k.type_gid = f.type_gid " +
                    " where a.dept_manager = '" + employee_gid + "' and a.status='Department Approval Pending' and (a.ticket_approval='Y' or a.ticket_approval='C') " +
                    " ORDER BY FIELD(a.status, 'Department Approval Pending'),a.complaint_gid desc;";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var department_approval = new List<departmentapproval_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lscomplaint_gid = (dr_datarow["complaint_gid"].ToString());
                        lscategory_gid = (dr_datarow["category_gid"].ToString());
                        lsapproval_status = (dr_datarow["status"].ToString());
                        msSQL = " select status from its_trn_tserviceapproval a " +
                               " where a.dept_manager = '" + employee_gid + "' and a.status='Department Approval Pending' and a.complaint_gid = '" + lscomplaint_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            objODBCDatareader.Close();
                            msSQL = " select internal_approval from its_mst_tcategory " +
                                     " where category_gid='" + lscategory_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsinternalapproval = objODBCDatareader["internal_approval"].ToString();
                            }
                            if (lsinternalapproval == "Y" && lsapproval_status == "Department Approval Pending")
                            {
                                lsinternalapproval = "Y";
                            }
                            objODBCDatareader.Close();
                        }
                        if (lsinternalapproval == null && lsapproval_status == "Department Approval Pending")
                        {
                            lsinternalapproval = "S";
                        }
                        department_approval.Add(new departmentapproval_list
                        {
                            lsinternalapproval = lsinternalapproval,
                            complaint_gid = (dr_datarow["complaint_gid"].ToString()),
                            category_gid = (dr_datarow["category_gid"].ToString()),
                            complaint_refno = (dr_datarow["complaint_refno"].ToString()),
                            complaint_date = (dr_datarow["complaint_date"].ToString()),
                            complaint_title = (dr_datarow["complaint_title"].ToString()),
                            complaint_remarks = (dr_datarow["complaint_remarks"].ToString()),
                            raisedfor_employee = (dr_datarow["raisedfor_employee"].ToString()),
                            category_name = (dr_datarow["category_name"].ToString()),
                            approvalstatus = (dr_datarow["status"].ToString()),
                            serviceapproval_gid = (dr_datarow["serviceapproval_gid"].ToString())
                        });
                        objODBCDatareader.Close();
                    }

                }
                objdepartmentapproval.departmentapproval_list = department_approval;
                dt_datatable.Dispose();
               
                msSQL = " select serviceapproval_gid,concat(e.user_code,'/',e.user_firstname,' ',e.user_lastname) as empname,e.user_code,date_format(f.complaint_date,'%d-%m-%Y %h:%i %p') as complaint_date, " +
                   " f.complaint_gid,f.complaint_refno, f.complaint_title, a.dept_manager, a.service_manager, a.status, " +
                   " concat(j.category_name,'/',i.subcategory_name,'/',k.type_name) as category_name,f.category_gid,f.complaint_remarks, " +
                   " concat(e.user_firstname,'',e.user_lastname,'/',f.department_name) as raisedfor_employee " +
                   " from its_trn_tserviceapproval a " +
                   " left join its_trn_tcomplaint f on f.complaint_gid=a.complaint_gid " +
                   " left join hrm_mst_temployee d on d.employee_gid = f.raisedfor_employee " +
                   " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                   " left join its_mst_tcategory j on j.category_gid =f.category_gid " +
                   " left join its_mst_tsubcategory i on i.subcategory_gid=f.subcategory_gid " +
                   " left join its_mst_ttype k on k.type_gid = f.type_gid " +
                   " where a.service_manager = '" + employee_gid + "' and a.status='Service Department Approval Pending' and (a.ticket_approval='Y' or a.ticket_approval='C') " +
                   " ORDER BY FIELD(a.status, 'Service Department Approval Pending'),a.complaint_gid desc;";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var service_approval = new List<serviceapproval_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsapproval_status = (dr_datarow["status"].ToString());
                        lscomplaintgid = (dr_datarow["complaint_gid"].ToString());
                        lscategorygid = (dr_datarow["category_gid"].ToString());
                        msSQL = " select status from its_trn_tserviceapproval a " +
                              " where a.service_manager = '" + employee_gid + "' and a.status='Service Department Approval Pending' and a.complaint_gid = '" + lscomplaintgid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            objODBCDatareader.Close();
                            msSQL = " select internal_approval from its_mst_tcategory " +
                                     " where category_gid='" + lscategorygid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsinternalapproval = objODBCDatareader["internal_approval"].ToString();
                            }
                            if (lsinternalapproval == "Y" && lsapproval_status == "Service Department Approval Pending")
                            {
                                lsinternalapproval = "Y";
                            }
                            objODBCDatareader.Close();
                        }
                        if (lsinternalapproval == "N" && lsapproval_status == "Service Department Approval Pending")
                        {
                            lsinternalapproval = "S";
                        }
                        if (lsinternalapproval == null && lsapproval_status == "Service Department Approval Pending")
                        {
                            lsinternalapproval = "S";
                        }
                        service_approval.Add(new serviceapproval_list
                        {
                            lsinternalapproval = lsinternalapproval,
                            complaint_gid = (dr_datarow["complaint_gid"].ToString()),
                            complaint_refno = (dr_datarow["complaint_refno"].ToString()),
                            complaint_date = (dr_datarow["complaint_date"].ToString()),
                            complaint_title = (dr_datarow["complaint_title"].ToString()),
                            complaint_remarks = (dr_datarow["complaint_remarks"].ToString()),
                            raisedfor_employee = (dr_datarow["raisedfor_employee"].ToString()),
                            category_name = (dr_datarow["category_name"].ToString()),
                            approvalstatus = (dr_datarow["status"].ToString()),
                            category_gid = (dr_datarow["category_gid"].ToString()),
                            serviceapproval_gid = (dr_datarow["serviceapproval_gid"].ToString())
                        });
                        objODBCDatareader.Close();
                    }
                }
                objdepartmentapproval.serviceapproval_list = service_approval;

                dt_datatable.Dispose();
               
                msSQL = " select serviceapproval_gid,concat(e.user_code,'/',e.user_firstname,' ',e.user_lastname) as empname,e.user_code,date_format(f.complaint_date,'%d-%m-%Y %h:%i %p') as complaint_date, " +
                  " f.complaint_gid,f.complaint_refno, f.complaint_title, a.dept_manager, a.service_manager, a.status,f.complaint_remarks, " +
                  " concat(j.category_name,'/',i.subcategory_name,'/',k.type_name) as category_name,f.category_gid, " +
                  " concat(e.user_firstname,'',e.user_lastname,'/',f.department_name) as raisedfor_employee " +
                  " from its_trn_tserviceapproval a " +
                  " left join its_trn_tcomplaint f on f.complaint_gid=a.complaint_gid " +
                  " left join hrm_mst_temployee d on d.employee_gid = f.raisedfor_employee " +
                  " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                  " left join its_mst_tcategory j on j.category_gid =f.category_gid " +
                  " left join its_mst_tsubcategory i on i.subcategory_gid=f.subcategory_gid " +
                  " left join its_mst_ttype k on k.type_gid = f.type_gid " +
                  " where a.manager_gid = '" + employee_gid + "' and a.status='Management Approval Pending' and (a.ticket_approval='Y' or a.ticket_approval='C')" +
                  " ORDER BY FIELD(a.status, 'Management Approval Pending'),a.complaint_gid desc;";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var manager_approval = new List<managerapproval_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        manager_approval.Add(new managerapproval_list
                        {
                            complaint_gid = (dr_datarow["complaint_gid"].ToString()),
                            complaint_refno = (dr_datarow["complaint_refno"].ToString()),
                            complaint_date = (dr_datarow["complaint_date"].ToString()),
                            complaint_title = (dr_datarow["complaint_title"].ToString()),
                            complaint_remarks = (dr_datarow["complaint_remarks"].ToString()),
                            raisedfor_employee = (dr_datarow["raisedfor_employee"].ToString()),
                            category_name = (dr_datarow["category_name"].ToString()),
                            approvalstatus = (dr_datarow["status"].ToString()),
                            serviceapproval_gid = (dr_datarow["serviceapproval_gid"].ToString())
                        });

                    }
                }
                objdepartmentapproval.managerapproval_list = manager_approval;
                dt_datatable.Dispose();
               

                msSQL = " select serviceapproval_gid,concat(e.user_code,'/',e.user_firstname,' ',e.user_lastname) as empname, " +
                       " e.user_code,date_format(f.complaint_date,'%d-%m-%Y %h:%i %p') as complaint_date, " +
                       " f.complaint_gid,f.complaint_refno, f.complaint_title, a.dept_manager, a.service_manager, a.status," +
                       " f.complaint_remarks, concat(j.category_name, '/', i.subcategory_name, '/', k.type_name) as category_name,f.category_gid," +
                       " concat(e.user_firstname,'',e.user_lastname,'/',f.department_name) as raisedfor_employee " +
                       " from its_trn_tserviceapproval a " +
                       " left join its_trn_tcomplaint f on f.complaint_gid = a.complaint_gid " +
                       " left join hrm_mst_temployee d on d.employee_gid = f.raisedfor_employee " +
                       " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                       " left join its_mst_tcategory j on j.category_gid = f.category_gid " +
                       " left join its_mst_tsubcategory i on i.subcategory_gid = f.subcategory_gid " +
                       " left join its_mst_ttype k on k.type_gid = f.type_gid " +
                       " where (a.dept_manager = '" + employee_gid + "' or a.service_manager = '" + employee_gid + "' or a.manager_gid = '" + employee_gid + "') " +
                       " and(a.status = 'Department Approval Rejected' or a.status = 'Service Department Rejected' or a.status = 'Management Approval Rejected' " +
                       " or a.status = 'Approval Done Internally' or a.status = 'Approval Done') " +
                       " ORDER BY FIELD(a.status, 'Approval Done', 'Approval Done Internally', 'Department Approval Rejected', 'Service Department Rejected', " +
                       "'Management Approval Rejected'),a.complaint_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var history_approval = new List<approvalhistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        history_approval.Add(new approvalhistory_list
                        {
                            complaint_gid = (dr_datarow["complaint_gid"].ToString()),
                            complaint_refno = (dr_datarow["complaint_refno"].ToString()),
                            complaint_date = (dr_datarow["complaint_date"].ToString()),
                            complaint_title = (dr_datarow["complaint_title"].ToString()),
                            complaint_remarks = (dr_datarow["complaint_remarks"].ToString()),
                            raisedfor_employee = (dr_datarow["raisedfor_employee"].ToString()),
                            category_name = (dr_datarow["category_name"].ToString()),
                            approvalstatus = (dr_datarow["status"].ToString()),
                            serviceapproval_gid = (dr_datarow["serviceapproval_gid"].ToString())
                        });

                    }
                }
                objdepartmentapproval.approvalhistory_list = history_approval;
                dt_datatable.Dispose();
               

                // Dependency Approval List ...//

                msSQL = " select b.approval_flag,b.ref_no,concat(c.application_code, '/', c.application_name) as application,b.done_by,c.vendor_name, " +
                       " b.release_status,a.release_gid,date_format(b.release_date, '%d-%m-%Y') as release_date,b.release_remarks,a.dependency_status, " +
                       " date_format(b.created_date,'%d-%m-%Y') as created_date,b.created_by from its_trn_tdependencyapproval a " +
                       " left join its_trn_trelease b on a.release_gid = b.release_gid " +
                       " left join  its_mst_tapplicationmaster c on b.application_gid = c.applicationmaster_gid " +
                       " where a.approval_gid = '" + employee_gid + "' and b.dependency_approval='Required' and a.dependency_status in ('Approval Pending...') " +
                       " and b.approval_flag = 'Y' group by a.release_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var dependencyapproval = new List<dependencyapproval_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        dependencyapproval.Add(new dependencyapproval_list
                        {
                            ref_no = dr_datarow["ref_no"].ToString(),
                            application = dr_datarow["application"].ToString(),
                            done_by = dr_datarow["done_by"].ToString(),
                            vendor_name = dr_datarow["vendor_name"].ToString(),
                            release_status = dr_datarow["release_status"].ToString(),
                            release_gid = dr_datarow["release_gid"].ToString(),
                            release_date = dr_datarow["release_date"].ToString(),
                            release_remarks = dr_datarow["release_remarks"].ToString(),
                            approval_status = dr_datarow["dependency_status"].ToString(),
                            created_by = dr_datarow["created_by"].ToString(),
                            created_date = dr_datarow["created_date"].ToString()
                        });
                    }
                    objdepartmentapproval.dependencyapproval_list = dependencyapproval;
                    dt_datatable.Dispose();
                }
               

                // Dependency History Approval List ....//

                msSQL = " select b.approval_flag,b.ref_no,concat(c.application_code, '/', c.application_name) as application,b.done_by,c.vendor_name, " +
                       " b.release_status,a.release_gid,date_format(b.release_date, '%d-%m-%Y') as release_date,b.release_remarks,a.dependency_status, " +
                       " date_format(b.created_date, '%d-%m-%Y') as created_date,b.created_by from its_trn_tdependencyapproval a " +
                       " left join its_trn_trelease b on a.release_gid = b.release_gid " +
                       " left join  its_mst_tapplicationmaster c on b.application_gid = c.applicationmaster_gid " +
                       " where a.approval_gid = '" + employee_gid + "' and b.dependency_approval = 'Required' and a.dependency_status in ('Approved') " +
                       " group by a.release_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var dependencyhistory = new List<dependencyhistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        dependencyhistory.Add(new dependencyhistory_list
                        {
                            ref_no = dr_datarow["ref_no"].ToString(),
                            application = dr_datarow["application"].ToString(),
                            done_by = dr_datarow["done_by"].ToString(),
                            vendor_name = dr_datarow["vendor_name"].ToString(),
                            release_status = dr_datarow["release_status"].ToString(),
                            release_gid = dr_datarow["release_gid"].ToString(),
                            release_date = dr_datarow["release_date"].ToString(),
                            release_remarks = dr_datarow["release_remarks"].ToString(),
                            approval_status = dr_datarow["dependency_status"].ToString(),
                            created_by = dr_datarow["created_by"].ToString(),
                            created_date = dr_datarow["created_date"].ToString()
                        });
                        objdepartmentapproval.dependencyhistory_list = dependencyhistory;
                        dt_datatable.Dispose();
                    }
                }
               

                // CAC History Approval List ....//

                msSQL = " select b.ref_no,concat(c.application_code, '/', c.application_name) as application,b.done_by,c.vendor_name, " +
                       " b.release_status,a.release_gid,date_format(b.release_date, '%d-%m-%Y') as release_date,b.release_remarks,a.approval_status, " +
                       " date_format(b.created_date, '%d-%m-%Y') as created_date,b.created_by from its_trn_tcacapproval a " +
                       " left join its_trn_trelease b on a.release_gid = b.release_gid " +
                       " left join  its_mst_tapplicationmaster c on b.application_gid = c.applicationmaster_gid " +
                       " where a.approval_member = '" + employee_gid + "' and a.approval_status in ('Approved','Rejected') " +
                       " and b.approval_flag = 'Y' and b.cap_approval = 'Required' group by a.release_gid ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var cachistory = new List<cachistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        cachistory.Add(new cachistory_list
                        {
                            ref_no = dr_datarow["ref_no"].ToString(),
                            application = dr_datarow["application"].ToString(),
                            done_by = dr_datarow["done_by"].ToString(),
                            vendor_name = dr_datarow["vendor_name"].ToString(),
                            release_status = dr_datarow["release_status"].ToString(),
                            release_gid = dr_datarow["release_gid"].ToString(),
                            release_date = dr_datarow["release_date"].ToString(),
                            release_remarks = dr_datarow["release_remarks"].ToString(),
                            approval_status = dr_datarow["approval_status"].ToString(),
                            created_by = dr_datarow["created_by"].ToString(),
                            created_date = dr_datarow["created_date"].ToString()
                        });
                        objdepartmentapproval.cachistory_list = cachistory;
                        dt_datatable.Dispose();
                    }
                }
               

                // CAC Approval List ...//

                msSQL = " select b.ref_no,concat(c.application_code, '/', c.application_name) as application,b.done_by,c.vendor_name," +
                       " b.release_status,a.release_gid,date_format(b.release_date, '%d-%m-%Y') as release_date,b.release_remarks,a.approval_status, " +
                       " date_format(b.created_date, '%d-%m-%Y') as created_date,b.created_by, a.approval_remarks from its_trn_tcacapproval a " +
                       " left join its_trn_trelease b on a.release_gid = b.release_gid " +
                       " left join  its_mst_tapplicationmaster c on b.application_gid = c.applicationmaster_gid " +
                       " where a.approval_member = '" + employee_gid + "' and a.approval_status in ('Approval Pending...') " +
                       " and b.approval_flag = 'Y' and b.cap_approval='Required' group by a.release_gid";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var cabapproval = new List<cabapproval_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        cabapproval.Add(new cabapproval_list
                        {
                            ref_no = dr_datarow["ref_no"].ToString(),
                            application = dr_datarow["application"].ToString(),
                            done_by = dr_datarow["done_by"].ToString(),
                            vendor_name = dr_datarow["vendor_name"].ToString(),
                            release_status = dr_datarow["release_status"].ToString(),
                            release_gid = dr_datarow["release_gid"].ToString(),
                            release_date = dr_datarow["release_date"].ToString(),
                            release_remarks = dr_datarow["release_remarks"].ToString(),
                            approval_remarks = dr_datarow["approval_remarks"].ToString(),
                            approval_status = dr_datarow["approval_status"].ToString(),
                            created_by = dr_datarow["created_by"].ToString(),
                            created_date = dr_datarow["created_date"].ToString()
                        });
                    }
                    objdepartmentapproval.cabapproval_list = cabapproval;
                    dt_datatable.Dispose();
                }
                msSQL = " SELECT a.service_count,b.department_count,c.management_count,sum( a.service_count + b.department_count + c.management_count) as overall_serviceapproval, " +
                       " d.dependency_count,e.cabapproval_count,sum(d.dependency_count + e.cabapproval_count) as overall_changemanagement,f.taskapproval_count from " +
                       " (SELECT COUNT(status) AS service_count FROM its_trn_tserviceapproval WHERE status = 'Service Department Approval Pending' and service_manager = '" + employee_gid + "' and ticket_approval='Y') AS a, " +
                       " (SELECT COUNT(status) AS department_count FROM its_trn_tserviceapproval WHERE status = 'Department Approval Pending' and ticket_approval = 'Y' and dept_manager = '" + employee_gid + "') AS b," +
                       " (SELECT COUNT(status) AS management_count FROM its_trn_tserviceapproval WHERE status = 'Management Approval Pending' and ticket_approval = 'Y' and manager_gid = '" + employee_gid + "') AS c," +
                       " (select COUNT(dependency_status) As dependency_count from its_trn_tdependencyapproval where dependency_status = 'Approval Pending...' and approval_gid = '" + employee_gid + "') As d," +
                       " (select count(approval_status) as cabapproval_count from its_trn_tcacapproval where approval_status = 'Approval Pending...' and approval_member = '" + employee_gid + "') As e," +
                       " (select count(activity) as taskapproval_count from ocs_trn_ttask2activity where activity = 'Department Head Approval' and assigned_gid = '" + user_gid + "' and status=('Pending')) As f " +
                       " group by taskapproval_count";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    var approval_count = new List<myapprovalcount>();
                    approval_count.Add(new myapprovalcount
                    {
                        serviceapprovalcount = objODBCDatareader["service_count"].ToString(),
                        departmentapprovalcount = objODBCDatareader["department_count"].ToString(),
                        managementapprovalcount = objODBCDatareader["management_count"].ToString(),
                        overallservice_approval = objODBCDatareader["overall_serviceapproval"].ToString(),
                        dependencyapprovalcount = objODBCDatareader["dependency_count"].ToString(),
                        cabapprovalcount = objODBCDatareader["cabapproval_count"].ToString(),
                        overallchangemana_approval = objODBCDatareader["overall_changemanagement"].ToString(),
                        task_approval = objODBCDatareader["taskapproval_count"].ToString()
                    });                   
                    objdepartmentapproval.myapprovalcount = approval_count;
                }
                objODBCDatareader.Close();
                return true;
            }
            catch
            {
                return false;
            }
            
        }


        //   Department Approved..............//

        public bool DaPostDepartmentApprove(departmentapproved values)
        {
           try
            {
                msSQL = " select approval_gid,manager_gid,manager_name from its_mst_tcategory " +
                   " where category_gid='" + values.category_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsmanager_gid = objODBCDatareader["manager_gid"].ToString();
                    lsmanagername = objODBCDatareader["manager_name"].ToString();
                }
                objODBCDatareader.Close();
                if (lsmanagername == "No Manager Approval")
                {
                    msSQL = " update its_trn_tserviceapproval set " +
                            " department_approval = 'C', ";
                      if (values.remarks == null || values.remarks == "")
                    {
                        msSQL += " department_remarks = '' ,";
                    }
                    else
                    {
                        msSQL += " department_remarks ='" + values.remarks.Replace("'", " ") + "', ";
                    }

                    msSQL+=" status = 'Service Department Approval Pending' " +
                            " where serviceapproval_gid = '" + values.serviceapproval_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " update its_trn_tserviceapproval set " +
                           " department_approval = 'M', ";
                    if(values.remarks == null || values.remarks == "")
                    {
                        msSQL += " department_remarks = '' ,";
                    }
                    else
                    {
                        msSQL += " department_remarks ='" + values.remarks.Replace("'", " ") + "', ";
                    }

                    msSQL +=" status = 'Management Approval Pending' " +
                           " where serviceapproval_gid = '" + values.serviceapproval_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = "select complaint_gid from its_trn_tserviceapproval where serviceapproval_gid='" + values.serviceapproval_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscomplaintgid = objODBCDatareader["complaint_gid"].ToString();

                }
                objODBCDatareader.Close();
                msgetgid = objcmnfunctions.GetMasterGID("CPAL");
                msSQL = " insert into its_trn_tcomplaintapprovallog (" +
                            " complaintlog_gid, " +
                            " complaint_gid, " +
                            " serviceapproval_gid, " +
                            " departmentapproved_date , " +
                            " departmentapproval_status )" +
                            " values ( " +
                            "'" + msgetgid + "'," +
                            "'" + lscomplaintgid + "'," +
                            "'" + values.serviceapproval_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Department Approval Done')";
                objODBCDatareader.Close();
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                
                ///  Mail.....//
                popmail(lscomplaintgid);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Ticket Approved Successfully !";
                    lsinternalapproval = "C";
                    return true;
                   
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured !";
                    return false;
                }
            }
            catch
            {
                values.status = false;
                values.message = "Error Occured !";
                return false;
                
            }
            

        }

        //   Department Reject..............//

        public bool DaPostDepartmentReject(departmentreject values)
        {
            try
            {
                msSQL = " update its_trn_tserviceapproval set " +
                    " department_approval = 'R', " +
                    " getapproval_flag='R'," +
                    " status = 'Department Approval Rejected', ";
                if (values.remarks == null || values.remarks == "")
                {
                    msSQL += " department_remarks = '' ";
                }
                else
                {
                    msSQL += " department_remarks ='"+ values.remarks.Replace("'"," ") + "' ";
                }
                     msSQL+= " where serviceapproval_gid = '" + values.serviceapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select complaint_gid from its_trn_tserviceapproval where serviceapproval_gid = '" + values.serviceapproval_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscomplaintgid = objODBCDatareader["complaint_gid"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " update its_trn_tcomplaint Set " +
                       " assign_status = 'Rejected' " +
                       " where complaint_gid = '" + lscomplaintgid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msgetgid = objcmnfunctions.GetMasterGID("CPAL");
                msSQL = " insert into its_trn_tcomplaintapprovallog (" +
                        " complaintlog_gid, " +
                        " complaint_gid, " +
                        " serviceapproval_gid, " +
                        " departmentrejected_date , " +
                        " departmentrejected_remarks," +
                        " departmentapproval_status )" +
                        " values ( " +
                        "'" + msgetgid + "'," +
                        "'" + lscomplaintgid + "'," +
                        "'" + values.serviceapproval_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                if (values.remarks == null || values.remarks == "")
                {
                    msSQL += " '' ,";
                }
                else
                {
                    msSQL += " '" + values.remarks.Replace("'", " ") + "', ";
                }
                msSQL+="'Department Approval Rejected')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                // Mail....//
                popreject(lscomplaintgid, values.remarks);

               
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Ticket Rejected !";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured !";
                    return false;
                }
            }
            catch
            {
                values.status = false;
                values.message = "Error Occured !";
                return false;
            }
        }

        //   Department Internal Approval..............//

        public bool DaPostDepartmentInternal(departmentinternal values)
        {
            try
            {
                msSQL = " select service_manager from its_trn_tserviceapproval a" +
                   " where a.complaint_gid = '" + values.complaint_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsservicemanager = objODBCDatareader["service_manager"].ToString();
                }
                objODBCDatareader.Close();
                if (lsservicemanager == "")
                {
                    msSQL = " update its_trn_tcomplaint2campaign Set " +
                              " ticket_approval = 'C' " +
                              " where complaint_gid = '" + values.complaint_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update its_trn_tserviceapproval set " +
                         " department_approval = 'C', " +
                         " internal_approval = 'C'," +
                         " getapproval_flag='N'," +
                         " status = 'Approval Done Internally' " +
                         " where serviceapproval_gid = '" + values.serviceapproval_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " update its_trn_tserviceapproval set " +
                         " department_approval = 'C', " +
                         " getapproval_flag='N'," +
                         " status = 'Service Department Approval Pending' " +
                         " where serviceapproval_gid = '" + values.serviceapproval_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msgetgid = objcmnfunctions.GetMasterGID("CPAL");
                msSQL = " insert into its_trn_tcomplaintapprovallog (" +
                        " complaintlog_gid, " +
                        " complaint_gid, " +
                        " serviceapproval_gid, " +
                        " departmentapproved_date , " +
                        "   )" +
                        " values ( " +
                        "'" + msgetgid + "'," +
                        "'" + values.complaint_gid + "'," +
                        "'" + values.serviceapproval_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'Department Approval Done Internally')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                ///  Mail.....//
                popmail(values.complaint_gid);
                values.status = true;
                values.message = "Internal Approved Successfully !";
                return true;
            }
            catch
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }


        //   Service Approve..............//

        public bool DaPostServiceApprove(serviceapprove values, string employee_gid)
        {
            try
            {
                msSQL = " update its_trn_tserviceapproval set" +
                   " department_approval = 'C'," +
                   " ticket_approval = 'C', ";
                if (values.remarks == null || values.remarks == "")
                {
                    msSQL += " service_remarks = '' ,";
                }
                else
                {
                    msSQL += " service_remarks ='" + values.remarks.Replace("'", " ") + "', ";
                }

                msSQL += " status = 'Approval Done'" +
                   " where serviceapproval_gid = '" + values.serviceapproval_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = " select employee_gid,b.campaign_gid from its_trn_tcomplaint2employee a" +
                            " left join its_trn_tcomplaint2campaign b on b.campaign_gid = a.campaign_gid" +
                            " left join its_mst_tcategory c on c.campaign_gid=b.campaign_gid " +
                            " where a.employee_gid  in (select assign_to from its_trn_tcomplaint2campaign)" +
                            " and  c.category_gid='" + values.category_gid + "' group by a.campaign2employee_gid order by a.campaign2employee_gid  asc ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsteam_gid = objODBCDatareader["campaign_gid"].ToString();
                    objODBCDatareader.Close();

                    msSQL = " select count(employee_gid) from its_trn_tcomplaint2employee " +
                           " where campaign_gid = '" + lsteam_gid + "' ";
                    lsnoofassigned_employee = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = " select count(distinct assign_to) from its_trn_tcomplaint2campaign " +
                           " where category_gid = '" + values.category_gid + "' and assign_to <> '' and leadstage_gid in ('1','2','Pending...')";
                    lsinprogress_count = objdbconn.GetExecuteScalar(msSQL);

                    int lsstatus = Convert.ToInt16(lsnoofassigned_employee) - Convert.ToInt16(lsinprogress_count);

                    if (Convert.ToString(lsstatus) == "0")
                    {
                        msSQL = " select a.employee_gid,b.campaign_gid from its_trn_tcomplaint2employee a" +
                          " left join its_trn_tcomplaint2campaign b on b.campaign_gid = a.campaign_gid" +
                          " left join its_mst_tcategory c on c.campaign_gid=b.campaign_gid " +
                          " where a.employee_gid not in (select assign_to from its_trn_tcomplaint2campaign where category_gid='" + values.category_gid + "')" +
                          " and  c.category_gid='" + values.category_gid + "' group by a.campaign2employee_gid order by a.campaign2employee_gid asc ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                            lscampaign_gid = objODBCDatareader["campaign_gid"].ToString();

                        }

                        else
                        {
                            objODBCDatareader.Close();
                            msSQL = " select a.employee_gid,count(b.assign_to) as min_count,b.campaign_gid from its_trn_tcomplaint2employee a" +
                                    " left join its_trn_tcomplaint2campaign b on b.assign_to = a.employee_gid and a.campaign_gid=b.campaign_gid " +
                                    " left join its_mst_tcategory c on c.campaign_gid = b.campaign_gid " +
                                    " where c.category_gid='" + values.category_gid + "' and leadstage_gid in ('1','2','pending...')" +
                                    " group by a.employee_gid" +
                                    " order by count(b.assign_to) asc,a.campaign2employee_gid asc";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                                lscampaign_gid = objODBCDatareader["campaign_gid"].ToString();

                            }
                            objODBCDatareader.Close();
                        }
                        objODBCDatareader.Close();
                    }
                    else
                    {

                        msSQL = " select distinct(employee_gid),campaign_gid from its_trn_tcomplaint2employee a " +
                                " where a.employee_gid not in (select  distinct assign_to from its_trn_tcomplaint2campaign " +
                                " where category_gid = '" + values.category_gid + "' and leadstage_gid in ('1','2','Pending...') " +
                                " and assign_to <> '') and a.campaign_gid = (select campaign_gid from its_mst_tcategory where category_gid='" + values.category_gid + "')";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                            lscampaign_gid = objODBCDatareader["campaign_gid"].ToString();

                        }
                        objODBCDatareader.Close();
                    }

                }
                else
                {
                    objODBCDatareader.Close();
                    msSQL = " select a.employee_gid,b.campaign_gid from its_trn_tcomplaint2employee a" +
                            " left join its_mst_tcategory b on b.campaign_gid=a.campaign_gid " +
                            " where a.employee_gid not in (select assign_to from its_trn_tcomplaint2campaign)" +
                            " and  b.category_gid='" + values.category_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                        lscampaign_gid = objODBCDatareader["campaign_gid"].ToString();
                    }
                    objODBCDatareader.Close();
                }


                msgetlead2campaign_gid = objcmnfunctions.GetMasterGID("BLCC");
                msSQL = " Insert into its_trn_tcomplaint2campaign ( " +
                        " complaint2campaign_gid, " +
                        " complaint_gid, " +
                        " campaign_gid, " +
                        " category_gid," +
                        " created_by, " +
                        " created_date, " +
                        " lead_status, " +
                        " leadstage_gid, " +
                        " assign_to ) " +
                        " Values ( " +
                        "'" + msgetlead2campaign_gid + "'," +
                        "'" + values.complaint_gid + "'," +
                        "'" + lscampaign_gid + "'," +
                        "'" + values.category_gid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'Open'," +
                         "'1'," +
                        "'" + lsemployee_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update its_trn_tcomplaint2campaign Set " +
                         " ticket_approval = 'C' " +
                         " where complaint_gid = '" + values.complaint_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update its_trn_tcomplaintapprovallog set " +
                            " serviceapproved_date =' " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                            " serviceapproval_status ='Service Department Approval Done' " +
                            " where serviceapproval_gid ='" + values.serviceapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update its_trn_tcomplaint set " +
                        " assign_status = 'Assigned' " +
                        " where complaint_gid = '" + values.complaint_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

               
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Ticket Approved Successfully !";
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

        //   Service Reject..............//

        public bool DaPostServiceReject(servicereject values)
        {
            try
            {
                msSQL = " update its_trn_tserviceapproval set " +
                    " department_approval = 'R', " +
                    " getapproval_flag='R'," +
                    " status = 'Service Department Rejected', ";
                if (values.remarks == null || values.remarks == "")
                {
                    msSQL += " service_remarks = '' ";
                }
                else
                {
                    msSQL += " service_remarks ='" + values.remarks.Replace("'", " ") + "' ";
                }
                msSQL +=" where serviceapproval_gid = '" + values.serviceapproval_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " select complaint_gid from its_trn_tserviceapproval where serviceapproval_gid = '" + values.serviceapproval_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscomplaint_gid = objODBCDatareader["complaint_gid"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = " update its_trn_tcomplaint2campaign Set " +
                          " ticket_approval = 'C' " +
                          " where complaint_gid = '" + lscomplaint_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update its_trn_tcomplaintapprovallog set " +
                         " servicerejected_date =' " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                if (values.remarks == null || values.remarks == "")
                {
                    msSQL += " servicerejected_remarks = '', ";
                }
                else
                {
                    msSQL += " servicerejected_remarks ='" + values.remarks.Replace("'", " ") + "', ";
                }
                msSQL +=  " serviceapproval_status ='Service Department Approval Rejected' " +
                         " where serviceapproval_gid ='" + values.serviceapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update its_trn_tcomplaint Set " +
                        " assign_status = 'Rejected' " +
                        " where complaint_gid = '" + lscomplaint_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                // Mail......//

                popreject(lscomplaint_gid,values.remarks);


                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Ticket Rejected !";
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

        //   Service Internal Approval..............//

        public bool DaPostServiceInternal(serviceinternal values)
        {
            try
            {
                msSQL = " update its_trn_tserviceapproval set " +
                   " department_approval = 'C', " +
                   " internal_approval = 'C'," +
                   " ticket_approval = 'C', " +
                   " getapproval_flag='N'," +
                   " status = 'Approval Done Internally' " +
                   " where serviceapproval_gid = '" + values.serviceapproval_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " select complaint_gid from its_trn_tserviceapproval where serviceapproval_gid = '" + values.serviceapproval_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscomplaint_gid = objODBCDatareader["complaint_gid"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = "select * from its_trn_tcomplaintapprovallog where complaint_gid = '" + lscomplaint_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    msSQL = " update its_trn_tcomplaintapprovallog set " +
                        " serviceapproved_date =' " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                        " serviceapproval_status ='Service Department Approval Done Internally' " +
                        " where serviceapproval_gid ='" + values.serviceapproval_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    objODBCDatareader.Close();
                    msgetgid = objcmnfunctions.GetMasterGID("CPAL");
                    msSQL = " insert into its_trn_tcomplaintapprovallog (" +
                            " complaintlog_gid, " +
                            " complaint_gid, " +
                            " serviceapproval_gid, " +
                            " serviceapproved_date , " +
                            " serviceapproval_status )" +
                            " values ( " +
                            "'" + msgetgid + "'," +
                            "'" + lscomplaint_gid + "'," +
                            "'" + values.serviceapproval_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Service Department Approval Done Internally')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                objODBCDatareader.Close();

                msSQL = " update its_trn_tcomplaint2campaign Set " +
                        " ticket_approval = 'C' " +
                        " where complaint_gid = '" + lscomplaint_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Internal Approved Successfully !";
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
                return false;
            }
        }


        // Management Approve .............//

        public bool DaPostManageApprove(managementapprove values)
        {
            try
            {
                msSQL = " update its_trn_tcomplaintapprovallog set " +
               " managementapproved_date =' " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
               " managementapproval_status ='Management Approval Done' " +
               " where serviceapproval_gid ='" + values.serviceapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update its_trn_tserviceapproval set " +
                        " department_approval = 'C', ";
                if (values.remarks == null || values.remarks == "")
                {
                    msSQL += " management_remarks = '' ,";
                }
                else
                {
                    msSQL += " management_remarks ='" + values.remarks.Replace("'", " ") + "', ";
                }

                msSQL += " status = 'Service Department Approval Pending' " +
                        " where serviceapproval_gid = '" + values.serviceapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select complaint_gid from its_trn_tserviceapproval where serviceapproval_gid = '" + values.serviceapproval_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscomplaintgid = objODBCDatareader["complaint_gid"].ToString();
                }
                objODBCDatareader.Close();

                ///  Mail.....//
                popmail(lscomplaintgid);


                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Ticket Approved Successfully !";
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

        //  Management Reject ..............//

        public bool DaPostManageReject(managementreject values)
        {
            try
            {
                msSQL = " update its_trn_tserviceapproval set " +
                   " department_approval = 'R', " +
                   " status = 'Management Approval Rejected', ";
                if (values.remarks == null || values.remarks == "")
                {
                    msSQL += " management_remarks = '' ";
                }
                else
                {
                    msSQL += " management_remarks ='" + values.remarks.Replace("'", " ") + "' ";
                }
                msSQL += " where serviceapproval_gid = '" + values.serviceapproval_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select complaint_gid from its_trn_tserviceapproval where serviceapproval_gid = '" + values.serviceapproval_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscomplaintgid = objODBCDatareader["complaint_gid"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " update its_trn_tcomplaintapprovallog set " +
                        " managementrejected_date =' " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                if (values.remarks == null || values.remarks == "")
                {
                    msSQL += " managementrejected_remarks = '', ";
                }
                else
                {
                    msSQL += " managementrejected_remarks ='" + values.remarks.Replace("'", " ") + "', ";
                }
                msSQL += " managementapproval_status ='Management Approval Rejected' " +
                        " where serviceapproval_gid ='" + values.serviceapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update its_trn_tcomplaint Set " +
                           " assign_status = 'Rejected' " +
                           " where complaint_gid = '" + lscomplaintgid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                // Mail....//
                popreject(lscomplaintgid, values.remarks);


                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Ticket Rejected !";
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

        // View Department Details....//
        public bool DaGetViewDepartment(string serviceapproval_gid, viewdepartment objviewdepartment)
        {
            try
            {
                msSQL = " select serviceapproval_gid,e.user_code,date_format(f.complaint_date,'%d-%m-%Y  %h:%i %p') as complaint_date,f.complaint_remarks, " +
                          " f.complaint_gid,f.complaint_refno, f.complaint_title, a.dept_manager, a.service_manager, a.status, " +
                          "  concat(e.user_firstname,'',e.user_lastname,'/',f.department_name) as raisedfor_employee, " +
                          " concat(j.category_name,'/',i.subcategory_name,'/',k.type_name) as category_name,f.category_gid " +
                          " from its_trn_tserviceapproval a " +
                          " left join its_trn_tcomplaint f on f.complaint_gid=a.complaint_gid " +
                          " left join hrm_mst_temployee d on d.employee_gid = f.raisedfor_employee " +
                          " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                          " left join its_mst_tcategory j on j.category_gid =f.category_gid " +
                          " left join its_mst_tsubcategory i on i.subcategory_gid=f.subcategory_gid " +
                          " left join its_mst_ttype k on k.type_gid = f.type_gid " +
                          " where a.serviceapproval_gid = '" + serviceapproval_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    objviewdepartment.complaint_gid = (objODBCDatareader["complaint_gid"].ToString());
                    objviewdepartment.category_gid = (objODBCDatareader["category_gid"].ToString());
                    objviewdepartment.complaint_refno = (objODBCDatareader["complaint_refno"].ToString());
                    objviewdepartment.complaint_date = (objODBCDatareader["complaint_date"].ToString());
                    objviewdepartment.complaint_title = (objODBCDatareader["complaint_title"].ToString());
                    objviewdepartment.complaint_remarks = (objODBCDatareader["complaint_remarks"].ToString());
                    objviewdepartment.raisedfor_employee = (objODBCDatareader["raisedfor_employee"].ToString());
                    objviewdepartment.category_name = (objODBCDatareader["category_name"].ToString());
                    objviewdepartment.approvalstatus = (objODBCDatareader["status"].ToString());
                    objviewdepartment.serviceapproval_gid = (objODBCDatareader["serviceapproval_gid"].ToString());
                    objODBCDatareader.Close();

                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as user_firstname, " +
                            " case when d.departmentapproved_date is null then date_format(d.departmentrejected_date,'%d-%m-%Y  %h:%i %p') " +
                            " else date_format(d.departmentapproved_date,'%d-%m-%Y  %h:%i %p') END as 'DepartmentApproved/RejectedDate', " +
                            " d.departmentapproval_status, case when d.departmentrejected_remarks is not null then d.departmentrejected_remarks"+
                            " when a.department_remarks is not null then a.department_remarks end as departmentrejected_remarks from its_trn_tserviceapproval a " +
                            " left join hrm_mst_temployee b on b.employee_gid = a.dept_manager " +
                            " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                            " left join its_trn_tcomplaintapprovallog d on a.complaint_gid = d.complaint_gid " +
                            " where a.complaint_gid = '" + objviewdepartment.complaint_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objviewdepartment.department_approval = objODBCDatareader["user_firstname"].ToString();
                        objviewdepartment.department_approveddate = objODBCDatareader["DepartmentApproved/RejectedDate"].ToString();
                        objviewdepartment.department_approvedstatus = objODBCDatareader["departmentapproval_status"].ToString();
                        objviewdepartment.departmentrejected_remarks = objODBCDatareader["departmentrejected_remarks"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select a.manager_name,b.managementapproval_status,case when b.managementapproved_date is null then " +
                            " date_format(b.managementrejected_date,'%d-%m-%Y  %h:%i %p') else date_format(b.managementapproved_date,'%d-%m-%Y  %h:%i %p') " +
                            " END as 'ManagementApproved/RejectedDate',case when b.managementrejected_remarks is not null then b.managementrejected_remarks" +
                            " when a.management_remarks is not null then a.management_remarks end as managementrejected_remarks " +
                            " from its_trn_tserviceapproval a " +
                            " left join its_trn_tcomplaintapprovallog b on a.complaint_gid = b.complaint_gid " +
                            "  where a.complaint_gid='" + objviewdepartment.complaint_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        objviewdepartment.management_approval = objODBCDatareader["manager_name"].ToString();
                        objviewdepartment.management_approveddate = objODBCDatareader["ManagementApproved/RejectedDate"].ToString();
                        objviewdepartment.management_approvedstatus = objODBCDatareader["managementapproval_status"].ToString();
                        objviewdepartment.managementrejected_remarks = objODBCDatareader["managementrejected_remarks"].ToString();
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
                        " where a.complaint_gid='" + objviewdepartment.complaint_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        objviewdepartment.service_approval = objODBCDatareader["user_firstname"].ToString();
                        objviewdepartment.service_approveddate = objODBCDatareader["ServiceApproved/RejectedDate"].ToString();
                        objviewdepartment.service_approvedstatus = objODBCDatareader["serviceapproval_status"].ToString();
                        objviewdepartment.servicerejected_remarks = objODBCDatareader["servicerejected_remarks"].ToString();
                    }
                    objODBCDatareader.Close();
                }
                else
                {
                    objODBCDatareader.Close();
                }
                objviewdepartment.status = true;
                return true;
            }
            catch
            {
                objviewdepartment.status = false;
                return false;
            }
        }


        // View Service Details....//
        public bool DaGetViewService(string serviceapproval_gid, viewservice objviewservice)
        {
           try
            {
                msSQL = " select serviceapproval_gid,concat(e.user_code,'/',e.user_firstname,' ',e.user_lastname) as empname,e.user_code, " +
              " date_format(f.complaint_date,'%d-%m-%Y  %h:%i %p') as complaint_date, " +
             " f.complaint_gid,f.complaint_refno, f.complaint_title, a.dept_manager, a.service_manager, a.status, " +
             " concat(j.category_name,'/',i.subcategory_name,'/',k.type_name) as category_name,f.category_gid,f.complaint_remarks, " +
             " concat(e.user_firstname,'',e.user_lastname,'/',f.department_name) as raisedfor_employee " +
             " from its_trn_tserviceapproval a " +
             " left join its_trn_tcomplaint f on f.complaint_gid=a.complaint_gid " +
             " left join hrm_mst_temployee d on d.employee_gid = f.raisedfor_employee " +
             " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
             " left join its_mst_tcategory j on j.category_gid =f.category_gid " +
             " left join its_mst_tsubcategory i on i.subcategory_gid=f.subcategory_gid " +
             " left join its_mst_ttype k on k.type_gid = f.type_gid " +
              " where a.serviceapproval_gid = '" + serviceapproval_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    objviewservice.complaint_gid = (objODBCDatareader["complaint_gid"].ToString());
                    objviewservice.category_gid = (objODBCDatareader["category_gid"].ToString());
                    objviewservice.complaint_refno = (objODBCDatareader["complaint_refno"].ToString());
                    objviewservice.complaint_date = (objODBCDatareader["complaint_date"].ToString());
                    objviewservice.complaint_title = (objODBCDatareader["complaint_title"].ToString());
                    objviewservice.complaint_remarks = (objODBCDatareader["complaint_remarks"].ToString());
                    objviewservice.raisedfor_employee = (objODBCDatareader["raisedfor_employee"].ToString());
                    objviewservice.category_name = (objODBCDatareader["category_name"].ToString());
                    objviewservice.approvalstatus = (objODBCDatareader["status"].ToString());
                    objviewservice.serviceapproval_gid = (objODBCDatareader["serviceapproval_gid"].ToString());
                    objODBCDatareader.Close();

                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as user_firstname, " +
                           " case when d.departmentapproved_date is null then date_format(d.departmentrejected_date,'%d-%m-%Y  %h:%i %p') " +
                           " else date_format(d.departmentapproved_date,'%d-%m-%Y  %h:%i %p') END as 'DepartmentApproved/RejectedDate', " +
                           " d.departmentapproval_status, case when d.departmentrejected_remarks is not null then d.departmentrejected_remarks"+
                            " when a.department_remarks is not null then a.department_remarks end as departmentrejected_remarks from its_trn_tserviceapproval a " +
                           " left join hrm_mst_temployee b on b.employee_gid = a.dept_manager " +
                           " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                           " left join its_trn_tcomplaintapprovallog d on a.complaint_gid = d.complaint_gid " +
                           " where a.complaint_gid = '" + objviewservice.complaint_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objviewservice.department_approval = objODBCDatareader["user_firstname"].ToString();
                        objviewservice.department_approveddate = objODBCDatareader["DepartmentApproved/RejectedDate"].ToString();
                        objviewservice.department_approvedstatus = objODBCDatareader["departmentapproval_status"].ToString();
                        objviewservice.departmentrejected_remarks = objODBCDatareader["departmentrejected_remarks"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select a.manager_name,b.managementapproval_status,case when b.managementapproved_date is null then " +
                            " date_format(b.managementrejected_date,'%d-%m-%Y  %h:%i %p') else date_format(b.managementapproved_date,'%d-%m-%Y  %h:%i %p') " +
                            " END as 'ManagementApproved/RejectedDate',case when b.managementrejected_remarks is not null then b.managementrejected_remarks" +
                            " when a.management_remarks is not null then a.management_remarks end as managementrejected_remarks " +
                            " from its_trn_tserviceapproval a " +
                            " left join its_trn_tcomplaintapprovallog b on a.complaint_gid = b.complaint_gid " +
                            "  where a.complaint_gid='" + objviewservice.complaint_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        objviewservice.management_approval = objODBCDatareader["manager_name"].ToString();
                        objviewservice.management_approveddate = objODBCDatareader["ManagementApproved/RejectedDate"].ToString();
                        objviewservice.management_approvedstatus = objODBCDatareader["managementapproval_status"].ToString();
                        objviewservice.managementrejected_remarks = objODBCDatareader["managementrejected_remarks"].ToString();
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
                        " where a.complaint_gid='" + objviewservice.complaint_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        objviewservice.service_approval = objODBCDatareader["user_firstname"].ToString();
                        objviewservice.service_approveddate = objODBCDatareader["ServiceApproved/RejectedDate"].ToString();
                        objviewservice.service_approvedstatus = objODBCDatareader["serviceapproval_status"].ToString();
                        objviewservice.servicerejected_remarks = objODBCDatareader["servicerejected_remarks"].ToString();
                    }
                    objODBCDatareader.Close();
                }
                else
                {
                    objODBCDatareader.Close();
                }
                objviewservice.status = true;
                return true;
            }
            catch
            {
                objviewservice.status = false;
                return false;
            }
        }

        // View Management Details....//

        public bool DaGetViewManagement(string serviceapproval_gid, viewmanagement objviewmanagement)
        {
           try
            {
                msSQL = " select serviceapproval_gid,concat(e.user_code,'/',e.user_firstname,' ',e.user_lastname) as empname,e.user_code, " +
            " date_format(f.complaint_date,'%d-%m-%Y  %h:%i %p') as complaint_date, " +
            " f.complaint_gid,f.complaint_refno, f.complaint_title, a.dept_manager, a.service_manager, a.status,f.complaint_remarks, " +
            " concat(j.category_name,'/',i.subcategory_name,'/',k.type_name) as category_name,f.category_gid, " +
            " concat(e.user_firstname,'',e.user_lastname,'/',f.department_name) as raisedfor_employee " +
            " from its_trn_tserviceapproval a " +
            " left join its_trn_tcomplaint f on f.complaint_gid=a.complaint_gid " +
            " left join hrm_mst_temployee d on d.employee_gid = f.raisedfor_employee " +
            " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
            " left join its_mst_tcategory j on j.category_gid =f.category_gid " +
            " left join its_mst_tsubcategory i on i.subcategory_gid=f.subcategory_gid " +
            " left join its_mst_ttype k on k.type_gid = f.type_gid " +
            " where a.serviceapproval_gid = '" + serviceapproval_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    objviewmanagement.complaint_gid = (objODBCDatareader["complaint_gid"].ToString());
                    objviewmanagement.complaint_refno = (objODBCDatareader["complaint_refno"].ToString());
                    objviewmanagement.complaint_date = (objODBCDatareader["complaint_date"].ToString());
                    objviewmanagement.complaint_title = (objODBCDatareader["complaint_title"].ToString());
                    objviewmanagement.complaint_remarks = (objODBCDatareader["complaint_remarks"].ToString());
                    objviewmanagement.raisedfor_employee = (objODBCDatareader["raisedfor_employee"].ToString());
                    objviewmanagement.category_name = (objODBCDatareader["category_name"].ToString());
                    objviewmanagement.approvalstatus = (objODBCDatareader["status"].ToString());
                    objviewmanagement.serviceapproval_gid = (objODBCDatareader["serviceapproval_gid"].ToString());
                    objODBCDatareader.Close();
                    msSQL = " select concat(c.user_firstname,' ',c.user_lastname) as user_firstname, " +
                            " case when d.departmentapproved_date is null then date_format(d.departmentrejected_date,'%d-%m-%Y  %h:%i %p') " +
                            " else date_format(d.departmentapproved_date,'%d-%m-%Y  %h:%i %p') END as 'DepartmentApproved/RejectedDate', " +
                            " d.departmentapproval_status,case when d.departmentrejected_remarks is not null then d.departmentrejected_remarks" +
                            " when a.department_remarks is not null then a.department_remarks end as departmentrejected_remarks from its_trn_tserviceapproval a " +
                            " left join hrm_mst_temployee b on b.employee_gid = a.dept_manager " +
                            " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                            " left join its_trn_tcomplaintapprovallog d on a.complaint_gid = d.complaint_gid " +
                            " where a.complaint_gid = '" + objviewmanagement.complaint_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objviewmanagement.department_approval = objODBCDatareader["user_firstname"].ToString();
                        objviewmanagement.department_approveddate = objODBCDatareader["DepartmentApproved/RejectedDate"].ToString();
                        objviewmanagement.department_approvedstatus = objODBCDatareader["departmentapproval_status"].ToString();
                        objviewmanagement.departmentrejected_remarks = objODBCDatareader["departmentrejected_remarks"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select a.manager_name,b.managementapproval_status,case when b.managementapproved_date is null then " +
                            " date_format(b.managementrejected_date,'%d-%m-%Y  %h:%i %p') else date_format(b.managementapproved_date,'%d-%m-%Y  %h:%i %p') " +
                            " END as 'ManagementApproved/RejectedDate',case when b.managementrejected_remarks is not null then b.managementrejected_remarks" +
                            " when a.management_remarks is not null then a.management_remarks end as managementrejected_remarks " +
                            " from its_trn_tserviceapproval a " +
                            " left join its_trn_tcomplaintapprovallog b on a.complaint_gid = b.complaint_gid " +
                            "  where a.complaint_gid='" + objviewmanagement.complaint_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        objviewmanagement.management_approval = objODBCDatareader["manager_name"].ToString();
                        objviewmanagement.management_approveddate = objODBCDatareader["ManagementApproved/RejectedDate"].ToString();
                        objviewmanagement.management_approvedstatus = objODBCDatareader["managementapproval_status"].ToString();
                        objviewmanagement.managementrejected_remarks = objODBCDatareader["managementrejected_remarks"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select a.complaint_gid,a.status,concat(c.user_firstname,' ',c.user_lastname) as user_firstname, " +
                        " case when d.serviceapproved_date is null then date_format(d.servicerejected_date,'%d-%m-%Y  %h:%i %p') " +
                        " else  date_format(d.serviceapproved_date,'%d-%m-%Y  %h:%i %p') END as 'ServiceApproved/RejectedDate', " +
                        " d.serviceapproval_status,case when d.servicerejected_remarks is not null then d.servicerejected_remarks" +
                        " when a.service_remarks is not null then a.service_remarks end as servicerejected_remarks from its_trn_tserviceapproval a " +
                        " left join hrm_mst_temployee b on b.employee_gid=a.service_manager " +
                        " left join adm_mst_tuser c on b.user_gid=c.user_gid " +
                         " left join its_trn_tcomplaintapprovallog d on a.complaint_gid = d.complaint_gid " +
                        " where a.complaint_gid='" + objviewmanagement.complaint_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        objviewmanagement.service_approval = objODBCDatareader["user_firstname"].ToString();
                        objviewmanagement.service_approveddate = objODBCDatareader["ServiceApproved/RejectedDate"].ToString();
                        objviewmanagement.service_approvedstatus = objODBCDatareader["serviceapproval_status"].ToString();
                        objviewmanagement.servicerejected_remarks = objODBCDatareader["servicerejected_remarks"].ToString();
                    }
                    objODBCDatareader.Close();
                }
                else
                {
                    objODBCDatareader.Close();
                }
                objviewmanagement.status = true;
                return true;
            }
            catch
            {
                objviewmanagement.status = false;
                return false;
            }
        }

        // Approval Mail.....//

        public void popmail(string complaint_gid)
        {
            
            msSQL = " select DATE_FORMAT(f.complaint_date,'%d-%m-%Y %H:%i %p') as complaint_date,f.complaint_refno,f.complaint_title,e.user_code,f.complaint_remarks," +
              " f.complaint_gid,f.complaint_refno, f.complaint_title, a.dept_manager, a.service_manager, a.status,a.manager_name,a.manager_gid, " +
              " concat(e.user_code,'/',e.user_firstname,'',e.user_lastname) as raisedfor_employee, " +
              " concat(j.category_name,'/',i.subcategory_name,'/',k.type_name) as category_name,f.category_gid " +
              " from its_trn_tserviceapproval a " +
              " left join its_trn_tcomplaint f on f.complaint_gid=a.complaint_gid " +
              " left join hrm_mst_temployee d on d.employee_gid = f.raisedfor_employee " +
              " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
              " left join its_mst_tcategory j on j.category_gid =f.category_gid " +
              " left join its_mst_tsubcategory i on i.subcategory_gid=f.subcategory_gid " +
              " left join its_mst_ttype k on k.type_gid = f.type_gid " +
              " where a.complaint_gid='" + complaint_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsdepartment_manager = objODBCDatareader["dept_manager"].ToString();
                lsservice_manager = objODBCDatareader["service_manager"].ToString();
                lsmanager_gid = objODBCDatareader["manager_gid"].ToString();
                lscategory_name = objODBCDatareader["category_name"].ToString();
                lsraised_employee = objODBCDatareader["raisedfor_employee"].ToString();
                lscomplaint_refno = objODBCDatareader["complaint_refno"].ToString();
                lscomplaint_title = objODBCDatareader["complaint_title"].ToString();
                lscomplaint_remarks = objODBCDatareader["complaint_remarks"].ToString();
                lscomplaint_date = objODBCDatareader["complaint_date"].ToString();
                lsstatus = objODBCDatareader["status"].ToString();
            }
            objODBCDatareader.Close();
            if (lsstatus == "Department Approval Pending")
            {
                lsstatus = "SERVICE TICKET- Department Approval Mail-" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "";
                lsemployee_gid = lsdepartment_manager;
            }
            else if (lsstatus == "Service Department Approval Pending")
            {
                lsstatus = "SERVICE TICKET-Service Department Approval Mail-" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "";
                lsemployee_gid = lsdepartment_manager;
            }
            else if (lsstatus == "Management Approval Pending")
            {
                lsstatus = "SERVICE TICKET-Management Approval Mail-" + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + "";
                lsemployee_gid = lsdepartment_manager;
            }

            msSQL = "select employee_emailid from hrm_mst_temployee where employee_gid='" + lsemployee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                employee_mailid = objODBCDatareader["employee_emailid"].ToString();
            }
            objODBCDatareader.Close();

            message = "Dear Sir/Madam,  <br />";
            message = message + "<br />";
            message = message + "Greetings,  <br />";
            message = message + "<br />";
            message = message + "" + lsraised_employee + " has raised Ticket and the details are as follows.<br />";
            message = message + "<br />";
            message = message + "<b>Ticket Ref No :</b> " + lscomplaint_refno + "<br />";
            message = message + "<br />";
            message = message + "<b>Ticket Raised Date/Time :</b> " + lscomplaint_date + "<br />";
            message = message + "<br />";
            message = message + "<b>Category/Subcategory/Type :</b> " + lscategory_name + "<br />";
            message = message + "<br />";
            message = message + "<b>Ticket Title :</b> " + lscomplaint_title + "<br />";
            message = message + "<br />";
            message = message + "<b>Ticket Description :</b> " + lscomplaint_remarks + "<br />";
            message = message + "<br />";
            message = message + "Kindly Login  <a href='https://" + ConfigurationManager.AppSettings["companylinkname"] + ".samunnati.com'>" + ConfigurationManager.AppSettings["companylinkname"] + ".samunnati.com </a>  and Do the Needful<br />";
            message = message + "<br />";
            message = message + "<b>Thanks & Regards, </b> ";
            message = message + "<br />";
            message = message + "<b> IT Support </b> ";
            message = message + "<br />";

            msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany where company_gid='1'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                lspop_mail = objODBCDatareader["pop_username"].ToString();
            }
            objODBCDatareader.Close();


            if ((employee_mailid != "") || employee_mailid != null)
            {
                mailflag = objcmnfunctions.mail(employee_mailid, lsstatus, message);
                if (mailflag == true)
                {
                    msgetmail_gid = objcmnfunctions.GetMasterGID("ITSMAIL");
                    msSQL = " insert into its_trn_tmailcount ( " +
                        " mail_gid, " +
                        " complaint_gid, " +
                        " from_mail, " +
                        " to_mail, " +
                        " cc_mail," +
                        " mail_status," +
                        " mail_senddate, " +
                        " created_by ," +
                        " created_date " +
                        " ) values( " +
                        "'" + msgetmail_gid + "'," +
                        "'" + complaint_gid + "'," +
                        "'" + lspop_mail + "'," +
                        "'" + employee_mailid + "'," +
                        "'null'," +
                        "'" + lsstatus + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + lsemployee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
           
        }

        public void popreject(string complaint_gid,string lsremarks)
        {
            
            msSQL = " select DATE_FORMAT(f.complaint_date,'%d-%m-%Y %H:%i %p') as complaint_date,f.complaint_refno,f.complaint_title,e.user_code,f.complaint_remarks," +
              " f.complaint_gid,f.complaint_refno, f.complaint_title, a.dept_manager, a.service_manager, a.status,a.manager_name,a.manager_gid, " +
              " f.raisedfor_employee, concat(j.category_name,'/',i.subcategory_name,'/',k.type_name) as category_name,f.category_gid " +
              " from its_trn_tserviceapproval a " +
              " left join its_trn_tcomplaint f on f.complaint_gid=a.complaint_gid " +
              " left join hrm_mst_temployee d on d.employee_gid = f.raisedfor_employee " +
              " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
              " left join its_mst_tcategory j on j.category_gid =f.category_gid " +
              " left join its_mst_tsubcategory i on i.subcategory_gid=f.subcategory_gid " +
              " left join its_mst_ttype k on k.type_gid = f.type_gid " +
              " where a.complaint_gid='" + complaint_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsdepartment_manager = objODBCDatareader["dept_manager"].ToString();
                lsservice_manager = objODBCDatareader["service_manager"].ToString();
                lsmanager_gid = objODBCDatareader["manager_gid"].ToString();
                lscategory_name = objODBCDatareader["category_name"].ToString();
                lsraised_employee = objODBCDatareader["raisedfor_employee"].ToString();
                lscomplaint_refno = objODBCDatareader["complaint_refno"].ToString();
                lscomplaint_title = objODBCDatareader["complaint_title"].ToString();
                lscomplaint_remarks = objODBCDatareader["complaint_remarks"].ToString();
                lscomplaint_date = objODBCDatareader["complaint_date"].ToString();
                lsstatus = objODBCDatareader["status"].ToString();
            }
            objODBCDatareader.Close();

            /// ......... Department Reject .........///

            if (lsstatus == "Department Approval Rejected")
            {
                lsmail_status = lsstatus;
                lsstatus = "SERVICE TICKET-Department Approval Rejected ";
                lsemployee_gid = lsdepartment_manager;
            }

            /// ......... Service Department Reject .........///

            if (lsstatus == "Service Department Rejected")
            {
                lsmail_status = lsstatus;
                lsstatus = "SERVICE TICKET-Service Department Approval Rejected ";
                lsemployee_gid = lsservice_manager;
            }

            /// ......... Management Reject .........///

            if (lsstatus == "Management Approval Rejected")
            {
                lsmail_status = lsstatus;
                lsstatus = "SERVICE TICKET-Management Approval Rejected";
                lsemployee_gid = lsmanager_gid;
            }

            msSQL = " select concat(a.user_firstname,'',a.user_lastname,'/',a.user_code) as approval_name from adm_mst_tuser a " +
                    " left join hrm_mst_temployee b on a.user_gid=b.user_gid where employee_gid ='" + lsemployee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsdepartment_manager = objODBCDatareader["approval_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select employee_emailid from hrm_mst_temployee where employee_gid='" + lsraised_employee + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                employee_mailid = objODBCDatareader["employee_emailid"].ToString();
            }
            objODBCDatareader.Close();

            message = "Dear Sir/Madam,  <br />";
            message = message + "<br />";
            message = message + "Greetings,  <br />";
            message = message + "<br />";
            message = message + "The Raised Ticket has been Rejected by <b>" + lsmail_status + " / " + lsdepartment_manager + "</b> and the details are as follows, <br/>";
            message = message + "<br />";
            message = message + "<b>Ticket Ref No :</b> " + lscomplaint_refno + "<br />";
            message = message + "<br />";
            message = message + "<b>Ticket Raised Date/Time :</b> " + lscomplaint_date + "<br />";
            message = message + "<br />";
            message = message + "<b>Category/Subcategory/Type :</b> " + lscategory_name + "<br />";
            message = message + "<br />";
            message = message + "<b>Ticket Title :</b> " + lscomplaint_title + "<br />";
            message = message + "<br />";
            message = message + "<b>Ticket Description :</b> " + lscomplaint_remarks + "<br />";
            message = message + "<br />";
            message = message + "<b>Remarks:</b> " + lsremarks + "<br />";
            message = message + "<br />";
            message = message + "<b>Thanks & Regards, </b> ";
            message = message + "<br />";
            message = message + "<b> IT Support </b> ";
            message = message + "<br />";

            msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany where company_gid='1'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                lspop_mail = objODBCDatareader["pop_username"].ToString();
            }
            objODBCDatareader.Close();

            if ((employee_mailid != "") || employee_mailid != null)
            {
                mailflag = objcmnfunctions.mail(employee_mailid, lsstatus, message);
                if (mailflag == true)
                {
                    msgetmail_gid = objcmnfunctions.GetMasterGID("ITSMAIL");
                    msSQL = " insert into its_trn_tmailcount ( " +
                        " mail_gid, " +
                        " complaint_gid, " +
                        " from_mail, " +
                        " to_mail, " +
                        " cc_mail," +
                        " mail_status," +
                        " mail_senddate, " +
                        " created_by ," +
                        " created_date " +
                        " ) values( " +
                        "'" + msgetmail_gid + "'," +
                        "'" + complaint_gid + "'," +
                        "'" + lspop_mail + "'," +
                        "'" + employee_mailid + "'," +
                        "'null'," +
                        "'" + lsstatus + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + lsemployee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
           
        }

        // Dependencty Approval...//

        public bool DaPostDependencyApprove(dependencyapprove values, string employee_gid)
        {
            try
            {
                msSQL = "update its_trn_tdependencyapproval set " +
                                   "dependency_status='Approved', " +
                                   " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                   " updated_by ='" + employee_gid + "'" +
                                   "where dependentapproval_gid='" + values.dependentapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select release_gid from its_trn_tdependencyapproval where dependentapproval_gid='" + values.dependentapproval_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsrelease_gid = objODBCDatareader["release_gid"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = "select count(dependentapproval_gid) as dependentapproval from its_trn_tdependencyapproval " +
                        "where release_gid='" + lsrelease_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsdependent_approvalcount = objODBCDatareader["dependentapproval"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = " select count(dependency_status) as dependencyapprovalcount from its_trn_tdependencyapproval " +
                        " where release_gid='" + lsrelease_gid + "' and dependency_status='Approved'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsdependency_approvedcount = objODBCDatareader["dependencyapprovalcount"].ToString();
                }
                objODBCDatareader.Close();

                if (lsdependent_approvalcount == lsdependency_approvedcount)
                {
                    msSQL = " update its_trn_trelease set approval_status='Dependency Approval Done' where release_gid='" + lsrelease_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select issuetracker_gid from its_trn_tissue2release where release_gid='" + lsrelease_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        msSQL = " update its_trn_tissue2release set approval_status ='Dependency Approval Done' " +
                                " where issuetracker_gid ='" + dr_datarow["issuetracker_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable.Dispose();
                }
                values.status = true;
                values.message = "Dependency Approved Successfully !";
                return true;
            }
            catch
            {
                values.status = true;
                values.message = "Dependency Approved Successfully !";
                return false;
            }
        }

        // Dependencty Reject...//

        public bool DaPostDependencyReject(dependencyreject values, string employee_gid)
        {
            try
            {
                msSQL = "update its_trn_tdependencyapproval set " +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " updated_by ='" + employee_gid + "'," +
                        "dependency_status='Rejected' where dependentapproval_gid='" + values.dependentapproval_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Dependency Rejected Successfully !";
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
                return false;
            }
        }

        // CAC Approval ....//

        public bool DaPostCabApprove(cabapprove values, string employee_gid)
        {
           try
            {
                msSQL = "select release_gid from its_trn_tcacapproval where cacapproval_gid='" + values.cacapproval_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsrelease_gid = objODBCDatareader["release_gid"].ToString();
                }
                objODBCDatareader.Close();

                //msSQL = "select role from its_mst_tcabapproval where cabmember_gid = '" + employee_gid + "'";
                msSQL = "select role, count(role) as head_count, count(approval_status) as approval_count, approval_member from its_trn_tcacapproval where role like '%Head' and approval_status like '%Approved' and release_gid = '" + lsrelease_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsrole = objODBCDatareader["role"].ToString();
                    approval_count = objODBCDatareader["approval_count"].ToString();
                    //lsapproval_member = objODBCDatareader["approval_member"].ToString();
                    head_count = objODBCDatareader["head_count"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = "select count(approval_status) as total_approval_count from its_trn_tcacapproval where role like '%Head' and release_gid='" + lsrelease_gid + "'";
                total_head_count = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "select approval_member from its_trn_tcacapproval where approval_member='" + employee_gid + "' and release_gid='" + lsrelease_gid + "'";
                lsapproval_member = objdbconn.GetExecuteScalar(msSQL);

                if (lsrole == "Head")
                {
                    if (employee_gid == lsapproval_member)
                    {
                        if (Convert.ToInt16(approval_count) >= (Convert.ToInt16(total_head_count) - 1))
                        {
                            msSQL = " update its_trn_tcacapproval set " +
                               " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               " approval_status='Approved'" +
                               " where approval_member='" + employee_gid + "' and  release_gid='" + lsrelease_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = "select approval_status,cacapproval_gid from its_trn_tcacapproval where release_gid = '" + lsrelease_gid + "'";
                            dt_datatable = objdbconn.GetDataTable(msSQL);
                            foreach (DataRow dt_datarow in dt_datatable.Rows)
                            {
                                if (dt_datarow["approval_status"].ToString() == "Approval Pending...")
                                {
                                    msSQL = " update its_trn_tcacapproval set " +
                                                " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                " approval_status='Approved'," +
                                                " headapproval_status='Approved by Head'," +
                                                " approval_remarks='" + values.approval_remarks + "' " +
                                                " where cacapproval_gid='" + dt_datarow["cacapproval_gid"].ToString() + "'";
                                    //" where release_gid='" + lsrelease_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            dt_datatable.Dispose();
                        }
                        msSQL = " update its_trn_tcacapproval set " +
                              " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                              " approval_status='Approved'" +
                              " where approval_member='" + employee_gid + "' and  release_gid='" + lsrelease_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {
                    msSQL = " update its_trn_tcacapproval set " +
                       " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                       " approval_status='Approved'," +
                       " approval_remarks='" + values.approval_remarks + "' " +
                       " where approval_member='" + employee_gid + "' and  release_gid='" + lsrelease_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = "select count(cacapproval_gid) as capapproval from its_trn_tcacapproval where release_gid='" + lsrelease_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscap_approvalcount = objODBCDatareader["capapproval"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select count(approval_status) as capapprovalcount from its_trn_tcacapproval where release_gid='" + lsrelease_gid + "'" +
                        " and (approval_status='Approved' or approval_status='CAC Approval - Not Required')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsapprovedcount = objODBCDatareader["capapprovalcount"].ToString();
                }
                objODBCDatareader.Close();
                if (lscap_approvalcount == lsapprovedcount)
                {
                    msSQL = " update its_trn_trelease set approval_status='CAC Approval Done' where release_gid='" + lsrelease_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select issuetracker_gid from its_trn_tissue2release where release_gid='" + lsrelease_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt_datarow in dt_datatable.Rows)
                    {
                        msSQL = " update its_trn_tissue2release set approval_status ='CAC Approval Done' " +
                                 " where issuetracker_gid ='" + dt_datarow["issuetracker_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable.Dispose();
                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "CAC Approved Successfully !";
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured !";
                    return false;
                }

            }
            catch
            {
                values.status = false;
                values.message = "Error Occured !";
                return false;
            }
        }
        // CAB Reject .....//
        public bool DaPostCabReject(cabreject values, string employee_gid)
        {
            try
            {
                msSQL = "select release_gid from its_trn_tcacapproval where cacapproval_gid='" + values.cacapproval_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsrelease_gid = objODBCDatareader["release_gid"].ToString();
                }
                objODBCDatareader.Close();

               

                //msSQL = "select role from its_mst_tcabapproval where cabmember_gid = '" + employee_gid + "'";
                msSQL = "select role,count(role) as head_count,count(approval_status) as approval_count,approval_member from its_trn_tcacapproval where role like '%Head' and approval_status like '%Rejected' and release_gid='" + lsrelease_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsrole = objODBCDatareader["role"].ToString();
                    approval_count = objODBCDatareader["approval_count"].ToString();
                    //lsapproval_member = objODBCDatareader["approval_member"].ToString();
                    head_count = objODBCDatareader["head_count"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = "select count(approval_status) as total_approval_count from its_trn_tcacapproval where role like '%Head' and release_gid='" + lsrelease_gid + "'";
                total_head_count = objdbconn.GetExecuteScalar(msSQL);
                msSQL = "select approval_member from its_trn_tcacapproval where approval_member='" + employee_gid + "' and release_gid='" + lsrelease_gid + "'";
                lsapproval_member = objdbconn.GetExecuteScalar(msSQL);

                if (lsrole == "Head")
                {
                    if (employee_gid == lsapproval_member)
                    {
                        if (Convert.ToInt16(approval_count) >= (Convert.ToInt16(total_head_count) - 1))
                        {
                            msSQL = " update its_trn_tcacapproval set " +
                               " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               " approval_status='Rejected'," +
                               " approval_remarks='" + values.reject_remarks + "' " +
                               " where approval_member='" + employee_gid + "' and  release_gid='" + lsrelease_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msSQL = "select approval_status,cacapproval_gid from its_trn_tcacapproval where release_gid = '" + lsrelease_gid + "'";
                            dt_datatable = objdbconn.GetDataTable(msSQL);
                            foreach (DataRow dt_datarow in dt_datatable.Rows)
                            {
                                if (dt_datarow["approval_status"].ToString() == "Approval Pending...")
                                {
                                    msSQL = " update its_trn_tcacapproval set " +
                                                " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                                " approval_status='Rejected'," +
                                                " headapproval_status='Rejected by Head'," +
                                                " approval_remarks='" + values.reject_remarks + "' " +
                                                " where cacapproval_gid='" + dt_datarow["cacapproval_gid"].ToString() + "'";
                                    //" where release_gid='" + lsrelease_gid + "'";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            dt_datatable.Dispose();
                        }
                        msSQL = " update its_trn_tcacapproval set " +
                                " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                " approval_status='Rejected'," +
                                " approval_remarks='" + values.reject_remarks + "' " +
                                " where approval_member='" + employee_gid + "' and  release_gid='" + lsrelease_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                else
                {
                    msSQL = " update its_trn_tcacapproval set " +
                       " approved_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                       " approval_status='Rejected'," +
                       " approval_remarks='" + values.reject_remarks + "' " +
                       " where approval_member='" + employee_gid + "' and  release_gid='" + lsrelease_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                msSQL = "select count(cacapproval_gid) as capapproval from its_trn_tcacapproval where release_gid='" + lsrelease_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscap_approvalcount = objODBCDatareader["capapproval"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select count(approval_status) as capapprovalcount from its_trn_tcacapproval where release_gid='" + lsrelease_gid + "'" +
                        " and (approval_status='Rejected' or approval_status='CAC Approval - Not Required')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsapprovedcount = objODBCDatareader["capapprovalcount"].ToString();
                }
                objODBCDatareader.Close();
                if (lscap_approvalcount == lsapprovedcount)
                {
                    msSQL = " update its_trn_trelease set approval_status='CAC Approval Rejected' where release_gid='" + lsrelease_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select issuetracker_gid from its_trn_tissue2release where release_gid='" + lsrelease_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dt_datarow in dt_datatable.Rows)
                    {
                        msSQL = " update its_trn_tissue2release set approval_status ='CAC Approval Rejected' " +
                                 " where issuetracker_gid ='" + dt_datarow["issuetracker_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    dt_datatable.Dispose();
                }
            //    try
            //{
            //    msSQL = " update its_trn_tcacapproval set " +

            //       " rejected_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
            //        " approval_remarks='" + values.reject_remarks + "'," +
            //       " approval_status='Rejected' where cacapproval_gid='" + values.cacapproval_gid + "'";
            //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "CAC Rejected Successfully !";
               
                   
                    return true;
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured !";
                    return false;
                }
            }
            catch
            {
                values.status = false;
                values.message = "Error Occured !";
                return false;
            }
        }

        // Release Details...//
        public bool DaGetReleaseDetails(string release_gid, releaseapprovaldetails values, string employee_gid)
        {
           try
            {

                msSQL = " select count(e.issuetracker_gid) as issue_releasecount,a.released_by,a.application,a.vendor, " +
                        " a.release_status,a.approval_status, a.release_gid,a.ref_no,date_format(a.release_date,'%d-%m-%Y') as release_date, a.release_remarks,a.done_by, " +
                        " a.created_date,a.released_on,a.release_notes,b.change_description,b.impacted_module,b.impacted_system, " +
                        " b.reason_change,b.alternative_way,b.resources from its_trn_trelease a " +
                        " left join its_trn_tchangedetails b on a.release_gid=b.release_gid " +
                        " left join its_trn_tissuetracker e on e.release_gid=a.release_gid " +
                        " where a.release_gid='" + release_gid + "' group by a.release_gid";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.applicationname = objODBCDatareader["application"].ToString();
                    values.vendorname = objODBCDatareader["vendor"].ToString();
                    values.ref_no = objODBCDatareader["ref_no"].ToString();
                    values.release_date = objODBCDatareader["release_date"].ToString();
                    values.released_by = objODBCDatareader["released_by"].ToString();
                    values.released_on = objODBCDatareader["released_on"].ToString();
                    values.release_status = objODBCDatareader["release_status"].ToString();
                    values.release_notes = objODBCDatareader["release_notes"].ToString();
                    values.approval_status = objODBCDatareader["approval_status"].ToString();
                    values.issuerelease_count = objODBCDatareader["issue_releasecount"].ToString();
                    values.release_remarks = objODBCDatareader["release_remarks"].ToString();
                    values.change_description = objODBCDatareader["change_description"].ToString();
                    values.impacted_module = objODBCDatareader["impacted_module"].ToString();
                    values.impacted_system = objODBCDatareader["impacted_system"].ToString();
                    values.reason_change = objODBCDatareader["reason_change"].ToString();
                    values.alternative_way = objODBCDatareader["alternative_way"].ToString();
                    values.resources = objODBCDatareader["resources"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select b.release_gid,b.issue2release_gid,a.issuetracker_gid,a.issue_refno,a.issue_status,a.issue_type,a.issue_title," +
                      " a.issue_remarks, a.Severity,a.priority,date_format(a.issue_date,'%d-%m-%Y') as issue_date,a.response_time from its_trn_tissuetracker a " +
                      " left join its_trn_tissue2release b on b.issuetracker_gid = a.issuetracker_gid " +
                      " where b.release_gid = '" + release_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var releaseissue = new List<releaseissue_list>();
                foreach (DataRow dt_datarow in dt_datatable.Rows)
                {
                    releaseissue.Add(new releaseissue_list
                    {
                        issue_refno = dt_datarow["issue_refno"].ToString(),
                        issue_status = dt_datarow["issue_status"].ToString(),
                        issue_date = dt_datarow["issue_date"].ToString(),
                        issue_type = dt_datarow["issue_type"].ToString(),
                        issue_title = dt_datarow["issue_title"].ToString(),
                        issue_remarks = dt_datarow["issue_remarks"].ToString(),
                        Severity = dt_datarow["Severity"].ToString(),
                        priority = dt_datarow["priority"].ToString(),
                        issuetracker_gid = dt_datarow["issuetracker_gid"].ToString()

                    });

                    values.releaseissue_list = releaseissue;
                    msSQL = " select a.issuetracker_gid,a.issuestatuslog_gid,b.issue_refno,b.issue_title,a.issue_status,date_format(b.issue_date,'%d-%m-%Y') as issue_date," +
                      " a.remarks,date_format(a.uat_date, '%d-%m-%Y') as uat_date,date_format(a.created_date, '%d-%m-%Y') as created_date," +
                      " case when a.done_by is null then concat(user_code,' / ', user_firstname, ' / ', user_lastname) else done_by end as created_by " +
                      " from its_trn_tissuestatuslog a " +
                      " left join its_trn_tissuetracker b on a.issuetracker_gid = b.issuetracker_gid " +
                      " left join its_trn_tuattracker d on a.issuetracker_gid = d.issuetracker_gid " +
                      " left join adm_mst_tuser c on a.created_by = c.user_gid " +
                      " where a.issuetracker_gid in ('" + dt_datarow["issuetracker_gid"].ToString() + "') group by a.issuestatuslog_gid order by a.created_date desc";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var uatlog = new List<uatlog_list>();
                    foreach (DataRow dtdatarow in dt_datatable.Rows)
                    {
                        uatlog.Add(new uatlog_list
                        {
                            issuetracker_gid = lsissuetracker_gid,
                            issuestatuslog_gid = dtdatarow["issuestatuslog_gid"].ToString(),
                            remarks = dtdatarow["remarks"].ToString(),
                            issue_status = dtdatarow["issue_status"].ToString(),
                            uat_date = dtdatarow["uat_date"].ToString(),
                            created_date = dtdatarow["created_date"].ToString(),
                            created_by = dtdatarow["created_by"].ToString()
                        });
                    }
                }

                dt_datatable.Dispose();

                msSQL = " select a.dependentapproval_gid,concat(c.application_code ,' / ',c.application_name) as applicationdependent," +
                           " a.dependency_status,b.stakeholder_name,b.approval_gid,concat(e.user_code,' - ',e.user_firstname,' ',e.user_lastname) as approval_name," +
                           " b.application_name,date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as approved_date from its_trn_tdependencyapproval a " +
                           " left join its_mst_tapplicationdependency b on a.applicationdependency_gid = b.applicationdependency_gid " +
                           " left join its_mst_tapplicationmaster c on b.application_dependent = c.applicationmaster_gid " +
                           " left join hrm_mst_temployee d on d.employee_gid=b.approval_gid " +
                           " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                           " where a.release_gid = '" + release_gid + "' and dependency_status<> 'Get Dependency Approval'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var dependency = new List<dependency_list>();

                foreach (DataRow dt_datarow in dt_datatable.Rows)
                {
                    lsapprovalgid = dt_datarow["approval_gid"].ToString();
                    if (lsapprovalgid == employee_gid && dt_datarow["dependency_status"].ToString() == "Approval Pending...")
                    {
                        lsdepndency_approval = "Y";
                        lsdependencyapproval_gid = dt_datarow["dependentapproval_gid"].ToString();
                    }
                    else
                    {
                        lsdepndency_approval = "N";
                    }
                    dependency.Add(new dependency_list
                    {
                        dependentapproval_gid = dt_datarow["dependentapproval_gid"].ToString(),
                        applicationdependent = dt_datarow["applicationdependent"].ToString(),
                        dependency_status = dt_datarow["dependency_status"].ToString(),
                        stakeholder_name = dt_datarow["stakeholder_name"].ToString(),
                        application_name = dt_datarow["application_name"].ToString(),
                        approval = dt_datarow["approval_name"].ToString(),
                        approved_date = dt_datarow["approved_date"].ToString(),
                        dependency_approval = lsdepndency_approval,
                        dependencyapprovalpending_gid = lsdependencyapproval_gid
                    });
                }
                values.dependency_list = dependency;
                dt_datatable.Dispose();

                msSQL = " select a.cacapproval_gid,a.release_gid,a.approval_member,case when headapproval_status is null then a.approval_status" +
                        " else concat(a.headapproval_status) end as approval_status,a.created_date,d.department_gid,d.department_name," +
                       " cast( case when a.approved_date is null and a.rejected_date is not null then date_format(a.rejected_date, '%d-%m-%Y %h:%i %p')" +
                       " when a.rejected_date is null and a.approved_date is not null then date_format(a.approved_date, '%d-%m-%Y %h:%i %p') " +
                       " else '-' end as char) as approval_date,case when a.approval_remarks is not null then a.approval_remarks else '-' end as approval_remarks ," +
                       " date_format(a.approval_requested, '%d-%m-%y %h:%i %p') as approval_requested, " +
                       " concat(c.user_firstname, '  ', c.user_lastname, ' / ', c.user_code) as approval_name from its_trn_tcacapproval a " +
                       " left join hrm_mst_temployee b on a.approval_member = b.employee_gid " +
                       " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                       " left join its_mst_tcabapproval d on d.cabmember_gid = a.approval_member " +
                       " where release_gid = '" + release_gid + "' and approval_status<> 'Get CAC Approval' order by approval_member asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var cab = new List<cab_list>();
                foreach (DataRow dt_datarow in dt_datatable.Rows)
                {
                    lsapprovalgid = dt_datarow["approval_member"].ToString();
                    if (lsapprovalgid == employee_gid && dt_datarow["approval_status"].ToString() == "Approval Pending...")
                    {
                        lscab_approval = "Y";
                        lscacapproval_gid = dt_datarow["cacapproval_gid"].ToString();
                    }
                    else
                    {
                        lscab_approval = "N";
                    }
                    cab.Add(new cab_list
                    {
                        cacapproval_gid = dt_datarow["cacapproval_gid"].ToString(),
                        approval_member = dt_datarow["approval_member"].ToString(),
                        approval_status = dt_datarow["approval_status"].ToString(),
                        department_name = dt_datarow["department_name"].ToString(),
                        approval_date = dt_datarow["approval_date"].ToString(),
                        approval_requested = dt_datarow["approval_requested"].ToString(),
                        approval_name = dt_datarow["approval_name"].ToString(),
                        approval_remarks = dt_datarow["approval_remarks"].ToString(),
                        cab_approval = lscab_approval,
                        cacapprovalpending_gid = lscacapproval_gid
                    });
                }
                values.cab_list = cab;
                dt_datatable.Dispose();

                msSQL = "select uatdocument_gid,document_name,document_path from its_trn_uatdocument where release_gid='" + release_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var uatdocument = new List<uatdocument_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        uatdocument.Add(new uatdocument_list
                        {
                            uatdocument_gid = dt["uatdocument_gid"].ToString(),
                            document_name = dt["document_name"].ToString(),
                            document_path = HttpContext.Current.Server.MapPath(dt["document_path"].ToString()),
                        });
                    }
                }
                values.uatdocument_list = uatdocument;
                dt_datatable.Dispose();

                msSQL = " select a.document_gid,a.documentref_no,a.document_name,a.document_path,date_format(a.created_date,'%d-%m-%Y %h:%p %i') as created_date, " +
                        " concat(b.user_code, ' / ' , b.user_firstname, b.user_lastname) as created_by " +
                        " from its_trn_tdocumentupload a " +
                        " left join adm_mst_tuser b on a.created_by = b.user_gid " +
                        " where release_gid='" + release_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var MdlApprovalDocuments = new List<MdlApprovalDocuments>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        MdlApprovalDocuments.Add(new MdlApprovalDocuments
                        {
                            document_gid = dt["document_gid"].ToString(),
                            documentref_no = dt["documentref_no"].ToString(),
                            document_name = dt["document_name"].ToString(),
                            document_path = HttpContext.Current.Server.MapPath(dt["document_path"].ToString()),
                            created_by = dt["created_by"].ToString(),
                            created_date = dt["created_date"].ToString(),
                        });
                    }
                }
                values.ApprovalDocuments_List = MdlApprovalDocuments;
                dt_datatable.Dispose();

                msSQL = " select a.dependency_count, b.cab_count from " +
                       " (select count(dependentapproval_gid) as dependency_count from its_trn_tdependencyapproval where release_gid = '" + release_gid + "') as a, " +
                       " (select count(cacapproval_gid) as cab_count from its_trn_tcacapproval where release_gid = '" + release_gid + "') as b ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    if (objODBCDatareader["dependency_count"].ToString() == "0")
                    {
                        values.dependency_count = "No Dependency Approval";
                    }
                    else
                    {
                        values.dependency_count = objODBCDatareader["dependency_count"].ToString();
                    }
                    if (objODBCDatareader["cab_count"].ToString() == "0")
                    {
                        values.cab_count = "No CAC Approval";
                    }
                    else
                    {
                        values.cab_count = objODBCDatareader["cab_count"].ToString();
                    }
                }
                objODBCDatareader.Close();

                values.status = true;
                return true;
            }
            catch
            {
                values.status = true;
                return false;
            }
        }

        public bool DaGetUatDetails(string issuetracker_gid, uatlog values)
        {
            try
            {
                msSQL = " select a.issuetracker_gid,a.issuestatuslog_gid,b.issue_refno,b.issue_title,a.issue_status,date_format(b.issue_date,'%d-%m-%Y') as issue_date," +
                  " a.remarks,date_format(a.uat_date, '%d-%m-%Y') as uat_date,date_format(a.created_date, '%d-%m-%Y') as created_date," +
                  " case when a.done_by is null then concat(user_code,' / ', user_firstname, ' / ', user_lastname) else done_by end as created_by " +
                  " from its_trn_tissuestatuslog a " +
                  " left join its_trn_tissuetracker b on a.issuetracker_gid = b.issuetracker_gid " +
                  " left join its_trn_tuattracker d on a.issuetracker_gid = d.issuetracker_gid " +
                  " left join adm_mst_tuser c on a.created_by = c.user_gid " +
                  " where a.issuetracker_gid in ('" + issuetracker_gid + "') group by a.issuestatuslog_gid order by a.created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var uatlog = new List<uatlog_list>();
                foreach (DataRow dtdatarow in dt_datatable.Rows)
                {
                    uatlog.Add(new uatlog_list
                    {
                        issuestatuslog_gid = dtdatarow["issuestatuslog_gid"].ToString(),
                        remarks = dtdatarow["remarks"].ToString(),
                        issue_status = dtdatarow["issue_status"].ToString(),
                        uat_date = dtdatarow["uat_date"].ToString(),
                        created_date = dtdatarow["created_date"].ToString(),
                        created_by = dtdatarow["created_by"].ToString()
                    });
                }
                values.uatlog_list = uatlog;
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }

        public void DaGetApprovalRemarksView(string release_gid, cabapprove values)
        {
            msSQL = " select approval_remarks, b.application from its_trn_tcacapproval a " +
                    " left join its_trn_trelease b on a.release_gid = b.release_gid " +
                    " where a.release_gid = '" + release_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.approval_remarks = objODBCDatareader["approval_remarks"].ToString();
                values.application = objODBCDatareader["application"].ToString();

            }
            objODBCDatareader.Close();

        }

    }
}
