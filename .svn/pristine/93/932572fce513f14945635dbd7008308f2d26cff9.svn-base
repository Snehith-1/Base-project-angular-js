(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptBucketWiseOverdueDetails', rptBucketWiseOverdueDetails);

        rptBucketWiseOverdueDetails.$inject =['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptBucketWiseOverdueDetails($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptBucketWiseOverdueDetails';


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
                report_id: '2aefde48-cf84-455e-9ef9-cd2fbc3ed2fe',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'080f6526-ed91-4059-986a-ef4b5053d851',
                roles:'RM,Manager,Superuser'
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
                page_name: 'BucketWise Overdue Details',
                page_head: 'Agri Finance'
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
