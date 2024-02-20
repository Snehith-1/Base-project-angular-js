(function () {
    'use strict';

    angular
        .module('vcx')
        .controller('responselog', responselog);

    responselog.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies'];

    function responselog($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'responselog';

        activate();

        function activate() {
          var val=  localStorage.getItem('issue_id');
            var params = {
                issue_gid: val
            };
            var url = apiManage.apiList["issuechat"].api;
            SocketService.getparams(url, params).then(function (resp) {
                $scope.chatdtl = resp.data.datelist;
                console.log('');
            });
        }
    }
})();
