(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstZoneMappingController', SysMstZoneMappingController);

    SysMstZoneMappingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function SysMstZoneMappingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstZoneMappingController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetZoneSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.zone_list = resp.data.zone_list;
                unlockUI();
            });
        }

        $scope.addzone = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addzone.html',
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

                var url = 'api/SystemMaster/GetUnTaggedRegions';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.region_list = resp.data.region_list;
                    unlockUI();
                });
                $scope.submit = function () {

                    var params = {
                        region_list: $scope.cboregion,
                        zone_name: $scope.txtzone_name

                    }
                    var url = 'api/SystemMaster/PostZoneAdd';
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
        
        $scope.editzone = function (zone_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editzonemapping.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    zone_gid: zone_gid
                }
                var url = 'api/SystemMaster/GetUnTaggedRegionsEdit';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.region_listedit = resp.data.region_list;
                    unlockUI();
                });

                var url = 'api/SystemMaster/GetZoneEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditzone_name = resp.data.zone_name;
                    $scope.region_list = resp.data.region_list;

                    $scope.cboregion = [];
                    if (resp.data.region_list != null) {
                        var count = resp.data.region_list.length;
                        for (var i = 0; i < count; i++) {
                            //var indexs = $scope.region_listedit.findIndex(x => x.region_gid === resp.data.region_list[i].region_gid);
                            var indexs = $scope.region_listedit.map(function (x) { return x.region_gid; }).indexOf(resp.data.region_list[i].region_gid);
                            $scope.cboregion.push($scope.region_listedit[indexs]);
                        }
                    }
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/SystemMaster/PostZoneUpdate';
                    var params = {
                        zone_gid: zone_gid,
                        zone_name: $scope.txteditzone_name,
                        region_list: $scope.cboregion
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

        $scope.Status_update = function (zone_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuszone.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.errormsg = false;
                var params = {
                    zone_gid: zone_gid
                }
                var url = 'api/SystemMaster/GetZoneEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.zone_gid = resp.data.zone_gid
                    $scope.txtzone_name = resp.data.zone_name;
                    $scope.rbo_status = resp.data.zone_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        zone_gid: zone_gid,
                        zone_name: $scope.txtzone_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/PostZoneInactive';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.errormsg = false;
                            $modalInstance.close('closed');
                            activate();

                        }
                        else {
                            if (resp.data.message == "N") {
                                $scope.rbo_status = "Y";
                                $scope.ocs_pendingcount = resp.data.ocs_pendingcount;
                                $scope.agrbyr_pendingcount = resp.data.agrbyr_pendingcount;
                                $scope.agrsupr_pendingcount = resp.data.agrsupr_pendingcount;
                                $scope.errormsg = true;
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                                activate();
                            }
                        }
                    });


                }

                var param = {
                    zone_gid: zone_gid
                }

                var url = 'api/SystemMaster/GetZoneInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.zoneinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }

        $scope.showPopover = function (zone_gid, zone_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showregions.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    zone_gid: zone_gid
                }
                var url = 'api/SystemMaster/GetZone2Region';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.region_list = resp.data.region_list;
                    $scope.zone_name = zone_name;
                    angular.forEach($scope.region_list, function (value, key) {
                        var params = {
                            region_gid: value.region_gid
                        };
                        var url = 'api/SystemMaster/GetRegion2Cluster';
                        SocketService.getparams(url, params).then(function (resp) {
                            value.cluster_list = resp.data.cluster_list;
                        });
                    });
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.exportreport = function () {
            lockUI();
            var url = 'api/SystemMaster/ZonalReportExcel';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lspath, resp.data.lsname); 
                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.lsname.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')

                }

            });
        }

    }
})();

