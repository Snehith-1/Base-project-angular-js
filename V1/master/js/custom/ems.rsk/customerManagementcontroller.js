(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customerSummaryManagementcontroller', customerSummaryManagementcontroller);

    customerSummaryManagementcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout'];

    function customerSummaryManagementcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'customerSummaryManagementcontroller';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/customerManagement/GetCustomerRMDetail';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.customer_data = resp.data.customer_list;
                if ($scope.customer_data == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.customer_data.length;
                    if ($scope.customer_data.length < 100) {
                        $scope.totalDisplayed = $scope.customer_data.length;
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

        $scope.assignRM = function (customer_gid) {
            var params = {
                customer_gid: customer_gid
            }
            
            var modalInstance = $modal.open({
                templateUrl: '/assign_RMModal.html',
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

                var url = "api/customerManagement/getAssignRMdetail";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.districtdtl = resp.data.districtdtl;
                    $scope.state_name = resp.data.state_name;
                    $scope.cboassignedRM_gid = resp.data.assigned_RM;
                    $scope.ZonalRM_gid = resp.data.zonal_riskmanager;
                    $scope.cbodistrict_gid = resp.data.district_gid;
                    $scope.zonal_name = resp.data.zonal_name;
                    $scope.zonal_gid = resp.data.zonal_gid;
                    $scope.customer_name = resp.data.customer_name;
                    $scope.addressline1 = resp.data.addressline1;
                    $scope.addressline2 = resp.data.addressline2;
                    $scope.cboPPA_gid = resp.data.ppa_gid;
                });

                unlockUI();

                $scope.onchangedistrict = function (cbodistrictgid) {
                    console.log(cbodistrictgid);
                    lockUI();
                    var params = {
                        district_gid: cbodistrictgid
                    }
                    var url = "api/allocationManagement/getallocateRM";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.assigned_RM == "") {
                            $scope.cboassignedRM_gid = "";
                            $scope.ZonalRM_gid = "";
                            alert('RM is Not Mapping for the Selected District');
                        }
                        else {
                            $scope.cboassignedRM_gid = resp.data.assignedRM_gid;
                            $scope.ZonalRM_gid = resp.data.ZonalRM_gid;
                        }
                    });
                    unlockUI();
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.submitmapping = function () {
                    lockUI();
                    var PPA_name = $('#ppa_name :selected').text();
                    var district_name = $('#cbodistrictname :selected').text();
                    var updatedtl = {
                        customer_gid: customer_gid,
                        district_gid: $scope.cbodistrict_gid,
                        district_name : district_name,
                        assignedRM_gid: $scope.cboassignedRM_gid,
                        ZonalRM_gid: $scope.ZonalRM_gid,
                        zonal_gid: $scope.zonal_gid,
                        PPA_gid: $scope.cboPPA_gid,
                        PPA_name: PPA_name
                    }
                   
                    var url = "api/rmMapping/postcustomerMappingdetails";
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

        $scope.EscrowCreate = function (customer_gid) {
            $scope.customer_gid = localStorage.setItem('customer_gid', customer_gid);
            $state.go('app.Customer2EscrowSummary')
        }

        $scope.customer360click = function (customer_gid) {
            $scope.customer_gid = localStorage.setItem('customer_gid', customer_gid);
            localStorage.setItem('MyCustomer', 'N');
            $state.go('app.customerManagement360');
        }

      
    }

})();
