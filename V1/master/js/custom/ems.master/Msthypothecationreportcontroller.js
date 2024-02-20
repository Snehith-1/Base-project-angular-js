(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MsthypothecationreportController', MsthypothecationreportController);

        MsthypothecationreportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MsthypothecationreportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MsthypothecationreportController';
        activate();

        function activate() {
            var url = 'api/MstApplicationReport/GethypothecationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.hypothecation_list = resp.data.hypothecation_list;
                unlockUI();
            });
            
        }
       

        $scope.exporthypothecation = function () {
            lockUI();
            var url = 'api/MstApplicationReport/ExporthypothecationSummaryReport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !')

                }

            });
        }
    }
})();
