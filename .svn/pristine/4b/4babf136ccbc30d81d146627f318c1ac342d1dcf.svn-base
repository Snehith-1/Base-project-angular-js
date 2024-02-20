(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprApplcreationProductchargesEditController', AgrMstSuprApplcreationProductchargesEditController);

    AgrMstSuprApplcreationProductchargesEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstSuprApplcreationProductchargesEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprApplcreationProductchargesEditController';

        $scope.application_gid = $location.search().lsapplication_gid;
        var lstab = $location.search().lstab;

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
          
            var url = 'api/AgrMstSuprApplicationEdit/GetProductChargesTempClear';
            SocketService.get(url).then(function (resp) {
            });

            $scope.txtinsurance_percent = "0.0017";

            var url = 'api/AgrMstSuprApplicationAdd/GetNatureFormStateofCommodity';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.natureformstateofcommoditydtl = resp.data.NatureFormStateofCommodity;
                unlockUI();
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
                $scope.securitytype_list = resp.data.securitytype_list;
                $scope.programlist = resp.data.programlist;
            });
            var url = 'api/AgrMstSuprApplicationAdd/GetDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.valuechainlist = resp.data.valuechainlist;
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

            var param = {
                application_gid: $scope.application_gid
            };
            var url = 'api/AgrMstSuprApplicationEdit/GetEditLimit';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lbloveralllimit_amount = resp.data.overalllimit_amount;
                $scope.lsloanfacility_amount = resp.data.loanfacility_amount;
                if (resp.data.loanfacility_amount == '' || resp.data.loanfacility_amount == null) {
                    $scope.lsloanfacility_amount = '0';
                }

            });
            var url = 'api/AgrMstSuprApplicationEdit/GetEditLoanDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstloan_list = resp.data.mstloan_list;
                $scope.servicecharges_list = resp.data.servicecharges_list;

                if ($scope.mstloan_list == null) {
                //if ($scope.mstloan_list != null) {

                    $scope.divshow = true;
                }
                else {
                    $scope.divshow = false;
                }

                if ( $scope.servicecharges_list == null) {
                    $scope.servicecharges = true;
                }
                else {
                    $scope.servicecharges = false;
                }

            });

            lockUI();
            var params = {
                application_gid: $scope.application_gid,
                tmp_status: false
            }
            var url = 'api/AgrMstSuprApplicationAdd/GetApplicationTradeList';
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
            var url = 'api/AgrMstSuprApplicationEdit/LoanDetailList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.Loandtl_list = resp.data.mstloan_list;
                $scope.collateral_status = resp.data.collateral_status;
                $scope.buyer_status = resp.data.buyer_status;
            });

            /*       var url = 'api/AgrMstSuprApplicationEdit/BuyerDetailsList';
                   SocketService.getparams(url, param).then(function (resp) {
                       $scope.buyerdtl_list = resp.data.mstbuyer_list;
                   }); */

            var url = 'api/AgrMstSuprApplicationEdit/CollateralDetailsList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.Collateral_list = resp.data.collatertal_list;
            });

            var url = 'api/AgrMstSuprApplicationEdit/HypothecationDetailsList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.Hypothecation_list = resp.data.hypothecation_list;
            });
            var url = 'api/AgrMstSuprApplicationAdd/GetChargeproduct';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.product_list = resp.data.product_list;
            });
            var url = 'api/AgrMstSuprApplicationAdd/GetScopedtl';
            SocketService.get(url).then(function (resp) {
                $scope.scope_list = resp.data.ScopeList;
            });
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstSuprApplicationAdd/GetTradeproduct';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.tradeproduct_list = resp.data.product_list;
            });
            var url = 'api/AgrMstSuprApplicationEdit/GetProductChargesEdit';

            SocketService.getparams(url, param).then(function (resp) {
                $scope.txtvalidityoveralllimit_year = resp.data.validityoveralllimit_year;
                $scope.txtvalidityoveralllimit_month = resp.data.validityoveralllimit_month;
                $scope.txtvalidityoveralllimit_day = resp.data.validityoveralllimit_days;
                $scope.txtcalculationoveralllimit_validity = resp.data.calculationoveralllimit_validity;

                $scope.txtenduse_purpose = resp.data.enduse_purpose;
                $scope.txtprocessing_fee = resp.data.processing_fee;
                if($scope.txtprocessing_fee!=null && $scope.txtprocessing_fee!=undefined && $scope.txtprocessing_fee!="")
                {
                    $scope.Overalllimit_amount_edit = (parseInt($scope.txtprocessing_fee.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.Overalllimit_amount_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount51').innerHTML = $scope.lblamountwords;
                }
                $scope.rdbprocessing_collectiontype = resp.data.processing_collectiontype;
                $scope.txtdoc_charges = resp.data.doc_charges;
                if($scope.txtdoc_charges!=null && $scope.txtdoc_charges!=undefined && $scope.txtdoc_charges!="")
                {
                    $scope.Overalllimit_amount_edit = (parseInt($scope.txtdoc_charges.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.Overalllimit_amount_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount52').innerHTML = $scope.lblamountwords;
                }
                $scope.rdbdoccharge_collectiontype = resp.data.doccharge_collectiontype;
                $scope.txtfieldvisit_charges = resp.data.fieldvisit_charge;
                if($scope.txtfieldvisit_charges!=null && $scope.txtfieldvisit_charges!=undefined && $scope.txtfieldvisit_charges!="")
                {
                    $scope.Overalllimit_amount_edit = (parseInt($scope.txtfieldvisit_charges.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.Overalllimit_amount_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount53').innerHTML = $scope.lblamountwords;
                }
                $scope.rdbfieldvisit_collectiontype = resp.data.fieldvisit_collectiontype;
                $scope.txtadhoc_fee = resp.data.adhoc_fee;
                if($scope.txtadhoc_fee!=null && $scope.txtadhoc_fee!=undefined && $scope.txtadhoc_fee!="")
                {
                    $scope.Overalllimit_amount_edit = (parseInt($scope.txtadhoc_fee.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.Overalllimit_amount_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount54').innerHTML = $scope.lblamountwords;
                }
                $scope.rdbadhoc_collectiontype = resp.data.adhoc_collectiontype;
                $scope.txtlife_insurance = resp.data.life_insurance;
                if($scope.txtlife_insurance!=null && $scope.txtlife_insurance!=undefined && $scope.txtlife_insurance!="")
                {
                    $scope.Overalllimit_amount_edit = (parseInt($scope.txtlife_insurance.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.Overalllimit_amount_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount55').innerHTML = $scope.lblamountwords;
                }
                $scope.rdblifeinsurance_collectiontype = resp.data.lifeinsurance_collectiontype;
                $scope.txtacct_insurance = resp.data.acct_insurance;
                if($scope.txtacct_insurance!=null && $scope.txtacct_insurance!=undefined && $scope.txtacct_insurance!="")
                {
                    $scope.Overalllimit_amount_edit = (parseInt($scope.txtacct_insurance.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.Overalllimit_amount_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount56').innerHTML = $scope.lblamountwords;
                }
                $scope.txttotal_collect = resp.data.total_collect;
                $scope.txttotal_deduct = resp.data.total_deduct;

                $scope.overalllimit_amount = resp.data.overalllimit_amount
                
                if (resp.data.overalllimit_amount != "") {
                    $scope.txtOveralllimit_amount = resp.data.overalllimit_amount
                    if($scope.txtOveralllimit_amount!=null && $scope.txtOveralllimit_amount!=undefined && $scope.txtOveralllimit_amount!="")
                    {
                        $scope.Overalllimit_amount_edit = (parseInt($scope.txtOveralllimit_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.lblamountseperator = (parseInt($scope.Overalllimit_amount_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                        document.getElementById('words_totalamount7').innerHTML = $scope.lblamountwords;
                    }
                }
                $scope.sa_status = resp.data.sa_status;
                if ($scope.sa_status == "Yes")
                    $scope.showsapayout = true;
                else
                    $scope.showsapayout = false;
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

            var params = {
                application2loan_gid: $scope.application2loan_gid,
                application_gid: $scope.application_gid
            }

            var url = 'api/AgrMstSuprApplicationView/GetLoanProgramValueChain';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mstproduct_list = resp.data.mstproductdtl_list;
            });
            var param = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstSuprApplicationAdd/GetLoanDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstloan_list = resp.data.mstloan_list;
                $scope.servicecharges_list = resp.data.servicecharges_list;

                if ($scope.mstloan_list == null) {
                    //$scope.divshow = true;
                    $scope.sameaddproduct = false;
                    $scope.lblproducttype = "";
                    $scope.lblproductsub_type = "";
                }
                else {
                    // $scope.divshow = false;
                    $scope.sameaddproduct = true;
                    $scope.lblproducttype = $scope.mstloan_list[0].product_type;
                    $scope.lblproductsub_type = $scope.mstloan_list[0].productsub_type;
                    $scope.cboProductTypelist = $scope.mstloan_list[0].producttype_gid;
                    $scope.cboProductSubTypelist = $scope.mstloan_list[0].productsubtype_gid;
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
                loanproduct_gid: $scope.cboProductTypelist.loanproduct_gid, application_gid: '',
                application2loan_gid: ''
            }
            var url = 'api/AgrMstSuprApplicationAdd/GetLoanSubProduct';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.loansubproductlist = resp.data.application_list;
            });
        }
        $scope.addLoan = function () {
            $scope.txtnet_yield = "0";
            var txtloanfaility_amount = parseInt($scope.txtloanfaility_amount.replace(/[\s,]+/g, '').trim());
            var lsoveralllimit_amount = parseInt($scope.overalllimit_amount);
            var lsloanfacility_amount = parseInt($scope.lsloanfacility_amount);
           
            
            if ((txtloanfaility_amount) > (lsoveralllimit_amount - lsloanfacility_amount)) {
                Notify.alert('Amount Exceeded from Overall Limit', 'warning');
            }
            else if (($scope.cboSourceType == 'Fixed' || $scope.cboSourceType == 'Moving' || $scope.cboSourceType == 'Property' || $scope.cboSourceType == 'Deposit') && ($scope.txtcolateralobservation_summary == '' || $scope.txtcolateralobservation_summary == null)) {
                Notify.alert('Kindly Fill Observation Summary Detail', 'warning')
            }
            else if ($scope.mstproduct_list == null || $scope.mstproduct_list == undefined || $scope.mstproduct_list == "") {
                Notify.alert('Atleast One Record should be added in Product Details', 'warning')
                return false
            }
            else if ($scope.txtloanfaility_amount == '0') {
                Notify.alert('Proposed Program Limit should not be 0', 'warning')
            }
            //else if ($scope.CollateralDocumentList == null && $scope.loantype == true) {
            //    Notify.alert("Kindly upload the document", {
            //        status: 'warning',
            //        pos: 'top-center',
            //        timeout: 3000
            //    });
            //}
            else {
                if ($scope.txtfacilityreqon_date == null || $scope.txtfacilityreqon_date == '' || $scope.cboProductTypelist == null || $scope.cboProductTypelist == '' || $scope.cboProductSubTypelist == null || $scope.cboProductSubTypelist == '' || $scope.cboLoanTypelist == null || $scope.cboLoanTypelist == '' || $scope.txtloanfaility_amount == null || $scope.txtloanfaility_amount == '' || $scope.txtrate_interest == null || $scope.txtrate_interest == '' || $scope.txtpanel_interest == null || $scope.txtpanel_interest == '' || $scope.cboFacilityTypelist == null || $scope.cboFacilityTypelist == '' || $scope.txttenure_days == null || $scope.txttenure_days == '' || $scope.cboFacilitymodelist == null || $scope.cboFacilitymodelist == '') {
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
                            var lsvariety_gid = '', lsmilestonepaymenttype_gid = '', lsmilestonepaymenttype_name = '';

                            if ($scope.lblproducttype != "") {
                                lsloanproduct_name = $scope.lblproducttype;
                                lsloanproduct_gid = $scope.cboProductTypelist;
                                lsloansubproduct_name = $scope.lblproductsub_type;
                                lsloansubproduct_gid = $scope.cboProductSubTypelist;
                            }
                            else {
                                if ($scope.lsloanproduct_gid != undefined || $scope.cboProductTypelist != null) {
                                    lsloanproduct_name = $scope.cboProductTypelist.loanproduct_name;
                                    lsloanproduct_gid = $scope.cboProductTypelist.loanproduct_gid;
                                }
                                if ($scope.cboProductSubTypelist != undefined || $scope.cboProductSubTypelist != null) {
                                    lsloansubproduct_name = $scope.cboProductSubTypelist.loansubproduct_name;
                                    lsloansubproduct_gid = $scope.cboProductSubTypelist.loansubproduct_gid;
                                }
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
                                lsprogram = $scope.cboProgram.program;
                                lsprogram_gid = $scope.cboProgram.program_gid;
                            }
                            if ($scope.cboproduct_name != undefined || $scope.cboproduct_name != null) {
                                lsproduct_name = $scope.cboproduct_name.product_name;
                                lsproduct_gid = $scope.cboproduct_name.product_gid;
                            }
                            if ($scope.cbovariety_name != undefined || $scope.cbovariety_name != null) {
                                lsvariety_name = $scope.cbovariety_name.variety_name;
                                lsvariety_gid = $scope.cbovariety_name.variety_gid;
                            }
                            if ($scope.cbomilestonepaymenttype != undefined || $scope.cbomilestonepaymenttype != null) {
                                lsmilestonepaymenttype_gid = $scope.cbomilestonepaymenttype.milestonepaymenttype_gid;
                                lsmilestonepaymenttype_name = $scope.cbomilestonepaymenttype.milestonepaymenttype_name;
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
                                product_gid: lsproduct_gid,
                                product_name: lsproduct_name,
                                variety_gid: lsvariety_gid,
                                variety_name: lsvariety_name,
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
                                enduse_purpose: $scope.txtenduse_purpose,
                            }
                            var url = 'api/AgrMstSuprApplicationEdit/PostLoanEditDtl';
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
                                    $scope.txtremaining = '';
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
                                    $scope.mstproduct_list = '';
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
                        var lsvariety_gid = '', lsmilestonepaymenttype_gid = '', lsmilestonepaymenttype_name = '';

                        if ($scope.lblproducttype != "") {
                            lsloanproduct_name = $scope.lblproducttype;
                            lsloanproduct_gid = $scope.cboProductTypelist;
                            lsloansubproduct_name = $scope.lblproductsub_type;
                            lsloansubproduct_gid = $scope.cboProductSubTypelist;
                        }
                        else {
                            if ($scope.lsloanproduct_gid != undefined || $scope.cboProductTypelist != null) {
                                lsloanproduct_name = $scope.cboProductTypelist.loanproduct_name;
                                lsloanproduct_gid = $scope.cboProductTypelist.loanproduct_gid;
                            }
                            if ($scope.cboProductSubTypelist != undefined || $scope.cboProductSubTypelist != null) {
                                lsloansubproduct_name = $scope.cboProductSubTypelist.loansubproduct_name;
                                lsloansubproduct_gid = $scope.cboProductSubTypelist.loansubproduct_gid;
                            }
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
                            lsprogram = $scope.cboProgram.program;
                            lsprogram_gid = $scope.cboProgram.program_gid;
                        }
                        if ($scope.cboproduct_name != undefined || $scope.cboproduct_name != null) {
                            lsproduct_name = $scope.cboproduct_name.product_name;
                            lsproduct_gid = $scope.cboproduct_name.product_gid;
                        }
                        if ($scope.cbovariety_name != undefined || $scope.cbovariety_name != null) {
                            lsvariety_name = $scope.cbovariety_name.variety_name;
                            lsvariety_gid = $scope.cbovariety_name.variety_gid;
                        }
                        if ($scope.cbomilestonepaymenttype != undefined || $scope.cbomilestonepaymenttype != null) {
                            lsmilestonepaymenttype_gid = $scope.cbomilestonepaymenttype.milestonepaymenttype_gid;
                            lsmilestonepaymenttype_name = $scope.cbomilestonepaymenttype.milestonepaymenttype_name;
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
                            product_gid: lsproduct_gid,
                            product_name: lsproduct_name,
                            variety_gid: lsvariety_gid,
                            variety_name: lsvariety_name,
                            sector_name: $scope.txtsector_name,
                            category_name: $scope.txtcategory_name,
                            botanical_name: $scope.txtbotanical_name,
                            alternative_name: $scope.txtalternative_name,
                            alternative_name: $scope.txtalternative_name,
                            milestone_applicability: $scope.rdbmilestone_applicablity,
                            insurance_applicability: $scope.rdbinsurance_applicability,
                            milestonepayment_gid: lsmilestonepaymenttype_gid,
                            milestonepayment_name: lsmilestonepaymenttype_name,
                            sa_payout: $scope.txtsapayout,
                            insurance_availability: $scope.lbloveralllimit_amount,
                            insurance_percent: $scope.txtinsurance_percent,
                            insurance_cost: $scope.txtinsurance_cost,
                            net_yield: $scope.txtnet_yield
                        }
                    }
                    var url = 'api/AgrMstSuprApplicationEdit/PostLoanEditDtl';
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
    
    //$scope.add_loaddetails = function () {
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
    $scope.productchargesloan_edit = function (application2loan_gid, product_gid, variety_gid) {
        $location.url('app/AgrMstSuprApplicationLoanEdit?lsapplication2loan_gid=' + application2loan_gid + '&lstab=edit&lsapplication_gid=' + $scope.application_gid + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
            
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

        var url = 'api/AgrMstSuprApplicationEdit/BuyerTempDetailsList';
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
            var url = 'api/AgrMstSuprApplicationAdd/PostBuyer';
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
                $scope.txteditForcedValueAssessedOn_date =new Date(resp.data.forcedvalueassessed_on);
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
                $scope.txtSecurityAssessededit_date =new Date(resp.data.securityassessed_date);
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
            var url = 'api/AgrMstSuprApplicationAdd/postcollateraldocument';
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

        var url = 'api/AgrMstSuprApplicationAdd/deletecollateraldoc';
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

    $scope.productcharge_Back = function () {
        if (lstab == 'add') {
            $location.url('app/AgrMstSuprApplicationGeneralAdd?lstab=' + lstab);
        }
        else {
            $state.go('app.AgrMstSuprApplicationGeneralEdit');
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
            acctinsurance_collectiontype: $scope.rdbpersonalaccident_collectiontype,
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
            acctinsurance_collectiontype: $scope.rdbpersonalaccident_collectiontype,
            total_collect: $scope.txttotal_collect,
            total_deduct: $scope.txttotal_deduct,
            application_gid: $scope.application_gid
        }
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
            Notify.alert('Amount Exceeded from entered loan facility amount','warning');
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
              producttype_gid: producttype_gid,
              application_gid: $scope.application_gid
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
                fnChargeSummary();
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
        var url = 'api/AgrMstSuprApplicationEdit/PostEditServiceCharges';
        lockUI();
        SocketService.post(url, params).then(function (resp) {
            unlockUI();
            if (resp.data.status == true) {
                fnChargeSummary();
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
            activate();
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
        //if ($scope.txttenure_year != undefined || $scope.txttenure_year != null) {
        //    var lsyear = $scope.txttenure_year + " - Year, ";
        //}
        //else {
        //    var lsyear = '';
        //}
        //if ($scope.txttenure_month != undefined || $scope.txttenure_month != null) {
        //    var lsmonth = $scope.txttenure_month + " - Month, ";
        //}
        //else {
        //    var lsmonth = '';
        //}
        if ($scope.txttenure_days != undefined || $scope.txttenure_days != null) {
            var lsday = $scope.txttenure_days + " - Day";
        }
        else {
            var lsday = '';
        }
        //$scope.txtoveralltenurevalidity_limit = lsyear + lsmonth + lsday;

        $scope.txtoveralltenurevalidity_limit = lsday;

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
    $scope.Other_view = function (application2loan_gid) {
        var modalInstance = $modal.open({
            templateUrl: '/OtherDetails.html',
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
            var url = 'api/AgrMstSuprApplicationView/GetLoantoBuyerList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.buyer_list = resp.data.mstbuyer_list;                    
            });
            var url = 'api/AgrMstSuprApplicationView/GetLoanProgramValueChain';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.program = resp.data.program;
                $scope.primaryvaluechain_name = resp.data.primaryvaluechain_name;
                $scope.secondaryvaluechain_name = resp.data.secondaryvaluechain_name;
                $scope.otherdetails = resp.data;
                //$scope.product_gid = resp.data.product_gid;
                //$scope.product_name = resp.data.product_name;
                //$scope.variety_gid = resp.data.variety_gid;
                //$scope.variety_name = resp.data.variety_name;
                //$scope.sector_name = resp.data.sector_name;
                //$scope.category_name = resp.data.category_name;
                //$scope.botanical_name = resp.data.botanical_name;
                //$scope.alternative_name = resp.data.alternative_name;
                $scope.mstproductdtl_list = resp.data.mstproductdtl_list;
            });

            $scope.ok = function () {
                $modalInstance.close('closed');
            };


        }

    }
    $scope.productdtl_add = function () {
        if (($scope.cboproduct_name == undefined) || ($scope.cboproduct_name == '') || ($scope.cboproduct_name == null) ||
           ($scope.cbovariety_name == undefined) || ($scope.cbovariety_name == undefined) || ($scope.cbovariety_name == '')) {
            Notify.alert('Select Product / Commodity Name', 'warning');
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
                hsn_code: $scope.txthsn_code,
                unitpricevalue_commodity: $scope.txtunitpricevalue_commodity,
                natureformstate_commodity: $scope.txtnatureformstate_commodity.natureformstateofcommodity_name,
                natureformstate_commoditygid: $scope.txtnatureformstate_commodity.natureformstateofcommodity_gid,
                qualityof_commodity: $scope.txtqualityof_commodity,
                quantity: $scope.txtquantity,
                uom_gid: $scope.cbocommodityuom.uom_gid,
                uom_name: $scope.cbocommodityuom.uom_name,
            }
            var url = 'api/AgrMstSuprApplicationAdd/PostProduct';
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
                $scope.txthsn_code = '';
                $scope.txtunitpricevalue_commodity = '';
                $scope.txtnatureformstate_commodity = '';
                $scope.txtqualityof_commodity = '';
                $scope.txtquantity = '';
                $scope.cbocommodityuom = '';
                $scope.varietyname_list = '';
            });
        }
    }

    $scope.product_delete = function (application2product_gid) {
        var params =
            {
                application2product_gid: application2product_gid
            }
        var url = 'api/AgrMstSuprApplicationAdd/DeleteProductDetail';
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


    $scope.tradeproducttype = function () {
        var getselected = $scope.tradeproduct_list.find(function (a) { return a.application2loan_gid === $scope.cboProductTypelist.application2loan_gid })
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

    $scope.tradedtl_edit = function (application2trade_gid) {
        $scope.application2trade_gid = application2trade_gid;
        $scope.TradeEditdivshow = true;
        var params = {
            application2trade_gid: application2trade_gid
        }
        lockUI();
        var url = 'api/AgrMstSuprApplicationAdd/GetApplicationTradeViewdtl';
        SocketService.getparams(url, params).then(function (resp) {
            $scope.cboeditproduct_type = resp.data.producttype_name;
            $scope.txteditProductsubType = resp.data.productsubtype_name;
            $scope.rdbeditsalescontract_availability = resp.data.salescontract_availability;
            $scope.cboeditScopeoftransport = resp.data.scopeof_transportgid;
            $scope.cboeditScopeofloading = resp.data.scopeof_loadinggid;
            $scope.cboeditScopeofunloading = resp.data.scopeof_unloadinggid;
            $scope.cboeditScopeofqualityandquantity = resp.data.scopeof_qualityandquantitygid;
            $scope.cboeditScopeofmoisturegainloss = resp.data.scopeof_moisturegainlossgid;
            unlockUI();
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
        var gettransport = $scope.scope_list.find(function (a) { return a.scope_gid == $scope.cboeditScopeoftransport })
        if (gettransport != null) {
            lstransport_name = gettransport.scope_name;
        }
        var getloading = $scope.scope_list.find(function (a) { return a.scope_gid == $scope.cboeditScopeofloading })
        if (getloading != null) {
            lsloading_name = getloading.scope_name;
        }
        var getunloading = $scope.scope_list.find(function (a) { return a.scope_gid == $scope.cboeditScopeofunloading })
        if (getunloading != null) {
            lsunloading_name = getunloading.scope_name;
        }
        var getqualityandquantity = $scope.scope_list.find(function (a) { return a.scope_gid == $scope.cboeditScopeofqualityandquantity })
        if (getqualityandquantity != null) {
            lsqualityandquantity_name = getqualityandquantity.scope_name;
        }
        var getmoisturegainloss = $scope.scope_list.find(function (a) { return a.scope_gid == $scope.cboeditScopeofmoisturegainloss})
        if (getmoisturegainloss != null) {
            lsmoisturegainloss_name = getmoisturegainloss.scope_name;
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
            application_gid: $scope.application_gid,
            tmpadd_status: false,
            application2trade_gid: $scope.application2trade_gid
        }
        var url = 'api/AgrMstSuprApplicationAdd/UpdateTradeDtl';
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
        var url = 'api/AgrMstSuprApplicationAdd/PostTradedtl';
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
        var url = 'api/AgrMstSuprApplicationAdd/GetApplicationTradeList';
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
        var url = 'api/AgrMstSuprApplicationAdd/GetTradeproduct';
        SocketService.getparams(url, params).then(function (resp) {
            unlockUI();
            $scope.tradeproduct_list = resp.data.product_list;
        });
    }

    $scope.DeleteTrade = function (application2trade_gid) {
        var params = {
            application2trade_gid: application2trade_gid
        }
        var url = 'api/AgrMstSuprApplicationAdd/DeleteTradeDtl';
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

    function fnChargeSummary() {
        lockUI();
        var params = {
            application_gid: $scope.application_gid
        }
        var url = 'api/AgrMstApplicationAdd/GetChargeproduct';
        SocketService.getparams(url, params).then(function (resp) {
            unlockUI();
            $scope.product_list = resp.data.product_list;
        });
    }

    $scope.downloadall = function () {
        for (var i = 0; i < $scope.CollateralDocumentList.length; i++) {
            DownloaddocumentService.Downloaddocument($scope.CollateralDocumentList[i].document_path, $scope.CollateralDocumentList[i].document_name);
        }
    }
}
})();

