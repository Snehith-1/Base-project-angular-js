using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.idas.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Configuration;
using System.Drawing;
using ems.storage.Functions;

namespace ems.idas.DataAccess
{
    public class DaIdasSanctionMIS
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        string msSQL, msGetGid;
        int mnResult, mnResult1;
        OdbcDataReader objODBCDataReader;
        

        public bool DaGetSanctionMISExport(string employee_gid, exportMIS values)
        {
            msSQL = " SELECT /*+ MAX_EXECUTION_TIME(900000) */  b.customer_urn as CustomerURN, b.customername as CustomerName, b.vertical_code as Vertical, " +
                    " a.sanction_refno as sanctionRefNo ,date_format(a.sanction_date, '%d-%m-%Y') as SanctionDate, " +
                    " format((sanction_amount), 2) as SanctionAmount,a.facility_type as FacilityType , " +
                    " b.relationshipmgmt_name as RelationshipManager, c.employee_emailid as RM_Email, " +
                    " c.employee_mobileno as RM_PhoneNo, b.creditmgmt_name as CreditManager, " +
                    " b.zonal_name as ZonalHead,b.businesshead_name as BusinessHead , " +
                    " b.cluster_manager_name as ClusterManager, b.constitution_name as Constitution,b.state as State , " +
                    " b.contactperson as ContactPerson , b.contact_no as ContactNumber, b.address as AddressLine1, " +
                    " b.address2 as AddressLine2 , b.mobileno as MobileNumber,b.postalcode as Pincode , " +
                    " b.email as EmailID,a.sanction_type as SanctionType, a.purpose_lending as PurposeofLending , " +
                    " case when virtual_accountno='' then 'NA' else virtual_accountno end as VirtualAccountNo, " +
                    " case when status_ofBAL = 'Yes' then 'Applicable' when status_ofBAL = 'No' " +
                    " then 'Not Applicable'  else '-' end as EscrowAccount , " +
                    " b.GST_number as GST_Number, date_format(ccapproved_date, '%d-%m-%Y') as CCApprovedDate , " +
                    " ccapproved_by as CCApprovedBy,msme_classification as MSME_Classification, " +
                    " validity_months as ValidityMonths, " +
                    " a.natureof_proposal as NatureOfProposal, " +
                    " a.es_application as ES_Application, a.esrisk_categorization as ES_Risk_Categorization, a.esdeclaration_status as ES_Declaration_Available, " +
                    " case when updated_flag = 'N' then 'Pending' when updated_flag = 'Y' " +
                    " then 'Completed'  else '-' end as Sanction_Status , case when colanding_status='Yes' then a.colander_name else '-' end as Colander_Name," +
                    " (select group_concat(distinct buyer_name) as buyer_name  from ids_mst_taddbuyer d " +
                    " where d.customer2sanction_gid = a.customer2sanction_gid ) as Buyer_Name, " +
                    " (select group_concat(distinct maker_name) from ids_trn_tsanctiondocumentdtls e " +
                    " where e.sanction_gid = a.customer2sanction_gid) as Maker_Name, " +
                    " (select group_concat(distinct checker_name) from ids_trn_tsanctiondocumentdtls f " +
                    " where f.sanction_gid = a.customer2sanction_gid) as Checker_Name, " +
                    " (select group_concat(distinct margin) from ocs_mst_tsanction2loanfacilitytype r " +
                    " where r.customer2sanction_gid = a.customer2sanction_gid) as Margin, " +
                    " (select group_concat(distinct tenure) from ocs_mst_tsanction2loanfacilitytype r " +
                    " where r.customer2sanction_gid = a.customer2sanction_gid) as Tenure, " +
                    " ( SELECT group_concat(distinct y.approval_date)  FROM ocs_trn_tdeferral z " +
                    " INNER join ocs_trn_tdeferralapproval y on z.deferral_gid = y.deferral_gid LEFT JOIN ocs_trn_tloan w on z.loan_gid = w.loan_gid " +
                    " WHERE w.sanction_gid =a.customer2sanction_gid and z.tracking_type = 'Deferral' and z.deferral_name = 'Original Documents') as Dateof_ReceiptofOriginalDoc," +
                    " q.entity_name as Entity, b.pan_number as PAN_Number, a.status_ofBAL as Status_ofBAL," +
                    " i.existing_limit as Existing_Limit, i.principal as Repayment_Principal, i.interest as Repayment_Interest, " +
                    " date_format(h.hypothecation_date,'%d-%m-%Y') as Deed_of_Hypothecation, i.rate_interest as Rateof_Interest, " +
                    " date_format(h.mortgage_date,'%d-%m-%Y') as Mortgage_Deed, k.recovered_amount as Processing_Fee, (j.document_charge) as Documentation_Charge, " +
                    " a.primary_value_chain as Primaryvalue_Chain, a.secondary_value_chain as Secondaryvalue_Chain, date_format(h.pdfgenerate_date,'%d-%m-%Y') as Date_ofLSA_Release, " +
                    " (select group_concat(distinct scandocument_date) from " +
                    " ids_trn_tsanctiondocumentdtls m where m.sanction_gid=a.customer2sanction_gid) as Dateof_ReceiptDocsVetting " +
                    " FROM ocs_mst_tcustomer2sanction a " +
                    " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid " +
                    " left join hrm_mst_temployee c on b.relationship_manager = c.employee_gid" +
                    " left join ids_trn_tlsa h on a.customer2sanction_gid=h.customer2sanction_gid " +
                    " left join ids_trn_tlimitinfodtl i on h.lsacreate_gid=i.lsacreate_gid" +
                    " left join ids_trn_tdocumentcharges j on h.lsacreate_gid=j.lsacreate_gid" +
                    " left join ids_trn_tprocessingfees k on h.lsacreate_gid=k.lsacreate_gid" +
                    " left join adm_mst_tentity q on q.entity_gid=a.entity_gid group by a.customer2sanction_gid" ;
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                string lscompany_code = string.Empty;
                MemoryStream ms = new MemoryStream();

                ExcelPackage excel = new ExcelPackage(ms);
                var workSheet = excel.Workbook.Worksheets.Add("SanctionMIS");
                try
                {
                    msSQL = " select company_code from adm_mst_tcompany";

                    lscompany_code = objdbconn.GetExecuteScalar(msSQL);
                    values.excel_path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "IDAS/SanctionMISDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    {
                        if ((!System.IO.Directory.Exists(values.excel_path)))
                            System.IO.Directory.CreateDirectory(values.excel_path);
                    }

                    values.excel_name = "SanctionMIS" + DateTime.Now.ToString("(dd-MM-yyyy HH-mm-ss)") + ".xlsx";
                    values.excel_path = ConfigurationManager.AppSettings["file_path"] + "/erp_documents" + "/" + lscompany_code + "/" + "IDAS/SanctionMISDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.excel_name;
                    values.excel_cloudpath = lscompany_code + "/" + "IDAS/SanctionMISDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.excel_name;
                    workSheet.Cells[1, 1].LoadFromDataTable(dt_datatable, true);
                    FileInfo file = new FileInfo(values.excel_path);
                    using (var range = workSheet.Cells[1, 1, 1, 59]) 
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.Green);
                        range.Style.Font.Color.SetColor(Color.White);
                    }
                    excel.SaveAs(ms);
                    bool status;
                    status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "IDAS/SanctionMISDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + values.excel_name, ms);
                    ms.Close();

                    dt_datatable.Dispose();
                    values.status = true;
                    values.message = "Success";
                    values.excel_path = objcmnstorage.EncryptData(values.excel_path);
                    values.excel_cloudpath = objcmnstorage.EncryptData(values.excel_cloudpath);
                }
                catch (Exception ex)
                {
                    dt_datatable.Dispose();
                    values.message = ex.ToString();
                    values.status = false;
                }
            }
            else
            {
                dt_datatable.Dispose();
                values.message = "No Records Found..!";
                values.status = false;
            }

            return true;
        }

        public void DaGetSanctionMISSummary(sanctionMISSummary values)
        {
            try
            {
                msSQL = " SELECT distinct a.customer2sanction_gid,b.customer_urn,a.sanction_refno, " +
                       " date_format(a.sanction_date,'%d-%m-%Y') as sanction_date, " +
                       " format((sanction_amount), 2) as sanction_amount,a.sanction_limit,b.customername,b.vertical_code, " +
                       " concat(d.user_code, ' / ', d.user_firstname, d.user_lastname) as created_by, " +
                       " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date " +
                       " FROM ocs_mst_tcustomer2sanction a " +
                       " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid " +
                       " LEFT JOIN hrm_mst_temployee c ON a.created_by = c.employee_gid " +
                       " LEFT JOIN adm_mst_tuser d ON c.user_gid = d.user_gid " +
                       " ORDER BY customer2sanction_gid DESC";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_sanctiondtl = new List<sanctionMISdtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        get_sanctiondtl.Add(new sanctionMISdtl
                        {
                            customer2sanction_gid = dt["customer2sanction_gid"].ToString(),
                            sanction_refno = dt["sanction_refno"].ToString(),
                            sanction_date = dt["sanction_date"].ToString(),
                            sanction_amount = dt["sanction_amount"].ToString(),
                            customername = dt["customername"].ToString(),
                            customer_urn = dt["customer_urn"].ToString(),
                            vertical_code =dt ["vertical_code"].ToString(),
                        });
                    }
                    values.sanctionMISdtl = get_sanctiondtl;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }


        public void DaGetSanctionMISDetails(string sanction_gid, sanctionMISviewdtl values)
        {
            try
            {
                msSQL = " SELECT h.entity_name,a.customer2sanction_gid,b.customer_urn,a.sanction_refno,a.state, " +
                 " format((sanction_amount), 2) as sanction_amount,b.customername, " +
                 " date_format(a.sanction_date,'%d-%m-%Y') as sanctionDate," +
                 " b.zonal_head,b.zonal_name,b.business_head,b.businesshead_name,b.relationshipmgmt_name,a.status_ofBAL, " +
                 " b.cluster_manager_name,b.creditmgmt_name,c.employee_emailid,c.employee_mobileno, " +
                 " b.contactperson,b.address,b.address2,b.mobileno, " +
                 " b.vertical_code,b.constitution_name,pincode,email_id,a.sanction_type, a.purpose_lending, " +
                 " b.email,b.postalcode,b.contact_no, " +
                 " case when virtual_accountno='' then 'NA' else virtual_accountno end as virtual_accountno, " +
                 " case when status_ofBAL='Yes' then 'Applicable' when status_ofBAL='No' then 'Not Applicable'  else '-' end as escrowaccount ," +
                 " case when updated_flag = 'N' then 'Pending' when updated_flag = 'Y' " +
                 " then 'Completed'  else '-' end as sanction_status , case when colanding_status='Yes' then a.colander_name else '-' end as colander_name," +
                 " b.GST_number, date_format(ccapproved_date,'%d-%m-%Y') as ccapproved_date ,ccapproved_by,msme_classification, " +
                 " validity_months,a.natureof_proposal, date_format(d.hypothecation_date,'%d-%m-%Y') as hypothecation_date, " +
                 " date_format(d.mortgage_date,'%d-%m-%Y') as mortgage_date, a.securitycheque_accountnumber," +
                 " a.bankand_chequeno, a.cheque_realizationdate, a.NACH_form,a.roissuing_totalamount,a.bank_statement, i.proposed_roi, " +
                 " a.primary_value_chain as primaryvalue_chain,a.secondary_value_chain as secondaryvalue_chain, a.collateral_security, b.pan_number," +
                 " a.audited_financials ,a.stock_statement ,a.purchase_statement ,a.sales_statement ,a.debtors_statement,a.scanneduploaded_Drive,a.monitoring_visit, " +
                 " e.existing_limit, g.recovered_amount, f.document_charge, d.pdfgenerate_date, a.es_application, a.esrisk_categorization, a.esdeclaration_status FROM ocs_mst_tcustomer2sanction a " +
                 " LEFT JOIN ocs_mst_tcustomer b ON a.customer_gid = b.customer_gid " +
                 " left join hrm_mst_temployee c on b.relationship_manager=c.employee_gid " +
                 " left join ids_trn_tlsa d on a.customer2sanction_gid=d.customer2sanction_gid " +
                 " left join ids_trn_tlimitinfodtl e on d.lsacreate_gid=e.lsacreate_gid" +
                 " left join ids_trn_tdocumentcharges f on d.lsacreate_gid=f.lsacreate_gid" +
                 " left join ids_trn_tprocessingfees g on d.lsacreate_gid=g.lsacreate_gid" +
                 " left join adm_mst_tentity h on h.entity_gid=a.entity_gid" +
                 " left join ocs_mst_tsanction2loanfacilitytype i on i.customer2sanction_gid=a.customer2sanction_gid " +
                 " WHERE a.customer2sanction_gid ='" + sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows)
                {
                    values.entity = objODBCDataReader["entity_name"].ToString();
                    values.constitution = objODBCDataReader["constitution_name"].ToString();
                    values.customername = objODBCDataReader["customername"].ToString();
                    values.customer_urn = objODBCDataReader["customer_urn"].ToString();
                    values.customer2sanction_gid = objODBCDataReader["customer2sanction_gid"].ToString();
                    values.sanction_refno = objODBCDataReader["sanction_refno"].ToString();
                    values.sanction_date = objODBCDataReader["sanctionDate"].ToString();
                    values.state = objODBCDataReader["state"].ToString();
                    values.sanction_amount = objODBCDataReader["sanction_amount"].ToString();
                    values.vertical = objODBCDataReader["vertical_code"].ToString();
                    values.nature_ofproposal = objODBCDataReader["natureof_proposal"].ToString();
                    values.GST_number = objODBCDataReader["GST_number"].ToString();
                    values.escrow_account = objODBCDataReader["escrowaccount"].ToString();
                    values.zonal_name = objODBCDataReader["zonal_name"].ToString();
                    values.businesshead_name = objODBCDataReader["businesshead_name"].ToString();
                    values.relationshipmgmt_name = objODBCDataReader["relationshipmgmt_name"].ToString();
                    values.rm_email = objODBCDataReader["employee_emailid"].ToString();
                    values.rm_mobileno = objODBCDataReader["employee_mobileno"].ToString();
                    values.cluster_manager_name = objODBCDataReader["cluster_manager_name"].ToString();
                    values.creditmanager_name = objODBCDataReader["creditmgmt_name"].ToString();
                    values.contactperson = objODBCDataReader["contactperson"].ToString();
                    values.mobileno = objODBCDataReader["mobileno"].ToString();
                    values.addressline1 = objODBCDataReader["address"].ToString();
                    values.addressline2 = objODBCDataReader["address2"].ToString();
                    values.purpose_lending = objODBCDataReader["purpose_lending"].ToString();
                    values.ccapprovedDate = (objODBCDataReader["ccapproved_date"].ToString());
                    values.customer_email = objODBCDataReader["email"].ToString();
                    values.postalcode = objODBCDataReader["postalcode"].ToString();
                    values.contact_no = objODBCDataReader["contact_no"].ToString();
                    values.virtual_accountno = objODBCDataReader["virtual_accountno"].ToString();
                    values.approval_authority = objODBCDataReader["ccapproved_by"].ToString();
                    values.ccapproval_date = objODBCDataReader["ccapproved_date"].ToString();
                    values.classification_MSME = objODBCDataReader["msme_classification"].ToString();
                    values.validity_months = objODBCDataReader["validity_months"].ToString();
                    values.hypothecation_date = objODBCDataReader["hypothecation_date"].ToString();
                    values.existing_limit = objODBCDataReader["existing_limit"].ToString();
                    values.processing_fee = objODBCDataReader["recovered_amount"].ToString();
                    values.documentation_clientvisitcharge = objODBCDataReader["document_charge"].ToString();
                    values.releaseorder_Date = objODBCDataReader["pdfgenerate_date"].ToString();
                    values.status_ofBAL = objODBCDataReader["status_ofBAL"].ToString();
                    values.primaryvalue_chain = objODBCDataReader["primaryvalue_chain"].ToString();
                    values.secondaryvalue_chain = objODBCDataReader["secondaryvalue_chain"].ToString();
                    values.collateral_security = objODBCDataReader["collateral_security"].ToString();
                    values.pan_number = objODBCDataReader["pan_number"].ToString();
                    values.sanction_type = objODBCDataReader["sanction_type"].ToString();
                    values.mortgage_date = objODBCDataReader["mortgage_date"].ToString();
                    values.sanction_status = objODBCDataReader["sanction_status"].ToString();
                    values.esrisk_categorization = objODBCDataReader["esrisk_categorization"].ToString();
                    values.es_application = objODBCDataReader["es_application"].ToString();
                    values.esdeclaration_status = objODBCDataReader["esdeclaration_status"].ToString();
                    values.rateof_interest = objODBCDataReader["proposed_roi"].ToString();
                    values.colander_name = objODBCDataReader["colander_name"].ToString();
                }
                objODBCDataReader.Close();

                msSQL = "select group_concat(26- c.rate_interest) as rate_interest,group_concat(c.facility_type) as facility_type from ocs_mst_tcustomer2sanction a " +
                " left join ids_trn_tlsa b on a.customer2sanction_gid=b.customer2sanction_gid " +
                " left join ids_trn_tlimitinfodtl c on c.lsacreate_gid=b.lsacreate_gid  " +
                " WHERE a.customer2sanction_gid ='" + sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.penal_interest = objODBCDataReader["rate_interest"].ToString();
                    values.facility_type = objODBCDataReader["facility_type"].ToString();
                }

                objODBCDataReader.Close();

                msSQL = " select group_concat(distinct buyer_name) as buyer_name from ids_mst_taddbuyer " +
                        " WHERE customer2sanction_gid ='" + sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if(objODBCDataReader.HasRows==true)
                {
                    values.nameofthe_buyers = objODBCDataReader["buyer_name"].ToString();
                }
                objODBCDataReader.Close();

                msSQL = " select group_concat(distinct margin) as margin, group_concat(distinct tenure) as tenure from ocs_mst_tsanction2loanfacilitytype " +
                      " WHERE customer2sanction_gid ='" + sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.margin = objODBCDataReader["margin"].ToString();
                    values.tenure_months = objODBCDataReader["tenure"].ToString();
                }
                objODBCDataReader.Close();

                msSQL = " select group_concat(distinct c.principal) as principal, group_concat(distinct c.interest) as interest, group_concat(distinct c.rate_interest) as rate_interest " +
                   " from ocs_mst_tcustomer2sanction a  left join ids_trn_tlsa b on a.customer2sanction_gid=b.customer2sanction_gid " +
                  " left join ids_trn_tlimitinfodtl c on c.lsacreate_gid=b.lsacreate_gid " +
                    " WHERE a.customer2sanction_gid ='" + sanction_gid + "'";
                objODBCDataReader = objdbconn.GetDataReader(msSQL);
                if (objODBCDataReader.HasRows == true)
                {
                    values.repayment_principal = objODBCDataReader["principal"].ToString();
                    values.repayment_interest = objODBCDataReader["interest"].ToString();
                    //values.rateof_interest = objODBCDataReader["rate_interest"].ToString();
                }
                objODBCDataReader.Close();

                msSQL = " SELECT group_concat(distinct b.approval_date) as approval_date" +
                        " FROM ocs_trn_tdeferral a" +
                        " INNER join ocs_trn_tdeferralapproval b on a.deferral_gid = b.deferral_gid" +
                        " LEFT JOIN ocs_trn_tloan c on a.loan_gid = c.loan_gid" +
                        " WHERE c.sanction_gid = '" + sanction_gid + "' and a.tracking_type = 'Deferral' and a.deferral_name = 'Original Documents'";
                values.dateof_receiptofOriginalDoc = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select group_concat(distinct scandocument_date) as scandocument_date from " +
                      " ids_trn_tsanctiondocumentdtls where sanction_gid='" + sanction_gid + "'";
                values.dateof_receiptDocsVetting = objdbconn.GetExecuteScalar(msSQL);

                //msSQL = " select group_concat(distinct phydocument_date) as phydocument_date from " +
                //      " ids_trn_tsanctiondocumentdtls where sanction_gid='" + sanction_gid + "'";
                //values.dateof_receiptofOriginalDoc = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select group_concat(distinct maker_name) as maker_name from " +
                       " ids_trn_tsanctiondocumentdtls where sanction_gid='" + sanction_gid + "'";
                values.casesvetted_bycadmaker = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select group_concat(distinct checker_name) as checker_name from " +
                       " ids_trn_tsanctiondocumentdtls where sanction_gid='" + sanction_gid + "'";
                values.casesvetted_bycadchecker = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select c.security_type,c.security_description,c.collateralref_no from ocs_mst_tcustomer2sanction a" +
                  " left join ids_trn_tlsa b on a.customer2sanction_gid=b.customer2sanction_gid " +
                  " left join ocs_trn_tcustomercollateral c on c.lsacreate_gid=b.lsacreate_gid " +
                  " left join ocs_trn_tsecuritytype d on d.securitytype_gid=c.securitytype_gid" +
                  " where a.customer2sanction_gid='" + sanction_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<customersecurity_listdtl>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new customersecurity_listdtl
                        {
                            security_type = (dr_datarow["security_type"].ToString()),
                            security_description = (dr_datarow["security_description"].ToString()),
                            collateralref_no = dr_datarow["collateralref_no"].ToString()
                        });
                    }
                    values.customersecurity_listdtl = get_filename;
                }
                dt_datatable.Dispose();

                msSQL = "SELECT  @s:=@s+1 sno,document_name,phydocument_type,phydocument_date FROM ids_trn_tsanctiondocumentdtls, " +
                       " (SELECT @s:= 0) AS s WHERE sanction_gid ='" + sanction_gid + "' order by document_name asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_documentdtl = new List<documentationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        get_documentdtl.Add(new documentationlist
                        {
                            sno = dt["sno"].ToString(),
                            documentation_name = dt["document_name"].ToString(),
                            phydocument_type = dt["phydocument_type"].ToString(),
                            phydocument_date = dt["phydocument_date"].ToString(),
                        });
                    }
                    values.documentationlist = get_documentdtl;
                }
                dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                values.message = ex.ToString();
                values.status = false;
            }

        }
    }
}