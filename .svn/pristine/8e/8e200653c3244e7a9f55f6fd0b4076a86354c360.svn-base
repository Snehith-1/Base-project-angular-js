(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier2Approval', tier2Approval);

    tier2Approval.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService'];

    function tier2Approval($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier2Approval';

        activate();

        function activate() {
            var params = {
                tier2preparation_gid: localStorage.getItem('tier2preparation_gid')
            }
            var url = 'api/TierMeeting/GetTier2ViewDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.zonal_name = resp.data.zonal_name;
                $scope.cbomonth = resp.data.tier2_monthname;
                $scope.vertical = resp.data.vertical;
                $scope.cbovertical_gid = resp.data.vertical_gid;
                $scope.cboemployeegid = resp.data.headRMD_gid;
                $scope.headRMD_name = resp.data.headRMD_name;
                $scope.txttier2_remarks = resp.data.tier2_remarks;
                $scope.tier2_approval_status = resp.data.tier2_approval_status;
                console.log($scope.tier2_approval_status);
                $scope.created_date = resp.data.created_date;
                $scope.created_by = resp.data.created_by;
                $scope.uploaddocument_list = resp.data.tier2document;
                $scope.tier2approvallog = resp.data.tier2approvallog;
                $scope.tierallocationdtl = resp.data.tierallocationdtl;
                $scope.tier3_status = resp.data.tier3_status;
                if ($scope.tier2_approval_status == 'Approved') {
                    $scope.editdisable = true;
                }
                else {
                    $scope.editenable = true;
                }
                if (resp.data.tier2approvallog == null) {
                    $scope.nohistoryapproval = true;
                }
                else {
                    $scope.historyapproval = true;
                }
                unlockUI();
            });
        }

        $scope.viewcustomerdtl = function (allocationdtl_gid, tier1format_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('tier1format_gid', tier1format_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/tierCustomer360";
            window.open(URL, '_blank');
        }

        $scope.tier2Approve = function () {
            var params = {
                tier2preparation_gid: localStorage.getItem('tier2preparation_gid'),
                approval_remarks: $scope.txttier2_approvalremarks
            }
            lockUI();
            var url = "api/TierMeeting/PostTier2Approved";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.tier2ApprovalSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
        }


        $scope.tier2Reject = function () {
            var params = {
                tier2preparation_gid: localStorage.getItem('tier2preparation_gid'),
                approval_remarks: $scope.txttier2_approvalremarks
            }
            lockUI();
            var url = "api/TierMeeting/PostTier2Rejected";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.tier2ApprovalSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
        }


        $scope.riskcodechange = function (allocationdtl_gid, customer_name, customer_urn, tier2_code) {

            var modalInstance = $modal.open({
                templateUrl: '/riskcodechangeModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customer_name = customer_name;
                $scope.customer_urn = customer_urn;
                $scope.cboriskcode = tier2_code;
                $scope.ok = function () {
                    $modalInstance.close('closed');

                };

                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                var url = "api/TierMeeting/GetTierColorDetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.tiercodedtl = resp.data.tiercodedtl;

                });
            }
        }

        $scope.downloads = function (val1, val2) {
           DownloaddocumentService.Downloaddocument(val1, val2);
        }
    }
})();
