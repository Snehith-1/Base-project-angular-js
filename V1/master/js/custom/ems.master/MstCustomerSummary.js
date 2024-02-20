(function () {
    'use strict';

    angular
        .module('angle')
        .controller('mstcustomersummarycontroller', mstcustomersummarycontroller);

    mstcustomersummarycontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function mstcustomersummarycontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'mstcustomersummarycontroller';
        debugger
        var lsdashpagename = $location.search().lsdashpagename;
        $scope.lsdashpagename = lsdashpagename;

        activate();

        function activate() {
            $scope.totalDisplayedpending = 100;
            $scope.totalDisplayedcompleted = 100;
            $scope.tab = {};
            var url = 'api/customer/customerdetail';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.customer_data = resp.data.customer_list;

                //if ($scope.customer_data == null) {
                //    $scope.totalDisplayedcompleted = 0;
                //    $scope.completedCount = 0;
                //}
                //else {
                //    $scope.completedCount = $scope.customer_data.length;
                //    if ($scope.customer_data.length < 100) {
                //        $scope.totalDisplayedcompleted = $scope.customer_data.length;
                //    }
                //}
                unlockUI();
               
            });
            var url = 'api/MstCustomertmp/GetCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.lsmstcount = resp.data.lsmstcount;
                $scope.lstmpcount = resp.data.lstmpcount;
            });
            var url = 'api/MstCustomerAdd/GetTempCustomerdetail';
            lockUI();
            SocketService.get(url).then(function (resp) {
              
                $scope.temp_customer = resp.data.customer_list;
                //if ($scope.temp_customer == null) {
                //    $scope.totalDisplayedpending = 0;
                //    $scope.pendingCount = 0;
                //}
                //else {
                //    $scope.pendingCount = $scope.temp_customer.length;
                //    if ($scope.temp_customer.length < 100) {
                //        $scope.totalDisplayedpending = $scope.temp_customer.length;
                //    }
                //}
                unlockUI();
               
            });
        }

        //$scope.loadMorepending = function (pagecountpending) {
        //    lockUI();
        //    var Number = parseInt(pagecountpending);

        //    $scope.totalDisplayedpending += Number;
        //    unlockUI();
        //};
        //$scope.loadMorecompleted = function (pagecountcompleted) {
        //    lockUI();
        //    var Number = parseInt(pagecountcompleted);

        //    $scope.totalDisplayedcompleted += Number;
        //    unlockUI();
        //};
        //---Add Customer---//
        $scope.addcustomer=function()
        {
            $state.go('app.MstCustomeradd');
        }
        $scope.customerView = function (val) {
            $scope.customer_gid = val;
            $scope.customer_gid = localStorage.setItem('customer_gid', val);
            $state.go('app.MstCustomerView');
        }
        $scope.customertmpView = function (val) {
            $scope.customer_gid = val;
            $scope.customer_gid = localStorage.setItem('customer_gid', val);
            $state.go('app.MstCustomertmpView');
        }
        $scope.customerEdit = function (val) {
            $scope.customer_gid = val;
            $scope.customer_gid = localStorage.setItem('customer_gid', val);
            $state.go('app.MstCustomerEdit');
        }

        $scope.updatecustomerURN = function (customer_gid, customername, customer_urn) {
            var modalInstance = $modal.open({
                templateUrl: '/updateURN.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.customername = customername;

                if (customer_urn != "") {
                    $scope.customer_urn = customer_urn;
                }
                else {
                    $scope.customer_urn = "-";
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.UrnUpdate = function () {

                    var params = {
                        customer_gid: customer_gid,
                        newcustomer_urn: $scope.txtnewcustomerURN,
                        currentcustomer_urn: customer_urn
                    }

                    lockUI();
                    var url = "api/customer/GetNewCustomerURN";
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
                            $modalInstance.close('closed');
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

        $scope.delete = function (customer_gid) {
            var params = {
                customer_gid: customer_gid
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
                    var url = 'api/customer/customerDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred !', {
                                status: 'warning',
                                pos: 'top-right',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };

        $scope.tagcustomer = function (customer_gid, customer_urn, customername) {
            var modalInstance = $modal.open({
                templateUrl: '/tagcustomer.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
            vm.close = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.closed = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var d = new Date();
            $scope.txtskipvalid_date = (new Date(d.setDate(d.getDate() + 15)));
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.customer_urn = customer_urn;
                $scope.customer_name = customername;
                $scope.confirm = function () {
                    lockUI();
                    var params = {
                        customer_gid: customer_gid,
                         }
                    var url = "api/MstCustomerAdd/PostTagCustomer"

                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            activate();
                            Notify.alert(resp.data.message,'success')
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning')
                        }
                        $modalInstance.close('closed');
                    });
                }

            }
        }
    }
})();
