using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.mastersamagro.Models
{

    /// <summary>
    /// This Models will store values to fetch data from credit mapping master in custopedia.
    /// </summary>
    /// <remarks>Written by Abilash.A</remarks>

    public class MdlCreditGroup : result
    {
        public string creditmapping_gid { get; set; }
        public string creditgroup_gid { get; set; }
        public string creditgroup_name { get; set; }
        public string creditgroup_status { get; set; }
        public List<CreditGroup> CreditGroup { get; set; }
        public List <Credithead> Credithead { get; set; }
        public List<Creditnationalmanager> Creditnationalmanager { get; set; }
        public List<Creditregionalmanager> Creditregionalmanager { get; set; }
        public List<CreditManager> CreditManager { get; set; }
        public List<Creditlog> Creditlog { get; set; }
        public List<Creditheadem_list> Creditheadem_list { get; set; }
        public List<Creditnationalmanagerem_list> Creditnationalmanagerem_list { get; set; }
        public List<Creditregionalmanagerem_list> Creditregionalmanagerem_list { get; set; }
        public List<CreditManagerem_list> CreditManagerem_list { get; set; }
        public List<creditgoupname> creditgoupname { get; set; }
    }
    public class Creditheadem_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class Creditnationalmanagerem_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class Creditregionalmanagerem_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class CreditManagerem_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class CreditGroup : result
    {      
        public string creditmapping_gid { get; set; }
        public string creditgroup_id { get; set; }
        public string creditgroup_name { get; set; }
        public string creditgroup_status { get; set; }
        public string remarks { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public char rbo_status { get; set; }
    }
    public class Credithead
    {
        public string credit2credithead_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class Creditnationalmanager
    {
        public string credit2nationalmanager_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class Creditregionalmanager
    {
        public string creditr2regionalmanager_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class CreditManager
    {
        public string credit2creditmanager_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class Creditlog
    {
        public string creditmapping_gid { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string remarks { get; set; }
        public string status { get; set; }
    }
    public class MdlCreditheadassign : result
    {
        public string creditgroup_gid { get; set; }
        public string creditmanager_gid { get; set; }
        public string regionalcredit_gid { get; set; }
        public string nationalcredit_gid { get; set; }
        public string credithead_gid { get; set; }
        public string creditgroup_name { get; set; }
        public string creditmanager_name { get; set; }
        public string regionalcredit_name { get; set; }
        public string nationalcredit_name { get; set; }
        public string credithead_name { get; set; }
        public string creditgroup_status { get; set; }
        public string application_gid { get; set; }
        public string remarks { get; set; }
    }
    public class creditgoupname : result
    {
        public string creditgroup_gid { get; set; }
        public string creditgroup_name { get; set; }
    }
    public class creditheads : result
    {
        public string credithead { get; set; }
        public string creditmanager { get; set; }
        public string creditregional_manager { get; set; }
        public string creditnational_manager { get; set; }
    }

    public class MdlreassignedlogInfo : result
    {
        public List<reassignedloglist> reassignedloglist { get; set; }
    }
    public class reassignedloglist
    {
        public string application_gid { get; set; }
        public string creditmanger_gid { get; set; }
        public string creditmanger_name { get; set; }
        public string reassignto_creditmanger_gid { get; set; }
        public string reassignto_creditmanger_name { get; set; }

        public string creditregionalmanager_gid { get; set; }
        public string creditregionalmanager_name { get; set; }
        public string reassignto_creditregionalmanager_gid { get; set; }
        public string reassignto_creditregionalmanager_name { get; set; }

        public string creditnationalmanager_gid { get; set; }
        public string creditnationalmanager_name { get; set; }
        public string reassignto_creditnationalmanager_gid { get; set; }
        public string reassignto_creditnationalmanager_name { get; set; }

        public string credithead_gid { get; set; }
        public string credithead_name { get; set; }
        public string reassignto_credithead_gid { get; set; }
        public string reassignto_credithead_name { get; set; }

        public string remarks { get; set; }
        public string reassign_remarks { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string reassignto_creditgroup_name { get; set; }
        public string reassignto_creditgroup_gid { get; set; }
        public string creditgroup_name { get; set; }
        public string creditgroup_gid { get; set; }
    }
}