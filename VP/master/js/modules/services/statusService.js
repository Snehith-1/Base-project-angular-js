
(function() {
    'use strict';

    angular
        .module('app.service')
        .service("statusService",statusService);
        statusService.$inject=['$http'];
        function statusService($http){
            this.getStatus = getStatus;
            function getStatus(onReady) {
                var menuJson = 'server/status.json',
              menuURL  = menuJson + '?v=' + (new Date().getTime()); // jumps cache
                $http
                .get(menuURL)
                .success(onReady)
            }
        };
})();