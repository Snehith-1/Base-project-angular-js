(function () {
    'use strict';

    angular
        .module('angle')
        .controller('servicetypecontroller', servicetypecontroller);

    servicetypecontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function servicetypecontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'servicetypecontroller';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/lglMstServiceType/getservicetype';
            SocketService.get(url).then(function (resp) {
                $scope.servicetype_list = resp.data.servicetype_list;
                $scope.total = $scope.servicetype_list.length;
            });
        }
        document.getElementById('pagecount').onkeyup = function () {
            // console.log(document.getElementById('pagecount').value);
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
            lockUI();
            var Number = parseInt(pagecount);

            $scope.totalDisplayed += Number;
            unlockUI();
        };
        $scope.popupservicetype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addservicetype.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.Submit = function () {
                    var params = {
                        service_type: $scope.txtservicetype,
                        service_code: $scope.txtservicecode
                    }
                    console.log(params);
                    var url = 'api/lglMstServiceType/postservicetype';

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
                        }
                    });

                }
            }
        }

        $scope.edit = function (servicetype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editservicetype.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            $scope.servicetype_gid = servicetype_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    servicetype_gid: servicetype_gid,

                }
                var url = 'api/lglMstServiceType/editservicetype';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtservicecodeedit = resp.data.service_code;
                    $scope.txtservicetypeedit = resp.data.service_type;
                    $scope.servicetype_gid = resp.data.servicetype_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };
                $scope.Update = function () {

                    var params = {
                        service_code: $scope.txtservicecodeedit,
                        service_type: $scope.txtservicetypeedit,
                        servicetype_gid: servicetype_gid,

                    }
                    var url = 'api/lglMstServiceType/updateservicetype';

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
        $scope.delete = function (servicetype_gid) {
            var params = {
                servicetype_gid: servicetype_gid
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
                    var url = 'api/lglMstServiceType/deleteservicetype';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            Notify.alert('Deleted Successfully', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert('Error Occurred !', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };

    }
})();
