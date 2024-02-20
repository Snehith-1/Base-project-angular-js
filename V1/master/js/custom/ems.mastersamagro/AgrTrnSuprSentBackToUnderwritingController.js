(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprSentBackToUnderwritingController', AgrTrnSuprSentBackToUnderwritingController);

    AgrTrnSuprSentBackToUnderwritingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnSuprSentBackToUnderwritingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprSentBackToUnderwritingController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/AgrTrnSuprCAD/GetSentBackToUnderwritingSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.Sentbacktounderwriting_list = resp.data.cadapplicationlist;
            });
            var url = 'api/AgrTrnSuprCAD/CADApplicationCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cadreview_count = resp.data.cadreview_count;
                $scope.sentbackcc_count = resp.data.sentbackcc_count;
                $scope.accept_count = resp.data.accept_count;
                $scope.backtounderwriting_count = resp.data.backtounderwriting_count;
                $scope.ccrejected_count = resp.data.ccrejected_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstSuprCadApplicationView?application_gid=' + val + '&lspage=SenetBackToUnderwriting');
        }

        $scope.pendincad_review = function () {
            $location.url('app/AgrTrnSuprPendingCADReview');
        }

        $scope.cadaccepted_customers = function () {
            $location.url('app/AgrTrnSuprCadAcceptedCustomers');
        }

        $scope.sendback_underwriting = function () {
            $location.url('app/AgrTrnSuprSentBackToUnderwriting');
        }

        $scope.sendback_cc = function () {
            $location.url('app/AgrTrnSuprSentBackToCC');
        }

        $scope.cc_rejected = function () {
            $location.url('app/AgrTrnSuprCCRejectedApplications');
        }

        $scope.remarks = function (val) {
            var modalInstance = $modal.open({
                templateUrl: '/ProcessstypeRemarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var application_gid = val;
                var params = {
                    application_gid: application_gid,
                }
                var url = 'api/AgrTrnSuprCAD/GetSentbackToCCUnderwritingView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.processtype_remarks = resp.data.processtype_remarks;
                    $scope.process_type = resp.data.process_type;
                    $scope.processupdated_by = resp.data.processupdated_by;
                    $scope.processupdated_date = resp.data.processupdated_date;
                });

                $modalInstance.close('closed');
            }
        }
    }
})();
