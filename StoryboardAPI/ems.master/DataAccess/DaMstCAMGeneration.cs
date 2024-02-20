using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Functions;
using ems.master.Models;
using System.Data;
using System.Data.Odbc;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Mail;
using Spire.Doc;
using Spire.Doc.Documents;
using ems.storage.Functions;
using Spire.Doc.Fields;

/// <summary>
/// (It's used for pages in CAMGeneration)CAMGeneration DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.DataAccess
{
    public class DaMstCAMGeneration
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msGetGID;
        string msSQL;
        int mnResult;
        string lsfilepath = string.Empty;
        string lsdocument_gid = string.Empty;
        MemoryStream ms = new MemoryStream();
        MemoryStream ms_stream = new MemoryStream();
        string document_gid = string.Empty;
        string lscompany_code = string.Empty;
        string lspath, path,lstemplate_content, lscontent, lsapplicant_type, lsGID, lsaddress, lspan="",lscin="", lssummarytemplate, lssupplier, lsbuyer;
        string lsrow1, lsrow2, lsrow3, lsrow4, lsrow5, lsrow6, lsrow7, lsrow8, lsrow9, lsrow10, lsrow11, lsrow12, lsrow13, lsrow14, lsrow15, lsrow16, lsrow17,
           lsrow18, lsrow19, lsrow20, lsrow21, lsrow22, lsrow23, lsrow24, lsrow25, lsrow26, lsrow27, lsrow28;

        public void DaGetCAMTemplate(string application_gid, MdlMstCAMGeneration values)
        {
            try
            {
               
                msSQL = " select a.template_content from adm_mst_ttemplate a " +
                  " where a.template_name='CAM GENERATION'"; 
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lstemplate_content = (objODBCDatareader["template_content"].ToString().Replace("''","'"));
                
                }

                objODBCDatareader.Close();
                lscontent = lstemplate_content;



               msSQL = " select a.application_gid, application_no, customer_urn, customerref_name as customer_name, vertical_name,applicant_type, " +
                        " constitution_name, sa_name,relationshipmanager_name,relationshipmanager_gid,businesshead_name,creditmanager_name,creditmanager_gid," +
                        " GROUP_CONCAT(distinct(b.primaryvaluechain_name) SEPARATOR ', ') as primaryvaluechain_name," +
                        " GROUP_CONCAT(distinct(c.secondaryvaluechain_name) SEPARATOR ', ') as secondaryvaluechain_name " +
                        " from ocs_mst_tapplication a" +
                        " left join ocs_mst_tapplication2primaryvaluechain b on b.application_gid = a.application_gid " +
                        " left join ocs_mst_tapplication2secondaryvaluechain c on c.application_gid = a.application_gid " +
                        " where a.application_gid='" + application_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscontent = lscontent.Replace("lsapplication_no", objODBCDatareader["application_no"].ToString());
                    lscontent = lscontent.Replace("lsborrower_name", objODBCDatareader["customer_name"].ToString());
                    lscontent = lscontent.Replace("lsborrower_id", objODBCDatareader["customer_urn"].ToString());
                    lscontent = lscontent.Replace("lsvertical", objODBCDatareader["vertical_name"].ToString());
                    lscontent = lscontent.Replace("lsconstitution", objODBCDatareader["constitution_name"].ToString());
                    lscontent = lscontent.Replace("lssa_name", objODBCDatareader["sa_name"].ToString());
                    lscontent = lscontent.Replace("lsprimary", objODBCDatareader["primaryvaluechain_name"].ToString());
                    lscontent = lscontent.Replace("lssecondary", objODBCDatareader["secondaryvaluechain_name"].ToString());
                    lsapplicant_type = objODBCDatareader["applicant_type"].ToString();
                    //RM Contact No
                    msSQL = "select employee_mobileno from hrm_mst_temployee where employee_gid='" + objODBCDatareader["relationshipmanager_gid"].ToString() + "'";
                    string lsrm_mobileno = objdbconn.GetExecuteScalar(msSQL);
                    //Credit Manager Contact No
                    msSQL = "select employee_mobileno from hrm_mst_temployee where employee_gid='" + objODBCDatareader["creditmanager_gid"].ToString() + "'";
                    string lscreditmngr_mobileno = objdbconn.GetExecuteScalar(msSQL);

                    lscontent = lscontent.Replace("lsrm_name", objODBCDatareader["relationshipmanager_name"].ToString()+" /" +lsrm_mobileno);
                    lscontent = lscontent.Replace("lscredit_manager", objODBCDatareader["creditmanager_name"].ToString() + " / " + lscreditmngr_mobileno);
                    lscontent = lscontent.Replace("lsbusiness_head", objODBCDatareader["businesshead_name"].ToString());

                }                
                objODBCDatareader.Close();
               
                if(lsapplicant_type=="Individual")
                { //Address, PAN
                    msSQL = "select contact_gid,pan_no from ocs_mst_tcontact where application_gid='" + application_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsGID = objODBCDatareader["contact_gid"].ToString();
                        lspan = objODBCDatareader["pan_no"].ToString();
                      
                    }
                    else
                    {
                        lspan = "";
                        lscin = "";
                    }
                    objODBCDatareader.Close();
                    msSQL = " select concat(addressline1,',', addressline2,',', taluka,',',city , ',',district,',',state ,'-'," +
                           " postal_code, ',', country)  as address from ocs_mst_tcontact2address where contact_gid='" + lsGID + "' and primary_status='Yes'";
                    lsaddress = objdbconn.GetExecuteScalar(msSQL); 
              
                                     
                }
                else
                { //Address, PAN , CIN
                    msSQL = "select institution_gid,companypan_no,cin_no from ocs_mst_tinstitution where application_gid='" + application_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsGID = objODBCDatareader["institution_gid"].ToString();
                        lspan = objODBCDatareader["companypan_no"].ToString();
                        lscin = objODBCDatareader["cin_no"].ToString();
                    }
                    else
                    {
                        lspan = "";
                        lscin = "";
                    }
                    objODBCDatareader.Close();
                    
                    msSQL = " select concat(addressline1,',', addressline2,',', taluka,',',city , ',',district,',',state ,'-'," +
                           " postal_code, ',', country)  as address from ocs_mst_tinstitution2address where institution_gid='" + lsGID + "' and primary_status='Yes'";
                    lsaddress = objdbconn.GetExecuteScalar(msSQL);
                }
                lscontent = lscontent.Replace("lsaddress", lsaddress);
                lscontent = lscontent.Replace("lspan_no", lspan);
                lscontent = lscontent.Replace("lscin_no", lscin);


                //Product
                string lshtml = "", lsexistingfacility = "", lsrepayment = "",lsproduct="";
                msSQL = "select product_type, " +
                               " productsub_type, loanfacility_amount, loan_type, rate_interest, penal_interest, facilityoverall_limit, " +
                               " tenureoverall_limit, facility_type, facility_mode, principalfrequency_name, interestfrequency_name, " +
                               " interest_status, moratorium_type, moratorium_status, date_format(moratorium_startdate, '%d-%m-%Y') as moratorium_startdate, " +
                               " date_format(moratorium_enddate, '%d-%m-%Y') as moratorium_enddate,scheme_type,tenureproduct_month,tenureproduct_days " +
                               " from ocs_mst_tapplication2loan " +
                               " where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getproduct = new List<mstloan_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsproduct += "<tr style='width: 19.8801 %;border-top: 1pt solid windowtext;border-left: 1pt solid windowtext;'>" +
                            " <td style='width: 19.8801 %;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-left: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'><b>" + dr_datarow["product_type"].ToString() + "</b><br>(" + dr_datarow["facility_mode"].ToString() + ")<br>"+
                            " Tenor:" + dr_datarow["tenureproduct_days"].ToString() + "<br>Availability:" + dr_datarow["tenureproduct_month"].ToString() + "</td>"+
                            " <td style='width: 9.8801 %;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'></td>"+
                            " <td style='width: 19.8801 %;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" +
                            "" + dr_datarow["loanfacility_amount"].ToString() + "</td>"+
                            " <td style='width: 19.8801 %;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'></td>"+
                            " <td style='width: 19.8801 %;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'></td>" +
                            " <td style='width: 19.8801 %;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'><table style='width: 100 %;'><tbody><tr>" +
                            " <td style = 'width: 50.0000%;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-top: 1pt solid windowtext;'> <br></td>" +
                            " <td style = 'width: 50.0000%;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-top: 1pt solid windowtext;'><b>Existing </b></td>" +
                            " <td style = 'width: 50.0000%;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-top: 1pt solid windowtext;'><b>Proposed </b></td></tr><tr>" +
                            " <td style = 'width: 50.0000%;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>Interest </td>" +
                            " <td style = 'width: 50.0000%;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'> <br></td>" +
                            " <td style = 'width: 50.0000%;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;''><br> </td></tr><tr>" +
                            " <td style = 'width: 50.0000%;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>Proc Fees </td>" +
                            " <td style = 'width: 50.0000%;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'> <br></td>" +
                            " <td style = 'width: 50.0000%;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'><br> </td></tr><tr>" +
                            " <td style = 'width: 50.0000%;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'> SA Payout </td>" +
                            " <td style = 'width: 50.0000%;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'> <br></td>" +
                            " <td style = 'width: 50.0000%;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'><br> </td></tr>" +
                            " </tbody></table></tr> ";
                    }
                }
                lscontent = lscontent.Replace("lsproduct", lsproduct);
                dt_datatable.Dispose();
                
                //Individual Info
             
            
                    msSQL = " select concat(first_name,' ' ,middle_name,' ',last_name) as individual_name,age,designation_name  from ocs_mst_tcontact " +
                        " where  application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getprofitloss_list = new List<individual_List>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lshtml += "<tr >" +
                            " <td style='width: 116.85pt;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-left: 1pt solid windowtext;border-image: initial;border-top: none;padding: 0in 5.4pt;vertical-align: top;'valign = 'top' width = '25%''>" + dr_datarow["individual_name"].ToString() + " </td>"+
                            " <td style='width: 50.0000%;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" + dr_datarow["age"].ToString() + "</td>"+
                            " <td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" +
                            "" + dr_datarow["designation_name"].ToString() + "</td>"+
                            " <td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'></td>" +
                            " <td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'></td>" +
                            " <td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'></td></tr>";                     
                    }
                }
                dt_datatable.Dispose();
                lscontent = lscontent.Replace("lsindvl_name", lshtml);
                //Contact Info

                msSQL = "select concat(contactpersonfirst_name,' ',contactpersonmiddle_name,' ' ,contactpersonlast_name) as contact_person,"+
                    " c.email_address,b.mobile_no,designation_type from ocs_mst_tapplication a " +
                   " left join ocs_mst_tapplication2contactno b on a.application_gid=b.application_gid" +
                   " left join ocs_mst_tapplication2email c  on a.application_gid = c.application_gid  where a.application_gid='" + application_gid + "' and" +
                   " primary_emailaddress = 'Yes' and primary_mobileno = 'Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lscontent = lscontent.Replace("lsemail", objODBCDatareader["email_address"].ToString());
                    lscontent = lscontent.Replace("lsmobile", objODBCDatareader["mobile_no"].ToString());
                    lscontent = lscontent.Replace("lscontperson", objODBCDatareader["contact_person"].ToString());
                    lscontent = lscontent.Replace("lsdesgn", objODBCDatareader["designation_type"].ToString());
                }
                else
                {
                    lscontent = lscontent.Replace("lsemail", "");
                    lscontent = lscontent.Replace("lsmobile", "");
                    lscontent = lscontent.Replace("lscontperson", "");
                    lscontent = lscontent.Replace("lsdesgn", "");
                }
                objODBCDatareader.Close();
                //Existing Bank Facility
                msSQL= " select  bank_name,facility_type,outstanding_amount,account_classification,fundedtypeindicator_name,"+
                        " sanctioned_limit from ocs_mst_tcreditbankfacilitydtl where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbankfacility = new List<cuwexistingbankfacility_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsexistingfacility += "<tr style='border-left: 1pt solid windowtext;'>" +
                            " <td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" +
                            ""+ dr_datarow["bank_name"].ToString() + " </td>"+
                            " <td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" +
                            "" + dr_datarow["facility_type"].ToString() + "</td><td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" +
                            "" + dr_datarow["fundedtypeindicator_name"].ToString() + "</td><td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" + dr_datarow["sanctioned_limit"].ToString() +""+
                            " </td><td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" + dr_datarow["outstanding_amount"].ToString() + "</td><td style='width: 9.8801 %;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" + dr_datarow["account_classification"].ToString() + "</td></tr>";
                    }
                }
                lscontent = lscontent.Replace("lsexistingfacility", lsexistingfacility);
                dt_datatable.Dispose();
                //Repayment Info
                msSQL = " select  bank_name,currentoutsatnding_amount,account_classification,"+
                        " sanction_amount from ocs_mst_tcreditrepaymentdtl where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getrepayment = new List<cuwexistingbankfacility_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrepayment += "<tr style='border-left: 1pt solid windowtext;'><td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" + dr_datarow["bank_name"].ToString() + " </td>"+
                            " <td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" + dr_datarow["sanction_amount"].ToString() + "" +
                            " </td><td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" +
                            "" + dr_datarow["currentoutsatnding_amount"].ToString() + "</td>"+
                            " <td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" +
                            ""+ dr_datarow["account_classification"].ToString() + "</td>"+
                            " <td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'></td>" +
                            " <td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'></td>" +
                            " </tr>";
                    }
                }
                lscontent = lscontent.Replace("lsrepayment", lsrepayment);
                dt_datatable.Dispose();
                
                 //supplier Info
                msSQL = " select supplier_name,relationship_vintage_year,purchase_amount,bankdebit_amount from ocs_mst_tcreditsupplier where "+
                    " application_gid='" + application_gid + "' and credit_gid='"+lsGID+"'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getsupplier = new List<supplier_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lssupplier += "<tr style='border-left: 1pt solid windowtext;'><td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" + dr_datarow["supplier_name"].ToString() + " </td>"+
                            " <td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" + dr_datarow["relationship_vintage_year"].ToString() + "" +
                            " </td><td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" +
                            "" + dr_datarow["purchase_amount"].ToString() + "</td>"+
                            " <td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" +
                            ""+ dr_datarow["bankdebit_amount"].ToString() + "</td>"+
                            " </tr>";
                    }
                }
                lscontent = lscontent.Replace("lssupplier", lssupplier);
                dt_datatable.Dispose();
                //Buyer Info
               
                string lsloanGID = objdbconn.GetExecuteScalar(" select quote(group_concat(application2loan_gid)) from ocs_mst_tapplication2loan where  " +
                    " application_gid='" + application_gid + "'");
                
                if (lsloanGID==null||lsloanGID=="")
                {

                    lscontent = lscontent.Replace("lsbuyer", "");
                }
                else
                {
                    lsloanGID = lsloanGID.Replace(",", "','");
                    msSQL = " select buyer_name, '' as relationship_vintage_year, '' as relationship_vintage_month from ocs_mst_tapplication2buyer where application2loan_gid in (" + lsloanGID + ") "+
                            " union "+
                            " select buyer_name, relationship_vintage_year, relationship_vintage_month from ocs_mst_tcreditbuyer " +
                            " where application_gid in ('"+ application_gid + "')";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getbuyer = new List<buyerlist>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            lsbuyer += "<tr style='border-left: 1pt solid windowtext;'><td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" + dr_datarow["buyer_name"].ToString() + " </td>" +
                                " <td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" + dr_datarow["relationship_vintage_year"].ToString() + " </td>" +
                                " </td><td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" + dr_datarow["relationship_vintage_month"].ToString() + " </td>" +
                                " <td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" +
                                " </td></tr>";
                        }
                    }
                    lscontent = lscontent.Replace("lsbuyer", lsbuyer);
                    dt_datatable.Dispose();

                }
                 //Summary Template -1 
                msSQL = " select date, audit_type, net_sales, other_income, total_revenue, growth_in_revenues, ebitda, ebitda_margin, depreciation," +
                        " interest, pat, pat_margin, total_outside_liabilities, total_bank_borrowings, tangible_net_worth, current_ratio, tol_tnw," +
                        " interest_coverage_ratio, dscr, sundry_creditors, sundry_debtors, inventories, payable_noofdays, recievable_noofdays, inventory_noofdays," +
                        " workingcapital_noofdays, debt_ebitda, msme" +
                        " from ocs_trn_tsummarytemplate1" +
                        " where application_gid='" + application_gid + "'" +
                        " and credit_gid='" + lsGID + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getsummarytemplate1_list = new List<summarytemplate1_list>();
               

                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow1 += "<td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'><strong> " + dr_datarow["date"].ToString().Replace('#', '.') + "</strong></span></p></td>";
                      }
                
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow2 += "<td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'><strong> " + dr_datarow["audit_type"].ToString() + "</strong></span></p></td>";
                    }
                 foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow3 += " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["net_sales"].ToString() + "</span></p></td>";
                    }
                
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow4 += " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["other_income"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow5 += " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["total_revenue"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow6 += "<td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["growth_in_revenues"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow7 += " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["ebitda"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow8 += " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["ebitda"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow9 += " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["ebitda"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow10 += " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["ebitda_margin"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow11 += " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["depreciation"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow12 += "<td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["interest"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow13 += " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["pat"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow14 += "<td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["pat_margin"].ToString() + "</span></p></td>";

                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow15 +=
                          " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["total_outside_liabilities"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow16 += " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["total_bank_borrowings"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow17 += " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["tangible_net_worth"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow18 += " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["current_ratio"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow19 +=
                           "<td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["tol_tnw"].ToString() + "</span></p></td>"; 
                    }
                    
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow20 += "<td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["interest_coverage_ratio"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow21 += " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["dscr"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow22 += " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["sundry_creditors"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow23 += "<td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["sundry_debtors"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow24 += " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["inventories"].ToString() + "</span></p></td>";

                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow25 += "<td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["payable_noofdays"].ToString() + "</span></p></td>";
                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow26 += " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["recievable_noofdays"].ToString() + "</span></p></td>";

                    }
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow27 += " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["inventory_noofdays"].ToString() + "</span></p></td>";

                    }
                    foreach(DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsrow28 += " <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                        " valign = 'top' width = '16%'><p><span style = 'color:black;'> " + dr_datarow["workingcapital_noofdays"].ToString() + "</span></p></td>";
                    }
                }
                dt_datatable.Dispose();
                string lsleftalign_border = "style='border-left: 1pt solid windowtext;'";
                lssummarytemplate = "<tr "+ lsleftalign_border + "><td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Particulars</td>" + lsrow1 + "</tr>" +
                            "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Type</td>" + lsrow2 + "</tr>" +
                            "<tr  " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Net Sales(NS)</td>"+ lsrow3 + "</tr>" +
                            "<tr style='border-left: 1pt solid windowtext;'><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Other Income</td>" + lsrow4 + "</tr>" +
                            "<tr  style='border-left: 1pt solid windowtext;'><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Total Revenue</td>" + lsrow5 + "</tr>"
                   + "<tr  style='border-left: 1pt solid windowtext;'> <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Growth in Revenues </td>" + lsrow6 + "</tr>"
                            + "<tr  style='border-left: 1pt solid windowtext;'><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Gross Profit</td>" + lsrow7 + "</tr>" 
                            + "<tr  style='border-left: 1pt solid windowtext;'><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Gross Margin%</td>" + lsrow8 + "</tr>" 
                            + "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>EBITDA</td>" + lsrow9 + "</tr>"
                   + "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>EBITDA Margin %</td>" + lsrow10 + "</tr>" +
                    "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Depreciation Cost</td>" + lsrow11 + "</tr>" +
                            "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Finance</td>" + lsrow12 + "</tr>" + "<tr " + lsleftalign_border + "> <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>PAT </td>" + lsrow13 + "</tr>"
                   + "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>PAT Margin%</td>" + lsrow14 + "</tr>" +
                            "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Total Outside Liabilities </td>" + lsrow15 + "</tr>" 
                   + "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Total Bank Borrowings</td>" + lsrow16 + "</tr>" + "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Tangible Net Worth </td>" + lsrow17 + "</tr>" + "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Current Ratio </td>" + lsrow18 + "</tr>"
                   + "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>TOL/TNW (a/b) </td>" + lsrow19 + "</tr>" + "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Interest Coverage Ratio </td>" + lsrow20 + "</tr>"
                   + "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>DSCR</td>" + lsrow21 + "</tr>" + "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Sundry Creditors</td>" + lsrow22 + "</tr>" + "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Sundry Debtors</td>" + lsrow23 + "</tr>"
                   + "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Inventories</td>" + lsrow24 + "</tr>" + "<tr " + lsleftalign_border + "> <td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Payable No of Days" + lsrow25 + "</tr>"
                   + "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Receivable No of Days" + lsrow26 + "</tr>" + "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Inventory Holding No of Days" + lsrow27 + "</tr>" + "<tr " + lsleftalign_border + "><td style = 'width: 77.9pt;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;border-image: initial;border-left: none;   padding: 0in 5.4pt;vertical-align: top;'" +
                            " valign = 'top' width = '16%'><p><span style = 'color:black;'>Working Capital (No of Days)" + lsrow28 + "</tr>";


                lscontent = lscontent.Replace("lssummarytemplate", lssummarytemplate);

                string lsfacilitytype = "", lstenure="", lsvalitidylimit="";
                msSQL = "select product_type from ocs_mst_tapplication2loan where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getproducttype = new List<mstloan_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsfacilitytype += "Facility:"+ dr_datarow["product_type"].ToString() + "</b><br>";
                    }
                }
                lscontent = lscontent.Replace("lsfacilitytype", lsfacilitytype);
                dt_datatable.Dispose();

                msSQL = "select product_type, tenureoverall_limit from ocs_mst_tapplication2loan where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gettenure = new List<mstloan_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lstenure += "<tr style='border-left: 1pt solid windowtext;'><td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" + dr_datarow["product_type"].ToString() + " </td>" +
                           " <td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" + dr_datarow["tenureoverall_limit"].ToString() + "" +
                           " </td>" +
                           " </tr>";
                    }
                }
                lscontent = lscontent.Replace("lstenure", lstenure);
                dt_datatable.Dispose();

                msSQL = "select product_type, facilityoverall_limit from ocs_mst_tapplication2loan where application_gid='" + application_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getvalitidylimit = new List<mstloan_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        lsvalitidylimit += "<tr style='border-left: 1pt solid windowtext;'><td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" + dr_datarow["product_type"].ToString() + " </td>" +
                           " <td style='width: 9.8801 %;border-left: 1pt solid windowtext;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>" + dr_datarow["facilityoverall_limit"].ToString() + "" +
                           " </td>" +
                           " </tr>";
                    }
                }
                lscontent = lscontent.Replace("lsvalitidylimit", lsvalitidylimit);
                dt_datatable.Dispose();

                values.template_content = lscontent;

                values.status = true;
            }
            catch(Exception e)
            {
                values.message = e.ToString();
                values.status = false;
            }
        }

        public void DaPostWordGenerate(string employee_gid, MdlMstCAMGeneration values)
        {
            try
            {


                msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                //path = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "Master/CAMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
                //{
                //    if ((!System.IO.Directory.Exists(path)))
                //        System.IO.Directory.CreateDirectory(path);
                //}
                string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                string lsfile_gid = msdocument_gid;

                lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CAMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                {
                    if ((!System.IO.Directory.Exists(lspath)))
                        System.IO.Directory.CreateDirectory(lspath);
                }
                objcmnfunctions.uploadFile(lspath, lsfile_gid);
                values.lsname = "CAM" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".docx";

                values.lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CAMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid + "/" + values.lsname;
                values.lscloudpath =  lscompany_code + "/" + "Master/CAMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid + "/" + values.lsname;
                msSQL = " update ocs_mst_tapplication set cam_content='" + values.content.Replace("'", "''") + "'," +
                    " document_path='" + lscompany_code + "/" + "Master/CAMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid + "/" + values.lsname + "'," +
                    " document_name='" + values.lsname + "'," +
                    " camgenerated_by='" + employee_gid + "'," +
                    " camgenerated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " update ocs_trn_tdynamictemplatedtl set template_content='" + values.defaulttemplate_content.Replace("'", "''") + "' " +
                        " where templatetype_gid='" + values.application_gid + "' and templatetype_name='" + getTemplateClass.CAMGeneration + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //HTML File Creation
                string htmlFolderPath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CAMHTML/";
                if ((!System.IO.Directory.Exists(htmlFolderPath)))
                    System.IO.Directory.CreateDirectory(htmlFolderPath);

                string htmlFilePath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "Master/CAMHTML/camfile.html";
                lstemplate_content = values.content.Replace("/public", ConfigurationManager.AppSettings["file_path"] + "/Public");


                File.WriteAllText(htmlFilePath, lstemplate_content.Replace("<td>", "<td style = 'width: 50.0000%;border-top: 1pt solid windowtext;border-right: 1pt solid windowtext;border-bottom: 1pt solid windowtext;'>"));

                Document document = new Document();
                document.LoadFromFile(htmlFilePath, FileFormat.Html, XHTMLValidationType.None);

                //Read Header and Footer File
                Document doc1 = new Document();
                doc1.LoadFromFile(ConfigurationManager.AppSettings["file_path"] + "/templates/Camheaderfile.docx");
                HeaderFooter header = doc1.Sections[0].HeadersFooters.Header;

                Document doc2 = new Document(htmlFilePath);

                foreach (Section section in doc2.Sections)
                {
                    foreach (DocumentObject obj in header.ChildObjects)
                    {
                        section.HeadersFooters.Header.ChildObjects.Add(obj.Clone());
                    }
                    foreach (Paragraph paragraph in section.Paragraphs)
                    {
                        paragraph.Format.BeforeSpacing = 0;
                        paragraph.Format.AfterAutoSpacing = false;
                        paragraph.Format.AfterSpacing = 0;
                        paragraph.Format.BeforeAutoSpacing = false;
                        paragraph.Format.HorizontalAlignment = HorizontalAlignment.Justify;

                    }
                    foreach (DocumentObject obj in section.Body.ChildObjects)
                    {
                        if (obj is Paragraph)
                        {
                            var para = obj as Paragraph;
                            foreach (DocumentObject Pobj in para.ChildObjects)
                            {

                                if (Pobj is TextRange)
                                {
                                    TextRange textRange = Pobj as TextRange;
                                    textRange.CharacterFormat.FontName = "Calibri";
                                    textRange.CharacterFormat.FontSize = 10;
                                }
                            }
                        }
                    }
                    foreach (Spire.Doc.Table table in section.Tables)
                    {
                        foreach (Spire.Doc.TableRow row in table.Rows)
                        {
                            foreach (Spire.Doc.TableCell cell in row.Cells)
                            {

                                foreach (Paragraph p in cell.Paragraphs)
                                {
                                    foreach (DocumentObject Pobj in p.ChildObjects)
                                    {
                                        if (Pobj is TextRange)
                                        {
                                            TextRange textRange = Pobj as TextRange;
                                            textRange.CharacterFormat.FontName = "Calibri";
                                            textRange.CharacterFormat.FontSize = 10;
                                        }
                                    }
                                    p.Format.BeforeSpacing = 0;
                                    p.Format.AfterAutoSpacing = false;
                                    p.Format.AfterSpacing = 0;
                                    p.Format.BeforeAutoSpacing = false;
                                    p.Format.HorizontalAlignment = HorizontalAlignment.Justify;
                                }
                            }

                        }
                    }
                }
                // Document
                string lscamfile = HttpContext.Current.Server.MapPath("../../../erpdocument" + "/" + lscompany_code + "/" + "TmpFile/Footer/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid + "/");
                {
                    if ((!System.IO.Directory.Exists(lscamfile)))
                        System.IO.Directory.CreateDirectory(lscamfile);
                }
                lscamfile = lscamfile + values.lsname;
                doc2.SaveToFile(lscamfile, FileFormat.Docx);

                Document doc3 = new Document();
                doc3.LoadFromFile(lscamfile);
                HeaderFooter footer = doc3.Sections[0].HeadersFooters.Footer;
                Paragraph footerParagraph = footer.AddParagraph();

                footerParagraph.AppendField("page number", FieldType.FieldPage);
                footerParagraph.AppendText(" of ");
                footerParagraph.AppendField("number of pages", FieldType.FieldNumPages);
                footerParagraph.Format.HorizontalAlignment = HorizontalAlignment.Right;

                doc3.SaveToFile(values.lspath, FileFormat.Docx);

                MemoryStream ms = new MemoryStream();
                doc3.SaveToStream(ms, FileFormat.Docx);

                bool status;
                status = objcmnstorage.UploadStream("../../../erpdocument", lscompany_code + "/" + "Master/CAMdocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + lsfile_gid + "/" + values.lsname, ms);
                ms.Close();
                values.lscloudpath = objcmnstorage.EncryptData(values.lscloudpath);
               

                File.Delete(htmlFilePath);

                values.status = true;
                values.message = "CAM Generated Successfully";
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
            }
        }
        //Get Application CAM Content
        public void DaGetApp2CAM(string application_gid, MdlTemplatelist values)
        {
            values.mstcontent_flag = "N";
            msSQL = " select cam_content,document_path,document_name,template_content,template_name,template_gid " +
                    " from ocs_mst_tapplication a " +
                    " left join ocs_trn_tdynamictemplatedtl b on b.templatetype_gid = a.application_gid " +
                    " and templatetype_name='" + getTemplateClass.CAMGeneration + "' " +
                    " where a.application_gid='" +application_gid+"'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.template_content = (objODBCDatareader["cam_content"].ToString().Replace("''", "'"));
                values.lspath = objcmnstorage.EncryptData(objODBCDatareader["document_path"].ToString());
                values.lsname = (objODBCDatareader["document_name"].ToString());
                values.defaulttemplate_content = (objODBCDatareader["template_content"].ToString());
                values.template_name = (objODBCDatareader["template_name"].ToString());
                values.template_gid = objODBCDatareader["template_gid"].ToString();
            }
            objODBCDatareader.Close();
              
            if (values.template_name == "" || values.template_name == null)
            {
                values.mstcontent_flag = "Y";
                msSQL = " select template_gid,template_name,template_content from ocs_mst_ttemplate a " +
                       " left join ocs_mst_tapplication b on a.vertical_gid = b.vertical_gid and a.program_gid = b.program_gid " +
                       " where b.application_gid = '" + application_gid + "' and a.template_type='" + getTemplateClass.CAMGeneration + "'" +
                       " and a.template_status='Y'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var objMdlTemplatedtl = new List<MdlTemplatedtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        objMdlTemplatedtl.Add(new MdlTemplatedtl
                        {
                            template_gid = dt["template_gid"].ToString(),
                            template_name = dt["template_name"].ToString(),
                            template_content = dt["template_content"].ToString(),
                        });
                    }
                }
                values.MdlTemplatedtl = objMdlTemplatedtl;
            }
            values.status = true;
        }

        public void DaPostWordSave(string employee_gid, MdlMstCAMGeneration values)
        {
            msSQL = " update ocs_mst_tapplication set cam_content='" + values.content.Replace("'", "''") + "'," +
                    " camgenerated_by='" + employee_gid + "'," +
                    " camgenerated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where application_gid='" + values.application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " update ocs_trn_tdynamictemplatedtl set template_content='" + values.defaulttemplate_content.Replace("'", "''") + "' " + 
                    " where templatetype_gid='" + values.application_gid + "' and templatetype_name='" + getTemplateClass.CAMGeneration + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "CAM Document Saved Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Saving CAM";
            }
        }


    }
}