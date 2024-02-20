using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data;
using ems.hrm.Models;
using System.Data.Odbc;
using ems.utilities.Functions;
namespace ems.hrm.DataAccess
{
    public class DaMyTeam
    {
        OdbcConnection objODBCconnection;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL;
        string msGetGID;
        string lsmobilenum, lsemployee_photo;
        int mnResult;
        string lsdob, PermanentaddressGID, TemporaryaddressGID;
        cmnfunctions objcmnfunctions = new cmnfunctions();
        dbconn objdbconn = new dbconn();
        public bool DaGetTeam(string employee_gid, myteam values)
        {
            
            msSQL = " select c.user_code, concat(c.user_firstname,' ',c.user_lastname) as user_firstname, b.employee_gid,d.designation_name,b.employee_mobileno, " +
                   " e.department_name,b.employee_photo from adm_mst_tmodule2employee a " +
                   " inner join hrm_mst_temployee b on a.employee_gid = b.employee_gid " +
                   " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                   " left join adm_mst_tdesignation d on b.designation_gid = d.designation_gid " +
                   " left join hrm_mst_tdepartment e on e.department_gid=b.department_gid " +
                   " where a.employeereporting_to = '" + employee_gid + "' and module_gid = 'HRM' and a.hierarchy_level > 1 order by c.user_firstname asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_myteam = new List<myteam_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    if (dt["employee_mobileno"].ToString() == "")
                    {
                        lsmobilenum = ".";
                    }
                    else
                    {
                        lsmobilenum = dt["employee_mobileno"].ToString();
                    }
                    if (dt["employee_photo"].ToString() == "")
                    {
                        lsemployee_photo = "N";
                    }
                    else
                    {
                        lsemployee_photo = dt["employee_photo"].ToString();
                    }
                    get_myteam.Add(new myteam_list
                    {
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_code = dt["user_code"].ToString(),
                        employee_name = dt["user_firstname"].ToString(),
                        designation = dt["designation_name"].ToString(),
                        employee_photo = lsemployee_photo,
                        employee_mobileno = lsmobilenum,
                        department = dt["department_name"].ToString()
                    });
                }
                values.myteam_list = get_myteam;
            }
            dt_datatable.Dispose();
            
            return true;
        }

        public bool DaGetTeamemployeeprofile(string employee_gid, employeedetails values)
        {
            
            try
            {
                msSQL = " select a.employeereporting_to,concat(c.user_firstname,' ',c.user_lastname) as employeereporting " +
                      " from adm_mst_tmodule2employee a " +
                      " left join hrm_mst_temployee b on a.employeereporting_to = b.employee_gid " +
                      " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                      " where a.employee_gid = '" + employee_gid + "' and module_gid = 'HRM'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.employeereporting_to = objODBCDatareader["employeereporting"].ToString();
                    values.employeereporting_gid = objODBCDatareader["employeereporting_to"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " Select distinct a.employee_gid,a.employee_gender,a.user_gid,a.employee_image,a.employee_photo,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name ," +
         " a.bloodgroup,date_format(a.employee_joiningdate,'%d/%m/%Y') as employee_joingdate,i.address1,i.address2,i.city,i.state,i.postal_code,j.country_name," +
         " date_format(a.employee_dob,'%d/%m/%Y') as employee_dob,a.employee_mobileno,a.employee_qualification,c.employee_emailid, " +
         " a.employee_experience,b.user_code,b.user_firstname,b.user_lastname,b.user_status as user_status,d.branch_name," +
         " e.department_name,c.bloodgroup_name,concat(b.user_firstname,' ',b.user_lastname) as username, " +
         " f.designation_name FROM hrm_mst_temployee a " +
         " LEFT JOIN adm_mst_tuser b ON a.user_gid = b.user_gid " +
         " LEFT JOIN hrm_mst_tbranch d ON a.branch_gid = d.branch_gid" +
         " LEFT JOIN hrm_mst_tdepartment e ON a.department_gid = e.department_gid" +
         " LEFT JOIN adm_mst_tdesignation f ON a.designation_gid = f.designation_gid" +
         " LEFT JOIN hrm_mst_temployee c ON a.user_gid = c.user_gid " +
         " left join adm_mst_tentity z on z.entity_gid=c.entity_gid " +
         " LEFT JOIN adm_mst_taddress i on i.parent_gid=c.employee_gid " +
         " LEFT JOIN adm_mst_tcountry j on i.country_gid=j.country_gid " +
         " WHERE a.employee_gid = '" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.user_code = objODBCDatareader["user_code"].ToString();
                    values.user_name = objODBCDatareader["username"].ToString();
                    values.designation = objODBCDatareader["designation_name"].ToString();
                    values.department = objODBCDatareader["department_name"].ToString();
                    values.branch = objODBCDatareader["branch_name"].ToString();
                    values.joining_date = objODBCDatareader["employee_joingdate"].ToString();
                    values.first_name = objODBCDatareader["user_firstname"].ToString();
                    values.last_name = objODBCDatareader["user_lastname"].ToString();
                    values.gender = objODBCDatareader["employee_gender"].ToString();
                    values.entity = objODBCDatareader["entity_name"].ToString();
                    values.employee_emailid = objODBCDatareader["employee_emailid"].ToString();
                    lsdob = objODBCDatareader["employee_dob"].ToString();

                    if (lsdob != "")
                    {
                        values.dob = DateTime.ParseExact(lsdob, "dd/MM/yyyy", null);
                    }
                    if (objODBCDatareader["employee_mobileno"].ToString() != "")
                    {
                        values.mobile = Convert.ToDouble(objODBCDatareader["employee_mobileno"].ToString());
                    }
                    values.qualification = objODBCDatareader["employee_qualification"].ToString();
                    values.experience = objODBCDatareader["employee_experience"].ToString();
                    values.blood_group = objODBCDatareader["bloodgroup_name"].ToString();
                    if (objODBCDatareader["user_status"].ToString() == "Y")
                    {
                        values.user_status = "Active";
                    }
                    else
                    {
                        values.user_status = "InActive";
                    }
                    values.employee_gid = employee_gid;
                    if (objODBCDatareader["employee_photo"].ToString() == "")
                    {
                        values.employee_photo = "N";
                    }
                    else
                    {
                        values.employee_photo = objODBCDatareader["employee_photo"].ToString();
                    }
                }
                objODBCDatareader.Close();

                msSQL = " Select permanentaddress_gid,temporaryaddress_gid from " +
                      " hrm_trn_temployeedtl where " +
                      " employee_gid = '" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    PermanentaddressGID = objODBCDatareader["permanentaddress_gid"].ToString();
                    TemporaryaddressGID = objODBCDatareader["temporaryaddress_gid"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " Select a.address1,a.address2,a.city, " +
                        " a.postal_code,a.state,b.country_name,b.country_gid" +
                        " from adm_mst_taddress a,adm_mst_tcountry b " +
                        " where  address_gid = '" + PermanentaddressGID + "' and " +
                        " a.country_gid = b.country_gid and " +
                        " a.parent_gid = '" + employee_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {

                    values.permanent_address1 = objODBCDatareader["address1"].ToString();
                    values.permanent_address2 = objODBCDatareader["address2"].ToString();
                    values.permanent_country = objODBCDatareader["country_name"].ToString();
                    values.permanent_city = objODBCDatareader["city"].ToString();
                    values.permanent_state = objODBCDatareader["state"].ToString();
                    if (objODBCDatareader["postal_code"].ToString() != "")
                    {
                        values.permanent_postalcode = Convert.ToDouble(objODBCDatareader["postal_code"].ToString());
                    }

                }
                objODBCDatareader.Close();
                msSQL = " Select a.address1,a.address2,a.city, " +
                      " a.postal_code,a.state,b.country_name,b.country_gid" +
                      " from adm_mst_taddress a,adm_mst_tcountry b " +
                      " where  address_gid = '" + TemporaryaddressGID + "' and " +
                      " a.country_gid = b.country_gid and " +
                      " a.parent_gid = '" + employee_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    values.temporary_address1 = objODBCDatareader["address1"].ToString();
                    values.temporary_address2 = objODBCDatareader["address2"].ToString();
                    values.temporary_country = objODBCDatareader["country_name"].ToString();
                    values.temporary_city = objODBCDatareader["city"].ToString();
                    values.temporary_state = objODBCDatareader["state"].ToString();
                    if (objODBCDatareader["postal_code"].ToString() != "")
                    {
                        values.temporary_postalcode = Convert.ToDouble(objODBCDatareader["postal_code"].ToString());
                    }

                }
                objODBCDatareader.Close();
                
                return true;
            }
            catch (Exception ex)
            {
                values.message = ex.Message;
                return false;
            }


        }
    }
}
