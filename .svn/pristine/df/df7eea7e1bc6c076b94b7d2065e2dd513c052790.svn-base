using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.storage.Functions;
using ems.its.Models;
using System.IO;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace ems.its.DataAccess
{
    public class DaNewServiceTicket 
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcConnection objODBCconnection;
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        DataTable dt_datatable, dt_level3, dt_levelone;
        string msSQL;
        int mnResult,ls_port;
        string lspath;
        string lscompany_code;
        HttpPostedFile httpPostedFile;
        string lsraisedemployee, lsmanager_gid;
        string lsdepartment_name, lsmanager_name;
        string ls_campaigngid, lsinternal_approval;
        string lsapproval_flag, lsemployee_gid;
        string lscampaign_description, lscampaign_gid;
        string lsotheremployee_gid, lsdepartment_manager;
        string lsservice_manager, lsnoofassigned_employee;
        string lsinprogress_count, lsstatus, lsteam_gid;
        string ls_server, ls_username, ls_password, lsto_mail, frommail_id, tomail_id, body, sub, lsuser_name, lscategory_name, lscomplaint_refno, lsraised_date;
        string lsmodulereportingto_gid;
        // Category List//

        public bool DaGetCategory(category objraiseticket)
        {
           try
            {
                msSQL = " select category_gid, concat(category_code,'/', category_name) as category_name " +
                   " from its_mst_tcategory where type_flag = 'User'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_category = new List<category_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_category.Add(new category_list
                        {
                            category_gid = (dr_datarow["category_gid"].ToString()),
                            category_name = (dr_datarow["category_name"].ToString()),
                        });
                    }
                    objraiseticket.category_list = get_category;
                }
                dt_datatable.Dispose();
                objraiseticket.status = true;
                objraiseticket.message = "Category Done";
               
                return true;
            }
            catch
            {
                objraiseticket.status = false;
                return false;
            }
        }

        // Subcategory List //

        public bool DaGetSubCategory(string category_gid,string employee_gid, subcategory objsubcategory)
        {
            try
            {

                msSQL = " SELECT subcategory_gid,subcategory_name from its_mst_tsubcategory where category_gid='" + category_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_subcategory = new List<subcategory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_subcategory.Add(new subcategory_list
                        {
                            subcategory_gid = (dr_datarow["subcategory_gid"].ToString()),
                            subcategory_name = (dr_datarow["subcategory_name"].ToString())
                        });
                    }
                    objsubcategory.subcategory_list = get_subcategory;
                }
                dt_datatable.Dispose();
                msSQL = " select category_code, category_name,campaign_title,manager_name,approval_name,approval_flag " +
                        " from its_mst_tcategory where category_gid='" + category_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    if (objODBCDatareader["approval_flag"].ToString() == "Y")

                    {
                        //objODBCDatareader.Close()
                        objsubcategory.department_approval = "Approval";
                        objODBCDatareader.Close();

                        //msSQL = " select distinct c.employeereporting_to,concat(a.user_firstname,' ',a.user_lastname) as user_name from adm_mst_tuser a " +
                        //        " left join hrm_mst_temployee b on b.user_gid = a.user_gid " +
                        //         " left join adm_mst_tmodule2employee c on b.employee_gid = c.employeereporting_to where c.module_gid='ITS' and c.employee_gid ='" + employee_gid + "'";
                        //objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                        //if (objODBCDatareader1.HasRows == true)
                        //{
                        //    objODBCDatareader1.Read();
                        //    objsubcategory.department_approval = objODBCDatareader1["user_name"].ToString();
                        //}
                        //objODBCDatareader1.Close();
                    }
                    else
                    {
                        objsubcategory.department_approval = "No Approval";
                        objODBCDatareader.Close();
                    }
                    msSQL = " select category_code, category_name,campaign_title,manager_name,approval_name,approval_flag " +
                     " from its_mst_tcategory where category_gid='" + category_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    objODBCDatareader.Read();
                    if (objODBCDatareader["approval_name"].ToString() == "null")
                    {
                        objsubcategory.service_approval = "No Approval";
                    }
                    else
                    {
                        objsubcategory.service_approval = objODBCDatareader["approval_name"].ToString();
                    }
                 
                    objsubcategory.approval_flag = objODBCDatareader["approval_flag"].ToString();
                    objsubcategory.management_approval = objODBCDatareader["manager_name"].ToString();
                    objODBCDatareader.Close();
                }
             
                    objODBCDatareader.Close();
             
               
                objsubcategory.status = true;
                return true;
            }
            catch
            {
                objsubcategory.status = false;
                return false;
            }
        }

        // Type List //

        public bool Gettype_da(string subcategory_gid, type objtype)
        {
            try
            {
                msSQL = " SELECT type_gid,type_name from its_mst_ttype where subcategory_gid='" + subcategory_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_type = new List<type_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_type.Add(new type_list
                        {
                            type_gid = (dr_datarow["type_gid"].ToString()),
                            type_name = (dr_datarow["type_name"].ToString())
                        });
                    }
                    objtype.type_list = get_type;
                }
                dt_datatable.Dispose();
                objtype.status = true;
                return true;
            }
            catch
            {
                objtype.status = false;
                return false;
            }
        }

        // Employee List //
        public bool DaGetEmployee(employee objemployee)
        {
          try
            {
                msSQL = " SELECT a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,'/',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                  " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                  " where user_status<>'N' order by employee_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_employee.Add(new employee_list
                        {
                            employee_gid = (dr_datarow["user_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            employee_id = (dr_datarow["employee_gid"].ToString())
                        });
                    }
                    objemployee.employee_list = get_employee;
                }
                dt_datatable.Dispose();
                objemployee.status = true;
                return true;
            }
            catch
            {
                objemployee.status = false;
                return false;
            }
        }


        // Get employee Details based on session(user_gid) //

        public bool DaEmployeeContactDetails(string employee_gid, employeecontactlist objvalues, string category_gid)
        {
          try
            {
                msSQL = "select modulereportingto_gid from adm_mst_tcompany ";
                lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);                
                msSQL = " select concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as user_name,a.employeereporting_to from adm_mst_tmodule2employee a " +
                        " left join hrm_mst_temployee f on f.employee_gid = a.employeereporting_to " +
                        " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                        " where  a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                        "  (select modulereportingto_gid from adm_mst_tcompany)) and g.user_status = 'Y' and a.employee_gid ='" + employee_gid + "' " +
                        "  group by a.employee_gid ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    msSQL = " SELECT employee_mobileno,employee_emailid from hrm_mst_temployee where employee_gid= '" + employee_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        objvalues.employee_emailid = objODBCDatareader["employee_emailid"].ToString();
                        objvalues.employee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = " select category_code, category_name,campaign_title,manager_name,approval_name,approval_flag " +
                            " from its_mst_tcategory where category_gid='" + category_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        if (objODBCDatareader["approval_flag"].ToString() == "Y")

                        {
                            objODBCDatareader.Close();
                            msSQL = " Select concat(a.user_firstname,' ',a.user_lastname) as user_name from adm_mst_tuser a " +
                                     " left join hrm_mst_temployee b on a.user_gid = b.user_gid" +
                                      " left join adm_mst_tsubmodule c on c.employee_gid = b.employee_gid  where c.module_gid = 'ITS' and c.employee_gid ='" + employee_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                objvalues.department_approval = objODBCDatareader["user_name"].ToString();
                                objODBCDatareader.Close();
                            }
                            else
                            {
                                objODBCDatareader.Close();
                                msSQL = "select modulereportingto_gid from adm_mst_tcompany ";
                                lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);
                                msSQL = " select distinct c.employeereporting_to,concat(a.user_firstname,' ',a.user_lastname) as user_name from adm_mst_tuser a " +
                                        " left join hrm_mst_temployee b on b.user_gid = a.user_gid " +
                                        " left join adm_mst_tmodule2employee c on b.employee_gid = c.employeereporting_to " +
                                        " left join hrm_mst_temployee d on d.employee_gid=c.employee_gid  " +
                                        " left join adm_mst_tprivilege e on e.user_gid=d.user_gid " +
                                        " where e.module_gid='" + lsmodulereportingto_gid + "' and c.employee_gid = '" + employee_gid + "' group by e.user_gid";
                                msSQL = " select concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as user_name,a.employeereporting_to from adm_mst_tmodule2employee a " +
                                        " left join hrm_mst_temployee f on f.employee_gid = a.employeereporting_to " +
                                        " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                                        " where  a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                                        "  (select modulereportingto_gid from adm_mst_tcompany)) and g.user_status = 'Y' and a.employee_gid ='" + employee_gid + "' " +
                                        "  group by a.employee_gid ";
                                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader1.HasRows == true)
                                {
                                    objODBCDatareader1.Read();
                                    objvalues.department_approval = objODBCDatareader1["user_name"].ToString();
                                }
                                objODBCDatareader1.Close();
                            }
                        }
                        else
                        {
                            objvalues.department_approval = "No Approval";
                            objODBCDatareader.Close();
                        }
                        msSQL = " select category_code, category_name,campaign_title,manager_name,approval_name,approval_flag " +
                         " from its_mst_tcategory where category_gid='" + category_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        objODBCDatareader.Read();
                        if (objODBCDatareader["approval_name"].ToString() == "null")
                        {
                            objvalues.service_approval = "No Approval";
                        }
                        else
                        {
                            objvalues.service_approval = objODBCDatareader["approval_name"].ToString();
                        }
                        objvalues.approval_flag = objODBCDatareader["approval_flag"].ToString();
                        objvalues.management_approval = objODBCDatareader["manager_name"].ToString();
                    }
                    objODBCDatareader.Close();
                    objvalues.status = true;
                    return true;

                }

                else
                {
                     objODBCDatareader.Close();
                    objvalues.status = false;
                    return false;
                }
            }
            catch
            {
                objvalues.status = false;
                return false;
            }
        }

        // Get employee Details based on get user_gid //

        public bool DaGetEmployeeContactDetails(string employee_gid, employeecontact_getgid objemployee_get,string category_gid)
        {
          try
            {

                msSQL = "select employee_gid from hrm_mst_temployee where user_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objemployee_get.employee = objODBCDatareader["employee_gid"].ToString();

                }
                objODBCDatareader.Close();
                msSQL = "select modulereportingto_gid from adm_mst_tcompany ";
                lsmodulereportingto_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select a.employeereporting_to from adm_mst_tmodule2employee a " +
                        " left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
                        " left join adm_mst_tprivilege c on c.user_gid = b.user_gid " +
                        " where c.module_gid = '" + lsmodulereportingto_gid + "' and employee_gid = '" + objemployee_get.employee + "' group by c.user_gid";
                msSQL = " select concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as user_name,a.employeereporting_to from adm_mst_tmodule2employee a " +
                        " left join hrm_mst_temployee f on f.employee_gid = a.employeereporting_to " +
                        " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                        " where  a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                        "  (select modulereportingto_gid from adm_mst_tcompany)) and g.user_status = 'Y' and a.employee_gid ='" + objemployee_get.employee + "' " +
                        "  group by a.employee_gid ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Close();
                    msSQL = " SELECT employee_mobileno,employee_emailid from hrm_mst_temployee where user_gid= '" + employee_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        objemployee_get.employee_emailid = objODBCDatareader["employee_emailid"].ToString();
                        objemployee_get.employee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = " select category_code, category_name,campaign_title,manager_name,approval_name,approval_flag " +
                            " from its_mst_tcategory where category_gid='" + category_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        if (objODBCDatareader["approval_flag"].ToString() == "Y")

                        {
                            objODBCDatareader.Close();
                            msSQL = " select concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as user_name,a.employeereporting_to from adm_mst_tmodule2employee a " +
                                    " left join hrm_mst_temployee f on f.employee_gid = a.employeereporting_to " +
                                    " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                                    " where  a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                                    "  (select modulereportingto_gid from adm_mst_tcompany)) and g.user_status = 'Y' and a.employee_gid ='" + objemployee_get.employee + "' " +
                                    "  group by a.employee_gid ";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                objemployee_get.department_approval = objODBCDatareader["user_name"].ToString();
                                objODBCDatareader.Close();
                            }
                            else
                            {
                                objODBCDatareader.Close(); 
								msSQL = " select concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as user_name,a.employeereporting_to from adm_mst_tmodule2employee a " +
                                		" left join hrm_mst_temployee f on f.employee_gid = a.employeereporting_to " +
                                		" left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                                		" where  a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                                		"  (select modulereportingto_gid from adm_mst_tcompany)) and g.user_status = 'Y' and a.employee_gid ='" + objemployee_get.employee + "' " +
                                		"  group by a.employee_gid ";
                                if (objODBCDatareader1.HasRows == true)
                                {
                                    objODBCDatareader1.Read();
                                    objemployee_get.department_approval = objODBCDatareader1["user_name"].ToString();
                                }
                                objODBCDatareader1.Close();
                            }
                        }
                        else
                        {
                            objemployee_get.department_approval = "No Approval";
                            objODBCDatareader.Close();
                        }
                        msSQL = " select category_code, category_name,campaign_title,manager_name,approval_name,approval_flag " +
                         " from its_mst_tcategory where category_gid='" + category_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        objODBCDatareader.Read();
                        if (objODBCDatareader["approval_name"].ToString() == "null")
                        {
                            objemployee_get.service_approval = "No Approval";
                        }
                        else
                        {
                            objemployee_get.service_approval = objODBCDatareader["approval_name"].ToString();
                        }
                        objemployee_get.approval_flag = objODBCDatareader["approval_flag"].ToString();
                        objemployee_get.management_approval = objODBCDatareader["manager_name"].ToString();
                    }
                    objODBCDatareader.Close();
                    objemployee_get.status = true;
                    return true;
                }
                else
                {
                    objODBCDatareader.Close();
                    objemployee_get.department_approval = " ";
                    objemployee_get.status = false;
                    return false;
                }
            }
            catch
            {
                objemployee_get.department_approval = " ";
                objemployee_get.status = false;
                return false;
            }     
        }

        // Document Delete //
       

        public bool DaGetDocumentCancel(string id, documentcancel objdocumentcancel)
        {
         try
            {
                msSQL = " delete from its_tmp_tticketdocument where id= '" + id + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                objdocumentcancel.status = true;
                objdocumentcancel.message = "Document Deleted Successfully";
           
                return true;
            }
            catch
            {
                objdocumentcancel.status = false;
                objdocumentcancel.message = "Error Occured";
                return false;
            }
        }


        public bool DaDocumentClear(string user_gid, cleartmpdocument  objtmpclearDocument )
        {
           try
            {

                msSQL = "delete from its_tmp_tticketdocument where created_by='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    objtmpclearDocument.status = true;
                    objtmpclearDocument.message = "Document Cleared Successfully!";
                    return true;
                }
                else
                {
                    objtmpclearDocument.status = false;
                    objtmpclearDocument.message = "Error Occured";
                    return false;
                }
            }
            catch
            {
                objtmpclearDocument.status = false;
                objtmpclearDocument.message = "Error Occured";
                return false;
            }
        }

        //  Raise Ticket Submit //

        public bool DaPostRaiseTicketInsert(raisesubmit values, string employee_gid,string user_gid)
        {
            try
            {

                string fileup = null;
                string msGetGID;

                string mscomplaingGid = objcmnfunctions.GetMasterGID("SMCOM");
                string ls_referenceno = objcmnfunctions.GetMasterGID("TK");

                if (values.raisedfor == "Self")
                {
                    lsraisedemployee = employee_gid;
                    msSQL = " select b.department_name from hrm_mst_tdepartment b " +
                            " left join hrm_mst_temployee a on a.department_gid = b.department_gid " +
                            " where a.employee_gid ='" + employee_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsdepartment_name = objODBCDatareader["department_name"].ToString();
                    }
                    objODBCDatareader.Close();
                    values.raisedemployee = "";
                }
                else
                {

                    msSQL = "select employee_gid from hrm_mst_temployee where user_gid='" + values.raisedemployee + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsraisedemployee = objODBCDatareader["employee_gid"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = "select employee_gid from adm_mst_tsubmodule where employee_gid='" + lsraisedemployee + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsdepartment_manager = objODBCDatareader["employee_gid"].ToString();
                        objODBCDatareader.Close();
                    }

                    else

                    {
                        objODBCDatareader.Close();
                        msSQL = " select concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as user_name,a.employeereporting_to from adm_mst_tmodule2employee a " +
                                " left join hrm_mst_temployee f on f.employee_gid = a.employeereporting_to " +
                                " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                                " where  a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                                "  (select modulereportingto_gid from adm_mst_tcompany)) and g.user_status = 'Y' and a.employee_gid ='" + lsraisedemployee + "' " +
                                "  group by a.employee_gid ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {

                            lsdepartment_manager = objODBCDatareader["employeereporting_to"].ToString();
                            objODBCDatareader.Close();
                        }
                        else
                        {
                            return false;
                        }


                    }
                    msSQL = " select b.department_name from hrm_mst_tdepartment b " +
                            " left join hrm_mst_temployee a on a.department_gid = b.department_gid " +
                            " where a.employee_gid ='" + lsraisedemployee + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsdepartment_name = objODBCDatareader["department_name"].ToString();
                    }
                    objODBCDatareader.Close();
                }

                msSQL = " select campaign_gid from its_mst_tcategory where category_gid='" + values.category_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    ls_campaigngid = objODBCDatareader["campaign_gid"].ToString();
                }
                objODBCDatareader.Close();
                if (values.txt_remarks == null)
                {

                    values.txt_remarks = "";
                }
                values.txt_title = values.txt_title.Replace("<", " ");
                values.txt_title = values.txt_title.Replace(">", " ");
                values.txt_remarks = values.txt_remarks.Replace("<", " ");
                values.txt_remarks = values.txt_remarks.Replace(">", " ");
                msSQL = " insert  into its_trn_tcomplaint (" +
                       " complaint_gid ," +
                       " complaint_date ," +
                       " customer_contactno," +
                       " customer_email, " +
                       " complaint_remarks, " +
                       " complaint_refno, " +
                       " complaint_title, " +
                       " created_by, " +
                       " created_date, " +
                       " raised_for, " +
                       " raisedfor_employee, " +
                       " campaign_gid," +
                       " department_name," +
                       " category_gid, " +
                       " subcategory_gid, " +
                       " type_gid)" +
                       " values(" +
                       "'" + mscomplaingGid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + values.txtcontact_number + "'," +
                        "'" + values.txtemail_address + "'," +
                        "'" + values.txt_remarks.Replace("\'", "") + "'," +
                        "'" + ls_referenceno + "'," +
                        "'" + values.txt_title.Replace("\'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + values.raisedfor + "'," +
                        "'" + lsraisedemployee + "'," +
                        "'" + ls_campaigngid + "'," +
                        "'" + lsdepartment_name + "'," +
                        "'" + values.category_gid + "'," +
                        "'" + values.subcategory_gid + "'," +
                        "'" + values.type_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select * from its_tmp_tticketdocument where created_by='" + user_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msGetGID = objcmnfunctions.GetMasterGID("TKDC");
                    msSQL = " insert into its_trn_tticketdocument( " +
                            " ticketdocument_gid," +
                            " complaint_gid," +
                            " document_path," +
                            " document_name," +
                            " created_by," +
                            " created_date" +
                            " )values(" +
                            " '" + msGetGID + "'," +
                            " '" + mscomplaingGid + "'," +
                            " '" + (dt["path"]) + "'," +
                            " '" + (dt["file_name"]) + "'," +
                            " '" + user_gid + "'," +
                            " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                dt_datatable.Dispose();
                msSQL = "delete from its_tmp_tticketdocument where created_by = '" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                string mscasemgrGID = objcmnfunctions.GetMasterGID("TKCT");
                msSQL = " insert into its_trn_tcasemanager " +
                         " (casemanager_gid, " +
                         " case_no," +
                         " case_title," +
                         " case_detail," +
                         " status, " +
                         " created_by, " +
                         " created_date," +
                         " ticket_from, " +
                         " raised_by, " +
                         " mobile_no, " +
                         " email, " +
                         " company_code, " +
                         " attachment, " +
                         " case_source, " +
                         " user_remarks)" +
                         " values(" +
                        " '" + mscasemgrGID + "'," +
                        " '" + ls_referenceno + "'," +
                        " '" + values.txt_title.Replace("\'", "") + "'," +
                        " '" + values.txt_remarks.Replace("\'", "") + "'," +
                        " 'New'," +
                        " '" + user_gid + "'," +
                        " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        " '" + employee_gid + "'," +
                        " '" + employee_gid + "'," +
                        " '" + values.txtcontact_number + "'," +
                        " '" + values.txtemail_address + "'," +
                        " 'Samunnati'," +
                        " '" + fileup + "', " +
                        " 'SUPPORT'," +
                        "'" + values.txt_remarks.Replace("\'", "") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    string mscaseLOG_GID = objcmnfunctions.GetMasterGID("TKLG");
                    msSQL = " insert into its_trn_tcasemanagerlog ( " +
                    " casemanagerlog_gid ," +
                      " casemanager_gid , " +
                      " case_title , " +
                      " case_detail , " +
                      " case_raiseddate , " +
                      " case_raisedby , " +
                      " case_status , " +
                      " user_remarks , " +
                      " created_by , " +
                      " ticket_from , " +
                      " mobile_no , " +
                      " email , " +
                      " case_no , " +
                      " attachment , " +
                      " created_date ) " +
                      " values(" +
                      " '" + mscaseLOG_GID + "'," +
                      " '" + mscasemgrGID + "'," +
                      " '" + values.txt_title.Replace("\'", "") + "'," +
                      " '" + values.txt_remarks.Replace("\'", "") + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                      " '" + employee_gid + "'," +
                      " 'New'," +
                      " '" + values.txt_remarks.Replace("\'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + employee_gid + "'," +
                      " '" + values.txtcontact_number + "'," +
                      " '" + values.txtemail_address + "'," +
                      " '" + ls_referenceno + "'," +
                      " '" + fileup + "', " +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

                string msgetlead2campaign_gid;
                string msgetserviceapproval_gid;
                msSQL = " select * from its_mst_tcategory where category_gid = '" + values.category_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    lsapproval_flag = objODBCDatareader["approval_flag"].ToString();
                }
                objODBCDatareader.Close();



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
                    objODBCDatareader.Close();
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
                }
                objODBCDatareader.Close();
                if(lsemployee_gid==null||lsemployee_gid =="")
                {                    
                        lscampaign_gid = objdbconn.GetExecuteScalar("select campaign_gid from its_mst_tcategory where category_gid='" + values.category_gid + "'");

                        msSQL = " select count(*),employee_gid from  its_trn_tcomplaint2employee a " +
                                " left join its_trn_tcomplaint2campaign b on a.employee_gid = b.assign_to" +
                                " where a.campaign_gid = '" + lscampaign_gid + "' and category_gid = '" + values.category_gid + "'" +
                                " and leadstage_gid   in ('1','2','Pending...')group by employee_gid  ORDER BY COUNT(*) ASC limit 0,1";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsemployee_gid = objODBCDatareader["employee_gid"].ToString();
                            objODBCDatareader.Close();
                        }
                        objODBCDatareader.Close();
                   

                }
                if (lsapproval_flag == "N")
                {
                    msgetlead2campaign_gid = objcmnfunctions.GetMasterGID("BLCC");
                    msSQL = " Insert into its_trn_tcomplaint2campaign ( " +
                            " complaint2campaign_gid, " +
                            " complaint_gid, " +
                            " campaign_gid, " +
                            " category_gid, " +
                            " created_by, " +
                            " created_date, " +
                            " lead_status, " +
                             " leadstage_gid, " +
                            " internal_notes, " +
                            " ticket_approval," +
                            " assign_to ) " +
                            " Values ( " +
                            "'" + msgetlead2campaign_gid + "'," +
                            "'" + mscomplaingGid + "'," +
                            "'" + lscampaign_gid + "'," +
                            "'" + values.category_gid + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'Open'," +
                             "'Pending...'," +
                            "'" + lscampaign_description + "'," +
                            "'N'," +
                            "'" + lsemployee_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult == 1)
                    {
                        msSQL = " update its_trn_tcomplaint Set " +
                               " assign_status = 'Pending...' " +
                               " where complaint_gid = '" + mscomplaingGid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " Update its_trn_tcasemanager set " +
                                " assigned_to='" + lsemployee_gid + "' where task_gid='" + mscomplaingGid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            objODBCDatareader.Read();
                            frommail_id = objODBCDatareader["company_mail"].ToString();
                            ls_server = objODBCDatareader["pop_server"].ToString();
                            ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                            ls_username = objODBCDatareader["pop_username"].ToString();
                            ls_password = objODBCDatareader["pop_password"].ToString();



                        }
                        objODBCDatareader.Close();

                        msSQL = "select concat(user_code, '// ' ,user_firstname, ' ', user_lastname) as user_name , category_name,complaint_refno,date_format(complaint_date, '%d-%m-%Y %h:%i %p') as raised_date " +
                               " from its_trn_tcomplaint a" +
                               " left join its_mst_tcategory b on a.category_gid=b.category_gid " +
                               " left join hrm_mst_temployee c on c.employee_gid=a.created_by " +
                               " left join adm_mst_tuser d on d.user_gid=c.user_gid " +
                               " where complaint_gid='" + mscomplaingGid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows)
                        {
                            lsuser_name = objODBCDatareader["user_name"].ToString();
                            lscategory_name = objODBCDatareader["category_name"].ToString();
                            lscomplaint_refno = objODBCDatareader["complaint_refno"].ToString();
                            lsraised_date = objODBCDatareader["raised_date"].ToString();
                            objODBCDatareader.Close();

                        }
                        msSQL = "select tomailid from ocs_trn_ttomaillist where mailtrigger_function='New Ticket'";
                        tomail_id = objdbconn.GetExecuteScalar(msSQL);
                        //tomail_id = "itsupport@samunnati.com";

                        sub = " New Ticket -  " + HttpUtility.HtmlEncode(lscomplaint_refno) + '/' + lsraised_date + " ";


                        body = "Dear Sir/Madam,  <br />";
                        body = body + "<br />";
                        body = body + "Greetings,  <br />";
                        body = body + "<br />";
                        body = body + " <b> " + HttpUtility.HtmlEncode(lsuser_name) + " </b> has raised Ticket in IT-Ticketing. Kindly do the needful.,<br />";
                        body = body + "<br />";
                        body = body + "<b>Ticket Ref number :</b> " + HttpUtility.HtmlEncode(lscomplaint_refno) + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Raised Date :</b> " + lsraised_date + "<br />";
                        body = body + "<br />";
                        body = body + "<b>Category :</b> " + HttpUtility.HtmlEncode(lscategory_name) + "<br />";
                        body = body + "<br />";

                        body = body + "Yours Sincerely,  ";
                        body = body + "<br />";
                        body = body + " Samunnati Financial Intermediation & Services Pvt Ltd ";
                        body = body + "<br />";

                        body = body + "<b> Please Note:  </b> This is an auto generated e-mail that cannot receive replies... ";
                        body = body + "<br />";


                        MailMessage message1 = new MailMessage();
                        SmtpClient smtp1 = new SmtpClient();
                        message1.From = new MailAddress(ls_username);
                        message1.To.Add(new MailAddress(tomail_id));



                        message1.Subject = sub;
                        message1.IsBodyHtml = true; //to make message body as html  
                        message1.Body = body;
                        smtp1.Port = ls_port;
                        smtp1.Host = ls_server; //for gmail host  
                        smtp1.EnableSsl = true;
                        smtp1.UseDefaultCredentials = false;
                        smtp1.Credentials = new NetworkCredential(ls_username, ls_password);
                        smtp1.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp1.Send(message1);

                        values.status = true;
                        values.message = "Ticket Raised Successfully!";
                        return true;
                    }
                    else
                    {
                        values.status = false;
                        return false;
                    }
                }
                else
                {

                    if (values.raisedfor == "Others")
                    {
                        msSQL = " select employee_gid from hrm_mst_temployee a " +
                                " left join adm_mst_tuser b on a.user_gid = b.user_gid " +
                                " where b.user_gid = '" + values.raisedemployee + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            objODBCDatareader.Read();
                            lsotheremployee_gid = objODBCDatareader["employee_gid"].ToString();
                        }
                        objODBCDatareader.Close();

                        msSQL = "select employee_gid from adm_mst_tsubmodule where employee_gid='" + lsotheremployee_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsdepartment_manager = objODBCDatareader["employee_gid"].ToString();
                            objODBCDatareader.Close();
                        }

                        else

                        {
                            objODBCDatareader.Close();
                            msSQL = " select concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as user_name,a.employeereporting_to from adm_mst_tmodule2employee a " +
                                    " left join hrm_mst_temployee f on f.employee_gid = a.employeereporting_to " +
                                    " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                                    " where  a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                                    "  (select modulereportingto_gid from adm_mst_tcompany)) and g.user_status = 'Y' and a.employee_gid ='" + lsotheremployee_gid + "' " +
                                    "  group by a.employee_gid ";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {

                                lsdepartment_manager = objODBCDatareader["employeereporting_to"].ToString();
                                objODBCDatareader.Close();
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    if (values.raisedfor == "Self")
                    {
                        msSQL = " Select employee_gid from adm_mst_tsubmodule where employee_gid ='" + employee_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lsdepartment_manager = objODBCDatareader["employee_gid"].ToString();
                            objODBCDatareader.Close();
                        }
                        else
                        {
                            objODBCDatareader.Close();
                            

                            msSQL = " select concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as user_name,a.employeereporting_to from adm_mst_tmodule2employee a " +
                                    " left join hrm_mst_temployee f on f.employee_gid = a.employeereporting_to " +
                                    " left join adm_mst_tuser g on g.user_gid = f.user_gid " +
                                    " where  a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                                    "  (select modulereportingto_gid from adm_mst_tcompany)) and g.user_status = 'Y' and a.employee_gid ='" + employee_gid + "' " +
                                    "  group by a.employee_gid ";

                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {

                                lsdepartment_manager = objODBCDatareader["employeereporting_to"].ToString();
                            }
                            objODBCDatareader.Close();
                        }
                        objODBCDatareader.Close();
                    }


                    msSQL = " select approval_gid,manager_gid,manager_name,internal_approval from its_mst_tcategory " +
                            " where category_gid='" + values.category_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsservice_manager = objODBCDatareader["approval_gid"].ToString();
                        lsmanager_gid = objODBCDatareader["manager_gid"].ToString();
                        lsmanager_name = objODBCDatareader["manager_name"].ToString();
                        lsinternal_approval = objODBCDatareader["internal_approval"].ToString();

                    }
                    objODBCDatareader.Close();

                    msSQL = " update its_trn_tcomplaint Set " +
                            " assign_status = 'Pending...' " +
                            " where complaint_gid = '" + mscomplaingGid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msgetserviceapproval_gid = objcmnfunctions.GetMasterGID("C2EM");

                    msSQL = " Insert into its_trn_tserviceapproval ( " +
                       " serviceapproval_gid, " +
                       " complaint_gid, " +
                       " complain_assignto, " +
                       " department_approval, " +
                       " service_approval, " +
                       " internal_approval, " +
                       " status, " +
                       " manager_gid, " +
                       " manager_name," +
                       " dept_manager, " +
                       " ticket_approval," +
                       " service_manager ) " +
                       " Values ( " +
                       "'" + msgetserviceapproval_gid + "'," +
                       "'" + mscomplaingGid + "'," +
                       "'" + lsemployee_gid + "'," +
                       "'Y'," +
                       "'Y'," +
                       "'" + lsinternal_approval + "'," +
                       "'Waiting For Approval...'," +
                       "'" + lsmanager_gid + "'," +
                       "'" + lsmanager_name + "'," +
                       "'" + lsdepartment_manager + "'," +
                       "'N'," +
                       "'" + lsservice_manager + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

              
                /*-----------------------------------------------Department- Autoapprove--------------------------------------------------------------------------------------*/

                if (lsdepartment_manager == employee_gid || lsdepartment_manager == lsotheremployee_gid)
                {
                    msSQL = " select approval_gid,manager_gid,manager_name from its_mst_tcategory " +
                               " where category_gid='" + values.category_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows)
                    {
                        lsmanager_gid = objODBCDatareader["manager_gid"].ToString();
                        lsmanager_name = objODBCDatareader["manager_name"].ToString();
                        objODBCDatareader.Close();
                    }
                    if (lsmanager_name == "No Manager Approval")
                    {
                        msSQL = " update its_trn_tserviceapproval set " +
                                    " department_approval = 'C', " +
                                    " status = 'Waiting For Approval' " +
                                    " where serviceapproval_gid = '" + msgetserviceapproval_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        msSQL = " update its_trn_tserviceapproval set " +
                                " department_approval = 'M', " +
                                " status = 'Waiting For Approval' " +
                                " where serviceapproval_gid = '" + msgetserviceapproval_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                /*-----------------------------------End------------Department- Autoapprove-----------------------------------------------------------------------------*/

                /*-----------------------------------------------Management- Autoapprove--------------------------------------------------------------------------------------*/

                if ((lsdepartment_manager == employee_gid && lsmanager_name == employee_gid) ||
                    (lsdepartment_manager == lsotheremployee_gid && lsmanager_name == lsotheremployee_gid))
                {

                    msSQL = " update its_trn_tserviceapproval set " +
                            " department_approval = 'C', " +
                            " status = 'Waiting For Approval' " +
                            " where serviceapproval_gid = '" + msgetserviceapproval_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }


                /*--------------------------------------End---------Management- Autoapprove--------------------------------------------------------------------------------------*/


                /*-----------------------------------------------Service- Autoapprove--------------------------------------------------------------------------------------*/
                if ((lsdepartment_manager == employee_gid && lsmanager_name == "No Manager Approval" && lsservice_manager == employee_gid)
                || (lsdepartment_manager == lsotheremployee_gid && lsmanager_name == "No Manager Approval" && lsservice_manager == lsotheremployee_gid))
                {
                    popserviceapproval(msgetserviceapproval_gid, values.category_gid, employee_gid, mscomplaingGid);


                }
                if ((lsdepartment_manager == employee_gid && lsmanager_gid == employee_gid && lsservice_manager == employee_gid)
                   || (lsdepartment_manager == lsotheremployee_gid && lsmanager_gid == lsotheremployee_gid && lsservice_manager == lsotheremployee_gid))
                {
                    popserviceapproval(msgetserviceapproval_gid, values.category_gid, employee_gid, mscomplaingGid);


                }
                /*------------------------------------End-----------Service- Autoapprove--------------------------------------------------------------------------------------*/



               

                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    frommail_id = objODBCDatareader["company_mail"].ToString();
                    ls_server = objODBCDatareader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                    ls_username = objODBCDatareader["pop_username"].ToString();
                    ls_password = objODBCDatareader["pop_password"].ToString();



                }
                objODBCDatareader.Close();

                msSQL = "select concat(user_firstname, ' ', user_lastname) as user_name , category_name,complaint_refno,date_format(complaint_date, '%d-%m-%Y %h:%i %p') as raised_date " +
                       " from its_trn_tcomplaint a" +
                       " left join its_mst_tcategory b on a.category_gid=b.category_gid " +
                       " left join hrm_mst_temployee c on c.employee_gid=a.created_by " +
                       " left join adm_mst_tuser d on d.user_gid=c.user_gid " +
                       " where complaint_gid='" + mscomplaingGid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    lsuser_name = objODBCDatareader["user_name"].ToString();
                    lscategory_name = objODBCDatareader["category_name"].ToString();
                    lscomplaint_refno = objODBCDatareader["complaint_refno"].ToString();
                    lsraised_date = objODBCDatareader["raised_date"].ToString();
                    objODBCDatareader.Close();

                }

                msSQL = "select tomailid from ocs_trn_ttomaillist where mailtrigger_function='New Ticket'";
                tomail_id = objdbconn.GetExecuteScalar(msSQL);

                sub = " New Ticket -  " + HttpUtility.HtmlEncode(lscomplaint_refno) + '/' + lsraised_date + " ";


                body = "Dear Sir/Madam,  <br />";
                body = body + "<br />";
                body = body + "Greetings,  <br />";
                body = body + "<br />";
                body = body + " <b> " + HttpUtility.HtmlEncode(lsuser_name) + " </b> has raised Ticket in IT-Ticketing. Kindly do the needful.,<br />";
                body = body + "<br />";
                body = body + "<b>Ticket Ref number :</b> " + HttpUtility.HtmlEncode(lscomplaint_refno) + "<br />";
                body = body + "<br />";
                body = body + "<b>Raised Date :</b> " + lsraised_date + "<br />";
                body = body + "<br />";
                body = body + "<b>Category :</b> " + HttpUtility.HtmlEncode(lscategory_name) + "<br />";
                body = body + "<br />";

                body = body + "<b>Yours Sincerely, </b> ";
                body = body + "<br />";
                body = body + "<b> Samunnati Financial Intermediation & Services Pvt Ltd </b> ";
                body = body + "<br />";

                body = body + "<b> Please Note:  </b> This is an auto generated e-mail that cannot receive replies... ";
                body = body + "<br />";


                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                message.To.Add(new MailAddress(tomail_id));



                message.Subject = sub;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = ls_port;
                smtp.Host = ls_server; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                values.status = true;
                values.message = "Ticket Raised Successfully!";

                return true;

            }
            catch(Exception e)
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }  
        }

        public void popserviceapproval(string msgetserviceapproval_gid, string category_gid, string employee_gid, string mscomplaingGid)
        {
           
            msSQL = " update its_trn_tserviceapproval set " +
            " department_approval = 'C', " +
            " status = 'Waiting For Approval' " +
            " where serviceapproval_gid = '" + msgetserviceapproval_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " update its_trn_tcomplaint Set " +
                            " assign_status = 'Pending...' " +
                            " where complaint_gid = '" + mscomplaingGid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
           
        }

        public UploadDocumentname DaPostUploadDocument(HttpRequest httpRequest, string employee_gid, string user_gid, UploadDocumentname objfilename)
        {
            UploadDocumentModel objdocumentmodel = new UploadDocumentModel();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string document_name = httpRequest.Form["document_name"];
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = "SELECT * from adm_mst_tcompany where 1=1";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                lscompany_code = objODBCDatareader["company_code"].ToString();
            }
            objODBCDatareader.Close();
            //path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "ITS/TicketDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "ITS/TicketDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            try
            {

                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;

                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {

                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        if ((FileExtension == ".jpg") || (FileExtension == ".jpeg") || (FileExtension == ".png") || (FileExtension == ".pdf") || (FileExtension == ".tif") || (FileExtension == ".tiff") || (FileExtension == ".txt") || (FileExtension == ".doc") || (FileExtension == ".docx") || (FileExtension == ".xls") || (FileExtension == ".xlsx"))
                        {
                            lsfile_gid = lsfile_gid + FileExtension;
                            ls_readStream = httpPostedFile.InputStream;
                            ls_readStream.CopyTo(ms);

                            // Check Document validation;

                            byte[] bytes = ms.ToArray();
                            if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                            {
                                objfilename.message = "File format is not supported";
                                objfilename.status = false;
                                return objfilename;
                            }

                            //CopyStream(ms, ls_readStream);
                            //lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "ITS/TicketDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                            //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                            bool status;
                            status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "ITS/TicketDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                            ms.Close();
                            lspath = "../../erpdocument" + "/" + lscompany_code + "/" + "ITS/TicketDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;

                            msSQL = " insert into its_tmp_tticketdocument( " +
                           " path," +
                           " file_name," +
                           " created_by" +
                           " )values(" +
                           "'" + lspath + "'," +
                           "'" + httpPostedFile.FileName + "'," +
                           "'" + user_gid + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult == 1)
                            {
                                objfilename.status = true;
                            }
                            else
                            {
                                objfilename.status = false;
                            }
                        }

                    }

                    msSQL = "select file_name,id from its_tmp_tticketdocument where created_by='" + user_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var get_filename = new List<UploadDocumentModel>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            get_filename.Add(new UploadDocumentModel
                            {
                                filename = (dr_datarow["file_name"].ToString()),
                                id = (dr_datarow["id"].ToString())
                            });
                        }
                        objfilename.filename_list = get_filename;
                    }
                    dt_datatable.Dispose();
                }
            }
            catch (Exception ex)
            {
                objfilename.status = false;
            }
            return objfilename;
        }
    } 
    }

                                   