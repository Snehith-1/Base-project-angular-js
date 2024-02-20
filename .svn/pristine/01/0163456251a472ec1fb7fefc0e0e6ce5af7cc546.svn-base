(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstProductsandChargesTabController', AgrMstProductsandChargesTabController);

    AgrMstProductsandChargesTabController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function AgrMstProductsandChargesTabController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstProductsandChargesTabController';
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
            vm.calender17 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open17 = true;
            };
            vm.calender18 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open18 = true;
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
            }; lockUI();
            var url = 'api/AgrMstApplicationAdd/GetproductDropDown';
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

            $scope.loantype_change = function (cboLoanTypelist) {
                if ($scope.cboLoanTypelist.loan_type == 'Secured') {
                    $scope.loantype = true;

                }
                else {

                    $scope.loantype = false;
                }
            }

            var url = 'api/AgrMstApplicationAdd/GetNatureFormStateofCommodity';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.natureformstateofcommoditydtl = resp.data.NatureFormStateofCommodity;
                unlockUI();
            });

            var url = 'api/AgrMstApplicationAdd/GetDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.productname_list = resp.data.productname_list;
                $scope.valuechainlist = resp.data.valuechainlist;
            });
            var url = 'api/AgrMstApplicationAdd/Getmilestonepaymentdtl';
            SocketService.get(url).then(function (resp) {
                $scope.milestonepaymentlist = resp.data.milestonepayment;
            });
            var url = 'api/AgrMstApplicationEdit/GetSupplierNameDropdown';
            SocketService.get(url).then(function (resp) {
                $scope.Supplierdtllist = resp.data.MdlSupplierDropdowndtl;
            });
          
            var url = 'api/AgrMstApplicationAdd/ProductchargesTmpClear';
            SocketService.get(url).then(function (resp) {
            });
            var param = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationAdd/GetLoanDtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstloan_list = resp.data.mstloan_list;
                $scope.servicecharges_list = resp.data.servicecharges_list;

                if ($scope.mstloan_list == null) {
                    $scope.sameaddproduct = false;
                    $scope.lblproducttype = "";
                    $scope.lblproductsub_type = "";
                }
                else {
                    $scope.sameaddproduct = true;
                    $scope.lblproducttype = $scope.mstloan_list[0].product_type;
                    $scope.lblproductsub_type = $scope.mstloan_list[0].productsub_type;
                    if ($scope.lblproductsub_type == 'STF') {
                        $scope.stfmandatory = true;
                        $scope.STFdivshow = true;
                    }
                    else {
                        $scope.stfmandatory = false;
                        $scope.STFdivshow = false;
                    }
                    $scope.cboProductTypelist = $scope.mstloan_list[0].producttype_gid;
                    $scope.cboProductSubTypelist = $scope.mstloan_list[0].productsubtype_gid;
                } 

            });

            var url = 'api/AgrMstApplicationAdd/GetLimit';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.lbloveralllimit_amount = resp.data.overalllimit_amount;
                $scope.lsloanfacility_amount = resp.data.loanfacility_amount;
                if (resp.data.loanfacility_amount == '' || resp.data.loanfacility_amount == null) {
                    $scope.lsloanfacility_amount = '0';
                }
                $scope.sa_status = resp.data.sa_status;
                if ($scope.sa_status == "Yes")
                    $scope.showsapayout = true;
                else
                    $scope.showsapayout = false;

            });
            var url = 'api/AgrMstApplicationAdd/GetChargeproduct';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.product_list = resp.data.product_list;
            });
            var url = 'api/AgrMstApplicationAdd/GetOverallInfo';
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

            //var url = 'api/AgrMstApplicationAdd/GetAppProductcharges';
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
            var url = 'api/AgrMstApplicationAdd/GetProceed';
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

            var url = 'api/AgrMstApplicationAdd/GetApprovalHierarchyFlag';
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

            $scope.txtinsurance_percent = "0.0017";
            var url = 'api/AgrMstUom/GetuomList';
            SocketService.get(url).then(function (resp) {
                $scope.uom_list = resp.data.Uomdtl;
            });
            var url = 'api/AgrMstApplicationAdd/Getmilestonepaymentdtl';
            SocketService.get(url).then(function (resp) {
                $scope.milestonepaymentlist = resp.data.milestonepayment;
            });
        }

        $scope.OverallLimitValidity = function () {
            var getoverallvalidity = cmnfunctionService.fnDatediff($scope.txtvalidity_from, $scope.txtvalidity_to)
            if (getoverallvalidity == '0') {
                Notify.alert('Validity To Date Should be Greater than Validity From Date', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else if (getoverallvalidity != '1') {
                $scope.txtcalculationoveralllimit_validity = getoverallvalidity;
            }
        }

        $scope.productname_change = function (cboproduct_name) {
            var params = {
                product_gid: $scope.cboproduct_name.product_gid
            }
            var url = 'api/AgrMstApplicationAdd/GetSectorcategory';
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
            var url = 'api/AgrMstApplicationAdd/GetVarietyDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.product_gid = resp.data.product_gid;
                $scope.variety_gid = resp.data.variety_gid;
                $scope.variety_name = resp.data.variety_name;
                $scope.txtbotanical_name = resp.data.botanical_name;
                $scope.txtalternative_name = resp.data.alternative_name;
                $scope.txthsn_code = resp.data.hsn_code;
                $scope.txttypeofsupplynature_name = resp.data.typeofsupplynature_name;
                $scope.lbltypeofsupplynature_gid = resp.data.typeofsupplynature_gid;
                $scope.lblsectorclassification_gid = resp.data.sectorclassification_gid;
                $scope.txtsectorclassification_name = resp.data.sectorclassification_name;
                $scope.txtproduct_name = resp.data.product_name;
                $scope.txtsector = resp.data.sector;
                $scope.txtcategory = resp.data.category;
                $scope.txtHeadingdesc_product = resp.data.headingdesc_product;
                GetVarietydetails($scope.cbovariety_name.variety_gid);
            });
            $scope.txtbotanical_name = '';
            $scope.txtalternative_name = '';
        }

        $scope.overalllimit = function () {
            var application_gid = $scope.application_gid;
            $location.url('app/AgrMstApplcreationProductchargesEdit?lsapplication_gid=' + application_gid + '&lstab=add');
        }

        $scope.hypothecationdtl = function () {
            var application_gid = $scope.application_gid;
            $location.url('app/AgrMstApplicationHypothecationEdit?lsapplication_gid=' + application_gid + '&lstab=add');
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
                    //validityoveralllimit_year: $scope.txtvalidityoveralllimit_year,
                    //validityoveralllimit_month: $scope.txtvalidityoveralllimit_month,
                    //validityoveralllimit_days: $scope.txtvalidityoveralllimit_day,
                    validityfrom_date: $scope.txtvalidity_from,
                    validityto_date: $scope.txtvalidity_to,
                    calculationoveralllimit_validity: $scope.txtcalculationoveralllimit_validity,
                    application_gid: $scope.application_gid,
                }

                var url = 'api/AgrMstApplicationAdd/SubmitOverallLimit';
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
                            $location.url('app/AgrMstApplicationProductChargesAdd?lsapplication_gid=' + $scope.application_gid + '&lstab=add');
                        }
                        else {
                            $location.url('app/AgrMstApplicationGeneralEdit');
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
                //else if ($scope.DocumentList == null && $scope.loantype == true) {
                //    Notify.alert("Kindly upload the document", {
                //        status: 'warning',
                //        pos: 'top-center',
                //        timeout: 3000
                //    });
                //}

            else {
                if ($scope.txtfacilityreqon_date == null || $scope.txtfacilityreqon_date == '' || $scope.cboProductTypelist == null || $scope.cboProductTypelist == '' ||
                    $scope.cboProductSubTypelist == null || $scope.cboProductSubTypelist == '' || $scope.cboLoanTypelist == null || $scope.cboLoanTypelist == '' ||
                    $scope.txtloanfaility_amount == null || $scope.txtloanfaility_amount == '' || $scope.txtrate_interest == null || $scope.txtrate_interest == '' ||
                    $scope.txtpanel_interest == null || $scope.txtpanel_interest == '' || 
                    $scope.cboFacilityTypelist == null || $scope.cboFacilityTypelist == '' ||
                    $scope.txttenure_days == null || $scope.txttenure_days == '' || $scope.cboFacilitymodelist == null || $scope.cboFacilitymodelist == '') {
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
                                programlimit_validdfrom: $scope.txtProgramLimitValidityfrom,
                                programlimit_validdto: $scope.txtProgramLimitValidityTo,
                                programoverall_limit: $scope.txtoverallprogramvalidity_limit,
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
                                alternative_name: $scope.txtalternative_name,
                                trade_orginatedby: $scope.rdbTradeOriginated,
                                SA_Brokerage: $scope.txtsabrokerage,
                                holding_periods: $scope.txtholdingperiod,
                                holdingmonthly_procurement: $scope.txtholdingMonthlyprocurement,
                                extendedholding_periods: $scope.txtextendedholdingperiod,
                                extendedmonthly_procurement: $scope.txtextendedMonthlyprocurement,
                                charges_extendedperiod: $scope.txtcharges_extendedperiod,
                                customer_advance: $scope.txtcustomer_advance,
                                reimburesementof_expenses: $scope.txtreimburesementof_expenses,
                                reimburesementof_expensespenalty: $scope.txtreimburesementof_expensespenalty,
                                bankfundingdata_filename: $scope.bankfunding_documentname,
                                bankfundingdata_filepath: $scope.bankfunding_documentpath,
                                needfor_stocking: $scope.txtneedfor_stocking,
                                product_portfolio: $scope.txtproduct_portfolio,
                                production_capacity: $scope.txtproduction_capacity,
                                natureof_operations: $scope.txtnatureof_operations,
                                averagemonthly_inventoryholding: $scope.txtaveragemonthly_inventoryholding,
                                financialinstitutions_relationship: $scope.txtfinancialinstitutions_relationship,
                            }
                            var url = 'api/AgrMstApplicationAdd/PostLoanDtl';
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
                                    $scope.rdbTradeOriginated = '';
                                    $scope.txtProgramLimitValidityTo = '';
                                    $scope.txtProgramLimitValidityfrom = '';
                                    $scope.txtoverallprogramvalidity_limit = '';
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
                                    $scope.txtsabrokerage = '';
                                    $scope.txtholdingperiod = '';
                                    $scope.txtholdingMonthlyprocurement = '';
                                    $scope.txtextendedholdingperiod = '';
                                    $scope.txtextendedMonthlyprocurement = '';
                                    $scope.txtcharges_extendedperiod = '';
                                    $scope.txtcustomer_advance = '';
                                    $scope.txtreimburesementof_expenses = '';
                                    $scope.txtreimburesementof_expensespenalty = '';
                                    $scope.bankfunding_documentname = '';
                                    $scope.bankfunding_documentpath = '';
                                    $scope.txtneedfor_stocking = '';
                                    $scope.txtproduct_portfolio = '';
                                    $scope.txtproduction_capacity = '';
                                    $scope.txtnatureof_operations = '';
                                    $scope.txtaveragemonthly_inventoryholding = '';
                                    $scope.txtfinancialinstitutions_relationship = '';
                                    $scope.MdlrePaymentdtl = '';
                                    $scope.MdlSupplierdtllist = '';
                                    $scope.MdlSupplierPaymentlist = '';
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
                            programlimit_validdfrom: $scope.txtProgramLimitValidityfrom,
                            programlimit_validdto: $scope.txtProgramLimitValidityTo,
                            programoverall_limit: $scope.txtoverallprogramvalidity_limit,
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
                            alternative_name: $scope.txtalternative_name,
                            trade_orginatedby: $scope.rdbTradeOriginated,
                            SA_Brokerage: $scope.txtsabrokerage,
                            holding_periods: $scope.txtholdingperiod,
                            holdingmonthly_procurement: $scope.txtholdingMonthlyprocurement,
                            extendedholding_periods: $scope.txtextendedholdingperiod,
                            extendedmonthly_procurement: $scope.txtextendedMonthlyprocurement,
                            charges_extendedperiod: $scope.txtcharges_extendedperiod,
                            customer_advance: $scope.txtcustomer_advance,
                            reimburesementof_expenses: $scope.txtreimburesementof_expenses,
                            reimburesementof_expensespenalty: $scope.txtreimburesementof_expensespenalty,
                            bankfundingdata_filename: $scope.bankfunding_documentname,
                            bankfundingdata_filepath: $scope.bankfunding_documentpath,
                            needfor_stocking: $scope.txtneedfor_stocking,
                            product_portfolio: $scope.txtproduct_portfolio,
                            production_capacity: $scope.txtproduction_capacity,
                            natureof_operations: $scope.txtnatureof_operations,
                            averagemonthly_inventoryholding: $scope.txtaveragemonthly_inventoryholding,
                            financialinstitutions_relationship: $scope.txtfinancialinstitutions_relationship,
                        }
                        var url = 'api/AgrMstApplicationAdd/PostLoanDtl';
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
                                $scope.rdbTradeOriginated = '';
                                $scope.txtProgramLimitValidityTo = '';
                                $scope.txtProgramLimitValidityfrom = '';
                                $scope.txtoverallprogramvalidity_limit = '';
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
                                $scope.txtsabrokerage = '';
                                $scope.txtholdingperiod = '';
                                $scope.txtholdingMonthlyprocurement = '';
                                $scope.txtextendedholdingperiod = '';
                                $scope.txtextendedMonthlyprocurement = '';
                                $scope.txtcharges_extendedperiod = '';
                                $scope.txtcustomer_advance = '';
                                $scope.txtreimburesementof_expenses = '';
                                $scope.txtreimburesementof_expensespenalty = '';
                                $scope.bankfunding_documentname = '';
                                $scope.bankfunding_documentpath = '';
                                $scope.txtneedfor_stocking = '';
                                $scope.txtproduct_portfolio = '';
                                $scope.txtproduction_capacity = '';
                                $scope.txtnatureof_operations = '';
                                $scope.txtaveragemonthly_inventoryholding = '';
                                $scope.txtfinancialinstitutions_relationship = '';
                                $scope.MdlrePaymentdtl = '';
                                $scope.MdlSupplierdtllist = '';
                                $scope.MdlSupplierPaymentlist = '';
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
        $scope.Deleteloan = function (application2loan_gid, producttype_gid) {
            var params =
              {
                  application2loan_gid: application2loan_gid,
                  producttype_gid: producttype_gid,
                  application_gid: $scope.application_gid
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

        $scope.buyer = function () {
            var params = {
                buyer_gid: $scope.cboBuyer.buyer_gid,
            }
            var url = 'api/AgrMstApplicationAdd/GetBuyerInfo';
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
        $scope.Deletebuyer = function (application2buyer_gid) {
            var params =
              {
                  application2buyer_gid: application2buyer_gid
              }
            var url = 'api/AgrMstApplicationAdd/DeleteBuyer';
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
            var url = 'api/AgrMstApplicationAdd/DeleteCharge';
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
                frm.append('project_flag', "documentformatonly");
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cboDocumentTitle);
                $scope.uploadfrm = frm;
                var url = 'api/AgrMstApplicationAdd/postcollateraldocument';
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

            var url = 'api/AgrMstApplicationAdd/deletecollateraldoc';
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
                var url = 'api/AgrMstApplicationAdd/PostCollateral';
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
            var url = 'api/AgrMstApplicationAdd/DeleteCollateral';
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
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                var url = 'api/AgrMstApplicationAdd/PostHypoDoc';
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

            var url = 'api/AgrMstApplicationAdd/deleteHypoDoc';
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
                //else if ($scope.hypo_documentList == null) {
                //    Notify.alert("Kindly upload the document", {
                //        status: 'warning',
                //        pos: 'top-center',
                //        timeout: 3000
                //    });
                //}
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
                var url = 'api/AgrMstApplicationAdd/PostHypothecation';
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
                            $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + lstab);
                        }
                        else {
                            $location.url('app/AgrMstApplicationGeneralEdit');
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
            var url = 'api/AgrMstApplicationAdd/DeleteHypothecation';
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
                acctinsurance_collectiontype: $scope.rdbpersonalaccident_collectiontype,
                acct_insurance: $scope.txtacct_insurance,
                total_collect: $scope.txttotal_collect,
                total_deduct: $scope.txttotal_deduct,
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationAdd/PostProductCharges';
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
            var url = 'api/AgrMstApplicationAdd/PostSubmitProductCharges';
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
                acctinsurance_collectiontype: $scope.rdbpersonalaccident_collectiontype,
                acct_insurance: $scope.txtacct_insurance,
                total_collect: document.getElementById("total_collect").value,
                total_deduct: document.getElementById("total_deduct").value,
                productTypelist: $scope.cboProductTypelist,
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationAdd/PostServiceCharges';
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
                    fnChargeSummary();
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
                activate();
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
            if ($scope.txtcommoditycreditvalidity_year != undefined || $scope.txtcommoditycreditvalidity_year != null) {
                var lsyear = $scope.txtcommoditycreditvalidity_year + " - Year, ";
            }
            else {
                var lsyear = '';
            }
            if ($scope.txtcommoditycreditvalidity_month != undefined || $scope.txtcommoditycreditvalidity_month != null) {
                var lsmonth = $scope.txtcommoditycreditvalidity_month + " - Month, ";
            }
            else {
                var lsmonth = '';
            }
            if ($scope.txtcommoditycreditvalidity_days != undefined || $scope.txtcommoditycreditvalidity_days != null) {
                var lsday = $scope.txtcommoditycreditvalidity_days + " - Day";
            }
            else {
                var lsday = '';
            }
            $scope.txtoverallcommoditycreditvalidity_limit = lsyear + lsmonth + lsday;
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

        $scope.general_Tab = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationGeneralAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
            }
            else {
                $scope.Company_dtls = true;
            }
        }

        $scope.company_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationInstitutionAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
            }
            else {
                $scope.Company_dtls = true;
            }
        }

        $scope.Individual_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationIndividualAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
            }
            else {
                $scope.Individual_dtls = true;
            }
        }

        $scope.Group_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationGroupAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
            }
            else {
                $scope.Group_dtls = true;
            }
        }

        $scope.EconomicCapital_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationSocialTradeCapitalAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
            }
            else {
                $scope.EconomicCapital_dtls = true;
            }
        }

        $scope.OverallLimit_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.OverallLimit_dtls = true;
            }
            else {
                $location.url('app/AgrMstApplicationOverallLimitAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
            }
        }

        $scope.ProductCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.ProductCharges_dtls = true;
            }
            else {
                $location.url('app/AgrMstApplicationProductChargesAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
            }
        }

        $scope.ServiceCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.ServiceCharges_dtls = true;
            }
            else {
                $location.url('app/AgrMstApplicationServiceChargeAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
            }
        }

        $scope.Hypothecation_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/AgrMstApplicationHypothecationAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
            }
        }

        $scope.BureauUpdates_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationCICUploadAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
            }
            else {
                $scope.BureauUpdates_dtls = true;
            }
        }

        $scope.doneclick = function () {
            lockUI();
            var url = 'api/AgrMstApplicationAdd/GetGeneralInfo';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.application_status = resp.data.application_status;
            });
            lockUI();
            var url = 'api/AgrMstApplicationAdd/GetProceed';
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

            var url = 'api/AgrMstApplicationAdd/GetApprovalHierarchyFlag';
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
            $state.go('app.AgrMstApplicationCreationSummary');
        }
        $scope.overallsubmit_application = function () {

            var params = {

            }
            var url = 'api/AgrMstApplicationAdd/PostAppProceed';
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
                $state.go('app.AgrMstApplicationCreationSummary');
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

                var url = 'api/AgrMstApplicationAdd/FnKycDcoumentValidation';
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


                var url = 'api/AgrMstApplicationAdd/GetApprovalHierarchyChangeList';
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
                    var url = 'api/AgrMstApplicationAdd/UpdateApprovalHierarchyChange';
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
            var lsmilestonepaymenttype_gid = "", lsmilestonepaymenttype_name = "";
            $scope.txtcommoditynet_yield = "0";
            $scope.txtnet_yield = "0";
            if (($scope.cboproduct_name == undefined) || ($scope.cboproduct_name == '') || ($scope.cboproduct_name == null) ||
               ($scope.cbovariety_name == undefined) || ($scope.cbovariety_name == undefined) || ($scope.cbovariety_name == '')) {
                Notify.alert('Select Product / Commodity Name', 'warning');
            }
            else if ($scope.showmilestonepaymenttype == true && ($scope.cbomilestonepaymenttype == null || $scope.cbomilestonepaymenttype == "")) {
                Notify.alert('Kindly Select Milestone payment Type', 'warning');
            }
            else if ($scope.showsapayout == true && ($scope.txtsapayout == "" || $scope.txtsapayout == null)) {
                Notify.alert('Kindly fill SA Payout (%)', 'warning');
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
                if ($scope.cbomilestonepaymenttype != undefined || $scope.cbomilestonepaymenttype != null) {
                    lsmilestonepaymenttype_gid = $scope.cbomilestonepaymenttype.milestonepaymenttype_gid;
                    lsmilestonepaymenttype_name = $scope.cbomilestonepaymenttype.milestonepaymenttype_name;
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
                    milestone_applicability: $scope.rdbmilestone_applicablity,
                    insurance_applicability: $scope.rdbinsurance_applicability,
                    milestonepayment_gid: lsmilestonepaymenttype_gid,
                    milestonepayment_name: lsmilestonepaymenttype_name,
                    sa_payout: $scope.txtsapayout,
                    insurance_availability: $scope.lbloveralllimit_amount,
                    insurance_percent: $scope.txtinsurance_percent,
                    insurance_cost: $scope.txtinsurance_cost,
                    net_yield: $scope.txtnet_yield,
                    markto_marketvalue: $scope.txtmarkto_marketvalue,
                    pricereference_source: $scope.txtpricereference_source,
                    headingdesc_product: $scope.txtHeadingdesc_product,
                    typeofsupply_naturegid: $scope.lbltypeofsupplynature_gid,
                    typeofsupply_naturename: $scope.txttypeofsupplynature_name,
                    sectorclassification_gid: $scope.lblsectorclassification_gid,
                    sectorclassification_name: $scope.txtsectorclassification_name,
                    creditperiod_years: $scope.txtcommoditycreditvalidity_year,
                    creditperiod_months: $scope.txtcommoditycreditvalidity_month,
                    creditperiod_days: $scope.txtcommoditycreditvalidity_days,
                    overallcreditperiod_limit: $scope.txtoverallcommoditycreditvalidity_limit,
                    commodity_margin: $scope.txtcommodity_margin,
                    commoditynet_yield: $scope.txtcommoditynet_yield
                }
                var url = 'api/AgrMstApplicationAdd/PostProduct';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                   
                    if (resp.data.status == true) {
                        unlockUI();
                        $scope.mstproduct_list = resp.data.mstproduct_list;
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
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
                        $scope.rdbmilestone_applicablity = '';
                        $scope.rdbinsurance_applicability = '';
                        $scope.cbomilestonepaymenttype = '';
                        $scope.txtsapayout = '';
                        $scope.txtinsurance_percent = '';
                        $scope.txtinsurance_cost = '';
                        $scope.txtnet_yield = '';
                        $scope.txtmarkto_marketvalue = '';
                        $scope.txtpricereference_source = '';
                        $scope.txtHeadingdesc_product = '';
                        $scope.lbltypeofsupplynature_gid = '';
                        $scope.txttypeofsupplynature_name = '';
                        $scope.lblsectorclassification_gid = '';
                        $scope.txtsectorclassification_name = '';
                        $scope.txtcommoditycreditvalidity_year = '';
                        $scope.txtcommoditycreditvalidity_month = '';
                        $scope.txtcommoditycreditvalidity_days = '';
                        $scope.txtoverallcommoditycreditvalidity_limit = '';
                        $scope.txtcommodity_margin = '';
                        $scope.txtcommoditynet_yield = '';
                        $scope.commodityDocumentUpload = '';
                        $scope.commodityTradeProdctlist = '';
                        $scope.commoditygststatuslist = '';
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    } 
                });
            }
        }

        $scope.product_delete = function (application2product_gid) {
            var params =
                {
                    application2product_gid: application2product_gid
                }
            var url = 'api/AgrMstApplicationAdd/DeleteProductDetail';
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

        $scope.Changeinsuranceapplicability = function () {
            if ($scope.rdbinsurance_applicability == "Yes") {
                $scope.showmilestonepaymenttype = true;
                $scope.disabledmilestonepaymenttype = false;
            }
            else {
                $scope.cbomilestonepaymenttype = "";
                $scope.showmilestonepaymenttype = false;
                $scope.disabledmilestonepaymenttype = true;
            }
        }

        $scope.Tradeclick = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Trade_dtls = true;
            }
            else {
                $location.url('app/AgrMstAppEditTradeAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
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

        function GetVarietydetails(variety_gid) {
            lockUI();
            var params = {
                variety_gid: variety_gid
            }
            var url = 'api/AgrMstSamAgroMaster/GetCommodityGstList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.commoditygststatuslist = resp.data.commoditygststatus;
            });
            var url = 'api/AgrMstSamAgroMaster/GetCommodityTradeProdctList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.commodityTradeProdctlist = resp.data.commodityTradeProdct;
            });
            var url = 'api/AgrMstSamAgroMaster/GetCommodityDocumentUploadList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.commodityDocumentUpload = resp.data.commodityDocumentUpload;
                unlockUI();
            });
        }

        $scope.ProgramLimitValidity = function () {
            var getoverallvalidity = cmnfunctionService.fnDatediff($scope.txtProgramLimitValidityfrom, $scope.txtProgramLimitValidityTo)
            if (getoverallvalidity == '0') {
                Notify.alert('Credit Period Validity To Date Should be Greater than Validity From Date', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else if (getoverallvalidity != '1') {
                $scope.txtoverallprogramvalidity_limit = getoverallvalidity;
            }
        }

        $scope.doc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.suppliername_change = function () {
            var params = {
                application_gid: $scope.cbosupplier_name.supplier_gid
            }
            var url = 'api/AgrMstApplicationEdit/GetSupplierNameDtlDropdown';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.txtsupplier_address = resp.data.supplier_address;
                    $scope.txtsupplieremail_id = resp.data.supplier_emailid;
                    $scope.txtsupplierphone_number = resp.data.supplier_phoneno;
                    $scope.MdlSupplierGSTdtllist = resp.data.MdlSupplierGSTdtl;
                    $scope.txtsupplierpan_details = resp.data.supplier_pandtl;
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });


        }

        $scope.suppliergstview = function (MdlSupplierGSTdtllist) {
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

        $scope.suppliergsttrnview = function (apploan2supplierdtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/SupplierGSTDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var params = {
                    apploan2supplierdtl_gid: apploan2supplierdtl_gid,
                }
                var url = 'api/AgrMstApplicationEdit/GetLoan2SupplierGSTdtl';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        $scope.SupplierGSTdtl_list = resp.data.MdlSupplierGSTdtl;
                    } else {
                        unlockUI();
                    }

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.supplierdtl_add = function () {
            var lsmilestonepaymenttype_gid = '', lsmilestonepaymenttype_name = '';
            if ($scope.cbosuppliermilestonepaymenttype != undefined || $scope.cbosuppliermilestonepaymenttype != null) {
                lsmilestonepaymenttype_gid = $scope.cbosuppliermilestonepaymenttype.milestonepaymenttype_gid;
                lsmilestonepaymenttype_name = $scope.cbosuppliermilestonepaymenttype.milestonepaymenttype_name;
            }
            var params = {
                application_gid: $scope.application_gid,
                supplier_gid: $scope.cbosupplier_name.supplier_gid,
                supplier_name: $scope.cbosupplier_name.supplier_name,
                supplier_address: $scope.txtsupplier_address,
                supplier_emailid: $scope.txtsupplieremail_id,
                supplier_phoneno: $scope.txtsupplierphone_number,
                MdlSupplierGSTdtl: $scope.MdlSupplierGSTdtllist,
                supplier_pandtl: $scope.txtsupplierpan_details,
                milestone_applicable: $scope.rdbsupplierMilestoneApplicable,
                milestonepaymenttype_gid: lsmilestonepaymenttype_gid,
                milestonepaymenttype_name: lsmilestonepaymenttype_name,
                supplier_vintage: $scope.txtvintagesupplier_buyer,
                tmpadd_status: true,
            }
            var url = 'api/AgrMstApplicationEdit/PostLoan2Supplierdtl';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    fnsupplierdtlbothlist();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                    $scope.cbosupplier_name = '';
                    $scope.txtsupplier_address = '';
                    $scope.txtsupplieremail_id = '';
                    $scope.txtsupplierphone_number = '';
                    $scope.MdlSupplierGSTdtllist = '';
                    $scope.txtsupplierpan_details = '';
                    $scope.rdbsupplierMilestoneApplicable = '';
                    $scope.txtvintagesupplier_buyer = '';
                    $scope.cbosuppliermilestonepaymenttype = '';
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
        }

        $scope.supplierdtl_delete = function (apploan2supplierdtl_gid) {

            var params = {
                apploan2supplierdtl_gid: apploan2supplierdtl_gid
            }
            var url = 'api/AgrMstApplicationEdit/DeleteSupplierDtl';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    fnsupplierdtlbothlist();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        function fnsupplierdtlbothlist() {
            lockUI();
            var params = {
                application_gid: $scope.application_gid,
                application2loan_gid: '',
                tmp_status: 'both',
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
        }

        $scope.uploadbankfunding_doc = function (val, val1, name) {
            var frm = new FormData();
            for (var i = 0; i < val.length; i++) {
                var item = {
                    name: val[i].name,
                    file: val[i]
                };
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");

                        if (IsValidExtension == false) {
                            Notify.alert("File format is not supported..!", {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            return false;
                        }
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('application_gid', $scope.application_gid);
                frm.append('project_flag', "documentformatonly");
            }
            if (frm != undefined) {

                var url = 'api/AgrMstApplicationEdit/PostBankFundingUpload';
                lockUI();
                SocketService.postFile(url, frm).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $("#Bankfundingfile").val('');
                        $scope.bankfunding_tmpdocumentGid = resp.data.tmp_documentGid;
                        $scope.bankfunding_documentname = resp.data.document_name;
                        $scope.bankfunding_documentpath = resp.data.document_path;
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
                });
            }
            else {
                alert('Please select a file.')
            }

        }

        $scope.canceltmpbankfunding = function (bankfunding_tmpdocumentGid) {

            var params = {
                bankfundingdataupload_gid: bankfunding_tmpdocumentGid
            }
            var url = 'api/AgrMstApplicationEdit/DeleteBankFundingUpload';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.bankfunding_tmpdocumentGid = '';
                    $scope.bankfunding_documentname = '';
                    $scope.bankfunding_documentpath = '';
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.Paymentdisbursementadd = function () {
            var commodity_name = '';
            if ($scope.cbodisbursementcommodity != null) {
                commodity_name = $scope.cbodisbursementcommodity.product_name + ' / ' + $scope.cbodisbursementcommodity.variety_name
            }
            var params = {
                application_gid: $scope.application_gid,
                commodity_gid: $scope.cbodisbursementcommodity.application2product_gid,
                commodity_name: commodity_name,
                supplierpayment_type: $scope.cbodisbursementPaymentlist.milestonepaymenttype_name,
                supplierpayment_typegid: $scope.cbodisbursementPaymentlist.milestonepaymenttype_gid,
                maxpercent_paymentterm: $scope.txtdisbursementmaxpercent_paymentterm,
                tmpadd_status: true,
            }
            var url = 'api/AgrMstApplicationEdit/PostLoan2SupplierPaymentdtl';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.cbodisbursementPaymentlist = '';
                    $scope.txtdisbursementmaxpercent_paymentterm = '';
                    $scope.cbodisbursementcommodity = '';
                    fnPaymentdisbursementbothlist();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.DeletePaymentdisbursement = function (apploan2supplierpayment_gid) {
            var params = {
                apploan2supplierpayment_gid: apploan2supplierpayment_gid
            }
            var url = 'api/AgrMstApplicationEdit/DeleteSupplierPaymentDtl';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    fnPaymentdisbursementbothlist();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        function fnPaymentdisbursementbothlist() {
            lockUI();
            var params = {
                application_gid: $scope.application_gid,
                application2loan_gid: '',
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
        }

        $scope.CustomerRepaymentadd = function () {
            var params = {
                application_gid: $scope.application_gid,
                customerpaymenttype_gid: $scope.cboRepaymentTypedtl.milestonepaymenttype_gid,
                customerpaymenttype_name: $scope.cboRepaymentTypedtl.milestonepaymenttype_name,
                maximumpercent_paymentterm: $scope.txtRepaymentMaximumpercent,
                tmpadd_status: true,
            }
            var url = 'api/AgrMstApplicationEdit/PostLoan2Repaymentdtl';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.txtRepaymentMaximumpercent = '';
                    $scope.cboRepaymentTypedtl = '';
                    fnCustomerRepaymentbothlist();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.DeleteCustomerRepayment = function (paymenttypecustomer_gid) {
            var params = {
                paymenttypecustomer_gid: paymenttypecustomer_gid
            }
            var url = 'api/AgrMstApplicationEdit/DeleteLoan2Repaymentdtl';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    fnCustomerRepaymentbothlist();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        function fnCustomerRepaymentbothlist() {
            lockUI();
            var params = {
                application_gid: $scope.application_gid,
                application2loan_gid: '',
                tmp_status: 'both',
            }
            var url = 'api/AgrMstApplicationEdit/GetLoan2Repaymentdtl';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.MdlrePaymentdtl = resp.data.MdlPaymentdtl;
                }
            });
        }

        $scope.cboProductSubTypechange = function () {
            if ($scope.cboProductSubTypelist.loansubproduct_name == 'STF') {
                $scope.stfmandatory = true;
                $scope.STFdivshow = true;
            }
            else {
                $scope.stfmandatory = false;
                $scope.STFdivshow = false;
            }
        }

        $scope.changeholdingperiod = function () {
            $scope.monthlyservice = false;
            if ($scope.txtholdingperiod != "") {
                $scope.monthlyservice = true;
            }
        }
        $scope.changeextendedholdingperiod = function () {
            $scope.monthlyextendedservice = false;
            if ($scope.txtextendedholdingperiod != "") {
                $scope.monthlyextendedservice = true;
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
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance', 'DownloaddocumentService'];
            function ModalInstanceCtrl($scope, $modalInstance, DownloaddocumentService) {
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
                $scope.downloadall_4 = function () {
                    for (var i = 0; i < $scope.commodityDocumentUpload.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.commodityDocumentUpload[i].commodityreport_filepath, $scope.commodityDocumentUpload[i].commodityreport_filename);
                    }
                }
                $scope.downloadall_5 = function () {
                    for (var i = 0; i < $scope.commodityDocumentUpload.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.commodityDocumentUpload[i].riskanalysisreport_filepath, $scope.commodityDocumentUpload[i].riskanalysisreport_filename);
                    }
                }
            }
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.hypo_documentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.hypo_documentList[i].document_path, $scope.hypo_documentList[i].document_name);
            }
        }
        $scope.downloadall_2 = function () {
            for (var i = 0; i < $scope.cicuploaddoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name);
            }
        }
        $scope.downloadall_3 = function () {
            for (var i = 0; i < $scope.DocumentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.DocumentList[i].document_path, $scope.DocumentList[i].document_name);
            }
        }
        $scope.downloadall_4 = function () {
            for (var i = 0; i < $scope.commodityDocumentUpload.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.commodityDocumentUpload[i].commodityreport_filepath, $scope.commodityDocumentUpload[i].commodityreport_filename);
            }
        }
        $scope.downloadall_5 = function () {
            for (var i = 0; i < $scope.commodityDocumentUpload.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.commodityDocumentUpload[i].riskanalysisreport_filepath, $scope.commodityDocumentUpload[i].riskanalysisreport_filename);
            }
        }

    }
})();

