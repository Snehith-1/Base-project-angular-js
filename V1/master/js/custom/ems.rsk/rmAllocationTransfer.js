(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmAllocationTransfer', rmAllocationTransfer);

    rmAllocationTransfer.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout'];

    function rmAllocationTransfer($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmAllocationTransfer';

        activate();

        function activate() {
            lockUI();
            var url = 'api/newServiceTicket/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var url = "api/rmMapping/getstatedtls";
            SocketService.get(url).then(function (resp) {
                $scope.statedtl = resp.data.statedtl;

            });

            var params = {
                assignedRM_gid: localStorage.getItem('assignedRM_gid')
            }
            var url = "api/allocationManagement/getassignedAllocation";

            SocketService.getparams(url, params).then(function (resp) {
                $scope.RMallocation = resp.data.mappingdtl;
            });
            unlockUI();
        }

        $scope.onchangestate = function (cbostatename) {
            lockUI();
            var params = {
                state_gid: cbostatename.state_gid
            }
            var url = "api/rmMapping/getdistrictdtls";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.districtdtl = resp.data.statedtl;
            });
            unlockUI();
        }

        $scope.onchangedistrict = function (cbodistrictgid) {
            lockUI();
            var params = {
                district_gid: cbodistrictgid.district_gid
            }
            console.log(params);
            var url = "api/allocationManagement/getallocateRM";
            SocketService.getparams(url, params).then(function (resp) {
                console.log(resp.data.assigned_RM);
                if (resp.data.assigned_RM == "") {
                    alert('RM is Not Mapping for the Selected District');
                }
                else {
                    $scope.allocateRMname = resp.data.assigned_RM;
                    $scope.assignedRM_gid = resp.data.assignedRM_gid;
                }
            });
            unlockUI();
        }


        $scope.checkall = function (selected) {
            //console.log(selected);
            angular.forEach($scope.RMallocation, function (val) {
                val.checked = selected;
            });
        }

        $scope.AllocationTransfer = function () {
            var allocationGidList = [];

            angular.forEach($scope.RMallocation, function (val) {

                if (val.checked == true) {
                    var allocationdtl_gid = val.allocationdtl_gid;
                    allocationGidList.push(allocationdtl_gid);
                }
            });

            if (Array.isArray(allocationGidList) && allocationGidList.length == 0)
            {
                alert('Select Atleast one Customer');
            }
            else {
                var params = {
                    allocationdtl_gid: allocationGidList,
                    transferred_to: $scope.cboemployeename.employee_id
                }

                var url = 'api/allocationManagement/postAllocationTransfer';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        $state.go('app.rmTransfer');
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
})();
