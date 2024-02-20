(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSecurityCoverageController', MstSecurityCoverageController);

        MstSecurityCoverageController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstSecurityCoverageController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSecurityCoverageController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
            var url = 'api/MstApplication360/GetSecurityCoverage';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.securitycoverage_data = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addSecurityCoverage = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addSecurityCoverage.html',
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
                        securitycoverage_name: $scope.txtSecurityClassification_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreateSecurityCoverage';
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });

                   // $modalInstance.close('closed');

                }
            }
        }

        $scope.editSecurityCoverage = function (securitycoverage_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editSecurityCoverage.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    securitycoverage_gid: securitycoverage_gid
                }
                var url = 'api/MstApplication360/EditSecurityCoverage';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditSecurityClassification_name = resp.data.securitycoverage_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.securitycoverage_gid = resp.data.securitycoverage_gid;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                                              
                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateSecurityCoverage';
                    var params = {
                        securitycoverage_name: $scope.txteditSecurityClassification_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        securitycoverage_gid: $scope.securitycoverage_gid
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

        $scope.Status_update = function (securitycoverage_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusSecurityCoverage.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                                
                var params = {
                    securitycoverage_gid: securitycoverage_gid
                }
                var url = 'api/MstApplication360/EditSecurityCoverage';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.securitycoverage_gid = resp.data.securitycoverage_gid
                    $scope.txtsecuritycoverage_name = resp.data.securitycoverage_name;
                    $scope.rbo_status = resp.data.Status;
                });
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        securitycoverage_name: $scope.txtsecuritycoverage_name,
                        securitycoverage_gid: $scope.securitycoverage_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    
                    }
                    var url = 'api/MstApplication360/InactiveSecurityCoverage';
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
                    securitycoverage_gid: securitycoverage_gid
                }

                var url = 'api/MstApplication360/InactiveSecurityCoverageHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.securitycoverageinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (securitycoverage_gid) {
              var params = {
                 securitycoverage_gid: securitycoverage_gid
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
                     var url = 'api/MstApplication360/DeleteSecurityCoverage';
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

