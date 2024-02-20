using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// (It's used for CADProbeAPI) CADProbeAPI Model Class accessed by API methods from related DataAccess class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Praveen Raj</remarks>

namespace ems.master.Models
{
    public class MdlProbe
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    public class MdlBaseDetailsRequest 
    {
        public string pan { get; set; }
        public string institution_gid { get; set; }
        public string application_gid { get; set; }
    }

    public class MdlBaseDetailsResponse : MdlProbe
    {
        public Metadata metadata { get; set; }
        public BaseDetailsData data { get; set; }
    }

    public class Metadata
    {
        public string api_version { get; set; }
        public string last_updated { get; set; }
    }

    public class BaseDetailsData
    {
        public BaseCompany company { get; set; }
        public AuthorizedSignatories[] authorized_signatories { get; set; }
        public OpenCharges[] open_charges { get; set; }

    }

    public class BaseCompany
    {
        public string cin { get; set; }
        public string legal_name { get; set; }
        public string efiling_status { get; set; }
        public string incorporation_date { get; set; }
        public string paid_up_capital { get; set; }
        public string sum_of_charges { get; set; }
        public string authorized_capital { get; set; }
        public string active_compliance { get; set; }
        public string cirp_status { get; set; }
        public RegiseredAddress registered_address { get; set; }
        public string classification { get; set; }
        public string status { get; set; }
        public string next_cin { get; set; }
        public string last_agm_date { get; set; }
        public string last_filing_date { get; set; }
        public string email { get; set; }
    }
    public class RegiseredAddress
    {
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string city { get; set; }
        public string pincode { get; set; }
        public string state { get; set; }
    }

    public class AuthorizedSignatories
    {
        public string pan { get; set; }
        public string din { get; set; }
        public string name { get; set; }
        public string designation { get; set; }
        public string din_status { get; set; }
        public string gender { get; set; }
        public string date_of_birth { get; set; }
        public string age { get; set; }
        public string date_of_appointment { get; set; }
        public string date_of_appointment_for_current_designation { get; set; }
        public string date_of_cessation { get; set; }
        public string nationality { get; set; }
        public string dsc_status { get; set; }
        public string dsc_expiry_date { get; set; }
        public string father_name { get; set; }
        public AddressAuthSign address { get; set; }     
    }
    public class AddressAuthSign
    {
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string city { get; set; }      
        public string state { get; set; }
        public string pincode { get; set; }
        public string country { get; set; }
    }

    public class OpenCharges
    {
        public string id { get; set; }
        public string date { get; set; }
        public string holder_name { get; set; }
        public string amount { get; set; }
        public string type { get; set; }       
    }

    public class MdlInstitutionProbe {
        public List<institutionprobe_list> institutionprobe_list { get; set; }
        public List<institutionprobelog_list> institutionprobelog_list { get; set; }
    }

    public class institutionprobe_list
    {
        public string institutionprobedetails_gid { get; set; }
        public string institution_type { get; set; }
        public string api_name { get; set; }
        public string apicall_status { get; set; }
        public string created_date { get; set; }

    }

    public class institutionprobelog_list
    {
        public string institutionprobedetailslog_gid { get; set; }
        public string api_name { get; set; }
        public string apicall_status { get; set; }
        public string created_date { get; set; }

    }

    public class MdlComprehensiveDetailsRequest
    {
        public string pan { get; set; }
        public string institution_gid { get; set; }
        public string application_gid { get; set; }
    }

    public class MdlComprehensiveDetailsResponse : MdlProbe
    {
        public Metadata metadata { get; set; }
        public ComprehensiveDetailsData data { get; set; }
    }

    public class ComprehensiveDetailsData
    {
        public ComprehensiveCompany company { get; set; }
        public Description description { get; set; }
        public NameHistory[] name_history { get; set; }
        public AuthorizedSignatories[] authorized_signatories { get; set; }
        public DirectorNetwork[] director_network { get; set; }
        public ContactDetails contact_details { get; set; }
        public OpenCharges[] open_charges { get; set; }

        public OpenChargesLatestEvent[] open_charges_latest_event { get; set; }
        public ChargeSequence[] charge_sequence { get; set; }
        public Financials[] financials { get; set; }
        public NBFCFinancials[] nbfc_financials { get; set; }

        public FinancialParameters[] financial_parameters { get; set; }
        public IndustrySegments[] industry_segments { get; set; }
        public PrincipalBusinessActivities[] principal_business_activities { get; set; }

        public Shareholdings[] shareholdings { get; set; }
        public ShareholdingsSummary[] shareholdings_summary { get; set; }
        public DirectorShareholdings[] director_shareholdings { get; set; }
        public BIFRHistory[] bifr_history { get; set; }
        public CDRHistory[] cdr_history { get; set; }
        public DefaulterList[] defaulter_list { get; set; }



        public LegalHistory[] legal_history { get; set; }
        public CreditRatings[] credit_ratings { get; set; }
        public HoldingEntities holding_entities { get; set; }
        public SubsidiaryEntities subsidiary_entities { get; set; }
        public AssociateEntities associate_entities { get; set; }
        public JointVentures joint_ventures { get; set; }
        public SecuritiesAllotment[] securities_allotment { get; set; }
        public PeerComparison[] peer_comparison { get; set; }
        public GSTDetails[] gst_details { get; set; }
        public StruckOff248Details struckoff248_details { get; set; }
    }

    public class ComprehensiveCompany
    {
        public string cin { get; set; }
        public string legal_name { get; set; }
        public string efiling_status { get; set; }
        public string incorporation_date { get; set; }
        public string paid_up_capital { get; set; }
        public string sum_of_charges { get; set; }
        public string authorized_capital { get; set; }
        public string active_compliance { get; set; }
        public string cirp_status { get; set; }
        public RegiseredAddress registered_address { get; set; }
        public RegiseredAddress business_address { get; set; }
        public string pan { get; set; }
        public string website { get; set; }
        public string classification { get; set; }
        public string status { get; set; }
        public string next_cin { get; set; }
        public string last_agm_date { get; set; }
        public string last_filing_date { get; set; }
        public string email { get; set; }
    }
    public class Description
    {
        public string desc_thousand_char { get; set; }
    }
    public class NameHistory
    {
        public string name { get; set; }
        public string date { get; set; }
    }

    public class DirectorNetwork
    {
        public string name { get; set; }
        public string pan { get; set; }
        public string din { get; set; }
        public Network network { get; set; }

    }
    public class Network
    {
        public DirNetworkCompany[] companies { get; set; }
        
    }
    public class DirNetworkCompany
    {
        public string cin { get; set; }
        public string legal_name { get; set; }
        public string company_status { get; set; }
        public string incorporation_date { get; set; }
        public string paid_up_capital { get; set; }
        public string sum_of_charges { get; set; }
        public string city { get; set; }        
        public string active_compliance { get; set; }
        public string cirp_status { get; set; }
        public string designation { get; set; }                
        public string date_of_appointment { get; set; }
        public string date_of_appointment_for_current_designation { get; set; }
        public string date_of_cessation { get; set; }  
    }

    public class ContactDetails
    {
        public Email[] email { get; set; }
        public Phone[] phone { get; set; }
    }
    public class Email
    {
        public string emailId { get; set; }
        public string status { get; set; }
    }
    public class Phone
    {
        public string phoneNumber { get; set; }
        public string status { get; set; }
    }

    public class OpenChargesLatestEvent
    {
        public string id { get; set; }
        public string date { get; set; }
        public string holder_name { get; set; }
        public string amount { get; set; }
        public string type { get; set; }
        public string property_type { get; set; }
        public string number_of_chargeholder { get; set; }
        public string instrument_description { get; set; }
        public string rate_of_interest { get; set; }
        public string terms_of_payment { get; set; }
        public string property_particulars { get; set; }
        public string extent_and_operation { get; set; }
        public string other_terms { get; set; }
        public string modification_particulars { get; set; }
        public string joint_holding { get; set; }
        public string consortium_holding { get; set; }
        public string filing_date { get; set; }

    }

    public class ChargeSequence
    {
        public string charge_id { get; set; }
        public string status { get; set; }
        public string date { get; set; }
        public string amount { get; set; }
        public string holder_name { get; set; }
        public string number_of_holder { get; set; }
        public string property_type { get; set; }
        public string filing_date { get; set; }
        public string property_particulars { get; set; }
    }

    public class Financials
    {
        public string year { get; set; }
        public string nature { get; set; }
        public string stated_on { get; set; }
        public string filing_type { get; set; }
        public string filing_standard { get; set; }
        public BS bs { get; set; }
        public PNL pnl { get; set; }
        public Auditor auditor { get; set; }
    }

    public class NBFCFinancials
    {
        public string year { get; set; }
        public string nature { get; set; }
        public string stated_on { get; set; }
        public string filing_type { get; set; }
        public string filing_standard { get; set; }
        public NBFCBS bs { get; set; }
        public NBFCPNL pnl { get; set; }
        public Auditor auditor { get; set; }
    }
    public class BS
    {
        public Assets assets { get; set; }
        public Liabilities liabilities { get; set; }
        public BSSubTotals subTotals { get; set; }
        public BSMetadata metadata { get; set; }
        public Notes notes { get; set; }            
    }
    public class Assets
    {
        public string tangible_assets { get; set; }
        public string producing_properties { get; set; }
        public string intangible_assets { get; set; }
        public string preproducing_properties { get; set; }
        public string tangible_assets_capital_work_in_progress { get; set; }
        public string intangible_assets_under_development { get; set; }
        public string noncurrent_investments { get; set; }
        public string deferred_tax_assets_net { get; set; }
        public string foreign_curr_monetary_item_trans_diff_asset_account { get; set; }
        public string long_term_loans_and_advances { get; set; }
        public string other_noncurrent_assets { get; set; }
        public string current_investments { get; set; }
        public string inventories { get; set; }
        public string trade_receivables { get; set; }
        public string cash_and_bank_balances { get; set; }
        public string short_term_loans_and_advances { get; set; }
        public string other_current_assets { get; set; }
        public string given_assets_total { get; set; }
    }
    public class Liabilities
    {
        public string share_capital { get; set; }
        public string reserves_and_surplus { get; set; }
        public string money_received_against_share_warrants { get; set; }
        public string share_application_money_pending_allotment { get; set; }
        public string deferred_government_grants { get; set; }
        public string minority_interest { get; set; }
        public string long_term_borrowings { get; set; }
        public string deferred_tax_liabilities_net { get; set; }
        public string foreign_curr_monetary_item_trans_diff_liability_account { get; set; }
        public string other_long_term_liabilities { get; set; }
        public string long_term_provisions { get; set; }
        public string short_term_borrowings { get; set; }
        public string trade_payables { get; set; }
        public string other_current_liabilities { get; set; }
        public string short_term_provisions { get; set; }
        public string given_liabilities_total { get; set; }       
    }

    public class BSSubTotals
    {
        public string total_equity { get; set; }
        public string total_non_current_liabilities { get; set; }
        public string total_current_liabilities { get; set; }
        public string net_fixed_assets { get; set; }
        public string total_current_assets { get; set; }
        public string capital_wip { get; set; }
        public string total_debt { get; set; }
        public string total_other_non_current_assets { get; set; }         
    }

    public class BSMetadata
    {
        public string doc_id { get; set; }     
    }
    public class Notes
    {
        public string gross_fixed_assets { get; set; }
        public string trade_receivable_exceeding_six_months { get; set; }
    }

    public class PNL
    {
        public LineItems lineItems { get; set; }
        public PNLSubTotals subTotals { get; set; }
        public RevenueBreakup revenue_breakup { get; set; }
        public DepreciationBreakup depreciation_breakup { get; set; }
        public PNLMetadata metadata { get; set; }
    }
    public class LineItems
    {
        public string net_revenue { get; set; }
        public string total_cost_of_materials_consumed { get; set; }
        public string total_purchases_of_stock_in_trade { get; set; }
        public string total_changes_in_inventories_or_finished_goods { get; set; }
        public string total_employee_benefit_expense { get; set; }
        public string total_other_expenses { get; set; }
        public string operating_profit { get; set; }
        public string other_income { get; set; }
        public string depreciation { get; set; }
        public string profit_before_interest_and_tax { get; set; }
        public string interest { get; set; }
        public string profit_before_tax_and_exceptional_items_before_tax { get; set; }
        public string exceptional_items_before_tax { get; set; }
        public string profit_before_tax { get; set; }
        public string income_tax { get; set; }
        public string profit_for_period_from_continuing_operations { get; set; }
        public string profit_from_discontinuing_operation_after_tax { get; set; }
        public string minority_interest_and_profit_from_associates_and_joint_ventures { get; set; }
        public string profit_after_tax { get; set; }        
    }
    public class PNLSubTotals
    {
        public string total_operating_cost { get; set; }
    }
    public class RevenueBreakup
    {
        public string revenue_from_operations { get; set; }
        public string revenue_from_interest { get; set; }
        public string revenue_from_other_financial_services { get; set; }
        public string revenue_from_sale_of_products { get; set; }
        public string revenue_from_sale_of_services { get; set; }
        public string other_operating_revenues { get; set; }
        public string excise_duty { get; set; }
        public string service_tax_collected { get; set; }
        public string other_duties_taxes_collected { get; set; }

        public string sale_of_goods_manufactured_domestic { get; set; }
        public string sale_of_goods_traded_domestic { get; set; }
        public string sale_or_supply_of_services_domestic { get; set; }
        public string sale_of_goods_manufactured_export { get; set; }
        public string sale_of_goods_traded_export { get; set; }
        public string sale_or_supply_of_services_export { get; set; }


    }
    public class DepreciationBreakup
    {
        public string depreciation { get; set; }
        public string amortisation { get; set; }
        public string depletion { get; set; }      
    }
    public class PNLMetadata
    {
        public string docId { get; set; }       
    }

    public class Auditor
    {
        public string auditor_name { get; set; }
        public string auditor_firm_name { get; set; }
        public string pan { get; set; }
        public string membership_number { get; set; }
        public string firm_registration_number { get; set; }
        public string address { get; set; }
    }

    public class NBFCBS
    {
        public NBFCAssets assets { get; set; }
        public NBFCLiabilities liabilities { get; set; }
        public NBFCBSSubTotals sub_totals { get; set; }
        public NBFCBSMetadata metadata { get; set; }    
    }

    public class NBFCAssets
    {
        public string cash_equivalents { get; set; }
        public string bank_balance_other_than_cash { get; set; }
        public string derivative_financial_assets { get; set; }
        public string trade_receivables { get; set; }
        public string other_receivables { get; set; }
        public string loans { get; set; }
        public string investments { get; set; }
        public string other_financial_assets { get; set; }
        public string inventories { get; set; }
        public string current_tax_assets_net { get; set; }
        public string deferred_tax_assets_net { get; set; }
        public string investment_property { get; set; }
        public string biological_assets { get; set; }
        public string property_plant_and_equipment { get; set; }
        public string capital_work_in_progress { get; set; }
        public string intangible_under_development { get; set; }
        public string goodwill { get; set; }
        public string other_intangibles { get; set; }
        public string other_non_financial_assets { get; set; }
        public string given_assets_total { get; set; }      
    }
    public class NBFCLiabilities
    {
        public string derivative_financial_instruments { get; set; }
        public string dues_of_micro_and_small_enterprises_TP { get; set; }
        public string dues_of_creditors_TP { get; set; }
        public string dues_of_micro_and_small_enterprises_OP { get; set; }
        public string dues_of_creditors_OP { get; set; }
        public string debt_securities { get; set; }
        public string borrowings_other_than_debt_securities { get; set; }
        public string deposits { get; set; }
        public string subordinated_liabilities { get; set; }
        public string other_financial_liabilities { get; set; }
        public string current_tax_liabilities_net { get; set; }
        public string provisions { get; set; }
        public string deferred_tax_liabilites_net { get; set; }
        public string other_non_financial_liabilities { get; set; }
        public string equity_share_capital { get; set; }
        public string sh_app_money_pending_allotment { get; set; }
        public string eq_comp_of_compound_fin_instruments { get; set; }
        public string statutory_reserves { get; set; }
        public string capital_reserves { get; set; }
        public string securities_premium { get; set; }
        public string other_reserves { get; set; }
        public string retained_earnings { get; set; }
        public string debt_through_other_comprehensive_income { get; set; }
        public string equity_through_other_comprehensive_income { get; set; }
        public string effective_portion_of_cash_flow_hedges { get; set; }
        public string revaluation_surplus { get; set; }
        public string exchange_diff_translating_of_foreign_operation { get; set; }
        public string other_items_of_other_comprehensive_income { get; set; }
        public string money_received_against_share_warrants { get; set; }
        public string non_controlling_interest { get; set; }
        public string given_other_equity { get; set; }
        public string given_liabilities_total { get; set; }
    }

    public class NBFCBSSubTotals
    {
        public string total_equity { get; set; }
        public string total_financial_liabilities { get; set; }
        public string total_non_financial_liabilities { get; set; }
        public string total_financial_assets { get; set; }
        public string total_non_financial_assets { get; set; }
        public string total_equity_and_liabilities { get; set; }
        public string total_assets { get; set; }
    }

    public class NBFCBSMetadata
    {
        public string filing_standard { get; set; }
        public string filing_type { get; set; }
        public string doc_id { get; set; }
    }


    public class NBFCPNL
    {
        public NBFCLineItems line_items { get; set; }       
        public NBFCRevenueBreakup revenue_breakup { get; set; }
        public NBFCDepreciationBreakup depreciation_breakup { get; set; }
        public NBFCPNLMetadata metadata { get; set; }
    }
    public class NBFCLineItems
    {
        public string total_interest_income { get; set; }
        public string total_dividend_income { get; set; }
        public string total_other_operating_income { get; set; }
        public string revenue { get; set; }
        public string other_income { get; set; }
        public string total_income { get; set; }
        public string interest { get; set; }
        public string total_cost_of_materials_consumed { get; set; }
        public string total_purchases_of_stock_in_trade { get; set; }
        public string total_changes_in_inventories_or_finished_goods { get; set; }
        public string total_employee_benefit_expense { get; set; }
        public string depreciation { get; set; }
        public string total_other_expenses { get; set; }
        public string total_expenses { get; set; }
        public string profit_before_tax_and_exceptional_items_before_tax { get; set; }
        public string total_exceptional_items { get; set; }
        public string profit_before_tax { get; set; }
        public string income_tax { get; set; }
        public string profit_Loss_for_the_period_from_continuing_operations { get; set; }
        public string profit_Loss_from_discontinued_operations_after_tax { get; set; }
        public string minority_interest_and_profit_from_associates_and_joint_ventures { get; set; }
        public string profit_after_tax { get; set; }      
    }
    public class NBFCRevenueBreakup
    {
        public string interest_income { get; set; }
        public string dividend_income { get; set; }
        public string rental_income { get; set; }
        public string fees_and_commission_income { get; set; }
        public string net_gain_on_fair_value_changes { get; set; }
        public string net_gain_on_derecoginition_of_fin_instruments { get; set; }
        public string sale_of_products { get; set; }
        public string sale_of_services { get; set; }
        public string other { get; set; }
    }
    public class NBFCDepreciationBreakup
    {
        public string depreciation_and_amortization_and_impairment { get; set; }
        
    }
    public class NBFCPNLMetadata
    {
        public string filing_standard { get; set; }
        public string filing_type { get; set; }
        public string doc_id { get; set; }
    }

    public class FinancialParameters
    {
        public string year { get; set; }
        public string nature { get; set; }
        public string earning_fc { get; set; }
        public string expenditure_fc { get; set; }
        public string transaction_related_parties_as_18 { get; set; }
        public string gross_fixed_assets { get; set; }
        public string trade_receivable_exceeding_six_months { get; set; }
        public string proposed_dividend { get; set; }       
    }

    public class IndustrySegments
    {
        public string industry { get; set; }
        public string[] segments { get; set; }
    }

    public class PrincipalBusinessActivities
    {
        public string year { get; set; }
        public string main_activity_group_code { get; set; }
        public string main_activity_group_description { get; set; }
        public string business_activity_code { get; set; }
        public string business_activity_description { get; set; }
        public string percentage_of_turnover { get; set; }
    }

    public class Shareholdings
    {        
        public string shareholders { get; set; }
        public string year { get; set; }
        public string category { get; set; }
        public string indian_held_no_of_shares { get; set; }
        public string indian_held_percentage_of_shares { get; set; }
        public string nri_held_no_of_shares { get; set; }
        public string nri_held_percentage_of_shares { get; set; }
        public string foreign_held_other_than_nri_no_of_shares { get; set; }
        public string foreign_held_other_than_nri_percentage_of_shares { get; set; }
        public string central_government_held_no_of_shares { get; set; }
        public string central_government_held_percentage_of_shares { get; set; }
        public string state_government_held_no_of_shares { get; set; }
        public string state_government_held_percentage_of_shares { get; set; }
        public string government_company_held_no_shares { get; set; }
        public string government_company_held_percentage_of_shares { get; set; }
        public string insurance_company_held_no_of_shares { get; set; }
        public string insurance_company_held_percentage_of_shares { get; set; }
        public string bank_held_no_of_shares { get; set; }
        public string bank_held_percentage_of_shares { get; set; }
        public string financial_institutions_held_no_of_shares { get; set; }
        public string financial_institutions_held_percentage_of_shares { get; set; }
        public string financial_institutions_investors_held_no_of_shares { get; set; }
        public string financial_institutions_investors_held_percentage_of_shares { get; set; }
        public string mutual_funds_held_no_of_shares { get; set; }
        public string mutual_funds_held_percentage_of_shares { get; set; }
        public string venture_capital_held_no_of_shares { get; set; }
        public string venture_capital_held_percentage_of_shares { get; set; }
        public string body_corporate_held_no_of_shares { get; set; }
        public string body_corporate_held_percentage_of_shares { get; set; }
        public string others_held_no_of_shares { get; set; }
        public string others_held_percentage_of_shares { get; set; }
        public string total_no_of_shares { get; set; }
        public string total_percentage_of_shares { get; set; }
    }

    public class ShareholdingsSummary
    {
        public string year { get; set; }
        public string total_equity_shares { get; set; }
        public string total_preference_shares { get; set; }
        public string promoter { get; set; }
        [JsonProperty("public")]
        public string publicShares { get; set; }
        public string total { get; set; }
        public ShareSummaryMetadata metadata { get; set; }
    }
    public class ShareSummaryMetadata
    {
        public string doc_id { get; set; }      
    }
    public class DirectorShareholdings
    {
        public string year { get; set; }
        public string din_pan { get; set; }
        public string full_name { get; set; }
        public string designation { get; set; }
        public string date_of_cessation { get; set; }
        public string no_of_shares { get; set; }
        public string percentage_holding { get; set; }
    }
    public class BIFRHistory
    {
        public string date { get; set; }
        public string case_number { get; set; }
        public string status { get; set; }
    }
    public class CDRHistory
    {
        public string date { get; set; }
        public string description { get; set; }
    }
    public class DefaulterList
    {
        public string date { get; set; }
        public string agency { get; set; }
        public string bank { get; set; }
        public string amount { get; set; }
    }

    public class LegalHistory
    {
        public string petitioner { get; set; }
        public string respondent { get; set; }
        public string court { get; set; }
        public string date { get; set; }
        public string case_status { get; set; }
        public string case_number { get; set; }
        public string case_type { get; set; }
        public string case_category { get; set; }
    }
    public class CreditRatings
    {
        public string rating_date { get; set; }
        public string rating_agency { get; set; }
        public string rating { get; set; }      
        public RatingDetails[] rating_details { get; set; }
        public string type_of_loan { get; set; }
        public string currency { get; set; }
        public string amount { get; set; }
    }
    public class RatingDetails
    {
        public string rating { get; set; }
        public string action { get; set; }
        public string outlook { get; set; }
        public string remarks { get; set; }
    }

    public class HoldingEntities
    {
        public EntityCompany[] company { get; set; }
        public EntityLLP[] llp { get; set; }
        public EntityOthers[] others { get; set; }
    }
    public class EntityCompany
    {
        public string cin { get; set; }
        public string legal_name { get; set; }
        public string paid_up_capital { get; set; }
        public string sum_of_charges { get; set; }
        public string incorporation_date { get; set; }
        public string share_holding_percentage { get; set; }
        public string city { get; set; }
        public string status { get; set; }
        public string active_compliance { get; set; }
        public string cirp_status { get; set; }
        public string next_cin { get; set; }
    }
    public class EntityLLP
    {
        public string llpin { get; set; }
        public string legal_name { get; set; }
        public string total_obligation_of_contribution { get; set; }
        public string sum_of_charges { get; set; }
        public string incorporation_date { get; set; }
        public string share_holding_percentage { get; set; }
        public string city { get; set; }
        public string status { get; set; }      
        public string cirp_status { get; set; }     
    }
    public class EntityOthers
    { 
        public string legal_name { get; set; }
        public string share_holding_percentage { get; set; }
    }
    public class SubsidiaryEntities
    {
        public EntityCompany[] company { get; set; }
        public EntityLLP[] llp { get; set; }
        public EntityOthers[] others { get; set; }
    }
    public class AssociateEntities
    {
        public EntityCompany[] company { get; set; }
        public EntityLLP[] llp { get; set; }
        public EntityOthers[] others { get; set; }
    }
    public class JointVentures
    {
        public EntityCompany[] company { get; set; }
        public EntityLLP[] llp { get; set; }
        public EntityOthers[] others { get; set; }
    }

    public class SecuritiesAllotment
    {
        public string allotment_type { get; set; }
        public string allotment_date { get; set; }
        public string instrument { get; set; }
        public string total_amount_raised { get; set; }
        public string number_of_securities_allotted { get; set; }
        public string nominal_amount_per_security { get; set; }
        public string premium_amount_per_security { get; set; }
    }
    public class PeerComparison
    {
        public string bizIndustry { get; set; }
        public string bizSegment { get; set; }
        public string refYear { get; set; }
        public Peers[] peers { get; set; }
        public Benchmarks[] benchMarks { get; set; }
    }
    public class Peers
    {
        public string cin { get; set; }
        public string legalName { get; set; }
        public string city { get; set; }
        public string revenue { get; set; }       
    }
    public class Benchmarks
    {
        public string year { get; set; }
        public string no_of_peers_in_sample { get; set; }
        public string revenue { get; set; }
        public string revenue_growth { get; set; }
        public string net_margin { get; set; }
        public string ebitda_margin { get; set; }
        public string return_on_equity { get; set; }
        public string sales_by_net_fixed_assets { get; set; }
        public string inventory_holding_period { get; set; }
        public string debtor_days_outstanding { get; set; }
        public string trade_payable_days { get; set; }
        public string cash_conversion_cycle { get; set; }
        public string debt_by_equity { get; set; }
        public string median_revenue { get; set; }
        public string median_revenue_growth { get; set; }
        public string median_net_margin { get; set; }
        public string median_ebitda_margin { get; set; }
        public string median_return_on_equity { get; set; }
        public string median_sales_by_net_fixed_assets { get; set; }
        public string median_inventory_holding_period { get; set; }
        public string median_debtor_days_outstanding { get; set; }
        public string median_trade_payable_days { get; set; }
        public string median_cash_conversion_cycle { get; set; }
        public string median_debt_by_equity { get; set; }
    }

    public class GSTDetails
    {
        public string gstin { get; set; }
        public string status { get; set; }
        public string company_name { get; set; }
        public string trade_name { get; set; }
        public string state { get; set; }
        public string state_jurisdiction { get; set; }
        public string centre_jurisdiction { get; set; }
        public string date_of_registration { get; set; }
        public string taxpayer_type { get; set; }
        public string nature_of_business_activities { get; set; }
        public Filings[] filings { get; set; }
    }

    public class Filings
    {
        public string return_type { get; set; }
        public string date_of_filing { get; set; }
        public string financial_year { get; set; }
        public string tax_period { get; set; }
        public string status { get; set; }
    }

    public class StruckOff248Details
    {
        public string struck_off_status { get; set; }
        public string restored_status { get; set; }
    }

    public class MdlDataStatusRequest
    {
        public string pan { get; set; }
        public string institution_gid { get; set; }
        public string application_gid { get; set; }
    }

    public class MdlDataStatusResponse : MdlProbe
    {
        public DataStatusMetadata metadata { get; set; }
        public DataStatusData data { get; set; }
    }

    public class DataStatusMetadata
    {
        public string api_version { get; set; }       
    }

    public class DataStatusData
    {
        public DataStatus data_status { get; set; }  
    }

    public class DataStatus
    {
        public string efiling_status { get; set; }
        public string next_cin { get; set; }
        public string last_base_updated { get; set; }
        public string last_details_updated { get; set; }
        public string last_fin_year_end { get; set; }
        public string last_filing_date { get; set; }
    }

    public class MdlInstitutionProbeDoc 
    {
        public string api_name { get; set; }
        public string probedocument_name { get; set; }
        public string probedocument_path { get; set; }
        public string created_date { get; set; }
        public List<institutionprobedoc_list> institutionprobedoc_list { get; set; }
        public List<institutionprobedoclog_list> institutionprobedoclog_list { get; set; }

    }

    public class institutionprobedoc_list
    {        
        public string institutionprobedocumentdetails_gid { get; set; }
        public string api_name { get; set; }
        public string probedocument_name { get; set; }
        public string probedocument_path { get; set; }
        public string created_date { get; set; }
    }

    public class institutionprobedoclog_list
    {
        public string institutionprobedocumentdetailslog_gid { get; set; }
        public string api_name { get; set; }
        public string apicall_status { get; set; }
        public string probedocument_name { get; set; }
        public string probedocument_path { get; set; }
        public string created_date { get; set; }
    }

    public class MdlProbeDocDetailsRequest 
    {
        public string pan { get; set; }
        public string institution_gid { get; set; }
        public string application_gid { get; set; }
    }

    public class MdlDocDetailsResponse : MdlProbe
    {
        
    }

    //LLP
    //Base Details
    public class MdlBaseDetailsLLPResponse : MdlProbe
    {
        public Metadata metadata { get; set; }
        public BaseDetailsDataLLP data { get; set; }
    }

    public class BaseDetailsDataLLP
    {
        public BaseLLP llp { get; set; }
        public DirectorsLLP[] directors { get; set; }
        public OpenChargesLLP[] open_charges { get; set; }
    }

    public class BaseLLP
    {
        public string llpin { get; set; }
        public string legal_name { get; set; }
        public string efiling_status { get; set; }
        public string cirp_status { get; set; }
        public string incorporation_date { get; set; }        
        public string sum_of_charges { get; set; }
        public string total_obligation_of_contribution { get; set; }       
        public RegiseredAddress registered_address { get; set; }
        public string classification { get; set; }
        public string last_financial_reporting_date { get; set; }
        public string last_annual_returns_filed_date { get; set; }
        public string email { get; set; }      
    }

    public class DirectorsLLP
    {
        public string pan { get; set; }
        public string din { get; set; }
        public string name { get; set; }
        public string designation { get; set; }
        public string din_status { get; set; }
        public string gender { get; set; }
        public string date_of_birth { get; set; }
        public string age { get; set; }
        public string date_of_appointment { get; set; }
        public string date_of_appointment_for_current_designation { get; set; }
        public string date_of_cessation { get; set; }
        public string nationality { get; set; }
        public string dsc_status { get; set; }
        public string dsc_expiry_date { get; set; }
        public string father_name { get; set; }
        public AddressDirector address { get; set; }
    }

    public class AddressDirector
    {
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string pincode { get; set; }
        public string country { get; set; }
    }

    public class OpenChargesLLP
    {
        public string id { get; set; }
        public string date { get; set; }
        public string holder_name { get; set; }
        public string amount { get; set; }
        public string type { get; set; }
    }

    //Comprehensive Details
    public class MdlComprehensiveDetailsLLPResponse : MdlProbe
    {
        public Metadata metadata { get; set; }
        public ComprehensiveDetailsDataLLP data { get; set; }
    }

    public class ComprehensiveDetailsDataLLP
    {
        public LLP llp { get; set; }
        public Description description { get; set; }
        public NameHistory[] name_history { get; set; }
        public DirectorsLLP[] directors { get; set; }
        public DirectorNetwork[] director_network { get; set; }
        public ContactDetails contact_details { get; set; }
        public OpenCharges[] open_charges { get; set; }
        public OpenChargesLatestEvent[] open_charges_latest_event { get; set; }
        public ChargeSequence[] charge_sequence { get; set; }
        public HoldingEntitiesLLP holding_entities { get; set; }
        public SubsidiaryEntitiesLLP subsidiary_entities { get; set; }
        public Financials[] financials { get; set; }
        public CreditRatings[] credit_ratings { get; set; }
        public ContributionDetails contribution_details { get; set; }
        public SummaryDesignatedPartners[] summary_designated_partners { get; set; }
        public IndustrySegments[] industry_segments { get; set; }
        public PrincipalBusinessActivitiesLLP[] principal_business_activities { get; set; }
        public GSTDetailsLLP[] gst_details { get; set; }
        public StruckOff248Details struckoff248_details { get; set; }
    }

    public class LLP
    {
        public string llpin { get; set; }
        public string legal_name { get; set; }
        public string efiling_status { get; set; }
        public string cirp_status { get; set; }
        public string incorporation_date { get; set; }
        public string sum_of_charges { get; set; }
        public string total_obligation_of_contribution { get; set; }
        public RegiseredAddress registered_address { get; set; }
        public string total_contribution_received { get; set; }
        public RegiseredAddress business_address { get; set; }       
        public string pan { get; set; }
        public string website { get; set; }
        public string classification { get; set; }
        public string last_financial_reporting_date { get; set; }
        public string last_annual_returns_filed_date { get; set; }
        public string email { get; set; }
    }

    public class DirectorNetworkLLP
    {
        public string name { get; set; }
        public string pan { get; set; }
        public string din { get; set; }
        public Network network { get; set; }

    }
    public class NetworkLLP
    {
        public DirNetworkCompany[] companies { get; set; }
        public DirNetworkLLP[] llps { get; set; }

    }
    
    public class DirNetworkLLP
    {
        public string llpin { get; set; }
        public string legal_name { get; set; }
        public string status { get; set; }
        public string incorporation_date { get; set; }
        public string total_obligation_of_contribution { get; set; }
        public string sum_of_charges { get; set; }
        public string city { get; set; }
        public string cirp_status { get; set; }
        public string designation { get; set; }
        public string date_of_appointment { get; set; }
        public string date_of_appointment_for_current_designation { get; set; }
        public string date_of_cessation { get; set; }
    }

    public class HoldingEntitiesLLP
    {
        public string financial_year { get; set; }
        public EntityCompany[] company { get; set; }
        public EntityLLP[] llp { get; set; }
        public EntityOthers[] others { get; set; }
    }

    public class SubsidiaryEntitiesLLP
    {
        public string financial_year { get; set; }
        public EntityCompany[] company { get; set; }
        public EntityLLP[] llp { get; set; }
        public EntityOthers[] others { get; set; }
    }

    public class FinancialsLLP
    {
        public string year { get; set; }        
        public string stated_on { get; set; }
        public StatementOfAssetsAndLiabilities statement_of_assets_and_liabilities { get; set; }
        public StatementOfIncomeAndExpenditure statement_of_income_and_expenditure { get; set; }
        public Certifiers certifiers { get; set; }
    }

    public class StatementOfAssetsAndLiabilities
    {
        public AssetsSAL assets { get; set; }
        public SubTotalsSAL subTotals { get; set; }
        public LiabilitiesSAL liabilities { get; set; }
        public MetadataSAL metadata { get; set; }
    }

    public class AssetsSAL
    {
        public string gross_fixed_assets { get; set; }
        public string depreciation_and_amortization { get; set; }
        public string investments { get; set; }
        public string loans_and_advances { get; set; }
        public string inventories { get; set; }
        public string trade_receivables { get; set; }
        public string cash_and_cash_equivalents { get; set; }
        public string other_assets { get; set; }
        public string net_fixed_assets { get; set; }
    }

    public class SubTotalsSAL
    {
        public string given_assets_total { get; set; }
        public string given_liabilities_total { get; set; }     
    }

    public class LiabilitiesSAL
    {
        public string contribution_received { get; set; }
        public string reserves_and_surplus { get; set; }
        public string secured_loan { get; set; }
        public string unsecured_loan { get; set; }
        public string short_term_borrowing { get; set; }
        public string trade_payables { get; set; }
        public string other_liabilities { get; set; }
        public string provisions_for_taxation { get; set; }
        public string provisions_for_contingencies { get; set; }
        public string provisions_for_insurance { get; set; }
        public string other_provisions { get; set; }
    }

    public class MetadataSAL
    {
        public string docId { get; set; }
    }

    public class StatementOfIncomeAndExpenditure
    {
        public LineItemsSIE assets { get; set; }
        public RevenueBreakupSIE revenue_breakup { get; set; }
        public DepreciationBreakUpSIE depreciation_breakup { get; set; }
        public MetadataSIE metadata { get; set; }
    }

    public class LineItemsSIE
    {
        public string net_revenue { get; set; }
        public string operating_cost { get; set; }
        public string total_cost_of_materials_consumed { get; set; }
        public string total_purchases_of_stock_in_trade { get; set; }
        public string total_changes_in_inventories_or_finished_goods { get; set; }
        public string total_employee_benefit_expense { get; set; }
        public string total_other_expenses { get; set; }
        public string operating_profit { get; set; }
        public string other_income { get; set; }
        public string depreciation { get; set; }
        public string profit_before_interest_and_tax { get; set; }
        public string interest { get; set; }
        public string profit_before_tax_and_exceptional_items_before_tax { get; set; }
        public string exceptional_items_before_tax { get; set; }
        public string profit_before_tax { get; set; }
        public string income_tax { get; set; }
        public string profit_for_period_from_continuing_operations { get; set; }
        public string profit_from_discontinuing_operation_after_tax { get; set; }
        public string minority_interest_and_profit_from_associates_and_joint_ventures { get; set; }
        public string profit_after_tax { get; set; }
    }

    public class RevenueBreakupSIE
    {
        public string sale_of_goods_manufactured_domestic { get; set; }
        public string sale_of_goods_traded_domestic { get; set; }
        public string sale_or_supply_of_services_domestic { get; set; }
        public string sale_of_goods_manufactured_export { get; set; }
        public string short_sale_of_goods_traded_exportterm_borrowing { get; set; }
        public string sale_or_supply_of_services_export { get; set; }
    }

    public class DepreciationBreakUpSIE
    {
        public string depreciation_and_amortization { get; set; }
    }
    public class MetadataSIE
    {
        public string docId { get; set; }
    }

    public class Certifiers
    {
        public string type { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public string address { get; set; }
        public string firm_id { get; set; }
        public string firm_name { get; set; }
    }

    public class ContributionDetails
    {
        public string financial_year { get; set; }
        public MetadataCD metadata { get; set; }
        //public IndividualPartners individual_partners { get; set; }
        public BodyCorporates[] body_corporates { get; set; }     
    }

    public class BodyCorporates
    {
        public string id { get; set; }
        public string id_type { get; set; }
        public string name { get; set; }
        public string obligation_contribution { get; set; }
        public string received_contribution { get; set; }
        public string nominee_id { get; set; }
        public string nominee_name { get; set; }
        public string nominee_id_type { get; set; }
    }
    public class MetadataCD
    {
        public string doc_id { get; set; }
    }

    public class SummaryDesignatedPartners
    {
        public string category { get; set; }
        public string partner { get; set; }
        public string indian_desig_partner { get; set; }
        public string other_desig_partner { get; set; }
        public string total { get; set; }
    }

    public class PrincipalBusinessActivitiesLLP
    {
        public string year { get; set; }
        public string business_classification { get; set; }
        public string principal_business_activities { get; set; }
    }

    public class GSTDetailsLLP
    {
        public string gstin { get; set; }
        public string status { get; set; }
        public string companyName { get; set; }
        public string tradeName { get; set; }
        public string state { get; set; }
        public string stateJurisdiction { get; set; }
        public string centreJurisdiction { get; set; }
        public string dateOfRegistration { get; set; }
        public string taxpayerType { get; set; }
        public string natureOfBusinessActivities { get; set; }
        public FilingsLLP[] filings { get; set; }
    }

    public class FilingsLLP
    {
        public string returnType { get; set; }
        public string dateOfFilling { get; set; }
        public string financialYear { get; set; }
        public string taxPeriod { get; set; }
        public string status { get; set; }
    }

    public static class ErrorResponseProbe
    {
        public const string
            errorResponse = "Error Response obtained - ";

    }


}