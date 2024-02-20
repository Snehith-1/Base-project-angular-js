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
                lawyerregister_gid: localStorage.getItem('lawyeruser_gid')
            }
            
            var url = "api/welcome/Getlawyerdetails";
            SocketService.getparams(url, params).then(function (resp) {
                console.log(resp);
                if (resp.data.lawyer_photo != "N") {
                    var pathArray = location.href.split('/');
                    var protocol = pathArray[0];
                    var host = pathArray[2];
                    var url = protocol + '//' + host;
                    var str = resp.data.lawyer_photo;
                    str = str.substring(str.indexOf("EMS/") + 3);
                    $scope.lawyer_photo = url.concat(str);

                    $rootScope.user = {
                        name: resp.data.lawyer_name,
                        job: resp.data.educational_qualification,
                        picture: $scope.lawyer_photo
                    };
                }
                else {
                    $rootScope.user = {
                        name: resp.data.lawyer_name,
                        job: resp.data.educational_qualification,
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
