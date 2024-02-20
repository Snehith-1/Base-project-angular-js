(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditUserSummaryController', MstCreditUserSummaryController);

    MstCreditUserSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];


    function MstCreditUserSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {

        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditUserSummaryController';

        activate();

        function activate() {
            var url = 'api/MstApplication360/GetCreditTypeSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.credittype_data = resp.data.application_list;
            });
        }

        $scope.addcredituser = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcredituser.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submitcredituser = function () {

                    var params = {
                        credittype_name: $scope.txtcredituser_code,
                        lms_code: $scope.txtcredituser_name,
                        bureau_code: $scope.txtcredituser_role

                    }
                    var url = 'api/MstApplication360/CreditTypeAdd';
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
        $scope.editcredituser = function () {
            var modalInstance = $modal.open({
                templateUrl: '/editcredituser.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                //var params = {
                   
                //}
               
                //var url = 'api/MstApplication360/CreditTypeEdit';
                //SocketService.getparams(url, params).then(function (resp) {

                //    $scope.txteditcredituser_code = resp.data.credittype_name;
                //    $scope.txteditcredituser_name = resp.data.lms_code;
                //    $scope.txteditcredituser_role = resp.data.bureau_code;
                //    $scope.credittype_gid = resp.data.credittype_gid;
                //});

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/CreditTypeUpdate';
                    var params = {
                        credittype_name: $scope.txteditcredituser_code,
                        lms_code: $scope.txteditcredituser_name,
                        bureau_code: $scope.txteditcredituser_role,
                        credittype_gid: $scope.credittype_gid
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

        $scope.delete = function (credittype_gid) {
            var params = {
                credittype_gid: credittype_gid
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
                    var url = 'api/MstApplication360/CreditTypeDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Credit Type!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
    }
})();
