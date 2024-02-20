(function () {
    'use strict';

    angular
        .module('angle')
        .controller('reloadController', reloadController);

    reloadController.$inject = ['$state', 'ScopeValueService'];

    function reloadController($state, ScopeValueService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'reloadController';

        activate();

        function activate() {

            $state.go(ScopeValueService.get("dataldCtrl").current);
        }
    }
})();
