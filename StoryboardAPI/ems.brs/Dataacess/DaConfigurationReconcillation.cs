using ems.brs.Models;
using ems.storage.Functions;
using ems.utilities.Functions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using static ems.brs.Models.MdlConfigurationReConcillation;

namespace ems.brs.Dataacess
{
    /// <summary>
    /// To add,edit the each bank template row - column dynamically fetch master
    /// </summary>
    /// <remarks>Written by Motches </remarks>
    public class DaConfigurationReconcillation
    {
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        DataTable dt_datatable;
        string msSQL, msGetGid;
        OdbcDataReader objODBCDatareader;
        dbconn objdbconn = new dbconn();
        int mnresult;
        string lsacc_no, lstransc_id, lsdatastart_row, lstrn_date, lsvalue_date,
                    lspayment_date, lstransact_particulars, lsdebit_amt, lscredit_amt, lstransact_val, lsremarks,
                    lscustref_no, lsbranch_name, lsbalance_amt, lschq_no, lscr_dr, lsknockoffby_finance;

        public void DaGetConfigurationSummary(MdlConfigurationReConcillation objConfigurationReconcillation, string user_gid)
        {
            try
            {

                msSQL = " SELECT distinct a.bankconfig_gid, a.brsbank_gid, a.transc_id, a.datastart_row, a.acc_no, a.trn_date, a.value_date, a.payment_date , " +
           " a.transact_particulars, a.debit_amt, a.credit_amt, a.transact_val, a.remarks, a.custref_no, a.branch_name, a.balance_amt, " +
         " a.chq_no, concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code) as created_by, date_format(a.created_date,'%d-%b-%Y %h:%i %p') as created_date, a.cr_dr, a.updated_by," +
         " a.updated_date,c.bankname_name,a.knockoffby_finance FROM brs_mst_tbankconfiguration a " +
             " left join brs_mst_tbank b on a.brsbank_gid = b.brsbank_gid " +
            " left join ocs_mst_tbankname c on  c.bankname_gid = a.brsbank_gid " +
           " left join hrm_mst_temployee d on a.created_by = d.employee_gid " +
          " left join adm_mst_tuser e on a.created_by = e.user_gid " +
          " order by created_date desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getconfiguration_summary = new List<configuration_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getconfiguration_summary.Add(new configuration_list
                        {
                            bankconfig_gid = (dr_datarow["bankconfig_gid"].ToString()),
                            bank_name = (dr_datarow["bankname_name"].ToString()),
                            bank_gid = (dr_datarow["brsbank_gid"].ToString()),
                            transc_id = (dr_datarow["transc_id"].ToString()),
                            datastart_row = (dr_datarow["datastart_row"].ToString()),
                            acc_no = (dr_datarow["acc_no"].ToString()),
                            trn_date = (dr_datarow["trn_date"].ToString()),
                            value_date = (dr_datarow["value_date"].ToString()),
                            payment_date = (dr_datarow["payment_date"].ToString()),
                            transact_particulars = (dr_datarow["transact_particulars"].ToString()),
                            debit_amt = (dr_datarow["debit_amt"].ToString()),
                            credit_amt = (dr_datarow["credit_amt"].ToString()),
                            transact_val = (dr_datarow["transact_val"].ToString()),
                            branch_name = (dr_datarow["branch_name"].ToString()),
                            balance_amt = (dr_datarow["balance_amt"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            chq_no = (dr_datarow["chq_no"].ToString()),                          
                            cr_dr = (dr_datarow["cr_dr"].ToString()),
                            custref_no = (dr_datarow["custref_no"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            knockoffby_finance = (dr_datarow["knockoffby_finance"].ToString())
                        });
                    }
                    objConfigurationReconcillation.configuration_summary = getconfiguration_summary;
                }
                dt_datatable.Dispose();

                objConfigurationReconcillation.status = true;
            }
            catch
            {
                objConfigurationReconcillation.status = false;
            }

        }

        public void DaAddbankconfiguration(MdlConfigurationReConcillation values, string user_gid)
        {

           //lstransact_particulars = string.Join(values.transact_particulars, values.transact_particulars);

            msGetGid = objcmnfunctions.GetMasterGID("BRSC");
            msSQL = " insert into brs_mst_tbankconfiguration(" +
                    " bankconfig_gid," +
                    " brsbank_gid," +
                    " transc_id," +
                    " datastart_row," +
                    " acc_no," +
                    " trn_date," +
                    " value_date," +
                    " payment_date," +
                    " transact_particulars," +
                    " debit_amt," +
                    " credit_amt," +
                    " transact_val," +
                    " remarks," +
                    " custref_no," +
                    " branch_name," +
                    " balance_amt," +
                    " chq_no," +
                    " cr_dr," +
                    " knockoffby_finance,"+
                    " created_by," +
                    " created_date)" +
                    " values(" +
                     "'" + msGetGid + "'," +
                    "'" + values.brsbank_gid + "'," +
                    "'" + values.transc_id + "'," +
                    "'" + values.datastart_row + "'," +
                    "'" + values.acc_no + "'," +
                    "'" + values.trn_date + "'," +
                    "'" + values.value_date + "'," +
                    "'" + values.payment_date + "'," +
                    "'" + values.transact_particulars + "'," +
                    "'" + values.debit_amt + "'," +
                    "'" + values.credit_amt + "'," +
                    "'" + values.transact_val + "'," +
                    "'" + values.remarks + "'," +
                    "'" + values.custref_no + "'," +
                    "'" + values.branch_name + "'," +
                    "'" + values.balance_amt + "'," +
                    "'" + values.chq_no + "'," +
                    "'" + values.cr_dr + "'," +
                    "'" + values.knockoffby_finance + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Bank Fields Added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Adding";
            }
        }
        public void DaEditbankconfiguration(string bankconfig_gid, MdlConfigurationReConcillation values)
        {
            try
            {
                msSQL = " SELECT a.bankconfig_gid,a.brsbank_gid, a.transc_id, a.datastart_row, a.acc_no, a.trn_date, a.value_date," +
                    " a.payment_date, a.transact_particulars, a.debit_amt, a.credit_amt, a.transact_val, a.remarks, " +
                    "a.custref_no, a.branch_name, a.balance_amt, a.chq_no, a.cr_dr,b.bank_name,a.knockoffby_finance FROM brs_mst_tbankconfiguration a " +
                    "left join brs_mst_tbank b on b.brsbank_gid= a.brsbank_gid"+
                        " where bankconfig_gid='" + bankconfig_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.brsbank_gid = objODBCDatareader["brsbank_gid"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    lsacc_no = objODBCDatareader["acc_no"].ToString();
                    //lscustref_no = objODBCDatareader["custref_no"].ToString();
                    lstransc_id = objODBCDatareader["transc_id"].ToString();
                    //lscustref_no = objODBCDatareader["custref_no"].ToString();
                    values.datastart_row = objODBCDatareader["datastart_row"].ToString();
                    lstrn_date = objODBCDatareader["trn_date"].ToString();
                    lsvalue_date = objODBCDatareader["value_date"].ToString();
                    lspayment_date = objODBCDatareader["payment_date"].ToString();
                    lstransact_particulars = objODBCDatareader["transact_particulars"].ToString();
                    lsdebit_amt = objODBCDatareader["debit_amt"].ToString();
                    lscredit_amt = objODBCDatareader["credit_amt"].ToString();
                    lstransact_val = objODBCDatareader["transact_val"].ToString();
                    lsremarks = objODBCDatareader["remarks"].ToString();
                    lscustref_no = objODBCDatareader["custref_no"].ToString();
                    lsbranch_name = objODBCDatareader["branch_name"].ToString();
                    lsbalance_amt = objODBCDatareader["balance_amt"].ToString();
                    lschq_no = objODBCDatareader["chq_no"].ToString();
                    lscr_dr = objODBCDatareader["cr_dr"].ToString();
                    lsknockoffby_finance = objODBCDatareader["knockoffby_finance"].ToString();

                }
                string[] splits = lsacc_no.Split(',');
                string[] splits1 = lscustref_no.Split(',');
                string[] splits2 = lstransc_id.Split(',');
                //string[] splits3 = lscustref_no.Split(',');
                string[] splits4 = lstrn_date.Split(',');
                string[] splits5 = lsvalue_date.Split(',');
                string[] splits6 = lspayment_date.Split(',');
                string[] splits7 = lstransact_particulars.Split(',');
                string[] splits8 = lsdebit_amt.Split(',');
                string[] splits9 = lscredit_amt.Split(',');
                string[] splits10 = lstransact_val.Split(',');
                //string[] splits11 = lscustref_no.Split(',');
                string[] splits12 = lsbranch_name.Split(',');
                string[] splits13 = lsbalance_amt.Split(',');
                string[] splits14 = lschq_no.Split(',');
                string[] splits15 = lscr_dr.Split(',');
                string[] splits16 = lsknockoffby_finance.Split(',');

                //values.datastart_row = lsdatastart_row;
                values.acc_norow = (splits[0]);
                values.acc_nocol = (splits[1]);
                values.custref_norow = (splits1[0]);
                values.custref_nocol = (splits1[1]);
                values.transc_idrow = (splits2[0]);
                values.transc_idcol = (splits2[1]);
                values.trn_daterow = (splits4[0]);
                values.trn_datecol = (splits4[1]);
                values.value_daterow = (splits5[0]);
                values.value_datecol = (splits5[1]);
                values.payment_daterow = (splits6[0]);
                values.payment_datecol = (splits6[1]);
                values.transact_particularsrow = (splits7[0]);
                values.transact_particularscol = (splits7[1]);
                values.debit_amtrow = (splits8[0]);
                values.debit_amtcol = (splits8[1]);
                values.credit_amtrow = (splits9[0]);
                values.credit_amtcol = (splits9[1]);
                values.transact_valrow = (splits10[0]);
                values.transact_valcol = (splits10[1]);
                values.branch_namerow= (splits12[0]);
                values.branch_namecol = (splits12[1]);
                values.balance_amtrow = (splits13[0]);
                values.balance_amtcol = (splits13[1]);
                values.chq_norow = (splits14[0]);
                values.chq_nocol = (splits14[1]);
                values.cr_drrow = (splits15[0]);
                values.cr_drcol = (splits15[1]);
                values.knockoffby_financerow = (splits16[0]);
                values.knockoffby_financecol = (splits16[1]);
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
         
        }
        public void DaDeleteConfigurationdata(string bankconfig_gid, MdlConfigurationReConcillation values)
        {

            msSQL = "delete from brs_mst_tbankconfiguration where bankconfig_gid='" + bankconfig_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnresult != 0)
            {

                values.message = "Configuration data deleted successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting the Configuration data";
                values.status = false;

            }
        }

        public void DaUpdatebankconfiguration(MdlConfigurationReConcillation values,string user_gid)
        {
            msSQL =  " update brs_mst_tbankconfiguration set " +
                     " brsbank_gid='" + values.brsbank_gid + "'," +
                     " datastart_row='" + values.datastart_row+ "'," +
                     " acc_no='" + values.acc_no + "'," +
                     " trn_date='" + values.trn_date + "'," +
                     " value_date='" + values.value_date + "'," +
                     " payment_date='" + values.payment_date + "'," +
                     " transact_particulars='" + values.transact_particulars + "'," +
                     " debit_amt='" + values.debit_amt + "'," +
                     " credit_amt='" + values.credit_amt + "'," +
                     " transact_val='" + values.transact_val + "'," +
                     " chq_no='" + values.chq_no + "'," +
                     " branch_name='" + values.branch_name + "'," +
                     " custref_no='" + values.custref_no + "'," +
                     " balance_amt='" + values.balance_amt + "'," +
                     " transc_id='" + values.transc_id + "'," +
                     " cr_dr='" +values.cr_dr+"',"+
                     " knockoffby_finance='" + values.knockoffby_finance+"'," +
                     " updated_by='" + user_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where bankconfig_gid='" + values.bankconfig_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);


            msSQL = " update brs_mst_tbank set " +
                    " bank_name='" + values.bank_name + "'" +
                    " where brsbank_gid='" + values.brsbank_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(msSQL);



            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Bank configuration updated successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error occurred while updating";
            }
        }
    }
}