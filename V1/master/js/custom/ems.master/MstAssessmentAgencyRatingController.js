(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAssessmentAgencyRatingController', MstAssessmentAgencyRatingController);

    MstAssessmentAgencyRatingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstAssessmentAgencyRatingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAssessmentAgencyRatingController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            var url = 'api/MstApplication360/GetAssessmentAgencyRating';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.assessmentagencyrating_data = resp.data.application_list;
                unlockUI();
            });
        }


        $scope.addassessmentagencyrating = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addassessmentagencyrating.html',
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
                        assessmentagencyrating_name: $scope.txtassessment_agency_rating,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreateAssessmentAgencyRating';
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

        $scope.editassessmentagencyrating = function (assessmentagencyrating_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editassessmentagencyrating.html',
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
                    assessmentagencyrating_gid: assessmentagencyrating_gid
                }
                var url = 'api/MstApplication360/EditAssessmentAgencyRating';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditassessment_agency_rating = resp.data.assessmentagencyrating_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.assessmentagencyrating_gid = resp.data.assessmentagencyrating_gid;
                });
               
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateAssessmentAgencyRating';
                    var params = {
                        assessmentagencyrating_name: $scope.txteditassessment_agency_rating,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        assessmentagencyrating_gid: $scope.assessmentagencyrating_gid
                    }
                    console.log(params)
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

        $scope.Status_update = function (assessmentagencyrating_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusassessmentagencyrating.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    assessmentagencyrating_gid: assessmentagencyrating_gid
                }
                var url = 'api/MstApplication360/EditAssessmentAgencyRating';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.assessmentagencyrating_gid = resp.data.assessmentagencyrating_gid
                    $scope.txtassessment_agency_rating = resp.data.assessmentagencyrating_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        assessmentagencyrating_gid: assessmentagencyrating_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveAssessmentAgencyRating';
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
                    assessmentagencyrating_gid: assessmentagencyrating_gid
                }

                var url = 'api/MstApplication360/InactiveAssessmentAgencyRatingHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.assessmentagencyratinginactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (assessmentagencyrating_gid) {
            var params = {
                assessmentagencyrating_gid: assessmentagencyrating_gid
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
                            var url = 'api/MstApplication360/DeleteAssessmentAgencyRating';
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

