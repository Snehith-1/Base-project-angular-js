(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnDeploymentSummaryController', sdcTrnDeploymentSummaryController);

    sdcTrnDeploymentSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcTrnDeploymentSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcMstModuleController';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/SdcMstModule/GetModuleSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.modulemaster_summary = resp.data.moduledtl;
                unlockUI();
            });

            var url = 'api/SdcTrnTestDeployment/GetTestSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.testsummary_list = resp.data.testsummary_list;
                unlockUI();

            });

        }

        $scope.checkall = function (selected) {
           
            angular.forEach($scope.testsummary_list, function (val) {
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

        // Edit Code Starts
        $scope.updateInProgressStatus = function (test_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/updateInprogressStatus.html',
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
                $scope.InprogressStatus = function () {
                    var params = {
                        test_gid: test_gid,
                        test_status: $scope.status,
                    }
                    var url = 'api/SdcTrnTestDeployment/PostStatusUpdate';
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
        // Edit Code Ends


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
                    
                    if ($scope.mail == true)
                    {
                       var mail_flag = 'Y';
                    }
                    else
                    {
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
                            var test_gid = val.test_gid;
                            uatList.push(test_gid);
                            
                        }
                    });
                    var params = {
                        test_gid: uatList,
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
                            var test_gid = val.test_gid;
                            uatList.push(test_gid);
                        }
                    });

                    if (uatList.length == 0)
                    {
                        Notify.alert('Select Atleast One Record!');
                        return false;
                        unlockUI();
                    }
                 
                    var params = {
                        test_gid: uatList,
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
                        test_gid: [val],
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

            );
        };

        // Delete Code Starts
        $scope.delete = function (val) {
            var params = {
                test_gid: val
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
                    var url = "api/SdcTrnTestDeployment/TestDelete";
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

        $scope.addDeployment = function () {

            $state.go('app.sdcTrnAddDeployment');

        }

        // View Code Starts
        $scope.view = function (test_gid) {

            localStorage.setItem('test_gid', test_gid)
            $location.url('app/sdcTrnTestDeploymentView');
        }

        $scope.view = function (test_gid) {

            localStorage.setItem('test_gid', test_gid)
            $location.url('app/sdcTrnTestDeploymentView');
        }
        $scope.testview = function (test_gid) {
            localStorage.setItem('test_gid', test_gid)
            $location.url('app/sdcTrnTestView');
        }
        
    }
})();
