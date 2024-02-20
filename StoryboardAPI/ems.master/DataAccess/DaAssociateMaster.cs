using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
/// <summary>
/// (It's used for Associate Master master) Associate Master DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>


namespace ems.master.DataAccess
{
    public class DaAssociateMaster
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msGetAPICode;
        int mnResult, GetApiMasterGID;

        public void DaGetAssociateMaster(MdlAssociateMaster objMdlAssociateMaster)
        {
            try
            {
                msSQL = " SELECT associatemaster_gid,api_code,name,associate_code,status FROM ocs_mst_tassociatemaster order by associatemaster_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getassociatemaster = new List<associatemaster_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getassociatemaster.Add(new associatemaster_list
                        {
                            associatemaster_gid = (dr_datarow["associatemaster_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            name = (dr_datarow["name"].ToString()),
                            associate_code = (dr_datarow["associate_code"].ToString()),
                            rdbstatus = (dr_datarow["status"].ToString()),
                        });
                    }
                    objMdlAssociateMaster.associatemaster_list = getassociatemaster;
                }
                dt_datatable.Dispose();
                objMdlAssociateMaster.status = true;
            }
            catch
            {
                objMdlAssociateMaster.status = false;
            }
        }

        public void DaCreateAssociateMaster(associatemaster values, string employee_gid)
        {
            msSQL = "select associatemaster_gid from ocs_mst_tassociatemaster where name = '" + values.name.Replace("'", "\\'") + "'";
            string lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                //if (lsdocumentgid != values.associatemaster_gid)
                //{
                    values.message = "This Associate Master Already Exists";
                    values.status = false;
                    return;
                //}
            }
            
            msGetGid = objcmnfunctions.GetMasterGID("MSAM");
            msGetAPICode = objcmnfunctions.GetApiMasterGID("ASSO");
            msSQL = " insert into ocs_mst_tassociatemaster(" +
                    " associatemaster_gid," +
                    " api_code," +
                    " name," +
                    " associate_code," +
                    " status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + values.name.Replace("'","\\'") + "'," +
                    "'" + values.associate_code + "'," +
                    "'" + values.rdbstatus + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Associate Mater Added Successfully..!!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Adding Associate Mater!";
            }
        }

        public void DaEditAssociateMaster(string associatemaster_gid, associatemaster values)
        {
            try
            {
                msSQL = " select associatemaster_gid,associate_code,name,status from ocs_mst_tassociatemaster where associatemaster_gid='" + associatemaster_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.associatemaster_gid = objODBCDatareader["associatemaster_gid"].ToString();
                    values.associate_code = objODBCDatareader["associate_code"].ToString();
                    values.name = objODBCDatareader["name"].ToString();
                    values.rdbstatus = objODBCDatareader["status"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateAssociateMaster(string employee_gid, associatemaster values)
        {
            msSQL = "select associatemaster_gid from ocs_mst_tcompanydocument where name = '" + values.name.Replace("'", "\\'") + "'";
            string lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                if (lsdocumentgid != values.associatemaster_gid)
                {
                    values.message = "This Associate Master Already Exists";
                    values.status = false;
                    return;
                }
            }
                       
            msSQL = " update ocs_mst_tassociatemaster set " +
                 " associate_code='" + values.associate_code + "'," +
                 " name='" + values.name.Replace("'", "\\'") + "'," +
                 " status='" + values.rdbstatus + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where associatemaster_gid='" + values.associatemaster_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Associate Mater Updated Successfully..!!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating Associate Mater!";
            }
        }

        public void DaDeleteAssociateMaster(string associatemaster_gid, associatemaster values)
        {

            msSQL = " delete from ocs_mst_tassociatemaster where associatemaster_gid='" + associatemaster_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
            }
            else
            {
                values.status = false;
            }
        }
        // ------Get Associate  with order by ASC-----------//
        public void DaGetAssociateMasterASC(MdlAssociateMaster objMdlAssociateMaster)
        {
            try
            {
                msSQL = " SELECT associatemaster_gid,name,associate_code,status FROM ocs_mst_tassociatemaster where status='Yes' order by associatemaster_gid asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getassociatemaster = new List<associatemaster_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getassociatemaster.Add(new associatemaster_list
                        {
                            associatemaster_gid = (dr_datarow["associatemaster_gid"].ToString()),
                            name = (dr_datarow["name"].ToString()),
                            associate_code = (dr_datarow["associate_code"].ToString()),
                            rdbstatus = (dr_datarow["status"].ToString()),
                        });
                    }
                    objMdlAssociateMaster.associatemaster_list = getassociatemaster;
                }
                dt_datatable.Dispose();
                objMdlAssociateMaster.status = true;
            }
            catch
            {
                objMdlAssociateMaster.status = false;
            }
        }
    }
}