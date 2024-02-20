using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;




namespace ems.hbapiconn.Functions
{
    public class HBAPICmnFunctions
    {
        string msSQL;
        dbconn objdbconn = new dbconn();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string lsvariety_gid, lserp_id;

        public string GetDepartmentLMSCode(string employee_externalid)
        {
            string lssubfunction_gid, lslms_code;

            msSQL = "select subfunction_gid from hrm_mst_temployee where employee_externalid = '" + employee_externalid + "'";
            lssubfunction_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select lms_code from sys_mst_tsubfunction where subfunction_gid = '" + lssubfunction_gid + "'";
            lslms_code = objdbconn.GetExecuteScalar(msSQL);

            return lslms_code;
        }

        public string GetEntityLMSCode(string employee_externalid)
        {
            string lsentity_gid, lslms_code;

            msSQL = "select entity_gid from hrm_mst_temployee where employee_externalid = '" + employee_externalid + "'";
            lsentity_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select lms_code from adm_mst_tentity where entity_gid = '" + lsentity_gid + "'";
            lslms_code = objdbconn.GetExecuteScalar(msSQL);

            return lslms_code;
        }

        public string GetProgramLMSCode(string loansubproduct_gid, string loanproduct_gid)
        {
            string lsprogram_lmscode;

            msSQL = "select lms_code from agr_mst_tloansubproduct where loansubproduct_gid = '" + loansubproduct_gid + "'";
            lsprogram_lmscode = objdbconn.GetExecuteScalar(msSQL);

            return lsprogram_lmscode;
        }

        public string GetScopeLMSCode(string scope_gid)
        {
            string lsscope_lmscode;

            msSQL = "select lms_code from agr_mst_tscope where scope_gid = '" + scope_gid + "'";
            lsscope_lmscode = objdbconn.GetExecuteScalar(msSQL);

            return lsscope_lmscode;
        }





        public string getCompanyTypeName(string companytype_gid)
        {
            msSQL = "select companytype_name from ocs_mst_tcompanytype where companytype_gid = '" + companytype_gid + "'";
            string companytype_name = objdbconn.GetExecuteScalar(msSQL);
            return companytype_name;
        }

        public string getNsGSTStateSode(string value)
        {

            msSQL = "select ns_state_code from agr_mst_tnsstatemst where gst_state_name = '" + value + "'";
            string ns_state_code = objdbconn.GetExecuteScalar(msSQL);
            return ns_state_code;

        }

        public string getNsAddrStateCode(string value)
        {
            msSQL = "select ns_addr_code from agr_mst_tnsaddrstatemst where addr_state_name = '" + value + "'";
            string ns_state_code = objdbconn.GetExecuteScalar(msSQL);
            return ns_state_code;
        }

        public bool validateProgramOfContract(string loansubproduct_gid, string loanproduct_gid)
        {
            bool programStatus = false;

            msSQL = " select loansubproduct_gid,loanproduct_gid from agr_trn_terpcontractprogram where loansubproduct_gid='" + loansubproduct_gid + "' and loanproduct_gid = '" + loanproduct_gid +"'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                programStatus = true;
            }
            objODBCDatareader.Close();

            return programStatus;
        }

        public string getScopeERPID(string scope_name)
        {
            string lslms_code;

            msSQL = "select lms_code from agr_mst_tscope where scope_name = '" + scope_name + "'";
            lslms_code = objdbconn.GetExecuteScalar(msSQL);

            return lslms_code;
        }

        public string getProgramERPID(string loansubproduct_gid, string loanproduct_gid)
        {
            string lslms_code;

            msSQL = "select lms_code from agr_mst_tloansubproduct where loansubproduct_gid = '" + loansubproduct_gid + "' and loanproduct_gid='" + loanproduct_gid + "'";
            lslms_code = objdbconn.GetExecuteScalar(msSQL);

            return lslms_code;


        }

        public bool validateCommodityERPMapping(string application2loan_gid)
        {
            msSQL = " select application2product_gid from agr_mst_tapplication2product where application2loan_gid = '" + application2loan_gid + "' and erp_status = 'No'";
            dt_datatable = objdbconn.GetDataTable(msSQL);


            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " select variety_gid from agr_mst_tapplication2product where application2product_gid = '" + dt["application2product_gid"] + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {
                        lsvariety_gid = objODBCDatareader["variety_gid"].ToString();
                    }
                    objODBCDatareader.Close();

                    msSQL = " select erp_id from ocs_mst_tvariety where variety_gid = '" + lsvariety_gid + "'";
                    lserp_id = objdbconn.GetExecuteScalar(msSQL);

                    if (String.IsNullOrEmpty(lserp_id))
                        return false;
                }
            }
            return true;
        }

        public string fetchCompanyTypeName(string companytype_gid)
        {
            msSQL = "select companytype_name from ocs_mst_tcompanytype where companytype_gid = '" + companytype_gid + "'";
            string companytype_name = objdbconn.GetExecuteScalar(msSQL);
            return companytype_name;
        }

        public string fetchCompanyCode()
        {
            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            string lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            return lscompany_code;
        }


    }
}