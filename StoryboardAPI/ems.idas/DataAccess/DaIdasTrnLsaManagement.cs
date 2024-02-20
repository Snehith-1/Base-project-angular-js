using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.IO;
using ems.utilities.Functions;
using ems.idas.Models;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using ems.storage.Functions;

namespace ems.idas.DataAccess
{
    public class DaIdasTrnLsaManagement
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDataReader, objODBCDataReader1, objODBCDatareader, objreader;
        DataTable dt_datatable, dt_tLSA;
        string msSQL, msGetGID, msGETGIDDoc, msGetGidREF, msGetGIDCOL;
        int mnResult;
        string lsmonthwise, lsdaywise, lssequencecode,lslimitref_no;
        int lsmonthcount, lsdaycount;
        HttpPostedFile httpPostedFile;
        string lspath, lsdocument_type;
        string ccmail_id = string.Empty;
        string frommail_id = string.Empty;
        string tomail_address = string.Empty;
        string lsmaker_name = string.Empty;
        string sub, body;
        string msGID = string.Empty;
        int ls_port;
        string  ls_username, ls_password, ls_server, lsinterchangeability,lsexisting_limit;
        string lsapplicable_condition;
        string lsUpdatedBy;
        string lsUpdatedDate;
        String[] lsCCReceipients;

        public bool DaGetCustomerdtl(string customer_gid, MdlLsaManagement values)
        {
            msSQL = " select a.state,a.customer_urn,a.address, concat(a.address2, a.state, '-', a.postalcode, ' ', a.country) as address2," +
                     " a.vertical_code,a.gst_number,a.pan_number,a.constitution_name,a.major_corporate,a.creditmanager_gid,a.cluster_manager_gid,a.zonal_head,"+
                     " a.business_head,a.relationship_manager,concat(a.creditmgmt_name, ' / ', b.employee_emailid) as creditmgmt_name,"+
                     " concat(a.cluster_manager_name, ' / ', c.employee_emailid) as cluster_manager_name,"+
                     " concat(a.zonal_name, ' / ', d.employee_emailid) as zonal_name,concat(a.businesshead_name, ' / ', e.employee_emailid) as businesshead_name,"+
                     " concat(a.relationshipmgmt_name, ' / ', f.employee_emailid) as relationshipmgmt_name"+
                     " from ocs_mst_tcustomer a"+
                     " left join hrm_mst_temployee b on a.creditmanager_gid = b.employee_gid" +
                     " left join hrm_mst_temployee c on a.cluster_manager_gid = c.employee_gid" +
                     " left join hrm_mst_temployee d on a.zonal_head = d.employee_gid" +
                     " left join hrm_mst_temployee e on a.business_head = e.employee_gid" +
                     " left join hrm_mst_temployee f on a.relationship_manager = f.employee_gid" +
                     " where customer_gid='" + customer_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.customer_urn = objODBCDataReader["customer_urn"].ToString();
                values.address = objODBCDataReader["address2"].ToString();
                values.business_head = objODBCDataReader["businesshead_name"].ToString();
                values.zonal_head = objODBCDataReader["zonal_name"].ToString();
                values.cluster_head = objODBCDataReader["cluster_manager_name"].ToString();
                values.credit_manager = objODBCDataReader["creditmgmt_name"].ToString();
                values.rm_name = objODBCDataReader["relationshipmgmt_name"].ToString();
                values.customer_location = objODBCDataReader["state"].ToString();
                values.vertical = objODBCDataReader["vertical_code"].ToString();
                values.gst_no = objODBCDataReader["gst_number"].ToString();
                values.pan_no = objODBCDataReader["pan_number"].ToString();
                values.constitution = objODBCDataReader["constitution_name"].ToString();
                values.major_corporate = objODBCDataReader["major_corporate"].ToString();
                values.address1 = objODBCDataReader["address"].ToString();
            }
            objODBCDataReader.Close();
            values.status = true;
            return true;
        }

        public bool DaGetcustomer2sanction(string customer_gid, MdlLsaManagement values)
        {
            msSQL = " select sanction_refno,customer2sanction_gid from ocs_mst_tcustomer2sanction where customer_gid='" + customer_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer = new List<customer2sanction_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomer.Add(new customer2sanction_list
                    {
                        sanctionref_no = (dr_datarow["sanction_refno"].ToString()),
                        customer2sanction_gid = (dr_datarow["customer2sanction_gid"].ToString()),
                    });
                }
                values.customer2sanction_list = getcustomer;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetsanctionmis_dtl(string customer2sanction_gid, Mdlcustomer2sanction values)
        {
            msSQL = " select date_format(sanction_date,'%d-%m-%Y') as sanction_date,purpose_lending,facility_secure_type,product_solution,major_intervention," +
                "  primary_value_chain,secondary_value_chain,nature_ofproposal,sanction_state_name,sanction_branch_name,ccapproved_by, " +
                " date_format(ccapproved_date,'%d-%m-%Y') as ccapproved_date,natureof_proposal,sanction_type from ocs_mst_tcustomer2sanction" +
                " where customer2sanction_gid='" + customer2sanction_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.purpose_lending = objODBCDataReader["purpose_lending"].ToString();
                values.facility = objODBCDataReader["facility_secure_type"].ToString();
                values.product_solution = objODBCDataReader["product_solution"].ToString();
                values.majot_intervention = objODBCDataReader["major_intervention"].ToString();
                values.primaryvalue_chain = objODBCDataReader["primary_value_chain"].ToString();
                values.secondaryvalue_chain = objODBCDataReader["secondary_value_chain"].ToString();
                values.sanction_date = objODBCDataReader["sanction_date"].ToString();
                values.sanction_state_name = objODBCDataReader["sanction_state_name"].ToString();
                values.sanction_branch_name = objODBCDataReader["sanction_branch_name"].ToString();
                values.ccapproved_date = objODBCDataReader["ccapproved_date"].ToString();
                values.ccapproved_by = objODBCDataReader["ccapproved_by"].ToString();
                values.natureof_proposal = objODBCDataReader["natureof_proposal"].ToString();
                values.sanction_type = objODBCDataReader["sanction_type"].ToString();
            }
            objODBCDataReader.Close();
           msSQL= "select customer2sanction_gid,lsacreate_gid from ids_trn_tlsa where customer2sanction_gid='" + customer2sanction_gid + "'"+
                " order by lsacreate_gid desc limit 0,1";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if(objODBCDataReader.HasRows==true)
            {
                values.customer2sanction_flag = "Y";
                msSQL= " select   format(existing_limit,2) as existing_limit,format(document_limit, 2) as document_limit,format(limit_released,2) as limit_released," +
                        " facility_type,format(odlim,2) as odlim,if(report_structure='','NA',report_structure) as report_structure,interchangeability" +
                        " from ids_trn_tlimitinfodtl " +
                        " where lsacreate_gid='" + objODBCDataReader["lsacreate_gid"].ToString() + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getlimitinfo_limit = new List<limitinfo_limit>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getlimitinfo_limit.Add(new limitinfo_limit
                        {
                            document_limit = (dr_datarow["document_limit"].ToString()),
                            existing_limit = (dr_datarow["existing_limit"].ToString()),
                            limit_released = (dr_datarow["limit_released"].ToString()),
                            odlim = (dr_datarow["odlim"].ToString()),
                            facility_type = (dr_datarow["facility_type"].ToString()),
                        });
                    }
                    values.limitinfo_limit = getlimitinfo_limit;
                }
                dt_datatable.Dispose();
            }
            objODBCDataReader.Close();
            values.status = true;
            return true;
        }

        public bool DaPostLSAcreation(string employee_gid, MdlLsaManagement values)
        {
            if(values.sanction_type== "Existing Customer")
            {
                if((values.natureof_proposal==null)||(values.natureof_proposal==""))
                {
                    values.message = "Kindly Select Nature of Proposal";
                    values.status = false;
                    return false;
                }
            }
            msGetGID = objcmnfunctions.GetMasterGID("LSAA");

            msSQL = "select date_format(sanction_date,'%Y-%m-%d') as sanction_date,date_format(ccapproved_date,'%Y-%m-%d') as ccapproved_date from ocs_mst_tcustomer2sanction where " +
                " customer2sanction_gid='" + values.customer2sanction_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {

                msSQL = "insert into ids_trn_tlsa(" +
                        " lsacreate_gid ," +
                        " customer2sanction_gid," +
                        " lsaref_no," +
                        " lsacreated_date," +
                        " customer_name," +
                        " branch_name ," +
                        " state ," +
                        " customer_urn ," +
                        " customer_address," +
                        " customer_address1," +
                        " customer_location," +
                        " rm_name ," +
                        " business_head ," +
                        " cluster_head ," +
                        " zonal_head ," +
                        " credit_manager," +
                        " sanctionref_no ," +
                        " approved_by ," +
                        " sanction_date ," +
                        " approved_date ," +
                        " gst_no ," +
                        " pan_no," +
                        " purpose_lending ," +
                        " facility ," +
                        " major_corporate ," +
                        " hypothecation_date," +
                        " mortgage_date ," +
                        " product_solution ," +
                        " majot_intervention ," +
                        " sector ," +
                        " primaryvalue_chain ," +
                        " secondaryvalue_chain ," +
                        " vertical," +
                        " constitution," +
                        " sa_code," +
                        " sanction_type," +
                        " natureof_proposal," +
                        " remarks ," +
                        " created_by ," +
                        " created_date)" +
                        " values (" +
                        "'" + msGetGID + "'," +
                        "'" + values.customer2sanction_gid + "'," +
                        "'---'," +
                        "current_timestamp," +
                        "'" + values.customer_name.Replace("'", "") + "'," +
                        "'" + values.branch_name.Replace("'", "") + "'," +
                        "'" + values.state.Replace("'", "") + "'," +
                        "'" + values.customer_urn + "'," +
                        "'" + values.address1 + "',";
                        if(values.address==null)
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.address.Replace("       ", "") + "',";
                }
                          
                  msSQL+="'" + values.customer_location.Replace("'", "") + "'," +
                        "'" + values.rm_name + "'," +
                        "'" + values.business_head + "'," +
                        "'" + values.cluster_head + "'," +
                        "'" + values.zonal_head + "'," +
                        "'" + values.credit_manager + "'," +
                        "'" + values.sanctionref_no + "'," +
                        "'" + values.approved_by + "',";
                if (objODBCDataReader["sanction_date"].ToString() == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + objODBCDataReader["sanction_date"].ToString() + "',";
                }
                if (objODBCDataReader["ccapproved_date"].ToString() == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + objODBCDataReader["ccapproved_date"].ToString() + "',";
                }
              

                if (values.gst_no == null)
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.gst_no + "',";
                }
                if (values.pan_no == null)
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.pan_no + "',";
                }
                msSQL += "'" + values.purpose_lending.Replace("'", "") + "'," +
                         "'" + values.facility + "'," +
                         "'" + values.major_corporate + "',";
                if ((values.hypothecation_date == null) || (values.hypothecation_date == "undefined"))
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.hypothecation_date).ToString("yyyy-MM-dd") + "',";
                }
                if ((values.mortgage_date == null) || (values.mortgage_date == "undefined"))
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.mortgage_date).ToString("yyyy-MM-dd") + "',";
                }
                msSQL += "'" + values.product_solution.Replace("'", "") + "'," +
                          "'" + values.majot_intervention.Replace("'", "") + "'," +
                          "'" + values.sector + "'," +
                          "'" + values.primaryvalue_chain.Replace("'", "") + "'," +
                          "'" + values.secondaryvalue_chain.Replace("'", "") + "'," +
                          "'" + values.vertical + "'," +
                          "'" + values.constitution + "',";
                if (values.sa_code == null)
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.sa_code + "',";
                }

                msSQL += "'" + values.sanction_type + "'," +
                         "'" + values.natureof_proposal + "',";

                if (values.remarks == null)
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.remarks.Replace("'", "") + "',";
                }

                msSQL += "'" + employee_gid + "'," +
                            "current_timestamp)";
            }
            objODBCDataReader.Close();

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult !=0)
            {
                //Checking Collateral Security
                msSQL= "select lsacreate_gid from ids_trn_tlsa where customer2sanction_gid = '" + values.customer2sanction_gid + "'"+
                    " and lsacreate_gid <>'"+msGetGID+"' order by lsacreate_gid desc limit 0,1";
                string lsacreate_gid = objdbconn.GetExecuteScalar(msSQL);

                        if (lsacreate_gid != "")
                        {
                            msSQL = " select * from ocs_trn_tcustomercollateral a,ocs_trn_tsecuritytype b where a.securitytype_gid=b.securitytype_gid" +
                         " and  a.lsacreate_gid='" + lsacreate_gid + "'";

                            dt_datatable = objdbconn.GetDataTable(msSQL);

                            if (dt_datatable.Rows.Count != 0)
                            {
                                foreach (DataRow dr_datarow in dt_datatable.Rows)
                                {
                                    msGetGIDCOL = objcmnfunctions.GetMasterGID("COLT");

                                    //msGetGidREF = objcmnfunctions.GetMasterGID("COL");

                                    msSQL = " insert into ocs_trn_tcustomercollateral(" +
                                            " collateral_gid," +
                                            " collateralref_no," +
                                            " lsacreate_gid," +
                                            " security_type," +
                                            " securitytype_gid," +
                                            " security_description," +
                                            " account_status," +
                                            " borrowercheque_no," +
                                            " borroweraccount_no," +
                                            " borrowertbank_name," +
                                            " borrowerdeviation," +
                                            " borrowerother_remarks," +
                                            " guarantor_cheque," +
                                            " guarantor_acno," +
                                            " guarantor_bankname," +
                                            " guarantor_deviation," +
                                            " personalguarantor_name," +
                                            " guarantor_panno," +
                                            " corporate_guarantee," +
                                            " personal_guarantee," +
                                            " fd_bank_name," +
                                            " fd_no," +
                                            " fd_expiry_date," +
                                            " auto_renewal," +
                                            " bankguarantee_bankname," +
                                            " bankguarantee_expirydate," +
                                            " insurancecompany_name," +
                                            " policy_no," +
                                            " policy_expiry_date," +
                                            " created_by," +
                                            " created_date)" +
                                            " values(" +
                                            "'" + msGetGIDCOL + "'," +
                                            "'" + dr_datarow["collateralref_no"].ToString() + "'," +
                                            "'" + msGetGID + "'," +
                                            "'" + dr_datarow["security_type"].ToString() + "'," +
                                            "'" + dr_datarow["securitytype_gid"].ToString() + "'," +
                                            "'" + dr_datarow["security_description"].ToString() + "'," +
                                            "'" + dr_datarow["account_status"].ToString() + "'," +
                                            "'" + dr_datarow["borrowercheque_no"].ToString() + "'," +
                                            "'" + dr_datarow["borroweraccount_no"].ToString() + "'," +
                                            "'" + dr_datarow["borrowertbank_name"].ToString() + "'," +
                                            "'" + dr_datarow["borrowerdeviation"].ToString() + "'," +
                                            "'" + dr_datarow["borrowerother_remarks"].ToString() + "'," +
                                            "'" + dr_datarow["guarantor_cheque"].ToString() + "'," +
                                            "'" + dr_datarow["guarantor_acno"].ToString() + "'," +
                                            "'" + dr_datarow["guarantor_bankname"].ToString() + "'," +
                                            "'" + dr_datarow["guarantor_deviation"].ToString() + "'," +
                                            "'" + dr_datarow["personalguarantor_name"].ToString() + "'," +
                                            "'" + dr_datarow["guarantor_panno"].ToString() + "'," +
                                            "'" + dr_datarow["corporate_guarantee"].ToString() + "'," +
                                            "'" + dr_datarow["personal_guarantee"].ToString() + "'," +
                                            "'" + dr_datarow["fd_bank_name"].ToString() + "'," +
                                            "'" + dr_datarow["fd_no"].ToString() + "',";
                                    if ((dr_datarow["fd_expiry_date"].ToString() == null) || (dr_datarow["fd_expiry_date"].ToString() == ""))
                                    {
                                        msSQL += "null,";
                                    }
                                    else
                                    {
                                        msSQL += "'" + dr_datarow["fd_expiry_date"].ToString() + "',";
                                    }

                                    msSQL += "'" + dr_datarow["auto_renewal"].ToString() + "'," +
                                     "'" + dr_datarow["bankguarantee_bankname"].ToString() + "',";
                                    if ((dr_datarow["bankguarantee_expirydate"].ToString() == null) || (dr_datarow["bankguarantee_expirydate"].ToString() == ""))
                                    {
                                        msSQL += "null,";
                                    }
                                    else
                                    {
                                        msSQL += "'" + dr_datarow["bankguarantee_expirydate"].ToString() + "',";
                                    }

                                    msSQL += "'" + dr_datarow["insurancecompany_name"].ToString() + "'," +
                                        "'" + dr_datarow["policy_no"].ToString() + "',";
                                    if ((dr_datarow["policy_expiry_date"].ToString() == null) || (dr_datarow["policy_expiry_date"].ToString() == ""))
                                    {
                                        msSQL += "null,";
                                    }
                                    else
                                    {
                                        msSQL += "'" + dr_datarow["policy_expiry_date"].ToString() + "',";
                                    }
                                    msSQL += "'" + employee_gid + "'," +
                                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                }
                            }
                            dt_datatable.Dispose();
                       
                    //Security Flag Updation
                    msSQL = " update ids_trn_tlsa set collateralsecurity_flag='Y'  where lsacreate_gid='" + msGetGID + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //Compliance Check updation
                    msSQL = "select * from ids_trn_tcompliancecheck where lsacreate_gid='" + lsacreate_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if(objODBCDataReader.HasRows==true)
                    {
                        msGID = objcmnfunctions.GetMasterGID("LDCH");

                        msSQL = " insert into ids_trn_tcompliancecheck(" +
                            " compliancecheck_gid," +
                            " lsacreate_gid," +
                            " nach_mandate," +
                            " sign_match," +
                            " sign_match_kyc," +
                            " escrow_opened," +
                            " appropriate_stamp," +
                            " roc_filling," +
                            " nach_mandate_remarks," +
                            " sign_match_remarks," +
                            " sign_match_kyc_remarks," +
                            " escrow_opened_remarks," +
                            " appropriate_stamp_remarks," +
                            " roc_filling_remarks," +
                            " cersai_remarks," +
                             " cersai," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGID + "'," +
                             "'" + msGetGID + "'," +
                             "'" + objODBCDataReader["nach_mandate"].ToString () + "'," +
                                "'" + objODBCDataReader["sign_match"].ToString()  + "'," +
                                "'" + objODBCDataReader["sign_match_kyc"].ToString()  + "'," +
                                "'" + objODBCDataReader["escrow_opened"].ToString()  + "'," +
                                "'" + objODBCDataReader["appropriate_stamp"].ToString()  + "'," +
                                "'" + objODBCDataReader["roc_filling"].ToString() + "',"+
                                "'" + objODBCDataReader["nach_mandate_remarks"].ToString()  + "'," +
                                "'" + objODBCDataReader["sign_match_remarks"].ToString() + "'," +
                                "'" + objODBCDataReader["sign_match_kyc_remarks"].ToString()  + "'," +
                                "'" + objODBCDataReader["escrow_opened_remarks"].ToString() + "'," +
                                "'" + objODBCDataReader["appropriate_stamp_remarks"].ToString()  + "'," +
                                "'" + objODBCDataReader["roc_filling_remarks"].ToString() + "'," +
                                "'" + objODBCDataReader["cersai_remarks"].ToString()  + "'," +
                                "'" + objODBCDataReader["cersai"].ToString()  + "'," +
                                "'" + employee_gid + "'," +
                                "current_timestamp)";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ids_trn_tlsa set compliance_flag='Y'  where lsacreate_gid='" + msGetGID + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    objODBCDataReader.Close();
                }
                if(values.customer2sanction_flag=="Y")
                {
                    //msSQL = " select limitinfodtl_gid,margin,existing_limit,document_limit,limit_released,tenure,revolving_type,rate_interest,facility_type,"+
                    //        " limitinfo_remarks,calloption,moratorium,interest,principal,facility_type_gid,limitref_no,interchangeability,report_structure," +
                    //        " odlim,report_structure_gid,facility_amount,change_request,node,sub_limit,date_format(expiry_date,'%Y-%m-%d') as expiry_date"+
                    //        " from ids_trn_tlimitinfodtl where lsacreate_gid='" + lsacreate_gid + "'";

                    //dt_datatable = objdbconn.GetDataTable(msSQL);
                    //if (dt_datatable.Rows.Count != 0)
                    //{
                    //    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    //    {
                    //        string msGetGIDLT = objcmnfunctions.GetMasterGID("LIFO");
                    //        msSQL = "insert into ids_trn_tlimitinfodtl(" +
                    //          " limitinfodtl_gid," +
                    //    " lsacreate_gid," +
                    //    " limitref_no," +
                    //    " interchangeability," +
                    //    " report_structure," +
                    //    " odlim," +
                    //    " node," +
                    //    " margin," +
                    //    " existing_limit," +
                    //    " document_limit," +
                    //    " limit_released," +
                    //    " tenure," +
                    //    " revolving_type," +
                    //    " sub_limit," +
                    //    " rate_interest," +
                    //    " facility_type," +
                    //    " facility_type_gid," +
                    //    " facility_amount," +
                    //    " limitinfo_remarks," +
                    //    " change_request," +
                    //    " expiry_date," +
                    //    " calloption,"+
                    //    " moratorium," +
                    //    " interest," +
                    //    " principal," +
                    //    " created_by," +
                    //    " created_date)" +
                    //    " values(" +
                    //    "'" + msGetGIDLT + "'," +
                    //     "'" + msGetGID + "'," +
                    //     "'" + dr_datarow["limitref_no"].ToString() + "'," +
                    //     "'" + dr_datarow["interchangeability"].ToString() + "'," +
                    //       "'" + dr_datarow["report_structure"].ToString() + "'," +
                    //       "'" + dr_datarow["odlim"].ToString() + "'," +
                    //       "'" + dr_datarow["node"].ToString() + "'," +
                    //       "'" + dr_datarow["margin"].ToString() + "'," +
                    //       "'" + dr_datarow["existing_limit"].ToString() + "'," +
                    //       "'" + dr_datarow["document_limit"].ToString() + "'," +
                    //       "'" + dr_datarow["limit_released"].ToString() + "'," +
                    //       "'" + dr_datarow["tenure"].ToString() + "'," +
                    //       "'" + dr_datarow["revolving_type"].ToString() + "'," +
                    //       "'" + dr_datarow["sub_limit"].ToString() + "'," +
                    //       "'" + dr_datarow["rate_interest"].ToString() + "'," +
                    //       "'" + dr_datarow["facility_type"].ToString() + "'," +
                    //       "'" + dr_datarow["facility_type_gid"].ToString() + "'," +
                    //       "'" + dr_datarow["facility_amount"].ToString() + "'," +
                    //       "'" + dr_datarow["limitinfo_remarks"].ToString() + "'," +
                    //       "'" + dr_datarow["change_request"].ToString() + "'," +
                    //       "'" + dr_datarow["expiry_date"].ToString() + "'," +
                    //       "'" + dr_datarow["calloption"].ToString() + "'," +
                    //       "'" + dr_datarow["moratorium"].ToString() + "'," +
                    //       "'" + dr_datarow["interest"].ToString() + "'," +
                    //       "'" + dr_datarow["principal"].ToString() + "'," +
                    //       "'" + employee_gid + "'," +
                    //       "current_timestamp)";
                    //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    //    }
                    //    }
                    msSQL = " select bank_name,account_no,ifsc_code from ids_mst_tlsacustomer2bankinfo " +
                            " where lsacreate_gid='" + lsacreate_gid + "'";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                           string msGetGIDBK = objcmnfunctions.GetMasterGID("LBNK");
                            msSQL = " insert into ids_mst_tlsacustomer2bankinfo(" +
                                       " lsacustomer2bankinfo," +
                                       " lsacreate_gid," +
                                       " bank_name," +
                                       " account_no," +
                                       " ifsc_code," +
                                       " created_by," +
                                       " created_date)" +
                                       " values(" +
                                       "'" + msGetGIDBK + "'," +
                                        "'" + msGetGID + "'," +
                                        "'" + dr_datarow["bank_name"].ToString() + "'," +
                                        "'" + dr_datarow["account_no"].ToString() + "'," +
                                          "'" + dr_datarow["ifsc_code"].ToString() + "'," +
                                        "'" + employee_gid + "'," +
                                        "current_timestamp)";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    msSQL= "select recovered_type,recovered_amount,chequeno_details,date_format(chequedate_details,'%Y-%m-%d') as chequedate_details,bank_name,"+
                             " account_no,recover_remarks,to_be_recoveredamount" +
                            " from ids_trn_tprocessingfees where lsacreate_gid='" + lsacreate_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        string msGetGIDPS = objcmnfunctions.GetMasterGID("LPFE");
                        msSQL = " insert into ids_trn_tprocessingfees(" +
                                  " processfee_gid," +
                                  " lsacreate_gid," +
                                  " recovered_type," +
                                  " recovered_amount," +
                                  " chequeno_details," +
                                  " chequedate_details," +
                                  " bank_name," +
                                  " account_no," +
                                  " to_be_recoveredamount," +
                                  " recover_remarks," +
                                  " created_by," +
                                  " created_date)" +
                                  " values(" +
                                  "'" + msGetGIDPS + "'," +
                                   "'" + msGetGID + "'," +
                                   "'" + objODBCDatareader["recovered_type"].ToString() + "'," +
                                   "'" + objODBCDatareader["recovered_amount"].ToString() + "'," +
                                   "'" + objODBCDatareader["chequeno_details"].ToString() + "',";
                        if ((objODBCDatareader["chequedate_details"].ToString() == null)||(objODBCDatareader["chequedate_details"].ToString() == ""))
                        {
                            msSQL += "null,";
                        }
                        else
                        {
                            msSQL += "'" + objODBCDatareader["chequedate_details"].ToString() + "',";
                        }
                        msSQL+=  "'" + objODBCDatareader["bank_name"].ToString() + "'," +
                                   "'" + objODBCDatareader["account_no"].ToString() + "'," +
                                   "'" + objODBCDatareader["to_be_recoveredamount"].ToString() + "'," +
                                   "'" + objODBCDatareader["recover_remarks"].ToString() + "'," +
                                   "'" + employee_gid + "'," +
                                   "current_timestamp)";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            msSQL = "update ids_trn_tlsa set recover_flag='Y' where lsacreate_gid='" + msGetGID + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                        objODBCDatareader.Close();
                    }

                        msSQL= "select recovered_amount,chequeno_details,date_format(chequedate_details,'%Y-%m-%d') as chequedate_details,bank_name,account_no,"+
                               " document_charge,document_charge_gst,documentcharge_applicable,documentcharge_remarks from  ids_trn_tdocumentcharges"+
                               " where lsacreate_gid='" + lsacreate_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            string msGetGIDDOC = objcmnfunctions.GetMasterGID("LPFE");
                        msSQL = " insert into ids_trn_tdocumentcharges(" +
                                " documentcharge_gid," +
                                " lsacreate_gid," +
                                " document_charge," +
                                " recovered_amount," +
                                " chequeno_details," +
                                " chequedate_details," +
                                " bank_name," +
                                " account_no," +
                                " document_charge_gst," +
                                " documentcharge_applicable," +
                                " documentcharge_remarks," +
                                " created_by," +
                                " created_date) values(" +
                               "'" + msGetGIDDOC + "'," +
                               "'" + msGetGID + "'," +
                               "'" + objODBCDatareader["document_charge"].ToString() + "'," +
                               "'" + objODBCDatareader["recovered_amount"].ToString() + "'," +
                               "'" + objODBCDatareader["chequeno_details"].ToString() + "',";
                            if ((objODBCDatareader["chequedate_details"].ToString() == null) || (objODBCDatareader["chequedate_details"].ToString() == ""))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + objODBCDatareader["chequedate_details"].ToString() + "',";
                            }
                           msSQL+=    "'" + objODBCDatareader["bank_name"].ToString() + "'," +
                                       "'" + objODBCDatareader["account_no"].ToString() + "'," +
                                       "'" + objODBCDatareader["document_charge_gst"].ToString() + "'," +
                                       "'" + objODBCDatareader["documentcharge_applicable"].ToString() + "'," +
                                       "'" + objODBCDatareader["documentcharge_remarks"].ToString() + "'," +
                                       "'" + employee_gid + "'," +
                                       "current_timestamp)";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if(mnResult!=0)
                            {
                                msSQL = "update ids_trn_tlsa set document_charge_flag='Y' where lsacreate_gid='" + msGetGID + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }
                            objODBCDatareader.Close();
                        }
                }
                    values.message = "LSA Details Added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding LSA details";
                values.status = false;
                return false;
            }
           
        }

        public bool Dagetlsadetails(MdlLsaManagement values)
        {
            msSQL = " select lsacreate_gid,customer_name,branch_name,state,customer_urn,customer_location,rm_name,business_head,business_head," +
                      " cluster_head,zonal_head,credit_manager,sanctionref_no,date_format(sanction_date, '%d-%m-%Y') as sanction_date,customer_address," +
                      " approved_by,date_format(approved_date, '%d-%m-%Y') as approved_date,gst_no,a.pan_no,purpose_lending,facility," +
                      " major_corporate,date_format(hypothecation_date, '%d-%m-%Y') as hypothecation_date,date_format(lsacreated_date,'%d-%m-%Y %h:%i %p') as lsacreated_date," +
                      " date_format(mortgage_date, '%d-%m-%Y') as mortgage_date,product_solution,majot_intervention,sector,primaryvalue_chain,secondaryvalue_chain," +
                      " a.remarks,vertical,sa_code,constitution,lsaref_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,proceed_flag, "+
                      " approval_status,date_format(approvalupdated_date, '%d-%m-%Y') as lsaapproved_date,"+
                      " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as lsaapproved_by from ids_trn_tlsa a" +
                      " left join hrm_mst_temployee b on a.created_by=b.employee_gid"+
                      " left join adm_mst_tuser c on b.user_gid = c.user_gid"+
                      " left join hrm_mst_temployee d on a.approvalupdated_by=d.employee_gid" +
                      " left join adm_mst_tuser e on e.user_gid = d.user_gid order by lsacreate_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlsa_list = new List<lsa_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlsa_list.Add(new lsa_list
                    {
                        lsacreate_gid = dr_datarow["lsacreate_gid"].ToString(),
                        sanctionref_no = (dr_datarow["sanctionref_no"].ToString()),
                        branch_name = dr_datarow["branch_name"].ToString(),
                        state = (dr_datarow["state"].ToString()),
                        customer_name = dr_datarow["customer_name"].ToString(),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        customer_location = dr_datarow["customer_location"].ToString(),
                        address = (dr_datarow["customer_address"].ToString()),
                        rm_name = dr_datarow["rm_name"].ToString(),
                        zonal_head = (dr_datarow["zonal_head"].ToString()),
                        cluster_head = dr_datarow["cluster_head"].ToString(),
                        credit_manager = (dr_datarow["credit_manager"].ToString()),
                        business_head = dr_datarow["business_head"].ToString(),
                        sanction_date = (dr_datarow["sanction_date"].ToString()),
                        approved_by = dr_datarow["approved_by"].ToString(),
                        approved_date = (dr_datarow["approved_date"].ToString()),
                        gst_no = dr_datarow["gst_no"].ToString(),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        purpose_lending = dr_datarow["purpose_lending"].ToString(),
                        major_corporate = (dr_datarow["major_corporate"].ToString()),
                        majot_intervention = dr_datarow["majot_intervention"].ToString(),
                        primaryvalue_chain = (dr_datarow["primaryvalue_chain"].ToString()),
                        secondaryvalue_chain = dr_datarow["secondaryvalue_chain"].ToString(),
                        sector = (dr_datarow["sector"].ToString()),
                        product_solution = dr_datarow["product_solution"].ToString(),
                        hypothecation_date = (dr_datarow["hypothecation_date"].ToString()),
                        mortgage_date = dr_datarow["mortgage_date"].ToString(),
                        remarks = dr_datarow["remarks"].ToString(),
                        lsaref_no = dr_datarow["lsaref_no"].ToString(),
                        lsacreated_date = dr_datarow["lsacreated_date"].ToString(),
                        vertical = dr_datarow["vertical"].ToString(),
                        created_by=dr_datarow["created_by"].ToString (),
                        proceed_flag=dr_datarow["proceed_flag"].ToString (),
                        approval_status = dr_datarow["approval_status"].ToString(),
                        lsaapproved_by = dr_datarow["lsaapproved_by"].ToString(),
                        lsaapproved_date = dr_datarow["lsaapproved_date"].ToString()
                    });
                }
                values.lsa_list = getlsa_list;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        public void DaGetBranchdtl(MdlLsaManagement objbranch)
        {
            try
            {
                msSQL = " select branch_gid,branch_name from hrm_mst_tbranch ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbranch = new List<branch_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbranch.Add(new branch_list
                        {
                            branch_gid = (dr_datarow["branch_gid"].ToString()),
                            branch_name = (dr_datarow["branch_name"].ToString()),
                        });
                    }
                    objbranch.branch_list = getbranch;
                }
                dt_datatable.Dispose();

                objbranch.status = true;
            }
            catch
            {
                objbranch.status = false;
            }

        }

        public void DAGetloanfacility(MdlLsaManagement objbranch)
        {
            try
            {
                msSQL = "select loanmaster_gid,loan_title from ocs_mst_tloan;";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getfacility = new List<loanfacility_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getfacility.Add(new loanfacility_list
                        {
                            loanmaster_gid = (dr_datarow["loanmaster_gid"].ToString()),
                            loan_title = (dr_datarow["loan_title"].ToString()),
                        });
                    }
                    objbranch.loanfacility_list = getfacility;
                }
                dt_datatable.Dispose();

                objbranch.status = true;
            }
            catch
            {
                objbranch.status = false;
            }

        }

        public bool DaPostlimitinfo(string employee_gid, MdlLsaManagement values)
        {
            int odlim_validation = Convert.ToInt32(values.odlim.Replace(",", "").Replace(".00",""));
            int limit_validation = Convert.ToInt32(values.limit_validation.Replace(",", "").Replace(".00", ""));
            int document_limit_validation = Convert.ToInt32(values.document_limit.Replace(",", "").Replace(".00", ""));
            int limit_released_validation= Convert.ToInt32(values.limit_released.Replace(",", "").Replace(".00", ""));
            int exiting_limit_validation = Convert.ToInt32(values.existing_limit.Replace(",", "").Replace(".00", ""));

            if ((odlim_validation- limit_validation)< document_limit_validation)
            {
                values.message = "Document Limit Exceeded From ODLIM";
                values.status = false;
                return false;
            }
            else
            {   
             if ((limit_released_validation)>(document_limit_validation-exiting_limit_validation))

               {
                    values.message = "Limit To be Released Exceeded From Document Limit";
                    
                    values.status = false;
                    return false;
                }
                else
                {

            
                msGetGID = objcmnfunctions.GetMasterGID("LIFO");
            msSQL = "select count(limitinfodtl_gid) as count from ids_trn_tlimitinfodtl where lsacreate_gid='" + values.lsacreate_gid + "'";
            string lscount = objdbconn.GetExecuteScalar(msSQL);

            int count = Convert.ToInt16(lscount);
            int limitref_no = count + 1;
            lslimitref_no = "REF" + limitref_no;

                  

                    msSQL = " insert into ids_trn_tlimitinfodtl(" +
                        " limitinfodtl_gid," +
                        " lsacreate_gid," +
                        " limitref_no," +
                        " interchangeability," +
                        " report_structure," +
                        " odlim," +
                        " node," +
                        " margin," +
                        " existing_limit," +
                        " document_limit," +
                        " limit_released," +
                        " tenure," +
                        " revolving_type," +
                        " sub_limit," +
                        " rate_interest," +
                        " facility_type," +
                        " facility_type_gid," +
                        " facility_amount," +
                        " limitinfo_remarks," +
                        " change_request,"+
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGID + "'," +
                         "'" + values.lsacreate_gid + "'," +
                         "'" + lslimitref_no + "'," +
                         "'" + values.interchangeability + "',";
                    if(values.report_structure==null)
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.report_structure.Replace("     ","") + "',";
                    }

                    msSQL += "'" + values.odlim.Replace(",", "") + "'," +
                   "'" + values.node + "'," +
                   "'" + values.margin + "'," +
                   "'" + values.existing_limit.Replace(",", "") + "'," +
                   "'" + values.document_limit.Replace(",", "") + "'," +
                   "'" + values.limit_released.Replace(",", "") + "'," +
                   "'" + values.tenure + "'," +
                   "'" + values.revolving_type + "'," +
                   "'" + values.sub_limit + "'," +
                   "'" + values.rate_interest + "'," +
                   "'" + values.facility_type + "'," +
                   "'" + values.facility_type_gid + "'," +
                   "'" + values.loanfacility_amount.Replace(",", "") + "',";
                    if(values.limitinfo_remarks==null|| values.limitinfo_remarks=="")
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.limitinfo_remarks.Replace("'", "") + "',";
                    }
               
                 msSQL +="'" + values.change_request + "'," +
                 "'" + employee_gid + "'," +
                 "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (Convert.ToDateTime(values.expirydate).ToString("yyyy-MM-dd") == "0001-01-01 00:00:00")
                    {
                       
                    }
                    else
                    {
                        msSQL = "update ids_trn_tlimitinfodtl set expiry_date='" + Convert.ToDateTime(values.expirydate).ToString("yyyy-MM-dd") + "'" +
                   " where limitinfodtl_gid='" + msGetGID + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    if (mnResult != 0)
                    {
                        msSQL = " update ids_trn_tlimitinfodtl set odlim='" + values.odlim.Replace(",", "") + "' where lsacreate_gid='" + values.lsacreate_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ids_trn_troidocument set limitinfodtl_gid='" + msGetGID + "',"+
                                " lsacreate_gid='"+ values.lsacreate_gid + "' where " +
                                " (limitinfodtl_gid='"+employee_gid+"' and lsacreate_gid='" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        values.message = "Limit Information Added Successfully";
                        values.status = true;
                        return true;
                    }
                    else
                    {
                        values.message = "Error Occured while adding Limit Information";
                        values.status = false;
                        return false;
                    }
                }
            }
                           
        }

    
        public void DaGetlimitinfodtl(MdlLsaManagement values,string lsacreate_gid)
        {
            try
            {
                msSQL = " select a.limitinfodtl_gid,node,margin,format(existing_limit,2,'en_IN') as existing_limit,format(document_limit,2,'en_IN') as document_limit," +
                        " format(limit_released,2,'en_IN') as limit_released,tenure,revolving_type,sub_limit,rate_interest,facility_type," +
                        " date_format(expiry_date,'%d-%m-%Y') as expiry_date,limitinfo_remarks,if(principal is null,'---',principal) as principal,"+
                        " if(interest is null,'---',interest) as interest,if(moratorium is null,'---',moratorium) as moratorium ,if(calloption is null,'---',calloption) as calloption,"+
                        " limitref_no,format(odlim,2,'en_IN') as odlim,if(report_structure='','NA',report_structure) as report_structure,interchangeability," +
                        " change_request ,b.document_name,b.document_path,(26- rate_interest) as penal_interest from ids_trn_tlimitinfodtl a " +
                        " left join  ids_trn_troidocument b on a.limitinfodtl_gid = b.limitinfodtl_gid " +
                        " where a.lsacreate_gid='" +lsacreate_gid+"'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getblimit_info= new List<limitinfo_limit>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getblimit_info.Add(new limitinfo_limit
                        {
                            limitinfodtl_gid=dr_datarow["limitinfodtl_gid"].ToString(),      
                            tenure = (dr_datarow["tenure"].ToString()),
                            margin=dr_datarow["margin"].ToString (),
                            node = (dr_datarow["node"].ToString()),
                            existing_limit = (dr_datarow["existing_limit"].ToString()),
                            document_limit = (dr_datarow["document_limit"].ToString()),
                            limit_released = (dr_datarow["limit_released"].ToString()),
                            revolving_type = (dr_datarow["revolving_type"].ToString()),
                            sub_limit = (dr_datarow["sub_limit"].ToString()),
                            rate_interest = (dr_datarow["rate_interest"].ToString()),
                            expiry_date = (dr_datarow["expiry_date"].ToString()),
                            facility_type = (dr_datarow["facility_type"].ToString()),
                            limitinfo_remarks = (dr_datarow["limitinfo_remarks"].ToString()),
                            principal = (dr_datarow["principal"].ToString()),
                            interest = (dr_datarow["interest"].ToString()),
                            moratorium = (dr_datarow["moratorium"].ToString()),
                            calloption = (dr_datarow["calloption"].ToString()),
                            lslimitref_no = (dr_datarow["limitref_no"].ToString()),
                            odlim = (dr_datarow["odlim"].ToString()),
                            report_structure = (dr_datarow["report_structure"].ToString()),
                            interchangeability=dr_datarow["interchangeability"].ToString (),
                            change_request = dr_datarow["change_request"].ToString(),
                            document_name = dr_datarow["document_name"].ToString(),
                            document_path = (dr_datarow["document_path"].ToString()),
                            penal_interest = dr_datarow["penal_interest"].ToString(),
                        });
                    }
                    values.limitinfo_limit = getblimit_info;
                    dt_datatable.Dispose();


                    }



                msSQL = "select lsacreate_gid from ids_trn_tlsa where recover_flag='Y' and document_charge_flag='Y'" +
                       " and clarify_flag = 'Y'  and lsacreate_gid='" + lsacreate_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.final_flag = "Y";
                }
                else
                {
                    values.final_flag = "N";
                }
                objODBCDataReader.Close();
                msSQL= "select a.interchangeability,applicable_condition from ids_trn_tlimitinfodtl a"+
                        " left join ocs_mst_tsanction2loanfacilitytype b on a.facility_type_gid = b.sanction2loanfacilitytype_gid"+
                        " where lsacreate_gid = '" + lsacreate_gid + "' limit 0,1";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if(objODBCDatareader.HasRows==true)
                {
                    if (objODBCDatareader["interchangeability"].ToString() == "No")
                    {
                        if ((objODBCDatareader["applicable_condition"].ToString()  == "No")|| (objODBCDatareader["applicable_condition"].ToString() == null) || (objODBCDatareader["applicable_condition"].ToString() == ""))
                        {

                            msSQL = "select format(sum(limit_released),2,'en_IN') from ids_trn_tlimitinfodtl  a " +
                                   " where lsacreate_gid ='" + lsacreate_gid + "'";

                            values.totol_limit_released = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = "select  case when b.sanctionref_no like '%DBS%' then format(document_limit,2,'en_IN')" +
                                  " when b.sanctionref_no not like '%DBS%' then format(sum(document_limit),2,'en_IN')  end as document_limit" +
                                  " from ids_trn_tlimitinfodtl a" +
                                  " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                                  " where a.lsacreate_gid ='" + lsacreate_gid + "'";
                            values.total_document_limit = objdbconn.GetExecuteScalar(msSQL);

                        }
                        else
                        {
                            msSQL = "select limit_released from ids_trn_tlimitinfodtl  a " +
                                            " where lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                            string totol_limit_released = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select odlim from ids_trn_tlimitinfodtl  a " +
                                           " where lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                            string odlim = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select sum(document_limit) from ids_trn_tlimitinfodtl a" +
                                    " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                                    " where a.lsacreate_gid ='" + lsacreate_gid + "' ";
                            string total_document_limit = objdbconn.GetExecuteScalar(msSQL);
                            var lstotol_limit_released = Int64.Parse(totol_limit_released);
                            var lsodlim = Int64.Parse(odlim);
                            var lstotal_document_limit = Int64.Parse(total_document_limit);

                            if ((lsodlim) >=(lstotol_limit_released))
                            {
                                msSQL = "select format(sum(limit_released),2,'en_IN') from ids_trn_tlimitinfodtl  a " +
                                           " where lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                                values.totol_limit_released = objdbconn.GetExecuteScalar(msSQL);
                            }
                            else
                            {
                                msSQL = "select format(limit_released,2,'en_IN') from ids_trn_tlimitinfodtl  a " +
                                          " where lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                                values.totol_limit_released = objdbconn.GetExecuteScalar(msSQL);
                            }
                            if ((lsodlim) == (lstotal_document_limit))
                            {
                                msSQL = "select  case when b.sanctionref_no like '%DBS%' then format(document_limit,2,'en_IN')" +
                                    " when b.sanctionref_no not like '%DBS%' then format(odlim,2,'en_IN')  end as document_limit" +
                                    " from ids_trn_tlimitinfodtl a" +
                                    " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                                    " where a.lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                                values.total_document_limit = objdbconn.GetExecuteScalar(msSQL);
                            }
                            else
                            {
                                msSQL = "select  case when b.sanctionref_no like '%DBS%' then document_limit" +
                                    " when b.sanctionref_no not like '%DBS%' then sum(document_limit)  end as document_limit" +
                                    " from ids_trn_tlimitinfodtl a" +
                                    " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                                    " where a.lsacreate_gid ='" + lsacreate_gid + "'";
                               string document_limit = objdbconn.GetExecuteScalar(msSQL);
                                var lsdocument_limit = Int64.Parse(document_limit);

                                if (lsodlim>= lsdocument_limit)
                                {
                                    msSQL = "select   format(document_limit,2,'en_IN')" +
                                   " from ids_trn_tlimitinfodtl a" +
                                   " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                                   " where a.lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                                    values.total_document_limit = objdbconn.GetExecuteScalar(msSQL);
                                }
                                else
                                {
                                    msSQL = "select  format(odlim,2,'en_IN')" +
                                                                       " from ids_trn_tlimitinfodtl a" +
                                                                       " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                                                                       " where a.lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                                    values.total_document_limit = objdbconn.GetExecuteScalar(msSQL);
                                }

                            }
                        }
                    }
                    else
                    {
                        msSQL = "select sum(limit_released) from ids_trn_tlimitinfodtl  a " +
                         " where lsacreate_gid ='" + lsacreate_gid + "' group by lsacreate_gid";
                        string totol_limit_released = objdbconn.GetExecuteScalar(msSQL);
                      
                        msSQL = "select  odlim from ids_trn_tlimitinfodtl a" +
                                                                      " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                                                                      " where a.lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                        string odlim = objdbconn.GetExecuteScalar(msSQL);
                       
                        var lstotol_limitreleased = Int64.Parse(totol_limit_released);
                        var lsodlim = Int64.Parse(odlim);

                        if (lsodlim >= lstotol_limitreleased)
                        {
                            msSQL = "select  format(sum(limit_released),2,'en_IN') from ids_trn_tlimitinfodtl  a " +
                           " where lsacreate_gid ='" + lsacreate_gid + "' group by lsacreate_gid";
                           
                            values.totol_limit_released = objdbconn.GetExecuteScalar(msSQL);

                        }
                        else
                        {
                            msSQL = "select format(odlim,2,'en_IN') from ids_trn_tlimitinfodtl a" +
                                                                    " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                                                                    " where a.lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                            values.totol_limit_released = objdbconn.GetExecuteScalar(msSQL); ;
                        }

                        msSQL = "select  case when b.sanctionref_no like '%DBS%' then format(document_limit,2,'en_IN')" +
                                  " when b.sanctionref_no not like '%DBS%' then format(odlim,2,'en_IN')  end as document_limit" +
                                  " from ids_trn_tlimitinfodtl a" +
                                  " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid " +
                                  " where a.lsacreate_gid ='" + lsacreate_gid + "' limit 0,1";
                        values.total_document_limit = objdbconn.GetExecuteScalar(msSQL);

                    }
                    objODBCDatareader.Close();
                }

          
                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }

        public void Dagetlimitref_no(MdlLsaManagement values, string lsacreate_gid)
        {
            try
            {
                msSQL = " select limitref_no,limitinfodtl_gid from ids_trn_tlimitinfodtl where interchangeability<>'No' and limitref_no<>'ODLIM'" +
                        " and lsacreate_gid='" + lsacreate_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getblimit_info = new List<limitinfo_limit>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getblimit_info.Add(new limitinfo_limit
                        {
                            lslimitref_no = (dr_datarow["limitref_no"].ToString()),
                            limitinfodtl_gid = (dr_datarow["limitinfodtl_gid"].ToString())
                        });
                    }
                    values.limitinfo_limit = getblimit_info;
                   
                }
                dt_datatable.Dispose();
            }
            catch
            {
                values.status = false;
            }

        }
   
        public void Dagetodlim(limit values, string lsacreate_gid)
        {
            try
            {
                msSQL = " select customer2sanction_gid from ids_trn_tlsa where lsacreate_gid='" + lsacreate_gid + "'";
                string lsacustomer2sanction_gid = objdbconn.GetExecuteScalar(msSQL);

             
               

                msSQL = " select sanction_amount from ocs_mst_tcustomer2sanction where customer2sanction_gid='" + lsacustomer2sanction_gid + "'";

                string odlim_amount = objdbconn.GetExecuteScalar(msSQL);

                string odlim = odlim_amount;
                decimal parsed1 = decimal.Parse(odlim, System.Globalization.CultureInfo.InvariantCulture);
                System.Globalization.CultureInfo indian_format1 = new System.Globalization.CultureInfo("hi-IN");
                string text1 = string.Format(indian_format1, "{0:c}", parsed1);

                msSQL = "select substring('" + text1 + "',2,20)";
                values.odlim = objdbconn.GetExecuteScalar(msSQL);

                msSQL= " select sum(distinct a.document_limit) as document_limit from ids_trn_tlimitinfodtl a"+
                     " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid"+ 
                     " left join ocs_mst_tsanction2loanfacilitytype c on b.customer2sanction_gid = c.customer2sanction_gid"+
                     " where c.interchangeability<> 'yes' and applicable_condition<> 'Yes' and a.lsacreate_gid='" + lsacreate_gid + "' group by a.lsacreate_gid";
                string document_limit = objdbconn.GetExecuteScalar(msSQL);

                string document_limitamount = document_limit;
                decimal parsed = decimal.Parse(document_limitamount, System.Globalization.CultureInfo.InvariantCulture);
                System.Globalization.CultureInfo indian_format = new System.Globalization.CultureInfo("hi-IN");
                string text = string.Format(indian_format, "{0:c}", parsed);

                msSQL = "select substring('" + text + "',2,20)";
                values.total_document_limit = objdbconn.GetExecuteScalar(msSQL);

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGETeditlimitref_no(MdlLsaManagement values, string limitinfodtl_gid)
        {
            try
            {
                msSQL = "select lsacreate_gid from ids_trn_tlimitinfodtl where limitinfodtl_gid='" + limitinfodtl_gid + "'";
                string lsacreate_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select limitref_no,limitinfodtl_gid from ids_trn_tlimitinfodtl where interchangeability<>'No'" +
                        " and lsacreate_gid='" + lsacreate_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getblimit_info = new List<limitinfo_limit>();
                if (dt_datatable.Rows.Count != 0)
                {
                    getblimit_info.Add(new limitinfo_limit
                    {
                        lslimitref_no = "ODLIM",
                        limitinfodtl_gid = "ODLIM",
                    });
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getblimit_info.Add(new limitinfo_limit
                        {
                            lslimitref_no = (dr_datarow["limitref_no"].ToString()),
                            limitinfodtl_gid = (dr_datarow["limitinfodtl_gid"].ToString())

                        });
                    }
                    values.limitinfo_limit = getblimit_info;

                }
                dt_datatable.Dispose();
            }
            catch
            {
                values.status = false;
            }

        }

        public bool Dapostbankinfo(string employee_gid, MdlLsaManagement values)
        {
            msGetGID = objcmnfunctions.GetMasterGID("LBNK");

            msSQL = " insert into ids_mst_tlsacustomer2bankinfo(" +
                " lsacustomer2bankinfo," +
                " lsacreate_gid," +
                " bank_name," +
                " account_no," +
                " ifsc_code," +
                " created_by," +
                " created_date)" +
                " values(" +
                "'" + msGetGID + "'," +
                 "'" + values.lsacreate_gid + "'," +
                 "'" + values.bank_name.Replace("'", "") + "'," +
                 "'" + values.account_no + "'," +
                 "'" + values.ifsc_code + "'," +
                 "'" + employee_gid + "'," +
                 "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Bank Information Added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding Bank Information";
                values.status = false;
                return false;
            }




        }

        public void DaGetbankinfodtl(MdlLsaManagement values, string lsacreate_gid)
        {
            try
            {
                msSQL = " select lsacustomer2bankinfo,bank_name,account_no,ifsc_code  from ids_mst_tlsacustomer2bankinfo where " +
                        "  lsacreate_gid='" + lsacreate_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbankinfo_info = new List<bankinfo_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbankinfo_info.Add(new bankinfo_list
                        {
                            bank_name = (dr_datarow["bank_name"].ToString()),
                            account_no = (dr_datarow["account_no"].ToString()),
                            ifsc_code = (dr_datarow["ifsc_code"].ToString()),
                            lsacustomer2bankinfo = (dr_datarow["lsacustomer2bankinfo"].ToString()),
                        });
                    }
                    values.bankinfo_list = getbankinfo_info;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }

        public void DaGetrepaymentinfo(MdlLsaManagement values, string limitinfodtl_gid)
        {
            try
            {
                msSQL = " select moratorium,principal,interest,calloption from ids_trn_tlimitinfodtl where " +
                         "  limitinfodtl_gid='" + limitinfodtl_gid + "'";

                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if(objODBCDataReader.HasRows==true)
                {
                   values.moratorium= objODBCDataReader["moratorium"].ToString();
                    values.principal = objODBCDataReader["principal"].ToString();
                    values.interest = objODBCDataReader["interest"].ToString();
                    values.call_option = objODBCDataReader["calloption"].ToString();
                }
                objODBCDataReader.Close();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }

        public bool Dapostrepaymentdtl(string employee_gid, MdlLsaManagement values)
        {

            msSQL = "select lsacreate_gid from ids_trn_tlimitinfodtl where limitinfodtl_gid = '" + values.limitinfodtl_gid + "'";
            string lsacreate_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select customer_name from ids_trn_tlsa where lsacreate_gid = '" + lsacreate_gid + "'";
            string customer_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select updated_by, date_format(updated_date, '%Y-%m-%d %H:%i:%s') as updated_date from ids_trn_tlimitinfodtl where limitinfodtl_gid = '" + values.limitinfodtl_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGID = objcmnfunctions.GetMasterGID("LSUL");
                    msSQL = " insert into ids_trn_tlsaupdatedlog(" +
                              " lsaupdatedlog_gid," +
                              " lsacreate_gid," +
                              " customer_name, " +
                              " remarks, " +
                              " lastupdated_by, " +
                              " lastupdated_date, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGID + "'," +
                              "'" + lsacreate_gid + "'," +
                              "'" + customer_name + "'," +
                              "'" + "Repayment Details" + "'," +
                              "'" + lsUpdatedBy + "'," +
                              "'" + lsUpdatedDate + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();


            msSQL = " update ids_trn_tlimitinfodtl set " +
               " principal='" + values.principal + "'," +
                " interest='" + values.interest + "'," +
                " moratorium='" + values.moratorium + "'," +
                " calloption='" + values.call_option + "'" +
                " where limitinfodtl_gid='" + values.limitinfodtl_gid + "'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Repayment Details  Added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding Repayment Details";
                values.status = false;
                return false;
            }

        }

        public bool Dapostprocessingfee(string employee_gid, MdlLsaManagement values)
        {



            msGetGID = objcmnfunctions.GetMasterGID("LPFE");

            msSQL = " insert into ids_trn_tprocessingfees(" +
                " processfee_gid," +
                " lsacreate_gid," +
                " recovered_type," +
                " recovered_amount," +
                " chequeno_details," +
                " chequedate_details," +
                " bank_name," +
                " account_no," +
                " to_be_recoveredamount,"+
                " recover_remarks," +
                " created_by," +
                " created_date)" +
                " values(" +
                "'" + msGetGID + "'," +
                 "'" + values.lsacreate_gid + "'," +
                 "'" + values.recovered_type + "',";
            if ((values.recovered_amount == null)|| (values.recovered_amount == "NaN"))
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.recovered_amount.Replace(",","") + "',";
            }
            if (values.chequeno_details == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.chequeno_details + "',";
            }
            if (values.chequedate_details == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime (values.chequedate_details).ToString ("yyyy-MM-dd") + "',";
            }
            if (values.processingfeebank_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.processingfeebank_name.Replace("'","") + "',";
            }
            if (values.processingfeaccount_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.processingfeaccount_name.Replace("'", "") + "',";
            }
            if ((values.to_be_recoveredamount == null) || (values.to_be_recoveredamount == "NaN"))
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.to_be_recoveredamount.Replace(",", "") + "',";
            }
            if (values.recover_remarks == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.recover_remarks.Replace("'", "") + "',";
            }
         msSQL +=  "'" + employee_gid + "'," +
                 "current_timestamp)";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update ids_trn_tlsa set recover_flag='Y' where lsacreate_gid='" + values.lsacreate_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.message = "Processing Fee / commision Added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding Processing Fee / commision";
                values.status = false;
                return false;
            }

        }

        public bool Dapostdocumentcharge(string employee_gid, MdlLsaManagement values)
        {
            msGetGID = objcmnfunctions.GetMasterGID("LDCH");

            msSQL = " insert into ids_trn_tdocumentcharges(" +
                " documentcharge_gid," +
                " lsacreate_gid," +
                " document_charge," +
                " recovered_amount," +
                " chequeno_details," +
                " chequedate_details," +
                " bank_name," +
                " account_no," +
                " document_charge_gst," +
                " documentcharge_applicable," +
                " documentcharge_remarks," +
                " created_by," +
                " created_date)" +
                " values(" +
                "'" + msGetGID + "'," +
                 "'" + values.lsacreate_gid + "'," +
                    "'" + values.document_charge.Replace(",", "") + "'," +
                    "'" + values.doc_recovered_amount.Replace(",", "") + "'," +
                    "'" + values.doc_chequeno_details + "'," +
                    "'" + Convert.ToDateTime(values.doc_chequedate_details).ToString("yyyy-MM-dd") + "'," +
                    "'" + values.doc_feebank_name + "'," +
                    "'" + values.doc_feaccount_name + "'," +
                    "'" + values.document_charge_gst + "'," +
                    "'" + values.documentcharge_applicable + "',";
                    if ((values.documentcharge_remarks == null) || (values.documentcharge_remarks == "") || (values.documentcharge_remarks == "undefined"))
                    {
                        msSQL += "null,";
                    }
                    {
                        msSQL += "'" + values.documentcharge_remarks.Replace("'", "") + "',";
                    }
              msSQL+= "'" + employee_gid + "'," +
                      "current_timestamp)";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update ids_trn_tlsa set document_charge_flag='Y' where lsacreate_gid='" + values.lsacreate_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Documentation charges Added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding Documentation charges ";
                values.status = false;
                return false;
            }

        }

      

        public bool DaGetprocessingfeeinfo(MdlLsaManagement values, string lsacreate_gid)
        {
            msSQL = " select recovered_type,format(recovered_amount,2) as recovered_amount,chequeno_details, chequedate_details,date_format(chequedate_details,'%d-%m-%Y') as processing_date," +
                " bank_name,account_no,recover_remarks,to_be_recoveredamount,lsacreate_gid from ids_trn_tprocessingfees " +
                    " where  lsacreate_gid='" + lsacreate_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.recovered_type = objODBCDataReader["recovered_type"].ToString();
             
                values.chequeno_details = objODBCDataReader["chequeno_details"].ToString();
               values.chequedate_details = objODBCDataReader["processing_date"].ToString();
                if (objODBCDataReader["chequedate_details"].ToString() != "")
                {
                    values.cheque_date = Convert.ToDateTime(objODBCDataReader["chequedate_details"].ToString());
                }
                values.processingfeebank_name = objODBCDataReader["bank_name"].ToString();
                values.processingfeaccount_name = objODBCDataReader["account_no"].ToString();
                values.recover_remarks = objODBCDataReader["recover_remarks"].ToString();
                values.lsacreate_gid= objODBCDataReader["lsacreate_gid"].ToString();
                if (objODBCDataReader["recovered_amount"].ToString() == "0.00")
                {
                    values.recovered_amount = objODBCDataReader["recovered_amount"].ToString();
                }
                else
                {
                    string fare = objODBCDataReader["recovered_amount"].ToString();
                    decimal parsed = decimal.Parse(fare, System.Globalization.CultureInfo.InvariantCulture);
                    System.Globalization.CultureInfo indian_format = new System.Globalization.CultureInfo("hi-IN");
                    string text = string.Format(indian_format, "{0:c}", parsed);

                    msSQL = "select substring('" + text + "',2,20)";
                    string recoveredAmount = objdbconn.GetExecuteScalar(msSQL);

                    values.recovered_amount = recoveredAmount;
                }

                if (objODBCDataReader["to_be_recoveredamount"].ToString() == "0.00")
                {
                    values.recovered_amount = objODBCDataReader["to_be_recoveredamount"].ToString();
                }
                else
                {
                    string fare1 = objODBCDataReader["to_be_recoveredamount"].ToString();
                    decimal parsed1 = decimal.Parse(fare1, System.Globalization.CultureInfo.InvariantCulture);
                    System.Globalization.CultureInfo indian_format1 = new System.Globalization.CultureInfo("hi-IN");
                    string text1 = string.Format(indian_format1, "{0:c}", parsed1);

                    msSQL = "select substring('" + text1 + "',2,20)";
                    string lsto_be_recoveredamount = objdbconn.GetExecuteScalar(msSQL);

                    values.to_be_recoveredamount = lsto_be_recoveredamount;
                }
                
            }
            objODBCDataReader.Close();
            values.status = true;
            return true;
        }

        public bool DaGetdocumentchargeinfo(MdlLsaManagement values, string lsacreate_gid)
        {
            msSQL = "select format(recovered_amount,2) as recovered_amount,chequeno_details,chequedate_details,date_format(chequedate_details,'%d-%m-%Y') as doc_date," +
                " bank_name,account_no,document_charge,document_charge_gst,documentcharge_applicable,documentcharge_remarks from ids_trn_tdocumentcharges " +
                    " where   lsacreate_gid='" + lsacreate_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
             
                values.doc_chequeno_details = objODBCDataReader["chequeno_details"].ToString();
               values.doc_chequedate_details = objODBCDataReader["doc_date"].ToString();
                values.doc_feebank_name = objODBCDataReader["bank_name"].ToString();
                values.doc_feaccount_name = objODBCDataReader["account_no"].ToString();
                values.document_charge = objODBCDataReader["document_charge"].ToString();
                values.document_charge_gst = objODBCDataReader["document_charge_gst"].ToString();
                values.documentcharge_remarks = objODBCDataReader["documentcharge_remarks"].ToString();
                values.documentcharge_applicable = objODBCDataReader["documentcharge_applicable"].ToString();

                if (objODBCDataReader["chequedate_details"].ToString() != "")
                {
                    values.doc_cheque_date = Convert.ToDateTime(objODBCDataReader["chequedate_details"].ToString());
                }
                string fare = objODBCDataReader["recovered_amount"].ToString();
                decimal parsed = decimal.Parse(fare, System.Globalization.CultureInfo.InvariantCulture);
                System.Globalization.CultureInfo indian_format = new System.Globalization.CultureInfo("hi-IN");
                string text = string.Format(indian_format, "{0:c}", parsed);

                msSQL = "select substring('" + text + "',2,20)";
                string recoveredAmount = objdbconn.GetExecuteScalar(msSQL);
                values.doc_recovered_amount = recoveredAmount;
            }
            objODBCDataReader.Close();
            msSQL = " select document_name,document_path " +
                          " from ids_tmp_tuploaddocumentcharges  where lsacreate_gid='" + lsacreate_gid + "'";
            objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader1.HasRows == true)
            {
                values.document_name = objODBCDataReader1["document_name"].ToString();
                values.document_path = objcmnstorage.EncryptData(objODBCDataReader1["document_path"].ToString());
            }
            objODBCDataReader1.Close();
            values.status = true;
            return true;
        }
        public bool DaGetlsainfo(MdlLsaManagement values, string lsacreate_gid)
        {
            msSQL = "select document_charge_flag,recover_flag,concat(customer_name,' / ',customer_urn) as customer_name,clarify_flag,"+
                    " compliance_flag,proceed_flag,approval_status from ids_trn_tlsa " +
                    " where   lsacreate_gid='" + lsacreate_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.document_charge_flag = objODBCDataReader["document_charge_flag"].ToString();
                values.recover_flag = objODBCDataReader["recover_flag"].ToString();
                values.customer_name = objODBCDataReader["customer_name"].ToString();
                values.clarify_flag = objODBCDataReader["clarify_flag"].ToString();
                values.compliance_flag = objODBCDataReader["compliance_flag"].ToString();
                values.proceed_flag = objODBCDataReader["proceed_flag"].ToString();
                values.approval_status = objODBCDataReader["approval_status"].ToString();
            }
            objODBCDataReader.Close();
            values.status = true;
            return true;
        }
        public bool DaGetcustomer2sanctioninfo(MdlLsaManagement values, string lsacreate_gid)
        {
            msSQL = "select customer2sanction_gid from ids_trn_tlsa " +
                    " where   lsacreate_gid='" + lsacreate_gid + "'";
            string lscustomer2sanction_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select * from ocs_mst_tcustomer2sanction where customer2sanction_gid='" + lscustomer2sanction_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.margin = objODBCDataReader["margin"].ToString();
                values.rate_interest = objODBCDataReader["rateof_interest"].ToString();
                values.tenure = objODBCDataReader["tenure_months"].ToString();
                values.expiry_date = objODBCDataReader["sanction_expirydate"].ToString();
            }
            objODBCDataReader.Close();
            values.status = true;
            return true;
        }
        public bool DaPostfinal(string employee_gid, MdlLsaManagement values)
        {
            msGetGID = objcmnfunctions.GetMasterGID("LDCH");

            msSQL = " insert into ids_trn_tfinal(" +
                " finalclarification_gid," +
                " lsacreate_gid," +
                " terms_conditions," +
                " deferral_captured," +
                " head," +
                " created_by," +
                " created_date)" +
                " values(" +
                "'" + msGetGID + "'," +
                 "'" + values.lsacreate_gid + "'," +
                 "'" + values.terms_conditions + "'," +
                    "'" + values.deferral_captured + "'," +
                    "'" + values.head + "'," +
                    "'" + employee_gid + "'," +
                    "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select  concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as user_name from ids_trn_tfinal a, hrm_mst_temployee b, adm_mst_tuser c" +
                " where a.created_by = b.employee_gid and b.user_gid = c.user_gid and lsacreate_gid='" + values.lsacreate_gid + "'";

            string lsmaker_signature = objdbconn.GetExecuteScalar(msSQL);

           
                msGETGIDDoc = objcmnfunctions.GetMasterGID("MKAR");
                msSQL = " insert into ids_trn_tmakersignature(" +
                        " makersignature_gid," +
                        " lsacreate_gid," +
                        " maker_signature," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGETGIDDoc + "'," +
                         "'" + values.lsacreate_gid + "'," +
                        "'" + lsmaker_signature  + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            
            if (mnResult != 0)
            {
                msSQL = " update ids_trn_tlsa set clarify_flag='Y' where lsacreate_gid='" + values.lsacreate_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

               

                values.message = " Maker Signature and final clarify details added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding ";
                values.status = false;
                return false;
            }

        }

        public bool DaPostcompliancecheck(string employee_gid, MdlLsaManagement values)
        {
            msGetGID = objcmnfunctions.GetMasterGID("LDCH");

            msSQL = " insert into ids_trn_tcompliancecheck(" +
                " compliancecheck_gid," +
                " lsacreate_gid," +
                " nach_mandate," +
                " sign_match," +
                " sign_match_kyc," +
                " escrow_opened," +
                " appropriate_stamp," +
                " roc_filling," +
                " nach_mandate_remarks," +
                " sign_match_remarks," +
                " sign_match_kyc_remarks," +
                " escrow_opened_remarks," +
                " appropriate_stamp_remarks," +
                " roc_filling_remarks,"   +           
                " cersai_remarks," +
                 " cersai," +
                " created_by," +
                " created_date)" +
                " values(" +
                "'" + msGetGID + "'," +
                 "'" + values.lsacreate_gid + "'," +
                 "'" + values.nach_mandate + "'," +
                    "'" + values.sign_match + "'," +
                    "'" + values.sign_match_kyc + "'," +
                     "'" + values.escrow_opened + "'," +
                 "'" + values.appropriate_stamp + "'," +
                    "'" + values.roc_filling + "',";
            if ((values.nach_mandate_remarks == "")|| (values.nach_mandate_remarks == null))
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.nach_mandate_remarks.Replace("'", "") + "',";
            }
            if ((values.sign_match_remarks == "") || (values.sign_match_remarks == null))
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.sign_match_remarks.Replace("'", "") + "',";
            }
            if ((values.sign_match_kyc_remarks == "") || (values.sign_match_kyc_remarks == null))
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.sign_match_kyc_remarks.Replace("'", "") + "',";
            }
            if ((values.escrow_opened_remarks == "") || (values.escrow_opened_remarks == null))
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.escrow_opened_remarks.Replace("'", "") + "',";
            }
            if ((values.appropriate_stamp_remarks == "") || (values.appropriate_stamp_remarks == null))
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.appropriate_stamp_remarks.Replace("'", "") + "',";
            }
            if ((values.roc_filling_remarks == "") || (values.roc_filling_remarks == null))
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.roc_filling_remarks.Replace("'", "") + "',";
            }
            if ((values.cersai_remarks == "") || (values.cersai_remarks == null))
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.cersai_remarks.Replace("'", "") + "',";
            }

            msSQL += "'" + values.cersai + "'," +
                "'" + employee_gid + "'," +
                    "current_timestamp)";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

           
            if (mnResult != 0)
            {
                msSQL = " update ids_trn_tlsa set compliance_flag='Y' where lsacreate_gid='" + values.lsacreate_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = " Compliance Check Details Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding ";
                values.status = false;
                return false;
            }

        }

        public bool DaGetmakerinfo(MdlLsaManagement values, string lsacreate_gid)
        {
            msSQL = "select terms_conditions,deferral_captured,head from ids_trn_tfinal "+
                " where   lsacreate_gid='" + lsacreate_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.terms_conditions = objODBCDataReader["terms_conditions"].ToString();
                values.deferral_captured = objODBCDataReader["deferral_captured"].ToString();
                values.head = objODBCDataReader["head"].ToString();
               
            }
            objODBCDataReader.Close();

            msSQL= " select maker_signature from ids_trn_tmakersignature "+
                    " where   lsacreate_gid='" + lsacreate_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.maker_signature = objODBCDataReader["maker_signature"].ToString();
              

            }
            objODBCDataReader.Close();

            values.status = true;
            return true;
        }

        public bool DaGetcompliancecheckinfo(MdlLsaManagement values, string lsacreate_gid)
        {
            msSQL = "select nach_mandate,sign_match,sign_match_kyc,escrow_opened,appropriate_stamp,roc_filling,"+
                    " nach_mandate_remarks,sign_match_remarks,sign_match_kyc_remarks,escrow_opened_remarks,appropriate_stamp_remarks,"+
                    " roc_filling_remarks,cersai,cersai_remarks from ids_trn_tcompliancecheck " +
                     " where   lsacreate_gid='" + lsacreate_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.nach_mandate = objODBCDataReader["nach_mandate"].ToString();
                values.sign_match = objODBCDataReader["sign_match"].ToString();
                values.sign_match_kyc = objODBCDataReader["sign_match_kyc"].ToString();
                values.escrow_opened = objODBCDataReader["escrow_opened"].ToString();
                values.appropriate_stamp = objODBCDataReader["appropriate_stamp"].ToString();
                values.roc_filling = objODBCDataReader["roc_filling"].ToString();
                values.nach_mandate_remarks = objODBCDataReader["nach_mandate_remarks"].ToString();
                values.sign_match_remarks = objODBCDataReader["sign_match_remarks"].ToString();
                values.sign_match_kyc_remarks = objODBCDataReader["sign_match_kyc_remarks"].ToString();
                values.escrow_opened_remarks = objODBCDataReader["escrow_opened_remarks"].ToString();
                values.appropriate_stamp_remarks = objODBCDataReader["appropriate_stamp_remarks"].ToString();
                values.roc_filling_remarks = objODBCDataReader["roc_filling_remarks"].ToString();
                values.cersai = objODBCDataReader["cersai"].ToString();
                values.cersai_remarks = objODBCDataReader["cersai_remarks"].ToString();
            }

            objODBCDataReader.Close();

            values.status = true;
            return true;
        }

        public bool Dapostproceed_finalinfo(string employee_gid, MdlLsaManagement values)
        {


            msSQL = "select count(*) from ids_trn_tlsa where month(pdfgenerate_date)=month(curdate()) and year(pdfgenerate_date)=year(curdate())";
            lsmonthwise = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(*) from ids_trn_tlsa where day(pdfgenerate_date)=day(curdate()) and month(pdfgenerate_date)=month(curdate()) and year(pdfgenerate_date)=year(curdate())";
            lsdaywise = objdbconn.GetExecuteScalar(msSQL);

            lsmonthcount = Int32.Parse(lsmonthwise) + 1;
            lsdaycount = Int16.Parse(lsdaywise) + 1;

            lssequencecode = msGETGIDDoc + lsmonthcount + lsdaycount;

            msSQL = " update ids_trn_tlsa set proceed_flag='Y'," +
                " lsaref_no= '" + lssequencecode + "'," +
                " pdfgenerate_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where lsacreate_gid='" + values.lsacreate_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                msSQL = "update ids_trn_tmakersignature set updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where lsacreate_gid='" + values.lsacreate_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = "SELECT company_mail FROM adm_mst_tcompany ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    frommail_id = objODBCDatareader["company_mail"].ToString();

                }
                objODBCDatareader.Close();
                msSQL = "select lsacreate_gid,date_format(pdfgenerate_date,'%d-%m-%Y') as generated_date,lsaref_no from ids_trn_tlsa where " +
                    " proceed_flag='Y' and  (collateralsecurity_flag='N' or compliance_flag='N') and lsacreate_gid='" + values.lsacreate_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                //if (dt_datatable.Rows.Count != 0)
                //{
                //    foreach (DataRow dr_datarow in dt_datatable.Rows)
                //    {
                //        msSQL = "select b.employee_emailid,concat(c.user_firstname,' ',c.user_lastname) as maker_name  from ids_trn_tmakersignature a" +
                //                " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                //                " left join adm_mst_tuser c on b.user_gid=c.user_gid where lsacreate_gid='" + dr_datarow["lsacreate_gid"] + "'";
                //        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                //        if (objODBCDatareader.HasRows == true)
                //        {
                //            objODBCDatareader.Read();
                //            tomail_address = objODBCDatareader["employee_emailid"].ToString();
                //            lsmaker_name = objODBCDatareader["maker_name"].ToString();

                //        }
                //        objODBCDatareader.Close();


                //        msSQL = "select group_concat(employee_mailid) as cc_mail from ocs_trn_tmailcclist where mailtrigger_function='LSA-Alert'";
                //        ccmail_id = objdbconn.GetExecuteScalar(msSQL);

                //        sub = "CAD Internal Data updation In LSA";
                //        body = "<b>From:</b>" + " " + "[" + "" + frommail_id + "" + "]" + "<br/>";
                //        body = body + "<b>Sent:</b>" + DateTime.Now.ToString("dd MMMM,yyyy hh:mm tt") + "<br/>";
                //        body = body + "<b>To:</b>" + "" + tomail_address + "" + "<br/>";
                //        body = body + "<b>Cc:</b>" + "" + ccmail_id + "";
                //        body = body + "<br />";
                //        body = body + "<b>Subject:</b>" + "CAD Internal Data updation In LSA";
                //        body = body + "<br />";
                //        body = body + "<br />";
                //        body = body + "Dear" + " " + "<b>" + lsmaker_name + "</b>" + "," + "";
                //        body = body + "<br/>";
                //        body = body + "<br/>";
                //        body = body + "CAD internal Data updation is pending for the LSA with ref. No. " + "<b>" + dr_datarow["lsaref_no"].ToString() + "</b>" + " released on Dt. " + "<b>" + dr_datarow["generated_date"].ToString() + "</b>" + ".";
                //        body = body + " Please update the same immediately and confirm. Also, note to track the related deferrals in ECMS and tag the approvals without fail.";
                //        body = body + "<br/>";

                //        body = body + "<br/>";
                //        body = body + "Yours Sincerely,";
                //        body = body + "<br/>";
                //        body = body + "<b>" + "Samunnati Financial Intermediation & Services Pvt Ltd." + "</b>";
                //        body = body + "<br/>";
                //        body = body + "<br/>";
                //        body = body + " **This is an automated e-mail. Please do not reply to this mailbox " + "<br/>";
                //        body = body + "<br/>";

                //        var mail = LSAMail(tomail_address, ccmail_id, sub, body);
                //        if (mail == true)
                //        {
                //            msGID = objcmnfunctions.GetMasterGID("MLSA");

                //            msSQL = " insert into ocs_trn_tlsapendingmail(" +
                //                    " lsamail_gid," +
                //                    " frommail_id," +
                //                    " tomail_id, " +
                //                    " ccmail_id, " +
                //                    " lsacreate_gid, " +
                //                    " sent_date)" +
                //                    " values(" +
                //                    "'" + msGID + "'," +
                //                    "'" + frommail_id + "'," +
                //                    "'" + tomail_address + "'," +
                //                    "'" + ccmail_id + "'," +
                //                    "'" + dr_datarow["lsacreate_gid"].ToString() + "'," +
                //                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //        }
                //    }

                //}
                values.message = "PDF Generated Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while Generating ";
                values.status = false;
                return false;
            }

        }

        public bool LSAMail(string to, string cc, string sub, string body)
        {
            try
            {
                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                objreader = objdbconn.GetDataReader(msSQL);
                if (objreader.HasRows)
                {
                    ls_server = objreader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objreader["pop_port"]);
                    ls_username = objreader["pop_username"].ToString();
                    ls_password = objreader["pop_password"].ToString();
                }
                objreader.Close();
                objdbconn.CloseConn();
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ls_username);
                message.To.Add(new MailAddress(to));
                if (cc != null & cc != string.Empty & cc != "")
                {
                    lsCCReceipients = cc.Split(',');
                    if (cc.Length == 0)
                    {
                        message.CC.Add(new MailAddress(cc));
                    }
                    else
                    {
                        foreach (string CCEmail in lsCCReceipients)
                        {
                            message.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id
                        }
                    }
                }
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
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DaPostUploaddocument(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
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

            string document_type = httpRequest.Form["document_type"].ToString();
            string lsacreate_gid= httpRequest.Form["lsacreate_gid"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "LSA/Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocuments" + "/" + lscompany_code + "/" + "LSA/Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month;


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
                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "LSA/Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        //FileStream file = new FileStream(lspath, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LSA/Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "LSA/Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        if (document_type == "undefined")
                        {
                            lsdocument_type = "";
                        }
                        else
                        {
                            lsdocument_type = document_type;
                        }
                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "LSA/Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        msGetGID = objcmnfunctions.GetMasterGID("UECD");
                        msSQL = " insert into ids_trn_tlsauploaddocument( " +
                                    " lsauploaddocument_gid," +
                                    " lsacreate_gid ," +
                                    " document_name," +
                                    " document_path," +
                                    " document_type ," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGID + "'," +
                                    "'" + lsacreate_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lsdocument_type.Replace("'", "") + "'," +
                                    "'" + user_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                msSQL = " select lsauploaddocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                     " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as uploaded_by" +
                     " from ids_trn_tlsauploaddocument a, adm_mst_tuser b where a.created_by=b.user_gid and lsacreate_gid='" + lsacreate_gid+ "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<UploadDocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new UploadDocumentList
                        {
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["document_gid"].ToString()),
                            document_type = dr_datarow["document_type"].ToString(),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString()
                        });
                    }
                    objfilename.UploadDocumentList = get_filename;
                }
                dt_datatable.Dispose();
            }
            catch
            {

            }
            if (mnResult == 1)
            {
                objfilename.status = true;
                objfilename.message = "Document Uploaded Successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Ocuured while uploading document";
                return false;
            }
        }
        public void DaGetdocument(UploadDocumentname values,string lsacreate_gid)
        {
            msSQL = " select lsauploaddocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                                " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as uploaded_by" +
                                " from ids_trn_tlsauploaddocument a, adm_mst_tuser b where a.created_by=b.user_gid and lsacreate_gid='" + lsacreate_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<UploadDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new UploadDocumentList
                    {
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = ((dr_datarow["document_path"].ToString())),
                        document_gid = (dr_datarow["lsauploaddocument_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.UploadDocumentList = get_filename;
            }
            dt_datatable.Dispose();
          

        }

        public void DaGetaccountno_validation(MdlLsaManagement values)
        {
            msSQL = "select account_no from ids_mst_tlsacustomer2bankinfo where  lsacreate_gid='" + values.lsacreate_gid + "' and"+
                " account_no='"+ values.doc_feaccount_name + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if(objODBCDataReader .HasRows ==true)
            {
                values.status = true;
            }
            else
            {
                values.status = false;
            }
            objODBCDataReader.Close();

        }
        public bool DaGetdetailsLSA(MdlLsaManagement values, string lsacreate_gid)
        {
            msSQL= " select lsacreate_gid,c.customername as customer_name,b.sanction_branch_name as branch_name,b.sanction_state_name as state,c.customer_urn,customer_location," +
                " b.sanction_refno as sanctionref_no,date_format(b.sanction_date, '%d-%m-%Y') as sanction_date,customer_address,b.ccapproved_by as approved_by," +
                " date_format(b.ccapproved_date, '%d-%m-%Y') as approved_date,c.gst_number as gst_no,c.pan_number as pan_no, b.purpose_lending,facility," +
                " c.major_corporate,date_format(hypothecation_date, '%d-%m-%Y') as hypothecation_date,date_format(approvalupdated_date, '%d-%m-%Y') as lsacreated_date, " +
                " date_format(mortgage_date, '%d-%m-%Y') as mortgage_date,b.product_solution,b.major_intervention as majot_intervention,sector," +
                " b.primary_value_chain as primaryvalue_chain,b.secondary_value_chain as secondaryvalue_chain,a.remarks,c.vertical_code as vertical,sa_code," +
                " c.constitution_name as constitution,lsaref_no,customer_address1,b.sanction_type,hypothecation_date as hypothecationdate,mortgage_date as mortgagedate," +
                " concat(c.creditmgmt_name, ' / ', d.employee_emailid) as credit_manager," +
                "  concat(c.cluster_manager_name, ' / ', e.employee_emailid) as cluster_head," +
                " concat(c.zonal_name, ' / ', f.employee_emailid) as zonal_head,concat(c.businesshead_name, ' / ', g.employee_emailid) as business_head," +
                " concat(c.relationshipmgmt_name, ' / ', h.employee_emailid) as rm_name from ids_trn_tlsa a" +
                " left join ocs_mst_tcustomer2sanction b on a.customer2sanction_gid = b.customer2sanction_gid" +
                " left join ocs_mst_tcustomer c on b.customer_gid = c.customer_gid" +
                " left join hrm_mst_temployee d on c.creditmanager_gid = d.employee_gid" +
                " left join hrm_mst_temployee e on c.cluster_manager_gid = e.employee_gid" +
                " left join hrm_mst_temployee f on c.zonal_head = f.employee_gid" +
                " left join hrm_mst_temployee g on c.business_head = g.employee_gid" +
                " left join hrm_mst_temployee h on c.relationship_manager = h.employee_gid  where lsacreate_gid ='" + lsacreate_gid + "' ";
         
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.lsacreate_gid = objODBCDataReader["lsacreate_gid"].ToString();
                values.sanctionref_no = (objODBCDataReader["sanctionref_no"].ToString());
                values.branch_name = objODBCDataReader["branch_name"].ToString();
                values.facility = objODBCDataReader["facility"].ToString();
                values.state = (objODBCDataReader["state"].ToString());
                values.vertical = objODBCDataReader["vertical"].ToString();
                values.sa_code = objODBCDataReader["sa_code"].ToString();
                values.constitution = objODBCDataReader["constitution"].ToString();
                values.sector = objODBCDataReader["sector"].ToString();
                values.customer_name = objODBCDataReader["customer_name"].ToString();
                values.customer_urn = (objODBCDataReader["customer_urn"].ToString());
                values.customer_location = objODBCDataReader["customer_location"].ToString();
                values.address = (objODBCDataReader["customer_address1"].ToString());
                values.rm_name = objODBCDataReader["rm_name"].ToString();
                values.zonal_head = (objODBCDataReader["zonal_head"].ToString());
                values.cluster_head = objODBCDataReader["cluster_head"].ToString();
                values.credit_manager = (objODBCDataReader["credit_manager"].ToString());
                values.business_head = objODBCDataReader["business_head"].ToString();
                values.sanction_date = (objODBCDataReader["sanction_date"].ToString());
                values.approved_by = objODBCDataReader["approved_by"].ToString();
                values.approved_date = (objODBCDataReader["approved_date"].ToString());
                values.gst_no = objODBCDataReader["gst_no"].ToString();
                values.pan_no = (objODBCDataReader["pan_no"].ToString());
                values.purpose_lending = objODBCDataReader["purpose_lending"].ToString();
                values.major_corporate = (objODBCDataReader["major_corporate"].ToString());
                values.majot_intervention = objODBCDataReader["majot_intervention"].ToString();
                values.primaryvalue_chain = (objODBCDataReader["primaryvalue_chain"].ToString());
                values.secondaryvalue_chain = objODBCDataReader["secondaryvalue_chain"].ToString();
                values.sector = (objODBCDataReader["sector"].ToString());
                values.address1 = objODBCDataReader["customer_address"].ToString();
                values.product_solution = objODBCDataReader["product_solution"].ToString();
                values.hypothecation_date = (objODBCDataReader["hypothecation_date"].ToString());
                values.mortgage_date = objODBCDataReader["mortgage_date"].ToString();
                if (objODBCDataReader["hypothecationdate"].ToString() != "")
                {
                    values.hypothecationdate = Convert.ToDateTime(objODBCDataReader["hypothecationdate"].ToString());
                }
                if (objODBCDataReader["mortgagedate"].ToString() != "")
                {
                    values.mortgagedate = Convert.ToDateTime(objODBCDataReader["mortgagedate"].ToString());
                }
                values.remarks = objODBCDataReader["remarks"].ToString();
                values.lsaref_no = objODBCDataReader["lsaref_no"].ToString();
                values.lsacreated_date = objODBCDataReader["lsacreated_date"].ToString();
                values.sanction_type = objODBCDataReader["sanction_type"].ToString();
            }
            objODBCDataReader.Close();

            msSQL = "select concat(c.user_firstname,' ',c.user_lastname) as approved_by ,date_format(approvalupdated_date,'%d-%m-%Y %h:%i %p') as approved_date " +
                " from ids_trn_tlsa a" +
                " left join hrm_mst_temployee b on a.approvalupdated_by=b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid=c.user_gid where lsacreate_gid='" + lsacreate_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.lsaapproved_by = objODBCDataReader["approved_by"].ToString();
                values.lsaapproved_date = objODBCDataReader["approved_date"].ToString();
                
            }
            objODBCDataReader.Close();
            msSQL = " select momdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_type,document_path, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                " from ids_trn_tuploadmomdocument a,hrm_mst_temployee b, adm_mst_tuser c, ids_trn_tlsa d where a.created_by=b.employee_gid" +
                " and b.user_gid = c.user_gid and a.customer2sanction_gid=d.customer2sanction_gid and lsacreate_gid ='" + lsacreate_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMOM_filename = new List<MOMDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMOM_filename.Add(new MOMDocumentList
                    {
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        document_gid = (dr_datarow["momdocument_gid"].ToString()),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.MOMDocumentList = getMOM_filename;
            }
            dt_datatable.Dispose();

            msSQL = " select camdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_type,document_path, " +
              " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
              " from ids_trn_tuploadcamdocument a,hrm_mst_temployee b, adm_mst_tuser c, ids_trn_tlsa d where a.created_by=b.employee_gid" +
              " and b.user_gid = c.user_gid and a.customer2sanction_gid=d.customer2sanction_gid and lsacreate_gid ='" + lsacreate_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCAM_filename = new List<COMDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getCAM_filename.Add(new COMDocumentList
                    {
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        document_gid = (dr_datarow["camdocument_gid"].ToString()),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString()
                    });
                }
                values.COMDocumentList = getCAM_filename;
            }
            dt_datatable.Dispose();

            msSQL = " select sanctionletter_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                    " from ids_trn_tuploadsanctionletter a,hrm_mst_temployee b, adm_mst_tuser c, ids_trn_tlsa d where a.created_by=b.employee_gid" +
                    " and b.user_gid = c.user_gid and a.customer2sanction_gid=d.customer2sanction_gid and lsacreate_gid ='" + lsacreate_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_sanfilename = new List<SANDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_sanfilename.Add(new SANDocumentList
                    {
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["sanctionletter_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString(),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString())
                    });
                }
                values.SANDocumentList = get_sanfilename;
            }
            dt_datatable.Dispose();

            msSQL = " select generaldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path,document_type, " +
                   " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by" +
                   " from ids_trn_tuploadgeneraldocument a,hrm_mst_temployee b, adm_mst_tuser c, ids_trn_tlsa d where a.created_by=b.employee_gid" +
                   " and b.user_gid = c.user_gid and a.customer2sanction_gid=d.customer2sanction_gid and lsacreate_gid ='" + lsacreate_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getfilename = new List<GeneralDocumentList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getfilename.Add(new GeneralDocumentList
                    {
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_gid = (dr_datarow["generaldocument_gid"].ToString()),
                        document_type = dr_datarow["document_type"].ToString(),
                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                        updated_date = dr_datarow["uploaded_date"].ToString(),
                        document_path = objcmnstorage.EncryptData(dr_datarow["document_path"].ToString())
                    });
                }
                values.GeneralDocumentList = getfilename;
            }
            dt_datatable.Dispose();

            values.status = true;
            return true;
        }
        public bool DaPostupdatebankinfo(string employee_gid, MdlLsaManagement values)
        {

            msSQL = "select lsacreate_gid from ids_mst_tlsacustomer2bankinfo where lsacustomer2bankinfo = '" + values.lsacustomer2bankinfo + "'";
            string lsacreate_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select customer_name from ids_trn_tlsa where lsacreate_gid = '" + lsacreate_gid + "'";
            string customer_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select updated_by, date_format(updated_date, '%Y-%m-%d %H:%i:%s') as updated_date from ids_mst_tlsacustomer2bankinfo where lsacustomer2bankinfo = '" + values.lsacustomer2bankinfo + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGID = objcmnfunctions.GetMasterGID("LSUL");
                    msSQL = " insert into ids_trn_tlsaupdatedlog(" +
                              " lsaupdatedlog_gid," +
                              " lsacreate_gid," +
                              " customer_name, " +
                              " remarks, " +
                              " lastupdated_by, " +
                              " lastupdated_date, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGID + "'," +
                              "'" + lsacreate_gid + "'," +
                              "'" + customer_name + "'," +
                              "'" + "Customer Bank A/c. Details" + "'," +
                              "'" + lsUpdatedBy + "'," +
                              "'" + lsUpdatedDate + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();


            msSQL = " update ids_mst_tlsacustomer2bankinfo set " +
                " bank_name='"+ values.bank_name+"'," +
                " account_no='"+values.account_no+"'," +
                " ifsc_code='"+ values.ifsc_code+"'," +
                " updated_by='"+ employee_gid +"'," +
                " updated_date=current_timestamp where lsacustomer2bankinfo='" + values.lsacustomer2bankinfo + "' ";
             
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Bank Information updated Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while updating Bank Information";
                values.status = false;
                return false;
            }




        }

        public bool DaGetBankinfo(MdlLsaManagement values, string lsacustomer2bankinfo)
        {
            msSQL = " select  bank_name,account_no,ifsc_code from ids_mst_tlsacustomer2bankinfo where lsacustomer2bankinfo='" + lsacustomer2bankinfo + "'";

                objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {
                values.bank_name = objODBCDataReader["bank_name"].ToString();
                values.account_no = (objODBCDataReader["account_no"].ToString());
                values.ifsc_code = objODBCDataReader["ifsc_code"].ToString();
               }
            objODBCDataReader.Close();
            values.status = true;
            return true;
        }
        public bool Dapostuploaddoc(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
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


            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";

            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            //path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDS/document_charges/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocuments" + "/" + lscompany_code + "/" + "IDS/document_charges/" + DateTime.Now.Year + "/" + DateTime.Now.Month;

            if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
           
            string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
            string lsfirstdocument_filepath = string.Empty;

            httpFileCollection = httpRequest.Files;

            httpPostedFile = httpFileCollection[0];
            string FileExtension = httpPostedFile.FileName;
            //string lsfile_gid = msdocument_gid + FileExtension;
            string lsfile_gid = msdocument_gid;
            FileExtension = Path.GetExtension(FileExtension).ToLower();
            lsfile_gid = lsfile_gid + FileExtension;
              ls_readStream = httpPostedFile.InputStream;
                ls_readStream.CopyTo(ms);
            //    lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDS/document_charges/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
            //    objcmnfunctions.uploadFile(lspath, lsfile_gid);
            //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDS/document_charges/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

            bool status;
            status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDS/document_charges/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
            ms.Close();
            lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDS/document_charges/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

            msGetGID = objcmnfunctions.GetMasterGID("UPPD");
                msSQL = " insert into ids_tmp_tuploaddocumentcharges( " +
                             " tempdocumentcharge_gid," +
                             " lsacreate_gid,"+
                             " document_name, " +
                             " document_path,"+
                             " created_by ," +
                             " created_date " +
                             " )values(" +
                             "'"+ msGetGID +"',"+
                             "'" + employee_gid + "'," +
                             "'" + httpPostedFile.FileName + "'," +
                             "'" + lspath + msdocument_gid + FileExtension + "'," +
                             "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
           
            if (mnResult != 0)
            {
                objfilename.status = true;
                objfilename.message = "Document uploaded successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured while uploading document";
                return false;
            }
        }
        public bool Dapostdocumentchargewithdoc(string employee_gid, MdlLsaManagement values)
        {
            msGetGID = objcmnfunctions.GetMasterGID("LDCH");

            msSQL = " insert into ids_trn_tdocumentcharges(" +
                " documentcharge_gid," +
                " lsacreate_gid," +
                " document_charge," +
                " recovered_amount," +
                " chequeno_details," +
                " chequedate_details," +
                " bank_name," +
                " account_no," +
                " document_charge_gst," +
                " documentcharge_applicable," +
                " documentcharge_remarks," +
                " created_by," +
                " created_date)" +
                " values(" +
                "'" + msGetGID + "'," +
                 "'" + values.lsacreate_gid + "'," +
                   "'" + values.document_charge.Replace(",", "") + "'," +
                 "'" + values.doc_recovered_amount.Replace(",", "") + "'," +
                    "'" + values.doc_chequeno_details + "'," +
                    "'" + Convert.ToDateTime(values.doc_chequedate_details).ToString("yyyy-MM-dd") + "'," +
                    "'" + values.doc_feebank_name + "'," +
                    "'" + values.doc_feaccount_name + "'," +
                    "'" + values.document_charge_gst + "'," +
                    "'" + values.documentcharge_applicable + "',";
                    if ((values.documentcharge_remarks == null) || (values.documentcharge_remarks == "") || (values.documentcharge_remarks == "undefined"))
            {
                msSQL += "null,";
            }
            {
                msSQL += "'" + values.documentcharge_remarks.Replace("'", "") + "',";
            }
            msSQL+= "'" + employee_gid + "'," +
                    "current_timestamp)";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update ids_trn_tlsa set document_charge_flag='Y' where lsacreate_gid='" + values.lsacreate_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update ids_tmp_tuploaddocumentcharges set lsacreate_gid='" + values.lsacreate_gid + "' where created_by='" + employee_gid + "'"+
                    " and lsacreate_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.message = "Documentation charges Added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding Documentation charges ";
                values.status = false;
                return false;
            }

        }
        public bool DaGetlimitinfo(string employee_gid, string limitinfodtl_gid, MdlLimitinfoEdit values)
        {
            try
            {
                msSQL = " select limitinfodtl_gid,node,margin,format(existing_limit,2,'en_IN') as existing_limit,format(document_limit,2,'en_IN') as document_limit," +
                        " format(limit_released,2,'en_IN') as limit_released,tenure,revolving_type,sub_limit,rate_interest, expiry_date," +
                        " facility_type,limitinfo_remarks,facility_type_gid, limitref_no,format(odlim,2,'en_IN') as odlim," +
                        " if(report_structure='','---',report_structure) as report_structure,interchangeability,facility_amount, " +
                        " change_request from ids_trn_tlimitinfodtl " +
                        " where limitinfodtl_gid='" + limitinfodtl_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.node_edit = objODBCDataReader["node"].ToString();
                    values.margin_edit = objODBCDataReader["margin"].ToString();
                    values.existing_limit_edit = objODBCDataReader["existing_limit"].ToString();
                    values.tenure_edit = objODBCDataReader["tenure"].ToString();
                    values.revolving_type_edit = objODBCDataReader["revolving_type"].ToString();
                    values.sub_limit_edit = objODBCDataReader["sub_limit"].ToString();
                    values.interchangeability = objODBCDataReader["interchangeability"].ToString();
                    values.limitref_no = objODBCDataReader["limitref_no"].ToString();
                    values.report_structure = objODBCDataReader["report_structure"].ToString();
                    values.rate_interest_edit = objODBCDataReader["rate_interest"].ToString();
                    if (objODBCDataReader["expiry_date"].ToString() != "")
                    {
                        values.expirydate_edit = Convert.ToDateTime(objODBCDataReader["expiry_date"].ToString());
                    }
                    
                    values.limitinfo_remarks_edit = objODBCDataReader["limitinfo_remarks"].ToString();
                    values.facility_type_gid = objODBCDataReader["facility_type_gid"].ToString();
                    values.change_request = objODBCDataReader["change_request"].ToString();
                    values.document_limit_edit = objODBCDataReader["document_limit"].ToString();
                    values.limit_released_edit = objODBCDataReader["limit_released"].ToString();
                    values.existing_limit_edit = objODBCDataReader["existing_limit"].ToString();
                    values.facility_amount = objODBCDataReader["facility_amount"].ToString();
                    values.odlim = objODBCDataReader["odlim"].ToString();
                    objODBCDataReader.Close();
                   
                }
                msSQL = " select document_name,document_path " +
                           " from ids_trn_troidocument  where  limitinfodtl_gid = '" + limitinfodtl_gid + "'";
                objODBCDataReader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader1.HasRows == true)
                {
                    values.document_name = objODBCDataReader1["document_name"].ToString();
                    values.document_path = objcmnstorage.EncryptData(objODBCDataReader1["document_path"].ToString());
                }
                objODBCDataReader1.Close();
                msSQL = "select lsacreate_gid from ids_trn_tlimitinfodtl  where limitinfodtl_gid = '" + limitinfodtl_gid + "'";
                string lsacreate_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select format(sum(distinct a.document_limit),2,'en_IN') as document_limit from ids_trn_tlimitinfodtl a" +
                 " left join ids_trn_tlsa b on a.lsacreate_gid = b.lsacreate_gid" +
                 " left join ocs_mst_tsanction2loanfacilitytype c on b.customer2sanction_gid = c.customer2sanction_gid" +
                 " where c.interchangeability<> 'yes' and applicable_condition<> 'Yes' and a.lsacreate_gid='" + lsacreate_gid + "' and" +
                 " a.limitinfodtl_gid <> '" + limitinfodtl_gid + "' group by a.lsacreate_gid";
                values.total_document_limit = objdbconn.GetExecuteScalar(msSQL);



                msSQL = "select customer2sanction_gid from ids_trn_tlsa where lsacreate_gid='" + lsacreate_gid + "'";
                string lscustomer2sanction_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select loanfacilityref_no" +
                " from ocs_mst_tsanction2loanfacilitytype where customer2sanction_gid='" + lscustomer2sanction_gid + "' ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<loanfacilitytype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new loanfacilitytype_list
                        {

                            loanfacilityref_no = dr_datarow["loanfacilityref_no"].ToString()
                        });
                    }
                    values.loanfacilitytype_list = get_filename;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message;
                return false;
            }
            values.status = true;
            return true;
        }

        public bool Daupdatelimitinfo(string employee_gid, MdlLimitinfoEdit values)
        {
            int odlim_validation = Convert.ToInt32(values.odlim.Replace(",", "").Replace(".00", ""));
            int limit_validation = Convert.ToInt32(values.limit_validation.Replace(",", "").Replace(".00", ""));
            int document_limit_validation = Convert.ToInt32(values.document_limit_edit.Replace(",", "").Replace(".00", ""));
            int limit_released_validation = Convert.ToInt32(values.limit_released_edit.Replace(",", "").Replace(".00", ""));
            int exiting_limit_validation = Convert.ToInt32(values.existing_limit_edit.Replace(",", "").Replace(".00", ""));

            if ((odlim_validation - limit_validation) < document_limit_validation)
            {
                values.message = "Document Limit Exceeded From ODLIM";
                values.status = false;
                return false;
            }
            else
            {
                if ((limit_released_validation) > (document_limit_validation - exiting_limit_validation))

                {
                    values.message = "Limit To be Released Exceeded From Document Limit";

                    values.status = false;
                    return false;
                }
                else
                {

                    msSQL = "select lsacreate_gid from ids_trn_tlimitinfodtl where limitinfodtl_gid = '" + values.limitinfodtl_gid + "'";
                    string lsacreate_gid = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = "select customer_name from ids_trn_tlsa where lsacreate_gid = '" + lsacreate_gid + "'";
                    string customer_name = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select updated_by, date_format(updated_date, '%Y-%m-%d %H:%i:%s') as updated_date from ids_trn_tlimitinfodtl where limitinfodtl_gid = '" + values.limitinfodtl_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                        lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                        if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                        {
                            msGetGID = objcmnfunctions.GetMasterGID("LSUL");
                            msSQL = " insert into ids_trn_tlsaupdatedlog(" +
                                      " lsaupdatedlog_gid," +
                                      " lsacreate_gid," +
                                      " customer_name, " +
                                      " remarks, " +
                                      " lastupdated_by, " +
                                      " lastupdated_date, " +
                                      " created_by, " +
                                      " created_date) " +
                                      " values(" +
                                      "'" + msGetGID + "'," +
                                      "'" + lsacreate_gid + "'," +
                                      "'" + customer_name + "'," +
                                      "'" + "Limit Information" + "'," +
                                      "'" + lsUpdatedBy + "'," +
                                      "'" + lsUpdatedDate + "'," +
                                      "'" + employee_gid + "'," +
                                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                    objODBCDatareader.Close();



                    msSQL = " update ids_trn_tlimitinfodtl set " +
                        " interchangeability='" + values.interchangeability + "'," +
                            " odlim='" + values.odlim.Replace(",", "") + "'," +
                            " report_structure='" + values.report_structure + "'," +
                          " node='" + values.node_edit.Replace("'", "") + "'," +
                            " margin='" + values.margin_edit + "'," +
                            " existing_limit='" + values.existing_limit_edit.Replace(",", "") + "'," +
                            " document_limit='" + values.document_limit_edit.Replace(",", "") + "'," +
                            " limit_released='" + values.limit_released_edit.Replace(",", "") + "'," +
                            " tenure='" + values.tenure_edit.Replace("'", "") + "'," +
                            " revolving_type='" + values.revolving_type_edit + "'," +
                            " sub_limit='" + values.sub_limit_edit.Replace("'", "") + "'," +
                            " rate_interest='" + values.rate_interest_edit + "'," +
                            " facility_type='" + values.facility_type_edit + "'," +
                            " facility_type_gid='" + values.facility_type_gid + "'," +
                            " facility_amount='" + values.facility_amount.Replace(",","") + "'," +
                            " limitinfo_remarks='" + values.limitinfo_remarks_edit.Replace("'", "") + "'," +
                            " change_request='" + values.change_request + "'," +
                            " updated_by='" + employee_gid + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where limitinfodtl_gid='" + values.limitinfodtl_gid + "' ";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (Convert.ToDateTime(values.expiry_date_edit).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                    {

                    }
                    else
                    {
                        msSQL = "update ids_trn_tlimitinfodtl set expiry_date='" + Convert.ToDateTime(values.expiry_date_edit).ToString("yyyy-MM-dd") + "'" +
                        "where  limitinfodtl_gid='" + values.limitinfodtl_gid + "' ";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                   


                    if (mnResult != 0)
                    {
                        msSQL = " update ids_trn_tlimitinfodtl set odlim='" + values.odlim.Replace(",", "") + "' where lsacreate_gid='" + values.lsacreate_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update ids_trn_troidocument set limitinfodtl_gid='" + values.limitinfodtl_gid + "'," +
                               " lsacreate_gid='" + values.lsacreate_gid + "' where " +
                               " (limitinfodtl_gid='" + employee_gid + "' and lsacreate_gid='" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        values.status = true;
                        values.message = "Limit Information Updated Successfully";
                        return true;
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured while updating limiting information";
                        return false;
                    }
                }
            }
        }
        public bool Daupdateprocessingfee(string employee_gid, MdlLsaManagement values)
        {

           
            msSQL = "select customer_name from ids_trn_tlsa where lsacreate_gid = '" + values.lsacreate_gid + "'";
            string customer_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select updated_by, date_format(updated_date, '%Y-%m-%d %H:%i:%s') as updated_date from ids_trn_tprocessingfees where lsacreate_gid = '" + values.lsacreate_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGID = objcmnfunctions.GetMasterGID("LSUL");
                    msSQL = " insert into ids_trn_tlsaupdatedlog(" +
                              " lsaupdatedlog_gid," +
                              " lsacreate_gid," +
                              " customer_name, " +
                              " remarks, " +
                              " lastupdated_by, " +
                              " lastupdated_date, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGID + "'," +
                              "'" + values.lsacreate_gid + "'," +
                              "'" + customer_name + "'," +
                              "'" + "Processing Fee / Commission" + "'," +
                              "'" + lsUpdatedBy + "'," +
                              "'" + lsUpdatedDate + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            if (values.recovered_type=="Already Recovered")
            {
                values.to_be_recoveredamount = "0.00";
                values.recover_remarks = "";
            }
            if (values.recovered_type == "To be Recovered")
            {
                values.recovered_amount = "";
                values.chequeno_details = "";
                values.chequedate_details = "";
                values.bank_name = "";
                values.account_no = "";
            }

            msSQL = " update ids_trn_tprocessingfees set " +
                " recovered_type='" + values.recovered_type + "',";

            if ((values.recovered_amount == null) || (values.recovered_amount == "NaN") || (values.recovered_amount == ""))
            {
                msSQL += " recovered_amount='0.00',";
            }
            else
            {
                msSQL += "recovered_amount='" + values.recovered_amount.Replace(",", "") + "',";
            }
            if (values.chequeno_details == null)
            {
                msSQL += "chequeno_details='',";
            }
            else
            {
                msSQL += "chequeno_details='" + values.chequeno_details + "',";
            }
            if ((values.chequedate_details == null) || (values.chequedate_details == "")) 
            {
                msSQL += "chequedate_details=null,";
            }
            else
            {
                msSQL += "chequedate_details='" + Convert.ToDateTime(values.chequedate_details).ToString("yyyy-MM-dd") + "',";
            }
            if (values.processingfeebank_name == null)
            {
                msSQL += "bank_name='',";
            }
            else
            {
                msSQL += "bank_name='" + values.processingfeebank_name.Replace("'", "") + "',";
            }
            if (values.processingfeaccount_name == null)
            {
                msSQL += "account_no='',";
            }
            else
            {
                msSQL += "account_no='" + values.processingfeaccount_name.Replace("'", "") + "',";
            }
            if ((values.to_be_recoveredamount == null) || (values.to_be_recoveredamount == "NaN") || (values.to_be_recoveredamount == ""))
            {
                msSQL += "to_be_recoveredamount='0.00',";
            }
            else
            {
                msSQL += "to_be_recoveredamount='" + values.to_be_recoveredamount.Replace(",", "") + "',";
            }
            if (values.recover_remarks == null)
            {
                msSQL += "recover_remarks='',";
            }
            else
            {
                msSQL += "recover_remarks='" + values.recover_remarks.Replace("'", "") + "',";
            }
            msSQL += "updated_by='" + employee_gid + "'," +
                    "updated_date=current_timestamp where lsacreate_gid='"+values.lsacreate_gid+"'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
               
                values.message = "Processing Fee / commision updated Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while updating Processing Fee / commision";
                values.status = false;
                return false;
            }

        }

        public bool Daupdatedocumentcharges(string employee_gid, MdlLsaManagement values)
        {

            msSQL = "select customer_name from ids_trn_tlsa where lsacreate_gid = '" + values.lsacreate_gid + "'";
            string customer_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select updated_by, date_format(updated_date, '%Y-%m-%d %H:%i:%s') as updated_date from ids_trn_tdocumentcharges where lsacreate_gid = '" + values.lsacreate_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGID = objcmnfunctions.GetMasterGID("LSUL");
                    msSQL = " insert into ids_trn_tlsaupdatedlog(" +
                              " lsaupdatedlog_gid," +
                              " lsacreate_gid," +
                              " customer_name, " +
                              " remarks, " +
                              " lastupdated_by, " +
                              " lastupdated_date, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGID + "'," +
                              "'" + values.lsacreate_gid + "'," +
                              "'" + customer_name + "'," +
                              "'" + "Documentation / Client Visit Charges" + "'," +
                              "'" + lsUpdatedBy + "'," +
                              "'" + lsUpdatedDate + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            if (values.documentcharge_applicable == "Yes")
            {

                msSQL = " update ids_trn_tdocumentcharges set ";

                if ((values.doc_recovered_amount == null) || (values.doc_recovered_amount == "NaN") || (values.doc_recovered_amount == ""))
                {
                    msSQL += " recovered_amount='0.00',";
                }
                else
                {
                    msSQL += "recovered_amount='" + values.doc_recovered_amount.Replace(",", "") + "',";
                }
                if (values.doc_chequeno_details == null)
                {
                    msSQL += "chequeno_details='',";
                }
                else
                {
                    msSQL += "chequeno_details='" + values.doc_chequeno_details + "',";
                }
                if ((values.doc_chequedate_details == null) || (values.doc_chequedate_details == ""))
                {
                    msSQL += "chequedate_details=null,";
                }
                else
                {
                    msSQL += "chequedate_details='" + Convert.ToDateTime(values.doc_chequedate_details).ToString("yyyy-MM-dd") + "',";
                }
                if (values.doc_feebank_name == null)
                {
                    msSQL += "bank_name='',";
                }
                else
                {
                    msSQL += "bank_name='" + values.doc_feebank_name.Replace("'", "") + "',";
                }
                if (values.doc_feaccount_name == null)
                {
                    msSQL += "account_no='',";
                }
                else
                {
                    msSQL += "account_no='" + values.doc_feaccount_name.Replace("'", "") + "',";
                }

                if (values.documentcharge_remarks == null)
                {
                    msSQL += "documentcharge_remarks='',";
                }
                else
                {
                    msSQL += "documentcharge_remarks='" + values.documentcharge_remarks.Replace("'", "") + "',";
                }
                msSQL += " documentcharge_applicable='" + values.documentcharge_applicable + "',"+
                         " document_charge_gst='" + values.document_charge_gst + "'," +
                         " document_charge='" + values.document_charge.Replace(",", "") + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date=current_timestamp where lsacreate_gid='" + values.lsacreate_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {

                msSQL = " update ids_trn_tdocumentcharges set " +
                        " recovered_amount='0.00'," +
                        " chequeno_details=''," +
                        " chequedate_details=null," +
                        " bank_name=''," +
                        " account_no=''," +
                        " document_charge_gst='',"+
                        " documentcharge_applicable='" + values.documentcharge_applicable+"',";
                if (values.documentcharge_remarks == null)
                {
                    msSQL += "documentcharge_remarks='',";
                }
                else
                {
                    msSQL += "documentcharge_remarks='" + values.documentcharge_remarks.Replace("'", "") + "',";
                }
                msSQL += "document_charge='0.00'," +
                        "updated_by='" + employee_gid + "'," +
                        "updated_date=current_timestamp where lsacreate_gid='" + values.lsacreate_gid + "'";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {

                values.message = "Documentation Charges updated Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while updating Documentation Charges";
                values.status = false;
                return false;
            }

        }
        public void DaGetcreditmanager(Mdlcredit_manager values)
        {
            try
            {
            msSQL = "select concat(c.user_firstname,' ',c.user_lastname,'/',c.user_code) as department_manager from hrm_mst_tdepartment a" +
                 " left join hrm_mst_temployee b on b.employee_gid = a.department_manager" +
                 " left join adm_mst_tuser c on b.user_gid = c.user_gid where a.department_gid = 'HDPM1811210068'";
            values.credit_manager = objdbconn.GetExecuteScalar(msSQL);

               
            values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetTempDelete(result values,string employee_gid)
        {
            try
            {
                msSQL = "delete from ids_tmp_tuploaddocumentcharges where lsacreate_gid ='"+ employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

              
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetdocumentmandatory_check(result values, string employee_gid)
        {
            try
            {
                msSQL = "select * from ids_tmp_tuploaddocumentcharges where lsacreate_gid='" + employee_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    
                        values.status = true;
                   
                }
                else
                {
                    values.message = "Kindly upload  Document";
                    values.status = false;
                }
                objODBCDataReader.Close();
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaPostsecurityinfo(Mdlsecurity values, string employee_gid)
        {

            msGetGID = objcmnfunctions.GetMasterGID("COLT");

            msGetGidREF = objcmnfunctions.GetMasterGID("COL");

            msSQL = " insert into ocs_trn_tcustomercollateral(" +
                    " collateral_gid," +
                    " collateralref_no," +
                    " lsacreate_gid," +
                    " security_type," +
                    " securitytype_gid," +
                    " security_description," +
                    " account_status," +
                    " borrowercheque_no," +
                    " borroweraccount_no," +
                    " borrowertbank_name," +
                    " borrowerdeviation," +
                    " borrowerother_remarks," +
                    " guarantor_cheque," +    
                    " guarantor_acno," +
                    " guarantor_bankname," +
                    " guarantor_deviation," +
                    " personalguarantor_name," +
                    " guarantor_panno," +
                    " corporate_guarantee," +
                    " personal_guarantee," +
                    " fd_bank_name," +
                    " fd_no," +
                    " fd_expiry_date," +
                    " auto_renewal," +
                    " bankguarantee_bankname," +
                    " bankguarantee_expirydate," +
                    " insurancecompany_name," +
                    " policy_no," +
                    " policy_expiry_date," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGID + "'," +
                    "'" + msGetGidREF + "'," +
                    "'" + values.lsacreate_gid + "'," +
                    "'" + values.security_type + "'," +
                    "'" + values.securitytype_gid + "'," +
                    "'" + values.security_description.Replace("'", "") + "'," +
                    "'" + values.account_status + "'," +
                    "'" + values.borrowercheque_no + "'," +
                    "'" + values.borroweraccount_no + "',";


            if (values.borrowertbank_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.borrowertbank_name.Replace("'", "") + "',";

            }
            if (values.borrowerdeviation == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.borrowerdeviation.Replace("'", "") + "',";

            }
            if (values.borrowerother_remarks == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.borrowerother_remarks.Replace("'", "") + "',";

            }
            msSQL += "'" + values.guarantor_cheque + "'," +
                    "'" + values.guarantor_acno + "',";
            if (values.guarantor_bankname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.guarantor_bankname.Replace("'", "") + "',";

            }
            if (values.guarantor_deviation == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.guarantor_deviation.Replace("'", "") + "',";

            }
            if (values.personalguarantor_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.personalguarantor_name.Replace("'", "") + "',";

            }
            if (values.guarantor_panno == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.guarantor_panno.Replace("'", "") + "',";

            }
            if (values.corporate_guarantee == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.corporate_guarantee.Replace("'", "") + "',";

            }
            if (values.personal_guarantee == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.personal_guarantee.Replace("'", "") + "',";

            }
            if (values.fd_bank_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.fd_bank_name.Replace("'", "") + "',";

            }
            if (values.fd_no == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.fd_no.Replace("'", "") + "',";

            }
            if (values.fd_expiry_date == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.fd_expiry_date).ToString("yyyy-MM-dd") + "',";

            }
            if (values.auto_renewal == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.auto_renewal.Replace("'", "") + "',";

            }
            if (values.bankguarantee_bankname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.bankguarantee_bankname.Replace("'", "") + "',";

            }
            if (values.bankguarantee_expirydate == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.bankguarantee_expirydate).ToString("yyyy-MM-dd") + "',";

            }
            if (values.insurancecompany_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.insurancecompany_name.Replace("'", "") + "',";

            }
            if (values.policy_no == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.policy_no.Replace("'", "") + "',";

            }
            if (values.policy_expiry_date == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.policy_expiry_date).ToString("yyyy-MM-dd") + "',";

            }
            msSQL +=  "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



            if (mnResult != 0)
            {
                msSQL = " update ids_trn_tlsa set collateralsecurity_flag='Y'  where lsacreate_gid='" + values.lsacreate_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Collateral Details Added Sucessfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while adding Collateral Details";
            }
        }
        public void DaGetsecurityinfo_delete(string collateral_gid, result values)
        {
            msSQL = "delete from ocs_trn_tcustomercollateral where collateral_gid='" + collateral_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }
        public void DaGetsanction2Colletarl(string lsacreate_gid, Mdlsecurity values, string employee_gid)
        {
            msSQL = " select collateral_gid,b.security_type,security_description,account_status,collateralref_no,security_code" +
                  " from ocs_trn_tcustomercollateral a,ocs_trn_tsecuritytype b where a.securitytype_gid=b.securitytype_gid" +
                  " and  a.lsacreate_gid='"+ lsacreate_gid +"'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<customersecurity_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new customersecurity_list
                    {
                        security_type = (dr_datarow["security_type"].ToString()),
                        security_description = (dr_datarow["security_description"].ToString()),
                        account_status = dr_datarow["account_status"].ToString(),
                        collateralref_no = dr_datarow["collateralref_no"].ToString(),
                        security_code = dr_datarow["security_code"].ToString(),
                        collateral_gid = dr_datarow["collateral_gid"].ToString()
                    });
                }
                values.customersecurity_list = get_filename;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaGetColletarl(string collateral_gid, Mdlsecurity values, string employee_gid)
        {
            msSQL =" select collateral_gid,b.security_type,security_description,account_status,collateralref_no,security_code,a.securitytype_gid,collateralref_no,"+
                   " borrowercheque_no,borroweraccount_no,borrowertbank_name,borrowerdeviation,borrowerother_remarks,guarantor_cheque,guarantor_acno,"+
                   " guarantor_bankname,guarantor_deviation,personalguarantor_name,guarantor_panno,corporate_guarantee,personal_guarantee,fd_bank_name,"+
                   " fd_no,fd_expiry_date,auto_renewal,bankguarantee_bankname,bankguarantee_expirydate,insurancecompany_name,policy_no,policy_expiry_date,lsacreate_gid," +
                   " date_format(fd_expiry_date,'%d-%m-%Y') as fd_date,date_format(bankguarantee_expirydate,'%d-%m-%Y') as bank_expirydate,date_format(policy_expiry_date,'%d-%m-%Y') as policy_date" +
                   " from ocs_trn_tcustomercollateral a,ocs_trn_tsecuritytype b where a.securitytype_gid=b.securitytype_gid" +
                   " and  a.collateral_gid='" + collateral_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader .HasRows ==true)
            {
                values.security_type = objODBCDataReader["security_type"].ToString();
                values.security_description = objODBCDataReader["security_description"].ToString();
                values.account_status = objODBCDataReader["account_status"].ToString();
                values.collateralref_no = objODBCDataReader["collateralref_no"].ToString();
                values.security_code = objODBCDataReader["security_code"].ToString();
                values.collateral_gid = objODBCDataReader["collateral_gid"].ToString();
                values.borrowercheque_no = objODBCDataReader["borrowercheque_no"].ToString();
                values.borroweraccount_no = objODBCDataReader["borroweraccount_no"].ToString();
                values.borrowertbank_name = objODBCDataReader["borrowertbank_name"].ToString();
                values.borrowerdeviation = objODBCDataReader["borrowerdeviation"].ToString();
                values.borrowerother_remarks = objODBCDataReader["borrowerother_remarks"].ToString();
                values.guarantor_cheque = objODBCDataReader["guarantor_cheque"].ToString();
                values.guarantor_acno = objODBCDataReader["guarantor_acno"].ToString();
                values.guarantor_bankname = objODBCDataReader["guarantor_bankname"].ToString();
                values.guarantor_deviation = objODBCDataReader["guarantor_deviation"].ToString();
                values.personalguarantor_name = objODBCDataReader["personalguarantor_name"].ToString();
                values.guarantor_panno = objODBCDataReader["guarantor_panno"].ToString();
                values.corporate_guarantee = objODBCDataReader["corporate_guarantee"].ToString();
                values.personal_guarantee = objODBCDataReader["personal_guarantee"].ToString();
                values.fd_bank_name = objODBCDataReader["fd_bank_name"].ToString();
                values.fd_no = objODBCDataReader["fd_no"].ToString();
                values.fd_expiry_date = objODBCDataReader["fd_date"].ToString();
                values.auto_renewal = objODBCDataReader["auto_renewal"].ToString();
                values.bankguarantee_bankname = objODBCDataReader["bankguarantee_bankname"].ToString();
                values.bankguarantee_expirydate = objODBCDataReader["bank_expirydate"].ToString();
                values.insurancecompany_name = objODBCDataReader["insurancecompany_name"].ToString();
                values.policy_no = objODBCDataReader["policy_no"].ToString();
                   values.policy_expiry_date = objODBCDataReader["policy_date"].ToString();
                values.lsacreate_gid = objODBCDataReader["lsacreate_gid"].ToString();
                values.securitytype_gid = objODBCDataReader["securitytype_gid"].ToString();
                if (objODBCDataReader["fd_expiry_date"].ToString() != "")
                {
                    values.fdexpiry_date = Convert.ToDateTime(objODBCDataReader["fd_expiry_date"].ToString());
                }
                if (objODBCDataReader["bankguarantee_expirydate"].ToString() != "")
                {
                    values.bankguarantee_expiry_date = Convert.ToDateTime(objODBCDataReader["bankguarantee_expirydate"].ToString());
                }
                if (objODBCDataReader["policy_expiry_date"].ToString() != "")
                {
                    values.policyexpiry_date = Convert.ToDateTime(objODBCDataReader["policy_expiry_date"].ToString());
                }
            }

            objODBCDataReader.Close();        
                   
            values.status = true;
        }

        public bool Daupdatesecurityinfo(Mdlsecurity values, string employee_gid)
        {

            msSQL = "select customer_name from ids_trn_tlsa where lsacreate_gid = '" + values.lsacreate_gid + "'";
            string customer_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select updated_by, date_format(updated_date, '%Y-%m-%d %H:%i:%s') as updated_date from ocs_trn_tcustomercollateral where collateral_gid = '" + values.collateral_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGID = objcmnfunctions.GetMasterGID("LSUL");
                    msSQL = " insert into ids_trn_tlsaupdatedlog(" +
                              " lsaupdatedlog_gid," +
                              " lsacreate_gid," +
                              " customer_name, " +
                              " remarks, " +
                              " lastupdated_by, " +
                              " lastupdated_date, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGID + "'," +
                              "'" + values.lsacreate_gid + "'," +
                              "'" + customer_name + "'," +
                              "'" + "CAD Internal Usage" + "'," +
                              "'" + lsUpdatedBy + "'," +
                              "'" + lsUpdatedDate + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = " update ocs_trn_tcustomercollateral set" +
                    " security_type='" + values.security_type.Replace(" ", "") + "'," +
                    " securitytype_gid='" + values.securitytype_gid + "'," +
                    " security_description='" + values.security_description.Replace("'", "") + "'," +
                    " account_status='" + values.account_status + "'," +
                    " borrowercheque_no='" + values.borrowercheque_no + "'," +
                    " borroweraccount_no='" + values.borroweraccount_no + "',";
            if (values.borrowertbank_name == null)
            {
                msSQL += "borrowertbank_name='',";
            }
            else
            {
                msSQL += "borrowertbank_name='" + values.borrowertbank_name.Replace("'", "") + "',";

            }
            if (values.borrowerdeviation == null)
            {
                msSQL += "borrowerdeviation='',";
            }
            else
            {
                msSQL += "borrowerdeviation='" + values.borrowerdeviation.Replace("'", "") + "',";

            }
            if (values.borrowerother_remarks == null)
            {
                msSQL += "borrowerother_remarks='',";
            }
            else
            {
                msSQL += "borrowerother_remarks='" + values.borrowerother_remarks.Replace("'", "") + "',";

            }
            if (values.guarantor_cheque == null)
            {
                msSQL += "borrowerother_remarks='',";
            }
            else
            {
                msSQL += "guarantor_cheque='" + values.guarantor_cheque.Replace("'", "") + "',";

            }
            if (values.guarantor_acno == null)
            {
                msSQL += "guarantor_acno='',";
            }
            else
            {
                msSQL += "guarantor_acno='" + values.guarantor_acno.Replace("'", "") + "',";

            }
            if (values.guarantor_bankname == null)
            {
                msSQL += "guarantor_bankname='',";
            }
            else
            {
                msSQL += "guarantor_bankname='" + values.guarantor_bankname.Replace("'", "") + "',";

            }
            if (values.guarantor_deviation == null)
            {
                msSQL += "guarantor_deviation='',";
            }
            else
            {
                msSQL += "guarantor_deviation='" + values.guarantor_deviation.Replace("'", "") + "',";

            }
            if (values.personalguarantor_name == null)
            {
                msSQL += "personalguarantor_name='',";
            }
            else
            {
                msSQL += "personalguarantor_name='" + values.personalguarantor_name.Replace("'", "") + "',";

            }
            if (values.guarantor_panno == null)
            {
                msSQL += "guarantor_panno='',";
            }
            else
            {
                msSQL += "guarantor_panno='" + values.guarantor_panno.Replace("'", "") + "',";

            }
            if (values.corporate_guarantee == null)
            {
                msSQL += "corporate_guarantee='',";
            }
            else
            {
                msSQL += "corporate_guarantee='" + values.corporate_guarantee.Replace("'", "") + "',";

            }
            if (values.personal_guarantee == null)
            {
                msSQL += "personal_guarantee='',";
            }
            else
            {
                msSQL += "personal_guarantee='" + values.personal_guarantee.Replace("'", "") + "',";

            }
            if (values.fd_bank_name == null)
            {
                msSQL += "fd_bank_name='',";
            }
            else
            {
                msSQL += "fd_bank_name='" + values.fd_bank_name.Replace("'", "") + "',";

            }
            if (values.fd_bank_name == null)
            {
                msSQL += "fd_no='',";
            }
            else
            {
                msSQL += "fd_no='" + values.fd_no.Replace("'", "") + "',";

            }
            if (values.fd_expiry_date == null)
            {
                msSQL += "fd_expiry_date=null,";
            }
            else
            {
                msSQL += "fd_expiry_date='" + Convert.ToDateTime(values.fd_expiry_date).ToString("yyyy-MM-dd") + "',";

            }
            if (values.auto_renewal == null)
            {
                msSQL += "auto_renewal='',";
            }
            else
            {
                msSQL += "auto_renewal='" + values.auto_renewal.Replace("'", "") + "',";

            }
            if (values.bankguarantee_bankname == null)
            {
                msSQL += "bankguarantee_bankname='',";
            }
            else
            {
                msSQL += "bankguarantee_bankname='" + values.bankguarantee_bankname.Replace("'", "") + "',";

            }
            if (values.bankguarantee_expirydate == null)
            {
                msSQL += "bankguarantee_expirydate=null,";
            }
            else
            {
                msSQL += "bankguarantee_expirydate='" + Convert.ToDateTime(values.bankguarantee_expirydate).ToString("yyyy-MM-dd") + "',";

            }
            if (values.insurancecompany_name == null)
            {
                msSQL += "insurancecompany_name='',";
            }
            else
            {
                msSQL += "insurancecompany_name='" + values.insurancecompany_name.Replace("'", "") + "',";

            }
            if (values.policy_no == null)
            {
                msSQL += "policy_no='',";
            }
            else
            {
                msSQL += "policy_no='" + values.policy_no.Replace("'", "") + "',";

            }
            if (values.policy_expiry_date == null)
            {
                msSQL += "policy_expiry_date=null,";
            }
            else
            {
                msSQL += "policy_expiry_date='" + Convert.ToDateTime(values.policy_expiry_date).ToString ("yyyy-MM-dd") + "',";

            }

            msSQL += "updated_by='" + employee_gid + "'," +
                       "updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where collateral_gid='" + values.collateral_gid+"'";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Collateral Security Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating Collateral Security";
                return false;
            }

        }

        public bool DaGetloanfacilityamount(Mdlloanfacility_type values)
        {
          
            msSQL = "select customer2sanction_gid from ids_trn_tlsa where lsacreate_gid='" + values.lsacreate_gid + "'";
            string customer2sanction_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select format(loanfacility_amount,2,'en_IN') as loanfacility_amount,loanfacility_type, applicable_condition," +
                " format(document_limit,2,'en_IN') as document_limit,margin, expiry_date,revolving_type,tenure," +
                " interchangeability,if(report_structure='','---',report_structure) as report_structure,loanfacilityref_no,proposed_roi " +
                " from ocs_mst_tsanction2loanfacilitytype where sanction2loanfacilitytype_gid='" + values.loanfacility_gid + "' "+
                " and customer2sanction_gid='" + customer2sanction_gid + "' ";
            
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {              
                values.margin = objODBCDataReader["margin"].ToString();
                if (objODBCDataReader["expiry_date"].ToString() == "")
                {
                }
                else
                {
                    values.expiry_date = Convert.ToDateTime(objODBCDataReader["expiry_date"]).ToString("MM-dd-yyyy");
                }
                values.revolving_type = objODBCDataReader["revolving_type"].ToString();
                values.tenure = objODBCDataReader["tenure"].ToString();
                values.interchangeability = objODBCDataReader["interchangeability"].ToString();
                lsinterchangeability= objODBCDataReader["interchangeability"].ToString();
                values.report_structure = objODBCDataReader["report_structure"].ToString();
                values.loanfacility_amount = objODBCDataReader["loanfacility_amount"].ToString();
                values.document_limit = objODBCDataReader["document_limit"].ToString();
                lsapplicable_condition= objODBCDataReader["applicable_condition"].ToString();
                values.proposed_roi = objODBCDataReader["proposed_roi"].ToString();

            }
            objODBCDataReader.Close();
            msSQL = "select loanfacilityref_no" +
               " from ocs_mst_tsanction2loanfacilitytype where customer2sanction_gid='" + customer2sanction_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<loanfacilitytype_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new loanfacilitytype_list
                    {


                        loanfacilityref_no = dr_datarow["loanfacilityref_no"].ToString()
                    });
                }
                values.loanfacilitytype_list = get_filename;
            }
            dt_datatable.Dispose();

            msSQL = "select lsacreate_gid from ids_trn_tlsa where  customer2sanction_gid='" + customer2sanction_gid + "' order by created_date desc  limit 1,1";
            string lslsacreate_gid = objdbconn.GetExecuteScalar(msSQL);
            if(lslsacreate_gid!="")
            {

            if(lsinterchangeability=="No")
            {
                if(lsapplicable_condition=="No")
                {

            msSQL = "select (existing_limit+limit_released)  from ids_trn_tlimitinfodtl  a " +
                   " where lsacreate_gid ='"+ lslsacreate_gid + "' and facility_type_gid ='"+values.loanfacility_gid + "'";

            lsexisting_limit = objdbconn.GetExecuteScalar(msSQL);

                }
                else
                {
                    msSQL = "select (sum(existing_limit)+sum(limit_released)) from ids_trn_tlimitinfodtl  a " +
                                    " where lsacreate_gid ='" + lslsacreate_gid + "' and facility_type_gid ='" + values.loanfacility_gid + "'";
                        lsexisting_limit = objdbconn.GetExecuteScalar(msSQL);
                }
            }
            else
            {
                msSQL = "select (sum(existing_limit)+sum(limit_released)) from ids_trn_tlimitinfodtl  a " +
                 " where lsacreate_gid ='" + lslsacreate_gid + "' and facility_type_gid ='" + values.loanfacility_gid + "'";
                    lsexisting_limit = objdbconn.GetExecuteScalar(msSQL);
            }
            if ((lsexisting_limit=="")||(lsexisting_limit==null))
            {
                values.existing_limit = "0,0";
            }
            else
            {

            decimal parsed4 = decimal.Parse(lsexisting_limit, System.Globalization.CultureInfo.InvariantCulture);
            System.Globalization.CultureInfo indian_format4 = new System.Globalization.CultureInfo("hi-IN");
            string text4 = string.Format(indian_format4, "{0:c}", parsed4);

            msSQL = "select substring('" + text4 + "',2,20)";
            string existing_limit = objdbconn.GetExecuteScalar(msSQL);

            values.existing_limit = existing_limit;

                }

            }
            else
            {
                msSQL = "select lsacreate_gid from ids_trn_tlsa where  customer2sanction_gid='" + customer2sanction_gid + "' order by created_date desc";
                lslsacreate_gid = objdbconn.GetExecuteScalar(msSQL);
                if (lsinterchangeability == "No")
                {
                    if (lsapplicable_condition == "No")
                    {

                        msSQL = "select (existing_limit+limit_released)  from ids_trn_tlimitinfodtl  a " +
                               " where lsacreate_gid ='" + lslsacreate_gid + "' and facility_type_gid ='" + values.loanfacility_gid + "'";

                        lsexisting_limit = objdbconn.GetExecuteScalar(msSQL);

                    }
                    else
                    {
                        msSQL = "select (sum(existing_limit)+sum(limit_released)) from ids_trn_tlimitinfodtl  a " +
                                        " where lsacreate_gid ='" + lslsacreate_gid + "' and facility_type_gid ='" + values.loanfacility_gid + "'";
                        lsexisting_limit = objdbconn.GetExecuteScalar(msSQL);
                    }
                }
                else

                {
                    msSQL = "select (sum(existing_limit)+sum(limit_released)) from ids_trn_tlimitinfodtl  a " +
                     " where lsacreate_gid ='" + lslsacreate_gid + "' and facility_type_gid ='" + values.loanfacility_gid + "'";
                    lsexisting_limit = objdbconn.GetExecuteScalar(msSQL);
                }
                if ((lsexisting_limit == "") || (lsexisting_limit == null))
                {
                    values.existing_limit = "0,0";
                }
                else
                {

                    decimal parsed4 = decimal.Parse(lsexisting_limit, System.Globalization.CultureInfo.InvariantCulture);
                    System.Globalization.CultureInfo indian_format4 = new System.Globalization.CultureInfo("hi-IN");
                    string text4 = string.Format(indian_format4, "{0:c}", parsed4);

                    msSQL = "select substring('" + text4 + "',2,20)";
                    string existing_limit = objdbconn.GetExecuteScalar(msSQL);

                    values.existing_limit = existing_limit;

                }
            }
            values.status = true;
            return true;
        }
        public bool DaGetEditloanfacilityamount(Mdlloanfacility_type values)
        {
            msSQL = "select lsacreate_gid from ids_trn_tlimitinfodtl where limitinfodtl_gid='" + values.limitinfodtl_gid + "'";
            string lsacreate_gid = objdbconn.GetExecuteScalar(msSQL);


            msSQL = "select customer2sanction_gid from ids_trn_tlsa where lsacreate_gid='" + lsacreate_gid + "'";

            string customer2sanction_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select format(loanfacility_amount,2,'en_IN') as loanfacility_amount,loanfacility_type, " +
               " format(document_limit,2,'en_IN') as document_limit,margin, expiry_date,revolving_type,tenure," +
               " interchangeability,if(report_structure='','---',report_structure) as report_structure,loanfacilityref_no,proposed_roi " +
               " from ocs_mst_tsanction2loanfacilitytype where customer2sanction_gid='" + customer2sanction_gid + "' " +
               " and sanction2loanfacilitytype_gid='" + values.loanfacility_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == true)
            {

                values.margin = objODBCDataReader["margin"].ToString();
                if (objODBCDataReader["expiry_date"].ToString() == "")
                {
                }
                else
                {
                    values.expiry_date = Convert.ToDateTime(objODBCDataReader["expiry_date"]).ToString("MM-dd-yyyy");
                }
                values.revolving_type = objODBCDataReader["revolving_type"].ToString();
                values.tenure = objODBCDataReader["tenure"].ToString();
                values.interchangeability = objODBCDataReader["interchangeability"].ToString();
                values.report_structure = objODBCDataReader["report_structure"].ToString();
                values.loanfacility_amount = objODBCDataReader["loanfacility_amount"].ToString();
                values.document_limit = objODBCDataReader["document_limit"].ToString();
                values.proposed_roi= objODBCDataReader["proposed_roi"].ToString();

            }
            objODBCDataReader.Close();
            msSQL = "select loanfacilityref_no" +
               " from ocs_mst_tsanction2loanfacilitytype where customer2sanction_gid='" + customer2sanction_gid + "' ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<loanfacilitytype_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new loanfacilitytype_list
                    {

                        loanfacilityref_no = dr_datarow["loanfacilityref_no"].ToString()
                    });
                }
                values.loanfacilitytype_list = get_filename;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        public bool Dapostdocumentchargeapplicable(string employee_gid, MdlLsaManagement values)
        {
            msGetGID = objcmnfunctions.GetMasterGID("LDCH");

            msSQL = " insert into ids_trn_tdocumentcharges(" +
                " documentcharge_gid," +
                " lsacreate_gid," +
                " documentcharge_applicable," +
                " documentcharge_remarks," +
                " created_by," +
                " created_date)" +
                " values(" +
                "'" + msGetGID + "'," +
                "'" + values.lsacreate_gid + "'," +
                "'" + values.documentcharge_applicable + "',";
            if((values.documentcharge_remarks==null) || (values.documentcharge_remarks=="") || (values.documentcharge_remarks == "undefined"))
            {
                msSQL += "null,";
            }
            {
                msSQL += "'" + values.documentcharge_remarks.Replace("'", "") + "',";
            }
                                  
                msSQL+="'" + employee_gid + "'," +
                    "current_timestamp)";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update ids_trn_tlsa set document_charge_flag='Y' where lsacreate_gid='" + values.lsacreate_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

               
                values.message = "Documentation charges Added Successfully";
                values.status = true;
                return true;
            }
            else
            {
                values.message = "Error Occured while adding Documentation charges ";
                values.status = false;
                return false;
            }

        }


        public void DaGetsanction2loanfacility(string lsacreate_gid,MdlLsaManagement values)
        {
            try
            {
                msSQL = "select customer2sanction_gid from ids_trn_tlsa where lsacreate_gid='" + lsacreate_gid + "'";
                string lscustomer2sanction_gid = objdbconn.GetExecuteScalar(msSQL);

             msSQL= " select sanction2loanfacilitytype_gid,concat(loanfacility_type,'-',loanfacilityref_no) as loanfacility_type from ocs_mst_tsanction2loanfacilitytype " +
                    " where customer2sanction_gid='" + lscustomer2sanction_gid + "'";

                  dt_datatable = objdbconn.GetDataTable(msSQL);
                var getfacility = new List<loanfacility_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getfacility.Add(new loanfacility_list
                        {
                            loanmaster_gid = (dr_datarow["sanction2loanfacilitytype_gid"].ToString()),
                            loan_title = (dr_datarow["loanfacility_type"].ToString()),
                        });
                    }
                    values.loanfacility_list = getfacility;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void Daeditsanction2loanfacility(string limitinfodtl_gid, MdlLsaManagement values)
        {
            try
            {
                msSQL = "select lsacreate_gid from ids_trn_tlimitinfodtl where limitinfodtl_gid ='" + limitinfodtl_gid + "'";
                string lsacreate_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select customer2sanction_gid from ids_trn_tlsa where lsacreate_gid='" + lsacreate_gid + "'";
                string lscustomer2sanction_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select sanction2loanfacilitytype_gid,concat(loanfacility_type,'-',loanfacilityref_no) as loanfacility_type from ocs_mst_tsanction2loanfacilitytype " +
                       " where customer2sanction_gid='" + lscustomer2sanction_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getfacility = new List<loanfacility_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getfacility.Add(new loanfacility_list
                        {
                            loanmaster_gid = (dr_datarow["sanction2loanfacilitytype_gid"].ToString()),
                            loan_title = (dr_datarow["loanfacility_type"].ToString()),
                        });
                    }
                    values.loanfacility_list = getfacility;
                }
                dt_datatable.Dispose();

                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void Daupdatelsa (string employee_gid, MdlLsaManagement values)
        {

            msSQL = "select updated_by, date_format(updated_date, '%Y-%m-%d %H:%i:%s') as updated_date, customer_name from ids_trn_tlsa where lsacreate_gid = '" + values.lsacreate_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGID = objcmnfunctions.GetMasterGID("LSUL");
                    msSQL = " insert into ids_trn_tlsaupdatedlog(" +
                              " lsaupdatedlog_gid," +
                              " lsacreate_gid," +
                              " customer_name, " +
                              " remarks, " +
                              " lastupdated_by, " +
                              " lastupdated_date, " +
                              " created_by, " +
                              " created_date) " +                              
                              " values(" +
                              "'" + msGetGID + "'," +
                              "'" + values.lsacreate_gid + "'," +
                              "'" + objODBCDatareader["customer_name"].ToString() + "'," +
                              "'" + "Basic LSA Information" + "'," +
                              "'" + lsUpdatedBy + "'," +
                              "'" + lsUpdatedDate + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();


            msSQL = " update ids_trn_tlsa set ";
            if ((values.hypothecation_date == null) || (values.hypothecation_date == "undefined"))
            {
                msSQL += "hypothecation_date=null,";
            }
            else
            {
                msSQL += "hypothecation_date='" + Convert.ToDateTime(values.hypothecation_date).ToString("yyyy-MM-dd") + "',";
            }
            if ((values.mortgage_date == null) || (values.mortgage_date == "undefined"))
            {
                msSQL += "mortgage_date=null,";
            }
            else
            {
                msSQL += "mortgage_date='" + Convert.ToDateTime(values.mortgage_date).ToString("yyyy-MM-dd") + "',";
            }
            msSQL+= " sa_code='" + values.sa_code + "'," +
                   " remarks='" +values.remarks+"',"+
                   " updated_by='" + employee_gid + "'," +
                   " updated_date=current_timestamp where lsacreate_gid='" + values.lsacreate_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult!=0)
            {
                values.message = "LSA Information Updated Successfully";
                values.status = true;

            }
            else
            {
                values.message = "Error Occured wile updating LSA Information ";
                values.status = false;
            }
        }

        public void Dapostapprovalstatuslsa(string employee_gid, MdlLsaManagement values)
        {
            msSQL = "select count(*) from ids_trn_tlimitinfodtl where lsacreate_gid='" + values.lsacreate_gid + "' group by lsacreate_gid";
            string lslimitcount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select customer2sanction_gid from ids_trn_tlsa where lsacreate_gid='" + values.lsacreate_gid + "'";
            string lscustomer2sanction_gid = objdbconn.GetExecuteScalar(msSQL);


            msSQL = "select count(*) from ocs_mst_tsanction2loanfacilitytype where customer2sanction_gid='" + lscustomer2sanction_gid + "' group by customer2sanction_gid";
            string lssanctioncount = objdbconn.GetExecuteScalar(msSQL);
            if (lslimitcount == lssanctioncount)
            {
                msSQL = "update ids_trn_tlsa set approval_status='Pending'," +
                " approvalsent_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                " approvalsent_by='" + employee_gid + "'" +
                 " where lsacreate_gid='" + values.lsacreate_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.message = "LSA Approval updated Successfully";
                    values.status = true;

                }
                else
                {
                    values.message = "Error Occured wile updating LSA Approval ";
                    values.status = false;
                }
            }
            else
            {
                values.message = "Kindly check the limit information";
                values.status = false;
            }
        }
        public bool DagetLSAapprovalpendinginfo(LSApending_list values)
        {
            msSQL = " select lsacreate_gid,a.customer_name,branch_name,a.state,a.customer_urn,customer_location,a.rm_name,business_head,business_head," +
                    " cluster_head,zonal_head,a.credit_manager,sanctionref_no,date_format(a.sanction_date, '%d-%m-%Y') as sanction_date,customer_address, " +
                    " approved_by,date_format(approved_date, '%d-%m-%Y') as approved_date,gst_no,a.pan_no,a.purpose_lending,facility, " +
                    " major_corporate,date_format(hypothecation_date, '%d-%m-%Y') as hypothecation_date,date_format(lsacreated_date,'%d-%m-%Y %h:%i %p') as lsacreated_date, " +
                    " date_format(mortgage_date, '%d-%m-%Y') as mortgage_date,a.product_solution,majot_intervention,sector,primaryvalue_chain,secondaryvalue_chain, " +
                    " a.remarks,a.vertical,sa_code,a.constitution,lsaref_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,proceed_flag, " +
                    " approval_status,(SELECT COUNT(*) FROM ids_trn_tsanctiondocumentdtls e " +
                    " WHERE d.customer2sanction_gid = e.sanction_gid GROUP BY d.customer2sanction_gid) AS tagged_document," +                       
                    " (SELECT COUNT(*) FROM ids_trn_tsanctiondocumentdtls e " +
                    " WHERE d.customer2sanction_gid = e.sanction_gid AND e.checker_status = 'Checker Confirmed' " +
                    " GROUP BY d.customer2sanction_gid) AS checkerconfirmed_count from ids_trn_tlsa a " +
                    " left join ocs_mst_tcustomer2sanction d on d.customer2sanction_gid=a.customer2sanction_gid " +                     
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid " +
                    " left join adm_mst_tuser c on b.user_gid = c.user_gid where a.approval_status='Pending' order by lsacreate_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlsa_list = new List<lsa_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlsa_list.Add(new lsa_list
                    {
                        lsacreate_gid = dr_datarow["lsacreate_gid"].ToString(),
                        sanctionref_no = (dr_datarow["sanctionref_no"].ToString()),
                        branch_name = dr_datarow["branch_name"].ToString(),
                        state = (dr_datarow["state"].ToString()),
                        customer_name = dr_datarow["customer_name"].ToString(),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        customer_location = dr_datarow["customer_location"].ToString(),
                        address = (dr_datarow["customer_address"].ToString()),
                        rm_name = dr_datarow["rm_name"].ToString(),
                        zonal_head = (dr_datarow["zonal_head"].ToString()),
                        cluster_head = dr_datarow["cluster_head"].ToString(),
                        credit_manager = (dr_datarow["credit_manager"].ToString()),
                        business_head = dr_datarow["business_head"].ToString(),
                        sanction_date = (dr_datarow["sanction_date"].ToString()),
                        approved_by = dr_datarow["approved_by"].ToString(),
                        approved_date = (dr_datarow["approved_date"].ToString()),
                        gst_no = dr_datarow["gst_no"].ToString(),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        purpose_lending = dr_datarow["purpose_lending"].ToString(),
                        major_corporate = (dr_datarow["major_corporate"].ToString()),
                        majot_intervention = dr_datarow["majot_intervention"].ToString(),
                        primaryvalue_chain = (dr_datarow["primaryvalue_chain"].ToString()),
                        secondaryvalue_chain = dr_datarow["secondaryvalue_chain"].ToString(),
                        sector = (dr_datarow["sector"].ToString()),
                        product_solution = dr_datarow["product_solution"].ToString(),
                        hypothecation_date = (dr_datarow["hypothecation_date"].ToString()),
                        mortgage_date = dr_datarow["mortgage_date"].ToString(),
                        remarks = dr_datarow["remarks"].ToString(),
                        lsaref_no = dr_datarow["lsaref_no"].ToString(),
                        lsacreated_date = dr_datarow["lsacreated_date"].ToString(),
                        vertical = dr_datarow["vertical"].ToString(),
                        created_by = dr_datarow["created_by"].ToString(),
                        proceed_flag = dr_datarow["proceed_flag"].ToString(),
                        approval_status = dr_datarow["approval_status"].ToString(),
                        tagged_document = dr_datarow["tagged_document"].ToString(),
                        checkerconfirmed_count = dr_datarow["checkerconfirmed_count"].ToString()
                    });
                }
                values.lsa_list = getlsa_list;
            }
            dt_datatable.Dispose();
            msSQL = " select lsacreate_gid,customer_name,branch_name,state,customer_urn,customer_location,rm_name,business_head,business_head," +
                      " cluster_head,zonal_head,credit_manager,sanctionref_no,date_format(sanction_date, '%d-%m-%Y') as sanction_date,customer_address," +
                      " approved_by,date_format(approved_date, '%d-%m-%Y') as approved_date,gst_no,a.pan_no,purpose_lending,facility," +
                      " major_corporate,date_format(hypothecation_date, '%d-%m-%Y') as hypothecation_date,date_format(lsacreated_date,'%d-%m-%Y %h:%i %p') as lsacreated_date," +
                      " date_format(mortgage_date, '%d-%m-%Y') as mortgage_date,product_solution,majot_intervention,sector,primaryvalue_chain,secondaryvalue_chain," +
                      " a.remarks,vertical,sa_code,constitution,lsaref_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,proceed_flag, " +
                      " approval_status from ids_trn_tlsa a" +
                      " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                       " left join adm_mst_tuser c on b.user_gid = c.user_gid where a.approval_status='Approved' order by lsacreate_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapprovedlsa_list = new List<approvedlsa_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getapprovedlsa_list.Add(new approvedlsa_list
                    {
                        lsacreate_gid = dr_datarow["lsacreate_gid"].ToString(),
                        sanctionref_no = (dr_datarow["sanctionref_no"].ToString()),
                        branch_name = dr_datarow["branch_name"].ToString(),
                        state = (dr_datarow["state"].ToString()),
                        customer_name = dr_datarow["customer_name"].ToString(),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        customer_location = dr_datarow["customer_location"].ToString(),
                        address = (dr_datarow["customer_address"].ToString()),
                        rm_name = dr_datarow["rm_name"].ToString(),
                        zonal_head = (dr_datarow["zonal_head"].ToString()),
                        cluster_head = dr_datarow["cluster_head"].ToString(),
                        credit_manager = (dr_datarow["credit_manager"].ToString()),
                        business_head = dr_datarow["business_head"].ToString(),
                        sanction_date = (dr_datarow["sanction_date"].ToString()),
                        approved_by = dr_datarow["approved_by"].ToString(),
                        approved_date = (dr_datarow["approved_date"].ToString()),
                        gst_no = dr_datarow["gst_no"].ToString(),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        purpose_lending = dr_datarow["purpose_lending"].ToString(),
                        major_corporate = (dr_datarow["major_corporate"].ToString()),
                        majot_intervention = dr_datarow["majot_intervention"].ToString(),
                        primaryvalue_chain = (dr_datarow["primaryvalue_chain"].ToString()),
                        secondaryvalue_chain = dr_datarow["secondaryvalue_chain"].ToString(),
                        sector = (dr_datarow["sector"].ToString()),
                        product_solution = dr_datarow["product_solution"].ToString(),
                        hypothecation_date = (dr_datarow["hypothecation_date"].ToString()),
                        mortgage_date = dr_datarow["mortgage_date"].ToString(),
                        remarks = dr_datarow["remarks"].ToString(),
                        lsaref_no = dr_datarow["lsaref_no"].ToString(),
                        lsacreated_date = dr_datarow["lsacreated_date"].ToString(),
                        vertical = dr_datarow["vertical"].ToString(),
                        created_by = dr_datarow["created_by"].ToString(),
                        proceed_flag = dr_datarow["proceed_flag"].ToString(),
                        approval_status = dr_datarow["approval_status"].ToString()
                    });
                }
                values.approvedlsa_list = getapprovedlsa_list;
            }
            dt_datatable.Dispose();
            msSQL = " select lsacreate_gid,customer_name,branch_name,state,customer_urn,customer_location,rm_name,business_head,business_head," +
                      " cluster_head,zonal_head,credit_manager,sanctionref_no,date_format(sanction_date, '%d-%m-%Y') as sanction_date,customer_address," +
                      " approved_by,date_format(approved_date, '%d-%m-%Y') as approved_date,gst_no,a.pan_no,purpose_lending,facility," +
                      " major_corporate,date_format(hypothecation_date, '%d-%m-%Y') as hypothecation_date,date_format(lsacreated_date,'%d-%m-%Y %h:%i %p') as lsacreated_date," +
                      " date_format(mortgage_date, '%d-%m-%Y') as mortgage_date,product_solution,majot_intervention,sector,primaryvalue_chain,secondaryvalue_chain," +
                      " a.remarks,vertical,sa_code,constitution,lsaref_no,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,proceed_flag, " +
                      " approval_status from ids_trn_tlsa a" +
                      " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                       " left join adm_mst_tuser c on b.user_gid = c.user_gid where a.approval_status='Rejected' order by lsacreate_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrejectedlsa_list = new List<rejectedlsa_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getrejectedlsa_list.Add(new rejectedlsa_list
                    {
                        lsacreate_gid = dr_datarow["lsacreate_gid"].ToString(),
                        sanctionref_no = (dr_datarow["sanctionref_no"].ToString()),
                        branch_name = dr_datarow["branch_name"].ToString(),
                        state = (dr_datarow["state"].ToString()),
                        customer_name = dr_datarow["customer_name"].ToString(),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        customer_location = dr_datarow["customer_location"].ToString(),
                        address = (dr_datarow["customer_address"].ToString()),
                        rm_name = dr_datarow["rm_name"].ToString(),
                        zonal_head = (dr_datarow["zonal_head"].ToString()),
                        cluster_head = dr_datarow["cluster_head"].ToString(),
                        credit_manager = (dr_datarow["credit_manager"].ToString()),
                        business_head = dr_datarow["business_head"].ToString(),
                        sanction_date = (dr_datarow["sanction_date"].ToString()),
                        approved_by = dr_datarow["approved_by"].ToString(),
                        approved_date = (dr_datarow["approved_date"].ToString()),
                        gst_no = dr_datarow["gst_no"].ToString(),
                        pan_no = (dr_datarow["pan_no"].ToString()),
                        purpose_lending = dr_datarow["purpose_lending"].ToString(),
                        major_corporate = (dr_datarow["major_corporate"].ToString()),
                        majot_intervention = dr_datarow["majot_intervention"].ToString(),
                        primaryvalue_chain = (dr_datarow["primaryvalue_chain"].ToString()),
                        secondaryvalue_chain = dr_datarow["secondaryvalue_chain"].ToString(),
                        sector = (dr_datarow["sector"].ToString()),
                        product_solution = dr_datarow["product_solution"].ToString(),
                        hypothecation_date = (dr_datarow["hypothecation_date"].ToString()),
                        mortgage_date = dr_datarow["mortgage_date"].ToString(),
                        remarks = dr_datarow["remarks"].ToString(),
                        lsaref_no = dr_datarow["lsaref_no"].ToString(),
                        lsacreated_date = dr_datarow["lsacreated_date"].ToString(),
                        vertical = dr_datarow["vertical"].ToString(),
                        created_by = dr_datarow["created_by"].ToString(),
                        proceed_flag = dr_datarow["proceed_flag"].ToString(),
                        approval_status = dr_datarow["approval_status"].ToString()
                    });
                }
                values.rejectedlsa_list = getrejectedlsa_list;
            }
            dt_datatable.Dispose();
            msSQL = "select count(lsacreate_gid) as count from ids_trn_tlsa where approval_status='Pending'";
            values.pendinglsa_count= objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(lsacreate_gid) as count from ids_trn_tlsa where approval_status='Approved'";
            values.approvedlsa_count = objdbconn.GetExecuteScalar(msSQL);
            values.status = true;
            return true;
        }

        public void DapostLSAstatusapprove(string employee_gid, MdlLsaManagement values)
        {
            msSQL = "update ids_trn_tlsa set approval_status='Approved',"+
                 " approvalupdated_by='" + employee_gid + "'," +
                " approvalupdated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where lsacreate_gid='" + values.lsacreate_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msGETGIDDoc = objcmnfunctions.GetMasterGID("LSA");

                msSQL = "select count(*) from ids_trn_tlsa where month(pdfgenerate_date)=month(curdate()) and year(pdfgenerate_date)=year(curdate())";
                lsmonthwise = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select count(*) from ids_trn_tlsa where day(pdfgenerate_date)=day(curdate()) and month(pdfgenerate_date)=month(curdate()) and year(pdfgenerate_date)=year(curdate())";
                lsdaywise = objdbconn.GetExecuteScalar(msSQL);

                lsmonthcount = Int32.Parse(lsmonthwise) + 1;
                lsdaycount = Int16.Parse(lsdaywise) + 1;

                lssequencecode = msGETGIDDoc + lsmonthcount + lsdaycount;

                msSQL = " update ids_trn_tlsa set proceed_flag='Y'," +
                    " lsaref_no= '" + lssequencecode + "'," +
                    " pdfgenerate_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where lsacreate_gid='" + values.lsacreate_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "LSA Approved Successfully";
                values.status = true;

            }
            else
            {
                values.message = "Error Occured wile updating LSA Approved ";
                values.status = false;
            }
        }
    
        //-----Validation- Checking Loan facility Info from sanction-----------// 
        public void DaGetloanfacilityinfo(string customer2sanction_gid, MdlLSAvalidation values)
        {
            try
            {
                msSQL = "select * from ocs_mst_tsanction2loanfacilitytype where customer2sanction_gid='" + customer2sanction_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if(objODBCDatareader.HasRows==false)
                {
                    values.loanfacility_validation = "Y";
                }

                else
                {
                    values.loanfacility_validation = "N";
                }
                objODBCDataReader.Close();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetdocument_validation(string customer2sanction_gid, MdlLSAvalidation values)
        {
            try
            {
                msSQL = "select count(*) from ids_trn_tsanctiondocumentdtls where maker_status = 'Maker Pending' and sanction_gid = '" + customer2sanction_gid + "'";
                values.document_validation = objdbconn.GetExecuteScalar(msSQL);
                values.status = true;
            }
            catch
            {
                values.status = false;
            }

        }
        public bool DaPostROIDocumentUpload(HttpRequest httpRequest, UploadDocumentname objfilename, string employee_gid, string user_gid)
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
            //path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/ROIDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocuments" + "/" + lscompany_code + "/" + "IDAS/ROIDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;


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
                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        //lspath = HttpContext.Current.Server.MapPath("../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/ROIDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        ////FileStream file = new FileStream(lspath, FileMode.Create, FileAccess.Write);
                        ////ms.WriteTo(file);
                        ////file.Close();
                        ////ms.Close();
                        //lspath = "../../../erp_documents" + "/" + lscompany_code + "/" + "IDAS/ROIDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/ROIDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "IDAS/ROIDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGID = objcmnfunctions.GetMasterGID("DOCS");
                        msSQL = " insert into ids_trn_troidocument( " +
                                    " roidocument_gid," +
                                    " limitinfodtl_gid ," +
                                    " lsacreate_gid,"+
                                    " document_name," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGID + "'," +
                                    "'" + employee_gid + "'," +
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
        public void DaCancelDocument(string employee_gid,string limitinfodtl_gid, MdlLimitinfoEdit values)
        {
            try
            {
                msSQL = "delete from ids_trn_troidocument where limitinfodtl_gid='" + limitinfodtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if(mnResult!=0)
                {
                    values.message = "Document deleted successfully";
                    values.status = true;
                }
                else
                {
                    values.status = false;
                }
               
            }
            catch
            {
                values.status = false;
            }

        }
        public void DaGetPenalInterest(Mdlloanfacility_type values, string lsacreate_gid, string employee_gid)
        {

            msSQL = "select (26- rate_interest) as rate_interest, facility_type from ids_trn_tlimitinfodtl where " +
                " lsacreate_gid='" + lsacreate_gid + "' ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloanfacilitytype_list = new List<loanfacilitytype_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    getloanfacilitytype_list.Add(new loanfacilitytype_list
                    {
                        penal_interest = (dr_datarow["rate_interest"].ToString()),
                        loanfacility_type = (dr_datarow["facility_type"].ToString())
                    });
                }
                values.loanfacilitytype_list = getloanfacilitytype_list;

            }
            dt_datatable.Dispose();
        }

        // export with header details
        public void DaGetExportDocumentCoversation(MdlExportConversation values)
        {

           
            msSQL = "delete from ids_tmp_tdownloadcount where created_date <>'" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select customername from ids_trn_tsanctiondocumentdtls a" +
                   " left join ocs_mst_tcustomer2sanction b on a.sanction_gid = b.customer2sanction_gid" +
                   " left join ocs_mst_tcustomer c on b.customer_gid = c.customer_gid where sanction_gid='" + values.sanction_gid + "' group by sanction_gid";
            string lscustomer_name = objdbconn.GetExecuteScalar(msSQL);


            msSQL = "select customer_name from ids_tmp_tdownloadcount where customer_name='" + lscustomer_name.Replace("'", "") + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows == false)
            {
                objODBCDataReader.Close();
                string lscount = "0";

                int days_count = Convert.ToInt32(lscount) + 1;

                msSQL = "insert into ids_tmp_tdownloadcount (" +
                " customer_name," +
                " day_count," +
                " created_date ) values(" +
                "'" + lscustomer_name + "'," +
                "'" + days_count + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                objODBCDataReader.Close();
                msSQL = "select day_count from ids_tmp_tdownloadcount where customer_name='" + lscustomer_name.Replace("'", "") + "'";
                string lscount = objdbconn.GetExecuteScalar(msSQL);

                int days_count = Convert.ToInt32(lscount) + 1;

                msSQL = "update ids_tmp_tdownloadcount set day_count= '" + days_count + "' where  customer_name='" + lscustomer_name.Replace("'", "") + "'" +
                        " and created_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            msSQL = " select day_count from ids_tmp_tdownloadcount where customer_name='" + lscustomer_name.Replace("'", "") + "'";
            string customerwise_count = objdbconn.GetExecuteScalar(msSQL);


            string lsfilename = lscustomer_name.Replace(".", " ").Replace("'", " ") + " " + DateTime.Now.ToString("yyyyMMdd") + customerwise_count;

            var query_no = objdbconn.GetExecuteScalar("SELECT max(query_no) from ids_trn_tdocconversation where sanction_gid='" + values.sanction_gid + "' "+
                " AND type_of_conversation='External' AND type_of_doc = '" + values.type_of_copy + "'");
            msSQL = " SELECT a.document_code as 'Document Code',a.document_name as 'Document Name'," +
                    " if(a.scandocument_date is null,'-',date_format(a.scandocument_date, '%d-%m-%Y')) as 'Document Date'," +
                    " a.types_of_copy as 'Types of Copy'," +
                    " a.documentrecord_id as 'Document Record ID','' as `Type of Copy(RM)`,'' as `Document Despatch Status(Despatched / Pending)`";
            
           
            msSQL += " FROM ids_trn_tsanctiondocumentdtls a" +
                     " LEFT JOIN ids_mst_tdocumentlist x on a.document_gid=x.documentlist_gid" +
                     " WHERE sanction_gid='" + values.sanction_gid + "'" +
                     " ORDER BY x.display_order ASC ";
            dt_datatable = objdbconn.GetDataTable(msSQL);


            if (dt_datatable.Rows.Count != 0)
            {
                string lscompany_code = string.Empty;
                MemoryStream ms = new MemoryStream();
                ExcelPackage excel = new ExcelPackage(ms);
                var workSheet = excel.Workbook.Worksheets.Add("" + lsfilename + "");
                try
                {
                    msSQL = " select company_code from adm_mst_tcompany";
                    lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                    values.attachment_name = "" + lsfilename + "" + ".xlsx";
                    var path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/CaseCreation/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                    values.attachment_path = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "IDAS/CaseCreation/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.attachment_name);
                    bool exists = System.IO.Directory.Exists(path);
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    workSheet.Cells["A11"].LoadFromDataTable(dt_datatable, true);
                    dt_datatable.Dispose();

                    workSheet.Cells["B1"].Value = "IDAS Document Check List -";
                    workSheet.View.FreezePanes(12, 9);
                    using (var range = workSheet.Cells[1, 2, 1, 5])
                    {
                        range.Merge = true;
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        range.Style.Font.Color.SetColor(Color.Black);
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    workSheet.Cells.Style.Locked = false;

                    workSheet.Cells["B3:E3"].Style.Locked = true;
                    workSheet.Cells["B4:E4"].Style.Locked = true;
                    workSheet.Cells["B5:E5"].Style.Locked = true;
                    workSheet.Cells["B6:E6"].Style.Locked = true;
                    workSheet.Cells["B7:E7"].Style.Locked = true;
                    workSheet.Cells["B8:E8"].Style.Locked = true;
                    workSheet.Cells["B9:D9"].Style.Locked = true;
                    workSheet.Cells["A11:G11"].Style.Locked = true;
                    workSheet.Cells["B10"].Style.Locked = true;
                    workSheet.Cells["D10"].Style.Locked = true;

                    workSheet.Column(1).AutoFit();
                    workSheet.Column(2).AutoFit();
                    workSheet.Column(3).AutoFit();
                    workSheet.Column(4).AutoFit();
                    workSheet.Column(5).AutoFit();
                    workSheet.Column(6).AutoFit();
                    workSheet.Column(7).AutoFit();

                    
                    msSQL = "select * from ids_trn_tsanctiondocumentdtls where sanction_gid='" + values.sanction_gid + "'";
                    DataTable dt_count = objdbconn.GetDataTable(msSQL);

                    var lsrowcount = dt_count.Rows.Count;
                    dt_count.Dispose();
                    var count = 0;
                    for (int rowcount = 0; rowcount <= lsrowcount; rowcount++)
                    {
                        count = rowcount + 12;
                        workSheet.Cells["A" + count + ":E" + count].Style.Locked = true;
                        using (var range = workSheet.Cells["A" + count + ":E" + count])
                        {
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }
                    }
                   
                    using (var range = workSheet.Cells["B3:C3"])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }
                   
                    using (var range = workSheet.Cells["B4:C4"])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }
                    using (var range = workSheet.Cells["B5:C5"])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }
                    using (var range = workSheet.Cells["B6:E6"])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }
                    using (var range = workSheet.Cells["B7:E7"])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }
                    using (var range = workSheet.Cells["B8:E8"])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }
                    using (var range = workSheet.Cells["B9:D9"])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }
                    using (var range = workSheet.Cells["B10"])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }
                    using (var range = workSheet.Cells["D10"])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }
                    int number = count + 1;
                    using (var range = workSheet.Cells["B" + number])
                    {
                        range.Merge = true;
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        range.Style.Font.Color.SetColor(Color.Black);
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    workSheet.Cells["B" + number].Value = "Please list if Any other additional Documents:";

                    workSheet.Protection.AllowEditObject = true;
                    workSheet.Protection.AllowEditScenarios = true;
                    workSheet.Protection.AllowFormatCells = true;
                    workSheet.Protection.AllowFormatColumns = true;
                    workSheet.Protection.AllowFormatRows = true;
                    workSheet.Protection.IsProtected = true;
                    workSheet.Protection.SetPassword("Welcome@123");

                    workSheet.Cells["B3"].Value = "Name of the Borrower";
                    using (var range = workSheet.Cells["B3"])
                    {

                        range.Style.Font.Bold = true;

                    }
                    workSheet.Cells["B4"].Value = "Name of the Facility";
                    using (var range = workSheet.Cells["B4"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["B5"].Value = "Facility Sanctioned";
                    using (var range = workSheet.Cells["B5"])
                    {
                        range.Style.Font.Bold = true;
                    }
                     workSheet.Cells["B6"].Value = "Sanction Ref & Date";
                    using (var range = workSheet.Cells["B6"])
                    {
                        range.Style.Font.Bold = true;
                    }
                     workSheet.Cells["D6"].Value = "Segment";
                    using (var range = workSheet.Cells["D6"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["B7"].Value = "Name of the RM";
                    using (var range = workSheet.Cells["B7"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["D7"].Value = "Name of the Cluster Head";
                    using (var range = workSheet.Cells["D7"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["B8"].Value = "Name of the Business Head";
                    using (var range = workSheet.Cells["B8"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["D8"].Value = "Name of the Zonal Head";
                    using (var range = workSheet.Cells["D8"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["B9"].Value = "Name of the Credit Manager";

                    using (var range = workSheet.Cells["B9"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["D9"].Value = "Name of the Courier";
                    using (var range = workSheet.Cells["D9"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["B10"].Value = "Date of Courier";

                    using (var range = workSheet.Cells["B10"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    workSheet.Cells["D10"].Value = "POD Number";

                    using (var range = workSheet.Cells["D10"])
                    {
                        range.Style.Font.Bold = true;
                    }
                    msSQL = " SELECT b.customer_urn,b.customername,a.facility_type,a.sanction_amount," +
                            "  CONCAT(a.sanction_refno,' / ',CAST(date_format(a.sanction_date, '%d-%m-%Y')AS CHAR)) as sanction_date," +
                            " b.vertical_code,a.collateral_security,b.zonal_name,b.businesshead_name,b.cluster_manager_name,b.creditmgmt_name,b.relationshipmgmt_name" +
                            " FROM ocs_mst_tcustomer2sanction a" +
                            " INNER JOIN ocs_mst_tcustomer b on a.customer_gid = b.customer_gid" +
                            " WHERE a.customer2sanction_gid='" + values.sanction_gid + "'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows)
                    {
                        workSheet.Cells["B1"].Value = "IDAS Document Check List -" + objODBCDataReader["customername"].ToString();
                        workSheet.Cells["C3"].Value = objODBCDataReader["customername"].ToString();
                        workSheet.Cells["C4"].Value = objODBCDataReader["facility_type"].ToString();
                        workSheet.Cells["C5"].Value = objODBCDataReader["sanction_amount"].ToString();
                        workSheet.Cells["C6"].Value = objODBCDataReader["sanction_date"].ToString();
                        workSheet.Cells["E6"].Value = objODBCDataReader["vertical_code"].ToString();
                        workSheet.Cells["C7"].Value = objODBCDataReader["relationshipmgmt_name"].ToString();
                        workSheet.Cells["E7"].Value = objODBCDataReader["cluster_manager_name"].ToString();
                        workSheet.Cells["C8"].Value = objODBCDataReader["businesshead_name"].ToString(); ;
                        workSheet.Cells["E8"].Value = objODBCDataReader["zonal_name"].ToString();
                        workSheet.Cells["C9"].Value = objODBCDataReader["creditmgmt_name"].ToString();
                    }
                    objODBCDataReader.Close();
                    FileInfo file = new FileInfo(values.attachment_path);
                    using (var range = workSheet.Cells[11, 1, 11,7])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                        range.Style.Font.Color.SetColor(Color.Black);

                    }


                    values.attachment_path =lscompany_code + "/" + "IDAS/CaseCreation/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.attachment_name;
                    excel.SaveAs(ms);
                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", values.attachment_path, ms);
                    ms.Close();

                }

                catch (Exception ex)
                {
                    values.status = false;
                    values.message = "Failure";
                }
                values.status = true;
                values.message = "Success";
                values.attachment_path = objcmnstorage.EncryptData(values.attachment_path);

            }
            else
            {
                values.status = false;
                values.message = "No records to export!";
                return;
            }



        }
    }

}