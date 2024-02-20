(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCustomerOnboardingSummaryController', AgrMstCustomerOnboardingSummaryController);

    AgrMstCustomerOnboardingSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', 'cmnfunctionService'];

    function AgrMstCustomerOnboardingSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCustomerOnboardingSummaryController';
        var selectedIndex = $location.search().selectedIndex;
        const lsdynamiclimitmanagementback = 'AgrMstCustomerOnboardingSummary';

        activate();

        function activate() {
            if (selectedIndex == "" || selectedIndex == undefined)
                $scope.selectedIndex = 0;
            else
                $scope.selectedIndex = selectedIndex;

            var url = 'api/AgrMstSupplierOnboard/GetonboardTempClear';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/AgrMstSupplierOnboard/suprronboardTmpClear';
            SocketService.get(url).then(function (resp) {

            });
            var url = 'api/AgrMstBuyerOnboard/byronboardTmpClear';
            SocketService.get(url).then(function (resp) {

            });
            //var url = 'api/AgrMstBuyerOnboard/GetBuyerApprovalPendingSummary';
            //SocketService.get(url).then(function (resp) {
            //    if (resp.data.status == true) {
            //        $scope.onboardbyrpendingapplicationlist = resp.data.onboardapplicationdtl;
            //    }
            //});


        }

        $scope.fnBuyeronboardingApprovalPendinglist = function () {
            getApprovalCount();
            lockUI();
            var url = 'api/AgrMstBuyerOnboard/GetBuyerApprovalPendingSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.onboardbyrpendingapplicationlist = resp.data.onboardapplicationdtl;
                }
                else unlockUI();
            });
        }

        $scope.fnBuyeronboardingApprovedlist = function () {
            getApprovalCount();
            lockUI();
            var url = 'api/AgrMstBuyerOnboard/GetBuyerApprovedSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.onboardbyrapprovedapplicationlist = resp.data.onboardapplicationdtl;
                }
                else unlockUI();
            });
        }

        $scope.fnSupplieronboardingpendinglist = function () {
            getApprovalCount();
            lockUI();
            var url = 'api/AgrMstBuyerOnboard/GetSupplierApprovalPendingSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.onboardsuprpendingapplicationlist = resp.data.onboardapplicationdtl;
                }
                else unlockUI();
            });
        }

        $scope.fnSupplieronboardingApprovedlist = function () {
            getApprovalCount();
            lockUI();
            var url = 'api/AgrMstBuyerOnboard/GetSupplierApprovedSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.onboardsuprapprovedapplicationlist = resp.data.onboardapplicationdtl;
                }
                else unlockUI();
            });
        }

        function getApprovalCount() {
            lockUI();
            var url = 'api/AgrMstBuyerOnboard/GetRMApprovalCountDetail';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.approvalcount = resp.data;
                }
                else unlockUI();
            });
        }

        $scope.addbuyer = function () {
            $state.go('app.AgrMstCustomerOnboardingInfoAdd');
        }

        $scope.addsupplier = function () {
            $state.go('app.AgrMstSupplierOnboardingInfoAdd');
        }

        $scope.applcreation_view = function (application_gid) {
        //    $location.url('app/AgrMstCustomerOnboardingApproval?application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsApp=N&FromRM=Y');
            $location.url('app/AgrMstCustomerOnboardingApproval?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsApp=N&FromRM=Y'));


        }

        $scope.Approvedapplcreation_view = function (application_gid) {
            $location.url('app/AgrMstCustomerOnboardingApproval?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsApp=Y&FromRM=Y'));
        }

        $scope.suprapplcreation_view = function (application_gid) {
            $location.url('app/AgrMstSupplierOnboardingApproval?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsApp=N&FromRM=Y'));
        }

        $scope.ApprovedSuprapplcreation_view = function (application_gid) {
            $location.url('app/AgrMstSupplierOnboardingApproval?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsApp=Y&FromRM=Y'));
        }

        $scope.rjectedsummary = function () {
            $location.url('app/AgrMstCustomerOnboardRejectedSummary?hash=' + cmnfunctionService.encryptURL('FromRM=Y'));
        }
        $scope.applcreation_edit = function (application_gid) {
            $location.url('app/AgrMstbyrOnboardInfoEdit?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid));
        }
        $scope.suprapplcreation_edit = function (application_gid) {
            $location.url('app/AgrMstSuprOnboardInfoEdit?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid));
        }

        
        $scope.initiate_buyeronboard = function (application_gid, application_no, customer_name) {
            $location.url('app/AgrMstInitiateApplication?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid));

            /*$location.url('app/AgrMstInitiateApplication?application_gid=' + application_gid);*/
            //var modalInstance = $modal.open({
            //    templateUrl: '/InitiateOnboard.html',
            //    controller: ModalInstanceCtrl,
            //    backdrop: 'static',
            //    keyboard: false,
            //    size: 'lg'
            //});
            //ModalInstanceCtrl.$inject = ['$scope', '$modalInstance', '$route'];
            //function ModalInstanceCtrl($scope, $modalInstance, $route) {
            //    $scope.Popupdtl = "Buyer";
            //    $scope.application_no = application_no;
            //    $scope.customer_name = customer_name;
            //    $scope.ok = function () {
            //        $modalInstance.close('closed');
            //    };
            //    $scope.initiate_onboard = function () {  
            //        var params = {
            //            application_gid: application_gid, 
            //            approval_remarks: $scope.txtremarks
            //        }
            //        var url = 'api/AgrMstBuyerOnboard/PostInitiateBuyerApplication';
            //        lockUI();
            //        SocketService.post(url, params).then(function (resp) {
            //            unlockUI();
            //            if (resp.data.status == true) {
            //                Notify.alert(resp.data.message, {
            //                    status: 'success',
            //                    pos: 'top-center',
            //                    timeout: 3000
            //                });
            //                $scope.current = $state.current.name;
            //                ScopeValueService.store("dataldCtrl", $scope);
            //                $state.go('app.pageredirect'); 
            //                $modalInstance.close('closed');
            //            }
            //            else {
            //                Notify.alert(resp.data.message, {
            //                    status: 'warning',
            //                    pos: 'top-center',
            //                    timeout: 3000
            //                });
            //            }
            //        });

            //        $modalInstance.close('closed');
            //    } 
            //}
        }

        $scope.initiate_supplieronboard = function (application_gid, application_no, customer_name) {
            var modalInstance = $modal.open({
                templateUrl: '/InitiateOnboard.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) { 
                $scope.application_no = application_no;
                $scope.Popupdtl = "Supplier";
                $scope.customer_name = customer_name;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.initiate_onboard = function () {
                    var params = {
                        application_gid: application_gid,
                        approval_remarks: $scope.txtremarks
                    }
                    var url = 'api/AgrMstSupplierOnboard/PostInitiateSupplierApplication';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.current = $state.current.name;
                            ScopeValueService.store("dataldCtrl", $scope);
                            $state.go('app.pageredirect');
                            $modalInstance.close('closed');
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');
                } 
            }
        }

        $scope.onboardappdetailinfo = function (onboard_gid) {
            $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lsApp=Y&FromRM=N'+ '&lsparent =RMOnboard'));
        }
       
        $scope.buyer_renewal = function (onboard_gid) {
            $location.url('app/AgrMstRenewalApplicationAdd?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lsApp=Y&FromRM=N'));
        }
    }
})();
