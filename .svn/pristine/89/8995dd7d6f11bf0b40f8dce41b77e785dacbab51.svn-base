(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditInstalmentFrequencyController', MstCreditInstalmentFrequencyController);

    MstCreditInstalmentFrequencyController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCreditInstalmentFrequencyController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditInstalmentFrequencyController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            $scope.total = 0;
            $scope.totalDisplayed = 100;
            var url = 'api/MstApplication360/GetCreditInstalmentFrequencySummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.creditinstalmentfrequency_data = resp.data.application_list;
                unlockUI();

                // new code start   
                if ($scope.creditinstalmentfrequency_data == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.creditinstalmentfrequency_data.length;
                    if ($scope.creditinstalmentfrequency_data.length < 100) {
                        $scope.totalDisplayed = $scope.creditinstalmentfrequency_data.length;
                    }
                }
                // new code endd
            });
        }

        $scope.addCreditInstalmentFrequency = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addCreditInstalmentFrequency.html',
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
                $scope.submit = function () {

                    var params = {
                        creditinstalmentfrequency_name: $scope.txtcreditinstalmentfrequency_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreditInstalmentFrequencyAdd';
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
                                status: 'info',
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

        $scope.editCreditInstalmentFrequency = function (creditinstalmentfrequency_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editCreditInstalmentFrequency.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    creditinstalmentfrequency_gid: creditinstalmentfrequency_gid
                }
                var url = 'api/MstApplication360/CreditInstalmentFrequencyEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditcreditinstalmentfrequency_name = resp.data.creditinstalmentfrequency_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.creditinstalmentfrequency_gid = resp.data.creditinstalmentfrequency_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/CreditInstalmentFrequencyUpdate';
                    var params = {
                        creditinstalmentfrequency_name: $scope.txteditcreditinstalmentfrequency_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        creditinstalmentfrequency_gid: $scope.creditinstalmentfrequency_gid
                    }
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
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });

                }
                
            }
        }

        $scope.Status_update = function (creditinstalmentfrequency_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusCreditInstalmentFrequency.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    creditinstalmentfrequency_gid: creditinstalmentfrequency_gid
                }
                var url = 'api/MstApplication360/CreditInstalmentFrequencyEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.creditinstalmentfrequency_gid = resp.data.creditinstalmentfrequency_gid
                    $scope.txtcreditinstalmentfrequency_name = resp.data.creditinstalmentfrequency_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        creditinstalmentfrequency_gid: creditinstalmentfrequency_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }

                    var url = 'api/MstApplication360/CreditInstalmentFrequencyInactive';
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }


                var param = {
                    creditinstalmentfrequency_gid: creditinstalmentfrequency_gid
                }

                var url = 'api/MstApplication360/CreditInstalmentFrequencyInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.creditinstalmentfrequencyinactivelog_data = resp.data.application_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (creditinstalmentfrequency_gid) {
            var params = {
                creditinstalmentfrequency_gid: creditinstalmentfrequency_gid
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
                    var url = 'api/MstApplication360/CreditInstalmentFrequencyDelete';
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
    }
})();

