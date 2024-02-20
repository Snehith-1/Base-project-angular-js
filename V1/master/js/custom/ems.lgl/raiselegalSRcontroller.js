(function () {
    'use strict';

    angular
        .module('angle')
        .controller('raiselegalSRcontroller', raiselegalSRcontroller);



    raiselegalSRcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function raiselegalSRcontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {


        /* jshint validthis:true */
        var vm = this;
        vm.title = 'raiselegalSRcontroller';
        activate();
        function activate() {
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };
            $scope.borrwer_dtl = false;
            $scope.customer_dtl = false;
            $scope.customer_panel = false;
            $scope.loan_dtl = false;
            vm.close = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.closed = true;
            };


            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var url = 'api/raiseLegalSR/Customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;

            });

        }

        $scope.legalSRback = function (val) {
            $location.url('app/requestCompliancesummary?lstab=legalsr');
          
        }


        $scope.onselectedchangecustomer = function (customer) {
            $scope.customer_gid = localStorage.setItem('onchangecustomer_gid', customer);
             var params = {
                    customer_gid: $scope.cbocustomergid.customer_gid
              
            }
            //var url = 'api/raiseLegalSR/Getpromoter';

            //SocketService.getparams(url, params).then(function (resp) {
            //    $scope.promoters_list = resp.data.promoter_list;

            //});
            //var url = 'api/raiseLegalSR/Getguarantor';

            //SocketService.getparams(url, params).then(function (resp) {
            //    $scope.guarantors_list = resp.data.guarantor_list;

            //});

            ////console.log(params);
            //var url = 'api/raiseLegalSR/getfacilitydtl';

            //SocketService.getparams(url, params).then(function (resp) {
            //    $scope.facility_list = resp.data.facility_list;

            //});

            var url = 'api/raiseLegalSR/Getcustomerdtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.address = resp.data.address;
                $scope.email = resp.data.email_id;
                $scope.mobileno = resp.data.mobile_no;
console.log(resp.data.mobile_no);
            });
            $scope.borrwer_dtl = true;
            $scope.customer_dtl = true;
            $scope.customer_panel = true;
            $scope.loan_dtl = true;
            $scope.rm_dtl = true;
        }

        $scope.addfacility = function (customer) {
            var modalInstance = $modal.open({
                templateUrl: '/facilities.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.customerfacilitySubmit = function () {

                    var params = {
                        facility_type: $scope.txtfacility,
                        limit: $scope.txtlimit,
                        outstanding: $scope.txtoutstanding,
                        customer_gid: customer

                    }

                    var url = 'api/raiseLegalSR/facility';
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            var params = {
                                customer_gid: localStorage.getItem('onchangecustomer_gid')
                            }
                            test();
                            var url = 'api/raiseLegalSR/getfacilitydtl';

                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.facility_list = resp.data.facility_list;
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });


                }
            }
        }

        var params = {
            customer_gid: localStorage.getItem('onchangecustomer_gid')
        }
        //test();
        var url = 'api/raiseLegalSR/getfacilitydtl';

        SocketService.getparams(url, params).then(function (resp) {
            $scope.facility_list = resp.data.facility_list;
        });

       
        $scope.legalsrsubmit = function (customer) {
            var account_name = $('#customername :selected').text();

                        var params = {
                            customer_gid: $scope.cbocustomergid.customer_gid,
                            account_name: account_name,
                            remarks: $scope.txtremarks
                        }
                     
                        var url = 'api/raiseLegalSR/raiselegalsr';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                    activate();
                    $location.url('app/requestCompliancesummary?lstab=legalsr');
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
            });
        }


    }

})();
