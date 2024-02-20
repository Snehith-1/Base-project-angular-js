(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnMyAuditApproverSummaryController', AtmTrnMyAuditApproverSummaryController);

    AtmTrnMyAuditApproverSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal', 'cmnfunctionService'];

    function AtmTrnMyAuditApproverSummaryController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnMyAuditApproverSummaryController';

        activate();

        function activate() {


            var url = 'api/AtmTrnAuditCreation/GetAuditCreationApprover';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.auditcreation_list = resp.data.auditcreation_list;
                unlockUI();

            });

            var url = 'api/AtmTrnAuditCreation/GetApprovedAuditCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.pendingapprovalaudit_count = resp.data.pendingapproval_count;
                $scope.approvedaudit_count = resp.data.approved_count;
                $scope.rejectedaudit_count = resp.data.rejected_count;

            });

        }

        $scope.pendingapproval_audit = function () {
            $location.url('app/AtmTrnMyAuditApproverSummary')
        }

        $scope.approved_audit = function () {
            $location.url('app/AtmTrnMyApprovedAuditSummary')
        }

        $scope.rejected_audit = function () {
            $location.url('app/AtmTrnMyRejectedAuditSummary')
        }

        $scope.pendingaudit = function () {

            var url = 'api/AtmTrnAuditCreation/GetAuditCreation';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.auditcreation_list = resp.data.auditcreation_list;
                unlockUI();

            });
        }


        $scope.closedaudit = function () {

            var url = 'api/AtmTrnAuditCreation/GetMyClosedAuditTask';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.auditcreation_list = resp.data.auditcreation_list;
                unlockUI();

            });
        }


        $scope.openaudit = function () {

            var url = 'api/AtmTrnAuditCreation/GetMyOpenAuditTask';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.auditcreation_list = resp.data.auditcreation_list;
                unlockUI();

            });
        }


        $scope.auditsonhold = function () {

            var url = 'api/AtmTrnAuditCreation/GetMyOnholdAuditTask';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.auditcreation_list = resp.data.auditcreation_list;
                unlockUI();

            });

        }


        $scope.approvalinformation = function (auditcreation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Approvalinformation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    auditcreation_gid: auditcreation_gid
                }
                var url = 'api/AtmTrnAuditCreation/GetAuditeeSummaryList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.multipleauditee_list = resp.data.multipleauditee_list;
                    unlockUI();

                });
                var url = 'api/AtmTrnAuditCreation/GetAssignedInformation';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lblaudit_maker = resp.data.audit_maker;
                    $scope.lblaudit_checker = resp.data.audit_checker;
                    $scope.lblauditapprover_name = resp.data.auditapprover_name;
                    $scope.lblauditperiod_fromdate = resp.data.auditperiod_fromdate;
                    $scope.lblauditperiod_todate = resp.data.auditperiod_todate;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.createaudit = function () {
            $state.go('app.AtmTrnCreateAudit');
        }
        $scope.approverview = function (val1, val2, val3) {
            $location.url('app/AtmTrnMyAuditApprover360View?hash=' + cmnfunctionService.encryptURL('auditcreation_gid=' + val1 + '&checklistmaster_gid=' + val2 + '&sampleimport_gid=' + val3 + '&lspage=Mypendingapproval'))
        }

        //$scope.view = function (val1) {
        //    $location.url('app/AtmTrnAuditCreationView?auditcreation_gid=' + val1 )
        //}
      
       
        //$scope.edit = function (val1) {
        //    $location.url('app/AtmTrnAuditCreationEdit?auditcreation_gid=' + val1)
        //}

        //$scope.sampleview = function (val1) {
        //    $location.url('app/AtmTrnSampling?auditcreation_gid=' + val1)
        //}

      

    }
})();
