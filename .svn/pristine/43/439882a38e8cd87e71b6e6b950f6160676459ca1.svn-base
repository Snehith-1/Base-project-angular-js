(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSanctionDtlViewSummaryController', AgrMstSanctionDtlViewSummaryController);

    AgrMstSanctionDtlViewSummaryController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', '$anchorScroll'];

    function AgrMstSanctionDtlViewSummaryController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, $anchorScroll) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSanctionDtlViewSummaryController';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.employee_gid = $location.search().employee_gid;
        var employee_gid = $scope.employee_gid;
        $scope.sanction_gid = $location.search().sanction_gid;
        var sanction_gid = $location.search().sanction_gid;
        $scope.sanctionapprovallog_gid = $location.search().sanctionapprovallog_gid;
        var sanctionapprovallog_gid = $location.search().sanctionapprovallog_gid;
        $scope.application2sanction_gid = $location.search().application2sanction_gid;
        var application2sanction_gid = $scope.application2sanction_gid;
        $scope.lsresubmit = $location.search().lsresubmit;
        var lsresubmit = $scope.lsresubmit;
        activate();

        function activate() {
            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrTrnContract/GetAppSanctionSummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.appsanction_list = resp.data.appsanction_list;
            });

            var url = 'api/AgrTrnContract/GetApprovalDetails';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.maker_name = resp.data.maker_name;
                $scope.checker_name = resp.data.checker_name;
                $scope.approver_name = resp.data.approver_name;
                $scope.maker_approveddate = resp.data.maker_approveddate;
                $scope.checker_approveddate = resp.data.checker_approveddate;
                $scope.approver_approveddate = resp.data.approver_approveddate;
            });
        }

        $scope.sanctionsubmit = function (application2sanction_gid, application_gid) {

            var params = {
                application2sanction_gid: application2sanction_gid,
                application_gid: application_gid

            }

            var url = 'api/AgrTrnContract/SanctionSubmitToApproval';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                    Notify.alert(resp.data.message, 'success')

                  

                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, 'warning')
                }
                activate();
            });
        }



        $scope.Back = function () {
            if (lspage == 'checkersummary' || lspage == "checkerfollowupsummary") {
                $state.go('app.AgrMstSanctionCheckerSummary');
            }
            else if (lspage == 'checkerapprovalsummary') {
                $state.go('app.AgrMstSanctionApprovalSummary');
            }
            else if (lspage == 'RMSanctionSummary') {
                $location.url('app/AgrMstRMSanctionSummary?application_gid=' + application_gid);
            }
            else if (lspage == 'SanctionApprovalCompleted') {
                $location.url('app/AgrMstSanctionApprovalCompleted?application2sanction_gid=' + application2sanction_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'SanctionAcceptedCustomer') {
                $location.url('app/AgrMstSanctionAccepted?application2sanction_gid=' + application2sanction_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);

            }
            else {
                $location.url('app/AgrMstAppSanctionLetterGeneration?sanction_gid=' + sanction_gid + '&employee_gid=' + employee_gid + '&application_gid=' + application_gid + '&lspage=SanctionMaker');
            }          

        }

        $scope.sanctiontocheckerview = function (application2sanction_gid, application_gid, followuppage) {
            var page = 'checkersummary';
            if (followuppage == 'Y')
                page = 'checkerfollowupsummary';
            $location.url('app/AgrMstAppContractLetterWordView?sanction_gid=' + application2sanction_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
        }
        $scope.edit = function (application2sanction_gid, application_gid, lsemployeegid) {
            $location.url('app/AgrMstSanctionEdit?application2sanction_gid=' + application2sanction_gid + '&employee_gid=' + lsemployeegid + '&application_gid=' + application_gid + '&lspage=' + lspage + '&lsresubmit=' + lsresubmit);
        }
        //$scope.View = function (application2sanction_gid, application_gid, lsemployeegid) {
        //    $location.url('app/AgrMstSanctionAcceptedView?application2sanction_gid=' + application2sanction_gid + '&employee_gid=' + lsemployeegid + '&application_gid=' + application_gid + '&lspage=' + lspage + '&lsresubmit=' + lsresubmit);
        //}

        $scope.sanctiontocheckerview = function (application2sanction_gid,application_gid, followuppage) {
            var page = 'checkersummary';
            if (followuppage == 'Y')
                page = 'checkerfollowupsummary';
            $location.url('app/AgrMstContractDtlViewSummary?sanction_gid=' + application2sanction_gid + '&application_gid=' + application_gid + '&lspage=' + page);
        }

        $scope.sanctionlettergenerate = function (sanction_gid, application_gid, lsemployeegid) {
            localStorage.setItem('RefreshTemplate', 'N');
            $location.url('app/AgrMstAppSanctionLetterGeneration?sanction_gid=' + sanction_gid + '&employee_gid=' + lsemployeegid + '&application_gid=' + application_gid + '&lspage=' + lspage + '&lsresubmit=' + lsresubmit);
        }
        //MstSanctionHistory
        $scope.sanctionhistory = function (application2sanction_gid, application_gid) {
            $location.url('app/AgrMstSanctionHistory?application2sanction_gid=' + application2sanction_gid + '&application_gid=' + application_gid + '&lspage=' + lspage + '&lsresubmit=' + lsresubmit);
        }
    }
})();
