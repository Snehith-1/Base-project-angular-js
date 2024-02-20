(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSanctionWaiverSummaryController', MstSanctionWaiverSummaryController);

    MstSanctionWaiverSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstSanctionWaiverSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSanctionWaiverSummaryController';

        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        
        activate();
        lockUI();
        function activate() {
            var url = 'api/MstSanctionWaiver/GetSanctionWaiver';
            SocketService.get(url).then(function (resp) {
                $scope.sanction_list = resp.data.sanctionwaiver;
                unlockUI();
            });
        }

        //Add

        $scope.addsanction = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addsanction.html',
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
                        sanctionwaiver_code: $scope.txtsanctionwaiver_code,
                        sanctionwaiver_name: $scope.txtsanctionwaiver_name,
                        description: $scope.txtdescription
                    }
                    lockUI();
                    var url = 'api/MstSanctionWaiver/PostSanctionWaiver';
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
                        }
                    });
                }
            }
        }


        //Edit

        $scope.editsanction = function (sanctionwaiver_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editsanction.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    sanctionwaiver_gid: sanctionwaiver_gid
                }
                lockUI();
                var url = 'api/MstSanctionWaiver/GetSanctionEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.sanctionwaivercodeedit = resp.data.sanctionwaiver_code;
                    $scope.txteditsanctionwaiver_name = resp.data.sanctionwaiver_name;
                    $scope.txteditdescription = resp.data.description;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    lockUI();
                    var url = 'api/MstSanctionWaiver/UpdateSanctionWaiver';
                    var params = {
                        sanctionwaiver_gid: sanctionwaiver_gid,
                        sanctionwaiver_code: $scope.sanctionwaivercodeedit,
                        sanctionwaiver_name: $scope.txteditsanctionwaiver_name,
                        description: $scope.txteditdescription,
                    }
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
                        }
                    });
                }
            }
        }


        // Showoverpopup

        $scope.description = function (description) {
            var modalInstance = $modal.open({
                templateUrl: '/description.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.description = description;
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
            }
        }


        //Status

        $scope.Status_update = function (sanctionwaiver_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statussanction.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    sanctionwaiver_gid: sanctionwaiver_gid
                }
                lockUI();
                var url = 'api/MstSanctionWaiver/GetSanctionEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.sanctionwaivercodeedit = resp.data.sanctionwaiver_code;
                    $scope.txteditsanctionwaiver_name = resp.data.sanctionwaiver_name;
                    $scope.txteditdescription = resp.data.description;
                    $scope.rbo_status = resp.data.Status;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        sanctionwaiver_gid: sanctionwaiver_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }
                    lockUI();
                    var url = 'api/MstSanctionWaiver/InactiveSanctionWaiver';
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
                        } activate();
                    });

                    $modalInstance.close('closed');
                }
                var params = {
                    sanctionwaiver_gid: sanctionwaiver_gid
                }
                lockUI();
                var url = 'api/MstSanctionWaiver/InactiveSanctionWaiverHistory';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sanctioninactivelog_data = resp.data.sanctioninactivehistory_list;
                    unlockUI();
                });
            }
        }

        //Delete

        $scope.delete = function (sanctionwaiver_gid) {
            var params = {
                sanctionwaiver_gid: sanctionwaiver_gid
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
                    var url = 'api/MstSanctionWaiver/DeleteSanctionWaiver';
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
                        }
                    });
                }
            });
        }
    }
})();