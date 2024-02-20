using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.Net;
using System.IO;
using ems.utilities.Functions;
using ems.master.Models;
using System.Configuration;
using System.Drawing;
using ems.storage.Functions;

namespace ems.master.DataAccess
{
    public class DaMstGuarantor
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, lscustomer2usertype_gid;
        int mnResult;
        public void DaGetGuarantorList(string employee_gid, MdlMstGuarantor values)
        {
            msSQL = "select a.customer2usertype_gid,a.customer2user_name,date_format(a.customer2user_dob,'%d-%m-%Y') as customer2user_dob,a.customer2user_gender,"+
                " a.personalemail_address,a.officialemail_address,a.telephone_no,a.contact_person,a.aadhar_no,a.pan_no,a.user_type,a.photo_path,a.photo_name,"+
                " a.customer2user_age,a.escrow,a.credit_rating,a.month_business,a.landmark,date_format(a.cin_date, '%d-%m-%Y') as cin_date,a.cin_no,"+
                " a. contactperson_designation,a.company_type,a.year_business,a.gst_no,a.customer_type,date_format(a.created_date,'%d-%m-%Y') as created_date,"+
                " concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as created_by,guarantor_id from ocs_mst_tcustomer2userdtl a"+
                " left join hrm_mst_temployee b on a.created_by=b.employee_gid"+
                " left join adm_mst_tuser c on b.user_gid=c.user_gid where a.user_type='Guarantor' group by a.customer2user_name ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getguarantor_list = new List<guarantor_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getguarantor_list.Add(new guarantor_list
                    {
                        name = (dr_datarow["customer2user_name"].ToString()),
                        dob = (dr_datarow["customer2user_dob"].ToString()),
                        gender = (dr_datarow["customer2user_gender"].ToString()),
                        personalemail_address = (dr_datarow["personalemail_address"].ToString()),
                        officailemail_address = (dr_datarow["officialemail_address"].ToString()),
                        telephone_no = (dr_datarow["telephone_no"].ToString()),
                        contact_person = (dr_datarow["contact_person"].ToString()),
                        aadhar_no = (dr_datarow["aadhar_no"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        photo_name = (dr_datarow["photo_name"].ToString()),
                        photo_path = objcmnstorage.EncryptData(dr_datarow["photo_path"].ToString()),
                        age = (dr_datarow["customer2user_age"].ToString()),
                        escrow = (dr_datarow["escrow"].ToString()),
                        credit_rating = (dr_datarow["credit_rating"].ToString()),
                        month_business = (dr_datarow["month_business"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString()),
                        cin_date = (dr_datarow["cin_date"].ToString()),
                        cin_no = (dr_datarow["cin_no"].ToString()),
                        contactperson_designation = (dr_datarow["contactperson_designation"].ToString()),
                        company_type = (dr_datarow["company_type"].ToString()),
                        year_business = (dr_datarow["year_business"].ToString()),
                        gst_no = (dr_datarow["gst_no"].ToString()),
                        customer_type = (dr_datarow["customer_type"].ToString()),
                        user_type = (dr_datarow["user_type"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                         customer2usertype_gid = (dr_datarow["customer2usertype_gid"].ToString()),
                        guarantor_id = (dr_datarow["guarantor_id"].ToString())
                    });
                }
                values.guarantor_list = getguarantor_list;
            }
            dt_datatable.Dispose();
            msSQL = "select address_type,addressline1,addressline2, city,state,taluka,country,district,postal_code,customer2address_gid,primary_address" +
                " from ocs_mst_tcustomer2address where " +
              " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getaddress_list = new List<guarantoraddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getaddress_list.Add(new guarantoraddress_list
                    {
                        primary_address = (dr_datarow["primary_address"].ToString()),
                        address_type = (dr_datarow["address_type"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        city = (dr_datarow["city"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        customer2address_gid = (dr_datarow["customer2address_gid"].ToString()),
                    });
                }
                values.guarantoraddress_list = getaddress_list;
            }
            dt_datatable.Dispose();

            msSQL = "select customer2identityproof_gid,idproof_type,idproof_number from ocs_mst_tcustomer2identityproof where " +
              " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getidprooflist = new List<guarantoridproof_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getidprooflist.Add(new guarantoridproof_list
                    {
                        idproof_type = (dr_datarow["idproof_type"].ToString()),
                        idproof_no = (dr_datarow["idproof_number"].ToString()),
                        customer2identityproof_gid = (dr_datarow["customer2identityproof_gid"].ToString()),
                    });
                }
                values.guarantoridproof_list = getidprooflist;
            }
            dt_datatable.Dispose();
            msSQL = "select mobile_no,customer2mobileno_gid,primary_mobileno from ocs_mst_tcustomer2mobileno where " +
               " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmobileno_list = new List<guarantormobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmobileno_list.Add(new guarantormobileno_list
                    {
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_mobileno = (dr_datarow["primary_mobileno"].ToString()),
                        customer2mobileno_gid = (dr_datarow["customer2mobileno_gid"].ToString()),
                    });
                }
                values.guarantormobileno_list = getmobileno_list;
            }
            dt_datatable.Dispose();
            msSQL = "select customer2member_gid,member_name,member_designation from ocs_mst_tcustomer2member where " +
              " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmember_list = new List<guarantormember_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmember_list.Add(new guarantormember_list
                    {
                        member_designation = (dr_datarow["member_designation"].ToString()),
                        member_name = (dr_datarow["member_name"].ToString()),
                        customer2member_gid = (dr_datarow["customer2member_gid"].ToString()),
                    });
                }
                values.guarantormember_list = getmember_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetList(string guarantor_id, MdlCustomer values)
        {
            msSQL = " select customername,customer_urn,vertical_code from ocs_mst_tcustomer2userdtl a " +
                    " left join ocs_mst_tcustomer b on a.customer_gid = b.customer_gid" +
                    " WHERE guarantor_id = '" + guarantor_id + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                values.customer_list = dt_datatable.AsEnumerable().Select(row => new customer_list
                {
                    customername = row["customername"].ToString(),
                    customer_urn = row["customer_urn"].ToString(),
                    vertical_code = row["vertical_code"].ToString(),
                }).ToList();
                values.status = true;
                values.message = "Record Retrieved";

                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "No Record";
            }

        }
    }
   
}