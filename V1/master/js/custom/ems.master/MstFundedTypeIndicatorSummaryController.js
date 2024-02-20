(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstFundedTypeIndicatorSummaryController', MstFundedTypeIndicatorSummaryController);

    MstFundedTypeIndicatorSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstFundedTypeIndicatorSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstFundedTypeIndicatorSummaryController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            var url = 'api/MstApplication360/GetFundedTypeIndicator';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.fundedtypeindicator_lists = resp.data.application_list;
                unlockUI();

            });
        }

        $scope.addfundedtypeindicator = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addfundedtypeindicator.html',
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
                        fundedtypeindicator_name: $scope.txtfunded_type_indicator,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreateFundedTypeIndicator';
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

        $scope.editfundedtypeindicator = function (fundedtypeindicator_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editfundedtypeindicator.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                
                var params = {
                    fundedtypeindicator_gid: fundedtypeindicator_gid
                }
                var url = 'api/MstApplication360/EditFundedTypeIndicator';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditfunded_type_indicator = resp.data.fundedtypeindicator_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.fundedtypeindicator_gid = resp.data.fundedtypeindicator_gid;
                });
                

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateFundedTypeIndicator';
                     var params = {
                         fundedtypeindicator_name: $scope.txteditfunded_type_indicator,
                         fundedtypeindicator_gid: $scope.fundedtypeindicator_gid,
                         lms_code: $scope.txteditlms_code,
                         bureau_code: $scope.txteditbureau_code
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

        $scope.Status_update = function (fundedtypeindicator_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusfundedtypeindicator.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    fundedtypeindicator_gid: fundedtypeindicator_gid
                }

                var url = 'api/MstApplication360/EditFundedTypeIndicator';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtfundedtypeindicator_name = resp.data.fundedtypeindicator_name;
                    $scope.fundedtypeindicator_gid = resp.data.fundedtypeindicator_gid;
                    $scope.rbo_status = resp.data.Status;
                });  

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        fundedtypeindicator_name: $scope.txtfundedtypeindicator_name,
                        fundedtypeindicator_gid: $scope.fundedtypeindicator_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveFundedTypeIndicator';
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
                    fundedtypeindicator_gid: fundedtypeindicator_gid
                }

                var url = 'api/MstApplication360/InactiveFundedTypeIndicatorHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.fundedtypeindicatorinactivelog_lists = resp.data.inactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (fundedtypeindicator_gid) {
              var params = {
                 fundedtypeindicator_gid: fundedtypeindicator_gid
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
                    var url = 'api/MstApplication360/DeleteFundedTypeIndicator';
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

