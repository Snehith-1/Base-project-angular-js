(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSecurityClassificationController', MstSecurityClassificationController);

        MstSecurityClassificationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstSecurityClassificationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSecurityClassificationController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            var url = 'api/MstApplication360/GetSecurityClassification';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.securityclassification_data = resp.data.application_list;
                unlockUI();
            });

        }

        $scope.addSecurityClassification = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addSecurityClassification.html',
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
                        securityclassification_name: $scope.txtSecurityClassification_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreateSecurityClassification'; 
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
                            activate();
                        }
                    }); 

                    $modalInstance.close('closed');

                }
                
            }
        }

        $scope.editSecurityClassification = function (securityclassification_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editSecurityClassification.html',
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
                    securityclassification_gid: securityclassification_gid
                }
                var url = 'api/MstApplication360/EditSecurityClassification';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditSecurityClassification_name = resp.data.securityclassification_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.securityclassification_gid = resp.data.securityclassification_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {


                    var url = 'api/MstApplication360/UpdateSecurityClassification';
                    var params = {
                        securityclassification_name: $scope.txteditSecurityClassification_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                       securityclassification_gid: $scope.securityclassification_gid
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

        $scope.Status_update = function (securityclassification_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusSecurityClassification.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    securityclassification_gid: securityclassification_gid
                }
                 var url = 'api/MstApplication360/EditSecurityClassification';
                 SocketService.getparams(url, params).then(function (resp) {
                     $scope.securityclassification_gid = resp.data.securityclassification_gid
                     $scope.txtSecurityClassification_name = resp.data.securityclassification_name;
                     $scope.rbo_status = resp.data.Status;
                 });

                 $scope.ok = function () {
                     $modalInstance.close('closed');
                 };
                 $scope.update_status = function () {

                     var params = {
                         securityclassification_gid: securityclassification_gid,
                         remarks: $scope.txtremarks,
                         rbo_status: $scope.rbo_status

                     }
                     var url = 'api/MstApplication360/InactiveSecurityClassification';
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

                         activate();
                     });

                     $modalInstance.close('closed');
                 }
                    var param = {
                        securityclassification_gid: securityclassification_gid
                    }

                    var url = 'api/MstApplication360/InactiveSecurityClassificationHistory';
                    lockUI();
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.securityclassificationinactivelog_data = resp.data.inactivehistory_list;
                        unlockUI();
                    });
                
            }
        }

        $scope.delete = function (securityclassification_gid) {
              var params = {
                  securityclassification_gid: securityclassification_gid
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
                     var url = 'api/MstApplication360/DeleteSecurityClassification';
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

