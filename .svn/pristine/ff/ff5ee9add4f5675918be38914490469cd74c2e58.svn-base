(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstBureauNameController', MstBureauNameController);

        MstBureauNameController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstBureauNameController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstBureauNameController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        $scope.lsoneapihardcodevalue = $location.search().lsoneapihardcodevalue;
        var lsoneapihardcodevalue = $scope.lsoneapihardcodevalue;
        activate();

        function activate() { 

            var url = 'api/MstApplication360/GetBureauName';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.bureauname_data = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addBureauName = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addBureauName.html',
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
                        bureauname_name: $scope.txtbureau_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreateBureauName';
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

        $scope.editBureauName = function (bureauname_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editBureauName.html',
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
                    bureauname_gid: bureauname_gid
                }
                var url = 'api/MstApplication360/EditBureauName';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditbureau_name = resp.data.bureauname_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.bureauname_gid = resp.data.bureauname_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateBureauName';
                    var params = {
                        bureauname_name: $scope.txteditbureau_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        bureauname_gid: $scope.bureauname_gid
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

        $scope.Status_update = function (bureauname_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusBureauName.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    bureauname_gid: bureauname_gid
                }
                var url = 'api/MstApplication360/EditBureauName';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.bureauname_gid = resp.data.bureauname_gid
                    $scope.txtbureau_name = resp.data.bureauname_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        bureauname_gid: bureauname_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveBureauName';
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
                    bureauname_gid: bureauname_gid
                }

                var url = 'api/MstApplication360/InactiveBureauNameHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.bureaunameinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (bureauname_gid) {
              var params = {
                 bureauname_gid: bureauname_gid
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
                              var url = 'api/MstApplication360/DeleteBureauName';
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

