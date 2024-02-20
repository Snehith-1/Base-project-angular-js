(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditTypeOfFacilityController', MstCreditTypeOfFacilityController);

    MstCreditTypeOfFacilityController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCreditTypeOfFacilityController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditTypeOfFacilityController';

        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {

            var url = 'api/MstApplication360/GetCreditTypeOfFacilitySummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.credittypeoffacility_data = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addCreditTypeofFacility = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addCreditTypeofFacility.html',
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
                        credittypeoffacility_name: $scope.txtcredittypeoffacility_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreateCreditTypeOfFacility';
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

        $scope.editCreditTypeofFacility = function (credittypeoffacility_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editCreditTypeofFacility.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    credittypeoffacility_gid: credittypeoffacility_gid
                }
                var url = 'api/MstApplication360/CreditTypeOfFacilityEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditcredittypeoffacility_name = resp.data.credittypeoffacility_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.credittypeoffacility_gid = resp.data.credittypeoffacility_gid;
                });
               
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/CreditTypeOfFacilityUpdate';
                     var params = {
                         credittypeoffacility_name: $scope.txteditcredittypeoffacility_name,
                         lms_code: $scope.txteditlms_code,
                         bureau_code: $scope.txteditbureau_code,
                         credittypeoffacility_gid: $scope.credittypeoffacility_gid
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

        $scope.Status_update = function (credittypeoffacility_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusCreditTypeofFacility.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    credittypeoffacility_gid: credittypeoffacility_gid
                }
                var url = 'api/MstApplication360/CreditTypeOfFacilityEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.credittypeoffacility_gid = resp.data.credittypeoffacility_gid
                    $scope.txteditcredittypeoffacility_name = resp.data.credittypeoffacility_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        credittypeoffacility_gid: credittypeoffacility_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }

                    var url = 'api/MstApplication360/CreditTypeOfFacilityInactive';
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
                    credittypeoffacility_gid: credittypeoffacility_gid
                }

                var url = 'api/MstApplication360/CreditTypeOfFacilityInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.credittypeoffacilityinactivelog_data = resp.data.application_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (credittypeoffacility_gid) {
             var params = {
                 credittypeoffacility_gid: credittypeoffacility_gid
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
                    var url = 'api/MstApplication360/CreditTypeOfFacilityDelete';
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

