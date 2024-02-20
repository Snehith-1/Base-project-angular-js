(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCcCommitteeApplicationViewController', AgrMstCcCommitteeApplicationViewController);

    AgrMstCcCommitteeApplicationViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', '$anchorScroll', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstCcCommitteeApplicationViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, $anchorScroll, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCcCommitteeApplicationViewController';

        const lsdynamiclimitmanagementback = 'AgrMstCcCommitteeApplicationView';

        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lstab = $location.search().lstab;
        var lstab = $scope.lstab;
        const lspagename = 'AgrMstCcCommitteeApplicationView';
        const lspagetype = 'CC';

        lockUI();
        activate();
        function activate() {

            var params = {
                application_gid: $scope.application_gid,
                tmp_status: false
            }
            var url = 'api/AgrMstApplicationAdd/GetApplicationTradeList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MdlTradelist = resp.data.MdlTradedtl;
                if ($scope.MdlTradelist == null) {
                    $scope.Tradedivshow = true;
                }
                else {
                    $scope.Tradedivshow = false;
                    $scope.TradeEditdivshow = false;
                }
                unlockUI();
            });

            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

            var params = {
                application_gid: $scope.application_gid
            }

            var url = 'api/AgrMstApplicationView/GetApplicationBasicView';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.txtcustomer_urn = resp.data.customer_urn;
                $scope.txtvertical = resp.data.vertical_name;
                $scope.txtvertical_tag = resp.data.verticaltaggs_name;
                $scope.txtconstitution = resp.data.constitution_name;
                $scope.txt_strategicbusiness_unit = resp.data.businessunit_name;
                $scope.txtprimayvalue_chain = resp.data.primaryvaluechain_name;
                $scope.txtsecondaryvalue_chain = resp.data.secondaryvaluechain_name;
                $scope.txtvernacular_language = resp.data.vernacular_language;
                $scope.txtApplfrom_SA = resp.data.sa_status;
                $scope.txtSAM_associateID = resp.data.sa_id;
                $scope.txtSAM_associatename = resp.data.sa_name;
                $scope.txtcontactperson_name = resp.data.contactperson_name;
                $scope.txtbasicdesignation = resp.data.designation_type;
                $scope.txtlandline_number = resp.data.landline_no;
                $scope.txtsocial_capital = resp.data.social_capital;
                $scope.txttrade_capital = resp.data.trade_capital;
                $scope.borrower_flag = resp.data.borrower_flag;
                $scope.borrower_type = resp.data.borrower_type;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.txtproduct_name = resp.data.product_name;
                $scope.txtsector_name = resp.data.sector_name;
                $scope.txtcategory_name = resp.data.category_name;
                $scope.txtvariety_name = resp.data.variety_name;
                $scope.txtbotanical_name = resp.data.botanical_name;
                $scope.txtalternative_name = resp.data.alternative_name;
                $scope.txtprogram_name = resp.data.program_name;
                $scope.onboard_gid = resp.data.buyeronboard_gid;
            });
            var url = 'api/AgrTrnAppCreditUnderWriting/Getproductprogramgid';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lsproduct_gid = resp.data.product_gid;
                $scope.lsprogram_gid = resp.data.program_gid;

            });

            var url = 'api/AgrTrnAppCreditUnderWriting/Getrenewalamendmentflag';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lsrenewal_flag = resp.data.renewal_flag;
                $scope.lsamendment_flag = resp.data.amendment_flag;

            });

            var url = 'api/AgrMstApplicationView/GetGeneticDetailsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.geneticcode_list = resp.data.geneticdetails_list;
            });

            var url = 'api/AgrMstApplicationView/GetMobileMailDetailsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtprimary_number = resp.data.primary_mobileno;
                $scope.basicmobileno_list = resp.data.mobilenumber_list;
                $scope.txtprimary_emailassdress = resp.data.primary_email;
                $scope.mailaddress_list = resp.data.mail_list;
            });

            var url = 'api/AgrMstApplicationView/GetProductChargesDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtoveralllimit_amt = resp.data.overalllimit_amount;
                $scope.txtvalidity_year = resp.data.validityoveralllimit_year;
                $scope.txtvalidity_month = resp.data.validityoveralllimit_month;
                $scope.txtvalidity_days = resp.data.validityoveralllimit_days;
                $scope.txtcalculation_limitvalidity = resp.data.calculationoveralllimit_validity;
                $scope.loandtls_list = resp.data.mstLoan_list;
                $scope.buyer_list = resp.data.mstbuyer_list;
                $scope.txtpurposeof_loan = resp.data.enduse_purpose;
                $scope.txt_processingfee = resp.data.processing_fee;
                $scope.txtprocessing_collecttype = resp.data.processing_collectiontype;
                $scope.txtdoc_charges = resp.data.doc_charges;
                $scope.txtdoc_collecttype = resp.data.doccharge_collectiontype;
                $scope.txtfield_visitcharges = resp.data.fieldvisit_charge;
                $scope.txtfield_collecttype = resp.data.fieldvisit_collectiontype;
                $scope.txtadhoc_fee = resp.data.adhoc_fee;
                $scope.txtadhoccollection_type = resp.data.adhoc_collectiontype;
                $scope.txtlife_insurance = resp.data.life_insurance;
                $scope.txtlife_collectiontype = resp.data.lifeinsurance_collectiontype;
                $scope.txtaccident_insurance = resp.data.acct_insurance;
                $scope.txttotal_collectible = resp.data.total_collect;
                $scope.txttotal_deductible = resp.data.total_deduct;
                $scope.Collateral_list = resp.data.mstcollateral_list;
                $scope.txtproduct_type = resp.data.product_type;
                $scope.servicecharge_List = resp.data.servicecharge_List;
                $scope.txtsecurity_type = resp.data.security_type;
                $scope.txtsecurity_description = resp.data.security_description;
                $scope.txtsecurity_value = resp.data.security_value;
                $scope.txtsecurityassessed_date = resp.data.securityassessed_date;
                $scope.txtasset_id = resp.data.asset_id;
                $scope.txtroc_fillingid = resp.data.roc_fillingid;
                $scope.txtCERSAI_fillingid = resp.data.CERSAI_fillingid;
                $scope.txthypoobservation_summary = resp.data.hypoobservation_summary;
                $scope.txtprimary_security = resp.data.primary_security;
                $scope.application2hypothecation_gid = resp.data.application2hypothecation_gid;
                $scope.txtvalidityfrom_date = resp.data.validityfrom_date;
                $scope.txtvalidityto_date = resp.data.validityto_date;
            });

            var params = {
                application_gid: application_gid,
                statusupdated_by: 'RM'

            }
            var url = 'api/AgrMstApplicationView/GetVisitReportList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.VisitReport_List = resp.data.VisitReport_List;
            });

            var params = {
                application_gid: application_gid,
                statusupdated_by: 'RM'
            }
            var url = 'api/AgrMstApplicationView/GetGradingToolDtls';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.gradetoolsummary_list = resp.data.mstgradetoolsummary_list;
            });

            var url = 'api/AgrMstApplicationView/GetIndividualList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditIndividual_List = resp.data.individual_List;
            });

            var url = 'api/AgrMstApplicationView/GetInstitutionList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditInstitution_List = resp.data.institution_List;
            });

            var url = 'api/AgrMstApplicationView/GetRMDetailsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtrmdepartment_name = resp.data.department_name;
                $scope.txtrm_name = resp.data.RM_Name;
                $scope.txtappl_initiateddate = resp.data.applicationinitiated_date;
                $scope.txtunderwritten_date = resp.data.creditunderwritten_date;
                $scope.txtunderwritten_by = resp.data.creditunderwritten_by;
            });

            var params = {
                application_gid: application_gid,
                statusupdated_by: 'Credit',
            }
            var url = 'api/AgrMstApplicationGradingTool/GetGradingTool';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.gradingtoolcredit_list = resp.data.grading_list;

            });

            var params = {
                application_gid: application_gid,
                statusupdated_by: 'Credit',
            }
            var url = 'api/AgrMstApplicationVisitReport/GetVisitReportList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.VisitReportCreditList = resp.data.VisitReportList;

            });

            //Get CAM Document
            var url = 'api/AgrTrnAppCreditUnderWriting/GetCAM';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.camuploaddocument_list = resp.data.camdocument_list;
            });

            var params = {
                application_gid: application_gid
            }
            var url = "api/AgrTrnCAMGeneration/GetApp2CAM"
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lspath = resp.data.lspath;
                $scope.lsname = resp.data.lsname;
            });

        }

        // <!-- product & charges view start -->
        $scope.tradedtl_view = function (application2trade_gid){
            if($scope.tradedtl_view_flag == undefined || $scope.tradedtl_view_flag == ''){
                $scope.tradedtl_view_flag = true;}
            else if($scope.tradedtl_view_flag == true){
                $scope.tradedtl_view_flag = false;}
            else{$scope.tradedtl_view_flag = true;}
            $scope.application2trade_gid = application2trade_gid;
            $scope.TradeEditdivshow = true;
            var params = {
                application2trade_gid: application2trade_gid
            }
            lockUI();
            var url = 'api/AgrMstApplicationAdd/GetApplicationTradeViewdtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cboeditproduct_type = resp.data.producttype_name;
                $scope.txteditProductsubType = resp.data.productsubtype_name;
                $scope.rdbeditsalescontract_availability = resp.data.salescontract_availability;
                $scope.cboeditScopeoftransport = resp.data.scopeof_transport;
                $scope.cboeditScopeofloading = resp.data.scopeof_loading;
                $scope.cboeditScopeofunloading = resp.data.scopeof_unloading;
                $scope.cboeditScopeofqualityandquantity = resp.data.scopeof_qualityandquantity;
                $scope.cboeditScopeofmoisturegainloss = resp.data.scopeof_moisturegainloss;
                $scope.cboScopeofInsurance = resp.data.scopeof_insurance;
                unlockUI();
            });
    
            var params = {
                application2trade_gid: application2trade_gid,
                application_gid: $scope.application_gid,
                tmp_status: "true",
                application2loan_gid:''
            }
    
            var url = 'api/AgrMstApplicationAdd/GetTrade2WarehouseTmpDetail';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.tradewarehouse_list = resp.data.creditor2warehouse_list;
            });

            var url = 'api/AgrMstCreditorMaster/GetTrade2CreditorTmpDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.trade2creditor_list = resp.data.trade2creditor_list;
            });
    
        }

        $scope.view = function (applicationtrade2warehouse_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    
                    applicationtrade2warehouse_gid: applicationtrade2warehouse_gid
                }
                var url = 'api/AgrMstApplicationAdd/EditTrade2WarehouseDetail';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cbowarehouseagency = resp.data.warehouse_agency;
                    $scope.cbowarehouse_name = resp.data.warehouse_name;
                    $scope.cbowarehousetype_name = resp.data.typeofwarehouse_name;
                    $scope.txtvolume_uom = resp.data.volume_uom;
                    $scope.txtcapacity_volume = resp.data.totalcapacity_volume;
                    $scope.txtareacapacity = resp.data.totalcapacity_area;
                    $scope.txtareacapacity_uom = resp.data.area_uom;
                    $scope.cbowarehouseaddress = resp.data.warehouse_address;
                    $scope.txtcapacity_commodity = resp.data.capacity_commodity;
                    $scope.txtcapacity_panina = resp.data.capacity_panina;
                    
                    unlockUI();
                });
    
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }    

        $scope.OtherProducts_view = function (application2loan_gid, viewproduct_type) {

            $scope.txtviewproduct_type = viewproduct_type;
            if($scope.Products_flag == undefined || $scope.Products_flag == ''){
                $scope.Products_flag = true;}
            else if($scope.Products_flag == true){
                $scope.Products_flag = false;}
            else{$scope.tradedtl_view_flag = true;}
            var params1 = {
                application_gid: '',
                application2loan_gid: application2loan_gid,
                tmp_status: 'false',
            }
            var url = 'api/AgrMstApplicationEdit/GetLoan2Supplierdtl';
            SocketService.getparams(url, params1).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.MdlSupplierdtllist = resp.data.MdlSupplierdtl;
                } else {
                    unlockUI();
                } 
            });
            var params = {
                application_gid: $scope.application_gid,
                application2loan_gid:application2loan_gid,
                tmp_status: 'both',
            }
            var url = 'api/AgrMstApplicationEdit/GetLoan2SupplierPaymentdtl';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.MdlSupplierPaymentlist = resp.data.MdlSupplierPaymentdtl;
                } else {
                    unlockUI();
                }

            });
            var params = {
                application2loan_gid: application2loan_gid,
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationView/GetLoanProgramValueChain';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mstproductdtl_list = resp.data.mstproductdtl_list;
            });
            var params2 = {
                application_gid: $scope.application_gid,
                application2loan_gid: application2loan_gid,
                tmp_status: 'false',
            }
            var url = 'api/AgrMstApplicationEdit/GetLoan2Repaymentdtl';
            SocketService.getparams(url, params2).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.MdlrePaymentdtl = resp.data.MdlPaymentdtl;
                }
            });

            var param = {
                application_gid: $scope.application_gid,
                application2loan_gid: application2loan_gid,
            }
            var url = 'api/AgrMstApplicationEdit/GetEditLoanLimit';
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                $scope.lbloveralllimit_amount = resp.data.overalllimit_amount;

                $scope.onboarding_status = resp.data.onboarding_status;

                if (resp.data.overalllimit_amount == "0.00" || resp.data.onboarding_status == "Direct") {

                    $scope.zerofacility = true

                    $scope.txtloanfaility_amount = '0';

                }

                else {

                    $scope.zerofacility = false

                }

                $scope.lsloanfacility_amount = resp.data.loanfacility_amount;
                if (resp.data.loanfacility_amount == '' || resp.data.loanfacility_amount == null) {
                    $scope.lsloanfacility_amount = '0';
                }
            });
            var params = {
                application2loan_gid: application2loan_gid
            }
            var url = 'api/AgrMstApplicationEdit/LoanDetailsEdit';
            SocketService.getparams(url, params).then(function (resp) {

                $scope.txtfacilityreqon_date = resp.data.facilityrequested_date;
                $scope.cboProductTypelist = resp.data.producttype_gid;
                $scope.loansubproduct_name = resp.data.productsub_type;
                $scope.product_type = resp.data.product_type;
                $scope.lblproducttype = resp.data.product_type;
                $scope.lblproductsub_type = resp.data.productsub_type;
                if ($scope.lblproductsub_type == 'STF') {
                    $scope.stfmandatory = true;
                    $scope.STFdivshow = true;
                }
                else {
                    $scope.stfmandatory = false;
                    $scope.STFdivshow = false;
                }
                var params = {
                    loanproduct_gid: resp.data.producttype_gid,
                    application_gid: '',
                    application2loan_gid: ''
                }
                var url = 'api/AgrMstApplicationAdd/GetLoanSubProduct';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.loansubproductlist = resp.data.application_list;
                });

                $scope.cboProductSubTypelist = resp.data.productsubtype_gid;
                $scope.cboLoanTypelist = resp.data.loantype_gid;
                $scope.loan_type = resp.data.loan_type;
                if ($scope.loan_type == 'Secured') {
                    $scope.Collateralshow = true;
                }
                else {
                    $scope.Collateralshow = false;
                }
                $scope.cboSourceType = resp.data.source_type;
                $scope.txtguidelinevalue = resp.data.guideline_value;
               
                $scope.txtguideline_date = resp.data.guideline_date;
                $scope.txtmarketvalue_date = resp.data.marketvalue_date;
                $scope.txtmarketValue = resp.data.market_value;
                
                
                $scope.txtforcedsource_value = resp.data.forcedsource_value;
                
                
                $scope.txtcollateralSSV_value = resp.data.collateralSSV_value;
                
                
                $scope.txtforcedvalueassessed_on = resp.data.forcedvalueassessed_on;
                $scope.txtcolateralobservation_summary = resp.data.collateralobservation_summary;

                $scope.txtloanfaility_amount = resp.data.facilityloan_amount;
                
                
                $scope.txteditrate_interest = resp.data.rate_interest;
                $scope.txteditpanel_interest = resp.data.penal_interest;
                $scope.txteditvalidity_years = resp.data.facilityvalidity_year;
                $scope.txteditvalidity_months = resp.data.facilityvalidity_month;
                $scope.txteditvalidity_days = resp.data.facilityvalidity_days;
                $scope.txtoverallfacilityvalidity_limit = resp.data.facilityoverall_limit;
                //$scope.txtedittenure_years = resp.data.tenureproduct_year;
                //$scope.txtedittenure_months = resp.data.tenureproduct_month;
                $scope.txtedittenure_days = resp.data.tenureproduct_days;
                $scope.txteditoveralllimit_validity = resp.data.tenureoverall_limit;
                $scope.cboFacilityTypelist = resp.data.facility_type;
                $scope.cboFacilitymodelist = resp.data.facility_mode;
                $scope.cboprincipalfrequency = resp.data.principalfrequency_gid;
                $scope.cboInterestFrequency = resp.data.interestfrequency_gid;
                $scope.cboProgram = resp.data.program_gid;

                $scope.valuechainlist = resp.data.valuechainlist;

                $scope.rdbmilestone_applicablity = resp.data.milestone_applicability,
                $scope.rdbinsurance_applicability = resp.data.insurance_applicability,
                $scope.cbomilestonepaymenttype =  resp.data.milestonepayment_gid, 
                $scope.txtsapayout = resp.data.sa_payout,
                $scope.insurance_availability = resp.data.insurance_availability,
                $scope.txtinsurance_percent = resp.data.insurance_percent,
                $scope.txtinsurance_cost = resp.data.insurance_cost,
                $scope.txtnet_yield = resp.data.net_yield,
                   $scope.sa_status = resp.data.sa_status;
                if ($scope.sa_status == "Yes")
                    $scope.showsapayout = true;
                else
                    $scope.showsapayout = false;

                if ($scope.rdbmilestone_applicablity == "Yes") {
                    $scope.showmilestonepaymenttype = true;
                    $scope.disabledmilestonepaymenttype = false;
                }
                else { 
                    $scope.showmilestonepaymenttype = false;
                    $scope.disabledmilestonepaymenttype = true;
                } 

                $scope.rdbinterest_status = resp.data.interest_status;
                $scope.rdbmoratorium_status = resp.data.moratorium_status;
                $scope.cbomoratorium_type = resp.data.moratorium_type;
                $scope.txtmoratorium_startdate = resp.data.moratorium_startdate;
                $scope.txtmoratorium_enddate = resp.data.moratorium_enddate;
                $scope.txtenduse_purpose = resp.data.enduse_purpose; 
                $scope.rdbTradeOriginated = resp.data.trade_orginatedby,
                $scope.txtsabrokerage = resp.data.SA_Brokerage,
                $scope.txtholdingperiod = resp.data.holding_periods,
                $scope.txtholdingMonthlyprocurement = resp.data.holdingmonthly_procurement,
                $scope.txtextendedholdingperiod = resp.data.extendedholding_periods,
                $scope.txtextendedMonthlyprocurement = resp.data.extendedmonthly_procurement,
                $scope.txtcharges_extendedperiod = resp.data.charges_extendedperiod,
                $scope.txtcustomer_advance = resp.data.customer_advance,
                $scope.txtreimburesementof_expenses = resp.data.reimburesementof_expenses,
                $scope.txtreimburesementof_expensespenalty = resp.data.reimburesementof_expensespenalty,
                $scope.bankfunding_documentname = resp.data.bankfundingdata_filename,
                $scope.bankfunding_documentpath = resp.data.bankfundingdata_filepath,
                $scope.txtneedfor_stocking = resp.data.needfor_stocking,
                $scope.txtproduct_portfolio = resp.data.product_portfolio,
                $scope.txtproduction_capacity = resp.data.production_capacity,
                $scope.txtnatureof_operations = resp.data.natureof_operations,
                $scope.txtaveragemonthly_inventoryholding = resp.data.averagemonthly_inventoryholding,
                $scope.txtfinancialinstitutions_relationship = resp.data.financialinstitutions_relationship;
                $scope.txtProgramLimitValidityfrom = resp.data.programlimit_validdfrom,
                $scope.txtProgramLimitValidityTo = resp.data.programlimit_validdto,
                $scope.txtoverallprogramvalidity_limit = resp.data.programoverall_limit;

                
                var lbloveralllimit_amount = ($scope.lbloveralllimit_amount).replaceAll(',','');
                var lsamount = (parseFloat(lbloveralllimit_amount) - parseFloat($scope.txtloanfaility_amount));
                $scope.txtremaining = parseFloat(lsamount);

                if(resp.data.product_type=='Agri Receivable Finance (ARF)') 
                {
                    $scope.ARF_condition = true;
                }
                else {
                    $scope.ARF_condition = false;
                }
            });
           
    
        }

        $scope.suppliergsttrnview = function (MdlSupplierGSTdtllist) {
            var modalInstance = $modal.open({
                templateUrl: '/SupplierGSTDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.SupplierGSTdtl_list = MdlSupplierGSTdtllist;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.commodity_view = function (application2product_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/CommodityViewDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance', 'DownloaddocumentService', 'cmnfunctionService'];
            function ModalInstanceCtrl($scope, $modalInstance, DownloaddocumentService, cmnfunctionService) {
                lockUI();
                var params = {
                    application2product_gid: application2product_gid
                }
                var url = 'api/AgrMstApplicationEdit/GetAppCommodityDtls';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.commoditydtls = resp.data;
                        unlockUI();
                    }
                });
                lockUI();
                var params = {
                    application2product_gid: application2product_gid
                }
                var url = 'api/AgrMstApplicationEdit/GetAppCommodityGstList';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.commoditygststatuslist = resp.data.commoditygststatus;
                        unlockUI();
                    }
                });
                var url = 'api/AgrMstApplicationEdit/GetAppCommodityTradeProdctList';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.commodityTradeProdctlist = resp.data.commodityTradeProdct;
                        unlockUI();
                    }
                });

                var url = 'api/AgrMstApplicationEdit/GetAppCommodityCustomerpaymentList';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.commoditycustomerpayment = resp.data.commoditycustomerpayment;
                        unlockUI();
                    }
                });

                var url = 'api/AgrMstApplicationEdit/GetAppCommodityDocumentUploadList';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.commodityDocumentUpload = resp.data.commodityDocumentUpload;
                        unlockUI();
                    }
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.doc_downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadall_8 = function () {
                    for (var i = 0; i < $scope.commodityDocumentUpload.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.commodityDocumentUpload[i].commodityreport_filepath, $scope.commodityDocumentUpload[i].commodityreport_filename);
                    }
                }
                $scope.downloadall_9 = function () {
                    for (var i = 0; i < $scope.commodityDocumentUpload.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.commodityDocumentUpload[i].riskanalysisreport_filepath, $scope.commodityDocumentUpload[i].riskanalysisreport_filename);
                    }
                }

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
                }


            }
        }
        // <!-- product & charges view end -->

        var params =
        {
            application_gid: application_gid
        }
        var url = "api/AgrMstApplicationEdit/GetGroupSummary";
        SocketService.getparams(url, params).then(function (resp) {
            $scope.group_list = resp.data.group_list;
            angular.forEach($scope.group_list, function (value, key) {
                var params = {
                    group_gid: value.group_gid
                };

                var url = 'api/AgrMstApplicationView/GetGrouptoMemberList';
                SocketService.getparams(url, params).then(function (resp) {
                    value.groupmember_list = resp.data.groupmember_list;
                    value.expand = false;
                });
            });
        });

        $scope.uploadeddoc_Hypothecation = function (application2hypothecation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Hypothecationdocuments.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                      application2hypothecation_gid: application2hypothecation_gid
                  }
                var url = 'api/AgrMstApplicationView/GetHypoDocDtl';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.Hypothecationdoc_list = resp.data.HypoDocumentList;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.download_Hypothecationdoc = function (val1, val2) {
                    //var phyPath = val1;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //link.download = val2;
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadall_2 = function () {
                    for (var i = 0; i < $scope.Hypothecationdoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.Hypothecationdoc_list[i].document_path, $scope.Hypothecationdoc_list[i].document_name);
                    }
                }


            }

        }

        $scope.uploadeddoc_Collateral = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Collateraldocuments.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       application2loan_gid: application2loan_gid
                   }
                var url = 'api/AgrMstApplicationView/GetCollateralDocDtl';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.Collateraldoc_list = resp.data.CollatralDocumentList;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.download_Collateraldoc = function (val1, val2) {
                    //var phyPath = val1;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //link.download = val2;
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadall_3 = function () {
                    for (var i = 0; i < $scope.Collateraldoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name);
                    }
                }

            }

        }

        $scope.PurposeofLoanOther_view = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/PurposeOfLoanOther.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       application2loan_gid: application2loan_gid
                   }
                var url = 'api/MstApplicationView/GetPurposeofLoan';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtpurposeof_loan = resp.data.enduse_purpose;

                });
                var url = 'api/AgrMstApplicationView/GetLoanProgramValueChain';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.program = resp.data.program;
                    $scope.primaryvaluechain_name = resp.data.primaryvaluechain_name;
                    $scope.secondaryvaluechain_name = resp.data.secondaryvaluechain_name;
                    $scope.product_gid = resp.data.product_gid;
                    $scope.product_name = resp.data.product_name;
                    $scope.variety_gid = resp.data.variety_gid;
                    $scope.variety_name = resp.data.variety_name;
                    $scope.sector_name = resp.data.sector_name;
                    $scope.category_name = resp.data.category_name;
                    $scope.botanical_name = resp.data.botanical_name;
                    $scope.alternative_name = resp.data.alternative_name;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.Buyer_view = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/BuyerDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                      application2loan_gid: application2loan_gid
                  }
                var url = 'api/AgrMstApplicationView/GetLoantoBuyerList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.buyer_list = resp.data.mstbuyer_list;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.Back = function () {
            if (lspage == 'ScheduleMeeting') {
                $location.url('app/AgrTrnCreditCommitteeSummary');
            }
            else if (lspage == 'ScheduledMeetingsummary') {
                $state.go('app.AgrTrnCCscheduledSummary');
            }
            else if (lspage == 'CompletedMeetingsummary') {
                $state.go('app.AgrTrnCCCompletedSummary');
            }
            else if (lspage == 'CCMmeetingScheduledcompleted') {
                $state.go('app.AgrTrnCcCompletedScheduledMeeting');
            }
            else if (lstab == 'RejectHoldAppl') {
                $state.go('app.AgrMstRejectandHoldSummary');
            }
            else if (lstab == 'CCSkippedAppl') {
                $state.go('app.AgrMstCCSkippedApplicationSummary');
            }
            else if (lstab == 'SubmittedToApproval') {
                $state.go('app.AgrMstSubmittedtoApprovalSummary');
            }
            else if (lstab == 'SubmittedToCC') {
                $state.go('app.AgrMstSubmittedtoCCSummary');
            }
            else if (lstab == 'CreditApproval') {
                $state.go('app.AgrTrnCreditApprovalSummary');
            }
            else if (lstab == 'CreditApproved') {
                $state.go('app.AgrTrnCreditApprovedSummary');
            }
            else if (lstab == 'CreditSubmittedtoCC') {
                $state.go('app.AgrTrnCreditSubmittedtoCCSummary');
            }
            else if (lstab == 'CreditCCSkipped') {
                $state.go('app.AgrTrnCreditCCSkippedSummary');
            }
            else if (lstab == 'CreditRejectHold') {
                $state.go('app.AgrTrnCreditRejectandHoldSummary');
            }
            else if (lstab == 'Pencreditmapping') {
                $state.go('app.AgrApplicationAssignmentSummary');
            }
            else if (lstab == 'Asscreditmapping') {
                $state.go('app.AgrAppassignedAssignmentSummary');
            }
            else if (lstab == 'ApplSubmittedToCC') {
                $state.go('app.AgrApplSubmittedtoCCSummary');
            }
            else if (lspage == 'SentBackToCredit') {
                $state.go('app.AgrMstSentbackcctoCredit');
            }
            else if (lstab == 'ProductSubmittedtoApproval') {
                $state.go('app.AgrMstProductSubmittedtoApprovalSummary');
            }
            else if (lstab == 'ProductDescRejected') {
                $state.go('app.AgrMstProductRejectedApplSummary');
            }
            else if (lstab == 'ProductDescMyApproval') {
                $state.go('app.AgrMstProductcDescApprovalSummary');
            }
            else if (lstab == 'CCApproved') {
                $state.go('app.AgrMstCCApprovedSummary');
            }
            else if (lstab == 'CreditRejectRevokeAppl') {
                $state.go('app.AgrMstCreditRevokeSummary');
            }
            else if (lstab == 'CreditHoldRevokeAppl') {
                $state.go('app.AgrMstCreditHoldRevokeSummary');
            }
            else if (lstab == 'CreditRevokedAppl') {
                $state.go('app.AgrMstCreditRevokedApplSummary');
            }
            else if (lstab == 'CreditStage') {
                $state.go('app.AgrMstCreditStageSummary');
            }
            else if (lstab == 'CcStage') {
                $state.go('app.AgrMstCcStageSummary');
            }
            else if (lstab == 'ProductHoldRevokeAppl') {
                $state.go('app.AgrMstProductHoldRevokeSummary');
            }
            else if (lstab == 'ProductRejectRevokeAppl') {
                $state.go('app.AgrMstProductRevokeSummary');
            }
            else if (lstab == 'ProductRevokedAppl') {
                $state.go('app.AgrMstProductRevokedApplSummary');
            }
            else if (lstab == 'ProductStage') {
                $state.go('app.AgrProductStageSummary');
            }
            else if (lspage  == 'CreditManagerRejectRevokeAppl'){
                $state.go('AgrMstCreditManagerRejectRevokeSummary');
            }
            else {
                $state.go('app.AgrTrnCcScheduledMeetingSummary');
            }
        }

        $scope.Kyc_view = function () {
            var application_gid = $scope.application_gid;
            $location.url('app/AgrMstCcCommitteeKycView?application_gid=' + application_gid + '&lstab=' + lstab);
        }

        $scope.gradingtool_view = function (application2gradingtool_gid) {
            var application2gradingtool_gid = application2gradingtool_gid;
            localStorage.setItem('application2gradingtool_gid', application2gradingtool_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrMstApplCreationGradingToolView";
            window.open(URL, '_blank');
        }

        $scope.visitreport_view = function (visitreport_gid) {
            var visitreport_gid = visitreport_gid;
            localStorage.setItem('visitreport_gid', visitreport_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrMstApplCreationVisitReportView";
            window.open(URL, '_blank');
        }

        $scope.institution_view = function (institution_gid) {
            var institution_gid = institution_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('institution_gid', institution_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnCCCommitteeInstitutionView?application_gid=" + application_gid + '&institution_gid='+ institution_gid + '&lspage='+ lspage +'&lspagetype='+ lspagetype;
            window.open(URL, '_blank');
        }

        $scope.individual_view = function (contact_gid) {
            var contact_gid = contact_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('contact_gid', contact_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnCCCommitteeIndividualView?application_gid=" + application_gid + '&contact_gid='+ contact_gid + '&lspage='+ lspage +'&lspagetype='+lspagetype;
            window.open(URL, '_blank');
        }

        $scope.group_view = function (group_gid) {
            var group_gid = group_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('group_gid', group_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnCCCommitteeGroupView";
            window.open(URL, '_blank');
        }

        $scope.member_view = function (contact_gid) {
            var contact_gid = contact_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('contact_gid', contact_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnCcCommitteeIndividualView?application_gid=" + application_gid + '&contact_gid='+ contact_gid + '&lspage='+ lspage +'&lspagetype='+lspagetype;
            window.open(URL, '_blank');
        }

        $scope.downloadsdocument = function (val1, val2) {
            //var phyPath = val1;
            //console.log(val1)
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.onboardappdetailinfo = function (onboard_gid) {
            $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab + '&lsparent=AppCCCommitteeView'));
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.camuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.camuploaddocument_list[i].document_path, $scope.camuploaddocument_list[i].document_name);
            }
        }
        $scope.downloadall_2 = function () {
            for (var i = 0; i < $scope.Hypothecationdoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.Hypothecationdoc_list[i].document_path, $scope.Hypothecationdoc_list[i].document_name);
            }
        }
        $scope.downloadall_3 = function () {
            for (var i = 0; i < $scope.Collateraldoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name);
            }
        }
        $scope.productcomaparisonview = function (onboard_gid, program_gid, product_gid) {
            //$location.url('app/AgrMstByrProductcomparisonView?hash=' + cmnfunctionService.encryptURL("&onboard_gid=" + onboard_gid));
            $location.url('app/AgrMstByrProductcomparisonView?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab + '&lsparent=AppCCCommitteeView' + '&product_gid=' + product_gid + '&program_gid=' + program_gid));

        }

    }
})();
