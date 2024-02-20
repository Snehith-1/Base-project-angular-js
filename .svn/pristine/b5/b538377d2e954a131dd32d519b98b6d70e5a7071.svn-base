(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstRegionMappingController', SysMstRegionMappingController);

    SysMstRegionMappingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function SysMstRegionMappingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstRegionMappingController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetRegionSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.region_list = resp.data.region_list;
                unlockUI();
            });
        }

        $scope.addregion = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addregion.html',
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

                var url = 'api/SystemMaster/GetUnTaggedClusters';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.cluster_list = resp.data.cluster_list;
                    unlockUI();
                });
                $scope.submit = function () {

                    var params = {
                        cluster_list: $scope.cbocluster,
                        region_name: $scope.txtregion_name

                    }
                    var url = 'api/SystemMaster/PostRegionAdd';
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
        $scope.editregion = function (region_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editregionmapping.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    region_gid: region_gid
                }
                var url = 'api/SystemMaster/GetUnTaggedClustersEdit';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cluster_listedit = resp.data.cluster_list;
                    unlockUI();
                });

                var params = {
                    region_gid: region_gid
                }
                var url = 'api/SystemMaster/GetRegionEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditregion_name = resp.data.region_name;
                    $scope.clusterlist = resp.data.cluster_list;
                    console.log(resp.data.cluster_list);
                    $scope.cbocluster = [];
                    if (resp.data.cluster_list != null) {
                        var count = resp.data.cluster_list.length;
                        for (var i = 0; i < count; i++) {
                            //var indexs = $scope.cluster_listedit.findIndex(x => x.cluster_gid === resp.data.cluster_list[i].cluster_gid);
                            var indexs = $scope.cluster_listedit.map(function (x) { return x.cluster_gid; }).indexOf(resp.data.cluster_list[i].cluster_gid);
                            $scope.cbocluster.push($scope.cluster_listedit[indexs]);
                        }
                    }
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/SystemMaster/PostRegionUpdate';
                    var params = {
                        region_gid: region_gid,
                        region_name: $scope.txteditregion_name,
                        cluster_list: $scope.cbocluster
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
              $scope.Status_update = function (region_gid) {
                    var modalInstance = $modal.open({
                        templateUrl: '/statusregion.html',
                        controller: ModalInstanceCtrl,
                        backdrop: 'static',
                        keyboard: false,
                        size: 'md'
                    });
                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {
                        $scope.errormsg = false;
                        var params = {
                            region_gid: region_gid
                        }
                        var url = 'api/SystemMaster/GetRegionEdit';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.region_gid = resp.data.region_gid
                            $scope.txtregion_name = resp.data.region_name;
                            $scope.rbo_status = resp.data.region_status;
                        });

                        $scope.ok = function () {
                            $modalInstance.close('closed');
                        };
                        $scope.update_status = function () {

                            var params = {
                                region_gid: region_gid,
                                region_name: $scope.txtregion_name,
                                remarks: $scope.txtremarks,
                                rbo_status: $scope.rbo_status

                            }
                            var url = 'api/SystemMaster/PostRegionInactive';
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
                            region_gid: region_gid
                        }

                        var url = 'api/SystemMaster/GetRegionInactiveLogview';
                        lockUI();
                        SocketService.getparams(url, param).then(function (resp) {
                            $scope.regioninactivelog_list = resp.data.master_list;
                            unlockUI();
                        });

                    }
                }

                $scope.showPopover = function (region_gid, region_name) {
                    var modalInstance = $modal.open({
                        templateUrl: '/showclusters.html',
                        controller: ModalInstanceCtrl,
                        backdrop: 'static',
                        keyboard: false,
                        size: 'md'
                    });
                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {
                        var params = {
                            region_gid: region_gid
                        }
                        var url = 'api/SystemMaster/GetRegion2Cluster';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.cluster_list = resp.data.cluster_list;
                            $scope.region_name = region_name;
                        });
                        $scope.ok = function () {
                            $modalInstance.close('closed');
                        };
                    }
                }

                $scope.exportreport = function () {
                    lockUI();
                    var url = 'api/SystemMaster/RegionalReportExcel';
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

