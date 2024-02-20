using ems.master.Models;
using ems.utilities.Functions;
using System;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Net;
using System.IO;
using System.Configuration;
using ems.storage.Functions;
namespace ems.master.DataAccess
{
    public class DaMstRMMapping
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable, dt_datatable1;
        string msSQL;        
        string lscustomer_gid, lscustomer2usertype_gid;
        public void DaGetHierarchyList(string baselocation_gid,string vertical_gid,string employeegid,MdlRMMappingview objMdlRMMapping, string employee_gid)
        {
            try
            {
                msSQL = "select count(user_gid) from adm_mst_tuser where user_status='Y' and user_gid not in ('U1')";
                objMdlRMMapping.employee_count = objdbconn.GetExecuteScalar(msSQL);

              

                msSQL = " select a.employeereporting_to,concat(c.user_firstname,' ',c.user_lastname,' || ',c.user_code) as level_zero,b.employee_gid, " +
                       "  concat( g.user_firstname, ' ', g.user_lastname, ' || ', g.user_code) as level_one,b.baselocation_gid,  " +
                       "  t.employee_name as clusterhead,v.employee_name as regionhead,x.employee_name as zonalhead, " +
                       "  y.employee_name as businesshead  " +
                       "  from adm_mst_tmodule2employee a " +
                       "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
                       "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                       "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                       "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +                        
                       "  left join  sys_mst_tcluster2baselocation s on s.baselocation_gid = b.baselocation_gid  " +
                       "  left join  sys_mst_tclusterhead t on s.cluster_gid = t.cluster_gid  " +
                       "  left join sys_mst_tregion2cluster u on u.cluster_gid = s.cluster_gid  " +
                       "  left join sys_mst_tregionhead v on v.region_gid = u.region_gid  " +
                       "  left join sys_mst_tzone2region w on w.region_gid = v.region_gid  " +
                       "  left join sys_mst_tzonalhead x on x.zonal_gid = w.zone_gid  " +
                       " left join sys_mst_tbusinesshead y on y.zone_gid=x.zonal_gid  " +
                       "  where a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " + 
                       " (select modulereportingto_gid from adm_mst_tcompany)) and c.user_status = 'Y' and ";
                if (baselocation_gid == null || baselocation_gid == "")
                {
                    msSQL += "1=1 ";
                }
                else
                {

                    msSQL += "b.baselocation_gid = '" + baselocation_gid + "'";
                }
                if (vertical_gid == null || vertical_gid == "")
                {
                    msSQL += "1=1 ";
                }
                else
                {

                    msSQL += "t.vertical_gid = '" + vertical_gid + "'";
                }
                if (employeegid == null || employeegid == "")
                {
                    msSQL += "1=1 group by a.employee_gid";
                }
                else
                {

                    msSQL += "b.employee_gid = '" + employeegid + "' group by a.employee_gid";
                }
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getRMMappingviewlist = new List<MdlRMMappingviewlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getRMMappingviewlist.Add(new MdlRMMappingviewlist
                        {
                            level_zero = (dr_datarow["level_zero"].ToString()),
                            level_one = (dr_datarow["level_one"].ToString()),
                            clusterhead = (dr_datarow["clusterhead"].ToString()),
                            regionhead = (dr_datarow["regionhead"].ToString()),
                            zonalhead = (dr_datarow["zonalhead"].ToString()),
                            businesshead = (dr_datarow["businesshead"].ToString()),
                        });
                    }
                    objMdlRMMapping.MdlRMMappingviewlist = getRMMappingviewlist;
                }
                dt_datatable.Dispose();
               
             objMdlRMMapping.status = true;
            }
            catch (Exception ex)
            {
                objMdlRMMapping.status = false;
            }
        }

      

        public void DaGetRMViewCustomer2UserDtl(string customer_gid, string employee_gid, mdlcustomer2userdtl values)
        {
            msSQL = "select customer_gid from ocs_mst_tcustomer where customer_gid='" + customer_gid + "'";
            lscustomer_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lscustomer_gid == "" || lscustomer_gid == null)
            {
                msSQL = "select customer_urn,vertical_code,zonal_name,businesshead_name,relationshipmgmt_name,cluster_manager_name,creditmgmt_name,constitution_name,customername," +
                " sa_payout,sa_idname,secondaryvaluechain_name,primaryvaluechain_name,SA_status,businessunit_name from ocs_tmp_tcustomer" +
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
                msSQL = " select a.customer2user_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,user_type,customer2usertype_gid," +
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
            else
            {
                msSQL = "select customer_urn,vertical_code,zonal_name,businesshead_name,relationshipmgmt_name,cluster_manager_name,creditmgmt_name,constitution_name,customername," +
                    " sa_payout,sa_idname,secondaryvaluechain_name,primaryvaluechain_name,SA_status,businessunit_name from ocs_mst_tcustomer where customer_gid='" + customer_gid + "'";
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
                msSQL = " select a.customer2user_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,user_type,customer2usertype_gid," +
                        " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as created_by,customer_type from ocs_mst_tcustomer2userdtl a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left  join adm_mst_tuser c on b.user_gid = c.user_gid where " +
                        " customer_gid='" + customer_gid + "'";
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

        }
        public void DaGetRMCustomer2UserInfo(string customer2usertype_gid, string employee_gid, mdlcustomer2userdtl values)
        {
            msSQL = "select customer2usertype_gid from ocs_mst_tcustomer2userdtl where customer2usertype_gid='" + customer2usertype_gid + "'";
            lscustomer2usertype_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lscustomer2usertype_gid == "" || lscustomer2usertype_gid == null)
            {

                msSQL = "select customer2user_name,date_format(customer2user_dob,'%d-%m-%Y %h:%i %p') as customer2user_dob,customer2user_gender,personalemail_address," +
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
                    values.photo_path = objcmnstorage.EncryptData(HttpContext.Current.Server.MapPath(objODBCDatareader["photo_path"].ToString()));
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
                }
                objODBCDatareader.Close();

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
            else
            {

                msSQL = "select customer2user_name,date_format(customer2user_dob,'%d-%m-%Y') as customer2user_dob,customer2user_gender,personalemail_address," +
                   " officialemail_address,telephone_no,contact_person,aadhar_no,pan_no,user_type,photo_path,photo_name,customer2user_age,escrow,credit_rating," +
                   " month_business,landmark,date_format(cin_date, '%d-%m-%Y') as cin_date,cin_no,contactperson_designation,company_type,year_business,gst_no," +
                   " customer_type,usertype_gid from  ocs_mst_tcustomer2userdtl where customer2usertype_gid='" + customer2usertype_gid + "'";
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
                    values.photo_path = objcmnstorage.EncryptData(HttpContext.Current.Server.MapPath(objODBCDatareader["photo_path"].ToString()));
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
                }
                objODBCDatareader.Close();

                msSQL = "select address_type,addressline1,addressline2, city,state,taluka,country,district,postal_code,customer2address_gid,primary_address" +
                    " from ocs_mst_tcustomer2address where " +
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

                msSQL = "select customer2identityproof_gid,idproof_type,idproof_number from ocs_mst_tcustomer2identityproof where " +
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
                msSQL = "select mobile_no,customer2mobileno_gid,primary_mobileno from ocs_mst_tcustomer2mobileno where " +
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
                msSQL = "select customer2member_gid,member_name,member_designation from ocs_mst_tcustomer2member where " +
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
        }

        public void DaGetEmployeelist(locationemployee objmaster)
        {
            try
            {

                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) " +
                       " as employee_name,b.employee_gid from adm_mst_tuser a " +
                       " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                       " LEFT JOIN adm_mst_tmodule2employee c on c.employee_gid=b.employee_gid " +
                       " where a.user_status<>'N' and c.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                       " (select modulereportingto_gid from adm_mst_tcompany)) order by a.user_firstname asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employeelists>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objmaster.employeelists = dt_datatable.AsEnumerable().Select(row =>
                      new employeelists
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }

        public void DaGetLocationEmployeelist(locationemployee objmaster, string baselocation_gid)
        {
            try
            {


           msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " LEFT JOIN adm_mst_tmodule2employee c on c.employee_gid=b.employee_gid " +
                   " where a.user_status<>'N' and c.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                   " (select modulereportingto_gid from adm_mst_tcompany)) and b.baselocation_gid='" + baselocation_gid  + "' order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employeelists>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objmaster.employeelists = dt_datatable.AsEnumerable().Select(row =>
                      new employeelists
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }

        public void DaPostAllHierarchyListSearch(MdlRMMappingview values, string employee_gid)
        {
            try
            {
             

                msSQL = "select count(a.user_gid) from adm_mst_tuser a " +
                         "where user_status='Y' and user_gid not in ('U1')";
                values.employee_count = objdbconn.GetExecuteScalar(msSQL);

           msSQL = " select a.employeereporting_to,concat(c.user_firstname,' ',c.user_lastname,' || ',c.user_code) as level_zero,b.employee_gid, " +
                   "  concat( g.user_firstname, ' ', g.user_lastname, ' || ', g.user_code) as level_one  " +
                   "  from adm_mst_tmodule2employee a " +
                   "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
                   "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                   "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                   "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +    
                   "  where a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                   " (select modulereportingto_gid from adm_mst_tcompany))  and c.user_status = 'Y' and ";
                if (values.baselocation_gid == null || values.baselocation_gid == "")
                {
                    msSQL += "1=1 and ";
                }
                else
                {

                    msSQL += "b.baselocation_gid = '" + values.baselocation_gid + "' and ";
                }
                if (values.employeegid == null || values.employeegid == "")
                {
                    msSQL += "1=1 group by a.employee_gid ";
                }
                else
                {

                    msSQL += "b.employee_gid = '" + values.employeegid + "' group by a.employee_gid";
                }

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getRMMappingviewlist = new List<MdlRMMappingviewlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        msSQL = "select distinct c.cluster_name,c.employee_name as clusterhead,c.vertical_name as clustervertical," +
                      "  e.region_name,e.employee_name as regionhead,g.zonal_name,g.employee_name as zonalhead ," +
                      "  h.employee_name as businesshead from hrm_mst_temployee a" +
                      "  left join sys_mst_tcluster2baselocation b on b.baselocation_gid = a.baselocation_gid" +
                      " left  join sys_mst_tclusterhead c on b.cluster_gid = c.cluster_gid" +
                      " left join sys_mst_tregion2cluster d on c.cluster_gid = d.cluster_gid" +
                      " left join sys_mst_tregionhead e on d.region_gid = e.region_gid" +
                      " left join sys_mst_tzone2region f on f.region_gid = d.region_gid" +
                      " left join sys_mst_tzonalhead g on f.zone_gid = g.zonal_gid" +
                      " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid where a.employee_gid = '" + dr_datarow["employee_gid"].ToString() + "' " +
                      " and e.status='Y' and g.status='Y' and h.status='Y' and c.status='Y' and ";
                        if (values.baselocation_gid == null || values.baselocation_gid == "")
                        {
                            msSQL += "1=1 and ";
                        }
                        else
                        {

                            msSQL += "b.baselocation_gid = '" + values.baselocation_gid + "' and ";
                        }
                        if (values.vertical_gid == null || values.vertical_gid == "")
                        {
                            msSQL += "1=1 and";
                        }
                        else
                        {

                            msSQL += " c.vertical_gid = '" + values.vertical_gid + "' and e.vertical_gid = '" + values.vertical_gid + "' " +
                                     " and g.vertical_gid = '" + values.vertical_gid + "' and h.vertical_gid = '" + values.vertical_gid + "' and ";
                        }
                        if (values.program_gid == null || values.program_gid == "")
                        {
                            msSQL += "1=1 ";
                        }
                        else
                        {

                            msSQL += " c.program_gid = '" + values.program_gid + "' and e.program_gid = '" + values.program_gid + "' " +
                                     " and g.program_gid = '" + values.program_gid + "' and h.program_gid = '" + values.program_gid + "' ";
                        }
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if(objODBCDatareader.HasRows == true)
                        {
                            getRMMappingviewlist.Add(new MdlRMMappingviewlist
                            {
                                level_zero = (dr_datarow["level_zero"].ToString()),
                                level_one = (dr_datarow["level_one"].ToString()),
                                clusterhead = (objODBCDatareader["clusterhead"].ToString()),
                                regionhead = (objODBCDatareader["regionhead"].ToString()),
                                zonalhead = (objODBCDatareader["zonalhead"].ToString()),
                                businesshead = (objODBCDatareader["businesshead"].ToString())
                            });
                        }
                        objODBCDatareader.Close();
                    }
                    values.MdlRMMappingviewlist = getRMMappingviewlist;
                }
                dt_datatable.Dispose();
            

                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaRMMappingviewExport(MdlRMMappingview values, string employee_gid)
        {
           

            msSQL = " select a.employeereporting_to,concat(c.user_firstname,' ',c.user_lastname,' || ',c.user_code) as level_zero,b.employee_gid, " +
                    "  concat( g.user_firstname, ' ', g.user_lastname, ' || ', g.user_code) as level_one  " +
                    "  from adm_mst_tmodule2employee a " +
                    "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
                    "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                    "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +      
                    "  where a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                    "  (select modulereportingto_gid from adm_mst_tcompany))  and c.user_status = 'Y' and ";
            if (values.baselocation_gid == null || values.baselocation_gid == "")
            {
                msSQL += "1=1 and ";
            }
            else
            {

                msSQL += "b.baselocation_gid = '" + values.baselocation_gid + "' and ";
            }
            if (values.employeegid == null || values.employeegid == "")
            {
                msSQL += "1=1 group by a.employee_gid ";
            }
            else
            {

                msSQL += "b.employee_gid = '" + values.employeegid + "' group by a.employee_gid";
            }

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getRMMappingexllist = new List<MdlRMMappingexllist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    msSQL = "select distinct c.cluster_name,c.employee_name as clusterhead,c.vertical_name as clustervertical,c.program_name, " +
                          " e.region_name,e.employee_name as regionhead,g.zonal_name,g.employee_name as zonalhead ," +
                          " h.employee_name as businesshead from hrm_mst_temployee a" +
                          " left join sys_mst_tcluster2baselocation b on b.baselocation_gid = a.baselocation_gid" +
                          " left  join sys_mst_tclusterhead c on b.cluster_gid = c.cluster_gid" +
                          " left join sys_mst_tregion2cluster d on c.cluster_gid = d.cluster_gid" +
                          " left join sys_mst_tregionhead e on d.region_gid = e.region_gid" +
                          " left join sys_mst_tzone2region f on f.region_gid = d.region_gid" +
                          " left join sys_mst_tzonalhead g on f.zone_gid = g.zonal_gid" +
                          " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid where a.employee_gid = '" + dr_datarow["employee_gid"].ToString() + "' " +
                          " and e.status='Y' and g.status='Y' and h.status='Y' and c.status='Y' and ";
                    if (values.baselocation_gid == null || values.baselocation_gid == "")
                    {
                        msSQL += "1=1 and ";
                    }
                    else
                    {

                        msSQL += "b.baselocation_gid = '" + values.baselocation_gid + "' and ";
                    }
                    if (values.vertical_gid == null || values.vertical_gid == "")
                    {
                        msSQL += "1=1 and ";
                    }
                    else
                    {

                        msSQL += " c.vertical_gid = '" + values.vertical_gid + "' and e.vertical_gid = '" + values.vertical_gid + "' " +
                                 " and g.vertical_gid = '" + values.vertical_gid + "' and h.vertical_gid = '" + values.vertical_gid + "' and ";
                    }
                    if (values.program_gid == null || values.program_gid == "")
                    {
                        msSQL += "1=1 ";
                    }
                    else
                    {

                        msSQL += " c.program_gid = '" + values.program_gid + "' and e.program_gid = '" + values.program_gid + "' " +
                                 " and g.program_gid = '" + values.program_gid + "' and h.program_gid = '" + values.program_gid + "' ";
                    }
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        getRMMappingexllist.Add(new MdlRMMappingexllist
                        {
                            vertical = (objODBCDatareader["clustervertical"].ToString()),
                            program = (objODBCDatareader["program_name"].ToString()),
                            level_zero = (dr_datarow["level_zero"].ToString()),
                            level_one = (dr_datarow["level_one"].ToString()),
                            clusterhead = (objODBCDatareader["clusterhead"].ToString()),
                            regionhead = (objODBCDatareader["regionhead"].ToString()),
                            zonalhead = (objODBCDatareader["zonalhead"].ToString()),
                            businesshead = (objODBCDatareader["businesshead"].ToString()),
                          
                        });
                    }
                    objODBCDatareader.Close();

                }
                values.MdlRMMappingexllist = getRMMappingexllist;

            }
            dt_datatable.Dispose();
            string lscompany_code = string.Empty;
            MemoryStream ms = new MemoryStream();
            ExcelPackage excel = new ExcelPackage(ms);
            ListtoDataTable lsttodt = new ListtoDataTable();
            DataTable dt1 = lsttodt.ToDataTable(getRMMappingexllist);
            var workSheet = excel.Workbook.Worksheets.Add("RM Mapping Report");
            try
            {
                msSQL = " select company_code from adm_mst_tcompany";

                lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Master/RM Mapping/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(values.lspath)))
                        System.IO.Directory.CreateDirectory(values.lspath);
                }

                values.lsname = "RMMapping_Report" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "Master/RM Mapping/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                values.lscloudpath =  lscompany_code + "/" + "Master/RM Mapping/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.lsname;
                workSheet.Cells[1, 1].LoadFromDataTable(dt1, true);
                FileInfo file = new FileInfo(values.lspath);
                using (var range = workSheet.Cells[1, 1, 1, 24])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }
                excel.SaveAs(ms);
                bool status;
                status = objcmnstorage.UploadStream("erpdocument", values.lscloudpath, ms);
                ms.Close();
                dt_datatable.Dispose();
                values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
                values.lspath = objcmnstorage.EncryptData(values.lspath);
                values.status = true;
                values.message = "Success";

            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Failure";
            }


        }

        public void DaPostAllHierarchyverticalListSearch(MdlRMMappingverticalview values)
        {
            try
            {
                msSQL = "select count(a.user_gid) from adm_mst_tuser a " +
                         "where user_status='Y' and user_gid not in ('U1')";
                values.employee_count = objdbconn.GetExecuteScalar(msSQL);
                string lscluster_gid;
                lscluster_gid = objdbconn.GetExecuteScalar("select cluster_gid from sys_mst_tcluster2baselocation where baselocation_gid in ( select baselocation_gid from hrm_mst_temployee where employee_gid ='" + values.employee_gid + "')");
                msSQL = " SELECT vertical_gid ,vertical_name FROM ocs_mst_tvertical where vertical_gid in (select vertical_gid from sys_mst_tclusterhead where cluster_gid ='" + lscluster_gid + "')" +
                        " order by vertical_name asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getverticallist = new List<verticallist>();
                if (dt_datatable.Rows.Count != 0)
                {

                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {

                        //msSQL = " select group_concat(a.program_gid)  from ocs_mst_tprogram2vertical a " +
                        //        " left join ocs_mst_tprogram b on a.program_gid = b.program_gid " +
                        //        " where a.vertical_gid = '" + dr_datarow["vertical_gid"].ToString() + "' and b.status = 'Y' and a.status = 'Y')";
                        //string lsprogram_gid = objdbconn.GetExecuteScalar(msSQL);
                        //lsprogram_gid = lsprogram_gid.Replace("\",\"", ",");

                        msSQL = "select distinct c.cluster_name, c.program_name, c.employee_name as clusterhead,c.vertical_name as clustervertical," +
                          "  e.region_name,e.employee_name as regionhead,g.zonal_name,g.employee_name as zonalhead ," +
                          "  h.employee_name as businesshead from hrm_mst_temployee a" +
                          "  left join sys_mst_tcluster2baselocation b on b.baselocation_gid = a.baselocation_gid" +
                          " left  join sys_mst_tclusterhead c on b.cluster_gid = c.cluster_gid " +
                          " left join sys_mst_tregion2cluster d on c.cluster_gid = d.cluster_gid " +
                          " left join sys_mst_tregionhead e on d.region_gid = e.region_gid  and c.program_gid = e.program_gid " +
                          " left join sys_mst_tzone2region f on f.region_gid = d.region_gid " +
                          " left join sys_mst_tzonalhead g on f.zone_gid = g.zonal_gid  and c.program_gid = g.program_gid " +
                          " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid and c.program_gid = h.program_gid where a.employee_gid = '" + values.employee_gid + "' and" +
                          " c.vertical_gid = '" + dr_datarow["vertical_gid"].ToString() + "'" +
                          " and e.vertical_gid = '" + dr_datarow["vertical_gid"].ToString() + "' and "+
                          " g.vertical_gid = '" + dr_datarow["vertical_gid"].ToString() + "' and h.vertical_gid = '" + dr_datarow["vertical_gid"].ToString() + "' " +
                          " and e.status='Y' and g.status='Y' and h.status='Y' and c.status='Y'";

                        dt_datatable1 = objdbconn.GetDataTable(msSQL);
                        var getMdlRMviewlist = new List<viewlist>();
                        if (dt_datatable1.Rows.Count != 0)
                        {
                            foreach (DataRow dr_datarow1 in dt_datatable1.Rows)
                            { 
                                getMdlRMviewlist.Add(new viewlist
                                {
                                    clusterhead = (dr_datarow1["clusterhead"].ToString()),
                                    regionhead = (dr_datarow1["regionhead"].ToString()),
                                    zonalhead = (dr_datarow1["zonalhead"].ToString()),
                                    businesshead = (dr_datarow1["businesshead"].ToString()),
                                    program_name = (dr_datarow1["program_name"].ToString()),
                                });
                                 
                            } 
                        }
                        dt_datatable1.Dispose();
                        getverticallist.Add(new verticallist
                        {
                            vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                            vertical_name = (dr_datarow["vertical_name"].ToString()),
                           
                            viewlist = getMdlRMviewlist
                        });
                    }
                    values.verticallist = getverticallist;
                }
               
             dt_datatable.Dispose();

               

                msSQL = " select a.employeereporting_to,concat(c.user_firstname,' ',c.user_lastname,' || ',c.user_code) as level_zero,b.employee_gid, " +
                        "  concat( g.user_firstname, ' ', g.user_lastname, ' || ', g.user_code) as level_one  " +
                        "  from adm_mst_tmodule2employee a " +
                        "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
                        "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                        "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                        "  where a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in  " +
                        "  (select modulereportingto_gid from adm_mst_tcompany))  and c.user_status = 'Y' and b.employee_gid ='" + values.employee_gid + "'  group by a.employee_gid";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.level_zero = objODBCDatareader["level_zero"].ToString();
                    values.level_one = objODBCDatareader["level_one"].ToString();
                  
                }
                objODBCDatareader.Close();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public class ListtoDataTable
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties by using reflection   
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names  
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {

                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }

                return dataTable;
            }
        }

    }
}