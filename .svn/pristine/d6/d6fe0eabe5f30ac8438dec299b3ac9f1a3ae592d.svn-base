(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnUatSummaryController', sdcTrnUatSummaryController);

    sdcTrnUatSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcTrnUatSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcTrnUatSummaryController';

        activate();

        function activate() {
           
            var url = 'api/SdcTrnUatDeployment/GetUatSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.uatsummary_list = resp.data.uatsummary_list;
                unlockUI();

            });

        }

        $scope.checkall = function (selected) {
          
            angular.forEach($scope.uatsummary_list, function (val) {
                val.checked = selected;
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

        $scope.movetoLive = function (val) {
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to move the file to Live..!',
                //type: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes..!',
                CancelButtonColor: '#DD6B55',
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();


                    var params = {
                        uat_gid: [val],
                    }
                  

                    var url = "api/SdcTrnUatDeployment/GetMovetoLive";
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Done Successfully!');
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            unlockUI();
                        }
                    });

                }

            }

            );
        };

        $scope.movetoLiveAll = function () {
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to move the files to LIVE..!',
                //type: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes..!',
                CancelButtonColor: '#DD6B55',
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();

                    var liveList = [];

                    angular.forEach($scope.uatsummary_list, function (val) {

                        if (val.checked == true) {
                            var uat_gid = val.uat_gid;
                            liveList.push(uat_gid);
                        }
                    });

                    if (liveList.length == 0) {
                        Notify.alert('Select Atleast One Record!');
                        return false;
                        unlockUI();
                    }

                    var params = {
                        uat_gid: liveList,
                    }
                   
                    var url = "api/SdcTrnUatDeployment/GetMovetoLive";
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Done Successfully!');
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            unlockUI();
                        }
                    });
                    unlockUI();
                }


            }

            );
        };

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


        $scope.uatview = function (uat_gid) {
            localStorage.setItem('uat_gid', uat_gid)
            $location.url('app/sdcTrnUatView');
        }

    }
})();
