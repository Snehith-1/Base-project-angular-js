(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnNocAndNdcController', idasTrnNocAndNdcController);

    idasTrnNocAndNdcController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function idasTrnNocAndNdcController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasTrnNocAndNdcController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/IdasNocAndNdc/GetIdasNocAndNdc';
            SocketService.get(url).then(function (resp) {
                $scope.nocandndc_list = resp.data.nocandndc_list;

                unlockUI();
            });  
        }

        $scope.addnocndc = function () {
            $location.url('app/idasTrnNocAndNdcAdd');
        }

        $scope.editnocndc = function (nocandndc_gid) {
            $location.url('app/idasTrnNocAndNdcEdit?lsnocandndc_gid=' + nocandndc_gid);
        }

        $scope.viewnocndc = function (nocandndc_gid) {
            $location.url('app/idasTrnNocAndNdcView?lsnocandndc_gid=' + nocandndc_gid);
        }

  

        $scope.delete = function (nocandndc_gid) {
            var params = {
                nocandndc_gid: nocandndc_gid
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
                    var url = 'api/IdasNocAndNdc/NocandNdcDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true)
                        {
                            activate();
                        }
                        else
                        {
                            Notify.alert('Error Occurred While Deleting NOC & NDC!', {
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

