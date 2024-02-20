(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstBusinessHierarchyUpdateController', AgrMstBusinessHierarchyUpdateController);

    AgrMstBusinessHierarchyUpdateController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstBusinessHierarchyUpdateController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstBusinessHierarchyUpdateController';
        //$scope.application_gid = $location.search().application_gid;
        //var application_gid = $scope.application_gid;
        //$scope.employee_gid = $location.search().employee_gid;
        //var employee_gid = $scope.employee_gid;
        //$scope.lspage = $location.search().lspage;
        //var lspage = $scope.lspage;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.application_gid = searchObject.application_gid;
        var application_gid = $scope.application_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.employee_gid = searchObject.employee_gid;
        var employee_gid = $scope.employee_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;

        activate();
        function activate() {
            var param = {
                application_gid: $scope.application_gid
            };

            var url = 'api/AgrAdminApplication/Getapplicationdetails';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lblapplref_no = resp.data.application_no;
                $scope.lblapplicant_name = resp.data.customer_name;
                $scope.lblvertical_name = resp.data.vertical_name;
                $scope.lblprogram_name = resp.data.program_name;
                $scope.lblrm_name = resp.data.rm_name;
            });

            var param = {
                application_gid: $scope.application_gid,
                employee_gid: $scope.employee_gid
            };

            var url = 'api/AgrTrnApplicationApproval/Getapplicationhierarchylist';

            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.clusterhead;
                $scope.zonalhead = resp.data.zonalhead;
                $scope.regionhead = resp.data.regionhead;
                $scope.businesshead = resp.data.businesshead;
            });

            var param = {
                application_gid: $scope.application_gid
            };

            var url = 'api/AgrTrnApplicationApproval/GetAppApprovalSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.approval_list = resp.data.applicationapprovallist;
            });

            var url = 'api/AgrTrnApplicationApproval/GetAppcommentsSummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.comment_list = resp.data.applicationcommentslist;
            });

            var url = 'api/AgrMstApplicationAdd/GetDropDown';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.vertical_list = resp.data.vertical_list;
            });

            var url = 'api/SystemMaster/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employeelist;
                unlockUI();
            });
        }

        $scope.OnchangeVertical = function (cbovertical) {
            var params = {
                vertical_gid: cbovertical.vertical_gid,
                lstype: '',
                lstypegid: ''
            }
            var url = 'api/SystemMaster/GetVerticalProgramList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.program_list = resp.data.program_list;
                unlockUI();
            });
        }

        $scope.OnchangeProgram = function () {
            var url = 'api/SystemMaster/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employeelist;
                unlockUI();
            });
        }

        $scope.searchsubmit = function () {
            if ($scope.cbovertical == null || $scope.cbovertical == undefined || $scope.cbovertical == "" ||
                $scope.cboprogram == null || $scope.cboprogram == undefined || $scope.cboprogram == "" ||
                $scope.cboemployee == null || $scope.cboemployee == undefined || $scope.cboemployee == "") {
                Notify.alert('Kindly Select Vertical, Program and RM Name!', 'warning');
            }
            else {
                var params = {
                    employee_gid: $scope.cboemployee.employee_gid,
                    vertical_gid: $scope.cbovertical.vertical_gid,
                    program_gid: $scope.cboprogram.program_gid
                }
                lockUI();
                var url = 'api/AgrAdminApplication/PostAllHierarchyverticalListSearch';
                SocketService.post(url, params).then(function (resp) {
                    $scope.rmmappingclusterhead = resp.data.clusterhead;
                    $scope.rmmappingregionhead = resp.data.regionhead;
                    $scope.rmmappingzonalhead = resp.data.zonalhead;
                    $scope.rmmappingbusinesshead = resp.data.businesshead;
                    $scope.rmmappingprogram_name = resp.data.program_name;
                    $scope.rmmappinglevel_one = resp.data.level_one;
                    $scope.rmmappinglevel_zero = resp.data.level_zero;
                    $scope.hierarchyavailable_status = resp.data.hierarchyavailable_status;
                    $scope.verticalshow = true;
                    unlockUI();
                });
                $scope.lsemployee_gid = $scope.cboemployee.employee_gid;
                $scope.cboemployee = '';

            }
        }

        $scope.refresh = function () {
            activate();
            $scope.verticalshow = false;
            $scope.program_list = "";
            $scope.vertical_list = "";
            var url = 'api/SystemMaster/GetVerticallist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
                unlockUI();
            });

        }

        $scope.hierarchyupdate_submit = function () {
            if ($scope.hierarchyavailable_status == true) {
                var lsemployee_gid = $scope.lsemployee_gid;
                var params = {
                    application_gid: $scope.application_gid,
                    hierarchyupdate_remarks: $scope.txthierarchy_remarks,
                    employee_gid: lsemployee_gid,
                    submitvertical_gid: $scope.cbovertical.vertical_gid,
                    submitvertical_name: $scope.cbovertical.vertical_name,
                    submitprogram_gid: $scope.cboprogram.program_gid,
                    submitprogram_name: $scope.cboprogram.program_name,
                    application_stage: $scope.lspage
                }
                var url = 'api/AgrAdminApplication/PostBusinessHierarchyUpdate';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        if (lspage == 'BusinessStage') {
                            $state.go('app.AgrMstBusinessHierarchyUpdateSummary');
                        }
                        else if (lspage == 'CreditStage') {
                            $state.go('app.AgrMstCreditStageSummary');
                        }
                        else if (lspage == 'CcStage') {
                            $state.go('app.AgrMstCcStageSummary');
                        }
                        else if (lspage == 'CadPendingStage') {
                            $state.go('app.AgrMstCadPendingStageSummary');
                        }
                        else if (lspage == 'CadAcceptedStage') {
                            $state.go('app.AgrMstCadAcceptedStageSummary');
                        }
                        else if (lspage == 'IncompleteStage') {
                            $state.go('app.AgrIncompleteStageSummary');
                        }
                        else if (lspage == 'ProductStage') {
                            $state.go('app.AgrProductStageSummary');
                        }
                        else {

                        }
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
            else {
                Notify.alert('Hierarchy not mapped for the above vertical and program.Kindly Check it.','warning');
            }
            
        }

        $scope.Back = function () {
            if (lspage == 'BusinessStage') {
                $state.go('app.AgrMstBusinessHierarchyUpdateSummary');
            }
            else if (lspage == 'CreditStage') {
                $state.go('app.AgrMstCreditStageSummary');
            }
            else if (lspage == 'CcStage') {
                $state.go('app.AgrMstCcStageSummary');
            }
            else if (lspage == 'CadPendingStage') {
                $state.go('app.AgrMstCadPendingStageSummary');
            }
            else if (lspage == 'CadAcceptedStage') {
                $state.go('app.AgrMstCadAcceptedStageSummary');
            }
            else if (lspage == 'IncompleteStage') {
                $state.go('app.AgrIncompleteStageSummary');
            }
            else if (lspage == 'ProductStage') {
                $state.go('app.AgrProductStageSummary');
            }
            else {

            }
        }

    }
})();
