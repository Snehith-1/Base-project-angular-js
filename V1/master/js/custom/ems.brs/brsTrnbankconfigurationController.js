(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnbankconfigurationController', brsTrnbankconfigurationController);

    brsTrnbankconfigurationController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function brsTrnbankconfigurationController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnbankconfigurationController';
        activate();


        function activate() {

            var url = 'api/ConfigurationReconcillation/GetConfigurationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.configuration_summary = resp.data.configuration_summary;
                unlockUI();
            });
        }
        $scope.addconfiguration = function () {
            $state.go('app.brsTrnbankconfigurationadd');

        }

        $scope.editconfiguration = function (bankconfig_gid) {
         /*   $state.go('app.brsTrnbankconfigurationedit');*/
            //$location.url('app/brsTrnbankconfigurationedit?bankconfig_gid=' + bankconfig_gid);
            $location.url("app/brsTrnbankconfigurationedit?hash=" + cmnfunctionService.encryptURL("bankconfig_gid=" + bankconfig_gid));

        }


        //$scope.popupconfiguration = function () {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/mybankContent.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {
        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };

        //        $scope.bankSubmit = function () {
        //            var params = {
        //                bank_name: $scope.txtbank_name,
        //                acc_no: $scope.txtacc_no,
        //                custref_no: $scope.txtcustref_no,
        //                branch_name: $scope.txtbranch
        //            }

        //            var url = 'api/BankReconcillation/Addbank';
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                else {
        //                    $modalInstance.close('closed');
        //                    Notify.alert(resp.data.message, {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }

        //                activate();

        //            });
        //            $modalInstance.close('closed');

        //        }

        //    }
        //}

        $scope.editbankfield = function (bank_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editbank.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    bank_gid: bank_gid
                }
                var url = 'api/BankReconcillation/EditBank';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditbank_name = resp.data.bank_name;
                    $scope.txteditacc_no = resp.data.acc_no;
                    $scope.txteditcustref_no = resp.data.custref_no;
                    $scope.txteditbranch = resp.data.branch_name;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.Updatebank = function () {

                    var url = 'api/BankReconcillation/UpdateBank';
                    var params = {
                        bank_name: $scope.txteditbank_name,
                        acc_no: $scope.txteditacc_no,
                        custref_no: $scope.txteditcustref_no,
                        branch_name: $scope.txteditbranch,
                        bank_gid: bank_gid
                    }
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

            }
        }


        $scope.deleteconfiguration = function (bankconfig_gid) {
            var params = {
                bankconfig_gid: bankconfig_gid
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
                    var url = 'api/ConfigurationReconcillation/DeleteConfigurationdata';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');

                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            });
        };
    }

})();