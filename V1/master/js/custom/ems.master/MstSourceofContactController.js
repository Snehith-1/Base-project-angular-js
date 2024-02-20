(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSourceofContactController', MstSourceofContactController);

    MstSourceofContactController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstSourceofContactController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSourceofContactController';

        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {          
           var url = 'api/MstApplication360/GetSourceofContact';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.sourceofcontact_data = resp.data.application_list;
                unlockUI();
            });
        }
        $scope.addsourceofcontact = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addsource.html',
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
                        sourceofcontact_name: $scope.txtsource_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                   var url = 'api/MstApplication360/CreateSourceofContact'; 
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
        $scope.editsourceofcontact = function (sourceofcontact_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editsource.html',
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
                    sourceofcontact_gid: sourceofcontact_gid
                }
                var url = 'api/MstApplication360/EditSourceofContact';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditsource_name = resp.data.sourceofcontact_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.sourceofcontact_gid = resp.data.sourceofcontact_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateSourceofContact';
                    var params = {
                        sourceofcontact_name: $scope.txteditsource_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        sourceofcontact_gid: $scope.sourceofcontact_gid
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

        $scope.Status_update = function (sourceofcontact_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statussource.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    sourceofcontact_gid: sourceofcontact_gid
                }            
                 var url = 'api/MstApplication360/EditSourceofContact';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sourceofcontact_gid = resp.data.sourceofcontact_gid
                    $scope.txtsource_name = resp.data.sourceofcontact_name;
                    $scope.rbo_status = resp.data.Status;
                });    
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        sourceofcontact_gid:sourceofcontact_gid,
                        remarks: $scope.txtremarks,
                        rbo_status:$scope.rbo_status
                    
                    }
                     var url = 'api/MstApplication360/InactiveSourceofContact'; 
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
                        }activate();
                    }); 

                    $modalInstance.close('closed');

                }
                var params = {
                    sourceofcontact_gid: sourceofcontact_gid
                }

                var url = 'api/MstApplication360/InactiveSourceofcontactHistory';

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sourceofcontactinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                }); 
            }
        }

        $scope.delete = function (sourceofcontact_gid) {
            var params = {
                sourceofcontact_gid: sourceofcontact_gid
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
                            var url = 'api/MstApplication360/DeleteSourceofContact';
                            SocketService.getparams(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                SweetAlert.swal('Deleted Successfully!');
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
                            });
                        }
                    });
        }
    }
})();