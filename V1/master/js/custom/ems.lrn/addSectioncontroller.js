(function () {
    'use strict';

    angular
        .module('angle')
        .controller('addSectioncontroller', addSectioncontroller);

    addSectioncontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function addSectioncontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'addSectioncontroller';

        activate();

        function activate() { }
        $scope.sectionback = function () {
            $state.go('app.sectionsummary')
        }
    }
})();
