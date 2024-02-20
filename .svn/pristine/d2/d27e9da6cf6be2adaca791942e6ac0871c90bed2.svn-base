(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstPartyTypeController', MstPartyTypeController);

        MstPartyTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstPartyTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstPartyTypeController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
           
            var url = 'api/MstApplication360/GetPartyType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.partytype_list = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addPartyType = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addPartyType.html',
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
                        partytype_name: $scope.txtParty_Type,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreatePartyType';
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

        $scope.editPartyType = function (partytype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editPartyType.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    partytype_gid: partytype_gid
                }
                var url = 'api/MstApplication360/EditPartyType';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditParty_Type = resp.data.partytype_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.partytype_gid = resp.data.partytype_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                                              
                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdatePartyType';
                    var params = {
                        partytype_name: $scope.txteditParty_Type,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        partytype_gid: $scope.partytype_gid
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

        $scope.Status_update = function (partytype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusPartyType.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                                
                var params = {
                    partytype_gid: partytype_gid
                }
                var url = 'api/MstApplication360/EditPartyType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.partytype_gid = resp.data.partytype_gid
                    $scope.txtpartytype_name = resp.data.partytype_name;
                    $scope.rbo_status = resp.data.Status;
                });
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        partytype_name: $scope.txtpartytype_name,
                        partytype_gid: $scope.partytype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    
                    }
                    var url = 'api/MstApplication360/InactivePartyType';
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
                    partytype_gid: partytype_gid
                }

                var url = 'api/MstApplication360/InactivePartyTypeHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.partytypeinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (partytype_gid) {
            var params = {
                partytype_gid: partytype_gid
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
                    var url = 'api/MstApplication360/DeletePartyType';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Party Type!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    }); 
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
    }
})();

