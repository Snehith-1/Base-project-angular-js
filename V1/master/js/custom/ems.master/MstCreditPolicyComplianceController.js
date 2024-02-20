(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditPolicyComplianceController', MstCreditPolicyComplianceController);

        MstCreditPolicyComplianceController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCreditPolicyComplianceController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditPolicyComplianceController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() { 
            var url = 'api/MstApplication360/GetCreditPolicyCompliance';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.creditpolicycompliance_list = resp.data.application_list;
               unlockUI();

            });
        }

        $scope.addcreditpolicycompliance = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcreditpolicycompliance.html',
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
                        creditpolicycompliance_name: $scope.txtcreditpolicycompliance_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                   var url = 'api/MstApplication360/CreateCreditPolicyCompliance';
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

        $scope.editcreditpolicycompliance = function (creditpolicycompliance_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcreditpolicycompliance.html',
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
                    creditpolicycompliance_gid: creditpolicycompliance_gid
                }
                var url = 'api/MstApplication360/EditCreditPolicyCompliance';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditcreditpolicycompliance_name = resp.data.creditpolicycompliance_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.creditpolicycompliance_gid = resp.data.creditpolicycompliance_gid;
                });
                $scope.update = function () {

                    var params = {
                        creditpolicycompliance_name: $scope.txteditcreditpolicycompliance_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        creditpolicycompliance_gid: $scope.creditpolicycompliance_gid
                    }
                    var url = 'api/MstApplication360/UpdateCreditPolicyCompliance';
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
                            activate();
                        }
                    }); $modalInstance.close('closed');

                }
                
            }
        }

        $scope.Status_update = function (creditpolicycompliance_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscreditpolicycompliance.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                                
                var params = {
                    creditpolicycompliance_gid: creditpolicycompliance_gid
                }

                var url = 'api/MstApplication360/EditCreditPolicyCompliance';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.creditpolicycompliance_gid = resp.data.creditpolicycompliance_gid
                    $scope.txtcreditpolicycompliance_name = resp.data.creditpolicycompliance_name;
                    $scope.rbo_status = resp.data.Status;
                }); 
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                   var params = {
                        creditpolicycompliance_name: $scope.txtcreditpolicycompliance_name,
                        creditpolicycompliance_gid: $scope.creditpolicycompliance_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    
                    }
                    var url = 'api/MstApplication360/InactiveCreditPolicyCompliance';
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
                    creditpolicycompliance_gid: creditpolicycompliance_gid
                }

               var url = 'api/MstApplication360/CreditPolicyComplianceInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.creditpolicycomplianceinactivelog_lists = resp.data.application_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (creditpolicycompliance_gid) {
            var params = {
                creditpolicycompliance_gid: creditpolicycompliance_gid
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
                     var url = 'api/MstApplication360/DeleteCreditPolicyCompliance';
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

