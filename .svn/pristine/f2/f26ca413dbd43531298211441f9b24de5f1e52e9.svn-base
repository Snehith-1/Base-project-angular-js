(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSentBackToUnderwritingController', AgrTrnSentBackToUnderwritingController);

        AgrTrnSentBackToUnderwritingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnSentBackToUnderwritingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSentBackToUnderwritingController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/AgrTrnCAD/GetSentBackToUnderwritingSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.Sentbacktounderwriting_list = resp.data.cadapplicationlist;
            });
            var url = 'api/AgrTrnCAD/CADApplicationCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cadreview_count = resp.data.cadreview_count;
                $scope.sentbackcc_count = resp.data.sentbackcc_count;
                $scope.accept_count = resp.data.accept_count;
                $scope.backtounderwriting_count = resp.data.backtounderwriting_count;
                $scope.ccrejected_count = resp.data.ccrejected_count;
                $scope.advancerejected_count = resp.data.advancerejected_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=SenetBackToUnderwriting');
        }

        $scope.pendincad_review = function () {
            $location.url('app/AgrTrnPendingCADReview');
        }

        $scope.cadaccepted_customers = function () {
            $location.url('app/AgrTrnCadAcceptedCustomers');
        }

        $scope.sendback_underwriting = function () {
            $location.url('app/AgrTrnSentBackToUnderwriting');
        }

        $scope.sendback_cc = function () {
            $location.url('app/AgrTrnSentBackToCC');
        }

        $scope.cc_rejected = function () {
            $location.url('app/AgrTrnCCRejectedApplications');
        }

        $scope.Advance_rejected = function () {
            $location.url('app/AgrTrnPmgAdvanceRejectedSummary');
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
                var url = 'api/AgrTrnCAD/GetSentbackToCCUnderwritingView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.processtype_remarks = resp.data.processtype_remarks;
                    $scope.process_type = resp.data.process_type;
                    $scope.processupdated_by = resp.data.processupdated_by;
                    $scope.processupdated_date = resp.data.processupdated_date;
                });
                var params = {
                    application_gid: application_gid,
                }
                var url = 'api/AgrTrnCC/GetSendbackCCMeetingSkippedReason';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.withoutccprocess_type = "Sendback to Credit Without CC";
                    $scope.ccmeetingskip_by = resp.data.ccmeetingskip_by;
                    $scope.ccmeetingskip_date = resp.data.ccmeetingskip_date;
                    $scope.ccmeetingskip_remarks = resp.data.ccmeetingskip_remarks;
                });


                $modalInstance.close('closed');
            }            
        }
    }

})();
