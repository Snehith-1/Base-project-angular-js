(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCustomertmpView', MstCustomertmpView);

    MstCustomertmpView.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function MstCustomertmpView($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCustomertmpView';
        activate();

        function activate() {
            var params = {
                customer_gid: localStorage.getItem('customer_gid')
            }
            var url = 'api/MstCustomertmp/GetViewCustomer2UserDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_urn = resp.data.customer_urn;
                $scope.vertical = resp.data.vertical;
                $scope.zonal_head = resp.data.zonal_head;
                $scope.business_head = resp.data.business_head;
                $scope.rm_name = resp.data.rm_name;
                $scope.cluster_manager = resp.data.cluster_manager;
                $scope.credit_manager = resp.data.credit_manager;
                $scope.constitution = resp.data.constitution;
                $scope.sa_payout = resp.data.sa_payout;
                $scope.sa_idname = resp.data.sa_idname;
                $scope.secondaryvalue_chain = resp.data.secondaryvalue_chain;
                $scope.primaryvalue_chain = resp.data.primaryvalue_chain;
                $scope.sa_status = resp.data.sa_status;
                $scope.business_unit = resp.data.business_unit;
                $scope.customername = resp.data.customername;
                $scope.customer2userdtl_list = resp.data.customer2userdtl_list;
            });

        }
        $scope.back = function () {
            $state.go('app.MstCustomerSummary');
        }
        $scope.viewuserdtl = function (customer2usertype_gid) {
            $scope.customer2usertype_gid = customer2usertype_gid;
            $scope.customer2usertype_gid = localStorage.setItem('customer2usertype_gid', customer2usertype_gid);
            console.log(localStorage.setItem('customer2usertype_gid', customer2usertype_gid));
            $location.url('app/MstCustomer2tmpuserView?lsredirect=view');
        }
    }
})();
