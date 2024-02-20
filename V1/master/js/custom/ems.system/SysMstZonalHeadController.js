(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstZonalHeadController', SysMstZonalHeadController);

    SysMstZonalHeadController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstZonalHeadController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstZonalHeadController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetZonalHeadSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.zonalhead_list = resp.data.zonalhead_list;
                unlockUI();
            });
        }

        $scope.addzonalhead = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addzonalhead.html',
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
                            lstype: 'zonal',
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

                var url = 'api/SystemMaster/GetZonallist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.zone_list = resp.data.zone_list;
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
                    var zone_name = "";
                    if ($scope.zone_list && $scope.zone_list.length > 0) {
                        //zone_name = $scope.zone_list.filter(e => e.zone_gid == $scope.cbozone);
                        zone_name = $scope.zone_list.filter(function (e) { return e.zone_gid == $scope.cbozone });
                        zone_name = zone_name[0].zone_name
                    }
                    var params = {
                        zonal_gid: $scope.cbozone,
                        zonal_name: zone_name,
                        vertical_gid: $scope.cbovertical,
                        vertical_name: VerticalName,
                        employee_gid: $scope.cboemployee.employee_gid,
                        employee_name: $scope.cboemployee.employee_name,
                        program_gid: $scope.cboprogram.program_gid,
                        program_name: $scope.cboprogram.program_name,
                    }
                  
                    var url = 'api/SystemMaster/PostZonalheadAdd';
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
        $scope.editzonal = function (zonalhead_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editzonalhead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    zonalhead_gid: zonalhead_gid
                }
                var url = 'api/SystemMaster/GetZonalHeadEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cbozoneedit = resp.data.zonal_gid,
                    $scope.cboverticaledit = resp.data.vertical_gid,
                     $scope.cboemployeeedit = resp.data.employee_gid,
                      $scope.zone_list = resp.data.zone_list,
                     $scope.cboprogramedit = resp.data.program_gid,
                    $scope.vertical_list = resp.data.vertical_list,
                    $scope.employee_list = resp.data.employeelist
                    if (resp.data.vertical_gid != "") {
                        var params = {
                            vertical_gid: resp.data.vertical_gid,
                            lstype: 'zonal',
                            lstypegid: resp.data.zonal_gid,
                            lsmaster_gid: zonalhead_gid
                        }
                        var url = 'api/SystemMaster/GetEditVerticalProgramList';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.program_list = resp.data.program_list;
                        });
                    }
                });

                $scope.OnchangeVertical = function (cbovertical, cboclusteredit) {
                    if (cbovertical != "" && cboclusteredit != "") {
                        var params = {
                            vertical_gid: cbovertical,
                            lstype: 'zonal',
                            lstypegid: cboclusteredit,
                            lsmaster_gid: zonalhead_gid
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
                    var zonename;
                    var zone_index = $scope.zone_list.map(function (e) { return e.zone_gid }).indexOf($scope.cbozoneedit);
                    if (zone_index == -1) { zonename = ''; } else { zonename = $scope.zone_list[zone_index].zone_name; };

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

                    var url = 'api/SystemMaster/PostZonalHeadUpdate';
                    var params = {
                        zonal_gid: $scope.cbozoneedit,
                        zonal_name: zonename,
                        vertical_gid: $scope.cboverticaledit,
                        vertical_name: verticalname,
                        employee_gid: $scope.cboemployeeedit,
                        employee_name: employeename,
                        zonalhead_gid: zonalhead_gid,
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

        $scope.Status_update = function (zonalhead_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuszonalhead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    zonalhead_gid: zonalhead_gid
                }
                var url = 'api/SystemMaster/GetZonalHeadEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtzonalhead_name = resp.data.employee_name,
                    $scope.rbo_status = resp.data.zonalhead_status
                    
                });

            

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        zonalhead_gid: zonalhead_gid,
                        employee_name: $scope.txtclusterhead_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/InactiveZonalhead';
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
                    zonalhead_gid: zonalhead_gid
                }

                var url = 'api/SystemMaster/ZonalheadInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.zonalheadinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }
    }
})();