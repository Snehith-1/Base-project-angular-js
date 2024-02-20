(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmMstRiskCategoryController', AtmMstRiskCategoryController);

    AtmMstRiskCategoryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AtmMstRiskCategoryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmMstRiskCategoryController';
        activate();


        function activate() {

            var url = 'api/AtmMstRiskCategory/GetRiskCategory';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.riskcategory_data = resp.data.riskcategory_list;
                unlockUI();
            });
        }

      
        $scope.popupriskcategory = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
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

                $scope.riskcategorySubmit = function () {
                    var params = {
                        riskcategory_name: $scope.txtrisk_category,
                        riskcategory_code: $scope.txtriskcategory_code,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/AtmMstRiskCategory/CreateRiskCategory';
                    lockUI();
                    SocketService.post(url,params).then(function (resp) {
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

        $scope.edit = function (riskcategory_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editriskcategory.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    riskcategory_gid: riskcategory_gid
                }
                var url = 'api/AtmMstRiskCategory/EditRiskCategory';
                SocketService.getparams(url,params).then(function (resp) {
                    $scope.txteditriskcategory_code = resp.data.riskcategory_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditrisk_category = resp.data.riskcategory_name;                  
                    $scope.riskcategory_gid = resp.data.riskcategory_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.riskcategoryUpdate = function () {

                    var url = 'api/AtmMstRiskCategory/UpdateRiskCategory';
                    var params = {
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        riskcategory_code: $scope.txteditriskcategory_code,
                        riskcategory_name: $scope.txteditrisk_category,                      
                        riskcategory_gid: $scope.riskcategory_gid
                    }
                    SocketService.post(url,params).then(function (resp) {
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
                            
                        }
                    });
                    $modalInstance.close('closed');
                }
                
            }
        }

        $scope.Status_update = function (riskcategory_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusriskcategory.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    riskcategory_gid: riskcategory_gid
                }
                var url = 'api/AtmMstRiskCategory/EditRiskCategory';
                SocketService.getparams(url,params).then(function (resp) {
                    $scope.riskcategory_gid = resp.data.riskcategory_gid
                    $scope.txtriskcategory_name = resp.data.riskcategory_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        riskcategory_name: $scope.txtriskcategory_name,
                        riskcategory_gid: $scope.riskcategory_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/AtmMstRiskCategory/InactiveRiskCategory';
                    lockUI();
                    SocketService.post(url,params).then(function (resp) {
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
                    riskcategory_gid: riskcategory_gid
                }

               var url = 'api/AtmMstRiskCategory/RiskCategoryInactiveLogview';
                lockUI();
                SocketService.getparams(url,param).then(function (resp) {
                    $scope.riskcategoryinactivelog_data = resp.data.riskcategory_list;
                    unlockUI();
                });
            }
        }

        //$scope.delete = function (riskcategory_gid) {
        //    var params = {
        //        riskcategory_gid: riskcategory_gid
        //    }
        //    var url = 'api/AtmMstRiskCategory/DeleteRiskCategory'; 
        //    SocketService.getparams(url,params).then(function (resp) {
        //        if (resp.data.status == true) {

        //            SweetAlert.swal({
        //                title: 'Are you sure?',
        //                text: 'Do You Want To Delete the Record ?',
        //                showCancelButton: true,
        //                confirmButtonColor: '#DD6B55',
        //                confirmButtonText: 'Yes, delete it!',
        //                closeOnConfirm: false
        //            }, function (isConfirm) {
        //                if (isConfirm) {
        //                    SweetAlert.swal('Deleted Successfully!');
        //                    unlockUI();
        //                    activate();
        //                }

        //            });
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //            unlockUI();
        //            activate();
        //        }
        //    });
        //};
            $scope.delete = function (riskcategory_gid) {
                var params = {
                    riskcategory_gid: riskcategory_gid
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
                    var url = 'api/AtmMstRiskCategory/DeleteRiskCategory';
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
        };

    }
})();