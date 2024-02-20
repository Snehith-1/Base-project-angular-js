(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptTradeProcessBusinessIndicators', rptTradeProcessBusinessIndicators);

        rptTradeProcessBusinessIndicators.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptTradeProcessBusinessIndicators($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptTradeProcessBusinessIndicators';


        activate();
        
        function fullscreenchanged() 
        {
        
        // if (document.fullscreenElement) {
        // console.log(
        
        // `Element: ${document.fullscreenElement.id} entered fullscreen mode.`
        // );
        
        //  } 
        // else 
        // {
        
        // console.log("Leaving fullscreen mode.");
        
        // }
        
        }
        
        const el = document.getElementById("fullscreen-div");
        
        el.addEventListener("fullscreenchange", fullscreenchanged);
        
        el.onfullscreenchange = fullscreenchanged;
        
        // When the toggle button is clicked, enter/exit fullscreen
        
        document
        
         .getElementById("toggle-fullscreen")
        
         .addEventListener("click", function(event)
          {
        
        if (document.fullscreenElement) {
        
        // exitFullscreen is only available on the Document object.
         document.exitFullscreen();
         }
         else
         { el.requestFullscreen();
         }
        
         });
        


        function activate() {
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: '629663d5-070e-4a25-a02a-c9952fc53911',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'00850e09-0e01-4b6d-b1a4-81fa36086c2d',
                roles:'RM, Manager,Superuser'
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
                page_name: 'TradeProcess Business Indicators',
                page_head: 'AgriCommerce'
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
