(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditAccountClassificationController', MstCreditAccountClassificationController);

    MstCreditAccountClassificationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCreditAccountClassificationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditAccountClassificationController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            $scope.total = 0;
            $scope.totalDisplayed = 100;
            var url = 'api/MstApplication360/GetCreditAccountClassificationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.creditaccountclassification_data = resp.data.application_list;
                unlockUI();
                
                // new code start   
                if ($scope.creditaccountclassification_data == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.creditaccountclassification_data.length;
                    if ($scope.creditaccountclassification_data.length < 100) {
                        $scope.totalDisplayed = $scope.creditaccountclassification_data.length;
                    }
                }
                // new code endd
            });
        }

        $scope.addCreditAccountClassification = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addCreditAccountClassification.html',
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
                        creditaccountclassification_name: $scope.txtcreditaccountclassification_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreditAccountClassificationAdd';
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

        $scope.editCreditAccountClassification = function (creditaccountclassification_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editCreditAccountClassification.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    creditaccountclassification_gid: creditaccountclassification_gid
                }
                var url = 'api/MstApplication360/CreditAccountClassificationEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditcreditaccountclassification_name = resp.data.creditaccountclassification_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.creditaccountclassification_gid = resp.data.creditaccountclassification_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/CreditAccountClassificationUpdate';
                    var params = {
                        creditaccountclassification_name: $scope.txteditcreditaccountclassification_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        creditaccountclassification_gid: $scope.creditaccountclassification_gid
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

        $scope.Status_update = function (creditaccountclassification_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusCreditAccountClassification.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    creditaccountclassification_gid: creditaccountclassification_gid
                }
                var url = 'api/MstApplication360/CreditAccountClassificationEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.creditaccountclassification_gid = resp.data.creditaccountclassification_gid
                    $scope.txteditcreditaccountclassification_name = resp.data.creditaccountclassification_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        creditaccountclassification_gid: creditaccountclassification_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }


                    var url = 'api/MstApplication360/CreditAccountClassificationInactive';
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
                    creditaccountclassification_gid: creditaccountclassification_gid
                }

                var url = 'api/MstApplication360/CreditAccountClassificationInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.creditaccountclassification_data = resp.data.application_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (creditaccountclassification_gid) {
            var params = {
                creditaccountclassification_gid: creditaccountclassification_gid
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
                    var url = 'api/MstApplication360/CreditAccountClassificationDelete';
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

