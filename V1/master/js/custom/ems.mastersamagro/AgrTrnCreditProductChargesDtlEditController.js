(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCreditProductChargesDtlEditController', AgrTrnCreditProductChargesDtlEditController);

    AgrTrnCreditProductChargesDtlEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function AgrTrnCreditProductChargesDtlEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCreditProductChargesDtlEditController';

        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.product_gid = $location.search().product_gid;
        var product_gid = $scope.product_gid;
        $scope.variety_gid = $location.search().variety_gid;
        var variety_gid = $scope.variety_gid;

        activate();
        function activate() {
            $scope.overlimit_warning = true;
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

            vm.calender6 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open6 = true;
            };
            vm.calender06 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open06 = true;
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

          
            var url = 'api/AgrMstApplicationEdit/GetProductChargesTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/AgrMstApplicationAdd/GetproductDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.loanproductlist = resp.data.loanproductlist;
                $scope.loantypelist = resp.data.loantypelist;
                $scope.principalfrequencylist = resp.data.principalfrequencylist;
                $scope.interestfrequencylist = resp.data.interestfrequencylist;
                $scope.buyerlist = resp.data.buyerlist;
                $scope.securitytype_list = resp.data.securitytype_list;
            });
            
            var param = {
                application_gid: $scope.application_gid
            };
            var url = 'api/AgrMstApplicationEdit/GetEditLimit';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lbloveralllimit_amount = resp.data.overalllimit_amount;


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

            var url = 'api/AgrMstApplicationAdd/TradeTmpClear';
            SocketService.get(url).then(function (resp) {
            });


            var url = 'api/AgrMstApplicationEdit/GetEditLoanDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstloan_list = resp.data.mstloan_list;
                $scope.servicecharges_list = resp.data.servicecharges_list;

                if ($scope.servicecharges_list == null) {
                    $scope.divshow = true;
                }
                else {
                    $scope.divshow = false;
                }

            }); 

            lockUI();
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

            var url = 'api/AgrMstCreditorMaster/GetNewcreditorSummary';
            unlockUI();
            SocketService.get(url).then(function (resp) {
                $scope.creditoradd_list = resp.data.MdlcreditorCreation;
            });
            var url = 'api/AgrMstApplicationEdit/GetWarehouseDetail';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.Warehousedtllist = resp.data.MdlWarehousedtl;
            });

            var url = 'api/AgrMstCreditorMaster/Getcreditorapplicanttype';
            unlockUI();
            SocketService.get(url).then(function (resp) {
                $scope.creditorwsp_list = resp.data.MdlcreditorCreation;
            });

            var url = 'api/AgrMstApplicationEdit/LoanDetailList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.Loandtl_list = resp.data.mstloan_list;
                $scope.collateral_status = resp.data.collateral_status;
                $scope.buyer_status = resp.data.buyer_status;
            });

     /*       var url = 'api/AgrMstApplicationEdit/BuyerDetailsList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.buyerdtl_list = resp.data.mstbuyer_list;
            }); */

            var url = 'api/AgrMstApplicationEdit/CollateralDetailsList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.Collateral_list = resp.data.collatertal_list;
            });

            var url = 'api/AgrMstApplicationEdit/HypothecationDetailsList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.Hypothecation_list = resp.data.hypothecation_list;
            });
            var url = 'api/AgrMstApplicationEdit/GetEditproduct';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.product_list = resp.data.product_list;
            });
            var url = 'api/AgrMstApplicationAdd/GetScopedtl';
            SocketService.get(url).then(function (resp) {
                $scope.scope_list = resp.data.ScopeList;
            });
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationAdd/GetTradeproduct';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.tradeproduct_list = resp.data.product_list;
            });

            var url = 'api/AgrMstApplicationEdit/GetProductChargesEdit';

           SocketService.getparams(url, param).then(function (resp) {
               //$scope.txtvalidityoveralllimit_year = resp.data.validityoveralllimit_year;
               //$scope.txtvalidityoveralllimit_month = resp.data.validityoveralllimit_month;
               //$scope.txtvalidityoveralllimit_day = resp.data.validityoveralllimit_days;


               $scope.onboarding_status = resp.data.onboarding_status;
               if (resp.data.onboarding_status == "Direct") {

                   $scope.txtvalidity_from = resp.data.validityfrom_date;
                   $scope.txtvalidity_to = resp.data.validityto_date;

                   $scope.txtOveralllimit_amount = '0';

                   $scope.limithide = true

                   //var days = 2;

                   //var Fromdate = new Date(Date.now());
                   ////var Todate = new Date(Date.now() + days * 24 * 60 * 60 * 1000);
                   //var Todate = new Date(new Date().setFullYear(new Date().getFullYear() + 1))

                   //var fromdate = JSON.stringify(Fromdate)
                   //fromdate = fromdate.slice(1, 11)


                   //var todate = JSON.stringify(Todate)
                   //todate = todate.slice(1, 11)

                   ////$scope.txtvalidity_from = new Date(Date.now() + days * 24 * 60 * 60 * 1000);
                   ////$scope.txtvalidity_to = new Date(Date.now() + days * 24 * 60 * 60 * 1000);

                   //$scope.txtvalidity_from = fromdate;
                   //$scope.txtvalidity_to = todate;
                   $scope.txtcalculationoveralllimit_validity = '1 Year';

               }

               else {

                    //$scope.limithide = false

                   $scope.overalllimit_amount = resp.data.overalllimit_amount
                   if (resp.data.overalllimit_amount != "") {
                       $scope.txtOveralllimit_amount = resp.data.overalllimit_amount
                       if ($scope.txtOveralllimit_amount != null && $scope.txtOveralllimit_amount != undefined && $scope.txtOveralllimit_amount != "") {
                           $scope.txtOveralllimit_amount_edit = (parseInt($scope.txtOveralllimit_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                           $scope.lblamountseperator = (parseInt($scope.txtOveralllimit_amount_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                           $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                           document.getElementById('words_totalamount7').innerHTML = $scope.lblamountwords;
                       }
                   }



                   $scope.txtvalidity_from = resp.data.validityfrom_date;
                   $scope.txtvalidity_to = resp.data.validityto_date;
                   $scope.txtcalculationoveralllimit_validity = resp.data.calculationoveralllimit_validity;

               }
                $scope.txtenduse_purpose = resp.data.enduse_purpose;
                $scope.txtprocessing_fee = resp.data.processing_fee;
                $scope.rdbprocessing_collectiontype = resp.data.processing_collectiontype;
                $scope.txtdoc_charges = resp.data.doc_charges;
                $scope.rdbdoccharge_collectiontype = resp.data.doccharge_collectiontype;
                $scope.txtfieldvisit_charges = resp.data.fieldvisit_charge;
                $scope.rdbfieldvisit_collectiontype = resp.data.fieldvisit_collectiontype;
                $scope.txtadhoc_fee = resp.data.adhoc_fee;
                $scope.rdbadhoc_collectiontype = resp.data.adhoc_collectiontype;
                $scope.txtlife_insurance = resp.data.life_insurance;
                $scope.rdblifeinsurance_collectiontype = resp.data.lifeinsurance_collectiontype;
                $scope.txtacct_insurance = resp.data.acct_insurance;
                $scope.rdbpersonalaccident_collectiontype = resp.data.acctinsurance_collectiontype;
                $scope.txttotal_collect = resp.data.total_collect;
                $scope.txttotal_deduct = resp.data.total_deduct;
               
                $scope.productcharges_status = resp.data.productcharges_status;
                if (resp.data.productcharges_status == "Incomplete") {
                    $scope.productchargesSubmit = true;
                    $scope.productchargesUpdate = false;
                }
                else {
                    $scope.productchargesSubmit = false;
                    $scope.productchargesUpdate = true;
                }

                unlockUI();
            });

            var url = 'api/AgrMstApplicationAdd/GetproductDropDown';
            SocketService.get(url).then(function (resp) {
                unlockUI();
               $scope.securitytype_list = resp.data.securitytype_list;
            });
          

        }

        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }

        $scope.OverallLimit_add = function () {
            $scope.limit_show = true;
        }
        $scope.tradeproducttype = function () {
            var getselected = $scope.tradeproduct_list.find(function (a) { return a.application2loan_gid == $scope.cboProductTypelist.application2loan_gid })
            if (getselected != null) {
                $scope.txtProductsubType = getselected.productsub_type
                $scope.txtProductsubTypeGid = getselected.productsubtype_gid
            }
        }

        $scope.tradeeditproducttype = function () {
            var getselected = $scope.tradeproduct_list.find(function (a) { return a.application2loan_gid == $scope.cboProductTypelist })
            if (getselected != null) {
                $scope.txteditProductsubType = getselected.productsub_type
                $scope.txteditProductsubTypeGid = getselected.productsubtype_gid
            }
        }


        $scope.warehouseagencychange = function () {
            var params = {
                creditor_gid: $scope.cbowarehouseagency.creditor_gid,
            }
            var url = 'api/AgrMstApplicationAdd/Getcreditor2warehouse';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.warehousename_list = resp.data.warehousename_list;
            });
            $scope.cbowarehousetype_name = '';
            $scope.txtvolume_uom = '';
            $scope.txtcapacity_volume = '';
            $scope.txtareacapacity = '';
            $scope.txtareacapacity_uom = '';
            $scope.cbowarehouseaddress = '';
            $scope.txtcapacity_commodity = '';
            $scope.txtcapacity_panina = '';
        }

        $scope.warehousenamechange = function () {
            if ($scope.cbowarehouse_name != "") {

                var data = $scope.Warehousedtllist.find(function (a) { return a.warehouse_gid === $scope.cbowarehouse_name.warehouse_gid })
                if (data != null)
                    $scope.cbowarehousetype_name = data.typeofwarehouse_name
                $scope.txtareacapacity = data.totalcapacity_area
                $scope.txtareacapacity_uom = data.totalcapacityarea_uom
                $scope.txtcapacity_volume = data.totalcapacity_volume
                $scope.txtvolume_uom = data.volume_uom
            }
            var params = {
                warehouse_gid: $scope.cbowarehouse_name.warehouse_gid,
            }
            var url = 'api/AgrMstApplicationEdit/GetWarehouseAddressDropdown';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.warehouseaddress_list = resp.data.MdlWarehouseAddressdtl;
            });


        }


        $scope.creditorchange = function () {
            if ($scope.cbocreditor != "") {

                var data = $scope.creditoradd_list.find(function (v) { return v.creditor_gid === $scope.cbocreditor.creditor_gid })
                if (data != null)
                    $scope.showdtls = true
                $scope.cboApplicant_name = data.Applicant_name
                $scope.txtApplicant_category = data.Applicant_category
                $scope.txtpan_no = data.pan_no
                $scope.txtdesignation_type = data.designation_type
                $scope.txtcontactperson_name = data.contactperson_name
                $scope.txtcontact_no = data.contact_no
            }



        }

        $scope.tradedtl_edit = function (application2trade_gid) {
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
                $scope.cboeditScopeoftransport = resp.data.scopeof_transportgid;
                $scope.cboeditScopeofloading = resp.data.scopeof_loadinggid;
                $scope.cboeditScopeofunloading = resp.data.scopeof_unloadinggid;
                $scope.cboeditScopeofqualityandquantity = resp.data.scopeof_qualityandquantitygid;
                $scope.cboeditScopeofmoisturegainloss = resp.data.scopeof_moisturegainlossgid;
                $scope.cboScopeofInsurance = resp.data.scopeof_insurancegid;
                $scope.tradeapplication2loan_gid = resp.data.application2loan_gid;
                $scope.trade_gid = resp.data.application2trade_gid
                unlockUI();
            });

            var params = {
                application2trade_gid: application2trade_gid,
                application_gid: $scope.application_gid,
                application2loan_gid: $scope.tradeapplication2loan_gid,
                tmp_status: "true"
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

        $scope.tradedtl_submit = function () {
            //var getproductselected = $scope.tradeproduct_list.find(a => a.application2loan_gid == $scope.cboeditProductTypelist);
            //var lsproducttype_gid = "", lsproducttype_name = "";
            var lstransport_name = "", lsloading_name = "", lsunloading_name, lsqualityandquantity_name, lsmoisturegainloss_name;
            //if (getselected != null) {
            //    lsproducttype_gid = getselected.product_type
            //    lsproducttype_name = getselected.producttype_gid
            //}

            var lsinsurance = '';

            var gettransport = $scope.scope_list.find(function (a) { return a.scope_gid == $scope.cboeditScopeoftransport });
            if (gettransport != null) {
                lstransport_name = gettransport.scope_name;
            }
            var getloading = $scope.scope_list.find(function (a) { return a.scope_gid == $scope.cboeditScopeofloading});
            if (getloading != null) {
                lsloading_name = getloading.scope_name;
            }
            var getunloading = $scope.scope_list.find(function (a) { return a.scope_gid == $scope.cboeditScopeofunloading});
            if (getunloading != null) {
                lsunloading_name = getunloading.scope_name;
            }
            var getqualityandquantity = $scope.scope_list.find(function (a) { return a.scope_gid == $scope.cboeditScopeofqualityandquantity});
            if (getqualityandquantity != null) {
                lsqualityandquantity_name = getqualityandquantity.scope_name;
            }
            var getmoisturegainloss = $scope.scope_list.find(function (a) { return a.scope_gid == $scope.cboeditScopeofmoisturegainloss});
            if (getmoisturegainloss != null) {
                lsmoisturegainloss_name = getmoisturegainloss.scope_name;
            }

            var getinsurance = $scope.scope_list.find(function (a) { return a.scope_gid == $scope.cboScopeofInsurance})
            if (getinsurance != null) {
                lsinsurance = getinsurance.scope_name;
            }
            var params = {
                //application2loan_gid: $scope.cboProductTypelist,
                //producttype_gid: lsproducttype_gid,
                //producttype_name: lsproducttype_name,
                //productsubtype_gid: $scope.txteditProductsubType,
                //productsubtype_name: $scope.txteditProductsubTypeGid,
                salescontract_availability: $scope.rdbeditsalescontract_availability,
                scopeof_transportgid: $scope.cboeditScopeoftransport,
                scopeof_transport: lstransport_name,
                scopeof_loadinggid: $scope.cboeditScopeofloading,
                scopeof_loading: lsloading_name,
                scopeof_unloadinggid: $scope.cboeditScopeofunloading,
                scopeof_unloading: lsunloading_name,
                scopeof_qualityandquantitygid: $scope.cboeditScopeofqualityandquantity,
                scopeof_qualityandquantity: lsqualityandquantity_name,
                scopeof_moisturegainlossgid: $scope.cboeditScopeofmoisturegainloss,
                scopeof_moisturegainloss: lsmoisturegainloss_name,
                scopeof_insurancegid: $scope.cboScopeofInsurance,
                scopeof_insurance: lsinsurance,
                application_gid: $scope.application_gid,
                tmpadd_status: false,
                application2trade_gid: $scope.application2trade_gid
            }
            var url = 'api/AgrMstApplicationAdd/UpdateTradeDtl';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.TradeEditdivshow = false;
                    fnTradeSummary();
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

        $scope.cancel_edit = function () {
            $scope.TradeEditdivshow = false;
        }

        $scope.OverallLimitValidity = function () {
            var getoverallvalidity = cmnfunctionService.fnDatediff($scope.txtvalidity_from, $scope.txtvalidity_to)
            if (getoverallvalidity == '0') {
                Notify.alert('Validity To Date Should be Greater than Validity From Date', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });

                $scope.txtvalidity_to = '';
                $scope.txtvalidity_from = '';
                $scope.txtcalculationoveralllimit_validity = '';

            }
            else if (getoverallvalidity != '1') {
                $scope.txtcalculationoveralllimit_validity = getoverallvalidity;
            }
        }

        $scope.SubmitTrade = function () {
            var params = {
                application2loan_gid: $scope.cboProductTypelist.application2loan_gid,
                producttype_gid: $scope.cboProductTypelist.producttype_gid,
                producttype_name: $scope.cboProductTypelist.product_type,
                productsubtype_gid: $scope.txtProductsubTypeGid,
                productsubtype_name: $scope.txtProductsubType,
                salescontract_availability: $scope.rdbsalescontract_availability,
                scopeof_transportgid: $scope.cboScopeoftransport.scope_gid,
                scopeof_transport: $scope.cboScopeoftransport.scope_name,
                scopeof_loadinggid: $scope.cboScopeofloading.scope_gid,
                scopeof_loading: $scope.cboScopeofloading.scope_name,
                scopeof_unloadinggid: $scope.cboScopeofunloading.scope_gid,
                scopeof_unloading: $scope.cboScopeofunloading.scope_name,
                scopeof_qualityandquantitygid: $scope.cboScopeofqualityandquantity.scope_gid,
                scopeof_qualityandquantity: $scope.cboScopeofqualityandquantity.scope_name,
                scopeof_moisturegainlossgid: $scope.cboScopeofmoisturegainloss.scope_gid,
                scopeof_moisturegainloss: $scope.cboScopeofmoisturegainloss.scope_name,
                application_gid: $scope.application_gid,
                tmpadd_status: false
            }
            var url = 'api/AgrMstApplicationAdd/PostTradedtl';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    fnTradeSummary();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.cboProductTypelist = '';
                    $scope.txtProductsubType = '';
                    $scope.txtProductsubTypeGid = '';
                    $scope.rdbsalescontract_availability = '';
                    $scope.cboScopeoftransport = '';
                    $scope.cboScopeofloading = '';
                    $scope.cboScopeofunloading = '';
                    $scope.cboScopeofqualityandquantity = '';
                    $scope.cboScopeofmoisturegainloss = '';
                    $scope.tradewarehouse_list = null;
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

        $scope.Submit_creditor = function (tradeapplication2loan_gid, trade_gid) {

            var lsdesignation_type = '';
            var lscontactperson_name = '';
            var lsemail_id = '';
            var lscontact_no = '';
            var lspan_no = '';

            if ($scope.designation_type != undefined || $scope.designation_type != null) {
                lsdesignation_type = $scope.designation_type;
            }
            if ($scope.contactperson_name != undefined || $scope.contactperson_name != null) {
                lscontactperson_name = $scope.contactperson_name;
            }
            if ($scope.email_id != undefined || $scope.email_id != null) {
                lsemail_id = $scope.email_id;
            }
            if ($scope.txtcontact_no != undefined || $scope.txtcontact_no != null) {
                lscontact_no = $scope.txtcontact_no;
            }
            if ($scope.txtpan_no != undefined || $scope.txtpan_no != null) {
                lspan_no = $scope.txtpan_no;
            }

            var params = {
                application2loan_gid: tradeapplication2loan_gid,
                creditor_gid: $scope.cbocreditor.creditor_gid,
                Applicant_name: $scope.cbocreditor.Applicant_name,
                Applicant_category: $scope.txtApplicant_category,
                designation_type: lsdesignation_type,
                contactperson_name: lscontactperson_name,
                contact_no: lscontact_no,
                pan_no: lspan_no,
                email_id: lsemail_id,
                application_gid: $scope.application_gid,
                application2trade_gid: trade_gid
            }
            var url = 'api/AgrMstCreditorMaster/PostEditTrade2CreditorDtl';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    tradecreditorlist(tradeapplication2loan_gid, trade_gid);
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.cbocreditor = '';
                    $scope.txtApplicant_category = '';
                    $scope.designation_type = '';
                    $scope.txtpan_no = '';
                    $scope.txtcontact_no = '';
                    $scope.contactperson_name = '';

                    $scope.trade2creditor_list = null;
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


        function fnTradeSummary() {
            lockUI();
            var params = {
                application_gid: $scope.application_gid,
                tmp_status: "both"
            }
            var url = 'api/AgrMstApplicationAdd/GetApplicationTradeList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MdlTradelist = resp.data.MdlTradedtl;
                if ($scope.MdlTradelist == null) {
                    $scope.Tradedivshow = true;
                    $scope.TradeEditdivshow = false;
                }
                else {
                    $scope.Tradedivshow = false;
                    $scope.TradeEditdivshow = false;
                }
                unlockUI();
            });
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationAdd/GetTradeproduct';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.tradeproduct_list = resp.data.product_list;
            });
        }

        $scope.DeleteTrade = function (application2trade_gid) {
            var params = {
                application2trade_gid: application2trade_gid
            }
            var url = 'api/AgrMstApplicationAdd/DeleteTradeDtl';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    fnTradeSummary();
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


        $scope.producttype = function () {
            var params = {
                loanproduct_gid: $scope.cboProductTypelist.loanproduct_gid, application_gid: '',
                application2loan_gid: ''
            }
            var url = 'api/AgrMstApplicationAdd/GetLoanSubProduct';
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

            else if ($scope.lbloveralllimit_amount != '0.00' && $scope.txtloanfaility_amount == '0') {
                Notify.alert('Proposed Program Limit should not be 0', 'warning')
            }

            //else if ($scope.txtloanfaility_amount == '0') {
            //    Notify.alert('Proposed Program Limit should not be 0', 'warning')
            //}
            else {
                if ($scope.txtfacilityreqon_date == null || $scope.txtfacilityreqon_date == '' || $scope.cboProductTypelist == null || $scope.cboProductTypelist == '' || $scope.cboProductSubTypelist == null || $scope.cboProductSubTypelist == '' || $scope.cboLoanTypelist == null || $scope.cboLoanTypelist == '' || $scope.txtloanfaility_amount == null || $scope.txtloanfaility_amount == '' || $scope.txtrate_interest == null || $scope.txtrate_interest == '' || $scope.txtpanel_interest == null || $scope.txtpanel_interest == '' ||  $scope.cboFacilityTypelist == null || $scope.cboFacilityTypelist == '' || $scope.txttenure_days == null || $scope.txttenure_days == '' || $scope.cboFacilitymodelist == null || $scope.cboFacilitymodelist == '') {
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
                                application_gid:$scope.application_gid
                            }
                            var url = 'api/AgrMstApplicationEdit/PostLoanEditDtl';
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
                            application_gid: $scope.application_gid
                        }
                        var url = 'api/AgrMstApplicationEdit/PostLoanEditDtl';
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
     
        function loandetailslist() {

            var param = {
                application_gid: $scope.application_gid
            };

            var url = 'api/AgrMstApplicationEdit/LoanTempDetailList';
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
            var personal_accident = 0;

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
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                personal_accident = parseInt(document.getElementById("acct_insurance").value)
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
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct + personal_accident_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }

        $scope.creditproductchargesloan_edit = function (application2loan_gid, product_gid, variety_gid) {
            $location.url('app/AgrTrnCreditLoanDtlEdit?application2loan_gid=' + application2loan_gid + '&application_gid=' + $scope.application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
        }

        $scope.productchargesloan_delete = function (application2loan_gid) {
            var params =
               {
                   application2loan_gid: application2loan_gid
               }
            var url = 'api/AgrMstApplicationEdit/DeleteLoanDetail';
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
            var url = 'api/AgrMstApplicationAdd/GetBuyerInfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtbuyer_limit = resp.data.buyer_limit;
            });
        }

        $scope.add_buyerdtl = function () {
            if (($scope.cboBuyer == undefined) || ($scope.cboBuyer == '') || ($scope.txtbill_tenuredays == undefined) || ($scope.txtmargin == undefined) || ($scope.txtbill_tenuredays == '') || ($scope.txtmargin == '')) {
                Notify.alert('Enter all Mandatory Fields');
            }
            else {
            var params = {
                buyer_name: $scope.cboBuyer.buyer_name,
                buyer_gid: $scope.cboBuyer.buyer_gid,
                buyer_limit: $scope.txtbuyer_limit,
                availed_limit: $scope.txtavailed_limit,
                balance_limit: $scope.txtbalance_limit,
                bill_tenure: $scope.txtbill_tenuredays,
                margin: $scope.txtmargin,
            }
            var url = 'api/AgrMstApplicationAdd/PostBuyer';
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
                    $scope.txtbill_tenuredays = '';
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

            var url = 'api/AgrMstApplicationEdit/BuyerTempDetailsList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.buyerdtl_list = resp.data.mstbuyer_list;
            });
        }

        $scope.buyerdtl_add = function () {
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
                var url = 'api/AgrMstApplicationAdd/PostBuyer';
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

                var url = 'api/AgrMstApplicationAdd/GetproductDropDown';
                SocketService.get(url).then(function (resp) {
                    $scope.buyerlist = resp.data.buyerlist;
                });

                var params = {
                    application2buyer_gid: application2buyer_gid
                }
                var url = 'api/AgrMstApplicationEdit/BuyerDetailsEdit';
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
                    var url = 'api/AgrMstApplicationAdd/GetBuyerInfo';
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
                    var url = 'api/AgrMstApplicationEdit/BuyerDetailsUpdate';
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
            var url = 'api/AgrMstApplicationEdit/DeleteBuyerDetails';
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
                var url = 'api/AgrMstApplicationAdd/PostCollateral';
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

            var url = 'api/AgrMstApplicationEdit/CollateralTempDetailsList';
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
              
                var url = 'api/AgrMstApplicationEdit/CollateralDetailsEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.cboeditSourceType = resp.data.source_type;
                    $scope.txteditguidelinevalue = resp.data.guideline_value;
                    $scope.txteditguideline_date = new Date(resp.data.guideline_date);
                    $scope.txteditMarketValue = resp.data.market_value;
                    $scope.txteditmarketvalue_date = new Date(resp.data.marketvalue_date);
                    $scope.txteditForcedSourceValue = resp.data.forcedsource_value;
                    $scope.txteditCollateralSSVvalue = resp.data.collateralSSV_value;
                    $scope.txteditForcedValueAssessedOn_date =new Date(resp.data.forcedvalueassessed_on);
                    $scope.txteditObservation_Summary = resp.data.collateralobservation_summary;
                
                });

                var url = 'api/AgrMstApplicationEdit/CollateralDocumentTempList';
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
                        var url = 'api/AgrMstApplicationEdit/Editcollateraldocument';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            unlockUI();
                            $("#file").val('');
                           
                            if (resp.data.status == true) {
                                $scope.DocumentList = resp.data.DocumentList;
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

                    var url = 'api/AgrMstApplicationAdd/deletecollateraldoc';
                    SocketService.getparams(url, params).then(function (resp) {
                        var params = {
                            application2collateral_gid: application2collateral_gid
                        }
                        var url = 'api/AgrMstApplicationEdit/CollateralDocumentTempList';
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
                    var url = 'api/AgrMstApplicationEdit/CollateralDetailsUpdate';
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
            var url = 'api/AgrMstApplicationEdit/DeleteCollateralDetails';
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
                var url = 'api/AgrMstApplicationAdd/PostHypothecation';
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

            var url = 'api/AgrMstApplicationEdit/HypothecationTempDetailsList';
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

                var url = 'api/AgrMstApplicationAdd/GetproductDropDown';
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
                var url = 'api/AgrMstApplicationEdit/HypothecationDetailsEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.cboeditSecurityType = resp.data.securitytype_gid;
                    $scope.txteditsecurity_desc = resp.data.security_description;
                    $scope.txteditSecurity_Value = resp.data.security_value;
                    $scope.txtSecurityAssessededit_date =new Date(resp.data.securityassessed_date);
                    $scope.txtassetedit_id = resp.data.asset_id;
                    $scope.txtrocedit_fillingid = resp.data.roc_fillingid;
                    $scope.txtCERSAIedit_fillingid = resp.data.CERSAI_fillingid;
                    $scope.txthypoobservationedit_summary = resp.data.hypoobservation_summary;
                    $scope.txtprimaryedit_security = resp.data.primary_security;
                });

                var url = 'api/AgrMstApplicationEdit/HypothecationDocumentTempList';
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
                var url = 'api/AgrMstApplicationEdit/EditHypoDoc';
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

            var url = 'api/AgrMstApplicationAdd/deleteHypoDoc';
            SocketService.getparams(url, params).then(function (resp) {
                var params = {
                    application2hypothecation_gid: application2hypothecation_gid
                }
                var url = 'api/AgrMstApplicationEdit/HypothecationDocumentTempList';
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
            var url = 'api/AgrMstApplicationEdit/HypothecationDetailsUpdate';
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
            var url = 'api/AgrMstApplicationEdit/DeleteHypothecationDetails';
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

        $scope.upload_collateraldoc = function (val, val1, name) {
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
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                lockUI();
                var url = 'api/AgrMstApplicationAdd/postcollateraldocument';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    unlockUI();
                    $("#file").val('');
                    if (resp.data.status == true) {
                        $scope.CollateralDocumentList = resp.data.DocumentList;
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

        $scope.uploadcollateraldocumentcancel = function (val, data) {
            var params = { document_gid: val };

            var url = 'api/AgrMstApplicationAdd/deletecollateraldoc';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.CollateralDocumentList = resp.data.DocumentList;
                    angular.forEach($scope.CollateralDocumentList, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.CollateralDocumentList.splice(key, 1);
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
                var url = 'api/AgrMstApplicationAdd/PostHypoDoc';
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

            var url = 'api/AgrMstApplicationAdd/deleteHypoDoc';
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
                acctinsurance_collectiontype: $scope.rdbpersonalaccident_collectiontype,
                total_collect: $scope.txttotal_collect,
                total_deduct: $scope.txttotal_deduct,
                application_gid: $scope.application_gid,
            }
            var url = 'api/AgrMstApplicationEdit/UpdateProductCharges';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lspage == "myapp") {
                        $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&lspage='+ lspage);
                    }
                    else if (lspage == "CreditApproval") {
                        $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
                    }
                    else if (lspage == "CADApplicationEdit") {
                        $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
                    }
                    else {

                    }
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                if (lspage == "myapp") {
                    $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&lspage='+ lspage);
                }
                else if (lspage == "CreditApproval") {
                    $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
                }
                else if (lspage == "CADApplicationEdit") {
                    $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
                }
                else {

                }
            }); 
         }
         
         $scope.SubmitOverallLimit = function () {
             var lsOveralllimit_amount = parseInt($scope.txtOveralllimit_amount.replace(/[\s,]+/g, '').trim());
             var lsloanfacility_amount = parseInt($scope.lsloanfacility_amount);
             if (lsOveralllimit_amount < lsloanfacility_amount) {
                 Notify.alert('Amount Exceeded from entered loan facility amount','warning');
             }
             if ($scope.txtcalculationoveralllimit_validity == 'NaN Years NaN Months NaN Days') {
                 Notify.alert('Kindly Enter Valid Date Format', 'warning');
             }
             else {
                 $scope.overlimit_warning = true;

                     try {
                         if ($scope.txtvalidity_from.split("-"))
                             $scope.txtvalidity_from = $scope.txtvalidity_from.split("-").reverse().join("-");
                     }
                     catch (e) { $scope.txtvalidity_from = $scope.txtvalidity_from  }
                     try {
                         if ($scope.txtvalidity_to.split("-"))
                             $scope.txtvalidity_to = $scope.txtvalidity_to.split("-").reverse().join("-");
                     }
                     catch (e) { $scope.txtvalidity_to = $scope.txtvalidity_to }
            
             var params = {
                 overalllimit_amount: $scope.txtOveralllimit_amount,
                 //validityoveralllimit_year: $scope.txtvalidityoveralllimit_year,
                 //validityoveralllimit_month: $scope.txtvalidityoveralllimit_month,
                 //validityoveralllimit_days: $scope.txtvalidityoveralllimit_day,
                 validityfrom_date: $scope.txtvalidity_from,
                 validityto_date: $scope.txtvalidity_to,
                 calculationoveralllimit_validity: $scope.txtcalculationoveralllimit_validity,
                 application_gid: $scope.application_gid
             }

             var url = 'api/AgrMstApplicationEdit/UpdateOverallLimit';
             lockUI();
             SocketService.post(url, params).then(function (resp) {
                 unlockUI();
                 if (resp.data.status == true) {
                     Notify.alert(resp.data.message, {
                         status: 'success',
                         pos: 'top-center',
                         timeout: 3000
                     });
                     if (lspage == "myapp") {
                         $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
                     }
                     else if (lspage == "CreditApproval") {
                         $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
                     }
                     else if (lspage == "CADApplicationEdit") {
                         $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
                     }
                     else if (lspage == "CADAcceptanceCustomers") {
                         $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
                     }
                     else if (lspage == "PendingCADReview") {
                         $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
                     }
                     else if (lspage == "submittoapp") {
                         $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
                     }

                     else {

                     }
                 }
                 else {
                     Notify.alert(resp.data.message, {
                         status: 'warning',
                         pos: 'top-center',
                         timeout: 3000
                     });
                 }
                 activate();
             });
             }
         }
         $scope.Deleteloan = function (application2loan_gid, producttype_gid) {
             var params =
               {
                   application2loan_gid: application2loan_gid,
                   producttype_gid: producttype_gid
               }
             var url = 'api/AgrMstApplicationAdd/DeleteLoan';
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
             var url = 'api/AgrMstApplicationAdd/DeleteCharge';
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
                 acctinsurance_collectiontype: $scope.rdbpersonalaccident_collectiontype,
                 total_collect: document.getElementById("total_collect").value,
                 total_deduct: document.getElementById("total_deduct").value,
                 productTypelist: $scope.cboProductTypelist,
                 application_gid: $scope.application_gid
             }
             var url = 'api/AgrMstApplicationEdit/PostEditServiceCharges';
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
                     $scope.rdbpersonalaccident_collectiontype = '';
                     $scope.cboProductTypelist = '';
                     document.getElementById('words_totalamount51').innerHTML = '';
                     document.getElementById('words_totalamount52').innerHTML = '';
                     document.getElementById('words_totalamount53').innerHTML = '';
                     document.getElementById('words_totalamount54').innerHTML = '';
                     document.getElementById('words_totalamount55').innerHTML = '';
                     document.getElementById('words_totalamount56').innerHTML = '';
                     document.getElementById("total_collect").value = '';
                     document.getElementById("total_deduct").value = '';
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
                 activate();
             });
         }

         $scope.Back = function () {
             if (lspage == "myapp") {
                 $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
             }
             else if (lspage == "CreditApproval") {
                 $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
             }
             else if (lspage == "CADApplicationEdit") {
                 $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
             }
             else if (lspage == "CADAcceptanceCustomers") {
                 $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
             }
             else if (lspage == "PendingCADReview") {
                 $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
             }
             else if (lspage == "submittoapp") {
                 $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
             }
             else {

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
              
                 if(txtOveralllimit_amount < lsloanfacility_amount)
                 {
                     $scope.overlimit_warning = false;
                 }
                 else {
                     $scope.overlimit_warning = true;
                 }
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
            if ($scope.txtvalidity_year != undefined || $scope.txtvalidity_year != null || $scope.txtvalidity_year != '') {
                var lsyear = $scope.txtvalidity_year + " - Year, ";
            }
            else {
                var lsyear = '';
            }
            if ($scope.txtvalidity_month != undefined || $scope.txtvalidity_month != null || $scope.txtvalidity_month != '') {
                var lsmonth = $scope.txtvalidity_month + " - Month, ";
            }
            else {
                var lsmonth = '';
            }
            if ($scope.txtvalidity_days != undefined || $scope.txtvalidity_days != null || $scope.v != '') {
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
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;

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
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                personal_accident = parseInt(document.getElementById("acct_insurance").value)
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
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value)
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
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;
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
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                personal_accident = parseInt(document.getElementById("acct_insurance").value)
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
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value)
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
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;

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
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                personal_accident = parseInt(document.getElementById("acct_insurance").value)
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
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value)
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
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;
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
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                personal_accident = parseInt(document.getElementById("acct_insurance").value)
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
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value)
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
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            var personal_accident = 0;

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
            if ($scope.rdbpersonalaccident_collectiontype == 'Collect') {
                personal_accident = parseInt(document.getElementById("acct_insurance").value)
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
            if ($scope.rdbpersonalaccident_collectiontype == 'Deduct') {
                personal_accident_deduct = parseInt(document.getElementById("acct_insurance").value)
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

        $scope.producttype = function () {
            var params = {
                loanproduct_gid: $scope.cboProductTypelist.loanproduct_gid, application_gid: '',
                application2loan_gid: ''
            }
            var url = 'api/AgrMstApplicationAdd/GetLoanSubProduct';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.loansubproductlist = resp.data.application_list;
            });
        }

        $scope.servicedtl_edit = function (application2servicecharge_gid) {
            $location.url('app/AgrTrnCreditServicesDtlEdit?application2servicecharge_gid=' + application2servicecharge_gid + '&application_gid=' + $scope.application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
        }

        $scope.Submit_Trade = function (tradeapplication2loan_gid, trade_gid) {
            var params = {
                application2loan_gid: tradeapplication2loan_gid,
                creditor_gid: $scope.cbowarehouseagency.creditor_gid,
                warehouse_agency: $scope.cbowarehouseagency.Applicant_name,
                warehouse_gid: $scope.cbowarehouse_name.warehouse_gid,
                warehouse_name: $scope.cbowarehouse_name.warehouse_name,
                typeofwarehouse_name: $scope.cbowarehousetype_name,
                volume_uom: $scope.txtvolume_uom,
                totalcapacity_volume: $scope.txtcapacity_volume,
                totalcapacity_area: $scope.txtareacapacity,
                area_uom: $scope.txtareacapacity_uom,
                warehouse2address_gid: $scope.cbowarehouseaddress.warehouse2address_gid,
                warehouse_address: $scope.cbowarehouseaddress.warehouseaddress_name,
                capacity_commodity: $scope.txtcapacity_commodity,
                capacity_panina: $scope.txtcapacity_panina,
                application_gid: $scope.application_gid,
                application2trade_gid: trade_gid
            }
            var url = 'api/AgrMstCreditorMaster/PostEditTrade2WarehouseDetail';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
               
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    tradewarehouselist(tradeapplication2loan_gid, trade_gid);
                    $scope.cbowarehouseagency = '';
                    $scope.cbowarehouse_name = '';
                    $scope.cbowarehousetype_name = '';
                    $scope.txtvolume_uom = '';
                    $scope.txtcapacity_volume = '';
                    $scope.txtareacapacity = '';
                    $scope.txtareacapacity_uom = '';
                    $scope.cbowarehouseaddress = '';
                    $scope.txtcapacity_commodity = '';
                    $scope.txtcapacity_panina = '';
                    $scope.warehousename_list = null;
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

        function tradewarehouselist(tradeapplication2loan_gid, trade_gid) {
            var params = {
                application_gid: $scope.application_gid,
                application2loan_gid: tradeapplication2loan_gid,
                application2trade_gid: trade_gid,
                tmp_status: false
            }

            var url = 'api/AgrMstCreditorMaster/GetEditTrade2WarehouseTmpDetail';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.tradewarehouse_list = resp.data.creditor2warehouse_list;
            });
        }


        function tradecreditorlist(tradeapplication2loan_gid, trade_gid) {
            var params = {

                application_gid: $scope.application_gid,
                application2loan_gid: tradeapplication2loan_gid,
                application2trade_gid: trade_gid,
                tmp_status: false
            }

            var url = 'api/AgrMstCreditorMaster/GetEditTrade2CreditorTmpDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.trade2creditor_list = resp.data.trade2creditor_list;
                $scope.showdtls = false
            });
        }



        $scope.Delete_trade = function (applicationtrade2warehouse_gid) {
            var params =
                {
                    applicationtrade2warehouse_gid: applicationtrade2warehouse_gid
                }
            var url = 'api/AgrMstApplicationAdd/DeleteTrade2WarehouseDetail';
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
                tradedeletewarehouselist();
            });

        }

        function tradedeletewarehouselist() {
            var params = {
                application_gid: $scope.application_gid,
              
                tmp_status: false
            }

            var url = 'api/AgrMstCreditorMaster/GetEditTrade2WarehouseDetail';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.tradewarehouse_list = resp.data.creditor2warehouse_list;
            });
        }


        $scope.Delete_creditor = function (applicationtrade2creditor_gid) {
            var params =
            {
                applicationtrade2creditor_gid: applicationtrade2creditor_gid
            }
            var url = 'api/AgrMstCreditorMaster/DeleteTrade2CreditorDtl';
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
                tradedeletecreditorlist();
            });

        }

        function tradedeletecreditorlist() {
            var params = {

                application_gid: $scope.application_gid,
               
            }

            var url = 'api/AgrMstCreditorMaster/GetEditTrade2CreditorDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.trade2creditor_list = resp.data.trade2creditor_list;
                $scope.showdtls = false
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

    }
})();

