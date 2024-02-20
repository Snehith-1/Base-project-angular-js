(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnLiveDeploymentSummaryController', sdcTrnLiveDeploymentSummaryController);

    sdcTrnLiveDeploymentSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcTrnLiveDeploymentSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcTrnLiveDeploymentSummaryController';

        activate();

        function activate() {

            var url = 'api/SdcTrnLiveDeployment/GetLiveSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.livesummary_list = resp.data.livesummary_list;
                unlockUI();

            });

        }


        // Update Code Starts
        $scope.updateInProgressStatus = function (live_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/updateInprogressStatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
              
                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.cancel = function () {
                    modalInstance.close('closed');
                };
                $scope.InprogressStatus = function () {
                    var params = {
                        live_gid: live_gid,
                        live_status: $scope.status,
                    }
                    var url = 'api/SdcTrnLiveDeployment/PostStatusUpdate';
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

                            alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }
        // Update Code Ends


        // Update Deploy Status Code Starts
        $scope.updateDeployStatus = function (live_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/updateDeployStatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
               
                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.cancel = function () {
                    modalInstance.close('closed');
                };
                $scope.deployStatus = function () {

                    if ($scope.mail == true) {
                        var mail_flag = 'Y';
                    }
                    else {
                        var mail_flag = 'N';
                    }
                    var params = {
                        live_gid: live_gid,
                        live_status: $scope.statusDeployed,
                        mail_flag: mail_flag,
                    }
                    console.log(params);
                    var url = 'api/SdcTrnLiveDeployment/PostDeployStatusUpdate';
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

                            alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }


        $scope.viewLive = function (live_gid) {

            localStorage.setItem('live_gid', live_gid)
            $location.url('app/sdcTrnLiveDeploymentView');
        }
    }
})();
