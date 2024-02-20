(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstMyApplicationsSummaryController', AgrMstMyApplicationsSummaryController);

    AgrMstMyApplicationsSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function AgrMstMyApplicationsSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstMyApplicationsSummaryController';
        lockUI();
        activate();
        function activate() {
            var url = 'api/AgrTrnCreditApproval/GetMyAppAssignedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.myappassignment_list = resp.data.applicaition_list;
            });
            var url = 'api/AgrTrnCreditApproval/MyApplicationCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.newcreditapplication_count = resp.data.newcreditapplication_count;
                $scope.approvedapplication_count = resp.data.approvedapplication_count;
                $scope.rejectholdapplication_count = resp.data.rejectholdapplication_count;
                $scope.submitted2ccapp_count = resp.data.submitted2ccapp_count;
                $scope.ccapproval_count = resp.data.ccapproval_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });

        }

        $scope.applcreation_view = function (val) {
            $location.url('app/AgrApplicationCreationView?application_gid=' + val + '&lstab=MyApplications');
        }

        $scope.start_creditunderwriting = function (application_gid, appcreditapproval_gid, product_gid, variety_gid, shortclosing_flag) {
            $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid + '&appcreditapproval_gid=' + appcreditapproval_gid + '&lspage=myapp' + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid + '&shortclosing_flag=' + shortclosing_flag);
        }

        $scope.GradingTool_add = function (val) {
            $location.url('app/AgrMstCreditAssessedScoreAdd?application_gid=' + val + '&lstab=MyApplications');          
        }

        $scope.VisitReport_add = function (val) {
            $location.url('app/AgrCreditVisitReportAdd?application_gid=' + val + '&lstab=MyApplications');
        }

        $scope.submitedto_approval = function () {
            $location.url('app/AgrMstSubmittedtoApprovalSummary');
        }

        $scope.submittedto_cc = function () {
            $location.url('app/AgrMstSubmittedtoCCSummary');
        }

        $scope.ccskipped_appl = function () {
            $location.url('app/AgrMstCCSkippedApplicationSummary');
        }

        $scope.rejected_holdappl = function () {
            $location.url('app/AgrMstRejectandHoldSummary');
        }

        $scope.inprogress_appl = function () {
            $location.url('app/AgrMstMyApplicationsSummary');
        }

        $scope.creditcmd_statusupdate = function (application_gid, employee_gid, applicationapproval_gid,initiate_flag) {
            $location.url('app/AgrMstCreditQueryStatus?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&lspage=MyApplications');
        }

        $scope.history = function (application_gid, employee_gid, applicationapproval_gid, initiate_flag) {
            $location.url('app/AgrMstSentbackcctoCreditHistory?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&lspage=MyApplications');
        }

        $scope.ccapproved = function () {
            $location.url('app/AgrMstCCApprovedSummary');
        } 

        $scope.shortclosing_creditapproval = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/BuyerShortClosingPopup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var param = {
                    application_gid: application_gid
                };

                var url = 'api/AgrTrnApplicationApproval/Getapplicationdetails';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.shortclosing_reason = resp.data.shortclosing_reason;
                    $scope.expired_flag = resp.data.expired_flag;
                });
                $scope.submitcredit_shortclosing = function () {                    
                    lockUI();
                    var params = {
                        application_gid: application_gid
                    }
                    var url = 'api/AgrTrnCreditApproval/Getappcreditapprovalinitiate';
                    SocketService.getparams(url, params).then(function (resp) {
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                    activate();
                };
            }

        }
    }
})();
