(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customer2EscrowView', customer2EscrowView);

    customer2EscrowView.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams'];


    function customer2EscrowView($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams) {

        $scope.title = 'customer2EscrowView';
        var escrow_gid;
        activate();

        function activate() {
            escrow_gid = localStorage.getItem("escrow_gid", escrow_gid);
            console.log(escrow_gid);
            var params = {
                escrow_gid: escrow_gid
            };
            var url = "api/customerManagement/EscrowView";
            SocketService.getparams(url, params).then(function (resp) {
                if(resp.data.status==true)
                {
                    $scope.escrowview = resp.data;
                   
                }
                

            });
        }

        $scope.back=function()
        {
            $state.go('app.Customer2EscrowSummary');
        }
    }
})();
