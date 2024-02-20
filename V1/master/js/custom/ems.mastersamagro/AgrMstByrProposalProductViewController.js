// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstByrProposalProductViewController', AgrMstByrProposalProductViewController);

    AgrMstByrProposalProductViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService','DownloaddocumentService'];

    function AgrMstByrProposalProductViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService, DownloaddocumentService) {

        var vm = this;

        vm.title = 'AgrMstByrProposalProductViewController';
        //const lsdynamiclimitmanagementback = 'AgrMstOnboardingApplicationInfo';

        /*    var banktransc_gid = $location.search().banktransc_gid;*/
        //var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        //var application2loan_gid = searchObject.application2loan_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.application2loan_gid = searchObject.application2loan_gid;
        var application2loan_gid = $scope.application2loan_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var onboard_gid = searchObject.onboard_gid;
        var lspage = searchObject.lspage;
        var lstab = searchObject.lstab;
        $scope.application_gid = searchObject.application_gid;
        var application_gid = $scope.application_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var onboard_gid = searchObject.onboard_gid;
        var lsdynamiclimitmanagementback = searchObject.lsdynamiclimitmanagementback;
        $scope.lsparent = searchObject.lsparent;
        var lsparent = $scope.lsparent;
        activate();
        function activate() { 
          
            var params = {
                application_gid: $scope.application_gid
            }

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
                $scope.txtapplication_no = resp.data.application_no;
            }); 

        }

        $scope.OtherProducts_view = function (application2loan_gid, viewproduct_type) {

            $scope.txtviewproduct_type = viewproduct_type;
            if ($scope.Products_flag == undefined || $scope.Products_flag == '') {
                $scope.Products_flag = true;
            }
            else if ($scope.Products_flag == true) {
                $scope.Products_flag = false;
            }
            else { $scope.tradedtl_view_flag = true; }
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
                application2loan_gid: application2loan_gid,
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


                var lbloveralllimit_amount = ($scope.lbloveralllimit_amount).replaceAll(',', '');
                var lsamount = (parseFloat(lbloveralllimit_amount) - parseFloat($scope.txtloanfaility_amount));
                $scope.txtremaining = parseFloat(lsamount);

                if (resp.data.product_type == 'Agri Receivable Finance (ARF)') {
                    $scope.ARF_condition = true;
                }
                else {
                    $scope.ARF_condition = false;
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

        $scope.Back = function () {
            //$location.url('app/AgrMstOnboardingApplicationInfo?application_gid=' + $scope.application_gid);

            //$location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('application_gid=' + $scope.application_gid + "&onboard_gid=" + onboard_gid + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lsApp=Y&FromRM=N' + '&lstab=' + lstab));

            if (lsparent == 'RMonboard') {

                $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lsApp=Y&FromRM=N'));

            }


            else if (lsparent == 'AppRMView') {

                $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab + '&lspage=' + lspage));

                //$location.url('app/AgrMstOnboardingApplicationInfo?onboard_gid = ' + onboard_gid + '&application_gid=' + $scope.application_gid + ' &selectedIndex=' + $scope.selectedIndex + ' &lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + ' &lstab=' + lstab);
                //$location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab));


            }
            else if (lsparent == 'AppBuySummary') {

                $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab + '&lspage=' + lspage));

                //$location.url('app/AgrMstOnboardingApplicationInfo?onboard_gid = ' + onboard_gid + '&application_gid=' + $scope.application_gid + ' &selectedIndex=' + $scope.selectedIndex + ' &lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + ' &lstab=' + lstab);
                //$location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab));


            }
            else if (lsparent == 'AppCCCommitteeView') {

                $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab + '&lspage=' + lspage));

                //$location.url('app/AgrMstOnboardingApplicationInfo?onboard_gid = ' + onboard_gid + '&application_gid=' + $scope.application_gid + ' &selectedIndex=' + $scope.selectedIndex + ' &lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + ' &lstab=' + lstab);
                //$location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab));


            }
            else if (lsparent == 'AppTrnCCCommittee') {

                $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab + '&lspage=' + lspage));

                //$location.url('app/AgrMstOnboardingApplicationInfo?onboard_gid = ' + onboard_gid + '&application_gid=' + $scope.application_gid + ' &selectedIndex=' + $scope.selectedIndex + ' &lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + ' &lstab=' + lstab);
                //$location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab));


            }
            else if (lsparent == 'AppCad') {

                $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab + '&lspage=' + lspage));

                //$location.url('app/AgrMstOnboardingApplicationInfo?onboard_gid = ' + onboard_gid + '&application_gid=' + $scope.application_gid + ' &selectedIndex=' + $scope.selectedIndex + ' &lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + ' &lstab=' + lstab);
                //$location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab));


            }
            else {
                $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab + '&lspage=' + lspage));

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