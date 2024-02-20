(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditAdminSummaryController', MstCreditAdminSummaryController);

    MstCreditAdminSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];


    function MstCreditAdminSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {

        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditAdminSummaryController';

        activate();

        function activate() {
            var url = 'api/MstApplication360/GetCreditTypeSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.credittype_data = resp.data.application_list;
            });
        }

        $scope.addcreditadmin = function ()
        {
            var modalInstance = $modal.open({
                templateUrl: '/addcreditadmin.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        credittype_name: $scope.txtcreditadmin_code,
                        lms_code: $scope.txtcreditadmin_name,
                        bureau_code: $scope.txtcreditadmin_role

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
        $scope.editcreditadmin = function ()
        {
            var modalInstance = $modal.open({
                templateUrl: '/editcreditadmin.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                //var params = {
                //    credittype_gid: credittype_gid
                //}
                //console.log(params);
                //var url = 'api/MstApplication360/CreditTypeEdit';
                //SocketService.getparams(url, params).then(function (resp) {

                //    $scope.txteditcreditadmin_code = resp.data.credittype_name;
                //    $scope.txteditcreditadmin_name = resp.data.lms_code;
                //    $scope.txteditcreditadmin_role = resp.data.bureau_code;
                //    $scope.credittype_gid = resp.data.credittype_gid;
                //});

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/CreditTypeUpdate';
                    var params = {
                        credittype_name: $scope.txteditcreditadmin_code,
                        lms_code: $scope.txteditcreditadmin_name,
                        bureau_code: $scope.txteditcreditadmin_role,
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
