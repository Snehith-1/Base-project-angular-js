(function () {
    'use strict';

    angular
        .module('angle')
        .controller('geo', geo);

    geo.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies'];
    function geo($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies) {
        var vm = this;
        activate();
        function activate() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    console.log(position.coords.latitude, position.coords.longitude);
                    var map, marker;
                    var param = {
                        lat: position.coords.latitude,
                        lon: position.coords.longitude
                    };
                    var url = 'api/geoMap/locate';
                    SocketService.post(url, param).then(function (resp) {
                        $scope.data = resp.data;
                        $scope.show = true;
                        $cookies.putObject('location', resp.data.freeformAddress);
                    });
                    //Initialize a map instance.
                    map = new atlas.Map('map', {
                        center: [position.coords.longitude, position.coords.latitude],
                        zoom:7,
                        view: 'Auto',
                        authOptions: {
                            authType: 'subscriptionKey',
                            subscriptionKey: 'uYsh9CG084Em15TPYkTRUtaBHu0VPYijY4I0JNG-M5M'
                        }
                    });
                    //Wait until the map resources are ready.
                    map.events.add('ready', function () {
                        //Create a HTML marker and add it to the map.
                        marker = new atlas.HtmlMarker({
                            htmlContent: '<div class="pulseIcon"></div>',
                            position: [position.coords.longitude, position.coords.latitude]
                        });

                        map.markers.add(marker);
                    });
                });
            }
        };
    }
})();