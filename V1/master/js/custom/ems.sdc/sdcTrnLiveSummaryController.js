(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnLiveSummaryController', sdcTrnLiveSummaryController);

    sdcTrnLiveSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcTrnLiveSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcTrnLiveSummaryController';

        activate();

        function activate() {

            var url = 'api/SdcTrnLiveDeployment/GetLiveSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.livesummary_list = resp.data.livesummary_list;
                console.log($scope.livesummary_list);
                unlockUI();

            });

        }


        // Add Code Starts
        $scope.popupmodule = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addModuleContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.activitySubmit = function () {
                    lockUI();
                    var params = {
                        module_name: $scope.module_name,
                        module_prefix: $scope.module_prefix,
                        availability: $scope.availability,
                    }
                    //console.log(params);
                    var url = 'api/SdcMstModule/PostModuleAdd';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                    });
                }

            }
        }
        //Add Code Ends

   
        // Update Deploy Status Code Starts
        $scope.updateDeployStatus = function (test_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/updateDeployStatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    test_gid: test_gid
                }

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
                        test_gid: test_gid,
                        test_status: $scope.statusDeployed,
                        mail_flag: mail_flag,
                    }
                    console.log(params);
                    var url = 'api/SdcTrnTestDeployment/PostDeployStatusUpdate';
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

        $scope.liveview = function (live_gid) {
            localStorage.setItem('live_gid', live_gid)
            $location.url('app/sdcTrnLiveView');
        }

    }
})();
