(function () {
    'use strict';

    angular
        .module('angle')
        .controller('temporaryHandovercontroller', temporaryHandovercontroller);

    temporaryHandovercontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route'];

    function temporaryHandovercontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'temporaryHandovercontroller';

        activate();

        function activate() {
            var url = 'api/temporaryHandover/tmphandover';
            //lockUI();
            SocketService.get(url).then(function (resp) {
                //unlockUI();
                $scope.tmphandoversummary = resp.data.tmphandoversummary;
                $scope.tempHoldersummary = resp.data.tempHoldersummary;
                $scope.tempadminsurrendersummary = resp.data.tempadminsurrendersummary;
                $scope.tempHoldinsassetsummary = resp.data.tempHoldinsassetsummary;
            });
        }

        //  Surrender to IT Admin....//

        $scope.surrenderitadminclick = function (val) {
            var params = { asset_id: val };
            var url = 'api/temporaryHandover/surrenderitadmin';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
                else {
                    Notify.alert('Internal Error Occurred!', {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }


        // Holding Asset....//

        $scope.holdingassetclick = function (val) {
            var params = { asset_id: val };
            var url = 'api/temporaryHandover/holdingasset';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
                else {
                    Notify.alert('Internal Error Occurred!', {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }

        $scope.surrender_submit = function (val1) {
            var params = { asset_id: val1 }
            var url = 'api/temporaryHandover/submittmphandover';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
                else {
                    Notify.alert('Internal Error Occurred!', {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }
    }
})();
