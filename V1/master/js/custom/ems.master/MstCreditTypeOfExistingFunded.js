(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditTypeofExistingFundedController', MstCreditTypeofExistingFundedController);

    MstCreditTypeofExistingFundedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCreditTypeofExistingFundedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditTypeofExistingFundedController';

        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            
            var url = 'api/MstApplication360/GetCreditTypeOfExistingFundedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.credittypeexistingfund_data = resp.data.application_list;
                unlockUI();
               
            });
        }

        $scope.addCreditTypeofExistingFunded = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addCreditTypeofExistingFunded.html',
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
                        credittypeofexistingfunded_name: $scope.txtcredittypeofexistingfunded_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreditTypeOfExistingFundedAdd';
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

        $scope.editCreditTypeofExistingFund = function (credittypeofexistingfunded_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editCreditTypeofExistingFund.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    credittypeofexistingfunded_gid: credittypeofexistingfunded_gid
                }
                var url = 'api/MstApplication360/CreditTypeOfExistingFundedEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditcredittypeofexistingfunded_name = resp.data.credittypeofexistingfunded_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.credittypeofexistingfunded_gid = resp.data.credittypeofexistingfunded_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/CreditTypeOfExistingFundedUpdate';
                    var params = {
                        credittypeofexistingfunded_name: $scope.txteditcredittypeofexistingfunded_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        credittypeofexistingfunded_gid: $scope.credittypeofexistingfunded_gid
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

        $scope.Status_update = function (credittypeofexistingfunded_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusCreditTypeofExistingFund.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    credittypeofexistingfunded_gid: credittypeofexistingfunded_gid
                }
                var url = 'api/MstApplication360/CreditTypeOfExistingFundedEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.credittypeofexistingfunded_gid = resp.data.credittypeofexistingfunded_gid
                    $scope.txtcredittypeofexistingfunded_name = resp.data.credittypeofexistingfunded_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        credittypeofexistingfunded_gid: credittypeofexistingfunded_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }

                    var url = 'api/MstApplication360/CreditTypeOfExistingFundedInactive';
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
                    credittypeofexistingfunded_gid: credittypeofexistingfunded_gid
                }

                var url = 'api/MstApplication360/CreditTypeExistingFundInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.credittypeofexistingfundinactivelog_data = resp.data.application_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (credittypeofexistingfunded_gid) {
            var params = {
                credittypeofexistingfunded_gid: credittypeofexistingfunded_gid
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
                    var url = 'api/MstApplication360/CreditTypeOfExistingFundedDelete';
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

