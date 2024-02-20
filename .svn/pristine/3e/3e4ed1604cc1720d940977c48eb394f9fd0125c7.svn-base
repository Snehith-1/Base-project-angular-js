(function () {
    'use strict';
    angular
        .module('angle')
        .controller('AtmTrnCheckpointObservationController', AtmTrnCheckpointObservationController);

    AtmTrnCheckpointObservationController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AtmTrnCheckpointObservationController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnCheckpointObservationController';
        var auditcreation_gid = $location.search().auditcreation_gid;
        var auditcreation2checklist_gid = $location.search().auditcreation2checklist_gid;
        $scope.sampleimport_gid = $location.search().sampleimport_gid;
        var sampleimport_gid = $scope.sampleimport_gid;

        activate();

        function activate() {
                            
            

            var url = 'api/AtmTrnMyAuditTask/EditCheckpointObservation';
            var params = {
                auditcreation_gid: auditcreation_gid,

            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cboauditfrequency = resp.data.auditfrequency_gid,
                  $scope.cboauditfrequency = resp.data.auditfrequency_name,
                 $scope.cboauditname = resp.data.checklistmaster_gid,
                  $scope.cboauditname = resp.data.audit_name,
                  $scope.cboauditmaker = resp.data.employee_gid,
                  $scope.cboauditmaker = resp.data.audit_maker,
                 $scope.cboauditchecker = resp.data.auditmapping_gid,
                  $scope.cboauditchecker = resp.data.audit_checker,
                  $scope.cboauditapprover = resp.data.employee_gid,
                  $scope.cboauditapprover = resp.data.audit_approver,
                 $scope.cboauditpriority = resp.data.auditpriority_gid,
                  $scope.cboauditpriority = resp.data.auditpriority_name,
                $scope.txtdue_date = resp.data.due_date,
                $scope.txtreport_date = resp.data.report_date,
                $scope.txtperiod_from = resp.data.periodfrom_date,
                $scope.txtperiod_to = resp.data.auditperiod_to,
                $scope.txtaudit_ref_no = resp.data.audit_uniqueno,
                  $scope.cboauditmaker_name = resp.data.auditmaker_name,
                $scope.cboauditchecker_name = resp.data.auditchecker_name,
                unlockUI();
            });
            var url = 'api/AtmTrnMyAuditTask/CheckpointObservationView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.checkpointobservation_list = resp.data.checkpointobservationview_list;
                $scope.txttotal_score = resp.data.total_score;

                for (var i = 0; i < $scope.checkpointobservation_list.length; i++) {
                    $scope.checkpointobservation_list[i].checked = true;

                }

                angular.forEach($scope.checkpointobservation_list, function (value, key) {

                    if (value.capture_score == value.yes_score) {
                        value.yes_radio = true;
                    }
                    else if (value.capture_score == value.no_score) {
                        value.no_radio = true;
                    }
                    else if (value.capture_score == value.partial_score) {
                        value.partialscore_radio = true;
                    }
                    else if (value.capture_score == value.na_score) {
                        value.nascore_radio = true;
                    }
                });
            });
            var url = 'api/AtmTrnSampling/GetSample';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI()
                $scope.sample_list = resp.data.sample_list

            });
        }
        $scope.onselected = function (val, val1,val2) {
            $scope.total_score = val;
            $scope.auditcreation2checklist_gid = val1;
            $scope.checklistmasteradd_gid = val2;

            var params = {
                auditcreation_gid: auditcreation_gid,
                auditcreation2checklist_gid: $scope.auditcreation2checklist_gid,
                checklistmasteradd_gid: $scope.checklistmasteradd_gid,
                capture_totalscore: $scope.total_score,

            }


            var url = 'api/AtmTrnMyAuditTask/PostObservationTotalAmount';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.txttotal_score = resp.data.total_amount;

                    unlockUI()
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
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
        $scope.submitobservation = function () {
            var assignList = [];
            angular.forEach($scope.checkpointobservation_list, function (val) {

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
                auditcreation_gid: auditcreation_gid,
            }

            var url = 'api/AtmTrnMyAuditTask/PostCheckpointObservation';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert('Check point  Successfully!', 'success');
                    $state.go('app.AtmTrnMyAuditTaskSummary');
                }

                else {
                    Notify.alert('Select Atleast One..!!', 'warning')
                }

                $scope.txttotal_score = "";
            });

        }



        $scope.back = function (val) {
            $state.go('app.AtmTrnMyAuditTaskSummary');
        }


        $scope.checkpointintent = function (auditcreation_gid, checkpoint_intent) {
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
                    auditcreation_gid: auditcreation_gid
                }
                lockUI();
                var url = 'api/AtmTrnMyAuditTask/GetAuditCreationIntent';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointintent = resp.data.checkpoint_intent;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.description = function (auditcreation_gid, checkpoint_description) {
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
                    auditcreation_gid: auditcreation_gid
                }
                lockUI();
                var url = 'api/AtmTrnMyAuditTask/GetAuditCreationDescription';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcheckpointdescription = resp.data.checkpoint_description;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.notetoauditor = function (auditcreation_gid, noteto_auditor) {
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
                    auditcreation_gid: auditcreation_gid
                }
                lockUI();
                var url = 'api/AtmTrnMyAuditTask/GetAuditCreationAuditor';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtnotetoauditor = resp.data.noteto_auditor;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }


        $scope.btntaguser = function (auditcreation_gid, sampleimport_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/taguser.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    sampleimport_gid: sampleimport_gid
                }

                var url = 'api/AtmTrnSampling/GetSampleName';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sampleimport_gid = resp.data.sampleimport_gid;
                    $scope.txtsample_name = resp.data.sample_name;
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

                $scope.btnconfirm = function () {

                    var params = {
                        employelist: $scope.cboemployee_name,
                        sample_name: $scope.txtsample_name,
                        sampleimport_gid: sampleimport_gid,
                        auditcreation_gid: auditcreation_gid
                    }

                    var url = 'api/AtmTrnSampling/GetTagUser';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $location.url('app/AtmTrnCheckpointObservation?auditcreation_gid=' + val1)
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

        $scope.showPopover = function (sampleimport_gid, sample_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showemployee.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    sampleimport_gid: sampleimport_gid
                }
                lockUI();
                var url = 'api/AtmTrnSampling/GetEmployeeName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.employee_name = resp.data.employee_name;
                    $scope.sample_name = resp.data.sample_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }


        $scope.raisequery = function (sampleimport_gid,auditcreation_gid) {
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
                    sampleimport_gid: sampleimport_gid
                }
                var url = 'api/AtmTrnSampling/EditSampleQuery';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sampleimport_gid = resp.data.sampleimport_gid

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
                        sampleimport_gid: $scope.sampleimport_gid,
                        employelist: $scope.cboemployee_name,
                        description: $scope.txtdescription,
                        auditcreation_gid: auditcreation_gid,

                    }

                    var url = 'api/AtmTrnSampling/PostRaiseQuery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $location.url('app/AtmTrnSampling?auditcreation_gid=' + val1)
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

        $scope.assignedquery = function (val, val1) {
            $location.url('app/AtmTrnSampleAssignedQuery?auditcreation_gid=' + val + '&sampleimport_gid=' + val1);
        }


        $scope.view = function (val, val1) {
            //var sampleimport_gid = $scope.sampleimport_gid;

            //var auditcreation_gid = $scope.auditcreation_gid;
            $location.url('app/AtmTrnSamplingView?auditcreation_gid=' + val + '&sampleimport_gid=' + val1)
        }


        $scope.back = function () {
            $state.go('app.AtmTrnMyAuditTaskSummary');
        }



    }

})();