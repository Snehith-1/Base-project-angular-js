(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrRptPhysicalDeferralHistoryController', AgrRptPhysicalDeferralHistoryController);

    AgrRptPhysicalDeferralHistoryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrRptPhysicalDeferralHistoryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrRptPhysicalDeferralHistoryController';

        var lspage = $location.search().lspage;
        var application_gid = $location.search().application_gid;
        var credit_gid = $location.search().credit_gid;
        var lstype = $location.search().lstype;
        var lsdocumentcheckdtl_gid = $location.search().lsdocumentcheckdtl_gid;
        var processtypeassign_gid = $location.search().processtypeassign_gid;
        var lspath = $location.search().lspath;

        activate();

        function activate() {
            lockUI();
            var params = {
                groupdocumentdtl_gid: lsdocumentcheckdtl_gid
            }
            var url = 'api/AgrTrnPhysicalDocument/GetDeferralHistorySummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lbldocumenttype_name = resp.data.document_name;
                $scope.lbldocumentcode = resp.data.document_type;
                $scope.taggeddeferralhistory = resp.data.deferraltagged;
            });
        }

        $scope.Back = function () {
            $location.url('app/AgrRptCadPhysicalDocchecklist?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lspath);
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
                var url = 'api/AgrTrnPhysicalDocument/GettaggedHistoryDeferralinfo';
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
