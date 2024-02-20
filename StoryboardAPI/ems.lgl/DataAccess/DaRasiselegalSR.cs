using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.Odbc;
using System.IO;
using ems.utilities.Functions;
using ems.lgl.Models;
using ems.storage.Functions;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace ems.lgl.DataAccess
{
    public class DaRasiselegalSR
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        
        OdbcDataReader objODBCDatareader;
        OdbcConnection objODBCconnection;
        DataTable dt_datatable;
        string msSQL, msgetGID, msGetGIDREF;
        string ls_server, ls_username, ls_password, lsto_mail, frommail_id, tomail_id, tomailid_list, ccmail_id, ccmailid_list, body, sub, lscontent, lawyeruser_password, lawyeruser_code, lawyeruser_name = string.Empty;
        string lssrref_no, lscustomer_name, lscustomer_urn, lsraised_by, lsauth_by, lsraised_date, lsapproved_by, lsapproved_date;
        HttpPostedFile httpPostedFile;
        int mnResult, ls_port, lspromotor_age, lsguarantor_age;
        double lspromotor_mobile;
        DataTable objTblRQ,table;
        DataTable dt_table;
        Double lscount;
        string employee, msGetGid,lspath, lscustomer_gid, deal_year,lsurn, lstemplegalsr_gid;

        public bool DaGetPromoterDtl(string templegalsr_gid, MdlRaiselegalSR values)
        {
            msSQL = "select customer_gid from lgl_tmp_traiselegalSR where templegalsr_gid='" + templegalsr_gid + "'";
            lscustomer_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select promoter_name,designation,promoter_age,mobile from ocs_mst_tcustomer2promotor where customer_gid='" + lscustomer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getpromoter_list = new List<promoter_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getpromoter_list.Add(new promoter_list
                    {
                        promoter_name = (dr_datarow["promoter_name"].ToString()),
                        promoter_designation = (dr_datarow["designation"].ToString()),
                        promoter_age = dr_datarow["promoter_age"].ToString(),
                        promotermobile_no = dr_datarow["mobile"].ToString(),
                    });
                }
                values.promoter_list = getpromoter_list;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetCollateral(string templegalsr_gid, MdlRaiselegalSR values)
        {           
            msSQL = "select customer_gid from lgl_tmp_traiselegalSR where templegalsr_gid='" + templegalsr_gid + "'";
            lscustomer_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select concat(security_type,' / ',security_description) as collateral from ocs_trn_tcustomercollateral where customer_gid='" + lscustomer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.collateral_securities = objODBCDatareader["collateral"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
            return true;
        }

        public bool DaGetCustomer(MdlRaiselegalSR objCustomer)
        {
            msSQL = " select a.customer_gid,a.customer_code,concat(a.customername,' / ',a.customer_urn) as customername,a.contactperson," +
                    " concat(address,'<br/>',address2,'<br/>',state,' - ',postalcode,'<br/>',country) as address,email" +
                    " from ocs_mst_tcustomer a where a.customer_gid not in (select customer_gid from lgl_tmp_traiselegalSR where auth_status='Approved' and approval_status='Approved'or auth_status='Hold' ) order by a.customername  ";           
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcustomer = new List<customer_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcustomer.Add(new customer_list
                    {
                        customer_gid = (dr_datarow["customer_gid"].ToString()),
                        customercode = (dr_datarow["customer_code"].ToString()),
                        customername = (dr_datarow["customername"].ToString()),
                        contactperson = (dr_datarow["contactperson"].ToString()),
                    });
                }
                objCustomer.customer_list = getcustomer;
            }
            dt_datatable.Dispose();
            objCustomer.status = true;
            return true;            
        }

        public bool DaGetGuarantorDtl(string templegalsr_gid, MdlRaiselegalSR values)
        {
            msSQL = "select customer_gid from lgl_tmp_traiselegalSR where templegalsr_gid='" + templegalsr_gid + "'";
            lscustomer_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select guarantors_name,guarantor_age,networth,basisofNW from  ocs_mst_tcustomer2guarantor where customer_gid='" + lscustomer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getguarantor_list = new List<guarantor_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getguarantor_list.Add(new guarantor_list
                    {
                        guarantors_name = (dr_datarow["guarantors_name"].ToString()),
                        guarantor_age = (dr_datarow["guarantor_age"].ToString()),
                        networth = dr_datarow["networth"].ToString(),
                        basisofNW = dr_datarow["basisofNW"].ToString(),
                    });
                }
                values.guarantor_list = getguarantor_list;
            }
            dt_datatable.Dispose();  
            
            msSQL= "select remarks from lgl_tmp_traiselegalSR where templegalsr_gid='" + templegalsr_gid + "'";
            values.remarks = objdbconn.GetExecuteScalar(msSQL);
            values.status = true;
            return true;
           
        }

        public bool DaGetCustomerDtl(string customer_gid, MdlRaiselegalSR values)
        {
            msSQL = "select concat(customername,' / ',customer_urn) as customername,concat(address,' ',address2,' ' ,state,' - ',postalcode,' ',country) as address,"+
                " email,mobileno from  ocs_mst_tcustomer where customer_gid='" + customer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.address = objODBCDatareader["address"].ToString();
                values.email_id = objODBCDatareader["email"].ToString();
                values.customer_name = objODBCDatareader["customername"].ToString();
                values.mobile_no = objODBCDatareader["mobileno"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = "select customer_urn from ocs_mst_tcustomer where customer_gid='" + customer_gid + "'";
            string lsurn = objdbconn.GetExecuteScalar(msSQL);
            if(lsurn==""||lsurn==null)
            {
                msSQL = "select customer_urn from ocs_tmp_tcustomer where tmpcustomer_gid='" + customer_gid + "'";
                 lsurn = objdbconn.GetExecuteScalar(msSQL);
                if (lsurn == "" || lsurn == null)
                {
                    values.deal_year = "URN Not Matched";
                    return false;
                }
            }
            msSQL = "select urn from lgl_tmp_tmisdata where urn ='" + lsurn + "'";
            string lsdata = objdbconn.GetExecuteScalar(msSQL);

            if(lsdata==""||lsdata==null)
            {
                values.deal_year = "URN Not Matched";
            }
            else
            {

            msSQL = " select  YEAR(str_to_date(disbursement_date,'%m-%d-%Y')) as deal_year from lgl_tmp_tmisdata  where urn='" + lsurn + "' group by urn";
            values.deal_year = objdbconn.GetExecuteScalar(msSQL);       
            if (values.deal_year == "" || values.deal_year == null)
            {
                msSQL = " select  YEAR(str_to_date(disbursement_date,'%m/%d/%Y')) as deal_year from lgl_tmp_tmisdata  where urn='" + lsurn + "' group by urn";
                values.deal_year = objdbconn.GetExecuteScalar(msSQL);
            }
            if (values.deal_year == "" || values.deal_year == null)
            {
                msSQL = " select  YEAR(str_to_date(disbursement_date,'%d-%m-%Y')) as deal_year from lgl_tmp_tmisdata  where urn='" + lsurn + "' group by urn";
                values.deal_year = objdbconn.GetExecuteScalar(msSQL);
            }
            if (values.deal_year == "" || values.deal_year == null)
            {
                msSQL = " select  YEAR(str_to_date(disbursement_date,'%d/%m-%Y')) as deal_year from lgl_tmp_tmisdata  where urn='" + lsurn + "' group by urn";
                values.deal_year = objdbconn.GetExecuteScalar(msSQL);
                
            }

            }
            values.status = true;
            return true;


        }

        public bool DaPostRaiseLegalSR(string employee_gid, string user_gid, MdlRaiselegalSR values)
        {       
            msSQL = "select customer_urn from ocs_mst_tcustomer where customer_gid = '" + values.customer_gid + "'";
            string urn = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select customername from ocs_mst_tcustomer where customer_gid = '" + values.customer_gid + "'";
            string lscustomername = objdbconn.GetExecuteScalar(msSQL);

            msSQL= "select vertical_code from ocs_mst_tcustomer where customer_gid = '" + values.customer_gid + "'";
            string lsvertical = objdbconn.GetExecuteScalar(msSQL);

            msgetGID = objcmnfunctions.GetMasterGID("TLSR");
            msGetGIDREF = objcmnfunctions.GetMasterGID("LSR_");
            msSQL = "insert into lgl_tmp_traiselegalSR (" +
                " templegalsr_gid," +
                " srref_no," +
                " vertical,"+
                " customer_gid," +
                " customer_urn ," +
                " account_name," +
                " remarks,"+
                " created_by," +
                " created_date)" +
                " values (" +
                  "'" + msgetGID + "'," +
                      "'" + msGetGIDREF + "'," +
                      "'" + lsvertical + "'," +
                      "'" + values.customer_gid + "'," +
                      "'" + urn + "'," +
                      "'" + lscustomername.Replace("    ", "").Replace("\n", "") + "'," +
                      "'" + values.remarks.Replace("'", "") + "'," +
                      "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Legal SR added successfully";

                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while adding Legal SR";
                return false;
            }
        }

        public bool DaPostSaveLegalSR(string employee_gid, string user_gid, MdlRaiselegalSR values)
        {

            msSQL = "update lgl_tmp_traiselegalSR set " +
                " constitution='" + values.constitution + "'," +
                " financed_by='" + values.financed_by + "'," +
                " deal_year='" + values.deal_year + "',";
            if (values.business_activity == null)
            {
                msSQL += " business_activity='',";

            }
            else
            {
                msSQL += " business_activity='" + values.business_activity.Replace("'", "\\'") + "'," ;
            }
            if (values.email_id == null)
            {
                msSQL += " email_id='',";

            }
            else
            {
                msSQL += " email_id='" + values.email_id.Replace("'", "\\'") + "'," ;
            }
            if (values.primary_securities == null)
            {
                msSQL += " primary_securities='',";

            }
            else
            {
                msSQL += " primary_securities='" + values.primary_securities.Replace("'", "\\'") + "'," ;
            }
            if (values.collateral_securities == null)
            {
                msSQL += " collateral_securities='',";

            }
            else
            {
                msSQL += " collateral_securities='" + values.collateral_securities.Replace("'", "\\'") + "'," ;
            }
            if (values.details_UDC_PDC == null)
            {
                msSQL += " details_UDC_PDC='',";

            }
            else
            {
                msSQL += " details_UDC_PDC='" + values.details_UDC_PDC.Replace("'", "\\'") + "',";
            }
            if (values.unit_working_status == null)
            {
                msSQL += " unit_working_status='',";

            }
            else
            {
                msSQL += " unit_working_status='" + values.unit_working_status.Replace("'", "\\'") + "',";
            }
            if (values.other_banker_exposures == null)
            {
                msSQL += " other_banker_exposures='',";

            }
            else
            {
                msSQL += " other_banker_exposures='" + values.other_banker_exposures.Replace("'", "\\'") + "',";
            }
            if (values.cibil_data == null)
            {
                msSQL += " cibil_data='',";

            }
            else
            {
                msSQL += " cibil_data='" + values.cibil_data.Replace("'", "\\'") + "',";
            }
            if (values.restructuring_data == null)
            {
                msSQL += " restructuring_data='',";

            }
            else
            {
                msSQL += " restructuring_data='" + values.restructuring_data.Replace("'", "\\'") + "',";
            }
            if (values.status_current_overdue == null)
            {
                msSQL += " status_current_overdue='',";

            }
            else
            {
                msSQL += " status_current_overdue='" + values.status_current_overdue.Replace("'", "\\'") + "',";
            }
            if (values.other_group_companies == null)
            {
                msSQL += " other_group_companies='',";

            }
            else
            {
                msSQL += " other_group_companies='" + values.other_group_companies.Replace("'", "\\'") + "',";
            }
            if (values.meeting_details == null)
            {
                msSQL += " meeting_details='',";

            }
            else
            {
                msSQL += " meeting_details='" + values.meeting_details.Replace("'", "\\'") + "',";
            }

            msSQL += " created_by='" + employee_gid + "'," +
             " created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";

            if (values.cycles_sanctiondated == null)
            {
                msSQL += " cycles_sanctiondated='',";

            }
            else
            {
                msSQL += " cycles_sanctiondated='" + values.cycles_sanctiondated.Replace("'", "\\'") + "',";
            }
            if (values.limit_sanction == null)
            {
                msSQL += " limit_sanction='',";

            }
            else
            {
                msSQL += " limit_sanction='" + values.limit_sanction.Replace("'", "\\'") + "',";
            }
            if (values.churing_account == null)
            {
                msSQL += " churing_account='',";

            }
            else
            {
                msSQL += " churing_account='" + values.churing_account.Replace("'", "\\'") + "',";
            }
            if (values.instances_PTP == null)
            {
                msSQL += " instances_PTP='',";

            }
            else
            {
                msSQL += " instances_PTP='" + values.instances_PTP.Replace("'", "\\'") + "',";
            }
            if (values.statuslegal_action == null)
            {
                msSQL += " statuslegal_action='',";

            }
            else
            {
                msSQL += " statuslegal_action='" + values.statuslegal_action.Replace("'", "\\'") + "',";
            }
            if (values.demandnotice_details == null)
            {
                msSQL += " demandnotice_details='',";

            }
            else
            {
                msSQL += " demandnotice_details='" + values.demandnotice_details.Replace("'", "\\'") + "',";
            }
            if (values.other_banker_borrower == null)
            {
                msSQL += " other_banker_borrower=''";

            }
            else
            {
                msSQL += " other_banker_borrower='" + values.other_banker_borrower.Replace("'", "\\'") + "'";
            }

            msSQL +=" where templegalsr_gid='" + values.templegalsr_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select * from lgl_tmp_tcontactdtlRM where created_by ='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string msget_Gid = objcmnfunctions.GetMasterGID("LGCA");

                    msSQL = "insert into lgl_trn_tcontactdtlRM (" +
                      " contactdtl_gid ," +
                      " legalSR_gid ," +
                      " contact_name ," +
                      " contact_location ," +
                      " contact_mobileno ," +
                      " created_by ," +
                      " created_date )" +
                      " values(" +
                      "'" + msget_Gid + "'," +
                       "'" + values.templegalsr_gid + "'," +
                       "'" + dt["contact_name"].ToString() + "'," +
                       "'" + dt["contact_location"].ToString() + "'," +
                       "'" + dt["contact_mobileno"].ToString() + "'," +
                       "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = "delete from lgl_tmp_tcontactdtlRM where tmpcontactdtl_gid='" + dt["tmpcontactdtl_gid"].ToString() + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Legal SR saved successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while save Legal SR";
                return false;
            }
        }

        public bool DaPostSubmitRaiseLegalSR(string employee_gid, string user_gid, MdlRaiselegalSR values)
        {         
            msSQL = "select customer_urn from ocs_mst_tcustomer where customer_gid = '" + values.customer_gid + "'";
            string urn = objdbconn.GetExecuteScalar(msSQL);            
           
            msSQL = "select customer_gid,srref_no,customer_urn,account_name from lgl_tmp_traiselegalsr where templegalsr_gid='" + values.templegalsr_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                msSQL = "update lgl_tmp_traiselegalSR set " +
               " constitution='" + values.constitution + "'," +
               " financed_by='" + values.financed_by + "'," +
               " deal_year='" + values.deal_year + "',";
                if (values.business_activity == null)
                {
                    msSQL += " business_activity='',";

                }
                else
                {
                    msSQL += " business_activity='" + values.business_activity.Replace("'", "\\'") + "',";
                }
                if (values.email_id == null)
                {
                    msSQL += " email_id='',";

                }
                else
                {
                    msSQL += " email_id='" + values.email_id.Replace("'", "\\'") + "',";
                }
                if (values.primary_securities == null)
                {
                    msSQL += " primary_securities='',";

                }
                else
                {
                    msSQL += " primary_securities='" + values.primary_securities.Replace("'", "\\'") + "',";
                }
                if (values.collateral_securities == null)
                {
                    msSQL += " collateral_securities='',";

                }
                else
                {
                    msSQL += " collateral_securities='" + values.collateral_securities.Replace("'", "\\'") + "',";
                }
                if (values.details_UDC_PDC == null)
                {
                    msSQL += " details_UDC_PDC='',";

                }
                else
                {
                    msSQL += " details_UDC_PDC='" + values.details_UDC_PDC.Replace("'", "\\'") + "',";
                }
                if (values.unit_working_status == null)
                {
                    msSQL += " unit_working_status='',";

                }
                else
                {
                    msSQL += " unit_working_status='" + values.unit_working_status.Replace("'", "\\'") + "',";
                }
                if (values.other_banker_exposures == null)
                {
                    msSQL += " other_banker_exposures='',";

                }
                else
                {
                    msSQL += " other_banker_exposures='" + values.other_banker_exposures.Replace("'", "\\'") + "',";
                }
                if (values.cibil_data == null)
                {
                    msSQL += " cibil_data='',";

                }
                else
                {
                    msSQL += " cibil_data='" + values.cibil_data.Replace("'", "\\'") + "',";
                }
                if (values.restructuring_data == null)
                {
                    msSQL += " restructuring_data='',";

                }
                else
                {
                    msSQL += " restructuring_data='" + values.restructuring_data.Replace("'", "\\'") + "',";
                }
                if (values.status_current_overdue == null)
                {
                    msSQL += " status_current_overdue='',";

                }
                else
                {
                    msSQL += " status_current_overdue='" + values.status_current_overdue.Replace("'", "\\'") + "',";
                }
                if (values.other_group_companies == null)
                {
                    msSQL += " other_group_companies='',";

                }
                else
                {
                    msSQL += " other_group_companies='" + values.other_group_companies.Replace("'", "\\'") + "',";
                }
                if (values.meeting_details == null)
                {
                    msSQL += " meeting_details='',";

                }
                else
                {
                    msSQL += " meeting_details='" + values.meeting_details.Replace("'", "\\'") + "',";
                }

                msSQL += " created_by='" + employee_gid + "'," +
                 " created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";

                if (values.cycles_sanctiondated == null)
                {
                    msSQL += " cycles_sanctiondated='',";

                }
                else
                {
                    msSQL += " cycles_sanctiondated='" + values.cycles_sanctiondated.Replace("'", "\\'") + "',";
                }
                if (values.limit_sanction == null)
                {
                    msSQL += " limit_sanction='',";

                }
                else
                {
                    msSQL += " limit_sanction='" + values.limit_sanction.Replace("'", "\\'") + "',";
                }
                if (values.churing_account == null)
                {
                    msSQL += " churing_account='',";

                }
                else
                {
                    msSQL += " churing_account='" + values.churing_account.Replace("'", "\\'") + "',";
                }
                if (values.instances_PTP == null)
                {
                    msSQL += " instances_PTP='',";

                }
                else
                {
                    msSQL += " instances_PTP='" + values.instances_PTP.Replace("'", "\\'") + "',";
                }
                if (values.statuslegal_action == null)
                {
                    msSQL += " statuslegal_action='',";

                }
                else
                {
                    msSQL += " statuslegal_action='" + values.statuslegal_action.Replace("'", "\\'") + "',";
                }
                if (values.demandnotice_details == null)
                {
                    msSQL += " demandnotice_details='',";

                }
                else
                {
                    msSQL += " demandnotice_details='" + values.demandnotice_details.Replace("'", "\\'") + "',";
                }
                if (values.other_banker_borrower == null)
                {
                    msSQL += " other_banker_borrower=''";

                }
                else
                {
                    msSQL += " other_banker_borrower='" + values.other_banker_borrower.Replace("'", "\\'") + "'";
                }

                msSQL += " where templegalsr_gid='" + values.templegalsr_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msgetGID = objcmnfunctions.GetMasterGID("RLSR");
                msSQL = "insert into lgl_trn_traiselegalSR (" +
                          " raiselegalSR_gid  ," +
                          " templegalsr_gid ," +
                          " srref_no ," +
                          " customer_gid ," +
                          " account_name ," +
                          " constitution ," +
                          " financed_by ," +
                          " deal_year ," +
                          " business_activity ," +
                          " email_id ," +
                          " primary_securities ," +
                          " cycles_sanctiondated ," +
                          " limit_sanction ," +
                          " collateral_securities," +
                          " details_UDC_PDC ," +
                          " unit_working_status ," +
                          " other_banker_exposures ," +
                          " other_banker_borrower," +
                          " cibil_data ," +
                          " restructuring_data ," +
                          " status_current_overdue," +
                          " other_group_companies," +
                          " meeting_details," +
                          " churing_account ," +
                          " urn ," +
                          " instances_PTP," +
                          " statuslegal_action," +
                          " demandnotice_details," +
                          " auth_status ," +
                          " approval_status ," +
                          " created_by," +
                          " created_date)" +
                          " values(" +
                          "'" + msgetGID + "'," +
                          "'" + values.templegalsr_gid + "'," +
                          "'" + objODBCDatareader["srref_no"].ToString() + "'," +
                          "'" + objODBCDatareader["customer_gid"].ToString() + "'," +
                          "'" + objODBCDatareader["account_name"].ToString() + "'," +
                          "'" + values.constitution + "'," +
                          "'" + values.financed_by + "'," +
                          "'" + values.deal_year + "',";
                if (values.business_activity == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.business_activity.Replace("'", "\\'") + "',";
                }
                if (values.email_id == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.email_id.Replace("'", "\\'") + "'," ;
                }

                if (values.primary_securities == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.primary_securities.Replace("'", "\\'") + "',";
                }
                if (values.cycles_sanctiondated == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.cycles_sanctiondated.Replace("'", "\\'") + "'," ;
                }
                if (values.limit_sanction == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.limit_sanction.Replace("'", "\\'") + "',";
                }
                if (values.collateral_securities == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.collateral_securities.Replace("'", "\\'") + "'," ;
                }
                if (values.details_UDC_PDC == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.details_UDC_PDC.Replace("'", "\\'") + "',";
                }
                if (values.unit_working_status == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.unit_working_status.Replace("'", "\\'") + "',";
                }
                if (values.other_banker_exposures == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.other_banker_exposures.Replace("'", "\\'") + "',";
                }
                if (values.other_banker_borrower == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.other_banker_borrower.Replace("'", "\\'") + "',";
                }
                if (values.cibil_data == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.cibil_data.Replace("'", "\\'") + "',";
                }
                if (values.restructuring_data == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.restructuring_data.Replace("'", "\\'") + "',";
                }
                if (values.status_current_overdue == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.status_current_overdue.Replace("'", "\\'") + "',";
                }
                if (values.other_group_companies == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.other_group_companies.Replace("'", "\\'") + "',";
                }
                if (values.meeting_details == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.meeting_details.Replace("'", "\\'") + "',";
                }
                if (values.churing_account == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.churing_account.Replace("'", "\\'") + "',";
                }

                msSQL += "'" + objODBCDatareader["customer_urn"].ToString() + "',";
                if (values.instances_PTP == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.instances_PTP.Replace("'", "\\'") + "',";
                }
                if (values.statuslegal_action == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.statuslegal_action.Replace("'", "\\'") + "',";
                }
                if (values.demandnotice_details == null)
                {
                    msSQL += "'',";

                }
                else
                {
                    msSQL += "'" + values.demandnotice_details.Replace("'", "\\'") + "',";
                }

             
                        msSQL += "'Pending'," +
                                " '---'," +
                              "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDatareader.Close();

            msSQL = "select * from lgl_tmp_tcontactdtlRM where created_by ='" + employee_gid + "' and customer_gid='"+ values.customer_gid+"'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string msget_Gid = objcmnfunctions.GetMasterGID("LGCA");

                    msSQL = "insert into lgl_trn_tcontactdtlRM (" +
                      " contactdtl_gid ," +
                      " legalSR_gid ," +
                      " contact_name ," +
                      " contact_location ," +
                      " contact_mobileno ," +
                      " created_by ," +
                      " created_date )" +
                      " values(" +
                      "'" + msget_Gid + "'," +
                       "'" + msgetGID + "'," +
                       "'" + dt["contact_name"].ToString() + "'," +
                       "'" + dt["contact_location"].ToString() + "'," +
                       "'" + dt["contact_mobileno"].ToString() + "'," +
                       "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    
                }
                dt_datatable.Dispose();
            }
            else
            {
                msSQL = "select * from lgl_trn_tcontactdtlRM where legalSR_gid='" + values.templegalsr_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {

                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        string msget_Gid = objcmnfunctions.GetMasterGID("LGCA");

                        msSQL = "insert into lgl_trn_tcontactdtlRM (" +
                          " contactdtl_gid ," +
                          " legalSR_gid ," +
                          " contact_name ," +
                          " contact_location ," +
                          " contact_mobileno ," +
                          " created_by ," +
                          " created_date )" +
                          " values(" +
                          "'" + msget_Gid + "'," +
                           "'" + msgetGID + "'," +
                           "'" + dt["contact_name"].ToString() + "'," +
                           "'" + dt["contact_location"].ToString() + "'," +
                           "'" + dt["contact_mobileno"].ToString() + "'," +
                           "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                       
                    }
                    
                }
                dt_datatable.Dispose();
            }

            msSQL = " select a.customer_name,a.security_type,a.security_description,a.account_status,b.loanref_no, " +
                      " b.loan_title,b.sanctionref_no,b.sanction_date from ocs_trn_tcustomercollateral a " +
                      " left join ocs_trn_tcollateral2loan b on a.collateral_gid = b.collateral_gid where customer_gid = '" + values.customer_gid + "'" +
                      " group by a.collateral_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
           
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    string msget_Gid = objcmnfunctions.GetMasterGID("LGCL");
                    msSQL ="insert into lgl_trn_tlegalsr2collateral("+
                        " legalsr2collateral_gid,"+
                        " legalsr_gid ,"+
                        " customer_gid ,"+
                        " security_type," +
                        " security_description ,"+
                        " account_status ,"+
                        " created_by,"+
                        " created_date)"+
                        " values ("+
                        "'"+ msget_Gid + "',"+
                        "'" + msgetGID + "'," +
                        "'" + values.customer_gid+"',"+
                        "'" + dr_datarow["security_type"].ToString() + "'," +
                        "'" + dr_datarow["security_description"].ToString() + "'," +
                         "'" + dr_datarow["account_status"].ToString() + "'," +
                         "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
               
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {               
                msSQL = "update lgl_tmp_traiselegalSR set auth_status='Pending' where templegalsr_gid='" + values.templegalsr_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Legal SR added successfully";

                try
                {

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();

                    }
                    objODBCDatareader.Close();

                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();

                    msSQL = "select tomailid from ocs_trn_ttomaillist where mailtrigger_function='Legal SR Raised'";

                    dt_table = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dr_datarow in dt_table.Rows)
                    {
                        tomail_id = "";
                        tomail_id += dr_datarow["tomailid"].ToString();
                        message.To.Add(new MailAddress(tomail_id));
                        tomailid_list += "" + tomail_id + ",";
                    }
                    tomailid_list = tomailid_list.TrimEnd(',');
                    dt_table.Dispose();

                    msSQL = "select employee_mailid from ocs_trn_tmailcclist where mailtrigger_function='Legal SR Raised'";

                    dt_table = objdbconn.GetDataTable(msSQL);
                    
                    foreach (DataRow dr_datarow in dt_table.Rows)
                    {
                        ccmail_id = "";
                        ccmail_id += dr_datarow["employee_mailid"].ToString();
                        message.CC.Add(new MailAddress(ccmail_id));
                        ccmailid_list += "" + ccmail_id + ",";
                    }
                    ccmailid_list = ccmailid_list.TrimEnd(',');
                    dt_table.Dispose();


                    msSQL = " select srref_no, d.customername as customer_name, d.customer_urn as customer_urn," +
                          " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as raised_by," +
                          " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raised_date " +
                          " from lgl_trn_traiselegalSR a " +
                          " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                          " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                          " left join ocs_mst_tcustomer d on a.customer_gid = d.customer_gid" +
                          " where raiselegalSR_gid ='" + msgetGID + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lssrref_no = objODBCDatareader["srref_no"].ToString();
                        lscustomer_name = objODBCDatareader["customer_name"].ToString();
                        lscustomer_urn = objODBCDatareader["customer_urn"].ToString();
                        lsraised_by = objODBCDatareader["raised_by"].ToString();
                        lsraised_date = objODBCDatareader["raised_date"].ToString();
                    }
                    objODBCDatareader.Close();

                    sub = " Legal SR ";


                    body = "Dear Sir/Madam,  <br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "<b>" + HttpUtility.HtmlEncode(lsraised_by) + "</b>" + " has raised Legal SR in Legal. Kindly do the needful.<br />";
                    body = body + "<br />";
                    body = body + "<b>Ref No :</b> " + HttpUtility.HtmlEncode(lssrref_no) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Customer Name/URN: :</b> " + HttpUtility.HtmlEncode(lscustomer_name) + "/" + HttpUtility.HtmlEncode(lscustomer_urn) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Raised Date :</b> " + lsraised_date + "<br />";
                    body = body + "<br />";


                    body = body + "<b>Yours Sincerely,</b> ";
                    body = body + "<br />";
                    body = body + "Samunnati Financial Intermediation & Services Pvt Ltd.<br /> ";
                    body = body + "<br />";
                    body = body + "<b>Please Note:</b> " + "This is an auto generated e-mail that cannot receive replies. " + "<br />";
                    body = body + "<br />";


                    message.From = new MailAddress(ls_username);


                    message.Subject = sub;
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = body;
                    smtp.Port = ls_port;
                    smtp.Host = ls_server; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    values.status = true; 

                        if (values.status == true)
                        {
                            msSQL = "Insert into lgl_trn_legalsrsentmail( " +
                            " raiselegalSR_gid," +
                            " from_mail," +
                            " to_mail," +
                            " cc_mail," +
                            " mail_status," +
                            " mail_senddate, " +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msgetGID + "'," + 
                            "'" + ls_username + "'," +
                            "'" + tomailid_list + "'," +
                            "'" + ccmailid_list + "'," +
                            "'Legal SR Raised'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }

                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                    values.status = false;
                }

                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while adding Legal SR";
                return false;
            }
        }


        public bool DaPostUpdateRaiseLegalSR(string employee_gid, string user_gid, MdlRaiselegalSR values)
        {
            msSQL = "select customer_urn from ocs_mst_tcustomer where customer_gid = '" + values.customer_gid + "'";
            string urn = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select customer_gid,srref_no,customer_urn,account_name from lgl_tmp_traiselegalsr where templegalsr_gid='" + values.templegalsr_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                msSQL = "update lgl_tmp_traiselegalSR  set " +
               " constitution='" + values.constitution + "'," +
               " financed_by='" + values.financed_by + "'," +
               " deal_year='" + values.deal_year + "',";
                if (values.business_activity == null)
                {
                    
                       msSQL += " business_activity='',";

                }
                else
                {
                    msSQL += " business_activity='" + values.business_activity.Replace("'", "\\'") + "',";
                }
                if (values.email_id == null)
                {
                    msSQL += " email_id='',";

                }
                else
                {
                    msSQL += " email_id='" + values.email_id.Replace("'", "\\'") + "',";
                }
                if (values.primary_securities == null)
                {
                    msSQL += " primary_securities='',";

                }
                else
                {
                    msSQL += " primary_securities='" + values.primary_securities.Replace("'", "\\'") + "',";
                }
                if (values.collateral_securities == null)
                {
                    msSQL += " collateral_securities='',";

                }
                else
                {
                    msSQL += " collateral_securities='" + values.collateral_securities.Replace("'", "\\'") + "',";
                }
                if (values.details_UDC_PDC == null)
                {
                    msSQL += " details_UDC_PDC='',";

                }
                else
                {
                    msSQL += " details_UDC_PDC='" + values.details_UDC_PDC.Replace("'", "\\'") + "',";
                }
                if (values.unit_working_status == null)
                {
                    msSQL += " unit_working_status='',";

                }
                else
                {
                    msSQL += " unit_working_status='" + values.unit_working_status.Replace("'", "\\'") + "',";
                }
                if (values.other_banker_exposures == null)
                {
                    msSQL += " other_banker_exposures='',";

                }
                else
                {
                    msSQL += " other_banker_exposures='" + values.other_banker_exposures.Replace("'", "\\'") + "',";
                }
                if (values.cibil_data == null)
                {
                    msSQL += " cibil_data='',";

                }
                else
                {
                    msSQL += " cibil_data='" + values.cibil_data.Replace("'", "\\'") + "',";
                }
                if (values.restructuring_data == null)
                {
                    msSQL += " restructuring_data='',";

                }
                else
                {
                    msSQL += " restructuring_data='" + values.restructuring_data.Replace("'", "\\'") + "',";
                }
                if (values.status_current_overdue == null)
                {
                    msSQL += " status_current_overdue='',";

                }
                else
                {
                    msSQL += " status_current_overdue='" + values.status_current_overdue.Replace("'", "\\'") + "',";
                }
                if (values.other_group_companies == null)
                {
                    msSQL += " other_group_companies='',";

                }
                else
                {
                    msSQL += " other_group_companies='" + values.other_group_companies.Replace("'", "\\'") + "',";
                }
                if (values.meeting_details == null)
                {
                    msSQL += " meeting_details='',";

                }
                else
                {
                    msSQL += " meeting_details='" + values.meeting_details.Replace("'", "\\'") + "',";
                }

                msSQL += " created_by='" + employee_gid + "'," +
                 " created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";

                if (values.cycles_sanctiondated == null)
                {
                    msSQL += " cycles_sanctiondated='',";

                }
                else
                {
                    msSQL += " cycles_sanctiondated='" + values.cycles_sanctiondated.Replace("'", "\\'") + "',";
                }
                if (values.limit_sanction == null)
                {
                    msSQL += " limit_sanction='',";

                }
                else
                {
                    msSQL += " limit_sanction='" + values.limit_sanction.Replace("'", "\\'") + "',";
                }
                if (values.churing_account == null)
                {
                    msSQL += " churing_account='',";

                }
                else
                {
                    msSQL += " churing_account='" + values.churing_account.Replace("'", "\\'") + "',";
                }
                if (values.instances_PTP == null)
                {
                    msSQL += " instances_PTP='',";

                }
                else
                {
                    msSQL += " instances_PTP='" + values.instances_PTP.Replace("'", "\\'") + "',";
                }
                if (values.statuslegal_action == null)
                {
                    msSQL += " statuslegal_action='',";

                }
                else
                {
                    msSQL += " statuslegal_action='" + values.statuslegal_action.Replace("'", "\\'") + "',";
                }
                if (values.demandnotice_details == null)
                {
                    msSQL += " demandnotice_details='',";

                }
                else
                {
                    msSQL += " demandnotice_details='" + values.demandnotice_details.Replace("'", "\\'") + "',";
                }
                if (values.other_banker_borrower == null)
                {
                    msSQL += " other_banker_borrower=''";

                }
                else
                {
                    msSQL += " other_banker_borrower='" + values.other_banker_borrower.Replace("'", "\\'") + "'";
                }

                msSQL += " where templegalsr_gid='" + values.templegalsr_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                 //msgetGID = objcmnfunctions.GetMasterGID("RLSR");
                msSQL = "update lgl_trn_traiselegalSR  set " +
              " constitution='" + values.constitution + "'," +
              " financed_by='" + values.financed_by + "'," +
              " deal_year='" + values.deal_year + "',";
                if (values.business_activity == null)
                {

                    msSQL += " business_activity='',";

                }
                else
                {
                    msSQL += " business_activity='" + values.business_activity.Replace("'", "\\'") + "',";
                }
                if (values.email_id == null)
                {
                    msSQL += " email_id='',";

                }
                else
                {
                    msSQL += " email_id='" + values.email_id.Replace("'", "\\'") + "',";
                }
                if (values.primary_securities == null)
                {
                    msSQL += " primary_securities='',";

                }
                else
                {
                    msSQL += " primary_securities='" + values.primary_securities.Replace("'", "\\'") + "',";
                }
                if (values.collateral_securities == null)
                {
                    msSQL += " collateral_securities='',";

                }
                else
                {
                    msSQL += " collateral_securities='" + values.collateral_securities.Replace("'", "\\'") + "',";
                }
                if (values.details_UDC_PDC == null)
                {
                    msSQL += " details_UDC_PDC='',";

                }
                else
                {
                    msSQL += " details_UDC_PDC='" + values.details_UDC_PDC.Replace("'", "\\'") + "',";
                }
                if (values.unit_working_status == null)
                {
                    msSQL += " unit_working_status='',";

                }
                else
                {
                    msSQL += " unit_working_status='" + values.unit_working_status.Replace("'", "\\'") + "',";
                }
                if (values.other_banker_exposures == null)
                {
                    msSQL += " other_banker_exposures='',";

                }
                else
                {
                    msSQL += " other_banker_exposures='" + values.other_banker_exposures.Replace("'", "\\'") + "',";
                }
                if (values.cibil_data == null)
                {
                    msSQL += " cibil_data='',";

                }
                else
                {
                    msSQL += " cibil_data='" + values.cibil_data.Replace("'", "\\'") + "',";
                }
                if (values.restructuring_data == null)
                {
                    msSQL += " restructuring_data='',";

                }
                else
                {
                    msSQL += " restructuring_data='" + values.restructuring_data.Replace("'", "\\'") + "',";
                }
                if (values.status_current_overdue == null)
                {
                    msSQL += " status_current_overdue='',";

                }
                else
                {
                    msSQL += " status_current_overdue='" + values.status_current_overdue.Replace("'", "\\'") + "',";
                }
                if (values.other_group_companies == null)
                {
                    msSQL += " other_group_companies='',";

                }
                else
                {
                    msSQL += " other_group_companies='" + values.other_group_companies.Replace("'", "\\'") + "',";
                }
                if (values.meeting_details == null)
                {
                    msSQL += " meeting_details='',";

                }
                else
                {
                    msSQL += " meeting_details='" + values.meeting_details.Replace("'", "\\'") + "',";
                }

                msSQL += " created_by='" + employee_gid + "'," +
                 " created_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";

                if (values.cycles_sanctiondated == null)
                {
                    msSQL += " cycles_sanctiondated='',";

                }
                else
                {
                    msSQL += " cycles_sanctiondated='" + values.cycles_sanctiondated.Replace("'", "\\'") + "',";
                }
                if (values.limit_sanction == null)
                {
                    msSQL += " limit_sanction='',";

                }
                else
                {
                    msSQL += " limit_sanction='" + values.limit_sanction.Replace("'", "\\'") + "',";
                }
                if (values.churing_account == null)
                {
                    msSQL += " churing_account='',";

                }
                else
                {
                    msSQL += " churing_account='" + values.churing_account.Replace("'", "\\'") + "',";
                }
                if (values.instances_PTP == null)
                {
                    msSQL += " instances_PTP='',";

                }
                else
                {
                    msSQL += " instances_PTP='" + values.instances_PTP.Replace("'", "\\'") + "',";
                }
                if (values.statuslegal_action == null)
                {
                    msSQL += " statuslegal_action='',";

                }
                else
                {
                    msSQL += " statuslegal_action='" + values.statuslegal_action.Replace("'", "\\'") + "',";
                }
                if (values.demandnotice_details == null)
                {
                    msSQL += " demandnotice_details='',";

                }
                else
                {
                    msSQL += " demandnotice_details='" + values.demandnotice_details.Replace("'", "\\'") + "',";
                }
                if (values.other_banker_borrower == null)
                {
                    msSQL += " other_banker_borrower=''";

                }
                else
                {
                    msSQL += " other_banker_borrower='" + values.other_banker_borrower.Replace("'", "\\'") + "'";
                }

                //msSQL = "select * from lgl_tmp_tcontactdtlRM where created_by ='" + employee_gid + "' and customer_gid='" + values.customer_gid + "'";
                //dt_datatable = objdbconn.GetDataTable(msSQL);
                //if (dt_datatable.Rows.Count != 0)
                //{

                //    foreach (DataRow dt in dt_datatable.Rows)
                //    {
                //        string msget_Gid = objcmnfunctions.GetMasterGID("LGCA");

                //        msSQL = "insert into lgl_trn_tcontactdtlRM (" +
                //          " contactdtl_gid ," +
                //          " legalSR_gid ," +
                //          " contact_name ," +
                //          " contact_location ," +
                //          " contact_mobileno ," +
                //          " created_by ," +
                //          " created_date )" +
                //          " values(" +
                //          "'" + msget_Gid + "'," +
                //           "'" + msgetGID + "'," +
                //           "'" + dt["contact_name"].ToString() + "'," +
                //           "'" + dt["contact_location"].ToString() + "'," +
                //           "'" + dt["contact_mobileno"].ToString() + "'," +
                //           "'" + employee_gid + "'," +
                //            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //    }
                //    dt_datatable.Dispose();
                //}
                //else
                //{
                //    msSQL = "select * from lgl_trn_tcontactdtlRM where legalSR_gid='" + values.templegalsr_gid + "'";
                //    dt_datatable = objdbconn.GetDataTable(msSQL);
                //    if (dt_datatable.Rows.Count != 0)
                //    {

                //        foreach (DataRow dt in dt_datatable.Rows)
                //        {
                //            string msget_Gid = objcmnfunctions.GetMasterGID("LGCA");

                //            msSQL = "insert into lgl_trn_tcontactdtlRM (" +
                //              " contactdtl_gid ," +
                //              " legalSR_gid ," +
                //              " contact_name ," +
                //              " contact_location ," +
                //              " contact_mobileno ," +
                //              " created_by ," +
                //              " created_date )" +
                //              " values(" +
                //              "'" + msget_Gid + "'," +
                //               "'" + msgetGID + "'," +
                //               "'" + dt["contact_name"].ToString() + "'," +
                //               "'" + dt["contact_location"].ToString() + "'," +
                //               "'" + dt["contact_mobileno"].ToString() + "'," +
                //               "'" + employee_gid + "'," +
                //                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                //        }
                //        dt_datatable.Dispose();
                //    }

                //}

                msSQL += " where templegalsr_gid='" + values.templegalsr_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update lgl_trn_traiselegalSR set auth_status='ReSubmitted' where templegalsr_gid='" + values.templegalsr_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);       
           }
            objODBCDatareader.Close();

            //msgetGID = objcmnfunctions.GetMasterGID("RLSR");
            msSQL = "select raiselegalSR_gid from lgl_trn_traiselegalSR where templegalsr_gid='" + values.templegalsr_gid + "'";
            string lsraiselegalsr_gid = objdbconn.GetExecuteScalar(msSQL);


            msSQL = "select * from lgl_tmp_tcontactdtlRM where created_by ='" + employee_gid + "' and customer_gid='" + values.customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string msget_Gid = objcmnfunctions.GetMasterGID("LGCA");
                    msSQL = "insert into lgl_trn_tcontactdtlRM (" +
                       " contactdtl_gid ," +
                       " legalSR_gid ," +
                       " contact_name ," +
                       " contact_location ," +
                       " contact_mobileno ," +
                       " created_by ," +
                       " created_date )" +
                       " values(" +
                       "'" + msget_Gid + "'," +
                        "'" + lsraiselegalsr_gid + "'," +
                        "'" + dt["contact_name"].ToString() + "'," +
                        "'" + dt["contact_location"].ToString() + "'," +
                        "'" + dt["contact_mobileno"].ToString() + "'," +
                        "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
                dt_datatable.Dispose();
            }
            else
            {
                msSQL = "select * from lgl_trn_tcontactdtlRM where legalSR_gid='" + values.templegalsr_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {

                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        string msget_Gid = objcmnfunctions.GetMasterGID("LGCA");

                        msSQL = "insert into lgl_trn_tcontactdtlRM (" +
                          " contactdtl_gid ," +
                          " legalSR_gid ," +
                          " contact_name ," +
                          " contact_location ," +
                          " contact_mobileno ," +
                          " created_by ," +
                          " created_date )" +
                          " values(" +
                          "'" + msget_Gid + "'," +
                            "'" + msgetGID + "'," +
                           "'" + dt["contact_name"].ToString() + "'," +
                           "'" + dt["contact_location"].ToString() + "'," +
                           "'" + dt["contact_mobileno"].ToString() + "'," +
                           "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                    
                }
                dt_datatable.Dispose();

            }
            //msSQL = "update lgl_trn_traiselegalSR set legalSR_gid='" + msgetGID + "' where templegalsr_gid='" + values.templegalsr_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select a.customer_name,a.security_type,a.security_description,a.account_status,b.loanref_no, " +
                      " b.loan_title,b.sanctionref_no,b.sanction_date from ocs_trn_tcustomercollateral a " +
                      " left join ocs_trn_tcollateral2loan b on a.collateral_gid = b.collateral_gid where customer_gid = '" + values.customer_gid + "'" +
                      " group by a.collateral_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    string msget_Gid = objcmnfunctions.GetMasterGID("LGCL");
                    msSQL = "insert into lgl_trn_tlegalsr2collateral(" +
                        " legalsr2collateral_gid," +
                        " legalsr_gid ," +
                        " customer_gid ," +
                        " security_type," +
                        " security_description ," +
                        " account_status ," +
                        " created_by," +
                        " created_date)" +
                        " values (" +
                        "'" + msget_Gid + "'," +
                        "'" + msgetGID + "'," +
                        "'" + values.customer_gid + "'," +
                        "'" + dr_datarow["security_type"].ToString() + "'," +
                        "'" + dr_datarow["security_description"].ToString() + "'," +
                         "'" + dr_datarow["account_status"].ToString() + "'," +
                         "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                msSQL = "update lgl_tmp_traiselegalSR set auth_status='Pending' where templegalsr_gid='" + values.templegalsr_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Legal SR updated successfully";

                try
                {

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();

                    }
                    objODBCDatareader.Close();

                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();

                    msSQL = "select tomailid from ocs_trn_ttomaillist where mailtrigger_function='Legal SR Raised'";

                    dt_table = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dr_datarow in dt_table.Rows)
                    {
                        tomail_id = "";
                        tomail_id += dr_datarow["tomailid"].ToString();
                        message.To.Add(new MailAddress(tomail_id));
                        tomailid_list += "" + tomail_id + ",";
                    }
                    tomailid_list = tomailid_list.TrimEnd(',');
                    dt_table.Dispose();

                    msSQL = "select employee_mailid from ocs_trn_tmailcclist where mailtrigger_function='Legal SR Raised'";

                    dt_table = objdbconn.GetDataTable(msSQL);

                    foreach (DataRow dr_datarow in dt_table.Rows)
                    {
                        ccmail_id = "";
                        ccmail_id += dr_datarow["employee_mailid"].ToString();
                        message.CC.Add(new MailAddress(ccmail_id));
                        ccmailid_list += "" + ccmail_id + ",";
                    }
                    ccmailid_list = ccmailid_list.TrimEnd(',');
                    dt_table.Dispose();


                    msSQL = " select srref_no, d.customername as customer_name, d.customer_urn as customer_urn," +
                          " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as raised_by," +
                          " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raised_date " +
                          " from lgl_trn_traiselegalSR a " +
                          " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                          " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                          " left join ocs_mst_tcustomer d on a.customer_gid = d.customer_gid" +
                          " where raiselegalSR_gid ='" + msgetGID + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lssrref_no = objODBCDatareader["srref_no"].ToString();
                        lscustomer_name = objODBCDatareader["customer_name"].ToString();
                        lscustomer_urn = objODBCDatareader["customer_urn"].ToString();
                        lsraised_by = objODBCDatareader["raised_by"].ToString();
                        lsraised_date = objODBCDatareader["raised_date"].ToString();
                    }
                    objODBCDatareader.Close();

                    sub = " Legal SR ";


                    body = "Dear Sir/Madam,  <br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "<b>" + HttpUtility.HtmlEncode(lsraised_by) + "</b>" + " has raised Legal SR in Legal. Kindly do the needful.<br />";
                    body = body + "<br />";
                    body = body + "<b>Ref No :</b> " + HttpUtility.HtmlEncode(lssrref_no) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Customer Name/URN: :</b> " + HttpUtility.HtmlEncode(lscustomer_name) + "/" + HttpUtility.HtmlEncode(lscustomer_urn) + "<br />";
                    body = body + "<br />";
                    body = body + "<b>Raised Date :</b> " + lsraised_date + "<br />";
                    body = body + "<br />";


                    body = body + "<b>Yours Sincerely,</b> ";
                    body = body + "<br />";
                    body = body + "Samunnati Financial Intermediation & Services Pvt Ltd.<br /> ";
                    body = body + "<br />";
                    body = body + "<b>Please Note:</b> " + "This is an auto generated e-mail that cannot receive replies. " + "<br />";
                    body = body + "<br />";


                    message.From = new MailAddress(ls_username);


                    message.Subject = sub;
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = body;
                    smtp.Port = ls_port;
                    smtp.Host = ls_server; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    values.status = true;

                    if (values.status == true)
                    {
                        msSQL = "Insert into lgl_trn_legalsrsentmail( " +
                        " raiselegalSR_gid," +
                        " from_mail," +
                        " to_mail," +
                        " cc_mail," +
                        " mail_status," +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msgetGID + "'," +
                        "'" + ls_username + "'," +
                        "'" + tomailid_list + "'," +
                        "'" + ccmailid_list + "'," +
                        "'Legal SR Raised'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }

                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                    values.status = false;
                }

                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while adding Legal SR";
                return false;
            }
        }

        public bool DaGetRaiseLegalSRUser(MdlRaiselegalSR values, string user_gid)
        {
            msSQL = " select a.customer_gid, a.raiselegalSR_gid,a.auth_status,a.approval_status," +
                    " a.srref_no,a.account_name,a.constitution,a.financed_by," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as raised_by," +
                    " d.customer_urn,d.customername,a.urn" +
                    " from lgl_trn_traiselegalSR a" +
                    " left join hrm_mst_temployee b on  a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
                    " left join ocs_mst_tcustomer d on a.customer_gid=d.customer_gid " +
                    " where a.created_by='" + user_gid + "' order by a.created_date desc , d.customername asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlagalSR = new List<RaiselegalSR_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlagalSR.Add(new RaiselegalSR_list
                    {
                        legalsr_gid = dr_datarow["raiselegalSR_gid"].ToString(),
                        srref_no = dr_datarow["srref_no"].ToString(),
                        customer_name = (dr_datarow["account_name"].ToString()),
                        constitution = (dr_datarow["constitution"].ToString()),
                        financed_by = (dr_datarow["financed_by"].ToString()),
                        raised_by = (dr_datarow["raised_by"].ToString()),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        auth_status = (dr_datarow["auth_status"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        customer_gid = (dr_datarow["customer_gid"].ToString())
                    });
                }
                values.RaiselegalSR_list = getlagalSR;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetSR(MdlRaiselegalSR values, string employee_gid)
        {
          
            msSQL = " select a.customer_gid, a.templegalsr_gid,d.email,d.mobileno,a.auth_status,a.approval_status," +
                    " a.srref_no,a.account_name," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as raised_by, " +
                    " d.customer_urn,d.customername " +
                    " from lgl_tmp_traiselegalSR a" +
                    " left join hrm_mst_temployee b on  a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
                    " left join ocs_mst_tcustomer d on a.customer_gid=d.customer_gid " +
                    " where a.created_by='" + employee_gid + "' order by a.created_date desc , d.customername asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlagalSR = new List<RaiselegalSR_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlagalSR.Add(new RaiselegalSR_list
                    {
                        templegalsr_gid = dr_datarow["templegalsr_gid"].ToString(),
                        srref_no = dr_datarow["srref_no"].ToString(),
                        customer_name = (dr_datarow["account_name"].ToString()),
                        raised_by = (dr_datarow["raised_by"].ToString()),
                        email_id = dr_datarow["email"].ToString(),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        customer_gid = (dr_datarow["customer_gid"].ToString()),
                        auth_status = (dr_datarow["auth_status"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        mobile_no = dr_datarow["mobileno"].ToString()
                    });
                }
                values.RaiselegalSR_list = getlagalSR;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetRaiseLegalSR(MdlRaiselegalSR values, string user_gid)
        {
            msSQL = " select a.customer_gid, a.raiselegalSR_gid,a.auth_status,a.approval_status," +
                    " a.srref_no,a.account_name,a.constitution,a.financed_by," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as raised_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raised_date, d.customer_urn,d.customername,a.urn," +
                    " e.department_name as raised_by_department" +
                    " from lgl_trn_traiselegalSR a" +
                    " left join hrm_mst_temployee b on  a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
                    " left join hrm_mst_tdepartment e on b.department_gid=e.department_gid" +
                    " left join ocs_mst_tcustomer d on a.customer_gid=d.customer_gid " +
                    " where 1=1 order by a.created_date desc , d.customername asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlagalSR = new List<RaiselegalSR_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlagalSR.Add(new RaiselegalSR_list
                    {
                        legalsr_gid = dr_datarow["raiselegalSR_gid"].ToString(),
                        srref_no = dr_datarow["srref_no"].ToString(),
                        customer_name = (dr_datarow["account_name"].ToString()),
                        constitution = (dr_datarow["constitution"].ToString()),
                        financed_by = (dr_datarow["financed_by"].ToString()),
                        raised_by = (dr_datarow["raised_by"].ToString()),
                        raised_date = (dr_datarow["raised_date"].ToString()),
                        raised_by_department = (dr_datarow["raised_by_department"].ToString()),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        auth_status = (dr_datarow["auth_status"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        customer_gid = (dr_datarow["customer_gid"].ToString())
                    });
                }
                values.RaiselegalSR_list = getlagalSR;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetFacility(string employee_gid, facility values)
        {
            msgetGID = objcmnfunctions.GetMasterGID("RFCT");
            msSQL = "insert into lgl_trn_tcustomer2facility (" +
                " facility_gid ," +
                " customer_gid ," +
                " facility_type ," +
                " limit_amount ," +
                " outstanding ," +
                " created_by ," +
                " created_date )" +
                " values(" +
                "'" + msgetGID + "'," +
                 "'" + values.customer_gid + "'," +
                 "'" + values.facility_type + "'," +
                 "'" + values.limit + "'," +
                 "'" + values.outstanding + "'," +
                 "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Loan Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while adding loan details";
                return false;
            }
        }

        public bool DaGetFacilityDtl(string customer_gid, facility values)
        {
            msSQL = "select  facility_gid,facility_type,limit_amount,outstanding from lgl_trn_tcustomer2facility" +
                " where customer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getfacility_list = new List<facility_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getfacility_list.Add(new facility_list
                    {
                        facility_gid = dr_datarow["facility_gid"].ToString(),
                        facility_type = dr_datarow["facility_type"].ToString(),
                        limit = (dr_datarow["limit_amount"].ToString()),
                        outstanding = (dr_datarow["outstanding"].ToString())
                    });
                }
                values.facility_list = getfacility_list;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetDeleteFacility(string facility_gid, facility values)
        {
            msSQL = "delete from lgl_trn_tcustomer2facility" +
                " where facility_gid='" + facility_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            values.status = true;
            return true;
        }

        public bool DaGetViewLegalSR(string raiselegalSR_gid, MdlRaiselegalSR values)
        {
            msSQL = "select raiselegalSR_gid,account_name,constitution,financed_by,deal_year,address,business_activity,email_id,primary_securities,collateral_securities," +
                   " details_UDC_PDC,unit_working_status,other_banker_exposures,cibil_data,restructuring_data,status_current_overdue,other_group_companies," +
                   " meeting_details,srref_no,cycles_sanctiondated,limit_lastsanctioned,limit_sanction,churing_account,date_format(created_date,'%d-%m-%Y') as created_date," +
                   " statuslegal_action,instances_PTP,demandnotice_details,other_banker_borrower from lgl_trn_traiselegalSR"+
                " where raiselegalSR_gid='" + raiselegalSR_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Read();
                values.legalsr_gid = objODBCDatareader["raiselegalSR_gid"].ToString();
                values.account_name = objODBCDatareader["account_name"].ToString();
                values.email_id = objODBCDatareader["email_id"].ToString();
                values.address = objODBCDatareader["address"].ToString();
                values.constitution = objODBCDatareader["constitution"].ToString();
                values.financed_by = objODBCDatareader["financed_by"].ToString();
                values.deal_year = objODBCDatareader["deal_year"].ToString();
                values.cycles_sanctiondated = objODBCDatareader["cycles_sanctiondated"].ToString();
                values.cibil_data = objODBCDatareader["cibil_data"].ToString();
                values.business_activity = objODBCDatareader["business_activity"].ToString();
                values.limit_sanction = objODBCDatareader["limit_sanction"].ToString();
                values.primary_securities = objODBCDatareader["primary_securities"].ToString();
                values.collateral_securities = objODBCDatareader["collateral_securities"].ToString();
                values.details_UDC_PDC = objODBCDatareader["details_UDC_PDC"].ToString();
                values.unit_working_status = objODBCDatareader["unit_working_status"].ToString();
                values.other_banker_exposures = objODBCDatareader["other_banker_exposures"].ToString();
                values.status_current_overdue = objODBCDatareader["status_current_overdue"].ToString();
                values.churing_account = objODBCDatareader["churing_account"].ToString();
                values.other_group_companies = objODBCDatareader["other_group_companies"].ToString();
                values.meeting_details = objODBCDatareader["meeting_details"].ToString();
                values.restructuring_data = objODBCDatareader["restructuring_data"].ToString();
                values.other_banker_borrower = objODBCDatareader["other_banker_borrower"].ToString();
                values.instances_PTP = objODBCDatareader["instances_PTP"].ToString();
                values.statuslegal_action = objODBCDatareader["statuslegal_action"].ToString();
                values.demandnotice_details = objODBCDatareader["demandnotice_details"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
            return true;
        }


        public bool DaPostLegalSRApproval(approvalstatus values, string employee_gid, string user_gid)
        {
            msSQL = " UPDATE lgl_trn_traiselegalSR SET" +
                    " auth_status='Approved'," +
                    " approval_status='Pending',"+
                    " auth_by='" + user_gid + "'," +
                    " auth_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " auth_remarks='" + values.approval_remarks + "'" +
                    " WHERE raiselegalSR_gid='" + values.legalsr_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL= "select templegalsr_gid from lgl_trn_traiselegalSR where raiselegalSR_gid='" + values.legalsr_gid + "'";
            string lstemplegalsr_gid = objdbconn.GetExecuteScalar(msSQL);

           
            msSQL = "update lgl_tmp_traiselegalsr set auth_status='Approved' where templegalsr_gid='" + lstemplegalsr_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "update lgl_tmp_traiselegalsr set approval_status='Pending' where templegalsr_gid='" + lstemplegalsr_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msGetGIDREF = objcmnfunctions.GetMasterGID("LGLH");
            msSQL = " INSERT INTO lgl_trn_tlegalsrapprovalhistory(" +
                    " apprpvalhistory_gid," +
                    " legalsr_gid," +
                    " status," +
                    " remarks," +
                    " created_by," +
                    "created_date)" +
                    " VALUES(" +
                    "'" + msGetGIDREF + "'," +
                    "'" + values.legalsr_gid + "'," +
                    "'Approved'," +
                    "'" + values.approval_remarks + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



            msGetGIDREF = objcmnfunctions.GetMasterGID("LSAL");
            msSQL = " INSERT INTO lgl_trn_tlegalsrauthenticationlog(" +
                    " authenticationlog_gid," +
                    " legalsr_gid," +
                    " auth_status," +
                    " auth_remarks," +
                    " created_by," +
                    "created_date)" +
                    " VALUES(" +
                    "'" + msGetGIDREF + "'," +
                    "'" + values.legalsr_gid + "'," +
                    "'Approved'," +
                    "'" + values.approval_remarks + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            objcmnfunctions.PopSummaryLGL(employee_gid);
            objTblRQ = objcmnfunctions.foundRow(table);
            lscount = objcmnfunctions.foundcount(lscount);
            if (lscount > 0)
            {
                foreach (DataRow objRow1 in objTblRQ.Rows)
                {
                    employee = objRow1["employee_gid"].ToString();
                    msgetGID = objcmnfunctions.GetMasterGID("EXAP");

                    msSQL = " INSERT INTO lgl_trn_tapproval ( " +
                            " approval_gid, " +
                            " approved_by, " +
                            " approved_date, " +
                            " submodule_gid, " +
                            " legalsr_gid ," +
                            " created_by," +
                            " created_date" +
                            " ) VALUES ( " +
                            "'" + msgetGID + "'," +
                            " '" + employee + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'LGLLCMSRP'," +
                            "'" + values.legalsr_gid + "'," +
                            "'" + user_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);                    
                }
                msSQL = "SELECT approval_flag FROM lgl_trn_tapproval WHERE submodule_gid='LGLLCMSRP' AND legalsr_gid='" + values.legalsr_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {                                        
                    msSQL = "SELECT approved_by FROM lgl_trn_tapproval WHERE submodule_gid='LGLLCMSRP' AND legalsr_gid='" + values.legalsr_gid + "' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.RecordsAffected == 1)
                    {
                        if (objODBCDatareader["approved_by"].ToString() == employee_gid)
                        {                                                       
                            msSQL = " UPDATE lgl_trn_tapproval SET " +
                                    " approval_flag = 'Y', " +
                                    " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                    " updated_by='" + user_gid + "'," +
                                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                    " WHERE approved_by = '" + employee_gid + "'" +
                                    " AND legalsr_gid = '" + values.legalsr_gid + "' AND submodule_gid='LGLLCMSRP'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            
                            msSQL = " UPDATE lgl_trn_traiselegalSR SET" +
                                    " approval_status='Approved'" +
                                    " WHERE raiselegalSR_gid='" + values.legalsr_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);                           
                        }

                        objODBCDatareader.Close();
                    }
                    else if (objODBCDatareader.RecordsAffected > 1)
                    {                                              
                        msSQL = " SELECT approved_by from lgl_trn_tapproval" +
                               " WHERE approved_by='" + employee_gid + "' AND legalsr_gid = '" + values.legalsr_gid + "' AND submodule_gid='LGLLCMSRP'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows)
                        {                                                      
                            msSQL = " UPDATE lgl_trn_tapproval SET " +
                              " approval_flag = 'Y', " +
                              " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                              " updated_by='" + user_gid + "'," +
                              " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                              " WHERE approved_by = '" + employee_gid + "'" +
                              " AND legalsr_gid = '" + values.legalsr_gid + "' AND submodule_gid='LGLLCMSRP'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);                            
                        }
                        else
                        {                                                        
                        }
                        objODBCDatareader.Close();
                    }
                    objODBCDatareader.Close();
                }
                else
                {
                    msSQL = " UPDATE lgl_trn_traiselegalSR SET" +
                            " approval_status='Approved'" +
                           " WHERE raiselegalSR_gid='" + values.legalsr_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    objODBCDatareader.Close();
                }
                
            }
            if (mnResult != 0)
            {
                values.status = true;

                try
                {

                    msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Read();
                        ls_server = objODBCDatareader["pop_server"].ToString();
                        ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                        ls_username = objODBCDatareader["pop_username"].ToString();
                        ls_password = objODBCDatareader["pop_password"].ToString();

                    }
                    objODBCDatareader.Close();

                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();

                    msSQL = "select b.employee_emailid from lgl_trn_tapproval a left join hrm_mst_temployee b on a.approved_by = b.employee_gid where a.legalsr_gid = '"+ values.legalsr_gid + "' and a.submodule_gid = 'LGLLCMSRP'";

                    dt_table = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dr_datarow in dt_table.Rows)
                    {
                        tomail_id = "";
                        tomail_id += dr_datarow["employee_emailid"].ToString();
                        message.To.Add(new MailAddress(tomail_id));
                        tomailid_list += "" + tomail_id + ",";
                    }
                    tomailid_list = tomailid_list.TrimEnd(',');
                    dt_table.Dispose();

                    msSQL = "select employee_mailid from ocs_trn_tmailcclist where mailtrigger_function='Legal SR - Authentication'";

                    dt_table = objdbconn.GetDataTable(msSQL);
                    foreach (DataRow dr_datarow in dt_table.Rows)
                    {
                        ccmail_id = "";
                        ccmail_id += dr_datarow["employee_mailid"].ToString();
                        message.CC.Add(new MailAddress(ccmail_id));
                        ccmailid_list += "" + ccmail_id + ",";
                    }
                    ccmailid_list = ccmailid_list.TrimEnd(',');
                    dt_table.Dispose();



                    msSQL = " select d.customername as customer_name, d.customer_urn as customer_urn," +
                          " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as raised_by," +
                          " concat(f.user_firstname, ' ', f.user_lastname, '/', f.user_code) as auth_by" +
                          " from lgl_trn_traiselegalSR a " +
                          " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                          " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                          " left join hrm_mst_temployee e on a.auth_by = e.employee_gid" +
                          " left join adm_mst_tuser f on e.user_gid = f.user_gid" +
                          " left join ocs_mst_tcustomer d on a.customer_gid = d.customer_gid" +
                          " where raiselegalSR_gid ='" + values.legalsr_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscustomer_name = objODBCDatareader["customer_name"].ToString();
                        lscustomer_urn = objODBCDatareader["customer_urn"].ToString();
                        lsraised_by = objODBCDatareader["raised_by"].ToString();
                        lsauth_by = objODBCDatareader["auth_by"].ToString();
                    }

                   

                    objODBCDatareader.Close();

                    sub = " Legal SR Authenticated";


                    body = "Dear Sir/Madam,  <br />";
                    body = body + "<br />";
                    body = body + "Greetings,  <br />";
                    body = body + "<br />";
                    body = body + "The raised Legal SR has been authenticated and is waiting for your ODC approval. Kindly find the details below," + "<br />";
                    body = body + "<br />";
                    body = body + "Legal SR: " + "<b>" + HttpUtility.HtmlEncode(lscustomer_name) + "/" + HttpUtility.HtmlEncode(lscustomer_urn) + "</b>" + "<br />";
                    body = body + "<br />";
                    body = body + "Raised By: <b>" + HttpUtility.HtmlEncode(lsraised_by) + "</b>" + "<br />";
                    body = body + "<br />";
                    body = body + "Authenticated By: <b>" + HttpUtility.HtmlEncode(lsauth_by) + "</b>" + "<br />";
                    body = body + "<br />";
                    body = body + "Kindly login to the Samunnati Application for more details.";
                    body = body + "<br />";

                    body = body + "<b>Regards,</b> ";
                    body = body + "<br />";
                    body = body + "Samunnati Financial Intermediation & Services Pvt Ltd.<br /> ";
                    body = body + "<br />";
                    body = body + "<b>Please Note:</b> " + "This is an auto generated e-mail that cannot receive replies. " + "<br />";
                    body = body + "<br />";


                    message.From = new MailAddress(ls_username);


                    message.Subject = sub;
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = body;
                    smtp.Port = ls_port;
                    smtp.Host = ls_server; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    values.status = true;

                    if (values.status == true)
                    {
                        msSQL = "Insert into lgl_trn_legalsrsentmail( " +
                        " raiselegalSR_gid," +
                        " from_mail," +
                        " to_mail," +
                        " cc_mail," +
                        " mail_status," +
                        " mail_senddate, " +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + values.legalsr_gid + "'," +
                        "'" + ls_username + "'," +
                        "'" + tomailid_list + "'," +
                        "'" + ccmailid_list + "'," +
                        "'Legal SR Authenticated'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }

                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                    values.status = false;
                }

                return true;
            }
            else
            {
                values.status = false;
                return false;
            }
        }

        //public bool DaPostlegalSRReApproval(approvalstatus values, string employee_gid, string user_gid)
        //{
        //    msSQL = " UPDATE lgl_trn_traiselegalSR SET" +
        //            " auth_status='Approved'," +
        //            " approval_status='Pending'," +
        //            " auth_by='" + employee_gid + "'," +
        //            " auth_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
        //            " auth_remarks='" + values.approval_remarks + "'" +
        //            " WHERE raiselegalSR_gid='" + values.legalsr_gid + "'";
        //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //    msSQL = "select templegalsr_gid from lgl_trn_traiselegalSR where raiselegalSR_gid='" + values.legalsr_gid + "'";
        //    string lstemplegalsr_gid = objdbconn.GetExecuteScalar(msSQL);


        //    msSQL = "update lgl_tmp_traiselegalsr set auth_status='Approved' where templegalsr_gid='" + lstemplegalsr_gid + "'";
        //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //    msGetGIDREF = objcmnfunctions.GetMasterGID("LGLH");
        //    msSQL = " INSERT INTO lgl_trn_tlegalsrapprovalhistory(" +
        //            " apprpvalhistory_gid," +
        //            " legalsr_gid," +
        //            " status," +
        //            " remarks," +
        //            " created_by," +
        //            "created_date)" +
        //            " VALUES(" +
        //            "'" + msGetGIDREF + "'," +
        //            "'" + values.legalsr_gid + "'," +
        //            "'Approved'," +
        //            "'" + values.approval_remarks + "'," +
        //            "'" + user_gid + "'," +
        //            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //    objcmnfunctions.PopSummaryLGL(employee_gid);
        //    objTblRQ = objcmnfunctions.foundRow(table);
        //    lscount = objcmnfunctions.foundcount(lscount);
        //    if (lscount > 0)
        //    {
        //        foreach (DataRow objRow1 in objTblRQ.Rows)
        //        {
        //            employee = objRow1["employee_gid"].ToString();
        //            msgetGID = objcmnfunctions.GetMasterGID("EXAP");

        //            msSQL = " INSERT INTO lgl_trn_tapproval ( " +
        //                    " approval_gid, " +
        //                    " approved_by, " +
        //                    " approved_date, " +
        //                    " submodule_gid, " +
        //                    " legalsr_gid ," +
        //                    " created_by," +
        //                    " created_date" +
        //                    " ) VALUES ( " +
        //                    "'" + msgetGID + "'," +
        //                    " '" + employee + "'," +
        //                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
        //                    "'LGLLCMSRP'," +
        //                    "'" + values.legalsr_gid + "'," +
        //                    "'" + user_gid + "'," +
        //                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

        //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //        }
        //        msSQL = "SELECT approval_flag FROM lgl_trn_tapproval WHERE submodule_gid='LGLLCMSRP' AND legalsr_gid='" + values.legalsr_gid + "' ";
        //        objODBCDatareader = objdbconn.GetDataReader(msSQL);
        //        if (objODBCDatareader.HasRows)
        //        {
        //            msSQL = "SELECT approved_by FROM lgl_trn_tapproval WHERE submodule_gid='LGLLCMSRP' AND legalsr_gid='" + values.legalsr_gid + "' ";
        //            objODBCDatareader = objdbconn.GetDataReader(msSQL);
        //            if (objODBCDatareader.RecordsAffected == 1)
        //            {
        //                if (objODBCDatareader["approved_by"].ToString() == employee_gid)
        //                {
        //                    msSQL = " UPDATE lgl_trn_tapproval SET " +
        //                            " approval_flag = 'Y', " +
        //                            " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
        //                            " updated_by='" + user_gid + "'," +
        //                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
        //                            " WHERE approved_by = '" + employee_gid + "'" +
        //                            " AND legalsr_gid = '" + values.legalsr_gid + "' AND submodule_gid='LGLLCMSRP'";
        //                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //                    msSQL = " UPDATE lgl_trn_traiselegalSR SET" +
        //                            " approval_status='Approved'" +
        //                            " WHERE raiselegalSR_gid='" + values.legalsr_gid + "'";
        //                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //                }
        //            }
        //            else if (objODBCDatareader.RecordsAffected > 1)
        //            {
        //                msSQL = " SELECT approved_by from lgl_trn_tapproval" +
        //                       " WHERE approved_by='" + employee_gid + "' AND legalsr_gid = '" + values.legalsr_gid + "' AND submodule_gid='LGLLCMSRP'";
        //                objODBCDatareader = objdbconn.GetDataReader(msSQL);
        //                if (objODBCDatareader.HasRows)
        //                {
        //                    msSQL = " UPDATE lgl_trn_tapproval SET " +
        //                      " approval_flag = 'Y', " +
        //                      " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
        //                      " updated_by='" + user_gid + "'," +
        //                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
        //                      " WHERE approved_by = '" + employee_gid + "'" +
        //                      " AND legalsr_gid = '" + values.legalsr_gid + "' AND submodule_gid='LGLLCMSRP'";
        //                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //                }
        //                else
        //                {
        //                }
        //            }
        //        }
        //        else
        //        {
        //            msSQL = " UPDATE lgl_trn_traiselegalSR SET" +
        //                    " approval_status='Approved'" +
        //                   " WHERE raiselegalSR_gid='" + values.legalsr_gid + "'";
        //            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        //        }
        //        objODBCDatareader.Close();
        //    }
        //    if (mnResult != 0)
        //    {
        //        values.status = true;

        //        try
        //        {

        //            msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
        //            objODBCDatareader = objdbconn.GetDataReader(msSQL);
        //            if (objODBCDatareader.HasRows == true)
        //            {
        //                objODBCDatareader.Read();
        //                ls_server = objODBCDatareader["pop_server"].ToString();
        //                ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
        //                ls_username = objODBCDatareader["pop_username"].ToString();
        //                ls_password = objODBCDatareader["pop_password"].ToString();

        //            }
        //            objODBCDatareader.Close();

        //            MailMessage message = new MailMessage();
        //            SmtpClient smtp = new SmtpClient();

        //            msSQL = "select b.employee_emailid from lgl_trn_tapproval a left join hrm_mst_temployee b on a.approved_by = b.employee_gid where a.legalsr_gid = '" + values.legalsr_gid + "' and a.submodule_gid = 'LGLLCMSRP'";

        //            dt_table = objdbconn.GetDataTable(msSQL);
        //            foreach (DataRow dr_datarow in dt_table.Rows)
        //            {
        //                tomail_id = "";
        //                tomail_id += dr_datarow["employee_emailid"].ToString();
        //                message.To.Add(new MailAddress(tomail_id));
        //                tomailid_list += "" + tomail_id + ",";
        //            }
        //            tomailid_list = tomailid_list.TrimEnd(',');

        //            msSQL = "select employee_mailid from ocs_trn_tmailcclist where mailtrigger_function='Legal SR - Authentication'";

        //            dt_table = objdbconn.GetDataTable(msSQL);
        //            foreach (DataRow dr_datarow in dt_table.Rows)
        //            {
        //                ccmail_id = "";
        //                ccmail_id += dr_datarow["employee_mailid"].ToString();
        //                message.CC.Add(new MailAddress(ccmail_id));
        //                ccmailid_list += "" + ccmail_id + ",";
        //            }
        //            ccmailid_list = ccmailid_list.TrimEnd(',');



        //            msSQL = " select d.customername as customer_name, d.customer_urn as customer_urn," +
        //                  " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as raised_by," +
        //                  " concat(f.user_firstname, ' ', f.user_lastname, '/', f.user_code) as auth_by" +
        //                  " from lgl_trn_traiselegalSR a " +
        //                  " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
        //                  " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
        //                  " left join hrm_mst_temployee e on a.auth_by = e.employee_gid" +
        //                  " left join adm_mst_tuser f on e.user_gid = f.user_gid" +
        //                  " left join ocs_mst_tcustomer d on a.customer_gid = d.customer_gid" +
        //                  " where raiselegalSR_gid ='" + values.legalsr_gid + "'";
        //            objODBCDatareader = objdbconn.GetDataReader(msSQL);
        //            if (objODBCDatareader.HasRows == true)
        //            {
        //                lscustomer_name = objODBCDatareader["customer_name"].ToString();
        //                lscustomer_urn = objODBCDatareader["customer_urn"].ToString();
        //                lsraised_by = objODBCDatareader["raised_by"].ToString();
        //                lsauth_by = objODBCDatareader["auth_by"].ToString();
        //            }



        //            objODBCDatareader.Close();

        //            sub = " Legal SR Authenticated";


        //            body = "Dear Sir/Madam,  <br />";
        //            body = body + "<br />";
        //            body = body + "Greetings,  <br />";
        //            body = body + "<br />";
        //            body = body + "The raised Legal SR has been authenticated and is waiting for your ODC approval. Kindly find the details below," + "<br />";
        //            body = body + "<br />";
        //            body = body + "Legal SR: " + "<b>" + lscustomer_name + "/" + lscustomer_urn + "</b>" + "<br />";
        //            body = body + "<br />";
        //            body = body + "Raised By: <b>" + lsraised_by + "</b>" + "<br />";
        //            body = body + "<br />";
        //            body = body + "Authenticated By: <b>" + lsauth_by + "</b>" + "<br />";
        //            body = body + "<br />";
        //            body = body + "Kindly login to the Samunnati Application for more details.";
        //            body = body + "<br />";

        //            body = body + "<b>Regards,</b> ";
        //            body = body + "<br />";
        //            body = body + "Samunnati Financial Intermediation & Services Pvt Ltd.<br /> ";
        //            body = body + "<br />";
        //            body = body + "<b>Please Note:</b> " + "This is an auto generated e-mail that cannot receive replies. " + "<br />";
        //            body = body + "<br />";


        //            message.From = new MailAddress(ls_username);


        //            message.Subject = sub;
        //            message.IsBodyHtml = true; //to make message body as html  
        //            message.Body = body;
        //            smtp.Port = ls_port;
        //            smtp.Host = ls_server; //for gmail host  
        //            smtp.EnableSsl = true;
        //            smtp.UseDefaultCredentials = false;
        //            smtp.Credentials = new NetworkCredential(ls_username, ls_password);
        //            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //            smtp.Send(message);

        //            values.status = true;

        //            if (values.status == true)
        //            {
        //                msSQL = "Insert into lgl_trn_legalsrsentmail( " +
        //                " raiselegalSR_gid," +
        //                " from_mail," +
        //                " to_mail," +
        //                " cc_mail," +
        //                " mail_status," +
        //                " mail_senddate, " +
        //                " created_by," +
        //                " created_date)" +
        //                " values(" +
        //                "'" + values.legalsr_gid + "'," +
        //                "'" + ls_username + "'," +
        //                "'" + tomailid_list + "'," +
        //                "'" + ccmailid_list + "'," +
        //                "'Legal SR Authenticated'," +
        //                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
        //                "'" + employee_gid + "'," +
        //                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

        //                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            values.message = ex.ToString();
        //            values.status = false;
        //        }

        //        return true;
        //    }
        //    else
        //    {
        //        values.status = false;
        //        return false;
        //    }
        //}

        public bool DaPostLegalSRReject(approvalstatus values, string employee_gid, string user_gid)

        {
            msSQL = " UPDATE lgl_trn_traiselegalSR SET" +
                  " auth_status='Hold'," +
                  " auth_by='" + employee_gid + "'," +
                  " auth_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                  " auth_remarks='" + values.approval_remarks + "'" +
                  " WHERE raiselegalSR_gid='" + values.legalsr_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        
            msSQL = "select templegalsr_gid from lgl_trn_traiselegalSR where raiselegalSR_gid='" + values.legalsr_gid + "'";
            string lstemplegalsr_gid = objdbconn.GetExecuteScalar(msSQL);
           

            msSQL = "update lgl_tmp_traiselegalsr set auth_status='Hold' where templegalsr_gid='" + lstemplegalsr_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
           

            msGetGIDREF = objcmnfunctions.GetMasterGID("LGLH");
            msSQL = " INSERT INTO lgl_trn_tlegalsrapprovalhistory(" +
                    " apprpvalhistory_gid," +
                    " legalsr_gid," +
                    " status," +
                    " remarks," +
                    " created_by," +
                    "created_date)" +
                    " VALUES(" +
                    "'" + msGetGIDREF + "'," +
                    "'" + values.legalsr_gid + "'," +
                    "'Hold'," +
                    "'" + values.approval_remarks + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msGetGIDREF = objcmnfunctions.GetMasterGID("LSAL");
            msSQL = " INSERT INTO lgl_trn_tlegalsrauthenticationlog(" +
                    " authenticationlog_gid," +
                    " legalsr_gid," +
                    " auth_status," +
                    " auth_remarks," +
                    " created_by," +
                    "created_date)" +
                    " VALUES(" +
                    "'" + msGetGIDREF + "'," +
                    "'" + values.legalsr_gid + "'," +
                    "'Hold'," +
                    "'" + values.approval_remarks + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
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
        public bool DaPostLegalSRRejected(approvalstatus values, string employee_gid, string user_gid)

        {
            msSQL = " UPDATE lgl_trn_traiselegalSR SET" +
                  " auth_status='Rejected'," +
                  " auth_by='" + employee_gid + "'," +
                  " auth_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                  " auth_remarks='" + values.approval_remarks + "'" +
                  " WHERE raiselegalSR_gid='" + values.legalsr_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select templegalsr_gid from lgl_trn_traiselegalSR where raiselegalSR_gid='" + values.legalsr_gid + "'";
            string lstemplegalsr_gid = objdbconn.GetExecuteScalar(msSQL);


            msSQL = "update lgl_tmp_traiselegalsr set auth_status='Rejected' where templegalsr_gid='" + lstemplegalsr_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msGetGIDREF = objcmnfunctions.GetMasterGID("LGLH");
            msSQL = " INSERT INTO lgl_trn_tlegalsrapprovalhistory(" +
                    " apprpvalhistory_gid," +
                    " legalsr_gid," +
                    " status," +
                    " remarks," +
                    " created_by," +
                    "created_date)" +
                    " VALUES(" +
                    "'" + msGetGIDREF + "'," +
                    "'" + values.legalsr_gid + "'," +
                    "'Rejected'," +
                    "'" + values.approval_remarks + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msGetGIDREF = objcmnfunctions.GetMasterGID("LSAL");
            msSQL = " INSERT INTO lgl_trn_tlegalsrauthenticationlog(" +
                    " authenticationlog_gid," +
                    " legalsr_gid," +
                    " auth_status," +
                    " auth_remarks," +
                    " created_by," +
                    "created_date)" +
                    " VALUES(" +
                    "'" + msGetGIDREF + "'," +
                    "'" + values.legalsr_gid + "'," +
                    "'Rejected'," +
                    "'" + values.approval_remarks + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
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
        //public bool DaPostlegalSRrereject(approvalstatus values, string employee_gid, string user_gid)

        //{
        //    msSQL = " UPDATE lgl_trn_traiselegalSR SET" +
        //          " auth_status='Hold'," +
        //          " auth_by='" + employee_gid + "'," +
        //          " auth_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
        //          " auth_remarks='" + values.approval_remarks + "'" +
        //          " WHERE raiselegalSR_gid='" + values.legalsr_gid + "'";
        //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //    msSQL = "select templegalsr_gid from lgl_trn_traiselegalSR where raiselegalSR_gid='" + values.legalsr_gid + "'";
        //    string lstemplegalsr_gid = objdbconn.GetExecuteScalar(msSQL);


        //    msSQL = "update lgl_tmp_traiselegalsr set auth_status='Hold' where templegalsr_gid='" + lstemplegalsr_gid + "'";
        //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


        //    msGetGIDREF = objcmnfunctions.GetMasterGID("LGLH");
        //    msSQL = " INSERT INTO lgl_trn_tlegalsrapprovalhistory(" +
        //            " apprpvalhistory_gid," +
        //            " legalsr_gid," +
        //            " status," +
        //            " remarks," +
        //            " created_by," +
        //            "created_date)" +
        //            " VALUES(" +
        //            "'" + msGetGIDREF + "'," +
        //            "'" + values.legalsr_gid + "'," +
        //            "'Hold'," +
        //            "'" + values.approval_remarks + "'," +
        //            "'" + user_gid + "'," +
        //            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

        //    if (mnResult != 0)
        //    {
        //        values.status = true;
        //        return true;
        //    }
        //    else
        //    {
        //        values.status = false;
        //        return false;
        //    }
        //}


        public bool DaGetLegalSRmgmt(MdlRaiselegalSR values, string user_gid)
        {
            msSQL = " SELECT a.auth_status,a.approval_status,a.raiselegalSR_gid,a.srref_no,a.account_name,date_format(a.created_date,'%d-%m-%Y %h:%i %p')  as raised_date," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as raised_by, " +
                    " d.customer_urn,d.customername,a.customer_gid" +
                    " FROM lgl_trn_traiselegalSR a" +
                    " LEFT JOIN hrm_mst_temployee b on  a.created_by = b.employee_gid" +
                    " LEFT JOIN adm_mst_tuser c on a.auth_by=c.user_gid" +
                    " LEFT JOIN ocs_mst_tcustomer d on a.customer_gid=d.customer_gid " +
                    " WHERE auth_status='Approved' AND" +
                    " raiselegalSR_gid in (select legalsr_gid from lgl_trn_tapproval where approved_by='" + user_gid + "') order by a.created_date desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlagalSR = new List<RaiselegalSR_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlagalSR.Add(new RaiselegalSR_list
                    {
                        legalsr_gid = dr_datarow["raiselegalSR_gid"].ToString(),
                        srref_no = dr_datarow["srref_no"].ToString(),
                        customer_name = (dr_datarow["account_name"].ToString()),
                        raised_date = (dr_datarow["raised_date"].ToString()),                   
                        raised_by = (dr_datarow["raised_by"].ToString()),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        auth_status = (dr_datarow["auth_status"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        customer_gid=dr_datarow["customer_gid"].ToString()
                    });
                }
                values.RaiselegalSR_list = getlagalSR;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetLegalSRmgmtSummary(MdlRaiselegalSR values, string employee_gid)
        {
            msSQL = " SELECT a.auth_status,a.approval_status,a.raiselegalSR_gid,a.srref_no,a.account_name,a.constitution,a.financed_by," +
                    " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as raised_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raised_date, " +
                    " d.customer_urn,d.customername,a.customer_gid,e.department_name as raised_by_department " +
                    " FROM lgl_trn_traiselegalSR a" +
                    " LEFT JOIN hrm_mst_temployee b on  a.created_by = b.employee_gid" +
                    " LEFT JOIN adm_mst_tuser c on b.user_gid=c.user_gid" +
                    " LEFT JOIN ocs_mst_tcustomer d on a.customer_gid=d.customer_gid " +
                    " LEFT JOIN hrm_mst_tdepartment e on b.department_gid=e.department_gid" +
                    " WHERE a.auth_status='Approved' AND a.approval_status='Approved' order by a.created_date desc , d.customername asc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getlagalSR = new List<RaiselegalSR_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getlagalSR.Add(new RaiselegalSR_list
                    {
                        legalsr_gid = dr_datarow["raiselegalSR_gid"].ToString(),
                        srref_no = dr_datarow["srref_no"].ToString(),
                        customer_name = (dr_datarow["account_name"].ToString()),
                        raised_date = (dr_datarow["raised_date"].ToString()),
                        raised_by_department = (dr_datarow["raised_by_department"].ToString()),
                        raised_by = (dr_datarow["raised_by"].ToString()),
                        customer_urn = (dr_datarow["customer_urn"].ToString()),
                        auth_status = (dr_datarow["auth_status"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        customer_gid=dr_datarow["customer_gid"].ToString()
                    });
                }
                values.RaiselegalSR_list = getlagalSR;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaPostLegalSRApprovalMgmt(approvalstatus values, string employee_gid, string user_gid)
        {
            msSQL = " UPDATE lgl_trn_tapproval SET " +
                    " approval_flag = 'Y', " +
                    " approval_remarks='" + values.approval_remarks + "'," +
                    " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " updated_by='" + user_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " WHERE approved_by = '" + employee_gid + "'" +
                    " AND legalsr_gid = '" + values.legalsr_gid + "' AND submodule_gid='LGLLCMSRP'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL); 
            msSQL = " SELECT approval_flag " +
                    " FROM lgl_trn_tapproval " +
                    " WHERE legalsr_gid='" + values.legalsr_gid + "' AND approval_flag='N'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
            }
            else
            {
                msSQL = " SELECT approval_flag " +
                  " FROM lgl_trn_tapproval " +
                  " WHERE legalsr_gid='" + values.legalsr_gid + "' AND approval_flag='N/A'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    msSQL = " UPDATE lgl_trn_traiselegalSR SET" +
                 " approval_status='Rejected'," +
                 " approval_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " WHERE raiselegalSR_gid='" + values.legalsr_gid + "'";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = "select templegalsr_gid from lgl_trn_traiselegalSR where raiselegalSR_gid='" + values.legalsr_gid + "'";
                     lstemplegalsr_gid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "update lgl_tmp_traiselegalsr set approval_status='Rejected' where templegalsr_gid='" + lstemplegalsr_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " UPDATE lgl_trn_traiselegalSR SET" +
                   " approval_status='Approved'," +
                   " approval_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " WHERE raiselegalSR_gid='" + values.legalsr_gid + "'";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "select templegalsr_gid from lgl_trn_traiselegalSR where raiselegalSR_gid='" + values.legalsr_gid + "'";
                     lstemplegalsr_gid = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "update lgl_tmp_traiselegalsr set approval_status='Approved' where templegalsr_gid='" + lstemplegalsr_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                objODBCDatareader.Close();
            }
            
            values.status = true;

            try
            {

                msSQL = "SELECT company_mail,pop_server,pop_port,pop_username,pop_password FROM adm_mst_tcompany ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();
                    ls_server = objODBCDatareader["pop_server"].ToString();
                    ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                    ls_username = objODBCDatareader["pop_username"].ToString();
                    ls_password = objODBCDatareader["pop_password"].ToString();

                }
                objODBCDatareader.Close();

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                msSQL = "select tomailid from ocs_trn_ttomaillist where mailtrigger_function='Legal SR - ODC Approval'";

                dt_table = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dr_datarow in dt_table.Rows)
                {
                    tomail_id = "";
                    tomail_id += dr_datarow["tomailid"].ToString();

                    message.To.Add(new MailAddress(tomail_id));
                    tomailid_list += "" + tomail_id + ",";
                }
                tomailid_list = tomailid_list.TrimEnd(',');
                dt_table.Dispose();

                msSQL = "select employee_mailid from ocs_trn_tmailcclist where mailtrigger_function='Legal SR - ODC Approval'";

                dt_table = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dr_datarow in dt_table.Rows)
                {
                    ccmail_id = "";
                    ccmail_id += dr_datarow["employee_mailid"].ToString();
                    message.CC.Add(new MailAddress(ccmail_id));
                    ccmailid_list += "" + ccmail_id + ",";
                }
                ccmailid_list = ccmailid_list.TrimEnd(',');
                dt_table.Dispose();



                msSQL = " select d.srref_no, e.customername as customer_name, e.customer_urn as customer_urn," +
                      " concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as approved_by," +
                      " date_format(a.approved_date,'%d-%m-%Y %h:%i %p') as approved_date" +
                      " from lgl_trn_tapproval a" +
                      " left join hrm_mst_temployee b on a.approved_by = b.employee_gid" +
                      " left join adm_mst_tuser c on b.user_gid = c.user_gid" +
                      " left join lgl_trn_traiselegalSR d on a.legalsr_gid = d.raiselegalSR_gid" +
                      " left join ocs_mst_tcustomer e on d.customer_gid = e.customer_gid" +
                      " where legalsr_gid ='" + values.legalsr_gid + "' and approved_by = '" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lssrref_no = objODBCDatareader["srref_no"].ToString();
                    lscustomer_name = objODBCDatareader["customer_name"].ToString();
                    lscustomer_urn = objODBCDatareader["customer_urn"].ToString();
                    lsapproved_by = objODBCDatareader["approved_by"].ToString();
                    lsapproved_date = objODBCDatareader["approved_date"].ToString();
                }
                objODBCDatareader.Close();

                sub = " Legal SR Approved ";


                body = "Dear Sir/Madam,  <br />";
                body = body + "<br />";
                body = body + "Greetings,  <br />";
                body = body + "<br />";
                body = body + "The following Legal SR" + " has been ODC approved. <br />";
                body = body + "<br />";
                body = body + "Ref No : <b>" + HttpUtility.HtmlEncode(lssrref_no) + "</b><br />";
                body = body + "<br />";
                body = body + "Customer Name/URN: <b>" + HttpUtility.HtmlEncode(lscustomer_name) + "/" + HttpUtility.HtmlEncode(lscustomer_urn) + "</b><br />";
                body = body + "<br />";
                body = body + "Approved By: <b>" + HttpUtility.HtmlEncode(lsapproved_by) + "</b><br />";
                body = body + "<br />";
                body = body + "Approved Date: <b>" + lsapproved_date + "</b><br />";
                body = body + "<br />";


                body = body + "<b>Yours Sincerely,</b> ";
                body = body + "<br />";
                body = body + "Samunnati Financial Intermediation & Services Pvt Ltd.<br /> ";
                body = body + "<br />";
                body = body + "<b>Please Note:</b> " + "This is an auto generated e-mail that cannot receive replies. " + "<br />";
                body = body + "<br />";


                message.From = new MailAddress(ls_username);


                message.Subject = sub;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = ls_port;
                smtp.Host = ls_server; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                values.status = true;

                if (values.status == true)
                {
                    msSQL = "Insert into lgl_trn_legalsrsentmail( " +
                    " raiselegalSR_gid," +
                    " from_mail," +
                    " to_mail," +
                    " cc_mail," +
                    " mail_status," +
                    " mail_senddate, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msgetGID + "'," +
                    "'" + ls_username + "'," +
                    "'" + tomailid_list + "'," +
                    "'" + ccmailid_list + "'," +
                    "'Legal SR Approved'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

            return true;
        }

        public bool DaPostLegalSRRejectedMgmt(approvalstatus values, string employee_gid, string user_gid)
        {
            msSQL = " UPDATE lgl_trn_tapproval SET " +
                    " approval_flag = 'N/A', " +
                    " approval_remarks='" + values.approval_remarks + "'," +
                    " approved_date = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " updated_by='" + user_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " WHERE approved_by = '" + employee_gid + "'" +
                    " AND legalsr_gid = '" + values.legalsr_gid + "' AND submodule_gid='LGLLCMSRP'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select templegalsr_gid from lgl_trn_traiselegalSR where raiselegalSR_gid='" + values.legalsr_gid + "'";
            string lstemplegalsr_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "update lgl_tmp_traiselegalsr set approval_status='Rejected' where templegalsr_gid='" + lstemplegalsr_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " UPDATE lgl_trn_traiselegalSR SET" +
                     " approval_status='Rejected'," +
                     " approval_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " WHERE raiselegalSR_gid='" + values.legalsr_gid + "'";

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

        public bool DaGetlegarSRapprovals(approvalstatus values, string legalsr_gid)
        {
            msSQL = " SELECT  c.user_code,concat(c.user_firstname,' ' ,c.user_lastname,' / ',d.designation_name) as user," +
                    " case when approval_flag = 'Y' then 'Approved'" +
                    " when approval_flag = 'N' then 'Pending'" +
                    " when approval_flag = 'N/A' then 'Rejected'" +
                    " else '-' end as approval_status," +
                    " approval_remarks, a.updated_date" +
                   " FROM lgl_trn_tapproval a" +
                   " LEFT JOIN hrm_mst_temployee b ON a.approved_by = b.employee_gid" +
                   " LEFT  JOIN adm_mst_tuser c ON c.user_gid = b.user_gid" +
                  " left join adm_mst_tdesignation d on b.designation_gid = d.designation_gid" +
                  " where a.legalsr_gid = '" + legalsr_gid + "' order by c.user_code";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var approvalstatuslist = new List<approvallist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    approvalstatuslist.Add(new approvallist
                    {
                        user_code = (dr_datarow["user_code"].ToString()),
                        user_name = (dr_datarow["user"].ToString()),
                        approval_status = (dr_datarow["approval_status"].ToString()),
                        approval_remarks = (dr_datarow["approval_remarks"].ToString()),
                        approval_date = (dr_datarow["updated_date"].ToString())
                    });
                }
                values.approvallist = approvalstatuslist;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaPostContactDetails(string employee_gid, contactdetailsRM values, contactdetailsRM_list objvalues)
        {
            msSQL = "insert into lgl_tmp_tcontactdtlRM (" +
              " contact_name ," +
              " contact_location ," +
              " contact_mobileno ," +
              " customer_gid ,"+
              " created_by ," +
              " created_date )" +
              " values(" +
               "'" + values.contact_name + "'," +
               "'" + values.contact_location + "'," +
               "'" + values.contact_mobileno + "'," +
                "'" + values.customer_gid + "'," +
               "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select * from lgl_tmp_tcontactdtlRM where created_by='" + employee_gid + "' and customer_gid='"+ values.customer_gid  + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactdetailsRM_list = new List<contactdetailsRM>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactdetailsRM_list.Add(new contactdetailsRM
                    {
                        contact_name = (dr_datarow["contact_name"].ToString()),
                        contact_location = (dr_datarow["contact_location"].ToString()),
                        contact_mobileno = (dr_datarow["contact_mobileno"].ToString()),
                        tmpcontactdtl_gid = (dr_datarow["tmpcontactdtl_gid"].ToString()),
                    });
                }
                objvalues.contactdetailsRM = getcontactdetailsRM_list;
            }
            dt_datatable.Dispose();
            
            if (mnResult != 0)
            {
                objvalues.status = true;
                objvalues.message = "Details are Added Successfully";
                return true;
            }
            else
            {
                objvalues.status = false;
                objvalues.message = "Error Occured while adding";
                return false;
            }
        }

        public bool DaGetLegalSRContactDtl(string customer_gid,string employee_gid, contactdetailsRM_list objvalues)
        {
            msSQL = "select * from lgl_tmp_tcontactdtlRM where created_by='" + employee_gid + "' and customer_gid='"+ customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactdetailsRM_list = new List<contactdetailsRM>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactdetailsRM_list.Add(new contactdetailsRM
                    {
                        contact_name = (dr_datarow["contact_name"].ToString()),
                        contact_location = (dr_datarow["contact_location"].ToString()),
                        contact_mobileno = (dr_datarow["contact_mobileno"].ToString()),
                        tmpcontactdtl_gid = (dr_datarow["tmpcontactdtl_gid"].ToString()),
                    });
                }
                objvalues.contactdetailsRM = getcontactdetailsRM_list;
            }
            dt_datatable.Dispose();
            objvalues.status = true;
            return true;
        }

        public bool DaGetContactDetailsRM(string templegalsr_gid, contactdetailsRM_list values)
        {
            msSQL = "select raiselegalSR_gid from lgl_trn_traiselegalSR where templegalsr_gid='" + templegalsr_gid + "'";
            string lsraiselegalsr_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select * from lgl_trn_tcontactdtlRM where legalsr_gid ='" + lsraiselegalsr_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactdetailsRM_list = new List<contactdetailsRM>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactdetailsRM_list.Add(new contactdetailsRM
                    {
                        contact_name = (dr_datarow["contact_name"].ToString()),
                        contact_location = (dr_datarow["contact_location"].ToString()),
                        contact_mobileno = (dr_datarow["contact_mobileno"].ToString()),
                    });
                }
                values.contactdetailsRM = getcontactdetailsRM_list;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetcontactdtl(string raiselegalSR_gid, contactdetailsRM_list values)
        {
            
            msSQL = " select * from lgl_trn_tcontactdtlRM where legalsr_gid ='" + raiselegalSR_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactdetailsRM_list = new List<contactdetailsRM>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactdetailsRM_list.Add(new contactdetailsRM
                    {
                        contact_name = (dr_datarow["contact_name"].ToString()),
                        contact_location = (dr_datarow["contact_location"].ToString()),
                        contact_mobileno = (dr_datarow["contact_mobileno"].ToString()),
                    });
                }
                values.contactdetailsRM = getcontactdetailsRM_list;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }


        public bool GaGetDeleteContactDtl(string tmpcontactdtl_gid,excel values)
        {         
            msSQL = "delete from lgl_tmp_tcontactdtlRM where tmpcontactdtl_gid='" + tmpcontactdtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            
            if(mnResult!=0)
            {
                values.status = true;
                values.message = "Details Deleted Successfully..!";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
                return false;
            }
        }

        public bool DaGetSamgDetails(MdlRaiselegalSR values, string customer_gid, string legalsr_gid)
        {            
            msSQL = "select account_name,constitution,financed_by,deal_year,a.address,business_activity,email_id,primary_securities,collateral_securities," +
                   " details_UDC_PDC,unit_working_status,other_banker_exposures,cibil_data,restructuring_data,status_current_overdue,other_group_companies," +
                   " meeting_details,srref_no,cycles_sanctiondated,limit_lastsanctioned,limit_sanction,churing_account,date_format(a.created_date,'%d-%m-%Y') as created_date," +
                   " statuslegal_action,instances_PTP,demandnotice_details from lgl_trn_traiselegalSR a " +
                   " left join ocs_mst_tcustomer b on a.urn=b.customer_urn where a.raiselegalSR_gid='" + legalsr_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.customer_name = objODBCDatareader["account_name"].ToString();
                values.constitution = objODBCDatareader["constitution"].ToString();
                values.financed_by = objODBCDatareader["financed_by"].ToString();
                values.deal_year = objODBCDatareader["deal_year"].ToString();
                values.address = objODBCDatareader["address"].ToString();
                values.business_activity = objODBCDatareader["business_activity"].ToString();
                values.email_id = objODBCDatareader["email_id"].ToString();
                values.primary_securities = objODBCDatareader["primary_securities"].ToString();
                values.collateral_securities = objODBCDatareader["collateral_securities"].ToString();
                values.details_UDC_PDC = objODBCDatareader["details_UDC_PDC"].ToString();
                values.unit_working_status = objODBCDatareader["unit_working_status"].ToString();
                values.other_banker_exposures = objODBCDatareader["other_banker_exposures"].ToString();
                values.cibil_data = objODBCDatareader["cibil_data"].ToString();
                values.restructuring_data = objODBCDatareader["restructuring_data"].ToString();
                values.status_current_overdue = objODBCDatareader["status_current_overdue"].ToString();
                values.other_group_companies = objODBCDatareader["other_group_companies"].ToString();
                values.meeting_details = objODBCDatareader["meeting_details"].ToString();
                values.srref_no = objODBCDatareader["srref_no"].ToString();
                values.cycles_sanctiondated = objODBCDatareader["cycles_sanctiondated"].ToString();
                values.limit_sanction = objODBCDatareader["limit_sanction"].ToString();
                values.churing_account = objODBCDatareader["churing_account"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
                values.statuslegal_action = objODBCDatareader["statuslegal_action"].ToString();
                values.instances_PTP = objODBCDatareader["instances_PTP"].ToString();
                values.demandnotice_details = objODBCDatareader["demandnotice_details"].ToString();
                //values.auth_remarks = objODBCDatareader["auth_remarks"].ToString();

                
            }
            objODBCDatareader.Close();
            msSQL = " select a.auth_remarks,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.auth_status," +
                        "concat(c.user_firstname, ' ', c.user_lastname) as created_by  from lgl_trn_tlegalsrauthenticationlog a " +
                        "left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                        " where legalsr_gid='" + legalsr_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);

            var getLegalSRdtls = new List<auth_remarks_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getLegalSRdtls.Add(new auth_remarks_list
                    {
                        created_date = dr_datarow["created_date"].ToString(),
                        created_by = (dr_datarow["created_by"].ToString()),
                        auth_status = (dr_datarow["auth_status"].ToString()),
                        auth_remarks = (dr_datarow["auth_remarks"].ToString())

                    });
                }
                values.auth_remarks_list = getLegalSRdtls;
            }
            dt_datatable.Dispose();
            //values.status = true;
            //return true;
            //msSQL = "select concat(user_firstname, ' ',user_lastname) as created_by from adm_mst_tuser where user_gid='" + user_gid + "'";
            //    values.created_by = objdbconn.GetExecuteScalar(msSQL);
            values.status = true;
            return true;



        }

        public bool DaPostSLNUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid, string user_gid)
        {
            upload_list objdocumentmodel = new upload_list();
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
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "LGL/SLNDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
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
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);
                        lspath = path;
                        //objcmnfunctions.uploadFile(lspath, lsfile_gid);
                        //lspath = "../../erp_documents" + "/" + lscompany_code + "/" + "LGL/SLNDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
							objfilename.status = false;
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "LGL/SLNDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "LGL/SLNDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("SLND");
                        msSQL = " insert into lgl_tmp_tSLNdocument( " +
                                    " document_name ," +
                                    " document_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath  + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);                                              
                    }
                }
                msSQL = "select tmpSLN_documentgid,document_name,document_path,created_date " +
                        " from lgl_tmp_tSLNdocument where created_by='" + employee_gid + "'";
                
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<uploadseek_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new uploadseek_list
                        {

                            tmpSLN_documentgid = (dr_datarow["tmpSLN_documentgid"].ToString()),
                            document_name = dr_datarow["document_name"].ToString(),
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                            created_date = dr_datarow["created_date"].ToString(),
                        });
                    }
                    objfilename.uploadseek_list = get_filename;
                }
                dt_datatable.Dispose();               
            }
            catch
            {
            }
            if (mnResult !=0)
            {
                objfilename.status = true;
                objfilename.message = "Document upload successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured while uploading document";
                return false;
            }
        }

        public bool DaGetSLNuploadcancel(string tmpseek_documentgid, uploaddocument objfilename, string employee_gid)
        {            
            msSQL = "delete from lgl_tmp_tSLNdocument where tmpSLN_documentgid='" + tmpseek_documentgid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "select tmpSLN_documentgid,document_name,document_path,created_date " +
                       " from lgl_tmp_tSLNdocument where created_by='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_filename = new List<uploadseek_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_filename.Add(new uploadseek_list
                    {
                        tmpSLN_documentgid = (dr_datarow["tmpSLN_documentgid"].ToString()),
                        document_name = dr_datarow["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        created_date = dr_datarow["created_date"].ToString(),
                    });
                }
                objfilename.uploadseek_list = get_filename;
            }
            dt_datatable.Dispose();
            if (mnResult != 0)
            {
                objfilename.status = true;
                objfilename.message = "Document deleted successfully";
                return true;
            }
            else
            {
                objfilename.status = false;
                objfilename.message = "Error Occured while deleting document";
                return false;
            }
        }

        public bool DaGettmpSLNdocumentClear(string employee_gid)
        {          
            msSQL = "delete from lgl_tmp_tSLNdocument where created_by='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);           

            if (mnResult != 0)
            {              
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DaGetSLNdocumentDtl(assignSRLawyer values,string legalSR_gid)
        {             
            msSQL = " select a.SLNdocument_gid,a.document_name,a.document_path,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                 " from lgl_trn_tSLNdocument a " +
                 " left join hrm_mst_temployee b on a.created_by=b.employee_gid " +
                 " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                 " where a.legal_SRgid='" + legalSR_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_fileseekname = new List<uploadseek_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    get_fileseekname.Add(new uploadseek_list
                    {
                        SLNdocument_gid = (dr_datarow["SLNdocument_gid"].ToString()),
                        document_name = dr_datarow["document_name"].ToString(),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                        created_by = dr_datarow["created_by"].ToString(),
                    });
                }
                values.uploadseek_list = get_fileseekname;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool DaGetCustomer(string customer_gid,mdlcustomer values)
        {
            try
            {
                msSQL = " select a.customer_gid,a.vertical_code,a.customer_code,a.customername, " +
                 " case when a.address<>'' then a.address else '-' end as address,address2, " +
                 " case when a.mobileno<>'' then a.mobileno else '-' end as mobileno, " +
                 " case when a.contactperson<>'' then a.contactperson else '-' end as contactperson, " +
                 " case when a.creditmanager_gid='' then 'NA' else a.creditmgmt_name end as creditmgmt_name," +
                 " case when a.zonal_head = '' then 'NA' else a.zonal_name end as zonal_head," +
                 " case when a.business_head = '' then 'NA' else a.businesshead_name end as business_head, " +
                 " case when a.cluster_manager_gid = '' then 'NA' else a.cluster_manager_name end as cluster_manager, " +
                 " case when a.relationship_manager = '' then 'NA' else a.relationshipmgmt_name end as relationship_manager,a.customer_urn " +
                 " from ocs_mst_tcustomer a where customer_gid='" + customer_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.customerCodeedit = objODBCDatareader["customer_code"].ToString();
                    values.customerNameedit = objODBCDatareader["customername"].ToString();
                    values.vertical_code = objODBCDatareader["vertical_code"].ToString();
                    values.businesshead_name = objODBCDatareader["business_head"].ToString();
                    values.zonal_name = objODBCDatareader["zonal_head"].ToString();
                    values.cluster_manager_name = objODBCDatareader["cluster_manager"].ToString();
                    values.relationshipmgmt_name = objODBCDatareader["relationship_manager"].ToString();
                    values.creditmanager_name = objODBCDatareader["creditmgmt_name"].ToString();
                    values.customer_urnedit = objODBCDatareader["customer_urn"].ToString();
                    values.contactPersonedit = objODBCDatareader["contactperson"].ToString();
                    values.addressline1edit = objODBCDatareader["address"].ToString();
                    values.addressline2edit = objODBCDatareader["address2"].ToString();
                    values.mobileNoedit = objODBCDatareader["mobileno"].ToString();
                    values.customer_urnedit = objODBCDatareader["customer_urn"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }

        public bool DaGetTempLegalSRdtl(string customer_gid, MdlRaiselegalSR values)
        {
            try
            {
                msSQL = "select * from lgl_tmp_traiselegalsr where customer_gid='" + customer_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                


                if (objODBCDatareader.HasRows==true)
                {
                    objODBCDatareader.Read();
            
                    values.account_name = objODBCDatareader["account_name"].ToString();
                    values.email_id = objODBCDatareader["email_id"].ToString();
                    values.address = objODBCDatareader["address"].ToString();
                    values.constitution = objODBCDatareader["constitution"].ToString();
                    values.financed_by = objODBCDatareader["financed_by"].ToString();
                    values.deal_year = objODBCDatareader["deal_year"].ToString();
                    values.cycles_sanctiondated = objODBCDatareader["cycles_sanctiondated"].ToString();
                    values.cibil_data = objODBCDatareader["cibil_data"].ToString();
                    values.business_activity = objODBCDatareader["business_activity"].ToString();
                    values.limit_sanction = objODBCDatareader["limit_sanction"].ToString();
                    values.primary_securities = objODBCDatareader["primary_securities"].ToString();
                    values.collateral_securities = objODBCDatareader["collateral_securities"].ToString();
                    values.details_UDC_PDC = objODBCDatareader["details_UDC_PDC"].ToString();
                    values.unit_working_status = objODBCDatareader["unit_working_status"].ToString();
                    values.other_banker_exposures = objODBCDatareader["other_banker_exposures"].ToString();
                    values.status_current_overdue = objODBCDatareader["status_current_overdue"].ToString();
                    values.churing_account = objODBCDatareader["churing_account"].ToString();
                    values.other_group_companies = objODBCDatareader["other_group_companies"].ToString();
                    values.meeting_details = objODBCDatareader["meeting_details"].ToString();
                    values.restructuring_data = objODBCDatareader["restructuring_data"].ToString();
                    values.other_banker_borrower = objODBCDatareader["other_banker_borrower"].ToString();
                    values.instances_PTP = objODBCDatareader["instances_PTP"].ToString();
                    values.statuslegal_action = objODBCDatareader["statuslegal_action"].ToString();
                    values.demandnotice_details = objODBCDatareader["demandnotice_details"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
                    values.remarks = objODBCDatareader["remarks"].ToString();
                    values.auth_status= objODBCDatareader["auth_status"].ToString();
                    
                }
                objODBCDatareader.Close();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }

        public bool DaGetTempLegalSRdtls(string templegalsr_gid,  MdlRaiselegalSR values)
        {
            try
            {
                msSQL = "select * from lgl_tmp_traiselegalsr where templegalsr_gid='" + templegalsr_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                //msSQL = "select auth_remarks from lgl_trn_traiselegalSR where templegalsr_gid='" + templegalsr_gid + "'";
                //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    objODBCDatareader.Read();

                    values.account_name = objODBCDatareader["account_name"].ToString();
                    values.email_id = objODBCDatareader["email_id"].ToString();
                    values.address = objODBCDatareader["address"].ToString();
                    values.constitution = objODBCDatareader["constitution"].ToString();
                    values.financed_by = objODBCDatareader["financed_by"].ToString();
                    values.deal_year = objODBCDatareader["deal_year"].ToString();
                    values.cycles_sanctiondated = objODBCDatareader["cycles_sanctiondated"].ToString();
                    values.cibil_data = objODBCDatareader["cibil_data"].ToString();
                    values.business_activity = objODBCDatareader["business_activity"].ToString();
                    values.limit_sanction = objODBCDatareader["limit_sanction"].ToString();
                    values.primary_securities = objODBCDatareader["primary_securities"].ToString();
                    values.collateral_securities = objODBCDatareader["collateral_securities"].ToString();
                    values.details_UDC_PDC = objODBCDatareader["details_UDC_PDC"].ToString();
                    values.unit_working_status = objODBCDatareader["unit_working_status"].ToString();
                    values.other_banker_exposures = objODBCDatareader["other_banker_exposures"].ToString();
                    values.status_current_overdue = objODBCDatareader["status_current_overdue"].ToString();
                    values.churing_account = objODBCDatareader["churing_account"].ToString();
                    values.other_group_companies = objODBCDatareader["other_group_companies"].ToString();
                    values.meeting_details = objODBCDatareader["meeting_details"].ToString();
                    values.restructuring_data = objODBCDatareader["restructuring_data"].ToString();
                    values.other_banker_borrower = objODBCDatareader["other_banker_borrower"].ToString();
                    values.instances_PTP = objODBCDatareader["instances_PTP"].ToString();
                    values.statuslegal_action = objODBCDatareader["statuslegal_action"].ToString();
                    values.demandnotice_details = objODBCDatareader["demandnotice_details"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
                    values.remarks = objODBCDatareader["remarks"].ToString();
                    values.auth_status = objODBCDatareader["auth_status"].ToString();

                }
                objODBCDatareader.Close();
                msSQL= "select raiselegalSR_gid from lgl_trn_traiselegalsr where  templegalsr_gid='" + templegalsr_gid + "'";
                string lsraiselegalSR_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select a.auth_remarks,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,a.auth_status," +
                        "concat(c.user_firstname, ' ', c.user_lastname) as created_by  from lgl_trn_tlegalsrauthenticationlog a " +
                        "left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                        " where legalsr_gid='" + lsraiselegalSR_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                var getLegalSRdtls = new List<auth_remarks_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getLegalSRdtls.Add(new auth_remarks_list
                        {
                            created_date = dr_datarow["created_date"].ToString(),
                            created_by = (dr_datarow["created_by"].ToString()),
                            auth_status = (dr_datarow["auth_status"].ToString()),
                            auth_remarks = (dr_datarow["auth_remarks"].ToString())

                        });
                    }
                    values.auth_remarks_list = getLegalSRdtls;
                }
                dt_datatable.Dispose();
                //values.status = true;
                //return true;
                //msSQL = "select concat(user_firstname, ' ',user_lastname) as created_by from adm_mst_tuser where user_gid='" + user_gid + "'";
                //    values.created_by = objdbconn.GetExecuteScalar(msSQL);
                values.status = true;
                return true;





            }
            catch
            {
                values.status = false;
                return false;
            }
        }

        public bool DaGetDemandNoticedtl(string customer_gid, Demand_notice values)
        {
            try
            {
                msSQL = "select customer_urn from ocs_mst_tcustomer where customer_gid='" + customer_gid + "'";
                string lsurn = objdbconn.GetExecuteScalar(msSQL);
                  
                msSQL= " select  dn1status,dn2status,dn3status,if (dn1status_created_date is null,'---',date_format(dn1status_created_date,'%d-%m-%Y')) as dn1send_date,"+
                    " if (dn2status_updated_date is null,'---',date_format(dn2status_updated_date, '%d-%m-%Y')) as dn2send_date,"+
                    " if (dn3status_updated_date is null,'---',date_format(dn3status_updated_date, '%d-%m-%Y')) as dn3send_date,"+
                    " if (dn1status_created_by is null,'---',concat(f.user_firstname, ' ', f.user_lastname, '/', f.user_code)) as dn1send_by," +
                        " if (dn2status_updated_by is null,'---',concat(g.user_firstname, ' ', g.user_lastname, '/', g.user_code)) dn2send_by," +
                        " if (dn3status_updated_by is null,'---',concat(h.user_firstname, ' ', h.user_lastname, '/', h.user_code)) dn3send_by" +
                        " from lgl_trn_tdncases a" +
                        " left join ocs_mst_tcustomer b on a.urn = b.customer_urn" +
                        " left join hrm_mst_temployee c on c.employee_gid = a.dn1status_created_by" +
                        " left join hrm_mst_temployee d on d.employee_gid = a.dn2status_updated_by" +
                        " left join hrm_mst_temployee e on e.employee_gid = a.dn3status_updated_by" +
                        " left join adm_mst_tuser f on f.user_gid = c.user_gid" +
                        " left join adm_mst_tuser g on g.user_gid = d.user_gid" +
                        " left join adm_mst_tuser h on h.user_gid = e.user_gid where urn= '"+ lsurn + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdemandnotice_list = new List<demandnotice_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdemandnotice_list.Add(new demandnotice_list
                        {
                            dn1status = (dr_datarow["dn1status"].ToString()),
                            dn2status = dr_datarow["dn2status"].ToString(),
                            dn3status = (dr_datarow["dn3status"].ToString()),
                            dn1send_date = dr_datarow["dn1send_date"].ToString(),
                            dn2send_date = (dr_datarow["dn2send_date"].ToString()),
                            dn3send_date = dr_datarow["dn3send_date"].ToString(),
                            dn1send_by = (dr_datarow["dn1send_by"].ToString()),
                            dn2send_by = dr_datarow["dn2send_by"].ToString(),
                            dn3send_by = dr_datarow["dn3send_by"].ToString()
                        });
                    }
                    values.demandnotice_list = getdemandnotice_list;
                    dt_datatable.Dispose();
                }
                else
                {
                    values.demand_status = "empty";
                    dt_datatable.Dispose();
                }
                
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }

        public bool Dagetlegalsr2dndtl(string legalsr_gid, Demand_notice values)
        {
            try
            {
                msSQL = "select dn2status,dn1status,dn3status,dn1status_created_by,dn2status_updated_by,dn3status_updated_by," +
                      " dn1send_date,dn2send_date,dn3send_date from  lgl_trn_tlegalsr2dndtl where legalsr_gid='" + legalsr_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdemandnotice_list = new List<demandnotice_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdemandnotice_list.Add(new demandnotice_list
                        {
                            dn1status = (dr_datarow["dn1status"].ToString()),
                            dn2status = dr_datarow["dn2status"].ToString(),
                            dn3status = (dr_datarow["dn3status"].ToString()),
                            dn1send_date = dr_datarow["dn1send_date"].ToString(),
                            dn2send_date = (dr_datarow["dn2send_date"].ToString()),
                            dn3send_date = dr_datarow["dn3send_date"].ToString(),
                            dn1send_by = (dr_datarow["dn1send_by"].ToString()),
                            dn2send_by = dr_datarow["dn2send_by"].ToString(),
                            dn3send_by = dr_datarow["dn3send_by"].ToString()
                        });
                    }
                    values.demandnotice_list = getdemandnotice_list;
                    dt_datatable.Dispose();
                }
                else
                {
                    values.demand_status = "empty";
                    dt_datatable.Dispose();
                }
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }





        //public bool DaGetHoldremarks(string raiselegalSR_gid, holdremarks values)
        //{
           
        //        msSQL = "select auth_remarks from lgl_trn_traiselegalSR where raiselegalSR_gid='" + raiselegalSR_gid + "'";
        //        dt_datatable = objdbconn.GetDataTable(msSQL);
        //    return false;
        //}








        public bool DaGetsanctionloandtl(string customer_gid, sanctionloanlist values)
        {
            try
            {
                msSQL = "select customer2sanction_gid,sanction_refno,date_format(sanction_date,'%d-%m-%Y') as sanctiondate,sanction_limit," +
                           "  sanction_amount from ocs_mst_tcustomer2sanction where customer_gid='" + customer_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getloanlistdtl = new List<customer2loandtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getloanlistdtl.Add(new customer2loandtl
                        {
                            sanction_amount = (dr_datarow["sanction_amount"].ToString()),
                            sanction_limit = dr_datarow["sanction_limit"].ToString(),
                            sanction_refno = (dr_datarow["sanction_refno"].ToString()),
                            sanction_date = (dr_datarow["sanctiondate"].ToString())
                           
                        });
                    }
                    values.customer2loandtl = getloanlistdtl;
                }
                dt_datatable.Dispose();
                values.status = true;
                return true;
            }
            catch
            {
                values.status = false;
                return false;
            }
        }
        public bool DaGetloanList(sanctionloanlist objvalues,string customer_gid)
        {
            msSQL = " select loanref_no,loan_gid,loan_title from ocs_trn_tloan where customer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloanlistdtl = new List<customer2loandtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt_datarow in dt_datatable.Rows)
                {
                    getloanlistdtl.Add(new customer2loandtl
                    {
                        loanref_no = (dt_datarow["loanref_no"].ToString()),
                        loan_title = dt_datarow["facility_type"].ToString(),
                    });
                    objvalues.customer2loandtl = getloanlistdtl;
                }
            }
            dt_datatable.Dispose();
            objvalues.status = true;
            return true;
        }

        public bool DagetdeletetempcontRM(string employee_gid)
        {
            msSQL = "delete from lgl_tmp_tcontactdtlRM where created_by='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult!=0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool DaGetCustomerGuarantors(string customer_gid, customerGuarantorslist values)
        {

            msSQL = " select customer2guarantor_gid,guarantors_name,guarantor_age,networth,basisofNW " +
                    " from ocs_mst_tcustomer2guarantor where customer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_customerGurantors = new List<customerGuarantors>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    if (dr_datarow["guarantor_age"].ToString() != "")
                    {
                        lsguarantor_age = Convert.ToInt16(dr_datarow["guarantor_age"].ToString());
                    }

                    get_customerGurantors.Add(new customerGuarantors
                    {
                        guarantors_name = (dr_datarow["guarantors_name"].ToString()),
                        guarantor_age = lsguarantor_age,
                        networth = (dr_datarow["networth"].ToString()),
                        basisofNW = (dr_datarow["basisofNW"].ToString()),
                        customer2guarantor_gid = (dr_datarow["customer2guarantor_gid"].ToString())
                    });
                }
                values.customerGuarantors = get_customerGurantors;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;

        }

        public bool DaGetCustomerPromoter(string customer_gid, customerpromotorslist values)
        {

            msSQL = " select customer2promotor_gid,promoter_name,designation,promoter_age,mobile " +
                    " from ocs_mst_tcustomer2promotor where customer_gid='" + customer_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_customerPromotor = new List<customerPromoter>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    if (dr_datarow["promoter_age"].ToString() != "")
                    {
                        lspromotor_age = Convert.ToInt16(dr_datarow["promoter_age"].ToString());
                    }

                    if (dr_datarow["mobile"].ToString() != "")
                    {
                        lspromotor_mobile = Convert.ToDouble(dr_datarow["mobile"].ToString());
                    }


                    get_customerPromotor.Add(new customerPromoter
                    {
                        customer2promotor_gid = (dr_datarow["customer2promotor_gid"].ToString()),
                        promoter_name = (dr_datarow["promoter_name"].ToString()),
                        designation = (dr_datarow["designation"].ToString()),
                        promoter_age = lspromotor_age,
                        mobile = lspromotor_mobile,
                    });
                }
                values.customerPromoter = get_customerPromotor;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }

        public bool Dagetcollateralinfo(string legalsr_gid, customerCollaterallist values)
            {
            msSQL = "select concat(security_type,'/',account_status,'/',security_description) as collateral_info from lgl_trn_tlegalsr2collateral " +
                " where legalsr_gid='" + legalsr_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcollateral_info = new List<customerCollateral>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt_datarow in dt_datatable.Rows)
                {
                    getcollateral_info.Add(new customerCollateral
                    {

                        collateral_info = dt_datarow["collateral_info"].ToString(),
                    });
                    values.customerCollateral = getcollateral_info;
                }
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
            }

        public bool Dagettemprmdtl(string templegalsr_gid, contactdetailsRM_list values)
        {
          
            msSQL = " select * from lgl_trn_tcontactdtlRM where legalsr_gid ='" + templegalsr_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactdetailsRM_list = new List<contactdetailsRM>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactdetailsRM_list.Add(new contactdetailsRM
                    {
                        contact_name = (dr_datarow["contact_name"].ToString()),
                        contact_location = (dr_datarow["contact_location"].ToString()),
                        contact_mobileno = (dr_datarow["contact_mobileno"].ToString()),
                    });
                }
                values.contactdetailsRM = getcontactdetailsRM_list;
            }
            dt_datatable.Dispose();
            values.status = true;
            return true;
        }
        public bool DaGetRequestedDtl(string legalsr_gid, MdlRaiselegalSR values)
        {
            msSQL = " select concat(c.user_firstname, ' ', c.user_lastname, '/', c.user_code) as raised_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as raised_date," +
                    " e.department_name as raised_by_department, b.employee_mobileno as raised_by_mobileno, b.employee_emailid as raised_by_emailid" +
                    " from lgl_trn_traiselegalSR a" +
                    " left join hrm_mst_temployee b on  a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on b.user_gid=c.user_gid" +
                    " left join hrm_mst_tdepartment e on b.department_gid=e.department_gid" +
                    " where raiselegalSR_gid = '" + legalsr_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.raised_by = objODBCDatareader["raised_by"].ToString();
                values.raised_date = objODBCDatareader["raised_date"].ToString();
                values.raised_by_department = objODBCDatareader["raised_by_department"].ToString();
                values.raised_by_mobileno = objODBCDatareader["raised_by_mobileno"].ToString();
                values.raised_by_emailid = objODBCDatareader["raised_by_emailid"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
            return true;
        }

    }

}