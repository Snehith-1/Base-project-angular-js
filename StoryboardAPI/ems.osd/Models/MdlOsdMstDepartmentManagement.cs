using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.osd.Models
{

    public class activatedeptlist : result
    {
        public List<acivatedept> acivatedept { get; set; }
        public List<deptlist> deptlist { get; set; }
        public string department_gid { get; set; }
        public string department_name { get; set; }
        public List<businessunit_list> businessunit_list { get; set; }
        public List<businessstatusunit_list> businessstatusunit_list { get; set; }
        public List<businessstatusEdit_list> businessstatusEdit_list { get; set; }

    }
    public class acivatedept : result
    {
        public string activedepartment_gid { get; set; }
        public char departmentstatus { get; set; }
        public string department_code { get; set; }
        public string remarks { get; set; }
        public string department_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string department_name { get; set; }
        public string department_status { get; set; }
    }
    public class businessstatusEdit_list : result
    {
        public string businessunit_gid { get; set; }
        public string businessunit_prefix { get; set; }
        public string businessunit_code { get; set; }
        public string businessunit_name { get; set; }
        public string businessunit_emailaddress { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string businessunit_status { get; set; }
        public string remarks { get; set; }
        public char businessunitstatus { get; set; }
        public string sequence_flag { get; set; }
        public string business_status { get; set; }
        public string businessstatus_gid { get; set; }


    }
    public class businessstatusunit_list : result
    {
        public string businessunit_gid { get; set; }
        public string businessunit_prefix { get; set; }
        public string businessunit_code { get; set; }
        public string businessunit_name { get; set; }
        public string businessunit_emailaddress { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string businessunit_status { get; set; }
        public string remarks { get; set; }
        public char businessunitstatus { get; set; }
        public string sequence_flag { get; set; }
        public string business_status { get; set; }
        public string businessstatus_gid { get; set; }


    }
    public class deptlist : result
    {
        public string activedepartment_gid { get; set; }
        public string department_gid { get; set; }
        public string department_name { get; set; }
    }

    public class departmentstatusHistory : result
    {
        public List<departmentstatusehistory_list> departmentstatusehistory_list { get; set; }
    }
    public class departmentstatusehistory_list : result
    {
        public string activedepartment_gid { get; set; }
        public string remarks { get; set; }
        public string department_gid { get; set; }
        public string department_name { get; set; }
        public string departmentstatus { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
    public class MdlEmployeeassign : result
    {
        public List<employeeasssign_list> employeeasssign_list { get; set; }
        public List<managereasssigned_list> managereasssigned_list { get; set; }
        public List<membereasssigned_list> membereasssigned_list { get; set; }
    }
    public class employeeasssign_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class membereasssigned_list
    {

        public string activedepartment2member_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string department_name { get; set; }
    }
    public class managereasssigned_list
    {
        public string activedepartment2manager_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string department_name { get; set; }
    }
    public class Mdlassignmanager : result
    {
        public string[] employeelist_gid { get; set; }
        public string activedepartment_gid { get; set; }
        public string department_gid { get; set; }
        public string department_name { get; set; }
    }

    public class mdldepartment : result
    {
        public List<department> department { get; set; }
    }
    public class department
    {
        public string department_gid { get; set; }
        public string department_name { get; set; }
    }
    public class businessunit_list : result
    {
        public string businessunit_gid { get; set; }
        public string businessunit_prefix { get; set; }
        public string businessunit_code { get; set; }
        public string businessunit_name { get; set; }
        public string businessunit_emailaddress { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string businessunit_status { get; set; }
        public string remarks { get; set; }
        public char businessunitstatus { get; set; }
        public string sequence_flag { get; set; }
        public string business_status { get; set; }
        public string businessstatus_gid { get; set; }
        

    }
    public class businessunitstatusHistory : result
    {
        public List<businessunitstatusHistory_list> businessunitstatusHistory_list { get; set; }
    }
    public class businessunitstatusHistory_list : result
    {
        public string remarks { get; set; }
        public string businessunit_gid { get; set; }
        public string businessunit_name { get; set; }
        public string businessunit_status { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
    }
}