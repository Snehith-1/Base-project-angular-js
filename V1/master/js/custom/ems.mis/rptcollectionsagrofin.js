(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptcollectionsagrofin', rptcollectionsagrofin);

        rptcollectionsagrofin.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptcollectionsagrofin($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptcollectionsagrofin';


        activate();

        function activate() {
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: 'ea41ad60-b34b-411a-919c-33e8a682942a',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'574fbdf2-f2c1-44fe-8ed5-c4bfda154161',
                roles:'RM'
            }

            SocketService.getparams(url, param).then(function (resp) { 
                var response = JSON.parse(resp.data);
                console.log(resp.data);
                console.log(response.EmbedReports[0].EmbedUrl);
                console.log(response.EmbedToken.token);
                
                
                var embedConfiguration = {

                    type: 'report',
                    tokenType: models.TokenType.Embed,
                    accessToken: response.EmbedToken.token,
                    embedUrl: response.EmbedReports[0].EmbedUrl,
                    id: response.EmbedReports[0].ReportId,
                    permissions: models.Permissions.All,
                    settings: {
                        // Enable this setting to remove gray shoulders from embedded report
                        // background: models.BackgroundType.Transparent,
                        filterPaneEnabled: true,
                        navContentPaneEnabled: true
                    }
               
                }; 

                var $reportContainer = $('#dashboardContainer');

                var report = powerbi.embed($reportContainer.get(0), embedConfiguration);

                var element = $('#dashboardContainer');
 
                var report = powerbi.get(element);
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'Agro & Fin - Collections',
                page_head: 'Risk & Collections'
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });


        };
    };
})();
