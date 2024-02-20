(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnUatDeploymentSummaryController', sdcTrnUatDeploymentSummaryController);

    sdcTrnUatDeploymentSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcTrnUatDeploymentSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcTrnUatDeploymentSummaryController';

        activate();

        function activate() {
          
            var url = 'api/SdcTrnUatDeployment/GetUatSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.uatsummary_list = resp.data.uatsummary_list;
                unlockUI();

            });

        }

      
        // Update Code Starts
        $scope.updateInProgressStatus = function (uat_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/updateInprogressStatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    uat_gid: uat_gid
                }

                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.cancel = function () {
                    modalInstance.close('closed');
                };
                $scope.InprogressStatus = function () {
                    var params = {
                        uat_gid: uat_gid,
                        uat_status: $scope.status,
                    }
                    var url = 'api/SdcTrnUatDeployment/PostStatusUpdate';
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
        $scope.updateDeployStatus = function (uat_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/updateDeployStatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    uat_gid: uat_gid
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
                        uat_gid: uat_gid,
                        uat_status: $scope.statusDeployed,
                        mail_flag: mail_flag,
                    }
                    console.log(params);
                    var url = 'api/SdcTrnUatDeployment/PostDeployStatusUpdate';
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






        $scope.demo1 = function () {



            var modalInstance = $modal.open({
                templateUrl: '/updateUatStatus.html',
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



                $scope.TestStatus = function () {
                    angular.forEach($scope.testsummary_list, function (val) {

                        if (val.checked == true) {
                            var uat_gid = val.uat_gid;
                            uatList.push(uat_gid);
                            console.log(uatList);
                        }
                    });
                    var params = {
                        uat_gid: uatList,
                        //module_gid: localStorage.getItem('module_gid')
                        //productgroup_gid: $scope.productgroup_gid;
                    }

                    var url = "api/SdcTrnTestDeployment/GetMovetoUat";
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

        };






        $scope.movetoUATAll = function () {
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to move the files to UAT..!',
                //type: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes..!',
                CancelButtonColor: '#DD6B55',
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();

                    var uatList = [];

                    angular.forEach($scope.testsummary_list, function (val) {

                        if (val.checked == true) {
                            var uat_gid = val.uat_gid;
                            uatList.push(uat_gid);
                        }
                    });

                    if (uatList.length == 0) {
                        Notify.alert('Select Atleast One Record!');
                        return false;
                        unlockUI();
                    }

                    var params = {
                        uat_gid: uatList,
                    }
                    console.log(params);
                    var url = "api/SdcTrnTestDeployment/GetMovetoUat";
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


        $scope.movetoUAT = function (val) {
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do you want to move the file to UAT..!',
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
                    console.log(params);

                    var url = "api/SdcTrnTestDeployment/GetMovetoUat";
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

        // Delete Code Starts
        $scope.delete = function (val) {
            var params = {
                module_gid: val
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
                    var url = "api/SdcMstModule/GetModuleDelete";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
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

            });
        }
        // Delete Code End

        $scope.viewUat = function (uat_gid) {

            localStorage.setItem('uat_gid', uat_gid)
            $location.url('app/sdcTrnUatDeploymentView');
        }
    }
})();
