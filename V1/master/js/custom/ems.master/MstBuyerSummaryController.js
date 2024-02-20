(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstBuyerSummaryController', MstBuyerSummaryController);

    MstBuyerSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstBuyerSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstBuyerSummaryController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
             var url = 'api/Mstbuyer/GetbuyerSummary';
               SocketService.get(url).then(function (resp) {
                $scope.buyer_list = resp.data.buyer_list;
            }); 
        }

        $scope.addbuyer = function () {
            $state.go('app.MstBuyerAdd');
        }

        $scope.editbuyer = function (val) {
            localStorage.setItem('buyer_gid', val);
            $state.go('app.MstBuyerEdit');
        }

        $scope.viewbuyer = function (val) {
            localStorage.setItem('buyer_gid', val);
            $state.go('app.MstBuyerView');
        }

        $scope.delete = function (buyer_gid) {
             var params = {
                buyer_gid: buyer_gid
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
                    var url = 'api/Mstbuyer/Deletebuyer';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Buyer!', {
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

