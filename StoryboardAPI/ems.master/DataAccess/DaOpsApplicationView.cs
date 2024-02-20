using ems.master.Models;
using ems.utilities.Functions;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Web;
using ems.storage.Functions;

namespace ems.master.DataAccess
{
    public class DaOpsApplicationView
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, lsopsapplication_gid, lsopsapplication_gidcontact;

        public void DaGetOPSApplicationBasicView(string opsapplication_gid, MdlOpsApplicationView values)
        {
            try
            {
                msSQL = " select a.opsapplication_gid, application_no, customer_urn, customerref_name as customer_name, vertical_name, verticaltaggs_name, " +
                        " constitution_name, businessunit_name, vernacular_language, sa_status, sa_id, sa_name, " +
                        " designation_type, landline_no, concat_ws(' ', contactpersonfirst_name, contactpersonmiddle_name, contactpersonlast_name) as contactperson_name," +
                        " b.primaryvaluechain_name, c.secondaryvaluechain_name, social_capital, trade_capital" +
                        " from ocs_mst_topsapplication a" +
                        " left join ocs_mst_topsapplication2primaryvaluechain b on b.opsapplication_gid = a.opsapplication_gid " +
                        " left join ocs_mst_topsapplication2secondaryvaluechain c on c.opsapplication_gid = a.opsapplication_gid " +
                        " where a.opsapplication_gid='" + opsapplication_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.opsapplication_gid = objODBCDatareader["opsapplication_gid"].ToString();
                    values.application_no = objODBCDatareader["application_no"].ToString();
                    values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                    values.customer_name = objODBCDatareader["customer_name"].ToString();
                    values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                    values.verticaltaggs_name = objODBCDatareader["verticaltaggs_name"].ToString();
                    values.constitution_name = objODBCDatareader["constitution_name"].ToString();
                    values.businessunit_name = objODBCDatareader["businessunit_name"].ToString();
                    values.vernacular_language = objODBCDatareader["vernacular_language"].ToString();
                    values.sa_status = objODBCDatareader["sa_status"].ToString();
                    values.sa_id = objODBCDatareader["sa_id"].ToString();
                    values.sa_name = objODBCDatareader["sa_name"].ToString();
                    values.landline_no = objODBCDatareader["landline_no"].ToString();
                    values.designation_type = objODBCDatareader["designation_type"].ToString();
                    values.contactperson_name = objODBCDatareader["contactperson_name"].ToString();
                    values.primaryvaluechain_name = objODBCDatareader["primaryvaluechain_name"].ToString();
                    values.secondaryvaluechain_name = objODBCDatareader["secondaryvaluechain_name"].ToString();
                    values.social_capital = objODBCDatareader["social_capital"].ToString();
                    values.trade_capital = objODBCDatareader["trade_capital"].ToString();

                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

                msSQL = " select opsapplication_gid from ocs_mst_topsinstitution " +
                        " where opsapplication_gid='" + opsapplication_gid + "' and (stakeholder_type='Applicant' or stakeholder_type='Borrower')";
                lsopsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsopsapplication_gid != "")
                {
                    values.borrower_flag = "Y";
                    values.borrower_type = "Institution";
                }
                else
                {
                    msSQL = " select opsapplication_gid from ocs_mst_tcontact " +
                            " where opsapplication_gid='" + opsapplication_gid + "' and (stakeholder_type='Applicant' or stakeholder_type='Borrower')";
                    lsopsapplication_gidcontact = objdbconn.GetExecuteScalar(msSQL);
                    if (lsopsapplication_gidcontact != "")
                    {
                        values.borrower_flag = "N";
                        values.borrower_type = "Individual";
                    }
                    else
                    {
                        values.borrower_type = "";
                        values.borrower_flag = "";
                    }
                }
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetOPSMobileMailDetailsView(string opsapplication_gid, MdlOpsApplicationView values)
        {
            msSQL = "select mobile_no from ocs_mst_topsapplication2contactno where opsapplication_gid='" + opsapplication_gid + "' and primary_mobileno = 'Yes'";
            values.primary_mobileno = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select email_address from ocs_mst_topsapplication2email where opsapplication_gid='" + opsapplication_gid + "' and primary_emailaddress = 'Yes'";
            values.primary_email = objdbconn.GetExecuteScalar(msSQL);
            try
            {
                msSQL = " select opsapplication2contact_gid, opsapplication_gid, mobile_no, whatsapp_mobileno " +
                        " from ocs_mst_topsapplication2contactno " +
                        " where opsapplication_gid = '" + opsapplication_gid + "' and primary_mobileno = 'No' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopsapplication_list = new List<opsmobilenumber_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getopsapplication_list.Add(new opsmobilenumber_list
                        {
                            opsapplication_gid = (dr_datarow["opsapplication_gid"].ToString()),
                            mobile_no = (dr_datarow["mobile_no"].ToString()),
                            whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString()),

                        });
                    }
                    values.opsmobilenumber_list = getopsapplication_list;
                }
                dt_datatable.Dispose();
                values.status = true;

                msSQL = " select opsapplication2email_gid, opsapplication_gid, email_address " +
                        " from ocs_mst_topsapplication2email " +
                        " where opsapplication_gid = '" + opsapplication_gid + "' and primary_emailaddress = 'No' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopsmail_list = new List<opsmail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getopsmail_list.Add(new opsmail_list
                        {
                            opsapplication_gid = (dr_datarow["opsapplication_gid"].ToString()),
                            opsapplication2email_gid = (dr_datarow["opsapplication2email_gid"].ToString()),
                            email_address = (dr_datarow["email_address"].ToString()),

                        });
                    }
                    values.opsmail_list = getopsmail_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetOPSGeneticDetailsView(string opsapplication_gid, MdlOpsApplicationView values)
        {
            try
            {
                msSQL = " select geneticcode_name, genetic_status, genetic_remarks, opsapplication_gid, geneticcode_gid " +
                        " from ocs_mst_topsapplication2geneticcode " +
                        " where opsapplication_gid = '" + opsapplication_gid + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopsgenetic_list = new List<opsgeneticdetails_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getopsgenetic_list.Add(new opsgeneticdetails_list
                        {
                            opsapplication_gid = (dr_datarow["opsapplication_gid"].ToString()),
                            geneticcode_name = (dr_datarow["geneticcode_name"].ToString()),
                            genetic_status = (dr_datarow["genetic_status"].ToString()),
                            genetic_remarks = (dr_datarow["genetic_remarks"].ToString()),

                        });
                    }
                    values.opsgeneticdetails_list = getopsgenetic_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetOPSInstitutionList(string opsapplication_gid, MdlOPSCreditView values)
        {
            msSQL = " select a.opsinstitution_gid, a.opsapplication_gid, cin_no, companytype_name, " +
                        " company_name, companypan_no, date_format(date_incorporation, '%d-%m-%Y') as date_incorporation," +
                        " date_format(a.created_date, '%d-%m-%Y') as created_date," +
                        "concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,stakeholder_type from ocs_mst_topsinstitution a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                        " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                        " where opsapplication_gid = '" + opsapplication_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getopsinstitutionList = new List<opsinstitution_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getopsinstitutionList.Add(new opsinstitution_List
                    {
                        opsinstitution_gid = (dr_datarow["opsinstitution_gid"].ToString()),
                        company_name = (dr_datarow["company_name"].ToString()),
                        companypan_no = (dr_datarow["companypan_no"].ToString()),
                        cin_no = (dr_datarow["cin_no"].ToString()),
                        companytype_name = (dr_datarow["companytype_name"].ToString()),
                        date_incorporation = (dr_datarow["date_incorporation"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                    });
                }
                values.opsinstitution_List = getopsinstitutionList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        
        public void DaGetOPSIndividualList(string opsapplication_gid, MdlOPSCreditView values)
        {
            msSQL = " select a.opscontact_gid, a.opsapplication_gid, concat_ws(' ', first_name, last_name, middle_name) as individual_name, " +
                        " a.pan_no, aadhar_no, date_format(individual_dob, '%d-%m-%Y') as individual_dob," +
                        " main_occupation, date_format(a.created_date, '%d-%m-%Y') as created_date," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,stakeholder_type,institution_name from ocs_mst_topscontact a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                        " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                        " where opsapplication_gid = '" + opsapplication_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getopsindividualList = new List<opsindividual_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getopsindividualList.Add(new opsindividual_List
                    {
                        opscontact_gid = (dr_datarow["opscontact_gid"].ToString()),
                        individual_name = (dr_datarow["individual_name"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        aadhar_no = (dr_datarow["aadhar_no"].ToString()),
                        individual_dob = (dr_datarow["individual_dob"].ToString()),
                        main_occupation = (dr_datarow["main_occupation"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                        company_name = (dr_datarow["institution_name"].ToString()),
                    });
                }
                values.opsindividual_List = getopsindividualList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        
        public void DaGetOPSRMDetailsView(string opsapplication_gid, MdlOPSRMDtlView values)
        {
            try
            {
                msSQL = " select d.department_name,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as RM_Name, " +
                        " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as opsapplicationinitiated_date from ocs_mst_topsapplication a " +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join hrm_mst_tdepartment d on d.department_gid = b.department_gid " +
                        " where opsapplication_gid = '" + opsapplication_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.department_name = objODBCDatareader["department_name"].ToString();
                    values.RM_Name = objODBCDatareader["RM_Name"].ToString();
                    values.opsapplicationinitiated_date = objODBCDatareader["opsapplicationinitiated_date"].ToString();
                }
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetOPSGurantorInstitutionView(string opsinstitution_gid, MdlOPSInstitutionDtlView values)
        {
            try
            {
                msSQL = " select opsinstitution_gid, opsapplication_gid, company_name, companypan_no, date_format(date_incorporation, '%d-%m-%Y') as date_incorporation, " +
                        " year_business, month_business, cin_no, official_telephoneno, officialemail_address, companytype_name, escrow, " +
                        " lastyear_turnover, date_format(start_date, '%d-%m-%Y') as start_date, " +
                        " date_format(end_date, '%d-%m-%Y') as end_date, assessmentagency_name, " +
                        " assessmentagencyrating_name, date_format(ratingas_on, '%d-%m-%Y') as ratingas_on, " +
                        " amlcategory_name, businesscategory_name, urn_status, urn from ocs_mst_topsinstitution " +
                        " where opsinstitution_gid = '" + opsinstitution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.opsapplication_gid = objODBCDatareader["opsapplication_gid"].ToString();
                    values.opsinstitution_gid = objODBCDatareader["opsinstitution_gid"].ToString();
                    values.company_name = objODBCDatareader["company_name"].ToString();
                    values.companypan_no = objODBCDatareader["companypan_no"].ToString();
                    values.date_incorporation = objODBCDatareader["date_incorporation"].ToString();
                    values.year_business = objODBCDatareader["year_business"].ToString();
                    values.month_business = objODBCDatareader["month_business"].ToString();
                    values.cin_no = objODBCDatareader["cin_no"].ToString();
                    values.official_telephoneno = objODBCDatareader["official_telephoneno"].ToString();
                    values.officialemail_address = objODBCDatareader["officialemail_address"].ToString();
                    values.companytype_name = objODBCDatareader["companytype_name"].ToString();
                    values.escrow = objODBCDatareader["escrow"].ToString();
                    values.lastyear_turnover = objODBCDatareader["lastyear_turnover"].ToString();
                    values.start_date = objODBCDatareader["start_date"].ToString();
                    values.end_date = objODBCDatareader["end_date"].ToString();
                    values.assessmentagency_name = objODBCDatareader["assessmentagency_name"].ToString();
                    values.assessmentagencyrating_name = objODBCDatareader["assessmentagencyrating_name"].ToString();
                    values.ratingas_on = objODBCDatareader["ratingas_on"].ToString();
                    values.amlcategory_name = objODBCDatareader["amlcategory_name"].ToString();
                    values.businesscategory_name = objODBCDatareader["businesscategory_name"].ToString();
                    values.borrower_type = "Institution";

                    values.urn_status = objODBCDatareader["urn_status"].ToString();
                    values.urn = objODBCDatareader["urn"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = "select opsinstitution2branch_gid,gst_state,gst_no, gst_registered from ocs_mst_topsinstitution2branch where opsinstitution_gid='" + opsinstitution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopsgst_list = new List<opsgst_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getopsgst_list.Add(new opsgst_list
                        {
                            opsinstitution2branch_gid = (dr_datarow["opsinstitution2branch_gid"].ToString()),
                            gst_state = (dr_datarow["gst_state"].ToString()),
                            gst_no = (dr_datarow["gst_no"].ToString()),
                            gst_registered = (dr_datarow["gst_registered"].ToString())
                        });
                    }
                    values.opsgst_list = getopsgst_list;
                }
                dt_datatable.Dispose();

                msSQL = "  select opsinstitution2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, landmark," +
                    " postal_code, city from ocs_mst_topsinstitution2address where opsinstitution_gid='" + opsinstitution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopsaddress_list = new List<opsaddress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getopsaddress_list.Add(new opsaddress_list
                        {
                            opsinstitution2address_gid = (dr_datarow["opsinstitution2address_gid"].ToString()),
                            address_type = (dr_datarow["addresstype_name"].ToString()),
                            primary_status = (dr_datarow["primary_status"].ToString()),
                            addressline1 = (dr_datarow["addressline1"].ToString()),
                            addressline2 = (dr_datarow["addressline2"].ToString()),
                            taluka = (dr_datarow["taluka"].ToString()),
                            district = (dr_datarow["district"].ToString()),
                            state = (dr_datarow["state"].ToString()),
                            country = (dr_datarow["country"].ToString()),
                            postal_code = (dr_datarow["postal_code"].ToString()),
                            city = (dr_datarow["city"].ToString()),
                            landmark = (dr_datarow["landmark"].ToString())
                        });
                    }
                    values.opsaddress_list = getopsaddress_list;
                }
                dt_datatable.Dispose();


                msSQL = "select mobile_no from ocs_mst_topsinstitution2mobileno where opsinstitution_gid='" + opsinstitution_gid + "' and primary_status = 'Yes'";
                values.primaryinstitution_mobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from ocs_mst_topsinstitution2email where opsinstitution_gid='" + opsinstitution_gid + "' and primary_status = 'Yes'";
                values.primaryinstitution_email = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select opsinstitution_gid, mobile_no, whatsapp_no " +
                        " from ocs_mst_topsinstitution2mobileno " +
                        " where opsinstitution_gid = '" + opsinstitution_gid + "' and primary_status = 'No' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopsapplication_list = new List<opsinstituionmobilenumber_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getopsapplication_list.Add(new opsinstituionmobilenumber_list
                        {
                            opsinstitution_gid = (dr_datarow["opsinstitution_gid"].ToString()),
                            mobile_no = (dr_datarow["mobile_no"].ToString()),
                            whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),

                        });
                    }
                    values.opsinstituionmobilenumber_list = getopsapplication_list;
                }
                dt_datatable.Dispose();
                values.status = true;

                msSQL = " select opsinstitution_gid, email_address " +
                        " from ocs_mst_topsinstitution2email " +
                        " where opsinstitution_gid = '" + opsinstitution_gid + "' and primary_status = 'No' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopsmail_list = new List<opsinstituionmail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getopsmail_list.Add(new opsinstituionmail_list
                        {
                            email_address = (dr_datarow["email_address"].ToString()),

                        });
                    }
                    values.opsinstituionmail_list = getopsmail_list;
                }
                dt_datatable.Dispose();


                msSQL = " select opsinstitution2form60documentupload_gid,form60document_name,form60document_path from ocs_mst_topsinstitution2form60documentupload " +
                               " where opsinstitution_gid ='" + opsinstitution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopsdocumentdtlList = new List<opsinstitutionform60_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getopsdocumentdtlList.Add(new opsinstitutionform60_list
                        {
                            document_name = dt["form60document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData(dt["form60document_path"].ToString()),
                            opsinstitution2form60documentupload_gid = dt["opsinstitution2form60documentupload_gid"].ToString()
                        });
                        values.opsinstitutionform60_list = getopsdocumentdtlList;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select opsinstitution2documentupload_gid,opsinstitution_gid,document_name,document_path,document_title,document_id from ocs_mst_topsinstitution2documentupload " +
                        " where opsinstitution_gid='" + opsinstitution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopsdocumentList = new List<opsinstitutiondoc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getopsdocumentList.Add(new opsinstitutiondoc_list
                        {
                            document_name = dt["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                            opsinstitution2documentupload_gid = dt["opsinstitution2documentupload_gid"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            document_id = dt["document_id"].ToString(),
                        });
                        values.opsinstitutiondoc_list = getopsdocumentList;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select opsinstitution2licensedtl_gid,licensetype_gid,licensetype_name,license_no,date_format(issue_date,'%d-%m-%Y') as issue_date," +
                        " date_format(expiry_date,'%d-%m-%Y') as expiry_date from ocs_mst_topsinstitution2licensedtl" +
                        " where opsinstitution_gid='" + opsinstitution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopslicense_list = new List<opslicense_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getopslicense_list.Add(new opslicense_list
                        {
                            opsinstitution2licensedtl_gid = (dr_datarow["opsinstitution2licensedtl_gid"].ToString()),
                            licensetype_gid = (dr_datarow["licensetype_gid"].ToString()),
                            licensetype_name = (dr_datarow["licensetype_name"].ToString()),
                            license_number = (dr_datarow["license_no"].ToString()),
                            licenseissue_date = (dr_datarow["issue_date"].ToString()),
                            licenseexpiry_date = (dr_datarow["expiry_date"].ToString())
                        });
                    }
                    values.opslicense_list = getopslicense_list;
                }
                dt_datatable.Dispose();


                msSQL = " select bureauname_gid,bureauname_name, bureau_score,date_format(bureauscore_date,'%d-%m-%Y') as bureauscore_date, observations, bureau_response " +
                       " from ocs_mst_topsinstitution where opsinstitution_gid='" + opsinstitution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.bureauname_gid = objODBCDatareader["bureauname_gid"].ToString();
                    values.bureauname_name = objODBCDatareader["bureauname_name"].ToString();
                    values.bureau_score = objODBCDatareader["bureau_score"].ToString();
                    values.observations = objODBCDatareader["observations"].ToString();
                    values.bureau_response = objODBCDatareader["bureau_response"].ToString();
                    values.bureauscore_date = objODBCDatareader["bureauscore_date"].ToString();
                   
                }
                objODBCDatareader.Close();

                msSQL = "select cicdocument_name,cicdocument_path from ocs_mst_topsinstitution2cicdocumentupload" +
                        " where opsinstitution_gid='" + opsinstitution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.cicdocument_name = objODBCDatareader["cicdocument_name"].ToString();
                    values.cicdocument_path = objcmnstorage.EncryptData(objODBCDatareader["cicdocument_path"].ToString());
                }

                objODBCDatareader.Close();

                values.status = true;
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetOPSGurantorIndividualView(string opscontact_gid, MdlOPSIndividualDtlView values)
        {
            try
            {
                msSQL = " select opscontact_gid, opsapplication_gid, concat_ws(' ', first_name, last_name, middle_name) as individual_name, " +
                        " pan_no, aadhar_no, individual_dob," +
                        " age, gender_name, main_occupation, pep_status, date_format(pepverified_date, '%d-%m-%Y') as pepverified_date, " +
                        " maritalstatus_name, concat_ws(' ', father_firstname, father_middlename, father_lastname) as father_name, " +
                        " father_dob, father_age, " +
                        " concat_ws(' ', mother_firstname, mother_middlename, mother_lastname) as mother_name, " +
                        " mother_dob, mother_age, " +
                        " concat_ws(' ', spouse_firstname, spouse_middlename, spouse_lastname) as spouse_name, " +
                        " spouse_dob, spouse_age, educationalqualification_name, " +
                        " annual_income, monthly_income, user_type, ownershiptype_name, propertyholder_name, residencetype_name, " +
                        " currentresidence_years, branch_distance, bureauname_name, bureau_score, observations, " +
                        " date_format(bureauscore_date, '%d-%m-%Y') as bureauscore_date, bureau_response,  " +
                        " group_name, profile, urn_status, urn, fathernominee_status, mothernominee_status, spousenominee_status, othernominee_status,institution_name," +
                        " relationshiptype, nomineefirst_name, nominee_middlename, nominee_lastname, nominee_dob, nominee_age, totallandinacres, cultivatedland, previouscrop, prposedcrop" +
                        " from ocs_mst_topscontact " +
                        " where opscontact_gid = '" + opscontact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.opsapplication_gid = objODBCDatareader["opsapplication_gid"].ToString();
                    values.opscontact_gid = objODBCDatareader["opscontact_gid"].ToString();
                    values.individual_name = objODBCDatareader["individual_name"].ToString();
                    values.pan_no = objODBCDatareader["pan_no"].ToString();
                    values.aadhar_no = objODBCDatareader["aadhar_no"].ToString();
                    values.individual_dob = objODBCDatareader["individual_dob"].ToString();
                    values.age = objODBCDatareader["age"].ToString();
                    values.gender_name = objODBCDatareader["gender_name"].ToString();
                    values.main_occupation = objODBCDatareader["main_occupation"].ToString();
                    values.pep_status = objODBCDatareader["pep_status"].ToString();
                    values.pepverified_date = objODBCDatareader["pepverified_date"].ToString();
                    values.maritalstatus_name = objODBCDatareader["maritalstatus_name"].ToString();
                    values.father_name = objODBCDatareader["father_name"].ToString();
                    values.father_dob = objODBCDatareader["father_dob"].ToString();
                    values.father_age = objODBCDatareader["father_age"].ToString();
                    values.mother_name = objODBCDatareader["mother_name"].ToString();
                    values.mother_dob = objODBCDatareader["mother_dob"].ToString();
                    values.mother_age = objODBCDatareader["mother_age"].ToString();
                    values.spouse_name = objODBCDatareader["spouse_name"].ToString();
                    values.spouse_dob = objODBCDatareader["spouse_dob"].ToString();
                    values.spouse_age = objODBCDatareader["spouse_age"].ToString();
                    values.educationalqualification_name = objODBCDatareader["educationalqualification_name"].ToString();
                    values.annual_income = objODBCDatareader["annual_income"].ToString();
                    values.monthly_income = objODBCDatareader["monthly_income"].ToString();
                    values.user_type = objODBCDatareader["user_type"].ToString();
                    values.ownershiptype_name = objODBCDatareader["ownershiptype_name"].ToString();
                    values.propertyholder_name = objODBCDatareader["propertyholder_name"].ToString();
                    values.residencetype_name = objODBCDatareader["residencetype_name"].ToString();
                    values.currentresidence_years = objODBCDatareader["currentresidence_years"].ToString();
                    values.branch_distance = objODBCDatareader["branch_distance"].ToString();
                    values.indbureauname_name = objODBCDatareader["bureauname_name"].ToString();
                    values.indbureau_score = objODBCDatareader["bureau_score"].ToString();
                    values.indobservations = objODBCDatareader["observations"].ToString();
                    values.indbureauscore_date = objODBCDatareader["bureauscore_date"].ToString();
                    values.indbureau_response = objODBCDatareader["bureau_response"].ToString();
                    values.borrower_type = "Individual";

                    values.group_name = objODBCDatareader["group_name"].ToString();
                    values.profile = objODBCDatareader["profile"].ToString();
                    values.urn_status = objODBCDatareader["urn_status"].ToString();
                    values.urn = objODBCDatareader["urn"].ToString();
                    values.fathernominee_status = objODBCDatareader["fathernominee_status"].ToString();
                    values.mothernominee_status = objODBCDatareader["mothernominee_status"].ToString();
                    values.spousenominee_status = objODBCDatareader["spousenominee_status"].ToString();
                    values.othernominee_status = objODBCDatareader["othernominee_status"].ToString();
                    values.relationshiptype = objODBCDatareader["relationshiptype"].ToString();
                    values.nomineefirst_name = objODBCDatareader["nomineefirst_name"].ToString();
                    values.nominee_middlename = objODBCDatareader["nominee_middlename"].ToString();
                    values.nominee_lastname = objODBCDatareader["nominee_lastname"].ToString();
                    values.nominee_dob = objODBCDatareader["nominee_dob"].ToString();
                    values.nominee_age = objODBCDatareader["nominee_age"].ToString();
                    values.totallandinacres = objODBCDatareader["totallandinacres"].ToString();
                    values.cultivatedland = objODBCDatareader["cultivatedland"].ToString();
                    values.previouscrop = objODBCDatareader["previouscrop"].ToString();
                    values.prposedcrop = objODBCDatareader["prposedcrop"].ToString();
                    values.institution_name = objODBCDatareader["institution_name"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select opscontact2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country," +
                   " postal_code, landmark, city from ocs_mst_topscontact2address where opscontact_gid='" + opscontact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopscontactaddress_list = new List<opscontactaddress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getopscontactaddress_list.Add(new opscontactaddress_list
                        {
                            opscontact2address_gid = (dr_datarow["opscontact2address_gid"].ToString()),
                            addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                            primary_status = (dr_datarow["primary_status"].ToString()),
                            addressline1 = (dr_datarow["addressline1"].ToString()),
                            addressline2 = (dr_datarow["addressline2"].ToString()),
                            taluka = (dr_datarow["taluka"].ToString()),
                            district = (dr_datarow["district"].ToString()),
                            state = (dr_datarow["state"].ToString()),
                            country = (dr_datarow["country"].ToString()),
                            postal_code = (dr_datarow["postal_code"].ToString()),
                            landmark = (dr_datarow["landmark"].ToString()),
                            city = (dr_datarow["city"].ToString()),
                        });
                    }
                    values.opscontactaddress_list = getopscontactaddress_list;
                }
                dt_datatable.Dispose();


                msSQL = "select mobile_no from ocs_mst_topscontact2mobileno where opscontact_gid='" + opscontact_gid + "' and primary_status = 'Yes'";
                values.primaryindividual_mobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from ocs_mst_topscontact2email where opscontact_gid='" + opscontact_gid + "' and primary_status = 'Yes'";
                values.primaryindividual_email = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address,opscontact2email_gid,primary_status from ocs_mst_topscontact2email where " +
                         " opscontact_gid='" + opscontact_gid + "' and primary_status = 'No'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopscontactemail_list = new List<opscontactemail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getopscontactemail_list.Add(new opscontactemail_list
                        {
                            opscontact2email_gid = (dr_datarow["opscontact2email_gid"].ToString()),
                            email_address = (dr_datarow["email_address"].ToString()),
                            primary_status = (dr_datarow["primary_status"].ToString()),
                        });
                    }
                }
                values.opscontactemail_list = getopscontactemail_list;
                dt_datatable.Dispose();

                msSQL = "select mobile_no,opscontact2mobileno_gid,primary_status,whatsapp_no from ocs_mst_topscontact2mobileno where " +
                        " opscontact_gid='" + opscontact_gid + "' and primary_status = 'No'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopscontactmobileno_list = new List<opscontactmobileno_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getopscontactmobileno_list.Add(new opscontactmobileno_list
                        {
                            opscontact2mobileno_gid = (dr_datarow["opscontact2mobileno_gid"].ToString()),
                            mobile_no = (dr_datarow["mobile_no"].ToString()),
                            primary_status = (dr_datarow["primary_status"].ToString()),
                            whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),
                        });
                    }
                }
                values.opscontactmobileno_list = getopscontactmobileno_list;
                dt_datatable.Dispose();

                msSQL = "select opscontact2idproof_gid,idproof_name,idproof_no,document_name, document_path from ocs_mst_topscontact2idproof where " +
                        " opscontact_gid='" + opscontact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopscontactidproof_list = new List<opscontactidproof_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getopscontactidproof_list.Add(new opscontactidproof_list
                        {
                            opscontact2idproof_gid = (dr_datarow["opscontact2idproof_gid"].ToString()),
                            idproof_name = (dr_datarow["idproof_name"].ToString()),
                            idproof_no = (dr_datarow["idproof_no"].ToString()),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        });

                        values.opscontactidproof_list = getopscontactidproof_list;
                    }
                    dt_datatable.Dispose();
                }

                msSQL = " select document_title,document_name,document_path from ocs_mst_topscontact2document " +
                                " where opscontact_gid='" + opscontact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopsdocumentdtlList = new List<uploadopsindividualdoc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getopsdocumentdtlList.Add(new uploadopsindividualdoc_list
                        {
                            document_title = dt["document_title"].ToString(),
                            document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                            document_name = dt["document_name"].ToString(),
                        });
                        values.uploadopsindividualdoc_list = getopsdocumentdtlList;
                    }
                }
                dt_datatable.Dispose();

                msSQL = "select cicdocument_name,cicdocument_path from ocs_mst_topsindividual2cicdocumentupload " +
                       " where opscontact_gid='" + opscontact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.indcicdocument_name = objODBCDatareader["cicdocument_name"].ToString();
                    values.indcicinddocument_path = objcmnstorage.EncryptData(objODBCDatareader["cicdocument_path"].ToString());
                }

                objODBCDatareader.Close();

            }
            catch
            {
                values.status = false;
            }

        }

        public void DaGetOPSProductChargesDtl(string opsapplication_gid, MdlOPSProductChargesView values)
        {
            try
            {
                msSQL = "select opsapplication2loan_gid,date_format(facilityrequested_date, '%d-%m-%Y') as facilityrequested_date, product_type, " +
                               " productsub_type, loanfacility_amount, loan_type, rate_interest, penal_interest, facilityoverall_limit, " +
                               " tenureoverall_limit, facility_type, facility_mode, principalfrequency_name, interestfrequency_name, " +
                               " interest_status, moratorium_type, moratorium_status, date_format(moratorium_startdate, '%d-%m-%Y') as moratorium_startdate, " +
                               " date_format(moratorium_enddate, '%d-%m-%Y') as moratorium_enddate,scheme_type from ocs_mst_topsapplication2loan " +
                               " where opsapplication_gid='" + opsapplication_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopsloansummary_list = new List<opsLoan_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getopsloansummary_list.Add(new opsLoan_list
                        {
                            opsapplication2loan_gid = (dr_datarow["opsapplication2loan_gid"].ToString()),
                            facilityrequested_date = (dr_datarow["facilityrequested_date"].ToString()),
                            product_type = (dr_datarow["product_type"].ToString()),
                            productsub_type = (dr_datarow["productsub_type"].ToString()),
                            loanfacility_amount = (dr_datarow["loanfacility_amount"].ToString()),
                            loan_type = (dr_datarow["loan_type"].ToString()),
                            rate_interest = (dr_datarow["rate_interest"].ToString()),
                            penal_interest = (dr_datarow["penal_interest"].ToString()),
                            facilityoverall_limit = (dr_datarow["facilityoverall_limit"].ToString()),
                            tenureoverall_limit = (dr_datarow["tenureoverall_limit"].ToString()),
                            facility_type = (dr_datarow["facility_type"].ToString()),
                            facility_mode = (dr_datarow["facility_mode"].ToString()),
                            principalfrequency_name = (dr_datarow["principalfrequency_name"].ToString()),
                            interestfrequency_name = (dr_datarow["interestfrequency_name"].ToString()),
                            interest_status = (dr_datarow["interest_status"].ToString()),
                            moratorium_status = (dr_datarow["moratorium_status"].ToString()),
                            moratorium_type = (dr_datarow["moratorium_type"].ToString()),
                            moratorium_startdate = (dr_datarow["moratorium_startdate"].ToString()),
                            moratorium_enddate = (dr_datarow["moratorium_enddate"].ToString()),
                            scheme_type = (dr_datarow["scheme_type"].ToString()),

                        });
                    }
                    values.opsLoan_list = getopsloansummary_list;
                }
                dt_datatable.Dispose();

                msSQL = " select overalllimit_amount, validityoveralllimit_year, validityoveralllimit_month, validityoveralllimit_days, " +
                                " calculationoveralllimit_validity from ocs_mst_topsapplication " +
                                " where opsapplication_gid='" + opsapplication_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.overalllimit_amount = objODBCDatareader["overalllimit_amount"].ToString();
                    values.validityoveralllimit_year = objODBCDatareader["validityoveralllimit_year"].ToString();
                    values.validityoveralllimit_month = objODBCDatareader["validityoveralllimit_month"].ToString();
                    values.validityoveralllimit_days = objODBCDatareader["validityoveralllimit_days"].ToString();
                    values.calculationoveralllimit_validity = objODBCDatareader["calculationoveralllimit_validity"].ToString();
                }
                objODBCDatareader.Close();


                msSQL = " select opsapplication2loan_gid, source_type, guideline_value, date_format(guideline_date, '%d-%m-%Y') as guideline_date, " +
                        " date_format(marketvalue_date, '%d-%m-%Y') as marketvalue_date, market_value, forcedsource_value, " +
                        " collateralSSV_value, date_format(forcedvalueassessed_on, '%d-%m-%Y') as forcedvalueassessed_on, " +
                        " collateralobservation_summary from ocs_mst_topsapplication2loan  " +
                        " where opsapplication_gid='" + opsapplication_gid + "' and product_type ='Agri Receivable Finance (ARF)' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopscollateral_list = new List<opscollateral_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getopscollateral_list.Add(new opscollateral_list
                        {
                            opsapplication2loan_gid = (dr_datarow["opsapplication2loan_gid"].ToString()),
                            source_type = (dr_datarow["source_type"].ToString()),
                            guideline_value = (dr_datarow["guideline_value"].ToString()),
                            guideline_date = (dr_datarow["guideline_date"].ToString()),
                            marketvalue_date = (dr_datarow["marketvalue_date"].ToString()),
                            market_value = (dr_datarow["market_value"].ToString()),
                            forcedsource_value = (dr_datarow["forcedsource_value"].ToString()),
                            collateralSSV_value = (dr_datarow["collateralSSV_value"].ToString()),
                            forcedvalueassessed_on = (dr_datarow["forcedvalueassessed_on"].ToString()),
                            collateralobservation_summary = (dr_datarow["collateralobservation_summary"].ToString()),
                        });
                    }
                    values.opscollateral_list = getopscollateral_list;
                }
                dt_datatable.Dispose();

                msSQL = "select processing_fee, processing_collectiontype, doc_charges, doccharge_collectiontype, fieldvisit_charges," +
                        " fieldvisit_charges_collectiontype, adhoc_fee, adhoc_collectiontype, life_insurance, lifeinsurance_collectiontype, " +
                        " acct_insurance, total_collect, total_deduct,product_type,producttype_gid from ocs_mst_topsapplicationservicecharge " +
                        " where opsapplication_gid='" + opsapplication_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopsservicecharge_list = new List<opsservicecharge_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getopsservicecharge_list.Add(new opsservicecharge_List
                        {
                            producttype_gid = (dr_datarow["producttype_gid"].ToString()),
                            product_type = (dr_datarow["product_type"].ToString()),
                            processing_fee = (dr_datarow["processing_fee"].ToString()),
                            processing_collectiontype = (dr_datarow["processing_collectiontype"].ToString()),
                            doc_charges = (dr_datarow["doc_charges"].ToString()),
                            doccharge_collectiontype = (dr_datarow["doccharge_collectiontype"].ToString()),
                            fieldvisit_charge = (dr_datarow["fieldvisit_charges"].ToString()),
                            fieldvisit_collectiontype = (dr_datarow["fieldvisit_charges_collectiontype"].ToString()),
                            adhoc_fee = (dr_datarow["adhoc_fee"].ToString()),
                            adhoc_collectiontype = (dr_datarow["adhoc_collectiontype"].ToString()),
                            life_insurance = (dr_datarow["life_insurance"].ToString()),
                            lifeinsurance_collectiontype = (dr_datarow["lifeinsurance_collectiontype"].ToString()),
                            acct_insurance = (dr_datarow["acct_insurance"].ToString()),
                            total_collect = (dr_datarow["total_collect"].ToString()),
                            total_deduct = (dr_datarow["total_deduct"].ToString())
                        });
                    }
                    values.opsservicecharge_List = getopsservicecharge_list;
                }
                dt_datatable.Dispose();

                msSQL = " select opsapplication2hypothecation_gid, security_type, security_description, security_value, " +
                        " date_format(securityassessed_date, '%d-%m-%Y') as securityassessed_date, asset_id, roc_fillingid, " +
                        " CERSAI_fillingid, hypoobservation_summary, primary_security " +
                        " from ocs_mst_topsapplication2hypothecation " +
                        " where opsapplication_gid='" + opsapplication_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.opsapplication2hypothecation_gid = objODBCDatareader["opsapplication2hypothecation_gid"].ToString();
                    values.security_type = objODBCDatareader["security_type"].ToString();
                    values.security_description = objODBCDatareader["security_description"].ToString();
                    values.security_value = objODBCDatareader["security_value"].ToString();
                    values.securityassessed_date = objODBCDatareader["securityassessed_date"].ToString();
                    values.asset_id = objODBCDatareader["asset_id"].ToString();
                    values.roc_fillingid = objODBCDatareader["roc_fillingid"].ToString();
                    values.CERSAI_fillingid = objODBCDatareader["CERSAI_fillingid"].ToString();
                    values.hypoobservation_summary = objODBCDatareader["hypoobservation_summary"].ToString();
                    values.primary_security = objODBCDatareader["primary_security"].ToString();
                }
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }

        }

        public void DaGetOPSHypoDocDtl(string opsapplication2hypothecation_gid, MdlOPSProductChargesView values)
        {
            try
            {
                msSQL = " select opsuploadhypothecationdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                      " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                      " from ocs_mst_topsuploadhypothecationdocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                      " and b.user_gid = c.user_gid and opsapplication2hypothecation_gid='" + opsapplication2hypothecation_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<opsHypoDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new opsHypoDocumentList
                        {
                            document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["opsuploadhypothecationdocument_gid"].ToString()),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString(),
                            document_title = dr_datarow["document_title"].ToString()
                        });
                    }
                    values.opsHypoDocumentList = get_filename;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "success";

            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }

        }

        public void DaGetOPSCollateralDocDtl(string opsapplication2loan_gid, MdlOPSProductChargesView values)
        {
            try
            {
                msSQL = " select opsuploadcollateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                       " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                       " from ocs_mst_topsuploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                       " and b.user_gid = c.user_gid and opsapplication2loan_gid='" + opsapplication2loan_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<opsCollatralDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new opsCollatralDocumentList
                        {
                            document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["opsuploadcollateraldocument_gid"].ToString()),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString(),
                            document_title = dr_datarow["document_title"].ToString()
                        });
                    }
                    values.opsCollatralDocumentList = get_filename;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }

        }

        public void DaGetOPSPurposeofLoan(string opsapplication2loan_gid, MdlOPSProductChargesView values)
        {
            try
            {
                msSQL = " select enduse_purpose from ocs_mst_topsapplication2loan where opsapplication2loan_gid='" + opsapplication2loan_gid + "'";
                values.enduse_purpose = objdbconn.GetExecuteScalar(msSQL);
                values.status = true;
                values.message = "success";

            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }

        }

        public void DaGetOPSLoantoBuyerList(string opsapplication2loan_gid, MdlOPSProductChargesView values)
        {
            try
            {
                msSQL = "select opsapplication2buyer_gid,buyer_name,buyer_gid,buyer_limit,availed_limit,balance_limit,margin,bill_tenure " +
                   " from ocs_mst_topsapplication2buyer where opsapplication2loan_gid='" + opsapplication2loan_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopsbuyer_list = new List<opsbuyer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getopsbuyer_list.Add(new opsbuyer_list
                        {
                            opsapplication2buyer_gid = (dr_datarow["opsapplication2buyer_gid"].ToString()),
                            buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                            buyer_name = (dr_datarow["buyer_name"].ToString()),
                            buyer_limit = (dr_datarow["buyer_limit"].ToString()),
                            availed_limit = (dr_datarow["availed_limit"].ToString()),
                            balance_limit = (dr_datarow["balance_limit"].ToString()),
                            bill_tenure = (dr_datarow["bill_tenure"].ToString()),
                            margin = (dr_datarow["margin"].ToString())
                        });
                    }
                    values.opsbuyer_list = getopsbuyer_list;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetOPSGroupSummary(string opsapplication_gid, MdlOPSGroup values)
        {
            msSQL = "select a.opsgroup_gid,a.group_name,a.date_of_formation,a.group_status,a.group_type," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from ocs_mst_topsgroup a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where opsapplication_gid='" + opsapplication_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getopsgroup_list = new List<opsgroup_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getopsgroup_list.Add(new opsgroup_list
                    {
                        opsgroup_gid = (dr_datarow["opsgroup_gid"].ToString()),
                        group_name = (dr_datarow["group_name"].ToString()),
                        date_of_formation = (dr_datarow["date_of_formation"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        group_status = (dr_datarow["group_status"].ToString()),
                        group_type = (dr_datarow["group_type"].ToString()),
                    });
                }
            }
            values.opsgroup_list = getopsgroup_list;
            dt_datatable.Dispose();
        }

        public void DaGetOPSGrouptoMemberList(string group_gid, MdlOPSGroupMember values)
        {
            msSQL = "select a.opscontact_gid,a.pan_no,a.aadhar_no,concat(first_name, ' ',middle_name,' ',last_name) as individual_name,stakeholder_type" +
                    " from ocs_mst_topscontact a " +
                    " where group_gid='" + group_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getopsgroupmember_list = new List<opsgroupmember_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getopsgroupmember_list.Add(new opsgroupmember_list
                    {
                        opscontact_gid = (dr_datarow["opscontact_gid"].ToString()),
                        individual_name = (dr_datarow["individual_name"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        aadhar_no = (dr_datarow["aadhar_no"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                    });
                }
            }
            values.opsgroupmember_list = getopsgroupmember_list;
            dt_datatable.Dispose();
        }

        public void DaGetOPSGroupView(string opsgroup_gid, MdlOPSGroup values)
        {
            try
            {
                msSQL = " select group_name,date_of_formation,group_type,groupmember_count,groupurn_status,group_urn,group_status" +
                        " from ocs_mst_topsgroup where opsgroup_gid='" + opsgroup_gid + "'";
                
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.group_name = objODBCDatareader["group_name"].ToString();
                    values.date_of_formation = objODBCDatareader["date_of_formation"].ToString();
                    values.group_type = objODBCDatareader["group_type"].ToString();
                    values.groupmember_count = objODBCDatareader["groupmember_count"].ToString();
                    values.groupurn_status = objODBCDatareader["groupurn_status"].ToString();
                    values.group_urn = objODBCDatareader["group_urn"].ToString();
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

        public void DaOPSGroupAddressList(string opsgroup_gid, MdlOPSAddressDetails values)
        {
            msSQL = "  select opsgroup2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, landmark," +
                    " postal_code from ocs_mst_topsgroup2address where opsgroup_gid='" + opsgroup_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getopsaddress_list = new List<opsaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getopsaddress_list.Add(new opsaddress_list
                    {
                        opsgroup2address_gid = (dr_datarow["opsgroup2address_gid"].ToString()),
                        address_type = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString())
                    });
                }
                values.opsaddress_list = getopsaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaOPSGroupBankList(string opsgroup_gid, MdOPSBankDetails values)
        {
            msSQL = "  select opsgroup2bank_gid,ifsc_code,bank_accountno, accountholder_name, bank_name, bank_branch" +
                    " from ocs_mst_topsgroup2bank where opsgroup_gid='" + opsgroup_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getopsbank_list = new List<opsbank_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getopsbank_list.Add(new opsbank_list
                    {
                        opsgroup2bank_gid = (dr_datarow["opsgroup2bank_gid"].ToString()),
                        ifsc_code = (dr_datarow["ifsc_code"].ToString()),
                        bank_accountno = (dr_datarow["bank_accountno"].ToString()),
                        accountholder_name = (dr_datarow["accountholder_name"].ToString()),
                        bank_name = (dr_datarow["bank_name"].ToString()),
                        bank_branch = (dr_datarow["bank_branch"].ToString())
                    });
                }
                values.opsbank_list = getopsbank_list;
            }
            dt_datatable.Dispose();
        }

        public void DaOPSGroupDocumentList(string opsgroup_gid, OPSGroupDocument values)
        {
            msSQL = " select opsgroup2document_gid,document_name,document_title,document_path from ocs_mst_topsgroup2document " +
                                 " where opsgroup_gid='" + opsgroup_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getopsdocumentdtlList = new List<opsgroupdocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getopsdocumentdtlList.Add(new opsgroupdocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        opsgroup2document_gid = dt["opsgroup2document_gid"].ToString(),
                    });
                    values.opsgroupdocument_list = getopsdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetOPSIndividualView(string opscontact_gid, MdlOPSContact values)
        {
            try
            {
                msSQL = " select pan_no,aadhar_no,first_name,middle_name,last_name,individual_dob,age,gender_gid,gender_name," +
                        " educationalqualification_gid,educationalqualification_name,main_occupation,annual_income,monthly_income," +
                        " pep_status,date_format(pepverified_date,'%d-%m-%Y') as pepverified_date,maritalstatus_gid,maritalstatus_name,stakeholdertype_gid,stakeholder_type," +
                        " father_firstname,father_middlename,father_lastname,father_dob,father_age," +
                        " mother_firstname,mother_middlename,mother_lastname,mother_dob,mother_age," +
                        " spouse_firstname,spouse_middlename,spouse_lastname,spouse_dob,spouse_age," +
                        " ownershiptype_gid,ownershiptype_name,residencetype_gid,residencetype_name,currentresidence_years,branch_distance, contact_status," +
                        " propertyholder_gid, propertyholder_name, incometype_gid, incometype_name, previouscrop, prposedcrop,institution_gid,institution_name," +
                        " group_gid, group_name, profile, urn_status, urn, fathernominee_status, mothernominee_status, spousenominee_status, othernominee_status," +
                        " relationshiptype, nomineefirst_name, nominee_middlename, nominee_lastname, nominee_dob, nominee_age, totallandinacres, cultivatedland" +
                        " from ocs_mst_topscontact where opscontact_gid='" + opscontact_gid + "'";
                
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.pan_no = objODBCDatareader["pan_no"].ToString();
                    values.aadhar_no = objODBCDatareader["aadhar_no"].ToString();
                    values.first_name = objODBCDatareader["first_name"].ToString();
                    values.middle_name = objODBCDatareader["middle_name"].ToString();
                    values.last_name = objODBCDatareader["last_name"].ToString();
                    values.individual_dob = objODBCDatareader["individual_dob"].ToString();
                    values.age = objODBCDatareader["age"].ToString();
                    values.gender_gid = objODBCDatareader["gender_gid"].ToString();
                    values.gender_name = objODBCDatareader["gender_name"].ToString();
                    values.educationalqualification_gid = objODBCDatareader["educationalqualification_gid"].ToString();
                    values.educationalqualification_name = objODBCDatareader["educationalqualification_name"].ToString();
                    values.main_occupation = objODBCDatareader["main_occupation"].ToString();
                    values.annual_income = objODBCDatareader["annual_income"].ToString();
                    values.monthly_income = objODBCDatareader["monthly_income"].ToString();

                    values.pep_status = objODBCDatareader["pep_status"].ToString();
                    values.pepverified_date = objODBCDatareader["pepverified_date"].ToString();
                    values.maritalstatus_gid = objODBCDatareader["maritalstatus_gid"].ToString();
                    values.maritalstatus_name = objODBCDatareader["maritalstatus_name"].ToString();
                    values.stakeholdertype_gid = objODBCDatareader["stakeholdertype_gid"].ToString();
                    values.stakeholdertype_name = objODBCDatareader["stakeholder_type"].ToString();

                    values.father_firstname = objODBCDatareader["father_firstname"].ToString();
                    values.father_middlename = objODBCDatareader["father_middlename"].ToString();
                    values.father_lastname = objODBCDatareader["father_lastname"].ToString();
                    values.father_dob = objODBCDatareader["father_dob"].ToString();
                    values.father_age = objODBCDatareader["father_age"].ToString();

                    values.mother_firstname = objODBCDatareader["mother_firstname"].ToString();
                    values.mother_middlename = objODBCDatareader["mother_middlename"].ToString();
                    values.mother_lastname = objODBCDatareader["mother_lastname"].ToString();
                    values.mother_dob = objODBCDatareader["mother_dob"].ToString();
                    values.mother_age = objODBCDatareader["mother_age"].ToString();

                    values.spouse_firstname = objODBCDatareader["spouse_firstname"].ToString();
                    values.spouse_middlename = objODBCDatareader["spouse_middlename"].ToString();
                    values.spouse_lastname = objODBCDatareader["spouse_lastname"].ToString();
                    values.spouse_dob = objODBCDatareader["spouse_dob"].ToString();
                    values.spouse_age = objODBCDatareader["spouse_age"].ToString();

                    values.ownershiptype_gid = objODBCDatareader["ownershiptype_gid"].ToString();
                    values.ownershiptype_name = objODBCDatareader["ownershiptype_name"].ToString();
                    values.residencetype_gid = objODBCDatareader["residencetype_gid"].ToString();
                    values.residencetype_name = objODBCDatareader["residencetype_name"].ToString();
                    values.currentresidence_years = objODBCDatareader["currentresidence_years"].ToString();
                    values.branch_distance = objODBCDatareader["branch_distance"].ToString();
                    values.contact_status = objODBCDatareader["contact_status"].ToString();

                    values.propertyholder_gid = objODBCDatareader["propertyholder_gid"].ToString();
                    values.propertyholder_name = objODBCDatareader["propertyholder_name"].ToString();
                    values.incometype_gid = objODBCDatareader["incometype_gid"].ToString();
                    values.incometype_name = objODBCDatareader["incometype_name"].ToString();

                    values.group_gid = objODBCDatareader["group_gid"].ToString();
                    values.group_name = objODBCDatareader["group_name"].ToString();
                    values.profile = objODBCDatareader["profile"].ToString();
                    values.urn_status = objODBCDatareader["urn_status"].ToString();
                    values.urn = objODBCDatareader["urn"].ToString();
                    values.fathernominee_status = objODBCDatareader["fathernominee_status"].ToString();
                    values.mothernominee_status = objODBCDatareader["mothernominee_status"].ToString();
                    values.spousenominee_status = objODBCDatareader["spousenominee_status"].ToString();
                    values.othernominee_status = objODBCDatareader["othernominee_status"].ToString();
                    values.relationshiptype = objODBCDatareader["relationshiptype"].ToString();
                    values.nomineefirst_name = objODBCDatareader["nomineefirst_name"].ToString();
                    values.nominee_middlename = objODBCDatareader["nominee_middlename"].ToString();
                    values.nominee_lastname = objODBCDatareader["nominee_lastname"].ToString();
                    values.nominee_dob = objODBCDatareader["nominee_dob"].ToString();
                    values.nominee_age = objODBCDatareader["nominee_age"].ToString();
                    values.totallandinacres = objODBCDatareader["totallandinacres"].ToString();
                    values.cultivatedland = objODBCDatareader["cultivatedland"].ToString();
                    values.previouscrop = objODBCDatareader["previouscrop"].ToString();
                    values.prposedcrop = objODBCDatareader["prposedcrop"].ToString();
                    values.institution_gid = objODBCDatareader["institution_gid"].ToString();
                    values.institution_name = objODBCDatareader["institution_name"].ToString();
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

        public void DaGetOPSIndividualAddressList(string opscontact_gid, string employee_gid, MdlOPSContactAddress values)
        {
            msSQL = " select opscontact2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country," +
                    " postal_code from ocs_mst_topscontact2address where opscontact_gid='" + opscontact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getopscontactaddress_list = new List<opscontactaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getopscontactaddress_list.Add(new opscontactaddress_list
                    {
                        opscontact2address_gid = (dr_datarow["opscontact2address_gid"].ToString()),
                        addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString())
                    });
                }
                values.opscontactaddress_list = getopscontactaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetOPSIndividualProofList(string opscontact_gid, string employee_gid, MdlOPSContactIdProof values)
        {
            msSQL = "select opscontact2idproof_gid,idproof_name,idproof_no,document_name, document_path from ocs_mst_topscontact2idproof where " +
              " opscontact_gid='" + opscontact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getopscontactidproof_list = new List<opscontactidproof_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getopscontactidproof_list.Add(new opscontactidproof_list
                    {
                        opscontact2idproof_gid = (dr_datarow["opscontact2idproof_gid"].ToString()),
                        idproof_name = (dr_datarow["idproof_name"].ToString()),
                        idproof_no = (dr_datarow["idproof_no"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = (objcmnstorage.EncryptData(dr_datarow["document_path"].ToString())),
                    });

                    values.opscontactidproof_list = getopscontactidproof_list;
                }
                dt_datatable.Dispose();
            }

        }

        public void DaGetOPSIndividualDocList(string opscontact_gid, MdlOPSContactIdProof values)
        {
            msSQL = " select opscontact2document_gid,document_name,document_title,document_path from ocs_mst_topscontact2document " +
                                 " where opscontact_gid='" + opscontact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getopsdocumentdtlList = new List<uploadopsindividualdoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getopsdocumentdtlList.Add(new uploadopsindividualdoc_list
                    {
                        document_name = dt["document_name"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_path = objcmnstorage.EncryptData(dt["document_path"].ToString()),
                        opscontact2document_gid = dt["opscontact2document_gid"].ToString(),
                    });
                    values.uploadopsindividualdoc_list = getopsdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetOPSPrimaryAndOtherMobileNumber(string opscontact_gid, MdlOPSContactMobileNumber values)
        {
            try
            {
                msSQL = "select mobile_no from ocs_mst_topscontact2mobileno where opscontact_gid='" + opscontact_gid + "' and primary_status = 'Yes'";
                values.primaryindividual_mobileno = objdbconn.GetExecuteScalar(msSQL);
                
                msSQL = " select opscontact_gid, mobile_no, whatsapp_no " +
                        " from ocs_mst_topscontact2mobileno " +
                        " where opscontact_gid = '" + opscontact_gid + "' and primary_status = 'No' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopsmobileno_list = new List<opsindividualmobileno_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getopsmobileno_list.Add(new opsindividualmobileno_list
                        {
                            opscontact_gid = (dr_datarow["opscontact_gid"].ToString()),
                            mobile_no = (dr_datarow["mobile_no"].ToString()),
                            whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),

                        });
                    }
                    values.opsindividualmobileno_list = getopsmobileno_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetOPSPrimaryAndOtherEmail(string opscontact_gid, MdlOPSContactEmail values)
        {
            try
            {
                msSQL = "select email_address from ocs_mst_topscontact2email where opscontact_gid='" + opscontact_gid + "' and primary_status = 'Yes'";
                values.primaryindividual_email = objdbconn.GetExecuteScalar(msSQL);
                
                msSQL = " select email_address from ocs_mst_topscontact2email " +
                        " where opscontact_gid = '" + opscontact_gid + "' and primary_status = 'No' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getopsemail_list = new List<opsindividualemail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getopsemail_list.Add(new opsindividualemail_list
                        {
                            email_address = (dr_datarow["email_address"].ToString()),

                        });
                    }
                    values.opsindividualemail_list = getopsemail_list;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetOPSIndividualBureauDtls(string opscontact_gid, MdlOPSContactBureau values)
        {
            try
            {
                msSQL = " select bureauname_gid,bureauname_name, bureau_score,date_format(bureauscore_date,'%d-%m-%Y') as bureauscore_date, observations, bureau_response," +
                        "cicdocument_name, cicdocument_path from ocs_mst_topscontact where opscontact_gid='" + opscontact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.bureauname_gid = objODBCDatareader["bureauname_gid"].ToString();
                    values.bureauname_name = objODBCDatareader["bureauname_name"].ToString();
                    values.bureau_score = objODBCDatareader["bureau_score"].ToString();
                    values.observations = objODBCDatareader["observations"].ToString();
                    values.bureau_response = objODBCDatareader["bureau_response"].ToString();
                    values.bureauscore_date = objODBCDatareader["bureauscore_date"].ToString();
                    
                }
                objODBCDatareader.Close();

                msSQL = "select cicdocument_name,cicdocument_path from ocs_mst_topsindividual2cicdocumentupload " +
                       " where opscontact_gid='" + opscontact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.cicdocument_name = objODBCDatareader["cicdocument_name"].ToString();
                    values.cicdocument_path = objcmnstorage.EncryptData(objODBCDatareader["cicdocument_path"].ToString());
                }

                objODBCDatareader.Close();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
    }
}