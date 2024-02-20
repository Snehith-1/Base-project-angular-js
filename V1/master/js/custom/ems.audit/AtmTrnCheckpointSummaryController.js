(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnCheckpointSummaryController', AtmTrnCheckpointSummaryController);

    AtmTrnCheckpointSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AtmTrnCheckpointSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnCheckpointSummaryController';
        var checklistmaster_gid = $location.search().checklistmaster_gid;
        $scope.checklistmaster_gid = $location.search().checklistmaster_gid;
        var checklistmaster_gid = $scope.checklistmaster_gid;
        var auditcreation_gid = $location.search().auditcreation_gid;
        var checklistmasteradd_gid = $location.search().checklistmasteradd_gid;
        $scope.checklistmasteradd_gid = $location.search().checklistmasteradd_gid;

        activate();

        function activate() {

            //var params = {
            //    checklistmaster_gid: checklistmaster_gid
            //}
            //var url = 'api/AtmTrnAuditCreation/CheckpointCreation';
            //lockUI();
            //SocketService.getparams(url, params).then(function (resp) {
            //    unlockUI();
            //    $scope.checkpointsummary_list = resp.data.auditcheckpointsummary_list;

            //});
            var params = {
                checklistmaster_gid: checklistmaster_gid
            }
            var url = 'api/AtmTrnAuditCreation/CheckpointCreation';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.checkpointsummary_list = resp.data.auditcheckpointsummary_list;

                //for (var i = 0; i < $scope.checkpointsummary_list.length; i++) {

                //    $scope.checkpointsummary_list[i].checked = true;

                //}
                    

                angular.forEach($scope.checkpointsummary_list, function (value,key) {
                                                      
                    if (value.checklist_flag == 'Y')
                    {
                        value.checked = true;
                    }
                    else 
                    {              
                    
                    };        

                });
                

            });

            //var params = {
            //    auditcreation_gid: auditcreation_gid
            //}
            //var url = 'api/AtmTrnAuditCreation/ChecklistAssignView';
            //lockUI();
            //SocketService.getparams(url, params).then(function (resp) {
            //    unlockUI();
            //    $scope.checklistassignview_list = resp.data.checklistassignview_list;

            //});


        }

        $scope.checkall = function (selected) {

            angular.forEach($scope.checkpointsummary_list, function (val) {
                val.checked = selected;
            });
        }
         
                        
        $scope.assignchecklist = function () {
            var assignList = [];        
            angular.forEach($scope.checkpointsummary_list, function (val) {

                if (val.checked == true) {
                    var checklistmasteradd_gid = val.checklistmasteradd_gid;
                    assignList.push(checklistmasteradd_gid);
                    var checklistmaster_gid = val.checklistmaster_gid;

                }
            });
            if (assignList.length == 0) {
                Notify.alert('Select Atleast One Record!');
                return false;
                unlockUI();
            }
            var params = {
                checklistmasteradd_gid: assignList,
                checklistmaster_gid : checklistmaster_gid
            }

            var url = 'api/AtmTrnAuditCreation/PostChecklistAssign';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert('Checklist Assigned Successfully!', 'success');
                    $state.go('app.AtmTrnAuditCreationSummary');
                }
                else {
                    Notify.alert('Select Atleast One..!!', 'warning')
                }

            });

        }


        //$scope.unassignchecklist = function () {
        //    var assignList = [];
        //    angular.forEach($scope.checklistassignview_list, function (val) {

        //        if (val.checked == true) {
        //            var auditcreation2checklist_gid = val.auditcreation2checklist_gid;
        //            assignList.push(auditcreation2checklist_gid);

        //        }
        //    });
        //    if (assignList.length == 0) {
        //        Notify.alert('Select Atleast One Record!');
        //        return false;
        //        unlockUI();
        //    }
        //    var params = {
        //        auditcreation2checklist_gid: assignList,

        //    }

        //    var url = 'api/AtmTrnAuditCreation/DeleteChecklistAssign';
        //    SocketService.post(url, params).then(function (resp) {
        //        if (resp.data.status == true) {
        //            Notify.alert('Checklist UnAssigned Successfully!', 'success');
        //            $state.go('app.AtmTrnAuditCreationSummary');
        //        }
        //        else {
        //            Notify.alert('Select Atleast One..!!', 'warning')
        //        }

        //    });

        //}


        $scope.back = function (val) {
            $state.go('app.AtmTrnAuditCreationSummary');
        }


        $scope.checkpointintent = function (checklistmasteradd_gid, checkpoint_intent) {
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
                    checklistmasteradd_gid: checklistmasteradd_gid
                }
                lockUI();
                var url = 'api/AtmMstChecklistMaster/GetChecklistMasterIntent';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointintent = resp.data.checkpoint_intent;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.description = function (checklistmasteradd_gid, checkpoint_description) {
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
                    checklistmasteradd_gid: checklistmasteradd_gid
                }
                lockUI();
                var url = 'api/AtmMstChecklistMaster/GetChecklistMasterDescription';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointdescription = resp.data.checkpoint_description;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.notetoauditor = function (checklistmasteradd_gid, noteto_auditor) {
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
                    checklistmasteradd_gid: checklistmasteradd_gid
                }
                lockUI();
                var url = 'api/AtmMstChecklistMaster/GetChecklistMasterAuditor';
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