(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstOccupationcontroller', MstOccupationcontroller);

    MstOccupationcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function MstOccupationcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstOccupationcontroller';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            var url = 'api/MstApplication360/GetOccupation';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.occupation_list = resp.data.application_list;
                unlockUI();
            });
           
        }

  

        //<!--ADD CODE START-->
        $scope.popupOccupation = function () {
            var modalInstance = $modal.open({
                templateUrl: '/occupationadd.html',
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

                $scope.OccupationSubmit = function () {
                    var params = {
                        Occupation_name: $scope.txtaddOccupation_name,
                        bureau_code: $scope.txtbureau_code,
                        lms_code: $scope.txtlms_code
                    }
                   
                    var url = 'api/MstApplication360/CreateOccupation';               
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


        $scope.editOccupation = function (occupation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editOccupation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    occupation_gid: occupation_gid
                   
                }
                var url = 'api/MstApplication360/EditOccupation';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditOccupation_name = resp.data.occupation_name;                                   
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.occupation_gid = resp.data.occupation_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateOccupation';
                    var params = {
                        occupation_name: $scope.txteditOccupation_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        occupation_gid: $scope.occupation_gid
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
                            $modalInstance.close('closed');
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


        $scope.Status_update = function (occupation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusOccupation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    occupation_gid: occupation_gid
                }
                var url = 'api/MstApplication360/EditOccupation';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.occupation_gid = resp.data.occupation_gid;
                    $scope.txtoccupation_name = resp.data.occupation_name;
                    $scope.rbo_status = resp.data.Status;                 
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        occupation_gid: occupation_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status                                              

                    }
                    var url = 'api/MstApplication360/InactiveOccupation';
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
                    occupation_gid: occupation_gid
                }

                var url = 'api/MstApplication360/OccupationInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.occupationinactivelog_list = resp.data.application_list;
                    unlockUI();
                });

            }
        }
        //<!--STATUS CODE END-->

        //<!--DELETE CODE START-->

        $scope.delete = function (occupation_gid) {
            var params = {
                occupation_gid: occupation_gid
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
                    var url = 'api/MstApplication360/DeleteOccupation';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting!', {
                                status: 'warning',
                                pos: 'top-right',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
        //<!--DELETE CODE END-->



    }
})();
