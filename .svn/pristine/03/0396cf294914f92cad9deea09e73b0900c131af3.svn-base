(function () {
    'use strict';

    angular
        .module('angle')
        .controller('zonalMappingcontroller', zonalMappingcontroller);

    zonalMappingcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function zonalMappingcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'zonalMappingcontroller';
        $scope.statedtlList = [];
        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/zonalMapping/getzonalMappingdtl";
            SocketService.get(url).then(function (resp) {
                $scope.zonalMappingList = resp.data.zonalMapping;
                //$scope.htmlContent = "<table></table>";
                if ($scope.zonalMappingList == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.zonalMappingList.length;
                    if ($scope.zonalMappingList.length < 100) {
                        $scope.totalDisplayed = $scope.zonalMappingList.length;
                    }
                }
                unlockUI();
            });
           
        }

        document.getElementById('pagecount').onkeyup = function () {

            if ($scope.pagecount == null) {
                var el = document.getElementById('loadmore');
                el.style.backgroundColor = '#DCDCDC';
            }
            else {
                var el = document.getElementById('loadmore');
                el.style.backgroundColor = '#ffa';
            }
        };

        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);
            $scope.totalDisplayed += Number;
            unlockUI();
        };

        $scope.addzonalMapping = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addZonalMapping.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();

                var url = 'api/newServiceTicket/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });

                unlockUI();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.submitzonalmapping = function () {
                    lockUI();
                    var zonalrisk_managername = $('#zonalriskmanager :selected').text();
                    var params = {
                        zonal_name: $scope.txtzonalname,
                        zonalrisk_managerGid: $scope.cboemployeegid,
                        zonalrisk_managername: zonalrisk_managername
                    }
                    var url = "api/zonalMapping/postzonalMapping";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }
            }
        }


        $scope.tagstatedtl = function (zonalmapping_gid) {
            var params = {
                zonalmapping_gid: zonalmapping_gid
            }
            var modalInstance = $modal.open({
                templateUrl: '/tagZonalstate.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance','ngTableParams'];
            function ModalInstanceCtrl($scope, $modalInstance,ngTableParams) {
                lockUI();

                var url = "api/rmMapping/getstatedtls";
                SocketService.get(url).then(function (resp) {
                    $scope.statedtllist = resp.data.statedtl;
                });

                var url = 'api/zonalMapping/getviewzonalmappingdtl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.zonal_name = resp.data.zonal_name;
                    $scope.zonalrisk_managerGid = resp.data.zonalrisk_managerGid;
                    $scope.zonalrisk_managername = resp.data.zonalrisk_managername;
                    $scope.statelist = resp.data.tagzonalmapping;
                    var count = $scope.statelist.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.statedtllist.map(function (x) { return x.state_gid; }).indexOf($scope.statelist[i].state_gid); 
                        $scope.statedtllist[indexs].checked = true;
                    }
                });

                unlockUI();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

               
                $scope.tagzonalmapping = function () {
                    lockUI();
                    var stateGidList = [];
                    angular.forEach($scope.statedtllist, function (val) {
                       
                        if (val.checked == true) {
                            var stateGid = val.state_gid;
                            stateGidList.push(stateGid);
                        }
                       
                    });
                    if (stateGidList.length==0) {
                        alert('Choose Atleast One State');
                        unlockUI();
                        return
                    }
                    else
                    {
                        var params = {
                            zonalmapping_gid: zonalmapping_gid,
                            zonalrisk_managerGid: $scope.zonalrisk_managerGid,
                            state_gid: stateGidList
                        }

                        var url = "api/zonalMapping/poststatetag2zonal";
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                                $modalInstance.close('closed');
                                activate();
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                            }
                        });
                    }
                  
                }
            }
        }


        $scope.viewtagstatedtl = function (zonalmapping_gid) {
            var params = {
                zonalmapping_gid: zonalmapping_gid
            }
            var modalInstance = $modal.open({
                templateUrl: '/viewtaggedZonalstate.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance', 'ngTableParams'];
            function ModalInstanceCtrl($scope, $modalInstance, ngTableParams) {
                lockUI();
                var url = 'api/zonalMapping/getviewzonalmappingdtl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.zonal_name = resp.data.zonal_name;
                    $scope.zonalrisk_managerGid = resp.data.zonalrisk_managerGid;
                    $scope.zonalrisk_managername = resp.data.zonalrisk_managername;
                    $scope.statedtlList = resp.data.tagzonalmapping;
                });

                unlockUI();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.editstatedtl = function (zonalmapping_gid) {
            var params = {
                zonalmapping_gid: zonalmapping_gid
            }
            var modalInstance = $modal.open({
                templateUrl: '/edittaggedZonalstate.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();

                var url = 'api/zonalMapping/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });

                var url = 'api/zonalMapping/getviewzonalmappingdtl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.zonal_name = resp.data.zonal_name;
                    $scope.zonalrisk_managerGid = resp.data.zonalrisk_managerGid;
                    $scope.zonalrisk_managername = resp.data.zonalrisk_managername;
                });

                unlockUI();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.updatezonalmapping = function (zonalrisk_managerGid) {
                    var zonalrisk_managername = $('#zonalrisk_managername :selected').text(); 
                    var params = {
                        zonalmapping_gid: zonalmapping_gid,
                        zonalrisk_managername: zonalrisk_managername,
                        zonalrisk_managerGid: $scope.zonalrisk_managerGid
                    }
                    var url = "api/zonalMapping/updatezonalMapping";

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }
            }
        }

    }
})();
