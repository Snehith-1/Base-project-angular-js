(function () {
    'use strict';

    angular
        .module('angle')
        .controller('externalRegistercontroller', externalRegistercontroller);

    externalRegistercontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function externalRegistercontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'externalRegistercontroller';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            lockUI();
            var url = "api/externalVendor/getexternalRegistersummary";
            SocketService.get(url).then(function (resp) {
                $scope.externalVendorList = resp.data.externalvendordtl;
                if ($scope.externalVendorList == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.externalVendorList.length;
                    if ($scope.externalVendorList.length < 100) {
                        $scope.totalDisplayed = $scope.externalVendorList.length;
                    }
                }
                unlockUI();
            });
        }

        document.getElementById('pagecount').onkeyup = function () {

            if ($scope.pagecount == null) {
                var el = document.getElementById('loadmore');
                el.style.backgroundColor = '#DCDCDC';
            }
            else {
                var el = document.getElementById('loadmore');
                el.style.backgroundColor = '#ffa';
            }
        };

        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);
            $scope.totalDisplayed += Number;
            unlockUI();
        };

        $scope.externalRegister = function () {
            $state.go('app.externalRegisterAdd');
        }

        $scope.viewExternalRegister = function (externalregister_gid) {
            localStorage.setItem('externalregister_gid', externalregister_gid);
            $state.go('app.externalRegisterView');
        }

        $scope.editExternalRegister = function (externalregister_gid) {
            localStorage.setItem('externalregister_gid', externalregister_gid);
            $state.go('app.externalRegisterEdit');
        }

        $scope.logincreation = function (externalregister_gid) {

            var params = {
                externalregister_gid: externalregister_gid
            }


            var modalInstance = $modal.open({
                templateUrl: '/lawyerLoginContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.close = function () {
                    $modalInstance.close('closed');
                }

                var url = 'api/externalVendor/getexternallogindtl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.external_Vendorname = resp.data.external_Vendorname;
                    $scope.external_vendorCode = resp.data.external_vendorCode;
                });

                $scope.sendaccouncreate = function () {
                    var params = {
                        external_vendorCode: $scope.external_vendorCode,
                        external_vendorPassword: $scope.external_vendorpassword,
                        externalregister_gid: externalregister_gid
                    }
                    var url = "api/externalVendor/postExternalLogin";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        console.log(resp.data.status);
                        if (resp.data.status == true) {

                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }

                    });
                }
            }
        }


        $scope.viewlogin = function (externalregister_gid) {

            var params = {
                externalregister_gid: externalregister_gid
            }


            var modalInstance = $modal.open({
                templateUrl: '/ViewlawyerLoginContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.close = function () {
                    $modalInstance.close('closed');
                }

                var url = 'api/externalVendor/getexternallogindtl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.external_Vendorname = resp.data.external_Vendorname;
                    $scope.external_vendorCode = resp.data.external_vendorCode;
                    $scope.external_vendorPassword = resp.data.external_vendorPassword;
                    $scope.loginuserstatus = resp.data.external_activeStatus;
                });

                $scope.sendaccounstatus = function () {
                    var params = {
                        external_activeStatus: $scope.loginuserstatus,
                        externalregister_gid: externalregister_gid
                    }
                    var url = "api/externalVendor/postExternalLoginStatus";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            activate();
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }

                    });
                }
            }
        }
    }
})();
