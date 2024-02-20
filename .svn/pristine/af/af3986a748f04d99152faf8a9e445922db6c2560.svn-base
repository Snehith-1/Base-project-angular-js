(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstRegionHeadController', SysMstRegionHeadController);

    SysMstRegionHeadController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstRegionHeadController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstRegionHeadController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetRegionHeadSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.regionhead_list = resp.data.regionhead_list;
                unlockUI();
            });
        }

        $scope.addRegionHead = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addregionhead.html',
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
                            lstype: 'region',
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

                var url = 'api/SystemMaster/GetRegionList';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.region_list = resp.data.region_list;
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
                    var region_name = "";
                    if ($scope.region_list && $scope.region_list.length > 0) {
                        //region_name = $scope.region_list.filter(e => e.region_gid == $scope.cboregion);
                        region_name = $scope.region_list.filter(function (e) { return e.region_gid == $scope.cboregion });
                        region_name = region_name[0].region_name
                    }
                    var params = {
                        region_gid: $scope.cboregion,
                        region_name: region_name,
                        vertical_gid: $scope.cbovertical,
                        vertical_name: VerticalName,
                        employee_gid: $scope.cboemployee.employee_gid,
                        employee_name: $scope.cboemployee.employee_name,
                        program_gid: $scope.cboprogram.program_gid,
                        program_name: $scope.cboprogram.program_name,
                    }

                    var url = 'api/SystemMaster/PostRegionHeadAdd';
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

        $scope.editregionhead = function (regionhead_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editregionhead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    regionhead_gid: regionhead_gid
                }
                var url = 'api/SystemMaster/GetRegionHeadEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cboregionedit = resp.data.region_gid,
                    $scope.cboverticaledit = resp.data.vertical_gid,
                    $scope.cboemployeeedit = resp.data.employee_gid,
                    $scope.cboprogramedit = resp.data.program_gid,
                    $scope.region_list = resp.data.region_list,
                    $scope.vertical_list = resp.data.vertical_list,
                    $scope.employee_list = resp.data.employeelist
                    if (resp.data.vertical_gid != "") {
                      var params = {
                            vertical_gid: resp.data.vertical_gid,
                            lstype: 'region',
                            lstypegid: resp.data.region_gid,
                            lsmaster_gid: regionhead_gid
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
                            lstype: 'region',
                            lstypegid: cboclusteredit,
                            lsmaster_gid: regionhead_gid
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
                    var regionname;
                    var region_index = $scope.region_list.map(function (e) { return e.region_gid }).indexOf($scope.cboregionedit);
                    if (region_index == -1) { regionname = ''; } else { regionname = $scope.region_list[region_index].region_name; };

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

                    var url = 'api/SystemMaster/PostRegionHeadUpdate';
                    var params = {
                        region_gid: $scope.cboregionedit,
                        region_name: regionname,
                        vertical_gid: $scope.cboverticaledit,
                        vertical_name: verticalname,
                        employee_gid: $scope.cboemployeeedit,
                        employee_name: employeename,
                        regionhead_gid: regionhead_gid,
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

        $scope.Status_update = function (regionhead_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/regionheadstatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    regionhead_gid: regionhead_gid
                }
                var url = 'api/SystemMaster/GetRegionHeadEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.regionhead_gid = resp.data.regionhead_gid
                    $scope.txtemployee_name = resp.data.employee_name;
                    $scope.txtemployee_gid = resp.data.employee_gid;
                    $scope.rbo_status = resp.data.regionhead_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        regionhead_gid: regionhead_gid,
                        employee_name: $scope.txtemployee_name,
                        employee_gid: $scope.txtemployee_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/PostRegionHeadInactive';
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
                    regionhead_gid: regionhead_gid
                }

                var url = 'api/SystemMaster/GetRegionHeadInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.regionheadinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }


    }
})();

