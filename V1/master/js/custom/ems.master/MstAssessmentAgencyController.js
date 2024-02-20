(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAssessmentAgencyController', MstAssessmentAgencyController);

        MstAssessmentAgencyController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstAssessmentAgencyController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAssessmentAgencyController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
            var url = 'api/MstApplication360/GetAssessmentAgency';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.assessmentagency_data = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addassessment = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addassessment.html',
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
                        assessmentagency_name: $scope.txtassessmentagency_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreateAssessmentAgency';
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
                }
                
            }
        }

        $scope.editassessmentagency = function (assessmentagency_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editassessment.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    assessmentagency_gid: assessmentagency_gid
                }
                var url = 'api/MstApplication360/EditAssessmentAgency';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditassessmentagency_name = resp.data.assessmentagency_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.assessmentagency_gid = resp.data.assessmentagency_gid;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                                              
                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateAssessmentAgency';
                    var params = {
                        assessmentagency_name: $scope.txteditassessmentagency_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        assessmentagency_gid: $scope.assessmentagency_gid
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

        $scope.Status_update = function (assessmentagency_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusassessment.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    assessmentagency_gid: assessmentagency_gid
                }              
                var url = 'api/MstApplication360/EditAssessmentAgency';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.assessmentagency_gid = resp.data.assessmentagency_gid
                    $scope.txtassessmentagency_name = resp.data.assessmentagency_name;
                    $scope.rbo_status = resp.data.Status;
                });
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        assessmentagency_gid: $scope.assessmentagency_gid,
                        assessmentagency_name: $scope.txtassessmentagency_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    
                    }
                    var url = 'api/MstApplication360/InactiveAssessmentAgency';
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
                    assessmentagency_gid: assessmentagency_gid
                }

                var url = 'api/MstApplication360/AssessmentAgencyInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.assessmentagencyinactivelog_list = resp.data.application_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (assessmentagency_gid) {
            var params = {
                assessmentagency_gid: assessmentagency_gid
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
                            var url = 'api/MstApplication360/DeleteAssessmentAgency';
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

