(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAppEditChargeAdd', MstAppEditChargeAdd);

    MstAppEditChargeAdd.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstAppEditChargeAdd($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAppEditChargeAdd';
        var application_gid = $location.search().lsapplication_gid;
        var lstab = $location.search().lstab;
        var lsstatus = $location.search().lsstatus;

        activate();
        lockUI();
        function activate() {
            $scope.amount_validation = true;
            $scope.application_gid = $location.search().lsapplication_gid;
            vm.calender8 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open8 = true;
            };

            vm.calender12 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open12 = true;
            };

            vm.calender13 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open13 = true;
            };

            vm.calender14 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open14 = true;
            };

            vm.calender15 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open15 = true;
            };

            vm.calender16 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open16 = true;
            };

            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
            vm.close = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.closed = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstApplicationEdit/EditProceed';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.proceed_flag = resp.data.proceed_flag;
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.cluster_head;
                $scope.zonalhead = resp.data.zonal_head;
                $scope.regionhead = resp.data.regional_head;
                $scope.businesshead = resp.data.business_head;
                $scope.approveinitiated_flag = resp.data.approveinitiated_flag;
                unlockUI();
            });

            var proceed_flag = $scope.proceed_flag;
            var approveinitiated_flag = $scope.approveinitiated_flag;
            var application_gid = $scope.application_gid;
            var params = {
                application_gid: application_gid
            }

            var url = 'api/MstApplicationAdd/GetApprovalHierarchyFlag';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lshierarchychange_flag = resp.data.lshierarchychange_flag;
                $scope.hierarchyupdated_flag = resp.data.hierarchyupdated_flag;
                if ($scope.hierarchyupdated_flag == 'N' && $scope.approveinitiated_flag == 'N' && proceed_flag == 'Y') {
                    $scope.hierarchyshow = true;
                    $scope.done_disable = true;
                    $scope.done_enable = false;
                    $scope.resubmitshow = false;
                }
                else if ($scope.proceed_flag == 'Y' && $scope.approveinitiated_flag == 'N' && $scope.hierarchyupdated_flag == 'Y') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = false;
                    $scope.done_enable = true;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'Y' && approveinitiated_flag == 'Y') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = true;
                    $scope.done_enable = false;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'N' && approveinitiated_flag == 'N') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = false;
                    $scope.done_disable = true;
                    $scope.resubmitshow = false;
                }
                else {

                }
            });

            var url = 'api/MstApplicationAdd/GetproductDropDown';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.loanproductlist = resp.data.loanproductlist;
                $scope.loantypelist = resp.data.loantypelist;
                $scope.principalfrequencylist = resp.data.principalfrequencylist;
                $scope.interestfrequencylist = resp.data.interestfrequencylist;
                $scope.buyerlist = resp.data.buyerlist;
                $scope.securitytype_list = resp.data.securitytype_list;
            });
            var url = 'api/MstApplicationAdd/ProductchargesTmpClear';
            SocketService.get(url).then(function (resp) {
            });
            
         
            var param = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstApplicationAdd/GetLoanDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstloan_list = resp.data.mstloan_list;
                $scope.servicecharges_list = resp.data.servicecharges_list;
                if ($scope.servicecharges_list != null) {
                    for (var i = 0; i < $scope.servicecharges_list.length; i++) {
                        var lblprocessing_fee = (parseInt($scope.servicecharges_list[i].processing_fee.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.servicecharges_list[i].processingfeeinwords = defaultamountwordschange(lblprocessing_fee);
                        $scope.servicecharges_list[i].lblprocessing_fee = lblprocessing_fee;

                    }
                    for (var i = 0; i < $scope.servicecharges_list.length; i++) {
                        var lbldoc_charges = (parseInt($scope.servicecharges_list[i].doc_charges.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.servicecharges_list[i].doc_chargesinwords = defaultamountwordschange(lbldoc_charges);
                        $scope.servicecharges_list[i].lbldoc_charges = lbldoc_charges;

                    }
                    for (var i = 0; i < $scope.servicecharges_list.length; i++) {
                        var lblfieldvisit_charge = (parseInt($scope.servicecharges_list[i].fieldvisit_charge.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.servicecharges_list[i].fieldvisit_chargeinwords = defaultamountwordschange(lblfieldvisit_charge);
                        $scope.servicecharges_list[i].lblfieldvisit_charge = lblfieldvisit_charge;

                    }
                    for (var i = 0; i < $scope.servicecharges_list.length; i++) {
                        var lbladhoc_fee = (parseInt($scope.servicecharges_list[i].adhoc_fee.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.servicecharges_list[i].adhoc_feeinwords = defaultamountwordschange(lbladhoc_fee);
                        $scope.servicecharges_list[i].lbladhoc_fee = lbladhoc_fee;

                    }
                    for (var i = 0; i < $scope.servicecharges_list.length; i++) {
                        var lbllife_insurance = (parseInt($scope.servicecharges_list[i].life_insurance.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.servicecharges_list[i].life_insuranceinwords = defaultamountwordschange(lbllife_insurance);
                        $scope.servicecharges_list[i].lbllife_insurance = lbllife_insurance;

                    }
                    for (var i = 0; i < $scope.servicecharges_list.length; i++) {
                        var lblacct_insurance = (parseInt($scope.servicecharges_list[i].acct_insurance.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.servicecharges_list[i].acct_insuranceinwords = defaultamountwordschange(lblacct_insurance);
                        $scope.servicecharges_list[i].lblacct_insurance = lblacct_insurance;

                    }
                }
            });
            
            var url = 'api/MstApplicationAdd/Getproduct';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.product_list = resp.data.product_list;
            });
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstApplicationEdit/GetEditProductcharges';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lbloveralllimit_amount = resp.data.overalllimit_amount;
                $scope.lblprocessing_fee = resp.data.processing_fee;
                $scope.lbldoc_charges = resp.data.doc_charges;
                $scope.application_gid = resp.data.application_gid;
                $scope.applicant_type = resp.data.applicant_type;
                $scope.created_date = resp.data.created_date;
                $scope.created_by = resp.data.created_by;
                $scope.productcharge_flag = resp.data.productcharge_flag;
                $scope.economical_flag = resp.data.economical_flag;
                $scope.lblproductcharges_status = resp.data.productcharges_status;
                $scope.application_status = resp.data.application_status;
                $scope.hypothecation_flag = resp.data.hypothecation_flag;
                if ($scope.applicant_type == "" || $scope.applicant_type == null) {
                    $scope.applicant_typenull = true;
                    $scope.applicant_typenotnull = false;
                }
                else {
                    $scope.applicant_typenotnull = true;
                    $scope.applicant_typenull = false;
                }
                if ($scope.hypothecation_flag == 'Y') {
                    $scope.hypothecation_tab = true;
                }
                else {
                    $scope.hypothecation_tab = false;
                }
                if ($scope.economical_flag == 'N') {
                    $scope.social_tradetab = false;
                    $scope.social_trade = true;
                }
                else {
                    $scope.social_tradetab = true;
                    $scope.social_trade = false;
                }

                if ($scope.productcharge_flag == 'N') {
                    $scope.product_chargetab = false;
                    $scope.product_charge = true;
                }
                else {
                    $scope.product_chargetab = true;
                    $scope.product_charge = false;
                }
            });

            //var url = 'api/MstApplicationAdd/GetAppProductcharges';
            //SocketService.get(url).then(function (resp) {
            //    $scope.economical_flag = resp.data.economical_flag;

            //    if ($scope.economical_flag == 'Y') {
            //        $scope.social_tradetab = false;
            //        $scope.social_trade = true;
            //    }
            //    else {
            //        $scope.social_tradetab = true;
            //        $scope.social_trade = false;
            //    }
            //});
        }


        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }
        $scope.overalllimit = function () {
            var application_gid = $scope.application_gid;
            $location.url('app/MstApplcreationProductchargesEdit?lsapplication_gid=' + application_gid + '&lstab=add');
        }

        $scope.hypothecationdtl = function () {
            var application_gid = $scope.application_gid;
            $location.url('app/MstApplicationHypothecationEdit?lsapplication_gid=' + application_gid + '&lstab=add');
        }
        $scope.SubmitOverallLimit = function () {
            var params = {
                overalllimit_amount: $scope.txtOveralllimit_amount,
                validityoveralllimit_year: $scope.txtvalidityoveralllimit_year,
                validityoveralllimit_month: $scope.txtvalidityoveralllimit_month,
                validityoveralllimit_days: $scope.txtvalidityoveralllimit_day,
                calculationoveralllimit_validity: $scope.txtcalculationoveralllimit_validity,
            }

            var url = 'api/MstApplicationAdd/SubmitOverallLimit';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lstab == 'add') {
                        $location.url('app/MstApplicationProductChargesAdd?lstab=' + lstab);
                    }
                    else {
                        $location.url('app/MstApplicationGeneralEdit');
                    }
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
        }
        $scope.producttype = function () {
            var params = {
                loanproduct_gid: $scope.cboProductTypelist.loanproduct_gid, application_gid: '',
                application2loan_gid: ''
            }
            var url = 'api/MstApplicationAdd/GetLoanSubProduct';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.loansubproductlist = resp.data.application_list;
            });
        }

        $scope.addLoan = function () {
            var txtloanfaility_amount = parseInt($scope.txtloanfaility_amount.replace(/[\s,]+/g, '').trim());
            var lsoveralllimit_amount = parseInt($scope.overalllimit_amount);
            var lsloanfacility_amount = parseInt($scope.lsloanfacility_amount);

            if ((txtloanfaility_amount) > (lsoveralllimit_amount - lsloanfacility_amount)) {
                Notify.alert('Amount Exceeded from Overall Limit', 'warning');
            }
            else {
                if ($scope.txtfacilityreqon_date == null || $scope.txtfacilityreqon_date == '' || $scope.cboProductTypelist == null || $scope.cboProductTypelist == '' || $scope.cboProductSubTypelist == null || $scope.cboProductSubTypelist == '' || $scope.cboLoanTypelist == null || $scope.cboLoanTypelist == '' || $scope.txtloanfaility_amount == null || $scope.txtloanfaility_amount == '' || $scope.txtrate_interest == null || $scope.txtrate_interest == '' || $scope.txtpanel_interest == null || $scope.txtpanel_interest == '' || $scope.txttenure_year == null || $scope.txttenure_year == '' || $scope.txttenure_month == null || $scope.txttenure_month == '' || $scope.cboFacilityTypelist == null || $scope.cboFacilityTypelist == '' || $scope.txttenure_days == null || $scope.txttenure_days == '' || $scope.cboFacilitymodelist == null || $scope.cboFacilitymodelist == '') {
                    Notify.alert('Kindly fill all mandatory values', 'warning');
                }
                else {
                    if ($scope.rdbmoratorium_status == 'Yes') {
                        if ($scope.cbomoratorium_type == null || $scope.cbomoratorium_type == '' || $scope.txtmoratorium_startdate == null || $scope.txtmoratorium_startdate == '' || $scope.txtmoratorium_enddate == null || $scope.txtmoratorium_enddate == '') {
                            Notify.alert('Kindly fill Moratorium Details', 'warning');
                        }
                        else {
                            var lsloanproduct_name = '';
                            var lsloanproduct_gid = '';
                            var lsloansubproduct_name = '';
                            var lsloansubproduct_gid = '';
                            var lsloantype_gid = '';
                            var lsloan_type = '';
                            var lsprincipalfrequency_gid = '';
                            var lsprincipalfrequency_name = '';
                            var lsinterestfrequency_name = '';
                            var lsinterestfrequency_gid = '';

                            if ($scope.lsloanproduct_gid != undefined || $scope.cboProductTypelist != null) {
                                lsloanproduct_name = $scope.cboProductTypelist.loanproduct_name;
                                lsloanproduct_gid = $scope.cboProductTypelist.loanproduct_gid;
                            }
                            if ($scope.cboProductSubTypelist != undefined || $scope.cboProductSubTypelist != null) {
                                lsloansubproduct_name = $scope.cboProductSubTypelist.loansubproduct_name;
                                lsloansubproduct_gid = $scope.cboProductSubTypelist.loansubproduct_gid;
                            }
                            if ($scope.cboLoanTypelist != undefined || $scope.cboLoanTypelist != null) {
                                lsloantype_gid = $scope.cboLoanTypelist.loantype_gid;
                                lsloan_type = $scope.cboLoanTypelist.loan_type;
                            }
                            if ($scope.cboprincipalfrequency != undefined || $scope.cboprincipalfrequency != null) {
                                lsprincipalfrequency_gid = $scope.cboprincipalfrequency.principalfrequency_gid;
                                lsprincipalfrequency_name = $scope.cboprincipalfrequency.principalfrequency_name;
                            }
                            if ($scope.cboInterestFrequency != undefined || $scope.cboInterestFrequency != null) {
                                lsinterestfrequency_name = $scope.cboInterestFrequency.interestfrequency_name;
                                lsinterestfrequency_gid = $scope.cboInterestFrequency.interestfrequency_gid;
                            }
                            var params = {
                                product_type: lsloanproduct_name,
                                producttype_gid: lsloanproduct_gid,
                                facilityrequested_date: $scope.txtfacilityreqon_date,
                                productsub_type: lsloansubproduct_name,
                                productsubtype_gid: lsloansubproduct_gid,
                                loantype_gid: lsloantype_gid,
                                loan_type: lsloan_type,
                                facilityloan_amount: $scope.txtloanfaility_amount,
                                rate_interest: $scope.txtrate_interest,
                                penal_interest: $scope.txtpanel_interest,
                                facilityvalidity_year: $scope.txtvalidity_year,
                                facilityvalidity_month: $scope.txtvalidity_month,
                                facilityvalidity_days: $scope.txtvalidity_days,
                                facilityoverall_limit: $scope.txtoverallfacilityvalidity_limit,
                                tenureproduct_year: $scope.txttenure_year,
                                tenureproduct_month: $scope.txttenure_month,
                                tenureproduct_days: $scope.txttenure_days,
                                tenureoverall_limit: $scope.txtoveralltenurevalidity_limit,
                                facility_type: $scope.cboFacilityTypelist,
                                facility_mode: $scope.cboFacilitymodelist,
                                principalfrequency_name: lsprincipalfrequency_name,
                                principalfrequency_gid: lsprincipalfrequency_gid,
                                interestfrequency_name: lsinterestfrequency_name,
                                interestfrequency_gid: lsinterestfrequency_gid,
                                interest_status: $scope.rdbinterest_status,
                                moratorium_status: $scope.rdbmoratorium_status,
                                moratorium_type: $scope.cbomoratorium_type,
                                moratorium_startdate: $scope.txtmoratorium_startdate,
                                moratorium_enddate: $scope.txtmoratorium_enddate,
                                source_type: $scope.cboSourceType,
                                guideline_value: $scope.txtguidelinevalue,
                                guideline_date: $scope.txtguideline_date,
                                market_value: $scope.txtmarketValue,
                                marketvalue_date: $scope.txtmarketvalue_date,
                                forcedsource_value: $scope.txtforcedsource_value,
                                collateralSSV_value: $scope.txtcollateralSSV_value,
                                forcedvalueassessed_on: $scope.txtforcedvalueassessed_on,
                                collateralobservation_summary: $scope.txtcolateralobservation_summary,
                            }
                            var url = 'api/MstApplicationAdd/PostLoanDtl';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();

                                if (resp.data.status == true) {
                                    activate();
                                    $scope.mstloan_list = resp.data.mstloan_list;
                                    $scope.collateral_status = resp.data.collateral_status;
                                    $scope.buyer_status = resp.data.buyer_status;
                                    Notify.alert(resp.data.message, {

                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    $scope.cboProductTypelist = '';
                                    $scope.txtfacilityreqon_date = '';
                                    $scope.cboProductSubTypelist = '';
                                    $scope.cboLoanTypelist = '';
                                    $scope.txtloanfaility_amount = '';
                                    $scope.txtrate_interest = '';
                                    $scope.txtpanel_interest = '';
                                    $scope.txtvalidity_year = '';
                                    $scope.txtvalidity_month = '';
                                    $scope.txtvalidity_days = '';
                                    $scope.txtoverallfacilityvalidity_limit = '';
                                    $scope.txttenure_year = '';
                                    $scope.txttenure_month = '';
                                    $scope.txttenure_days = '';
                                    $scope.txtoveralltenurevalidity_limit = '';
                                    $scope.cboFacilitymodelist = '';
                                    document.getElementById('words_totalamount1').innerHTML = '';
                                    $scope.cboprincipalfrequency = '';
                                    $scope.cboInterestFrequency = '';
                                    $scope.rdbinterest_status = '';
                                    $scope.rdbmoratorium_status = '';
                                    $scope.cbomoratorium_type = '';
                                    $scope.txtmoratorium_startdate = '';
                                    $scope.txtmoratorium_enddate = '';
                                    $scope.cboFacilityTypelist =
                                    $scope.txtenduse_purpose = '';
                                    $scope.cboSourceType = '';
                                    $scope.txtguidelinevalue = '';
                                    $scope.txtguideline_date = '';
                                    $scope.txtmarketValue = '';
                                    $scope.txtmarketvalue_date = '';
                                    $scope.txtforcedsource_value = '';
                                    $scope.txtcollateralSSV_value = '';
                                    $scope.txtforcedvalueassessed_on = '';
                                    $scope.txtcolateralobservation_summary = '';
                                    document.getElementById('words_totalamount2').innerHTML = '';
                                    document.getElementById('words_totalamount3').innerHTML = '';
                                    document.getElementById('words_totalamount4').innerHTML = '';
                                    document.getElementById('words_totalamount5').innerHTML = '';
                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                }
                            });
                        }
                    }
                    else {


                        var lsloanproduct_name = '';
                        var lsloanproduct_gid = '';
                        var lsloansubproduct_name = '';
                        var lsloansubproduct_gid = '';
                        var lsloantype_gid = '';
                        var lsloan_type = '';
                        var lsprincipalfrequency_gid = '';
                        var lsprincipalfrequency_name = '';
                        var lsinterestfrequency_name = '';
                        var lsinterestfrequency_gid = '';

                        if ($scope.lsloanproduct_gid != undefined || $scope.cboProductTypelist != null) {
                            lsloanproduct_name = $scope.cboProductTypelist.loanproduct_name;
                            lsloanproduct_gid = $scope.cboProductTypelist.loanproduct_gid;
                        }
                        if ($scope.cboProductSubTypelist != undefined || $scope.cboProductSubTypelist != null) {
                            lsloansubproduct_name = $scope.cboProductSubTypelist.loansubproduct_name;
                            lsloansubproduct_gid = $scope.cboProductSubTypelist.loansubproduct_gid;
                        }
                        if ($scope.cboLoanTypelist != undefined || $scope.cboLoanTypelist != null) {
                            lsloantype_gid = $scope.cboLoanTypelist.loantype_gid;
                            lsloan_type = $scope.cboLoanTypelist.loan_type;
                        }
                        if ($scope.cboprincipalfrequency != undefined || $scope.cboprincipalfrequency != null) {
                            lsprincipalfrequency_gid = $scope.cboprincipalfrequency.principalfrequency_gid;
                            lsprincipalfrequency_name = $scope.cboprincipalfrequency.principalfrequency_name;
                        }
                        if ($scope.cboInterestFrequency != undefined || $scope.cboInterestFrequency != null) {
                            lsinterestfrequency_name = $scope.cboInterestFrequency.interestfrequency_name;
                            lsinterestfrequency_gid = $scope.cboInterestFrequency.interestfrequency_gid;
                        }
                        var params = {
                            product_type: lsloanproduct_name,
                            producttype_gid: lsloanproduct_gid,
                            facilityrequested_date: $scope.txtfacilityreqon_date,
                            productsub_type: lsloansubproduct_name,
                            productsubtype_gid: lsloansubproduct_gid,
                            loantype_gid: lsloantype_gid,
                            loan_type: lsloan_type,
                            facilityloan_amount: $scope.txtloanfaility_amount,
                            rate_interest: $scope.txtrate_interest,
                            penal_interest: $scope.txtpanel_interest,
                            facilityvalidity_year: $scope.txtvalidity_year,
                            facilityvalidity_month: $scope.txtvalidity_month,
                            facilityvalidity_days: $scope.txtvalidity_days,
                            facilityoverall_limit: $scope.txtoverallfacilityvalidity_limit,
                            tenureproduct_year: $scope.txttenure_year,
                            tenureproduct_month: $scope.txttenure_month,
                            tenureproduct_days: $scope.txttenure_days,
                            tenureoverall_limit: $scope.txtoveralltenurevalidity_limit,
                            facility_type: $scope.cboFacilityTypelist,
                            facility_mode: $scope.cboFacilitymodelist,
                            principalfrequency_name: lsprincipalfrequency_name,
                            principalfrequency_gid: lsprincipalfrequency_gid,
                            interestfrequency_name: lsinterestfrequency_name,
                            interestfrequency_gid: lsinterestfrequency_gid,
                            interest_status: $scope.rdbinterest_status,
                            moratorium_status: $scope.rdbmoratorium_status,
                            moratorium_type: $scope.cbomoratorium_type,
                            moratorium_startdate: $scope.txtmoratorium_startdate,
                            moratorium_enddate: $scope.txtmoratorium_enddate,
                        }
                        var url = 'api/MstApplicationAdd/PostLoanDtl';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                activate();
                                $scope.mstloan_list = resp.data.mstloan_list;
                                $scope.collateral_status = resp.data.collateral_status;
                                $scope.buyer_status = resp.data.buyer_status;
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $scope.cboProductTypelist = '';
                                $scope.txtfacilityreqon_date = '';
                                $scope.cboProductSubTypelist = '';
                                $scope.cboLoanTypelist = '';
                                $scope.txtloanfaility_amount = '';
                                $scope.txtrate_interest = '';
                                $scope.txtpanel_interest = '';
                                $scope.txtvalidity_year = '';
                                $scope.txtvalidity_month = '';
                                $scope.txtvalidity_days = '';
                                $scope.txtoverallfacilityvalidity_limit = '';
                                $scope.txttenure_year = '';
                                $scope.txttenure_month = '';
                                $scope.txttenure_days = '';
                                $scope.txtoveralltenurevalidity_limit = '';
                                $scope.cboFacilitymodelist = '';
                                document.getElementById('words_totalamount1').innerHTML = '';
                                $scope.cboprincipalfrequency = '';
                                $scope.cboInterestFrequency = '';
                                $scope.rdbinterest_status = '';
                                $scope.rdbmoratorium_status = '';
                                $scope.cbomoratorium_type = '';
                                $scope.txtmoratorium_startdate = '';
                                $scope.txtmoratorium_enddate = '';
                                $scope.cboFacilityTypelist = '';
                                $scope.txtenduse_purpose = '';
                                $scope.cboSourceType = '';
                                $scope.txtguidelinevalue = '';
                                $scope.txtguideline_date = '';
                                $scope.txtmarketValue = '';
                                $scope.txtmarketvalue_date = '';
                                $scope.txtforcedsource_value = '';
                                $scope.txtcollateralSSV_value = '';
                                $scope.txtforcedvalueassessed_on = '';
                                $scope.txtcolateralobservation_summary = '';
                                document.getElementById('words_totalamount2').innerHTML = '';
                                document.getElementById('words_totalamount3').innerHTML = '';
                                document.getElementById('words_totalamount4').innerHTML = '';
                                document.getElementById('words_totalamount5').innerHTML = '';
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                        });
                    }
                }
            }
        }
        $scope.Deleteloan = function (application2loan_gid, producttype_gid) {
            var params =
              {
                  application2loan_gid: application2loan_gid,
                  producttype_gid: producttype_gid
              }
            var url = 'api/MstApplicationAdd/DeleteLoan';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.mstloan_list = resp.data.mstloan_list
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
        $scope.collectdeduct = function () {
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;

            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                var processing_fee = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                var doc_charges = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                var fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                var adhoc_fee = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                var life_insurance = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                var personal_accident = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance + personal_accident

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            var personal_accident_deduct = 0;

            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                var processing_fee_deduct = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                var doc_charges_deduct = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                var fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                var adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                var life_insurance_deduct = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                var personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct + personal_accident_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }

        $scope.buyer = function () {
            var params = {
                buyer_gid: $scope.cboBuyer.buyer_gid,
            }
            var url = 'api/MstApplicationAdd/GetBuyerInfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtbuyer_limit = resp.data.buyer_limit;
            });
        }
        $scope.add_buyer = function () {
            if ($scope.cboBuyer == null || $scope.cboBuyer == '' || $scope.txtbill_tenure == null || $scope.txtbill_tenure == '' || $scope.txtmargin == null || $scope.txtmargin == '') {
                Notify.alert('Kindly fill manadatory values', 'warning')
            }
            else {


                var params = {
                    buyer_name: $scope.cboBuyer.buyer_name,
                    buyer_gid: $scope.cboBuyer.buyer_gid,
                    buyer_limit: $scope.txtbuyer_limit,
                    availed_limit: $scope.txtavailed_limit,
                    balance_limit: $scope.txtbalance_limit,
                    bill_tenure: $scope.txtbill_tenure,
                    margin: $scope.txtmargin,
                }
                var url = 'api/MstApplicationAdd/PostBuyer';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.mstbuyer_list = resp.data.mstbuyer_list;
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.cboBuyer = '';
                        $scope.txtbuyer_limit = '';
                        $scope.txtavailed_limit = '';
                        $scope.txtbalance_limit = '';
                        $scope.txtbill_tenure = '';
                        $scope.txtmargin = '';
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });
            }
        }
        $scope.Deletebuyer = function (application2buyer_gid) {
            var params =
              {
                  application2buyer_gid: application2buyer_gid
              }
            var url = 'api/MstApplicationAdd/DeleteBuyer';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.mstbuyer_list = resp.data.mstbuyer_list
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
        $scope.DeleteCharge = function (application2servicecharge_gid) {
            var params =
              {
                  application2servicecharge_gid: application2servicecharge_gid
              }
            var url = 'api/MstApplicationAdd/DeleteCharge';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.servicecharges_list = resp.data.servicecharges_list
                    if ($scope.servicecharges_list != null) {
                        for (var i = 0; i < $scope.servicecharges_list.length; i++) {
                            var lblprocessing_fee = (parseInt($scope.servicecharges_list[i].processing_fee.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.servicecharges_list[i].processingfeeinwords = defaultamountwordschange(lblprocessing_fee);
                            $scope.servicecharges_list[i].lblprocessing_fee = lblprocessing_fee;

                        }
                        for (var i = 0; i < $scope.servicecharges_list.length; i++) {
                            var lbldoc_charges = (parseInt($scope.servicecharges_list[i].doc_charges.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.servicecharges_list[i].doc_chargesinwords = defaultamountwordschange(lbldoc_charges);
                            $scope.servicecharges_list[i].lbldoc_charges = lbldoc_charges;

                        }
                        for (var i = 0; i < $scope.servicecharges_list.length; i++) {
                            var lblfieldvisit_charge = (parseInt($scope.servicecharges_list[i].fieldvisit_charge.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.servicecharges_list[i].fieldvisit_chargeinwords = defaultamountwordschange(lblfieldvisit_charge);
                            $scope.servicecharges_list[i].lblfieldvisit_charge = lblfieldvisit_charge;

                        }
                        for (var i = 0; i < $scope.servicecharges_list.length; i++) {
                            var lbladhoc_fee = (parseInt($scope.servicecharges_list[i].adhoc_fee.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.servicecharges_list[i].adhoc_feeinwords = defaultamountwordschange(lbladhoc_fee);
                            $scope.servicecharges_list[i].lbladhoc_fee = lbladhoc_fee;

                        }
                        for (var i = 0; i < $scope.servicecharges_list.length; i++) {
                            var lbllife_insurance = (parseInt($scope.servicecharges_list[i].life_insurance.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.servicecharges_list[i].life_insuranceinwords = defaultamountwordschange(lbllife_insurance);
                            $scope.servicecharges_list[i].lbllife_insurance = lbllife_insurance;

                        }
                        for (var i = 0; i < $scope.servicecharges_list.length; i++) {
                            var lblacct_insurance = (parseInt($scope.servicecharges_list[i].acct_insurance.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.servicecharges_list[i].acct_insuranceinwords = defaultamountwordschange(lblacct_insurance);
                            $scope.servicecharges_list[i].lblacct_insurance = lblacct_insurance;

                        }
                    }
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.upload_doc = function (val, val1, name) {
            if (($scope.cboDocumentTitle == null) || ($scope.cboDocumentTitle == '') || ($scope.cboDocumentTitle == undefined)) {
                $("#file").val('');
                Notify.alert('Kindly Select the Document Title', 'warning');
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");
                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cboDocumentTitle);
                $scope.uploadfrm = frm;
                var url = 'api/MstApplicationAdd/postcollateraldocument';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $("#file").val('');
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.DocumentList = resp.data.DocumentList;
                        $scope.cboDocumentTitle = '';

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert('File Format Not Supported!', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                });
            }
        }
        $scope.uploaddocumentcancel = function (val, data) {
            var params = { document_gid: val };

            var url = 'api/MstApplicationAdd/deletecollateraldoc';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.DocumentList = resp.data.DocumentList;
                    angular.forEach($scope.DocumentList, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.DocumentList.splice(key, 1);
                        }
                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
        $scope.add_Collateral = function () {
            if ($scope.cboSourceType == null || $scope.cboSourceType == '') {
                Notify.alert('Kindly select source type', 'warning')
            }
            else {

                var params = {
                    source_type: $scope.cboSourceType,
                    guideline_value: $scope.txtguidelinevalue,
                    guideline_date: $scope.txtguideline_date,
                    market_value: $scope.txtmarketValue,
                    marketvalue_date: $scope.txtmarketvalue_date,
                    forcedsource_value: $scope.txtforcedsource_value,
                    collateralSSV_value: $scope.txtcollateralSSV_value,
                    forcedvalueassessed_on: $scope.txtforcedvalueassessed_on,
                    collateralobservation_summary: $scope.txtcolateralobservation_summary,
                }
                var url = 'api/MstApplicationAdd/PostCollateral';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.collatertal_list = resp.data.collatertal_list;
                        $scope.DocumentList = resp.data.DocumentList;
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.cboSourceType = '';
                        document.getElementById('words_totalamount2').innerHTML = '';
                        document.getElementById('words_totalamount3').innerHTML = '';
                        document.getElementById('words_totalamount4').innerHTML = '';
                        document.getElementById('words_totalamount5').innerHTML = '';
                        $scope.txtguidelinevalue = '';
                        $scope.txtguideline_date = '';
                        $scope.txtmarketValue = '';
                        $scope.txtmarketvalue_date = '';
                        $scope.txtforcedsource_value = '';
                        $scope.txtcollateralSSV_value = '';
                        $scope.txtforcedvalueassessed_on = '';
                        $scope.txtcolateralobservation_summary = '';
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });
            }
        }
        $scope.Deletecollateral = function (application2collateral_gid) {
            var params =
              {
                  application2collateral_gid: application2collateral_gid
              }
            var url = 'api/MstApplicationAdd/DeleteCollateral';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.collatertal_list = resp.data.collatertal_list;
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.uploadhypothecationdoc = function (val, val1, name) {
            if (($scope.cbohypodoc_title == null) || ($scope.cbohypodoc_title == '') || ($scope.cbohypodoc_title == undefined)) {
                $("#file").val('');
                Notify.alert('Kindly Select the Document Title', 'warning');
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");
                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cbohypodoc_title);
                $scope.uploadfrm = frm;
                var url = 'api/MstApplicationAdd/PostHypoDoc';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $("#file").val('');
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.hypo_documentList = resp.data.DocumentList;
                        $scope.cbohypodoc_title = '';

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert('File Format Not Supported!', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                });
            }
        }
        $scope.hypodoccancel = function (val, data) {
            var params = { document_gid: val };

            var url = 'api/MstApplicationAdd/deleteHypoDoc';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.hypo_documentList = resp.data.DocumentList;
                    angular.forEach($scope.hypo_documentList, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.hypo_documentList.splice(key, 1);
                        }
                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
        $scope.SubmitHypothecation = function () {
            if ($scope.cboSecurityType == null || $scope.cboSecurityType == '') {
                Notify.alert('Kindly select security type', 'warning')
            }
            else {

                var params = {
                    securitytype_gid: $scope.cboSecurityType.securitytype_gid,
                    security_type: $scope.cboSecurityType.security_type,
                    security_description: $scope.txtsecurity_desc,
                    security_value: $scope.txtSecurity_Value,
                    securityassessed_date: $scope.txtSecurityAssessed_date,
                    asset_id: $scope.txtasset_id,
                    roc_fillingid: $scope.txtroc_fillingid,
                    CERSAI_fillingid: $scope.txtCERSAI_fillingid,
                    hypoobservation_summary: $scope.txthypoobservation_summary,
                    primary_security: $scope.txtprimary_security,
                    application_gid: $location.search().lsapplication_gid
                }
                var url = 'api/MstApplicationAdd/PostHypothecation';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.hypothecation_list = resp.data.hypothecation_list;
                        $scope.hypo_documentList = resp.data.DocumentList;
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        if (lstab == 'add') {
                            $location.url('app/MstApplicationProductChargesAdd?lstab=' + lstab);
                        }
                        else {
                            $location.url('app/MstApplicationGeneralEdit');
                        }
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });
            }
        }

        $scope.DeleteHypothecation = function (application2hypothecation_gid) {
            var params =
              {
                  application2hypothecation_gid: application2hypothecation_gid
              }
            var url = 'api/MstApplicationAdd/DeleteHypothecation';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.hypothecation_list = resp.data.hypothecation_list;
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
        $scope.saveProduct_charges = function () {
            var params = {
                overalllimit_amount: $scope.txtOveralllimit_amount,
                validityoveralllimit_year: $scope.txtvalidityoveralllimit_year,
                validityoveralllimit_month: $scope.txtvalidityoveralllimit_month,
                validityoveralllimit_days: $scope.txtvalidityoveralllimit_day,
                calculationoveralllimit_validity: $scope.txtcalculationoveralllimit_validity,
                enduse_purpose: $scope.txtenduse_purpose,
                processing_fee: $scope.txtprocessing_fee,
                processing_collectiontype: $scope.rdbprocessing_collectiontype,
                doc_charges: $scope.txtdoc_charges,
                doccharge_collectiontype: $scope.rdbdoccharge_collectiontype,
                fieldvisit_charge: $scope.txtfieldvisit_charges,
                fieldvisit_collectiontype: $scope.rdbfieldvisit_collectiontype,
                adhoc_fee: $scope.txtadhoc_fee,
                adhoc_collectiontype: $scope.rdbadhoc_collectiontype,
                life_insurance: $scope.txtlife_insurance,
                lifeinsurance_collectiontype: $scope.rdblifeinsurance_collectiontype,
                acct_insurance: $scope.txtacct_insurance,
                acctinsurance_collectiontype: $scope.rdbpersonalaccident_collectiontype,
                total_collect: $scope.txttotal_collect,
                total_deduct: $scope.txttotal_deduct,
                application_gid: $scope.application_gid
            }
            var url = 'api/MstApplicationAdd/PostProductCharges';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    window.scrollTo(0, 0);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.Submitproduct = function () {
            var params = {
                overalllimit_amount: $scope.txtOveralllimit_amount,
                validityoveralllimit_year: $scope.txtvalidityoveralllimit_year,
                validityoveralllimit_month: $scope.txtvalidityoveralllimit_month,
                validityoveralllimit_days: $scope.txtvalidityoveralllimit_day,
                calculationoveralllimit_validity: $scope.txtcalculationoveralllimit_validity,
                enduse_purpose: $scope.txtenduse_purpose,
                processing_fee: $scope.txtprocessing_fee,
                processing_collectiontype: $scope.rdbprocessing_collectiontype,
                doc_charges: $scope.txtdoc_charges,
                doccharge_collectiontype: $scope.rdbdoccharge_collectiontype,
                fieldvisit_charge: $scope.txtfieldvisit_charges,
                fieldvisit_collectiontype: $scope.rdbfieldvisit_collectiontype,
                adhoc_fee: $scope.txtadhoc_fee,
                adhoc_collectiontype: $scope.rdbadhoc_collectiontype,
                life_insurance: $scope.txtlife_insurance,
                lifeinsurance_collectiontype: $scope.rdblifeinsurance_collectiontype,
                acct_insurance: $scope.txtacct_insurance,
                acctinsurance_collectiontype: $scope.rdbpersonalaccident_collectiontype,
                total_collect: $scope.txttotal_collect,
                total_deduct: $scope.txttotal_deduct,
                application_gid: $scope.application_gid
            }
            var url = 'api/MstApplicationAdd/PostSubmitProductCharges';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    window.scrollTo(0, 0);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.SubmitServiceCharges = function () {
            if ($scope.txtprocessing_fee == '' || $scope.txtprocessing_fee == null || $scope.txtprocessing_fee == undefined) {
                var lsprocessing_fee = null;
            }
            else {
                var lsprocessing_fee = parseInt($scope.txtprocessing_fee.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.txtdoc_charges == '' || $scope.txtdoc_charges == null || $scope.txtdoc_charges == undefined) {
                var lsdoc_charges = null;               
            }
            else {
                var lsdoc_charges = parseInt($scope.txtdoc_charges.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.txtfieldvisit_charges == '' || $scope.txtfieldvisit_charges == null || $scope.txtfieldvisit_charges == undefined) {
                var lsfieldvisit_charges = null;               
            }
            else {
                var lsfieldvisit_charges = parseInt($scope.txtfieldvisit_charges.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.txtadhoc_fee == '' || $scope.txtadhoc_fee == null || $scope.txtadhoc_fee == undefined) {
                var lsadhoc_fee = null;                
            }
            else {
                var lsadhoc_fee = parseInt($scope.txtadhoc_fee.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.txtlife_insurance == '' || $scope.txtlife_insurance == null || $scope.txtlife_insurance == undefined) {
                var lslife_insurance = null;               
            }
            else {
                var lslife_insurance = parseInt($scope.txtlife_insurance.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.txtacct_insurance == '' || $scope.txtacct_insurance == null || $scope.txtacct_insurance == undefined) {
                var lsacct_insurance = null;               
            }
            else {
                var lsacct_insurance = parseInt($scope.txtacct_insurance.replace(/[\s,]+/g, '').trim());
            }

            var params = {
                processing_fee: lsprocessing_fee,
                processing_collectiontype: $scope.rdbprocessing_collectiontype,
                doc_charges: lsdoc_charges,
                doccharge_collectiontype: $scope.rdbdoccharge_collectiontype,
                fieldvisit_charge: lsfieldvisit_charges,
                fieldvisit_collectiontype: $scope.rdbfieldvisit_collectiontype,
                adhoc_fee: lsadhoc_fee,
                adhoc_collectiontype: $scope.rdbadhoc_collectiontype,
                life_insurance: lslife_insurance,
                lifeinsurance_collectiontype: $scope.rdblifeinsurance_collectiontype,
                acct_insurance: lsacct_insurance,
                acctinsurance_collectiontype: $scope.rdbpersonalaccident_collectiontype,
                total_collect: document.getElementById("total_collect").value,
                total_deduct: document.getElementById("total_deduct").value,
                productTypelist: $scope.cboProductTypelist,
                application_gid: $scope.application_gid
            }
            var url = 'api/MstApplicationAdd/PostServiceCharges';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var param = {
                        application_gid: $scope.application_gid
                    }
                    var url = 'api/MstApplicationAdd/Getproduct';
                    SocketService.getparams(url, param).then(function (resp) {
                        unlockUI();
                        $scope.product_list = resp.data.product_list;
                    });
                    $scope.servicecharges_list = resp.data.servicecharges_list;
                    if ($scope.servicecharges_list != null) {
                        for (var i = 0; i < $scope.servicecharges_list.length; i++) {
                            var lblprocessing_fee = (parseInt($scope.servicecharges_list[i].processing_fee.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.servicecharges_list[i].processingfeeinwords = defaultamountwordschange(lblprocessing_fee);
                            $scope.servicecharges_list[i].lblprocessing_fee = lblprocessing_fee;

                        }
                        for (var i = 0; i < $scope.servicecharges_list.length; i++) {
                            var lbldoc_charges = (parseInt($scope.servicecharges_list[i].doc_charges.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.servicecharges_list[i].doc_chargesinwords = defaultamountwordschange(lbldoc_charges);
                            $scope.servicecharges_list[i].lbldoc_charges = lbldoc_charges;

                        }
                        for (var i = 0; i < $scope.servicecharges_list.length; i++) {
                            var lblfieldvisit_charge = (parseInt($scope.servicecharges_list[i].fieldvisit_charge.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.servicecharges_list[i].fieldvisit_chargeinwords = defaultamountwordschange(lblfieldvisit_charge);
                            $scope.servicecharges_list[i].lblfieldvisit_charge = lblfieldvisit_charge;

                        }
                        for (var i = 0; i < $scope.servicecharges_list.length; i++) {
                            var lbladhoc_fee = (parseInt($scope.servicecharges_list[i].adhoc_fee.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.servicecharges_list[i].adhoc_feeinwords = defaultamountwordschange(lbladhoc_fee);
                            $scope.servicecharges_list[i].lbladhoc_fee = lbladhoc_fee;

                        }
                        for (var i = 0; i < $scope.servicecharges_list.length; i++) {
                            var lbllife_insurance = (parseInt($scope.servicecharges_list[i].life_insurance.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.servicecharges_list[i].life_insuranceinwords = defaultamountwordschange(lbllife_insurance);
                            $scope.servicecharges_list[i].lbllife_insurance = lbllife_insurance;

                        }
                        for (var i = 0; i < $scope.servicecharges_list.length; i++) {
                            var lblacct_insurance = (parseInt($scope.servicecharges_list[i].acct_insurance.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.servicecharges_list[i].acct_insuranceinwords = defaultamountwordschange(lblacct_insurance);
                            $scope.servicecharges_list[i].lblacct_insurance = lblacct_insurance;

                        }
                    }
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.txtprocessing_fee = '';
                    $scope.rdbprocessing_collectiontype = '';
                    $scope.txtdoc_charges = '';
                    $scope.rdbdoccharge_collectiontype = '';
                    $scope.txtfieldvisit_charges = '';
                    $scope.rdbfieldvisit_collectiontype = '';
                    $scope.txtadhoc_fee = '';
                    $scope.rdbadhoc_collectiontype = '';
                    $scope.txtlife_insurance = '';
                    $scope.rdblifeinsurance_collectiontype = '';
                    $scope.txtacct_insurance = '';
                    $scope.rdbpersonalaccident_collectiontype = '';
                    $scope.txttotal_collect = '';
                    $scope.txttotal_deduct = '';
                    $scope.cboProductTypelist = '';
                    document.getElementById('words_totalamount51').innerHTML = '';
                    document.getElementById('words_totalamount52').innerHTML = '';
                    document.getElementById('words_totalamount53').innerHTML = '';
                    document.getElementById('words_totalamount54').innerHTML = '';
                    document.getElementById('words_totalamount55').innerHTML = '';
                    document.getElementById('words_totalamount56').innerHTML = '';
                    document.getElementById("total_collect").value = '';
                    document.getElementById("total_deduct").value = '';
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
        $scope.txtfacility = function () {
            var input = document.getElementById('valueamount').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount1 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtvalue = "";
            }
            else {
                //  $scope.txtvalue = output;
                document.getElementById('words_totalamount1').innerHTML = lswords_totalamount1;
                var txtloanfaility_amount = parseInt($scope.txtloanfaility_amount.replace(/[\s,]+/g, '').trim());
                var lsoveralllimit_amount = parseInt($scope.overalllimit_amount);
                var lsloanfacility_amount = parseInt($scope.lsloanfacility_amount);
                if ((txtloanfaility_amount) > (lsoveralllimit_amount - lsloanfacility_amount)) {
                    $scope.amount_validation = false;
                }
                else {
                    $scope.amount_validation = true;
                }
                var lsamount = (lsoveralllimit_amount - lsloanfacility_amount);
                $scope.txtremaining = (lsamount - txtloanfaility_amount);
            }

        }
        // function inWords1(num) {
        //     var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
        //     var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
        //     var s = num.toString();
        //     s = s.replace(/[\, ]/g, '');
        //     if (s != parseFloat(s)) return '';
        //     if ((num = num.toString()).length > 9) return 'Overflow';
        //     var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
        //     if (!n) return; var str = '';
        //     str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
        //     str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
        //     str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
        //     str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';

        //     str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
        //     return str;
        // }

        $scope.Overalllimit_amountchange = function () {
            var input = document.getElementById('Overalllimitamount').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount7 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtOveralllimit_amount = "";
            }
            else {

                // $scope.txtOveralllimit_amount = output;
                document.getElementById('words_totalamount7').innerHTML = lswords_totalamount7;
            }
        }

        $scope.calculatelimit = function () {
            if ($scope.txtvalidityoveralllimit_year != undefined || $scope.txtvalidityoveralllimit_year != null) {
                var lsyear = $scope.txtvalidityoveralllimit_year + " - Year, ";
            }
            else {
                var lsyear = '';
            }
            if ($scope.txtvalidityoveralllimit_month != undefined || $scope.txtvalidityoveralllimit_month != null) {
                var lsmonth = $scope.txtvalidityoveralllimit_month + " - Month, ";
            }
            else {
                var lsmonth = '';
            }
            if ($scope.txtvalidityoveralllimit_day != undefined || $scope.txtvalidityoveralllimit_day != null) {
                var lsday = $scope.txtvalidityoveralllimit_day + " - Day";
            }
            else {
                var lsday = '';
            }
            $scope.txtcalculationoveralllimit_validity = lsyear + lsmonth + lsday;
        }
        $scope.calculatefacility = function () {
            if ($scope.txtvalidity_year != undefined || $scope.txtvalidity_year != null) {
                var lsyear = $scope.txtvalidity_year + " - Year, ";
            }
            else {
                var lsyear = '';
            }
            if ($scope.txtvalidity_month != undefined || $scope.txtvalidity_month != null) {
                var lsmonth = $scope.txtvalidity_month + " - Month, ";
            }
            else {
                var lsmonth = '';
            }
            if ($scope.txtvalidity_days != undefined || $scope.txtvalidity_days != null) {
                var lsday = $scope.txtvalidity_days + " - Day";
            }
            else {
                var lsday = '';
            }
            $scope.txtoverallfacilityvalidity_limit = lsyear + lsmonth + lsday;
        }
        $scope.calculatetenure = function () {
            if ($scope.txttenure_year != undefined || $scope.txttenure_year != null) {
                var lsyear = $scope.txttenure_year + " - Year, ";
            }
            else {
                var lsyear = '';
            }
            if ($scope.txttenure_month != undefined || $scope.txttenure_month != null) {
                var lsmonth = $scope.txttenure_month + " - Month, ";
            }
            else {
                var lsmonth = '';
            }
            if ($scope.txttenure_days != undefined || $scope.txttenure_days != null) {
                var lsday = $scope.txttenure_days + " - Day";
            }
            else {
                var lsday = '';
            }
            $scope.txtoveralltenurevalidity_limit = lsyear + lsmonth + lsday;
        }
        $scope.txtguidelinevaluechange = function () {
            var input = document.getElementById('GuidelineValue').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount2 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtguidelinevalue = "";
            }
            else {
                // $scope.txtguidelinevalue = output;
                document.getElementById('words_totalamount2').innerHTML = lswords_totalamount2;
            }
        }


        $scope.txtMarketValuechange = function () {
            var input = document.getElementById('MarketValue').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount3 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtMarketValue = "";
            }
            else {
                //  $scope.txtMarketValue = output;
                document.getElementById('words_totalamount3').innerHTML = lswords_totalamount3;
            }
        }

        $scope.txtForcedSourceValuechange = function () {
            var input = document.getElementById('ForcedSourceValue').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount4 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtForcedSourceValue = "";
            }
            else {
                //  $scope.txtForcedSourceValue = output;
                document.getElementById('words_totalamount4').innerHTML = lswords_totalamount4;
            }
        }

        $scope.txtCollateralSSVvaluechange = function () {
            var input = document.getElementById('CollateralSSVvalue').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount5 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtCollateralSSVvalue = "";
            }
            else {
                // $scope.txtCollateralSSVvalue = output;
                document.getElementById('words_totalamount5').innerHTML = lswords_totalamount5;
            }
        }
        $scope.txtSecurityValuechange = function () {
            var input = document.getElementById('SecurityValue').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount6 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtSecurity_Value = "";
            }
            else {
                // $scope.txtSecurity_Value = output;
                document.getElementById('words_totalamount6').innerHTML = lswords_totalamount6;
            }
        }
        $scope.txtamountchange = function () {
            var input = document.getElementById('MarketValue').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount3 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtMarketValue = "";
            }
            else {
                // $scope.txtMarketValue = output;
                document.getElementById('words_totalamount3').innerHTML = lswords_totalamount3;
            }
        }
        $scope.txtamountchange1 = function () {
            var input = document.getElementById('processingfee').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount51 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtprocessing_fee = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount51').innerHTML = lswords_totalamount51;
                var txtprocessing_fee = parseInt($scope.txtprocessing_fee.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtprocessing_fee = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;           

            if ($scope.rdbprocessing_collectiontype == 'Collect') {
               var processing_fee = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                var doc_charges = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                var fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                var adhoc_fee = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                var life_insurance = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                var personal_accident = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance + personal_accident

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            var personal_accident_deduct = 0;

            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                var processing_fee_deduct = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                var doc_charges_deduct = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                var fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                var adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                var life_insurance_deduct = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }

            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                var personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
            }

            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct + personal_accident_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }

        $scope.txtamountchange2 = function () {
            var input = document.getElementById('doc_charges').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount52 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdoc_charges = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount52').innerHTML = lswords_totalamount52;
                var txtdoc_charges = parseInt($scope.txtdoc_charges.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtdoc_charges = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;

            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                var processing_fee = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                var doc_charges = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                var fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                var adhoc_fee = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                var life_insurance = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                var personal_accident = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance + personal_accident

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            var personal_accident_deduct = 0;

            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                var processing_fee_deduct = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                var doc_charges_deduct = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                var fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                var adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                var life_insurance_deduct = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                var personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct + personal_accident_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }

        $scope.txtamountchange3 = function () {
            var input = document.getElementById('fieldvisit_charges').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount53 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtfieldvisit_charges = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount53').innerHTML = lswords_totalamount53;
                var txtfieldvisit_charges = parseInt($scope.txtfieldvisit_charges.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtfieldvisit_charges = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;
            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                var processing_fee = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                var doc_charges = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                var fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                var adhoc_fee = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                var life_insurance = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                var personal_accident = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance +personal_accident

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            var personal_accident_deduct = 0;
            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                var processing_fee_deduct = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                var doc_charges_deduct = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                var fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                var adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                var life_insurance_deduct = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                var personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct + personal_accident_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }
        $scope.txtamountchange4 = function () {
            var input = document.getElementById('adhoc_fee').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount54 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtadhoc_fee = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount54').innerHTML = lswords_totalamount54;
                var txtadhoc_fee = parseInt($scope.txtadhoc_fee.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtadhoc_fee = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;

            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                var processing_fee = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                var doc_charges = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                var fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                var adhoc_fee = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                var life_insurance = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                var personal_accident = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance + personal_accident

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            var personal_accident_deduct = 0;

            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                var processing_fee_deduct = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                var doc_charges_deduct = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                var fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                var adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                var life_insurance_deduct = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                var personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct + personal_accident_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }

        $scope.txtamountchange5 = function () {
            var input = document.getElementById('life_insurance').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount55 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtlife_insurance = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount55').innerHTML = lswords_totalamount55;
                var txtlife_insurance = parseInt($scope.txtlife_insurance.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtlife_insurance = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;

            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                var processing_fee = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                var doc_charges = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                var fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                var adhoc_fee = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                var life_insurance = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                var personal_accident = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance + personal_accident

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            var personal_accident_deduct = 0;

            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                var processing_fee_deduct = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                var doc_charges_deduct = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                var fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                var adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                var life_insurance_deduct = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                var  personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct + personal_accident_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }
        $scope.txtamountchange6 = function () {
            var input = document.getElementById('acct_insurance').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount56 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtacct_insurance = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount56').innerHTML = lswords_totalamount56;
                var txtacct_insurance = parseInt($scope.txtacct_insurance.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtacct_insurance = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;

            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                var processing_fee = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                var doc_charges = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                var fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                var adhoc_fee = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                var life_insurance = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                var personal_accident = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance + personal_accident

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            var personal_accident_deduct = 0;

            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                var processing_fee_deduct = parseInt(document.getElementById("processingfee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                var doc_charges_deduct = parseInt(document.getElementById("doc_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                var fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                var adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                var life_insurance_deduct = parseInt(document.getElementById("life_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                var  personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value.replace(/[\s,]+/g, '').trim());
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct + personal_accident_deduct

            document.getElementById("total_deduct").value = result_deduct;
        }
        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("EMS");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.general_Tab = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/MstApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Company_dtls = true;
            }
        }

        $scope.company_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/MstApplicationInstitutionEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Institution_dtls = true;
            }
        }
        $scope.Group_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/MstApplicationGroupEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Group_dtls = true;
            }
        }

        $scope.Individual_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/MstApplicationIndividualEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Individual_dtls = true;
            }
        }

        $scope.EconomicCapital_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/MstApplicationSocialTradeCapitalEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.EconomicCapital_dtls = true;
            }
        }

        $scope.ProductCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.ProductCharges_dtls = true;
            }
            else {
                $location.url('app/MstApplcreationProductchargesEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.EditHypothecation = function (application_gid) {
            $location.url('app/MstApplicationHypothecationEdit?lsapplication_gid=' + application_gid + '&lstab=edit');
        }
        $scope.Hypothecation_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/MstAppEditHypothecationAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }

        $scope.OverallLimit_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/MstAppEditOverallLimitAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.ProductCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/MstAppEditProductAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.ServiceCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/MstAppEditChargeAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.BureauUpdates_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/MstApplicationEditCICUploadAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.BureauUpdates_dtls = true;
            }
        }

        $scope.doneclick = function () {
            lockUI();
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstApplicationAdd/GetOverallInfo';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.application_status = resp.data.application_status;
            });
            lockUI();
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstApplicationEdit/EditProceed';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.proceed_flag = resp.data.proceed_flag;
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.cluster_head;
                $scope.zonalhead = resp.data.zonal_head;
                $scope.regionhead = resp.data.regional_head;
                $scope.businesshead = resp.data.business_head;
                $scope.approveinitiated_flag = resp.data.approveinitiated_flag;
                unlockUI();
            });
            var proceed_flag = $scope.proceed_flag;
            var approveinitiated_flag = $scope.approveinitiated_flag;
            var application_gid = $scope.application_gid;
            var params = {
                application_gid: application_gid
            }

            var url = 'api/MstApplicationAdd/GetApprovalHierarchyFlag';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lshierarchychange_flag = resp.data.lshierarchychange_flag;
                $scope.hierarchyupdated_flag = resp.data.hierarchyupdated_flag;
                if ($scope.hierarchyupdated_flag == 'N' && $scope.approveinitiated_flag == 'N' && proceed_flag == 'Y') {
                    $scope.hierarchyshow = true;
                    $scope.done_disable = true;
                    $scope.done_enable = false;
                    $scope.resubmitshow = false;
                }
                else if ($scope.proceed_flag == 'Y' && $scope.approveinitiated_flag == 'N' && $scope.hierarchyupdated_flag == 'Y') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = false;
                    $scope.done_enable = true;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'Y' && approveinitiated_flag == 'Y') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = true;
                    $scope.done_enable = false;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'N' && approveinitiated_flag == 'N') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = false;
                    $scope.done_disable = true;
                    $scope.resubmitshow = false;
                }
                else {

                }
            });
        }

        $scope.Back = function () {
            $state.go('app.MstApplicationCreationSummary');
        }
        $scope.overallsubmit_application = function () {
            var url = 'api/MstApplicationEdit/EditAppProceed';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $state.go('app.MstApplicationCreationSummary');
            });

        }

        $scope.hierarchy_change = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/HierarchyChange.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            var application_gid = $scope.application_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    application_gid: application_gid
                }

                var url = 'api/MstApplicationAdd/FnKycDcoumentValidation';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == false) {

                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $modalInstance.close('closed');
                    }
                    else {

                    }

                });


                var url = 'api/MstApplicationAdd/GetApprovalHierarchyChangeList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.rm_name = resp.data.rm_name;
                    $scope.directreportingto_name = resp.data.directreportingto_name;
                    $scope.clustermanager_gid = resp.data.clustermanager_gid;
                    $scope.clustermanager_name = resp.data.clustermanager_name;
                    $scope.regionalhead_gid = resp.data.regionalhead_gid;
                    $scope.regionhead_name = resp.data.regionhead_name;
                    $scope.zonalhead_gid = resp.data.zonalhead_gid;
                    $scope.zonalhead_name = resp.data.zonalhead_name;
                    $scope.businesshead_gid = resp.data.businesshead_gid;
                    $scope.businesshead_name = resp.data.businesshead_name;
                });

                $scope.Update_hierarchy = function () {
                    var params = {
                        application_gid: application_gid,
                        clustermanager_gid: $scope.clustermanager_gid,
                        clustermanager_name: $scope.clustermanager_name,
                        regionalhead_gid: $scope.regionalhead_gid,
                        regionalhead_name: $scope.regionhead_name,
                        zonalhead_gid: $scope.zonalhead_gid,
                        zonalhead_name: $scope.zonalhead_name,
                        businesshead_gid: $scope.businesshead_gid,
                        businesshead_name: $scope.businesshead_name
                    }
                    var url = 'api/MstApplicationAdd/UpdateApprovalHierarchyChange';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $modalInstance.close('closed');
                    });
                    $modalInstance.close('closed');
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

    }
})();

