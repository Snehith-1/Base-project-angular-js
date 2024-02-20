(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewSectioncontroller', viewSectioncontroller);

    viewSectioncontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function viewSectioncontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewSectioncontroller';

        activate();

        function activate() { }
        $scope.viewsectionback = function () {
            $state.go('app.sectionsummary')
        }
    }
})();
