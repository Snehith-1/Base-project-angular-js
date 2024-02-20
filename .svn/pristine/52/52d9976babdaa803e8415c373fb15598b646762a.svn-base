using ems.mastersamagro.Models;
using ems.utilities.Functions;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using ems.storage.Functions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Globalization;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.Linq;

namespace ems.mastersamagro.DataAccess
{

    /// <summary>
    /// This DataAccess will provide access to add datas from various stages in Supplier Application creation (General, Company, Individual, Overall limit, Product, charges, trade, Bureau & Done)
    /// </summary>
    /// <remarks>Written by Sherin Augusta, Logapriya, Abilash.A, Premchander.K </remarks>
    public class DaAgrMstSuprApplicationAdd
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        HttpPostedFile httpPostedFile;
        DataTable dt_datatable, dt_tloan, dt_tcontact, dt_tinstitution, dt_thypothecation;
        string msSQL, msGetGid, msGetGid1, msGetDocumentGid, lsapp_refno;
        int mnResult;
        string lsdocument_path, lsdocument_name, lsapplication_gid;
        string lscustomer_name, lsmobileno;
        string lsmobile_no, lsprimary_status, lswhatsapp_no, lsinstitution2mobileno_gid, lsemail_address, lsinstitution2email_gid;
        string lsaddress_typegid, lsaddress_type, lsaddressline1, lsaddressline2, lslandmark, lstaluka, lspostal_code, lscity, lsdistrict, lsinstitution2branch_gid;
        string lsstate_gid, lsstate, lscountry, lslatitude, lslongitude, lsinstitution2address_gid, lsinstitution_gid, lsgststate_gid, lsgst_state, lsgst_no, lsgst_registered;
        string lsinstitution2licensedtl_gid, lslicenseexpiry_date, lslicenseissue_date, lslicense_number, lslicensetype_name, lslicensetype_gid, lspath;
        string lsgroup_gid, lsgroup2address_gid, lsgroup2bank_gid, lsifsc_code, lsbank_accountno, lsaccountholder_name, lsbank_name, lsbank_branch, lsregion;
        string lscompany_code;
        int logCount = 0, mnResultMobile, mnResultEmail, mnResultAddress, mnResultGST, mnResultBank;
        string lsapplication_no, lsurn_status, lsurn, lsgroup_name, lsinsitution_name, lspan_status, lspan_no, lspanstatusvalue,
        lsaadhar_no, lsfirst_name, lsmiddle_name, lslast_name, lsindividual_dob, lsgender_name, lsgender_gid,
        lsdesignation_type, lsdesignation_gid, lspep_status, lspepverified_date, lsuser_type, lsusertype_gid,
        lsmaritalstatus_name, lsmaritalstatus_gid, lsfather_firstname, lsfather_middlename, lsfather_lastname,
        lsfathernominee_status, lsfather_dob, lsmother_firstname, lsmother_middlename, lsmother_lastname, lsmothernominee_status,
        lsmother_dob, lsspouse_firstname, lsspouse_middlename, lsspouse_lastname, lsspousenominee_status, lsspouse_dob,
        lseducationalqualification_name, lseducationalqualification_gid, lsmain_occupation, lsannual_income,
        lsmonthly_income, lsincometype_name, lsincometype_gid, lsyearscurrentresidece, lsdistancebranch;
        string msGetGidMobile, endRange;
        string msGetGidEmail, msGetGidpan;
        string msGetGidGST, msGetGidBank;
        string msGetGidAddress, lsaddresstype_name, lsaddresstype_gid;
        string contactimportlog_message = "";
        string lscompany_name, lscompanypan_no, lsdate_incorporation, lsbusinessstart_date, lscin_no, lsofficial_telephoneno,
        lsofficialemail_address, lscompanytype_name, lscompanytype_gid, lsassessmentagency_name, lsassessmentagency_gid,
        lsassessmentagencyrating_name, lsassessmentagencyrating_gid, lsratingas_on, lsamlcategory_name, lsamlcategory_gid,
        lsbusinesscategory_name, lsbusinesscategory_gid, lsstart_date, lsend_date, lsescrow, lslastyear_turnover, lscontactperson_firstname,
        lscontactperson_middlename, lscontactperson_lastname, lsdesignation;
        DateTime lddate_incorporation, ldbusinessstart_date, ldstart_date, ldend_date, lddate_of_formation;
        string institutionimportlog_message = "", groupimportlog_message = "";
        string lsdate_of_formation, lsgroup_type, lsgroupmember_count, lsgroupurn_status, lsgroup_urn;
        string excelRange;
        int rowCount, columnCount;
        string lsemployee_name, lsrmquery_flag;
        string sToken = string.Empty;
        Random rand = new Random();
        private string cc_mailid;
        private IEnumerable<string> lsCCReceipients, lsBCCReceipients;
        private string body;
        private string sub;
        private int ls_port;
        private string ls_server;
        private string ls_username;
        private string ls_password;
        private string tomail_id, lsBccmail_id;
        private object cc;
        private string lsclusterhead;
        private string lszonalhead;
        private string lsregionahead;
        private string lsbusinesshead;
        private string lssource;
        private string cluster_head_mailid;
        private string regional_head_mailid;
        private string business_head_mailid;
        private string zonalhead_mailid;
        private string reportingto_name;
        private string reportingto_mailid;
        private string creater_mailid;
        private string customer_name;
        private string application_no;
        private string reportingto_gid;
        private string business_head_gid;
        private string regional_head_gid;
        private string zonal_head_gid;
        private string cluster_head_gid;
        private string lsdrm_gid, lsdrm_name;
        string lsrmclustermanager_gid, lsrmregionalhead_gid, lsrmzonalhead_gid, lsrmbusinesshead_gid, lsappclustermanager_gid, lsappregionalhead_gid, lsappzonalhead_gid, lsappbusinesshead_gid, lshierarchychange_flag;
        string lsoveralllimit_amount, ls_Product, ls_Program, ls_Margin, regionalhead_name;
        public void DaGetDropDown(string employee_gid, MdlDropDown values)
        {
            //Vertical
            msSQL = " SELECT a.vertical_gid,a.vertical_name,vertical_code " +
                    " FROM ocs_mst_tvertical a  where status_log='Y' order by a.vertical_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getSegment = new List<vertical_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getSegment.Add(new vertical_list
                    {
                        vertical_gid = (dr_datarow["vertical_gid"].ToString()),
                        vertical_name = (dr_datarow["vertical_name"].ToString()),
                        vertical_code = (dr_datarow["vertical_code"].ToString()),
                    });
                }
                values.vertical_list = getSegment;
            }
            dt_datatable.Dispose();
            //Vertical Tag
            msSQL = " SELECT verticaltaggs_gid,verticaltaggs_name" +
                        " FROM ocs_mst_tverticaltaggs a  where status='Y' order by a.verticaltaggs_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplication_list = new List<verticaltaggs_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getapplication_list.Add(new verticaltaggs_list
                    {
                        verticaltaggs_gid = (dr_datarow["verticaltaggs_gid"].ToString()),
                        verticaltaggs_name = (dr_datarow["verticaltaggs_name"].ToString()),
                    });
                }
                values.verticaltaggs_list = getapplication_list;
            }
            dt_datatable.Dispose();
            //Constitution
            msSQL = " SELECT constitution_gid,constitution_name FROM ocs_mst_tconstitution a" +
                   " where status_log='Y' order by constitution_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getconstitution = new List<constitutionlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getconstitution.Add(new constitutionlist
                    {
                        constitution_gid = (dr_datarow["constitution_gid"].ToString()),
                        constitution_name = (dr_datarow["constitution_name"].ToString()),
                    });
                }
                values.constitutionlist = getconstitution;
            }
            dt_datatable.Dispose();
            //Strategic business Unit
            msSQL = " SELECT businessunit_gid,businessunit_name from ocs_mst_tbusinessunit a" +
                   " where status_log='Y' order by businessunit_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbusinessunit = new List<businessunitlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbusinessunit.Add(new businessunitlist
                    {
                        businessunit_gid = (dr_datarow["businessunit_gid"].ToString()),
                        businessunit_name = (dr_datarow["businessunit_name"].ToString()),
                    });
                }
                values.businessunitlist = getbusinessunit;
            }
            dt_datatable.Dispose();
            //Value Chain
            msSQL = " SELECT valuechain_gid,valuechain_name from ocs_mst_tvaluechain a" +
                    " where status_log='Y' order by valuechain_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvaluechain = new List<valuechainlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getvaluechain.Add(new valuechainlist
                    {
                        valuechain_gid = (dr_datarow["valuechain_gid"].ToString()),
                        valuechain_name = (dr_datarow["valuechain_name"].ToString()),
                    });
                }
                values.valuechainlist = getvaluechain;
            }
            dt_datatable.Dispose();
            //Vernacular Language
            msSQL = " SELECT vernacularlanguage_gid,vernacular_language FROM ocs_mst_tvernacularlanguage a" +
                      " where status='Y' order by a.vernacularlanguage_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvernacularlang_list = new List<vernacularlang_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getvernacularlang_list.Add(new vernacularlang_list
                    {
                        vernacularlanguage_gid = (dr_datarow["vernacularlanguage_gid"].ToString()),
                        vernacular_language = (dr_datarow["vernacular_language"].ToString()),
                    });
                }
                values.vernacularlang_list = getvernacularlang_list;
            }
            dt_datatable.Dispose();
            //Samunnati Associate - SBA
            //msSQL = " SELECT associatemaster_gid,concat(name,' / ',associate_code) as name,associate_code FROM ocs_mst_tassociatemaster" +
            //    " where status='Yes' order by associatemaster_gid desc ";

            msSQL = " (select  concat(sa_associatename, ' / ' ,samagro_code) as 'name'  , sacontactinstitution_gid as 'associatemaster_gid'  from " +
                     " ocs_mst_tsainstitution  where approvalinitated_flag = 'Y' and approval_flag = 'Y' and approvalstatus not in ('Rejected')" +
                     " and code_created_flag='Y')  union " +
                     "  (select CONCAT(sa_firstname,' ',sa_middlename,' ',sa_lastname,' / ',samagro_code) , sacontact_gid from ocs_mst_tsacontact " +
                     "  where approvalinitated_flag = 'Y' and approval_flag = 'Y' and approvalstatus not in ('Rejected') and code_created_flag='Y') ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getassociatemaster = new List<associatemasterlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getassociatemaster.Add(new associatemasterlist
                    {
                        associatemaster_gid = (dr_datarow["associatemaster_gid"].ToString()),
                        name = (dr_datarow["name"].ToString()),
                        associate_code = (dr_datarow["associate_code"].ToString()),
                    });
                }
                values.associatemasterlist = getassociatemaster;
            }
            dt_datatable.Dispose();
            //Designation
            msSQL = " SELECT a.designation_gid,a.designation_type from ocs_mst_tdesignation a" +
                   "  where status_log='Y' order by a.designation_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdesignation = new List<designationlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getdesignation.Add(new designationlist
                    {
                        designation_gid = (dr_datarow["designation_gid"].ToString()),
                        designation_type = (dr_datarow["designation_type"].ToString()),
                    });
                }
                values.designationlist = getdesignation;
            }
            dt_datatable.Dispose();
            //Credit Group
            msSQL = " SELECT creditmapping_gid,creditgroup_name FROM ocs_mst_tcreditmapping a" +
                       " where status='Y' order by a.creditmapping_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcreditgrouplist = new List<creditgrouplist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcreditgrouplist.Add(new creditgrouplist
                    {
                        creditmapping_gid = (dr_datarow["creditmapping_gid"].ToString()),
                        creditgroup_name = (dr_datarow["creditgroup_name"].ToString()),
                    });
                }
                values.creditgrouplist = getcreditgrouplist;
            }
            dt_datatable.Dispose();
            // Program 
            msSQL = " SELECT program_gid,program FROM ocs_mst_tprogram a" +
                       " where status='Y' order by a.program_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getprogram_list = new List<program_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getprogram_list.Add(new program_list
                    {
                        program_gid = (dr_datarow["program_gid"].ToString()),
                        program = (dr_datarow["program"].ToString()),
                    });
                }
                values.program_list = getprogram_list;
            }
            dt_datatable.Dispose();
            // Product 
            msSQL = " SELECT product_gid,product_name FROM ocs_mst_tproducts a" +
                       " where status='Y' order by a.product_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getproductname_list = new List<productname_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getproductname_list.Add(new productname_list
                    {
                        product_gid = (dr_datarow["product_gid"].ToString()),
                        product_name = (dr_datarow["product_name"].ToString()),
                    });
                }
                values.productname_list = getproductname_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaGetproductDropDown(string employee_gid, MdlProductDropDown values)
        {
            //Loan Product
            msSQL = " SELECT loanproduct_gid,loanproduct_name FROM agr_mst_tloanproduct a" +
                       " where status='Y' order by a.loanproduct_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloanproduct_list = new List<loanproductlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getloanproduct_list.Add(new loanproductlist
                    {
                        loanproduct_gid = (dr_datarow["loanproduct_gid"].ToString()),
                        loanproduct_name = (dr_datarow["loanproduct_name"].ToString()),
                    });
                }
                values.loanproductlist = getloanproduct_list;
            }
            dt_datatable.Dispose();
            //LoanType
            msSQL = " SELECT loantype_gid,loan_type FROM ocs_mst_tloantype a" +
                       " where status_log='Y' order by a.loantype_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getloantypelist = new List<loantypelist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getloantypelist.Add(new loantypelist
                    {
                        loantype_gid = (dr_datarow["loantype_gid"].ToString()),
                        loan_type = (dr_datarow["loan_type"].ToString()),
                    });
                }
                values.loantypelist = getloantypelist;
            }
            dt_datatable.Dispose();
            //Principal Frequency
            msSQL = " SELECT principalfrequency_gid,principalfrequency_name FROM ocs_mst_tprincipalfrequency a" +
                        " where status='Y' order by a.principalfrequency_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getprincipalfrequencylist = new List<principalfrequencylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getprincipalfrequencylist.Add(new principalfrequencylist
                    {
                        principalfrequency_gid = (dr_datarow["principalfrequency_gid"].ToString()),
                        principalfrequency_name = (dr_datarow["principalfrequency_name"].ToString()),
                    });
                }
                values.principalfrequencylist = getprincipalfrequencylist;
            }
            dt_datatable.Dispose();
            //Interest Frequency
            msSQL = " SELECT  interestfrequency_gid,interestfrequency_name FROM ocs_mst_tinterestfrequency a" +
                       " where status='Y' order by a.interestfrequency_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinterestfrequency = new List<interestfrequencylist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getinterestfrequency.Add(new interestfrequencylist
                    {
                        interestfrequency_gid = (dr_datarow["interestfrequency_gid"].ToString()),
                        interestfrequency_name = (dr_datarow["interestfrequency_name"].ToString()),
                    });
                }
                values.interestfrequencylist = getinterestfrequency;
            }
            dt_datatable.Dispose();

            //Buyer
            msSQL = " SELECT buyer_gid,concat(buyer_name,' / ',buyer_code) as buyer_name " +
                    " from ocs_mst_tbuyer where creditstatus_Approval in ('Y','N') and creditActive_status = 'Y' order by buyer_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbuyerlist = new List<buyerlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbuyerlist.Add(new buyerlist
                    {
                        buyer_name = (dr_datarow["buyer_name"].ToString()),
                        buyer_gid = (dr_datarow["buyer_gid"].ToString()),
                    });
                }
                values.buyerlist = getbuyerlist;
            }
            dt_datatable.Dispose();
            //Security Type
            msSQL = " select securitytype_gid,security_type from ocs_trn_tsecuritytype a" +
                   " where status_log='Y' order by securitytype_gid desc ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getSecurity = new List<securitytype_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getSecurity.Add(new securitytype_list
                    {
                        securitytype_gid = (dr_datarow["securitytype_gid"].ToString()),
                        security_type = (dr_datarow["security_type"].ToString()),
                    });
                }
                values.securitytype_list = getSecurity;
            }
            dt_datatable.Dispose();

            //Program
            msSQL = " SELECT  program_gid,program FROM ocs_mst_tprogram a" +
                       " where status='Y' order by a.program_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getprogramlist = new List<programlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getprogramlist.Add(new programlist
                    {
                        program_gid = (dr_datarow["program_gid"].ToString()),
                        program = (dr_datarow["program"].ToString()),
                    });
                }
                values.programlist = getprogramlist;
            }
            dt_datatable.Dispose();

            values.status = true;
        }
        public void DaGetGeneticCodeList(string employee_gid, MdlGeneticCode values)
        {
            msSQL = " SELECT geneticcode_gid,geneticcode_name FROM ocs_mst_tgeneticcode a" +
                         " where status='Y' order by a.geneticcode_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplication_list = new List<genetic_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getapplication_list.Add(new genetic_list
                    {
                        geneticcode_gid = (dr_datarow["geneticcode_gid"].ToString()),
                        geneticcode_name = (dr_datarow["geneticcode_name"].ToString()),
                    });
                }
                values.genetic_list = getapplication_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }
        public void DaSaveGeneralDtl(MdlMstApplicationAdd values, string employee_gid)
        {

            msGetGid = objcmnfunctions.GetMasterGID("APPC");
            string gsvernacularlanguage_gid = string.Empty;
            string gsvernacular_language = string.Empty;

            if (values.vernacularlanguage_list != null)
            {
                for (var i = 0; i < values.vernacularlanguage_list.Count; i++)
                {
                    gsvernacularlanguage_gid += values.vernacularlanguage_list[i].vernacularlanguage_gid + ",";
                    gsvernacular_language += values.vernacularlanguage_list[i].vernacular_language + ",";

                }

                gsvernacularlanguage_gid = gsvernacularlanguage_gid.TrimEnd(',');
                gsvernacular_language = gsvernacular_language.TrimEnd(',');
            }

            msSQL = "select concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name" +
                    " from hrm_mst_temployee a" +
                    " left join adm_mst_tuser b on a.user_gid=b.user_gid" +
                    " where a.employee_gid='" + employee_gid + "'";
            lsemployee_name = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one ,a.employeereporting_to " +
                               "  from adm_mst_tmodule2employee a " +
                               "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                               "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                                "  where a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in " +
                                "  (select modulereportingto_gid from adm_mst_tcompany)) " +
                    " and g.user_status = 'Y' and a.employee_gid ='" + employee_gid + "' group by a.employee_gid ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsdrm_gid = objODBCDatareader["employeereporting_to"].ToString();
                lsdrm_name = objODBCDatareader["level_one"].ToString();
            }
            objODBCDatareader.Close();

            if (values.onboarding_status == "Direct")
            {
                msSQL = " SELECT creditgroup_name,creditmapping_gid from ocs_mst_tcreditmapping where creditgroup_name='" + getAutoApprovalClass.CreditGroupName + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.creditgroup_gid = objODBCDatareader["creditmapping_gid"].ToString();
                    values.creditgroup_name = objODBCDatareader["creditgroup_name"].ToString();
                }
                objODBCDatareader.Close();
            }

            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            if (lsapplication_gid == "" || lsapplication_gid == null)
            {
                lsapplication_gid = values.application_gid;
            }

            if (lsapplication_gid == "" || lsapplication_gid == null)
            {
                string lsclusterhead, lsregionalhead, lszonalhead, lsbusinesshead;
                string lsclusterheadgid, lsregionalheadgid, lszonalheadgid, lsbusinessheadgid;
                string lsbaselocationgid, lsclustergid, lsregiongid, lszonalgid;
                string lsbaselocationname, lsclustername, lsregionname, lszonalname;

                msSQL = " select distinct b.baselocation_gid,b.baselocation_name,c.cluster_gid,c.cluster_name, " +
                        " c.employee_name as clusterhead,c.vertical_name as clustervertical," +
                        " e.region_gid,e.region_name,e.employee_name as regionhead,g.zonal_gid,g.zonal_name,g.employee_name as zonalhead ," +
                        " c.employee_gid as clusterhead_gid,e.employee_gid as regionhead_gid, " +
                        " g.employee_gid as zonalhead_gid,h.employee_gid as businesshead_gid, " +
                        " h.employee_name as businesshead from hrm_mst_temployee a" +
                        " left join sys_mst_tcluster2baselocation b on b.baselocation_gid = a.baselocation_gid" +
                        " left  join sys_mst_tclusterhead c on b.cluster_gid = c.cluster_gid" +
                        " left join sys_mst_tregion2cluster d on c.cluster_gid = d.cluster_gid" +
                        " left join sys_mst_tregionhead e on d.region_gid = e.region_gid" +
                        " left join sys_mst_tzone2region f on f.region_gid = d.region_gid" +
                        " left join sys_mst_tzonalhead g on f.zone_gid = g.zonal_gid" +
                        " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid where a.employee_gid = '" + employee_gid + "' and" +
                        " c.vertical_gid = '" + values.vertical_gid + "'" +
                        " and e.vertical_gid = '" + values.vertical_gid + "' and " +
                        " g.vertical_gid = '" + values.vertical_gid + "' and h.vertical_gid = '" + values.vertical_gid + "'" +
                        " and c.program_gid = '" + values.program_gid + "' and e.program_gid = '" + values.program_gid + "' and " +
                        " g.program_gid = '" + values.program_gid + "' and h.program_gid = '" + values.program_gid + "' " +
                        " and e.status='Y' and g.status='Y' and h.status='Y' and c.status='Y'";
                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader1.HasRows == true)
                {
                    lsclusterhead = objODBCDatareader1["clusterhead"].ToString();
                    lsregionalhead = objODBCDatareader1["regionhead"].ToString();
                    lszonalhead = objODBCDatareader1["zonalhead"].ToString();
                    lsbusinesshead = objODBCDatareader1["businesshead"].ToString();
                    lsclusterheadgid = objODBCDatareader1["clusterhead_gid"].ToString();
                    lsregionalheadgid = objODBCDatareader1["regionhead_gid"].ToString();
                    lszonalheadgid = objODBCDatareader1["zonalhead_gid"].ToString();
                    lsbusinessheadgid = objODBCDatareader1["businesshead_gid"].ToString();
                    lsbaselocationgid = objODBCDatareader1["baselocation_gid"].ToString();
                    lsbaselocationname = objODBCDatareader1["baselocation_name"].ToString();
                    lsclustergid = objODBCDatareader1["cluster_gid"].ToString();
                    lsclustername = objODBCDatareader1["cluster_name"].ToString();
                    lsregiongid = objODBCDatareader1["region_gid"].ToString();
                    lsregionname = objODBCDatareader1["region_name"].ToString();
                    lszonalgid = objODBCDatareader1["zonal_gid"].ToString();
                    lszonalname = objODBCDatareader1["zonal_name"].ToString();

                    msSQL = " insert into agr_mst_tsuprapplication(" +
                        " application_gid," +
                        " customer_urn," +
                        " customerref_name," +
                        " vertical_gid," +
                        " vertical_name," +
                        //" verticaltaggs_gid," +
                        //" verticaltaggs_name," +
                        " constitution_gid," +
                        " constitution_name," +
                        " onboarding_status, " +
                        //" businessunit_gid," +
                        //" businessunit_name," +
                        " sa_status," +
                        " saname_gid," +
                        " sa_name," +
                        " relationshipmanager_name," +
                        " relationshipmanager_gid," +
                        " drm_gid," +
                        " drm_name," +
                        " vernacular_language," +
                        " vernacularlanguage_gid," +
                        " contactpersonfirst_name," +
                        " contactpersonmiddle_name," +
                        " contactpersonlast_name," +
                        " designation_gid," +
                        " designation_type," +
                        " landline_no," +
                        " baselocation_gid," +
                       " baselocation_name," +
                       " cluster_gid," +
                       " cluster_name," +
                       " region_gid," +
                       " region_name," +
                       " zone_gid," +
                       " zone_name," +
                       " clustermanager_gid," +
                       " clustermanager_name," +
                       " regionalhead_name," +
                       " regionalhead_gid," +
                       " zonalhead_name," +
                       " zonalhead_gid," +
                       " businesshead_name," +
                       " businesshead_gid," +
                       " creditgroup_gid," +
                       " creditgroup_name," +
                       " program_gid," +
                       " program_name," +
                       " product_gid," +
                       " product_name," +
                       " variety_gid," +
                       " variety_name," +
                       " sector_name," +
                       " category_name," +
                       " botanical_name," +
                       " alternative_name," +
                     //" creditmanager_name," +
                     //" creditmanager_gid," +
                     //" zonalriskmanager_name," +
                     //" zonalriskmanager_gid," +
                     //" riskmanager_gid," +
                     //" riskmanager_name," +
                     //" headriskmonitoring_gid," +
                     //" headriskmonitoring_name," +
                     " status," +
                        " created_by," +
                        " created_date) values(" +
                          "'" + msGetGid + "'," +
                            "'" + values.customer_urn + "'," +
                            "'" + values.customer_name + "'," +
                            "'" + values.vertical_gid + "'," +
                            "'" + values.vertical_name + "'," +
                            //"'" + values.verticaltaggs_gid + "'," +
                            //"'" + values.verticaltaggs_name + "'," +
                            "'" + values.constitution_gid + "'," +
                            "'" + values.constitution_name + "'," +
                            "'" + values.onboarding_status + "'," +
                            //"'" + values.businessunit_gid + "'," +
                            //"'" + values.businessunit_name + "'," +
                            "'" + values.sa_status + "'," +
                            "'" + values.saname_gid + "'," +
                            "'" + values.sa_name + "'," +
                            "'" + lsemployee_name + "'," +
                            "'" + employee_gid + "'," +
                            "'" + lsdrm_gid + "'," +
                            "'" + lsdrm_name + "'," +
                            "'" + gsvernacular_language + "'," +
                            "'" + gsvernacularlanguage_gid + "'," +
                            "'" + values.contactpersonfirst_name + "'," +
                            "'" + values.contactpersonmiddle_name + "'," +
                            "'" + values.contactpersonlast_name + "'," +
                            "'" + values.designation_gid + "'," +
                            "'" + values.designation_type + "'," +
                            "'" + values.landline_no + "'," +
                            "'" + lsbaselocationgid + "'," +
                            "'" + lsbaselocationname + "'," +
                            "'" + lsclustergid + "'," +
                            "'" + lsclustername + "'," +
                            "'" + lsregiongid + "'," +
                            "'" + lsregionname + "'," +
                            "'" + lszonalgid + "'," +
                            "'" + lszonalname + "'," +
                            "'" + lsclusterheadgid + "'," +
                            "'" + lsclusterhead + "'," +
                            "'" + lsregionalhead + "'," +
                            "'" + lsregionalheadgid + "'," +
                            "'" + lszonalhead + "'," +
                            "'" + lszonalheadgid + "'," +
                            "'" + lsbusinesshead + "'," +
                            "'" + lsbusinessheadgid + "'," +
                            "'" + values.creditgroup_gid + "'," +
                            "'" + values.creditgroup_name + "'," +
                            "'" + values.program_gid + "'," +
                            "'" + values.program_name + "'," +
                            "'" + values.product_gid + "'," +
                            "'" + values.product_name + "'," +
                            "'" + values.variety_gid + "'," +
                            "'" + values.variety_name + "'," +
                            "'" + values.sector_name + "'," +
                            "'" + values.category_name + "'," +
                            "'" + values.botanical_name + "'," +
                            "'" + values.alternative_name + "'," +
                            "'Incomplete'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    objODBCDatareader1.Close();
                }
                else
                {
                    objODBCDatareader1.Close();
                    values.message = "Location / Customer/Supplier Type not Assigned for Business Approval";
                    values.status = false;
                    return;
                }

                if (mnResult != 0)
                {
                    //if (values.primaryvaluechain_list == null)
                    //{
                    //}
                    //else
                    //{
                    //    for (var i = 0; i < values.primaryvaluechain_list.Count; i++)
                    //    {


                    //        msGetGid1 = objcmnfunctions.GetMasterGID("CPRI");
                    //        msSQL = " insert into agr_mst_tsuprapplication2primaryvaluechain(" +
                    //                " application2primaryvaluechain_gid," +
                    //                " application_gid," +
                    //                " primaryvaluechain_name," +
                    //                " primaryvaluechain_gid," +
                    //                " created_by," +
                    //                " created_date)" +
                    //                " values(" +
                    //                "'" + msGetGid1 + "'," +
                    //                "'" + msGetGid + "'," +
                    //                "'" + values.primaryvaluechain_list[i].valuechain_name + "'," +
                    //                "'" + values.primaryvaluechain_list[i].valuechain_gid + "'," +
                    //                "'" + employee_gid + "'," +
                    //                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    //    }
                    //}
                    //if (values.secondaryvaluechain_list == null)
                    //{
                    //}
                    //else
                    //{
                    //    for (var j = 0; j < values.secondaryvaluechain_list.Count; j++)
                    //    {

                    //        msGetGid1 = objcmnfunctions.GetMasterGID("CSEC");
                    //        msSQL = " insert into agr_mst_tsuprapplication2secondaryvaluechain(" +
                    //                " application2secondaryvaluechain_gid," +
                    //                " application_gid," +
                    //                " secondaryvaluechain_name," +
                    //                " secondaryvaluechain_gid," +
                    //                " created_by," +
                    //                " created_date)" +
                    //                " values(" +
                    //                "'" + msGetGid1 + "'," +
                    //                "'" + msGetGid + "'," +
                    //                "'" + values.secondaryvaluechain_list[j].valuechain_name + "'," +
                    //                "'" + values.secondaryvaluechain_list[j].valuechain_gid + "'," +
                    //                "'" + employee_gid + "'," +
                    //                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    //    }
                    //}

                    msSQL = "update agr_mst_tsuprapplication2product set application_gid ='" + msGetGid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsuprapplication2contactno set application_gid='" + msGetGid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (mnResult != 0)
                    {
                        msSQL = "update agr_mst_tsuprapplication2email set application_gid='" + msGetGid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            msSQL = "update agr_mst_tsuprapplication2geneticcode set application_gid='" + msGetGid + "' where application_gid='" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {
                                msSQL = "insert into tmp_application(application_gid,employee_gid)values('" + msGetGid + "','" + employee_gid + "')";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                values.message = "General Information Saved successfully";
                                values.status = true;
                            }
                            else
                            {
                                values.message = "Error Occured while Saving Information";
                                values.status = false;
                            }
                        }
                        else
                        {
                            values.message = "Error Occured while Saving Information";
                            values.status = false;
                        }
                    }
                    else
                    {
                        values.message = "Error Occured while Saving Information";
                        values.status = false;
                    }
                }
                else
                {
                    values.message = "Error Occured while Saving Information";
                    values.status = false;
                }
            }
            else
            {
                if (lsapplication_gid == null || lsapplication_gid == "")
                {
                    lsapplication_gid = values.application_gid;
                }
                string lsclusterhead, lsregionalhead, lszonalhead, lsbusinesshead;
                string lsclusterheadgid, lsregionalheadgid, lszonalheadgid, lsbusinessheadgid;
                string lsbaselocationgid, lsclustergid, lsregiongid, lszonalgid;
                string lsbaselocationname, lsclustername, lsregionname, lszonalname;

                msSQL = " select distinct b.baselocation_gid,b.baselocation_name,c.cluster_gid,c.cluster_name,c.employee_name as clusterhead,c.vertical_name as clustervertical," +
                        " e.region_gid,e.region_name,e.employee_name as regionhead,g.zonal_gid,g.zonal_name,g.employee_name as zonalhead ," +
                        " c.employee_gid as clusterhead_gid,e.employee_gid as regionhead_gid, " +
                        " g.employee_gid as zonalhead_gid,h.employee_gid as businesshead_gid, " +
                        " h.employee_name as businesshead from hrm_mst_temployee a" +
                        " left join sys_mst_tcluster2baselocation b on b.baselocation_gid = a.baselocation_gid" +
                        " left  join sys_mst_tclusterhead c on b.cluster_gid = c.cluster_gid" +
                        " left join sys_mst_tregion2cluster d on c.cluster_gid = d.cluster_gid" +
                        " left join sys_mst_tregionhead e on d.region_gid = e.region_gid" +
                        " left join sys_mst_tzone2region f on f.region_gid = d.region_gid" +
                        " left join sys_mst_tzonalhead g on f.zone_gid = g.zonal_gid" +
                        " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid where a.employee_gid = '" + employee_gid + "' and" +
                        " c.vertical_gid = '" + values.vertical_gid + "'" +
                        " and e.vertical_gid = '" + values.vertical_gid + "' and " +
                        " g.vertical_gid = '" + values.vertical_gid + "' and h.vertical_gid = '" + values.vertical_gid + "'" +
                        " and c.program_gid = '" + values.program_gid + "' and e.program_gid = '" + values.program_gid + "' and " +
                        " g.program_gid = '" + values.program_gid + "' and h.program_gid = '" + values.program_gid + "' " +
                        " and e.status='Y' and g.status='Y' and h.status='Y' and c.status='Y'";
                objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader1.HasRows == true)
                {
                    lsclusterhead = objODBCDatareader1["clusterhead"].ToString();
                    lsregionalhead = objODBCDatareader1["regionhead"].ToString();
                    lszonalhead = objODBCDatareader1["zonalhead"].ToString();
                    lsbusinesshead = objODBCDatareader1["businesshead"].ToString();
                    lsclusterheadgid = objODBCDatareader1["clusterhead_gid"].ToString();
                    lsregionalheadgid = objODBCDatareader1["regionhead_gid"].ToString();
                    lszonalheadgid = objODBCDatareader1["zonalhead_gid"].ToString();
                    lsbusinessheadgid = objODBCDatareader1["businesshead_gid"].ToString();
                    lsbaselocationgid = objODBCDatareader1["baselocation_gid"].ToString();
                    lsbaselocationname = objODBCDatareader1["baselocation_name"].ToString();
                    lsclustergid = objODBCDatareader1["cluster_gid"].ToString();
                    lsclustername = objODBCDatareader1["cluster_name"].ToString();
                    lsregiongid = objODBCDatareader1["region_gid"].ToString();
                    lsregionname = objODBCDatareader1["region_name"].ToString();
                    lszonalgid = objODBCDatareader1["zonal_gid"].ToString();
                    lszonalname = objODBCDatareader1["zonal_name"].ToString();
                    msSQL = " update agr_mst_tsuprapplication set " +
                          " application_no='" + lsapp_refno + "'," +
                           " customer_urn='" + values.customer_urn + "'," +
                           " customerref_name='" + values.customer_name + "'," +
                           " vertical_gid='" + values.vertical_gid + "'," +
                           " vertical_name='" + values.vertical_name + "'," +
                           " program_gid='" + values.program_gid + "'," +
                           " program_name='" + values.program_name + "'," +
                           //" verticaltaggs_gid='" + values.verticaltaggs_gid + "'," +
                           //" verticaltaggs_name='" + values.verticaltaggs_name + "'," +
                           " constitution_gid='" + values.constitution_gid + "'," +
                           " constitution_name='" + values.constitution_name + "'," +
                           " onboarding_status='" + values.onboarding_status + "'," +
                           //" businessunit_gid='" + values.businessunit_gid + "'," +
                           //" businessunit_name='" + values.businessunit_name + "'," +
                           " sa_status='" + values.sa_status + "'," +
                           " sa_name='" + values.sa_name + "'," +
                           " saname_gid='" + values.saname_gid + "'," +
                           " vernacularlanguage_gid='" + gsvernacularlanguage_gid + "'," +
                           " vernacular_language='" + gsvernacular_language + "'," +
                           " contactpersonfirst_name='" + values.contactpersonfirst_name + "'," +
                           " contactpersonmiddle_name='" + values.contactpersonmiddle_name + "'," +
                           " contactpersonlast_name='" + values.contactpersonlast_name + "'," +
                           " designation_gid='" + values.designation_gid + "'," +
                           " designation_type='" + values.designation_type + "'," +
                           " landline_no='" + values.landline_no + "'," +
                           " cluster_gid='" + lsclustergid + "'," +
                           " cluster_name='" + lsclustername + "'," +
                           " region_gid='" + lsregiongid + "'," +
                           " region_name='" + lsregionname + "'," +
                           " zone_gid='" + lszonalgid + "'," +
                           " zone_name='" + lszonalname + "'," +
                           " drm_gid='" + lsdrm_gid + "'," +
                           " drm_name='" + lsdrm_name + "'," +
                           " clustermanager_gid='" + lsclusterheadgid + "'," +
                           " clustermanager_name='" + lsclusterhead + "'," +
                           " regionalhead_name='" + lsregionalhead + "'," +
                           " regionalhead_gid='" + lsregionalheadgid + "'," +
                           " zonalhead_name='" + lszonalhead + "'," +
                           " zonalhead_gid='" + lszonalheadgid + "'," +
                           " businesshead_name='" + lsbusinesshead + "'," +
                           " businesshead_gid='" + lsbusinessheadgid + "'," +
                           " creditgroup_gid= '" + values.creditgroup_gid + "'," +
                           " creditgroup_name='" + values.creditgroup_name + "'," +
                           " product_gid= '" + values.product_gid + "'," +
                           " product_name='" + values.product_name + "'," +
                           " variety_gid= '" + values.variety_gid + "'," +
                           " variety_name='" + values.variety_name + "'," +
                           " sector_name= '" + values.sector_name + "'," +
                           " category_name='" + values.category_name + "'," +
                           " botanical_name= '" + values.botanical_name + "'," +
                           " alternative_name='" + values.alternative_name + "'," +
                           " status = 'Incomplete'," +
                           " updated_by='" + employee_gid + "'," +
                           " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           " where application_gid='" + lsapplication_gid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    objODBCDatareader1.Close();
                }
                else
                {
                    objODBCDatareader1.Close();
                    values.message = "Location / Customer/Supplier Type not Assigned for Business Approval";
                    values.status = false;
                    return;
                }

                if (mnResult != 0)
                {
                    //if (values.primaryvaluechain_list == null)
                    //{
                    //}
                    //else
                    //{
                    //    msSQL = "delete from  agr_mst_tsuprapplication2primaryvaluechain where application_gid='" + lsapplication_gid + "'";
                    //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    //    for (var i = 0; i < values.primaryvaluechain_list.Count; i++)
                    //    {

                    //        msGetGid1 = objcmnfunctions.GetMasterGID("CPRI");
                    //        msSQL = " insert into agr_mst_tsuprapplication2primaryvaluechain(" +
                    //                " application2primaryvaluechain_gid," +
                    //                " application_gid," +
                    //                " primaryvaluechain_name," +
                    //                " primaryvaluechain_gid," +
                    //                " created_by," +
                    //                " created_date)" +
                    //                " values(" +
                    //                "'" + msGetGid1 + "'," +
                    //                "'" + lsapplication_gid + "'," +
                    //                "'" + values.primaryvaluechain_list[i].valuechain_name + "'," +
                    //                "'" + values.primaryvaluechain_list[i].valuechain_gid + "'," +
                    //                "'" + employee_gid + "'," +
                    //                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    //    }
                    //}
                    //if (values.secondaryvaluechain_list == null)
                    //{
                    //}
                    //else
                    //{
                    //    msSQL = "delete from  agr_mst_tsuprapplication2secondaryvaluechain where application_gid='" + lsapplication_gid + "'";
                    //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    //    for (var j = 0; j < values.secondaryvaluechain_list.Count; j++)
                    //    {


                    //        msGetGid1 = objcmnfunctions.GetMasterGID("CSEC");
                    //        msSQL = " insert into agr_mst_tsuprapplication2secondaryvaluechain(" +
                    //                " application2secondaryvaluechain_gid," +
                    //                " application_gid," +
                    //                " secondaryvaluechain_name," +
                    //                " secondaryvaluechain_gid," +
                    //                " created_by," +
                    //                " created_date)" +
                    //                " values(" +
                    //                "'" + msGetGid1 + "'," +
                    //                "'" + lsapplication_gid + "'," +
                    //                "'" + values.secondaryvaluechain_list[j].valuechain_name + "'," +
                    //                "'" + values.secondaryvaluechain_list[j].valuechain_gid + "'," +
                    //                "'" + employee_gid + "'," +
                    //                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    //    }
                    //}

                    msSQL = "update agr_mst_tsuprapplication2product set application_gid ='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsuprapplication2contactno set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsuprapplication2email set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsuprapplication2geneticcode set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.message = "General Information Saved successfully";
                    values.status = true;
                }
                else
                {
                    values.message = "Error Occured while Saving Information";
                    values.status = false;
                }
            }

        }

        public void DaSubmitGeneralDtl(MdlMstApplicationAdd values, string employee_gid)
        {

            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsapplication_gid == "" || lsapplication_gid == null)
            {
                lsapplication_gid = values.application_gid;
            }
            msSQL = "select count(geneticcode_gid) from ocs_mst_tgeneticcode where status='Y'";
            string lsmastercount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select count(application2geneticcode_gid) from agr_mst_tsuprapplication2geneticcode where application_gid='" + employee_gid + "' or application_gid='" + lsapplication_gid + "'";
            string lsgeneticcount = objdbconn.GetExecuteScalar(msSQL);
            if (lsmastercount == lsgeneticcount)
            {

                msSQL = "select application_gid from agr_mst_tsuprapplication2contactno where (application_gid='" + employee_gid + "' or application_gid='" + lsapplication_gid + "') and primary_mobileno='Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Kindly Add Primary Mobile No";
                    return;
                }


                msSQL = "select application_gid from agr_mst_tsuprapplication2email where (application_gid='" + employee_gid + "' or application_gid='" + lsapplication_gid + "') and primary_emailaddress='Yes'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    values.status = false;
                    values.message = "Kindly Add Primary Email Adddress";
                    return;
                }

                msSQL = "select vertical_refno from ocs_mst_tvertical where vertical_gid='" + values.vertical_gid + "'";
                string lsvertical_refno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select entity_gid from ocs_mst_tvertical where vertical_gid='" + values.vertical_gid + "'";
                string lsentity_gid = objdbconn.GetExecuteScalar(msSQL);

                //msSQL = "select entity_code from adm_mst_tentity where entity_gid='" + lsentity_gid + "'";
                //string lsentity_code = objdbconn.GetExecuteScalar(msSQL);
                string lsentity_code = "SA";

                //string lsapp_refno = "ARN" + lsentity_code + lsvertical_refno + DateTime.Now.ToString("ddMMyyyy");

                //string msGETRef = objcmnfunctions.GetMasterGID("APP");
                //msGETRef = msGETRef.Replace("APP", "");

                //lsapp_refno = lsapp_refno + msGETRef + "IN01";

                msGetGid = objcmnfunctions.GetMasterGID("APPC");
                string gsvernacularlanguage_gid = string.Empty;
                string gsvernacular_language = string.Empty;
                if (values.vernacularlanguage_list != null)
                {
                    for (var i = 0; i < values.vernacularlanguage_list.Count; i++)
                    {
                        gsvernacularlanguage_gid += values.vernacularlanguage_list[i].vernacularlanguage_gid + ",";
                        gsvernacular_language += values.vernacularlanguage_list[i].vernacular_language + ",";

                    }
                    gsvernacularlanguage_gid = gsvernacularlanguage_gid.TrimEnd(',');
                    gsvernacular_language = gsvernacular_language.TrimEnd(',');
                }

                msSQL = "select concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name" +
                        " from hrm_mst_temployee a" +
                        " left join adm_mst_tuser b on a.user_gid=b.user_gid" +
                        " where a.employee_gid='" + employee_gid + "'";
                lsemployee_name = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one ,a.employeereporting_to " +
                               "  from adm_mst_tmodule2employee a " +
                               "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                               "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                                "  where a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in " +
                                 "  (select modulereportingto_gid from adm_mst_tcompany)) " +
                    " and g.user_status = 'Y' and a.employee_gid ='" + employee_gid + "' group by a.employee_gid ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsdrm_gid = objODBCDatareader["employeereporting_to"].ToString();
                    lsdrm_name = objODBCDatareader["level_one"].ToString();
                }
                objODBCDatareader.Close();

                if (values.onboarding_status == "Direct")
                {
                    msSQL = " SELECT creditgroup_name,creditmapping_gid from ocs_mst_tcreditmapping where creditgroup_name='" + getAutoApprovalClass.CreditGroupName + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.creditgroup_gid = objODBCDatareader["creditmapping_gid"].ToString();
                        values.creditgroup_name = objODBCDatareader["creditgroup_name"].ToString();
                    }
                    objODBCDatareader.Close();
                }

                if (lsapplication_gid == "" || lsapplication_gid == null)
                {
                    string lsclusterhead, lsregionalhead, lszonalhead, lsbusinesshead;
                    string lsclusterheadgid, lsregionalheadgid, lszonalheadgid, lsbusinessheadgid;
                    string lsbaselocationgid, lsclustergid, lsregiongid, lszonalgid;
                    string lsbaselocationname, lsclustername, lsregionname, lszonalname;
                    msSQL = " select distinct b.baselocation_gid,b.baselocation_name,c.cluster_gid,c.cluster_name,c.employee_name as clusterhead,c.vertical_name as clustervertical," +
                          " e.region_gid,e.region_name,e.employee_name as regionhead,g.zonal_gid,g.zonal_name,g.employee_name as zonalhead ," +
                          " c.employee_gid as clusterhead_gid,e.employee_gid as regionhead_gid, " +
                          " g.employee_gid as zonalhead_gid,h.employee_gid as businesshead_gid, " +
                          " h.employee_name as businesshead from hrm_mst_temployee a" +
                          " left join sys_mst_tcluster2baselocation b on b.baselocation_gid = a.baselocation_gid" +
                          " left  join sys_mst_tclusterhead c on b.cluster_gid = c.cluster_gid" +
                          " left join sys_mst_tregion2cluster d on c.cluster_gid = d.cluster_gid" +
                          " left join sys_mst_tregionhead e on d.region_gid = e.region_gid" +
                          " left join sys_mst_tzone2region f on f.region_gid = d.region_gid" +
                          " left join sys_mst_tzonalhead g on f.zone_gid = g.zonal_gid" +
                          " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid where a.employee_gid = '" + employee_gid + "' and" +
                          " c.vertical_gid = '" + values.vertical_gid + "'" +
                          " and e.vertical_gid = '" + values.vertical_gid + "' and " +
                          " g.vertical_gid = '" + values.vertical_gid + "' and h.vertical_gid = '" + values.vertical_gid + "'" +
                          " and c.program_gid = '" + values.program_gid + "' and e.program_gid = '" + values.program_gid + "' and " +
                          " g.program_gid = '" + values.program_gid + "' and h.program_gid = '" + values.program_gid + "' " +
                          " and e.status='Y' and g.status='Y' and h.status='Y' and c.status='Y'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        lsclusterhead = objODBCDatareader1["clusterhead"].ToString();
                        lsregionalhead = objODBCDatareader1["regionhead"].ToString();
                        lszonalhead = objODBCDatareader1["zonalhead"].ToString();
                        lsbusinesshead = objODBCDatareader1["businesshead"].ToString();
                        lsclusterheadgid = objODBCDatareader1["clusterhead_gid"].ToString();
                        lsregionalheadgid = objODBCDatareader1["regionhead_gid"].ToString();
                        lszonalheadgid = objODBCDatareader1["zonalhead_gid"].ToString();
                        lsbusinessheadgid = objODBCDatareader1["businesshead_gid"].ToString();
                        lsbaselocationgid = objODBCDatareader1["baselocation_gid"].ToString();
                        lsbaselocationname = objODBCDatareader1["baselocation_name"].ToString();
                        lsclustergid = objODBCDatareader1["cluster_gid"].ToString();
                        lsclustername = objODBCDatareader1["cluster_name"].ToString();
                        lsregiongid = objODBCDatareader1["region_gid"].ToString();
                        lsregionname = objODBCDatareader1["region_name"].ToString();
                        lszonalgid = objODBCDatareader1["zonal_gid"].ToString();
                        lszonalname = objODBCDatareader1["zonal_name"].ToString();
                        msSQL = " insert into agr_mst_tsuprapplication(" +
                        " application_gid," + 
                        " customer_urn," +
                        " customerref_name," +
                        " vertical_gid," +
                        " vertical_name," +
                        //" verticaltaggs_gid," +
                        //" verticaltaggs_name," +
                        " constitution_gid," +
                        " constitution_name," +
                        " onboarding_status, " +
                        //" businessunit_gid," +
                        //" businessunit_name," +
                        " sa_status," +
                        " saname_gid," +
                        " sa_name," +
                        " relationshipmanager_name," +
                        " relationshipmanager_gid," +
                        " drm_gid," +
                        " drm_name," +
                        " vernacular_language," +
                        " vernacularlanguage_gid," +
                        " contactpersonfirst_name," +
                        " contactpersonmiddle_name," +
                        " contactpersonlast_name," +
                        " designation_gid," +
                        " designation_type," +
                        " landline_no," +
                        " baselocation_gid," +
                        " baselocation_name," +
                        " cluster_gid," +
                        " cluster_name," +
                        " region_gid," +
                        " region_name," +
                        " zone_gid," +
                        " zone_name," +
                        " clustermanager_gid," +
                        " clustermanager_name," +
                        " zonalhead_name," +
                        " zonalhead_gid," +
                        " regionalhead_name," +
                        " regionalhead_gid," +
                        " businesshead_name," +
                        " businesshead_gid," +
                        " creditgroup_gid," +
                        " creditgroup_name," +
                        " program_gid," +
                        " program_name," +
                    //" product_gid," +
                    //" product_name," +
                    //" variety_gid," +
                    //" variety_name," +
                    //" sector_name," +
                    //" category_name," +
                    //" botanical_name," +
                    //" alternative_name," +
                    //" creditmanager_name," +
                    //" creditmanager_gid," +
                    //" zonalriskmanager_name," +
                    //" zonalriskmanager_gid," +
                    //" riskmanager_gid," +
                    //" riskmanager_name," +
                    //" headriskmonitoring_gid," +
                    //" headriskmonitoring_name," +
                    " status," +
                        " created_by," +
                        " created_date) values(" +
                          "'" + msGetGid + "'," + 
                            "'" + values.customer_urn + "'," +
                            "'" + values.customer_name + "'," +
                            "'" + values.vertical_gid + "'," +
                            "'" + values.vertical_name + "'," +
                            //"'" + values.verticaltaggs_gid + "'," +
                            //"'" + values.verticaltaggs_name + "'," +
                            "'" + values.constitution_gid + "'," +
                            "'" + values.constitution_name + "'," +
                            "'" + values.onboarding_status + "'," +
                            //"'" + values.businessunit_gid + "'," +
                            //"'" + values.businessunit_name + "'," +
                            "'" + values.sa_status + "'," +
                            "'" + values.saname_gid + "'," +
                            "'" + values.sa_name + "'," +
                            "'" + lsemployee_name + "'," +
                            "'" + employee_gid + "'," +
                            "'" + lsdrm_gid + "'," +
                            "'" + lsdrm_name + "'," +
                            "'" + gsvernacular_language + "'," +
                            "'" + gsvernacularlanguage_gid + "'," +
                            "'" + values.contactpersonfirst_name + "'," +
                            "'" + values.contactpersonmiddle_name + "'," +
                            "'" + values.contactpersonlast_name + "'," +
                            "'" + values.designation_gid + "'," +
                            "'" + values.designation_type + "'," +
                            "'" + values.landline_no + "'," +
                            "'" + lsbaselocationgid + "'," +
                            "'" + lsbaselocationname + "'," +
                            "'" + lsclustergid + "'," +
                            "'" + lsclustername + "'," +
                            "'" + lsregiongid + "'," +
                            "'" + lsregionname + "'," +
                            "'" + lszonalgid + "'," +
                            "'" + lszonalname + "'," +
                            "'" + lsclusterheadgid + "'," +
                            "'" + lsclusterhead + "'," +
                            "'" + lszonalhead + "'," +
                            "'" + lszonalheadgid + "'," +
                            "'" + lsregionalhead + "'," +
                            "'" + lsregionalheadgid + "'," +
                            "'" + lsbusinesshead + "'," +
                            "'" + lsbusinessheadgid + "'," +
                            "'" + values.creditgroup_gid + "'," +
                            "'" + values.creditgroup_name + "'," +
                            "'" + values.program_gid + "'," +
                            "'" + values.program_name + "'," +
                            //"'" + values.product_gid + "'," +
                            //"'" + values.product_name + "'," +
                            //"'" + values.variety_gid + "'," +
                            //"'" + values.variety_name + "'," +
                            //"'" + values.sector_name + "'," +
                            //"'" + values.category_name + "'," +
                            //"'" + values.botanical_name + "'," +
                            //"'" + values.alternative_name + "'," +
                            "'Completed'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        objODBCDatareader1.Close();
                    }
                    else
                    {
                        objODBCDatareader1.Close();
                        values.message = "Location / Customer/Supplier Type not Assigned for Business Approval";
                        values.status = false;
                        return;
                    }

                    if (mnResult != 0)
                    {
                        //if (values.primaryvaluechain_list == null)
                        //{
                        //}
                        //else
                        //{
                        //    for (var i = 0; i < values.primaryvaluechain_list.Count; i++)
                        //    {

                        //        msGetGid1 = objcmnfunctions.GetMasterGID("CPRI");
                        //        msSQL = " insert into agr_mst_tsuprapplication2primaryvaluechain(" +
                        //                " application2primaryvaluechain_gid," +
                        //                " application_gid," +
                        //                " primaryvaluechain_name," +
                        //                " primaryvaluechain_gid," +
                        //                " created_by," +
                        //                " created_date)" +
                        //                " values(" +
                        //                "'" + msGetGid1 + "'," +
                        //                "'" + msGetGid + "'," +
                        //                "'" + values.primaryvaluechain_list[i].valuechain_name + "'," +
                        //                "'" + values.primaryvaluechain_list[i].valuechain_gid + "'," +
                        //                "'" + employee_gid + "'," +
                        //                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        //    }
                        //}
                        //if (values.secondaryvaluechain_list == null)
                        //{
                        //}
                        //else
                        //{
                        //    for (var j = 0; j < values.secondaryvaluechain_list.Count; j++)
                        //    {

                        //        msGetGid1 = objcmnfunctions.GetMasterGID("CSEC");
                        //        msSQL = " insert into agr_mst_tsuprapplication2secondaryvaluechain(" +
                        //                " application2secondaryvaluechain_gid," +
                        //                " application_gid," +
                        //                " secondaryvaluechain_name," +
                        //                " secondaryvaluechain_gid," +
                        //                " created_by," +
                        //                " created_date)" +
                        //                " values(" +
                        //                "'" + msGetGid1 + "'," +
                        //                "'" + msGetGid + "'," +
                        //                "'" + values.secondaryvaluechain_list[j].valuechain_name + "'," +
                        //                "'" + values.secondaryvaluechain_list[j].valuechain_gid + "'," +
                        //                "'" + employee_gid + "'," +
                        //                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        //    }
                        //}


                        msSQL = "update agr_mst_tsuprapplication2product set application_gid ='" + msGetGid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tsuprapplication2contactno set application_gid='" + msGetGid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult != 0)
                        {
                            msSQL = "update agr_mst_tsuprapplication2email set application_gid='" + msGetGid + "' where application_gid='" + employee_gid + "'";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            if (mnResult != 0)
                            {
                                msSQL = "update agr_mst_tsuprapplication2geneticcode set application_gid='" + msGetGid + "' where application_gid='" + employee_gid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                if (mnResult != 0)
                                {
                                    msSQL = "insert into tmp_application(application_gid,employee_gid)values('" + msGetGid + "','" + employee_gid + "')";
                                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                    values.application_no = lsapp_refno;

                                    values.message = "General Information Saved successfully";
                                    values.status = true;
                                }
                                else
                                {
                                    values.message = "Error Occured while Saving Information";
                                    values.status = false;
                                }
                            }
                            else
                            {
                                values.message = "Error Occured while Saving Information";
                                values.status = false;
                            }
                        }
                        else
                        {
                            values.message = "Error Occured while Saving Information";
                            values.status = false;
                        }
                    }
                    else
                    {
                        values.message = "Location / Customer/Supplier Type not Assigned for Business Approval";
                        values.status = false;
                        return;
                    }
                }
                else
                {
                    string lsclusterhead, lsregionalhead, lszonalhead, lsbusinesshead;
                    string lsclusterheadgid, lsregionalheadgid, lszonalheadgid, lsbusinessheadgid;
                    string lsbaselocationgid, lsclustergid, lsregiongid, lszonalgid;
                    string lsbaselocationname, lsclustername, lsregionname, lszonalname;
                    msSQL = " select distinct b.baselocation_gid,b.baselocation_name,c.cluster_gid, c.cluster_name,c.employee_name as clusterhead,c.vertical_name as clustervertical," +
                        " e.region_gid,e.region_name,e.employee_name as regionhead,g.zonal_gid,g.zonal_name,g.employee_name as zonalhead ," +
                        " c.employee_gid as clusterhead_gid,e.employee_gid as regionhead_gid, " +
                        " g.employee_gid as zonalhead_gid,h.employee_gid as businesshead_gid, " +
                        " h.employee_name as businesshead from hrm_mst_temployee a" +
                        " left join sys_mst_tcluster2baselocation b on b.baselocation_gid = a.baselocation_gid" +
                        " left  join sys_mst_tclusterhead c on b.cluster_gid = c.cluster_gid" +
                        " left join sys_mst_tregion2cluster d on c.cluster_gid = d.cluster_gid" +
                        " left join sys_mst_tregionhead e on d.region_gid = e.region_gid" +
                        " left join sys_mst_tzone2region f on f.region_gid = d.region_gid" +
                        " left join sys_mst_tzonalhead g on f.zone_gid = g.zonal_gid" +
                        " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid where a.employee_gid = '" + employee_gid + "' and" +
                        " c.vertical_gid = '" + values.vertical_gid + "'" +
                        " and e.vertical_gid = '" + values.vertical_gid + "' and " +
                        " g.vertical_gid = '" + values.vertical_gid + "' and h.vertical_gid = '" + values.vertical_gid + "'" +
                        " and c.program_gid = '" + values.program_gid + "' and e.program_gid = '" + values.program_gid + "' and " +
                        " g.program_gid = '" + values.program_gid + "' and h.program_gid = '" + values.program_gid + "' " +
                        " and e.status='Y' and g.status='Y' and h.status='Y' and c.status='Y'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == true)
                    {
                        lsclusterhead = objODBCDatareader1["clusterhead"].ToString();
                        lsregionalhead = objODBCDatareader1["regionhead"].ToString();
                        lszonalhead = objODBCDatareader1["zonalhead"].ToString();
                        lsbusinesshead = objODBCDatareader1["businesshead"].ToString();
                        lsclusterheadgid = objODBCDatareader1["clusterhead_gid"].ToString();
                        lsregionalheadgid = objODBCDatareader1["regionhead_gid"].ToString();
                        lszonalheadgid = objODBCDatareader1["zonalhead_gid"].ToString();
                        lsbusinessheadgid = objODBCDatareader1["businesshead_gid"].ToString();
                        lsbaselocationgid = objODBCDatareader1["baselocation_gid"].ToString();
                        lsbaselocationname = objODBCDatareader1["baselocation_name"].ToString();
                        lsclustergid = objODBCDatareader1["cluster_gid"].ToString();
                        lsclustername = objODBCDatareader1["cluster_name"].ToString();
                        lsregiongid = objODBCDatareader1["region_gid"].ToString();
                        lsregionname = objODBCDatareader1["region_name"].ToString();
                        lszonalgid = objODBCDatareader1["zonal_gid"].ToString();
                        lszonalname = objODBCDatareader1["zonal_name"].ToString();
                        msSQL = " update agr_mst_tsuprapplication set " +
                          " application_no='" + lsapp_refno + "'," +
                           " customer_urn='" + values.customer_urn + "'," +
                           " customerref_name='" + values.customer_name + "'," +
                           " vertical_gid='" + values.vertical_gid + "'," +
                           " vertical_name='" + values.vertical_name + "'," +
                           " program_gid='" + values.program_gid + "'," +
                           " program_name='" + values.program_name + "'," +
                           //" verticaltaggs_gid='" + values.verticaltaggs_gid + "'," +
                           //" verticaltaggs_name='" + values.verticaltaggs_name + "'," +
                           " constitution_gid='" + values.constitution_gid + "'," +
                           " constitution_name='" + values.constitution_name + "'," +
                           " onboarding_status='" + values.onboarding_status + "'," +
                           //" businessunit_gid='" + values.businessunit_gid + "'," +
                           //" businessunit_name='" + values.businessunit_name + "'," +
                           " sa_status='" + values.sa_status + "'," +
                           " sa_name='" + values.sa_name + "'," +
                           " saname_gid='" + values.saname_gid + "'," +
                           " vernacularlanguage_gid='" + gsvernacularlanguage_gid + "'," +
                           " vernacular_language='" + gsvernacular_language + "'," +
                           " contactpersonfirst_name='" + values.contactpersonfirst_name + "'," +
                           " contactpersonmiddle_name='" + values.contactpersonmiddle_name + "'," +
                           " contactpersonlast_name='" + values.contactpersonlast_name + "'," +
                           " designation_gid='" + values.designation_gid + "'," +
                           " designation_type='" + values.designation_type + "'," +
                           " landline_no='" + values.landline_no + "'," +
                           " cluster_gid='" + lsclustergid + "'," +
                           " cluster_name='" + lsclustername + "'," +
                           " region_gid='" + lsregiongid + "'," +
                           " region_name='" + lsregionname + "'," +
                           " zone_gid='" + lszonalgid + "'," +
                           " zone_name='" + lszonalname + "'," +
                           " drm_gid='" + lsdrm_gid + "'," +
                           " drm_name='" + lsdrm_name + "'," +
                           " clustermanager_gid='" + lsclusterheadgid + "'," +
                           " clustermanager_name='" + lsclusterhead + "'," +
                           " zonalhead_name='" + lszonalhead + "'," +
                           " zonalhead_gid='" + lszonalheadgid + "'," +
                           " regionalhead_name='" + lsregionalhead + "'," +
                           " regionalhead_gid='" + lsregionalheadgid + "'," +
                           " businesshead_name='" + lsbusinesshead + "'," +
                           " businesshead_gid='" + lsbusinessheadgid + "'," +
                           " creditgroup_gid= '" + values.creditgroup_gid + "'," +
                           " creditgroup_name= '" + values.creditgroup_name + "'," +
                           //" product_gid= '" + values.product_gid + "'," +
                           //" product_name='" + values.product_name + "'," +
                           //" variety_gid= '" + values.variety_gid + "'," +
                           //" variety_name='" + values.variety_name + "'," +
                           //" sector_name= '" + values.sector_name + "'," +
                           //" category_name='" + values.category_name + "'," +
                           //" botanical_name= '" + values.botanical_name + "'," +
                           //" alternative_name='" + values.alternative_name + "'," +
                           " status = 'Completed'," +
                           " updated_by='" + employee_gid + "'," +
                           " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           " where application_gid='" + lsapplication_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        objODBCDatareader1.Close();
                    }
                    else
                    {
                        objODBCDatareader1.Close();
                        values.message = "Location / Customer/Supplier Type not Assigned for Business Approval";
                        values.status = false;
                        return;
                    }
                    if (mnResult != 0)
                    {
                        //if (values.primaryvaluechain_list == null)
                        //{
                        //}
                        //else
                        //{
                        //    msSQL = "delete from  agr_mst_tsuprapplication2primaryvaluechain where application_gid='" + lsapplication_gid + "'";
                        //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        //    for (var i = 0; i < values.primaryvaluechain_list.Count; i++)
                        //    {


                        //        msGetGid1 = objcmnfunctions.GetMasterGID("CPRI");
                        //        msSQL = " insert into agr_mst_tsuprapplication2primaryvaluechain(" +
                        //                " application2primaryvaluechain_gid," +
                        //                " application_gid," +
                        //                " primaryvaluechain_name," +
                        //                " primaryvaluechain_gid," +
                        //                " created_by," +
                        //                " created_date)" +
                        //                " values(" +
                        //                "'" + msGetGid1 + "'," +
                        //                "'" + lsapplication_gid + "'," +
                        //                "'" + values.primaryvaluechain_list[i].valuechain_name + "'," +
                        //                "'" + values.primaryvaluechain_list[i].valuechain_gid + "'," +
                        //                "'" + employee_gid + "'," +
                        //                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        //    }
                        //}
                        //if (values.secondaryvaluechain_list == null)
                        //{
                        //}
                        //else
                        //{
                        //    msSQL = "delete from  agr_mst_tsuprapplication2secondaryvaluechain where application_gid='" + lsapplication_gid + "'";
                        //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        //    for (var j = 0; j < values.secondaryvaluechain_list.Count; j++)
                        //    {

                        //        msGetGid1 = objcmnfunctions.GetMasterGID("CSEC");
                        //        msSQL = " insert into agr_mst_tsuprapplication2secondaryvaluechain(" +
                        //                " application2secondaryvaluechain_gid," +
                        //                " application_gid," +
                        //                " secondaryvaluechain_name," +
                        //                " secondaryvaluechain_gid," +
                        //                " created_by," +
                        //                " created_date)" +
                        //                " values(" +
                        //                "'" + msGetGid1 + "'," +
                        //                "'" + lsapplication_gid + "'," +
                        //                "'" + values.secondaryvaluechain_list[j].valuechain_name + "'," +
                        //                "'" + values.secondaryvaluechain_list[j].valuechain_gid + "'," +
                        //                "'" + employee_gid + "'," +
                        //                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        //        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        //    }
                        //}

                        msSQL = "update agr_mst_tsuprapplication2product set application_gid ='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tsuprapplication2contactno set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tsuprapplication2email set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tsuprapplication2geneticcode set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        values.message = "General Information Submitted Successfully";
                        values.status = true;
                    }
                    else
                    {
                        values.message = "Error Occured While Submitting Information";
                        values.status = false;
                    }

                }
            }
            else
            {
                values.message = "Kindly Add all Genetic details";
                values.status = false;
            }

        }
        public bool DaPostMobileNo(string employee_gid, MdlMstMobileNo values)
        {
            msSQL = "select primary_mobileno from agr_mst_tsuprapplication2contactno where primary_mobileno='Yes' and application_gid='" + employee_gid + "'";
            string lsprimary_mobileno = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_mobileno == (values.primary_mobileno))
            {

                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                return false;
            }
            msSQL = "select application2contact_gid from agr_mst_tsuprapplication2contactno where mobile_no='" + values.mobile_no + "' " +
                " and application_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Mobile Number Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("A2CN");
            msSQL = " insert into agr_mst_tsuprapplication2contactno(" +
                    " application2contact_gid," +
                    " application_gid," +
                    " mobile_no," +
                    " primary_mobileno," +
                    " whatsapp_mobileno," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.mobile_no + "'," +
                    "'" + values.primary_mobileno + "'," +
                    "'" + values.whatsapp_mobileno + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Mobile Number Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }
        public void DaGetAppMobileNoList(string employee_gid, MdlMstMobileNo values)
        {
            msSQL = "select mobile_no,application2contact_gid,primary_mobileno,whatsapp_mobileno from agr_mst_tsuprapplication2contactno where " +
              " application_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstmobileno_list = new List<mstmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstmobileno_list.Add(new mstmobileno_list
                    {
                        application2contact_gid = (dr_datarow["application2contact_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_mobileno = (dr_datarow["primary_mobileno"].ToString()),
                        whatsapp_mobileno = (dr_datarow["whatsapp_mobileno"].ToString()),
                    });
                }
                values.mstmobileno_list = getmstmobileno_list;
            }
            dt_datatable.Dispose();
        }
        public void DaDeleteMobileNo(string application2contact_gid, MdlMstMobileNo values)
        {
            msSQL = "delete from agr_mst_tsuprapplication2contactno where application2contact_gid='" + application2contact_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Mobile Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        public bool DaPostEmailAddress(string employee_gid, MdlMstEmailAddress values)
        {
            msSQL = "select primary_emailaddress from agr_mst_tsuprapplication2email where primary_emailaddress='Yes' and application_gid='" + employee_gid + "'";
            string lsprimary_emailaddress = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_emailaddress == (values.primary_emailaddress))
            {

                values.status = false;
                values.message = "Already Primary Email Address Added";
                return false;
            }
            msSQL = "select application2email_gid from agr_mst_tsuprapplication2email where email_address='" + values.email_address + "' and application_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Email Address Added";
                return false;
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("A2EA");
            msSQL = " insert into agr_mst_tsuprapplication2email(" +
                    " application2email_gid," +
                    " application_gid," +
                    " email_address," +
                    " primary_emailaddress," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.email_address + "'," +
                    "'" + values.primary_emailaddress + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Email Address Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetAppEmailAddressList(string employee_gid, MdlMstEmailAddress values)
        {
            msSQL = "select email_address,application2email_gid,primary_emailaddress from agr_mst_tsuprapplication2email where " +
              " application_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstemailaddress_list = new List<mstemailaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstemailaddress_list.Add(new mstemailaddress_list
                    {
                        application2email_gid = (dr_datarow["application2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_emailaddress = (dr_datarow["primary_emailaddress"].ToString())
                    });
                }
                values.mstemailaddress_list = getmstemailaddress_list;
            }
            dt_datatable.Dispose();
            values.status = true;
        }

        public void DaDeleteEmailAddress(string application2email_gid, MdlMstEmailAddress values)
        {
            msSQL = "delete from agr_mst_tsuprapplication2email where application2email_gid='" + application2email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Email Address Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }
        public void DaSocialAndTradeCapitalSave(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " update agr_mst_tsuprapplication set ";
            if (values.social_capital == null || values.social_capital == "")
            {
                msSQL += " social_capital='',";
            }
            else
            {
                msSQL += " social_capital='" + values.social_capital.Replace("'", "") + "',";

            }
            if (values.trade_capital == null || values.trade_capital == "")
            {
                msSQL += " trade_capital='',";
            }
            else
            {
                msSQL += " trade_capital='" + values.trade_capital.Replace("'", "") + "',";

            }
            msSQL += " economical_flag='Y'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where application_gid='" + lsapplication_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Social/Trade Capital details Submitted successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while submit Social/Trade Capital details";
            }
        }

        public void DaPostGeneticCode(string employee_gid, MdlMstGeneticCode values)
        {

            msSQL = "select geneticcode_gid from agr_mst_tsuprapplication2geneticcode where application_gid='" + employee_gid + "' and geneticcode_gid='" + values.geneticcode_gid + "'";
            string lsgenetic_code = objdbconn.GetExecuteScalar(msSQL);
            if (lsgenetic_code == (values.geneticcode_gid))
            {

                values.status = false;
                values.message = "Already Genetic Code Added";
                return;

            }
            msGetGid = objcmnfunctions.GetMasterGID("A2GC");
            msSQL = " insert into agr_mst_tsuprapplication2geneticcode(" +
                   " application2geneticcode_gid," +
                   " application_gid," +
                   " geneticcode_gid," +
                   " geneticcode_name," +
                   " genetic_status," +
                   " genetic_remarks," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + employee_gid + "'," +
                   "'" + values.geneticcode_gid + "'," +
                   "'" + values.geneticcode_name.Replace("'", " ") + "'," +
                   "'" + values.genetic_status + "'," +
                   "'" + values.genetic_remarks.Replace("'", " ") + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Genetic Code details Added successfully";

                msSQL = " select application2geneticcode_gid,geneticcode_gid,geneticcode_name,genetic_status,genetic_remarks" +
                     " from agr_mst_tsuprapplication2geneticcode where " +
                     " application_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getgenetic_list = new List<mstgeneticcode_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getgenetic_list.Add(new mstgeneticcode_list
                        {
                            application2geneticcode_gid = (dr_datarow["application2geneticcode_gid"].ToString()),
                            geneticcode_gid = (dr_datarow["geneticcode_gid"].ToString()),
                            geneticcode_name = (dr_datarow["geneticcode_name"].ToString()),
                            genetic_status = (dr_datarow["genetic_status"].ToString()),
                            genetic_remarks = (dr_datarow["genetic_remarks"].ToString()),
                        });
                    }
                    values.mstgeneticcode_list = getgenetic_list;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Adding Genetic Code";
            }
        }

        public void DaDeleteGenetic(string application2geneticcode_gid, MdlMstGeneticCode values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tsuprapplication2geneticcode where application2geneticcode_gid='" + application2geneticcode_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Genetic Code details Deleted successfully";

                msSQL = " select application2geneticcode_gid,geneticcode_gid,geneticcode_name,genetic_status,genetic_remarks" +
                     " from agr_mst_tsuprapplication2geneticcode where " +
                     " application_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getgenetic_list = new List<mstgeneticcode_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getgenetic_list.Add(new mstgeneticcode_list
                        {
                            application2geneticcode_gid = (dr_datarow["application2geneticcode_gid"].ToString()),
                            geneticcode_gid = (dr_datarow["geneticcode_gid"].ToString()),
                            geneticcode_name = (dr_datarow["geneticcode_name"].ToString()),
                            genetic_status = (dr_datarow["genetic_status"].ToString()),
                            genetic_remarks = (dr_datarow["genetic_remarks"].ToString()),
                        });
                    }
                    values.mstgeneticcode_list = getgenetic_list;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while deleting Genetic Code";
            }
        }
        public void DaPostLoanDtl(string employee_gid, MdlMstLoanDtl values)
        {
            //string fsprimaryvaluechain_gid = string.Empty;
            //string fsprimaryvaluechain_name = string.Empty;
            //if (values.primaryvaluechain_list != null)
            //{
            //    for (var i = 0; i < values.primaryvaluechain_list.Count; i++)
            //    {
            //        fsprimaryvaluechain_gid += values.primaryvaluechain_list[i].valuechain_gid + ",";
            //        fsprimaryvaluechain_name += values.primaryvaluechain_list[i].valuechain_name + ",";

            //    }
            //    fsprimaryvaluechain_gid = fsprimaryvaluechain_gid.TrimEnd(',');
            //    fsprimaryvaluechain_name = fsprimaryvaluechain_name.TrimEnd(',');
            //}

            //string fssecondaryvaluechain_gid = string.Empty;
            //string fssecondaryvaluechain_name = string.Empty;
            //if (values.secondaryvaluechain_list != null)
            //{
            //    for (var i = 0; i < values.secondaryvaluechain_list.Count; i++)
            //    {
            //        fssecondaryvaluechain_gid += values.secondaryvaluechain_list[i].valuechain_gid + ",";
            //        fssecondaryvaluechain_name += values.secondaryvaluechain_list[i].valuechain_name + ",";

            //    }
            //    fssecondaryvaluechain_gid = fssecondaryvaluechain_gid.TrimEnd(',');
            //    fssecondaryvaluechain_name = fssecondaryvaluechain_name.TrimEnd(',');
            //}

            //msSQL = "select application_gid from agr_mst_tsuprapplication2loan where producttype_gid='" + values.producttype_gid + "' and " +
            //    " productsubtype_gid='" + values.productsubtype_gid + "' and application_gid='" + values.application_gid + "'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == false)
            //{
            //    objODBCDatareader.Close(); =>
            if (values.product_type == "Agri Receivable Finance (ARF)")
            {
                msSQL = "select application2loan_gid from agr_mst_tsuprapplication2buyer  where application2loan_gid='" + employee_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    objODBCDatareader.Close();
                    values.message = "Kindly add atleast one Buyer";
                    values.status = false;
                    return;
                }
                else
                {
                    objODBCDatareader.Close();
                    msGetGid = objcmnfunctions.GetMasterGID("AP2L");

                    msSQL = " insert into agr_mst_tsuprapplication2loan(" +
                            " application2loan_gid ," +
                            " application_gid," +
                            " facilityrequested_date," +
                            " product_type," +
                            " producttype_gid," +
                            " productsub_type," +
                            " productsubtype_gid," +
                            " loantype_gid," +
                            " loan_type ," +
                            " loanfacility_amount," +
                            " rate_interest," +
                            " penal_interest," +
                            " facilityvalidity_year," +
                            " facilityvalidity_month," +
                            " facilityvalidity_days," +
                            " facilityoverall_limit ," +
                            " tenureproduct_year," +
                            " tenureproduct_month," +
                            " tenureproduct_days," +
                            " tenureoverall_limit ," +
                            " facility_type," +
                            " facility_mode," +
                            " principalfrequency_name," +
                            " principalfrequency_gid," +
                            " interestfrequency_name," +
                            " interestfrequency_gid," +
                            " program_gid," +
                            " program," +
                            //" primaryvaluechain_gid," +
                            //" primaryvaluechain_name," +
                            //" secondaryvaluechain_gid," +
                            //" secondaryvaluechain_name," +
                            " interest_status," +
                            " moratorium_status," +
                            " moratorium_type," +
                            " moratorium_startdate," +
                            " moratorium_enddate," +
                            " source_type," +
                            " guideline_value," +
                            " guideline_date," +
                            " marketvalue_date ," +
                            " market_value," +
                            " forcedsource_value," +
                            " collateralSSV_value," +
                            " forcedvalueassessed_on," +
                            " collateralobservation_summary," +
                            " enduse_purpose," +
                            " product_gid," +
                            " product_name," +
                            " variety_gid," +
                            " variety_name," +
                            " sector_name," +
                            " category_name," +
                            " botanical_name," +
                            " alternative_name," +
                            " milestone_applicability, " +
                            " insurance_applicability, " +
                            " milestonepayment_gid, " +
                            " milestonepayment_name, " +
                            " sa_payout, " +
                            " insurance_availability, " +
                            " insurance_percent, " +
                            " insurance_cost, " +
                            " net_yield, " +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.application_gid + "'," +
                            "'" + Convert.ToDateTime(values.facilityrequested_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                            "'" + values.product_type + "'," +
                            "'" + values.producttype_gid + "'," +
                            "'" + values.productsub_type + "'," +
                            "'" + values.productsubtype_gid + "'," +
                            "'" + values.loantype_gid + "'," +
                            "'" + values.loan_type + "'," +
                            "'" + values.facilityloan_amount + "'," +
                            "'" + values.rate_interest + "'," +
                            "'" + values.penal_interest + "'," +
                            "'" + values.facilityvalidity_year + "'," +
                            "'" + values.facilityvalidity_month + "'," +
                            "'" + values.facilityvalidity_days + "'," +
                            "'" + values.facilityoverall_limit + "'," +
                            "'" + values.tenureproduct_year + "'," +
                            "'" + values.tenureproduct_month + "'," +
                            "'" + values.tenureproduct_days + "'," +
                            "'" + values.tenureoverall_limit + "'," +
                            "'" + values.facility_type + "'," +
                            "'" + values.facility_mode + "'," +
                             "'" + values.principalfrequency_name + "'," +
                            "'" + values.principalfrequency_gid + "'," +
                            "'" + values.interestfrequency_name + "'," +
                            "'" + values.interestfrequency_gid + "'," +
                            "'" + values.program_gid + "'," +
                            "'" + values.program + "'," +
                            //"'" + fsprimaryvaluechain_gid + "'," +
                            //"'" + fsprimaryvaluechain_name + "'," +
                            //"'" + fssecondaryvaluechain_gid + "'," +
                            //"'" + fssecondaryvaluechain_name + "'," +
                            "'" + values.interest_status + "'," +
                            "'" + values.moratorium_status + "'," +
                            "'" + values.moratorium_type + "',";
                    if (values.moratorium_startdate == null || values.moratorium_startdate == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(values.moratorium_startdate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (values.moratorium_enddate == null || values.moratorium_enddate == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(values.moratorium_enddate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    msSQL += "'" + values.source_type + "',";
                    if (values.guideline_value == null || values.guideline_value == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.guideline_value.Replace(",", "") + "',";
                    }
                    if (values.guideline_date == null || values.guideline_date == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(values.guideline_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (values.marketvalue_date == null || values.marketvalue_date == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(values.marketvalue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (values.market_value == null || values.market_value == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.market_value.Replace(",", "") + "',";
                    }
                    if (values.forcedsource_value == null || values.forcedsource_value == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.forcedsource_value.Replace(",", "") + "',";
                    }
                    if (values.collateralSSV_value == null || values.collateralSSV_value == "")
                    {
                        msSQL += "'0.00',";
                    }
                    else
                    {
                        msSQL += "'" + values.collateralSSV_value.Replace(",", "") + "',";
                    }
                    if (values.forcedvalueassessed_on == null || values.forcedvalueassessed_on == "")
                    {
                        msSQL += "null,";
                    }
                    else
                    {
                        msSQL += "'" + Convert.ToDateTime(values.forcedvalueassessed_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                    }
                    if (values.collateralobservation_summary == null || values.collateralobservation_summary == "")
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.collateralobservation_summary.Replace("'", "") + "',";
                    }
                    if (values.enduse_purpose == null || values.enduse_purpose == "")
                    {
                        msSQL += "'',";
                    }
                    else
                    {
                        msSQL += "'" + values.enduse_purpose.Replace("'", "") + "',";
                    }
                    msSQL += "'" + values.product_gid + "'," +
                             "'" + values.product_name + "'," +
                             "'" + values.variety_gid + "'," +
                             "'" + values.variety_name + "'," +
                             "'" + values.sector_name + "'," +
                             "'" + values.category_name + "'," +
                             "'" + values.botanical_name + "'," +
                             "'" + values.alternative_name + "'," +
                             "'" + values.milestone_applicability + "'," +
                             "'" + values.insurance_applicability + "'," +
                             "'" + values.milestonepayment_gid + "'," +
                             "'" + values.milestonepayment_name + "'," +
                             "'" + values.sa_payout + "'," +
                             "'" + values.insurance_availability + "'," +
                             "'" + values.insurance_percent + "'," +
                             "'" + values.insurance_cost + "'," +
                             "'" + values.net_yield + "'," +
                             "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = "update agr_mst_tsuprapplication2product set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = "update agr_mst_tsuprapplication2buyer set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        msSQL = "update agr_mst_tsuprapplication set productcharges_status='Completed',productcharge_flag='Y' where application_gid = '" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        values.status = true;


                        values.message = "Loan details Added successfully";
                        msSQL = " select date_format(facilityrequested_date, '%d-%m-%Y') as facilityrequested_date, product_type,facilityrequested_date,scheme_type, " +
                                 " productsub_type, loanfacility_amount, loan_type, rate_interest, penal_interest, facilityoverall_limit, " +
                                 " tenureoverall_limit, facility_type, facility_mode, principalfrequency_name, interestfrequency_name,producttype_gid, " +
                                 " interest_status, moratorium_type, moratorium_status, date_format(moratorium_startdate, '%d-%m-%Y') as moratorium_startdate,application2loan_gid, " +
                                 " date_format(moratorium_enddate, '%d-%m-%Y') as moratorium_enddate, product_gid,product_name,variety_gid,variety_name, " +
                                 " sector_name, category_name, botanical_name, alternative_name from agr_mst_tsuprapplication2loan " +
                                 " where application_gid='" + values.application_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getmstloan_list = new List<mstloan_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dr_datarow in dt_datatable.Rows)
                            {
                                getmstloan_list.Add(new mstloan_list
                                {
                                    facilityrequested_date = (dr_datarow["facilityrequested_date"].ToString()),
                                    producttype_gid = (dr_datarow["producttype_gid"].ToString()),
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
                                    application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                                    product_gid = (dr_datarow["product_gid"].ToString()),
                                    product_name = (dr_datarow["product_name"].ToString()),
                                    variety_gid = (dr_datarow["variety_gid"].ToString()),
                                    variety_name = (dr_datarow["variety_name"].ToString()),
                                    sector_name = (dr_datarow["sector_name"].ToString()),
                                    category_name = (dr_datarow["category_name"].ToString()),
                                    botanical_name = (dr_datarow["botanical_name"].ToString()),
                                    alternative_name = (dr_datarow["alternative_name"].ToString()),

                                });
                            }
                            values.mstloan_list = getmstloan_list;
                        }
                        dt_datatable.Dispose();

                        msSQL = "update agr_mst_tsupruploadcollateraldocument set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " select collateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                                            " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                                            " from agr_mst_tsupruploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                                            " and b.user_gid = c.user_gid and application2loan_gid='" + msGetGid + "'";

                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var get_filename = new List<DocumentList>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dr_datarow in dt_datatable.Rows)
                            {
                                get_filename.Add(new DocumentList
                                {
                                    //document_path = (dr_datarow["document_path"].ToString()),
                                    document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                                    document_name = (dr_datarow["document_name"].ToString()),
                                    document_gid = (dr_datarow["collateraldocument_gid"].ToString()),
                                    uploaded_by = dr_datarow["uploaded_by"].ToString(),
                                    updated_date = dr_datarow["uploaded_date"].ToString(),
                                    document_title = dr_datarow["document_title"].ToString()
                                });
                            }
                            values.DocumentList = get_filename;
                        }
                        dt_datatable.Dispose();
                        msSQL = "select application_gid from agr_mst_tsuprapplication2loan where product_type='Agri Receivable Finance (ARF)' and application_gid='" + employee_gid + "' ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            values.buyer_status = "Y";
                        }
                        objODBCDatareader.Close();

                        msSQL = "select application_gid from agr_mst_tsuprapplication2loan where loan_type='Secured' and application_gid='" + employee_gid + "' ";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            values.collateral_status = "Y";
                        }
                        objODBCDatareader.Close();

                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured while Adding Loan";
                    }
                }
            }
            else
            {

                msGetGid = objcmnfunctions.GetMasterGID("AP2L");
                msSQL = " insert into agr_mst_tsuprapplication2loan(" +
                       " application2loan_gid ," +
                       " application_gid," +
                       " facilityrequested_date," +
                       " product_type," +
                       " producttype_gid," +
                       " productsub_type," +
                       " productsubtype_gid," +
                       " loantype_gid," +
                       " loan_type ," +
                       " loanfacility_amount," +
                       " rate_interest," +
                       " penal_interest," +
                       " facilityvalidity_year," +
                       " facilityvalidity_month," +
                       " facilityvalidity_days," +
                       " facilityoverall_limit ," +
                       " tenureproduct_year," +
                       " tenureproduct_month," +
                       " tenureproduct_days," +
                       " tenureoverall_limit ," +
                       " facility_type," +
                       " facility_mode," +
                       " principalfrequency_name," +
                       " principalfrequency_gid," +
                       " interestfrequency_name," +
                       " interestfrequency_gid," +
                       " program_gid," +
                       " program," +
                       //" primaryvaluechain_gid," +
                       //" primaryvaluechain_name," +
                       //" secondaryvaluechain_gid," +
                       //" secondaryvaluechain_name," +
                       " interest_status," +
                       " moratorium_status," +
                       " moratorium_type," +
                       " moratorium_startdate," +
                       " moratorium_enddate," +
                       " source_type," +
                       " guideline_value," +
                       " guideline_date," +
                       " marketvalue_date ," +
                       " market_value," +
                       " forcedsource_value," +
                       " collateralSSV_value," +
                       " forcedvalueassessed_on," +
                       " collateralobservation_summary," +
                       " enduse_purpose," +
                       " insurance_availability, " +
                         " insurance_cost, " +
                         " net_yield, " +
                       " product_gid," +
                       " product_name," +
                       " variety_gid," +
                       " variety_name," +
                       " sector_name," +
                       " category_name," +
                       " botanical_name," +
                       " alternative_name," +
                       " milestone_applicability, " +
                       " insurance_applicability, " +
                       " milestonepayment_gid, " +
                       " milestonepayment_name, " +
                       " sa_payout, " +
                       //" insurance_availability, " +
                       " insurance_percent, " +
                       //" insurance_cost, " +
                       //" net_yield, " +
                       " created_by," +
                       " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                           "'" + values.application_gid + "'," +
                           "'" + Convert.ToDateTime(values.facilityrequested_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                           "'" + values.product_type + "'," +
                           "'" + values.producttype_gid + "'," +
                           "'" + values.productsub_type + "'," +
                           "'" + values.productsubtype_gid + "'," +
                           "'" + values.loantype_gid + "'," +
                           "'" + values.loan_type + "'," +
                           "'" + values.facilityloan_amount + "'," +
                           "'" + values.rate_interest + "'," +
                           "'" + values.penal_interest + "'," +
                           "'" + values.facilityvalidity_year + "'," +
                           "'" + values.facilityvalidity_month + "'," +
                           "'" + values.facilityvalidity_days + "'," +
                           "'" + values.facilityoverall_limit + "'," +
                           "'" + values.tenureproduct_year + "'," +
                           "'" + values.tenureproduct_month + "'," +
                           "'" + values.tenureproduct_days + "'," +
                           "'" + values.tenureoverall_limit + "'," +
                           "'" + values.facility_type + "'," +
                           "'" + values.facility_mode + "'," +
                            "'" + values.principalfrequency_name + "'," +
                           "'" + values.principalfrequency_gid + "'," +
                           "'" + values.interestfrequency_name + "'," +
                           "'" + values.interestfrequency_gid + "'," +
                           "'" + values.program_gid + "'," +
                           "'" + values.program + "'," +
                           //"'" + fsprimaryvaluechain_gid + "'," +
                           //"'" + fsprimaryvaluechain_name + "'," +
                           //"'" + fssecondaryvaluechain_gid + "'," +
                           //"'" + fssecondaryvaluechain_name + "'," +
                           "'" + values.interest_status + "'," +
                           "'" + values.moratorium_status + "'," +
                           "'" + values.moratorium_type + "',";
                if (values.moratorium_startdate == null || values.moratorium_startdate == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.moratorium_startdate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                if (values.moratorium_enddate == null || values.moratorium_enddate == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.moratorium_enddate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                msSQL += "'" + values.source_type + "',";
                if (values.guideline_value == null || values.guideline_value == "")
                {
                    msSQL += "'0.00',";
                }
                else
                {
                    msSQL += "'" + values.guideline_value.Replace(",", "") + "',";
                }
                if (values.guideline_date == null || values.guideline_date == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.guideline_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                if (values.marketvalue_date == null || values.marketvalue_date == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.marketvalue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                if (values.market_value == null || values.market_value == "")
                {
                    msSQL += "'0.00',";
                }
                else
                {
                    msSQL += "'" + values.market_value.Replace(",", "") + "',";
                }
                if (values.forcedsource_value == null || values.forcedsource_value == "")
                {
                    msSQL += "'0.00',";
                }
                else
                {
                    msSQL += "'" + values.forcedsource_value.Replace(",", "") + "',";
                }
                if (values.collateralSSV_value == null || values.collateralSSV_value == "")
                {
                    msSQL += "'0.00',";
                }
                else
                {
                    msSQL += "'" + values.collateralSSV_value.Replace(",", "") + "',";
                }
                if (values.forcedvalueassessed_on == null || values.forcedvalueassessed_on == "")
                {
                    msSQL += "null,";
                }
                else
                {
                    msSQL += "'" + Convert.ToDateTime(values.forcedvalueassessed_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                }
                if (values.collateralobservation_summary == null || values.collateralobservation_summary == "")
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.collateralobservation_summary.Replace("'", "") + "',";
                }
                if (values.enduse_purpose == null || values.enduse_purpose == "")
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.enduse_purpose.Replace("'", "") + "',";
                }
                if (values.insurance_availability == null || values.insurance_availability == "")
                {
                    msSQL += "0.00,";
                }
                else
                {
                    msSQL += "'" + values.insurance_availability.Replace(",", "") + "',";
                }
                if (values.insurance_cost == null || values.insurance_cost == "")
                {
                    msSQL += "0.00,";
                }
                else
                {
                    msSQL += "'" + values.insurance_cost.Replace("'", "") + "',";
                }
                if (values.net_yield == null || values.net_yield == "")
                {
                    msSQL += "0.00,";
                }
                else
                {
                    msSQL += "'" + values.net_yield.Replace("'", "") + "',";
                }
                msSQL += "'" + values.product_gid + "'," +
                         "'" + values.product_name + "'," +
                         "'" + values.variety_gid + "'," +
                         "'" + values.variety_name + "'," +
                         "'" + values.sector_name + "'," +
                         "'" + values.category_name + "'," +
                         "'" + values.botanical_name + "'," +
                         "'" + values.alternative_name + "'," +
                         "'" + values.milestone_applicability + "'," +
                         "'" + values.insurance_applicability + "'," +
                         "'" + values.milestonepayment_gid + "'," +
                         "'" + values.milestonepayment_name + "'," +
                         "'" + values.sa_payout + "'," +
                         //"'" + values.insurance_availability.Replace(",", "") + "'," +
                         "'" + values.insurance_percent + "'," +
                         //"'" + values.insurance_cost + "'," +
                         //"'" + values.net_yield + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = "update agr_mst_tsuprapplication2product set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsuprapplication2buyer set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = "update agr_mst_tsuprapplication set productcharges_status='Completed',productcharge_flag='Y' where application_gid = '" + values.application_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;


                    values.message = "Loan details Added successfully";
                    msSQL = "select date_format(facilityrequested_date, '%d-%m-%Y') as facilityrequested_date, product_type,facilityrequested_date,scheme_type, " +
                              " productsub_type, loanfacility_amount, loan_type, rate_interest, penal_interest, facilityoverall_limit, " +
                              " tenureoverall_limit, facility_type, facility_mode, principalfrequency_name, interestfrequency_name,producttype_gid, " +
                              " interest_status, moratorium_type, moratorium_status, date_format(moratorium_startdate, '%d-%m-%Y') as moratorium_startdate,application2loan_gid, " +
                              " date_format(moratorium_enddate, '%d-%m-%Y') as moratorium_enddate,product_gid,product_name, " +
                              " variety_gid,variety_name,sector_name,category_name,botanical_name,alternative_name from agr_mst_tsuprapplication2loan " +
                              " where application_gid='" + values.application_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getmstloan_list = new List<mstloan_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getmstloan_list.Add(new mstloan_list
                            {
                                facilityrequested_date = (dr_datarow["facilityrequested_date"].ToString()),
                                producttype_gid = (dr_datarow["producttype_gid"].ToString()),
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
                                application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                                product_gid = (dr_datarow["product_gid"].ToString()),
                                product_name = (dr_datarow["product_name"].ToString()),
                                variety_gid = (dr_datarow["variety_gid"].ToString()),
                                variety_name = (dr_datarow["variety_name"].ToString()),
                                sector_name = (dr_datarow["sector_name"].ToString()),
                                category_name = (dr_datarow["category_name"].ToString()),
                                botanical_name = (dr_datarow["botanical_name"].ToString()),
                                alternative_name = (dr_datarow["alternative_name"].ToString()),

                            });
                        }
                        values.mstloan_list = getmstloan_list;
                    }
                    dt_datatable.Dispose();

                    msSQL = "update agr_mst_tsupruploadcollateraldocument set application2loan_gid='" + msGetGid + "' where application2loan_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " select collateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                                        " from agr_mst_tsupruploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                                        " and b.user_gid = c.user_gid and application2loan_gid='" + msGetGid + "'";

                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var get_filename = new List<DocumentList>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            get_filename.Add(new DocumentList
                            {
                                document_name = (dr_datarow["document_name"].ToString()),
                                document_gid = (dr_datarow["collateraldocument_gid"].ToString()),
                                uploaded_by = dr_datarow["uploaded_by"].ToString(),
                                updated_date = dr_datarow["uploaded_date"].ToString(),
                                document_title = dr_datarow["document_title"].ToString()
                            });
                        }
                        values.DocumentList = get_filename;
                    }
                    dt_datatable.Dispose();
                    msSQL = "select application_gid* from agr_mst_tsuprapplication2loan where product_type='Agri Receivable Finance (ARF)' and application_gid='" + employee_gid + "' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.buyer_status = "Y";
                    }
                    objODBCDatareader.Close();

                    msSQL = "select application_gid from agr_mst_tsuprapplication2loan where loan_type='Secured' and application_gid='" + employee_gid + "' ";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        values.collateral_status = "Y";
                    }
                    objODBCDatareader.Close();

                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured while Adding Loan";
                }
            }
            //}
            //else
            //{
            //    objODBCDatareader.Close();
            //    values.status = false;
            //    values.message = "Already this product sub type added.";
            //}
        }

        public void DaDeleteLoan(MdlMstLoanDtl values, string employee_gid)
        {

            int lsapplicationTrade = 0, lsApplicationCharge = 0;
            msSQL = "SELECT count(application2trade_gid) FROM agr_mst_tsuprapplication2trade where application2loan_gid='" + values.application2loan_gid + "'";
            lsapplicationTrade = Convert.ToInt16(objdbconn.GetExecuteScalar(msSQL));

            msSQL = " select group_concat(application2loan_gid) from agr_mst_tsuprapplicationservicecharge  " +
                   " where application_gid = '" + values.application_gid + "' and application2loan_gid is not null";
            string lsapplication2loan_gid = objdbconn.GetExecuteScalar(msSQL);
            List<string> application2loan_gid = lsapplication2loan_gid.Split(',').Reverse().ToList();
            lsApplicationCharge = application2loan_gid.Where(a => a == values.application2loan_gid).Count(); 
            if (lsapplicationTrade != 0 && lsApplicationCharge != 0)
            {
                values.status = false;
                values.message = "Can't able to delete because already Trade & service charges are added against this product type";
            }
            else if (lsapplicationTrade != 0 && lsApplicationCharge == 0)
            {
                values.status = false;
                values.message = "Can't able to delete because already Trade details are added against this product type";
            }
            else if (lsapplicationTrade == 0 && lsApplicationCharge != 0)
            {
                values.status = false;
                values.message = "Can't able to delete because already service charges added against this product type";
            }
            else
            {  
                msSQL = "delete from agr_mst_tsuprapplication2loan where application2loan_gid='" + values.application2loan_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Loan details Deleted successfully";
                    msSQL = "select application_gid from agr_mst_tsuprapplication2loan  where application_gid = '" + values.application_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {

                        msSQL = "update agr_mst_tsuprapplication set productcharges_status='Incomplete',productcharge_flag='N' where application_gid = '" + values.application_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                    objODBCDatareader.Close();
                    msSQL = "select date_format(facilityrequested_date, '%d-%m-%Y') as facilityrequested_date, product_type,facilityrequested_date,scheme_type, " +
                               " productsub_type, loanfacility_amount, loan_type, rate_interest, penal_interest, facilityoverall_limit, " +
                               " tenureoverall_limit, facility_type, facility_mode, principalfrequency_name, interestfrequency_name,producttype_gid, " +
                               " interest_status, moratorium_type, moratorium_status, date_format(moratorium_startdate, '%d-%m-%Y') as moratorium_startdate,application2loan_gid, " +
                               " date_format(moratorium_enddate, '%d-%m-%Y') as moratorium_enddate from agr_mst_tsuprapplication2loan " +
                               " where application_gid='" + values.application_gid + "'";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var getmstloan_list = new List<mstloan_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            getmstloan_list.Add(new mstloan_list
                            {
                                facilityrequested_date = (dr_datarow["facilityrequested_date"].ToString()),
                                producttype_gid = (dr_datarow["producttype_gid"].ToString()),
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
                                application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),

                            });
                        }
                        values.mstloan_list = getmstloan_list;
                    }
                    dt_datatable.Dispose();

                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred while deleting";
                }
            }
        }

        public void DaGetLoanDtl(string application_gid, MdlMstLoanDtl values, string employee_gid)
        {


            msSQL = " select date_format(facilityrequested_date, '%d-%m-%Y') as facilityrequested_date,productsubtype_gid, product_type,facilityrequested_date,scheme_type, " +
                    " productsub_type, loanfacility_amount, loan_type, rate_interest, penal_interest, facilityoverall_limit, " +
                    " tenureoverall_limit, facility_type, facility_mode, principalfrequency_name, interestfrequency_name,producttype_gid, " +
                    " interest_status, moratorium_type, moratorium_status, date_format(moratorium_startdate, '%d-%m-%Y') as moratorium_startdate,application2loan_gid, " +
                    " date_format(moratorium_enddate, '%d-%m-%Y') as moratorium_enddate,product_gid,variety_gid from agr_mst_tsuprapplication2loan " +
                    " where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstloan_list = new List<mstloan_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstloan_list.Add(new mstloan_list
                    {
                        facilityrequested_date = (dr_datarow["facilityrequested_date"].ToString()),
                        producttype_gid = (dr_datarow["producttype_gid"].ToString()),
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
                        application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                        product_gid = (dr_datarow["product_gid"].ToString()),
                        variety_gid = (dr_datarow["variety_gid"].ToString()),
                        productsubtype_gid = (dr_datarow["productsubtype_gid"].ToString()),
                    });
                }
                values.mstloan_list = getmstloan_list;
            }
            dt_datatable.Dispose();
            msSQL = " select application_gid,application2servicecharge_gid, processing_fee,processing_collectiontype,doc_charges," +
                   " doccharge_collectiontype,fieldvisit_charges,fieldvisit_charges_collectiontype,adhoc_fee,adhoc_collectiontype," +
                   " life_insurance,lifeinsurance_collectiontype,acct_insurance,total_collect,total_deduct,product_type," +
                   " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                   " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, acctinsurance_collectiontype " +
                   " from agr_mst_tsuprapplicationservicecharge a " +
                   " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                   " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                     " where a.application_gid = '" + application_gid + "' order by application2servicecharge_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getproductcharges_list = new List<servicecharges_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getproductcharges_list.Add(new servicecharges_list
                    {
                        application2servicecharge_gid = (dr_datarow["application2servicecharge_gid"].ToString()),
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
                        total_deduct = (dr_datarow["total_deduct"].ToString()),
                        product_type = (dr_datarow["product_type"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        acctinsurance_collectiontype = (dr_datarow["acctinsurance_collectiontype"].ToString()),
                    });
                }
                values.servicecharges_list = getproductcharges_list;
            }
            dt_datatable.Dispose();

            values.status = true;
        }
        public void DaGetLoanSubProduct(string loanproduct_gid, string application_gid, string application2loan_gid, MdlMstApplication360 values)
        {
            msSQL = "select loansubproduct_gid,loansubproduct_name from agr_mst_tloansubproduct where status='Y' and loanproduct_gid='" + loanproduct_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplication_list = new List<application_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getapplication_list.Add(new application_list
                    {
                        loansubproduct_gid = (dr_datarow["loansubproduct_gid"].ToString()),
                        loansubproduct_name = (dr_datarow["loansubproduct_name"].ToString()),
                    });
                }
                values.application_list = getapplication_list;
            }
            dt_datatable.Dispose();
            msSQL = "select producttype_gid from agr_mst_tsuprapplication2loan where application2loan_gid='" + application2loan_gid + "'";
            string producttype_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select application_gid from agr_mst_tsuprapplicationservicecharge where application_gid='" + application_gid + "' and producttype_gid='" + producttype_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.charge_flag = "Y";
            }
            else
            {
                values.charge_flag = "N";
            }
            objODBCDatareader.Close();
        }

        public void DaPostBuyer(string employee_gid, MdlMstBuyer values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AP2B");
            msSQL = " insert into agr_mst_tsuprapplication2buyer(" +
                   " application2buyer_gid ," +
                   " application2loan_gid," +
                   " buyer_gid," +
                   " buyer_name," +
                   " buyer_limit," +
                   " availed_limit," +
                   " balance_limit ," +
                   " bill_tenure," +
                   " margin," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + employee_gid + "'," +
                   "'" + values.buyer_gid + "'," +
                   "'" + values.buyer_name + "',";
            if (values.buyer_limit == null || values.buyer_limit == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.buyer_limit.Replace(",", "") + "',";
            }
            if (values.availed_limit == null || values.availed_limit == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.availed_limit.Replace(",", "") + "',";
            }


            if (values.balance_limit == null || values.balance_limit == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.balance_limit.Replace(",", "") + "',";
            }
            msSQL += "'" + values.bill_tenure + "'," +
                     "'" + values.margin + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Buyer details Added successfully";

                msSQL = "select application2buyer_gid,buyer_name,buyer_gid,buyer_limit,availed_limit,balance_limit,margin,bill_tenure " +
                    " from agr_mst_tsuprapplication2buyer where application2loan_gid='" + employee_gid + "'";
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
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Adding Buyer";
            }
        }

        public void DaPostCollateral(string employee_gid, MdlMstCollatertal values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AP2C");
            msSQL = " insert into agr_mst_tsuprapplication2collateral(" +
                   " application2collateral_gid ," +
                   " application_gid," +
                   " source_type," +
                   " guideline_value," +
                   " guideline_date," +
                   " marketvalue_date ," +
                   " market_value," +
                   " forcedsource_value," +
                   " collateralSSV_value," +
                   " forcedvalueassessed_on," +
                   " collateralobservation_summary," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + employee_gid + "'," +
                   "'" + values.source_type + "',";
            if (values.guideline_value == null || values.guideline_value == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.guideline_value.Replace(",", "") + "',";
            }
            if (values.guideline_date == null || values.guideline_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.guideline_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.marketvalue_date == null || values.marketvalue_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.marketvalue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.market_value == null || values.market_value == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.market_value.Replace(",", "") + "',";
            }
            if (values.forcedsource_value == null || values.forcedsource_value == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.forcedsource_value.Replace(",", "") + "',";
            }
            if (values.collateralSSV_value == null || values.collateralSSV_value == "")
            {
                msSQL += "'0.00',";
            }
            else
            {
                msSQL += "'" + values.collateralSSV_value.Replace(",", "") + "',";
            }
            if (values.forcedvalueassessed_on == null || values.forcedvalueassessed_on == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.forcedvalueassessed_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if (values.collateralobservation_summary == null || values.collateralobservation_summary == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.collateralobservation_summary.Replace("'", "") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Collateral details Added successfully";

                msSQL = "update agr_mst_tsupruploadcollateraldocument set application2collateral_gid='" + msGetGid + "' where application2collateral_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select application2collateral_gid,source_type,guideline_value,market_value,forcedsource_value,collateralSSV_value," +
                    " date_format(guideline_date,'%d-%m-%Y') as guideline_date,date_format(forcedvalueassessed_on,'%d-%m-%Y') as forcedvalueassessed_on," +
                    " date_format(marketvalue_date,'%d-%m-%Y') as marketvalue_date,collateralobservation_summary " +
                    " from agr_mst_tsuprapplication2collateral where application_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcollatertal_list = new List<collatertal_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcollatertal_list.Add(new collatertal_list
                        {
                            application2collateral_gid = (dr_datarow["application2collateral_gid"].ToString()),
                            source_type = (dr_datarow["source_type"].ToString()),
                            guideline_value = (dr_datarow["guideline_value"].ToString()),
                            market_value = (dr_datarow["market_value"].ToString()),
                            forcedsource_value = (dr_datarow["forcedsource_value"].ToString()),
                            collateralSSV_value = (dr_datarow["collateralSSV_value"].ToString()),
                            collateralobservation_summary = (dr_datarow["collateralobservation_summary"].ToString()),
                            guideline_date = (dr_datarow["guideline_date"].ToString()),
                            forcedvalueassessed_on = (dr_datarow["forcedvalueassessed_on"].ToString()),
                            marketvalue_date = (dr_datarow["marketvalue_date"].ToString()),
                        });
                    }
                    values.collatertal_list = getcollatertal_list;
                }
                dt_datatable.Dispose();
                msSQL = " select collateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                      " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                      " from agr_mst_tsupruploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                      " and b.user_gid = c.user_gid and application2collateral_gid='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<DocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new DocumentList
                        {
                            //document_path = (dr_datarow["document_path"].ToString()),
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["collateraldocument_gid"].ToString()),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString(),
                            document_title = dr_datarow["document_title"].ToString()
                        });
                    }
                    values.DocumentList = get_filename;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Adding";
            }
        }

        public void DaDeleteCollateral(string application2collateral_gid, MdlMstCollatertal values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tsuprapplication2collateral where application2collateral_gid='" + application2collateral_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Collateral details Deleted successfully";

                msSQL = "select application2collateral_gid,source_type,guideline_value,market_value,forcedsource_value,collateralSSV_value,collateralobservation_summary " +
                     " from agr_mst_tsuprapplication2collateral where application_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcollatertal_list = new List<collatertal_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcollatertal_list.Add(new collatertal_list
                        {
                            application2collateral_gid = (dr_datarow["application2collateral_gid"].ToString()),
                            source_type = (dr_datarow["source_type"].ToString()),
                            guideline_value = (dr_datarow["guideline_value"].ToString()),
                            market_value = (dr_datarow["market_value"].ToString()),
                            forcedsource_value = (dr_datarow["forcedsource_value"].ToString()),
                            collateralSSV_value = (dr_datarow["collateralSSV_value"].ToString()),
                            collateralobservation_summary = (dr_datarow["collateralobservation_summary"].ToString()),
                        });
                    }
                    values.collatertal_list = getcollatertal_list;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while deleting";
            }
        }

        public bool Dapostcollateraldocument(HttpRequest httpRequest, Documentname objfilename, string employee_gid)
        {
            DocumentList objdocumentmodel = new DocumentList();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            //MemoryStream ms = new MemoryStream();
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
            path = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }


            string document_title = httpRequest.Form["document_title"].ToString();

            if (httpRequest.Files.Count > 0)
            {
                string lsfirstdocument_filepath = string.Empty;
                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                    httpPostedFile = httpFileCollection[i];
                    string FileExtension = httpPostedFile.FileName;
                    //string lsfile_gid = msdocument_gid + FileExtension;
                    string lsfile_gid = msdocument_gid;
                    FileExtension = Path.GetExtension(FileExtension).ToLower();
                    lsfile_gid = lsfile_gid + FileExtension;
                    if ((FileExtension == ".xls") || (FileExtension == ".xlsx") || (FileExtension == ".doc") || (FileExtension == ".docx") || (FileExtension == ".pdf") || (FileExtension == ".zip") || (FileExtension == ".msg") || (FileExtension == ".oft") || (FileExtension == ".txt") || (FileExtension == ".txtx"))
                    {
                        //ls_readStream = httpPostedFile.InputStream;
                        //ls_readStream.CopyTo(ms);
                        //lspath = ("erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/");
                        //bool status;
                        //status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        //ms.Close();
                        //lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        MemoryStream ms = new MemoryStream();
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CollateralDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("CAMD");
                        msSQL = " insert into agr_mst_tsupruploadcollateraldocument( " +
                                     " collateraldocument_gid," +
                                     " document_name, " +
                                     " document_title," +
                                     " document_path, " +
                                     " application2loan_gid," +
                                     " created_by ," +
                                     " created_date " +
                                     " )values(" +
                                     "'" + msGetGid + "'," +
                                     "'" + httpPostedFile.FileName + "'," +
                                     "'" + document_title + "'," +
                                     "'" + lspath + msdocument_gid + FileExtension + "'," +
                                     "'" + employee_gid + "'," +
                                     "'" + employee_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            msSQL = " select collateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                               " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                               " from agr_mst_tsupruploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                               " and b.user_gid = c.user_gid and application2loan_gid='" + employee_gid + "'";

                            dt_datatable = objdbconn.GetDataTable(msSQL);
                            var get_filename = new List<DocumentList>();
                            if (dt_datatable.Rows.Count != 0)
                            {
                                foreach (DataRow dr_datarow in dt_datatable.Rows)
                                {
                                    get_filename.Add(new DocumentList
                                    {
                                        //document_path = (dr_datarow["document_path"].ToString()),
                                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                                        document_name = (dr_datarow["document_name"].ToString()),
                                        document_gid = (dr_datarow["collateraldocument_gid"].ToString()),
                                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                                        updated_date = dr_datarow["uploaded_date"].ToString(),
                                        document_title = dr_datarow["document_title"].ToString()
                                    });
                                }
                                objfilename.DocumentList = get_filename;
                            }
                            dt_datatable.Dispose();

                            objfilename.status = true;
                            objfilename.message = "Collateral Document uploaded successfully";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured while uploading Collateral document";
                        }
                    }
                    else
                    {
                        objfilename.status = false;
                        objfilename.message = "File format is not supported";
                    }
                }
            }
            return true;
        }
        public void Dadeletecollateraldoc(string document_gid, Documentname values, string employee_gid)
        {
            msSQL = "delete from  agr_mst_tsupruploadcollateraldocument where collateraldocument_gid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document deleted successfully";
                values.status = true;

                msSQL = " select collateraldocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                      " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                      " from agr_mst_tsupruploadcollateraldocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                      " and b.user_gid = c.user_gid and application2loan_gid='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<DocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new DocumentList
                        {
                            //document_path = (dr_datarow["document_path"].ToString()),
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["collateraldocument_gid"].ToString()),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString(),
                            document_title = dr_datarow["document_title"].ToString()
                        });
                    }
                    values.DocumentList = get_filename;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }
        public bool DaPostHypoDoc(HttpRequest httpRequest, Documentname objfilename, string employee_gid)
        {
            DocumentList objdocumentmodel = new DocumentList();
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
            path = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/" + "SamAgro/HypothecationDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month);
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }

            string document_title = httpRequest.Form["document_title"].ToString();


            if (httpRequest.Files.Count > 0)
            {
                string lsfirstdocument_filepath = string.Empty;
                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                    httpPostedFile = httpFileCollection[i];
                    string FileExtension = httpPostedFile.FileName;
                    //string lsfile_gid = msdocument_gid + FileExtension;
                    string lsfile_gid = msdocument_gid;
                    FileExtension = Path.GetExtension(FileExtension).ToLower();
                    lsfile_gid = lsfile_gid + FileExtension;
                    if ((FileExtension == ".xls") || (FileExtension == ".xlsx") || (FileExtension == ".doc") || (FileExtension == ".docx") || (FileExtension == ".pdf") || (FileExtension == ".zip") || (FileExtension == ".msg") || (FileExtension == ".oft") || (FileExtension == ".txt") || (FileExtension == ".txtx"))
                    {
                        ls_readStream = httpPostedFile.InputStream;
                        ls_readStream.CopyTo(ms);

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/HypothecationDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/HypothecationDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/HypothecationDoc/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("HYPD");
                        msSQL = " insert into agr_mst_tsupruploadhypothecationocument( " +
                                     " hypothecationdocument_gid," +
                                     " document_name, " +
                                     " document_title," +
                                     " document_path, " +
                                     " application2hypothecation_gid," +
                                     " created_by ," +
                                     " created_date " +
                                     " )values(" +
                                     "'" + msGetGid + "'," +
                                     "'" + httpPostedFile.FileName + "'," +
                                     "'" + document_title + "'," +
                                     "'" + lspath + msdocument_gid + FileExtension + "'," +
                                     "'" + employee_gid + "'," +
                                     "'" + employee_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult != 0)
                        {
                            msSQL = " select hypothecationdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                               " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                               " from agr_mst_tsupruploadhypothecationocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                               " and b.user_gid = c.user_gid and application2hypothecation_gid='" + employee_gid + "'";

                            dt_datatable = objdbconn.GetDataTable(msSQL);
                            var get_filename = new List<DocumentList>();
                            if (dt_datatable.Rows.Count != 0)
                            {
                                foreach (DataRow dr_datarow in dt_datatable.Rows)
                                {
                                    get_filename.Add(new DocumentList
                                    {
                                        //document_path = (dr_datarow["document_path"].ToString()),
                                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                                        document_name = (dr_datarow["document_name"].ToString()),
                                        document_gid = (dr_datarow["hypothecationdocument_gid"].ToString()),
                                        uploaded_by = dr_datarow["uploaded_by"].ToString(),
                                        updated_date = dr_datarow["uploaded_date"].ToString(),
                                        document_title = dr_datarow["document_title"].ToString()
                                    });
                                }
                                objfilename.DocumentList = get_filename;
                            }
                            dt_datatable.Dispose();

                            objfilename.status = true;
                            objfilename.message = "Hypothecation Document uploaded successfully";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured while uploading document";
                        }
                    }
                    else
                    {
                        objfilename.status = false;
                        objfilename.message = "File format is not supported";
                    }
                }
            }
            return true;
        }

        public void DadeleteHypoDoc(string document_gid, Documentname values, string employee_gid)
        {
            msSQL = "delete from  agr_mst_tsupruploadhypothecationocument where hypothecationdocument_gid='" + document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Document deleted successfully";
                values.status = true;

                msSQL = " select hypothecationdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                      " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                      " from agr_mst_tsupruploadhypothecationocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                      " and b.user_gid = c.user_gid and application2hypothecation_gid='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<DocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new DocumentList
                        {
                            //document_path = (dr_datarow["document_path"].ToString()),
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["hypothecationdocument_gid"].ToString()),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString(),
                            document_title = dr_datarow["document_title"].ToString()
                        });
                    }
                    values.DocumentList = get_filename;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.message = "Error Occrued while deleting document";
                values.status = false;
            }
        }

        public void DaPostHypothecation(string employee_gid, MdlMstHypothecation values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AP2C");
            msSQL = " insert into agr_mst_tsuprapplication2hypothecation(" +
                   " application2hypothecation_gid ," +
                   " application_gid," +
                   " securitytype_gid," +
                   " security_type," +
                   " security_description," +
                   " security_value ," +
                   " securityassessed_date," +
                   " asset_id," +
                   " roc_fillingid," +
                   " CERSAI_fillingid," +
                   " hypoobservation_summary," +
                   " primary_security," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.application_gid + "'," +
                   "'" + values.securitytype_gid + "'," +
                   "'" + values.security_type + "'," +
                   "'" + values.security_description + "',";
            if (values.security_value == null || values.security_value == "")
            {
                //msSQL += "'0.00',";
                msSQL += " security_value=null,";
            }
            else
            {
                msSQL += "'" + values.security_value.Replace(",", "") + "',";
            }
            if (values.securityassessed_date == null)
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.securityassessed_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL += "'" + values.asset_id + "'," +
                     "'" + values.roc_fillingid + "'," +
                     "'" + values.CERSAI_fillingid + "',";
            if (values.hypoobservation_summary == null || values.hypoobservation_summary == "")
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.hypoobservation_summary.Replace("'", "") + "',";
            }
            msSQL += "'" + values.primary_security + "'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Hypothecation details Added successfully";

                msSQL = "update agr_mst_tsupruploadhypothecationocument set application2hypothecation_gid='" + msGetGid + "' where application2hypothecation_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprapplication set hypothecation_flag='Y' where application_gid='" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select application2hypothecation_gid,securitytype_gid,security_type,security_description,security_value," +
                    " date_format(securityassessed_date,'%d-%m-%Y') as securityassessed_date,asset_id,roc_fillingid,CERSAI_fillingid," +
                    " hypoobservation_summary,primary_security " +
                    " from agr_mst_tsuprapplication2hypothecation where application_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gethypothecation_list = new List<hypothecation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gethypothecation_list.Add(new hypothecation_list
                        {
                            application2hypothecation_gid = (dr_datarow["application2hypothecation_gid"].ToString()),
                            securitytype_gid = (dr_datarow["securitytype_gid"].ToString()),
                            security_type = (dr_datarow["security_type"].ToString()),
                            security_description = (dr_datarow["security_description"].ToString()),
                            security_value = (dr_datarow["security_value"].ToString()),
                            securityassessed_date = (dr_datarow["securityassessed_date"].ToString()),
                            asset_id = (dr_datarow["asset_id"].ToString()),
                            roc_fillingid = (dr_datarow["roc_fillingid"].ToString()),
                            CERSAI_fillingid = (dr_datarow["CERSAI_fillingid"].ToString()),
                            hypoobservation_summary = (dr_datarow["hypoobservation_summary"].ToString()),
                            primary_security = (dr_datarow["primary_security"].ToString()),
                        });
                    }
                    values.hypothecation_list = gethypothecation_list;
                }
                dt_datatable.Dispose();
                msSQL = " select hypothecationdocument_gid,document_name,concat(date_format(a.created_date,'%d-%m-%Y %H:%i %p')) as uploaded_date,document_path, " +
                       " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as uploaded_by,a.document_title " +
                       " from agr_mst_tsupruploadhypothecationocument a,hrm_mst_temployee b, adm_mst_tuser c where a.created_by=b.employee_gid" +
                       " and b.user_gid = c.user_gid and application2hypothecation_gid='" + employee_gid + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_filename = new List<DocumentList>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_filename.Add(new DocumentList
                        {
                            //document_path = (dr_datarow["document_path"].ToString()),
                            document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                            document_name = (dr_datarow["document_name"].ToString()),
                            document_gid = (dr_datarow["hypothecationdocument_gid"].ToString()),
                            uploaded_by = dr_datarow["uploaded_by"].ToString(),
                            updated_date = dr_datarow["uploaded_date"].ToString(),
                            document_title = dr_datarow["document_title"].ToString()
                        });
                    }
                    values.DocumentList = get_filename;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while Adding";
            }
        }

        public void DaDeleteHypothecation(string application2hypothecation_gid, MdlMstHypothecation values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tsuprapplication2hypothecation where application2hypothecation_gid='" + application2hypothecation_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Hypothecation details Deleted successfully";

                msSQL = "select application2hypothecation_gid,securitytype_gid,security_type,security_description,security_value," +
                   " date_format(securityassessed_date,'%d-%m-%Y') as securityassessed_date,asset_id,roc_fillingid,CERSAI_fillingid," +
                   " hypoobservation_summary,primary_security " +
                   " from agr_mst_tsuprapplication2hypothecation where application_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var gethypothecation_list = new List<hypothecation_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        gethypothecation_list.Add(new hypothecation_list
                        {
                            application2hypothecation_gid = (dr_datarow["application2hypothecation_gid"].ToString()),
                            securitytype_gid = (dr_datarow["securitytype_gid"].ToString()),
                            security_type = (dr_datarow["security_type"].ToString()),
                            security_description = (dr_datarow["security_description"].ToString()),
                            security_value = (dr_datarow["security_value"].ToString()),
                            securityassessed_date = (dr_datarow["securityassessed_date"].ToString()),
                            asset_id = (dr_datarow["asset_id"].ToString()),
                            roc_fillingid = (dr_datarow["roc_fillingid"].ToString()),
                            CERSAI_fillingid = (dr_datarow["CERSAI_fillingid"].ToString()),
                            hypoobservation_summary = (dr_datarow["hypoobservation_summary"].ToString()),
                            primary_security = (dr_datarow["primary_security"].ToString()),
                        });
                    }
                    values.hypothecation_list = gethypothecation_list;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while deleting";
            }
        }
        public void DaDeleteBuyer(string application2buyer_gid, MdlMstBuyer values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tsuprapplication2buyer where application2buyer_gid='" + application2buyer_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Buyer Deleted successfully";

                msSQL = "select application2buyer_gid,buyer_name,buyer_gid,buyer_limit,availed_limit,balance_limit,margin,bill_tenure " +
                    " from agr_mst_tsuprapplication2buyer where application2loan_gid='" + employee_gid + "'";
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
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while deleting";
            }
        }

        public void DaDeleteCharge(string application2servicecharge_gid, MdlProductCharges values, string employee_gid)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "delete from agr_mst_tsuprapplicationservicecharge where application2servicecharge_gid='" + application2servicecharge_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Service Charges Deleted successfully";

                msSQL = " select application_gid,application2servicecharge_gid, processing_fee,processing_collectiontype,doc_charges," +
                     " doccharge_collectiontype,fieldvisit_charges,fieldvisit_charges_collectiontype,adhoc_fee,adhoc_collectiontype," +
                     " life_insurance,lifeinsurance_collectiontype,acct_insurance,total_collect,total_deduct,product_type,acctinsurance_collectiontype, " +
                     " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                     " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                     " from agr_mst_tsuprapplicationservicecharge a " +
                     " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                     " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                       " where a.application_gid = '" + lsapplication_gid + "' order by application2servicecharge_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getproductcharges_list = new List<servicecharges_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getproductcharges_list.Add(new servicecharges_list
                        {
                            application2servicecharge_gid = (dr_datarow["application2servicecharge_gid"].ToString()),
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
                            total_deduct = (dr_datarow["total_deduct"].ToString()),
                            product_type = (dr_datarow["product_type"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            acctinsurance_collectiontype = (dr_datarow["acctinsurance_collectiontype"].ToString()),
                        });
                    }
                    values.servicecharges_list = getproductcharges_list;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred while deleting";
            }
        }

        public void DaGetBuyerInfo(string buyer_gid, MdlMstBuyer values)
        {
            msSQL = "select buyer_limit from ocs_mst_tbuyer where buyer_gid='" + buyer_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.buyer_limit = objODBCDatareader["buyer_limit"].ToString();
                //values.buyer_limit = objODBCDatareader["buyer_limit"].ToString();
                //values.buyer_limit = objODBCDatareader["buyer_limit"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
        }

        public void DaPostProductCharges(string employee_gid, MdlProductCharges values)
        {

            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsapplication_gid != "" || lsapplication_gid != null)
            {
                msSQL = " update agr_mst_tsuprapplication set " +
                      " overalllimit_amount='" + values.overalllimit_amount + "'," +
                      " validityoveralllimit_year='" + values.validityoveralllimit_year + "'," +
                      " validityoveralllimit_month='" + values.validityoveralllimit_month + "'," +
                      " validityoveralllimit_days='" + values.validityoveralllimit_days + "'," +
                      " calculationoveralllimit_validity='" + values.calculationoveralllimit_validity + "',";
                if (values.enduse_purpose == null || values.enduse_purpose == "")
                {
                    msSQL += " enduse_purpose='',";
                }
                else
                {
                    msSQL += " enduse_purpose='" + values.enduse_purpose.Replace("'", "") + "',";
                }
                msSQL += " processing_fee='" + values.processing_fee + "'," +
                 " processing_collectiontype='" + values.processing_collectiontype + "'," +
                 " doc_charges='" + values.doc_charges + "'," +
                 " doccharge_collectiontype='" + values.doccharge_collectiontype + "'," +
                 " fieldvisit_charge='" + values.fieldvisit_charge + "'," +
                 " fieldvisit_collectiontype='" + values.fieldvisit_collectiontype + "'," +
                 " adhoc_fee='" + values.adhoc_fee + "'," +
                 " adhoc_collectiontype='" + values.adhoc_collectiontype + "'," +
                 " life_insurance='" + values.life_insurance + "'," +
                 " lifeinsurance_collectiontype='" + values.lifeinsurance_collectiontype + "'," +
                 " acct_insurance='" + values.acct_insurance + "'," +
                 " total_collect='" + values.total_collect + "'," +
                 " total_deduct='" + values.total_deduct + "'," +
                 " productcharge_flag='Y'," +
                 " productcharges_status='Incomplete'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where application_gid='" + lsapplication_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update agr_mst_tsuprapplication2loan set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    msSQL = " update agr_mst_tsuprapplication2collateral set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_mst_tsuprapplication2hypothecation set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    values.status = true;
                    values.message = "Product&Charges Details Saved Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured";
                }
            }
            else
            {
                msSQL = " update agr_mst_tsuprapplication set " +
                     " overalllimit_amount='" + values.overalllimit_amount + "'," +
                     " validityoveralllimit_year='" + values.validityoveralllimit_year + "'," +
                     " validityoveralllimit_month='" + values.validityoveralllimit_month + "'," +
                     " validityoveralllimit_days='" + values.validityoveralllimit_days + "'," +
                     " calculationoveralllimit_validity='" + values.calculationoveralllimit_validity + "',";
                if (values.enduse_purpose == null || values.enduse_purpose == "")
                {
                    msSQL += " enduse_purpose='',";
                }
                else
                {
                    msSQL += " enduse_purpose='" + values.enduse_purpose.Replace("'", "") + "',";
                }
                msSQL +=
                " processing_fee='" + values.processing_fee + "'," +
                      " processing_collectiontype='" + values.processing_collectiontype + "'," +
                      " doc_charges='" + values.doc_charges + "'," +
                      " doccharge_collectiontype='" + values.doccharge_collectiontype + "'," +
                      " fieldvisit_charge='" + values.fieldvisit_charge + "'," +
                      " fieldvisit_collectiontype='" + values.fieldvisit_collectiontype + "'," +
                      " adhoc_fee='" + values.adhoc_fee + "'," +
                      " adhoc_collectiontype='" + values.adhoc_collectiontype + "'," +
                      " life_insurance='" + values.life_insurance + "'," +
                      " lifeinsurance_collectiontype='" + values.lifeinsurance_collectiontype + "'," +
                      " acct_insurance='" + values.acct_insurance + "'," +
                      " total_collect='" + values.total_collect + "'," +
                      " productcharge_flag='Y'," +
                      " productcharges_status='Incomplete'," +
                      " total_deduct='" + values.total_deduct + "'," +
                      " updated_by='" + employee_gid + "'," +
                      " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                      " where application_gid='" + values.application_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update agr_mst_tsuprapplication2loan set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    msSQL = " update agr_mst_tsuprapplication2collateral set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_mst_tsuprapplication2hypothecation set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Product&Charges Details Saved Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured";
                }
            }

        }

        public void DaPostSubmitProductCharges(string employee_gid, MdlProductCharges values)
        {


            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsapplication_gid != "" || lsapplication_gid != null)
            {
                msSQL = "select application_gid from agr_mst_tsuprapplication2loan where  application_gid='" + employee_gid + "'  ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == false)
                {
                    values.status = false;
                    values.message = "Kindly add Loan Details";
                    return;
                }
                objODBCDatareader.Close();

                //msSQL = "select application_gid from agr_mst_tsuprapplication2loan where product_type='Agri Receivable Finance (ARF)' and application_gid='" + employee_gid + "' ";
                //objODBCDatareader = objdbconn.GetDataReader(msSQL);
                //if (objODBCDatareader.HasRows == true)
                //{
                //    //msSQL = "select application_gid from agr_mst_tsuprapplication2buyer where application_gid='" + employee_gid + "'";
                //    ////objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                //    ////if (objODBCDatareader1.HasRows == false)
                //    ////{
                //    ////    values.status = false;
                //    ////    values.message = "Kindly add Buyer Details";
                //    ////    return;
                //    ////}
                //    //objODBCDatareader1.Close();

                //}
                //objODBCDatareader.Close();

                msSQL = "select application_gid from agr_mst_tsuprapplication2loan where loan_type='Secured' and application_gid='" + employee_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    msSQL = "select application_gid from agr_mst_tsuprapplication2collateral where application_gid='" + employee_gid + "'";
                    objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader1.HasRows == false)
                    {
                        values.status = false;
                        values.message = "Kindly add Collateral Details";
                        return;
                    }
                    objODBCDatareader1.Close();
                }
                objODBCDatareader.Close();

                msSQL = " update agr_mst_tsuprapplication set " +
                      " overalllimit_amount='" + values.overalllimit_amount + "'," +
                      " validityoveralllimit_year='" + values.validityoveralllimit_year + "'," +
                      " validityoveralllimit_month='" + values.validityoveralllimit_month + "'," +
                      " validityoveralllimit_days='" + values.validityoveralllimit_days + "'," +
                      " calculationoveralllimit_validity='" + values.calculationoveralllimit_validity + "',";
                if (values.enduse_purpose == null || values.enduse_purpose == "")
                {
                    msSQL += " enduse_purpose='',";
                }
                else
                {
                    msSQL += " enduse_purpose='" + values.enduse_purpose.Replace("'", "") + "',";
                }
                msSQL += " processing_fee='" + values.processing_fee + "'," +
                       " processing_collectiontype='" + values.processing_collectiontype + "'," +
                       " doc_charges='" + values.doc_charges + "'," +
                       " doccharge_collectiontype='" + values.doccharge_collectiontype + "'," +
                       " fieldvisit_charge='" + values.fieldvisit_charge + "'," +
                       " fieldvisit_collectiontype='" + values.fieldvisit_collectiontype + "'," +
                       " adhoc_fee='" + values.adhoc_fee + "'," +
                       " adhoc_collectiontype='" + values.adhoc_collectiontype + "'," +
                       " life_insurance='" + values.life_insurance + "'," +
                       " lifeinsurance_collectiontype='" + values.lifeinsurance_collectiontype + "'," +
                       " acct_insurance='" + values.acct_insurance + "'," +
                       " total_collect='" + values.total_collect + "'," +
                       " total_deduct='" + values.total_deduct + "'," +
                       " productcharge_flag='Y'," +
                       " productcharges_status='Completed'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where application_gid='" + lsapplication_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update agr_mst_tsuprapplication2loan set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_mst_tsuprapplication2buyer set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_mst_tsuprapplication2collateral set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_mst_tsuprapplication2hypothecation set application_gid='" + lsapplication_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                    values.status = true;
                    values.message = "Product&Charges Details Submitted Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured";
                }
            }
            else
            {
                msSQL = " update agr_mst_tsuprapplication set " +
                    " overalllimit_amount='" + values.overalllimit_amount + "'," +
                    " validityoveralllimit_year='" + values.validityoveralllimit_year + "'," +
                    " validityoveralllimit_month='" + values.validityoveralllimit_month + "'," +
                    " validityoveralllimit_days='" + values.validityoveralllimit_days + "'," +
                    " calculationoveralllimit_validity='" + values.calculationoveralllimit_validity + "',";
                if (values.enduse_purpose == null || values.enduse_purpose == "")
                {
                    msSQL += " enduse_purpose='',";
                }
                else
                {
                    msSQL += " enduse_purpose='" + values.enduse_purpose.Replace("'", "") + "',";
                }
                msSQL +=
               " processing_fee='" + values.processing_fee + "'," +
                     " processing_collectiontype='" + values.processing_collectiontype + "'," +
                     " doc_charges='" + values.doc_charges + "'," +
                     " doccharge_collectiontype='" + values.doccharge_collectiontype + "'," +
                     " fieldvisit_charge='" + values.fieldvisit_charge + "'," +
                     " fieldvisit_collectiontype='" + values.fieldvisit_collectiontype + "'," +
                     " adhoc_fee='" + values.adhoc_fee + "'," +
                     " adhoc_collectiontype='" + values.adhoc_collectiontype + "'," +
                     " life_insurance='" + values.life_insurance + "'," +
                     " lifeinsurance_collectiontype='" + values.lifeinsurance_collectiontype + "'," +
                     " acct_insurance='" + values.acct_insurance + "'," +
                     " total_collect='" + values.total_collect + "'," +
                     " productcharge_flag='Y'," +
                     " productcharges_status='Completed'," +
                     " total_deduct='" + values.total_deduct + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where application_gid='" + values.application_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " update agr_mst_tsuprapplication2loan set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_mst_tsuprapplication2buyer set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_mst_tsuprapplication2collateral set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " update agr_mst_tsuprapplication2hypothecation set application_gid='" + values.application_gid + "' where application_gid='" + employee_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Product&Charges Details Submitted Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured";
                }
            }
        }
        public void DaSubmitApplication(string employee_gid, MdlProductCharges values)
        {


            msSQL = "update agr_mst_tsuprapplication set application_flag='Y' where  application_gid='" + values.application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Application Submitted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occurred while Submitting Application";
                values.status = true;
            }
        }

        // Institution GST Details

        public bool DaPostInstitutionGST(string employee_gid, MdlMstGST values)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select institution_gid from agr_mst_tsuprinstitution2branch where gst_no='" + values.gst_no + "' and institution_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("ITGS");
            msSQL = " insert into agr_mst_tsuprinstitution2branch(" +
                    " institution2branch_gid," +
                    " institution_gid," +
                    " gst_state," +
                    " gst_no," +
                    " gst_registered," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.gst_state + "'," +
                    "'" + values.gst_no + "'," +
                    "'" + values.gst_registered + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "GST Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public bool DaPostInstitutionGSTList(string employee_gid, MdlMstGST values)
        {

            InstitutionGSTDetails[] GstArray = values.GSTArray;
            string GSTValue, GSTStateCode, GSTState;

            for (int i = 0; i < GstArray.Length; i++)
            {
                GSTValue = GstArray[i].gstinId;
                GSTStateCode = GSTValue.Substring(0, 2);

                msSQL = "select gst_state from agr_mst_tgstcode2state where " +
                       " gst_code='" + GSTStateCode + "'";
                GSTState = objdbconn.GetExecuteScalar(msSQL);

                msGetGid = objcmnfunctions.GetMasterGID("ITGS");
                msSQL = " insert into agr_mst_tsuprinstitution2branch(" +
                    " institution2branch_gid," +
                    " institution_gid," +
                    " gst_state," +
                    " gst_no," +
                    " gst_registered," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + GSTState + "'," +
                    "'" + GSTValue + "'," +
                    "'" + "Yes" + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "GST Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetInstitutionGSTList(string employee_gid, MdlMstGST values)
        {
            msSQL = " select institution2branch_gid,gst_state,gst_no, gst_registered from agr_mst_tsuprinstitution2branch where institution_gid='" + employee_gid + "' ";
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
                        gst_registered = (dr_datarow["gst_registered"].ToString())
                    });
                }
                values.mstgst_list = getmstgst_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditInstitutionGST(string institution2branch_gid, MdlMstGST values)
        {
            try
            {
                msSQL = "select gst_state, gst_no, institution_gid, institution2branch_gid, gst_registered" +
                    " from agr_mst_tsuprinstitution2branch where institution2branch_gid='" + institution2branch_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.gst_state = objODBCDatareader["gst_state"].ToString();
                    values.gst_no = objODBCDatareader["gst_no"].ToString();
                    values.institution2branch_gid = objODBCDatareader["institution2branch_gid"].ToString();
                    values.institution_gid = objODBCDatareader["institution_gid"].ToString();
                    values.gst_registered = objODBCDatareader["gst_registered"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaUpdateInstitutionGST(string employee_gid, MdlMstGST values)
        {
            msSQL = "select gst_state, gst_no, gst_registered, institution_gid, institution2branch_gid" +
                " from agr_mst_tsuprinstitution2branch where institution2branch_gid='" + values.institution2branch_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsgst_state = objODBCDatareader["gst_state"].ToString();
                lsgst_no = objODBCDatareader["gst_no"].ToString();
                lsinstitution2branch_gid = objODBCDatareader["institution2branch_gid"].ToString();
                lsinstitution_gid = objODBCDatareader["institution_gid"].ToString();
                lsgst_registered = objODBCDatareader["gst_registered"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprinstitution2branch set " +
                         " gst_state='" + values.gst_state + "'," +
                         " gst_no='" + values.gst_no + "'," +
                         " gst_registered='" + values.gst_registered + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution2branch_gid='" + values.institution2branch_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IGUL");

                    msSQL = "Insert into agr_mst_tsuprinstitution2branchupdatelog(" +
                   " institution2gstupdatelog_gid, " +
                   " institution2branch_gid, " +
                   " institution_gid, " +
                   " gst_state," +
                   " gst_no," +
                   " gst_registered," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.institution2branch_gid + "'," +
                   "'" + values.institution_gid + "'," +
                   "'" + lsgst_state + "'," +
                   "'" + lsgst_no + "'," +
                   "'" + lsgst_registered + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "GST Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteInstitutionGST(string institution2branch_gid, MdlMstGST values)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2branch where institution2branch_gid='" + institution2branch_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprinstitution2branchupdatelog where institution2branch_gid='" + institution2branch_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "GST Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        // Institution Mobile Number

        public bool DaPostInstitutionMobileNo(string employee_gid, MdlMstMobileNo values)
        {
            msSQL = "select primary_status from agr_mst_tsuprinstitution2mobileno where primary_status='Yes' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                return false;
            }

            msSQL = "select institution2mobileno_gid from agr_mst_tsuprinstitution2mobileno where mobile_no='" + values.mobile_no + "' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Mobile Number Added";
                return false;
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("IT2M");
            msSQL = " insert into agr_mst_tsuprinstitution2mobileno(" +
                    " institution2mobileno_gid," +
                    " institution_gid," +
                    " mobile_no," +
                    " primary_status," +
                    " whatsapp_no," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.mobile_no + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.whatsapp_no + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Mobile Number Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetInstitutionMobileNoList(string employee_gid, MdlMstMobileNo values)
        {
            msSQL = "select mobile_no,institution2mobileno_gid,primary_status,whatsapp_no from agr_mst_tsuprinstitution2mobileno where " +
              " institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstmobileno_list = new List<mstmobileno_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstmobileno_list.Add(new mstmobileno_list
                    {
                        institution2mobileno_gid = (dr_datarow["institution2mobileno_gid"].ToString()),
                        mobile_no = (dr_datarow["mobile_no"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        whatsapp_no = (dr_datarow["whatsapp_no"].ToString()),
                    });
                }
                values.mstmobileno_list = getmstmobileno_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditInstitutionMobileNo(string institution2mobileno_gid, MdlMstMobileNo values)
        {
            try
            {
                msSQL = " select mobile_no,institution2mobileno_gid,primary_status,whatsapp_no from agr_mst_tsuprinstitution2mobileno where " +
                        " institution2mobileno_gid='" + institution2mobileno_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.mobile_no = objODBCDatareader["mobile_no"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.whatsapp_no = objODBCDatareader["whatsapp_no"].ToString();
                    values.institution2mobileno_gid = objODBCDatareader["institution2mobileno_gid"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaUpdateInstitutionMobileNo(string employee_gid, MdlMstMobileNo values)
        {
            msSQL = " select mobile_no,institution2mobileno_gid,primary_status,whatsapp_no from agr_mst_tsuprinstitution2mobileno where " +
                    " institution2mobileno_gid='" + values.institution2mobileno_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsmobile_no = objODBCDatareader["mobile_no"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lswhatsapp_no = objODBCDatareader["whatsapp_no"].ToString();
                lsinstitution2mobileno_gid = objODBCDatareader["institution2mobileno_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprinstitution2mobileno set " +
                         " mobile_no='" + values.mobile_no + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " whatsapp_no='" + values.whatsapp_no + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution2mobileno_gid='" + values.institution2mobileno_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IMUL");

                    msSQL = "Insert into agr_mst_tsuprinstitution2mobilenoupdatelog(" +
                   " institution2mobilenoupdatelog_gid, " +
                   " institution2mobileno_gid, " +
                   " institution_gid, " +
                   " mobile_no," +
                   " primary_status," +
                   " whatsapp_no," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.institution2mobileno_gid + "'," +
                   "'" + values.institution_gid + "'," +
                   "'" + lsmobile_no + "'," +
                   "'" + lsprimary_status + "'," +
                   "'" + lswhatsapp_no + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Institution Mobile Number Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteInstitutionMobileNo(string institution2mobileno_gid, MdlMstMobileNo values)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2mobileno where institution2mobileno_gid='" + institution2mobileno_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprinstitution2mobilenoupdatelog where institution2mobileno_gid='" + institution2mobileno_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Mobile Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
            }
        }

        // Institution Email Address

        public bool DaPostInstitutionEmailAddress(string employee_gid, MdlMstEmailAddress values)
        {
            msSQL = "select primary_status from agr_mst_tsuprinstitution2email where primary_status='Yes' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {

                values.status = false;
                values.message = "Already Primary Email Address Added";
                return false;
            }
            msSQL = "select institution2email_gid from agr_mst_tsuprinstitution2email where email_address='" + values.email_address + "' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already This Email Address Added";
                return false;
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("IT2E");
            msSQL = " insert into agr_mst_tsuprinstitution2email(" +
                    " institution2email_gid," +
                    " institution_gid," +
                    " email_address," +
                    " primary_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.email_address + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Email Address Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetInstitutionEmailAddressList(string employee_gid, MdlMstEmailAddress values)
        {
            msSQL = " select email_address,institution2email_gid,primary_status from agr_mst_tsuprinstitution2email where institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstemailaddress_list = new List<mstemailaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstemailaddress_list.Add(new mstemailaddress_list
                    {
                        institution2email_gid = (dr_datarow["institution2email_gid"].ToString()),
                        email_address = (dr_datarow["email_address"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString())
                    });
                }
                values.mstemailaddress_list = getmstemailaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditInstitutionEmailAddress(string institution2email_gid, MdlMstEmailAddress values)
        {
            try
            {
                msSQL = " select email_address,institution2email_gid,primary_status from agr_mst_tsuprinstitution2email where " +
                        " institution2email_gid='" + institution2email_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.email_address = objODBCDatareader["email_address"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.institution2email_gid = objODBCDatareader["institution2email_gid"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaUpdateInstitutionEmailAddress(string employee_gid, MdlMstEmailAddress values)
        {
            msSQL = " select email_address,institution2email_gid,primary_status from agr_mst_tsuprinstitution2email where " +
                        " institution2email_gid='" + values.institution2email_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsemail_address = objODBCDatareader["email_address"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lsinstitution2email_gid = objODBCDatareader["institution2email_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprinstitution2email set " +
                         " email_address='" + values.email_address + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution2email_gid='" + values.institution2email_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IEUL");

                    msSQL = "Insert into agr_mst_tsuprinstitution2emailupdatelog(" +
                   " institution2emailaddressupdatelog_gid, " +
                   " institution2email_gid, " +
                   " institution_gid, " +
                   " email_address," +
                   " primary_status," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.institution2email_gid + "'," +
                   "'" + values.institution_gid + "'," +
                   "'" + lsemail_address + "'," +
                   "'" + lsprimary_status + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "Email Address Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteInstitutionEmailAddress(string institution2email_gid, MdlMstEmailAddress values)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2email where institution2email_gid='" + institution2email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprinstitution2emailupdatelog where institution2email_gid='" + institution2email_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Email Address Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        // Institution Address Details

        public bool DaPostInstitutionAddressDetail(string employee_gid, string user_gid, MdlMstAddressDetails values)
        {
            msSQL = "select primary_status from agr_mst_tsuprinstitution2address where primary_status='Yes' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Address Added";
                return false;
            }
            msSQL = "select institution2address_gid from agr_mst_tsuprinstitution2address where addresstype_name='" + values.address_type + "' and (institution_gid='" + employee_gid + "' or institution_gid='" + values.institution_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Address Type Added";
                return false;
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("IT2A");
            msSQL = " insert into agr_mst_tsuprinstitution2address(" +
                    " institution2address_gid," +
                    " institution_gid," +
                    " addresstype_gid," +
                    " addresstype_name," +
                    " addressline1," +
                    " addressline2," +
                    " primary_status," +
                    " landmark," +
                    " postal_code," +
                    " city," +
                    " taluka," +
                    " district," +
                    " state," +
                    " country," +
                    " latitude," +
                    " longitude," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.address_typegid + "'," +
                    "'" + values.address_type + "'," +
                    "'" + values.addressline1 + "'," +
                    "'" + values.addressline2 + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.landmark + "'," +
                    "'" + values.postal_code + "'," +
                    "'" + values.city + "'," +
                    "'" + values.taluka + "'," +
                    "'" + values.district + "'," +
                    "'" + values.state + "'," +
                    "'" + values.country + "'," +
                    "'" + values.latitude + "'," +
                    "'" + values.longitude + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Address Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }

        public void DaGetInstitutionAddressList(string employee_gid, MdlMstAddressDetails values)
        {
            msSQL = "  select institution2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, landmark, latitude, longitude," +
                    " postal_code from agr_mst_tsuprinstitution2address where institution_gid='" + employee_gid + "'";
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
                        landmark = (dr_datarow["landmark"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                    });
                }
                values.mstaddress_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditInstitutionAddressDetail(string institution2address_gid, MdlMstAddressDetails values)
        {
            try
            {
                msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, institution_gid, institution2address_gid " +
                    " from agr_mst_tsuprinstitution2address where institution2address_gid='" + institution2address_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.address_typegid = objODBCDatareader["addresstype_gid"].ToString();
                    values.address_type = objODBCDatareader["addresstype_name"].ToString();
                    values.addressline1 = objODBCDatareader["addressline1"].ToString();
                    values.addressline2 = objODBCDatareader["addressline2"].ToString();
                    values.landmark = objODBCDatareader["landmark"].ToString();
                    values.taluka = objODBCDatareader["taluka"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.postal_code = objODBCDatareader["postal_code"].ToString();
                    values.city = objODBCDatareader["city"].ToString();
                    values.district = objODBCDatareader["district"].ToString();
                    values.state = objODBCDatareader["state"].ToString();
                    values.country = objODBCDatareader["country"].ToString();
                    values.latitude = objODBCDatareader["latitude"].ToString();
                    values.longitude = objODBCDatareader["longitude"].ToString();
                    values.institution_gid = objODBCDatareader["institution_gid"].ToString();
                    values.institution2address_gid = objODBCDatareader["institution2address_gid"].ToString();
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

        public void DaUpdateInstitutionAddressDetail(string employee_gid, MdlMstAddressDetails values)
        {
            msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, institution_gid, institution2address_gid " +
                    " from agr_mst_tsuprinstitution2address where institution2address_gid='" + values.institution2address_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsaddress_typegid = objODBCDatareader["addresstype_gid"].ToString();
                lsaddress_type = objODBCDatareader["addresstype_name"].ToString();
                lsaddressline1 = objODBCDatareader["addressline1"].ToString();
                lsaddressline2 = objODBCDatareader["addressline2"].ToString();
                lslandmark = objODBCDatareader["landmark"].ToString();
                lstaluka = objODBCDatareader["taluka"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lspostal_code = objODBCDatareader["postal_code"].ToString();
                lscity = objODBCDatareader["city"].ToString();
                lsdistrict = objODBCDatareader["district"].ToString();
                lsstate = objODBCDatareader["state"].ToString();
                lscountry = objODBCDatareader["country"].ToString();
                lslatitude = objODBCDatareader["latitude"].ToString();
                lslongitude = objODBCDatareader["longitude"].ToString();
                lsinstitution_gid = objODBCDatareader["institution_gid"].ToString();
                lsinstitution2address_gid = objODBCDatareader["institution2address_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprinstitution2address set " +
                         " addresstype_gid='" + values.address_typegid + "'," +
                         " addresstype_name='" + values.address_type + "'," +
                         " addressline1='" + values.addressline1 + "'," +
                         " addressline2='" + values.addressline2 + "'," +
                         " landmark='" + values.landmark + "'," +
                         " taluka='" + values.taluka + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " postal_code='" + values.postal_code + "'," +
                         " city='" + values.city + "'," +
                         " district='" + values.district + "'," +
                         " state='" + values.state + "'," +
                         " country='" + values.country + "'," +
                         " latitude='" + values.latitude + "'," +
                         " longitude='" + values.longitude + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution2address_gid='" + values.institution2address_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("IAUL");

                    msSQL = " insert into agr_mst_tsuprinstitution2addressupdatelog(" +
                  " institution2addressupdatelog_gid," +
                  " institution2address_gid," +
                  " institution_gid," +
                  " addresstype_gid," +
                  " addresstype_name," +
                  " addressline1," +
                  " addressline2," +
                  " primary_status," +
                  " landmark," +
                  " postal_code," +
                  " city," +
                  " taluka," +
                  " district," +
                  " state," +
                  " country," +
                  " latitude," +
                  " longitude," +
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.institution2address_gid + "'," +
                  "'" + lsaddress_typegid + "'," +
                  "'" + lsaddress_type + "'," +
                  "'" + lsaddressline1 + "'," +
                  "'" + lsaddressline2 + "'," +
                  "'" + lsprimary_status + "'," +
                  "'" + lslandmark + "'," +
                  "'" + lspostal_code + "'," +
                  "'" + lscity + "'," +
                  "'" + lstaluka + "'," +
                  "'" + lsdistrict + "'," +
                  "'" + lsstate + "'," +
                  "'" + lscountry + "'," +
                  "'" + lslatitude + "'," +
                  "'" + lslongitude + "'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Address Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteInstitutionAddressDetail(string institution2address_gid, string employee_gid, MdlMstAddressDetails values)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2address where institution2address_gid='" + institution2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprinstitution2addressupdatelog where institution2address_gid='" + institution2address_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Address Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        // Institution License Details

        public bool DaPostInstitutionLicenseDetail(string employee_gid, string user_gid, MdlMstLicenseDetails values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("IT2L");
            msSQL = " insert into agr_mst_tsuprinstitution2licensedtl(" +
                    " institution2licensedtl_gid," +
                    " institution_gid," +
                    " licensetype_gid," +
                    " licensetype_name," +
                    " license_no," +
                    " issue_date," +
                    " expiry_date," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.licensetype_gid + "'," +
                    "'" + values.licensetype_name + "'," +
                    "'" + values.license_number + "',";
            if ((values.licenseissue_date == null) || (values.licenseissue_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.licenseissue_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.licenseexpiry_date == null) || (values.licenseexpiry_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.licenseexpiry_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + employee_gid + "'," +
             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "License Details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }

        }

        public void DaGetInstitutionLicenseList(string employee_gid, MdlMstLicenseDetails values)
        {
            msSQL = " select institution2licensedtl_gid,licensetype_gid,licensetype_name,license_no,date_format(issue_date,'%d-%m-%Y') as issue_date," +
                    " date_format(expiry_date,'%d-%m-%Y') as expiry_date from agr_mst_tsuprinstitution2licensedtl" +
                    " where institution_gid='" + employee_gid + "'";
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
        }

        public void DaEditInstitutionLicenseDetail(string institution2licensedtl_gid, MdlMstLicenseDetails values)
        {
            try
            {
                //msSQL = " select institution2licensedtl_gid,licensetype_gid,licensetype_name,license_no,date_format(issue_date,'%d-%m-%Y') as issue_date," +
                //   " date_format(expiry_date,'%d-%m-%Y') as expiry_date, date_format(expiry_date,'%Y-%m-%d') as expiry_dateedit,date_format(issue_date,'%Y-%m-%d') as issue_dateedit,institution_gid from agr_mst_tsuprinstitution2licensedtl" +
                //   " where institution2licensedtl_gid='" + institution2licensedtl_gid + "'";

                msSQL = " select institution2licensedtl_gid,licensetype_gid,licensetype_name,license_no, date_format(issue_date,'%d-%m-%Y') as issue_date," +
                  " date_format(expiry_date,'%d-%m-%Y') as expiry_date, institution_gid from agr_mst_tsuprinstitution2licensedtl" +
                  " where institution2licensedtl_gid='" + institution2licensedtl_gid + "'";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.licensetype_gid = objODBCDatareader["licensetype_gid"].ToString();
                    values.licensetype_name = objODBCDatareader["licensetype_name"].ToString();
                    values.license_number = objODBCDatareader["license_no"].ToString();
                    values.licenseissue_date = objODBCDatareader["issue_date"].ToString();
                    values.licenseexpiry_date = objODBCDatareader["expiry_date"].ToString();
                    //values.licenseissue_dateedit = objODBCDatareader["issue_dateedit"].ToString();
                    //values.licenseexpiry_dateedit = objODBCDatareader["expiry_dateedit"].ToString();
                    values.institution2licensedtl_gid = objODBCDatareader["institution2licensedtl_gid"].ToString();
                    values.institution_gid = objODBCDatareader["institution_gid"].ToString();
                }
                values.status = true;
                values.message = "success";
                objODBCDatareader.Close();
            }
            catch
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaUpdateInstitutionLicenseDetail(string employee_gid, MdlMstLicenseDetails values)
        {
            msSQL = " select institution2licensedtl_gid,licensetype_gid,licensetype_name,license_no,date_format(issue_date,'%d-%m-%Y') as issue_date," +
                  " date_format(expiry_date,'%d-%m-%Y') as expiry_date, institution_gid from agr_mst_tsuprinstitution2licensedtl" +
                  " where institution2licensedtl_gid='" + values.institution2licensedtl_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lslicensetype_gid = objODBCDatareader["licensetype_gid"].ToString();
                lslicensetype_name = objODBCDatareader["licensetype_name"].ToString();
                lslicense_number = objODBCDatareader["license_no"].ToString();
                lslicenseissue_date = objODBCDatareader["issue_date"].ToString();
                lslicenseexpiry_date = objODBCDatareader["expiry_date"].ToString();
                lsinstitution2licensedtl_gid = objODBCDatareader["institution2licensedtl_gid"].ToString();
                lsinstitution_gid = objODBCDatareader["institution_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprinstitution2licensedtl set " +
                         " licensetype_gid='" + values.licensetype_gid + "'," +
                         " licensetype_name='" + values.licensetype_name + "'," +
                         " license_no='" + values.license_number + "',";
                if (Convert.ToDateTime(values.licenseissue_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " issue_date='" + Convert.ToDateTime(values.licenseissue_date).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                if (Convert.ToDateTime(values.licenseexpiry_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {

                }
                else
                {
                    msSQL += " expiry_date='" + Convert.ToDateTime(values.licenseexpiry_date).ToString("yyyy-MM-dd 00:00:00") + "',";
                }
                msSQL += " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where institution2licensedtl_gid='" + values.institution2licensedtl_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("ILUL");

                    msSQL = "Insert into agr_mst_tsuprinstitution2licenseupdatelog(" +
                   " institution2licenseupdatelog_gid, " +
                   " institution2licensedtl_gid, " +
                   " institution_gid, " +
                   " licensetype_gid," +
                   " licensetype_name," +
                   " license_no," +
                   " issue_date," +
                   " expiry_date," +
                   " created_by," +
                   " created_date)" +
                   " values (" +
                   "'" + msGetGid + "'," +
                   "'" + values.institution2licensedtl_gid + "'," +
                   "'" + values.institution_gid + "'," +
                   "'" + lslicensetype_gid + "'," +
                   "'" + lslicensetype_name + "'," +
                   "'" + lslicense_number + "'," +
                   "'" + lslicenseissue_date + "'," +
                   "'" + lslicenseexpiry_date + "'," +
                   "'" + employee_gid + "'," +
                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "License Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteInstitutionLicenseDetail(string institution2licensedtl_gid, MdlMstLicenseDetails values)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2licensedtl where institution2licensedtl_gid='" + institution2licensedtl_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprinstitution2licenseupdatelog where institution2licensedtl_gid='" + institution2licensedtl_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "License Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaInstitutionDocumentUpload(HttpRequest httpRequest, institutionuploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lsdocument_id = httpRequest.Form["document_id"].ToString();
            string lscompanydocument_gid = httpRequest.Form["companydocument_gid"].ToString();
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msSQL = "select covenant_type from ocs_mst_tcompanydocument where companydocument_gid='" + lscompanydocument_gid + "'";
                        string lscovenant_type = objdbconn.GetExecuteScalar(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("INDO");
                        msSQL = " insert into agr_mst_tsuprinstitution2documentupload( " +
                                    " institution2documentupload_gid," +
                                    " institution_gid," +
                                    " document_title ," +
                                    " document_id," +
                                    " document_name ," +
                                    " document_path," +
                                    " companydocument_gid, " +
                                    " covenant_type," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + lsdocument_title + "'," +
                                    "'" + lsdocument_id + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lscompanydocument_gid + "'," +
                                    "'" + lscovenant_type + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }

                        msSQL = " select institution2documentupload_gid,institution_gid,document_name,document_path,document_title,document_id from agr_mst_tsuprinstitution2documentupload " +
                                " where institution_gid='" + employee_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getdocumentdtlList = new List<institutionupload_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getdocumentdtlList.Add(new institutionupload_list
                                {
                                    document_name = dt["document_name"].ToString(),
                                    //document_path = (dt["document_path"].ToString()),
                                    document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                                    institution_gid = dt["institution_gid"].ToString(),
                                    institution2documentupload_gid = dt["institution2documentupload_gid"].ToString(),
                                    document_title = dt["document_title"].ToString(),
                                    document_id = dt["document_id"].ToString(),
                                });
                                objfilename.institutionupload_list = getdocumentdtlList;
                            }
                        }
                        dt_datatable.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaInstitutionDocumentDelete(string institution2documentupload_gid, institutionuploaddocument objfilename, string employee_gid)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2documentupload where institution2documentupload_gid='" + institution2documentupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult == 1)
            {
                msSQL = " select groupdocumentchecklist_gid from agr_trn_tsuprdocumentchecktls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                string lsgroupdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsgroupdocumentchecklist_gid != "")
                {
                    msSQL = " select count(*) as documentcount from agr_trn_tsuprdocumentchecktls " +
                            " where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tsuprgroupdocumentchecklist where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                msSQL = " select groupcovdocumentchecklist_gid from agr_trn_tsuprcovanantdocumentcheckdtls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                string lschecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lschecklist_gid != "")
                {
                    msSQL = " select count(*) as documentcount from agr_trn_tsuprcovanantdocumentcheckdtls " +
                      " where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tsuprgroupcovenantdocumentchecklist where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                msSQL = "delete from agr_trn_tsuprcovanantdocumentcheckdtls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from agr_trn_tsuprdocumentchecktls where documentuploaded_gid='" + institution2documentupload_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }

            msSQL = " select institution2documentupload_gid,institution_gid,document_name,document_path,document_title,document_id from agr_mst_tsuprinstitution2documentupload " +
                               " where institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<institutionupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new institutionupload_list
                    {
                        document_name = dt["document_name"].ToString(),
                        //document_path = (dt["document_path"].ToString()),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        institution_gid = dt["institution_gid"].ToString(),
                        institution2documentupload_gid = dt["institution2documentupload_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                        document_id = dt["document_id"].ToString(),
                    });
                    objfilename.institutionupload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                objfilename.message = "Document Deleted Successfully";
                objfilename.status = true;
            }
            else
            {
                objfilename.message = "Error Occured";
                objfilename.status = false;

            }
        }

        public bool DaInstitutionForm_60DocumentUpload(HttpRequest httpRequest, institutionuploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IntitutionForm_60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IntitutionForm_60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/IntitutionForm_60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IntitutionForm_60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("IF6D");
                        msSQL = " insert into agr_mst_tsuprinstitution2form60documentupload( " +
                                    " institution2form60documentupload_gid, " +
                                    " institution_gid," +
                                    " form60document_name ," +
                                    " form60document_path," +
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
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }

                        msSQL = " select institution2form60documentupload_gid,form60document_name,form60document_path from agr_mst_tsuprinstitution2form60documentupload " +
                                " where institution_gid='" + employee_gid + "'";
                        dt_datatable = objdbconn.GetDataTable(msSQL);
                        var getdocumentdtlList = new List<institutionupload_list>();
                        if (dt_datatable.Rows.Count != 0)
                        {
                            foreach (DataRow dt in dt_datatable.Rows)
                            {
                                getdocumentdtlList.Add(new institutionupload_list
                                {
                                    document_name = dt["form60document_name"].ToString(),
                                    //document_path = (dt["form60document_path"].ToString()),
                                    document_path = objcmnstorage.EncryptData((dt["form60document_path"].ToString())),
                                    institution2form60documentupload_gid = dt["institution2form60documentupload_gid"].ToString()
                                });
                                objfilename.institutionupload_list = getdocumentdtlList;
                            }
                        }
                        dt_datatable.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaInstitutionForm_60DocumentDelete(string institution2form60documentupload_gid, institutionuploaddocument objfilename, string employee_gid)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2form60documentupload where institution2form60documentupload_gid='" + institution2form60documentupload_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select institution2form60documentupload_gid,form60document_name,form60document_path from agr_mst_tsuprinstitution2form60documentupload " +
                                " where institution_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<institutionupload_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new institutionupload_list
                    {
                        document_name = dt["form60document_name"].ToString(),
                        //document_path = (dt["form60document_path"].ToString()),
                        document_path = objcmnstorage.EncryptData((dt["form60document_path"].ToString())),
                        institution2form60documentupload_gid = dt["institution2form60documentupload_gid"].ToString()
                    });
                    objfilename.institutionupload_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                objfilename.message = "Document Deleted Successfully";
                objfilename.status = true;
            }
            else
            {
                objfilename.message = "Error Occured";
                objfilename.status = false;

            }
        }

        public bool DaSaveInstitutionDtl(MdlMstInstitutionAdd values, string employee_gid)
        {

            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select stakeholder_type from agr_mst_tsuprcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            string lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholder_type)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return false;
            }

            msSQL = "select stakeholder_type from agr_mst_tsuprinstitution where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholder_type)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("APIN");
            msSQL = " insert into agr_mst_tsuprinstitution(" +
                " institution_gid," +
                " application_gid," +
                " company_name," +
                " date_incorporation," +
                " businessstart_date," +
                " year_business," +
                " month_business," +
                " companypan_no," +
                " cin_no," +
                " official_telephoneno," +
                " officialemail_address," +
                " companytype_gid," +
                " companytype_name," +
                " stakeholdertype_gid," +
                " stakeholder_type," +
                " assessmentagency_gid," +
                " assessmentagency_name," +
                " assessmentagencyrating_gid," +
                " assessmentagencyrating_name," +
                " ratingas_on," +
                " amlcategory_gid," +
                " amlcategory_name," +
                " businesscategory_gid," +
                " businesscategory_name," +
                " contactperson_firstname," +
                " contactperson_middlename," +
                " contactperson_lastname," +
                " designation_gid," +
                " designation," +
                " start_date," +
                " end_date," +
                " lastyear_turnover," +
                " escrow," +
                " urn_status," +
                " urn," +
                " institution_status," +
                " created_by," +
                " created_date) values(" +
                  "'" + msGetGid + "'," +
                  "'" + lsapplication_gid + "'," +
                  "'" + values.company_name + "',";
            if ((values.date_incorporation == null) || (values.date_incorporation == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.date_incorporation).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.businessstartdate == null) || (values.businessstartdate == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.businessstartdate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.year_business + "'," +
                    "'" + values.month_business + "'," +
                    "'" + values.companypan_no + "'," +
                    "'" + values.cin_no + "'," +
                    "'" + values.official_telephoneno + "'," +
                    "'" + values.official_mailid + "'," +
                    "'" + values.companytype_gid + "'," +
                    "'" + values.companytype_name + "'," +
                    "'" + values.stakeholdertype_gid + "'," +
                    "'" + values.stakeholder_type + "'," +
                    "'" + values.assessmentagency_gid + "'," +
                    "'" + values.assessmentagency_name + "'," +
                    "'" + values.assessmentagencyrating_gid + "'," +
                    "'" + values.assessmentagencyrating_name + "',";
            if ((values.ratingas_on == null) || (values.ratingas_on == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.ratingas_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.amlcategory_gid + "'," +
                    "'" + values.amlcategory_name + "'," +
                    "'" + values.businesscategory_gid + "'," +
                    "'" + values.businesscategory_name + "'," +
                    "'" + values.contactperson_firstname + "'," +
                    "'" + values.contactperson_middlename + "'," +
                    "'" + values.contactperson_lastname + "'," +
                    "'" + values.designation_gid + "'," +
                    "'" + values.designation + "',";
            if ((values.start_date == null) || (values.start_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.start_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.end_date == null) || (values.end_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.end_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.lastyear_turnover + "'," +
                    "'" + values.escrow + "'," +
                    "'" + values.urn_status + "'," +
                    "'" + values.urn + "'," +
                    "'Incomplete'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update agr_mst_tsuprinstitution2branch set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprinstitution2mobileno set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprinstitution2email set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprinstitution2address set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprinstitution2ratingdetail set institution_gid='" + msGetGid + "', application_gid ='" + lsapplication_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprinstitution2licensedtl set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select institution2documentupload_gid, companydocument_gid from agr_mst_tsuprinstitution2documentupload where institution_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lscovenant_type = "", lsdocumenttype_gid = "", lsdocumenttype_name = "", lscompanydocument_name = "";

                    string msGetdefDocchecklistGID = objcmnfunctions.GetMasterGID("DOCG");
                    msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                                " from ocs_mst_tcompanydocument where companydocument_gid='" + dt["companydocument_gid"].ToString() + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                        lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                        lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                        lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = " insert into agr_trn_tsuprdocumentchecktls(" +
                            " documentcheckdtl_gid," +
                            " application_gid," +
                            " credit_gid, " +
                            " companydocument_gid, " +
                            " documentuploaded_gid, " +
                            " documenttype_gid," +
                        " documenttype_code," +
                        " documenttype_name," +
                        " covenant_type, " +
                        " tagged_by, " +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetdefDocchecklistGID + "'," +
                        "'" + lsapplication_gid + "'," +
                        "'" + msGetGid + "'," +
                        "'" + dt["companydocument_gid"].ToString() + "'," +
                        "'" + dt["institution2documentupload_gid"].ToString() + "'," +
                        "'" + lsdocumenttype_gid + "'," +
                        "'" + lsdocumenttype_name + "'," +
                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                        "'" + lscovenant_type + "'," +
                        "'N'," +
                        "current_timestamp," +
                        "'" + employee_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (lscovenant_type == "Y")
                    {
                        string msGetDocchecklistGID = objcmnfunctions.GetMasterGID("CDCL");
                        msSQL = " insert into agr_trn_tsuprcovanantdocumentcheckdtls(" +
                       " covenantdocumentcheckdtl_gid," +
                       " application_gid," +
                       " credit_gid," +
                       " companydocument_gid," +
                       " documentuploaded_gid, " +
                       " documenttype_gid," +
                       " documenttype_code," +
                       " documenttype_name," +
                       " covenant_type, " +
                       " tagged_by, " +
                       " created_date," +
                       " created_by)" +
                       " VALUES(" +
                       "'" + msGetDocchecklistGID + "'," +
                       "'" + lsapplication_gid + "'," +
                       "'" + msGetGid + "'," +
                       "'" + dt["companydocument_gid"].ToString() + "'," +
                       "'" + dt["institution2documentupload_gid"].ToString() + "'," +
                       "'" + lsdocumenttype_gid + "'," +
                       "'" + lsdocumenttype_name + "'," +
                       "'" + lscompanydocument_name.Replace("'", "") + "'," +
                       "'" + lscovenant_type + "'," +
                       "'N'," +
                       "current_timestamp," +
                       "'" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                DaAgrMstSuprScannedDocument objvalues = new DaAgrMstSuprScannedDocument();
                objvalues.DaGroupDocChecklistinfo(lsapplication_gid, msGetGid, employee_gid);

                msSQL = "update agr_mst_tsuprinstitution2documentupload set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprinstitution2form60documentupload set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprkycgstsbpan set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Institution Information Saved Successfully";
                values.status = true;
                return true;

            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
                return false;
            }
        }

        public bool DaSubmitInstitutionDtl(MdlMstInstitutionAdd values, string employee_gid)
        {
            msSQL = "select institution_gid from agr_mst_tsuprinstitution2mobileno where institution_gid='" + employee_gid + "' and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = "select institution_gid from agr_mst_tsuprinstitution2mobileno where institution_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Mobile Number";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = "select institution_gid from agr_mst_tsuprinstitution2email where institution_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Email Address";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = "select institution_gid from agr_mst_tsuprinstitution2address where institution_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast One Address Detail";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select stakeholder_type from agr_mst_tsuprcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            string lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholder_type)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return false;
            }

            msSQL = "select stakeholder_type from agr_mst_tsuprinstitution where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholder_type)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("APIN");
            msSQL = " insert into agr_mst_tsuprinstitution(" +
                " institution_gid," +
                " application_gid," +
                " company_name," +
                " date_incorporation," +
                " businessstart_date," +
                " year_business," +
                " month_business," +
                " companypan_no," +
                " cin_no," +
                " official_telephoneno," +
                " officialemail_address," +
                " companytype_gid," +
                " companytype_name," +
                " stakeholdertype_gid," +
                " stakeholder_type," +
                " assessmentagency_gid," +
                " assessmentagency_name," +
                " assessmentagencyrating_gid," +
                " assessmentagencyrating_name," +
                " ratingas_on," +
                " amlcategory_gid," +
                " amlcategory_name," +
                " businesscategory_gid," +
                " businesscategory_name," +
                " contactperson_firstname," +
                " contactperson_middlename," +
                " contactperson_lastname," +
                " designation_gid," +
                " designation," +
                " start_date," +
                " end_date," +
                " lastyear_turnover," +
                " escrow," +
                " urn_status," +
                " urn," +
                " institution_status," +
                " created_by," +
                " created_date) values(" +
                  "'" + msGetGid + "'," +
                  "'" + lsapplication_gid + "'," +
                  "'" + values.company_name + "',";
            if ((values.date_incorporation == null) || (values.date_incorporation == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.date_incorporation).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.businessstartdate == null) || (values.businessstartdate == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.businessstartdate).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.year_business + "'," +
                    "'" + values.month_business + "'," +
                    "'" + values.companypan_no + "'," +
                    "'" + values.cin_no + "'," +
                    "'" + values.official_telephoneno + "'," +
                    "'" + values.official_mailid + "'," +
                    "'" + values.companytype_gid + "'," +
                    "'" + values.companytype_name + "'," +
                    "'" + values.stakeholdertype_gid + "'," +
                    "'" + values.stakeholder_type + "'," +
                    "'" + values.assessmentagency_gid + "'," +
                    "'" + values.assessmentagency_name + "'," +
                    "'" + values.assessmentagencyrating_gid + "'," +
                    "'" + values.assessmentagencyrating_name + "',";
            if ((values.ratingas_on == null) || (values.ratingas_on == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.ratingas_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.amlcategory_gid + "'," +
                    "'" + values.amlcategory_name + "'," +
                    "'" + values.businesscategory_gid + "'," +
                    "'" + values.businesscategory_name + "'," +
                    "'" + values.contactperson_firstname + "'," +
                    "'" + values.contactperson_middlename + "'," +
                    "'" + values.contactperson_lastname + "'," +
                    "'" + values.designation_gid + "'," +
                    "'" + values.designation + "',";
            if ((values.start_date == null) || (values.start_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.start_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            if ((values.end_date == null) || (values.end_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.end_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }
            msSQL += "'" + values.lastyear_turnover + "'," +
                    "'" + values.escrow + "'," +
                    "'" + values.urn_status + "'," +
                    "'" + values.urn + "'," +
                    "'Completed'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "update agr_mst_tsuprinstitution2branch set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprinstitution2mobileno set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprinstitution2email set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprinstitution2address set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprinstitution2licensedtl set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprinstitution2ratingdetail set institution_gid='" + msGetGid + "', application_gid ='" + lsapplication_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "select companydocument_gid , institution2documentupload_gid from agr_mst_tsuprinstitution2documentupload where institution_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);

                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lscovenant_type = "", lsdocumenttype_gid = "", lsdocumenttype_name = "", lscompanydocument_name = "";

                    string msGetdefDocchecklistGID = objcmnfunctions.GetMasterGID("DOCG");
                    msSQL = " select companydocument_gid,documenttypes_gid,documenttype_name,companydocument_name,covenant_type " +
                                " from ocs_mst_tcompanydocument where companydocument_gid='" + dt["companydocument_gid"].ToString() + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                        lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                        lscompanydocument_name = objODBCDatareader["companydocument_name"].ToString();
                        lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = " insert into agr_trn_tsuprdocumentchecktls(" +
                            " documentcheckdtl_gid," +
                            " application_gid," +
                            " credit_gid, " +
                            " companydocument_gid, " +
                            " documentuploaded_gid, " +
                            " documenttype_gid," +
                        " documenttype_code," +
                        " documenttype_name," +
                        " covenant_type, " +
                        " tagged_by, " +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetdefDocchecklistGID + "'," +
                        "'" + lsapplication_gid + "'," +
                        "'" + msGetGid + "'," +
                        "'" + dt["companydocument_gid"].ToString() + "'," +
                        "'" + dt["institution2documentupload_gid"].ToString() + "'," +
                        "'" + lsdocumenttype_gid + "'," +
                        "'" + lsdocumenttype_name + "'," +
                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                        "'" + lscovenant_type + "'," +
                        "'N'," +
                        "current_timestamp," +
                        "'" + employee_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (lscovenant_type == "Y")
                    {
                        string msGetDocchecklistGID = objcmnfunctions.GetMasterGID("CDCL");
                        msSQL = " insert into agr_trn_tsuprcovanantdocumentcheckdtls(" +
                       " covenantdocumentcheckdtl_gid," +
                       " application_gid," +
                       " credit_gid," +
                       " companydocument_gid," +
                       " documentuploaded_gid, " +
                       " documenttype_gid," +
                       " documenttype_code," +
                       " documenttype_name," +
                       " covenant_type, " +
                       " tagged_by, " +
                       " created_date," +
                       " created_by)" +
                       " VALUES(" +
                       "'" + msGetDocchecklistGID + "'," +
                       "'" + lsapplication_gid + "'," +
                       "'" + msGetGid + "'," +
                       "'" + dt["companydocument_gid"].ToString() + "'," +
                       "'" + dt["institution2documentupload_gid"].ToString() + "'," +
                       "'" + lsdocumenttype_gid + "'," +
                       "'" + lsdocumenttype_name + "'," +
                       "'" + lscompanydocument_name.Replace("'", "") + "'," +
                       "'" + lscovenant_type + "'," +
                       "'N'," +
                       "current_timestamp," +
                       "'" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                DaAgrMstSuprScannedDocument objvalues = new DaAgrMstSuprScannedDocument();
                objvalues.DaGroupDocChecklistinfo(lsapplication_gid, msGetGid, employee_gid);

                msSQL = "update agr_mst_tsuprinstitution2documentupload set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprinstitution2form60documentupload set institution_gid='" + msGetGid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprkycgstsbpan set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "select mobile_no from agr_mst_tsuprinstitution2mobileno where institution_gid='" + msGetGid + "' and primary_status='yes'";
                lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tsuprinstitution2email where institution_gid='" + msGetGid + "' and primary_status='yes'";
                lsemail_address = objdbconn.GetExecuteScalar(msSQL);
                if (values.stakeholder_type == "Borrower" || values.stakeholder_type == "Applicant")
                {
                    msSQL = "update agr_mst_tsuprapplication set applicant_type ='Institution' where application_gid='" + lsapplication_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsuprinstitution set mobile_no='" + lsmobileno + "'," +
                     " email_address='" + lsemail_address + "' where institution_gid='" + msGetGid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }


                values.message = "Institution Information Submitted Successfully";
                values.status = true;
                return true;

            }
            else
            {
                values.message = "Error Occured";
                values.status = false;
                return false;
            }
        }

        public void DaGetIntitutionTempClear(string employee_gid, result values)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2mobileno where institution_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprinstitution2email where institution_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprinstitution2address where institution_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprinstitution2branch where institution_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprinstitution2licensedtl where institution_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprinstitution2documentupload where institution_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprinstitution2form60documentupload where institution_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprinstitution2ratingdetail where institution_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public bool DaMobileNumberAdd(string employee_gid, MdlContactMobileNo values)
        {
            msSQL = "select primary_status from agr_mst_tsuprcontact2mobileno where primary_status='Yes' and contact_gid='" + employee_gid + "'";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Mobile Number Added";
                objdbconn.CloseConn();
                return false;
            }

            msSQL = "select mobile_no from agr_mst_tsuprcontact2mobileno where mobile_no='" + values.mobile_no + "' and contact_gid='" + employee_gid + "'";
            string lsmobile_no = objdbconn.GetExecuteScalar(msSQL);
            if (lsmobile_no == (values.mobile_no))
            {

                values.status = false;
                values.message = "Already This Mobile Number Added";
                objdbconn.CloseConn();
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("C2MN");

            msSQL = " insert into agr_mst_tsuprcontact2mobileno(" +
                    " contact2mobileno_gid," +
                    " contact_gid," +
                    " mobile_no," +
                    " primary_status," +
                    " whatsapp_no," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.mobile_no + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.whatsapp_no + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            objdbconn.CloseConn();

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Mobile Number Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetMobileNoList(string employee_gid, MdlContactMobileNo values)
        {
            msSQL = "select mobile_no,contact2mobileno_gid,primary_status,whatsapp_no from agr_mst_tsuprcontact2mobileno where " +
              " contact_gid='" + employee_gid + "'";
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
        }

        public void DaMobileNoDelete(string contact2mobileno_gid, MdlContactMobileNo values)
        {
            msSQL = "delete from agr_mst_tsuprcontact2mobileno where contact2mobileno_gid='" + contact2mobileno_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Mobile Number Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaEmailAddressAdd(string employee_gid, MdlContactEmail values)
        {
            msSQL = "select primary_status from agr_mst_tsuprcontact2email where primary_status='Yes' and contact_gid='" + employee_gid + "'";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Email Address Added";
                objdbconn.CloseConn();
                return false;
            }

            msSQL = "select email_address from agr_mst_tsuprcontact2email where email_address='" + values.email_address + "' and contact_gid='" + employee_gid + "'";
            string lsemail_address = objdbconn.GetExecuteScalar(msSQL);
            if (lsemail_address == (values.email_address))
            {
                values.status = false;
                values.message = "Already This Email Address Added";
                objdbconn.CloseConn();
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("C2EA");
            msSQL = " insert into agr_mst_tsuprcontact2email(" +
                    " contact2email_gid," +
                    " contact_gid," +
                    " email_address," +
                    " primary_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.email_address + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            objdbconn.CloseConn();
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Email Address Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetEmailList(string employee_gid, MdlContactEmail values)
        {
            msSQL = "select email_address,contact2email_gid,primary_status from agr_mst_tsuprcontact2email where " +
              " contact_gid='" + employee_gid + "'";
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
        }

        public void DaEmailAddressDelete(string contact2email_gid, MdlContactEmail values)
        {
            msSQL = "delete from agr_mst_tsuprcontact2email where contact2email_gid='" + contact2email_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Email Address Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaAddressAdd(string employee_gid, MdlContactAddress values)
        {
            msSQL = "select primary_status from agr_mst_tsuprcontact2address where primary_status='Yes' and contact_gid='" + employee_gid + "'";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Address Added";
                return false;
            }

            msSQL = "select contact2address_gid from agr_mst_tsuprcontact2address where addresstype_name='" + values.addresstype_name + "' and " +
                " contact_gid='" + employee_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Address Type Added";
                return false;
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("C2AD");
            msSQL = " insert into agr_mst_tsuprcontact2address(" +
                    " contact2address_gid," +
                    " contact_gid," +
                    " addresstype_gid," +
                    " addresstype_name," +
                    " primary_status," +
                    " addressline1," +
                    " addressline2," +
                    " landmark," +
                    " postal_code," +
                    " city," +
                    " taluka," +
                    " district," +
                    " state," +
                    " country," +
                    " latitude," +
                    " longitude," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.addresstype_gid + "'," +
                    "'" + values.addresstype_name + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.addressline1 + "'," +
                    "'" + values.addressline2 + "'," +
                    "'" + values.landmark + "'," +
                    "'" + values.postal_code + "'," +
                    "'" + values.city + "'," +
                    "'" + values.taluka + "'," +
                    "'" + values.district + "'," +
                    "'" + values.state + "'," +
                    "'" + values.country + "'," +
                    "'" + values.latitude + "'," +
                    "'" + values.longitude + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Address Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }

        public void DaGetAddressList(string employee_gid, MdlContactAddress values)
        {
            msSQL = " select contact2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude," +
                    " postal_code from agr_mst_tsuprcontact2address where contact_gid='" + employee_gid + "'";
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
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString())
                    });
                }
                values.contactaddress_list = getcontactaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaAddressDelete(string contact2address_gid, MdlContactAddress values)
        {
            msSQL = "delete from agr_mst_tsuprcontact2address where contact2address_gid='" + contact2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Address Detail Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaIndividualProofDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsidproof_type = httpRequest.Form["idproof_type"].ToString();
            string lsidproof_no = httpRequest.Form["idproof_no"].ToString();
            string lsidproof_dob = httpRequest.Form["idproof_dob"].ToString();
            string lsfile_no = httpRequest.Form["file_no"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();

            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IndividualProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IndividualProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/IndividualProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IndividualProofDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("C2IP");
                        msSQL = " insert into agr_mst_tsuprcontact2idproof(" +
                                " contact2idproof_gid," +
                                " contact_gid," +
                                " idproof_name," +
                                " idproof_no," +
                                " idproof_dob," +
                                " file_no," +
                                " document_name," +
                                " document_path," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + lsidproof_type + "'," +
                                "'" + lsidproof_no + "'," +
                                "'" + lsidproof_dob + "'," +
                                "'" + lsfile_no + "'," +
                                "'" + httpPostedFile.FileName + "'," +
                                "'" + lspath + msdocument_gid + FileExtension + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaGetIndividualProofList(string employee_gid, MdlContactIdProof values)
        {
            msSQL = "select contact2idproof_gid,idproof_name,idproof_no,idproof_dob,file_no,document_name, document_path from agr_mst_tsuprcontact2idproof where " +
              " contact_gid='" + employee_gid + "'";
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
                        idproof_dob = (dr_datarow["idproof_dob"].ToString()),
                        file_no = (dr_datarow["file_no"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString()))
                    });

                    values.contactidproof_list = getcontactidproof_list;
                }
                dt_datatable.Dispose();
            }

        }

        public void DaIndividualProofDelete(string contact2idproof_gid, MdlContactIdProof values)
        {
            msSQL = "delete from agr_mst_tsuprcontact2idproof where contact2idproof_gid='" + contact2idproof_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "ID Proof Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }


        public bool DaIndividualDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            String path = lspath;
            string lsindividualdocument_gid = httpRequest.Form["individualdocument_gid"].ToString();
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);
                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msSQL = "select covenant_type from ocs_mst_tindividualdocument where individualdocument_gid='" + lsindividualdocument_gid + "'";
                        string lscovenant_type = objdbconn.GetExecuteScalar(msSQL);

                        msGetGid = objcmnfunctions.GetMasterGID("C2DO");
                        msGetDocumentGid = objcmnfunctions.GetMasterGID("BSDA");

                        msSQL = " insert into agr_mst_tsuprcontact2document( " +
                                    " contact2document_gid ," +
                                    " contact_gid ," +
                                    " document_gid ," +
                                    " document_title ," +
                                    " document_name ," +
                                    " document_path," +
                                    " individualdocument_gid, " +
                                    " covenant_type," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + msGetDocumentGid + "'," +
                                    "'" + lsdocument_title + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lsindividualdocument_gid + "'," +
                                    "'" + lscovenant_type + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaGetIndividualDocList(string employee_gid, MdlContactDocument values)
        {
            msSQL = " select contact2document_gid,document_name,document_path,document_title from agr_mst_tsuprcontact2document " +
                                 " where contact_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<uploadindividualdoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new uploadindividualdoc_list
                    {
                        document_name = dt["document_name"].ToString(),
                        //document_path = (dt["document_path"].ToString()),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        contact2document_gid = dt["contact2document_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                    values.uploadindividualdoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaIndividualDocDelete(string contact2document_gid, MdlContactDocument values)
        {
            msSQL = "delete from agr_mst_tsuprcontact2document where contact2document_gid='" + contact2document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = " select groupdocumentchecklist_gid from agr_trn_tsuprdocumentchecktls where documentuploaded_gid='" + contact2document_gid + "'";
                string lsgroupdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsgroupdocumentchecklist_gid != "")
                {
                    msSQL = " select count(*) as documentcount from agr_trn_tsuprdocumentchecktls " +
                            " where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tsuprgroupdocumentchecklist where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = " select groupcovdocumentchecklist_gid from agr_trn_tsuprcovanantdocumentcheckdtls where documentuploaded_gid='" + contact2document_gid + "'";
                string lschecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lschecklist_gid != "")
                {
                    msSQL = " select count(*) as documentcount from agr_trn_tsuprcovanantdocumentcheckdtls " +
                      " where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tsuprgroupcovenantdocumentchecklist where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = "delete from agr_trn_tsuprcovanantdocumentcheckdtls where documentuploaded_gid='" + contact2document_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from agr_trn_tsuprdocumentchecktls where documentuploaded_gid='" + contact2document_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {

                values.message = "Document Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaIndividualSave(string employee_gid, MdlMstContact values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CTCT");

            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select stakeholder_type from agr_mst_tsuprcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            string lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholdertype_name)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return;
            }

            msSQL = "select stakeholder_type from agr_mst_tsuprinstitution where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholdertype_name)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return;
            }
            msSQL = " insert into agr_mst_tsuprcontact(" +
                   " contact_gid," +
                   " application_gid," +
                   " application_no," +
                   " pan_status," +
                   " pan_no," +
                   " aadhar_no," +
                   " first_name," +
                   " middle_name," +
                   " last_name," +
                   " individual_dob," +
                   " age," +
                   " gender_gid," +
                   " gender_name," +
                   " designation_gid," +
                   " designation_name," +
                   " educationalqualification_gid," +
                   " educationalqualification_name," +
                   " main_occupation," +
                   " annual_income," +
                   " monthly_income," +
                   " pep_status," +
                   " pepverified_date," +
                   " stakeholdertype_gid," +
                   " stakeholder_type," +
                   " maritalstatus_gid," +
                   " maritalstatus_name," +
                   " father_firstname," +
                   " father_middlename," +
                   " father_lastname," +
                   " father_dob," +
                   " father_age," +
                   " mother_firstname," +
                   " mother_middlename," +
                   " mother_lastname," +
                   " mother_dob," +
                   " mother_age," +
                   " spouse_firstname," +
                   " spouse_middlename," +
                   " spouse_lastname," +
                   " spouse_dob," +
                   " spouse_age," +
                   " ownershiptype_gid," +
                   " ownershiptype_name," +
                   " propertyholder_gid," +
                   " propertyholder_name," +
                   " residencetype_gid," +
                   " residencetype_name," +
                   " incometype_gid," +
                   " incometype_name," +
                   " currentresidence_years," +
                   " branch_distance," +
                   " group_gid," +
                   " group_name," +
                   " profile," +
                   " urn_status," +
                   " urn," +
                   " fathernominee_status," +
                   " mothernominee_status," +
                   " spousenominee_status," +
                   " othernominee_status," +
                   " relationshiptype," +
                   " nomineefirst_name," +
                   " nominee_middlename," +
                   " nominee_lastname," +
                   " nominee_dob," +
                   " nominee_age," +
                   " totallandinacres," +
                   " cultivatedland," +
                   " previouscrop," +
                   " prposedcrop," +
                   " institution_gid," +
                   " institution_name," +
                   " contact_status," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + lsapplication_gid + "'," +
                   "'" + employee_gid + "'," +
                   "'" + values.pan_status + "'," +
                   "'" + values.pan_no + "'," +
                   "'" + values.aadhar_no + "',";
            if (values.first_name == "" || values.first_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.first_name.Replace("'", "") + "',";
            }
            if (values.middle_name == "" || values.middle_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.middle_name.Replace("'", "") + "',";
            }
            if (values.last_name == "" || values.last_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.last_name.Replace("'", "") + "',";
            }
            msSQL += "'" + values.individual_dob + "'," +
                     "'" + values.age + "'," +
                         "'" + values.gender_gid + "'," +
                         "'" + values.gender_name + "'," +
                         "'" + values.designation_gid + "'," +
                         "'" + values.designation_name + "'," +
                         "'" + values.educationalqualification_gid + "'," +
                         "'" + values.educationalqualification_name + "'," +
                         "'" + values.main_occupation + "'," +
                         "'" + values.annual_income + "'," +
                         "'" + values.monthly_income + "'," +
                         "'" + values.pep_status + "',";

            if ((values.pepverified_date == null) || (values.pepverified_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.pepverified_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }



            msSQL += "'" + values.stakeholdertype_gid + "'," +
                     "'" + values.stakeholdertype_name + "'," +
                     "'" + values.maritalstatus_gid + "'," +
                     "'" + values.maritalstatus_name + "',";
            if (values.father_firstname == "" || values.father_firstname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.father_firstname.Replace("'", "") + "',";
            }
            if (values.father_middlename == "" || values.father_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.father_middlename.Replace("'", "") + "',";
            }
            if (values.father_lastname == "" || values.father_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.father_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.father_dob + "'," +
                     "'" + values.father_age + "',";
            if (values.mother_firstname == "" || values.mother_firstname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.mother_firstname.Replace("'", "") + "',";
            }
            if (values.mother_middlename == "" || values.mother_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.mother_middlename.Replace("'", "") + "',";
            }
            if (values.mother_lastname == "" || values.mother_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.mother_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.mother_dob + "'," +
                     "'" + values.mother_age + "',";
            if (values.spouse_firstname == "" || values.spouse_firstname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.spouse_firstname.Replace("'", "") + "',";
            }
            if (values.spouse_middlename == "" || values.spouse_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.spouse_middlename.Replace("'", "") + "',";
            }
            if (values.spouse_lastname == "" || values.spouse_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.spouse_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.spouse_dob + "'," +
                     "'" + values.spouse_age + "'," +
                         "'" + values.ownershiptype_gid + "'," +
                         "'" + values.ownershiptype_name + "'," +
                         "'" + values.propertyholder_gid + "'," +
                         "'" + values.propertyholder_name + "'," +
                         "'" + values.residencetype_gid + "'," +
                         "'" + values.residencetype_name + "'," +
                         "'" + values.incometype_gid + "'," +
                         "'" + values.incometype_name + "'," +
                         "'" + values.currentresidence_years + "'," +
                         "'" + values.branch_distance + "'," +
                          "'" + values.group_gid + "'," +
                     "'" + values.group_name + "'," +
                     "'" + values.profile + "'," +
                     "'" + values.urn_status + "'," +
                     "'" + values.urn + "'," +
                     "'" + values.fathernominee_status + "'," +
                     "'" + values.mothernominee_status + "'," +
                     "'" + values.spousenominee_status + "'," +
                     "'" + values.othernominee_status + "'," +
                     "'" + values.relationshiptype + "',";
            if (values.nomineefirst_name == "" || values.nomineefirst_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.nomineefirst_name.Replace("'", "") + "',";
            }
            if (values.nominee_middlename == "" || values.nominee_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.nominee_middlename.Replace("'", "") + "',";
            }
            if (values.nominee_lastname == "" || values.nominee_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.nominee_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.nominee_dob + "'," +
                     "'" + values.nominee_age + "'," +
                     "'" + values.totallandinacres + "'," +
                     "'" + values.cultivatedland + "'," +
                     "'" + values.previouscrop + "'," +
                     "'" + values.prposedcrop + "'," +
                     "'" + values.institution_gid + "'," +
                     "'" + values.institution_name + "'," +
                         "'Incomplete'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                if (values.pan_status == "Customer Submitting Form 60")
                {
                    // PAN Update
                    foreach (string reason in values.panabsencereason_selectedlist)
                    {
                        msGetGidpan = objcmnfunctions.GetMasterGID("C2PR");
                        msSQL = " INSERT INTO agr_mst_tsuprcontact2panabsencereason(" +
                               " contact2panabsencereason_gid," +
                               " contact_gid," +
                               " panabsencereason," +
                               " created_date," +
                               " created_by)" +
                               " VALUES(" +
                               "'" + msGetGidpan + "'," +
                               "'" + msGetGid + "'," +
                               "'" + reason + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                // Updates

                msSQL = "update agr_mst_tsuprcontact2mobileno set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprcontact2email set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprcontact2address set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprcontact2idproof set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select contact2document_gid, individualdocument_gid from agr_mst_tsuprcontact2document where contact_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lscovenant_type = "", lsdocumenttype_gid = "", lsdocumenttype_name = "", lscompanydocument_name = "";

                    string msGetdefDocchecklistGID = objcmnfunctions.GetMasterGID("DOCG");
                    msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                         " from ocs_mst_tindividualdocument where individualdocument_gid='" + dt["individualdocument_gid"].ToString() + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                        lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                        lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                        lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = " insert into agr_trn_tsuprdocumentchecktls(" +
                            " documentcheckdtl_gid," +
                            " application_gid," +
                            " credit_gid, " +
                            " individualdocument_gid, " +
                             " documentuploaded_gid, " +
                            " documenttype_gid," +
                        " documenttype_code," +
                        " documenttype_name," +
                        " covenant_type, " +
                        " tagged_by, " +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetdefDocchecklistGID + "'," +
                        "'" + lsapplication_gid + "'," +
                        "'" + msGetGid + "'," +
                        "'" + dt["individualdocument_gid"].ToString() + "'," +
                        "'" + dt["contact2document_gid"].ToString() + "'," +
                        "'" + lsdocumenttype_gid + "'," +
                        "'" + lsdocumenttype_name + "'," +
                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                        "'" + lscovenant_type + "'," +
                        "'N'," +
                        "current_timestamp," +
                        "'" + employee_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (lscovenant_type == "Y")
                    {
                        string msGetDocchecklistGID = objcmnfunctions.GetMasterGID("CDCL");
                        msSQL = " insert into agr_trn_tsuprcovanantdocumentcheckdtls(" +
                       " covenantdocumentcheckdtl_gid," +
                       " application_gid," +
                       " credit_gid," +
                       " individualdocument_gid," +
                        " documentuploaded_gid, " +
                       " documenttype_gid," +
                       " documenttype_code," +
                       " documenttype_name," +
                       " covenant_type, " +
                       " tagged_by, " +
                       " created_date," +
                       " created_by)" +
                       " VALUES(" +
                       "'" + msGetDocchecklistGID + "'," +
                       "'" + lsapplication_gid + "'," +
                       "'" + msGetGid + "'," +
                       "'" + dt["individualdocument_gid"].ToString() + "'," +
                       "'" + dt["contact2document_gid"].ToString() + "'," +
                       "'" + lsdocumenttype_gid + "'," +
                       "'" + lsdocumenttype_name + "'," +
                       "'" + lscompanydocument_name.Replace("'", "") + "'," +
                       "'" + lscovenant_type + "'," +
                       "'N'," +
                       "current_timestamp," +
                       "'" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                DaAgrMstSuprScannedDocument objvalues = new DaAgrMstSuprScannedDocument();
                objvalues.DaGroupDocChecklistinfo(lsapplication_gid, msGetGid, employee_gid);

                msSQL = "update agr_mst_tsuprcontact2document set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprkycpanauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprkycpanaadhaarlink set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprkycdlauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprkycepicauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprkycpassportauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprcontact2panform60 set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprcontact2panabsencereason set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Individual Details Saved Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaIndividualSubmit(string employee_gid, MdlMstContact values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("CTCT");


            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select stakeholder_type from agr_mst_tsuprcontact where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            string lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholdertype_name)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return;
            }

            msSQL = "select stakeholder_type from agr_mst_tsuprinstitution where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Borrower','Applicant')";
            lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

            if (lsstakeholder_type == values.stakeholdertype_name)
            {

                values.status = false;
                values.message = "Applicant/Borrower Information Already Added";
                return;
            }

            msSQL = "select contact_gid from agr_mst_tsuprcontact2mobileno where contact_gid='" + employee_gid + "' and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Mobile Number ";
                return;
            }
            objODBCDatareader.Close();

            msSQL = "select contact_gid from agr_mst_tsuprcontact2email where contact_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Email Address";
                return;
            }
            objODBCDatareader.Close();
            msSQL = "select contact_gid from agr_mst_tsuprcontact2address where contact_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add primary Address";
                return;
            }
            objODBCDatareader.Close();
            msSQL = " insert into agr_mst_tsuprcontact(" +
                   " contact_gid," +
                   " application_gid," +
                   " application_no," +
                   " pan_status, " +
                   " pan_no," +
                   " aadhar_no," +
                   " first_name," +
                   " middle_name," +
                   " last_name," +
                   " individual_dob," +
                   " age," +
                   " gender_gid," +
                   " gender_name," +
                   " designation_gid," +
                   " designation_name," +
                   " educationalqualification_gid," +
                   " educationalqualification_name," +
                   " main_occupation," +
                   " annual_income," +
                   " monthly_income," +
                   " pep_status," +
                   " pepverified_date," +
                   " stakeholdertype_gid," +
                   " stakeholder_type," +
                   " maritalstatus_gid," +
                   " maritalstatus_name," +
                   " father_firstname," +
                   " father_middlename," +
                   " father_lastname," +
                   " father_dob," +
                   " father_age," +
                   " mother_firstname," +
                   " mother_middlename," +
                   " mother_lastname," +
                   " mother_dob," +
                   " mother_age," +
                   " spouse_firstname," +
                   " spouse_middlename," +
                   " spouse_lastname," +
                   " spouse_dob," +
                   " spouse_age," +
                   " ownershiptype_gid," +
                   " ownershiptype_name," +
                   " propertyholder_gid," +
                   " propertyholder_name," +
                   " residencetype_gid," +
                   " residencetype_name," +
                   " incometype_gid," +
                   " incometype_name," +
                   " currentresidence_years," +
                   " branch_distance," +
                   " group_gid," +
                   " group_name," +
                   " profile," +
                   " urn_status," +
                   " urn," +
                   " fathernominee_status," +
                   " mothernominee_status," +
                   " spousenominee_status," +
                   " othernominee_status," +
                   " relationshiptype," +
                   " nomineefirst_name," +
                   " nominee_middlename," +
                   " nominee_lastname," +
                   " nominee_dob," +
                   " nominee_age," +
                   " totallandinacres," +
                   " cultivatedland," +
                   " previouscrop," +
                   " prposedcrop," +
                   " institution_gid," +
                   " institution_name," +
                   " contact_status," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + lsapplication_gid + "'," +
                   "'" + employee_gid + "'," +
                   "'" + values.pan_status + "'," +
                   "'" + values.pan_no + "'," +
                   "'" + values.aadhar_no + "',";
            if (values.first_name == "" || values.first_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.first_name.Replace("'", "") + "',";
            }
            if (values.middle_name == "" || values.middle_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.middle_name.Replace("'", "") + "',";
            }
            if (values.last_name == "" || values.last_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.last_name.Replace("'", "") + "',";
            }
            msSQL += "'" + values.individual_dob + "'," +
                     "'" + values.age + "'," +
                     "'" + values.gender_gid + "'," +
                     "'" + values.gender_name + "'," +
                     "'" + values.designation_gid + "'," +
                     "'" + values.designation_name + "'," +
                     "'" + values.educationalqualification_gid + "'," +
                     "'" + values.educationalqualification_name + "'," +
                     "'" + values.main_occupation + "'," +
                     "'" + values.annual_income + "'," +
                     "'" + values.monthly_income + "'," +
                     "'" + values.pep_status + "',";

            if ((values.pepverified_date == null) || (values.pepverified_date == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.pepverified_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }



            msSQL += "'" + values.stakeholdertype_gid + "'," +
                "'" + values.stakeholdertype_name + "'," +
                     "'" + values.maritalstatus_gid + "'," +
                     "'" + values.maritalstatus_name + "',";
            if (values.father_firstname == "" || values.father_firstname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.father_firstname.Replace("'", "") + "',";
            }
            if (values.father_middlename == "" || values.father_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.father_middlename.Replace("'", "") + "',";
            }
            if (values.father_lastname == "" || values.father_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.father_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.father_dob + "'," +
                     "'" + values.father_age + "',";
            if (values.mother_firstname == "" || values.mother_firstname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.mother_firstname.Replace("'", "") + "',";
            }
            if (values.mother_middlename == "" || values.mother_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.mother_middlename.Replace("'", "") + "',";
            }
            if (values.mother_lastname == "" || values.mother_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.mother_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.mother_dob + "'," +
                     "'" + values.mother_age + "',";
            if (values.spouse_firstname == "" || values.spouse_firstname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.spouse_firstname.Replace("'", "") + "',";
            }
            if (values.spouse_middlename == "" || values.spouse_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.spouse_middlename.Replace("'", "") + "',";
            }
            if (values.spouse_lastname == "" || values.spouse_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.spouse_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.spouse_dob + "'," +
                     "'" + values.spouse_age + "'," +
                     "'" + values.ownershiptype_gid + "'," +
                     "'" + values.ownershiptype_name + "'," +
                     "'" + values.propertyholder_gid + "'," +
                     "'" + values.propertyholder_name + "'," +
                     "'" + values.residencetype_gid + "'," +
                     "'" + values.residencetype_name + "'," +
                     "'" + values.incometype_gid + "'," +
                     "'" + values.incometype_name + "'," +
                     "'" + values.currentresidence_years + "'," +
                     "'" + values.branch_distance + "'," +
                     "'" + values.group_gid + "'," +
                     "'" + values.group_name + "'," +
                     "'" + values.profile + "'," +
                     "'" + values.urn_status + "'," +
                     "'" + values.urn + "'," +
                     "'" + values.fathernominee_status + "'," +
                     "'" + values.mothernominee_status + "'," +
                     "'" + values.spousenominee_status + "'," +
                     "'" + values.othernominee_status + "'," +
                     "'" + values.relationshiptype + "',";
            if (values.nomineefirst_name == "" || values.nomineefirst_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.nomineefirst_name.Replace("'", "") + "',";
            }
            if (values.nominee_middlename == "" || values.nominee_middlename == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.nominee_middlename.Replace("'", "") + "',";
            }
            if (values.nominee_lastname == "" || values.nominee_lastname == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.nominee_lastname.Replace("'", "") + "',";
            }
            msSQL += "'" + values.nominee_dob + "'," +
                     "'" + values.nominee_age + "'," +
                     "'" + values.totallandinacres + "'," +
                     "'" + values.cultivatedland + "'," +
                     "'" + values.previouscrop + "'," +
                     "'" + values.prposedcrop + "'," +
                     "'" + values.institution_gid + "'," +
                     "'" + values.institution_name + "'," +
                     "'Completed'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                // PAN Update
                if (values.pan_status == "Customer Submitting Form 60")
                {
                    foreach (string reason in values.panabsencereason_selectedlist)
                    {
                        msGetGidpan = objcmnfunctions.GetMasterGID("C2PR");
                        msSQL = " INSERT INTO agr_mst_tsuprcontact2panabsencereason(" +
                               " contact2panabsencereason_gid," +
                               " contact_gid," +
                               " panabsencereason," +
                               " created_date," +
                               " created_by)" +
                               " VALUES(" +
                               "'" + msGetGidpan + "'," +
                               "'" + msGetGid + "'," +
                               "'" + reason + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                //Updates

                msSQL = "update agr_mst_tsuprcontact2mobileno set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprcontact2email set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprcontact2address set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprcontact2idproof set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprcontact2panform60 set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprcontact2panabsencereason set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select individualdocument_gid, contact2document_gid from agr_mst_tsuprcontact2document where contact_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lscovenant_type = "", lsdocumenttype_gid = "", lsdocumenttype_name = "", lscompanydocument_name = "";

                    string msGetdefDocchecklistGID = objcmnfunctions.GetMasterGID("DOCG");
                    msSQL = " select individualdocument_gid,documenttypes_gid,documenttype_name,individualdocument_name,covenant_type " +
                         " from ocs_mst_tindividualdocument where individualdocument_gid='" + dt["individualdocument_gid"].ToString() + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                        lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                        lscompanydocument_name = objODBCDatareader["individualdocument_name"].ToString();
                        lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = " insert into agr_trn_tsuprdocumentchecktls(" +
                            " documentcheckdtl_gid," +
                            " application_gid," +
                            " credit_gid, " +
                            " individualdocument_gid, " +
                             " documentuploaded_gid, " +
                            " documenttype_gid," +
                        " documenttype_code," +
                        " documenttype_name," +
                        " covenant_type, " +
                        " tagged_by, " +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetdefDocchecklistGID + "'," +
                        "'" + lsapplication_gid + "'," +
                        "'" + msGetGid + "'," +
                        "'" + dt["individualdocument_gid"].ToString() + "'," +
                        "'" + dt["contact2document_gid"].ToString() + "'," +
                        "'" + lsdocumenttype_gid + "'," +
                        "'" + lsdocumenttype_name + "'," +
                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                        "'" + lscovenant_type + "'," +
                        "'N'," +
                        "current_timestamp," +
                        "'" + employee_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (lscovenant_type == "Y")
                    {
                        string msGetDocchecklistGID = objcmnfunctions.GetMasterGID("CDCL");
                        msSQL = " insert into agr_trn_tsuprcovanantdocumentcheckdtls(" +
                       " covenantdocumentcheckdtl_gid," +
                       " application_gid," +
                       " credit_gid," +
                       " individualdocument_gid," +
                        " documentuploaded_gid, " +
                       " documenttype_gid," +
                       " documenttype_code," +
                       " documenttype_name," +
                       " covenant_type, " +
                       " tagged_by, " +
                       " created_date," +
                       " created_by)" +
                       " VALUES(" +
                       "'" + msGetDocchecklistGID + "'," +
                       "'" + lsapplication_gid + "'," +
                       "'" + msGetGid + "'," +
                       "'" + dt["individualdocument_gid"].ToString() + "'," +
                       "'" + dt["contact2document_gid"].ToString() + "'," +
                       "'" + lsdocumenttype_gid + "'," +
                       "'" + lsdocumenttype_name + "'," +
                       "'" + lscompanydocument_name.Replace("'", "") + "'," +
                       "'" + lscovenant_type + "'," +
                       "'N'," +
                       "current_timestamp," +
                       "'" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                DaAgrMstSuprScannedDocument objvalues = new DaAgrMstSuprScannedDocument();
                objvalues.DaGroupDocChecklistinfo(lsapplication_gid, msGetGid, employee_gid);

                msSQL = "update agr_mst_tsuprcontact2document set contact_gid ='" + msGetGid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprkycpanauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprkycpanaadhaarlink set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprkycdlauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprkycepicauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprkycpassportauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "select mobile_no from agr_mst_tsuprcontact2mobileno where contact_gid='" + msGetGid + "' and primary_status='yes'";
                lsmobileno = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select email_address from agr_mst_tsuprcontact2email where contact_gid='" + msGetGid + "' and primary_status='yes'";
                lsemail_address = objdbconn.GetExecuteScalar(msSQL);

                if (values.stakeholdertype_name == "Borrower" || values.stakeholdertype_name == "Applicant")
                {
                    msSQL = "update agr_mst_tsuprapplication set applicant_type ='Individual' where application_gid='" + lsapplication_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = "update agr_mst_tsuprcontact set mobile_no='" + lsmobileno + "'," +
                        " email_address='" + lsemail_address + "' where contact_gid='" + msGetGid + "' ";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                values.status = true;
                values.message = "Individual Details Submitted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaGetPostalCodeDetails(string postal_code, MdlContactAddress values)
        {
            try
            {
                msSQL = "select city,taluka,district,state from ocs_mst_tpostalcode where " +
                        " postalcode_value='" + postal_code + "'";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        values.city = (dr_datarow["city"].ToString());
                        values.taluka = (dr_datarow["taluka"].ToString());
                        values.district = (dr_datarow["district"].ToString());
                        values.state = (dr_datarow["state"].ToString());
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

        public void GetIndividualTempClear(string employee_gid, result values)
        {
            msSQL = "delete from agr_mst_tsuprcontact2mobileno where contact_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprcontact2email where contact_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprcontact2address where contact_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprcontact2idproof where contact_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprcontact2document where contact_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprindividual2cicdocumentupload where contact_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprinstitution2cicdocumentupload where institution_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprcontact2panform60 where contact_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprcontact2panabsencereason where contact_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }
        public void DaGetCICIndividualSummary(string employee_gid, MdlCICIndividual values)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select contact_gid,first_name,middle_name,last_name," +
                    " case when bureauname_name is null then '-'" +
                    " else bureauname_name end as bureauname_name," +
                    " case when bureau_score is null then '-'" +
                    " else bureau_score end as bureau_score," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from agr_mst_tsuprcontact a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where a.application_gid='" + lsapplication_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcicindividualList = new List<cicindividual_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcicindividualList.Add(new cicindividual_list
                    {
                        contact_gid = dt["contact_gid"].ToString(),
                        first_name = dt["first_name"].ToString(),
                        middle_name = dt["middle_name"].ToString(),
                        last_name = dt["last_name"].ToString(),
                        bureauname_name = dt["bureauname_name"].ToString(),
                        bureau_score = dt["bureau_score"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });

                }
            }
            values.cicindividual_list = getcicindividualList;
            dt_datatable.Dispose();
        }

        public void DaGetCICInstitutionSummary(string employee_gid, MdlCICInstitution values)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select institution_gid,company_name," +
                    " case when bureauname_name is null then '-'" +
                    " else bureauname_name end as bureauname_name," +
                    " case when bureau_score is null then '-'" +
                    " else bureau_score end as bureau_score," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from agr_mst_tsuprinstitution a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where a.application_gid='" + lsapplication_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcicinstitutionList = new List<cicinstitution_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcicinstitutionList.Add(new cicinstitution_list
                    {
                        institution_gid = dt["institution_gid"].ToString(),
                        company_name = dt["company_name"].ToString(),
                        bureauname_name = dt["bureauname_name"].ToString(),
                        bureau_score = dt["bureau_score"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                    });

                }
            }
            values.cicinstitution_list = getcicinstitutionList;
            dt_datatable.Dispose();
        }

        public bool DaCICIndividualDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CICUploadIndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }

            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);
                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CICUploadIndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/CICUploadIndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CICUploadIndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("IDCU");
                        msSQL = " insert into agr_mst_tsuprindividual2cicdocumentupload( " +
                                    " individual2cicdocumentupload_gid, " +
                                    " contact_gid," +
                                    " contact2bureau_gid," +
                                    " cicdocument_name ," +
                                    " cicdocument_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public bool DaCICInstitutionDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CICUploadInstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }

            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CICUploadInstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        //FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                        //ms.WriteTo(file);
                        //file.Close();
                        //ms.Close();

                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/CICUploadInstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();

                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/CICUploadInstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("INCU");
                        msSQL = " insert into agr_mst_tsuprinstitution2cicdocumentupload( " +
                                    " institution2cicdocumentupload_gid, " +
                                    " institution_gid," +
                                    " institution2bureau_gid," +
                                    " cicdocument_name ," +
                                    " cicdocument_path," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public bool DaPostCICUploadIndividual(string employee_gid, MdlCICIndividual values)
        {


            //msSQL = "select contact2bureau_gid from  agr_mst_tsuprindividual2cicdocumentupload where  contact2bureau_gid='" + employee_gid + "'  ";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == false)
            //{
            //    values.status = false;
            //    values.message = "Kindly Add The Document";
            //    return false;
            //}

            // Document Attachments
            msSQL = "select document_name from agr_tmp_tsuprcicdocument where created_by='" + employee_gid + "'";
            lsdocument_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select document_path from agr_tmp_tsuprcicdocument where created_by='" + employee_gid + "'";
            lsdocument_path = objdbconn.GetExecuteScalar(msSQL);
            try
            {
                var bureauscore_date = Convert.ToDateTime(values.bureauscore_date).ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Kindly enter the valid date format";
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("C2BR");
            msSQL = " insert into agr_mst_tsuprcontact2bureau(" +
                   " contact2bureau_gid ," +
                   " contact_gid," +
                   " bureauname_gid," +
                   " bureauname_name," +
                   " bureau_score," +
                   " bureauscore_date," +
                   " bureau_response," +
                   " observations," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.contact_gid + "'," +
                   "'" + values.bureauname_gid + "'," +
                   "'" + values.bureauname_name + "'," +
                   "'" + values.bureau_score + "',";

            if (values.bureauscore_date == null || values.bureauscore_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.bureauscore_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }


            msSQL += "'" + values.bureau_response.Replace("'", "") + "'," +
                      "'" + values.observations.Replace("'", "") + "'," +
                      "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                msSQL = "update agr_mst_tsuprindividual2cicdocumentupload set contact_gid='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprindividual2cicdocumentupload set contact2bureau_gid='" + msGetGid + "' where contact2bureau_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprcontact2tuhighriskalert set contact_gid='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprcontact2tuhighriskalert set contact2bureau_gid='" + msGetGid + "' where contact2bureau_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Bureau Updates Added for Individual Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }

        public bool DaPostCICUploadInstitution(string employee_gid, MdlCICInstitution values)
        {


            //msSQL = "select institution2bureau_gid from  agr_mst_tsuprinstitution2cicdocumentupload where  institution2bureau_gid='" + employee_gid + "' ";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == false)
            //{
            //    values.status = false;
            //    values.message = "Kindly Add The Document";
            //    return false;
            //}

            // Document Attachments
            msSQL = "select document_name from agr_tmp_tsuprcicdocument where created_by='" + employee_gid + "'";
            lsdocument_name = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select document_path from agr_tmp_tsuprcicdocument where created_by='" + employee_gid + "'";
            lsdocument_path = objdbconn.GetExecuteScalar(msSQL);

            try
            {
                var bureauscore_date = Convert.ToDateTime(values.bureauscore_date).ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Kindly enter the valid date format";
                return false;
            }

            msGetGid = objcmnfunctions.GetMasterGID("I2BR");
            msSQL = " insert into agr_mst_tsuprinstitution2bureau(" +
                   " institution2bureau_gid ," +
                   " institution_gid," +
                   " bureauname_gid," +
                   " bureauname_name," +
                   " bureau_score," +
                   " bureauscore_date," +
                   " bureau_response," +
                   " observations," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + values.institution_gid + "'," +
                   "'" + values.bureauname_gid + "'," +
                   "'" + values.bureauname_name + "'," +
                   "'" + values.bureau_score + "',";

            if (values.bureauscore_date == null || values.bureauscore_date == "")
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.bureauscore_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }


            msSQL += "'" + values.bureau_response.Replace("'", "") + "'," +
                      "'" + values.observations.Replace("'", "") + "'," +
                      "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                msSQL = "update agr_mst_tsuprinstitution2cicdocumentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprinstitution2cicdocumentupload set institution2bureau_gid='" + msGetGid + "' where institution2bureau_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Bureau Updates Added for Institution Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

            if (mnResult != 0)
            {
                msSQL = "update agr_mst_tsuprinstitution2cicdocumentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Bureau Updates Uploaded for Institution Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }

        public void DaCICUploadIndividualDocTempList(string employee_gid, MdlCICIndividual values)
        {
            msSQL = " select tmpcicdocument_gid,document_name,document_path from agr_tmp_tsuprcicdocument " +
                                 " where created_by='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<cicuploaddoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new cicuploaddoc_list
                    {
                        document_name = dt["document_name"].ToString(),
                        //document_path = (dt["document_path"].ToString()),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        tmpcicdocument_gid = dt["tmpcicdocument_gid"].ToString(),
                    });
                    values.cicuploaddoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaCICUploadInstitutionDocTempList(string employee_gid, MdlCICInstitution values)
        {
            msSQL = " select tmpcicdocument_gid,document_name,document_path from agr_tmp_tsuprcicdocument " +
                                 " where created_by='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<cicuploaddoc_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new cicuploaddoc_list
                    {
                        document_name = dt["document_name"].ToString(),
                        //document_path = (dt["document_path"].ToString()),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        tmpcicdocument_gid = dt["tmpcicdocument_gid"].ToString(),
                    });
                    values.cicuploaddoc_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaTempCICUploadIndividualDocDelete(string tmpcicdocument_gid, MdlCICIndividual values)
        {
            msSQL = " delete from agr_tmp_tsuprcicdocument where tmpcicdocument_gid='" + tmpcicdocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Document Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaTempCICUploadInstitutionDocDelete(string tmpcicdocument_gid, MdlCICInstitution values)
        {
            msSQL = " delete from agr_tmp_tsuprcicdocument where tmpcicdocument_gid='" + tmpcicdocument_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Document Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
        }

        public void DaGetIndividualSummary(string employee_gid, MdlCICIndividual values)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if(lsapplication_gid != "")
            {
                msSQL = " select contact_gid,concat(first_name, ' ',middle_name,' ',last_name) as individual_name," +
                    " a.pan_no,aadhar_no,stakeholder_type,contact_status,institution_name,group_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from agr_mst_tsuprcontact a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where a.application_gid='" + lsapplication_gid + "' order by contact_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcicindividualList = new List<cicindividual_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcicindividualList.Add(new cicindividual_list
                        {
                            contact_gid = dt["contact_gid"].ToString(),
                            individual_name = dt["individual_name"].ToString(),
                            pan_no = dt["pan_no"].ToString(),
                            aadhar_no = dt["aadhar_no"].ToString(),
                            stakeholder_type = dt["stakeholder_type"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            contact_status = dt["contact_status"].ToString(),
                            institution_name = dt["institution_name"].ToString(),
                            group_name = dt["group_name"].ToString(),
                        });

                    }
                }
                values.cicindividual_list = getcicindividualList;
                dt_datatable.Dispose();
            }
             
            msSQL = "select application_gid,overalllimit_amount,processing_fee,doc_charges from agr_mst_tsuprapplication where application_gid='" + lsapplication_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.overalllimit_amount = objODBCDatareader["overalllimit_amount"].ToString();
                values.processing_fee = objODBCDatareader["processing_fee"].ToString();
                values.doc_charges = objODBCDatareader["doc_charges"].ToString();
                values.application_gid = objODBCDatareader["application_gid"].ToString();
            }
            objODBCDatareader.Close();
        }
        public void DaGetInstitutionSummary(string employee_gid, MdlCICInstitution values)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsapplication_gid != "")
            {
                msSQL = " select institution_gid,company_name,date_incorporation,stakeholder_type,institution_status,businessstart_date," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from agr_mst_tsuprinstitution a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where a.application_gid='" + lsapplication_gid + "' order by institution_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcicinstitutionList = new List<cicinstitution_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcicinstitutionList.Add(new cicinstitution_list
                        {
                            institution_gid = dt["institution_gid"].ToString(),
                            company_name = dt["company_name"].ToString(),
                            date_incorporation = dt["date_incorporation"].ToString(),
                            stakeholder_type = dt["stakeholder_type"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            institution_status = dt["institution_status"].ToString(),
                            businessstart_date = dt["businessstart_date"].ToString(),
                        });

                    }
                }
                values.cicinstitution_list = getcicinstitutionList;
                dt_datatable.Dispose();
            }
            
        }

        public void DaGetOverallInfo(string application_gid, string employee_gid, MdlMstApplicationAdd values)
        {

            msSQL = " select application_gid,if(application_no is null,'-',application_no) as application_no,customerref_name as customer_name,customer_urn,social_capital," +
                    " vertical_name,trade_capital,overalllimit_amount,processing_fee,doc_charges,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,status,applicant_type,hypothecation_flag,productcharge_flag " +
                    " from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where a.application_gid='" + application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.application_gid = objODBCDatareader["application_gid"].ToString();
                values.social_capital = objODBCDatareader["social_capital"].ToString();
                values.trade_capital = objODBCDatareader["trade_capital"].ToString();
                values.overalllimit_amount = objODBCDatareader["overalllimit_amount"].ToString();
                values.processing_fee = objODBCDatareader["processing_fee"].ToString();
                values.doc_charges = objODBCDatareader["doc_charges"].ToString();
                values.application_no = objODBCDatareader["application_no"].ToString();
                values.customer_name = objODBCDatareader["customer_name"].ToString();
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                values.created_by = objODBCDatareader["created_by"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
                values.application_status = objODBCDatareader["status"].ToString();
                values.applicant_type = objODBCDatareader["applicant_type"].ToString();
                values.hypothecation_flag = objODBCDatareader["hypothecation_flag"].ToString();
                values.productcharge_flag = objODBCDatareader["productcharge_flag"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
        }
        public void DaGetGeneralInfo(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " select application_gid,if(application_no is null,'-',application_no) as application_no,customerref_name as customer_name,customer_urn,social_capital," +
                    " vertical_name,trade_capital,overalllimit_amount,processing_fee,doc_charges,concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,status,applicant_type,hypothecation_flag,productcharge_flag, " +
                    " product_gid,variety_gid from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where a.application_gid='" + lsapplication_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.application_gid = objODBCDatareader["application_gid"].ToString();
                values.social_capital = objODBCDatareader["social_capital"].ToString();
                values.trade_capital = objODBCDatareader["trade_capital"].ToString();
                values.overalllimit_amount = objODBCDatareader["overalllimit_amount"].ToString();
                values.processing_fee = objODBCDatareader["processing_fee"].ToString();
                values.doc_charges = objODBCDatareader["doc_charges"].ToString();
                values.application_no = objODBCDatareader["application_no"].ToString();
                values.customer_name = objODBCDatareader["customer_name"].ToString();
                values.customer_urn = objODBCDatareader["customer_urn"].ToString();
                values.vertical_name = objODBCDatareader["vertical_name"].ToString();
                values.created_by = objODBCDatareader["created_by"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
                values.application_status = objODBCDatareader["status"].ToString();
                values.applicant_type = objODBCDatareader["applicant_type"].ToString();
                values.hypothecation_flag = objODBCDatareader["hypothecation_flag"].ToString();
                values.productcharge_flag = objODBCDatareader["productcharge_flag"].ToString();
                values.product_gid = objODBCDatareader["product_gid"].ToString();
                values.variety_gid = objODBCDatareader["variety_gid"].ToString();
            }
            objODBCDatareader.Close();
            values.status = true;
        }
        public void DaGetApplicationSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name," +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,approval_status,applicant_type,a.created_by as createdby," +
                " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date," +
                " productcharge_flag, economical_flag from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid where a.created_by='" + employee_gid + "' order by application_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        createdby = dt["createdby"].ToString(),
                    });

                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaDeleteGeneral(string application_gid, MdlMstApplicationAdd values)
        {
            msSQL = "Delete from agr_mst_tsuprapplication where application_gid='" + application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "Delete from agr_mst_tsuprapplication2contactno where application_gid='" + application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "Delete from agr_mst_tsuprapplication2email where application_gid='" + application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            msSQL = "Delete from ocs_mst_tgeneticcode where application_gid='" + application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "General Information Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while deleting";
                values.status = false;
            }
        }

        public void DaDeleteindividual(string contact_gid, string employee_gid, MdlCICIndividual values)
        {
            msSQL = "Delete from agr_mst_tsuprcontact where contact_gid='" + contact_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
                lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select contact_gid,concat(first_name, ' ',middle_name,' ',last_name) as individual_name," +
                   " a.pan_no,aadhar_no,stakeholder_type,contact_status," +
                   " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                   " from agr_mst_tsuprcontact a " +
                   " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                   " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                   " where a.application_gid='" + lsapplication_gid + "' order by contact_gid desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcicindividualList = new List<cicindividual_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcicindividualList.Add(new cicindividual_list
                        {
                            contact_gid = dt["contact_gid"].ToString(),
                            individual_name = dt["individual_name"].ToString(),
                            pan_no = dt["pan_no"].ToString(),
                            aadhar_no = dt["aadhar_no"].ToString(),
                            stakeholder_type = dt["stakeholder_type"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            contact_status = dt["contact_status"].ToString(),
                        });

                    }
                }
                values.cicindividual_list = getcicindividualList;
                dt_datatable.Dispose();
                values.message = "Individual Information Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while Deleting";
                values.status = false;
            }

        }

        public void DaDeleteinstitution(string institution_gid, string employee_gid, MdlCICInstitution values)
        {
            msSQL = "Delete from agr_mst_tsuprinstitution where institution_gid='" + institution_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
                lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);


                msSQL = " select institution_gid,company_name,date_incorporation,stakeholder_type,institution_status," +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                        " from agr_mst_tsuprinstitution a " +
                        " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                        " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                        " where a.application_gid='" + lsapplication_gid + "' order by institution_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcicinstitutionList = new List<cicinstitution_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getcicinstitutionList.Add(new cicinstitution_list
                        {
                            institution_gid = dt["institution_gid"].ToString(),
                            company_name = dt["company_name"].ToString(),
                            date_incorporation = dt["date_incorporation"].ToString(),
                            stakeholder_type = dt["stakeholder_type"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            created_by = dt["created_by"].ToString(),
                            institution_status = dt["institution_status"].ToString(),
                        });

                    }
                }
                values.cicinstitution_list = getcicinstitutionList;
                dt_datatable.Dispose();
                values.message = "Company Information Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while Deleting";
                values.status = false;
            }

        }

        public void DaGetProductList(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select application_gid,social_capital,trade_capital from agr_mst_tsuprapplication where application_gid='" + lsapplication_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {

                values.trade_capital = objODBCDatareader["trade_capital"].ToString();
                values.social_capital = objODBCDatareader["social_capital"].ToString();
                values.application_gid = objODBCDatareader["application_gid"].ToString();

            }
            objODBCDatareader.Close();
            values.status = true;
        }

        public void DaGetInstitutionEditSummary(string application_gid, MdlMstInstitutionAdd values)
        {
            msSQL = " select institution_gid, company_name,companypan_no,cin_no,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, stakeholder_type,institution_status," +
                    " date_format(a.date_incorporation,'%d-%m-%Y') as date_incorporation, date_format(a.businessstart_date,'%d-%m-%Y') as businessstart_date " +
                    " from agr_mst_tsuprinstitution a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                    " where a.application_gid = '" + application_gid + "' order by institution_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinstitution_list = new List<institution_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getinstitution_list.Add(new institution_list
                    {
                        institution_gid = (dr_datarow["institution_gid"].ToString()),
                        company_name = (dr_datarow["company_name"].ToString()),
                        companypan_no = (dr_datarow["companypan_no"].ToString()),
                        cin_no = (dr_datarow["cin_no"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        date_incorporation = (dr_datarow["date_incorporation"].ToString()),
                        stakeholder_type = (dr_datarow["stakeholder_type"].ToString()),
                        businessstart_date = (dr_datarow["businessstart_date"].ToString()),
                        institution_status = (dr_datarow["institution_status"].ToString()),
                    });
                }
                values.institution_list = getinstitution_list;
            }
            dt_datatable.Dispose();
        }

        public void DaGetProductChargesEditSummary(string application_gid, MdlProductCharges values)
        {
            msSQL = " select application_gid, application_no,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                    " from agr_mst_tsuprapplication a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                      " where a.application_gid = '" + application_gid + "' order by application_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getproductcharges_list = new List<productcharges_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getproductcharges_list.Add(new productcharges_list
                    {
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        application_no = (dr_datarow["application_no"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                    });
                }
                values.productcharges_list = getproductcharges_list;
            }
            dt_datatable.Dispose();

            msSQL = "select productcharge_flag,economical_flag, applicant_type from agr_mst_tsuprapplication where application_gid='" + application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.productcharge_flag = objODBCDatareader["productcharge_flag"].ToString();
                values.economical_flag = objODBCDatareader["economical_flag"].ToString();
                values.applicant_type = objODBCDatareader["applicant_type"].ToString();
            }
            objODBCDatareader.Close();
        }

        public void DaGetSocialTradeSummary(string application_gid, MdlMstApplicationAdd values)
        {
            try
            {
                msSQL = " SELECT application_gid,application_no,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                        " FROM agr_mst_tsuprapplication a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                        " where a.application_gid = '" + application_gid + "' order by a.application_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplication_list = new List<applicationlist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplication_list.Add(new applicationlist
                        {
                            application_gid = (dr_datarow["application_gid"].ToString()),
                            application_no = (dr_datarow["application_no"].ToString()),

                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),

                        });
                    }
                    values.applicationlist = getapplication_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetAppTempClear(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = "delete from tmp_application where employee_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
        }
        public void DaGetProceed(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);


            msSQL = "select a.application_gid from agr_mst_tsuprapplication a" +
                   " left join agr_mst_tsuprcontact b on a.application_gid = b.application_gid " +
                   " left join agr_mst_tsuprinstitution c on a.application_gid = c.application_gid" +
                   " where a.application_gid ='" + lsapplication_gid + "'" +
                   " and(b.stakeholder_type in ('Applicant','Borrower') or c.stakeholder_type in ('Applicant','Borrower'))";
            string application_gid = objdbconn.GetExecuteScalar(msSQL);
            if (application_gid == "" || application_gid == null)
            {
                values.proceed_flag = "N";
            }
            else
            {
                string lsapplicant_type = "";
                msSQL = "select applicant_type,onboarding_status from agr_mst_tsuprapplication where application_gid='" + lsapplication_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    lsapplicant_type = objODBCDatareader["applicant_type"].ToString();
                    values.onboarding_status = objODBCDatareader["onboarding_status"].ToString();
                }
                objODBCDatareader.Close(); 

                if (lsapplicant_type == "" || lsapplicant_type == null)
                {
                    values.proceed_flag = "N";
                }
                else
                { 
                    msSQL = "select productcharge_flag from  agr_mst_tsuprapplication where application_gid='" + lsapplication_gid + "'";
                    string lsproductcharge_flag = objdbconn.GetExecuteScalar(msSQL);
                    if (lsproductcharge_flag == "N" || lsproductcharge_flag == null || lsproductcharge_flag == "")
                    {
                        values.proceed_flag = "N";
                    }
                    else
                    {
                        bool lsreaderstatus = true;
                        if (values.onboarding_status == "Direct")
                            lsreaderstatus = false;
                        msSQL = "select application_gid from agr_mst_tsuprapplication2loan where application_gid='" + lsapplication_gid + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == lsreaderstatus)
                        {
                            msSQL = "select application_gid from agr_mst_tsuprgroup where application_gid = '" + lsapplication_gid + "' and group_status='Incomplete'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == false)
                            {
                                objODBCDatareader.Close();
                                msSQL = "select application_gid from agr_mst_tsuprcontact where application_gid = '" + lsapplication_gid + "' and contact_status='Incomplete'";
                                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                if (objODBCDatareader.HasRows == false)
                                {
                                    objODBCDatareader.Close();
                                    msSQL = "select application_gid from agr_mst_tsuprinstitution where application_gid = '" + lsapplication_gid + "' and institution_status='Incomplete'";
                                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                                    if (objODBCDatareader.HasRows == false)
                                    {
                                        objODBCDatareader.Close();
                                        msSQL = " select clustermanager_name,zonalhead_name,regionalhead_name,businesshead_name from agr_mst_tsuprapplication where application_gid = '" + lsapplication_gid + "'";
                                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader1.HasRows == true)
                                        {
                                            values.cluster_head = objODBCDatareader1["clustermanager_name"].ToString();
                                            values.zonal_head = objODBCDatareader1["zonalhead_name"].ToString();
                                            values.regional_head = objODBCDatareader1["regionalhead_name"].ToString();
                                            values.business_head = objODBCDatareader1["businesshead_name"].ToString();
                                        }

                                        lsclusterhead = values.cluster_head;
                                        lszonalhead = values.zonal_head;
                                        lsregionahead = values.regional_head;
                                        lsbusinesshead = values.business_head;

                                        objODBCDatareader1.Close();
                                        msSQL = " select a.employeereporting_to,concat(c.user_firstname,' ',c.user_lastname,' || ',c.user_code) as level_zero,b.employee_gid, " +
                                                "  concat( g.user_firstname, ' ', g.user_lastname, ' || ', g.user_code) as level_one  " +
                                                "  from adm_mst_tmodule2employee a " +
                                                "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
                                                "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                                                "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                                                "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                                                 "  where a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in " +
                                                 "  (select modulereportingto_gid from adm_mst_tcompany)) " +
                                                 " and c.user_status = 'Y' and b.employee_gid ='" + employee_gid + "'  group by a.employee_gid ";
                                        objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                                        if (objODBCDatareader1.HasRows == true)
                                        {
                                            values.level_zero = objODBCDatareader1["level_zero"].ToString();
                                            values.level_one = objODBCDatareader1["level_one"].ToString();
                                            objODBCDatareader1.Close();
                                        } 
                                        values.proceed_flag = "Y";  
                                    }
                                    else
                                    {
                                        objODBCDatareader.Close();
                                        values.proceed_flag = "N";
                                    }
                                }
                                else
                                {
                                    objODBCDatareader.Close();
                                    values.proceed_flag = "N";
                                }
                            }
                            else
                            {
                                objODBCDatareader.Close();
                                values.proceed_flag = "N";
                            }
                            objODBCDatareader.Close();
                        }
                        else
                        {
                            objODBCDatareader.Close();
                            values.proceed_flag = "N";
                        }

                    }
                }
            }

        }
        public void DaPostAppProceed(string employee_gid, string user_gid, MdlMstApplicationAdd values)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            int k;
            string lsapproval_gid;
            string lsapprovalname;
           
            msSQL = " select drm_gid, drm_name from agr_mst_tsuprapplication where application_gid = '" + lsapplication_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                k = 1;
                string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                sToken = "";
                int Length = 100;
                for (int j = 0; j < Length; j++)
                {
                    string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                    sToken += sTempChars;
                }

                msGetGid = objcmnfunctions.GetMasterGID("APAP");
                msSQL = "Insert into agr_trn_tsuprapplicationapproval( " +
                       " applicationapproval_gid, " +
                       " application_gid," +
                       " approval_gid," +
                       " approval_name," +
                       " approval_type," +
                       " hierary_level," +
                       " approval_token," +
                       " initiate_flag," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + lsapplication_gid + "'," +
                       "'" + objODBCDatareader["drm_gid"].ToString() + "'," +
                       "'" + objODBCDatareader["drm_name"].ToString() + "'," +
                       "'sequence'," +
                       "'" + k + "'," +
                       "'" + sToken + "'," +
                       "'Y'," +
                       "'" + user_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }
            objODBCDatareader.Close();

            msSQL = " select clustermanager_name,zonalhead_name,regionalhead_name,businesshead_name,clustermanager_gid,zonalhead_gid,regionalhead_gid,businesshead_gid from agr_mst_tsuprapplication where application_gid = '" + lsapplication_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {

                for (k = 2; k < 6; k++)
                {
                    char level;
                    level = Convert.ToChar(k);
                    lsapproval_gid = "";
                    lsapprovalname = "";

                    if (level == '\u0002')
                    {
                        lsapproval_gid = objODBCDatareader["clustermanager_gid"].ToString();
                        lsapprovalname = objODBCDatareader["clustermanager_name"].ToString();
                    }
                    else if (level == '\u0003')
                    {
                        lsapproval_gid = objODBCDatareader["regionalhead_gid"].ToString();
                        lsapprovalname = objODBCDatareader["regionalhead_name"].ToString();
                    }
                    else if (level == '\u0004')
                    {
                        lsapproval_gid = objODBCDatareader["zonalhead_gid"].ToString();
                        lsapprovalname = objODBCDatareader["zonalhead_name"].ToString();
                    }
                    else if (level == '\u0005')
                    {
                        lsapproval_gid = objODBCDatareader["businesshead_gid"].ToString();
                        lsapprovalname = objODBCDatareader["businesshead_name"].ToString();
                    }
                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                    sToken = "";
                    int Length = 100;
                    for (int j = 0; j < Length; j++)
                    {
                        string sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                        sToken += sTempChars;
                    }

                    msGetGid = objcmnfunctions.GetMasterGID("APAP");

                    msSQL = "Insert into agr_trn_tsuprapplicationapproval( " +
                           " applicationapproval_gid, " +
                           " application_gid," +
                           " approval_gid," +
                           " approval_name," +
                           " approval_type," +
                           " hierary_level," +
                           " approval_token," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                           "'" + lsapplication_gid + "'," +
                           "'" + lsapproval_gid + "'," +
                           "'" + lsapprovalname + "'," +
                           "'sequence'," +
                           "'" + k + "'," +
                           "'" + sToken + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                }
            }

            objODBCDatareader.Close();


            //msSQL = "update agr_mst_tsuprapplication set approval_flag='Y', approval_status='Submitted to Underwriting',submitted_by='" + employee_gid + "'," +
            //    " submitted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
            //    " where application_gid='" + lsapplication_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



            msSQL = "update agr_mst_tsuprapplication set approval_status='Submitted to Approval',submitted_by='" + employee_gid + "'," +
                " submitted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where application_gid='" + lsapplication_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "select applicant_type from agr_mst_tsuprapplication where application_gid='" + lsapplication_gid + "'";
                string lsapplicant_type = objdbconn.GetExecuteScalar(msSQL);

                if (lsapplicant_type == "Individual")
                {



                    msSQL = "select concat(first_name,middle_name,last_name) as customer_name,mobile_no,email_address,contact_gid from agr_mst_tsuprcontact" +
                        " where application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscustomer_name = objODBCDatareader["customer_name"].ToString();
                        lsmobileno = objODBCDatareader["mobile_no"].ToString();
                        lsemail_address = objODBCDatareader["email_address"].ToString();

                        //Region
                        msSQL = "select state from agr_mst_tsuprcontact2address where primary_status='Yes' and contact_gid='" + objODBCDatareader["contact_gid"].ToString() + "'";
                        lsregion = objdbconn.GetExecuteScalar(msSQL);

                        //Main Table Insertion
                        msSQL = " update agr_mst_tsuprapplication set customer_name='" + lscustomer_name + "'," +
                       " mobile_no='" + lsmobileno + "'," +
                       " email_address='" + lsemail_address + "'," +
                       " region='" + lsregion + "'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where application_gid='" + lsapplication_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    objODBCDatareader.Close();

                }
                else
                {


                    msSQL = "select company_name,mobile_no,email_address,institution_gid from agr_mst_tsuprinstitution where " +
                        " application_gid='" + lsapplication_gid + "' and stakeholder_type in ('Applicant','Borrower')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lscustomer_name = objODBCDatareader["company_name"].ToString();
                        lsmobileno = objODBCDatareader["mobile_no"].ToString();
                        lsemail_address = objODBCDatareader["email_address"].ToString();

                        //Region
                        msSQL = "select state from agr_mst_tsuprinstitution2address where primary_status='Yes' and institution_gid='" + objODBCDatareader["institution_gid"].ToString() + "'";
                        lsregion = objdbconn.GetExecuteScalar(msSQL);

                        //Main Table 
                        msSQL = " update agr_mst_tsuprapplication set customer_name='" + lscustomer_name + "'," +
                       " mobile_no='" + lsmobileno + "'," +
                       " email_address='" + lsemail_address + "'," +
                       " region='" + lsregion + "'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where application_gid='" + lsapplication_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    objODBCDatareader.Close();
                }
                msSQL = "select onboarding_status from agr_mst_tsuprapplication where application_gid='" + lsapplication_gid + "'";
                string lsonboarding_status = objdbconn.GetExecuteScalar(msSQL);

                if (lsonboarding_status == "Direct")
                {
                    if (mnResult != 0)
                    {
                        DaAgrMstSuprApplicationEdit objDaAgrMstApplicationEdit = new DaAgrMstSuprApplicationEdit();
                        objDaAgrMstApplicationEdit.FnAutoApprovalFlow(lsapplication_gid, employee_gid, user_gid);
                        values.status = true;
                        values.message = "Supplier Proposal Submitted successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured while updated";
                    }
                }
                else
                {
                    if (mnResult != 0)
                    {
                        string cluster_head, zonal_head, rm_name;
                        try
                        {
                            msSQL = " select clustermanager_gid,zonalhead_gid,regionalhead_gid,businesshead_gid from agr_mst_tsuprapplication where application_gid = '" + lsapplication_gid + "'";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                cluster_head_gid = objODBCDatareader1["clustermanager_gid"].ToString();
                                zonal_head_gid = objODBCDatareader1["zonalhead_gid"].ToString();
                                regional_head_gid = objODBCDatareader1["regionalhead_gid"].ToString();
                                business_head_gid = objODBCDatareader1["businesshead_gid"].ToString();
                            }

                            objODBCDatareader1.Close();
                            msSQL = " select approval_gid,approval_name from agr_trn_tsuprapplicationapproval where application_gid = '" + lsapplication_gid + "' and hierary_level ='1'";
                            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader1.HasRows == true)
                            {
                                reportingto_gid = objODBCDatareader1["approval_gid"].ToString();
                                reportingto_name = objODBCDatareader1["approval_name"].ToString();
                            }
                            objODBCDatareader1.Close();
                            msSQL = " SELECT pop_server, pop_port, pop_username, pop_password" +
                                    " FROM adm_mst_tcompany";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                ls_server = objODBCDatareader["pop_server"].ToString();
                                ls_port = Convert.ToInt32(objODBCDatareader["pop_port"]);
                                ls_username = objODBCDatareader["pop_username"].ToString();
                                ls_password = objODBCDatareader["pop_password"].ToString();
                            }
                            objODBCDatareader.Close();

                            msSQL = "select application_no from agr_mst_tsuprapplication where application_gid='" + lsapplication_gid + "'";
                            application_no = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = "select customerref_name from agr_mst_tsuprapplication where application_gid='" + lsapplication_gid + "'";
                            customer_name = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = "select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.clustermanager_gid where application_gid='" + lsapplication_gid + "'";
                            cluster_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.zonalhead_gid where a.application_gid='" + lsapplication_gid + "'";
                            zonalhead_mailid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid where a.application_gid='" + lsapplication_gid + "'";
                            regional_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.businesshead_gid where a.application_gid='" + lsapplication_gid + "'";
                            business_head_mailid = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select b.employee_emailid from agr_trn_tsuprapplicationapproval a left join hrm_mst_temployee b on b.employee_gid = a.approval_gid where a.application_gid='" + lsapplication_gid + "'  and hierary_level ='1'";
                            reportingto_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select b.employee_emailid from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.created_by where a.application_gid='" + lsapplication_gid + "'";
                            creater_mailid = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = "select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.clustermanager_gid join adm_mst_tuser c on c.user_gid = b.user_gid where application_gid='" + lsapplication_gid + "'";
                            cluster_head = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.zonalhead_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + lsapplication_gid + "'";
                            zonal_head = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.relationshipmanager_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + lsapplication_gid + "'";
                            rm_name = objdbconn.GetExecuteScalar(msSQL);
                            msSQL = " select  concat( c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code)  from agr_mst_tsuprapplication a left join hrm_mst_temployee b on b.employee_gid = a.regionalhead_gid left join adm_mst_tuser c on c.user_gid = b.user_gid where a.application_gid='" + lsapplication_gid + "'";
                            regionalhead_name = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select  a.overalllimit_amount  from agr_mst_tsuprapplication a  where a.application_gid='" + lsapplication_gid + "'";
                            lsoveralllimit_amount = objdbconn.GetExecuteScalar(msSQL);

                            msSQL = " select  group_concat(product_type) as Product, group_concat(productsub_type) as Program, group_concat(rate_interest) as Margin from agr_mst_tsuprapplication2loan  where application_gid = '" + lsapplication_gid + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                ls_Product = objODBCDatareader["Product"].ToString();
                                ls_Program = objODBCDatareader["Program"].ToString();
                                ls_Margin = objODBCDatareader["Margin"].ToString();
                            }
                            objODBCDatareader.Close();

                            tomail_id = reportingto_mailid;
                            //lssource = ConfigurationManager.AppSettings["img_path"];

                            sub = " ARN(" + application_no + ") : Application approval required ";
                            body = "<style>table, th, td {border: 1px solid black;border-collapse: collapse;}</style>";
                            body = body + "<table style='border-right: 1px solid black;border-top: 1px solid black;border-bottom: 1px solid black;'><tr><td style='border-right-color:white;align:center;'>";
                            //body = body + "<br />";
                            //body = body + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp <img style='height:150px; width:380px;' src='" + lssource + "'><br />";
                            //body = body + "<br />";
                            body = body + " &nbsp&nbsp Dear Sir/Madam,<br />";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp Greetings<br />";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp The below application has been submitted, please validate and approve to proceed for underwriting.<br />";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp <b>Application Number:</b> " + application_no + "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Customer Name:</b> " + HttpUtility.HtmlEncode(customer_name)+ "<br /><br />";
                            body = body + "&nbsp&nbsp <b>RM Name: </b>" + HttpUtility.HtmlEncode(rm_name )+ "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Cluster Head Name:</b> " + HttpUtility.HtmlEncode(cluster_head )+ "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Zonal Head Name:</b> " + HttpUtility.HtmlEncode(zonal_head )+ "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Regional Head Name:</b> " + HttpUtility.HtmlEncode(regionalhead_name )+ "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Product:</b> " + ls_Product + "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Program:</b> " + ls_Program + "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Overall Limit Amount:</b> " + HttpUtility.HtmlEncode(lsoveralllimit_amount)+ "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Margin:</b> " + HttpUtility.HtmlEncode(ls_Margin) + "<br /><br />";
                            body = body + "&nbsp&nbsp <b>Action Time:</b> " + DateTime.Now.ToString("dd-MM-yyyy hh:mm tt") + "<br /><br />";
                            body = body + "<br />";
                            body = body + "&nbsp&nbsp Login to " + ConfigurationManager.AppSettings["livedomain_url"].ToString() + " and complete the necessary actions. <br /> ";
                            //body = body + "&nbsp&nbsp Regards,";
                            //body = body + "<br />";
                            //body = body + "&nbsp&nbsp Sam-Custopedia <br /> ";
                            body = body + "<br />";
                            body = body + "</td><td style='margin-left:20px; border-left-color:white;'>&nbsp&nbsp</td></tr></table>";

                            MailMessage message = new MailMessage();
                            SmtpClient smtp = new SmtpClient();
                            message.From = new MailAddress(ls_username);
                            message.To.Add(new MailAddress(tomail_id));
                            lsBccmail_id = ConfigurationManager.AppSettings["SamagroApprovalBccMail"].ToString();

                            if (lsBccmail_id != null & lsBccmail_id != string.Empty & lsBccmail_id != "")
                            {
                                lsBCCReceipients = lsBccmail_id.Split(',');
                                if (lsBccmail_id.Length == 0)
                                {
                                    message.Bcc.Add(new MailAddress(lsBccmail_id));
                                }
                                else
                                {
                                    foreach (string BCCEmail in lsBCCReceipients)
                                    {
                                        message.Bcc.Add(new MailAddress(BCCEmail)); //Adding Multiple BCC email Id
                                    }
                                }
                            }
                            cc_mailid = "" + cluster_head_mailid + "," + regional_head_mailid + "," + zonalhead_mailid + "";

                            if (cc_mailid != null & cc_mailid != string.Empty & cc_mailid != "")
                            {
                                lsCCReceipients = cc_mailid.Split(',');
                                if (cc_mailid.Length == 0)
                                {
                                    message.CC.Add(new MailAddress(cc_mailid));
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
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            smtp.Credentials = new NetworkCredential(ls_username, ls_password);
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Send(message);
                            values.status = true;
                            values.message = "Supplier Proposal Submitted successfully";
                        }
                        catch (Exception ex)
                        {
                            values.message = ex.ToString();
                            values.status = false;

                        }


                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured while updated";
                    }
                }
            }
        }

        public void DaGetAppAssignmentSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.status,a.applicant_type," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date, a.productcharge_flag, a.economical_flag,a.approval_status, " +
                    " a.overalllimit_amount, region from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " where a.approval_flag='Y' group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        region = dt["region"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                    });

                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }
        public void GetTempApp(string employee_gid, result values)
        {
            msSQL = "delete from agr_mst_tsuprapplication2contactno where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprapplication2email where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprapplication2geneticcode where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprapplication2product where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = "delete from agr_mst_tsuprcontact where application_gid='" + employee_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //msSQL = "delete from agr_mst_tsuprinstitution where application_gid='" + employee_gid + "'";
            //mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);




            values.status = true;
        }
        public void DaDeleteApplicationAdd(string application_gid, MdlMstApplicationAdd values)
        {
            msSQL = "delete from agr_mst_tsuprapplication where application_gid='" + application_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Application Deleted successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while deleted";
            }
        }
        public void DaGetAppSocialTradeSummary(MdlMstApplicationAdd values, string employee_gid)
        {
            try
            {
                msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
                lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "SELECT application_gid,application_no,social_capital,trade_capital," +
                         "date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                         " date_format(a.updated_date, '%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by, " +
                        " concat(e.user_firstname, ' ', e.user_lastname, ' / ', e.user_code) as updated_by " +
                        " FROM agr_mst_tsuprapplication a left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " left join hrm_mst_temployee d on a.updated_by = d.employee_gid " +
                        " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                        " where a.application_gid = '" + lsapplication_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.application_no = objODBCDatareader["application_no"].ToString();
                    values.social_capital = objODBCDatareader["social_capital"].ToString();
                    values.trade_capital = objODBCDatareader["trade_capital"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.created_date = objODBCDatareader["created_date"].ToString();
                    values.updated_date = objODBCDatareader["updated_date"].ToString();
                    values.created_by = objODBCDatareader["created_by"].ToString();
                    values.updated_by = objODBCDatareader["updated_by"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetAppProductcharges(MdlMstApplicationAdd values, string employee_gid)
        {
            try
            {
                msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
                lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select application_gid,overalllimit_amount,processing_fee,doc_charges, applicant_type, productcharge_flag, economical_flag," +
                    " productcharges_status" +
                    " from agr_mst_tsuprapplication where application_gid='" + lsapplication_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.overalllimit_amount = objODBCDatareader["overalllimit_amount"].ToString();
                    values.processing_fee = objODBCDatareader["processing_fee"].ToString();
                    values.doc_charges = objODBCDatareader["doc_charges"].ToString();
                    values.application_gid = objODBCDatareader["application_gid"].ToString();
                    values.applicant_type = objODBCDatareader["applicant_type"].ToString();
                    values.economical_flag = objODBCDatareader["economical_flag"].ToString();
                    values.productcharge_flag = objODBCDatareader["productcharge_flag"].ToString();
                    values.productcharges_status = objODBCDatareader["productcharges_status"].ToString();

                }
                objODBCDatareader.Close();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetDOB(string age, MdlMstApplicationAdd values)
        {
            if (age == "" || age == null)
            {

            }
            else
            {
                int dobYear = 21;
                int dobMonth = 10;
                int dobDay = 22;

                //LocalDate now = LocalDate.now();
                //LocalDate dob = now.minusYears(dobYear).minusMonths(dobMonth).minusDays(dobDay);

                //DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd/MM/yyyy");
                //System.out.println(dob.format(formatter));

                msSQL = " select (year(now())-('" + age + "')) as dob ";
                values.dob = objdbconn.GetExecuteScalar(msSQL);
            }

            values.status = true;
        }
        public void DaProductchargesTmpClear(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = "delete from agr_mst_tsuprapplication2loan where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprapplication2collateral where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprapplication2buyer where application2loan_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprapplication2hypothecation where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsupruploadhypothecationocument where application2hypothecation_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsupruploadcollateraldocument where application2loan_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from agr_mst_tsuprapplication2product  where application2loan_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaPostcheckDate(MdlMstInstitutionAdd values)
        {
            DateTime start_date = DateTime.Parse(Convert.ToDateTime(values.start_date).ToShortDateString());
            DateTime nowdate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            if (start_date > nowdate)
            {
                values.status = false;
                values.message = "Future Date is Not Allowed...";
                return;
            }
        }
        public void DaSubmitOverallLimit(string employee_gid, MdlProductCharges values)
        {

            msSQL = " update agr_mst_tsuprapplication set " +
                    " overalllimit_amount='" + values.overalllimit_amount + "'," +
                    " validityoveralllimit_year='" + values.validityoveralllimit_year + "'," +
                    " validityoveralllimit_month='" + values.validityoveralllimit_month + "'," +
                    " validityoveralllimit_days='" + values.validityoveralllimit_days + "'," +
                    " calculationoveralllimit_validity='" + values.calculationoveralllimit_validity + "'," +
                    " productcharge_flag='Y'," +
                    " productcharges_status='Incomplete'," +
               " updated_by='" + employee_gid + "'," +
               " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
               " where application_gid='" + values.application_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Overall Limit Submitted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }

        }

        public void DaPostServiceCharges(string employee_gid, MdlProductCharges values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AP2C");

            string producttypegid = string.Empty;
            string producttype = string.Empty;
            string application2loan_gid = string.Empty;
            if (values.producttypelist != null)
            {
                for (var i = 0; i < values.producttypelist.Count; i++)
                {
                    producttypegid += values.producttypelist[i].producttype_gid + ",";
                    producttype += values.producttypelist[i].product_type + ",";
                    application2loan_gid += values.producttypelist[i].application2loan_gid + ",";
                }
                producttypegid = producttypegid.TrimEnd(',');
                producttype = producttype.TrimEnd(',');
                application2loan_gid = application2loan_gid.TrimEnd(',');
            }

            msSQL = "insert into agr_mst_tsuprapplicationservicecharge(" +
                " application2servicecharge_gid," +
                " application2loan_gid, " +
                " application_gid," +
                " processing_fee," +
                " processing_collectiontype," +
                " doc_charges," +
                " doccharge_collectiontype," +
                " fieldvisit_charges," +
                " fieldvisit_charges_collectiontype," +
                " adhoc_fee," +
                " adhoc_collectiontype," +
                " life_insurance," +
                " lifeinsurance_collectiontype," +
                " acct_insurance," +
                " acctinsurance_collectiontype," +
                " total_collect," +
                " total_deduct," +
                " product_type," +
                " producttype_gid," +
                " created_by," +
                " created_date) values(" +
                 "'" + msGetGid + "'," +
                 "'" + application2loan_gid + "'," +
                       "'" + values.application_gid + "'," +
                       "'" + values.processing_fee + "'," +
                       "'" + values.processing_collectiontype + "'," +
                       "'" + values.doc_charges + "'," +
                       "'" + values.doccharge_collectiontype + "'," +
                       "'" + values.fieldvisit_charge + "'," +
                       "'" + values.fieldvisit_collectiontype + "'," +
                       "'" + values.adhoc_fee + "'," +
                       "'" + values.adhoc_collectiontype + "'," +
                       "'" + values.life_insurance + "'," +
                       "'" + values.lifeinsurance_collectiontype + "'," +
                       "'" + values.acct_insurance + "'," +
                       "'" + values.acctinsurance_collectiontype + "'," +
                       "'" + values.total_collect + "'," +
                       "'" + values.total_deduct + "'," +
                       "'" + producttype + "'," +
                       "'" + producttypegid + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



            msSQL = " select application_gid,application2servicecharge_gid, processing_fee,processing_collectiontype,doc_charges," +
                    " doccharge_collectiontype,fieldvisit_charges,fieldvisit_charges_collectiontype,adhoc_fee,adhoc_collectiontype," +
                    " life_insurance,lifeinsurance_collectiontype,acct_insurance,total_collect,total_deduct,product_type,acctinsurance_collectiontype, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                    " from agr_mst_tsuprapplicationservicecharge a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                      " where a.application_gid = '" + values.application_gid + "' order by application2servicecharge_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getproductcharges_list = new List<servicecharges_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getproductcharges_list.Add(new servicecharges_list
                    {
                        application2servicecharge_gid = (dr_datarow["application2servicecharge_gid"].ToString()),
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
                        total_deduct = (dr_datarow["total_deduct"].ToString()),
                        product_type = (dr_datarow["product_type"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                    });
                }
                values.servicecharges_list = getproductcharges_list;
            }
            dt_datatable.Dispose();
            if (mnResult != 0)
            {
                msSQL = "update agr_mst_tsuprapplication set productcharges_status='Completed',productcharge_flag='Y' where application_gid = '" + values.application_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Service Charge Details added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }
        public void DaGetServiceCharge(string employee_gid, MdlProductCharges values)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);



            msSQL = " select application_gid,application2servicecharge_gid, processing_fee,processing_collectiontype,doc_charges," +
                    " doccharge_collectiontype,fieldvisit_charges,fieldvisit_charges_collectiontype,adhoc_fee,adhoc_collectiontype," +
                    " life_insurance,lifeinsurance_collectiontype,acct_insurance,total_collect,total_deduct,product_type," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by" +
                    " from agr_mst_tsuprapplicationservicecharge a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                      " where a.application_gid = '" + lsapplication_gid + "' order by application2servicecharge_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getproductcharges_list = new List<servicecharges_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getproductcharges_list.Add(new servicecharges_list
                    {
                        application2servicecharge_gid = (dr_datarow["application2servicecharge_gid"].ToString()),
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
                        total_deduct = (dr_datarow["total_deduct"].ToString()),
                        product_type = (dr_datarow["product_type"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                    });
                }
                values.servicecharges_list = getproductcharges_list;
            }
            dt_datatable.Dispose();

        }
        public void DaGetLimit(string application_gid, MdlMstApplicationAdd values, string employee_gid)
        {

            msSQL = "select format(overalllimit_amount,2,'en_IN') from agr_mst_tsuprapplication where application_gid='" + application_gid + "'";
            values.overalllimit_amount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select sum(loanfacility_amount) from agr_mst_tsuprapplication2loan where application_gid='" + employee_gid + "'" +
                " or application_gid='" + application_gid + "'";
            values.loanfacility_amount = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select sa_status from agr_mst_tsuprapplication where application_gid='" + application_gid + "'";
            values.sa_status = objdbconn.GetExecuteScalar(msSQL);

        }
        public void DaGetproduct(string application_gid, MdlList values, string employee_gid)
        {

            msSQL = "select application2loan_gid,producttype_gid,product_type,productsubtype_gid,productsub_type from agr_mst_tsuprapplication2loan where application_gid='" + application_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getSegment = new List<product_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getSegment.Add(new product_list
                    {
                        application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                        producttype_gid = (dr_datarow["producttype_gid"].ToString()),
                        product_type = (dr_datarow["product_type"].ToString()),
                        productsubtype_gid = (dr_datarow["productsubtype_gid"].ToString()),
                        productsub_type = (dr_datarow["productsub_type"].ToString()),
                    });
                }
                values.product_list = getSegment;
            }
            dt_datatable.Dispose();
            values.status = true;

        }
        public void DaGetHypothecation(string employee_gid, MdlMstHypothecation values)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select application2hypothecation_gid,securitytype_gid,security_type,security_description,security_value," +
                    " date_format(securityassessed_date,'%d-%m-%Y') as securityassessed_date,asset_id,roc_fillingid,CERSAI_fillingid," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " hypoobservation_summary,primary_security " +
                    " from agr_mst_tsuprapplication2hypothecation a " +
                     " left join hrm_mst_temployee b on b.employee_gid = a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid where application_gid='" + lsapplication_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.application2hypothecation_gid = objODBCDatareader["application2hypothecation_gid"].ToString();
                values.securitytype_gid = objODBCDatareader["securitytype_gid"].ToString();
                values.security_type = objODBCDatareader["security_type"].ToString();
                values.security_description = objODBCDatareader["security_description"].ToString();
                values.security_value = objODBCDatareader["security_value"].ToString();
                values.securityassessed_date = objODBCDatareader["securityassessed_date"].ToString();
                values.asset_id = objODBCDatareader["asset_id"].ToString();
                values.roc_fillingid = objODBCDatareader["roc_fillingid"].ToString();
                values.CERSAI_fillingid = objODBCDatareader["CERSAI_fillingid"].ToString();
                values.hypoobservation_summary = objODBCDatareader["hypoobservation_summary"].ToString();
                values.primary_security = objODBCDatareader["primary_security"].ToString();
                values.created_by = objODBCDatareader["created_by"].ToString();
                values.created_date = objODBCDatareader["created_date"].ToString();
            }
            values.status = true;
            values.message = "success";
            objODBCDatareader.Close();
        }


        // Group Address Details

        public bool DaPostGroupAddressDetail(string employee_gid, string user_gid, MdlMstAddressDetails values)
        {
            msSQL = "select primary_status from agr_mst_tsuprgroup2address where primary_status='Yes' and (group_gid='" + employee_gid + "' or group_gid='" + values.group_gid + "')";
            string lsprimary_status = objdbconn.GetExecuteScalar(msSQL);
            if (lsprimary_status == (values.primary_status))
            {
                values.status = false;
                values.message = "Already Primary Address Added";
                return false;
            }
            msSQL = "select group2address_gid from agr_mst_tsuprgroup2address where addresstype_name='" + values.address_type + "' and (group_gid='" + employee_gid + "' or group_gid='" + values.group_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Address Type Added";
                return false;
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("G2AD");
            msSQL = " insert into agr_mst_tsuprgroup2address(" +
                    " group2address_gid," +
                    " group_gid," +
                    " addresstype_gid," +
                    " addresstype_name," +
                    " addressline1," +
                    " addressline2," +
                    " primary_status," +
                    " landmark," +
                    " postal_code," +
                    " city," +
                    " taluka," +
                    " district," +
                    " state," +
                    " country," +
                    " latitude," +
                    " longitude," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.address_typegid + "'," +
                    "'" + values.address_type + "'," +
                    "'" + values.addressline1 + "'," +
                    "'" + values.addressline2 + "'," +
                    "'" + values.primary_status + "'," +
                    "'" + values.landmark + "'," +
                    "'" + values.postal_code + "'," +
                    "'" + values.city + "'," +
                    "'" + values.taluka + "'," +
                    "'" + values.district + "'," +
                    "'" + values.state + "'," +
                    "'" + values.country + "'," +
                    "'" + values.latitude + "'," +
                    "'" + values.longitude + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Address Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }

        public void DaGetGroupAddressList(string employee_gid, MdlMstAddressDetails values)
        {
            msSQL = "  select group2address_gid,addresstype_name,primary_status, addressline1, addressline2, taluka, district, state, country, latitude, longitude, landmark," +
                    " postal_code from agr_mst_tsuprgroup2address where group_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstaddress_list = new List<mstaddress_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstaddress_list.Add(new mstaddress_list
                    {
                        group2address_gid = (dr_datarow["group2address_gid"].ToString()),
                        address_type = (dr_datarow["addresstype_name"].ToString()),
                        primary_status = (dr_datarow["primary_status"].ToString()),
                        addressline1 = (dr_datarow["addressline1"].ToString()),
                        addressline2 = (dr_datarow["addressline2"].ToString()),
                        taluka = (dr_datarow["taluka"].ToString()),
                        district = (dr_datarow["district"].ToString()),
                        state = (dr_datarow["state"].ToString()),
                        country = (dr_datarow["country"].ToString()),
                        latitude = (dr_datarow["latitude"].ToString()),
                        longitude = (dr_datarow["longitude"].ToString()),
                        postal_code = (dr_datarow["postal_code"].ToString()),
                        landmark = (dr_datarow["landmark"].ToString())
                    });
                }
                values.mstaddress_list = getmstaddress_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditGroupAddressDetail(string group2address_gid, MdlMstAddressDetails values)
        {
            try
            {
                msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, group_gid, group2address_gid " +
                    " from agr_mst_tsuprgroup2address where group2address_gid='" + group2address_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.address_typegid = objODBCDatareader["addresstype_gid"].ToString();
                    values.address_type = objODBCDatareader["addresstype_name"].ToString();
                    values.addressline1 = objODBCDatareader["addressline1"].ToString();
                    values.addressline2 = objODBCDatareader["addressline2"].ToString();
                    values.landmark = objODBCDatareader["landmark"].ToString();
                    values.taluka = objODBCDatareader["taluka"].ToString();
                    values.primary_status = objODBCDatareader["primary_status"].ToString();
                    values.postal_code = objODBCDatareader["postal_code"].ToString();
                    values.city = objODBCDatareader["city"].ToString();
                    values.district = objODBCDatareader["district"].ToString();
                    values.state = objODBCDatareader["state"].ToString();
                    values.country = objODBCDatareader["country"].ToString();
                    values.latitude = objODBCDatareader["latitude"].ToString();
                    values.longitude = objODBCDatareader["longitude"].ToString();
                    values.group_gid = objODBCDatareader["group_gid"].ToString();
                    values.group2address_gid = objODBCDatareader["group2address_gid"].ToString();
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

        public void DaUpdateGroupAddressDetail(string employee_gid, MdlMstAddressDetails values)
        {
            msSQL = "select addresstype_gid, addresstype_name, addressline1, addressline2, landmark, taluka, primary_status, postal_code, city," +
                    " district, state, country, latitude, longitude, group_gid, group2address_gid " +
                    " from agr_mst_tsuprgroup2address where group2address_gid='" + values.group2address_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsaddress_typegid = objODBCDatareader["addresstype_gid"].ToString();
                lsaddress_type = objODBCDatareader["addresstype_name"].ToString();
                lsaddressline1 = objODBCDatareader["addressline1"].ToString();
                lsaddressline2 = objODBCDatareader["addressline2"].ToString();
                lslandmark = objODBCDatareader["landmark"].ToString();
                lstaluka = objODBCDatareader["taluka"].ToString();
                lsprimary_status = objODBCDatareader["primary_status"].ToString();
                lspostal_code = objODBCDatareader["postal_code"].ToString();
                lscity = objODBCDatareader["city"].ToString();
                lsdistrict = objODBCDatareader["district"].ToString();
                lsstate = objODBCDatareader["state"].ToString();
                lscountry = objODBCDatareader["country"].ToString();
                lslatitude = objODBCDatareader["latitude"].ToString();
                lslongitude = objODBCDatareader["longitude"].ToString();
                lsgroup_gid = objODBCDatareader["group_gid"].ToString();
                lsgroup2address_gid = objODBCDatareader["group2address_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprgroup2address set " +
                         " addresstype_gid='" + values.address_typegid + "'," +
                         " addresstype_name='" + values.address_type + "'," +
                         " addressline1='" + values.addressline1 + "'," +
                         " addressline2='" + values.addressline2 + "'," +
                         " landmark='" + values.landmark + "'," +
                         " taluka='" + values.taluka + "'," +
                         " primary_status='" + values.primary_status + "'," +
                         " postal_code='" + values.postal_code + "'," +
                         " city='" + values.city + "'," +
                         " district='" + values.district + "'," +
                         " state='" + values.state + "'," +
                         " country='" + values.country + "'," +
                         " latitude='" + values.latitude + "'," +
                         " longitude='" + values.longitude + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where group2address_gid='" + values.group2address_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("GAUL");

                    msSQL = " insert into agr_mst_tsuprgroup2addressupdatelog(" +
                  " group2addressupdatelog_gid," +
                  " group2address_gid," +
                  " group_gid," +
                  " addresstype_gid," +
                  " addresstype_name," +
                  " addressline1," +
                  " addressline2," +
                  " primary_status," +
                  " landmark," +
                  " postal_code," +
                  " city," +
                  " taluka," +
                  " district," +
                  " state," +
                  " country," +
                  " latitude," +
                  " longitude," +
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.group2address_gid + "'," +
                  "'" + lsaddress_typegid + "'," +
                  "'" + lsaddress_type + "'," +
                  "'" + lsaddressline1 + "'," +
                  "'" + lsaddressline2 + "'," +
                  "'" + lsprimary_status + "'," +
                  "'" + lslandmark + "'," +
                  "'" + lspostal_code + "'," +
                  "'" + lscity + "'," +
                  "'" + lstaluka + "'," +
                  "'" + lsdistrict + "'," +
                  "'" + lsstate + "'," +
                  "'" + lscountry + "'," +
                  "'" + lslatitude + "'," +
                  "'" + lslongitude + "'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Address Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteGroupAddressDetail(string group2address_gid, string employee_gid, MdlMstAddressDetails values)
        {
            msSQL = "delete from agr_mst_tsuprgroup2address where group2address_gid='" + group2address_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprgroup2addressupdatelog where group2address_gid='" + group2address_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Address Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }


        // Group Address Details

        public bool DaPostGroupBankDetail(string employee_gid, string user_gid, MdlMstBankDetails values)
        {

            msSQL = "select group2bank_gid from agr_mst_tsuprgroup2bank where ifsc_code='" + values.ifsc_code + "' and (group_gid='" + employee_gid + "' or group_gid='" + values.group_gid + "')";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Already Bank Added";
                return false;
            }
            objODBCDatareader.Close();
            msGetGid = objcmnfunctions.GetMasterGID("G2BK");
            msSQL = " insert into agr_mst_tsuprgroup2bank(" +
                    " group2bank_gid," +
                    " group_gid," +
                    " ifsc_code," +
                    " bank_accountno," +
                    " accountholder_name," +
                    " bank_name," +
                    " bank_branch," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "'" + values.ifsc_code + "'," +
                    "'" + values.bank_accountno + "'," +
                    "'" + values.accountholder_name + "'," +
                    "'" + values.bank_name + "'," +
                    "'" + values.bank_branch + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Bank Details Added Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }

        public void DaGetGroupBankList(string employee_gid, MdlMstBankDetails values)
        {
            msSQL = "  select group2bank_gid,ifsc_code,bank_accountno, accountholder_name, bank_name, bank_branch " +
                    "  from agr_mst_tsuprgroup2bank where group_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstbank_list = new List<mstbank_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstbank_list.Add(new mstbank_list
                    {
                        group2bank_gid = (dr_datarow["group2bank_gid"].ToString()),
                        ifsc_code = (dr_datarow["ifsc_code"].ToString()),
                        bank_accountno = (dr_datarow["bank_accountno"].ToString()),
                        accountholder_name = (dr_datarow["accountholder_name"].ToString()),
                        bank_name = (dr_datarow["bank_name"].ToString()),
                        bank_branch = (dr_datarow["bank_branch"].ToString()),
                    });
                }
                values.mstbank_list = getmstbank_list;
            }
            dt_datatable.Dispose();
        }

        public void DaEditGroupBankDetail(string group2bank_gid, MdlMstBankDetails values)
        {
            try
            {
                msSQL = "select ifsc_code, bank_accountno, accountholder_name, bank_name, bank_branch," +
                    " group_gid, group2bank_gid " +
                    " from agr_mst_tsuprgroup2bank where group2bank_gid='" + group2bank_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.ifsc_code = objODBCDatareader["ifsc_code"].ToString();
                    values.bank_accountno = objODBCDatareader["bank_accountno"].ToString();
                    values.accountholder_name = objODBCDatareader["accountholder_name"].ToString();
                    values.bank_name = objODBCDatareader["bank_name"].ToString();
                    values.bank_branch = objODBCDatareader["bank_branch"].ToString();
                    values.group_gid = objODBCDatareader["group_gid"].ToString();
                    values.group2bank_gid = objODBCDatareader["group2bank_gid"].ToString();
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

        public void DaUpdateGroupBankDetail(string employee_gid, MdlMstBankDetails values)
        {
            msSQL = "select ifsc_code, bank_accountno, accountholder_name, bank_name, bank_branch," +
                    " group_gid, group2bank_gid " +
                    " from agr_mst_tsuprgroup2bank where group2bank_gid='" + values.group2bank_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsifsc_code = objODBCDatareader["ifsc_code"].ToString();
                lsbank_accountno = objODBCDatareader["bank_accountno"].ToString();
                lsaccountholder_name = objODBCDatareader["accountholder_name"].ToString();
                lsbank_name = objODBCDatareader["bank_name"].ToString();
                lsbank_branch = objODBCDatareader["bank_branch"].ToString();
                lsgroup_gid = objODBCDatareader["group_gid"].ToString();
                lsgroup2bank_gid = objODBCDatareader["group2bank_gid"].ToString();
            }
            objODBCDatareader.Close();
            try
            {
                msSQL = " update agr_mst_tsuprgroup2bank set " +
                         " ifsc_code='" + values.ifsc_code + "'," +
                         " bank_accountno='" + values.bank_accountno + "'," +
                         " accountholder_name='" + values.accountholder_name + "'," +
                         " bank_name='" + values.bank_name + "'," +
                         " bank_branch='" + values.bank_branch + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where group2bank_gid='" + values.group2bank_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("GBUL");

                    msSQL = " insert into agr_mst_tsuprgroup2bankupdatelog(" +
                  " group2bankupdatelog_gid," +
                  " group2bank_gid," +
                  " group_gid," +
                  " ifsc_code," +
                  " bank_accountno," +
                  " accountholder_name," +
                  " bank_name," +
                  " bank_branch," +
                  " created_by," +
                  " created_date)" +
                  " values(" +
                  "'" + msGetGid + "'," +
                  "'" + values.group2bank_gid + "'," +
                  "'" + values.group_gid + "'," +
                  "'" + values.ifsc_code + "'," +
                  "'" + values.bank_accountno + "'," +
                  "'" + values.accountholder_name + "'," +
                  "'" + values.bank_name + "'," +
                  "'" + values.bank_branch + "'," +
                  "'" + employee_gid + "'," +
                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    values.status = true;
                    values.message = "Bank Details Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Error Occured..";
            }
        }

        public void DaDeleteGroupBankDetail(string group2bank_gid, string employee_gid, MdlMstBankDetails values)
        {
            msSQL = "delete from agr_mst_tsuprgroup2bank where group2bank_gid='" + group2bank_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                msSQL = "delete from agr_mst_tsuprgroup2bankupdatelog where group2bank_gid='" + group2bank_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.message = "Bank Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaGroupDocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string lsdocument_title = httpRequest.Form["document_title"].ToString();
            string lsgroupdocument_gid = httpRequest.Form["groupdocument_gid"].ToString();
            String path = lspath;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/GroupDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);

                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/GroupDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/GroupDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/GroupDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("G2DO");
                        msGetDocumentGid = objcmnfunctions.GetMasterGID("GPDA");

                        msSQL = "select covenant_type from ocs_mst_tgroupdocument where groupdocument_gid='" + lsgroupdocument_gid + "'";
                        string lscovenant_type = objdbconn.GetExecuteScalar(msSQL);

                        msSQL = " insert into agr_mst_tsuprgroup2document( " +
                                    " group2document_gid ," +
                                    " group_gid ," +
                                    " document_gid ," +
                                    " document_title ," +
                                    " document_name ," +
                                    " document_path," +
                                    " groupdocument_gid, " +
                                    " covenant_type," +
                                    " created_by," +
                                    " created_date" +
                                    " )values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + msGetDocumentGid + "'," +
                                    "'" + lsdocument_title + "'," +
                                    "'" + httpPostedFile.FileName + "'," +
                                    "'" + lspath + msdocument_gid + FileExtension + "'," +
                                    "'" + lsgroupdocument_gid + "'," +
                                    "'" + lscovenant_type + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaGetGroupDocumentList(string employee_gid, MdlGroupDocument values)
        {
            msSQL = " select group2document_gid,document_name,document_path,document_title from agr_mst_tsuprgroup2document " +
                                 " where group_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdocumentdtlList = new List<groupdocument_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdocumentdtlList.Add(new groupdocument_list
                    {
                        document_name = dt["document_name"].ToString(),
                        //document_path = (dt["document_path"].ToString()),
                        document_path = objcmnstorage.EncryptData((dt["document_path"].ToString())),
                        group2document_gid = dt["group2document_gid"].ToString(),
                        document_title = dt["document_title"].ToString(),
                    });
                    values.groupdocument_list = getdocumentdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGroupDocumentDelete(string group2document_gid, MdlGroupDocument values)
        {
            msSQL = "delete from agr_mst_tsuprgroup2document where group2document_gid='" + group2document_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = " select groupdocumentchecklist_gid from agr_trn_tsuprdocumentchecktls where documentuploaded_gid='" + group2document_gid + "'";
                string lsgroupdocumentchecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lsgroupdocumentchecklist_gid != "")
                {
                    msSQL = " select count(*) as documentcount from agr_trn_tsuprdocumentchecktls " +
                            " where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tsuprgroupdocumentchecklist where groupdocumentchecklist_gid='" + lsgroupdocumentchecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = " select groupcovdocumentchecklist_gid from agr_trn_tsuprcovanantdocumentcheckdtls where documentuploaded_gid='" + group2document_gid + "'";
                string lschecklist_gid = objdbconn.GetExecuteScalar(msSQL);

                if (lschecklist_gid != "")
                {
                    msSQL = " select count(*) as documentcount from agr_trn_tsuprcovanantdocumentcheckdtls " +
                      " where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                    string lsdocumentcount = objdbconn.GetExecuteScalar(msSQL);
                    if (lsdocumentcount == "1")
                    {
                        msSQL = "delete from agr_trn_tsuprgroupcovenantdocumentchecklist where groupcovdocumentchecklist_gid='" + lschecklist_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                msSQL = "delete from agr_trn_tsuprcovanantdocumentcheckdtls where documentuploaded_gid='" + group2document_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from agr_trn_tsuprdocumentchecktls where documentuploaded_gid='" + group2document_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            if (mnResult != 0)
            {

                values.message = "Document Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }


        public void DaGroupSave(string employee_gid, MdlMstGroup values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("GRUP");

            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " insert into agr_mst_tsuprgroup(" +
                   " group_gid," +
                   " application_gid," +
                   " group_name," +
                   " date_of_formation," +
                   " group_type," +
                   " groupmember_count," +
                   " groupurn_status," +
                   " group_urn," +
                   " group_status," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + lsapplication_gid + "',";


            if (values.group_name == "" || values.group_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.group_name.Replace("'", "") + "',";
            }

            if ((values.date_of_formation == null) || (values.date_of_formation == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.date_of_formation).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL += "'" + values.group_type + "'," +
                     "'" + values.groupmember_count + "'," +
                     "'" + values.groupurn_status + "'," +
                     "'" + values.group_urn + "'," +
                     "'Incomplete'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {

                //Updates

                msSQL = "update agr_mst_tsuprgroup2address set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprgroup2bank set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                msSQL = "select group2document_gid, groupdocument_gid from agr_mst_tsuprgroup2document where group_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lscovenant_type = "", lsdocumenttype_gid = "", lsdocumenttype_name = "", lscompanydocument_name = "";

                    string msGetdefDocchecklistGID = objcmnfunctions.GetMasterGID("DOCG");
                    msSQL = " select groupdocument_gid,documenttypes_gid,documenttype_name,groupdocument_name,covenant_type " +
                           " from ocs_mst_tgroupdocument where groupdocument_gid='" + dt["groupdocument_gid"].ToString() + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                        lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                        lscompanydocument_name = objODBCDatareader["groupdocument_name"].ToString();
                        lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = " insert into agr_trn_tsuprdocumentchecktls(" +
                            " documentcheckdtl_gid," +
                            " application_gid," +
                            " credit_gid, " +
                            " groupdocument_gid, " +
                            " documentuploaded_gid, " +
                            " documenttype_gid," +
                        " documenttype_code," +
                        " documenttype_name," +
                        " covenant_type, " +
                        " tagged_by, " +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetdefDocchecklistGID + "'," +
                        "'" + lsapplication_gid + "'," +
                        "'" + msGetGid + "'," +
                        "'" + dt["groupdocument_gid"].ToString() + "'," +
                        "'" + dt["group2document_gid"].ToString() + "'," +
                        "'" + lsdocumenttype_gid + "'," +
                        "'" + lsdocumenttype_name + "'," +
                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                        "'" + lscovenant_type + "'," +
                        "'N'," +
                        "current_timestamp," +
                        "'" + employee_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (lscovenant_type == "Y")
                    {
                        string msGetDocchecklistGID = objcmnfunctions.GetMasterGID("CDCL");
                        msSQL = " insert into agr_trn_tsuprcovanantdocumentcheckdtls(" +
                       " covenantdocumentcheckdtl_gid," +
                       " application_gid," +
                       " credit_gid," +
                       " groupdocument_gid," +
                       " documentuploaded_gid, " +
                       " documenttype_gid," +
                       " documenttype_code," +
                       " documenttype_name," +
                       " covenant_type, " +
                       " tagged_by, " +
                       " created_date," +
                       " created_by)" +
                       " VALUES(" +
                       "'" + msGetDocchecklistGID + "'," +
                       "'" + lsapplication_gid + "'," +
                       "'" + msGetGid + "'," +
                       "'" + dt["groupdocument_gid"].ToString() + "'," +
                       "'" + dt["group2document_gid"].ToString() + "'," +
                       "'" + lsdocumenttype_gid + "'," +
                       "'" + lsdocumenttype_name + "'," +
                       "'" + lscompanydocument_name.Replace("'", "") + "'," +
                       "'" + lscovenant_type + "'," +
                       "'N'," +
                       "current_timestamp," +
                       "'" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                DaAgrMstSuprScannedDocument objvalues = new DaAgrMstSuprScannedDocument();
                objvalues.DaGroupDocChecklistinfo(lsapplication_gid, msGetGid, employee_gid);

                msSQL = "update agr_mst_tsuprgroup2document set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprkycifscauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprkycbankaccverification set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Group Details Saved Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }


        public void DaGroupSubmit(string employee_gid, MdlMstGroup values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("GRUP");

            msSQL = "select group_gid from agr_mst_tsuprgroup2address where group_gid='" + employee_gid + "' and primary_status='Yes'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Primary Address ";
                return;
            }
            objODBCDatareader.Close();
            msSQL = "select group_gid from agr_mst_tsuprgroup2bank where group_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast one Bank detail ";
                return;
            }
            objODBCDatareader.Close();
            msSQL = "select group_gid from agr_mst_tsuprgroup2document where group_gid='" + employee_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == false)
            {
                values.status = false;
                values.message = "Add Atleast one Document detail ";
                return;
            }
            objODBCDatareader.Close();
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " insert into agr_mst_tsuprgroup(" +
                   " group_gid," +
                   " application_gid," +
                   " group_name," +
                   " date_of_formation," +
                   " group_type," +
                   " groupmember_count," +
                   " groupurn_status," +
                   " group_urn," +
                   " group_status," +
                   " created_by," +
                   " created_date)" +
                   " values(" +
                   "'" + msGetGid + "'," +
                   "'" + lsapplication_gid + "',";

            if (values.group_name == "" || values.group_name == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.group_name.Replace("'", "") + "',";
            }

            if ((values.date_of_formation == null) || (values.date_of_formation == ""))
            {
                msSQL += "null,";
            }
            else
            {
                msSQL += "'" + Convert.ToDateTime(values.date_of_formation).ToString("yyyy-MM-dd HH:mm:ss") + "',";
            }

            msSQL += "'" + values.group_type + "'," +
                     "'" + values.groupmember_count + "'," +
                     "'" + values.groupurn_status + "'," +
                     "'" + values.group_urn + "'," +
                     "'Completed'," +
                     "'" + employee_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            //}

            //objODBCDatareader.Close();

            if (mnResult != 0)
            {

                //Updates

                msSQL = "update agr_mst_tsuprgroup2address set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprgroup2bank set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "select groupdocument_gid, group2document_gid from agr_mst_tsuprgroup2document where group_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string lscovenant_type = "", lsdocumenttype_gid = "", lsdocumenttype_name = "", lscompanydocument_name = "";

                    string msGetdefDocchecklistGID = objcmnfunctions.GetMasterGID("DOCG");
                    msSQL = " select groupdocument_gid,documenttypes_gid,documenttype_name,groupdocument_name,covenant_type " +
                           " from ocs_mst_tgroupdocument where groupdocument_gid='" + dt["groupdocument_gid"].ToString() + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        lsdocumenttype_gid = objODBCDatareader["documenttypes_gid"].ToString();
                        lsdocumenttype_name = objODBCDatareader["documenttype_name"].ToString();
                        lscompanydocument_name = objODBCDatareader["groupdocument_name"].ToString();
                        lscovenant_type = objODBCDatareader["covenant_type"].ToString();
                    }
                    objODBCDatareader.Close();
                    msSQL = " insert into agr_trn_tsuprdocumentchecktls(" +
                            " documentcheckdtl_gid," +
                            " application_gid," +
                            " credit_gid, " +
                            " groupdocument_gid, " +
                            " documentuploaded_gid, " +
                            " documenttype_gid," +
                        " documenttype_code," +
                        " documenttype_name," +
                        " covenant_type, " +
                        " tagged_by, " +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetdefDocchecklistGID + "'," +
                        "'" + lsapplication_gid + "'," +
                        "'" + msGetGid + "'," +
                        "'" + dt["groupdocument_gid"].ToString() + "'," +
                        "'" + dt["group2document_gid"].ToString() + "'," +
                        "'" + lsdocumenttype_gid + "'," +
                        "'" + lsdocumenttype_name + "'," +
                        "'" + lscompanydocument_name.Replace("'", "") + "'," +
                        "'" + lscovenant_type + "'," +
                        "'N'," +
                        "current_timestamp," +
                        "'" + employee_gid + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (lscovenant_type == "Y")
                    {
                        string msGetDocchecklistGID = objcmnfunctions.GetMasterGID("CDCL");
                        msSQL = " insert into agr_trn_tsuprcovanantdocumentcheckdtls(" +
                       " covenantdocumentcheckdtl_gid," +
                       " application_gid," +
                       " credit_gid," +
                       " groupdocument_gid," +
                       " documentuploaded_gid, " +
                       " documenttype_gid," +
                       " documenttype_code," +
                       " documenttype_name," +
                       " covenant_type, " +
                       " tagged_by, " +
                       " created_date," +
                       " created_by)" +
                       " VALUES(" +
                       "'" + msGetDocchecklistGID + "'," +
                       "'" + lsapplication_gid + "'," +
                       "'" + msGetGid + "'," +
                       "'" + dt["groupdocument_gid"].ToString() + "'," +
                       "'" + dt["group2document_gid"].ToString() + "'," +
                       "'" + lsdocumenttype_gid + "'," +
                       "'" + lsdocumenttype_name + "'," +
                       "'" + lscompanydocument_name.Replace("'", "") + "'," +
                       "'" + lscovenant_type + "'," +
                       "'N'," +
                       "current_timestamp," +
                       "'" + employee_gid + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                dt_datatable.Dispose();

                DaAgrMstSuprScannedDocument objvalues = new DaAgrMstSuprScannedDocument();
                objvalues.DaGroupDocChecklistinfo(lsapplication_gid, msGetGid, employee_gid);

                msSQL = "update agr_mst_tsuprgroup2document set group_gid ='" + msGetGid + "' where group_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprkycifscauthentication set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprkycbankaccverification set function_gid ='" + lsapplication_gid + "' where function_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Group Details Submitted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaGetGroupSummary(string employee_gid, MdlMstGroup values)
        {
            msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
            lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsapplication_gid != "")
            {
                msSQL = " select group_gid,group_name,date_of_formation,group_status," +
                   " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                   " from agr_mst_tsuprgroup a " +
                   " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                   " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                   " where a.application_gid='" + lsapplication_gid + "' order by group_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getgroupList = new List<group_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getgroupList.Add(new group_list
                        {
                            group_gid = dt["group_gid"].ToString(),
                            group_name = dt["group_name"].ToString(),
                            date_of_formation = dt["date_of_formation"].ToString(),
                            group_status = dt["group_status"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            created_by = dt["created_by"].ToString()
                        });

                    }
                }
                values.group_list = getgroupList;
                dt_datatable.Dispose();
            } 
        }

        public void DaGetGroupTempClear(string employee_gid, result values)
        {
            msSQL = "delete from agr_mst_tsuprgroup2address where group_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprgroup2bank where group_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprgroup2document where group_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaGetGroupList(string application_gid, MdlDropDown values)
        {
            msSQL = " SELECT group_name, group_gid from agr_mst_tsuprgroup where application_gid='" + application_gid + "' order by group_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgroup = new List<grouplist>();
            if (dt_datatable.Rows.Count != 0)
            {
                getgroup.Add(new grouplist
                {
                    group_name = "NA",
                    group_gid = "NA",
                });
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getgroup.Add(new grouplist
                    {
                        group_name = (dr_datarow["group_name"].ToString()),
                        group_gid = (dr_datarow["group_gid"].ToString()),
                    });
                }
                values.grouplist = getgroup;
            }

            if (dt_datatable.Rows.Count == 0)
            {
                getgroup.Add(new grouplist
                {
                    group_name = "NA",
                    group_gid = "NA",
                });
                values.grouplist = getgroup;
            }
            dt_datatable.Dispose();

            values.status = true;
        }

        public void DaGetContactGroupList(string contact_gid, MdlDropDown values)
        {
            msSQL = " SELECT application_gid from agr_mst_tsuprcontact where contact_gid='" + contact_gid + "' order by contact_gid desc ";
            string lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT group_name, group_gid from agr_mst_tsuprgroup where application_gid='" + lsapplication_gid + "' order by group_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getgroup = new List<grouplist>();
            if (dt_datatable.Rows.Count != 0)
            {
                getgroup.Add(new grouplist
                {
                    group_name = "NA",
                    group_gid = "NA",
                });
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getgroup.Add(new grouplist
                    {
                        group_name = (dr_datarow["group_name"].ToString()),
                        group_gid = (dr_datarow["group_gid"].ToString()),
                    });
                }
                values.grouplist = getgroup;
            }

            if (dt_datatable.Rows.Count == 0)
            {
                getgroup.Add(new grouplist
                {
                    group_name = "NA",
                    group_gid = "NA",
                });
                values.grouplist = getgroup;
            }
            dt_datatable.Dispose();

            values.status = true;
        }

        public void DaGetGSTState(string gst_code, MdlMstGST objMdlMstGST)
        {
            try
            {
                msSQL = "select gst_state from agr_mst_tgstcode2state where " +
                        " gst_code='" + gst_code + "'";
                objMdlMstGST.gst_state = objdbconn.GetExecuteScalar(msSQL);

                objMdlMstGST.status = true;
            }
            catch
            {
                objMdlMstGST.status = false;
            }
        }

        public void DaFutureDateCheck(string date, result values)
        {
            try
            {
                DateTime documentdate = DateTime.Parse(Convert.ToDateTime(date).ToShortDateString());
                DateTime nowdate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                if (documentdate > nowdate)
                {
                    values.status = false;
                    values.message = "Future Date is Not Allowed...";
                }
                else
                {
                    values.status = true;
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Kindly select valid Date...";
            }

        }

        public void DaGetCompanyList(string application_gid, MdlDropDown values)
        {
            msSQL = " SELECT company_name, institution_gid from agr_mst_tsuprinstitution where application_gid='" + application_gid + "' order by institution_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinstitution = new List<institutionlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                getinstitution.Add(new institutionlist
                {
                    institution_name = "NA",
                    institution_gid = "NA",
                });
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getinstitution.Add(new institutionlist
                    {
                        institution_name = (dr_datarow["company_name"].ToString()),
                        institution_gid = (dr_datarow["institution_gid"].ToString()),
                    });
                }
                values.institutionlist = getinstitution;
            }

            if (dt_datatable.Rows.Count == 0)
            {
                getinstitution.Add(new institutionlist
                {
                    institution_name = "NA",
                    institution_gid = "NA",
                });
                values.institutionlist = getinstitution;
            }
            dt_datatable.Dispose();

            values.status = true;
        }

        public void DaGetContactCompanyList(string contact_gid, MdlDropDown values)
        {
            msSQL = " SELECT application_gid from agr_mst_tsuprcontact where contact_gid='" + contact_gid + "' order by contact_gid desc ";
            string lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " SELECT company_name, institution_gid from agr_mst_tsuprinstitution where application_gid='" + lsapplication_gid + "' order by institution_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinstitution = new List<institutionlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                getinstitution.Add(new institutionlist
                {
                    institution_name = "NA",
                    institution_gid = "NA",
                });
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getinstitution.Add(new institutionlist
                    {
                        institution_name = (dr_datarow["company_name"].ToString()),
                        institution_gid = (dr_datarow["institution_gid"].ToString()),
                    });
                }
                values.institutionlist = getinstitution;
            }
            dt_datatable.Dispose();
            if (dt_datatable.Rows.Count == 0)
            {
                getinstitution.Add(new institutionlist
                {
                    institution_name = "NA",
                    institution_gid = "NA",
                });
                values.institutionlist = getinstitution;
            }
            dt_datatable.Dispose();

            values.status = true;
        }

        public void DaDeleteGSTInstitution(string employee_gid, string institution_gid, MdlMstGST values)
        {
            msSQL = "select institution2branch_gid from agr_mst_tsuprinstitution2branch where institution_gid='" + employee_gid + "' or institution_gid='" + institution_gid + "'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            string institution2branch_gid;
            foreach (DataRow dr_datarow in dt_datatable.Rows)
            {
                institution2branch_gid = (dr_datarow["institution2branch_gid"].ToString());
                msSQL = "delete from agr_mst_tsuprinstitution2branch where institution2branch_gid='" + institution2branch_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            dt_datatable.Dispose();

            if (mnResult != 0)
            {
                values.message = "GST Details Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured While Deleting The Gst Details";
                values.status = false;

            }
        }


        public void DaImportExcelIndividual(HttpRequest httpRequest, string employee_gid, result objResult)
        {
            try
            {
                int insertCount = 0;
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;
                string application_gid = httpRequest.Form["application_gid"];
                string project_flag = httpRequest.Form["project_flag"].ToString();

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/SamAgro/IndividualDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

                if ((!System.IO.Directory.Exists(lsfilePath)))
                    System.IO.Directory.CreateDirectory(lsfilePath);


                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    httpPostedFile = httpFileCollection[i];
                }
                string FileExtension = httpPostedFile.FileName;

                string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                string lsfile_gid = msdocument_gid;
                FileExtension = Path.GetExtension(FileExtension).ToLower();
                lsfile_gid = lsfile_gid + FileExtension;

                Stream ls_readStream;
                ls_readStream = httpPostedFile.InputStream;
                MemoryStream ms = new MemoryStream();
                ls_readStream.CopyTo(ms);

                // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objResult.message = "File format is not supported";
                            objResult.status = false;
                            return;
                        }

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);

                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                    columnCount = worksheet.Dimension.End.Column;
                    endRange = worksheet.Dimension.End.Address;
                }


                file.Close();
                ms.Close();

                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";



                excelRange = "A2:" + endRange + rowCount.ToString();
                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);
                try
                {
                    dt = dt.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                }
                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = "No Records Found";
                    return;
                }

                Nullable<DateTime> ldpepverified_date, ldfather_dob, ldmother_dob, ldspouse_dob;

                foreach (DataRow row in dt.Rows)
                {
                    contactimportlog_message = "";

                    lsapplication_no = row["* Application No"].ToString();
                    if (lsapplication_no == "")
                    {

                    }
                    else
                    {
                        lsurn_status = row["* Having URN (Yes/No)"].ToString();
                        lsurn = row["If Yes, URN"].ToString();

                        lsgroup_name = row["If Group Yes, Group Name *"].ToString();
                        msSQL = "select group_gid from agr_mst_tsuprgroup where group_name='" + lsgroup_name + "'";
                        lsgroup_gid = objdbconn.GetExecuteScalar(msSQL);
                        if (lsgroup_name == "NA" && lsgroup_gid == "")
                            lsgroup_gid = "NA";

                        lsinsitution_name = row["If company/Institution yes, Company Name *"].ToString();
                        msSQL = "select institution_gid from agr_mst_tsuprinstitution where company_name='" + lsinsitution_name + "'";
                        lsinstitution_gid = objdbconn.GetExecuteScalar(msSQL);
                        if (lsinsitution_name == "NA" && lsinstitution_gid == "")
                            lsinstitution_gid = "NA";

                        lspan_status = row["* PAN Status (Yes / No)"].ToString();
                        lspan_no = row["PAN Value (If PAN Status is Yes, PAN Value is mandatory)"].ToString();
                        lsaadhar_no = row["* Aadhar Number"].ToString();

                        lsfirst_name = row["* First Name"].ToString();
                        lsmiddle_name = row["Middle Name"].ToString();
                        lslast_name = row["* Last Name"].ToString();

                        lsindividual_dob = row["Date of Birth (DD-MM-YYYY)"].ToString();

                        if (lsindividual_dob.Length > 10)
                        {
                            lsindividual_dob = dateFormatStandardizer(lsindividual_dob);
                        }

                        lsgender_name = row["* Gender"].ToString();
                        msSQL = "select gender_gid from ocs_mst_tgender where gender_name='" + row["* Gender"].ToString() + "'";
                        lsgender_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsdesignation_type = row["Designation"].ToString();
                        msSQL = "select designation_gid from ocs_mst_tdesignation where designation_type='" + row["Designation"].ToString() + "'";
                        lsdesignation_gid = objdbconn.GetExecuteScalar(msSQL);

                        lspep_status = row["* Politically Exposed person (PEP)(Yes/No)"].ToString();

                        lspepverified_date = row["* PEP Verified On (DD-MM-YYYY)"].ToString();
                        if (lspepverified_date.Length > 10)
                        {
                            lspepverified_date = dateFormatStandardizer(lspepverified_date);
                        }
                        lspepverified_date = lspepverified_date.Replace('-', '/');
                        ldpepverified_date = DateTime.ParseExact(lspepverified_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        lsuser_type = row["* Stakeholder Type"].ToString();
                        msSQL = "select usertype_gid from ocs_mst_tusertype where user_type='" + row["* Stakeholder Type"].ToString() + "'";
                        lsusertype_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsmaritalstatus_name = row["* Marital Status"].ToString();
                        msSQL = "select maritalstatus_gid from agr_mst_tmaritalstatus where maritalstatus_name='" + row["* Marital Status"].ToString() + "'";
                        lsmaritalstatus_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsfather_firstname = row["* Father's First Name"].ToString();
                        lsfather_middlename = row["Father's Middle Name"].ToString();
                        lsfather_lastname = row["* Father's Last Name"].ToString();
                        lsfathernominee_status = row["Father Nominee(Yes/No)"].ToString();

                        lsfather_dob = row["Father's Date of Birth(DD-MM-YYYY)"].ToString();
                        if (lsfather_dob.Length > 10)
                        {
                            lsfather_dob = dateFormatStandardizer(lsfather_dob);
                        }
                        if (lsfather_dob.Length > 0)
                        {
                            lsfather_dob = lsfather_dob.Replace('-', '/');
                            ldfather_dob = DateTime.ParseExact(lsfather_dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            ldfather_dob = null;
                        }


                        lsmother_firstname = row["Mother's First Name"].ToString();
                        lsmother_middlename = row["Mother's Middle Name"].ToString();
                        lsmother_lastname = row["Mother's Last Name"].ToString();
                        lsmothernominee_status = row["Mother Nominee(Yes/No)"].ToString();


                        lsmother_dob = row["Mother's Date of Birth(DD-MM-YYYY)"].ToString();
                        if (lsmother_dob.Length > 10)
                        {
                            lsmother_dob = dateFormatStandardizer(lsmother_dob);
                        }
                        if (lsmother_dob.Length > 0)
                        {
                            lsmother_dob = lsmother_dob.Replace('-', '/');
                            ldmother_dob = DateTime.ParseExact(lsmother_dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            ldmother_dob = null;
                        }

                        lsspouse_firstname = row["Spouse First Name"].ToString();
                        lsspouse_middlename = row["Spouse Middle Name"].ToString();
                        lsspouse_lastname = row["Spouse Last Name"].ToString();
                        lsspousenominee_status = row["Spouse Nominee(Yes/No)"].ToString();

                        lsspouse_dob = row["Spouse's Date of Birth(DD-MM-YYYY)"].ToString();
                        if (lsspouse_dob.Length > 10)
                        {
                            lsspouse_dob = dateFormatStandardizer(lsspouse_dob);
                        }
                        if (lsspouse_dob.Length > 0)
                        {
                            lsspouse_dob = lsspouse_dob.Replace('-', '/');
                            ldspouse_dob = DateTime.ParseExact(lsspouse_dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            ldspouse_dob = null;
                        }

                        lseducationalqualification_name = row["* Educational Qualification"].ToString();
                        msSQL = "select educationalqualification_gid from ocs_mst_teducationalqualification where educationalqualification_name='" + row["* Educational Qualification"].ToString() + "'";
                        lseducationalqualification_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsmain_occupation = row["* Main Occupation"].ToString();
                        lsannual_income = row["* Annual Income"].ToString();
                        lsmonthly_income = row["Monthly Income"].ToString();

                        lsincometype_name = row["Income Type"].ToString();
                        msSQL = "select incometype_gid from ocs_mst_tincometype where incometype_name='" + row["Income Type"].ToString() + "'";
                        lsincometype_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsyearscurrentresidece = row["* Years in Current Residence"].ToString();
                        lsdistancebranch = row["* Distance from Branch/Regional Office (in Kms)"].ToString();

                        lsmobile_no = row["* Mobile Number"].ToString();
                        lswhatsapp_no = row["* Whatsapp  (Yes/No)"].ToString();

                        lsemail_address = row["* Email Address"].ToString();

                        lsaddresstype_name = row["* Address Type"].ToString();
                        msSQL = "select address_gid from ocs_mst_taddresstype where address_type='" + row["* Address Type"].ToString() + "'";
                        lsaddresstype_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsaddressline1 = row["* AddressLine1"].ToString();
                        lsaddressline2 = row["AddressLine2"].ToString();
                        lspostal_code = row["* Postal Code"].ToString();

                        msSQL = " select city,taluka,district,state from ocs_mst_tpostalcode where " +
                           "postalcode_value='" + lspostal_code + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lscity = objODBCDatareader["city"].ToString();
                            lstaluka = objODBCDatareader["taluka"].ToString();
                            lsdistrict = objODBCDatareader["district"].ToString();
                            lsstate = objODBCDatareader["state"].ToString();
                        }
                        objODBCDatareader.Close();
                        lscountry = row["* Country"].ToString();

                        msSQL = "select stakeholder_type from agr_mst_tsuprcontact where application_gid='" + application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
                        string lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

                        if (lsstakeholder_type == lsuser_type)
                        {
                            contactimportlog_message = "Applicant/Borrower Information Already Added in Individual";

                        }

                        msSQL = "select stakeholder_type from agr_mst_tsuprinstitution where application_gid='" + application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
                        lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

                        if (lsstakeholder_type == lsuser_type)
                        {
                            contactimportlog_message = "Applicant/Borrower Information Already Added in Insitution";
                        }

                        msSQL = "select stakeholder_type from agr_mst_tsuprinstitution where application_gid='" + application_gid + "' and stakeholder_type in ('Borrower','Applicant')";

                        if (contactimportlog_message == "")
                        {
                            if (lspan_status == "Yes" && lspan_no == "")
                            {
                                lspanstatusvalue = "empty";
                            }

                            if ((lsurn_status == "") || (lsgroup_name == "") || (lsinsitution_name == "") || (lspan_status == "") || (lsaadhar_no == "") || (lsfirst_name == "")
                            || (lslast_name == "") || (lsgender_name == "") || (lspep_status == "") || (lspepverified_date == "") || (lsuser_type == "") || (lsmaritalstatus_name == "")
                            || (lsfather_firstname == "") || (lsfather_lastname == "") || (lseducationalqualification_name == "") || (lsmain_occupation == "") || (lsannual_income == "")
                            || (lsmobile_no == "") || (lswhatsapp_no == "") || (lsemail_address == "") || (lsaddresstype_name == "") || (lsaddressline1 == "") || (lsaddressline1 == "")
                            || (lspostal_code == "") || (lscountry == "") || (lspanstatusvalue == "empty"))
                            {
                                contactimportlog_message = "Mandatory fields are empty";
                            }

                            if (contactimportlog_message == "")
                            {
                                msSQL = "select first_name, last_name, pan_no from agr_mst_tsuprcontact where application_gid='" + application_gid + "'";
                                dt_datatable = objdbconn.GetDataTable(msSQL);
                                if (dt_datatable.Rows.Count != 0)
                                {
                                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                                    {
                                        if ((lsfirst_name == dr_datarow["first_name"].ToString()) && (lslast_name == dr_datarow["last_name"].ToString()) && (lspan_no == dr_datarow["pan_no"].ToString()))
                                        {
                                            contactimportlog_message = "Record has many duplicate values";
                                            break;
                                        }
                                    }
                                }
                                dt_datatable.Dispose();

                            }

                        }


                        if (contactimportlog_message != "")
                        {

                            msGetGid = objcmnfunctions.GetMasterGID("CTIL");

                            msSQL = " insert into agr_trn_tsuprcontactimportlog(" +
                    " contactimportlog_gid," +
                    " application_gid," +
                    " application_no," +

                    " urn_status," +
                    " urn," +
                    " group_name," +
                    " institution_name," +
                    " pan_status," +
                    " pan_no," +
                    " aadhar_no," +
                    " first_name," +
                    " middle_name," +
                    " last_name," +
                    " individual_dob," +

                    " gender_name," +
                    " designation_name," +
                    " pep_status," +
                    " pepverified_date," +

                    " stakeholder_type," +
                    " maritalstatus_name," +

                    " father_firstname," +
                    " father_middlename," +
                    " father_lastname," +
                    " fathernominee_status," +
                    " father_dob," +

                    " mother_firstname," +
                    " mother_middlename," +
                    " mother_lastname," +
                    " mothernominee_status," +
                    " mother_dob," +

                    " spouse_firstname," +
                    " spouse_middlename," +
                    " spouse_lastname," +
                    " spousenominee_status," +
                    " spouse_dob," +

                    " educationalqualification_name," +
                    " main_occupation," +
                    " annual_income," +
                    " monthly_income," +
                    " incometype_name," +

                    " mobile_no," +
                    " whatsapp_no," +
                    " email_address," +
                    " addresstype_name," +
                    " addressline1," +
                    " addressline2," +
                    " postal_code," +
                    " country," +

                    " contactimportlog_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + application_gid + "'," +
                    "'" + lsapplication_no + "'," +

                     "'" + lsurn_status + "'," +
                     "'" + lsurn + "',";
                            if (lsgroup_name == "" || lsgroup_name == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsgroup_name.Replace("'", "") + "',";
                            }

                            if (lsinsitution_name == "" || lsinsitution_name == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsinsitution_name.Replace("'", "") + "',";
                            }

                            msSQL += "'" + lspan_no + "'," +
                                     "'" + lspan_status + "'," +
                                     "'" + lsaadhar_no + "'," +
                                     "'" + lsfirst_name + "'," +
                                      "'" + lsmiddle_name + "'," +
                                      "'" + lslast_name + "'," +
                                      "'" + lsindividual_dob + "'," +

                                      "'" + lsgender_name + "'," +
                                      "'" + lsdesignation_type + "'," +
                                      "'" + lspep_status + "'," +
                                      "'" + Convert.ToDateTime(ldpepverified_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +

                                      "'" + lsuser_type + "'," +
                                      "'" + lsmaritalstatus_name + "'," +

                                      "'" + lsfather_firstname + "'," +
                                      "'" + lsfather_middlename + "'," +
                                      "'" + lsfather_lastname + "'," +
                                      "'" + lsfathernominee_status + "',";

                            if ((ldfather_dob == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldfather_dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }



                            msSQL += "'" + lsmother_firstname + "'," +
                                      "'" + lsmother_middlename + "'," +
                                      "'" + lsmother_lastname + "'," +
                                      "'" + lsmothernominee_status + "',";

                            if ((ldmother_dob == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldmother_dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }

                            msSQL += "'" + lsspouse_firstname + "'," +
                                      "'" + lsspouse_middlename + "'," +
                                      "'" + lsspouse_lastname + "'," +
                                      "'" + lsspousenominee_status + "',";

                            if ((ldspouse_dob == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldspouse_dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }

                            msSQL += "'" + lseducationalqualification_name + "'," +
                                     "'" + lsmain_occupation + "'," +
                                     "'" + lsannual_income + "'," +
                                     "'" + lsmonthly_income + "'," +
                                     "'" + lsincometype_name + "'," +

                                     "'" + lsmobile_no + "'," +
                                     "'" + lswhatsapp_no + "'," +
                                     "'" + lsemail_address + "'," +
                                     "'" + lsaddresstype_name + "',";
                            if (lsaddressline1 == "" || lsaddressline1 == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsaddressline1.Replace("'", "") + "',";
                            }
                            if (lsaddressline2 == "" || lsaddressline2 == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsaddressline2.Replace("'", "") + "',";
                            }

                            msSQL += "'" + lspostal_code + "'," +
                                   "'" + lscountry + "'," +

                                   "'" + contactimportlog_message + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            logCount++;
                        }
                        else
                        {
                            if (lspan_status == "Yes")
                            {
                                lspan_status = "Customer Submitting PAN";
                            }
                            else if (lspan_status == "No")
                            {
                                lspan_status = "Customer Submitting Form 60";
                            }
                            else
                            {

                            }
                            msGetGid = objcmnfunctions.GetMasterGID("CTCT");

                            msSQL = " insert into agr_mst_tsuprcontact(" +
                           " contact_gid," +
                           " application_gid," +
                           " application_no," +

                           " urn_status," +
                           " urn," +
                           " group_gid," +
                           " group_name," +
                           " institution_gid," +
                           " institution_name," +
                           " pan_status," +
                           " pan_no," +
                           " aadhar_no," +
                           " first_name," +
                           " middle_name," +
                           " last_name," +
                           " individual_dob," +

                           " gender_gid," +
                           " gender_name," +
                           " designation_gid," +
                           " designation_name," +
                           " pep_status," +
                           " pepverified_date," +

                           " stakeholdertype_gid," +
                           " stakeholder_type," +
                           " maritalstatus_gid," +
                           " maritalstatus_name," +

                           " father_firstname," +
                           " father_middlename," +
                           " father_lastname," +
                           " fathernominee_status," +
                           " father_dob," +

                           " mother_firstname," +
                           " mother_middlename," +
                           " mother_lastname," +
                           " mothernominee_status," +
                           " mother_dob," +

                           " spouse_firstname," +
                           " spouse_middlename," +
                           " spouse_lastname," +
                           " spousenominee_status," +
                           " spouse_dob," +


                           " educationalqualification_gid," +
                           " educationalqualification_name," +
                           " main_occupation," +
                           " annual_income," +
                           " monthly_income," +
                           " incometype_gid," +
                           " incometype_name," +

                           " currentresidence_years," +
                           " branch_distance," +
                           " contact_status," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGid + "'," +
                           "'" + application_gid + "'," +
                           "'" + lsapplication_no + "'," +

                            "'" + lsurn_status + "'," +
                            "'" + lsurn + "'," +
                            "'" + lsgroup_gid + "'," +
                            "'" + lsgroup_name.Replace("'", "") + "'," +
                            "'" + lsinstitution_gid + "'," +
                            "'" + lsinsitution_name.Replace("'", "") + "'," +
                           "'" + lspan_status + "'," +
                           "'" + lspan_no + "'," +
                           "'" + lsaadhar_no + "'," +
                           "'" + lsfirst_name + "'," +
                            "'" + lsmiddle_name + "'," +
                            "'" + lslast_name + "'," +
                            "'" + lsindividual_dob + "'," +

                            "'" + lsgender_gid + "'," +
                            "'" + lsgender_name + "'," +
                            "'" + lsdesignation_gid + "'," +
                            "'" + lsdesignation_type + "'," +
                            "'" + lspep_status + "'," +
                            "'" + Convert.ToDateTime(ldpepverified_date).ToString("yyyy-MM-dd HH:mm:ss") + "'," +


                            "'" + lsusertype_gid + "'," +
                            "'" + lsuser_type + "'," +
                            "'" + lsmaritalstatus_gid + "'," +
                            "'" + lsmaritalstatus_name + "'," +

                            "'" + lsfather_firstname + "'," +
                            "'" + lsfather_middlename + "'," +
                            "'" + lsfather_lastname + "'," +
                            "'" + lsfathernominee_status + "',";

                            if ((ldfather_dob == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldfather_dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }



                            msSQL += "'" + lsmother_firstname + "'," +
                                      "'" + lsmother_middlename + "'," +
                                      "'" + lsmother_lastname + "'," +
                                      "'" + lsmothernominee_status + "',";

                            if ((ldmother_dob == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldmother_dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }

                            msSQL += "'" + lsspouse_firstname + "'," +
                                      "'" + lsspouse_middlename + "'," +
                                      "'" + lsspouse_lastname + "'," +
                                      "'" + lsspousenominee_status + "',";

                            if ((ldspouse_dob == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldspouse_dob).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }

                            msSQL += "'" + lseducationalqualification_gid + "'," +
                                  "'" + lseducationalqualification_name + "'," +
                                  "'" + lsmain_occupation + "'," +
                                  "'" + lsannual_income + "'," +
                                  "'" + lsmonthly_income + "'," +
                                  "'" + lsincometype_gid + "'," +
                                  "'" + lsincometype_name + "'," +

                                  "'" + lsyearscurrentresidece + "'," +
                                  "'" + lsdistancebranch + "'," +
                                  "'Incomplete'," +
                                  "'" + employee_gid + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msGetGidMobile = objcmnfunctions.GetMasterGID("C2MN");



                            msSQL = " insert into agr_mst_tsuprcontact2mobileno(" +
                           " contact2mobileno_gid," +
                           " contact_gid," +
                           " mobile_no," +
                           " primary_status," +
                           " whatsapp_no," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGidMobile + "'," +
                           "'" + msGetGid + "'," +
                           "'" + lsmobile_no + "'," +
                           "'" + "Yes" + "'," +
                           "'" + lswhatsapp_no + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResultMobile = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msGetGidEmail = objcmnfunctions.GetMasterGID("C2EA");



                            msSQL = " insert into agr_mst_tsuprcontact2email(" +
                                    " contact2email_gid," +
                                    " contact_gid," +
                                    " email_address," +
                                    " primary_status," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGidEmail + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + lsemail_address + "'," +
                                    "'" + "Yes" + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResultEmail = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msGetGidAddress = objcmnfunctions.GetMasterGID("C2AD");



                            msSQL = " insert into agr_mst_tsuprcontact2address(" +
                                    " contact2address_gid," +
                                    " contact_gid," +
                                    " addresstype_gid," +
                                    " addresstype_name," +
                                    " primary_status," +
                                    " addressline1," +
                                    " addressline2," +
                                    " landmark," +
                                    " postal_code," +
                                    " city," +
                                    " taluka," +
                                    " district," +
                                    " state," +
                                    " country," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGidAddress + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + lsaddresstype_gid + "'," +
                                    "'" + lsaddresstype_name + "'," +
                                    "'" + "Yes" + "'," +
                                    "'" + lsaddressline1.Replace("'", "") + "',";

                            if (lsaddressline2 == "" || lsaddressline2 == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsaddressline2.Replace("'", "") + "',";
                            }

                            msSQL += "''," +
                                       "'" + lspostal_code + "'," +
                                       "'" + lscity + "'," +
                                       "'" + lstaluka + "'," +
                                       "'" + lsdistrict + "'," +
                                       "'" + lsstate + "'," +
                                       "'" + lscountry + "'," +
                                       "'" + employee_gid + "'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResultAddress = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult == 1 && mnResultMobile == 1 && mnResultEmail == 1 && mnResultAddress == 1)
                            {
                                insertCount++;
                            }
                            else
                            {
                                msSQL = "delete from agr_mst_tsuprcontact where contact_gid ='" + msGetGid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                msSQL = "delete from agr_mst_tsuprcontact2mobileno where contact2mobileno_gid ='" + msGetGidMobile + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                msSQL = "delete from agr_mst_tsuprcontact2email where contact2address_gid ='" + msGetGidEmail + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                msSQL = "delete from agr_mst_tsuprcontact2address where contact2address_gid ='" + msGetGidAddress + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            }

                        }


                    }

                }

                if (insertCount > 0)
                {
                    objResult.status = true;
                    objResult.message = insertCount.ToString() + " Of " + dt.Rows.Count.ToString() + " Records Uploaded Successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error occured in uploading Excel Sheet Details";
                }

                dt.Dispose();



            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }
        }

        public void DaImportExcelInstitution(HttpRequest httpRequest, string employee_gid, result objResult)
        {
            try
            {
                int insertCount = 0;
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;
                string application_gid = httpRequest.Form["application_gid"];
                string project_flag = httpRequest.Form["project_flag"].ToString();

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/SamAgro/InstitutionDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

                if ((!System.IO.Directory.Exists(lsfilePath)))
                    System.IO.Directory.CreateDirectory(lsfilePath);


                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    httpPostedFile = httpFileCollection[i];
                }
                string FileExtension = httpPostedFile.FileName;

                string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                string lsfile_gid = msdocument_gid;
                FileExtension = Path.GetExtension(FileExtension).ToLower();
                lsfile_gid = lsfile_gid + FileExtension;

                Stream ls_readStream;
                ls_readStream = httpPostedFile.InputStream;
                MemoryStream ms = new MemoryStream();
                ls_readStream.CopyTo(ms);

                // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objResult.message = "File format is not supported";
                            objResult.status = false;
                            return;
                        }

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);

                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                    columnCount = worksheet.Dimension.End.Column;
                    endRange = worksheet.Dimension.End.Address;
                }

                file.Close();
                ms.Close();

                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";

                excelRange = "A2:" + endRange + rowCount.ToString();

                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);
                try
                {
                    dt = dt.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                }
                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = "No Records Found";
                    return;
                }
                Nullable<DateTime> ldratingas_on;

                foreach (DataRow row in dt.Rows)
                {
                    institutionimportlog_message = "";

                    lsapplication_no = row["* Application No"].ToString();
                    if (lsapplication_no == "")
                    {

                    }
                    else
                    {
                        lsurn_status = row["* Having URN (Yes/No)"].ToString();
                        lsurn = row["If Yes, URN"].ToString();

                        lscompany_name = row["* Legal/Trade Name"].ToString();
                        lscompanypan_no = row["* PAN Value"].ToString();

                        lsgst_registered = row["* GST Registered"].ToString();
                        lsgst_no = row["*  GST Number"].ToString();

                        if (lsgst_no != "")
                        {
                            msSQL = " select gst_state from agr_mst_tgstcode2state where " +
                          "gst_code='" + lsgst_no.Substring(0, 2) + "'";
                            objODBCDatareader = objdbconn.GetDataReader(msSQL);
                            if (objODBCDatareader.HasRows == true)
                            {
                                lsgst_state = objODBCDatareader["gst_state"].ToString();
                            }
                            objODBCDatareader.Close();
                        }
                        else
                        {
                            lsgst_state = "";
                        }



                        lsdate_incorporation = row["* Certificate of Incorporation(DD-MM-YYYY)"].ToString();
                        if (lsdate_incorporation.Length > 10)
                        {
                            lsdate_incorporation = dateFormatStandardizer(lsdate_incorporation);
                        }
                        lsdate_incorporation = lsdate_incorporation.Replace('-', '/');
                        lddate_incorporation = DateTime.ParseExact(lsdate_incorporation, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        lsbusinessstart_date = row["* Business Start Date(DD-MM-YYYY)"].ToString();
                        if (lsbusinessstart_date.Length > 10)
                        {
                            lsbusinessstart_date = dateFormatStandardizer(lsbusinessstart_date);
                        }
                        lsbusinessstart_date = lsbusinessstart_date.Replace('-', '/');
                        ldbusinessstart_date = DateTime.ParseExact(lsbusinessstart_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                        lscin_no = row["Corporate Identification Number(CIN)"].ToString();
                        lsofficial_telephoneno = row["* Official Landline No"].ToString();
                        lsofficialemail_address = row["* Official Email Address"].ToString();

                        lscompanytype_name = row["* Company Type"].ToString();
                        msSQL = "select companytype_gid from ocs_mst_tcompanytype where companytype_name='" + lscompanytype_name + "'";
                        lscompanytype_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsuser_type = row["* Stakeholder Type"].ToString();
                        msSQL = "select usertype_gid from ocs_mst_tusertype where user_type='" + row["* Stakeholder Type"].ToString() + "'";
                        lsusertype_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsassessmentagency_name = row["Credit Rating Agency"].ToString();
                        msSQL = "select assessmentagency_gid from ocs_mst_tassessmentagency where assessmentagency_name='" + lsassessmentagency_name + "'";
                        lsassessmentagency_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsassessmentagencyrating_name = row["Credit Rating"].ToString();
                        msSQL = "select assessmentagencyrating_gid from ocs_mst_tassessmentagencyrating where assessmentagencyrating_name='" + lsassessmentagencyrating_name + "'";
                        lsassessmentagencyrating_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsratingas_on = row["Assessed On(DD-MM-YYYY)"].ToString();

                        if (lsratingas_on.Length > 10)
                        {
                            lsratingas_on = dateFormatStandardizer(lsratingas_on);
                        }
                        if (lsratingas_on.Length > 0)
                        {
                            lsratingas_on = lsratingas_on.Replace('-', '/');
                            ldratingas_on = DateTime.ParseExact(lsratingas_on, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            ldratingas_on = null;
                        }

                        lsamlcategory_name = row["Category - AML(Anti Money Laundering)"].ToString();
                        msSQL = "select amlcategory_gid from ocs_mst_tamlcategory where amlcategory_name='" + lsamlcategory_name + "'";
                        lsamlcategory_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsbusinesscategory_name = row["* Category - Business"].ToString();
                        msSQL = "select businesscategory_gid from ocs_mst_tbusinesscategory where businesscategory_name='" + lsbusinesscategory_name + "'";
                        lsbusinesscategory_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsstart_date = row["* Start Date (DD-MM-YYYY)"].ToString();
                        if (lsstart_date.Length > 10)
                        {
                            lsstart_date = dateFormatStandardizer(lsstart_date);
                        }
                        lsstart_date = lsstart_date.Replace('-', '/');
                        ldstart_date = DateTime.ParseExact(lsstart_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                        lsend_date = row["* End Date (DD-MM-YYYY)"].ToString();
                        if (lsend_date.Length > 10)
                        {
                            lsend_date = dateFormatStandardizer(lsend_date);
                        }
                        lsend_date = lsend_date.Replace('-', '/');
                        ldend_date = DateTime.ParseExact(lsend_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        lsescrow = row["* Escrow"].ToString();
                        lslastyear_turnover = row["* Last Year Turn Over"].ToString();


                        lscontactperson_firstname = row["* First Name"].ToString();
                        lscontactperson_middlename = row["Middle Name"].ToString();
                        lscontactperson_lastname = row["Last Name"].ToString();

                        lsdesignation = row["* Designation"].ToString();
                        msSQL = "select designation_gid from ocs_mst_tdesignation where designation_type='" + lsdesignation + "'";
                        lsdesignation_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsmobile_no = row["* Mobile no"].ToString();
                        lswhatsapp_no = row["* Whatsapp  (Yes/No)"].ToString();
                        lsemail_address = row["* Email Address"].ToString();

                        lsaddresstype_name = row["* Address Type"].ToString();
                        msSQL = "select address_gid from ocs_mst_taddresstype where address_type='" + lsaddresstype_name + "'";
                        lsaddresstype_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsaddressline1 = row["* AddressLine1"].ToString();
                        lsaddressline2 = row["AddressLine2"].ToString();
                        lspostal_code = row["* Postal Code"].ToString();

                        msSQL = " select city,taluka,district,state from ocs_mst_tpostalcode where " +
                           "postalcode_value='" + lspostal_code + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lscity = objODBCDatareader["city"].ToString();
                            lstaluka = objODBCDatareader["taluka"].ToString();
                            lsdistrict = objODBCDatareader["district"].ToString();
                            lsstate = objODBCDatareader["state"].ToString();
                        }
                        objODBCDatareader.Close();
                        lscountry = row["* Country"].ToString();

                        msSQL = "select stakeholder_type from agr_mst_tsuprcontact where application_gid='" + application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
                        string lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

                        if (lsstakeholder_type == lsuser_type)
                        {
                            institutionimportlog_message = "Applicant/Borrower Information Already Added in Individual";

                        }

                        msSQL = "select stakeholder_type from agr_mst_tsuprinstitution where application_gid='" + application_gid + "' and stakeholder_type in ('Borrower','Applicant')";
                        lsstakeholder_type = objdbconn.GetExecuteScalar(msSQL);

                        if (lsstakeholder_type == lsuser_type)
                        {
                            institutionimportlog_message = "Applicant/Borrower Information Already Added in Insitution";
                        }

                        if (institutionimportlog_message == "")
                        {
                            if ((lsapplication_no == "") || (lsurn_status == "") || (lscompany_name == "") || (lscompanypan_no == "") || (lsgst_registered == "") || (lsgst_no == "") || (lsdate_incorporation == "")
                            || (lsbusinessstart_date == "") || (lsofficial_telephoneno == "") || (lsofficialemail_address == "") || (lscompanytype_name == "") || (lsuser_type == "") || (lsbusinesscategory_name == "")
                            || (lsstart_date == "") || (lsend_date == "") || (lsescrow == "") || (lslastyear_turnover == "") || (lscontactperson_firstname == "")
                            || (lsdesignation == "") || (lsmobile_no == "") || (lswhatsapp_no == "") || (lsemail_address == "") || (lsaddresstype_name == "") || (lsaddressline1 == "")
                            || (lspostal_code == "") || (lscountry == ""))
                            {
                                institutionimportlog_message = "Mandatory fields are empty";
                            }

                            if (institutionimportlog_message == "")
                            {
                                msSQL = "select company_name, companypan_no, cin_no from agr_mst_tsuprinstitution where application_gid='" + application_gid + "'";
                                dt_datatable = objdbconn.GetDataTable(msSQL);
                                if (dt_datatable.Rows.Count != 0)
                                {
                                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                                    {
                                        if ((lscompany_name == dr_datarow["company_name"].ToString()) && (lscompanypan_no == dr_datarow["companypan_no"].ToString()) && (lscin_no == dr_datarow["cin_no"].ToString()))
                                        {
                                            institutionimportlog_message = "Record has many duplicate values";
                                            break;
                                        }
                                    }
                                }
                                dt_datatable.Dispose();
                            }

                        }



                        if (institutionimportlog_message != "")
                        {

                            msGetGid = objcmnfunctions.GetMasterGID("INIL");

                            msSQL = " insert into agr_trn_tsuprinstitutionimportlog(" +
                                    " institutionimportlog_gid," +
                                    " application_gid," +
                                    " application_no," +

                                    " urn_status," +
                                    " urn," +

                                    " company_name," +
                                    " companypan_no," +
                                    " gst_registered," +
                                    " gst_no," +

                                    " date_incorporation," +
                                    " businessstart_date," +
                                    " cin_no," +
                                    " official_telephoneno," +
                                    " officialemail_address," +

                                    " companytype_name," +
                                    " stakeholder_type," +
                                    " assessmentagency_name," +
                                    " assessmentagencyrating_name," +

                                    " ratingas_on," +
                                    " amlcategory_name," +
                                    " businesscategory_name," +

                                    " start_date," +
                                    " end_date," +
                                    " escrow," +
                                    " lastyear_turnover," +

                                    " contactperson_firstname," +
                                    " contactperson_middlename," +
                                    " contactperson_lastname," +
                                    " designation," +

                                    " mobile_no," +
                                    " whatsapp_no," +
                                    " email_address," +
                                    " addresstype_name," +
                                    " addressline1," +
                                    " addressline2," +
                                    " postal_code," +
                                    " country," +

                                    " institutionimportlog_status," +

                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + application_gid + "'," +
                                    "'" + lsapplication_no + "'," +

                                     "'" + lsurn_status + "'," +
                                     "'" + lsurn + "',";
                            if (lscompany_name == "" || lscompany_name == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lscompany_name.Replace("'", "") + "',";
                            }


                            msSQL += "'" + lscompanypan_no + "'," +
                                       "'" + lsgst_registered + "'," +
                                       "'" + lsgst_no + "',";

                            if ((lddate_incorporation == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(lddate_incorporation).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }

                            if ((ldbusinessstart_date == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldbusinessstart_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }

                            msSQL += "'" + lscin_no + "'," +
                                      "'" + lsofficial_telephoneno + "'," +
                                      "'" + lsofficialemail_address + "'," +

                                      "'" + lscompanytype_name + "'," +
                                      "'" + lsuser_type + "'," +
                                      "'" + lsassessmentagency_name + "'," +
                                      "'" + lsassessmentagencyrating_name + "',";

                            if ((ldratingas_on == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldratingas_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }


                            msSQL += "'" + lsamlcategory_name + "'," +
                                          "'" + lsbusinesscategory_name + "',";

                            if ((ldstart_date == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldstart_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }

                            if ((ldend_date == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldend_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }


                            msSQL += "'" + lsescrow + "'," +
                                           "'" + lslastyear_turnover + "'," +

                                           "'" + lscontactperson_firstname + "'," +
                                           "'" + lscontactperson_middlename + "'," +
                                           "'" + lscontactperson_lastname + "'," +
                                           "'" + lsdesignation + "'," +

                                           "'" + lsmobile_no + "'," +
                                           "'" + lswhatsapp_no + "'," +
                                           "'" + lsemail_address + "'," +

                                           "'" + lsaddresstype_name + "',";

                            if (lsaddressline1 == "" || lsaddressline1 == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsaddressline1.Replace("'", "") + "',";
                            }

                            if (lsaddressline2 == "" || lsaddressline2 == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsaddressline2.Replace("'", "") + "',";
                            }


                            msSQL += "'" + lspostal_code + "'," +
                                     "'" + lscountry + "'," +

                                   "'" + institutionimportlog_message + "'," +

                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                        else
                        {

                            msGetGid = objcmnfunctions.GetMasterGID("APIN");

                            msSQL = " insert into agr_mst_tsuprinstitution(" +
                       " institution_gid," +
                       " application_gid," +
                       " application_no," +

                       " urn_status," +
                       " urn," +

                       " company_name," +
                       " companypan_no," +

                       " date_incorporation," +
                       " businessstart_date," +
                       " cin_no," +
                       " official_telephoneno," +
                       " officialemail_address," +

                       " companytype_gid," +
                       " companytype_name," +
                       " stakeholdertype_gid," +
                       " stakeholder_type," +
                       " assessmentagency_gid," +
                       " assessmentagency_name," +
                       " assessmentagencyrating_gid," +
                       " assessmentagencyrating_name," +

                       " ratingas_on," +
                       " amlcategory_gid," +
                       " amlcategory_name," +
                       " businesscategory_gid," +
                       " businesscategory_name," +

                       " start_date," +
                       " end_date," +
                       " escrow," +
                       " lastyear_turnover," +

                       " contactperson_firstname," +
                       " contactperson_middlename," +
                       " contactperson_lastname," +
                       " designation_gid," +
                       " designation," +

                       " institution_status," +
                       " created_by," +
                       " created_date) values(" +
                         "'" + msGetGid + "'," +
                         "'" + application_gid + "'," +
                         "'" + lsapplication_no + "'," +

                         "'" + lsurn_status + "'," +
                         "'" + lsurn + "'," +

                         "'" + lscompany_name.Replace("'", "") + "'," +
                         "'" + lscompanypan_no + "',";

                            if ((lddate_incorporation == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(lddate_incorporation).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }

                            if ((ldbusinessstart_date == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldbusinessstart_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }


                            msSQL += "'" + lscin_no + "'," +
                                    "'" + lsofficial_telephoneno + "'," +
                                    "'" + lsofficialemail_address + "'," +
                                    "'" + lscompanytype_gid + "'," +
                                    "'" + lscompanytype_name + "'," +
                                    "'" + lsusertype_gid + "'," +
                                    "'" + lsuser_type + "'," +
                                    "'" + lsassessmentagency_gid + "'," +
                                    "'" + lsassessmentagency_name + "'," +
                                    "'" + lsassessmentagencyrating_gid + "'," +
                                    "'" + lsassessmentagencyrating_name + "',";
                            if ((ldratingas_on == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldratingas_on).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }
                            msSQL += "'" + lsamlcategory_gid + "'," +
                                    "'" + lsamlcategory_name + "'," +
                                    "'" + lsbusinesscategory_gid + "'," +
                                    "'" + lsbusinesscategory_name + "',";

                            if ((ldstart_date == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldstart_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }
                            if ((ldend_date == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(ldend_date).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }

                            msSQL += "'" + lsescrow + "'," +
                                     "'" + lslastyear_turnover + "'," +

                                    "'" + lscontactperson_firstname + "'," +
                                    "'" + lscontactperson_middlename + "'," +
                                    "'" + lscontactperson_lastname + "'," +
                                    "'" + lsdesignation_gid + "'," +
                                    "'" + lsdesignation + "'," +

                                "'" + "Incomplete" + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msGetGidGST = objcmnfunctions.GetMasterGID("ITGS");

                            msSQL = " insert into agr_mst_tsuprinstitution2branch(" +
                           " institution2branch_gid," +
                           " institution_gid," +
                           " gst_registered," +
                           " gst_state," +
                           " gst_no," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGidGST + "'," +
                           "'" + msGetGid + "'," +
                           "'" + lsgst_registered + "'," +
                           "'" + lsgst_state + "'," +
                           "'" + lsgst_no + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResultGST = objdbconn.ExecuteNonQuerySQL(msSQL);


                            msGetGidMobile = objcmnfunctions.GetMasterGID("IT2M");



                            msSQL = " insert into agr_mst_tsuprinstitution2mobileno(" +
                           " institution2mobileno_gid," +
                           " institution_gid," +
                           " mobile_no," +
                           " primary_status," +
                           " whatsapp_no," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGidMobile + "'," +
                           "'" + msGetGid + "'," +
                           "'" + lsmobile_no + "'," +
                           "'" + "Yes" + "'," +
                           "'" + lswhatsapp_no + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResultMobile = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msGetGidEmail = objcmnfunctions.GetMasterGID("C2EA");



                            msSQL = " insert into agr_mst_tsuprinstitution2email(" +
                                    " institution2email_gid," +
                                    " institution_gid," +
                                    " email_address," +
                                    " primary_status," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGidEmail + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + lsemail_address + "'," +
                                    "'" + "Yes" + "'," +
                                    "'" + employee_gid + "'," +
                                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResultEmail = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msGetGidAddress = objcmnfunctions.GetMasterGID("C2AD");



                            msSQL = " insert into agr_mst_tsuprinstitution2address(" +
                                    " institution2address_gid," +
                                    " institution_gid," +
                                    " addresstype_gid," +
                                    " addresstype_name," +
                                    " primary_status," +
                                    " addressline1," +
                                    " addressline2," +
                                    " landmark," +
                                    " postal_code," +
                                    " city," +
                                    " taluka," +
                                    " district," +
                                    " state," +
                                    " country," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGidAddress + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + lsaddresstype_gid + "'," +
                                    "'" + lsaddresstype_name + "'," +
                                    "'" + "Yes" + "'," +
                                    "'" + lsaddressline1.Replace("'", "") + "',";

                            if (lsaddressline2 == "" || lsaddressline2 == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsaddressline2.Replace("'", "") + "',";
                            }

                            msSQL += "''," +
                                       "'" + lspostal_code + "'," +
                                       "'" + lscity + "'," +
                                       "'" + lstaluka + "'," +
                                       "'" + lsdistrict + "'," +
                                       "'" + lsstate + "'," +
                                       "'" + lscountry + "'," +
                                       "'" + employee_gid + "'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResultAddress = objdbconn.ExecuteNonQuerySQL(msSQL);

                            if (mnResult == 1 && mnResultMobile == 1 && mnResultEmail == 1 && mnResultAddress == 1 && mnResultGST == 1)
                            {
                                insertCount++;
                            }
                            else
                            {
                                msSQL = "delete from agr_mst_tsuprinstitution where institution_gid ='" + msGetGid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                msSQL = "delete from agr_mst_tsuprinstitution2branch where institution2branch_gid ='" + msGetGidGST + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                msSQL = "delete from agr_mst_tsuprinstitution2mobileno where institution2mobileno_gid ='" + msGetGidMobile + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                msSQL = "delete from agr_mst_tsuprinstitution2email where institution2email_gid ='" + msGetGidEmail + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                msSQL = "delete from agr_mst_tsuprinstitution2address where institution2address_gid ='" + msGetGidAddress + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                        }

                    }
                }

                if (insertCount > 0)
                {
                    objResult.status = true;
                    objResult.message = insertCount.ToString() + " Of " + dt.Rows.Count.ToString() + " Records Uploaded Successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error occured in uploading Excel Sheet Details";
                }


                dt.Dispose();



            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }
        }

        public void DaImportExcelGroup(HttpRequest httpRequest, string employee_gid, result objResult)
        {
            try
            {
                int insertCount = 0;
                HttpFileCollection httpFileCollection;
                DataTable dt = null;
                string lspath, lsfilePath;
                string application_gid = httpRequest.Form["application_gid"];
                string project_flag = httpRequest.Form["project_flag"].ToString();

                msSQL = " select company_code from adm_mst_tcompany";
                lscompany_code = objdbconn.GetExecuteScalar(msSQL);

                // Create Directory
                lsfilePath = HttpContext.Current.Server.MapPath("erpdocument" + "/" + lscompany_code + "/SamAgro/GroupDocument/" + DateTime.Now.Year + "/" + DateTime.Now.Month);

                if ((!System.IO.Directory.Exists(lsfilePath)))
                    System.IO.Directory.CreateDirectory(lsfilePath);


                httpFileCollection = httpRequest.Files;
                for (int i = 0; i < httpFileCollection.Count; i++)
                {
                    httpPostedFile = httpFileCollection[i];
                }
                string FileExtension = httpPostedFile.FileName;

                string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                string lsfile_gid = msdocument_gid;
                FileExtension = Path.GetExtension(FileExtension).ToLower();
                lsfile_gid = lsfile_gid + FileExtension;

                Stream ls_readStream;
                ls_readStream = httpPostedFile.InputStream;
                MemoryStream ms = new MemoryStream();
                ls_readStream.CopyTo(ms);

                // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objResult.message = "File format is not supported";
                            objResult.status = false;
                            return;
                        }

                //path creation        
                lspath = lsfilePath + "/";
                FileStream file = new FileStream(lspath + lsfile_gid, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);

                using (ExcelPackage xlPackage = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    rowCount = worksheet.Dimension.End.Row;
                    columnCount = worksheet.Dimension.End.Column;
                    endRange = worksheet.Dimension.End.Address;
                }

                file.Close();
                ms.Close();

                objcmnfunctions.uploadFile(lspath, lsfile_gid);

                //Excel To DataTable

                lsfilePath = @"" + lsfilePath.Replace("/", "\\") + "\\" + lsfile_gid + "";

                excelRange = "A2:" + endRange + rowCount.ToString();


                dt = objcmnfunctions.ExcelToDataTable(lsfilePath, excelRange);
                try
                {
                    dt = dt.Rows.Cast<DataRow>().Where(r => string.Join("", r.ItemArray).Trim() != string.Empty).CopyToDataTable();
                }
                catch (Exception ex)
                {
                    objResult.status = false;
                    objResult.message = "No Records Found";
                    return;
                }
                Nullable<DateTime> lddate_of_formation;

                foreach (DataRow row in dt.Rows)
                {
                    groupimportlog_message = "";

                    lsapplication_no = row["* Application No"].ToString();
                    if (lsapplication_no == "")
                    {

                    }
                    else
                    {
                        lsgroup_name = row["* Group Name"].ToString();

                        lsdate_of_formation = row["Date of Formation(DD-MM-YYYY)"].ToString();

                        if (lsdate_of_formation.Length > 10)
                        {
                            lsdate_of_formation = dateFormatStandardizer(lsdate_of_formation);
                        }
                        if (lsdate_of_formation.Length > 0)
                        {
                            lsdate_of_formation = lsdate_of_formation.Replace('-', '/');
                            lddate_of_formation = DateTime.ParseExact(lsdate_of_formation, "d/M/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            lddate_of_formation = null;
                        }

                        lsgroup_type = row["* Group Type"].ToString();
                        lsgroupmember_count = row["* Member Count of the Group"].ToString();

                        lsgroupurn_status = row["* Having URN (Yes/No)"].ToString();
                        lsgroup_urn = row["If Yes, URN"].ToString();

                        lsaddresstype_name = row["* Address Type"].ToString();
                        msSQL = "select address_gid from ocs_mst_taddresstype where address_type='" + lsaddresstype_name + "'";
                        lsaddresstype_gid = objdbconn.GetExecuteScalar(msSQL);

                        lsaddressline1 = row["* AddressLine1"].ToString();
                        lsaddressline2 = row["AddressLine2"].ToString();
                        lspostal_code = row["* Postal Code"].ToString();

                        msSQL = " select city,taluka,district,state from ocs_mst_tpostalcode where " +
                           "postalcode_value='" + lspostal_code + "'";
                        objODBCDatareader = objdbconn.GetDataReader(msSQL);
                        if (objODBCDatareader.HasRows == true)
                        {
                            lscity = objODBCDatareader["city"].ToString();
                            lstaluka = objODBCDatareader["taluka"].ToString();
                            lsdistrict = objODBCDatareader["district"].ToString();
                            lsstate = objODBCDatareader["state"].ToString();
                        }
                        objODBCDatareader.Close();
                        lscountry = row["* Country"].ToString();

                        lsifsc_code = row["* IFSC Code"].ToString();
                        lsbank_accountno = row["* Bank Account Number"].ToString();
                        lsaccountholder_name = row["* Account Holder Name"].ToString();
                        lsbank_name = row["* Bank Name"].ToString();
                        lsbank_branch = row["* Branch Name"].ToString();

                        if ((lsapplication_no == "") || (lsgroup_name == "") || (lsgroup_type == "") || (lsgroupmember_count == "") || (lsgroupurn_status == "")
                            || (lsaddresstype_name == "") || (lsaddressline1 == "") || (lspostal_code == "") || (lscountry == "")
                            || (lsifsc_code == "") || (lsbank_accountno == "") || (lsaccountholder_name == "") || (lsbank_name == "") || (lsbank_branch == ""))
                        {
                            groupimportlog_message = "Mandatory fields are empty";
                        }

                        if (groupimportlog_message == "")
                        {
                            msSQL = "select group_name, groupmember_count, groupurn_status from agr_mst_tsuprgroup where application_gid='" + application_gid + "'";
                            dt_datatable = objdbconn.GetDataTable(msSQL);
                            if (dt_datatable.Rows.Count != 0)
                            {
                                foreach (DataRow dr_datarow in dt_datatable.Rows)
                                {
                                    if ((lsgroup_name == dr_datarow["group_name"].ToString()) && (lsgroupmember_count == dr_datarow["groupmember_count"].ToString()) && (lsgroupurn_status == dr_datarow["groupurn_status"].ToString()))
                                    {
                                        groupimportlog_message = "Record has many duplicate values";
                                        break;
                                    }
                                }
                            }
                            dt_datatable.Dispose();
                        }

                        if (groupimportlog_message != "")
                        {

                            msGetGid = objcmnfunctions.GetMasterGID("GRIL");

                            msSQL = " insert into agr_trn_tsuprgroupimportlog(" +
                                    " groupimportlog_gid," +
                                    " application_gid," +
                                    " application_no," +

                                    " group_name," +
                                    " date_of_formation," +
                                    " group_type," +
                                    " groupmember_count," +
                                    " groupurn_status," +
                                    " group_urn," +

                                    " addresstype_name," +
                                    " addressline1," +
                                    " addressline2," +
                                    " postal_code," +
                                    " country," +

                                    " ifsc_code," +
                                    " bank_accountno," +
                                    " accountholder_name," +
                                    " bank_name," +
                                    " bank_branch," +



                                    " groupimportlog_status," +

                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGid + "'," +
                                    "'" + application_gid + "'," +
                                    "'" + lsapplication_no + "',";

                            if (lsgroup_name == "" || lsgroup_name == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsgroup_name.Replace("'", "") + "',";
                            }

                            if ((lddate_of_formation == null))
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(lddate_of_formation).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }

                            msSQL += "'" + lsgroup_type + "'," +
                                       "'" + lsgroupmember_count + "'," +
                                       "'" + lsgroupurn_status + "'," +
                                       "'" + lsgroup_urn + "'," +
                                       "'" + lsaddresstype_name + "',";

                            if (lsaddressline1 == "" || lsaddressline1 == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsaddressline1.Replace("'", "") + "',";
                            }

                            if (lsaddressline2 == "" || lsaddressline2 == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsaddressline2.Replace("'", "") + "',";
                            }


                            msSQL += "'" + lspostal_code + "'," +
                                     "'" + lscountry + "'," +

                                     "'" + lsifsc_code + "'," +
                                     "'" + lsbank_accountno + "',";

                            if (lsaccountholder_name == "" || lsaccountholder_name == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsaccountholder_name.Replace("'", "") + "',";
                            }

                            if (lsbank_name == "" || lsbank_name == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsbank_name.Replace("'", "") + "',";
                            }

                            msSQL += "'" + lsbank_branch + "'," +
                                       "'" + groupimportlog_message + "'," +

                                       "'" + employee_gid + "'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        }
                        else
                        {

                            msGetGid = objcmnfunctions.GetMasterGID("GRUP");

                            msSQL = " insert into agr_mst_tsuprgroup(" +
                            " group_gid," +
                            " application_gid," +
                            " group_name," +
                            " date_of_formation," +
                            " group_type," +
                            " groupmember_count," +
                            " groupurn_status," +
                            " group_urn," +
                            " group_status," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + application_gid + "'," +
                            "'" + lsgroup_name.Replace("'", "") + "',";

                            if (lddate_of_formation == null)
                            {
                                msSQL += "null,";
                            }
                            else
                            {
                                msSQL += "'" + Convert.ToDateTime(lddate_of_formation).ToString("yyyy-MM-dd HH:mm:ss") + "',";
                            }

                            msSQL += "'" + lsgroup_type + "'," +
                                     "'" + lsgroupmember_count + "'," +
                                     "'" + lsgroupurn_status + "'," +
                                     "'" + lsgroup_urn + "'," +
                                     "'Incomplete'," +
                                     "'" + employee_gid + "'," +
                                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                            msGetGidGST = objcmnfunctions.GetMasterGID("ITGS");

                            msSQL = " insert into agr_mst_tsuprinstitution2branch(" +
                           " institution2branch_gid," +
                           " institution_gid," +
                           " gst_registered," +
                           " gst_state," +
                           " gst_no," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetGidGST + "'," +
                           "'" + msGetGid + "'," +
                           "'" + lsgst_registered + "'," +
                           "'" + lsgst_state + "'," +
                           "'" + lsgst_no + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResultGST = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msGetGidAddress = objcmnfunctions.GetMasterGID("C2AD");


                            msSQL = " insert into agr_mst_tsuprgroup2address(" +
                                    " group2address_gid," +
                                    " group_gid," +
                                    " addresstype_gid," +
                                    " addresstype_name," +
                                    " primary_status," +
                                    " addressline1," +
                                    " addressline2," +
                                    " landmark," +
                                    " postal_code," +
                                    " city," +
                                    " taluka," +
                                    " district," +
                                    " state," +
                                    " country," +
                                    " created_by," +
                                    " created_date)" +
                                    " values(" +
                                    "'" + msGetGidAddress + "'," +
                                    "'" + msGetGid + "'," +
                                    "'" + lsaddresstype_gid + "'," +
                                    "'" + lsaddresstype_name + "'," +
                                    "'" + "Yes" + "'," +
                                    "'" + lsaddressline1.Replace("'", "") + "',";

                            if (lsaddressline2 == "" || lsaddressline2 == null)
                            {
                                msSQL += "'',";
                            }
                            else
                            {
                                msSQL += "'" + lsaddressline2.Replace("'", "") + "',";
                            }

                            msSQL += "''," +
                                       "'" + lspostal_code + "'," +
                                       "'" + lscity + "'," +
                                       "'" + lstaluka + "'," +
                                       "'" + lsdistrict + "'," +
                                       "'" + lsstate + "'," +
                                       "'" + lscountry + "'," +
                                       "'" + employee_gid + "'," +
                                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResultAddress = objdbconn.ExecuteNonQuerySQL(msSQL);

                            msGetGidBank = objcmnfunctions.GetMasterGID("G2BK");

                            msSQL = " insert into agr_mst_tsuprgroup2bank(" +
                                  " group2bank_gid," +
                                  " group_gid," +
                                  " ifsc_code," +
                                  " bank_accountno," +
                                  " accountholder_name," +
                                  " bank_name," +
                                  " bank_branch," +

                                  " created_by," +
                                  " created_date)" +
                                  " values(" +
                                  "'" + msGetGidBank + "'," +
                                  "'" + msGetGid + "'," +
                                  "'" + lsifsc_code + "'," +
                                  "'" + lsbank_accountno + "'," +
                                  "'" + lsaccountholder_name.Replace("'", "") + "'," +
                                  "'" + lsbank_name.Replace("'", "") + "'," +
                                  "'" + lsbank_branch + "'," +
                                  "'" + employee_gid + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            mnResultBank = objdbconn.ExecuteNonQuerySQL(msSQL);


                            if (mnResult == 1 && mnResultAddress == 1 && mnResultBank == 1)
                            {
                                insertCount++;
                            }
                            else
                            {
                                msSQL = "delete from agr_mst_tsuprgroup where group_gid ='" + msGetGid + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                msSQL = "delete from agr_mst_tsuprgroup2address where group2address_gid ='" + msGetGidAddress + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                                msSQL = "delete from agr_mst_tsuprgroup2bank where group2bank_gid ='" + msGetGidBank + "'";
                                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                            }

                        }

                    }
                }

                if (insertCount > 0)
                {
                    objResult.status = true;
                    objResult.message = insertCount.ToString() + " Of " + dt.Rows.Count.ToString() + " Records Uploaded Successfully";
                }
                else
                {
                    objResult.status = false;
                    objResult.message = "Error occured in uploading Excel Sheet Details";
                }
                dt.Dispose();
            }
            catch (Exception ex)
            {
                objResult.status = false;
                objResult.message = ex.ToString();
            }
        }

        public string dateFormatStandardizer(string sentDate)
        {
            string[] dateArr = sentDate.Split(' ');
            DateTime ldreturnDate = DateTime.ParseExact(dateArr[0], "M/d/yyyy", CultureInfo.InvariantCulture);
            string returnDate = ldreturnDate.ToString("dd-MM-yyyy");
            return returnDate;
        }

        public void DaGetApplicationNewSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,approval_status,applicant_type,a.created_by as createdby," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date," +
                    " case when a.headapproval_status='Pending' then 'Pending' when a.headapproval_status='Comment Raised' then 'Comment Raised' else 'Level Approved' end as headapproval_status," +
                    " productcharge_flag, economical_flag,creditheadapproval_status from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid where a.created_by='" + employee_gid + "' and " +
                    " (application_gid not in (select application_gid from agr_mst_tsuprapplication where approval_status ='Rejected By Credit' or  approval_status ='Hold By Credit' " +
                    " or approval_status = 'Rejected By Business' or approval_status = 'Hold By Business')) and " +
                    " (a.headapproval_status='Pending' or a.headapproval_status='Comment Raised' or a.headapproval_status like '%Approved' or a.creditheadapproval_status like '%Approved' or " +
                    " a.approval_status='Submitted to CC'  or a.approval_status='Submitted to Underwriting' or a.approval_status='Submitted to Credit Approval'  or a.approval_status='Submitted to Approval')  order by application_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " select application_gid from agr_trn_tsuprapplicationcreditquery where query_status = 'Open' and application_gid = '" + dt["application_gid"].ToString() + "' " +
                            " and queryraised_to = 'RM'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == false)
                    {
                        lsrmquery_flag = "Y";
                    }
                    else
                    {
                        lsrmquery_flag = "N";
                    }
                    objODBCDatareader.Close();
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        headapproval_status = dt["headapproval_status"].ToString(),
                        creditheadapproval_status = dt["creditheadapproval_status"].ToString(),
                        rmquery_flag = lsrmquery_flag
                    });

                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetApplicationRejectedSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,a.headapproval_status," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,approval_status,applicant_type,a.created_by as createdby," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date," +
                    " productcharge_flag, economical_flag from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid where a.created_by='" + employee_gid + "' and (a.headapproval_status like '%Rejected' or  a.creditheadapproval_status like '%Rejected')  order by application_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        headapproval_status = dt["headapproval_status"].ToString(),
                    });

                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetApplicationHoldSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,a.headapproval_status," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,approval_status,applicant_type,a.created_by as createdby," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date," +
                    " productcharge_flag, economical_flag from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid where a.created_by='" + employee_gid + "' and (a.headapproval_status like '%Hold' or a.creditheadapproval_status like '%Hold') order by application_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        headapproval_status = dt["headapproval_status"].ToString(),
                    });

                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetApplicationApprovedSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select application_gid,application_no,customerref_name as customer_name,customer_urn,vertical_name,a.headapproval_status," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,approval_status,applicant_type,a.created_by as createdby," +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date,date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date," +
                    " productcharge_flag, economical_flag from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid where a.created_by='" + employee_gid + "' and a.approval_status='CC Approved' order by application_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["approval_status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        updated_date = dt["updated_date"].ToString(),
                        createdby = dt["createdby"].ToString(),
                        headapproval_status = dt["headapproval_status"].ToString(),
                    });

                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaApplicationCount(string user_gid, string employee_gid, ApplicationCount values)
        {
            msSQL = " select count(application_gid) as newapplication_count from agr_mst_tsuprapplication a where a.created_by='" + employee_gid + "' and " +
                    " (application_gid not in (select application_gid from agr_mst_tsuprapplication where approval_status ='Rejected By Credit' or  approval_status ='Hold By Credit' " +
                   " or approval_status = 'Rejected By Business' or approval_status = 'Hold By Business')) and" +
                    " ( a.headapproval_status='Pending' or a.headapproval_status='Comment Raised' or a.headapproval_status like '%Approved' or a.creditheadapproval_status like '%Approved'  or " +
                    " a.approval_status='Submitted to CC'  or a.approval_status='Submitted to Underwriting' or a.approval_status='Submitted to Credit Approval'  or a.approval_status='Submitted to Approval')  ";
            values.newapplication_count = objdbconn.GetExecuteScalar(msSQL);
            int newapplicationcount = Convert.ToInt16(values.newapplication_count);

            msSQL = "select count(application_gid) as rejected_count from agr_mst_tsuprapplication a where a.created_by='" + employee_gid + "' and (a.headapproval_status like '%Rejected' or  a.creditheadapproval_status like '%Rejected') ";
            values.rejected_count = objdbconn.GetExecuteScalar(msSQL);
            int rejectedcount = Convert.ToInt16(values.rejected_count);

            msSQL = "select count(application_gid) as hold_count from agr_mst_tsuprapplication a where a.created_by='" + employee_gid + "' and (a.headapproval_status like '%Hold' or  a.creditheadapproval_status like '%Hold') ";
            values.hold_count = objdbconn.GetExecuteScalar(msSQL);
            int holdcount = Convert.ToInt16(values.hold_count);

            msSQL = "select count(application_gid) as ccapproved_count from agr_mst_tsuprapplication a where a.created_by='" + employee_gid + "' and a.approval_status='CC Approved'";
            values.ccapproved_count = objdbconn.GetExecuteScalar(msSQL);
            int ccapprovedcount = Convert.ToInt16(values.ccapproved_count);


            int totalcount = newapplicationcount + rejectedcount + holdcount + ccapprovedcount;
            values.lstotalcount = Convert.ToInt16(totalcount);

        }

        public void DaGetAppAssignedAssignmentSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.status,a.applicant_type,date_format(a.headapproval_date,'%d-%m-%Y %h:%i %p') as headapproval_date," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date, a.productcharge_flag, a.economical_flag,a.approval_status,a.creditmanager_name, " +
                    " a.overalllimit_amount, region,a.creditgroup_gid,concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code)  as creditassigned_by,date_format(a.creditassigned_date,'%d-%m-%Y %h:%i %p') as creditassigned_date from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join hrm_mst_temployee d on d.employee_gid=a.creditassigned_by " +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                    " where a.approval_flag='Y' and creditgroup_status='Assigned' and a.approval_status!='Submitted to CC' and" +
                    " (a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcredit2credithead where employee_gid ='" + employee_gid + "')" +
                    " or a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcredit2nationalmanager where employee_gid ='" + employee_gid + "')" +
                    " or a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcreditr2regionalmanager where employee_gid ='" + employee_gid + "')" +
                    " or a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcredit2creditmanager where employee_gid ='" + employee_gid + "'))" +
                    " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        region = dt["region"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        creditassigned_date = dt["creditassigned_date"].ToString(),
                        creditassigned_by = dt["creditassigned_by"].ToString(),
                        creditassigned_to = dt["creditmanager_name"].ToString(),
                        headapproval_date = dt["headapproval_date"].ToString()
                    });

                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetAppPendingAssignmentSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.status,a.applicant_type," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date, a.productcharge_flag, a.economical_flag,a.approval_status, " +
                    " a.overalllimit_amount, region,a.creditgroup_gid,a.creditgroup_name,date_format(a.headapproval_date,'%d-%m-%Y %h:%i %p') as headapproval_date  from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " where a.approval_flag='Y' and creditgroup_status='Pending' and " +
                    " (a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcredit2credithead where employee_gid ='" + employee_gid + "')" +
                    " or a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcredit2nationalmanager where employee_gid ='" + employee_gid + "')" +
                    " or a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcreditr2regionalmanager where employee_gid ='" + employee_gid + "')" +
                    " or a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcredit2creditmanager where employee_gid ='" + employee_gid + "'))" +
                    " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        region = dt["region"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        creditgroup_name = dt["creditgroup_name"].ToString(),
                        headapproval_date = dt["headapproval_date"].ToString()
                    });

                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaAssignApplicationCount(string user_gid, string employee_gid, AssignApplicationCount values)
        {
            msSQL = "select count(application_gid) as pending_count from agr_mst_tsuprapplication a where a.approval_flag='Y' and creditgroup_status='Pending'and " +
                    " (a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcredit2credithead where employee_gid ='" + employee_gid + "')" +
                    " or a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcredit2nationalmanager where employee_gid ='" + employee_gid + "')" +
                    " or a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcreditr2regionalmanager where employee_gid ='" + employee_gid + "')" +
                    " or a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcredit2creditmanager where employee_gid ='" + employee_gid + "'))";
            values.pending_count = objdbconn.GetExecuteScalar(msSQL);
            int pending_count = Convert.ToInt16(values.pending_count);

            msSQL = "select count(application_gid) as assigned_count from agr_mst_tsuprapplication a where a.approval_flag='Y' and creditgroup_status='Assigned' and a.approval_status != 'Submitted to CC' and" +
                    " (a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcredit2credithead where employee_gid ='" + employee_gid + "')" +
                    " or a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcredit2nationalmanager where employee_gid ='" + employee_gid + "')" +
                    " or a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcreditr2regionalmanager where employee_gid ='" + employee_gid + "')" +
                    " or a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcredit2creditmanager where employee_gid ='" + employee_gid + "'))";
            values.assigned_count = objdbconn.GetExecuteScalar(msSQL);
            int assigned_count = Convert.ToInt16(values.assigned_count);

            msSQL = "select count(application_gid) as assigned_count from agr_mst_tsuprapplication a where a.approval_flag='Y' and creditgroup_status='Assigned' and a.approval_status='Submitted to CC' and" +
                   " (a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcredit2credithead where employee_gid ='" + employee_gid + "')" +
                   " or a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcredit2nationalmanager where employee_gid ='" + employee_gid + "')" +
                   " or a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcreditr2regionalmanager where employee_gid ='" + employee_gid + "')" +
                   " or a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcredit2creditmanager where employee_gid ='" + employee_gid + "'))";
            values.submittedtocc_count = objdbconn.GetExecuteScalar(msSQL);
            int submittedtocc_count = Convert.ToInt16(values.submittedtocc_count);

            int totalcount = pending_count + assigned_count + submittedtocc_count;
            values.lstotalcount = Convert.ToInt16(totalcount);

        }

        public void DaGetApplSubmittedToCCSummary(string employee_gid, MdlMstApplicationAdd values)
        {
            msSQL = " select a.application_gid,a.application_no,a.customerref_name as customer_name,a.customer_urn,a.vertical_name,date_format(a.headapproval_date,'%d-%m-%Y %h:%i %p') as headapproval_date," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,a.status,a.applicant_type," +
                    " date_format(a.submitted_date,'%d-%m-%Y %h:%i %p') as updated_date, a.productcharge_flag, a.economical_flag,a.approval_status,a.creditmanager_name, " +
                    " a.overalllimit_amount, region,a.creditgroup_gid,concat(e.user_firstname,' ',e.user_lastname,' / ',e.user_code)  as creditassigned_by,date_format(a.creditassigned_date,'%d-%m-%Y %h:%i %p') as creditassigned_date from agr_mst_tsuprapplication a" +
                    " left join hrm_mst_temployee b on b.employee_gid=a.submitted_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    " left join hrm_mst_temployee d on d.employee_gid=a.creditassigned_by " +
                    " left join adm_mst_tuser e on e.user_gid=d.user_gid " +
                    " where a.approval_flag='Y' and a.approval_status='Submitted to CC' and " +
                    " (a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcredit2credithead where employee_gid ='" + employee_gid + "')" +
                    " or a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcredit2nationalmanager where employee_gid ='" + employee_gid + "')" +
                    " or a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcreditr2regionalmanager where employee_gid ='" + employee_gid + "')" +
                    " or a.creditgroup_gid in (select creditmapping_gid from ocs_mst_tcredit2creditmanager where employee_gid ='" + employee_gid + "'))" +
                    " group by a.application_gid order by a.updated_date desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getapplicationadd_list = new List<applicationadd_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getapplicationadd_list.Add(new applicationadd_list
                    {
                        application_no = dt["application_no"].ToString(),
                        customer_name = dt["customer_name"].ToString(),
                        customer_urn = dt["customer_urn"].ToString(),
                        vertical_name = dt["vertical_name"].ToString(),
                        created_date = dt["updated_date"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        application_gid = dt["application_gid"].ToString(),
                        economical_flag = dt["economical_flag"].ToString(),
                        productcharge_flag = dt["productcharge_flag"].ToString(),
                        application_status = dt["status"].ToString(),
                        applicant_type = dt["applicant_type"].ToString(),
                        approval_status = dt["approval_status"].ToString(),
                        region = dt["region"].ToString(),
                        overalllimit_amount = dt["overalllimit_amount"].ToString(),
                        creditgroup_gid = dt["creditgroup_gid"].ToString(),
                        creditassigned_date = dt["creditassigned_date"].ToString(),
                        creditassigned_by = dt["creditassigned_by"].ToString(),
                        creditassigned_to = dt["creditmanager_name"].ToString(),
                        headapproval_date = dt["headapproval_date"].ToString()
                    });

                }
            }
            values.applicationadd_list = getapplicationadd_list;
            dt_datatable.Dispose();
        }

        public void DaGetContactBureauList(string contact_gid, MdlContactBureau values)
        {
            msSQL = "select contact2bureau_gid,bureauname_name,bureau_score,date_format(bureauscore_date, '%d-%m-%Y') as bureauscore_date from agr_mst_tsuprcontact2bureau where " +
              " contact_gid='" + contact_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactbureau_list = new List<contactbureau_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactbureau_list.Add(new contactbureau_list
                    {
                        contact2bureau_gid = (dr_datarow["contact2bureau_gid"].ToString()),
                        bureauname_name = (dr_datarow["bureauname_name"].ToString()),
                        bureau_score = (dr_datarow["bureau_score"].ToString()),
                        bureauscore_date = (dr_datarow["bureauscore_date"].ToString()),
                    });
                }
            }
            values.contactbureau_list = getcontactbureau_list;
            dt_datatable.Dispose();
        }

        public void DaDeleteContactBureau(string contact2bureau_gid, MdlContactBureau values)
        {
            msSQL = "delete from agr_mst_tsuprcontact2bureau where contact2bureau_gid='" + contact2bureau_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Bureau Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaUpdateCICUploadIndividual(string employee_gid, MdlCICIndividual values)
        {
            //msSQL = "select contact2bureau_gid from  agr_mst_tsuprindividual2cicdocumentupload where  contact2bureau_gid='" + employee_gid + "' or contact2bureau_gid='" + values.contact2bureau_gid + "' ";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
            //if (objODBCDatareader.HasRows == false)
            //{
            //    values.status = false;
            //    values.message = "Kindly Add The Document";
            //    return false;
            //}

            try
            {
                var bureauscore_date = Convert.ToDateTime(values.bureauscore_date).ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Kindly enter the valid date format";
                return false;
            }

            msSQL = " update agr_mst_tsuprcontact2bureau set " +
                       " bureauname_gid='" + values.bureauname_gid + "'," +
                       " bureauname_name='" + values.bureauname_name + "'," +
                       " bureau_score='" + values.bureau_score + "',";

            if ((values.bureauscore_date == null) || (values.bureauscore_date == ""))
            {
                msSQL += "bureauscore_date=null,";
            }
            else if (Convert.ToDateTime(values.bureauscore_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " bureauscore_date='" + Convert.ToDateTime(values.bureauscore_date).ToString("yyyy-MM-dd 00:00:00") + "',";
            }

            msSQL += " observations='" + values.observations.Replace("'", "") + "'," +
                       " bureau_response='" + values.bureau_response.Replace("'", "") + "'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where contact2bureau_gid='" + values.contact2bureau_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                msSQL = "update agr_mst_tsuprindividual2cicdocumentupload set contact_gid='" + values.contact_gid + "' where contact_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprindividual2cicdocumentupload set contact2bureau_gid='" + values.contact2bureau_gid + "' where contact2bureau_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



                values.status = true;
                values.message = "Bureau Details Updated for Individual Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }

        public void DaGetInstitutionBureauList(string institution_gid, MdlInstitutionBureau values)
        {
            msSQL = "select institution2bureau_gid,bureauname_name,bureau_score,date_format(bureauscore_date, '%d-%m-%Y') as bureauscore_date from agr_mst_tsuprinstitution2bureau where " +
              " institution_gid='" + institution_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getinstitutionbureau_list = new List<institutionbureau_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getinstitutionbureau_list.Add(new institutionbureau_list
                    {
                        institution2bureau_gid = (dr_datarow["institution2bureau_gid"].ToString()),
                        bureauname_name = (dr_datarow["bureauname_name"].ToString()),
                        bureau_score = (dr_datarow["bureau_score"].ToString()),
                        bureauscore_date = (dr_datarow["bureauscore_date"].ToString()),
                    });
                }
            }
            values.institutionbureau_list = getinstitutionbureau_list;
            dt_datatable.Dispose();
        }

        public void DaDeleteInstitutionBureau(string institution2bureau_gid, MdlInstitutionBureau values)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2bureau where institution2bureau_gid='" + institution2bureau_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Bureau Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public bool DaUpdateCICUploadInstitution(string employee_gid, MdlCICInstitution values)
        {

         
            try
            {
                var bureauscore_date = Convert.ToDateTime(values.bureauscore_date).ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = "Kindly enter the valid date format";
                return false;
            }

            msSQL = " update agr_mst_tsuprinstitution2bureau set " +
                       " bureauname_gid='" + values.bureauname_gid + "'," +
                       " bureauname_name='" + values.bureauname_name + "'," +
                       " bureau_score='" + values.bureau_score + "',";

            if ((values.bureauscore_date == null) || (values.bureauscore_date == ""))
            {
                msSQL += "bureauscore_date=null,";
            }
            else if (Convert.ToDateTime(values.bureauscore_date).ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {

            }
            else
            {
                msSQL += " bureauscore_date='" + Convert.ToDateTime(values.bureauscore_date).ToString("yyyy-MM-dd 00:00:00") + "',";
            }

            msSQL += " observations='" + values.observations.Replace("'", "") + "'," +
                       " bureau_response='" + values.bureau_response.Replace("'", "") + "'," +
                       " updated_by='" + employee_gid + "'," +
                       " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                       " where institution2bureau_gid='" + values.institution2bureau_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                msSQL = "update agr_mst_tsuprinstitution2cicdocumentupload set institution_gid='" + values.institution_gid + "' where institution_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "update agr_mst_tsuprinstitution2cicdocumentupload set institution2bureau_gid='" + values.institution2bureau_gid + "' where institution2bureau_gid='" + employee_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Bureau Details Updated for Institution Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }

        }

        public void DaGetIndividualBureauTempClear(string employee_gid, result values)
        {
            msSQL = "delete from agr_mst_tsuprindividual2cicdocumentupload where contact2bureau_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "delete from agr_mst_tsuprcontact2tuhighriskalert where contact2bureau_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaGetInstitutionBureauTempClear(string employee_gid, result values)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2cicdocumentupload where institution2bureau_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            values.status = true;
        }

        public void DaDeleteGroup(string group_gid, string employee_gid, MdlMstGroup values)
        {
            msSQL = "Delete from agr_mst_tsuprgroup where group_gid='" + group_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                msSQL = "select application_gid from tmp_application where employee_gid='" + employee_gid + "'";
                lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " select group_gid,group_name,date_of_formation,group_status," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from agr_mst_tsuprgroup a " +
                    " left join hrm_mst_temployee b on b.employee_gid=a.created_by " +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " where a.application_gid='" + lsapplication_gid + "' order by group_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getgroupList = new List<group_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getgroupList.Add(new group_list
                        {
                            group_gid = dt["group_gid"].ToString(),
                            group_name = dt["group_name"].ToString(),
                            date_of_formation = dt["date_of_formation"].ToString(),
                            group_status = dt["group_status"].ToString(),
                            created_date = dt["created_date"].ToString(),
                            created_by = dt["created_by"].ToString()
                        });

                    }
                }
                values.group_list = getgroupList;
                dt_datatable.Dispose();
                values.message = "Group Information Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while Deleting";
                values.status = false;
            }

        }

        public void DaGetApprovalHierarchyFlag(string employee_gid, string application_gid, MdlApprovalHierarchy values)
        {
            msSQL = " select vertical_gid from agr_mst_tsuprapplication where application_gid= '" + application_gid + "'";
            string vertical_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select distinct c.cluster_name,c.employee_name as clusterhead,c.employee_gid as clustermanager_gid, " +
                    " e.region_name,e.employee_name as regionhead,e.employee_gid as regionalhead_gid, " +
                    " g.zonal_name,g.employee_name as zonalhead, g.employee_gid as zonalhead_gid, " +
                    " h.employee_name as businesshead, h.employee_gid as businesshead_gid from hrm_mst_temployee a " +
                    " left join sys_mst_tcluster2baselocation b on b.baselocation_gid = a.baselocation_gid " +
                    " left join sys_mst_tclusterhead c on b.cluster_gid = c.cluster_gid " +
                    " left join sys_mst_tregion2cluster d on c.cluster_gid = d.cluster_gid " +
                    " left join sys_mst_tregionhead e on d.region_gid = e.region_gid " +
                    " left join sys_mst_tzone2region f on f.region_gid = d.region_gid " +
                    " left join sys_mst_tzonalhead g on f.zone_gid = g.zonal_gid " +
                    " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid where a.employee_gid = '" + employee_gid + "' and " +
                    " c.vertical_gid = '" + vertical_gid + "'" +
                    " and e.vertical_gid = '" + vertical_gid + "' and " +
                    " g.vertical_gid = '" + vertical_gid + "' and h.vertical_gid = '" + vertical_gid + "' " +
                    " and e.status = 'Y' and g.status = 'Y' and h.status = 'Y' and c.status = 'Y'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsrmclustermanager_gid = objODBCDatareader["clustermanager_gid"].ToString();
                lsrmregionalhead_gid = objODBCDatareader["regionalhead_gid"].ToString();
                lsrmzonalhead_gid = objODBCDatareader["zonalhead_gid"].ToString();
                lsrmbusinesshead_gid = objODBCDatareader["businesshead_gid"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select clustermanager_gid, zonalhead_gid, regionalhead_gid, businesshead_gid from agr_mst_tsuprapplication " +
                    " where application_gid= '" + application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsappclustermanager_gid = objODBCDatareader["clustermanager_gid"].ToString();
                lsappregionalhead_gid = objODBCDatareader["regionalhead_gid"].ToString();
                lsappzonalhead_gid = objODBCDatareader["zonalhead_gid"].ToString();
                lsappbusinesshead_gid = objODBCDatareader["businesshead_gid"].ToString();
            }
            objODBCDatareader.Close();

            if (lsrmclustermanager_gid == lsappclustermanager_gid && lsrmregionalhead_gid == lsappregionalhead_gid && lsrmzonalhead_gid == lsappzonalhead_gid && lsrmbusinesshead_gid == lsappbusinesshead_gid)
            {
                values.lshierarchychange_flag = "N";
                values.status = true;
            }
            else
            {
                values.lshierarchychange_flag = "Y";
                values.status = true;
            }
            msSQL = " select hierarchyupdated_flag from agr_mst_tsuprapplication " +
                   " where application_gid= '" + application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.hierarchyupdated_flag = objODBCDatareader["hierarchyupdated_flag"].ToString();
            }
            objODBCDatareader.Close();

        }

        public void DaGetApprovalHierarchyChangeList(string employee_gid, string application_gid, MdlApprovalHierarchyChange values)
        {
            msSQL = " select vertical_gid from agr_mst_tsuprapplication where application_gid= '" + application_gid + "'";
            string vertical_gid = objdbconn.GetExecuteScalar(msSQL);

            msSQL = " select distinct c.cluster_name,c.employee_name as clustermanager_name,c.employee_gid as clustermanager_gid, " +
                    " e.region_name,e.employee_name as regionhead_name,e.employee_gid as regionalhead_gid, " +
                    " g.zonal_name,g.employee_name as zonalhead_name, g.employee_gid as zonalhead_gid, " +
                    " h.employee_name as businesshead_name, h.employee_gid as businesshead_gid from hrm_mst_temployee a " +
                    " left join sys_mst_tcluster2baselocation b on b.baselocation_gid = a.baselocation_gid " +
                    " left join sys_mst_tclusterhead c on b.cluster_gid = c.cluster_gid " +
                    " left join sys_mst_tregion2cluster d on c.cluster_gid = d.cluster_gid " +
                    " left join sys_mst_tregionhead e on d.region_gid = e.region_gid " +
                    " left join sys_mst_tzone2region f on f.region_gid = d.region_gid " +
                    " left join sys_mst_tzonalhead g on f.zone_gid = g.zonal_gid " +
                    " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid where a.employee_gid = '" + employee_gid + "' and " +
                    " c.vertical_gid = '" + vertical_gid + "'" +
                    " and e.vertical_gid = '" + vertical_gid + "' and " +
                    " g.vertical_gid = '" + vertical_gid + "' and h.vertical_gid = '" + vertical_gid + "' " +
                    " and e.status = 'Y' and g.status = 'Y' and h.status = 'Y' and c.status = 'Y'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.clustermanager_gid = objODBCDatareader["clustermanager_gid"].ToString();
                values.clustermanager_name = objODBCDatareader["clustermanager_name"].ToString();
                values.regionalhead_gid = objODBCDatareader["regionalhead_gid"].ToString();
                values.regionhead_name = objODBCDatareader["regionhead_name"].ToString();
                values.zonalhead_gid = objODBCDatareader["zonalhead_gid"].ToString();
                values.zonalhead_name = objODBCDatareader["zonalhead_name"].ToString();
                values.businesshead_gid = objODBCDatareader["businesshead_gid"].ToString();
                values.businesshead_name = objODBCDatareader["businesshead_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select a.employeereporting_to,concat(c.user_firstname,' ',c.user_lastname,' || ',c.user_code) as rm_name,b.employee_gid, " +
                    "  concat( g.user_firstname, ' ', g.user_lastname, ' || ', g.user_code) as directreportingto_name  " +
                    "  from adm_mst_tmodule2employee a " +
                    "  left join hrm_mst_temployee b on b.employee_gid = a.employee_gid " +
                    "  left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                    "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                    "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                 "  where a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in " +
                         "  (select modulereportingto_gid from adm_mst_tcompany)) " +
                         " and c.user_status = 'Y' and b.employee_gid ='" + employee_gid + "'  group by a.employee_gid ";
            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader1.HasRows == true)
            {
                values.rm_name = objODBCDatareader1["rm_name"].ToString();
                values.directreportingto_name = objODBCDatareader1["directreportingto_name"].ToString();
            }
            objODBCDatareader.Close();

        }

        public void DaUpdateApprovalHierarchyChange(string employee_gid, MdlMstUpdateApproval values)
        {
            string vertical_gid = "", program_gid = "";
            msSQL = " select vertical_gid,program_gid from agr_mst_tsuprapplication where application_gid= '" + values.application_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                vertical_gid = objODBCDatareader["vertical_gid"].ToString();
                program_gid = objODBCDatareader["program_gid"].ToString();
            }
            objODBCDatareader.Close();

            string lsclusterhead, lsregionalhead, lszonalhead, lsbusinesshead;
            string lsclusterheadgid, lsregionalheadgid, lszonalheadgid, lsbusinessheadgid;
            string lsbaselocationgid, lsclustergid, lsregiongid, lszonalgid;
            string lsbaselocationname, lsclustername, lsregionname, lszonalname;

            msSQL = " select concat( g.user_firstname, ' ', g.user_lastname, ' / ', g.user_code) as level_one ,a.employeereporting_to " +
                               "  from adm_mst_tmodule2employee a " +
                               "  left join hrm_mst_temployee f on a.employeereporting_to = f.employee_gid " +
                               "  left join adm_mst_tuser g on g.user_gid = f.user_gid  " +
                                "  where a.module_gid in  (select module_gid_parent from adm_mst_tmodule where module_gid in " +
                                "  (select modulereportingto_gid from adm_mst_tcompany)) " +
                    " and g.user_status = 'Y' and a.employee_gid ='" + employee_gid + "' group by a.employee_gid  ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsdrm_gid = objODBCDatareader["employeereporting_to"].ToString();
                lsdrm_name = objODBCDatareader["level_one"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select distinct b.baselocation_gid,b.baselocation_name,c.cluster_gid,c.cluster_name, " +
                        " c.employee_name as clusterhead,c.vertical_name as clustervertical," +
                        " e.region_gid,e.region_name,e.employee_name as regionhead,g.zonal_gid,g.zonal_name,g.employee_name as zonalhead ," +
                        " c.employee_gid as clusterhead_gid,e.employee_gid as regionhead_gid, " +
                        " g.employee_gid as zonalhead_gid,h.employee_gid as businesshead_gid, " +
                        " h.employee_name as businesshead from hrm_mst_temployee a" +
                        " left join sys_mst_tcluster2baselocation b on b.baselocation_gid = a.baselocation_gid" +
                        " left  join sys_mst_tclusterhead c on b.cluster_gid = c.cluster_gid" +
                        " left join sys_mst_tregion2cluster d on c.cluster_gid = d.cluster_gid" +
                        " left join sys_mst_tregionhead e on d.region_gid = e.region_gid" +
                        " left join sys_mst_tzone2region f on f.region_gid = d.region_gid" +
                        " left join sys_mst_tzonalhead g on f.zone_gid = g.zonal_gid" +
                        " left join sys_mst_tbusinesshead h on h.zone_gid = g.zonal_gid where a.employee_gid = '" + employee_gid + "' and" +
                        " c.vertical_gid = '" + vertical_gid + "'" +
                        " and e.vertical_gid = '" + vertical_gid + "' and " +
                        " g.vertical_gid = '" + vertical_gid + "' and h.vertical_gid = '" + vertical_gid + "'" +
                        " and c.program_gid = '" + program_gid + "' and e.program_gid = '" + program_gid + "' and " +
                        " g.program_gid = '" + program_gid + "' and h.program_gid = '" + program_gid + "' " +
                        " and e.status='Y' and g.status='Y' and h.status='Y' and c.status='Y'";
            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader1.HasRows == true)
            {
                lsclusterhead = objODBCDatareader1["clusterhead"].ToString();
                lsregionalhead = objODBCDatareader1["regionhead"].ToString();
                lszonalhead = objODBCDatareader1["zonalhead"].ToString();
                lsbusinesshead = objODBCDatareader1["businesshead"].ToString();
                lsclusterheadgid = objODBCDatareader1["clusterhead_gid"].ToString();
                lsregionalheadgid = objODBCDatareader1["regionhead_gid"].ToString();
                lszonalheadgid = objODBCDatareader1["zonalhead_gid"].ToString();
                lsbusinessheadgid = objODBCDatareader1["businesshead_gid"].ToString();
                lsbaselocationgid = objODBCDatareader1["baselocation_gid"].ToString();
                lsbaselocationname = objODBCDatareader1["baselocation_name"].ToString();
                lsclustergid = objODBCDatareader1["cluster_gid"].ToString();
                lsclustername = objODBCDatareader1["cluster_name"].ToString();
                lsregiongid = objODBCDatareader1["region_gid"].ToString();
                lsregionname = objODBCDatareader1["region_name"].ToString();
                lszonalgid = objODBCDatareader1["zonal_gid"].ToString();
                lszonalname = objODBCDatareader1["zonal_name"].ToString();

                msSQL = " update agr_mst_tsuprapplication set " +
                    " drm_gid='" + lsdrm_gid + "'," +
                   " drm_name='" + lsdrm_name + "'," +
                   " clustermanager_gid='" + lsclusterheadgid + "'," +
                   " clustermanager_name='" + lsclusterhead + "'," +
                   " zonalhead_gid='" + lszonalheadgid + "'," +
                   " zonalhead_name='" + lszonalhead + "'," +
                   " regionalhead_gid='" + lsregionalheadgid + "'," +
                   " regionalhead_name='" + lsregionalhead + "'," +
                   " businesshead_gid='" + lsbusinessheadgid + "'," +
                   " businesshead_name='" + lsbusinesshead + "'," +
                   " hierarchyupdated_flag='Y'," +
                   " baselocation_gid='" + lsbaselocationgid + "'," +
                   " baselocation_name='" + lsbaselocationname + "'," +
                   " cluster_gid='" + lsclustergid + "'," +
                   " cluster_name='" + lsclustername + "'," +
                   " region_gid='" + lsregiongid + "'," +
                   " region_name='" + lsregionname + "'," +
                   " zone_gid='" + lszonalgid + "'," +
                   " zone_name='" + lszonalname + "'," +
                   " updated_by='" + employee_gid + "'," +
                   " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                   " where application_gid='" + values.application_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            objODBCDatareader1.Close();
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Hierarchy confirmed, Click Proceed to Approval..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating Hierarchy....!";
            }

        }

        public void DaGetSectorcategory(string employee_gid, string product_gid, MdlSectorcategory values)
        {
            msSQL = " SELECT businessunit_gid,businessunit_name,valuechain_gid,valuechain_name FROM ocs_mst_tproducts a " +
                    " where product_gid = '" + product_gid + "' order by a.product_gid desc ";

            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader1.HasRows == true)
            {
                values.businessunit_gid = objODBCDatareader1["businessunit_gid"].ToString();
                values.businessunit_name = objODBCDatareader1["businessunit_name"].ToString();
                values.valuechain_gid = objODBCDatareader1["valuechain_gid"].ToString();
                values.valuechain_name = objODBCDatareader1["valuechain_name"].ToString();
            }
            objODBCDatareader1.Close();

            // Variety 
            msSQL = " SELECT variety_gid,variety_name FROM ocs_mst_tvariety a" +
                       " where product_gid='" + product_gid + "' order by a.product_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getvarietyname_list = new List<varietyname_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getvarietyname_list.Add(new varietyname_list
                    {
                        variety_gid = (dr_datarow["variety_gid"].ToString()),
                        variety_name = (dr_datarow["variety_name"].ToString()),
                    });
                }
                values.varietyname_list = getvarietyname_list;
            }
            dt_datatable.Dispose();

        }

        public void DaGetVarietyDtl(string employee_gid, string variety_gid, MdlSectorcategory values)
        {
            msSQL = " SELECT product_gid,variety_gid,variety_name,botanical_name,alternative_name,hsn_code FROM ocs_mst_tvariety a " +
                   " where variety_gid = '" + variety_gid + "'";

            objODBCDatareader1 = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader1.HasRows == true)
            {
                values.product_gid = objODBCDatareader1["product_gid"].ToString();
                values.variety_gid = objODBCDatareader1["variety_gid"].ToString();
                values.variety_name = objODBCDatareader1["variety_name"].ToString();
                values.botanical_name = objODBCDatareader1["botanical_name"].ToString();
                values.alternative_name = objODBCDatareader1["alternative_name"].ToString();
                values.hsn_code = objODBCDatareader1["hsn_code"].ToString();
            }
            objODBCDatareader1.Close();

        }

        public bool DaPANForm60DocumentUpload(HttpRequest httpRequest, uploaddocument objfilename, string employee_gid)
        {
            upload_list objdocumentmodel = new upload_list();
            HttpFileCollection httpFileCollection;
            string lsfilepath = string.Empty;
            string lsdocument_gid = string.Empty;
            MemoryStream ms_stream = new MemoryStream();
            string document_gid = string.Empty;
            string lscompany_code = string.Empty;
            string project_flag = httpRequest.Form["project_flag"].ToString();

            String path = lspath;

            msSQL = " SELECT a.company_code FROM adm_mst_tcompany a ";
            lscompany_code = objdbconn.GetExecuteScalar(msSQL);
            path = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PANForm60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month;
            {
                if ((!System.IO.Directory.Exists(path)))
                    System.IO.Directory.CreateDirectory(path);
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    string lsfirstdocument_filepath = string.Empty;
                    httpFileCollection = httpRequest.Files;
                    for (int i = 0; i < httpFileCollection.Count; i++)
                    {
                        string msdocument_gid = objcmnfunctions.GetMasterGID("UPLF");
                        httpPostedFile = httpFileCollection[i];
                        string FileExtension = httpPostedFile.FileName;
                        string lsfile_gid = msdocument_gid;
                        FileExtension = Path.GetExtension(FileExtension).ToLower();
                        lsfile_gid = lsfile_gid + FileExtension;
                        Stream ls_readStream;
                        ls_readStream = httpPostedFile.InputStream;
                        MemoryStream ms = new MemoryStream();
                        ls_readStream.CopyTo(ms);
                        // Check Document validation;

                        byte[] bytes = ms.ToArray();
                        if ((objcmnstorage.CheckIsValidfilename(FileExtension, project_flag) == false) || (objcmnstorage.CheckIsExecutable(bytes) == true))
                        {
                            objfilename.message = "File format is not supported";
                            return false;
                        }
                        lspath = ConfigurationManager.AppSettings["file_path"] + "/erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PANForm60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        bool status;
                        status = objcmnstorage.UploadStream("erpdocument", lscompany_code + "/" + "SamAgro/PANForm60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + msdocument_gid + FileExtension, ms);
                        ms.Close();
                        lspath = "erpdocument" + "/" + lscompany_code + "/" + "SamAgro/PANForm60Document/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";

                        msGetGid = objcmnfunctions.GetMasterGID("CF60");
                        msSQL = " insert into agr_mst_tsuprcontact2panform60(" +
                                " contact2panform60_gid," +
                                " contact_gid," +
                                " document_name," +
                                " document_path," +
                                " created_by," +
                                " created_date)" +
                                " values(" +
                                "'" + msGetGid + "'," +
                                "'" + employee_gid + "'," +
                                "'" + httpPostedFile.FileName + "'," +
                                "'" + lspath + msdocument_gid + FileExtension + "'," +
                                "'" + employee_gid + "'," +
                                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        if (mnResult == 1)
                        {
                            objfilename.status = true;
                            objfilename.message = "Document Uploaded Successfully..!";
                        }
                        else
                        {
                            objfilename.status = false;
                            objfilename.message = "Error Occured..!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objfilename.message = ex.ToString();
            }
            return true;
        }

        public void DaPANForm60Delete(string contact2panform60_gid, MdlContactPANForm60 values)
        {
            msSQL = "delete from agr_mst_tsuprcontact2panform60 where contact2panform60_gid='" + contact2panform60_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Form-60 Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaGetPANForm60List(string employee_gid, MdlContactPANForm60 values)
        {
            msSQL = "select contact2panform60_gid,document_name, document_path from agr_mst_tsuprcontact2panform60 where " +
              " contact_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcontactpanform60_list = new List<contactpanform60_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getcontactpanform60_list.Add(new contactpanform60_list
                    {
                        contact2panform60_gid = (dr_datarow["contact2panform60_gid"].ToString()),
                        document_name = (dr_datarow["document_name"].ToString()),
                        //document_path = (dr_datarow["document_path"].ToString()),
                        document_path = objcmnstorage.EncryptData((dr_datarow["document_path"].ToString())),
                    });

                    values.contactpanform60_list = getcontactpanform60_list;
                }
                dt_datatable.Dispose();
            }

        }

        public void DaPANAbsenceReasonList(MdlPANAbsenceReason objMdlPANAbsenceReason)
        {
            try
            {
                msSQL = " SELECT panabsencereason" +
                   " from agr_mst_tsuprpanabsencereason";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getpanabsencereason_list = new List<panabsencereason_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objMdlPANAbsenceReason.panabsencereason_list = dt_datatable.AsEnumerable().Select(row =>
                      new panabsencereason_list
                      {
                          panabsencereason = row["panabsencereason"].ToString(),
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objMdlPANAbsenceReason.status = true;
            }
            catch (Exception ex)
            {
                objMdlPANAbsenceReason.status = false;
            }

        }

        public void DaPostPANAbsenceReasons(MdlPANAbsenceReason values, string employee_gid)
        {
            foreach (string reason in values.panabsencereason_selectedlist)
            {

                msGetGid = objcmnfunctions.GetMasterGID("C2PR");
                msSQL = " INSERT INTO agr_mst_tsuprcontact2panabsencereason(" +
                        " contact2panabsencereason_gid," +
                        " contact_gid," +
                        " panabsencereason," +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetGid + "'," +
                        "'" + employee_gid + "'," +
                        "'" + reason + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + employee_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            }

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "PAN Absence Reasons submitted successfully...";
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
            }
        }

        public void DaPANReasonsCheck(MdlPANAbsenceReason objMdlPANAbsenceReason, string employee_gid)
        {
            try
            {
                msSQL = " SELECT count(panabsencereason)" +
                   " from agr_mst_tsuprcontact2panabsencereason" +
                   " where contact_gid='" + employee_gid + "'";

                string lspanabsencereason_count = objdbconn.GetExecuteScalar(msSQL);

                if (int.Parse(lspanabsencereason_count) > 0)
                {
                    objMdlPANAbsenceReason.status = true;
                }
                else
                {
                    objMdlPANAbsenceReason.status = false;
                }
            }
            catch (Exception ex)
            {
                objMdlPANAbsenceReason.status = false;
            }
        }

        public bool DaPostProductDetailAdd(string employee_gid, MdlMstProductDetailAdd values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AP2P");
            msSQL = " insert into agr_mst_tsuprapplication2product (" +
                    " application2product_gid," +
                    " application2loan_gid," +
                    " application_gid," +
                    " product_gid," +
                    " product_name," +
                    " variety_gid," +
                    " variety_name," +
                    " sector_name," +
                    " category_name," +
                    " botanical_name," +
                    " alternative_name," +
                    " hsn_code, " +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "null," +
                    "'" + employee_gid + "'," +
                    "'" + values.product_gid + "'," +
                    "'" + values.product_name + "'," +
                    "'" + values.variety_gid + "'," +
                    "'" + values.variety_name + "'," +
                    "'" + values.sector_name + "'," +
                    "'" + values.category_name + "'," +
                    "'" + values.botanical_name + "'," +
                    "'" + values.alternative_name + "'," +
                    "'" + values.hsn_code + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Product Details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }

        public void DaGetProductDetailList(string employee_gid, MdlMstProductDetailList values)
        {
            msSQL = " select application2product_gid,product_gid,product_name,variety_gid,variety_name,sector_name,category_name,hsn_code, " +
                    " botanical_name,alternative_name from agr_mst_tsuprapplication2product where application_gid='" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstproduct_list = new List<mstproduct_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstproduct_list.Add(new mstproduct_list
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
                        hsn_code = (dr_datarow["hsn_code"].ToString())
                    });
                }
                values.mstproduct_list = getmstproduct_list;
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteProductDetail(string application2product_gid, MdlMstProductDetailAdd values, string employee_gid)
        {
            msSQL = "delete from agr_mst_tsuprapplication2product where application2product_gid='" + application2product_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.message = "Product Details are Deleted Successfully";
                values.status = true;
                msSQL = " select application2product_gid,product_gid,product_name,variety_gid,variety_name,sector_name,category_name,hsn_code," +
                        " botanical_name,alternative_name,application2loan_gid,unitpricevalue_commodity, natureformstate_commodity,qualityof_commodity, " +
                        " quantity,uom_name " +
                        " from agr_mst_tsuprapplication2product where application2loan_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstproduct_list = new List<mstproduct_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstproduct_list.Add(new mstproduct_list
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
                            application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                            hsn_code = (dr_datarow["hsn_code"].ToString()),
                            unitpricevalue_commodity = (dr_datarow["unitpricevalue_commodity"].ToString()),
                            natureformstate_commodity = (dr_datarow["natureformstate_commodity"].ToString()),
                            qualityof_commodity = (dr_datarow["qualityof_commodity"].ToString()),
                            quantity = (dr_datarow["quantity"].ToString()),
                            uom_name = (dr_datarow["uom_name"].ToString()),
                        });
                    }
                    values.mstproduct_list = getmstproduct_list;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaPostProduct(string employee_gid, MdlMstProductDetailAdd values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AP2P");
            msSQL = " insert into agr_mst_tsuprapplication2product (" +
                    " application2product_gid," +
                    " application2loan_gid," +
                    " application_gid," +
                    " product_gid," +
                    " product_name," +
                    " variety_gid," +
                    " variety_name," +
                    " sector_name," +
                    " category_name," +
                    " botanical_name," +
                    " alternative_name," +
                    " hsn_code," +
                    " unitpricevalue_commodity, " +
                    " natureformstate_commoditygid, " +
                    " natureformstate_commodity," +
                    " qualityof_commodity," +
                    " quantity," +
                    " uom_gid," +
                    " uom_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + employee_gid + "'," +
                    "null," +
                    "'" + values.product_gid + "'," +
                    "'" + values.product_name + "'," +
                    "'" + values.variety_gid + "'," +
                    "'" + values.variety_name + "'," +
                    "'" + values.sector_name + "'," +
                    "'" + values.category_name + "'," +
                    "'" + values.botanical_name + "'," +
                    "'" + values.alternative_name + "'," +
                    "'" + values.hsn_code + "'," +
                    "'" + values.unitpricevalue_commodity + "'," +
                    "'" + values.natureformstate_commoditygid + "'," +
                    "'" + values.natureformstate_commodity + "'," +
                    "'" + values.qualityof_commodity + "'," +
                    "'" + values.quantity + "'," +
                    "'" + values.uom_gid + "'," +
                    "'" + values.uom_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Commodity Details are Added Successfully";

                msSQL = " select application2product_gid,product_gid,product_name,variety_gid,variety_name,sector_name,category_name,hsn_code, " +
                       " botanical_name,alternative_name,application2loan_gid,unitpricevalue_commodity,natureformstate_commoditygid, " +
                       " natureformstate_commodity,qualityof_commodity, " +
                       " quantity,uom_gid,uom_name " +
                       " from agr_mst_tsuprapplication2product where application2loan_gid='" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmstproduct_list = new List<mstproduct_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmstproduct_list.Add(new mstproduct_list
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
                            application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                            hsn_code = (dr_datarow["hsn_code"].ToString()),
                            unitpricevalue_commodity = (dr_datarow["unitpricevalue_commodity"].ToString()),
                            natureformstate_commoditygid = (dr_datarow["natureformstate_commoditygid"].ToString()),
                            natureformstate_commodity = (dr_datarow["natureformstate_commodity"].ToString()),
                            qualityof_commodity = (dr_datarow["qualityof_commodity"].ToString()),
                            quantity = (dr_datarow["quantity"].ToString()),
                            uom_gid = (dr_datarow["uom_gid"].ToString()),
                            uom_name = (dr_datarow["uom_name"].ToString()),
                        });
                    }
                    values.mstproduct_list = getmstproduct_list;
                }
                dt_datatable.Dispose();
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Adding Commodity";
            }
        }

        public void DaGetProductDtlList(string application2loan_gid, string employee_gid, MdlMstProductDetailList values)
        {
            msSQL = " select application2product_gid,product_gid,product_name,variety_gid,variety_name,sector_name,category_name,hsn_code, " +
                    " botanical_name,alternative_name ,unitpricevalue_commodity,natureformstate_commodity,qualityof_commodity, " +
                    " quantity,uom_gid,uom_name " +
                    " from agr_mst_tsuprapplication2product " +
                    " where application2loan_gid='" + employee_gid + "' or application2loan_gid='" + application2loan_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getmstproduct_list = new List<mstproduct_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getmstproduct_list.Add(new mstproduct_list
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
                    });
                }
                values.mstproduct_list = getmstproduct_list;
            }
            dt_datatable.Dispose();
        }

        public bool DaPostProductDtlAdd(string employee_gid, MdlMstProductDetailAdd values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AP2P");
            msSQL = " insert into agr_mst_tsuprapplication2product (" +
                    " application2product_gid," +
                    " application2loan_gid," +
                    " application_gid," +
                    " product_gid," +
                    " product_name," +
                    " variety_gid," +
                    " variety_name," +
                    " sector_name," +
                    " category_name," +
                    " botanical_name," +
                    " alternative_name," +
                    " hsn_code, " +
                    " unitpricevalue_commodity, " +
                    " natureformstate_commodity," +
                    " qualityof_commodity," +
                    " quantity," +
                    " uom_gid," +
                    " uom_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.application2loan_gid + "'," +
                    "null," +
                    "'" + values.product_gid + "'," +
                    "'" + values.product_name + "'," +
                    "'" + values.variety_gid + "'," +
                    "'" + values.variety_name + "'," +
                    "'" + values.sector_name + "'," +
                    "'" + values.category_name + "'," +
                    "'" + values.botanical_name + "'," +
                    "'" + values.alternative_name + "'," +
                    "'" + values.hsn_code + "'," +
                    "'" + values.unitpricevalue_commodity + "'," +
                    "'" + values.natureformstate_commodity + "'," +
                    "'" + values.qualityof_commodity + "'," +
                    "'" + values.quantity + "'," +
                    "'" + values.uom_gid + "'," +
                    "'" + values.uom_name + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Product Details Added Successfully";
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured";
                return false;
            }
        }


        public void DaGetScopedtl(MdlScopeList values)
        {
            msSQL = "select scope_gid,scope_name from agr_mst_tscope where status='Y' order by scope_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getScopeList = new List<ScopeList>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getScopeList.Add(new ScopeList
                    {
                        scope_gid = (dr_datarow["scope_gid"].ToString()),
                        scope_name = (dr_datarow["scope_name"].ToString()),
                    });
                }
                values.ScopeList = getScopeList;
            }
            dt_datatable.Dispose();
            values.status = true;

        }

        public void DaPostTradedtl(string employee_gid, MdlTradedtl values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("APTR");

            if (values.tmpadd_status == true)
                values.application_gid = employee_gid;

            msSQL = "insert into agr_mst_tsuprapplication2trade(" +
                    " application2trade_gid," +
                    " application2loan_gid, " +
                    " application_gid," +
                    " producttype_gid," +
                    " producttype_name," +
                    " productsubtype_gid," +
                    " productsubtype_name," +
                    " salescontract_availability," +
                    " scopeof_transportgid," +
                    " scopeof_transport," +
                    " scopeof_loadinggid," +
                    " scopeof_loading," +
                    " scopeof_unloadinggid," +
                    " scopeof_unloading," +
                    " scopeof_qualityandquantitygid," +
                    " scopeof_qualityandquantity," +
                    " scopeof_moisturegainlossgid," +
                    " scopeof_moisturegainloss," +
                    " created_by," +
                    " created_date) values(" +
                     "'" + msGetGid + "'," +
                     "'" + values.application2loan_gid + "'," +
                           "'" + values.application_gid + "'," +
                           "'" + values.producttype_gid + "'," +
                           "'" + values.producttype_name + "'," +
                           "'" + values.productsubtype_gid + "'," +
                           "'" + values.productsubtype_name + "'," +
                           "'" + values.salescontract_availability + "'," +
                           "'" + values.scopeof_transportgid + "'," +
                           "'" + values.scopeof_transport + "'," +
                           "'" + values.scopeof_loadinggid + "'," +
                           "'" + values.scopeof_loading + "'," +
                           "'" + values.scopeof_unloadinggid + "'," +
                           "'" + values.scopeof_unloading + "'," +
                           "'" + values.scopeof_qualityandquantitygid + "'," +
                           "'" + values.scopeof_qualityandquantity + "'," +
                           "'" + values.scopeof_moisturegainlossgid + "'," +
                           "'" + values.scopeof_moisturegainloss + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Trade Details are added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        //public void DaGetApplicationTradeList(string application_gid, MdlTradeList values)
        //{
        //    msSQL = " select application2trade_gid,application_gid, producttype_gid,producttype_name,productsubtype_gid," +
        //            " productsubtype_name,salescontract_availability,scopeof_transportgid,scopeof_transport,scopeof_loadinggid," +
        //            " scopeof_loading,scopeof_unloadinggid,scopeof_unloading,scopeof_qualityandquantitygid,scopeof_qualityandquantity, " +
        //            " scopeof_moisturegainlossgid,scopeof_moisturegainloss, " +
        //            " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
        //            " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
        //            " from agr_mst_tsuprapplication2trade a " +
        //            " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
        //            " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
        //            " where a.application_gid = '" + application_gid + "' order by application2trade_gid desc";
        //    dt_datatable = objdbconn.GetDataTable(msSQL);
        //    var getTradedtllist = new List<MdlTradedtl>();
        //    if (dt_datatable.Rows.Count != 0)
        //    {
        //        foreach (DataRow dr_datarow in dt_datatable.Rows)
        //        {
        //            getTradedtllist.Add(new MdlTradedtl
        //            {
        //                application2trade_gid = (dr_datarow["application2trade_gid"].ToString()),
        //                application_gid = (dr_datarow["application_gid"].ToString()),
        //                producttype_gid = (dr_datarow["producttype_gid"].ToString()),
        //                producttype_name = (dr_datarow["producttype_name"].ToString()),
        //                productsubtype_gid = (dr_datarow["productsubtype_gid"].ToString()),
        //                productsubtype_name = (dr_datarow["productsubtype_name"].ToString()),
        //                salescontract_availability = (dr_datarow["salescontract_availability"].ToString()),
        //                scopeof_transportgid = (dr_datarow["scopeof_transportgid"].ToString()),
        //                scopeof_transport = (dr_datarow["scopeof_transport"].ToString()),
        //                scopeof_loadinggid = (dr_datarow["scopeof_loadinggid"].ToString()),
        //                scopeof_loading = (dr_datarow["scopeof_loading"].ToString()),
        //                scopeof_unloadinggid = (dr_datarow["scopeof_unloadinggid"].ToString()),
        //                scopeof_unloading = (dr_datarow["scopeof_unloading"].ToString()),
        //                scopeof_qualityandquantitygid = (dr_datarow["scopeof_qualityandquantitygid"].ToString()),
        //                scopeof_qualityandquantity = (dr_datarow["scopeof_qualityandquantity"].ToString()),
        //                scopeof_moisturegainlossgid = (dr_datarow["scopeof_moisturegainlossgid"].ToString()),
        //                scopeof_moisturegainloss = (dr_datarow["scopeof_moisturegainloss"].ToString()),
        //                created_by = (dr_datarow["created_by"].ToString()),
        //                created_date = (dr_datarow["created_date"].ToString()),
        //            });
        //        }
        //        values.MdlTradedtl = getTradedtllist;
        //    }
        //    dt_datatable.Dispose();
        //}

        public void DaGetApplicationTradeList(string application_gid, string employee_gid,string tmp_status, MdlTradeList values)
        {
            msSQL = " select application2trade_gid,application_gid, producttype_gid,producttype_name,productsubtype_gid," +
                    " productsubtype_name,salescontract_availability,scopeof_transportgid,scopeof_transport,scopeof_loadinggid," +
                    " scopeof_loading,scopeof_unloadinggid,scopeof_unloading,scopeof_qualityandquantitygid,scopeof_qualityandquantity, " +
                    " scopeof_moisturegainlossgid,scopeof_moisturegainloss, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                    " from agr_mst_tsuprapplication2trade a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid";
            if (tmp_status == "true")
                msSQL += " where a.application_gid = '" + employee_gid + "' order by application2trade_gid asc";
            else if (tmp_status == "both")
                msSQL += " where (a.application_gid = '" + employee_gid + "' or a.application_gid = '" + application_gid + "') order by application2trade_gid asc";
            else
                msSQL += " where a.application_gid = '" + application_gid + "' order by application2trade_gid asc"; 
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getTradedtllist = new List<MdlTradedtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getTradedtllist.Add(new MdlTradedtl
                    {
                        application2trade_gid = (dr_datarow["application2trade_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        producttype_gid = (dr_datarow["producttype_gid"].ToString()),
                        producttype_name = (dr_datarow["producttype_name"].ToString()),
                        productsubtype_gid = (dr_datarow["productsubtype_gid"].ToString()),
                        productsubtype_name = (dr_datarow["productsubtype_name"].ToString()),
                        salescontract_availability = (dr_datarow["salescontract_availability"].ToString()),
                        scopeof_transportgid = (dr_datarow["scopeof_transportgid"].ToString()),
                        scopeof_transport = (dr_datarow["scopeof_transport"].ToString()),
                        scopeof_loadinggid = (dr_datarow["scopeof_loadinggid"].ToString()),
                        scopeof_loading = (dr_datarow["scopeof_loading"].ToString()),
                        scopeof_unloadinggid = (dr_datarow["scopeof_unloadinggid"].ToString()),
                        scopeof_unloading = (dr_datarow["scopeof_unloading"].ToString()),
                        scopeof_qualityandquantitygid = (dr_datarow["scopeof_qualityandquantitygid"].ToString()),
                        scopeof_qualityandquantity = (dr_datarow["scopeof_qualityandquantity"].ToString()),
                        scopeof_moisturegainlossgid = (dr_datarow["scopeof_moisturegainlossgid"].ToString()),
                        scopeof_moisturegainloss = (dr_datarow["scopeof_moisturegainloss"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                    });
                }
                values.MdlTradedtl = getTradedtllist;
            }
            dt_datatable.Dispose();
        }

        public void DaGetApplicationTradeViewdtl(string application2trade_gid, MdlTradedtl values)
        {
            msSQL = " select application2trade_gid,application_gid, producttype_gid,producttype_name,productsubtype_gid," +
                    " productsubtype_name,salescontract_availability,scopeof_transportgid,scopeof_transport,scopeof_loadinggid," +
                    " scopeof_loading,scopeof_unloadinggid,scopeof_unloading,scopeof_qualityandquantitygid,scopeof_qualityandquantity, " +
                    " scopeof_moisturegainlossgid,scopeof_moisturegainloss, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by " +
                    " from agr_mst_tsuprapplication2trade a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid" +
                    " where a.application2trade_gid = '" + application2trade_gid + "' order by application2trade_gid desc";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.application2trade_gid = (objODBCDatareader["application2trade_gid"].ToString());
                values.application_gid = (objODBCDatareader["application_gid"].ToString());
                values.producttype_gid = (objODBCDatareader["producttype_gid"].ToString());
                values.producttype_name = (objODBCDatareader["producttype_name"].ToString());
                values.productsubtype_gid = (objODBCDatareader["productsubtype_gid"].ToString());
                values.productsubtype_name = (objODBCDatareader["productsubtype_name"].ToString());
                values.salescontract_availability = (objODBCDatareader["salescontract_availability"].ToString());
                values.scopeof_transportgid = (objODBCDatareader["scopeof_transportgid"].ToString());
                values.scopeof_transport = (objODBCDatareader["scopeof_transport"].ToString());
                values.scopeof_loadinggid = (objODBCDatareader["scopeof_loadinggid"].ToString());
                values.scopeof_loading = (objODBCDatareader["scopeof_loading"].ToString());
                values.scopeof_unloadinggid = (objODBCDatareader["scopeof_unloadinggid"].ToString());
                values.scopeof_unloading = (objODBCDatareader["scopeof_unloading"].ToString());
                values.scopeof_qualityandquantitygid = (objODBCDatareader["scopeof_qualityandquantitygid"].ToString());
                values.scopeof_qualityandquantity = (objODBCDatareader["scopeof_qualityandquantity"].ToString());
                values.scopeof_moisturegainlossgid = (objODBCDatareader["scopeof_moisturegainlossgid"].ToString());
                values.scopeof_moisturegainloss = (objODBCDatareader["scopeof_moisturegainloss"].ToString());
                values.created_by = (objODBCDatareader["created_by"].ToString());
                values.created_date = (objODBCDatareader["created_date"].ToString());
            }
            objODBCDatareader.Close();
        }

        public void DaUpdateTradeDtl(string employee_gid, MdlTradedtl values)
        {
            try
            {
                msSQL = " update agr_mst_tsuprapplication2trade set " +
                         //" producttype_gid='" + values.producttype_gid + "'," +
                         //" producttype_name='" + values.producttype_name + "'," +
                         //" productsubtype_gid='" + values.productsubtype_gid + "'," +
                         //" productsubtype_name='" + values.productsubtype_name + "'," +
                         " salescontract_availability='" + values.salescontract_availability + "'," +
                         " scopeof_transportgid='" + values.scopeof_transportgid + "'," +
                         " scopeof_transport='" + values.scopeof_transport + "'," +
                         " scopeof_loadinggid='" + values.scopeof_loadinggid + "'," +
                         " scopeof_loading='" + values.scopeof_loading + "'," +
                         " scopeof_unloadinggid='" + values.scopeof_unloadinggid + "'," +
                         " scopeof_unloading='" + values.scopeof_unloading + "'," +
                         " scopeof_qualityandquantitygid='" + values.scopeof_qualityandquantitygid + "'," +
                         " scopeof_qualityandquantity='" + values.scopeof_qualityandquantity + "'," +
                         " scopeof_moisturegainlossgid='" + values.scopeof_moisturegainlossgid + "'," +
                         " scopeof_moisturegainloss='" + values.scopeof_moisturegainloss + "'," +
                         " updated_by='" + employee_gid + "'," +
                         " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                         " where application2trade_gid='" + values.application2trade_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult == 1)
                {
                    values.status = true;
                    values.message = "Trade Details are Updated Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..";
                }
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.ToString();
            }
        }

        public void DaDeleteTradeDtl(string application2trade_gid, result values)
        {
            msSQL = "delete from agr_mst_tsuprapplication2trade where application2trade_gid='" + application2trade_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Trade Details are Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaTradeTmpClear(string employee_gid)
        {
            msSQL = "delete from agr_mst_tsuprapplication2trade where application_gid='" + employee_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL); 
        }

        public void DaGetTradeproduct(string application_gid, MdlList values)
        {

            msSQL = " select application2loan_gid,producttype_gid,product_type,productsubtype_gid,productsub_type " +
                    " from agr_mst_tsuprapplication2loan where application_gid='" + application_gid + "'" +
                    " and application2loan_gid not in (select application2loan_gid from agr_mst_tsuprapplication2trade " +
                    " where application_gid='" + application_gid + "' and application2loan_gid is not null)";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getSegment = new List<product_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getSegment.Add(new product_list
                    {
                        application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                        producttype_gid = (dr_datarow["producttype_gid"].ToString()),
                        product_type = (dr_datarow["product_type"].ToString()),
                        productsubtype_gid = (dr_datarow["productsubtype_gid"].ToString()),
                        productsub_type = (dr_datarow["productsub_type"].ToString()),
                    });
                }
                values.product_list = getSegment;
            }
            dt_datatable.Dispose();
            values.status = true;

        }

        public void DaPostRatingdtl(string employee_gid, MdlRatingdtl values)
        {
            msGetGid = objcmnfunctions.GetMasterGID("INRD");

            if (values.tmpadd_status == true)
                values.institution_gid = employee_gid;

            msSQL = " insert into agr_mst_tsuprinstitution2ratingdetail(" +
                    " institution2ratingdetail_gid," +
                    " institution_gid," +
                    " application_gid," +
                    " creditrating_agencygid," +
                    " creditrating_agencyname," +
                    " creditrating_gid," +
                    " creditrating_name," +
                    " assessed_on," +
                    " creditrating_link," +
                    " created_by," +
                    " created_date) values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.institution_gid + "'," +
                    "'" + values.application_gid + "'," +
                    "'" + values.creditrating_agencygid + "'," +
                    "'" + values.creditrating_agencyname + "'," +
                    "'" + values.creditrating_gid + "'," +
                    "'" + values.creditrating_name + "'," +
                    "'" + Convert.ToDateTime(values.assessed_on).ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "'" + values.creditrating_link.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Rating Details are added Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
            }
        }

        public void DaGetInstitutionRatingList(string institution_gid, string employee_gid, string tmp_status,MdlRatingList values)
        {
            msSQL = " select institution2ratingdetail_gid,application_gid, institution_gid,creditrating_agencygid,creditrating_agencyname," +
                    " creditrating_gid,creditrating_name,date_format(a.assessed_on,'%d-%m-%Y') as assessed_on,creditrating_link, " +
                    " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date " +
                    " from agr_mst_tsuprinstitution2ratingdetail a ";
            if (tmp_status == "true")
                msSQL += " where a.institution_gid = '" + employee_gid + "' order by institution2ratingdetail_gid desc";
            else if(tmp_status == "both")
                msSQL += " where (a.institution_gid = '" + employee_gid + "' or a.institution_gid = '" + institution_gid + "') order by institution2ratingdetail_gid desc";
            else
                msSQL += " where a.institution_gid = '" + institution_gid + "' order by institution2ratingdetail_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getMdlRatingdtllist = new List<MdlRatingdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getMdlRatingdtllist.Add(new MdlRatingdtl
                    {
                        institution2ratingdetail_gid = (dr_datarow["institution2ratingdetail_gid"].ToString()),
                        application_gid = (dr_datarow["application_gid"].ToString()),
                        institution_gid = (dr_datarow["institution_gid"].ToString()),
                        creditrating_agencygid = (dr_datarow["creditrating_agencygid"].ToString()),
                        creditrating_agencyname = (dr_datarow["creditrating_agencyname"].ToString()),
                        creditrating_gid = (dr_datarow["creditrating_gid"].ToString()),
                        creditrating_name = (dr_datarow["creditrating_name"].ToString()),
                        assessed_on = (dr_datarow["assessed_on"].ToString()),
                        creditrating_link = (dr_datarow["creditrating_link"].ToString()),  
                        created_date = (dr_datarow["created_date"].ToString()),
                    });
                }
                values.MdlRatingdtl = getMdlRatingdtllist;
            }
            dt_datatable.Dispose();
        }

        public void DaDeleteRatingDtl(string institution2ratingdetail_gid, result values)
        {
            msSQL = "delete from agr_mst_tsuprinstitution2ratingdetail where institution2ratingdetail_gid='" + institution2ratingdetail_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.message = "Rating Details are Deleted Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured";
                values.status = false;

            }
        }

        public void DaGetmilestonepaymentdtl(MdlmilestonepaymentList values)
        {
            msSQL = "select milestonepaymenttype_gid,milestonepaymenttype_name from agr_mst_tmilestonepaymenttype where status='Y' order by milestonepaymenttype_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getScopeList = new List<milestonepayment>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getScopeList.Add(new milestonepayment
                    {
                        milestonepaymenttype_gid = (dr_datarow["milestonepaymenttype_gid"].ToString()),
                        milestonepaymenttype_name = (dr_datarow["milestonepaymenttype_name"].ToString()),
                    });
                }
                values.milestonepayment = getScopeList;
            }
            dt_datatable.Dispose();
            values.status = true;

        }

        public void DaGetNatureFormStateofCommodity(NatureFormStateofCommodityList values)
        {
            msSQL = "select natureformstateofcommodity_gid,natureformstateofcommodity_name from agr_mst_tnatureformstateofcommodity where status='Y' order by natureformstateofcommodity_name asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getScopeList = new List<NatureFormStateofCommodity>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getScopeList.Add(new NatureFormStateofCommodity
                    {
                        natureformstateofcommodity_gid = (dr_datarow["natureformstateofcommodity_gid"].ToString()),
                        natureformstateofcommodity_name = (dr_datarow["natureformstateofcommodity_name"].ToString()),
                    });
                }
                values.NatureFormStateofCommodity = getScopeList;
            }
            dt_datatable.Dispose();
            values.status = true;

        }

        public void DaGetChargeproduct(string application_gid, MdlList values)
        {
            msSQL = " select group_concat(application2loan_gid) from agr_mst_tsuprapplicationservicecharge  " +
                    " where application_gid = '" + application_gid + "' and application2loan_gid is not null";
            string lsapplication2loan_gid = objdbconn.GetExecuteScalar(msSQL);
            lsapplication2loan_gid = lsapplication2loan_gid.Replace(",", "','");

            msSQL = " select application2loan_gid,producttype_gid,product_type,productsubtype_gid,productsub_type " +
                    " from agr_mst_tsuprapplication2loan where application_gid='" + application_gid + "'" +
                    " and application2loan_gid not in ('" + lsapplication2loan_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getSegment = new List<product_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getSegment.Add(new product_list
                    {
                        application2loan_gid = (dr_datarow["application2loan_gid"].ToString()),
                        producttype_gid = (dr_datarow["producttype_gid"].ToString()),
                        product_type = (dr_datarow["product_type"].ToString()),
                        productsubtype_gid = (dr_datarow["productsubtype_gid"].ToString()),
                        productsub_type = (dr_datarow["productsub_type"].ToString()),
                    });
                }
                values.product_list = getSegment;
            }
            dt_datatable.Dispose();
            values.status = true;

        }

        public void DaGetOnboardAppValidatePANAadhar(MdlonboardValidatedtl values)
        {
            string lsapplication_gid = "", lspan = "", lsaadhar = "";
            //msSQL = " select  GROUP_CONCAT('\\\'', application_gid, '\\\'') as applicationgid  from agr_mst_tsuprapplication " +
            //        " where buyeronboard_gid in (select buyeronboard_gid from agr_mst_tsuprapplication " +
            //        " where application_gid = '" + values.application_gid + "')";
            //lsapplication_gid = objdbconn.GetExecuteScalar(msSQL);

            if (values.application_gid != "" || values.application_gid != null)
            {
                msSQL = " select application_gid from agr_mst_tsuprinstitution where";
                if (values.stakeholder_type == "Applicant")
                {
                    msSQL += " stakeholder_type='Applicant' and companypan_no='" + values.pan_no + "'" +
                             " and institution_gid !='" + values.institution_gid + "' and application_gid not in (" + lsapplication_gid + ") ";
                }
                else
                {
                    msSQL += " stakeholder_type in ('Guarantor','Member','Applicant') and companypan_no='" + values.pan_no + "'" +
                             " and application_gid ='" + values.application_gid + "'";
                    if (values.institution_gid != null)
                        msSQL += " and institution_gid !='" + values.institution_gid + "'";

                } 
                lspan = objdbconn.GetExecuteScalar(msSQL);
                if (lspan != "")
                    values.panoraadhar = "PAN";
                if (values.application_gid != "")
                {
                    msSQL = " select application_gid from agr_mst_tsuprcontact where";
                    if (values.stakeholder_type == "Applicant")
                    {
                        msSQL += " stakeholder_type='Applicant' and pan_no ='" + values.pan_no + "' " +
                                 " and contact_gid !='" + values.contact_gid + "' and application_gid not in (" + lsapplication_gid + ") ";
                    }
                    else
                    {
                        msSQL += " stakeholder_type in ('Guarantor','Member','Applicant') and pan_no ='" + values.pan_no + "' " +
                                 " and application_gid ='" + values.application_gid + "'";
                        if (values.contact_gid != null)
                            msSQL += " and contact_gid != '" + values.contact_gid + "'";
                    }
                    lspan = objdbconn.GetExecuteScalar(msSQL);

                    if (values.aadhar_no != "" && values.aadhar_no != null)
                    {
                        msSQL = " select application_gid from agr_mst_tsuprcontact where ";
                        if (values.stakeholder_type == "Applicant")
                        {
                            msSQL += " stakeholder_type='Applicant' and aadhar_no ='" + values.aadhar_no + "'" +
                                " and contact_gid !='" + values.contact_gid + "' and application_gid not in (" + lsapplication_gid + ") ";
                        }
                        else
                        {
                            msSQL += " stakeholder_type in ('Guarantor','Member','Applicant') and aadhar_no ='" + values.aadhar_no + "'" +
                                     " and application_gid ='" + values.application_gid + "'";
                            if (values.contact_gid != null)
                                msSQL += " and contact_gid !='" + values.contact_gid + "'";
                        }
                        lsaadhar = objdbconn.GetExecuteScalar(msSQL);
                    }
                    if (lspan != "" && lsaadhar == "")
                        values.panoraadhar = "PAN";
                    else if (lsaadhar != "" && lspan == "")
                        values.panoraadhar = "Aadhar";
                    else if (lspan != "" && lsaadhar != "")
                        values.panoraadhar = "Both";
                }
                values.status = true;
            }
        }

    }
}