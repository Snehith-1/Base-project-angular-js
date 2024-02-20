(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRMMappingController', MstRMMappingController);

    MstRMMappingController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstRMMappingController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRMMappingController';

        activate();
        function activate() {
            $scope.locationbasedexport = true;
            var url = 'api/SystemMaster/GetBaseLocationlist';
            SocketService.get(url).then(function (resp) {
                $scope.location_list = resp.data.location_list;
            });

            var url = 'api/MstRMMapping/GetEmployeelist';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employeelists;

            });
        }


        $scope.OnchangeVertical = function (cbovertical) {
            if (cbovertical != "") {
                var params = {
                    vertical_gid: cbovertical,
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
        }

        $scope.onselectdlocation = function (cbobaselocation) {
            var param = {
                baselocation_gid: cbobaselocation.baselocation_gid
            };


            var url = 'api/SystemMaster/GetVerticallist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
                unlockUI();
            });

            var url = 'api/MstRMMapping/GetLocationEmployeelist';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.employee_list = resp.data.employeelists;

            });
        }

        $scope.export = function () {
            lockUI();
            if ($scope.cboemployee == null || $scope.cboemployee == undefined || $scope.cboemployee == "") {
                var employee_gid = "";
            }
            else {
                var employee_gid = $scope.cboemployee.employee_gid;
            }
            if ($scope.cbobaselocation == null || $scope.cbobaselocation == undefined || $scope.cbobaselocation == "") {
                var baselocation_gid = "";
            }
            else {
                var baselocation_gid = $scope.cbobaselocation.baselocation_gid;
            }
            if ($scope.cboprogram == null || $scope.cboprogram == undefined || $scope.cboprogram == "") {
                var program_gid = "";
            }
            else {
                var program_gid = $scope.cboprogram.program_gid;
            }
            if ($scope.cbovertical == null || $scope.cbovertical == undefined || $scope.cbovertical == "") {
                var vertical_gid = "";
            }
            else {
                var vertical_gid = $scope.cbovertical.vertical_gid;
            }
            var params = {
                baselocation_gid: baselocation_gid,
                vertical_gid: vertical_gid,
                program_gid: program_gid,
                employeegid: employee_gid,
            }
            var url = 'api/MstRMMapping/RMMappingExport';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    //var phyPath = resp.data.lspath;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = resp.data.lsname.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);

                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')
                    activate();

                }

            });
        }
        $scope.refresh = function () {
            activate();
            $scope.locationbased = false;
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

        $scope.searchsubmit = function () {
            if ($scope.cbobaselocation == null || $scope.cbobaselocation == undefined || $scope.cbobaselocation == "") {
                var params = {
                    employee_gid: $scope.cboemployee.employee_gid,
                }
                var url = 'api/MstRMMapping/DaPostAllHierarchyverticalListSearch';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    $scope.vertical = resp.data.verticallist;
                    $scope.level_one = resp.data.level_one;
                    $scope.level_zero = resp.data.level_zero;

                    $scope.locationbased = false;
                    $scope.locationbasedexport = false;
                    $scope.verticalshow = true;
                    unlockUI();

                });
            }
            else if ($scope.cbovertical != null || $scope.cbovertical != undefined || $scope.cbovertical != "" || $scope.cboprogram != null || $scope.cboprogram != undefined || $scope.cboprogram != "") {
                if ($scope.cboemployee == null || $scope.cboemployee == undefined || $scope.cboemployee == "") {
                    var employee_gid = "";
                }
                else {
                    var employee_gid = $scope.cboemployee.employee_gid;
                }
                var params = {
                    baselocation_gid: $scope.cbobaselocation.baselocation_gid,
                    vertical_gid: $scope.cbovertical,
                    program_gid: $scope.cboprogram.program_gid,
                    employeegid: employee_gid,
                }
                var url = 'api/MstRMMapping/PostAllHierarchyListSearch';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    $scope.employee_data = resp.data.MdlRMMappingviewlist;
                    $scope.employee_count = resp.data.employee_count;
                    $scope.locationbased = true;
                    $scope.locationbasedexport = true;
                    $scope.verticalshow = false;
                    if ($scope.employee_data == null) {
                        $scope.locationemployee_count = "0";
                        Notify.alert('No record Found..!', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        $scope.locationemployee_count = $scope.employee_data.length;
                    }

                    unlockUI();

                });
            }
            else {
                if ($scope.cboemployee == null || $scope.cboemployee == undefined || $scope.cboemployee == "") {
                    var employee_gid = "";
                }
                else {
                    var employee_gid = $scope.cboemployee.employee_gid;
                }
                var params = {
                    baselocation_gid: $scope.cbobaselocation.baselocation_gid,
                    vertical_gid: $scope.cbovertical,
                    employeegid: employee_gid,
                }
                var url = 'api/MstRMMapping/PostAllHierarchyListSearch';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    $scope.employee_data = resp.data.MdlRMMappingviewlist;
                    $scope.employee_count = resp.data.employee_count;
                    $scope.locationbased = true;
                    $scope.locationbasedexport = true;
                    $scope.verticalshow = false;
                    if ($scope.employee_data == null) {
                        $scope.locationemployee_count = "0";
                        Notify.alert('No record Found..!', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        $scope.locationemployee_count = $scope.employee_data.length;
                    }

                    unlockUI();

                });
            }
        }
    }
})();