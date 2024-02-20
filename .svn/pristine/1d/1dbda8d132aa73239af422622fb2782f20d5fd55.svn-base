(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstPurposecolumncontroller', MstPurposecolumncontroller);

    MstPurposecolumncontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function MstPurposecolumncontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstPurposecolumncontroller';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/MstApplication360/GetPurposecolumn';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.purposecolumn_list = resp.data.application_list;
                unlockUI();
            });

        }



        //<!--ADD CODE START-->
        $scope.popupPurposecolumn = function () {
            var modalInstance = $modal.open({
                templateUrl: '/purposecolumnadd.html',
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

                $scope.PurposecolumnSubmit = function () {
                    var params = {
                        Purposecolumn_name: $scope.txtaddPurposecolumn_name,
                        bureau_code: $scope.txtbureau_code,
                        lms_code: $scope.txtlms_code
                    }
                    
                    var url = 'api/MstApplication360/CreatePurposecolumn';
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


        $scope.editPurposecolumn = function (purposecolumn_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editPurposecolumn.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    purposecolumn_gid: purposecolumn_gid

                }
                var url = 'api/MstApplication360/EditPurposecolumn';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditPurposecolumn_name = resp.data.purposecolumn_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.purposecolumn_gid = resp.data.purposecolumn_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdatePurposecolumn';
                    var params = {
                        purposecolumn_name: $scope.txteditPurposecolumn_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        purposecolumn_gid: $scope.purposecolumn_gid
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


        $scope.Status_update = function (purposecolumn_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusPurposecolumn.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    purposecolumn_gid: purposecolumn_gid
                }
                var url = 'api/MstApplication360/EditPurposecolumn';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.purposecolumn_gid = resp.data.purposecolumn_gid;
                    $scope.txtpurposecolumn_name = resp.data.purposecolumn_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        purposecolumn_gid: purposecolumn_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactivePurposecolumn';
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
                    });

                    $modalInstance.close('closed');

                }

                var param = {
                    purposecolumn_gid: purposecolumn_gid
                }

                var url = 'api/MstApplication360/PurposecolumnInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.purposecolumninactivelog_list = resp.data.application_list;
                    unlockUI();
                });

            }
        }
        //<!--STATUS CODE END-->

        //<!--DELETE CODE START-->

        $scope.delete = function (purposecolumn_gid) {
            var params = {
                purposecolumn_gid: purposecolumn_gid
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
                    var url = 'api/MstApplication360/DeletePurposecolumn';
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
        //<!--DELETE CODE END-->



    }
})();
