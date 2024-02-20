(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstClusterMappingController', SysMstClusterMappingController);

    SysMstClusterMappingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function SysMstClusterMappingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstClusterMappingController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetClusterSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.cluster_list = resp.data.cluster_list;
                unlockUI();
            });
        }

        $scope.addcluster = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcluster.html',
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

                var url = 'api/SystemMaster/GetUnTaggedLocations';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.location_list = resp.data.master_list;
                    unlockUI();
                });

                $scope.submit = function () {

                    var params = {
                        locationlist: $scope.cbolocation,
                        cluster_name: $scope.txtcluster_name
                    }

                    var url = 'api/SystemMaster/PostClusterAdd';
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
        $scope.editcluster = function (cluster_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editclustermapping.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
           
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    cluster_gid: cluster_gid
                }
                var url = 'api/SystemMaster/GetUnTaggedLocationsEdit';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.location_listedit = resp.data.master_list;
                    unlockUI();
                });

                var params = {
                    cluster_gid: cluster_gid
                }
                var url = 'api/SystemMaster/GetClusterEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditcluster_name = resp.data.cluster_name;

                    $scope.locationlist = resp.data.locationlist;

                    $scope.cbolocation = [];
                    if (resp.data.locationlist != null) {
                        var count = resp.data.locationlist.length;
                        for (var i = 0; i < count; i++) {
                            //var indexs = $scope.location_listedit.findIndex(x => x.baselocation_gid === resp.data.locationlist[i].baselocation_gid);
                            var indexs = $scope.location_listedit.map(function (x) { return x.baselocation_gid; }).indexOf(resp.data.locationlist[i].baselocation_gid);
                            $scope.cbolocation.push($scope.location_listedit[indexs]);
                        }
                    }
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    lockUI();
                    var url = 'api/SystemMaster/PostClusterUpdate';
                    var params = {
                        cluster_gid: cluster_gid,
                        cluster_name: $scope.txteditcluster_name,
                        locationlist: $scope.cbolocation
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            unlockUI();
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

        $scope.Status_update = function (cluster_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscluster.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.errormsg = false;
                var params = {
                    cluster_gid: cluster_gid
                }
                var url = 'api/SystemMaster/GetClusterEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cluster_gid = resp.data.cluster_gid
                    $scope.txtcluster_name = resp.data.cluster_name;
                    $scope.rbo_status = resp.data.cluster_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        cluster_gid: cluster_gid,
                        cluster_name: $scope.txtcluster_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/InactiveCluster';
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
                                $scope.rbo_status ="Y";
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
                    cluster_gid: cluster_gid
                }

                var url = 'api/SystemMaster/ClusterInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.clusterinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }

        $scope.showPopover = function (cluster_gid, cluster_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showlocation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    cluster_gid: cluster_gid
                }
                var url = 'api/SystemMaster/GetCluster2BaseLocation';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.locationlist = resp.data.locationlist;
                    $scope.cluster_name = cluster_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.exportreport = function () {
            lockUI();
            var url = 'api/SystemMaster/ClusterReportExcel';
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

