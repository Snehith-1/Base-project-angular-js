(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstClusterHeadController', SysMstClusterHeadController);

    SysMstClusterHeadController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstClusterHeadController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstClusterHeadController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {

            var url = 'api/SystemMaster/GetClusterHeadSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.clusterhead_list = resp.data.clusterhead_list;
                unlockUI();
            });
        }



        $scope.addclusterhead = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addclusterhead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.OnchangeVertical = function (cbovertical, cbocluster) {
                    if (cbovertical != "" && cbocluster != "") {
                        var params = {
                            vertical_gid: cbovertical,
                            lstype: 'cluster',
                            lstypegid: cbocluster
                        }
                        var url = 'api/SystemMaster/GetVerticalProgramList';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.program_list = resp.data.program_list;
                            unlockUI();
                        });
                    } 
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/SystemMaster/GetClusterslist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.cluster_list = resp.data.cluster_list;
                    unlockUI();
                });
                var url = 'api/SystemMaster/GetVerticallist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.vertical_list = resp.data.vertical_list;
                    unlockUI();
                });
                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });

                $scope.submit = function () {
                    var VerticalName = "";
                    if ($scope.vertical_list && $scope.vertical_list.length > 0) {
                        //VerticalName = $scope.vertical_list.filter(e => e.vertical_gid == $scope.cbovertical);
                        VerticalName = $scope.vertical_list.filter(function (e) { return e.vertical_gid == $scope.cbovertical });
                        VerticalName = VerticalName[0].vertical_name
                    }
                    var cluster_name = "";
                    if ($scope.cluster_list && $scope.cluster_list.length > 0) {
                        //cluster_name = $scope.cluster_list.filter(e => e.cluster_gid == $scope.cbocluster);
                        cluster_name = $scope.cluster_list.filter(function (e) { return e.cluster_gid == $scope.cbocluster });
                        cluster_name = cluster_name[0].cluster_name
                    }
                    var params = {
                        cluster_gid: $scope.cbocluster,
                        cluster_name: cluster_name,
                        vertical_gid: $scope.cbovertical,
                        vertical_name: VerticalName,
                        employee_gid: $scope.cboemployee.employee_gid,
                        employee_name: $scope.cboemployee.employee_name,
                        program_gid: $scope.cboprogram.program_gid,
                        program_name: $scope.cboprogram.program_name,
                    }

                    var url = 'api/SystemMaster/PostClusterHeadAdd';
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
        $scope.editcluster = function (clusterhead_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editclusterhead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    clusterhead_gid: clusterhead_gid
                }
                lockUI();
                var url = 'api/SystemMaster/GetClusterHeadEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cboclusteredit = resp.data.cluster_gid,
                    $scope.cboverticaledit = resp.data.vertical_gid,
                    $scope.cboemployeeedit = resp.data.employee_gid,
                     $scope.cboprogramedit = resp.data.program_gid,
                    $scope.cluster_list = resp.data.cluster_list,
                    $scope.vertical_list = resp.data.vertical_list,
                    $scope.employee_list = resp.data.employeelist
                    if (resp.data.vertical_gid != "") {
                        var params = {
                            vertical_gid: resp.data.vertical_gid,
                            lstype: 'cluster',
                            lstypegid: resp.data.cluster_gid,
                            lsmaster_gid: clusterhead_gid
                        }
                        var url = 'api/SystemMaster/GetEditVerticalProgramList';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.program_list = resp.data.program_list; 
                        });
                    }
                    unlockUI();
                });

                $scope.OnchangeVertical = function (cbovertical, cboclusteredit) {
                    if (cbovertical != "" && cboclusteredit != "") {
                        var params = {
                            vertical_gid: cbovertical,
                            lstype: 'cluster',
                            lstypegid: cboclusteredit,
                            lsmaster_gid: clusterhead_gid
                        }
                        var url = 'api/SystemMaster/GetEditVerticalProgramList';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.program_list = resp.data.program_list;
                            unlockUI();
                        });
                    } 
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    var clustername;
                    var cluster_index = $scope.cluster_list.map(function (e) { return e.cluster_gid }).indexOf($scope.cboclusteredit);
                    if (cluster_index == -1) { clustername = ''; } else { clustername = $scope.cluster_list[cluster_index].cluster_name; };

                    var verticalname;
                    var vertical_index = $scope.vertical_list.map(function (e) { return e.vertical_gid }).indexOf($scope.cboverticaledit);
                    if (vertical_index == -1) { verticalname = ''; } else { verticalname = $scope.vertical_list[vertical_index].vertical_name; };

                    var employeename;
                    var employee_index = $scope.employee_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboemployeeedit);
                    if (employee_index == -1) { employeename = ''; } else { employeename = $scope.employee_list[employee_index].employee_name; };

                    var programName = "";
                    if ($scope.program_list && $scope.program_list.length > 0) {
                        //programName = $scope.program_list.filter(e => e.program_gid == $scope.cboprogramedit);
                        programName = $scope.program_list.filter(function (e) { return e.program_gid == $scope.cboprogramedit });
                        programName = programName[0].program_name
                    }
                    var url = 'api/SystemMaster/PostClusterHeadUpdate';
                    var params = {
                        cluster_gid: $scope.cboclusteredit,
                        cluster_name: clustername,
                        vertical_gid: $scope.cboverticaledit,
                        vertical_name: verticalname,
                        employee_gid: $scope.cboemployeeedit,
                        employee_name: employeename,
                        clusterhead_gid: clusterhead_gid,
                        program_gid: $scope.cboprogramedit,
                        program_name: programName,
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
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

        $scope.Status_update = function (clusterhead_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusclusterhead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    clusterhead_gid: clusterhead_gid
                }
                var url = 'api/SystemMaster/GetClusterHeadEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtclusterhead_name = resp.data.employee_name,
                    $scope.txtvertical_name = resp.data.vertical_name,
                    $scope.txtcluster_name = resp.data.cluster_name,
                    $scope.rbo_status = resp.data.clusterhead_status

                });



                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        clusterhead_gid: clusterhead_gid,
                        employee_name: $scope.txtclusterhead_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/InactiveClusterhead';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }

                var param = {
                    clusterhead_gid: clusterhead_gid
                }

                var url = 'api/SystemMaster/ClusterheadInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.clusterheadinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }



    }
})();
