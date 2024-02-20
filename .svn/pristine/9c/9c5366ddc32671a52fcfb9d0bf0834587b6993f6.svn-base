using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.hrm.Models;

namespace ems.hrm.DataAccess
{
    public class DaMyProfile
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcConnection objODBCconnection;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL;
        int mnResult;
        HttpRequest httpRequest;
        HttpPostedFile httpPostedFile;
        string lscompany_code, lsemployee_photo;
        string lschang_fields;
        string msGetGid;
        string encripedpassword;
        string PermanentaddressGID;
        string TemporaryaddressGID;
        string lsdob, lspath;
        string lsfirst_name, lslast_name, lsemployee_gender, lsemployee_qualification, lsemployee_dob,
            lsemployee_mobileno, lsemployee_personalno, lsemployee_experiencedtl, lsbloodgroup;
        DateTime employee_dob;

        public void DaGetCountry(countryname objcountryname)
        {
            
            msSQL = "select country_gid,country_name from adm_mst_tcountry";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_country = new List<countryname_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    get_country.Add(new countryname_list
                    {
                        country_gid = dt["country_gid"].ToString(),
                        country_name = dt["country_name"].ToString()
                    });
                }
                objcountryname.countryname_list = get_country;
            }

            dt_datatable.Dispose();

            objcountryname.status = true;
            objcountryname.message = "Data Fetched";
        }

        public void DaGetEmployeedetails(employeedetails values, string employee_gid)
        {
            try
            {
                
                msSQL = " Select distinct a.employee_gid,a.employee_gender,a.user_gid,a.employee_image,a.employee_photo," +
                    " a.bloodgroup,date_format(a.employee_joiningdate,'%d/%m/%Y') as employee_joingdate,i.address1,i.address2,i.city,i.state,i.postal_code,j.country_name," +
                    " date_format(a.employee_dob,'%d/%m/%Y') as employee_dob,a.employee_mobileno,a.employee_personalno,a.employee_qualification," +
                    " a.employee_experience,b.user_code,b.user_firstname,b.user_lastname,b.user_status as user_status,d.branch_name," +
                    " e.department_name,c.bloodgroup_name,concat(b.user_firstname,' ',b.user_lastname) as username, " +
                    " f.designation_name FROM hrm_mst_temployee a " +
                    " LEFT JOIN adm_mst_tuser b ON a.user_gid = b.user_gid " +
                    " LEFT JOIN hrm_mst_tbranch d ON a.branch_gid = d.branch_gid" +
                    " LEFT JOIN hrm_mst_tdepartment e ON a.department_gid = e.department_gid" +
                    " LEFT JOIN adm_mst_tdesignation f ON a.designation_gid = f.designation_gid" +
                    " LEFT JOIN hrm_mst_temployee c ON a.user_gid = c.user_gid " +
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
                    lsdob = objODBCDatareader["employee_dob"].ToString();
                    if (lsdob != "")
                    {
                        values.dob = DateTime.ParseExact(lsdob, "dd/MM/yyyy", null);
                    }
                    if (objODBCDatareader["employee_photo"].ToString() == "")
                    {
                        values.employee_photo = "N";
                    }
                    else
                    {
                        values.employee_photo = objODBCDatareader["employee_photo"].ToString();
                    }

                    if (objODBCDatareader["employee_mobileno"].ToString() != "")
                    {
                        values.mobile = Convert.ToDouble(objODBCDatareader["employee_mobileno"].ToString());
                    }
                    if (objODBCDatareader["employee_personalno"].ToString() != "")
                    {
                        values.personal_number = Convert.ToDouble(objODBCDatareader["employee_personalno"].ToString());
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
                }
                objODBCDatareader.Close();

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
                values.message = "Data Fetched";
                values.status = true;
                
            }
            catch (Exception ex)
            {
                values.message = ex.Message;
                values.status = false;
            }

        }

        public void Daupdateemployeedetails(string employee_gid, String user_gid, employeedetails values)
        {
            

            msSQL = " Select b.user_code,b.user_firstname,b.user_lastname,a.employee_qualification,a.employee_mobileno,a.employee_personalno,a.employee_dob," +
                    " a.employee_experiencedtl,a.employee_gender,a.bloodgroup from hrm_mst_temployee a " +
                " left join adm_mst_tuser b on b.user_gid=a.user_gid  where a.employee_gid='" + employee_gid + "' ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)

            {
                lsfirst_name = objODBCDatareader["user_firstname"].ToString();
                lslast_name = objODBCDatareader["user_lastname"].ToString();
                lsemployee_gender = objODBCDatareader["employee_gender"].ToString();
                lsemployee_qualification = objODBCDatareader["employee_qualification"].ToString();
                lsemployee_mobileno = objODBCDatareader["employee_mobileno"].ToString();
                lsemployee_personalno = objODBCDatareader["employee_personalno"].ToString();
                lsemployee_dob = objODBCDatareader["employee_dob"].ToString();
                if (lsemployee_dob != "")
                {
                    employee_dob = Convert.ToDateTime(lsemployee_dob);
                }
                lsemployee_experiencedtl = objODBCDatareader["employee_experiencedtl"].ToString();
                lsbloodgroup = objODBCDatareader["bloodgroup"].ToString();

                objODBCDatareader.Close();
                if (values.first_name != lsfirst_name)

                {
                    msGetGid = objcmnfunctions.GetMasterGID("HADU");
                    lschang_fields = "First Name";
                    msSQL = "Insert into  hrm_trn_tauditlog( " +
                             " auditlog_gid," +
                             " employee_gid," +
                             " changed_field," +
                             " old_value," +
                             " new_value," +
                             " created_by," +
                             " created_date)" +
                             " values(" +
                             "'" + msGetGid + "', " +
                             "'" + employee_gid + "'," +
                             "'" + lschang_fields + "'," +
                             "'" + lsfirst_name + "'," +
                             "'" + values.first_name + "'," +
                             "'" + user_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    objODBCDatareader.Close();
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (values.last_name != lslast_name)
                {
                    objODBCDatareader.Close();
                    msGetGid = objcmnfunctions.GetMasterGID("HADU");
                    lschang_fields = "Last Name";

                    msSQL = "Insert into  hrm_trn_tauditlog( " +
                             " auditlog_gid," +
                             " employee_gid," +
                             " changed_field," +
                             " old_value," +
                             " new_value," +
                             " created_by," +
                             " created_date)" +
                             " values(" +
                             "'" + msGetGid + "', " +
                             "'" + user_gid + "'," +
                             "'" + lschang_fields + "'," +
                             "'" + lslast_name + "'," +
                             "'" + values.last_name + "'," +
                             "'" + user_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    objODBCDatareader.Close();
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (values.gender != lsemployee_gender)
                {
                    objODBCDatareader.Close();
                    msGetGid = objcmnfunctions.GetMasterGID("HADU");
                    lschang_fields = "Gender";
                    msSQL = "Insert into  hrm_trn_tauditlog( " +
                             " auditlog_gid," +
                             " employee_gid," +
                             " changed_field," +
                             " old_value," +
                             " new_value," +
                             " created_by," +
                             " created_date)" +
                             " values(" +
                             "'" + msGetGid + "', " +
                             "'" + user_gid + "'," +
                             "'" + lschang_fields + "'," +
                             "'" + lsemployee_gender + "'," +
                             "'" + values.gender + "'," +
                             "'" + user_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                if (values.dob.ToString("yyyy-MM-dd") != employee_dob.ToString("yyyy-MM-dd"))
                {
                    objODBCDatareader.Close();
                    msGetGid = objcmnfunctions.GetMasterGID("HADU");
                    lschang_fields = "DOB";

                    msSQL = "Insert into  hrm_trn_tauditlog( " +
                        " auditlog_gid," +
                        " employee_gid," +
                        " changed_field," +
                        " old_value," +
                        " new_value," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "', " +
                        "'" + user_gid + "'," +
                        "'" + lschang_fields + "'," +
                        "'" + employee_dob.ToString("yyyy-MM-dd") + "'," +
                        "'" + values.dob.ToString("yyyy-MM-dd") + "'," +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }

                if (Convert.ToString(values.mobile) != lsemployee_mobileno)
                {
                    objODBCDatareader.Close();
                    msGetGid = objcmnfunctions.GetMasterGID("HADU");
                    lschang_fields = "Mobile No";
                    msSQL = "Insert into  hrm_trn_tauditlog( " +
                              " auditlog_gid," +
                              " employee_gid," +
                              " changed_field," +
                              " old_value," +
                              " new_value," +
                              " created_by," +
                              " created_date)" +
                              " values(" +
                              "'" + msGetGid + "', " +
                              "'" + user_gid + "'," +
                              "'" + lschang_fields + "'," +
                              "'" + lsemployee_mobileno + "'," +
                              "'" + values.mobile + "'," +
                              "'" + user_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (Convert.ToString(values.personal_number) != lsemployee_personalno)
                {
                    objODBCDatareader.Close();
                    msGetGid = objcmnfunctions.GetMasterGID("HADU");
                    lschang_fields = "Personal No";
                    msSQL = "Insert into  hrm_trn_tauditlog( " +
                                 " auditlog_gid," +
                                 " employee_gid," +
                                 " changed_field," +
                                 " old_value," +
                                 " new_value," +
                                 " created_by," +
                                 " created_date)" +
                                 " values(" +
                                 "'" + msGetGid + "', " +
                                 "'" + user_gid + "'," +
                                 "'" + lschang_fields + "'," +
                                 "'" + lsemployee_personalno + "'," +
                                 "'" + values.personal_number + "'," +
                                 "'" + user_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (values.qualification != lsemployee_qualification)
                {
                    objODBCDatareader.Close();
                    msGetGid = objcmnfunctions.GetMasterGID("HADU");
                    lschang_fields = "Qualification";
                    msSQL = "Insert into  hrm_trn_tauditlog( " +
                                    " auditlog_gid," +
                                    " employee_gid," +
                                    " changed_field," +
                                    " old_value," +
                                    " new_value," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "', " +
                                    "'" + user_gid + "'," +
                                    "'" + lschang_fields + "'," +
                                    "'" + lsemployee_qualification + "'," +
                                    "'" + values.qualification + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (values.blood_group != lsbloodgroup)
                {
                    objODBCDatareader.Close();
                    msGetGid = objcmnfunctions.GetMasterGID("HADU");
                    lschang_fields = "Blood Group";
                    msSQL = "Insert into  hrm_trn_tauditlog( " +
                                  " auditlog_gid," +
                                  " employee_gid," +
                                  " changed_field," +
                                  " old_value," +
                                  " new_value," +
                                  " created_by," +
                                  " created_date)" +
                                  " values(" +
                                  "'" + msGetGid + "', " +
                                  "'" + user_gid + "'," +
                                  "'" + lschang_fields + "'," +
                                  "'" + lsbloodgroup + "'," +
                                  "'" + values.blood_group + "'," +
                                  "'" + user_gid + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                if (values.experience != lsemployee_experiencedtl)
                {
                    objODBCDatareader.Close();
                    msGetGid = objcmnfunctions.GetMasterGID("HADU");
                    lschang_fields = "Experience";
                    msSQL = "Insert into  hrm_trn_tauditlog( " +
                                      " auditlog_gid," +
                                      " employee_gid," +
                                      " changed_field," +
                                      " old_value," +
                                      " new_value," +
                                      " created_by," +
                                      " created_date)" +
                                      " values(" +
                                      "'" + msGetGid + "', " +
                                      "'" + user_gid + "'," +
                                      "'" + lschang_fields + "'," +
                                      "'" + lsemployee_experiencedtl + "'," +
                                      "'" + values.experience + "'," +
                                      "'" + user_gid + "'," +
                                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            msSQL = " update adm_mst_tuser set " +
                    " user_firstname='" + values.first_name + "'," +
                    " user_lastname='" + values.last_name + "'" +
                    " where user_gid='" + user_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);




            msSQL = " update hrm_mst_temployee set " +
                                     " employee_gender='" + values.gender + "'," +
                                     " employee_dob='" + values.dob.ToString("yyyy-MM-dd") + "'," +
                                     " employee_mobileno ='" + Convert.ToDouble(values.mobile) + "'," +
                                     " employee_personalno ='" + Convert.ToDouble(values.personal_number) + "'," +
                                     " employee_qualification='" + values.qualification + "'," +
                                     " bloodgroup_name ='" + values.blood_group + "'," +
                                     " employee_experience ='" + values.experience + "'" +
                                     " where employee_gid='" + employee_gid + "' ";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if(mnResult !=0)
            {
                values.status = true;
                values.message = "Person Details Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "failure";
            }

            objODBCDatareader.Close();
            
          
        }

        public void Daupdatepassword(string employee_gid, string user_gid, updatepassword values)

        {
            
            encripedpassword = objcmnfunctions.ConvertToAscii(values.confirm_passsword);

            msSQL = " select user_password from adm_mst_tuser where user_gid ='" + user_gid + "'" +
                    " and user_password ='" + objcmnfunctions.ConvertToAscii(values.current_password) + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                msSQL = " update adm_mst_tuser set" +
                    " user_password= '" + encripedpassword + "', " +
                    " password_changedate = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " WHERE user_gid='" + user_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
               
                values.status = true;
                values.message = "Password Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "failure";
            }


        }

        public void DaGetAddressdetails(string employee_gid, string user_gid, employeedetails objaddress)

        {
            

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

                objaddress.permanent_address1 = objODBCDatareader["address1"].ToString();
                objaddress.permanent_address2 = objODBCDatareader["address2"].ToString();
                if (objODBCDatareader["country_gid"].ToString() == "")
                {
                    objaddress.permanent_country = "CN06070099";
                }
                if (objODBCDatareader["country_gid"].ToString() != "")
                {
                    objaddress.permanent_country = objODBCDatareader["country_gid"].ToString();
                }
                objaddress.permanent_city = objODBCDatareader["city"].ToString();
                objaddress.permanent_state = objODBCDatareader["state"].ToString();
                if ((objODBCDatareader["postal_code"].ToString()) != "")
                {
                    objaddress.permanent_postalcode = Convert.ToDouble(objODBCDatareader["postal_code"].ToString());
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
                objaddress.temporary_address1 = objODBCDatareader["address1"].ToString();
                objaddress.temporary_address2 = objODBCDatareader["address2"].ToString();
                if (objODBCDatareader["country_gid"].ToString() == "")
                {
                    objaddress.temporary_country = "CN06070099";
                }
                if (objODBCDatareader["country_gid"].ToString() != "")
                {
                    objaddress.temporary_country = objODBCDatareader["country_gid"].ToString();
                }
                objaddress.temporary_city = objODBCDatareader["city"].ToString();
                objaddress.temporary_state = objODBCDatareader["state"].ToString();
                string lspostal = objODBCDatareader["postal_code"].ToString();
                if (lspostal != "")
                {
                    objaddress.temporary_postalcode = Convert.ToDouble(objODBCDatareader["postal_code"].ToString());
                }

            }
            objaddress.status = true;
            objaddress.message = "Data Fetched";
            objODBCDatareader.Close();
            
           
        }

        public void Daupdateaddressdetails(string employee_gid, string user_gid, employeedetails values)

        {
            

            msSQL = " select * from adm_mst_taddress where parent_gid='" + employee_gid + "'";
            string lsparent_gid = objdbconn.GetExecuteScalar(msSQL);


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

            if (lsparent_gid != "")
            {
                msSQL = " update adm_mst_taddress SET " +
                     " country_gid = '" + values.permanent_country + "', " +
                     " address1 =  '" + values.permanent_address1 + "', " +
                     " address2 ='" + values.permanent_address2 + "'," +
                     " city = '" + values.permanent_city + "', " +
                     " postal_code = '" + values.permanent_postalcode + "'," +
                     " state = '" + values.permanent_state + "' " +
                     " where address_gid = '" + PermanentaddressGID + "' and " +
                     " parent_gid = '" + employee_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update adm_mst_taddress SET " +
                    " country_gid = '" + values.temporary_country + "', " +
                    " address1 =  '" + values.temporary_address1 + "', " +
                    " address2 ='" + values.temporary_address2 + "'," +
                    " city = '" + values.temporary_city + "', " +
                    " postal_code = '" + values.temporary_postalcode + "'," +
                    " state = '" + values.temporary_state + "' " +
                    " where address_gid = '" + TemporaryaddressGID + "' and " +
                    " parent_gid = '" + employee_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            else
            {
                string Msget_peraddressGid = objcmnfunctions.GetMasterGID("SADM");
                if (Msget_peraddressGid == "E")
                {

                }
                msSQL = "insert into adm_mst_taddress(" +
                    " address_gid," +
                    " country_gid," +
                    " address1," +
                    " address2," +
                    " city," +
                    " state," +
                    " postal_code," +
                    " parent_gid," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + Msget_peraddressGid + "'," +
                    "'" + values.permanent_country + "'," +
                    "'" + values.permanent_address1.Replace("'", "") + "'," +
                    "'" + values.permanent_address2.Replace("'", "") + "'," +
                    "'" + values.permanent_city + "'," +
                    "'" + values.permanent_state + "'," +
                    "'" + values.permanent_postalcode + "'," +
                    "'" + employee_gid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                string Msget_tempaddressGid = objcmnfunctions.GetMasterGID("SADM");
                if (Msget_tempaddressGid == "E")
                {
                    values.message = "";
                }


                msSQL = "insert into adm_mst_taddress(" +
                    " address_gid," +
                    " country_gid," +
                    " address1," +
                    " address2," +
                    " city," +
                    " state," +
                    " postal_code," +
                    " parent_gid," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + Msget_tempaddressGid + "'," +
                    "'" + values.temporary_country + "'," +
                    "'" + values.temporary_address1.Replace("'", "") + "'," +
                    "'" + values.temporary_address2.Replace("'", "") + "'," +
                    "'" + values.temporary_city + "'," +
                    "'" + values.temporary_country + "'," +
                    "'" + values.temporary_postalcode + "'," +
                    "'" + employee_gid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                string Msget_employeedtl = objcmnfunctions.GetMasterGID("SADM");
                if (Msget_employeedtl == "E")
                {
                    values.message = "";
                }
                msSQL = " insert into hrm_trn_temployeedtl(" +
                    " employeedtl_gid," +
                    " employee_gid," +
                    " temporaryaddress_gid," +
                    " permanentaddress_gid," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + Msget_employeedtl + "'," +
                    "'" + employee_gid + "'," +
                    "'" + Msget_peraddressGid + "'," +
                    "'" + Msget_tempaddressGid + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Address Details Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "failure";
            }
            objODBCDatareader.Close();

            
           
        }

        public void DauploadEmployeePhoto(HttpRequest httpRequest, employeePhotoUpload documentname, string employee_gid, string user_gid)
        {
            UploadDocumentList objdocumentmodel = new UploadDocumentList();
            HttpFileCollection httpFileCollection;

            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms = new MemoryStream();
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string pdfFilName = string.Empty;
            Stream ls_readStream;
            string lsdocumenttype_gid = string.Empty;
            string lspathstore;
            String path = lspath;


            msSQL = " SELECT company_code FROM adm_mst_tcompany where 1=1";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "HR/Photos";
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
                        string lsfile_gid = msdocument_gid + FileExtension;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        if ((FileExtension == ".jpg") || (FileExtension == ".jpeg") || (FileExtension == ".png") || (FileExtension == ".pdf") || (FileExtension == ".tif") || (FileExtension == ".tiff") || (FileExtension == ".txt") || (FileExtension == ".doc") || (FileExtension == ".docx") || (FileExtension == ".xls") || (FileExtension == ".xlsx"))
                        {
                            ls_readStream = httpPostedFile.InputStream;
                            ls_readStream.CopyTo(ms);
                            //CopyStream(ms, ls_readStream);
                            lspathstore = "../../erp_documents" + "/" + lscompany_code + "/" + "HR/Photos/" + lsfile_gid;
                            lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "HR/Photos/" + lsfile_gid;
                            FileStream file = new FileStream(lspath, FileMode.Create, FileAccess.Write);
                            ms.WriteTo(file);
                            file.Close();
                            ms.Close();

                            msSQL = " update hrm_mst_temployee set " +
                                    " employee_photo = '" + lspathstore + "'" +
                                    " where employee_gid='" + employee_gid + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {
                                documentname.status = true;
                                documentname.message = "Photo Uploaded Successfully";

                            }
                            else
                            {
                                documentname.status = false;
                            }
                        }
                    }
                }
            }
            catch
            {
                documentname.message = "Error Occured !";
                documentname.status = false;
            }
            

        }
    }
}
