(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnCheckpointSummaryAssignController', AtmTrnCheckpointSummaryAssignController);

    AtmTrnCheckpointSummaryAssignController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AtmTrnCheckpointSummaryAssignController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnCheckpointSummaryAssignController';
        var auditcreation_gid = $location.search().auditcreation_gid;

        activate();

        function activate() {
            var params = {
                auditcreation_gid: auditcreation_gid
            }
            var url = 'api/AtmTrnAuditCreation/ChecklistAssignView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.checklistassignview_list = resp.data.checklistassignview_list;

            });


        }

        $scope.checkall = function (selected) {

            angular.forEach($scope.checklistassignview_list, function (val) {
                val.checked = selected;
            });
        }
       

        $scope.unassignchecklist = function () {
            var assignList = [];
            angular.forEach($scope.checklistassignview_list, function (val) {

                if (val.checked == true) {
                    var auditcreation2checklist_gid = val.auditcreation2checklist_gid;
                    assignList.push(auditcreation2checklist_gid);

                }
            });
            if (assignList.length == 0) {
                Notify.alert('Select Atleast One Record!');
                return false;
                unlockUI();
            }
            var params = {
                auditcreation2checklist_gid: assignList,

            }

            var url = 'api/AtmTrnAuditCreation/DeleteChecklistAssign';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert('Checklist UnAssigned Successfully!', 'success');
                    $state.go('app.AtmTrnAuditCreationSummary');
                }
                else {
                    Notify.alert('Select Atleast One..!!', 'warning')
                }

            });

        }
        $scope.back = function (val) {
            $state.go('app.AtmTrnAuditCreationSummary');
        }
        $scope.checkpointintent = function (auditcreation2checklist_gid, checkpoint_intent) {
            var modalInstance = $modal.open({
                templateUrl: '/checkpointintent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    auditcreation2checklist_gid: auditcreation2checklist_gid
                }
                lockUI();
                var url = 'api/AtmTrnAuditCreation/GetAuditCreationIntent';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointintent = resp.data.checkpoint_intent;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.description = function (auditcreation2checklist_gid, checkpoint_description) {
            var modalInstance = $modal.open({
                templateUrl: '/checkpointdescription.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                'use strict';

                angular
                    .module('angle')
                var params = {
                    auditcreation2checklist_gid: auditcreation2checklist_gid
                }
                lockUI();
                var url = 'api/AtmTrnAuditCreation/GetAuditCreationDescription';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointdescription = resp.data.checkpoint_description;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.notetoauditor = function (auditcreation2checklist_gid, noteto_auditor) {
            var modalInstance = $modal.open({
                templateUrl: '/notetoauditor.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    auditcreation2checklist_gid: auditcreation2checklist_gid
                }
                lockUI();
                var url = 'api/AtmTrnAuditCreation/GetAuditCreationAuditor';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtnotetoauditor = resp.data.noteto_auditor;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }


    }





})();