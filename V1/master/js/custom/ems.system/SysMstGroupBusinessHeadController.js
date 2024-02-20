(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstGroupBusinessHeadController', SysMstGroupBusinessHeadController);

    SysMstGroupBusinessHeadController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstGroupBusinessHeadController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstGroupBusinessHeadController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetGroupBusinessHeadSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.groupbusinesshead_list = resp.data.businesshead_list;
                unlockUI();
            });
        }

        $scope.addGroupBusinessHead = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addgroupbusinesshead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/SystemMaster/GetZoneList';
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

                    var params = {
                        zone_gid: $scope.cbozone.zone_gid,
                        zone_name: $scope.cbozone.zone_name,
                        vertical_gid: $scope.cbovertical.vertical_gid,
                        vertical_name: $scope.cbovertical.vertical_name,
                        employee_gid: $scope.cboemployee.employee_gid,
                        employee_name: $scope.cboemployee.employee_name
                    }

                    var url = 'api/SystemMaster/PostGroupBusinessHeadAdd';
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

        $scope.editgroupbusinesshead = function (groupbusinesshead_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editgroupbusinesshead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    groupbusinesshead_gid: groupbusinesshead_gid
                }
                var url = 'api/SystemMaster/GetGroupBusinessHeadEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cbozoneedit = resp.data.zone_gid,
                    $scope.cboverticaledit = resp.data.vertical_gid,
                    $scope.cboemployeeedit = resp.data.employee_gid,
                    $scope.zone_list = resp.data.zone_list,
                    $scope.vertical_list = resp.data.vertical_list,
                    $scope.employee_list = resp.data.employeelist
                });


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

                    var url = 'api/SystemMaster/PostGroupBusinessHeadUpdate';
                    var params = {
                        zone_gid: $scope.cbozoneedit,
                        zone_name: zonename,
                        vertical_gid: $scope.cboverticaledit,
                        vertical_name: verticalname,
                        employee_gid: $scope.cboemployeeedit,
                        employee_name: employeename,
                        groupbusinesshead_gid: groupbusinesshead_gid
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

        $scope.Status_update = function (groupbusinesshead_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/groupbusinessheadstatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    groupbusinesshead_gid: groupbusinesshead_gid
                }
                var url = 'api/SystemMaster/GetGroupBusinessHeadEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.groupbusinesshead_gid = resp.data.groupbusinesshead_gid
                    $scope.txtemployee_name = resp.data.employee_name;
                    $scope.txtemployee_gid = resp.data.employee_gid;
                    $scope.rbo_status = resp.data.businesshead_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        groupbusinesshead_gid: groupbusinesshead_gid,
                        employee_name: $scope.txtemployee_name,
                        employee_gid: $scope.txtemployee_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/PostGroupBusinessHeadInactive';
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
                    groupbusinesshead_gid: groupbusinesshead_gid
                }

                var url = 'api/SystemMaster/GetGroupBusinessHeadInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.groupbusinessheadinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }


    }
})();

