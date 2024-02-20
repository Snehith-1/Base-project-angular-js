(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAccountOverview', rptAccountOverview);

        rptAccountOverview.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAccountOverview ($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAccountOverview';


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
            lockUI();
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: '77d4cfe4-8a33-4f04-928b-530b4aff2ee1',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'254e1541-023d-499d-b697-15deed56fbc9',
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
                unlockUI();
                var report = powerbi.get(element);
                
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'Account Overview',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAgriCommerce', rptAgriCommerce);

        rptAgriCommerce.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAgriCommerce($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAgriCommerce';



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
                report_id: '0d5c8a0b-915c-4153-817a-55f90949302b',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'50c2514f-3178-4fb9-b1c1-87732fc01a64',
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
                page_name: 'Portfolio Profile ',
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAgriCommerceConsolidated', rptAgriCommerceConsolidated);

        rptAgriCommerceConsolidated.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAgriCommerceConsolidated($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAgriCommerceConsolidated';


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
                report_id: '85240615-ee17-406f-80e4-2bc9a6e72244',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'73b8b08e-2ee2-4aa4-a758-0c2c45509ece',
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
                page_name: 'AgriCommerce Consolidated',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAgricommercecontest', rptAgricommercecontest);

        rptAgricommercecontest.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAgricommercecontest($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAgricommercecontest';


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
                report_id: '6e241cd6-a474-44dd-a5bb-264557aed0f9',
                workspace_Id :'364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'2b6588de-2652-4fd0-ac92-10a702c1cbde',
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
                page_name: 'AgriCommerce Contest',
                page_head: 'FY23 Contest'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAgricommercecontroller', rptAgricommercecontroller);

        rptAgricommercecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAgricommercecontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAgricommercecontroller';


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
                report_id: '6e241cd6-a474-44dd-a5bb-264557aed0f9',
                workspace_Id :'364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'2b6588de-2652-4fd0-ac92-10a702c1cbde',
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
                page_name: 'Agri Commerce',
                page_head: 'FY23 Contest'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAgriCommerceSummary', rptAgriCommerceSummary);

        rptAgriCommerceSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAgriCommerceSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAgriCommerceSummary';


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
                report_id: '80a4cd34-db7b-4072-b8b0-780e83b68e41',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'4d995410-ace6-423c-a220-c97baac488ad',
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
                page_name: 'AgriCommerceSummary',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAgriEnterprisesPerformance', rptAgriEnterprisesPerformance);

        rptAgriEnterprisesPerformance.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAgriEnterprisesPerformance($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAgriEnterprisesPerformance';


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
                report_id: 'cf8cffc5-d8f4-44d6-9ea1-d6a119f85c07',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'d38763a5-d1e6-4224-be1e-92279cd73fe2',
                roles:'RM,Superuser'
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
                page_name: 'RM Scorecard',
                page_head: 'Agri Enterprises'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAgriFinanceAOP', rptAgriFinanceAOP);

        rptAgriFinanceAOP.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAgriFinanceAOP($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAgriFinanceAOP';


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
                report_id:'358e4a85-6bcb-4615-a655-62b6ad2fce1f',
                workspace_Id :'364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'15369a63-9299-4ee9-ba99-b535c0b73034',
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
                page_name: 'AgriFinance AOP',
                page_head: 'AgriFinance'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAgriFinanceContest', rptAgriFinanceContest);

        rptAgriFinanceContest.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAgriFinanceContest($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAgriFinanceContest';


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
                report_id:'806037b1-fab0-4660-b281-e123979a43a3 ',
                workspace_Id :'364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'5bc8cf0b-4f7b-4dde-ab6a-475cc9e7925c',
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
                page_name: 'AgriFinance Contest',
                page_head: 'FY23 Contest'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAgriFinancecontroller', rptAgriFinancecontroller);

        rptAgriFinancecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAgriFinancecontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAgriFinancecontroller';


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
                report_id:'806037b1-fab0-4660-b281-e123979a43a3 ',
                workspace_Id :'364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'5bc8cf0b-4f7b-4dde-ab6a-475cc9e7925c',
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
                page_name: 'Agri Finance',
                page_head: 'FY23 Contest'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAgriFinancePortfolio', rptAgriFinancePortfolio);

        rptAgriFinancePortfolio.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAgriFinancePortfolio($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAgriFinancePortfolio';


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
                report_id: '9e80d166-984d-495e-bd9b-57a9b8282496',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'c2564ae6-9115-475c-9d4c-b5b91fefe97b',
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
                page_name: 'Agri Finance Portfolio',
                page_head: 'Finance'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAgriFinanceRevenueRecon', rptAgriFinanceRevenueRecon);

        rptAgriFinanceRevenueRecon.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAgriFinanceRevenueRecon($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAgriFinanceRevenueRecon';


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
                report_id: 'bd2abe77-781a-48f7-9079-1308282ce48c',
                workspace_Id :'364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'b09768de-f617-432e-8e4a-f2491c19673a',
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
                page_name: 'AgriFinance Revenue Recon',
                page_head: 'Finance'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAgriRMActualVsTarget', rptAgriRMActualVsTarget);

        rptAgriRMActualVsTarget.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAgriRMActualVsTarget($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAgriRMActualVsTarget';


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
                report_id: 'acd658d5-54c8-4321-99d8-753c9158609b',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'296deed5-cf37-4e10-8156-a1bbe9d54686',
                roles:'RM,Superuser'
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
                page_name: 'RM Target Vs Actual',
                page_head: 'Agri Enterprises'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptagrofintop20customers', rptagrofintop20customers);

        rptagrofintop20customers.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptagrofintop20customers($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptagrofintop20customers';


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
                report_id: '236db06f-fc69-48df-bd7c-901f11a167bc',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'4143cfb3-0c37-4887-addf-fc185c3c06d5',
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
                page_name: 'Agro & Fin - Top 20 customers',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAllDataModified', rptAllDataModified);

        rptAllDataModified.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAllDataModified($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAllDataModified';


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
                report_id: '4e7a1b2b-11cd-4190-8223-c15eba8305dd',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'74c0dbf9-e4f4-4edb-863a-3aead8208977',
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
                page_name: 'All Data Modified',
                page_head: 'Others'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAppAgriCommerce', rptAppAgriCommerce);

        rptAppAgriCommerce.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAppAgriCommerce($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAppAgriCommerce';


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
            lockUI();
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: '3267959e-0ede-46ed-8c09-4592ddef7598',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'bb25abab-1160-4bfc-9260-3e5dfd08f197',
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
                unlockUI();
                var report = powerbi.get(element);
                
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'AgriCommerce',
                page_head: 'Application Status'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAppAgriCommercestatus', rptAppAgriCommercestatus);

        rptAppAgriCommercestatus.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAppAgriCommercestatus($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAppAgriCommercestatus';


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
            lockUI();
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: '3267959e-0ede-46ed-8c09-4592ddef7598',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'bb25abab-1160-4bfc-9260-3e5dfd08f197',
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
                unlockUI();
                var report = powerbi.get(element);
                
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'AgriCommerce Status',
                page_head: 'Application Status'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAppAgriFinance', rptAppAgriFinance);

        rptAppAgriFinance.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAppAgriFinance($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAppAgriFinance';


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
            lockUI();
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: '5cf1c9b8-14d4-47cb-9cff-673126fdeaad',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'4ed94781-eb29-4056-a289-626cd9b1955c',
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
                unlockUI();
                var report = powerbi.get(element);
                
            });

            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'Agri Finance',
                page_head: 'Application Status'
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
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAppAgriFinancestatus', rptAppAgriFinancestatus);

        rptAppAgriFinancestatus.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAppAgriFinancestatus($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAppAgriFinancestatus';


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
            lockUI();
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: '5cf1c9b8-14d4-47cb-9cff-673126fdeaad',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'4ed94781-eb29-4056-a289-626cd9b1955c',
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
                unlockUI();
                var report = powerbi.get(element);
                
            });

            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'AgriFinance Status ',
                page_head: 'Application Status'
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
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAUMCuts', rptAUMCuts);

        rptAUMCuts.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAUMCuts($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAUMCuts';


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
                report_id: '89728bd1-c844-4d92-b868-8656ca3e02a9',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:"Empty",
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
                page_name: 'AUM Cuts',
                page_head: 'Others'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptAUMTrend', rptAUMTrend);

        rptAUMTrend.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptAUMTrend($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptAUMTrend';


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
                report_id: '40930b02-9f0c-46dc-bf25-1a4d89061653',
                workspace_Id :'364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'51a351b3-2fa4-4314-b679-5916aa3d7c57',
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
                page_name: ' AUM Trend',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptCohortsOverview', rptCohortsOverview);

        rptCohortsOverview.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptCohortsOverview($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptCohortsOverview';


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
                report_id: 'b5bdcf37-5352-4f62-9b8f-8f79f1fb2679',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'cf6e12cc-250e-4d75-9e4d-eb4839bdc555',
                roles:'RM, Manager, Superuser'
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
                page_name: 'Cohorts Overview',
                page_head: 'Customer Profile'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptCollectionsFeedback', rptCollectionsFeedback);

        rptCollectionsFeedback.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptCollectionsFeedback($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptCollectionsFeedback';


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
                page_name: 'Collections Feedback',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptCommodityViX', rptCommodityViX);

        rptCommodityViX.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptCommodityViX($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptCommodityViX';


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
                report_id: 'f992bae4-3707-4bd7-9c19-9a7ba0af9b16',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'cecc282a-347e-4bb9-b816-c21bba08867f',
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
                page_name: 'Commodity ViX',
                page_head: 'Agri Enterprises'
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptCovenantTracker', rptCovenantTracker);

        rptCovenantTracker.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptCovenantTracker($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptCovenantTracker';


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
                report_id: '9b9fa71c-d8ad-4625-9c9a-eb1996a8d38c',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'2b285ac0-bb9e-4bc7-9710-43176203a1ae',
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
                page_name: 'Covenant Tracker',
                page_head: 'Finance'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptCreditteam', rptCreditteam);

        rptCreditteam.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptCreditteam($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptCreditteam';


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
                report_id: 'f816c0d7-cbf9-414e-9820-c13ab1c31f13',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'dae4a8b7-b893-4eac-bd71-86fb009d9840',
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
                page_name: 'Credit team',
                page_head: 'Performance Metrics'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptCropSurveys', rptCropSurveys);

        rptCropSurveys.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function  rptCropSurveys($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptCropSurveys';


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
                report_id: 'ec9c9424-4129-40f8-a380-25a3b3b34ccb',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'2a944b1f-e9ae-41a7-9204-b65e14c17e3f',
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
                page_name: 'Crop Surveys',
                page_head: 'Research Desk'
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
 

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptCUProductivityScorecard', rptCUProductivityScorecard);

        rptCUProductivityScorecard.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptCUProductivityScorecard($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptCUProductivityScorecard';


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
                report_id: '31c8542a-59ee-4f33-92b1-f97132a904f3',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'a1e7a11a-1623-49c2-bf50-eb269832ae1d',
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
                page_name: 'CU Productivity Scorecard',
                page_head: 'Performance Metrics'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptCustomerRealtimeStatus', rptCustomerRealtimeStatus);

        rptCustomerRealtimeStatus.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptCustomerRealtimeStatus($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptCustomerRealtimeStatus';


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
            lockUI();
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: '76ddf38a-2c4d-4376-8946-8ac061b320b8',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'5ff0bc98-34ac-487d-99a1-82befd47d6ea',
                roles:'RM,Superuser'
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
                unlockUI();
                var report = powerbi.get(element);
                
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'Customer Realtime Status',
                page_head: 'Customer Profile'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptCustomerRMMapping', rptCustomerRMMapping);

        rptCustomerRMMapping.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptCustomerRMMapping($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptCustomerRMMapping';


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
            lockUI();
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            // var param = {
            //     report_id: 'f12da148-d884-4aec-b79d-e2fd5f2d49be',
            //     workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
            //     dataset:'57f7d3dd-310e-47ab-966c-e9614c944b61',
            //     roles:'RM'
            // }
            var param = {
                    report_id: 'f12da148-d884-4aec-b79d-e2fd5f2d49be',
                    workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                    dataset:'57f7d3dd-310e-47ab-966c-e9614c944b61',
                    roles:'RM,Superuser'
                }
           
            SocketService.getparams(url, param).then(function (resp) { 
                unlockUI();
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
                page_name: 'Customer RM Mapping',
                page_head: 'Others'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptDailyADMReport', rptDailyADMReport);

        rptDailyADMReport.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptDailyADMReport($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptDailyADMReport';


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
                report_id: '12d531e0-80d0-4f81-aa59-932200ed5d53',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:"Empty",
                roles: 'RM,Manager,Superuser'
                
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
                page_name: 'Account Statement',
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
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptdashboardcontroller', rptdashboardcontroller);

        rptdashboardcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptdashboardcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptdashboardcontroller';


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
                report_id: 'e13de545-8ebf-4a1c-a820-7893ea7def78',
                workspace_Id : '93595955-1de9-4abd-aaf8-1e2d10f33d43'
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
                page_name: 'Burea & Repayment Behaviour',
                page_head: 'Customer Profile'
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

 

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptdashboardcontroller', rptdashboardcontroller);

        rptdashboardcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptdashboardcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptdashboardcontroller';


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
                report_id: 'e13de545-8ebf-4a1c-a820-7893ea7def78',
                workspace_Id : '93595955-1de9-4abd-aaf8-1e2d10f33d43',
                dataset:'5fc125eb-a355-46a0-b0e7-57480ae50786',
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
        };
    };
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptDemandSchedule', rptDemandSchedule);

        rptDemandSchedule.$inject =['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptDemandSchedule($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptDemandSchedule';


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
                report_id: 'fa29dca3-7d3c-4322-91f9-ca7bad042935',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'16ac447d-0e33-488c-b1bc-5e77a0f24e36',
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
                page_name: 'Demand Schedule',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptDisbursementInternalAudit', rptDisbursementInternalAudit);

        rptDisbursementInternalAudit.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptDisbursementInternalAudit($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptDisbursementInternalAudit';


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
                report_id: 'dd26b496-4694-4f01-ab96-0f2dfb8a9ddc',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'ac400a05-2bf3-4162-af17-fea024172621',
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
                page_name: 'Disbursement - Internal Audit',
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptDisbursementProjection',rptDisbursementProjection);

        rptDisbursementProjection.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptDisbursementProjection($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptDisbursementProjection';


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
                report_id:'9ebe50a9-5a8e-4280-9b13-265dc4a270e2',
                workspace_Id :'364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'626fd844-39d1-4f04-872c-6357dac510c4',
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
                page_name: 'Disbursement Projection',
                page_head: 'Planning '
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptDisbursementReport', rptDisbursementReport);

        rptDisbursementReport.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptDisbursementReport($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptDisbursementReport';


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
                report_id: 'b3d2c363-aca0-4f16-bb2d-b5c0f23977ec',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'0f4749e8-8f2d-4aff-9037-b65a425f54bf',
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
                page_name: 'Disbursement Report',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptFPOScorecard', rptFPOScorecard);

        rptFPOScorecard.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptFPOScorecard($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptFPOScorecard';


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
                report_id: '8ebe4b27-e0b4-4286-9418-19168a455bae',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'637b96d7-3427-4369-b48a-838a270900cf',
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
                page_name: 'FPO Scorecard',
                page_head: 'Farmer Network'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptFutureCashFlow', rptFutureCashFlow);

        rptFutureCashFlow.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptFutureCashFlow($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptFutureCashFlow';


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
                report_id: '4c6634c8-9078-403e-b341-eebe63804e17',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'80a9738f-7a25-465b-b0a9-639458ec6fa4',
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
                page_name: 'Future Cash Flow',
                page_head: 'Finance'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptFY23Target', rptFY23Target);

        rptFY23Target.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptFY23Target($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptFY23Target';


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
                report_id:'1121f4b7-95e8-4d1e-80a1-a093f702a631',
                workspace_Id :'364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'2db18ca1-4b8a-4357-85b5-70d1d9286e5f',
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
                page_name: 'FY23 Target',
                page_head: 'Planning '
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptGTVandYield', rptGTVandYield);

        rptGTVandYield.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptGTVandYield($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptGTVandYield';


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
                report_id: '50cb7e4e-2396-47ae-87b3-080cb0f61ebc',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'d392af67-eabd-4470-9fdf-341892ad6d9e',
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
                page_name: 'AgriFinance Summary',
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptHighExposureClients', rptHighExposureClients);

        rptHighExposureClients.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptHighExposureClients($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptHighExposureClients';


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
                report_id: '8cdd92df-6f68-49d0-9151-99151f2a7001',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'9b988da1-6b4b-44e5-8065-16c8b3c01766',
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
                page_name: 'High Exposure clients ',
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rpthighmarkcontroller', rpthighmarkcontroller);

        rpthighmarkcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rpthighmarkcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rpthighmarkcontroller';


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
            lockUI();
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: 'dcc05e68-07f1-41b7-a67a-8a986a775d9c',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d'
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
                unlockUI();
                var report = powerbi.get(element);
                
            });
        };
    };
})();

 

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rpthighmarkcontroller', rpthighmarkcontroller);

        rpthighmarkcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rpthighmarkcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rpthighmarkcontroller';


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
            lockUI();
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: '2884ac56-cb54-46ed-bbd2-4337528889f3',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'2eb7c9b2-c3dd-4e56-8c89-9c3ae6d5b65a',
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
                unlockUI();
                var report = powerbi.get(element);
                
            });

            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'Burea & Repayment Behaviour',
                page_head: 'Customer Profile'
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptHistoricalPortfolioQuality', rptHistoricalPortfolioQuality);

        rptHistoricalPortfolioQuality.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptHistoricalPortfolioQuality($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptHistoricalPortfolioQuality';


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
            lockUI();
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: 'd217fc99-b990-4d0c-bf87-a67aee209553',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'12194ed5-2989-471f-a302-e5eace6b8933',
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
                unlockUI();
                var report = powerbi.get(element);
                
            });

            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'Historical Portfolio Quality',
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptInsightsLoginLog', rptInsightsLoginLog);

        rptInsightsLoginLog.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptInsightsLoginLog($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptInsightsLoginLog';


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
                report_id: '8dc1ce0b-3767-4564-a17e-776d82ffc450',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'164358e6-2fb7-43f7-98df-31bc848b458a',
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
                page_name: 'Insights Login Log',
                page_head: 'Performance Metrics'
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



(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptKMPPerformance', rptKMPPerformance);

        rptKMPPerformance.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptKMPPerformance($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptKMPPerformance';


        activate();

        function activate() {
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: 'abda9c43-9058-4663-9f16-c4353900a61d',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'d4a9de4b-65f4-4781-af8d-caed8a98acd1',
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
                page_name: 'KMP Performance',
                page_head: 'Performance Metrics'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptLeadManagement', rptLeadManagement);

        rptLeadManagement.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptLeadManagement($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptLeadManagement';


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
                report_id: 'dcb3a816-e3f6-4819-96aa-fb0593e02728',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'37a7c94f-14a6-4d77-a8ec-d4c92ada4848',
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
                page_name: 'Lead Management',
                page_head: 'Customer cohorts'
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptLeads', rptLeads);

        rptLeads.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptLeads($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptLeads';


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
                report_id: 'dcb3a816-e3f6-4819-96aa-fb0593e02728',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'37a7c94f-14a6-4d77-a8ec-d4c92ada4848',
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
                page_name: 'Leads',
                page_head: 'Lead Management'
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptmisPipelinePlanner', rptmisPipelinePlanner);

        rptmisPipelinePlanner.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptmisPipelinePlanner($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptmisPipelinePlanner';


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
                report_id:'1caec33f-824d-4626-b895-91e60c66a57b',
                workspace_Id :'364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'d93833b4-4274-452c-aa1c-4dbccb1e6a99',
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
                page_name: 'Pipeline Planner',
                page_head: 'Planning '
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptMLWHStockSummary', rptMLWHStockSummary);

        rptMLWHStockSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptMLWHStockSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptMLWHStockSummary';


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
                report_id:'e8e545ef-220c-442b-970e-25e3d2c7354b',
                workspace_Id :'364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'bcb7737b-377b-4a72-8836-ab3921bbe9f9',
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
                page_name: 'ML WH Stock Summary',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptOTRR', rptOTRR);

        rptOTRR.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptOTRR($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptOTRR';


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
            lockUI();
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: 'ee85028a-5f64-439b-a43d-6ca6e48fc651',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'645429ff-c742-448c-af12-68480928bcb7',
                roles:'RM,Superuser'
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
                unlockUI();
                var report = powerbi.get(element);
                
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'OTRR',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptPARProfile', rptPARProfile);

        rptPARProfile.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptPARProfile($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptPARProfile';


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
            lockUI();
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: 'a255b27d-9423-4f76-bb6a-740c0069cfda',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'2f83ab36-7455-482f-855d-8f143eb27183',
                roles:'RM,Superuser'
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
                unlockUI();
                var report = powerbi.get(element);
                
            });

            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'Portfolio Profile',
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptPATMarginsFT', rptPATMarginsFT);

        rptPATMarginsFT.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptPATMarginsFT($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptPATMarginsFT';


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
                report_id: 'dc1394b4-f2f4-496b-9303-1038c3ca9bc6',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'15f63178-165c-411a-9a30-0b67a9baf415',
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
        };
    };
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptPerformance', rptPerformance);

        rptPerformance.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptPerformance($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptPerformance';


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
                report_id: '18863ea8-9661-4d5c-875e-aa9dba2d0829',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'f3dcf10a-62cf-487b-b662-a0029e84e127',
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
                page_name: 'RM Scorecard ',
                page_head: 'Farmer Network'
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptPerformanceOverview', rptPerformanceOverview);

        rptPerformanceOverview.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptPerformanceOverview($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptPerformanceOverview';


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
                report_id: '64764517-d290-48ac-a2d3-5eaf720fc235',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'1f1202a5-1be9-4dbd-adfc-c14c5b5c789b',
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
                page_name: 'Performance Overview',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptPlaceholder1', rptPlaceholder1);

        rptPlaceholder1.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptPlaceholder1($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptPlaceholder1';


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
                report_id: '09fc14fc-7519-40b6-b714-09347053d5c0',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'c75d2eed-0f1a-457f-953f-02031f90df7f',
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
                page_name: 'Placeholder 1',
                page_head: 'PlaceHolder'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptPlaceholder2', rptPlaceholder2);

        rptPlaceholder2.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptPlaceholder2($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptPlaceholder2';


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
                report_id: '7d026526-a13d-4a7c-a823-af7c79d6f6b6',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'f11438c6-ee32-4bbb-99c2-154cd0078a21',
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
                page_name: 'Placeholder 2',
                page_head: 'PlaceHolder'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptPlaceholder3', rptPlaceholder3);

        rptPlaceholder3.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptPlaceholder3($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptPlaceholder3';


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
                report_id: 'f7e27e11-3972-48b9-b772-af3b0442f476',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'0330e1d7-364d-412b-b8e4-5758fe522ed5',
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
                page_name: 'Placeholder 3',
                page_head: 'PlaceHolder'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptPlaceholder4', rptPlaceholder4);

        rptPlaceholder4.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptPlaceholder4($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptPlaceholder4';


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
                report_id: '2f2969ec-f532-4e1f-bf0c-7d2a20155802',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'c5bbcc3f-8fd1-45e0-8572-eb850b90ad74',
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
                page_name: 'Placeholder 4',
                page_head: 'PlaceHolder'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptPlaceholder6', rptPlaceholder6);

        rptPlaceholder6.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptPlaceholder6($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptPlaceholder6';


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
                report_id: '0a7db06e-a160-4ebe-871c-7238d7ac1209',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'5fcbe3e0-add1-4c90-a638-04739f57e3cb',
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
                page_name: 'Placeholder 6',
                page_head: 'PlaceHolder'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptPortfolio', rptPortfolio);

        rptPortfolio.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptPortfolio($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptPortfolio';


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
                report_id: '981dc80f-c37b-4ff2-a944-f9e2b47905a5',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'0c615294-8600-49fe-97f5-63d5caf50082',
                roles:'RM,Superuser'
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
                page_name: 'Program/Product',
                page_head: 'Farmer Network'
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

 

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptProduct', rptProduct);

        rptProduct.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptProduct($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptProduct';


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
                report_id: '355d9f3f-14c6-4da7-b3c8-c5f681bfcd17',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'df114afa-4309-48a2-95e3-17a45f3168ac',
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
                page_name: 'Program/Product ',
                page_head: 'AgriEnterprises'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptRepaymentEfficiency', rptRepaymentEfficiency);

        rptRepaymentEfficiency.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptRepaymentEfficiency ($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptRepaymentEfficiency';


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
            lockUI();
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: '6e04e499-931c-4270-a59a-443eb1610c68',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'578542ee-5119-448d-b8a3-744f9b37d40a',
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
                unlockUI();
                var report = powerbi.get(element);
                
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'Repayment Efficiency',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptRepaymentReport', rptRepaymentReport);

        rptRepaymentReport.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptRepaymentReport($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptRepaymentReport';


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
            lockUI();
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: 'bfbf8ab4-1ae4-40fe-9431-d165dea14172',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'cf720a47-c542-4646-8446-c94969ea60d9',
                roles:'RM, Manager, Superuser'
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
                unlockUI();
                var report = powerbi.get(element);
                
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'Repayment Report',
                page_head: 'Agri Finance'
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });


        };
    };
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptrepaymentStatement', rptrepaymentStatement);

        rptrepaymentStatement.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptrepaymentStatement($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptrepaymentStatement';


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
                report_id: '41b542c6-f17b-4e97-b271-bef11fe183e3',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:"Empty",
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
                page_name: 'Repayment Statement',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptRepaymentsWaivers', rptRepaymentsWaivers);

        rptRepaymentsWaivers.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptRepaymentsWaivers($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptRepaymentsWaivers';


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
                report_id: '153dc01d-7449-48c4-ba47-ae35f7b1c389',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'7bac47d6-a236-4d7b-9ab4-92aeabf52605',
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
                page_name: 'Repayments & Waivers',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptreport3', rptreport3);

        rptreport3.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptreport3($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptreport3';


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
                report_id: '2abfe21e-d20c-46df-8142-d12b1ccd3f2d',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d'
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
        };
    };
})();

 

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptReports', rptReports);

        rptReports.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function  rptReports($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptReports';


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
                report_id: 'ec9c9424-4129-40f8-a380-25a3b3b34ccb',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'2a944b1f-e9ae-41a7-9204-b65e14c17e3f',
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
                page_name: 'rptReports',
                page_head: 'Research Desk'
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
 

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptRHPerformance', rptRHPerformance);

        rptRHPerformance.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptRHPerformance($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptRHPerformance';


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
                report_id: 'ed6581d4-c218-4f03-8f04-814ecdd8fcce',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'ff5a3931-4f48-4f4f-b95a-dd1e7ae82a1a',
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
                page_name: 'RH Performance',
                page_head: 'Performance Metrics'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptRMActualVsTarget', rptRMActualVsTarget);

        rptRMActualVsTarget.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptRMActualVsTarget($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptRMActualVsTarget';


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
                report_id: 'dee70aaa-20ef-4346-b11b-36664a587ca8',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'edfb5548-6785-4823-9299-a9fe01be2823',
                roles:'RM,Superuser'
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
                page_name: 'RM Target Vs Actual',
                page_head: 'Farmer Network'
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptRMPerformance', rptRMPerformance);

        rptRMPerformance.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptRMPerformance($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptRMPerformance';


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
                report_id: 'acd658d5-54c8-4321-99d8-753c9158609b',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'296deed5-cf37-4e10-8156-a1bbe9d54686',
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
                page_name: 'RM Performance',
                page_head: 'Performance Metrics'
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptRMProfile', rptRMProfile);

        rptRMProfile.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptRMProfile($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptRMProfile';


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
                report_id: 'cfb9c47a-c730-4b28-bd37-d120aaa8a5ff',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'15fdce90-7a84-4a55-9403-e09a53fc9517',
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
                page_name: 'RM Profile',
                page_head: 'Performance Metrics'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptsamgdashboard', rptsamgdashboard);

        rptsamgdashboard.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptsamgdashboard($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptsamgdashboard';


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
                report_id: '10dd179a-55dc-436e-8a11-5e27e3d321ff',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'d9f91e62-a4bd-40f6-8422-c671400cbeeb',
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
                page_name: 'SAMG',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptSBIcontroller',rptSBIcontroller);

        rptSBIcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptSBIcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title ='rptSBIcontroller';


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
                report_id:'ba11f7ec-4522-4c49-a7bb-71c5e1ba2083',
                workspace_Id :'364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'eac6eb82-5cea-409a-908c-470e46c65688',
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
                page_name: 'SBI',
                page_head: 'Colending'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptServiceRequestsPerformance', rptServiceRequestsPerformance);

        rptServiceRequestsPerformance.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptServiceRequestsPerformance($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptServiceRequestsPerformance';


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
                report_id: '09d967ea-8591-4af7-9a47-c8bf0a447410',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'58d4cece-481c-43cd-8e0c-20b6a028ab6a',
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
                page_name: 'Service Request Performance',
                page_head: 'Others'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptSettlementReport', rptSettlementReport);

        rptSettlementReport.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptSettlementReport($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptSettlementReport';


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
                report_id: 'be1f27fb-be4d-4ac5-ad26-1cda653dc184',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'584e2c81-8af8-4c4f-a822-cfd09e0a513f',
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
                page_name: 'Settlement Report',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptSpilloverPortfolioQuality', rptSpilloverPortfolioQuality);

        rptSpilloverPortfolioQuality.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptSpilloverPortfolioQuality($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptSpilloverPortfolioQuality';


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
                report_id: '84f7afd1-b525-4ed1-8f10-4ba4adc8836a',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'256597fa-a011-446f-834c-8ee14e46987d',
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
                page_name: 'Spillover Portfolio Quality',
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptSTFMLEXIMReports', rptSTFMLEXIMReports);

        rptSTFMLEXIMReports.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptSTFMLEXIMReports($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptSTFMLEXIMReports';


        activate();

        function activate() {
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: '50378cdb-841f-4af8-bd82-c58595470ca4',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'06c8c3e8-7cac-4b2c-9b93-dadc57f6736f',
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
                page_name: 'STF/ ML / EXIM Reports',
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptSTFMTMReport', rptSTFMTMReport);

        rptSTFMTMReport.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptSTFMTMReport($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptSTFMTMReport';


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
            report_id: 'f3b57f52-e9aa-4a54-bcff-c882eb24ea69',
            workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
            dataset:'eb7444b2-5a13-4629-97d2-3560819a53c7',
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
                page_name: 'STF MTM Report',
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptSTFWHStockSummary', rptSTFWHStockSummary);

        rptSTFWHStockSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptSTFWHStockSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptSTFWHStockSummary';


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
                report_id: '50378cdb-841f-4af8-bd82-c58595470ca4',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'06c8c3e8-7cac-4b2c-9b93-dadc57f6736f',
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
                page_name: 'STF WH Stock Summary',
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


//RESEARCH DESK

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptSubscriptions', rptSubscriptions);

        rptSubscriptions.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptSubscriptions($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptSubscriptions';


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
                report_id: '3ffb2d8c-640d-41f0-b57f-ee781f72597d',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'fb832b80-ac47-4ccc-b97a-2dd906fee7cc',
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
                page_name: 'Subscriptions',
                page_head: 'Research Desk'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptTeamHierarchy', rptTeamHierarchy);

        rptTeamHierarchy.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptTeamHierarchy($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptTeamHierarchy';


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
                report_id: 'd7937f81-e23e-4f72-bf94-516963c011d3',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'d6e19fa3-93c8-4100-adc4-3fc93ac08ee8',
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
                page_name: 'Team Hierarchy',
                page_head: 'Others'
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptTeleCaller', rptTeleCaller);

        rptTeleCaller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptTeleCaller ($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptTeleCaller';


        activate();

        function activate() {
            lockUI();
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: '77d4cfe4-8a33-4f04-928b-530b4aff2ee1',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'254e1541-023d-499d-b697-15deed56fbc9',
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
                unlockUI();
                var report = powerbi.get(element);
                
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'TeleCaller',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptTrnServiceRequestAdd', rptTrnServiceRequestAdd);

    rptTrnServiceRequestAdd.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];


    function rptTrnServiceRequestAdd($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rptTrnServiceRequestAdd';
        var lspage = 'myrequest';
        activate();
        

        function activate() {
           
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'Help Desk',
                page_head: 'Help'
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });


            // $scope.cbodepartment.department_name = 'In sights - Data Correction';
            var url = 'api/OsdTrnServiceRequest/GetEmployees';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
                unlockUI();
            });

            var url = 'api/OsdTrnServiceRequest/tempdelete';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/OsdMstDepartmentManagement/GetActivaterequestdept';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.deptmasterlist = resp.data.deptlist;

                

                for(var i=0; i<=$scope.deptmasterlist.length; i++)
            {
                if($scope.deptmasterlist[i].department_name == "Insights - Data Correction")
                {
                    $scope.cbodepartment =$scope.deptmasterlist[i];
                    onstartdept($scope.cbodepartment);
                    break;
                }
            }
            
                unlockUI();
                
            });
            
            // $scope.cbodepartment = 'Insights - Data Correction';
            //Insights - Data Correction
        }
        function onstartdept(department_gid)
        {
            var params = {
                department_gid: department_gid.department_gid
            }         
            var url = 'api/OsdMstDepartmentManagement/GetDeptActivityList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.activity_list = resp.data.activitydtl;
            });
        }

        $scope.titlename = function (string) {
            if (string.length >= 150) {
                $scope.message = "Maximum 150 characters Length";
            }
            else {
                $scope.message = ""
            }
        }
        $scope.onselectdept = function (department_gid) {
            var params = {
                department_gid: department_gid.department_gid
            }         
            var url = 'api/OsdMstDepartmentManagement/GetDeptActivityList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.activity_list = resp.data.activitydtl;
            });
        }

        $scope.submitserviceRequest = function () {
            lockUI();
            var params = {
                activitymaster_gid: $scope.cboactivity.activitymaster_gid,
                activity_name: $scope.cboactivity.activity_name,
                request_title: $scope.request_title,
                request_description: $scope.request_description,
                tagmemberdtl: $scope.tagmember_list,
                department_gid: $scope.cbodepartment.department_gid,
                department_name: $scope.cbodepartment.department_name
            }


            var url = "api/OsdTrnServiceRequest/PostServiceRequest"
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.osdTrnServiceRequestSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });
           
        }


        $scope.ServiceRequestDocumentUpload = function () {
            lockUI();
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {

                //document.getElementById('fp').innerHTML =
                //    'Total Files: <b>' + fi.files + '</b></br >';

                var frm = new FormData();
                //frm.append('allocationdtl_gid', 'RSK001')
                for (var i = 0; i <= fi.files.length - 1; i++) {

                    frm.append(fi.files[i].name, fi.files[i])
                    $scope.uploadfrm = frm;
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                    //document.getElementById('fp').innerHTML =
                    //    document.getElementById('fp').innerHTML + '<br /> ' + fname;
                }
                var url = 'api/OsdTrnServiceRequest/RequestDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');
                    $scope.upload_list = resp.data.upload_list;
                    $scope.lblfilename = resp.data.filename;
                    $scope.lblfilepath = resp.data.filepath;
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
        }

        $scope.uploaddocumentcancel = function (tmp_documentGid) {
            lockUI();
            var params = {
                tmp_documentGid: tmp_documentGid
            }
            var url = 'api/OsdTrnServiceRequest/GettmpDocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.upload_list = resp.data.upload_list;
                $scope.lblfilename = resp.data.filename;
                $scope.lblfilepath = resp.data.filepath;
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                unlockUI();
            });
        }

        //$scope.downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}
        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.downloadall = function (val1, val2) {
            for (var i=0;i < val2.length; i++) 
            {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }

        // Back Code Starts
        $scope.cancel = function () {
            if (lspage == "myrequest") {
                $location.url('app/osdTrnServiceRequestSummary');
            }
            else if (lspage == "taggedrequest") {
                $location.url('app/osdTrnTaggedRequestSummary');
            }
            else if (lspage == "forwardrequest") {
                $location.url('app/osdTrnForwardTransferSummary');
            }
            else if (lspage == "reopenactivity") {
                $location.url('app/osdTrnReopenRequestSummary');
            }
            else if (lspage == "closerequest") {
                $location.url('app/osdTrnCloseRequestSummary');
            }
        }
        // Back Code Ends

  
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rptYOYAEDisbursementSummary', rptYOYAEDisbursementSummary);

        rptYOYAEDisbursementSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function rptYOYAEDisbursementSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'rptYOYAEDisbursementSummary';


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
                report_id: 'b4776d8d-4f7f-417b-9f3f-14b5575032e6',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'f0a05e94-1ca7-43c3-a14c-dc05b16529c1',
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
                page_name: 'YOY AE Disbursement Summary',
                page_head: 'Agri Enterprises'
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
