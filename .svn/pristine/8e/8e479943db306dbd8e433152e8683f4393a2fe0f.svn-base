(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCountryCodeController', MstCountryCodeController);

        MstCountryCodeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCountryCodeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCountryCodeController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
           
            var url = 'api/MstApplication360/GetCountryCode';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.countrycode_list = resp.data.countrycode_list;
                unlockUI();

               
            });
        }

        $scope.addcountrycode = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcountrycode.html',
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
                        country_code: $scope.txtcountrycode_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreateCountryCode';
                     lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
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

        $scope.editcountrycode = function (countrycode_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcountrycode.html',
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
               var params = {
                    countrycode_gid: countrycode_gid
                }
               
                var url = 'api/MstApplication360/EditCountryCode';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.countrycode_gid = resp.data.countrycode_gid;
                    $scope.txteditcountrycode_name = resp.data.country_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.rbo_status = resp.data.status_log;
                });
                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateCountryCode';
                    var params = {
                        country_code: $scope.txteditcountrycode_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        countrycode_gid: $scope.countrycode_gid
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

        $scope.Status_update = function (countrycode_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscountrycode.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
             var params = {
                    countrycode_gid: countrycode_gid
                } 
             var url = 'api/MstApplication360/EditCountryCode';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.countrycode_gid=resp.data.countrycode_gid;
                    $scope.txtcountrycode_name = resp.data.country_code;
                    $scope.rbo_status = resp.data.status_log;
                });     
                var url = 'api/MstApplication360/GetCountryCodeActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.countrycode_list = resp.data.countrycode_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        countrycode_gid: countrycode_gid,
                        country_code: $scope.txtcountrycode_name,
                        remarks: $scope.txtremarks,
                        status_log:$scope.rbo_status
                    
                    }
                    var url = 'api/MstApplication360/CountryStatusUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    }); 

                    $modalInstance.close('closed');

                }
            
            }
        }

        $scope.delete = function (countrycode_gid) {
             var params = {
                countrycode_gid: countrycode_gid
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
                   
                    var url = 'api/MstApplication360/CountryCodeDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        
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
                          
                        }
                    });
                    }
            });
        }
    }
})();

