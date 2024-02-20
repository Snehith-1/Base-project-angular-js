(function () {
    'use strict';

    angular
        .module('angle')
        .controller('editSectioncontroller', editSectioncontroller);

    editSectioncontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function editSectioncontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'editSectioncontroller';

        activate();

        function activate() { }
        $scope.sectionbackedit = function () {
            $state.go('app.sectionSummary')
        }
    }
})();
