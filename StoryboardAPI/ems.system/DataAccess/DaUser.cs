using ems.system.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ems.system.DataAccess
{
    public class DaUser
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        OdbcDataReader objodbcDataReader;
        DataTable dt_levelone, dt_leveltwo, dt_levelthree, dt_levelfour;
        string menu_ind_up_first = string.Empty;
        string menu_ind_down_first = string.Empty;
        string menu_ind_up_second = string.Empty;
        string menu_ind_down_second = string.Empty;



        public void userDataFromDB(string user_gid, UserData values)
        {
            try
            {
                msSQL = " SELECT a.user_code, CONCAT(a.user_firstname,' ',a.user_lastname) as user_name,d.employee_photo ,b.designation_name,c.department_name FROM hrm_mst_temployee d " +
                       " LEFT JOIN adm_mst_tuser a ON a.user_gid = d.user_gid " +
                       " LEFT JOIN adm_mst_tdesignation b ON b.designation_gid = d.designation_gid " +
                       " LEFT JOIN hrm_mst_tdepartment c ON c.department_gid = d.department_gid WHERE d.user_gid = '" + user_gid + "'";
                objodbcDataReader = objdbconn.GetDataReader(msSQL);
                if (objodbcDataReader.HasRows)
                {
                    values.user_code = objodbcDataReader["user_code"].ToString();
                    values.user_name = objodbcDataReader["user_name"].ToString();
                    if (objodbcDataReader["employee_photo"].ToString() != "")
                    {
                        values.user_photo = objodbcDataReader["employee_photo"].ToString();
                    }
                    else
                    {
                        values.user_photo = "N";
                    }

                    values.user_designation = objodbcDataReader["designation_name"].ToString();
                    values.user_department = objodbcDataReader["department_name"].ToString();
                    values.message = "success";
                    values.status = true;
                }
                else
                {
                    values.message = "failure";
                    values.status = false;
                }
                objodbcDataReader.Close();
            }
            catch
            {
                values.message = "error";
                values.status = false;
            }
        }

        public void loadMenuFromDB(string user_gid, menu_response values)
        {
            var dt_data =new DataTable();
            var getmenu = new List<sys_menu>();
            //var limoduleprivilege_userlist = new List<moduleprivilege_userlist>();
            //var Get_Summary = new List<sys_sub1menu>();
            // List<sys_menu> getmenu;
            try
            {

                msSQL = " SELECT a.module_gid,b.module_name,b.sref,b.icon,b.menu_level FROM adm_mst_tprivilege a " +
                             " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid"+
                             " WHERE user_gid = '" + user_gid + "' AND menu_level=1 AND b.lw_flag='Y' group by a.module_gid order by b.display_order asc";
                dt_levelone = objdbconn.GetDataTable(msSQL);
                if (dt_levelone.Rows.Count != 0)
                {
                    foreach (DataRow drOne in dt_levelone.Rows)
                    {


                        msSQL = " SELECT a.module_gid,b.module_name,b.sref,b.icon,b.menu_level FROM adm_mst_tprivilege a " +
                                " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid" +
                                " WHERE user_gid = '" + user_gid + "' " +
                                " AND b.menu_level = 2 AND b.module_gid_parent = '" + drOne["module_gid"].ToString() + "' AND b.lw_flag='Y' group by a.module_gid order by b.display_order asc";
                        dt_leveltwo = objdbconn.GetDataTable(msSQL);
                        var getmenu2 = new List<sys_submenu>();
                        if (dt_leveltwo.Rows.Count != 0)
                        {
                            //menu_ind_up_first = "fa fa-angle-up";
                            //menu_ind_down_first = "fa fa-angle-down";

                            foreach (DataRow drTwo in dt_leveltwo.Rows)
                            {

                                msSQL = " SELECT a.module_gid,b.module_name,b.sref,b.icon,b.menu_level FROM adm_mst_tprivilege a " +
                                        " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid WHERE user_gid = '" + user_gid + "' " +
                                        " AND b.menu_level = 3 AND b.module_gid_parent= '" + drTwo["module_gid"].ToString() + "' AND b.lw_flag='Y' group by a.module_gid order by b.display_order asc";
                                dt_levelthree = objdbconn.GetDataTable(msSQL);
                                var getmenu3 = new List<sys_sub1menu>();
                                if (dt_levelthree.Rows.Count !=0)
                                {
                                    foreach(DataRow drthree in dt_levelthree.Rows)
                                    {
                                        msSQL = " SELECT a.module_gid,b.module_name,b.sref,b.icon,b.menu_level FROM adm_mst_tprivilege a " +
                                        " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid WHERE user_gid = '" + user_gid + "' " +
                                        " AND b.menu_level = 4 AND b.module_gid_parent='" + drthree["module_gid"].ToString() + "' AND b.lw_flag='Y' group by a.module_gid order by b.display_order asc";
                                        dt_levelfour = objdbconn.GetDataTable(msSQL);
                                        var getmenu4 = new List<sys_sub2menu>();
                                        if(dt_levelfour.Rows.Count !=0)
                                        {
                                            menu_ind_up_second = "fa fa-angle-up";
                                            menu_ind_down_second = "fa fa-angle-down";
                                            getmenu4 = dt_levelfour.AsEnumerable().Select(row =>
                                              new sys_sub2menu
                                              {

                                                  text = row["module_name"].ToString(),
                                                  sref = row["sref"].ToString(),
                                                  icon = row["icon"].ToString(),
                                                  // sub_icon = row["icon_name"].ToString()
                                              }).ToList();
                                        }
                                        dt_levelfour.Clear();
                                        getmenu3.Add(new sys_sub1menu
                                        {
                                            text = drthree["module_name"].ToString(),
                                            sref = drthree["sref"].ToString(),
                                            sub2menu = getmenu4
                                        });
                                    }
                                    
                                }
                                dt_levelthree.Clear();
                                getmenu2.Add(new sys_submenu
                                {
                                    text = drTwo["module_name"].ToString(),
                                    sref = drTwo["sref"].ToString(),
                                    menu_indication = menu_ind_up_second,
                                    menu_indication1 = menu_ind_down_second,
                                    // sub_icon = drTwo["icon_name"].ToString(),
                                    sub1menu = getmenu3
                                }) ;

                            }

                            dt_leveltwo.Clear();
                        }
                        else
                        {
                            menu_ind_up_first = "";
                            menu_ind_down_first = "";
                        }
                        //var getmenu = new List<sys_menu>();
                        getmenu.Add(new sys_menu
                        {
                            text = drOne["module_name"].ToString(),
                            sref = drOne["sref"].ToString(),
                            icon = drOne["icon"].ToString(),
                            menu_indication = menu_ind_up_first,
                            menu_indication1 = menu_ind_down_first,
                            label = "label label-success",
                            submenu = getmenu2
                        });
                        values.menu_list = getmenu;
                    }

                }
           
                dt_levelone.Clear();
            }
            catch 
            {
                values.status = false;
            }
            finally
            {
            }

        }



        public void projectprivilege(string user_gid, project_list values)
        {

            msSQL = " SELECT a.module_gid FROM adm_mst_tprivilege a " +
                    " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid WHERE user_gid = '" + user_gid + "' AND menu_level=1 AND b.lw_flag='Y' order by b.display_order asc";
            dt_levelone = objdbconn.GetDataTable(msSQL);
            if (dt_levelone.Rows.Count != 0)
            {
                values.privileges = dt_levelone.AsEnumerable().Select(row => new privilege
                {
                    project = row["module_gid"].ToString()
                }).ToList();
            }
            objdbconn.CloseConn();
        }
        public void privilegelevel3(string user_gid, projectlist values)
        {

            msSQL = " SELECT a.module_gid FROM adm_mst_tprivilege a " +
                    " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid WHERE user_gid = '" + user_gid + "' AND menu_level=4 AND b.lw_flag='Y' order by b.display_order asc";
            dt_levelone = objdbconn.GetDataTable(msSQL);
            if (dt_levelone.Rows.Count != 0)
            {
                values.privilegelevel3 = dt_levelone.AsEnumerable().Select(row => new privilegelevel3
                {
                    project = row["module_gid"].ToString()
                }).ToList();
            }
            objdbconn.CloseConn();
        }

        public void getcompanydetails(companydetails values)
        {
            msSQL = "select welcome_logo,companylogo_responsive,company_name from adm_mst_tcompany where 1=1";
            objodbcDataReader = objdbconn.GetDataReader(msSQL);
            if (objodbcDataReader.HasRows == true)
            {
                values.company_logo = objodbcDataReader["welcome_logo"].ToString();
                values.companylogo_responsive = objodbcDataReader["companylogo_responsive"].ToString();
                values.company_name = objodbcDataReader["company_name"].ToString();
            }
            objodbcDataReader.Close();
            values.status = true;
        }

        public void loadTopMenuFromDB(string user_gid, menu_response values)
        {
            var dt_data = new DataTable();
            var getmenu = new List<sys_menu>();
            //var limoduleprivilege_userlist = new List<moduleprivilege_userlist>();
            //var Get_Summary = new List<sys_sub1menu>();
            // List<sys_menu> getmenu;
            try
            {

                msSQL = " SELECT a.module_gid,b.module_name,b.angular_sref,b.sref,b.icon,b.menu_level FROM adm_mst_tprivilege a " +
                             " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid" +
                             " WHERE user_gid = '" + user_gid + "' AND menu_level=1 AND b.lw_flag='Y'  group by a.module_gid order by b.display_order asc";
                dt_levelone = objdbconn.GetDataTable(msSQL);
                if (dt_levelone.Rows.Count != 0)
                {
                    foreach (DataRow drOne in dt_levelone.Rows)
                    {


                        msSQL = " SELECT a.module_gid,b.module_name,b.angular_sref,b.sref,b.icon,b.menu_level FROM adm_mst_tprivilege a " +
                                " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid" +
                                " WHERE user_gid = '" + user_gid + "' " +
                                " AND b.menu_level = 2 AND b.module_gid_parent = '" + drOne["module_gid"].ToString() + "' AND b.lw_flag='Y' " +
                                "  group by a.module_gid order by b.display_order asc";
                        dt_leveltwo = objdbconn.GetDataTable(msSQL);
                        var getmenu2 = new List<sys_submenu>();
                        if (dt_leveltwo.Rows.Count != 0)
                        {
                            //menu_ind_up_first = "fa fa-angle-up";
                            //menu_ind_down_first = "fa fa-angle-down";

                            foreach (DataRow drTwo in dt_leveltwo.Rows)
                            {

                                msSQL = " SELECT a.module_gid,b.module_name,b.angular_sref,b.sref,b.icon,b.menu_level FROM adm_mst_tprivilege a " +
                                        " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid WHERE user_gid = '" + user_gid + "' " +
                                        " AND b.menu_level = 3 AND b.module_gid_parent= '" + drTwo["module_gid"].ToString() + "' AND b.lw_flag='Y' " +
                                        "  group by a.module_gid order by b.display_order asc";
                                dt_levelthree = objdbconn.GetDataTable(msSQL);
                                var getmenu3 = new List<sys_sub1menu>();
                                if (dt_levelthree.Rows.Count != 0)
                                {
                                    foreach (DataRow drthree in dt_levelthree.Rows)
                                    {
                                        msSQL = " SELECT a.module_gid,b.module_name,b.angular_sref,b.sref,b.icon,b.menu_level FROM adm_mst_tprivilege a " +
                                        " LEFT JOIN adm_mst_tmodule b ON a.module_gid = b.module_gid WHERE user_gid = '" + user_gid + "' " +
                                        " AND b.menu_level = 4 AND b.module_gid_parent='" + drthree["module_gid"].ToString() + "' AND b.lw_flag='Y' " +
                                        "  group by a.module_gid order by b.display_order asc";
                                        dt_levelfour = objdbconn.GetDataTable(msSQL);
                                        var getmenu4 = new List<sys_sub2menu>();
                                        if (dt_levelfour.Rows.Count != 0)
                                        {
                                            menu_ind_up_second = "fa fa-angle-up";
                                            menu_ind_down_second = "fa fa-angle-down";
                                            getmenu4 = dt_levelfour.AsEnumerable().Select(row =>
                                              new sys_sub2menu
                                              {

                                                  text = row["module_name"].ToString(),
                                                  angular_sref = row["angular_sref"].ToString(),
                                                  sref = row["sref"].ToString(),
                                                  icon = row["icon"].ToString(),
                                                  // sub_icon = row["icon_name"].ToString()
                                              }).ToList();
                                        }
                                        dt_levelfour.Clear();
                                        getmenu3.Add(new sys_sub1menu
                                        {
                                            text = drthree["module_name"].ToString(),
                                            angular_sref = drthree["angular_sref"].ToString(),
                                            sref = drthree["sref"].ToString(),
                                            sub2menu = getmenu4
                                        });
                                    }

                                }
                                dt_levelthree.Clear();
                                getmenu2.Add(new sys_submenu
                                {
                                    text = drTwo["module_name"].ToString(),
                                    angular_sref = drTwo["angular_sref"].ToString(),
                                    sref = drTwo["sref"].ToString(),
                                    menu_indication = menu_ind_up_second,
                                    menu_indication1 = menu_ind_down_second,
                                    // sub_icon = drTwo["icon_name"].ToString(),
                                    ennableState = true,
                                    activeState = true,
                                    sub1menu = getmenu3
                                });

                            }

                            dt_leveltwo.Clear();
                        }
                        else
                        {
                            menu_ind_up_first = "";
                            menu_ind_down_first = "";
                        }
                        //var getmenu = new List<sys_menu>();
                        getmenu.Add(new sys_menu
                        {
                            text = drOne["module_name"].ToString(),
                            angular_sref = drOne["angular_sref"].ToString(),
                            sref = drOne["sref"].ToString(),
                            icon = drOne["icon"].ToString(),
                            menu_indication = menu_ind_up_first,
                            menu_indication1 = menu_ind_down_first,
                            label = "label label-success",
                            submenu = getmenu2
                        });
                        values.menu_list = getmenu;
                    }

                }

                dt_levelone.Clear();
            }
            catch
            {
                values.status = false;
            }
            finally
            {
            }

        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            dt.Dispose();
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    //in case you have a enum/GUID datatype in your model
                    //We will check field's dataType, and convert the value in it.
                    if (pro.Name == column.ColumnName.TrimEnd())
                    {
                        try
                        {
                            var convertedValue = GetValueByDataType(pro.PropertyType, dr[column.ColumnName.TrimEnd()]);
                            pro.SetValue(obj, convertedValue, null);
                        }
                        catch (Exception e)
                        {
                            //ex handle code                   
                            throw;
                        }
                        //pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
        private static object GetValueByDataType(Type propertyType, object o)
        {
            if (o.ToString() == "null")
            {
                return null;
            }
            if (propertyType == (typeof(Guid)) || propertyType == typeof(Guid?))
            {
                return Guid.Parse(o.ToString());
            }
            else if (propertyType == typeof(int) || propertyType.IsEnum)
            {
                return Convert.ToInt32(o);
            }
            else if (propertyType == typeof(decimal))
            {
                return Convert.ToDecimal(o);
            }
            else if (propertyType == typeof(long))
            {
                return Convert.ToInt64(o);
            }
            else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
            {
                return Convert.ToBoolean(o);
            }
            else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
            {
                return Convert.ToDateTime(o);
            }
            return o.ToString();
        }

    }
   
}