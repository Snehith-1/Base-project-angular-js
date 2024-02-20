(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewAuthorcontroller', viewAuthorcontroller);

    viewAuthorcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function viewAuthorcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewAuthorcontroller';

        activate();

        function activate() {
            var params = {
                author_gid: localStorage.getItem('author_gid')
            };
            var url = 'api/author/authorUpdatedetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.author_details = resp.data;
            });
        }
        $scope.back = function () {
            $state.go('app.authorSummary');
        }
    }
})();
