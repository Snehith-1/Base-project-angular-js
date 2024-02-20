(function () {
    'use strict';

    angular
        .module('angle')
        .controller('collateralSummaryController', collateralSummaryController);

    collateralSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function collateralSummaryController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'collateralSummaryController';

        activate();

        function activate() {
            var url = 'api/collateral/getCollateralSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.collateral_data = resp.data.customercollateral_list;
            });
        }
        $scope.popupcollateral=function(){
            $state.go('app.collateral');
        }
        $scope.delete = function (collateral_gid) {
            var params = {
                collateral_gid: collateral_gid
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
                    var url = 'api/collateral/collateralDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                           
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
        $scope.edit = function (collateral_gid) {
            $scope.collateral_gid = localStorage.setItem('collateral_gid', collateral_gid);
          
            $state.go('app.editCollateral');
        }
    }
})();
