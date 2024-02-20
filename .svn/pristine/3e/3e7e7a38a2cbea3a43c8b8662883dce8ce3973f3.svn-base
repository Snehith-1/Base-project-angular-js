(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCADGroupProcessAssignController', AgrTrnCADGroupProcessAssignController);

    AgrTrnCADGroupProcessAssignController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnCADGroupProcessAssignController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCADGroupProcessAssignController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.processshow = false;
        $scope.acceptshow = false;
        $scope.cadgroupacceptshow = false;
        //$scope.selectallmenu_show = true;
        activate();
        lockUI();
        function activate() {

            var url = 'api/AgrMstCADGroup/GetCADGroup';
            SocketService.get(url).then(function (resp) {
                $scope.cadgroup_list = resp.data.cadgroup;
                unlockUI();
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrTrnCAD/GetMenu';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.menu_list = resp.data.menu_list;
                unlockUI();
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrTrnCAD/GetProcessTypeSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.mdlprocesstype_list = resp.data.processtype_list;
                $scope.processtype_name = resp.data.processtype_name;
                $scope.cadgroup_name = resp.data.cadgroup_name;
                $scope.cadgroup_gid = resp.data.cadgroup_gid;
                unlockUI();
                if ($scope.processtype_name == 'Accept') {
                    $scope.cboprocesstype = '';
                    $scope.processdisabled = true;
                    $scope.processenabled = false;
                    $scope.processautoenabled = false;
                    $scope.cadgroupacceptshow = false;
                    $scope.acceptshow = true;
                    $scope.selectmenu_show = true;
                    $scope.selectallmenu_show = false;
                    var params = {
                        cadgroup_gid: $scope.cadgroup_gid
                    }
                    var url = 'api/AgrTrnCAD/GetCADMembers';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.cadmembers_list = resp.data.cadmembers;
                        unlockUI();
                    });
                }
                else {
                    $scope.cadgroupacceptshow = false;
                    if (resp.data.onboarding_status == "Direct")
                        $scope.processautoenabled = true; 
                    else
                        $scope.processenabled = true;
                    $scope.selectmenu_show = false;
                    $scope.selectallmenu_show = false;
                }

            });



        }

        $scope.change_process = function (cboprocesstype) {
            if ($scope.cboprocesstype == 'Sendback to CC') {
                $scope.processshow = true;
                $scope.acceptshow = false;
                $scope.cadgroupacceptshow = false;
                $scope.selectmenu_show = false;
                $scope.selectallmenu_show = false;
            }
            else if ($scope.cboprocesstype == 'Sendback to Credit Underwriting') {
                $scope.processshow = true;
                $scope.acceptshow = false;
                $scope.cadgroupacceptshow = false;
                $scope.selectmenu_show = false;
                $scope.selectallmenu_show = false;
            }
            else if ($scope.cboprocesstype == 'Accept') {
                $scope.acceptshow = true;
                $scope.cadgroupacceptshow = true;
                $scope.processshow = false;
                $scope.selectmenu_show = true;
                $scope.selectallmenu_show = false;
            }
            else if ($scope.cboprocesstype == 'Reject') {
                $scope.processshow = true;
                $scope.acceptshow = false;
                $scope.cadgroupacceptshow = false;
                $scope.selectmenu_show = false;
                $scope.selectallmenu_show = false;
            }
            else if ($scope.cboprocesstype == 'Sendback to Credit Without CC') {
                $scope.processshow = true;
                $scope.acceptshow = false;
                $scope.cadgroupacceptshow = false;
                $scope.selectmenu_show = false;
                $scope.selectallmenu_show = false;
            }
            else {
                $scope.processshow = false;
                $scope.acceptshow = false;
                $scope.cadgroupacceptshow = false;
            }
        }

        $scope.Process_add = function () {
            if (($scope.cboprocesstype == 'Sendback to CC' || $scope.cboprocesstype == 'Sendback to Credit Underwriting') && ($scope.remarks == '' || $scope.remarks == null)) {
                Notify.alert('Kindly Enter the Remarks Details', 'warning');
            }
            else if (($scope.cboprocesstype == 'Accept') && (
                $scope.cbocadgroup_name == '' || $scope.cbocadgroup_name == null || $scope.cbocadgroup_name == undefined ||
                $scope.cbomaker_name == '' || $scope.cbomaker_name == null || $scope.cbomaker_name == undefined ||
                $scope.cbochecker_name == '' || $scope.cbochecker_name == null || $scope.cbochecker_name == undefined ||
                $scope.cboapprover_name == '' || $scope.cboapprover_name == null || $scope.cboapprover_name == undefined ||
                $scope.cbomenu_name == '' || $scope.cbomenu_name == null || $scope.cbomenu_name == undefined)) {
                Notify.alert('Kindly Enter All Mandatory Fields', 'warning');
            }
            else if (($scope.processtype_name == 'Accept') && (
                $scope.cbomaker_name == '' || $scope.cbomaker_name == null || $scope.cbomaker_name == undefined ||
                $scope.cbochecker_name == '' || $scope.cbochecker_name == null || $scope.cbochecker_name == undefined ||
                $scope.cboapprover_name == '' || $scope.cboapprover_name == null || $scope.cboapprover_name == undefined ||
                $scope.cbomenu_name == '' || $scope.cbomenu_name == null || $scope.cbomenu_name == undefined)) {
                Notify.alert('Kindly Assign the Menu', 'warning');
            }
            else if (($scope.mdlprocesstype_list == null || $scope.mdlprocesstype_list == '' || $scope.mdlprocesstype_list == undefined) &&
                ($scope.cboprocesstype == '' || $scope.cboprocesstype == null)) {
                Notify.alert('Kindly Select Process Type Detail', 'warning');
            }
            else {
                if ($scope.rdbassign == 'Menuselection') {
                    var application_gid = $scope.application_gid;
                    if ($scope.processtype_name == 'Accept') {
                        var processtype_name = $scope.processtype_name;
                        var cadgroup_name = $scope.cadgroup_name;
                        var cadgroup_gid = $scope.cadgroup_gid;
                    }
                    else {
                        var processtype_name = $scope.cboprocesstype;
                        var cadgroup_gid = $scope.cbocadgroup_name.cadgroup_gid;
                        var cadgroup_name = $scope.cbocadgroup_name.cadgroup_name;
                    }
                    var params = {
                        application_gid: application_gid,
                        processtype_name: processtype_name,
                        cadgroup_gid: cadgroup_gid,
                        cadgroup_name: cadgroup_name,
                        menulist: $scope.cbomenu_name,
                        maker_gid: $scope.cbomaker_name.employee_gid,
                        maker_name: $scope.cbomaker_name.employee_name,
                        checker_gid: $scope.cbochecker_name.employee_gid,
                        checker_name: $scope.cbochecker_name.employee_name,
                        approver_gid: $scope.cboapprover_name.employee_gid,
                        approver_name: $scope.cboapprover_name.employee_name,
                        assign_type: $scope.rdbassign,
                        applyall_flag: "N"
                    }
                    var url = "api/AgrTrnCAD/PostProcessType";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                            var params = {
                                application_gid: application_gid
                            }
                            var url = 'api/AgrTrnCAD/GetProcessTypeSummary';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.mdlprocesstype_list = resp.data.processtype_list;
                                unlockUI();
                            });
                            activate();
                            //$scope.cboprocesstype = '';
                            //$scope.cbocadgroup_name = '';
                            $scope.cbomenu_name = '';
                            $scope.cbomaker_name = '';
                            $scope.cbochecker_name = '';
                            $scope.cboapprover_name = '';
                            //$scope.rdbassign = '';
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });
                }
                else {
                    var application_gid = $scope.application_gid;
                    if ($scope.processtype_name == 'Accept') {
                        var processtype_name = $scope.processtype_name;
                        var cadgroup_name = $scope.cadgroup_name;
                        var cadgroup_gid = $scope.cadgroup_gid;
                    }
                    else {
                        var processtype_name = $scope.cboprocesstype;
                        var cadgroup_gid = $scope.cbocadgroup_name.cadgroup_gid;
                        var cadgroup_name = $scope.cbocadgroup_name.cadgroup_name;
                    }
                    var params = {
                        application_gid: application_gid,
                        processtype_name: processtype_name,
                        cadgroup_gid: cadgroup_gid,
                        cadgroup_name: cadgroup_name,
                        maker_gid: $scope.cbomaker_name.employee_gid,
                        maker_name: $scope.cbomaker_name.employee_name,
                        checker_gid: $scope.cbochecker_name.employee_gid,
                        checker_name: $scope.cbochecker_name.employee_name,
                        approver_gid: $scope.cboapprover_name.employee_gid,
                        approver_name: $scope.cboapprover_name.employee_name,
                        assign_type: $scope.rdbassign,
                        applyall_flag: "Y"
                    }
                    var url = "api/AgrTrnCAD/PostProcessType";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                            var params = {
                                application_gid: application_gid
                            }
                            var url = 'api/AgrTrnCAD/GetProcessTypeSummary';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.mdlprocesstype_list = resp.data.processtype_list;
                                unlockUI();
                            });
                            activate();
                            //$scope.cboprocesstype = '';
                            //$scope.cbocadgroup_name = '';
                            $scope.cbomenu_name = '';
                            $scope.cbomaker_name = '';
                            $scope.cbochecker_name = '';
                            $scope.cboapprover_name = '';
                            //$scope.rdbassign = '';
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });
                }
            }
        }

        $scope.applytoall_add = function () {
            if (($scope.cboprocesstype == 'Sendback to CC' || $scope.cboprocesstype == 'Sendback to Credit Underwriting') && ($scope.remarks == '' || $scope.remarks == null)) {
                Notify.alert('Kindly Enter the Remarks Details', 'warning');
            }
            else if (($scope.cboprocesstype == 'Accept') && (
                $scope.cbocadgroup_name == '' || $scope.cbocadgroup_name == null || $scope.cbocadgroup_name == undefined ||
                $scope.cbomaker_name == '' || $scope.cbomaker_name == null || $scope.cbomaker_name == undefined ||
                $scope.cbochecker_name == '' || $scope.cbochecker_name == null || $scope.cbochecker_name == undefined ||
                $scope.cboapprover_name == '' || $scope.cboapprover_name == null || $scope.cboapprover_name == undefined)) {
                Notify.alert('Kindly Enter All Mandatory Fields', 'warning');
            }
            else if (($scope.processtype_name == 'Accept') && (
                $scope.cbomaker_name == '' || $scope.cbomaker_name == null || $scope.cbomaker_name == undefined ||
                $scope.cbochecker_name == '' || $scope.cbochecker_name == null || $scope.cbochecker_name == undefined ||
                $scope.cboapprover_name == '' || $scope.cboapprover_name == null || $scope.cboapprover_name == undefined)) {
                Notify.alert('Kindly Enter All Mandatory Fields', 'warning');
            }
            else if (($scope.mdlprocesstype_list == null || $scope.mdlprocesstype_list == '' || $scope.mdlprocesstype_list == undefined) &&
                ($scope.cboprocesstype == '' || $scope.cboprocesstype == null)) {
                Notify.alert('Kindly Select Process Type Detail', 'warning');
            }
            else {
                if ($scope.rdbassign == 'Menuselection') {
                    var application_gid = $scope.application_gid;
                    if ($scope.processtype_name == 'Accept') {
                        var processtype_name = $scope.processtype_name;
                        var cadgroup_name = $scope.cadgroup_name;
                        var cadgroup_gid = $scope.cadgroup_gid;
                    }
                    else {
                        var processtype_name = $scope.cboprocesstype;
                        var cadgroup_gid = $scope.cbocadgroup_name.cadgroup_gid;
                        var cadgroup_name = $scope.cbocadgroup_name.cadgroup_name;
                    }
                    var params = {
                        application_gid: application_gid,
                        processtype_name: processtype_name,
                        cadgroup_gid: cadgroup_gid,
                        cadgroup_name: cadgroup_name,
                        menulist: $scope.cbomenu_name,
                        maker_gid: $scope.cbomaker_name.employee_gid,
                        maker_name: $scope.cbomaker_name.employee_name,
                        checker_gid: $scope.cbochecker_name.employee_gid,
                        checker_name: $scope.cbochecker_name.employee_name,
                        approver_gid: $scope.cboapprover_name.employee_gid,
                        approver_name: $scope.cboapprover_name.employee_name,
                        assign_type: $scope.rdbassign,
                        applyall_flag: "N"
                    }
                    var url = "api/AgrTrnCAD/PostProcessType";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                            var params = {
                                application_gid: application_gid
                            }
                            var url = 'api/AgrTrnCAD/GetProcessTypeSummary';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.mdlprocesstype_list = resp.data.processtype_list;
                                unlockUI();
                            });
                            activate();
                            //$scope.cboprocesstype = '';
                            //$scope.cbocadgroup_name = '';
                            $scope.cbomenu_name = '';
                            $scope.cbomaker_name = '';
                            $scope.cbochecker_name = '';
                            $scope.cboapprover_name = '';
                            //$scope.rdbassign = '';
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });
                }
                else {
                    var application_gid = $scope.application_gid;
                    if ($scope.processtype_name == 'Accept') {
                        var processtype_name = $scope.processtype_name;
                        var cadgroup_name = $scope.cadgroup_name;
                        var cadgroup_gid = $scope.cadgroup_gid;
                    }
                    else {
                        var processtype_name = $scope.cboprocesstype;
                        var cadgroup_gid = $scope.cbocadgroup_name.cadgroup_gid;
                        var cadgroup_name = $scope.cbocadgroup_name.cadgroup_name;
                    }
                    var params = {
                        application_gid: application_gid,
                        processtype_name: processtype_name,
                        cadgroup_gid: cadgroup_gid,
                        cadgroup_name: cadgroup_name,
                        maker_gid: $scope.cbomaker_name.employee_gid,
                        maker_name: $scope.cbomaker_name.employee_name,
                        checker_gid: $scope.cbochecker_name.employee_gid,
                        checker_name: $scope.cbochecker_name.employee_name,
                        approver_gid: $scope.cboapprover_name.employee_gid,
                        approver_name: $scope.cboapprover_name.employee_name,
                        assign_type: $scope.rdbassign,
                        applyall_flag: "Y"
                    }
                    var url = "api/AgrTrnCAD/PostProcessType";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                            var params = {
                                application_gid: application_gid
                            }
                            var url = 'api/AgrTrnCAD/GetProcessTypeSummary';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.mdlprocesstype_list = resp.data.processtype_list;
                                unlockUI();
                            });
                            activate();
                            //$scope.cboprocesstype = '';
                            //$scope.cbocadgroup_name = '';
                            $scope.cbomenu_name = '';
                            $scope.cbomaker_name = '';
                            $scope.cbochecker_name = '';
                            $scope.cboapprover_name = '';
                            //$scope.rdbassign = '';
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });
                }
            }
        }

        $scope.Submit = function () {

            if (($scope.cboprocesstype == 'Sendback to CC' || $scope.cboprocesstype == 'Sendback to Credit Underwriting' || $scope.cboprocesstype == 'Sendback to Credit Without CC') && ($scope.remarks == '' || $scope.remarks == null)) {
                Notify.alert('Kindly Enter the Remarks Details', 'warning');
            }
            else if (($scope.cboprocesstype == 'Accept') && ($scope.cbocadgroup_name == '' || $scope.cbocadgroup_name == null || $scope.cbomaker_name == '' || $scope.cbomaker_name == null || $scope.cbochecker_name == '' || $scope.cbochecker_name == null || $scope.cboapprover_name == '' || $scope.cboapprover_name == null || $scope.cbomenu_name == '' || $scope.cbomenu_name == null)) {
                Notify.alert('Kindly Enter All Mandatory Fields', 'warning');
            }
            else if (($scope.mdlprocesstype_list == null || $scope.mdlprocesstype_list == '' || $scope.mdlprocesstype_list == undefined) && ($scope.cboprocesstype == '' || $scope.cboprocesstype == null)) {
                Notify.alert('Kindly Select Process Type Detail', 'warning');
            }
            else {
                var processtype;
                if ($scope.mdlprocesstype_list != null) {
                    processtype = 'Accept'
                    var application_gid = $scope.application_gid;
                    var params = {
                        application_gid: application_gid,
                        processtype_remarks: $scope.remarks,
                        process_type: processtype
                    }

                    var url = "api/AgrTrnCAD/UpdateProcessType";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                            if (lspage == 'PendingCADReview') {
                                $location.url('app/AgrTrnPendingCADReview');
                            }
                            else if (lspage == 'CADAcceptanceCustomers') {
                                $location.url('app/AgrTrnCadAcceptedCustomers');
                            }
                            else {

                            }
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });
                    //if (lspage == 'PendingCADReview') {
                    //    $location.url('app/AgrTrnPendingCADReview');
                    //}
                    //else if (lspage == 'CADAcceptanceCustomers') {
                    //    $location.url('app/AgrTrnCadAcceptedCustomers');
                    //}
                    //else {

                    //}

                }

                else if ($scope.cboprocesstype == 'Sendback to CC') {
                    var application_gid = $scope.application_gid;
                    var processtype;
                    if ($scope.mdlprocesstype_list != null) {
                        processtype = 'Accept'
                    }
                    else {
                        processtype = $scope.cboprocesstype
                    }
                    var params = {
                        application_gid: application_gid,
                        cadtocc_reason: $scope.remarks
                    }
                    var url = "api/AgrTrnCAD/PostRevertCADtoCC";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                            var application_gid = $scope.application_gid;
                            var params = {
                                application_gid: application_gid,
                                processtype_remarks: $scope.remarks,
                                process_type: processtype
                            }

                            var url = "api/AgrTrnCAD/UpdateProcessType";
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status = true) {
                                    //Notify.alert(resp.data.message, 'success');
                                    if (lspage == 'PendingCADReview') {
                                        $location.url('app/AgrTrnPendingCADReview');
                                    }
                                    else if (lspage == 'CADAcceptanceCustomers') {
                                        $location.url('app/AgrTrnCadAcceptedCustomers');
                                    }
                                    else {

                                    }
                                }
                                else {
                                    Notify.alert(resp.data.message, 'warning');
                                    activate();
                                }
                            });
                            if (lspage == 'PendingCADReview') {
                                $location.url('app/AgrTrnPendingCADReview');
                            }
                            else if (lspage == 'CADAcceptanceCustomers') {
                                $location.url('app/AgrTrnCadAcceptedCustomers');
                            }
                            else {

                            }
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });
                }
                else if ($scope.cboprocesstype == 'Sendback to Credit Underwriting') {
                    var application_gid = $scope.application_gid;
                    var processtype;
                    if ($scope.mdlprocesstype_list != null) {
                        processtype = 'Accept'
                    }
                    else {
                        processtype = $scope.cboprocesstype
                    }
                    var params = {
                        application_gid: application_gid,
                        cadtocredit_reason: $scope.remarks
                    }
                    var url = "api/AgrTrnCAD/PostRevertCADtoCredit";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                            var application_gid = $scope.application_gid;
                            var params = {
                                application_gid: application_gid,
                                processtype_remarks: $scope.remarks,
                                process_type: processtype
                            }

                            var url = "api/AgrTrnCAD/UpdateProcessType";
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status = true) {
                                    //Notify.alert(resp.data.message, 'success');
                                    if (lspage == 'PendingCADReview') {
                                        $location.url('app/AgrTrnPendingCADReview');
                                    }
                                    else if (lspage == 'CADAcceptanceCustomers') {
                                        $location.url('app/AgrTrnCadAcceptedCustomers');
                                    }
                                    else {

                                    }
                                }
                                else {
                                    Notify.alert(resp.data.message, 'warning');
                                    activate();
                                }
                            });
                            if (lspage == 'PendingCADReview') {
                                $location.url('app/AgrTrnPendingCADReview');
                            }
                            else if (lspage == 'CADAcceptanceCustomers') {
                                $location.url('app/AgrTrnCadAcceptedCustomers');
                            }
                            else {

                            }
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });
                }

                else if ($scope.cboprocesstype == 'Reject') {
                    var application_gid = $scope.application_gid;
                    var processtype;
                    if ($scope.mdlprocesstype_list != null) {
                        processtype = 'Reject'
                    }
                    else {
                        processtype = $scope.cboprocesstype
                    }
                    var params = {
                        application_gid: application_gid,
                        processtype_remarks: $scope.remarks,
                        process_type: processtype
                    }
                            

                            var url = "api/AgrTrnCAD/UpdateRejectProcessType";
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status = true) {
                                    //Notify.alert(resp.data.message, 'success');
                                    if (lspage == 'PendingCADReview') {
                                        $location.url('app/AgrTrnPendingCADReview');
                                    }
                                    else if (lspage == 'CADAcceptanceCustomers') {
                                        $location.url('app/AgrTrnCadAcceptedCustomers');
                                    }
                                    else {

                                    }
                                }
                                else {
                                    Notify.alert(resp.data.message, 'warning');
                                    activate();
                                }
                            });
                            if (lspage == 'PendingCADReview') {
                                $location.url('app/AgrTrnPendingCADReview');
                            }
                            else if (lspage == 'CADAcceptanceCustomers') {
                                $location.url('app/AgrTrnCadAcceptedCustomers');
                            }
                            else {

                            }
                }
                else if ($scope.cboprocesstype == 'Sendback to Credit Without CC') {
                    var application_gid = $scope.application_gid;
                    var processtype;
                    if ($scope.mdlprocesstype_list != null) {
                        processtype = 'Accept'
                    }
                    else {
                        processtype = $scope.cboprocesstype
                    }
                    var params = {
                        application_gid: application_gid,
                        ccmeetingskip_reason: $scope.remarks
                    }
                    var url = 'api/AgrTrnCC/PostCcMeetingSkip';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            if (lspage == 'PendingCADReview') {
                                $location.url('app/AgrTrnPendingCADReview');
                            }
                            else if (lspage == 'CADAcceptanceCustomers') {
                                $location.url('app/AgrTrnCadAcceptedCustomers');
                            }
                            else {

                            }
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });
                }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }      

            }

        }

        $scope.Back = function () {
            if (lspage == 'PendingCADReview') {
                $location.url('app/AgrTrnPendingCADReview');
            }
            else if (lspage == 'CADAcceptanceCustomers') {
                $location.url('app/AgrTrnCadAcceptedCustomers');
            }
            else {

            }
        }

        $scope.cadgroup_change = function () {
            if ($scope.cbocadgroup_name != undefined || $scope.cbocadgroup_name != null) {
                var lscadgroup_gid = '';
                lscadgroup_gid = $scope.cbocadgroup_name.cadgroup_gid;
            }
            var params = {
                cadgroup_gid: lscadgroup_gid
            }
            var url = 'api/AgrTrnCAD/GetCADMembers';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cadmembers_list = resp.data.cadmembers;
                //$scope.checker_list = resp.data.cadmembers;
                //$scope.approver_list = resp.data.cadmanager;
                unlockUI();
            });
        }

        $scope.assign_selectmenu = function () {
            $scope.selectmenu_show = true;
            $scope.selectallmenu_show = false;
        }

        $scope.assign_applyallmenu = function () {
            $scope.selectallmenu_show = true;
            $scope.selectmenu_show = false;
        }

        $scope.delete = function (processtypeassign_gid) {
            var params = {
                processtypeassign_gid: processtypeassign_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/AgrTrnCAD/DeleteCADAssignment';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Process Type!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };


    }
})();
