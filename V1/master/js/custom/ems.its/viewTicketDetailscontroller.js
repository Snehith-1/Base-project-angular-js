(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewTicketDetailscontroller', viewTicketDetailscontroller);

    viewTicketDetailscontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function viewTicketDetailscontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewTicketDetailscontroller';

        activate();

        function activate() {
            var params = {
                complaint_gid: localStorage.getItem('complaint_gid')
            };
            var url = 'api/viewServiceTicket/ticketdetails_view';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.ticket_details = resp.data;
              
            });
            var url = 'api/viewServiceTicket/viewdocument';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.viewDocumentList = resp.data.viewDocumentList;
            });
            //var url = apiManage.apiList['document_tmpclear'].api;
            //SocketService.get(url).then(function (resp) {
            //});
        }
        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }
        $scope.back = function () {
            $state.go('app.viewServiceTicket');
        }
          }
})();
