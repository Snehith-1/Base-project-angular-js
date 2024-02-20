
'use strict'
angular
    .module('angle')
    .factory('ScopeValueService', ['$rootScope', function ($rootScope) {
        var mem = {};

        return {
            store: function (key, value) {
                $rootScope.$emit('scope.stored', key);
                mem[key] = value;
            },
            get: function (key) {
                return mem[key];
            }
        };
    }]);
