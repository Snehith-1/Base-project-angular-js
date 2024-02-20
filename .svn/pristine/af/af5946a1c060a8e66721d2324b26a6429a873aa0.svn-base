using System.Web;
using ems.utilities.Functions;
using ems.storage.Functions;
using ems.lgl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Configuration;

namespace ems.lgl.DataAccess
{
    public class DalglTrnDn2CustomerDetails
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGETGID;
        string lscustomer_gid, lscustomer2usertype_gid, lsurn, lspath;
        int mnResult;
        string  lsod_days, lsdntemplate_content, lsnatureof_credit_amount;
        string lscontent = string.Empty;
        HttpPostedFile httpPostedFile;

        public bool DaGetsanctionloandetails(sanctionloanurn values, string urn)
        {
            msSQL = " select customer_gid from ocs_mst_tcustomer where customer_urn ='" + urn + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            objODBCDatareader.Read();
            if (objODBCDatareader.HasRows== true)
            {
                lscustomer_gid = objODBCDatareader["customer_gid"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " select customer2sanction_gid,a.sanction_refno,date_format(a.sanction_date,'%d-%m-%Y') as sanctiondate, " +
                    " a.sanction_type,format(sanction_amount, 2) as sanction_amount,facility_type,concat(sanction_limit,'.00') as sanction_limit" +
                    " from ocs_mst_tcustomer2sanction a " +
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
                        sanction_limit = dr_datarow["sanction_limit"].ToString(),
                        sanction_refno = (dr_datarow["sanction_refno"].ToString()),
                        sanction_date = (dr_datarow["sanctiondate"].ToString()),
                        sanction_gid = (dr_datarow["customer2sanction_gid"].ToString()),
                        facility_type = (dr_datarow["facility_type"].ToString()),
                        sanction_type = (dr_datarow["sanction_type"].ToString()),
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
                        document_path = objcmnstorage.EncryptData((dr_datarow["file_path"].ToString())),
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

        public void DaGetEditCustomerurn(string urn, customerediturn values)
        {
            try
            {
                msSQL = " select customer_gid from ocs_mst_tcustomer where customer_urn ='" + urn + "'";
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
                    objODBCDatareader.Close();
                }
                else
                {
                    msSQL = " select tmpcustomer_gid from ocs_tmp_tcustomer where customer_urn ='" + urn + "'";
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
                        objODBCDatareader.Close();

                    }
                    

                }
                
                values.status = true;
                values.message = "success";

            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetloanListDetails(string sanction_gid, loanListdetailurn values)
        {
            msSQL = " select sanction_gid,loanref_no,loan_title from ocs_trn_tloan " +
                    " where sanction_gid='" + sanction_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloanlistdtl = new List<loanListurn>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt_datarow in dt_datatable.Rows)
                {
                    getloanlistdtl.Add(new loanListurn
                    {
                        loanref_no = (dt_datarow["loanref_no"].ToString()),
                        loan_title = dt_datarow["loan_title"].ToString(),
                        sanction_gid = (dt_datarow["sanction_gid"].ToString()),
                    });
                    values.loanListurn = getloanlistdtl;
                }
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetcustomerdetails(string urn, mdlcustomer2userdtl values)
        {
            try
            {
                msSQL = " select customer_gid from ocs_mst_tcustomer where customer_urn ='" + urn + "'";
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
                    msSQL = " select tmpcustomer_gid from ocs_tmp_tcustomer where customer_urn ='" + urn + "'";
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

        public void DaGetGuarantordetails(string urn, mdlcustomer2userdtl values)
        {
            try
            {
                msSQL = " select customer_gid from ocs_mst_tcustomer where customer_urn ='" + urn + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                objODBCDatareader.Read();
                if (objODBCDatareader.HasRows == true)
                {
                    lscustomer_gid = objODBCDatareader["customer_gid"].ToString();
                }
                objODBCDatareader.Close();
               
             
                msSQL = " select customer2user_name,concat(date_format(customer2user_dob,'%d-%m-%Y'),customer2user_age) as dobage,customer2user_gender,personalemail_address,officialemail_address," +
                      " telephone_no,contact_person,aadhar_no,pan_no,user_type,escrow,credit_rating,month_business,landmark,date_format(cin_date,'%d-%m-%Y') as cin_date," +
                      " contactperson_designation,company_type,cin_date,cin_no,year_business,gst_no,customer_type,customer2usertype_gid from ocs_mst_tcustomer2userdtl where" +
                  " customer_gid='" + lscustomer_gid + "' and user_type<>'Applicant'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomer2userdtl_list = new List<customer2userdtl_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomer2userdtl_list.Add(new customer2userdtl_list
                        {
                            name = (dr_datarow["customer2user_name"].ToString()),
                            dob = (dr_datarow["dobage"].ToString()),
                            gender = (dr_datarow["customer2user_gender"].ToString()),
                            personalemail_address = (dr_datarow["personalemail_address"].ToString()),
                            officailemail_address = (dr_datarow["officialemail_address"].ToString()),
                            customer_type = (dr_datarow["customer_type"].ToString()),
                            telephone_no = (dr_datarow["telephone_no"].ToString()),
                            contact_person = (dr_datarow["contact_person"].ToString()),
                            aadhar_no = (dr_datarow["aadhar_no"].ToString()),
                            pan_no = (dr_datarow["pan_no"].ToString()),
                            user_type = (dr_datarow["user_type"].ToString()),
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
                            customer2usertype_gid = (dr_datarow["customer2usertype_gid"].ToString())
                        });
                    }
                    values.customer2userdtl_list = getcustomer2userdtl_list;
                }
                dt_datatable.Dispose();



            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }
        public void DaGetGuarantorlist(string urn, mdlcustomer2userdtl values)
        {
            try
            {
                msSQL = " select customer_gid from ocs_mst_tcustomer where customer_urn ='" + urn + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                objODBCDatareader.Read();
                if (objODBCDatareader.HasRows == true)
                {
                    lscustomer_gid = objODBCDatareader["customer_gid"].ToString();
                }
                objODBCDatareader.Close();


                msSQL = " select concat(customer2user_name,'/ ',pan_no) as guarantorname,customer2usertype_gid from ocs_mst_tcustomer2userdtl where" +
                  " customer_gid='" + lscustomer_gid + "' and user_type='Guarantor'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcustomer2userdtl_list = new List<customer2userdtl_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcustomer2userdtl_list.Add(new customer2userdtl_list
                        {
                            name = (dr_datarow["guarantorname"].ToString()),
                           
                            customer2usertype_gid = (dr_datarow["customer2usertype_gid"].ToString())
                        });
                    }
                    values.customer2userdtl_list = getcustomer2userdtl_list;
                }
                dt_datatable.Dispose();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public bool DaPostDN1sanctiondtl(DNsanctiondtl values, string employee_gid)
        {
            msSQL = "select customer_urn from lgl_trn_tsanctiondtl where customer_urn='" + values.urn + "'  and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select sanction_date from ocs_mst_tcustomer2sanction where sanction_refno='" + values.dnsanctionref_no + "' and customer_urn='" + values.urn + "'";
            string lssanction_date = objdbconn.GetExecuteScalar(msSQL);

            if (lsurn == "")
            {

                msGetGid = objcmnfunctions.GetMasterGID("DNSA");
                msSQL = "insert into lgl_trn_tsanctiondtl(" +
                      " sanction_gid," +
                      " customer_urn," +
                      " dn1ref_no," +
                      " dn1sanctionref_no," +
                      " dn1sanction_date," +
                      " dn1sanction_amount," +
                      " dn1_flag, " +
                      " dn1user_type," +
                      " dn1template_type," +
                      " dn1guarantor_name," +
                      " dn1template_gid," +
                      " dn1guarantor_gid," +
                      " dn_type," +
                      " created_by ," +
                      " created_date)" +
                      " values (" +
                      "'" + msGetGid + "'," +
                       "'" + values.urn + "'," +
                       "'" + values.dnref_no + "'," +
                       "'" + values.dnsanctionref_no + "',";


                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "'" + Convert.ToDateTime(values.dnsanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "'" + lssanction_date + "',";
                }

                msSQL += "'" + values.dnsanction_amount.Replace(",", "") + "'," +
                  "'Y'," +
                  "'" + values.user_type + "'," +
                  "'" + values.template_type + "'," +
                  "'" + values.guarantor_name + "'," +
                   "'" + values.template_gid + "'," +
                  "'" + values.guarantor_gid + "'," +
                  "'DN1'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select document_name,document_path from lgl_trn_tdn1annexuredocupload where sanction_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    msSQL = "update lgl_trn_tsanctiondtl set dn1annexuredocument_name='" + objODBCDatareader["document_name"].ToString() + "'," +
                        " dn1annexuredocument_path ='" + objODBCDatareader["document_path"].ToString() + "' where sanction_gid='" + msGetGid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                   

                    if (mnResult != 0)
                    {
                        msSQL = "delete from lgl_trn_tdn1annexuredocupload where sanction_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objODBCDatareader.Close();

            }
            else
            {

                msSQL = " update lgl_trn_tsanctiondtl set dn1ref_no='" + values.dnref_no + "'," +
                     " dn1sanctionref_no='" + values.dnsanctionref_no + "',";
                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "dn1sanction_date='" + Convert.ToDateTime(values.dnsanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "dn1sanction_date='" + lssanction_date + "',";
                }

                msSQL += " dn1sanction_amount='" + values.dnsanction_amount.Replace(",", "") + "'," +
                       " dn1_flag='Y'," +
                       " dn1user_type='" + values.user_type + "'," +
                       " dn1template_type='" + values.template_type + "'," +
                       " dn1guarantor_name='" + values.guarantor_name+ "'," +
                        " dn1template_gid='" + values.template_gid + "'," +
                       " dn1guarantor_gid='" + values.guarantor_gid + "'," +
                       " dn_type='DN1',"+
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer_urn='" + values.urn + "'  and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select document_name,document_path from lgl_trn_tdn1annexuredocupload where sanction_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    msSQL = "update lgl_trn_tsanctiondtl set dn1annexuredocument_name='" + objODBCDatareader["document_name"].ToString() + "'," +
                        " dn1annexuredocument_path ='" + objODBCDatareader["document_path"].ToString() + "' where customer_urn='" + values.urn + "'  and status<>'Closed'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    

                    if (mnResult != 0)
                    {
                        msSQL = "delete from lgl_trn_tdn1annexuredocupload where sanction_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objODBCDatareader.Close();
            }
            if (mnResult != 0)
            {
                values.message = "Sanction Details Added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = false;
                return false;
            }
        }
        public bool DaGetTemplateDN1Content(template_list values, string urn)
        {
            msSQL= "select dn1template_gid,dn1user_type,dn1guarantor_gid,dn1sanctionref_no,format(dn1sanction_amount,2) as dn1sanction_amount,"+
                " date_format(dn1sanction_date,'%d-%m-%Y') as dn1sanction_date," +
                " dn1ref_no from lgl_trn_tsanctiondtl where customer_urn='" + urn + "' and status<>'Closed'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows==true)
            {
                //Get Template Content
                msSQL = " select  a.template_content from adm_mst_ttemplate a " +
                  " where a.template_gid='" + objODBCDatareader["dn1template_gid"].ToString() + "'";
                lsdntemplate_content = objdbconn.GetExecuteScalar(msSQL);
                //Get Address Information
                if(objODBCDatareader["dn1user_type"].ToString()=="guarantor")
                {
                    msSQL = " select * from ocs_mst_tcustomer2userdtl " +
                      " where customer2usertype_gid='" + objODBCDatareader["dn1guarantor_gid"].ToString() + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        objODBCDatareader1.Read();
                        values.customer_mail = objODBCDatareader1["personalemail_address"].ToString();
                        values.customer_name = objODBCDatareader1["customer2user_name"].ToString();
                        values.address1 = objODBCDatareader1["addressline1"].ToString();
                        values.address2 = objODBCDatareader1["addressline2"].ToString();
                        values.mobile_no = objODBCDatareader1["mobile_no"].ToString();
                        values.email_address = objODBCDatareader1["personalemail_address"].ToString();
                    }
                    objODBCDatareader1.Close();
                }
                else
                {
                    msSQL = " select customername,email,contactperson,address,address2,mobileno,email from ocs_mst_tcustomer " +
                       " where customer_urn='" + urn + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        objODBCDatareader1.Read();
                        values.customer_mail = objODBCDatareader1["email"].ToString();
                        values.customer_name = objODBCDatareader1["customername"].ToString();
                        values.address1 = objODBCDatareader1["address"].ToString();
                        values.address2 = objODBCDatareader1["address2"].ToString();
                        values.mobile_no = objODBCDatareader1["mobileno"].ToString();
                        values.email_address = objODBCDatareader1["email"].ToString();
                    }
                    objODBCDatareader1.Close();
                }
               

                msSQL = "select max(cast(od_days as unsigned))  as od_days  from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
                lsod_days = objdbconn.GetExecuteScalar(msSQL);

                lscontent = lsdntemplate_content;
                lscontent = lscontent.Replace("now_date", DateTime.Now.ToString("dd-MM-yyyy"));
                //  lscontent = lscontent.Replace("customer_name", values.customer_name);
                lscontent = lscontent.Replace("Customer", values.customer_name + ",");
                lscontent = lscontent.Replace("addressline1", values.address1);
                lscontent = lscontent.Replace("addressline2", values.address2);
                lscontent = lscontent.Replace("od_days", lsod_days);
                lscontent = lscontent.Replace("mobile_no", values.mobile_no);
                lscontent = lscontent.Replace("email_address", values.email_address);


                values.dn1sanctionref_no = objODBCDatareader["dn1sanctionref_no"].ToString();
                values.dn1sanction_amount = objODBCDatareader["dn1sanction_amount"].ToString();
                values.dn1sanction_date = objODBCDatareader["dn1sanction_date"].ToString();
                values.dn1ref_no = objODBCDatareader["dn1ref_no"].ToString();

                string fare = objODBCDatareader["dn1sanction_amount"].ToString();
                decimal parsed = decimal.Parse(fare, System.Globalization.CultureInfo.InvariantCulture);
                System.Globalization.CultureInfo hindi = new System.Globalization.CultureInfo("hi-IN");
                string text = string.Format(hindi, "{0:c}", parsed);

                msSQL = "select substring('" + text + "',2,20)";
                string dn1sanction_amount = objdbconn.GetExecuteScalar(msSQL);
                //   var amount = new intl.NumberFormat("en-IN").format(objODBCDatareader["dn1sanction_amount"].ToString());

                lscontent = lscontent.Replace("sanctionref_no", values.dn1sanctionref_no);
                lscontent = lscontent.Replace("sanction_date", values.dn1sanction_date);
                lscontent = lscontent.Replace("sanction_amount", dn1sanction_amount);
                lscontent = lscontent.Replace("dn1ref_no", values.dn1ref_no);

                msSQL = "select format(natureof_credit_amount,2) as amount  from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
                lsnatureof_credit_amount = objdbconn.GetExecuteScalar(msSQL);

                string amount = lsnatureof_credit_amount;
                decimal parsed1 = decimal.Parse(amount, System.Globalization.CultureInfo.InvariantCulture);
                System.Globalization.CultureInfo indian1 = new System.Globalization.CultureInfo("hi-IN");
                string amount_format = string.Format(indian1, "{0:c}", parsed1);

                msSQL = "select substring('" + amount_format + "',2,20)";
                string totaldemand_amount = objdbconn.GetExecuteScalar(msSQL);

                lscontent = lscontent.Replace("totaldemand_amount", totaldemand_amount);

                values.template_content = lscontent;
                
            }
            objODBCDatareader.Close();


            values.status = true;
            return true;
        }

        public bool DaGettemplate_list( mdltemplate values)
        {
            msSQL = " select a.template_gid,template_name from adm_mst_ttemplate a" +
                    " left join adm_trn_ttemplate2module b on a.template_gid = b.template_gid where module_gid = 'LGL' and template_name  not like '%CBO%'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettemplatelist = new List<template_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettemplatelist.Add(new template_list
                    {
                        template_gid = (dr_datarow["template_gid"].ToString()),
                        template_name = (dr_datarow["template_name"].ToString()),                     
                    });
                }
                values.template_list = gettemplatelist;
            }
            dt_datatable.Dispose();


            values.status = true;
            return true;
        }
        public bool DaGetDN2template_list(mdltemplate values)
        {
            msSQL = " select a.template_gid,template_name from adm_mst_ttemplate a" +
                    " left join adm_trn_ttemplate2module b on a.template_gid = b.template_gid where module_gid = 'LGL' and template_name  not like '%DN1%'"+
                    " and template_name  not like '%CBO%' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettemplatelist = new List<template_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettemplatelist.Add(new template_list
                    {
                        template_gid = (dr_datarow["template_gid"].ToString()),
                        template_name = (dr_datarow["template_name"].ToString()),
                    });
                }
                values.template_list = gettemplatelist;
            }
            dt_datatable.Dispose();


            values.status = true;
            return true;
        }
        public bool DaGetDN3template_list(mdltemplate values)
        {
            msSQL = " select a.template_gid,template_name from adm_mst_ttemplate a" +
                    " left join adm_trn_ttemplate2module b on a.template_gid = b.template_gid where module_gid = 'LGL' and "+
                    " (template_name not like ('%DN1%') and template_name not like ( '%DN2%') and template_name not like '%CBO%' )";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettemplatelist = new List<template_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettemplatelist.Add(new template_list
                    {
                        template_gid = (dr_datarow["template_gid"].ToString()),
                        template_name = (dr_datarow["template_name"].ToString()),
                    });
                }
                values.template_list = gettemplatelist;
            }
            dt_datatable.Dispose();


            values.status = true;
            return true;
        }
        public bool DaGetDN1Cancel(template_list values, string urn,string employee_gid)
        {
            msSQL = "delete from lgl_trn_tsanctiondtl where customer_urn='" + urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from lgl_trn_tdn1annexuredocupload where sanction_gid='"+employee_gid+"'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult!=0)
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

        public bool DaGetDN2Cancel(template_list values, string urn, string employee_gid)
        {
            msSQL = "delete from lgl_trn_tsanctiondtl where customer_urn='" + urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from lgl_trn_tdn2annexuredocupload where sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
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
        public bool DaGetDN3Cancel(template_list values, string urn, string employee_gid)
        {
            msSQL = "delete from lgl_trn_tsanctiondtl where customer_urn='" + urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "delete from lgl_trn_tdn3annexuredocupload where sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
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
        public bool DaGetSanctionDN2Cancel(template_list values, string urn, string employee_gid)
        {
            msSQL = " update lgl_trn_tsanctiondtl set "+
                "  dn2sanction_date=null,"+
                " dn2sanction_amount='0.00',"+
                " dn2sanctionref_no=null,"+
                " dn2_flag='N',"+
                " dn2ref_no=null,"+
                " dn2guarantor_name=null,"+
                " dn2template_type=null,"+
                " dn2user_type=null,"+
                " dn2guarantor_gid=null,"+
                " dn2template_gid=null,"+
                " dn2annexuredocument_name=null,"+
                " dn2annexuredocument_path=null where customer_urn ='" + urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from lgl_trn_tdn2annexuredocupload where sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
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
        public bool DaGetSanctionDN3Cancel(template_list values, string urn, string employee_gid)
        {
            msSQL = " update lgl_trn_tsanctiondtl set " +
                 "  dn3sanction_date=null," +
                 " dn3sanction_amount='0.00'," +
                 " dn3sanctionref_no=null," +
                 " dn3_flag='N'," +
                 " dn3ref_no=null," +
                 " dn3guarantor_name=null," +
                 " dn3template_type=null," +
                 " dn3user_type=null," +
                 " dn3guarantor_gid=null," +
                 " dn3template_gid=null," +
                 " dn3annexuredocument_name=null," +
                 " dn3annexuredocument_path=null where customer_urn ='" + urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
             
            msSQL = "delete from lgl_trn_tdn3annexuredocupload where sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
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
        public bool DaPostDN2sanctiondtl(DNsanctiondtl values, string employee_gid)
        {
            msSQL = "select customer_urn from lgl_trn_tsanctiondtl where customer_urn='" + values.urn + "'  and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select sanction_date from ocs_mst_tcustomer2sanction where sanction_refno='" + values.dn2sanctionref_no + "' and customer_urn='" + values.urn + "'";
            string lssanction_date = objdbconn.GetExecuteScalar(msSQL);

            if (lsurn == "")
            {

                msGetGid = objcmnfunctions.GetMasterGID("DNSA");
                msSQL = "insert into lgl_trn_tsanctiondtl(" +
                      " sanction_gid," +
                      " customer_urn," +
                      " dn2ref_no," +
                      " dn2sanctionref_no," +
                      " dn2sanction_date," +
                      " dn2sanction_amount," +
                      " dn2_flag," +
                      " dn2user_type," +
                      " dn2template_type," +
                      " dn2guarantor_name," +
                      " dn2template_gid," +
                      " dn2guarantor_gid," +
                      " dn_type,"+
                      " created_by ," +
                      " created_date)" +
                      " values (" +
                      "'" + msGetGid + "'," +
                       "'" + values.urn + "'," +
                       "'" + values.dnref_no + "'," +
                       "'" + values.dnsanctionref_no + "',";
                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "'" + Convert.ToDateTime(values.dnsanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "'" + lssanction_date + "',";
                }

                msSQL += "'" + values.dnsanction_amount.Replace(",", "") + "'," +
                       "'Y'," +
                        "'" + values.user_type + "'," +
                        "'" + values.template_type + "'," +
                        "'" + values.guarantor_name + "'," +
                        "'" + values.template_gid + "'," +
                        "'" + values.guarantor_gid + "'," +
                        "'DN2'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL= "select document_name,document_path from lgl_trn_tdn2annexuredocupload where sanction_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    msSQL = "update lgl_trn_tsanctiondtl set dn2annexuredocument_name='" + objODBCDatareader["document_name"].ToString() + "',"+
                        " dn2annexuredocument_path ='"+ objODBCDatareader["document_path"].ToString() + "' where sanction_gid='" + msGetGid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    

                    if(mnResult!=0)
                    {
                        msSQL = "delete from lgl_trn_tdn2annexuredocupload where sanction_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objODBCDatareader.Close();

            }
            else
            {
                msSQL = " update lgl_trn_tsanctiondtl set dn2ref_no='" + values.dnref_no + "'," +
                       " dn2sanctionref_no='" + values.dnsanctionref_no + "',";
                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "dn2sanction_date='" + Convert.ToDateTime(values.dnsanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "dn2sanction_date='" + lssanction_date + "',";
                }

                msSQL += " dn2sanction_amount='" + values.dnsanction_amount.Replace(",", "") + "'," +
                       " dn2_flag='Y'," +
                       " dn2user_type='" + values.user_type + "'," +
                       " dn2template_type='" + values.template_type + "'," +
                       " dn2guarantor_name='" + values.guarantor_name + "'," +
                       " dn2template_gid='" + values.template_gid + "'," +
                       " dn2guarantor_gid='" + values.guarantor_gid + "'," +
                        " dn_type='DN2'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer_urn='" + values.urn + "'  and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select document_name,document_path from lgl_trn_tdn2annexuredocupload where sanction_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    msSQL = "update lgl_trn_tsanctiondtl set dn2annexuredocument_name='" + objODBCDatareader["document_name"].ToString() + "'," +
                        " dn2annexuredocument_path ='" + objODBCDatareader["document_path"].ToString() + "'  where customer_urn='" + values.urn + "'  and status<>'Closed'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    

                    if (mnResult != 0)
                    {
                        msSQL = "delete from lgl_trn_tdn2annexuredocupload where sanction_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objODBCDatareader.Close();
            }
            if (mnResult != 0)
            {
                values.message = "Sanction Details Added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = false;
                return false;
            }


           
        }
        public bool DaGetTemplateDN2Content(template_list values, string urn)
        {
            msSQL = "select dn2template_gid,dn2user_type,dn2guarantor_gid,dn2sanctionref_no,format(dn2sanction_amount,2) as dn2sanction_amount," +
                " date_format(dn2sanction_date,'%d-%m-%Y') as dn2sanction_date,dn1ref_no,date_format(dn1generate_date,'%d-%m-%Y') as dn1sanction_date," +
                " dn2ref_no from lgl_trn_tsanctiondtl where customer_urn='" + urn + "' and status<>'Closed'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                //Get Template Content
                msSQL = " select  a.template_content from adm_mst_ttemplate a " +
                  " where a.template_gid='" + objODBCDatareader["dn2template_gid"].ToString() + "'";
                lsdntemplate_content = objdbconn.GetExecuteScalar(msSQL);
                //Get Address Information
                if (objODBCDatareader["dn2user_type"].ToString() == "guarantor")
                {
                    msSQL = " select * from ocs_mst_tcustomer2userdtl " +
                      " where customer2usertype_gid='" + objODBCDatareader["dn2guarantor_gid"].ToString() + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        objODBCDatareader1.Read();
                        values.customer_mail = objODBCDatareader1["personalemail_address"].ToString();
                        values.customer_name = objODBCDatareader1["customer2user_name"].ToString();
                        values.address1 = objODBCDatareader1["addressline1"].ToString();
                        values.address2 = objODBCDatareader1["addressline2"].ToString();
                        values.mobile_no = objODBCDatareader1["mobile_no"].ToString();
                        values.email_address = objODBCDatareader1["personalemail_address"].ToString();
                    }
                    objODBCDatareader1.Close();
                }
                else
                {
                    msSQL = " select customername,email,contactperson,address,address2,mobileno,email from ocs_mst_tcustomer " +
                       " where customer_urn='" + urn + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        objODBCDatareader1.Read();
                        values.customer_mail = objODBCDatareader1["email"].ToString();
                        values.customer_name = objODBCDatareader1["customername"].ToString();
                        values.address1 = objODBCDatareader1["address"].ToString();
                        values.address2 = objODBCDatareader1["address2"].ToString();
                        values.mobile_no = objODBCDatareader1["mobileno"].ToString();
                        values.email_address = objODBCDatareader1["email"].ToString();
                    }
                    objODBCDatareader1.Close();
                }


                msSQL = "select max(cast(od_days as unsigned))  as od_days  from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
                lsod_days = objdbconn.GetExecuteScalar(msSQL);

                lscontent = lsdntemplate_content;
                lscontent = lscontent.Replace("now_date", DateTime.Now.ToString("dd-MM-yyyy"));
                //  lscontent = lscontent.Replace("customer_name", values.customer_name);
                lscontent = lscontent.Replace("Customer", values.customer_name + ",");
                lscontent = lscontent.Replace("addressline1", values.address1);
                lscontent = lscontent.Replace("addressline2", values.address2);
                lscontent = lscontent.Replace("od_days", lsod_days);
                lscontent = lscontent.Replace("mobile_no", values.mobile_no);
                lscontent = lscontent.Replace("email_address", values.email_address);


                values.dn2sanctionref_no = objODBCDatareader["dn2sanctionref_no"].ToString();

                string fare = objODBCDatareader["dn2sanction_amount"].ToString();
                decimal parsed = decimal.Parse(fare, System.Globalization.CultureInfo.InvariantCulture);
                System.Globalization.CultureInfo hindi = new System.Globalization.CultureInfo("hi-IN");
                string text = string.Format(hindi, "{0:c}", parsed);

                msSQL = "select substring('" + text + "',2,20)";
                string dn2sanction_amount = objdbconn.GetExecuteScalar(msSQL);

                values.dn2sanction_date = objODBCDatareader["dn2sanction_date"].ToString();
                values.dn2ref_no = objODBCDatareader["dn2ref_no"].ToString();

                if (objODBCDatareader["dn1ref_no"].ToString() != "")
                {
                    values.dn1sanctionref_no = objODBCDatareader["dn1ref_no"].ToString();
                    values.dn1sanction_date = objODBCDatareader["dn1sanction_date"].ToString();
                }
                else
                {
                    values.dn1sanctionref_no = "_______";
                    values.dn1sanction_date = "_______";
                }
                lscontent = lscontent.Replace("dn2sanctionref_no", values.dn2sanctionref_no);
                lscontent = lscontent.Replace("dn2sanction_date", values.dn2sanction_date);
                lscontent = lscontent.Replace("dn2sanction_amount", dn2sanction_amount);
                lscontent = lscontent.Replace("dn2ref_no", values.dn2ref_no);
                lscontent = lscontent.Replace("dn1ref_no", values.dn1sanctionref_no);
                lscontent = lscontent.Replace("dn1sanction_date", values.dn1sanction_date);

                msSQL = "select format(natureof_credit_amount,2) as amount  from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
                lsnatureof_credit_amount = objdbconn.GetExecuteScalar(msSQL);

                string amount = lsnatureof_credit_amount;
                decimal parsed1 = decimal.Parse(amount, System.Globalization.CultureInfo.InvariantCulture);
                System.Globalization.CultureInfo indian1 = new System.Globalization.CultureInfo("hi-IN");
                string amount_format = string.Format(indian1, "{0:c}", parsed1);

                msSQL = "select substring('" + amount_format + "',2,20)";
                string totaldemand_amount = objdbconn.GetExecuteScalar(msSQL);

                lscontent = lscontent.Replace("totaldemand_amount", totaldemand_amount);

                values.template_content = lscontent;
                
            }
            objODBCDatareader.Close();

            values.status = true;
            return true;
        }

        public bool DaPostDN3sanctiondtl(DNsanctiondtl values, string employee_gid)
        {
            msSQL = "select customer_urn from lgl_trn_tsanctiondtl where customer_urn='" + values.urn + "'  and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select sanction_date from ocs_mst_tcustomer2sanction where sanction_refno='" + values.dnsanctionref_no + "' and customer_urn='" + values.urn + "'";
            string lssanction_date = objdbconn.GetExecuteScalar(msSQL);

            if (lsurn == "")
            {

                msGetGid = objcmnfunctions.GetMasterGID("DNSA");
                msSQL = "insert into lgl_trn_tsanctiondtl(" +
                      " sanction_gid," +
                      " customer_urn," +
                      " dn3ref_no," +
                      " dn3sanctionref_no," +
                      " dn3sanction_date," +
                      " dn3sanction_amount," +
                      " dn3_flag," +
                      " dn3user_type," +
                      " dn3template_type," +
                      " dn3guarantor_name," +
                      " dn3template_gid," +
                      " dn3guarantor_gid," +
                      " dn_type," +
                      " created_by ," +
                      " created_date)" +
                      " values (" +
                      "'" + msGetGid + "'," +
                       "'" + values.urn + "'," +
                       "'" + values.dnref_no + "'," +
                       "'" + values.dnsanctionref_no + "',";
                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "'" + Convert.ToDateTime(values.dnsanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "'" + lssanction_date + "',";
                }

                msSQL += "'" + values.dnsanction_amount.Replace(",", "") + "'," +
                       "'Y'," +
                        "'" + values.user_type + "'," +
                        "'" + values.template_type + "'," +
                        "'" + values.guarantor_name + "'," +
                        "'" + values.template_gid + "'," +
                        "'" + values.guarantor_gid + "'," +
                        "'DN3'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "select document_name,document_path from lgl_trn_tdn3annexuredocupload where sanction_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    msSQL = "update lgl_trn_tsanctiondtl set dn3annexuredocument_name='" + objODBCDatareader["document_name"].ToString() + "'," +
                        " dn3annexuredocument_path ='" + objODBCDatareader["document_path"].ToString() + "' where sanction_gid='" + msGetGid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    

                    if (mnResult != 0)
                    {
                        msSQL = "delete from lgl_trn_tdn3annexuredocupload where sanction_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objODBCDatareader.Close();
            }
            else
            {
                msSQL = " update lgl_trn_tsanctiondtl set dn3ref_no='" + values.dnref_no + "'," +
                       " dn3sanctionref_no='" + values.dnsanctionref_no + "',";
                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "dn3sanction_date='" + Convert.ToDateTime(values.dnsanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "dn3sanction_date='" + lssanction_date + "',";
                }

                msSQL += " dn3sanction_amount='" + values.dnsanction_amount.Replace(",", "") + "'," +
                       " dn3_flag='Y'," +
                       " dn3user_type='" + values.user_type + "'," +
                       " dn3template_type='" + values.template_type + "'," +
                       " dn3guarantor_name='" + values.guarantor_name + "'," +
                       " dn3template_gid='" + values.template_gid + "'," +
                       " dn3guarantor_gid='" + values.guarantor_gid + "'," +
                        " dn_type='DN3'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer_urn='" + values.urn + "'  and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "select document_name,document_path from lgl_trn_tdn3annexuredocupload where sanction_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    msSQL = "update lgl_trn_tsanctiondtl set dn3annexuredocument_name='" + objODBCDatareader["document_name"].ToString() + "'," +
                        " dn3annexuredocument_path ='" + objODBCDatareader["document_path"].ToString() + "'  where customer_urn='" + values.urn + "'  and status<>'Closed'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    

                    if (mnResult != 0)
                    {
                        msSQL = "delete from lgl_trn_tdn3annexuredocupload where sanction_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objODBCDatareader.Close();
            }
            if (mnResult != 0)
            {
                values.message = "Sanction Details Added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = false;
                return false;
            }



        }
        public bool DaGetTemplateDN3Content(template_list values, string urn)
        {
            msSQL = "select dn3template_gid,dn3user_type,dn3guarantor_gid,dn3sanctionref_no,format(dn3sanction_amount,2) as dn3sanction_amount,dn3ref_no," +
                " date_format(dn3sanction_date,'%d-%m-%Y') as dn3sanction_date,dn1ref_no,date_format(dn1generate_date,'%d-%m-%Y') as dn1sanction_date," +
                " dn2ref_no,date_format(dn2generate_date,'%d-%m-%Y') as dn2sanction_date from lgl_trn_tsanctiondtl where customer_urn='" + urn + "' and status<>'Closed'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                //Get Template Content
                msSQL = " select  a.template_content from adm_mst_ttemplate a " +
                  " where a.template_gid='" + objODBCDatareader["dn3template_gid"].ToString() + "'";
                lsdntemplate_content = objdbconn.GetExecuteScalar(msSQL);
                //Get Address Information
                if (objODBCDatareader["dn3user_type"].ToString() == "guarantor")
                {
                    msSQL = " select * from ocs_mst_tcustomer2userdtl " +
                      " where customer2usertype_gid='" + objODBCDatareader["dn3guarantor_gid"].ToString() + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        objODBCDatareader1.Read();
                        values.customer_mail = objODBCDatareader1["personalemail_address"].ToString();
                        values.customer_name = objODBCDatareader1["customer2user_name"].ToString();
                        values.address1 = objODBCDatareader1["addressline1"].ToString();
                        values.address2 = objODBCDatareader1["addressline2"].ToString();
                        values.mobile_no = objODBCDatareader1["mobile_no"].ToString();
                        values.email_address = objODBCDatareader1["personalemail_address"].ToString();
                    }
                    objODBCDatareader1.Close();
                }
                else
                {
                    msSQL = " select customername,email,contactperson,address,address2,mobileno,email from ocs_mst_tcustomer " +
                       " where customer_urn='" + urn + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        objODBCDatareader1.Read();
                        values.customer_mail = objODBCDatareader1["email"].ToString();
                        values.customer_name = objODBCDatareader1["customername"].ToString();
                        values.address1 = objODBCDatareader1["address"].ToString();
                        values.address2 = objODBCDatareader1["address2"].ToString();
                        values.mobile_no = objODBCDatareader1["mobileno"].ToString();
                        values.email_address = objODBCDatareader1["email"].ToString();
                    }
                    objODBCDatareader1.Close();
                }


                msSQL = "select max(cast(od_days as unsigned))  as od_days  from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
                lsod_days = objdbconn.GetExecuteScalar(msSQL);

                lscontent = lsdntemplate_content;
                lscontent = lscontent.Replace("now_date", DateTime.Now.ToString("dd-MM-yyyy"));
                //  lscontent = lscontent.Replace("customer_name", values.customer_name);
                lscontent = lscontent.Replace("Customer", values.customer_name + ",");
                lscontent = lscontent.Replace("addressline1", values.address1);
                lscontent = lscontent.Replace("addressline2", values.address2);
                lscontent = lscontent.Replace("od_days", lsod_days);
                lscontent = lscontent.Replace("mobile_no", values.mobile_no);
                lscontent = lscontent.Replace("email_address", values.email_address);


                values.dn3sanctionref_no = objODBCDatareader["dn3sanctionref_no"].ToString();

                string fare = objODBCDatareader["dn3sanction_amount"].ToString();
                decimal parsed = decimal.Parse(fare, System.Globalization.CultureInfo.InvariantCulture);
                System.Globalization.CultureInfo hindi = new System.Globalization.CultureInfo("hi-IN");
                string text = string.Format(hindi, "{0:c}", parsed);

                msSQL = "select substring('" + text + "',2,20)";
                string dn3sanction_amount = objdbconn.GetExecuteScalar(msSQL);

                values.dn3sanction_date = objODBCDatareader["dn3sanction_date"].ToString();
                values.dn3ref_no = objODBCDatareader["dn3ref_no"].ToString();

                if (objODBCDatareader["dn2ref_no"].ToString() != "")
                {
                    values.dn2sanctionref_no = objODBCDatareader["dn2ref_no"].ToString();
                    values.dn2sanction_date = objODBCDatareader["dn2sanction_date"].ToString();
                }
                else
                {
                    values.dn2sanctionref_no = "_______";
                    values.dn2sanction_date = "________";
                }
                if (objODBCDatareader["dn1ref_no"].ToString() != "")
                {
                    values.dn1sanctionref_no = objODBCDatareader["dn1ref_no"].ToString();
                    values.dn1sanction_date = objODBCDatareader["dn1sanction_date"].ToString();
                }
                else
                {
                    values.dn1sanctionref_no = "_______";
                    values.dn1sanction_date = "________";
                }

                lscontent = lscontent.Replace("dn3sanctionref_no", values.dn3sanctionref_no);
                lscontent = lscontent.Replace("dn3sanction_date", values.dn3sanction_date);
                lscontent = lscontent.Replace("dn3sanction_amount", dn3sanction_amount);
                lscontent = lscontent.Replace("dn3ref_no", values.dn3ref_no);
                lscontent = lscontent.Replace("dn2ref_no", values.dn2sanctionref_no);
                lscontent = lscontent.Replace("dn2sanction_date", values.dn2sanction_date);
                lscontent = lscontent.Replace("dn1ref_no", values.dn1sanctionref_no);
                lscontent = lscontent.Replace("dn1sanction_date", values.dn1sanction_date);
                
            }
            objODBCDatareader.Close();

            msSQL = "select format(natureof_credit_amount,2) as amount  from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
            lsnatureof_credit_amount = objdbconn.GetExecuteScalar(msSQL);

            string amount = lsnatureof_credit_amount;
            decimal parsed1 = decimal.Parse(amount, System.Globalization.CultureInfo.InvariantCulture);
            System.Globalization.CultureInfo indian1 = new System.Globalization.CultureInfo("hi-IN");
            string amount_format = string.Format(indian1, "{0:c}", parsed1);

            msSQL = "select substring('" + amount_format + "',2,20)";
            string totaldemand_amount = objdbconn.GetExecuteScalar(msSQL);

            lscontent = lscontent.Replace("totaldemand_amount", totaldemand_amount);

            values.template_content = lscontent;
            values.status = true;
            return true;
        }

        public bool DaGetCBOTemplate_List(mdltemplate values)
        {
            msSQL = " select a.template_gid,template_name from adm_mst_ttemplate a" +
                    " left join adm_trn_ttemplate2module b on a.template_gid = b.template_gid where module_gid = 'LGL' and template_name  like '%CBO%' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettemplatelist = new List<template_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettemplatelist.Add(new template_list
                    {
                        template_gid = (dr_datarow["template_gid"].ToString()),
                        template_name = (dr_datarow["template_name"].ToString()),
                    });
                }
                values.template_list = gettemplatelist;
            }
            dt_datatable.Dispose();


            values.status = true;
            return true;
        }
        public bool DaGetCBODN2template_list(mdltemplate values)
        {
            msSQL = msSQL = " select a.template_gid,template_name from adm_mst_ttemplate a" +
                    " left join adm_trn_ttemplate2module b on a.template_gid = b.template_gid where module_gid = 'LGL' and template_name  not like '%DN1%'" +
                    " and template_name  like '%CBO%' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettemplatelist = new List<template_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettemplatelist.Add(new template_list
                    {
                        template_gid = (dr_datarow["template_gid"].ToString()),
                        template_name = (dr_datarow["template_name"].ToString()),
                    });
                }
                values.template_list = gettemplatelist;
            }
            dt_datatable.Dispose();


            values.status = true;
            return true;
        }
        public bool DaGetCBODN3template_list(mdltemplate values)
        {
            msSQL = " select a.template_gid,template_name from adm_mst_ttemplate a" +
                    " left join adm_trn_ttemplate2module b on a.template_gid = b.template_gid where module_gid = 'LGL' and " +
                    " (template_name not like ('%DN1%') and template_name not like ( '%DN2%') and template_name like '%CBO%' )";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var gettemplatelist = new List<template_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    gettemplatelist.Add(new template_list
                    {
                        template_gid = (dr_datarow["template_gid"].ToString()),
                        template_name = (dr_datarow["template_name"].ToString()),
                    });
                }
                values.template_list = gettemplatelist;
            }
            dt_datatable.Dispose();


            values.status = true;
            return true;
        }

        public bool DaPostCBODN1sanctiondtl(DNsanctiondtl values, string employee_gid)
        {
            msSQL = "select customer_urn from lgl_trn_tsanctiondtl where customer_urn='" + values.urn + "'  and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select sanction_date from ocs_mst_tcustomer2sanction where sanction_refno='" + values.dnsanctionref_no + "' and customer_urn='" + values.urn + "'";
            string lssanction_date = objdbconn.GetExecuteScalar(msSQL);

            if (lsurn == "")
            {

                msGetGid = objcmnfunctions.GetMasterGID("DNSA");
                msSQL = "insert into lgl_trn_tsanctiondtl(" +
                      " sanction_gid," +
                      " customer_urn," +
                      " dnCBOref_no," +
                      " dnCBOsanctionref_no," +
                      " dnCBOsanction_date," +
                      " dnCBOsanction_amount," +
                      " dnCBO_flag," +
                      " dnCBOuser_type," +
                      " dncbotemplate_type," +
                      " dncboguarantor_name," +
                      " dncbotemplate_gid," +
                      " dncboguarantor_gid," +
                      " dn_type," +
                      " created_by ," +
                      " created_date)" +
                      " values (" +
                      "'" + msGetGid + "'," +
                       "'" + values.urn + "'," +
                       "'" + values.dnref_no + "'," +
                       "'" + values.dnsanctionref_no + "',";


                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "'" + Convert.ToDateTime(values.dnsanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "'" + lssanction_date + "',";
                }

                msSQL += "'" + values.dnsanction_amount.Replace(",", "") + "'," +
                  "'Y'," +
                  "'" + values.user_type + "'," +
                  "'" + values.template_type + "'," +
                  "'" + values.guarantor_name + "'," +
                  "'" + values.template_gid + "'," +
                  "'" + values.guarantor_gid + "'," +
                  "'DN1'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "select document_name,document_path from lgl_trn_tdn1annexuredocupload where sanction_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    msSQL = "update lgl_trn_tsanctiondtl set dn1annexuredocument_name='" + objODBCDatareader["document_name"].ToString() + "'," +
                        " dn1annexuredocument_path ='" + objODBCDatareader["document_path"].ToString() + "' where sanction_gid='" + msGetGid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    

                    if (mnResult != 0)
                    {
                        msSQL = "delete from lgl_trn_tdn1annexuredocupload where sanction_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objODBCDatareader.Close();

            }
            else
            {

                msSQL = " update lgl_trn_tsanctiondtl set dnCBOref_no='" + values.dnref_no + "'," +
                     " dnCBOsanctionref_no='" + values.dnsanctionref_no + "',";
                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "dnCBOsanction_date='" + Convert.ToDateTime(values.dnsanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "dnCBOsanction_date='" + lssanction_date + "',";
                }

                msSQL += " dnCBOsanction_amount='" + values.dnsanction_amount.Replace(",", "") + "'," +
                       " dnCBO_flag='Y'," +
                       " dncbouser_type='" + values.user_type + "'," +
                       " dncbotemplate_type='" + values.template_type + "'," +
                       " dncboguarantor_name='" + values.guarantor_name + "'," +
                       " dncbotemplate_gid='" + values.template_gid + "'," +
                       " dncboguarantor_gid='" + values.guarantor_gid + "'," +
                       " dn_type='DN1'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer_urn='" + values.urn + "'  and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "select document_name,document_path from lgl_trn_tdn1annexuredocupload where sanction_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    msSQL = "update lgl_trn_tsanctiondtl set dn1annexuredocument_name='" + objODBCDatareader["document_name"].ToString() + "'," +
                        " dn1annexuredocument_path ='" + objODBCDatareader["document_path"].ToString() + "'  where customer_urn='" + values.urn + "'  and status<>'Closed'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    

                    if (mnResult != 0)
                    {
                        msSQL = "delete from lgl_trn_tdn1annexuredocupload where sanction_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objODBCDatareader.Close();
            }
            if (mnResult != 0)
            {
                values.message = "Sanction Details Added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = false;
                return false;
            }
        }

        public bool DaPostCBODN2sanctiondtl(DNsanctiondtl values, string employee_gid)
        {
            msSQL = "select customer_urn from lgl_trn_tsanctiondtl where customer_urn='" + values.urn + "'  and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select sanction_date from ocs_mst_tcustomer2sanction where sanction_refno='" + values.dnsanctionref_no + "' and customer_urn='" + values.urn + "'";
            string lssanction_date = objdbconn.GetExecuteScalar(msSQL);

            if (lsurn == "")
            {

                msGetGid = objcmnfunctions.GetMasterGID("DNSA");
                msSQL = "insert into lgl_trn_tsanctiondtl(" +
                      " sanction_gid," +
                      " customer_urn," +
                      " dncbo2ref_no," +
                      " dncbo2sanctionref_no," +
                      " dncbo2sanction_date," +
                      " dncbo2sanction_amount," +
                      " dncbo2_flag," +
                      " dncbo2user_type," +
                      " dncbo2template_type," +
                      " dncbo2guarantor_name," +
                      " dncbo2template_gid," +
                      " dncbo2guarantor_gid," +
                      " dn_type," +
                      " created_by ," +
                      " created_date)" +
                      " values (" +
                      "'" + msGetGid + "'," +
                       "'" + values.urn + "'," +
                       "'" + values.dnref_no + "'," +
                       "'" + values.dnsanctionref_no + "',";


                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "'" + Convert.ToDateTime(values.dnsanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "'" + lssanction_date + "',";
                }

                msSQL += "'" + values.dnsanction_amount.Replace(",", "") + "'," +
                  "'Y'," +
                  "'" + values.user_type + "'," +
                  "'" + values.template_type + "'," +
                  "'" + values.guarantor_name + "'," +
                  "'" + values.template_gid + "'," +
                  "'" + values.guarantor_gid + "'," +
                  "'DN2'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "select document_name,document_path from lgl_trn_tdn2annexuredocupload where sanction_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    msSQL = "update lgl_trn_tsanctiondtl set dn2annexuredocument_name='" + objODBCDatareader["document_name"].ToString() + "'," +
                        " dn2annexuredocument_path ='" + objODBCDatareader["document_path"].ToString() + "' where sanction_gid='" + msGetGid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    

                    if (mnResult != 0)
                    {
                        msSQL = "delete from lgl_trn_tdn2annexuredocupload where sanction_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objODBCDatareader.Close();
            }
            else
            {

                msSQL = " update lgl_trn_tsanctiondtl set dncbo2ref_no='" + values.dnref_no + "'," +
                     " dncbo2sanctionref_no='" + values.dnsanctionref_no + "',";
                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "dncbo2sanction_date='" + Convert.ToDateTime(values.dnsanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "dncbo2sanction_date='" + lssanction_date + "',";
                }

                msSQL += " dncbo2sanction_amount='" + values.dnsanction_amount.Replace(",", "") + "'," +
                       " dncbo2_flag='Y'," +
                       " dncbo2user_type='" + values.user_type + "'," +
                       " dncbo2template_type='" + values.template_type + "'," +
                       " dncbo2guarantor_name='" + values.guarantor_name + "'," +
                       " dncbo2template_gid='" + values.template_gid + "'," +
                       " dncbo2guarantor_gid='" + values.guarantor_gid + "'," +
                       " dn_type='DN2'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer_urn='" + values.urn + "'  and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "select document_name,document_path from lgl_trn_tdn2annexuredocupload where sanction_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    msSQL = "update lgl_trn_tsanctiondtl set dn2annexuredocument_name='" + objODBCDatareader["document_name"].ToString() + "'," +
                        " dn2annexuredocument_path ='" + objODBCDatareader["document_path"].ToString() + "' where customer_urn='" + values.urn + "'  and status<>'Closed'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    

                    if (mnResult != 0)
                    {
                        msSQL = "delete from lgl_trn_tdn2annexuredocupload where sanction_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objODBCDatareader.Close();
            }
            if (mnResult != 0)
            {
                values.message = "Sanction Details Added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = false;
                return false;
            }
        }

        public bool DaPostCBODN3sanctiondtl(DNsanctiondtl values, string employee_gid)
        {
            msSQL = "select customer_urn from lgl_trn_tsanctiondtl where customer_urn='" + values.urn + "'  and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select sanction_date from ocs_mst_tcustomer2sanction where sanction_refno='" + values.dnsanctionref_no + "' and customer_urn='" + values.urn + "'";
            string lssanction_date = objdbconn.GetExecuteScalar(msSQL);

            if (lsurn == "")
            {

                msGetGid = objcmnfunctions.GetMasterGID("DNSA");
                msSQL = "insert into lgl_trn_tsanctiondtl(" +
                      " sanction_gid," +
                      " customer_urn," +
                      " dncbo3ref_no," +
                      " dncbo3sanctionref_no," +
                      " dncbo3sanction_date," +
                      " dncbo3sanction_amount," +
                      " dncbo3_flag," +
                      " dncbo3user_type," +
                      " dncbo3template_type," +
                      " dncbo3guarantor_name," +
                      " dncbo3template_gid," +
                      " dncbo3guarantor_gid," +
                      " dn_type," +
                      " created_by ," +
                      " created_date)" +
                      " values (" +
                      "'" + msGetGid + "'," +
                       "'" + values.urn + "'," +
                       "'" + values.dnref_no + "'," +
                       "'" + values.dnsanctionref_no + "',";


                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "'" + Convert.ToDateTime(values.dnsanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "'" + lssanction_date + "',";
                }

                msSQL += "'" + values.dnsanction_amount.Replace(",", "") + "'," +
                  "'Y'," +
                  "'" + values.user_type + "'," +
                  "'" + values.template_type + "'," +
                  "'" + values.guarantor_name + "'," +
                  "'" + values.template_gid + "'," +
                  "'" + values.guarantor_gid + "'," +
                  "'DN3'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "select document_name,document_path from lgl_trn_tdn3annexuredocupload where sanction_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    msSQL = "update lgl_trn_tsanctiondtl set dn3annexuredocument_name='" + objODBCDatareader["document_name"].ToString() + "'," +
                        " dn3annexuredocument_path ='" + objODBCDatareader["document_path"].ToString() + "' where sanction_gid='" + msGetGid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    

                    if (mnResult != 0)
                    {
                        msSQL = "delete from lgl_trn_tdn3annexuredocupload where sanction_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objODBCDatareader.Close();
            }
            else
            {

                msSQL = " update lgl_trn_tsanctiondtl set dncbo3ref_no='" + values.dnref_no + "'," +
                     " dncbo3sanctionref_no='" + values.dnsanctionref_no + "',";
                if ((lssanction_date == null) || (lssanction_date == ""))
                {
                    msSQL += "dncbo3sanction_date='" + Convert.ToDateTime(values.dnsanction_date).AddDays(1).ToString("yyyy-MM-dd") + "',";

                }
                else
                {
                    msSQL += "dncbo3sanction_date='" + lssanction_date + "',";
                }

                msSQL += " dncbo3sanction_amount='" + values.dnsanction_amount.Replace(",", "") + "'," +
                       " dncbo3_flag='Y'," +
                       " dncbo3user_type='" + values.user_type + "'," +
                       " dncbo3template_type='" + values.template_type + "'," +
                       " dncbo3guarantor_name='" + values.guarantor_name + "'," +
                       " dncbo3template_gid='" + values.template_gid + "'," +
                       " dncbo3guarantor_gid='" + values.guarantor_gid + "'," +
                       " dn_type='DN3'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where customer_urn='" + values.urn + "'  and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "select document_name,document_path from lgl_trn_tdn3annexuredocupload where sanction_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    msSQL = "update lgl_trn_tsanctiondtl set dn3annexuredocument_name='" + objODBCDatareader["document_name"].ToString() + "'," +
                        " dn3annexuredocument_path ='" + objODBCDatareader["document_path"].ToString() + "' where customer_urn='" + values.urn + "'  and status<>'Closed'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    

                    if (mnResult != 0)
                    {
                        msSQL = "delete from lgl_trn_tdn3annexuredocupload where sanction_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objODBCDatareader.Close();
            }
            if (mnResult != 0)
            {
                values.message = "Sanction Details Added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding";
                values.status = false;
                return false;
            }
        }
        public bool DaGetCBOTemplateDN1Content(template_list values, string urn)
        {
            msSQL = "select dncbotemplate_gid,dncbouser_type,dncboguarantor_gid,dnCBOsanctionref_no,format(dnCBOsanction_amount,2) as dnCBOsanction_amount," +
                " date_format(dnCBOsanction_date,'%d-%m-%Y') as dnCBOsanction_date," +
                " dnCBOref_no from lgl_trn_tsanctiondtl where customer_urn='" + urn + "' and status<>'Closed'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                //Get Template Content
                msSQL = " select  a.template_content from adm_mst_ttemplate a " +
                  " where a.template_gid='" + objODBCDatareader["dncbotemplate_gid"].ToString() + "'";
                lsdntemplate_content = objdbconn.GetExecuteScalar(msSQL);
                //Get Address Information
                if (objODBCDatareader["dncbouser_type"].ToString() == "guarantor")
                {
                    msSQL = " select * from ocs_mst_tcustomer2userdtl " +
                      " where customer2usertype_gid='" + objODBCDatareader["dncboguarantor_gid"].ToString() + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        objODBCDatareader1.Read();
                        values.customer_mail = objODBCDatareader1["personalemail_address"].ToString();
                        values.customer_name = objODBCDatareader1["customer2user_name"].ToString();
                        values.address1 = objODBCDatareader1["addressline1"].ToString();
                        values.address2 = objODBCDatareader1["addressline2"].ToString();
                        values.mobile_no = objODBCDatareader1["mobile_no"].ToString();
                        values.email_address = objODBCDatareader1["personalemail_address"].ToString();
                    }
                    objODBCDatareader1.Close();
                }
                else
                {
                    msSQL = " select customername,email,contactperson,address,address2,mobileno,email from ocs_mst_tcustomer " +
                       " where customer_urn='" + urn + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        objODBCDatareader1.Read();
                        values.customer_mail = objODBCDatareader1["email"].ToString();
                        values.customer_name = objODBCDatareader1["customername"].ToString();
                        values.address1 = objODBCDatareader1["address"].ToString();
                        values.address2 = objODBCDatareader1["address2"].ToString();
                        values.mobile_no = objODBCDatareader1["mobileno"].ToString();
                        values.email_address = objODBCDatareader1["email"].ToString();
                    }
                    objODBCDatareader1.Close();
                }


                msSQL = "select max(cast(od_days as unsigned))  as od_days  from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
                lsod_days = objdbconn.GetExecuteScalar(msSQL);

                lscontent = lsdntemplate_content;
                lscontent = lscontent.Replace("now_date", DateTime.Now.ToString("dd-MM-yyyy"));
                //  lscontent = lscontent.Replace("customer_name", values.customer_name);
                lscontent = lscontent.Replace("Customer", values.customer_name + ",");
                lscontent = lscontent.Replace("addressline1", values.address1);
                lscontent = lscontent.Replace("addressline2", values.address2);
                lscontent = lscontent.Replace("od_days", lsod_days);
                lscontent = lscontent.Replace("mobile_no", values.mobile_no);
                lscontent = lscontent.Replace("email_address", values.email_address);


                values.dnCBOsanctionref_no = objODBCDatareader["dnCBOsanctionref_no"].ToString();
                values.dnCBOsanction_amount = objODBCDatareader["dnCBOsanction_amount"].ToString();
                values.dnCBOsanction_date = objODBCDatareader["dnCBOsanction_date"].ToString();
                values.dnCBOref_no = objODBCDatareader["dnCBOref_no"].ToString();

                string fare = objODBCDatareader["dnCBOsanction_amount"].ToString();
                decimal parsed = decimal.Parse(fare, System.Globalization.CultureInfo.InvariantCulture);
                System.Globalization.CultureInfo hindi = new System.Globalization.CultureInfo("hi-IN");
                string text = string.Format(hindi, "{0:c}", parsed);

                msSQL = "select substring('" + text + "',2,20)";
                string dnCBOsanction_amount = objdbconn.GetExecuteScalar(msSQL);
                //   var amount = new intl.NumberFormat("en-IN").format(objODBCDatareader["dn1sanction_amount"].ToString());

                lscontent = lscontent.Replace("dnCBOsanctionref_no", values.dnCBOsanctionref_no);
                lscontent = lscontent.Replace("dnCBOsanction_date", values.dnCBOsanction_date);
                lscontent = lscontent.Replace("dnCBOsanction_amount", dnCBOsanction_amount);
                lscontent = lscontent.Replace("dnCBOref_no", values.dnCBOref_no);

                msSQL = "select format(natureof_credit_amount,2) as amount  from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
                lsnatureof_credit_amount = objdbconn.GetExecuteScalar(msSQL);

                string amount = lsnatureof_credit_amount;
                decimal parsed1 = decimal.Parse(amount, System.Globalization.CultureInfo.InvariantCulture);
                System.Globalization.CultureInfo indian1 = new System.Globalization.CultureInfo("hi-IN");
                string amount_format = string.Format(indian1, "{0:c}", parsed1);

                msSQL = "select substring('" + amount_format + "',2,20)";
                string totaldemand_amount = objdbconn.GetExecuteScalar(msSQL);

                lscontent = lscontent.Replace("totaldemand_amount", totaldemand_amount);

                values.template_content = lscontent;
                
            }
            objODBCDatareader.Close();

            values.status = true;
            return true;
        }
        public bool DaGetCBO2TemplateDNContent(template_list values, string urn)
        {
            msSQL = "select dncbo2template_gid,dncbo2user_type,dncbo2guarantor_gid,dncbo2sanctionref_no,format(dncbo2sanction_amount,2) as dncbo2sanction_amount," +
                " date_format(dncbo2sanction_date,'%d-%m-%Y') as dncbo2sanction_date,date_format(dnCBOgenerate_date,'%d-%m-%Y') as dnCBOsanction_date," +
                " dncbo2ref_no,dnCBOref_no from lgl_trn_tsanctiondtl where customer_urn='" + urn + "' and status<>'Closed'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                //Get Template Content
                msSQL = " select  a.template_content from adm_mst_ttemplate a " +
                  " where a.template_gid='" + objODBCDatareader["dncbo2template_gid"].ToString() + "'";
                lsdntemplate_content = objdbconn.GetExecuteScalar(msSQL);
                //Get Address Information
                if (objODBCDatareader["dncbo2user_type"].ToString() == "guarantor")
                {
                    msSQL = " select * from ocs_mst_tcustomer2userdtl " +
                      " where customer2usertype_gid='" + objODBCDatareader["dncbo2guarantor_gid"].ToString() + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        objODBCDatareader1.Read();
                        values.customer_mail = objODBCDatareader1["personalemail_address"].ToString();
                        values.customer_name = objODBCDatareader1["customer2user_name"].ToString();
                        values.address1 = objODBCDatareader1["addressline1"].ToString();
                        values.address2 = objODBCDatareader1["addressline2"].ToString();
                        values.mobile_no = objODBCDatareader1["mobile_no"].ToString();
                        values.email_address = objODBCDatareader1["personalemail_address"].ToString();
                    }
                    objODBCDatareader1.Close();
                }
                else
                {
                    msSQL = " select customername,email,contactperson,address,address2,mobileno,email from ocs_mst_tcustomer " +
                       " where customer_urn='" + urn + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        objODBCDatareader1.Read();
                        values.customer_mail = objODBCDatareader1["email"].ToString();
                        values.customer_name = objODBCDatareader1["customername"].ToString();
                        values.address1 = objODBCDatareader1["address"].ToString();
                        values.address2 = objODBCDatareader1["address2"].ToString();
                        values.mobile_no = objODBCDatareader1["mobileno"].ToString();
                        values.email_address = objODBCDatareader1["email"].ToString();
                    }
                    objODBCDatareader1.Close();
                }


                msSQL = "select max(cast(od_days as unsigned))  as od_days  from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
                lsod_days = objdbconn.GetExecuteScalar(msSQL);

                lscontent = lsdntemplate_content;
                lscontent = lscontent.Replace("now_date", DateTime.Now.ToString("dd-MM-yyyy"));
                //  lscontent = lscontent.Replace("customer_name", values.customer_name);
                lscontent = lscontent.Replace("Customer", values.customer_name + ",");
                lscontent = lscontent.Replace("addressline1", values.address1);
                lscontent = lscontent.Replace("addressline2", values.address2);
                lscontent = lscontent.Replace("od_days", lsod_days);
                lscontent = lscontent.Replace("mobile_no", values.mobile_no);
                lscontent = lscontent.Replace("email_address", values.email_address);


                values.dncbo2sanctionref_no = objODBCDatareader["dncbo2sanctionref_no"].ToString();
                values.dncbo2sanction_amount = objODBCDatareader["dncbo2sanction_amount"].ToString();
                values.dncbo2sanction_date = objODBCDatareader["dncbo2sanction_date"].ToString();
                values.dncbo2ref_no = objODBCDatareader["dncbo2ref_no"].ToString();
                if (objODBCDatareader["dnCBOref_no"].ToString() != "")
                {
                    values.dnCBOref_no = objODBCDatareader["dnCBOref_no"].ToString();
                    values.dnCBOsanction_date = objODBCDatareader["dnCBOsanction_date"].ToString();
                }
                else
                {
                    values.dnCBOref_no = "_______";
                    values.dnCBOsanction_date = "_______";
                }
                string fare = objODBCDatareader["dncbo2sanction_amount"].ToString();
                decimal parsed = decimal.Parse(fare, System.Globalization.CultureInfo.InvariantCulture);
                System.Globalization.CultureInfo hindi = new System.Globalization.CultureInfo("hi-IN");
                string text = string.Format(hindi, "{0:c}", parsed);

                msSQL = "select substring('" + text + "',2,20)";
                string dncbo2sanction_amount = objdbconn.GetExecuteScalar(msSQL);
                //   var amount = new intl.NumberFormat("en-IN").format(objODBCDatareader["dn1sanction_amount"].ToString());

                lscontent = lscontent.Replace("dncbo2sanctionref_no", values.dncbo2sanctionref_no);
                lscontent = lscontent.Replace("dncbo2sanction_date", values.dncbo2sanction_date);
                lscontent = lscontent.Replace("dncbo2sanction_amount", dncbo2sanction_amount);
                lscontent = lscontent.Replace("dncbo2ref_no", values.dncbo2ref_no);
                lscontent = lscontent.Replace("dnCBOref_no", values.dnCBOref_no);
                lscontent = lscontent.Replace("dnCBOsanction_date", values.dnCBOsanction_date);

                msSQL = "select format(natureof_credit_amount,2) as amount  from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
                lsnatureof_credit_amount = objdbconn.GetExecuteScalar(msSQL);

                string amount = lsnatureof_credit_amount;
                decimal parsed1 = decimal.Parse(amount, System.Globalization.CultureInfo.InvariantCulture);
                System.Globalization.CultureInfo indian1 = new System.Globalization.CultureInfo("hi-IN");
                string amount_format = string.Format(indian1, "{0:c}", parsed1);

                msSQL = "select substring('" + amount_format + "',2,20)";
                string totaldemand_amount = objdbconn.GetExecuteScalar(msSQL);

                lscontent = lscontent.Replace("totaldemand_amount", totaldemand_amount);

                values.template_content = lscontent;
                
            }
            objODBCDatareader.Close();

            values.status = true;
            return true;
        }
        public bool DaGetCBO3TemplateDNContent(template_list values, string urn)
        {
            msSQL = "select dncbo3template_gid,dncbo3user_type,dncbo3guarantor_gid,dncbo3sanctionref_no,format(dncbo3sanction_amount,2) as dncbo3sanction_amount," +
                " date_format(dncbo3sanction_date,'%d-%m-%Y') as dncbo3sanction_date,date_format(dncbo2generate_date,'%d-%m-%Y') as dncbo2sanction_date," +
                " date_format(dnCBOgenerate_date,'%d-%m-%Y') as dnCBOsanction_date,dnCBOref_no,dncbo2ref_no,dncbo3ref_no from lgl_trn_tsanctiondtl where customer_urn='" + urn + "' and status<>'Closed'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                //Get Template Content
                msSQL = " select  a.template_content from adm_mst_ttemplate a " +
                  " where a.template_gid='" + objODBCDatareader["dncbo3template_gid"].ToString() + "'";
                lsdntemplate_content = objdbconn.GetExecuteScalar(msSQL);
                //Get Address Information
                if (objODBCDatareader["dncbo3user_type"].ToString() == "guarantor")
                {
                    msSQL = " select * from ocs_mst_tcustomer2userdtl " +
                      " where customer2usertype_gid='" + objODBCDatareader["dncbo3guarantor_gid"].ToString() + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        objODBCDatareader1.Read();
                        values.customer_mail = objODBCDatareader1["personalemail_address"].ToString();
                        values.customer_name = objODBCDatareader1["customer2user_name"].ToString();
                        values.address1 = objODBCDatareader1["addressline1"].ToString();
                        values.address2 = objODBCDatareader1["addressline2"].ToString();
                        values.mobile_no = objODBCDatareader1["mobile_no"].ToString();
                        values.email_address = objODBCDatareader1["personalemail_address"].ToString();
                    }
                    objODBCDatareader1.Close();
                }
                else
                {
                    msSQL = " select customername,email,contactperson,address,address2,mobileno,email from ocs_mst_tcustomer " +
                       " where customer_urn='" + urn + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        objODBCDatareader1.Read();
                        values.customer_mail = objODBCDatareader1["email"].ToString();
                        values.customer_name = objODBCDatareader1["customername"].ToString();
                        values.address1 = objODBCDatareader1["address"].ToString();
                        values.address2 = objODBCDatareader1["address2"].ToString();
                        values.mobile_no = objODBCDatareader1["mobileno"].ToString();
                        values.email_address = objODBCDatareader1["email"].ToString();
                    }
                    objODBCDatareader1.Close();
                }


                msSQL = "select max(cast(od_days as unsigned))  as od_days  from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
                lsod_days = objdbconn.GetExecuteScalar(msSQL);

                lscontent = lsdntemplate_content;
                lscontent = lscontent.Replace("now_date", DateTime.Now.ToString("dd-MM-yyyy"));
                //  lscontent = lscontent.Replace("customer_name", values.customer_name);
                lscontent = lscontent.Replace("Customer", values.customer_name + ",");
                lscontent = lscontent.Replace("addressline1", values.address1);
                lscontent = lscontent.Replace("addressline2", values.address2);
                lscontent = lscontent.Replace("od_days", lsod_days);
                lscontent = lscontent.Replace("mobile_no", values.mobile_no);
                lscontent = lscontent.Replace("email_address", values.email_address);


                values.dncbo3sanctionref_no = objODBCDatareader["dncbo3sanctionref_no"].ToString();
                values.dncbo3sanction_amount = objODBCDatareader["dncbo3sanction_amount"].ToString();
                values.dncbo3sanction_date = objODBCDatareader["dncbo3sanction_date"].ToString();
                values.dncbo3ref_no = objODBCDatareader["dncbo3ref_no"].ToString();
                if (objODBCDatareader["dnCBOref_no"].ToString() != "")
                {
                    values.dnCBOref_no = objODBCDatareader["dnCBOref_no"].ToString();
                    values.dnCBOsanction_date = objODBCDatareader["dnCBOsanction_date"].ToString();
                }
                else
                {
                    values.dnCBOref_no = "_______";
                    values.dnCBOsanction_date = "________";
                }
                if (objODBCDatareader["dncbo2ref_no"].ToString() != "")
                {
                    values.dncbo2ref_no = objODBCDatareader["dncbo2ref_no"].ToString();
                    values.dncbo2sanction_date = objODBCDatareader["dncbo2sanction_date"].ToString();
                }
                else
                {
                    values.dncbo2ref_no = "_______";
                    values.dncbo2sanction_date = "________";
                }
                //string fare = objODBCDatareader["dncbo3sanction_amount"].ToString();
                //decimal parsed = decimal.Parse(fare, System.Globalization.CultureInfo.InvariantCulture);
                //System.Globalization.CultureInfo hindi = new System.Globalization.CultureInfo("hi-IN");
                //string text = string.Format(hindi, "{0:c}", parsed);

                //msSQL = "select substring('" + text + "',2,20)";
                //string dncbo3sanction_amount = objdbconn.GetExecuteScalar(msSQL);

                //   var amount = new intl.NumberFormat("en-IN").format(objODBCDatareader["dn1sanction_amount"].ToString());

                lscontent = lscontent.Replace("dncbo3sanctionref_no", values.dncbo3sanctionref_no);
                lscontent = lscontent.Replace("dncbo3sanction_date", values.dncbo3sanction_date);
                lscontent = lscontent.Replace("dncbo3sanction_amount", values.dncbo3sanction_amount);
                lscontent = lscontent.Replace("dncbo3ref_no", values.dncbo3ref_no);
                lscontent = lscontent.Replace("dncbo2sanction_date", values.dncbo2sanction_date);
                lscontent = lscontent.Replace("dncbo2sanctionref_no", values.dncbo2sanctionref_no);
                lscontent = lscontent.Replace("dnCBOsanction_date", values.dnCBOsanction_date);
                lscontent = lscontent.Replace("dnCBOsanctionref_no", values.dnCBOsanctionref_no);

                msSQL = "select format(natureof_credit_amount,2) as amount  from lgl_trn_tmisdata where urn='" + urn + "' group by urn";
                lsnatureof_credit_amount = objdbconn.GetExecuteScalar(msSQL);

                string amount = lsnatureof_credit_amount;
                decimal parsed1 = decimal.Parse(amount, System.Globalization.CultureInfo.InvariantCulture);
                System.Globalization.CultureInfo indian1 = new System.Globalization.CultureInfo("hi-IN");
                string amount_format = string.Format(indian1, "{0:c}", parsed1);

                msSQL = "select substring('" + amount_format + "',2,20)";
                string totaldemand_amount = objdbconn.GetExecuteScalar(msSQL);

                lscontent = lscontent.Replace("totaldemand_amount", totaldemand_amount);

                values.template_content = lscontent;
               
            }
            objODBCDatareader.Close();

            values.status = true;
            return true;
        }

        public bool DaPostCBODN1Generate(MdlMisdataimportlist values, string employee_gid)
        {
            msGETGID = objcmnfunctions.GetMasterGID("DN1F");
            msSQL = "insert into lgl_tmp_tdnformat(" +
                " tempdn1format_gid, " +
                " DN1template_content, " +
                " customer_urn," +
                " created_by," +
                " created_date)" +
                " values (" +
                "'" + msGETGID + "'," +
                "'" + values.template_content + "'," +
                "'" + values.urn + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update lgl_trn_tmisdata set DN_status='DN1 Generated'," +
                " acknowledgement_status='--'," +
               " updated_by='" + employee_gid + "'," +
               "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGid = objcmnfunctions.GetMasterGID("LGST");
            msSQL = "insert into lgl_trn_tdnstatus(" +
                " dnstatus_gid, " +
                " dn1status, " +
                " customer_urn," +
                " created_by," +
                " created_date)" +
                " values (" +
                "'" + msGetGid + "'," +
                "'DN1 Generated'," +
                "'" + values.urn + "'," +
                "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update lgl_trn_tsanctiondtl set dnCBOgenerate_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where customer_urn='" + values.urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
            return true;
        }

        public bool DaPostCBODN2Generate(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL = "select customer_urn from lgl_tmp_tdnformat where customer_urn='" + values.urn + "' and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);
            if (lsurn == "")
            {
                msGETGID = objcmnfunctions.GetMasterGID("DN1F");
                msSQL = "insert into lgl_tmp_tdnformat(" +
                    " tempdn1format_gid, " +
                    " DN2template_content, " +
                    " customer_urn," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGETGID + "'," +
                    "'" + values.template_content + "'," +
                    "'" + values.urn + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set DN_status='DN2 Generated'," +
                    " acknowledgement_status='--'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update lgl_trn_tsanctiondtl set dncbo2generate_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
               " where customer_urn='" + values.urn + "' and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = "update lgl_tmp_tdnformat set DN2template_content='" + values.template_content + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " updated_by ='" + employee_gid + "' where customer_urn='" + values.urn + "' and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set DN_status='DN2 Generated'," +
                    " acknowledgement_status='--'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update lgl_trn_tsanctiondtl set dncbo2generate_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
             " where customer_urn='" + values.urn + "' and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            msSQL = "select customer_urn from lgl_trn_tdnstatus where customer_urn='" + values.urn + "'  and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);
            if (lsurn == "")
            {
                msGetGid = objcmnfunctions.GetMasterGID("LGST");
                msSQL = "insert into lgl_trn_tdnstatus(" +
                    " dnstatus_gid, " +
                    " dn2status, " +
                    " customer_urn," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGetGid + "'," +
                    "'DN2 Generated'," +
                    "'" + values.urn + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = "update lgl_trn_tdnstatus set dn2status='DN2 Generated'," +
                    " updated_by ='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where customer_urn='" + values.urn + "' and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            values.status = true;
            return true;
        }

        public bool DaPostCBODN3Generate(MdlMisdataimportlist values, string employee_gid)
        {
            msSQL = "select customer_urn from lgl_tmp_tdnformat where customer_urn='" + values.urn + "' and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);
            if (lsurn == "")
            {
                msGETGID = objcmnfunctions.GetMasterGID("DN1F");
                msSQL = "insert into lgl_tmp_tdnformat(" +
                    " tempdn1format_gid, " +
                    " DN3template_content, " +
                    " customer_urn," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGETGID + "'," +
                    "'" + values.template_content + "'," +
                    "'" + values.urn + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set DN_status='DN3 Generated'," +
                    " acknowledgement_status='--'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update lgl_trn_tsanctiondtl set dncbo3generate_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
             " where customer_urn='" + values.urn + "' and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = "update lgl_tmp_tdnformat set DN3template_content='" + values.template_content + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " updated_by ='" + employee_gid + "' where customer_urn='" + values.urn + "' and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_tmisdata set DN_status='DN3 Generated'," +
                    " acknowledgement_status='--'," +
                   " updated_by='" + employee_gid + "'," +
                   "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update lgl_trn_tsanctiondtl set dncbo3generate_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where customer_urn='" + values.urn + "' and status<>'Closed'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            msSQL = "select customer_urn from lgl_trn_tdnstatus where customer_urn='" + values.urn + "' and status<>'Closed'";
            lsurn = objdbconn.GetExecuteScalar(msSQL);
            if (lsurn == "")
            {
                msGetGid = objcmnfunctions.GetMasterGID("LGST");
                msSQL = "insert into lgl_trn_tdnstatus(" +
                    " dnstatus_gid, " +
                    " dn3status, " +
                    " customer_urn," +
                    " created_by," +
                    " created_date)" +
                    " values (" +
                    "'" + msGetGid + "'," +
                    "'DN3 Generated'," +
                    "'" + values.urn + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                msSQL = "update lgl_trn_tdnstatus set dn3status='DN3 Generated'," +
                    " updated_by ='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' and status<>'Closed'" +
                    " where customer_urn='" + values.urn + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            values.status = true;
            return true;
        }

        public void DaGetSkippedHistory(MdlDNSkipHistory values, string urn)
        {
            msSQL = "select concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as skipped_by," +
                " date_format(a.created_date, '%d-%m-%Y') as skipped_date,skip_reason,"+
                " date_format(a.validity, '%d-%m-%Y') as validity from lgl_trn_tdnskipcase" +
                " a, hrm_mst_temployee b, adm_mst_tuser c where urn = '" + urn + "'" +
                " and a.created_by = b.employee_gid and b.user_gid = c.user_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdnhistory_list = new List<dnskiphistory_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdnhistory_list.Add(new dnskiphistory_list
                    {
                        skipped_by = dr_datarow["skipped_by"].ToString(),
                        skipped_date = dr_datarow["skipped_date"].ToString(),
                        skip_reason = dr_datarow["skip_reason"].ToString(),
                        validity = dr_datarow["validity"].ToString()
                    });
                }
                values.dnskiphistory_list = getdnhistory_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public bool DaPostDN1AnnexureUpload(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
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

            String path = lspath;

            string project_flag = httpRequest.Form["project_flag"].ToString();



            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "LGL/DN1Annexure/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

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
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "LGL/DN1Annexure/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/DN1Annexure/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/DN1Annexure/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";





                        //FileStream file = new FileStream(lspath, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                       // lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "LGL/DN1Annexure/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        msGetGid = objcmnfunctions.GetMasterGID("DOCS");
                        msSQL = " insert into lgl_trn_tdn1annexuredocupload( " +
                                    " dn1annexuredocument_gid," +
                                    " sanction_gid ," +
                                    " document_name," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

            }
            catch
            {

            }
            if (mnResult == 1)
            {
                objfilename.status = true;

                return true;
            }
            else
            {
                objfilename.status = false;

                return false;
            }
        }
        public bool DaPostDN2AnnexureUpload(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
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
            string project_flag = httpRequest.Form["project_flag"].ToString();

            String path = lspath;



            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "LGL/DN2Annexure/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

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
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "LGL/DN2Annexure/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        //FileStream file = new FileStream(lspath, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        // lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "LGL/DN2Annexure/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/DN2Annexure/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/DN2Annexure/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";



                        msGetGid = objcmnfunctions.GetMasterGID("DOCS");
                        msSQL = " insert into lgl_trn_tdn2annexuredocupload( " +
                                    " dn2annexuredocument_gid," +
                                    " sanction_gid ," +
                                    " document_name," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

            }
            catch
            {

            }
            if (mnResult == 1)
            {
                objfilename.status = true;

                return true;
            }
            else
            {
                objfilename.status = false;

                return false;
            }
        }
        public bool DaPostDN3AnnexureUpload(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
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

            String path = lspath;

            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "LGL/DN3Annexure/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

            if ((!System.IO.Directory.Exists(path)))
                System.IO.Directory.CreateDirectory(path);

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
                        //string lsfile_gid = msdocument_gid + FileExtension;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "LGL/DN3Annexure/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        //FileStream file = new FileStream(lspath, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();
                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "LGL/DN3Annexure/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/DN3Annexure/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/DN3Annexure/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("DOCS");
                        msSQL = " insert into lgl_trn_tdn3annexuredocupload( " +
                                    " dn3annexuredocument_gid," +
                                    " sanction_gid ," +
                                    " document_name," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

            }
            catch
            {

            }
            if (mnResult == 1)
            {
                objfilename.status = true;

                return true;
            }
            else
            {
                objfilename.status = false;

                return false;
            }
        }
        public bool DaGetCBODN1Cancel(template_list values, string urn, string employee_gid)
        {
            msSQL = "delete from lgl_trn_tsanctiondtl where customer_urn='" + urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from lgl_trn_tdn1annexuredocupload where sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
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

        public bool DaGetCBODN2Cancel(template_list values, string urn, string employee_gid)
        {
            msSQL = "delete from lgl_trn_tsanctiondtl where customer_urn='" + urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from lgl_trn_tdn2annexuredocupload where sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
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
        public bool DaGetCBODN3Cancel(template_list values, string urn, string employee_gid)
        {
            msSQL = "delete from lgl_trn_tsanctiondtl where customer_urn='" + urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "delete from lgl_trn_tdn3annexuredocupload where sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
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
        public bool DaGetSanctionCBODN2Cancel(template_list values, string urn, string employee_gid)
        {
            msSQL = " update lgl_trn_tsanctiondtl set " +
                "  dncbo2sanctionref_no=null," +
                " dncbo2ref_no=null," +
                " dncbo2sanction_date=null," +
                " dncbo2_flag='N'," +
                " dncbo2sanction_amount='0.00'," +
                " dncbo2user_type=null," +
                " dncbo2template_type=null," +
                " dncbo2guarantor_name=null," +
                " dncbo2template_gid=null," +
                " dncbo2guarantor_gid=null," +
                " dn2annexuredocument_name=null," +
                " dn2annexuredocument_path=null where customer_urn ='" + urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from lgl_trn_tdn2annexuredocupload where sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
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
        public bool DaGetSanctionCBODN3Cancel(template_list values, string urn, string employee_gid)
        {
            msSQL = " update lgl_trn_tsanctiondtl set " +
                "  dncbo3sanctionref_no=null," +
                " dncbo3ref_no=null," +
                " dncbo3sanction_date=null," +
                " dncbo3_flag='N'," +
                " dncbo3sanction_amount='0.00'," +
                " dncbo3user_type=null," +
                " dncbo3template_type=null," +
                " dncbo3guarantor_name=null," +
                " dncbo3template_gid=null," +
                " dncbo3guarantor_gid=null," +
                " dn3annexuredocument_name=null," +
                " dn3annexuredocument_path=null where customer_urn ='" + urn + "' and status<>'Closed'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from lgl_trn_tdn3annexuredocupload where sanction_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
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
        public void DaGetDNGenerated_History(MdlLglReport values, string urn)
        {
            msSQL = " select a.customer_urn,x.customername,x.vertical_code,case when o.dn1sanctionref_no is null then '---' else o.dn1sanctionref_no end as dn1sanctionref_no," +
                   " case when o.dn2sanctionref_no is null then '---' else o.dn2sanctionref_no end as dn2sanctionref_no," +
                   " case when o.dn3sanctionref_no is null then '---' else o.dn3sanctionref_no end as dn3sanctionref_no," +
                   " case when o.dn1sanction_date is null  then '---' else date_format(o.dn1sanction_date,'%d-%m-%Y') end as dn1sanction_date," +
                   " case when o.dn2sanction_date is null  then '---' else date_format(o.dn2sanction_date, '%d-%m-%Y') end as dn2sanction_date," +
                   " case when o.dn3sanction_date is null  then '---' else date_format(o.dn3sanction_date, '%d-%m-%Y') end as dn3sanction_date," +
                   " case when o.dn1sanction_amount is null  then '---' else format(o.dn1sanction_amount, 2) end as dn1sanction_amount," +
                   " case when o.dn2sanction_amount is null  then '---' else  format(o.dn2sanction_amount, 2) end as dn2sanction_amount," +
                   " case when o.dn3sanction_amount is null  then '---' else format(o.dn3sanction_amount, 2) end  as dn3sanction_amount," +
                   " case when dn1generated_date is null  then '---' else date_format(dn1generated_date, '%d-%m-%Y') end as dn1generated_date," +
                   " case when dn2generated_date is null  then '---' else date_format(dn2generated_date, '%d-%m-%Y') end as dn2generated_date," +
                   " case when dn3generated_date is null  then '---' else date_format(dn3generated_date, '%d-%m-%Y') end as dn3generated_date, " +
                   " case when b.dn1status_created_by is null  then '---' else concat(f.user_firstname, ' ', f.user_lastname, '/', f.user_code) end as dn1sent_by," +
                   " case when b.dn2status_updated_by is null  then '---' else concat(g.user_firstname, ' ', g.user_lastname, '/', g.user_code) end as dn2sent_by," +
                   " case when b.dn3status_updated_by is null  then '---' else concat(h.user_firstname, ' ', h.user_lastname, '/', h.user_code) end as dn3sent_by," +
                   " case when dn1status_created_date is null  then '---' else date_format(dn1status_created_date, '%d-%m-%Y') end as dn1sent_date," +
                   " case when dn2status_updated_date is null  then '---' else date_format(dn2status_updated_date, '%d-%m-%Y') end as dn2sent_date," +
                   " case when dn2status_updated_date is null  then '---' else date_format(dn3status_updated_date, '%d-%m-%Y') end as dn3sent_date," +
                   " case when a.dn1generated_by is null  then '---' else concat(l.user_firstname, ' ', l.user_lastname, '/', l.user_code) end as dn1generated_by," +
                   " case when a.dn2generated_by is null  then '---' else concat(m.user_firstname, ' ', m.user_lastname, '/', m.user_code) end as dn2generated_by," +
                   " case when a.dn3generated_by is null  then '---' else concat(n.user_firstname, ' ', n.user_lastname, '/', n.user_code) end as dn3generated_by," +
                   " case when courier_refno is null  then '---' else courier_refno end as dn1courier_refno," +
                   " case when dn2courier_refno is null  then '---' else dn2courier_refno end as dn2courier_refno," +
                   " case when dn3courier_refno is null  then '---' else dn3courier_refno end as dn3courier_refno ," +
                   " case when courier_center is null  then '---' else courier_center end as dn1courier_center," +
                   " case when dn2courier_center is null  then '---' else dn2courier_center end as dn2courier_center," +
                   " case when dn3courier_center is null  then '---' else dn3courier_center end as dn3courier_center," +
                   " case when courier_date is null  then '---' else date_format(courier_date, '%d-%m-%Y') end as dn1courier_date," +
                   " case when dn2courier_date is null  then '---' else date_format(dn2courier_date, '%d-%m-%Y') end as dn2courier_date," +
                   " case when dn3courier_date is null  then '---' else date_format(dn3courier_date, '%d-%m-%Y') end as dn3courier_date," +
                   " case when delivered_date is null  then '---' else date_format(delivered_date, '%d-%m-%Y') end as dn1delivered_date, " +
                   " case when dn2delivered_date is null  then '---' else date_format(dn2delivered_date, '%d-%m-%Y') end as dn2delivered_date, " +
                   " case when dn3delivered_date is null  then '---' else date_format(dn3delivered_date, '%d-%m-%Y') end as dn3delivered_date, " +
                   " case when returened_date is null  then '---' else date_format(returened_date, '%d-%m-%Y') end as dn1returened_date, " +
                   " case when dn2returened_date is null  then '---' else date_format(dn2returened_date, '%d-%m-%Y') end as dn2returened_date, " +
                   " case when dn3returened_date is null  then '---' else date_format(dn3returened_date, '%d-%m-%Y') end as dn3returened_date, " +
                   " couriered_by as dn1couriered_by, dn2couriered_by,dn3couriered_by,p.status as dn1courier_status,dn2courier_status,dn3courier_status, " +
                   " case when dn3status_updated_date is not null then dn3status" +
                   " when dn2status_updated_date  is not null then dn2status" +
                   " when dn1status_created_date  is not null then dn1status" +
                   " when dn3generated_date  is not null then 'DN3 Generated'" +
                   " when dn2generated_date  is not null then 'DN2 Generated'" +
                   " when dn1generated_date  is not null then 'DN1 Generated' end as dnstatus," +
                   " dn3annexuredocument_name,dn3annexuredocument_path,  dn2annexuredocument_name,dn2annexuredocument_path,  dn1annexuredocument_name," +
                   " dn1annexuredocument_path,p.remarks as dn1remarks,p.dn2remarks,p.dn3remarks,a.tempdn1format_gid  from lgl_tmp_tdnformat a" + 
                   " left join lgl_trn_tdncases b on a.tempdn1format_gid = b.tempdn1format_gid" +
                   " left join ocs_mst_tcustomer x on a.customer_urn = x.customer_urn" +
                   " left join hrm_mst_temployee c on c.employee_gid = b.dn1status_created_by" +
                    " left join hrm_mst_temployee d on d.employee_gid = b.dn2status_updated_by" +
                    " left join hrm_mst_temployee e on e.employee_gid = b.dn3status_updated_by" +
                    " left join adm_mst_tuser f on f.user_gid = c.user_gid" +
                    " left join adm_mst_tuser g on g.user_gid = d.user_gid" +
                    " left join adm_mst_tuser h on h.user_gid = e.user_gid" +
                    " left join hrm_mst_temployee i on i.employee_gid = a.dn1generated_by" +
                    " left join hrm_mst_temployee j on j.employee_gid = a.dn2generated_by" +
                    " left join hrm_mst_temployee k on k.employee_gid = a.dn3generated_by" +
                    " left join adm_mst_tuser l on l.user_gid = i.user_gid" +
                    " left join adm_mst_tuser m on m.user_gid = j.user_gid" +
                    " left join adm_mst_tuser n on n.user_gid = k.user_gid" +
                    " left join lgl_trn_tsanctiondtl o on a.tempdn1format_gid = o.tempdn1format_gid" +
                    " left join lgl_trn_tcourierdetails p on b.dncase_gid = p.dncase_gid where a.customer_urn='"+ urn+"'" +
                    "  group by a.tempdn1format_gid";
         
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdn_list = new List<dn_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdn_list.Add(new dn_list
                    {
                        customer_name = (dr_datarow["customername"].ToString()),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        vertical = (dr_datarow["vertical_code"].ToString()),
                        dn1sanctionref_no = dr_datarow["dn1sanctionref_no"].ToString(),
                        dn2sanctionref_no = dr_datarow["dn2sanctionref_no"].ToString(),
                        dn3sanctionref_no = dr_datarow["dn3sanctionref_no"].ToString(),
                        dn1sanction_date = (dr_datarow["dn1sanction_date"].ToString()),
                        dn2sanction_date = (dr_datarow["dn2sanction_date"].ToString()),
                        dn3sanction_date = (dr_datarow["dn3sanction_date"].ToString()),
                        dn1sanction_amount = dr_datarow["dn1sanction_amount"].ToString(),
                        dn2sanction_amount = dr_datarow["dn2sanction_amount"].ToString(),
                        dn3sanction_amount = dr_datarow["dn3sanction_amount"].ToString(),
                        dn1generated_date = dr_datarow["dn1generated_date"].ToString(),
                        dn2generated_date = dr_datarow["dn2generated_date"].ToString(),
                        dn3generated_date = dr_datarow["dn3generated_date"].ToString(),
                        dn1sent_by = dr_datarow["dn1sent_by"].ToString(),
                        dn2sent_by = dr_datarow["dn2sent_by"].ToString(),
                        dn3sent_by = dr_datarow["dn3sent_by"].ToString(),
                        dn1sent_date = dr_datarow["dn1sent_date"].ToString(),
                        dn2sent_date = dr_datarow["dn2sent_date"].ToString(),
                        dn3sent_date = dr_datarow["dn3sent_date"].ToString(),
                        dn1generated_by = dr_datarow["dn1generated_by"].ToString(),
                        dn2generated_by = dr_datarow["dn2generated_by"].ToString(),
                        dn3generated_by = dr_datarow["dn3generated_by"].ToString(),
                        dn1courier_refno = dr_datarow["dn1courier_refno"].ToString(),
                        dn2courier_refno = dr_datarow["dn2courier_refno"].ToString(),
                        dn3courier_refno = dr_datarow["dn3courier_refno"].ToString(),
                        dn1courier_center = dr_datarow["dn1courier_center"].ToString(),
                        dn2courier_center = dr_datarow["dn2courier_center"].ToString(),
                        dn3courier_center = dr_datarow["dn3courier_center"].ToString(),
                        dn1courier_date = dr_datarow["dn1courier_date"].ToString(),
                        dn2courier_date = dr_datarow["dn2courier_date"].ToString(),
                        dn3courier_date = dr_datarow["dn3courier_date"].ToString(),
                        dn1delivered_date = dr_datarow["dn1delivered_date"].ToString(),
                        dn2delivered_date = dr_datarow["dn2delivered_date"].ToString(),
                        dn3delivered_date = dr_datarow["dn3delivered_date"].ToString(),
                        dn1returened_date = dr_datarow["dn1returened_date"].ToString(),
                        dn2returened_date = dr_datarow["dn2returened_date"].ToString(),
                        dn3returened_date = dr_datarow["dn3returened_date"].ToString(),
                        dn1couriered_by = dr_datarow["dn1couriered_by"].ToString(),
                        dn2couriered_by = dr_datarow["dn2couriered_by"].ToString(),
                        dn3couriered_by = dr_datarow["dn3couriered_by"].ToString(),
                        dn1courier_status = dr_datarow["dn1courier_status"].ToString(),
                        dn2courier_status = dr_datarow["dn2courier_status"].ToString(),
                        dn3courier_status = dr_datarow["dn3courier_status"].ToString(),
                        dnstatus = dr_datarow["dnstatus"].ToString(),
                        dn1annexuredocument_name = dr_datarow["dn1annexuredocument_name"].ToString(),
                        dn1annexuredocument_path = objcmnstorage.EncryptData(dr_datarow["dn1annexuredocument_path"].ToString()),
                        dn2annexuredocument_name = dr_datarow["dn2annexuredocument_name"].ToString(),
                        dn2annexuredocument_path = objcmnstorage.EncryptData(dr_datarow["dn2annexuredocument_path"].ToString()),
                        dn3annexuredocument_name = dr_datarow["dn3annexuredocument_name"].ToString(),
                        dn3annexuredocument_path = objcmnstorage.EncryptData(dr_datarow["dn3annexuredocument_path"].ToString()),
                        dn1remarks = dr_datarow["dn1remarks"].ToString(),
                        dn2remarks = dr_datarow["dn2remarks"].ToString(),
                        dn3remarks = dr_datarow["dn3remarks"].ToString(),
                        tempdn1format_gid = dr_datarow["tempdn1format_gid"].ToString()
                    });
                }
                values.dn_list = getdn_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
    }
}