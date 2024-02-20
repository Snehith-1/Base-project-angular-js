(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstDisbursementAssignmentSummaryController', MstDisbursementAssignmentSummaryController);

    MstDisbursementAssignmentSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstDisbursementAssignmentSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstDisbursementAssignmentSummaryController';

        activate();

        function activate() {
            var url = 'api/MstCreditOpsApplication/GetDisbursementPendingSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.disbursementrequest_list = resp.data.disbursementrequest_list;
            });

            var url = 'api/MstCreditOpsApplication/DisbursementAssignCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.pending_count = resp.data.pending_count;
                $scope.assigned_count = resp.data.assigned_count;
                $scope.lstotalcount = resp.data.lstotalcount;
                $scope.rejected_count = resp.data.rejected_count;
            });
        }

        $scope.assigned_disbursement = function () {
            $location.url('app/MstAssignedDisbursementSummary');
        }

        $scope.pending_disbursement = function () {
            $location.url('app/MstDisbursementAssignmentSummary');
        } 

        $scope.rejected_disbursement = function () {
            $location.url('app/MstDisbursementRejectedSummary');
        } 

        $scope.assign_disbursement = function (application_gid, rmdisbursementrequest_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/assigndisbursement.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {                           

                $scope.creditopsgroup_change = function (cbocreditopsgroup) {
                    var params = {
                        creditopsgroupmapping_gid: cbocreditopsgroup
                    }
                    var url = 'api/MstCreditOpsApplication/GetCreditOps2Heads';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.Creditops_maker = resp.data.Creditops_maker;
                        $scope.Creditops_checker = resp.data.Creditops_checker;
                    });
                }

                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.approver_list = resp.data.employeelist;
                    unlockUI();
                });

                var params = {
                    application_gid: application_gid
                }
                var url = 'api/MstCreditOpsApplication/GetCreditOpsGroupDropDown';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.creditOpsGrouplist = resp.data.creditOpsGrouplist;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.assign_application = function () {

                    var lscreditopsmaker_gid = '';
                    var lscreditopsmaker_name = '';
                    var lscreditopschecker_gid = '';
                    var lscreditopschecker_name = '';
                    //var lscreditopsapprover_gid = '';
                    //var lscreditopsapprover_name = '';

                    var creditopsgroupname = $('#creditopsgroup :selected').text();
                    if ($scope.cbocreditops_maker != undefined || $scope.cbocreditops_maker != null) {
                        lscreditopsmaker_gid = $scope.cbocreditops_maker.employee_gid;
                        lscreditopsmaker_name = $scope.cbocreditops_maker.employee_name;
                    }
                    if ($scope.cborcreditops_checker != undefined || $scope.cborcreditops_checker != null) {
                        lscreditopschecker_gid = $scope.cborcreditops_checker.employee_gid;
                        lscreditopschecker_name = $scope.cborcreditops_checker.employee_name;
                    }
                    //if ($scope.cbocreditops_approver != undefined || $scope.cbocreditops_approver != null) {
                    //    lscreditopsapprover_gid = $scope.cbocreditops_approver.employee_gid;
                    //    lscreditopsapprover_name = $scope.cbocreditops_approver.employee_name;
                    //}                   

                    var params = {
                        application_gid: application_gid,
                        creditopsgroup_gid: $scope.cbocreditopsgroup,
                        creditopsgroup_name: creditopsgroupname,
                        creditopsmaker_gid: lscreditopsmaker_gid,
                        creditopsmaker_name: lscreditopsmaker_name,
                        creditopschecker_gid: lscreditopschecker_gid,
                        creditopschecker_name: lscreditopschecker_name,
                        remarks: $scope.txtremarks,
                        rmdisbursementrequest_gid: rmdisbursementrequest_gid
                        //creditopsapprover_gid: lscreditopsapprover_gid,
                        //creditopsapprover_name: lscreditopsapprover_name,
                       
                    }
                    var url = 'api/MstCreditOpsApplication/PostDisbursementAssignment';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
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

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=DisbursementAssignment');
        }

    }
})();
