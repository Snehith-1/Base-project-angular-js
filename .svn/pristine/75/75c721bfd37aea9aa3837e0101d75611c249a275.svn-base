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
    public class DaMstCustomertmp
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        HttpPostedFile httpPostedFile;
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetGid1, msGetGidREF, lspath;
        int mnResult;
        string lscustomer_name, lscustomer2userdtl_gid, lsmobile_no, lspersonalemail_address, lspan_no, lsgst_no, lscontact_person;
        string lscustomer2usertype_gid, lscustomer_type;
        public void DaGetViewCustomer2UserDtl(string customer_gid, string employee_gid, mdlcustomer2userdtl values)
        {
            msSQL = "select customer_urn,vertical_code,zonal_name,businesshead_name,relationshipmgmt_name,cluster_manager_name,creditmgmt_name,constitution_name,customername," +
                " sa_payout,sa_idname,secondaryvaluechain_name,primaryvaluechain_name,SA_status,businessunit_name from ocs_tmp_tcustomer"+
                " where tmpcustomer_gid='" + customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.vertical = objODBCDatareader["vertical_code"].ToString();
                values.zonal_head = objODBCDatareader["zonal_name"].ToString();
                values.business_head = objODBCDatareader["businesshead_name"].ToString();
                values.rm_name = objODBCDatareader["relationshipmgmt_name"].ToString();
                values.cluster_manager = objODBCDatareader["cluster_manager_name"].ToString();
                values.credit_manager = objODBCDatareader["creditmgmt_name"].ToString();
                values.constitution = objODBCDatareader["constitution_name"].ToString();
                values.sa_payout = objODBCDatareader["sa_payout"].ToString();
                values.sa_idname = objODBCDatareader["sa_idname"].ToString();
                values.secondaryvalue_chain = objODBCDatareader["secondaryvaluechain_name"].ToString();
                values.primaryvalue_chain = objODBCDatareader["primaryvaluechain_name"].ToString();
                values.sa_status = objODBCDatareader["SA_status"].ToString();
                values.business_unit = objODBCDatareader["businessunit_name"].ToString();
                values.customername = objODBCDatareader["customername"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " select a.customer2user_name,date_format(a.created_date,'%d-%m-%Y') as created_date,user_type,customer2usertype_gid," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as created_by,customer_type from ocs_tmp_tcustomer2userdtl a" +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left  join adm_mst_tuser c on b.user_gid = c.user_gid where " +
                    " tmpcustomer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer2userdtl_list = new List<customer2userdtl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomer2userdtl_list.Add(new customer2userdtl_list
                    {
                        name = (dr_datarow["customer2user_name"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        user_type = (dr_datarow["user_type"].ToString()),
                        customer2usertype_gid = (dr_datarow["customer2usertype_gid"].ToString()),
                        customer_type = (dr_datarow["customer_type"].ToString()),
                    });
                }
                values.customer2userdtl_list = getcustomer2userdtl_list;
            }
            dt_datatable.Dispose();
        }
        public void DaGetCustomer2UserInfo(string customer2usertype_gid, string employee_gid, mdlcustomer2userdtl values)
        {
            msSQL = "select customer2user_name,date_format(customer2user_dob,'%d-%m-%Y') as customer2user_dob,customer2user_gender,personalemail_address," +
                   " officialemail_address,telephone_no,contact_person,aadhar_no,pan_no,user_type,photo_path,photo_name,customer2user_age,escrow,credit_rating," +
                   " month_business,landmark,date_format(cin_date, '%d-%m-%Y') as cin_date,cin_no,contactperson_designation,company_type,year_business,gst_no," +
                   " customer_type,usertype_gid from  ocs_tmp_tcustomer2userdtl where customer2usertype_gid='" + customer2usertype_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.name = objODBCDatareader["customer2user_name"].ToString();
                values.dob = objODBCDatareader["customer2user_dob"].ToString();
                values.gender = objODBCDatareader["customer2user_gender"].ToString();
                values.personalemail_address = objODBCDatareader["personalemail_address"].ToString();
                values.officailemail_address = objODBCDatareader["officialemail_address"].ToString();
                values.telephone_no = objODBCDatareader["telephone_no"].ToString();
                values.contact_person = objODBCDatareader["contact_person"].ToString();
                values.aadhar_no = objODBCDatareader["aadhar_no"].ToString();
                values.pan_no = objODBCDatareader["pan_no"].ToString();
                values.photo_name = objODBCDatareader["photo_name"].ToString();
                values.photo_path = objcmnstorage.EncryptData(objODBCDatareader["photo_path"].ToString());
                values.age = objODBCDatareader["customer2user_age"].ToString();
                values.escrow = objODBCDatareader["escrow"].ToString();
                values.credit_rating = objODBCDatareader["credit_rating"].ToString();
                values.month_business = objODBCDatareader["month_business"].ToString();
                values.landmark = objODBCDatareader["landmark"].ToString();
                values.cin_date = objODBCDatareader["cin_date"].ToString();
                values.cin_no = objODBCDatareader["cin_no"].ToString();
                values.contactperson_designation = objODBCDatareader["contactperson_designation"].ToString();
                values.company_type = objODBCDatareader["company_type"].ToString();
                values.year_business = objODBCDatareader["year_business"].ToString();
                values.gst_no = objODBCDatareader["gst_no"].ToString();
                values.customer_type = objODBCDatareader["customer_type"].ToString();
                values.user_type = objODBCDatareader["user_type"].ToString();
                values.usertype_gid = objODBCDatareader["usertype_gid"].ToString();
                objODBCDatareader.Close();
            }

            msSQL = "select address_type,addressline1,addressline2, city,state,taluka,country,district,postal_code,customer2address_gid,primary_address" +
                " from ocs_tmp_tcustomer2address where " +
              " customer2usertype_gid='" + customer2usertype_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getaddress_list = new List<address_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getaddress_list.Add(new address_list
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
                values.address_list = getaddress_list;
            }
            dt_datatable.Dispose();

            msSQL = "select customer2identityproof_gid,idproof_type,idproof_number from ocs_tmp_tcustomer2identityproof where " +
              " customer2usertype_gid='" + customer2usertype_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getidprooflist = new List<idproof_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getidprooflist.Add(new idproof_list
                    {
                        idproof_type = (dr_datarow["idproof_type"].ToString()),
                        idproof_no = (dr_datarow["idproof_number"].ToString()),
                        customer2identityproof_gid = (dr_datarow["customer2identityproof_gid"].ToString()),
                    });
                }
                values.idproof_list = getidprooflist;
            }
            dt_datatable.Dispose();
            msSQL = "select mobile_no,customer2mobileno_gid,primary_mobileno from ocs_tmp_tcustomer2mobileno where " +
               " customer2usertype_gid='" + customer2usertype_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmobileno_list = new List<mobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmobileno_list.Add(new mobileno_list
                    {
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_mobileno = (dr_datarow["primary_mobileno"].ToString()),
                        customer2mobileno_gid = (dr_datarow["customer2mobileno_gid"].ToString()),
                    });
                }
                values.mobileno_list = getmobileno_list;
            }
            dt_datatable.Dispose();
            msSQL = "select customer2member_gid,member_name,member_designation from ocs_tmp_tcustomer2member where " +
              " customer2usertype_gid='" + customer2usertype_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmember_list = new List<member_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmember_list.Add(new member_list
                    {
                        member_designation = (dr_datarow["member_designation"].ToString()),
                        member_name = (dr_datarow["member_name"].ToString()),
                        customer2member_gid = (dr_datarow["customer2member_gid"].ToString()),
                    });
                }
                values.member_list = getmember_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetCount( mdlcustomer2userdtl values)
        {
            msSQL = "select count(*) from ocs_tmp_tcustomer";
            values.lstmpcount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) from ocs_mst_tcustomer";
            values.lsmstcount = objdbconn.GetExecuteScalar(msSQL);
            values.status = true;
        }
    }
}