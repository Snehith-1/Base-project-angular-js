(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customerManagement360controller', customerManagement360controller);

    customerManagement360controller.$inject = ['$rootScope', '$scope', '$state','$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams','SweetAlert'];

    function customerManagement360controller($rootScope, $scope, $state,$modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, SweetAlert) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'customerManagement360controller';

        activate();

        function activate() {
            $scope.MyCustomer = localStorage.getItem('MyCustomer');
            var params = {
                customer_gid: localStorage.getItem('customer_gid')
            }
            
            var url = "api/customer/Getcustomerdetails";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerdetails = resp.data;
              
            });

            var url = "api/customerManagement/getcustomerPromoter";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerPromotorlist = resp.data.customerPromoter;
               
            });
           
            var url = "api/customerManagement/getcustomerGuarantors";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerguarantorlist = resp.data.customerGuarantors;
            });

            var url = "api/customerManagement/getCollateraldetail";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerCollateral = resp.data.customerCollateral;
            });
 
        }
      
        $scope.addgurantor = function () {
            var modalInstance = $modal.open({
                templateUrl: '/GuarantorsAddModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.customerGuarantorsSubmit = function () {

                    var params = {
                        guarantors_name: $scope.txtGuarantorsName,
                        guarantor_age: $scope.txtAge,
                        networth: $scope.txtNetWorth,
                        basisofNW: $scope.txtBasisofNW,
                        customer_gid: localStorage.getItem('customer_gid')
                    }
                    var url = "api/customerManagement/postcustomerGuarantors";
                    SocketService.post(url, params).then(function (resp) {
                        console.log(resp);
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
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });
                }
            }
        }

        $scope.addpromotor = function () {
           
            var modalInstance = $modal.open({
                templateUrl: '/promotorAddModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.customerPromoterSubmit = function () {

                    var params = {
                        promoter_name: $scope.txtPromoterName,
                        designation: $scope.txtDesignation,
                        promoter_age: $scope.txtAge,
                        mobile: $scope.txtmobile,
                        customer_gid: localStorage.getItem('customer_gid')
                    }
                    
                    var url = "api/customerManagement/postcustomerPromoter";
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
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });
                }
            }
        }

      

     

        $scope.deleteguarantor = function (customer2guarantor_gid) {
            var params = {
                customer2guarantor_gid: customer2guarantor_gid,
               
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
                    var url = "api/customerManagement/postGuarantorsdetail";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        }

        $scope.deletepromotor = function (customer2promotor_gid) {
            var params = {
                customer2promotor_gid: customer2promotor_gid
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
                    var url = "api/customerManagement/postPromoterdetail";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        }

        $scope.editpromotor = function (customer2promotor_gid) {
           
            var modalInstance = $modal.open({
                templateUrl: '/promotorModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer2promotor_gid: customer2promotor_gid
                }
                var url = "api/customerManagement/getPromoterdetail"
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtPromoterNameedit = resp.data.promoter_name
                    $scope.txtDesignationedit = resp.data.designation
                    $scope.txtAgeedit = resp.data.promoter_age
                    $scope.txtmobileedit = resp.data.mobile
                    
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.updatepromotor = function () {
                    var params = {
                        customer2promotor_gid: customer2promotor_gid,
                        promoter_name: $scope.txtPromoterNameedit,
                        designation: $scope.txtDesignationedit,
                        promoter_age: $scope.txtAgeedit,
                        mobile: $scope.txtmobileedit
                    }
                    console.log(params);
                    var url = "api/customerManagement/postcustomerPromoterEdit";
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
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                }
            }
        }

        $scope.editguarantor = function (customer2guarantor_gid) {
           
            var modalInstance = $modal.open({
                templateUrl: '/gurantorModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer2guarantor_gid: customer2guarantor_gid
                }
                var url = "api/customerManagement/getGuarantorsdetail"
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtGuarantorsNameedit = resp.data.guarantors_name
                    $scope.txtAgeedit = resp.data.guarantor_age
                    $scope.txtNetWorthedit = resp.data.networth
                    $scope.txtBasisofNWedit = resp.data.basisofNW
                    console.log(resp);
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.updateguarantor = function () {
                    var params = {
                        customer2guarantor_gid: customer2guarantor_gid,
                        guarantors_name: $scope.txtGuarantorsNameedit,
                        guarantor_age: $scope.txtAgeedit,
                        networth: $scope.txtNetWorthedit,
                        basisofNW: $scope.txtBasisofNWedit
                    }
                    console.log(params);
                    var url = "api/customerManagement/postcustomerGuarantorsEdit";
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
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                }
            }
        }

        $scope.back = function () {
            $state.go('app.customerManagement');
        }
    }
})();
