(function () {
        'use strict';

        angular
            .module('angle')
            .controller('AtmTrnMyAuditTaskSummaryController', AtmTrnMyAuditTaskSummaryController);

        AtmTrnMyAuditTaskSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$filter', '$route', 'ngTableParams'];

        function AtmTrnMyAuditTaskSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $filter, $route, ngTableParams) {
            /* jshint validthis:true */
            var vm = this;
            vm.title = 'AtmTrnMyAuditTaskSummaryController';
            
            activate();

            function activate() {
                var url = 'api/AtmTrnMyAuditTask/GetMyAuditTask';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.myaudittask_list = resp.data.myaudittask_list;
                    $scope.employee_gid = resp.data.employee_gid;
                    unlockUI();

                });

                var url = 'api/AtmTrnMyAuditTask/GetMyAuditTaskCounts';
                SocketService.get(url).then(function (resp) {
                    unlockUI()
                    $scope.auditsonhold_count = resp.data.auditsonhold_count;
                    $scope.closedaudit_count = resp.data.closedaudit_count;
                    $scope.openaudit_count = resp.data.openaudit_count;
                });

            }

            $scope.assignedaudit = function () {

                var url = 'api/AtmTrnMyAuditTask/GetMyAuditTask';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.myaudittask_list = resp.data.myaudittask_list;
                    $scope.employee_gid = resp.data.employee_gid;
                    unlockUI();

                });
            }

            $scope.closedaudit = function () {

                var url = 'api/AtmTrnMyAuditTask/GetMyClosedAuditTask';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.myaudittask_list = resp.data.myaudittask_list;
                    $scope.employee_gid = resp.data.employee_gid;
                    unlockUI();

                });
            }


            $scope.openaudit = function () {

                var url = 'api/AtmTrnMyAuditTask/GetMyOpenAuditTask';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.myaudittask_list = resp.data.myaudittask_list;
                    $scope.employee_gid = resp.data.employee_gid;
                    unlockUI();

                });
            }


            $scope.auditsonhold = function () {

                var url = 'api/AtmTrnMyAuditTask/GetMyOnholdAuditTask';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.myaudittask_list = resp.data.myaudittask_list;
                    $scope.employee_gid = resp.data.employee_gid;
                    unlockUI();

                });
            }


            //$scope.taggedsamples = function () {

            //    //var url = 'api/AtmTrnMyAuditTask/GettaggedsamplesSummary';
            //    lockUI();
            //    SocketService.get(url).then(function (resp) {
            //        $scope.taggedsamples_list = resp.data.taggedsamples_list;
            //        unlockUI();

            //    });
            //}

            $scope.updateobservations = function (val1) {
                $location.url('app/AtmTrnCheckpointObservation?auditcreation_gid=' + val1)
            }

            $scope.auditraisequery = function (val1) {
                $location.url('app/AtmTrnAuditRaiseQuery?auditcreation_gid=' + val1)
            }

            //$scope.observations = function (val1,val2)
            //{
            //    $location.url('app/AtmTrnCheckpointObservation?auditcreation_gid=' + val1 + '&checklistmasteradd_gid=' + val2)
            //}
            $scope.viewtask = function (val) {
                $location.url('app/AtmTrnMyAuditTask?auditcreation_gid=' + val)
            }
            $scope.approval = function (val2) {
                $location.url('app/AtmTrnApproval?auditcreation_gid=' + val2)
            }
            $scope.assignedquery = function (val) {
                $location.url('app/AtmTrnAssignedQuery?auditcreation_gid=' + val);
            }
            $scope.auditapproval = function (val3) {
                $location.url('app/AtmTrnAuditApproval?auditcreation_gid=' + val3)
            }
            $scope.statusupdate = function (auditcreation_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/statusupdate.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'md'
                });

                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    var params = {
                        auditcreation_gid: auditcreation_gid
                    }
                    var url = 'api/AtmTrnMyAuditTask/EditMyAuditTask';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.auditcreation_gid = resp.data.auditcreation_gid
                        $scope.txtaudit_name = resp.data.audit_name;
                        $scope.txtstatus_update = resp.data.status_update;

                    });

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };

                    

                    $scope.submit = function () {

                        var params = {

                            auditcreation_gid: auditcreation_gid,
                            status_update: $scope.status_update

                        }

                        var url = 'api/AtmTrnMyAuditTask/GetMyAuditTaskStatus';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
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
    
            $scope.getApprovalRequest = function (auditcreation_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/getApprovalRequest.html',
                    controller: ModalInstanceCtrl,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {
                    var params = {
                        auditcreation_gid: auditcreation_gid
                    }

                    var url = 'api/AtmMstAuditMapping/GetAuditMappingChecker';
                    lockUI();
                    SocketService.get(url).then(function (resp) {
                        unlockUI();
                        $scope.auditmapping_list = resp.data.auditmappingchecker_list;

                    });
                    var url = 'api/AtmTrnMyAuditTask/TmpAllMembersDelete';
                    SocketService.get(url).then(function (resp) {

                    });
                    $scope.ok = function () {
                        modalInstance.close('closed');
                    };
                    $scope.ApprovalMembercancel = function (tmpapprovalmember_gid) {
                        var params = {
                            tmpapprovalmember_gid: tmpapprovalmember_gid,
                            auditcreation_gid: auditcreation_gid,
                        }
                        var url = 'api/AtmTrnMyAuditTask/TmpApprovalMembersDelete';
                        SocketService.post(url, params).then(function (resp) {
                            $scope.approvalmember = resp.data.approvalmember;                          
                        });



                    }

                    $scope.Changed = function (cboapproval_membername) {
                        var params = {
                            approvalgid: $scope.cboapproval_membername.auditmapping_gid,
                            approvalname: $scope.cboapproval_membername.employee_name,
                            auditcreation_gid: auditcreation_gid,
                        }

                        lockUI();
                        var url = "api/AtmTrnMyAuditTask/TempApprovalMember";
                        SocketService.post(url, params).then(function (resp) {

                            if (resp.data.status == true) {
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000

                                });
                                unlockUI();
                                $scope.current = $state.current.name;
                                ScopeValueService.store("dataldCtrl", $scope);
                                //$state.go('app.pageredirect');
                                $scope.cboapproval_membername = "";
                                var params = {
                                    auditcreation_gid: auditcreation_gid
                                }
                                var url = 'api/AtmTrnMyAuditTask/TmpApprovalMembersView';
                                SocketService.getparams(url, params).then(function (resp) {
                                    $scope.approvalmember = resp.data.approvalmember;
                                });                               
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();

                            }
                        });


                    }

                    $scope.getapprovalclick = function () {
                        var params = {
                            approve_remarks: $scope.approve_remarks,
                            approve_type: $scope.approve_type,
                            auditcreation_gid: auditcreation_gid,                     
                           
                        }

                        lockUI();
                        var url = "api/AtmTrnMyAuditTask/PostApprovalGet";
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                modalInstance.close('closed');
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                                activate();

                            }
                            else {
                                //modalInstance.close('closed');
                                alert(resp.data.message, {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();

                            }
                        });
                    }
                }
            }

          
            $scope.raisequery = function (auditcreation_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/raisequery.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'md'
                });

                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    var params = {
                        auditcreation_gid: auditcreation_gid
                    }
                    var url = 'api/AtmTrnMyAuditTask/EditMyAuditTask';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.auditcreation_gid = resp.data.auditcreation_gid

                    });


                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };

                    var url = 'api/SystemMaster/GetEmployeelist';
                    lockUI();
                    SocketService.get(url).then(function (resp) {
                        $scope.employee_list = resp.data.employeelist;
                        unlockUI();
                    });

                    

                    $scope.submit = function () {

                        var params = {
                            auditcreation_gid: $scope.auditcreation_gid,
                            employe: $scope.cboemployee_name,
                            description: $scope.txtdescription,
                           
                        }

                        var url = 'api/AtmTrnMyAuditTask/PostRaiseQuery';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
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
        }
    })();


