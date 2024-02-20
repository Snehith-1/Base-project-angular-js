(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstRenewalApplicationAddController', AgrMstRenewalApplicationAddController);

    AgrMstRenewalApplicationAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function AgrMstRenewalApplicationAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstRenewalApplicationAddController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);

        var onboard_gid = searchObject.onboard_gid;
        var lsdynamiclimitmanagementback = searchObject.lsdynamiclimitmanagementback;
        var lspage = searchObject.lspage;
        var lstab = searchObject.lstab;
        $scope.appcreditapproval_gid = searchObject.appcreditapproval_gid;
        var appcreditapproval_gid = $scope.appcreditapproval_gid;
        $scope.application_gid = searchObject.application_gid;
        var application_gid = $scope.application_gid;

        activate();
        lockUI();
        function activate() {
            var params = {
                onboard_gid: onboard_gid
            }
            var url = 'api/AgrTrnCloneApplication/GetOnboardLimitManagementdtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.customerref_name = resp.data.customerref_name;
                $scope.application_no = resp.data.application_no;
                $scope.lgltag_status = resp.data.lgltag_status;
                $scope.ProductTypeList = resp.data.MdlRenewalProductTypeList;
                $scope.ProductSubTypeList = resp.data.MdlRenewalProductSubTypeList;
                $scope.ApplicationList = resp.data.MdlRenewalApplicationList;
                $scope.ApplicationFacilityList = resp.data.MdlRenewalFaclilityDtl;
              /*  $scope.MdlPmgExpiryDate = resp.data.MdlPmgcloneExpiryDate;*/

                if ($scope.ApplicationList && $scope.ApplicationList.length != 0) {
                    var getnotexpiredapplication = $scope.ApplicationList.filter(function (el) { return el.contract_status!="Expired" }); 
                    var totalPrice = getnotexpiredapplication.reduce(function (accumulator, item) { 
                        return accumulator + parseInt(item.product_overallamount);
                    }, 0);
                    $scope.customeroverall_limit = (parseInt(totalPrice) || 0).toLocaleString('en-IN');;
                }

                angular.forEach($scope.ApplicationFacilityList, function (value, key) {
                    value.ApprovedLimit = (parseInt(value.ApprovedLimit) || 0).toLocaleString('en-IN');
                    // value.UtilizedLimit = (parseInt(value.UtilizedLimit) || 0).toLocaleString('en-IN');
                    // value.AvailableLimit = (parseInt(value.AvailableLimit) || 0).toLocaleString('en-IN');
                });

                angular.forEach($scope.ApplicationList, function (value, key) {
                    if (value.application_gid != "") {
                        var getapplicationFacilityListArray = $scope.ApplicationFacilityList.filter(function (el) { return el.application_gid === value.application_gid });
                        if (getapplicationFacilityListArray != null) {
                            value.ApplicationFacilityList = getapplicationFacilityListArray; 
                        } 
                    }
                });

                angular.forEach($scope.ProductSubTypeList, function (value, key) {
                    if (value.application_gid != "") {
                        var getapplicationListArray = $scope.ApplicationList.filter(function (el) { return el.application_gid === value.application_gid });
                        if (getapplicationListArray != null) {
                            value.ApplicationListdtl = getapplicationListArray;  
                            var activedata = getapplicationListArray.filter(function (el) { return el.contract_status === "Active" });
                            var totalPrice = activedata.reduce(function (accumulator, item) {
                                return accumulator + item.product_overallamount;
                            }, 0);
                            value.overalllimit_subamount = (parseInt(totalPrice) || 0).toLocaleString('en-IN');
                            value.overalllimit_subamountvalue = parseInt(totalPrice); 
                        }
                       
                    }
                });

                angular.forEach($scope.ProductTypeList, function (value, key) {
                    if (value.producttype_gid != "") {
                        var getsubproductArray = $scope.ProductSubTypeList.filter(function (el) { return el.producttype_gid === value.producttype_gid });
                        if (getsubproductArray != null) {
                            value.SubproductArray = getsubproductArray; 
                            var totalPrice = getsubproductArray.reduce(function (accumulator, item) {
                                return accumulator + item.overalllimit_subamountvalue;
                            }, 0);
                            value.overalllimit_amount = (parseInt(totalPrice) || 0).toLocaleString('en-IN');
                        } 
                    }
                }); 

                //angular.forEach($scope.MdlPmgExpiryDate, function (value, key) {
                //    if (value.processupdated_date != "") {
                //        var getprocessupdateddateArray = $scope.MdlPmgExpiryDate.filter(function (el) { return el.processupdated_date === value.processupdated_date });
                //        if (getprocessupdateddateArray != null) {
                //            value.MdlPmgExpiryDate = getprocessupdateddateArray;

                //        }
                //    }
                //}); 

            });

            var params = {
                onboard_gid: onboard_gid
            }
            var url = 'api/AgrTrnCloneApplication/GetApplCloneHistoryDtlLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.Applclonehistorylog_list = resp.data.Applclonehistorylog_list;
            });
        }

        $scope.BackPageClick = function () {                    
              $location.url('app/AgrMstCustomerOnboardingSummary');               

        }

        $scope.facilitydetailsview = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/facilitydetailsviewid.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) { 

                var params = {
                    application_gid: application_gid
                }
                var url = 'api/AgrTrnCloneApplication/GetOnboardLimitFacilitydtl';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.MdlFaclilitydtl = resp.data.MdlRenewalFaclilitydtl; 
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                }; 

            }

        }

        $scope.buyer_renewal = function (buyeronboard_gid,application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/BuyerRenewalpopup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.renewedshow = false;
                $scope.renewalshow = true;
                $scope.renewl_confirm = function () {
                    var params = {
                        buyer_gid: buyeronboard_gid,
                        application_gid: application_gid
                      

                    }
                    lockUI();
                    var url = 'api/AgrTrnCloneApplication/PostRenewalAdd';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            $scope.renewedapplication_no = resp.data.application_no;
                            $scope.renewedshow = true;
                            $scope.renewalshow = false;
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                    activate();
                };
            }

        }

        $scope.buyer_amendment = function (buyeronboard_gid, application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/BuyerAmendmentpopup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.amendmentedshow = false;
                $scope.amendmentshow = true;

                var url = 'api/AgrTrnCloneApplication/GetAmentmentMasterList';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.amendment_list = resp.data.cloneamendment_list;
                    unlockUI();
                });

                $scope.amendment_confirm = function () {
                    var params = {
                        buyer_gid: buyeronboard_gid,
                        application_gid: application_gid,
                        amendmentreason: $scope.cboamendment,
                        amendment_remarks: $scope.txtremarks,
                    }
                    lockUI();
                    var url = 'api/AgrTrnCloneApplication/PostAmendmentAdd';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            $scope.amendmentapplication_no = resp.data.application_no;
                            $scope.amendmentedshow = true;
                            $scope.amendmentshow = false;
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                    activate();
                };
            }

        }

        $scope.remarks_log = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/HistoryRemarksView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    application_gid: application_gid
                }
                var url = 'api/AgrTrnCloneApplication/GetHistoryLogRemarksView';
                lockUI();
                SocketService.getparams(url,params).then(function (resp) {
                    $scope.historylogremarks_list = resp.data.historylogremarks_list;
                    unlockUI();
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                    activate();
                };
            }

        }

        $scope.buyer_shortclosing = function (buyeronboard_gid,application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/BuyerShortClosingPopup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.shortclosedshow = false;
                $scope.shortclosingshow = true;
                $scope.shortclosing_confirm = function () {
                    if ($scope.txtremarks == null || $scope.txtremarks == '' || $scope.txtremarks == undefined) {
                        Notify.alert('Enter the Remarks', 'warning');
                    }
                    else {
                    var params = {
                        buyer_gid: buyeronboard_gid,
                        application_gid: application_gid,
                        shortclosing_reason: $scope.txtremarks
                    }
                    lockUI();
                    var url = 'api/AgrTrnCloneApplication/PostShortClosingAdd';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            $scope.shortclosingapplication_no = resp.data.application_no;
                            $scope.shortclosedshow = true;
                            $scope.shortclosingshow = false;
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });

                }
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                    activate();
                };
            }

        }

    }
})();
