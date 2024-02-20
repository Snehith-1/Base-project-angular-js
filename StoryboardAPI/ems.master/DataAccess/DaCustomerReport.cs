﻿using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
//using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Globalization;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.Linq;
using ems.storage.Functions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

/// <summary>
/// (It's used for Customer Report ) Customer Report DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>


namespace ems.master.DataAccess
{
    public class DaCustomerReport
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGETGID;
        string lscustomer_gid, lscustomer2usertype_gid, lsurn, lspath;
      
        public void DaGetEditCustomerurn(string customer_gid, customerediturn values)
        {
            try
            {
                msSQL = " select customer_gid from ocs_mst_tcustomer where customer_gid ='" + customer_gid + "'";
                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                objODBCDatareader1.Read();
                if (objODBCDatareader1.HasRows == true)
                {
                    lscustomer_gid = objODBCDatareader1["customer_gid"].ToString();
                    objODBCDatareader1.Close();

                    msSQL = " select customer_code,customername,customer_gid,contactperson,mobileno,gst_number,pan_number," +
                            " contact_no,email,address,address2,region,vertical_gid,vertical_code,ccmail_text,state," +
                            " country,state_gid,postalcode,business_head,zonal_head,zonal_name,businesshead_name,district_name,zonal_riskmanager,assigned_RM," +
                            " cluster_manager_gid,cluster_manager_name,relationshipmgmt_name,relationship_manager,creditmanager_gid,creditmgmt_name,customer_urn," +
                            " major_corporate,constitution_name,constitution_gid from ocs_mst_tcustomer where customer_gid = '" + lscustomer_gid + "' ";

                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {

                        values.customerCodeedit = objODBCDatareader["customer_code"].ToString();
                        values.customerNameedit = objODBCDatareader["customername"].ToString();
                        values.contactPersonedit = objODBCDatareader["contactperson"].ToString();
                        if (objODBCDatareader["mobileno"].ToString() != "")
                        {
                            values.mobileNo_edit = Convert.ToDouble(objODBCDatareader["mobileno"].ToString());
                        }
                        values.mobileNoedit = objODBCDatareader["mobileno"].ToString();
                        if (objODBCDatareader["contact_no"].ToString() != "")
                        {
                            values.contactno_edit = Convert.ToDouble(objODBCDatareader["contact_no"].ToString());
                        }
                        values.contactnoedit = objODBCDatareader["contact_no"].ToString();
                        values.emailedit = objODBCDatareader["email"].ToString();
                        values.addressline1edit = objODBCDatareader["address"].ToString();
                        values.addressline2edit = objODBCDatareader["address2"].ToString();
                        values.regionedit = objODBCDatareader["region"].ToString();
                        values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                        values.vertical_code = objODBCDatareader["vertical_code"].ToString();
                        values.ccmailedit = objODBCDatareader["ccmail_text"].ToString();
                        values.countryedit = objODBCDatareader["country"].ToString();
                        values.state_gid = objODBCDatareader["state_gid"].ToString();
                        values.state = objODBCDatareader["state"].ToString();
                        if (objODBCDatareader["postalcode"].ToString() != "")
                        {
                            values.postalcode_edit = Convert.ToDouble(objODBCDatareader["postalcode"].ToString());
                        }
                        values.postalcodeedit = objODBCDatareader["postalcode"].ToString();
                        values.businessHeadGid = objODBCDatareader["business_head"].ToString();
                        values.zonalGid = objODBCDatareader["zonal_head"].ToString();
                        values.employee_gid = objODBCDatareader["zonal_head"].ToString();
                        values.employee_name = objODBCDatareader["zonal_name"].ToString();
                        values.zonal_name = objODBCDatareader["zonal_name"].ToString();
                        values.businesshead_name = objODBCDatareader["businesshead_name"].ToString();
                        values.clustermanagerGid = objODBCDatareader["cluster_manager_gid"].ToString();
                        values.cluster_manager_name = objODBCDatareader["cluster_manager_name"].ToString();
                        values.relationshipmgmt_name = objODBCDatareader["relationshipmgmt_name"].ToString();
                        values.relationshipMgmtGid = objODBCDatareader["relationship_manager"].ToString();
                        values.creditmanagerGid = objODBCDatareader["creditmanager_gid"].ToString();
                        values.creditmanager_name = objODBCDatareader["creditmgmt_name"].ToString();
                        values.customer_urnedit = objODBCDatareader["customer_urn"].ToString();
                        values.gst_number = objODBCDatareader["gst_number"].ToString();
                        values.pan_number = objODBCDatareader["pan_number"].ToString();
                        values.customer_gid = objODBCDatareader["customer_gid"].ToString();
                        values.constitution_nameedit = objODBCDatareader["constitution_name"].ToString();
                        values.constitution_gidedit = objODBCDatareader["constitution_gid"].ToString();
                        values.major_corporateedit = objODBCDatareader["major_corporate"].ToString();
                        values.district_nameedit = objODBCDatareader["district_name"].ToString();
                        values.riskmanageredit = objODBCDatareader["assigned_RM"].ToString();
                        msSQL = " select concat(b.user_firstname,' ',b.user_lastname, ' / ',b.user_code) as assignedRM_name from hrm_mst_temployee a " +
                                            " left join adm_mst_tuser b on a.user_gid=b.user_gid " +
                                            " where a.employee_gid='" + values.riskmanageredit + "'";
                        values.risk_managernameedit = objdbconn.GetExecuteScalar(msSQL);

                    }
                }
                else
                {
                    msSQL = " select tmpcustomer_gid from ocs_tmp_tcustomer where tmpcustomer_gid ='" + customer_gid + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);

                    objODBCDatareader1.Read();
                    if (objODBCDatareader1.HasRows == true)
                    {
                        lscustomer_gid = objODBCDatareader1["tmpcustomer_gid"].ToString();
                        objODBCDatareader1.Close();

                        msSQL = " select customer_code,customername,tmpcustomer_gid,contactperson,mobileno,gst_number,pan_number," +
                                " contact_no,email,address,address2,region,vertical_gid,vertical_code,ccmail_text,state," +
                                " country,state_gid,postalcode,business_head,zonal_head,zonal_name,businesshead_name,district_name,zonal_riskmanager,assigned_RM," +
                                " cluster_manager_gid,cluster_manager_name,relationshipmgmt_name,relationship_manager,creditmanager_gid,creditmgmt_name,customer_urn," +
                                " major_corporate,constitution_name,constitution_gid from ocs_tmp_tcustomer where tmpcustomer_gid = '" + lscustomer_gid + "' ";

                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {

                            values.customerCodeedit = objODBCDatareader["customer_code"].ToString();
                            values.customerNameedit = objODBCDatareader["customername"].ToString();
                            values.contactPersonedit = objODBCDatareader["contactperson"].ToString();
                            if (objODBCDatareader["mobileno"].ToString() != "")
                            {
                                values.mobileNo_edit = Convert.ToDouble(objODBCDatareader["mobileno"].ToString());
                            }
                            values.mobileNoedit = objODBCDatareader["mobileno"].ToString();
                            if (objODBCDatareader["contact_no"].ToString() != "")
                            {
                                values.contactno_edit = Convert.ToDouble(objODBCDatareader["contact_no"].ToString());
                            }
                            values.contactnoedit = objODBCDatareader["contact_no"].ToString();
                            values.emailedit = objODBCDatareader["email"].ToString();
                            values.addressline1edit = objODBCDatareader["address"].ToString();
                            values.addressline2edit = objODBCDatareader["address2"].ToString();
                            values.regionedit = objODBCDatareader["region"].ToString();
                            values.vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                            values.vertical_code = objODBCDatareader["vertical_code"].ToString();
                            values.ccmailedit = objODBCDatareader["ccmail_text"].ToString();
                            values.countryedit = objODBCDatareader["country"].ToString();
                            values.state_gid = objODBCDatareader["state_gid"].ToString();
                            values.state = objODBCDatareader["state"].ToString();
                            if (objODBCDatareader["postalcode"].ToString() != "")
                            {
                                values.postalcode_edit = Convert.ToDouble(objODBCDatareader["postalcode"].ToString());
                            }
                            values.postalcodeedit = objODBCDatareader["postalcode"].ToString();
                            values.businessHeadGid = objODBCDatareader["business_head"].ToString();
                            values.zonalGid = objODBCDatareader["zonal_head"].ToString();
                            values.employee_gid = objODBCDatareader["zonal_head"].ToString();
                            values.employee_name = objODBCDatareader["zonal_name"].ToString();
                            values.zonal_name = objODBCDatareader["zonal_name"].ToString();
                            values.businesshead_name = objODBCDatareader["businesshead_name"].ToString();
                            values.clustermanagerGid = objODBCDatareader["cluster_manager_gid"].ToString();
                            values.cluster_manager_name = objODBCDatareader["cluster_manager_name"].ToString();
                            values.relationshipmgmt_name = objODBCDatareader["relationshipmgmt_name"].ToString();
                            values.relationshipMgmtGid = objODBCDatareader["relationship_manager"].ToString();
                            values.creditmanagerGid = objODBCDatareader["creditmanager_gid"].ToString();
                            values.creditmanager_name = objODBCDatareader["creditmgmt_name"].ToString();
                            values.customer_urnedit = objODBCDatareader["customer_urn"].ToString();
                            values.gst_number = objODBCDatareader["gst_number"].ToString();
                            values.pan_number = objODBCDatareader["pan_number"].ToString();
                            values.customer_gid = objODBCDatareader["tmpcustomer_gid"].ToString();
                            values.constitution_nameedit = objODBCDatareader["constitution_name"].ToString();
                            values.constitution_gidedit = objODBCDatareader["constitution_gid"].ToString();
                            values.major_corporateedit = objODBCDatareader["major_corporate"].ToString();
                            values.district_nameedit = objODBCDatareader["district_name"].ToString();
                            values.riskmanageredit = objODBCDatareader["assigned_RM"].ToString();
                            msSQL = " select concat(b.user_firstname,' ',b.user_lastname, ' / ',b.user_code) as assignedRM_name from hrm_mst_temployee a " +
                                                " left join adm_mst_tuser b on a.user_gid=b.user_gid " +
                                                " where a.employee_gid='" + values.riskmanageredit + "'";
                            values.risk_managernameedit = objdbconn.GetExecuteScalar(msSQL);

                        }
                    }
                }
                objODBCDatareader.Close();
                values.status = true;
                values.message = "success";

            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }
        public void DaGetcustomerdetails(string customer_gid, mdlcustomer2userdtl values)
        {
            try
            {
                msSQL = " select customer_gid from ocs_mst_tcustomer where customer_gid ='" + customer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                objODBCDatareader.Read();
                if (objODBCDatareader.HasRows == true)
                {
                    lscustomer_gid = objODBCDatareader["customer_gid"].ToString();

                    objODBCDatareader.Close();
                    msSQL = "select customer_urn,vertical_code,zonal_name,businesshead_name,relationshipmgmt_name,cluster_manager_name,creditmgmt_name,constitution_name," +
                     " sa_payout,sa_idname,secondaryvaluechain_name,primaryvaluechain_name,SA_status,businessunit_name,customer_type from ocs_mst_tcustomer where customer_gid='" + lscustomer_gid + "'";
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
                        values.customer_type = objODBCDatareader["customer_type"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = "select customer2usertype_gid from ocs_mst_tcustomer2userdtl where customer_gid='" + lscustomer_gid + "' and user_type='Applicant'";
                    lscustomer2usertype_gid = objdbconn.GetExecuteScalar(msSQL);


                    msSQL = "select address_type,addressline1,addressline2, city,state,taluka,country,district,postal_code,customer2address_gid,primary_address" +
                        " from ocs_mst_tcustomer2address where " +
                      " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
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
                      " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
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
                       " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
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
                      " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getmember_list = new List<institutionmember_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getmember_list.Add(new institutionmember_list
                            {
                                member_designation = (dr_datarow["member_designation"].ToString()),
                                member_name = (dr_datarow["member_name"].ToString()),
                                customer2member_gid = (dr_datarow["customer2member_gid"].ToString()),
                            });
                        }
                        values.institutionmember_list = getmember_list;
                    }
                    dt_datatable.Dispose();
                    msSQL = " select customer2user_name,concat(date_format(customer2user_dob,'%d-%m-%Y'),'/',customer2user_age) as dobage,customer2user_gender,personalemail_address,officialemail_address," +
                           " telephone_no,contact_person,aadhar_no,pan_no,user_type,escrow,credit_rating,month_business,landmark,date_format(cin_date,'%d-%m-%Y') as cin_date," +
                           " contactperson_designation,company_type,cin_date,cin_no,year_business,gst_no,customer_type from ocs_mst_tcustomer2userdtl where" +
                       " customer_gid='" + lscustomer_gid + "' and user_type='Applicant'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.name = objODBCDatareader["customer2user_name"].ToString();
                        values.age = objODBCDatareader["dobage"].ToString();
                        values.aadhar_no = objODBCDatareader["aadhar_no"].ToString();
                        values.gender = objODBCDatareader["customer2user_gender"].ToString();
                        values.personalemail_address = objODBCDatareader["personalemail_address"].ToString();
                        values.officailemail_address = objODBCDatareader["officialemail_address"].ToString();
                        values.telephone_no = objODBCDatareader["telephone_no"].ToString();
                        values.contact_person = objODBCDatareader["contact_person"].ToString();
                        values.pan_no = objODBCDatareader["pan_no"].ToString();
                        values.user_type = objODBCDatareader["user_type"].ToString();
                        values.escrow = objODBCDatareader["escrow"].ToString();
                        values.credit_rating = objODBCDatareader["credit_rating"].ToString();
                        values.month_business = objODBCDatareader["month_business"].ToString();
                        values.landmark = objODBCDatareader["landmark"].ToString();
                        if (objODBCDatareader["cin_date"].ToString() == "")
                        {
                        }
                        else
                        {
                            values.cin_date = Convert.ToDateTime(objODBCDatareader["cin_date"]).ToString("MM-dd-yyyy");
                        }
                        values.contactperson_designation = objODBCDatareader["contactperson_designation"].ToString();
                        values.company_type = objODBCDatareader["company_type"].ToString();
                        values.cin_no = objODBCDatareader["cin_no"].ToString();
                        values.year_business = objODBCDatareader["year_business"].ToString();
                        values.gst_no = objODBCDatareader["gst_no"].ToString();
                        values.customer_type = objODBCDatareader["customer_type"].ToString();
                    }
                    objODBCDatareader.Close();
                    values.status = true;
                    values.message = "success";
                }
                else
                {
                    msSQL = " select tmpcustomer_gid from ocs_tmp_tcustomer where tmpcustomer_gid ='" + customer_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        lscustomer_gid = objODBCDatareader["tmpcustomer_gid"].ToString();
                        objODBCDatareader.Close();
                        msSQL = "select customer_urn,vertical_code,zonal_name,businesshead_name,relationshipmgmt_name,cluster_manager_name,creditmgmt_name,constitution_name," +
                     " sa_payout,sa_idname,secondaryvaluechain_name,primaryvaluechain_name,SA_status,businessunit_name,customer_type from ocs_tmp_tcustomer where tmpcustomer_gid='" + lscustomer_gid + "'";
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
                            values.customer_type = objODBCDatareader["customer_type"].ToString();
                        }
                        objODBCDatareader.Close();
                        msSQL = "select customer2usertype_gid from ocs_tmp_tcustomer2userdtl where tmpcustomer_gid='" + lscustomer_gid + "' and user_type='Applicant'";
                        lscustomer2usertype_gid = objdbconn.GetExecuteScalar(msSQL);


                        msSQL = "select address_type,addressline1,addressline2, city,state,taluka,country,district,postal_code,customer2address_gid,primary_address" +
                            " from ocs_tmp_tcustomer2address where " +
                          " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
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
                          " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
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
                           " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
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
                          " customer2usertype_gid='" + lscustomer2usertype_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getmember_list = new List<institutionmember_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dr_datarow in dt_datatable.Rows)
                            {
                                getmember_list.Add(new institutionmember_list
                                {
                                    member_designation = (dr_datarow["member_designation"].ToString()),
                                    member_name = (dr_datarow["member_name"].ToString()),
                                    customer2member_gid = (dr_datarow["customer2member_gid"].ToString()),
                                });
                            }
                            values.institutionmember_list = getmember_list;
                        }
                        dt_datatable.Dispose();
                        msSQL = " select customer2user_name,concat(date_format(customer2user_dob,'%d-%m-%Y'),'/',customer2user_age) as dobage,customer2user_gender,personalemail_address,officialemail_address," +
                               " telephone_no,contact_person,aadhar_no,pan_no,user_type,escrow,credit_rating,month_business,landmark,date_format(cin_date,'%d-%m-%Y') as cin_date," +
                               " contactperson_designation,company_type,cin_date,cin_no,year_business,gst_no,customer_type from ocs_tmp_tcustomer2userdtl where" +
                           " tmpcustomer_gid='" + lscustomer_gid + "' and user_type='Applicant'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            values.name = objODBCDatareader["customer2user_name"].ToString();
                            values.age = objODBCDatareader["dobage"].ToString();
                            values.aadhar_no = objODBCDatareader["aadhar_no"].ToString();
                            values.gender = objODBCDatareader["customer2user_gender"].ToString();
                            values.personalemail_address = objODBCDatareader["personalemail_address"].ToString();
                            values.officailemail_address = objODBCDatareader["officialemail_address"].ToString();
                            values.telephone_no = objODBCDatareader["telephone_no"].ToString();
                            values.contact_person = objODBCDatareader["contact_person"].ToString();
                            values.pan_no = objODBCDatareader["pan_no"].ToString();
                            values.user_type = objODBCDatareader["user_type"].ToString();
                            values.escrow = objODBCDatareader["escrow"].ToString();
                            values.credit_rating = objODBCDatareader["credit_rating"].ToString();
                            values.month_business = objODBCDatareader["month_business"].ToString();
                            values.landmark = objODBCDatareader["landmark"].ToString();
                            if (objODBCDatareader["cin_date"].ToString() == "")
                            {
                            }
                            else
                            {
                                values.cin_date = Convert.ToDateTime(objODBCDatareader["cin_date"]).ToString("MM-dd-yyyy");
                            }
                            values.contactperson_designation = objODBCDatareader["contactperson_designation"].ToString();
                            values.company_type = objODBCDatareader["company_type"].ToString();
                            values.cin_no = objODBCDatareader["cin_no"].ToString();
                            values.year_business = objODBCDatareader["year_business"].ToString();
                            values.gst_no = objODBCDatareader["gst_no"].ToString();
                            values.customer_type = objODBCDatareader["customer_type"].ToString();
                        }
                        objODBCDatareader.Close();
                        values.status = true;
                        values.message = "success";
                    }
                }
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }
        public bool DaGetsanctionloandetails(sanctionloanurn values, string customer_gid)
        {
           
                lscustomer_gid = customer_gid;
           
            msSQL = " select customer2sanction_gid,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanctiondate, " +
                    " a.sanction_type,format(sanction_amount, 2) as sanction_amount,facility_type," +
                    " a.entity,a.colanding_status,a.colander_name from ocs_mst_tcustomer2sanction a " +
                    " where a.customer_gid = '" + lscustomer_gid + "' order by a.customer2sanction_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsanctionlistdtl = new List<sanctionloanListurn>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getsanctionlistdtl.Add(new sanctionloanListurn
                    {
                        sanction_amount = (dr_datarow["sanction_amount"].ToString()),
                        sanction_refno = (dr_datarow["sanction_refno"].ToString()),
                        sanction_date = (dr_datarow["sanctiondate"].ToString()),
                        sanction_gid = (dr_datarow["customer2sanction_gid"].ToString()),
                        facility_type = (dr_datarow["facility_type"].ToString()),
                        sanction_type = (dr_datarow["sanction_type"].ToString()),
                        entity = (dr_datarow["entity"].ToString()),
                        colanding_status = (dr_datarow["colanding_status"].ToString()),
                        colander_name = (dr_datarow["colander_name"].ToString()),
                    });
                    values.sanctionloanListurn = getsanctionlistdtl;
                }
            }
            dt_datatable.Dispose();

            msSQL = " select file_name,file_path,document_name,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                    " date_format(a.created_date,'%d-%m-%Y') as created_date   from rsk_mst_tsanctiondocumentdtl a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " where  customer_gid = '" + lscustomer_gid + "' and file_path<>''";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_fileseekname = new List<upload_listurn>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_fileseekname.Add(new upload_listurn
                    {
                        document_name = dr_datarow["file_name"].ToString(),
                        document_path = objcmnstorage.EncryptData(dr_datarow["file_path"].ToString()),
                        document_type = (dr_datarow["document_name"].ToString()),
                        created_by = dr_datarow["created_by"].ToString(),
                        created_date = dr_datarow["created_date"].ToString(),
                    });
                }
                values.upload_listurn = get_fileseekname;
            }
            dt_datatable.Dispose();


            values.status = true;
            return true;
        }
        public bool DaGetloanFacilityDetails(string sanction_gid,sanctionloanurn values)
        {

            msSQL = "select format(loanfacility_amount,2) as loanfacility_amount,loanfacility_type, " +
                  " format(document_limit,2) as document_limit,date_format(expiry_date, '%d-%m-%Y') as expiry_date,tenure,loanfacilityref_no,proposed_roi" +
                  " from ocs_mst_tsanction2loanfacilitytype  where customer2sanction_gid='" + sanction_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloanfacilitytype = new List<loanfacilitytype_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getloanfacilitytype.Add(new loanfacilitytype_list
                    {
                        loanfacility_amount = (dr_datarow["loanfacility_amount"].ToString()),
                        loanfacility_type = (dr_datarow["loanfacility_type"].ToString()),
                        document_limit = (dr_datarow["document_limit"].ToString()),
                        expiry_date = (dr_datarow["expiry_date"].ToString()),
                        tenure = (dr_datarow["tenure"].ToString()),
                         loanfacilityref_no = (dr_datarow["loanfacilityref_no"].ToString()),
                        proposed_roi = (dr_datarow["proposed_roi"].ToString()),
                    });
                }
                values.loanfacilitytype_list = getloanfacilitytype;
            }
            dt_datatable.Dispose();

            values.status = true;
            return true;
        }
        public bool DaGetCustomer2Loandtls(string customer_gid, string employee_gid, MdlMisdataimportloanlist values)
        {
            msSQL = "select customer_urn from ocs_mst_tcustomer where customer_gid ='" + customer_gid + "'";
            string lscustomer_urn = objdbconn.GetExecuteScalar(msSQL);
            if (lscustomer_urn == "" || lscustomer_urn == null)
            {
                msSQL = "select customer_urn from ocs_tmp_tcustomer where tmpcustomer_gid='" + customer_gid + "'";
                lscustomer_urn = objdbconn.GetExecuteScalar(msSQL);
            }
            msSQL = " select a.account_no,a.Customer_name,a.urn,a.od_days,a.maturity_date,a.schedulde_payment,a.frequency,a.tenure,a.RO_name,a.vertical,a.Guarantor_Name," +
                " b.DN_status,format(b.TotalDemandDu,2) as TotalDemandDu,format(b.Net_Payoff_Amt,2) as Net_Payoff_Amt from lgl_tmp_tmisdata a" +
                " left join lgl_trn_tmisdata b on a.urn = b.urn and a.account_no=b.account_no where a.urn = b.urn and b.urn = '" + lscustomer_urn + "' and" +
                " a.ac_status = '0' group by a.account_no";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer2Loan = new List<MdlMisdataimport>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomer2Loan.Add(new MdlMisdataimport
                    {
                        od_days = (dr_datarow["od_days"].ToString()),
                        account_no = (dr_datarow["account_no"].ToString()),
                        maturity_date = (dr_datarow["maturity_date"].ToString()),
                        schedulde_payment = (dr_datarow["schedulde_payment"].ToString()),
                        Customer_name = (dr_datarow["Customer_name"].ToString()),
                        Guarantor_Name = (dr_datarow["Guarantor_Name"].ToString()),
                        Vertical = (dr_datarow["Vertical"].ToString()),
                        RO_Name = (dr_datarow["RO_Name"].ToString()),
                        urn = (dr_datarow["urn"].ToString()),
                        frequency = dr_datarow["frequency"].ToString(),
                        tenure = dr_datarow["tenure"].ToString(),
                        DNstatus = dr_datarow["DN_status"].ToString(),
                        TotalDemandDu = dr_datarow["TotalDemandDu"].ToString(),
                        Net_Payoff_Amt = dr_datarow["Net_Payoff_Amt"].ToString()
                    });
                }
                values.mdlMisdataimport = getcustomer2Loan;
            }
            dt_datatable.Dispose();
            msSQL = "select customer_name from lgl_trn_tmisdata where urn='" + lscustomer_urn + "' group by urn";
            values.customer_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select DN1status,DN3status,DN2status,cbo_status from lgl_trn_tdncases where urn='" + lscustomer_urn + "' and status <> 'Closed' ";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.DN1status = objODBCDatareader["DN1status"].ToString();
                values.DN2status = objODBCDatareader["DN2status"].ToString();
                values.DN3status = objODBCDatareader["DN3status"].ToString();
                values.cbo_status = objODBCDatareader["cbo_status"].ToString();
            }
            else
            {
                values.DN1status = "Pending";
                values.DN2status = "Pending";
                values.DN3status = "Pending";
                values.cbo_status = "Pending";
            }

            objODBCDatareader.Close();

            values.status = true;
            return true;
        }

        public void DaGetCustomerCibilSummary(string account_no, MdlCibilSummarydtl values)
        {
            msSQL = " select cibildatadtl_gid,account_no,name,indicator,submission_type,account_status,format(overdue_amount,2,'en_IN') as overdue_amount,submitted_on" +
                " from ocs_trn_tcibildatadtl  where account_no ='" + account_no + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcibilsummary = new List<cibilsummarydtl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getcibilsummary.Add(new cibilsummarydtl_list
                    {
                        account_no = dt["account_no"].ToString(),
                        name = dt["name"].ToString(),
                        indicator = dt["indicator"].ToString(),
                        submission_type = dt["submission_type"].ToString(),
                        account_status = dt["account_status"].ToString(),
                        overdue_amount = dt["overdue_amount"].ToString(),
                        cibildatadtl_gid = dt["cibildatadtl_gid"].ToString(),
                        submitted_on =dt["submitted_on"].ToString(),
                    });
                }
                values.cibilsummarydtl_list = getcibilsummary;
            }
            dt_datatable.Dispose();
        }

        public bool DaGetCustomerCibilView(string cibildatadtl_gid, MdlCibilViewdtl values)
        {
            msSQL = " select cibildatadtl_gid,submission_type,submitted_on,case when indicator='01' then 'Borrower' else 'Guarantor' end as indicator,name,account_no," +
                " format(current_balance,2,'en_IN') as current_balance,format(overdue_amount,2,'en_IN') as overdue_amount,odbucket_01,odbucket_02,odbucket_03,odbucket_04,odbucket_05,od_days," +
                " account_status, closed_on,cibil,highmark,experian,euifax from ocs_trn_tcibildatadtl  where cibildatadtl_gid ='" + cibildatadtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Read();
                values.submission_type = objODBCDatareader["submission_type"].ToString();
                if (objODBCDatareader["submitted_on"].ToString() == "")
                {
                }
                else
                {
                    values.submitted_on = Convert.ToDateTime(objODBCDatareader["submitted_on"]).ToString("dd-MM-yyyy");
                }
                //values.submitted_on = objODBCDatareader["submitted_on"].ToString();
                values.indicator = objODBCDatareader["indicator"].ToString();
                values.name = objODBCDatareader["name"].ToString();
                values.account_no = objODBCDatareader["account_no"].ToString();
                values.current_balance = objODBCDatareader["current_balance"].ToString();
                values.overdue_amount = objODBCDatareader["overdue_amount"].ToString();
                values.odbucket_01 = objODBCDatareader["odbucket_01"].ToString();
                values.odbucket_02 = objODBCDatareader["odbucket_02"].ToString();
                values.odbucket_03 = objODBCDatareader["odbucket_03"].ToString();
                values.odbucket_04 = objODBCDatareader["odbucket_04"].ToString();
                values.odbucket_05 = objODBCDatareader["odbucket_05"].ToString();
                values.od_days = objODBCDatareader["od_days"].ToString();
                values.account_status = objODBCDatareader["account_status"].ToString();
                if (objODBCDatareader["closed_on"].ToString() == "")
                {
                }
                else
                {
                    values.closed_on = Convert.ToDateTime(objODBCDatareader["closed_on"]).ToString("dd-MM-yyyy");
                }
                //values.closed_on = objODBCDatareader["closed_on"].ToString();
                values.cibil = objODBCDatareader["cibil"].ToString();
                values.highmark = objODBCDatareader["highmark"].ToString();
                values.experian = objODBCDatareader["experian"].ToString();
                values.euifax = objODBCDatareader["euifax"].ToString();
            }
            objODBCDatareader.Close();
            return true;
        }
    }
}