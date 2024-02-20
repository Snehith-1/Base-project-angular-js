using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.Net;
using System.IO;
using ems.utilities.Functions;
using ems.mastersamagro.Models;
using System.Configuration;
using System.Drawing;
using System.Net.Mail;
using ems.storage.Functions;

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will provide access to view datas from various stages in Application creation (General, Company, Individual, Overall limit, Product, charges, trade, Bureau & Done)
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Logapriya, Abilash.A, Premchander.K </remarks>
    public class DaAgrMstApplicationView
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, lsapplication_gid, lsapplication_gidcontact;
        int mnResult;
        string lsurn = string.Empty;
        string institution_urn = string.Empty;
        string individual_urn = string.Empty;
        string group_urn = string.Empty;
        private object objMdlPANAbsenceReason;

        public void DaGetApplicationBasicView(string application_gid, MdlMstApplicationView values)
        {
            try
            {
                msSQL = " select a.application_gid, a.application_no,contract_id,date_format(a.validityfrom_date,'%d-%m-%Y') as validityfrom_date," +
                        " date_format(a.validityto_date,'%d-%m-%Y') as validityto_date,customer_urn, customerref_name as customer_name, vertical_name, verticaltaggs_name,a.cccompleted_flag, " +
                        " constitution_name, businessunit_name, vernacular_language, sa_status, sa_id, sa_name, a.social_capital, a.trade_capital, a.buyeronboard_gid," +
                        " designation_type, landline_no, concat_ws(' ', contactpersonfirst_name, contactpersonmiddle_name, contactpersonlast_name) as contactperson_name," +
                        "  a.region,a.buyeragreement_id," +
                        " date_format(a.headapproval_date,'%d-%m-%Y %h:%i %p') as businessapproved_date, date_format(a.cccompleted_date,'%d-%m-%Y %h:%i %p') as ccapproved_date," +
                        " momapproval_flag,a.approval_status,creditgroup_name,date_format(f.approved_date,'%d-%m-%Y') as approved_date,date_format(g.approved_date,'%d-%m-%Y') as lblbusinessapproved_date, " +
                        " docchecklist_makerflag,docchecklist_checkerflag,docchecklist_approvalflag,product_gid,product_name,h.ccgroup_name, " +
                        " sector_name,category_name,variety_gid,variety_name,botanical_name,alternative_name,program_gid,program_name, " +
                         " CASE WHEN(onboarding_status = 'Proposal')  THEN 'Credit' " +
                        "  WHEN(onboarding_status = 'Direct')  THEN 'Advance'" +
                         " ELSE '' END as onboarding_status," +
                        " case when e.urn = '' then d.urn else e.urn end as customer_urnno," +
                        " case when a.docchecklist_approvalflag = 'Y' then 'After Approval' else 'false' end as document_status,shortclosing_reason,amendment_remarks  " +
                        " from agr_mst_tapplication a" +
                        " left join agr_mst_tinstitution d on d.application_gid = a.application_gid " +
                        " left join agr_trn_tAppcreditapproval f on f.application_gid = a.application_gid  and hierary_level = '3' " +
                        " left join agr_trn_tapplicationapproval g on g.application_gid = a.application_gid  and g.hierary_level = '5' " +
                        " left join agr_mst_tcontact e on e.application_gid = a.application_gid " +
                        " left join agr_mst_tccschedulemeeting h on h.application_gid = a.application_gid " +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.application_no = objODBCDatareader["application_no"].ToString();
                    values.contract_id = objODBCDatareader["contract_id"].ToString();
                    values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                    values.validityfrom_date = objODBCDatareader["validityfrom_date"].ToString();
                    values.validityto_date = objODBCDatareader["validityto_date"].ToString();
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
                    values.approved_date = objODBCDatareader["approved_date"].ToString();
                    values.social_capital = objODBCDatareader["social_capital"].ToString();
                    values.trade_capital = objODBCDatareader["trade_capital"].ToString();
                    values.momapproval_flag = objODBCDatareader["momapproval_flag"].ToString();
                    values.approval_status = objODBCDatareader["approval_status"].ToString();
                    values.creditgroup_name = objODBCDatareader["creditgroup_name"].ToString();
                    values.businessapproved_date = objODBCDatareader["businessapproved_date"].ToString();
                    values.ccapproved_date = objODBCDatareader["ccapproved_date"].ToString();
                    values.region = objODBCDatareader["region"].ToString();
                    values.docchecklist_makerflag = objODBCDatareader["docchecklist_makerflag"].ToString();
                    values.docchecklist_checkerflag = objODBCDatareader["docchecklist_checkerflag"].ToString();
                    values.docchecklist_approvalflag = objODBCDatareader["docchecklist_approvalflag"].ToString();
                    values.product_gid = objODBCDatareader["product_gid"].ToString();
                    values.product_name = objODBCDatareader["product_name"].ToString();
                    values.ccgroup_name = objODBCDatareader["ccgroup_name"].ToString();
                    values.sector_name = objODBCDatareader["sector_name"].ToString();
                    values.category_name = objODBCDatareader["category_name"].ToString();                   
                    values.variety_gid = objODBCDatareader["variety_gid"].ToString();
                    values.variety_name = objODBCDatareader["variety_name"].ToString();
                    values.botanical_name = objODBCDatareader["botanical_name"].ToString();
                    values.alternative_name = objODBCDatareader["alternative_name"].ToString();
                    values.program_gid = objODBCDatareader["program_gid"].ToString();
                    values.program_name = objODBCDatareader["program_name"].ToString();
                    values.customer_urnno = objODBCDatareader["customer_urnno"].ToString();
                    values.cccompleted_flag = objODBCDatareader["cccompleted_flag"].ToString();
                    values.buyeronboard_gid = objODBCDatareader["buyeronboard_gid"].ToString();
                    values.onboarding_status = objODBCDatareader["onboarding_status"].ToString();
                    values.document_status = objODBCDatareader["document_status"].ToString();
                    values.shortclosing_reason = objODBCDatareader["shortclosing_reason"].ToString();
                    values.amendment_remarks = objODBCDatareader["amendment_remarks"].ToString();
                    values.lblbusinessapproved_date = objODBCDatareader["lblbusinessapproved_date"].ToString();
                    values.buyeragreement_id = objODBCDatareader["buyeragreement_id"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();

                msSQL = " select application_gid from agr_mst_tinstitution " +
                        " where application_gid='" + application_gid + "' and (stakeholder_type='Applicant' or stakeholder_type='Borrower')";
                lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsapplication_gid != "")
                {
                    values.borrower_flag = "Y";
                    values.borrower_type = "Institution";
                }
                else
                {
                    msSQL = " select application_gid from agr_mst_tcontact " +
                            " where application_gid='" + application_gid + "' and (stakeholder_type='Applicant' or stakeholder_type='Borrower')";
                    lsapplication_gidcontact = objdbconn.GetExecuteScalar(msSQL);
                    if (lsapplication_gidcontact != "")
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

                //objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetMobileMailDetailsView(string application_gid, MdlMstApplicationView values)
        {
            msSQL = "select mobile_no from agr_mst_tapplication2contactno where application_gid='" + application_gid + "' and primary_mobileno = 'Yes'";
            values.primary_mobileno = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select email_address from agr_mst_tapplication2email where application_gid='" + application_gid + "' and primary_emailaddress = 'Yes'";
            values.primary_email = objdbconn.GetExecuteScalar(msSQL);
            try
            {
                msSQL = " select application2contact_gid, application_gid, mobile_no, whatsapp_mobileno " +
                        " from agr_mst_tapplication2contactno " +
                        " where application_gid = '" + application_gid + "' and primary_mobileno = 'No' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplication_list = new List<mobilenumber_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplication_list.Add(new mobilenumber_list
                        {
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            mobile_no = (dr_datarow["mobile_no"].ToString()),
                            whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString()),

                        });
                    }
                    values.mobilenumber_list = getapplication_list;
                }
                dt_datatable.Dispose();
                values.status = true;

                msSQL = " select application2email_gid, application_gid, email_address " +
                        " from agr_mst_tapplication2email " +
                        " where application_gid = '" + application_gid + "' and primary_emailaddress = 'No' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmail_list = new List<mail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmail_list.Add(new mail_list
                        {
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            application2email_gid = (dr_datarow["application2email_gid"].ToString()),
                            email_address = (dr_datarow["email_address"].ToString()),

                        });
                    }
                    values.mail_list = getmail_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetGeneticDetailsView(string application_gid, MdlMstApplicationView values)
        {

            try
            {
                msSQL = " select geneticcode_name, genetic_status, genetic_remarks, application_gid, geneticcode_gid " +
                        " from agr_mst_tapplication2geneticcode " +
                        " where application_gid = '" + application_gid + "' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getgenetic_list = new List<geneticdetails_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getgenetic_list.Add(new geneticdetails_list
                        {
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            geneticcode_name = (dr_datarow["geneticcode_name"].ToString()),
                            genetic_status = (dr_datarow["genetic_status"].ToString()),
                            genetic_remarks = (dr_datarow["genetic_remarks"].ToString()),

                        });
                    }
                    values.geneticdetails_list = getgenetic_list;
                }
                dt_datatable.Dispose();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetBorrowerInstitutionView(string application_gid, MdlMstInstitutionDtlView values)
        {
            try
            {
                msSQL = " select institution_gid, application_gid, company_name, companypan_no, date_format(date_incorporation, '%d-%m-%Y') as date_incorporation, " +
                        " year_business, month_business, cin_no, official_telephoneno, officialemail_address, companytype_name, escrow, " +
                        " lastyear_turnover, date_format(start_date, '%d-%m-%Y') as start_date, " +
                        " date_format(end_date, '%d-%m-%Y') as end_date, assessmentagency_name, " +
                        " assessmentagencyrating_name, date_format(ratingas_on, '%d-%m-%Y') as ratingas_on, " +
                        " amlcategory_name, businesscategory_name, urn_status, urn, msme_registration,lei_renewaldate,kin,lglentity_id, " +
                        " contactperson_firstname,contactperson_middlename,contactperson_lastname,designation, " +
                        " tan_number,incometax_returnsstatus,revenue,stakeholder_type,profit,fixed_assets,sundrydebt_adv, " +
                        " date_format(businessstart_date, '%d-%m-%Y') as businessstart_date from agr_mst_tinstitution " +
                        " where application_gid = '" + application_gid + "' and (stakeholder_type = 'Applicant' or stakeholder_type='Borrower')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.institution_gid = objODBCDatareader["institution_gid"].ToString();
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
                    values.urn_status = objODBCDatareader["urn_status"].ToString();
                    values.urn = objODBCDatareader["urn"].ToString();
                    values.contactperson_firstname = objODBCDatareader["contactperson_firstname"].ToString();
                    values.contactperson_middlename = objODBCDatareader["contactperson_middlename"].ToString();
                    values.contactperson_lastname = objODBCDatareader["contactperson_lastname"].ToString();
                    values.designation = objODBCDatareader["designation"].ToString();
                    values.businessstart_date = objODBCDatareader["businessstart_date"].ToString();
                    values.tan_number = objODBCDatareader["tan_number"].ToString();
                    values.revenue = objODBCDatareader["revenue"].ToString();
                    values.incometax_returnsstatus = objODBCDatareader["incometax_returnsstatus"].ToString();
                    values.profit = objODBCDatareader["profit"].ToString();
                    values.fixed_assets = objODBCDatareader["fixed_assets"].ToString();
                    values.sundrydebt_adv = objODBCDatareader["sundrydebt_adv"].ToString();
                    values.companytype_name = objODBCDatareader["companytype_name"].ToString();
                    values.stakeholder_type = objODBCDatareader["stakeholder_type"].ToString();
                    if (objODBCDatareader["lei_renewaldate"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.editlei_renewaldate = Convert.ToDateTime(objODBCDatareader["lei_renewaldate"]).ToString("dd-MM-yyyy");
                    }
                    values.msme_registration = objODBCDatareader["msme_registration"].ToString();
                    values.lglentity_id = objODBCDatareader["lglentity_id"].ToString();
                    values.kin = objODBCDatareader["kin"].ToString();
                }

                objODBCDatareader.Close();

                msSQL = "select institution2branch_gid,gst_state,gst_no, gst_registered,headoffice_status from agr_mst_tinstitution2branch where institution_gid='" + values.institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstgst_list = new List<mstgst_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstgst_list.Add(new mstgst_list
                        {
                            institution2branch_gid = (dr_datarow["institution2branch_gid"].ToString()),
                            gst_state = (dr_datarow["gst_state"].ToString()),
                            gst_no = (dr_datarow["gst_no"].ToString()),
                            gst_registered = (dr_datarow["gst_registered"].ToString()),
                            headoffice_status = (dr_datarow["headoffice_status"].ToString())
                        });
                    }
                    values.mstgst_list = getmstgst_list;
                }
                dt_datatable.Dispose();

                msSQL = "  select institution2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, landmark," +
                    " postal_code, city, latitude, longitude  from agr_mst_tinstitution2address where institution_gid='" + values.institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstaddress_list = new List<mstaddress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstaddress_list.Add(new mstaddress_list
                        {
                            institution2address_gid = (dr_datarow["institution2address_gid"].ToString()),
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
                            landmark = (dr_datarow["landmark"].ToString()),
                            latitude = (dr_datarow["latitude"].ToString()),
                            longitude = (dr_datarow["longitude"].ToString())
                        });
                    }
                    values.mstaddress_list = getmstaddress_list;
                }
                dt_datatable.Dispose();


                msSQL = "select mobile_no from agr_mst_tinstitution2mobileno where institution_gid='" + values.institution_gid + "' and primary_status = 'Yes'";
                values.primaryinstitution_mobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tinstitution2email where institution_gid='" + values.institution_gid + "' and primary_status = 'Yes'";
                values.primaryinstitution_email = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select institution_gid, mobile_no, whatsapp_no " +
                        " from agr_mst_tinstitution2mobileno " +
                        " where institution_gid = '" + values.institution_gid + "' and primary_status = 'No' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplication_list = new List<instituionmobilenumber_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplication_list.Add(new instituionmobilenumber_list
                        {
                            institution_gid = (dr_datarow["institution_gid"].ToString()),
                            mobile_no = (dr_datarow["mobile_no"].ToString()),
                            whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),

                        });
                    }
                    values.instituionmobilenumber_list = getapplication_list;
                }
                dt_datatable.Dispose();
                values.status = true;

                msSQL = " select institution_gid, email_address " +
                        " from agr_mst_tinstitution2email " +
                        " where institution_gid = '" + values.institution_gid + "' and primary_status = 'No' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmail_list = new List<instituionmail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmail_list.Add(new instituionmail_list
                        {
                            email_address = (dr_datarow["email_address"].ToString()),

                        });
                    }
                    values.instituionmail_list = getmail_list;
                }
                dt_datatable.Dispose();


                msSQL = " select institution2form60documentupload_gid,form60document_name,form60document_path from agr_mst_tinstitution2form60documentupload " +
                               " where institution_gid ='" + values.institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocumentdtlList = new List<institutionform60_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdocumentdtlList.Add(new institutionform60_list
                        {
                            document_name = dt["form60document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData((dt["form60document_path"].ToString())),
                            institution2form60documentupload_gid = dt["institution2form60documentupload_gid"].ToString()
                        });
                        values.institutionform60_list = getdocumentdtlList;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select institution2documentupload_gid,institution_gid,document_name,document_path,document_title,document_id from agr_mst_tinstitution2documentupload " +
                        " where institution_gid='" + values.institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocumentList = new List<institutiondoc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdocumentList.Add(new institutiondoc_list
                        {
                            document_name = dt["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                            institution2documentupload_gid = dt["institution2documentupload_gid"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            document_id = dt["document_id"].ToString(),
                        });
                        values.institutiondoc_list = getdocumentList;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select institution2licensedtl_gid,licensetype_gid,licensetype_name,license_no,date_format(issue_date,'%d-%m-%Y') as issue_date," +
                        " date_format(expiry_date,'%d-%m-%Y') as expiry_date from agr_mst_tinstitution2licensedtl" +
                        " where institution_gid='" + values.institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstlicense_list = new List<mstlicense_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstlicense_list.Add(new mstlicense_list
                        {
                            institution2licensedtl_gid = (dr_datarow["institution2licensedtl_gid"].ToString()),
                            licensetype_gid = (dr_datarow["licensetype_gid"].ToString()),
                            licensetype_name = (dr_datarow["licensetype_name"].ToString()),
                            license_number = (dr_datarow["license_no"].ToString()),
                            licenseissue_date = (dr_datarow["issue_date"].ToString()),
                            licenseexpiry_date = (dr_datarow["expiry_date"].ToString())
                        });
                    }
                    values.mstlicense_list = getmstlicense_list;
                }
                dt_datatable.Dispose();


                msSQL = " select bureauname_gid,bureauname_name, bureau_score,date_format(bureauscore_date,'%d-%m-%Y') as bureauscore_date, observations, bureau_response " +
                       " from agr_mst_tinstitution where institution_gid='" + values.institution_gid + "'";
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
                msSQL = "select institution2cicdocumentupload_gid,institution2bureau_gid,cicdocument_name,cicdocument_path from agr_mst_tinstitution2cicdocumentupload" +
                        " where institution_gid='" + values.institution_gid + "'";
                //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                //if (objODBCDatareader.HasRows == true)
                //{
                //    values.cicdocument_name = objODBCDatareader["cicdocument_name"].ToString();
                //    values.cicdocument_path = (objODBCDatareader["cicdocument_path"].ToString());
                //}
                //objODBCDatareader.Close();
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcicdocumentList = new List<Institutioncicdoc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcicdocumentList.Add(new Institutioncicdoc_list
                        {
                            cicdocument_name = dt["cicdocument_name"].ToString(),
                            cicdocument_path = objcmnstorage.EncryptData((dt["cicdocument_path"].ToString())),
                            institution2cicdocumentupload_gid = (dt["institution2cicdocumentupload_gid"].ToString()),
                            institution2bureau_gid = (dt["institution2bureau_gid"].ToString())

                        });
                        values.Institutioncicdoc_list = getcicdocumentList;
                    }
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetVisitReportList(string application_gid, string statusupdated_by, MdlMstVisitPersonView values)
        {
            msSQL = "select visitreport_id, concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by, date_format(a.created_date,'%d-%m-%Y') as created_date, " +
                " date_format(a.applicationvisit_date,'%d-%m-%Y') as applicationvisit_date,a.applicationvisit_gid " +
                " from agr_mst_tapplicationvisitreport a " +
                " left join hrm_mst_temployee d on d.employee_gid = a.created_by" +
                " left join adm_mst_tuser e on d.user_gid = e.user_gid" +
                " where application_gid='" + application_gid + "' and a.statusupdated_by='" + statusupdated_by + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getVisitReportList = new List<VisitReport_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getVisitReportList.Add(new VisitReport_List
                    {
                        visitreport_gid = (dr_datarow["applicationvisit_gid"].ToString()),
                        visitreport_date = (dr_datarow["applicationvisit_date"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        visitreport_id = (dr_datarow["visitreport_id"].ToString()),
                    });
                }
                values.VisitReport_List = getVisitReportList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetVisitReportDtls(string visitreport_gid, MdlMstVisitPersonView values)
        {
            try
            {
                msSQL = " select visitreport_id, date_format(a.applicationvisit_date,'%d-%m-%Y') as applicationvisit_date, a.clientkmp_activities, a.promoter_background, " +
               " a.overall_observations, a.inspectingofficial_recommenation, a.trading_relationship, summary, GROUP_CONCAT(distinct(b.inspectingofficials_name) SEPARATOR ', ') as inspectingofficials_name, " +
               " GROUP_CONCAT(distinct(c.visitdone_name) SEPARATOR ', ') as visitdone_name " +
               " from agr_mst_tapplicationvisitreport a " +
               " left join agr_mst_tapplicationvisit2inspectingofficial b on a.applicationvisit_gid = b.applicationvisit_gid" +
               " left join agr_mst_tapplicationvisit2visitdone c on c.applicationvisit_gid = a.applicationvisit_gid " +
               " where a.applicationvisit_gid='" + visitreport_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.applicationvisit_date = objODBCDatareader["applicationvisit_date"].ToString();
                    values.inspectingofficials_name = objODBCDatareader["inspectingofficials_name"].ToString();
                    values.visitdone_name = objODBCDatareader["visitdone_name"].ToString();
                    values.clientkmp_activities = objODBCDatareader["clientkmp_activities"].ToString();
                    values.promoter_background = objODBCDatareader["promoter_background"].ToString();
                    values.overall_observations = objODBCDatareader["overall_observations"].ToString();
                    values.inspectingofficial_recommenation = objODBCDatareader["inspectingofficial_recommenation"].ToString();
                    values.trading_relationship = objODBCDatareader["trading_relationship"].ToString();
                    values.summary = objODBCDatareader["summary"].ToString();
                    values.visitreport_id = objODBCDatareader["visitreport_id"].ToString();


                }
                objODBCDatareader.Close();



                msSQL = "select applicationvisit2person_gid,clientrepresentative_name,clientrepresentative_designationname,personal_mail,office_mail from agr_mst_tapplicationvisit2person where " +
                        " applicationvisit_gid='" + visitreport_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstVisitpersondtl_list = new List<mstVisitpersondtl_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstVisitpersondtl_list.Add(new mstVisitpersondtl_list
                        {
                            applicationvisit2person_gid = (dr_datarow["applicationvisit2person_gid"].ToString()),
                            clientrepresentative_name = (dr_datarow["clientrepresentative_name"].ToString()),
                            clientrepresentative_designationname = (dr_datarow["clientrepresentative_designationname"].ToString()),
                            clientrepresentative_personalmail = (dr_datarow["personal_mail"].ToString()),
                            clientrepresentative_officemail = (dr_datarow["office_mail"].ToString())
                        });
                    }
                    values.mstVisitpersondtl_list = getmstVisitpersondtl_list;
                }
                dt_datatable.Dispose();




                msSQL = "select applicationvisit2address_gid,addresstype_gid,addresstype_name,primary_status,address_line1,address_line2,landmark,postal_code,city,taluk,district,state_gid,state_name,country from agr_mst_tapplicationvisit2address where " +
                        " applicationvisit_gid='" + visitreport_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstVisitpersonaddress_list = new List<mstVisitpersonaddress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstVisitpersonaddress_list.Add(new mstVisitpersonaddress_list
                        {
                            applicationvisit2address_gid = (dr_datarow["applicationvisit2address_gid"].ToString()),
                            addresstype_gid = (dr_datarow["addresstype_gid"].ToString()),
                            addresstype_name = (dr_datarow["addresstype_name"].ToString()),
                            primary_status = (dr_datarow["primary_status"].ToString()),
                            address_line1 = (dr_datarow["address_line1"].ToString()),
                            address_line2 = (dr_datarow["address_line2"].ToString()),
                            landmark = (dr_datarow["landmark"].ToString()),
                            postal_code = (dr_datarow["postal_code"].ToString()),
                            city = (dr_datarow["city"].ToString()),
                            taluk = (dr_datarow["taluk"].ToString()),
                            district = (dr_datarow["district"].ToString()),
                            state_name = (dr_datarow["state_name"].ToString()),
                            country = (dr_datarow["country"].ToString()),
                        });
                    }
                    values.mstVisitpersonaddress_list = getmstVisitpersonaddress_list;
                }
                dt_datatable.Dispose();




                msSQL = "select applicationvisit2document_gid,document_name,document_path,file_name, date_format(created_date,'%d-%m-%Y') as created_date from agr_mst_tapplicationvisit2document where " +
                        " applicationvisit_gid='" + visitreport_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getUploadDocumentList = new List<UploadDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getUploadDocumentList.Add(new UploadDocumentList
                        {
                            applicationvisit2document_gid = (dr_datarow["applicationvisit2document_gid"].ToString()),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                            filename = (dr_datarow["file_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString())
                        });
                    }
                    values.UploadDocumentList = getUploadDocumentList;
                }
                dt_datatable.Dispose();




                msSQL = "select applicationvisit2photo_gid,visitphoto_name,visitphoto_path,file_name,date_format(created_date,'%d-%m-%Y') as created_date from agr_mst_tapplicationvisit2photo where " +
                        " applicationvisit_gid='" + visitreport_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getUploadphotoList = new List<UploadphotoList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getUploadphotoList.Add(new UploadphotoList
                        {
                            applicationvisit2photo_gid = (dr_datarow["applicationvisit2photo_gid"].ToString()),
                            photo_name = (dr_datarow["visitphoto_name"].ToString()),
                            document_path = objcmnstorage.EncryptData((dr_datarow["visitphoto_path"].ToString())),
                            filename = (dr_datarow["file_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString())
                        });
                    }
                    values.UploadphotoList = getUploadphotoList;
                }
                dt_datatable.Dispose();



            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }



        }

        public void DaGetVisitContactList(string employee_gid, string applicationvisit2person_gid, mstVisitpersondtl_list values)
        {
            msSQL = "select applicationvisitperson2contact_gid,mobile_no,primary_status,whatsapp_mobileno from agr_mst_tapplicationvisitperson2contactno where " +
              " applicationvisit2person_gid='" + applicationvisit2person_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstVisitpersoncontact_list = new List<mstVisitpersoncontact_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstVisitpersoncontact_list.Add(new mstVisitpersoncontact_list
                    {
                        applicationvisitperson2contact_gid = (dr_datarow["applicationvisitperson2contact_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString())
                    });
                }
                values.mstVisitpersoncontact_list = getmstVisitpersoncontact_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        // Grading Tool

        public void DaGetGradingToolDtls(string application_gid, string statusupdated_by, MdlMstGradeToolView values)
        {
            try
            {
                msSQL = " select fpo_acscore, date_format(dateofsurvey, '%d-%m-%Y') as dateofsurvey, overallfporating, overallfpograde, application2gradingtool_gid," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, date_format(a.created_date,'%d-%m-%Y') as created_date  " +
                        " from agr_mst_tapplication2gradingtool a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                        " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                        " where application_gid='" + application_gid + "' and a.statusupdated_by='" + statusupdated_by + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstgradetoolsummary_list = new List<mstgradetoolsummary_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstgradetoolsummary_list.Add(new mstgradetoolsummary_list
                        {
                            fpo_acscore = (dr_datarow["fpo_acscore"].ToString()),
                            dateofsurvey = (dr_datarow["dateofsurvey"].ToString()),
                            overallfporating = (dr_datarow["overallfporating"].ToString()),
                            overallfpograde = (dr_datarow["overallfpograde"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            application2gradingtool_gid = (dr_datarow["application2gradingtool_gid"].ToString()),

                        });
                    }
                    values.mstgradetoolsummary_list = getmstgradetoolsummary_list;
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

        public void DaGetBorrowerIndividualView(string application_gid, MdlMstIndividualDtlView values)
        {
            try
            {

                msSQL = " select contact_gid, application_gid, concat_ws(' ', first_name, last_name, middle_name) as individual_name, " +
                        " pan_no, aadhar_no, individual_dob," +
                        " age, gender_name, designation_name, main_occupation, pep_status, date_format(pepverified_date, '%d-%m-%Y') as pepverified_date, " +
                        " maritalstatus_name, concat_ws(' ', father_firstname, father_middlename, father_lastname) as father_name, " +
                        " father_dob, father_age, " +
                        " concat_ws(' ', mother_firstname, mother_middlename, mother_lastname) as mother_name, " +
                        " mother_dob, mother_age, " +
                        " concat_ws(' ', spouse_firstname, spouse_middlename, spouse_lastname) as spouse_name, " +
                        " spouse_dob, spouse_age, educationalqualification_name, " +
                        " annual_income, monthly_income, incometype_name as user_type, ownershiptype_name, propertyholder_name, residencetype_name, " +
                        " currentresidence_years, branch_distance, bureauname_name, bureau_score, observations, " +
                        " date_format(bureauscore_date, '%d-%m-%Y') as bureauscore_date, bureau_response,pan_status, " +
                        " group_name, profile, urn_status, urn, fathernominee_status, mothernominee_status, spousenominee_status, othernominee_status,institution_name," +
                        " relationshiptype, nomineefirst_name, nominee_middlename, nominee_lastname, nominee_dob, nominee_age, totallandinacres, cultivatedland, previouscrop, prposedcrop" +
                        " from agr_mst_tcontact " +
                        " where application_gid = '" + application_gid + "' and (stakeholder_type = 'Applicant'  or stakeholder_type='Borrower')";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.contact_gid = objODBCDatareader["contact_gid"].ToString();
                    values.individual_name = objODBCDatareader["individual_name"].ToString();
                    values.pan_no = objODBCDatareader["pan_no"].ToString();
                    values.aadhar_no = objODBCDatareader["aadhar_no"].ToString();
                    values.individual_dob = objODBCDatareader["individual_dob"].ToString();
                    values.age = objODBCDatareader["age"].ToString();
                    values.gender_name = objODBCDatareader["gender_name"].ToString();
                    values.designation_name = objODBCDatareader["designation_name"].ToString();
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
                    values.pan_status = objODBCDatareader["pan_status"].ToString();
                }

                objODBCDatareader.Close();

                msSQL = " SELECT panabsencereason" +
                  " from agr_mst_tcontact2panabsencereason where contact_gid='" + values.contact_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcontactpanabsencereasons_list = new List<contactpanabsencereasons_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    values.contactpanabsencereasons_list = dt_datatable.AsEnumerable().Select(row =>
                      new contactpanabsencereasons_list
                      {
                          panabsencereason = row["panabsencereason"].ToString(),
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();

                msSQL = " select contact2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country," +
                   " postal_code, landmark, city, latitude, longitude from agr_mst_tcontact2address where contact_gid='" + values.contact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcontactaddress_list = new List<contactaddress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcontactaddress_list.Add(new contactaddress_list
                        {
                            contact2address_gid = (dr_datarow["contact2address_gid"].ToString()),
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
                            latitude = (dr_datarow["latitude"].ToString()),
                            longitude = (dr_datarow["longitude"].ToString()),
                        });
                    }
                    values.contactaddress_list = getcontactaddress_list;
                }
                dt_datatable.Dispose();


                msSQL = "select mobile_no from agr_mst_tcontact2mobileno where contact_gid='" + values.contact_gid + "' and primary_status = 'Yes'";
                values.primaryindividual_mobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tcontact2email where contact_gid='" + values.contact_gid + "' and primary_status = 'Yes'";
                values.primaryindividual_email = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address,contact2email_gid,primary_status from agr_mst_tcontact2email where " +
                         " contact_gid='" + values.contact_gid + "' and primary_status = 'No'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcontactemail_list = new List<contactemail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcontactemail_list.Add(new contactemail_list
                        {
                            contact2email_gid = (dr_datarow["contact2email_gid"].ToString()),
                            email_address = (dr_datarow["email_address"].ToString()),
                            primary_status = (dr_datarow["primary_status"].ToString()),
                        });
                    }
                }
                values.contactemail_list = getcontactemail_list;
                dt_datatable.Dispose();

                msSQL = "select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from agr_mst_tcontact2mobileno where " +
                        " contact_gid='" + values.contact_gid + "' and primary_status = 'No'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcontactmobileno_list = new List<contactmobileno_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcontactmobileno_list.Add(new contactmobileno_list
                        {
                            contact2mobileno_gid = (dr_datarow["contact2mobileno_gid"].ToString()),
                            mobile_no = (dr_datarow["mobile_no"].ToString()),
                            primary_status = (dr_datarow["primary_status"].ToString()),
                            whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),
                        });
                    }
                }
                values.contactmobileno_list = getcontactmobileno_list;
                dt_datatable.Dispose();

                msSQL = "select contact2idproof_gid,idproof_name,idproof_no,document_name, document_path from agr_mst_tcontact2idproof where " +
                        " contact_gid='" + values.contact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcontactidproof_list = new List<contactidproof_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcontactidproof_list.Add(new contactidproof_list
                        {
                            contact2idproof_gid = (dr_datarow["contact2idproof_gid"].ToString()),
                            idproof_name = (dr_datarow["idproof_name"].ToString()),
                            idproof_no = (dr_datarow["idproof_no"].ToString()),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        });

                        values.contactidproof_list = getcontactidproof_list;
                    }
                    dt_datatable.Dispose();
                }

                msSQL = " select document_title,document_name,document_path from agr_mst_tcontact2document " +
                                " where contact_gid='" + values.contact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocumentdtlList = new List<uploadindividualdoc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdocumentdtlList.Add(new uploadindividualdoc_list
                        {
                            document_title = dt["document_title"].ToString(),
                            document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                            document_name = dt["document_name"].ToString(),
                        });
                        values.uploadindividualdoc_list = getdocumentdtlList;
                    }
                }
                dt_datatable.Dispose();

                msSQL = "select individual2cicdocumentupload_gid,contact2bureau_gid,cicdocument_name,cicdocument_path from agr_mst_tindividual2cicdocumentupload " +
                        " where contact_gid='" + values.contact_gid + "'";
                //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                //if (objODBCDatareader.HasRows == true)
                //{
                //    values.indcicdocument_name = objODBCDatareader["cicdocument_name"].ToString();
                //    values.indcicinddocument_path = (objODBCDatareader["cicdocument_path"].ToString());
                //}
                //objODBCDatareader.Close();
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcicdocumentList = new List<Individualcicdoc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcicdocumentList.Add(new Individualcicdoc_list
                        {
                            cicindividualdocument_name = dt["cicdocument_name"].ToString(),
                            cicindividualdocument_path = objcmnstorage.EncryptData((dt["cicdocument_path"].ToString())),
                            individual2cicdocumentupload_gid = (dt["individual2cicdocumentupload_gid"].ToString()),
                            contact2bureau_gid = (dt["contact2bureau_gid"].ToString())

                        });
                        values.Individualcicdoc_list = getcicdocumentList;
                    }
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }

        public void DaGetGurantorIndividualList(string application_gid, MdlMstGurantorView values)
        {
            msSQL = " select a.contact_gid, a.application_gid, concat_ws(' ', first_name, last_name, middle_name) as individual_name, " +
                        " a.pan_no, aadhar_no, date_format(individual_dob, '%d-%m-%Y') as individual_dob," +
                        " main_occupation, date_format(a.created_date, '%d-%m-%Y') as created_date," +
                        "concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,stakeholder_type from agr_mst_tcontact a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                        " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                        " where application_gid = '" + application_gid + "' and stakeholder_type!='Applicant' and stakeholder_type!='Borrower'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getIndividualList = new List<GurantorIndividual_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getIndividualList.Add(new GurantorIndividual_List
                    {
                        contact_gid = (dr_datarow["contact_gid"].ToString()),
                        individual_name = (dr_datarow["individual_name"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        aadhar_no = (dr_datarow["aadhar_no"].ToString()),
                        individual_dob = (dr_datarow["individual_dob"].ToString()),
                        main_occupation = (dr_datarow["main_occupation"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                    });
                }
                values.GurantorIndividual_List = getIndividualList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetGurantorInstitutionList(string application_gid, MdlMstGurantorView values)
        {
            msSQL = " select a.institution_gid, a.application_gid, cin_no, companytype_name, " +
                        " company_name, companypan_no, date_format(date_incorporation, '%d-%m-%Y') as date_incorporation," +
                        " date_format(a.created_date, '%d-%m-%Y') as created_date," +
                        "concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,stakeholder_type from agr_mst_tinstitution a" +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                        " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                        " where application_gid = '" + application_gid + "' and stakeholder_type!='Applicant' and stakeholder_type!='Borrower'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getInstitutionList = new List<GurantorInstitution_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getInstitutionList.Add(new GurantorInstitution_List
                    {
                        institution_gid = (dr_datarow["institution_gid"].ToString()),
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
                values.GurantorInstitution_List = getInstitutionList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        // Gurantor Institution

        public void DaGetGurantorInstitutionView(string institution_gid, MdlMstInstitutionDtlView values)
        {
            try
            {
                msSQL = " select institution_gid, application_gid, company_name, companypan_no, date_format(date_incorporation, '%d-%m-%Y') as date_incorporation, " +
                        " year_business, month_business, cin_no, official_telephoneno, officialemail_address, companytype_name, escrow,tan_number, stakeholder_type,incometax_returnsstatus, revenue, profit, fixed_assets, sundrydebt_adv, " +
                        " lastyear_turnover, date_format(start_date, '%d-%m-%Y') as start_date, " +
                        " date_format(end_date, '%d-%m-%Y') as end_date, assessmentagency_name, " +
                        " assessmentagencyrating_name, date_format(ratingas_on, '%d-%m-%Y') as ratingas_on, " +
                        " amlcategory_name, businesscategory_name, urn_status, urn,  msme_registration,lei_renewaldate,kin,lglentity_id, " +
                        " contactperson_firstname,contactperson_middlename,contactperson_lastname,designation, " +
                        " date_format(businessstart_date, '%d-%m-%Y') as businessstart_date from agr_mst_tinstitution " +
                        " where institution_gid = '" + institution_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.institution_gid = objODBCDatareader["institution_gid"].ToString();
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
                    values.contactperson_firstname = objODBCDatareader["contactperson_firstname"].ToString();
                    values.contactperson_middlename = objODBCDatareader["contactperson_middlename"].ToString();
                    values.contactperson_lastname = objODBCDatareader["contactperson_lastname"].ToString();
                    values.designation = objODBCDatareader["designation"].ToString();
                    values.businessstart_date = objODBCDatareader["businessstart_date"].ToString();
                    values.tan_number = objODBCDatareader["tan_number"].ToString();
                    values.stakeholder_type = objODBCDatareader["stakeholder_type"].ToString();
                    values.revenue = objODBCDatareader["revenue"].ToString();
                    values.incometax_returnsstatus = objODBCDatareader["incometax_returnsstatus"].ToString();
                    values.profit = objODBCDatareader["profit"].ToString();
                    values.fixed_assets = objODBCDatareader["fixed_assets"].ToString();
                    values.sundrydebt_adv = objODBCDatareader["sundrydebt_adv"].ToString();
                    if (objODBCDatareader["lei_renewaldate"].ToString() == "")
                    {
                    }
                    else
                    {
                        values.editlei_renewaldate = Convert.ToDateTime(objODBCDatareader["lei_renewaldate"]).ToString("dd-MM-yyyy");
                    }
                    values.msme_registration = objODBCDatareader["msme_registration"].ToString();
                    values.lglentity_id = objODBCDatareader["lglentity_id"].ToString();
                    values.kin = objODBCDatareader["kin"].ToString();
                }

                objODBCDatareader.Close();

                msSQL = "select institution2branch_gid,gst_state,gst_no, gst_registered,headoffice_status from agr_mst_tinstitution2branch where institution_gid='" + institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstgst_list = new List<mstgst_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstgst_list.Add(new mstgst_list
                        {
                            institution2branch_gid = (dr_datarow["institution2branch_gid"].ToString()),
                            gst_state = (dr_datarow["gst_state"].ToString()),
                            gst_no = (dr_datarow["gst_no"].ToString()),
                            gst_registered = (dr_datarow["gst_registered"].ToString()),
                            headoffice_status = (dr_datarow["headoffice_status"].ToString())
                        });
                    }
                    values.mstgst_list = getmstgst_list;
                }
                dt_datatable.Dispose();

                msSQL = "  select institution2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, landmark," +
                    " postal_code, city from agr_mst_tinstitution2address where institution_gid='" + institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstaddress_list = new List<mstaddress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstaddress_list.Add(new mstaddress_list
                        {
                            institution2address_gid = (dr_datarow["institution2address_gid"].ToString()),
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
                    values.mstaddress_list = getmstaddress_list;
                }
                dt_datatable.Dispose();


                msSQL = "select mobile_no from agr_mst_tinstitution2mobileno where institution_gid='" + institution_gid + "' and primary_status = 'Yes'";
                values.primaryinstitution_mobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tinstitution2email where institution_gid='" + institution_gid + "' and primary_status = 'Yes'";
                values.primaryinstitution_email = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select institution_gid, mobile_no, whatsapp_no " +
                        " from agr_mst_tinstitution2mobileno " +
                        " where institution_gid = '" + institution_gid + "' and primary_status = 'No' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplication_list = new List<instituionmobilenumber_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplication_list.Add(new instituionmobilenumber_list
                        {
                            institution_gid = (dr_datarow["institution_gid"].ToString()),
                            mobile_no = (dr_datarow["mobile_no"].ToString()),
                            whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),

                        });
                    }
                    values.instituionmobilenumber_list = getapplication_list;
                }
                dt_datatable.Dispose();
                values.status = true;

                msSQL = " select institution_gid, email_address " +
                        " from agr_mst_tinstitution2email " +
                        " where institution_gid = '" + institution_gid + "' and primary_status = 'No' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmail_list = new List<instituionmail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmail_list.Add(new instituionmail_list
                        {
                            email_address = (dr_datarow["email_address"].ToString()),

                        });
                    }
                    values.instituionmail_list = getmail_list;
                }
                dt_datatable.Dispose();


                msSQL = " select institution2form60documentupload_gid,form60document_name,form60document_path from agr_mst_tinstitution2form60documentupload " +
                               " where institution_gid ='" + institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocumentdtlList = new List<institutionform60_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdocumentdtlList.Add(new institutionform60_list
                        {
                            document_name = dt["form60document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData((dt["form60document_path"].ToString())),
                            institution2form60documentupload_gid = dt["institution2form60documentupload_gid"].ToString()
                        });
                        values.institutionform60_list = getdocumentdtlList;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select institution2documentupload_gid,institution_gid,document_name,document_path,document_title,document_id from agr_mst_tinstitution2documentupload " +
                        " where institution_gid='" + institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocumentList = new List<institutiondoc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdocumentList.Add(new institutiondoc_list
                        {
                            document_name = dt["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                            institution2documentupload_gid = dt["institution2documentupload_gid"].ToString(),
                            document_title = dt["document_title"].ToString(),
                            document_id = dt["document_id"].ToString(),
                        });
                        values.institutiondoc_list = getdocumentList;
                    }
                }
                dt_datatable.Dispose();

                msSQL = " select institution2licensedtl_gid,licensetype_gid,licensetype_name,license_no,date_format(issue_date,'%d-%m-%Y') as issue_date," +
                        " date_format(expiry_date,'%d-%m-%Y') as expiry_date from agr_mst_tinstitution2licensedtl" +
                        " where institution_gid='" + institution_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstlicense_list = new List<mstlicense_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstlicense_list.Add(new mstlicense_list
                        {
                            institution2licensedtl_gid = (dr_datarow["institution2licensedtl_gid"].ToString()),
                            licensetype_gid = (dr_datarow["licensetype_gid"].ToString()),
                            licensetype_name = (dr_datarow["licensetype_name"].ToString()),
                            license_number = (dr_datarow["license_no"].ToString()),
                            licenseissue_date = (dr_datarow["issue_date"].ToString()),
                            licenseexpiry_date = (dr_datarow["expiry_date"].ToString())
                        });
                    }
                    values.mstlicense_list = getmstlicense_list;
                }
                dt_datatable.Dispose();


                msSQL = " select bureauname_gid,bureauname_name, bureau_score,date_format(bureauscore_date,'%d-%m-%Y') as bureauscore_date, observations, bureau_response " +
                        "from agr_mst_tinstitution where institution_gid='" + institution_gid + "'";
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
                msSQL = "select institution2cicdocumentupload_gid,institution2bureau_gid,cicdocument_name,cicdocument_path from agr_mst_tinstitution2cicdocumentupload" +
                        " where institution_gid='" + values.institution_gid + "'";
                //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                //if (objODBCDatareader.HasRows == true)
                //{
                //    values.cicdocument_name = objODBCDatareader["cicdocument_name"].ToString();
                //    values.cicdocument_path = (objODBCDatareader["cicdocument_path"].ToString());
                //}

                //objODBCDatareader.Close();
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcicdocumentList = new List<Institutioncicdoc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcicdocumentList.Add(new Institutioncicdoc_list
                        {
                            cicdocument_name = dt["cicdocument_name"].ToString(),
                            cicdocument_path = objcmnstorage.EncryptData((dt["cicdocument_path"].ToString())),
                            institution2cicdocumentupload_gid = (dt["institution2cicdocumentupload_gid"].ToString()),
                            institution2bureau_gid = (dt["institution2bureau_gid"].ToString())

                        });
                        values.Institutioncicdoc_list = getcicdocumentList;
                    }
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetGurantorIndividualView(string contact_gid, MdlMstIndividualDtlView values)
        {
            try
            {
                msSQL = " select contact_gid, application_gid, concat_ws(' ', first_name, last_name, middle_name) as individual_name, " +
                        " pan_no, aadhar_no, individual_dob," +
                        " age, gender_name, designation_name, main_occupation, pep_status, date_format(pepverified_date, '%d-%m-%Y') as pepverified_date, " +
                        " maritalstatus_name, concat_ws(' ', father_firstname, father_middlename, father_lastname) as father_name, " +
                        " father_dob, father_age, " +
                        " concat_ws(' ', mother_firstname, mother_middlename, mother_lastname) as mother_name, " +
                        " mother_dob, mother_age, " +
                        " concat_ws(' ', spouse_firstname, spouse_middlename, spouse_lastname) as spouse_name, " +
                        " spouse_dob, spouse_age, educationalqualification_name, " +
                        " annual_income, monthly_income,  incometype_name as user_type, ownershiptype_name, propertyholder_name, residencetype_name, " +
                        " currentresidence_years, branch_distance, bureauname_name, bureau_score, observations, " +
                        " date_format(bureauscore_date, '%d-%m-%Y') as bureauscore_date, bureau_response,pan_status, " +
                        " group_name, profile, urn_status, urn, fathernominee_status, mothernominee_status, spousenominee_status, othernominee_status,institution_name," +
                        " relationshiptype, nomineefirst_name, nominee_middlename, nominee_lastname, nominee_dob, nominee_age, totallandinacres, cultivatedland, previouscrop, prposedcrop" +
                        " from agr_mst_tcontact " +
                        " where contact_gid = '" + contact_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.contact_gid = objODBCDatareader["contact_gid"].ToString();
                    values.individual_name = objODBCDatareader["individual_name"].ToString();
                    values.pan_no = objODBCDatareader["pan_no"].ToString();
                    values.aadhar_no = objODBCDatareader["aadhar_no"].ToString();
                    values.individual_dob = objODBCDatareader["individual_dob"].ToString();
                    values.age = objODBCDatareader["age"].ToString();
                    values.gender_name = objODBCDatareader["gender_name"].ToString();
                    values.designation_name = objODBCDatareader["designation_name"].ToString();
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
                    values.pan_status = objODBCDatareader["pan_status"].ToString();
                }

                objODBCDatareader.Close();

                msSQL = " SELECT panabsencereason" +
              " from agr_mst_tcontact2panabsencereason where contact_gid='" + contact_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcontactpanabsencereasons_list = new List<contactpanabsencereasons_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    values.contactpanabsencereasons_list = dt_datatable.AsEnumerable().Select(row =>
                      new contactpanabsencereasons_list
                      {
                          panabsencereason = row["panabsencereason"].ToString(),
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();

                msSQL = " select contact2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country," +
                   " postal_code, landmark,latitude,longitude, city from agr_mst_tcontact2address where contact_gid='" + contact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcontactaddress_list = new List<contactaddress_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcontactaddress_list.Add(new contactaddress_list
                        {
                            contact2address_gid = (dr_datarow["contact2address_gid"].ToString()),
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
                            latitude = (dr_datarow["latitude"].ToString()),
                            longitude = (dr_datarow["longitude"].ToString()),
                        });
                    }
                    values.contactaddress_list = getcontactaddress_list;
                }
                dt_datatable.Dispose();


                msSQL = "select mobile_no from agr_mst_tcontact2mobileno where contact_gid='" + contact_gid + "' and primary_status = 'Yes'";
                values.primaryindividual_mobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tcontact2email where contact_gid='" + contact_gid + "' and primary_status = 'Yes'";
                values.primaryindividual_email = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address,contact2email_gid,primary_status from agr_mst_tcontact2email where " +
                         " contact_gid='" + contact_gid + "' and primary_status = 'No'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcontactemail_list = new List<contactemail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcontactemail_list.Add(new contactemail_list
                        {
                            contact2email_gid = (dr_datarow["contact2email_gid"].ToString()),
                            email_address = (dr_datarow["email_address"].ToString()),
                            primary_status = (dr_datarow["primary_status"].ToString()),
                        });
                    }
                }
                values.contactemail_list = getcontactemail_list;
                dt_datatable.Dispose();

                msSQL = "select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from agr_mst_tcontact2mobileno where " +
                        " contact_gid='" + contact_gid + "' and primary_status = 'No'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcontactmobileno_list = new List<contactmobileno_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcontactmobileno_list.Add(new contactmobileno_list
                        {
                            contact2mobileno_gid = (dr_datarow["contact2mobileno_gid"].ToString()),
                            mobile_no = (dr_datarow["mobile_no"].ToString()),
                            primary_status = (dr_datarow["primary_status"].ToString()),
                            whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),
                        });
                    }
                }
                values.contactmobileno_list = getcontactmobileno_list;
                dt_datatable.Dispose();

                msSQL = "select contact2idproof_gid,idproof_name,idproof_no,document_name, document_path from agr_mst_tcontact2idproof where " +
                        " contact_gid='" + contact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcontactidproof_list = new List<contactidproof_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcontactidproof_list.Add(new contactidproof_list
                        {
                            contact2idproof_gid = (dr_datarow["contact2idproof_gid"].ToString()),
                            idproof_name = (dr_datarow["idproof_name"].ToString()),
                            idproof_no = (dr_datarow["idproof_no"].ToString()),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        });

                        values.contactidproof_list = getcontactidproof_list;
                    }
                    dt_datatable.Dispose();
                }

                msSQL = " select document_title,document_name,document_path from agr_mst_tcontact2document " +
                                " where contact_gid='" + contact_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdocumentdtlList = new List<uploadindividualdoc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getdocumentdtlList.Add(new uploadindividualdoc_list
                        {
                            document_title = dt["document_title"].ToString(),
                            document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                            document_name = dt["document_name"].ToString(),
                        });
                        values.uploadindividualdoc_list = getdocumentdtlList;
                    }
                }
                dt_datatable.Dispose();

                msSQL = "select individual2cicdocumentupload_gid,contact2bureau_gid,cicdocument_name,cicdocument_path from agr_mst_tindividual2cicdocumentupload" +
                        " where contact_gid='" + values.contact_gid + "'";
                //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                //if (objODBCDatareader.HasRows == true)
                //{
                //    values.indcicdocument_name = objODBCDatareader["cicdocument_name"].ToString();
                //    values.indcicinddocument_path = (objODBCDatareader["cicdocument_path"].ToString());
                //}

                //objODBCDatareader.Close();
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcicdocumentList = new List<Individualcicdoc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcicdocumentList.Add(new Individualcicdoc_list
                        {
                            cicindividualdocument_name = dt["cicdocument_name"].ToString(),
                            cicindividualdocument_path = objcmnstorage.EncryptData((dt["cicdocument_path"].ToString())),
                            individual2cicdocumentupload_gid = (dt["individual2cicdocumentupload_gid"].ToString()),
                            contact2bureau_gid = (dt["contact2bureau_gid"].ToString())

                        });
                        values.Individualcicdoc_list = getcicdocumentList;
                    }
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }

        public void DaGetProductChargesDtl(string application_gid, MdlMstProductChargesView values)
        {
            try
            {
                msSQL = " select application2loan_gid,date_format(facilityrequested_date, '%d-%m-%Y') as facilityrequested_date, product_type,limit_product, " +
                        " productsub_type, loanfacility_amount, loan_type, rate_interest, penal_interest, programoverall_limit, " +
                        " tenureoverall_limit, facility_type, facility_mode, principalfrequency_name, interestfrequency_name, " +
                        " interest_status, moratorium_type, moratorium_status, date_format(moratorium_startdate, '%d-%m-%Y') as moratorium_startdate, " +
                        " date_format(moratorium_enddate, '%d-%m-%Y') as moratorium_enddate,scheme_type,tenureproduct_month,tenureproduct_days, " +
                        " product_gid,product_name,sector_name,category_name,variety_gid,variety_name,botanical_name,alternative_name,program_gid,program " +
                        " from agr_mst_tapplication2loan " +
                        " where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstloansummary_list = new List<mstLoan_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstloansummary_list.Add(new mstLoan_list
                        {
                           
                            application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                            facilityrequested_date = (dr_datarow["facilityrequested_date"].ToString()),
                            product_type = (dr_datarow["product_type"].ToString()),
                            productsub_type = (dr_datarow["productsub_type"].ToString()),
                            loanfacility_amount = (dr_datarow["loanfacility_amount"].ToString()),
                            loan_type = (dr_datarow["loan_type"].ToString()),
                            rate_interest = (dr_datarow["rate_interest"].ToString()),
                            penal_interest = (dr_datarow["penal_interest"].ToString()),
                            facilityoverall_limit = (dr_datarow["programoverall_limit"].ToString()),
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
                            tenureproduct_days = (dr_datarow["tenureproduct_days"].ToString()),
                            tenureproduct_month = (dr_datarow["tenureproduct_month"].ToString()),
                            limit_product = (dr_datarow["limit_product"].ToString()),
                            product_gid = (dr_datarow["product_gid"].ToString()),
                            product_name = (dr_datarow["product_name"].ToString()),
                            sector_name = (dr_datarow["sector_name"].ToString()),
                            category_name = (dr_datarow["category_name"].ToString()),
                            variety_gid = (dr_datarow["variety_gid"].ToString()),
                            variety_name = (dr_datarow["variety_name"].ToString()),
                            botanical_name = (dr_datarow["botanical_name"].ToString()),
                            alternative_name = (dr_datarow["alternative_name"].ToString()),
                            program_gid = (dr_datarow["program_gid"].ToString()),
                            program = (dr_datarow["program"].ToString()),
                        });
                    }
                    values.mstLoan_list = getmstloansummary_list;
                }
                dt_datatable.Dispose();

                msSQL = " select overalllimit_amount, validityoveralllimit_year, validityoveralllimit_month, validityoveralllimit_days, date_format(validityfrom_date, '%d-%m-%Y') as validityfrom_date , date_format(validityto_date, '%d-%m-%Y') as validityto_date ,application_no," +
                                " calculationoveralllimit_validity from agr_mst_tapplication " +
                                " where application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.overalllimit_amount = objODBCDatareader["overalllimit_amount"].ToString();
                    values.validityoveralllimit_year = objODBCDatareader["validityoveralllimit_year"].ToString();
                    values.validityoveralllimit_month = objODBCDatareader["validityoveralllimit_month"].ToString();
                    values.validityoveralllimit_days = objODBCDatareader["validityoveralllimit_days"].ToString();
                    values.calculationoveralllimit_validity = objODBCDatareader["calculationoveralllimit_validity"].ToString();
                    values.validityfrom_date = objODBCDatareader["validityfrom_date"].ToString();
                    values.validityto_date = objODBCDatareader["validityto_date"].ToString();
                    values.application_no = objODBCDatareader["application_no"].ToString();
                }
                objODBCDatareader.Close();


                msSQL = " select application2loan_gid, source_type, guideline_value, date_format(guideline_date, '%d-%m-%Y') as guideline_date, " +
                        " date_format(marketvalue_date, '%d-%m-%Y') as marketvalue_date, market_value, forcedsource_value, " +
                        " collateralSSV_value, date_format(forcedvalueassessed_on, '%d-%m-%Y') as forcedvalueassessed_on, " +
                        " collateralobservation_summary,product_type,productsub_type from agr_mst_tapplication2loan  " +
                        " where application_gid='" + application_gid + "' and loan_type ='Secured'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstcollateral_list = new List<mstcollateral_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstcollateral_list.Add(new mstcollateral_list
                        {
                            application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                            source_type = (dr_datarow["source_type"].ToString()),
                            guideline_value = (dr_datarow["guideline_value"].ToString()),
                            guideline_date = (dr_datarow["guideline_date"].ToString()),
                            marketvalue_date = (dr_datarow["marketvalue_date"].ToString()),
                            market_value = (dr_datarow["market_value"].ToString()),
                            forcedsource_value = (dr_datarow["forcedsource_value"].ToString()),
                            collateralSSV_value = (dr_datarow["collateralSSV_value"].ToString()),
                            forcedvalueassessed_on = (dr_datarow["forcedvalueassessed_on"].ToString()),
                            collateralobservation_summary = (dr_datarow["collateralobservation_summary"].ToString()),
                            product_type = (dr_datarow["product_type"].ToString()),
                            productsub_type = (dr_datarow["productsub_type"].ToString()),
                        });
                    }
                    values.mstcollateral_list = getmstcollateral_list;
                }
                dt_datatable.Dispose();

                msSQL = "select processing_fee, processing_collectiontype, doc_charges, doccharge_collectiontype, fieldvisit_charges," +
                        " fieldvisit_charges_collectiontype, adhoc_fee, adhoc_collectiontype, life_insurance, lifeinsurance_collectiontype, " +
                        " acct_insurance, total_collect, total_deduct,product_type,producttype_gid,acctinsurance_collectiontype from agr_mst_tapplicationservicecharge " +
                        " where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstservicecharge_list = new List<servicecharge_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstservicecharge_list.Add(new servicecharge_List
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
                            acctinsurance_collectiontype = (dr_datarow["acctinsurance_collectiontype"].ToString()),
                            total_collect = (dr_datarow["total_collect"].ToString()),
                            total_deduct = (dr_datarow["total_deduct"].ToString())
                        });
                    }
                    values.servicecharge_List = getmstservicecharge_list;
                }
                dt_datatable.Dispose();

                msSQL = " select application2hypothecation_gid, security_type, security_description, security_value, " +
                        " date_format(securityassessed_date, '%d-%m-%Y') as securityassessed_date, asset_id, roc_fillingid, " +
                        " CERSAI_fillingid, hypoobservation_summary, primary_security " +
                        " from agr_mst_tapplication2hypothecation " +
                        " where application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application2hypothecation_gid = objODBCDatareader["application2hypothecation_gid"].ToString();
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

        public void DaGetHypoDocDtl(string application2hypothecation_gid, MdlMstProductChargesView values)
        {
            try
            {
                msSQL = " select hypothecationdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                      " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                      " from agr_mst_tuploadhypothecationocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                      " and b.user_gid = c.user_gid and application2hypothecation_gid='" + application2hypothecation_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<HypoDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new HypoDocumentList
                        {
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["hypothecationdocument_gid"].ToString()),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString(),
                            document_title = dr_datarow["document_title"].ToString()
                        });
                    }
                    values.HypoDocumentList = get_filename;
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

        public void DaGetCollateralDocDtl(string application2loan_gid, MdlMstProductChargesView values)
        {
            try
            {
                msSQL = " select collateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                       " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                       " from agr_mst_tuploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                       " and b.user_gid = c.user_gid and application2loan_gid='" + application2loan_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<CollatralDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new CollatralDocumentList
                        {
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["collateraldocument_gid"].ToString()),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString(),
                            document_title = dr_datarow["document_title"].ToString()
                        });
                    }
                    values.CollatralDocumentList = get_filename;
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
        public void DaGetPurposeofLoan(string application2loan_gid, MdlMstProductChargesView values)
        {
            try
            {
                msSQL = " select enduse_purpose from agr_mst_tapplication2loan where application2loan_gid='" + application2loan_gid + "'";
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

        public void DaGetLoantoBuyerList(string application2loan_gid, MdlMstProductChargesView values)
        {
            try
            {
                msSQL = "select application2buyer_gid,buyer_name,buyer_gid,buyer_limit,availed_limit,balance_limit,margin,bill_tenure " +
                   " from agr_mst_tapplication2buyer where application2loan_gid='" + application2loan_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstbuyer_list = new List<mstbuyer_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstbuyer_list.Add(new mstbuyer_list
                        {
                            application2buyer_gid = (dr_datarow["application2buyer_gid"].ToString()),
                            buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                            buyer_name = (dr_datarow["buyer_name"].ToString()),
                            buyer_limit = (dr_datarow["buyer_limit"].ToString()),
                            availed_limit = (dr_datarow["availed_limit"].ToString()),
                            balance_limit = (dr_datarow["balance_limit"].ToString()),
                            bill_tenure = (dr_datarow["bill_tenure"].ToString()),
                            margin = (dr_datarow["margin"].ToString())
                        });
                    }
                    values.mstbuyer_list = getmstbuyer_list;
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

        public void DaGetGradingView(string application2gradingtool_gid, MdlMstGradeToolView values)
        {
            try
            {
                msSQL = " select fpo_acscore, numnerofaactive_fig, existinglending_directandindirect, nonnegotiableconditions_met, " +
                         " outstandingportfolio_directandindirect, institution_directandindrectborrowing, totaldisbursements_otherlenders, " +
                         " par90_managedbyonlyinstitution_direct,recommendation, majorcrops, alternativeincomesource, " +
                         " objevtiveoffpo, recommendation2, overallfporating, overallfpograde, date_format(dateofsurvey, '%d-%m-%Y') as dateofsurvey, " +
                         " b.numberofstates, b.numberofdistricts, b.numberofbranches, b.numberofmembers, b.numberof_activemembers," +
                         " b.numberofgroups, b.zonaloffices, b.regionaloffices, b.branches, b.adminstaff, b.fieldstaff, b.fieldstaff_ratio " +
                         " from agr_mst_tapplication2gradingtool a" +
                         " left join agr_mst_tapplication2geographic b on a.application2gradingtool_gid = b.application2gradingtool_gid" +
                         " where a.application2gradingtool_gid='" + application2gradingtool_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.fpo_acscore = objODBCDatareader["fpo_acscore"].ToString();
                    values.numnerofaactive_fig = objODBCDatareader["numnerofaactive_fig"].ToString();
                    values.existinglending_directandindirect = objODBCDatareader["existinglending_directandindirect"].ToString();
                    values.outstandingportfolio_directandindirect = objODBCDatareader["outstandingportfolio_directandindirect"].ToString();
                    values.nonnegotiableconditions_met = objODBCDatareader["nonnegotiableconditions_met"].ToString();
                    values.institution_directandindrectborrowing = objODBCDatareader["institution_directandindrectborrowing"].ToString();
                    values.totaldisbursements_otherlenders = objODBCDatareader["totaldisbursements_otherlenders"].ToString();
                    values.par90_managedbyonlyinstitution_direct = objODBCDatareader["par90_managedbyonlyinstitution_direct"].ToString();
                    values.fpo_recommendation = objODBCDatareader["recommendation"].ToString();
                    values.majorcrops = objODBCDatareader["majorcrops"].ToString();
                    values.alternativeincomesource = objODBCDatareader["alternativeincomesource"].ToString();
                    values.objevtiveoffpo = objODBCDatareader["objevtiveoffpo"].ToString();
                    values.recommendation = objODBCDatareader["recommendation2"].ToString();
                    values.overallfporating = objODBCDatareader["overallfporating"].ToString();
                    values.recommendation = objODBCDatareader["recommendation"].ToString();
                    values.numberofstates = objODBCDatareader["numberofstates"].ToString();
                    values.numberofdistricts = objODBCDatareader["numberofdistricts"].ToString();
                    values.numberofbranches = objODBCDatareader["numberofbranches"].ToString();
                    values.numberofmembers = objODBCDatareader["numberofmembers"].ToString();
                    values.numberof_activemembers = objODBCDatareader["numberof_activemembers"].ToString();
                    values.numberofgroups = objODBCDatareader["numberofgroups"].ToString();
                    values.zonaloffices = objODBCDatareader["zonaloffices"].ToString();
                    values.regionaloffices = objODBCDatareader["regionaloffices"].ToString();
                    values.branches = objODBCDatareader["branches"].ToString();
                    values.adminstaff = objODBCDatareader["adminstaff"].ToString();
                    values.fieldstaff = objODBCDatareader["fieldstaff"].ToString();
                    values.fieldstaff_ratio = objODBCDatareader["fieldstaff_ratio"].ToString();
                    values.dateofsurvey = objODBCDatareader["dateofsurvey"].ToString();
                    values.overallfpograde = objODBCDatareader["overallfpograde"].ToString();

                }
                objODBCDatareader.Close();


                msSQL = "select application2gradingassesment_gid,application2gradingtool_gid,maximumscored,actualscored," +
                        " assessmentcriteria_in,assessmentcriteria_ingrade,shareholdermale_in,shareholderfemale_in,bodmale_in,bodfemale_in, " +
                        " (select group_concat(distinct assessmentcriteria_name) as assessmentcriteria_name  from agr_mst_tassessmentcriteria2dropdown d " +
                        " where d.application2gradingassesment_gid = a.application2gradingassesment_gid ) as assessmentcriteria_name " +
                        " from agr_mst_tapplication2gradingassessmentcriteria a  " +
                        " where application2gradingtool_gid ='" + application2gradingtool_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstgradingassesment_list = new List<mstassessment_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstgradingassesment_list.Add(new mstassessment_list
                        {
                            assessmentcriteria = (dr_datarow["assessmentcriteria_name"].ToString()),
                            maximumscored = (dr_datarow["maximumscored"].ToString()),
                            actualscored = (dr_datarow["actualscored"].ToString()),
                            assessmentcriteria_in = (dr_datarow["assessmentcriteria_in"].ToString()),
                            assessmentcriteria_ingrade = (dr_datarow["assessmentcriteria_ingrade"].ToString()),
                            shareholdermale_in = (dr_datarow["shareholdermale_in"].ToString()),
                            shareholderfemale_in = (dr_datarow["shareholderfemale_in"].ToString()),
                            bodmale_in = (dr_datarow["bodmale_in"].ToString()),
                            bodfemale_in = (dr_datarow["bodfemale_in"].ToString()),
                        });
                    }
                    values.mstassessment_list = getmstgradingassesment_list;
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

        public void DaGetGrouptoMemberList(string group_gid, MdlMstGroupMember values)
        {
            msSQL = "select a.contact_gid,a.pan_no,a.aadhar_no,concat(first_name, ' ',middle_name,' ',last_name) as individual_name,stakeholder_type,credit_status," +
                    " (select count(*) from agr_trn_tgroupdocumentchecklist where credit_gid =a.contact_gid " +
                    " and(untagged_type is null or untagged_type = 'N')) as OverallDeferralcount, " +
                    " (select count(*) from agr_trn_tgroupcovenantdocumentchecklist where credit_gid = a.contact_gid " +
                    " and(untagged_type is null or untagged_type = 'N')) as overallCovenantCount, " +
                    " (select count(*) from agr_trn_tgroupdocumentchecklist where credit_gid = a.contact_gid " +
                    " and(untagged_type is null or untagged_type = 'N') and overall_docstatus in ('Waived','Document Verified')) as verifieddeferraldoc, " +
                    " (select count(*) from agr_trn_tgroupcovenantdocumentchecklist where credit_gid = a.contact_gid " +
                    " and(untagged_type is null or untagged_type = 'N') and overall_docstatus in ('Waived','Document Verified'))  as verifiedcovenantdoc " +
                    " from agr_mst_tcontact a " +
                    " where group_gid='" + group_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgroupmember_list = new List<groupmember_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getgroupmember_list.Add(new groupmember_list
                    {
                        contact_gid = (dr_datarow["contact_gid"].ToString()),
                        individual_name = (dr_datarow["individual_name"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        aadhar_no = (dr_datarow["aadhar_no"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                        credit_status = (dr_datarow["credit_status"].ToString()),
                        overallCovenantCount = (dr_datarow["overallCovenantCount"].ToString()),
                        OverallDeferralcount = (dr_datarow["OverallDeferralcount"].ToString()),
                        verifieddeferraldoc = (dr_datarow["verifieddeferraldoc"].ToString()),
                        verifiedcovenantdoc = (dr_datarow["verifiedcovenantdoc"].ToString()),
                    });
                }
            }
            values.groupmember_list = getgroupmember_list;
            dt_datatable.Dispose();
        }

        public void DaGetPrimaryAndOtherMobileNumbers(string contact_gid, MdlMstContactMobileNumbers values)
        {

            try
            {
                msSQL = "select mobile_no from agr_mst_tcontact2mobileno where contact_gid='" + contact_gid + "' and primary_status = 'Yes'";
                values.primaryindividual_mobileno = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select contact_gid, mobile_no, whatsapp_no " +
                        " from agr_mst_tcontact2mobileno " +
                        " where contact_gid = '" + contact_gid + "' and primary_status = 'No' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmobileno_list = new List<individualmobileno_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmobileno_list.Add(new individualmobileno_list
                        {
                            contact_gid = (dr_datarow["contact_gid"].ToString()),
                            mobile_no = (dr_datarow["mobile_no"].ToString()),
                            whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),

                        });
                    }
                    values.individualmobileno_list = getmobileno_list;
                }
                dt_datatable.Dispose();

                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetGetPrimaryAndOtherEmails(string contact_gid, MdlMstContactEmails values)
        {

            try
            {
                msSQL = "select email_address from agr_mst_tcontact2email where contact_gid='" + contact_gid + "' and primary_status = 'Yes'";
                values.primaryindividual_email = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select email_address " +
                        " from agr_mst_tcontact2email " +
                        " where contact_gid = '" + contact_gid + "' and primary_status = 'No' ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getemail_list = new List<individualemail_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getemail_list.Add(new individualemail_list
                        {
                            email_address = (dr_datarow["email_address"].ToString()),

                        });
                    }
                    values.individualemail_list = getemail_list;
                }
                dt_datatable.Dispose();

                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetIndividualBureauDtls(string contact_gid, MdlMstContactBureau values)
        {

            try
            {
                msSQL = " select bureauname_gid,bureauname_name, bureau_score,date_format(bureauscore_date,'%d-%m-%Y') as bureauscore_date, observations, bureau_response " +
                        " from agr_mst_tcontact where contact_gid='" + contact_gid + "'";
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

                msSQL = "select individual2cicdocumentupload_gid,contact2bureau_gid,cicdocument_name,cicdocument_path from agr_mst_tindividual2cicdocumentupload" +
                        " where contact_gid='" + contact_gid + "'";
                //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                //if (objODBCDatareader.HasRows == true)
                //{
                //    values.cicdocument_name = objODBCDatareader["cicdocument_name"].ToString();
                //    values.cicdocument_path = (objODBCDatareader["cicdocument_path"].ToString());
                //}
                //objODBCDatareader.Close();
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcicdocumentList = new List<Individualcicdoc_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcicdocumentList.Add(new Individualcicdoc_list
                        {
                            cicdocument_name = dt["cicdocument_name"].ToString(),
                            cicdocument_path = objcmnstorage.EncryptData((dt["cicdocument_path"].ToString())),
                            individual2cicdocumentupload_gid = (dt["individual2cicdocumentupload_gid"].ToString()),
                            contact2bureau_gid = (dt["contact2bureau_gid"].ToString())

                        });
                        values.Individualcicdoc_list = getcicdocumentList;
                    }
                }
                dt_datatable.Dispose();

                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetIndividualList(string application_gid, MdlCreditView values)
        {
            msSQL = " select a.contact_gid, a.application_gid, concat_ws(' ', first_name, last_name, middle_name) as individual_name, " +
                 " (select crimecheck_flag from agr_trn_tcrimechecksearchrecord xx where " +
                      " xx.contact_gid = a.contact_gid order by (xx.created_date) desc limit 1) as crimecheck_flag," +
                      " (select crimecheck_flag from agr_trn_tcrimecheckrtsearchrecord cc where " +
                      " cc.contact_gid = a.contact_gid order by (cc.created_date) desc limit 1) as rtcrimecheck_flag," +
                      " a.pan_no, aadhar_no, date_format(individual_dob, '%d-%m-%Y') as individual_dob,a.age,designation_name," +
                      " main_occupation, date_format(a.created_date, '%d-%m-%Y') as created_date," +
                      " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,stakeholder_type,institution_name,credit_status, " +
                      " (select count(*) from agr_trn_tgroupdocumentchecklist where credit_gid =a.contact_gid " +
                      " and(untagged_type is null or untagged_type = 'N')) as OverallDeferralcount, " +
                      " (select count(*) from agr_trn_tgroupcovenantdocumentchecklist where credit_gid = a.contact_gid " +
                      " and(untagged_type is null or untagged_type = 'N')) as overallCovenantCount, " +
                      " (select count(*) from agr_trn_tgroupdocumentchecklist where credit_gid = a.contact_gid " +
                      " and(untagged_type is null or untagged_type = 'N') and overall_docstatus in ('Waived','Document Verified')) as verifieddeferraldoc, " +
                      " (select count(*) from agr_trn_tgroupcovenantdocumentchecklist where credit_gid = a.contact_gid " +
                      " and(untagged_type is null or untagged_type = 'N') and overall_docstatus in ('Waived','Document Verified'))  as verifiedcovenantdoc " +
                      " from agr_mst_tcontact a" +
                      " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                      " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                   //   " left join agr_trn_tcrimechecksearchrecord d on d.contact_gid = a.contact_gid" +
                   //   " left join agr_trn_tcrimecheckrtsearchrecord e on e.contact_gid = a.contact_gid" +
                      " where a.application_gid = '" + application_gid + "' group by a.contact_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getindividualList = new List<individual_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getindividualList.Add(new individual_List
                    {
                        contact_gid = (dr_datarow["contact_gid"].ToString()),
                        crimecheck_flag = (dr_datarow["crimecheck_flag"].ToString()),
                        rtcrimecheck_flag = (dr_datarow["rtcrimecheck_flag"].ToString()),
                        individual_name = (dr_datarow["individual_name"].ToString()),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        aadhar_no = (dr_datarow["aadhar_no"].ToString()),
                        individual_dob = (dr_datarow["individual_dob"].ToString()),
                        main_occupation = (dr_datarow["main_occupation"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                        company_name = (dr_datarow["institution_name"].ToString()),
                        credit_status = (dr_datarow["credit_status"].ToString()),
                        age = (dr_datarow["age"].ToString()),
                        designation_name = (dr_datarow["designation_name"].ToString()),
                        overallCovenantCount = (dr_datarow["overallCovenantCount"].ToString()),
                        OverallDeferralcount = (dr_datarow["OverallDeferralcount"].ToString()),
                        verifieddeferraldoc = (dr_datarow["verifieddeferraldoc"].ToString()),
                        verifiedcovenantdoc = (dr_datarow["verifiedcovenantdoc"].ToString()),
                    });
                }
                values.individual_List = getindividualList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetInstitutionList(string application_gid, MdlCreditView values)
        {
            msSQL = " select a.institution_gid, a.application_gid, cin_no, companytype_name, " +
                     " company_name, companypan_no, date_format(date_incorporation, '%d-%m-%Y') as date_incorporation," +
                     " date_format(a.created_date, '%d-%m-%Y') as created_date," +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,stakeholder_type,credit_status, " +
                      " (select crimecheck_flag from agr_trn_tcrimechecksearchrecord xx where " +
                     " xx.institution_gid = a.institution_gid order by (xx.created_date) desc limit 1) as crimecheck_flag," +
                     " (select crimecheck_flag from agr_trn_tcrimecheckrtsearchrecord cc where " +
                     " cc.institution_gid = a.institution_gid order by (cc.created_date) desc limit 1) as rtcrimecheck_flag," +
                     " (select count(*) from agr_trn_tgroupdocumentchecklist where credit_gid =a.institution_gid " +
                     " and(untagged_type is null or untagged_type = 'N')) as OverallDeferralcount, " +
                     " (select count(*) from agr_trn_tgroupcovenantdocumentchecklist where credit_gid = a.institution_gid " +
                     " and(untagged_type is null or untagged_type = 'N')) as overallCovenantCount, " +
                     " (select count(*) from agr_trn_tgroupdocumentchecklist where credit_gid = a.institution_gid " +
                     " and(untagged_type is null or untagged_type = 'N') and overall_docstatus in ('Waived','Document Verified')) as verifieddeferraldoc, " +
                     " (select count(*) from agr_trn_tgroupcovenantdocumentchecklist where credit_gid = a.institution_gid " +
                     " and(untagged_type is null or untagged_type = 'N') and overall_docstatus in ('Waived','Document Verified'))  as verifiedcovenantdoc " +
                     " from agr_mst_tinstitution a" +
                     " left join hrm_mst_temployee b on b.employee_gid = a.created_by" +
                     " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                //     " left join agr_trn_tcrimechecksearchrecord d on d.institution_gid = a.institution_gid" +
                 //    " left join agr_trn_tcrimecheckrtsearchrecord e on e.institution_gid = a.institution_gid" +
                     " where a.application_gid = '" + application_gid + "' group by a.institution_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinstitutionList = new List<institution_List>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getinstitutionList.Add(new institution_List
                    {
                        institution_gid = (dr_datarow["institution_gid"].ToString()),
                        crimecheck_flag = (dr_datarow["crimecheck_flag"].ToString()),
                        rtcrimecheck_flag = (dr_datarow["rtcrimecheck_flag"].ToString()),
                        company_name = (dr_datarow["company_name"].ToString()),
                        companypan_no = (dr_datarow["companypan_no"].ToString()),
                        cin_no = (dr_datarow["cin_no"].ToString()),
                        companytype_name = (dr_datarow["companytype_name"].ToString()),
                        date_incorporation = (dr_datarow["date_incorporation"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                        credit_status = (dr_datarow["credit_status"].ToString()),
                        overallCovenantCount = (dr_datarow["overallCovenantCount"].ToString()),
                        OverallDeferralcount = (dr_datarow["OverallDeferralcount"].ToString()),
                        verifieddeferraldoc = (dr_datarow["verifieddeferraldoc"].ToString()),
                        verifiedcovenantdoc = (dr_datarow["verifiedcovenantdoc"].ToString()),
                    });
                }
                values.institution_List = getinstitutionList;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetRMDetailsView(string application_gid, MdlRMDtlView values)
        {
            try
            {
                msSQL = " select d.department_name,concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as RM_Name, " +
                        " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as applicationinitiated_date,date_format(a.ccsubmitted_date, '%d-%m-%Y %h:%i %p') as ccsubmitted_date, " +
                        " concat(f.user_firstname, ' ', f.user_lastname, ' / ', f.user_code) as ccsubmitted_by from agr_mst_tapplication a " +
                        " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join hrm_mst_tdepartment d on d.department_gid = b.department_gid " +
                        " left join hrm_mst_temployee e on e.employee_gid = a.ccsubmitted_by " +
                        " left join adm_mst_tuser f on f.user_gid = e.user_gid " +
                        " where application_gid = '" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.department_name = objODBCDatareader["department_name"].ToString();
                    values.RM_Name = objODBCDatareader["RM_Name"].ToString();
                    values.applicationinitiated_date = objODBCDatareader["applicationinitiated_date"].ToString();
                    values.ccsubmitted_date = objODBCDatareader["ccsubmitted_date"].ToString();
                    values.ccsubmitted_by = objODBCDatareader["ccsubmitted_by"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select approval_name, date_format(approved_date, '%d-%m-%Y %h:%i %p') as approved_date " +
                        " from agr_trn_tappcreditapproval " +
                        " where application_gid = '" + application_gid + "' and hierary_level = '0'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.creditunderwritten_by = objODBCDatareader["approval_name"].ToString();
                    values.creditunderwritten_date = objODBCDatareader["approved_date"].ToString();
                }
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetLoanProgramValueChain(string application2loan_gid, MdlMstProductChargesView values)
        {
            try
            {
                msSQL = " select program, primaryvaluechain_name, secondaryvaluechain_name,product_gid,product_name, " +
                        " variety_gid,variety_name,sector_name,category_name,botanical_name,alternative_name  " + 
                        " from agr_mst_tapplication2loan where application2loan_gid='" + application2loan_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.program = objODBCDatareader["program"].ToString();
                    values.primaryvaluechain_name = objODBCDatareader["primaryvaluechain_name"].ToString();
                    values.secondaryvaluechain_name = objODBCDatareader["secondaryvaluechain_name"].ToString();
                    //values.milestone_applicability = objODBCDatareader["milestone_applicability"].ToString();
                    //values.insurance_applicability = objODBCDatareader["insurance_applicability"].ToString();
                    //values.milestonepayment_gid = objODBCDatareader["milestonepayment_gid"].ToString();
                    //values.milestonepayment_name = objODBCDatareader["milestonepayment_name"].ToString();
                    //values.sa_payout = objODBCDatareader["sa_payout"].ToString();
                    //values.insurance_availability = objODBCDatareader["insurance_availability"].ToString();
                    //values.insurance_percent = objODBCDatareader["insurance_percent"].ToString();
                    //values.insurance_cost = objODBCDatareader["insurance_cost"].ToString();
                    //values.net_yield = objODBCDatareader["net_yield"].ToString();
                }
                objODBCDatareader.Close();

                msSQL = " select application2product_gid,product_gid,product_name,variety_gid,variety_name,sector_name,category_name,hsn_code, " +
                        " botanical_name,alternative_name,unitpricevalue_commodity,natureformstate_commodity,qualityof_commodity, " +
                        " quantity,uom_gid,uom_name, headingdesc_product,typeofsupply_naturename,sectorclassification_name " +
                        " from agr_mst_tapplication2product where application2loan_gid='" + application2loan_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstproductdtl_list = new List<mstproductdtl_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstproductdtl_list.Add(new mstproductdtl_list
                        {
                            application2product_gid = (dr_datarow["application2product_gid"].ToString()),
                            product_gid = (dr_datarow["product_gid"].ToString()),
                            product_name = (dr_datarow["product_name"].ToString()),
                            variety_gid = (dr_datarow["variety_gid"].ToString()),
                            variety_name = (dr_datarow["variety_name"].ToString()),
                            sector_name = (dr_datarow["sector_name"].ToString()),
                            category_name = (dr_datarow["category_name"].ToString()),
                            botanical_name = (dr_datarow["botanical_name"].ToString()),
                            alternative_name = (dr_datarow["alternative_name"].ToString()),
                            hsn_code = (dr_datarow["hsn_code"].ToString()),
                            unitpricevalue_commodity = (dr_datarow["unitpricevalue_commodity"].ToString()),
                            natureformstate_commodity = (dr_datarow["natureformstate_commodity"].ToString()),
                            qualityof_commodity = (dr_datarow["qualityof_commodity"].ToString()),
                            quantity = (dr_datarow["quantity"].ToString()),
                            uom_gid = (dr_datarow["uom_gid"].ToString()),
                            uom_name = (dr_datarow["uom_name"].ToString()),
                            headingdesc_product = dr_datarow["headingdesc_product"].ToString(),
                            typeofsupply_naturename = dr_datarow["typeofsupply_naturename"].ToString(),
                            sectorclassification_name = dr_datarow["sectorclassification_name"].ToString(),
                        });
                    }
                    values.mstproductdtl_list = getmstproductdtl_list;
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

        public void DaGetIndividualImportLog(string application_gid, MdlExcelImportApplication values)
        {
            try
            {
                msSQL = " select concat(first_name, ' ', last_name) as individual_name, contactimportlog_status as reason " +
                        " from agr_trn_tcontactimportlog" +
                        " where application_gid = '" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getindividualimport_List = new List<individualimport_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getindividualimport_List.Add(new individualimport_List
                        {
                            individual_name = (dr_datarow["individual_name"].ToString()),
                            reason = (dr_datarow["reason"].ToString()),
                        });
                    }
                    values.individualimport_List = getindividualimport_List;
                }
                dt_datatable.Dispose();
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetInstitutionImportLog(string application_gid, MdlExcelImportApplication values)
        {
            try
            {
                msSQL = " select company_name, institutionimportlog_status as reason " +
                        " from agr_trn_tcontactimportlog" +
                        " where application_gid = '" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getinstitutionimport_List = new List<institutionimport_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getinstitutionimport_List.Add(new institutionimport_List
                        {
                            company_name = (dr_datarow["company_name"].ToString()),
                            reason = (dr_datarow["reason"].ToString()),
                        });
                    }
                    values.institutionimport_List = getinstitutionimport_List;
                }
                dt_datatable.Dispose();
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetGroupImportLog(string application_gid, MdlExcelImportApplication values)
        {
            try
            {
                msSQL = " select group_name, groupimportlog_status as reason " +
                        " from agr_trn_tcontactimportlog" +
                        " where application_gid = '" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getgroupimport_List = new List<groupimport_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getgroupimport_List.Add(new groupimport_List
                        {
                            group_name = (dr_datarow["group_name"].ToString()),
                            reason = (dr_datarow["reason"].ToString()),
                        });
                    }
                    values.groupimport_List = getgroupimport_List;
                }
                dt_datatable.Dispose();
            }
            catch
            {
                values.status = false;
            }
        }

        //Loan Details View
        public void DaGetLoanDetailsView(string application_gid, MdlMstApplicationView values)
        {
            msSQL = " select group_concat(ifnull(c.urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tinstitution c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            institution_urn = objdbconn.GetExecuteScalar(msSQL);
            institution_urn = institution_urn.Replace(",,", ",");
            institution_urn = institution_urn.Replace(",,,", ",");
            institution_urn = institution_urn.TrimEnd(',');

            msSQL = " select group_concat(ifnull(c.urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tcontact c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            individual_urn = objdbconn.GetExecuteScalar(msSQL);
            individual_urn = individual_urn.Replace(",,", ",");
            individual_urn = individual_urn.Replace(",,,", ",");
            individual_urn = individual_urn.TrimEnd(',');

            msSQL = " select group_concat(ifnull(c.group_urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tgroup c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            group_urn = objdbconn.GetExecuteScalar(msSQL);
            group_urn = group_urn.Replace(",,", ",");
            group_urn = group_urn.Replace(",,,", ",");
            group_urn = group_urn.TrimEnd(',');

            try
            {
                msSQL = " select a.customer_name, b.stakeholder_type, 'Institution' as applicant_type, a.urn,a.account_no,(format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt, " +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN ' Active' " +
                        " WHEN a.ac_status = '2' THEN ' Closed' " +
                        " WHEN a.ac_status = '3' THEN ' Frozen' " +
                        "END as ac_status,(format((a.ledger * -1),2,'en_IN')) as ledger,a.rbiold_oddays,a.late_charge,  " +
                        " a.max_dpd,a.Vertical,a.lastdemandrundate from lgl_tmp_tmisdata a " +
                        " left join agr_mst_tinstitution b on a.urn = b.urn " +
                        " where a.urn in ('" + institution_urn.Replace(",", "','") + "')" +
                        " union " +
                        " select a.customer_name, b.stakeholder_type, 'Individual' as applicant_type, a.urn,a.account_no,(format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt, " +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN ' Active' " +
                        " WHEN a.ac_status = '2' THEN ' Closed' " +
                        "  WHEN a.ac_status = '3' THEN ' Frozen' " +
                         "END as ac_status,(format((a.ledger * -1),2,'en_IN')) as ledger,a.rbiold_oddays, a.late_charge, " +
                        " a.max_dpd,a.Vertical,a.lastdemandrundate from lgl_tmp_tmisdata a " +
                        " left join agr_mst_tcontact b on a.urn = b.urn " +
                        " where a.urn in ('" + individual_urn.Replace(",", "','") + "')" +
                        " union " +
                        " select a.customer_name, '-' as stakeholder_type, 'Group' as applicant_type, a.urn,a.account_no,(format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt, " +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN ' Active' " +
                        " WHEN a.ac_status = '2' THEN ' Closed' " +
                        "  WHEN a.ac_status = '3' THEN ' Frozen' " +
                         "END as ac_status,(format((a.ledger * -1),2,'en_IN')) as ledger,a.rbiold_oddays,a.late_charge,  " +
                        " a.max_dpd,a.Vertical,a.lastdemandrundate from lgl_tmp_tmisdata a " +
                        " left join agr_mst_tgroup b on a.urn = b.group_urn " +
                        " where a.urn in ('" + group_urn.Replace(",", "','") + "')";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var loandetails_List = new List<alldatamodified_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        if (!String.IsNullOrEmpty(dr_datarow["lastdemandrundate"].ToString()))
                        {
                            loandetails_List.Add(new alldatamodified_List
                            {
                                urn = (dr_datarow["urn"].ToString()),
                                lan = (dr_datarow["account_no"].ToString()),
                                account_status = (dr_datarow["ac_status"].ToString()),
                                ledger_balance = (dr_datarow["ledger"].ToString().Replace("-0.00", "0")),
                                maximum_od_day = (dr_datarow["max_dpd"].ToString()),
                                rbi_od_days = (dr_datarow["rbiold_oddays"].ToString()),
                                vertical = (dr_datarow["Vertical"].ToString()),
                                next_due_date = Convert.ToDateTime(dr_datarow["lastdemandrundate"].ToString()).ToString("dd/MM/yyyy"),
                                customer_name = (dr_datarow["customer_name"].ToString()),
                                stackholder_type = (dr_datarow["stakeholder_type"].ToString()),
                                company_type = (dr_datarow["applicant_type"].ToString()),
                                Net_Payoff_Amt = (dr_datarow["Net_Payoff_Amt"].ToString().Replace("0.00", "0")),
                                late_charge = (dr_datarow["late_charge"].ToString())
                            });
                        }
                        else
                        {
                            loandetails_List.Add(new alldatamodified_List
                            {
                                urn = (dr_datarow["urn"].ToString()),
                                lan = (dr_datarow["account_no"].ToString()),
                                account_status = (dr_datarow["ac_status"].ToString()),
                                ledger_balance = (dr_datarow["ledger"].ToString().Replace("-0.00", "0")),
                                maximum_od_day = (dr_datarow["max_dpd"].ToString()),
                                rbi_od_days = (dr_datarow["rbiold_oddays"].ToString()),
                                vertical = (dr_datarow["Vertical"].ToString()),
                                next_due_date = (dr_datarow["lastdemandrundate"].ToString()),
                                customer_name = (dr_datarow["customer_name"].ToString()),
                                stackholder_type = (dr_datarow["stakeholder_type"].ToString()),
                                company_type = (dr_datarow["applicant_type"].ToString()),
                                Net_Payoff_Amt = (dr_datarow["Net_Payoff_Amt"].ToString().Replace("0.00", "0"))
                            });
                        }
                    }
                    values.alldatamodified_List = loandetails_List;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }
        }

        //Penality View
        public void DaGetPenaltyView(string application_gid, MdlMstApplicationView values)
        {
            msSQL = " select group_concat(ifnull(c.urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tinstitution c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            institution_urn = objdbconn.GetExecuteScalar(msSQL);
            institution_urn = institution_urn.Replace(",,", ",");
            institution_urn = institution_urn.Replace(",,,", ",");
            institution_urn = institution_urn.TrimEnd(',');

            msSQL = " select group_concat(ifnull(c.urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tcontact c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            individual_urn = objdbconn.GetExecuteScalar(msSQL);
            individual_urn = individual_urn.Replace(",,", ",");
            individual_urn = individual_urn.Replace(",,,", ",");
            individual_urn = individual_urn.TrimEnd(',');

            msSQL = " select group_concat(ifnull(c.group_urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tgroup c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            group_urn = objdbconn.GetExecuteScalar(msSQL);
            group_urn = group_urn.Replace(",,", ",");
            group_urn = group_urn.Replace(",,,", ",");
            group_urn = group_urn.TrimEnd(',');

            try
            {
                msSQL = " select a.customer_name, b.stakeholder_type, 'Institution' as applicant_type, a.urn, a.account_no,(format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt, " +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN 'Active'  " +
                        " WHEN a.ac_status = '2' THEN 'Closed' " +
                        " WHEN a.ac_status = '3' THEN 'Frozen' " +
                        " END as ac_status, (format((a.BookedPenalIntere),2,'en_IN')) as  BookedPenalIntere,  " +
                        " (format((a.TotalPenalInterestR),2,'en_IN')) as TotalPenalInterestR,(format((a.BookedNotDuePenalInter),2,'en_IN')) as BookedNotDuePenalInter from lgl_tmp_tmisdata a " +
                        " left join agr_mst_tinstitution b on a.urn = b.urn " +
                        " where a.urn in ('" + institution_urn.Replace(",", "','") + "')" +
                        " union " +
                        " select a.customer_name, b.stakeholder_type,  'Individual' as applicant_type, a.urn, a.account_no, (format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt, " +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN 'Active'  " +
                        " WHEN a.ac_status = '2' THEN 'Closed' " +
                        " WHEN a.ac_status = '3' THEN 'Frozen' " +
                        " END as ac_status, (format((a.BookedPenalIntere),2,'en_IN')) as  BookedPenalIntere,  " +
                        " (format((a.TotalPenalInterestR),2,'en_IN')) as TotalPenalInterestR,(format((a.BookedNotDuePenalInter),2,'en_IN')) as BookedNotDuePenalInter from lgl_tmp_tmisdata a " +
                        " left join agr_mst_tcontact b on a.urn = b.urn " +
                        " where a.urn in ('" + individual_urn.Replace(",", "','") + "')" +
                        " union " +
                        " select a.customer_name, '-' as stakeholder_type, 'Group' as applicant_type, a.urn, a.account_no,(format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt, " +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN 'Active'  " +
                        " WHEN a.ac_status = '2' THEN 'Closed' " +
                        " WHEN a.ac_status = '3' THEN 'Frozen' " +
                        " END as ac_status, (format((a.BookedPenalIntere),2,'en_IN')) as  BookedPenalIntere,  " +
                        " (format((a.TotalPenalInterestR),2,'en_IN')) as TotalPenalInterestR,(format((a.BookedNotDuePenalInter),2,'en_IN')) as BookedNotDuePenalInter from lgl_tmp_tmisdata a " +
                        " left join agr_mst_tgroup b on a.urn = b.group_urn " +
                        " where a.urn in ('" + group_urn.Replace(",", "','") + "')";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var pending_List = new List<alldatamodified_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        pending_List.Add(new alldatamodified_List
                        {
                            urn = (dr_datarow["urn"].ToString()),
                            lan = (dr_datarow["account_no"].ToString()),
                            account_status = (dr_datarow["ac_status"].ToString()),
                            penal_interest_raised = (dr_datarow["BookedPenalIntere"].ToString().Replace("0.00", "0")),
                            penal_interest_collected = (dr_datarow["TotalPenalInterestR"].ToString().Replace("0.00", "0")),
                            penal_interest_pending = (dr_datarow["BookedNotDuePenalInter"].ToString().Replace("0.00", "0")),
                            customer_name = (dr_datarow["customer_name"].ToString()),
                            stackholder_type = (dr_datarow["stakeholder_type"].ToString()),
                            company_type = (dr_datarow["applicant_type"].ToString()),
                            Net_Payoff_Amt = (dr_datarow["Net_Payoff_Amt"].ToString().Replace("0.00", "0"))
                        });
                    }
                    values.alldatamodified_List = pending_List;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }
        }

        //TDS View
        public void DaGetTDSView(string application_gid, MdlMstApplicationView values)
        {

            msSQL = " select group_concat(ifnull(c.urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tinstitution c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            institution_urn = objdbconn.GetExecuteScalar(msSQL);
            institution_urn = institution_urn.Replace(",,", ",");
            institution_urn = institution_urn.Replace(",,,", ",");
            institution_urn = institution_urn.TrimEnd(',');

            msSQL = " select group_concat(ifnull(c.urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tcontact c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            individual_urn = objdbconn.GetExecuteScalar(msSQL);
            individual_urn = individual_urn.Replace(",,", ",");
            individual_urn = individual_urn.Replace(",,,", ",");
            individual_urn = individual_urn.TrimEnd(',');

            msSQL = " select group_concat(ifnull(c.group_urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tgroup c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            group_urn = objdbconn.GetExecuteScalar(msSQL);
            group_urn = group_urn.Replace(",,", ",");
            group_urn = group_urn.Replace(",,,", ",");
            group_urn = group_urn.TrimEnd(',');
            try
            {
                msSQL = "select a.customer_name, c.stakeholder_type, 'Institution' as applicant_type, a.urn, a.account_no, (format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt, " +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN ' Active' " +
                        " WHEN a.ac_status = '2' THEN ' Closed' " +
                        " WHEN a.ac_status = '3' THEN ' Frozen' " +
                         "END as ac_status, a.Uncollected_Interest, " +
                        "  (format((a.ledger * -1),2,'en_IN')) as ledger from lgl_tmp_tmisdata a " +
                         "left join agr_mst_tinstitution c on c.urn = a.urn " +
                         " where a.urn in ('" + institution_urn.Replace(",", "','") + "')" +
                        " union " +
                        " select a.customer_name, c.stakeholder_type,  'Individual' as applicant_type,a.urn, a.account_no, (format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt , " +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN ' Active' " +
                        " WHEN a.ac_status = '2' THEN ' Closed' " +
                        " WHEN a.ac_status = '3' THEN ' Frozen' " +
                        " END as ac_status, a.Uncollected_Interest,    " +
                        "  (format((a.ledger * -1),2,'en_IN')) as ledger from lgl_tmp_tmisdata a " +
                        " left join agr_mst_tcontact c on c.urn = a.urn " +
                        " where a.urn in ('" + individual_urn.Replace(",", "','") + "')" +
                        " union " +
                         "select a.customer_name, '-' as stakeholder_type, 'Group' as applicant_type, a.urn, a.account_no,  (format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt," +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN ' Active' " +
                        " WHEN a.ac_status = '2' THEN ' Closed' " +
                        "  WHEN a.ac_status = '3' THEN ' Frozen' " +
                         "END as ac_status, a.Uncollected_Interest,    " +
                         " (format((a.ledger * -1),2,'en_IN')) as ledger from lgl_tmp_tmisdata a " +
                         "left join agr_mst_tgroup c on c.group_urn = a.urn " +
                         " where a.urn in ('" + group_urn.Replace(",", "','") + "')";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var tds_List = new List<alldatamodified_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        tds_List.Add(new alldatamodified_List
                        {
                            urn = (dr_datarow["urn"].ToString()),
                            lan = (dr_datarow["account_no"].ToString()),
                            account_status = (dr_datarow["ac_status"].ToString()),
                            uncollected_interest = (dr_datarow["Uncollected_Interest"].ToString()),
                            ledger_balance = (dr_datarow["ledger"].ToString().Replace("-0.00", "0")),
                            customer_name = (dr_datarow["customer_name"].ToString()),
                            stackholder_type = (dr_datarow["stakeholder_type"].ToString()),
                            company_type = (dr_datarow["applicant_type"].ToString()),
                            Net_Payoff_Amt = (dr_datarow["Net_Payoff_Amt"].ToString().Replace("0.00", "0"))
                        });
                    }
                    values.alldatamodified_List = tds_List;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }
        }

        //NOC View
        public void DaGetNOCView(string application_gid, MdlMstApplicationView values)
        {
            msSQL = " select group_concat(ifnull(c.urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tinstitution c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            institution_urn = objdbconn.GetExecuteScalar(msSQL);
            institution_urn = institution_urn.Replace(",,", ",");
            institution_urn = institution_urn.Replace(",,,", ",");
            institution_urn = institution_urn.TrimEnd(',');

            msSQL = " select group_concat(ifnull(c.urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tcontact c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            individual_urn = objdbconn.GetExecuteScalar(msSQL);
            individual_urn = individual_urn.Replace(",,", ",");
            individual_urn = individual_urn.Replace(",,,", ",");
            individual_urn = individual_urn.TrimEnd(',');

            msSQL = " select group_concat(ifnull(c.group_urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tgroup c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            group_urn = objdbconn.GetExecuteScalar(msSQL);
            group_urn = group_urn.Replace(",,", ",");
            group_urn = group_urn.Replace(",,,", ",");
            group_urn = group_urn.TrimEnd(',');

            try
            {
                msSQL = "select a.customer_name, c.stakeholder_type, 'Institution' as applicant_type, a.urn, a.account_no,(format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt ," +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN ' Active' " +
                        " WHEN a.ac_status = '2' THEN ' Closed' " +
                        " WHEN a.ac_status = '3' THEN ' Frozen' " +
                         "END as ac_status, a.ac_closed_date, " +
                        "  (format((a.ledger * -1),2,'en_IN')) as ledger from lgl_tmp_tmisdata a " +
                         "left join agr_mst_tinstitution c on c.urn = a.urn " +
                         " where a.urn in ('" + institution_urn.Replace(",", "','") + "')" +
                        " union " +
                        " select a.customer_name, c.stakeholder_type,  'Individual' as applicant_type,a.urn, a.account_no,(format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt,  " +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN ' Active' " +
                        " WHEN a.ac_status = '2' THEN ' Closed' " +
                        " WHEN a.ac_status = '3' THEN ' Frozen' " +
                        " END as ac_status, a.Uncollected_Interest,    " +
                        "  (format((a.ledger * -1),2,'en_IN')) as ledger from lgl_tmp_tmisdata a " +
                        " left join agr_mst_tcontact c on c.urn = a.urn " +
                        " where a.urn in ('" + individual_urn.Replace(",", "','") + "')" +
                        " union " +
                         "select a.customer_name, '-' as stakeholder_type, 'Group' as applicant_type, a.urn, a.account_no, (format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt , " +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN ' Active' " +
                        " WHEN a.ac_status = '2' THEN ' Closed' " +
                        "  WHEN a.ac_status = '3' THEN ' Frozen' " +
                         "END as ac_status, a.Uncollected_Interest,    " +
                         " (format((a.ledger * -1),2,'en_IN')) as ledger from lgl_tmp_tmisdata a " +
                         "left join agr_mst_tgroup c on c.group_urn = a.urn " +
                         " where a.urn in ('" + group_urn.Replace(",", "','") + "')";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var tds_List = new List<alldatamodified_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        if (!String.IsNullOrEmpty(dr_datarow["ac_closed_date"].ToString()))
                        { 
                            if(((dr_datarow["ac_closed_date"].ToString()) == "0") || ((dr_datarow["ac_closed_date"].ToString()).Contains(".")))
                            {  
                                tds_List.Add(new alldatamodified_List
                                {
                                    urn = (dr_datarow["urn"].ToString()),
                                    lan = (dr_datarow["account_no"].ToString()),
                                    account_status = (dr_datarow["ac_status"].ToString()),
                                    ac_closed_date = (dr_datarow["ac_closed_date"].ToString()),
                                    ledger_balance = (dr_datarow["ledger"].ToString().Replace("-0.00", "0")),
                                    customer_name = (dr_datarow["customer_name"].ToString()),
                                    stackholder_type = (dr_datarow["stakeholder_type"].ToString()),
                                    company_type = (dr_datarow["applicant_type"].ToString()),
                                    Net_Payoff_Amt = (dr_datarow["Net_Payoff_Amt"].ToString().Replace("0.00", "0"))
                                });
                            }
                            else
                            {
                                tds_List.Add(new alldatamodified_List
                                {
                                    urn = (dr_datarow["urn"].ToString()),
                                    lan = (dr_datarow["account_no"].ToString()),
                                    account_status = (dr_datarow["ac_status"].ToString()),
                                    ac_closed_date = Convert.ToDateTime(dr_datarow["ac_closed_date"].ToString()).ToString("dd/MM/yyyy"),
                                    ledger_balance = (dr_datarow["ledger"].ToString().Replace("-0.00", "0")),
                                    customer_name = (dr_datarow["customer_name"].ToString()),
                                    stackholder_type = (dr_datarow["stakeholder_type"].ToString()),
                                    company_type = (dr_datarow["applicant_type"].ToString()),
                                    Net_Payoff_Amt = (dr_datarow["Net_Payoff_Amt"].ToString().Replace("0.00", "0"))
                                });
                            }

                        }
                        else
                        {
                            tds_List.Add(new alldatamodified_List
                            {
                                urn = (dr_datarow["urn"].ToString()),
                                lan = (dr_datarow["account_no"].ToString()),
                                account_status = (dr_datarow["ac_status"].ToString()),
                                ac_closed_date = (dr_datarow["ac_closed_date"].ToString()),
                                ledger_balance = (dr_datarow["ledger"].ToString().Replace("-0.00", "0")),
                                customer_name = (dr_datarow["customer_name"].ToString()),
                                stackholder_type = (dr_datarow["stakeholder_type"].ToString()),
                                company_type = (dr_datarow["applicant_type"].ToString()),
                                Net_Payoff_Amt = (dr_datarow["Net_Payoff_Amt"].ToString().Replace("0.00", "0"))
                            });
                        }
                    }
                    values.alldatamodified_List = tds_List;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "success";
            }
            catch (Exception ex)
            {
                values.status = false;
                 values.message = ex.ToString();
            }
        }
        //Penality View
        public void DaGetNDCView(string application_gid, MdlMstApplicationView values)
        {
            msSQL = " select group_concat(ifnull(c.urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tinstitution c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            institution_urn = objdbconn.GetExecuteScalar(msSQL);
            institution_urn = institution_urn.Replace(",,", ",");
            institution_urn = institution_urn.Replace(",,,", ",");
            institution_urn = institution_urn.TrimEnd(',');

            msSQL = " select group_concat(ifnull(c.urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tcontact c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            individual_urn = objdbconn.GetExecuteScalar(msSQL);
            individual_urn = individual_urn.Replace(",,", ",");
            individual_urn = individual_urn.Replace(",,,", ",");
            individual_urn = individual_urn.TrimEnd(',');

            msSQL = " select group_concat(ifnull(c.group_urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tgroup c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            group_urn = objdbconn.GetExecuteScalar(msSQL);
            group_urn = group_urn.Replace(",,", ",");
            group_urn = group_urn.Replace(",,,", ",");
            group_urn = group_urn.TrimEnd(',');


            try
            {
                msSQL = "select a.customer_name, c.stakeholder_type, 'Institution' as applicant_type, a.urn, a.account_no,(format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt, " +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN ' Active' " +
                        " WHEN a.ac_status = '2' THEN ' Closed' " +
                        " WHEN a.ac_status = '3' THEN ' Frozen' " +
                         "END as ac_status, a.ac_closed_date, " +
                        "  (format((a.ledger * -1),2,'en_IN')) as ledger from lgl_tmp_tmisdata a " +
                         "left join agr_mst_tinstitution c on c.urn = a.urn " +
                         "left join agr_mst_tapplication b on b.application_gid = c.application_gid " +
                         " where a.urn in ('" + institution_urn.Replace(",", "','") + "')" +
                        " union " +
                        " select a.customer_name, c.stakeholder_type,  'Individual' as applicant_type,a.urn, a.account_no, (format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt  ," +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN ' Active' " +
                        " WHEN a.ac_status = '2' THEN ' Closed' " +
                        " WHEN a.ac_status = '3' THEN ' Frozen' " +
                        " END as ac_status, a.Uncollected_Interest,    " +
                        "  (format((a.ledger * -1),2,'en_IN')) as ledger from lgl_tmp_tmisdata a " +
                        " left join agr_mst_tcontact c on c.urn = a.urn " +
                        " where a.urn in ('" + individual_urn.Replace(",", "','") + "')" +
                        " union " +
                         "select a.customer_name, '-' as stakeholder_type, 'Group' as applicant_type, a.urn, a.account_no,  (format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt, " +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN ' Active' " +
                        " WHEN a.ac_status = '2' THEN ' Closed' " +
                        "  WHEN a.ac_status = '3' THEN ' Frozen' " +
                         "END as ac_status, a.Uncollected_Interest,    " +
                         " (format((a.ledger * -1),2,'en_IN')) as ledger from lgl_tmp_tmisdata a " +
                         "left join agr_mst_tgroup c on c.group_urn = a.urn " +
                         " where a.urn in ('" + group_urn.Replace(",", "','") + "')";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var tds_List = new List<alldatamodified_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                        if (!String.IsNullOrEmpty(dr_datarow["ac_closed_date"].ToString()))
                        {
                            if (((dr_datarow["ac_closed_date"].ToString()) == "0") || ((dr_datarow["ac_closed_date"].ToString()).Contains(".")))
                            {
                                tds_List.Add(new alldatamodified_List
                                {
                                    urn = (dr_datarow["urn"].ToString()),
                                    lan = (dr_datarow["account_no"].ToString()),
                                    account_status = (dr_datarow["ac_status"].ToString()),
                                    ac_closed_date = (dr_datarow["ac_closed_date"].ToString()),
                                    ledger_balance = (dr_datarow["ledger"].ToString().Replace("-0.00", "0")),
                                    customer_name = (dr_datarow["customer_name"].ToString()),
                                    stackholder_type = (dr_datarow["stakeholder_type"].ToString()),
                                    company_type = (dr_datarow["applicant_type"].ToString()),
                                    Net_Payoff_Amt = (dr_datarow["Net_Payoff_Amt"].ToString().Replace("0.00", "0"))
                                });
                            }
                            else
                            {
                                tds_List.Add(new alldatamodified_List
                                {
                                    urn = (dr_datarow["urn"].ToString()),
                                    lan = (dr_datarow["account_no"].ToString()),
                                    account_status = (dr_datarow["ac_status"].ToString()),
                                    ac_closed_date = Convert.ToDateTime(dr_datarow["ac_closed_date"].ToString()).ToString("dd/MM/yyyy"),
                                    ledger_balance = (dr_datarow["ledger"].ToString().Replace("-0.00", "0")),
                                    customer_name = (dr_datarow["customer_name"].ToString()),
                                    stackholder_type = (dr_datarow["stakeholder_type"].ToString()),
                                    company_type = (dr_datarow["applicant_type"].ToString()),
                                    Net_Payoff_Amt = (dr_datarow["Net_Payoff_Amt"].ToString().Replace("0.00", "0"))
                                });
                            }

                        }
                        else
                        {
                            tds_List.Add(new alldatamodified_List
                            {
                                urn = (dr_datarow["urn"].ToString()),
                                lan = (dr_datarow["account_no"].ToString()),
                                account_status = (dr_datarow["ac_status"].ToString()),
                                ac_closed_date = (dr_datarow["ac_closed_date"].ToString()),
                                ledger_balance = (dr_datarow["ledger"].ToString().Replace("-0.00", "0")),
                                customer_name = (dr_datarow["customer_name"].ToString()),
                                stackholder_type = (dr_datarow["stakeholder_type"].ToString()),
                                company_type = (dr_datarow["applicant_type"].ToString()),
                                Net_Payoff_Amt = (dr_datarow["Net_Payoff_Amt"].ToString().Replace("0.00", "0"))
                            });
                        }
                    values.alldatamodified_List = tds_List;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
            }
        }
        //Penality View
        public void DaGetMoratoriumView(string application_gid, MdlMstApplicationView values)
        {
            msSQL = " select group_concat(ifnull(c.urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tinstitution c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            institution_urn = objdbconn.GetExecuteScalar(msSQL);
            institution_urn = institution_urn.Replace(",,", ",");
            institution_urn = institution_urn.Replace(",,,", ",");
            institution_urn = institution_urn.TrimEnd(',');

            msSQL = " select group_concat(ifnull(c.urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tcontact c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            individual_urn = objdbconn.GetExecuteScalar(msSQL);
            individual_urn = individual_urn.Replace(",,", ",");
            individual_urn = individual_urn.Replace(",,,", ",");
            individual_urn = individual_urn.TrimEnd(',');

            msSQL = " select group_concat(ifnull(c.group_urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tgroup c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            group_urn = objdbconn.GetExecuteScalar(msSQL);
            group_urn = group_urn.Replace(",,", ",");
            group_urn = group_urn.Replace(",,,", ",");
            group_urn = group_urn.TrimEnd(',');

            try
            {
                msSQL = "select a.customer_name, c.stakeholder_type, 'Institution' as applicant_type, a.urn, a.account_no, (format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt, " +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN ' Active' " +
                        " WHEN a.ac_status = '2' THEN ' Closed' " +
                        " WHEN a.ac_status = '3' THEN ' Frozen' " +
                         "END as ac_status, a.Moratorium_Tenure, a.Moratorium_Interest," +
                        "  (format((a.ledger * -1),2,'en_IN')) as ledger from lgl_tmp_tmisdata a " +
                         "left join agr_mst_tinstitution c on c.urn = a.urn " +
                         " where a.urn in ('" + institution_urn.Replace(",", "','") + "')" +
                        " union " +
                        " select a.customer_name, c.stakeholder_type, 'Individual' as applicant_type,a.urn, a.account_no,  (format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt, " +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN ' Active' " +
                        " WHEN a.ac_status = '2' THEN ' Closed' " +
                        " WHEN a.ac_status = '3' THEN ' Frozen' " +
                        " END as ac_status, a.Moratorium_Tenure, a.Moratorium_Interest, " +
                        "  (format((a.ledger * -1),2,'en_IN')) as ledger from lgl_tmp_tmisdata a " +
                        " left join agr_mst_tcontact c on c.urn = a.urn " +
                        " where a.urn in ('" + individual_urn.Replace(",", "','") + "')" +
                        " union " +
                         "select a.customer_name, '-' as stakeholder_type, 'Group' as applicant_type, a.urn, a.account_no, (format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt , " +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN ' Active' " +
                        " WHEN a.ac_status = '2' THEN ' Closed' " +
                        "  WHEN a.ac_status = '3' THEN ' Frozen' " +
                         "END as ac_status, a.Moratorium_Tenure, a.Moratorium_Interest, " +
                         " (format((a.ledger * -1),2,'en_IN')) as ledger from lgl_tmp_tmisdata a " +
                         "left join agr_mst_tgroup c on c.group_urn = a.urn " +
                         " where a.urn in ('" + group_urn.Replace(",", "','") + "')";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var tds_List = new List<alldatamodified_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        tds_List.Add(new alldatamodified_List
                        {
                            urn = (dr_datarow["urn"].ToString()),
                            lan = (dr_datarow["account_no"].ToString()),
                            account_status = (dr_datarow["ac_status"].ToString()),
                            ledger_balance = (dr_datarow["ledger"].ToString().Replace("-0.00", "0")),
                            customer_name = (dr_datarow["customer_name"].ToString()),
                            stackholder_type = (dr_datarow["stakeholder_type"].ToString()),
                            company_type = (dr_datarow["applicant_type"].ToString()),
                            Moratorium_Tenure = (dr_datarow["Moratorium_Tenure"].ToString()),
                            Moratorium_Interest = (dr_datarow["Moratorium_Interest"].ToString()),
                            Net_Payoff_Amt = (dr_datarow["Net_Payoff_Amt"].ToString().Replace("0.00", "0"))
                        });
                    }
                    values.alldatamodified_List = tds_List;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }
        }
        public void DaBankAccountDetailsList(string application_gid, MdlMstBankAccountDetails values)
        {
            msSQL = " select a.group_gid,a.group2bank_gid,ifsc_code,bank_accountno,accountholder_name,bank_name,bank_branch,group_name from agr_mst_tgroup2bank a " +
                    " left join agr_mst_tgroup b on a.group_gid = b.group_gid " +
                    " left join agr_mst_tapplication c on c.application_gid = b.application_gid " +
                    " where c.application_gid = '" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstbankacctdtl_list = new List<mstbankacctdtl_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstbankacctdtl_list.Add(new mstbankacctdtl_list
                    {
                        group2bank_gid = (dr_datarow["group2bank_gid"].ToString()),
                        group_gid = (dr_datarow["group_gid"].ToString()),
                        ifsc_code = (dr_datarow["ifsc_code"].ToString()),
                        bank_accountno = (dr_datarow["bank_accountno"].ToString()),
                        accountholder_name = (dr_datarow["accountholder_name"].ToString()),
                        bank_name = (dr_datarow["bank_name"].ToString()),
                        bank_branch = (dr_datarow["bank_branch"].ToString()),
                        group_name = (dr_datarow["group_name"].ToString())
                    });
                }
                values.mstbankacctdtl_list = getmstbankacctdtl_list;
            }
            dt_datatable.Dispose();

            msSQL = "select creditbankdtl_gid,credit_gid,application_gid,bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_name," +
                 " bankaccounttype_gid,bankaccounttype_name,bankaccount_number,confirmbankaccountnumber,joinaccount_status,joinaccount_name," +
                 " chequebook_status,DATE_FORMAT(accountopen_date,'%d-%m-%Y') as accountopen_date, bankaccount_name as accountholder_name, " +
                 " joinaccount_name as jointaccountholder_name" +
                 " from agr_mst_tcreditbankdtl where application_gid= '" + application_gid + "' and credit_gid like 'APIN%'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinstitutionbankacc_list = new List<institutionbankacc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getinstitutionbankacc_list.Add(new institutionbankacc_list
                    {
                        creditbankdtl_gid = dt["creditbankdtl_gid"].ToString(),
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        bank_address = dt["bank_address"].ToString(),
                        micr_code = dt["micr_code"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        bankaccount_name = dt["bankaccount_name"].ToString(),
                        bankaccounttype_name = dt["bankaccounttype_name"].ToString(),
                        bankaccount_number = dt["bankaccount_number"].ToString(),
                        confirmbankaccountnumber = dt["confirmbankaccountnumber"].ToString(),
                        joinaccount_status = dt["joinaccount_status"].ToString(),
                        joinaccount_name = dt["joinaccount_name"].ToString(),
                        chequebook_status = dt["chequebook_status"].ToString(),
                        accountopen_date = dt["accountopen_date"].ToString(),
                        accountholder_name = dt["accountholder_name"].ToString(),
                        jointaccountholder_name = dt["jointaccountholder_name"].ToString(),
                    });
                    values.institutionbankacc_list = getinstitutionbankacc_list;
                }
            }
            dt_datatable.Dispose();
            msSQL = "select creditbankdtl_gid,credit_gid,application_gid,bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_name," +
                 " bankaccounttype_gid,bankaccounttype_name,bankaccount_number,confirmbankaccountnumber,joinaccount_status,joinaccount_name," +
                 " chequebook_status,DATE_FORMAT(accountopen_date,'%d-%m-%Y') as accountopen_date, bankaccount_name as accountholder_name, " +
                 " joinaccount_name as jointaccountholder_name" +
                 " from agr_mst_tcreditbankdtl where application_gid= '" + application_gid + "' and credit_gid like 'CTCT%'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getindividualbankacc_list = new List<individualbankacc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getindividualbankacc_list.Add(new individualbankacc_list
                    {
                        creditbankdtl_gid = dt["creditbankdtl_gid"].ToString(),
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        bank_address = dt["bank_address"].ToString(),
                        micr_code = dt["micr_code"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        bankaccount_name = dt["bankaccount_name"].ToString(),
                        bankaccounttype_name = dt["bankaccounttype_name"].ToString(),
                        bankaccount_number = dt["bankaccount_number"].ToString(),
                        confirmbankaccountnumber = dt["confirmbankaccountnumber"].ToString(),
                        joinaccount_status = dt["joinaccount_status"].ToString(),
                        joinaccount_name = dt["joinaccount_name"].ToString(),
                        chequebook_status = dt["chequebook_status"].ToString(),
                        accountopen_date = dt["accountopen_date"].ToString(),
                        accountholder_name = dt["accountholder_name"].ToString(),
                        jointaccountholder_name = dt["jointaccountholder_name"].ToString(),
                    });
                    values.individualbankacc_list = getindividualbankacc_list;
                }
            }
            dt_datatable.Dispose();
            msSQL = "select creditbankdtl_gid,credit_gid,application_gid,bank_name,branch_name,bank_address,micr_code,ifsc_code,bankaccount_name," +
                " bankaccounttype_gid,bankaccounttype_name,bankaccount_number,confirmbankaccountnumber,joinaccount_status,joinaccount_name," +
                " chequebook_status,DATE_FORMAT(accountopen_date,'%d-%m-%Y') as accountopen_date, bankaccount_name as accountholder_name, " +
                " joinaccount_name as jointaccountholder_name" +
                " from agr_mst_tcreditbankdtl where application_gid= '" + application_gid + "' and credit_gid like 'GRUP%'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgroupbankacc_list = new List<groupbankacc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getgroupbankacc_list.Add(new groupbankacc_list
                    {
                        creditbankdtl_gid = dt["creditbankdtl_gid"].ToString(),
                        bank_name = dt["bank_name"].ToString(),
                        branch_name = dt["branch_name"].ToString(),
                        bank_address = dt["bank_address"].ToString(),
                        micr_code = dt["micr_code"].ToString(),
                        ifsc_code = dt["ifsc_code"].ToString(),
                        bankaccount_name = dt["bankaccount_name"].ToString(),
                        bankaccounttype_name = dt["bankaccounttype_name"].ToString(),
                        bankaccount_number = dt["bankaccount_number"].ToString(),
                        confirmbankaccountnumber = dt["confirmbankaccountnumber"].ToString(),
                        joinaccount_status = dt["joinaccount_status"].ToString(),
                        joinaccount_name = dt["joinaccount_name"].ToString(),
                        chequebook_status = dt["chequebook_status"].ToString(),
                        accountopen_date = dt["accountopen_date"].ToString(),
                        accountholder_name = dt["accountholder_name"].ToString(),
                        jointaccountholder_name = dt["jointaccountholder_name"].ToString(),
                    });
                    values.groupbankacc_list = getgroupbankacc_list;
                }
            }
            dt_datatable.Dispose();
        }
        //Deviation View
        public void DaGetDeviationView(string application_gid, MdlMstApplicationView values)
        {
            msSQL = " select group_concat(ifnull(c.urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tinstitution c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            institution_urn = objdbconn.GetExecuteScalar(msSQL);
            institution_urn = institution_urn.Replace(",,", ",");
            institution_urn = institution_urn.Replace(",,,", ",");
            institution_urn = institution_urn.TrimEnd(',');

            msSQL = " select group_concat(ifnull(c.urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tcontact c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            individual_urn = objdbconn.GetExecuteScalar(msSQL);
            individual_urn = individual_urn.Replace(",,", ",");
            individual_urn = individual_urn.Replace(",,,", ",");
            individual_urn = individual_urn.TrimEnd(',');

            msSQL = " select group_concat(ifnull(c.group_urn,''))   from agr_mst_tapplication a " +
                    " left join agr_mst_tgroup c on c.application_gid = a.application_gid " +
                    " where a.application_gid = '" + application_gid + "'";
            group_urn = objdbconn.GetExecuteScalar(msSQL);
            group_urn = group_urn.Replace(",,", ",");
            group_urn = group_urn.Replace(",,,", ",");
            group_urn = group_urn.TrimEnd(',');

            try
            {
                msSQL = " select a.customer_name, b.stakeholder_type, 'Institution' as applicant_type, a.urn,a.account_no,(format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt, " +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN ' Active' " +
                        " WHEN a.ac_status = '2' THEN ' Closed' " +
                        " WHEN a.ac_status = '3' THEN ' Frozen' " +
                        "END as ac_status,(format((a.ledger * -1),2,'en_IN')) as ledger,a.rbiold_oddays,a.late_charge,  " +
                        " a.max_dpd,a.Vertical,a.lastdemandrundate from lgl_tmp_tmisdata a " +
                        " left join agr_mst_tinstitution b on a.urn = b.urn " +
                        " where a.urn in ('" + institution_urn.Replace(",", "','") + "')" +
                        " union " +
                        " select a.customer_name, b.stakeholder_type, 'Individual' as applicant_type, a.urn,a.account_no,(format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt, " +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN ' Active' " +
                        " WHEN a.ac_status = '2' THEN ' Closed' " +
                        "  WHEN a.ac_status = '3' THEN ' Frozen' " +
                         "END as ac_status,(format((a.ledger * -1),2,'en_IN')) as ledger,a.rbiold_oddays, a.late_charge, " +
                        " a.max_dpd,a.Vertical,a.lastdemandrundate from lgl_tmp_tmisdata a " +
                        " left join agr_mst_tcontact b on a.urn = b.urn " +
                        " where a.urn in ('" + individual_urn.Replace(",", "','") + "')" +
                        " union " +
                        " select a.customer_name, '-' as stakeholder_type, 'Group' as applicant_type, a.urn,a.account_no,(format((a.Net_Payoff_Amt ),2,'en_IN')) as Net_Payoff_Amt, " +
                        " CASE " +
                        " WHEN a.ac_status = '0' THEN ' Active' " +
                        " WHEN a.ac_status = '2' THEN ' Closed' " +
                        "  WHEN a.ac_status = '3' THEN ' Frozen' " +
                         "END as ac_status,(format((a.ledger * -1),2,'en_IN')) as ledger,a.rbiold_oddays,a.late_charge,  " +
                        " a.max_dpd,a.Vertical,a.lastdemandrundate from lgl_tmp_tmisdata a " +
                        " left join agr_mst_tgroup b on a.urn = b.group_urn " +
                        " where a.urn in ('" + group_urn.Replace(",", "','") + "')";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var loandetails_List = new List<alldatamodified_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        if (!String.IsNullOrEmpty(dr_datarow["lastdemandrundate"].ToString()))
                        {
                            loandetails_List.Add(new alldatamodified_List
                            {
                                urn = (dr_datarow["urn"].ToString()),
                                lan = (dr_datarow["account_no"].ToString()),
                                account_status = (dr_datarow["ac_status"].ToString()),
                                ledger_balance = (dr_datarow["ledger"].ToString().Replace("-0.00", "0")),
                                maximum_od_day = (dr_datarow["max_dpd"].ToString()),
                                rbi_od_days = (dr_datarow["rbiold_oddays"].ToString()),
                                vertical = (dr_datarow["Vertical"].ToString()),
                                next_due_date = Convert.ToDateTime(dr_datarow["lastdemandrundate"].ToString()).ToString("dd/MM/yyyy"),
                                customer_name = (dr_datarow["customer_name"].ToString()),
                                stackholder_type = (dr_datarow["stakeholder_type"].ToString()),
                                company_type = (dr_datarow["applicant_type"].ToString()),
                                Net_Payoff_Amt = (dr_datarow["Net_Payoff_Amt"].ToString().Replace("0.00", "0")),
                                late_charge = (dr_datarow["late_charge"].ToString())
                            });
                        }
                        else
                        {
                            loandetails_List.Add(new alldatamodified_List
                            {
                                urn = (dr_datarow["urn"].ToString()),
                                lan = (dr_datarow["account_no"].ToString()),
                                account_status = (dr_datarow["ac_status"].ToString()),
                                ledger_balance = (dr_datarow["ledger"].ToString().Replace("-0.00", "0")),
                                maximum_od_day = (dr_datarow["max_dpd"].ToString()),
                                rbi_od_days = (dr_datarow["rbiold_oddays"].ToString()),
                                vertical = (dr_datarow["Vertical"].ToString()),
                                next_due_date = (dr_datarow["lastdemandrundate"].ToString()),
                                customer_name = (dr_datarow["customer_name"].ToString()),
                                stackholder_type = (dr_datarow["stakeholder_type"].ToString()),
                                company_type = (dr_datarow["applicant_type"].ToString()),
                                Net_Payoff_Amt = (dr_datarow["Net_Payoff_Amt"].ToString().Replace("0.00", "0")),
                                late_charge = (dr_datarow["late_charge"].ToString())
                            });
                        }
                    }
                    values.alldatamodified_List = loandetails_List;
                }
                dt_datatable.Dispose();
                values.status = true;
                values.message = "success";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaGetlabels(string credit_gid, MdlCreditView values)
        {

            msSQL = " select company_name, stakeholder_type from agr_mst_tinstitution " +

               " where institution_gid = '" + credit_gid + "' " ;

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customer_name = objODBCDatareader["company_name"].ToString();
                values.stakeholder_type = objODBCDatareader["stakeholder_type"].ToString();

            }

            else if (objODBCDatareader.HasRows == false)
            {

                msSQL = " select concat_ws(' ', first_name, last_name, middle_name) as individual_name, stakeholder_type from agr_mst_tcontact " +

              " where contact_gid = '" + credit_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.customer_name = objODBCDatareader["individual_name"].ToString();
                    values.stakeholder_type = objODBCDatareader["stakeholder_type"].ToString();

                }
            }

            objODBCDatareader.Close();

        }


    }
}