(function () {
    'use strict';

    angular
        .module('app.sidebar')
        .controller('UserBlockController', UserBlockController);

    UserBlockController.$inject = ['$rootScope', 'SocketService','$scope'];
    function UserBlockController($rootScope, SocketService, $scope) {

        activate();

        ////////////////

        function activate() {
           
            var params = {
                externaluser_gid: localStorage.getItem('externaluser_gid')
            }
            
            var url = "api/epwelcome/GetExternaldetails";
            SocketService.getparams(url, params).then(function (resp) {
                
                if (resp.data.external_photo != "N") {
                    var pathArray = location.href.split('/');
                    var protocol = pathArray[0];
                    var host = pathArray[2];
                    var url = protocol + '//' + host;
                    var str = resp.data.external_photo;
                    var str = str.split("StoryboardAPI");
                    var relpath1 = str[1].replace("\\", "/");
                    $scope.external_photo = url.concat(relpath1);
                    console.log($scope.external_photo);

                    $rootScope.user = {
                        name: resp.data.externaluser_code,
                        job: resp.data.externaluser_name,
                        picture: $scope.external_photo
                    };
                }
                else {
                    $rootScope.user = {
                        name: resp.data.externaluser_code,
                        job: resp.data.externaluser_name,
                        picture: 'app/img/user/lawyer.jfif'
                    };
                }
               
            });

            // Hides/show user avatar on sidebar
            $rootScope.toggleUserBlock = function () {
                $rootScope.$broadcast('toggleUserBlock');
            };

            $rootScope.userBlockVisible = false;

            $rootScope.$on('toggleUserBlock', function (/*event, args*/) {

                $rootScope.userBlockVisible = !$rootScope.userBlockVisible;

            });
        }
    }
})();
