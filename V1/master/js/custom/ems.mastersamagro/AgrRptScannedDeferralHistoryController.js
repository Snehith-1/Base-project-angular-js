(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrRptScannedDeferralHistoryController', AgrRptScannedDeferralHistoryController);

    AgrRptScannedDeferralHistoryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrRptScannedDeferralHistoryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrRptScannedDeferralHistoryController';
        var lspage = $location.search().lspage;
        var application_gid = $location.search().application_gid;
        var credit_gid = $location.search().credit_gid;
        var lstype = $location.search().lstype;
        var lsdocumentcheckdtl_gid = $location.search().lsdocumentcheckdtl_gid;
        var processtypeassign_gid = $location.search().processtypeassign_gid;
        var lspath = $location.search().lspath;
        var lsscanned = $location.search().lsscanned;
        var lsdeferraltag = $location.search().lsdeferraltag;

        activate();

        function activate() {
            lockUI();
            var params = {
                groupdocumentdtl_gid: lsdocumentcheckdtl_gid
            }
            var url = 'api/AgrMstScannedDocument/GetDeferralHistorySummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lbldocumenttype_name = resp.data.document_name;
                $scope.lbldocumentcode = resp.data.document_type;
                $scope.taggeddeferralhistory = resp.data.deferraltagged;
            });

        }

        $scope.Back = function () {
            if (lspage == "RMDocChecklist")
                $location.url('app/AgrTrnRMDeferralDtlsView?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lspath);
            else
                $location.url('app/AgrRptCadScannedDocchecklist?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lspath);
        }

        $scope.reasonapproval_view = function (reason, approval_status, initiateextendorwaiver_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/remarksandreasondtl.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblreason = reason;
                $scope.lblapproval_status = approval_status;
                if (approval_status != 'No Approval') {
                    $scope.lblapproval_status = '';
                    var params = {
                        initiateextendorwaiver_gid: initiateextendorwaiver_gid
                    }
                    var url = 'api/AgrMstScannedDocument/GetApprovalExtensionwaiver';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.approvallist = resp.data.mdlapprovaldtl;

                    });
                }


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }


        $scope.deferraldoc_view = function (deferraltagdoc_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewdeferralinfo.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var params = {
                    deferraltagdoc_gid: deferraltagdoc_gid
                }
                var url = 'api/AgrMstScannedDocument/GettaggedHistoryDeferralinfo';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcad_remarks = resp.data.cad_remarks;
                    $scope.deferraltaggedchecklist = resp.data.deferraltaggedchecklist;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
    }
})();
