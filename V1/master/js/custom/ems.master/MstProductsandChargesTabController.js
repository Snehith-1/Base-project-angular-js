(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstProductsandChargesTabController', MstProductsandChargesTabController);

    MstProductsandChargesTabController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstProductsandChargesTabController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstProductsandChargesTabController';
        var application_gid = $location.search().lsapplication_gid;
        var application_gid = $scope.application_gid;
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

            var url = 'api/MstApplicationAdd/GetCSAActivityDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.csactivity_list = resp.data.csactivity_list;
            });

            lockUI();
            var url = 'api/MstApplicationAdd/GetproductDropDown';
               SocketService.get(url).then(function (resp) {
                   unlockUI();
                   $scope.loanproductlist = resp.data.loanproductlist;
                   $scope.loantypelist = resp.data.loantypelist;
                   $scope.principalfrequencylist = resp.data.principalfrequencylist;
                   $scope.interestfrequencylist = resp.data.interestfrequencylist;
                   $scope.buyerlist = resp.data.buyerlist;
                   $scope.securitytype_list = resp.data.securitytype_list;
                   $scope.programlist = resp.data.programlist;
               });
               var url = 'api/MstApplicationAdd/GetDropDown';
               SocketService.get(url).then(function (resp) {
                   $scope.productname_list = resp.data.productname_list;
               });
               var url = 'api/MstApplicationAdd/GetDropDown';
               SocketService.get(url).then(function (resp) {
                   $scope.valuechainlist = resp.data.valuechainlist;
               });
               var url = 'api/MstApplicationAdd/ProductchargesTmpClear';
               SocketService.get(url).then(function (resp) {
               });
               var param = {
                   application_gid: $scope.application_gid 
               }
               var url = 'api/MstApplicationAdd/GetLoanDtl';
               SocketService.getparams(url,param).then(function (resp) {
                   $scope.mstloan_list = resp.data.mstloan_list;
                   for (var i = 0; i < $scope.mstloan_list.length; i++) {
                       var lblloanfacility_amount = (parseInt($scope.mstloan_list[i].loanfacility_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                       $scope.mstloan_list[i].loanfacility_amountinwords = defaultamountwordschange(lblloanfacility_amount);
                       $scope.mstloan_list[i].lblloanfacility_amount = lblloanfacility_amount;
                   }
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
              
               var url = 'api/MstApplicationAdd/GetLimit';
               SocketService.getparams(url, param).then(function (resp) {
                   unlockUI();
                   $scope.lbloveralllimit_amount = resp.data.overalllimit_amount;
                   $scope.lsloanfacility_amount = resp.data.loanfacility_amount;
                   if(resp.data.loanfacility_amount==''||resp.data.loanfacility_amount==null)
                   {
                       $scope.lsloanfacility_amount = '0';
                   }
                
               });
               var url = 'api/MstApplicationAdd/Getproduct';
               SocketService.getparams(url, param).then(function (resp) {
                   unlockUI();
                   $scope.product_list = resp.data.product_list;
               });
               var url = 'api/MstApplicationAdd/GetOverallInfo';
               SocketService.getparams(url, param).then(function (resp) {
                   $scope.lblapplication_no = resp.data.application_no;
                   $scope.lblcustomer_name = resp.data.customer_name;
                   $scope.lblvertical_name = resp.data.vertical_name;
                   $scope.lblcustomer_urn = resp.data.customer_urn;
                   $scope.lblcreated_by = resp.data.created_by;
                   $scope.lblcreated_date = resp.data.created_date;
                   $scope.application_gid = resp.data.application_gid;
                   $scope.application_status = resp.data.application_status;
                   $scope.applicant_type = resp.data.applicant_type;
                   $scope.productcharge_flag = resp.data.productcharge_flag;
                   $scope.hypothecation_flag = resp.data.hypothecation_flag;
                   $scope.overalllimit_amount = resp.data.overalllimit_amount;
                   if ($scope.overalllimit_amount != null && $scope.overalllimit_amount != "") {
                       $scope.product_charge = true;
                       $scope.overalllimit_adddtl = false;
                   }
                   else if ($scope.overalllimit_amount == null || $scope.overalllimit_amount == "" || $scope.overalllimit_amount == undefined) {
                       $scope.overalllimit_adddtl = true;
                       $scope.product_charge = false;
                   }
                   else {

                   }

                   //if(resp.data.productcharge_flag=='Y')
                   //{
                   //    $scope.limit = false;
                   //}
                   //else {
                   //    $scope.limit = true;
                   //}
                   if (resp.data.hypothecation_flag == 'Y') {
                       $scope.hypothecation = false;
                   }
                   else {
                       $scope.hypothecation = true;
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
               var url = 'api/MstApplicationAdd/GetProceed';
               SocketService.get(url).then(function (resp) {
                   $scope.proceed_flag = resp.data.proceed_flag;
                   $scope.level_zero = resp.data.level_zero;
                   $scope.level_one = resp.data.level_one;
                   $scope.clusterhead = resp.data.cluster_head;
                   $scope.zonalhead = resp.data.zonal_head;
                   $scope.regionhead = resp.data.regional_head;
                   $scope.businesshead = resp.data.business_head;
                   unlockUI();
               });
               var proceed_flag = $scope.proceed_flag;
               var application_gid = $scope.application_gid;
               var params = {
                   application_gid: application_gid
               }

               var url = 'api/MstApplicationAdd/GetApprovalHierarchyFlag';
               SocketService.getparams(url, params).then(function (resp) {
                   unlockUI();
                   $scope.lshierarchychange_flag = resp.data.lshierarchychange_flag;
                   $scope.hierarchyupdated_flag = resp.data.hierarchyupdated_flag;
                   if ($scope.hierarchyupdated_flag == 'N' && proceed_flag == 'Y') {
                       $scope.hierarchyshow = true;
                       $scope.done_disable = true;
                       $scope.done_enable = false;
                   }
                   else if ($scope.hierarchyupdated_flag == 'Y' && proceed_flag == 'Y') {
                       $scope.done_enable = true;
                       $scope.hierarchyshow = false;
                       $scope.done_disable = false;
                   }
                   else if (proceed_flag == 'N') {
                       $scope.done_disable = true;
                       $scope.hierarchyshow = false;
                       $scope.done_enable = false;
                   }
                   else {

                   }
               });
        }

        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }

        $scope.productname_change = function (cboproduct_name) {
            var params = {
                product_gid: $scope.cboproduct_name.product_gid
            }
            var url = 'api/MstApplicationAdd/GetSectorcategory';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.businessunit_gid = resp.data.businessunit_gid;
                $scope.txtsector_name = resp.data.businessunit_name;
                $scope.valuechain_gid = resp.data.valuechain_gid;
                $scope.txtcategory_name = resp.data.valuechain_name;
                $scope.varietyname_list = resp.data.varietyname_list;
            });
        }

        $scope.Variety_change = function (cbovariety_name) {
            var params = {
                variety_gid: $scope.cbovariety_name.variety_gid
            }
            var url = 'api/MstApplicationAdd/GetVarietyDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.product_gid = resp.data.product_gid;
                $scope.variety_gid = resp.data.variety_gid;
                $scope.variety_name = resp.data.variety_name;
                $scope.txtbotanical_name = resp.data.botanical_name;
                $scope.txtalternative_name = resp.data.alternative_name;
            });
            $scope.txtbotanical_name = '';
            $scope.txtalternative_name = '';
        }

        $scope.overalllimit=function()
        {
            var application_gid = $scope.application_gid;
            $location.url('app/MstApplcreationProductchargesEdit?lsapplication_gid=' + application_gid + '&lstab=add');
        }
       
        $scope.hypothecationdtl = function () {
            var application_gid = $scope.application_gid;
            $location.url('app/MstApplicationHypothecationEdit?lsapplication_gid=' + application_gid + '&lstab=add');
        }
        $scope.SubmitOverallLimit = function () {
            var lssector_gid = '';
            var lssector_name = '';

            var lsOveralllimit_amount = parseInt($scope.txtOveralllimit_amount.replace(/[\s,]+/g, '').trim());
            var lsloanfacility_amount = parseInt($scope.lsloanfacility_amount);

            if ($scope.cboCSAActivity != undefined || $scope.cboCSAActivity != null) {
                lssector_gid = $scope.cboCSAActivity.sector_gid;
                lssector_name = $scope.cboCSAActivity.sector_name;
            }

            if (lsOveralllimit_amount < lsloanfacility_amount) {
                Notify.alert('Amount Exceeded from entered loan facility amount', 'warning');
            }
            else if ((($scope.rdbcsaapplicability == '') || ($scope.rdbcsaapplicability == undefined) || ($scope.rdbcsaapplicability == '')) &&
                (($scope.cboCSAActivity == undefined) || ($scope.cboCSAActivity == '') || ($scope.cboCSAActivity == undefined))) {
                Notify.alert('Kindly Select CSA Activity Field', 'warning');
            }
            else {
                $scope.overlimit_warning = true;

                var params = {
                    overalllimit_amount: lsOveralllimit_amount,
                    validityoveralllimit_year: $scope.txtvalidityoveralllimit_year,
                    validityoveralllimit_month: $scope.txtvalidityoveralllimit_month,
                    validityoveralllimit_days: $scope.txtvalidityoveralllimit_day,
                    calculationoveralllimit_validity: $scope.txtcalculationoveralllimit_validity,
                    application_gid: $scope.application_gid,
                    csa_applicability: $scope.rdbcsaapplicability,
                    csaactivity_gid: lssector_gid,
                    csaactivity_name: lssector_name,
                    percentageoftotal_limit: $scope.txtpercentageoftotal_limit
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
                            $location.url('app/MstApplicationProductChargesAdd?lsapplication_gid=' + $scope.application_gid + '&lstab=add');
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

        $scope.Csa_Applicability = function (rdbcsaapplicability) {
            var rdbcsaapplicability = rdbcsaapplicability;
            if (rdbcsaapplicability == 'Yes') {
                $scope.csaactivity_show = true;
            }
            else {
                $scope.csaactivity_show = false;
            }
        } 

        $scope.producttype=function()
        {
            var params = {
                loanproduct_gid: $scope.cboProductTypelist.loanproduct_gid, application_gid: '',
                application2loan_gid: ''
            }
            var url = 'api/MstApplicationAdd/GetLoanSubProduct';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.loansubproductlist = resp.data.application_list;
            });
        }
     
        $scope.addLoan=function()
        {
            var txtloanfaility_amount = parseInt($scope.txtloanfaility_amount.replace(/[\s,]+/g, '').trim());
            var lsoveralllimit_amount = parseInt($scope.overalllimit_amount);
            var lsloanfacility_amount = parseInt($scope.lsloanfacility_amount);

            if ((txtloanfaility_amount) > (lsoveralllimit_amount - lsloanfacility_amount)) {
                Notify.alert('Amount Exceeded from Overall Limit', 'warning');
            }
            else if (($scope.cboSourceType == 'Fixed' || $scope.cboSourceType == 'Moving' || $scope.cboSourceType == 'Property' || $scope.cboSourceType == 'Deposit') && ($scope.txtcolateralobservation_summary == '' || $scope.txtcolateralobservation_summary == null)) {
                Notify.alert('Kindly Fill Observation Summary Detail', 'warning')
            }
            else if ($scope.txtloanfaility_amount == '0') {
                Notify.alert('Loan Facility Amount should not be 0', 'warning')
            }
            else {
                if ($scope.txtfacilityreqon_date == null || $scope.txtfacilityreqon_date == '' ||
                    $scope.cboProductTypelist == null || $scope.cboProductTypelist == '' || $scope.cboProductSubTypelist == null ||
                    $scope.cboProductSubTypelist == '' || $scope.cboLoanTypelist == null || $scope.cboLoanTypelist == '' ||
                    $scope.txtloanfaility_amount == null || $scope.txtloanfaility_amount == '' || $scope.txtrate_interest == null ||
                    $scope.txtrate_interest == '' || $scope.txtpanel_interest == null || $scope.txtpanel_interest == '' || $scope.txttenure_year == null ||
                    $scope.txttenure_year == '' || $scope.txttenure_month == null || $scope.txttenure_month == '' || $scope.cboFacilityTypelist == null ||
                    $scope.cboFacilityTypelist == '' || $scope.txttenure_days == null || $scope.txttenure_days == '' || $scope.cboFacilitymodelist == null ||
                    $scope.cboFacilitymodelist == '' || $scope.txt_margin == null || $scope.txt_margin == '' || $scope.rdbinterest_status == '' || $scope.rdbinterest_status == null) {
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
                            var lsprogram_gid = '';
                            var lsprogram = '';
                            var lsproduct_name = '';
                            var lsproduct_gid = '';
                            var lsvariety_name = '';
                            var lsvariety_gid = '';


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
                            if ($scope.cboProgram != undefined || $scope.cboProgram != null) {
                              /*  lsprogram = $scope.cboProgram.program;*/
                                lsprogram_gid = $scope.cboProgram;
                                var lsprogram = $('#program :selected').text();
                            }
                            if ($scope.cboproduct_name != undefined || $scope.cboproduct_name != null) {
                                lsproduct_name = $scope.cboproduct_name.product_name;
                                lsproduct_gid = $scope.cboproduct_name.product_gid;
                            }
                            if ($scope.cbovariety_name != undefined || $scope.cbovariety_name != null) {
                                lsvariety_name = $scope.cbovariety_name.variety_name;
                                lsvariety_gid = $scope.cbovariety_name.variety_gid;
                            }
                            var params = {
                                product_type: lsloanproduct_name,
                                producttype_gid: lsloanproduct_gid,
                                facilityrequested_date: $scope.txtfacilityreqon_date,
                                productsub_type: lsloansubproduct_name,
                                productsubtype_gid: lsloansubproduct_gid,
                                loantype_gid: lsloantype_gid,
                                loan_type: lsloan_type,
                                facilityloan_amount: txtloanfaility_amount,
                                rate_interest: $scope.txtrate_interest,
                                margin: $scope.txt_margin,
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
                                program: lsprogram,
                                program_gid: lsprogram_gid,
                                //primaryvaluechain_list: $scope.cboPrimaryValueChain,
                                //secondaryvaluechain_list: $scope.cboSecondaryValueChain,
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
                                application_gid: $scope.application_gid,
                                enduse_purpose: $scope.txtenduse_purpose,
                                product_gid: lsproduct_gid,
                                product_name: lsproduct_name,
                                variety_gid: lsvariety_gid,
                                variety_name: lsvariety_name,
                                sector_name: $scope.txtsector_name,
                                category_name: $scope.txtcategory_name,
                                botanical_name: $scope.txtbotanical_name,
                                alternative_name: $scope.txtalternative_name
                            }
                            var url = 'api/MstApplicationAdd/PostLoanDtl';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();

                                if (resp.data.status == true) {
                                    activate();
                                    $scope.mstloan_list = resp.data.mstloan_list;
                                    for (var i = 0; i < $scope.mstloan_list.length; i++) {
                                        var lblloanfacility_amount = (parseInt($scope.mstloan_list[i].loanfacility_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                                        $scope.mstloan_list[i].loanfacility_amountinwords = defaultamountwordschange(lblloanfacility_amount);
                                        $scope.mstloan_list[i].lblloanfacility_amount = lblloanfacility_amount;
                                    }
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
                                    $scope.txt_margin = '';
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
                                    $scope.cboProgram = '';
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
                                    $scope.txtremaining = '';
                                    $scope.mstbuyer_list = '';
                                    $scope.cboproduct_name = '';
                                    $scope.cbovariety_name = '';
                                    $scope.txtsector_name = '';
                                    $scope.txtcategory_name = '';
                                    $scope.txtbotanical_name = '';
                                    $scope.txtalternative_name = '';
                                    $scope.mstproduct_list = '';
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
                        var lsprogram_gid = '';
                        var lsprogram = '';
                        var lsprogram_gid = '';
                        var lsprogram = '';
                        var lsproduct_name = '';
                        var lsproduct_gid = '';
                        var lsvariety_name = '';
                        var lsvariety_gid = '';

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
                        if ($scope.cboProgram != undefined || $scope.cboProgram != null) {
                            //lsprogram = $scope.cboProgram.program;
                            var lsprogram = $('#program :selected').text();
                            lsprogram_gid = $scope.cboProgram;
                        }
                        if ($scope.cboproduct_name != undefined || $scope.cboproduct_name != null) {
                            lsproduct_name = $scope.cboproduct_name.product_name;
                            lsproduct_gid = $scope.cboproduct_name.product_gid;
                        }
                        if ($scope.cbovariety_name != undefined || $scope.cbovariety_name != null) {
                            lsvariety_name = $scope.cbovariety_name.variety_name;
                            lsvariety_gid = $scope.cbovariety_name.variety_gid;
                        }
                        var params = {
                            product_type: lsloanproduct_name,
                            producttype_gid: lsloanproduct_gid,
                            facilityrequested_date: $scope.txtfacilityreqon_date,
                            productsub_type: lsloansubproduct_name,
                            productsubtype_gid: lsloansubproduct_gid,
                            loantype_gid: lsloantype_gid,
                            loan_type: lsloan_type,
                            facilityloan_amount: txtloanfaility_amount,
                            rate_interest: $scope.txtrate_interest,
                            margin: $scope.txt_margin,
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
                            program: lsprogram,
                            program_gid: lsprogram_gid,
                            //primaryvaluechain_list: $scope.cboPrimaryValueChain,
                            //secondaryvaluechain_list: $scope.cboSecondaryValueChain,
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
                            application_gid: $scope.application_gid,
                            enduse_purpose: $scope.txtenduse_purpose,
                            product_gid: lsproduct_gid,
                            product_name: lsproduct_name,
                            variety_gid: lsvariety_gid,
                            variety_name: lsvariety_name,
                            sector_name: $scope.txtsector_name,
                            category_name: $scope.txtcategory_name,
                            botanical_name: $scope.txtbotanical_name,
                            alternative_name: $scope.txtalternative_name
                        }
                        var url = 'api/MstApplicationAdd/PostLoanDtl';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                activate();
                                $scope.mstloan_list = resp.data.mstloan_list;
                                for (var i = 0; i < $scope.mstloan_list.length; i++) {
                                    var lblloanfacility_amount = (parseInt($scope.mstloan_list[i].loanfacility_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                                    $scope.mstloan_list[i].loanfacility_amountinwords = defaultamountwordschange(lblloanfacility_amount);
                                    $scope.mstloan_list[i].lblloanfacility_amount = lblloanfacility_amount;
                                }
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
                                $scope.txt_margin = '';
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
                                $scope.cboProgram = '';
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
                                $scope.txtremaining = '';
                                $scope.mstbuyer_list = '';
                                $scope.cboproduct_name = '';
                                $scope.cbovariety_name = '';
                                $scope.txtsector_name = '';
                                $scope.txtcategory_name = '';
                                $scope.txtbotanical_name = '';
                                $scope.txtalternative_name = '';
                                $scope.mstproduct_list = '';
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $scope.mstbuyer_list = '';
                            }
                        });
                    }
                }
            }
        }
        $scope.Deleteloan = function (application2loan_gid, producttype_gid)
        {
            var params =
              {
                  application2loan_gid: application2loan_gid,
                  producttype_gid: producttype_gid,
                  application_gid: $scope.application_gid
              }
            var url = 'api/MstApplicationAdd/DeleteLoan';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.mstloan_list = resp.data.mstloan_list
                    for (var i = 0; i < $scope.mstloan_list.length; i++) {
                        var lblloanfacility_amount = (parseInt($scope.mstloan_list[i].loanfacility_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.mstloan_list[i].loanfacility_amountinwords = defaultamountwordschange(lblloanfacility_amount);
                        $scope.mstloan_list[i].lblloanfacility_amount = lblloanfacility_amount;
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
      
        $scope.buyer=function()
        {
            var params = {
                buyer_gid: $scope.cboBuyer.buyer_gid,
            }
             var url = 'api/MstApplicationAdd/GetBuyerInfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtbuyer_limit = resp.data.buyer_limit;
                 });
        }
        $scope.add_buyer=function()
        {
            if ($scope.cboBuyer == null || $scope.cboBuyer == '' || $scope.txtbill_tenure == null || $scope.txtbill_tenure == '' || $scope.txtmargin == null || $scope.txtmargin == '')
            {
                Notify.alert('Kindly fill manadatory values','warning')
            }
            else{      
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
                var frm = new FormData();

                for (var i = 0; i < val.length; i++) {
                    var item = {
                        name: val[i].name,
                        file: val[i]
                    };
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        return false;
                    }
                }

                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cboDocumentTitle);
                frm.append('project_flag', "Default");
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
        $scope.uploaddocumentcancel = function (val, data) 
        {
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
        $scope.add_Collateral=function()
        {
            if ($scope.cboSourceType == null || $scope.cboSourceType == '')
            {
                Notify.alert('Kindly select source type','warning')
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
                var frm = new FormData();

                for (var i = 0; i < val.length; i++) {
                    var item = {
                        name: val[i].name,
                        file: val[i]
                    };
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        return false;
                    }
                }
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cbohypodoc_title);
                frm.append('project_flag', "Default");
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

        $scope.documentviewer = function (val1, val2, val3) {
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

            if (val3 == 'N') {
                DownloaddocumentService.DocumentViewer(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDocumentViewer(val1, val2, val3);
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
        $scope.SubmitHypothecation=function()
        {
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
                     application_gid:$location.search().lsapplication_gid
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
                            $location.url('app/MstApplicationGeneralAdd?lstab=' + lstab);
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
        $scope.saveProduct_charges=function()
        {
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
                acctinsurance_collectiontype: $scope.rdbpersonalaccident_collectiontype,
                acct_insurance: $scope.txtacct_insurance,
                total_collect: $scope.txttotal_collect,
                total_deduct: $scope.txttotal_deduct,
                application_gid:$scope.application_gid
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
                acctinsurance_collectiontype: $scope.rdbpersonalaccident_collectiontype,
                acct_insurance: $scope.txtacct_insurance,
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
                acctinsurance_collectiontype: $scope.rdbpersonalaccident_collectiontype,
                acct_insurance: lsacct_insurance,
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
                    $scope.rdbpersonalaccident_collectiontype = '';
                    $scope.txtacct_insurance = '';
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
             if ((txtloanfaility_amount) > (lsoveralllimit_amount - lsloanfacility_amount)) 
            {
                $scope.amount_validation = false;
             }
            else {
                $scope.amount_validation = true;
             }
             var lsamount = (lsoveralllimit_amount - lsloanfacility_amount);
             $scope.txtremaining = (lsamount - txtloanfaility_amount);
             }
        $scope.txtloanfaility_amount = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN')
    }
  

    $scope.Overalllimit_amountchange = function () {
        var input = document.getElementById('Overalllimitamount').value;
        var str1 = input.replace(/,/g, '');
        var str = Math.round (str1);
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
            var txtOveralllimit_amount = parseInt($scope.txtOveralllimit_amount.replace(/[\s,]+/g, '').trim());
        }
        $scope.txtOveralllimit_amount = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN')
    }
   
    $scope.calculatelimit=function()
    {
        if (($scope.txtvalidityoveralllimit_year == undefined || $scope.txtvalidityoveralllimit_year == null) || $scope.txtvalidityoveralllimit_year == '0') {
            var lsyear = '';
        }
        else {
            var lsyear = $scope.txtvalidityoveralllimit_year + " - Year, ";
        }
        if (($scope.txtvalidityoveralllimit_month == undefined || $scope.txtvalidityoveralllimit_month == null) || $scope.txtvalidityoveralllimit_month == '0') {
            var lsmonth = '';
        }
        else {
            var lsmonth = $scope.txtvalidityoveralllimit_month + " - Month, ";
        }
        if (($scope.txtvalidityoveralllimit_day == undefined || $scope.txtvalidityoveralllimit_day == null) || $scope.txtvalidityoveralllimit_day == '0') {
            var lsday = '';
        }
        else {
            var lsday = $scope.txtvalidityoveralllimit_day + " - Day ";
        }
        $scope.txtcalculationoveralllimit_validity = lsyear + lsmonth + lsday;        
    }

    $scope.calculatefacility = function () {
        if (($scope.txtvalidity_year == undefined || $scope.txtvalidity_year == null) || $scope.txtvalidity_year == '0') {
            var lsyear = '';
        }
        else {
            var lsyear = $scope.txtvalidity_year + " - Year, ";
        }
        if (($scope.txtvalidity_month == undefined || $scope.txtvalidity_month == null) || $scope.txtvalidity_month == '0') {
            var lsmonth = '';
        }
        else {
            var lsmonth = $scope.txtvalidity_month + " - Month, ";
        }
        if (($scope.txtvalidity_days == undefined || $scope.txtvalidity_days == null) || $scope.txtvalidity_days == '0') {
            var lsday = '';
        }
        else {
            var lsday = $scope.txtvalidity_days + " - Day ";
        }
        $scope.txtoverallfacilityvalidity_limit = lsyear + lsmonth + lsday;
    }
    $scope.calculatetenure = function () {
        if (($scope.txttenure_year == undefined || $scope.txttenure_year == null) || $scope.txttenure_year == '0') {
            var lsyear = '';
        }
        else {
            var lsyear = $scope.txttenure_year + " - Year, ";
        }
        if (($scope.txttenure_month == undefined || $scope.txttenure_month == null) || $scope.txttenure_month == '0') {
            var lsmonth = '';
        }
        else {
            var lsmonth = $scope.txttenure_month + " - Month, ";
        }
        if (($scope.txttenure_days == undefined || $scope.txttenure_days == null) || $scope.txttenure_days == '0') {
            var lsday = '';
        }
        else {
            var lsday = $scope.txttenure_days + " - Day ";
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
            var txtguidelinevalue = parseInt($scope.txtguidelinevalue.replace(/[\s,]+/g, '').trim());
        }
        $scope.txtguidelinevalue = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
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
            var txtmarketValue = parseInt($scope.txtmarketValue.replace(/[\s,]+/g, '').trim());
        }
        $scope.txtmarketValue = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
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
            var txtforcedsource_value = parseInt($scope.txtforcedsource_value.replace(/[\s,]+/g, '').trim());
        }
        $scope.txtforcedsource_value = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
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
            var txtcollateralSSV_value = parseInt($scope.txtcollateralSSV_value.replace(/[\s,]+/g, '').trim());
        }
        $scope.txtcollateralSSV_value = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
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
            var txtSecurity_Value = parseInt($scope.txtSecurity_Value.replace(/[\s,]+/g, '').trim());
        }
        $scope.txtSecurity_Value = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
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
    //$scope.downloads = function (val1, val2) {
    //    DownloaddocumentService.Downloaddocument(val1, val2);
    //}


        $scope.downloads = function (val1, val2, val3) {
            if (val3 == 'N') {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
            }
        }

    $scope.general_Tab = function () {
        var application_gid = $scope.application_gid;
        var application_status = $scope.application_status;
        if ($scope.application_status=='Completed')
        {
            $location.url('app/MstApplicationGeneralAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
        }
        else {
            $scope.Company_dtls=true;
            }
        }

    $scope.company_add = function () {
        var application_gid = $scope.application_gid;
        var application_status = $scope.application_status;
        if ($scope.application_status=='Completed')
        {
            $location.url('app/MstApplicationInstitutionAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
        }
        else {
            $scope.Company_dtls=true;
            }
        }

    $scope.Individual_add = function () {
        var application_gid = $scope.application_gid;
        var application_status = $scope.application_status;
        if ($scope.application_status=='Completed')
            {
                $location.url('app/MstApplicationIndividualAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
            }
            else {
                $scope.Individual_dtls=true;
                }
            }

    $scope.Group_add = function () {
        var application_gid = $scope.application_gid;
        var application_status = $scope.application_status;
            if ($scope.application_status=='Completed')
            {
                $location.url('app/MstApplicationGroupAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
            }
            else {
                $scope.Group_dtls=true;
            }
        }

    $scope.EconomicCapital_add = function () {
        var application_gid = $scope.application_gid;
        var application_status = $scope.application_status;
        if ($scope.application_status=='Completed')
            {
                $location.url('app/MstApplicationSocialTradeCapitalAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
            }
            else {
                $scope.EconomicCapital_dtls=true;
            }
        }

    $scope.OverallLimit_add = function () {
        var application_gid = $scope.application_gid;
        var applicant_type = $scope.applicant_type;
        if ($scope.applicant_type == null || $scope.applicant_type == '' )
            {
                $scope.OverallLimit_dtls=true;
            }
            else {                    
                $location.url('app/MstApplicationOverallLimitAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
            }
        }

    $scope.ProductCharges_add = function () {
        var application_gid = $scope.application_gid;
        var applicant_type = $scope.applicant_type;
        if ($scope.applicant_type == null || $scope.applicant_type == '' )
            {
                $scope.ProductCharges_dtls=true;
            }
            else {                    
                $location.url('app/MstApplicationProductChargesAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
            }
        }

    $scope.ServiceCharges_add = function () {
        var application_gid = $scope.application_gid;
        var applicant_type = $scope.applicant_type;
        if ($scope.applicant_type == null || $scope.applicant_type == '' )
            {
                $scope.ServiceCharges_dtls=true;
            }
            else {                    
                $location.url('app/MstApplicationServiceChargeAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
            }
        }

    $scope.Hypothecation_add = function () {
        var application_gid = $scope.application_gid;
        var applicant_type = $scope.applicant_type;
        if ($scope.applicant_type == null || $scope.applicant_type == '' )
            {
                $scope.Hypothecation_dtls=true;
            }
            else {                    
                $location.url('app/MstApplicationHypothecationAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
            }
        }

    $scope.BureauUpdates_add = function () {
        var application_gid = $scope.application_gid;
        var application_status = $scope.application_status;
        if ($scope.application_status=='Completed')
            {
                $location.url('app/MstApplicationCICUploadAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
            }
        else {
             $scope.BureauUpdates_dtls=true;
            }
        }

    $scope.doneclick = function () {
        lockUI();
        var url = 'api/MstApplicationAdd/GetGeneralInfo';
        SocketService.get(url).then(function (resp) {
            unlockUI();
            $scope.application_status = resp.data.application_status;
        });
        lockUI();
            var url = 'api/MstApplicationAdd/GetProceed';
            SocketService.get(url).then(function (resp) {
                $scope.proceed_flag = resp.data.proceed_flag;
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.cluster_head;
                $scope.zonalhead = resp.data.zonal_head;
                $scope.regionhead = resp.data.regional_head;
                $scope.businesshead = resp.data.business_head;
                unlockUI();
            });
            var proceed_flag = $scope.proceed_flag;
            var application_gid = $scope.application_gid;
            var params = {
                application_gid: application_gid
            }

            var url = 'api/MstApplicationAdd/GetApprovalHierarchyFlag';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lshierarchychange_flag = resp.data.lshierarchychange_flag;
                $scope.hierarchyupdated_flag = resp.data.hierarchyupdated_flag;
                if ($scope.hierarchyupdated_flag == 'N' && proceed_flag == 'Y') {
                    $scope.hierarchyshow = true;
                    $scope.done_disable = true;
                    $scope.done_enable = false;
                }
                else if ($scope.hierarchyupdated_flag == 'Y' && proceed_flag == 'Y') {
                    $scope.done_enable = true;
                    $scope.hierarchyshow = false;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'N') {
                    $scope.done_disable = true;
                    $scope.hierarchyshow = false;
                    $scope.done_enable = false;
                }
                else {

                }
            });
    }

    $scope.Back = function () {
        $state.go('app.MstApplicationCreationSummary');
    }
    $scope.overallsubmit_application = function () {

        var params = {

        }
        var url = 'api/MstApplicationAdd/PostAppProceed';
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
                    status: 'warning',
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

    $scope.productdtl_add = function () {
        if (($scope.cboproduct_name == undefined) || ($scope.cboproduct_name == '') || ($scope.cboproduct_name == null) ||
            ($scope.cbovariety_name == undefined) || ($scope.cbovariety_name == undefined) || ($scope.cbovariety_name == '') ||
            ($scope.txtcsacommodity_average == undefined) || ($scope.txtcsacommodity_average == undefined) || ($scope.txtcsacommodity_average == '') ||
            ($scope.txtcsapercentage_limit == undefined) || ($scope.txtcsapercentage_limit == undefined) || ($scope.txtcsapercentage_limit == '')) {
            Notify.alert('Kindly Enter All Mandatory Fields', 'warning');
        }
        else {
            var lsproduct_gid = '';
            var lsproduct_name = '';
            if ($scope.cboproduct_name != undefined || $scope.cboproduct_name != null) {
                lsproduct_gid = $scope.cboproduct_name.product_gid;
                lsproduct_name = $scope.cboproduct_name.product_name;
            }

            var lsvariety_gid = '';
            var lsvariety_name = '';
            if ($scope.cbovariety_name != undefined || $scope.cbovariety_name != null) {
                lsvariety_gid = $scope.cbovariety_name.variety_gid;
                lsvariety_name = $scope.cbovariety_name.variety_name;
            }

            var params = {
                product_gid: lsproduct_gid,
                product_name: lsproduct_name,
                variety_gid: lsvariety_gid,
                variety_name: lsvariety_name,
                sector_name: $scope.txtsector_name,
                category_name: $scope.txtcategory_name,
                botanical_name: $scope.txtbotanical_name,
                alternative_name: $scope.txtalternative_name,
                application_gid: $scope.application_gid,
                csacommodity_average: $scope.txtcsacommodity_average,
                csapercentageoftotal_limit: $scope.txtcsapercentage_limit
            }
            var url = 'api/MstApplicationAdd/PostProduct';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.mstproduct_list = resp.data.mstproduct_list;
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
                $scope.cboproduct_name = '';
                $scope.cbovariety_name = '';
                $scope.txtsector_name = '';
                $scope.txtcategory_name = '';
                $scope.txtbotanical_name = '';
                $scope.txtalternative_name = '';
                $scope.txtcsacommodity_average = '';
                $scope.txtcsapercentage_limit = '';
            });
        }
    }

    $scope.product_delete = function (application2product_gid) {
        var params =
            {
                application2product_gid: application2product_gid
            }
        var url = 'api/MstApplicationAdd/DeleteProductDetail';
        lockUI();
        SocketService.getparams(url, params).then(function (resp) {
            unlockUI();
            if (resp.data.status == true) {
                $scope.mstproduct_list = resp.data.mstproduct_list;
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

        $scope.downloadall = function () {

            for (var i = 0; i < $scope.DocumentList.length; i++) {
                if ($scope.DocumentList[i].migration_flag == 'N') {
                    //DownloaddocumentService.Downloaddocument(val1, val2);
                    DownloaddocumentService.Downloaddocument($scope.DocumentList[i].document_path, $scope.DocumentList[i].document_name);
                }
                else {
                    //DownloaddocumentService.OtherDownloaddocument(val1, val2);
                    DownloaddocumentService.OtherDownloaddocument($scope.DocumentList[i].document_path, $scope.DocumentList[i].document_name, $scope.DocumentList[i].migration_flag);
                }
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

    $scope.downloadallhypo = function () {
        for (var i = 0; i < $scope.hypo_documentList.length; i++) {
            DownloaddocumentService.Downloaddocument($scope.hypo_documentList[i].document_path, $scope.hypo_documentList[i].document_name);
        }
    }

    }
})();

