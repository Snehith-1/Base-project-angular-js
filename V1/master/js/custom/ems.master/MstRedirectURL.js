(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRedirectURL', MstRedirectURL);

        MstRedirectURL.$inject = ['$rootScope', '$window', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', '$anchorScroll', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstRedirectURL($rootScope, $window, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, $anchorScroll, DownloaddocumentService, cmnfunctionService) {
        var vm = this;
        vm.title = 'MstRedirectURL';
        activate();
        function activate() {
            lockUI();
            $window.location.href = 'https://myapps.microsoft.com/signin/f52aaca8-5635-4041-aa84-e81a804cf3dc?tenantId=655a0e0e-4a74-4a0c-86d8-370a992e90a6';
        }
    }
})();