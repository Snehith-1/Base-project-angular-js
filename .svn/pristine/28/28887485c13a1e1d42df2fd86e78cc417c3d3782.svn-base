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

namespace ems.mastersamagro.DataAccess
{
    /// <summary>
    /// This DataAccess will provide access to fetch data from CC Members master in custopedia.
    /// </summary>
    /// <remarks>Written by Abilash.A, Premchander.K </remarks>
    public class DaAgrMstCCMemeber
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid;
        string lspath, lsdocument_type;

        int mnResult;
        public bool Dapostccmember(string employee_gid, MdlMstCCMember values)
        {
            for (var i = 0; i < values.ccmember_list.Count; i++)
            {
                msSQL = "select ccmembermaster_gid from ocs_mst_tccmember where ccmember_gid='" + values.ccmember_list[i].employee_gid + "' and ccgroupname_gid='" + values.ccgroupname_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.message = "Already CC Member Exist";
                    values.status = false;
                    objODBCDatareader.Close();
                    return false;
                }

                msGetGid = objcmnfunctions.GetMasterGID("CCMR");
                msSQL = " insert into ocs_mst_tccmember(" +
                        " ccmembermaster_gid," +
                        " ccmember_gid," +
                        " ccmember_name," +
                        " ccgroupname_gid," +
                        " ccgroup_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.ccmember_list[i].employee_gid + "'," +
                        "'" + values.ccmember_list[i].employee_name.Replace("   ", "").Replace("'", "") + "'," +
                        "'" + values.ccgroupname_gid + "'," +
                        "'" + values.ccgroup_name.Replace("   ", "").Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }           

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "CC Member added sucessfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while adding CC Member";
                return false;
            }
        }

        public void DaGetccmember(MdlMstCCMember objccmember)
        {
            try
            {
                msSQL = " select ccmembermaster_gid,ccmember_name,ccmember_gid,b.ccgroup_name from ocs_mst_tccmember a" +
                        " left join ocs_mst_tccgroupname b on a.ccgroupname_gid=b.ccgroupname_gid order by ccmembermaster_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getccmember = new List<ccmember_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getccmember.Add(new ccmember_list
                        {
                            ccmembermaster_gid = (dr_datarow["ccmembermaster_gid"].ToString()),
                            CCMember_name = (dr_datarow["ccmember_name"].ToString()),
                            ccmember_gid = (dr_datarow["ccmember_gid"].ToString()),
                            ccgroup_name = (dr_datarow["ccgroup_name"].ToString())
                        });
                    }
                    objccmember.ccmember_list = getccmember;
                }
                dt_datatable.Dispose();
            }
            catch
            {

            }
        }

        public void DaGetdeleteccmember(string ccmembermaster_gid, MdlMstCCMember values)
        {
            msSQL = "delete from ocs_mst_tccmember where ccmembermaster_gid='" + ccmembermaster_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "CC Member deleted sucessfully";

            }
            else
            {
                values.status = true;
                values.message = "Error Occured while deleting CC Member";

            }
        }
        public void DaGetccgroup2member(MdlMstCCMember values, string ccgroupname_gid)
        {
            try
            {
                msSQL = " select ccmember_name,ccmember_gid from ocs_mst_tccmember where ccgroupname_gid='" + ccgroupname_gid + "' ";


                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getccmember_list = new List<ccmember_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getccmember_list.Add(new ccmember_list
                        {
                            CCMember_name = dt["ccmember_name"].ToString(),
                            ccmember_gid = dt["ccmember_gid"].ToString(),
                        });
                    }
                    values.ccmember_list = getccmember_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message.ToString();
            }
        }
        public bool Dapostccgroup(string employee_gid, ccgroupname values)
        {

            msSQL = "select ccgroupname_gid from ocs_mst_tccgroupname where ccgroup_name='" + values.ccgroup_name + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.message = "Already CC Group Exist";
                values.status = false;
                objODBCDatareader.Close();
                return false;
            }
            msGetGid = objcmnfunctions.GetMasterGID("CCMR");
            msSQL = " insert into ocs_mst_tccgroupname(" +
                    " ccgroupname_gid," +
                    " ccgroup_code," +
                    " ccgroup_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.ccgroup_code.Replace("   ", "").Replace("'", "") + "'," +
                    "'" + values.ccgroup_name.Replace("   ", "").Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "CC Group Name added sucessfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured while adding CC Group Name";
                return false;
            }
        }
        public void DaGetccgroup(ccgroupname objccmember)
        {
            try
            {
                msSQL = " select ccgroupname_gid,ccgroup_code,ccgroup_name from ocs_mst_tccgroupname order by ccgroupname_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getccgroup = new List<ccgroup_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getccgroup.Add(new ccgroup_list
                        {
                            ccgroupname_gid = (dr_datarow["ccgroupname_gid"].ToString()),
                            ccgroup_code = (dr_datarow["ccgroup_code"].ToString()),
                            ccgroup_name = (dr_datarow["ccgroup_name"].ToString()),

                        });
                    }
                    objccmember.ccgroup_list = getccgroup;
                }
                dt_datatable.Dispose();
            }
            catch
            {

            }
        }
        public void Dageteditccgroup(ccgroupname values, string ccgroupname_gid)
        {
            try
            {
                msSQL = " select ccgroupname_gid,ccgroup_code,ccgroup_name from ocs_mst_tccgroupname where ccgroupname_gid='" + ccgroupname_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.ccgroup_code = objODBCDatareader["ccgroup_code"].ToString();
                    values.ccgroup_name = objODBCDatareader["ccgroup_name"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message.ToString();
            }
        }
        public void Daupdateccgroup(ccgroupname values, string employee_gid)
        {
            msSQL = "update ocs_mst_tccgroupname set ccgroup_code='" + values.ccgroup_code + "'," +
                " ccgroup_name='" + values.ccgroup_name + "'," +
                " updated_by ='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where ccgroupname_gid='" + values.ccgroupname_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {

                values.status = true;
                values.message = "CC Group Name Updated sucessfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occured while updating CC Group Name";

            }
        }
        public void Dageteditccmember(MdlMstCCMember values, string ccmembermaster_gid)
        {
            try
            {
                msSQL = " select ccmember_name,ccmember_gid,ccgroupname_gid from ocs_mst_tccmember where ccmembermaster_gid='" + ccmembermaster_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.ccmember_gid = objODBCDatareader["ccmember_gid"].ToString();
                    values.CCMember_name = objODBCDatareader["ccmember_name"].ToString();
                    values.ccgroupname_gid = objODBCDatareader["ccgroupname_gid"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
                values.message = ex.Message.ToString();
            }
        }
        public void Daupdateccmember(MdlMstCCMember values, string employee_gid)
        {

            msSQL = "select ccmembermaster_gid from ocs_mst_tccmember where ccmember_gid='" + values.ccmember_gid + "' and ccgroupname_gid='" + values.ccgroupname_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.message = "Already CC Member Exist";
                values.status = false;
                objODBCDatareader.Close();

            }
            else
            {

                msSQL = "select concat(b.user_code,'/',b.user_firstname,' ',b.user_lastname) as ccmembername from hrm_mst_temployee a" +
                    " left join adm_mst_tuser b on a.user_gid=b.user_gid where a.employee_gid='" + values.ccmember_gid + "'";
                string lsccmember_name = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "update ocs_mst_tccmember set ccmember_gid='" + values.ccmember_gid + "'," +
                    " ccgroupname_gid='" + values.ccgroupname_gid + "'," +
                    " ccmember_name='" + lsccmember_name + "'," +
                    " updated_by ='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where ccmembermaster_gid='" + values.ccmembermaster_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {

                    values.status = true;
                    values.message = "CC Group Name Updated sucessfully";

                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured while updating CC Group Name";

                }

            }
        }
    }

}