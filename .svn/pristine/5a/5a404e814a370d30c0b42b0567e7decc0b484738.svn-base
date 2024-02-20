(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCovenantPeriodController', MstCovenantPeriodController);

        MstCovenantPeriodController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCovenantPeriodController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCovenantPeriodController'
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            
         var url = 'api/MstApplication360/GetCovenantPeriod';
          lockUI();
         
         SocketService.get(url).then(function (resp) {
         $scope.covenantperiod_list = resp.data.covenantperiod_list;
         unlockUI();
         });
        }

        // Add
             
        $scope.addcovenantperiod = function () {
                var modalInstance = $modal.open({
                templateUrl: '/addpopup.html',
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
                        covenantperiod_name: $scope.txtcovenantperiod_name,
                         lms_code: $scope.txtlms_code,
                         bureau_code: $scope.txtbureau_code,
                         remarks:$scope.txtremarks
                    }
                var url = 'api/MstApplication360/CreateCovenantPeriod';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
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
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }
                
            }
        }

        //Edit

        $scope.editcovenantperiod = function (covenantperiod_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editpopup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    covenantperiod_gid: covenantperiod_gid
                }
                var url = 'api/MstApplication360/EditCovenantPeriod';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditcovenantperiod_name = resp.data.covenantperiod_name;
                    $scope.txteditcovenantperiod_code = resp.data.covenantperiod_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditremarks = resp.data.remarks;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {
                    var url = 'api/MstApplication360/UpdateCovenantPeriod';
                    var params = {
                        covenantperiod_gid: covenantperiod_gid,
                        covenantperiod_name: $scope.txteditcovenantperiod_name,
                        covenantperiod_code: $scope.txteditcovenantperiod_code,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        remarks: $scope.txteditremarks
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


    // Remarks
        
        $scope.showremarks = function (remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/remarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.remarks = remarks;
                $scope.back = function () {
                $modalInstance.close('closed');
                };
            }
        }

        //Delete
        
        $scope.delete = function (covenantperiod_gid) {
            var params = {
                covenantperiod_gid: covenantperiod_gid
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
                   var url = 'api/MstApplication360/DeleteCovenantPeriod';
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


       // status

        $scope.Status_update = function (covenantperiod_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscovenantperiod.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                                   
                var params = {
                    covenantperiod_gid: covenantperiod_gid
                }
                var url = 'api/MstApplication360/EditCovenantPeriod';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.covenantperiod_gid = resp.data.covenantperiod_gid
                    $scope.txteditcovenantperiod_name = resp.data.covenantperiod_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        covenantperiod_gid: covenantperiod_gid,
                        covenantperiod_name: $scope.txtcovenantperiod_name,
                        remarks: $scope.txtremarks,
                        Status: $scope.rbo_status
                    }
                    var url = 'api/MstApplication360/InactiveCovenantPeriod';
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

                    }
                    );

                    $modalInstance.close('closed');

                }
              
                var param = {
                    covenantperiod_gid: covenantperiod_gid
                }
                var url = 'api/MstApplication360/GetCovenantPeriodInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                $scope.covenantperiodinactivelog_data = resp.data.covenantperiod_list;
                    unlockUI();
                });
            }
        }


    }

    

})();