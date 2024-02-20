(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCreditManagerRejectRevokeSummaryController', AgrMstCreditManagerRejectRevokeSummaryController);

        AgrMstCreditManagerRejectRevokeSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

    function AgrMstCreditManagerRejectRevokeSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCreditManagerRejectRevokeSummaryController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.employee_gid = $location.search().employee_gid;
        var employee_gid = $scope.employee_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();
        lockUI();
        function activate() {
            var param = {
                application_gid: $scope.application_gid
            };

            var url = 'api/AgrTrnApplicationApproval/Getapplicationdetails';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lblprogram_name = resp.data.program_name;
                $scope.lblapplref_no = resp.data.application_no;
                $scope.lblapplicant_name = resp.data.customer_name;
                $scope.lblcurrentappl_stage = resp.data.approval_status;
                $scope.lblappl_state = resp.data.region;
                $scope.lbloveralllimit_request = resp.data.overalllimit_amount;
                $scope.productlist = resp.data.productlist;
            });

            var url = 'api/AgrTrnCreditApproval/Getcreditheadsview';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.txtcredit_head = resp.data.credithead_name;
                $scope.txtnational_manager = resp.data.nationalcredit_name;
                $scope.txtregional_manager = resp.data.regionalcredit_name;
                $scope.txtcredit_manager = resp.data.creditmanager_name;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.remarks = resp.data.remarks;
                unlockUI();
            });

            var url = 'api/AgrAdminApplication/GetCreditManagerRejectedApplSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditmanagerrejectedappl_list = resp.data.creditmanagerrejectedappl_list;
            });

            var url = 'api/AgrTrnCreditApproval/GetAppcreditApprovalSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.creditapproval_list = resp.data.appcreditapprovallist;
            });

            var url = 'api/AgrTrnCreditApproval/GetAppcreditquerysSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.creditquery_list = resp.data.appcreditquerylist;
            });

            var url = 'api/AgrTrnCreditApproval/GetApprmquerysSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.rmquery_list = resp.data.appcreditquerylist;
            });

            var url = 'api/AgrTrnCreditApproval/CADGetAppqueryStatus';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.querystatus_flag = resp.data.querystatus_flag;
                $scope.approved_flag = resp.data.approved_flag;

            });

            var url = 'api/AgrTrnCreditApproval/GetAppcreditRejectedSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.creditrejected_list = resp.data.Getappcreditrejectedlist;

            if ($scope.creditrejected_list !=null){
                $scope.rejectstatus_flag = $scope.creditrejected_list[0].rejectstatus_flag;
                }

            });  

            var url = 'api/AgrAdminApplication/CreditApplicationRevokeCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditreject_count = resp.data.creditreject_count;
                $scope.credithold_count = resp.data.credithold_count;
                $scope.creditrevoked_count = resp.data.creditrevoked_count;
                $scope.creditmanagerreject_count = resp.data.creditmanagerreject_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });

        }

        $scope.view_querydesc = function (appcreditquery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    appcreditquery_gid: appcreditquery_gid
                }
                var url = 'api/AgrTrnCreditApproval/GetAppcreditqueryesc';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblquery_desc = resp.data.querydesc;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.revoke_submit = function () {
            var params = {
                reason: $scope.txtrevoke_remarks,
                application_gid: application_gid
            }
            var url = 'api/AgrMstAdminApplication/PostCreditManagerRevokeApplication';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.AgrMstCreditManagerRejectRevokeSummary');
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

        $scope.Back = function () {           
            $state.go('app.AgrMstCreditManagerRejectRevokeSummary');
        }

        $scope.hold_applications = function () {
            $state.go('app.AgrMstCreditHoldRevokeSummary');
        }

        $scope.rejected_applications = function () {
            $state.go('app.AgrMstCreditRevokeSummary');
        }

        $scope.business_revoked = function () {
            $state.go('app.AgrMstCreditRevokedApplSummary');
        }

        $scope.creditmanagerreject_applications = function () {
            $state.go('app.AgrMstCreditManagerRejectRevokeSummary');
        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/AgrMstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CreditManagerRejectRevokeAppl');
        }

        $scope.Rejected_revoke = function (application_gid, created_by) {
            $location.url('app/AgrMstCreditManagerRejectRevoke?application_gid=' + application_gid + '&employee_gid=' + created_by + '&lspage=CreditManagerRejectRevokeAppl');
        }

        $scope.revoke_history = function (application_gid) {
            $location.url("app/AgrMstCreditRejectHoldRevokeHistory?hash=" + cmnfunctionService.encryptURL("application_gid=" + application_gid + '&lspage=CreditManagerRejectRevokeAppl'));
      
        }


    }
})();
