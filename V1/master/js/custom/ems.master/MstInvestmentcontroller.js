(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstInvestmentcontroller', MstInvestmentcontroller);

    MstInvestmentcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function MstInvestmentcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstInvestmentcontroller';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/MstApplication360/GetInvestment';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.investment_list = resp.data.application_list;
                unlockUI();
            });

        }



        //<!--ADD CODE START-->
        $scope.popupInvestment = function () {
            var modalInstance = $modal.open({
                templateUrl: '/investmentadd.html',
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

                $scope.InvestmentSubmit = function () {
                    var params = {
                        Investment_name: $scope.txtaddInvestment_name,
                        bureau_code: $scope.txtbureau_code,
                        lms_code: $scope.txtlms_code
                    }
                    
                    var url = 'api/MstApplication360/CreateInvestment';
                    SocketService.post(url, params).then(function (resp) {
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
        //<!--ADD CODE END-->



        //<!--EDIT CODE START-->


        $scope.editInvestment = function (investment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editInvestment.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    investment_gid: investment_gid

                }
                var url = 'api/MstApplication360/EditInvestment';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditInvestment_name = resp.data.investment_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.investment_gid = resp.data.investment_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateInvestment';
                    var params = {
                        investment_name: $scope.txteditInvestment_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        investment_gid: $scope.investment_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
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

        //<!--EDIT CODE END-->


        //<!--STATUS CODE START-->
        
        $scope.Status_update = function (investment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusInvestment.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    investment_gid: investment_gid
                }
                var url = 'api/MstApplication360/EditInvestment';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.investment_gid = resp.data.investment_gid;
                    $scope.txtinvestment_name = resp.data.investment_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        investment_gid: investment_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveInvestment';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }

                var param = {
                    investment_gid: investment_gid
                }

                var url = 'api/MstApplication360/InvestmentInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.investmentinactivelog_list = resp.data.application_list;
                    unlockUI();
                });

            }
        }
        //<!--STATUS CODE END-->

        //<!--DELETE CODE START-->

        $scope.delete = function (investment_gid) {
            var params = {
                investment_gid: investment_gid
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
                    lockUI();
                    var url = 'api/MstApplication360/DeleteInvestment';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI;
                        }
                    });
                    }
            });
        }
        //<!--DELETE CODE END-->



    }
})();
