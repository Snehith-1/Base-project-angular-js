(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprApplicationLoanEditController', AgrMstSuprApplicationLoanEditController);

    AgrMstSuprApplicationLoanEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstSuprApplicationLoanEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprApplicationLoanEditController';

        $scope.application2loan_gid = $location.search().lsapplication2loan_gid;
        $scope.application_gid = $location.search().lsapplication_gid;
        $scope.product_gid = $location.search().product_gid;
        $scope.variety_gid = $location.search().variety_gid;
        var lstab = $location.search().lstab;

        activate();
        function activate() {
            $scope.overlimit_warning = true;
            $scope.showmilestonepaymenttype = true;

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };

             // Calender Popup... //
             $scope.amount_validation = true;
             vm.calender2 = function ($event) {
                 $event.preventDefault();
                 $event.stopPropagation();
 
                 vm.open2 = true;
             };

              // Calender Popup... //
            $scope.amount_validation = true;
            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open3 = true;
            };

            // Calender Popup... //
            $scope.amount_validation = true;
            vm.calender8 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open8 = true;
            };

            // Calender Popup... //

            vm.calender11 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open11 = true;
            };
            // Calender Popup... //

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

            vm.calender17 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open17 = true;
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
            lockUI();
            $scope.cboPrimaryValueChain = [];
            $scope.cboSecondaryValueChain = [];

            var url = 'api/AgrMstSuprApplicationEdit/GetProductChargesTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/AgrMstUom/GetuomList';
            SocketService.get(url).then(function (resp) {
                $scope.uom_list = resp.data.Uomdtl;
            });
            var url = 'api/AgrMstSuprApplicationAdd/Getmilestonepaymentdtl';
            SocketService.get(url).then(function (resp) {
                $scope.milestonepaymentlist = resp.data.milestonepayment;
            });


            var url = 'api/AgrMstSuprApplicationAdd/GetproductDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.loanproductlist = resp.data.loanproductlist;
                $scope.loantypelist = resp.data.loantypelist;
                $scope.principalfrequencylist = resp.data.principalfrequencylist;
                $scope.interestfrequencylist = resp.data.interestfrequencylist;
                $scope.buyerlist = resp.data.buyerlist;
                $scope.programlist = resp.data.programlist;
            });

            var url = 'api/AgrMstSuprApplicationAdd/GetDropDown';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.productname_list = resp.data.productname_list;
            });

            $scope.loantype_change = function (cboLoanTypelist) {
                if ($scope.cboLoanTypelist.loan_type == 'Secured') {
                    $scope.loantype = true;

                }
                else {

                    $scope.loantype = false;
                }
            }

            var url = 'api/AgrMstSuprApplicationAdd/GetNatureFormStateofCommodity';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.natureformstateofcommoditydtl = resp.data.NatureFormStateofCommodity;
                unlockUI();
            });

            var params = {
                product_gid: $scope.product_gid
            }
            var url = 'api/AgrMstSuprApplicationAdd/GetSectorcategory';
            unlockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.varietyname_list = resp.data.varietyname_list;
            });
            
            var param = {
                application_gid: $scope.application_gid,
                application2loan_gid: $scope.application2loan_gid,
            }
            var url = 'api/AgrMstSuprApplicationEdit/GetEditLoanLimit';
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                $scope.lbloveralllimit_amount = resp.data.overalllimit_amount;
                $scope.lsloanfacility_amount = resp.data.loanfacility_amount;
                if (resp.data.loanfacility_amount == '' || resp.data.loanfacility_amount == null) {
                    $scope.lsloanfacility_amount = '0';
                }
            });
           
            var url = 'api/AgrMstSuprApplicationAdd/Getproduct';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.product_list = resp.data.product_list;
            });
            var params = {
                application2loan_gid: $scope.application2loan_gid
            }
            var url = 'api/AgrMstSuprApplicationEdit/LoanDetailsEdit';
            SocketService.getparams(url, params).then(function (resp) {

                $scope.txteditfacilityreqon_date = resp.data.facilityrequested_date;
                $scope.cboProductTypelist = resp.data.producttype_gid;
                $scope.product_type = resp.data.product_type;
                $scope.lblproduct_type = resp.data.product_type;
                $scope.lblproductsub_type = resp.data.productsub_type;
                var params = {
                    loanproduct_gid: resp.data.producttype_gid,
                    application_gid: '',
                    application2loan_gid: ''
                }
                var url = 'api/AgrMstSuprApplicationAdd/GetLoanSubProduct';
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
                if($scope.txtguidelinevalue!=null && $scope.txtguidelinevalue!=undefined && $scope.txtguidelinevalue!="")
                {
                    $scope.txtOveralllimit_amount_edit = (parseInt($scope.txtguidelinevalue.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtOveralllimit_amount_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount2').innerHTML = $scope.lblamountwords;
                }
                
                $scope.txtguideline_date = resp.data.guideline_date;
                $scope.txtmarketvalue_date = resp.data.marketvalue_date;
                
                $scope.txtmarketValue = resp.data.market_value;
                if($scope.txtmarketValue!=null && $scope.txtmarketValue!=undefined && $scope.txtmarketValue!="")
                {
                    $scope.txtOveralllimit_amount_edit = (parseInt($scope.txtmarketValue.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtOveralllimit_amount_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount3').innerHTML = $scope.lblamountwords;
                }
                
                $scope.txtforcedsource_value = resp.data.forcedsource_value;
                if($scope.txtforcedsource_value!=null && $scope.txtforcedsource_value!=undefined && $scope.txtforcedsource_value!="")
                {
                    $scope.txtOveralllimit_amount_edit = (parseInt($scope.txtforcedsource_value.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtOveralllimit_amount_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount4').innerHTML = $scope.lblamountwords;
                }
                
                
                $scope.txtcollateralSSV_value = resp.data.collateralSSV_value;
                if($scope.txtcollateralSSV_value!=null && $scope.txtcollateralSSV_value!=undefined && $scope.txtcollateralSSV_value!="")
                {
                    $scope.txtOveralllimit_amount_edit = (parseInt($scope.txtcollateralSSV_value.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtOveralllimit_amount_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount5').innerHTML = $scope.lblamountwords;
                }
                
                $scope.txtforcedvalueassessed_on = resp.data.forcedvalueassessed_on;
                $scope.txtcolateralobservation_summary = resp.data.collateralobservation_summary;
                
                $scope.txtloanfaility_amount = resp.data.facilityloan_amount;
                if($scope.txtloanfaility_amount!=null && $scope.txtloanfaility_amount!=undefined && $scope.txtloanfaility_amount!="")
                {
                    $scope.txtOveralllimit_amount_edit = (parseInt($scope.txtloanfaility_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtOveralllimit_amount_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount1').innerHTML = $scope.lblamountwords;
                }
                
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
                //$scope.loanprimaryvaluechain_list = resp.data.primaryvaluechain_list;

                //if (resp.data.primaryvaluechain_list != null) {
                //    var count = resp.data.primaryvaluechain_list.length;
                //    for (var i = 0; i < count; i++) {
                //        var indexs = $scope.valuechainlist.findIndex(x => x.valuechain_gid === resp.data.primaryvaluechain_list[i].valuechain_gid);
                //        $scope.cboPrimaryValueChain.push($scope.valuechainlist[indexs]);
                //        $scope.$parent.cboPrimaryValueChain = $scope.cboPrimaryValueChain;
                //    }
                //}

                //$scope.loansecondaryvaluechain_list = resp.data.secondaryvaluechain_list;

                //if (resp.data.secondaryvaluechain_list != null) {
                //    var count = resp.data.secondaryvaluechain_list.length;
                //    for (var i = 0; i < count; i++) {
                //        var indexs = $scope.valuechainlist.findIndex(x => x.valuechain_gid === resp.data.secondaryvaluechain_list[i].valuechain_gid);
                //        $scope.cboSecondaryValueChain.push($scope.valuechainlist[indexs]);
                //        $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
                //    }
                //}

                $scope.rdbinterest_status = resp.data.interest_status;
                $scope.rdbmoratorium_status = resp.data.moratorium_status;
                $scope.cbomoratorium_type = resp.data.moratorium_type;
                $scope.txtmoratorium_startdate = resp.data.moratorium_startdate;
                $scope.txtmoratorium_enddate = resp.data.moratorium_enddate;
                $scope.txtenduse_purpose = resp.data.enduse_purpose;
              

                if(resp.data.product_type=='Agri Receivable Finance (ARF)') 
                {
                    $scope.ARF_condition = true;
                }
                else {
                    $scope.ARF_condition = false;
                }
            });

            var url = 'api/AgrMstSuprApplicationEdit/BuyerDetailsList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstbuyer_list = resp.data.mstbuyer_list;
            });
            
            var url = 'api/AgrMstSuprApplicationEdit/CollateralDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.DocumentList = resp.data.DocumentList;
            });

            var params = {
                application2loan_gid: $scope.application2loan_gid,
                application_gid: $scope.application_gid
            }

            var url = 'api/AgrMstSuprApplicationView/GetLoanProgramValueChain';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mstproductdtl_list = resp.data.mstproductdtl_list;
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
            var url = 'api/AgrMstSuprApplicationAdd/GetSectorcategory';
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
            var url = 'api/AgrMstSuprApplicationAdd/GetVarietyDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.product_gid = resp.data.product_gid;
                $scope.variety_gid = resp.data.variety_gid;
                $scope.variety_name = resp.data.variety_name;
                $scope.txtbotanical_name = resp.data.botanical_name;
                $scope.txtalternative_name = resp.data.alternative_name;
                $scope.txthsn_code = resp.data.hsn_code;
            });
            $scope.txtbotanical_name = '';
            $scope.txtalternative_name = '';
        }
        $scope.OverallLimit_add = function () {
            $scope.limit_show = true;
        }
        $scope.producttype = function () {
            var params = {
                loanproduct_gid: $scope.cboProductTypelist, application_gid: '',
                application2loan_gid: ''
            }
            var url = 'api/AgrMstSuprApplicationAdd/GetLoanSubProduct';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.loansubproductlist = resp.data.application_list;
            });
            var loanproduct_name = $('#loanproduct_name :selected').text();
            if(loanproduct_name=='Agri Receivable Finance (ARF)')
            {
                $scope.ARF_condition = true;
            }
            else {
                $scope.ARF_condition = false;
            }

            if(loanproduct_name !='Agri Receivable Finance (ARF)')
            {
                $scope.cboSourceType = '';
                $scope.txtguidelinevalue = '';
                $scope.txtguideline_date = '';
                $scope.txtmarketValue = '';
                $scope.txtmarketvalue_date = '';
                $scope.txtforcedsource_value = '';
                $scope.txtcollateralSSV_value = '';
                $scope.txtforcedvalueassessed_on = '';
                $scope.txtcolateralobservation_summary = '';
                $scope.DocumentList = '';
                $scope.mstbuyer_list = '';
            }
        }

        $scope.loantypechange = function () {
            var loan_type = $('#loan_type :selected').text();
            if (loan_type == 'Secured') {
                $scope.Collateralshow = true;
            }
            else {
                $scope.Collateralshow = false;
            }            
        }

        $scope.updateLoan = function () {
            $scope.txtnet_yield = "0";
            var txtloanfaility_amount = parseInt($scope.txtloanfaility_amount.replace(/[\s,]+/g, '').trim());
            var lsoveralllimit_amount = parseInt($scope.overalllimit_amount);
            var lsloanfacility_amount = parseInt($scope.lsloanfacility_amount);
            var loanproduct_name = $('#loanproduct_name :selected').text();

            if ((txtloanfaility_amount) > (lsoveralllimit_amount - lsloanfacility_amount)) {
                Notify.alert('Amount Exceeded from Overall Limit', 'warning');
            }
            else if (($scope.cboSourceType == 'Fixed' || $scope.cboSourceType == 'Moving' || $scope.cboSourceType == 'Property' || $scope.cboSourceType == 'Deposit') && ($scope.txtcolateralobservation_summary == '' || $scope.txtcolateralobservation_summary == null)) {
                Notify.alert('Kindly Fill Observation Summary Detail', 'warning')
            }
            else if ($scope.mstproductdtl_list == null || $scope.mstproductdtl_list == undefined || $scope.mstproductdtl_list == "") {
                Notify.alert('Atleast One Record should be added in Product Details', 'warning')
                return false
            }
            else if ($scope.txtloanfaility_amount == '0') {
                Notify.alert('Proposed Program Limit should not be 0', 'warning')
            }
            //    else if ($scope.DocumentList == null && $scope.Collateralshow == true) {
            //    Notify.alert("Kindly upload the document", {
            //        status: 'warning',
            //        pos: 'top-center',
            //        timeout: 3000
            //    });
            //}
            else {
                if ($scope.txteditfacilityreqon_date == null || $scope.txteditfacilityreqon_date == '' || $scope.cboProductTypelist == null || $scope.cboProductTypelist == '' || $scope.cboProductSubTypelist == null || $scope.cboProductSubTypelist == '' || 
                $scope.cboLoanTypelist == null || $scope.cboLoanTypelist == '' || $scope.txtloanfaility_amount == null || $scope.txtloanfaility_amount == '' || $scope.txteditrate_interest == null || $scope.txteditrate_interest == '' || $scope.txteditpanel_interest == null ||
                 $scope.txteditpanel_interest == '' || $scope.cboFacilityTypelist == null || 
                 $scope.cboFacilityTypelist == '' || $scope.txtedittenure_days == null || $scope.txtedittenure_days == '' || $scope.cboFacilitymodelist == null || 
                 $scope.cboFacilitymodelist == '') {
                Notify.alert('Kindly fill all mandatory values', 'warning');
                }
                else {
                    if ($scope.rdbmoratorium_status == 'Yes') {
                        if ($scope.cbomoratorium_type == null || $scope.cbomoratorium_type == '' || $scope.txtmoratorium_startdate == null || $scope.txtmoratorium_startdate == '' || $scope.txtmoratorium_enddate == null || $scope.txtmoratorium_enddate == '') {
                            Notify.alert('Kindly fill Moratorium Details', 'warning');
                        }
                        else {
                            var lsloanproduct_gid = '';
                            var lsloansubproduct_gid = '';
                            var lsloantype_gid = '';
                           var lsprincipalfrequency_gid = '';
                           var lsinterestfrequency_gid = '';
                           var lsprogram_gid = '';
                           var lsproduct_gid = '';
                           var lsvariety_gid = '', lsmilestonepaymenttype_gid = '', lsmilestonepaymenttype_name = '';

                            //var lsloanproduct_name = $('#loanproduct_name :selected').text();
                            //var lsloansubproduct_name = $('#loansubproduct_name :selected').text();
                           var lsloanproduct_name = $scope.lblproduct_type;
                           var lsloansubproduct_name = $scope.lblproductsub_type;

                            var lsloan_type = $('#loan_type :selected').text();
                            var lsprincipalfrequency_name = $('#principalfrequency_name :selected').text();
                            var lsinterestfrequency_name = $('#interestfrequency_name :selected').text();
                            var lsprogram = $('#program :selected').text();
                            var Product_Name = $('#ProductName :selected').text();
                            var Variety_Name = $('#Variety :selected').text();
                            lsmilestonepaymenttype_name = $('#MilestoneName :selected').text();
                            if ($scope.cboProductTypelist != undefined || $scope.cboProductTypelist != null) {
                                lsloanproduct_gid = $scope.cboProductTypelist;
                            }
                            if ($scope.cboProductSubTypelist != undefined || $scope.cboProductSubTypelist != null) {
                               lsloansubproduct_gid = $scope.cboProductSubTypelist;
                            }
                            if ($scope.cboLoanTypelist != undefined || $scope.cboLoanTypelist != null) {
                                lsloantype_gid = $scope.cboLoanTypelist;
                               
                            }
                            if ($scope.cboprincipalfrequency != undefined || $scope.cboprincipalfrequency != null) {
                                lsprincipalfrequency_gid = $scope.cboprincipalfrequency;
                                }
                            if ($scope.cboInterestFrequency != undefined || $scope.cboInterestFrequency != null) {
                                lsinterestfrequency_gid = $scope.cboInterestFrequency;
                            }
                            if ($scope.cboProgram != undefined || $scope.cboProgram != null) {
                                lsprogram_gid = $scope.cboProgram;
                            }
                            if ($scope.cboproduct_name != undefined || $scope.cboproduct_name != null) {
                                lsproduct_gid = $scope.cboproduct_name;
                            }
                            if ($scope.cbovariety_name != undefined || $scope.cbovariety_name != null) {
                                lsvariety_gid = $scope.cbovariety_name;
                            }
                            if ($scope.cbomilestonepaymenttype != undefined || $scope.cbomilestonepaymenttype != null) {
                                lsmilestonepaymenttype_gid = $scope.cbomilestonepaymenttype;
                                 
                            }

                            var params = {
                                product_type: lsloanproduct_name,
                                producttype_gid: lsloanproduct_gid,
                                facilityrequested_date: $scope.txteditfacilityreqon_date,
                                productsub_type: lsloansubproduct_name,
                                productsubtype_gid: lsloansubproduct_gid,
                                loantype_gid: lsloantype_gid,
                                loan_type: lsloan_type,
                                facilityloan_amount: $scope.txtloanfaility_amount,
                                rate_interest: $scope.txteditrate_interest,
                                penal_interest: $scope.txteditpanel_interest,
                                facilityvalidity_year: $scope.txteditvalidity_years,
                                facilityvalidity_month: $scope.txteditvalidity_months,
                                facilityvalidity_days: $scope.txteditvalidity_days,
                                facilityoverall_limit: $scope.txtoverallfacilityvalidity_limit,
                                tenureproduct_year: $scope.txtedittenure_years,
                                tenureproduct_month: $scope.txtedittenure_months,
                                tenureproduct_days: $scope.txtedittenure_days,
                                tenureoverall_limit: $scope.txteditoveralllimit_validity,
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
                                application2loan_gid: $scope.application2loan_gid,
                                enduse_purpose: $scope.txtenduse_purpose,
                                product_gid: $scope.cboproduct_name,
                                product_name: Product_Name,
                                variety_gid: $scope.cbovariety_name,
                                variety_name: Variety_Name,
                                sector_name: $scope.txtsector_name,
                                category_name: $scope.txtcategory_name,
                                botanical_name: $scope.txtbotanical_name,
                                alternative_name: $scope.txtalternative_name,
                                milestone_applicability: $scope.rdbmilestone_applicablity,
                                insurance_applicability: $scope.rdbinsurance_applicability,
                                milestonepayment_gid: lsmilestonepaymenttype_gid,
                                milestonepayment_name: lsmilestonepaymenttype_name,
                                sa_payout: $scope.txtsapayout,
                                insurance_availability: $scope.lbloveralllimit_amount,
                                insurance_percent: $scope.txtinsurance_percent,
                                insurance_cost: $scope.txtinsurance_cost,
                                net_yield: $scope.txtnet_yield,
                            }
                            var url = 'api/AgrMstSuprApplicationEdit/LoanDetailsUpdate';
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
                                    if (lstab == 'add') {
                                        $location.url('app/AgrMstSuprApplicationProductChargesAdd?lstab=' + lstab);
                                    }
                                    else {
                                        $location.url('app/AgrMstSuprApplicationGeneralEdit');
                                    }
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
                                    $scope.mstbuyer_list = '';
                                    $scope.cboproduct_name = '';
                                    $scope.cbovariety_name = '';
                                    $scope.txtsector_name = '';
                                    $scope.txtcategory_name = '';
                                    $scope.txtbotanical_name = '';
                                    $scope.txtalternative_name = '';
                                    $scope.rdbmilestone_applicablity = '';
                                    $scope.rdbinsurance_applicability = '';
                                    $scope.cbomilestonepaymenttype = '';
                                    $scope.txtsapayout = '';
                                    $scope.txtinsurance_percent = '';
                                    $scope.txtinsurance_cost = '';
                                    $scope.txtnet_yield = '';
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

                        var lsloanproduct_gid = '';
                        var lsloansubproduct_gid = '';
                        var lsloantype_gid = '';
                        var lsprincipalfrequency_gid = '';
                        var lsinterestfrequency_gid = '';
                        var lsprogram_gid = '';
                        var lsproduct_gid = '';
                        var lsvariety_gid = '', lsmilestonepaymenttype_gid = '', lsmilestonepaymenttype_name = '';
                        var lsloanproduct_name = $scope.lblproduct_type;
                        var lsloansubproduct_name = $scope.lblproductsub_type;

                        //var lsloanproduct_name = $('#loanproduct_name :selected').text();
                        //var lsloansubproduct_name = $('#loansubproduct_name :selected').text();
                        var lsloan_type = $('#loan_type :selected').text();
                        var lsprincipalfrequency_name = $('#principalfrequency_name :selected').text();
                        var lsinterestfrequency_name = $('#interestfrequency_name :selected').text();
                        var lsprogram = $('#program :selected').text();
                        var Product_Name = $('#ProductName :selected').text();
                        var Variety_Name = $('#Variety :selected').text();
                        lsmilestonepaymenttype_name = $('#MilestoneName :selected').text();

                        if ($scope.cboProductTypelist != undefined || $scope.cboProductTypelist != null) {
                            lsloanproduct_gid = $scope.cboProductTypelist;
                        }
                        if ($scope.cboProductSubTypelist != undefined || $scope.cboProductSubTypelist != null) {
                            lsloansubproduct_gid = $scope.cboProductSubTypelist;
                        }
                        if ($scope.cboLoanTypelist != undefined || $scope.cboLoanTypelist != null) {
                            lsloantype_gid = $scope.cboLoanTypelist;

                        }
                        if ($scope.cboprincipalfrequency != undefined || $scope.cboprincipalfrequency != null) {
                            lsprincipalfrequency_gid = $scope.cboprincipalfrequency;
                        }
                        if ($scope.cboInterestFrequency != undefined || $scope.cboInterestFrequency != null) {
                            lsinterestfrequency_gid = $scope.cboInterestFrequency;
                        }
                        if ($scope.cboProgram != undefined || $scope.cboProgram != null) {
                            lsprogram_gid = $scope.cboProgram;
                        }
                        if ($scope.cboproduct_name != undefined || $scope.cboproduct_name != null) {
                            lsproduct_gid = $scope.cboproduct_name;
                        }
                        if ($scope.cbovariety_name != undefined || $scope.cbovariety_name != null) {
                            lsvariety_gid = $scope.cbovariety_name;
                        }
                        if ($scope.cbomilestonepaymenttype != undefined || $scope.cbomilestonepaymenttype != null) {
                            lsmilestonepaymenttype_gid = $scope.cbomilestonepaymenttype; 
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
                            rate_interest: $scope.txteditrate_interest,
                            penal_interest: $scope.txteditpanel_interest,
                            facilityvalidity_year: $scope.txteditvalidity_years,
                            facilityvalidity_month: $scope.txteditvalidity_months,
                            facilityvalidity_days: $scope.txteditvalidity_days,
                            facilityoverall_limit: $scope.txtoverallfacilityvalidity_limit,
                            tenureproduct_year: $scope.txtedittenure_years,
                            tenureproduct_month: $scope.txtedittenure_months,
                            tenureproduct_days: $scope.txtedittenure_days,
                            tenureoverall_limit: $scope.txteditoveralllimit_validity,
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
                            application2loan_gid: $scope.application2loan_gid,
                            enduse_purpose: $scope.txtenduse_purpose,
                            product_gid: $scope.cboproduct_name,
                            product_name: Product_Name,
                            variety_gid: $scope.cbovariety_name,
                            variety_name: Variety_Name,
                            sector_name: $scope.txtsector_name,
                            category_name: $scope.txtcategory_name,
                            botanical_name: $scope.txtbotanical_name,
                            alternative_name: $scope.txtalternative_name,
                            milestone_applicability: $scope.rdbmilestone_applicablity,
                            insurance_applicability: $scope.rdbinsurance_applicability,
                            milestonepayment_gid: lsmilestonepaymenttype_gid,
                            milestonepayment_name: lsmilestonepaymenttype_name,
                            sa_payout: $scope.txtsapayout,
                            insurance_availability: $scope.lbloveralllimit_amount,
                            insurance_percent: $scope.txtinsurance_percent,
                            insurance_cost: $scope.txtinsurance_cost,
                            net_yield: $scope.txtnet_yield,
                        }
                        var url = 'api/AgrMstSuprApplicationEdit/LoanDetailsUpdate';
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
                                if (lstab == 'add') {
                                    $location.url('app/AgrMstSuprApplicationProductChargesAdd?lstab=' + lstab);
                                }
                                else {
                                    $location.url('app/AgrMstSuprApplicationGeneralEdit');
                                }
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
                                $scope.mstbuyer_list = '';
                                $scope.cboproduct_name = '';
                                $scope.cbovariety_name = '';
                                $scope.txtsector_name = '';
                                $scope.txtcategory_name = '';
                                $scope.txtbotanical_name = '';
                                $scope.txtalternative_name = '';
                                $scope.rdbmilestone_applicablity = '';
                                $scope.rdbinsurance_applicability = '';
                                $scope.cbomilestonepaymenttype = '';
                                $scope.txtsapayout = '';
                                $scope.txtinsurance_percent = '';
                                $scope.txtinsurance_cost = '';
                                $scope.txtnet_yield = '';
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


        $scope.Changeinsuranceapplicability = function () {
            if ($scope.rdbmilestone_applicablity == "Yes") {
                $scope.showmilestonepaymenttype = true;
                $scope.disabledmilestonepaymenttype = false;
            }
            else {
                $scope.cbomilestonepaymenttype = "";
                $scope.showmilestonepaymenttype = false;
                $scope.disabledmilestonepaymenttype = true;
            }
        }



        //$scope.Changeinsuranceapplicability = function () {
        //    if ($scope.rdbinsurance_applicability == "Yes") {
        //        $scope.showmilestonepaymenttype = true;
        //        $scope.disabledmilestonepaymenttype = false;
        //    }
        //    else {
        //        $scope.cbomilestonepaymenttype = "";
        //        $scope.showmilestonepaymenttype = false;
        //        $scope.disabledmilestonepaymenttype = true;
        //    }
        //}
        ////$scope.add_loaddetails = function () {
        //    if ($scope.txteditfacilityreqon_date == null || $scope.txteditfacilityreqon_date == '' || $scope.cboProductTypelist == null || $scope.cboProductTypelist == '' || $scope.cboProductSubTypelist == null || $scope.cboProductSubTypelist == '' || $scope.cboLoanTypelist == null || $scope.cboLoanTypelist == '' || $scope.txtloanfaility_amount == null || $scope.txtloanfaility_amount == '' || $scope.txteditrate_interest == null || $scope.txteditrate_interest == '' || $scope.txteditpanel_interest == null || $scope.txteditpanel_interest == '' || $scope.txtedittenure_years == null || $scope.txtedittenure_years == '' || $scope.txtedittenure_months == null || $scope.txtedittenure_months == '' || $scope.cboFacilityTypelist == null || $scope.cboFacilityTypelist == '' || $scope.txtedittenure_days == null || $scope.txtedittenure_days == '' || $scope.cboFacilitymodelist == null || $scope.cboFacilitymodelist == '') {
        //        Notify.alert('Kindly Fill all mandatory values', 'warning');
        //    }
        //    else {
        //        if ($scope.rdbmoratorium_status == 'Yes') {
        //            if ($scope.cbomoratorium_type == null || $scope.cbomoratorium_type == '' || $scope.txtmoratorium_startdate == null || $scope.txtmoratorium_startdate == '' || $scope.txtmoratorium_enddate == null || $scope.txtmoratorium_enddate == '') {
        //                Notify.alert('Kindly fill Moratorium Details', 'warning');
        //            }
        //            else {
        //                var lsloanproduct_name = '';
        //                var lsloanproduct_gid = '';
        //                var lsloansubproduct_name = '';
        //                var lsloansubproduct_gid = '';
        //                var lsloantype_gid = '';
        //                var lsloan_type = '';
        //                var lsprincipalfrequency_gid = '';
        //                var lsprincipalfrequency_name = '';
        //                var lsinterestfrequency_name = '';
        //                var lsinterestfrequency_gid = '';

        //                if ($scope.lsloanproduct_gid != undefined || $scope.cboProductTypelist != null) {
        //                    lsloanproduct_name = $scope.cboProductTypelist.loanproduct_name;
        //                    lsloanproduct_gid = $scope.cboProductTypelist.loanproduct_gid;
        //                }
        //                if ($scope.cboProductSubTypelist != undefined || $scope.cboProductSubTypelist != null) {
        //                    lsloansubproduct_name = $scope.cboProductSubTypelist.loansubproduct_name;
        //                    lsloansubproduct_gid = $scope.cboProductSubTypelist.loansubproduct_gid;
        //                }
        //                if ($scope.cboLoanTypelist != undefined || $scope.cboLoanTypelist != null) {
        //                    lsloantype_gid = $scope.cboLoanTypelist.loantype_gid;
        //                    lsloan_type = $scope.cboLoanTypelist.loan_type;
        //                }
        //                if ($scope.cboprincipalfrequency != undefined || $scope.cboprincipalfrequency != null) {
        //                    lsprincipalfrequency_gid = $scope.cboprincipalfrequency.principalfrequency_gid;
        //                    lsprincipalfrequency_name = $scope.cboprincipalfrequency.principalfrequency_name;
        //                }
        //                if ($scope.cboInterestFrequency != undefined || $scope.cboInterestFrequency != null) {
        //                    lsinterestfrequency_name = $scope.cboInterestFrequency.interestfrequency_name;
        //                    lsinterestfrequency_gid = $scope.cboInterestFrequency.interestfrequency_gid;
        //                }
        //                var params = {
        //                    product_type: lsloanproduct_name,
        //                    producttype_gid: lsloanproduct_gid,
        //                    productsub_type: lsloansubproduct_name,
        //                    productsubtype_gid: lsloansubproduct_gid,
        //                    loantype_gid: lsloantype_gid,
        //                    loan_type: lsloan_type,
        //                    facilityloan_amount: $scope.txtloanfaility_amount,
        //                    facilityrequested_date: $scope.txteditfacilityreqon_date,
        //                    rate_interest: $scope.txteditrate_interest,
        //                    penal_interest: $scope.txteditpanel_interest,
        //                    facilityvalidity_year: $scope.txteditvalidity_years,
        //                    facilityvalidity_month: $scope.txteditvalidity_months,
        //                    facilityvalidity_days: $scope.txteditvalidity_days,
        //                    facilityoverall_limit: $scope.txtoverallfacilityvalidity_limit,
        //                    tenureproduct_year: $scope.txtedittenure_years,
        //                    tenureproduct_month: $scope.txtedittenure_months,
        //                    tenureproduct_days: $scope.txtedittenure_days,
        //                    tenureoverall_limit: $scope.txteditoveralllimit_validity,
        //                    facility_type: $scope.cboFacilityTypelist,
        //                    facility_mode: $scope.cboFacilitymodelist,
        //                    principalfrequency_name: lsprincipalfrequency_name,
        //                    principalfrequency_gid: lsprincipalfrequency_gid,
        //                    interestfrequency_name: lsinterestfrequency_name,
        //                    interestfrequency_gid: lsinterestfrequency_gid,
        //                    interest_status: $scope.rdbinterest_status,
        //                    moratorium_status: $scope.rdbmoratorium_status,
        //                    moratorium_type: $scope.cbomoratorium_type,
        //                    moratorium_startdate: $scope.txtmoratorium_startdate,
        //                    moratorium_enddate: $scope.txtmoratorium_enddate,
        //                }
        //                var url = 'api/AgrMstSuprApplicationEdit/PostEditLoanDtl';
        //                lockUI();
        //                SocketService.post(url, params).then(function (resp) {
        //                    unlockUI();

        //                    if (resp.data.status == true) {
        //                        var param = {
        //                            application_gid: $scope.application_gid
        //                        }
        //                        var url = 'api/AgrMstSuprApplicationEdit/LoanDetailList';
        //                        SocketService.getparams(url, param).then(function (resp) {
        //                            $scope.Loandtl_list = resp.data.mstloan_list;
        //                            $scope.collateral_status = resp.data.collateral_status;
        //                            $scope.buyer_status = resp.data.buyer_status;
        //                        });
        //                         Notify.alert(resp.data.message, {

        //                            status: 'success',
        //                            pos: 'top-center',
        //                            timeout: 3000
        //                        });
        //                        $scope.cboProductTypelist = '';
        //                        $scope.txteditfacilityreqon_date = '';
        //                        $scope.cboProductSubTypelist = '';
        //                        $scope.cboLoanTypelist = '';
        //                        $scope.txtloanfaility_amount = '';
        //                        $scope.txteditrate_interest = '';
        //                        $scope.txteditpanel_interest = '';
        //                        $scope.txteditvalidity_years = '';
        //                        $scope.txteditvalidity_months = '';
        //                        $scope.txteditvalidity_days = '';
        //                        $scope.txtoverallfacilityvalidity_limit = '';
        //                        $scope.txtedittenure_years = '';
        //                        $scope.txtedittenure_months = '';
        //                        $scope.txtedittenure_days = '';
        //                        $scope.txteditoveralllimit_validity = '';
        //                        $scope.cboFacilitymodelist = '';
        //                        document.getElementById('words_totalamount1').innerHTML = '';
        //                        $scope.cboprincipalfrequency = '';
        //                        $scope.cboInterestFrequency = '';
        //                        $scope.rdbinterest_status = '';
        //                        $scope.rdbmoratorium_status = '';
        //                        $scope.cbomoratorium_type = '';
        //                        $scope.txtmoratorium_startdate = '';
        //                        $scope.txtmoratorium_enddate = '';
        //                        $scope.cboFacilityTypelist = '';
        //                    }
        //                    else {
        //                        Notify.alert(resp.data.message, {
        //                            status: 'info',
        //                            pos: 'top-center',
        //                            timeout: 3000
        //                        });
        //                    }
        //                });
        //            }
        //        }
        //        else {
        //            var lsloanproduct_name = '';
        //            var lsloanproduct_gid = '';
        //            var lsloansubproduct_name = '';
        //            var lsloansubproduct_gid = '';
        //            var lsloantype_gid = '';
        //            var lsloan_type = '';
        //            var lsprincipalfrequency_gid = '';
        //            var lsprincipalfrequency_name = '';
        //            var lsinterestfrequency_name = '';
        //            var lsinterestfrequency_gid = '';

        //            if ($scope.lsloanproduct_gid != undefined || $scope.cboProductTypelist != null) {
        //                lsloanproduct_name = $scope.cboProductTypelist.loanproduct_name;
        //                lsloanproduct_gid = $scope.cboProductTypelist.loanproduct_gid;
        //            }
        //            if ($scope.cboProductSubTypelist != undefined || $scope.cboProductSubTypelist != null) {
        //                lsloansubproduct_name = $scope.cboProductSubTypelist.loansubproduct_name;
        //                lsloansubproduct_gid = $scope.cboProductSubTypelist.loansubproduct_gid;
        //            }
        //            if ($scope.cboLoanTypelist != undefined || $scope.cboLoanTypelist != null) {
        //                lsloantype_gid = $scope.cboLoanTypelist.loantype_gid;
        //                lsloan_type = $scope.cboLoanTypelist.loan_type;
        //            }
        //            if ($scope.cboprincipalfrequency != undefined || $scope.cboprincipalfrequency != null) {
        //                lsprincipalfrequency_gid = $scope.cboprincipalfrequency.principalfrequency_gid;
        //                lsprincipalfrequency_name = $scope.cboprincipalfrequency.principalfrequency_name;
        //            }
        //            if ($scope.cboInterestFrequency != undefined || $scope.cboInterestFrequency != null) {
        //                lsinterestfrequency_name = $scope.cboInterestFrequency.interestfrequency_name;
        //                lsinterestfrequency_gid = $scope.cboInterestFrequency.interestfrequency_gid;
        //            }

        //            var params = {
        //                product_type: lsloanproduct_name,
        //                producttype_gid: lsloanproduct_gid,
        //                productsub_type: lsloansubproduct_name,
        //                productsubtype_gid: lsloansubproduct_gid,
        //                loantype_gid: lsloantype_gid,
        //                loan_type: lsloan_type,
        //                facilityloan_amount: $scope.txtloanfaility_amount,
        //                facilityrequested_date: $scope.txteditfacilityreqon_date,
        //                rate_interest: $scope.txteditrate_interest,
        //                penal_interest: $scope.txteditpanel_interest,
        //                facilityvalidity_year: $scope.txteditvalidity_years,
        //                facilityvalidity_month: $scope.txteditvalidity_months,
        //                facilityvalidity_days: $scope.txteditvalidity_days,
        //                facilityoverall_limit: $scope.txtoverallfacilityvalidity_limit,
        //                tenureproduct_year: $scope.txtedittenure_years,
        //                tenureproduct_month: $scope.txtedittenure_months,
        //                tenureproduct_days: $scope.txtedittenure_days,
        //                tenureoverall_limit: $scope.txteditoveralllimit_validity,
        //                facility_type: $scope.cboFacilityTypelist,
        //                facility_mode: $scope.cboFacilitymodelist,
        //                principalfrequency_name: lsprincipalfrequency_name,
        //                principalfrequency_gid: lsprincipalfrequency_gid,
        //                interestfrequency_name: lsinterestfrequency_name,
        //                interestfrequency_gid: lsinterestfrequency_gid,
        //                interest_status: $scope.rdbinterest_status,
        //                moratorium_status: $scope.rdbmoratorium_status,
        //                moratorium_type: $scope.cbomoratorium_type,
        //                moratorium_startdate: $scope.txtmoratorium_startdate,
        //                moratorium_enddate: $scope.txtmoratorium_enddate,
        //            }

        //            var url = 'api/AgrMstSuprApplicationEdit/PostEditLoanDtl';
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();

        //                if (resp.data.status == true) {
        //                    var param = {
        //                        application_gid: $scope.application_gid
        //                    }
        //                    var url = 'api/AgrMstSuprApplicationEdit/LoanDetailList';
        //                    SocketService.getparams(url, param).then(function (resp) {
        //                        $scope.Loandtl_list = resp.data.mstloan_list;
        //                        $scope.collateral_status = resp.data.collateral_status;
        //                        $scope.buyer_status = resp.data.buyer_status;
        //                    });
        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    loandetailslist();
        //                    $scope.cboProductTypelist = '';
        //                    $scope.txteditfacilityreqon_date = '';
        //                    $scope.cboProductSubTypelist = '';
        //                    $scope.cboLoanTypelist = '';
        //                    $scope.txtloanfaility_amount = '';
        //                    $scope.txteditrate_interest = '';
        //                    $scope.txteditpanel_interest = '';
        //                    $scope.txteditvalidity_years = '';
        //                    $scope.txteditvalidity_months = '';
        //                    $scope.txteditvalidity_days = '';
        //                    $scope.txtoverallfacilityvalidity_limit = '';
        //                    $scope.txtedittenure_years = '';
        //                    $scope.txtedittenure_months = '';
        //                    $scope.txtedittenure_days = '';
        //                    $scope.txteditoveralllimit_validity = '';
        //                    $scope.cboFacilitymodelist = '';
        //                    document.getElementById('words_totalamount1').innerHTML = '';
        //                    $scope.cboprincipalfrequency = '';
        //                    $scope.cboInterestFrequency = '';
        //                    $scope.rdbinterest_status = '';
        //                    $scope.rdbmoratorium_status = '';
        //                    $scope.cbomoratorium_type = '';
        //                    $scope.txtmoratorium_startdate = '';
        //                    $scope.txtmoratorium_enddate = '';
        //                    $scope.cboFacilityTypelist = '';
        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'info',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //            });
        //        }
        //    }
        //}

        function loandetailslist() {

            var param = {
                application_gid: $scope.application_gid
            };

            var url = 'api/AgrMstSuprApplicationEdit/LoanTempDetailList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.Loandtl_list = resp.data.mstloan_list;
            });
        }
        $scope.collectdeduct = function () {
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                processing_fee = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                doc_charges = parseInt(document.getElementById("doc_charges").value)
                console.log(doc_charges)
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                adhoc_fee = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                life_insurance = parseInt(document.getElementById("life_insurance").value)
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                processing_fee_deduct = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                doc_charges_deduct = parseInt(document.getElementById("doc_charges").value)
                console.log(doc_charges)
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                life_insurance_deduct = parseInt(document.getElementById("life_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }
        $scope.productchargesloan_edit = function (application2loan_gid) {
            $location.url('app/AgrMstSuprApplicationLoanEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            var modalInstance = $modal.open({
                templateUrl: '/loanedit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/AgrMstSuprApplicationAdd/GetproductDropDown';
                SocketService.get(url).then(function (resp) {
                    $scope.loanproductlist = resp.data.loanproductlist;
                    $scope.loantypelist = resp.data.loantypelist;
                    $scope.principalfrequencylist = resp.data.principalfrequencylist;
                    $scope.interestfrequencylist = resp.data.interestfrequencylist;
                });

                var params = {
                    application2loan_gid: application2loan_gid
                }
                var url = 'api/AgrMstSuprApplicationEdit/LoanDetailsEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditfacilityreqon_date = new Date(resp.data.facilityrequested_date);
                    $scope.cboProductTypelist = resp.data.producttype_gid;

                    var params = {
                        loanproduct_gid: resp.data.producttype_gid, application_gid: '',
                        application2loan_gid: ''
                    }
                    var url = 'api/AgrMstSuprApplicationAdd/GetLoanSubProduct';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.loansubproductlist = resp.data.application_list;
                    });

                    $scope.cboProductSubTypelist = resp.data.productsubtype_gid;
                    $scope.cboLoanTypelist = resp.data.loantype_gid;
                    $scope.txtloanfaility_amount = resp.data.facilityloan_amount;
                    $scope.txteditrate_interest = resp.data.rate_interest;
                    $scope.txteditpanel_interest = resp.data.penal_interest;
                    $scope.txteditvalidity_years = resp.data.facilityvalidity_year;
                    $scope.txteditvalidity_months = resp.data.facilityvalidity_month;
                    $scope.txteditvalidity_days = resp.data.facilityvalidity_days;
                    $scope.txtoverallfacilityvalidity_limit = resp.data.facilityoverall_limit;
                    $scope.txtedittenure_years = resp.data.tenureproduct_year;
                    $scope.txtedittenure_months = resp.data.tenureproduct_month;
                    $scope.txtedittenure_days = resp.data.tenureproduct_days;
                    $scope.txteditoveralllimit_validity = resp.data.tenureoverall_limit;
                    $scope.cboFacilityTypelist = resp.data.facility_type;
                    $scope.cboFacilitymodelist = resp.data.facility_mode;
                    $scope.cboprincipalfrequency = resp.data.principalfrequency_gid;
                    $scope.cboInterestFrequency = resp.data.interestfrequency_gid;
                    $scope.rdbinterest_status = resp.data.interest_status;
                    $scope.rdbmoratorium_status = resp.data.moratorium_status;
                    $scope.cbomoratorium_type = resp.data.moratorium_type;
                    $scope.txtmoratorium_startdate = new Date(resp.data.moratorium_startdate);
                    $scope.txtmoratorium_enddate = new Date(resp.data.moratorium_enddate);

                });

                $scope.producttype = function () {
                    var params = {
                        loanproduct_gid: $scope.cboProductTypelist.loanproduct_gid, application_gid: '',
                        application2loan_gid: ''
                    }
                    var url = 'api/AgrMstSuprApplicationAdd/GetLoanSubProduct';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.loansubproductlist = resp.data.application_list;
                    });
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_loandtl = function () {

                    var loanproductname = $('#loanproduct_name :selected').text();
                    var loansubproductname = $('#loansubproduct_name :selected').text();
                    var loantype = $('#loan_type :selected').text();
                    var principalfrequencyname = $('#principalfrequency_name :selected').text();
                    var interestfrequencyname = $('#interestfrequency_name :selected').text();

                    var params = {
                        product_type: loanproductname,
                        producttype_gid: $scope.cboProductTypelist,
                        facilityrequested_date: $scope.txteditfacilityreqon_date,
                        productsub_type: loansubproductname,
                        productsubtype_gid: $scope.cboProductSubTypelist,
                        loantype_gid: $scope.cboLoanTypelist,
                        loan_type: loantype,
                        facilityloan_amount: $scope.txtloanfaility_amount,
                        rate_interest: $scope.txteditrate_interest,
                        penal_interest: $scope.txteditpanel_interest,
                        facilityvalidity_year: $scope.txteditvalidity_years,
                        facilityvalidity_month: $scope.txteditvalidity_months,
                        facilityvalidity_days: $scope.txteditvalidity_days,
                        facilityoverall_limit: $scope.txtoverallfacilityvalidity_limit,
                        tenureproduct_year: $scope.txtedittenure_years,
                        tenureproduct_month: $scope.txtedittenure_months,
                        tenureproduct_days: $scope.txtedittenure_days,
                        tenureoverall_limit: $scope.txteditoveralllimit_validity,
                        facility_type: $scope.cboFacilityTypelist,
                        facility_mode: $scope.cboFacilitymodelist,
                        principalfrequency_name: principalfrequencyname,
                        principalfrequency_gid: $scope.cboprincipalfrequency,
                        interestfrequency_name: interestfrequencyname,
                        interestfrequency_gid: $scope.cboInterestFrequency,
                        interest_status: $scope.rdbinterest_status,
                        moratorium_status: $scope.rdbmoratorium_status,
                        moratorium_type: $scope.cbomoratorium_type,
                        moratorium_startdate: $scope.txtmoratorium_startdate,
                        moratorium_enddate: $scope.txtmoratorium_enddate,
                        application2loan_gid: application2loan_gid,
                        application_gid: localStorage.getItem("application_gid"),
                    }
                    var url = 'api/AgrMstSuprApplicationEdit/LoanDetailsUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            loandetailslist();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.productchargesloan_delete = function (application2loan_gid) {
            var params =
               {
                   application2loan_gid: application2loan_gid
               }
            var url = 'api/AgrMstSuprApplicationEdit/DeleteLoanDetail';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    loandetailslist();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });

        }

        $scope.buyer = function () {
            var params = {
                buyer_gid: $scope.cboBuyer.buyer_gid,
            }
            var url = 'api/AgrMstSuprApplicationAdd/GetBuyerInfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtbuyer_limit = resp.data.buyer_limit;
            });
        }

        $scope.buyerdtl_add = function () {
            if (($scope.cboBuyer == undefined) || ($scope.cboBuyer == '') || ($scope.txtbill_tenure == undefined) || ($scope.txtmargin == undefined) || ($scope.txtbill_tenure == '') || ($scope.txtmargin == '')) {
                Notify.alert('Enter all Mandatory Fields');
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
                var url = 'api/AgrMstSuprApplicationAdd/PostBuyer';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        buyerdetailslist();
                        $scope.cboBuyer = '';
                        $scope.txtbuyer_limit = '';
                        $scope.txtavailed_limit = '';
                        $scope.txtbalance_limit = '';
                        $scope.txtbill_tenure = '';
                        $scope.txtmargin = '';
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });
            }
        }

        function buyerdetailslist() {

            var param = {
                application2loan_gid: $scope.application2loan_gid
            };

            var url = 'api/AgrMstSuprApplicationEdit/BuyerTempDetailsList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstbuyer_list = resp.data.mstbuyer_list;
            });
        }

        $scope.buyerdtl_edit = function (application2buyer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/buyerdtledit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/AgrMstSuprApplicationAdd/GetproductDropDown';
                SocketService.get(url).then(function (resp) {
                    $scope.buyerlist = resp.data.buyerlist;
                });

                var params = {
                    application2buyer_gid: application2buyer_gid
                }
                var url = 'api/AgrMstSuprApplicationEdit/BuyerDetailsEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.cboBuyer = resp.data.buyer_gid;
                    $scope.txteditbuyer_limit = resp.data.buyer_limit;
                    $scope.txteditavailed_limit = resp.data.availed_limit;
                    $scope.txteditbalance_limit = resp.data.balance_limit;
                    $scope.txteditbill_tenuredays = resp.data.bill_tenure;
                    $scope.txteditmargin = resp.data.margin;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.buyer = function () {
                    var params = {
                        buyer_gid: $scope.cboBuyer.buyer_gid,
                    }
                    var url = 'api/AgrMstSuprApplicationAdd/GetBuyerInfo';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtbuyer_limit = resp.data.buyer_limit;
                    });
                }

                $scope.update_buyerdtl = function () {

                    var buyername = $('#buyer_name :selected').text();

                    var params = {
                        buyer_name: buyername,
                        buyer_gid: $scope.cboBuyer,
                        buyer_limit: $scope.txteditbuyer_limit,
                        availed_limit: $scope.txteditavailed_limit,
                        balance_limit: $scope.txteditbalance_limit,
                        bill_tenure: $scope.txteditbill_tenuredays,
                        margin: $scope.txteditmargin,
                        application2buyer_gid: application2buyer_gid,
                        application_gid: localStorage.getItem("application_gid"),
                    }
                    var url = 'api/AgrMstSuprApplicationEdit/BuyerDetailsUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            buyerdetailslist();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.buyerdtl_delete = function (application2buyer_gid) {
            var params =
               {
                   application2buyer_gid: application2buyer_gid
               }
            var url = 'api/AgrMstSuprApplicationEdit/DeleteBuyerDetails';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    buyerdetailslist();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
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
                    application_gid: $scope.application_gid,
                }
                var url = 'api/AgrMstSuprApplicationAdd/PostCollateral';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        collatertaldetailslist();
                        $scope.cboSourceType = '';
                        $scope.txtguidelinevalue = '';
                        $scope.txtguideline_date = '';
                        $scope.txtmarketValue = '';
                        $scope.txtmarketvalue_date = '';
                        $scope.txtforcedsource_value = '';
                        $scope.txtcollateralSSV_value = '';
                        $scope.txtforcedvalueassessed_on = '';
                        $scope.txtcolateralobservation_summary = '';
                        $scope.DocumentList = '';
                        document.getElementById('words_totalamount2').innerHTML = '';
                        document.getElementById('words_totalamount3').innerHTML = '';
                        document.getElementById('words_totalamount4').innerHTML = '';
                        document.getElementById('words_totalamount5').innerHTML = '';
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });
            }
        }

        function collatertaldetailslist() {

            var param = {
                application_gid: $scope.application_gid
            };

            var url = 'api/AgrMstSuprApplicationEdit/CollateralTempDetailsList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.Collateral_list = resp.data.collatertal_list;
                $scope.CollateralDocumentList = resp.data.DocumentList;
            });

        }

        $scope.edit_Collateraldtls = function (application2collateral_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Collateraldtledit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];

            function ModalInstanceCtrl($scope, $modalInstance) {

                
                $scope.txtguidelinevaluechange = function () {
                    var input = document.getElementById('GuidelineValueedit').value;
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var lswords_totalamountedit2 = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txteditguidelinevalue = "";
                    }
                    else {
                        //  $scope.txteditguidelinevalue = output;
                        document.getElementById('words_totalamountedit2').innerHTML = lswords_totalamountedit2;
                    }
                }

                $scope.txteditMarketValuechange = function () {
                    var input = document.getElementById('MarketValueedit').value;
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var lswords_totalamountedit3 = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txteditMarketValue = "";
                    }
                    else {
                        //   $scope.txteditMarketValue = output;
                        document.getElementById('words_totalamountedit3').innerHTML = lswords_totalamountedit3;
                    }
                }

                $scope.txtForcedSourceValuechange = function () {
                    var input = document.getElementById('ForcedSourceValueedit').value;
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var lswords_totalamountedit4 = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txteditForcedSourceValue = "";
                    }
                    else {
                        //    $scope.txteditForcedSourceValue = output;
                        document.getElementById('words_totalamountedit4').innerHTML = lswords_totalamountedit4;
                    }
                }

                $scope.txtCollateralSSVvaluechange = function () {
                    var input = document.getElementById('CollateralSSVvalueedit').value;
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var lswords_totalamountedit5 = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txteditCollateralSSVvalue = "";
                    }
                    else {
                        //   $scope.txteditCollateralSSVvalue = output;
                        document.getElementById('words_totalamountedit5').innerHTML = lswords_totalamountedit5;
                    }
                }

                var params = {
                    application2collateral_gid: application2collateral_gid
                }

                var url = 'api/AgrMstSuprApplicationEdit/CollateralDetailsEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.cboeditSourceType = resp.data.source_type;
                    $scope.txteditguidelinevalue = resp.data.guideline_value;
                    $scope.txteditguideline_date = new Date(resp.data.guideline_date);
                    $scope.txteditMarketValue = resp.data.market_value;
                    $scope.txteditmarketvalue_date = new Date(resp.data.marketvalue_date);
                    $scope.txteditForcedSourceValue = resp.data.forcedsource_value;
                    $scope.txteditCollateralSSVvalue = resp.data.collateralSSV_value;
                    $scope.txteditForcedValueAssessedOn_date = new Date(resp.data.forcedvalueassessed_on);
                    $scope.txteditObservation_Summary = resp.data.collateralobservation_summary;

                });

                var url = 'api/AgrMstSuprApplicationEdit/CollateralDocumentTempList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.DocumentList = resp.data.DocumentList;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.upload_doc = function (val, val1, name) {
                    if (($scope.cboDocumentTitle == null) || ($scope.cboDocumentTitle == '') || ($scope.cboDocumentTitle == undefined)) {
                        $("#file").val('');
                        Notify.alert('Kindly Select the Document Title', 'warning');
                    }
                    else {
                        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

                        if (IsValidExtension == false) {
                            Notify.alert("File format is not supported..!", {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            return false;
                        }
                        var item = {
                            name: val[0].name,
                            file: val[0]
                        };
                        var frm = new FormData();
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name);
                        frm.append('document_name', $scope.documentname);
                        frm.append('document_title', $scope.cboDocumentTitle);
                        frm.append('application2collateral_gid', application2collateral_gid);
                        frm.append('project_flag', "documentformatonly");
                        $scope.uploadfrm = frm;
                        lockUI();
                        var url = 'api/AgrMstSuprApplicationEdit/Editcollateraldocument';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            unlockUI();
                            $("#file").val('');

                            if (resp.data.status == true) {
                                $scope.DocumentList = resp.data.DocumentList;
                                console.log(resp.data.DocumentList)
                                $scope.cboDocumentTitle = '';
                                $scope.uploadfrm = undefined;
                                $("#file").val('');
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
                }

                $scope.uploaddocumentcancel = function (val, val1) {
                    var params = { document_gid: val };

                    var url = 'api/AgrMstSuprApplicationAdd/deletecollateraldoc';
                    SocketService.getparams(url, params).then(function (resp) {
                        var params = {
                            application2collateral_gid: application2collateral_gid
                        }
                        var url = 'api/AgrMstSuprApplicationEdit/CollateralDocumentTempList';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.DocumentList = resp.data.DocumentList;
                        });
                        if (resp.data.status == true) {
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

                $scope.collateraldownloads = function (val1, val2) {
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

                $scope.update_Collateraldtl = function () {
                    var params = {
                        source_type: $scope.cboeditSourceType,
                        guideline_value: $scope.txteditguidelinevalue,
                        guideline_date: $scope.txteditguideline_date,
                        market_value: $scope.txteditMarketValue,
                        marketvalue_date: $scope.txteditmarketvalue_date,
                        forcedsource_value: $scope.txteditForcedSourceValue,
                        collateralSSV_value: $scope.txteditCollateralSSVvalue,
                        forcedvalueassessed_on: $scope.txteditForcedValueAssessedOn_date,
                        collateralobservation_summary: $scope.txteditObservation_Summary,
                        application2collateral_gid: application2collateral_gid,
                        applicationgid: localStorage.getItem("application_gid"),

                    }
                    var url = 'api/AgrMstSuprApplicationEdit/CollateralDetailsUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            collatertaldetailslist();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.delete_Collateraldtls = function (application2collateral_gid) {
            var params =
               {
                   application2collateral_gid: application2collateral_gid
               }
            var url = 'api/AgrMstSuprApplicationEdit/DeleteCollateralDetails';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    collatertaldetailslist();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.add_Hypothecationdtls = function () {
            if ($scope.cboSecurityType == null || $scope.cboSecurityType == '') {
                Notify.alert('Kindly select security type', 'warning')
            }
            else {

                var lsSecurityType_name = '';
                var lsSecurityType_gid = '';

                if ($scope.cboSecurityType != undefined || $scope.cboSecurityType != null) {
                    lsSecurityType_name = $scope.cboSecurityType.security_type;
                    lsSecurityType_gid = $scope.cboSecurityType.securitytype_gid;
                }

                var params = {
                    securitytype_gid: lsSecurityType_gid,
                    security_type: lsSecurityType_name,
                    security_description: $scope.txtsecurity_desc,
                    security_value: $scope.txtSecurity_Value,
                    securityassessed_date: $scope.txtSecurityAssessed_date,
                    asset_id: $scope.txtasset_id,
                    roc_fillingid: $scope.txtroc_fillingid,
                    CERSAI_fillingid: $scope.txtCERSAI_fillingid,
                    hypoobservation_summary: $scope.txthypoobservation_summary,
                    primary_security: $scope.txtprimary_security
                }
                var url = 'api/AgrMstSuprApplicationAdd/PostHypothecation';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        hypothecationdetailslist();
                        $scope.cboSecurityType = '';
                        $scope.txtsecurity_desc = '';
                        $scope.txtSecurity_Value = '';
                        $scope.txtSecurityAssessed_date = '';
                        $scope.txtasset_id = '';
                        $scope.txtroc_fillingid = '';
                        $scope.txtCERSAI_fillingid = '';
                        $scope.txthypoobservation_summary = '';
                        $scope.txtprimary_security = '';
                        $scope.DocumentList = '';
                        document.getElementById('words_totalamount6').innerHTML = '';
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });
            }
        }

        function hypothecationdetailslist() {

            var param = {
                application_gid: $scope.application_gid
            };

            var url = 'api/AgrMstSuprApplicationEdit/HypothecationTempDetailsList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.Hypothecation_list = resp.data.hypothecation_list;
                $scope.HypothecationDocumentList = resp.data.DocumentList;
            });
        }

        $scope.edit_Hypothecationdtls = function (application2hypothecation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Hypothecationdtlsedit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/AgrMstSuprApplicationAdd/GetproductDropDown';
                SocketService.get(url).then(function (resp) {
                    $scope.securitytype_list = resp.data.securitytype_list;
                });

                $scope.txtSecurityValuechange = function () {
                    var input = document.getElementById('SecurityValueedit').value;
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var lswords_totalamountedit6 = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txteditSecurity_Value = "";
                    }
                    else {
                        //    $scope.txteditSecurity_Value = output;
                        document.getElementById('words_totalamountedit6').innerHTML = lswords_totalamountedit6;
                    }
                }

                
                var params = {
                    application2hypothecation_gid: application2hypothecation_gid
                }
                var url = 'api/AgrMstSuprApplicationEdit/HypothecationDetailsEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.cboeditSecurityType = resp.data.securitytype_gid;
                    $scope.txteditsecurity_desc = resp.data.security_description;
                    $scope.txteditSecurity_Value = resp.data.security_value;
                    $scope.txtSecurityAssessededit_date = new Date(resp.data.securityassessed_date);
                    $scope.txtassetedit_id = resp.data.asset_id;
                    $scope.txtrocedit_fillingid = resp.data.roc_fillingid;
                    $scope.txtCERSAIedit_fillingid = resp.data.CERSAI_fillingid;
                    $scope.txthypoobservationedit_summary = resp.data.hypoobservation_summary;
                    $scope.txtprimaryedit_security = resp.data.primary_security;
                });

                var url = 'api/AgrMstSuprApplicationEdit/HypothecationDocumentTempList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.DocumentList = resp.data.DocumentList;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }
        }
        $scope.uploadhypothecationdoc = function (val, val1, name) {
            if (($scope.cbohypodoc_title == null) || ($scope.cbohypodoc_title == '') || ($scope.cbohypodoc_title == undefined)) {
                $("#file").val('');
                Notify.alert('Kindly Select the Document Title', 'warning');
            }
            else {
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

                        if (IsValidExtension == false) {
                            Notify.alert("File format is not supported..!", {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            return false;
                        }
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cbohypodoc_title);
                frm.append('application2hypothecation_gid', application2hypothecation_gid);
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                lockUI();
                var url = 'api/AgrMstSuprApplicationEdit/EditHypoDoc';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    unlockUI();
                    $("#file").val('');
                    if (resp.data.status == true) {
                        $scope.DocumentList = resp.data.DocumentList;
                        $scope.cbohypodoc_title = '';
                        $scope.uploadfrm = undefined;
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
        }
        $scope.hypodoccancel = function (val, val1) {
            var params = { document_gid: val };

            var url = 'api/AgrMstSuprApplicationAdd/deleteHypoDoc';
            SocketService.getparams(url, params).then(function (resp) {
                var params = {
                    application2hypothecation_gid: application2hypothecation_gid
                }
                var url = 'api/AgrMstSuprApplicationEdit/HypothecationDocumentTempList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.DocumentList = resp.data.DocumentList;
                });
                if (resp.data.status == true) {
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

        $scope.hypodocdownloads = function (val1, val2) {
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

        $scope.update_Hypothecationdtl = function () {

            var securitytype = '';
            var securitytype_gid = '';

            if ($scope.cboeditSecurityType != undefined || $scope.cboeditSecurityType != null) {
                securitytype = $('#security_type :selected').text();

                securitytype_gid = $scope.cboeditSecurityType;
            }

            var params = {
                securitytype_gid: securitytype_gid,
                security_type: securitytype,
                security_description: $scope.txteditsecurity_desc,
                security_value: $scope.txteditSecurity_Value,
                securityassessed_date: $scope.txtSecurityAssessededit_date,
                asset_id: $scope.txtassetedit_id,
                roc_fillingid: $scope.txtrocedit_fillingid,
                CERSAI_fillingid: $scope.txtCERSAIedit_fillingid,
                hypoobservation_summary: $scope.txthypoobservationedit_summary,
                primary_security: $scope.txtprimaryedit_security,
                application2hypothecation_gid: application2hypothecation_gid,
                application_gid: application_gid
            }
            var url = 'api/AgrMstSuprApplicationEdit/HypothecationDetailsUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //  hypothecationdetailslist();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });

        }
        $scope.delete_Hypothecationdtls = function (application2hypothecation_gid) {
            var params =
               {
                   application2hypothecation_gid: application2hypothecation_gid
               }
            var url = 'api/AgrMstSuprApplicationEdit/DeleteHypothecationDetails';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    hypothecationdetailslist();
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

        $scope.upload_doc = function (val, val1, name) {
            if (($scope.cboDocumentTitle == null) || ($scope.cboDocumentTitle == '') || ($scope.cboDocumentTitle == undefined)) {
                $("#file").val('');
                Notify.alert('Kindly Select the Document Title', 'warning');
            }
            else {
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cboDocumentTitle);
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                lockUI();
                var url = 'api/AgrMstSuprApplicationAdd/postcollateraldocument';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    unlockUI();
                    $("#file").val('');
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
                        Notify.alert(resp.data.message, {
                            status: 'Warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });
            }
        }

        $scope.uploaddocumentcancel = function (val) {
            var params = { document_gid: val };

            var url = 'api/AgrMstSuprApplicationAdd/deletecollateraldoc';
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

        $scope.uploadhypothecationdoc = function (val, val1, name) {
            if (($scope.cbohypodoc_title == null) || ($scope.cbohypodoc_title == '') || ($scope.cbohypodoc_title == undefined)) {
                $("#hypofile").val('');
                Notify.alert('Kindly Select the Document Title', 'warning');
            }
            else {
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

                        if (IsValidExtension == false) {
                            Notify.alert("File format is not supported..!", {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            return false;
                        }
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cbohypodoc_title);
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                lockUI();
                var url = 'api/AgrMstSuprApplicationAdd/PostHypoDoc';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    unlockUI();
                    $("#hypofile").val('');
                    if (resp.data.status == true) {
                        $scope.HypothecationDocumentList = resp.data.DocumentList;
                        $scope.cbohypodoc_title = '';

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
        }

        $scope.hypodoccancel = function (val, data) {
            var params = { document_gid: val };

            var url = 'api/AgrMstSuprApplicationAdd/deleteHypoDoc';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.HypothecationDocumentList = resp.data.DocumentList;
                    angular.forEach($scope.HypothecationDocumentList, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.HypothecationDocumentList.splice(key, 1);
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

        $scope.downloads = function (val1, val2) {
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

        $scope.back = function () {
            if (lstab == 'add') {
                $location.url('app/AgrMstSuprApplicationProductChargesAdd?lstab=' + lstab);
            }
            else {
                $location.url('app/AgrMstSuprApplicationGeneralEdit');
            }
        }

        $scope.update_productcharge = function () {
            var params = {
                overalllimit_amount: $scope.txtOveralllimit_amount,
                validityoveralllimit_year: $scope.txtvalidityoveralllimit_year,
                validityoveralllimit_month: $scope.txtvalidityoveralllimit_months,
                validityoveralllimit_days: $scope.txtvalidityoveralllimit_days,
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
                total_collect: $scope.txttotal_collect,
                total_deduct: $scope.txttotal_deduct,
                application_gid: $scope.application_gid,
            }
            var url = 'api/AgrMstSuprApplicationEdit/UpdateProductCharges';
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
                        $location.url('app/AgrMstSuprApplicationGeneralAdd?lstab=' + lstab);
                    }
                    else {
                        $state.go('app.AgrMstSuprApplicationGeneralEdit');
                    }
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                if (lstab == 'add') {
                    $location.url('app/AgrMstSuprApplicationGeneralAdd?lstab=' + lstab);
                }
                else {
                    $state.go('app.AgrMstSuprApplicationGeneralEdit');
                }
            });
        }

        $scope.Submitproduct = function () {
            var params = {
                overalllimit_amount: $scope.txtOveralllimit_amount,
                validityoveralllimit_year: $scope.txtvalidityoveralllimit_year,
                validityoveralllimit_month: $scope.txtvalidityoveralllimit_months,
                validityoveralllimit_days: $scope.txtvalidityoveralllimit_days,
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
                total_collect: $scope.txttotal_collect,
                total_deduct: $scope.txttotal_deduct,
                application_gid: $scope.application_gid,
            }
            var url = 'api/AgrMstSuprApplicationEdit/SubmitEditProductCharges';
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
                        $location.url('app/AgrMstSuprApplicationGeneralAdd?lstab=' + lstab);
                    }
                    else {
                        $state.go('app.AgrMstSuprApplicationGeneralEdit');
                    }
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                if (lstab == 'add') {
                    $location.url('app/AgrMstSuprApplicationGeneralAdd?lstab=' + lstab);
                }
                else {
                    $state.go('app.AgrMstSuprApplicationGeneralEdit');
                }
            });
        }

        $scope.saveProduct_charges = function () {
            var params = {
                overalllimit_amount: $scope.txtOveralllimit_amount,
                validityoveralllimit_year: $scope.txtvalidityoveralllimit_year,
                validityoveralllimit_month: $scope.txtvalidityoveralllimit_months,
                validityoveralllimit_days: $scope.txtvalidityoveralllimit_days,
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
                total_collect: $scope.txttotal_collect,
                total_deduct: $scope.txttotal_deduct,
                application_gid: $scope.application_gid
            }
            console.log(params)
            var url = 'api/AgrMstSuprApplicationEdit/SaveEditProductCharges';
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
                        $location.url('app/AgrMstSuprApplicationGeneralAdd?lstab=' + lstab);
                    }
                    else {
                        $state.go('app.AgrMstSuprApplicationGeneralEdit');
                    }
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                if (lstab == 'add') {
                    $location.url('app/AgrMstSuprApplicationGeneralAdd?lstab=' + lstab);
                }
                else {
                    $state.go('app.AgrMstSuprApplicationGeneralEdit');
                }
            });
        }
        $scope.SubmitOverallLimit = function () {
            var lsOveralllimit_amount = parseInt($scope.txtOveralllimit_amount.replace(/[\s,]+/g, '').trim());
            var lsloanfacility_amount = parseInt($scope.lsloanfacility_amount);
            if (lsOveralllimit_amount < lsloanfacility_amount) {
                Notify.alert('Amount Exceeded from entered loan facility amount', 'warning');
            }
            else {
                $scope.overlimit_warning = true;

                var params = {
                    overalllimit_amount: $scope.txtOveralllimit_amount,
                    validityoveralllimit_year: $scope.txtvalidityoveralllimit_year,
                    validityoveralllimit_month: $scope.txtvalidityoveralllimit_month,
                    validityoveralllimit_days: $scope.txtvalidityoveralllimit_day,
                    calculationoveralllimit_validity: $scope.txtcalculationoveralllimit_validity,
                    application_gid: $scope.application_gid
                }

                var url = 'api/AgrMstSuprApplicationEdit/UpdateOverallLimit';
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
                            $location.url('app/AgrMstSuprApplicationGeneralAdd?lstab=' + lstab);
                        }
                        else {
                            $state.go('app.AgrMstSuprApplicationGeneralEdit');
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
        $scope.Deleteloan = function (application2loan_gid, producttype_gid) {
            var params =
              {
                  application2loan_gid: application2loan_gid,
                  producttype_gid: producttype_gid
              }
            var url = 'api/AgrMstSuprApplicationAdd/DeleteLoan';
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
        $scope.DeleteCharge = function (application2servicecharge_gid) {
            var params =
              {
                  application2servicecharge_gid: application2servicecharge_gid
              }
            var url = 'api/AgrMstSuprApplicationAdd/DeleteCharge';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.servicecharges_list = resp.data.servicecharges_list
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
        $scope.SubmitServiceCharges = function () {
            var params = {
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
                total_collect: $scope.txttotal_collect,
                toatl: total_collect,
                total_deduct: $scope.txttotal_deduct,
                product_type: $scope.cboProductTypelist.product_type,
                producttype_gid: $scope.cboProductTypelist.producttype_gid,
                application_gid: $scope.application_gid
            }
            console.log(params)
            var url = 'api/AgrMstSuprApplicationEdit/PostEditServiceCharges';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.servicecharges_list = resp.data.servicecharges_list;
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
                    $scope.txttotal_collect = '';
                    $scope.txttotal_deduct = '';
                    $scope.cboProductTypelist = '';
                    document.getElementById('words_totalamount51').innerHTML = '';
                    document.getElementById('words_totalamount52').innerHTML = '';
                    document.getElementById('words_totalamount53').innerHTML = '';
                    document.getElementById('words_totalamount54').innerHTML = '';
                    document.getElementById('words_totalamount55').innerHTML = '';
                    document.getElementById('words_totalamount56').innerHTML = '';
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
        $scope.Back = function () {
            if (lstab == 'add') {
                $location.url('app/AgrMstSuprApplicationGeneralAdd?lstab=' + lstab);
            }
            else {
                $state.go('app.AgrMstSuprApplicationGeneralEdit');
            }
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
                var lsoveralllimit_amount = parseInt($scope.lbloveralllimit_amount.replace(/[\s,]+/g, '').trim());
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
                var txtOveralllimit_amount = parseInt($scope.txtOveralllimit_amount.replace(/[\s,]+/g, '').trim());
                var lsloanfacility_amount = parseInt($scope.lsloanfacility_amount);

                if (txtOveralllimit_amount < lsloanfacility_amount) {
                    $scope.overlimit_warning = false;
                }
                else {
                    $scope.overlimit_warning = true;
                }
            }
        }

        $scope.onselectedmoratoriumyes = function () {
            if ($scope.rdbmoratorium_status == 'No'){
                $scope.cbomoratorium_type = '';
                $scope.txtmoratorium_startdate = '';
                $scope.txtmoratorium_enddate = '';
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
            if ($scope.txteditvalidity_years != undefined || $scope.txteditvalidity_years != null || $scope.txteditvalidity_years != '') {
                var lsyear = $scope.txteditvalidity_years + " - Year, ";
            }
            else {
                var lsyear = '';
            }
            if ($scope.txteditvalidity_months != undefined || $scope.txteditvalidity_months != null || $scope.txteditvalidity_months != '') {
                var lsmonth = $scope.txteditvalidity_months + " - Month, ";
            }
            else {
                var lsmonth = '';
            }
            if ($scope.txteditvalidity_days != undefined || $scope.txteditvalidity_days != null || $scope.txteditvalidity_days != '') {
                var lsday = $scope.txteditvalidity_days + " - Day";
            }
            else {
                var lsday = '';
            }
            $scope.txtoverallfacilityvalidity_limit = lsyear + lsmonth + lsday;
        }
        $scope.calculatetenure = function () {
            //if ($scope.txtedittenure_years != undefined || $scope.txtedittenure_years != null || $scope.txtedittenure_years != '') {
            //    var lsyear = $scope.txtedittenure_years + " - Year, ";
            //}
            //else {
            //    var lsyear = '';
            //}
            //if ($scope.txtedittenure_months != undefined || $scope.txtedittenure_months != null || $scope.txtedittenure_months != '') {
            //    var lsmonth = $scope.txtedittenure_months + " - Month, ";
            //}
            //else {
            //    var lsmonth = '';
            //}
            if ($scope.txtedittenure_days != undefined || $scope.txtedittenure_days != null || $scope.txtedittenure_days != '') {
                var lsday = $scope.txtedittenure_days + " - Day";
            }
            else {
                var lsday = '';
            }
            //$scope.txteditoveralllimit_validity = lsyear + lsmonth + lsday;

            $scope.txteditoveralllimit_validity =  lsday;


            if ($scope.txtedittenure_days == '0'){
                $scope.cbomoratorium_type = '';
                $scope.txtmoratorium_startdate = '';
                $scope.txtmoratorium_enddate = '';
                $scope.rdbmoratorium_status = '';
            }
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
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                processing_fee = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                doc_charges = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                adhoc_fee = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                life_insurance = parseInt(document.getElementById("life_insurance").value)
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                processing_fee_deduct = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                doc_charges_deduct = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                life_insurance_deduct = parseInt(document.getElementById("life_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct

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
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                processing_fee = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                doc_charges = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                adhoc_fee = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                life_insurance = parseInt(document.getElementById("life_insurance").value)
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                processing_fee_deduct = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                doc_charges_deduct = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                life_insurance_deduct = parseInt(document.getElementById("life_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct

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
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                processing_fee = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                doc_charges = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                adhoc_fee = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                life_insurance = parseInt(document.getElementById("life_insurance").value)
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                processing_fee_deduct = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                doc_charges_deduct = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                life_insurance_deduct = parseInt(document.getElementById("life_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct

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
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                processing_fee = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                doc_charges = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                adhoc_fee = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                life_insurance = parseInt(document.getElementById("life_insurance").value)
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                processing_fee_deduct = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                doc_charges_deduct = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                life_insurance_deduct = parseInt(document.getElementById("life_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct

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
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                processing_fee = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                doc_charges = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                adhoc_fee = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                life_insurance = parseInt(document.getElementById("life_insurance").value)
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                processing_fee_deduct = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                doc_charges_deduct = parseInt(document.getElementById("doc_charges").value)

            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                life_insurance_deduct = parseInt(document.getElementById("life_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct

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
            }
        }

        $scope.doneclick = function () {
            var url = 'api/AgrMstSuprApplicationEdit/EditProceed';
            SocketService.get(url).then(function (resp) {
                $scope.proceed_flag = resp.data.proceed_flag;
            });
        }

        $scope.overallsubmit_application = function () {
            var url = 'api/AgrMstSuprApplicationEdit/EditAppProceed';
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
                $state.go('app.AgrMstSuprApplicationCreationSummary');
            });

        }


        $scope.productdtl_add = function () {
            if (($scope.cboproduct_name == undefined) || ($scope.cboproduct_name == '') || ($scope.cboproduct_name == null) ||
               ($scope.cbovariety_name == undefined) || ($scope.cbovariety_name == undefined) || ($scope.cbovariety_name == '')) {
                Notify.alert('Select Product / Variety Name', 'warning');
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
                    application2loan_gid: $scope.application2loan_gid,
                    application_gid: $scope.application_gid,
                    hsn_code: $scope.txthsn_code,
                    unitpricevalue_commodity: $scope.txtunitpricevalue_commodity,
                    natureformstate_commodity: $scope.txtnatureformstate_commodity.natureformstateofcommodity_name,
                    natureformstate_commoditygid: $scope.txtnatureformstate_commodity.natureformstateofcommodity_gid,
                    qualityof_commodity: $scope.txtqualityof_commodity,
                    quantity: $scope.txtquantity,
                    uom_gid: $scope.cbocommodityuom.uom_gid,
                    uom_name: $scope.cbocommodityuom.uom_name,
                }
                var url = 'api/AgrMstSuprApplicationAdd/PostProductDtlAdd';
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
                    $scope.cboproduct_name = '';
                    $scope.cbovariety_name = '';
                    $scope.txtsector_name = '';
                    $scope.txtcategory_name = '';
                    $scope.txtbotanical_name = '';
                    $scope.txtalternative_name = '';
                    $scope.txthsn_code = '';
                    $scope.txtunitpricevalue_commodity = '';
                    $scope.txtnatureformstate_commodity = '';
                    $scope.txtqualityof_commodity = '';
                    $scope.txtquantity = '';
                    $scope.cbocommodityuom = '';
                    Tempproductdetaillist();
                });
            }
        }

        function Tempproductdetaillist() {
            var params = {
                application2loan_gid: $scope.application2loan_gid
            }

            var url = 'api/AgrMstSuprApplicationAdd/GetProductDtlList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mstproductdtl_list = resp.data.mstproduct_list;
            });
        }

        $scope.product_delete = function (application2product_gid) {
            var params =
                {
                    application2product_gid: application2product_gid
                }
            var url = 'api/AgrMstSuprApplicationEdit/DeleteAppProductDtl';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
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

                Tempproductdetaillist();
            });

        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.DocumentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.DocumentList[i].document_path, $scope.DocumentList[i].document_name);
            }
        }

    }
})();

