(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstAppEditTradeAdd', AgrMstAppEditTradeAdd);

    AgrMstAppEditTradeAdd.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstAppEditTradeAdd($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstAppEditTradeAdd';
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

            var url = 'api/AgrTrnApplicationApproval/Getproceedapprovalflag';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.proceedtoapproval_flag = resp.data.proceedtoapproval_flag;

            });

            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationEdit/EditProceed';
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

            $scope.amendmentshow = false;
            var url = 'api/AgrMstApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lblapplication_no = resp.data.application_no;
                $scope.lblbasiccustomer_name = resp.data.customer_name;
                $scope.lblamendment_remarks = resp.data.amendment_remarks;
                unlockUI();

                if ($scope.lblamendment_remarks == null || $scope.lblamendment_remarks == '' || $scope.lblamendment_remarks == undefined) {
                    $scope.amendmentshow = false;
                }
                else {
                    $scope.amendmentshow = true;
                }
            });


            var proceed_flag = $scope.proceed_flag;
            var approveinitiated_flag = $scope.approveinitiated_flag;
            var application_gid = $scope.application_gid;
            var params = {
                application_gid: application_gid
            }

            var url = 'api/AgrMstApplicationAdd/GetApprovalHierarchyFlag';
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
             
            var url = 'api/AgrMstApplicationAdd/TradeTmpClear';
            SocketService.get(url).then(function (resp) {
            });

            lockUI();
            var params = {
                application_gid: $scope.application_gid,
                tmp_status: false
            }
            var url = 'api/AgrMstApplicationAdd/GetApplicationTradeList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MdlTradelist = resp.data.MdlTradedtl;
                unlockUI();
            });

            $scope.divshow = true;
            //var param = {
            //    application_gid: $scope.application_gid
            //}
            //var url = 'api/AgrMstApplicationAdd/GetLoanDtl';
            //SocketService.getparams(url, param).then(function (resp) {
            //    $scope.mstloan_list = resp.data.mstloan_list;
            //    $scope.servicecharges_list = resp.data.servicecharges_list;

            //    if ($scope.servicecharges_list == null) {
            //        $scope.divshow = true;
            //    }
            //    else {
            //        $scope.divshow = false;
            //    }
            //});
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationAdd/GetTradeproduct';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.product_list = resp.data.product_list;
            });
             
            var url = 'api/AgrMstApplicationEdit/GetWarehouseDetail';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.Warehousedtllist = resp.data.MdlWarehousedtl;
            });

            var url = 'api/AgrMstCreditorMaster/GetNewcreditorSummary';
            unlockUI();
            SocketService.get(url).then(function (resp) {
                $scope.creditoradd_list = resp.data.MdlcreditorCreation;
            });


            var url = 'api/AgrMstCreditorMaster/Getcreditorapplicanttype';
            unlockUI();
            SocketService.get(url).then(function (resp) {
                $scope.creditorwsp_list = resp.data.MdlcreditorCreation;
            });

            var url = 'api/AgrMstApplicationAdd/GetScopedtl';
            SocketService.get(url).then(function (resp) {
                $scope.scope_list = resp.data.ScopeList;
            });
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationEdit/GetEditProductcharges';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lbloveralllimit_amount = resp.data.overalllimit_amount;  
                $scope.applicant_type = resp.data.applicant_type; 
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
            var params = {
                overalllimit_amount: $scope.txtOveralllimit_amount,
                validityoveralllimit_year: $scope.txtvalidityoveralllimit_year,
                validityoveralllimit_month: $scope.txtvalidityoveralllimit_month,
                validityoveralllimit_days: $scope.txtvalidityoveralllimit_day,
                calculationoveralllimit_validity: $scope.txtcalculationoveralllimit_validity,
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
                        $location.url('app/AgrMstApplicationProductChargesAdd?lstab=' + lstab);
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
        $scope.producttype = function () {
            var getselected = $scope.product_list.find(function (v) { return v.application2loan_gid == $scope.cboProductTypelist.application2loan_gid})
            if (getselected != null) {
                $scope.txtProductsubType = getselected.productsub_type
                $scope.txtProductsubTypeGid = getselected.productsubtype_gid
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

                var data = $scope.Warehousedtllist.find(function (v) { return v.warehouse_gid === $scope.cbowarehouse_name.warehouse_gid})
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




        $scope.Submit_creditor = function () {

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
                creditor_gid: $scope.cbocreditor.creditor_gid,
                Applicant_name: $scope.cbocreditor.Applicant_name,
                Applicant_category: $scope.txtApplicant_category,
                designation_type: lsdesignation_type,
                contactperson_name: lscontactperson_name,
                contact_no: lscontact_no,
                pan_no: lspan_no,
                email_id: lsemail_id,
                application_gid: $scope.application_gid,
            }
            var url = 'api/AgrMstCreditorMaster/PostTrade2CreditorDtl';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    tradecreditorlist();
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
                scopeof_insurancegid: $scope.cboScopeofInsurance.scope_gid,
                scopeof_insurance: $scope.cboScopeofInsurance.scope_name,
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
                    $scope.cboScopeofInsurance = '';
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

        function fnTradeSummary() {
            lockUI();
            var params = {
                application_gid: $scope.application_gid,
                tmp_status: "both"
            }
            var url = 'api/AgrMstApplicationAdd/GetApplicationTradeList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MdlTradelist = resp.data.MdlTradedtl;
                unlockUI();
            });
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationAdd/GetTradeproduct';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.product_list = resp.data.product_list;
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


        $scope.general_Tab = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Company_dtls = true;
            }
        }

        $scope.company_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationInstitutionEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Institution_dtls = true;
            }
        }
        $scope.Group_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationGroupEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Group_dtls = true;
            }
        }

        $scope.Individual_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationIndividualEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Individual_dtls = true;
            }
        }

        $scope.EconomicCapital_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationSocialTradeCapitalEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
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
                $location.url('app/AgrMstApplcreationProductchargesEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.EditHypothecation = function (application_gid) {
            $location.url('app/AgrMstApplicationHypothecationEdit?lsapplication_gid=' + application_gid + '&lstab=edit');
        }
        $scope.Hypothecation_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/AgrMstAppEditHypothecationAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }

        $scope.OverallLimit_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/AgrMstAppEditOverallLimitAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.ProductCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/AgrMstAppEditProductAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.ServiceCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/AgrMstAppEditChargeAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.BureauUpdates_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationEditCICUploadAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
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
            var url = 'api/AgrMstApplicationAdd/GetOverallInfo';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.application_status = resp.data.application_status;
            });
            lockUI();
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationEdit/EditProceed';
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

            var url = 'api/AgrMstApplicationAdd/GetApprovalHierarchyFlag';
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
            $state.go('app.AgrMstApplicationCreationSummary');
        }
        $scope.overallsubmit_application = function () {
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationEdit/EditAppProceed';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();
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

        

        $scope.Submit_Trade = function () {
            var params = {
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
            }
            var url = 'api/AgrMstApplicationAdd/PostTrade2WarehouseDetail';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    tradewarehouselist();
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

        function tradewarehouselist() {
            //var params = {

            //    application_gid: $scope.application_gid,
            //}

            var url = 'api/AgrMstApplicationAdd/GetTrade2WarehouseDetail';
            SocketService.get(url).then(function (resp) {
                $scope.creditor2warehouse_list = resp.data.creditor2warehouse_list;
            });
        }

        function tradecreditorlist() {
            //var params = {

            //    application_gid: $scope.application_gid,
            //}

            var url = 'api/AgrMstCreditorMaster/GetTrade2CreditorDtl';
            SocketService.get(url).then(function (resp) {
                $scope.trade2creditor_list = resp.data.trade2creditor_list;
                $scope.showdtls = false
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
                tradecreditorlist();
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
                tradewarehouselist();
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

