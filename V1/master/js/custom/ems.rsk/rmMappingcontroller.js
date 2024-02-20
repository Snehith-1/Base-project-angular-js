(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmMappingcontroller', rmMappingcontroller);

    rmMappingcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function rmMappingcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmMappingcontroller';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/rmMapping/getmappingsummary";
            SocketService.get(url).then(function (resp) {
                $scope.mappingdtlList = resp.data.mappingdtl;
                if ($scope.mappingdtlList == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.mappingdtlList.length;
                    if ($scope.mappingdtlList.length < 100) {
                        $scope.totalDisplayed = $scope.mappingdtlList.length;
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

        $scope.addpincodeMapping = function (val) {
            var modalInstance = $modal.open({
                templateUrl: '/addMaping.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var url = "api/rmMapping/getstatedtls";
                SocketService.get(url).then(function (resp) {
                    $scope.statedtl = resp.data.statedtl;
                });

                var url = 'api/newServiceTicket/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });

                unlockUI();
                $scope.onselectedchange = function (state_gid) {
                    var params = {
                        state_gid: state_gid
                    }
                    var url = "api/rmMapping/getdistrictdtls";
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.districtdtl = resp.data.statedtl;
                    });
                }


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.submitmapping = function () {
                    var state_name = $('#cbostatename :selected').text();
                    var district_name = $('#cbodistrictname :selected').text();
                    var params = {
                        state_gid: $scope.cbostate_gid,
                        district_gid: $scope.cbodistrict_gid,
                        state_name: state_name,
                        district_name: district_name,
                        assigned_RM: $scope.cboemployeegid
                    }
                    lockUI();
                    var url = "api/rmMapping/postmappingdetails";
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


        $scope.editmappingdtl = function (val) {
            var params = {
                RMmapping_gid: val
            }

            var modalInstance = $modal.open({
                templateUrl: '/EditMaping.html',
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
              
                var url = "api/rmMapping/getmappingdtl";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.district_name = resp.data.district_name;
                    $scope.state_name = resp.data.state_name;
                    $scope.employee_id = resp.data.assigned_RM;
                    $scope.ZonalRMname = resp.data.ZonalRMname;
                });

                unlockUI();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.updatemapping = function () {
                    lockUI();
                    var updatedtl = {
                        RMmapping_gid: val,
                        assigned_RM: $scope.employee_id,
                        //ZonalRM_gid: $scope.zonal_employeegid
                    }
                    var url = "api/rmMapping/updatemappingdetails";
                    SocketService.post(url, updatedtl).then(function (resp) {
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


        $scope.deletemappingdtl = function (val) {
            var params = {
                RMmapping_gid: val
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
                    var url = "api/rmMapping/deletemappingdtl";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });

                }

            });
        }
    }
})();
