(function () {
    'use strict';

    angular
        .module('angle')
        .controller('requesttypecontroller', requesttypecontroller);

    requesttypecontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function requesttypecontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'requesttypecontroller';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/requestCompliance/getrequesttype';
            SocketService.get(url).then(function (resp) {
                $scope.requesttype_list = resp.data.requesttype_list;
                $scope.total = $scope.requesttype_list.length;
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
        $scope.popuprequesttype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addrequesttype.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.requestSubmit = function () {
                    var params = {
                        request_type: $scope.txtrequesttype,
                        request_code: $scope.txtrequestcode
                    }
                    console.log(params);
                    var url = 'api/requestCompliance/postrequesttype';

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

        $scope.edit = function (requesttype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editrequesttype.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
             $scope.requesttype_gid = requesttype_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    requesttype_gid: requesttype_gid,
                    
                }
                var url = 'api/requestCompliance/editrequesttype';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtrequestcodeedit = resp.data.request_code;
                    $scope.txtrequesttypeedit = resp.data.request_type;
                    $scope.requesttype_gid = resp.data.requesttype_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };
                $scope.requestUpdate = function () {

                    var params = {
                        request_code: $scope.txtrequestcodeedit,
                        request_type: $scope.txtrequesttypeedit,
                        requesttype_gid: requesttype_gid,

                    }
                    var url = 'api/requestCompliance/updaterequesttype';

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
        $scope.delete = function (requesttype_gid) {
            var params = {
                requesttype_gid: requesttype_gid
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
                    var url = 'api/requestCompliance/deleterequesttype';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred !', {
                                status: 'warning',
                                pos: 'top-right',
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
