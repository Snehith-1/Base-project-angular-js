(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasMsthistorysanctionrefno', idasMsthistorysanctionrefno);

    idasMsthistorysanctionrefno.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function idasMsthistorysanctionrefno($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        $scope.title = 'idasMsthistorysanctionrefno';

        activate();

        function activate() {
            $scope.customer2sanction_gid = localStorage.getItem('customer2sanction_gid');
          
            lockUI();
           
            var url = 'api/IdasMstSanction/historysanctionref_no';
            var params = {
                customer2sanction_gid: $scope.customer2sanction_gid
            };
         
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblcustomer_name = resp.data.customer_name;
                $scope.lblcustomer_urn = resp.data.customer_urn;
                $scope.historySanctionDtlsEdit = resp.data.historySanctionDtlsEdit;
                console.log(resp.data.historySanctionDtlsEdit);

            });
           

        }
        $scope.back = function (relpath1)
        {
            $state.go('app.IdasMstSanctionReset');
        }
    }
})();
