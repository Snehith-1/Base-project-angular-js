// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstByrProductcomparisonViewController', AgrMstByrProductcomparisonViewController);

    AgrMstByrProductcomparisonViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService', 'DownloaddocumentService'];

    function AgrMstByrProductcomparisonViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService, DownloaddocumentService) {

        var vm = this;

        vm.title = 'AgrMstByrProductcomparisonViewController';
        //const lsdynamiclimitmanagementback = 'AgrMstOnboardingApplicationInfo';

        /*    var banktransc_gid = $location.search().banktransc_gid;*/
        //var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        //var application2loan_gid = searchObject.application2loan_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.application2loan_gid = searchObject.application2loan_gid;
        var application2loan_gid = $scope.application2loan_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var onboard_gid = searchObject.onboard_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var lspage = searchObject.lspage;
        var lstab = searchObject.lstab;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.program_gid = searchObject.program_gid;
        var program_gid = $scope.program_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.product_gid = searchObject.product_gid;
        var product_gid = $scope.product_gid;
        $scope.application_gid = searchObject.application_gid;
        var application_gid = $scope.application_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var onboard_gid = searchObject.onboard_gid;
        var lsdynamiclimitmanagementback = searchObject.lsdynamiclimitmanagementback;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.lsparent = searchObject.lsparent;
        var lsparent = $scope.lsparent;
        $scope.appcreditapproval_gid = searchObject.appcreditapproval_gid;
        var appcreditapproval_gid = $scope.appcreditapproval_gid;
        activate();
        function activate() {
            $scope.listhighlight_flag = 'N';
            $scope.listhighlight_flag1 = 'N';
            var params = {
                onboard_gid: onboard_gid,
                product_gid: product_gid,
                program_gid: program_gid
                
            }
            var url = 'api/AgrTrnAppCreditUnderWriting/Getoldnewapplication';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.newapplication_gid = resp.data.newapplication_gid;
                $scope.oldapplication_gid = resp.data.oldapplication_gid;
                //$scope.newapplication2loan_gid = resp.data.newapplication2loan_gid;
                //$scope.oldapplication2loan_gid = resp.data.oldapplication2loan_gid;  
                $scope.newapplicationno = resp.data.newapplicationno;
                $scope.oldapplicationno = resp.data.oldapplicationno;
                $scope.newproduct_type = resp.data.newproduct_type;
                $scope.oldproduct_type = resp.data.oldproduct_type; 

                var params = {
                    application_gid: $scope.newapplication_gid
                }

                var url = 'api/AgrMstApplicationView/GetProductChargesDtl';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();                   
                    $scope.newloandtls_list = resp.data.mstLoan_list;
                   
                }); 
                var params = {
                    application_gid: $scope.oldapplication_gid
                }

                var url = 'api/AgrMstApplicationView/GetProductChargesDtl';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.loandtls_list = resp.data.mstLoan_list;

                }); 
                var params = {
                    application2loan_gid: '',
                    application_gid: oldapplication_gid
                }
                var url = 'api/AgrTrnAppCreditUnderWriting/Getproductapplicationloangid';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        //$scope.newapplication2loan_gid = resp.data.newapplication2loan_gid;
                        $scope.loandtls_list = resp.data.mstLoan_list;
                        unlockUI();
                    }
                });
                var params = {
                    application2loan_gid: '',
                    application_gid: newapplication_gid
                }
                var url = 'api/AgrTrnAppCreditUnderWriting/Getproductapplicationloangid';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        //$scope.newapplication2loan_gid = resp.data.newapplication2loan_gid;
                        $scope.loandtls_list = resp.data.mstLoan_list;
                        unlockUI();
                    }
                });

               

            });
         

        }

        $scope.OtherProducts_oldview = function (oldapplication2loan_gid, product_type, newapplication_gid, oldapplication_gid, index) {

            $scope.NAflag = '';

            var newapplication_gid = newapplication_gid;
            var oldapplication_gid = oldapplication_gid;
            //var list1= ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20'];
          
            var number = index;

            //if (list1[0] == number || list1[1] == number || list1[2] == number || list1[3] == number || list1[4] == number) {
            //    $scope.listhighlight_flag ='Y';
            //}
            //else {
            //    $scope.listhighlight_flag = 'N';
            //    $scope.listhighlight_flag1 = 'N';
            //}
            if ($scope.Products_flag == undefined || $scope.Products_flag == '') {
                $scope.Products_flag = true;
            }
            else if ($scope.Products_flag == true) {
                $scope.Products_flag = false;
                $scope.NAflag = '';
                $scope.NAoldflag = '';
                var params = {
                    application2loan_gid:'',
                    application_gid: oldapplication_gid
                }
                var url = 'api/AgrTrnAppCreditUnderWriting/Getproductapplicationloangid';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                       
                        //$scope.newapplication2loan_gid = resp.data.newapplication2loan_gid;
                        $scope.loandtls_list = resp.data.mstLoan_list;
                     
                       unlockUI();
                    }
                });
                activate();

                $scope.NAflag = '';
                $scope.NAoldflag = '';
            }
            else { $scope.tradedtl_view_flag = true; }

            $scope.newproduct_type = product_type;
            $scope.oldproduct_type = product_type; 
             if ((oldapplication_gid != null || oldapplication_gid != '' ||oldapplication_gid != undefined) && (oldapplication2loan_gid != null ||oldapplication2loan_gid != '' || oldapplication2loan_gid != undefined))
                {
                    var params = {
                        application_gid:oldapplication_gid
                    }

                    var url = 'api/AgrMstApplicationView/GetProductChargesDtl';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.txtoveralllimit_amt = resp.data.overalllimit_amount;
                        $scope.txtvalidity_year = resp.data.validityoveralllimit_year;
                        $scope.txtvalidity_month = resp.data.validityoveralllimit_month;
                        $scope.txtvalidity_days = resp.data.validityoveralllimit_days;
                        $scope.txtcalculation_limitvalidity = resp.data.calculationoveralllimit_validity;                     
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
                        $scope.txtapplication_no = resp.data.application_no;
                    });
                  
                    var params = {
                        application_gid: '',
                        application2loan_gid:oldapplication2loan_gid,
                        tmp_status: 'false',
                    }
                    var url = 'api/AgrMstApplicationEdit/GetLoan2Supplierdtl';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            $scope.MdlSupplierdtllist = resp.data.MdlSupplierdtl;
                        } else {
                            unlockUI();
                        }
                    });
                 var params = {
                     application_gid: $scope.newapplication_gid
                 }

                 var url = 'api/AgrMstApplicationView/GetProductChargesDtl';
                 SocketService.getparams(url, params).then(function (resp) {
                     unlockUI();
                     $scope.newloandtls_list = resp.data.mstLoan_list;

                 }); 
                    var params = {
                        application_gid:oldapplication_gid,
                        application2loan_gid: oldapplication2loan_gid,
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
                        application2loan_gid:oldapplication2loan_gid,
                        application_gid:oldapplication_gid
                    }
                    var url = 'api/AgrMstApplicationView/GetLoanProgramValueChain';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.mstproductdtl_list = resp.data.mstproductdtl_list;
                    });
                    var params2 = {
                        application_gid:oldapplication_gid,
                        application2loan_gid:oldapplication2loan_gid,
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
                        application_gid:oldapplication_gid,
                        application2loan_gid:oldapplication2loan_gid,
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
                        application2loan_gid:oldapplication2loan_gid
                    }
                    var url = 'api/AgrMstApplicationEdit/LoanDetailsEdit';
                    SocketService.getparams(url, params).then(function (resp) {

                        $scope.txtfacilityreqon_date = resp.data.facilityrequested_date;
                        $scope.cboProductTypelist = resp.data.producttype_gid;
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
                        $scope.loansubproduct_name = resp.data.productsub_type;
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
                            $scope.cbomilestonepaymenttype = resp.data.milestonepayment_gid,
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

                        

                        if (resp.data.product_type == 'Agri Receivable Finance (ARF)') {
                            $scope.ARF_condition = true;
                        }
                        else {
                            $scope.ARF_condition = false;
                        }
                    });
                }

            var params = {               
                application2loan_gid: oldapplication2loan_gid,
                application_gid: oldapplication_gid
            }
            var url = 'api/AgrTrnAppCreditUnderWriting/Getproductapplicationloangid';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.newapplication2loan_gid = resp.data.newapplication2loan_gid;
                    $scope.loandtls_list = resp.data.mstLoan_list;
                    if ($scope.newapplication2loan_gid == undefined || $scope.newapplication2loan_gid == null || $scope.newapplication2loan_gid == '') {
                        $scope.NAoldflag = 'Y';
                        $scope.NAflag = '';
                    }
                    else {
                        $scope.NAoldflag = '';
                        $scope.NAflag = '';

                    }
                } else {
                    unlockUI();
                }
          

                if ((newapplication_gid != null || newapplication_gid != '' || newapplication_gid != undefined) && ($scope.newapplication2loan_gid != null || $scope.newapplication2loan_gid != '' || $scope.newapplication2loan_gid != undefined))
                {
                    var params = {
                        application_gid: newapplication_gid
                    }

                    var url = 'api/AgrMstApplicationView/GetProductChargesDtl';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.txtoveralllimit_amt = resp.data.overalllimit_amount;
                        $scope.txtvalidity_year = resp.data.validityoveralllimit_year;
                        $scope.txtvalidity_month = resp.data.validityoveralllimit_month;
                        $scope.txtvalidity_days = resp.data.validityoveralllimit_days;
                        $scope.txtcalculation_limitvalidity = resp.data.calculationoveralllimit_validity;                       
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
                        $scope.newCollateral_list = resp.data.mstcollateral_list;
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
                        $scope.txtapplication_no = resp.data.application_no;
                    });
                    if ($scope.Products_flag1 == undefined || $scope.Products_flag1 == '') {
                        $scope.Products_flag1 = true;
                    }
                    else if ($scope.Products_flag1 == true) {
                        $scope.Products_flag1 = false;
                    }
                    else { $scope.tradedtl_view_flag1 = true; }

                    if(($scope.newapplication2loan_gid !=null || $scope.newapplication2loan_gid!=undefined)&&(oldapplication2loan_gid !=null || oldapplication2loan_gid!=undefined)){
                        var params = {
                            application_gid: '',
                            newapplication2loan_gid: $scope.newapplication2loan_gid,
                            oldapplication2loan_gid: oldapplication2loan_gid,
                            tmp_status: 'false',
                        }
                        var url = 'api/AgrTrnAppCreditUnderWriting/GetLoan2Supplierdtl';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            $scope.MdlNewSupplierdtllist = resp.data.MdlSupplierdtl;
                        } else {
                            unlockUI();
                        }
                    });
                    }
                    else{
                        var params1 = {
                            application_gid: '',
                            application2loan_gid: $scope.newapplication2loan_gid,
                            tmp_status: 'false',
                        }
                        var url = 'api/AgrMstApplicationEdit/GetLoan2Supplierdtl';
                        SocketService.getparams(url, params1).then(function (resp) {
                            if (resp.data.status == true) {
                                unlockUI();
                                $scope.MdlNewSupplierdtllist = resp.data.MdlSupplierdtl;
                            } else {
                                unlockUI();
                            }
                        });
                    }


                    if(($scope.newapplication2loan_gid !=null || $scope.newapplication2loan_gid!=undefined)&&(oldapplication2loan_gid !=null || oldapplication2loan_gid!=undefined)){
                        var params = {
                            application_gid: newapplication_gid,
                            newapplication2loan_gid: $scope.newapplication2loan_gid,
                            oldapplication2loan_gid:oldapplication2loan_gid,
                            tmp_status: 'both',
                        }
                        var url = 'api/AgrTrnAppCreditUnderWriting/GetLoan2SupplierPaymentdtl';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                unlockUI();
                                $scope.MdlNewSupplierPaymentlist = resp.data.MdlSupplierPaymentdtl;
                            } else {
                                unlockUI();
                            }

                        });
                    }
                    else{
                        var params = {
                            application_gid: newapplication_gid,
                            application2loan_gid: $scope.newapplication2loan_gid,
                            tmp_status: 'both',
                        }
                        var url = 'api/AgrMstApplicationEdit/GetLoan2SupplierPaymentdtl';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            $scope.MdlNewSupplierPaymentlist = resp.data.MdlSupplierPaymentdtl;
                        } else {
                            unlockUI();
                        }

                    });
                    }
                    if(($scope.newapplication2loan_gid !=null || $scope.newapplication2loan_gid!=undefined)&&(oldapplication2loan_gid !=null ||oldapplication2loan_gid!=undefined))
                    {
                        var params = {
                            newapplication2loan_gid: $scope.newapplication2loan_gid,
                            oldapplication2loan_gid: oldapplication2loan_gid,
                            application_gid: newapplication_gid
                        }
                        var url = 'api/AgrTrnAppCreditUnderWriting/GetLoanProgramValueChain';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.mstnewproductdtl_list = resp.data.mstproductdtl_list;
                        });
                    }
                    else{
                        var params = {
                            application2loan_gid: $scope.newapplication2loan_gid,
                            application_gid: newapplication_gid
                        }
                        var url = 'api/AgrMstApplicationView/GetLoanProgramValueChain';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.mstnewproductdtl_list = resp.data.mstproductdtl_list;
                        });
                    }


                     var params = {
                         newapplication2loan_gid: $scope.newapplication2loan_gid,
                         oldapplication2loan_gid:oldapplication2loan_gid
                     }
                     var url = 'api/AgrTrnAppCreditUnderWriting/Getproductvariety';
                     lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                         //$scope.mstnewproductdtl_listflag = resp.data.mstnewproductdtl_listflag;
                         //$scope.MdlNewSupplierdtllistflag = resp.data.MdlNewSupplierdtllistflag;
                         //$scope.MdlNewSupplierPaymentlistflag = resp.data.MdlNewSupplierPaymentlistflag;
                         $scope.Mdlcollateralobservation_summaryflag = resp.data.Mdlcollateralobservation_summaryflag;
                         $scope.Mdlsource_typeflag = resp.data.Mdlsource_typeflag;

                     });

                    var params2 = {
                        application_gid: newapplication_gid,
                        application2loan_gid: $scope.newapplication2loan_gid,
                        tmp_status: 'false',
                    }
                    var url = 'api/AgrMstApplicationEdit/GetLoan2Repaymentdtl';
                    SocketService.getparams(url, params2).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            $scope.MdlNewrePaymentdtl = resp.data.MdlPaymentdtl;
                        }
                    });

                    var param = {
                        application_gid: newapplication_gid,
                        application2loan_gid: $scope.newapplication2loan_gid,
                    }
                    var url = 'api/AgrMstApplicationEdit/GetEditLoanLimit';
                    SocketService.post(url, param).then(function (resp) {
                        unlockUI();
                        $scope.lblnewoveralllimit_amount = resp.data.overalllimit_amount;

                        $scope.onboarding_status = resp.data.onboarding_status;

                        if (resp.data.overalllimit_amount == "0.00" || resp.data.onboarding_status == "Direct") {

                            $scope.zerofacility = true

                            $scope.txtnewloanfaility_amount = '0';

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
                        application2loan_gid:  $scope.newapplication2loan_gid
                    }
                    var url = 'api/AgrMstApplicationEdit/LoanDetailsEdit';
                    SocketService.getparams(url, params).then(function (resp) {

                        $scope.txtnewfacilityreqon_date = resp.data.facilityrequested_date;
                        if ($scope.txtfacilityreqon_date != $scope.txtnewfacilityreqon_date) {

                            $scope.txtfacilityreqon_dateflag = 'Y';
                        }
                        else {
                            $scope.txtfacilityreqon_dateflag = 'N';
                        }
                        $scope.cbonewProductTypelist = resp.data.producttype_gid;
                        $scope.newproduct_type = resp.data.product_type;
                        $scope.lblnewproducttype = resp.data.product_type;
                        $scope.lblnewproductsub_type = resp.data.productsub_type;
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
                            $scope.newloansubproductlist = resp.data.application_list;
                        });

                        $scope.cbonewProductSubTypelist = resp.data.productsubtype_gid;
                        $scope.loannewsubproduct_name = resp.data.productsub_type;
                        $scope.cbonewLoanTypelist = resp.data.loantype_gid;
                        $scope.newloan_type = resp.data.loan_type;
                        if ($scope.newloan_type == 'Secured') {
                            $scope.Collateralshow = true;
                        }
                        else {
                            $scope.Collateralshow = false;
                        }
                        if ($scope.newloan_type != $scope.loan_type) {

                            $scope.loan_typeflag = 'Y';
                        }
                        else {
                            $scope.loan_typeflag = 'N';
                        }

                        $scope.cbonewSourceType = resp.data.source_type;
                        $scope.txtnewguidelinevalue = resp.data.guideline_value;

                        $scope.txtnewguideline_date = resp.data.guideline_date;
                        $scope.txtnewmarketvalue_date = resp.data.marketvalue_date;
                        $scope.txtnewmarketValue = resp.data.market_value;


                        $scope.txtnewforcedsource_value = resp.data.forcedsource_value;


                        $scope.txtnewcollateralSSV_value = resp.data.collateralSSV_value;


                        $scope.txtnewforcedvalueassessed_on = resp.data.forcedvalueassessed_on;
                        $scope.txtnewcolateralobservation_summary = resp.data.collateralobservation_summary;

                        $scope.txtnewloanfaility_amount = resp.data.facilityloan_amount;
                        if ($scope.txtnewloanfaility_amount != $scope.txtloanfaility_amount) {

                            $scope.txtloanfaility_amountflag = 'Y';
                        }
                        else {
                            $scope.txtloanfaility_amountflag = 'N';
                        }


                        $scope.txtneweditrate_interest = resp.data.rate_interest;
                        if ($scope.txtneweditrate_interest != $scope.txteditrate_interest) {

                            $scope.txteditrate_interestflag = 'Y';
                        }
                        else {
                            $scope.txteditrate_interestflag = 'N';
                        }
                        $scope.txtneweditpanel_interest = resp.data.penal_interest;

                        if ($scope.txteditpanel_interest != $scope.txtneweditpanel_interest) {

                            $scope.txteditpanel_interestflag = 'Y';
                        }
                        else {
                            $scope.txteditpanel_interestflag = 'N';
                        }
                        $scope.txtneweditvalidity_years = resp.data.facilityvalidity_year;
                        $scope.txtneweditvalidity_months = resp.data.facilityvalidity_month;
                        $scope.txtneweditvalidity_days = resp.data.facilityvalidity_days;
                        $scope.txtnewoverallfacilityvalidity_limit = resp.data.facilityoverall_limit;
                        //$scope.txtnewedittenure_years = resp.data.tenureproduct_year;
                        //$scope.txtnewedittenure_months = resp.data.tenureproduct_month;
                        $scope.txtnewedittenure_days = resp.data.tenureproduct_days;
                        if ($scope.txtedittenure_days != $scope.txtnewedittenure_days) {

                            $scope.txtedittenure_daysflag = 'Y';
                        }
                        else {
                            $scope.txtedittenure_daysflag = 'N';
                        }
                        $scope.txtneweditoveralllimit_validity = resp.data.tenureoverall_limit;
                        if ($scope.txteditoveralllimit_validity != $scope.txtneweditoveralllimit_validity) {

                            $scope.txteditoveralllimit_validityflag = 'Y';
                        }
                        else {
                            $scope.txteditoveralllimit_validityflag = 'N';
                        }
                        $scope.cbonewFacilityTypelist = resp.data.facility_type;
                        if ($scope.cboFacilityTypelist != $scope.cbonewFacilityTypelist) {

                            $scope.cboFacilityTypelistflag = 'Y';
                        }
                        else {
                            $scope.cboFacilityTypelistflag = 'N';
                        }
                        $scope.cbonewFacilitymodelist = resp.data.facility_mode;
                        if ($scope.cboFacilitymodelist != $scope.cbonewFacilitymodelist) {

                            $scope.cboFacilitymodelistflag = 'Y';
                        }
                        else {
                            $scope.cboFacilitymodelistflag = 'N';
                        }
                        $scope.cbonewprincipalfrequency = resp.data.principalfrequency_gid;
                        $scope.cbonewInterestFrequency = resp.data.interestfrequency_gid;
                        $scope.cbonewProgram = resp.data.program_gid;

                        $scope.valuenewchainlist = resp.data.valuechainlist;

                        $scope.newrdbmilestone_applicablity = resp.data.milestone_applicability,
                            $scope.newrdbinsurance_applicability = resp.data.insurance_applicability,
                            $scope.cbonewmilestonepaymenttype = resp.data.milestonepayment_gid,
                            $scope.txtnewsapayout = resp.data.sa_payout,
                            $scope.newinsurance_availability = resp.data.insurance_availability,
                            $scope.newtxtinsurance_percent = resp.data.insurance_percent,
                            $scope.newtxtinsurance_cost = resp.data.insurance_cost,
                            $scope.newtxtnet_yield = resp.data.net_yield,
                            $scope.newsa_status = resp.data.sa_status;
                        if ($scope.newsa_status == "Yes")
                            $scope.showsapayout = true;
                        else
                            $scope.showsapayout = false;

                        if ($scope.newrdbmilestone_applicablity == "Yes") {
                            $scope.showmilestonepaymenttype = true;
                            $scope.disabledmilestonepaymenttype = false;
                        }
                        else {
                            $scope.showmilestonepaymenttype = false;
                            $scope.disabledmilestonepaymenttype = true;
                        }

                        $scope.rdbnewinterest_status = resp.data.interest_status;
                        $scope.rdbnewmoratorium_status = resp.data.moratorium_status;
                        $scope.cbonewmoratorium_type = resp.data.moratorium_type;
                        $scope.txtnewmoratorium_startdate = resp.data.moratorium_startdate;
                        $scope.txtnewmoratorium_enddate = resp.data.moratorium_enddate;
                        $scope.txtnewenduse_purpose = resp.data.enduse_purpose;
                        if ($scope.txtnewenduse_purpose != $scope.txtenduse_purpose) {

                            $scope.txtenduse_purposeflag = 'Y';
                        }
                        else {
                            $scope.txtenduse_purposeflag = 'N';
                        }
                        $scope.rdbnewTradeOriginated = resp.data.trade_orginatedby;

                        if ($scope.rdbnewTradeOriginated != $scope.rdbTradeOriginated) {

                            $scope. rdbTradeOriginatedflag = 'Y';
                        }
                        else {
                            $scope.rdbTradeOriginatedflag = 'N';
                        }
                        $scope.txtnewsabrokerage = resp.data.SA_Brokerage;
                        if ($scope.txtsabrokerage != $scope.txtnewsabrokerage) {

                            $scope.txtsabrokerageflag = 'Y';
                        }
                        else {
                            $scope.txtsabrokerageflag = 'N';
                        }
                        $scope.txtnewholdingperiod = resp.data.holding_periods;
                        if ($scope.txtholdingperiod != $scope.txtnewholdingperiod) {

                            $scope.txtholdingperiodflag = 'Y';
                        }
                        else {
                            $scope.txtholdingperiodflag = 'N';
                        }
                        $scope.txtnewholdingMonthlyprocurement = resp.data.holdingmonthly_procurement;
                        if ($scope.txtholdingMonthlyprocurement != $scope.txtnewholdingMonthlyprocurement) {

                            $scope.txtholdingMonthlyprocurementflag = 'Y';
                        }
                        else {
                            $scope.txtholdingMonthlyprocurementflag = 'N';
                        }
                            $scope.txtnewextendedholdingperiod = resp.data.extendedholding_periods;
                        if ($scope.txtextendedholdingperiod != $scope.txtnewextendedholdingperiod) {

                            $scope.txtextendedholdingperiodflag = 'Y';
                        }
                        else {
                            $scope.txtextendedholdingperiodflag = 'N';
                        }
                        $scope.txtnewextendedMonthlyprocurement = resp.data.extendedmonthly_procurement;
                        if ($scope.txtextendedMonthlyprocurement != $scope.txtnewextendedMonthlyprocurement) {

                            $scope.txtextendedMonthlyprocurementflag = 'Y';
                        }
                        else {
                            $scope.txtextendedMonthlyprocurementflag = 'N';
                        }
                        $scope.txtnewcharges_extendedperiod = resp.data.charges_extendedperiod;
                        if ($scope.txtcharges_extendedperiod != $scope.txtnewcharges_extendedperiod) {

                            $scope.txtcharges_extendedperiodflag = 'Y';
                        }
                        else {
                            $scope.txtcharges_extendedperiodflag = 'N';
                        }
                        $scope.txtnewcustomer_advance = resp.data.customer_advance;
                        if ($scope.txtcustomer_advance != $scope.txtnewcustomer_advance) {

                            $scope.txtcustomer_advanceflag = 'Y';
                        }
                        else {
                            $scope.txtcustomer_advanceflag = 'N';
                        }
                        $scope.txtnewreimburesementof_expenses = resp.data.reimburesementof_expenses;
                        if ($scope.txtreimburesementof_expenses != $scope.txtnewreimburesementof_expenses) {

                            $scope.txtreimburesementof_expensesflag = 'Y';
                        }
                        else {
                            $scope.txtreimburesementof_expensesflag = 'N';
                        }
                        $scope.txtnewreimburesementof_expensespenalty = resp.data.reimburesementof_expensespenalty;
                        if ($scope.txtreimburesementof_expensespenalty != $scope.txtnewreimburesementof_expensespenalty) {

                            $scope.txtreimburesementof_expensespenaltyflag = 'Y';
                        }
                        else {
                            $scope.txtreimburesementof_expensespenaltyflag = 'N';
                        }
                        $scope.bankfundingnew_documentname = resp.data.bankfundingdata_filename,
                            $scope.bankfundingnew_documentpath = resp.data.bankfundingdata_filepath,
                            $scope.txtneedfornew_stocking = resp.data.needfor_stocking;
                        if ($scope.txtneedfor_stocking != $scope.txtneedfornew_stocking) {

                            $scope.txtneedfor_stockingflag = 'Y';
                        }
                        else {
                            $scope.txtneedfor_stockingflag = 'N';
                        }
                        $scope.txtproductnew_portfolio = resp.data.product_portfolio;
                        if ($scope.txtproduct_portfolio != $scope.txtproductnew_portfolio) {

                            $scope.txtproduct_portfolioflag = 'Y';
                        }
                        else {
                            $scope.txtproduct_portfolioflag = 'N';
                        }
                        $scope.txtproductionnew_capacity = resp.data.production_capacity;
                        if ($scope.txtproduction_capacity != $scope.txtproductionnew_capacity) {

                            $scope.txtproduction_capacityflag = 'Y';
                        }
                        else {
                            $scope.txtproduction_capacityflag = 'N';
                        }
                        $scope.txtnatureofnew_operations = resp.data.natureof_operations;
                        if ($scope.txtnatureof_operations != $scope.txtnatureofnew_operations) {

                            $scope.txtnatureof_operationsflag = 'Y';
                        }
                        else {
                            $scope.txtnatureof_operationsflag = 'N';
                        }
                        $scope.txtnewaveragemonthly_inventoryholding = resp.data.averagemonthly_inventoryholding;
                        if ($scope.txtaveragemonthly_inventoryholding != $scope.txtnewaveragemonthly_inventoryholding) {

                            $scope.txtaveragemonthly_inventoryholdingflag = 'Y';
                        }
                        else {
                            $scope.txtaveragemonthly_inventoryholdingflag = 'N';
                        }
                        $scope.txtnewfinancialinstitutions_relationship = resp.data.financialinstitutions_relationship;
                        if ($scope.txtfinancialinstitutions_relationship != $scope.txtnewfinancialinstitutions_relationship) {

                            $scope.txtfinancialinstitutions_relationshipflag = 'Y';
                        }
                        else {
                            $scope.txtfinancialinstitutions_relationshipflag = 'N';
                        }
                        $scope.txtnewProgramLimitValidityfrom = resp.data.programlimit_validdfrom;
                        if ($scope.txtProgramLimitValidityfrom != $scope.txtnewProgramLimitValidityfrom) {

                            $scope.txtProgramLimitValidityfromflag = 'Y';
                        }
                        else {
                            $scope.txtProgramLimitValidityfromflag = 'N';
                        }
                        $scope.txtnewProgramLimitValidityTo = resp.data.programlimit_validdto;
                        if ($scope.txtProgramLimitValidityTo != $scope.txtnewProgramLimitValidityTo) {

                            $scope.txtProgramLimitValidityToflag = 'Y';
                        }
                        else {
                            $scope.txtProgramLimitValidityToflag = 'N';
                        }
                            $scope.txtnewoverallprogramvalidity_limit = resp.data.programoverall_limit;

                        if ($scope.txtoverallprogramvalidity_limit != $scope.txtnewoverallprogramvalidity_limit) {

                            $scope.txtoverallprogramvalidity_limitflag = 'Y';
                        }
                        else {
                            $scope.txtoverallprogramvalidity_limitflag = 'N';
                        }
                        //var newlbloveralllimit_amount = ($scope.newlbloveralllimit_amount).replaceAll(',', '');
                        //var newlsamount = (parseFloat(newlbloveralllimit_amount) - parseFloat($scope.txtnewloanfaility_amount));
                        //$scope.txtnewremaining = parseFloat(lsamount);

                        if (resp.data.newproduct_type == 'Agri Receivable Finance (ARF)') {
                            $scope.ARF_condition = true;
                        }
                        else {
                            $scope.ARF_condition = false;
                        }
                    });
                }

            });
        }

        $scope.OtherProducts_newview = function (newapplication2loan_gid, product_type, newapplication_gid, oldapplication_gid, index) {
            $scope.NAflag = '';
          /*  var list1 = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20'];*/

            var number = index;

            //if (list1[0] == number || list1[1] == number || list1[2] == number || list1[3] == number || list1[4] == number) {
            //    $scope.listhighlight_flag1 = 'Y';
            //}
            //else {
            //    $scope.listhighlight_flag1 = 'N';
            //    $scope.listhighlight_flag = 'N';
            //}

            var oldapplication_gid = oldapplication_gid;
            if ($scope.Products_flag == undefined || $scope.Products_flag == '') {
                $scope.Products_flag = true;
            }
            else if ($scope.Products_flag == true) {
                $scope.Products_flag = false;
             /*   $scope.highlight_flag = 'N';*/
                $scope.NAflag = '';
                $scope.NAoldflag = '';
                var params = {
                    application2loan_gid:'',
                    application_gid: newapplication_gid


                }
                var url = 'api/AgrTrnAppCreditUnderWriting/Getproductapplicationloangid';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();

                    /*    $scope.oldapplication2loan_gid = resp.data.newapplication2loan_gid;*/
                        $scope.newloandtls_list = resp.data.mstLoan_list;
                    //    if ($scope.oldapplication2loan_gid == undefined || $scope.oldapplication2loan_gid == null || $scope.oldapplication2loan_gid == '') {
                    //        $scope.NAflag = 'Y';
                    //    }
                    }
                    //else {
                    //    $scope.NAflag = 'N';
                    //}
                });
                activate();


            }
            else { $scope.tradedtl_view_flag = true; }

            $scope.newproduct_type = product_type;
            $scope.oldproduct_type = product_type;
            $scope.newapplication2loan_gid = newapplication2loan_gid;
         
           

            var params = {
                application2loan_gid: $scope.newapplication2loan_gid,
                application_gid: newapplication_gid


            }
            var url = 'api/AgrTrnAppCreditUnderWriting/Getproductapplicationloangid';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.oldapplication2loan_gid = resp.data.newapplication2loan_gid;
                    $scope.newloandtls_list = resp.data.mstLoan_list;
                    if ($scope.oldapplication2loan_gid == undefined || $scope.oldapplication2loan_gid == null || $scope.oldapplication2loan_gid == '') {
                        $scope.NAflag = 'Y';
                         $scope.NAoldflag = '';
                    }
                }
                else {
                    $scope.NAflag = '';
                    $scope.NAoldflag = '';
                }
               

                if ((oldapplication_gid != null || oldapplication_gid != '' || oldapplication_gid != undefined) && ($scope.oldapplication2loan_gid != null || $scope.oldapplication2loan_gid != '' || $scope.oldapplication2loan_gid != undefined)) {
                    var params = {
                        application_gid: oldapplication_gid
                    }

                    var url = 'api/AgrMstApplicationView/GetProductChargesDtl';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.txtoveralllimit_amt = resp.data.overalllimit_amount;
                        $scope.txtvalidity_year = resp.data.validityoveralllimit_year;
                        $scope.txtvalidity_month = resp.data.validityoveralllimit_month;
                        $scope.txtvalidity_days = resp.data.validityoveralllimit_days;
                        $scope.txtcalculation_limitvalidity = resp.data.calculationoveralllimit_validity;
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
                        $scope.txtapplication_no = resp.data.application_no;
                    });

                    var params = {
                        application_gid: '',
                        application2loan_gid: $scope.oldapplication2loan_gid,
                        tmp_status: 'false',
                    }
                    var url = 'api/AgrMstApplicationEdit/GetLoan2Supplierdtl';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            $scope.MdlSupplierdtllist = resp.data.MdlSupplierdtl;
                        } else {
                            unlockUI();
                        }
                    });
                    var params = {
                        application_gid: oldapplication_gid,
                        application2loan_gid: $scope.oldapplication2loan_gid,
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
                        application2loan_gid: $scope.oldapplication2loan_gid,
                        application_gid: oldapplication_gid
                    }
                    var url = 'api/AgrMstApplicationView/GetLoanProgramValueChain';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.mstproductdtl_list = resp.data.mstproductdtl_list;
                    });
                    var params2 = {
                        application_gid: oldapplication_gid,
                        application2loan_gid: $scope.oldapplication2loan_gid,
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
                        application_gid: oldapplication_gid,
                        application2loan_gid: $scope.oldapplication2loan_gid,
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
                        application2loan_gid: $scope.oldapplication2loan_gid
                    }
                    var url = 'api/AgrMstApplicationEdit/LoanDetailsEdit';
                    SocketService.getparams(url, params).then(function (resp) {

                        $scope.txtfacilityreqon_date = resp.data.facilityrequested_date;
                        $scope.cboProductTypelist = resp.data.producttype_gid;
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
                        $scope.loansubproduct_name = resp.data.productsub_type;
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
                            $scope.cbomilestonepaymenttype = resp.data.milestonepayment_gid,
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



                        if (resp.data.product_type == 'Agri Receivable Finance (ARF)') {
                            $scope.ARF_condition = true;
                        }
                        else {
                            $scope.ARF_condition = false;
                        }
                    });
                }

                if ((newapplication_gid != null || newapplication_gid != '' || newapplication_gid != undefined) && (newapplication2loan_gid != null || newapplication2loan_gid != '' || newapplication2loan_gid != undefined)) {
                    var params = {
                        application_gid: newapplication_gid
                    }

                    var url = 'api/AgrMstApplicationView/GetProductChargesDtl';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.txtoveralllimit_amt = resp.data.overalllimit_amount;
                        $scope.txtvalidity_year = resp.data.validityoveralllimit_year;
                        $scope.txtvalidity_month = resp.data.validityoveralllimit_month;
                        $scope.txtvalidity_days = resp.data.validityoveralllimit_days;
                        $scope.txtcalculation_limitvalidity = resp.data.calculationoveralllimit_validity;
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
                        $scope.newCollateral_list = resp.data.mstcollateral_list;
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
                        $scope.txtapplication_no = resp.data.application_no;
                    });
                    if ($scope.Products_flag1 == undefined || $scope.Products_flag1 == '') {
                        $scope.Products_flag1 = true;
                    }
                    else if ($scope.Products_flag1 == true) {
                        $scope.Products_flag1 = false;
                    }
                    else { $scope.tradedtl_view_flag1 = true; }

                    if (($scope.newapplication2loan_gid != null || $scope.newapplication2loan_gid != undefined)) {
                        var params = {
                            application_gid: '',
                            newapplication2loan_gid: $scope.newapplication2loan_gid,
                            oldapplication2loan_gid: $scope.oldapplication2loan_gid,
                            tmp_status: 'false',
                        }
                        var url = 'api/AgrTrnAppCreditUnderWriting/GetLoan2Supplierdtl';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                unlockUI();
                                $scope.MdlNewSupplierdtllist = resp.data.MdlSupplierdtl;
                            } else {
                                unlockUI();
                            }
                        });
                    }
                    else {
                        var params1 = {
                            application_gid: '',
                            application2loan_gid: $scope.newapplication2loan_gid,
                            tmp_status: 'false',
                        }
                        var url = 'api/AgrMstApplicationEdit/GetLoan2Supplierdtl';
                        SocketService.getparams(url, params1).then(function (resp) {
                            if (resp.data.status == true) {
                                unlockUI();
                                $scope.MdlNewSupplierdtllist = resp.data.MdlSupplierdtl;
                            } else {
                                unlockUI();
                            }
                        });
                    }


                    if (($scope.newapplication2loan_gid != null || $scope.newapplication2loan_gid != undefined)) {
                        var params = {
                            application_gid: newapplication_gid,
                            newapplication2loan_gid: $scope.newapplication2loan_gid,
                            oldapplication2loan_gid: $scope.oldapplication2loan_gid,
                            tmp_status: 'both',
                        }
                        var url = 'api/AgrTrnAppCreditUnderWriting/GetLoan2SupplierPaymentdtl';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                unlockUI();
                                $scope.MdlNewSupplierPaymentlist = resp.data.MdlSupplierPaymentdtl;
                            } else {
                                unlockUI();
                            }

                        });
                    }
                    else {
                        var params = {
                            application_gid: newapplication_gid,
                            application2loan_gid: $scope.newapplication2loan_gid,
                            tmp_status: 'both',
                        }
                        var url = 'api/AgrMstApplicationEdit/GetLoan2SupplierPaymentdtl';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                unlockUI();
                                $scope.MdlNewSupplierPaymentlist = resp.data.MdlSupplierPaymentdtl;
                            } else {
                                unlockUI();
                            }

                        });
                    }
                    if (($scope.newapplication2loan_gid != null || $scope.newapplication2loan_gid != undefined)) {
                        var params = {
                            newapplication2loan_gid: $scope.newapplication2loan_gid,
                            oldapplication2loan_gid: $scope.oldapplication2loan_gid,
                            application_gid: newapplication_gid
                        }
                        var url = 'api/AgrTrnAppCreditUnderWriting/GetLoanProgramValueChain';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.mstnewproductdtl_list = resp.data.mstproductdtl_list;
                        });
                    }
                    else {
                        var params = {
                            application2loan_gid: $scope.newapplication2loan_gid,
                            application_gid: newapplication_gid
                        }
                        var url = 'api/AgrMstApplicationView/GetLoanProgramValueChain';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
                            $scope.mstnewproductdtl_list = resp.data.mstproductdtl_list;
                        });
                    }


                    var params = {
                        newapplication2loan_gid: $scope.newapplication2loan_gid,
                        oldapplication2loan_gid: $scope.oldapplication2loan_gid
                    }
                    var url = 'api/AgrTrnAppCreditUnderWriting/Getproductvariety';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        //$scope.mstnewproductdtl_listflag = resp.data.mstnewproductdtl_listflag;
                        //$scope.MdlNewSupplierdtllistflag = resp.data.MdlNewSupplierdtllistflag;
                        //$scope.MdlNewSupplierPaymentlistflag = resp.data.MdlNewSupplierPaymentlistflag;
                        $scope.Mdlcollateralobservation_summaryflag = resp.data.Mdlcollateralobservation_summaryflag;
                        $scope.Mdlsource_typeflag = resp.data.Mdlsource_typeflag;

                    });

                    var params2 = {
                        application_gid: newapplication_gid,
                        application2loan_gid: $scope.newapplication2loan_gid,
                        tmp_status: 'false',
                    }
                    var url = 'api/AgrMstApplicationEdit/GetLoan2Repaymentdtl';
                    SocketService.getparams(url, params2).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            $scope.MdlNewrePaymentdtl = resp.data.MdlPaymentdtl;
                        }
                    });

                    var param = {
                        application_gid: newapplication_gid,
                        application2loan_gid: $scope.newapplication2loan_gid,
                    }
                    var url = 'api/AgrMstApplicationEdit/GetEditLoanLimit';
                    SocketService.post(url, param).then(function (resp) {
                        unlockUI();
                        $scope.lblnewoveralllimit_amount = resp.data.overalllimit_amount;

                        $scope.onboarding_status = resp.data.onboarding_status;

                        if (resp.data.overalllimit_amount == "0.00" || resp.data.onboarding_status == "Direct") {

                            $scope.zerofacility = true

                            $scope.txtnewloanfaility_amount = '0';

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
                        application2loan_gid: $scope.newapplication2loan_gid
                    }
                    var url = 'api/AgrMstApplicationEdit/LoanDetailsEdit';
                    SocketService.getparams(url, params).then(function (resp) {

                        $scope.txtnewfacilityreqon_date = resp.data.facilityrequested_date;
                        if ($scope.txtfacilityreqon_date != $scope.txtnewfacilityreqon_date) {

                            $scope.txtfacilityreqon_dateflag = 'Y';
                        }
                        else {
                            $scope.txtfacilityreqon_dateflag = 'N';
                        }
                        $scope.cbonewProductTypelist = resp.data.producttype_gid;
                        $scope.newproduct_type = resp.data.product_type;
                        $scope.lblnewproducttype = resp.data.product_type;
                        $scope.lblnewproductsub_type = resp.data.productsub_type;
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
                            $scope.newloansubproductlist = resp.data.application_list;
                        });

                        $scope.cbonewProductSubTypelist = resp.data.productsubtype_gid;
                        $scope.loannewsubproduct_name = resp.data.productsub_type;
                        $scope.cbonewLoanTypelist = resp.data.loantype_gid;
                        $scope.newloan_type = resp.data.loan_type;
                        if ($scope.newloan_type == 'Secured') {
                            $scope.Collateralshow = true;
                        }
                        else {
                            $scope.Collateralshow = false;
                        }
                        if ($scope.newloan_type != $scope.loan_type) {

                            $scope.loan_typeflag = 'Y';
                        }
                        else {
                            $scope.loan_typeflag = 'N';
                        }

                        $scope.cbonewSourceType = resp.data.source_type;
                        $scope.txtnewguidelinevalue = resp.data.guideline_value;

                        $scope.txtnewguideline_date = resp.data.guideline_date;
                        $scope.txtnewmarketvalue_date = resp.data.marketvalue_date;
                        $scope.txtnewmarketValue = resp.data.market_value;


                        $scope.txtnewforcedsource_value = resp.data.forcedsource_value;


                        $scope.txtnewcollateralSSV_value = resp.data.collateralSSV_value;


                        $scope.txtnewforcedvalueassessed_on = resp.data.forcedvalueassessed_on;
                        $scope.txtnewcolateralobservation_summary = resp.data.collateralobservation_summary;

                        $scope.txtnewloanfaility_amount = resp.data.facilityloan_amount;
                        if ($scope.txtnewloanfaility_amount != $scope.txtloanfaility_amount) {

                            $scope.txtloanfaility_amountflag = 'Y';
                        }
                        else {
                            $scope.txtloanfaility_amountflag = 'N';
                        }


                        $scope.txtneweditrate_interest = resp.data.rate_interest;
                        if ($scope.txtneweditrate_interest != $scope.txteditrate_interest) {

                            $scope.txteditrate_interestflag = 'Y';
                        }
                        else {
                            $scope.txteditrate_interestflag = 'N';
                        }
                        $scope.txtneweditpanel_interest = resp.data.penal_interest;

                        if ($scope.txteditpanel_interest != $scope.txtneweditpanel_interest) {

                            $scope.txteditpanel_interestflag = 'Y';
                        }
                        else {
                            $scope.txteditpanel_interestflag = 'N';
                        }
                        $scope.txtneweditvalidity_years = resp.data.facilityvalidity_year;
                        $scope.txtneweditvalidity_months = resp.data.facilityvalidity_month;
                        $scope.txtneweditvalidity_days = resp.data.facilityvalidity_days;
                        $scope.txtnewoverallfacilityvalidity_limit = resp.data.facilityoverall_limit;
                        //$scope.txtnewedittenure_years = resp.data.tenureproduct_year;
                        //$scope.txtnewedittenure_months = resp.data.tenureproduct_month;
                        $scope.txtnewedittenure_days = resp.data.tenureproduct_days;
                        if ($scope.txtedittenure_days != $scope.txtnewedittenure_days) {

                            $scope.txtedittenure_daysflag = 'Y';
                        }
                        else {
                            $scope.txtedittenure_daysflag = 'N';
                        }
                        $scope.txtneweditoveralllimit_validity = resp.data.tenureoverall_limit;
                        if ($scope.txteditoveralllimit_validity != $scope.txtneweditoveralllimit_validity) {

                            $scope.txteditoveralllimit_validityflag = 'Y';
                        }
                        else {
                            $scope.txteditoveralllimit_validityflag = 'N';
                        }
                        $scope.cbonewFacilityTypelist = resp.data.facility_type;
                        if ($scope.cboFacilityTypelist != $scope.cbonewFacilityTypelist) {

                            $scope.cboFacilityTypelistflag = 'Y';
                        }
                        else {
                            $scope.cboFacilityTypelistflag = 'N';
                        }
                        $scope.cbonewFacilitymodelist = resp.data.facility_mode;
                        if ($scope.cboFacilitymodelist != $scope.cbonewFacilitymodelist) {

                            $scope.cboFacilitymodelistflag = 'Y';
                        }
                        else {
                            $scope.cboFacilitymodelistflag = 'N';
                        }
                        $scope.cbonewprincipalfrequency = resp.data.principalfrequency_gid;
                        $scope.cbonewInterestFrequency = resp.data.interestfrequency_gid;
                        $scope.cbonewProgram = resp.data.program_gid;

                        $scope.valuenewchainlist = resp.data.valuechainlist;

                        $scope.newrdbmilestone_applicablity = resp.data.milestone_applicability,
                            $scope.newrdbinsurance_applicability = resp.data.insurance_applicability,
                            $scope.cbonewmilestonepaymenttype = resp.data.milestonepayment_gid,
                            $scope.txtnewsapayout = resp.data.sa_payout,
                            $scope.newinsurance_availability = resp.data.insurance_availability,
                            $scope.newtxtinsurance_percent = resp.data.insurance_percent,
                            $scope.newtxtinsurance_cost = resp.data.insurance_cost,
                            $scope.newtxtnet_yield = resp.data.net_yield,
                            $scope.newsa_status = resp.data.sa_status;
                        if ($scope.newsa_status == "Yes")
                            $scope.showsapayout = true;
                        else
                            $scope.showsapayout = false;

                        if ($scope.newrdbmilestone_applicablity == "Yes") {
                            $scope.showmilestonepaymenttype = true;
                            $scope.disabledmilestonepaymenttype = false;
                        }
                        else {
                            $scope.showmilestonepaymenttype = false;
                            $scope.disabledmilestonepaymenttype = true;
                        }

                        $scope.rdbnewinterest_status = resp.data.interest_status;
                        $scope.rdbnewmoratorium_status = resp.data.moratorium_status;
                        $scope.cbonewmoratorium_type = resp.data.moratorium_type;
                        $scope.txtnewmoratorium_startdate = resp.data.moratorium_startdate;
                        $scope.txtnewmoratorium_enddate = resp.data.moratorium_enddate;
                        $scope.txtnewenduse_purpose = resp.data.enduse_purpose;
                        if ($scope.txtnewenduse_purpose != $scope.txtenduse_purpose) {

                            $scope.txtenduse_purposeflag = 'Y';
                        }
                        else {
                            $scope.txtenduse_purposeflag = 'N';
                        }
                        $scope.rdbnewTradeOriginated = resp.data.trade_orginatedby;

                        if ($scope.rdbnewTradeOriginated != $scope.rdbTradeOriginated) {

                            $scope.rdbTradeOriginatedflag = 'Y';
                        }
                        else {
                            $scope.rdbTradeOriginatedflag = 'N';
                        }
                        $scope.txtnewsabrokerage = resp.data.SA_Brokerage;
                        if ($scope.txtsabrokerage != $scope.txtnewsabrokerage) {

                            $scope.txtsabrokerageflag = 'Y';
                        }
                        else {
                            $scope.txtsabrokerageflag = 'N';
                        }
                        $scope.txtnewholdingperiod = resp.data.holding_periods;
                        if ($scope.txtholdingperiod != $scope.txtnewholdingperiod) {

                            $scope.txtholdingperiodflag = 'Y';
                        }
                        else {
                            $scope.txtholdingperiodflag = 'N';
                        }
                        $scope.txtnewholdingMonthlyprocurement = resp.data.holdingmonthly_procurement;
                        if ($scope.txtholdingMonthlyprocurement != $scope.txtnewholdingMonthlyprocurement) {

                            $scope.txtholdingMonthlyprocurementflag = 'Y';
                        }
                        else {
                            $scope.txtholdingMonthlyprocurementflag = 'N';
                        }
                        $scope.txtnewextendedholdingperiod = resp.data.extendedholding_periods;
                        if ($scope.txtextendedholdingperiod != $scope.txtnewextendedholdingperiod) {

                            $scope.txtextendedholdingperiodflag = 'Y';
                        }
                        else {
                            $scope.txtextendedholdingperiodflag = 'N';
                        }
                        $scope.txtnewextendedMonthlyprocurement = resp.data.extendedmonthly_procurement;
                        if ($scope.txtextendedMonthlyprocurement != $scope.txtnewextendedMonthlyprocurement) {

                            $scope.txtextendedMonthlyprocurementflag = 'Y';
                        }
                        else {
                            $scope.txtextendedMonthlyprocurementflag = 'N';
                        }
                        $scope.txtnewcharges_extendedperiod = resp.data.charges_extendedperiod;
                        if ($scope.txtcharges_extendedperiod != $scope.txtnewcharges_extendedperiod) {

                            $scope.txtcharges_extendedperiodflag = 'Y';
                        }
                        else {
                            $scope.txtcharges_extendedperiodflag = 'N';
                        }
                        $scope.txtnewcustomer_advance = resp.data.customer_advance;
                        if ($scope.txtcustomer_advance != $scope.txtnewcustomer_advance) {

                            $scope.txtcustomer_advanceflag = 'Y';
                        }
                        else {
                            $scope.txtcustomer_advanceflag = 'N';
                        }
                        $scope.txtnewreimburesementof_expenses = resp.data.reimburesementof_expenses;
                        if ($scope.txtreimburesementof_expenses != $scope.txtnewreimburesementof_expenses) {

                            $scope.txtreimburesementof_expensesflag = 'Y';
                        }
                        else {
                            $scope.txtreimburesementof_expensesflag = 'N';
                        }
                        $scope.txtnewreimburesementof_expensespenalty = resp.data.reimburesementof_expensespenalty;
                        if ($scope.txtreimburesementof_expensespenalty != $scope.txtnewreimburesementof_expensespenalty) {

                            $scope.txtreimburesementof_expensespenaltyflag = 'Y';
                        }
                        else {
                            $scope.txtreimburesementof_expensespenaltyflag = 'N';
                        }
                        $scope.bankfundingnew_documentname = resp.data.bankfundingdata_filename,
                            $scope.bankfundingnew_documentpath = resp.data.bankfundingdata_filepath,
                            $scope.txtneedfornew_stocking = resp.data.needfor_stocking;
                        if ($scope.txtneedfor_stocking != $scope.txtneedfornew_stocking) {

                            $scope.txtneedfor_stockingflag = 'Y';
                        }
                        else {
                            $scope.txtneedfor_stockingflag = 'N';
                        }
                        $scope.txtproductnew_portfolio = resp.data.product_portfolio;
                        if ($scope.txtproduct_portfolio != $scope.txtproductnew_portfolio) {

                            $scope.txtproduct_portfolioflag = 'Y';
                        }
                        else {
                            $scope.txtproduct_portfolioflag = 'N';
                        }
                        $scope.txtproductionnew_capacity = resp.data.production_capacity;
                        if ($scope.txtproduction_capacity != $scope.txtproductionnew_capacity) {

                            $scope.txtproduction_capacityflag = 'Y';
                        }
                        else {
                            $scope.txtproduction_capacityflag = 'N';
                        }
                        $scope.txtnatureofnew_operations = resp.data.natureof_operations;
                        if ($scope.txtnatureof_operations != $scope.txtnatureofnew_operations) {

                            $scope.txtnatureof_operationsflag = 'Y';
                        }
                        else {
                            $scope.txtnatureof_operationsflag = 'N';
                        }
                        $scope.txtnewaveragemonthly_inventoryholding = resp.data.averagemonthly_inventoryholding;
                        if ($scope.txtaveragemonthly_inventoryholding != $scope.txtnewaveragemonthly_inventoryholding) {

                            $scope.txtaveragemonthly_inventoryholdingflag = 'Y';
                        }
                        else {
                            $scope.txtaveragemonthly_inventoryholdingflag = 'N';
                        }
                        $scope.txtnewfinancialinstitutions_relationship = resp.data.financialinstitutions_relationship;
                        if ($scope.txtfinancialinstitutions_relationship != $scope.txtnewfinancialinstitutions_relationship) {

                            $scope.txtfinancialinstitutions_relationshipflag = 'Y';
                        }
                        else {
                            $scope.txtfinancialinstitutions_relationshipflag = 'N';
                        }
                        $scope.txtnewProgramLimitValidityfrom = resp.data.programlimit_validdfrom;
                        if ($scope.txtProgramLimitValidityfrom != $scope.txtnewProgramLimitValidityfrom) {

                            $scope.txtProgramLimitValidityfromflag = 'Y';
                        }
                        else {
                            $scope.txtProgramLimitValidityfromflag = 'N';
                        }
                        $scope.txtnewProgramLimitValidityTo = resp.data.programlimit_validdto;
                        if ($scope.txtProgramLimitValidityTo != $scope.txtnewProgramLimitValidityTo) {

                            $scope.txtProgramLimitValidityToflag = 'Y';
                        }
                        else {
                            $scope.txtProgramLimitValidityToflag = 'N';
                        }
                        $scope.txtnewoverallprogramvalidity_limit = resp.data.programoverall_limit;

                        if ($scope.txtoverallprogramvalidity_limit != $scope.txtnewoverallprogramvalidity_limit) {

                            $scope.txtoverallprogramvalidity_limitflag = 'Y';
                        }
                        else {
                            $scope.txtoverallprogramvalidity_limitflag = 'N';
                        }
                        //var newlbloveralllimit_amount = ($scope.newlbloveralllimit_amount).replaceAll(',', '');
                        //var newlsamount = (parseFloat(newlbloveralllimit_amount) - parseFloat($scope.txtnewloanfaility_amount));
                        //$scope.txtnewremaining = parseFloat(lsamount);

                        if (resp.data.newproduct_type == 'Agri Receivable Finance (ARF)') {
                            $scope.ARF_condition = true;
                        }
                        else {
                            $scope.ARF_condition = false;
                        }
                    });
                }
              

            });
        }
        $scope.doc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
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

        $scope.uploadeddoc_Collateral = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Collateraldocuments.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance', 'DownloaddocumentService', 'cmnfunctionService'];
            function ModalInstanceCtrl($scope, $modalInstance, DownloaddocumentService, cmnfunctionService) {
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
                //$scope.download_Collateraldoc = function (val1, val2) {
                //    var phyPath = val1;
                //    var relPath = phyPath.split("EMS");
                //    var relpath1 = relPath[1].replace("\\", "/");
                //    var hosts = window.location.host;
                //    var prefix = location.protocol + "//";
                //    var str = prefix.concat(hosts, relpath1);
                //    var link = document.createElement("a");
                //    link.download = val2;
                //    var uri = str;
                //    link.href = uri;
                //    link.click();
                //}

                //        $scope.download_Collateraldoc = function (val1, val2) {
                //            var phyPath = val1;
                //            var relPath = phyPath.split("EMS");

                //            var relpath1 = relPath[1].replace("\\", "/");

                //            var hosts = window.location.host;

                //            var prefix = location.protocol + "//";

                //            var str = prefix.concat(hosts, relpath1);

                //            var link = document.createElement("a");

                //            link.download = val2;

                //            var uri = str;

                //            link.href = uri;

                //            link.click();

                //        }

                //    }

                //}

                $scope.download_Collateraldoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);

                }

                $scope.downloadall_2 = function () {
                    for (var i = 0; i < $scope.Collateraldoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name);
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


        //$scope.Back = function () {
        //    //$location.url('app/AgrMstOnboardingApplicationInfo?application_gid=' + $scope.application_gid);

        //    //$location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('application_gid=' + $scope.application_gid + "&onboard_gid=" + onboard_gid + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lsApp=Y&FromRM=N' + '&lstab=' + lstab));

        //    if (lsparent == 'RMonboard') {

        //        $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lsApp=Y&FromRM=N'));

        //    }


        //    else if (lsparent == 'AppRMView') {

        //        $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab + '&lspage=' + lspage));

        //        //$location.url('app/AgrMstOnboardingApplicationInfo?onboard_gid = ' + onboard_gid + '&application_gid=' + $scope.application_gid + ' &selectedIndex=' + $scope.selectedIndex + ' &lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + ' &lstab=' + lstab);
        //        //$location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab));


        //    }
        //    else if (lsparent == 'AppBuySummary') {

        //        $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab + '&lspage=' + lspage));

        //        //$location.url('app/AgrMstOnboardingApplicationInfo?onboard_gid = ' + onboard_gid + '&application_gid=' + $scope.application_gid + ' &selectedIndex=' + $scope.selectedIndex + ' &lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + ' &lstab=' + lstab);
        //        //$location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab));


        //    }
        //    else if (lsparent == 'AppCCCommitteeView') {

        //        $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab + '&lspage=' + lspage));

        //        //$location.url('app/AgrMstOnboardingApplicationInfo?onboard_gid = ' + onboard_gid + '&application_gid=' + $scope.application_gid + ' &selectedIndex=' + $scope.selectedIndex + ' &lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + ' &lstab=' + lstab);
        //        //$location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab));


        //    }
        //    else if (lsparent == 'AppTrnCCCommittee') {

        //        $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab + '&lspage=' + lspage));

        //        //$location.url('app/AgrMstOnboardingApplicationInfo?onboard_gid = ' + onboard_gid + '&application_gid=' + $scope.application_gid + ' &selectedIndex=' + $scope.selectedIndex + ' &lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + ' &lstab=' + lstab);
        //        //$location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab));


        //    }
        //    else if (lsparent == 'AppCad') {

        //        $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab + '&lspage=' + lspage));

        //        //$location.url('app/AgrMstOnboardingApplicationInfo?onboard_gid = ' + onboard_gid + '&application_gid=' + $scope.application_gid + ' &selectedIndex=' + $scope.selectedIndex + ' &lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + ' &lstab=' + lstab);
        //        //$location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab));


        //    }
        //    else {
        //        $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab + '&lspage=' + lspage));

        //    }



        //}
        $scope.Back = function () {
            if (lsdynamiclimitmanagementback == 'AgrMstCustomerOnboardingSummary' || lsdynamiclimitmanagementback == 'AgrMstOnboardingApprovalCompleted' || lsdynamiclimitmanagementback == 'AgrMstBuyerApprovedSummary' || lsdynamiclimitmanagementback == 'AgrMstCustomerApprovalSummary') {

                $location.url('app/' + lsdynamiclimitmanagementback);
            }
            else if (lsdynamiclimitmanagementback == 'AgrApplicationCreationView') {

                $location.url('app/AgrApplicationCreationView?application_gid=' + application_gid + '&lstab=' + lstab + '&product_gid=' + product_gid + '&program_gid=' + program_gid);
            }
            if (lsdynamiclimitmanagementback == 'AgrMstCcCommitteeApplicationView') {

                $location.url('app/AgrMstCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab=' + lstab + '&product_gid=' + product_gid + '&program_gid=' + program_gid);
            }
     
            else if (lsdynamiclimitmanagementback == 'AgrTrnCcCommitteeApplicationView') {

                $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&program_gid=' + program_gid + '&lstab=' + lstab);
            }
            else if (lsparent == 'AppTrnCCCommittee') {   

                $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=' + lspage + '&lstab=' + lstab);

         }
            else if (lsdynamiclimitmanagementback == 'AgrMstCadApplicationView') {

                $location.url('app/AgrMstCadApplicationView?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&program_gid=' + program_gid);
            }

            else if (lsdynamiclimitmanagementback == 'AgrTrnStartCreditUnderwriting') {

                $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&appcreditapproval_gid=' + appcreditapproval_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&program_gid=' + program_gid);
            }

            else if (lsdynamiclimitmanagementback == 'AgrMstCreateContract') {

                $location.url('app/AgrMstCreateContract?application_gid=' + application_gid + '&appcreditapproval_gid=' + appcreditapproval_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&program_gid=' + program_gid);
            }

            else if (lsdynamiclimitmanagementback == 'AgrTrnStartScheduledMeeting') {

                if (typeof lspage === 'undefined') {

                    $location.url('app/AgrTrnStartScheduledMeeting?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&program_gid=' + program_gid);
                }
                else {

                    $location.url('app/AgrTrnStartScheduledMeeting?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&program_gid=' + program_gid);
                }

                // $location.url('app/AgrMstCadApplicationView?application_gid=' + onboard_gid + '&lspage=' + lspage);
            }

        }
        $scope.downloadall = function () {
            for (var i = 0; i < $scope.Hypothecationdoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.Hypothecationdoc_list[i].document_path, $scope.Hypothecationdoc_list[i].document_name);
            }
        }
        $scope.downloadall_8 = function () {
            for (var i = 0; i < $scope.UploadMemberDocumentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.UploadMemberDocumentList[i].document_path, $scope.UploadMemberDocumentList[i].document_name);
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


})();